using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CrossCutting.Command
{
    public abstract class CommandHandler<T> : IRequestHandler<T, CommandResult> where T:IRequest<CommandResult>
    {
        public abstract Task<CommandResult> Handle(T request, CancellationToken cancellationToken);
    }
}
