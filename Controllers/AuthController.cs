using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Konusarak_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly string signinKey = "BuBenimUzunAnahtarBuKez1234567890";

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;

            // Anahtarın uzunluğunu kontrol etmeye gerek yok.
            // Anahtar her zaman 256 bit (32 byte) olmalı.
        }

        [HttpGet]
        public IActionResult Get(string userName, string password)
        {
            try
            {
                var claims = new[]{
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(JwtRegisteredClaimNames.Email, userName)
                };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey.PadRight(32))); // 32 byte (256 bit)
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: "https://www.melih.com",
                    audience: "BuBenimKullandigimAudienceDegeri",
                    claims: claims,
                    expires: DateTime.Now.AddDays(15),
                    notBefore: DateTime.Now,
                    signingCredentials: credentials
                );

                var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hata oluştu: {ex}");

                // Hata durumunda BadRequest dönebilirsiniz.
                return BadRequest($"Hata: {ex.Message}");
            }
        }

        [HttpGet("ValidateToken")]
        public bool ValidateToken(string token)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey.PadRight(32)));
                var handler = new JwtSecurityTokenHandler();

                handler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

// using System;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using Microsoft.IdentityModel.Tokens;

// namespace Konusarak_backend.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class AuthController : ControllerBase
//     {
//         private readonly ILogger<AuthController> _logger;

//         public AuthController(ILogger<AuthController> logger)
//         {
//             _logger = logger;
//         }

//         string signinKey = "BuBenimUzunAnahtarBuKez1234567890";
//         [HttpGet]
//         public IActionResult Get(string userName, string password)
//         {
//             try
//             {
//                 var claims = new[]{
//                     new Claim(ClaimTypes.Name, userName),
//                     new Claim(JwtRegisteredClaimNames.Email, userName)
//                 };

//                 if (Encoding.UTF8.GetBytes(signinKey).Length != 32)
//                 {
//                     // Anahtarın uzunluğunu elle kontrol et ve düzelt
//                     signinKey = "BuBenimUzunAnahtarBuKez12345678901234"; // Örnek olarak 256 bit'e çıkarılmış hali
//                 }

//                 var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey.PadRight(32))); // 32 byte (256 bit)
//                 var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);



//                 var jwtSecurityToken = new JwtSecurityToken(
//                     issuer: "https://www.melih.com",
//                     audience: "BuBenimKullandigimAudienceDegeri",
//                     claims: claims,
//                     expires: DateTime.Now.AddDays(15),
//                     notBefore: DateTime.Now,
//                     signingCredentials: credentials
//                 );

//                 var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
//                 return Ok(token);



//                 [HttpGet("ValidateToken")]
//                 public bool ValidateToken(string token)
//                 {
//                     var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey));
//                     try
//                     {
//                         JwtSecurityTokenHandler handler = new();
//                         handler.ValidateToken(token, new TokenValidationParameters()
//                         {
//                             ValidateIssuerSigningKey = true,
//                             IssuerSigningKey = securityKey,
//                             ValidateLifetime = true,
//                             ValidateAudience = false,
//                             ValidateIssuer = false

//                         }, out SecurityToken validatedToken);
//                         var jwtToken = (JwtSecurityToken)validatedToken;
//                         var claims = jwtToken.Claims.ToList();
//                         return true;

//                     }
//                     catch (System.Exception)
//                     {
//                         return false;
//                     }
//                 }
//             }
// }
//     }
