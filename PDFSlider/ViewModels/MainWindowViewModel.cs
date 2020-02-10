using NLog;
using PDFSlider.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PDFSlider.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Commands
        public ICommand ESCCommand { get; private set; }
        #endregion

        #region Fields, Properties, Instances

        private readonly IPdfService _pdfService;
        private readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private ThreadStart SwitchPDFThreadStart = null;
        private Thread SwitchPDFThread = null;

        private int SecondsBetweenSlides { get; set; }
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
        #endregion
        public MainWindowViewModel()
        {
            _pdfService = Bootstrap.Resolve<IPdfService>();

            try
            {
                SecondsBetweenSlides = CfgService.ReadPropertyParseInt("secondsBetweenSlides");
            }
            catch (Exception ex)
            {
                if (ex is FormatException || ex is OverflowException)
                {
                    SecondsBetweenSlides = 3;
                    Logger.Warn($"Invalid read from config file, key 'secondsBetweenSlides'\n ex: {ex.StackTrace}");
                }
            }
            InitCommands();

            _pdfService.Initialize();

            SwitchPDFThreadStart = new ThreadStart(() => SwitchPDFs());
            SwitchPDFThread = new Thread(SwitchPDFThreadStart);

            SwitchPDFThread.Start();
        }
        private void InitCommands()
        {
            ESCCommand = new RelayCommand(obj => ExitProgram(), _ => true);
        }
        private void SwitchPDFs()
        {
            while (true)
            {
                var pdfPathsQueue = _pdfService.GetFilesQueue();
                if (pdfPathsQueue.Count > 0)
                {
                    foreach (var item in pdfPathsQueue)
                    {
                        CurrPdfPath = item;
                        Thread.Sleep(SecondsBetweenSlides * 1000);
                    }
                }
                else
                    Thread.Sleep(5000);
            }
        }
        private void ExitProgram()
        {
            Environment.Exit(0);
        }
    }
}
