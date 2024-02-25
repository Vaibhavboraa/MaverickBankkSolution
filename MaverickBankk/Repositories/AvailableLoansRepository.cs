using MaverickBankk.Context;
using MaverickBankk.Interfaces;
using MaverickBankk.Models;
using Microsoft.EntityFrameworkCore;

namespace MaverickBankk.Repositories
{
    public class AvailableLoansRepository : IRepository<int, AvailableLoans>
    {
        private readonly MavericksBankContext _context;

        public AvailableLoansRepository(MavericksBankContext context)
        {
            _context = context;
        }

        public async Task<AvailableLoans> Add(AvailableLoans item)
        {
            _context.AvailableLoans.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<AvailableLoans?> Delete(int key)
        {
            var loan = await _context.AvailableLoans.FindAsync(key);
            if (loan == null)
            {
                return null;
            }

            _context.AvailableLoans.Remove(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<AvailableLoans?> Get(int key)
        {
            return await _context.AvailableLoans.FindAsync(key);
        }

        public async Task<List<AvailableLoans>?> GetAll()
        {
            return await _context.AvailableLoans.ToListAsync();
        }

        public async Task<AvailableLoans> Update(AvailableLoans item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
