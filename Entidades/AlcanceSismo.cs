using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string getNombre()
        {
            return nombre;
        }
    }



}
