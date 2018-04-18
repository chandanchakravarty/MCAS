using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Cms.BusinessLayer.BlCommon
{
    [Serializable]
    public class ATTRIBUTE
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Parent { get; set; }
    }
    [Serializable]
    public class NODE
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Parent { get; set; }
    }

    [Serializable]
    public class FACTOR
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Parent { get; set; }
    }
    [Serializable]
    public class PRODUCT
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Parent { get; set; }
    }
}