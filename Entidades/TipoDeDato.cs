using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI_REDSISMICA.Entidades
{
    public class TipoDeDato
    {
        public string denominacion { get; set; }

        public string nombreUnidadMedida { get; set; }

        public double valorUmbral { get; set; }

        public TipoDeDato(string denominacion, string nombreUnidadMedida, double valorUmbral)
        {
            this.denominacion = denominacion;
            this.nombreUnidadMedida = nombreUnidadMedida;
            this.valorUmbral = valorUmbral;
        }

        public TipoDeDato getDatos()
        {
            if (string.IsNullOrEmpty(denominacion) || string.IsNullOrEmpty(nombreUnidadMedida) || valorUmbral < 0)
            {
                throw new InvalidOperationException("Los datos del tipo de dato no son válidos.");
            }
            return this; // Retorna el objeto actual si los datos son válidos
        }
    }
}
