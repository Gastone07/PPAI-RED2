using PPAI_REDSISMICA.Entidades;
using System;
using System.Collections.Generic;

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

    public EventoSismico()
    {
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


