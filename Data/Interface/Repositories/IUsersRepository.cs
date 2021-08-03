using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Common.Enums;
using WebApi.Data.Entities;

namespace WebApi.Data.Interface.Repositories
{
    public interface IUsersRepository
    {
        Task<IReadOnlyCollection<UserEntity>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyCollection<UserEntity>> GetAsync(string? orderBy, OrderDirection order, CancellationToken ct = default);
        Task<UserEntity?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<UserEntity> CreateAsync(UserEntity entity, CancellationToken ct = default);
        Task<UserEntity> UpdateAsync(UserEntity entity, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
        Task<UserEntity?> GetByNameAndPasswordAsync(string? Name, string? Password, CancellationToken ct = default);
    }
}
