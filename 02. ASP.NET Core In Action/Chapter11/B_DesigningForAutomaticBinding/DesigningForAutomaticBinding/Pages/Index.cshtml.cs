namespace DesigningForAutomaticBinding.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Options;

    public class IndexModel : PageModel
    {
        public IndexModel(IOptions<TestOptions> options)
        {
            this.Options = options.Value;
        }

        public TestOptions Options { get; }

        public void OnGet()
        {

        }
    }
}
