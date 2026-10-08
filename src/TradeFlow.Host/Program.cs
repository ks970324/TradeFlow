using TradeFlow.Adapter.Api;
using TradeFlow.Adapter.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Driving Adapter: REST API Controllers
builder.Services.AddControllers()
    .AddApplicationPart(typeof(ApiServiceRegistration).Assembly);
builder.Services.AddOpenApi();
builder.Services.AddApiAdapter();

// Driven Adapter: EF Core + SQL Server
builder.Services.AddPersistenceAdapter(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
