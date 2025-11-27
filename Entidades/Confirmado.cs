using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI_REDSISMICA.Entidades
{
    public class Confirmado : Estado
    {
        private string ambito { get; set; }
        private string nombreEstado { get; set; }

        private int idEstado = 4;
        private CambioEstado cambioEstado { get; set; }


        public Confirmado(string ambito, string nombreEstado)
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

        public void cambiarEstadoEventoSismico(DateTime fechaHoraActual)
        {
            this.buscarCambioEstadoActual(fechaHoraActual);
        }

        public void buscarCambioEstadoActual(DateTime fechaHoraActual)
        {
            cambioEstado.sosActual(fechaHoraActual);
        }

        public CambioEstado crearNuevoCambioEstado(DateTime fechaHoraActual, BloqueadoEnRevision estado)
        {
            return new CambioEstado(fechaHoraActual, null, estado);

        }

        public override bool esAutoDetectado() { return false; }
        public override bool esBloqueadoEnRevision() { return false; }
        public override bool esPendienteRevisionExperto() { return false; }
        public override bool esRechazado() { return false; }
        public override bool esConfirmado() { return true; }
        public override bool esPendienteParaRevision() { return false; }


    }
}
