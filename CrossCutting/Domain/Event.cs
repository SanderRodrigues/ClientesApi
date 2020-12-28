using System;
using MediatR;

namespace CrossCutting.Domain
{
    public abstract class Event : INotification
    {
        public DateTime DateTimeCreated { get; private set; }

        protected Event()
        {
            DateTimeCreated = DateTime.Now;
        }
    }
}
