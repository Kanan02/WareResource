using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WaterResourcesManager.Models.Concrete;

namespace WaterResourcesManager
{
    public class LitresForAreaCounter
    {
        private HttpClient _client = new HttpClient();

        //      mean daily percentage of annual daytime hours
        // default immutable values
        private List<List<double>> _ps = new List<List<double>>
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


        private Dictionary<string, List<double>> _kcs = new Dictionary<string, List<double>>
        {
            ["tomato"]=new List<double> {0.45,0.75,1.15,0.84 },
            ["potato"] = new List<double> { 0.45, 0.75, 1.15, 0.85 },
            ["wheat"] = new List<double> { 0.35, 0.75, 1.15, 0.45 },
            ["cotton"] = new List<double> { 0.45, 0.75, 1.15, 0.75 },
            ["carrot"] = new List<double> { 0.45, 0.75, 1.05, 0.9 },
            ["onion"] = new List<double> { 0.5, 0.75, 1.05, 0.85 },
            ["pepper"] = new List<double> { 0.35, 0.7, 1.05, 0.9 },
            ["cucumber"] = new List<double> { 0.45, 0.7, 0.9, 0.75 }
        };

        public double CalculateLitres(double area,string product, int fieldId, string city)
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

            dynamic content;
            double daytimeHoursPercentage = 0;

            using (var response = _client.Send(request))
            {
                response.EnsureSuccessStatusCode();
                var body =  response.Content.ReadAsStringAsync().Result;
                content = JsonConvert.DeserializeObject(body);

                daytimeHoursPercentage = double.Parse( content.city.coord.lat.ToString());
                daytimeHoursPercentage = (Math.Round(daytimeHoursPercentage));
                daytimeHoursPercentage = daytimeHoursPercentage - daytimeHoursPercentage % 5;
            }

            // Calculating eto
            double p = _ps[Convert.ToInt32(daytimeHoursPercentage) / 5][DateTime.Now.Month - 1];
            double tmean = (double.Parse(content.list[0].temp.max.ToString()) + double.Parse(content.list[0].temp.max.ToString())) / 2 - 273.15;
            double eto = (p * (0.46 * tmean + 8));



            // Calculating of effective rainfall
            double precipitation = content.list[0].rain;  // in mm/day
            double effectiveRainfall = 0;
            if (precipitation <= 2.5) effectiveRainfall = 0.6/30 * precipitation - 10/30;
            else effectiveRainfall = 0.8/30 * precipitation - 25/30;


            // Getting soil data from api
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7198/api/");
            _client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );
            var response2 = _client.GetStringAsync($"Field/{fieldId}").Result;
            Field? field = JsonConvert.DeserializeObject<Field>(response2);


            // Getting kc
            double kc = _kcs[product.ToLower()][field.Stage - 1];
            double Etcrop = eto * kc;

            double irrigationWaterNeed = Etcrop + (field.SAT + field.PERC + field.Wl)/30 - effectiveRainfall;  // need of l per 1m^2
            
            return area*irrigationWaterNeed;
        }






    }
}
