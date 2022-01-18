namespace CarRentingSystem.Infrastructure.TagHelpers
{
    using System.Text.Encodings.Web;

    using CarRentingSystem.Infrastructure.Exceptions;

    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement(Attributes = IsValidAttributeName)]
    public class ValidationClassTagHelper : TagHelper
    {
        private const string IsValidAttributeName = "is-valid";

        private string? key;
        private ViewContext? viewContext;

        [HtmlAttributeName(IsValidAttributeName)]
        public string Key
        {
            get => this.key ?? throw new UninitializedPropertyException(nameof(this.Key));
            set => this.key = value;
        }

        [ViewContext]
        public ViewContext ViewContext
        {
            get => this.viewContext ?? throw new UninitializedPropertyException(nameof(this.ViewContext));
            set => this.viewContext = value;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var modelState = this.ViewContext.ViewData.ModelState;
            var modelStateEntry = modelState[this.Key];

            if (modelStateEntry?.ValidationState is ModelValidationState.Invalid)
            {
                output.AddClass("is-invalid", HtmlEncoder.Default);
            }

            if (modelStateEntry?.ValidationState is ModelValidationState.Valid)
            {
                output.AddClass("is-valid", HtmlEncoder.Default);
            }

            return;
        }
    }
}
