using MessageRoomSolution.Data.Configuration;
using MessageRoomSolution.Data.Entities;
using MessageRoomSolution.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MessageRoomSolution.Data.EF
{
    public class MessageRoomDbContext : DbContext
    {
        public MessageRoomDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AttachmentConfiguration());
            builder.ApplyConfiguration(new ConversationConfiguration());
            builder.ApplyConfiguration(new MessageConfiguration());
            builder.ApplyConfiguration(new ParticipantConfiguration());
            builder.ApplyConfiguration(new AppUserConfiguration());
            //Data Seeding
            builder.Seed();
            //base.OnModelCreating(builder);
        }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Participant> Participants { get; set; }
    }
}