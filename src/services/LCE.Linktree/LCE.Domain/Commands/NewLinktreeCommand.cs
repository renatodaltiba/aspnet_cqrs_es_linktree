using LCE.Core.Commands;

namespace LCE.Domain.Commands;

public class NewLinktreeCommand : BaseCommand
{
    public string Username { get; set; }
    public string Avatar { get; set; }
    public string Bio { get; set; }
}