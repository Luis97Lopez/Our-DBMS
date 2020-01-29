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
         * |\________________________/|
         * |\________________________/|
         * |\________________________/|
         * |\________________________/|
         * |\________________________/|
         * |\________________________/|
         * \__________________________/
         *
         **/

        // Nombre de la carpeta con los archivos de datos.
        public string NombreBaseDeDatos { get; set; }

        // Conjunto de tablas de la base de datos.
        public SortedSet<string> Tablas { get; }
    
        /**
         * Crea una nueva base de datos. 
         **/
        public BaseDeDatos(string nombre)
        {
            NombreBaseDeDatos = nombre;
            Tablas = new SortedSet<string>();

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
                // Obtiene el nombre de la tabla.
                string nomTabla = Path.GetFileName(archivoDeDatos).Replace(".dat", "");
                Tablas.Add(nomTabla);
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
            bool res = Tablas.Add(nomTabla);

            if (res)
            {
                // Crea el archivo de datos de la tabla nueva.
                string nombre = NombreBaseDeDatos + "/" + nomTabla + ".dat";
                GuardaArchivoDeDatos(new ArchivoDeDatos(nombre, new Tabla(nomTabla)));
            }

            return res;
        }

        /**
         * Agrega un atributo nuevo a una tabla de la base de datos,
         * siempre y cuando a la tabla no se le haya agregado ningún
         * registro.
         **/
        public bool AgregaAtributo(string nomTabla, Atributo atributo)
        {
            string nombreArchivoDeDatos = NombreBaseDeDatos + "/" + nomTabla + ".dat";
            var archivoDeDatos = LeeArchivoDeDatos(nombreArchivoDeDatos);
            var tabla = archivoDeDatos.Tabla;

            // Verifica que no se haya insertado ningún registro a
            // este archivo de datos.
            bool res = tabla.Editable;

            if (res)
            {

                // Agrega el atributo y actualiza el archivo de
                // datos.
                tabla.Atributos.Add(atributo);
                GuardaArchivoDeDatos(archivoDeDatos);
            }

            return res;
        }

        /**
         * Guarda/Actualiza el archivo de datos en la base de
         * datos.
         **/
        private void GuardaArchivoDeDatos(ArchivoDeDatos archivoDeDatos)
        {
            using (var archivoDAT = new FileStream(archivoDeDatos.NombreArchivo, FileMode.OpenOrCreate))
            {
                var serializador = new BinaryFormatter();
                serializador.Serialize(archivoDAT, archivoDeDatos);
            }
        }

        /**
         * Lee el archivo de datos encontrado dentro de la base
         * de datos.
         **/
        private ArchivoDeDatos LeeArchivoDeDatos(string nombreArchivoDeDatos)
        {
            ArchivoDeDatos archivoDeDatos = null;

            using (var archivoDAT = new FileStream(nombreArchivoDeDatos, FileMode.Open))
            {
                var serializador = new BinaryFormatter();
                archivoDeDatos = (ArchivoDeDatos)serializador.Deserialize(archivoDAT);
            }

            return archivoDeDatos;
        }
    }
}