using System.Threading.Tasks;
using WebApi.Models.Request;
using WebApi.Models.Response;
using WebApi.Models.UserModel;

namespace WebApi.Interfaces.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model);
        Task<RegisterResponse> Register(RegisterRequest model);
        Task<User> GetById(int id);
    }
}
