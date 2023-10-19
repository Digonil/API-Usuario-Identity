using ApiUsuarios.Data.Dtos;
using ApiUsuarios.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.Services;

public class CadastroService
{
    private UserManager<Usuario> _userManager;
    private IMapper _mapper;

    public CadastroService(IMapper mapper, UserManager<Usuario> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task Cadastro(CreateUsuarioDto dto)
    {
        Usuario usuario = _mapper.Map<Usuario>(dto);
        var result = await _userManager.CreateAsync(usuario, dto.Password);

        if (!result.Succeeded)
        {
            throw new ApplicationException("Falha ao cadastrar usuário!");
        }
    }
}
