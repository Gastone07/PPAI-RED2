using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI_REDSISMICA.Entidades
{
    public class Usuario
    {
        public string contraseña { get; set; }

        public string nombreUsuario { get; set; }

        public Usuario(string nombreUsuario, string contraseña)
        {
            this.nombreUsuario = nombreUsuario;
            this.contraseña = contraseña;
        }
        
        public Usuario()
        {
            // Constructor por defecto
        }

        public Usuario obtenerLogueado()
        {
            return this;
        }


    }
}
