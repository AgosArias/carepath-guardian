using CarePathGuardian.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using CarePathGuardian.Infrastructure.Persistence.Repositories;
using CarePathGuardian.Application.Abstractions.Persistence;
using CarePathGuardian.Application.Patients.CreatePatient;
using CarePathGuardian.Application.Patients.GetPatientById;
using CarePathGuardian.Application.Referrals.CreateReferral;
using CarePathGuardian.Application.Referrals.GetReferralById;
using CarePathGuardian.Application.Appointments.CreateAppointment;
using CarePathGuardian.Application.Appointments.GetAppointmentById;
using CarePathGuardian.Application.DataQualityIssues.GetDataQualityIssueById;
using CarePathGuardian.Application.DataQualityIssues.CreateDataQualityIssue;
using CarePathGuardian.Application.DataQualityIssues.EvaluateReferralDataQuality;
using CarePathGuardian.Application.DataQualityIssues.GetAllDataQualityIssues;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using CarePathGuardian.Domain.DataQualityIssues;
using CarePathGuardian.Application.DataQualityIssues.ResolveDataQualityIssue;

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

builder.Services.AddScoped<IReferralRepository, ReferralRepository>();
builder.Services.AddScoped<CreateReferralHandle>();
builder.Services.AddScoped<GetReferralByIdHandler>();

builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<CreateAppointmentHandler>();
builder.Services.AddScoped<GetAppointmentByIdHandler>();

builder.Services.AddScoped<IDataQualityIssueRepository, DataQualityIssueRepository>();
builder.Services.AddScoped<CreateDataQualityIssueHandler>();
builder.Services.AddScoped<GetDataQualityIssueByIdHandler>();
builder.Services.AddScoped<GetAllDataQualityIssuesHandler>();
builder.Services.AddScoped<ResolveDataQualityIssueHandler>();

builder.Services.AddScoped<EvaluateReferralDataQualityHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
	app.MapScalarApiReference();
}

app.UseHttpsRedirection();


app.MapPost("/patients", async (
	[FromBody] CreatePatientCommand command,
	[FromServices] CreatePatientHandler handler) =>
{
	var patient = await handler.Handle(command);
    return Results.Created($"/patients/{patient.Id}",patient);
});

app.MapGet("/patients/{id:guid}", async (
	Guid id,
	[FromServices] GetPatientByIdHandler handler) =>
{
	var query = new GetPatientByIdQuery(id);
	var patient = await handler.Handle(query);
	return patient is null? Results.NotFound(): Results.Ok(patient);
});

app.MapPost("/referrals", async(
	[FromBody] CreateReferralCommand command,
	[FromServices] CreateReferralHandle handler) =>
{
	var referral = await handler.Handle(command);
	return Results.Created($"/referrals/{referral.Id}", referral);
});

app.MapGet("/referrals/{id:guid}", async(
	Guid id, 
	[FromServices] GetReferralByIdHandler handler) =>
{
	var query = new GetReferralByIdQuery(id);
	var referral = await handler.Handle(query);
	return referral is null? Results.NotFound(): Results.Ok(referral);
});

app.MapPost("/appointments", async(
	[FromBody] CreateAppointmentCommand command,
	[FromServices] CreateAppointmentHandler handler) =>
{
	var appointment = await handler.Handle(command);
	return Results.Created($"/appointments/{appointment.Id}", appointment);
});

app.MapGet("/appointments/{id:guid}", async(
	Guid id, 
	[FromServices] GetAppointmentByIdHandler handler) =>
{
	var query = new GetAppointmentByIdQuery(id);
	var appointments = await handler.Handle(query);
	return appointments is null? Results.NotFound(): Results.Ok(appointments);
});

app.MapPost("/data-quality-issues", async(
	[FromBody] CreateDataQualityIssueCommand command,
	[FromServices] CreateDataQualityIssueHandler handler) =>
{
	var issue = await handler.Handle(command);
	return Results.Created($"/data-quality-issues/{issue.Id}", issue);
});

app.MapGet("/data-quality-issues", async(
	IssueStatus? status,
	IssueSeverity? severity,
	[FromServices] GetAllDataQualityIssuesHandler handler) =>
{
	var query = new GetAllDataQualityIssuesQuery(status, severity);
	var issue = await handler.Handle(query);
	return Results.Ok(issue);
});

app.MapGet("/data-quality-issues/{id:guid}", async(
	Guid id, 
	[FromServices] GetDataQualityIssueByIdHandler handler) =>
{
	var query = new GetDataQualityIssueByIdQuery(id);
	var issue = await handler.Handle(query);
	return issue is null? Results.NotFound(): Results.Ok(issue);
});

app.MapPatch("/data-quality-issues/{id:guid}/resolve", async(
	Guid id,
	[FromBody] ResolveDataQualityIssueCommand command,
	[FromServices] ResolveDataQualityIssueHandler handler) =>
	{
		var issue = await handler.Handle(command with {Id = id});

		return issue is null? Results.NotFound(): Results.Ok(issue);
});

app.Run();


