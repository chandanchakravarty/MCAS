/******************************************************************************************
	Modification History
	
	<Modified Date			: - > 11/03/2005
	<Modified By			: - > Shrikant Bhatt
	<Purpose				: - > Implementing the Default Values for GetImageFolder methord
								  if there is no value in the SESSION
	
	<Modified Date			: - > 31/04/2005
	<Modified By			: - > Shrikant Bhatt
	<Purpose				: - > Remove "Message Constants" as messages are comming from Message XML 
								  class
	
	<Modified Date			: - > 05/04/2005
	<Modified By			: - > Vijay Joshi
	<Purpose				: - > Adding the SCREENID property in this class and calling the SetSecurityXML from its setter
	
	<Modified Date			: - > 12/04/2005
	<Modified By			: - > Ashwani 
	<Purpose				: - > Get the value for phone/fax  regular expressions on each page.

	<Modified Date			: - > 15/04/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > declaring three new date variable for storing common date settings
	
	<Modified Date			: - > 21/04/2005
	<Modified By			: - > Pradeep
	<Purpose				: - > Createad a new version of MapTransactionLabel
								  Uses alias WC for System.Web.UI.WeControls
                                  
    <Modified Date			: - > 28/04/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Creating new session for app_id,customer_id and app_version_id
	
	<Modified Date			: - > 03/05/2005
	<Modified By			: - > Pradeep Iyer
	<Purpose				: - > Made changes to PopulateModel object for empty strings
	
	<Modified Date			: - > 09/05/2005
	<Modified By			: - > Pradeep Iyer
	<Purpose				: - > Added functionality for setting focus to controls at startup
	
	<Modified Date			: - > 12/05/2005
	<Modified By			: - > Gaurav
	<Purpose				: - > Change Regular Expression of RegExpMobile to RegExpPhone

	<Modified Date			: - > 24/05/2005
	<Modified By			: - > Ashwani
	<Purpose				: - > Added SubmitForm() Java Script function 

	<Modified Date			: - > 26/05/2005
	<Modified By			: - > Anshuman
	<Purpose				: - > Added SetUserType and GetUserType. Also set securityxml aginst userid and sceenid
	
	<Modified Date			: - > 27/05/2005
	<Modified By			: - > Anshuman
	<Purpose				: - > writting userTypeId in cookies
	
	<Modified Date			: - > 31/05/2005
	<Modified By			: - > Mohit
	<Purpose				: - > Added SetGridSize and GetGridSize.
	
	<Modified Date			: - > 31/05/2005
	<Modified By			: - > Vijay
	<Purpose				: - > Added PullCustomerAddress function
	
	<Modified Date			: - > 02/06/2005
	<Modified By			: - > Anshuman
	<Purpose				: - > GetSecurityXML function is being called from ClsSecurity and not from ClsCommon
	
	<Modified Date			: - > 04/06/2005
	<Modified By			: - > Pradeep
	<Purpose				: - > Added RegExp strings for positive double values
	
	<Modified Date			: - > 28-10-2005
	<Modified By			: - > Vijay Arora
	<Purpose				: - > Added Session Variable and property for Policy,Policy Version and LOBID.
	
	<Modified Date			: - > 31-01-206
	<Modified By			: - > Sumit Chaabra
	<Purpose				: - > Correct the condition of returning AppVersionID Session.

	<Modified Date			: - > 15-02-2006
	<Modified By			: - > Vijay Joshi
	<Purpose				: - > Implements function, to be used for pdf printing and reposrting
	
    <Modified Date			: - > 08/08/2007
	<Modified By			: - > Praveen Kasana
	<Purpose				: - > Implements function HttpProtocol for determining SSL pages.
	
    <Modified Date			: - > 09/08/2007
	<Modified By			: - > Praveen Kasana
	<Purpose				: - > Implements Encryption Decryption in Index Grid Pages..
	
	<Modified Date			: - > 13/03/2008
	<Modified By			: - > Praveen Kasana
	<Purpose				: - > Implements function HttpProtocol for determining SSL pages For Loading QQ :
	
	<Modified Date			: - > 24/02/2009
	<Modified By			: - > Praveen Kasana
	<Purpose				: - > Remove Cookies dependency on Login.
*******************************************************************************************/

using System;
using System.Text;
using System.Xml;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI;
using WC = System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.Model;
using System.Data.SqlClient;
using System.Data;
using Cms.BusinessLayer.BlCommon;
using System.Resources;
using System.Reflection;
//using System.Windows.Forms;
using System.Collections;
using System.Web.UI.WebControls;
//using APToolkitNET;
using System.Web.Security;
using System.Security.Cryptography;

//Added by Charles on 8-Mar-2010 for Multilingual Implementation
using System.Threading;
using System.Globalization;
using System.ComponentModel;
//Added till here

//Added by Agniswar for Grid Implementation using Xml
using Cms.CmsWeb.WebControls;


namespace Cms.CmsWeb
{
    /// <summary>
    /// Summary description for cmsbase.
    /// </summary>
    /// 
    public class cmsbase : System.Web.UI.Page
    {

        #region Constants

        private const string SETFOCUSFUNCTIONNAME = "setFocus";
        private const string SETFOCUSSCRIPTNAME = "inputFocusHandler";
        public const string DEFAULT_LANG_CULTURE = "en-US"; //Added by Charles on 11-Mar-2010 for Multilingual Implementation

        public const string COUNTRY_NAME = "Country_Name";
        public const string COUNTRY_ID = "Country_Id";
        public const string STATE_NAME = "State_Name";
        public const string STATE_ID = "State_Id";
        public const string TRANS_TYPE_ID = "TRANS_TYPE_ID";
        public const string TRANS_TYPE_NAME = "TRANS_TYPE_NAME";
        public const string USERNAME = "username";
        public const string USERID = "userid";
        public const string TYPEID = "TYPEID";
        public const string TYPEDESC = "TYPEDESC";
        public const string GRIDSIZE = "gridSize";
        //Added By raghav 
        public const string FILE_TYPE_PDF = "PDF";
        public const string CONTENT_TYPE_PDF = "APPLICATION/PDF";

        public const string FILE_TYPE_TEXT = "TXT";
        public const string CONTENT_TYPE_TEXT = "TEXT/HTML";
        //PRAVEER ITRACK NO 581
        public const string FILE_TYPE_HTML = "HTM";
        public const string CONTENT_TYPE_HTML = "TEXT/HTML";
        public const string FILE_TYPE_IMG = "IMG";
        public const string CONTENT_TYPE_IMAGE = "IMAGE/JPEG";
        public const string FILE_TYPE_WORD = "DOC";
        public const string CONTENT_TYPE_WORD = "APPLICATION/MSWORD";
        public const string FILE_TYPE_GIF = "GIF";
        public const string CONTENT_TYPE_GIF = "IMAGE/GIF";
        public const string FILE_TYPE_EXCEL = "XLS";
        public const string CONTENT_TYPE_EXCEL = "application/vnd.ms-excel";
        public const string FILE_TYPE_PNG = "PNG";
        public const string CONTENT_TYPE_PNG = "IMAGE/X-PNG";
        public const string FILE_TYPE_PPT = "PPT";
        public const string CONTENT_TYPE_PPT = "APPLICATION/MS-POWERPOINT";
        public const string FILE_TYPE_XML = "XML";
        public const string CONTENT_TYPE_XML = "TEXT/XML";
        public const string FILE_TYPE_BMP = "BMP";
        public const string CONTENT_TYPE_BMP = "IMAGE/BMP";
        public const string FILE_TYPE_DOCX = "DOCX";
        public const string CONTENT_TYPE_MSWORD = "application/vnd.ms-word.document.12";
        public const string FILE_TYPE_CSS = "CSS";
        public const string CONTENT_TYPE_CSS = "text/css";
        public const string FILE_TYPE_BITMAP = "XBM";
        public const string CONTENT_TYPE_BITMAP = "IMAGE/X-XBITMAP";
        public const string FILE_TYPE_TIFF = "TIFF TIF";
        public const string CONTENT_TYPE_TIFF = "image/tiff";
        public const string FILE_TYPE_MPEG = "mpeg";
        public const string CONTENT_TYPE_MPEG = "video/mpeg";
        public const string FILE_TYPE_MPG = "mpg";
        public const string CONTENT_TYPE_MPG = "video/mpeg";
        public const string FILE_TYPE_PPTX = "PPTX";
        public const string CONTENT_TYPE_PPTX = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public const string FILE_TYPE_RTF = "RTF";
        public const string CONTENT_TYPE_RTF = "application/rtf";//Changed on 14 May 2009
        public const string FILE_TYPE_XLSX = "XLSX";
        public const string CONTENT_TYPE_XLSX = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public const string FILE_TYPE_DOTX = "DOTX";
        public const string CONTENT_TYPE_DOTX = "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
        public const string FILE_TYPE_DOCM = "DOCM";
        public const string CONTENT_TYPE_DOCM = "application/vnd.ms-word.document.macroEnabled.12";

        #endregion

        public static string CommonCarrierCode = ""; //Added by Agniswar on 09 Dec 2011

        public static string gstrStyleSheetName = "";
        public static string gstrImageFolder = "";
        public static string xmlString = "";

        //public variables that will store regular expression string
        public string aRegExpPhone;
        public string aRegExpAgencyPhone;
        public string aRegExpExtn;
        public string aRegExpFax;
        public string aRegExpZip;
        public string aRegExpZipUS; //Added by Aditya for TFS BUG # 1832
        //added by Abhinav 
        public string aRegExpAccountNumber;
        public string aRegExpBankBranch;
        public string aRegExpBankofAgency;
        public string aRegExpTollFree;
        //added by Chetna
        public string aRegExpZipBrazil;
        public string aRegExpPhoneBrazil;
        public string aRegExpMobile;
        public string aRegExpEmail;
        public string aRegExpEmailList;
        public string aRegExpAlpha;
        public string aRegExpAlphaAdd;//FOR ADDRESS FIELDS IT ACCEPT [,-,0-9,A-Z,a-z]
        public string aRegExpAlphaNum;
        public string aRegExpAlphaNumStrict;
        public string aRegExpDecimal;
        public string aRegExpAlphaNumSpaceStrict;
        public string aRegExpInteger;
        public string aRegExpIntegerSign;
        public string aRegExpDouble;
        public string aRegExpBaseDouble;
        public string aRegExpCurrency;
        public string aRegExpDate;
        public string aRegExpShortDate;
        public string aRegExpRate;
        public static string aAppNumFormat;
        public string aRegExpCurrencyformat;
        public string aRegExpBaseCurrencyformat;
        public string aRegExpBaseDoublePositiveWithZero;
        public string aRegExpSiteUrl;
        public string aRegExpSUSEPNUM;
        //Manoj Rathore - Reg exp for website without "http://" part
        public string aRegExpSiteUrlWithoutHttp;
        public string aRegExpWebSiteUrl;
        //***********
        public string RegExpSiteUrlWeb;
        public string aRegExpPolicyNum;
        public string aRegExpTextArea150;
        public string aRegExpTextArea100;
        public string aRegExpTextArea255;
        public string aRegExpTextArea1000;
        public string aRegExpTextArea500;
        public string aRegExpClientName;
        public string aRegExpPositiveCurrency;
        public string aRegExpPercent;
        public string aRegExpSSN;
        public string aRegExpDoublePositiveNonZero;
        public string aRegExpBaseDoublePositiveNonZero;
        public string aRegExpDoublePositiveWithZeroFourDeci;
        public string aRegExpDoublePositiveWithZero;
        public string aRegExpDoublePositiveZero;
        public string aRegExpDoublePositiveNonZeroFourDeci;
        public string aRegExpAcNumber;
        public string aRegExpSubAcNumber;
        public string aRegExpDoubleZeroToPositive;
        //public static string CarrierSystemID = "";
        public string aRegExpDoublePositiveNonZeroStartWithZero;
        public string aRegExpDoublePositiveNonZeroStartNotZero;
        public string aRegExpDoublePositiveNonZeroStartNotZeroNonDollar;//Added for Itrack Issue 5639 on 28 April 09
        public string aRegExpAlphaNumWithDash;
        public string aRegExpBankAccountNumber; //Added by Aditya or TFS BUG # 2246
        public string aRegExpFederalID;
        public string aRegExpDrivLicIN;
        public string aRegExpDrivLicMI;
        public string aRegExpDrivLicWI;
        public string aRegExpNegativeCurrency;
        public string aRegExpDoublePositiveNonZeroNotLessThanOne;
        public string aRegExpDoublePositiveStartWithDecimal;
        public string aRegExpBaseDoublePositiveStartWithDecimal;
        public string aRegExpDoublePositiveWithMoreThanOneDecimalAndComma;
        public string aRegExpDoublePositiveNonZeroStartWithZeroForFedId;
        public string aRegExpPhoneAll;
        public string aRegExpIntegerPositiveNonZero;
        public string aRegExpBankBranchNumber;//itrack- 1495

        public static string USER_TYPE_UNDERWRITER = "13";
        //Added by Raghav(11-July-2008) - iTrack #4471
        public string aRegExpHexadecimal;
        public string aRegExpPasswordOneNumeric;
        //Added For Itrack Issue #6366.
        public string aRegExpIntegerPositiveWithMoreLength;
        //Added by Charles on 5-Jan-10 for Itrack 6830
        public string aRegExpUnderwritingTier;
        public string aRegExpTransactionID;
        public string aRegExpBranchCode;
        public string aRegExpAppPolicyNum;
        public string aAppWebDtFormat = "";
        public string aAppWebDtSep = "";
        public string aAppDtFormat = "";
        public string aAppMinYear;
        public string aCountry;

        private string LOBString = "";
        private string userId = "";
        private string userTypeId = "";
        private string userName = "";
        private string systemId = "";
        private string colorScheme = "";

        private string calendarExtn = ""; // Added by Charles on 18-May-10 for Multilingual Support

        protected string languageId;//added by Chetna
        protected string languageCode;//added by Chetna

        private string imageFolder = "";
        private string fLName = "";
        private string strScreenId = "";
        private string gridSize = "";

        private string appID = "";
        private string QQID = "";
        private string CalledFor = "";
        private string customerID = "";
        private string appVersionID = "";

        private string policyID = "";
        private string policyVersionID = "";
        private string lobID = "";
        private string SUB_LOB_ID = ""; //Added by Charles on 13-Apr-10 for Clause Page
        private string CLAIM_ADD_NEW = "";
        private string policyStatus = "";
        private string policyCurrency = "";
        private String ApplicationStatus = "";
        //added by sumit on 08-05-2006 to set claim id in session
        private string claimID = "";
        //added by Asfa on 25-Oct-2006 to set claim status in session
        private string claimStatus = "";
        private string activityStatus = "";
        public string NOT_COVERED = "Not Covered";

        //The ID of the control that needs focus at startup
        private string focusedControl;

        private string isRead = "";
        private string isWrite = "";
        private string isDelete = "";
        private string isExecute = "";

        //added by kuldeep
        private string agencyid = "";
        private string SuperVisor_Equivalent = "";

        public const string CALLED_FROM_PROCESS = "PROCESS";
        public const string CALLED_FROM_PROCESS_POLICY = "PROCESS_POLICY";
        public const string COMMIT_ANYWAYS_RULES = "COMMIT_ANYWAYS_RULES";
        public string httpProtocol = "";
        public string httpProtocolQQ = "";
        private String SYSBaseCurrency = String.Empty;
        private static string gStrkey = "PR0$^CTB^IL$ER";
        public NumberFormatInfo numberFormatInfo;
        public NumberFormatInfo NfiBaseCurrency;

        // Start Added By Lalit 04/20/2010
        public string aRegExpCpf_Cnpj;
        // End

        //public const string MASTER_POLICY = "14680";        
        //public const string SIMPLE_POLICY = "14681";

        public const string MASTER_POLICY = "14560";
        public const string SIMPLE_POLICY = "";
        public string PRODUCT_TYPE = "";

        public const string CO_INSURANCE_LEADER = "14548";
        public const string CO_INSURANCE_FOLLOWER = "14549";
        public const string CO_INSURANCE_DIRECT = "14547";
        //Unique ID of Expert Service Provider Other at Clm_type_detail table
        public const int EXPERT_SERVICE_PROVIDER_TYPE_OTHER = 191;

        ArrayList NodeCollection = new ArrayList();

        Int32 LangID = 0;
        /// <summary>
        /// default constructor of cmsbase class which will assign regular expression string from web.config file
        /// </summary>
        public cmsbase()
        {
            #region Commented Reg Expression Initialiazation
            // Regular Expression Moved to Global.aspx
            //all the regular expressions to be used while validating input values

            //Added by Raghav(11-July-2008) - iTrack #4471
            if (System.Web.HttpContext.Current.Session != null)
            {
                LangID = ClsCommon.BL_LANG_ID;
            }
            aRegExpBaseCurrencyformat = ConfigurationManager.AppSettings["RegExpCurrencyFormat"];//Added by Pradeep Kushwaha on 11-Nov-2010
            aRegExpBaseDoublePositiveWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZero"];
            aRegExpBaseDoublePositiveNonZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZero"];
            aRegExpBaseDoublePositiveStartWithDecimal = ConfigurationManager.AppSettings["RegExpDoublePositiveStartWithDecimal"];
            aRegExpBankBranchNumber = ConfigurationManager.AppSettings["RegExpBankBranchNumber"];//Added by Pradeep Kushwaha itrack 1495
            aRegExpBaseDouble = ConfigurationManager.AppSettings["RegExpDouble"];
            numberFormatInfo = new CultureInfo(enumCulture.US, true).NumberFormat;//Added by Pradeep Kushwaha on 07-Oct-2010 to format the number according to the culture
            numberFormatInfo.NumberDecimalDigits = 4;

            aRegExpHexadecimal = ConfigurationManager.AppSettings["RegExpHexadecimal"];

            aRegExpSubAcNumber = ConfigurationManager.AppSettings["RegExpSubAcNumber"];
            aRegExpAcNumber = ConfigurationManager.AppSettings["RegExpAcNumber"];
            aRegExpPhone = ConfigurationManager.AppSettings["RegExpPhone"];
            aRegExpAgencyPhone = ConfigurationManager.AppSettings["RegExpPhoneBrazil"];
            aRegExpZip = ConfigurationManager.AppSettings["RegExpZipBrazil"];//ConfigurationManager.AppSettings["RegExpZip"];
            aRegExpZipUS = ConfigurationManager.AppSettings["RegExpZip"]; //Added by Aditya for TFS BUG # 1832
            //added by Chetna
            aRegExpZipBrazil = ConfigurationManager.AppSettings["RegExpZipBrazil"];
            aRegExpExtn = ConfigurationManager.AppSettings["RegExpExtn"];
            aRegExpFax = ConfigurationManager.AppSettings["RegExpPhone"];
            aRegExpMobile = ConfigurationManager.AppSettings["RegExpPhone"];
            aRegExpPhoneBrazil = ConfigurationManager.AppSettings["RegExpPhoneBrazil"];
            aRegExpEmail = ConfigurationManager.AppSettings["RegExpEmail"];
            aRegExpEmailList = ConfigurationManager.AppSettings["RegExpEmailList"];
            aRegExpAlpha = ConfigurationManager.AppSettings["RegExpAlpha"];
            aRegExpAlphaAdd = ConfigurationManager.AppSettings["RegExpAlphaAdd"];
            aRegExpAlphaNum = ConfigurationManager.AppSettings["RegExpAlphaNum"];
            aRegExpAlphaNumStrict = ConfigurationManager.AppSettings["RegExpAlphaNumStrict"];
            aRegExpDecimal = ConfigurationManager.AppSettings["RegExpDecimal"];
            aRegExpAlphaNumSpaceStrict = ConfigurationManager.AppSettings["RegExpAlphaNumSpaceStrict"];
            aRegExpTextArea150 = ConfigurationManager.AppSettings["RegExpTextArea150"];
            aRegExpTextArea100 = ConfigurationManager.AppSettings["RegExpTextArea100"];
            aRegExpTextArea255 = ConfigurationManager.AppSettings["RegExpTextArea255"];
            aRegExpTextArea1000 = ConfigurationManager.AppSettings["RegExpTextArea1000"];
            aRegExpTextArea500 = ConfigurationManager.AppSettings["RegExpTextArea500"];
            aRegExpInteger = ConfigurationManager.AppSettings["RegExpInteger"];
            aRegExpIntegerSign = ConfigurationManager.AppSettings["RegExpIntegerSign"];
            aRegExpDouble = ConfigurationManager.AppSettings["RegExpDouble"];
            aRegExpCurrency = ConfigurationManager.AppSettings["RegExpCurrency"];
            aRegExpPolicyNum = ConfigurationManager.AppSettings["RegExpPolicyNum"];
            aRegExpDate = ConfigurationManager.AppSettings["RegExpDate"];
            aRegExpSUSEPNUM = ConfigurationManager.AppSettings["RegExpSUSEPNUM"];
            //Added by Asfa(10-June-2008) - iTrack #3904
            aRegExpShortDate = ConfigurationManager.AppSettings["RegExpShortDate"];
            aRegExpRate = ConfigurationManager.AppSettings["RegExpRate"];
            aRegExpCurrencyformat = ConfigurationManager.AppSettings["RegExpCurrencyFormat"];
            aRegExpPositiveCurrency = ConfigurationManager.AppSettings["RegExpPositiveCurrency"];
            aRegExpSiteUrl = ConfigurationManager.AppSettings["RegExpSiteUrl"];
            aRegExpNegativeCurrency = ConfigurationManager.AppSettings["RegExpNegativeCurrency"];
            //Manoj Rathore - to show website addedd without http://
            aRegExpSiteUrlWithoutHttp = ConfigurationManager.AppSettings["RegExpSiteUrlWoHttp"];
            aRegExpWebSiteUrl = ConfigurationManager.AppSettings["RegExpWebSiteUrl"];
            aRegExpBankofAgency = ConfigurationManager.AppSettings["RegExpBankofAgency"];
            RegExpSiteUrlWeb = ConfigurationManager.AppSettings["RegExpSiteUrlWeb"];
            aRegExpClientName = ConfigurationManager.AppSettings["RegExpClientName"];

            aRegExpPercent = ConfigurationManager.AppSettings["RegExpPercent"];
            aRegExpSSN = ConfigurationManager.AppSettings["RegExpSSN"];
            aRegExpAccountNumber = ConfigurationManager.AppSettings["RegExpAccountNumber"];
            aRegExpBankBranch = ConfigurationManager.AppSettings["RegExpBankBranch"];

            aAppWebDtFormat = ConfigurationManager.AppSettings["WebServerDateFormat"];
            aAppWebDtSep = ConfigurationManager.AppSettings["WebServerDateFormatSeparator"];
            aAppDtFormat = ConfigurationManager.AppSettings["DateFormat"];

            aAppMinYear = ConfigurationManager.AppSettings["MinYear"];
            aCountry = ConfigurationManager.AppSettings["Country"];
            aRegExpTollFree = ConfigurationManager.AppSettings["RegExpTollFree"];

            aRegExpDoubleZeroToPositive = ConfigurationManager.AppSettings["RegExpDoubleZeroToPositive"];
            aRegExpDoublePositiveNonZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZero"];
            aRegExpDoublePositiveNonZeroStartWithZeroForFedId = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartWithZeroForFedId"];
            aRegExpDoublePositiveWithZeroFourDeci = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZeroFourDeci"];
            aRegExpDoublePositiveNonZeroStartWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartWithZero"];
            aRegExpAlphaNumWithDash = ConfigurationManager.AppSettings["RegExpAlphaNumWithDash"];
            aRegExpBankAccountNumber = ConfigurationManager.AppSettings["RegExpBankAccountNumber"]; //Added by Aditya or TFS BUG # 2246
            aRegExpDoublePositiveNonZeroStartNotZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartNotZero"];
            aRegExpDoublePositiveNonZeroStartNotZeroNonDollar = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartNotZeroNonDollar"];//Added for Itrack Issue 5639 on 29 April 2009
            aRegExpFederalID = ConfigurationManager.AppSettings["RegExpFederalID"];
            aRegExpDrivLicIN = ConfigurationManager.AppSettings["RegExpDrivLicIN"];
            aRegExpDrivLicMI = ConfigurationManager.AppSettings["RegExpDrivLicMI"];
            aRegExpDrivLicWI = ConfigurationManager.AppSettings["RegExpDrivLicWI"];
            aRegExpDoublePositiveZero = ConfigurationManager.AppSettings["RegExpDoublePositiveZero"];
            aRegExpDoublePositiveWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZero"];
            aRegExpDoublePositiveNonZeroFourDeci = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroFourDeci"];

            aRegExpDoublePositiveNonZeroNotLessThanOne = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroNotLessThanOne"];

            aRegExpDoublePositiveStartWithDecimal = ConfigurationManager.AppSettings["RegExpDoublePositiveStartWithDecimal"];
            aRegExpDoublePositiveWithMoreThanOneDecimalAndComma = ConfigurationManager.AppSettings["RegExpDoublePositiveWithMoreThanOneDecimalAndComma"];
            aRegExpPhoneAll = ConfigurationManager.AppSettings["RegExpPhoneAll"];
            aRegExpIntegerPositiveNonZero = ConfigurationManager.AppSettings["RegExpIntegerPositiveNonZero"];
            aRegExpPasswordOneNumeric = ConfigurationManager.AppSettings["RegExpPasswordOneNumeric"];

            //CarrierSystemID = getCarrierSystemID();//ConfigurationManager.AppSettings["CarrierSystemID"].ToUpper().Trim();


            aAppWebDtFormat = ConfigurationManager.AppSettings["WebServerDateFormat"];
            aAppWebDtSep = ConfigurationManager.AppSettings["WebServerDateFormatSeparator"];
            aAppDtFormat = ConfigurationManager.AppSettings["DateFormat"];
            aRegExpCpf_Cnpj = ConfigurationManager.AppSettings["RegExpCpf_Cnpj"];

            aRegExpTransactionID = ConfigurationManager.AppSettings["RegExpTransactionID"];
            aRegExpBranchCode = ConfigurationManager.AppSettings["RegExpBranchCode"];
            aRegExpAppPolicyNum = ConfigurationManager.AppSettings["RegExpAppPolicyNum"];
            aRegExpUnderwritingTier = ConfigurationManager.AppSettings["RegExpUnderwritingTier"];

            aAppWebDtFormat = ConfigurationManager.AppSettings["WebServerDateFormat"];
            aAppWebDtSep = ConfigurationManager.AppSettings["WebServerDateFormatSeparator"];

            if (LangID != 0 && (LangID == 2 || LangID == 3)) // LangID = 3 Added by Agniswar for Singapore Implementation.
            {
                aRegExpBaseCurrencyformat = ConfigurationManager.AppSettings["RegExpCurrencyFormatBrazil"];//Added by Pradeep Kushwaha on 11-Nov-2010
                aRegExpBaseDoublePositiveWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZeroBrazil"];
                aRegExpBaseDoublePositiveNonZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroBrazil"];
                aRegExpBaseDoublePositiveStartWithDecimal = ConfigurationManager.AppSettings["RegExpDoublePositiveStartWithDecimalBrazil"];

                aRegExpBaseDouble = ConfigurationManager.AppSettings["RegExpDoubleBrazil"];
                numberFormatInfo = new CultureInfo(enumCulture.BR, true).NumberFormat;//Added by Pradeep Kushwaha on 07-Oct-2010 to format the number according to the culture
                aRegExpBankBranchNumber = ConfigurationManager.AppSettings["RegExpBankBranchNumber"];//Added by Pradeep Kushwaha itrack 1495
                numberFormatInfo.NumberDecimalDigits = 4;
                aRegExpAgencyPhone = ConfigurationManager.AppSettings["RegExpAgencyPhone"];
                aRegExpCurrency = ConfigurationManager.AppSettings["RegExpCurrencyBrazil"];
                //aRegExpCurrencyformat = ConfigurationManager.AppSettings["RegExpCurrencyFormatBrazil"];
                //aRegExpPositiveCurrency = ConfigurationManager.AppSettings["RegExpPositiveCurrencyBrazil"];
                aRegExpNegativeCurrency = ConfigurationManager.AppSettings["RegExpNegativeCurrencyBrazil"];

                aRegExpDoubleZeroToPositive = ConfigurationManager.AppSettings["RegExpDoubleZeroToPositiveBrazil"];
                //aRegExpDouble = ConfigurationManager.AppSettings["RegExpDoubleBrazil"];
                aRegExpDoublePositiveNonZeroStartNotZeroNonDollar = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartNotZeroNonDollarBrazil"];
                aRegExpDoublePositiveNonZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroBrazil"];
                aRegExpDoublePositiveZero = ConfigurationManager.AppSettings["RegExpDoublePositiveZeroBrazil"];
                //aRegExpDoublePositiveWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZeroBrazil"];
                aRegExpDoublePositiveNonZeroFourDeci = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroFourDeciBrazil"];
                aRegExpDoublePositiveNonZeroStartWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartWithZeroBrazil"];
                aRegExpDoublePositiveWithZeroFourDeci = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZeroFourDeciBrazil"];
                aRegExpIntegerPositiveNonZero = ConfigurationManager.AppSettings["RegExpIntegerPositiveNonZeroBrazil"];
                aRegExpDecimal = ConfigurationManager.AppSettings["RegExpDecimalBrazil"];
                aRegExpDoublePositiveNonZeroNotLessThanOne = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroNotLessThanOneBrazil"];
                aRegExpDoublePositiveNonZeroStartNotZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartNotZeroBrazil"];
                aRegExpDoublePositiveStartWithDecimal = ConfigurationManager.AppSettings["RegExpDoublePositiveStartWithDecimalBrazil"];
                aRegExpDoublePositiveWithMoreThanOneDecimalAndComma = ConfigurationManager.AppSettings["RegExpDoublePositiveWithMoreThanOneDecimalAndCommaBrazil"];
                aRegExpClientName = ConfigurationManager.AppSettings["RegExpClientNameBrazil"];//Added By Pradeep Kushwaha on 23-July-2010
                aRegExpAppPolicyNum = ConfigurationManager.AppSettings["RegExpAppPolicyNumBrazil"];

                aRegExpDate = ConfigurationManager.AppSettings["RegExpDateBrazil"];
            }
            #endregion
            //aAppDtFormat = ConfigurationManager.AppSettings["DateFormat"];
            // Register a PreRender handler
            this.PreRender += new EventHandler(cmsbase_PreRender);


            //Check secure
            if (System.Web.HttpContext.Current.Request.IsSecureConnection)
            {
                httpProtocol = "https://";
                httpProtocolQQ = "https:";

            }
            else
            {
                httpProtocol = "http://";
                httpProtocolQQ = "http:";
            }
        }

        #region Default Values of Ebix DataTypes
        public int GetEbixIntDefaultValue()
        {
            return Int32.MinValue;
        }
        public double GetEbixDoubleDefaultValue()
        {
            return Double.MinValue;
        }
        //Added by Pradeep Kushwaha on 08-Oct-2010
        public Decimal GetEbixDecimalDefaultValue()
        {
            return Decimal.MinValue;
        }
        public double ConvertEbixDoubleValue(String value)
        {
            return Convert.ToDouble(value, numberFormatInfo);
        }
        #endregion
        #region SET AND GET SESSION VARIABLES

        //[AK-001]Start
        /*	public string GetRatingAdminLOBId()
            {
                if(RatingAdminLOBId=="")
                    return Session["RatingAdminLOBId"].ToString(); 
                else
                    return RatingAdminLOBId;
            }

            public void SetRatingAdminLOBId(string sessionValue)
            {
                Session.Add("RatingAdminLOBId",sessionValue); 
                RatingAdminLOBId=sessionValue;
            }		*/

        //[AK-001]End	


        //Function Definition Changed By Ravindra to Redirect User in case of expired session


        public string GetUserId()
        {
            //			if(userId=="")
            //			{
            //				//Check if user
            //				string strSessionID="";
            //				string userSessionID ="";
            //				if(Session["userId"]!=null)
            //				{
            //					userSessionID = Session["userId"].ToString();
            //					DataSet ldsStatus = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetUserLoggedStatus " + int.Parse(userSessionID.ToString()));
            //					if(ldsStatus!=null && ldsStatus.Tables[0].Rows.Count>0)
            //					{
            //						strSessionID = ldsStatus.Tables[0].Rows[0]["SESSION_ID"].ToString();
            //
            //					}
            //				}	
            //		
            //							
            //				if(Session["userId"] == null || strSessionID != Session.SessionID.ToString())
            //				{
            //					RedirectToLogin();
            //				}
            //				
            //				return Session["userId"].ToString(); 
            //			}
            //			else
            //				return userId;	

            string UserID = "";
            if (Session["userId"] == null || Session["userId"].ToString() == "")
            {
                RedirectToLogin();
            }
            else
            {
                UserID = Session["userId"].ToString();
            }

            return UserID;
        }
        //<Mohit Gupta>  31-May-2005 : START : <For dynamically setting the page size of the grid on the undex pages for the login user>
        public string GetGridSize()
        {
            if (gridSize == "")
            {
                if (Session["gridSize"] == null)
                {
                    RedirectToLogin();
                }
                return Session["gridSize"].ToString();
            }
            else
                return gridSize;
        }
        public void SetGridSize(string sessionValue)
        {
            Session.Add("gridSize", sessionValue);
            gridSize = sessionValue;
        }
        //<Mohit Gupta>  31-May-2005 : END

        public void SetUserId(string sessionValue)
        {
            Session.Add("userId", sessionValue);
            userId = sessionValue;
        }
        //Added By Lalit March 28,2011
         #region Set user equivalent to super visor vale in session
        public void SetIsUserSuperVisor(string sessionValue)
        {
            Session.Add("SuperVisor_Equivalent", sessionValue);
            SuperVisor_Equivalent = sessionValue;
        }
        public string getIsUserSuperVisor()
        {
            if (Session["SuperVisor_Equivalent"] != null)
            {
                if (Session["SuperVisor_Equivalent"].ToString() != "")
                    return Session["SuperVisor_Equivalent"].ToString();
                else
                    return "N";
            }
            else
                return "N";
        }
        #endregion


        //Added By Lalit Oct 25,2011.for tree Navigation.tfs# 1774
        public void SetSysNavigation(string SystemNavigation)
        {
            Session.Add("SYSTEMNAVIGATION", SystemNavigation);

        }
        //Added By Lalit Oct 25,2011
        public string GetSysNavigation()
        {
            //Session.Add("SYSTEMNAVIGATION", SystemNavigation);
            if (System.Web.HttpContext.Current.Session["SYSTEMNAVIGATION"] != null)
                return System.Web.HttpContext.Current.Session["SYSTEMNAVIGATION"].ToString();
            else
                return "";
        }//End tfs # 1774 Changes

        public string GetUserTypeId()
        {
            if (userTypeId == "")
            {
                if (Session["userTypeId"] == null)
                {
                    RedirectToLogin();
                }
                return Session["userTypeId"].ToString();
            }
            else
                return userTypeId;
        }
        public void SetConnStr(string sessionValue)
        {
            Session.Add("ConnStr", ClsCommon.EncryptString(sessionValue));
        }
        public string GetConnStr()
        {
            if (Session["ConnStr"] == null)
            {
                RedirectToLogin(); return "";
            }
            else
                return ClsCommon.DecryptString(Session["ConnStr"].ToString());
        }
        public void SetConnGridStr(string sessionValue)
        {
            Session.Add("ConnGridStr", ClsCommon.EncryptString(sessionValue));
        }
        public string GetConnGridStr()
        {
            if (Session["ConnGridStr"] == null)
            {
                RedirectToLogin(); return "";
            }
            else
                return ClsCommon.DecryptString(Session["ConnGridStr"].ToString());
        }
        public void SetUserTypeId(string sessionValue)
        {
            Session.Add("userTypeId", sessionValue);
            userTypeId = sessionValue;
        }


        //Added by Kuldeep on 30-11-2011 For Agency Id
         public string GetAgencyId()
        {
            if (Session["agencyId"] != null)
                return Session["agencyId"].ToString();
            else
                return agencyid;
           
        }
        public void SetAgencyId(string sessionValue)
        {
            Session.Add("agencyId", sessionValue);
            agencyid = sessionValue;
        }
        /// <summary>
        /// Added user key
        /// 16 March 2009
        /// </summary>
        /// <param name="sessionValue"></param>


        public string GetUserName()
        {
            if (userName == "")
            {
                if (Session["userName"] == null)
                {
                    RedirectToLogin();
                }
                return Session["userName"].ToString();
            }
            else
                return userName;
        }

        public void SetUserName(string sessionValue)
        {
            Session.Add("userName", sessionValue);
            userName = sessionValue;
        }

        public string GetUserFLName()
        {
            if (fLName == "")
            {
                if (Session["userFLName"] == null)
                {
                    RedirectToLogin();
                }
                return Session["userFLName"].ToString();
            }
            else
                return fLName;
        }
        public static string CarrierSystemID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["CarrierSystemID"] == null)
                {
                    System.Web.HttpContext.Current.Response.Redirect("/cms/cmsweb/aspx/login.aspx", true);
                    return "";
                }
                else
                    return System.Web.HttpContext.Current.Session["CarrierSystemID"].ToString();
            }

            set { System.Web.HttpContext.Current.Session.Add("CarrierSystemID", value); }
        }

        public void SetUserFLName(string sessionValue)
        {
            Session.Add("userFLName", sessionValue);
            fLName = sessionValue;
        }
        public string getCarrierSystemID()
        {
            if (Session["CarrierSystemID"] == null)
            {
                RedirectToLogin();
            }
            return Session["CarrierSystemID"].ToString().ToUpper();

        }
        public void setCarrierSystemID(string strCarrierSystemID)
        {
            Session.Add("CarrierSystemID", strCarrierSystemID);
            CarrierSystemID = strCarrierSystemID;

        }

        // Added by Agniswar on 09/12/2011

        public string getCommonCarrierCode()
        {
            if (Session["CommonCarrierCode"] == null)
            {
                RedirectToLogin();
            }
            return Session["CommonCarrierCode"].ToString().ToUpper();

        }
        public void setCommonCarrierCode(string strCommonCarrierCode)
        {
            Session.Add("CommonCarrierCode", strCommonCarrierCode);
            CommonCarrierCode = strCommonCarrierCode;

        }

        //Till Here

        public string GetSystemId()
        {
            if (systemId == "")
            {
                if (Session["systemId"] == null)
                {
                    RedirectToLogin();
                }
                return Session["systemId"].ToString().ToUpper();
            }
            else
                return systemId;
        }
        public void SetSystemId(string sessionValue)
        {
            Session.Add("systemId", sessionValue);
            systemId = sessionValue;
        }
        public string GetImageFolder()
        {
            if (imageFolder == "")
            {
                if (Session["imageFolder"] == null)
                {
                    RedirectToLogin();
                }
                return Session["imageFolder"].ToString();
            }
            else
                return imageFolder;
        }
        public void SetImageFolder(string sessionValue)
        {
            Session.Add("imageFolder", sessionValue);
            imageFolder = sessionValue;
        }

        public string GetColorScheme()
        {
            if (colorScheme == "")
            {
                if (Session["colorScheme"] == null)
                {
                    RedirectToLogin();
                }
                return Session["colorScheme"].ToString();
            }
            else
                return colorScheme;
        }

        public void SetColorScheme(string sessionValue)
        {
            Session.Add("colorScheme", sessionValue);
            colorScheme = sessionValue;
        }

        //Added By Chetna
        //Start
        public string GetLanguageID()
        {

            if (languageId == "" || languageId == null)
            {
                if (Session["languageId"] == null)
                {
                    RedirectToLogin();
                }
                return Session["languageId"].ToString();
            }
            else
                return languageId;
        }
        public void SetLanguageID(string sessionValue)
        {
            Session.Add("languageId", sessionValue);
            languageId = sessionValue;
        }
        //Added by Pradeep on 29-Sep-2010 for Base currency implementation
        public void SetSYSBaseCurrency(string sessionValue)
        {
            Session.Add("SYSBaseCurrency", sessionValue);
            SYSBaseCurrency = sessionValue;
        }
        public string GetSYSBaseCurrency()
        {
            try
            {
                if (SYSBaseCurrency == "" || SYSBaseCurrency == null)
                {
                    if (Session["SYSBaseCurrency"] == null)
                    {
                        RedirectToLogin();
                    }
                    return Session["SYSBaseCurrency"].ToString();
                }
                else
                    return SYSBaseCurrency;
            }
            catch
            {
                return "";
            }
        }
        //Added till here 

        /// <summary>
        /// Gets the User specific Language and Culture
        /// </summary>
        /// <returns>Language-Code as string</returns>
        public string GetLanguageCode()
        {
            // Try Catch added by Charles on 10-Mar-2010 for Multilingual Implementation
            try
            {
                if (languageCode == "" || languageCode == null)
                {
                    if (Session["languageCode"] == null)
                    {
                        RedirectToLogin();
                    }
                    return Session["languageCode"].ToString();
                }
                else
                    return languageCode;
            }
            catch
            {
                RedirectToLogin(); return "";
            }
        }
        public void SetLanguageCode(string sessionValue)
        {
            Session.Add("languageCode", sessionValue);
            languageCode = sessionValue;
        }

        #region setting/getting Model Object in session
        public void SetModelObject(IModelInfo objModel)
        {
            Session.Add("PageModelObject", objModel);
        }

        public IModelInfo GetModelObject()
        {
            if (Session["PageModelObject"] != null)
                return (IModelInfo)Session["PageModelObject"];
            else
                RedirectToLogin();
            return null;
        }

        public void SetPageModelObject(Model.Support.IEbixModel objModel)
        {
            MemoryStream Sms = SerializeBinary(objModel);
            Session.Add("PageModelObject", Sms.GetBuffer());
            Sms.Flush(); Sms.Close(); Sms.Dispose();
        }
        public Model.Support.IEbixModel GetPageModelObject()
        {

            if (Session["PageModelObject"] != null)
            {
                byte[] blob = (byte[])Session["PageModelObject"];
                if (blob != null)
                {
                    MemoryStream Dms = new MemoryStream(blob);
                    Model.Support.IEbixModel objModel = (Model.Support.IEbixModel)DeSerializeBinary(Dms);
                    Dms.Flush(); Dms.Close(); Dms.Dispose();
                    return objModel;
                }
                else
                    RedirectToLogin();
            }
            else
                RedirectToLogin();
            return null;
        }
        public void SetPageModelObjects(object objModel)
        {
            //Session.Add("PageModelObjects", objModel);
            MemoryStream Sms = SerializeBinary(objModel);
            Session.Add("PageModelObjects", Sms.GetBuffer());
            Sms.Flush(); Sms.Close(); Sms.Dispose();
        }
        public object GetPageModelObjects()
        {
            if (Session["PageModelObjects"] != null)
            {
                byte[] blob = (byte[])Session["PageModelObjects"];
                if (blob != null)
                {
                    MemoryStream Dms = new MemoryStream(blob);
                    object objModel = (object)DeSerializeBinary(Dms);
                    Dms.Flush(); Dms.Close(); Dms.Dispose();
                    return objModel;
                }
                else
                    RedirectToLogin();
            }
            else
                RedirectToLogin();
            return null;
        }

        #endregion

        // Common Function to Populate page controls from ModelObject or PageXml
        public void PopulatePageFromEbixModelObject(System.Web.UI.Page objCaller, Model.Support.IEbixModel objModel)
        {
            //Get the Schema Collectio from the Model object
            Hashtable TempIndt = objModel.htPropertyCollection;
            string strValue = "";
            string strID = "";
            string strTxt = "txt", strCmb = "cmb", strDdl = "ddl", strRb = "rdb", strChk = "chk", strHid = "hid", strLbl = "lbl"; ;
            //looping through all controls
            foreach (object key in TempIndt.Keys)
            {
                string ColumnType = TempIndt[key].GetType().Name.ToString();
                switch (ColumnType)
                {
                    case "EbixInt32":
                        strValue = ((Cms.EbixDataTypes.EbixInt32)TempIndt[key]).CurrentValue.ToString();
                        if (strValue == "-1" || Convert.ToInt32(strValue) == Int32.MinValue) continue;
                        break;
                    case "EbixDouble":
                        //strValue = ((Cms.EbixDataTypes.EbixDouble)TempIndt[key]).CurrentValue.ToString();
                        strValue = String.Format("{0:0.0000}", ((Cms.EbixDataTypes.EbixDouble)TempIndt[key]).CurrentValue);
                        if (strValue == "-1" || strValue == "-1.0" || strValue == "-1.0000" || Convert.ToDouble(strValue, numberFormatInfo) == Double.MinValue)
                            continue;
                        else if (strValue == "-1" || strValue == "-1,0" || strValue == "-1,0000" || Convert.ToDouble(strValue, numberFormatInfo) == Double.MinValue)
                            continue;

                        strValue = ((Cms.EbixDataTypes.EbixDouble)TempIndt[key]).CurrentValue.ToString("N", numberFormatInfo);//Added by Pradeep Kushwaha on 

                        //strValue = double.Parse(strValue, numberFormatInfo).ToString();
                        break;
                    case "EbixDecimal":

                        strValue = String.Format("{0:0.0000}", ((Cms.EbixDataTypes.EbixDecimal)TempIndt[key]).CurrentValue);
                        if (strValue == "-1" || strValue == "-1.0" || strValue == "-1.0000" || Convert.ToDecimal(strValue, numberFormatInfo) == Decimal.MinValue)
                            continue;
                        else if (strValue == "-1" || strValue == "-1,0" || strValue == "-1,0000" || Convert.ToDecimal(strValue, numberFormatInfo) == Decimal.MinValue)
                            continue;
                        strValue = ((Cms.EbixDataTypes.EbixDecimal)TempIndt[key]).CurrentValue.ToString("N", numberFormatInfo);//Added by Pradeep Kushwaha on 

                        break;
                    case "EbixString":
                        strValue = ((Cms.EbixDataTypes.EbixString)TempIndt[key]).CurrentValue.ToString();
                        break;
                    case "EbixDateTime":
                        if (((Cms.EbixDataTypes.EbixDateTime)TempIndt[key]).CurrentValue != Convert.ToDateTime(null))
                            strValue = ((Cms.EbixDataTypes.EbixDateTime)TempIndt[key]).CurrentValue.ToShortDateString();
                        else
                            strValue = String.Empty;
                        break;
                    case "EbixBoolean":
                        strValue = ((Cms.EbixDataTypes.EbixBoolean)TempIndt[key]).CurrentValue.ToString();
                        break;
                    default:
                        strValue = ((Cms.EbixDataTypes.EbixString)TempIndt[key]).CurrentValue.ToString();
                        break;
                }
                if (objCaller.FindControl(strTxt + key.ToString()) != null)
                {
                    WC.TextBox txt = (WC.TextBox)Page.FindControl(strTxt + key.ToString()); ;
                    if (txt != null)
                    {
                        strID = txt.ID;
                        txt.Text = strValue;
                    }
                    txt.Dispose();
                }
                else if (objCaller.FindControl(strCmb + key.ToString()) != null || objCaller.FindControl(strDdl + key.ToString()) != null)
                {
                    WC.DropDownList dl = (WC.DropDownList)Page.FindControl(strCmb + key.ToString());
                    if (dl == null) dl = (WC.DropDownList)Page.FindControl(strDdl + key.ToString());
                    if (dl != null)
                    {
                        dl.SelectedIndex = dl.Items.IndexOf(dl.Items.FindByValue(strValue));
                    }
                    dl.Dispose();
                }
                else if (objCaller.FindControl(strRb + key.ToString()) != null)
                {
                    WC.RadioButton rd = (WC.RadioButton)Page.FindControl(strRb + key.ToString());
                    if (rd != null)
                    {
                        strID = rd.ID;
                        if (strValue == "1" || strValue == "Y" || strValue == "10963")
                            rd.Checked = true;
                        else
                            rd.Checked = false;
                    }
                    rd.Dispose();
                }
                else if (objCaller.FindControl(strChk + key.ToString()) != null)
                {
                    WC.CheckBox chk = (WC.CheckBox)Page.FindControl(strChk + key.ToString());
                    if (chk != null)
                    {
                        strID = chk.ID;
                        if (strValue == "1" || strValue == "Y" || strValue == "10963")
                            chk.Checked = true;
                        else
                            chk.Checked = false;
                    }
                    chk.Dispose();
                }
                else if (objCaller.FindControl(strHid + key.ToString()) != null)
                {
                    System.Web.UI.HtmlControls.HtmlInputHidden hid = (System.Web.UI.HtmlControls.HtmlInputHidden)Page.FindControl(strHid + key.ToString());
                    if (hid != null)
                    {
                        strID = hid.ID;
                        hid.Value = strValue;

                    }
                    hid.Dispose();
                }
                else if (objCaller.FindControl(strLbl + key.ToString()) != null)
                {
                    WC.Label lbl = (WC.Label)Page.FindControl(strLbl + key.ToString());
                    if (lbl != null)
                    {
                        strID = lbl.ID;
                    }
                    lbl.Dispose();
                }
            }//end of page control loop
        }
        // Common Function to Populate page controls from ModelObject or PageXml
        public void PopulatePageFromModelObject(System.Web.UI.Page objCaller, Model.IModelInfo objModel)
        {
            //Get the table schema from the Model object
            System.Data.DataTable TempIndt = objModel.TableInfo;

            //looping through all controls
            foreach (System.Web.UI.Control cn in objCaller.Controls)
            {
                FillPageControls(cn, TempIndt);
            }//end of page control loop
        }
        private void FillPageControls(System.Web.UI.Control cn, DataTable TempIndt)
        {
            string strValue = "";
            string strID = "";
            //looping through all child controls
            foreach (System.Web.UI.Control childC in cn.Controls)
            {
                //every child control is checked against web controls
                if (childC is WC.TextBox)	// Checking the TextBox control
                {
                    if (childC.ID != null)
                    {
                        WC.TextBox txt = (WC.TextBox)Page.FindControl(childC.ID);
                        if (txt != null)
                        {
                            strID = txt.ID;
                            if (TempIndt.Columns.Contains(strID.Substring(3)))
                                txt.Text = TempIndt.Rows[0][TempIndt.Columns[strID.Substring(3)]].ToString();

                        }
                        txt.Dispose();
                    }
                }
                else if (childC is WC.DropDownList)	// Checking the DropDownList control
                {
                    WC.DropDownList dl = (WC.DropDownList)Page.FindControl(childC.ID);
                    if (dl != null)
                    {
                        strID = dl.ID;
                        if (TempIndt.Columns.Contains(strID.Substring(3)))
                        {
                            strValue = TempIndt.Rows[0][TempIndt.Columns[strID.Substring(3)]].ToString();
                            dl.SelectedIndex = dl.Items.IndexOf(dl.Items.FindByValue(strValue));
                        }
                    }
                    dl.Dispose();
                }
                // worked to be done
                else if (childC is WC.RadioButton)	// Checking the RadioButton control
                {
                    WC.RadioButton rd = (WC.RadioButton)Page.FindControl(childC.ID);
                    if (rd != null)
                    {
                        strID = rd.ID;
                        if (TempIndt.Columns.Contains(strID.Substring(3)))
                        {
                            strValue = TempIndt.Rows[0][TempIndt.Columns[strID.Substring(3)]].ToString();
                            if (strValue == "1" || strValue == "Y" || strValue == "10963")
                                rd.Checked = true;
                            else
                                rd.Checked = false;
                        }
                    }
                    rd.Dispose();
                }
                // worked to be done
                else if (childC is WC.CheckBox)		// Checking the CheckBox control
                {
                    WC.CheckBox chk = (WC.CheckBox)Page.FindControl(childC.ID);
                    if (chk != null)
                    {
                        if (chk.Checked == true)
                        {
                            strID = chk.ID;
                            if (TempIndt.Columns.Contains(strID.Substring(3)))
                            {
                                strValue = TempIndt.Rows[0][TempIndt.Columns[strID.Substring(3)]].ToString();
                                if (strValue == "1" || strValue == "Y" || strValue == "10963")
                                    chk.Checked = true;
                                else
                                    chk.Checked = false;
                            }
                        }
                    }
                    chk.Dispose();
                }
                if (childC is WC.HiddenField)	// Checking the hidden control
                {
                    if (childC.ID != null)
                    {
                        //WC.HiddenField hid = (WC.HiddenField)Page.FindControl(childC.ID);
                        System.Web.UI.HtmlControls.HtmlInputHidden hid = (System.Web.UI.HtmlControls.HtmlInputHidden)Page.FindControl(childC.ID);
                        if (hid != null)
                        {
                            strID = hid.ID;
                            if (TempIndt.Columns.Contains(strID.Substring(3)))
                                hid.Value = TempIndt.Rows[0][TempIndt.Columns[strID.Substring(3)]].ToString();

                        }
                        hid.Dispose();
                    }
                }
                // worked to be done
                else if (childC is WC.Label)		// Checking the Label control
                {
                    WC.Label lbl = (WC.Label)Page.FindControl(childC.ID);
                    if (lbl != null)
                    {
                        strID = lbl.ID;
                    }
                    lbl.Dispose();
                }
                else
                {
                    FillPageControls(cn, TempIndt);
                }
                strValue = "";
                strID = "";
            }//end of child control loop
        }
        private void FillPageControls(System.Web.UI.Control cn, XmlDocument xmlDoc)
        {
            string strValue = "";
            string strID = "";
            //looping through all child controls
            foreach (System.Web.UI.Control childC in cn.Controls)
            {
                //every child control is checked against web controls
                if (childC is WC.TextBox)	// Checking the TextBox control
                {
                    if (childC.ID != null)
                    {
                        WC.TextBox txt = (WC.TextBox)Page.FindControl(childC.ID);
                        if (txt != null)
                        {
                            strID = txt.ID;

                            if (xmlDoc.SelectNodes("//" + strID.Substring(3).ToUpper().Trim()).Count > 0)
                            {
                                txt.Text = xmlDoc.SelectNodes("//" + strID.Substring(3).ToUpper().Trim()).Item(0).InnerText.Trim();
                            }

                        }
                        txt.Dispose();
                    }
                }
                else if (childC is WC.DropDownList)	// Checking the DropDownList control
                {
                    WC.DropDownList dl = (WC.DropDownList)Page.FindControl(childC.ID);
                    if (dl != null)
                    {
                        strID = dl.ID;
                        if (xmlDoc.SelectNodes("//" + strID.Substring(3).ToUpper().Trim()).Count > 0)
                        {
                            strValue = xmlDoc.SelectNodes("//" + strID.Substring(3).ToUpper().Trim()).Item(0).InnerText.Trim();
                            dl.SelectedIndex = dl.Items.IndexOf(dl.Items.FindByValue(strValue));
                        }
                    }
                    dl.Dispose();
                }
                // worked to be done
                else if (childC is WC.RadioButton)	// Checking the RadioButton control
                {
                    WC.RadioButton rd = (WC.RadioButton)Page.FindControl(childC.ID);
                    if (rd != null)
                    {
                        strID = rd.ID;
                        if (xmlDoc.SelectNodes("//" + strID.Substring(3).ToUpper().Trim()).Count > 0)
                        {
                            strValue = xmlDoc.SelectNodes("//" + strID.Substring(3).ToUpper().Trim()).Item(0).InnerText.Trim();
                            if (strValue == "1" || strValue == "Y" || strValue == "10963")
                                rd.Checked = true;
                            else
                                rd.Checked = false;
                        }
                    }
                    rd.Dispose();
                }
                // worked to be done
                else if (childC is WC.CheckBox)		// Checking the CheckBox control
                {
                    WC.CheckBox chk = (WC.CheckBox)Page.FindControl(childC.ID);
                    if (chk != null)
                    {
                        if (chk.Checked == true)
                        {
                            strID = chk.ID;
                            if (xmlDoc.SelectNodes("//" + strID.Substring(3).ToUpper().Trim()).Count > 0)
                            {
                                strValue = xmlDoc.SelectNodes("//" + strID.Substring(3).ToUpper().Trim()).Item(0).InnerText.Trim();
                                if (strValue == "1" || strValue == "Y" || strValue == "10963")
                                    chk.Checked = true;
                                else
                                    chk.Checked = false;
                            }
                        }
                    }
                    chk.Dispose();
                }
                if (childC is WC.HiddenField)	// Checking the hidden control
                {
                    if (childC.ID != null)
                    {
                        System.Web.UI.HtmlControls.HtmlInputHidden hid = (System.Web.UI.HtmlControls.HtmlInputHidden)Page.FindControl(childC.ID);
                        if (hid != null)
                        {
                            strID = hid.ID;
                            if (xmlDoc.SelectNodes("//" + strID.Substring(3).ToUpper().Trim()).Count > 0)
                                hid.Value = xmlDoc.SelectNodes("//" + strID.Substring(3).ToUpper().Trim()).Item(0).InnerText.Trim();

                        }
                        hid.Dispose();
                    }
                }
                // worked to be done
                else if (childC is WC.Label)		// Checking the Label control
                {
                    WC.Label lbl = (WC.Label)Page.FindControl(childC.ID);
                    if (lbl != null)
                    {
                        strID = lbl.ID;
                    }
                    lbl.Dispose();
                }
                else
                {
                    FillPageControls(childC, xmlDoc);
                }
                strValue = "";
                strID = "";
            }//end of child control loop
        }
        public void PopulatePageFromModelObject(System.Web.UI.Page objCaller, string strModelXML)
        {
            if (strModelXML == "") return;
            //Get the table schema from the Model object
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strModelXML);
            //looping through all controls
            foreach (System.Web.UI.Control cn in objCaller.Controls)
            {
                FillPageControls(cn, xmlDoc);
            }//end of page control loop
        }

        /// <summary>
        /// Multilingual Support for Regular Expressions
        /// </summary>
        /// Added by Charles on 27-May-2010 for Multilingual Support
        public void SetRegExpCulture()
        {
            try
            {
                if (aAppDtFormat == enumDateFormat.DDMMYYYY)
                {
                    aRegExpDate = ConfigurationManager.AppSettings["RegExpDateBrazil"];
                }
                else
                {
                    aRegExpDate = ConfigurationManager.AppSettings["RegExpDate"];
                }

                if (ClsCommon.BL_LANG_ID == 2)
                {
                    aRegExpCurrency = ConfigurationManager.AppSettings["RegExpCurrencyBrazil"];
                    //aRegExpCurrencyformat = ConfigurationManager.AppSettings["RegExpCurrencyFormatBrazil"];
                    //aRegExpPositiveCurrency = ConfigurationManager.AppSettings["RegExpPositiveCurrencyBrazil"];
                    aRegExpNegativeCurrency = ConfigurationManager.AppSettings["RegExpNegativeCurrencyBrazil"];

                    aRegExpDoubleZeroToPositive = ConfigurationManager.AppSettings["RegExpDoubleZeroToPositiveBrazil"];
                    //aRegExpDouble = ConfigurationManager.AppSettings["RegExpDoubleBrazil"];
                    aRegExpDoublePositiveNonZeroStartNotZeroNonDollar = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartNotZeroNonDollarBrazil"];
                    aRegExpDoublePositiveNonZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroBrazil"];
                    aRegExpDoublePositiveZero = ConfigurationManager.AppSettings["RegExpDoublePositiveZeroBrazil"];
                    //aRegExpDoublePositiveWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZeroBrazil"];
                    aRegExpDoublePositiveNonZeroFourDeci = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroFourDeciBrazil"];
                    aRegExpDoublePositiveNonZeroStartWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartWithZeroBrazil"];
                    aRegExpDoublePositiveWithZeroFourDeci = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZeroFourDeciBrazil"];
                    aRegExpIntegerPositiveNonZero = ConfigurationManager.AppSettings["RegExpIntegerPositiveNonZeroBrazil"];
                    aRegExpDecimal = ConfigurationManager.AppSettings["RegExpDecimalBrazil"];
                    aRegExpDoublePositiveNonZeroNotLessThanOne = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroNotLessThanOneBrazil"];
                    aRegExpDoublePositiveNonZeroStartNotZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartNotZeroBrazil"];
                    aRegExpDoublePositiveStartWithDecimal = ConfigurationManager.AppSettings["RegExpDoublePositiveStartWithDecimalBrazil"];
                    aRegExpDoublePositiveWithMoreThanOneDecimalAndComma = ConfigurationManager.AppSettings["RegExpDoublePositiveWithMoreThanOneDecimalAndCommaBrazil"];
                    aRegExpClientName = ConfigurationManager.AppSettings["RegExpClientNameBrazil"];//Added By Pradeep Kushwaha on 23-July-2010
                    aRegExpAppPolicyNum = ConfigurationManager.AppSettings["RegExpAppPolicyNumBrazil"];
                    aRegExpAgencyPhone = ConfigurationManager.AppSettings["RegExpAgencyPhone"];
                    aRegExpBankBranchNumber = ConfigurationManager.AppSettings["RegExpBankBranchNumber"];//Added by Pradeep Kushwaha itrack 1495
                }
                else
                {
                    aRegExpCurrencyformat = ConfigurationManager.AppSettings["RegExpCurrencyFormat"];


                    aRegExpPositiveCurrency = ConfigurationManager.AppSettings["RegExpPositiveCurrency"];
                    aRegExpNegativeCurrency = ConfigurationManager.AppSettings["RegExpNegativeCurrency"];
                    aRegExpDoubleZeroToPositive = ConfigurationManager.AppSettings["RegExpDouble"];
                    aRegExpDouble = ConfigurationManager.AppSettings["RegExpDouble"];
                    aRegExpDoublePositiveNonZeroStartNotZeroNonDollar = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartNotZeroNonDollar"];
                    aRegExpDoublePositiveNonZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZero"];
                    aRegExpDoublePositiveZero = ConfigurationManager.AppSettings["RegExpDoublePositiveZero"];
                    aRegExpDoublePositiveWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZero"];
                    aRegExpDoublePositiveNonZeroFourDeci = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroFourDeci"];
                    aRegExpDoublePositiveNonZeroStartWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartWithZero"];
                    aRegExpDoublePositiveWithZeroFourDeci = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZeroFourDeci"];
                    aRegExpIntegerPositiveNonZero = ConfigurationManager.AppSettings["RegExpIntegerPositiveNonZero"];
                    aRegExpDecimal = ConfigurationManager.AppSettings["RegExpDecimal"];
                    aRegExpDoublePositiveNonZeroNotLessThanOne = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroNotLessThanOne"];
                    aRegExpDoublePositiveNonZeroStartNotZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartNotZero"];
                    aRegExpDoublePositiveStartWithDecimal = ConfigurationManager.AppSettings["RegExpDoublePositiveStartWithDecimal"];
                    aRegExpDoublePositiveWithMoreThanOneDecimalAndComma = ConfigurationManager.AppSettings["RegExpDoublePositiveWithMoreThanOneDecimalAndComma"];
                    aRegExpClientName = ConfigurationManager.AppSettings["RegExpClientName"];//Added By Pradeep Kushwaha on 23-July-2010
                    aRegExpAppPolicyNum = ConfigurationManager.AppSettings["RegExpAppPolicyNum"];
                    aRegExpAgencyPhone = ConfigurationManager.AppSettings["RegExpPhoneBrazil"];
                    aRegExpBankBranchNumber = ConfigurationManager.AppSettings["RegExpBankBranchNumber"];//Added by Pradeep Kushwaha itrack 1495
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Sets the Current Working Thread To User's Culture
        /// </summary>
        /// <param name="lang_CULTURE">String as language-Culture, e.g "en-US"</param>
        /// Added by Charles on 8-Mar-2010 for Multilingual Implementation
        public void SetCultureThread(string lang_CULTURE)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture((lang_CULTURE == null || lang_CULTURE == "") ? DEFAULT_LANG_CULTURE : lang_CULTURE);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                //added by Pravesh on 8 oct 2010 to support multilogin for multiculture
                ClsMessages.SetCustomizedXml((lang_CULTURE == null || lang_CULTURE == "") ? DEFAULT_LANG_CULTURE : lang_CULTURE);
                ClsCommon.SetCustomizedXml((lang_CULTURE == null || lang_CULTURE == "") ? DEFAULT_LANG_CULTURE : lang_CULTURE);
                ClsCommon.BL_LANG_CULTURE = (lang_CULTURE == null || lang_CULTURE == "") ? DEFAULT_LANG_CULTURE : lang_CULTURE;
                if (GetLanguageID() != "")
                {
                    ClsSingleton.strLangSelectClause = "LANG_ID = " + GetLanguageID();
                    ClsCommon.BL_LANG_ID = int.Parse(GetLanguageID());
                    ClsCommon.BL_LANG_CULTURE = GetLanguageCode();
                }
                ClsSingleton.strCarrierCode = getCarrierSystemID();
                // end here 
                //Date Format
                if (lang_CULTURE == DEFAULT_LANG_CULTURE)
                {
                    Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = enumDateFormat.MMDDYYYY;
                    Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern = enumDateFormat.MMDDYYYY;

                    aAppDtFormat = enumDateFormat.MMDDYYYY;
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = enumDateFormat.DDMMYYYY;
                    Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern = enumDateFormat.DDMMYYYY;

                    aAppDtFormat = enumDateFormat.DDMMYYYY;
                }

                if (lang_CULTURE == DEFAULT_LANG_CULTURE)
                {
                    //Currency Format                
                    Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = enumDecimalSeperator.US;
                    Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyDecimalSeparator = enumDecimalSeperator.US;
                    Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = enumGroupSeperator.US;
                    Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyGroupSeparator = enumGroupSeperator.US;

                    //Number Format
                    Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = enumDecimalSeperator.US;
                    Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator = enumDecimalSeperator.US;
                    Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = enumGroupSeperator.US;
                    Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberGroupSeparator = enumGroupSeperator.US;
                }
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        public string GetQQ_ID()
        {
            if (Session["QQID"] != null)
                return Session["QQID"].ToString();
            else
                return QQID;
        }

        public void SetQQ_ID(string sessionValue)
        {
            Session.Add("QQID", sessionValue);
            QQID = sessionValue;
        }

        public string GetCalledFor()
        {
            if (Session["CalledFor"] != null)
                return Session["CalledFor"].ToString();
            else
                return CalledFor;
        }

        public void SetCalledFor(string sessionValue)
        {
            Session.Add("CalledFor", sessionValue);
            CalledFor = sessionValue;
        }

        public string GetAppID()
        {
            if (Session["appID"] != null)
                return Session["appID"].ToString();
            else
                return appID;
        }

        public void SetAppID(string sessionValue)
        {
            Session.Add("appID", sessionValue);
            appID = sessionValue;
        }

        public string GetCustomerID()
        {
            if (Session["customerID"] != null)
            {
                return Session["customerID"].ToString();
            }
            else
                return "";
        }

        public void SetAttachedCustomerID(string sessionValue)
        {
            Session.Add("attachedcustomerID", sessionValue);
            //customerID = sessionValue;
        }
        public string GetAttachedCustomerID()
        {
            if (Session["attachedcustomerID"] != null)
            {
                return Session["attachedcustomerID"].ToString();
            }
            else
                return "";
        }

        public void SetCustomerID(string sessionValue)
        {
            Session.Add("customerID", sessionValue);
            customerID = sessionValue;
        }
        public string GetAppVersionID()
        {
            if (appVersionID == "" && Session["appVersionID"] != null)
                return Session["appVersionID"].ToString();
            else
                return appVersionID;
        }


        public void SetAppVersionID(string sessionValue)
        {
            Session.Add("appVersionID", sessionValue);
            appVersionID = sessionValue;
        }

        public void SetLOBString(string sessionValue)
        {
            Session.Add("LOBString", sessionValue);
            LOBString = sessionValue;
        }

        public string GetLOBString()
        {
            if (LOBString == "" )
            {
                if (Session["LOBString"] == null)
                {
                    RedirectToLogin();
                }
                return Session["LOBString"].ToString();                
            }
            else
            {
                return LOBString;
            }
        }


        //added by vj on 28-10-2005
        public string GetPolicyID()
        {
            if (policyID == "")
            {
                if (Session["policyID"] != null)
                    return Session["policyID"].ToString();
                else
                    return "";
            }
            else
                return policyID;
        }


        public void SetPolicyID(string sessionValue)
        {
            Session.Add("policyID", sessionValue);
            policyID = sessionValue;
        }

        public string GetPolicyVersionID()
        {
            if (policyVersionID == "")
            {
                if (Session["policyVersionID"] != null)
                    return Session["policyVersionID"].ToString();
                else
                    return "";
            }
            else
                return policyVersionID;
        }

        public void SetPolicyVersionID(string sessionValue)
        {
            Session.Add("policyVersionID", sessionValue);
            policyVersionID = sessionValue;
        }

        public string GetClaimID()
        {
            if (claimID == "")
            {
                if (Session["claimID"] != null && Session["claimID"].ToString() != "")
                    return Session["claimID"].ToString();
                else
                    return "";
            }
            else
                return claimID;
        }

        public void SetClaimID(string sessionValue)
        {
            Session.Add("claimID", sessionValue);
            claimID = sessionValue;
        }

        //Added By Asfa (25-Oct-2007)
        public string GetClaimStatus()
        {
            if (claimStatus == "")
            {
                if (Session["claimStatus"] != null && Session["claimStatus"].ToString() != "")
                    return Session["claimStatus"].ToString();
                else
                    return FetchClaimStatus(GetClaimID());
            }
            else
                return claimStatus;
        }

        public void SetClaimStatus(string sessionValue)
        {
            Session.Add("claimStatus", sessionValue);
            claimStatus = sessionValue;
        }

        public string GetLOBID()
        {
            //if (lobID == "")
            //{
            if (Session["lobID"] != null)
                return Session["lobID"].ToString();
            else
                return "";
            //}
            //else
            //    return lobID;
        }

        public void SetLOBID(string sessionValue)
        {
            Session.Add("lobID", sessionValue);
            lobID = sessionValue;
        }

        //Added by Charles on 13-Apr-10 for Clause Page
        public void SetSUB_LOB_ID(string sessionValue)
        {
            Session.Add("SUB_LOB_ID", sessionValue);
            SUB_LOB_ID = sessionValue;
        }
        public string GetSUB_LOB_ID()
        {
            if (SUB_LOB_ID == "")
            {
                if (Session["SUB_LOB_ID"] != null)
                    return Session["SUB_LOB_ID"].ToString();
                else
                    return "";
            }
            else
                return SUB_LOB_ID;
        }//Added till here

        //Added by Asfa (28-Jan-2008) - iTrack issue #3438
        public string GetClaimAddNew()
        {
            if (CLAIM_ADD_NEW == "")
                return Session["CLAIM_ADD_NEW"].ToString();
            else
                return CLAIM_ADD_NEW;
        }

        public void SetClaimAddNew(string sessionValue)
        {
            Session.Add("CLAIM_ADD_NEW", sessionValue);
            CLAIM_ADD_NEW = sessionValue;
        }

        //added by vj on 27-03-2006
        public string GetPolicyStatus()
        {
            if (policyStatus == "")
            {
                if (Session["policyStatus"] != null)
                    return Session["policyStatus"].ToString();
                else
                    return "";
            }
            else
                return policyStatus;
        }

        public void SetPolicyStatus(string sessionValue)
        {
            Session.Add("policyStatus", sessionValue);
            policyStatus = sessionValue;
        }
        public void SetEndorsementCoApplicant(string sessionValue)
        {
            Session.Add("EndorsementCoApplicantID", sessionValue);
        }
        public string GetEndorsementCoApplicant()
        {
            if (Session["EndorsementCoApplicantID"] != null)
                return Session["EndorsementCoApplicantID"].ToString();
            else
                return "0";
        }

        public void SetPolicyCurrency(string sessionValue)
        {
            Session.Add("policyCurrency", sessionValue);
            policyCurrency = sessionValue;
        }
        public string GetPolicyCurrency()
        {
            if (policyCurrency == "")
            {
                if (Session["policyCurrency"] != null)
                    return Session["policyCurrency"].ToString();
                else
                    return "";
            }
            else
                return policyCurrency;
        }
        //Added by pradeep Kushwaha on 17-06-2010
        public string GetApplicationStatus()
        {
            if (ApplicationStatus == "")
            {
                if (Session["ApplicationStatus"] != null)
                    return Session["ApplicationStatus"].ToString();
                else
                    return "";
            }
            else
                return ApplicationStatus;
        }

        public void SetApplicationStatus(string sessionValue)
        {
            Session.Add("ApplicationStatus", sessionValue);
            ApplicationStatus = sessionValue;
        }
        //Added end 


        //added by vj on 22-06-2006
        public string GetActivityStatus()
        {
            if (activityStatus == "")
            {
                if (Session["activityStatus"] != null)
                    return Session["activityStatus"].ToString();
                else
                    return "";
            }
            else
                return activityStatus;
        }

        public void SetActivityStatus(string sessionValue)
        {
            Session.Add("activityStatus", sessionValue);
            activityStatus = sessionValue;
        }

        public string GetProduct_Type(int LobID)
        {
            DataTable tbl = CmsWeb.ClsFetcher.LOBs.Select("LOB_ID=" + LobID.ToString()).CopyToDataTable();
            if (tbl != null && tbl.Rows.Count > 0)
            {
                return tbl.Rows[0]["PRODUCT_TYPE"].ToString();
            }
            else
                return SIMPLE_POLICY; //simple policy

        }
        public string GetTransaction_Type(string Tran_Type)
        {
            if (Tran_Type == MASTER_POLICY)
                return MASTER_POLICY;
            else
                return SIMPLE_POLICY;
        }
        public string GetTransaction_Type()
        {
            string Value = "";
            if (Session["TRANSACTION_TYPE"] != null)
                Value = Session["TRANSACTION_TYPE"].ToString();
            else
                Value = SIMPLE_POLICY;

            return Value;
        }
        public void SetTransaction_Type(string Value)
        {
            Session.Add("TRANSACTION_TYPE", Value);
            PRODUCT_TYPE = Value;
        }

        #endregion

        #region Public properties of CmsBase class
        public virtual string ScreenId
        {
            get
            {
                return strScreenId;
            }
            set
            {
                //Ravindra(03-13-2009): Disable browser caching
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.CacheControl = "no-cache";
                Response.Expires = -1500;
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);

                strScreenId = value;
                //Calling the setSecurity XML method for generating the SecurityXML
                SetSecurityXML(strScreenId, int.Parse(GetUserId()));
                SetCultureThread(GetLanguageCode());
            }
        }

        public string IsRead
        {
            get
            {
                return isRead;
            }
            set
            {
                isRead = value;
            }
        }

        public string IsWrite
        {
            get
            {
                return isWrite;
            }
            set
            {
                isWrite = value;
            }
        }

        public string IsDelete
        {
            get
            {
                return isDelete;
            }
            set
            {
                isDelete = value;
            }
        }

        public string IsExecute
        {
            get
            {
                return isExecute;
            }
            set
            {
                isExecute = value;
            }
        }
        #endregion

        #region SET AND GET WINDOWS GRID COMMON PARAMETERS
        public string GetWindowsGridColor()
        {
            string gridColor = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR" + GetColorScheme());
            string[] gridColorArray;
            gridColorArray = gridColor.Split(',');

            string strRedStyle = Int32.Parse(gridColorArray[1].Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
            string strGreenStyle = Int32.Parse(gridColorArray[1].Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
            string strBlueStyle = Int32.Parse(gridColorArray[1].Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();

            string strRedBase = Int32.Parse(gridColorArray[0].Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
            string strGreenBase = Int32.Parse(gridColorArray[0].Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
            string strBlueBase = Int32.Parse(gridColorArray[0].Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();

            string strRedAlternative = Int32.Parse(gridColorArray[2].Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
            string strGreenAlternative = Int32.Parse(gridColorArray[2].Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
            string strBlueAlternative = Int32.Parse(gridColorArray[2].Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();

            StringBuilder windowsGridColor = new StringBuilder();
            windowsGridColor.Append(strRedBase + "~" + strGreenBase + "~" + strBlueBase + "^" + strRedStyle + "~" + strGreenStyle + "~" + strBlueStyle + "^" + strRedAlternative + "~" + strGreenAlternative + "~" + strBlueAlternative);

            return windowsGridColor.ToString();
        }

        public string GetCacheSize()
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get("GRID_CACHE_SIZE") == null ? "40" : System.Configuration.ConfigurationManager.AppSettings.Get("GRID_CACHE_SIZE");
        }

        public string GetPageSize()
        {
            //return System.Configuration.ConfigurationManager.AppSettings.Get("GRID_PAGE_SIZE") == null ? "10" : System.Configuration.ConfigurationManager.AppSettings.Get("GRID_PAGE_SIZE");
            return GetGridSize();
        }
        #endregion

        #region MAKING USER NAME STRING
        public string GetProperCaseName(string strTitle, string strFName, string strLName)
        {
            string strProperName = ""; // final return string
            if (strTitle != "")
                strProperName = strTitle.Substring(0, 1).ToUpper() + strTitle.Substring(0, strTitle.Length - 1).ToLower() + "";
            else
                strProperName = "";

            if (strFName != "")
                strProperName = strProperName + strFName.Substring(0, 1).ToUpper() + strFName.Substring(0, strFName.Length - 1).ToLower() + "";
            else
                strProperName = strProperName + "";

            if (strLName != "")
                strProperName = strProperName + "" + strLName.Substring(0, 1).ToUpper() + strLName.Substring(0, strLName.Length - 1).ToLower();
            else
                strProperName = strProperName + "";

            return strProperName;

        }
        #endregion


        /// <summary>
        /// TO SET THE COOKIE VARIABLES CALL SetCookie METHOD 
        /// </summary>
        /// <param></param>
        /// <returns>void</returns>
        public void SetCookie()
        {
            /*************************************************************************/
            /// SETTING EACH COOKIE VARIABLE WITH THE CORRESPONDING SESSION VARIABLES 
            /// AND SETTING ITS EXPIRY TIME TO 31/12/9999
            /*************************************************************************/

            /*************************************************************************/
            /// COMMENTED  COOKIE VARIABLES
            /// 24 FEB 2009 
            /*************************************************************************/

            /*System.Web.HttpContext.Current.Response.Cookies["ckUserId"].Value=GetUserId(); 		
            System.Web.HttpContext.Current.Response.Cookies["ckUserId"].Expires =new DateTime(2050,12,31);
            System.Web.HttpContext.Current.Response.Cookies["ckUserId"].Path ="/" ;
		
			
            System.Web.HttpContext.Current.Response.Cookies["ckUserTypeId"].Value=GetUserTypeId();
            System.Web.HttpContext.Current.Response.Cookies["ckUserTypeId"].Expires =new DateTime(2050,12,31);

			
            System.Web.HttpContext.Current.Response.Cookies["ckUserNm"].Value=GetUserName(); 		
            System.Web.HttpContext.Current.Response.Cookies["ckUserNm"].Expires =new DateTime(2050,12,31);

            System.Web.HttpContext.Current.Response.Cookies["ckSysId"].Value=GetSystemId(); 		
            System.Web.HttpContext.Current.Response.Cookies["ckSysId"].Expires =new DateTime(2050,12,31);

            System.Web.HttpContext.Current.Response.Cookies["ckImgFld"].Value=GetImageFolder(); 		
            System.Web.HttpContext.Current.Response.Cookies["ckImgFld"].Expires =new DateTime(2050,12,31);

            System.Web.HttpContext.Current.Response.Cookies["ckClrSch"].Value=GetColorScheme(); 		
            System.Web.HttpContext.Current.Response.Cookies["ckClrSch"].Expires =new DateTime(2050,12,31);

            System.Web.HttpContext.Current.Response.Cookies["ckUserFLNm"].Value=GetUserFLName();
            System.Web.HttpContext.Current.Response.Cookies["ckUserFLNm"].Expires =new DateTime(2050,12,31);

            //<Mohit Gupta>  31-May-2005 : START : <some reason>
            System.Web.HttpContext.Current.Response.Cookies["ckGridSize"].Value=GetGridSize();
            System.Web.HttpContext.Current.Response.Cookies["ckGridSize"].Expires =new DateTime(2050,12,31);
            //<Mohit Gupta>  31-May-2005 : END :*/

        }

        /// <summary>
        /// Formats the Date according to user culture, to be used when assigning datetime model properties 
        /// </summary>
        /// <param name="date">Date as string, from page fileds</param>
        /// <returns>Culture Formatted Date as DateTime</returns>
        public DateTime ConvertToDate(string date)
        {
            if ((date.IndexOf(" ") != -1) && (date.IndexOf("-", date.IndexOf(" ")) != -1))
            {
                date = date.Substring(0, date.IndexOf(" "));
            }
            // Changed by Charles for Multilingual Implementation
            //return Convert.ToDateTime(date, System.Globalization.DateTimeFormatInfo.InvariantInfo);
            return Convert.ToDateTime(date, System.Globalization.DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Formatted date according to user culture, to be used in showing page date fields
        /// </summary>
        /// <param name="date">Date as DateTime in culture specific format</param>
        /// <returns>Culture Formatted Date as string</returns>    
        /// Added by Charles on 17-May-2010 for Multilingual Implementation
        public static string ConvertToDateCulture(DateTime date)
        {
            return Convert.ToDateTime(date, System.Globalization.DateTimeFormatInfo.CurrentInfo).ToShortDateString();
        }

        /// <summary>
        /// Converts string date object in mm/dd/yyyy format to dd/mm/yyyy format, if current culture date format is dd/mm/yyyy
        /// </summary>
        /// <param name="date">string date object in mm/dd/yyyy format</param>
        /// <returns>string date object in dd/mm/yyyy format</returns>
        /// Added by Charles for Multilingual Implementation
        public static string ConvertDBDateToCulture(string date)
        {
            return Convert.ToDateTime(Convert.ToDateTime(date, System.Globalization.DateTimeFormatInfo.InvariantInfo)
                , System.Globalization.DateTimeFormatInfo.CurrentInfo).ToShortDateString();
        }

        /// <summary>
        /// Formats the Date nodes to current culture format in XML, eg. hidOldData or string oldxml
        /// </summary>
        /// <param name="strXml">XML String</param>
        /// <param name="strNodes">Date Nodes Name Array</param>
        /// <returns>XML string with current culture formatted Date nodes</returns>
        /// Added by Charles on 19-May-10 for Multilingual Support
        public static string FormatXMLDateNode(string strXml, string[] strNodes)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNodeList xNode = null;
            try
            {
                if (strXml != null && strXml.Trim() != "" && strNodes.Length > 0)
                {
                    xDoc.LoadXml(strXml);

                    for (int i = 0; i < strNodes.Length; i++)
                    {
                        xNode = xDoc.GetElementsByTagName(strNodes[i]);

                        if (xNode != null && xNode.Count > 0 && xNode.Item(0) != null && xNode.Item(0).InnerText != "")
                        {
                            xNode.Item(0).InnerText = ConvertDBDateToCulture(xNode.Item(0).InnerText);
                        }
                    }

                    return xDoc.OuterXml.ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                xDoc = null;
                xNode = null;
            }
        }

        /*** function returns logged in agency code from session.*/
        public int GetAgencycode()
        {
            return 1;
        }

        // Set the control with the input focus
        new public void SetFocus(string controlID)
        {
            focusedControl = controlID;
        }

        private string FetchClaimStatus(string strCalimID)
        {
            DataSet ds = BusinessLayer.BLClaims.ClsReserveDetails.CheckClaimStatus(strCalimID, "-1");
            if (ds.Tables[1].Rows.Count > 0)
            {
                string strCalimStatus = ds.Tables[1].Rows[0]["CLAIM_STATUS"].ToString();
                return strCalimStatus;
            }
            else
                return "";
        }

        // Add any script code required for the SetFocus feature
        private void AddSetFocusScript()
        {
            if (focusedControl == "")
                return;

            // Add the script to declare the function
            // (Only one form in ASP.NET pages)
            StringBuilder sb = new StringBuilder("");
            sb.Append("<script language=javascript>");
            sb.Append("function ");
            sb.Append(SETFOCUSFUNCTIONNAME);
            sb.Append("(ctl) {");
            sb.Append("  if (document.forms[0][ctl] != null)");
            sb.Append("  {try {document.forms[0][ctl].focus();}catch(err){} }");
            sb.Append("}");

            // Add the script to call the function
            sb.Append(SETFOCUSFUNCTIONNAME);
            sb.Append("('");
            sb.Append(focusedControl);
            sb.Append("');<");
            sb.Append("/");   // break like this to avoid misunderstandings...
            sb.Append("script>");

            // Register the script (names are CASE-SENSITIVE)
            if (!ClientScript.IsStartupScriptRegistered(SETFOCUSSCRIPTNAME))
                ClientScript.RegisterStartupScript(this.GetType(), SETFOCUSSCRIPTNAME, sb.ToString());
        }

        // Add any script code required for trapping resize
        private void CheckResize()
        {
            if (!ClientScript.IsStartupScriptRegistered("CheckResize"))
            {
                // Add the script to declare the function
                // (Only one form in ASP.NET pages)
                StringBuilder sb = new StringBuilder("");
                sb.Append("<script language=javascript>");
                sb.Append("window.onresize=showScroll;");
                sb.Append("<");
                sb.Append("/");   // break like this to avoid misunderstandings...
                sb.Append("script>");

                // Register the script (names are CASE-SENSITIVE)
                ClientScript.RegisterStartupScript(this.GetType(), "CheckResize", sb.ToString());
            }
        }
        #region Parser for Page Render Using Page XML
        public void setPageControls(System.Web.UI.Page CallerPage, string strPageResourceFile)
        {

            string PageLangCode = GetLanguageCode() == "" ? "en-US" : GetLanguageCode();
            XmlDocument PageResource = new XmlDocument();
            try
            {
                PageResource.Load(strPageResourceFile);
            }
            catch (Exception ex)
            { throw (new Exception("Exception : Not a valid Page Resource file", ex)); }
            string PageTitle = PageResource.SelectSingleNode("Root").SelectSingleNode("PageTitle/Culture[@Code='" + PageLangCode + "']").InnerText;
            string ElementsLayout = PageResource.SelectSingleNode("Root").SelectSingleNode("ElementsLayout") != null ? PageResource.SelectSingleNode("Root").SelectSingleNode("ElementsLayout").InnerText : "";
            //XmlNode PageLayout = PageResource.SelectSingleNode("Root").SelectSingleNode("ElementsLayout");
            //string strElementWidth = PageLayout.Attributes["Element_Width"].Value;
            //string strCaptionWidth = PageLayout.Attributes["Caption_Width"].Value;
            //Page.Title = PageTitle;
            XmlNodeList PageControls = PageResource.SelectSingleNode("Root").SelectNodes("PageElement");
            string ctlName, ctlType, strCap, strDefaultValue, IsMand, rfvMessage, IsReadOnly, IsDisabled, IsFormatingRequired, JsFunctionToFormat, OnClickJSFunction, OnChangeJSFunction, MaxSize, MaxLength, ParentTableCell,OnKeyPress,OnPaste; 
            string IsDisplay, revExpEnabled, revExpKey, revExpMessage, csvEnabled, csvMessage, csvValidationFunction, leftPosition, topPosiion, strCSS, strPosition, cpvEnabled, cpvMessage, cpvOperator, cpvControlToValidate, cpvType, cpvControlToCompare, cpvValueToCompare;
            foreach (XmlNode Ctl in PageControls)
            {
                revExpEnabled = revExpKey = csvEnabled = csvValidationFunction = cpvEnabled = cpvMessage= cpvOperator = cpvControlToCompare = cpvControlToValidate = cpvValueToCompare = cpvType="";
                ctlName = Ctl.Attributes["name"].Value;
                ctlType = Ctl.Attributes["type"].Value;

                IsMand = Ctl.Attributes["IsMandatory"] != null ? Ctl.Attributes["IsMandatory"].InnerText : "";
                IsReadOnly = Ctl.Attributes["IsReadOnly"] != null ? Ctl.Attributes["IsReadOnly"].InnerText : "";
                IsDisabled = Ctl.Attributes["IsDisabled"] != null ? Ctl.Attributes["IsDisabled"].InnerText : "";
                IsFormatingRequired = Ctl.Attributes["IsFormatingRequired"] != null ? Ctl.Attributes["IsFormatingRequired"].InnerText : "";
                JsFunctionToFormat = Ctl.Attributes["JsFunctionToFormat"] != null ? Ctl.Attributes["JsFunctionToFormat"].InnerText : "";
                OnClickJSFunction = Ctl.Attributes["OnClickJSFunction"] != null ? Ctl.Attributes["OnClickJSFunction"].InnerText : "";
                OnChangeJSFunction = Ctl.Attributes["OnChangeJSFunction"] != null ? Ctl.Attributes["OnChangeJSFunction"].InnerText : "";
                IsDisplay = Ctl.Attributes["IsDisplay"] != null ? Ctl.Attributes["IsDisplay"].InnerText : "";
                MaxLength = Ctl.Attributes["MaxLength"] != null ? Ctl.Attributes["MaxLength"].InnerText : "";
                MaxSize = Ctl.Attributes["MaxSize"] != null ? Ctl.Attributes["MaxSize"].InnerText : "";
                OnKeyPress = Ctl.Attributes["OnKeyPress"] != null ? Ctl.Attributes["OnKeyPress"].InnerText : "";
                OnPaste = Ctl.Attributes["OnPaste"] != null ? Ctl.Attributes["OnPaste"].InnerText : ""; ;
                //initialze Regular Expression attributes
                XmlNode RevCtl = Ctl.SelectSingleNode("rev");
                if (RevCtl != null)
                {
                    revExpEnabled = RevCtl.Attributes["Enabled"] != null ? RevCtl.Attributes["Enabled"].InnerText : "";
                    revExpKey = RevCtl.Attributes["ExpKey"] != null ? RevCtl.Attributes["ExpKey"].InnerText : "";
                }
                //initialze Custom validator attributes
                XmlNode CsvCtl = Ctl.SelectSingleNode("csv");
                if (CsvCtl != null)
                {
                    csvEnabled = CsvCtl.Attributes["Enabled"] != null ? CsvCtl.Attributes["Enabled"].InnerText : "";
                    csvValidationFunction = CsvCtl.Attributes["ValidationFunction"] != null ? CsvCtl.Attributes["ValidationFunction"].InnerText : "";
                }
                XmlNode CpvCtl = Ctl.SelectSingleNode("cpv");
                if (CpvCtl != null)
                {
                    cpvEnabled  = CpvCtl.Attributes["Enabled"] != null ? CpvCtl.Attributes["Enabled"].InnerText : "";
                    cpvOperator = CpvCtl.Attributes["Operator"] != null ? CpvCtl.Attributes["Operator"].InnerText : "";
                    cpvType = CpvCtl.Attributes["Type"] != null ? CpvCtl.Attributes["Type"].InnerText : "";
                    cpvControlToValidate = CpvCtl.Attributes["ControlToValidate"] != null ? CpvCtl.Attributes["ControlToValidate"].InnerText : "";
                    cpvControlToCompare = CpvCtl.Attributes["ControlToCompare"] != null ? CpvCtl.Attributes["ControlToCompare"].InnerText : "";
                    cpvValueToCompare= CpvCtl.Attributes["ValueToCompare"] != null ? CpvCtl.Attributes["ValueToCompare"].InnerText : ""; 
                }
                //Geting Control Style
                XmlNode CtlStyle = Ctl.SelectSingleNode("Style");
                leftPosition = topPosiion = strCSS = strPosition = ParentTableCell = "";
                if (CtlStyle != null)
                {
                    leftPosition = CtlStyle.Attributes["Top"] != null ? CtlStyle.Attributes["Top"].InnerText : "";
                    topPosiion = CtlStyle.Attributes["Left"] != null ? CtlStyle.Attributes["Left"].InnerText : "";
                    strCSS = CtlStyle.Attributes["CssClass"] != null ? CtlStyle.Attributes["CssClass"].InnerText : "midcolora";
                    strPosition = CtlStyle.Attributes["Position"] != null ? CtlStyle.Attributes["Position"].InnerText : "Absolute";
                    ParentTableCell = CtlStyle.Attributes["ParentTableCell"] != null ? CtlStyle.Attributes["ParentTableCell"].InnerText : "";
                }
                //Fetching Culture Specific Message and Labels
                strCap = Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/Caption") != null ? Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/Caption").InnerText : "";
                strDefaultValue = Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/DefaultValue") != null ? Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/DefaultValue").InnerText : "";
                rfvMessage = Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/rfvMessage") != null ? Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/rfvMessage").InnerText : "";
                revExpMessage = Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/revExpMessage") != null ? Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/revExpMessage").InnerText : "";
                csvMessage = Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/csvMessage") != null ? Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/csvMessage").InnerText : "";
                cpvMessage = Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/cpvMessage") != null ? Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/cpvMessage").InnerText : "";
                WC.WebControl ctl = null;
                #region if control not exists on the page then create the control/element
                if (CallerPage.FindControl(ctlName) == null)
                {
                    if (ctlType == "TextBox")
                    {
                        WC.TextBox ctl1 = new System.Web.UI.WebControls.TextBox();
                        ctl1.Text = strDefaultValue;

                        WC.Label cap = new System.Web.UI.WebControls.Label();
                        cap.ID = "cap" + ctlName.Substring(3);
                        cap.Text = strCap != "" ? strCap : cap.Text;

                        //CallerPage.Form.Controls.Add(new LiteralControl("<br>"));
                        //CallerPage.Form.Controls.Add(cap);
                        //LiteralControl dv = new LiteralControl("<div>");
                        System.Web.UI.HtmlControls.HtmlGenericControl dv = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                        dv.ID = "dv" + ctlName.Substring(3);
                        dv.Style["Position"] = strPosition; // "Absolute";
                        dv.Style["Top"] = topPosiion + "px";
                        dv.Style["Left"] = leftPosition + "px";
                        dv.Style["Class"] = strCSS;
                        dv.Controls.Add(cap);

                        ctl1.Text = strDefaultValue;
                        ctl1.ReadOnly = IsReadOnly == "Yes" ? true : false;
                        ctl1.ID = ctlName;
                        ctl1.Enabled = true;
                        ctl1.Style["Position"] = "Absolute";
                        ctl1.MaxLength = 10;
                        ctl1.Style["size"] = "10";
                        //CallerPage.Form.Controls.Add(rev);
                        //CallerPage.Form.Controls.Add(new LiteralControl("<br>"));
                        //CallerPage.Form.Controls.Add(ctl1);
                        dv.Controls.Add(new LiteralControl("<br>"));
                        dv.Controls.Add(ctl1);
                        CallerPage.Form.Controls.Add(dv);
                        ctl = ctl1;
                    }

                }
                #endregion
                if (CallerPage.FindControl(ctlName) != null)
                {
                    //WC.WebControl ctl=null;
                    if (ctlType == "TextBox")
                    {
                        WC.TextBox ctl1 = (WC.TextBox)CallerPage.FindControl(ctlName);
                        ctl1 = (WC.TextBox)CallerPage.FindControl(ctlName);
                        ctl1.Text = strDefaultValue;
                        ctl1.ReadOnly = IsReadOnly == "Yes" ? true : false;
                        if (MaxLength != "")
                            ctl1.MaxLength = int.Parse(MaxLength);
                        if (MaxSize != "")
                            ctl1.Attributes.Add("size", MaxSize);
                        ctl = ctl1;

                    }
                    else if (ctlType == "TR")
                    {
                        System.Web.UI.HtmlControls.HtmlTableRow tr = (System.Web.UI.HtmlControls.HtmlTableRow)CallerPage.FindControl(ctlName);

                        if (IsDisplay == "Yes")
                        {
                            tr.Attributes.Add("style", "display:block");
                        }
                        else
                        {
                            tr.Attributes.Add("style", "display:none");
                        }
                    }
                    else if (ctlType == "DDL")
                    {
                        WC.DropDownList ctl1 = (WC.DropDownList)CallerPage.FindControl(ctlName);
                        XmlNode DBObject = Ctl.SelectSingleNode("Bindings/DataSource");
                        string DBobjectName, objectType, SourceTextField, SourceValueField, whereClause;
                        if (DBObject != null)
                        {
                            DBobjectName = DBObject.Attributes["name"].InnerText;
                            if (DBobjectName != "")
                            {
                                objectType = DBObject.Attributes["type"].Value;
                                SourceTextField = DBObject.Attributes["TextField"] != null ? DBObject.Attributes["TextField"].InnerText : "";
                                SourceValueField = DBObject.Attributes["ValueField"] != null ? DBObject.Attributes["ValueField"].InnerText : "";
                                whereClause = DBObject.Attributes["WhereClause"] != null ? DBObject.Attributes["WhereClause"].InnerText : "";
                                XmlNodeList Params = DBObject.SelectNodes("Params");
                                Hashtable ParamList = new Hashtable();
                                foreach (XmlNode Param in Params)
                                {
                                    string ParamName = Param.Attributes["name"] != null ? Param.Attributes["name"].InnerText : "";
                                    string ParamValue = Param.Attributes["Value"] != null ? Param.Attributes["Value"].InnerText : "";
                                    if (ParamName != "")
                                        ParamList.Add(ParamName, ParamValue);
                                }
                                try
                                {
                                    ctl1.DataSource = (new ClsCommon()).GetDDLDataSource(DBobjectName, objectType, SourceTextField, SourceValueField, whereClause, ParamList);
                                    ctl1.DataTextField = SourceTextField;
                                    ctl1.DataValueField = SourceValueField;
                                    ctl1.DataBind();
                                    ctl1.Items.Insert(0, "");
                                    ctl1.SelectedIndex = -1;
                                }
                                catch { }
                            }
                        }
                        //Added by Kuldeep for TFS # 834 (To display Drop down for Country for Singapore)
                        if (IsDisplay == "Yes")
                        {
                            ctl1.Attributes.Add("style", "display:block");
                        }
                        else if (IsDisplay == "No")
                            ctl1.Attributes.Add("style", "display:none");
                        ctl1.SelectedIndex = ctl1.Items.IndexOf(ctl1.Items.FindByValue(strDefaultValue));
                        if (MaxSize != "")
                            ctl1.Style.Add("width", MaxSize);
                        ctl = ctl1;
                    }
//add by Rahul Dwivedi on 06 dec 2011 for Listbox control
                    else if (ctlType == "LST")
                    {
                        WC.ListBox ctl1 = (WC.ListBox)CallerPage.FindControl(ctlName);
                        XmlNode DBObject = Ctl.SelectSingleNode("Bindings/DataSource");
                        string DBobjectName, objectType, SourceTextField, SourceValueField, whereClause;
                        if (DBObject != null)
                        {
                            DBobjectName = DBObject.Attributes["name"].InnerText;
                            if (DBobjectName != "")
                            {
                                objectType = DBObject.Attributes["type"].Value;
                                SourceTextField = DBObject.Attributes["TextField"] != null ? DBObject.Attributes["TextField"].InnerText : "";
                                SourceValueField = DBObject.Attributes["ValueField"] != null ? DBObject.Attributes["ValueField"].InnerText : "";
                                whereClause = DBObject.Attributes["WhereClause"] != null ? DBObject.Attributes["WhereClause"].InnerText : "";
                                XmlNodeList Params = DBObject.SelectNodes("Params");
                                Hashtable ParamList = new Hashtable();
                                foreach (XmlNode Param in Params)
                                {
                                    string ParamName = Param.Attributes["name"] != null ? Param.Attributes["name"].InnerText : "";
                                    string ParamValue = Param.Attributes["Value"] != null ? Param.Attributes["Value"].InnerText : "";
                                    if (ParamName != "")
                                        ParamList.Add(ParamName, ParamValue);
                                }
                                try
                                {
                                    ctl1.DataSource = (new ClsCommon()).GetDDLDataSource(DBobjectName, objectType, SourceTextField, SourceValueField, whereClause, ParamList);
                                    ctl1.DataTextField = SourceTextField;
                                    ctl1.DataValueField = SourceValueField;
                                    ctl1.DataBind();
                                    ctl1.Items.Insert(0, "");
                                    ctl1.SelectedIndex = -1;
                                }
                                catch { }
                            }
                        }
                        ctl1.SelectedIndex = ctl1.Items.IndexOf(ctl1.Items.FindByValue(strDefaultValue));
                        if (MaxSize != "")
                            ctl1.Style.Add("width", MaxSize);
                        ctl = ctl1;
                    }
                    else if (ctlType == "CmsButton")
                    {
                        Cms.CmsWeb.Controls.CmsButton ctl1 = (Cms.CmsWeb.Controls.CmsButton)CallerPage.FindControl(ctlName);
                        ctl1.Text = strCap != "" ? strCap : ctl1.Text;
                        ctl = ctl1;
                    }
                    else if (ctlType == "Label")
                    {
                        WC.Label ctl1 = (WC.Label)CallerPage.FindControl(ctlName);
                        ctl1.Text = strDefaultValue != "" ? strDefaultValue : ctl1.Text;
                        //sneha for iTrack 834
                        if (IsDisplay == "Yes")
                        {
                            ctl1.Attributes.Add("style", "display:block");
                        }
                        else if (IsDisplay == "No")
                            ctl1.Attributes.Add("style", "display:none");
                        // END
                        ctl = ctl1;
                    }
                    else if (ctlType == "CheckBox")
                    {
                        WC.CheckBox ctl1 = (WC.CheckBox)CallerPage.FindControl(ctlName);
                        ctl1.Text = strCap != "" ? strCap : ctl1.Text;
                        ctl1.Checked = strDefaultValue == "True" ? true : false;
                        ctl = ctl1;
                    }
                    //Added by Agniswar for Div Structure implementation
                    else if (ctlType == "CHK")
                    {
                        WC.CheckBoxList ctl1 = (WC.CheckBoxList)CallerPage.FindControl(ctlName);
                        ctl1.Text = strCap != "" ? strCap : ctl1.Text;
                        //ctl1.Checked = strDefaultValue == "True" ? true : false;
                        ctl = ctl1;
                    }
                    else if (ctlType == "HyperLink") // Added by Ruchika Chauhan on 7-Dec-2011 for TFS# 1211
                    {
                        WC.HyperLink ctl1 = (WC.HyperLink)CallerPage.FindControl(ctlName);
                        //ctl1.Text = strCap != "" ? strCap : ctl1.Text;
                        if (IsDisplay == "Yes") 
                        {
                            ctl1.Attributes.Add("style", "display:inline");
                        }
                        else if(IsDisplay == "No")
                        {
                            ctl1.Attributes.Add("style", "display:none");
                        }
                        ctl = ctl1;
                    }
                    else if (ctlType == "ANCHOR")
                    {
                        //WC.Image ctl1 = (WC.Image)CallerPage.FindControl(ctlName);
                        System.Web.UI.HtmlControls.HtmlAnchor ctl1 = (System.Web.UI.HtmlControls.HtmlAnchor)CallerPage.FindControl(ctlName);
                        if (IsDisplay == "Yes")
                        {
                            ctl1.Attributes.Add("style", "display:block");
                        }
                        else if(IsDisplay == "No")
                        {
                            ctl1.Attributes.Add("style", "display:none");
                        }
                    }
                    else if (ctlType == "select")  // Added by Ruchika Chauhan on 6-Dec-2011 for TFS# 1211
                    {                                                                     
                            System.Web.UI.HtmlControls.HtmlSelect ctl1 = (System.Web.UI.HtmlControls.HtmlSelect)CallerPage.FindControl(ctlName);                           
                            ctl1.Attributes.Add("style", "display:block");
                                                                       
                            WC.RequiredFieldValidator rfv = (WC.RequiredFieldValidator)CallerPage.FindControl("rfv" + ctlName.Substring(3));
                            System.Web.UI.HtmlControls.HtmlGenericControl spn = (System.Web.UI.HtmlControls.HtmlGenericControl)CallerPage.FindControl("spn" + ctlName.Substring(3));
                            if (IsMand == "No" && rfv!=null)
                            {
                                
                                if (spn != null) spn.Attributes.Add("style", "display:none");

                                rfv.ID = "rfv" + ctlName.Substring(3);
                                rfv.Enabled = false;
                                rfv.ErrorMessage = "";
                            }

                            if (IsDisabled == "Yes")
                            {
                                ctl1.Attributes.Add("Disabled", "true");
                            }

                            ctl = null;
                    }
                    else if (ctlType == "RadioButton")
                    {
                        WC.RadioButton ctl1 = (WC.RadioButton)CallerPage.FindControl(ctlName);
                        ctl1.Text = strCap != "" ? strCap : ctl1.Text;
                        ctl = ctl1;
                    }
                    else if (ctlType == "file")
                    {
                        //added by Amit k. mishra

                        if (IsDisplay == "Yes")
                        {
                            System.Web.UI.HtmlControls.HtmlInputFile ctl1 = (System.Web.UI.HtmlControls.HtmlInputFile)CallerPage.FindControl(ctlName);
                            WC.Label cap = (WC.Label)CallerPage.FindControl("cap" + ctlName.Substring(3));
                            if (cap != null) cap.Text = strCap != "" ? strCap : cap.Text;
                            ctl1.Attributes.Add("style", "display:block");
                        }
                        else if (IsDisplay == "No")
                        {
                            System.Web.UI.HtmlControls.HtmlInputFile ctl1 = (System.Web.UI.HtmlControls.HtmlInputFile)CallerPage.FindControl(ctlName);
                            if (ctl1 != null)
                            {
                                ctl1.Attributes.Add("style", "display:none");
                                WC.Label cap = (WC.Label)CallerPage.FindControl("cap" + ctlName.Substring(3));
                                if (cap != null) cap.Attributes.Add("style", "display:none");
                                WC.Label cap1 = (WC.Label)CallerPage.FindControl("lbl" + ctlName.Substring(3));
                                if (cap1 != null) cap1.Attributes.Add("style", "display:none");
                            }

                        }
                        if (IsDisabled == "Yes")
                        {
                            System.Web.UI.HtmlControls.HtmlInputFile ctl1 = (System.Web.UI.HtmlControls.HtmlInputFile)CallerPage.FindControl(ctlName);
                            WC.Label cap = (WC.Label)CallerPage.FindControl("cap" + ctlName.Substring(3));
                            if (cap != null) cap.Text = strCap != "" ? strCap : cap.Text;
                            ctl1.Attributes.Add("Disabled", "true");

                        }
                    }
                    if (ctl != null)
                    {
                        //ctl.Attributes.Add("style", "width:"+strElementWidth+"%");

                        System.Web.UI.HtmlControls.HtmlGenericControl dv = (System.Web.UI.HtmlControls.HtmlGenericControl)CallerPage.FindControl("dv" + ctlName.Substring(3));
                        if (dv == null)
                            dv = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                        dv.ID = "dv" + ctlName.Substring(3);
                        System.Web.UI.HtmlControls.HtmlTableCell td = (System.Web.UI.HtmlControls.HtmlTableCell)CallerPage.FindControl(ParentTableCell);
                        ctl.Enabled = IsDisabled == "Yes" ? false : true;
                         WC.Label cap = (WC.Label)CallerPage.FindControl("cap" + ctlName.Substring(3));
                         if (cap != null)
                         {
                             cap.Text = strCap != "" ? strCap : cap.Text;
                             //cap.Attributes.Add("style", "width:"+strCaptionWidth+"%");
                             if (td!=null) dv.Controls.Add(cap);
                         }
                        WC.RequiredFieldValidator rfv = (WC.RequiredFieldValidator)CallerPage.FindControl("rfv" + ctlName.Substring(3));
                        if (rfv != null && IsMand == "Yes")
                        {
                            rfv.Enabled = true;
                            rfv.ErrorMessage = rfvMessage;
                            System.Web.UI.HtmlControls.HtmlGenericControl spn = (System.Web.UI.HtmlControls.HtmlGenericControl)CallerPage.FindControl("spn" + ctlName.Substring(3));
                            if (spn == null)
                                spn = new System.Web.UI.HtmlControls.HtmlGenericControl("span");
                            spn.ID = "spn" + ctlName.Substring(3);
                            spn.InnerText = "*";
                            spn.Attributes.Add("Class", "mandatory");
                            if (td != null) dv.Controls.Add(spn);
                        }
                        else if (rfv != null && IsMand == "No")
                        {
                            rfv.Enabled = false;
                            System.Web.UI.HtmlControls.HtmlGenericControl spn = (System.Web.UI.HtmlControls.HtmlGenericControl)CallerPage.FindControl("spn" + ctlName.Substring(3));
                            if (spn != null) spn.Attributes.Add("style", "display:none");
                        }
                        else if (rfv == null && IsMand == "Yes")
                        {
                            System.Web.UI.HtmlControls.HtmlGenericControl spn = (System.Web.UI.HtmlControls.HtmlGenericControl)CallerPage.FindControl("spn" + ctlName.Substring(3));
                            if (spn == null)
                                spn = new System.Web.UI.HtmlControls.HtmlGenericControl("span");
                            spn.ID = "spn" + ctlName.Substring(3);
                            spn.InnerText = "*";
                           /* spn.Style["Position"] = strPosition; */
                            spn.Attributes.Add("Class", "mandatory");
                            //if (cap != null) cap.Parent.Controls.Add(spn);
                           

                            rfv = new System.Web.UI.WebControls.RequiredFieldValidator();
                            rfv.ID = "rfv" + ctlName.Substring(3);
                            rfv.Enabled = true;
                            rfv.ErrorMessage = rfvMessage;
                            rfv.ControlToValidate = ctlName;
                            rfv.Display = WC.ValidatorDisplay.Dynamic;
                            //rfv.Style["Position"] = "Absolute";
                            //CallerPage.Form.Controls.Add(rfv);
                            if (td == null)
                            {
                                ctl.Parent.Controls.Add(rfv);
                                ctl.Parent.Controls.Add(new LiteralControl("<br>"));
                            }
                            else
                                dv.Controls.Add(spn);
                           
                        }
                        if(rfv==null && IsMand=="No" )
                        {
                            System.Web.UI.HtmlControls.HtmlGenericControl spn = (System.Web.UI.HtmlControls.HtmlGenericControl)CallerPage.FindControl("spn" + ctlName.Substring(3));
                            if (spn != null) spn.Attributes.Add("style", "display:none");                                                
                        }
                        if (ElementsLayout.ToUpper() == "TOPBOTTOM")
                            dv.Controls.Add(new LiteralControl("<br>"));
                        if (td != null) dv.Controls.Add(ctl);
                        if (rfv != null && td != null)
                        {
                            dv.Controls.Add(new LiteralControl("<br>"));
                            dv.Controls.Add(rfv);
                        }
                        WC.RegularExpressionValidator rev = (WC.RegularExpressionValidator)Page.FindControl("rev" + ctlName.Substring(3));
                        if (rev != null)
                        {
                            if (revExpEnabled == "Yes")
                            {
                                rev.ValidationExpression = revExpKey != "" ? revExpKey : rev.ValidationExpression;
                                rev.ErrorMessage = revExpMessage != "" ? revExpMessage : rev.ErrorMessage;
                                rev.Enabled = true;
                            }
                            else if (revExpEnabled == "No")
                                rev.Enabled = false;
                        }
                        else if (rev == null && revExpEnabled == "Yes")
                        {
                            rev = new System.Web.UI.WebControls.RegularExpressionValidator();
                            rev.ID = "rev" + ctlName.Substring(3);
                            rev.Enabled = true;
                            rev.ErrorMessage = revExpMessage;
                            rev.ValidationExpression = revExpKey;
                            rev.ControlToValidate = ctlName;
                            rev.Display = WC.ValidatorDisplay.Dynamic;
                            //rev.Style["Position"] = "Absolute";
                            //CallerPage.Form.Controls.Add(rev);
                            if (td == null)
                            {
                                ctl.Parent.Controls.Add(new LiteralControl("<br>"));
                                ctl.Parent.Controls.Add(rev);
                            }
                        }
                        if (rev != null && td != null)
                        {
                            dv.Controls.Add(new LiteralControl("<br>"));
                            dv.Controls.Add(rev);
                        }
                        WC.CompareValidator cpv = (WC.CompareValidator)Page.FindControl("cpv" + ctlName.Substring(3));
                        if (cpv != null)
                        {
                            if (cpvEnabled == "Yes")
                            {
                                cpv.ErrorMessage = cpvMessage != "" ? cpvMessage : cpv.ErrorMessage;
                                cpv.Enabled = true;
                                switch (cpvOperator)
                                {
                                    case "Equal":
                                        cpv.Operator = ValidationCompareOperator.Equal;
                                        break;

                                    case "DataTypeCheck":
                                        cpv.Operator = ValidationCompareOperator.DataTypeCheck;
                                        break;

                                    case "GreaterThan":
                                        cpv.Operator = ValidationCompareOperator.GreaterThan;
                                        break;

                                    case "GreaterThanEqual":
                                        cpv.Operator = ValidationCompareOperator.GreaterThanEqual;
                                        break;

                                    case "LessThan":
                                        cpv.Operator = ValidationCompareOperator.LessThan;
                                        break;

                                    case "LessThanEqual":
                                        cpv.Operator = ValidationCompareOperator.LessThanEqual;
                                        break;

                                    case "NotEqual":
                                        cpv.Operator = ValidationCompareOperator.NotEqual;
                                        break;

                                    default:
                                        cpv.Operator = ValidationCompareOperator.Equal;
                                        break;

                                }
                                switch (cpvType)
                                {
                                    case "String":
                                        cpv.Type = ValidationDataType.String;
                                        break;

                                    case "Integer":
                                        cpv.Type = ValidationDataType.Integer;
                                        break;

                                    case "Double":
                                        cpv.Type = ValidationDataType.Double;
                                        break;

                                    case "Date":
                                        cpv.Type = ValidationDataType.Date;
                                        break;

                                    case "Currency":
                                        cpv.Type = ValidationDataType.Currency;
                                        break;

                                    default:
                                        cpv.Type = ValidationDataType.String;
                                        break;

                                }

                                if (!string.IsNullOrEmpty(cpvControlToCompare))
                                {
                                    cpv.ControlToCompare = cpvControlToCompare;
                                }
                                else 
                                {
                                    cpv.ValueToCompare = cpvValueToCompare;
                                }
                            }
                            else if (cpvEnabled == "No")
                            {
                                cpv.Enabled = false; cpv.Attributes.Add("display", "none");
                            }
                            
                        }
                        else if (cpv == null && cpvEnabled == "Yes")
                        {
                            cpv = new System.Web.UI.WebControls.CompareValidator();
                            cpv.ID = "cpv" + ctlName.Substring(3);
                            cpv.Enabled = true;
                            cpv.ErrorMessage = cpvMessage;
                            switch (cpvOperator)
                            {
                                case "Equal":
                                    cpv.Operator = ValidationCompareOperator.Equal;
                                    break;

                                case "DataTypeCheck":
                                    cpv.Operator = ValidationCompareOperator.DataTypeCheck;
                                    break;

                                case "GreaterThan":
                                    cpv.Operator = ValidationCompareOperator.GreaterThan;
                                    break;

                                case "GreaterThanEqual":
                                    cpv.Operator = ValidationCompareOperator.GreaterThanEqual;
                                    break;

                                case "LessThan":
                                    cpv.Operator = ValidationCompareOperator.LessThan;
                                    break;

                                case "LessThanEqual":
                                    cpv.Operator = ValidationCompareOperator.LessThanEqual;
                                    break;

                                case "NotEqual":
                                    cpv.Operator = ValidationCompareOperator.NotEqual;
                                    break;

                                default:
                                    cpv.Operator = ValidationCompareOperator.Equal;
                                    break;

                            }
                            if (!string.IsNullOrEmpty(cpvControlToCompare))
                            {
                                cpv.ControlToCompare = cpvControlToCompare;
                            }             
                            cpv.ControlToValidate = ctlName;
                            cpv.Display = WC.ValidatorDisplay.Dynamic;
                            //cpv.Style["Position"] = "Absolute";
                            //CallerPage.Form.Controls.Add(rev);
                            if (td == null)
                            {
                                ctl.Parent.Controls.Add(new LiteralControl("<br>"));
                                ctl.Parent.Controls.Add(cpv);
                            }
                        }
                        if (cpv != null && td != null)
                        {
                            dv.Controls.Add(new LiteralControl("<br>"));
                            dv.Controls.Add(cpv);
                        }
                        WC.CustomValidator csv = (WC.CustomValidator)Page.FindControl("csv" + ctlName.Substring(3));
                        if (csv != null)
                        {
                            if (csvEnabled == "Yes")
                            {
                                csv.ClientValidationFunction = csvValidationFunction;
                                csv.ErrorMessage = csvMessage;
                                csv.Enabled = true;
                            }
                            else if (csvEnabled == "No")
                                csv.Enabled = false;
                           // System.Web.UI.HtmlControls.HtmlGenericControl spn = (System.Web.UI.HtmlControls.HtmlGenericControl)CallerPage.FindControl("spn" + ctlName.Substring(3));
                           // if (spn != null) spn.Attributes.Add("style", "display:none");
                        }
                        else if (csv == null && csvEnabled == "Yes")
                        {
                            csv = new System.Web.UI.WebControls.CustomValidator();
                            csv.ID = "csv" + ctlName.Substring(3);
                            csv.Enabled = true;
                            csv.ErrorMessage = revExpMessage;
                            csv.ClientValidationFunction = csvValidationFunction;
                            csv.ControlToValidate = ctlName;
                            csv.Display = WC.ValidatorDisplay.Dynamic;
                            //csv.Style["Position"] = "Absolute";
                            //CallerPage.Form.Controls.Add(rev);
                            if (td == null)
                            {
                                ctl.Parent.Controls.Add(new LiteralControl("<br>"));
                                ctl.Parent.Controls.Add(csv);
                            }
                        }
                        if (csv != null && td != null)
                        {
                            dv.Controls.Add(new LiteralControl("<br>"));
                            dv.Controls.Add(csv);
                        }
                       
                        if (td != null)
                            td.Controls.Add(dv);
                        if (IsDisplay == "No")
                        {
                            ctl.Attributes.Add("style", "display:none");

                            if (cap != null) cap.Attributes.Add("style", "display:none");
                            if (rfv != null) rfv.Enabled = false;
                            if (rev != null) rev.Enabled = false;
                            if (cpv != null) cpv.Enabled = false;
                            if (csv != null) csv.Enabled = false;
                        }

                        //else
                        //{
                        //    if (IsDisabled == "No")
                        //    {
                        //        ctl.Attributes.Add("style", "display:Inline");

                        //        if (cap != null) cap.Attributes.Add("style", "display:Inline");
                        //        if (rfv != null) rfv.Enabled = true;
                        //        if (rev != null) rev.Enabled = true;
                        //        if (cpv != null) cpv.Enabled = true;
                        //        if (csv != null) csv.Enabled = true;
                        //    }
                        //}
                        if (IsFormatingRequired == "Yes" && JsFunctionToFormat != "")
                            ctl.Attributes.Add("onblur", "javascript:" + JsFunctionToFormat + ";");
                        if (OnClickJSFunction != "")
                            ctl.Attributes.Add("onclick", "javascript:" + OnClickJSFunction + ";");
                        if (OnChangeJSFunction != "")
                            ctl.Attributes.Add("onchange", "javascript:" + OnChangeJSFunction + ";");
                        if (OnKeyPress != "")
                            ctl.Attributes.Add("onkeypress", "javascript:" + OnKeyPress+";");
                        else
                            ctl.Attributes.Add("onkeypress","");
                        if (OnPaste != "")
                            ctl.Attributes.Add("onpaste", "javascript:" + OnPaste+";");
                        else
                            ctl.Attributes.Add("onpaste","");
                    }

                }
            }
            PageResource = null;
        }

        #endregion

        #region Parser to Render Grid Using XML

        public void SetDBGrid(BaseDataGrid objDBGrid,string strXMLFilePath,string strCalledFor)
        {
            try
            {
                string PageLangCode = GetLanguageCode() == "" ? "en-US" : GetLanguageCode();
                XmlDocument PageResource = new XmlDocument();
                try
                {
                    PageResource.Load(strXMLFilePath);
                }
                catch (Exception ex)
                { 
                    throw (new Exception("Exception : Not a valid Page Xml file", ex)); 
                }

                XmlNode xParentNode = PageResource.SelectSingleNode("Root/PageCalledFor");

                if (xParentNode == null)
                {
                    throw (new Exception("Exception : Root Node is not present in Xml"));
                }
                else
                {
                    XmlNode xCurrentNode = xParentNode.SelectSingleNode("HeaderString/value[@Culture='" + PageLangCode + "']");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.HeaderString = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("SearchColumnHeadings/value[@Culture='" + PageLangCode + "']");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.SearchColumnHeadings = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("DisplayColumnHeadings/value[@Culture='" + PageLangCode + "']");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.DisplayColumnHeadings = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("SearchMessage/value[@Culture='" + PageLangCode + "']");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.SearchMessage = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("FilterLabel/value[@Culture='" + PageLangCode + "']");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.FilterLabel = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("SearchColumnNames/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.SearchColumnNames = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("SearchColumnType/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.SearchColumnType = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("DisplayColumnNumbers/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.DisplayColumnNumbers = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("DisplayColumnNames/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.DisplayColumnNames = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("DisplayTextLength/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.DisplayTextLength = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("DisplayColumnPercent/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.DisplayColumnPercent = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("FetchColumns/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.FetchColumns = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("ColumnTypes/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.ColumnTypes = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("FilterColumnName/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.FilterColumnName = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("SelectClause/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.SelectClause = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("DropDownColumns/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.DropDownColumns = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("FromClause/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.FromClause = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("WhereClause/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.WhereClause = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("DropDownColumns/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.DropDownColumns = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("PrimaryColumns/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.PrimaryColumns = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("PrimaryColumnsName/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.PrimaryColumnsName = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("OrderByClause/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.OrderByClause = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("AllowDBLClick/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.AllowDBLClick = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("ExtraButtons/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.ExtraButtons = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("RequireQuery/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.RequireQuery = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("QueryStringColumns/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.QueryStringColumns = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("DefaultSearch/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.DefaultSearch = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("FilterValue/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.FilterValue = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("ColumnsLink/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.ColumnsLink = xCurrentNode.InnerText;
                    }

                    xCurrentNode = xParentNode.SelectSingleNode("GroupQueryColumns/value");

                    if (xCurrentNode != null)
                    {
                        objDBGrid.GroupQueryColumns = xCurrentNode.InnerText;
                    }

                }     
      
                //Check for Other Caller Types

                if (strCalledFor != "")
                {

                    xParentNode = PageResource.SelectSingleNode("Root/PageCalledFor[@TypeID='" + strCalledFor + "']");

                    if (xParentNode == null)
                    {
                        throw (new Exception("Exception : Root Node is not present in Xml"));
                    }
                    else
                    {
                        XmlNode xCurrentNode = xParentNode.SelectSingleNode("HeaderString/value[@Culture='" + PageLangCode + "']");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.HeaderString = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("SearchColumnHeadings/value[@Culture='" + PageLangCode + "']");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.SearchColumnHeadings = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("DisplayColumnHeadings/value[@Culture='" + PageLangCode + "']");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.DisplayColumnHeadings = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("SearchMessage/value[@Culture='" + PageLangCode + "']");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.SearchMessage = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("FilterLabel/value[@Culture='" + PageLangCode + "']");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.FilterLabel = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("SearchColumnNames/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.SearchColumnNames = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("SearchColumnType/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.SearchColumnType = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("DisplayColumnNumbers/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.DisplayColumnNumbers = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("DisplayColumnNames/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.DisplayColumnNames = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("DisplayTextLength/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.DisplayTextLength = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("DisplayColumnPercent/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.DisplayColumnPercent = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("FetchColumns/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.FetchColumns = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("ColumnTypes/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.ColumnTypes = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("FilterColumnName/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.FilterColumnName = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("SelectClause/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.SelectClause = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("FromClause/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.FromClause = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("WhereClause/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.WhereClause = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("PrimaryColumns/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.PrimaryColumns = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("PrimaryColumnsName/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.PrimaryColumnsName = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("OrderByClause/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.OrderByClause = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("AllowDBLClick/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.AllowDBLClick = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("ExtraButtons/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.ExtraButtons = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("RequireQuery/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.RequireQuery = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("QueryStringColumns/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.QueryStringColumns = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("DefaultSearch/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.DefaultSearch = xCurrentNode.InnerText;
                        }

                        xCurrentNode = xParentNode.SelectSingleNode("FilterValue/value");

                        if (xCurrentNode != null)
                        {
                            objDBGrid.FilterValue = xCurrentNode.InnerText;
                        }

                    }

                }
               
            
            }
            catch (Exception ex)
            { throw (ex); }

        }

        #endregion

        public string GetScreenIDForLOB(string strLOBID)
        {
            string LOBID;
            switch (strLOBID)
            {
                case "1": // HOME
                    LOBID = "224_1";
                    break;
                case "2": // Private passenger automobile
                    LOBID = "224_2";
                    break;
                case "3": // Motorcycle
                    LOBID = "224_3";
                    break;
                case "4": // Watercraft
                    LOBID = "224_4";
                    break;
                case "5": // Umbrella
                    LOBID = "224_6";
                    break;
                case "6": // Rental dwelling
                    LOBID = "224_5";
                    break;
                case "7": // General liability
                    LOBID = "224_7";
                    break;
                case "8": // Aviation
                    LOBID = "224_13";
                    break;
                case "9"://All Risks and Named Perils
                    LOBID = "224_14";
                    break;
                case "10"://Comprehensive Condominium
                    LOBID = "224_15";
                    break;
                case "11"://Comprehensive Company
                    LOBID = "224_16";
                    break;
                case "12"://General Civil Liability
                    LOBID = "224_17";
                    break;
                case "13"://Maritime
                    LOBID = "224_18";
                    break;
                case "14"://Diversified Risks
                    LOBID = "224_19";
                    break;
                case "15"://Individual Personal Accident
                    LOBID = "224_20";
                    break;
                case "16"://Robbery
                    LOBID = "224_21";
                    break;
                case "17"://Facultative Liability
                    LOBID = "224_22";
                    break;
                case "18"://Civil Liability Transportation
                    LOBID = "224_23";
                    break;
                case "19"://Dwelling
                    LOBID = "224_24";
                    break;
                case "20"://National Cargo Transport
                    LOBID = "224_27";
                    break;
                //Added By Pradeep Kushwaha on 28-April-2010
                case "21"://Group Passenger Personal Accident 
                    LOBID = "224_35";
                    break;
                case "22"://Passenger Personal Accident 
                    LOBID = "224_36";
                    break;
                case "23"://International Cargo Transport 
                    LOBID = "224_37";
                    break;
                //End Added 
                case "35"://Pehor Rural Product
                    LOBID = "224_49";
                    break;
                //End Added 
                case "36"://Dpvat2(DPVAT(Cat. 1,2,9 e 10))
                    LOBID = "224_50";
                    break;
                //End Added 
                case "37"://Dpvat2(DPVAT(Cat. 1,2,9 e 10))
                    LOBID = "224_51";
                    break;
                case "38": // Motor Singapore
                    LOBID = "224_2";
                    break;
                case "39": // Fire Singapore
                    LOBID = "224_2";
                    break;
                case "40": // Marine Cargo Singapore
                    LOBID = "224_2";
                    break;
                //End Added 
                case "":
                case "0":
                    LOBID = "201_0";
                    break;
                default: //
                    LOBID = "";
                    break;
            }


            return LOBID; 
        }

        private XmlNode CreateTreeXml(XmlNodeList xlist, XmlNode targetNode, XmlDocument TargetDoc, XmlDocument SourceDoc)
        {
            ClsCommon obj = new ClsCommon();
            foreach (XmlNode cnode in xlist)
            {
                XmlNode xNODE = TargetDoc.CreateNode(targetNode.NodeType, targetNode.Name, targetNode.NamespaceURI);
                xNODE.InnerXml = cnode.InnerXml;

                string strHasChild = cnode.SelectSingleNode("HASCHILD").InnerText;
                string strParent = cnode.SelectSingleNode("ID").InnerText;
                string strpageUrl = cnode.SelectSingleNode("PageURL").InnerText;
                                               
                if (strHasChild == "1")
                {
                    XmlNodeList cNodeList = SourceDoc.SelectNodes("Nodes/Node[ParentID='" + strParent + "']");
                    xNODE = CreateTreeXml(cNodeList, xNODE, TargetDoc, SourceDoc);
                }
                
                
                if (!NodeCollection.Contains(xNODE.SelectSingleNode("ID").InnerText))
                {
                    NodeCollection.Add(strParent);
                    targetNode.AppendChild(xNODE);
                }

            }
            return targetNode;
        }

        public string GetTreeLayoutXML()
        {
            ClsCommon obj = new ClsCommon();
            XmlDocument xdoc = new XmlDocument();

            DataSet ds = obj.GetDefaultLayoutXml(GetLOBString());
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                string strTreeXml = dt.Rows[0][0].ToString();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strTreeXml);
                XmlElement elmRoot = doc.DocumentElement;
                XmlNodeList lstNodes = doc.GetElementsByTagName("Node");

                XmlDocument TreeXML = new XmlDocument();
                XmlElement rootElement = TreeXML.CreateElement("Nodes");

                foreach (XmlNode cnode in lstNodes)
                {
                    XmlNode xNODE = TreeXML.CreateNode(cnode.NodeType, cnode.Name, cnode.NamespaceURI);

                    if (!NodeCollection.Contains(cnode.SelectSingleNode("ID").InnerText))
                        rootElement.AppendChild(CreateTreeXml(lstNodes, xNODE, TreeXML, doc));
                    else
                        break;
                }

                string strXml = rootElement.OuterXml.Replace("\n", "");
                strXml = strXml.Replace("\r", "");
                //strXml = strXml.Replace(" ", "");
                strXml = strXml.Replace("<Nodes><Node>", "<Nodes>");
                strXml = strXml.Replace("</Node></Nodes>", "</Nodes>");

                return strXml;

            }
            else
                return "";



            
        }

        public string GetLayoutXML()
        {
            ClsCommon obj = new ClsCommon();

            DataSet ds = obj.GetDefaultLayoutXml(GetLOBString());
            //DataTable dt;

            string strPolicyScreenID = GetScreenIDForLOB(GetLOBID());

            string strRedirectURL = @"../../../Cms/Policies/Aspx/WebForm1.aspx";

            XmlDocument XDoc = new XmlDocument();
            // Create root node.
            XmlElement XElemRoot = XDoc.CreateElement("Nodes");

            //Add the node to the document.
            XDoc.AppendChild(XElemRoot);            

            DataView dv = new DataView(ds.Tables[0]);
            string tableFilter = "menu_id = '134'";
            dv.RowFilter = tableFilter;

                                    
            if (dv.Count > 0)
            {
                XmlElement XPNode = XDoc.CreateElement("Node");
                XElemRoot.AppendChild(XPNode);

                XmlElement XPNodeChild = XDoc.CreateElement("ID");
                XPNodeChild.InnerText = "134";
                XPNode.AppendChild(XPNodeChild);

                XPNodeChild = XDoc.CreateElement("PageName");
                XPNodeChild.InnerText = "Insured Applicant Details";
                XPNode.AppendChild(XPNodeChild);

                XPNodeChild = XDoc.CreateElement("PageURL");
                XPNodeChild.InnerText = "";
                XPNode.AppendChild(XPNodeChild);

                XPNodeChild = XDoc.CreateElement("RedirectURL");
                XPNodeChild.InnerText = strRedirectURL;
                XPNode.AppendChild(XPNodeChild);

                for (int i = 0; i < dv.Count; i++)
                {
                    string strMenuScreenID = dv[i]["SCREEN_ID"].ToString();

                    if (strMenuScreenID == "134_0" || strMenuScreenID == "134_2" || strMenuScreenID == "134_5_0")
                    {
                        XmlElement XNode = XDoc.CreateElement("Node");
                        XPNode.AppendChild(XNode);

                        XmlElement XNodeChild = XDoc.CreateElement("ID");
                        XNodeChild.InnerText = dv[i]["SCREEN_ID"].ToString();
                        XNode.AppendChild(XNodeChild);

                        XNodeChild = XDoc.CreateElement("PageName");
                        XNodeChild.InnerText = dv[i]["SCREEN_DESC"].ToString();
                        XNode.AppendChild(XNodeChild);

                        XNodeChild = XDoc.CreateElement("PageURL");
                        if (dv[i]["SCREEN_PATH"].ToString().StartsWith("/"))
                            XNodeChild.InnerText = "../../.." + dv[i]["SCREEN_PATH"].ToString();
                        else
                            XNodeChild.InnerText = "../../../" + dv[i]["SCREEN_PATH"].ToString();
                        XNode.AppendChild(XNodeChild);

                        XNodeChild = XDoc.CreateElement("RedirectURL");
                        XNodeChild.InnerText = "../../../" + strRedirectURL;
                        XNode.AppendChild(XNodeChild);

                        XNodeChild = XDoc.CreateElement("ParentID");
                        XNodeChild.InnerText = dv[i]["menu_id"].ToString();
                        XNode.AppendChild(XNodeChild);
                    }
                }

            }

            dv = new DataView(ds.Tables[0]);
            tableFilter = "menu_id = '224'";
            dv.RowFilter = tableFilter;

            // For LOB specific Policy Information Menu
            if (dv.Count > 0)
            {
                for (int i = 0; i < dv.Count; i++)
                {
                    if (dv[i]["SCREEN_ID"].ToString() == strPolicyScreenID)
                    {

                        XmlElement XNode = XDoc.CreateElement("Node");
                        XElemRoot.AppendChild(XNode);

                        XmlElement XNodeChild = XDoc.CreateElement("ID");
                        XNodeChild.InnerText = dv[i]["SCREEN_ID"].ToString();
                        XNode.AppendChild(XNodeChild);

                        XNodeChild = XDoc.CreateElement("PageName");
                        XNodeChild.InnerText = "Policy Information";
                        XNode.AppendChild(XNodeChild);

                        XNodeChild = XDoc.CreateElement("PageURL");
                        XNodeChild.InnerText = dv[i]["SCREEN_PATH"].ToString();
                        XNode.AppendChild(XNodeChild);

                        XNodeChild = XDoc.CreateElement("RedirectURL");
                        XNodeChild.InnerText = strRedirectURL;
                        XNode.AppendChild(XNodeChild);
                    }

                }

            }

            // For Billing Information Menu
            if (dv.Count > 0)
            {
                for (int i = 0; i < dv.Count; i++)
                {
                    if (dv[i]["SCREEN_ID"].ToString() == "224_11")
                    {

                        XmlElement XNode = XDoc.CreateElement("Node");
                        XElemRoot.AppendChild(XNode);

                        XmlElement XNodeChild = XDoc.CreateElement("ID");
                        XNodeChild.InnerText = dv[i]["SCREEN_ID"].ToString();
                        XNode.AppendChild(XNodeChild);

                        XNodeChild = XDoc.CreateElement("PageName");
                        XNodeChild.InnerText = dv[i]["SCREEN_DESC"].ToString();
                        XNode.AppendChild(XNodeChild);

                        XNodeChild = XDoc.CreateElement("PageURL");
                        XNodeChild.InnerText = dv[i]["SCREEN_PATH"].ToString();
                        XNode.AppendChild(XNodeChild);

                        XNodeChild = XDoc.CreateElement("RedirectURL");
                        XNodeChild.InnerText = strRedirectURL;
                        XNode.AppendChild(XNodeChild);
                    }

                }

            }

            //For Nature of Business
            XmlElement XNOBNode = XDoc.CreateElement("Node");
            XElemRoot.AppendChild(XNOBNode);

            XmlElement XNOBNodeChild = XDoc.CreateElement("ID");
            XNOBNodeChild.InnerText = "569";
            XNOBNode.AppendChild(XNOBNodeChild);

            XNOBNodeChild = XDoc.CreateElement("PageName");
            XNOBNodeChild.InnerText = "Nature of Business";
            XNOBNode.AppendChild(XNOBNodeChild);

            XNOBNodeChild = XDoc.CreateElement("PageURL");
            XNOBNodeChild.InnerText = "../../../cms/cmsweb/Construction.html";
            XNOBNode.AppendChild(XNOBNodeChild);

            XNOBNodeChild = XDoc.CreateElement("RedirectURL");
            XNOBNodeChild.InnerText = "../../../" + strRedirectURL;
            XNOBNode.AppendChild(XNOBNodeChild);

            //For Underwriter Questions

            XNOBNode = XDoc.CreateElement("Node");
            XElemRoot.AppendChild(XNOBNode);

            XNOBNodeChild = XDoc.CreateElement("ID");
            XNOBNodeChild.InnerText = "570";
            XNOBNode.AppendChild(XNOBNodeChild);

            XNOBNodeChild = XDoc.CreateElement("PageName");
            XNOBNodeChild.InnerText = "Underwrirer Question";
            XNOBNode.AppendChild(XNOBNodeChild);

            XNOBNodeChild = XDoc.CreateElement("PageURL");
            XNOBNodeChild.InnerText = "../../../cms/cmsweb/Construction.html";
            XNOBNode.AppendChild(XNOBNodeChild);

            XNOBNodeChild = XDoc.CreateElement("RedirectURL");
            XNOBNodeChild.InnerText = "../../../" + strRedirectURL;
            XNOBNode.AppendChild(XNOBNodeChild);

            //For Liability Coverages

            XNOBNode = XDoc.CreateElement("Node");
            XElemRoot.AppendChild(XNOBNode);

            XNOBNodeChild = XDoc.CreateElement("ID");
            XNOBNodeChild.InnerText = "571";
            XNOBNode.AppendChild(XNOBNodeChild);

            XNOBNodeChild = XDoc.CreateElement("PageName");
            XNOBNodeChild.InnerText = "Liability Coverages";
            XNOBNode.AppendChild(XNOBNodeChild);

            XNOBNodeChild = XDoc.CreateElement("PageURL");
            XNOBNodeChild.InnerText = "../../../cms/cmsweb/Construction.html";
            XNOBNode.AppendChild(XNOBNodeChild);

            XNOBNodeChild = XDoc.CreateElement("RedirectURL");
            XNOBNodeChild.InnerText = "../../../" + strRedirectURL;
            XNOBNode.AppendChild(XNOBNodeChild);

            //dv = new DataView(ds.Tables[0]);
            //tableFilter = "menu_id = '224'";
            //dv.RowFilter = tableFilter;

            // For Location Information Menu
            if (dv.Count > 0)
            {
                XmlElement XPNode = XDoc.CreateElement("Node");
                XElemRoot.AppendChild(XPNode);

                XmlElement XPNodeChild = XDoc.CreateElement("ID");
                XPNodeChild.InnerText = dv[0]["menu_id"].ToString(); ;
                XPNode.AppendChild(XPNodeChild);

                XPNodeChild = XDoc.CreateElement("PageName");
                XPNodeChild.InnerText = dv[0]["SCREEN_DESC"].ToString();
                XPNode.AppendChild(XPNodeChild);

                XPNodeChild = XDoc.CreateElement("PageURL");
                XPNodeChild.InnerText = "";
                XPNode.AppendChild(XPNodeChild);

                XPNodeChild = XDoc.CreateElement("RedirectURL");
                XPNodeChild.InnerText = strRedirectURL;
                XPNode.AppendChild(XPNodeChild);

                int custID = int.Parse(GetCustomerID());
                int polID = int.Parse(GetPolicyID());
                int PolVersionID = int.Parse(GetPolicyVersionID());
                DataSet dsLoc = obj.GetLocationDetails(custID, polID, PolVersionID);
                
                
                for (int i = 0; i < dv.Count; i++)
                {
                    string strMenuScreenID = dv[i]["SCREEN_ID"].ToString();

                    if (dv[i]["SCREEN_ID"].ToString() == "224_0")
                    {
                        if (dsLoc.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsLoc.Tables[0].Rows.Count; j++)
                            {
                                XmlElement XNode = XDoc.CreateElement("Node");
                                XPNode.AppendChild(XNode);

                                XmlElement XNodeChild = XDoc.CreateElement("ID");
                                XNodeChild.InnerText = dv[i]["SCREEN_ID"].ToString();
                                XNode.AppendChild(XNodeChild);

                                XNodeChild = XDoc.CreateElement("PageName");
                                XNodeChild.InnerText = dv[i]["SCREEN_DESC"].ToString() + " " +(j+1);
                                XNode.AppendChild(XNodeChild);

                                XNodeChild = XDoc.CreateElement("PageURL");
                                XNodeChild.InnerText = "../../.." + dv[i]["SCREEN_PATH"].ToString() + "?LOCATION_ID=" + dsLoc.Tables[0].Rows[i]["LOCATION_ID"].ToString();
                                XNode.AppendChild(XNodeChild);

                                XNodeChild = XDoc.CreateElement("RedirectURL");
                                XNodeChild.InnerText = "../../../" + strRedirectURL;
                                XNode.AppendChild(XNodeChild);

                                XNodeChild = XDoc.CreateElement("ParentID");
                                XNodeChild.InnerText = dv[i]["menu_id"].ToString();
                                XNode.AppendChild(XNodeChild);
                            }
                        }
                    }
                }
                    

            }
                       
            
            return XDoc.OuterXml;
        }

        #region Layout XML Parser


        #endregion

        #region security XML Code
        public string gstrSecurityXML;
        /// <summary>
        /// Returns security setting against a user for a screen
        /// </summary>
        /// <param name="screenId"></param>
        /// <param name="userId"></param>
        /// <returns>security string in XML format</returns>
        public void SetSecurityXML(string screenId, int userId)
        {
            //Check if User already logged in at a diffferent machine
            string strSessionID = "";
            DataSet ldsStatus = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr, System.Data.CommandType.Text, "Proc_GetUserLoggedStatus " + userId.ToString());
            if (ldsStatus != null && ldsStatus.Tables[0].Rows.Count > 0)
            {
                strSessionID = ldsStatus.Tables[0].Rows[0]["SESSION_ID"].ToString();
            }

            if (strSessionID != Session.SessionID.ToString() && strSessionID != "")
            {
                RedirectToLogin();
            }

            // get security xml for the user
            gstrSecurityXML = ClsSecurity.GetSecurityXML(int.Parse(GetUserId()), int.Parse(GetUserTypeId()), screenId);
            // if security xml is blank, set it to default
            if (gstrSecurityXML == "")
            {
                gstrSecurityXML = System.Configuration.ConfigurationManager.AppSettings["securityXML"];
                /*if(screenId=="211")
                    gstrSecurityXML	= "<security><Read>N</Read><Write>Y</Write><Delete>N</Delete><Execute>Y</Execute></security>";*/
            }

            //Initializing the securty
            InitializeSecuritySettings();
        }

        protected void RedirectToLogin()
        {
            //Session.Abandon();
            //System.Web.HttpContext.Current.Server.Transfer("~/cmsweb/aspx/login.aspx");
            //System.Web.HttpContext.Current.Response.Redirect("/cms/cmsweb/aspx/login.aspx?Top=1",true);
            System.Web.HttpContext.Current.Response.Redirect("/cms/cmsweb/aspx/login.aspx", true);
        }

        protected void InitializeSecuritySettings()
        {
            //Added By Ravindra (02-14-2007)
            // To prevent User from navigating directlty to a page
            /*if(Request.UrlReferrer == null || Request.UrlReferrer.AbsolutePath == null 
                    || Request.UrlReferrer.AbsolutePath.Trim() == "" )
            {
                RedirectToLogin();
            }*/
            if (gstrSecurityXML != "")
            {
                StringReader sRead = new StringReader(gstrSecurityXML);
                XmlTextReader xRead = new XmlTextReader(sRead);
                while (xRead.Read())
                {
                    if (xRead.NodeType == XmlNodeType.Element)
                    {
                        if (xRead.Name.Equals("Read"))
                        {
                            IsRead = xRead.ReadString();
                        }
                        if (xRead.Name.Equals("Write"))
                        {
                            IsWrite = xRead.ReadString();
                        }
                        if (xRead.Name.Equals("Delete"))
                        {
                            IsDelete = xRead.ReadString();
                        }
                        if (xRead.Name.Equals("Execute"))
                        {
                            IsExecute = xRead.ReadString();
                        }
                    }
                }

                if (IsRead.Equals("N"))
                {
                    System.Web.HttpContext.Current.Server.Transfer("~/cmsweb/aspx/securitymsg.aspx");
                }
                //Server.Transfer("../aspx/error.aspx") ;
            }
        }
        #endregion

        #region Common Save and Update Code
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCaller"></param>
        /// <param name="objModel"></param>
        /// <returns></returns>
        public void PopulateModelObject(System.Web.UI.Page objCaller, Model.IModelInfo objModel)
        {
            //Get the table schema from the Model object
            System.Data.DataTable TempIndt = objModel.TableInfo;

            string strValue = "";
            string strTempValue = "";
            string strID = "";
            //System.Data.DataTable TempOutdt = objModel.GetTableInfo();

            //looping through all controls
            foreach (System.Web.UI.Control cn in objCaller.Controls)
            {
                //looping through all child controls
                foreach (System.Web.UI.Control childC in cn.Controls)
                {
                    //every child control is checked against web controls
                    if (childC is WC.TextBox)	// Checking the TextBox control
                    {
                        if (childC.ID != null)
                        {
                            WC.TextBox txt = (WC.TextBox)Page.FindControl(childC.ID);
                            if (txt != null)
                            {
                                strValue = txt.Text;
                                strID = txt.ID;
                            }
                            txt.Dispose();
                        }
                    }
                    if (childC is WC.DropDownList)	// Checking the DropDownList control
                    {
                        WC.DropDownList dl = (WC.DropDownList)Page.FindControl(childC.ID);
                        if (dl != null)
                        {
                            strValue = dl.SelectedValue;
                            strID = dl.ID;
                        }
                        dl.Dispose();
                    }
                    // worked to be done
                    if (childC is WC.RadioButton)	// Checking the RadioButton control
                    {
                        WC.RadioButton rd = (WC.RadioButton)Page.FindControl(childC.ID);
                        if (rd != null)
                        {
                            strID = rd.ID;
                        }
                        rd.Dispose();
                    }
                    // worked to be done
                    if (childC is WC.CheckBox)		// Checking the CheckBox control
                    {
                        WC.CheckBox chk = (WC.CheckBox)Page.FindControl(childC.ID);
                        if (chk != null)
                        {
                            if (chk.Checked == true)
                            {
                                strID = chk.ID;
                            }
                        }
                        chk.Dispose();
                    }
                    // worked to be done
                    if (childC is WC.Label)		// Checking the Label control
                    {
                        WC.Label lbl = (WC.Label)Page.FindControl(childC.ID);
                        if (lbl != null)
                        {
                        }
                        lbl.Dispose();
                    }
                    if (strValue != "")		//Fetching the Colum Name Value
                    {

                        if (TempIndt.Columns.Contains(strID.Substring(3)))
                        {
                            // Storing the column name 
                            strTempValue = TempIndt.Columns[strID.Substring(3)].ToString();

                            // Checking the Data Type
                            switch (TempIndt.Columns[strTempValue].DataType.ToString())
                            {

                                case "System.UInt16":
                                case "System.UInt32":
                                case "System.UInt64":
                                case "System.Int16":
                                case "System.Int32":
                                case "System.Double":
                                    if (strValue.Trim() != "")
                                        TempIndt.Rows[0][strTempValue] = double.Parse(strValue);
                                    break;
                                case "System.Int64":
                                    TempIndt.Rows[0][strTempValue] = strValue;
                                    break;
                                case "System.Boolean":
                                case "System.String":
                                    TempIndt.Rows[0][strTempValue] = strValue;
                                    break;
                                case "System.DateTime":
                                    if (strValue.Trim() != "")
                                    {
                                        TempIndt.Rows[0][strTempValue] = ConvertToDate(strValue);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        strValue = "";
                        strID = "";
                    }
                }//end of child control loop
            }//end of page control loop

            objModel.TableInfo = TempIndt;
        }
        /// <summary>
        /// Overload function for filling model object from XML File
        /// </summary>
        /// <param name="objModel"></param>
        /// <param name="xmlValue"></param>
        /// <returns></returns>
        public void PopulateModelObject(Model.IModelInfo objModel, string xmlValue)
        {
            //Get the table schema from the Model object
            System.Data.DataTable TempIndt = objModel.TableInfo;

            //	
            System.Data.DataTable TempInXml = objModel.TableInfo;

            string strValue = "";

            // Creating the XML Document
            XmlDocument xmlDoc = new XmlDocument();
            // Loading the XML Value
            xmlDoc.LoadXml(xmlValue);

            //
            foreach (System.Data.DataColumn Col in TempIndt.Columns)
            {
                try
                {
                    //Initialize the variable at loop start to blank value discard old saved value
                    strValue = "";
                    if (xmlDoc.SelectNodes("//" + Col.ColumnName.ToUpper().Trim()).Count > 0)
                    {
                        strValue = xmlDoc.SelectNodes("//" + Col.ColumnName.ToUpper().Trim()).Item(0).InnerText.Trim();
                    }

                    //ClsCommon.GetNodeValue(xmlDoc,"//" +  Col.ColumnName.ToUpper()).Trim();	
                    // Checking the Data Type
                    switch (TempIndt.Columns[Col.ColumnName].DataType.ToString())
                    {
                        case "System.UInt16":
                        case "System.UInt32":
                        case "System.UInt64":
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Single":
                        case "System.Double":
                        case "System.Decimal":
                            if (strValue == "")
                            {
                                TempInXml.Rows[0][Col.ColumnName] = System.DBNull.Value;
                            }
                            else
                            {
                                TempInXml.Rows[0][Col.ColumnName] = strValue;
                            }
                            break;
                        case "System.Boolean":
                        case "System.String":
                            //if(strValue != "")
                            //{
                            strValue = ClsCommon.DecodeXMLCharacters(strValue);
                            TempInXml.Rows[0][Col.ColumnName] = strValue;
                            //}

                            break;
                        case "System.DateTime":
                            if (strValue == "")
                            {
                                TempInXml.Rows[0][Col.ColumnName] = System.DBNull.Value;
                            }
                            else
                            {
                                TempInXml.Rows[0][Col.ColumnName] = strValue;
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch(Exception ex)
                {
                    continue;
                }
            }
            strValue = "";
            objModel.TableInfo = TempInXml;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objResourceMgr"></param>
        /// <param name="objCaller"></param>
        /// <returns></returns>
        public string MapTransactionLabel(ResourceManager objResourceMgr, System.Web.UI.Page objCaller)
        {
            const string ROOTNODENAME = "LabelFieldMapping";
            const string MAPPINGNODENAME = "Map";
            XmlDocument xmlTransaction = new XmlDocument();// hold the XML string having map information for field and label
            // create root node of transactLabel
            XmlElement rootNode = xmlTransaction.CreateElement(ROOTNODENAME);
            string objectId; // hold the value of object's id for a label. Eg : if txtCUSTOMER_ID  is the name of control's id, the object id will be Customer_id
            foreach (System.Web.UI.Control cn in objCaller.Controls)
            {
                foreach (System.Web.UI.Control childCn in cn.Controls)
                {
                    if ((childCn.ID != null) && (objResourceMgr.GetString(childCn.ID) != null))
                    {
                        objectId = childCn.ID.Substring(3);
                        XmlElement labelNode = xmlTransaction.CreateElement(MAPPINGNODENAME);
                        labelNode.SetAttribute("label", objResourceMgr.GetString(childCn.ID));
                        labelNode.SetAttribute("field", objectId);
                        rootNode.AppendChild(labelNode);
                    }
                }
            }
            xmlTransaction.AppendChild(rootNode);
            return xmlTransaction.OuterXml;
        }

        /// <summary>
        /// Reads the RESX file passed as an argument and returns a Transaction XML string
        /// </summary>
        /// <param name="resourceFileName">The name of the RESX resource file to read.</param>
        /// <returns></returns>
        public string MapTransactionLabel(string resourceFileName)
        {
            const string ROOTNODENAME = "LabelFieldMapping";
            const string MAPPINGNODENAME = "Map";

            // hold the XML string having map information for field and label
            XmlDocument xmlTransaction = new XmlDocument();

            // create root node of transactLabel
            XmlElement rootNode = xmlTransaction.CreateElement(ROOTNODENAME);

            string filePath = Server.MapPath(resourceFileName);

            // Create a ResXResourceReader for the file items.resx.
            //ResXResourceReader rsxr = new ResXResourceReader(filePath);
            System.Resources.ResourceReader rsxr = new ResourceReader(filePath);

            // Create an IDictionaryEnumerator to iterate through the resources.
            IDictionaryEnumerator id = rsxr.GetEnumerator();

            // Iterate through the key value pairs and create XML 
            foreach (DictionaryEntry d in rsxr)
            {
                //Eliminate the key values starting with $
                if (d.Key.ToString().StartsWith("$") == false)
                {
                    string strFieldName = d.Key.ToString().Substring(3);
                    string strLabel = d.Value.ToString();

                    XmlElement labelNode = xmlTransaction.CreateElement(MAPPINGNODENAME);
                    labelNode.SetAttribute("label", strLabel);
                    labelNode.SetAttribute("field", strFieldName);
                    rootNode.AppendChild(labelNode);
                }

            }

            //Close the reader.
            rsxr.Close();

            xmlTransaction.AppendChild(rootNode);

            return xmlTransaction.OuterXml;

        }


        #endregion
        protected void InitialiseRegExp()
        {

            if (System.Web.HttpContext.Current.Session == null || Session["userId"] == null || Session["userId"].ToString() == "") return;

            if (GetPolicyCurrency() != String.Empty && GetPolicyCurrency() == enumCurrencyId.BR)
            {
                aRegExpCurrencyformat = ConfigurationManager.AppSettings["RegExpCurrencyFormatBrazil"];
                numberFormatInfo = new CultureInfo(enumCulture.BR, true).NumberFormat;//Added by Pradeep Kushwaha on 07-Oct-2010 to format the number according to the Policy Currency
                numberFormatInfo.NumberDecimalDigits = 4;
                aRegExpDouble = ConfigurationManager.AppSettings["RegExpDoubleBrazil"];
                aRegExpDoublePositiveWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZeroBrazil"];
                aRegExpPositiveCurrency = ConfigurationManager.AppSettings["RegExpPositiveCurrencyBrazil"];
            }

            if (GetSYSBaseCurrency() == enumCurrencyId.BR)
            {
                aRegExpBaseCurrencyformat = ConfigurationManager.AppSettings["RegExpCurrencyFormatBrazil"];
                aRegExpBaseDoublePositiveWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZeroBrazil"];
                aRegExpBaseDouble = ConfigurationManager.AppSettings["RegExpDoubleBrazil"];
                aRegExpBaseDoublePositiveNonZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroBrazil"];
                aRegExpBaseDoublePositiveStartWithDecimal = ConfigurationManager.AppSettings["RegExpDoublePositiveStartWithDecimalBrazil"];

                System.Globalization.CultureInfo oldculture = Thread.CurrentThread.CurrentCulture;
                NfiBaseCurrency = new CultureInfo(enumCulture.BR, true).NumberFormat;
                NfiBaseCurrency.NumberDecimalDigits = 4;
                Thread.CurrentThread.CurrentCulture = oldculture;
            }
            else
            {
                aRegExpBaseCurrencyformat = ConfigurationManager.AppSettings["RegExpCurrencyFormat"];
                aRegExpBaseDoublePositiveWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZero"];
                aRegExpBaseDouble = ConfigurationManager.AppSettings["RegExpDouble"];
                aRegExpBaseDoublePositiveNonZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZero"];
                aRegExpBaseDoublePositiveStartWithDecimal = ConfigurationManager.AppSettings["RegExpDoublePositiveStartWithDecimal"];
                System.Globalization.CultureInfo oldculture = Thread.CurrentThread.CurrentCulture;
                NfiBaseCurrency = new CultureInfo(enumCulture.US, true).NumberFormat;
                NfiBaseCurrency.NumberDecimalDigits = 4;
                Thread.CurrentThread.CurrentCulture = oldculture;
            }
            if (GetLanguageID() == "2" || GetLanguageID() == "3") // Line edited by Agniswar for Singapore Implementation
            {
                aRegExpCurrency = ConfigurationManager.AppSettings["RegExpCurrencyBrazil"];

                //aRegExpPositiveCurrency = ConfigurationManager.AppSettings["RegExpPositiveCurrencyBrazil"];
                aRegExpNegativeCurrency = ConfigurationManager.AppSettings["RegExpNegativeCurrencyBrazil"];

                aRegExpDoubleZeroToPositive = ConfigurationManager.AppSettings["RegExpDoubleZeroToPositiveBrazil"];
                //aRegExpDouble = ConfigurationManager.AppSettings["RegExpDoubleBrazil"];
                aRegExpDoublePositiveNonZeroStartNotZeroNonDollar = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartNotZeroNonDollarBrazil"];
                aRegExpDoublePositiveNonZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroBrazil"];
                aRegExpDoublePositiveZero = ConfigurationManager.AppSettings["RegExpDoublePositiveZeroBrazil"];
                //aRegExpDoublePositiveWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZeroBrazil"];
                aRegExpDoublePositiveNonZeroFourDeci = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroFourDeciBrazil"];
                aRegExpDoublePositiveNonZeroStartWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartWithZeroBrazil"];
                aRegExpDoublePositiveWithZeroFourDeci = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZeroFourDeciBrazil"];
                aRegExpIntegerPositiveNonZero = ConfigurationManager.AppSettings["RegExpIntegerPositiveNonZeroBrazil"];
                aRegExpDecimal = ConfigurationManager.AppSettings["RegExpDecimalBrazil"];
                aRegExpDoublePositiveNonZeroNotLessThanOne = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroNotLessThanOneBrazil"];
                aRegExpDoublePositiveNonZeroStartNotZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroStartNotZeroBrazil"];
                aRegExpDoublePositiveStartWithDecimal = ConfigurationManager.AppSettings["RegExpDoublePositiveStartWithDecimalBrazil"];
                aRegExpDoublePositiveWithMoreThanOneDecimalAndComma = ConfigurationManager.AppSettings["RegExpDoublePositiveWithMoreThanOneDecimalAndCommaBrazil"];
                aRegExpClientName = ConfigurationManager.AppSettings["RegExpClientNameBrazil"];//Added By Pradeep Kushwaha on 23-July-2010
                aRegExpAppPolicyNum = ConfigurationManager.AppSettings["RegExpAppPolicyNumBrazil"];
                aRegExpAgencyPhone = ConfigurationManager.AppSettings["RegExpAgencyPhone"];
                aRegExpDate = ConfigurationManager.AppSettings["RegExpDateBrazil"];
            }
        }
        //protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, String eventArgument)
        //{
        //    InitialiseRegExp();
        //}
        protected override void OnPreInit(EventArgs e)
        {
            InitialiseRegExp();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (base.Form != null)
            {
                string url = QueryStringModule.EncriptUrl(System.Web.HttpContext.Current);
                if (url != "")
                    base.Form.Action = url;
            }
            if (!IsPostBack)
            {
                Session["InValidateSession"] = "Y";
                if (GetPolicyStatus() == "UENDRS" && System.Web.HttpContext.Current.Session["EndorsementTranIds"] != null)// changed by praveer for TFS# 996
                {
                    Session["InValidateSession"] = "N";
                }
            }

            Response.Write("<script language ='javascript' type='text/javascript'> var aRegExpPhone = '" + aRegExpPhone.Replace("\\", "\\\\") + "' </script>");
            Response.Write("<script language ='javascript' type='text/javascript'> var aRegExpDate = '" + aRegExpDate.Replace("\\", "\\\\") + "' </script>");
            Response.Write("<script language ='javascript' type='text/javascript'> var aRegExpZip = '" + aRegExpZip.Replace("\\", "\\\\") + "' </script>");
            Response.Write("<script language ='javascript' type='text/javascript'> var aRegExpZipUS = '" + aRegExpZipUS.Replace("\\", "\\\\") + "' </script>"); //Added by Aditya for TFS BUG # 1832
            Response.Write("<script language ='javascript' type='text/javascript'> var aRegExpSSN = '" + aRegExpSSN.Replace("\\", "\\\\") + "' </script>");
            Response.Write("<script language ='javascript' type='text/javascript'> var aRegExpShortDate = '" + aRegExpShortDate.Replace("\\", "\\\\") + "' </script>");

            //Added by Charles on 19-May-10 for Multilingual Support
            if (ClsCommon.BL_LANG_ID != 0)
            {

                Response.Write("<script language ='javascript' type='text/javascript'> var iLangID = '" + ClsCommon.BL_LANG_ID + "' </script>");
                //For Policy currency format implementation Added by pradeep Kushwaha on 23-Sep-2010
                if (Session["userId"] != null && Session["userId"].ToString() != "")
                {
                    DataTable dt = Cms.CmsWeb.ClsFetcher.Currency;
                    Int32 Currency_id = 0;
                    Int32 BaseCurrency_id = 0;

                    if (GetPolicyCurrency() != String.Empty)
                    {
                        Currency_id = int.Parse(GetPolicyCurrency());
                        if (Currency_id == 2)
                        {
                            aRegExpCurrencyformat = ConfigurationManager.AppSettings["RegExpCurrencyFormatBrazil"];
                            numberFormatInfo = new CultureInfo(enumCulture.BR, true).NumberFormat;//Added by Pradeep Kushwaha on 07-Oct-2010 to format the number according to the Policy Currency
                            numberFormatInfo.NumberDecimalDigits = 4;
                            aRegExpDouble = ConfigurationManager.AppSettings["RegExpDoubleBrazil"];
                            aRegExpDoublePositiveWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZeroBrazil"];
                            aRegExpPositiveCurrency = ConfigurationManager.AppSettings["RegExpPositiveCurrencyBrazil"];

                        }
                    }
                    else
                        Currency_id = int.Parse(GetSYSBaseCurrency());

                    //Set the base currency format based on the sys param 
                    if (GetSYSBaseCurrency() == enumCurrencyId.BR)
                    {
                        BaseCurrency_id = int.Parse(GetSYSBaseCurrency());
                        System.Globalization.CultureInfo oldculture = Thread.CurrentThread.CurrentCulture;
                        NfiBaseCurrency = new CultureInfo(enumCulture.BR, true).NumberFormat;
                        NfiBaseCurrency.NumberDecimalDigits = 4;
                        Thread.CurrentThread.CurrentCulture = oldculture;

                        aRegExpBaseCurrencyformat = ConfigurationManager.AppSettings["RegExpCurrencyFormatBrazil"];
                        aRegExpBaseDoublePositiveWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZeroBrazil"];
                        aRegExpBaseDouble = ConfigurationManager.AppSettings["RegExpDoubleBrazil"];
                        aRegExpBaseDoublePositiveNonZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZeroBrazil"];
                        aRegExpBaseDoublePositiveStartWithDecimal = ConfigurationManager.AppSettings["RegExpDoublePositiveStartWithDecimalBrazil"];

                    }
                    else
                    {
                        BaseCurrency_id = int.Parse(GetSYSBaseCurrency());
                        aRegExpBaseCurrencyformat = ConfigurationManager.AppSettings["RegExpCurrencyFormat"];
                        aRegExpBaseDoublePositiveWithZero = ConfigurationManager.AppSettings["RegExpDoublePositiveWithZero"];
                        aRegExpBaseDoublePositiveNonZero = ConfigurationManager.AppSettings["RegExpDoublePositiveNonZero"];
                        aRegExpBaseDouble = ConfigurationManager.AppSettings["RegExpDouble"];
                        aRegExpBaseDoublePositiveStartWithDecimal = ConfigurationManager.AppSettings["RegExpDoublePositiveStartWithDecimal"];
                        System.Globalization.CultureInfo oldculture = Thread.CurrentThread.CurrentCulture;
                        NfiBaseCurrency = new CultureInfo(enumCulture.US, true).NumberFormat;
                        NfiBaseCurrency.NumberDecimalDigits = 4;
                        Thread.CurrentThread.CurrentCulture = oldculture;

                    }


                    DataRow[] dr = dt.Select("CURRENCY_ID='" + Currency_id + "'");
                    if (dr.Length > 0)
                    {
                        Response.Write("<script language ='javascript' type='text/javascript'> var sCurrencyFormat = '" + dr[0]["CURR_SYMBOL"].ToString() + "' </script>");
                        Response.Write("<script language ='javascript' type='text/javascript'> var sDecimalSep = '" + dr[0]["CURR_DECIMALSEPR"].ToString() + "' </script>");
                        Response.Write("<script language ='javascript' type='text/javascript'> var sGroupSep = '" + dr[0]["CURR_THOUSANDSEPR"].ToString() + "' </script>");

                    }
                    DataRow[] drbaseCurrency = dt.Select("CURRENCY_ID='" + BaseCurrency_id + "'");

                    Response.Write("<script language ='javascript' type='text/javascript'> var sBaseCurrencyFormat = '" + drbaseCurrency[0]["CURR_SYMBOL"].ToString() + "' </script>");
                    Response.Write("<script language ='javascript' type='text/javascript'> var sBaseDecimalSep = '" + drbaseCurrency[0]["CURR_DECIMALSEPR"].ToString() + "' </script>");
                    Response.Write("<script language ='javascript' type='text/javascript'> var sBaseGroupSep = '" + drbaseCurrency[0]["CURR_THOUSANDSEPR"].ToString() + "' </script>");

                }

            }
            if (aAppDtFormat != "" && aAppDtFormat != null)
            {
                Response.Write("<script language ='javascript' type='text/javascript'> var sCultureDateFormat = '" + aAppDtFormat.ToUpper() + "' </script>");
            }
            //Added till here            

            Page.GetPostBackEventReference(this);
            // Added by pravesh on 15 july 09 to disabled all buttons on page once save button submitted
            //			System.Web.UI.Control c1 = Page.FindControl("btnSave");
            //			if (c1!=null)
            //				((Cms.CmsWeb.Controls.CmsButton)c1).Attributes.Add("onclick","javascript:DisableButton(this);return false;");
            foreach (System.Web.UI.Control c in Page.Controls)
            {
                if (c.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlForm")
                {
                    //SetCmsbuttonCaption(c); //Added by Charles on 9-Mar-2010 for Multilingual Implementation
                    SetCommonCmsButtonCaptions(c);// Added by Charles on 16-Apr-2010 for Multilingul Implementation
                    ((System.Web.UI.HtmlControls.HtmlForm)c).Attributes.Add("onKeyPress", "javascript:return SubmitForm('" + c.ID + "',event);");

                    break;
                }
            }
        }

        /// <summary>
        /// Set Common Cms Button Captions for Save, Delete & Reset
        /// </summary>
        /// <param name="c">HTML Form Control</param>
        /// Added by Charles on 16-Apr-2010 for Multilingul Implementation
        private void SetCommonCmsButtonCaptions(System.Web.UI.Control c)
        {
            try
            {
                CmsWeb.Controls.CmsButton cmsb;

                if (ScreenId != "120_8")
                {
                    cmsb = (CmsWeb.Controls.CmsButton)c.FindControl("btnSave");
                    if (cmsb != null)
                    {
                        cmsb.Text = ClsMessages.FetchGeneralButtonsText("btnSave");
                    }
                }

                cmsb = (CmsWeb.Controls.CmsButton)c.FindControl("btnDelete");
                if (cmsb != null)
                {
                    cmsb.Text = ClsMessages.FetchGeneralButtonsText("btnDelete");
                }
                cmsb = (CmsWeb.Controls.CmsButton)c.FindControl("btnReset");
                if (cmsb != null)
                {
                    cmsb.Text = ClsMessages.FetchGeneralButtonsText("btnReset");
                }
                cmsb = (CmsWeb.Controls.CmsButton)c.FindControl("btnConvertAppToPolicy");
                if (cmsb != null)
                {
                    cmsb.Text = ClsMessages.FetchGeneralButtonsText("btnConvertAppToPolicy");
                }
                cmsb = (CmsWeb.Controls.CmsButton)c.FindControl("btnAppQuote");
                if (cmsb != null)
                {
                    cmsb.Text = ClsMessages.FetchGeneralButtonsText("btnAppQuote");
                }
                cmsb = (CmsWeb.Controls.CmsButton)c.FindControl("btnCustomerAssistant");
                if (cmsb != null)
                {
                    cmsb.Text = ClsMessages.FetchGeneralButtonsText("btnCustomerAssistant");
                }
                cmsb = (CmsWeb.Controls.CmsButton)c.FindControl("btnBack");
                if (cmsb != null)
                {
                    cmsb.Text = ClsMessages.FetchGeneralButtonsText("btnBack");
                }
                cmsb = (CmsWeb.Controls.CmsButton)c.FindControl("btnEdit");
                if (cmsb != null)
                {
                    cmsb.Text = ClsMessages.FetchGeneralButtonsText("btnEdit");
                }
                cmsb = (CmsWeb.Controls.CmsButton)c.FindControl("btnAppQuote");
                if (cmsb != null)
                {
                    cmsb.Text = ClsMessages.FetchGeneralButtonsText("btnAppQuote");
                }
                cmsb = (CmsWeb.Controls.CmsButton)c.FindControl("btnSubmitAnyway");
                if (cmsb != null)
                {
                    cmsb.Text = ClsMessages.FetchGeneralButtonsText("btnSubmitAnyway");
                }

                cmsb = (CmsWeb.Controls.CmsButton)c.FindControl("btnPrint");
                if (cmsb != null)
                {
                    cmsb.Text = ClsMessages.FetchGeneralButtonsText("btnPrint");
                }
                cmsb = (CmsWeb.Controls.CmsButton)c.FindControl("btnClose");
                if (cmsb != null)
                {
                    cmsb.Text = ClsMessages.FetchGeneralButtonsText("btnClose");
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Multilingual Implementation for CmsButton
        /// </summary>
        /// <param name="c">Control</param>
        /// Added by Charles on 9-Mar-2010 for Multilingual Implementation
        private void SetCmsbuttonCaption(System.Web.UI.Control c)
        {
            if (c.GetType().ToString() == "Cms.CmsWeb.Controls.CmsButton")
            {
                if (((Cms.CmsWeb.Controls.CmsButton)c).ID.ToUpper() != "BTNACTIVATEDEACTIVATE")
                {
                    string btnText = CmsWeb.ClsMessages.GetButtonsText(ScreenId, ((Cms.CmsWeb.Controls.CmsButton)c).ID);
                    if (btnText != null && btnText != "")
                    {
                        ((Cms.CmsWeb.Controls.CmsButton)c).Text = btnText;
                    }
                }
            }
            foreach (System.Web.UI.Control chld in c.Controls)
            {
                SetCmsbuttonCaption(chld);
            }
        }

        // Added by pravesh on 16 july 09 to disabled all buttons on page once page submitted
        private void AddCmsbuttonAttributes(System.Web.UI.Control c)
        {
            if (c.GetType().ToString() == "Cms.CmsWeb.Controls.CmsButton")
            {
                ((Cms.CmsWeb.Controls.CmsButton)c).Attributes.Add("onclick", "javascript:DisableButtonOnClick(document.getElementById('" + c.ID + "'));return false;");
            }
            foreach (System.Web.UI.Control chld in c.Controls)
            {
                AddCmsbuttonAttributes(chld);
            }
        }

        //		protected override void OnError(EventArgs e)
        //		{
        //			string strUrl	=	Request.Url.ToString();
        //			System.Collections.Specialized.NameValueCollection additionalInfo = new System.Collections.Specialized.NameValueCollection();
        //			additionalInfo.Add("page visited",strUrl);
        //			Exception ex	=	Server.GetLastError();
        //			string exMessage = "";
        //			if(ex != null)
        //			{
        //				if(ex.InnerException != null)
        //				{
        //					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex.InnerException,additionalInfo);
        //					exMessage	=	ex.InnerException.Message;
        //				}
        //				else
        //				{
        //					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,additionalInfo);
        //					exMessage	=	ex.Message;
        //				}
        //			}
        //			System.Web.HttpContext.Current.Items.Clear();
        //			System.Web.HttpContext.Current.Items.Add("refPath",strUrl);
        //			System.Web.HttpContext.Current.Items.Add("ExMesssage",exMessage);
        //			Server.Transfer("/cms/cmsweb/aspx/error.aspx",true);
        //			//Response.Redirect("/cms/cmsweb/aspx/error.aspx?refPath=" + strUrl,true);
        //		}
        // Handle the PreRender event
        private void cmsbase_PreRender(object sender, EventArgs e)
        {
            AddSetFocusScript();
            //CheckResize();
        }

        /// <summary>
        /// Added the pull customer adddres functionality with specified button and address controls
        /// </summary>
        /// <param name="txtAddress1"></param>
        /// <param name="txtAddress2"></param>
        /// <param name="txtCity"></param>
        /// <param name="cmbCountry"></param>
        /// <param name="cmbState"></param>
        /// <param name="txtZip"></param>
        /// <param name="PullButton"></param>
        public void RequiredPullCustAdd(
            System.Web.UI.WebControls.TextBox txtAddress1,
            System.Web.UI.WebControls.TextBox txtAddress2,
            System.Web.UI.WebControls.TextBox txtCity,
            System.Web.UI.WebControls.DropDownList cmbCountry,
            System.Web.UI.WebControls.DropDownList cmbState,
            System.Web.UI.WebControls.TextBox txtZip,
            Cms.CmsWeb.Controls.CmsButton PullButton)
        {
            //Saving the XML of client address in hidden field
            //To be retreived from database
            if (GetCustomerID().Trim() != "")
            {
                /*Whole functionality is required only when there is a customer in session*/
                ClientScript.RegisterHiddenField("hidCustAddXml", Cms.BusinessLayer.BlClient.ClsCustomer.PopulateClientAddress(int.Parse(GetCustomerID())));

                /*Whole functionality is required only when there is a customer in session*/
                //Page.RegisterHiddenField("hidCountyXML",Cms.BusinessLayer.BlClient.ClsCustomer.PopulateClientCounty(int.Parse(GetCustomerID())));

                string strCountryId;
                if (cmbCountry == null)
                    strCountryId = "null";
                else
                    strCountryId = cmbCountry.ID;

                //Attaching the javascript function on click event
                PullButton.Attributes.Add("onClick", "javascript:return PullCustomerAddress("
                    + "document.getElementById('" + txtAddress1.ID + "'),"
                    + "document.getElementById('" + txtAddress2.ID + "'),"
                    + "document.getElementById('" + txtCity.ID + "'),"
                    + "document.getElementById('" + strCountryId + "'),"
                    + "document.getElementById('" + cmbState.ID + "'),"
                    + "document.getElementById('" + txtZip.ID + "')"
                    + ",null);");
            }
            else
            {
                /*Customer is not in session, hence registring empty field*/
                ClientScript.RegisterHiddenField("hidCustAddXml", "");
            }
        }

        public void RequiredPullCustAdd(
           System.Web.UI.WebControls.TextBox txtAddress1,
           System.Web.UI.WebControls.TextBox txtAddress2,
           System.Web.UI.WebControls.TextBox txtCity,
           System.Web.UI.WebControls.DropDownList cmbCountry,
           System.Web.UI.WebControls.DropDownList cmbState,
           System.Web.UI.WebControls.TextBox txtZip,
           Cms.CmsWeb.Controls.CmsButton PullButton,
           System.Web.UI.WebControls.TextBox txtNUMBER,
           System.Web.UI.WebControls.TextBox txtDISTRICT
           )
        {
            //Saving the XML of client address in hidden field
            //To be retreived from database
            if (GetCustomerID().Trim() != "")
            {
                /*Whole functionality is required only when there is a customer in session*/
                ClientScript.RegisterHiddenField("hidCustAddXml", Cms.BusinessLayer.BlClient.ClsCustomer.PopulateClientAddress(int.Parse(GetCustomerID())));

                /*Whole functionality is required only when there is a customer in session*/
                //Page.RegisterHiddenField("hidCountyXML",Cms.BusinessLayer.BlClient.ClsCustomer.PopulateClientCounty(int.Parse(GetCustomerID())));

                string strCountryId;
                if (cmbCountry == null)
                    strCountryId = "null";
                else
                    strCountryId = cmbCountry.ID;

                //Attaching the javascript function on click event
                PullButton.Attributes.Add("onClick", "javascript:return PullCustomerAddress("
                    + "document.getElementById('" + txtAddress1.ID + "'),"
                    + "document.getElementById('" + txtAddress2.ID + "'),"
                    + "document.getElementById('" + txtCity.ID + "'),"
                    + "document.getElementById('" + strCountryId + "'),"
                    + "document.getElementById('" + cmbState.ID + "'),"
                    + "document.getElementById('" + txtZip.ID + "'),"
                    + "null,"
                    + "document.getElementById('" + txtNUMBER.ID + "')"
                    + ",document.getElementById('" + txtDISTRICT.ID + "'));");
            }
            else
            {
                /*Customer is not in session, hence registring empty field*/
                ClientScript.RegisterHiddenField("hidCustAddXml", "");
            }
        }

        public void RequiredPullCustAdd(
            System.Web.UI.WebControls.TextBox txtAddress1,
            System.Web.UI.WebControls.TextBox txtAddress2,
            System.Web.UI.WebControls.TextBox txtCity,
            System.Web.UI.WebControls.DropDownList cmbCountry,
            System.Web.UI.WebControls.DropDownList cmbState,
            System.Web.UI.WebControls.TextBox txtZip,
            System.Web.UI.WebControls.TextBox txtPhone,
            System.Web.UI.WebControls.TextBox txtEmail,
            System.Web.UI.WebControls.TextBox txtMobile,
            System.Web.UI.WebControls.TextBox txtBUSINESS_PHONE,
            System.Web.UI.WebControls.TextBox txtEXT,
            Cms.CmsWeb.Controls.CmsButton PullButton)
        {
            //Saving the XML of client address in hidden field
            //To be retreived from database
            if (GetCustomerID().Trim() != "")
            {
                /*Whole functionality is required only when there is a customer in session*/
                ClientScript.RegisterHiddenField("hidCustAddXml", Cms.BusinessLayer.BlClient.ClsCustomer.PopulateClientAddress(int.Parse(GetCustomerID())));

                /*Whole functionality is required only when there is a customer in session*/
                //Page.RegisterHiddenField("hidCountyXML",Cms.BusinessLayer.BlClient.ClsCustomer.PopulateClientCounty(int.Parse(GetCustomerID())));

                string strCountryId;
                if (cmbCountry == null)
                    strCountryId = "null";
                else
                    strCountryId = cmbCountry.ID;

                string strPullScript = "javascript:return PullCustomerAddressPhoneMobile("
                    + "document.getElementById('" + txtAddress1.ID + "'),"
                    + "document.getElementById('" + txtAddress2.ID + "'),"
                    + "document.getElementById('" + txtCity.ID + "'),"
                    + "document.getElementById('" + strCountryId + "'),"
                    + "document.getElementById('" + cmbState.ID + "'),"
                    + "document.getElementById('" + txtZip.ID + "'),"
                    + "document.getElementById('" + txtPhone.ID + "'),"
                    + "document.getElementById('" + txtEmail.ID + "'),"
                    + "document.getElementById('" + txtMobile.ID + "'),";
                if (txtBUSINESS_PHONE != null)
                    strPullScript += "document.getElementById('" + txtBUSINESS_PHONE.ID + "'),";
                else
                    strPullScript += "null,";

                if (txtEXT != null)
                    strPullScript += "document.getElementById('" + txtEXT.ID + "')";
                else
                    strPullScript += "null";

                strPullScript += ");";
                //Attaching the javascript function on click event
                PullButton.Attributes.Add("onClick", strPullScript);
            }
            else
            {
                /*Customer is not in session, hence registring empty field*/
                ClientScript.RegisterHiddenField("hidCustAddXml", "");
            }
        }

        public void RequiredPullCustAdd(
            System.Web.UI.WebControls.TextBox txtAddress1,
            System.Web.UI.WebControls.TextBox txtAddress2,
            System.Web.UI.WebControls.TextBox txtCity,
            System.Web.UI.WebControls.DropDownList cmbCountry,
            System.Web.UI.WebControls.DropDownList cmbState,
            System.Web.UI.WebControls.TextBox txtZip,
            System.Web.UI.WebControls.TextBox txtPhone,
            System.Web.UI.WebControls.TextBox txtEmail,
            Cms.CmsWeb.Controls.CmsButton PullButton)
        {
            //Saving the XML of client address in hidden field
            //To be retreived from database
            if (GetCustomerID().Trim() != "")
            {
                /*Whole functionality is required only when there is a customer in session*/
                ClientScript.RegisterHiddenField("hidCustAddXml", Cms.BusinessLayer.BlClient.ClsCustomer.PopulateClientAddress(int.Parse(GetCustomerID())));

                /*Whole functionality is required only when there is a customer in session*/
                //Page.RegisterHiddenField("hidCountyXML",Cms.BusinessLayer.BlClient.ClsCustomer.PopulateClientCounty(int.Parse(GetCustomerID())));

                string strCountryId;
                if (cmbCountry == null)
                    strCountryId = "null";
                else
                    strCountryId = cmbCountry.ID;

                //Attaching the javascript function on click event
                PullButton.Attributes.Add("onClick", "javascript:return PullCustomerAddressPhone("
                    + "document.getElementById('" + txtAddress1.ID + "'),"
                    + "document.getElementById('" + txtAddress2.ID + "'),"
                    + "document.getElementById('" + txtCity.ID + "'),"
                    + "document.getElementById('" + strCountryId + "'),"
                    + "document.getElementById('" + cmbState.ID + "'),"
                    + "document.getElementById('" + txtZip.ID + "'),"
                    + "document.getElementById('" + txtPhone.ID + "'),"
                    + "document.getElementById('" + txtEmail.ID + "')"
                    + ",null);");
            }
            else
            {
                /*Customer is not in session, hence registring empty field*/
                ClientScript.RegisterHiddenField("hidCustAddXml", "");
            }
        }

        //end
        /// <summary>
        /// Added the pull customer adddres functionality with specified button and address controls
        /// </summary>
        /// <param name="txtAddress1"></param>
        /// <param name="txtAddress2"></param>
        /// <param name="txtCity"></param>
        /// <param name="cmbCountry"></param>
        /// <param name="cmbState"></param>
        /// <param name="txtZip"></param>
        /// <param name="PullButton"></param>
        public void RequiredPullCustAddWithCounty(
            System.Web.UI.WebControls.TextBox txtAddress1,
            System.Web.UI.WebControls.TextBox txtAddress2,
            System.Web.UI.WebControls.TextBox txtCity,
            System.Web.UI.WebControls.DropDownList cmbCountry,
            System.Web.UI.WebControls.DropDownList cmbState,
            System.Web.UI.WebControls.TextBox txtZip,
            System.Web.UI.WebControls.TextBox txtCounty,
            System.Web.UI.WebControls.TextBox txtTerritory,
            Cms.CmsWeb.Controls.CmsButton PullButton)
        {
            //Saving the XML of client address in hidden field
            //To be retreived from database
            if (GetCustomerID().Trim() != "")
            {
                RequiredPullCustAdd(txtAddress1, txtAddress2, txtCity, cmbCountry, cmbState, txtZip, PullButton);
                /*Whole functionality is requitxtCity,red only when there is a customer in session*/
                ClientScript.RegisterHiddenField("hidCountyXML", Cms.BusinessLayer.BlClient.ClsCustomer.PopulateClientCounty(int.Parse(GetCustomerID())));

                string strCounty, strTerritory;

                if (txtTerritory == null)
                    strTerritory = "null";
                else
                    strTerritory = txtTerritory.ID;

                if (txtCounty == null)
                    strCounty = "null";
                else
                    strCounty = txtCounty.ID;

                string strCountryId;
                if (cmbCountry == null)
                    strCountryId = "null";
                else
                    strCountryId = cmbCountry.ID;

                //Attaching the javascript function on click event
                PullButton.Attributes.Add("onClick", "javascript:return PullCustomerAddress("
                    + "document.getElementById('" + txtAddress1.ID + "'),"
                    + "document.getElementById('" + txtAddress2.ID + "'),"
                    + "document.getElementById('" + txtCity.ID + "'),"
                    + "document.getElementById('" + strCountryId + "'),"
                    + "document.getElementById('" + cmbState.ID + "'),"
                    + "document.getElementById('" + txtZip.ID + "'),"
                    + "document.getElementById('" + strCounty + "'),"
                    + "document.getElementById('" + strTerritory + "')"
                    + ");");
            }
            else
            {
                /*Customer is not in session, hence registring empty field*/
                ClientScript.RegisterHiddenField("hidCountyXML", "");
            }
        }

        //Added By Ravindra(04-04-2006)
        //To enable call of another Java Script function on click event
        #region RequiredPullCustAddWithCountyEx Function
        public void RequiredPullCustAddWithCountyEx(
            System.Web.UI.WebControls.TextBox txtAddress1,
            System.Web.UI.WebControls.TextBox txtAddress2,
            System.Web.UI.WebControls.TextBox txtCity,
            System.Web.UI.WebControls.DropDownList cmbCountry,
            System.Web.UI.WebControls.DropDownList cmbState,
            System.Web.UI.WebControls.TextBox txtZip,
            System.Web.UI.WebControls.TextBox txtCounty,
            System.Web.UI.WebControls.TextBox txtTerritory,
            Cms.CmsWeb.Controls.CmsButton PullButton, string strCustomFunction)
        {
            //Saving the XML of client address in hidden field
            //To be retreived from database
            if (GetCustomerID().Trim() != "")
            {
                RequiredPullCustAdd(txtAddress1, txtAddress2, txtCity, cmbCountry, cmbState, txtZip, PullButton);
                /*Whole functionality is requitxtCity,red only when there is a customer in session*/
                ClientScript.RegisterHiddenField("hidCountyXML", Cms.BusinessLayer.BlClient.ClsCustomer.PopulateClientCounty(int.Parse(GetCustomerID())));

                string strCounty, strTerritory;

                if (txtTerritory == null)
                    strTerritory = "null";
                else
                    strTerritory = txtTerritory.ID;

                if (txtCounty == null)
                    strCounty = "null";
                else
                    strCounty = txtCounty.ID;

                string strCountryId;
                if (cmbCountry == null)
                    strCountryId = "null";
                else
                    strCountryId = cmbCountry.ID;

                //Attaching the javascript function on click event
                PullButton.Attributes.Add("onClick", "javascript: PullCustomerAddress("
                    + "document.getElementById('" + txtAddress1.ID + "'),"
                    + "document.getElementById('" + txtAddress2.ID + "'),"
                    + "document.getElementById('" + txtCity.ID + "'),"
                    + "document.getElementById('" + strCountryId + "'),"
                    + "document.getElementById('" + cmbState.ID + "'),"
                    + "document.getElementById('" + txtZip.ID + "'),"
                    + "document.getElementById('" + strCounty + "'),"
                    + "document.getElementById('" + strTerritory + "')"
                    + ");return  " + strCustomFunction + "();");
            }
            else
            {
                /*Customer is not in session, hence registring empty field*/
                ClientScript.RegisterHiddenField("hidCountyXML", "");
            }
        }

        #endregion

        protected void VerifyAddress(System.Web.UI.WebControls.WebControl ctrl, System.Web.UI.WebControls.TextBox txtAdd1,
            System.Web.UI.WebControls.TextBox txtAdd2, System.Web.UI.WebControls.TextBox txtCity,
            DropDownList cmbState, System.Web.UI.WebControls.TextBox txtZip)
        {
            string strJavascript = @"VerifyAddress(document.getElementById('"
                + txtAdd1.ID + "'),document.getElementById('"
                + txtAdd2.ID + "'),document.getElementById('" + txtCity.ID
                + "'),document.getElementById('" + cmbState.ID
                + "'),document.getElementById('" + txtZip.ID + "'))";

            ctrl.Attributes.Add("onClick", strJavascript);
        }
        protected void VerifyAddressDetailsBR(System.Web.UI.WebControls.WebControl ctrl, System.Web.UI.WebControls.TextBox txtAdd1,
           TextBox txtDistrict, System.Web.UI.WebControls.TextBox txtCity,
           DropDownList cmbState, System.Web.UI.WebControls.TextBox txtZip)
        {
            string strJavascript = @"VerifyAddressDetailsForBR(document.getElementById('"
                + txtAdd1.ID + "'),document.getElementById('"
                + txtDistrict.ID + "') , document.getElementById('" + txtCity.ID
                + "'),document.getElementById('" + cmbState.ID
                + "'),document.getElementById('" + txtZip.ID + "'))";

            ctrl.Attributes.Add("onClick", strJavascript);
        }
        protected void VerifyAddressState(System.Web.UI.WebControls.WebControl ctrl, System.Web.UI.WebControls.TextBox txtAdd1,
            System.Web.UI.WebControls.TextBox txtAdd2, System.Web.UI.WebControls.TextBox txtCity,
            System.Web.UI.WebControls.TextBox txtState, System.Web.UI.WebControls.TextBox txtZip)
        {
            string strJavascript = @"VerifyAddress(document.getElementById('"
                + txtAdd1.ID + "'),document.getElementById('"
                + txtAdd2.ID + "'),document.getElementById('" + txtCity.ID
                + "'),document.getElementById('" + txtState.ID
                + "'),document.getElementById('" + txtZip.ID + "'))";

            ctrl.Attributes.Add("onClick", strJavascript);
        }

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ViewStateUserKey = Session.SessionID;
        }

        #region Enumerations
        /// <summary>
        /// State
        /// </summary>
        public enum enumState
        {
            Michigan = 22,
            Indiana = 14,
            Wisconsin = 49,
            Arkansas = 4
        }

        /// <summary>
        /// Line of Business
        /// </summary>
        public enum enumLOB
        {
            HOME = 1,
            AUTOP = 2,
            CYCL = 3,
            BOAT = 4,
            UMB = 5,
            REDW = 6,
            GENL = 7,
            AVIAT = 8,
            ARPERIL = 9,
            COMPCONDO = 10,
            COMPCOMPY = 11,
            GENCVLLIB = 12,
            MTIME = 13,
            DRISK = 14,
            INDPA = 15,
            ROBBERY = 16,
            FACLIAB = 17,
            CVLIABTR = 18,
            DWELLING = 19,
            NATNTR = 20,
            CPCACC = 21,
            PAPEACC = 22,
            INTERN = 23,
            TFIRE = 25,
            ERISK = 26,
            GLBANK = 27,
            AERO = 28,
            MTOR = 29,
            DPVA = 30,
            CTCL = 31,
            JDLGR = 32,
            MRTG = 33,
            GRPLF = 34,
            RLLE = 35,
            DPVAT2 = 36,
            RETSURTY = 37,
            //Modified by Abhishek Goel
            MOT = 38//Added by kuldeep 


        }

        /// <summary>
        /// Customer Type
        /// </summary>
        public enum enumCUSTOMER_TYPE
        {
            COMMERCIAL = 11109,
            PERSONAL = 11110
        }
        public enum enumYESNO_LOOKUP_UNIQUE_ID
        {
            YES = 10963,
            NO = 10964
        }
        public enum enumYESNO_LOOKUP_CODE
        {
            YES = 1,
            NO = 0
        }
        public enum enumClaimDefaultValues
        {
            CATASTTROPHE_EVENT_TYPES = 1,
            PARTY_TYPES = 2,
            CLAIMANT_TYPES = 3,
            EXPERT_SERVICE_PROVIDER_TYPES = 4,
            LOSS_TYPES_SUB_TYPES = 5,
            RECOVERY_TYPE = 6,
            CLAIMS_STATUS = 7,
            CLAIM_TRANSACTION_CODE = 8,
            SERVICE_TYPES = 9,
            RELATIONSHIP_TO_THE_INSURED = 10
        }

        public enum enumPaymentMethod
        {
            CHECK = 11787,
            EFT = 11788,
            MANUAL_CHECK = 11789
        }

        public enum enumClaimAdjusterTypes
        {
            INSIDE_ADJUSTER = 11736,
            OUTSIDE_ADJUSTER = 11737,
            THIRD_PARTY_ADJUSTER = 11738

        }
        public enum enumAgencyType
        {
            BROKER_AGENCY = 14701,
            SALES_AGENT = 14702
        }
        public enum enumCommissionType
        {
            COMMISSION = 43,
            ENROLLMENT_FEE = 44,
            PRO_LABORE = 45,
        }
        #endregion
        /// <summary>
        /// DB Action
        /// </summary>
        public struct enumAction
        {
            public const String Insert = "I";
            public const String Update = "U";
            public const String Delete = "D";
        }

        /// <summary>
        /// Culture specific currency symbol
        /// </summary>
        /// Added by Charles on 24-May-2010 for Multilingual Implementation
        public struct enumCurrencySymbol
        {
            public const String US = "$";
            public const String BRAZIL = "R$";
        }

        /// <summary>
        /// Culture specific Decimal Seperator for Number & Currency
        /// </summary>
        /// Added by Charles on 4-Jun-2010 for Multilingual Implementation
        public struct enumDecimalSeperator
        {
            public const String US = ".";
            public const String BRAZIL = ",";
        }

        /// <summary>
        /// Culture specific Group Seperator for Number & Currency
        /// </summary>
        /// Added by Charles on 4-Jun-2010 for Multilingual Implementation
        public struct enumGroupSeperator
        {
            public const String US = ",";
            public const String BRAZIL = ".";
        }

        /// <summary>
        /// Date Format
        /// </summary>
        ///  Added by Charles on 28-May-2010 for Multilingual Implementation
        public struct enumDateFormat
        {
            public const String DDMMYYYY = "dd/MM/yyyy";
            public const String MMDDYYYY = "MM/dd/yyyy";
        }
        //added by Pradeep kushwaha on 1-Oct-2010
        public struct enumCurrencyId
        {
            public const String US = "1";
            public const String BR = "2";
        }
        //added by Pradeep kushwaha on 07-Oct-2010
        public struct enumCulture
        {
            public const String US = "en-US";
            public const String BR = "pt-BR";
        }
        #region Encryption/Decryption
        public static string EncryptMessage(string plainMessage)
        {
            plainMessage = System.Web.HttpUtility.UrlEncode(plainMessage);

            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.IV = new byte[8];
            byte[] encryptedBytes = null;

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(gStrkey, new byte[0]);

            try
            {
                // Hash Algorithms can be MD5 , SHA , SHA1
                //des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, new byte[8]);
                des.Key = pdb.CryptDeriveKey("RC2", "SHA1", 128, new byte[8]);

                MemoryStream ms = new MemoryStream(plainMessage.Length * 2);
                CryptoStream encStream = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

                byte[] plainBytes = Encoding.UTF8.GetBytes(plainMessage);
                encStream.Write(plainBytes, 0, plainBytes.Length);
                encStream.FlushFinalBlock();
                encryptedBytes = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(encryptedBytes, 0, (int)ms.Length);
                encStream.Close();
            }
            finally
            {
                des.Clear();
                des = null;
                pdb = null;
            }

            return Convert.ToBase64String(encryptedBytes);

        }

        public static string DecryptMessage(string encryptedBase64)
        {
            string plainMessage = null;

            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.IV = new byte[8];
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(gStrkey, new byte[0]);
            // Hash Algorithms can be MD5 , SHA , SHA1
            des.Key = pdb.CryptDeriveKey("RC2", "SHA1", 128, new byte[8]);
            try
            {

                byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
                MemoryStream ms = new MemoryStream(encryptedBase64.Length);
                CryptoStream decStream = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                decStream.FlushFinalBlock();
                byte[] plainBytes = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(plainBytes, 0, (int)ms.Length);
                decStream.Close();
                plainMessage = Encoding.UTF8.GetString(plainBytes);

            }
            finally
            {
                des.Clear();
                des = null;
            }
            return System.Web.HttpUtility.UrlDecode(plainMessage);

        }
        #endregion
        #region Binary Serializers
        /// <summary>
        /// To serialize an object By Pravesh K chandel
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static System.IO.MemoryStream SerializeBinary(object request)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter serializer =
            new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            serializer.Serialize(memStream, request);
            return memStream;
        }
        /// <summary>
        /// To Deserialize an object By Pravesh K chandel
        /// </summary>
        /// <param name="memStream"></param>
        /// <returns></returns>
        public static object DeSerializeBinary(System.IO.MemoryStream memStream)
        {
            memStream.Position = 0;
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter deserializer =
            new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            object newobj = deserializer.Deserialize(memStream);
            memStream.Close();
            return newobj;
        }
        #endregion

        #region XML Serializers
        /*
        public static System.IO.MemoryStream SerializeSOAP(object request)
        {
            System.Runtime.Serialization.Formatters.Soap.SoapFormatter serializer =
            new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            serializer.Serialize(memStream, request);
            return memStream;
        }

        public static object DeSerializeSOAP(System.IO.MemoryStream memStream)
        {
            object sr;
            System.Runtime.Serialization.Formatters.Soap.SoapFormatter deserializer =
            new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();
            memStream.Position = 0;
            sr = deserializer.Deserialize(memStream);
            memStream.Close();
            return sr;
        }*/
        #endregion
        #region struct For Products based Screen_ID
        //Added By Pradeep Kushwaha 29-04-2010 for Diversified Risk Product 

        /// <summary>
        /// Declared the Diversified Risk Product related Screen IDs
        /// </summary>

        public struct DIVERSIFIED_RISKSscreenId
        {
            public const String RISK_INDEX_PAGE = "470";
            public const String LOCATION_INFORMATION = "470_0";
            public const String PROTECTION_DEVICES = "470_1";
            public const String DISCOUNTS_SURCHARGES = "470_2";
            public const String COVERAGES = "470_3";
        }
        /// <summary>
        /// Declared the Comprehensive Company Product related Screen IDs
        /// </summary>
        public struct COMPREHENSIVE_COMPANYscreenId
        {
            public const String INDEX_PAGE = "462";
            public const String LOCATION_INFORMATION = "462_0";
            public const String PROTECTION_DEVICES = "462_1";
            public const String DISCOUNTS_SURCHARGES = "462_2";
            public const String COVERAGES = "462_3";
        }
        /// <summary>
        /// Comprehensive Condominium Product related Screen IDs
        /// </summary>
        public struct COMPREHENSIVE_CONDOMINIUMscreenId
        {
            public const String INDEX_PAGE = "458";
            public const String LOCATION_INFORMATION = "458_0";
            public const String PROTECTION_DEVICES = "458_1";
            public const String DISCOUNTS_SURCHARGES = "458_2";
            public const String COVERAGES = "458_3";
        }
        /// <summary>
        /// Comprehensive All Risk & Named Perils  related Screen IDs
        /// </summary>
        public struct ALL_RISK_NAMED_PERILSscreenId
        {
            public const String INDEX_PAGE = "453";
            public const String LOCATION_INFORMATION = "453_0";
            public const String PROTECTION_DEVICES = "453_1";
            public const String DISCOUNTS_SURCHARGES = "453_2";
            public const String COVERAGES = "453_3";
        }
        /// <summary>
        /// Comprehensive National Cargo Transportation   related Screen IDs
        /// </summary>
        public struct NATIONAL_CARGO_TRANSPORTATIONscreenId
        {
            public const String INDEX_PAGE = "488";
            public const String INFORMATION_PAGE = "488_0";
            public const String COVERAGES = "488_1";
            public const String DISCOUNTS_SURCHARGES = "488_2";
        }
        /// <summary>
        /// Comprehensive Civil Liability Transportation related Screen IDs
        /// </summary>
        public struct CIVIL_LIABILITY_TRANSPORTATIONscreenId
        {
            public const String INDEX_PAGE = "486";
            public const String INFORMATION_PAGE = "486_0";
            public const String DISCOUNTS_SURCHARGES = "486_1";
            public const String COVERAGES = "486_2";

        }
        /// <summary>
        /// Comprehensive Facultative Liability related Screen IDs
        /// </summary>
        public struct FACULTATIVE_LIABILITYscreenId
        {
            public const String INDEX_PAGE = "482";
            public const String VEH_INFORMATION_PAGE = "482_0";
            public const String DISCOUNTS_SURCHARGES = "482_1";
            public const String COVERAGES = "482_2";

        }
        /// <summary>
        /// INTERNational Cargo Transportation   related Screen IDs
        /// </summary>
        public struct INTERNATIONAL_CARGO_TRANSPORTATIONscreenId
        {
            public const String INDEX_PAGE = "495";
            public const String INFORMATION_PAGE = "495_0";
            public const String COVERAGES = "495_1";
            public const String DISCOUNTS_SURCHARGES = "495_2";
        }
        /// <summary>
        /// Dwelling related Screen IDs
        /// </summary>
        public struct DWELLINGscreenId
        {
            public const String INDEX_PAGE = "491";
            public const String LOCATION_INFORMATION = "491_0";
            public const String PROTECTION_DEVICES = "491_1";
            public const String DISCOUNTS_SURCHARGES = "491_2";
            public const String COVERAGES = "491_3";

        }
        /// <summary>
        /// Robbery related Screen IDs
        /// </summary>
        public struct ROBBERYscreenId
        {
            public const String INDEX_PAGE = "478";
            public const String LOCATION_INFORMATION = "478_0";
            public const String PROTECTION_DEVICES = "478_1";
            public const String DISCOUNTS_SURCHARGES = "478_2";
            public const String COVERAGES = "478_3";

        }
        /// <summary>
        /// Group Personal Accident for Passenger related Screen IDs
        /// </summary>
        public struct GROUP_PERSONAL_ACCIDENTscreenId
        {
            public const String INDEX_PAGE = "497";
            public const String INDIVIDUAL_INFORMATION = "497_0";
            public const String DISCOUNTS_SURCHARGES = "497_1";
            public const String COVERAGES = "497_2";
            public const String BENEFICIARY = "497_3";
            public const String BENEFICIARY_INFO = "497_3_0";

        }
        /// <summary>
        /// 
        /// <summary>
        /// Individual Personal Accident  related Screen IDs
        /// </summary>
        /// itrack 1161

        public struct INDIVIDUAL_PERSONAL_ACCIDENTscreenId
        {
            public const String INDEX_PAGE = "474";
            public const String INDIVIDUAL_INFORMATION = "474_0";
            public const String COVERAGES = "474_1";
            public const String DISCOUNTS_SURCHARGES = "474_2";
            public const String BENEFICIARY = "474_3";
            public const String BENEFICIARY_INFO = "474_4";

        }
        /// <summary>
        /// Passengers Personal Accident related Screen IDs
        /// </summary>
        public struct PASSENGERS_PERSONAL_ACCIDENTscreenId
        {
            public const String INDEX_PAGE = "499";
            public const String INSURED_INFORMATION = "499_0";
            public const String DISCOUNTS_SURCHARGES = "499_1";
            public const String COVERAGES = "499_2";

        }
        /// <summary>
        /// General Civil Liability  related Screen IDs
        /// </summary>
        public struct GENERAL_CIVIL_LIABILITYscreenId
        {
            public const String INDEX_PAGE = "466";
            public const String LOCATION_INFORMATION = "466_0";
            public const String PROTECTION_DEVICES = "466_1";
            public const String COVERAGES = "466_2";
            public const String DISCOUNTS_SURCHARGES = "466_3";

        }

        public struct GROUP_LIFEscreenId
        {
            public const String INDEX_PAGE = "510";
            public const String INDIVIDUAL_INFORMATION = "510_0";
            public const String DISCOUNTS_SURCHARGES = "510_1";
            public const String COVERAGES = "510_2";
            public const String BENEFICIARY = "510_3";
            public const String BENEFICIARY_INFO = "510_3_0";


        }
        public struct MORTGAGEscreenId
        {
            public const String INDEX_PAGE = "518";
            public const String INDIVIDUAL_INFORMATION = "518_0";
            public const String DISCOUNTS_SURCHARGES = "518_1";
            public const String COVERAGES = "518_2";
            //public const String BENEFICIARY = "510_3";

        }
        public struct DPVATscreenId
        {
            public const String INDEX_PAGE = "516";
            public const String INDIVIDUAL_INFORMATION = "516_0";
            public const String DISCOUNTS_SURCHARGES = "516_1";
            public const String COVERAGES = "516_2";

        }
        public struct DPVAT2screenId
        {
            public const String INDEX_PAGE = "528";
            public const String INDIVIDUAL_INFORMATION = "528_0";
            public const String DISCOUNTS_SURCHARGES = "528_1";
            public const String COVERAGES = "528_2";

        }
        public struct ENGENEERING_RISKSscreenId
        {
            public const String INDEX_PAGE = "506";
            public const String LOCATION_INFORMATION = "506_0";
            public const String PROTECTION_DEVICES = "506_3";
            public const String DISCOUNTS_SURCHARGES = "506_1";
            public const String COVERAGES = "506_2";
        }
        public struct JUDICIAL_GUARANTEEscreenId
        {
            public const String RISK_INDEX_PAGE = "522";
            public const String LOCATION_INFORMATION = "522_0";
            public const String PROTECTION_DEVICES = "522_1";
            public const String DISCOUNTS_SURCHARGES = "522_2";
            public const String COVERAGES = "522_3";
        }
        public struct GLOBAL_OF_BANKscreenId
        {
            public const String RISK_INDEX_PAGE = "508";
            public const String LOCATION_INFORMATION = "508_0";
            public const String PROTECTION_DEVICES = "508_1";
            public const String DISCOUNTS_SURCHARGES = "508_2";
            public const String COVERAGES = "508_3";
        }
        public struct TRADITIONAL_FIREscreenId
        {
            public const String INDEX_PAGE = "504";
            public const String LOCATION_INFORMATION = "504_0";
            public const String PROTECTION_DEVICES = "504_1";
            public const String DISCOUNTS_SURCHARGES = "504_2";
            public const String COVERAGES = "504_3";
        }
        public struct MOTORscreenId
        {
            public const String INDEX_PAGE = "514";
            public const String VEH_INFORMATION_PAGE = "514_0";
            public const String DISCOUNTS_SURCHARGES = "514_1";
            public const String COVERAGES = "514_2";

        }
        public struct AERONAUTICscreenId
        {
            public const String INDEX_PAGE = "512";
            public const String VEH_INFORMATION_PAGE = "512_0";
            public const String DISCOUNTS_SURCHARGES = "512_1";
            public const String COVERAGES = "512_2";

        }
        public struct CARGO_TRANSPORTATION_CIVIL_LIABILITYscreenId
        {
            public const String INDEX_PAGE = "520";
            public const String INFORMATION_PAGE = "520_0";
            public const String DISCOUNTS_SURCHARGES = "520_1";
            public const String COVERAGES = "520_2";

        }
        public struct PENHOR_RURALscreenId
        {
            public const String INDEX_PAGE = "526";
            public const String INFORMATION_PAGE = "526_0";
            public const String COVERAGES = "526_1";

        }
        public struct RENTAL_SURETYscreenId
        {
            public const String INDEX_PAGE = "532";
            public const String INFORMATION_PAGE = "532_0";
            public const String DISCOUNTS_SURCHARGES = "532_1";
            public const String COVERAGES = "532_2";

        }
        #endregion
    }
}
