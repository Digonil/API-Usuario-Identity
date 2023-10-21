using ApiUsuarios.Authorization;
using ApiUsuarios.Data;
using ApiUsuarios.Models;
using ApiUsuarios.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration["ConnectionStrings:UsuarioConnection"];

// Add services to the container.

//Adding DbContext (In this case is a IdentityDbContext).
builder.Services.AddDbContext<UsuarioDbContext>(opts =>
{
    opts.UseMySql(connString,
        ServerVersion.AutoDetect(connString));
}
);

//Adiciona o Identity que será realizado sobre o usuário e que terá um papel no sistema gerenciado pelo IdentityRole.
builder.Services.AddIdentity<Usuario, IdentityRole>()//Adiciona o Identity que será realizado sobre o usuário e que terá um papel no sistema gerenciado pelo IdentityRole.
    .AddEntityFrameworkStores<UsuarioDbContext>()//Adiciona a conexão com o entity que irá salvar os usuarios
    .AddDefaultTokenProviders();//adiciona o provisionamento dos tokens para autenticação.


//adiciona o Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Adiciona os Services (Scoped, pois utilizara a mesma instância em toda a aplicação), criando suas instâncias.
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();

//Injetando as dependecias para autorização
builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();


//Adding the controllers
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding Authentication (Irá validar o token e indica que será realizado via JWT Token)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

//Adding the authorization service
builder.Services.AddAuthorization(options=>
{
    options.AddPolicy("IdadeMinima", policy =>
    {
        policy.AddRequirements(new IdadeMinima(18));
    });
});

//******************************************************************************************************************************************************************************//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

//Mappping the controllers
app.MapControllers();

app.Run();
