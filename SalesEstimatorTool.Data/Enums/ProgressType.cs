using System.ComponentModel;
using SalesEstimatorTool.Data.Attributes;

namespace SalesEstimatorTool.Data.Enums
{
    public enum ProgressType
    {
        [Description("Text")]
        [Color("Black")]
        Text = 0,

        [Description("Information")]
        [Color("Blue")]
        Information = 1,

        [Description("Warning")]
        [Color("Goldenrod")]
        Warning = 2,

        [Description("Error")]
        [Color("Red")]
        Error = 3
    }
}
