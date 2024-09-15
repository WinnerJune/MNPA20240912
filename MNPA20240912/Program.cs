var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

//crear una lista para almacenar objetos de tipo Categoria
var categorys = new List<Category>();

//Configurar una ruta GET para obtener todos las categorias
app.MapGet("/categorys", () =>
{
    return categorys; //Devuelve la lista de categoria
});

//Configurar una rua GET para obtener una categoria especifica por su ID
app.MapGet("/categorys/{id}", (int id) =>
{
    var category = categorys.FirstOrDefault(c => c.Id == id);
    return category;//devuelve la categoria encontrada (o null si no se encuentra)
});

//configurar una ruta POST para agregar una nuev categoria a la lista
app.MapPost("/categorys", (Category category) =>
{
    categorys.Add(category);//agregar la nueva categoria a la lista
    return Results.Ok();//devuelve una respuesta HTTP 200 OK
});

//configurar una ruta PUT para actualizar una categoria existente por su ID
app.MapPut("/categorys/{id}", (int id, Category category) =>
{
    //buscar una categoria en la lista que tenga el ID especificado
    var existingCategory = categorys.FirstOrDefault(c => c.Id == id);
    if (existingCategory != null)
    {
        //actualiza los datos dela categoria existente con los datos proporcionados
        existingCategory.Name = category.Name;
        existingCategory.LastName = category.LastName;
        return Results.Ok(); //devuelve un respuesta HTTP 200 OK
    }
    else
    {
        return Results.NotFound(); //devuelve una respuesta HTTP 404 Not Found si la categoria no existe
    }
});

//configurar uan ruta DELETE para eliminar una categoria por su ID
app.MapDelete("/categorys/{id}", (int id) =>
{
    //busca una categoria en la lista que tenga el ID especifico
    var existingCategory = categorys.FirstOrDefault(c => c.Id == id);
    if (existingCategory != null)
    {
        //elimina la categoria de la lista
        categorys.Remove(existingCategory);
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

//EJECUTAR LA APLICACION
app.Run();

//Definicion de la clase Categoria que representa la estructura de un cliente
internal class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
}