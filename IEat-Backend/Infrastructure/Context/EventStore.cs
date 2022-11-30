using Core.Events;
using Core.EventStoreContext;
using Google.Cloud.Firestore;
using Infrastructure.Common.Constants;
using Infrastructure.Data.Entities;
using MediatR;
using Newtonsoft.Json;
using System.Reflection;

namespace Infrastructure.Data.Context
{
    public class EventStore : IEventStore
    {
        private readonly FirestoreDb db = FirestoreDb.Create(FirebaseConstants.FirebaseProjectId);
        private readonly IMediator mediator;

        public EventStore(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IEnumerable<IDomainEvent>> GetEvents(Guid aggregateId, string aggregateName)
        {
            var resultSnapshot = await db.Collection(path: aggregateName)
                                         .GetSnapshotAsync();
            if (!resultSnapshot.Any()) return Enumerable.Empty<IDomainEvent>();

            var eventsForAggregate = resultSnapshot.Where(e => Guid.Parse(e.GetValue<string>(DomainEventFieldConstants.AggregateId)) == aggregateId && aggregateId != Guid.Empty);
            return eventsForAggregate.Select(MapDataToEvent)
                .OrderBy(GetVersionCounter);
        }

        public async Task Save(DomainEvent @event)
        {
            @event.Version = DomainEvent.NextVersion(GetLatestVersion(@event));
            var firestoreEvent = FirestoreEvent.FromDomainEvent(@event);
            await db.Collection(firestoreEvent.AggregateName)
                    .AddAsync(firestoreEvent);
        }

        private string? GetLatestVersion(DomainEvent @event) => GetEvents(@event.AggregateId, @event.AggregateName).Result?.LastOrDefault()?.Version ?? @event.Version;
        
        private static Type GetTypeFromAssemblies(string typeName) => Assembly.GetExecutingAssembly()
                                                                              .GetReferencedAssemblies()
                                                                              .Select(Assembly.Load)
                                                                              .SelectMany(x => x.GetTypes()).First(x => x.FullName == typeName);

        private DomainEvent MapDataToEvent(DocumentSnapshot data) => JsonConvert.DeserializeObject(data.GetValue<string>(DomainEventFieldConstants.Data), GetTypeFromAssemblies(data.GetValue<string>(DomainEventFieldConstants.EventType))) as DomainEvent ?? throw new Exception("Error during data serialization!!");

        public Task PublishAsync(DomainEvent @event) => mediator.Publish(@event);

        private int GetVersionCounter(DomainEvent document) => int.Parse(document.Version.Split("|").Last());
    }
}
