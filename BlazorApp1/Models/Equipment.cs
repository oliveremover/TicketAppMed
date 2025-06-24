using System.Reflection;

namespace BlazorApp1.Models
{
    public class Equipment
    {
        public int EquipmentNumber { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string DeviceType { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string AltID { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;
    }
}