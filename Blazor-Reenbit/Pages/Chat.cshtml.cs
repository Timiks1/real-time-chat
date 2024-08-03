using Blazor_Reenbit.database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    public class ChatModel : PageModel
    {
        private readonly ChatContext _context;

        public ChatModel(ChatContext context)
        {
            _context = context;
        }

        public IList<ChatMessage> Messages { get; set; }

        public async Task OnGetAsync()
        {
            Messages = await _context.ChatMessages
                .OrderByDescending(m => m.Timestamp)
                .ToListAsync();
        }
    }
}
