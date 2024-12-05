using CustomsExternal.Data;
using CustomsExternal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using System.Net;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace CustomsExternal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public ActionResult<int> Post([FromBody] Registration registration)
        {
            SendEmailToUser(registration);
            registration.AllowPromotion = false;
            _context.Registrations.Add(registration);
            _context.SaveChanges();

            return Ok();
            //return CreatedAtAction(nameof(GetById), new { id = registration.RowId }, registration.RowId);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private async Task SendEmailToUser(Registration registration)
        {
            try
            {            
                string baseUrl = _configuration["AppSettings:BaseUrl"];
                string encodeEmail = Convert.ToBase64String(Encoding.UTF8.GetBytes(registration.Email));
                string confirmationLink = $"{baseUrl}/api/User/ConfirmEmail?email={encodeEmail}";

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync("moveappdriver@gmail.com", "wnxl xcik hptq xusj");

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("YourApp", "moveappdriver@gmail.com"));
                    message.To.Add(MailboxAddress.Parse(registration.Email));
                    message.Subject = "Confirm Your Registration";

                    message.Body = new TextPart("html")
                    {
                        Text = $"<p>שלום {registration.FirstName} {registration.LastName},</p><p>אנא אשר את הרישום שלך על ידי לחיצה על הקישור למטה:</p><a href='{confirmationLink}'>אשר את הרישום</a>"
                    };

                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                    Console.WriteLine("Confirmation email sent successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }

        // GET api/<UserController>/ConfirmEmail
        [HttpGet("ConfirmEmail")]
        public IActionResult ConfirmEmail(string email)
        {

            string decodedEmail = DecodeEmail(email);
            var registration = _context.Registrations.FirstOrDefault(r => r.Email == decodedEmail);


            if (registration == null)
            {
                return BadRequest("Invalid confirmation token.");
            }

            registration.AllowPromotion = true;

            try
            {
                _context.SaveChanges();
                return Ok("האימייל אושר בהצלחה. תודה!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private string DecodeEmail(string encodedEmail)
        {
            var bytes = Convert.FromBase64String(encodedEmail);
            return Encoding.UTF8.GetString(bytes);
        }

        private string GenerateToken(string email)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(email + DateTime.UtcNow);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash).Replace("/", "").Replace("+", "").Substring(0, 20); 
            }
        }

    }
}
