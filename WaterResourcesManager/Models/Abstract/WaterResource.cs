namespace WaterResourcesManager.Models.Abstract
{
    public abstract class WaterResource
    {
        public int Id { get; set; }
        public string? Name { get; set; } = String.Empty;
        public double PollutionLevel { get; set; }  // In Ph
    }
}
