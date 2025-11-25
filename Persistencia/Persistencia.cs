using PPAI_REDSISMICA.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
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
            Sismografo sismografo1 = new Sismografo(DateTime.Now, 1, 1999, estacion1, [serieTemporal1, serieTemporal2]);
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

        public static (List<EventoSismico>, List<Estado>, List<CambioEstado>, List<Sesion>, List<Sismografo>) ObtenerDatos2()
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta =  generalAdapterSQL.EjecutarVista("EventoSismico");
            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0]["RESULTADO"].ToString() == "ERROR")
            {
                List<EventoSismico> listaEventos = new List<EventoSismico>();
                
                //Hubo un error al consultar la base de datos
                foreach (DataRow item  in respuesta.Rows)
                {
                    listaEventos.Add(new EventoSismico(item));
                }

                return (new List<EventoSismico>(), new List<Estado>(), new List<CambioEstado>(), new List<Sesion>(), new List<Sismografo>());
            }

            return (new List<EventoSismico>(), new List<Estado>(), new List<CambioEstado>(), new List<Sesion>(), new List<Sismografo>());
        }


        public class GeneralAdapterSQL
    {
        /// <summary>
        /// La cadena de conexion que esta usando actualmente nuestra API
        /// </summary>
        private static string? CadenaConexion = null;
        /// <summary>
        /// La configuracion que tenemos en nuestra API
        /// </summary>
        private static SettingsReader? Configuracion = null;
        /// <summary>
        /// Metodo que recupera la configuracion de nuestro appsettings.json
        /// </summary>
        private static SettingsReader ObtenerConfiguracion() => SettingsReader.GetAppSettings();
        /// <summary>
        /// Recupera la cadena de conexion desde nuestra configuracion
        /// </summary>
        private static void ObtenerCadenaConexion()
        {
            if (CadenaConexion != null && CadenaConexion.Trim() != "") return;
            //Este if no tiene el else porque una vez que recupera la conexion deberia continuar normalmente
            if (Configuracion == null) Configuracion = ObtenerConfiguracion();

            //En esta situacion siempre esta asignado algun valor a Configuracion

            //Comprueba que exista un valor de env y una clave para este entorno
            if (Configuracion.Env.Trim() != "" && Configuracion.ConnectionStrings.ContainsKey(Configuracion.Env))
                CadenaConexion = Configuracion.ConnectionStrings[Configuracion.Env];
            else CadenaConexion = null;
        }
        /// <summary>
        /// Con el nombre de la vista permite recuperar los valores que contiene
        /// </summary>
        /// <param name="vista">El nombre de la vista que queremos consultar</param>
        /// <returns>Una tabla con valores</returns>
        public DataTable EjecutarVista(string vista)
        {
            ObtenerCadenaConexion();
            //Es necesario ponerlo por fuera para poder usar el bloque finally
            using SqlConnection conexionBase = new(CadenaConexion);
            DataTable respuesta = new();
            try
            {
                //Con esto recupera la informacion de la vista
                string consulta = "SELECT * FROM " + vista;
                using var comando = new SqlCommand(consulta, conexionBase);//Creamos el comando en la base con la conexion
                comando.CommandType = CommandType.Text;//Notificamos a la base que vamos a enviar
                SqlDataAdapter Adaptador = new(comando);
                conexionBase.Open(); //Abre la conexion (Puede fallar)
                Adaptador.Fill(respuesta); //Contacta la base y ejecuta el comando
            }
            catch (Exception ex)
            {   
                //Esta parte es para devolver un codigo de error al endpoint
                respuesta.Columns.Add("RESULTADO");
                respuesta.Rows.Add("ERROR");
            }
            //Lo va a ejecutar no importa que parte del codigo realice
            finally
            {
                //Limpia cualquier cadena de conexion que tengamos
                SqlConnection.ClearAllPools();
                //Cierra la base de datos siempre
                conexionBase.Close();
            }
            return respuesta;
        }
        /// <summary>
        /// Es un esquema de conversion de tipo de variables C# a tipo de variables SQL
        /// </summary>
        /// <param name="variable">La variable que usamos de parametro</param>
        /// <returns>Un tipo de base de datos SQL para ejecutar el procedimiento</returns>
        private static SqlDbType GetDBType(object variable)
        {
            string name = variable.GetType().Name;
            switch (variable.GetType().Name)
            {
                case "Int32":
                    return SqlDbType.Int;
                case "String":
                    return SqlDbType.VarChar;
                case "DateTime":
                    return SqlDbType.DateTime;
                //Lo agregue despues
                case "TimeOnly":
                    return SqlDbType.Time;
                case "Single"://Considera valores con un decimal como single
                    return SqlDbType.Decimal;
                case "Decimal":
                    return SqlDbType.Decimal;
                case "Float":
                    return SqlDbType.Float;
                case "Bool":
                    return SqlDbType.Bit;
                //Es muy probable que la base de datos pueda convertir de string a cualquier tipo de variable
                default:
                    return SqlDbType.VarChar;
            }
        }


        /// <summary>
        /// Metodo para ejecutar procedimientos en la base de datos de manera comoda
        /// </summary>
        /// <param name="procedimiento">Nombre del procedimiento</param>
        /// <param name="parametros">Un diccionario con los parametros con su nombre en el procedimiento</param>
        /// <returns>Una tabla con la respuesta del procedimiento</returns>
        public DataTable ExecuteStoredProcedure(string procedimiento, Dictionary<string, object?> parametros)
        {
            ObtenerCadenaConexion();
            //Es necesario ponerlo por fuera para poder usar el bloque finally
            using SqlConnection conexionBase = new(CadenaConexion);
            DataTable respuesta = new();
            try
            {
                using var comando = new SqlCommand(procedimiento, conexionBase);//Creamos el comando en la base con la conexion
                comando.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter Adaptador = new(comando);
                foreach (var item in parametros)
                {
                    if (item.Value == null || item.Value.ToString()?.Trim() == "NULL")
                    {
                        comando.Parameters.AddWithValue(item.Key, DBNull.Value);
                    }
                    else
                    {
                        comando.Parameters.Add(item.Key, GetDBType(item.Value));
                        comando.Parameters[item.Key].Value = item.Value;
                    }
                }
                conexionBase.Open();
                Adaptador.Fill(respuesta);
            }
            catch (Exception ex)
            {
                //Registramos el error en nuestra carpeta
                //Esta parte es para devolver un codigo de error al endpoint
                respuesta.Columns.Add("RESULTADO");
                respuesta.Rows.Add("ERROR");
            }
            //Lo va a ejecutar no importa que parte del codigo realice
            finally
            {
                //Limpia cualquier cadena de conexion que tengamos
                SqlConnection.ClearAllPools();
                //Cierra la base de datos siempre
                conexionBase.Close();
            }
            return respuesta;
        }
        /// <summary>
        /// Este metodo nos permite ejecutar un conjunto de procedimientos con su parametros en un bloque TRANSACT
        /// </summary>
        /// <param name="transaccionPropia">Es un objeto que contiene la informacion para realizar la transaccion</param>
        /// <returns>Un boleano dependiendo si se completo exitosamente o no la accion</returns>
        public bool EjecutarTransaccion(TransaccionSQL transaccionPropia)
        {
            ObtenerCadenaConexion();

            DataTable respuesta = new();
            bool resultado = true;

            using (var conexionBase = new SqlConnection(CadenaConexion))
            {
                conexionBase.Open();

                //Iniciamos la transaccion
                using var transaccionSQL = conexionBase.BeginTransaction(transaccionPropia.nombre_transaccion);
                try
                {
                    //Recorro todos los procedimientos en orden
                    for (int i = 0; i < transaccionPropia.procedimientos.Count; i++)
                    {
                        string procedimiento = transaccionPropia.procedimientos[i];
                        Dictionary<string, object> parametros = transaccionPropia.parametros[i];

                        respuesta = new();
                        using var comando = new SqlCommand(procedimiento, conexionBase)
                        {
                            CommandType = CommandType.StoredProcedure,
                            Transaction = transaccionSQL
                        };

                        SqlDataAdapter adapter = new(comando);

                        foreach (var item in parametros)
                        {
                            if (item.Value == null || item.Value.ToString()?.Trim() == "NULL")
                            {
                                comando.Parameters.AddWithValue(item.Key, DBNull.Value);
                            }
                            else
                            {
                                comando.Parameters.Add(item.Key, GetDBType(item.Value));
                                comando.Parameters[item.Key].Value = item.Value;
                            }
                        }

                        adapter.Fill(respuesta);

                        //En todas las respuestas debemos devolver algo
                        //Podemos elegir que sea un codigo o que sea la tabla
                        if (respuesta.Rows.Count == 0)
                        {
                            respuesta = new();
                            //Esto es para que la API pueda devolver un conflict() (409)
                            respuesta.Columns.Add("MENSAJE");
                            respuesta.Rows.Add("ERROR");
                            //Si falla hacemos un rollback y rompemos le ciclo con return
                            transaccionSQL.Rollback();
                            resultado = false;
                            break;
                        }
                    }
                    if (resultado) transaccionSQL.Commit();

                }
                catch (Exception ex)
                {
                    respuesta = new();
                    //Esto es para que la API pueda devolver un conflict() (409)
                    respuesta.Columns.Add("MENSAJE");
                    respuesta.Rows.Add("ERROR");
                    //Registramos en el Log el error
                    transaccionSQL.Rollback();
                    resultado = false;
                }
                finally
                {
                    SqlConnection.ClearAllPools();
                    conexionBase.Close();
                }

            }
            ;

            return resultado;
        }
    }
        /// <summary>
        /// Clase que nos permite manipular una transaccion
        /// </summary>
        public class TransaccionSQL
        {
            /// <summary>
            /// El nombre que le vamos a asignar a esta operacion en la base de datos
            /// </summary>
            public string nombre_transaccion { get; set; }
            /// <summary>
            /// Es el nombre de los procedimientos que queremos ejecutar
            /// </summary>
            public List<string> procedimientos { get; set; }
            /// <summary>
            /// Es un listado de parametros asociados a cada procedimiento
            /// </summary>
            public List<Dictionary<string, object>> parametros { get; set; }
            //No los creo dentro de un diccionario porque se hacia muy complejo de explicar. Pero quedaria como:
            //Dictionary<string,Dictionary<string, object>>

            /// <summary>
            /// Constructor Generico del controlador
            /// </summary>
            public TransaccionSQL(string nombreTransaccion)
            {
                this.nombre_transaccion = nombreTransaccion;
                this.procedimientos = new();
                this.parametros = new();
            }
        }

    }

}

