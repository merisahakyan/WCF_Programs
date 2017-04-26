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

        StackPanel sp;
        Label label;
        Button btn;
        private async void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            BookShopClient proxy = new BookShopClient();
            var list = await proxy.GetBooksAsync();


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

                label = new Label();
                label.Content = $"{m.Name}  {m.Author}  {m.Genre}  {m.Price}$";

                btn = new Button();
                btn.Content = "Add to cart";

                sp.Children.Add(label);
                sp.Children.Add(btn);

                listbox.Items.Add(sp);
            }

        }
    }
}
