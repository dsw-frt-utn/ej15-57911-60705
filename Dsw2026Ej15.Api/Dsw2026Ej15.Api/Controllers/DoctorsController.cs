using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Domain.Exceptions;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IPersistence _persistence;
        public DoctorController(IPersistence persistence)
        {
            _persistence = persistence;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                throw new ValidationException("Nombre y matrícula son requeridos");
            }

            var speciality = await _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality is null)
            {
                throw new ValidationException("La especialidad no existe");
            }

            var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
            await _persistence.SaveDoctor(doctor);
            return Created();
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _persistence.GetAllDoctors();
            return Ok(doctors.Select(d => new DoctorModel.Response(d.Name,
                d.LicenseNumber, d.Speciality?.Name)));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(Guid id)
        {
            var doctor = await _persistence.GetDoctorById(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(new DoctorModel.Response(doctor.Name, doctor.LicenseNumber,
                  doctor.Speciality?.Name));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var doctor = await _persistence.GetDoctorById(id);


            if (doctor == null)
            {
                return NotFound();
            }

            doctor.Deactivate();
            await _persistence.UpdateDoctor(doctor);

            return NoContent();
        }

    }
}
