using Saga.Choreography.Rockets;
using Saga.Choreography.Rockets.Rockets;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.MapPost("missions/new", async (NewMissionRequest request, IRocketService rocketService) =>
{
    await rocketService.BuildRocketAsync(request);
    return Results.Accepted();
});

app.UseHttpsRedirection();
app.Run();