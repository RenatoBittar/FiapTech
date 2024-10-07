using Microsoft.EntityFrameworkCore;
using PosFiapTech1.Domain.Entities;
using PosFiapTech1.Domain.Interfaces;
using PosFiapTech1.Infrastructure.Data;

namespace PosFiapTech1.Infrastructure.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly ContatosContext _context;

        public ContatoRepository(ContatosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contato>> GetAllAsync()
        {
            return await _context.Contatos.ToListAsync();
        }

        public async Task<Contato> GetByIdAsync(int id)
        {
            return await _context.Contatos.FindAsync(id);
        }

        public async Task<IEnumerable<Contato>> GetByDDDAsync(string ddd)
        {
            return await _context.Contatos.Where(c => c.Ddd == ddd).ToListAsync();
        }

        public async Task AddAsync(Contato contato)
        {
            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contato contato)
        {
            _context.Entry(contato).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var contato = await _context.Contatos.FindAsync(id);
            if (contato != null)
            {
                _context.Contatos.Remove(contato);
                await _context.SaveChangesAsync();
            }
        }
    }
}
