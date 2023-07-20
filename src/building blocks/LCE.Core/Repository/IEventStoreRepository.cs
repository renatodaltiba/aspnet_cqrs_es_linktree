using LCE.Core.Events;

namespace LCE.Core.Repository;

public interface IEventStoreRepository
{
    Task SaveAsync(EventModel @event);
    Task<List<EventModel>> FindByAggregateId(Guid aggregateId);
    Task<List<EventModel>> findAllAsync();
}