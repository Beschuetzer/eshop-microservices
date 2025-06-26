var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(config =>
{
    Console.WriteLine($"Using connection string: {builder.Configuration.GetConnectionString("Database")}");
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
var app = builder.Build();
app.MapCarter();
app.Run();
