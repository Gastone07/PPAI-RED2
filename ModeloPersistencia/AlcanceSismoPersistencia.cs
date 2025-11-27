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
    internal class AlcanceSismoPersistencia
    {
        public static AlcanceSismo recuperarAlcanceSismoXID(int id)
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("Alcance WHERE idSismo = " + id);
            AlcanceSismo alcance = new AlcanceSismo();
            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                string nombre = respuesta.Rows[0][0].ToString() ?? "";
                string descripcion = respuesta.Rows[0][1].ToString() ?? "";
                alcance = new(nombre, descripcion);

            }
            return alcance;

        }

    }
}
