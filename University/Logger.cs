using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University
{
    public static class Logger
    {
        private static readonly ILogger logger;

        static Logger()
        {
            logger = new LoggerConfiguration()
                .WriteTo.File(System.Web.Hosting.HostingEnvironment.MapPath("~/Logs/log.txt"))
                .CreateLogger();
        }

        public static void LogError(string error)
        {
            logger.Error(error);
        }
    }
}