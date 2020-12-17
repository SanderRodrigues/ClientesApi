namespace Infrastructure.DataModel.Models
{
    internal class ClienteDbModel: DbModel
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
    }
}
