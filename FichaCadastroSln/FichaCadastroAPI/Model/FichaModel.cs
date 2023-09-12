using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FichaCadastroAPI.Model
{
    [Table("Ficha")]
    public class FichaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR"), StringLength(250), Required]
        public string Nome { get; set; }
        [Column(TypeName = "VARCHAR"), StringLength(100), Required]
        public string  Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}