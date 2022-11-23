namespace JoinsDapperDemo.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly IDbConnection _dbConnection;
    public PessoaRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<List<Pessoa>> GetPessoas()
    {
        var getPessoasQuery = @"SELECT *
                                FROM Pessoas;";

        _dbConnection.Open();
        var result = await _dbConnection.QueryAsync<Pessoa>(getPessoasQuery);

        return result.ToList();
    }

    public async Task<Pessoa> GetPessoa(int id)
    {
        var getPessoaQuery = @"SELECT *
                               FROM Pessoas 
                               WHERE IdPessoa = @Id;";

        _dbConnection.Open();

        var result = await _dbConnection.QueryFirstOrDefaultAsync<Pessoa>(getPessoaQuery, new { Id = id });
        return result;
    }

    public async Task<List<Pessoa>> GetPessoasInnerJoinDetalhes()
    {
        var getPessoasInnerJoinDetalhesQuery = @"SELECT * 
                                                 FROM Pessoas p 
                                                 INNER JOIN Detalhes d
                                                    ON p.IdPessoa = d.IdPessoa;";
        _dbConnection.Open();

        var result = await _dbConnection.QueryAsync<Pessoa, Detalhe, Pessoa>(getPessoasInnerJoinDetalhesQuery, (pessoaFunc, detalheFunc) =>
        {
            Pessoa pessoa = pessoaFunc;
            pessoa.Detalhe = detalheFunc;

            return pessoa;

        },

        splitOn: "IdDetalhe");

        return result.ToList();
    }

    public async Task<List<Pessoa>> GetPessoasInnerJoinTelefones()
    {
        var getPessoasInnerJoinTelefonesQuery = @"SELECT *
                                                  FROM Pessoas p
                                                  INNER JOIN Telefones t
                                                      ON p.IdPessoa = t.IdPessoa;";

        _dbConnection.Open();

        var result = await _dbConnection.QueryAsync<Pessoa, Telefone, Pessoa>(getPessoasInnerJoinTelefonesQuery, (pessoaFunc, telefoneFunc) =>
        {
            Pessoa pessoa = pessoaFunc;
            pessoa.Telefone = telefoneFunc;

            return pessoa;
        },

        splitOn: "IdTelefone");

        return result.ToList();
    }

    public async Task<Pessoa> CreatePessoa(Pessoa pessoa)
    {
        var createPessoaQuery = @"INSERT INTO Pessoas(Nome)
                                  VALUES(@Nome);";

        _dbConnection.Open();

        await _dbConnection.ExecuteAsync(createPessoaQuery, new
        {
            Nome = pessoa.Nome
        });

        return pessoa;
    }

    public async Task DeletePessoa(int id)
    {
        var deletePessoaQuery = @"DELETE 
                                  FROM Telefones
                                  WHERE IdTelefone = @id
                                  AND IdPessoa = @id;

                                  DELETE 
                                  FROM Detalhes
                                  WHERE IdDetalhe = @id
                                  AND IdPessoa = @id;
                              
                                  DELETE 
                                  FROM Pessoas
                                  WHERE IdPessoa = @id;";

        _dbConnection.Open();

        await _dbConnection.ExecuteAsync(deletePessoaQuery, new { Id = id });
    }
}