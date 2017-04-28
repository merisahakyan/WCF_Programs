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
        static List<Book> oncard = new List<Book>();
        int id;
        StackPanel sp, sp_forcard, remove_sp;
        Label label_books, label;
        Button add_btn, Remove_btn;
        private async void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            BookShopClient proxy = new BookShopClient();
            var list = await proxy.GetBooksAsync();
            books_listbox.Items.Remove(sp);

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
                label_books.Content = $"{m.Name}  {m.Author}  {m.Genre}  {m.Price}$";
                sp.Children.Add(label_books);

                add_btn = new Button();
                add_btn.Name = "i" + Convert.ToString(m.ID);
                add_btn.Click += (s, ea) =>
                {
                    id = int.Parse((s as Button).Name.Trim('i'));

                    foreach (var book in list)
                    {
                        if (book.ID == id && book.Quantity > 0)
                        {
                            book.Quantity--;
                            books_listbox.Items.Refresh();
                            oncard.Add(book);

                            sp_forcard = new StackPanel();
                            sp_forcard.Orientation = Orientation.Horizontal;
                            sp_forcard.Name = "i" + book.ID.ToString();

                            label = new Label();
                            label.Content = $"{book.Name}";

                            Remove_btn = new Button();
                            Remove_btn.Content = "Remove";
                            Remove_btn.Name = "i" + card_listbox.Items.Count.ToString();
                            Remove_btn.Click += (se, ear) =>
                              {
                                  book.Quantity++;
                                  card_listbox.Items.RemoveAt(int.Parse((se as Button).Name.Trim('i')));
                              };

                            sp_forcard.Children.Add(label);
                            sp_forcard.Children.Add(Remove_btn);

                            card_listbox.Items.Add(sp_forcard);
                            break;
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
