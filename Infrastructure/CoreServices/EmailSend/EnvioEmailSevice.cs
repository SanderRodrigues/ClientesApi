using System.Threading.Tasks;

namespace Infrastructure.CoreServices.EmailSend
{
    public interface IEnvioEmailSevice
    {
        Task EnviarEmail(string destinatario, string body);
    }

    internal class EnvioEmailSevice: IEnvioEmailSevice
    {
        public Task EnviarEmail(string destinatario, string body)
        {
            return Task.CompletedTask;
        }
    }
}
