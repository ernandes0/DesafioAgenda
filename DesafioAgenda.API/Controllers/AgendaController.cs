using DesafioAgenda.Infra.Filters;
using DesafioAgenda.API.Handlers;
using DesafioAgenda.Domain.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DesafioAgenda.Interface.IServices;
using DesafioAgenda.Domain.Commands;
using DesafioAgenda.Domain.Models;

namespace DesafioAgenda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(ValidationFilter))]
    public class AgendaController : ControllerBase
    {
        private readonly CreateContatoHandler _createContatoHandler;
        private readonly UpdateContatoHandler _updateContatoHandler;
        private readonly DeleteContatoHandler _deleteContatoHandler;
        private readonly GetContatoByIdHandler _getContatoByIdHandler;
        private readonly GetAllContatosHandler _getAllContatosHandler;
        private readonly IAuthenticationService _authenticationService;

        public AgendaController(
            CreateContatoHandler createContatoHandler,
            UpdateContatoHandler updateContatoHandler,
            DeleteContatoHandler deleteContatoHandler,
            GetContatoByIdHandler getContatoByIdHandler,
            GetAllContatosHandler getAllContatosHandler,
            IAuthenticationService authenticationService
        )
        {
            _createContatoHandler = createContatoHandler;
            _updateContatoHandler = updateContatoHandler;
            _deleteContatoHandler = deleteContatoHandler;
            _getContatoByIdHandler = getContatoByIdHandler;
            _getAllContatosHandler = getAllContatosHandler;
            _authenticationService = authenticationService;
        }

        // rota para criar um contato
        [HttpPost]
        public async Task<IActionResult> CreateContato([FromBody] CreateContatoCommand command)
        {
            var result = await _createContatoHandler.Handle(command);

            if (!result.Sucesso)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetContatoById), new { id = result.Dados.Id }, result);
        }

        // rota para atualizar um contato
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContato(int id, [FromBody] UpdateContatoCommand command)
        {
            var result = await _updateContatoHandler.Handle(command, id);

            if (!result.Sucesso)
                return BadRequest(result);

            return Ok(result);
        }

        // rota para desativar um contato
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContato(int id)
        {
            var result = await _deleteContatoHandler.Handle(new DeleteContatoCommand { Id = id });

            if (!result.Sucesso)
                return BadRequest(result);

            return Ok(result);
        }

        // rota para buscar um contato pelo id
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<AgendaModel>>> GetContatoById(int id)
        {
            var result = await _getContatoByIdHandler.Handle(new GetContatoByIdQuery { Id = id });

            if (!result.Sucesso)
                return BadRequest(result);

            return Ok(result);
        }

        // rota para buscar todos os contatos ativos
        [HttpGet("ativos")]
        public async Task<ActionResult<ServiceResponse<List<AgendaModel>>>> GetActiveContatos()
        {
            var userId = _authenticationService.GetUserId();
            var result = await _getAllContatosHandler.HandleGetActiveContatos(userId);

            if (!result.Sucesso)
                return BadRequest(result);

            return Ok(result);
        }

        // rota para buscar todos os contatos inativos
        [HttpGet("inativos")]
        public async Task<ActionResult<ServiceResponse<List<AgendaModel>>>> GetInactiveContatos()
        {
            var userId = _authenticationService.GetUserId();
            var result = await _getAllContatosHandler.HandleGetInactiveContatos(userId);

            if (!result.Sucesso)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
