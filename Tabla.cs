using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_BDA
{
    [Serializable]
    class Tabla
    {
        // Nombre de la tabla.
        public string Nombre { get; set; }
        public bool Editable { get; set; }
        public bool Modificable { get; set; }
        public List<Atributo>Atributos { get; }
        public Tabla(string nombre)
        {
            Nombre = nombre;
            Editable = true;
            Modificable = true;
            Atributos = new List<Atributo>();
        }

        public bool ContieneLlavePrimaria()
        {
            bool res = false;

            for (int i = 0; i < Atributos.Count && !res; i++)
                res = Atributos[i].Llave == TipoLlave.Primaria;

            return res;
        }
        public override string ToString() => Nombre;
    }
}