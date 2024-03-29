﻿using System.Net;

namespace CServer.Classes.ServerComponents
{
    /// <summary>
    /// A class that holds logging and debugging methods
    /// </summary>
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
        ///     Default is "<see cref="HttpStatusCode.InternalServerError"/> (=500)"
        ///     </para>
        /// </param>
        /// <param name="displayStackTrace">
        ///     If the stack trace should be printed.
        ///     <para>
        ///     Default is "<see langword="true"></see>"
        ///     </para>
        /// </param>
        /// <returns>
        /// If <paramref name="request"/> was not null then the altered <see cref="RequestData"/> object will be returned.
        /// Otherwise it will return null.
        /// </returns>
		public static RequestData? LogException(Exception exception, string environment, RequestData? request = null, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, bool displayStackTrace = true)
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

        /// <summary>
        /// Logs a request's values
        /// </summary>
        /// <param name="request">The request to log</param>
        public static void LogRequest(RequestData request)
        {
            Console.WriteLine($"\nModule: {request.Module} ({(int)request.Module})");
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

        /// <summary>
        /// Set the status code and description of a request on which an exception occurred
        /// <para>
        /// The description will be inferred from the status code
        /// </para>
        /// </summary>
        /// <param name="request">The request that needs the status code and description</param>
        /// <param name="status">The status code</param>
        /// <returns>The modified request</returns>
        private static RequestData SetRequestStatus(RequestData request, HttpStatusCode status)
        {
            Dictionary<HttpStatusCode, string> descriptions = new()
            {
                {HttpStatusCode.BadRequest, "Bad request" },
                {HttpStatusCode.InternalServerError, "Internal server error" },
                {HttpStatusCode.NotImplemented, "Not implemented" }
            };

            request.Result = $"{status} {descriptions[status]}";
            request.StatusCode = status;
            request.StatusDescription = descriptions[status];
            Console.WriteLine($"Sent '{status} {descriptions[status]}' to request origin");
            Console.WriteLine("\n-------------------------------------\n");

            return request;
        }
    }
}
