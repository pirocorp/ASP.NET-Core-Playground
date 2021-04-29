namespace StoreViewerApplication.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Options;

    using System.Collections.Generic;

    public class IndexModel : PageModel
    {
        public List<Store> Stores { get; }

        public MapSettings MapSettings { get; }

        public AppDisplaySettings AppSettings { get; }

        // With IOptions you can’t use the reloadOnChange, but with IOptionsSnapshot you can
        public IndexModel(
            IOptions<List<Store>> stores,
            IOptions<MapSettings> mapSettings,
            IOptionsSnapshot<AppDisplaySettings> appSettings)
        {
            this.Stores = stores.Value;
            this.MapSettings = mapSettings.Value;
            this.AppSettings = appSettings.Value;
        }

        public void OnGet()
        {

        }
    }
}
