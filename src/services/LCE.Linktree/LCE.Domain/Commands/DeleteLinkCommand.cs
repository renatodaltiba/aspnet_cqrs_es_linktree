using LCE.Core.Commands;

namespace LCE.Domain.Commands;

public class DeleteLinkCommand : BaseCommand
{
    public Guid LinkId { get; set; }
    public string Username { get; set; }
}