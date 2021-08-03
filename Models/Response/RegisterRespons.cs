using WebApi.Models.Request;

namespace WebApi.Models.Response
{
    public class RegisterResponse
    {
        public string Name { get; set; }

        public RegisterResponse(RegisterRequest model)
        {
            Name = model.Name;
        }
    }
}
