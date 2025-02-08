using Blazored.Toast;
using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Components;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Servicio para la notificacion de error
builder.Services.AddBlazoredToast();

//INYECCION DEL CONTEXTO

//obtenemos el ConStr para usarlocon el contexto
var ConSrt = builder.Configuration.GetConnectionString("SqlConStr");
//Agregar contexto al builder con ConStr
builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConSrt));

//Inyeccion del service
builder.Services.AddScoped<TecnicosService>();
builder.Services.AddScoped<ClientesService>();
builder.Services.AddScoped<TicketsService>();
builder.Services.AddScoped<CiudadesService>();
builder.Services.AddScoped<SistemasService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
