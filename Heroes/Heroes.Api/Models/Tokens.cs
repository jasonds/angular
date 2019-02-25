namespace Heroes.Api.Models
{
    public class Tokens
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Key { get; set; }
    }
}