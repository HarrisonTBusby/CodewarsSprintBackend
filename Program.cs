using CodewarsSprintBackend.Services;
using CodewarsSprintBackend.Services.Context;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<ReservationService>();

var connectionString = builder.Configuration.GetConnectionString("CodewarsBackendString");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddCors(options => {
    options.AddPolicy("CodewarsBackendPolicy", 
    builder => {
        builder.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3002", "https://codewarsreservationapp.azurewebsites.net")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CodewarsBackendPolicy");


app.UseAuthorization();

app.MapControllers();

app.Run();
