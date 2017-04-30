using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookStore.ServiceReference1;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        static List<Book> Books_oncard = new List<Book>();
        Book book_oncard;
        int id;
        StackPanel sp, sp_forcard, remove_sp;
        Label label_books, label;
        Button add_btn, Remove_btn;
        private async void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            books_listbox.Items.Refresh();
            card_listbox.Items.Refresh();
            BookShopClient proxy = new BookShopClient();
            var list = await proxy.GetBooksAsync();
            while (books_listbox.Items.Count > 0)
                books_listbox.Items.RemoveAt(0);

            switch (combo.SelectedIndex)
            {
                case 0:
                    list = await proxy.GetBooksByAuthorAsync(searchtext.Text);
                    break;
                case 1:
                    list = await proxy.GetBooksByNameAsync(searchtext.Text);
                    break;
                case 2:
                    list = await proxy.GetBookByIDAsync(searchtext.Text);
                    break;
                case 3:
                    list = await proxy.GetBooksByPriceAsync(searchtext.Text);
                    break;
                case 4:
                    list = await proxy.GetBooksByGenreAsync(searchtext.Text);
                    break;
            }


            foreach (var m in list)
            {
                sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;

                label_books = new Label();
                label_books.Content = $"{m.Name}  {m.Author}  {m.Genre}  {m.Price}$   {m.Quantity}";
                sp.Children.Add(label_books);

                add_btn = new Button();
                add_btn.Name = "i" + Convert.ToString(m.ID);
                add_btn.Click += (s, ea) =>
                {
                    books_listbox.Items.Refresh();
                    card_listbox.Items.Refresh();
                    id = int.Parse((s as Button).Name.Trim('i'));

                    foreach (var book in list)
                    {
                        if (book.ID == id && book.Quantity > 0)
                        {
                            if (!Books_oncard.Contains(book))
                            {
                                book.Quantity--;
                                book_oncard = book;
                                book_oncard.Quantity = 1;
                                


                                sp_forcard = new StackPanel();
                                sp_forcard.Orientation = Orientation.Horizontal;
                                sp_forcard.Name = "i" + id.ToString();

                                label = new Label();
                                label.Content = $"{book.Name} {book_oncard.Quantity}";

                                Remove_btn = new Button();
                                Remove_btn.Content = "Remove";
                                Remove_btn.Name = "i" + id.ToString();
                                Remove_btn.Click += (se, ear) =>
                                {
                                    book.Quantity++;
                                    foreach (var k in card_listbox.Items)
                                        if ((k as StackPanel).Name == (se as Button).Name)
                                        {
                                            if (book_oncard.Quantity == 0)
                                                remove_sp = (StackPanel)k;
                                            else
                                                book_oncard.Quantity--;
                                            break;
                                        }
                                    card_listbox.Items.Remove(remove_sp);
                                   
                                };

                                sp_forcard.Children.Add(label);
                                sp_forcard.Children.Add(Remove_btn);

                                card_listbox.Items.Add(sp_forcard);
                                Books_oncard.Add(book_oncard);
                                break;
                            }
                            else
                            {
                                book.Quantity--;
                                ((Book)Books_oncard.Select((b) => (b.ID == book.ID))).Quantity++;
                                
                            }
                        }
                    }

                };
                add_btn.Content = "Add to cart";


                sp.Children.Add(add_btn);

                books_listbox.Items.Add(sp);
            }

        }
    }
}
