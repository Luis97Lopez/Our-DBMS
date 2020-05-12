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
                // Ya existe una base de datos con ese nombre.
                LeeTablas();
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
                            return item;
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

            if (tablas[nomTabla].PrimaryKey.Length == 1
            && tablas[nomTabla].PrimaryKey[0].ColumnName.Equals(nomAtributo))
                tablas[nomTabla].PrimaryKey = new DataColumn[] { };

            if (constraint != null)
                tablas[nomTabla].Constraints.Remove(constraint);

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
        public void AgregaLlaveForanea(string nomTabla, DataColumn llavePrimaria, DataColumn llaveForanea)
        {
            var tablas = Set.Tables;
            string nombreLlave = llavePrimaria.ColumnName;

            // Relaciona la llave primaria de un atributo de una tabla
            // con la llave foránea de esta tabla.
            tablas[nomTabla].Constraints.Add(nombreLlave, llavePrimaria, llaveForanea);
            var restriccion = tablas[nomTabla].Constraints[nombreLlave] as ForeignKeyConstraint;

            // Establece las reglas de modificación y eliminación.
            restriccion.UpdateRule = Rule.Cascade;
            restriccion.DeleteRule = Rule.Cascade;
        }

        /**
         * Agrega un registro a una tabla.
         **/
        public void AgregaRegistro(string nomTabla, DataRow registro)
        {
            var tablas = Set.Tables;
            tablas[nomTabla].Rows.Add(registro);
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
                    dataRow["Tipo de Llave"] = "Foránea";
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
