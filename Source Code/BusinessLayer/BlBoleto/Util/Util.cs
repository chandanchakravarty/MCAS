using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Drawing;
using System.ComponentModel;

namespace Cms.BusinessLayer.BlBoleto
{
    sealed class Utils
    {

        internal static long DateDiff(DateInterval Interval, System.DateTime StartDate, System.DateTime EndDate)
        {
            long lngDateDiffValue = 0;
            System.TimeSpan TS = new System.TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (Interval)
            {
                case DateInterval.Day:
                    lngDateDiffValue = (long)TS.Days;
                    break;
                case DateInterval.Hour:
                    lngDateDiffValue = (long)TS.TotalHours;
                    break;
                case DateInterval.Minute:
                    lngDateDiffValue = (long)TS.TotalMinutes;
                    break;
                case DateInterval.Month:
                    lngDateDiffValue = (long)(TS.Days / 30);
                    break;
                case DateInterval.Quarter:
                    lngDateDiffValue = (long)((TS.Days / 30) / 3);
                    break;
                case DateInterval.Second:
                    lngDateDiffValue = (long)TS.TotalSeconds;
                    break;
                case DateInterval.Week:
                    lngDateDiffValue = (long)(TS.Days / 7);
                    break;
                case DateInterval.Year:
                    lngDateDiffValue = (long)(TS.Days / 365);
                    break;
            }
            return (lngDateDiffValue);
        }

        // uislc: I think the FormatCode function () should be renamed to Complete ().
        /*
         * "For the record type A (Alphanumeric) complete with uppercase characters and trailing spaces
         * filling all the space field. To sort the records N (Numeric) fill with zeros
         * filling the whole left field. "(p.9)
         * 
         * Available at: http://www.sicoobpr.com.br/download/manualcobranca/Manual_Cedentes_Sistema_Proprio.doc
         */

        /// <summary>
        /// Function to complete a string of zeros or blank spaces. Can serve to create the referral.
        /// </summary>
        /// <param name="text">The value gets zeros or blanks</param>
        /// <param name="with">character to be inserted</param>
        /// <param name="size">Field Size</param>
        /// <param name="left">Indicates whether characters are inserted to the left or right, the value defaults to booting from the left (left)</param>
        /// <returns></returns>
        internal static string FormatCode(string text, string with, int length, bool left)
        {
            length -= text.Length;
            if (left)
            {
                for (int i = 0; i < length; ++i)
                {
                    text = with + text;
                }
            }
            else
            {
                for (int i = 0; i < length; ++i)
                {
                    text += with;
                }
            }
            return text;
        }

        internal static string FormatCode(string text, string with, int length)
        {
            return FormatCode(text, with, length, false);
        }

        internal static string FormatCode(string text, int length)
        {
            return FormatCode(text, "0", length, true);
        }

        /// <summary>
        /// Remove todos os acentos das palavras.
        /// </summary>
        /// <param name="value">palavra acentuada</param>
        /// <returns>palavra sem acento</returns>
        internal static String RemoveAcento(String value)
        {
            String normalizedString = value.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// returned an array of strings with variable size data line (can be used to read any file the return | | Referral)
        /// string pattern in the data correspond to closed intervals in mathematics eg [2-19] (closed 2-19)
        /// </summary>
        /// <param name="linha">string where the data will be extracted. For example, a line of a file return</param>
        /// <param name="pattern">PAR is compulsorily required number of numeric values in the string pattern. eg 1-1,2-19</param>
        /// <returns>an array of variable-length strings containing the data read on line: string []</returns>
        /// <example>
        /// string [] data = TDEE (sLine, "1-1,2-394,395-400");
        /// </example>
        internal static string[] GetDados(string linha, string pattern)
        {
            // separates the numbers
            pattern = pattern.Replace('-', ',');
            string[] coord = pattern.Split(',');

            //create object for storage buffer.
            string[] dados = new string[coord.Length / 2];

            //takes the two numbers in two and fills the array
            int x = 0;
            for (int i = 0; i < coord.Length; i += 2)
            {
                dados[x] = linha.Substring(Convert.ToInt32(coord[i]) - 1, Convert.ToInt32(coord[i + 1]) - Convert.ToInt32(coord[i]) + 1);
                //arg[x] = linha.Substring(Convert.ToInt32(coord[i]), Convert.ToInt32(coord[i + 1]));
                x++;
            }
            //retorna os dados
            return dados;
        }

        /* Example of Reading a file delivery
        private void button1_Click(object sender, EventArgs e)
        { //ler arquivo de texto
            StreamReader objReader = new StreamReader("C:\\Documents and Settings\\uis\\Desktop\\bancos\\CED006877211081.REM");
            string sLine = "";
            string[] dados;
        
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null){
                    dados = getDados(sLine, "1-1,2-394,395-400");
                    // adicionar os dados a um string
                    textBox1.Text += " posição:<" + dados[2] + ">";
                    // poderia ser
                    //new boleto_dados(dados[0],dados[1],dados[2]);
                }
            }
            objReader.Close();
        }
        */

        internal static bool IsNumber(string value)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            return !objNotNumberPattern.IsMatch(value) &&
                   !objTwoDotPattern.IsMatch(value) &&
                   !objTwoMinusPattern.IsMatch(value) &&
                   objNumberPattern.IsMatch(value);
        }

        internal static bool ToBool(object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return false;
            }
        }

        internal static int ToInt32(string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        internal static long ToInt64(string value)
        {
            try
            {
                return Convert.ToInt64(value);
            }
            catch
            {
                return 0;
            }
        }

        internal static string ToString(object value)
        {
            try
            {
                return Convert.ToString(value).Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        internal static DateTime ToDateTime(object value)
        {
            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                return new DateTime(1, 1, 1);
            }
        }


        /// <summary>
        ///Formats the CPF or CNPJ the Transferor or Sacado format: 000000000-00, 00.000.000/0001-00 respectively.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static string FormataCPFCPPJ(string value)
        {
            if (value.Trim().Length == 11)
                return FormataCPF(value);
            else if (value.Trim().Length == 14)
                return FormataCNPJ(value);

            throw new Exception(string.Format("O CPF ou CNPJ: {0} é inválido.", value));
        }

        /// <summary>
        /// Formats the number 92074286520 to CPF 920742865-20
        /// </summary>
        /// <param name="cpf">Number sequence of 11 digits. Example: 00000000000</param>
        /// <returns>CPF formatted</returns>
        internal static string FormataCPF(string cpf)
        {
            try
            {
                return string.Format("{0}.{1}.{2}-{3}", cpf.Substring(0, 3), cpf.Substring(3, 3), cpf.Substring(6, 3), cpf.Substring(9, 2));
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Formata o CNPJ. Exemplo 00.316.449/0001-63
        /// </summary>
        /// <param name="cnpj">Sequencia numérica de 14 dígitos. Exemplo: 00000000000000</param>
        /// <returns>CNPJ formatted</returns>
        internal static string FormataCNPJ(string cnpj)
        {
            try
            {
                return string.Format("{0}.{1}.{2}/{3}-{4}", cnpj.Substring(0, 2), cnpj.Substring(2, 3), cnpj.Substring(5, 3), cnpj.Substring(8, 4), cnpj.Substring(12, 2));
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Formato o CEP em 00.000-000
        /// </summary>
        /// <param name="cep">8-digit number sequence. Example: 00000000</param>
        /// <returns>ZIP formatted</returns>
        internal static string FormataCEP(string cep)
        {
            try
            {
                cep=cep.Length==7 ? cep.PadLeft(8,'0'):cep;
                return string.Format("{0}{1}-{2}", cep.Substring(0, 2), cep.Substring(2, 3), cep.Substring(5, 3));
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Formats and agency account
        /// </summary>
        /// <param name="agencia">Branch code</param>
        /// <param name="digitoAgencia">Check digit agency. May be empty.</param>
        /// <param name="conta">Account Code</param>
        /// <param name="digitoConta">check digit of the account. May be empty.</param>
        /// <returns>Agency and formatted account</returns>
        internal static string FormataAgenciaConta(string agencia, string digitoAgencia, string conta, string digitoConta)
        {
            string agenciaConta = string.Empty;
            try
            {
                agenciaConta = agencia;
                if (digitoAgencia != string.Empty)
                    agenciaConta += "-" + digitoAgencia;

                agenciaConta += "/" + conta;
                if (digitoConta != string.Empty)
                    agenciaConta += "-" + digitoConta;

                return agenciaConta;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Formats the field according to the type and size
        /// </summary>        
        public static string FitStringLength(string SringToBeFit, int maxLength, int minLength, char FitChar, int maxStartPosition, bool maxTest, bool minTest, bool isNumber)
        {
            try
            {
                string result = "";

                if (maxTest == true)
                {
                    // max
                    if (SringToBeFit.Length > maxLength)
                    {
                        result += SringToBeFit.Substring(maxStartPosition, maxLength);
                    }
                }

                if (minTest == true)
                {
                    // min
                    if (SringToBeFit.Length <= minLength)
                    {
                        if (isNumber == true)
                        {
                            result += (string)(new string(FitChar, (minLength - SringToBeFit.Length)) + SringToBeFit);
                        }
                        else
                        {
                            result += (string)(SringToBeFit + new string(FitChar, (minLength - SringToBeFit.Length)));
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Exception tmpEx = new Exception("Problemas ao Formatar a string. String = " + SringToBeFit, ex);
                throw tmpEx;
            }
        }

        public static string SubstituiCaracteresEspeciais(string strline)
        {
            try
            {
                strline = strline.Replace("ã", "a");
                strline = strline.Replace('Ã', 'A');
                strline = strline.Replace('â', 'a');
                strline = strline.Replace('Â', 'A');
                strline = strline.Replace('á', 'a');
                strline = strline.Replace('Á', 'A');
                strline = strline.Replace('à', 'a');
                strline = strline.Replace('À', 'A');
                strline = strline.Replace('ç', 'c');
                strline = strline.Replace('Ç', 'C');
                strline = strline.Replace('é', 'e');
                strline = strline.Replace('É', 'E');
                strline = strline.Replace('õ', 'o');
                strline = strline.Replace('Õ', 'O');
                strline = strline.Replace('ó', 'o');
                strline = strline.Replace('Ó', 'O');
                strline = strline.Replace('ô', 'o');
                strline = strline.Replace('Ô', 'O');
                strline = strline.Replace('ú', 'u');
                strline = strline.Replace('Ú', 'U');
                strline = strline.Replace('ü', 'u');
                strline = strline.Replace('Ü', 'U');
                strline = strline.Replace('í', 'i');
                strline = strline.Replace('Í', 'I');

                return strline;
            }
            catch (Exception ex)
            {
                Exception tmpEx = new Exception("Error when formatting string.", ex);
                throw tmpEx;
            }
        }

        /// <summary>
        /// Converts an image into byte array.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ConvertImageToByte(Image image)
        {
            if (image == null)
                return null;

            byte[] bytes;
            if (image.GetType().ToString() == "System.Drawing.Image")
            {
                ImageConverter converter = new ImageConverter();
                bytes = (byte[])converter.ConvertTo(image, typeof(byte[]));
                return bytes;
            }
            else if (image.GetType().ToString() == "System.Drawing.Bitmap")
            {
                bytes = (byte[])TypeDescriptor.GetConverter(image).ConvertTo(image, typeof(byte[]));
                return bytes;
            }
            else
                throw new NotImplementedException("ConvertImageToByte invalid type " + image.GetType().ToString());
        }
        //DataValida
        internal static bool ValidateDate(DateTime dateTime)
        {
            if (dateTime.ToString("dd/MM/yyyy") == "01/01/1900" | dateTime.ToString("dd/MM/yyyy") == "01/01/0001")
                return false;
            else
                return true;
        }
    }
}
