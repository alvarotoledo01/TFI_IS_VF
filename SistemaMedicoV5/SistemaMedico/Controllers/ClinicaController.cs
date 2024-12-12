using SistemaMedico.Services;
using SistemaMedico.Models;
using SistemaMedicoV3.Models;
using Microsoft.AspNetCore.Mvc;
using SistemaMedico.Repository;

namespace SistemaMedico.Controllers
{
    public class ClinicaController : Controller
    {
        private PacienteService _service;
        private ApiResponseService _apiService;
        public ClinicaController(PacienteService pacienteService, ApiResponseService apiService)
        {
            _service = pacienteService;
            _apiService = apiService;
        }

        public void AgregarEvolucion(int dni, Guid idDiagnostico, string informe, string nombreCompletoMedico)
        {
            // Recuperar el nombre del médico desde la sesión
            //var nombreCompletoMedico = HttpContext.Session.GetString("MedicoNombreCompleto");
            // es mejor hacerlo desde la accion

            //if (string.IsNullOrEmpty(nombreCompletoMedico))
            //{
            //    throw new Exception("Sesión no válida. Inicie sesión nuevamente.");
            //}
            // ver esto porque habria que modificar los tests
            var paciente = _service.BuscarPorDni(dni);

            if (paciente == null)
            {
                throw new Exception($"Paciente con DNI {dni} no encontrado");
            }

            if (string.IsNullOrEmpty(informe))
            {
                throw new ArgumentException();
            }

            paciente.AgregarEvolucion(idDiagnostico, informe, nombreCompletoMedico);
        }

        public void AgregarDiagnostico(int dni, string enfermedad)
        {
            var paciente = _service.BuscarPorDni(dni);

            if (paciente == null)
            {
                throw new Exception($"Paciente con DNI {dni} no encontrado");
            }

            paciente.AgregarDiagnostico(enfermedad);
        }

        public void AgregarPedidoLaboratorio(int dni, Guid idDiagnostico, Guid idEvolucion, string PlantillaPedido)
        {
            var paciente = _service.BuscarPorDni(dni);

            if (paciente == null)
            {
                throw new Exception($"Paciente con DNI {dni} no encontrado");
            }

            paciente.AgregarPedidoLaboratorio(idDiagnostico, idEvolucion, PlantillaPedido);
        }

        public async Task CrearRecetaDigital(int dni, Guid idDiagnostico, Guid idEvolucion, string nombreMedicamento, string obraSocial )
        {
            var paciente = _service.BuscarPorDni(dni);

            if (paciente == null) throw new Exception($"Paciente con DNI {dni} no encontrado");

            var medicamentovalido = await _apiService.ValidarMedicamento(nombreMedicamento);

            if (medicamentovalido == null) throw new Exception("El medicamento ingresado no es válido.");

            var obraSocialValida = await _apiService.ValidarObraSocial(obraSocial);

            if (obraSocialValida == null) throw new Exception("La Obra Social ingresada no es válida.");

            paciente.CrearRecetaDigital(idDiagnostico, idEvolucion, medicamentovalido.Descripcion, medicamentovalido.Codigo, paciente.NombreCompleto, paciente.NroAfiliado, obraSocialValida.Denominacion);

        }
        [HttpGet]
        public IActionResult Evolucion(int dni)
        {
            var paciente = _service.BuscarPorDni(dni);
            if (paciente == null)
            {
                return NotFound($"Paciente con DNI {dni} no encontrado.");
            }

            return View("~/Views/Paciente/Evolucion.cshtml", paciente); // Carga la vista Evolucion.cshtml con el modelo del paciente
        }

        

        

       

    }

} 
