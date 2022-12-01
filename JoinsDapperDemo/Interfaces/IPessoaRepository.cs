namespace JoinsDapperDemo.Interfaces;

public interface IPessoaRepository
{
    Task<List<Pessoa>> GetPessoas();
    Task<Pessoa> GetPessoa(int id);
    Task<List<Pessoa>> GetPessoasInnerJoinDetalhes();
    Task<List<Pessoa>> GetPessoasInnerJoinTelefones();
    Task<List<Pessoa>> GetPessoasLeftJoinTelefones();
    Task<List<Pessoa>> GetPessoasInnerJoinTelefonesDetalhes();
    Task<Pessoa> CreatePessoa(Pessoa pessoa);
    Task DeletePessoa(int id);
}