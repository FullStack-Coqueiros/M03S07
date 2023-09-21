using System.ComponentModel.DataAnnotations;

namespace FichaCadastroAPI.DTO.Ficha
{
    public class FichaReadDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string ContatenacaoNomeEmail { get; set; }
    }
}
