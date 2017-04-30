using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BookService : IBooks
    {
        BooksEntities _c = new BooksEntities();
        public List<Book> GetBooks()
        {
            return _c.Books.ToList();
        }
    }
}
