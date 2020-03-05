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
    class BaseDeDatos2
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
                LeeTablas();
            }
        }

        // Almacén de datos.
        private DataSet Set { get; set; }

        /**
         * Crea una nueva base de datos y se le asigna su
         * nombre correspondiente. Si no existe, crea una
         * nueva, de lo contrario, lee la base de datos
         * existente.
         **/
        public BaseDeDatos2(string nombre)
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
        public bool AgregaTabla(string nomTabla)
        {
            // Verifica si la tabla no existe, de no ser así,
            // no se genera un archivo de datos nuevo.
            bool res = !Set.Tables.Contains(nomTabla);

            if (res)
            {
                DataTable tablaNueva = new DataTable(nomTabla);
                Set.Tables.Add(tablaNueva);
                nomTabla = NombreBaseDeDatos + "\\" + nomTabla + ".dat";
                GuardaArchivoDeDatos(tablaNueva, nomTabla);
            }

            return res;
        }

        /**
         * Modifica el nombre de una tabla de la base de datos
         **/
        public void ModificaNombreTabla(string nomTablaAnterior, string nomTablaNueva)
        {
            var tablas = Set.Tables;

            if (AgregaTabla(nomTablaNueva))
            {
                tablas[nomTablaNueva].Merge(tablas[nomTablaAnterior]);
                EliminaTabla(nomTablaAnterior);
                GuardaArchivoDeDatos(tablas[nomTablaNueva], NombreBaseDeDatos + "\\" + nomTablaNueva + ".dat");
            }
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
         * Agrega un atributo nuevo a una tabla de la base de datos,
         * siempre y cuando a la tabla no se le haya agregado ningún
         * registro.
         **/
        public bool AgregaAtributo(string nomTabla, DataColumn columna)
        {
            var tablas = Set.Tables;
            bool res = !tablas[nomTabla].Columns.Contains(columna.ColumnName);
            res &= !columna.Unique && tablas[nomTabla].PrimaryKey.Length == 0;
            
            if (res)
            {
                tablas[nomTabla].Columns.Add(columna);
                GuardaArchivoDeDatos(tablas[nomTabla], NombreBaseDeDatos + "\\" + nomTabla + ".dat");
            }

            return res;
        }

        /**
         * Modifica un atributo en la tabla actual de esta base de 
         * datos, siempre y cuando no se haya eliminado uno
         * anteriormente.
         **/
        public bool ModificaAtributo(string nomTabla, string nomAtributo, DataColumn atributoNuevo)
        {
            var tablas = Set.Tables;
            bool res = !tablas[nomTabla].Columns.Contains(atributoNuevo.ColumnName);
            
            if (res)
            {
                tablas[nomTabla].Columns.Remove(nomAtributo);
                tablas[nomTabla].Columns.Add(atributoNuevo);
                GuardaArchivoDeDatos(tablas[nomTabla], NombreBaseDeDatos + "\\" + nomTabla + ".dat");
            }

            return res;
        }

        /**
         * Elimina un atributo y bloquea la posibilidad de poder modificarlo en un futuro.
         **/
        public void EliminaAtributo(string nomTabla, string nomAtributo)
        {
            var tablas = Set.Tables;
            tablas[nomTabla].Columns.Remove(nomAtributo);
            GuardaArchivoDeDatos(tablas[nomTabla], NombreBaseDeDatos + "\\" + nomTabla + ".dat");
        }

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

        /*
        public DataTable ObtenAtributos(string nomTabla)
        {
            var archivoDeDatos = LeeArchivoDeDatos(Tablas[nomTabla]);
            var tabla = archivoDeDatos.Tabla;

            DataTable tablaDatos = new DataTable();
            string[] nomColumnas = { "Nombre", "Tipo de Dato", "Longitud", "Tipo de Llave" };

            foreach (var nomColumna in nomColumnas)
                tablaDatos.Columns.Add(nomColumna);

            foreach (var atributo in tabla.Atributos)
            {
                DataRow dataRow = tablaDatos.NewRow();

                dataRow["Nombre"] = atributo.Nombre;
                dataRow["Tipo de Dato"] = atributo.Tipo;
                dataRow["Longitud"] = atributo.Tamaño;

                switch (atributo.Llave)
                {
                    case TipoLlave.Primaria: dataRow["Tipo de Llave"] = "Primaria"; break;
                    case TipoLlave.Foranea: dataRow["Tipo de Llave"] = "Foránea"; break;
                    case TipoLlave.SinLlave: dataRow["Tipo de Llave"] = "Ninguna"; break;
                }

                tablaDatos.Rows.Add(dataRow);
            }

            return tablaDatos;
        }*/
    }
}
