using LCE.Core.Events;

namespace LCE.Domain.Events;

public class LinkUpdatedEvent : BaseEvent
{
    public LinkUpdatedEvent() : base(nameof(LinkUpdatedEvent))
    {
    }

    public Guid LinkId { get; set; }
    public string Link { get; set; }
    public string Title { get; set; }
    
    public string DateUpdated { get; set; }
}