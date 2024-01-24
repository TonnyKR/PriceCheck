using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCheck.BusinessLogic.Interfaces
{
    public interface IParserService
    {
        public Task Run();
        public Task ParseName(string url);
        public Task ParsePrice(string url);
    }
}
