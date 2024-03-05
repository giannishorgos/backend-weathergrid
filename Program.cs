using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WeatherForecastAPI.Authorization;
using WeatherForecastAPI.Data;
using WeatherForecastAPI.Interfaces;
using WeatherForecastAPI.Repository;
using WeatherForecastAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var FrontendOrigin = "Frontend Origin";

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: FrontendOrigin,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200").WithMethods("GET", "POST", "DELETE").AllowAnyHeader();
            // .WithHeaders("Authorization");
        }
    );
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddScoped<IHttp, HttpService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<FavoriteLocationRepository>();
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

string domain = $"https://{builder.Configuration["Auth0:Domain"]}";
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = builder.Configuration["Auth0:Audience"];
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "read:messages",
        policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain))
    );
});

builder.Services.AddSingleton<AuthorizationHandler<HasScopeRequirement>, HasScopeHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(FrontendOrigin);

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
