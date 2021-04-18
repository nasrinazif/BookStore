using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Interfaces
{
    interface ICategoryRepository: IRepository<Category>
    {
    }
}
