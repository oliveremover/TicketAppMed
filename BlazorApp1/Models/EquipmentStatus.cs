namespace BlazorApp1.Models
{
    public enum EquipmentStatus
    {
        Available = 0,
        InUse = 1,
        UnderMaintenance = 2,
        Defective = 3
    }
    
    public static class EquipmentStatusExtensions
    {
        public static string ToDisplayString(this EquipmentStatus status)
        {
            return status switch
            {
                EquipmentStatus.Available => "Available",
                EquipmentStatus.InUse => "In Use",
                EquipmentStatus.UnderMaintenance => "Under Maintenance",
                EquipmentStatus.Defective => "Defective",
                _ => "Unknown"
            };
        }
        
        public static string GetStatusClass(this EquipmentStatus status)
        {
            // Only green or red colors based on equipment state
            return status switch
            {
                EquipmentStatus.Available => "status-green",
                EquipmentStatus.InUse => "status-green",
                EquipmentStatus.UnderMaintenance => "status-red",
                EquipmentStatus.Defective => "status-red",
                _ => "status-red"
            };
        }
    }
}