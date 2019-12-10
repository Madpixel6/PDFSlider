using PDFSlider.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PDFSlider.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand ESCCommand { get; private set; }
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
            InitCommands();

            pdfService = new PdfService();
            pdfService.Run();

            SwitchPDFsTask = SwitchPDFs();

        }
        private void InitCommands()
        {
            ESCCommand = new RelayCommand(obj => ExitProgram(), _ => true);
        }
        private async Task SwitchPDFs()
        {
            while (true)
            {
                CurrPdfPath = pdfService.CurrentPdfPath;
                await Task.Delay(1000);
            }
        }
        private void ExitProgram()
        {
            System.Environment.Exit(0);
        }
    }
}
