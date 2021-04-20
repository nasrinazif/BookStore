﻿using BookStore.Domain.Interfaces;
using BookStore.Domain.Services;
using BookStore.Infrustructure.Context;
using BookStore.Infrustructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<BookStoreDbContext>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBookService, BookService>();

            return services;
        }
    }
}
