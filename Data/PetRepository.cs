using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using PetManager.Models;
using System.Data;

namespace PetManager.Data;
public class PetRepository
{
    private readonly IDbConnection _dbConnection;

    public PetRepository(IOptions<ConnectionString> connectionString)
    {
        _dbConnection = new MySqlConnection(connectionString.Value.ProjectConnection);
    }

    public async Task<List<PetDto>> GetAllPets()
    {
        using (_dbConnection)
        {
            string query = "SELECT * FROM pet";

            var pets = await _dbConnection.QueryAsync<PetDto>(query);
            return pets.ToList();
        }
    }

    public async Task<OwnerDto> GetOwner(string ownerId)
    {
        using (_dbConnection)
        {
            string query = "select * from owner where id = @OwnerId";

            var owner = await _dbConnection.QueryAsync<OwnerDto>(query, new { OwnerId = ownerId });

            return owner.SingleOrDefault();
        }
    }

}
