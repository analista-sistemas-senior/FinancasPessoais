using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using FinancasPessoais.Infra.Data.Repositories;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FinancasPessoaisDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(options => { options.DefaultScheme = "Auth"; }).AddCookie("Auth", options => {
        options.LoginPath = "/Autenticacao/Login";
        options.AccessDeniedPath = "/Autenticacao/AcessoNegado";
        options.SlidingExpiration = true;
    });

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ICarteiraRepository, CarteiraRepository>();
builder.Services.AddScoped<IIndiceFinanceiroRepository, IndiceFinanceiroRepository>();
builder.Services.AddScoped<IIndexadorRepository, IndexadorRepository>();
builder.Services.AddScoped<IDespesaTipoRepository, DespesaTipoRepository>();
builder.Services.AddScoped<IDespesaFonteRepository, DespesaFonteRepository>();
builder.Services.AddScoped<IDespesaRepository, DespesaRepository>();
builder.Services.AddScoped<IReceitaTipoRepository, ReceitaTipoRepository>();
builder.Services.AddScoped<IReceitaFonteRepository, ReceitaFonteRepository>();
builder.Services.AddScoped<IReceitaRepository, ReceitaRepository>();
builder.Services.AddScoped<IEmissorRepository, EmissorRepository>();
builder.Services.AddScoped<IInvestimentoTipoRepository, InvestimentoTipoRepository>();
builder.Services.AddScoped<IInvestimentoRepository, InvestimentoRepository>();
builder.Services.AddScoped<IInvestimentoHistoricoRepository, InvestimentoHistoricoRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICarteiraService, CarteiraService>();
builder.Services.AddScoped<IIndiceFinanceiroService, IndiceFinanceiroService>();
builder.Services.AddScoped<IIndexadorService, IndexadorService>();
builder.Services.AddScoped<IDespesaTipoService, DespesaTipoService>();
builder.Services.AddScoped<IDespesaFonteService, DespesaFonteService>();
builder.Services.AddScoped<IDespesaService, DespesaService>();
builder.Services.AddScoped<IReceitaTipoService, ReceitaTipoService>();
builder.Services.AddScoped<IReceitaFonteService, ReceitaFonteService>();
builder.Services.AddScoped<IReceitaService, ReceitaService>();
builder.Services.AddScoped<IEmissorService, EmissorService>();
builder.Services.AddScoped<IInvestimentoTipoService, InvestimentoTipoService>();
builder.Services.AddScoped<IInvestimentoService, InvestimentoService>();
builder.Services.AddScoped<IInvestimentoHistoricoService, InvestimentoHistoricoService>();
builder.Services.AddScoped<IRelatorioService, RelatorioService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

var supportedCultures = new[] { "pt-BR" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0]).AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.MapStaticAssets();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}").WithStaticAssets();

app.Run();