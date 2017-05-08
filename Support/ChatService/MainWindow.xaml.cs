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
        int compID = -1;
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
                    GetMessages(m.CompanyID);
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
        void GetMessages(int companyID)
        {
            messagess = _context.Messagings.Where((m) => (m.CompanyID == companyID)).Select((s) => (s)).ToList();
        }


        public void AddMessage(string message, string sender, int userid, int companyid)
        {
            Messaging mess = new Messaging();

            mess.MessageText = message;
            mess.Sender = sender;
            mess.UserID = userid;
            mess.CompanyID = companyid;
            mess.Time = DateTime.Now;


            _context.Messagings.Add(mess);
            _context.SaveChanges();
        }

        

        private void sendbutton_Click(object sender, RoutedEventArgs e)
        {
            AddMessage(message.Text, "Meri", 1, compID);
            messages.Text += "Meri" + "-->" + message.Text + System.Environment.NewLine;
            message.Text = "";
        }
    }
}
