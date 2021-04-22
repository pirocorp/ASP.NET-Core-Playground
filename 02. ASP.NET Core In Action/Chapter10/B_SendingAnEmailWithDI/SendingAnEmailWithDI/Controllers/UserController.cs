namespace SendingAnEmailWithoutDI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SendingAnEmailWithoutDI.Services;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public UserController(IEmailSender emailSender)
        {
            this._emailSender = emailSender;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(string username)
        {
            this._emailSender.SendEmail(username);
            return this.Ok("Email sent!");
        }
    }
}
