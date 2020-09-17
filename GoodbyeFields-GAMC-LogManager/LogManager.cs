using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace LogManagers
{
    public class LogManagers
    {

        //private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ILogger<LogManagers> logger;

        public LogManagers(ILogger<LogManagers> loggers)
        {
            logger = loggers;
        }

        /// <summary>
        /// </summary>
        /// <param name="message">String Builder</param>      
        /// <name>Vikas Kumar</name>
        /// <lastmodifiedOn></lastmodifiedOn>
        /// <version>1.0</version>
        public static void WriteTraceLog(StringBuilder message)
        {
            //// check the configur ation setting for write the trace log.
            //if (string.Compare(Convert.ToString(ConfigurationManager.AppSettings["IsTrace"], CultureInfo.CurrentCulture).ToUpper(CultureInfo.CurrentCulture), "True",
            //    true, CultureInfo.CurrentCulture) == 0 && message != null)
            //{
            logger.LogInformation(Convert.ToString(message));
            // }
        }

        /// <summary>
        /// WriteErrorLog function write the exception in error log file.
        /// </summary>
        /// <param name="objException">Exception occured</param>
        /// <name>Vikas Kumar</name>
        /// <lastmodifiedOn></lastmodifiedOn>
        /// <version>1.0</version>
        public static void WriteErrorLog(Exception objException)
        {
            ////// check the configuration setting for write the error log.
            //if (string.Compare(ConfigurationManager.AppSettings["MakeErrorLog"].ToUpper(CultureInfo.CurrentCulture), "True", true, CultureInfo.CurrentCulture) == 0
            //    && objException != null)
            //{
            bool yes = false;
            if (yes == false)
            {
                logger.LogError(objException, objException.Message.ToString());
            }
            // }
        }

    }
}

