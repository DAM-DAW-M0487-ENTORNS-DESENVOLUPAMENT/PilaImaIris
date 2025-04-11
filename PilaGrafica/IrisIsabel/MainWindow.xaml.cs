using IrisIsabel.Control;
using Microsoft.Win32;
using System.Collections.ObjectModel;
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
using System.IO;

namespace IrisIsabel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Compilador compilador = new Compilador();
        //aixo es per a que es vegi
        private ObservableCollection<ExpressioFila> expressions = new ObservableCollection<ExpressioFila>();
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

        private void validacioCompButton_Click(object sender, RoutedEventArgs e)
        {
            string expressio = validarCompText.Text;
            bool esValid = compilador.Validar(expressio);
            validarCompCheck.IsChecked = esValid;
        }

        private void fitxerCompBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Fitxers de text (*.txt)|*.txt"
            };

            if (dlg.ShowDialog() == true)
            {
                string fitxerRuta = dlg.FileName;
                string[] linies = File.ReadAllLines(fitxerRuta);

                expressions.Clear(); 

                foreach (string linia in linies)
                {
                    bool valida = compilador.Validar(linia);
                    expressions.Add(new ExpressioFila { Expressio = linia, EsValid = valida });
                }

                CompiladorGrid.ItemsSource = expressions;
            }
        }

        public class ExpressioFila
        {
            public string Expressio { get; set; }
            public bool EsValid { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}