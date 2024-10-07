using PosFiapTech1.Application.Interfaces;
using PosFiapTech1.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PosFiapTech1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly IContatoService _contatoService;

        public ContatosController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contato>>> Get()
        {
            var contatos = await _contatoService.GetAllContatosAsync();
            return Ok(contatos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contato>> Get(int id)
        {
            var contato = await _contatoService.GetContatoByIdAsync(id);
            if (contato == null)
            {
                return NotFound();
            }
            return Ok(contato);
        }

        [HttpGet("ddd/{ddd}")]
        public async Task<ActionResult<IEnumerable<Contato>>> GetByDDD(string ddd)
        {
            var contatos = await _contatoService.ObterContatosPorDDDAsync(ddd);
            if (!contatos.Any())
            {
                return NotFound();
            }
            return Ok(contatos);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Contato contato)
        {
            await _contatoService.AdicionarContatoAsync(contato);
            return CreatedAtAction(nameof(Get), new { id = contato.Id }, contato);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Contato contato)
        {
            if (id != contato.Id)
            {
                return BadRequest();
            }

            await _contatoService.AtualizarContatoAsync(contato);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _contatoService.DeletarContatoAsync(id);
            return NoContent();
        }
    }
}
