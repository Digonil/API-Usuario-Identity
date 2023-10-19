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
    private UsuarioService _usuarioService;

    public UsuarioController(UsuarioService cadastroService)
    {
        _usuarioService = cadastroService;
    }


    [HttpPost("cadastro")]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
    {
        await _usuarioService.Cadastro(dto);

        return Ok(dto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUsuarioDto dto)
    {
       var token =  await _usuarioService.Login(dto);
        return Ok(token);
    }

}
