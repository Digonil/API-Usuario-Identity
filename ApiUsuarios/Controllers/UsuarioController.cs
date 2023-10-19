using ApiUsuarios.Data;
using ApiUsuarios.Data.Dtos;
using ApiUsuarios.Models;
using ApiUsuarios.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{ 
    private CadastroService _cadastroService;

    public UsuarioController(CadastroService cadastroService)
    {
        _cadastroService = cadastroService;
    }


    [HttpPost]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
    {
        await _cadastroService.Cadastro(dto);

        return Ok(dto);
    }

}
