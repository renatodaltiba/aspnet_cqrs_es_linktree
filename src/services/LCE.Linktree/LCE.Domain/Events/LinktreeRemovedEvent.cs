using LCE.Core.Events;

namespace LCE.Domain.Events;

public class LinktreeRemovedEvent : BaseEvent
{
    public LinktreeRemovedEvent() : base(nameof(LinktreeRemovedEvent))
    {
    }
}