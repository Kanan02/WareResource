//var client = new HttpClient();
//var request = new HttpRequestMessage
//{
//    Method = HttpMethod.Get,
//    RequestUri = new Uri("https://community-open-weather-map.p.rapidapi.com/forecast/daily?q=baku&lat=35&lon=139&cnt=10&units=metric%20or%20imperial"),
//    Headers =
//    {
//        { "x-rapidapi-host", "community-open-weather-map.p.rapidapi.com" },
//        { "x-rapidapi-key", "b30b696ee1msh94829ab29706161p1a588bjsnda89895ffd67" },
//    },
//};
//using (var response = await client.SendAsync(request))
//{
//    response.EnsureSuccessStatusCode();
//    var body = await response.Content.ReadAsStringAsync();
//    Console.WriteLine(body);
//}

using WaterResourcesManager;
using WaterResourcesManager.Models.Abstract;
using WaterResourcesManager.Models.Concrete;

//LitresForAreaCounter counter = new LitresForAreaCounter();
//counter.CalculateLitres(0, "carrot", "", "Baku");





//==============================================================================================================================================================

// Gound water has more than needed

Channel channel = new Channel { StandardWaterHeight = 9, CriticalWaterLevel = 5, CurrentWaterHeight = 10, PollutionLevel = 7 };
RainWaterReservoir rainWaterReservoir = new RainWaterReservoir { Height = 10, Width = 3, Length = 3, CurrentWaterLevel = 6, PollutionLevel = 6.5 };
GroundWaterReservoir groundWaterReservoir = new GroundWaterReservoir { Height = 10, Width = 2, Length = 5, CurrentWaterLevel = 7, PollutionLevel = 8.3 };
WaterReservoir waterReservoir = new WaterReservoir();



// Rain water has more than needed

//Channel channel = new Channel { StandardWaterHeight = 10, CriticalWaterLevel = 10, CurrentWaterHeight = 10 };
//RainWaterReservoir rainWaterReservoir = new RainWaterReservoir { Height = 10, Width = 0.1, Length = 10, CurrentWaterLevel = 10 };
//GroundWaterReservoir groundWaterReservoir = new GroundWaterReservoir { Height = 10, Width = 10, Length = 10, CurrentWaterLevel = 10 };
//WaterReservoir waterReservoir = new WaterReservoir();



// Distribution between rain ground and channel

//Channel channel = new Channel { StandardWaterHeight = 10, CriticalWaterLevel = 5, CurrentWaterHeight = 10 };
//RainWaterReservoir rainWaterReservoir = new RainWaterReservoir { Height = 10, Width = 0.1, Length = 0.01, CurrentWaterLevel = 10 };  //10
//GroundWaterReservoir groundWaterReservoir = new GroundWaterReservoir { Height = 10, Width = 0.1, Length = 0.1, CurrentWaterLevel = 10 };
//WaterReservoir waterReservoir = new WaterReservoir();



// Not enough from all sources (taking from water reservoir)

//Channel channel = new Channel { StandardWaterHeight = 10, CriticalWaterLevel = 10, CurrentWaterHeight = 5 };
//RainWaterReservoir rainWaterReservoir = new RainWaterReservoir { Height = 10, Width = 0.1, Length = 0.01, CurrentWaterLevel = 10 };  //10
//GroundWaterReservoir groundWaterReservoir = new GroundWaterReservoir { Height = 10, Width = 0.1, Length = 0.1, CurrentWaterLevel = 10 };
//WaterReservoir waterReservoir = new WaterReservoir();




//==============================================================================================================================================================



List<WaterResource> waterResources = new List<WaterResource>();
waterResources.Add(channel);
waterResources.Add(rainWaterReservoir);
waterResources.Add(groundWaterReservoir);
waterResources.Add(waterReservoir);

LitresDistribution litresDistribution = new LitresDistribution(45000, "tomato", 1, "Baku", waterResources);
Dictionary<WaterResource, double> result = litresDistribution.FindBestDistribution();
int a;
a = 5;
int b = a + 5;


