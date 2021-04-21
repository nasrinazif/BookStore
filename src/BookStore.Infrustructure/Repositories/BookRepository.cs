using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Infrustructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrustructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(BookStoreDbContext db) : base(db)
        {
        }

        public override async Task<List<Book>> GetAll()
        {
            var q = from b in Db.Books
                    join c in Db.Categories on b.CategoryId equals c.Id into cs
                    from p in cs.DefaultIfEmpty()
                    select new { b.Id, b.Name, b.Author, b.Description, b.Value, b.PublishDate,};
            return await Db.Books.AsNoTracking().Include(b => b.Category)
                .OrderBy(b => b.Name)
                .ToListAsync();
        }
        public override async Task<Book> GetById(int id)
        {
            return await Db.Books.AsNoTracking().Include(b => b.Category)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<Book>> GetBooksByCategory(int categoryId)
        {
            return await Search(b => b.CategoryId == categoryId);
        }

        public async Task<IEnumerable<Book>> SearchBookWithCategory(string searchedValue)
        {
            return await Db.Books.AsNoTracking()
                .Include(b => b.Category)
                .Where(b => b.Name.Contains(searchedValue) ||
                            b.Author.Contains(searchedValue) ||
                            b.Description.Contains(searchedValue) ||
                            b.Category.Name.Contains(searchedValue))
                .ToListAsync();
        }
    }
}
