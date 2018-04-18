using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{   
 
   public class DraweeInformation : List<DraweeInfo>
    {

        /// <summary>
        /// Returns HTML representative of all content
        /// </summary>
       public String GenerateHTML(Boolean novaLinha)
        {
            String rtn = "";

            if (this.Count > 0)
            {
                foreach (DraweeInfo I in this)
                {
                    rtn += I.HTML;
                }
                if (!novaLinha) rtn = rtn.Substring(6);
            }
            return rtn;
        }

        
    }
}
