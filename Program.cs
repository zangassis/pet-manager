using Microsoft.EntityFrameworkCore;
using PetManager.Data;
using PetManager.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<PetDbContext>();
builder.Services.AddSingleton<PetRepository>();
builder.Services.Configure<ConnectionString>(builder.Configuration.GetSection("ConnectionStrings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/pets", (PetDbContext db) =>
{
    var pets = db.Pets;
    var owners = db.Owner;

    foreach (var petItem in pets)
    {
        var owner = owners.SingleOrDefault(o => o.Id == petItem.OwnerId);
        petItem.Owner = owner;
    }
    return Results.Ok(pets);
});

app.MapGet("/pets/viadapper", async (PetRepository db) =>
{
    var pets = await db.GetAllPets();
    foreach (var pet in pets)
        pet.Owner = await db.GetOwner(pet.OwnerId);

    return Results.Ok(pets);
});

app.MapGet("/pets/{id}", (Guid id, PetDbContext db) =>
{
    var pets = db.Pets;
    var pet = pets.SingleOrDefault(p => p.Id == id);

    if (pet == null)
        return Results.NotFound();

    var owners = db.Owner;
    var owner = owners.SingleOrDefault(o => o.Id == pet.OwnerId);
    pet.Owner = owner;

    return Results.Ok(pet);
});

app.MapPost("/pets", (PetDbContext db, Pet pet) =>
{
    var pets = db.Pets;
    db.Pets.Add(pet);
    db.SaveChanges();
    return Results.Created($"/pets/{pet.Id}", pet);
});

app.MapPut("/pets/{id}", (Guid id, Pet pet, PetDbContext db) =>
{
    db.Entry(pet).State = EntityState.Modified;
    db.Entry(pet.Owner).State = EntityState.Modified;
    db.SaveChanges();
    return Results.Ok(pet);
});

app.MapDelete("/pets/{id}", (Guid id, PetDbContext db) =>
{
    var pets = db.Pets;
    var petEntity = db.Pets.SingleOrDefault(p => p.Id == id);
    if (petEntity == null)
        return Results.NotFound();

    var owners = db.Owner;
    var owner = owners.SingleOrDefault(o => o.Id == petEntity.OwnerId);
    owners.Remove(owner);

    pets.Remove(petEntity);
    db.SaveChanges();
    return Results.NoContent();
});

app.UseHttpsRedirection();
app.Run();