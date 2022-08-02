using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.EmailClient
{
    public interface IEmailClient
    {
        public void SendEmail(string to, string body);
    }
}
