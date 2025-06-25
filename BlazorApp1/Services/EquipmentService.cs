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

            _mockEquipment = new List<Equipment>
            {
                new Equipment
                {
                    EquipmentNumber = 1,
                    Brand = "Zoll",
                    DeviceType = "Defibrillator halvautomatisk (AED)",
                    Model = "AED Pro",
                    SerialNumber = "900057",
                    AltID = null,
                    Code = "SE48049-00057",
                    ImageUrl = "/ZOLL_AED-Pro.png",
                    Status = EquipmentStatus.InUse
                },
                new Equipment
                {
                    EquipmentNumber = 2,
                    Brand = "Zoll",
                    DeviceType = "Batteri, laddningsbart, mobilt",
                    Model = "AutoPulse Li-Ion battery",
                    SerialNumber = "100010",
                    AltID = null,
                    Code = "SE36534-00010",
                    ImageUrl = null,
                    Status = EquipmentStatus.UnderMaintenance
                },
                new Equipment
                {
                    EquipmentNumber = 3,
                    Brand = "Zoll",
                    DeviceType = "Defibrillator med patientmonitorering",
                    Model = "R Series Plus",
                    SerialNumber = "800001",
                    AltID = null,
                    Code = "SE17882-00001",
                    ImageUrl = "/Product_R_Zoll.jpg",
                    Status = EquipmentStatus.InUse
                },
                new Equipment
                {
                    EquipmentNumber = 4,
                    Brand = "Zoll",
                    DeviceType = "Defibrillator med patientmonitorering",
                    Model = "R Series ALS",
                    SerialNumber = "400002",
                    AltID = null,
                    Code = "SE17882-00002",
                    ImageUrl = null,
                    Status = EquipmentStatus.UnderMaintenance
                },
                new Equipment
                {
                    EquipmentNumber = 5,
                    Brand = "Zoll",
                    DeviceType = "EKG-simulator",
                    Model = "ECG Simulator for ZOLL M & R Series Defibrillators",
                    SerialNumber = "200004",
                    AltID = null,
                    Code = "SE35026-00004",
                    ImageUrl = "/icon-192.png",
                    Status = EquipmentStatus.InUse
                },
                new Equipment
                {
                    EquipmentNumber = 6,
                    Brand = "Zoll",
                    DeviceType = "Batteri, laddningsbart, mobilt",
                    Model = "SurePower Battery Pack",
                    SerialNumber = "300012",
                    AltID = null,
                    Code = "SE36534-00012",
                    ImageUrl = null,
                    Status = EquipmentStatus.UnderMaintenance
                },
                new Equipment
                {
                    EquipmentNumber = 7,
                    Brand = "Zoll",
                    DeviceType = "Defibrillator med patientmonitorering",
                    Model = "R Series",
                    SerialNumber = "200003",
                    AltID = null,
                    Code = "SE17882-00003",
                    ImageUrl = "/icon-192.png",
                    Status = EquipmentStatus.InUse
                },
                new Equipment
                {
                    EquipmentNumber = 8,
                    Brand = "Zoll",
                    DeviceType = "Hjärtkompressionsapparat",
                    Model = "AutoPulse",
                    SerialNumber = "100002",
                    AltID = null,
                    Code = "SE35309-00002",
                    ImageUrl = "/icon-192.png",
                    Status = EquipmentStatus.UnderMaintenance
                },
                new Equipment
                {
                    EquipmentNumber = 9,
                    Brand = "Zoll",
                    DeviceType = "Hypotermirustning intravasål",
                    Model = "Thermogard XP",
                    SerialNumber = "400001",
                    AltID = null,
                    Code = "SET1538-00001",
                    ImageUrl = null,
                    Status = EquipmentStatus.InUse
                },
                new Equipment
                {
                    EquipmentNumber = 10,
                    Brand = "Zoll",
                    DeviceType = "EKG-simulator",
                    Model = "AED Pro Simulator",
                    SerialNumber = "100040",
                    AltID = null,
                    Code = "SE35026-00040",
                    ImageUrl = "/icon-192.png",
                    Status = EquipmentStatus.UnderMaintenance
                },
                new Equipment
                {
                    EquipmentNumber = 11,
                    Brand = "Zoll",
                    DeviceType = "Testinstrument batterier",
                    Model = "SurePower System",
                    SerialNumber = "100010",
                    AltID = null,
                    Code = "SE37174-00010",
                    ImageUrl = "/icon-192.png",
                    Status = EquipmentStatus.UnderMaintenance
                },
                new Equipment
                {
                    EquipmentNumber = 12,
                    Brand = "Zoll",
                    DeviceType = "Defibrillator halvautomatisk (AED)",
                    Model = "AED 3 BLS",
                    SerialNumber = "900074",
                    AltID = null,
                    Code = "SE48049-00074",
                    ImageUrl = null,
                    Status = EquipmentStatus.UnderMaintenance
                },
                new Equipment
                {
                    EquipmentNumber = 13,
                    Brand = "Zoll",
                    DeviceType = "Givare för CO2-mätning i andningsslang",
                    Model = "9355-000590",
                    SerialNumber = "300000",
                    AltID = "1",
                    Code = "EU46822-00000",
                    ImageUrl = "/icon-192.png",
                    Status = EquipmentStatus.UnderMaintenance
                },
                new Equipment
                {
                    EquipmentNumber = 14,
                    Brand = "Zoll",
                    DeviceType = "Defibrillator halvautomatisk (AED)",
                    Model = "XSCP-1",
                    SerialNumber = "000003",
                    AltID = null,
                    Code = "EU48049-00003",
                    ImageUrl = null,
                    Status = EquipmentStatus.UnderMaintenance
                },
                new Equipment
                {
                    EquipmentNumber = 15,
                    Brand = "Zoll",
                    DeviceType = "Defibrillator halvautomatisk (AED)",
                    Model = "AED 3",
                    SerialNumber = "60004",
                    AltID = null,
                    Code = "EU48049-00004",
                    ImageUrl = "/icon-192.png",
                    Status = EquipmentStatus.UnderMaintenance
                },
                new Equipment
                {
                    EquipmentNumber = 16,
                    Brand = "Zoll",
                    DeviceType = "Batteriladdare MTU",
                    Model = "1119-0500",
                    SerialNumber = "12010",
                    AltID = null,
                    Code = "EU17115-00010",
                    ImageUrl = null,
                    Status = EquipmentStatus.UnderMaintenance
                },
                new Equipment
                {
                    EquipmentNumber = 17,
                    Brand = "Zoll",
                    DeviceType = "Batteriladdare MTU",
                    Model = "1211-000100-01",
                    SerialNumber = "123123",
                    AltID = null,
                    Code = "EU17115-00011",
                    ImageUrl = "/icon-192.png",
                    Status = EquipmentStatus.UnderMaintenance
                }
            };

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