using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Models;
using Security.Models.DTOS;
using Security.Services;

namespace Security.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HospitalController:ControllerBase
    {
        private readonly IHospitalService _service;
        public HospitalController(IHospitalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHospitals()
        {
            IEnumerable<Hospital> items = await _service.GetAll();
            return Ok(items);
        }
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var hospital = await _service.GetOne(id);
            return Ok(hospital);
        }
        [HttpGet("type13")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllHospitalsType13()
        {
            IEnumerable<Hospital> items = await _service.GetAllHospitalsType13();
            return Ok(items);
        }
[HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateHospital([FromBody] CreateHospitalDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var hospital = await _service.CreateHospital(dto);
            return CreatedAtAction(nameof(GetOne), new { id = hospital.Id }, hospital);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task <IActionResult> UpdateHospital(Guid id, [FromBody] UpdateHospitalDto dto)
        {
            if(!ModelState.IsValid) return ValidationProblem(ModelState);
            var updated= await _service.UpdateHospital(id,dto);
            return Ok(updated);
        }
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task <IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteHospital(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
