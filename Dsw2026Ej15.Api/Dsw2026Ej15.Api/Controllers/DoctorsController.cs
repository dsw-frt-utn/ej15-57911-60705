using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateDoctor([FromBody]DoctorModel.Request request )
        {
            if (string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.LicenseNumber))
                {
                return BadRequest("Nombre y matricula son requeridos");
            }

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if(speciality == null)
            {
                return BadRequest("La especialidad no existe");
            }
            return Created();
        }
       
    }
}
