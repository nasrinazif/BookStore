﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Models
{
    class Category : Entity
    {
        public string Name { get; set; }
        /* EF Relations */
        public IEnumerable<Book> Books { get; set; }
    }
}