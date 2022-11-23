namespace JoinsDapperDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TelefonesController : ControllerBase
{
    private readonly ITelefoneRepository _repository;
    public TelefonesController(ITelefoneRepository repository)
    {
        _repository = repository;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> GetAllTelefones()
    {
        var telefones = await _repository.GetTelefones();

        return telefones.Any()
        ? Ok(telefones)
        : NoContent();
    }

    // Get By Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTelefone(int id)
    {
        var telefone = await _repository.GetTelefone(id);
        return (telefone != null)
            ? Ok(telefone)
            : NotFound("Telefone com Id informado não encontrado!");
    }

    // PATCH
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateTelefone(int id, Telefone telefone)
    {
        if (id != telefone.IdTelefone)
            return BadRequest();

        await _repository.UpdateTelefone(telefone);

        return Ok("Texto Telefone atualizado com exito!");
    }

}