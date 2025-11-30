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
    internal class ClasificionSismoPersistencia
    {
        public static ClasificacionSismo recuperarClasificacionSismoXID(int id)
        {

            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("Clasificaciones WHERE idEventoSismico = " + id);
            ClasificacionSismo clasificiacion = new ClasificacionSismo();
            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                int kmProfundidadDesde = int.Parse(respuesta.Rows[0][0].ToString() ?? "");
                int kmProfundidadHasta = int.Parse(respuesta.Rows[0][1].ToString() ?? "");
                string nombre = respuesta.Rows[0][2].ToString() ?? "";

                clasificiacion = new(kmProfundidadDesde, kmProfundidadHasta, nombre);

            }
            return clasificiacion;
        }
    }
}
