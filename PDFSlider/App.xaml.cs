using PDFSlider.Services;
using PDFSlider.View;
using System.Windows;

namespace PDFSlider
{
    public partial class App : Application
    {
        public void AppStart(object sender, StartupEventArgs args)
        {
            if (CfgService.ReadPropertyParseBool("showCfgWndOnStartup"))
            {
                var optionsWindow = new OptionsWindow();
                optionsWindow?.ShowDialog();
            }
            else
            {
                var mainWindow = new MainWindow();
                mainWindow?.Show();
            }

        }
    }
}
