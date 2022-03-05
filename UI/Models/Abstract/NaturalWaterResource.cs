namespace UI.Models.Abstract
{
    public abstract class NaturalWaterResource:WaterResource
    {
        public double StandardWaterLevel { get; set; }
        public double CurrentWaterLevel { get; set; }
        public double PollutionPercentage { get; set; }
    }
}
