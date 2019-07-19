using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace VideoHub.ServiceApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseKestrel()
                .UseStartup<Startup>().ConfigureKestrel((context,options)=> {
                    options.Listen(IPAddress.Any, 9000);
                    options.Listen(IPAddress.Any, 9001, listenOptions =>
                    {
                        listenOptions.UseHttps();
                    });
                });
            
    }
}
