using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FichaCadastroAPI.Model
{
    [Table("Detalhe")]
    public class DetalheModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR"), Required, StringLength(500)]
        public string Feedback { get; set; }
        [Required]
        public int Nota { get; set; }
        [Required]
        public bool Situacao { get; set; }
    }
}
