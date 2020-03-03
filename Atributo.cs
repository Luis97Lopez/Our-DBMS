using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_BDA
{
    enum TipoLlave
    {
        Primaria,
        Foranea,
        SinLlave
    }
    [Serializable]
    class Atributo
    {
        // Nombre del atributo.
        public string Nombre { get; set; }
        // Tipo de atributo.
        // Entero, decimal, cadena.
        public string Tipo { get; set; }
        // Tamaño del atributo.
        public int Tamaño { get; set; }
        // Tipo de llave
        public TipoLlave Llave { get; set; }
        public Atributo(string nombre, string tipo, int tamaño, TipoLlave tipoLlave)
        {
            Nombre = nombre;
            Tipo = tipo;
            Tamaño = tamaño;
            Llave = tipoLlave;
        }
        public override string ToString() => Nombre;
    }
}
