var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/",() =>
{
    return "API Polleria funcionando";
});

app.MapGet("/api/polleria",() =>
{
    return Results.Ok(new[]
    {
        new{
            id=1,
            codigo="P001",
            nombre="Pollo a la brasa",
        },
        new{
            id=2,
            codigo="P002",
            nombre="Pollo broaster",
        }
    });
});


app.Run();