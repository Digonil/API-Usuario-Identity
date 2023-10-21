using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ApiUsuarios.Authorization;

public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
    {
        var dateBirthClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);//Busco a claim do usuário que possui o dateBirth

        if (dateBirthClaim == null) //Faço a validação (necessário configuração para deixar de dar null)
        {
            return Task.CompletedTask;
        }

        var dateBirth = Convert.ToDateTime(dateBirthClaim.Value);//Converto a informação recebida para um datetime.

        var idadeUsuario = DateTime.Today.Year - dateBirth.Year;

        if(dateBirth > DateTime.Today.AddYears(-idadeUsuario))
        {
            idadeUsuario--;
        }

        if(idadeUsuario > requirement.Idade)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

