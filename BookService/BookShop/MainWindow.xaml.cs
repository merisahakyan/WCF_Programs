using System;
using System.Windows;
using System.Windows.Controls;
using BookShop.BookService;

namespace BookShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StackPanel sp;
        Grid grid;
        Label label_books, label;
        Button add_btn, remove_btn;
        Book[] list_books, list_card;
        BookShopClient bookclient = new BookShopClient();
        OnCardClient oncardclient = new OnCardClient();
        public MainWindow()
        {
            InitializeComponent();
            InitializeBooks();
            InitializeCard();
            ShowCardList();
        }
        void InitializeBooks()
        {
            switch (combo.SelectedIndex)
            {
                case 0:
                    list_books = bookclient.GetBooksByAuthor(searchtext.Text);
                    break;
                case 1:
                    list_books = bookclient.GetBooksByName(searchtext.Text);
                    break;
                case 2:
                    list_books[0] = bookclient.GetBookByID(searchtext.Text);
                    break;
                case 3:
                    list_books = bookclient.GetBooksByPrice(searchtext.Text);
                    break;
                case 4:
                    list_books = bookclient.GetBooksByGenre(searchtext.Text);
                    break;
                default:
                    list_books = bookclient.GetBooks();
                    break;
            }

        }
        void InitializeCard()
        {
            list_card = oncardclient.GetOnCardBooks();
        }
        private void ShowBooksList()
        {
            while (books_listbox.Items.Count > 0)
                books_listbox.Items.RemoveAt(0);
            foreach (var m in list_books)
            {
                grid = new Grid();
                grid.Height = 30;
                ColumnDefinition[] c = new ColumnDefinition[6];
                for(int i=0;i<6;i++)
                {
                    c[i] = new ColumnDefinition();
                    c[i].Width = new GridLength(((System.Windows.Controls.Panel)Application.Current.MainWindow.Content).ActualWidth*4/36);
                }
                
                label = new Label();
                label.Content = $"{m.Name}";
                grid.Children.Add(label);
                Grid.SetColumn(label, 0);

                label = new Label();
                label.Content = $"{m.Author}";
                grid.Children.Add(label);
                Grid.SetColumn(label, 1);

                label = new Label();
                label.Content = $"{m.Genre}";
                grid.Children.Add(label);
                Grid.SetColumn(label, 2);

                label = new Label();
                label.Content = $"{m.Price}";
                grid.Children.Add(label);
                Grid.SetColumn(label, 3);

                label = new Label();
                label.Content = $"{m.Quantity}";
                grid.Children.Add(label);
                Grid.SetColumn(label, 4);

                add_btn = new Button();
                add_btn.Name = "i" + Convert.ToString(m.ID);
                add_btn.Click += (s, ea) =>
                {
                    oncardclient.AddToCard((s as Button).Name);
                    InitializeBooks();
                    InitializeCard();
                    ShowBooksList();
                    ShowCardList();
                };
                add_btn.Content = "Add to cart";
                grid.Children.Add(add_btn);
                Grid.SetColumn(add_btn, 5);

                for (int i = 0; i < 6; i++)
                {
                    grid.ColumnDefinitions.Add(c[i]);
                }

                books_listbox.Items.Add(grid);
            }
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            InitializeBooks();
            ShowBooksList();
        }


        private void ShowCardList()
        {

            while (card_listbox.Items.Count > 0)
                card_listbox.Items.RemoveAt(0);
            foreach (var m in list_card)
            {
                sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;

                label_books = new Label();
                label_books.Content = $"{m.Name}  {m.OnCard}";
                sp.Children.Add(label_books);

                remove_btn = new Button();
                remove_btn.Name = "i" + Convert.ToString(m.ID);
                remove_btn.Content = "Remove";
                remove_btn.Click += (s, ea) =>
                  {
                      oncardclient.RemoveFromCard((s as Button).Name);
                      InitializeBooks();
                      InitializeCard();
                      ShowBooksList();
                      ShowCardList();
                  };
                sp.Children.Add(remove_btn);

                card_listbox.Items.Add(sp);

                //grid = new Grid();
                //grid.Height = 30;
                //ColumnDefinition[] x = new ColumnDefinition[3];
                //for (int i = 0; i < 3; i++)
                //{
                //    x[i] = new ColumnDefinition();
                //    x[i].Width = new GridLength(1, GridUnitType.Star);
                //}


                //label = new Label();
                //label.Content = $"{m.Name}";
                //grid.Children.Add(label);
                //Grid.SetColumn(label, 0);
                //grid.ColumnDefinitions.Add(x[0]);

                //label = new Label();
                //label.Content = $"{m.OnCard}";
                //grid.Children.Add(label);
                //Grid.SetColumn(label, 1);
                //grid.ColumnDefinitions.Add(x[1]);


                //remove_btn = new Button();
                //remove_btn.Name = "i" + Convert.ToString(m.ID);
                //remove_btn.Content = "Remove";
                //remove_btn.Click += (s, ea) =>
                //{
                //    oncardclient.RemoveFromCard((s as Button).Name);
                //    InitializeBooks();
                //    InitializeCard();
                //    ShowBooksList();
                //    ShowCardList();
                //};
                //grid.Children.Add(remove_btn);
                //Grid.SetColumn(label, 2);
                //grid.ColumnDefinitions.Add(x[2]);


                //card_listbox.Items.Add(grid);
            }
        }
    }
}
