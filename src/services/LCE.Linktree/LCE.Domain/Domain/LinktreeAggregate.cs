using LCE.Core.Handlers;
using LCE.Domain.Events;

namespace LCE.Domain.Domain;

public class LinktreeAggregate : AggregateRoot
{
    private bool _active;
    
    private string _username { get; set; }

    private readonly Dictionary<Guid, Tuple<string, string>> _links = new();
    
    public LinktreeAggregate() { }

    public LinktreeAggregate(Guid id, string username, string avatar, string bio)
    {
        RaiseEvent(new LinktreeCreatedEvent
        {
            Id = id,
            Avatar = avatar,
            Username = username,
            Bio = bio,
            DatePosted = DateTime.Now
        });
    }
    
    public void Apply(LinktreeCreatedEvent @event)
    {
        _id = @event.Id;
       _active = true;
       _username = @event.Username;
    }
    
    public void UpdateLinktree(string avatar, string bio)
    {
        if (!_active)
        {
            throw new InvalidOperationException("Linktree is not active!");
        }

        if (string.IsNullOrWhiteSpace(avatar))
        {
            throw new InvalidOperationException($"The value of {nameof(avatar)} cannot be null or empty, Please provide a valid value!");
        }
        
        if (string.IsNullOrWhiteSpace(bio))
        {
            throw new InvalidOperationException($"The value of {nameof(bio)} cannot be null or empty, Please provide a valid value!");
        }
        
        RaiseEvent(new LinktreeUpdatedEvent
        {
            Id = _id,
            Avatar = avatar,
            Bio = bio,
            LinktreeUpdatedDate = DateTime.Now
        });
    }
    
    public void Apply(LinktreeUpdatedEvent @event)
    {
        _id = @event.Id;
    }
    
    public void AddLink(string title, string url)
    {
        if (!_active)
        {
            throw new InvalidOperationException("Linktree is not active!");
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new InvalidOperationException($"The value of {nameof(title)} cannot be null or empty, Please provide a valid value!");
        }
        
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new InvalidOperationException($"The value of {nameof(url)} cannot be null or empty, Please provide a valid value!");
        }
        
        RaiseEvent(new LinkAddedEvent
        {
            Id = _id,
            LinkId = Guid.NewGuid(),
            Title = title,
            Link = url,
            DateAdded = DateTime.Now
        });
    }
    
    public void Apply(LinkAddedEvent @event)
    {
        _id = @event.Id;
    }
    
    public void RemoveLink(Guid linkId, string username)
    {
        if (!_active)
        {
            throw new InvalidOperationException("Linktree is not active!");
        }

        if (!_links[linkId].Item2.Equals(username, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new InvalidOperationException($"The value of {nameof(username)} does not match the username of the link you are trying to remove!");
        }
        
        RaiseEvent(new LinkRemovedEvent
        {
            Id = _id,
            LinkId = linkId
        });
    }
    
    public void Apply(LinkRemovedEvent @event)
    {
        _id = @event.Id;
        _links.Remove(@event.LinkId);
    }


    public void UpdateLink(Guid linkId, string username, string url, string title)
    {
        if(!_active)
        {
            throw new InvalidOperationException("Linktree is not active!");
        }
        
        if (!_links[linkId].Item2.Equals(username, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new InvalidOperationException($"The value of {nameof(username)} does not match the username of the link you are trying to update!");
        }
        
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new InvalidOperationException($"The value of {nameof(title)} cannot be null or empty, Please provide a valid value!");
        }
        
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new InvalidOperationException($"The value of {nameof(url)} cannot be null or empty, Please provide a valid value!");
        }
        
        RaiseEvent(new LinkUpdatedEvent
        {
            Id = _id,
            LinkId = linkId,
            Title = title,
            Link = url
        });
    }
    
    public void Apply(LinkUpdatedEvent @event)
    {
        _id = @event.Id;
        _links[@event.LinkId] = new Tuple<string, string>(@event.Title, @event.Link);
    }
    
    
    public void RemoveLinktree(string username)
    {
        if (!_active)
        {
            throw new InvalidOperationException("Linktree is not active!");
        }

        if (!_username.Equals(username, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new InvalidOperationException($"The value of {nameof(username)} does not match the username of the linktree you are trying to remove!");
        }
        
        RaiseEvent(new LinktreeRemovedEvent
        {
            Id = _id
        });
    }
    
    public void Apply(LinktreeRemovedEvent @event)
    {
        _id = @event.Id;
        _active = false;
    }
 
}