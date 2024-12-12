using SistemaMedicoV3.Models;

namespace SistemaMedico.Services
{
    public class RecetaDigitalService
    {
        private readonly ApiResponseService _apiService;
        private readonly List<RecetaDigital> _recetas = new List<RecetaDigital>();

        public RecetaDigitalService()
        {
            _apiService = new ApiResponseService();
        }

    }
}
