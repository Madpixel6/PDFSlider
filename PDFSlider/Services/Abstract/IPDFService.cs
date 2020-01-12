using PDFSlider.Services.Abstract;

namespace PDFSlider.Services
{
    public interface IPdfService : IService
    {
        string CurrentPdfPath { get; set; }
        void Run();
    }
}
