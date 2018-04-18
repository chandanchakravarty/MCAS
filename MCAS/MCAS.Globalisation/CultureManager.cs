/******************************************************************************************
<Author				    : - Pravesh K Chandel
<Start Date				: -	15 Apr 2014
<End Date				: -	16 Apr 2014
<Description			: - This file is used to maintain Culture to Globlisation support
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;

namespace MCAS.Globalisation
{
    public static class CultureManager
    {
        const string GermanCultureName = "de";
        const string FrenchCultureName = "fr";
        const string ItalianCultureName = "it";
        const string EnglishCultureName = "en";

        static CultureInfo DefaultCulture
        {
            get
            {
                return SupportedCultures[EnglishCultureName];
            }
        }

        static Dictionary<string, CultureInfo> SupportedCultures { get; set; }


        static void AddSupportedCulture(string name)
        {
            SupportedCultures.Add(name, CultureInfo.CreateSpecificCulture(name));
        }

        static void InitializeSupportedCultures()
        {
            SupportedCultures = new Dictionary<string, CultureInfo>();
            //AddSupportedCulture(GermanCultureName);
            AddSupportedCulture(FrenchCultureName);
            //AddSupportedCulture(ItalianCultureName);
            AddSupportedCulture(EnglishCultureName);
        }

        static string ConvertToShortForm(string code)
        {
            return code.Substring(0, 2);
        }

        static bool CultureIsSupported(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return false;
            code = code.ToLowerInvariant();
            if (code.Length == 2)
                return SupportedCultures.ContainsKey(code);
            return CultureFormatChecker.FormattedAsCulture(code) && SupportedCultures.ContainsKey(ConvertToShortForm(code));
        }

        static CultureInfo GetCulture(string code)
        {
            if (!CultureIsSupported(code))
                return DefaultCulture;
            string shortForm = ConvertToShortForm(code).ToLowerInvariant(); ;
            return SupportedCultures[shortForm];
        }

        public static void SetCulture(string code)
        {
            CultureInfo cultureInfo = GetCulture(code);
            cultureInfo.DateTimeFormat.ShortDatePattern = System.Configuration.ConfigurationManager.AppSettings["DateFormat"];
            cultureInfo.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
        }

        static CultureManager()
        {
            InitializeSupportedCultures();
        }
    }

    public static class CultureFormatChecker
    {
        //This matches the format xx or xx-xx 
        // where x is any alpha character, case insensitive
        //The router will determine if it is a supported language
        static Regex _cultureRegexPattern = new Regex(@"^([a-zA-Z]{2})(-[a-zA-Z]{2})?$", RegexOptions.IgnoreCase & RegexOptions.Compiled);

        public static bool FormattedAsCulture(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return false;
            return _cultureRegexPattern.IsMatch(code);

        }
    }
}
