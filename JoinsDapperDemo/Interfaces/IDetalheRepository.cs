namespace JoinsDapperDemo.Interfaces;

public interface IDetalheRepository
{
    Task<List<Detalhe>> GetDetalhes();
    Task<Detalhe> GetDetalhe(int id);
    Task UpdateDetalhe(Detalhe detalhe);
}