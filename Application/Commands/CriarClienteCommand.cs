using CrossCutting.Command;
using Flunt.Validations;

namespace Application.Commands
{
    public class CriarClienteCommand: Command
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract().Requires().IsNotNullOrEmpty(Nome, nameof(Nome), "Nome deve ser informado"));

            AddNotifications(new Contract().Requires().IsNotNullOrEmpty(SobreNome, nameof(SobreNome), "SobreNome deve ser informado"));

            AddNotifications(new Contract().Requires().IsEmail(Email, nameof(Email), "Email deve ser informado"));
        }
    }
}
