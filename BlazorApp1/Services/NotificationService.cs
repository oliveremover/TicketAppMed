using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp1.Models;

namespace BlazorApp1.Services
{
    public interface INotificationService
    {
        int UnreadCount { get; }
        event Action OnNotificationCountChanged;
        Task<List<NotificationItem>> GetNotificationsAsync();
        Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync();
    }

    public class NotificationService : INotificationService
    {
        private readonly ITicketService _ticketService;
        private List<NotificationItem> _notifications;
        
        public int UnreadCount => _notifications?.Count(n => !n.IsRead) ?? 0;
        
        public event Action OnNotificationCountChanged;

        public NotificationService(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<List<NotificationItem>> GetNotificationsAsync()
        {
            if (_notifications == null)
            {
                await LoadNotificationsAsync();
            }
            
            return _notifications;
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            if (_notifications == null)
            {
                await LoadNotificationsAsync();
            }
            
            var notification = _notifications.FirstOrDefault(n => n.Id == notificationId);
            if (notification != null && !notification.IsRead)
            {
                notification.IsRead = true;
                OnNotificationCountChanged?.Invoke();
            }
        }

        public async Task MarkAllAsReadAsync()
        {
            if (_notifications == null)
            {
                await LoadNotificationsAsync();
            }
            
            bool anyChanged = false;
            
            foreach (var notification in _notifications.Where(n => !n.IsRead))
            {
                notification.IsRead = true;
                anyChanged = true;
            }
            
            if (anyChanged)
            {
                OnNotificationCountChanged?.Invoke();
            }
        }

        private async Task LoadNotificationsAsync()
        {
            // In a real application, you would fetch notifications from a database
            // For now, we'll create mock data based on tickets and their comments
            var tickets = await _ticketService.GetAllTicketsAsync();
            _notifications = new List<NotificationItem>();
            
            if (tickets?.Any() == true)
            {
                int notificationId = 1;
                foreach (var ticket in tickets)
                {
                    // For demonstration purposes, convert each comment into a notification
                    if (ticket.Comments?.Any() == true)
                    {
                        foreach (var comment in ticket.Comments.OrderByDescending(c => c.Date).Take(3))
                        {
                            if (comment.Author != "current.user@example.com") // Don't notify about own comments
                            {
                                _notifications.Add(new NotificationItem
                                {
                                    Id = notificationId++,
                                    TicketId = ticket.ID,
                                    Message = comment.Text,
                                    Author = comment.Author,
                                    Date = comment.Date,
                                    IsRead = false
                                });
                            }
                        }
                    }
                }
            }
            
            // Sort notifications by date (newest first)
            _notifications = _notifications.OrderByDescending(n => n.Date).ToList();
            
            OnNotificationCountChanged?.Invoke();
        }
    }

    public class NotificationItem
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
    }
}