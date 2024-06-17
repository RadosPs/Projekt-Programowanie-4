using Biblioteka.Model;
using Biblioteka.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Biblioteka.Service
{
    public class CheckOutService
    {
        private readonly LibraryContext _context;

        public CheckOutService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<List<CheckOut>> GetAllCheckOutsAsync()
        {
            return await _context.CheckOuts.Include(co => co.Book).ToListAsync();
        }

        public async Task<CheckOut> GetCheckOutByIdAsync(int id)
        {
            return await _context.CheckOuts.Include(co => co.Book).FirstOrDefaultAsync(co => co.Id == id);
        }

        public async Task AddCheckOutAsync(CheckOut checkOut)
        {
            _context.CheckOuts.Add(checkOut);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCheckOutAsync(CheckOut checkOut)
        {
            _context.Entry(checkOut).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCheckOutAsync(int id)
        {
            var checkOut = await _context.CheckOuts.FindAsync(id);
            if (checkOut != null)
            {
                _context.CheckOuts.Remove(checkOut);
                await _context.SaveChangesAsync();
            }
        }
    }
}
