using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI_REDSISMICA.Entidades
{
    public class ClasificacionSismo
    {
        private int kmProfundidadDesde;
        private int kmProfundidadHasta;
        private string nombre;


        public ClasificacionSismo(int kmProfundidadDesde, int kmProfundidadHasta, string nombre)
        {
            this.kmProfundidadDesde = kmProfundidadDesde;
            this.kmProfundidadHasta = kmProfundidadHasta;
            this.nombre = nombre;
        }

        public string getNombre()
        {
            return nombre;
        }
    }
}
