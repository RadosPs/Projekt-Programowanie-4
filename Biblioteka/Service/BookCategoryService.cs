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
    public class BookCategoryService
    {
        private readonly LibraryContext _context;

        public BookCategoryService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<List<BookCategory>> GetAllCategoriesAsync()
        {
            return await _context.BookCategories.ToListAsync();
        }

        public async Task<BookCategory> GetCategoryByIdAsync(int id)
        {
            return await _context.BookCategories.FindAsync(id);
        }

        public async Task AddCategoryAsync(BookCategory category)
        {
            _context.BookCategories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(BookCategory category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.BookCategories.FindAsync(id);
            if (category != null)
            {
                _context.BookCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
