using MessageRoomSolution.Data.Entities;
using MessageRoomSolution.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageRoomSolution.Data.Configuration
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable("Participants");
            builder.HasKey(x => new { x.ConversationId, x.UserId });
            builder.Property(x => x.IsAdmin).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.IsJoined).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.UpdatedAt).IsRequired().HasDefaultValue(DateTime.Now);
            builder.HasOne(x => x.Conversation)
                .WithMany(x => x.Participants)
                .HasForeignKey(x => x.ConversationId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.User)
                .WithMany(x => x.Participants)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}