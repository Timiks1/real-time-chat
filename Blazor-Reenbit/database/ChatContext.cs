using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blazor_Reenbit.database
{

    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }

        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}
