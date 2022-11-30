using Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EventStoreContext
{
    public interface IEventStore
    {
        Task PublishAsync(DomainEvent @event);
        Task Save(DomainEvent @event);
        Task<IEnumerable<IDomainEvent>> GetEvents(Guid id, string aggregateName);
    }
}
