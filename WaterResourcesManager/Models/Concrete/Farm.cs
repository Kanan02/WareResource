using System.ComponentModel.DataAnnotations;

namespace WaterResourcesManager.Models.Concrete
{
    public class Farm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
    }
}
