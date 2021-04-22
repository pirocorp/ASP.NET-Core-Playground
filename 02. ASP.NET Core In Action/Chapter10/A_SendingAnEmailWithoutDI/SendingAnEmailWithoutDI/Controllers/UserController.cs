namespace SendingAnEmailWithoutDI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SendingAnEmailWithoutDI.Services;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult RegisterUser(string username)
        {
            var emailSender = new EmailSender(
                new MessageFactory(),
                new NetworkClient(
                    new EmailServerSettings
                    {
                        Host = "smtp.server.com",
                        Port = 25
                    })
                );
            emailSender.SendEmail(username);
            return this.Ok("Email sent!");
        }
    }
}
