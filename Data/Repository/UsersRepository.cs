using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Common.Enums;
using WebApi.Data.DbContext;
using WebApi.Data.Entities;
using WebApi.Data.Interface.Repositories;

namespace WebApi.Data.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserDbContext _dbContext;

        public UsersRepository(UserDbContext dbContext)

        {
            _dbContext = dbContext;
        }

        public UserEntity Create(UserEntity entity)
        {
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public async Task<UserEntity> CreateAsync(UserEntity entity, CancellationToken ct = default)
        {
            var entityEntry = await _dbContext.Users.AddAsync(entity, ct).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
            return entityEntry.Entity;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await GetByIdAsync(id, ct).ConfigureAwait(false);
            if (entity != null)
            {
                var entityEntry = _dbContext.Users.Remove(entity);
                await _dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
                return entityEntry != null;
            }

            return false;
        }

        public async Task<IReadOnlyCollection<UserEntity>> GetAllAsync(CancellationToken ct = default)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .ToListAsync(ct)
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<UserEntity>> GetAsync(string? orderBy, OrderDirection order, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return await GetAllAsync(ct).ConfigureAwait(false);
            }

            Expression<Func<UserEntity, object>> orderByExp;

            orderBy = orderBy.ToUpper(CultureInfo.CurrentCulture);

#pragma warning disable CA1304 // Specify CultureInfo
            if (orderBy == nameof(UserEntity.Name).ToUpper())
#pragma warning restore CA1304 // Specify CultureInfo
            {
                orderByExp = entity => entity.Name;
            }
#pragma warning disable CA1304 // Specify CultureInfo
            else if (orderBy == nameof(UserEntity.HashPassword).ToUpper())
#pragma warning restore CA1304 // Specify CultureInfo
            {
                orderByExp = entity => entity.HashPassword;
            }
            else
            {
                return await GetAllAsync(ct).ConfigureAwait(false);
            }

            if (order == OrderDirection.Asc)
            {
                return await _dbContext.Users.OrderBy(orderByExp).ToListAsync(ct).ConfigureAwait(false);
            }

            return await _dbContext.Users.OrderByDescending(orderByExp).ToListAsync(ct).ConfigureAwait(false);
        }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public Task<UserEntity> GetByIdAsync(int id, CancellationToken ct = default)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        {
            return _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == id, ct)!;
        }

        public UserEntity GetByNameAndPassword(string? Name, string? Password)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Name == Name && user.HashPassword == Password)!;
        }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public async Task<UserEntity> GetByNameAndPasswordAsync(string Name, string Password, CancellationToken ct = default)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        {
            return await _dbContext.Users
               .AsNoTracking()
               .FirstOrDefaultAsync(user => user.Name == Name && user.HashPassword == Password, ct).ConfigureAwait(false)!;
        }

        public async Task<UserEntity> UpdateAsync(UserEntity entity, CancellationToken ct = default)
        {
            var entityEntry = _dbContext.Users.Update(entity);
            _ = await _dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
            return entityEntry.Entity;
        }
    }
}
