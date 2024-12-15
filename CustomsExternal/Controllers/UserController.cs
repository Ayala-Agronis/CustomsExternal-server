
using System.Net.Mail;
using System.Text;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using System.Net;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using System.Web.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using System.Configuration;

namespace CustomsExternal.Controllers
{
    public class UserController : ApiController
    {
        private CustomsExternalEntities db = new CustomsExternalEntities();
        //private readonly IConfiguration _configuration;

        public UserController()
        {           
        }

        // POST api/<UserController>
        [HttpPost]
        public IHttpActionResult Post(Registration registration)
        {
            SendEmailToUser(registration);
            registration.AllowPromotion = false;
            db.Registration.Add(registration);
            db.SaveChanges();

            return Ok();
            //return CreatedAtAction(nameof(GetById), new { id = registration.RowId }, registration.RowId);
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public void Put(int id, string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete]
        public void Delete(int id)
        {
        }
        private async Task SendEmailToUser(Registration registration)
        {
            try
            {
                // קריאת כתובת בסיס מ-Web.config
                string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
                string encodeEmail = Convert.ToBase64String(Encoding.UTF8.GetBytes(registration.Email));
                string confirmationLink = $"{baseUrl}/api/User/ConfirmEmail?email={encodeEmail}";

                // יצירת הלקוח לשליחה עם MailKit
                using (var client = new SmtpClient())
                {
                    // התחברות לשרת ה-SMTP של Gmail
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                    //await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                    // הזדהות עם חשבון Gmail
                    await client.AuthenticateAsync("moveappdriver@gmail.com", "wnxl xcik hptq xusj");

                    // יצירת הודעת דוא"ל
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("YourApp", "moveappdriver@gmail.com"));
                    message.To.Add(MailboxAddress.Parse(registration.Email));
                    message.Subject = "Confirm Your Registration";

                    // תוכן הדוא"ל (HTML)
                    message.Body = new TextPart("html")
                    {
                        Text = $"<p>שלום {registration.FirstName} {registration.LastName},</p><p>אנא אשר את הרישום שלך על ידי לחיצה על הקישור למטה:</p><a href='{confirmationLink}'>אשר את הרישום</a>"
                    };

                    // שליחת ההודעה
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                    // הדפסה למסך (לוג)
                    Console.WriteLine("Confirmation email sent successfully!");
                }
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות וכתיבתן לקונסול
                Console.WriteLine($"Failed to send email: {ex.Message}");
                // אפשר גם לזרוק את השגיאה למעלה או לשמור בלוג
            }
        }
        //private async Task SendEmailToUser(Registration registration)
        //{
        //    try
        //    {
        //        string baseUrl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];
        //        string encodeEmail = Convert.ToBase64String(Encoding.UTF8.GetBytes(registration.Email));
        //        string confirmationLink = $"{baseUrl}/api/User/ConfirmEmail?email={encodeEmail}";

        //        using (var client = new SmtpClient())
        //        {
        //            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        //            await client.AuthenticateAsync("moveappdriver@gmail.com", "wnxl xcik hptq xusj");

        //            var message = new MimeMessage();
        //            message.From.Add(new MailboxAddress("YourApp", "moveappdriver@gmail.com"));
        //            message.To.Add(MailboxAddress.Parse(registration.Email));
        //            message.Subject = "Confirm Your Registration";

        //            message.Body = new TextPart("html")
        //            {
        //                Text = $"<p>שלום {registration.FirstName} {registration.LastName},</p><p>אנא אשר את הרישום שלך על ידי לחיצה על הקישור למטה:</p><a href='{confirmationLink}'>אשר את הרישום</a>"
        //            };

        //            await client.SendAsync(message);
        //            await client.DisconnectAsync(true);

        //            Console.WriteLine("Confirmation email sent successfully!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Failed to send email: {ex.Message}");
        //    }
        //}

        // GET api/<UserController>/ConfirmEmail
        [HttpGet]
        [Route("api/User/ConfirmEmail")]
        public IHttpActionResult ConfirmEmail(string email)
        {

            string decodedEmail = DecodeEmail(email);
            var registration = db.Registration.FirstOrDefault(r => r.Email == decodedEmail);


            if (registration == null)
            {
                return BadRequest("Invalid confirmation token.");
            }

            registration.AllowPromotion = true;

            try
            {
                db.SaveChanges();
                return Ok("האימייל אושר בהצלחה. תודה!");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
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
