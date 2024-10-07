using PosFiapTech1.Application.Interfaces;
using PosFiapTech1.Domain.Entities;
using PosFiapTech1.Domain.Interfaces;

namespace PosFiapTech1.Application.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly ILogger<ContatoService> _logger;

        public ContatoService(IContatoRepository contatoRepository, ILogger<ContatoService> logger)
        {
            _contatoRepository = contatoRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Contato>> GetAllContatosAsync()
        {
            return await _contatoRepository.GetAllAsync();
        }

        public async Task<Contato> GetContatoByIdAsync(int id)
        {
            return await _contatoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Contato>> ObterContatosPorDDDAsync(string ddd)
        {
            return await _contatoRepository.GetByDDDAsync(ddd);
        }

        public async Task AdicionarContatoAsync(Contato contato)
        {
            try
            {
                await _contatoRepository.AddAsync(contato);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao adicionar contato: {Message}", ex.Message);
                throw;
            }
        }

        public async Task AtualizarContatoAsync(Contato contato)
        {
            try
            {
                await _contatoRepository.UpdateAsync(contato);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao atualizar contato: {Message}", ex.Message);
                throw;
            }
        }

        public async Task DeletarContatoAsync(int id)
        {
            await _contatoRepository.DeleteAsync(id);
        }
    }
}
