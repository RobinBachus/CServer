namespace CServer.Classes
{
    internal class Calculations
    {
        enum SubModules
        {
            Calculator = 1,
            Divisibility = 2,
            Pi = 3
        }
        public static RequestData GetResult(RequestData request)
        {
            if (request.Parameters == null)
            {
                throw new Exception("Parameters array is null");
            }

            Console.WriteLine($"\nModule: {request.Module}");
            Console.WriteLine($"Submodule: {(SubModules)request.Submodule}");
            for (int i = 0; i < request.Parameters.Length; i++)
            {
                Console.WriteLine($"Param{i}: {request.Parameters[i]}");
            }

            switch ((SubModules)request.Submodule)
            {
                case SubModules.Calculator:
                    double result = Calculator(
                        Convert.ToDouble(request.Parameters[0]), 
                        Convert.ToDouble(request.Parameters[1]), 
                        request.Parameters[2]);
                    request.Result = result;
                    break;
                case SubModules.Divisibility:
                case SubModules.Pi:
                default:
                    request.Result = 0;
                    break;
            }
            Console.WriteLine($"\nResult: {request.Result}");
            Console.WriteLine("\n-------------------------------------\n");
            return request;
        }

        private static double Calculator(double a, double b, string oparator)
        {
            switch (oparator)
            {
                case "+":
                    return a + b;
                case "-":
                    return a - b;
                case "*":
                    return a * b;
                case "/":
                    return a / b;
                case "^":
                    return Math.Pow(a, b);
                case "√":
                    return Math.Sqrt(a);
                default:
                    break;
            }
            throw new Exception($"Oparator {oparator} not recognized");
        }
    }
}