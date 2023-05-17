using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDoctor(int id)
        {

           // var trips = await _tripsService.GetTrips2();
           //
            return Ok();
        }
    }
}
