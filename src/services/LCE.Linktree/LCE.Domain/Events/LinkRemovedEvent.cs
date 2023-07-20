using LCE.Core.Events;

namespace LCE.Domain.Events;

public class LinkRemovedEvent : BaseEvent
{
    public LinkRemovedEvent() : base(nameof(LinkRemovedEvent))
    {
    }
    public Guid LinkId { get; set; }
}