using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/doctors")] //utiliza como ruta en el endpoint 
    public class DoctorController : ControllerBase
    {
        private readonly IPersistence _persistence;
        public DoctorController(IPersistence persistence)
        {
            _persistence = persistence;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorModel.Request request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                //return BadRequest("Nombre y matricula son requeridos"); se cambia esto?
                throw new ValidationException("Nombre y matrícula son requeridos");
            }

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality == null)
            {
                //return BadRequest("La especialidad no existe");
                throw new ValidationException("La especialidad no existe");
            }

            var doctor = new Doctor(
            request.Name,
            request.LicenseNumber,
            speciality);

            _persistence.AddDoctor(doctor);

            return Created();
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            var doctors = _persistence.GetDoctors()
                .Where(d => d.IsActive);


            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public IActionResult GetDoctorById(Guid id)
        {
            var doctor = _persistence.GetDoctors().SingleOrDefault(d => d.Id == id && d.IsActive);

            if (doctor == null || !doctor.IsActive)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(Guid id)
        {
            var doctor = _persistence.GetDoctors()
                .SingleOrDefault(d => d.Id == id && d.IsActive);

            if (doctor == null)
            {
                return NotFound();
            }

            doctor.Deactivate();

            return NoContent();
        }

    }
}
