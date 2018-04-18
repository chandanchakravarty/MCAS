using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    public interface ICodeMotion
    {
        IBank Bank { get; }
        int Code { get; set;}
        string Description { get; }
    }
}
