using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FichaCadastroAPI.Base;

namespace FichaCadastroAPI.Model
{
    [Table("Ficha")]
    public class FichaModel : RelacionalBase
    {
        [Column(TypeName = "VARCHAR"), StringLength(250), Required]
        public string Nome { get; set; }
        [Column(TypeName = "VARCHAR"), StringLength(100), Required]
        public string  Email { get; set; }
        public DateTime DataNascimento { get; set; }

        public Collection<DetalheModel> DetalheModels { get; set; }
    }
}