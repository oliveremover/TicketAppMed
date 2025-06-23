using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp1.Models;

namespace BlazorApp1.Services
{
    public interface IEquipmentService
    {
        Task<List<Equipment>> GetAllEquipmentAsync();
        Task<Equipment?> GetEquipmentByIdAsync(int id);
        Task<List<Equipment>> SearchEquipmentAsync(string searchTerm);
    }
    
    public class EquipmentService : IEquipmentService
    {
        private static List<Equipment> _mockEquipment = new();
        private static bool _initialized = false;
        
        private void InitializeMockData()
        {
            if (_initialized) return;
            
            // Create sample equipment data
            var random = new Random();
            var equipmentTypes = new[] { "Computer", "Printer", "Scanner", "Server", "Monitor", "Microscope", "Analyzer" };
            var locations = new[] { "Drammen", "Bærum", "Kongsberg", "Ringerike", "Mikrobiologen" };
            
            _mockEquipment = Enumerable.Range(1, 20).Select(i => new Equipment
            {
                EquipmentNumber = i,
                Model = $"{equipmentTypes[random.Next(equipmentTypes.Length)]} {(char)('A' + random.Next(26))}{random.Next(100)}",
                SerialNumber = $"SN-{random.Next(10000, 99999)}",
                Brand = equipmentTypes[random.Next(equipmentTypes.Length)],
                DeviceType = equipmentTypes[random.Next(equipmentTypes.Length)],
            }).ToList();
            
            _initialized = true;
        }
        
        public async Task<List<Equipment>> GetAllEquipmentAsync()
        {
            InitializeMockData();
            await Task.Delay(300); // Simulate network delay
            
            return _mockEquipment.ToList();
        }
        
        public async Task<Equipment?> GetEquipmentByIdAsync(int id)
        {
            InitializeMockData();
            await Task.Delay(200); // Simulate network delay
            
            return _mockEquipment.FirstOrDefault(e => e.EquipmentNumber == id);
        }
        
        public async Task<List<Equipment>> SearchEquipmentAsync(string searchTerm)
        {
            InitializeMockData();
            await Task.Delay(300); // Simulate network delay
            
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _mockEquipment.Take(5).ToList();
                
            return _mockEquipment
                .Where(e => e.Brand.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || 
                            e.SerialNumber.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || 
                            e.Model.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}