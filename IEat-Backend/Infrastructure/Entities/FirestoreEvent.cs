using Core.Events;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities
{
    [FirestoreData]
    public class FirestoreEvent
    {
        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty]
        public string AggregateId { get; set; }

        [FirestoreProperty]
        public string AggregateName { get; set; }

        [FirestoreProperty]
        public string Data { get; set; }

        [FirestoreProperty]
        public string Version { get; set; }

        [FirestoreProperty]
        public string EventType { get; set; }

        public static FirestoreEvent FromDomainEvent(DomainEvent @event)
        => new()
        {
            Id = @event.Id.ToString(),
            AggregateId = @event.AggregateId.ToString(),
            AggregateName = @event.AggregateName.ToString(),
            Data = JsonConvert.SerializeObject(@event),
            Version = @event.Version.ToString(),
            EventType = @event.EventType.ToString(),
        };
    }
}
