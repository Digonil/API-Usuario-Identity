using Microsoft.AspNetCore.Identity;

namespace ApiUsuarios.Models;

public class Usuario : IdentityUser
{
    public DateTime BirthDate { get; set; }

    public Usuario() : base()
    {
       
    }
}
