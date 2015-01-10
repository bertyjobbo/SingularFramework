using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Singular.Core.Context;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.ViewModels
{
    public class FormsAuthViewModel : CoreViewModelBase
    {
        public FormsAuthViewModel(ISingularContext ctx, ISectionManager mvcManager, ISiteContext siteContext) : base(ctx, mvcManager, siteContext)
        {
        }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}