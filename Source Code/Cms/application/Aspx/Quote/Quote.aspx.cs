/******************************************************************************************
<Author					: -   S K Bhatt/Nidhi
<Start Date				: -	 8/26/2005 3:13:20 PM
<End Date				: -	
<Description			: - 
<Review Date			: - 
<Reviewed By			: - 

<Modified Date			: Oct. 05,2005
<Modified By			: Ashwani 
<Purpose				: Modified  methods Page_Load()
*************************************************************************/
 
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlQuote;
using Microsoft.Xml.XQuery;
using Cms.BusinessLayer.BlCommon;
using System.Threading;
using System.Globalization;
namespace Cms.Application.Aspx.Quote
{
    /// <summary>
    /// Summary description for Quote.
    /// </summary>
    public class Quote : Cms.Application.appbase
    {
        System.Web.UI.HtmlControls.HtmlInputHidden hidRATE_HTML;

        private const int SHOW_QUOTE_DETAILS = 1;
        private const int SHOW_WRONG_INPUT = 0;
        private const int SHOW_FAILED_MESSAGE = 3;
        private const int SHOW_POLICY_QUOTE = 4;
        private const int SHOW_OLD_QUOTE_DETAILS = 6;
        private const int SHOW_QUICK_RATE = 9;
        //public bool IS_RULE_VERIFICATION_REQUIRED=true;

        protected System.Web.UI.WebControls.Label lblMessage;

        private const string LOB_HOME = "1";
        private const string LOB_PRIVATE_PASSENGER = "2";
        private const string LOB_MOTORCYCLE = "3";
        private const string LOB_WATERCRAFT = "4";
        private const string LOB_UMBRELLA = "5";
        private const string LOB_RENTAL_DWELLING = "6";
        private const string LOB_AVIATION = "8";
        public string header;
        int customerID = 0, quoteId = 0, qqId = 0, showDetails = 0, appID = 0, appVersionID = 0, dwellingID = 2, PolicyID = 0, PolicyVersionID = 0, PolicyNewVersionID = 0, ProcessRowID = 0, CntGrossPrmCal = 0;
        //int customerID = 0, quoteId = 0, qqId = 0, showDetails = 0, appID = 0, appVersionID = 0, dwellingID = 2, PolicyID = 0, PolicyVersionID = 0, PolicyNewVersionID = 0, ProcessRowID = 0, CntGrossPrmCal = 0;
        DateTime EFFECTIVE_DATE, EXPIRY_DATE;
        string showRateDetails="", appLobID = "", strCalledFrom = "", QQInputXml = "", QQPremiumXmlOld = "", QQPremiumXmlNew = "", QQAPPNumber = "";
        bool blBoat_Home = false;
        XPathNavigator nav;

        eRateEngine objeRateEngine;
        QuoteRequestType objQuoteRequestType;
        QuoteResponseType objQuoteResponseType;

        ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
        public Quote()
        { }

        //Added By Kuldeep on 4-feb-2012

        public Quote(string CALLEDFROM, int CUCTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, string LOBID, int QUOTE_ID, string ONLYEFFECTIVE, string SHOW,bool Is_Rule_verification_req)
        {
            customerID = CUCTOMER_ID;
            PolicyID = POLICY_ID;
            PolicyVersionID = POLICY_VERSION_ID;
            appLobID = LOBID;
            quoteId = QUOTE_ID;
            strCalledFrom = CALLEDFROM;
            showDetails = int.Parse(SHOW);
            //IS_RULE_VERIFICATION_REQUIRED = Is_Rule_verification_req;
        }

        public void Generate_Quote_Details()
        {
            try
            {
                //get the values from the querystring
               // GetQueryString();
                SetCultureThread(GetLanguageCode());//Added by Lalit .itrack # 1402.set page culture 
                setcaption();
                if (!strCalledFrom.ToUpper().ToString().Equals("QUOTE_POL") && !strCalledFrom.ToUpper().ToString().Equals("QQ") && !strCalledFrom.ToUpper().ToString().Equals("QAPP"))
                {
                    // to display Policy premium
                    #region Application
                    switch (showDetails)
                    {
                        case SHOW_QUOTE_DETAILS:
                        case SHOW_OLD_QUOTE_DETAILS:
                            //FETCH THE DATA FROM THE TABLE
                            ClsGenerateQuote objGenerateQuote = new ClsGenerateQuote(CarrierSystemID);
                            //DataSet dsTemp = objGenerateQuote.FetchQuote(customerID ,quoteId);
                            DataSet dsTemp = objGenerateQuote.FetchQuote(customerID, quoteId, appID, appVersionID);
                            //int dwelling;
                            if (dsTemp != null && dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0)
                            {
                                string quoteFirstComponentXml = "", quoteSecondComponentXml = "";
                                quoteFirstComponentXml = dsTemp.Tables[0].Rows[0]["QUOTE_XML"].ToString();
                                quoteFirstComponentXml = ShowPremium(quoteFirstComponentXml);

                                //check the rowcount
                                if (dsTemp.Tables[0].Rows.Count > 1)
                                {
                                    //if count>1 then it has both home+boat
                                    appLobID = LOB_WATERCRAFT;
                                    quoteSecondComponentXml = dsTemp.Tables[0].Rows[1]["QUOTE_XML"].ToString();
                                    quoteSecondComponentXml = ShowPremium(quoteSecondComponentXml);

                                }
                                string strPremium = quoteFirstComponentXml + quoteSecondComponentXml;
                                strPremium = UndeclaredEntitiesInXml(strPremium); //Added on 18April 2008
                                //Response.Write(strPremium);
                                //Response.End();
                            }
                            break;
                        case SHOW_QUICK_RATE:

                            //string rateHtml = GeteRateXML(); //GenerateQuickQuote();
                            string rateHtml = GenerateQuickQuote();
                            showRateDetails = UndeclaredEntitiesInXml(rateHtml);
                            //Response.Write(strPremium);
                            //Response.Write(rateHtml);
                            //Response.End();

                            break;
                        case SHOW_WRONG_INPUT:

                            string verificationHTML = "";
                            ClsGenerateQuote ObjGenerQuote = new ClsGenerateQuote(CarrierSystemID);
                            ClsGeneralInformation objGenInformation = new ClsGeneralInformation();
                            string strInputXML = objGenInformation.GetInputXML(customerID, appID, appVersionID, appLobID);
                            string InputxmlVerificationQuote = ClsCommon.GetKeyValueForSetup("InputVerificationForQuote");//System.Configuration.ConfigurationSettings.AppSettings.Get("InputVerificationForQuote").ToString();
                            if (InputxmlVerificationQuote.Trim().ToUpper() == "Y")
                            {
                                string retVal = ObjGenerQuote.InputXmlVerification(strInputXML, appLobID); //return is in the format string#0 or string#1 . 0 --> invalid  1-->valid
                                string[] retValue = retVal.Split('#');
                                verificationHTML = retValue[0];
                                //Response.Write(verificationHTML);
                                //Response.End();
                            }
                            else
                            {
                                //Response.Write("This Application has been modified. Please verify Application again");
                            }
                            return;
                        case SHOW_FAILED_MESSAGE:

                            break;

                        case SHOW_POLICY_QUOTE:
                            //FETCH THE DATA FROM THE TABLE
                            ClsGeneralInformation ObjGenInformation = new ClsGeneralInformation();
                            dsTemp = ObjGenInformation.GetPolicyQuoteDetails(customerID, PolicyID, PolicyVersionID);
                            if (dsTemp != null && dsTemp.Tables[0] != null)
                            {
                                string quoteXml = dsTemp.Tables[0].Rows[0]["POLICY_PREMIUM_XML"].ToString();
                                // Transform the QUOTE_XML depending on the LOB and show it on screen.
                                string strPremium = ShowPremium(quoteXml);
                                strPremium = strPremium.Replace("H673GSUYD7G3J73UDH", "'");
                                strPremium = strPremium.Replace("D673GSUYD7G3J73UDD", "\"");
                                //Response.Write(strPremium);
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                else if (!strCalledFrom.ToUpper().ToString().Equals("QQ") && !strCalledFrom.ToUpper().ToString().Equals("QAPP"))
                {

                    # region for old Lobs
                    Cms.BusinessLayer.BlQuote.ClsGenerateQuote objGenerateQuote = new Cms.BusinessLayer.BlQuote.ClsGenerateQuote(CarrierSystemID);
                    string strCSSNo = base.GetColorScheme();
                    int intPolQuote_ID;
                    showDetails = objGenerateQuote.GeneratePolicyQuote(customerID, PolicyID, PolicyVersionID, appLobID, strCSSNo, out intPolQuote_ID, GetUserId());
                    quoteId = intPolQuote_ID;
                    //if (IS_RULE_VERIFICATION_REQUIRED == false)
                    //    showDetails = 1;//Added by kuldeep to generate premium after saving vehicle risk
                
                    if (showDetails == 5)
                    {
                        //Response.Write(Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1169"));//This policy has been modified. Please verify policy again
                        return;

                    }
                    ///////////
                    switch (showDetails)
                    {
                        case SHOW_QUOTE_DETAILS:
                        case SHOW_OLD_QUOTE_DETAILS:
                            DataSet dsTemp = new DataSet();
                            //Created by kuldeep for singapore
                            if (((GetSystemId().ToUpper() == "S001" || GetSystemId().ToUpper() == "SUAT")) && GetLOBID().ToString() != "1")
                            {
                                string rateHtml = GenerateQuickQuote();
                                showRateDetails = UndeclaredEntitiesInXml(rateHtml);
                                //Response.Write(strPremium);
                                //Response.Write(rateHtml);
                            }
                            else
                            {
                                //FETCH THE DATA FROM THE TABLE
                                ClsGenerateQuote objGenerQuote = new ClsGenerateQuote(CarrierSystemID);
                                dsTemp = objGenerQuote.FetchQuote_Pol(customerID, quoteId, PolicyID, PolicyVersionID);

                                if (dsTemp != null && dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0)
                                {
                                    string quoteFirstComponentXml = "", quoteSecondComponentXml = "";
                                    quoteFirstComponentXml = dsTemp.Tables[0].Rows[0]["QUOTE_XML"].ToString();

                                    if (Request.QueryString["ONLYEFFECTIVE"] != null && Request.QueryString["ONLYEFFECTIVE"].ToUpper() != "FALSE")
                                    {
                                        //quoteFirstComponentXml = AppendEffectivePremiumNode(quoteFirstComponentXml,0);
                                        quoteFirstComponentXml = AppendEffectivePremiumNode(dsTemp);

                                        #region Temp Code to Alter Premium xml change policy version in xml to show current verion

                                        string newValue = string.Empty;
                                        string DocXML = string.Empty;
                                        XmlDocument xmlDoc = new XmlDocument();
                                        string SubstringXML = quoteFirstComponentXml;
                                        int NodePlace = SubstringXML.IndexOf("<PRIMIUM>");
                                        string CheckXML = SubstringXML.Substring(NodePlace, SubstringXML.Length - NodePlace);
                                        DocXML = SubstringXML.Substring(0, NodePlace);

                                        xmlDoc.LoadXml(CheckXML);
                                        XmlNode node = xmlDoc.SelectSingleNode("PRIMIUM/CLIENT_TOP_INFO");
                                        DataSet DsPolicy = ClsCommon.GetPolicyDisplayVersion(customerID, PolicyID, PolicyVersionID, null);
                                        String PolicyVersion = string.Empty;
                                        if (DsPolicy != null && DsPolicy.Tables.Count > 0 && DsPolicy.Tables[0].Rows.Count > 0)
                                        {
                                            PolicyVersion = DsPolicy.Tables[0].Rows[0]["POLICY_DISP_VERSION"].ToString();
                                        }
                                        node.Attributes["POL_VERSION"].Value = PolicyVersion;
                                        string FinalXML = DocXML + xmlDoc.InnerXml.ToString();

                                        #endregion

                                        quoteFirstComponentXml = ShowPremium(FinalXML, "EFFECTIVEPREMIUM");
                                    }
                                    else
                                        quoteFirstComponentXml = ShowPremium(quoteFirstComponentXml);




                                    //check the rowcount
                                    if (dsTemp.Tables[0].Rows.Count > 1)
                                    {
                                        //if count>1 then it has both home+boat
                                        appLobID = LOB_WATERCRAFT;
                                        quoteSecondComponentXml = dsTemp.Tables[0].Rows[1]["QUOTE_XML"].ToString();

                                        if (Request.QueryString["ONLYEFFECTIVE"] != null && Request.QueryString["ONLYEFFECTIVE"].ToUpper() != "FALSE")
                                        {
                                            //quoteSecondComponentXml = AppendEffectivePremiumNode(quoteSecondComponentXml,1);
                                            //quoteSecondComponentXml=ShowPremium(quoteSecondComponentXml,"EFFECTIVEPREMIUM");
                                        }
                                        else
                                        {
                                            quoteSecondComponentXml = ShowPremium(quoteSecondComponentXml);
                                        }

                                    }

                                    string strPremium = quoteFirstComponentXml + quoteSecondComponentXml;
                                    strPremium = UndeclaredEntitiesInXml(strPremium); //Added on 18April 2008
                                    //Response.Write(strPremium);
                                    //Response.End();
                                }
                            }
                            break;
                        case SHOW_WRONG_INPUT:
                            // This function has to be modify 
                            //get the input xml and transform it to show the gaps
                            /*string strRulePath= Server.MapPath(@"\cms\cmsweb\XSL\Quote\ho\ho-3\InputMessage.xsl");
                            XslTransform xslt = new XslTransform();
                            xslt.Load(strRulePath);
                            string strReturnXML = objGeneralInformation.GetInputXML(customerID,appID,appVersionID);
                            // Transform the file and output an HTML string.
                            StringWriter writer = new StringWriter();
                            XmlDocument xmlDocTemp = new XmlDocument();
                            xmlDocTemp.LoadXml("<INPUTXML>" +strReturnXML+"</INPUTXML>"); 					
                            nav = ((IXPathNavigable) xmlDocTemp).CreateNavigator();
                            xslt.Transform(nav,null,writer);
   
                            string XMLoutput = writer.ToString();
                            if (XMLoutput.Trim()!="")
                            {
                                Response.Write(XMLoutput);
                            }*/
                            string verificationHTML = "";
                            ClsGenerateQuote ObjGenerQuote = new ClsGenerateQuote(CarrierSystemID);
                            ClsGeneralInformation objGenInformation = new ClsGeneralInformation();
                            string strInputXML = objGenInformation.GetPolicyInputXML(customerID, PolicyID, PolicyVersionID, appLobID);
                            string inputxmlverificationquote = ClsCommon.GetKeyValueForSetup("InputVerificationForQuote");//System.Configuration.ConfigurationSettings.AppSettings.Get("InputVerificationForQuote").ToString();
                            if (inputxmlverificationquote.Trim().ToUpper() == "Y")
                            {
                                string retVal = ObjGenerQuote.InputXmlVerification(strInputXML, appLobID); //return is in the format string#0 or string#1 . 0 --> invalid  1-->valid
                                string[] retValue = retVal.Split('#');
                                verificationHTML = retValue[0];
                                //Response.Write(verificationHTML);
                                //Response.End();
                            }
                            else
                            {
                                //Response.Write("This Application has been modified. Please verify Application again");
                            }

                            break;
                        case SHOW_FAILED_MESSAGE:

                            break;

                        case SHOW_POLICY_QUOTE:
                            //FETCH THE DATA FROM THE TABLE
                            ClsGeneralInformation ObjGenInformation = new ClsGeneralInformation();
                            dsTemp = ObjGenInformation.GetPolicyQuoteDetails(customerID, PolicyID, PolicyVersionID);
                            if (dsTemp != null && dsTemp.Tables[0] != null)
                            {
                                string quoteXml = dsTemp.Tables[0].Rows[0]["POLICY_PREMIUM_XML"].ToString();
                                // Transform the QUOTE_XML depending on the LOB and show it on screen.
                                string strPremium = ShowPremium(quoteXml);
                                strPremium = strPremium.Replace("H673GSUYD7G3J73UDH", "'");
                                strPremium = strPremium.Replace("D673GSUYD7G3J73UDD", "\"");
                                //Response.Write(strPremium);
                                //Response.End();
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion

                }
                //Modified by Lalit August 03,2010
                else if (strCalledFrom.ToUpper().ToString().Equals("QAPP")) // added by sonal show show quote from quick app
                {
                    string strQappPrmXml = "";
                    ClsGenerateQuote objGenerateQuote = new ClsGenerateQuote(CarrierSystemID);
                    int QappQoteId;
                    //DataSet dsTemp = objGenerateQuote.FetchQuote(customerID ,quoteId);
                    QappQoteId = objGenerateQuote.GetCoverages_QuoteId(customerID, PolicyID, PolicyVersionID, int.Parse(appLobID), int.Parse(GetColorScheme()), int.Parse(GetUserId()), strCalledFrom);

                    DataSet dsTemp = objGenerateQuote.FetchQuote(customerID, QappQoteId, PolicyID, PolicyVersionID);
                    string QappXml;
                    if (dsTemp != null && dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0)
                    {
                        QappXml = dsTemp.Tables[0].Rows[0]["QUOTE_XML"].ToString();
                        if (QappXml != "")
                            strQappPrmXml = ShowPremium(QappXml);
                        strQappPrmXml = strQappPrmXml.Replace("H673GSUYD7G3J73UDH", "'");
                        strQappPrmXml = strQappPrmXml.Replace("D673GSUYD7G3J73UDD", "\"");
                        //Response.Write(strQappPrmXml);
                        //Response.End();
                    }
                }
                else if (strCalledFrom.ToUpper().ToString().Equals("QQUOTE")) // added by sonal show show quote from quick app
                {
                    //Response.Write(showDetails);
                    //Response.End();

                }
                else
                {
                    //to display QQ Rates
                    #region Quick Quote
                    //FETCH THE DATA FROM THE TABLE
                    ClsGenerateQuote objGenerateQuote = new ClsGenerateQuote(CarrierSystemID);
                    //DataSet dsTemp = objGenerateQuote.FetchQuote(customerID ,quoteId);
                    DataSet dsTemp = objGenerateQuote.FetchQuote(customerID, qqId);
                    if (dsTemp != null && dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0)
                    {
                        string quoteFirstComponentXml = "", quoteSecondComponentXml = "";
                        QQPremiumXmlOld = dsTemp.Tables[0].Rows[0]["QQ_RATING_REPORT"].ToString();
                        QQInputXml = dsTemp.Tables[0].Rows[0]["QQ_XML"].ToString();
                        QQAPPNumber = dsTemp.Tables[0].Rows[0]["QQ_APP_NUMBER"].ToString();
                        if (appLobID == "2")
                            QQPremiumXmlOld = QQPremiumXmlOld.Replace("AMP;", "amp;");
                        else
                            QQPremiumXmlOld = QQPremiumXmlOld.Replace("AMP;", "");
                        if (QQAPPNumber == "")
                        {
                            QQPremiumXmlNew = objGenerateQuote.GetQuoteXML(QQInputXml, appLobID);
                            quoteFirstComponentXml = QQPremiumXmlNew;
                            ClsQuickQuote objQuickQuote = new ClsQuickQuote();
                            objQuickQuote.UpdateQuickQuoteRatingReport(customerID.ToString(), qqId.ToString(), quoteFirstComponentXml);

                        }
                        else
                        {
                            quoteFirstComponentXml = QQPremiumXmlOld;
                        }
                        quoteFirstComponentXml = ShowPremium(quoteFirstComponentXml);
                        string strPremium = quoteFirstComponentXml + quoteSecondComponentXml;
                        strPremium = UndeclaredEntitiesInXml(strPremium); //Added on 18April 2008
                        //Response.Write(strPremium);
                        //Response.End();
                    }

                    #endregion
                }
                //}
            }
            catch (Exception exc)
            {
                //throw (exc);
                lblMessage.Text = "Error Occured while generating Quote: " + exc.Message;


            }
            finally
            { }


        }
        public void Page_Load(object sender, System.EventArgs e)
        {

            try
            {
                //get the values from the querystring
                GetQueryString();
                SetCultureThread(GetLanguageCode());//Added by Lalit .itrack # 1402.set page culture 
                setcaption();
                if (!strCalledFrom.ToUpper().ToString().Equals("QUOTE_POL") && !strCalledFrom.ToUpper().ToString().Equals("QQ") && !strCalledFrom.ToUpper().ToString().Equals("QAPP"))
                {
                    // to display Policy premium
                    #region Application
                    switch (showDetails)
                    {
                        case SHOW_QUOTE_DETAILS:
                        case SHOW_OLD_QUOTE_DETAILS:
                            //FETCH THE DATA FROM THE TABLE
                            ClsGenerateQuote objGenerateQuote = new ClsGenerateQuote(CarrierSystemID);
                            //DataSet dsTemp = objGenerateQuote.FetchQuote(customerID ,quoteId);
                            DataSet dsTemp = objGenerateQuote.FetchQuote(customerID, quoteId, appID, appVersionID);
                            //int dwelling;
                            if (dsTemp != null && dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0)
                            {
                                string quoteFirstComponentXml = "", quoteSecondComponentXml = "";
                                quoteFirstComponentXml = dsTemp.Tables[0].Rows[0]["QUOTE_XML"].ToString();
                                quoteFirstComponentXml = ShowPremium(quoteFirstComponentXml);

                                //check the rowcount
                                if (dsTemp.Tables[0].Rows.Count > 1)
                                {
                                    //if count>1 then it has both home+boat
                                    appLobID = LOB_WATERCRAFT;
                                    quoteSecondComponentXml = dsTemp.Tables[0].Rows[1]["QUOTE_XML"].ToString();
                                    quoteSecondComponentXml = ShowPremium(quoteSecondComponentXml);

                                }
                                string strPremium = quoteFirstComponentXml + quoteSecondComponentXml;
                                strPremium = UndeclaredEntitiesInXml(strPremium); //Added on 18April 2008
                                Response.Write(strPremium);
                                Response.End();
                            }
                            break;
                        case SHOW_QUICK_RATE:

                            //string rateHtml = GeteRateXML(); //GenerateQuickQuote();
                            string rateHtml = GenerateQuickQuote();
                            showRateDetails = UndeclaredEntitiesInXml(rateHtml);
                            //Response.Write(strPremium);
                            Response.Write(rateHtml);
                            //Response.End();

                            break;
                        case SHOW_WRONG_INPUT:

                            string verificationHTML = "";
                            ClsGenerateQuote ObjGenerQuote = new ClsGenerateQuote(CarrierSystemID);
                            ClsGeneralInformation objGenInformation = new ClsGeneralInformation();
                            string strInputXML = objGenInformation.GetInputXML(customerID, appID, appVersionID, appLobID);
                            string InputxmlVerificationQuote = ClsCommon.GetKeyValueForSetup("InputVerificationForQuote");//System.Configuration.ConfigurationSettings.AppSettings.Get("InputVerificationForQuote").ToString();
                            if (InputxmlVerificationQuote.Trim().ToUpper() == "Y")
                            {
                                string retVal = ObjGenerQuote.InputXmlVerification(strInputXML, appLobID); //return is in the format string#0 or string#1 . 0 --> invalid  1-->valid
                                string[] retValue = retVal.Split('#');
                                verificationHTML = retValue[0];
                                Response.Write(verificationHTML);
                                Response.End();
                            }
                            else
                            {
                                Response.Write("This Application has been modified. Please verify Application again");
                            }
                            return;
                        case SHOW_FAILED_MESSAGE:

                            break;

                        case SHOW_POLICY_QUOTE:
                            //FETCH THE DATA FROM THE TABLE
                            ClsGeneralInformation ObjGenInformation = new ClsGeneralInformation();
                            dsTemp = ObjGenInformation.GetPolicyQuoteDetails(customerID, PolicyID, PolicyVersionID);
                            if (dsTemp != null && dsTemp.Tables[0] != null)
                            {
                                string quoteXml = dsTemp.Tables[0].Rows[0]["POLICY_PREMIUM_XML"].ToString();
                                // Transform the QUOTE_XML depending on the LOB and show it on screen.
                                string strPremium = ShowPremium(quoteXml);
                                strPremium = strPremium.Replace("H673GSUYD7G3J73UDH", "'");
                                strPremium = strPremium.Replace("D673GSUYD7G3J73UDD", "\"");
                                Response.Write(strPremium);
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                else if (!strCalledFrom.ToUpper().ToString().Equals("QQ") && !strCalledFrom.ToUpper().ToString().Equals("QAPP"))
                {

                    # region for old Lobs
                    Cms.BusinessLayer.BlQuote.ClsGenerateQuote objGenerateQuote = new Cms.BusinessLayer.BlQuote.ClsGenerateQuote(CarrierSystemID);
                    string strCSSNo = base.GetColorScheme();
                    int intPolQuote_ID;
                    showDetails = objGenerateQuote.GeneratePolicyQuote(customerID, PolicyID, PolicyVersionID, appLobID, strCSSNo, out intPolQuote_ID, GetUserId());
                    quoteId = intPolQuote_ID;

                    if (showDetails == 5)
                    {
                        Response.Write(Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1169"));//This policy has been modified. Please verify policy again
                        return;

                    }
                    ///////////
                    switch (showDetails)
                    {
                        case SHOW_QUOTE_DETAILS:
                        case SHOW_OLD_QUOTE_DETAILS:
                            DataSet dsTemp = new DataSet();
                            //Created by kuldeep for singapore
                            if (((GetSystemId().ToUpper() == "S001" || GetSystemId().ToUpper() == "SUAT")) && GetLOBID().ToString() != "1")
                            {
                                string rateHtml = GenerateQuickQuote();
                                showRateDetails = UndeclaredEntitiesInXml(rateHtml);
                                //Response.Write(strPremium);
                                Response.Write(rateHtml);
                            }
                            else
                            {
                                //FETCH THE DATA FROM THE TABLE
                                ClsGenerateQuote objGenerQuote = new ClsGenerateQuote(CarrierSystemID);
                                dsTemp = objGenerQuote.FetchQuote_Pol(customerID, quoteId, PolicyID, PolicyVersionID);

                                if (dsTemp != null && dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0)
                                {
                                    string quoteFirstComponentXml = "", quoteSecondComponentXml = "";
                                    quoteFirstComponentXml = dsTemp.Tables[0].Rows[0]["QUOTE_XML"].ToString();

                                    if (Request.QueryString["ONLYEFFECTIVE"] != null && Request.QueryString["ONLYEFFECTIVE"].ToUpper() != "FALSE")
                                    {
                                        //quoteFirstComponentXml = AppendEffectivePremiumNode(quoteFirstComponentXml,0);
                                        quoteFirstComponentXml = AppendEffectivePremiumNode(dsTemp);

                                        #region Temp Code to Alter Premium xml change policy version in xml to show current verion

                                        string newValue = string.Empty;
                                        string DocXML = string.Empty;
                                        XmlDocument xmlDoc = new XmlDocument();
                                        string SubstringXML = quoteFirstComponentXml;
                                        int NodePlace = SubstringXML.IndexOf("<PRIMIUM>");
                                        string CheckXML = SubstringXML.Substring(NodePlace, SubstringXML.Length - NodePlace);
                                        DocXML = SubstringXML.Substring(0, NodePlace);

                                        xmlDoc.LoadXml(CheckXML);
                                        XmlNode node = xmlDoc.SelectSingleNode("PRIMIUM/CLIENT_TOP_INFO");
                                        DataSet DsPolicy = ClsCommon.GetPolicyDisplayVersion(customerID, PolicyID, PolicyVersionID, null);
                                        String PolicyVersion = string.Empty;
                                        if (DsPolicy != null && DsPolicy.Tables.Count > 0 && DsPolicy.Tables[0].Rows.Count > 0)
                                        {
                                            PolicyVersion = DsPolicy.Tables[0].Rows[0]["POLICY_DISP_VERSION"].ToString();
                                        }
                                        node.Attributes["POL_VERSION"].Value = PolicyVersion;
                                        string FinalXML = DocXML + xmlDoc.InnerXml.ToString();

                                        #endregion

                                        quoteFirstComponentXml = ShowPremium(FinalXML, "EFFECTIVEPREMIUM");
                                    }
                                    else
                                        quoteFirstComponentXml = ShowPremium(quoteFirstComponentXml);




                                    //check the rowcount
                                    if (dsTemp.Tables[0].Rows.Count > 1)
                                    {
                                        //if count>1 then it has both home+boat
                                        appLobID = LOB_WATERCRAFT;
                                        quoteSecondComponentXml = dsTemp.Tables[0].Rows[1]["QUOTE_XML"].ToString();

                                        if (Request.QueryString["ONLYEFFECTIVE"] != null && Request.QueryString["ONLYEFFECTIVE"].ToUpper() != "FALSE")
                                        {
                                            //quoteSecondComponentXml = AppendEffectivePremiumNode(quoteSecondComponentXml,1);
                                            //quoteSecondComponentXml=ShowPremium(quoteSecondComponentXml,"EFFECTIVEPREMIUM");
                                        }
                                        else
                                        {
                                            quoteSecondComponentXml = ShowPremium(quoteSecondComponentXml);
                                        }

                                    }

                                    string strPremium = quoteFirstComponentXml + quoteSecondComponentXml;
                                    strPremium = UndeclaredEntitiesInXml(strPremium); //Added on 18April 2008
                                    Response.Write(strPremium);
                                    Response.End();
                                }
                            }
                            break;
                        case SHOW_WRONG_INPUT:
                            // This function has to be modify 
                            //get the input xml and transform it to show the gaps
                            /*string strRulePath= Server.MapPath(@"\cms\cmsweb\XSL\Quote\ho\ho-3\InputMessage.xsl");
                            XslTransform xslt = new XslTransform();
                            xslt.Load(strRulePath);
                            string strReturnXML = objGeneralInformation.GetInputXML(customerID,appID,appVersionID);
                            // Transform the file and output an HTML string.
                            StringWriter writer = new StringWriter();
                            XmlDocument xmlDocTemp = new XmlDocument();
                            xmlDocTemp.LoadXml("<INPUTXML>" +strReturnXML+"</INPUTXML>"); 					
                            nav = ((IXPathNavigable) xmlDocTemp).CreateNavigator();
                            xslt.Transform(nav,null,writer);
   
                            string XMLoutput = writer.ToString();
                            if (XMLoutput.Trim()!="")
                            {
                                Response.Write(XMLoutput);
                            }*/
                            string verificationHTML = "";
                            ClsGenerateQuote ObjGenerQuote = new ClsGenerateQuote(CarrierSystemID);
                            ClsGeneralInformation objGenInformation = new ClsGeneralInformation();
                            string strInputXML = objGenInformation.GetPolicyInputXML(customerID, PolicyID, PolicyVersionID, appLobID);
                            string inputxmlverificationquote = ClsCommon.GetKeyValueForSetup("InputVerificationForQuote");//System.Configuration.ConfigurationSettings.AppSettings.Get("InputVerificationForQuote").ToString();
                            if (inputxmlverificationquote.Trim().ToUpper() == "Y")
                            {
                                string retVal = ObjGenerQuote.InputXmlVerification(strInputXML, appLobID); //return is in the format string#0 or string#1 . 0 --> invalid  1-->valid
                                string[] retValue = retVal.Split('#');
                                verificationHTML = retValue[0];
                                Response.Write(verificationHTML);
                                Response.End();
                            }
                            else
                            {
                                Response.Write("This Application has been modified. Please verify Application again");
                            }

                            break;
                        case SHOW_FAILED_MESSAGE:

                            break;

                        case SHOW_POLICY_QUOTE:
                            //FETCH THE DATA FROM THE TABLE
                            ClsGeneralInformation ObjGenInformation = new ClsGeneralInformation();
                            dsTemp = ObjGenInformation.GetPolicyQuoteDetails(customerID, PolicyID, PolicyVersionID);
                            if (dsTemp != null && dsTemp.Tables[0] != null)
                            {
                                string quoteXml = dsTemp.Tables[0].Rows[0]["POLICY_PREMIUM_XML"].ToString();
                                // Transform the QUOTE_XML depending on the LOB and show it on screen.
                                string strPremium = ShowPremium(quoteXml);
                                strPremium = strPremium.Replace("H673GSUYD7G3J73UDH", "'");
                                strPremium = strPremium.Replace("D673GSUYD7G3J73UDD", "\"");
                                Response.Write(strPremium);
                                Response.End();
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion

                }
                //Modified by Lalit August 03,2010
                else if (strCalledFrom.ToUpper().ToString().Equals("QAPP")) // added by sonal show show quote from quick app
                {
                    string strQappPrmXml = "";
                    ClsGenerateQuote objGenerateQuote = new ClsGenerateQuote(CarrierSystemID);
                    int QappQoteId;
                    //DataSet dsTemp = objGenerateQuote.FetchQuote(customerID ,quoteId);
                    QappQoteId = objGenerateQuote.GetCoverages_QuoteId(customerID, PolicyID, PolicyVersionID, int.Parse(appLobID), int.Parse(GetColorScheme()), int.Parse(GetUserId()), strCalledFrom);

                    DataSet dsTemp = objGenerateQuote.FetchQuote(customerID, QappQoteId, PolicyID, PolicyVersionID);
                    string QappXml;
                    if (dsTemp != null && dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0)
                    {
                        QappXml = dsTemp.Tables[0].Rows[0]["QUOTE_XML"].ToString();
                        if (QappXml != "")
                            strQappPrmXml = ShowPremium(QappXml);
                        strQappPrmXml = strQappPrmXml.Replace("H673GSUYD7G3J73UDH", "'");
                        strQappPrmXml = strQappPrmXml.Replace("D673GSUYD7G3J73UDD", "\"");
                        Response.Write(strQappPrmXml);
                        Response.End();
                    }
                }
                else if (strCalledFrom.ToUpper().ToString().Equals("QQUOTE")) // added by sonal show show quote from quick app
                {
                    Response.Write(showDetails);
                    Response.End();

                }
                else
                {
                    //to display QQ Rates
                    #region Quick Quote
                    //FETCH THE DATA FROM THE TABLE
                    ClsGenerateQuote objGenerateQuote = new ClsGenerateQuote(CarrierSystemID);
                    //DataSet dsTemp = objGenerateQuote.FetchQuote(customerID ,quoteId);
                    DataSet dsTemp = objGenerateQuote.FetchQuote(customerID, qqId);
                    if (dsTemp != null && dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0)
                    {
                        string quoteFirstComponentXml = "", quoteSecondComponentXml = "";
                        QQPremiumXmlOld = dsTemp.Tables[0].Rows[0]["QQ_RATING_REPORT"].ToString();
                        QQInputXml = dsTemp.Tables[0].Rows[0]["QQ_XML"].ToString();
                        QQAPPNumber = dsTemp.Tables[0].Rows[0]["QQ_APP_NUMBER"].ToString();
                        if (appLobID == "2")
                            QQPremiumXmlOld = QQPremiumXmlOld.Replace("AMP;", "amp;");
                        else
                            QQPremiumXmlOld = QQPremiumXmlOld.Replace("AMP;", "");
                        if (QQAPPNumber == "")
                        {
                            QQPremiumXmlNew = objGenerateQuote.GetQuoteXML(QQInputXml, appLobID);
                            quoteFirstComponentXml = QQPremiumXmlNew;
                            ClsQuickQuote objQuickQuote = new ClsQuickQuote();
                            objQuickQuote.UpdateQuickQuoteRatingReport(customerID.ToString(), qqId.ToString(), quoteFirstComponentXml);

                        }
                        else
                        {
                            quoteFirstComponentXml = QQPremiumXmlOld;
                        }
                        quoteFirstComponentXml = ShowPremium(quoteFirstComponentXml);
                        string strPremium = quoteFirstComponentXml + quoteSecondComponentXml;
                        strPremium = UndeclaredEntitiesInXml(strPremium); //Added on 18April 2008
                        Response.Write(strPremium);
                        Response.End();
                    }

                    #endregion
                }
                //}
            }
            catch (Exception exc)
            {
                //throw (exc);
                lblMessage.Text = "Error Occured while generating Quote: " + exc.Message;


            }
            finally
            { }

        }
        #region Handle Undeclared Entity / Invalid Char in XML (Application and Policy Rates) : Praveen

        private string UndeclaredEntitiesInXml(string strPremiumXml)
        {
            strPremiumXml = strPremiumXml.Replace("H673GSUYD7G3J73UDH", "'");
            strPremiumXml = strPremiumXml.Replace("D673GSUYD7G3J73UDD", "\"");
            /*Remove Invalid Char from XML*/
            strPremiumXml = strPremiumXml.Replace("&amp;", "&");
            strPremiumXml = strPremiumXml.Replace("&amp;gt;", ">");
            strPremiumXml = strPremiumXml.Replace("&amp;lt;", "<");
            strPremiumXml = strPremiumXml.Replace("&amp;GT;", ">");
            strPremiumXml = strPremiumXml.Replace("&amp;LT;", "<");
            //After Converting GT and LT..COnvert & Sign.
            strPremiumXml = strPremiumXml.Replace("&amp;", "&");
            return strPremiumXml;

        }
        #endregion
        private string AppendEffectivePremiumNode(DataSet dsTempXml)
        {
            //System.Threading.
            //Thread.CurrentThread.CurrentCulture objOldThread = new System.Globalization.CultureInfo(); 
            System.Globalization.CultureInfo oldculture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            //System.Threading.Thread.CurrentThread.CurrentUICulture

            double EffectivePremium = 0, GrossPremium = 0, NewGrossPremium = 0, EffectivePremiumProRata = 0;
            string quoteFirstComponentXml = "", quoteSecondComponentXml = "", quoteXml = "";
            quoteFirstComponentXml = dsTempXml.Tables[0].Rows[0]["QUOTE_XML"].ToString();
            int NewquoteId = 0;
            XmlDocument doc = new XmlDocument();
            ClsGenerateQuote objGenerQuote = new ClsGenerateQuote(CarrierSystemID);
            int intPolQuote_ID;
            string strCSSNo = base.GetColorScheme();

            // Gross Premium for previous version of policy
            doc.LoadXml(quoteFirstComponentXml);
            XmlNodeList xmlPremium = doc.GetElementsByTagName("PRIMIUM");
            quoteXml = xmlPremium.Item(0).OuterXml;
            doc.LoadXml(quoteXml);

            //Commented by Lalit august,2010            
            //GrossPremium = GetGrossPremium(doc);            
            //modified by Lalit
            NewGrossPremium = GetGrossPremium(doc);

            if (appLobID == LOB_HOME && dsTempXml.Tables[0].Rows.Count > 1)
            {
                quoteSecondComponentXml = dsTempXml.Tables[0].Rows[1]["QUOTE_XML"].ToString();
                XmlDocument docSec = new XmlDocument();
                docSec.LoadXml(quoteSecondComponentXml);
                XmlNodeList xmlSecPremium = docSec.GetElementsByTagName("PRIMIUM");
                quoteXml = xmlSecPremium.Item(0).OuterXml;
                docSec.LoadXml(quoteXml);
                appLobID = LOB_WATERCRAFT;
                GrossPremium = GrossPremium + GetGrossPremium(docSec);
                appLobID = LOB_HOME;
            }
            if (PolicyNewVersionID == 0)
                PolicyNewVersionID = PolicyVersionID;
            // Gross Premium for current version of policy
            showDetails = objGenerQuote.GeneratePolicyQuote(customerID, PolicyID, PolicyNewVersionID, appLobID, strCSSNo, out intPolQuote_ID, GetUserId());
            NewquoteId = intPolQuote_ID;
            if (showDetails == 5)
            {
                Response.Write(Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1169"));
                Response.End();
                return "";
            }
            DataSet dsTemp = objGenerQuote.FetchQuote_Pol(customerID, NewquoteId, PolicyID, PolicyNewVersionID, "ONLYEFFECTIVE");

            if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0] != null)
            {
                for (int i = 0; i < dsTemp.Tables[0].Rows.Count; i++)//changed by Lalit for new implimentation.tfs # 860
                {
                    quoteFirstComponentXml = dsTemp.Tables[0].Rows[i]["QUOTE_XML"].ToString();
                    doc.LoadXml(quoteFirstComponentXml);
                    XmlNodeList xmlNewPremium = doc.GetElementsByTagName("PRIMIUM");
                    quoteFirstComponentXml = xmlNewPremium.Item(0).OuterXml;
                    doc.LoadXml(quoteFirstComponentXml);
                    GrossPremium += GetGrossPremium(doc);
                }
            }
            if (appLobID == LOB_HOME && dsTemp.Tables[0].Rows.Count > 1)
            {
                quoteSecondComponentXml = dsTemp.Tables[0].Rows[1]["QUOTE_XML"].ToString();
                doc.LoadXml(quoteSecondComponentXml);
                XmlNodeList xmlNewPremium = doc.GetElementsByTagName("PRIMIUM");
                quoteSecondComponentXml = xmlNewPremium.Item(0).OuterXml;
                doc.LoadXml(quoteSecondComponentXml);
                appLobID = LOB_WATERCRAFT;
                NewGrossPremium = NewGrossPremium + GetGrossPremium(doc);
                appLobID = LOB_HOME;
            }
            /*
            Brazil implimentation xml havng only version wise premium. total effective premium will sum of all previous version premium 
            tfs # 860
            */
            EffectivePremium = NewGrossPremium + GrossPremium; //Total policy premium //commented by Lalit sep 27,2011.
            EffectivePremiumProRata = EffectivePremium; //GetEffectivePremium(GrossPremium, NewGrossPremium);

            XmlNodeList xmlHeadr = doc.GetElementsByTagName("HEADER");
            string Header = xmlHeadr.Item(0).OuterXml;
            XmlNodeList xmlClientInfo = doc.GetElementsByTagName("CLIENT_TOP_INFO");
            string Clientinfo = xmlClientInfo.Item(0).OuterXml;
            string OutXML = Header + Clientinfo;

            OutXML = OutXML + "<TOTALOLDPREMIUM>" + GrossPremium.ToString("N") + "</TOTALOLDPREMIUM>";
            //OutXML = OutXML + "<TOTALNEWPREMIUM>" + (NewGrossPremium + EffectivePremiumProRata).ToString("N") + "</TOTALNEWPREMIUM>";
            OutXML = OutXML + "<TOTALNEWPREMIUM>" + (NewGrossPremium).ToString("N") + "</TOTALNEWPREMIUM>";
            OutXML = OutXML + "<EFFECTIVEPREMIUM>" + EffectivePremium.ToString("N") + "</EFFECTIVEPREMIUM>";
            OutXML = OutXML + "<EFFECTIVEPREMIUMPRORATA>" + EffectivePremiumProRata.ToString("N") + "</EFFECTIVEPREMIUMPRORATA>";
            OutXML = "<PRIMIUM>" + OutXML + "</PRIMIUM>";

            Thread.CurrentThread.CurrentCulture = oldculture;
            return OutXML;
        }
        private string AppendEffectivePremiumNode(string quoteXml, int flagWater)
        {

            double EffectivePremium = 0, GrossPremium = 0, NewGrossPremium = 0, EffectivePremiumProRata = 0;
            int NewquoteId = 0;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(quoteXml);
            string quoteFirstComponentXml = "", quoteSecondComponentXml = "";
            XmlNodeList xmlPremium = doc.GetElementsByTagName("PRIMIUM");
            quoteXml = xmlPremium.Item(0).OuterXml;
            doc.LoadXml(quoteXml);
            GrossPremium = GetGrossPremium(doc);
            ClsGenerateQuote objGenerQuote = new ClsGenerateQuote(CarrierSystemID);
            int intPolQuote_ID;
            string strCSSNo = base.GetColorScheme();
            if (flagWater == 0)
            {
                showDetails = objGenerQuote.GeneratePolicyQuote(customerID, PolicyID, PolicyNewVersionID, appLobID, strCSSNo, out intPolQuote_ID, GetUserId());
                NewquoteId = intPolQuote_ID;
                flagWater++;
            }

            if (showDetails == 5)
            {
                Response.Write(Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1169"));
                Response.End();
                return "";
            }
            else
            {
                DataSet dsTemp = objGenerQuote.FetchQuote_Pol(customerID, NewquoteId, PolicyID, PolicyNewVersionID);

                if (appLobID == "4" && dsTemp.Tables[0].Rows.Count > 1)
                {
                    quoteSecondComponentXml = dsTemp.Tables[0].Rows[1]["QUOTE_XML"].ToString();
                    doc.LoadXml(quoteSecondComponentXml);
                    XmlNodeList xmlNewPremium = doc.GetElementsByTagName("PRIMIUM");
                    quoteSecondComponentXml = xmlNewPremium.Item(0).OuterXml;
                    doc.LoadXml(quoteSecondComponentXml);
                    NewGrossPremium = GetGrossPremium(doc);
                }
                if (dsTemp != null && dsTemp.Tables[0] != null && CntGrossPrmCal != 1)
                {
                    quoteFirstComponentXml = dsTemp.Tables[0].Rows[0]["QUOTE_XML"].ToString();
                    doc.LoadXml(quoteFirstComponentXml);
                    XmlNodeList xmlNewPremium = doc.GetElementsByTagName("PRIMIUM");
                    quoteFirstComponentXml = xmlNewPremium.Item(0).OuterXml;
                    doc.LoadXml(quoteFirstComponentXml);
                    NewGrossPremium = GetGrossPremium(doc);
                    // If home Boat Lob
                    // Check for watercraft rate if exists then add its premium to NewGrossPremium
                    if (appLobID == LOB_HOME && dsTemp.Tables[0].Rows.Count > 1)
                    {
                        quoteSecondComponentXml = dsTemp.Tables[0].Rows[1]["QUOTE_XML"].ToString();
                        doc.LoadXml(quoteSecondComponentXml);
                        xmlNewPremium = doc.GetElementsByTagName("PRIMIUM");
                        quoteSecondComponentXml = xmlNewPremium.Item(0).OuterXml;
                        doc.LoadXml(quoteSecondComponentXml);
                        appLobID = LOB_WATERCRAFT;
                        NewGrossPremium += GetGrossPremium(doc);
                        appLobID = LOB_HOME;
                    }
                    CntGrossPrmCal++;
                }
                //				else
                //				{
                //					NewGrossPremium=0;
                //				}
            }
            //			else
            //					NewGrossPremium=0;


            EffectivePremium = NewGrossPremium - GrossPremium;

            EffectivePremiumProRata = GetEffectivePremium(GrossPremium, NewGrossPremium);
            //if (NewGrossPremium != 0)
            //{

            XmlNodeList xmlHeadr = doc.GetElementsByTagName("HEADER");
            string Header = xmlHeadr.Item(0).OuterXml;
            XmlNodeList xmlClientInfo = doc.GetElementsByTagName("CLIENT_TOP_INFO");
            string Clientinfo = xmlClientInfo.Item(0).OuterXml;
            string OutXML = Header + Clientinfo;

            OutXML = OutXML + "<TOTALOLDPREMIUM>" + GrossPremium.ToString("N") + "</TOTALOLDPREMIUM>";

            OutXML = OutXML + "<TOTALNEWPREMIUM>" + NewGrossPremium.ToString("N") + "</TOTALNEWPREMIUM>";
            OutXML = OutXML + "<EFFECTIVEPREMIUM>" + EffectivePremium.ToString("N") + "</EFFECTIVEPREMIUM>";
            OutXML = OutXML + "<EFFECTIVEPREMIUMPRORATA>" + EffectivePremiumProRata.ToString("N") + "</EFFECTIVEPREMIUMPRORATA>";
            /*XmlElement child1 = doc.CreateElement("TOTALNEWPREMIUM");
            child1.InnerXml = NewGrossPremium.ToString("N");
				
            XmlElement child2 = doc.CreateElement("EFFECTIVEPREMIUM");
            child2.InnerXml = EffectivePremium.ToString("N");
            doc.ChildNodes.Item(3).AppendChild(child2);

            XmlElement child3 = doc.CreateElement("EFFECTIVEPREMIUMPRORATA");
            child3.InnerXml = EffectivePremiumProRata.ToString("N");
            doc.ChildNodes.Item(3).AppendChild(child3);
				
            quoteXml = doc.InnerXml;*/
            OutXML = "<PRIMIUM>" + OutXML + "</PRIMIUM>";

            /*XmlNodeList nodeRemove;

            nodeRemove = doc.SelectNodes("//OPERATOR");
            for( int xnode=0;xnode< nodeRemove.Count;xnode++)
            {
                nodeRemove.Item(xnode).RemoveAll();
            }
            nodeRemove = doc.SelectNodes("//RISK");
            for( int xnode=0;xnode< nodeRemove.Count;xnode++)
            {
                nodeRemove.Item(xnode).RemoveAll();
            }
			   
        quoteXml = doc.InnerXml;
        //}

        //returning the final xml
        return quoteXml;
        */
            return OutXML;

        }

        private double GetGrossPremium(XmlDocument doc)
        {
            double GrossPremium = 0;
            XmlNodeList objList = null;
            switch (appLobID)
            {
                case LOB_HOME:
                    //objList = doc.SelectNodes("PREMIUMXML/DWELLINGDETAILS/STEP[@COMPONENT_CODE ='SUMTOTAL']/@STEPPREMIUM");
                    objList = doc.SelectNodes("PRIMIUM/GRANDTOTAL");
                    break;
                case LOB_PRIVATE_PASSENGER:
                    //objList = doc.SelectNodes("PRIMIUM/STEP[@COMPONENT_CODE ='SUMTOTAL']/@STEPPREMIUM");
                    objList = doc.SelectNodes("PRIMIUM/RISK/STEP[@COMPONENT_CODE ='SUMTOTAL']/@STEPPREMIUM");
                    break;
                case LOB_MOTORCYCLE:
                    //objList = doc.SelectNodes("PRIMIUM/STEP[@COMPONENT_CODE ='SUMTOTAL']/@STEPPREMIUM");
                    objList = doc.SelectNodes("PRIMIUM/RISK/STEP[@COMPONENT_CODE ='SUMTOTAL']/@STEPPREMIUM");
                    break;
                case LOB_WATERCRAFT:
                    objList = doc.SelectNodes("PRIMIUM/RISK/STEP[@COMPONENT_CODE ='SUMTOTAL' or @COMPONENT_CODE ='SUMTOTAL_S' or @COMPONENT_CODE ='BOAT_UNATTACH_PREMIUM']/@STEPPREMIUM");
                    break;
                case LOB_RENTAL_DWELLING:
                    //objList = doc.SelectNodes("PREMIUMXML/DWELLINGDETAILS/STEP[@COMPONENT_CODE ='SUMTOTAL']/@STEPPREMIUM");
                    objList = doc.SelectNodes("PRIMIUM/RISK/STEP[@COMPONENT_CODE ='SUMTOTAL']/@STEPPREMIUM");
                    break;
                case LOB_UMBRELLA:
                    //objList = doc.SelectNodes("PREMIUMXML/DWELLINGDETAILS/STEP[@STEPDESC ='-  Final Premium']/@STEPPREMIUM");
                    objList = doc.SelectNodes("PRIMIUM/RISK/STEP[@COMPONENT_CODE ='SUMTOTAL']/@STEPPREMIUM");
                    break;
                default:
                    objList = doc.SelectNodes("PRIMIUM/RISK/STEP[@COMPONENT_CODE ='SUMTOTAL']/@STEPPREMIUM");

                    break;

            }

            for (int i = 0; i < objList.Count; i++)
            {
                GrossPremium += double.Parse(objList.Item(i).InnerText);
            }
            return GrossPremium;
        }

        private double GetEffectivePremium(double OldGrossPremium, double NewGrossPremium)
        {
            // double dblPremium = 0, dblFees = 0, dblMCCAFees = 0;
            double EffectivePremium = 0;
            int TotalPolicyTime = 365;
            int DaysDiff = 0;
            ClsGeneralInformation objGen = new ClsGeneralInformation();
            DataSet dsPolicy = objGen.GetPolicyDetails(customerID, 0, 0, PolicyID, PolicyNewVersionID);
            if (dsPolicy.Tables[0].Rows.Count > 0)
            {
                TotalPolicyTime = Convert.ToInt32(dsPolicy.Tables[0].Rows[0]["POLICY_DAYS"].ToString());
            }
            Cms.BusinessLayer.BlProcess.ClsEndorsmentProcess objProcess = new
                Cms.BusinessLayer.BlProcess.ClsEndorsmentProcess();

            Cms.Model.Policy.Process.ClsProcessInfo objInfo = objProcess.GetRunningProcess(customerID, PolicyID);
            if (objInfo != null)
            {
                if (objInfo.PROCESS_ID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS)
                {
                    //				objProcess.GetPreviousVersionPremiumDetails(PolicyID, objInfo.POLICY_VERSION_ID, customerID
                    //					, out dblPremium, out dblFees, out dblMCCAFees);
                    //
                    //				double TotalPremium = dblPremium + dblFees + dblMCCAFees;



                    DaysDiff = TimeSpan.FromTicks(objInfo.EXPIRY_DATE.Ticks - objInfo.EFFECTIVE_DATETIME.Ticks).Days;

                    //EffectivePremium = ((GrossPremium - TotalPremium) / TotalPolicyTime) * DaysDiff;
                    //EffectivePremium = ((NewGrossPremium - OldGrossPremium) / TotalPolicyTime) * DaysDiff;

                }
            }
            else
            {
                if (EXPIRY_DATE != DateTime.MinValue && EFFECTIVE_DATE != DateTime.MinValue)
                    DaysDiff = TimeSpan.FromTicks(EXPIRY_DATE.Ticks - EFFECTIVE_DATE.Ticks).Days;
                else
                    DaysDiff = 0;
            }
            EffectivePremium = ((NewGrossPremium - OldGrossPremium) / TotalPolicyTime) * DaysDiff;

            return Convert.ToDouble(Convert.ToInt32(EffectivePremium));
        }





        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Shows the premium
        /// </summary>
        private string ShowPremium(string quoteXml)
        {
            return ShowPremium(quoteXml, "");
        }
        private string ShowPremium(string quoteXml, string CalledFor)
        {
            string finalQuoteXSLPath = "";
            //
            string msgFilepath = ClsCommon.GetKeyValueWithIP("FilePathRuleMessages");

            if (ClsCommon.BL_LANG_CULTURE == enumCulture.BR)
            {
                msgFilepath = msgFilepath.Replace(".xml", "." + ClsCommon.BL_LANG_CULTURE + ".xml");
            }
            quoteXml = quoteXml.Replace("<PRIMIUM>", "<PRIMIUM>" + "<MESSAGE_FILE_PATH>" + msgFilepath + "</MESSAGE_FILE_PATH>");

            //
            if (CalledFor == "EFFECTIVEPREMIUM")
                finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("EffectivePremiumPath");
            else
            {
                switch (appLobID)
                {
                    case LOB_HOME:
                        finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteHO3");//System.Configuration.ConfigurationSettings.AppSettings.Get("FinalQuoteHO3").ToString();
                        break;
                    case LOB_PRIVATE_PASSENGER:
                        finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteAuto");//System.Configuration.ConfigurationSettings.AppSettings.Get("FinalQuoteAuto").ToString();
                        break;
                    case LOB_MOTORCYCLE:
                        finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteCYCL");//System.Configuration.ConfigurationSettings.AppSettings.Get("FinalQuoteAuto").ToString();
                        break;
                    case LOB_WATERCRAFT:
                        finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteBOAT");//System.Configuration.ConfigurationSettings.AppSettings.Get("FinalQuoteAuto").ToString();
                        break;
                    case LOB_RENTAL_DWELLING:
                        finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteREDW");//System.Configuration.ConfigurationSettings.AppSettings.Get("FinalQuoteAuto").ToString();
                        break;
                    case LOB_UMBRELLA:
                        finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQutoteUMB");//System.Configuration.ConfigurationSettings.AppSettings.Get("FinalQuoteAuto").ToString();
                        break;
                    case LOB_AVIATION:
                        finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQutoteAVIA");//System.Configuration.ConfigurationSettings.AppSettings.Get("FinalQuoteAuto").ToString();
                        break;
                    default:
                        finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FilePathRatingXML");
                        break;

                }

            }



            string cssnum = "";
            cssnum = "<HEADER" + " CSSNUM=\"" + GetColorScheme() + "\"";
            quoteXml = quoteXml.Replace("<HEADER", cssnum);
            quoteXml = quoteXml.Replace("&gt;", ">");
            quoteXml = quoteXml.Replace("&lt;", "<");
            quoteXml = quoteXml.Replace("\"", "'");
            quoteXml = quoteXml.Replace("\r", "");
            quoteXml = quoteXml.Replace("\n", "");
            quoteXml = quoteXml.Replace("\t", "");
            quoteXml = quoteXml.Replace("<br>", "<!--br-->");
            //quoteXml=quoteXml.Replace("<META http-equiv='Content-Type' content='text/html; charset=utf-16'>","<META http-equiv='Content-Type' content='text/html; charset=utf-16'/>");
            //quoteXml=quoteXml.Replace("<LINK id='lk' href='/cms/cmsweb/css/css1.css' type='text/css' rel='stylesheet'>","<LINK id='lk' href='/cms/cmsweb/css/css1.css' type='text/css' rel='stylesheet'/>");
            quoteXml = quoteXml.Replace("<input type='button' id='btnPrint' value='" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1505") + "' onClick='printReport();' vAlign='bottom' class='clsButton'>", "<input type='button' id='btnPrint' value='" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1505") + "' onClick='printReport();' vAlign='bottom' class='clsButton'/>");
            quoteXml = quoteXml.Replace("<META", "<!-- <META");
            quoteXml = quoteXml.Replace("rel='stylesheet'>", "rel='stylesheet'>-->");
            quoteXml = quoteXml.Replace("</html>", "");
            quoteXml = quoteXml.Replace("</span>", "</span></html>");

            XmlDocument xmlDocInput = new XmlDocument();
            if (appLobID == "2")
            {
                if (quoteXml.IndexOf("&amp;") > 0)
                {
                    quoteXml = ReplaceChar(quoteXml, '&');
                }
                else
                {
                    if (quoteXml.IndexOf("&") > 0)
                    {
                        quoteXml = quoteXml.Replace("&", "&amp;");
                    }
                }
            }
            xmlDocInput.LoadXml(quoteXml);
            quoteXml = quoteXml.Replace("<!-- <META", "<META");
            quoteXml = quoteXml.Replace("rel='stylesheet'>-->", "rel='stylesheet'>");
            quoteXml = quoteXml.Replace("<META http-equiv='Content-Type' content='text/html; charset=utf-16'>", "<META http-equiv='Content-Type' content='text/html; charset=utf-16'/>");
            quoteXml = quoteXml.Replace("<LINK id='lk' href='/cms/cmsweb/css/css1.css' type='text/css' rel='stylesheet'>", "<LINK id='lk' href='/cms/cmsweb/css/css1.css' type='text/css' rel='stylesheet'/>");
            quoteXml = quoteXml.Replace("<!--br-->", "<br>");
            //quoteXml=quoteXml.Replace("</html>","");
            //quoteXml=quoteXml.Replace("<span","</html><span");

            /* itrack # 1402 
          * Added by Abhinav for multi culture support.convert xml date according to user language.
          */
            if (GetLanguageID() != "" && int.Parse(GetLanguageID()) == 2)
            {
                XmlNode item = xmlDocInput.SelectSingleNode("PRIMIUM/HEADER");
                if (item != null)
                    item.Attributes["QUOTE_DATE"].Value = ConvertDBDateToCulture(item.Attributes["QUOTE_DATE"].Value.ToString());

            }

            //Transform the Input XMl to generate the premium
            XslTransform tr = new XslTransform();
            tr.Load(finalQuoteXSLPath);

            nav = ((IXPathNavigable)xmlDocInput).CreateNavigator();
            StringWriter swReport = new StringWriter();
            tr.Transform(nav, null, swReport);
            // here we will chk for Boat attached to a Home or not 		
            string strPremium = swReport.ToString();
            return strPremium;

        }


        public string GenerateQuickQuote()
        {
            //string _strRatingEngineUrl = ClsCommon.GetKeyValueWithIP("eRate_Webservice_URL");
            //"http://192.168.90.38/eRateEngine/eRateEngine.asmx";
            string _strRatingEngineUrl = System.Configuration.ConfigurationSettings.AppSettings["eRateWebServiceURL"];
            string strShowPremium = "";
            // Saving the values into the database
            string strRequestXml = "";
            if (GetLOBID() == "13")
            {
                strRequestXml = GetMarineRateXML();
            }
            else
            {
                strRequestXml = GeteRateXML();
            }

            

            if (strRequestXml.Trim() == "" || strRequestXml == null)
            {
                strShowPremium = "<html><head><title>Quote</title></head><body><div style='color:Maroon'><table><tr><td>Quote not generated.</td></tr><tr><td>Please fill all the required details, and try again.</td></tr></table></div></body></html>";
                return strShowPremium;
            }
            //strRequestXml = "<Product><ProductCode>AUTO</ProductCode><ProductName>Demo Auto Product</ProductName><SessionActionCode>NewBusiness</SessionActionCode><EndSessionURL>http://115.113.95.22/iClosePortal/CallProduct.aspx?endPolicyRequest=Y</EndSessionURL><MessageStatus><MessageStatusCode>Success</MessageStatusCode><MessageStatusDescription>Test</MessageStatusDescription><ExtendedStatusCode>Test</ExtendedStatusCode><ExtendedStatusDescription /></MessageStatus><PolicyHeader><DefaultLanguageCode>en-AU</DefaultLanguageCode><DefaultCurrencyCode>AUD</DefaultCurrencyCode><IsTestIndicator>false</IsTestIndicator><ContractDetails><ContractStateCode>NewBusiness</ContractStateCode><RevisionStateCode>Original</RevisionStateCode><BindStateCode>New</BindStateCode><IsClosedIndicator>false</IsClosedIndicator><UKI>urn:iclose:contract:bea91c4c-7511-4992-a55e-94a9c2a224d7,1,1</UKI><CloseOnPaymentIndicator>false</CloseOnPaymentIndicator><StartContractActionCode>NewBusiness</StartContractActionCode><StartRevisionActionCode>NewBusiness</StartRevisionActionCode><LapseOnRenewalIndicator>false</LapseOnRenewalIndicator></ContractDetails><PeriodOfInsurance><InceptionDateTime>2011-07-01T00:00:00</InceptionDateTime><EffectiveDateTime>2011-07-01T00:00:00</EffectiveDateTime><ExpiryDateTime>2012-07-01T00:00:00</ExpiryDateTime></PeriodOfInsurance><InsurerProductCode>AUTO</InsurerProductCode><AccountNumberId>TBA</AccountNumberId><PolicyPremiums><FullTermPremiums><AssessmentAmount>0.00</AssessmentAmount><FireServicesLevyAmount>0.00</FireServicesLevyAmount><EarthQuakeLevyAmount>0.00</EarthQuakeLevyAmount><StampDutyAmount>0.00</StampDutyAmount><TaxAmount>0.00</TaxAmount><CommissionAmount>0.00</CommissionAmount><CommissionTaxAmount>0.00</CommissionTaxAmount><CommissionPercent>0.00</CommissionPercent><UnderwritingFeeAmount>0.00</UnderwritingFeeAmount><UnderwritingFeeTaxAmount>0.00</UnderwritingFeeTaxAmount><IntermediaryFeeAmount>0.00</IntermediaryFeeAmount><IntermediaryFeeTaxAmount>0.00</IntermediaryFeeTaxAmount><IntermediaryDiscountAmount>0.00</IntermediaryDiscountAmount><IntermediaryDiscountTaxAmount>0.00</IntermediaryDiscountTaxAmount></FullTermPremiums><CurrentTermPremiums><AssessmentAmount>0.00</AssessmentAmount><FireServicesLevyAmount>0.00</FireServicesLevyAmount><EarthQuakeLevyAmount>0.00</EarthQuakeLevyAmount><StampDutyAmount>0.00</StampDutyAmount><TaxAmount>0.00</TaxAmount><CommissionAmount>0.00</CommissionAmount><CommissionTaxAmount>0.00</CommissionTaxAmount><CommissionPercent>0.00</CommissionPercent><UnderwritingFeeAmount>0.00</UnderwritingFeeAmount><UnderwritingFeeTaxAmount>0.00</UnderwritingFeeTaxAmount><IntermediaryFeeAmount>0.00</IntermediaryFeeAmount><IntermediaryFeeTaxAmount>0.00</IntermediaryFeeTaxAmount><IntermediaryDiscountAmount>0.00</IntermediaryDiscountAmount><IntermediaryDiscountTaxAmount>0.00</IntermediaryDiscountTaxAmount></CurrentTermPremiums><TransactionPremiums><AssessmentAmount>0.00</AssessmentAmount><FireServicesLevyAmount>0.00</FireServicesLevyAmount><EarthQuakeLevyAmount>0.00</EarthQuakeLevyAmount><StampDutyAmount>0.00</StampDutyAmount><TaxAmount>0.00</TaxAmount><CommissionAmount>0.00</CommissionAmount><CommissionTaxAmount>0.00</CommissionTaxAmount><CommissionPercent>0.00</CommissionPercent><UnderwritingFeeAmount>0.00</UnderwritingFeeAmount><UnderwritingFeeTaxAmount>0.00</UnderwritingFeeTaxAmount><IntermediaryFeeAmount>0.00</IntermediaryFeeAmount><IntermediaryFeeTaxAmount>0.00</IntermediaryFeeTaxAmount><IntermediaryDiscountAmount>0.00</IntermediaryDiscountAmount><IntermediaryDiscountTaxAmount>0.00</IntermediaryDiscountTaxAmount></TransactionPremiums></PolicyPremiums><BillingPartyCode>Intermediary</BillingPartyCode><BillingMethodCode>FullAmount</BillingMethodCode><CreatedDateTime>2011-07-01T18:33:51.1858926+05:30</CreatedDateTime><CreatedBy /><LastSavedDateTime>2011-07-01T18:33:51.1858926+05:30</LastSavedDateTime><LastSavedBy /></PolicyHeader><LOBs><LOB><LOBCode>Vehicle</LOBCode><LOBID>6</LOBID><LOBVersion>1</LOBVersion><LOBName>Vehicle Auto Product</LOBName><CoverCode>DEF</CoverCode><PolicyPremiums><FullTermPremiums>
            
            string strResponseXml = "";
            Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQQDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();

            objQQDetails.UpdateRateXML(customerID, PolicyID, PolicyVersionID, quoteId, strRequestXml, "");

            XmlDataDocument objInputXml = new XmlDataDocument();
            objInputXml.LoadXml(strRequestXml);

            objQuoteRequestType = new QuoteRequestType();
            objQuoteResponseType = new QuoteResponseType();
            objeRateEngine = new eRateEngine(_strRatingEngineUrl);
            objQuoteRequestType.CallOuterWebservice = false;
            objQuoteRequestType.CreateOutputXMLFile = false;
            objQuoteRequestType.GetInputXMLFromPath = false;
            objQuoteRequestType.CallerApplication = "ICLOSE";
            objQuoteRequestType.InputFilePath = "";
            objQuoteRequestType.InputXMLOBJ = objInputXml;

            //---Second Phase

            objQuoteResponseType = objeRateEngine.GetQuote(objQuoteRequestType);
            if (objQuoteResponseType.ReturnStatus == "100")
            {
                XmlDataDocument objOutputXml = new XmlDataDocument();
                objOutputXml.LoadXml(objQuoteResponseType.OutputXMLOBJ.OuterXml);

                //strResponseXml = objOutputXml.OuterXml;//  xmlns="urn:iclose:schema:1_0_0"

                /*
                */
                strResponseXml = System.Text.RegularExpressions.Regex.Replace(objOutputXml.OuterXml, @"( xmlns:?[^=]*=[""][^""]*[""])", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);

                 

                //SAVE OUTPUT XML
                //objOutputXml.Save(_strSessionDirPath + "\\04_Product.xml");
                objQQDetails.UpdateRateXML(customerID, PolicyID, PolicyVersionID, quoteId, "", strResponseXml);                
                //------------------------------------------
            }
            else
            {
                //lblMessage.Visible = true;
                //lblMessage.Text = "Error generating rate.Following error occured: " + objQuoteResponseType.ReturnMessage;
                Response.Write("<script language='javascript'> alert('Error in generating Quote: " + objQuoteResponseType.ReturnMessage + "'); </script>");
            }

            if (strResponseXml == "" || strResponseXml == null)
            {
                //donothing
            }
            string strBasePremium, strDemeritDisc, strGSTAmount; 
            string inputXML = "";
            if (strResponseXml != "")
            {
                if (GetLOBID() != "13")
                {
                    
                    string strPremium = GetPremiumFromXml(strResponseXml, out strBasePremium, out strGSTAmount, out strDemeritDisc);
                    objQQDetails.UpdateQQPremium(customerID, PolicyID, PolicyVersionID, strBasePremium);
                   
                    if (!strCalledFrom.ToUpper().ToString().Equals("QQ") && !strCalledFrom.ToUpper().ToString().Equals("QAPP") && strCalledFrom.ToUpper().ToString() != "")
                    {
                        objQQDetails.UpdateQuoteDetailsPremiumNonQQ(customerID, PolicyID, PolicyVersionID, quoteId, strBasePremium, strDemeritDisc, strGSTAmount, strPremium);
                        inputXML = objQQDetails.GetQuoteXMLNonQQ(customerID, PolicyID, PolicyVersionID);

                    }
                    else
                    {
                        objQQDetails.UpdateQuoteDetailsPremium(customerID, PolicyID, PolicyVersionID, quoteId, strBasePremium, strDemeritDisc, strGSTAmount, strPremium);
                        inputXML = objQQDetails.GetQuickQuoteXML(customerID, PolicyID, PolicyVersionID);
                    }
                }
                else
                {
                    string strSumInsured, strTotalPremium;
                    string strPremium = GetPremiumFromXmlMarine(strResponseXml, out strSumInsured, out strTotalPremium);
                    objQQDetails.UpdateQQPremium(customerID, PolicyID, PolicyVersionID, strPremium);
                    objQQDetails.UpdateQuoteDetailsPremiumForMarine(customerID, PolicyID, PolicyVersionID, quoteId, double.Parse(strSumInsured), double.Parse(strTotalPremium));
                    inputXML = objQQDetails.GetQuickQuoteXMLForMarine(customerID, PolicyID, PolicyVersionID);
                }

                //string cssnum = "";
                //cssnum = "<HEADER" + " CSSNUM=\"" + GetColorScheme() + "\"";
                //inputXML = inputXML.Replace("<HEADER", cssnum);
                //inputXML = inputXML.Replace("&gt;", ">");
                //inputXML = inputXML.Replace("&lt;", "<");
                //inputXML = inputXML.Replace("\"", "'");
                //inputXML = inputXML.Replace("\r", "");
                //inputXML = inputXML.Replace("\n", "");
                //inputXML = inputXML.Replace("\t", "");
                //inputXML = inputXML.Replace("<br>", "<!--br-->");              
                //inputXML = inputXML.Replace("<input type='button' id='btnPrint' value='" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1505") + "' onClick='printReport();' vAlign='bottom' class='clsButton'>", "<input type='button' id='btnPrint' value='" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1505") + "' onClick='printReport();' vAlign='bottom' class='clsButton'/>");
                //inputXML = inputXML.Replace("<META", "<!-- <META");
                //inputXML = inputXML.Replace("rel='stylesheet'>", "rel='stylesheet'>-->");
                //inputXML = inputXML.Replace("</html>", "");
                //inputXML = inputXML.Replace("</span>", "</span></html>");
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(inputXML);

                string finalQuoteXSLPath = "";
                if (GetLOBID() != "13")
                {
                   finalQuoteXSLPath= ClsCommon.GetKeyValueWithIP("FilePathQQRatingXML");
                }
                else
                {
                    finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FilePathQQRatingXMLMarine");
                }
                XslTransform tr = new XslTransform();
                tr.Load(finalQuoteXSLPath);

                XPathNavigator nav = ((IXPathNavigable)xDoc).CreateNavigator();
                StringWriter swReport = new StringWriter();
                tr.Transform(nav, null, swReport);

                string strPremiumXml = swReport.ToString();
                //return strPremiumXml;

                strPremiumXml = strPremiumXml.Replace("&gt;", ">");
                strPremiumXml = strPremiumXml.Replace("&lt;", "<");
                strPremiumXml = strPremiumXml.Replace("\"", "'");
                strPremiumXml = strPremiumXml.Replace("\r", "");
                strPremiumXml = strPremiumXml.Replace("\n", "");
                strPremiumXml = strPremiumXml.Replace("\t", "");

                strShowPremium = strPremiumXml;

                //Response.Redirect("/cms/application/Aspx/Quote/Quote.aspx?CUSTOMER_ID=" + customerID + "&POLICY_ID=" + PolicyID + "&POLICY_VERSION_ID=" + PolicyVersionID + "&LOBID=" + GetLOBID() + "&QUOTE_ID=" + quoteId + "&CALLEDFROM=QQUOTE" + "&SHOWRATE=" + strPremiumXml);
                //Response.Redirect("/cms/application/Aspx/Quote/Quote.aspx?CUSTOMER_ID=" + lIntCUSTOMER_ID + "&APP_ID=" + lIntAPP_ID + "&APP_VERSION_ID=" + lIntAPP_VERSION_ID + "&LOBID=" + lStrLobID + "&QUOTE_ID=" + lIntQuoteID + "&SHOW=" + lIntShowQuote);
            }

            return strShowPremium;
        }

        private string GetQuoteRateXML()
        {
            Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQuoteDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();
            DataSet dsRate = objQuoteDetails.FetchDataForQQRqXml(customerID, PolicyID, PolicyVersionID);

            DataTable dtRate = dsRate.Tables[0];

            if (dtRate.Rows.Count != 0)
            {
                XmlDocument doc = new XmlDocument();
                //XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                //doc.AppendChild(docNode);

                XmlNode productsNode = doc.CreateElement("Product");
                doc.AppendChild(productsNode);

                XmlNode productCode = doc.CreateElement("ProductCode");
                productsNode.AppendChild(productCode);
                productCode.InnerText = "AUTO";

                XmlNode productname = doc.CreateElement("ProductName");
                productsNode.AppendChild(productname);
                productname.InnerText = "Auto Product";

                XmlNode sessionName = doc.CreateElement("SessionActionCode");
                productsNode.AppendChild(sessionName);
                sessionName.InnerText = "NewBusiness";

                XmlNode sessionURL = doc.CreateElement("EndSessionURL");
                productsNode.AppendChild(sessionURL);
                sessionURL.InnerText = "http://115.113.95.22/iClosePortal/CallProduct.aspx?endPolicyRequest=Y";

                XmlNode messageStatus = doc.CreateElement("MessageStatus");
                productsNode.AppendChild(messageStatus);
                //messageStatus.InnerText = "";

                XmlNode messageCode = doc.CreateElement("MessageStatusCode");
                messageStatus.AppendChild(messageCode);
                messageCode.InnerText = "";



                XmlNode messageDesc = doc.CreateElement("MessageStatusDescription");
                messageStatus.AppendChild(messageDesc);

                XmlNode msgExCode = doc.CreateElement("ExtendedStatusCode");
                messageStatus.AppendChild(msgExCode);

                XmlNode msgStatusDesc = doc.CreateElement("ExtendedStatusDescription");
                messageStatus.AppendChild(msgStatusDesc);

                XmlNode policyHeader = doc.CreateElement("PolicyHeader");
                productsNode.AppendChild(policyHeader);

                XmlNode languageCode = doc.CreateElement("DefaultLanguageCode");
                policyHeader.AppendChild(languageCode);
                languageCode.InnerText = "en-US";

                XmlNode currencyCode = doc.CreateElement("DefaultCurrencyCode");
                policyHeader.AppendChild(currencyCode);
                currencyCode.InnerText = "USD";

                XmlNode indicator = doc.CreateElement("IsTestIndicator");
                policyHeader.AppendChild(indicator);

                XmlNode contractDetails = doc.CreateElement("ContractDetails");
                policyHeader.AppendChild(contractDetails);

                XmlNode cStateCode = doc.CreateElement("ContractStateCode");
                contractDetails.AppendChild(cStateCode);
                cStateCode.InnerText = "NewBusiness";

                XmlNode rStateCode = doc.CreateElement("RevisionStateCode");
                contractDetails.AppendChild(rStateCode);

                XmlNode bStateCode = doc.CreateElement("BindStateCode");
                contractDetails.AppendChild(bStateCode);
                bStateCode.InnerText = "New";

                XmlNode closeIndicator = doc.CreateElement("IsClosedIndicator");
                contractDetails.AppendChild(closeIndicator);

                XmlNode uki = doc.CreateElement("UKI");
                contractDetails.AppendChild(uki);

                XmlNode cPaymentInd = doc.CreateElement("CloseOnPaymentIndicator");
                contractDetails.AppendChild(cPaymentInd);

                XmlNode cActionCode = doc.CreateElement("StartContractActionCode");
                contractDetails.AppendChild(cActionCode);

                XmlNode rActionCode = doc.CreateElement("StartRevisionActionCode");
                contractDetails.AppendChild(rActionCode);

                XmlNode renewInd = doc.CreateElement("LapseOnRenewalIndicator");
                contractDetails.AppendChild(renewInd);

                XmlNode insuranceTerm = doc.CreateElement("PeriodOfInsurance");
                policyHeader.AppendChild(insuranceTerm);

                XmlNode inceptionDate = doc.CreateElement("InceptionDateTime");
                insuranceTerm.AppendChild(inceptionDate); // To be filled from DB

                if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                    inceptionDate.InnerText = String.Format("{0:s}", dt); 
                }

                XmlNode effectiveDate = doc.CreateElement("EffectiveDateTime");
                insuranceTerm.AppendChild(effectiveDate); // To be filled from DB

                if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    //effectiveDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString()).ToShortDateString();
                    DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                    effectiveDate.InnerText = String.Format("{0:s}", dt); 
                }

                XmlNode expiryDate = doc.CreateElement("ExpiryDateTime");
                insuranceTerm.AppendChild(expiryDate); // To be filled from DB

                if (dtRate.Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
                {
                    //expiryDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString()).ToShortDateString();
                    DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString());
                    expiryDate.InnerText = String.Format("{0:s}", dt); 
                }

                XmlNode insproductCode = doc.CreateElement("InsurerProductCode");
                policyHeader.AppendChild(insproductCode);
                insproductCode.InnerText = "AUTO";

                XmlNode accNumId = doc.CreateElement("AccountNumberId");
                policyHeader.AppendChild(accNumId);

                XmlNode policyPremium1 = doc.CreateElement("PolicyPremiums");
                policyHeader.AppendChild(policyPremium1);

                XmlNode fullTermP1 = doc.CreateElement("FullTermPremiums");
                policyPremium1.AppendChild(fullTermP1);

                //
                XmlNode assessAmtFTP1 = doc.CreateElement("AssessmentAmount");
                fullTermP1.AppendChild(assessAmtFTP1);
                assessAmtFTP1.InnerText = "0.00";

                XmlNode fireServAmtFTP1 = doc.CreateElement("FireServicesLevyAmount");
                fullTermP1.AppendChild(fireServAmtFTP1);
                fireServAmtFTP1.InnerText = "0.00";

                XmlNode earthQkAmtFTP1 = doc.CreateElement("EarthQuakeLevyAmount");
                fullTermP1.AppendChild(earthQkAmtFTP1);
                earthQkAmtFTP1.InnerText = "0.00";

                XmlNode stampAmtFTP1 = doc.CreateElement("StampDutyAmount");
                fullTermP1.AppendChild(stampAmtFTP1);
                stampAmtFTP1.InnerText = "0.00";

                XmlNode taxAmtFTP1 = doc.CreateElement("TaxAmount");
                fullTermP1.AppendChild(taxAmtFTP1);
                stampAmtFTP1.InnerText = "0.00";

                XmlNode commAmtFTP1 = doc.CreateElement("CommissionAmount");
                fullTermP1.AppendChild(commAmtFTP1);
                commAmtFTP1.InnerText = "0.00";

                XmlNode commTaxAmtFTP1 = doc.CreateElement("CommissionTaxAmount");
                fullTermP1.AppendChild(commTaxAmtFTP1);
                commTaxAmtFTP1.InnerText = "0.00";

                XmlNode commPctFTP1 = doc.CreateElement("CommissionPercent");
                fullTermP1.AppendChild(commPctFTP1);
                commPctFTP1.InnerText = "0.00";

                XmlNode uwFeeAmtFTP1 = doc.CreateElement("UnderwritingFeeAmount");
                fullTermP1.AppendChild(uwFeeAmtFTP1);
                uwFeeAmtFTP1.InnerText = "0.00";

                XmlNode uwFeetaxAmtFTP1 = doc.CreateElement("UnderwritingFeeTaxAmount");
                fullTermP1.AppendChild(uwFeetaxAmtFTP1);
                uwFeetaxAmtFTP1.InnerText = "0.00";

                XmlNode interFeeAmtFTP1 = doc.CreateElement("IntermediaryFeeAmount");
                fullTermP1.AppendChild(interFeeAmtFTP1);
                interFeeAmtFTP1.InnerText = "0.00";

                XmlNode interFeeTaxAmtFTP1 = doc.CreateElement("IntermediaryFeeTaxAmount");
                fullTermP1.AppendChild(interFeeTaxAmtFTP1);
                interFeeTaxAmtFTP1.InnerText = "0.00";

                XmlNode interDiscAmtFTP1 = doc.CreateElement("IntermediaryDiscountAmount");
                fullTermP1.AppendChild(interDiscAmtFTP1);
                interDiscAmtFTP1.InnerText = "0.00";

                XmlNode interDiscTaxAmtFTP1 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                fullTermP1.AppendChild(interDiscTaxAmtFTP1);
                interDiscTaxAmtFTP1.InnerText = "0.00";
                //

                XmlNode currTermP1 = doc.CreateElement("CurrentTermPremiums");
                policyPremium1.AppendChild(currTermP1);
                //
                XmlNode assessAmtCTP1 = doc.CreateElement("AssessmentAmount");
                currTermP1.AppendChild(assessAmtCTP1);
                assessAmtCTP1.InnerText = "0.00";

                XmlNode fireServAmtCTP1 = doc.CreateElement("FireServicesLevyAmount");
                currTermP1.AppendChild(fireServAmtCTP1);
                fireServAmtCTP1.InnerText = "0.00";

                XmlNode earthQkAmtCTP1 = doc.CreateElement("EarthQuakeLevyAmount");
                currTermP1.AppendChild(earthQkAmtCTP1);
                earthQkAmtCTP1.InnerText = "0.00";

                XmlNode stampAmtCTP1 = doc.CreateElement("StampDutyAmount");
                currTermP1.AppendChild(stampAmtCTP1);
                stampAmtCTP1.InnerText = "0.00";

                XmlNode taxAmtCTP1 = doc.CreateElement("TaxAmount");
                currTermP1.AppendChild(taxAmtCTP1);
                taxAmtCTP1.InnerText = "0.00";

                XmlNode commAmtCTP1 = doc.CreateElement("CommissionAmount");
                currTermP1.AppendChild(commAmtCTP1);
                commAmtCTP1.InnerText = "0.00";

                XmlNode commTaxAmtCTP1 = doc.CreateElement("CommissionTaxAmount");
                currTermP1.AppendChild(commTaxAmtCTP1);
                commTaxAmtCTP1.InnerText = "0.00";

                XmlNode commPctCTP1 = doc.CreateElement("CommissionPercent");
                currTermP1.AppendChild(commPctCTP1);
                commPctCTP1.InnerText = "0.00";

                XmlNode uwFeeAmtCTP1 = doc.CreateElement("UnderwritingFeeAmount");
                currTermP1.AppendChild(uwFeeAmtCTP1);
                uwFeeAmtCTP1.InnerText = "0.00";

                XmlNode uwFeetaxAmtCTP1 = doc.CreateElement("UnderwritingFeeTaxAmount");
                currTermP1.AppendChild(uwFeetaxAmtCTP1);
                uwFeetaxAmtCTP1.InnerText = "0.00";

                XmlNode interFeeAmtCTP1 = doc.CreateElement("IntermediaryFeeAmount");
                currTermP1.AppendChild(interFeeAmtCTP1);
                interFeeAmtCTP1.InnerText = "0.00";

                XmlNode interFeeTaxAmtCTP1 = doc.CreateElement("IntermediaryFeeTaxAmount");
                currTermP1.AppendChild(interFeeTaxAmtCTP1);
                interFeeTaxAmtCTP1.InnerText = "0.00";

                XmlNode interDiscAmtCTP1 = doc.CreateElement("IntermediaryDiscountAmount");
                currTermP1.AppendChild(interDiscAmtCTP1);
                interDiscAmtCTP1.InnerText = "0.00";

                XmlNode interDiscTaxAmtCTP1 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                currTermP1.AppendChild(interDiscTaxAmtCTP1);
                interDiscTaxAmtCTP1.InnerText = "0.00";
                //

                XmlNode TransP1 = doc.CreateElement("TransactionPremiums");
                policyPremium1.AppendChild(TransP1);

                //
                XmlNode assessAmtTP1 = doc.CreateElement("AssessmentAmount");
                TransP1.AppendChild(assessAmtTP1);
                assessAmtTP1.InnerText = "0.00";

                XmlNode fireServAmtTP1 = doc.CreateElement("FireServicesLevyAmount");
                TransP1.AppendChild(fireServAmtTP1);
                fireServAmtTP1.InnerText = "0.00";

                XmlNode earthQkAmtTP1 = doc.CreateElement("EarthQuakeLevyAmount");
                TransP1.AppendChild(earthQkAmtTP1);
                earthQkAmtTP1.InnerText = "0.00";

                XmlNode stampAmtTP1 = doc.CreateElement("StampDutyAmount");
                TransP1.AppendChild(stampAmtTP1);
                stampAmtTP1.InnerText = "0.00";

                XmlNode taxAmtTP1 = doc.CreateElement("TaxAmount");
                TransP1.AppendChild(taxAmtTP1);
                taxAmtTP1.InnerText = "0.00";

                XmlNode commAmtTP1 = doc.CreateElement("CommissionAmount");
                TransP1.AppendChild(commAmtTP1);
                commAmtTP1.InnerText = "0.00";

                XmlNode commTaxAmtTP1 = doc.CreateElement("CommissionTaxAmount");
                TransP1.AppendChild(commTaxAmtTP1);
                commTaxAmtTP1.InnerText = "0.00";

                XmlNode commPctTP1 = doc.CreateElement("CommissionPercent");
                TransP1.AppendChild(commPctTP1);
                commPctTP1.InnerText = "0.00";

                XmlNode uwFeeAmtTP1 = doc.CreateElement("UnderwritingFeeAmount");
                TransP1.AppendChild(uwFeeAmtTP1);
                uwFeeAmtTP1.InnerText = "0.00";

                XmlNode uwFeetaxAmtTP1 = doc.CreateElement("UnderwritingFeeTaxAmount");
                TransP1.AppendChild(uwFeetaxAmtTP1);
                uwFeetaxAmtTP1.InnerText = "0.00";

                XmlNode interFeeAmtTP1 = doc.CreateElement("IntermediaryFeeAmount");
                TransP1.AppendChild(interFeeAmtTP1);
                interFeeAmtTP1.InnerText = "0.00";

                XmlNode interFeeTaxAmtTP1 = doc.CreateElement("IntermediaryFeeTaxAmount");
                TransP1.AppendChild(interFeeTaxAmtTP1);
                interFeeTaxAmtTP1.InnerText = "0.00";

                XmlNode interDiscAmtTP1 = doc.CreateElement("IntermediaryDiscountAmount");
                TransP1.AppendChild(interDiscAmtTP1);
                interDiscAmtTP1.InnerText = "0.00";

                XmlNode interDiscTaxAmtTP1 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                TransP1.AppendChild(interDiscTaxAmtTP1);
                interDiscTaxAmtTP1.InnerText = "0.00";
                //

                XmlNode billpartyCode = doc.CreateElement("BillingPartyCode");
                policyHeader.AppendChild(billpartyCode);

                XmlNode billMethodCode = doc.CreateElement("BillingMethodCode");
                policyHeader.AppendChild(billMethodCode);

                XmlNode createDate = doc.CreateElement("CreatedDateTime");
                policyHeader.AppendChild(createDate);

                XmlNode createdBy = doc.CreateElement("CreatedBy");
                policyHeader.AppendChild(createdBy);

                XmlNode lastSaveDate = doc.CreateElement("LastSavedDateTime");
                policyHeader.AppendChild(lastSaveDate);

                XmlNode lastSavedBy = doc.CreateElement("LastSavedBy");
                policyHeader.AppendChild(lastSavedBy);

                XmlNode nodeLOBs = doc.CreateElement("LOBs");
                productsNode.AppendChild(nodeLOBs);

                XmlNode nodeLOB = doc.CreateElement("LOB");
                nodeLOBs.AppendChild(nodeLOB);

                XmlNode nodeLOBCode = doc.CreateElement("LOBCode");
                nodeLOB.AppendChild(nodeLOBCode);
                nodeLOBCode.InnerText = "Vehicle";

                XmlNode nodeLOBId = doc.CreateElement("LOBID");
                nodeLOB.AppendChild(nodeLOBId);
                nodeLOBId.InnerText = "6";

                XmlNode nodeLOBVersion = doc.CreateElement("LOBVersion");
                nodeLOB.AppendChild(nodeLOBVersion);

                XmlNode nodeLOBName = doc.CreateElement("LOBName");
                nodeLOB.AppendChild(nodeLOBName);
                nodeLOBName.InnerText = "Vehicle Auto Product";

                XmlNode nodeLOBCoverCode = doc.CreateElement("CoverCode");
                nodeLOB.AppendChild(nodeLOBCoverCode);


                XmlNode policyPremium2 = doc.CreateElement("PolicyPremiums");
                nodeLOB.AppendChild(policyPremium2);

                XmlNode fullTermP2 = doc.CreateElement("FullTermPremiums");
                policyPremium2.AppendChild(fullTermP2);

                //
                XmlNode assessAmtFTP2 = doc.CreateElement("AssessmentAmount");
                fullTermP2.AppendChild(assessAmtFTP2);
                assessAmtFTP2.InnerText = "0.00";

                XmlNode fireServAmtFTP2 = doc.CreateElement("FireServicesLevyAmount");
                fullTermP2.AppendChild(fireServAmtFTP2);
                fireServAmtFTP2.InnerText = "0.00";

                XmlNode earthQkAmtFTP2 = doc.CreateElement("EarthQuakeLevyAmount");
                fullTermP2.AppendChild(earthQkAmtFTP2);
                earthQkAmtFTP2.InnerText = "0.00";

                XmlNode stampAmtFTP2 = doc.CreateElement("StampDutyAmount");
                fullTermP2.AppendChild(stampAmtFTP2);
                stampAmtFTP2.InnerText = "0.00";

                XmlNode taxAmtFTP2 = doc.CreateElement("TaxAmount");
                fullTermP2.AppendChild(taxAmtFTP2);
                taxAmtFTP2.InnerText = "0.00";

                XmlNode commAmtFTP2 = doc.CreateElement("CommissionAmount");
                fullTermP2.AppendChild(commAmtFTP2);
                commAmtFTP2.InnerText = "0.00";

                XmlNode commTaxAmtFTP2 = doc.CreateElement("CommissionTaxAmount");
                fullTermP2.AppendChild(commTaxAmtFTP2);
                commTaxAmtFTP2.InnerText = "0.00";

                XmlNode commPctFTP2 = doc.CreateElement("CommissionPercent");
                fullTermP2.AppendChild(commPctFTP2);
                commPctFTP2.InnerText = "0.00";

                XmlNode uwFeeAmtFTP2 = doc.CreateElement("UnderwritingFeeAmount");
                fullTermP2.AppendChild(uwFeeAmtFTP2);
                uwFeeAmtFTP2.InnerText = "0.00";

                XmlNode uwFeetaxAmtFTP2 = doc.CreateElement("UnderwritingFeeTaxAmount");
                fullTermP2.AppendChild(uwFeetaxAmtFTP2);
                uwFeetaxAmtFTP2.InnerText = "0.00";

                XmlNode interFeeAmtFTP2 = doc.CreateElement("IntermediaryFeeAmount");
                fullTermP2.AppendChild(interFeeAmtFTP2);
                interFeeAmtFTP2.InnerText = "0.00";

                XmlNode interFeeTaxAmtFTP2 = doc.CreateElement("IntermediaryFeeTaxAmount");
                fullTermP2.AppendChild(interFeeTaxAmtFTP2);
                interFeeTaxAmtFTP2.InnerText = "0.00";

                XmlNode interDiscAmtFTP2 = doc.CreateElement("IntermediaryDiscountAmount");
                fullTermP2.AppendChild(interDiscAmtFTP2);
                interDiscAmtFTP2.InnerText = "0.00";

                XmlNode interDiscTaxAmtFTP2 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                fullTermP2.AppendChild(interDiscTaxAmtFTP2);
                interDiscTaxAmtFTP2.InnerText = "0.00";
                //

                XmlNode currTermP2 = doc.CreateElement("CurrentTermPremiums");
                policyPremium2.AppendChild(currTermP2);
                //
                XmlNode assessAmtCTP2 = doc.CreateElement("AssessmentAmount");
                currTermP2.AppendChild(assessAmtCTP2);
                assessAmtCTP2.InnerText = "0.00";

                XmlNode fireServAmtCTP2 = doc.CreateElement("FireServicesLevyAmount");
                currTermP2.AppendChild(fireServAmtCTP2);
                fireServAmtCTP2.InnerText = "0.00";

                XmlNode earthQkAmtCTP2 = doc.CreateElement("EarthQuakeLevyAmount");
                currTermP2.AppendChild(earthQkAmtCTP2);
                earthQkAmtCTP2.InnerText = "0.00";

                XmlNode stampAmtCTP2 = doc.CreateElement("StampDutyAmount");
                currTermP2.AppendChild(stampAmtCTP2);
                stampAmtCTP2.InnerText = "0.00";

                XmlNode taxAmtCTP2 = doc.CreateElement("TaxAmount");
                currTermP2.AppendChild(taxAmtCTP2);
                taxAmtCTP2.InnerText = "0.00";

                XmlNode commAmtCTP2 = doc.CreateElement("CommissionAmount");
                currTermP2.AppendChild(commAmtCTP2);
                commAmtCTP2.InnerText = "0.00";

                XmlNode commTaxAmtCTP2 = doc.CreateElement("CommissionTaxAmount");
                currTermP2.AppendChild(commTaxAmtCTP2);
                commTaxAmtCTP2.InnerText = "0.00";

                XmlNode commPctCTP2 = doc.CreateElement("CommissionPercent");
                currTermP2.AppendChild(commPctCTP2);
                commPctCTP2.InnerText = "0.00";

                XmlNode uwFeeAmtCTP2 = doc.CreateElement("UnderwritingFeeAmount");
                currTermP2.AppendChild(uwFeeAmtCTP2);
                uwFeeAmtCTP2.InnerText = "0.00";

                XmlNode uwFeetaxAmtCTP2 = doc.CreateElement("UnderwritingFeeTaxAmount");
                currTermP2.AppendChild(uwFeetaxAmtCTP2);
                uwFeetaxAmtCTP2.InnerText = "0.00";

                XmlNode interFeeAmtCTP2 = doc.CreateElement("IntermediaryFeeAmount");
                currTermP2.AppendChild(interFeeAmtCTP2);
                interFeeAmtCTP2.InnerText = "0.00";

                XmlNode interFeeTaxAmtCTP2 = doc.CreateElement("IntermediaryFeeTaxAmount");
                currTermP2.AppendChild(interFeeTaxAmtCTP2);
                interFeeTaxAmtCTP2.InnerText = "0.00";

                XmlNode interDiscAmtCTP2 = doc.CreateElement("IntermediaryDiscountAmount");
                currTermP2.AppendChild(interDiscAmtCTP2);
                interDiscAmtCTP2.InnerText = "0.00";

                XmlNode interDiscTaxAmtCTP2 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                currTermP2.AppendChild(interDiscTaxAmtCTP2);
                interDiscTaxAmtCTP2.InnerText = "0.00";
                //

                XmlNode TransP2 = doc.CreateElement("TransactionPremiums");
                policyPremium2.AppendChild(TransP2);

                //
                XmlNode assessAmtTP2 = doc.CreateElement("AssessmentAmount");
                TransP2.AppendChild(assessAmtTP2);
                assessAmtTP2.InnerText = "0.00";

                XmlNode fireServAmtTP2 = doc.CreateElement("FireServicesLevyAmount");
                TransP2.AppendChild(fireServAmtTP2);
                fireServAmtTP2.InnerText = "0.00";

                XmlNode earthQkAmtTP2 = doc.CreateElement("EarthQuakeLevyAmount");
                TransP2.AppendChild(earthQkAmtTP2);
                earthQkAmtTP2.InnerText = "0.00";

                XmlNode stampAmtTP2 = doc.CreateElement("StampDutyAmount");
                TransP2.AppendChild(stampAmtTP2);
                stampAmtTP2.InnerText = "0.00";

                XmlNode taxAmtTP2 = doc.CreateElement("TaxAmount");
                TransP2.AppendChild(taxAmtTP2);
                taxAmtTP2.InnerText = "0.00";

                XmlNode commAmtTP2 = doc.CreateElement("CommissionAmount");
                TransP2.AppendChild(commAmtTP2);
                commAmtTP2.InnerText = "0.00";

                XmlNode commTaxAmtTP2 = doc.CreateElement("CommissionTaxAmount");
                TransP2.AppendChild(commTaxAmtTP2);
                commTaxAmtTP2.InnerText = "0.00";

                XmlNode commPctTP2 = doc.CreateElement("CommissionPercent");
                TransP2.AppendChild(commPctTP2);
                commPctTP2.InnerText = "0.00";

                XmlNode uwFeeAmtTP2 = doc.CreateElement("UnderwritingFeeAmount");
                TransP2.AppendChild(uwFeeAmtTP2);
                uwFeeAmtTP2.InnerText = "0.00";

                XmlNode uwFeetaxAmtTP2 = doc.CreateElement("UnderwritingFeeTaxAmount");
                TransP2.AppendChild(uwFeetaxAmtTP2);
                uwFeetaxAmtTP2.InnerText = "0.00";

                XmlNode interFeeAmtTP2 = doc.CreateElement("IntermediaryFeeAmount");
                TransP2.AppendChild(interFeeAmtTP2);
                interFeeAmtTP2.InnerText = "0.00";

                XmlNode interFeeTaxAmtTP2 = doc.CreateElement("IntermediaryFeeTaxAmount");
                TransP2.AppendChild(interFeeTaxAmtTP2);
                interFeeTaxAmtTP2.InnerText = "0.00";

                XmlNode interDiscAmtTP2 = doc.CreateElement("IntermediaryDiscountAmount");
                TransP2.AppendChild(interDiscAmtTP2);
                interDiscAmtTP2.InnerText = "0.00";

                XmlNode interDiscTaxAmtTP2 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                TransP2.AppendChild(interDiscTaxAmtTP2);
                interDiscTaxAmtTP2.InnerText = "0.00";
                //

                XmlNode productRiskData = doc.CreateElement("ProductRiskData");
                nodeLOB.AppendChild(productRiskData);

                XmlNode nodeQQInfo = doc.CreateElement("QuickQuoteInformation");
                productRiskData.AppendChild(nodeQQInfo);

                XmlNode nodePolLOB = doc.CreateElement("POLICY_LOB");
                nodeQQInfo.AppendChild(nodePolLOB);

                if (dtRate.Rows[0]["POLICY_LOB"] != System.DBNull.Value)
                {
                    int LOB_UID = int.Parse(dtRate.Rows[0]["POLICY_LOB"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(LOB_UID);
                        nodePolLOB.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }

                }

                XmlNode nodePOLCurr = doc.CreateElement("PolicyCurrencydropdown");
                nodeQQInfo.AppendChild(nodePOLCurr);

                if (dtRate.Rows[0]["POLICY_CURRENCY"] != System.DBNull.Value)
                {
                    int POL_CURR = int.Parse(dtRate.Rows[0]["POLICY_CURRENCY"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(POL_CURR);
                        nodePOLCurr.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeTerm = doc.CreateElement("APP_TERMS");
                nodeQQInfo.AppendChild(nodeTerm);

                if (dtRate.Rows[0]["APP_TERMS"] != System.DBNull.Value)
                {
                    nodeTerm.InnerText = dtRate.Rows[0]["APP_TERMS"].ToString() + " " + "Months";
                }

                XmlNode nodeEffDate = doc.CreateElement("APP_EFFECTIVE_DATE");
                nodeQQInfo.AppendChild(nodeEffDate);
                if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    nodeEffDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString()).ToShortDateString();
                }

                XmlNode nodeExpDate = doc.CreateElement("APP_EXPIRATION_DATE");
                nodeQQInfo.AppendChild(nodeExpDate);
                if (dtRate.Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
                {
                    nodeExpDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString()).ToShortDateString();
                }

                XmlNode nodeBillType = doc.CreateElement("BILL_TYPE_ID");
                nodeQQInfo.AppendChild(nodeBillType);
                if (dtRate.Rows[0]["BILL_TYPE"] != System.DBNull.Value)
                {
                    string BillCode = dtRate.Rows[0]["BILL_TYPE"].ToString();
                    int BILL_UID = 0;
                    switch (BillCode)
                    {
                        case "AB":
                            BILL_UID = 8459;
                            break;
                        case "DB":
                            BILL_UID = 8460;
                            break;
                        case "DM":
                            BILL_UID = 11150;
                            break;
                        case "DI":
                            BILL_UID = 11191;
                            break;
                        case "MB":
                            BILL_UID = 11276;
                            break;
                        case "AM":
                            BILL_UID = 11277;
                            break;
                        case "IM":
                            BILL_UID = 11278;
                            break;
                        default:
                            BILL_UID = 8459;
                            break;
                    }
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(BILL_UID);
                        nodeBillType.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeInstallType = doc.CreateElement("INSTALL_PLAN_ID");
                nodeQQInfo.AppendChild(nodeInstallType);
                if (dtRate.Rows[0]["INSTALL_PLAN_ID"] != System.DBNull.Value)
                {
                    //nodeInstallType.InnerText = dtRate.Rows[0]["INSTALL_PLAN_ID"].ToString();
                    nodeInstallType.InnerText = "Full Pay Plan (FULLPAY)-Check";
                }

                XmlNode nodeMultiple = doc.CreateElement("MULTIPLIER");
                nodeQQInfo.AppendChild(nodeMultiple);
                nodeMultiple.InnerText = "100";

                XmlNode nodeClientCode = doc.CreateElement("eProfessionalClientCode");
                nodeQQInfo.AppendChild(nodeClientCode);

                XmlNode nodeCustomerType = doc.CreateElement("CUSTOMER_TYPE");
                nodeQQInfo.AppendChild(nodeCustomerType);
                if (dtRate.Rows[0]["CUSTOMER_TYPE"] != System.DBNull.Value)
                {
                    int custTypeID = int.Parse(dtRate.Rows[0]["CUSTOMER_TYPE"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(custTypeID);
                        nodeCustomerType.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeFName = doc.CreateElement("FirstName");
                nodeQQInfo.AppendChild(nodeFName);
                if (dtRate.Rows[0]["CUSTOMER_FIRST_NAME"] != System.DBNull.Value)
                {
                    nodeFName.InnerText = dtRate.Rows[0]["CUSTOMER_FIRST_NAME"].ToString();
                }

                XmlNode nodeMName = doc.CreateElement("MiddleName");
                nodeQQInfo.AppendChild(nodeMName);
                if (dtRate.Rows[0]["CUSTOMER_MIDDLE_NAME"] != System.DBNull.Value)
                {
                    nodeMName.InnerText = dtRate.Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString();
                }

                XmlNode nodeLName = doc.CreateElement("LastName");
                nodeQQInfo.AppendChild(nodeLName);
                if (dtRate.Rows[0]["CUSTOMER_LAST_NAME"] != System.DBNull.Value)
                {
                    nodeLName.InnerText = dtRate.Rows[0]["CUSTOMER_LAST_NAME"].ToString();
                }

                XmlNode nodeGender = doc.CreateElement("GENDER");
                nodeQQInfo.AppendChild(nodeGender);
                if (dtRate.Rows[0]["GENDER"] != System.DBNull.Value)
                {
                    string strGender = dtRate.Rows[0]["GENDER"].ToString();
                    if (strGender == "M")
                    {
                        nodeGender.InnerText = "Male";
                    }
                    else
                    {
                        nodeGender.InnerText = "Female";
                    }
                }

                XmlNode nodeDOB = doc.CreateElement("DOB");
                nodeQQInfo.AppendChild(nodeDOB);
                if (dtRate.Rows[0]["DATE_OF_BIRTH"] != System.DBNull.Value)
                {
                    nodeDOB.InnerText = Convert.ToDateTime(dtRate.Rows[0]["DATE_OF_BIRTH"].ToString()).ToShortDateString();
                }

                XmlNode nodeNationality = doc.CreateElement("NationalityExcludeForeigner");
                nodeQQInfo.AppendChild(nodeNationality);
                if (dtRate.Rows[0]["NATIONALITY"] != System.DBNull.Value)
                {
                    string strCountry = dtRate.Rows[0]["NATIONALITY"].ToString();
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetCountryName(strCountry);
                        nodeNationality.InnerText = dt.Rows[0]["COUNTRY_NAME"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeOccupationType = doc.CreateElement("OCCUPATION");
                nodeQQInfo.AppendChild(nodeOccupationType);
                if (dtRate.Rows[0]["IS_HOME_EMPLOYEE"] != System.DBNull.Value)
                {
                    string strOCCType = dtRate.Rows[0]["IS_HOME_EMPLOYEE"].ToString();
                    if (strOCCType == "1")
                    {
                        nodeOccupationType.InnerText = "Indoor";
                    }
                    else
                    {
                        nodeOccupationType.InnerText = "Outdoor";
                    }
                }

                XmlNode nodeOccupation = doc.CreateElement("OccupationList");
                nodeQQInfo.AppendChild(nodeOccupation);
                if (dtRate.Rows[0]["CUSTOMER_OCCU"] != System.DBNull.Value)
                {
                    int strOccId = int.Parse(dtRate.Rows[0]["CUSTOMER_OCCU"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(strOccId);
                        nodeOccupation.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeDriveExp = doc.CreateElement("DRIVING_EXP");
                nodeQQInfo.AppendChild(nodeDriveExp);
                if (dtRate.Rows[0]["DRIVER_EXP_YEAR"] != System.DBNull.Value)
                {
                    nodeDriveExp.InnerText = dtRate.Rows[0]["DRIVER_EXP_YEAR"].ToString();
                }

                XmlNode nodeExistNCD = doc.CreateElement("EXISTING_NCD");
                nodeQQInfo.AppendChild(nodeExistNCD);
                if (dtRate.Rows[0]["EXISTING_NCD"] != System.DBNull.Value)
                {
                    string EXNCD = dtRate.Rows[0]["EXISTING_NCD"].ToString();
                    nodeExistNCD.InnerText = EXNCD.Remove(EXNCD.IndexOf("%"));
                }

                XmlNode nodeAnyClaim = doc.CreateElement("AnyClaimmade");
                nodeQQInfo.AppendChild(nodeAnyClaim);
                if (dtRate.Rows[0]["ANY_CLAIM"] != System.DBNull.Value)
                {
                    string strIsClaim = dtRate.Rows[0]["ANY_CLAIM"].ToString();
                    if (strIsClaim.Trim() == "1")
                    {
                        nodeAnyClaim.InnerText = "Yes";
                    }
                    else
                    {
                        nodeAnyClaim.InnerText = "No";
                    }
                }

                XmlNode nodeNCDLESS10 = doc.CreateElement("Isyourexisting");
                nodeQQInfo.AppendChild(nodeNCDLESS10);
                if (dtRate.Rows[0]["EXIST_NCD_LESS_10"] != System.DBNull.Value)
                {
                    string NCDLess = dtRate.Rows[0]["EXIST_NCD_LESS_10"].ToString();
                    if (NCDLess.Trim() == "1")
                    {
                        nodeNCDLESS10.InnerText = "Yes";
                    }
                    else
                    {
                        nodeNCDLESS10.InnerText = "No";
                    }
                }

                XmlNode nodeDemerit = doc.CreateElement("DemeritsPointsFreeDiscounts");
                nodeQQInfo.AppendChild(nodeDemerit);
                if (dtRate.Rows[0]["DEMERIT_DISCOUNT"] != System.DBNull.Value)
                {
                    string strDemerit = dtRate.Rows[0]["DEMERIT_DISCOUNT"].ToString();
                    if (strDemerit.Trim() == "1")
                    {
                        nodeDemerit.InnerText = "Yes";
                    }
                    else
                    {
                        nodeDemerit.InnerText = "No";
                    }
                }

                XmlNode nodeDriverAge = doc.CreateElement("Driver_AGE");
                nodeQQInfo.AppendChild(nodeDriverAge);


                int yearDOB = 0;
                int yearEff = 0;
                if (dtRate.Rows[0]["DATE_OF_BIRTH"] != System.DBNull.Value)
                {
                    DateTime dtDOB = Convert.ToDateTime(dtRate.Rows[0]["DATE_OF_BIRTH"].ToString());
                    yearDOB = dtDOB.Year;
                }
                if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    DateTime dtEff = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                    yearEff = dtEff.Year;
                }

                int dAge = yearEff - yearDOB;
                nodeDriverAge.InnerText = dAge.ToString();



                XmlNode nodeRiskInfo = doc.CreateElement("RiskInformation");
                productRiskData.AppendChild(nodeRiskInfo);

                XmlNode nodeYearReg = doc.CreateElement("YEAR_OF_REG");
                nodeRiskInfo.AppendChild(nodeYearReg);
                if (dtRate.Rows[0]["YEAR_OF_REG"] != System.DBNull.Value)
                {
                    nodeYearReg.InnerText = dtRate.Rows[0]["YEAR_OF_REG"].ToString();
                }

                XmlNode nodeMake = doc.CreateElement("MAKE");
                nodeRiskInfo.AppendChild(nodeMake);
                if (dtRate.Rows[0]["MAKE"] != System.DBNull.Value)
                {
                    int makeID = int.Parse(dtRate.Rows[0]["MAKE"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(makeID);
                        nodeMake.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeModel = doc.CreateElement("MODEL");
                nodeRiskInfo.AppendChild(nodeModel);
                if (dtRate.Rows[0]["MODEL"] != System.DBNull.Value)
                {
                    int ModelID = int.Parse(dtRate.Rows[0]["MODEL"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetModelDesc(ModelID);
                        nodeModel.InnerText = dt.Rows[0]["MODEL"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeVehType = doc.CreateElement("VEHICLE_TYPE");
                nodeRiskInfo.AppendChild(nodeVehType);
                if (dtRate.Rows[0]["MODEL_TYPE"] != System.DBNull.Value)
                {
                    int TypeId = int.Parse(dtRate.Rows[0]["MODEL_TYPE"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetModelTypeDesc(TypeId);
                        nodeVehType.InnerText = dt.Rows[0]["MODEL_TYPE"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeEngCapacity = doc.CreateElement("ENG_CAPACITY");
                nodeRiskInfo.AppendChild(nodeEngCapacity);
                if (dtRate.Rows[0]["ENG_CAPACITY"] != System.DBNull.Value)
                {
                    nodeEngCapacity.InnerText = dtRate.Rows[0]["ENG_CAPACITY"].ToString();
                }

                XmlNode nodeNumDriver = doc.CreateElement("NO_OF_DRIVERS");
                nodeRiskInfo.AppendChild(nodeNumDriver);
                if (dtRate.Rows[0]["NO_OF_DRIVERS"] != System.DBNull.Value)
                {
                    nodeNumDriver.InnerText = dtRate.Rows[0]["NO_OF_DRIVERS"].ToString();
                }

                XmlNode nodeClaims = doc.CreateElement("Areclaims");
                nodeRiskInfo.AppendChild(nodeClaims);
                if (dtRate.Rows[0]["CLAIM_RISK"] != System.DBNull.Value)
                {
                    string strRiskClaim = dtRate.Rows[0]["CLAIM_RISK"].ToString();
                    if (strRiskClaim.Trim() == "1")
                    {
                        nodeClaims.InnerText = "Yes";
                    }
                    else
                    {
                        nodeClaims.InnerText = "No";
                    }
                }

                XmlNode nodeNumClaim = doc.CreateElement("Noofclaims");
                nodeRiskInfo.AppendChild(nodeNumClaim);
                if (dtRate.Rows[0]["NO_OF_CLAIM"] != System.DBNull.Value)
                {
                    nodeNumClaim.InnerText = dtRate.Rows[0]["NO_OF_CLAIM"].ToString();
                }

                XmlNode nodeTotalClaimAmt = doc.CreateElement("TOTAL_CLAIM_AMT");
                nodeRiskInfo.AppendChild(nodeTotalClaimAmt);
                if (dtRate.Rows[0]["TOTAL_CLAIM_AMT"] != System.DBNull.Value)
                {
                    nodeTotalClaimAmt.InnerText = dtRate.Rows[0]["TOTAL_CLAIM_AMT"].ToString();
                }

                XmlNode nodeCoverage = doc.CreateElement("COVERAGE_TYPE");
                nodeRiskInfo.AppendChild(nodeCoverage);
                if (dtRate.Rows[0]["COVERAGE_TYPE"] != System.DBNull.Value)
                {
                    int COVID = int.Parse(dtRate.Rows[0]["COVERAGE_TYPE"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetCoverageInfobyID(COVID);
                        nodeCoverage.InnerText = dt.Rows[0]["COV_DES"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeNoClaimDisc = doc.CreateElement("NoClaimsDiscount");
                nodeRiskInfo.AppendChild(nodeNoClaimDisc);
                if (dtRate.Rows[0]["NO_CLAIM_DISCOUNT"] != System.DBNull.Value)
                {
                    string strNoClaim = dtRate.Rows[0]["NO_CLAIM_DISCOUNT"].ToString();
                    if (strNoClaim.Trim() == "1")
                    {
                        nodeNoClaimDisc.InnerText = "Yes";
                    }
                    else
                    {
                        nodeNoClaimDisc.InnerText = "No";
                    }
                }

                XmlNode nodeFromDate = doc.CreateElement("Fromdate");
                nodeRiskInfo.AppendChild(nodeFromDate);
                if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                    nodeFromDate.InnerText = String.Format("{0:dd/MM/yyyy}", dt);
                }

                XmlNode nodeToDate = doc.CreateElement("ToDate");
                nodeRiskInfo.AppendChild(nodeToDate);
                if (dtRate.Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
                {
                    DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString());
                    nodeToDate.InnerText = String.Format("{0:dd/MM/yyyy}", dt);
                }

                XmlNode nodeCalEvoke = doc.CreateElement("CalculateEvokePage");
                productRiskData.AppendChild(nodeCalEvoke);

                XmlNode nodepremiumOprion = doc.CreateElement("PagePremiumOptionExist");
                productRiskData.AppendChild(nodepremiumOprion);

                XmlNode nodeDisplayCom = doc.CreateElement("DisplayComputation");
                productRiskData.AppendChild(nodeDisplayCom);

                XmlNode nodeMultiQuote = doc.CreateElement("MultiQuoteApplies");
                productRiskData.AppendChild(nodeMultiQuote);


                string strRqXML = doc.OuterXml.ToString();

                return strRqXML;

            }
            else
            {
                return "";
            }
        }

        private string GeteRateXML()
        {
            //Code by kuldeep on 10_jan_2012
            Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQuoteDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();
            DataSet dsRate = new DataSet();
            if (!strCalledFrom.ToUpper().ToString().Equals("QQ") && !strCalledFrom.ToUpper().ToString().Equals("QAPP") && strCalledFrom.ToUpper().ToString() != "")
            {
                dsRate = objQuoteDetails.FetchpQuoteDataForRXml(customerID, PolicyID, PolicyVersionID);
            }
            else
            {
              
                 dsRate = objQuoteDetails.FetchDataForQQRqXml(customerID, PolicyID, PolicyVersionID);
               
            }
            DataTable dtRate = dsRate.Tables[0];

            XmlDocument objeRateXML = new XmlDocument();

            if (dtRate.Rows.Count != 0)
            {

                string eRateRequestXmlPath = ClsCommon.GetKeyValueWithIP("eRate_RequestXmlPath");
                //objeRateXML.Load(@"D:\Projects\EbixAdvantage-Brazil\Source Code\Cms\application\aspx\eRateInput.xml");
                objeRateXML.Load(eRateRequestXmlPath);

                XmlNode dataNode = objeRateXML.SelectSingleNode("//Product/PolicyHeader/PeriodOfInsurance");
                XmlNode currentNode = null;

                if (dataNode != null)
                {
                    currentNode = dataNode.SelectSingleNode("InceptionDateTime");
                    if (currentNode != null)
                    {
                        DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                        currentNode.InnerText = String.Format("{0:s}", dt);                         
                    }

                    currentNode = dataNode.SelectSingleNode("EffectiveDateTime");
                    if (currentNode != null)
                    {
                        DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                        currentNode.InnerText = String.Format("{0:s}", dt);
                    }

                    currentNode = dataNode.SelectSingleNode("ExpiryDateTime");
                    if (currentNode != null)
                    {
                        DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString());
                        currentNode.InnerText = String.Format("{0:s}", dt);
                    }

                }

                dataNode = objeRateXML.SelectSingleNode("//Product/LOBs/LOB/ProductRiskData/QuickQuoteInformation");
                if (dataNode != null)
                {
                    currentNode = dataNode.SelectSingleNode("POLICY_LOB");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["POLICY_LOB"] != System.DBNull.Value)
                        {
                            int LOB_UID = int.Parse(dtRate.Rows[0]["POLICY_LOB"].ToString());
                            DataTable dt = new DataTable();
                            try
                            {
                                dt = ClsLookup.GetLookupValueFromUniqueID(LOB_UID);
                                currentNode.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                            }
                            catch
                            { }

                        }
                    }

                    currentNode = dataNode.SelectSingleNode("PolicyCurrencydropdown");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["POLICY_CURRENCY"] != System.DBNull.Value)
                        {
                            int POL_CURR = int.Parse(dtRate.Rows[0]["POLICY_CURRENCY"].ToString());
                            DataTable dt = new DataTable();
                            try
                            {
                                dt = ClsLookup.GetLookupValueFromUniqueID(POL_CURR);
                                currentNode.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                            }
                            catch
                            { }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("APP_TERMS");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["APP_TERMS"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["APP_TERMS"].ToString() + " " + "Months";
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("APP_EFFECTIVE_DATE");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                        {
                            DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                            currentNode.InnerText = String.Format("{0:dd/MM/yyyy}", dt);
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("APP_EXPIRATION_DATE");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
                        {
                            DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString());
                            currentNode.InnerText = String.Format("{0:dd/MM/yyyy}", dt);
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("BILL_TYPE_ID");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["BILL_TYPE"] != System.DBNull.Value)
                        {
                            string BillCode = dtRate.Rows[0]["BILL_TYPE"].ToString();
                            int BILL_UID = 0;
                            switch (BillCode)
                            {
                                case "AB":
                                    BILL_UID = 8459;
                                    break;
                                case "DB":
                                    BILL_UID = 8460;
                                    break;
                                case "DM":
                                    BILL_UID = 11150;
                                    break;
                                case "DI":
                                    BILL_UID = 11191;
                                    break;
                                case "MB":
                                    BILL_UID = 11276;
                                    break;
                                case "AM":
                                    BILL_UID = 11277;
                                    break;
                                case "IM":
                                    BILL_UID = 11278;
                                    break;
                                default:
                                    BILL_UID = 8459;
                                    break;
                            }
                            DataTable dt = new DataTable();
                            try
                            {
                                dt = ClsLookup.GetLookupValueFromUniqueID(BILL_UID);
                                currentNode.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                            }
                            catch
                            { }
                        }
                    }

                    //currentNode = dataNode.SelectSingleNode("INSTALL_PLAN_ID");
                    //if (currentNode != null)
                    //{
                    //    // Currently not required
                    //}

                    //currentNode = dataNode.SelectSingleNode("MULTIPLIER");
                    //if (currentNode != null)
                    //{
                    //    currentNode.InnerText = "100";
                    //}

                    currentNode = dataNode.SelectSingleNode("eProfessionalClientCode");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["CUSTOMER_CODE"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["CUSTOMER_CODE"].ToString();
                           
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("CUSTOMER_TYPE");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["CUSTOMER_TYPE"] != System.DBNull.Value)
                        {
                            int custTypeID = int.Parse(dtRate.Rows[0]["CUSTOMER_TYPE"].ToString());
                            DataTable dt = new DataTable();
                            try
                            {
                                dt = ClsLookup.GetLookupValueFromUniqueID(custTypeID);
                                currentNode.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                            }
                            catch
                            { }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("FirstName");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["CUSTOMER_FIRST_NAME"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["CUSTOMER_FIRST_NAME"].ToString();
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("MiddleName");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["CUSTOMER_MIDDLE_NAME"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString();
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("LastName");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["CUSTOMER_LAST_NAME"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["CUSTOMER_LAST_NAME"].ToString();
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("GENDER");
                    if (currentNode != null)
                    {
                        string strGender = dtRate.Rows[0]["GENDER"].ToString();
                        if (strGender == "M")
                        {
                            currentNode.InnerText = "Male";
                        }
                        else
                        {
                            currentNode.InnerText = "Female";
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("DOB");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["DATE_OF_BIRTH"] != System.DBNull.Value)
                        {
                            DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["DATE_OF_BIRTH"].ToString());
                            currentNode.InnerText = String.Format("{0:dd/MM/yyyy}", dt);
                            
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("NationalityExcludeForeigner");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["NATIONALITY"] != System.DBNull.Value)
                        {
                            string strCountry = dtRate.Rows[0]["NATIONALITY"].ToString();
                            DataTable dt = new DataTable();
                            try
                            {
                                dt = ClsLookup.GetCountryName(strCountry);
                                currentNode.InnerText = dt.Rows[0]["COUNTRY_NAME"].ToString();
                            }
                            catch
                            { }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("OCCUPATION");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["IS_HOME_EMPLOYEE"] != System.DBNull.Value)
                        {
                            string strOCCType = dtRate.Rows[0]["IS_HOME_EMPLOYEE"].ToString();
                            if (strOCCType == "1")
                            {
                                currentNode.InnerText = "Indoor";
                            }
                            else
                            {
                                currentNode.InnerText = "Outdoor";
                            }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("OccupationList");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["CUSTOMER_OCCU"] != System.DBNull.Value)
                        {
                            int strOccId = int.Parse(dtRate.Rows[0]["CUSTOMER_OCCU"].ToString());
                            DataTable dt = new DataTable();
                            try
                            {
                                dt = ClsLookup.GetLookupValueFromUniqueID(strOccId);
                                currentNode.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                            }
                            catch
                            { }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("DRIVING_EXP");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["DRIVER_EXP_YEAR"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["DRIVER_EXP_YEAR"].ToString();
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("EXISTING_NCD");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["EXISTING_NCD"] != System.DBNull.Value)
                        {
                            string EXNCD = dtRate.Rows[0]["EXISTING_NCD"].ToString();
                            currentNode.InnerText = EXNCD.Remove(EXNCD.IndexOf("%"));
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("AnyClaimmade");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["ANY_CLAIM"] != System.DBNull.Value)
                        {
                            string strIsClaim = dtRate.Rows[0]["ANY_CLAIM"].ToString();
                            if (strIsClaim.Trim() == "1")
                            {
                                currentNode.InnerText = "Yes";
                            }
                            else
                            {
                                currentNode.InnerText = "No";
                            }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("Isyourexisting");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["EXIST_NCD_LESS_10"] != System.DBNull.Value)
                        {
                            string NCDLess = dtRate.Rows[0]["EXIST_NCD_LESS_10"].ToString();
                            if (NCDLess.Trim() == "1")
                            {
                                currentNode.InnerText = "Yes";
                            }
                            else
                            {
                                currentNode.InnerText = "No";
                            }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("DemeritsPointsFreeDiscounts");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["DEMERIT_DISCOUNT"] != System.DBNull.Value)
                        {
                            string strDemerit = dtRate.Rows[0]["DEMERIT_DISCOUNT"].ToString();
                            if (strDemerit.Trim() == "1")
                            {
                                currentNode.InnerText = "Yes";
                            }
                            else
                            {
                                currentNode.InnerText = "No";
                            }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("Driver_AGE");
                    if (currentNode != null)
                    {
                        int yearDOB = 0;
                        int yearEff = 0;
                        if (dtRate.Rows[0]["DATE_OF_BIRTH"] != System.DBNull.Value)
                        {
                            DateTime dtDOB = Convert.ToDateTime(dtRate.Rows[0]["DATE_OF_BIRTH"].ToString());
                            yearDOB = dtDOB.Year;
                        }
                        if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                        {
                            DateTime dtEff = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                            yearEff = dtEff.Year;
                        }

                        int dAge = yearEff - yearDOB;
                        currentNode.InnerText = dAge.ToString();
                    }


                }

                dataNode = objeRateXML.SelectSingleNode("Product/LOBs/LOB/ProductRiskData/RiskInformation");

                if (dataNode != null)
                {
                    currentNode = dataNode.SelectSingleNode("YEAR_OF_REG");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["YEAR_OF_REG"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["YEAR_OF_REG"].ToString();
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("MAKE");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["MAKE"] != System.DBNull.Value)
                        {
                            int makeID = int.Parse(dtRate.Rows[0]["MAKE"].ToString());
                            DataTable dt = new DataTable();
                            try
                            {
                                dt = ClsLookup.GetLookupValueFromUniqueID(makeID);
                                currentNode.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString().ToUpper();
                            }
                            catch
                            { }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("MODEL");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["MODEL"] != System.DBNull.Value)
                        {
                            int ModelID = int.Parse(dtRate.Rows[0]["MODEL"].ToString());
                            DataTable dt = new DataTable();
                            try
                            {
                                dt = ClsLookup.GetModelDesc(ModelID);
                                string strModel = dt.Rows[0]["MODEL"].ToString().ToUpper();
                                strModel = strModel.Replace("(", "");
                                strModel = strModel.Replace(")", "");
                                currentNode.InnerText = strModel;
                            }
                            catch
                            { }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("VEHICLE_TYPE");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["MODEL_TYPE"] != System.DBNull.Value)
                        {
                            int TypeId = int.Parse(dtRate.Rows[0]["MODEL_TYPE"].ToString());
                            DataTable dt = new DataTable();
                            try
                            {
                                dt = ClsLookup.GetModelTypeDesc(TypeId);
                                string strModel = dt.Rows[0]["MODEL_TYPE"].ToString().ToUpper();
                                strModel = strModel.Replace("(", "");
                                strModel = strModel.Replace(")", "");
                                currentNode.InnerText = strModel;
                            }
                            catch
                            { }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("ENG_CAPACITY");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["ENG_CAPACITY"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["ENG_CAPACITY"].ToString();
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("NO_OF_DRIVERS");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["NO_OF_DRIVERS"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["NO_OF_DRIVERS"].ToString();
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("Areclaims");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["CLAIM_RISK"] != System.DBNull.Value)
                        {
                            string strRiskClaim = dtRate.Rows[0]["CLAIM_RISK"].ToString();
                            if (strRiskClaim.Trim() == "1")
                            {
                                currentNode.InnerText = "Yes";
                            }
                            else
                            {
                                currentNode.InnerText = "No";
                            }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("Noofclaims");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["NO_OF_CLAIM"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["NO_OF_CLAIM"].ToString();
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("Areclaims");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["CLAIM_RISK"] != System.DBNull.Value)
                        {
                            string strRiskClaim = dtRate.Rows[0]["CLAIM_RISK"].ToString();
                            if (strRiskClaim.Trim() == "1")
                            {
                                currentNode.InnerText = "Yes";
                            }
                            else
                            {
                                currentNode.InnerText = "No";
                            }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("TOTAL_CLAIM_AMT");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["TOTAL_CLAIM_AMT"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["TOTAL_CLAIM_AMT"].ToString();
                        }

                    }

                    currentNode = dataNode.SelectSingleNode("COVERAGE_TYPE");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["COVERAGE_TYPE"] != System.DBNull.Value)
                        {
                            int COVID = int.Parse(dtRate.Rows[0]["COVERAGE_TYPE"].ToString());
                            DataTable dt = new DataTable();
                            try
                            {
                                dt = ClsLookup.GetCoverageInfobyID(COVID);
                                currentNode.InnerText = dt.Rows[0]["COV_DES"].ToString().ToUpper();
                            }
                            catch
                            { }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("NoClaimsDiscount");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["NO_CLAIM_DISCOUNT"] != System.DBNull.Value)
                        {
                            string strNoClaim = dtRate.Rows[0]["NO_CLAIM_DISCOUNT"].ToString();
                            if (strNoClaim.Trim() == "1")
                            {
                                currentNode.InnerText = "Yes";
                            }
                            else
                            {
                                currentNode.InnerText = "No";
                            }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("Fromdate");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                        {
                            DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                            currentNode.InnerText = String.Format("{0:dd/MM/yyyy}", dt);
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("ToDate");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
                        {
                            DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString());
                            currentNode.InnerText = String.Format("{0:dd/MM/yyyy}", dt);
                        }
                    }
                }




            }

            string strRateXml = objeRateXML.OuterXml.ToString();

            return strRateXml;

        }
        //Added By Kuldeep for Marine Rate XML 22-03-2012
        private string GetMarineRateXML()
        {
          
            Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQuoteDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();
            DataSet dsRate = new DataSet();
            if (!strCalledFrom.ToUpper().ToString().Equals("QQ") && !strCalledFrom.ToUpper().ToString().Equals("QAPP") && strCalledFrom.ToUpper().ToString() != "")
            {
                
                if (GetLOBID() == "13")
                {
                    dsRate = objQuoteDetails.FetchpQuoteDataForMarineXml(customerID, PolicyID, PolicyVersionID);
                }
                else
                {
                    dsRate = objQuoteDetails.FetchpQuoteDataForRXml(customerID, PolicyID, PolicyVersionID);
                }
            }
            else
            {
                //Added by Kuldeep to Call Marine Cargo Quote Details on 22-03-2012
                if (GetLOBID() == "13")
                {
                    dsRate = objQuoteDetails.FetchpQuoteDataForMarineXml(customerID, PolicyID, PolicyVersionID);
                }
                else
                {
                    dsRate = objQuoteDetails.FetchDataForQQRqXml(customerID, PolicyID, PolicyVersionID);
                }
            }
            DataTable dtRate = dsRate.Tables[0];

            XmlDocument objeRateXML = new XmlDocument();

            if (dtRate.Rows.Count != 0)
            {

                string eRateRequestXmlPath = ClsCommon.GetKeyValueWithIP("eRate_RequestXmlPathMarine");
                //objeRateXML.Load(@"D:\Projects\EbixAdvantage-Brazil\Source Code\Cms\application\aspx\eRateInput.xml");
                objeRateXML.Load(eRateRequestXmlPath);

                XmlNode dataNode = objeRateXML.SelectSingleNode("//Product/PolicyHeader/PeriodOfInsurance");
                XmlNode currentNode = null;

                if (dataNode != null)
                {
                    currentNode = dataNode.SelectSingleNode("InceptionDateTime");
                    if (currentNode != null)
                    {
                        DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                        currentNode.InnerText = String.Format("{0:s}", dt);
                    }

                    currentNode = dataNode.SelectSingleNode("EffectiveDateTime");
                    if (currentNode != null)
                    {
                        DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                        currentNode.InnerText = String.Format("{0:s}", dt);
                    }

                    currentNode = dataNode.SelectSingleNode("ExpiryDateTime");
                    if (currentNode != null)
                    {
                        DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString());
                        currentNode.InnerText = String.Format("{0:s}", dt);
                    }

                }

                dataNode = objeRateXML.SelectSingleNode("//Product/LOBs/LOB/ProductRiskData/POLICY_INFORMATION/QuickQuoteInformation");
                if (dataNode != null)
                {
                    currentNode = dataNode.SelectSingleNode("POLICY_LOB");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["POLICY_LOB"] != System.DBNull.Value)
                        {
                            int LOB_UID = int.Parse(dtRate.Rows[0]["POLICY_LOB"].ToString());
                            DataTable dt = new DataTable();
                            try
                            {
                                dt = ClsLookup.GetLookupValueFromUniqueID(LOB_UID);
                                currentNode.InnerText = "MARINE CARGO";
                                //currentNode.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                            }
                            catch
                            { }

                        }
                    }

                    currentNode = dataNode.SelectSingleNode("POLICY_CURRENCY");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["POLICY_CURRENCY"] != System.DBNull.Value)
                        {
                            int POL_CURR = int.Parse(dtRate.Rows[0]["POLICY_CURRENCY"].ToString());
                            DataTable dt = new DataTable();
                            try
                            {
                                dt = ClsLookup.GetLookupValueFromUniqueID(POL_CURR);
                                currentNode.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                            }
                            catch
                            { }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("APP_TERMS");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["APP_TERMS"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["APP_TERMS"].ToString() + " " + "Months";
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("APP_EFFECTIVE_DATE");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                        {
                            DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                            currentNode.InnerText = String.Format("{0:dd/MM/yyyy}", dt);
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("APP_EXPIRATION_DATE");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
                        {
                            DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString());
                            currentNode.InnerText = String.Format("{0:dd/MM/yyyy}", dt);
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("BILL_TYPE_ID");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["BILL_TYPE"] != System.DBNull.Value)
                        {
                            string BillCode = dtRate.Rows[0]["BILL_TYPE"].ToString();
                            int BILL_UID = 0;
                            switch (BillCode)
                            {
                                case "AB":
                                    BILL_UID = 8459;
                                    break;
                                case "DB":
                                    BILL_UID = 8460;
                                    break;
                                case "DM":
                                    BILL_UID = 11150;
                                    break;
                                case "DI":
                                    BILL_UID = 11191;
                                    break;
                                case "MB":
                                    BILL_UID = 11276;
                                    break;
                                case "AM":
                                    BILL_UID = 11277;
                                    break;
                                case "IM":
                                    BILL_UID = 11278;
                                    break;
                                default:
                                    BILL_UID = 8459;
                                    break;
                            }
                            DataTable dt = new DataTable();
                            try
                            {
                                dt = ClsLookup.GetLookupValueFromUniqueID(BILL_UID);
                                currentNode.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                            }
                            catch
                            { }
                        }
                    }

                    //currentNode = dataNode.SelectSingleNode("INSTALL_PLAN_ID");
                    //if (currentNode != null)
                    //{
                    //    // Currently not required
                    //}

                    //currentNode = dataNode.SelectSingleNode("MULTIPLIER");
                    //if (currentNode != null)
                    //{
                    //    currentNode.InnerText = "100";
                    //}

                    //currentNode = dataNode.SelectSingleNode("eProfessionalClientCode");
                    //if (currentNode != null)
                    //{
                    //    if (dtRate.Rows[0]["CUSTOMER_CODE"] != System.DBNull.Value)
                    //    {
                    //        currentNode.InnerText = dtRate.Rows[0]["CUSTOMER_CODE"].ToString();

                    //    }
                    //}

                    currentNode = dataNode.SelectSingleNode("CUSTOMER_TYPE");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["CUSTOMER_TYPE"] != System.DBNull.Value)
                        {
                            int custTypeID = int.Parse(dtRate.Rows[0]["CUSTOMER_TYPE"].ToString());
                            DataTable dt = new DataTable();
                            try
                            {
                                dt = ClsLookup.GetLookupValueFromUniqueID(custTypeID);
                                currentNode.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                            }
                            catch
                            { }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("COMPANY_NAME");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["COMPANY_NAME"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["COMPANY_NAME"].ToString();
                        }
                    }
                    currentNode = dataNode.SelectSingleNode("BUSINESS_TYPE");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["BUSINESS_TYPE"] != System.DBNull.Value)
                        {
                            int strOccId = int.Parse(dtRate.Rows[0]["BUSINESS_TYPE"].ToString());
                            DataTable dt = new DataTable();
                            try
                            {
                                dt = ClsLookup.GetLookupValueFromUniqueID(strOccId);
                                currentNode.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                            }
                            catch
                            { }
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("INVOICE_AMOUNT");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["INVOICE_AMOUNT"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["INVOICE_AMOUNT"].ToString();
                        }
                    }
                    currentNode = dataNode.SelectSingleNode("MARK_UP_RATE_PERC");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["MARK_UP_RATE_PERC"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["MARK_UP_RATE_PERC"].ToString();
                        }
                    }
                    #region Commented for motor
                    //currentNode = dataNode.SelectSingleNode("MiddleName");
                    //if (currentNode != null)
                    //{
                    //    if (dtRate.Rows[0]["CUSTOMER_MIDDLE_NAME"] != System.DBNull.Value)
                    //    {
                    //        currentNode.InnerText = dtRate.Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString();
                    //    }
                    //}

                    //currentNode = dataNode.SelectSingleNode("LastName");
                    //if (currentNode != null)
                    //{
                    //    if (dtRate.Rows[0]["CUSTOMER_LAST_NAME"] != System.DBNull.Value)
                    //    {
                    //        currentNode.InnerText = dtRate.Rows[0]["CUSTOMER_LAST_NAME"].ToString();
                    //    }
                    //}

                    //currentNode = dataNode.SelectSingleNode("GENDER");
                    //if (currentNode != null)
                    //{
                    //    string strGender = dtRate.Rows[0]["GENDER"].ToString();
                    //    if (strGender == "M")
                    //    {
                    //        currentNode.InnerText = "Male";
                    //    }
                    //    else
                    //    {
                    //        currentNode.InnerText = "Female";
                    //    }
                    //}

                    //currentNode = dataNode.SelectSingleNode("DOB");
                    //if (currentNode != null)
                    //{
                    //    if (dtRate.Rows[0]["DATE_OF_BIRTH"] != System.DBNull.Value)
                    //    {
                    //        DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["DATE_OF_BIRTH"].ToString());
                    //        currentNode.InnerText = String.Format("{0:dd/MM/yyyy}", dt);

                    //    }
                    //}

                    //currentNode = dataNode.SelectSingleNode("NationalityExcludeForeigner");
                    //if (currentNode != null)
                    //{
                    //    if (dtRate.Rows[0]["NATIONALITY"] != System.DBNull.Value)
                    //    {
                    //        string strCountry = dtRate.Rows[0]["NATIONALITY"].ToString();
                    //        DataTable dt = new DataTable();
                    //        try
                    //        {
                    //            dt = ClsLookup.GetCountryName(strCountry);
                    //            currentNode.InnerText = dt.Rows[0]["COUNTRY_NAME"].ToString();
                    //        }
                    //        catch
                    //        { }
                    //    }
                    //}


                

                    //currentNode = dataNode.SelectSingleNode("DRIVING_EXP");
                    //if (currentNode != null)
                    //{
                    //    if (dtRate.Rows[0]["DRIVER_EXP_YEAR"] != System.DBNull.Value)
                    //    {
                    //        currentNode.InnerText = dtRate.Rows[0]["DRIVER_EXP_YEAR"].ToString();
                    //    }
                    //}

                    //currentNode = dataNode.SelectSingleNode("EXISTING_NCD");
                    //if (currentNode != null)
                    //{
                    //    if (dtRate.Rows[0]["EXISTING_NCD"] != System.DBNull.Value)
                    //    {
                    //        string EXNCD = dtRate.Rows[0]["EXISTING_NCD"].ToString();
                    //        currentNode.InnerText = EXNCD.Remove(EXNCD.IndexOf("%"));
                    //    }
                    //}

                    //currentNode = dataNode.SelectSingleNode("AnyClaimmade");
                    //if (currentNode != null)
                    //{
                    //    if (dtRate.Rows[0]["ANY_CLAIM"] != System.DBNull.Value)
                    //    {
                    //        string strIsClaim = dtRate.Rows[0]["ANY_CLAIM"].ToString();
                    //        if (strIsClaim.Trim() == "1")
                    //        {
                    //            currentNode.InnerText = "Yes";
                    //        }
                    //        else
                    //        {
                    //            currentNode.InnerText = "No";
                    //        }
                    //    }
                    //}

                    //currentNode = dataNode.SelectSingleNode("Isyourexisting");
                    //if (currentNode != null)
                    //{
                    //    if (dtRate.Rows[0]["EXIST_NCD_LESS_10"] != System.DBNull.Value)
                    //    {
                    //        string NCDLess = dtRate.Rows[0]["EXIST_NCD_LESS_10"].ToString();
                    //        if (NCDLess.Trim() == "1")
                    //        {
                    //            currentNode.InnerText = "Yes";
                    //        }
                    //        else
                    //        {
                    //            currentNode.InnerText = "No";
                    //        }
                    //    }
                    //}

                    //currentNode = dataNode.SelectSingleNode("DemeritsPointsFreeDiscounts");
                    //if (currentNode != null)
                    //{
                    //    if (dtRate.Rows[0]["DEMERIT_DISCOUNT"] != System.DBNull.Value)
                    //    {
                    //        string strDemerit = dtRate.Rows[0]["DEMERIT_DISCOUNT"].ToString();
                    //        if (strDemerit.Trim() == "1")
                    //        {
                    //            currentNode.InnerText = "Yes";
                    //        }
                    //        else
                    //        {
                    //            currentNode.InnerText = "No";
                    //        }
                    //    }
                    //}

                    //currentNode = dataNode.SelectSingleNode("Driver_AGE");
                    //if (currentNode != null)
                    //{
                    //    int yearDOB = 0;
                    //    int yearEff = 0;
                    //    if (dtRate.Rows[0]["DATE_OF_BIRTH"] != System.DBNull.Value)
                    //    {
                    //        DateTime dtDOB = Convert.ToDateTime(dtRate.Rows[0]["DATE_OF_BIRTH"].ToString());
                    //        yearDOB = dtDOB.Year;
                    //    }
                    //    if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                    //    {
                    //        DateTime dtEff = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                    //        yearEff = dtEff.Year;
                    //    }

                    //    int dAge = yearEff - yearDOB;
                    //    currentNode.InnerText = dAge.ToString();
                    //}
                    #endregion

                }

                dataNode = objeRateXML.SelectSingleNode("Product/LOBs/LOB/ProductRiskData/POLICY_INFORMATION/RiskInformation");

                if (dataNode != null)
                {
                     currentNode = dataNode.SelectSingleNode("VOYAGE_FROM");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["VOYAGE_FROM"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["VOYAGE_FROM"].ToString();
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("VOYAGE_TO");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["VOYAGE_TO"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["VOYAGE_TO"].ToString();
                        }
                    }

                    currentNode = dataNode.SelectSingleNode("VOYAGE_FROM_DATE");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["VOYAGE_FROM_DATE"] != System.DBNull.Value)
                        {
                            DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["VOYAGE_FROM_DATE"].ToString());
                            currentNode.InnerText = String.Format("{0:dd/MM/yyyy}", dt);
                        }
                    }
                    currentNode = dataNode.SelectSingleNode("MARINE_RATE");
                    if (currentNode != null)
                    {
                        if (dtRate.Rows[0]["MARINE_RATE"] != System.DBNull.Value)
                        {
                            currentNode.InnerText = dtRate.Rows[0]["MARINE_RATE"].ToString();
                        }
                    }
#region Commented_Motor_Risk
                //    currentNode = dataNode.SelectSingleNode("YEAR_OF_REG");
                //    if (currentNode != null)
                //    {
                //        if (dtRate.Rows[0]["YEAR_OF_REG"] != System.DBNull.Value)
                //        {
                //            currentNode.InnerText = dtRate.Rows[0]["YEAR_OF_REG"].ToString();
                //        }
                //    }

                //    currentNode = dataNode.SelectSingleNode("MAKE");
                //    if (currentNode != null)
                //    {
                //        if (dtRate.Rows[0]["MAKE"] != System.DBNull.Value)
                //        {
                //            int makeID = int.Parse(dtRate.Rows[0]["MAKE"].ToString());
                //            DataTable dt = new DataTable();
                //            try
                //            {
                //                dt = ClsLookup.GetLookupValueFromUniqueID(makeID);
                //                currentNode.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString().ToUpper();
                //            }
                //            catch
                //            { }
                //        }
                //    }

                //    currentNode = dataNode.SelectSingleNode("MODEL");
                //    if (currentNode != null)
                //    {
                //        if (dtRate.Rows[0]["MODEL"] != System.DBNull.Value)
                //        {
                //            int ModelID = int.Parse(dtRate.Rows[0]["MODEL"].ToString());
                //            DataTable dt = new DataTable();
                //            try
                //            {
                //                dt = ClsLookup.GetModelDesc(ModelID);
                //                string strModel = dt.Rows[0]["MODEL"].ToString().ToUpper();
                //                strModel = strModel.Replace("(", "");
                //                strModel = strModel.Replace(")", "");
                //                currentNode.InnerText = strModel;
                //            }
                //            catch
                //            { }
                //        }
                //    }

                //    currentNode = dataNode.SelectSingleNode("VEHICLE_TYPE");
                //    if (currentNode != null)
                //    {
                //        if (dtRate.Rows[0]["MODEL_TYPE"] != System.DBNull.Value)
                //        {
                //            int TypeId = int.Parse(dtRate.Rows[0]["MODEL_TYPE"].ToString());
                //            DataTable dt = new DataTable();
                //            try
                //            {
                //                dt = ClsLookup.GetModelTypeDesc(TypeId);
                //                string strModel = dt.Rows[0]["MODEL_TYPE"].ToString().ToUpper();
                //                strModel = strModel.Replace("(", "");
                //                strModel = strModel.Replace(")", "");
                //                currentNode.InnerText = strModel;
                //            }
                //            catch
                //            { }
                //        }
                //    }

                //    currentNode = dataNode.SelectSingleNode("ENG_CAPACITY");
                //    if (currentNode != null)
                //    {
                //        if (dtRate.Rows[0]["ENG_CAPACITY"] != System.DBNull.Value)
                //        {
                //            currentNode.InnerText = dtRate.Rows[0]["ENG_CAPACITY"].ToString();
                //        }
                //    }

                //    currentNode = dataNode.SelectSingleNode("NO_OF_DRIVERS");
                //    if (currentNode != null)
                //    {
                //        if (dtRate.Rows[0]["NO_OF_DRIVERS"] != System.DBNull.Value)
                //        {
                //            currentNode.InnerText = dtRate.Rows[0]["NO_OF_DRIVERS"].ToString();
                //        }
                //    }

                //    currentNode = dataNode.SelectSingleNode("Areclaims");
                //    if (currentNode != null)
                //    {
                //        if (dtRate.Rows[0]["CLAIM_RISK"] != System.DBNull.Value)
                //        {
                //            string strRiskClaim = dtRate.Rows[0]["CLAIM_RISK"].ToString();
                //            if (strRiskClaim.Trim() == "1")
                //            {
                //                currentNode.InnerText = "Yes";
                //            }
                //            else
                //            {
                //                currentNode.InnerText = "No";
                //            }
                //        }
                //    }

                //    currentNode = dataNode.SelectSingleNode("Noofclaims");
                //    if (currentNode != null)
                //    {
                //        if (dtRate.Rows[0]["NO_OF_CLAIM"] != System.DBNull.Value)
                //        {
                //            currentNode.InnerText = dtRate.Rows[0]["NO_OF_CLAIM"].ToString();
                //        }
                //    }

                //    currentNode = dataNode.SelectSingleNode("Areclaims");
                //    if (currentNode != null)
                //    {
                //        if (dtRate.Rows[0]["CLAIM_RISK"] != System.DBNull.Value)
                //        {
                //            string strRiskClaim = dtRate.Rows[0]["CLAIM_RISK"].ToString();
                //            if (strRiskClaim.Trim() == "1")
                //            {
                //                currentNode.InnerText = "Yes";
                //            }
                //            else
                //            {
                //                currentNode.InnerText = "No";
                //            }
                //        }
                //    }

                //    currentNode = dataNode.SelectSingleNode("TOTAL_CLAIM_AMT");
                //    if (currentNode != null)
                //    {
                //        if (dtRate.Rows[0]["TOTAL_CLAIM_AMT"] != System.DBNull.Value)
                //        {
                //            currentNode.InnerText = dtRate.Rows[0]["TOTAL_CLAIM_AMT"].ToString();
                //        }

                //    }

                //    currentNode = dataNode.SelectSingleNode("COVERAGE_TYPE");
                //    if (currentNode != null)
                //    {
                //        if (dtRate.Rows[0]["COVERAGE_TYPE"] != System.DBNull.Value)
                //        {
                //            int COVID = int.Parse(dtRate.Rows[0]["COVERAGE_TYPE"].ToString());
                //            DataTable dt = new DataTable();
                //            try
                //            {
                //                dt = ClsLookup.GetCoverageInfobyID(COVID);
                //                currentNode.InnerText = dt.Rows[0]["COV_DES"].ToString().ToUpper();
                //            }
                //            catch
                //            { }
                //        }
                //    }

                //    currentNode = dataNode.SelectSingleNode("NoClaimsDiscount");
                //    if (currentNode != null)
                //    {
                //        if (dtRate.Rows[0]["NO_CLAIM_DISCOUNT"] != System.DBNull.Value)
                //        {
                //            string strNoClaim = dtRate.Rows[0]["NO_CLAIM_DISCOUNT"].ToString();
                //            if (strNoClaim.Trim() == "1")
                //            {
                //                currentNode.InnerText = "Yes";
                //            }
                //            else
                //            {
                //                currentNode.InnerText = "No";
                //            }
                //        }
                //    }

                //    currentNode = dataNode.SelectSingleNode("Fromdate");
                //    if (currentNode != null)
                //    {
                //        if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                //        {
                //            DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                //            currentNode.InnerText = String.Format("{0:dd/MM/yyyy}", dt);
                //        }
                //    }

                //    currentNode = dataNode.SelectSingleNode("ToDate");
                //    if (currentNode != null)
                //    {
                //        if (dtRate.Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
                //        {
                //            DateTime dt = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString());
                //            currentNode.InnerText = String.Format("{0:dd/MM/yyyy}", dt);
                //        }
                    //    }
#endregion
                }
            }

            string strRateXml = objeRateXML.OuterXml.ToString();

            return strRateXml;

        }

        private string GetPremiumFromXmlMarine(string strResponseXml,out string strSumInsured, out string strTotalPremium)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strResponseXml);  
            strSumInsured = "0";
                    strTotalPremium = "0";
            if (doc.InnerText != null)
            {
                XmlNode nodePremium = doc.SelectSingleNode("Product/LOBs/LOB/Computation/Adjustments");

                if (nodePremium != null)
                {
                    XmlNode currentNode =nodePremium.ChildNodes[0].SelectSingleNode("Value");
                    if (currentNode != null)
                    {
                        strSumInsured = currentNode.InnerText.ToString();
                      
                    }


                    currentNode = nodePremium.ChildNodes[1].SelectSingleNode("Value");
                    if (currentNode != null)
                    {
                        strTotalPremium = currentNode.InnerText.ToString();

                    }
                  
              

                }
            }
            //Added by kuldeep to override billing info premium over erate calculated premium(21-feb-2012)
            //DataSet ds = new DataSet();
            //Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQQDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();
            //ds = objQQDetails.FetchBillingInfoPRemiumDetails(customerID, PolicyID, PolicyVersionID);
            //if (ds.Tables.Count > 0)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {

            //        BasePremium = double.Parse(ds.Tables[0].Rows[0]["GROSS_PREMIUM"].ToString());
            //        strBasePremium = BasePremium.ToString();

            //        GSTAmount = double.Parse(ds.Tables[0].Rows[0]["GST"].ToString());
            //        strGSTAmount = GSTAmount.ToString();

            //        //DemeritDisc = double.Parse(ds.Tables[0].Rows[0]["DEMERIT_DISC_AMT"].ToString());
            //       // if (DemeritDisc != 0)
            //       // {
            //            DemeritDisc = BasePremium * 5 / 100;
            //       // }
            //        strDemeritDisc = DemeritDisc.ToString();

            //    }
            //}
           
            


            //strTotalPremium = BasePremium - DemeritDisc + GSTAmount;
            return strTotalPremium;
        }
        private string GetPremiumFromXml(string strResponseXml, out string strBasePremium, out string strGSTAmount, out string strDemeritDisc)
        {
            double strTotalPremium = 0;
            strBasePremium = strGSTAmount = strDemeritDisc = "";
            double BasePremium = 0, GSTAmount = 0, DemeritDisc = 0;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strResponseXml);
            if (doc.InnerText != null)
            {
                XmlNode nodePremium = doc.SelectSingleNode("Product/PolicyHeader/PolicyPremiums/FullTermPremiums");

                if (nodePremium != null)
                {
                    XmlNode currentNode = nodePremium.SelectSingleNode("AssessmentAmount");
                    if (currentNode != null)
                    {
                        BasePremium = double.Parse(currentNode.InnerText.ToString());
                        strBasePremium = BasePremium.ToString();
                    }

                    currentNode = nodePremium.SelectSingleNode("StampDutyAmount");
                    if (currentNode != null)
                    {
                        GSTAmount = double.Parse(currentNode.InnerText.ToString());
                        strGSTAmount = GSTAmount.ToString();
                    }

                    currentNode = nodePremium.SelectSingleNode("IntermediaryDiscountAmount");
                    if (currentNode != null)
                    {
                        DemeritDisc = double.Parse(currentNode.InnerText.ToString());
                        strDemeritDisc = DemeritDisc.ToString();
                    }

                }
            }
            //Added by kuldeep to override billing info premium over erate calculated premium(21-feb-2012)
            DataSet ds = new DataSet();
            Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQQDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();
            ds = objQQDetails.FetchBillingInfoPRemiumDetails(customerID, PolicyID, PolicyVersionID);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    BasePremium = double.Parse(ds.Tables[0].Rows[0]["GROSS_PREMIUM"].ToString());
                    strBasePremium = BasePremium.ToString();

                    GSTAmount = double.Parse(ds.Tables[0].Rows[0]["GST"].ToString());
                    strGSTAmount = GSTAmount.ToString();

                    //DemeritDisc = double.Parse(ds.Tables[0].Rows[0]["DEMERIT_DISC_AMT"].ToString());
                   // if (DemeritDisc != 0)
                   // {
                        DemeritDisc = BasePremium * 5 / 100;
                   // }
                    strDemeritDisc = DemeritDisc.ToString();

                }
            }
           
            


            strTotalPremium = BasePremium - DemeritDisc + GSTAmount;
            return strTotalPremium.ToString();
        }

        private string ShowCombinedPremium(string strPremium)
        {
            try
            {
                if (strPremium.Trim() != "")
                {
                    XmlDocument objXmlDocument = new XmlDocument();
                    strPremium = strPremium.Replace("\t", "");
                    strPremium = strPremium.Replace("\r\n", "");
                    strPremium = strPremium.Replace("<LINK", "<!-- <LINK");
                    //strPremium= strPremium.Replace( " rel=\"stylesheet\"> ","rel=\"stylesheet\" />");
                    strPremium = strPremium.Replace("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-16\">", "");
                    strPremium = strPremium.Replace("class=\"clsButton\">", "class=\"clsButton\" />");
                    strPremium = strPremium.Replace(" charset=utf-16\"> ", " charset=utf-16\" /> ");
                    strPremium = "<OUTPUTXML>" + strPremium + "</OUTPUTXML>";
                    objXmlDocument.LoadXml(strPremium);

                    XmlNodeList nodLst = objXmlDocument.GetElementsByTagName("returnHome");
                    // get total home premium
                    int intTotalHomeBoatPrm = 0;

                    if (nodLst != null && nodLst.Count > 0)
                    {
                        intTotalHomeBoatPrm = int.Parse(nodLst.Item(0).InnerText);
                    }
                    // get total boat premium
                    nodLst = objXmlDocument.GetElementsByTagName("returnBoat");
                    if (nodLst != null && nodLst.Count > 0)
                    {
                        intTotalHomeBoatPrm = intTotalHomeBoatPrm + int.Parse(nodLst.Item(0).InnerText);

                    }
                    //strPremium= strPremium.Replace( "<!-- <LINK" ,"<LINK");	
                    //strPremium= strPremium.Replace( " rel=\"stylesheet/\"> --> "," rel=\"stylesheet\"> ");


                    strPremium.Replace("</OUTPUT>", "<TR> <td class='midcolora'>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1887") + " = + intTotalHomeBoatPrm + </td></TR> </OUTPUT>");//Total Premium
                }

                return strPremium;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns> </returns>	
        private string BoatSection()
        {
            string strBoat = "";
            appLobID = LOB_WATERCRAFT;
            ClsGenerateQuote objGenerateQuote = new ClsGenerateQuote(CarrierSystemID);
            DataSet dsTemp = objGenerateQuote.FetchQuote(customerID, quoteId, appID, appVersionID);
            if (dsTemp != null && dsTemp.Tables[0] != null)
            {
                string quoteXML = dsTemp.Tables[0].Rows[1]["QUOTE_XML"].ToString();
                strBoat = ShowPremium(quoteXML);
            }
            return strBoat;
        }



        /// <summary>
        /// chk for Boat attached to home or not
        /// </summary>
        /// <returns>true if exists else false</returns>
        private bool CheckHome_Boat()
        {
            bool blResult = false;
            try
            {
                ClsGenerateQuote objGenerateQuote = new ClsGenerateQuote(CarrierSystemID);
                DataSet dsTemp = objGenerateQuote.FetchQuote(customerID, quoteId, appID, appVersionID);
                if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 1)
                {
                    string quote_type = dsTemp.Tables[0].Rows[1]["QUOTE_TYPE"].ToString();
                    if (quote_type == "HOME-BOAT")
                        blResult = true;
                }
                return blResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        /// <summary>
        /// Get query string 
        /// </summary>
        private void GetQueryString()
        {
            if (Request.QueryString["CUSTOMER_ID"] != null)
            {
                customerID = int.Parse(Request.QueryString["CUSTOMER_ID"].ToString());
            }
            if (Request.QueryString["APP_ID"] != null)
            {
                appID = int.Parse(Request.QueryString["APP_ID"].ToString());
            }
            if (Request.QueryString["APP_VERSION_ID"] != null)
            {
                appVersionID = int.Parse(Request.QueryString["APP_VERSION_ID"].ToString());
            }
            if (Request.QueryString["QUOTE_ID"] != null)
            {
                quoteId = int.Parse(Request.QueryString["QUOTE_ID"].ToString());
            }
            if (Request.QueryString["QQ_ID"] != null)
            {
                qqId = int.Parse(Request.QueryString["QQ_ID"].ToString());
            }
            // SHOW 
            if (Request.QueryString["SHOW"] != null)
            {
                showDetails = int.Parse(Request.QueryString["SHOW"].ToString());
            }
            if (Request.QueryString["SHOWRATE"] != null)
            {
                showRateDetails = Request.QueryString["SHOWRATE"].ToString();
            }
            if (Request.QueryString["LOBID"] != null)
            {
                appLobID = Request.QueryString["LOBID"].ToString();
            }
            if (Request.QueryString["POLICY_ID"] != null)
            {
                PolicyID = int.Parse(Request.QueryString["POLICY_ID"].ToString());
            }
            if (Request.QueryString["POLICY_VERSION_ID"] != null)
            {
                PolicyVersionID = int.Parse(Request.QueryString["POLICY_VERSION_ID"].ToString());
            }
            if (Request.QueryString["NEW_POLICY_VERSION_ID"] != null)
            {
                PolicyNewVersionID = int.Parse(Request.QueryString["NEW_POLICY_VERSION_ID"].ToString());
            }
            if (Request.QueryString["EFFECTIVE_DATETIME"] != null && Request.QueryString["EFFECTIVE_DATETIME"].ToString() != "0" && Request.QueryString["EFFECTIVE_DATETIME"].ToString() != "")
            {
                EFFECTIVE_DATE = Convert.ToDateTime(Request.QueryString["EFFECTIVE_DATETIME"].ToString());
            }
            if (Request.QueryString["EXPIRY_DATE"] != null && Request.QueryString["EXPIRY_DATE"].ToString() != "0" && Request.QueryString["EXPIRY_DATE"].ToString() != "")
            {
                EXPIRY_DATE = Convert.ToDateTime(Request.QueryString["EXPIRY_DATE"].ToString());
            }
            if (Request.QueryString["CALLEDFROM"] != null)
            {
                strCalledFrom = Request.QueryString["CALLEDFROM"].ToString();
            }
        }
        public string ReplaceChar(string input, char c)
        {
            int cnt = 0;
            foreach (char s in input)
            {
                if (s == '&')
                {
                    if (input.IndexOf(input.Substring(cnt, 4)) > 0)
                    {
                        string test = "";
                        test = input.Substring(cnt, 4).ToString().ToLower();
                        if (input.Substring(cnt, 4).ToString().ToLower() != "&amp" && (input.Substring(cnt, 1).ToString() == "&"))
                            input = input.Remove(cnt, 1).Insert(cnt, "&amp;");
                    }
                }
                cnt++;
            }
            return input;
        }
        private void setcaption()
        {
            header = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2071");
        }

    }
}
