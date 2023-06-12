using Microsoft.EntityFrameworkCore;
using PetManager.Models;
namespace PetManager.Data;

public class PetDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
       options.UseSqlite("DataSource=petManagerDb; Cache=Shared");

    public DbSet<Pet> Pets { get; set; }
    public DbSet<Owner> Owner { get; set; }
}
