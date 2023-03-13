using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalMarzo
{
    class Alumnos
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }
        public int Legajo { get; set; }

        public Alumnos()
        {
        }

        public Alumnos(string Nom, string Ape, int Leg, int Dni)
        {
            Nombre = Nom;
            Apellido = Ape;
            DNI = Dni;
            Legajo = Leg;
        }
    }
}
