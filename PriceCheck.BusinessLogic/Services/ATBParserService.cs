using AngleSharp.Html.Parser;
using PriceCheck.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCheck.BusinessLogic.Services
{
    public class ATBParserService : IParserService
    {
        private readonly HttpClient _httpClient;
        private IATBService _ATBservice;
        public ATBParserService(HttpClient httpClient, IATBService aTBservice)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; U; Linux i686) Gecko/20071127 Firefox/2.0.0.11");
            
            _ATBservice = aTBservice;
        }
        public async Task<string> ParseName(string url)
        {
            var html = await DownloadUrl(url);
            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync(html);

            var name = document.QuerySelector("h1").TextContent;
            return name;
        }

        public Task<string> ParsePrice(string url)
        {
            throw new NotImplementedException();
        }
        public async Task<string> DownloadUrl(string url)
        {
            string responseString = "";
            try
            {
                responseString = await _httpClient.GetStringAsync(url);
            }
            catch (Exception)
            {

                return responseString;
            }
            return responseString;

        }
    }
}
