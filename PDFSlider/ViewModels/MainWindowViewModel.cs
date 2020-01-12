using PDFSlider.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PDFSlider.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private readonly IPdfService _pdfService;
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
        public MainWindowViewModel()
        {
            _pdfService = Bootstrap.Resolve<IPdfService>();
            InitCommands();

            _pdfService.Run();

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
                CurrPdfPath = _pdfService.CurrentPdfPath;
                await Task.Delay(1000);
            }
        }
        private void ExitProgram()
        {
            Environment.Exit(0);
        }
    }
}
