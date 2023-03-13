using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalMarzo
{
    class Curso
    {
        public string NombreCurso { get; set; }
        public string Profesor { get; set; }

        List<Alumnos> ListaAlumnos = new List<Alumnos>();

        public Curso()
        {

        }

        public Curso(string Nom, string Prof)
        {
            NombreCurso = Nom;
            Profesor = Prof;
        }
    }
}
