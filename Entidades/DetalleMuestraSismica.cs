using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.Entidades
{
    public class DetalleMuestraSismica
    {
        public double valor { get; set; }

        private TipoDeDato tipoDeDato;

        public DetalleMuestraSismica(double valor, TipoDeDato tipoDeDato)
        {
            this.valor = valor;
            this.tipoDeDato = tipoDeDato;
        }

        public TipoDeDato getTipoDato()
        {
            if (tipoDeDato == null)
            {
                throw new InvalidOperationException("No hay tipo de dato asociado a la muestra sismica.");
            }
            return tipoDeDato.getDatos();
        }
    }
}
