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
        
        private List<WaterResource> allWaterBodies;
        private Farm? farm;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var responseFarm = client.GetStringAsync($"Farm/2").Result;
            farm = JsonConvert.DeserializeObject<Farm>(responseFarm);
            FarmLabel.Content = $"{farm.Name} Farm, {farm.City}";

            LitresForAreaCounter litresForArea = new LitresForAreaCounter();
            for(int i = 0;i< litresForArea._kcs.Count;i++)
            {
                ComboBoxItem comboBox = new ComboBoxItem();
                comboBox.Content = litresForArea._kcs.ElementAt(i).Key.ToUpper();
                comboBox.FontSize = 15;

                CropComboBox.Items.Add(comboBox);
            }
            var response = client.GetStringAsync($"Field").Result;
            List<Field>? fields = JsonConvert.DeserializeObject<List<Field>>(response);
            FieldsValLabel.Content = fields.Count.ToString();
            for (int i = 0; i < fields.Count; i++)
            {
                ComboBoxItem comboBox = new ComboBoxItem();
                comboBox.Content = fields[i].Name;
                comboBox.Tag = fields[i].Id;
                comboBox.FontSize = 20;

                FieldsListCB.Items.Add(comboBox);
            }
            for (int i = 0; i < fields.Count; i++)
            {
                ComboBoxItem comboBox = new ComboBoxItem();
                comboBox.Content = fields[i].Name;
                comboBox.Tag = fields[i].Id;
                comboBox.FontSize = 15;

                FieldComboBox.Items.Add(comboBox);
            }

            int totalValWaters = 0;
            var responseChannels = client.GetStringAsync($"Channel").Result;
            List<Channel>? channels = JsonConvert.DeserializeObject<List<Channel>>(responseChannels);
            totalValWaters += channels.Count;
            var responseGrounds = client.GetStringAsync($"GroundWaterReservoir").Result;
            List<GroundWaterReservoir>? grounds = JsonConvert.DeserializeObject<List<GroundWaterReservoir>>(responseGrounds);
            totalValWaters += grounds.Count;
            var responseRains = client.GetStringAsync($"RainWaterReservoir").Result;
            List<RainWaterReservoir>? rains = JsonConvert.DeserializeObject<List<RainWaterReservoir>>(responseRains);
            totalValWaters += rains.Count;
            var responseReserv = client.GetStringAsync($"WaterReservoir").Result;
            List<WaterReservoir>? reservoirs = JsonConvert.DeserializeObject<List<WaterReservoir>>(responseReserv);
            totalValWaters += reservoirs.Count;
            WaterValLabel.Content = totalValWaters.ToString();

            allWaterBodies = new List<WaterResource>();
            allWaterBodies.AddRange(channels);
            allWaterBodies.AddRange(grounds);
            allWaterBodies.AddRange(rains);
            allWaterBodies.AddRange(reservoirs);


        }

        private void ShowWaterDistrib(Dictionary<WaterResource, double> waterDistrib)
        {
            waterResultsGrid.Children.Clear();
            for (int i = 0; i < waterDistrib.Count; i++)
            {
                string waterResourceName = waterDistrib.ElementAt(i).Key.Name;
                string waterAmountValue = waterDistrib.ElementAt(i).Value.ToString("F2");
                string phLevelValue = waterDistrib.ElementAt(i).Key.PollutionLevel.ToString("F2");

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
                gridAns.Background = new SolidColorBrush(Colors.Aqua);
                gridAns.Margin = new Thickness(5);
                //RegisterName($"GridAns{i + 1}", gridAns);

                TextBlock waterResource = new TextBlock();
                waterResource.SetValue(Grid.ColumnProperty, 0);
                waterResource.Name = $"WaterResource{i + 1}";
                waterResource.TextWrapping = TextWrapping.Wrap;
                waterResource.HorizontalAlignment = HorizontalAlignment.Stretch;
                waterResource.VerticalAlignment = VerticalAlignment.Center;
                waterResource.Margin = new Thickness(5);
                waterResource.FontSize = 17;
                waterResource.Text = waterResourceName;
                //RegisterName($"WaterResource{i + 1}", waterResource);

                TextBlock waterAmount = new TextBlock();
                waterAmount.SetValue(Grid.ColumnProperty, 1);
                waterAmount.Name = $"WaterAmount{i + 1}";
                waterAmount.TextWrapping = TextWrapping.Wrap;
                waterAmount.HorizontalAlignment = HorizontalAlignment.Stretch;
                waterAmount.VerticalAlignment = VerticalAlignment.Center;
                waterAmount.Margin = new Thickness(5);
                waterAmount.FontSize = 17;
                waterAmount.Text = $"{waterAmountValue}L";
                //RegisterName($"WaterAmount{i + 1}", waterAmount);

                TextBlock phLevel = new TextBlock();
                phLevel.SetValue(Grid.ColumnProperty, 2);
                phLevel.Name = $"phLevel{i + 1}";
                phLevel.TextWrapping = TextWrapping.Wrap;
                phLevel.HorizontalAlignment = HorizontalAlignment.Stretch;
                phLevel.VerticalAlignment = VerticalAlignment.Center;
                phLevel.Margin = new Thickness(5);
                phLevel.FontSize = 17;
                phLevel.Text = $"ph={phLevelValue}";
                //RegisterName($"phLevel{i + 1}", phLevel);

                gridAns.Children.Add(waterResource);
                gridAns.Children.Add(waterAmount);
                gridAns.Children.Add(phLevel);
                waterResultsGrid.Children.Add(gridAns);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(FarmRegulator.Visibility == Visibility.Visible)
            {
                FarmRegulator.Visibility = Visibility.Hidden;
            }
            else
            {
                FarmRegulator.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string typeofWaterBody = WaterBodiesTypesListCB.Text;
            ResTextBlock.FontSize = 20;
            if (typeofWaterBody == "Channel")
            {
                var responseChannels = client.GetStringAsync($"Channel/{((ComboBoxItem)WaterBodiesListCB.SelectedItem).Tag}").Result;
                Channel? channel = JsonConvert.DeserializeObject<Channel>(responseChannels);
                ResTextBlock.Text="Name: "+channel.Name+ "\nStandard water level: " + channel.StandardWaterHeight + "\nCritical Water level: " +channel.CriticalWaterLevel+
                    "\nCurrent channel level: " + channel.CurrentWaterHeight+"\nPollution level: "+channel.PollutionLevel;
            }
            else if (typeofWaterBody == "GroundWaterReservoir")
            {
                var responseGrounds = client.GetStringAsync($"GroundWaterReservoir/{((ComboBoxItem)WaterBodiesListCB.SelectedItem).Tag}").Result;
                GroundWaterReservoir? ground = JsonConvert.DeserializeObject<GroundWaterReservoir>(responseGrounds);
                ResTextBlock.Text = "Name: " + ground.Name + "\nLength: " + ground.Length + "\nWidth: " + ground.Width + "\nHeight: " + ground.Height+
                    "\nCurrent water height: " + ground.CurrentWaterLevel + "\nPollution level: " + ground.PollutionLevel;
            }
            else if (typeofWaterBody == "RainWaterReservoir")
            {
                var responseRains = client.GetStringAsync($"RainWaterReservoir/{((ComboBoxItem)WaterBodiesListCB.SelectedItem).Tag}").Result;
                RainWaterReservoir? rain = JsonConvert.DeserializeObject<RainWaterReservoir>(responseRains);
                ResTextBlock.Text = "Name: " + rain.Name + "\nLength: " + rain.Length + "\nWidth: " + rain.Width + "\nHeight: " + rain.Height +
                   "\nCurrent water height: " + rain.CurrentWaterLevel + "\nPollution level: " + rain.PollutionLevel;
            }

            else if (typeofWaterBody == "WaterReservoir")
            {
                var responseReserv = client.GetStringAsync($"WaterReservoir/{((ComboBoxItem)WaterBodiesListCB.SelectedItem).Tag}").Result;
                WaterReservoir? reservoir = JsonConvert.DeserializeObject<WaterReservoir>(responseReserv);
                ResTextBlock.Text = "Name: " + reservoir.Name + "\nPollution level: " + reservoir.PollutionLevel;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var response = client.GetStringAsync($"Field/{((ComboBoxItem)FieldsListCB.SelectedItem).Tag}").Result;
            Field? field = JsonConvert.DeserializeObject<Field>(response);
            MessageBox.Show(field.Name.ToString());
            FieldTextBlock.FontSize = 20;
            FieldTextBlock.Text= "Name: " + field.Name+"\nPERC: "+ field.PERC + "\nSAT: " + field.SAT+"\nWl: " + field.Wl + "\nStage: " + field.Stage;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var response = client.GetStringAsync($"Farm/2").Result;
            Field? field = JsonConvert.DeserializeObject<Field>(response);
            LitresDistribution distribution = null;
            int fieldId = Int32.Parse(((ComboBoxItem)FieldComboBox.SelectedItem).Tag.ToString());
            if (MeasureUnitCB.Text ==  "m2")
            {
                distribution = new LitresDistribution(double.Parse(AreaVal.Text), CropComboBox.Text, fieldId, farm.City, allWaterBodies);
            }
            else if(MeasureUnitCB.Text == "ha")
            {
                distribution = new LitresDistribution(double.Parse(AreaVal.Text)*10000, CropComboBox.Text, fieldId, farm.City, allWaterBodies);
            }
            ShowWaterDistrib(distribution.FindBestDistribution());
        }

        private void WaterBodiesTypesListCB_DropDownClosed(object sender, EventArgs e)
        {
            string typeofWaterBody = WaterBodiesTypesListCB.Text;
            WaterBodiesListCB.Items.Clear();
            if (typeofWaterBody == "Channel")
            {
                var responseChannels = client.GetStringAsync($"Channel").Result;
                List<Channel>? channels = JsonConvert.DeserializeObject<List<Channel>>(responseChannels);
                for (int i = 0; i < channels.Count; i++)
                {
                    ComboBoxItem comboBox = new ComboBoxItem();
                    comboBox.Content = channels[i].Name;
                    comboBox.Tag = channels[i].Id;
                    comboBox.FontSize = 20;

                    WaterBodiesListCB.Items.Add(comboBox);
                }
            }
            else if (typeofWaterBody == "GroundWaterReservoir")
            {
                var responseGrounds = client.GetStringAsync($"GroundWaterReservoir").Result;
                List<GroundWaterReservoir>? grounds = JsonConvert.DeserializeObject<List<GroundWaterReservoir>>(responseGrounds);
                for (int i = 0; i < grounds.Count; i++)
                {
                    ComboBoxItem comboBox = new ComboBoxItem();
                    comboBox.Content = grounds[i].Name;
                    comboBox.Tag = grounds[i].Id;
                    comboBox.FontSize = 20;

                    WaterBodiesListCB.Items.Add(comboBox);
                }
            }
            else if (typeofWaterBody == "RainWaterReservoir")
            {
                var responseRains = client.GetStringAsync($"RainWaterReservoir").Result;
                List<RainWaterReservoir>? rains = JsonConvert.DeserializeObject<List<RainWaterReservoir>>(responseRains);
                for (int i = 0; i < rains.Count; i++)
                {
                    ComboBoxItem comboBox = new ComboBoxItem();
                    comboBox.Content = rains[i].Name;
                    comboBox.Tag = rains[i].Id;
                    comboBox.FontSize = 20;

                    WaterBodiesListCB.Items.Add(comboBox);
                }
            }
            else if (typeofWaterBody == "WaterReservoir")
            {
                var responseReserv = client.GetStringAsync($"WaterReservoir").Result;
                List<WaterReservoir>? reservoirs = JsonConvert.DeserializeObject<List<WaterReservoir>>(responseReserv);
                for (int i = 0; i < reservoirs.Count; i++)
                {
                    ComboBoxItem comboBox = new ComboBoxItem();
                    comboBox.Content = reservoirs[i].Name;
                    comboBox.Tag = reservoirs[i].Id;
                    comboBox.FontSize = 20;

                    WaterBodiesListCB.Items.Add(comboBox);
                }
            }
        }
    }
}
