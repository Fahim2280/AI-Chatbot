using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AI_Chatbot.Models
{
    
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Register> Register { get; set; }
        public DbSet<ChatRespons> ChatResponses { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

    }
}
