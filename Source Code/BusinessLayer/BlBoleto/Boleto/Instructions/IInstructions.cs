using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    public interface IInstructions
    {

        /// <summary>
        /// Validates the data on education
        /// </summary>
        void Validate();

        IBank Bank { get; set; }
        int Code { get; set; }
        string Description { get; set; }
        int QuantityDays { get; set; }


    }
}
