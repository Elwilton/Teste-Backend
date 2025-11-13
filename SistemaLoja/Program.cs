using Microsoft.EntityFrameworkCore;
using SistemaLoja.Application.Services;
using SistemaLoja.Domain.Interfaces;
using SistemaLoja.Infrastructure.Data;
using SistemaLoja.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Banco de dados
builder.Services.AddDbContext<LojaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de dependências
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<PedidoService>();

// MVC + Razor Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Migrações automáticas
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LojaDbContext>();
    dbContext.Database.Migrate();
}

// Middlewares
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage(); // mostra erros detalhados no ambiente de dev
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Roteamento MVC (único necessário)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
