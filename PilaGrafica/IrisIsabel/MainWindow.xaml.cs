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

namespace IrisIsabel
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void validacioNotaButton_Click(object sender, RoutedEventArgs e)
        {
            string infijo = inputNotaText.Text;

            if (string.IsNullOrWhiteSpace(infijo))
            {
                MessageBox.Show("Si us plau, introdueix una expressió infix.", "Advertència", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string postfijo = NotacioPolaca.InfijoAPostfijo(infijo);
                outputNotaText.Text = postfijo;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en convertir l'expressió: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}