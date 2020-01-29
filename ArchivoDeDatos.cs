using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace proyecto_BDA
{
    [Serializable]
    class ArchivoDeDatos
    {
        /**
         * Clase que permite almacenar en un archivo una
         * colección de registros lógicamente relacionados.
         **/

        // Nombre del archivo de datos.
        public string NombreArchivo { get; set; }

        // La tabla que determinará la forma y organización
        // de los registros en el archivo de datos.
        public Tabla Tabla { get; set; }

        // Estructuras de campos lógicamente relacionadas.
        public List<IComparable[]> Registros { get; } 
        public ArchivoDeDatos(string nombreArchivo, Tabla tabla)
        {
            Tabla = tabla;
            NombreArchivo = nombreArchivo;
            Registros = new List<IComparable[]>();
        }

        /**
         * Agrega un registro de datos al archivo de datos. 
         * Una vez agregado, la tabla no puede modificarse.
         **/
        public void AgregaRegistro(IComparable[] registro)
        {
            // Determina que la tabla ya no podrá ser
            // editada.
            Tabla.Editable = false;

            // Agrega el registro al archivo de datos.
            Registros.Add(registro);
        }

        /**
         * Permite visualizar los registros en una tabla
         * de datos.
         **/
        public DataTable ObtenRegistros()
        {
            DataTable tabla = new DataTable();

            // Se le agregan columnas a la tabla de datos.
            // El nombre de cada una de ellas será el
            // nombre de cada atributo contenido en la 
            // tabla del archivo de datos.
            foreach (var atributo in Tabla.Atributos)
                tabla.Columns.Add(atributo.Nombre);

            // Agrega todos los registros a la tabla de 
            // datos.
            foreach (var registro in Registros)
            {
                // Crea una fila nueva para insertar los
                // campos de este registro.
                DataRow dataRow = tabla.NewRow();

                // Le asigna a la columna i de la tabla
                // de datos el campo i del registro.
                for (int i = 0; i < registro.Length; i++)
                    dataRow[i] = registro[i];
            }

            return tabla;
        }
    }
}
