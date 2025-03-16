using OrderingCompare.Domain.Interfaces;
using OrderingCompare.Domain.Services;
using Serilog;
using Serilog.Sinks.Elasticsearch;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<INumbersGeneratorService, NumbersGeneratorService>();

builder.Host.UseSerilog((context, config) =>
{
    config.Enrich.FromLogContext()
          .Enrich.WithProcessId()
          .Enrich.WithThreadId()
          .Enrich.WithEnvironmentName()
          .WriteTo.Console()
          .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
          {
              IndexFormat = "dotnet-logs-{0:yyyy.MM.dd}",
              AutoRegisterTemplate = true,
              OverwriteTemplate = true,
              ModifyConnectionSettings = conn => conn.BasicAuthentication("elastic", "gFo0Vj4+-AvytNLgqlbf")
          });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
