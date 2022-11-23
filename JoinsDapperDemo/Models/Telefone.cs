namespace JoinsDapperDemo.Models;

public class Telefone
{
    public int IdTelefone { get; set; }
    public string TelefoneTexto { get; set; } = string.Empty!;
    public int IdPessoa { get; set; }
    public bool Ativo { get; set; }
}