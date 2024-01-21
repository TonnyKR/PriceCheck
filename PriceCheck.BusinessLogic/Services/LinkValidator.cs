using PriceCheck.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCheck.BusinessLogic.Services
{
    public class LinkValidator : ILinkValidator
    {
        List<string> ExcludedPhrases = new List<string>() {"promo", "wishlist", "email", "PrivacyPolicy", "oferta", "rules", "about", "garanty"};
        public bool Validate(string url)
        {
            if (string.IsNullOrEmpty(url) || ExcludedPhrases.Any(url.Contains))
            {
                return false;
            }
            else
            {
                return true;
            }         
        }
    }
}
