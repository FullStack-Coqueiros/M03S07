using System.ComponentModel.DataAnnotations;

namespace FichaCadastroAPI.DTO.Ficha
{
    public class FichaUpdateDTO
    {
        [Required(ErrorMessage = "E-mail obrigat�rio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Data de nascimento obrigat�rio")]
        [Range(typeof(DateTime), "1980-01-01", "2050-12-31", ErrorMessage = "Range de data invalida")]
        public DateTime DataNascimento { get; set; }
    }
}