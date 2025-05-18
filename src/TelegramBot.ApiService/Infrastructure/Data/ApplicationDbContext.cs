using Microsoft.EntityFrameworkCore;
using TelegramBot.ApiService.Domain.Entities;

namespace TelegramBot.ApiService.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<MessageDelivery> MessageDeliveries { get; set; }
    public DbSet<MailingList> MailingLists { get; set; }
    public DbSet<ChatMailingList> ChatMailingLists { get; set; }
    public DbSet<Setting> Settings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ChatMailingList>()
            .HasKey(cml => new { cml.ChatId, cml.MailingListId });

        modelBuilder.Entity<ChatMailingList>()
            .HasOne(cml => cml.Chat)
            .WithMany(c => c.ChatMailingLists)
            .HasForeignKey(cml => cml.ChatId);

        modelBuilder.Entity<ChatMailingList>()
            .HasOne(cml => cml.MailingList)
            .WithMany(ml => ml.ChatMailingLists)
            .HasForeignKey(cml => cml.MailingListId);
    }
}

