using LCE.Core.Events;

namespace LCE.Domain.Events;

public class LinktreeUpdatedEvent : BaseEvent
{
    public LinktreeUpdatedEvent() : base(nameof(LinktreeUpdatedEvent))
    {
    }
    
    public string Avatar { get; set; }
    public string Bio { get; set; }
    public DateTime LinktreeUpdatedDate { get; set; }
}