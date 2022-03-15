using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class CreatePaymentIntentRequest
    {
        [JsonProperty("paymentMethodType")]
        public string PaymentMethodType { get; set; } 
        
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
