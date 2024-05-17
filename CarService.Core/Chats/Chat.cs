using CarService.Core.Users;
using CSharpFunctionalExtensions;

namespace CarService.Core.Chats;

public class Chat
{
    private Chat(Guid id, string name, Guid createBy, DateTime createDate, DateTime lastMessageDate)
    {
        Id = id;
        Name = name;
        CreateBy = createBy;
        CreateDate = createDate;
        LastMessageDate = lastMessageDate;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public Guid CreateBy { get; private set; }

    public UserAuth CreateByUser { get; private set; } = null!;

    public DateTime CreateDate { get; private set; }

    public DateTime LastMessageDate { get; private set; }
    
    public virtual List<Message> Messages { get; private set; } = [];

    public static Result<Chat> Create(Guid id, string name, Guid createBy, DateTime createDate,
        DateTime lastMessageDate)
    {
        if (id == Guid.Empty)
            return Result.Failure<Chat>("Id can't be empty");

        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Chat>("Name can't be empty");

        if (createBy == Guid.Empty)
            return Result.Failure<Chat>("CreateBy can't be empty");

        if (createDate == DateTime.MinValue)
            return Result.Failure<Chat>("CreateDate can't be empty");

        if (lastMessageDate == DateTime.MinValue)
            return Result.Failure<Chat>("LastMessageDate can't be empty");

        var chat = new Chat(id, name, createBy, createDate, lastMessageDate);

        return Result.Success(chat);
    }
}