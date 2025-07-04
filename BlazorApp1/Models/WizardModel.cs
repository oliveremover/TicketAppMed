namespace BlazorApp1.Models
{
    public class WizardModel
    {
        public string? Category { get; set; }
        public string? Equipment { get; set; }
        public int? EquipmentNumber { get; set; } // Add this new property
        public string? Header { get; set; }
        public string? Details { get; set; }
        public List<string> Files { get; set; } = new List<string>();
        
        // New properties for switches
        public bool isContaminated { get; set; }
        public bool isCleaned { get; set; }
    }
}