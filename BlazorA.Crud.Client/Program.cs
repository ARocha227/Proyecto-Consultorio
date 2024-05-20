using Blazor1.Crud.Client.Services;
using BlazorA.Crud.Client;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorA.Crud.Client.Extensiones;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5194") });


builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AutenticacionExtension>();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IProfesionalService, ProfesionalService>();
builder.Services.AddScoped<IDiagnosticoService, DiagnosticoService>();

builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
