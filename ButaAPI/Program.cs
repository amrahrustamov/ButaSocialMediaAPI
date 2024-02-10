using ButaAPI.Database;
using ButaAPI.Exceptions;
using ButaAPI.Services.Abstracts;
using ButaAPI.Services.Concretes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(
    options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services
    .AddDbContext<ButaDbContext>(o =>
    {
        o.UseNpgsql(builder.Configuration.GetConnectionString("ButaDbContext"), b => b.MigrationsAssembly("ButaAPI"));
    })
    .AddScoped<IUserService, UserServices>()
    .AddScoped<AuthExceptions>()
    .AddHttpContextAccessor();

var app = builder.Build();

app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();