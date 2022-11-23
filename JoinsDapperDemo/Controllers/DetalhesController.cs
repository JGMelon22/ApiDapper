namespace JoinsDapperDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetalhesController : ControllerBase
{
    private readonly IDetalheRepository _repository;
    public DetalhesController(IDetalheRepository repository)
    {
        _repository = repository;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> GetAllDetalhes()
    {
        var detalhes = await _repository.GetDetalhes();

        return detalhes.Any()
        ? Ok(detalhes)
        : NoContent();
    }

    // GET By Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetalhe(int id)
    {
        var detalhe = await _repository.GetDetalhe(id);
        return (detalhe != null)
        ? Ok(detalhe)
        : NotFound("Detalhe com Id informado não encontrado!");
    }

    // PUT
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDetalhe(int id, Detalhe detalhe)
    {
        if (id != detalhe.IdDetalhe)
            return BadRequest();

        await _repository.UpdateDetalhe(detalhe);
        return Ok("Detalhe atualizado com exito!");
    }
}