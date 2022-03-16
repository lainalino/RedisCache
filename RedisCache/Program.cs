using RedisCache.Migrations;
using Microsoft.EntityFrameworkCore;
using RedisCache.Services.Interfaces;
using RedisCache.Services;
using RedisCache.Repositories.Interfaces;
using RedisCache.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SQLDBConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("AzureRedisConnection");
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSwaggerGen();

#region Injeção de Dependência
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<IPessoaService, PessoaService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSwagger();
app.UseSwaggerUI(options =>
{
#if DEBUG
    // For Debug in Kestrel
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
#else
      options.SwaggerEndpoint("/mysite/swagger/v1/swagger.json", "Web API V1");
#endif
    options.RoutePrefix = string.Empty;
});

app.Run();
