using System;
using System.Data.Entity;
using Singular.Core.Authentication;
using Singular.Core.Data.DataContext;
using Singular.Core.Data.Entities;
using Singular.Core.Data.Enums;
using Singular.Core.Encryption;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;

namespace MySingularApplication
{
    public class Global : Singular.Web.Mvc.Application.SingularMvcApplication
    {
        protected override void SingularApplicationStart(object sender, EventArgs eventArgs)
        {
            // set embed path
            EmbeddedResourceManager
                .Current
                .SetMsBuildPath(@"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe")
                .SetSolutionPath(@"C:\ffd248e91f0c43bbbe5921e4\Git\SingularFramework\Singular.sln")
                .SetRazorGeneratorPsScriptPath(@"C:\ffd248e91f0c43bbbe5921e4\Git\SingularFramework\packages\RazorGenerator.Mvc.2.2.3\tools\Init.ps1");

            // initialize db
            Database.SetInitializer(new MyInitializer());
            var ctx = new SingularEntityFrameworkContext();
            ctx.Database.Initialize(true);

            // base go!
            base.SingularApplicationStart(sender, eventArgs);
        }
    }

    public class MyInitializer: DropCreateDatabaseIfModelChanges<SingularEntityFrameworkContext>
    {
        protected override void Seed(SingularEntityFrameworkContext context)
        {
            
            var user1 = new SingularUser
            {
                Email = "robjohnson1978@gmail.com", 
                IsActive = true,
                AuthenticationType = AuthenticationType.Forms,
                Created = DateTime.UtcNow,
                CreatedBy = null, Name = "Rob Johnson",
                IsSuperUser = true
            };

            var helper = new EncryptionHelper();
            var password = "robjohnson1978@gmail.com";
            var hashedPassword = helper.EncryptToString(password);
            user1.EncryptedFormsAuthPassword = hashedPassword;

            var user2 = new SingularUser
            {
                Email = "robjohnsondeveloper@gmail.com",
                IsActive = true,
                AuthenticationType = AuthenticationType.Forms,
                Created = DateTime.UtcNow,
                CreatedBy = null,
                Name = "Rob Johnson Dev"
            };

            password = "robjohnsondeveloper@gmail.com";
            hashedPassword = helper.EncryptToString(password);
            user2.EncryptedFormsAuthPassword = hashedPassword;

            // add
            context.SingularUsers.Add(user1);
            context.SingularUsers.Add(user2);
            context.SaveChanges();
        }
    }
}