namespace CustomTagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.Encodings.Web;

    public class SystemInfoTagHelper : TagHelper
    {
        private readonly HtmlEncoder _htmlEncoder;

        public SystemInfoTagHelper(HtmlEncoder htmlEncoder)
        {
            this._htmlEncoder = htmlEncoder;
        }

        /// <summary>
        /// Show the current <see cref="Environment.MachineName"/>. true by default
        /// </summary>
        [HtmlAttributeName("add-machine")]
        public bool IncludeMachine { get; set; } = true;

        /// <summary>
        /// Show the current OS
        /// </summary>
        [HtmlAttributeName("add-os")]
        public bool IncludeOS { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";                       // Replaces <system-info> with <div>
            output.TagMode = TagMode.StartTagAndEndTag;  // <div> is not self closing

            var sb = new StringBuilder();

            if (this.IncludeMachine)
            {
                sb.Append(" <strong>Machine</strong> ");
                sb.Append(this._htmlEncoder.Encode(Environment.MachineName));
            }

            if (this.IncludeOS)
            {
                sb.Append(" <strong>OS</strong> ");
                sb.Append(this._htmlEncoder.Encode(RuntimeInformation.OSDescription));
            }

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
