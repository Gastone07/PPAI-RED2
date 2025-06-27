using System.Windows;

namespace PPAI_REDSISMICA
{
    public partial class VentanaRevisionManual : Window
    {
        public VentanaRevisionManual()
        {
            InitializeComponent();
        }

        // Método para instanciar y mostrar la pantalla para su posterior uso
        public static VentanaRevisionManual Habilitar()
        {
            return new VentanaRevisionManual();         
        }
    }
}