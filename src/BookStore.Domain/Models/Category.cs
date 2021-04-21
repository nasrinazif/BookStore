﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Models
{
    public class Category : Entity
    {
        
        public string Name { get; set; }

        public string CatDescription { get; set; }
        /* EF Relations */
        public IEnumerable<Book> Books { get; set; }
    }
}
