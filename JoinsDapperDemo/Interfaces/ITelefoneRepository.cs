namespace JoinsDapperDemo.Interfaces;

public interface ITelefoneRepository
{
    Task<List<Telefone>> GetTelefones();
    Task<Telefone> GetTelefone(int id);
    Task UpdateTelefone(Telefone telefone);
}