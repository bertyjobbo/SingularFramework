using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Singular.Core.Context;
using Singular.Modules.Core.Data.Services;
using Singular.Modules.Core.HtmlExtensions;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.ViewModels
{
    public class FormsAuthViewModel : CoreViewModelBase
    {
        public FormsAuthViewModel(ISingularContext ctx) : base(ctx)
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

        public IList<SelectListItem> GetYesNoList()
        {
            return new List<SelectListItem>(2)
            {
                new SelectListItem
                {
                    Selected = false,
                    Text = TranslationService.GetTranslation("Yes"),
                    Value = "true"
                },
                new SelectListItem
                {
                    Selected = true,
                    Text = TranslationService.GetTranslation("No"),
                    Value = "false"
                }
            };
        }
    }
}