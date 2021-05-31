namespace FluentValidationConverter.Pages
{
    using FluentValidation;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using System.Linq;

    public class IndexModel : PageModel
    {
        public IndexModel(ICurrencyProvider provider)
        {
            this.Currencies = provider.GetCurrencies();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string[] Currencies { get; set; }

        public string Results { get; set; }

        public void OnGet()
        {
            this.Input = new InputModel
            {
                CurrencyFrom = "CAD",
                CurrencyTo = "USD",
                Quantity = 50
            };
        }
        
        public void OnPost()
        {
            this.Results = this.ModelState.IsValid
                ? $"Converting {this.Input.Quantity} {this.Input.CurrencyFrom} to {this.Input.CurrencyTo}"
                : "Please correct the errors";
        }

        public class InputModel
        {
            public string CurrencyFrom { get; set; }

            public string CurrencyTo { get; set; }

            public decimal Quantity { get; set; }
        }

        public class InputValidator : AbstractValidator<InputModel>
        {
            private readonly string[] _allowedValues = { "GBP", "USD", "CAD", "EUR" };

            public InputValidator(ICurrencyProvider provider)
            {
                this.RuleFor(x => x.CurrencyFrom)
                    .NotEmpty()
                    .Length(3)
                    .Must(value => this._allowedValues.Contains(value))
                    .WithMessage("Not a valid currency code");

                this.RuleFor(x => x.CurrencyTo)
                    .NotEmpty()
                    .Length(3)
                    .MustBeCurrencyCode(provider)
                    .Must((model, currencyTo) => currencyTo != model.CurrencyFrom)
                    .WithMessage("Cannot convert currency to itself");

                this.RuleFor(x => x.Quantity)
                    .NotNull()
                    .InclusiveBetween(1, 1000);
            }
        }
    }
}
