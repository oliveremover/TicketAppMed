using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp1.Models;

namespace BlazorApp1.Services
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetAllTicketsAsync();
        Task<Ticket?> GetTicketByIdAsync(int id);
        Task<Ticket> CreateTicketAsync(Ticket ticket);
        Task<bool> UpdateTicketAsync(Ticket ticket);
        Task<bool> DeleteTicketAsync(int id);
        Task<List<Ticket>> GetMyTicketsAsync(string userId);
        Task<List<string>> GetCategoriesAsync();
        Task<bool> AssignEquipmentToTicketAsync(int ticketId, int equipmentId);
        Task<bool> RemoveEquipmentFromTicketAsync(int ticketId);
    }
    
    public class TicketService : ITicketService
    {
        private static List<Ticket> _mockTickets = new();
        private static bool _initialized = false;
        
        private readonly IEquipmentService _equipmentService;
        
        public TicketService(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }
        
        private void InitializeMockData()
        {
            if (_initialized) return;
            
            var random = new Random(123); // Fixed seed for consistent mock data
            var startDate = DateTime.Now;
            var categories = new[] { "Drammen", "Bærum", "Kongsberg", "Ringerike", "Feilmeld", "Mikrobiologen", "Martina Hansen", "Prioritering" };
            
            _mockTickets = Enumerable.Range(1, 10).Select(index => new Ticket
            {
                ID = index,
                Header = $"Ticket {index}: {Lorem(random, 4)}",
                Details = Lorem(random, 20),
                Date = startDate.AddDays(-index),
                Category = categories[random.Next(categories.Length)],
                Status = (TicketStatus)random.Next(3), // 0: Open, 1: Pending, 2: Closed
                CreatedBy = "current.user@example.com",
                AssignedTo = random.Next(3) > 0 ? "support@example.com" : null // Some tickets unassigned
            }).ToList();
            
            // Add some comments to a few tickets
            _mockTickets[0].Comments.Add(new Comment { 
                ID = 1, 
                Author = "support@example.com", 
                Text = "We are looking into this issue.", 
                Date = DateTime.Now.AddHours(-2) 
            });
            
            _mockTickets[1].Comments.Add(new Comment { 
                ID = 2, 
                Author = "current.user@example.com", 
                Text = "Thanks for your quick response!", 
                Date = DateTime.Now.AddHours(-1) 
            });
            
            _initialized = true;
        }
        
        private string Lorem(Random random, int words)
        {
            var loremWords = "Lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua".Split(' ');
            return string.Join(" ", Enumerable.Range(0, words).Select(_ => loremWords[random.Next(loremWords.Length)]));
        }
        
        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            InitializeMockData();
            await Task.Delay(300); // Simulate network delay
            return _mockTickets.ToList();
        }
        
        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            InitializeMockData();
            await Task.Delay(500); // Simulate network delay
            return _mockTickets.FirstOrDefault(t => t.ID == id);
        }
        
        public async Task<Ticket> CreateTicketAsync(Ticket ticket)
        {
            InitializeMockData();
            await Task.Delay(800); // Simulate network delay
            
            ticket.ID = _mockTickets.Count > 0 ? _mockTickets.Max(t => t.ID) + 1 : 1;
            ticket.Date = DateTime.Now;
            
            _mockTickets.Add(ticket);
            return ticket;
        }
        
        public async Task<bool> UpdateTicketAsync(Ticket ticket)
        {
            InitializeMockData();
            await Task.Delay(500); // Simulate network delay
            
            var existingTicket = _mockTickets.FirstOrDefault(t => t.ID == ticket.ID);
            if (existingTicket == null) return false;
            
            var index = _mockTickets.IndexOf(existingTicket);
            _mockTickets[index] = ticket;
            
            return true;
        }
        
        public async Task<bool> DeleteTicketAsync(int id)
        {
            InitializeMockData();
            await Task.Delay(500); // Simulate network delay
            
            var existingTicket = _mockTickets.FirstOrDefault(t => t.ID == id);
            if (existingTicket == null) return false;
            
            _mockTickets.Remove(existingTicket);
            return true;
        }
        
        public async Task<List<Ticket>> GetMyTicketsAsync(string userId)
        {
            InitializeMockData();
            await Task.Delay(300); // Simulate network delay
            
            return _mockTickets
                .Where(t => t.CreatedBy == userId || t.AssignedTo == userId)
                .ToList();
        }
        
        public async Task<List<string>> GetCategoriesAsync()
        {
            InitializeMockData();
            await Task.Delay(200); // Simulate network delay
            
            return _mockTickets
                .Select(t => t.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToList();
        }
        
        public async Task<bool> AssignEquipmentToTicketAsync(int ticketId, int equipmentId)
        {
            InitializeMockData();
            await Task.Delay(400); // Simulate network delay
            
            var ticket = _mockTickets.FirstOrDefault(t => t.ID == ticketId);
            if (ticket == null) return false;
            
            var equipment = await _equipmentService.GetEquipmentByIdAsync(equipmentId);
            if (equipment == null) return false;
            
            ticket.EquipmentId = equipmentId;
            ticket.Equipment = equipment;
            
            return true;
        }
        
        public async Task<bool> RemoveEquipmentFromTicketAsync(int ticketId)
        {
            InitializeMockData();
            await Task.Delay(300); // Simulate network delay
            
            var ticket = _mockTickets.FirstOrDefault(t => t.ID == ticketId);
            if (ticket == null) return false;
            
            ticket.EquipmentId = null;
            ticket.Equipment = null;
            
            return true;
        }
    }
}