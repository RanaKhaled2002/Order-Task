using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderService.DTOs
{
    public class TokenDTO
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; }

        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}
