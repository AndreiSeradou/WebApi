using System.Threading.Tasks;
using WebApi.Models.UserModel;

namespace WebApi.Interfaces.Providers
{
    public interface IUserProvider
    {
        User Create(User entity);
        Task<User> UpdateAsync(User entity);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByNameAndPasswordAsync(string Name, string Password); 
    }
}
