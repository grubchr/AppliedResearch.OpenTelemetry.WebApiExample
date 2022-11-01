using Chr.Common.OpenTelemetry.Exporter;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetryTracing(tracerBuilder =>
{
  var tracerOptions = new OtlpTracerProviderOptions()
  {
    SourceNames = new[] {"AppliedResearch.OpenTelemetry.WebApiExample"},
    TracerProviderExporter = ConfiguredExporters.ConsoleExporter,
    ServiceName = "AppliedResearch.OpenTelemetry.WebApiExample",
    ServiceVersion = "1.0.0"
  };

  tracerBuilder
    .AddConsoleTracerProvider()
    .SetActivitySources(tracerOptions)
    .AddAttributesAndService(tracerOptions)
    .AddAspNetCoreInstrumentation();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();