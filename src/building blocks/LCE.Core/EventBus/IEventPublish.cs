using LCE.Core.Events;

namespace LCE.Core.EventBus;

public interface IEventPublish
{
    Task ProduceAsync<T>(string topic, T @event) where T : BaseEvent;
}