using TelegramBot.ApiService.Domain.Entities;

namespace TelegramBot.ApiService.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Chat> Chats { get; }
    public DbSet<Message> Messages { get; }
    public DbSet<MessageDelivery> MessageDeliveries { get; }
    public DbSet<MailingList> MailingLists { get; }
    public DbSet<ChatMailingList> ChatMailingLists { get; }
    public DbSet<Bot> Bots { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
