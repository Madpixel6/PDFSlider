using PDFSlider.ViewModels;

namespace PDFSlider.Utils
{
    public class ViewModelLocator
    {
        public OptionsWindowViewModel OptionsWindowViewModel
        {
            get => Bootstrap.Resolve<OptionsWindowViewModel>();
        }
        public MainWindowViewModel MainWindowViewModel
        {
            get => Bootstrap.Resolve<MainWindowViewModel>();
        }
    }
}
