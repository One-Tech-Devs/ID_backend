using ID_model.DTOs;
using ID_model.Models;
using ID_repository.Data;
using ID_service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ID_service.Services
{
    public class LoginService : ILoginService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly Generator _generator;


        public LoginService(DataContext context, IConfiguration configuration, Generator generator)
        {
            _context = context;
            _configuration = configuration;
            _generator = generator;
        }

        public async Task<string> LoginClient(LoginDTO request)
        {
            
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return null;
            }
            var user = _context.Clients.FirstOrDefault(u => u.Username == request.Username);

            // Se o usuário não existir ou a senha estiver incorreta, retorne null
            if (user == null || !VerifyPassword(request.Password, user.Password, user.Key, user.Iv))
            {
                return null;
            }

            return GenerateJwtToken(user);
        }
        public async Task<string> LoginCompany(LoginDTO request)
        {

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return null;
            }
            var user = _context.Companies.FirstOrDefault(u => u.Username == request.Username);

            // Se o usuário não existir ou a senha estiver incorreta, retorne null
            if (user == null || !VerifyPassword(request.Password, user.Password, user.Key, user.Iv))
            {
                return null;
            }

            return GenerateJwtToken(user);
        }
        private bool VerifyPassword(string inputPassword, byte[] storedPassword, byte[] key, byte[] iv)
        {
            string criptPassword = Encryption.Decrypt(storedPassword, key , iv);

            return inputPassword == criptPassword;
        }

        private string GenerateJwtToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtSettings:DurationInDays"]));

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
