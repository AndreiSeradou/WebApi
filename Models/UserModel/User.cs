using System.Text.Json.Serialization;

namespace WebApi.Models.UserModel
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public string? HashPassword { get; set; }
    }
}
