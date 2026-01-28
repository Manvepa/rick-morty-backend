using Microsoft.EntityFrameworkCore;
using rick_morty_backend.Data;
using rick_morty_backend.Services;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURACIÓN DE CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// SERVICIOS
builder.Services.AddControllers();
builder.Services.AddOpenApi();

Console.WriteLine(
    ">>> Connection string: " +
    builder.Configuration.GetConnectionString("DefaultConnection")
);


// REGISTRO DEL DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 45))
    );
});

// HttpClient y Seed
builder.Services.AddHttpClient();
builder.Services.AddScoped<CharacterSeedService>();

// Servicio externo Rick & Morty (si lo usas en controllers)
builder.Services.AddTransient<RickMortyService>();

var app = builder.Build();

// CORS
app.UseCors("AllowAngularApp");

// PIPELINE
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// SEED AUTOMÁTICO
using (var scope = app.Services.CreateScope())
{
    var seedService = scope.ServiceProvider
        .GetRequiredService<CharacterSeedService>();

    await seedService.SeedIfEmptyAsync();
}

app.Run();
