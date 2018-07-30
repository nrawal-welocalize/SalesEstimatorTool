using System.ComponentModel;

namespace SalesEstimatorTool.Data.Enums
{
    public enum ProgressTaskStatus
    {
        [Description("Working")]
        Working,

        [Description("Connect To Exchange")]
        ConnectToExchange,

        [Description("Find Word File(s)")]
        FindWordFiles,

        [Description("Create MateCat Project")]
        CreateMateCatProject,

        [Description("Read MateCat Status")]
        ReadMateCatProject,

        [Description("Generate Excel File")]
        GenerateExcelFile,

        [Description("Download OmegaT")]
        DownloadOmegaT,

        [Description("Ended")]
        Ended
    }
}
