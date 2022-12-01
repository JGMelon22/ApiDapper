namespace JoinsDapperDemo.Models;

public class Pessoa
{
    public int IdPessoa { get; set; }
    public string Nome { get; set; } = string.Empty!;
    public List<Detalhe>? Detalhe { get; set; }
    public List<Telefone>? Telefone { get; set; }  // Going MacGyver - DO NOT USE IN REAL WORLD SCENARIO
}