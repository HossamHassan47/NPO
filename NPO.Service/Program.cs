using System.ServiceProcess;

namespace NPO.Service
{
    static class Program
    {

        public static string mailBackUpPath = @"E:\NPO\MailBackUp";

      

        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new NPOService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
