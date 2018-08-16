using System.ServiceProcess;

namespace NPO.Service
{
    static class Program
    {

        public static string mailBackUpPath = @"E:\NPO\MailBackUp";

        public static string userName = "NPOtest@outlook.com";

        public static string password = "npo@123456";

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
