using Singular.Core.Data.Enums;

namespace Singular.Web.Mvc.Context
{
    public interface ISingularMvcAuthenticationContext
    {
        AuthenticationType AuthenticationType { get; } 
    }
}