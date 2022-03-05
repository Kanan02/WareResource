var client = new HttpClient();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://community-open-weather-map.p.rapidapi.com/forecast/daily?q=baku&lat=35&lon=139&cnt=10&units=metric%20or%20imperial"),
    Headers =
    {
        { "x-rapidapi-host", "community-open-weather-map.p.rapidapi.com" },
        { "x-rapidapi-key", "b30b696ee1msh94829ab29706161p1a588bjsnda89895ffd67" },
    },
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    Console.WriteLine(body);
}