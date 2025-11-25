using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI_REDSISMICA.Entidades
{
    public class PendienteParaRevision : Estado 
    {
        private string ambito { get; set; }
        private string nombreEstado { get; set; }

        private CambioEstado cambioEstado { get; set; }


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

        public void cambiarEstadoEventoSismico(DateTime fechaHoraActual)
        {
            this.buscarCambioEstadoActual(fechaHoraActual);
        }

        public void buscarCambioEstadoActual(DateTime fechaHoraActual)
        {
            cambioEstado.sosActual(fechaHoraActual);
        }

        public BloqueadoEnRevision crearEstadoBloqueadoEnRevision()
        {
            return new BloqueadoEnRevision("Evento Sismico", "BloqueadoParaRevision");
        }

        public CambioEstado crearNuevoCambioEstado(DateTime fechaHoraActual, BloqueadoEnRevision estado)
        {
            return new CambioEstado(fechaHoraActual, null, estado);
            
        }
    }
}
