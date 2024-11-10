using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Interfaces;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.BusinessLogic.Services;
using WrestlingTournamentSystem.DataAccess.Repositories;
using WrestlingTournamentSystem.BusinessLogic.Validation;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Helpers.Mappers;
using WrestlingTournamentSystem.DataAccess.Helpers.Settings;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WrestlingTournamentSystemDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDatabase"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Wrestling Tournament System API",
        Version = "v1",
        Description = "API for managing wrestling tournaments and participants",
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

//Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Add repositories
builder.Services.AddScoped< ITournamentRepository, TournamentRepository>();
builder.Services.AddScoped<ITournamentStatusRepository, TournamentStatusRepository>();
builder.Services.AddScoped<ITournamentWeightCategoryRepository, TournamentWeightCategoryRepository>();
builder.Services.AddScoped<IWeightCategoryRepository, WeightCategoryRepository>();
builder.Services.AddScoped<ITournamentWeightCategoryStatusRepository, TournamentWeightCategoryStatusRepository>();
builder.Services.AddScoped<IWrestlingStyleRepository, WrestlingStyleRepository>();
builder.Services.AddScoped<IWrestlerRepository, WrestlerRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

//Add services
builder.Services.AddScoped<ITournamentsService, TournamentsService>();
builder.Services.AddScoped<ITournamentWeightCategoryService, TournamentWeightCategoryService>();
builder.Services.AddScoped<IWrestlerService, WrestlerService>();
builder.Services.AddTransient<JwtTokenService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<DatabaseSeeder>();

//Add Validation
builder.Services.AddScoped<IValidationService, ValidationService>();

//Add Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<WrestlingTournamentSystemDbContext>()
    .AddDefaultTokenProviders();

//Add Authentication
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

    option.MapInboundClaims = false;
    option.TokenValidationParameters.ValidAudience = jwtSettings.Audience;
    option.TokenValidationParameters.ValidIssuer = jwtSettings.Issuer;
    option.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
});

//Add Authorization
builder.Services.AddAuthorization();

var app = builder.Build();

//Seed database
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<WrestlingTournamentSystemDbContext>();
var dbSeeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
await dbSeeder.SeedAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseAuthentication();

app.UseAuthorization();

app.Run();
