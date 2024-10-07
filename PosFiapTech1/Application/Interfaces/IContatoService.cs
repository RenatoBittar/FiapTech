using PosFiapTech1.Domain.Entities;

namespace PosFiapTech1.Application.Interfaces
{
    public interface IContatoService
    {

        Task<IEnumerable<Contato>> GetAllContatosAsync();
        Task<Contato> GetContatoByIdAsync(int id);
        Task<IEnumerable<Contato>> ObterContatosPorDDDAsync(string ddd);
        Task AdicionarContatoAsync(Contato contato);
        Task AtualizarContatoAsync(Contato contato);
        Task DeletarContatoAsync(int id);
    }
}
