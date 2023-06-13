namespace PetManager.Models;
public class PetDto
{
   public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Species { get; set; }
    public string? Breed { get; set; }
    public int Age { get; set; }
    public string? Color { get; set; }
    public double Weight { get; set; }
    public bool Vaccinated { get; set; }
    public string? LastVaccinationDate { get; set; }
    public string? OwnerId { get; set; }
    public OwnerDto? Owner { get; set;}
}

public class OwnerDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}