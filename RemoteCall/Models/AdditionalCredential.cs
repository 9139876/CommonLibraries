using CommonLibraries.RemoteCall.Enums;

namespace CommonLibraries.RemoteCall.Models
{
    public class AdditionalCredential
    {
        public string HeaderKey { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public CredentialsTypeEnum Type { get; set; }
    }
}
