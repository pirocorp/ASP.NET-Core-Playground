namespace CustomTagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement(Attributes = "if")]
    public abstract class IfTagHelper : TagHelper
    {
        // Ensures the tag helper will run first
        public override int Order => int.MinValue;

        [HtmlAttributeName("if")]
        public bool RenderContent { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (this.RenderContent == false)
            {
                output.TagName = null;
                output.SuppressOutput();
            }

            output.Attributes.RemoveAll("if");
        }
    }
}
