using ApiUsuarios.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiUsuarios.Services;

public class TokenService
{
    private IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(Usuario usuario)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("username", usuario.UserName),
            new Claim("id", usuario.Id),
            new Claim(ClaimTypes.DateOfBirth, usuario.BirthDate.ToString()),
            new Claim("loginTimeStamp", DateTime.UtcNow.ToString())
        };

        //Gerar o token
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));//Criado a chave aleatória

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //Gera uma credencial com uma chave e codifica com o algoritmo especificado.

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddMinutes(10),
            claims: claims,
            signingCredentials: signingCredentials);//Na criação do token, especifica-se coisas como o tempo de expiraçãao, define que será usados os claims e os signing credencials)

        return new JwtSecurityTokenHandler().WriteToken(token);//Pega o token e escreve uma cadeia de caracteres, retornando uma string
    }
}
