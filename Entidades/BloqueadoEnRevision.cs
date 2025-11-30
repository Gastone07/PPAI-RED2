using PPAI_REDSISMICA.ModeloPersistencia;
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

        private int idEstado = 3;
        public BloqueadoEnRevision(string ambito, string nombreEstado)
        {
            this.ambito = ambito;
            this.nombreEstado = nombreEstado;
        }

        public BloqueadoEnRevision()
        {
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

        public override bool esAutoDetectado() { return false; }
        public override bool esBloqueadoEnRevision() { return true; }
        public override bool esPendienteRevisionExperto() { return false; }
        public override bool esRechazado() { return false; }
        public override bool esConfirmado() { return false; }
        public override bool esPendienteParaRevision() { return false; }

        public Rechazado crearEstadoRechazado()
        {
            return new(ambito, "Rechazado");
        }

        public CambioEstado crearNuevoCambioEstado(Estado estado)
        {
            return new CambioEstado(DateTime.Now, null, estado );
        }
        public override void cambiarEstadoEventoSismico(DateTime fechaHora, CambioEstado cambio, EventoSismico evento) 
        {
            cambio.setFechaHoraFin(fechaHora);        
            CambioEstadoPersistencia.setHoraFin(fechaHora, cambio.getId());
            Rechazado rechazado = crearEstadoRechazado();
            CambioEstado actual = crearNuevoCambioEstado(rechazado);

            CambioEstadoPersistencia.insertarCambioEstado(idEstado,fechaHora, evento.getId());

            //return rechazado;
        }
    }
}
