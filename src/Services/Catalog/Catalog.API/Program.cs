var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);

    //adding pipeline behaviors from the building blocks lib
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();

builder.Services.AddMarten(config =>
{
    Console.WriteLine($"Using connection string: {builder.Configuration.GetConnectionString("Database")}");
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options =>
{
});
app.Run();
