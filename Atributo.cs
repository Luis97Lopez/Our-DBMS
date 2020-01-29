using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_BDA
{
    [Serializable]
    class Atributo
    {
        public string Nombre { get; set; }
        public char Tipo { get; set; }
        public int TipoIndice { get; set; }

        public Atributo(string nombre, char tipo, int tipoIndice)
        {
            Nombre = nombre;
            Tipo = tipo;
            TipoIndice = tipoIndice;
        }
        public override string ToString() => Nombre;
    }
}
