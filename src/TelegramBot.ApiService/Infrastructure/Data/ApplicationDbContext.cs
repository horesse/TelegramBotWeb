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
    public DbSet<Setting> Settings => Set<Setting>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Указываем схему для всех таблиц (опционально)
        modelBuilder.HasDefaultSchema("public");

        // Настройка для работы с PostgreSQL
        modelBuilder.UseIdentityByDefaultColumns();

        // Настройка для enum - сохраняем как строки
        modelBuilder.Entity<Message>()
            .Property(e => e.MessageType)
            .HasConversion<string>();

        modelBuilder.Entity<MessageDelivery>()
            .Property(e => e.Status)
            .HasConversion<string>();

        // Настройка составного ключа
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

        // Настройка индексов
        modelBuilder.Entity<Chat>()
            .HasIndex(e => e.ChatId)
            .IsUnique();

        modelBuilder.Entity<Setting>()
            .HasIndex(e => e.Key)
            .IsUnique();
    }
}

