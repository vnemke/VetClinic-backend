﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class CreatePaymentIntentResponse
    {
        [JsonProperty("clientSecret")]
        public string ClientSecret { get; set; }
    }
}
