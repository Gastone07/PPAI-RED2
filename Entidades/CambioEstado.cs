using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.Entidades
{
    public class CambioEstado
    {
        private DateTime fechaHoraInicio { get; set; }

        private DateTime? fechaHoraFin { get; set; }
        private Estado estado { get; set; }

        private int idCambioEstado { get; set; }

        public CambioEstado(DateTime fechaHoraInicio, DateTime? fechaHoraFin, Estado estado)
        {
            this.fechaHoraInicio = fechaHoraInicio;
            this.fechaHoraFin = fechaHoraFin;
            this.estado = estado;
        }

        public CambioEstado()
        {
            // Constructor por defecto
        }

        public CambioEstado sosActual(DateTime fechaHoraActual)
        {
            this.setFechaHoraFin(fechaHoraActual);
            return this; // Retorna el primer cambio de estado abierto encontrado
        }

        public void setFechaHoraFin(DateTime fecha)
        {
           fechaHoraFin = fecha;
        }

        public CambioEstado(DataRow data)
        {
            this.fechaHoraFin = data["fechaHoraFin"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(data["fechaHoraFin"]) : null;
            this.fechaHoraInicio = Convert.ToDateTime(data["fechaHoraInicio"]);
            this.estado = Estado.recuperarEstadoXID((int)data["idEstado"]);
            this.idCambioEstado = Convert.ToInt32(data["idCambioEstado"]);
        }

        public static List<CambioEstado> obtenerCambiosEstados()
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("CambiosEstado");
            List<CambioEstado> listaCambioEstado = new List<CambioEstado>();

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {

                //Hubo un error al consultar la base de datos
                foreach (DataRow item in respuesta.Rows)
                {
                    listaCambioEstado.Add(new CambioEstado(item));
                }

            }
            return listaCambioEstado;
        }


        public bool actualizarCambioEstado()
        {
            string consulta = "UPDATE CambiosEstado SET fechaHoraFin = " + this.fechaHoraFin + " WHERE IdCambioEstado = " +  this.idCambioEstado + ";" +
                "Select * from CambiosEstado where IdCambioEstado = " + this.idCambioEstado;

            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarQuery(consulta);
            
            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR") return true;
            else return false;



        }

    }
}
