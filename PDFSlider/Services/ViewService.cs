using System.Windows;

namespace PDFSlider.Services
{
    public static class ViewService
    {
        public static void ShowWindow<T>() where T : Window, new()
        {
            T wnd = new T();
            wnd.Show();
        }
        public static void CloseWindow<T>(T window) where T : Window
        {
            if (window.IsLoaded)
                window.Close();
        }
    }
}
