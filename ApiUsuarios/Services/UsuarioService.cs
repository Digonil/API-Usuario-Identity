using ApiUsuarios.Data.Dtos;
using ApiUsuarios.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.Services;

public class UsuarioService
{
    private UserManager<Usuario> _userManager;
    private IMapper _mapper;
    private SignInManager<Usuario> _signInManager;
    private TokenService _tokenService;

    public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
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

    public async Task<string> Login(LoginUsuarioDto dto)
    {
       var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);//Realiza o login pelo método do SignInManager

        if (!result.Succeeded)
        {
            throw new ApplicationException("Usuário não autenticado");
        }

        var usuario = _signInManager.UserManager.Users.FirstOrDefault(x => x.NormalizedUserName == dto.UserName.ToUpper());//Utiliza um método do signinmanager para buscar o nome do usuario.

        var token = _tokenService.GenerateToken(usuario);//Gera o token com o método do Service.

        return token;
    }
}
