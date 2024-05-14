using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;
        public DoctorController(IHospitalService Service)
        {
            _hospitalService = Service;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetDoctor()
        {

            var res = await _hospitalService.GetDoctor();
           
            return Ok(res);
        }
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {

            var res = await _hospitalService.DeleteDoctor(id);

            return Ok(res);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostDoctor(DoctorToAdd doctor)
        {

            var res = await _hospitalService.PostDoctor(doctor);

            return Ok(res);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> PutDoctor(DoctorToAdd doctor)
        {

            var res = await _hospitalService.PutDoctor(doctor);

            return Ok(res);
        }
    }
}
