namespace Singular.Modules.Core.Data.Configuration
{
    public class SingularFormsAuthenticationConfiguration
    {
        public SingularFormsAuthenticationConfiguration()
        {
            NumberOfAllowedFailedPasswordAttempts = 10;
            RegistrationMustBeApproved = true;
        }
        public bool RegistrationMustBeApproved { get; set; }
        public int NumberOfAllowedFailedPasswordAttempts { get; set; }
    }
}