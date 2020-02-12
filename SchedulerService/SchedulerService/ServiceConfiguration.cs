using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SchedulerService
{
    public class ServiceConfiguration
    {
        public static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<Service>(service => 
                {
                    service.ConstructUsing(s => new Service());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });

                configure.RunAsLocalSystem();
                configure.SetServiceName("SchedulerService");
                configure.SetDisplayName("SchedulerService");
                configure.SetDescription("Service running background jobs using scheduler.");
            });
        }
    }
}
