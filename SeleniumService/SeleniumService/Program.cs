using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumService
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            var startMyCar = new StartMyCar();
            log.Info("Starting Service ...");
            if (Environment.UserInteractive)
            {
                startMyCar.ServiceStart();
                Console.WriteLine("Press Enter to exit");
                Console.ReadLine();
            }
            ServiceBase.Run(startMyCar);
            startMyCar.Dispose();
        }
    }
}
