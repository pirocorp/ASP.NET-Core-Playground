namespace LifetimeExamples.Pages
{
    using System.Collections.Generic;

    using LifetimeExamples.Services;

    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class CapturedModel : PageModel
    {
        private static readonly List<RowCountViewModel.Count> _previousCounts = new(); // c#9 feature - target typing

        private readonly CapturingRepository _capturingRepo;
        private readonly ScopedDataContext _scopedDataContext;

        public CapturedModel(
            CapturingRepository capturingRepo,
            ScopedDataContext scopedDataContext)
        {
            this._capturingRepo = capturingRepo;
            this._scopedDataContext = scopedDataContext;
        }

        public RowCountViewModel RowCounts { get; set; }

        public void OnGet()
        {
            var count = new RowCountViewModel.Count
            {
                DataContext = this._scopedDataContext.RowCount,
                Repository = this._capturingRepo.RowCount,
            };
            _previousCounts.Insert(0, count);
            this.RowCounts = new RowCountViewModel
            {
                Current = count,
                Previous = _previousCounts,
            };
        }
    }
}
