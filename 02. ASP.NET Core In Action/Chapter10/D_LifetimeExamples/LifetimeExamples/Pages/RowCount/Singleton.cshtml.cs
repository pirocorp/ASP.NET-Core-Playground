namespace LifetimeExamples.Pages
{
    using System.Collections.Generic;

    using LifetimeExamples.Services;

    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class SingletonModel : PageModel
    {
        private static readonly List<RowCountViewModel.Count> _previousCounts = new(); // c#9 feature - target typing

        private readonly SingletonRepository _singletonRepo;
        private readonly SingletonDataContext _singletonDataContext;

        public SingletonModel(
            SingletonRepository singletonRepo,
            SingletonDataContext singletonDataContext
            )
        {
            this._singletonRepo = singletonRepo;
            this._singletonDataContext = singletonDataContext;
        }

        public RowCountViewModel RowCounts { get; set; }

        public void OnGet()
        {
            var count = new RowCountViewModel.Count
            {
                DataContext = this._singletonDataContext.RowCount,
                Repository = this._singletonRepo.RowCount,
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
