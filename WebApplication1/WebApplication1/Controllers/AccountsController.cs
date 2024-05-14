using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IHospitalService _hospitalService;
        public AccountsController(IHospitalService Service, IConfiguration config)
        {
            _hospitalService = Service;
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationRequest regreq)
        {
            var res = await _hospitalService.Register(regreq);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest logreq)
        {

            var val = await _hospitalService.Login(logreq);

            if (val!=null) { 
            var secret = _config["JWT:Secret"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(0.5),
                signingCredentials: creds
                );

           

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);// to nam daje bez secreta wszystko
                //var jwt2 = new JwtSecurityTokenHandler().WriteToken(refreshToken);

                return Ok(new
                {
                    token = jwt,
                    refreshToken = val
                });
            }

            //user.RefreshToken = refreshToken;
            //_context.SaveChanges();




            //var res = await  _hospitalService.Login(logreq);//dorobic\
            // tutaj wydajemy nowy token




            return NotFound("Incorrect data");

           


            //return Ok(jwt, refreshToken);
        }


        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(string refreshToken)
        {
           
            var doesExist = await _hospitalService.Check(refreshToken);


            if (doesExist!=null)
            {
                var secret = _config["JWT:Secret"];
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var tokenTemp = new JwtSecurityToken(
                    issuer: _config["JWT:Issuer"],
                    audience: _config["JWT:Audience"],
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: creds
                    );
                var token = new JwtSecurityTokenHandler().WriteToken(tokenTemp);
                return Ok(new
                {
                    token = token,
                    refreshToken = doesExist
                });
            }
            return  NotFound("Refresh token not found");
        }

        }
}
