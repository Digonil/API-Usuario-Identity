using ApiUsuarios.Data;
using ApiUsuarios.Data.Dtos;
using ApiUsuarios.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{
    private UserManager<Usuario> _userManager;
    private IMapper _mapper;

    public UsuarioController(IMapper mapper, UserManager<Usuario> userManager)
    {

        _mapper = mapper;
        _userManager = userManager;
    }


    [HttpPost]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
    {
        Usuario usuario = _mapper.Map<Usuario>(dto);
        var result = await _userManager.CreateAsync(usuario, dto.Password);

        if (result.Succeeded)
        {
            return Ok("Usuário cadastrado!");
        }

        throw new ApplicationException("Falha ao cadastrar usuário!");
    }

}
