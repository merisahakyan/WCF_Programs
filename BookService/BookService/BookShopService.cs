using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BookService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class BookShopService : IBookShop
    {
        BooksEntities _entities = new BooksEntities();

        public BookShopService()
        {

        }

        public void AddToCard(string id)
        {
            int i = int.Parse(id);
            foreach (var b in _entities.Books)
            {
                if (b.ID == i && b.Quantity > 0)
                {
                    if(Contains(b))
                    {
                        ChangeQuantity(b, 1);
                        b.Quantity--;
                    }
                    else
                    {
                        OnCard oc = new OnCard();
                        oc.BookId = b.ID;
                        oc.Quantity = 1;
                        _entities.OnCards.Add(oc);
                    }
                }
            }
            

        }

        private bool Contains(Book b)
        {
            foreach (var oc in _entities.OnCards)
            {
                if (oc.Book == b)
                    return true;
            }
            return false;
        }
        private void ChangeQuantity(Book b,int ch)
        {
            foreach(var c in _entities.OnCards)
            {
                if(c.Book==b)
                    switch(ch>0)
                    {
                        case true:c.Quantity++;break;
                        case false:c.Quantity--;break;
                    }
            }
        }

        public Book GetBookByID(string id)
        {
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

        public OnCard GetOnCardBookByID(string id)
        {
            int i = int.Parse(id);
            OnCard book = null;
            foreach (var m in _entities.OnCards)
            {
                if (m.BookId == i)
                {
                    book = m;
                    break;
                }
            }
            return book;
        }

        public List<OnCard> GetOnCardBooks()
        {
            return _entities.OnCards.ToList();
        }

        public void RemoveFromCard(string id)
        {
            int i = int.Parse(id);
            foreach (var b in _entities.OnCards)
            {
                if(b.BookId==i && b.Quantity>0)
                {
                    var book = GetBookByID(id);
                    ChangeQuantity(book, -1);
                }
            }
            
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
