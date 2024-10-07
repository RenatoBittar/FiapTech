using PosFiapTech1.Domain.Entities;

namespace PosFiapTech1.Domain.Interfaces
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> GetAllAsync();
        Task<Contato> GetByIdAsync(int id);
        Task<IEnumerable<Contato>> GetByDDDAsync(string ddd);
        Task AddAsync(Contato contato);
        Task UpdateAsync(Contato contato);
        Task DeleteAsync(int id);
    }
}
