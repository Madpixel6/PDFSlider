using PDFSlider.Services;
using System.Threading.Tasks;

namespace PDFSlider.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields, Properties
        public string currPdfPath;
        public string CurrPdfPath
        {
            get => currPdfPath;
            set
            {
                currPdfPath = value;
                OnPropertyChanged("CurrPdfPath");
            }

        }

        private Task SwitchPDFsTask { get; }
        #endregion
        private readonly IPdfService pdfService;
        public MainWindowViewModel()
        {
            pdfService = new PdfService();
            pdfService.Run();

            SwitchPDFsTask = SwitchPDFs();

        }
        private async Task SwitchPDFs()
        {
            while (true)
            {
                CurrPdfPath = pdfService.CurrentPdfPath;
                await Task.Delay(1000);
            }
        }
    }
}
