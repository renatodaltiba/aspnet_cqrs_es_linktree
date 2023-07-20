using LCE.Core.EventBus;
using LCE.Core.Handlers;
using LCE.Core.Infrastructure;
using LCE.Domain.Domain;

namespace LCE.Infra.Handler;

public class EventSourcingHandler : IEventSourcingHandler<LinktreeAggregate>
{
    private readonly IEventStore _eventStore;
    private readonly IEventPublish _eventProducer;

    public EventSourcingHandler(IEventStore eventStore, IEventPublish eventProducer)
    {
        _eventStore = eventStore;
        _eventProducer = eventProducer;
    }

    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await _eventStore.SaveEventAsync(aggregate.Id, aggregate.GetUncommittedChanges(), aggregate.Version);
        
        aggregate.MarkChangeAsCommitted();
    }

    public async Task<LinktreeAggregate> GetByIdAsync(Guid aggregateId)
    {
        var aggregate = new LinktreeAggregate();
        
        var events = await _eventStore.GetEventsAsync(aggregateId);
        
        if (events == null || !events.Any()) return aggregate;
        
        aggregate.ReplayEvents(events);
        
        aggregate.Version = events.Select(x => x.Version).Max();
        
        return aggregate;
    }

    public Task RepublishEventsAsync()
    {
        throw new NotImplementedException();
    }
}