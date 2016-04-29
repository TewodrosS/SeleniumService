using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;
using Newtonsoft.Json;
using System.Configuration;

namespace SeleniumService
{
    partial class StartMyCar : ServiceBase
    {
        HttpSelfHostServer _server;
        public StartMyCar()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ServiceStart();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        public void ServiceStart()
        {
            var myUri = ConfigurationManager.AppSettings["ServiceUrl"];
            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(myUri);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{Controller}/{start}",
                defaults: new { start = RouteParameter.Optional }
                );

            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            _server = new HttpSelfHostServer(config);

            _server.OpenAsync().Wait();
        }
    }
}
