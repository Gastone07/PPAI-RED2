using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.Entidades
{
    public class OrigenDeGeneracion
    {
        private string descripcion;
        private string nombre;

        public OrigenDeGeneracion(string nombre, string descripcion)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
        }

        public OrigenDeGeneracion()
        {
        }   

        public string getNombre()
        {
            return nombre;
        }

        public OrigenDeGeneracion(DataRow data)
        {
            this.nombre = Convert.ToString(data["nombre"]) ?? string.Empty;
            this.descripcion = Convert.ToString(data["descripcion"]) ?? string.Empty;
        }

        public static OrigenDeGeneracion recuperarOrigenDeGeneracionXID(int id)
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("Origenes WHERE idEventoSismico = " + id);
            OrigenDeGeneracion origen = new OrigenDeGeneracion();
            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                origen = new(respuesta.Rows[0]);

            }
            return origen;
        }
    }
}
