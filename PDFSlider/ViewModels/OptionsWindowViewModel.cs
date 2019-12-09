using PDFSlider.Services;
using System;
using System.Windows;
using System.Windows.Input;

namespace PDFSlider.ViewModels
{
    public class OptionsWindowViewModel : ViewModelBase
    {

        #region Commands
        public ICommand OKCommand { get; private set; }
        public ICommand ChooseDirCommand { get; private set; }
        #endregion
        #region Fields
        public bool IsCtrlEnabled { get; set; } = true;
        public int SecondsBetweenSlides { get; set; }
        private string pdfDirectoryPath;
        public string PdfDirectoryPath
        {
            get => pdfDirectoryPath;
            set
            {
                pdfDirectoryPath = value;
                OnPropertyChanged("PdfDirectoryPath");
            }

        }
        #endregion
        public OptionsWindowViewModel()
        {
            try
            {
                SecondsBetweenSlides = CfgService.ReadPropertyParseInt("secondsBetweenSlides");
                PdfDirectoryPath = CfgService.ReadProperty("selectedDirPath");
            }
            catch (Exception)
            {
                IsCtrlEnabled = false;
                SecondsBetweenSlides = 30; //default value
            }
            InitCommands();
        }

        private void InitCommands()
        {
            OKCommand = new RelayCommand(wndObj => OK_CloseCurrentOpenMain(wndObj), _ => true);
            ChooseDirCommand = new RelayCommand(obj => ChooseDir_Method(), o => true);
        }


        #region Command methods
        private void OK_CloseCurrentOpenMain(object wnd)
        {
            CfgService.SaveProperty("selectedDirPath", PdfDirectoryPath);

            ViewService.ShowWindow<MainWindow>();
            if (wnd != null && wnd is Window)
                ViewService.CloseWindow<Window>((Window)wnd);
        }
        private void ChooseDir_Method()
        {
            using (var fBD = new System.Windows.Forms.FolderBrowserDialog())
            {
                var result = fBD.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fBD.SelectedPath))
                {
                    PdfDirectoryPath = fBD.SelectedPath;
                }
            }
        }
        #endregion
    }
}
