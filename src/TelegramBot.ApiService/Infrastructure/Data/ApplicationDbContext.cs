using Microsoft.EntityFrameworkCore;
using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Domain.Entities;

namespace TelegramBot.ApiService.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<MessageDelivery> MessageDeliveries => Set<MessageDelivery>();
    public DbSet<MailingList> MailingLists => Set<MailingList>();
    public DbSet<ChatMailingList> ChatMailingLists => Set<ChatMailingList>();
    public DbSet<Bot> Bots => Set<Bot>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("public");
        modelBuilder.UseIdentityByDefaultColumns();

        modelBuilder.Entity<Message>()
            .Property(e => e.MessageType)
            .HasConversion<string>();

        modelBuilder.Entity<MessageDelivery>()
            .Property(e => e.Status)
            .HasConversion<string>();

        modelBuilder.Entity<ChatMailingList>(entity =>
        {
            entity.HasKey(e => new { e.ChatId, e.MailingListId });

            entity.HasOne(e => e.Chat)
                .WithMany(e => e.ChatMailingLists)
                .HasForeignKey(e => e.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.MailingList)
                .WithMany(e => e.ChatMailingLists)
                .HasForeignKey(e => e.MailingListId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Настройка связи между Bot и Chat
        modelBuilder.Entity<Chat>()
            .HasOne(c => c.Bot)
            .WithMany(b => b.Chats)
            .HasForeignKey(c => c.BotId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Chat>()
            .HasIndex(e => e.ChatId)
            .IsUnique();

        modelBuilder.Entity<Bot>()
            .HasIndex(e => e.Key)
            .IsUnique();
    }
}
