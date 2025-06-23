using System;
using System.Collections.Generic;

namespace BlazorApp1.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        public string Header { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public string Category { get; set; } = string.Empty;
        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public string? AssignedTo { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public int? EquipmentNumber { get; set; }
        public Equipment? Equipment { get; set; }
        
        // Helper method to generate a formatted ticket ID
        public string GetFormattedID() => ID.ToString().PadLeft(6, '0');
        
        // Helper method to get relative time
        public string GetTimeAgo()
        {
            var timeSpan = DateTime.Now - Date;

            if (timeSpan.TotalDays > 30)
                return Date.ToString("MMM d");
            else if (timeSpan.TotalDays > 1)
                return $"{(int)timeSpan.TotalDays}d ago";
            else if (timeSpan.TotalHours > 1)
                return $"{(int)timeSpan.TotalHours}h ago";
            else if (timeSpan.TotalMinutes > 1)
                return $"{(int)timeSpan.TotalMinutes}m ago";
            else
                return "just now";
        }
    }
    
    public class Attachment
    {
        public int ID { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public long FileSize { get; set; }
    }
    
    public class Comment
    {
        public int ID { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public string Author { get; set; } = string.Empty;
        public bool IsInternal { get; set; }
    }
}