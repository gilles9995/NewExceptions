using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoutenAfhandeling.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string linksGetal, rechtsGetal;

        public MainWindow()
        {
            InitializeComponent();
            txtGetal1.Focus();
        }

        string VoerBerekeningUit(string input1, string input2, string bewerking, TextBlock errorTextBlock)
        {
            string resultaat = "";
            string errorMessage = "";
            errorTextBlock.Text = errorMessage;
            int getal1, getal2;

            try
            {
            getal1 = int.Parse(input1);
             getal2 = int.Parse(input2); if (bewerking == "/")
            {
                float quotient;
                quotient = getal1 / getal2;
                resultaat = quotient.ToString();
                if (getal1 % getal2 > 0)
                {
                    resultaat += " rest " + getal1 % getal2;
                }
            }
            else if (bewerking == "+")
            {
                    if (getal1 < 0 || getal2 < 0 )
                    {
                        throw new ArgumentOutOfRangeException("Wil enkel positieve getallen intikken");
                    }
                int som = TelPositieveGetallenOp(getal1, getal2);
                resultaat = som.ToString();
            }
            }
            catch (DivideByZeroException zEx)
            {
                errorMessage = "Je probeert door nul te delen. " +
                    "Ga terug naar de lagere school en kies een andere deler\n";
                errorMessage += "Details: " + zEx.Message + "\n";
                errorTextBlock.Text = errorMessage;
                txtGetal2.Text = "1";
                
            }

            catch (FormatException fEx)
            {
                errorMessage = "Wil enkel positieve gehele getallen invoeren\n";
                errorMessage += "Details: " + fEx.Message + "\n";
                errorTextBlock.Text = errorMessage;

            }

            catch (ArgumentOutOfRangeException oEx)
            {
                errorMessage = "Wil enkel positieve gehele getallen invoeren\n";
                errorMessage = oEx.ParamName;
                errorTextBlock.Text = errorMessage;
            }
                
            catch (Exception gEx)
            {
                errorMessage = "Er is een fout opgetreden\n";
               
                    throw;
            }
           
            finally
            {
                Console.WriteLine("Laatste uitvoering:" + DateTime.Now.ToString("dd/MM:yyyy HH:mm:ss"));
                //txtGetal1.focus()
            }
            

            return resultaat;
        }

        void LeesInput()
        {
            linksGetal = txtGetal1.Text;
            rechtsGetal = txtGetal2.Text;
            lblResultaat.Content = "";
            tbErrors.Text = "";
        }

        int TelPositieveGetallenOp(int getal1, int getal2)
        {
            //Grenzen integer: 2147483647 en -2147483648 
            int som = checked( getal1 + getal2);
            return som;
        }

        private void btnTelOp_Click(object sender, RoutedEventArgs e)
        {
            string uitkomst = "";
            LeesInput();
            uitkomst = VoerBerekeningUit(linksGetal, rechtsGetal, "+", tbErrors);
            lblResultaat.Content = uitkomst;
        }

        private void btnDeel_Click(object sender, RoutedEventArgs e)
        {
            string uitkomst = "";
            LeesInput();
            uitkomst = VoerBerekeningUit(linksGetal, rechtsGetal, "/", tbErrors);
            lblResultaat.Content = uitkomst;
        }
    }
}
