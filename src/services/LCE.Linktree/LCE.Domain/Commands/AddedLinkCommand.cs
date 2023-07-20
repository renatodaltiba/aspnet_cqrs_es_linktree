using LCE.Core.Commands;

namespace LCE.Domain.Commands;

public class AddedLinkCommand : BaseCommand
{
    public string Title { get; set; }
    public string Username { get; set; }
    public string Link { get; set; }
}