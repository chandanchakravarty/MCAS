using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
