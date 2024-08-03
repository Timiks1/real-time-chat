using Blazor_Reenbit.database;
using Blazor_Reenbit.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;

namespace Blazor_Reenbit.Hubs
{


    public class ChatHub : Hub
    {
        private readonly ChatContext _context;
        private readonly SentimentAnalysisService _sentimentAnalysisService;

        public ChatHub(ChatContext context, SentimentAnalysisService sentimentAnalysisService)
        {
            _context = context;
            _sentimentAnalysisService = sentimentAnalysisService;
        }

        public async Task SendMessage(string user, string message)
        {
            var sentimentScore = await _sentimentAnalysisService.AnalyzeSentimentAsync(message);

            var chatMessage = new ChatMessage
            {
                User = user,
                Message = message,
                Timestamp = DateTime.UtcNow,
                SentimentScore = sentimentScore
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", user, message, sentimentScore);
        }
    }

}