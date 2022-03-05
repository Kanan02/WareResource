using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterResourcesManager
{
    public class LitresForAreaCounter
    {
        HttpClient client = new HttpClient();

        List<List<double>> ps = new List<List<double>>
        {
            new List<double>
            {
                0.27,0.27,0.27,0.27,0.27,0.27,0.27,.27,.27,.27,.27,.27
            },

             new List<double>
            {
                0.27,0.27,0.27,0.28,0.28,0.28,0.28,.28,.28,.27,.27,.27
            },
               new List<double>
            {
                0.26,0.27,0.27,0.28,0.28,0.29,0.29,.28,.28,.27,.26,.26
            },

                 new List<double>
            {
                0.26,0.26,0.27,0.28,0.29,0.29,0.29,.28,.28,.27,.26,.25
            },
           new List<double>
            {
                0.25,0.26,0.27,0.28,0.29,0.3,0.3,.29,.28,.26,.25,.25
            },
            new List<double>
            {
                0.24,0.26,0.27,0.29,0.30,0.31,0.31,.29,.28,.26,.25,.24
            },
              new List<double>
            {
                0.24,0.25,0.27,0.29,0.31,0.32,0.31,0.30,.28,.26,.24,0.23
            },

            new List<double>
            {
                0.23,0.25,0.27,0.29,0.31,0.32,0.32,.30,.28,.25,.23,.22
            },
            new List<double>
            {
                0.22,0.24,0.27,0.30,0.32,0.34,0.33,.31,.28,.25,.22,.21
            },
            new List<double>
            {
                0.20,0.23,0.27,0.30,0.34,0.35,0.34,.32,.28,.24,.21,.20
            },
            new List<double>
            {
                0.19,0.23,0.27,0.31,0.34,0.36,0.35,.32,.28,.24,.20,.18
            },
            new List<double>
            {
                0.17,0.21,0.26,0.32,0.36,0.39,0.38,.33,.28,.23,.18,.16
            },
            new List<double>
            {
                0.15,0.20,0.26,0.32,0.38,0.41,0.40,.34,.28,.22,.17,.13
            }
        };






        Dictionary<string, List<double>> kcs = new Dictionary<string, List<double>>
        {
            ["Tomato"]=new List<double> {0.45,0.75,1.15,0.84 },
            ["Potato"] = new List<double> { 0.45, 0.75, 1.15, 0.85 },
            ["Wheat"] = new List<double> { 0.35, 0.75, 1.15, 0.45 },
            ["Cotton"] = new List<double> { 0.45, 0.75, 1.15, 0.75 },
            ["Carrot"] = new List<double> { 0.45, 0.75, 1.05, 0.9 },
            ["Onion"] = new List<double> { 0.5, 0.75, 1.05, 0.85 },
            ["Pepper"] = new List<double> { 0.35, 0.7, 1.05, 0.9 },
            ["Cucumber"] = new List<double> { 0.45, 0.7, 0.9, 0.75 },
        };

        public double CalculateLitres(double area,string product, string fieldName,string city)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://community-open-weather-map.p.rapidapi.com/forecast/daily?q={city}"),
                Headers =
                {
                    { "x-rapidapi-host", "community-open-weather-map.p.rapidapi.com" },
                    { "x-rapidapi-key", "b30b696ee1msh94829ab29706161p1a588bjsnda89895ffd67" },
                },
            };
            using (var response = client.Send(request))
            {
                response.EnsureSuccessStatusCode();
                var body =  response.Content.ReadAsStringAsync().Result;
                dynamic content = JsonConvert.DeserializeObject(body);

                double num=double.Parse( content.city.coord.lat.ToString());
              
                num=(Math.Round(num));
                num = num - num % 5;

                double p=ps[Convert.ToInt32(num) / 5][DateTime.Now.Month - 1];

                double tmean = (double.Parse(content.list[0].temp.max.ToString()) + double.Parse(content.list[0].temp.max.ToString())) / 2 - 273.15;

                double eto = (p * (0.46 * tmean + 1));


                
                Console.WriteLine(p);
                Console.WriteLine(tmean);
            }
            return 0;
        }
    }
}
