using NLog;
using System.Collections.Generic;
using System.IO;

namespace PDFSlider.Services
{
    public class PdfService : IPdfService
    {
        #region Instances, Fields
        public string CurrentPdfPath { get; set; }
        private string DirPath { get; set; }
        #endregion

        private readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public void Initialize()
        {
            DirPath = CfgService.ReadProperty("selectedDirPath");
        }
        public Queue<string> GetFilesQueue()
        {
            var pdfsPathsQueue = new Queue<string>();

            var filePaths = new List<string>();
            ReadFilesInDirectory(ref filePaths);
            foreach (var item in filePaths)
            {
                if (!File.Exists(item))
                {
                    Logger.Warn("Files were changed while getting them");
                    return null;
                }
                else
                    pdfsPathsQueue.Enqueue(item);
            }
            return pdfsPathsQueue;
        }

        private void ReadFilesInDirectory(ref List<string> filePaths)
        {
            if (filePaths.Count > 0)
                filePaths.Clear();
            if (!string.IsNullOrEmpty(DirPath))
            {
                try
                {
                    filePaths.AddRange(Directory.GetFiles(DirPath, "*.pdf"));
                    if (filePaths.Count < 1)
                        Logger.Error(new FileNotFoundException(), "No files with .pdf extension found in provided directory!");
                }
                catch (DirectoryNotFoundException ex)
                {
                    Logger.Error(ex, "Directory not found or path is invalid!");
                }
            }
            else
                Logger.Error(new InvalidDataException(), "Directory path is invalid!");
        }
    }
}
