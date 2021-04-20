namespace ExampleCalculatorBinding.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class CalculateSquareModel : PageModel
    {
        public int Input { get; set; }

        public int Square { get; set; }
        
        public void OnGet(int number)
        {
            this.Square = number * number;
            this.Input = number;
        }
    }
}