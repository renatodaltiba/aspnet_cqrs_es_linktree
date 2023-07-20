using System.Text.Json;
using LCE.Core.EventBus;
using LCE.Core.Events;
using LCE.Core.Infrastructure;
using LCE.Core.Repository;

namespace LCE.Infra.Stores;

public class EventStore : IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IEventPublish _eventPublish;
    
    public EventStore(IEventStoreRepository eventStoreRepository, IEventPublish eventPublish)
    {
        _eventStoreRepository = eventStoreRepository;
        _eventPublish = eventPublish;
    }

    
    public async Task SaveEventAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
    {
        var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);
        
        if(expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
        {
            throw new Exception($"Aggregate {aggregateId} has been modified");
        }
        
        var version = expectedVersion;
        
        foreach (var @event in events)
        {
            version++;
            @event.Version = version;
            var eventType = @event.GetType().Name;
            var eventStore = new EventModel
            {
                AggregateIdentifier = aggregateId,
                EventType = eventType,
                EventData = @event,
                Version = version
            };
            await _eventStoreRepository.SaveAsync(eventStore);
            await _eventPublish.ProduceAsync("LinktreeEvents", @event);
        }
        
    }

    public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
    {
        var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

        if (eventStream == null || !eventStream.Any())
        {
            throw new Exception($"Aggregate with id {aggregateId} not found");
        }

        return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
    }
}