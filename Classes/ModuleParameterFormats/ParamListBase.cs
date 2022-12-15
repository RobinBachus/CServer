namespace CServer.Classes.ModuleParameterFormats
{
    internal abstract partial class ParamListBase
    {
        public enum SingleInputOperators
        {
            None = 0,
            V = 1, // Using V as √ because it is not allowed in enums
            π = 2,
            P = 3,
        }


        // TODO: document function
        public static bool IsSingleInputOpaerator(string op)
        {
            if (op == "√") op = "V";
            string[] sIO = Enum.GetNames(typeof(SingleInputOperators));
            return sIO.Contains(op);
        }
    }
}
