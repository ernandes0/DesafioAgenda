using DesafioAgenda.API.Commands;
using DesafioAgenda.API.Common;
using DesafioAgenda.API.Filters;
using DesafioAgenda.API.Handlers;
using DesafioAgenda.API.Models;
using DesafioAgenda.API.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAgenda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ValidationFilter))]
    public class AgendaController : ControllerBase
    {
        private readonly CreateContatoHandler _createContatoHandler;
        private readonly UpdateContatoHandler _updateContatoHandler;
        private readonly DeleteContatoHandler _deleteContatoHandler;
        private readonly GetContatoByIdHandler _getContatoByIdHandler;
        private readonly GetAllContatosHandler _getAllContatosHandler;

        public AgendaController(
            CreateContatoHandler createContatoHandler,
            UpdateContatoHandler updateContatoHandler,
            DeleteContatoHandler deleteContatoHandler,
            GetContatoByIdHandler getContatoByIdHandler,
            GetAllContatosHandler getAllContatosHandler)
        {
            _createContatoHandler = createContatoHandler;
            _updateContatoHandler = updateContatoHandler;
            _deleteContatoHandler = deleteContatoHandler;
            _getContatoByIdHandler = getContatoByIdHandler;
            _getAllContatosHandler = getAllContatosHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContato([FromBody] CreateContatoCommand command)
        {
            var result = await _createContatoHandler.Handle(command);

            if (!result.Sucesso)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetContatoById), new { id = result.Dados.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContato(int id, [FromBody] UpdateContatoCommand command)
        {
            var result = await _updateContatoHandler.Handle(command, id);

            if (!result.Sucesso)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContato(int id)
        {
            var result = await _deleteContatoHandler.Handle(new DeleteContatoCommand { Id = id });

            if (!result.Sucesso)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<AgendaModel>>> GetContatoById(int id)
        {
            var result = await _getContatoByIdHandler.Handle(new GetContatoByIdQuery { Id = id });

            if (!result.Sucesso)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<AgendaModel>>>> GetAllContatos()
        {
            var result = await _getAllContatosHandler.Handle(new GetAllContatosQuery());

            if (!result.Sucesso)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
