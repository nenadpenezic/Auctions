using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.Services
{
    public class StringGenerator
    {
        public static string GenerateVerificationString()
        {
            char[] allowedCharacters = "qwertyuiopasdfghjklzxcvbnm1234567890".ToCharArray();
            Random random = new Random();
            string verificationString = "";
            for (int i = 0; i < 195; i++)
                verificationString += allowedCharacters[random.Next(0, allowedCharacters.Length)];
            return verificationString;
        }
    }

}
