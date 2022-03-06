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
using WaterResourcesManager.Models.Abstract;
using WaterResourcesManager.Models.Concrete;
using WaterResourcesManager;

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
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var response = client.GetStringAsync($"Farm/2").Result;
            //Field? field = JsonConvert.DeserializeObject<Field>(response);
            List<WaterResource> listWates = new List<WaterResource>();
            listWates.Add(new GroundWaterReservoir()
            {
                Id = 1,
                Name = "Kolodec",
                CurrentWaterLevel = 10,
                Height = 15,
                Length = 10,
                Width = 10,
                PollutionLevel = 7
            });
            listWates.Add(new Channel()
            {
                Id = 1,
                Name = "Kolodec",
                CriticalWaterLevel = 2,
                CurrentWaterHeight = 5,
                StandardWaterHeight = 4,
                PollutionLevel = 5
            });
            LitresDistribution distribution = new LitresDistribution(500,"Tomato",1,"Baku",listWates);
            ShowWaterDistrib(distribution.FindBestDistribution());
        }

        private void ShowWaterDistrib(Dictionary<WaterResource, double> waterDistrib)
        {
            for (int i = 0; i < waterDistrib.Count; i++)
            {
                string waterResourceName = waterDistrib.ElementAt(i).Key.Name;
                string waterAmountValue = waterDistrib.ElementAt(i).Value.ToString();
                string phLevelValue = waterDistrib.ElementAt(i).Key.PollutionLevel.ToString();

                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                waterResultsGrid.RowDefinitions.Add(row);
                Grid gridAns = new Grid();
                gridAns.Name = $"GridAns{i + 1}";
                gridAns.SetValue(Grid.RowProperty, i);
                ColumnDefinition gridCol1 = new ColumnDefinition();
                gridCol1.Width = new GridLength(1, GridUnitType.Star);
                ColumnDefinition gridCol2 = new ColumnDefinition();
                gridCol2.Width = new GridLength(1, GridUnitType.Star);
                ColumnDefinition gridCol3 = new ColumnDefinition();
                gridCol3.Width = new GridLength(1, GridUnitType.Star);
                gridAns.ColumnDefinitions.Add(gridCol1);
                gridAns.ColumnDefinitions.Add(gridCol2);
                gridAns.ColumnDefinitions.Add(gridCol3);
                gridAns.Visibility = Visibility.Collapsed;
                RegisterName($"GridAns{i + 1}", gridAns);

                TextBlock waterResource = new TextBlock();
                waterResource.SetValue(Grid.ColumnProperty, 1);
                waterResource.Name = $"WaterResource{i + 1}";
                waterResource.TextWrapping = TextWrapping.Wrap;
                waterResource.HorizontalAlignment = HorizontalAlignment.Stretch;
                waterResource.VerticalAlignment = VerticalAlignment.Center;
                waterResource.Margin = new Thickness(10);
                waterResource.FontSize = 17;
                waterResource.Text = waterResourceName;
                RegisterName($"WaterResource{i + 1}", waterResource);

                TextBlock waterAmount = new TextBlock();
                waterAmount.SetValue(Grid.ColumnProperty, 1);
                waterAmount.Name = $"WaterAmount{i + 1}";
                waterAmount.TextWrapping = TextWrapping.Wrap;
                waterAmount.HorizontalAlignment = HorizontalAlignment.Stretch;
                waterAmount.VerticalAlignment = VerticalAlignment.Center;
                waterAmount.Margin = new Thickness(10);
                waterAmount.FontSize = 17;
                waterAmount.Text = waterAmountValue;
                RegisterName($"WaterAmount{i + 1}", waterAmount);

                TextBlock phLevel = new TextBlock();
                phLevel.SetValue(Grid.ColumnProperty, 1);
                phLevel.Name = $"phLevel{i + 1}";
                phLevel.TextWrapping = TextWrapping.Wrap;
                phLevel.HorizontalAlignment = HorizontalAlignment.Stretch;
                phLevel.VerticalAlignment = VerticalAlignment.Center;
                phLevel.Margin = new Thickness(10);
                phLevel.FontSize = 17;
                phLevel.Text = phLevelValue;
                RegisterName($"phLevel{i + 1}", phLevel);

                gridAns.Children.Add(waterResource);
                gridAns.Children.Add(waterAmount);
                gridAns.Children.Add(phLevel);
                waterResultsGrid.Children.Add(gridAns);
            }
        }
    }
}
