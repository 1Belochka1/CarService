using CarService.Core.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarService.Infrastructure.Persistence.Configurations.ChatsCongfiguration;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        
        builder.Property(x => x.ChatId)
            .IsRequired();
        
        builder.Property(x => x.SenderId)
            .IsRequired();
        
        builder.Property(x => x.Content)
            .IsRequired();
        
        builder.Property(x => x.SendDate)
            .HasDefaultValue(DateTime.UtcNow);

        builder.HasOne(x => x.Sender)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.SenderId);
    }
    
}