using System;

namespace Domain.Entitys.Cliente
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string SobreNome { get; private set; }
        public string Email { get; private set; }

        public Cliente(string nome, string sobreNome, string email)
        {
            Nome = nome;
            SobreNome = sobreNome;
            Email= email;
        }
    }
}
