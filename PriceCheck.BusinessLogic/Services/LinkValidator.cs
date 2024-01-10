using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCheck.BusinessLogic.Services
{
    static public class LinkValidator
    {
        static List<string> ExcludedPhrases = new List<string>() {"promo", "wishlist","email", "PrivacyPolicy", "oferta", "rules", "about", "garanty"};
        public static bool Validate(string url)
        {
            if (string.IsNullOrEmpty(url) || ExcludedPhrases.Any(url.Contains))
            {
                return false;
            } 
/*            else if (url.StartsWith("https://") && (url.Remove(0, 8).Contains("//") || url.Remove(0, 8).Contains("///")) && ExcludedPhrases.Any(url.Contains))
            { 
                return false; 
            }
            else if (!url.StartsWith("https://") && (url.Contains("//") || url.Contains("///")) && ExcludedPhrases.Any(url.Contains))
            {
                return false;
            }*/
            else
            return true;
            
        }
    }
}
