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
using BookShop.ServiceReference1;

namespace BookShop
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
        StackPanel sp, sp_forcard, remove_sp;
        Label label_books, label;
        Button add_btn, Remove_btn;
        private async void Search_Button_Click(object sender, RoutedEventArgs e)
        {
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
                    list[0] = await proxy.GetBookByIDAsync(searchtext.Text);
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
                add_btn.Name =Convert.ToString(m.ID);
                add_btn.Click += (s, ea) =>
                {
                    

                };
                add_btn.Content = "Add to cart";


                sp.Children.Add(add_btn);
                sp.Name= Convert.ToString(m.ID);

                books_listbox.Items.Add(sp);
            }


        }
    }
}
