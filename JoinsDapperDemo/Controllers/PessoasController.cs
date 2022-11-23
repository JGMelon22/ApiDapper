namespace JoinsDapperDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly IPessoaRepository _repository;
    public PessoasController(IPessoaRepository repository)
    {
        _repository = repository;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> GetAllPessoas()
    {
        var pessoas = await _repository.GetPessoas();

        return pessoas.Any()
        ? Ok(pessoas)
        : NoContent();
    }

    // GET By Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPessoa(int id)
    {
        var pessoa = await _repository.GetPessoa(id);
        return (pessoa != null)
        ? Ok(pessoa)
        : NotFound("Pessoa com Id informado não encontrada!");
    }

    // GET Inner Join Pessoas Detalhes
    [HttpGet("innerJoinDetalhes")]
    public async Task<IActionResult> GetAllPessoasInnerJoinDetalhes()
    {
        var pessoas = await _repository.GetPessoasInnerJoinDetalhes();

        return pessoas.Any()
        ? Ok(pessoas)
        : NoContent();
    }

    // Get Inner Join Pessoas Telefones
    [HttpGet("innerJoinTelefones")]
    public async Task<IActionResult> GetAllPessoasInnerJoinTelefones()
    {
        var pessoas = await _repository.GetPessoasInnerJoinTelefones();
        return pessoas.Any()
        ? Ok(pessoas)
        : NoContent();
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> AddPessoa(Pessoa pessoa)
    {
        var pessoaToAdd = await _repository.CreatePessoa(pessoa);
        return (pessoaToAdd != null)
            ? Ok(pessoaToAdd)
            : BadRequest();
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePessoa(int id)
    {
        await _repository.DeletePessoa(id);
        return Ok("Dados da pessoa deletados com exito!");
    }
}