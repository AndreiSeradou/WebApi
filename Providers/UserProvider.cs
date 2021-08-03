using System.Threading.Tasks;
using WebApi.Data.Entities;
using WebApi.Data.Interface.Repositories;
using WebApi.Interfaces.Providers;
using WebApi.Models.UserModel;

namespace WebApi.Providers
{
    public class UserProvider : IUserProvider
    {
        private IUsersRepository _usersRepository;
        public UserProvider(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }


        public async Task<User?> GetByIdAsync(int id)
        {
            var entity = await _usersRepository.GetByIdAsync(id).ConfigureAwait(false);

            return new User { Name = entity.Name, Password = entity.HashPassword };
        }

        public async Task<User?> GetByNameAndPasswordAsync(string Name, string Password)
        {
            var entity = await _usersRepository.GetByNameAndPasswordAsync(Name, Password).ConfigureAwait(false);

            return new User { Name = entity.Name, Password = entity.HashPassword };
        }

        public User Create(User entity)
        {
            _usersRepository.CreateAsync(new UserEntity { Name = entity.Name, HashPassword = entity.Password });

            return entity;
        }

        public async Task<User> UpdateAsync(User model)
        {
            var entity = await _usersRepository.UpdateAsync(new UserEntity { Name = model.Name, HashPassword = model.Password }).ConfigureAwait(false);

            return model;

        }
    }
}
