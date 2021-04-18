using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Infrustructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Infrustructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BookStoreDbContext db) : base(db)
        {
        }
    }
}
