using Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Mapping;
using Application.Patients;
using Application.Extension;
using Microsoft.AspNetCore.Mvc;

[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt =>
{
	opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("PatientREST"));
});
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
