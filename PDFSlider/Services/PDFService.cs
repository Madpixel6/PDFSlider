using NLog;
using PDFSlider.CustomExceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace PDFSlider.Services
{
    public class PdfService : IPdfService, IDisposable
    {
        #region Instances, Fields
        private ThreadStart ReadAndUpdateThreadStart = null;
        private Thread ReadAndUpdateThread = null;

        public string CurrentPdfPath { get; set; }
        private int SecondsBetweenSlides { get; set; } = 0;
        private string DirPath { get; set; }
        #endregion

        private readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public PdfService()
        {
            ReadAndUpdateThreadStart = new ThreadStart(() => ReadAndUpdate());
            ReadAndUpdateThread = new Thread(ReadAndUpdateThreadStart);
        }

        private void ReadFilesInDirectory(ref List<string> filePaths)
        {
            if(filePaths.Count > 0)
                filePaths.Clear();
            if (!string.IsNullOrEmpty(DirPath))
            {
                try
                {
                    filePaths.AddRange(Directory.GetFiles(DirPath, "*.pdf"));
                    if (filePaths.Count < 1)
                        Logger.Error(new FileNotFoundException(), "No files with .pdf extension found in provided directory!");
                }
                catch(DirectoryNotFoundException ex)
                {
                    Logger.Error(ex, "Directory not found or path is invalid!");
                }
            }
            else
                Logger.Error(new InvalidDataException(), "Directory path is invalid!");
        }

        public void Run()
        {
            SecondsBetweenSlides = CfgService.ReadPropertyParseInt("secondsBetweenSlides");
            DirPath = CfgService.ReadProperty("selectedDirPath");

            if (!ReadAndUpdateThread.IsAlive)
            {
                ReadAndUpdateThread.Start();
            }
            else
                Logger.Error(new AlreadyRunningException(), "ReadAndUpdate thread is already running!");
        }

        private void ReadAndUpdate()
        {
            var filePaths = new List<string>();
            ReadFilesInDirectory(ref filePaths);
            while (true)
            {
                foreach (var item in filePaths)
                {
                    if (!File.Exists(item))
                        ReadFilesInDirectory(ref filePaths);
                    else
                        CurrentPdfPath = item;
                    Thread.Sleep(SecondsBetweenSlides * 1000);
                }
            }
        }

        public void Dispose()
        {
            ReadAndUpdateThread.Abort();
        }
    }
}
