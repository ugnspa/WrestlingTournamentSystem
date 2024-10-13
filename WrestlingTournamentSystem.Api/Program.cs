using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Interfaces;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.BusinessLogic.Services;
using WrestlingTournamentSystem.DataAccess.Mappers;
using WrestlingTournamentSystem.DataAccess.Repositories;
using WrestlingTournamentSystem.BusinessLogic.Validation;
using System.Reflection;


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

//Add services
builder.Services.AddScoped<ITournamentsService, TournamentsService>();
builder.Services.AddScoped<ITournamentWeightCategoryService, TournamentWeightCategoryService>();
builder.Services.AddScoped<IWrestlerService, WrestlerService>();

//Add Validation
builder.Services.AddScoped<IValidationService, ValidationService>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
