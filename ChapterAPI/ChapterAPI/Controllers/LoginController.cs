using ChapterAPI.Interfaces;
using ChapterAPI.Models;
using ChapterAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChapterAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _iUsuarioRepository;
        public LoginController(IUsuarioRepository iUsuarioRepository) 
        {
            _iUsuarioRepository = iUsuarioRepository;

        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login) 
        {
            try
            {
                Usuario UsuarioBuscado = _iUsuarioRepository.Login(login.email, login.senha);

                if (UsuarioBuscado == null) 
                {
                    return Unauthorized(new { msg = "Email e/ou Senha inválidos" });
                }
                // define o dados fornecidos no token no payload
                var minhasClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, UsuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, UsuarioBuscado.Id.ToString()),
                    new Claim(ClaimTypes.Role, UsuarioBuscado.Tipo)
                };
                // definde chave de acesso ao token
                var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chave-autentificacao"));
                //define as credenciais do token(header)
                var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
                // gera o token
                var meuToken = new JwtSecurityToken(
                    issuer: "Chapter",
                    audience: "Chapter",
                    claims: minhasClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credenciais
                    );
                return Ok(
                        new { token = new JwtSecurityTokenHandler().WriteToken(meuToken) }
                    );
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        
        }
    }
}
