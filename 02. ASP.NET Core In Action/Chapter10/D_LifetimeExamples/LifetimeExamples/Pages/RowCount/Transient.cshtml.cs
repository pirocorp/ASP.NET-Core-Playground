namespace LifetimeExamples.Pages
{
    using System.Collections.Generic;

    using LifetimeExamples.Services;

    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class TransientModel : PageModel
    {
        private static readonly List<RowCountViewModel.Count> _previousCounts = new(); // c#9 feature - target typing

        private readonly TransientRepository _transientRepo;
        private readonly TransientDataContext _transientDataContext;

        public TransientModel(
            TransientRepository transientRepo,
            TransientDataContext transientDataContext
            )
        {
            this._transientRepo = transientRepo;
            this._transientDataContext = transientDataContext;
        }

        public RowCountViewModel RowCounts { get; set; }

        public void OnGet()
        {
            var count = new RowCountViewModel.Count
            {
                DataContext = this._transientDataContext.RowCount,
                Repository = this._transientRepo.RowCount,
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
