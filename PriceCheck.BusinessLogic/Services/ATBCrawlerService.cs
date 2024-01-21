using PriceCheck.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser.Tokens;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System.Xml.Linq;
using PriceCheck.BusinessLogic.Exceptions;
using PriceCheck.Data.Interfaces;
using PriceCheck.BusinessLogic.Dtos;
using PriceCheck.BusinessLogic.Dtos.ATB;
using PriceCheck.Data.Entities;


namespace PriceCheck.BusinessLogic.Services
{
    public class ATBCrawlerService : ICrawlerService
    {
        private readonly HttpClient _httpClient;

        private Stack<string> _UrlToVisit = new Stack<string>();
        private List<string> _VisitedUrls = new List<string>();
        private List<ATBDto>? _products;
        private string _BaseUrl = "https://www.atbmarket.com";
        private IATBService _ATBservice;
        private ILinkValidator _linkValidator;
        public ATBCrawlerService(HttpClient httpClient, IATBService ATBservice, ILinkValidator linkValidator)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; U; Linux i686) Gecko/20071127 Firefox/2.0.0.11");
            _UrlToVisit.Push(_BaseUrl);

            _ATBservice = ATBservice;
            _products = _ATBservice.GetAllShopProducts().GetAwaiter().GetResult() as List<ATBDto>;
            _linkValidator = linkValidator;
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

        public async Task<List<string>> GetLinkedUrls(string url)
        {
            List<string> hrefTags = new List<string>();

            string _html = await DownloadUrl(url);
            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync(_html);

            foreach (IElement element in document.QuerySelectorAll("a"))
            {
                if(element.GetAttribute("href") != null && element.GetAttribute("href").StartsWith(url))
                {
                    hrefTags.Add(element.GetAttribute("href"));
                }
                if (element.GetAttribute("href") != null && element.GetAttribute("href").StartsWith("/"))
                {
                    hrefTags.Add(_BaseUrl + element.GetAttribute("href"));
                }              
            }
            return hrefTags;
        }
        public async Task AddUrlToVisit(string url)
        {
            bool validator = _linkValidator.Validate(url);

            if (validator && !_UrlToVisit.Contains(url) && !_VisitedUrls.Contains(url))
            {
                _UrlToVisit.Push(url);
            }
        }

        public async Task CrawlPage(string url)
        {
            var _links = await GetLinkedUrls(url);
            foreach(string link in _links)
            {
                await AddUrlToVisit(link);
                bool validator = _linkValidator.Validate(link);
                if (validator && link.Contains("product") && !_products.Any(p => p.ProductLink == link))
                {
                    await _ATBservice.CreateShopProduct(new ATBDto { ProductLink = link});
                    _products.Add(new ATBDto { ProductLink = link });
                }
            }
        }

        public async Task Run()
        {
            while(_UrlToVisit.Any())
            {
                Console.WriteLine(_UrlToVisit.Count.ToString() + " To visit");
                Console.WriteLine(_VisitedUrls.Count.ToString() + " Visited");

                var _url = _UrlToVisit.Pop();

                Console.WriteLine("Crawling: " + _url);
                await CrawlPage(_url);
                _VisitedUrls.Add(_url);
            }
        }
    }
}
