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
    internal class OrigenDeGeneracionPersistencia
    {

        public static OrigenDeGeneracion recuperarOrigenDeGeneracionXID(int id)
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("Origenes WHERE idEventoSismico = " + id);
            OrigenDeGeneracion origen = new OrigenDeGeneracion();

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                string nombre = respuesta.Rows[0][0].ToString() ?? "";
                string descripcion = respuesta.Rows[0][1].ToString() ?? "";

                origen = new(nombre, descripcion);

            }
            return origen;
        }
    }
}
