namespace Singular.Web.Mvc.Context
{
    public class SiteContext : ISiteContext
    {
        private SiteContext()
        {
            BrandViewPath = "~/Views/Shared/_CoreBrand.cshtml";
            HomepageViewPath = "~/Views/Home/Index.cshtml";
            SiteName = "Singular Framework";
        }
        private static ISiteContext _current;
        public static ISiteContext Current
        {
            get
            {
                return _current ?? (_current = new SiteContext());
            }
        }

        public ISiteContext OverrideBrandView(string viewPath)
        {
            BrandViewPath = viewPath;
            return this;
        }

        public string BrandViewPath { get; private set; }

        public ISiteContext OverrideHomepageView(string viewPath)
        {
            HomepageViewPath = viewPath;
            return this;
        }

        public string HomepageViewPath { get; private set; }

        public ISiteContext SetSiteName(string name)
        {
            SiteName = name;
            return this;
        }

        public string SiteName { get; private set; }
    }
}
