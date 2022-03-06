namespace WaterDataAPI.Models.Abstract
{
    public abstract class PrivateWaterResources : WaterResource
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double CurrentWaterLevel { get; set; }

        public virtual bool IsEmpty()
        {
            return CurrentWaterLevel == 0;
        }
        public virtual bool IsFull()
        {
            return CurrentWaterLevel >= Height;
        }

        public virtual double GetResourceVolume()
        {
            return Width * Height * Length;
        }

        public virtual double GetCurrentWaterVolume()
        {
            return Width * CurrentWaterLevel* Length;
        }

    }
}
