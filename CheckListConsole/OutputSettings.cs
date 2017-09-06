using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CheckListConsole
{
    public class OutputSettings
    {
        public OutputSettings()
        {
            File = "file.txt";
        }

        public string Folder { get; set; }
        public string File { get; set; }

        public string GetResportFilePath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), Folder, File);
        }

        public string GetReportDirectory()
        {
            return Path.GetDirectoryName(GetResportFilePath());
        }
    }
}
