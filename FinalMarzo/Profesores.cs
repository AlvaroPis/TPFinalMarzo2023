using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalMarzo
{
    class Profesores
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }
        public int Legajo { get; set; }

        public Profesores()
        {
        }

        public Profesores(string Nom, string Ape, int Leg, int Dni)
        {
            Nombre = Nom;
            Apellido = Ape;
            DNI = Dni;
            Legajo = Leg;
        }
    }
}
