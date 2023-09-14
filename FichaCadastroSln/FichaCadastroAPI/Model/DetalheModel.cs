using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FichaCadastroAPI.Base;
using FichaCadastroAPI.Enumerators;

namespace FichaCadastroAPI.Model
{
    [Table("Detalhe")]
    public class DetalheModel : RelacionalBase
    {
        [Column(TypeName = "VARCHAR"), Required, StringLength(500)]
        public string Feedback { get; set; }
        [Required]
        public NotasEnum Nota { get; set; }
        [Required]
        public bool Situacao { get; set; }

        [Required]
        public FichaModel Ficha { get; set; }
    }
}
