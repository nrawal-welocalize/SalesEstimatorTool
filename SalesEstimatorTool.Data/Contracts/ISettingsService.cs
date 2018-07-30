using System;
using System.Collections.Generic;

namespace SalesEstimatorTool.Data.Contracts
{
    public interface ISettingsService
    {
        Int32 TaskPauseSeconds { get; }  

        String ExcelSettingPath { get; }

        String TempAttachmentFilePath { get; }

        String ExchangeLogin { get; }

        String ExchangePassword { get; }

        String SubjectRegexFilter { get; }

        List<String> ExchangeAdminEmails { get; }

        List<String> AbbyFormats { get; }

        String AbbyHotFolderInPath { get; }

        String AbbyHotFolderOutPath { get; }

        Int32 AbbyMaxWaitTimeMinutes { get; }
        Int32 AbbySleepTimeMilliseconds { get; }

        decimal MinimumPrice { get; }
    }
}
