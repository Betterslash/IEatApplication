using Core.Events;
using Core.EventStoreContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IReconstituteHandlerRoot { }
    public interface IReconstituteHandler<T> : IReconstituteHandlerRoot where T : class, IDomainEvent
    {
        void When(T @event);
    }

    public interface IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Version { get; set; }
        public IList<IDomainEvent> PendingEvents { get; set; }
        public IEnumerable<IDomainEvent> AllEvents { get; set; }
        public string AggregateName { get; }
        Task ProcessEvent(DomainEvent @event);
    }

    public abstract class AggregateRoot : IAggregateRoot
    {
        protected readonly IEventStore eventStore;

        protected AggregateRoot(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public Guid Id { get; set; }
        public string Version { get; set; }
        public IList<IDomainEvent> PendingEvents { get; set; }
        public IEnumerable<IDomainEvent> AllEvents { get; set; }
        public string AggregateName { get => GetType().Name; }

        public async Task ProcessEvent(DomainEvent @event)
        {
            await SaveEventAsync(@event);
            await eventStore.PublishAsync(@event);
            Reconstitute();
        }

        public void Reconstitute()
        {
            AllEvents = eventStore.GetEvents(Id, AggregateName).Result;
            if (!AllEvents.Any()) return;

            var implementingType = GetType();
            if (implementingType.IsAbstract) throw new Exception($"Type getter failed because {implementingType.Name} is an abstract type!!");
            var handlersMap = implementingType.GetInterfaces()
                                              .Where(x => x.GetInterfaces().Contains(typeof(IReconstituteHandlerRoot)))
                                              .SelectMany(x => x.GetMethods())
                                              .ToDictionary(x => x.GetParameters().First().ParameterType.FullName ?? string.Empty, x => x);
            AllEvents.OrderBy(e => e.Version)
                     .ToList()
                     .ForEach(@event => handlersMap[@event.EventType].Invoke(this, new[] { @event }));
            Version = AllEvents.Last().Version;
        }

        protected async Task SaveEventAsync(DomainEvent @event)
        {
            await eventStore.Save(@event);
            if (!PendingEvents?.Any() ?? true) PendingEvents = new List<IDomainEvent>();
            PendingEvents?.Add(@event);
        }
    }
}
