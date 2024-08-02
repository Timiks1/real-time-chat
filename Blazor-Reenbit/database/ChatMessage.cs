namespace Blazor_Reenbit.database
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public double? SentimentScore { get; set; }
    }
}
