using System;
using System.IO;
using System.Threading.Tasks;

namespace PDFSlider.Services
{
    public class PdfService : IPdfService
    {
        private Task readUpdateTask;

        public string CurrentPdfPath { get; set; }
        private int SecondsBetweenSlides { get; set; } = 0;
        private string DirPath { get; set; }

        private string[] FilePaths { get; set; }

        private void ReadFilesInDirectory()
        {
            if (!string.IsNullOrEmpty(DirPath))
            {
                FilePaths = Directory.GetFiles(DirPath, "*.pdf");
            }
        }

        public void Run()
        {
            SecondsBetweenSlides = CfgService.ReadPropertyParseInt("secondsBetweenSlides");
            DirPath = CfgService.ReadProperty("selectedDirPath");

            if (readUpdateTask is null)
            {
                readUpdateTask = ReadAndUpdate();
            }
            else
                throw new Exception("Slider task is already running!");
        }

        private async Task ReadAndUpdate()
        {
            ReadFilesInDirectory();
            while (true)
            {
                foreach (var item in FilePaths)
                {
                    if (!File.Exists(item))
                        ReadFilesInDirectory();
                    else
                        CurrentPdfPath = item;
                    await Task.Delay(SecondsBetweenSlides * 1000);
                }


            }
        }
    }
}
