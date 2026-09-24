var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("PermitirTodo");

app.UseDefaultFiles();
app.UseStaticFiles();

var productos = new List<object>
{
    new
    {
        id = 1,
        codigo = "P001",
        nombre = "Pollo a la Brasa",
        categoria = "Pollos",
        descripcion = "Pollo entero a la brasa acompañado de papas y ensalada.",
        precio = 45.00,
        stock = 20,
        imagen = "🍗",
        disponible = true
    },
    new
    {
        id = 2,
        codigo = "P002",
        nombre = "1/4 de Pollo",
        categoria = "Pollos",
        descripcion = "Cuarto de pollo a la brasa con papas y ensalada.",
        precio = 18.00,
        stock = 35,
        imagen = "🍗",
        disponible = true
    },
    new
    {
        id = 3,
        codigo = "P003",
        nombre = "1/2 Pollo",
        categoria = "Pollos",
        descripcion = "Medio pollo a la brasa con papas y ensalada.",
        precio = 32.00,
        stock = 25,
        imagen = "🍗",
        disponible = true
    },
    new
    {
        id = 4,
        codigo = "P004",
        nombre = "Papas Fritas",
        categoria = "Acompañamientos",
        descripcion = "Porción de papas fritas crocantes.",
        precio = 8.00,
        stock = 40,
        imagen = "🍟",
        disponible = true
    },
    new
    {
        id = 5,
        codigo = "P005",
        nombre = "Ensalada Familiar",
        categoria = "Acompañamientos",
        descripcion = "Ensalada fresca para compartir.",
        precio = 10.00,
        stock = 30,
        imagen = "🥗",
        disponible = true
    },
    new
    {
        id = 6,
        codigo = "P006",
        nombre = "Inca Kola 1.5L",
        categoria = "Bebidas",
        descripcion = "Gaseosa Inca Kola de 1.5 litros.",
        precio = 7.00,
        stock = 50,
        imagen = "🥤",
        disponible = true
    },
    new
    {
        id = 7,
        codigo = "P007",
        nombre = "Coca Cola 1.5L",
        categoria = "Bebidas",
        descripcion = "Gaseosa Coca Cola de 1.5 litros.",
        precio = 7.00,
        stock = 45,
        imagen = "🥤",
        disponible = true
    },
    new
    {
        id = 8,
        codigo = "P008",
        nombre = "Chicha Morada",
        categoria = "Bebidas",
        descripcion = "Jarra de chicha morada tradicional.",
        precio = 12.00,
        stock = 25,
        imagen = "🧃",
        disponible = true
    },
    new
    {
        id = 9,
        codigo = "P009",
        nombre = "Combo Familiar",
        categoria = "Combos",
        descripcion = "Pollo entero, papas, ensalada y gaseosa.",
        precio = 58.00,
        stock = 15,
        imagen = "🍗",
        disponible = true
    },
    new
    {
        id = 10,
        codigo = "P010",
        nombre = "Combo Personal",
        categoria = "Combos",
        descripcion = "1/4 de pollo, papas, ensalada y gaseosa.",
        precio = 23.00,
        stock = 20,
        imagen = "🍗",
        disponible = true
    }
};

// HOME
app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        mensaje = "API Pollería funcionando",
        version = "1.0",
        arquitectura = "MVC",
        orm = "Entity Framework Core",
        endpoints = new[]
        {
            "/api/productos",
            "/api/productos/{id}",
            "/api/categorias"
        }
    });
});

// GET - todos los productos
app.MapGet("/api/productos", () =>
{
    return Results.Ok(productos);
});

// GET - producto por ID
app.MapGet("/api/productos/{id:int}", (int id) =>
{
    var producto = productos.FirstOrDefault(p =>
        (int)p.GetType().GetProperty("id")!.GetValue(p)! == id);

    if (producto == null)
        return Results.NotFound(new
        {
            mensaje = "Producto no encontrado"
        });

    return Results.Ok(producto);
});

// GET - categorías
app.MapGet("/api/categorias", () =>
{
    var categorias = productos
        .Select(p => p.GetType().GetProperty("categoria")!.GetValue(p)!.ToString())
        .Distinct()
        .OrderBy(c => c);

    return Results.Ok(categorias);
});

var port = Environment.GetEnvironmentVariable("Port")??"10000";
app.Run($"http://0.0.0.0:{port}");
