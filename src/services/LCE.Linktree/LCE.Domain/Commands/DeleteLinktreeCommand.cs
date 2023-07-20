using LCE.Core.Commands;

namespace LCE.Domain.Commands;

public class DeleteLinktreeCommand : BaseCommand
{
    public string Username { get; set; }
}