namespace PDFSlider.Services
{
    interface IPdfService
    {
        string CurrentPdfPath { get; set; }
        void Run();
    }
}
