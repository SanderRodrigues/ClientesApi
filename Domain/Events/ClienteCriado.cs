using System;
using CrossCutting.Domain;

namespace Domain.Events
{
    public class ClienteCriado: Event
    {
        public readonly Guid Id;
        public readonly string Email;

        public ClienteCriado(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
