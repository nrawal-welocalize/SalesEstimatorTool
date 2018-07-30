using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using SalesEstimatorTool.Data.Contracts;

namespace SalesEstimatorTool.Data.Access.Services
{
    public class SettingsService : ISettingsService
    {
        public Int32 TaskPauseSeconds
        {
            get
            {
                return Int32.Parse(GetConfigurationSettingValue("TaskPauseSeconds"));
            }
        }

        public String ExcelSettingPath
        {
            get
            {
                return GetConfigurationSettingValue("ExcelSettingPath");
            }
        }

        public String TempAttachmentFilePath
        {
            get
            {
                String path = GetConfigurationSettingValue("TempAttachmentFilePath");
                if (path[path.Length - 1] != '\\')
                {
                    path += "\\";
                }
                return path;

            }
        }

        public String ExchangeLogin
        {
            get
            {
                return GetConfigurationSettingValue("ExchangeLogin");
            }
        }

        public String ExchangePassword
        {
            get
            {
                return GetConfigurationSettingValue("ExchangePassword");
            }
        }

        public List<String> ExchangeAdminEmails
        {
            get
            {
                return GetConfigurationSettingValue("ExchangeAdminEmails")
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
            }
        }

        public String SubjectRegexFilter
        {
            get
            {
                return GetConfigurationSettingValue("SubjectRegexFilter");
            }
        }

        public List<String> AbbyFormats
        {
            get
            {
                return GetConfigurationSettingValue("AbbyFormat")
                    .Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
            }

        }

        public String AbbyHotFolderInPath
        {
            get
            {
                String path = GetConfigurationSettingValue("AbbyHotFolderInPath");
                if (path[path.Length-1] != '\\')
                {
                    path += "\\";
                }
                return path;
            }
        }

        public String AbbyHotFolderOutPath
        {
            get
            {
                String path = GetConfigurationSettingValue("AbbyHotFolderOutPath");
                if (path[path.Length - 1] != '\\')
                {
                    path += "\\";
                }
                return path;
            }
        }

        public Int32 AbbyMaxWaitTimeMinutes
        {
            get
            {
                return Int32.Parse(GetConfigurationSettingValue("AbbyMaxWaitTimeMinutes"));
            }
        }

        public Int32 AbbySleepTimeMilliseconds
        {
            get { return Int32.Parse(GetConfigurationSettingValue("AbbySleepTimeMilliseconds")); }
        }

        public decimal MinimumPrice
        {
            get { return decimal.Parse(GetConfigurationSettingValue("MinimumPrice")); }
        }

        #region Private

        private static String GetConfigurationSettingValue(String key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }

        #endregion Private
    }
}
