using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Ppr_Model.Entity;

namespace Ppr_Model.DBOperations
{
    public static class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return; // Veritabanı zaten verilerle doluysa, işlem yapmadan çık.
                }

                context.Books.AddRange(
                    new Book
                    {
                        Id = 1,
                        Title = "Lean Zort",
                        GenreId = 1,
                        PageCount = 100,
                        PublishDate = new DateTime(2000, 07, 17)
                    },
                    new Book
                    {
                        Id = 2,
                        Title = "Lean Kooko",
                        GenreId = 2,
                        PageCount = 190,
                        PublishDate = new DateTime(2010, 05, 22)
                    },
                    new Book
                    {
                        Id = 3,
                        Title = "Lean Dune",
                        GenreId = 3,
                        PageCount = 190,
                        PublishDate = new DateTime(1999, 12, 13)
                    }
                );

                context.SaveChanges(); // Veritabanı değişikliklerini kaydet.
            }
        }
    }
}
