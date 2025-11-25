using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.Entidades
{
    public class ClasificacionSismo
    {
        private int kmProfundidadDesde;
        private int kmProfundidadHasta;
        private string nombre;


        public ClasificacionSismo(int kmProfundidadDesde, int kmProfundidadHasta, string nombre)
        {
            this.kmProfundidadDesde = kmProfundidadDesde;
            this.kmProfundidadHasta = kmProfundidadHasta;
            this.nombre = nombre;
        }

        public string getNombre()
        {
            return nombre;
        }

        public ClasificacionSismo()
        { 
        }
        public ClasificacionSismo(DataRow data)
        {
            this.kmProfundidadDesde = Convert.ToInt32(data["kmProfundidadDesde"]);
            this.kmProfundidadHasta = Convert.ToInt32(data["kmProfundidadHasta"]);
            this.nombre = Convert.ToString(data["nombre"]) ?? string.Empty;
        }
        public static ClasificacionSismo recuperarClasificacionSismoXID(int id)
        {

            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("Clasificaciones WHERE idEventoSismico = " + id);
            ClasificacionSismo clasificiacion = new ClasificacionSismo();
            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                clasificiacion = new(respuesta.Rows[0]);

            }
            return clasificiacion;
        }
    }
}
