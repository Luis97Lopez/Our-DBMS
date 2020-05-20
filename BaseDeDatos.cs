using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace proyecto_BDA
{
    class BaseDeDatos
    {
        /**
          * Clase que permite manejar y coleccionar archivos en
          * un repositorio (carpeta). 
          *   ________________________
          *  /                        \
          * |                          |
          * |\________________________/|
          * |                          |
          * |\________________________/|
          * |                          |
          * |\________________________/|
          * |                          |
          * \__________________________/
          *
          **/
        private string nombreBaseDeDatos;
        // Nombre de la carpeta con los archivos de datos.
        public string NombreBaseDeDatos
        {
            // Obtiene el nombre de la base de datos.
            get => nombreBaseDeDatos;

            // Modifica el nombre de la base de datos.
            // Si éste cambia, se debe de renombrar el
            // directorio.
            set
            {
                Directory.Move(nombreBaseDeDatos, value);
                nombreBaseDeDatos = value;
                //LeeTablas();
            }
        }

        // Almacén de datos.
        public DataSet Set { get; set; }

        /**
         * Crea una nueva base de datos y se le asigna su
         * nombre correspondiente. Si no existe, crea una
         * nueva, de lo contrario, lee la base de datos
         * existente.
         **/

        public BaseDeDatos(string nombre)
        {
            nombreBaseDeDatos = nombre;
            Set = new DataSet(nombre);

            if (Directory.Exists(NombreBaseDeDatos))
            {
                // Ya existe una base de datos con ese nombre.
                LeeTablas();
                if(Set.Tables.Count > 0 )
                    LeeRelaciones();
            }
            else
                // No existe, por lo tanto, se crea una nueva.
                Directory.CreateDirectory(NombreBaseDeDatos);
        }

        /**
         * Lee el conjunto de tablas que contiene la base de datos. 
         **/
        private void LeeTablas()
        {
            // Obtiene un listado del nombre de los archivos de datos
            // de cada tabla en la base de datos.
            var archivosDeDatos = Directory.EnumerateFiles(NombreBaseDeDatos, "*.dat", SearchOption.AllDirectories);

            foreach (var archivoDeDatos in archivosDeDatos)
            {
                // Carga los archivos de datos correspondientes a cada
                // tabla.
                using (var archivoDAT = new FileStream(archivoDeDatos, FileMode.Open))
                {
                    var deserializador = new BinaryFormatter();
                    Set.Tables.Add((DataTable)deserializador.Deserialize(archivoDAT));
                }
            }
        }

        /**
         * Lee el conjunto de relaciones que pueden existir en cada
         * una de las tablas. 
         **/
        private void LeeRelaciones()
        {
            // Obtiene un listado del nombre de los archivos índices
            // que representan una relación en la base de datos.
            var archivosIDX = Directory.EnumerateFiles(NombreBaseDeDatos, "*.idx", SearchOption.AllDirectories);


            foreach (var archivoIDX in archivosIDX)
                // Carga las relaciones a la base de datos.
                try
                {
                    Set.Relations.Add(LeeArchivoRelacion(archivoIDX));
                }
                catch
                {
                    Console.WriteLine("Excepción Generada: Error en lectura de Relación");
                }
        }

        /**
         * Agrega una tabla nueva a la base de datos, siempre y
         * cuando no exista una con el mismo nombre.
         **/
        public void AgregaTabla(string nomTabla)
        {
            DataTable tablaNueva = new DataTable(nomTabla);
            Set.Tables.Add(tablaNueva);
            nomTabla = NombreBaseDeDatos + "\\" + nomTabla + ".dat";
            GuardaArchivoDeDatos(tablaNueva, nomTabla);
        }

        /**
         * Modifica el nombre de una tabla de la base de datos
         **/
        public void ModificaNombreTabla(string nomTablaAnterior, string nomTablaNueva)
        {
            var tablas = Set.Tables;

            tablas.Add(nomTablaNueva);
            tablas[nomTablaNueva].Merge(tablas[nomTablaAnterior]);

            EliminaTabla(nomTablaAnterior);
            GuardaArchivoDeDatos(tablas[nomTablaNueva], NombreBaseDeDatos + "\\" + nomTablaNueva + ".dat");
        }

        /**
         * Elimina la tabla de la base de datos
         **/
        public void EliminaTabla(string nomTabla)
        {
            Set.Tables.Remove(nomTabla);
            File.Delete(NombreBaseDeDatos + "\\" + nomTabla + ".dat");
        }

        /**
         * Devuelve la restricción en la que se encuentra el atributo
         **/
        private DataRelation GetRelation(string nomAtributo)
        {
            foreach (DataRelation item in Set.Relations)
            {
                if (item.ParentColumns[0].ColumnName == nomAtributo)
                    return item;
            }
            return null;
        }

        /**
         * Agrega un atributo nuevo a una tabla de la base de datos,
         * siempre y cuando a la tabla no se le haya agregado ningún
         * registro.
         **/
        public bool AgregaAtributo(string nomTabla, DataColumn columna)
        {
            var tablas = Set.Tables;
            bool pk = false;

            if (columna.Unique)
                if (tablas[nomTabla].PrimaryKey.Length == 0)
                {
                    pk = true;
                }
                else
                {
                    throw new DuplicatePrimaryKeyException();
                }

            tablas[nomTabla].Columns.Add(columna);

            if(pk)
                AgregaLlavePrimaria(nomTabla, columna);

            GuardaArchivoDeDatos(tablas[nomTabla], NombreBaseDeDatos + "\\" + nomTabla + ".dat");
            return true;
        }

        /**
         * Modifica un atributo en la tabla actual de esta base de 
         * datos, siempre y cuando no se haya eliminado uno
         * anteriormente.
         **/
        public void ModificaAtributo(string nomTabla, string nomAtributo, DataColumn atributoNuevo)
        {
            var tablas = Set.Tables;

            if (atributoNuevo.Unique && tablas[nomTabla].PrimaryKey.Length > 0 &&
                tablas[nomTabla].PrimaryKey[0].ColumnName != nomAtributo)
                throw new DuplicatePrimaryKeyException();

            try
            {
                EliminaAtributo(nomTabla, nomAtributo);
            }
            catch (ArgumentException error)
            {
                if (!atributoNuevo.Unique)
                    throw error;
                else if (atributoNuevo.DataType != tablas[nomTabla].PrimaryKey[0].DataType)
                    throw error;
                else if (atributoNuevo.MaxLength != tablas[nomTabla].PrimaryKey[0].MaxLength)
                    throw error;
                else
                {
                    tablas[nomTabla].Columns[nomAtributo].ColumnName = atributoNuevo.ColumnName;
                    GuardaArchivoDeDatos(tablas[nomTabla], NombreBaseDeDatos + "\\" + nomTabla + ".dat");
                    GuardaArchivoIndice(GetRelation(atributoNuevo.ColumnName));
                }
                return;
            }
            AgregaAtributo(nomTabla, atributoNuevo);
        }

        /**
         * Elimina un atributo y bloquea la posibilidad de poder modificarlo en un futuro.
         **/
        public void EliminaAtributo(string nomTabla, string nomAtributo)
        {
            var tablas = Set.Tables;
            //Constraint constraint = GetConstraint(nomTabla, nomAtributo);

            // Verifica que el atributo no pertenezca a la clave primaria,
            // de ser así se vacía el campo PrimaryKey. Al hacer esto, au-
            // tomáticamente se eliminan las restricciones (Constraints) a
            // las que pertenezca la clave primaria.
            if (tablas[nomTabla].PrimaryKey.Length == 1 && tablas[nomTabla].PrimaryKey[0].ColumnName.Equals(nomAtributo))
            {
                try
                {
                    tablas[nomTabla].PrimaryKey = null;
                }
                catch(ArgumentException)
                {
                    tablas[nomTabla].PrimaryKey = new DataColumn[] { tablas[nomTabla].Columns[nomAtributo] };
                    throw new ArgumentException("No se puede eliminar atributo porque es parte de una relación con una llave foránea.");
                }
            }

            // Si este atributo contiene alguna relación con otra tabla, 
            // se elimina.
            if (Set.Relations.Contains(nomAtributo))
            {
                Set.Relations.Remove(nomAtributo);
                tablas[nomTabla].Constraints.Remove(nomAtributo);
                File.Delete(NombreBaseDeDatos + "\\" + nomAtributo + ".idx");
            }

            // Se elimina el atributo y se guarda el nuevo estado de la
            // tabla en el archivo de datos.
            tablas[nomTabla].Columns.Remove(nomAtributo);
            GuardaArchivoDeDatos(tablas[nomTabla], NombreBaseDeDatos + "\\" + nomTabla + ".dat");
        }

        /**
         * Verifica que no exista una llave primaria.
         **/
        public bool ContieneLlavePrimaria(string nomTabla) => Set.Tables[nomTabla].PrimaryKey.Length != 0;

        /**
         * Agrega una llave primaria a una tabla.
         **/
        public void AgregaLlavePrimaria(string nomTabla, DataColumn llavePrimaria)
        {
            var tablas = Set.Tables;
            tablas[nomTabla].PrimaryKey = new DataColumn[] { llavePrimaria };
            GuardaArchivoDeDatos(tablas[nomTabla], NombreBaseDeDatos + "\\" + nomTabla + ".dat");
        }

        /**
         * Agrega una llave foránea, asociando una tabla con otra.
         **/
        public void AgregaLlaveForanea(DataColumn llavePrimaria, DataColumn llaveForanea)
        {
            string nomLlave = llaveForanea.ColumnName;

            try
            {
                DataRelation relacion = new DataRelation(nomLlave, llavePrimaria, llaveForanea);
                Set.Relations.Add(relacion);
                GuardaArchivoIndice(relacion);
            }
            catch (Exception error)
            {
                throw new Exception("No se pudo crear relación entre tablas.");
            }
            
        }

        /**
        * Lee una llave foránea, de un archivo IDX.
        **/
        public DataRelation LeeArchivoRelacion(string nomArchivo)
        {
            var tablas = Set.Tables;
          
            // Crea un diccionario que obtendrá la información de
            // la relación.
            var infoRelacion = new Dictionary<string, string>();

            // Lee el archivo .idx correspondiente a la relación.
            using (var archivoIDX = new FileStream(nomArchivo, FileMode.Open))
            {
                var deserializador = new BinaryFormatter();
                infoRelacion = (Dictionary<string, string>)deserializador.Deserialize(archivoIDX);
            }

            // Obtiene la columna que representa a la llave primaria, 
            // usando los metadatos del diccionario.
            DataColumn llavePrimaria = tablas[infoRelacion["tabla primaria"]].Columns[infoRelacion["clave primaria"]];

            // Obtiene la columna que representa a la llave foránea, 
            // usando los metadatos del diccionario.
            DataColumn llaveForanea = tablas[infoRelacion["tabla foranea"]].Columns[infoRelacion["clave foranea"]];
            
            return new DataRelation(infoRelacion["nombre relacion"], llavePrimaria, llaveForanea);
        }

        /**
         * Agrega un registro a una tabla.
         **/
        public void AgregaRegistro(string nomTabla, object[] registro)
        {
            var tablas = Set.Tables;
            tablas[nomTabla].Rows.Add(registro);
            GuardaArchivoDeDatos(tablas[nomTabla], NombreBaseDeDatos + "\\" + nomTabla + ".dat");
        }

        /**
         * Guarda/Actualiza el archivo de datos en la base de
         * datos.
         **/
        private void GuardaArchivoDeDatos(DataTable tabla, string nombreTabla)
        {
            using (var archivoDAT = new FileStream(nombreTabla, FileMode.OpenOrCreate))
            {
                var serializador = new BinaryFormatter();
                serializador.Serialize(archivoDAT, tabla);
            }
        }

        /**
         * 
         * 
         * */
         private void GuardaArchivoIndice(DataRelation relation)
        {

            // Crea un diccionario con la información que va a almacenarse.
            var informacion = new Dictionary<string, string>()
            {
                // El nombre de la relación de la clave foránea con otra
                // tabla.
                ["nombre relacion"] = relation.RelationName,

                // El nombre de la tabla a la que pertenece la clave 
                // primaria.
                ["tabla primaria"] = relation.ParentTable.TableName,

                // El nombre de la tabla a la que pertenece la clave 
                // foránea.
                ["tabla foranea"] = relation.ChildTable.TableName,

                // El nombre de la columna que representa la clave
                // primaria.
                ["clave primaria"] = relation.ParentColumns[0].ColumnName,

                // El nombre de la columna que representa la clave
                // foránea.
                ["clave foranea"] = relation.ChildColumns[0].ColumnName
            };

            // Guarda los metadatos del diccionario en un archivo
            // índice.
            string nomArchivo = NombreBaseDeDatos + "\\" + relation.RelationName + ".idx";

            using (var archivoIDX = new FileStream(nomArchivo, FileMode.OpenOrCreate))
            {
                var serializador = new BinaryFormatter();
                serializador.Serialize(archivoIDX, informacion);
            }
        }

        /**
         * Obtiene las columnas que representan a una
         * tabla en la base de datos.
         **/ 
        public DataTable ObtenAtributos(string nomTabla)
        {
            var tabla = Set.Tables[nomTabla];

            DataTable tablaDatos = new DataTable();
            string[] nomColumnas = { "Nombre", "Tipo de Dato", "Longitud", "Tipo de Llave" };

            foreach (var nomColumna in nomColumnas)
                tablaDatos.Columns.Add(nomColumna);

            foreach (DataColumn atributo in tabla.Columns)
            {
                DataRow dataRow = tablaDatos.NewRow();
                dataRow["Nombre"] = atributo.ColumnName;
                dataRow["Tipo de Dato"] = atributo.DataType;
                dataRow["Longitud"] = atributo.MaxLength == - 1 ? "4" : atributo.MaxLength.ToString();

                if (tabla.Constraints.Contains(atributo.ColumnName))
                {
                    string nomLlave = atributo.ColumnName;
                    dataRow["Tipo de Llave"] = "Foranea";
                } 
                else if (tabla.PrimaryKey.Length == 1
                        && tabla.PrimaryKey[0].ColumnName.Equals(atributo.ColumnName))
                    dataRow["Tipo de Llave"] = "Primaria";
                else
                    dataRow["Tipo de Llave"] = "Sin Llave";

                tablaDatos.Rows.Add(dataRow);
            }

            return tablaDatos;
        }

        /**
         * Obtiene el nombre de las columnas de una tabla.
         **/
        private string[] NomColumnas(string nomTabla)
        {
            var tabla = Set.Tables[nomTabla];
            string[] nombres = new string[tabla.Columns.Count];

            for (int i = 0; i < tabla.Columns.Count; i++)
                nombres[i] = tabla.Columns[i].ColumnName;

            return nombres;
        }

        /**
         * Obtiene una tabla nueva, en base a una sentencia SQL.
         **/
        private DataTable CreaTablaConsulta(string nomTabla, string[] nomColumnas, GroupCollection argumentos)
        {
            // Operador de selección. Ejemplo: >, <, >=, <=, =, <>
            string operador = argumentos["operador"].Value;

            // Es el nombre de la columna que va a compararse.
            string columnaComp = argumentos["nomAtributo"].Value;

            // El valor del argumento que se va a comparar con cada tupla
            // de la columna correspondiente. En caso de que el valor a
            // comparar sea una cadena, este campo es nulo.
            string numero = argumentos["valor"].Value;

            // La cadena que se va a comparar con cada tupla de la columna 
            // correspondiente. En caso de que el valor a comparar sea un
            // valor numérico, este campo es nulo.
            string cadena = argumentos["cadena"].Value;

            var tabla = Set.Tables[nomTabla];

            // Se crea la nueva tabla de resultados.
            DataTable tablaSQL = new DataTable("resultado");

            // Se crean las columnas correspondientes a la nueva tabla.
            foreach (var nomColumna in nomColumnas)
                tablaSQL.Columns.Add(tabla.Columns[nomColumna]);

            // Se realiza la búsqueda en cada una de las tablas.
            foreach (DataRow tupla in tabla.Rows)
            {
                if (!string.IsNullOrEmpty(operador))
                {
                    if (!string.IsNullOrEmpty(numero))
                    {
                        int a = int.Parse(tupla[columnaComp].ToString());
                        int b = int.Parse(numero);

                        switch (operador)
                        {
                            case ">" when a <= b: continue;
                            case "<" when a >= b: continue;
                            case ">=" when a < b: continue;
                            case "<=" when a > b: continue;
                            case "<>" when a == b: continue;
                            case "=" when a != b: continue;
                        }
                    }
                    else if (!string.IsNullOrEmpty(cadena) && !cadena.Equals(tupla[columnaComp].ToString()))
                        continue;
                }

                DataRow tuplaSQL = tablaSQL.NewRow();

                foreach (var nomColumna in nomColumnas)
                    tuplaSQL[nomColumna] = tupla[nomColumna];
            }

            return tablaSQL;
        }



        /**
         * Compila una sentencia SQL y retorna la tabla de datos correspondiente. 
         * En caso de ocurrir un error, se arroja una excepción.
         **/
        public void CompilaSentencia(string oracion)
        {
            // Conjunto de expresiones regulares para validar la sentencia SQL.
            string ws = @"(?:[ \n\t\r]+)";
            string wso = @"(?:" + ws + ")?";
            string id = wso + @"(?!(WHERE|SELECT))([A-Za-z][_0-9A-Za-z]*?)" + wso;
            string inicio = @"^SELECT" + ws;
            string todos = @"(?<todos>[*])";
            string nomAtributos = @"(?<atributos>" + id + "(," + id + ")*)";
            string atributos = @"(" + todos + "|" + nomAtributos + ")";
            string nomTab = ws + @"FROM" + ws + "(?<nomTabla>" + id + ")";
            string comp1 = @"((?<operador>=|>|<|>=|<=|<>)" + wso + @"(?<valor>\d+))";
            string comp2 = @"(=" + wso + @"\'(?<cadena>\w*)\')";
            string comparacion = @"(" + comp1 + "|" + comp2 + ")";
            string restriccion = @"(" + ws + "WHERE" + ws + "(?<nomAtributo>" + id + ")" + comparacion + ")?";

            // Junta las expresiones en una sola cadena, formando un patrón.
            string sentencia = inicio + atributos + nomTab + restriccion + "$";
            Regex expresionRegular = new Regex(sentencia, RegexOptions.IgnoreCase);

            // Compila la expresión regular y evalúa la sintaxis de la sentencia SQL.
            var match = expresionRegular.Match(oracion);

            // Verifica que la sintaxis de la oración sea correcta.
            if (!match.Success)
                throw new Exception("Tu sentencia SQL contiene un error de sintaxis.");

            // Verifica que la tabla especificada en la sentencia exista.
            if (!Set.Tables.Contains(match.Groups["nomTabla"].Value))
                throw new Exception("La tabla especificada no existe.");

            string nomTabla = match.Groups["nomTabla"].Value;
            string atributoComp = match.Groups["nomAtributo"].Value;
            string cad = match.Groups["cadena"].Value;
            string numero = match.Groups["valor"].Value;
            var tabla = Set.Tables[nomTabla];
            string[] nombres;

            // Checa si van a proyectarse todos los atributos o solo algunos.
            if (!string.IsNullOrEmpty(match.Groups["todos"].Value))
                nombres = NomColumnas(nomTabla);
            else
            {
                string valAtributos = Regex.Replace(match.Groups["atributos"].Value, "[ \n\t\r]+", "");
                nombres = valAtributos.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var atributo in nombres)
                    if (!tabla.Columns.Contains(atributo))
                        throw new Exception("\"" + atributo + "\" no existe en la tabla \"" + nomTabla + "\"");
            }

            // Verifica si se va a comparar una tabla con un valor. De ser así también
            // se verifica que dicha columna exista y que los tipos de datos que se 
            // quieran comparar sean consistentes.
            if (!string.IsNullOrEmpty(atributoComp))
            {
                if (!tabla.Columns.Contains(atributoComp))
                    throw new Exception("\"" + atributoComp + "\" no existe en la tabla \"" + nomTabla + "\"");
                else if (tabla.Columns[atributoComp].Equals("String") && !string.IsNullOrEmpty(numero))
                    throw new Exception("La columna \"" + atributoComp + "\" es de tipo cadena. No puedes comparar un valor" +
                        "numérico con una cadena.");
                else if (tabla.Columns[atributoComp].Equals("Int32") && !string.IsNullOrEmpty(cad))
                    throw new Exception("La columna \"" + atributoComp + "\" es de tipo entero. No puedes comparar una cadena" +
                        "con un valor entero.");
                else if (tabla.Columns[atributoComp].Equals("Single") && !string.IsNullOrEmpty(cad))
                    throw new Exception("La columna \"" + atributoComp + "\" es de tipo flotante. No puedes comparar una cadena" +
                        "con un valor flotante.");

            }

            DataTable tablaSQL = CreaTablaConsulta(nomTabla, nombres, match.Groups);

        }
    }
}
