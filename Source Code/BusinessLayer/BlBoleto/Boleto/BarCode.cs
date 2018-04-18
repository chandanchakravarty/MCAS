using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using Microsoft.VisualBasic;

namespace Cms.BusinessLayer.BlBoleto
{
   public class BarCode
    {

        private string _Code = "";
        private string _LineDigitavel = "";
        private Image _images = null;
        private string _Key = "";
               /// <summary>
        /// Bar Code
        /// </summary>
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public Image Images
        {
            get { return _images; }
        }

        /// <summary>
        /// Returns the numeric representation of the barcode
        /// </summary>
        public string LineDigitavel
        {
            get { return _LineDigitavel; }
            set { _LineDigitavel = value; }
        }

        /// <summary>
        /// Key to mount Codigo Barra
        /// </summary>
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }

        private string FormataLinhaDigitavel(string _Code)
        {
            try
            {
                string cmplivre;
                string field1;
                string field2;
                string field3;
                string field4;
                string field5;
                long ifield5;
                int digitMod;

                cmplivre = Strings.Mid(_Code, 20, 25);
                field1 = Strings.Left(_Code, 4) + Strings.Mid(cmplivre, 1, 5);
                digitMod =  AbstractBank.Mod10(field1);
                field1 = field1 + digitMod.ToString();
                field1 = Strings.Mid(field1, 1, 5) + "." + Strings.Mid(field1, 6, 5);

                field2 = Strings.Mid(cmplivre, 6, 10);
                digitMod = AbstractBank.Mod10(field2);
                field2 = field2 + digitMod.ToString();
                field2 = Strings.Mid(field2, 1, 5) + "." + Strings.Mid(field2, 6, 6);

                field3 = Strings.Mid(cmplivre, 16, 10);
                digitMod = AbstractBank.Mod10(field3);
                field3 = field3 + digitMod;
                field3 = Strings.Mid(field3, 1, 5) + "." + Strings.Mid(field3, 6, 6);

                field4 = Strings.Mid(_Code, 5, 1);

                ifield5 = Convert.ToInt64(Strings.Mid(_Code, 6, 14));

                if (ifield5 == 0)
                    field5 = "000";
                else
                    field5 = ifield5.ToString();

                return field1 + "  " + field2 + "  " + field3 + "  " + field4 + "  " + field5;
            }
            catch
            {
                throw new Exception("Bar code invalid");
            }
        }
    }
}
