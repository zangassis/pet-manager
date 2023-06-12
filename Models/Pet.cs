namespace PetManager.Models;
public class Pet
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Species { get; set; }
    public string? Breed { get; set; }
    public int Age { get; set; }
    public string? Color { get; set; }
    public double Weight { get; set; }
    public bool Vaccinated { get; set; }
    public string? LastVaccinationDate { get; set; }
    public Owner? Owner { get; set; }
    public Guid OwnerId { get; set; }
}

public class Owner
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}
