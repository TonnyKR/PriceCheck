using AngleSharp.Html.Parser;
using AngleSharp.Text;
using AutoMapper;
using PriceCheck.BusinessLogic.Dtos;
using PriceCheck.BusinessLogic.Dtos.ATB;
using PriceCheck.BusinessLogic.Interfaces;
using PriceCheck.Data.Entities;
using PriceCheck.Data.Interfaces;
using PriceCheck.Data.Migrations;
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
        private IMapper _mapper;

        public ATBParserService(HttpClient httpClient, IATBService ATBservice, IMapper mapper)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; U; Linux i686) Gecko/20071127 Firefox/2.0.0.11");
            
            _mapper = mapper;

            _ATBservice = ATBservice;
        }
        public async Task ParseName(string url)
        {
            var html = await DownloadUrl(url);
            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync(html);

            var product = await _ATBservice.GetShopProductByLink(url);

            string name = null;
            if (document.QuerySelector("h1") != null)
            {
                name = document.QuerySelector("h1").TextContent;
            }              

            await _ATBservice.UpdateShopProduct(product.Id, new ATBUpdateDto {ProductName = name });
        }

        public async Task ParsePrice(string url)
        {
            var html = await DownloadUrl(url);
            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync(html);

            var product = await _ATBservice.GetShopProductByLink(url);

            string price = null;
            string rawPrice = null;
            if (document.QuerySelector(".product-price__top") != null)
            {
                rawPrice = document.QuerySelector(".product-price__top").TextContent;
                foreach (char item in rawPrice)
                {
                    if (item.IsDigit() || item.Equals('.'))
                    {
                        price += item;
                    }
                }
            }

            await _ATBservice.UpdateShopProduct(product.Id, new ATBUpdateDto { ProductPrice = price });
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

        public async Task Run()
        {
            var productsList = await _ATBservice.GetAllShopProducts();
            foreach (var product in productsList)
            {
                await ParsePrice(product.ProductLink);
            }
        }
    }
}
