using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Interfaces;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.BusinessLogic.Services;
using WrestlingTournamentSystem.DataAccess.Mappers;
using WrestlingTournamentSystem.DataAccess.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WrestlingTournamentSystemDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDatabase"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Add repositories
builder.Services.AddScoped< ITournamentsRepository, TournamentsRepository>();
builder.Services.AddScoped<ITournamentStatusRepository, TournamentStatusRepository>();

//Add services
builder.Services.AddScoped<ITournamentsService, TournamentsService>();


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
