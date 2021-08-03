using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Common.Enums;
using WebApi.Data.Entities;
using WebApi.Models.UserModel;

namespace WebApi.Interfaces.Providers
{
    public interface IUsersProvider
    {
        Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyCollection<User>> GetAsync(string? orderBy, OrderDirection order, CancellationToken ct = default);
        Task<User?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<User> CreateAsync(User entity, CancellationToken ct = default);
        Task<User> UpdateAsync(User entity, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
        Task<User?> GetByNameAndPasswordAsync(string? Name, string? Password, CancellationToken ct = default);
    }
}

