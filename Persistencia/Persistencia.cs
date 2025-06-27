using PPAI_REDSISMICA.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PPAI_REDSISMICA.Persistencia
{
    public static class Persistencia
    {
        public static (List<EventoSismico>, List<Estado>, List<CambioEstado>, List<Sesion>, List<Sismografo>) ObtenerDatos()
        {
            // Simulación de datos para corregir el error CS0234
            var eventosSismicos = new List<EventoSismico>();
            var listadoEstado = new List<Estado>();
            var listadoCambiosEstado = new List<CambioEstado>();
            var listadoSesiones = new List<Sesion>();

            //tipo dato
            var tipoDato1 = new TipoDeDato("Velocidad Onda", "Km/seg", 8.0);
            var tipoDato2 = new TipoDeDato("Frecuencia de onda", "Hz", 15.0);
            var tipoDato3 = new TipoDeDato("Longitud", "Km/ciclo", 1.0);

            //Detalle muestra sismica 
            var detalleMuestra1 = new DetalleMuestraSismica(7, tipoDato1);
            var detalleMuestra2 = new DetalleMuestraSismica(10, tipoDato2);
            var detalleMuestra3 = new DetalleMuestraSismica(0.7, tipoDato3);

            var detalleMuestra4 = new DetalleMuestraSismica(7.02, tipoDato1);
            var detalleMuestra5 = new DetalleMuestraSismica(10, tipoDato2);
            var detalleMuestra6 = new DetalleMuestraSismica(0.69, tipoDato3);

            var detalleMuestra7 = new DetalleMuestraSismica(6.99, tipoDato1);
            var detalleMuestra8 = new DetalleMuestraSismica(10.01, tipoDato2);
            var detalleMuestra9 = new DetalleMuestraSismica(0.7, tipoDato3);

            var detalleMuestra10 = new DetalleMuestraSismica(5.01, tipoDato1);
            var detalleMuestra11 = new DetalleMuestraSismica(9.82, tipoDato2);
            var detalleMuestra12 = new DetalleMuestraSismica(0.33, tipoDato3);

            var detalleMuestra13 = new DetalleMuestraSismica(7.36, tipoDato1);
            var detalleMuestra14 = new DetalleMuestraSismica(6.12, tipoDato2);
            var detalleMuestra15 = new DetalleMuestraSismica(0.14, tipoDato3);

            //Muestra sismica 
            var muestraSismica1 = new MuestraSismica(
                new DateTime(2024, 6, 11, 14, 30, 0),
                [detalleMuestra1, detalleMuestra2, detalleMuestra3]
            );
            var muestraSismica2 = new MuestraSismica(
                new DateTime(2024, 6, 11, 14, 35, 0),
                [detalleMuestra4, detalleMuestra5, detalleMuestra6]
            );
            var muestraSismica3 = new MuestraSismica(
                new DateTime(2024, 6, 11, 14, 40, 0),
                [detalleMuestra7, detalleMuestra8, detalleMuestra9]
            );
            var muestraSismica4 = new MuestraSismica(
                new DateTime(2024, 6, 11, 14, 45, 0),
                [detalleMuestra10, detalleMuestra11, detalleMuestra12]
            );
            var muestraSismica5 = new MuestraSismica(
                new DateTime(2024, 8, 12, 14, 50, 0),
                [detalleMuestra13, detalleMuestra14, detalleMuestra15]
            );

            //Estacion Sismografica
            EstacionSismografica estacion1 = new EstacionSismografica(1, "DocumentoCertificacion1", DateTime.Now, 48.99, 29.01, "La boca", 1);
            EstacionSismografica estacion2 = new EstacionSismografica(2, "DocumentoCertificacion2", DateTime.Now, 19.44, 07.58, "Capital", 2);


            //Series temporales
            var serieTemporal1 = new SerieTemporal(false, DateTime.Now, DateTime.Now, 50, [muestraSismica1, muestraSismica2, muestraSismica3]);
            var serieTemporal2 = new SerieTemporal(false, DateTime.Now, DateTime.Now, 50, [muestraSismica4]);
            var serieTemporal3 = new SerieTemporal(false, DateTime.Now, DateTime.Now, 50, [muestraSismica5]);

            //sismografo
            Sismografo sismografo1 = new Sismografo(DateTime.Now, 1, 1999, estacion1, [serieTemporal1,serieTemporal2]);
            Sismografo sismografo2 = new Sismografo(DateTime.Now, 2, 2000, estacion2, [serieTemporal3]);

            var listadoSismografos = new List<Sismografo>
            {
                sismografo1,
                sismografo2
            };

            //Estados
            var estadoNoRevisado = new Estado("evento", "NoRevisado");
            var estadoRevisado = new Estado("evento", "Revisado");
            var estadoBloqueado = new Estado("evento", "BloqueadoEnRevision");
            var estadoRechazado = new Estado("evento", "Rechazado");
            var estadoConfirmado = new Estado("evento", "Confirmado");
            var estadoRevisadoExperto = new Estado("evento", "RevisadoPorExperto");
            var estadoBloqueado2 = new Estado("sismografo", "SismografoBloqueado");
            var estadoBloqueado3 = new Estado("sismografo", "SismografoDisponible");

            listadoEstado.Add(estadoNoRevisado);
            listadoEstado.Add(estadoRevisado);
            listadoEstado.Add(estadoBloqueado);
            listadoEstado.Add(estadoBloqueado2);
            listadoEstado.Add(estadoBloqueado3);
            listadoEstado.Add(estadoRechazado);
            listadoEstado.Add(estadoConfirmado);
            listadoEstado.Add(estadoRevisadoExperto);

            //clasificacion
            ClasificacionSismo clasificacionSismo1 = new ClasificacionSismo(0, 70, "Superficial");
            ClasificacionSismo clasificacionSismo2 = new ClasificacionSismo(70, 300, "Intermedio");
            ClasificacionSismo clasificacionSismo3 = new ClasificacionSismo(300, 700, "profundo");

            //Origen de Generacion
            OrigenDeGeneracion origenGeneracion1 = new OrigenDeGeneracion("Tectonico", "Movimientos de las placas tectónicas en la corteza terrestre, causados por la acumulación y liberación de energía en fallas geológicas");
            OrigenDeGeneracion origenGeneracion2 = new OrigenDeGeneracion("Volcanico", "Actividad volcánica, como el movimiento de magma, gases o fracturamiento de roca en cámaras magmáticas.");
            OrigenDeGeneracion origenGeneracion3 = new OrigenDeGeneracion("Inducido", "Actividades humanas, como la extracción de petróleo o gas, la inyección de fluidos en el subsuelo (fracking), la construcción de embalses o la minería.");

            // Crear Alcance Sismo
            AlcanceSismo alcanceSismo1 = new AlcanceSismo("Local", "Afecta una región geográfica limitada, como una ciudad o un área metropolitana.");
            AlcanceSismo alcanceSismo2 = new AlcanceSismo("Regional", "Afectan una región más amplia, abarcando decenas o cientos de kilómetros desde el epicentro.");
            AlcanceSismo alcanceSismo3 = new AlcanceSismo("Telurico", "Pueden sentirse a cientos o miles de kilómetros del epicentro, afectando grandes regiones o incluso países.");

            // Cambios de Estado
            var cambioEstado1 = new CambioEstado(
                new DateTime(2024, 6, 11, 14, 30, 0),
                new DateTime(2024, 6, 11, 14, 45, 0),
                estadoNoRevisado
            );
            var cambioEstado2 = new CambioEstado(
                new DateTime(2024, 6, 23, 9, 15, 0),
                new DateTime(2024, 6, 23, 9, 30, 0),
                estadoNoRevisado
            );
            var cambioEstado3 = new CambioEstado(
                new DateTime(2024, 6, 19, 22, 5, 0),
                new DateTime(2024, 6, 19, 22, 20, 0),
                estadoNoRevisado
            );
            var cambioEstado4 = new CambioEstado(
                new DateTime(2024, 6, 29, 3, 50, 0),
                new DateTime(2024, 6, 29, 14, 5, 0),
                estadoNoRevisado
            );
            var cambioEstado5 = new CambioEstado(
                new DateTime(2024, 6, 29, 3, 50, 0),
                new DateTime(2024, 6, 29, 14, 5, 0),
                estadoRevisado
            );
            var cambioEstado6 = new CambioEstado(
                new DateTime(2025, 6, 29, 3, 50, 0),
                new DateTime(2025, 6, 29, 14, 5, 0),
                estadoRevisado
            );

            listadoCambiosEstado.Add(cambioEstado1);
            listadoCambiosEstado.Add(cambioEstado2);
            listadoCambiosEstado.Add(cambioEstado3);
            listadoCambiosEstado.Add(cambioEstado4);
            listadoCambiosEstado.Add(cambioEstado5);
            listadoCambiosEstado.Add(cambioEstado6);

            // Usuario
            var usuario1 = new Usuario("12345", "Serna");
            var usuario2 = new Usuario("bocaboca", "Roman");

            var sesion1 = new Sesion(usuario2, DateTime.Now.AddHours(-1), DateTime.Now);
            var sesionActual = new Sesion(usuario1, DateTime.Now, null);

            listadoSesiones.Add(sesion1);
            listadoSesiones.Add(sesionActual);

            // Eventos Sísmicos
            eventosSismicos.Add(new EventoSismico(
                new DateTime(2024, 6, 11, 14, 30, 0),
                new DateTime(2024, 6, 11, 14, 45, 0),
                34.6037,
                58.3816,
                34.6040,
                58.3820,
                5.2,
                cambioEstado1,
                estadoNoRevisado,
                [serieTemporal1],
                alcanceSismo1, origenGeneracion1, clasificacionSismo1
            ));
            eventosSismicos.Add(new EventoSismico(
                new DateTime(2024, 6, 23, 9, 15, 0),
                new DateTime(2024, 6, 23, 9, 30, 0),
                31.4201,
                64.1888,
                31.4210,
                64.1895,
                4.8,
                cambioEstado2,
                estadoNoRevisado, [serieTemporal2, serieTemporal3],
                alcanceSismo2, origenGeneracion2, clasificacionSismo2
            ));
            eventosSismicos.Add(new EventoSismico(
                new DateTime(2024, 6, 19, 22, 5, 0),
                new DateTime(2024, 6, 19, 22, 20, 0),
                32.9471,
                60.6505,
                32.9480,
                60.6510,
                6.1,
                cambioEstado3,
                estadoNoRevisado, null,
                alcanceSismo3, origenGeneracion3, clasificacionSismo3
            ));
            eventosSismicos.Add(new EventoSismico(
                new DateTime(2024, 6, 29, 3, 50, 0),
                new DateTime(2024, 6, 29, 14, 5, 0),
                24.7821,
                65.4232,
                24.7830,
                65.4240,
                5.7,
                cambioEstado4,
                estadoNoRevisado, null,
                alcanceSismo1, origenGeneracion1, clasificacionSismo1
            ));
            //evetos "revisados"
            eventosSismicos.Add(new EventoSismico(
                new DateTime(2024, 6, 29, 3, 50, 0),
                new DateTime(2024, 6, 29, 14, 5, 0),
                24.7821,
                65.4232,
                24.7830,
                65.4240,
                5.7,
                cambioEstado5,
                estadoRevisado, null,
                alcanceSismo2, origenGeneracion2, clasificacionSismo2
            ));
            eventosSismicos.Add(new EventoSismico(
                new DateTime(2025, 6, 29, 3, 50, 0),
                new DateTime(2025, 6, 29, 14, 5, 0),
                24.7821,
                65.4232,
                24.7830,
                65.4240,
                5.7,
                cambioEstado6,
                estadoRevisado, null,
                alcanceSismo3, origenGeneracion3, clasificacionSismo3
            ));

            // Ordenar por fecha de ocurrencia
            return (
                eventosSismicos.OrderBy(e => e.FechaHoraOcurrencia).ToList(),
                listadoEstado,
                listadoCambiosEstado,
                listadoSesiones,
                listadoSismografos
            );
        }
    }
}