using ClientPaymentSimulator.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Linq;

namespace ClientPaymentSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TokenDTO tokenDTO = new TokenDTO();
        HttpClient client = new HttpClient();
        

        public MainWindow()
        {
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri("https://localhost:44362/api/");
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenDTO.Token);
            //InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Login();
        }

        private async void Login()
        {
            Registersucces.Visibility = Visibility.Hidden;
            tokenDTO = new TokenDTO();
            LoginDTO model = new LoginDTO()
            {
                Username = textUserName.Text,
                Password = passwordBox.Password
            };

            try
            {
                var postData = JsonConvert.SerializeObject(model);
                JSONSent.Text = postData.ToString();
                var contentData = new StringContent(postData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("authentication/login", contentData);

                if (response.IsSuccessStatusCode)
                {
                    var stringData = await response.Content.ReadAsStringAsync();
                    tokenDTO = JsonConvert.DeserializeObject<TokenDTO>(stringData);
                    JSONReceived.Text = stringData.ToString();
                    textUser.Text = tokenDTO.Username;
                    textUserName.Text = "";
                    passwordBox.Password = "";
                }
                else
                {
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private void buttonPay1(object sender, RoutedEventArgs e)
        {
            this.Pay();
        }

        private async void Pay()
        {
            ComboBoxItem typeItem = (ComboBoxItem)ComboBox1.SelectedItem;
            string value = typeItem.Tag.ToString();
            int curr = int.Parse(value);

            decimal aman = decimal.Parse(amount23.Text.ToString());
            AmountInput amount = new AmountInput()
            {
                TypeCyrrency = curr,
                Amount = aman

            };

            try
            {
                client.DefaultRequestHeaders.Authorization = null;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenDTO.Token);
                var postData = JsonConvert.SerializeObject(amount);
                JSONSent.Text = postData.ToString();
                var contentData = new StringContent(postData, Encoding.UTF8, "application/json");
                string srtl = "customers/" + tokenDTO.Username;
                var response = await client.PutAsync(srtl, contentData);

                if (response.IsSuccessStatusCode)
                {
                    var stringData = await response.Content.ReadAsStringAsync();
                    int transactionId = JsonConvert.DeserializeObject<int>(stringData);
                    JSONReceived.Text = stringData.ToString();
                    trasactionText.Text = transactionId.ToString();
                    amount23.Text = "";
                }
                else
                {
                }
                

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private void GetBalance(object sender, RoutedEventArgs e)
        {
            this.GetBalance();
        }

        private async void GetBalance()
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = null;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenDTO.Token);
                var postData = JsonConvert.SerializeObject("");
                JSONSent.Text = postData.ToString();
                var contentData = new StringContent(postData, Encoding.UTF8, "application/json");

                var response = await client.GetAsync("customers/" + tokenDTO.Username);

                //var response = await client.GetStringAsync("customers/" + tokenDTO.Username);

                if (response.IsSuccessStatusCode)
                {
                    var stringData = await response.Content.ReadAsStringAsync();
                    decimal Balance = JsonConvert.DeserializeObject<decimal>(stringData);
                    JSONReceived.Text = stringData.ToString();
                    BalanceAmount.Text = Balance.ToString();
                }
                else
                {
                }
                

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private void amount_TextComposition(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            this.Registration();
        }

        private async void Registration()
        {
            tokenDTO = null;
            tokenDTO = new TokenDTO();
            textUser.Text = "";

            RegistrationDTO model = new RegistrationDTO()
            {
                Username = textUserName.Text,
                Password = passwordBox.Password
            };

            try
            {
                client.DefaultRequestHeaders.Authorization = null;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenDTO.Token);

                var postData = JsonConvert.SerializeObject(model);
                JSONSent.Text = postData.ToString();
                
                var contentData = new StringContent(postData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("authentication/register", contentData);

                if (response.IsSuccessStatusCode)
                {
                    var stringData = await response.Content.ReadAsStringAsync();
                    Registersucces.Visibility = Visibility.Visible;
                    textUserName.Text = "";
                    passwordBox.Password = "";
                }
                else
                {
                }
            }
            catch
            { 
            }
        }
    }
}
