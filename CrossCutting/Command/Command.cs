using Flunt.Notifications;
using MediatR;

namespace CrossCutting.Command
{
    public abstract class Command : Notifiable, IRequest<CommandResult>
    {
        public abstract void Validate();
    }
}
