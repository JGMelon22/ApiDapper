namespace JoinsDapperDemo.Repositories;

public class TelefoneRepository : ITelefoneRepository
{
    private readonly IDbConnection _dbConnection;
    public TelefoneRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Telefone> GetTelefone(int id)
    {
        var getTelefone = @"SELECT *
                            FROM Telefones
                            WHERE IdTelefone = @id;";

        _dbConnection.Open();

        var result = await _dbConnection.QueryFirstOrDefaultAsync<Telefone>(getTelefone, new { Id = id });
        return result;
    }

    public async Task<List<Telefone>> GetTelefones()
    {
        var getTelefonesQuery = @"SELECT IdTelefone,
                                         TelefoneTexto,
                                         IdPessoa,
                                         REPLACE(REPLACE(Ativo, 1, 'True'), 0, 'False') AS 'Ativo'
                                  FROM Telefones;";

        _dbConnection.Open();

        var result = await _dbConnection.QueryAsync<Telefone>(getTelefonesQuery);
        return result.ToList();
    }

    public async Task UpdateTelefone(Telefone telefone)
    {
        var updateTelefoneQuery = @"UPDATE Telefones
                                    SET TelefoneTexto = @TelefoneTexto
                                    WHERE IdTelefone = @Id";

        _dbConnection.Open();

        await _dbConnection.ExecuteAsync(updateTelefoneQuery, new
        {
            TelefoneTexto = telefone.TelefoneTexto,
            Id = telefone.IdTelefone
        });
    }
}