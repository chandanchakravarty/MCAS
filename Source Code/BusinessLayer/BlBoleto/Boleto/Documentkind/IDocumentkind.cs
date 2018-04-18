using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    public interface IDocumentkind
    {
        IBank Bank { get; set; }
        int Code { get; set;}
        string Sigla { get; set; }
        string Especie { get; set;}
    }

}