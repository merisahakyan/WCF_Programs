using Hut;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceHost host = new ServiceHost(typeof(HutService));
                host.Open();
                Console.WriteLine("Any key to exit");
                Console.ReadKey();
                host.Close();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
        }
    }
}
