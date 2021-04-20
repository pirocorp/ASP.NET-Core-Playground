namespace ManageUsers.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class IndexModel : PageModel
    {
        /// <summary>
        /// WARNING: For demo only, not thread safe, you would store these values in a database etc
        /// </summary>
        private static readonly List<string> _users = new()
        {
            "Bart",
            "Jimmy",
            "Robbie"
        };

        [BindProperty, Required]
        public string NewUser{ get; set; }

        public List<string> ExistingUsers { get; set; }

        public void OnGet()
        {
            this.ExistingUsers = _users;
        }

        public IActionResult OnPost()
        {
            this.ExistingUsers = _users;
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }
            if (_users.Contains(this.NewUser))
            {
                //Note, you would typically extract this to a validation attribute
                //But I do it here as the users list is hard coded above
                this.ModelState.AddModelError(nameof(this.NewUser), "This user already exists!");
                return this.Page();
            }

            //all ok, add the user and clear the existing value
            _users.Insert(0, this.NewUser);
            return this.RedirectToPage();
        }
    }
}
