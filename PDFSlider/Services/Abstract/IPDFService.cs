using PDFSlider.Services.Abstract;
using System.Collections.Generic;

namespace PDFSlider.Services
{
    public interface IPdfService : IService
    {
        Queue<string> GetFilesQueue();
        void Initialize();
    }
}
