using FichaCadastroAPI.Enumerators;

namespace FichaCadastroAPI.DTO.Ficha
{
    public class DetalheReadDTO 
    {

        public int Id { get; set; }
        public string Feedback { get; set; }
        public NotasEnum Nota { get; set; }
    }

}
