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
        public int secondsBetweenSlides;
        public int SecondsBetweenSlides
        {
            get => secondsBetweenSlides;
            set
            {
                if(secondsBetweenSlides != value)
                {
                    secondsBetweenSlides = value;
                    OnPropertyChanged(nameof(SecondsBetweenSlides));
                }
            }
        }
        private string pdfDirectoryPath;
        public string PdfDirectoryPath
        {
            get => pdfDirectoryPath;
            set
            {
                if(pdfDirectoryPath != value)
                {
                    pdfDirectoryPath = value;
                    OnPropertyChanged(nameof(PdfDirectoryPath));
                }
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
            OKCommand = new RelayCommand(OK_CloseCurrentOpenMain, _ => true);
            ChooseDirCommand = new RelayCommand(_ => ChooseDir_Method(), _ => true);
        }


        #region Command methods
        private void OK_CloseCurrentOpenMain(object wnd)
        {
            if (IsDirPathValid())
            {
                CfgService.SaveProperty("selectedDirPath", PdfDirectoryPath);

                ViewService.ShowWindow<MainWindow>();
                if (wnd != null && wnd is Window)
                    ViewService.CloseWindow((Window)wnd);
            }
            else
                throw new Exception("Provided directory path is invalid!"); // TODO: localize, add custom exceptions
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

        private bool IsDirPathValid()
        {
            if(string.IsNullOrEmpty(PdfDirectoryPath))
                return false;
            return true;
        }
    }
}
