using LCE.Core.Commands;
using LCE.Core.Infrastructure;
using IndexOutOfRangeException = System.IndexOutOfRangeException;

namespace LCE.Infra.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    public readonly Dictionary<Type, Func<BaseCommand, Task>> _handlers = new();
    
    public void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand
    {
        if (_handlers.ContainsKey(typeof(T)))
        {
            throw new IndexOutOfRangeException("You cannot register more than one handler for a command");
        }

        _handlers.Add(typeof(T), x => handler((T)x));
    }

    public async Task SendAsync(BaseCommand command)
    {
        if (_handlers.TryGetValue(command.GetType(), out Func<BaseCommand, Task> handler))
        {
            await handler(command);
        }
        else
        {
            throw new ArgumentNullException(nameof(handler), "Handler was not found for the command");
        }
    }
}