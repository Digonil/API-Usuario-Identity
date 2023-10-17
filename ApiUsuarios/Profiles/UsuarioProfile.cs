using ApiUsuarios.Data.Dtos;
using ApiUsuarios.Models;
using AutoMapper;

namespace ApiUsuarios.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<CreateUsuarioDto, Usuario>();    
    }
}
