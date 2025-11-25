using PPAI_REDSISMICA.Entidades;
using PPAI_REDSISMICA.Persistencia;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Documents;
using Vistas;

namespace Controladores
{
    public class controladorRegistrarResultadoRevisionManual
    {
        #region aributos

        private readonly pantallaRegistrarResultadoRevisionManual pantalla;
        // Declarar una variable que contenga un listado de eventos s�smicos
        private List<EventoSismico> eventosSismicos = new List<EventoSismico>();

        private List<EventoSismico> eventosPendientes = new List<EventoSismico>();

        private Estado estadoBloqueado = new Estado();

        private Usuario usuarioLogeado = new Usuario();

        private EventoSismico eventoSelec = new EventoSismico();

        private List<Estado> listadoEstado = new List<Estado>();

        private List<Sesion> listadoSesiones = new List<Sesion>();

        private DateTime fechaHoraActual;

        private List<CambioEstado> listadoCambiosEstado = new List<CambioEstado>();

        private CambioEstado cambioEstadoAbierto = new CambioEstado();

        private CambioEstado cambioEstadoNuevo = new CambioEstado();

        private string nombreAlcance = "";
        private string nombreClasificacion = "";
        private string nombreOrigen = "";

        private List<SerieTemporal> seriesVisitadas = new List<SerieTemporal>();
        private List<MuestraSismica> muestrasVisitadas = new List<MuestraSismica>();
        private List<DetalleMuestraSismica> detallesVisitados = new List<DetalleMuestraSismica>();
        private List<(DetalleMuestraSismica, TipoDeDato)> tipoDatoPorDetalle = new List<(DetalleMuestraSismica, TipoDeDato)>();

        private List<Sismografo> sismografos = new List<Sismografo>();
        private string nombreEstacion = "";

        private Estado estadoSeleccionado = new Estado();

        // diccionario de sismografo y serie temporal
        private Dictionary<Sismografo, SerieTemporal> diccionarioSismografoSerie = [];

        #endregion
        //Paso 1 del Caso de Uso
        public controladorRegistrarResultadoRevisionManual(pantallaRegistrarResultadoRevisionManual pan)
        {
            this.pantalla = pan;
        }

        public void registrarResultadoDeRevisionManual()
        {
            // Cargar los datos iniciales y asignarlos a los atributos del controlador
            (eventosSismicos, listadoEstado, listadoCambiosEstado, listadoSesiones, sismografos) = Persistencia.ObtenerDatos2();

            buscarAutodetectado();
        }

        public void buscarAutodetectado()
        {
            // Limpiar la lista de eventos pendientes antes de agregar nuevos
            eventosPendientes.Clear();

            foreach (var evento in eventosSismicos)
            {
                if (evento.esPendienteDeRevision())
                {
                    eventosPendientes.Add(evento);
                }
            }

            // Preparar los datos (objetos anónimos) para la pantalla
            // Cada objeto contiene las propiedades que la vista necesita y una referencia
            // al EventoSismico original en la propiedad EventoOriginal.
            var eventosPreparados = eventosPendientes.Select(evento => new
            {
                FechaHora = evento.FechaHoraOcurrencia,
                Magnitud = evento.ValorMagnitud,
                Estado = evento.GetEstadoActual().getNombreEstado(),
                CoordenadasEpicentro = string.Join(" - ", evento.getCordenadasEpicentro().Cast<object>()),
                CoordenadasHipocentro = string.Join(" - ", evento.getCordenadasHipocentro().Cast<object>()),
                EventoOriginal = evento
            }).ToList<object>();

            // Pasar los datos preparados a la pantalla (la vista mostrará las propiedades del objeto anónimo)
            pantalla.presentarEventosSismicosPendientesDeRevision(eventosPreparados);
        }

        public void tomarSeleccionEventoSismico(EventoSismico eventoSeleccionado)
        {
            // paso 7 caso de uso
            // busco el puntero de la para el estado bloqueado
            eventoSelec = eventoSeleccionado;
            //estadoBloqueado = buscarEstadoBloqueado(listadoEstado);

            buscarEstadoBloqueado();
            //busco el usuario logueado en ese momento
            usuarioLogeado = buscarUsuarioLogeado();

            //getFechaHoraActual
            fechaHoraActual = getFechaYHoraActual();

            actualizarCambioEstado(eventoSeleccionado);

        }

        public DateTime getFechaYHoraActual()
        {
            // Retorna la fecha y hora actual del sistema
            return DateTime.Now;
        }

        public void buscarEstadoBloqueado()
        {
            foreach (Estado estado in listadoEstado)
            {
                if (estado.esAmbitoEvento() && estado.esEstadoBloqueado())
                {
                    estadoBloqueado = estado; // Asignar el estado bloqueado encontrado
                    break; // Salir del bucle una vez encontrado
                }
            }
            
        }

        private Usuario buscarUsuarioLogeado()
        {
            foreach (var sesion in listadoSesiones)
            {
                if (sesion.esSesionActiva()) // Usar el método de la entidad
                {
                    var usuario = sesion.obtenerUsuarioLogeado();
                    if (usuario != null) 
                        return usuario;
                }       
            }
            
            // Si no se encuentra un usuario logueado, retornar un Usuario vacío
            return new Usuario();
        }

        // paso 8 del caso de uso seria nuestro revisar()
        private void actualizarCambioEstado(EventoSismico eventoSeleccionado)
        {

            //cambioEstadoAbierto = eventoSeleccionado.buscarCambioEstadoAbierto(fechaHoraActual);
            //cambioEstadoAbierto = eventoSeleccionado.actualizarCambioEstado();

            cambioEstadoNuevo = eventoSeleccionado.crearCambioEstado(estadoBloqueado, fechaHoraActual);
            
            listadoCambiosEstado.Add(cambioEstadoNuevo);

            //fin paso 8
            buscarDetallesEventoSismico(eventoSeleccionado);
        }

        // paso 9 del caso
        public void buscarDetallesEventoSismico(EventoSismico eventoSeleccionado)
        {
            //guardo los valores de los detalles del evento sismico
            (nombreAlcance, nombreClasificacion, nombreOrigen) = eventoSeleccionado.getDetallesEventoSismico();

            obtenerDatosSeriesTemporal(eventoSeleccionado);
            generarSismograma(seriesVisitadas, muestrasVisitadas, detallesVisitados, tipoDatoPorDetalle);

            pantalla.mostrarDetalleEventoSismico(nombreAlcance, nombreClasificacion, nombreOrigen, eventoSeleccionado.ValorMagnitud);

        }
        public void obtenerDatosSeriesTemporal(EventoSismico eventoSeleccionado)
        {
            //limpio las listas para evitar duplicados
            seriesVisitadas.Clear();
            muestrasVisitadas.Clear();
            detallesVisitados.Clear();
            tipoDatoPorDetalle.Clear();
            diccionarioSismografoSerie.Clear();

            //Obtengo las series Temporales
            seriesVisitadas = eventoSeleccionado.buscarSeriesTemporal();
            foreach (SerieTemporal serie in seriesVisitadas)
            {
                //obtengo los datos de cada serie temporal
                serie.getDatos(muestrasVisitadas, detallesVisitados, tipoDatoPorDetalle);

                //recorro las sismografos para saber a cual pertenece cada serie temporal
                foreach (Sismografo sismografo in sismografos)
                {
                    foreach (SerieTemporal serieSismografo in sismografo.getSeriesTemporales())
                    {
                        if (serieSismografo.Equals(serie)) // Compara la serie temporal del sismografo con la serie temporal del evento sismico
                        {
                            diccionarioSismografoSerie.Add(sismografo, serie);
                        }
                    }
                }
            }            
        }

        public void generarSismograma(List<SerieTemporal> seriesVisitadas, 
                                        List<MuestraSismica> muestrasVisitadas, 
                                        List<DetalleMuestraSismica> detallesVisitados, 
                                        List<(DetalleMuestraSismica, TipoDeDato)> tipoDatoPorDetalle)
        {
            //Generar Sismograma
            //aca llamamos al CU externo

        }

        public void tomarOpcionGrilla(string opcionCombo, string alcance, string origen, double magnitud)
        {
            //paso 14 al 17 con alternativas
            if (opcionCombo == "Rechazar evento")
            {
                estadoSeleccionado = Estado.esRechazado(listadoEstado);
                cambioEstadoAbierto = eventoSelec.buscarCambioEstadoAbierto(fechaHoraActual);
                
                cambioEstadoNuevo = eventoSelec.crearCambioEstado(estadoSeleccionado, fechaHoraActual);
                listadoCambiosEstado.Add(cambioEstadoNuevo);
            }
            else if (opcionCombo == "Confirmar evento")
            {
                estadoSeleccionado = Estado.esConfirmado(listadoEstado);
                cambioEstadoAbierto = eventoSelec.buscarCambioEstadoAbierto(fechaHoraActual);
                
                cambioEstadoNuevo = eventoSelec.crearCambioEstado(estadoSeleccionado, fechaHoraActual);
                listadoCambiosEstado.Add(cambioEstadoNuevo);
            }
            else if (opcionCombo == "Solicitar revisi�n a experto")
            {
                estadoSeleccionado = Estado.esRevisadoExperto(listadoEstado);
                cambioEstadoAbierto = eventoSelec.buscarCambioEstadoAbierto(fechaHoraActual);
                
                cambioEstadoNuevo = eventoSelec.crearCambioEstado(estadoSeleccionado, fechaHoraActual);
                listadoCambiosEstado.Add(cambioEstadoNuevo);
            }
            else
            {
                // Manejar caso no v�lido
                throw new ArgumentException("Opci�n no v�lida");
            }
            buscarAutodetectado();
        }

        // La presentación de eventos la realiza la vista (pantalla). El controlador prepara los objetos
        // y llama a pantalla.presentarEventosSismicosPendientesDeRevision(...)
    }
}