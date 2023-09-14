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
                .ForMember(destino => destino.Email, origem => origem.MapFrom(dados => dados.EmailInformado))
                .ForMember(destino => destino.DataNascimento, origem => origem.MapFrom(dados => dados.DataDeNascimento));
        }
    }
}