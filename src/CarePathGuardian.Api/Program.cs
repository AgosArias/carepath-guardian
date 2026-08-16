using CarePathGuardian.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using CarePathGuardian.Infrastructure.Persistence.Repositories;
using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Application.Patients.CreatePatient;
using CarePathGuardian.Application.Patients.GetPatientById;
using Microsoft.AspNetCore.Mvc;
using CarePathGuardian.Domain.Patients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<CarePathGuardianDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));
		
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<CreatePatientHandler>();
builder.Services.AddScoped<GetPatientByIdHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapPost("/patients", async (
	[FromBody] CreatePatientCommand command,
	[FromServices] CreatePatientHandler handler) =>
{
	var patient = await handler.Handle(command);
    return Results.Created($"/patients/{patient.Id}",patient);
});

app.MapGet("patients/{id:guid}", async (
	Guid id,
	[FromServices] GetPatientByIdHandler handler) =>
{
	var query = new GetPatientByIdQuery(id);
	var patient = await handler.Handle(query);
	return patient is null? Results.NotFound(): Results.Ok(patient);
});


app.Run();


