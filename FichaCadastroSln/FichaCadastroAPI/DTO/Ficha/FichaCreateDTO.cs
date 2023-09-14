using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FichaCadastroAPI.DTO.Ficha
{
    public class FichaCreateDTO
    {
        public string NomeCompleto { get; set; }
        public string EmailInformado { get; set; }
        public DateTime DataDeNascimento { get; set; }
    }
}