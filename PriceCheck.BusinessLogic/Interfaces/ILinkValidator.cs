using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCheck.BusinessLogic.Interfaces
{
    public interface ILinkValidator
    {
        public bool Validate(string url);
    }
}
