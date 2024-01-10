using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCheck.BusinessLogic.Interfaces
{
    public interface ICrawlerService
    {
        Task<string> DownloadUrl(string url);
        Task<List<string>> GetLinkedUrls(string url);
        Task AddUrlToVisit(string url);
        Task CrawlPage(string url);
        Task Run();
        
    }
}
