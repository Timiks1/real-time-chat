using System;
using System.Threading.Tasks;
using Azure;
using Azure.AI.TextAnalytics;

namespace Blazor_Reenbit.Services
{
    public class SentimentAnalysisService
    {
        private readonly TextAnalyticsClient _client;

        public SentimentAnalysisService(string apiKey, string endpoint)
        {
            var credential = new AzureKeyCredential(apiKey);
            var endpointUri = new Uri(endpoint);
            _client = new TextAnalyticsClient(endpointUri, credential);
        }

        public async Task<double> AnalyzeSentimentAsync(string message)
        {
            try
            {
                DocumentSentiment documentSentiment = await _client.AnalyzeSentimentAsync(message);
                return documentSentiment.ConfidenceScores.Positive;
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
