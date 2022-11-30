using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public interface IDomainEvent : INotification
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string AggregateName { get; }
        public string Version { get; set; }
        public string EventType { get; }
    }

    public abstract class DomainEvent : IDomainEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public abstract string AggregateName { get; }
        public string Version { get; set; }
        public string EventType { get => GetType().FullName; }

        public static string NextVersion(string? version)
        {
            if(version == null) throw new ArgumentNullException(nameof(version));
            var splitVersion = version.Split("|");
            var id = splitVersion[0];
            var counter = int.Parse(splitVersion[1]);
            counter++;
            return id+ "|" + counter;
        }
    }
}
