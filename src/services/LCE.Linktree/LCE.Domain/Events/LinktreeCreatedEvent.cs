using LCE.Core.Events;

namespace LCE.Domain.Events;

public class LinktreeCreatedEvent : BaseEvent
{
    public LinktreeCreatedEvent() : base(nameof(LinktreeCreatedEvent))
    {
    }

    public string Username { get; set; }
    public string Avatar { get; set; }
    public string Bio { get; set; }
    public DateTime DatePosted { get; set; }
}