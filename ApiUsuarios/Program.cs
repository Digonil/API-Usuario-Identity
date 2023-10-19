using ApiUsuarios.Data;
using ApiUsuarios.Models;
using ApiUsuarios.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("UsuarioConnection");

// Add services to the container.

//Adding DbContext (In this case is a IdentityDbContext).
builder.Services.AddDbContext<UsuarioDbContext>(opts =>
{
    opts.UseMySql(connString,
        ServerVersion.AutoDetect(connString));
}
);

//Adiciona o Identity que ser� realizado sobre o usu�rio e que ter� um papel no sistema gerenciado pelo IdentityRole.
builder.Services.AddIdentity<Usuario, IdentityRole>()//Adiciona o Identity que ser� realizado sobre o usu�rio e que ter� um papel no sistema gerenciado pelo IdentityRole.
    .AddEntityFrameworkStores<UsuarioDbContext>()//Adiciona a conex�o com o entity que ir� salvar os usuarios
    .AddDefaultTokenProviders();//adiciona o provisionamento dos tokens para autentica��o.


//adiciona o Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Adiciona os Services (Scoped, pois utilizara a mesma inst�ncia em toda a aplica��o), criando suas inst�ncias.
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();


//Adding the controllers
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Mappping the controllers
app.MapControllers();

app.Run();
