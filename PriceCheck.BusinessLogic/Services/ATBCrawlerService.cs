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


namespace PriceCheck.BusinessLogic.Services
{
    public class ATBCrawlerService : ICrawlerService
    {
        private readonly HttpClient _httpClient;

        private Stack<string> _UrlToVisit = new Stack<string>();
        private List<string> _VisitedUrls = new List<string>();
        private string _BaseUrl = "https://www.atbmarket.com";
        private ATBService _ATBservice;
        public ATBCrawlerService(HttpClient httpClient, ATBService ATBservice)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; U; Linux i686) Gecko/20071127 Firefox/2.0.0.11");
            _UrlToVisit.Push(_BaseUrl);

            _ATBservice = ATBservice;
            //_UrlToVisit.Push("https://www.atbmarket.com/certificate/charity/images/charity-certificate/certificate_rules_uk.pdf");
        }

        public async Task<string> DownloadUrl(string url)
        {
            string responseString = null;
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
            if (!_UrlToVisit.Contains(url) && !_VisitedUrls.Contains(url) && LinkValidator.Validate(url))
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

                if (link != null && link.Contains("product") && _ATBservice.GetShopPositionByLink(link) == null)
                {
                    _ATBservice.CreateShopPosition(new ATBDto { ProductLink = link});
                }
            }
        }

        public async Task Run()
        {
            while(_UrlToVisit.Any())
            {
                Console.WriteLine(_UrlToVisit.Count.ToString() + " To visit");
                Console.WriteLine(_VisitedUrls.Count.ToString() + " Visited");

                foreach (var item in _UrlToVisit)
                {
                    Console.WriteLine(item);
                }

                var _url = _UrlToVisit.Pop();

                Console.WriteLine("Crawling: " + _url);
                await CrawlPage(_url);
                _VisitedUrls.Add(_url);
            }
        }
    }
}
