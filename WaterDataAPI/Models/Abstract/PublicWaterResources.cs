namespace WaterDataAPI.Models.Abstract
{
    public abstract class PublicWaterResources : WaterResource
    {
        public double StandardWaterHeight { get; set; }
        public double CurrentWaterHeight { get; set; }
        public double CriticalWaterLevel { get; set; }
    }
}
