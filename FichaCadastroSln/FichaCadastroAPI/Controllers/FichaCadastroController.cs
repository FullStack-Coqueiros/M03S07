using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FichaCadastroAPI.DTO.Ficha;
using FichaCadastroAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FichaCadastroAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FichaCadastroController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly FichaCadastroContextDB _fichaCadastroContextDB;
        private readonly ILogger<FichaCadastroController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FichaCadastroController(IMapper mapper,
                                       FichaCadastroContextDB fichaCadastroContextDB, 
                                       ILogger<FichaCadastroController> logger,
                                       IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _fichaCadastroContextDB = fichaCadastroContextDB;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FichaReadDTO> Post([FromBody] FichaCreateDTO fichaCreateDTO)
        {
            try
            {

                _logger.LogCritical($"Dados {fichaCreateDTO.ToString()}");
                var ip = _httpContextAccessor
                                    .HttpContext!
                                    .Connection!
                                    .RemoteIpAddress!
                                    .ToString();

                _logger.LogWarning($"IP Requisição {ip}");

                bool existeEmailInformado = _fichaCadastroContextDB
                                            .FichaModels
                                            .ToList()
                                            .Exists(exists => exists.Email == fichaCreateDTO.EmailInformado);

                if (existeEmailInformado)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "E-mail j� cadastrado");
                }

                FichaModel fichaModel = _mapper.Map<FichaModel>(fichaCreateDTO);

                _fichaCadastroContextDB.FichaModels.Add(fichaModel);
                _fichaCadastroContextDB.SaveChanges();

                FichaReadDTO fichaReadDTO = _mapper.Map<FichaReadDTO>(fichaModel);
                
                return StatusCode(HttpStatusCode.Created.GetHashCode(), fichaReadDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FichaReadDTO> Put([FromRoute] int id, [FromBody] FichaUpdateDTO fichaUpdateDTO)
        {
            try
            {
                FichaModel? fichaModel = _fichaCadastroContextDB
                                                                .FichaModels
                                                                .Find(id);

                if (fichaModel == null)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "N�o pode alterar um registro que n�o exista na base de dados");
                }

                fichaModel = _mapper.Map(fichaUpdateDTO, fichaModel);

                _fichaCadastroContextDB.FichaModels.Update(fichaModel);
                _fichaCadastroContextDB.SaveChanges();

                FichaReadDTO fichaReadDTO = _mapper.Map<FichaReadDTO>(fichaModel);

                return StatusCode(HttpStatusCode.OK.GetHashCode(), fichaReadDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                FichaModel? fichaModel = _fichaCadastroContextDB
                                                    .FichaModels
                                                    .Include(i => i.DetalheModels)
                                                    .Where(w => w.Id == id)
                                                    .FirstOrDefault();

                if (fichaModel == null)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "N�o existe dados a ser exclu�do");
                }

                if (fichaModel.DetalheModels != null && fichaModel.DetalheModels.Count > 0)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "A ficha cont�m detalhes");
                }

                _fichaCadastroContextDB.FichaModels.Remove(fichaModel);
                _fichaCadastroContextDB.SaveChanges();

                return StatusCode(HttpStatusCode.OK.GetHashCode());
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<FichaDetalhesReadDTO>> Get([FromQuery] string? email)
        {
            try
            {
                List<FichaModel>? fichasModel;

                if (email == null || email == "")
                {
                    fichasModel = _fichaCadastroContextDB
                                    .FichaModels
                                    .Include(i => i.DetalheModels)
                                    .ToList();
                }
                else 
                {
                    fichasModel = _fichaCadastroContextDB
                                                    .FichaModels
                                                    .Include(i => i.DetalheModels)
                                                    .Where(w => w.Email.Contains(email.ToLower()))
                                                    .ToList();
                }

                IEnumerable<FichaDetalhesReadDTO> fichasReadDTO = _mapper.Map<IEnumerable<FichaDetalhesReadDTO>>(fichasModel);

                return StatusCode(HttpStatusCode.OK.GetHashCode(), fichasReadDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FichaDetalhesReadDTO> Get([FromRoute] int id)
        {
            try
            {
                if (id == 0)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Id está igual a zero");
                }

                FichaModel?  fichaModel = _fichaCadastroContextDB
                                                    .FichaModels
                                                    .Include(i => i.DetalheModels)
                                                    .Where(w => w.Id == id)
                                                    .FirstOrDefault();


                FichaDetalhesReadDTO fichaReadDTO = _mapper.Map<FichaDetalhesReadDTO>(fichaModel);

                return StatusCode(HttpStatusCode.OK.GetHashCode(), fichaReadDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex);
            }
        }



    }
}