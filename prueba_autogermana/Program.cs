using Autogermana.Api.Middleware;
using Autogermana.Application.Interfaces;
using Autogermana.Application.Services;
using Autogermana.Infrastructure.Options;
using Autogermana.Infrastructure.Repository;
using Autogermana.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.Configure<PAOptions>(builder.Configuration.GetSection("PowerAutomate"));
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("OAuth"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<ICustomerService, CustomerService>();


builder.Services.AddDataProtection();
builder.Services.AddSingleton<IProtectionSecret, ProtectionSecret>();


builder.Services.AddHttpClient<ITokenService, TokenService>(http =>
{
    http.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddHttpClient<IPowerAutomateRepository, PowerAutomateRepository>(http =>
{
    http.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddTransient<Exceptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<Exceptions>();

app.UseAuthorization();

app.MapControllers();

app.Run();
