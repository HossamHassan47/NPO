using NPO.Code.Entity;
using NPO.Code.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NPO.Service
{
    public partial class NPOService : ServiceBase
    {
        Thread mailboxThread;

        public NPOService()
        {
            InitializeComponent();

            //TODO: Start new thread that reads emails every 20 seconds
            mailboxThread = new Thread(MailboxRead);
            mailboxThread.Start();
        }

        static void MailboxRead()
        {
            while (true)
            {
                new MailboxHelper().StartRead();

                Thread.Sleep(20000);
            }
        }
    }
}
