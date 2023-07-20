using LCE.Core.Commands;

namespace LCE.Domain.Commands;

public class EditLinkCommand : BaseCommand
{
    public Guid LinkId { get; set; }
    public string Username { get; set; }
    public string Title { get; set; }
    public string Link { get; set; }
}