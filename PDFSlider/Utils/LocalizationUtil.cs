using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Windows.Data;

namespace PDFSlider.Utils
{
    public class TranslationSource : INotifyPropertyChanged
    {
        public static TranslationSource Instance { get; } = new TranslationSource();

        private readonly ResourceManager resManager = Properties.Resources.ResourceManager;
        private CultureInfo currentCulture;

        public string this[string key] => resManager.GetString(key, currentCulture);

        public CultureInfo CurrentCulture
        {
            get => currentCulture;
            set
            {
                if (currentCulture != value)
                {
                    currentCulture = value;
                    var @event = PropertyChanged;
                    if (@event != null)
                    {
                        @event.Invoke(this, new PropertyChangedEventArgs(string.Empty));
                    }
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class LocExtension : Binding
    {
        public LocExtension(string name) : base("[" + name + "]")
        {
            Mode = BindingMode.OneWay;
            Source = TranslationSource.Instance;
        }
    }
}
