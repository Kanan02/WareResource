namespace WaterDataAPI.Models
{
    public class River
    {
        public int Id { get; set; }
        public string? Name { get; set; }=String.Empty;
        public double PollutionPercentage { get; set; }
        public double StandardWaterLevel { get; set; }
        public double CurrentWaterLevel { get; set; }
        public double SpeedOfWater{ get; set; }
        public double AirTemperature { get; set; }

    }
}
