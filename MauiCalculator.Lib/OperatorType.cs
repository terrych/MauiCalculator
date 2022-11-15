using System.ComponentModel;

namespace MauiCalculator.Lib
{
    public enum OperatorType
    {
        None,
        [Description("+")]
        Plus,
        [Description("-")]
        Minus,
        [Description("×")]
        Multiply,
        [Description("÷")]
        Divide
    }
}
