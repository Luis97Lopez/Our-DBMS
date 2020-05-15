using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

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
                Set.Relations.Add(LeeArchivoRelacion(archivoIDX));
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
        private Constraint GetConstraint(string nomTabla, string nomAtributo)
        {
            var table = Set.Tables[nomTabla];
            foreach (Constraint item in table.Constraints)
            {
                if (item is UniqueConstraint)
                {
                    UniqueConstraint unique = (UniqueConstraint)item;
                    foreach (var column in unique.Columns)
                    {
                        if (column.ColumnName == nomAtributo)
                            return item;
                    }
                }
                
                else if (item is ForeignKeyConstraint)
                {
                    ForeignKeyConstraint foreign = (ForeignKeyConstraint)item;
                    foreach (var column in foreign.Columns)
                    {
                        if (column.ColumnName == nomAtributo)
                            return null;
                    }
                }
                
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
            tablas[nomTabla].Columns.Add(columna);

            if (columna.Unique)
                if (tablas[nomTabla].PrimaryKey.Length == 0)
                    AgregaLlavePrimaria(nomTabla, columna);
                else
                {
                    EliminaAtributo(nomTabla, columna.ColumnName);
                    throw new DuplicatePrimaryKeyException();
                }

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

            if (atributoNuevo.Unique && tablas[nomTabla].PrimaryKey[0].ColumnName != nomAtributo)
                throw new DuplicatePrimaryKeyException();

            EliminaAtributo(nomTabla, nomAtributo);
            AgregaAtributo(nomTabla, atributoNuevo);
        }

        /**
         * Elimina un atributo y bloquea la posibilidad de poder modificarlo en un futuro.
         **/
        public void EliminaAtributo(string nomTabla, string nomAtributo)
        {
            var tablas = Set.Tables;
            Constraint constraint = GetConstraint(nomTabla, nomAtributo);

            // Verifica que el atributo no pertenezca a la clave primaria,
            // de ser así se vacía el campo PrimaryKey. Al hacer esto, au-
            // tomáticamente se eliminan las restricciones (Constraints) a
            // las que pertenezca la clave primaria.
            if (tablas[nomTabla].PrimaryKey.Length == 1 && tablas[nomTabla].PrimaryKey[0].ColumnName.Equals(nomAtributo))
            {
                //tablas[nomTabla].PrimaryKey = null;
                tablas[nomTabla].PrimaryKey = new DataColumn[] { };
                //tablas[nomTabla].Constraints.Remove(constraint); // El reinicializar el arreglo ya hace esto.
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

            DataRelation relacion = new DataRelation(nomLlave, llavePrimaria, llaveForanea);
            Set.Relations.Add(relacion);

            // Crea un diccionario con la información que va a almacenarse.
            var informacion = new Dictionary<string, string>()
            {
                // El nombre de la relación de la clave foránea con otra
                // tabla.
                ["nombre relacion"] = nomLlave,

                // El nombre de la tabla a la que pertenece la clave 
                // primaria.
                ["tabla primaria"] = llavePrimaria.Table.TableName,

                // El nombre de la tabla a la que pertenece la clave 
                // foránea.
                ["tabla foranea"] = llaveForanea.Table.TableName,

                // El nombre de la columna que representa la clave
                // primaria.
                ["clave primaria"] = llavePrimaria.ColumnName,

                // El nombre de la columna que representa la clave
                // foránea.
                ["clave foranea"] = llaveForanea.ColumnName
            };

            // Guarda los metadatos del diccionario en un archivo
            // índice.
            string nomArchivo = NombreBaseDeDatos + "\\" + nomLlave + ".idx";

            using (var archivoIDX = new FileStream(nomArchivo, FileMode.OpenOrCreate))
            {
                var serializador = new BinaryFormatter();
                serializador.Serialize(archivoIDX, informacion);
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
    }
}
