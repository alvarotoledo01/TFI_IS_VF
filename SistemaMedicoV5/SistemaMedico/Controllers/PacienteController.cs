﻿using Microsoft.AspNetCore.Mvc;
using SistemaMedico.Services;

namespace SistemaMedico.Controllers
{
    public class PacienteController : Controller
    {
        private readonly PacienteService _pacienteService;

        public PacienteController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        public IActionResult Detalles(int id)
        {
            var paciente = _pacienteService.BuscarPorDni(id);
            if (paciente == null)
            {
                return NotFound(); // Manejo de error si no se encuentra el paciente
            }
            return View(paciente); // Pasar el paciente como modelo a la vista
        }
       
    
}
}
