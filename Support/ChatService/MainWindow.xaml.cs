using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Support;
using System.ServiceModel;

namespace ChatService
{
    public partial class MainWindow : Window, IMessage
    {
        public ServiceHost host = null;
        SupportServiceContext _context = new SupportServiceContext();
        List<Company> companies = new List<Company>();
        List<Messaging> messagess = new List<Messaging>();
        public static bool log = false;
        int compID = -1;

        public static User user;
        public MainWindow()
        {
            InitializeComponent();

            if (_context.Users
                 .Where((m) => (m.Usernae.Equals(Environment.UserDomainName)))
                 .Select((s) => (s)).Count() == 0)
            {
                user = new User { Usernae = Environment.UserDomainName, Password = "password" };
                _context.Users.Add(user);
                //_context.SaveChanges();
            }
            else
            {
                user = _context.Users.
                Where((m) => (m.Usernae.Equals(System.Environment.UserName))).
                Select((s) => (s)).First();
            }

            sendbutton.IsEnabled = false;
            host = new ServiceHost(typeof(MainWindow), new Uri("net.tcp://localhost:7000"));
            host.AddServiceEndpoint(typeof(IMessage), new NetTcpBinding(), "");
            host.Open();
            GetCompanies();
            foreach (var m in companies)
            {
                ListBoxItem newitem = new ListBoxItem();
                newitem.Content = m.CompanyName;
                newitem.Name = "i" + m.CompanyID;
                newitem.MouseDoubleClick += (sender, e) =>
                {
                    messages.Text = "";
                    companyname.Content = m.CompanyName;
                    sendbutton.IsEnabled = true;
                    GetMessages(m.CompanyID, user.UserID);
                    foreach (var n in messagess)
                    {
                        messages.Text += n.Sender + "-->" + n.MessageText + System.Environment.NewLine;
                    }
                    compID = int.Parse((sender as ListBoxItem).Name.Trim('i'));
                };
                companieslistbox.Items.Add(newitem);
            }
        }

        void GetCompanies()
        {
            companies = _context.Companies.ToList();
        }
        void GetMessages(int companyID, int userID)
        {
            messagess = _context.Messagings.
                Where((m) => (m.CompanyID == companyID && m.UserID == userID)).
                Select((s) => (s)).
                ToList();
        }


        public void AddMessage(string message, string sender, int userid, int companyid)
        {

            using (var db = new SupportServiceContext())
            {
                Messaging mess = new Messaging();

                mess.MessageText = message;
                mess.Sender = sender;
                mess.UserID = userid;
                mess.CompanyID = companyid;
                mess.Time = DateTime.Now;

                db.Database.ExecuteSqlCommand($"insert into Messaging (UserID,CompanyID,MessageText,[Time],Sender) values ({mess.UserID},{mess.CompanyID},'{mess.MessageText}',{mess.Time.Day}/{mess.Time.Month}/{mess.Time.Year},'{mess.Sender}')");
                db.SaveChanges();

            }
        }



        private void sendbutton_Click(object sender, RoutedEventArgs e)
        {
            AddMessage(message.Text, user.Usernae, user.UserID, compID);
            messages.Text += user.Usernae + "-->" + message.Text + System.Environment.NewLine;
            message.Text = "";
        }
    }
}
