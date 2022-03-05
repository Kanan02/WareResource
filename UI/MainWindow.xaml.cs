using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
using UI.Models;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("https://localhost:7198/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );
        }
        private async void GetAllRivers()
        {

            var response = await client.GetStringAsync("Channel");
            var rivers = JsonConvert.DeserializeObject<List<River>>(response);
            dataGrid1.DataContext= rivers;

        }
        private async void UpdateRiver(River river)
        {

             await client.PutAsJsonAsync("channel/"+river.Id,river);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (resourcesComboBox.SelectedIndex==0)
            {
                this.GetAllRivers();
            }

        }
        private async void GetLitres(double area,int productId,string fieldName)
        {

           // XXXXX.CalclateLitres(area, product, fieldName);
           // await client.PutAsJsonAsync("channel/" + river.Id, river);
        }


    }
}
