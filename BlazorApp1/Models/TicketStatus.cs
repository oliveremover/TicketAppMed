namespace BlazorApp1.Models
{
    public enum TicketStatus
    {
        Open = 0,
        Pending = 1,
        Closed = 2,
        Unknown = -1
    }
    
    public static class StatusExtensions
    {
        public static string ToDisplayString(this TicketStatus status)
        {
            return status switch
            {
                TicketStatus.Open => "Open",
                TicketStatus.Pending => "Pending",
                TicketStatus.Closed => "Closed",
                _ => "Unknown"
            };
        }
    }
}