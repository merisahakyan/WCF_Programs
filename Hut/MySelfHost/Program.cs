using Hut;
using System;
using System.Diagnostics;
using System.ServiceModel;

namespace MySelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceHost host = new ServiceHost(typeof(HutService));
                host.Open();
                Console.WriteLine("Press any key!");
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
