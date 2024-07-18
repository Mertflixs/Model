using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ppr_Model.DBOperations;
using Microsoft.AspNetCore.Mvc;
using Ppr_Model.Entity;
using Ppr_Model.Common;

namespace Ppr_Model.BookOperations.DeleteBook
{
    public class DeleteBook
    {
        private readonly BookStoreDbContext _dbContext;
        public int bookId { get; set; }

        public DeleteBook(BookStoreDbContext dbContext, int id)
        {
            _dbContext = dbContext;
            bookId = id;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == bookId);
            if (book is null)
            {
                throw new InvalidOperationException("Kitap mevcut değil");
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}