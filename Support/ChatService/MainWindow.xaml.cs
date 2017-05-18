using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Support;
using System.ServiceModel;
using System.Net.Mail;
using System.Net;

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
            Messaging mess = new Messaging();
            try
            {
                Company c = _context.Companies.Where((b) => (b.CompanyID == companyid)).Select((s) => (s)).First();
                User u = _context.Users.Where((b) => (b.UserID == userid)).Select((s) => (s)).First();
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "smtp.gmail.com";
                client.Credentials = new System.Net.NetworkCredential("your gmail address", "password");
                sender = "your gmail address";
                MailMessage mail = new MailMessage(sender, c.email);
                mail.Subject = $"SupportService from:'{userid}' user";
                mail.Body = message;
                client.Send(mail);

                using (var db = new SupportServiceContext())
                {
                    mess.MessageText = message;
                    mess.Sender = sender;
                    mess.UserID = userid;
                    mess.CompanyID = companyid;
                    mess.Time = DateTime.Now;

                    db.Database.ExecuteSqlCommand($"insert into Messaging (UserID,CompanyID,MessageText,[Time],Sender) values ({mess.UserID},{mess.CompanyID},'{mess.MessageText}',{mess.Time.Day}/{mess.Time.Month}/{mess.Time.Year},'{mess.Sender}')");
                    db.SaveChanges();
                }
            }
            catch
            {

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
