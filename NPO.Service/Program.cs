using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NPO.Service
{
    static class Program
    {
        //Abdelhamy
        // test 
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
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
