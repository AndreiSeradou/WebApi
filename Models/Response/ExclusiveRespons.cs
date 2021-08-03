namespace WebApi.Models.Response
{
    public class ExclusiveRespons
    {
        public string Name { get; set; }
        public string Token { get; set; }

        public ExclusiveRespons(string user, string token)
        {
            Name = user;
            Token = token;
        }
    }
}
