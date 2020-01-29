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
        public string Nombre { get; set; }
        public bool Editable { get; set; }
        public List<Atributo>Atributos { get; }
        public Tabla(string nombre)
        {
            Nombre = nombre;
            Editable = true;
            Atributos = new List<Atributo>();
        }

        public override string ToString() => Nombre;
    }
}