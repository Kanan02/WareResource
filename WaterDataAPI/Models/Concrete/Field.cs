namespace WaterDataAPI.Models.Concrete
{
    public class Field
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Stage { get; set; }
        public double SAT { get; set; }
        public double PERC { get; set; }
        public double Wl { get; set; }

    }
}
