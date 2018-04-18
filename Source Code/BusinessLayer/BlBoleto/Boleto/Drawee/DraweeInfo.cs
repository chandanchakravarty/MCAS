using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
  public  class DraweeInfo
    {

      String[] _data;
       
      /// <summary></summary>
      /// <param name="info">Text information</param>
        public DraweeInfo(String info)
        {
            _data = new String[] { info };
        }
        
       /// <summary></summary>
        /// <param name="linha1">Texto da primeira linha</param>
        /// <param name="linha2">Texto da segunda linha</param>
        public DraweeInfo(String lines1, String lines2)
        {
            _data = new String[] { lines1, lines2 };
        }


       /// <summary></summary>
        /// <param name="linhas">Vetor com as infomaçoes do Sacado, onde cada posição é uma linha da informação no boleto</param>
        public DraweeInfo(String[] lines) 
        {
            _data = lines;
        }

     
     public String HTML
        {
            get
            {
                String rtn = "";
                foreach (String S in _data)
                {
                    rtn += "<br />" + S; 
                
                }
                return rtn;
            }
        }
     public static String Render(String lines1, String lines2, Boolean NewLine)
     {
         return Render(new String[] { lines1, lines2 }, NewLine);
     }

     public static String Render(String[] lines, Boolean NewLine)
     {
         String rtn = "";
         foreach (String S in lines)
         {
             rtn += "<br />" + S;

         }
         if (!NewLine) rtn = rtn.Substring(6);
         return rtn;
     }






    }
}
