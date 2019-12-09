using System;
using System.IO;
using System.Threading.Tasks;

namespace PDFSlider.Services
{
    public class PdfService : IPdfService
    {
        private Task sliderTask;

        public string CurrentPdfPath { get; set; }
        private int SecondsBetweenSlides { get; set; } = 0;
        private string DirPath { get; set; }

        private string[] FilePaths { get; set; }


        public PdfService()
        {
            SecondsBetweenSlides = CfgService.ReadPropertyParseInt("secondsBetweenSlides");
            DirPath = CfgService.ReadProperty("selectedDirPath");
        }

        private void ReadFilesInDirectory()
        {
            if (!string.IsNullOrEmpty(DirPath))
            {
                FilePaths = Directory.GetFiles(DirPath, "*.pdf");
            }
            //string localPath;
            //var assemblyNameLen = System.Reflection.Assembly.GetExecutingAssembly()?.ManifestModule.Name.Length;
            //if (assemblyNameLen.HasValue)
            //{
            //    var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //    localPath = assemblyLocation.Substring(0, assemblyLocation.Length - (assemblyNameLen.Value + 1));


            //   // filePaths = Directory.GetFiles(localPath, "*.pdf");
            //   // PdfPath = "Resources/testPDF.pdf";
            //    return true; // success
            //}
        }

        public void Run()
        {
            if (sliderTask is null)
            {
                var tsk = ReadAndUpdate();
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
                    CurrentPdfPath = item;
                    await Task.Delay(SecondsBetweenSlides * 1000);
                }


            }
        }
    }
}
