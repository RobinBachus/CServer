using CServer.Interfaces;
using System.Net;

namespace CServer.Classes.ServerComponents
{
    internal class Information
    {
        /// <summary>
        /// Logs a formatted and colored error message to the console
        /// </summary>
        /// <param name="exception">The exception that got thrown</param>
        /// <param name="environment">A message that should describe where the exception was caught</param>
        /// <param name="request">
        ///     Give a <see cref="RequestData"></see> object to set the status and result in case of errors
        /// </param>
        /// <param name="statusCode">
        ///     The status code that should be set on the <see cref="RequestData"></see>.
        ///     <para>
        ///     The status description will be automatically inferred from this code.
        ///     </para>
        ///     <para>
        ///     Default is "<see langword="500"></see>"
        ///     </para>
        /// </param>
        /// <param name="displayStackTrace">
        ///     If the stack trace should be printed.
        ///     <para>
        ///     Default is "<see langword="true"></see>"
        ///     </para>
        /// </param>
		public static RequestData? LogException(Exception exception, string environment, RequestData? request = null, int statusCode = 500, bool displayStackTrace = true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{exception.GetType()}: {environment}");
            if (exception.Message != "")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Massage: ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(exception.Message);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Exception did not include a message");
            }
            if (displayStackTrace)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Stack trace:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(exception.StackTrace);
            }
            Console.ResetColor();
            Console.WriteLine("\n");
            if (request != null)
            {
                return SetRequestStatus(request, statusCode);
            }
            return null;
        }

        public static void LogRequest(RequestData request)
        {
            Console.WriteLine($"\nModule: {request.Module}");
            if ((!(request.Parameters == null)) && request.Parameters.Length > 0)
            {
                for (int i = 0; i < request.Parameters.Length; i++)
                {
                    Console.WriteLine($"Param{i}: {request.Parameters[i]}");
                }
            }
            else
            {
                Console.WriteLine("No parameters given");
            }
            Console.WriteLine($"\nResult: {request.Result}");
            Console.WriteLine("\n-------------------------------------\n");
        }

        private static RequestData SetRequestStatus(RequestData request, int status)
        {
            Dictionary<int, string> descriptions = new()
            {
                {500, "Internal server error" },
                {501, "Not implemented" }
            };

            request.Result = $"{status} {descriptions[status]}";
            request.StatusCode = (HttpStatusCode)status;
            request.StatusDescription = descriptions[status];
            Console.WriteLine($"Sent '{status} {descriptions[status]}' to request origin");
            Console.WriteLine("\n-------------------------------------\n");

            return request;
        }
    }
}
