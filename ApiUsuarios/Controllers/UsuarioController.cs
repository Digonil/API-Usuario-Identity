using ApiUsuarios.Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{
    [HttpPost]
    public IActionResult CadastraUsuario(CreateUsuarioDto dto)
    {
        throw new NotImplementedException();
    }

}
