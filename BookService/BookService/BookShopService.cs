using System.Collections.Generic;
using System.Linq;

namespace BookService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class BookShopService : IBookShop, IOnCard
    {
        BooksEntities _entities = new BooksEntities();

        public BookShopService()
        {

        }

        public void AddToCard(string id)
        {
            id = id.Trim('i');
            int i = int.Parse(id);
            foreach (var b in _entities.Books)
            {
                if (b.ID == i && b.Quantity > 0)
                {
                    if (b.OnCard > 0)
                    {
                        b.OnCard++;
                        b.Quantity--;
                    }
                    else
                    {
                        b.OnCard = 1;
                        b.Quantity--;
                    }
                }
            }
            _entities.SaveChanges();
        }


        public Book GetBookByID(string id)
        {
            id = id.Trim('i');
            int i = int.Parse(id);
            Book book = null;
            foreach (var m in _entities.Books)
            {
                if (m.ID == i)
                {
                    book = m;
                    break;
                }
            }
            return book;
        }

        public List<Book> GetBooks()
        {
            return _entities.Books.ToList();
        }

        public List<Book> GetBooksByAuthor(string author)
        {
            List<Book> list = new List<Book>();
            foreach (var m in _entities.Books)
            {
                if (m.Author.ToLower().Contains(author.ToLower()))
                    list.Add(m);
            }
            return list;
        }

        public List<Book> GetBooksByGenre(string genre)
        {
            List<Book> list = new List<Book>();
            foreach (var m in _entities.Books)
            {
                if (m.Genre.ToLower().Contains(genre.ToLower()))
                    list.Add(m);
            }
            return list;
        }

        public List<Book> GetBooksByName(string name)
        {
            List<Book> list = new List<Book>();
            foreach (var m in _entities.Books)
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
            foreach (var m in _entities.Books)
            {
                if (m.Price <= p)
                    list.Add(m);
            }
            return list;
        }

        public Book GetOnCardBookByID(string id)
        {
            id = id.Trim('i');
            int i = int.Parse(id);
            Book book = null;
            foreach (var m in _entities.Books)
            {
                if (m.ID == i && m.OnCard > 0)
                {
                    book = m;
                    break;
                }
            }
            return book;
        }

        public List<Book> GetOnCardBooks()
        {
            //var q = from b in _entities.Books
            //        where b.OnCard > 0
            //        select b;
            //return q.ToList();
            return _entities.Books.Where((b) => (b.OnCard > 0)).Select((s) => (s)).ToList();
        }

        public void RemoveFromCard(string id)
        {
            int i = int.Parse(id.Trim('i'));
            foreach (var  b in _entities.Books)
            {
                if (b.ID == i && b.OnCard > 0)
                {
                    b.OnCard--;
                    b.Quantity++;
                }
            }
            _entities.SaveChanges();
        }

        public List<Book> Search(string value)
        {
            List<Book> list = new List<Book>();
            foreach (var m in _entities.Books)
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
