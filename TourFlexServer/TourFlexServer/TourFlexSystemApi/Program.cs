using Serilog;
using TourFlexSystem.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, services, config) =>
{
    config.ReadFrom.Configuration(context.Configuration)
          .ReadFrom.Services(services);
});

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddHealthChecks();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO Update this to limit api access 
builder.Services.AddCors(
    options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });

builder.Services.AddHttpClients(builder.Configuration)
    .AddGrokRateLimit(builder.Configuration);
builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseRateLimiter();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
