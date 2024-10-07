using System.ComponentModel.DataAnnotations;

namespace PosFiapTech1.Domain.Entities
{
    public class Contato
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public string Ddd { get; set; }

        [Required]
        public string Endereco { get; set; }

        // Construtor padrão
        public Contato() { }

        // Construtor com parâmetros (opcional)
        public Contato(string nome, string email, string telefone, string ddd, string endereco)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Ddd = ddd;
            Endereco = endereco;
        }
    }
}
