using System.Text.Json.Serialization;
using Domain.Ports;
using Domain.Ports.Mocks;
using Domain.UseCases;
using Domain.UseCases.Implementations;
using Infra.Resources.Hypermedia;


var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register repositories as singletons
var timeSlotRepo = new MockTimeSlots();
var reservationRepo = new MockReservations();

builder.Services.AddSingleton<ITimeSlots>(timeSlotRepo);
builder.Services.AddSingleton<IReservations>(reservationRepo);

// Register use cases
builder.Services.AddScoped<IBookTimeSlot, BookTimeSlot>();
builder.Services.AddScoped<ICancelBooking, CancelBooking>();

//LinkService
builder.Services.AddScoped<ILinkService, LinkService>();
builder.Services.AddHttpContextAccessor();

// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .WithExposedHeaders("Location");
    });
});

var app = builder.Build();

// Enable CORS - Important: Place this before other middleware
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();