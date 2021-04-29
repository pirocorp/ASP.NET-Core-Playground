namespace UsingDifferentEnvironments.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Options;

    public class IndexModel : PageModel
    {
        public MyValues Values { get; }

        public IndexModel(IOptions<MyValues> values)
        {
            this.Values = values.Value;
        }

        public void OnGet()
        {

        }
    }
}
