using System;
namespace UI.Models.Abstract
{
    public abstract class WaterResource
    {
        public int Id { get; set; }
        public string? Name { get; set; } = String.Empty;
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
