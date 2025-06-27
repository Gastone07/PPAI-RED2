using Controladores;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vistas;

namespace PPAI_REDSISMICA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRegistrarRevision_Click(object sender, RoutedEventArgs e)
        {
            pantallaRegistrarResultadoRevisionManual pantalla = new pantallaRegistrarResultadoRevisionManual();

            pantalla.Show(); // Mostrar la pantalla

        }

        private void btnTexto1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Te la creiste We.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnTexto2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Te la creiste We.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}