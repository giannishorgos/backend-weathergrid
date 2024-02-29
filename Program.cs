using WeatherForecastAPI.Services;
using WeatherForecastAPI.Data;
using WeatherForecastAPI.Interfaces;
using WeatherForecastAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var FrontendOrigin = "Frontend Origin";
builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: FrontendOrigin,
                policy => 
                {
                  policy.WithOrigins("http://localhost:4200")
                     .WithMethods("GET");
                });
        });
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddScoped<IHttp, HttpService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDBContext>(options => 
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(FrontendOrigin);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
