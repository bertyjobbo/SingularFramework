using System.Configuration;
using Singular.Core.Data.Enums;

namespace Singular.Web.Mvc.Context
{
    public class SingularMvcAuthenticationContext : ISingularMvcAuthenticationContext
    {
        private SingularMvcAuthenticationContext()
        {
            var configSetting = ConfigurationManager.AppSettings["SingularAuthenticationType"];
            switch (configSetting)
            {
                case "ActiveDirectorty": AuthenticationType = AuthenticationType.ActiveDirectory;
                    break;
                case "Domain": AuthenticationType= AuthenticationType.Domain;
                    break;
                default: AuthenticationType = AuthenticationType.Forms;
                    break;
            }
        }

        private static ISingularMvcAuthenticationContext _current;
        public static ISingularMvcAuthenticationContext Current
        {
            get
            {
                return _current ?? (_current= new SingularMvcAuthenticationContext());
            }
        }

        public AuthenticationType AuthenticationType { get; private set; }
    }
}