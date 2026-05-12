using FullStackDemo.WebApi.Mappings;
using FullStackDemo.WebApi.Configurations;
using FullStackDemo.Infrastructure.Persistence.Data.Dapper;

var builder = WebApplication.CreateBuilder(args);

// Configure log4net
builder.ConfigureLogging();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddCors(options =>
{
    var serverHost = builder.Configuration["ReactWebApp:ServerHost"];
        options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins(serverHost) // Allow both HTTP and HTTPS
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials());
});

// Configure database
//DbContext
builder.Services.ConfigureDatabase(builder.Configuration);
//Dapper
builder.Services.AddScoped<IGundamDb, GundamDb>();

// Add JWT authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

// Register repositories
builder.Services.AddRepositories();

// Register application services
builder.Services.AddApplicationServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed the database
await app.SeedDatabaseAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Only use HTTPS redirection in non-Docker environments
if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();