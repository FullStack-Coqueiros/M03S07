using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FichaCadastroAPI.DTO.Ficha;
using FichaCadastroAPI.Model;

namespace FichaCadastroAPI.AutoMapper
{
    public class ConfigurationMapper : Profile
    {
        public ConfigurationMapper()
        {
            //Origem .... Destino
            CreateMap<FichaCreateDTO, FichaModel>()
                .ForMember(destino => destino.Nome, origem => origem.MapFrom(dados => dados.NomeCompleto))
                .ForMember(destino => destino.Email, origem => origem.MapFrom(dados => dados.EmailInformado.ToLower()))
                .ForMember(destino => destino.DataNascimento, origem => origem.MapFrom(dados => dados.DataDeNascimento));

            //Origem .... Destino
            CreateMap<FichaModel, FichaReadDTO>();

            //Origem .... Destino
            CreateMap<FichaUpdateDTO, FichaModel>();

            //Origem .... Destino
            CreateMap<FichaModel, FichaDetalhesReadDTO>()
                .ForMember(destino => destino.ContatenacaoNomeEmail,
                           origem => origem.MapFrom(dados => $"{dados.Nome} - {dados.Email}"))
                .ForMember(destino => destino.Detalhes, origem => origem.MapFrom(dados => dados.DetalheModels));
            
            //Origem .... Destino
            CreateMap<DetalheModel, DetalheReadDTO>();
        }
    }
}