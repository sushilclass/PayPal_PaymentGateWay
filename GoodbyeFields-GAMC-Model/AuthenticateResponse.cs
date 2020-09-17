using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeFields_GAMC_Model
{
    public class AuthenticateResponse
    {
        public string PlayerId { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(Player player, string token)
        {
            PlayerId = player.PlayerId;
            Token = token;
        }
    }
}

