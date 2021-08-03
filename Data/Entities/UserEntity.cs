using WebApi.Data.Entities.Base;

namespace WebApi.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string HashPassword { get; set; } = string.Empty;
    }
}
