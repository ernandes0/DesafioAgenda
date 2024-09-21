using DesafioAgenda.API.Handlers;
using DesafioAgenda.Domain.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAgenda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CreateUserHandler _createUserHandler;
        private readonly LoginUserHandler _loginUserHandler;

        public AuthController(CreateUserHandler createUserHandler, LoginUserHandler loginUserHandler)
        {
            _createUserHandler = createUserHandler;
            _loginUserHandler = loginUserHandler;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            var result = await _createUserHandler.Handle(command);
            if (!result.Sucesso)
                return BadRequest(result.Mensagem);

            return Ok(result.Mensagem);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _loginUserHandler.Handle(command);
            if (!result.Sucesso)
                return BadRequest(result.Mensagem);

            return Ok(new { Token = result.Dados });
        }
    }
}