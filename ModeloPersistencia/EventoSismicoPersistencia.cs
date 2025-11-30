using PPAI_REDSISMICA.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.ModeloPersistencia
{
    internal class EventoSismicoPersistencia
    {
        /*
        public static List<CambioEstado> recuperarCambiosEstados(int id)
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("CambioEstado WHERE idEstado = " + id);
            List<CambioEstado> listaCambiosEstados = new List<CambioEstado>();
            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                foreach (DataRow item in respuesta.Rows)
                {
                    listaCambiosEstados.Add(new(item));
                }
            }
            return listaCambiosEstados;
        }
        */


        public static List<EventoSismico> obtenerTodoEventoSismico()
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("EventosSismico");
            List<EventoSismico> listaEventos = new List<EventoSismico>();

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {

                //Hubo un error al consultar la base de datos
                foreach (DataRow item in respuesta.Rows)
                {
                    int id = int.Parse(item["idSismo"].ToString() ?? "");

                    AlcanceSismo alcance = AlcanceSismoPersistencia.recuperarAlcanceSismoXID(id);
                    OrigenDeGeneracion origen = OrigenDeGeneracionPersistencia.recuperarOrigenDeGeneracionXID(id);
                    ClasificacionSismo clasificacion = ClasificionSismoPersistencia.recuperarClasificacionSismoXID(id);
                    List<SerieTemporal> series = serieTemporalPersistencia.recuperarSeriesTemporalesPorEventoSismico(id);
                    List<CambioEstado> cambiosEstado = CambioEstadoPersistencia.obtenerCambiosEstadosXID(id);
                    Estado estadoActual = EstadoPersistencia.recuperarEstadoXID(int.Parse(item["idEstadoActual"].ToString() ?? ""));


                    listaEventos.Add(new EventoSismico(
                        id,
                        DateTime.Parse(item["fechaHoraFin"].ToString() ?? ""),
                        DateTime.Parse(item["fechaHoraOcurrencia"].ToString() ?? ""),
                        Double.Parse(item["laitudEpicentro"].ToString() ?? ""),
                        Double.Parse(item["laitudHipocentro"].ToString() ?? ""),
                        Double.Parse(item["longitudEpicentro"].ToString() ?? ""),
                        Double.Parse(item["longitudHipocentro"].ToString() ?? ""),
                        Double.Parse(item["valorMagnitud"].ToString() ?? ""),
                        cambiosEstado,
                        estadoActual,
                        origen,
                        alcance,
                        clasificacion,
                        series
                        )
                        );


                }

            }
            return listaEventos;
        }
    }
}
