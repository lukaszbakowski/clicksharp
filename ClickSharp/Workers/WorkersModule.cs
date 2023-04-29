using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;


namespace ClickSharp.Workers
{
    public static class WorkersModule
    {
        public static IServiceCollection AddWorkersModule(this IServiceCollection services)
        {
            services.AddHostedService<ClientStoreWorker>();
            return services;
        }
    }
}
