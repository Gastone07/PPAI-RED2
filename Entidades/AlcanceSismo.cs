using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.Entidades
{
    public class AlcanceSismo
    {
        private string descripcion;
        private string nombre;

        public AlcanceSismo(string nombre, string descripcion)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
        }

        public AlcanceSismo()
        {

        }

        public string getNombre()
        {
            return nombre;
        }

        



    }
}
