using LCE.Core.Handlers;
using LCE.Domain.Domain;

namespace LCE.Domain.Commands;

public class CommandHandler : ICommandHandler
{

    private readonly IEventSourcingHandler<LinktreeAggregate> _eventSourcingHandler;

    public CommandHandler(IEventSourcingHandler<LinktreeAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }

    public async Task HandleAsync(NewLinktreeCommand command)
    {
        var aggregate = new LinktreeAggregate(command.Id, command.Avatar, command.Username, command.Bio);
        
        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(EditLinktreeCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        
        aggregate.UpdateLinktree(command.Avatar, command.Username);

       await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(EditLinkCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        
        aggregate.UpdateLink(command.LinkId, command.Username, command.Link, command.Title);
        
        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(DeleteLinkCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        
        aggregate.RemoveLink(command.LinkId, command.Username);
        
        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(DeleteLinktreeCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        
        aggregate.RemoveLinktree(command.Username);
        
        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(AddedLinkCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        
        aggregate.AddLink(command.Title, command.Link);
        
        await _eventSourcingHandler.SaveAsync(aggregate);
    }
}