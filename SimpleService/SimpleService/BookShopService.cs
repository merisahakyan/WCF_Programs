using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleService
{
    public class BookShopService : IBookShop
    {
        BooksEntities _context = new BooksEntities();
        public List<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        public List<Book> GetBookByID(string id)
        {
            int i = int.Parse(id);
            List<Book> list = new List<Book>();
            foreach (var m in _context.Books)
            {
                if (m.ID==i)
                    list.Add(m);
            }
            return list;
        }
        public List<Book> GetBooksByAuthor(string author)
        {
            List<Book> list = new List<Book>();
            foreach (var m in _context.Books)
            {
                if (m.Author.ToLower().Contains(author.ToLower()))
                    list.Add(m);
            }
            return list;
        }

        public List<Book> GetBooksByGenre(string genre)
        {
            List<Book> list = new List<Book>();
            foreach (var m in _context.Books)
            {
                if (m.Genre.ToLower().Contains(genre.ToLower()))
                    list.Add(m);
            }
            return list;
        }

        public List<Book> GetBooksByName(string name)
        {
            List<Book> list = new List<Book>();
            foreach (var m in _context.Books)
            {
                if (m.Name.ToLower().Contains(name.ToLower()))
                    list.Add(m);
            }
            return list;
        }

        public List<Book> GetBooksByPrice(string price)
        {
            int p = int.Parse(price);
            List<Book> list = new List<Book>();
            foreach (var m in _context.Books)
            {
                if (m.Price <= p)
                    list.Add(m);
            }
            return list;
        }

        public List<Book> Search(string value)
        {
            List<Book> list = new List<Book>();
            foreach (var m in _context.Books)
            {
                if (m.Author.ToLower().Contains(value.ToLower()) 
                    || m.Name.ToLower().Contains(value.ToLower())
                    || m.Description.ToLower().Contains(value.ToLower()))
                    list.Add(m);
            }
            return list;
        }
    }
}
