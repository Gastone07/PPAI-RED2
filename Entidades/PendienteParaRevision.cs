using PPAI_REDSISMICA.ModeloPersistencia;
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

        private int idEstado = 6;
        private CambioEstado cambioEstado { get; set; }


        public PendienteParaRevision(string ambito, string nombreEstado)
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

        public override int recuperarId()
        {
            return idEstado;
        }
        public override bool esAutoDetectado(){  return false; }
        public override bool esBloqueadoEnRevision() { return false; }
        public override bool esPendienteRevisionExperto() { return false; }
        public override bool esRechazado() { return false; }
        public override bool esConfirmado() { return false; }
        public override bool esPendienteParaRevision(){ return true; }

        public BloqueadoEnRevision crearEstadoBloqueadoParaRevision()
        {
            return new(ambito, "BloqueadoParaRevision");
        }

        public CambioEstado crearNuevoCambioEstado(Estado estado)
        {
            return new CambioEstado(DateTime.Now, null, estado);
        }

        public override void cambiarEstadoEventoSismico(DateTime fechaHora, CambioEstado cambio, EventoSismico evento)
        {
            cambio.setFechaHoraFin(fechaHora);
            CambioEstadoPersistencia.setHoraFin(fechaHora, cambio.getId());
            BloqueadoEnRevision bloqueado = crearEstadoBloqueadoParaRevision();
            CambioEstado actual = crearNuevoCambioEstado(bloqueado);

            CambioEstadoPersistencia.insertarCambioEstado(idEstado, fechaHora, evento.getId());
            evento.setEstado(bloqueado);
            evento.agregarCambioEstado(actual);
            
        }

        public CambioEstado crearNuevoCambioEstado(DateTime fechaHoraActual, BloqueadoEnRevision estado)
        {
            return new CambioEstado(fechaHoraActual, null, estado);
            
        }

       
    }
}
