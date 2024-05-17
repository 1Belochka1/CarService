using CarService.Core.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarService.Infrastructure.Persistence.Configurations.ChatsCongfiguration;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired();
        
        builder.Property(x => x.CreateDate)
            .HasDefaultValue(DateTime.UtcNow);
        
        builder.HasOne(x => x.CreateByUser)
            .WithMany(x => x.Chats)
            .HasForeignKey(x => x.CreateBy)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Messages)
            .WithOne(x => x.Chat)
            .HasForeignKey(x => x.ChatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}