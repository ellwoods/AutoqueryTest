using Funq;
using ServiceStack;
using ServiceStack.Admin;
using Autoquery.ServiceInterface;
using ServiceStack.VirtualPath;
using System;

namespace Autoquery
{
    //VS.NET Template Info: https://servicestack.net/vs-templates/EmptyAspNet
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("Autoquery", typeof(MyServices).Assembly) { }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            //Config examples
            //this.Plugins.Add(new PostmanFeature());
            //this.Plugins.Add(new CorsFeature());
            Plugins.Add(new RequestLogsFeature
            {
                RequestLogger = new CsvRequestLogger(
        files: new FileSystemVirtualPathProvider(this, Config.WebHostPhysicalPath),
        requestLogsPattern: "requestlogs/{year}-{month}/{year}-{month}-{day}.csv",
        errorLogsPattern: "requestlogs/{year}-{month}/{year}-{month}-{day}-errors.csv",
        appendEvery: TimeSpan.FromSeconds(1)
    ),
                EnableResponseTracking = true
            });

            Plugins.Add(new AutoQueryFeature { MaxLimit = 100 });
            Plugins.Add(new AdminFeature());
        }
    }
}