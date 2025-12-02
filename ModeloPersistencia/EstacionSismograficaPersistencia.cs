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
    internal class EstacionSismograficaPersistencia
    {
        public static EstacionSismografica obtenerEstacionXID(int id)
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("Estaciones WHERE codigoEstacion = " + id);


            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {

                EstacionSismografica estacion = new EstacionSismografica(
                int.Parse(respuesta.Rows[0][0].ToString() ?? ""),
                respuesta.Rows[0][1].ToString() ?? "",
                DateTime.Parse(respuesta.Rows[0][2].ToString() ?? ""),
                double.Parse(respuesta.Rows[0][3].ToString() ?? ""),
                double.Parse(respuesta.Rows[0][4].ToString() ?? ""),
                respuesta.Rows[0][5].ToString() ?? "",
                int.Parse(respuesta.Rows[0][6].ToString() ?? "")
                );

                return estacion;
            }
            else 
            {
                return new EstacionSismografica(-1,"", DateTime.Now, 0.0, 0.0, "", 0);
            }  
                
        }
    }
}
