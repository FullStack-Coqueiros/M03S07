namespace FichaCadastroAPI.DTO.Ficha
{
    public class FichaDetalhesReadDTO : FichaReadDTO
    {
        public List<DetalheReadDTO>? Detalhes { get; set; }
    }

}
