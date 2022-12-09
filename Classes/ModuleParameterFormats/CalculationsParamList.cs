namespace CServer.Classes.ModuleParameterFormats
{
    internal class CalculationsParamList
    {
        public CalculationsParamList(double param1, double param2, string oparator)
        {
            this.param1 = param1;
            this.param2 = param2;
            this.oparator = oparator;
        }

        public readonly double param1;
        public readonly double param2;
        public readonly string @oparator;
    }
}
