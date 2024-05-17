using CarService.Core.Users;
using CSharpFunctionalExtensions;

namespace CarService.Core.Chats;

public class Message
{
    private Message(Guid id, Guid chatId, Guid senderId, string content, DateTime sendDate)
    {
        Id = id;
        ChatId = chatId;
        SenderId = senderId;
        Content = content;
        SendDate = sendDate;
    }

    public Guid Id { get; private set; }

    public Guid ChatId { get; private set; }

    public Guid SenderId { get; private set; }

    public string Content { get; private set; }

    public DateTime SendDate { get; private set; }

    public Chat Chat { get; private set; } = null!;
    
    public UserAuth Sender { get; private set; } = null!;

    public static Result<Message> Create(Guid id, Guid chatId, Guid senderId, string content, DateTime sendDate)
    {
        if (id == Guid.Empty)
            return Result.Failure<Message>("Id can't be empty");

        if (chatId == Guid.Empty)
            return Result.Failure<Message>("ChatId can't be empty");

        if (senderId == Guid.Empty)
            return Result.Failure<Message>("SenderId can't be empty");

        if (string.IsNullOrWhiteSpace(content))
            return Result.Failure<Message>("Content can't be empty");

        var message = new Message(id, chatId, senderId, content, sendDate);

        return Result.Success(message);
    }
}