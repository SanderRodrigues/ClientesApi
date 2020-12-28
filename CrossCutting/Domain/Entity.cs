using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flunt.Notifications;
using MediatR;

namespace CrossCutting.Domain
{
    public abstract class Entity: Notifiable
    {
        private readonly List<INotification> _events = new List<INotification>();

        public virtual Guid Id { get; protected set; }

        protected Entity() => Id = Guid.NewGuid();

        protected void AddEvent(INotification eventItem)
        {
            _events.Add(eventItem);
        }

        protected void ClearEvents()
        {
            _events.Clear();
        }

        public async Task RaiseEventsAsync(IMediator mediator, bool clearEvents = false)
        {
            foreach (var ev in _events)
            {
                await mediator.Publish(ev);
            }

            if (clearEvents)
            {
                ClearEvents();
            }
        }
    }
}
