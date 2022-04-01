using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Shop.Models;
using Shop.Repositories;
using Shop.Services;

namespace Shop.Controllers
{
    [Route("home/account")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model) 
        {
            var user = UserRepository.Get(model.UserName,model.Password);

            if (user == null)
                return NotFound(new {message = "Usuário ou senha inválidos"});

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user.UserName,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => string.Format("Authenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("maneger")]
        [Authorize(Roles = "Manager")]
        public string Manager() => "Gerente";

    }
}
