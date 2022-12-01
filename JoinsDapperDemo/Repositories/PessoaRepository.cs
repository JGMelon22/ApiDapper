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
            pessoa.Detalhe = new List<Detalhe>();

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
            // pessoa.Telefone = telefoneFunc;
            pessoa.Telefone = new List<Telefone>();

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

    public async Task<List<Pessoa>> GetPessoasLeftJoinTelefones()
    {
        var getPessoasLeftJoinTelefonesQuery = @"SELECT *
                                                 FROM Pessoas p
                                                 LEFT JOIN  Telefones t
                                                     ON p.IdPessoa = t.IdPessoa;";

        _dbConnection.Open();

        var lookup = new Dictionary<int, Pessoa>();

        var result = await _dbConnection.QueryAsync<Pessoa, Telefone, Pessoa>(getPessoasLeftJoinTelefonesQuery, (pessoaFunc, telefoneFunc) =>
        {
            Pessoa pessoa1;

            if (!lookup.TryGetValue(pessoaFunc.IdPessoa, out pessoa1))
            {
                pessoa1 = pessoaFunc;
                pessoa1.Telefone = new List<Telefone>();
                lookup.Add(pessoa1.IdPessoa, pessoa1);
            }

            pessoa1.Telefone.Add(telefoneFunc);

            return pessoa1;

        },

        splitOn: "IdTelefone");

        return result.ToList();
    }

    public async Task<List<Pessoa>> GetPessoasInnerJoinTelefonesDetalhes()
    {
        var getPessoasInnerJoinTelefonesInnerJoinDetalhesQuery = @"SELECT *
                                                                   FROM Pessoas p
                                                                   INNER JOIN Telefones t
                                                                       ON p.IdPessoa = t.IdPessoa
                                                                   INNER JOIN Detalhes d
                                                                       ON p.IdPessoa = d.IdPessoa;";
        _dbConnection.Open();

        var lookup = new Dictionary<int, Pessoa>();

        var result = await _dbConnection.QueryAsync<Pessoa, Telefone, Detalhe, Pessoa>(getPessoasInnerJoinTelefonesInnerJoinDetalhesQuery, (pessoaFunc, telefoneFunc, detalheFunc) =>
        {
            Pessoa pessoa1;

            if (!lookup.TryGetValue(pessoaFunc.IdPessoa, out pessoa1))
            {
                pessoa1 = pessoaFunc;
                pessoa1.Telefone = new List<Telefone>();
                pessoa1.Detalhe = new List<Detalhe>();
                lookup.Add(pessoa1.IdPessoa, pessoa1);
            }

            pessoa1.Telefone.Add(telefoneFunc);
            pessoa1.Detalhe.Add(detalheFunc);

            return pessoa1;
        });

        return result.Distinct().ToList();
    }
}