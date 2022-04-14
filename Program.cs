using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Orders;

var builder = WebApplication.CreateBuilder(args);

// added for using DateTime.Now
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var configuartion = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .Build();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var projectDirectory = AppContext.BaseDirectory;
    var projectName = Assembly.GetExecutingAssembly().GetName().Name;
    var xmlFileName = $"{projectName}.xml";
    
    options.IncludeXmlComments(Path.Combine(projectDirectory,xmlFileName));
});
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    var connectionString = configuartion.GetConnectionString("PostgreSQL");
    options.UseNpgsql(connectionString);
});

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

app.Run();
