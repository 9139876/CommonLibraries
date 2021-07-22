
using CommonLibraries.RemoteCall.Enums;

namespace CommonLibraries.RemoteCall.Models
{
    public class Credentials
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public CredentialsTypeEnum Type { get; set; }

        public AdditionalCredential AdditionalCredential { get; set; }
    }
}
