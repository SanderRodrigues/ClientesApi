using System;
using CrossCutting.Domain;
using Domain.Events;

namespace Domain.Entitys.Cliente
{
    public class Cliente: Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string SobreNome { get; private set; }
        public string Email { get; private set; }
        public bool EmailConfirmacaoEnviado { get; private set; }
        public string MensagemErro { get; private set; }

        public Cliente(Guid id, string nome, string sobreNome, string email) : base()
        {
            Id = id;
            Nome = nome;
            SobreNome = sobreNome;
            Email = email;
        }

        public Cliente(string nome, string sobreNome, string email): base()
        {
            Nome = nome;
            SobreNome = sobreNome;
            Email= email;
            AddEvent(new ClienteCriado(Id, email));
        }

        public void SinalizarEmailConfirmacaoEnviado()
        {
            EmailConfirmacaoEnviado = true;
            MensagemErro = "";
        }

        public void SinalizarErroEnvioEmailConfirmacao(string mensagem)
        {
            EmailConfirmacaoEnviado = false;
            MensagemErro = mensagem;
        }
    }
}
