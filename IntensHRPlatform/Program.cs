using IntensHRPlatform.Application.Interfaces;
using IntensHRPlatform.Application.Mappings;
using IntensHRPlatform.Application.Services;
using IntensHRPlatform.Infrastructure.Data;
using IntensHRPlatform.Infrastructure.Repositories;
using IntensHRPlatform.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repos
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();

//Services
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<ISkillService, SkillService>();

//mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//connection to db
builder.Services.AddDbContext<AppDbContext>(options => 
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
