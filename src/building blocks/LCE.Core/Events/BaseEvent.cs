using LCE.Core.Messages;

namespace LCE.Core.Events;

public class BaseEvent : Message
{
    public BaseEvent(string type)
    {
        this.Type = type;
    }

    public int Version { get; set; }
    public string Type { get; set; }
}