namespace CServer.Classes.ModuleParameterFormats
{
    /// <summary>
    /// These classes are used to format the <see cref="ServerComponents.RequestData">request </see><see cref="ServerComponents.RequestData.Parameters">parameters</see> 
    /// so they are easier to use in the <see cref="Interfaces.IModule">Module Classes</see>
    /// </summary>
    internal abstract partial class ParamListBase
    {
        public enum SingleInputOperators
        {
            // Using V as √ because it is not allowed in enums
            None, V, π, P,
        }


        /// <summary>
        /// Checks if a given operator only takes one input or not.
        /// </summary>
        /// <param name="operator">The operator to test</param>
        /// <returns><see cref="bool">true</see> if <paramref name="operator"/> takes a single input and <see cref="bool">false</see> if not</returns>
        public static bool IsSingleInputOpaerator(string @operator)
        {
            if (@operator == "√") @operator = "V";
            string[] sIO = Enum.GetNames(typeof(SingleInputOperators));
            return sIO.Contains(@operator);
        }
    }
}
