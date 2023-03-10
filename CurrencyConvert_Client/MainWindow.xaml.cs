using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
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

namespace CurrencyConvert_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            lblResult.Content = string.Empty;
        }

        private async void CurrencyConverter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInputCurrency.Text))
                {
                    MessageBox.Show("Please enter currency");
                    lblResult.Content = string.Empty;
                    return;
                }


                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5157/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync($"CurrencyConverter?currency={txtInputCurrency.Text}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result.Replace('"', ' ');
                    lblResult.Content = responseData;
                    lblResult.Foreground = Brushes.Green;
                }
                else
                {
                    lblResult.Content = "Please enter currency value in numbers";
                    lblResult.Foreground = Brushes.Red;
                    lblResult.HorizontalAlignment = HorizontalAlignment.Center;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
