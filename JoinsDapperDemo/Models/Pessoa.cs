namespace JoinsDapperDemo.Models;

public class Pessoa
{
    public int IdPessoa { get; set; }
    public string Nome { get; set; } = string.Empty!;
    public Detalhe? Detalhe { get; set; }
    public Telefone? Telefone { get; set; }  // Going MacGyver - DO NOT USE IN REAL WORLD SCENARIO
}