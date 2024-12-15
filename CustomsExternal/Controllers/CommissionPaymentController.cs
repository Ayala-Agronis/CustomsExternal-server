using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CustomsExternal.Controllers
{
    public class CommissionPaymentController : ApiController
    {
        // GET: api/CommissionPayment
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CommissionPayment/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CommissionPayment
        [HttpPost]
        [Route("authorize")]
        public async Task<IHttpActionResult> AuthorizePayment(PaymentRequest request)
        {
            string tranzilaUrl = "https://secure5.tranzila.com/cgi-bin/tranzila.cgi";
            string suplierId = "";
           
            var postData = new StringContent(
                $"supplier={suplierId}&sum=200&tranmode=A&ccno={request.CardNumber}&expdate={request.ExpiryDate}&mycvv={request.CVV}",
                Encoding.UTF8,
                "application/x-www-form-urlencoded"
            );

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(tranzilaUrl, postData);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return Ok(new { message = "Authorization hold successful", data = responseBody });
                }

                return BadRequest( "Failed to authorize payment"  + response.ReasonPhrase );
            }
        }

        // PUT: api/CommissionPayment/5
        public void Put(int id, string value)
        {
        }

        // DELETE: api/CommissionPayment/5
        public void Delete(int id)
        {
        }

        public class PaymentRequest
        {
            public string CardNumber { get; set; }
            public string ExpiryDate { get; set; } // MMYY
            public string CVV { get; set; }
        }
    }
}
