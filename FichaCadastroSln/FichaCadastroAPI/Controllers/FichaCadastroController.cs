using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FichaCadastroAPI.DTO.Ficha;
using FichaCadastroAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace FichaCadastroAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FichaCadastroController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly FichaCadastroContextDB _fichaCadastroContextDB;

        public FichaCadastroController(IMapper mapper, FichaCadastroContextDB fichaCadastroContextDB)
        {
            _mapper = mapper;
            _fichaCadastroContextDB = fichaCadastroContextDB;
        }

        [HttpPost]
        public ActionResult Post([FromBody] FichaCreateDTO fichaCreateDTO)
        {
            FichaModel fichaModel = _mapper.Map<FichaModel>(fichaCreateDTO);

            _fichaCadastroContextDB.FichaModels.Add(fichaModel);
            _fichaCadastroContextDB.SaveChanges();

            return Ok();
        }
    }
}