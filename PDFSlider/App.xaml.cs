using PDFSlider.Services;
using PDFSlider.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;

namespace PDFSlider
{
    public partial class App : Application
    {
        public void AppStart(object sender, StartupEventArgs args)
        {
            Localize();
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
        private void Localize()
        {
           // List<ResourceDictionary> resourceDictionaries = LoadLangComponents();
        }
        // Returns a list of language components
        private List<ResourceDictionary> LoadLangComponents()
        {
            var langObjects = new List<ResourceDictionary>();
            var langFiles = Directory.GetFiles("Resources/","*Lang.xaml");
            if(langFiles.Length > 0)
            {
                var uriPaths = new List<Uri>();
                for (var i = 0; i < langFiles.Length; i++)
                {
                    var uriObj = new Uri(langFiles[i], UriKind.Relative);
                    uriPaths.Add(uriObj);
                }
                foreach (var item in uriPaths)
                    langObjects.Add((ResourceDictionary)LoadComponent(item));
            }
            return langObjects;
        }
    }
}
