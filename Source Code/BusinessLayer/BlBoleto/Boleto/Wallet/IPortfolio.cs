using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    public interface IPortfolio
    {
        IBank Bank { get; set; }
        int PortfolioNumber { get; set; }
        string Code { get; set;}
        string Type { get; set; }
        string Description { get; set; }
    }
}
