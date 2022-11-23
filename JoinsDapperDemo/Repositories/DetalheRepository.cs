namespace JoinsDapperDemo.Repositories;

public class DetalheRepository : IDetalheRepository
{
    private readonly IDbConnection _dbConnection;
    public DetalheRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<List<Detalhe>> GetDetalhes()
    {
        var getDetalhesQuery = @"SELECT *
                                 FROM Detalhes;";
        _dbConnection.Open();

        var result = await _dbConnection.QueryAsync<Detalhe>(getDetalhesQuery);
        return result.ToList();
    }

    public async Task<Detalhe> GetDetalhe(int id)
    {
        var getDetalheQuery = @"SELECT *
                                FROM Detalhes 
                                WHERE IdDetalhe = @id";

        _dbConnection.Open();

        var result = await _dbConnection.QueryFirstOrDefaultAsync<Detalhe>(getDetalheQuery, new { Id = id });
        return result;
    }

    public async Task UpdateDetalhe(Detalhe detalhe)
    {
        var updateDetalheQuery = @"UPDATE Detalhes
                                   SET DetalheTexto = @DetalheTexto
                                   WHERE IdDetalhe = @Id;";

        _dbConnection.Open();

        await _dbConnection.ExecuteAsync(updateDetalheQuery, new
        {
            DetalheTexto = detalhe.DetalheTexto,
            Id = detalhe.IdDetalhe
        });
    }
}