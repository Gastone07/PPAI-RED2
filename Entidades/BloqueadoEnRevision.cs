using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI_REDSISMICA.Entidades
{
    public class BloqueadoEnRevision : Estado
    {
        private string ambito { get; set; }
        private string nombreEstado { get; set; }
        public BloqueadoEnRevision(string ambito, string nombreEstado)
        {
            this.ambito = ambito;
            this.nombreEstado = nombreEstado;
        }
        public string getAmbito()
        {
            return ambito;
        }
        public void setAmbito(string ambito)
        {
            this.ambito = ambito;
        }
        public new string getNombreEstado()
        {
            return nombreEstado;
        }
        public void setNombreEstado(string nombreEstado)
        {
            this.nombreEstado = nombreEstado;
        }



    }
}
