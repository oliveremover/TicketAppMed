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
        Task<Equipment?> GetEquipmentByNumberAsync(int equipmentNumber);
        Task<Equipment> CreateEquipmentAsync(Equipment equipment);
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
            var brands = new[] { "HP", "Dell", "Lenovo", "Canon", "Epson", "Cisco", "Zeiss", "Olympus", "Roche" };
            var codes = new[] { "LAB", "IT", "MED", "ADMIN", "RESEARCH" };

            _mockEquipment = Enumerable.Range(1, 20).Select(i => new Equipment
            {
                EquipmentNumber = i,
                Brand = brands[random.Next(brands.Length)],
                DeviceType = equipmentTypes[random.Next(equipmentTypes.Length)],
                Model = $"{(char)('A' + random.Next(26))}{random.Next(100)}",
                SerialNumber = $"SN-{random.Next(10000, 99999)}",
                AltID = random.Next(0, 2) == 0 ? $"ALT-{random.Next(1000, 9999)}" : string.Empty,
                Code = random.Next(0, 2) == 0 ? $"{codes[random.Next(codes.Length)]}-{random.Next(100, 999)}" : string.Empty,
                ImageUrl = random.Next(0, 2) == 0 ? "/icon-192.png" : null,
                Status = (EquipmentStatus)random.Next(0, 4),
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

        public async Task<Equipment?> GetEquipmentByNumberAsync(int equipmentNumber)
        {
            InitializeMockData();
            await Task.Delay(200); // Simulate network delay

            return _mockEquipment.FirstOrDefault(e => e.EquipmentNumber == equipmentNumber);
        }

        public async Task<Equipment> CreateEquipmentAsync(Equipment equipment)
        {
            InitializeMockData();
            
            // Ensure we're not duplicating equipment numbers
            if (equipment.EquipmentNumber == 0)
            {
                // Generate a new equipment number
                equipment.EquipmentNumber = _mockEquipment.Max(e => e.EquipmentNumber) + 1;
            }
            else if (_mockEquipment.Any(e => e.EquipmentNumber == equipment.EquipmentNumber))
            {
                // If the equipment number already exists, generate a new one
                equipment.EquipmentNumber = _mockEquipment.Max(e => e.EquipmentNumber) + 1;
            }

            // Add the new equipment to our mock data
            _mockEquipment.Add(equipment);
            
            await Task.Delay(300); // Simulate network delay
            return equipment;
        }
    }
}