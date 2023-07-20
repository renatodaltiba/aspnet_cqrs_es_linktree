namespace LCE.Domain.Commands;

public interface ICommandHandler
{
    Task HandleAsync(NewLinktreeCommand command);
    Task HandleAsync(EditLinktreeCommand command);
    Task HandleAsync(EditLinkCommand command);
    Task HandleAsync(DeleteLinkCommand command);
    Task HandleAsync(DeleteLinktreeCommand command);
    Task HandleAsync(AddedLinkCommand command);
}