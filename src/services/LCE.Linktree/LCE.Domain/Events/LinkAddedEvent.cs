using LCE.Core.Events;

namespace LCE.Domain.Events;

public class LinkAddedEvent : BaseEvent
{
    public LinkAddedEvent() : base(nameof(LinkAddedEvent))
    {
    }
    
    public Guid LinkId { get; set; }
    public string Link { get; set; }
    public string Title { get; set; }
    public DateTime DateAdded { get; set; }
}