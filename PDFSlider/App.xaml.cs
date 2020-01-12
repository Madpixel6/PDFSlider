using NLog;
using PDFSlider.Services;
using PDFSlider.View;
using System;
using System.Windows;
using System.Windows.Threading;

namespace PDFSlider
{
    public partial class App : Application
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public void AppStart(object sender, StartupEventArgs args)
        {
            Bootstrap.Build();
            if (CfgService.ReadPropertyParseBool("showCfgWndOnStartup"))
            {
                Window optionsWindow = new OptionsWindow();
                optionsWindow?.ShowDialog();
            }
            else
            {
                Window mainWindow = new MainWindow();
                mainWindow?.Show();
            }

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            // add handlers for unhandled exceptions
            AppDomain.CurrentDomain.UnhandledException += DomainExceptionHandler;
            Current.DispatcherUnhandledException += DispatcherExceptionHandler;

            base.OnStartup(e);
        }
        #region Exception handlers
        private void DomainExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Logger.Info("App domain exception occured");
            HandleInternalException(args.ExceptionObject as Exception);
        }

        private void DispatcherExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            args.Handled = true;
            Logger.Info("Dispatcher thread exception occured");
            HandleInternalException(args.Exception);
        }

        private void HandleInternalException(Exception ex)
        {
            Logger.Error(ex);
        }
        #endregion
    }
}
