using PPAI_REDSISMICA.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.RightsManagement;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

public class EventoSismico
{
    public DateTime FechaHoraFin { get; set; }
    public DateTime FechaHoraOcurrencia { get; set; }
    public double LatitudEpicentro { get; set; }
    public double LatitudHipocentro { get; set; }
    public double LongitudEpicentro { get; set; }
    public double LongitudHipocentro { get; set; }
    public double ValorMagnitud { get; set; }
    private CambioEstado CambioEstado { get; set; }
    private Estado estadoActual { get; set; }

    private OrigenDeGeneracion origenDeGeneracion { get; set; }
    private AlcanceSismo alcance { get; set; }
    private ClasificacionSismo clasificacion { get; set; }

    private List<SerieTemporal>? seriesTemporales = new List<SerieTemporal>();

    public EventoSismico(
        DateTime fechaHoraOcurrencia,
        DateTime fechaHoraFin,
        double latitudEpicentro,
        double longitudEpicentro,
        double latitudHipocentro,
        double longitudHipocentro,
        double valorMagnitud,
        CambioEstado cambioEstado,
        Estado estadoActual,
        List<SerieTemporal> seriesTemporales,
        AlcanceSismo alcanceSismo,
        OrigenDeGeneracion origenDeGeneracionSismo,
        ClasificacionSismo clasificacionSismo)
    {
        FechaHoraOcurrencia = fechaHoraOcurrencia;
        FechaHoraFin = fechaHoraFin;
        LatitudEpicentro = latitudEpicentro;
        LongitudEpicentro = longitudEpicentro;
        LatitudHipocentro = latitudHipocentro;
        LongitudHipocentro = longitudHipocentro;
        ValorMagnitud = valorMagnitud;
        CambioEstado = cambioEstado;
        this.estadoActual = estadoActual;
        this.seriesTemporales = seriesTemporales;
        this.alcance = alcanceSismo;
        this.origenDeGeneracion = origenDeGeneracionSismo;
        this.clasificacion = clasificacionSismo;
    }

    public EventoSismico(DataRow data)
    {
        FechaHoraOcurrencia = Convert.ToDateTime(data["fecha_hora_ocurrencia"]);
        FechaHoraFin = Convert.ToDateTime(data["fecha_hora_fin"]);
        LatitudEpicentro = Convert.ToDouble(data["latitud_epicentro"]);
        LongitudEpicentro = Convert.ToDouble(data["longitud_epicentro"]);
        LatitudHipocentro = Convert.ToDouble(data["latitud_hipocentro"]);
        LongitudHipocentro = Convert.ToDouble(data["longitud_hipocentro"]);
        ValorMagnitud = Convert.ToDouble(data["valor_magnitud"]);
    }

    public static List<CambioEstado> recuperarCambiosEstados(int id)
    {
        GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
        DataTable respuesta = generalAdapterSQL.EjecutarVista("CambioEstado WHERE "+ , id);
        List<CambioEstado> listaCambiosEstados = new List<CambioEstado>();
        if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0]["RESULTADO"].ToString() != "ERROR")
        {
            foreach (DataRow item in respuesta.Rows)
            {
                // Crear instancia de CambioEstado a partir de DataRow
                CambioEstado cambioEstado = new CambioEstado(
                    Convert.ToDateTime(item["fecha_hora_inicio"]),
                    item["fecha_hora_fin"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(item["fecha_hora_fin"]) : null,
                    new Estado(item["ambito"].ToString(), item["nombre_estado"].ToString())
                );
                listaCambiosEstados.Add(cambioEstado);
            }
        }
        return listaCambiosEstados;
    }

    public static List<EventoSismico> obtenerTodoEventoSismico()
    {
        GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
        DataTable respuesta = generalAdapterSQL.EjecutarVista("EventoSismico");
        List<EventoSismico> listaEventos = new List<EventoSismico>();

        if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0]["RESULTADO"].ToString() == "ERROR")
        {
            
            //Hubo un error al consultar la base de datos
            foreach (DataRow item in respuesta.Rows)
            {
                listaEventos.Add(new EventoSismico(item));
            }
 
        }
        return listaEventos;
    }

    public bool esPendienteDeRevision()
    {
        if (this.estadoActual.esPendienteDeRevision())
        {
            return true; // El evento sismico esta pendiente de revision
        }

        return false; // El evento sismico no esta pendiente de revision
    }


    public CambioEstado buscarCambioEstadoAbierto(DateTime fechaHoraActual)
    {
        return this.CambioEstado.sosActual(fechaHoraActual);// busco el cambio de estado abierto del evento sismico
    }

    public  void actualizarCambioEstado(DateTime fechaHoraActual)
    {
       // this.estadoActual.cambiarEstadoEventoSismico(fechaHoraActual);
    }

    public CambioEstado crearCambioEstado(Estado estado, DateTime fechaHoraInicio)
    {
        //creo el nuevo cambio de estado del evento sismico
        CambioEstado = new CambioEstado(fechaHoraInicio, null, estado);

        this.CambioEstado = CambioEstado; // Actualiza el cambio de estado del evento sismico

        //TENGO QUE HACER SET ESTADO
        this.setEstado(estado); // Actualiza el estado actual del evento sismico

        return CambioEstado; // Retorna el cambio de estado creado

    }

    public void setEstado(Estado estado)
    {
        this.estadoActual = estado; // Actualiza el estado actual del evento sismico
    }

    public (string origen, string alcance, string clasificacion) getDetallesEventoSismico()
    {
        return (
            this.alcance.getNombre(),
            this.origenDeGeneracion.getNombre(),
            this.clasificacion.getNombre());
    }

    public List<SerieTemporal> buscarSeriesTemporal()
    {
        if (this.seriesTemporales == null || this.seriesTemporales.Count == 0)
        {
            throw new InvalidOperationException("No hay series temporales asociadas al evento sismico.");
        }
        else
        {
            return this.seriesTemporales; // Retorna la lista de series temporales asociadas al evento sismico
        }

    }

    public CambioEstado GetCambioEstado()
    {
        return CambioEstado;
    }

    public Estado GetEstadoActual()
    {
        return estadoActual;
    }

    public Array getCordenadasHipocentro()
    {
        return new double[] { LatitudHipocentro, LongitudHipocentro };
    }

    public Array getCordenadasEpicentro()
    {
        return new double[] { LatitudEpicentro, LongitudEpicentro };
    }

    public double getMagnitud()
    {
        return ValorMagnitud; // Retorna la magnitud del evento sismico
    }

    public DateTime getFechaHoraOcurrencia()
    {
        return FechaHoraOcurrencia; // Retorna la fecha y hora de ocurrencia del evento sismico
    }

    public DateTime getFechaHoraFin()
    {
        return FechaHoraFin; // Retorna la fecha y hora de fin del evento sismico
    }

    public List<SerieTemporal> getSeriesTemporales()
    {
        return seriesTemporales; // Retorna la lista de series temporales asociadas al evento sismico
    }
}


