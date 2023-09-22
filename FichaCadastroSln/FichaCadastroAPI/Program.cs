using FichaCadastroAPI.HealthCheck;
using FichaCadastroAPI.Model;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
       .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var variavelAmbiente = builder.Environment.EnvironmentName;
var diretorio = Directory.GetCurrentDirectory();

builder.Configuration
       .SetBasePath(diretorio)
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.{variavelAmbiente}.json", optional: false, reloadOnChange: true);


//string connectionString = "Server=localhost;Database=FichaCadastro;Trusted_Connection=True;TrustServerCertificate=True;";
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;


builder.Services
       .AddDbContext<FichaCadastroContextDB>(options => 
                                             options.UseSqlServer(connectionString));

//ConfigurationMapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddRouting(options => 
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

string nomeHealthCheckCustom = nameof(HealthCheckCustom);

builder.Services
       .AddHealthChecks()
       .AddCheck<HealthCheckCustom>(nomeHealthCheckCustom);
       //.AddDbContextCheck<FichaCadastroContextDB>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHealthChecks("/api/healthcheck", new HealthCheckOptions() 
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.Run();
