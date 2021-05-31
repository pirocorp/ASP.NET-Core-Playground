namespace ReplacingDefaultConfigurationProviders.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Configuration;

    public class IndexModel : PageModel
    {
        public IConfiguration Config { get; }

        public IndexModel(IConfiguration config)
        {
            this.Config = config;
        }

        public void OnGet()
        {

        }
    }
}
