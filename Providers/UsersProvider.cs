using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Common.Enums;
using WebApi.Data.Entities;
using WebApi.Data.Interface.Repositories;
using WebApi.Interfaces.Providers;
using WebApi.Models.UserModel;

namespace WebApi.Providers
{
    public class UsersProvider : IUsersProvider
    {
        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepository;

        public UsersProvider(IMapper mapper, IUsersRepository usersRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
        }

        public async Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _usersRepository.GetAllAsync(ct).ConfigureAwait(false);

            return _mapper.Map<IReadOnlyCollection<User>>(entities);
        }

        public async Task<IReadOnlyCollection<User>> GetAsync(string? orderBy, OrderDirection order, CancellationToken ct = default)
        {
            var entities = await _usersRepository.GetAsync(orderBy, order, ct).ConfigureAwait(false);

            return _mapper.Map<IReadOnlyCollection<User>>(entities);
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _usersRepository.GetByIdAsync(id, ct).ConfigureAwait(false);

            return _mapper.Map<User?>(entity);
        }

        public async Task<User> CreateAsync(User model, CancellationToken ct = default)
        {
            var entity = _mapper.Map<UserEntity>(model);

            entity = await _usersRepository.CreateAsync(entity, ct).ConfigureAwait(false);

            return _mapper.Map<User>(entity);
        }

        public async Task<User> UpdateAsync(User model, CancellationToken ct = default)
        {
            var entity = _mapper.Map<UserEntity>(model);

            entity = await _usersRepository.UpdateAsync(entity, ct).ConfigureAwait(false);

            return _mapper.Map<User>(entity);
        }

        public Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            return _usersRepository.DeleteAsync(id, ct);
        }

        public async Task<User?> GetByNameAndPasswordAsync(string? Name, string? Password, CancellationToken ct = default)
        {
            var entity = await _usersRepository.GetByNameAndPasswordAsync(Name, Password, ct).ConfigureAwait(false);

            return _mapper.Map<User?>(entity);
        }
    }
}
