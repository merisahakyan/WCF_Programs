﻿using System;
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
using BookShop.BookService;

namespace BookShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StackPanel sp;
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
                sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;

                label_books = new Label();
                label_books.Content = $"{m.Name}  {m.Author}  {m.Genre}  {m.Price}$   {m.Quantity}";
                sp.Children.Add(label_books);

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


                sp.Children.Add(add_btn);
                sp.Name = "i" + Convert.ToString(m.ID);

                books_listbox.Items.Add(sp);
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
            }
        }
    }
}
