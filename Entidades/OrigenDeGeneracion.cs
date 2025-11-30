using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.Entidades
{
    public class OrigenDeGeneracion
    {
        private string descripcion;
        private string nombre;

        public OrigenDeGeneracion(string nombre, string descripcion)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
        }

        public OrigenDeGeneracion()
        {
        }   

        public string getNombre()
        {
            return nombre;
        }

    }
}
