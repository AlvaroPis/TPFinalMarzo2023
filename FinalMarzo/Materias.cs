using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalMarzo
{
    class Materias
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Materias()
        {

        }
        public Materias(string Nom, string Desc)
        {
            Nombre = Nom;
            Descripcion = Desc;
        }
    }
}
