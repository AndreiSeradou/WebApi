using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data.Interface.Repositories;
using WebApi.Helpers;
using WebApi.Interfaces.Providers;
using WebApi.Interfaces.Services;
using WebApi.Models.Request;
using WebApi.Models.Response;
using WebApi.Models.UserModel;

namespace WebApi.Services
{


    public class UserService : IUserService
    {
        private IUsersProvider _userProvider;
        private IUsersRepository _usersRepository;
        private readonly AppSettings _appSettings;
        

        public UserService(IOptions<AppSettings> appSettings, IUsersProvider userProvider, IUsersRepository usersRepository)
        {
            _appSettings = appSettings.Value;
            _userProvider = userProvider;
            _usersRepository = usersRepository;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
        {
            var user = await _userProvider.GetByNameAndPasswordAsync(model.Name, model.Password).ConfigureAwait(false);
           
            if (user == null) return null;

            var token = generateJwtToken(new User { Id = user.Id,  Name = user.Name, HashPassword = user.HashPassword });

            return new AuthenticateResponse(new User {Id = user.Id, Name = user.Name, HashPassword = user.HashPassword }, token);
        }

        public async Task<RegisterResponse> Register(RegisterRequest model)
        {
            await _userProvider.CreateAsync(new User { Name = model.Name, HashPassword = model.Password }).ConfigureAwait(false);
            
            return new RegisterResponse(model);
        }

        public async Task<User> GetById(int id)
        {
            return new User { Name = (await _userProvider.GetByIdAsync(id)).Name, HashPassword = (await _userProvider.GetByIdAsync(id)).HashPassword };
        }


        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}