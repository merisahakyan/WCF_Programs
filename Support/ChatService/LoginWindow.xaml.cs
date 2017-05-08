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
using System.Windows.Shapes;

namespace ChatService
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        List<User> users = new List<User>();
        SupportServiceContext _c = new SupportServiceContext();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void loginbutton_Click(object sender, RoutedEventArgs e)
        {
            users = _c.Users.ToList();
            foreach(var m in users)
                if(m.Usernae==usernamebox.Text && m.Password==passwordbox.Password.ToString())
                {
                    MainWindow.log = true;
                    MainWindow.user = m;
                    break;
                }
            if (!MainWindow.log)
                validationlabel.Content = "Wrong username or password!";
            else
            {
                validationlabel.Content = "";
            }
        }
    }
}
