/****************************************************************************************** 
<Author					: -		nidhi and shrikant
<Start Date				: -		7/8/2005 11:38:09 AM
<End Date				: -	
<Description			: - 	business class for quote section
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: august 31,2005
<Modified By			: nidhi
<Purpose				: added methods VelidInputXML(),GetQuote() 

<Modified Date			: september 21,2005
<Modified By			: Shrikant Bhatt
<Purpose				: modify the GetHO3QuoteXMLForQuickQuote() function as per the changes in the rating logic

<Modified Date			: september 22,2005
<Modified By			: Shrikant Bhatt
<Purpose				: added methods GetProductFactorMasterPath()

<Modified Date			: Oct. 05,2005
<Modified By			: Ashwani 
<Purpose				: added  methods GetQuoteXML()

<Modified Date			: Nov. 23,2005
<Modified By			: Shrikant and Praveen 
<Purpose				: added  method GetWatercraftQuoteXMLForQuickQuote()

<Modified Date			: Nov. 25,2005
<Modified By			: Shrikant  
<Purpose				: added  method GetMotorcycleQuoteXMLForQuickQuote()

<Modified Date			: Dec. 07,2005
<Modified By			: Shrikant  
<Purpose				: added  method GetProductPremiumPath()

<Modified Date			: Dec. 07,2005
<Modified By			: Shrikant 
<Purpose				: Modified  method GetMotorcycleQuoteXMLForQuickQuote()

<Modified Date			: Dec. 08,2005
<Modified By			: Shrikant 
<Purpose				: Modified  method GetWatercraftQuoteXMLForQuickQuote()

<Modified Date			: Dec. 08,2005
<Modified By			: Shrikant 
<Purpose				: Modified  method GetRentaDewllingForQuickQuote()

<Modified Date			: Dec. 08,2005
<Modified By			: Shrikant 
<Purpose				: Modified  method GetHO3QuoteXMLForQuickQuote()

<Modified Date			: Jan. 2,2006
<Modified By			: Shrikant 
<Purpose				: Method GetPremiumFromXML() Created

<Modified Date			: Jan. 4,2006
<Modified By			: Shrikant 
<Purpose				: Modified  method GetQuoteXMLForPolicy()

<Modified Date			: Jan. 6,2006
<Modified By			: Shrikant 
<Purpose				: Modified  method GetPremiumFromXML()


<Modified Date			: Feb. 28,2006
<Modified By			: Ashwani 
<Purpose				: 

<Modified Date			: Nov. 21,2006
<Modified By			: Nidhi
<Purpose				: capital rating comparison
<temp test>
*******************************************************************************************/
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using Cms.BusinessLayer.BlQuote;
using Cms.Model.Quote;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using System.IO;
using Cms.BusinessLayer.BlApplication;
using Cms;
using System.Globalization;



namespace Cms.BusinessLayer.BlQuote
{
    enum LOBType
    {
        HO = 1,
        AUTO = 2,
        MOTERCYCLE = 3,
        RENTALDEWLLING = 4,
        WATERCRAFT = 5
    }

    /// <summary>
    /// business class for quote section
    /// </summary>
    public class ClsGenerateQuote : clsquote
    {
        private const string QOT_CUSTOMER_QUOTE_LIST = "QOT_CUSTOMER_QUOTE_LIST";
        private const string ACTIVATE_DEACTIVATE_PROC = "";
        private const string strRisk = "RISK";
        private const string strType = "TYPE";



        private string mSystemID;

        #region Private Instance Variables
        private bool boolTransactionLog;
        //private int QUOTE_ID;
        private const string LOB_HOME = "1";
        private const string LOB_PRIVATE_PASSENGER = "2";
        private const string LOB_MOTORCYCLE = "3";
        private const string LOB_WATERCRAFT = "4";
        private const string LOB_UMBRELLA = "5";
        private const string LOB_RENTAL_DWELLING = "6";
        private const string LOB_AVIATION = "8";

        private const string LOB_HOME_CODE = "HOME";
        private const string LOB_PRIVATE_PASSENGER_CODE = "AUTOP";
        private const string LOB_MOTORCYCLE_CODE = "CYCL";
        private const string LOB_WATERCRAFT_CODE = "BOAT";
        private const string LOB_UMBRELLA_CODE = "UMB";
        private const string LOB_RENTAL_DWELLING_CODE = "REDW";

        public const string POLICY_STATUS_RENEWAL_SUSPENSE = "RSUSPENSE";
        //public const string POLICY_STATUS_UNDER_REWRITE = "UREWRITE";
        public const string POLICY_STATUS_SUSPENSE_ENDORSEMENT = "ESUSPENSE";
        //public const string POLICY_STATUS_SUSPENSE_REWRITE = "REWRTSUSP";
        public const string POLICY_STATUS_UNDER_REVERT = "UREVERT";
        public const string POLICY_STATUS_SUSPENSE_CANCEL = "SCANCEL";
        public const string POLICY_STATUS_UNDER_CANCEL = "UCANCL";
        public const string POLICY_STATUS_CANCELLED = "CANCEL";
        public const string POLICY_STATUS_MARKED_REINSTATE = "MREINSTATE";
        public const string POLICY_STATUS_UNDER_REINSTATE = "UREINST";
        //public const string POLICY_STATUS_APPLICATION = "APPLICATION";  //Added By Lalit For Aplication

        /*Added By Lalit For New Qoute Genrate */
        public const string POLICY_STATUS_NBS_INPROGRESS = "New Business";
        public const string POLICY_STATUS_NBS_COMMIT = "Commit New Business";
        public const string POLICY_STATUS_END_INPROGRESS = "Endorsement in progress";
        public const string POLICY_STATUS_END_COMMIT = "Commit Endorsement Process";
        public const string POLICY_STATUS_REN_INPROGRESS = "Renewal";
        public const string POLICY_STATUS_REN_COMMIT = "Commit Renewal Process";
        //end


        string QUOTE_DATE = "";
        string QUOTE_EFFECTIVE_DATE = "";
        string RATE_EFFECTIVE_DATE = "";
        string NEW_BUSINESS = "";
        //string RENEWAL="";
        string BUSINESS_TYPE;
        string strStateName = "";
        string strRateEffectiveDate = "";
        string strBusinessType = "";




        #endregion

        #region Public Properties
        public bool TransactionLog
        {
            set
            {
                boolTransactionLog = value;
            }
            get
            {
                return boolTransactionLog;
            }
        }
        #endregion

        #region  Utility Functions

        /// <summary>
        /// Accepts InputXML and checks if the XML is well formed or not.
        /// Each node must have a value.
        /// </summary>
        /// <param name="inputXML">inputXML that has to be checked </param>
        /// <returns>Boolean</returns>
        public static bool ValidInputXML(string inputXML)
        {
            try
            {
                bool retVal = true;
                XmlDocument tempDoc = new XmlDocument();
                if ((!inputXML.StartsWith("<ERROR")) && (inputXML.Trim() != "<INPUTXML></INPUTXML>"))
                {
                    tempDoc.LoadXml("<INPUTXML>" + inputXML + "</INPUTXML>");
                    XmlElement tempElement = tempDoc.DocumentElement;
                    XmlNodeList tempNodes = tempElement.ChildNodes;
                    foreach (XmlNode nodTempNode in tempNodes)
                    {
                        foreach (XmlNode nodTempChild in nodTempNode.ChildNodes)
                        {
                            if (nodTempChild.InnerText.Trim() == "" || nodTempChild.InnerText.Trim() == "NULL")
                            {
                                retVal = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    retVal = false;
                }
                return retVal;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {

            }
        }

        /*public string GetUserId()
        {
            userSessionID = Session["userId"].ToString();
            DataSet ldsStatus = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetUserLoggedStatus " + int.Parse(userSessionID.ToString()));
            if(ldsStatus!=null && ldsStatus.Tables[0].Rows.Count>0)
            {
                strSessionID = ldsStatus.Tables[0].Rows[0]["SESSION_ID"].ToString();

            }
            return Session["userId"].ToString(); 
        }*/
        /// <summary>
        /// Accepts customerID and quoteID and returns the policy quote html
        /// </summary>
        /// <param name="customerID">Customer ID</param>
        /// <param name="quoteID">quoteId of the Quote</param>
        /// <returns>Dataset</returns>
        public DataSet FetchQuote_Pol(int customerId, int quoteId, int policyID, int polVersionID, string calledFrom)
        {
            string strStoredProc = "Proc_CustomerQuoteInfo_Pol";
            DataSet dsQuote = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerId, SqlDbType.Int);
                objDataWrapper.AddParameter("@QUOTE_ID", quoteId, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", policyID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", polVersionID, SqlDbType.Int);
                objDataWrapper.AddParameter("@CALLED_FROM", calledFrom, SqlDbType.NVarChar);

                dsQuote = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsQuote;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }

        public DataSet FetchQuote_Pol(int customerId, int quoteId, int policyID, int polVersionID)
        {
            return FetchQuote_Pol(customerId, quoteId, policyID, polVersionID, "");

        }


        /// <summary>
        /// Accepts customerID and quoteID and returns the quote html
        /// </summary>
        /// <param name="customerID">Customer ID</param>
        /// <param name="quoteID">quoteId of the Quote</param>
        /// <returns>Dataset</returns>
        public DataSet FetchQuote(int customerId, int quoteId, int appID, int appVersionID)
        {
            string strStoredProc = "Proc_CustomerQuoteInfo";
            DataSet dsQuote = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerId, SqlDbType.Int);
                objDataWrapper.AddParameter("@QUOTE_ID", quoteId, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID", appID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID, SqlDbType.Int);
                dsQuote = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsQuote;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }

        //Asfa Praveen - 22-June-2007
        public DataSet FetchAppOldInputXML(int customerId, int appID, int appVersionID)
        {
            string strStoredProc = "Proc_GetAppOldInputXML";
            DataSet dsQuote = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerId, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID", appID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID, SqlDbType.Int);
                dsQuote = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsQuote;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }
        //Asfa Praveen - 22-June-2007
        public DataSet FetchPolicyOldInputXML(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetPolicyOldInputXML";
            DataSet dsQuote = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                dsQuote = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsQuote;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }

        /// <summary>
        /// Accepts customerID and quoteID and returns the quote html
        /// </summary>
        /// <param name="customerID">Customer ID</param>
        /// <param name="quoteID">quoteId of the Quote</param>
        /// <returns>Dataset</returns>
        public DataSet FetchQuote(int customerId, int quoteId)
        {
            string strStoredProc = "Proc_CustomerQuickQuoteInfo";
            DataSet dsQuote = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerId, SqlDbType.Int);
                objDataWrapper.AddParameter("@QUOTE_ID", quoteId, SqlDbType.Int);
                dsQuote = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsQuote;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }

        /// <summary>
        /// Accepts inputXML
        /// Returns an xmlstring 
        /// </summary>
        /// <param name="strInputXML">strInputXML as inputXML </param>
        /// <param name="oLobId">oLobId as LOB</param>
        /// <returns>string</returns>
        public string GetQuoteXML(string strInputXML, string intLobID)
        {
            try
            {
                string finalPremiumXML = "";
                strInputXML = strInputXML.Replace("\r", "");
                strInputXML = strInputXML.Replace("\n", "");
                strInputXML = strInputXML.Replace("&AMP;", "&amp;");
                if (intLobID != "0")
                {
                    // switch case on the basis of the lob
                    switch (intLobID)
                    {
                        case LOB_HOME:
                            finalPremiumXML = GetHO3QuoteXMLForQuickQuote(strInputXML);
                            break;
                        case LOB_PRIVATE_PASSENGER:
                            finalPremiumXML = GetAutoQuoteXMLForQuickQuote(strInputXML);
                            break;
                        case LOB_MOTORCYCLE:
                            finalPremiumXML = GetMotorcycleQuoteXMLForQuickQuote(strInputXML);
                            break;
                        case LOB_WATERCRAFT:
                            finalPremiumXML = GetWatercraftQuoteXMLForQuickQuote(strInputXML);
                            break;
                        case LOB_RENTAL_DWELLING:
                            finalPremiumXML = GetRentaDewllingForQuickQuote(strInputXML);
                            break;
                        case LOB_UMBRELLA:
                            finalPremiumXML = GetUmbrelllaQuoteXmlForQuickQuote(strInputXML);
                            break;
                        case LOB_AVIATION:
                            finalPremiumXML = GetAviationQuoteXMLForQuickQuote(strInputXML);
                            break;
                        default:
                            finalPremiumXML = GetRateXMLForInsert(strInputXML, 1); // for Other Products
                            break;
                    }

                }
                else
                    finalPremiumXML = "";
                return finalPremiumXML;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }


        public string GetQuoteXMLForPolicy(string strInputXML, string intLobID)
        {
            try
            {
                string finalPremiumXML = "";
                strInputXML = strInputXML.Replace("\r", "");
                strInputXML = strInputXML.Replace("\n", "");
                if (intLobID != "0")
                {
                    // switch case on the basis of the lob
                    switch (intLobID)
                    {
                        case LOB_HOME:
                            finalPremiumXML = GetHO3QuoteXMLForQuickQuote(strInputXML);
                            break;
                        case LOB_PRIVATE_PASSENGER:
                            finalPremiumXML = GetAutoQuoteXMLForQuickQuote(strInputXML);
                            break;
                        case LOB_MOTORCYCLE:
                            finalPremiumXML = (strInputXML);
                            break;
                        case LOB_WATERCRAFT:
                            finalPremiumXML = "";
                            break;
                        case LOB_RENTAL_DWELLING:
                            finalPremiumXML = "";
                            break;
                        case LOB_UMBRELLA:
                            finalPremiumXML = "";
                            break;
                        default:
                            break;
                    }

                }
                else
                    finalPremiumXML = "";
                return finalPremiumXML;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }




        /// <summary>
        /// Accepts inputXML
        /// Returns an xmlstring 
        /// </summary>
        /// <param name="customerID">inputXML that has to be checked </param>
        /// <param name="inputXML">inputXML that has to be checked </param>
        /// <returns>string</returns>
        public string GetQuoteXML(int Customer_ID, int App_ID, int App_Version_ID)
        {
            try
            {
                string finalPremiumXML = "";
                string inputXML = "", premiumXML = "";
                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                inputXML = objGeneralInformation.GetInputXML(Customer_ID, App_ID, App_Version_ID);

                if (inputXML != "<NewDataSet />" && inputXML != "")
                {
                    /* strReturnXML is of the form <INPUTXML><DWELLLINGDETAILS> ... </DWELLLINGDETAILS></INPUTXML>
                     * 1.  Run a loop to calculate the premium for each dwelling. 
                     * 2.  Save the output xml in another xmlstring. 
                     * 3.  add the <DWELLLINGDETAILS> tags for each premium.
                     * 4.  add the <PREMIUMXML> as the root node.
                     */


                    string combinedPremiumComponent = "";

                    XmlDocument xmlTempDoc = new XmlDocument();

                    inputXML = "<INPUTXML>" + inputXML + "</INPUTXML>";
                    xmlTempDoc.LoadXml(inputXML);
                    XmlElement xmlTempElement = xmlTempDoc.DocumentElement;

                    finalPremiumXML = "<PREMIUMXML>";
                    //foreach (XmlNode nodChildNode in xmlTempElement.ChildNodes)
                    //{	
                    foreach (XmlNode nodInput in xmlTempElement.ChildNodes)
                    {
                        string premiumComponent = "<";
                        premiumComponent = premiumComponent + nodInput.Name.Trim() + " " + nodInput.Attributes[0].Name.Trim() + " = '" + nodInput.Attributes[0].Value.Trim() + "' " + nodInput.Attributes[1].Name.Trim() + " = '" + nodInput.Attributes[1].Value.Trim() + "' >";

                        string premiumInput = premiumComponent + nodInput.InnerXml + "</" + nodInput.Name + ">";
                        string premiumXslPath = "", inputXSLPath = "";
                        premiumXslPath = ClsCommon.GetKeyValueWithIP("PremiumXSL");//System.Configuration.ConfigurationSettings.AppSettings.Get("PremiumXSL").ToString();
                        inputXSLPath = ClsCommon.GetKeyValueWithIP("InputXSL");//System.Configuration.ConfigurationSettings.AppSettings.Get("InputXSL").ToString();

                        QEngineDll.QuoteEngine strGetPath = new QEngineDll.QuoteEngine(premiumInput, premiumXslPath, inputXSLPath);
                        string shortestPath = strGetPath.GetPath();
                        premiumXML = strGetPath.CalculatePremium(shortestPath.Trim());
                        XmlDocument xmlTempPremiumXML = new XmlDocument();
                        xmlTempPremiumXML.LoadXml(premiumXML);
                        XmlElement xmlTempPremiumElement = xmlTempPremiumXML.DocumentElement;
                        premiumComponent = premiumComponent + xmlTempPremiumElement.InnerXml + "</" + nodInput.Name + ">";
                        combinedPremiumComponent = combinedPremiumComponent + premiumComponent;
                    }
                    finalPremiumXML = finalPremiumXML + combinedPremiumComponent + "</PREMIUMXML>";
                    //}
                    //--------------------------------------------------------------------------------------------
                }
                return finalPremiumXML;

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }


        //Ravindra (01/08/2010): obselete method not in use
        #region Commented

        //
        //		public string GetProductPremiumPath_old(string InputXML,string SelectLOB)
        //		{
        //			
        //			
        //			try
        //			{
        //				string strReturnVal="";
        //				//-----------------------------------------------------------------------------
        //				XmlDocument doc1		=	 new XmlDocument();
        //				//For Transforming the .xsl and .xml files
        //				XslTransform xslt		=	 new XslTransform();
        //				// Load the xml into a document object
        //				XmlDocument XmlDoc		=	 new XmlDocument();
        //				XmlDocument XmlDocINPUT =	 new XmlDocument();
        //				//-----------------------------------------------------------------------------
        //				XmlDocument xmlCheckProductName = new XmlDocument();
        //				xmlCheckProductName.LoadXml(InputXML);
        //
        //				if (strStateName == "")
        //				{
        //					strStateName = xmlCheckProductName.GetElementsByTagName("STATENAME").Item(0).InnerText;
        //				}
        //
        //				//Get the QUOTEEFFDATE from the input
        //				XmlNodeList nodlstdate			=	xmlCheckProductName.GetElementsByTagName("QUOTEEFFDATE");
        //				XmlNodeList nodDateList;
        //				string strLOBType="";
        //				if(nodlstdate!=null)
        //				{
        //					string strQEffectiveDate		=  nodlstdate.Item(0).InnerText;
        //					QUOTE_EFFECTIVE_DATE = strQEffectiveDate;
        //					
        //					string strDateXML="" ;
        //					DateTime dtEffectiveDate = DateTime.Parse(strQEffectiveDate);
        //			
        //					//Fetch all dates and their details from the productfactordatemaster xml
        //					string strProductFactorPath = "";
        //
        //					//
        //					
        //					
        //					strProductFactorPath = ClsCommon.GetKeyValueWithIP("ProductPremiumDetailsPath");//ConfigurationSettings.AppSettings.Get("ProductFactorMasterDetailsPath").ToString();
        //					
        //					
        //					xmlCheckProductName.Load( strProductFactorPath );			
        //					XmlDocument xmlProductFactorPath = new XmlDocument();
        //
        //					if(SelectLOB == "HO" )
        //					{
        //						
        //						
        //						//nodDateList		=	xmlCheckProductName.GetElementsByTagName("HO");
        //						nodDateList		=	xmlCheckProductName.SelectNodes("MAIN/HO/ATTRIBUTE[@STATE='" + strStateName + "']");
        //						strDateXML		=	nodDateList.Item(0).InnerXml;
        //						strLOBType		=	"HO";	
        //					
        //					}
        //					else if(SelectLOB == "AUTOP" )
        //					{
        //						
        //						//nodDateList		=	xmlCheckProductName.GetElementsByTagName("AUTOP");
        //						nodDateList		=	xmlCheckProductName.SelectNodes("MAIN/AUTOP/ATTRIBUTE[@STATE='" + strStateName + "']");
        //
        //						strDateXML		=	nodDateList.Item(0).InnerXml;
        //						strLOBType		=	"AUTOP";	
        //					
        //					}
        //	
        //					else if(SelectLOB == "AUTOC" )
        //					{
        //						
        //						//nodDateList		=	xmlCheckProductName.GetElementsByTagName("AUTOC");
        //						nodDateList		=	xmlCheckProductName.SelectNodes("MAIN/AUTOC/ATTRIBUTE[@STATE='" + strStateName + "']");
        //
        //						strDateXML		=	nodDateList.Item(0).InnerXml;
        //						strLOBType		=	"AUTOC";	
        //					
        //					}
        //
        //
        //					else if(SelectLOB == "REDW" )
        //					{
        //						
        //						//nodDateList		=	xmlCheckProductName.GetElementsByTagName("REDW/" + strStateName);
        //						//	nodDateList=	xmlCheckProductName.SelectNodes("MAIN/REDW/" + strStateName);
        //						nodDateList		=	xmlCheckProductName.SelectNodes("MAIN/REDW/ATTRIBUTE[@STATE='" + strStateName + "']");
        //
        //						strDateXML		=	nodDateList.Item(0).InnerXml;
        //						 
        //
        //						strLOBType		=	"REDW";	
        //					
        //					}
        //					else if(SelectLOB == "BOAT" )
        //					{
        //						
        //						//nodDateList		=	xmlCheckProductName.GetElementsByTagName("BOAT");
        //						nodDateList		=	xmlCheckProductName.SelectNodes("MAIN/BOAT/ATTRIBUTE[@STATE='" + strStateName + "']");
        //
        //						strDateXML		=	nodDateList.Item(0).InnerXml;
        //						strLOBType		=	"BOAT";	
        //					
        //					}
        //					else if(SelectLOB == "CYCL" )
        //					{
        //						
        //						//nodDateList		=	xmlCheckProductName.GetElementsByTagName("CYCL");
        //						nodDateList		=	xmlCheckProductName.SelectNodes("MAIN/CYCL/ATTRIBUTE[@STATE='" + strStateName + "']");
        //
        //						strDateXML		=	nodDateList.Item(0).InnerXml;
        //						strLOBType		=	"CYCL";	
        //					
        //					}
        //										
        //					xmlProductFactorPath.LoadXml("<" + strLOBType + ">" + strDateXML + "</" + strLOBType + ">");
        //					nodDateList		=	xmlProductFactorPath.GetElementsByTagName("DATE");
        //
        //					DateTime dtTempDate;
        //					
        //					
        //					//compare quote date from inputxml against each date fetched from master xml
        //					string filePath="";
        //					if(nodDateList.Count >0)
        //					{
        //						
        //						foreach (XmlNode nodTempDate in nodDateList)								
        //						{					
        //							if (nodTempDate.InnerText !="")
        //							{
        //
        //								/* Check if it is the first node.
        //								 * If it is the first node and the date passed is less than the date fetched in the first node then take the first node
        //								 * Check if it is the last node.
        //								 * If it is the last node and the date passed is greater than or equal to the date fetched in the last node then take hte last node.
        //								 * Else check the duration and use accordingly */
        //
        //
        //								dtTempDate = DateTime.Parse(nodTempDate.InnerText);
        //								int iTemp= DateTime.Compare(dtEffectiveDate,dtTempDate);
        //							
        //								if(nodTempDate.NextSibling ==null) //last node
        //								{
        //									if(iTemp >=0)
        //									{
        //										filePath= "<EDATE>"+nodTempDate.InnerText+"</EDATE>";
        //										break;
        //									}	
        //									else
        //									{
        //										filePath= "<EDATE>"+nodTempDate.PreviousSibling.InnerText+"</EDATE>";
        //										break;
        //									}
        //
        //								}
        //								else if (nodTempDate.PreviousSibling ==null) // First node
        //								{
        //									if(iTemp <0)
        //									{
        //										filePath= "<EDATE>"+nodTempDate.InnerText+"</EDATE>";
        //										break;
        //									}	
        //								}
        //								else
        //								{
        //									DateTime dtTempNextDate = DateTime.Parse(nodTempDate.NextSibling.InnerText);
        //									int nxtTemp = DateTime.Compare(dtEffectiveDate,dtTempNextDate);
        //									if(iTemp >0 && nxtTemp <0)
        //									{
        //										filePath= "<EDATE>"+nodTempDate.InnerText+"</EDATE>";
        //										break;
        //									}
        //								}
        //							}
        //						}
        //
        //					}
        //					
        //					string strProductFactorsMasterPath	="";
        //					if(SelectLOB == "HO" )
        //					{
        //						strProductFactorsMasterPath			=	ClsCommon.GetKeyValueWithIP("ProductPremiumrPath_HO"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath").ToString();
        //						InputXML = InputXML.Replace("<LOB_ID>",filePath+"<LOBNAME>HO</LOBNAME><LOB_ID>");
        //					}
        //					else if(SelectLOB == "AUTOP" || SelectLOB == "AUTOC")
        //					{
        //						strProductFactorsMasterPath			=	ClsCommon.GetKeyValueWithIP("ProductPremium_Auto"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
        //						InputXML = InputXML.Replace("<QUICKQUOTE>","<QUICKQUOTE>" + filePath + "<LOBNAME>AUTO</LOBNAME>");
        //					}
        //
        //
        //				 
        //
        //					else if(SelectLOB == "REDW" )
        //					{
        //						strProductFactorsMasterPath			=	ClsCommon.GetKeyValueWithIP("ProductPremium_REDW"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
        //						InputXML = InputXML.Replace("<LOB_ID>",filePath+"<LOBNAME>REDW</LOBNAME><LOB_ID>");
        //					}
        //					else if(SelectLOB == "BOAT" )
        //					{
        //						strProductFactorsMasterPath			=	ClsCommon.GetKeyValueWithIP("ProductPremium_BOAT"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
        //						InputXML = InputXML.Replace("<QUICKQUOTE>","<QUICKQUOTE>" + filePath + "<LOBNAME>BOAT</LOBNAME>");
        //					}
        //					else if(SelectLOB == "CYCL" )
        //					{
        //						strProductFactorsMasterPath			=	ClsCommon.GetKeyValueWithIP("ProductPremium_CYCL"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
        //						InputXML = InputXML.Replace("<QUICKQUOTE>","<QUICKQUOTE>" + filePath + "<LOBNAME>CYCL</LOBNAME>");
        //					}
        //					xslt.Load(strProductFactorsMasterPath);
        //					//merge the inputxml with XSL. to get the filepath			
        //					
        //					// Transform the file and output an HTML string.
        //						
        //					InputXML=InputXML.Replace("<INPUTXML>","");
        //					InputXML=InputXML.Replace("</INPUTXML>","");
        //					InputXML= "<INPUTXML>"+InputXML+"</INPUTXML>";
        //
        //					StringWriter writer = new StringWriter();
        //					XmlDocument xmlDocTemp = new XmlDocument();
        //					xmlDocTemp.LoadXml(InputXML); 
        //					XPathNavigator nav = ((IXPathNavigable) xmlDocTemp).CreateNavigator();
        //
        //					xslt.Transform(nav,null,writer);
        //		
        //					string XMLoutput = writer.ToString();
        //
        //					XmlDoc.LoadXml(XMLoutput);
        //			
        //			
        //					//Fetch Product Factor Path
        //					XmlNodeList nodlst;
        //					nodlst					=	XmlDoc.GetElementsByTagName("FACTORMASTERPATH");
        //			
        //					//Final Product Factor Master Name
        //					strReturnVal			=	nodlst.Item(0).InnerText;
        //			
        //					strReturnVal.Replace("\t","");
        //					strReturnVal.Replace("\r","");
        //					strReturnVal.Replace("\n","");
        //					string strPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetApplicationPath();
        //					strReturnVal = strPath + strReturnVal;
        //
        //					writer.Close();
        //				}
        //				return strReturnVal;
        //			
        //			}
        //			catch(Exception exc)
        //			{
        //				throw(exc);
        //			}
        //			finally
        //			{}
        //
        //
        //		}
        //
        //		public string GetProductFactorMasterPath_old(string InputXML,string SelectLOB)
        //		{
        //			
        //			
        //			try
        //			{
        //				string strReturnVal="";
        //				//-----------------------------------------------------------------------------
        //				XmlDocument doc1		=	 new XmlDocument();
        //				//For Transforming the .xsl and .xml files
        //				XslTransform xslt		=	 new XslTransform();
        //				// Load the xml into a document object
        //				XmlDocument XmlDoc		=	 new XmlDocument();
        //				XmlDocument XmlDocINPUT =	 new XmlDocument();
        //				//-----------------------------------------------------------------------------
        //				XmlDocument xmlCheckProductName = new XmlDocument();
        //				xmlCheckProductName.LoadXml(InputXML);
        //
        //
        //				strStateName = xmlCheckProductName.GetElementsByTagName("STATENAME").Item(0).InnerText;
        //
        //				//Get the QUOTEEFFDATE from the input
        //				XmlNodeList nodlstdate			=	xmlCheckProductName.GetElementsByTagName("QUOTEEFFDATE");
        //				XmlNodeList nodDateList;
        //				string strLOBType="";
        //				if(nodlstdate!=null)
        //				{
        //					string strQEffectiveDate		=  nodlstdate.Item(0).InnerText;
        //					
        //					string strDateXML="" ;
        //					DateTime dtEffectiveDate = DateTime.Parse(strQEffectiveDate);
        //			
        //					//Fetch all dates and their details from the productfactordatemaster xml
        //					string strProductFactorPath = "";
        //
        //					//
        //					
        //					if(SelectLOB == "HO" )
        //					{
        //						strProductFactorPath = ClsCommon.GetKeyValueWithIP("ProductFactorMasterDetailsPath"); //ConfigurationSettings.AppSettings.Get("ProductFactorMasterDetailsPath").ToString();
        //					}
        //						//to pick the "productfactorpathdetail"  file for both type of auto	
        //					else if(SelectLOB == "AUTOP" || SelectLOB == "AUTOC" )
        //					{
        //						strProductFactorPath = ClsCommon.GetKeyValueWithIP("ProductFactorMasterDetailsPath");//ConfigurationSettings.AppSettings.Get("ProductFactorMasterDetailsPath").ToString();
        //					}
        //
        //					else if(SelectLOB == "REDW" )
        //					{
        //						strProductFactorPath = ClsCommon.GetKeyValueWithIP("ProductFactorMasterDetailsPath");//ConfigurationSettings.AppSettings.Get("ProductFactorMasterDetailsPath").ToString();
        //					}
        //					else if(SelectLOB == "BOAT" )
        //					{
        //						strProductFactorPath = ClsCommon.GetKeyValueWithIP("ProductFactorMasterDetailsPath");//ConfigurationSettings.AppSettings.Get("ProductFactorMasterDetailsPath").ToString();
        //					}
        //					else if(SelectLOB == "CYCL" )
        //					{
        //						strProductFactorPath = ClsCommon.GetKeyValueWithIP("ProductFactorMasterDetailsPath");//ConfigurationSettings.AppSettings.Get("ProductFactorMasterDetailsPath").ToString();
        //					}
        //					else if(SelectLOB == "RV" )
        //					{
        //						strProductFactorPath = ClsCommon.GetKeyValueWithIP("ProductFactorMasterDetailsPath");//ConfigurationSettings.AppSettings.Get("ProductFactorMasterDetailsPath").ToString();
        //					}
        //					else if(SelectLOB == "UMB" )
        //					{
        //						strProductFactorPath = ClsCommon.GetKeyValueWithIP("ProductFactorMasterDetailsPath");//ConfigurationSettings.AppSettings.Get("ProductFactorMasterDetailsPath").ToString();
        //					}
        //					xmlCheckProductName.Load( strProductFactorPath );			
        //					XmlDocument xmlProductFactorPath = new XmlDocument();
        //
        //					if(SelectLOB == "HO" )
        //					{
        //						
        //						
        //						//nodDateList		=	xmlCheckProductName.GetElementsByTagName("HO");
        //						nodDateList		=	xmlCheckProductName.SelectNodes("MAIN/HO/ATTRIBUTE[@STATE='" + strStateName + "']");
        //
        //						strDateXML		=	nodDateList.Item(0).InnerXml;
        //						strLOBType		=	"HO";	
        //					
        //					}
        //					else if(SelectLOB == "AUTOP" )
        //					{
        //						
        //						//nodDateList		=	xmlCheckProductName.GetElementsByTagName("AUTOP");
        //						nodDateList		=	xmlCheckProductName.SelectNodes("MAIN/AUTOP/ATTRIBUTE[@STATE='" + strStateName + "']");
        //
        //						strDateXML		=	nodDateList.Item(0).InnerXml;
        //						strLOBType		=	"AUTOP";	
        //					}
        //
        //					else if(SelectLOB == "AUTOC" )
        //					{
        //						
        //						//nodDateList		=	xmlCheckProductName.GetElementsByTagName("AUTOC");
        //						nodDateList		=	xmlCheckProductName.SelectNodes("MAIN/AUTOC/ATTRIBUTE[@STATE='" + strStateName + "']");
        //
        //						strDateXML		=	nodDateList.Item(0).InnerXml;
        //						strLOBType		=	"AUTOC";	
        //					
        //					}
        //
        //					else if(SelectLOB == "REDW" )
        //					{
        //						
        //						//nodDateList		=	xmlCheckProductName.SelectNodes("MAIN/REDW/ATTRIBUTE[id=" + strStateName);
        //						nodDateList		=	xmlCheckProductName.SelectNodes("MAIN/REDW/ATTRIBUTE[@STATE='" + strStateName + "']");
        //						strDateXML		=	nodDateList.Item(0).InnerXml;
        //						strLOBType		=	"REDW";	
        //					
        //					}
        //					else if(SelectLOB == "BOAT" )
        //					{
        //						
        //						//nodDateList		=	xmlCheckProductName.GetElementsByTagName("BOAT");
        //						nodDateList		=	xmlCheckProductName.SelectNodes("MAIN/BOAT/ATTRIBUTE[@STATE='" + strStateName + "']");
        //
        //						strDateXML		=	nodDateList.Item(0).InnerXml;
        //						strLOBType		=	"BOAT";	
        //					
        //					}
        //					else if(SelectLOB == "CYCL" )
        //					{
        //						
        //						//nodDateList		=	xmlCheckProductName.GetElementsByTagName("CYCL");
        //						nodDateList		=	xmlCheckProductName.SelectNodes("MAIN/CYCL/ATTRIBUTE[@STATE='" + strStateName + "']");
        //
        //						strDateXML		=	nodDateList.Item(0).InnerXml;
        //						strLOBType		=	"CYCL";	
        //					
        //					}
        //					else if(SelectLOB == "UMB" )
        //					{
        //						
        //						//nodDateList		=	xmlCheckProductName.GetElementsByTagName("CYCL");
        //						nodDateList		=	xmlCheckProductName.SelectNodes("MAIN/UMB/ATTRIBUTE[@STATE='" + strStateName + "']");
        //
        //						strDateXML		=	nodDateList.Item(0).InnerXml;
        //						strLOBType		=	"UMB";	
        //					
        //					}
        //					
        //					xmlProductFactorPath.LoadXml("<" + strLOBType + ">" + strDateXML + "</" + strLOBType + ">");
        //					nodDateList		=	xmlProductFactorPath.GetElementsByTagName("DATE");
        //
        //					DateTime dtTempDate;
        //					
        //					
        //					//compare quote date from inputxml against each date fetched from master xml
        //					string filePath="";
        //					if(nodDateList.Count >0)
        //					{
        //						
        //						foreach (XmlNode nodTempDate in nodDateList)								
        //						{					
        //							if (nodTempDate.InnerText !="")
        //							{
        //
        //								/* Check if it is the first node.
        //								 * If it is the first node and the date passed is less than the date fetched in the first node then take the first node
        //								 * Check if it is the last node.
        //								 * If it is the last node and the date passed is greater than or equal to the date fetched in the last node then take hte last node.
        //								 * Else check the duration and use accordingly */
        //
        //
        //								dtTempDate = DateTime.Parse(nodTempDate.InnerText);
        //								int iTemp= DateTime.Compare(dtEffectiveDate,dtTempDate);
        //							
        //								if(nodTempDate.NextSibling ==null) //last node
        //								{
        //									if(iTemp >=0)
        //									{
        //										filePath= "<EDATE>"+nodTempDate.InnerText+"</EDATE>";
        //										RATE_EFFECTIVE_DATE=nodTempDate.InnerText;
        //										break;
        //									}	
        //									else
        //									{
        //										filePath= "<EDATE>"+nodTempDate.PreviousSibling.InnerText+"</EDATE>";
        //										RATE_EFFECTIVE_DATE=nodTempDate.PreviousSibling.InnerText;
        //										break;
        //									}
        //
        //								}
        //								else if (nodTempDate.PreviousSibling ==null) // First node
        //								{
        //									if(iTemp <0)
        //									{
        //										filePath= "<EDATE>"+nodTempDate.InnerText+"</EDATE>";
        //										RATE_EFFECTIVE_DATE=nodTempDate.InnerText;
        //										break;
        //									}	
        //								}
        //								else
        //								{
        //									DateTime dtTempNextDate = DateTime.Parse(nodTempDate.NextSibling.InnerText);
        //									int nxtTemp = DateTime.Compare(dtEffectiveDate,dtTempNextDate);
        //									if(iTemp >0 && nxtTemp <0)
        //									{
        //										filePath= "<EDATE>"+nodTempDate.InnerText+"</EDATE>";
        //										RATE_EFFECTIVE_DATE=nodTempDate.InnerText;
        //										break;
        //									}
        //								}
        //							}
        //						}
        //
        //					}
        //					
        //					string strProductFactorsMasterPath	="";
        //					if(SelectLOB == "HO" )
        //					{
        //						strProductFactorsMasterPath			=	ClsCommon.GetKeyValueWithIP("ProductFactorsMasterPath"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath").ToString();
        //						InputXML = InputXML.Replace("<LOB_ID>",filePath+"<LOBNAME>HO</LOBNAME><LOB_ID>");
        //					}
        //					else if(SelectLOB == "AUTOP" || SelectLOB == "AUTOC")
        //					{
        //						strProductFactorsMasterPath			=	ClsCommon.GetKeyValueWithIP("ProductFactorsMasterPath_Auto"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
        //						InputXML = InputXML.Replace("<QUICKQUOTE>","<QUICKQUOTE>" + filePath + "<LOBNAME>AUTO</LOBNAME>");
        //					}
        //					else if(SelectLOB == "REDW" )
        //					{
        //						strProductFactorsMasterPath			=	ClsCommon.GetKeyValueWithIP("ProductFactorsMasterPath_REDW"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
        //						InputXML = InputXML.Replace("<LOB_ID>",filePath+"<LOBNAME>REDW</LOBNAME><LOB_ID>");
        //					}
        //					else if(SelectLOB == "BOAT" )
        //					{
        //						strProductFactorsMasterPath			=	ClsCommon.GetKeyValueWithIP("ProductFactorsMasterPath_BOAT"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
        //						InputXML = InputXML.Replace("<QUICKQUOTE>","<QUICKQUOTE>" + filePath + "<LOBNAME>BOAT</LOBNAME>");
        //					}
        //					else if(SelectLOB == "CYCL" )
        //					{
        //						strProductFactorsMasterPath			=	ClsCommon.GetKeyValueWithIP("ProductFactorsMasterPath_CYCL"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
        //						InputXML = InputXML.Replace("<QUICKQUOTE>","<QUICKQUOTE>" + filePath + "<LOBNAME>CYCL</LOBNAME>");
        //					}
        //					else if(SelectLOB == "UMB" )
        //					{
        //						strProductFactorsMasterPath			=	ClsCommon.GetKeyValueWithIP("ProductFactorsMasterPath_UMB"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
        //						InputXML = InputXML.Replace("<QUICKQUOTE>","<QUICKQUOTE>" + filePath + "<LOBNAME>UMB</LOBNAME>");
        //					}
        //
        //					xslt.Load(strProductFactorsMasterPath);
        //					//merge the inputxml with XSL. to get the filepath			
        //					
        //					// Transform the file and output an HTML string.
        //
        //					InputXML= "<INPUTXML>"+InputXML+"</INPUTXML>";
        //
        //					StringWriter writer = new StringWriter();
        //					XmlDocument xmlDocTemp = new XmlDocument();
        //					xmlDocTemp.LoadXml(InputXML); 
        //					XPathNavigator nav = ((IXPathNavigable) xmlDocTemp).CreateNavigator();
        //
        //					xslt.Transform(nav,null,writer);
        //		
        //					string XMLoutput = writer.ToString();
        //
        //					XmlDoc.LoadXml(XMLoutput);
        //			
        //			
        //					//Fetch Product Factor Path
        //					XmlNodeList nodlst;
        //					nodlst					=	XmlDoc.GetElementsByTagName("FACTORMASTERPATH");
        //			
        //					//Final Product Factor Master Name
        //					strReturnVal			=	nodlst.Item(0).InnerText;
        //			
        //					strReturnVal.Replace("\t","");
        //					strReturnVal.Replace("\r","");
        //					strReturnVal.Replace("\n","");
        //					//System.Web.HttpContext.Current.Request.Url.Host
        //					string strPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetApplicationPath();
        //					strReturnVal = strPath + strReturnVal;
        //					writer.Close();
        //				}
        //				return strReturnVal;
        //			
        //			}
        //			catch(Exception exc)
        //			{
        //				throw(exc);
        //			}
        //			finally
        //			{}
        //
        //
        //		}
        //		
        //		

        #endregion

        public string GetProductFactorMasterPath(string InputXML, string SelectLOB)
        {


            try
            {
                string strReturnVal = "";
                //-----------------------------------------------------------------------------				 
                //For Transforming the .xsl and .xml files
                //XslTransform xslt = new XslTransform();
                XslCompiledTransform xslt = new XslCompiledTransform();
                // Load the xml into a document object
                XmlDocument XmlDoc = new XmlDocument();
                XmlDocument XmlDocINPUT = new XmlDocument();
                //-----------------------------------------------------------------------------
                XmlDocument xmlCheckProductName = new XmlDocument();
                xmlCheckProductName.LoadXml(InputXML);


                strStateName = xmlCheckProductName.GetElementsByTagName("STATENAME").Item(0).InnerText;

                //Get the QUOTEEFFDATE from the input
                XmlNodeList nodlstdate = xmlCheckProductName.GetElementsByTagName("QUOTEEFFDATE");
                XmlNodeList nodDateList;

                if (nodlstdate != null)
                {
                    string strQEffectiveDate = nodlstdate.Item(0).InnerText;

                    string strDateXML = "";
                    DateTime dtEffectiveDate = DateTime.Parse(strQEffectiveDate);

                    //Fetch all dates and their details from the productfactordatemaster xml
                    string strProductFactorPath = "";
                    strProductFactorPath = ClsCommon.GetKeyValueWithIP("ProductFactorMasterDetailsPath");
                    xmlCheckProductName.Load(strProductFactorPath);

                    XmlDocument xmlProductFactorPath = new XmlDocument();
                    nodDateList = xmlCheckProductName.SelectNodes("MAIN/" + SelectLOB + "/ATTRIBUTE[@STATE='" + strStateName + "']");
                    if (nodDateList != null && nodDateList.Count > 0)
                    {
                        strDateXML = nodDateList.Item(0).InnerXml;
                    }

                    xmlProductFactorPath.LoadXml("<" + SelectLOB + ">" + strDateXML + "</" + SelectLOB + ">");

                    nodDateList = xmlProductFactorPath.GetElementsByTagName("DATE");
                    DateTime dtTempDate;

                    //compare quote date from inputxml against each date fetched from master xml
                    string filePath = "";
                    if (nodDateList.Count > 0)
                    {

                        foreach (XmlNode nodTempDate in nodDateList)
                        {
                            if (nodTempDate.InnerText != "")
                            {

                                /* Check if it is the first node.
                                 * If it is the first node and the date passed is less than the date fetched in the first node then take the first node
                                 * Check if it is the last node.
                                 * If it is the last node and the date passed is greater than or equal to the date fetched in the last node then take hte last node.
                                 * Else check the duration and use accordingly */


                                dtTempDate = DateTime.Parse(nodTempDate.InnerText);
                                int iTemp = DateTime.Compare(dtEffectiveDate, dtTempDate);

                                if (nodTempDate.NextSibling == null) //last node
                                {
                                    if (iTemp >= 0)
                                    {
                                        filePath = "<EDATE>" + nodTempDate.InnerText + "</EDATE>";
                                        RATE_EFFECTIVE_DATE = nodTempDate.InnerText;
                                        break;
                                    }
                                    else
                                    {
                                        filePath = "<EDATE>" + nodTempDate.PreviousSibling.InnerText + "</EDATE>";
                                        RATE_EFFECTIVE_DATE = nodTempDate.PreviousSibling.InnerText;
                                        break;
                                    }

                                }
                                else if (nodTempDate.PreviousSibling == null) // First node
                                {
                                    if (iTemp < 0)
                                    {
                                        filePath = "<EDATE>" + nodTempDate.InnerText + "</EDATE>";
                                        RATE_EFFECTIVE_DATE = nodTempDate.InnerText;
                                        break;
                                    }
                                }
                                else
                                {
                                    DateTime dtTempNextDate = DateTime.Parse(nodTempDate.NextSibling.InnerText);
                                    int nxtTemp = DateTime.Compare(dtEffectiveDate, dtTempNextDate);
                                    if (iTemp > 0 && nxtTemp < 0)
                                    {
                                        filePath = "<EDATE>" + nodTempDate.InnerText + "</EDATE>";
                                        RATE_EFFECTIVE_DATE = nodTempDate.InnerText;
                                        break;
                                    }
                                }
                            }
                        }

                    }

                    string strProductFactorsMasterPath = "";
                    if (SelectLOB == "HO")
                    {
                        strProductFactorsMasterPath = ClsCommon.GetKeyValueWithIP("ProductFactorsMasterPath"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath").ToString();
                        InputXML = InputXML.Replace("<LOB_ID>", filePath + "<LOBNAME>HO</LOBNAME><LOB_ID>");
                    }
                    if (SelectLOB == "RV")
                    {
                        strProductFactorsMasterPath = ClsCommon.GetKeyValueWithIP("ProductFactorsMasterPath_RV"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath").ToString();
                        InputXML = InputXML.Replace("<LOB_ID>", filePath + "<LOBNAME>RV</LOBNAME><LOB_ID>");
                    }
                    else if (SelectLOB == "AUTOP" || SelectLOB == "AUTOC")
                    {
                        strProductFactorsMasterPath = ClsCommon.GetKeyValueWithIP("ProductFactorsMasterPath_Auto"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
                        InputXML = InputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE>" + filePath + "<LOBNAME>AUTO</LOBNAME>");
                    }
                    else if (SelectLOB == "REDW")
                    {
                        strProductFactorsMasterPath = ClsCommon.GetKeyValueWithIP("ProductFactorsMasterPath_REDW"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
                        InputXML = InputXML.Replace("<LOB_ID>", filePath + "<LOBNAME>REDW</LOBNAME><LOB_ID>");
                    }
                    else if (SelectLOB == "BOAT")
                    {
                        strProductFactorsMasterPath = ClsCommon.GetKeyValueWithIP("ProductFactorsMasterPath_BOAT"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
                        InputXML = InputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE>" + filePath + "<LOBNAME>BOAT</LOBNAME>");
                    }
                    else if (SelectLOB == "CYCL")
                    {
                        strProductFactorsMasterPath = ClsCommon.GetKeyValueWithIP("ProductFactorsMasterPath_CYCL"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
                        InputXML = InputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE>" + filePath + "<LOBNAME>CYCL</LOBNAME>");
                    }
                    else if (SelectLOB == "UMB")
                    {
                        strProductFactorsMasterPath = ClsCommon.GetKeyValueWithIP("ProductFactorsMasterPath_UMB"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
                        InputXML = InputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE>" + filePath + "<LOBNAME>UMB</LOBNAME>");
                    }
                    xslt.Load(strProductFactorsMasterPath);
                    //merge the inputxml with XSL. to get the filepath			

                    // Transform the file and output an HTML string.

                    InputXML = "<INPUTXML>" + InputXML + "</INPUTXML>";

                    StringWriter writer = new StringWriter();
                    XmlDocument xmlDocTemp = new XmlDocument();
                    xmlDocTemp.LoadXml(InputXML);
                    XPathNavigator nav = ((IXPathNavigable)xmlDocTemp).CreateNavigator();

                    xslt.Transform(nav, null, writer);

                    string XMLoutput = writer.ToString();

                    XmlDoc.LoadXml(XMLoutput);


                    //Fetch Product Factor Path
                    XmlNodeList nodlst;
                    nodlst = XmlDoc.GetElementsByTagName("FACTORMASTERPATH");

                    //Final Product Factor Master Name
                    strReturnVal = nodlst.Item(0).InnerText;

                    strReturnVal.Replace("\t", "");
                    strReturnVal.Replace("\r", "");
                    strReturnVal.Replace("\n", "");
                    //System.Web.HttpContext.Current.Request.Url.Host

                    //					string strPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetApplicationPath();
                    //					strReturnVal = strPath + strReturnVal;

                    string strPath = Cms.BusinessLayer.BlCommon.ClsCommon.WebAppUNCPath;
                    strReturnVal = System.IO.Path.GetFullPath(strPath + @"\" + strReturnVal);

                    writer.Close();

                    //Added by Manoj -Sept.08.2009
                    strRateEffectiveDate = RATE_EFFECTIVE_DATE.ToString();
                }
                return strReturnVal;

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }


        }


        public string GetProductPremiumPath(string InputXML, string SelectLOB)
        {


            try
            {
                string strReturnVal = "";

                //For Transforming the .xsl and .xml files
                //XslTransform xslt = new XslTransform();
                XslCompiledTransform xslt = new XslCompiledTransform();
                // Load the xml into a document object
                XmlDocument XmlDoc = new XmlDocument();
                XmlDocument XmlDocINPUT = new XmlDocument();
                //-----------------------------------------------------------------------------
                XmlDocument xmlCheckProductName = new XmlDocument();
                xmlCheckProductName.LoadXml(InputXML);

                if (strStateName.Trim() == "")
                {
                    strStateName = xmlCheckProductName.GetElementsByTagName("STATENAME").Item(0).InnerText;
                }

                //Get the QUOTEEFFDATE from the input
                XmlNodeList nodlstdate = xmlCheckProductName.GetElementsByTagName("QUOTEEFFDATE");
                XmlNodeList nodDateList;
                string strLOBType = "";
                if (nodlstdate != null)
                {
                    string strQEffectiveDate = nodlstdate.Item(0).InnerText;
                    QUOTE_EFFECTIVE_DATE = strQEffectiveDate;

                    string strDateXML = "";
                    DateTime dtEffectiveDate = DateTime.Parse(strQEffectiveDate);

                    //Fetch all dates and their details from the productfactordatemaster xml
                    string strProductFactorPath = "";
                    strProductFactorPath = ClsCommon.GetKeyValueWithIP("ProductPremiumDetailsPath");//ConfigurationSettings.AppSettings.Get("ProductFactorMasterDetailsPath").ToString();
                    xmlCheckProductName.Load(strProductFactorPath);
                    XmlDocument xmlProductFactorPath = new XmlDocument();

                    nodDateList = xmlCheckProductName.SelectNodes("MAIN/" + SelectLOB + "/ATTRIBUTE[@STATE='" + strStateName + "']");
                    strDateXML = nodDateList.Item(0).InnerXml;
                    strLOBType = SelectLOB;

                    xmlProductFactorPath.LoadXml("<" + strLOBType + ">" + strDateXML + "</" + strLOBType + ">");
                    nodDateList = xmlProductFactorPath.GetElementsByTagName("DATE");

                    DateTime dtTempDate;


                    //compare quote date from inputxml against each date fetched from master xml
                    string filePath = "";
                    if (nodDateList.Count > 0)
                    {

                        foreach (XmlNode nodTempDate in nodDateList)
                        {
                            if (nodTempDate.InnerText != "")
                            {

                                /* Check if it is the first node.
                                 * If it is the first node and the date passed is less than the date fetched in the first node then take the first node
                                 * Check if it is the last node.
                                 * If it is the last node and the date passed is greater than or equal to the date fetched in the last node then take hte last node.
                                 * Else check the duration and use accordingly */


                                dtTempDate = DateTime.Parse(nodTempDate.InnerText);
                                int iTemp = DateTime.Compare(dtEffectiveDate, dtTempDate);

                                if (nodTempDate.NextSibling == null) //last node
                                {
                                    if (iTemp >= 0)
                                    {
                                        filePath = "<EDATE>" + nodTempDate.InnerText + "</EDATE>";
                                        break;
                                    }
                                    else
                                    {
                                        filePath = "<EDATE>" + nodTempDate.PreviousSibling.InnerText + "</EDATE>";
                                        break;
                                    }

                                }
                                else if (nodTempDate.PreviousSibling == null) // First node
                                {
                                    if (iTemp < 0)
                                    {
                                        filePath = "<EDATE>" + nodTempDate.InnerText + "</EDATE>";
                                        break;
                                    }
                                }
                                else
                                {
                                    DateTime dtTempNextDate = DateTime.Parse(nodTempDate.NextSibling.InnerText);
                                    int nxtTemp = DateTime.Compare(dtEffectiveDate, dtTempNextDate);
                                    if (iTemp > 0 && nxtTemp < 0)
                                    {
                                        filePath = "<EDATE>" + nodTempDate.InnerText + "</EDATE>";
                                        break;
                                    }
                                }
                            }
                        }

                    }

                    string strProductFactorsMasterPath = "";
                    if (SelectLOB == "HO")
                    {
                        strProductFactorsMasterPath = ClsCommon.GetKeyValueWithIP("ProductPremiumrPath_HO"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath").ToString();
                        InputXML = InputXML.Replace("<LOB_ID>", filePath + "<LOBNAME>HO</LOBNAME><LOB_ID>");
                    }
                    else if (SelectLOB == "AUTOP" || SelectLOB == "AUTOC")
                    {
                        strProductFactorsMasterPath = ClsCommon.GetKeyValueWithIP("ProductPremium_Auto"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
                        InputXML = InputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE>" + filePath + "<LOBNAME>AUTO</LOBNAME>");
                    }

                    else if (SelectLOB == "REDW")
                    {
                        strProductFactorsMasterPath = ClsCommon.GetKeyValueWithIP("ProductPremium_REDW"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
                        InputXML = InputXML.Replace("<LOB_ID>", filePath + "<LOBNAME>REDW</LOBNAME><LOB_ID>");
                    }
                    else if (SelectLOB == "BOAT")
                    {
                        strProductFactorsMasterPath = ClsCommon.GetKeyValueWithIP("ProductPremium_BOAT"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
                        InputXML = InputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE>" + filePath + "<LOBNAME>BOAT</LOBNAME>");
                    }
                    else if (SelectLOB == "CYCL")
                    {
                        strProductFactorsMasterPath = ClsCommon.GetKeyValueWithIP("ProductPremium_CYCL"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
                        InputXML = InputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE>" + filePath + "<LOBNAME>CYCL</LOBNAME>");
                    }
                    else if (SelectLOB == "RV")
                    {
                        strProductFactorsMasterPath = ClsCommon.GetKeyValueWithIP("ProductPremium_RV"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
                        InputXML = InputXML.Replace("<LOB_ID>", filePath + "<LOBNAME>RV</LOBNAME><LOB_ID>");
                    }
                    else if (SelectLOB == "UMB")
                    {
                        strProductFactorsMasterPath = ClsCommon.GetKeyValueWithIP("ProductPremium_UMB"); //System.Configuration.ConfigurationSettings.AppSettings.Get("ProductFactorsMasterPath_Auto").ToString();
                        InputXML = InputXML.Replace("<LOB_ID>", filePath + "<LOBNAME>UMB</LOBNAME><LOB_ID>");
                    }

                    xslt.Load(strProductFactorsMasterPath);
                    //merge the inputxml with XSL. to get the filepath			

                    // Transform the file and output an HTML string.

                    InputXML = InputXML.Replace("<INPUTXML>", "");
                    InputXML = InputXML.Replace("</INPUTXML>", "");
                    InputXML = "<INPUTXML>" + InputXML + "</INPUTXML>";

                    StringWriter writer = new StringWriter();
                    XmlDocument xmlDocTemp = new XmlDocument();
                    xmlDocTemp.LoadXml(InputXML);
                    XPathNavigator nav = ((IXPathNavigable)xmlDocTemp).CreateNavigator();

                    xslt.Transform(nav, null, writer);

                    string XMLoutput = writer.ToString();

                    XmlDoc.LoadXml(XMLoutput);


                    //Fetch Product Factor Path
                    XmlNodeList nodlst;
                    nodlst = XmlDoc.GetElementsByTagName("FACTORMASTERPATH");

                    //Final Product Factor Master Name
                    strReturnVal = nodlst.Item(0).InnerText;

                    strReturnVal.Replace("\t", "");
                    strReturnVal.Replace("\r", "");
                    strReturnVal.Replace("\n", "");

                    //					string strPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetApplicationPath();
                    //					strReturnVal = strPath + strReturnVal;

                    string strPath = Cms.BusinessLayer.BlCommon.ClsCommon.WebAppUNCPath;
                    strReturnVal = System.IO.Path.GetFullPath(strPath + @"\" + strReturnVal);

                    writer.Close();
                }
                return strReturnVal;

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }


        }


        /// <summary>
        /// Accepts inputXML.
        /// Returns an quote xmlstring.	
        /// </summary>
        /// <param name="inputXML"></param>	 
        /// <returns> Quote XMl string</param>	 
        public string GenerateHO3QuoteForQuickQuote(string InputXML)
        {

            string strOutputXML = "", returnString = "";

            try
            {
                #region		GENERATE QUOTE

                //Get the QuoteXML *************************
                strOutputXML = this.GetHO3QuoteXMLForQuickQuote(InputXML);//98,2,1,2);

                // Load the InputXML
                XmlDocument xmlDocInput = new XmlDocument();
                xmlDocInput.LoadXml(strOutputXML);

                //Transform the Input XMl to generate the premium
                string xslFilePath = ClsCommon.GetKeyValueWithIP("FinalQuoteHO3"); //System.Configuration.ConfigurationSettings.AppSettings.Get("FinalQuoteHO3").ToString();
                //XslTransform tr = new XslTransform();
                XslCompiledTransform tr = new XslCompiledTransform();
                tr.Load(xslFilePath);
                XPathNavigator nav = ((IXPathNavigable)xmlDocInput).CreateNavigator();
                StringWriter sw1 = new StringWriter();
                tr.Transform(nav, null, sw1);

                //Get the premium XML
                string quoteXML = sw1.ToString();
                if (quoteXML.Trim() != "")
                {
                    returnString = quoteXML;
                }
                sw1.Close();

                return returnString;

                # endregion
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {

            }
        }

        #region  CODE COMMENTED BY ASHWANI AS THIS IS NOT BEING USED
        //		/// <summary>
        //		/// Accepts customerID appId and app version id and an out parameter.
        //		/// Returns an xmlstring.this string will be
        //		/// 1. Input string in case any input is missing.
        //		/// 2. Quote XML in case the quote is generated and saved in the quote table 
        //		/// </summary>
        //		/// <param name="customerID"></param>
        //		/// <param name="appID"></param>
        //		/// <param name="appVersionID"></param>
        //		/// <returns> integer 1/2/3. 2=Invalid Input, 1= valid quote generated, 3=quote not generated.(error)</param>
        //	 
        //		public int GenerateQuote(int customerID, int appID, int appVersionID)
        //		{
        //		
        //			string strInputXML="",strOutputXML="",returnString="";		
        //			int returnValue=0;
        //
        //			try 
        //			{
        //				#region		GENERATE QUOTE
        //				
        //				ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();			 		
        //				
        //				//get the input xml.
        //				strInputXML		= objGeneralInfo.GetInputXML(customerID,appID,appVersionID);//98,2,1,2);
        //	
        //				/* Check if the input xml is well defined.i.e. it has no nodes blank
        //				 * 1. If it is not valid then prompt a message to the user 
        //				 * else proceed */ 
        //				bool validXML= ValidInputXML(strInputXML);
        //				if(!validXML )
        //				{					
        //					returnValue=2;		 // 2 implies that InputMessage.xsl will be used to show the insufficient input details			
        //					returnString= strInputXML;
        //				}
        //				else
        //				{
        //					//Get the QuoteXML *************************
        //					strOutputXML	= GetQuoteXML(customerID,appID,appVersionID);//98,2,1,2);
        //					
        //					// Load the InputXML
        //					XmlDocument xmlDocInput = new XmlDocument();
        //					xmlDocInput.LoadXml(strOutputXML);  
        //					
        //					//Transform the Input XMl to generate the premium
        //					string xslFilePath = ClsCommon.GetKeyValueWithIP("FinalQuoteHO3");//System.Configuration.ConfigurationSettings.AppSettings.Get("FinalQuoteHO3").ToString();
        //					XslTransform tr	= new XslTransform();				
        //
        //					tr.Load(xslFilePath);
        //					XPathNavigator nav = ((IXPathNavigable) xmlDocInput).CreateNavigator();
        //					StringWriter sw1 = new StringWriter();
        //					tr.Transform(nav,null,sw1);
        //					
        //					//Get the premium XML
        //					string quoteXML= sw1.ToString();
        //					if (quoteXML.Trim()!="")
        //					{				
        //						returnString=quoteXML;
        //
        //						//save the quote
        //						ClsGenerateQuote objGenerateQuote= new ClsGenerateQuote();
        //						ClsGenerateQuoteInfo objGenerateQuoteInfo = new ClsGenerateQuoteInfo();
        //						objGenerateQuoteInfo.CUSTOMER_ID =  customerID ;
        //						objGenerateQuoteInfo.APP_ID=appID ;
        //						objGenerateQuoteInfo.APP_VERSION_ID=appVersionID ;
        //						objGenerateQuoteInfo.QUOTE_TYPE="FULL";
        //						objGenerateQuoteInfo.QUOTE_DESCRIPTION="New Quote";
        //						objGenerateQuoteInfo.IS_ACCEPTED="N";
        //
        //						objGenerateQuoteInfo.QUOTE_XML=quoteXML.Trim();
        //						objGenerateQuoteInfo.CREATED_BY = LoggedInUserId;
        //						objGenerateQuoteInfo.CREATED_DATETIME = DateTime.Now;
        //						objGenerateQuoteInfo.IS_ACTIVE ="Y";
        //						int gIntQuoteID = objGenerateQuote.Add(objGenerateQuoteInfo);
        //						
        //						//Show the xml in another window. 
        //						returnValue=1;   // 1 implies that Premium.xsl and Input.xsl will be used to transform and show the premium
        //					}
        //					else
        //					{
        //						returnValue=3;
        //					}
        //					sw1.Close();
        //					
        //				}
        //				return returnValue ;
        //				# endregion
        //			}
        //			catch(Exception exc)
        //			{
        //				throw(exc);
        //			}
        //			finally
        //			{
        //			
        //			}
        //		}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="policyID"></param>
        /// <param name="policyVersionID"></param>
        /// <param name="policyLobID"></param>
        /// <param name="appQuodeID"></param>
        /// <returns> 
        /// 1= valid quote generated,
        /// 2=Invalid Input, 
        /// 3 =quote not generated.(error)
        /// 5= Msg Verify the rules again </returns>

        // This var use to chk Home-Boat  
        /*	bool blWCExists_Pol=false;
            public int GeneratePolicyQuote(int customerID, int policyID, int policyVersionID,string policyLobID,string strCSSNo,out int policyQuodeID)
            {
                string strInputXML="",strOutputXML="",returnString="";		
                int returnValue=0;
                string LOBID=policyLobID;
			
                try 
                {
                    egion		GENERATE QUOTE				
                    ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();			 						
                    //get the input xml from the procs for the current app.
                    strInputXML		= objGeneralInfo.GetPolicyInputXML(customerID,policyID ,policyVersionID,policyLobID);
						
    //				1. 	Get the inputXML(old) for the generated quote(if any) for this app (QOT_CUSTOMER_LIST -- also fetch the QuoteIDs)
    //				2. Compare the Old and the new inputXML
    //				3. If they are same then return the QuoteID of the generated quote (old)
    //				4. If they are not same, then verify the application
    //				5. If the output parameter from the VerifyApp.. method returns 1 (valid app) then proceed with generation of Quote
    //					else prompt a message 'This application has been modified. Pls verify again'
    //				
				
                    //1. 	Get the inputXML(old) for the generated quote(if any) for this app (QOT_CUSTOMER_LIST -- also fetch the QuoteIDs)
                				
                    DataSet ds =new DataSet();
                    string strOldInputXML="";
                    int intQuodeID=0,validApplication=0;
                    ds=GetQuoteOldXML_Policy(customerID,policyID,policyVersionID);				
                    if(ds.Tables[0].Rows.Count>0)
                    {
                        strOldInputXML=ds.Tables[0].Rows[0]["QUOTE_INPUT_XML"].ToString();
                        intQuodeID=int.Parse(ds.Tables[0].Rows[0]["QUOTE_ID"].ToString());
                    }
                    //2. Compare the Old and the new inputXML
                    if(strOldInputXML!=strInputXML)
                    {
                        //4. If they are not same, then verify the application
                        ClsRatingAndUnderwritingRules objVerifyRules = new ClsRatingAndUnderwritingRules();
                        bool validXML = false;
                        string strRulesStatus= "";

                        string strShowHTML = objVerifyRules.VerifyPolicy(customerID,policyID,policyVersionID,policyLobID,out validXML,strCSSNo,out strRulesStatus);
                        if(validXML)
                        {
                            strOutputXML	= GetQuoteXML(strInputXML,LOBID);						
                            if (strOutputXML.Trim()!="")
                            {				
                                returnString=strOutputXML;
                                //save the quote	
                                ClsGenerateQuote objGenerateQuote = new ClsGenerateQuote();
                                ClsGeneratePolicyQuoteInfo objGeneratePolicyQuoteInfo = new ClsGeneratePolicyQuoteInfo();
                                objGeneratePolicyQuoteInfo.CUSTOMER_ID =  customerID ;
                                objGeneratePolicyQuoteInfo.POLICY_ID=policyID;
                                objGeneratePolicyQuoteInfo.POLICY_VERSION_ID=policyVersionID ;					    
						    
                                //flag to check if watercraft is attached to the home application
                                if(blWCExists_Pol)
                                    objGeneratePolicyQuoteInfo.QUOTE_TYPE="HOME-BOAT";		
                                else
                                    objGeneratePolicyQuoteInfo.QUOTE_TYPE="HOME";		

                                objGeneratePolicyQuoteInfo.QUOTE_DESCRIPTION="Premium"; //"New Quote"
                                objGeneratePolicyQuoteInfo.IS_ACCEPTED="N";
                                objGeneratePolicyQuoteInfo.QUOTE_XML=strOutputXML.Trim();
                                objGeneratePolicyQuoteInfo.CREATED_BY = LoggedInUserId;
                                objGeneratePolicyQuoteInfo.CREATED_DATETIME = DateTime.Now;
                                objGeneratePolicyQuoteInfo.IS_ACTIVE ="Y";
                                objGeneratePolicyQuoteInfo.QUOTE_INPUT_XML=strInputXML;					
                                policyQuodeID = objGenerateQuote.Add_Pol(objGeneratePolicyQuoteInfo);					
                                //Show the xml in another window. 
                                returnValue=1;   // 1 implies that Premium.xsl and Input.xsl will be used to transform and show the premium
                            }
                            else
                            {
                                policyQuodeID=0;
                                returnValue=3; // 3 = quote not generated.(error)							
                            }
                        }
                        else
                        {
                            // show msg 'This policy has been modified. Pls verify again'
                            policyQuodeID=0;
                            returnValue=5;						
                        }
                    }
                    else
                    {
                        //3. If they are same then return the QuoteID of the generated quote (old)
                        policyQuodeID=intQuodeID;	
                        returnValue=1;					
                    }			

                    // chk here if lob is HO then chk for watercraft				
                    if( (policyLobID.ToUpper().Trim()=="HOME" && returnValue==1) || (policyLobID.ToUpper().Trim()=="1" && returnValue==1))
                    {
                        // only if  ho and return ==1 check if watercraft is attached to the application
                        if(CheckWatercraft_Pol(customerID,policyID,policyVersionID))
                        {
                            //set flag to true
                            blWCExists=true;
                            //sending the lob as watercraft for HOME-BOAT
                            policyLobID=LOB_WATERCRAFT ;// "4";
                            returnValue=GeneratePolicyQuote(customerID,policyID,policyVersionID,policyLobID,strCSSNo,out policyQuodeID);				
                        }					
                    }							
                    return returnValue;
                    endregion
                }
                catch(Exception exc)
                {
                    throw(exc);
                }
                finally
                {		
                }
            }*/

        #endregion

        public int GeneratePolicyQuote(int customerID, int policyID, int policyVersionID, string policyLobID, string strCSSNo, out int policyQuodeID, string UserID)
        {
            string strPolicyQuote = GetPolicyQuoteCode(customerID, policyID, policyVersionID);
            return GeneratePolicyQuote(customerID, policyID, policyVersionID, policyLobID, strPolicyQuote, strCSSNo, out policyQuodeID, UserID);
        }


        #region Added By Ravindra For Accounting


        public int GeneratePolicyQuote(int customerID, int policyID, int policyVersionID, string policyLobID, out int policyQuodeID, string UserID)
        {
            string strPolicyQuote = GetPolicyQuoteCode(customerID, policyID, policyVersionID);
            return GeneratePolicyQuote(customerID, policyID, policyVersionID, policyLobID, strPolicyQuote, "N.A", out policyQuodeID, UserID);
        }
        private int GeneratePolicyQuote(int customerID, int policyID, int policyVersionID, string policyLobID, string strQuote_Type, string strCSSNo, out int policyQuodeID, string UserID)
        {
            string strInputXML = "", strOutputXML = "";
            int intQuodeID = 0;
            int returnValue = 0, colorScheme = 1;
            string LOBID = policyLobID;
            double Num;
            if (double.TryParse(strCSSNo, out Num)) colorScheme = int.Parse(strCSSNo);
            try
            {
                //If Policy is in "Non-editable" State then send OldInputXML on Quote. -- Asfa Praveen 22-June-2007
                policyQuodeID = intQuodeID;

                DataSet ds = FetchPolicyOldInputXML(customerID, policyID, policyVersionID);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    string quoteFirstComponentXml = "", policyStatus = "";
                    quoteFirstComponentXml = ds.Tables[0].Rows[0]["QUOTE_INPUT_XML"].ToString();
                    policyStatus = ds.Tables[0].Rows[0]["POLICY_STATUS"].ToString();
                    if (!(policyStatus == POLICY_STATUS_UNDER_ENDORSEMENT
                        || policyStatus == POLICY_STATUS_UNDER_RENEW
                        || policyStatus == POLICY_STATUS_UNDER_CORRECTIVE_USER
                        || policyStatus == POLICY_STATUS_UNDER_REINSTATE
                        || policyStatus == POLICY_STATUS_UNDER_ISSUE
                        || policyStatus == POLICY_STATUS_SUSPENDED
                        || policyStatus == POLICY_STATUS_RENEWAL_SUSPENSE
                        || policyStatus == POLICY_STATUS_UNDER_REWRITE
                        || policyStatus == POLICY_STATUS_SUSPENSE_REWRITE
                        || policyStatus == POLICY_STATUS_SUSPENSE_ENDORSEMENT
                        || policyStatus == POLICY_STATUS_APPLICATION
                        ))
                    {
                        intQuodeID = int.Parse(ds.Tables[0].Rows[0]["QUOTE_ID"].ToString());
                        policyQuodeID = intQuodeID;
                        returnValue = 6;
                        return returnValue;
                    }

                }
                //-------------------------------------------

                ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();
                //get the input xml from the procs for the current app.
                if (int.Parse(policyLobID) >= 9)
                    strInputXML = GetPolicyProductInputXML(customerID, policyID, policyVersionID, int.Parse(policyLobID), colorScheme, int.Parse(UserID), "");
                else
                    strInputXML = objGeneralInfo.GetPolicyInputXML(customerID, policyID, policyVersionID, policyLobID);

                ds = new DataSet();
                string strOldInputXML = "";

                ds = GetQuoteOldXML_Policy(customerID, policyID, policyVersionID, strQuote_Type);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    strOldInputXML = ds.Tables[0].Rows[0]["QUOTE_INPUT_XML"].ToString();
                    intQuodeID = int.Parse(ds.Tables[0].Rows[0]["QUOTE_ID"].ToString());
                }
                //2. Compare the Old and the new inputXML
                if (strOldInputXML != strInputXML)
                {

                    string isValidInput = "0";
                    string verificationHTML = "";
                    string strVerify = ClsCommon.GetKeyValueForSetup("InputVerificationForQuote");//System.Configuration.ConfigurationSettings.AppSettings.Get("InputVerificationForQuote").ToString();
                    if (strVerify.Trim().ToUpper() == "Y" && int.Parse(policyLobID) < 9)
                    {
                        // verifying the input xml
                        string retVal = InputXmlVerification(strInputXML, policyLobID); //return is in the format string#0 or string#1 . 0 --> invalid  1-->valid
                        string[] retValue = retVal.Split('#');
                        verificationHTML = retValue[0];
                        isValidInput = retValue[1].ToString();
                    }
                    else
                    {
                        isValidInput = "1";
                    }
                    if (isValidInput.Trim() == "0")
                    {
                        policyQuodeID = intQuodeID;
                        return returnValue = 0;  //if Inputxml is invalid 
                    }
                    else
                    {
                        //4. If they are not same, then verify the application
                        ClsRatingAndUnderwritingRules objVerifyRules = new ClsRatingAndUnderwritingRules(mSystemID);
                        bool validXML = false;
                        if (strCSSNo != "N.A") // Verify Rules
                        {
                            string strRulesStatus = "";
                            string strShowHTML = objVerifyRules.VerifyPolicy(customerID, policyID, policyVersionID, policyLobID, out validXML, strCSSNo, out strRulesStatus, "QUOTE");
                        }
                        else //No Need To Verify Rules Called from Policy Processes
                        {
                            validXML = true;

                        }

                        if (validXML)
                        {
                            strOutputXML = GetQuoteXML(strInputXML, LOBID);
                            if (strOutputXML.Trim() != "")
                            {
                                //returnString=strOutputXML;
                                //save the quote	
                                //ClsGenerateQuote objGenerateQuote = new ClsGenerateQuote();
                                ClsGeneratePolicyQuoteInfo objGeneratePolicyQuoteInfo = new ClsGeneratePolicyQuoteInfo();
                                objGeneratePolicyQuoteInfo.CUSTOMER_ID = customerID;
                                objGeneratePolicyQuoteInfo.POLICY_ID = policyID;
                                objGeneratePolicyQuoteInfo.POLICY_VERSION_ID = policyVersionID;
                                objGeneratePolicyQuoteInfo.QUOTE_TYPE = strQuote_Type;
                                objGeneratePolicyQuoteInfo.QUOTE_DESCRIPTION = "Premium"; //"New Quote"
                                objGeneratePolicyQuoteInfo.IS_ACCEPTED = "N";
                                objGeneratePolicyQuoteInfo.QUOTE_XML = strOutputXML.Trim();
                                //								if(IsEODProcess)
                                //									objGeneratePolicyQuoteInfo.CREATED_BY = EODUserID;
                                //								else
                                //									objGeneratePolicyQuoteInfo.CREATED_BY = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());

                                objGeneratePolicyQuoteInfo.CREATED_BY = Convert.ToInt32(UserID);

                                objGeneratePolicyQuoteInfo.CREATED_DATETIME = DateTime.Now;
                                objGeneratePolicyQuoteInfo.IS_ACTIVE = "Y";
                                objGeneratePolicyQuoteInfo.QUOTE_INPUT_XML = strInputXML;
                                //Added by Manoj Rathore Sept.08.2009
                                if (strRateEffectiveDate != "")
                                    objGeneratePolicyQuoteInfo.RATE_EFFECTIVE_DATE = Convert.ToDateTime(strRateEffectiveDate);
                                else
                                    objGeneratePolicyQuoteInfo.RATE_EFFECTIVE_DATE = Convert.ToDateTime(ConvertDBDateToCulture("01/19/2010"));
                                objGeneratePolicyQuoteInfo.BUSINESS_TYPE = strBusinessType.ToString();

                                //policyQuodeID = objGenerateQuote.Add_Pol(objGeneratePolicyQuoteInfo);					

                                policyQuodeID = Add_Pol(objGeneratePolicyQuoteInfo);

                                returnValue = 1;
                                //Split Premium
                                //							if(SplitPremiumsPol(customerID,policyID,policyVersionID,"Change It"))
                                //							{
                                //								returnValue=1;  
                                //							}
                                //							else
                                //							{
                                //								policyQuodeID=0;
                                //								returnValue=3; // 3 = quote not generated.(error)							
                                //							}
                            }
                            else
                            {
                                policyQuodeID = 0;
                                returnValue = 3; // 3 = quote not generated.(error)							
                            }
                        }
                        else
                        {
                            // show msg 'This policy has been modified. Pls verify again'
                            policyQuodeID = 0;
                            returnValue = 5;
                        }
                    }
                }
                else
                {
                    //4. If they are same then return the QuoteID of the generated quote (old)
                    policyQuodeID = intQuodeID;
                    returnValue = 1;
                }

                // chk here if lob is HO then chk for watercraft				
                if ((policyLobID.ToUpper().Trim() == LOB_HOME_CODE && returnValue.ToString().Trim() == LOB_HOME) || (policyLobID.ToUpper().Trim() == LOB_HOME && returnValue.ToString().Trim() == "1"))
                {
                    // only if  ho and return ==1 check if watercraft is attached to the application
                    if (CheckWatercraft_Pol(customerID, policyID, policyVersionID))
                    {
                        //sending the lob as watercraft for HOME-BOAT
                        policyLobID = LOB_WATERCRAFT;// "4";
                        returnValue = GeneratePolicyQuote(customerID, policyID, policyVersionID, policyLobID, "HOME-BOAT", strCSSNo, out policyQuodeID, UserID);
                    }
                }
                return returnValue;

            }
            catch (Exception exc)
            {
                throw new Exception("Error occurred at generating policy quote", exc);
                //throw(exc);
            }
            finally
            {
            }
        }

        public string GetPolicyQuoteCode(int CustomerId, int PolicyId, int PolicyVersionId)
        {
            try
            {
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                DataSet dsTemp;
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                dsTemp = objWrapper.ExecuteDataSet("Proc_GetAppLobDetailsFromPolicy");
                return dsTemp.Tables[0].Rows[0]["QUOTE_TYPE"].ToString().Trim();

            }
            catch (Exception objExp)
            {
                throw new Exception("Error occured while fetching Quote type at Policy Quote", objExp);
                //throw (objExp);
            }
        }




        #endregion


        /// <summary>
        /// Returns an xmlstring.this string will be
        /// 1. Input string in case any input is missing.
        /// 2. Quote XML in case the quote is generated and saved in the quote table 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <returns> integer 1/2/3. 
        /// 2=Invalid Input,
        /// 1= valid quote generated, 
        /// 3 =quote not generated.(error)
        /// 4= Msg Verify the rules again 
        ///</param>

        // This var use to chk Home-Boat  
        bool blWCExists = false;

        public string GenerateQuote(int customerID, int appID, int appVersionID, string appLobID, out int appQuodeID)
        {
            string strInputXML = "", strOutputXML = "", returnString = "";
            int intQuodeID = 0;
            string returnValue = "0";
            try
            {
                #region		GENERATE QUOTE
                //If Application is in "Complete" State then send OldInputXML on Quote. -- Asfa Praveen 22-June-2007
                appQuodeID = intQuodeID;

                DataSet ds = FetchAppOldInputXML(customerID, appID, appVersionID);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    string quoteFirstComponentXml = "", quoteSecondComponentXml = "";
                    quoteFirstComponentXml = ds.Tables[0].Rows[0]["QUOTE_INPUT_XML"].ToString();
                    quoteSecondComponentXml = ds.Tables[0].Rows[0]["APP_STATUS"].ToString();
                    if (quoteSecondComponentXml.Equals("Complete"))
                    {
                        intQuodeID = int.Parse(ds.Tables[0].Rows[0]["QUOTE_ID"].ToString());
                        appQuodeID = intQuodeID;
                        returnValue = "6";
                        return returnValue;
                    }

                }
                //-------------------------------------------

                ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();
                //get the input xml from the procs for the current app.
                strInputXML = objGeneralInfo.GetInputXML(customerID, appID, appVersionID, appLobID);//98,2,1,2);				
                /* 
                1. 	Get the inputXML(old) for the generated quote(if any) for this app (QOT_CUSTOMER_LIST -- also fetch the QuoteIDs)
                2. Compare the Old and the new inputXML
                3. If they are same then return the QuoteID of the generated quote (old)
                4. If they are not same, then verify the application
                5. If the output parameter from the VerifyApp.. method returns 1 (valid app) then proceed with generation of Quote
                    else prompt a message 'This application has been modified. Pls verify again'
                */

                //1. 	Get the inputXML(old) for the generated quote(if any) for this app (QOT_CUSTOMER_LIST -- also fetch the QuoteIDs)
                ds = new DataSet();
                string strOldInputXML = "";
                int validApplication = 0;
                ds = GetQuoteOldXML(customerID, appID, appVersionID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    strOldInputXML = ds.Tables[0].Rows[0]["QUOTE_INPUT_XML"].ToString();
                    intQuodeID = int.Parse(ds.Tables[0].Rows[0]["QUOTE_ID"].ToString());
                }

                //2. Compare the Old and the new inputXML.If there is any change in input then verify the input				
                if (strOldInputXML != strInputXML)
                {
                    //4. If they are not same, then verify the application
                    string verificationHTML = "", isValidInput = "0";
                    string strVerify = ClsCommon.GetKeyValueForSetup("InputVerificationForQuote");//System.Configuration.ConfigurationSettings.AppSettings.Get("InputVerificationForQuote").ToString();
                    if (strVerify.Trim().ToUpper() == "Y")
                    {
                        // verifying the input xml
                        string retVal = InputXmlVerification(strInputXML, appLobID); //return is in the format string#0 or string#1 . 0 --> invalid  1-->valid
                        string[] retValue = retVal.Split('#');
                        verificationHTML = retValue[0].ToString();
                        isValidInput = retValue[1].ToString();
                    }
                    else
                    {
                        isValidInput = "1";
                    }

                    //4. If the input is valid then calculate the rate.
                    if (isValidInput.Trim() == "0")
                    {
                        appQuodeID = intQuodeID;
                        returnValue = "3^" + verificationHTML;
                        return returnValue;
                    }
                    else
                    {
                        ClsRatingAndUnderwritingRules objVerifyRules = new ClsRatingAndUnderwritingRules(mSystemID);
                        string strShowHTML = objVerifyRules.VerifyApplication(customerID, appID, appVersionID, appLobID, "1", out validApplication);
                        if (validApplication == 1)
                        {
                            strOutputXML = GetQuoteXML(strInputXML, appLobID);
                            if (strOutputXML.Trim() != "")
                            {
                                returnString = strOutputXML;
                                //save the quote
                                //ClsGenerateQuote objGenerateQuote= new ClsGenerateQuote();
                                ClsGenerateQuoteInfo objGenerateQuoteInfo = new ClsGenerateQuoteInfo();
                                objGenerateQuoteInfo.CUSTOMER_ID = customerID;
                                objGenerateQuoteInfo.APP_ID = appID;
                                objGenerateQuoteInfo.APP_VERSION_ID = appVersionID;

                                //flag to check if watercraft is attached to the home application
                                if (blWCExists)
                                    objGenerateQuoteInfo.QUOTE_TYPE = "HOME-BOAT";
                                else
                                    objGenerateQuoteInfo.QUOTE_TYPE = "HOME";

                                objGenerateQuoteInfo.QUOTE_DESCRIPTION = "New Quote";
                                objGenerateQuoteInfo.IS_ACCEPTED = "N";
                                objGenerateQuoteInfo.QUOTE_XML = strOutputXML.Trim();
                                //objGenerateQuoteInfo.CREATED_BY = LoggedInUserId;
                                if (IsEODProcess)
                                    objGenerateQuoteInfo.CREATED_BY = EODUserID;
                                else
                                    objGenerateQuoteInfo.CREATED_BY = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());
                                objGenerateQuoteInfo.CREATED_DATETIME = DateTime.Now;
                                objGenerateQuoteInfo.IS_ACTIVE = "Y";
                                objGenerateQuoteInfo.QUOTE_INPUT_XML = strInputXML;
                                objGenerateQuoteInfo.QUOTE_ID = intQuodeID;
                                //Added by Manoj Rathore Sept.08.2009
                                if (strRateEffectiveDate != "")
                                    objGenerateQuoteInfo.RATE_EFFECTIVE_DATE = Convert.ToDateTime(strRateEffectiveDate);
                                else
                                    objGenerateQuoteInfo.RATE_EFFECTIVE_DATE = Convert.ToDateTime("01/19/2010");
                                objGenerateQuoteInfo.BUSINESS_TYPE = strBusinessType.ToString();

                                //appQuodeID = objGenerateQuote.Add(objGenerateQuoteInfo);					

                                appQuodeID = Add(objGenerateQuoteInfo);

                                //Show the xml in another window. 
                                returnValue = "1";   // 1 implies that Premium.xsl and Input.xsl will be used to transform and show the premium
                            }
                            else
                            {
                                appQuodeID = 0;
                                returnValue = "3"; // 3 = quote not generated.(error)							
                            }
                            //	sw1.Close();							
                        }
                        else
                        {
                            // show msg 'This application has been modified. Pls verify again'
                            appQuodeID = 0;
                            returnValue = "4";
                        }

                    }
                }
                else
                {
                    //3. If they are same then return the QuoteID of the generated quote (old)
                    appQuodeID = intQuodeID;
                    returnValue = "1";
                }


                // chk here if lob is HO then chk for watercraft				
                if ((appLobID.ToUpper().Trim() == LOB_HOME_CODE && returnValue.Trim() == LOB_HOME) || (appLobID.ToUpper().Trim() == LOB_HOME && returnValue == "1"))
                {
                    // only if  ho and return ==1 check if watercraft is attached to the application
                    if (CheckWatercraft(customerID, appID, appVersionID))
                    {
                        //set flag to true
                        blWCExists = true;
                        //sending the lob as watercraft for HOME-BOAT
                        appLobID = LOB_WATERCRAFT;// "4";
                        returnValue = GenerateQuote(customerID, appID, appVersionID, appLobID, out appQuodeID);
                    }
                }
                // Added By Swarup	
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                Cms.BusinessLayer.BlQuote.clsSplitPremium objSplitPremium = new Cms.BusinessLayer.BlQuote.clsSplitPremium();
                objSplitPremium.SplitPremiumsApp(customerID, appID, appVersionID, null, objDataWrapper);
                return returnValue;
                # endregion

            }

            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
            }
        }


        /// <summary>
        /// /
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <returns></returns>
        private bool CheckWatercraft(int customerID, int appID, int appVersionID)
        {
            // if watercraft is attached to HO then call GenerateQuote() 
            string strStoredProcName = "Proc_Chk_Watercraft_Exists", strIS_EXISTS = "";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@APP_ID", appID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
                ds = objDataWrapper.ExecuteDataSet(strStoredProcName);
                //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
                objDataWrapper.ClearParameteres();
                strIS_EXISTS = ds.Tables[0].Rows[0]["IS_EXISTS"].ToString();
                if (strIS_EXISTS.ToUpper().Trim() == "Y")
                    return true;
                else
                    return false;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }


        /// <summary>
        ///  chk for the watercraft in Homeowners
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="polID"></param>
        /// <param name="polVersionID"></param>
        /// <returns></returns>
        private bool CheckWatercraft_Pol(int customerID, int polID, int polVersionID)
        {
            string strStoredProcName = "Proc_Chk_Watercraft_Exists_Pol", strIS_EXISTS = "";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@POLICY_ID", polID);
                objDataWrapper.AddParameter("@POL_VERSION_ID", polVersionID);
                ds = objDataWrapper.ExecuteDataSet(strStoredProcName);
                objDataWrapper.ClearParameteres();
                strIS_EXISTS = ds.Tables[0].Rows[0]["IS_EXISTS"].ToString();
                if (strIS_EXISTS.ToUpper().Trim() == "Y")
                    return true;
                else
                    return false;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        private DataSet GetQuoteOldXML(int customerID, int appID, int appVersionID)
        {
            string strStoredProcName = "Proc_AppQuoteOldInputXML";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@APP_ID", appID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
                ds = objDataWrapper.ExecuteDataSet(strStoredProcName);
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                objDataWrapper.ClearParameteres();
                return ds;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        // get the OLD xml for Policy if any 
        private DataSet GetQuoteOldXML_Policy(int customerID, int polID, int polVersionID, string strQuote_Type)
        {
            string strStoredProcName = "Proc_PolQuoteOldInputXML";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@POLICY_ID", polID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", polVersionID);
                objDataWrapper.AddParameter("@QUOTE_TYPE", strQuote_Type);
                ds = objDataWrapper.ExecuteDataSet(strStoredProcName);
                //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
                objDataWrapper.ClearParameteres();
                return ds;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }


        /// <summary>
        /// Accepts  and app version id.
        /// Returns an xmlstring 
        /// </summary>		
        /// <param name="inputXML">inputXML that has to be checked </param>
        /// <returns>string</returns>
        public string GetHO3QuoteXMLForQuickQuote(string inputXML)
        {

            try
            {
                string finalPremiumXML = "";
                string premiumXML = "", premiumXML_RV = "";
                string premiumXslPath = "";
                string strProductFactorPath = "";
                string strAdditionalText = "<?xml version='1.0'?> " +
                    "<?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?>" +
                    "<!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple'" +
                    " xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'>" +
                    " <!ATTLIST person id ID #IMPLIED>" +
                    "]>";
                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();

                if (inputXML != "<NewDataSet />" && inputXML != "")
                {
                    /* inputXML is of the form <QUICKQUOTE><DWELLLINGDETAILS> ... </DWELLLINGDETAILS></QUICKQUOTE>
                     * 1.  Run a loop to calculate the premium for each dwelling. 
                     * 2.  Save the output xml in another xmlstring. 
                     * 3.  add the <DWELLLINGDETAILS> tags for each premium.
                     * 4.  add the <PREMIUMXML> as the root node.
                     */


                    // To get the combined premium of multiple dwellings
                    string combinedPremiumComponent = "";

                    /* Loading the input xml into a document. This document will be used further to
                     * extract each dwelling node for premium calculation */
                    XmlDocument xmlTempDoc = new XmlDocument();
                    inputXML = "<INPUTXML>" + inputXML + "</INPUTXML>";
                    //inputXML = inputXML.Replace("'","h673GSUYD7G3J73UDH");
                    xmlTempDoc.LoadXml(inputXML);
                    XmlElement xmlTempElement = xmlTempDoc.DocumentElement;

                    //To Pick the State name
                    strStateName = xmlTempElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/STATENAME").InnerText;
                    string netpremium = xmlTempElement.SelectSingleNode("QUICKQUOTE/BILLINGINFOS/PLAN/NET_AMOUNT").InnerText;

                    //Initialising Final Premium variable
                    //finalPremiumXML="<PREMIUMXML>";
                    //Changed By Ravindra 
                    finalPremiumXML = "<PRIMIUM>";

                    /* Run a loop for each dwelling. <INPUTXML><QUICKQUOTE><DWELLINGDETAILS.... >..
                     * xmlTempElement.FirstChild.Childnodes coz xmlTempElement=<INPUTXML> */

                    foreach (XmlNode nodInput in xmlTempElement.FirstChild.SelectNodes("DWELLINGDETAILS"))
                    {

                        string premiumComponent = "<";
                        //	premiumComponent = "<" + nodInput.Name.Trim() ; 
                        //premiumComponent=premiumComponent + nodInput.Name.Trim() + " " + nodInput.Attributes[0].Name.Trim() + " = '"  + nodInput.Attributes[0].Value.Trim() +"' " + nodInput.Attributes[1].Name.Trim() + " = '"  + nodInput.Attributes[1].Value.Trim() + "' >" ;
                        premiumComponent = premiumComponent + strRisk.Trim() + " " + nodInput.Attributes[0].Name.Trim() + " = '" + nodInput.Attributes[0].Value.Trim() + "' " + strType.Trim() + " = 'HOME'" + "  " + nodInput.Attributes[1].Name.Trim() + " = '" + EncodeXMLCharacters(nodInput.Attributes[1].Value.Trim()) + "' >";

                        // Storing the Input XML 
                        //string premiumInput =	premiumComponent + nodInput.InnerXml + "</"+nodInput.Name+">";
                        string premiumInput = nodInput.OuterXml;//  + nodInput.InnerXml + "</"+nodInput.Name+">";

                        //premiumInput		=	inputXML;
                        //premiumInput		=	premiumInput.Replace("<INPUTXML>","");
                        //premiumInput		=	premiumInput.Replace("</INPUTXML>","");

                        //Geting the Product Name and State values from the Input XML
                        XmlDocument xmlCheckProductName = new XmlDocument();
                        xmlCheckProductName.LoadXml(premiumInput);



                        strProductFactorPath = GetProductFactorMasterPath(premiumInput, "HO");
                        premiumXslPath = GetProductPremiumPath(premiumInput, "HO");

                        QEngineDll.QuoteEngine strGetPath = new QEngineDll.QuoteEngine("<DWELLINGDETAILS ID=\"1\" ADDRESS=\"33 NORTH  SKJDLK; INDIANA 46001\" POLICYTYPE=\"HO-2 REPLACE\"><LOB_ID>1</LOB_ID><POLICY_ID></POLICY_ID> <STATENAME>INDIANA</STATENAME><NEWBUSINESSFACTOR>Y</NEWBUSINESSFACTOR><QUOTEEFFDATE>02/14/2012</QUOTEEFFDATE><QUOTEEXPDATE /><POL_NUMBER>H1000799</POL_NUMBER><POL_VERSION>1.00</POL_VERSION><TERMFACTOR>12</TERMFACTOR><SEASONALSECONDARY>N</SEASONALSECONDARY><WOLVERINEINSURESPRIMARY>N</WOLVERINEINSURESPRIMARY><PRODUCTNAME>HO-2</PRODUCTNAME><PRODUCT_PREMIER>REPLACE</PRODUCT_PREMIER><REPLACEMENTCOSTFACTOR>50000</REPLACEMENTCOSTFACTOR><DWELLING_LIMITS>50000</DWELLING_LIMITS><FIREPROTECTIONCLASS>01</FIREPROTECTIONCLASS><PROTECTIONCLASS>01</PROTECTIONCLASS><DISTANCET_FIRESTATION>5</DISTANCET_FIRESTATION><FEET2HYDRANT>1000 OR LESS</FEET2HYDRANT><DEDUCTIBLE>500</DEDUCTIBLE> <EXTERIOR_CONSTRUCTION>11852</EXTERIOR_CONSTRUCTION><EXTERIOR_CONSTRUCTION_DESC>ALUMINIUM</EXTERIOR_CONSTRUCTION_DESC><EXTERIOR_CONSTRUCTION_F_M>F</EXTERIOR_CONSTRUCTION_F_M><DOC>1974</DOC><AGEOFHOME>38</AGEOFHOME><NUMBEROFFAMILIES>1</NUMBEROFFAMILIES><NUMBEROFUNITS>0</NUMBEROFUNITS><PERSONALLIABILITY_LIMIT>100000.00</PERSONALLIABILITY_LIMIT>  <PERSONALLIABILITY_PREMIUM>0</PERSONALLIABILITY_PREMIUM>  <PERSONALLIABILITY_DEDUCTIBLE>0</PERSONALLIABILITY_DEDUCTIBLE>  <MEDICALPAYMENTSTOOTHERS_LIMIT>1000.00</MEDICALPAYMENTSTOOTHERS_LIMIT>  <MEDICALPAYMENTSTOOTHERS_PREMIUM>0</MEDICALPAYMENTSTOOTHERS_PREMIUM>  <MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE>0</MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE>  <FORM_CODE>01F</FORM_CODE>  <HO20>Y</HO20>  <HO21>N</HO21>  <HO25 />  <HO23>N</HO23>  <HO22>N</HO22>  <HO24>N</HO24>  <HO34>Y</HO34>  <HO11>N</HO11>  <HO32>N</HO32>  <HO277>N</HO277>  <HO455>N</HO455>  <HO327>0</HO327>  <HO33>N</HO33>  <HO315>N</HO315>  <HO9>N</HO9>  <HO287>N</HO287>  <HO42>N</HO42>  <HO96FINALVALUE>0.00</HO96FINALVALUE>  <HO96INCLUDE>0</HO96INCLUDE>  <HO96ADDITIONAL>0</HO96ADDITIONAL>  <HO48INCLUDE>5000</HO48INCLUDE>  <HO48ADDITIONAL>0</HO48ADDITIONAL>  <HO40INCLUDE></HO40INCLUDE>  <HO40ADDITIONAL>0.00</HO40ADDITIONAL>  <HO42ADDITIONAL>0.00</HO42ADDITIONAL>  <PERSONALPROPERTYINCREASEDLIMITINCLUDE>35000</PERSONALPROPERTYINCREASEDLIMITINCLUDE>  <PERSONALPROPERTYINCREASEDLIMITADDITIONAL>0</PERSONALPROPERTYINCREASEDLIMITADDITIONAL>  <PERSONALPROPERTYAWAYINCLUDE>0.00</PERSONALPROPERTYAWAYINCLUDE>  <PERSONALPROPERTYAWAYADDITIONAL>0.00</PERSONALPROPERTYAWAYADDITIONAL>  <UNSCHEDULEDJEWELRYINCLUDE>0.00</UNSCHEDULEDJEWELRYINCLUDE>  <UNSCHEDULEDJEWELRYADDITIONAL>0.00</UNSCHEDULEDJEWELRYADDITIONAL>  <REPAIRCOSTADDITIONAL />  <MONEYINCLUDE>0.00</MONEYINCLUDE>  <MONEYADDITIONAL>0.00</MONEYADDITIONAL>  <SECURITIESINCLUDE>0.00</SECURITIESINCLUDE>  <SECURITIESADDITIONAL>0.00</SECURITIESADDITIONAL>  <SILVERWAREINCLUDE>0.00</SILVERWAREINCLUDE>  <SILVERWAREADDITIONAL>0.00</SILVERWAREADDITIONAL>  <FIREARMSINCLUDE>0.00</FIREARMSINCLUDE>  <FIREARMSADDITIONAL>0.00</FIREARMSADDITIONAL>  <HO312INCLUDE>0.00</HO312INCLUDE>  <HO312ADDITIONAL>0.00</HO312ADDITIONAL>  <LOSSOFUSE_LIMIT>15000</LOSSOFUSE_LIMIT>  <ADDITIONALLIVINGEXPENSEADDITIONAL>0.00</ADDITIONALLIVINGEXPENSEADDITIONAL>  <ADDITIONALLIVINGEXPENSEINCLUDE>15000</ADDITIONALLIVINGEXPENSEINCLUDE>  <HO53INCLUDE>0.00</HO53INCLUDE>  <HO53ADDITIONAL>0.00</HO53ADDITIONAL>  <HO35INCLUDE>0</HO35INCLUDE>  <HO35ADDITIONAL>0.00</HO35ADDITIONAL>  <SPECIFICSTRUCTURESADDITIONAL>0</SPECIFICSTRUCTURESADDITIONAL>  <HO493>N</HO493>  <OCCUPIED_INSURED>0.00</OCCUPIED_INSURED>  <RESIDENCE_PREMISES>0.00</RESIDENCE_PREMISES>  <OTHER_LOC_1FAMILY>0.00</OTHER_LOC_1FAMILY>  <OTHER_LOC_2FAMILY>0.00</OTHER_LOC_2FAMILY>  <ONPREMISES_HO42>N</ONPREMISES_HO42>  <LOCATED_OTH_STRUCTURE />  <INSTRUCTIONONLY_HO42>N</INSTRUCTIONONLY_HO42>  <OFF_PREMISES_HO43>N</OFF_PREMISES_HO43>  <PIP_HO82>N</PIP_HO82>  <HO200>N</HO200>  <HO64RENTERDELUXE>N</HO64RENTERDELUXE>  <HO66CONDOMINIUMDELUXE>N</HO66CONDOMINIUMDELUXE>  <RESIDENCE_EMP_NUMBER>0</RESIDENCE_EMP_NUMBER>  <CLERICAL_OFFICE_HO71>N</CLERICAL_OFFICE_HO71>  <SALESMEN_INC_INSTALLATION>N</SALESMEN_INC_INSTALLATION>  <TEACHER_ATHELETIC>N</TEACHER_ATHELETIC>  <TEACHER_NOC>N</TEACHER_NOC>  <INCIDENTAL_FARMING_HO72>N</INCIDENTAL_FARMING_HO72>  <OTH_LOC_OPR_EMPL_HO73>0.00</OTH_LOC_OPR_EMPL_HO73>  <OTH_LOC_OPR_OTHERS_HO73>0</OTH_LOC_OPR_OTHERS_HO73>  <LOSSFREE>Y</LOSSFREE>  <NOTLOSSFREE>N</NOTLOSSFREE>  <VALUEDCUSTOMER>N</VALUEDCUSTOMER>  <MULTIPLEPOLICYFACTOR>Y</MULTIPLEPOLICYFACTOR>  <NONSMOKER>N</NONSMOKER>  <EXPERIENCE>28</EXPERIENCE>  <CONSTRUCTIONCREDIT>N</CONSTRUCTIONCREDIT>  <REDUCTION_IN_COVERAGE_C>N</REDUCTION_IN_COVERAGE_C>  <N0_LOCAL_ALARM>0</N0_LOCAL_ALARM>  <BURGLER_ALERT_POLICE>N</BURGLER_ALERT_POLICE>  <FIRE_ALARM_FIREDEPT>N</FIRE_ALARM_FIREDEPT>  <BURGLAR>N</BURGLAR>  <CENTRAL_FIRE>N</CENTRAL_FIRE>  <CENT_ST_BURG_FIRE>N</CENT_ST_BURG_FIRE>  <DIR_FIRE_AND_POLICE>N</DIR_FIRE_AND_POLICE>  <LOC_FIRE_GAS>N</LOC_FIRE_GAS>  <TWO_MORE_FIRE>N</TWO_MORE_FIRE>  <INSURANCESCORE>100</INSURANCESCORE>  <INSURANCESCOREDIS />  <WOODSTOVE_SURCHARGE>N</WOODSTOVE_SURCHARGE>  <PRIOR_LOSS_SURCHARGE>N</PRIOR_LOSS_SURCHARGE>  <DOGSURCHARGE>0</DOGSURCHARGE>  <DOGFACTOR>N</DOGFACTOR>  <SCH_BICYCLE_DED>0</SCH_BICYCLE_DED>  <SCH_BICYCLE_AMOUNT>0</SCH_BICYCLE_AMOUNT>  <SCH_CAMERA_DED>0</SCH_CAMERA_DED>  <SCH_CAMERA_AMOUNT>0</SCH_CAMERA_AMOUNT>  <SCH_CELL_DED>0</SCH_CELL_DED>  <SCH_CELL_AMOUNT>0</SCH_CELL_AMOUNT>  <SCH_FURS_DED>0</SCH_FURS_DED>  <SCH_FURS_AMOUNT>0</SCH_FURS_AMOUNT>  <SCH_GUNS_DED>0</SCH_GUNS_DED>  <SCH_GUNS_AMOUNT>0</SCH_GUNS_AMOUNT>  <SCH_GOLF_DED>0</SCH_GOLF_DED>  <SCH_GOLF_AMOUNT>0</SCH_GOLF_AMOUNT>  <SCH_JWELERY_DED>0</SCH_JWELERY_DED>  <SCH_JWELERY_AMOUNT>0</SCH_JWELERY_AMOUNT>  <SCH_MUSICAL_DED>0</SCH_MUSICAL_DED>  <SCH_MUSICAL_AMOUNT>0</SCH_MUSICAL_AMOUNT>  <SCH_PERSCOMP_DED>0</SCH_PERSCOMP_DED>  <SCH_PERSCOMP_AMOUNT>0</SCH_PERSCOMP_AMOUNT>  <SCH_SILVER_DED>0</SCH_SILVER_DED>  <SCH_SILVER_AMOUNT>0</SCH_SILVER_AMOUNT>  <SCH_STAMPS_DED>0</SCH_STAMPS_DED>  <SCH_STAMPS_AMOUNT>0</SCH_STAMPS_AMOUNT>  <SCH_RARECOINS_DED>0</SCH_RARECOINS_DED>  <SCH_RARECOINS_AMOUNT>0</SCH_RARECOINS_AMOUNT>  <SCH_FINEARTS_WO_BREAK_DED>0</SCH_FINEARTS_WO_BREAK_DED>  <SCH_FINEARTS_WO_BREAK_AMOUNT>0</SCH_FINEARTS_WO_BREAK_AMOUNT>  <SCH_FINEARTS_BREAK_DED>0</SCH_FINEARTS_BREAK_DED>  <SCH_FINEARTS_BREAK_AMOUNT>0</SCH_FINEARTS_BREAK_AMOUNT>  <SCH_BICYCLE_DED1>0</SCH_BICYCLE_DED1>  <SCH_BICYCLE_AMOUNT1>0</SCH_BICYCLE_AMOUNT1>  <SCH_CAMERA_DED1>0</SCH_CAMERA_DED1>  <SCH_CAMERA_AMOUNT1>0</SCH_CAMERA_AMOUNT1>  <SCH_CELL_DED1>0</SCH_CELL_DED1>  <SCH_CELL_AMOUNT1>0</SCH_CELL_AMOUNT1>  <SCH_FURS_DED1>0</SCH_FURS_DED1>  <SCH_FURS_AMOUNT1>0</SCH_FURS_AMOUNT1>  <SCH_GUNS_DED1>0</SCH_GUNS_DED1>  <SCH_GUNS_AMOUNT1>0</SCH_GUNS_AMOUNT1>  <SCH_GOLF_DED1>0</SCH_GOLF_DED1>  <SCH_GOLF_AMOUNT1>0</SCH_GOLF_AMOUNT1>  <SCH_JWELERY_DED1>0</SCH_JWELERY_DED1>  <SCH_JWELERY_AMOUNT1>0</SCH_JWELERY_AMOUNT1>  <SCH_MUSICAL_DED1>0</SCH_MUSICAL_DED1>  <SCH_MUSICAL_AMOUNT1>0</SCH_MUSICAL_AMOUNT1>  <SCH_PERSCOMP_DED1>0</SCH_PERSCOMP_DED1>  <SCH_PERSCOMP_AMOUNT1>0</SCH_PERSCOMP_AMOUNT1>  <SCH_SILVER_DED1>0</SCH_SILVER_DED1>  <SCH_SILVER_AMOUNT1>0</SCH_SILVER_AMOUNT1>  <SCH_STAMPS_DED1>0</SCH_STAMPS_DED1>  <SCH_STAMPS_AMOUNT1>0</SCH_STAMPS_AMOUNT1>  <SCH_RARECOINS_DED1>0</SCH_RARECOINS_DED1>  <SCH_RARECOINS_AMOUNT1>0</SCH_RARECOINS_AMOUNT1>  <SCH_FINEARTS_WO_BREAK_DED1>0</SCH_FINEARTS_WO_BREAK_DED1>  <SCH_FINEARTS_WO_BREAK_AMOUNT1>0</SCH_FINEARTS_WO_BREAK_AMOUNT1>  <SCH_FINEARTS_BREAK_DED1>0</SCH_FINEARTS_BREAK_DED1>  <SCH_FINEARTS_BREAK_AMOUNT1>0</SCH_FINEARTS_BREAK_AMOUNT1>  <SCH_HANDICAP_ELECTRONICS_DED>0</SCH_HANDICAP_ELECTRONICS_DED>  <SCH_HANDICAP_ELECTRONICS_AMOUNT>0</SCH_HANDICAP_ELECTRONICS_AMOUNT>  <SCH_HEARING_AIDS_DED>0</SCH_HEARING_AIDS_DED>  <SCH_HEARING_AIDS_AMOUNT>0</SCH_HEARING_AIDS_AMOUNT>  <SCH_INSULIN_PUMPS_DED>0</SCH_INSULIN_PUMPS_DED>  <SCH_INSULIN_PUMPS_AMOUNT>0</SCH_INSULIN_PUMPS_AMOUNT>  <SCH_MART_KAY_DED>0</SCH_MART_KAY_DED>  <SCH_MART_KAY_AMOUNT>0</SCH_MART_KAY_AMOUNT> <SCH_PERSONAL_COMPUTERS_LAPTOP_DED>0</SCH_PERSONAL_COMPUTERS_LAPTOP_DED>  <SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT>0</SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT>  <SCH_SALESMAN_SUPPLIES_DED>0</SCH_SALESMAN_SUPPLIES_DED>  <SCH_SALESMAN_SUPPLIES_AMOUNT>0</SCH_SALESMAN_SUPPLIES_AMOUNT>  <SCH_SCUBA_DRIVING_DED>0</SCH_SCUBA_DRIVING_DED>  <SCH_SCUBA_DRIVING_AMOUNT>0</SCH_SCUBA_DRIVING_AMOUNT>  <SCH_SNOW_SKIES_DED>0</SCH_SNOW_SKIES_DED>  <SCH_SNOW_SKIES_AMOUNT>0</SCH_SNOW_SKIES_AMOUNT>  <SCH_TACK_SADDLE_DED>0</SCH_TACK_SADDLE_DED>  <SCH_TACK_SADDLE_AMOUNT>0</SCH_TACK_SADDLE_AMOUNT>  <SCH_TOOLS_PREMISES_DED>0</SCH_TOOLS_PREMISES_DED>  <SCH_TOOLS_PREMISES_AMOUNT>0</SCH_TOOLS_PREMISES_AMOUNT>  <SCH_TOOLS_BUSINESS_DED>0</SCH_TOOLS_BUSINESS_DED>  <SCH_TOOLS_BUSINESS_AMOUNT>0</SCH_TOOLS_BUSINESS_AMOUNT>  <SCH_TRACTORS_DED>0</SCH_TRACTORS_DED>  <SCH_TRACTORS_AMOUNT>0</SCH_TRACTORS_AMOUNT>  <SCH_TRAIN_COLLECTIONS_DED>0</SCH_TRAIN_COLLECTIONS_DED>  <SCH_TRAIN_COLLECTIONS_AMOUNT>0</SCH_TRAIN_COLLECTIONS_AMOUNT>  <SCH_WHEELCHAIRS_DED>0</SCH_WHEELCHAIRS_DED>  <SCH_WHEELCHAIRS_AMOUNT>0</SCH_WHEELCHAIRS_AMOUNT>  <SCH_CAMERA_PROF_AMOUNT>0</SCH_CAMERA_PROF_AMOUNT>  <SCH_CAMERA_PROF_DED>0</SCH_CAMERA_PROF_DED>  <SCH_MUSICAL_REMUN_AMOUNT>0</SCH_MUSICAL_REMUN_AMOUNT>  <SCH_MUSICAL_REMUN_DED>0</SCH_MUSICAL_REMUN_DED>  <MINESUBSIDENCE_ADDITIONAL>0</MINESUBSIDENCE_ADDITIONAL>  <TERRITORYCODES>11</TERRITORYCODES>  <EARTHQUAKEZONE>5</EARTHQUAKEZONE>  <COVERAGEVALUE>50</COVERAGEVALUE>  <DWELLING>N</DWELLING>  <YEARS>0</YEARS>  <PREMIUMGROUP>REPLACE</PREMIUMGROUP>  <OTHERSTRUCTURES_LIMIT>0</OTHERSTRUCTURES_LIMIT>  <PERSONALPROPERTY_LIMIT>35000</PERSONALPROPERTY_LIMIT>  <AGEHOMECREDIT>Y</AGEHOMECREDIT><POLICYTYPE>11192</POLICYTYPE>  <VALUESCUSTOMERAPP>N</VALUESCUSTOMERAPP>  <PRIORLOSSSURCHARGE>N</PRIORLOSSSURCHARGE>  <TERRITORYNAME>ALEXANDRIA </TERRITORYNAME>  <TERRITORYCOUNTY>MADISON </TERRITORYCOUNTY>  <BREEDOFDOG />  <ISACTIVE>Y</ISACTIVE>  <SUBPROTDIS>N</SUBPROTDIS>  <YEARSCONTINSURED>0</YEARSCONTINSURED>  <YEARSCONTINSUREDWITHWOLVERINE>0</YEARSCONTINSUREDWITHWOLVERINE>  <TOTALLOSS>0</TOTALLOSS>  <HO42IOOPDS />  <PREMIER_DISCOUNT>N</PREMIER_DISCOUNT></DWELLINGDETAILS>", premiumXslPath, strProductFactorPath);
                        string shortestPath = strGetPath.GetPath();
                        premiumXML = strGetPath.CalculatePremium(shortestPath.Trim());




                        //added by praveen singh 
                        string str_Title = "";
                        string RATING_HEADER = "";
                        RATING_HEADER = ClsCommon.GetKeyValue("Rating_Header");
                        str_Title = "<HEADER TITLE='" + RATING_HEADER + "'> </HEADER></PRIMIUM>";
                        premiumXML = premiumXML.Replace("</PRIMIUM>", str_Title);

                        //to add the Business type 
                        NEW_BUSINESS = xmlTempElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/NEWBUSINESSFACTOR").InnerText;

                        if (NEW_BUSINESS.ToString().ToUpper().Trim() == "TRUE" || NEW_BUSINESS.ToString().ToUpper().Trim() == "Y")
                        {
                            BUSINESS_TYPE = "New";
                            strBusinessType = "New";//Added by Manoj 16.Sep.2009
                        }
                        else
                        {
                            BUSINESS_TYPE = "Ren";
                            strBusinessType = "Ren";//Added by Manoj 16.Sep.2009
                        }

                        //to add the Qeffective date rate and effective date 						
                        QUOTE_DATE = " QUOTE_DATE='" + DateTime.Now.ToShortDateString().ToString() + "'";
                        QUOTE_EFFECTIVE_DATE = " QUOTE_EFFECTIVE_DATE='" + string.Format(QUOTE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";
                        RATE_EFFECTIVE_DATE = " RATE_EFFECTIVE_DATE='" + string.Format(RATE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";
                        BUSINESS_TYPE = " BUSINESS_TYPE='" + BUSINESS_TYPE + "'";

                        string final_string = "<HEADER " + QUOTE_DATE + QUOTE_EFFECTIVE_DATE + RATE_EFFECTIVE_DATE + BUSINESS_TYPE + " ";

                        premiumXML = premiumXML.Replace("<HEADER", final_string);

                        //Get the Primary App Nodes
                        XmlDocument xmlAppDoc = new XmlDocument();
                        //string strInputAppXML = "<INPUTXML>"+inputXML+"</INPUTXML>";
                        string strInputAppXML = inputXML;
                        string strFName = "";
                        string strLName = "";
                        string strMName = "";
                        string strAdd1 = "";
                        string strAdd2 = "";
                        string strCity = "";
                        string strState = "";
                        string strZipcode = "";
                        string strStateCode = "";
                        //AGency Info
                        string strAgencyAdd1 = "";
                        string strAgencyAdd2 = "";
                        string strAgencyCity = "";
                        string strAgencyState = "";
                        string strAgencyZip = "";
                        string strAgencyName = "";
                        string strAgencyPhone = "";
                        //QQ,APP,POL Info				
                        string strQQNumber = "";
                        string strAPPNumber = "";
                        string strAPPVersion = "";
                        string strPOLNumber = "";
                        string strPOLVersion = "";

                        /*string strAgencyName = "";
                        string strAgencyPhone="";     //Agency Phone
                        string strStateCode ="";
                        string strQQNumber = "";
                        string strAPPNumber = "";
                        string strAPPVersion = "";
                        string strPOLNumber = "";
                        string strPOLVersion = "";*/

                        strInputAppXML = strInputAppXML.Replace("&", "&amp;");
                        xmlAppDoc.LoadXml(strInputAppXML);
                        XmlElement xmlAppElement = xmlAppDoc.DocumentElement;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/FIRST_NAME") != null)
                            strFName = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/FIRST_NAME").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/MIDDLE_NAME") != null)
                            strMName = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/MIDDLE_NAME").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/LAST_NAME") != null)
                            strLName = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/LAST_NAME").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/ADDRESS1") != null)
                            strAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/ADDRESS1").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/ADDRESS2") != null)
                            strAdd2 = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/ADDRESS2").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/CITY") != null)
                            strCity = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/CITY").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/STATE") != null)
                            strState = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/STATE").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/ZIP_CODE") != null)
                            strZipcode = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/ZIP_CODE").InnerText;

                        //AGency Info

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_ADD1") != null)
                            strAgencyAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_ADD1").InnerText;

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_ADD1") != null)
                            strAgencyAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_ADD1").InnerText;

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_CITY") != null)
                            strAgencyCity = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_CITY").InnerText;


                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_STATE") != null)
                            strAgencyState = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_STATE").InnerText;

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_ZIP") != null)
                            strAgencyZip = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_ZIP").InnerText;


                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME") != null)
                            strAgencyName = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME").InnerText;

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_PHONE") != null)
                            strAgencyPhone = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_PHONE").InnerText;

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE_CODE") != null)
                            strStateCode = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE_CODE").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/QQNUMBER") != null)
                            strQQNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/QQNUMBER").InnerText.ToString().Trim();
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APP_NUMBER") != null)
                            strAPPNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APP_NUMBER").InnerText.ToString().Trim();
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APP_VERSION") != null)
                            strAPPVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APP_VERSION").InnerText.ToString().Trim();
                        //POL
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/POL_NUMBER") != null)
                            strPOLNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/POL_NUMBER").InnerText.ToString().Trim();
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/POL_VERSION") != null)
                            strPOLVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/POL_VERSION").InnerText.ToString().Trim();



                        string strClientTop = "<CLIENT_TOP_INFO   PRIMARY_APP_NAME ='" + strFName + "'" +
                            " PRIMARY_APP_LNAME ='" + strLName + "'" +
                            " PRIMARY_APP_ADDRESS1 ='" + strAdd1 + "'" +
                            " PRIMARY_APP_ADDRESS2 ='" + strAdd2 + "'" +
                            " PRIMARY_APP_CITY ='" + strCity + "'" +
                            " PRIMARY_APP_STATE ='" + strState + "'" +
                            " PRIMARY_APP_ZIPCODE ='" + strZipcode + "'" +

                            " AGENCY_ADD1='" + strAgencyAdd1 + "'" +
                            " AGENCY_ADD2='" + strAgencyAdd2 + "'" +
                            " AGENCY_CITY='" + strAgencyCity + "'" +
                            " AGENCY_STATE='" + strAgencyState + "'" +
                            " AGENCY_ZIP='" + strAgencyZip + "'" +

                            " AGENCY_NAME='" + strAgencyName + "'" +
                            " AGENCY_PHONE='" + strAgencyPhone + "'" +
                            " QQ_NUMBER='" + strQQNumber + "'" +
                            " APP_NUMBER='" + strAPPNumber + "'" +
                            " APP_VERSION='" + strAPPVersion + "'" +
                            " POL_NUMBER='" + strPOLNumber + "'" +
                            " POL_VERSION='" + strPOLVersion + "'" +
                            " STATE_CODE ='" + strStateCode + "'>";


                        /*" AGENCY_NAME='" + strAgencyName + "'" +
                            " AGENCY_PHONE='" + strAgencyPhone + "'" +
                            " QQ_NUMBER='" + strQQNumber + "'" +
                            " APP_NUMBER='" + strAPPNumber + "'" +
                            " APP_VERSION='" + strAPPVersion + "'" +
                            " POL_NUMBER='" + strPOLNumber + "'" +
                            " POL_VERSION='" + strPOLVersion + "'" +
                            " STATE_CODE ='" + strStateCode  + "'>";*/

                        premiumXML = premiumXML.Replace("</HEADER>", "</HEADER>" + strClientTop + "</CLIENT_TOP_INFO>");

                        //End Primary Applicants Nodes




                        //
                        XmlDocument xmlTempPremiumXML = new XmlDocument();
                        xmlTempPremiumXML.LoadXml(premiumXML);
                        //
                        XmlElement xmlTempPremiumElement = xmlTempPremiumXML.DocumentElement;
                        premiumComponent = premiumComponent + xmlTempPremiumElement.InnerXml + "</" + strRisk.Trim() + ">";
                        //
                        combinedPremiumComponent = combinedPremiumComponent + premiumComponent;
                    }

                    // for the recreational vehicle part.

                    /* 1. check if any recreational vehicle exists in the inputxml
                         * 2. if recreatin vehicle exists then run a loop for all recr vehicles and get the premium.
                         *  3. add the premium to the ho premium output
                         */
                    XmlNode nodRecreationVehicles = xmlTempElement.FirstChild.SelectSingleNode("RECREATIONVEHICLES"); // WILL B UNCOMMENTED LATER

                    premiumXML_RV = "";
                    string consolidatePremium_RV = "";
                    if (nodRecreationVehicles != null && nodRecreationVehicles.ChildNodes.Count > 0)
                    {
                        foreach (XmlNode nodRecreationVehicle in nodRecreationVehicles.ChildNodes)
                        {
                            strProductFactorPath = GetProductFactorMasterPath(nodRecreationVehicle.OuterXml, "RV");
                            premiumXslPath = GetProductPremiumPath(nodRecreationVehicle.OuterXml, "RV");
                            QEngineDll.QuoteEngine strGetPathForRV = new QEngineDll.QuoteEngine(nodRecreationVehicle.OuterXml, premiumXslPath, strProductFactorPath);
                            string shortestPathForRV = strGetPathForRV.GetPath();
                            premiumXML_RV = strGetPathForRV.CalculatePremium(shortestPathForRV.Trim());

                            premiumXML_RV = premiumXML_RV.Replace("<PRIMIUM>", "");
                            premiumXML_RV = premiumXML_RV.Replace("</PRIMIUM>", "");
                            //consolidatePremium_RV = consolidatePremium_RV +  "<RECREATIONVEHICLE>"+ premiumXML_RV.Replace(strAdditionalText,"") +"</RECREATIONVEHICLE>";
                            string riskId = "";
                            const string riskRVType = "RV";
                            if (nodRecreationVehicle.Attributes["ID"] != null)
                                riskId = nodRecreationVehicle.Attributes["ID"].Value.ToString();

                            consolidatePremium_RV = consolidatePremium_RV + "<RISK ID= '" + riskId + "'  TYPE = '" + riskRVType + "'>" + premiumXML_RV.Replace(strAdditionalText, "") + "</RISK>";
                        }
                    }

                    // end -recreational vehicle
                    //Final Premium XML
                    finalPremiumXML = finalPremiumXML + combinedPremiumComponent + consolidatePremium_RV + "</PRIMIUM>";

                    // appened final premium fetched after parsing from xsl
                    //1.	fetch step premium of each step of each risk.
                    //2.	Add all step premiums.
                    //3.	appened total premium with sum of all step premiums.

                    // access premium xml

                    XmlDocument docpremiumInput = new XmlDocument();
                    docpremiumInput.LoadXml(finalPremiumXML);


                    XmlNode nodTempstepPremium, nodTempdwellingstepInPremium, nodTempdwellingSumTotalPremium;
                    //XmlNodeList nodedwellingstep, nodervstep;
                    string premiumValueInPremium = "";//, dwellingid = "";
                    int countOfrisk = 0, TotalPremium = 0;//, countofstep = 0, rvrisk = 0, totalriskpemium = 0;

                    // count number of Dwellings and rvs
                    XmlElement nodTotalPremium;
                    XmlNodeList noddwellingInInput = docpremiumInput.SelectNodes("PRIMIUM/RISK");
                    countOfrisk = noddwellingInInput.Count;
                    ArrayList FinaleSteppremium = new ArrayList();
                    // for each risk take sumtotal of risk and add it to final
                    //nodedwellingstep = docpremiumInput.SelectNodes("PRIMIUM/RISK");
                    foreach (XmlNode nod in noddwellingInInput)
                    {
                        nodTempstepPremium = nod.SelectSingleNode("STEP[@COMPONENT_CODE ='SUMTOTAL']");
                        if (nodTempstepPremium != null)
                        {
                            premiumValueInPremium = nodTempstepPremium.Attributes["STEPPREMIUM"].Value.ToString().Trim();
                            if (premiumValueInPremium != "")
                            {
                                bool IsNumeric = false;
                                try
                                {
                                    int iTest = Int32.Parse(premiumValueInPremium);
                                    IsNumeric = true;
                                }
                                catch (Exception ex)
                                {
                                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                                    IsNumeric = false;
                                }
                                if (IsNumeric)
                                {
                                    TotalPremium += System.Convert.ToInt32(premiumValueInPremium);

                                }
                            }
                        }
                    }
                    /*for (int count =1; count<=countOfrisk;count++)
                    {
                        //itrack # 6254 13 aug 09 -Manoj
                        int index=count -1;
                        string RiskID=noddwellingInInput.Item(index).Attributes["ID",""].Value;

                        dwellingid = "RISK"+count.ToString();
                        nodedwellingstep = docpremiumInput.SelectNodes("PRIMIUM/RISK[@ID='"+RiskID.ToString()+"' and @TYPE='HOME']/STEP");
                        nodervstep =  docpremiumInput.SelectNodes("PRIMIUM/RISK[@ID='"+RiskID.ToString()+"' and @TYPE='RV']/STEP");
                        if(nodedwellingstep != null)
                        {
                            countofstep = nodedwellingstep.Count;
						
                            // fetching step premium
                            for(int stepcount=0; stepcount<=countofstep; stepcount++)
                            {	
                                //nodTempdwellingstepInPremium =	docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+count.ToString()+"' and @TYPE='HOME']/STEP[@TS='"+stepcount.ToString()+"' and @COMPONENT_CODE !='SUMTOTAL']/@STEPPREMIUM");
                                //itrack # 6254 13 aug 09 -Manoj
                                nodTempdwellingstepInPremium =	docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+RiskID.ToString()+"' and @TYPE='HOME']/STEP[@TS='"+stepcount.ToString()+"' and @COMPONENT_CODE !='SUMTOTAL']/@STEPPREMIUM");

                                if (nodTempdwellingstepInPremium != null)
                                {
                                    premiumValueInPremium = nodTempdwellingstepInPremium.InnerText;
                                    if(premiumValueInPremium !="")
                                    {
                                        bool IsNumeric = false;
                                        try
                                        {
                                            int iTest = Int32.Parse(premiumValueInPremium);
                                            IsNumeric = true;
                                        }
                                        catch(Exception ex)
                                        {
                                            IsNumeric = false;
                                        }
                                        if(IsNumeric)
                                        {
                                            TotalPremium += System.Convert.ToInt32(premiumValueInPremium); 
																				
                                        }
                                    }	
								
                                }
                            }
										
                        }

                        // add RV premium steps
                        if(nodervstep != null)
                        {
                            countofstep = nodervstep.Count;
						
                            // fetching step premium
                            for(int stepcount=0; stepcount<=countofstep; stepcount++)
                            {
                                //nodTempdwellingstepInPremium =	docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+count.ToString()+"' and @TYPE='RV']/STEP[@TS='"+stepcount.ToString()+"' and @COMPONENT_CODE !='SUMTOTAL']/@STEPPREMIUM");
                                //itrack # 6254 13 aug 09 -Manoj
                                nodTempdwellingstepInPremium =	docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+RiskID.ToString()+"' and @TYPE='RV']/STEP[@TS='"+stepcount.ToString()+"' and @COMPONENT_CODE !='SUMTOTAL']/@STEPPREMIUM");

                                if (nodTempdwellingstepInPremium != null)
                                {
                                    premiumValueInPremium = nodTempdwellingstepInPremium.InnerText;
                                    if(premiumValueInPremium !="")
                                    {
                                        bool IsNumeric = false;
                                        try
                                        {
                                            int iTest = Int32.Parse(premiumValueInPremium);
                                            IsNumeric = true;
                                        }
                                        catch(Exception ex)
                                        {
                                            IsNumeric = false;
                                        }
                                        if(IsNumeric)
                                        {
                                            TotalPremium += System.Convert.ToInt32(premiumValueInPremium); 
                                        }
                                    }	
								
                                }
                            }

                        }
						
                    }*/

                    // appending final premium
                    nodTempdwellingstepInPremium = docpremiumInput.SelectSingleNode("PRIMIUM");
                    //TotalPremium = (TotalPremium - System.Convert.ToInt32(nodTempdwellingstepInPremium.InnerText));
                    if (countOfrisk == 1)
                    {
                        //itrack # 6254 13-aug-09 -Manoj
                        string strRiskID = noddwellingInInput.Item(0).Attributes["ID", ""].Value;
                        nodTempdwellingSumTotalPremium = docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='" + strRiskID.ToString() + "' and @TYPE='HOME']/STEP[@COMPONENT_CODE ='SUMTOTAL']/@STEPPREMIUM");
                        nodTempdwellingSumTotalPremium.InnerText = TotalPremium.ToString();
                    }
                    nodTotalPremium = docpremiumInput.CreateElement("GRANDTOTAL");
                    nodTotalPremium.InnerText = netpremium;
                    nodTempdwellingstepInPremium.InsertAfter(nodTotalPremium, nodTempdwellingstepInPremium.LastChild);
                    finalPremiumXML = docpremiumInput.InnerXml;
                }

                return finalPremiumXML;

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }

        public string GetRentaDewllingForQuickQuote(string inputXML)
        {

            try
            {
                string finalPremiumXML = "";
                string premiumXML = "";
                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();

                if (inputXML != "<NewDataSet />" && inputXML != "")
                {
                    /* strReturnXML is of the form <INPUTXML><DWELLLINGDETAILS> ... </DWELLLINGDETAILS></INPUTXML>
                     * 1.  Run a loop to calculate the premium for each dwelling. 
                     * 2.  Save the output xml in another xmlstring. 
                     * 3.  add the <DWELLLINGDETAILS> tags for each premium.
                     * 4.  add the <PREMIUM> as the root node.
                     */
                    /* Set Flag for InputVerificationForQuote */
                    string strVerify = ClsCommon.GetKeyValueForSetup("InputVerificationForQuote");//System.Configuration.ConfigurationSettings.AppSettings.Get("InputVerificationForQuote").ToString();
                    if (strVerify == "Y")
                    {
                        //######################CHECK INPUT XML#########################
                        string xmlVerificationHTML, str_returnValue;
                        xmlVerificationHTML = InputXmlVerification(inputXML, "6");
                        int l1, l2;
                        l1 = xmlVerificationHTML.LastIndexOf("<returnValue>");
                        l2 = xmlVerificationHTML.Length;
                        str_returnValue = "";

                        if (l1 > 0)
                        {
                            str_returnValue = xmlVerificationHTML.Substring(l1, l2 - l1);
                        }

                        str_returnValue = str_returnValue.Replace("<returnValue>", "");
                        str_returnValue = str_returnValue.Replace("</returnValue>", "");
                        str_returnValue = str_returnValue.Replace("</span>", "");
                        if (str_returnValue.ToString() == "0")
                        {
                            //appQuodeID =0 ;
                            return xmlVerificationHTML;
                        }
                    }
                    //######################CHECK INPUT XML##########################

                    string combinedPremiumComponent = "";

                    XmlDocument xmlTempDoc = new XmlDocument();
                    inputXML = inputXML.Replace("&AMP;", "");
                    inputXML = inputXML.Replace("&", "&amp;");
                    //inputXML = inputXML.Replace("'","h673GSUYD7G3J73UDH");
                    inputXML = "<INPUTXML>" + inputXML + "</INPUTXML>";
                    xmlTempDoc.LoadXml(inputXML);

                    XmlElement xmlTempElement = xmlTempDoc.DocumentElement;

                    //To Pick the State name
                    strStateName = xmlTempElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/STATENAME").InnerText;

                    //finalPremiumXML="<PREMIUMXML>";
                    //Changed By Ravindra 
                    finalPremiumXML = "<PRIMIUM>";
                    //foreach (XmlNode nodChildNode in xmlTempElement.ChildNodes)
                    //{	
                    foreach (XmlNode nodInput in xmlTempElement.FirstChild.SelectNodes("DWELLINGDETAILS"))
                    {
                        //
                        string premiumComponent = "<";
                        //premiumComponent=premiumComponent + nodInput.Name.Trim() + " " + nodInput.Attributes[0].Name.Trim() + " = '"  + nodInput.Attributes[0].Value.Trim() +"' " + nodInput.Attributes[1].Name.Trim() + " = '"  + nodInput.Attributes[1].Value.Trim() + "' >" ;
                        premiumComponent = premiumComponent + strRisk.Trim() + " " + nodInput.Attributes[0].Name.Trim() + " = '" + nodInput.Attributes[0].Value.Trim() + "' " + strType.Trim() + " = 'REDW'" + "  " + nodInput.Attributes[1].Name.Trim() + " = '" + EncodeXMLCharacters(nodInput.Attributes[1].Value.Trim()) + "' >";

                        // Storing the Input XML 
                        string premiumInput = nodInput.OuterXml;//  + nodInput.InnerXml + "</"+nodInput.Name+">";

                        //premiumInput		=	inputXML;
                        //premiumInput		=	premiumInput.Replace("<INPUTXML>","");
                        //premiumInput		=	premiumInput.Replace("</INPUTXML>","");

                        //Geting the Product Name and State values from the Input XML
                        XmlDocument xmlCheckProductName = new XmlDocument();
                        xmlCheckProductName.LoadXml(premiumInput);


                        //string premiumXslPath				=	ClsCommon.GetKeyValueWithIP("PremiumXSL_RENTAL_DWELLING"); //System.Configuration.ConfigurationSettings.AppSettings.Get("PremiumXSL").ToString();

                        string premiumXslPath = GetProductPremiumPath(premiumInput, "REDW");
                        string strProductFactorPath = "";
                        strProductFactorPath = GetProductFactorMasterPath(premiumInput, "REDW");

                        //Calling The Rating Engine
                        QEngineDll.QuoteEngine strGetPath = new QEngineDll.QuoteEngine(premiumInput, premiumXslPath, strProductFactorPath);
                        string shortestPath = strGetPath.GetPath();
                        premiumXML = strGetPath.CalculatePremium(shortestPath.Trim());

                        //added by praveen singh 
                        string str_Title = "";
                        string RATING_HEADER = "";
                        RATING_HEADER = ClsCommon.GetKeyValue("Rating_Header");
                        str_Title = "<HEADER TITLE='" + RATING_HEADER + "'> </HEADER></PRIMIUM>";
                        premiumXML = premiumXML.Replace("</PRIMIUM>", str_Title);

                        //to add the Business type 
                        NEW_BUSINESS = xmlTempElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/NEWBUSINESSFACTOR").InnerText;



                        if (NEW_BUSINESS.ToString().ToUpper().Trim() == "TRUE" || NEW_BUSINESS.ToString().ToUpper().Trim() == "Y")
                        {
                            BUSINESS_TYPE = "New";
                            strBusinessType = "New";//Added by Manoj 16.Sep.2009
                        }
                        else
                        {
                            BUSINESS_TYPE = "Ren";
                            strBusinessType = "Ren";//Added by Manoj 16.Sep.2009
                        }

                        //to add the Qeffective date rate and effective date 

                        QUOTE_DATE = " QUOTE_DATE='" + DateTime.Now.ToShortDateString().ToString() + "'";
                        QUOTE_EFFECTIVE_DATE = " QUOTE_EFFECTIVE_DATE='" + string.Format(QUOTE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";
                        RATE_EFFECTIVE_DATE = " RATE_EFFECTIVE_DATE='" + string.Format(RATE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";

                        BUSINESS_TYPE = " BUSINESS_TYPE='" + BUSINESS_TYPE + "'";

                        string final_string = "<HEADER " + QUOTE_DATE + QUOTE_EFFECTIVE_DATE + RATE_EFFECTIVE_DATE + BUSINESS_TYPE + " ";

                        premiumXML = premiumXML.Replace("<HEADER", final_string);

                        //Get the Primary App Nodes
                        XmlDocument xmlAppDoc = new XmlDocument();
                        //string strInputAppXML = "<INPUTXML>"+inputXML+"</INPUTXML>";
                        string strInputAppXML = inputXML;
                        string strFName = "";
                        string strLName = "";
                        string strAdd1 = "";
                        string strAdd2 = "";
                        string strCity = "";
                        string strState = "";
                        string strZipcode = "";
                        string strStateCode = "";
                        //AGency Info
                        string strAgencyAdd1 = "";
                        string strAgencyAdd2 = "";
                        string strAgencyCity = "";
                        string strAgencyState = "";
                        string strAgencyZip = "";
                        string strAgencyName = "";
                        string strAgencyPhone = "";
                        //QQ,APP,POL Info				
                        string strQQNumber = "";
                        string strAPPNumber = "";
                        string strAPPVersion = "";
                        string strPOLNumber = "";
                        string strPOLVersion = "";

                        /*string strAgencyName = "";
                        string strAgencyPhone="";//Agency Phone
                        string strStateCode ="";
                        string strQQNumber = "";string strAPPNumber = "";string strAPPVersion = "";
                        string strPOLNumber = "";string strPOLVersion = "";*/

                        strInputAppXML = strInputAppXML.Replace("&", "&amp;");
                        xmlAppDoc.LoadXml(strInputAppXML);
                        XmlElement xmlAppElement = xmlAppDoc.DocumentElement;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/FIRST_NAME") != null)
                            strFName = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/FIRST_NAME").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/LAST_NAME") != null)
                            strLName = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/LAST_NAME").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/ADDRESS1") != null)
                            strAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/ADDRESS1").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/ADDRESS2") != null)
                            strAdd2 = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/ADDRESS2").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/CITY") != null)
                            strCity = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/CITY").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/STATE") != null)
                            strState = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/STATE").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/ZIP_CODE") != null)
                            strZipcode = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/ZIP_CODE").InnerText;

                        //AGency Info

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_ADD1") != null)
                            strAgencyAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_ADD1").InnerText;

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_ADD1") != null)
                            strAgencyAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_ADD1").InnerText;

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_CITY") != null)
                            strAgencyCity = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_CITY").InnerText;


                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_STATE") != null)
                            strAgencyState = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_STATE").InnerText;

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_ZIP") != null)
                            strAgencyZip = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_ZIP").InnerText;


                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME") != null)
                            strAgencyName = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME").InnerText;

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_PHONE") != null)
                            strAgencyPhone = xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_PHONE").InnerText;

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE_CODE") != null)
                            strStateCode = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE_CODE").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/QQNUMBER") != null)
                            strQQNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/QQNUMBER").InnerText.ToString().Trim();

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APP_NUMBER") != null)
                            strAPPNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APP_NUMBER").InnerText.ToString().Trim();
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APP_VERSION") != null)
                            strAPPVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APP_VERSION").InnerText.ToString().Trim();
                        //POL
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/POL_NUMBER") != null)
                            strPOLNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/POL_NUMBER").InnerText.ToString().Trim();
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/POL_VERSION") != null)
                            strPOLVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/POL_VERSION").InnerText.ToString().Trim();




                        /*if(xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME")!=null)
                            strAgencyName  =	xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME").InnerText;

                            //Agency Phone	
                        if(xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_PHONE")!=null)
                            strAgencyPhone=    xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/AGENCY_PHONE").InnerText;

						
                        if(xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/STATE_CODE")!=null)
                            strStateCode  =	xmlAppElement.SelectSingleNode("QUICKQUOTE/PRIMARYAPPLICANT/STATE_CODE").InnerText;

                        //QQ
                        if(xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/QQNUMBER")!=null)
                            strQQNumber  =	xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/QQNUMBER").InnerText;

                        if(xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APP_NUMBER")!=null)
                            strAPPNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APP_NUMBER").InnerText.ToString().Trim();
                        if(xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APP_VERSION")!=null)
                            strAPPVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APP_VERSION").InnerText.ToString().Trim();
                        //POL
                        if(xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/POL_NUMBER")!=null)
                            strPOLNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/POL_NUMBER").InnerText.ToString().Trim();
                        if(xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/POL_VERSION")!=null)
                            strPOLVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/POL_VERSION").InnerText.ToString().Trim();*/




                        string strClientTop = "<CLIENT_TOP_INFO   PRIMARY_APP_NAME ='" + strFName + "'" +
                            " PRIMARY_APP_LNAME ='" + strLName + "'" +
                            " PRIMARY_APP_ADDRESS1 ='" + strAdd1 + "'" +
                            " PRIMARY_APP_ADDRESS2 ='" + strAdd2 + "'" +
                            " PRIMARY_APP_CITY ='" + strCity + "'" +
                            " PRIMARY_APP_STATE ='" + strState + "'" +
                            " PRIMARY_APP_ZIPCODE ='" + strZipcode + "'" +
                            " AGENCY_ADD1='" + strAgencyAdd1 + "'" +
                            " AGENCY_ADD2='" + strAgencyAdd2 + "'" +
                            " AGENCY_CITY='" + strAgencyCity + "'" +
                            " AGENCY_STATE='" + strAgencyState + "'" +
                            " AGENCY_ZIP='" + strAgencyZip + "'" +

                            " AGENCY_NAME='" + strAgencyName + "'" +
                            " AGENCY_PHONE='" + strAgencyPhone + "'" +
                            " QQ_NUMBER='" + strQQNumber + "'" +
                            " APP_NUMBER='" + strAPPNumber + "'" +
                            " APP_VERSION='" + strAPPVersion + "'" +
                            " POL_NUMBER='" + strPOLNumber + "'" +
                            " POL_VERSION='" + strPOLVersion + "'" +
                            " STATE_CODE ='" + strStateCode + "'>";


                        /*	" AGENCY_NAME='" + strAgencyName + "'" +
                            " AGENCY_PHONE='" + strAgencyPhone + "'" +
                            " QQ_NUMBER='" + strQQNumber + "'" +
                            " APP_NUMBER='" + strAPPNumber + "'" +
                            " APP_VERSION='" + strAPPVersion + "'" +
                            " POL_NUMBER='" + strPOLNumber + "'" +
                            " POL_VERSION='" + strPOLVersion + "'" +
                            " STATE_CODE ='" + strStateCode  + "'>";*/

                        premiumXML = premiumXML.Replace("</HEADER>", "</HEADER>" + strClientTop + "</CLIENT_TOP_INFO>");

                        //End Primary Applicants Nodes





                        //
                        XmlDocument xmlTempPremiumXML = new XmlDocument();
                        xmlTempPremiumXML.LoadXml(premiumXML);
                        //
                        XmlElement xmlTempPremiumElement = xmlTempPremiumXML.DocumentElement;
                        premiumComponent = premiumComponent + xmlTempPremiumElement.InnerXml + "</" + strRisk.Trim() + ">";
                        //
                        combinedPremiumComponent = combinedPremiumComponent + premiumComponent;
                    }

                    finalPremiumXML = finalPremiumXML + combinedPremiumComponent + "</PRIMIUM>";
                }
                //finalPremiumXML.Replace("&amp"," ");
                return finalPremiumXML;

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }
        /// <summary>
        /// Accepts  and app version id.
        /// Returns an xmlstring 
        /// </summary>		
        /// <param name="inputXML">inputXML that has to be checked </param>
        /// <returns>string</returns>
        public string GetAviationQuoteXMLForQuickQuote(string inputXML)
        {
            try
            {

                string finalPremiumXML = "";
                //string premiumXML = "";
                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                string driverComponent = "", vehicleComponent = "";
               // string premiumComponent = "";
                // Fetching the ProductFactor master Path as per the InputXML
                string strProductFactorPath = "";//, strQQInputXml = "";

                //Get the QuoteXML *************************		 
                if (inputXML != "<NewDataSet />" && inputXML != "")
                {

                    string strAdditionalText = "<?xml version='1.0'?> " +
                        "<?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?>" +
                        "<!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple'" +
                        " xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'>" +
                        " <!ATTLIST person id ID #IMPLIED>" +
                        "]>";


                    XmlDocument xmlTempDoc = new XmlDocument();
                    /* will not be required if deepak removes this from the input xml */
                    inputXML = inputXML.Replace("\"", "'");
                    inputXML = inputXML.Replace("<?xml version='1.0' encoding='utf-8'?>", "");
                    inputXML = inputXML.ToUpper();
                    inputXML = inputXML.Replace("&AMP;", "&amp;");
                    //Modified on 13 June 2008
                    //inputXML = inputXML.Replace("&","&amp;");
                    //inputXML = inputXML.Replace("'","hh673GSUYD7G3J73UDH");

                    /*********************/


                    string strInputXML = "<INPUTXML>" + inputXML + "</INPUTXML>";
                    xmlTempDoc.LoadXml(strInputXML);

                    // Append Xml in QQ for Violation and accident point
                    //XmlNode tmpnode = xmlTempDoc.SelectSingleNode("INPUTXML/QUICKQUOTE/POLICY/QQNUMBER");
                    //					if(tmpnode != null)
                    //					{
                    //						if(tmpnode.InnerText =="CAPITALRATER")
                    //						strInputXML=objGeneralInformation.SetAssignDriverAcciVioPointsCapitalRaterNode(strInputXML);
                    //					}
                    xmlTempDoc.LoadXml(strInputXML);
                    XmlElement xmlTempElement = xmlTempDoc.DocumentElement;
                    strStateName = xmlTempElement.SelectSingleNode("QUICKQUOTE/POLICY/STATENAME").InnerText;




                    /* Run the loop for each driver component and transform
                     * Store the results in a variable
                     */
                    //string driverInputXML = "";
                    //make inputs for drivers
                    XmlNode nodPolicy = xmlTempElement.FirstChild.FirstChild;
                    XmlNode nodVehicles = nodPolicy.NextSibling;
                    XmlNode nodDrivers = nodVehicles.NextSibling;
                    string premiumXslPath = "";





                    /* Run the loop for vehicles component for each vehicle and transform
                     * Store the results in a variable */
                    string vehicleInputXML = "", strAutoTypeUse = "";//vehicletype = "",
                    //premiumXslPath =	ClsCommon.GetKeyValueWithIP("PremiumVehicleComponentXSL");//System.Configuration.ConfigurationSettings.AppSettings.Get("PremiumVehicleComponentXSL").ToString();
                    //make inputs for drivers
                    nodPolicy = xmlTempElement.FirstChild.FirstChild;
                    nodVehicles = nodPolicy.NextSibling;
                    //nodDrivers = nodVehicles.NextSibling ;
                    XmlDocument xmlVehDoc = new XmlDocument();
                    foreach (XmlNode nodChild in nodVehicles.ChildNodes)
                    {
                        vehicleInputXML = "<" + nodPolicy.ParentNode.Name + ">"
                            + "<" + nodPolicy.Name + ">" + nodPolicy.InnerXml + "</" + nodPolicy.Name + ">"
                            + "<" + nodChild.ParentNode.Name + ">" + nodChild.OuterXml + "</" + nodChild.ParentNode.Name + ">"
                            //+ "<" + nodDrivers.Name +">" + nodDrivers.InnerXml  + "</" + nodDrivers.Name +">"
                            + "</" + nodPolicy.ParentNode.Name + ">";


                        xmlVehDoc.LoadXml(vehicleInputXML);
                        //vehicletype = xmlVehDoc.SelectSingleNode("QUICKQUOTE/VEHICLES/VEHICLE/VEHICLETYPEUSE").InnerText;
                        strAutoTypeUse = "AVIAT";
                        string strPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetApplicationPath();
                        strProductFactorPath = strPath + "/cmsweb/XSL/Quote/MasterData/Aviation/ProductFactorsMaster_AUTO_Personal_Michigan_07_2009.xml";
                        //strProductFactorPath				= GetProductFactorMasterPath(vehicleInputXML,strAutoTypeUse);		
                        vehicleInputXML = vehicleInputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>AIRCRAFT</TYPE>");
                        //premiumXslPath						=	GetProductPremiumPath(vehicleInputXML,strAutoTypeUse);
                        premiumXslPath = strPath + "/cmsweb/XSL/Quote/PremiumPath/Aviation/Premium.xsl";

                        vehicleInputXML = "<INPUTXML>" + vehicleInputXML + "</INPUTXML>";
                        vehicleInputXML = vehicleInputXML.Replace("\"", "'");
                        //string tempStr = GetPremiumComponent(vehicleInputXML,premiumXslPath); :SH
                        string tempStr = GetPremiumComponent(vehicleInputXML, premiumXslPath, strProductFactorPath);
                        tempStr = tempStr.Replace("<PRIMIUM>", "");
                        tempStr = tempStr.Replace("</PRIMIUM>", "");

                        /* Adding Risk ID */
                        string riskId = "", riskType = "";

                        if (nodChild.Attributes["ID"] != null)
                        {
                            riskId = nodChild.Attributes["ID"].Value.ToString();
                            if (nodChild.SelectSingleNode("VEHICLETYPE") != null)
                                riskType = nodChild.SelectSingleNode("VEHICLETYPE").InnerText.ToString().Trim();


                        }
                        else //For old records 
                        {
                            if (nodChild.Attributes["id"] != null)
                            {
                                riskId = nodChild.Attributes["id"].Value.ToString();
                                if (nodChild.SelectSingleNode("VEHICLETYPE") != null)
                                    riskType = nodChild.SelectSingleNode("VEHICLETYPE").InnerText.ToString().Trim();
                            }
                        }
                        vehicleComponent = vehicleComponent + "<RISK ID='" + riskId + "' TYPE='" + riskType + "'>" + tempStr.Replace(strAdditionalText, "") + "</RISK>";
                        //Modified on 13 June 2008
                        //vehicleComponent = vehicleComponent.Replace("&","&amp;");

                        //vehicleComponent = vehicleComponent + tempStr.Replace(strAdditionalText,"");
                        /* END Adding Risk ID */


                    }

                    /* Concatenate the two strings and transform with FinalQuote.
                     *	Replace & with &amp; before sending it back */
                    finalPremiumXML = strAdditionalText + "<PRIMIUM>" + driverComponent + vehicleComponent + "</PRIMIUM>";
                    //added by praveen singh 
                    string str_Title = "";
                    string RATING_HEADER = "";
                    RATING_HEADER = ClsCommon.GetKeyValue("Rating_Header");
                    str_Title = "<HEADER TITLE='" + RATING_HEADER + "'> </HEADER></PRIMIUM>";
                    finalPremiumXML = finalPremiumXML.Replace("</PRIMIUM>", str_Title);

                    //to add the Business type 
                    NEW_BUSINESS = xmlTempDoc.SelectSingleNode("INPUTXML/QUICKQUOTE/POLICY/NEWBUSINESS").InnerText;


                    if (NEW_BUSINESS.ToString().ToUpper().Trim() == "TRUE")
                    {
                        BUSINESS_TYPE = "New";
                        strBusinessType = "New"; //Added by Manoj 16.Sep.2009
                    }
                    else
                    {
                        BUSINESS_TYPE = "Ren";
                        strBusinessType = "Ren"; //Added by Manoj 16.Sep.2009
                    }

                    //to add the Qeffective date rate and effective date 
                    QUOTE_DATE = " QUOTE_DATE='" + DateTime.Now.ToShortDateString().ToString() + "'";
                    QUOTE_EFFECTIVE_DATE = " QUOTE_EFFECTIVE_DATE='" + string.Format(QUOTE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";
                    RATE_EFFECTIVE_DATE = " RATE_EFFECTIVE_DATE='" + string.Format(RATE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";
                    BUSINESS_TYPE = " BUSINESS_TYPE='" + BUSINESS_TYPE + "'";
                    string final_string = "<HEADER " + QUOTE_DATE + QUOTE_EFFECTIVE_DATE + RATE_EFFECTIVE_DATE + BUSINESS_TYPE + " ";

                    finalPremiumXML = finalPremiumXML.Replace("<HEADER", final_string);

                    //Get the Primary App Nodes
                    XmlDocument xmlAppDoc = new XmlDocument();
                    string strInputAppXML = "<INPUTXML>" + inputXML + "</INPUTXML>";
                    string strFName = "";
                    string strMName = "";
                    string strLName = "";
                    string strAdd1 = "";
                    string strAdd2 = "";
                    string strCity = "";
                    string strState = "";
                    string strZipcode = "";
                    string strStateCode = "";
                    //AGency Info
                    string strAgencyAdd1 = "";
                    string strAgencyAdd2 = "";
                    string strAgencyCity = "";
                    string strAgencyState = "";
                    string strAgencyZip = "";
                    string strAgencyName = "";
                    string strAgencyPhone = "";
                    //QQ,APP,POL Info				
                    string strQQNumber = "";
                    string strAPPNumber = "";
                    string strAPPVersion = "";
                    string strPOLNumber = "";
                    string strPOLVersion = "";

                    /*string strAgencyName = "";
                    string strAgencyPhone="";//Agency Phone
                    string strStateCode ="";
                    string strQQNumber = "";
                    string strAPPNumber = "";
                    string strAPPVersion = "";
                    string strPOLNumber = "";
                    string strPOLVersion = "";*/

                    strInputAppXML = strInputAppXML.Replace("&", "&amp;");
                    xmlAppDoc.LoadXml(strInputAppXML);
                    XmlElement xmlAppElement = xmlAppDoc.DocumentElement;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/FIRST_NAME") != null)
                        strFName = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/FIRST_NAME").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/MIDDLE_NAME") != null)
                        strMName = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/MIDDLE_NAME").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/LAST_NAME") != null)
                        strLName = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/LAST_NAME").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS1") != null)
                        strAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS1").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS2") != null)
                        strAdd2 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS2").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/CITY") != null)
                        strCity = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/CITY").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE") != null)
                        strState = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ZIP_CODE") != null)
                        strZipcode = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ZIP_CODE").InnerText;


                    //AGency Info

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1") != null)
                        strAgencyAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1") != null)
                        strAgencyAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_CITY") != null)
                        strAgencyCity = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_CITY").InnerText;


                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_STATE") != null)
                        strAgencyState = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_STATE").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ZIP") != null)
                        strAgencyZip = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ZIP").InnerText;


                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME") != null)
                        strAgencyName = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_PHONE") != null)
                        strAgencyPhone = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_PHONE").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE_CODE") != null)
                        strStateCode = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE_CODE").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/QQNUMBER") != null)
                        strQQNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/QQNUMBER").InnerText.ToString().Trim();
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_NUMBER") != null)
                        strAPPNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_NUMBER").InnerText.ToString().Trim();
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_VERSION") != null)
                        strAPPVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_VERSION").InnerText.ToString().Trim();
                    //POL
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_NUMBER") != null)
                        strPOLNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_NUMBER").InnerText.ToString().Trim();
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_VERSION") != null)
                        strPOLVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_VERSION").InnerText.ToString().Trim();


                    string strClientTop = "<CLIENT_TOP_INFO PRIMARY_APP_NAME ='" + strFName + ' ' + strMName + ' ' + strLName + "'" +
                        " PRIMARY_APP_ADDRESS1 ='" + strAdd1 + "'" +
                        " PRIMARY_APP_ADDRESS2 ='" + strAdd2 + "'" +
                        " PRIMARY_APP_CITY ='" + strCity + "'" +
                        " PRIMARY_APP_STATE ='" + strState + "'" +
                        " PRIMARY_APP_ZIPCODE ='" + strZipcode + "'" +

                        " AGENCY_ADD1='" + strAgencyAdd1 + "'" +
                        " AGENCY_ADD2='" + strAgencyAdd2 + "'" +
                        " AGENCY_CITY='" + strAgencyCity + "'" +
                        " AGENCY_STATE='" + strAgencyState + "'" +
                        " AGENCY_ZIP='" + strAgencyZip + "'" +

                        " AGENCY_NAME='" + strAgencyName + "'" +
                        " AGENCY_PHONE='" + strAgencyPhone + "'" +
                        " QQ_NUMBER='" + strQQNumber + "'" +
                        " APP_NUMBER='" + strAPPNumber + "'" +
                        " APP_VERSION='" + strAPPVersion + "'" +
                        " POL_NUMBER='" + strPOLNumber + "'" +
                        " POL_VERSION='" + strPOLVersion + "'" +
                        " STATE_CODE ='" + strStateCode + "'>";


                    /*" AGENCY_NAME='" + strAgencyName + "'" +
                        " QQ_NUMBER='" + strQQNumber + "'" +
                        " APP_NUMBER='" + strAPPNumber + "'" +
                        " APP_VERSION='" + strAPPVersion + "'" +
                        " POL_NUMBER='" + strPOLNumber + "'" +
                        " POL_VERSION='" + strPOLVersion + "'" +
                        " STATE_CODE ='" + strStateCode  + "'>";*/

                    //					XmlDocument xmlTempPremiumXML		=	new XmlDocument();
                    //					xmlTempPremiumXML.LoadXml(finalPremiumXML);
                    //					//
                    //					
                    //					XmlElement xmlTempPremiumElement	=	xmlTempPremiumXML.DocumentElement;
                    //					premiumComponent					=	xmlTempPremiumElement.InnerXml; 

                    finalPremiumXML = finalPremiumXML.Replace("</HEADER>", "</HEADER>" + strClientTop + "</CLIENT_TOP_INFO>");

                    //End Primary Applicants Nodes				
                }

                finalPremiumXML = finalPremiumXML.Replace("&", "&amp;");
                //				XmlDocument xmlTempPremiumXML		=	new XmlDocument();
                //				xmlTempPremiumXML.LoadXml(finalPremiumXML);
                //				//
                //				string premiumComponent	= "";
                //				XmlElement xmlTempPremiumElement	=	xmlTempPremiumXML.DocumentElement;
                //				premiumComponent					=	xmlTempPremiumElement.InnerXml; 
                //					
                return finalPremiumXML;

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }
        /// <summary>
        /// Accepts  and app version id.
        /// Returns an xmlstring 
        /// </summary>		
        /// <param name="inputXML">inputXML that has to be checked </param>
        /// <returns>string</returns>
        public string GetAutoQuoteXMLForQuickQuote(string inputXML)
        {
            try
            {

                string finalPremiumXML = "";
                //string premiumXML = "";
                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                string driverComponent = "", vehicleComponent = "";
                //string premiumComponent = "";
                // Fetching the ProductFactor master Path as per the InputXML
                string strProductFactorPath = "";//, strQQInputXml = "";

                //Get the QuoteXML *************************		 
                if (inputXML != "<NewDataSet />" && inputXML != "")
                {

                    string strAdditionalText = "<?xml version='1.0'?> " +
                        "<?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?>" +
                        "<!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple'" +
                        " xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'>" +
                        " <!ATTLIST person id ID #IMPLIED>" +
                        "]>";


                    XmlDocument xmlTempDoc = new XmlDocument();
                    /* will not be required if deepak removes this from the input xml */
                    inputXML = inputXML.Replace("\"", "'");
                    inputXML = inputXML.Replace("<?xml version='1.0' encoding='utf-8'?>", "");
                    inputXML = inputXML.ToUpper();
                    inputXML = inputXML.Replace("&AMP;", "&amp;");
                    //Modified on 13 June 2008
                    //inputXML = inputXML.Replace("&","&amp;");
                    //inputXML = inputXML.Replace("'","hh673GSUYD7G3J73UDH");

                    /*********************/


                    string strInputXML = "<INPUTXML>" + inputXML + "</INPUTXML>";
                    xmlTempDoc.LoadXml(strInputXML);

                    // Append Xml in QQ for Violation and accident point
                    //XmlNode tmpnode = xmlTempDoc.SelectSingleNode("INPUTXML/QUICKQUOTE/POLICY/QQNUMBER");
                    //					if(tmpnode != null)
                    //					{
                    //						if(tmpnode.InnerText =="CAPITALRATER")
                    //						strInputXML=objGeneralInformation.SetAssignDriverAcciVioPointsCapitalRaterNode(strInputXML);
                    //					}
                    xmlTempDoc.LoadXml(strInputXML);
                    XmlElement xmlTempElement = xmlTempDoc.DocumentElement;
                    strStateName = xmlTempElement.SelectSingleNode("QUICKQUOTE/POLICY/STATENAME").InnerText;

                    string strVehicleType = xmlTempDoc.SelectSingleNode("INPUTXML/QUICKQUOTE/VEHICLES/VEHICLE/VEHICLETYPEUSE").InnerText;

                    string strAutoType = "";
                    if (strVehicleType.ToUpper().ToString() == "COMMERCIAL")
                        strAutoType = "AUTOC";
                    else
                        strAutoType = "AUTOP";



                    /* Run the loop for each driver component and transform
                     * Store the results in a variable
                     */
                    string driverInputXML = "";
                    //make inputs for drivers
                    XmlNode nodPolicy = xmlTempElement.FirstChild.FirstChild;
                    XmlNode nodVehicles = nodPolicy.NextSibling;
                    XmlNode nodDrivers = nodVehicles.NextSibling;
                    string premiumXslPath = "";

                    foreach (XmlNode nodChild in nodDrivers.ChildNodes)
                    {
                        //premiumXslPath				=	ClsCommon.GetKeyValueWithIP("PremiumDriverComponentXSL");	 //System.Configuration.ConfigurationSettings.AppSettings.Get("PremiumDriverComponentXSL").ToString();
                        driverInputXML = "<" + nodPolicy.ParentNode.Name + ">"
                            + "<" + nodPolicy.Name + ">" + nodPolicy.InnerXml + "</" + nodPolicy.Name + ">"
                            + "<" + nodVehicles.Name + ">" + nodVehicles.InnerXml + "</" + nodVehicles.Name + ">"
                            + "<" + nodChild.ParentNode.Name + ">" + nodChild.OuterXml + "</" + nodChild.ParentNode.Name + ">"
                            + "</" + nodPolicy.ParentNode.Name + ">";


                        /*
                        strProductFactorPath		= GetProductFactorMasterPath(driverInputXML,"AUTO");
                        driverInputXML						=	driverInputXML.Replace("<QUICKQUOTE>","<QUICKQUOTE><TYPE>DRIVER</TYPE>");
                        premiumXslPath						=	GetProductPremiumPath(driverInputXML,"AUTO");
                        */
                        //Modified on 13 June 2008
                        driverInputXML = driverInputXML.Replace("&", "&amp;");
                        //added by praveen singh
                        strProductFactorPath = GetProductFactorMasterPath(driverInputXML, strAutoType);
                        driverInputXML = driverInputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>DRIVER</TYPE>");
                        premiumXslPath = GetProductPremiumPath(driverInputXML, strAutoType);



                        driverInputXML = "<INPUTXML>" + driverInputXML + "</INPUTXML>";
                        string tempStr = GetPremiumComponent(driverInputXML, premiumXslPath, strProductFactorPath);

                        tempStr = tempStr.Replace("<PRIMIUM>", "");
                        tempStr = tempStr.Replace("</PRIMIUM>", "");
                        /*Added Driver ID*/
                        if (nodChild.Attributes["ID"] != null)
                            driverComponent = driverComponent + "<DRIVER ID='" + nodChild.Attributes["ID"].Value.ToString() + "'>" + tempStr.Replace(strAdditionalText, strAutoType) + "</DRIVER>";
                        else
                        {
                            if (nodChild.Attributes["id"] != null)
                                driverComponent = driverComponent + "<DRIVER ID='" + nodChild.Attributes["id"].Value.ToString() + "'>" + tempStr.Replace(strAdditionalText, strAutoType) + "</DRIVER>";
                        }
                        //driverComponent = driverComponent + tempStr.Replace(strAdditionalText,strAutoType);
                        /* END Added Operator ID*/



                        //For each driver get the Violation/Accident for display (if any)
                        XmlNode nodViolations = nodChild.SelectSingleNode("VIOLATIONS");
                        if (nodViolations != null && nodViolations.HasChildNodes == true)
                        {
                            XmlNodeList nodLstViolation = nodViolations.SelectNodes("VIOLATION");
                            if (nodLstViolation.Count > 0)
                            {
                                XmlNode nodTempDate = nodPolicy.SelectSingleNode("QUOTEEFFDATE");
                                string strQuoteEffectiveDate = "<QUOTEEFFDATE></QUOTEEFFDATE>";
                                if (nodTempDate != null)
                                {
                                    strQuoteEffectiveDate = nodTempDate.OuterXml.ToString().Trim();
                                }
                                //premiumXslPath				=	ClsCommon.GetKeyValueWithIP("PremiumViolationComponentXSL"); //System.Configuration.ConfigurationSettings.AppSettings.Get("PremiumViolationComponentXSL").ToString();

                                //AUTO
                                nodTempDate = nodPolicy.SelectSingleNode("STATENAME");
                                string strTempStateName = "<STATENAME></STATENAME>";

                                if (nodTempDate != null)
                                {
                                    strTempStateName = nodTempDate.OuterXml.ToString().Trim();
                                }



                                foreach (XmlNode nodTemp in nodLstViolation)
                                {
                                    string violationInputXML = "<QUICKQUOTE>" + strQuoteEffectiveDate + strTempStateName + nodTemp.OuterXml + "</QUICKQUOTE>";

                                    violationInputXML = violationInputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>VIOLATION</TYPE>");

                                    strProductFactorPath = GetProductFactorMasterPath(violationInputXML, strAutoType);
                                    premiumXslPath = GetProductPremiumPath(violationInputXML, strAutoType);

                                    violationInputXML = "<INPUTXML>" + violationInputXML + "</INPUTXML>";
                                    //added by praveen singh
                                    //strProductFactorPath = GetProductFactorMasterPath(violationInputXML,strAutoType);
                                    //driverInputXML		 =	driverInputXML.Replace("<QUICKQUOTE>","<QUICKQUOTE><TYPE>Violation</TYPE>");
                                    //premiumXslPath		 =	GetProductPremiumPath(driverInputXML,strAutoType);					


                                    tempStr = GetPremiumComponent(violationInputXML, premiumXslPath, strProductFactorPath);

                                    tempStr = tempStr.Replace("<PRIMIUM>", "");
                                    tempStr = tempStr.Replace("</PRIMIUM>", "");

                                    driverComponent = driverComponent + tempStr.Replace(strAdditionalText, strAutoType);

                                }
                            }
                        }


                    }



                    /* Run the loop for vehicles component for each vehicle and transform
                     * Store the results in a variable */
                    string vehicleInputXML = "", vehicletype = "", strAutoTypeUse = "";
                    //premiumXslPath =	ClsCommon.GetKeyValueWithIP("PremiumVehicleComponentXSL");//System.Configuration.ConfigurationSettings.AppSettings.Get("PremiumVehicleComponentXSL").ToString();
                    //make inputs for drivers
                    nodPolicy = xmlTempElement.FirstChild.FirstChild;
                    nodVehicles = nodPolicy.NextSibling;
                    nodDrivers = nodVehicles.NextSibling;
                    XmlDocument xmlVehDoc = new XmlDocument();
                    foreach (XmlNode nodChild in nodVehicles.ChildNodes)
                    {
                        vehicleInputXML = "<" + nodPolicy.ParentNode.Name + ">"
                            + "<" + nodPolicy.Name + ">" + nodPolicy.InnerXml + "</" + nodPolicy.Name + ">"
                            + "<" + nodChild.ParentNode.Name + ">" + nodChild.OuterXml + "</" + nodChild.ParentNode.Name + ">"
                            + "<" + nodDrivers.Name + ">" + nodDrivers.InnerXml + "</" + nodDrivers.Name + ">"
                            + "</" + nodPolicy.ParentNode.Name + ">";


                        xmlVehDoc.LoadXml(vehicleInputXML);
                        vehicletype = xmlVehDoc.SelectSingleNode("QUICKQUOTE/VEHICLES/VEHICLE/VEHICLETYPEUSE").InnerText;

                        if (vehicletype.ToUpper().ToString() == "COMMERCIAL")
                            strAutoTypeUse = "AUTOC";
                        else
                            strAutoTypeUse = "AUTOP";

                        strProductFactorPath = GetProductFactorMasterPath(vehicleInputXML, strAutoTypeUse);
                        vehicleInputXML = vehicleInputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>VEHICLE</TYPE>");
                        premiumXslPath = GetProductPremiumPath(vehicleInputXML, strAutoTypeUse);

                        vehicleInputXML = "<INPUTXML>" + vehicleInputXML + "</INPUTXML>";
                        vehicleInputXML = vehicleInputXML.Replace("\"", "'");
                        //string tempStr = GetPremiumComponent(vehicleInputXML,premiumXslPath); :SH
                        string tempStr = GetPremiumComponent(vehicleInputXML, premiumXslPath, strProductFactorPath);
                        tempStr = tempStr.Replace("<PRIMIUM>", "");
                        tempStr = tempStr.Replace("</PRIMIUM>", "");

                        /* Adding Risk ID */
                        string riskId = "", riskType = "";

                        if (nodChild.Attributes["ID"] != null)
                        {
                            riskId = nodChild.Attributes["ID"].Value.ToString();
                            if (nodChild.SelectSingleNode("VEHICLETYPE") != null)
                                riskType = nodChild.SelectSingleNode("VEHICLETYPE").InnerText.ToString().Trim();


                        }
                        else //For old records 
                        {
                            if (nodChild.Attributes["id"] != null)
                            {
                                riskId = nodChild.Attributes["id"].Value.ToString();
                                if (nodChild.SelectSingleNode("VEHICLETYPE") != null)
                                    riskType = nodChild.SelectSingleNode("VEHICLETYPE").InnerText.ToString().Trim();
                            }
                        }
                        vehicleComponent = vehicleComponent + "<RISK ID='" + riskId + "' TYPE='" + riskType + "'>" + tempStr.Replace(strAdditionalText, "") + "</RISK>";
                        //Modified on 13 June 2008
                        //vehicleComponent = vehicleComponent.Replace("&","&amp;");

                        //vehicleComponent = vehicleComponent + tempStr.Replace(strAdditionalText,"");
                        /* END Adding Risk ID */


                    }

                    /* Concatenate the two strings and transform with FinalQuote.
                     *	Replace & with &amp; before sending it back */
                    finalPremiumXML = strAdditionalText + "<PRIMIUM>" + driverComponent + vehicleComponent + "</PRIMIUM>";
                    //added by praveen singh 
                    string str_Title = "";
                    string RATING_HEADER = "";
                    RATING_HEADER = ClsCommon.GetKeyValue("Rating_Header");
                    str_Title = "<HEADER TITLE='" + RATING_HEADER + "'> </HEADER></PRIMIUM>";
                    finalPremiumXML = finalPremiumXML.Replace("</PRIMIUM>", str_Title);

                    //to add the Business type 
                    NEW_BUSINESS = xmlTempDoc.SelectSingleNode("INPUTXML/QUICKQUOTE/POLICY/NEWBUSINESS").InnerText;


                    if (NEW_BUSINESS.ToString().ToUpper().Trim() == "TRUE")
                    {
                        BUSINESS_TYPE = "New";
                        strBusinessType = "New"; //Added by Manoj 16.Sep.2009
                    }
                    else
                    {
                        BUSINESS_TYPE = "Ren";
                        strBusinessType = "Ren"; //Added by Manoj 16.Sep.2009
                    }

                    //to add the Qeffective date rate and effective date 
                    QUOTE_DATE = " QUOTE_DATE='" + DateTime.Now.ToShortDateString().ToString() + "'";
                    QUOTE_EFFECTIVE_DATE = " QUOTE_EFFECTIVE_DATE='" + string.Format(QUOTE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";
                    RATE_EFFECTIVE_DATE = " RATE_EFFECTIVE_DATE='" + string.Format(RATE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";
                    BUSINESS_TYPE = " BUSINESS_TYPE='" + BUSINESS_TYPE + "'";
                    string final_string = "<HEADER " + QUOTE_DATE + QUOTE_EFFECTIVE_DATE + RATE_EFFECTIVE_DATE + BUSINESS_TYPE + " ";

                    finalPremiumXML = finalPremiumXML.Replace("<HEADER", final_string);

                    //Get the Primary App Nodes
                    XmlDocument xmlAppDoc = new XmlDocument();
                    string strInputAppXML = "<INPUTXML>" + inputXML + "</INPUTXML>";
                    string strFName = "";
                    string strMName = "";
                    string strLName = "";
                    string strAdd1 = "";
                    string strAdd2 = "";
                    string strCity = "";
                    string strState = "";
                    string strZipcode = "";
                    string strStateCode = "";
                    //AGency Info
                    string strAgencyAdd1 = "";
                    string strAgencyAdd2 = "";
                    string strAgencyCity = "";
                    string strAgencyState = "";
                    string strAgencyZip = "";
                    string strAgencyName = "";
                    string strAgencyPhone = "";
                    //QQ,APP,POL Info				
                    string strQQNumber = "";
                    string strAPPNumber = "";
                    string strAPPVersion = "";
                    string strPOLNumber = "";
                    string strPOLVersion = "";

                    /*string strAgencyName = "";
                    string strAgencyPhone="";//Agency Phone
                    string strStateCode ="";
                    string strQQNumber = "";
                    string strAPPNumber = "";
                    string strAPPVersion = "";
                    string strPOLNumber = "";
                    string strPOLVersion = "";*/

                    strInputAppXML = strInputAppXML.Replace("&", "&amp;");
                    xmlAppDoc.LoadXml(strInputAppXML);
                    XmlElement xmlAppElement = xmlAppDoc.DocumentElement;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/FIRST_NAME") != null)
                        strFName = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/FIRST_NAME").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/MIDDLE_NAME") != null)
                        strMName = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/MIDDLE_NAME").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/LAST_NAME") != null)
                        strLName = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/LAST_NAME").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS1") != null)
                        strAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS1").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS2") != null)
                        strAdd2 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS2").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/CITY") != null)
                        strCity = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/CITY").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE") != null)
                        strState = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ZIP_CODE") != null)
                        strZipcode = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ZIP_CODE").InnerText;


                    //AGency Info

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1") != null)
                        strAgencyAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1") != null)
                        strAgencyAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_CITY") != null)
                        strAgencyCity = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_CITY").InnerText;


                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_STATE") != null)
                        strAgencyState = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_STATE").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ZIP") != null)
                        strAgencyZip = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ZIP").InnerText;


                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME") != null)
                        strAgencyName = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_PHONE") != null)
                        strAgencyPhone = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_PHONE").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE_CODE") != null)
                        strStateCode = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE_CODE").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/QQNUMBER") != null)
                        strQQNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/QQNUMBER").InnerText.ToString().Trim();
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_NUMBER") != null)
                        strAPPNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_NUMBER").InnerText.ToString().Trim();
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_VERSION") != null)
                        strAPPVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_VERSION").InnerText.ToString().Trim();
                    //POL
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_NUMBER") != null)
                        strPOLNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_NUMBER").InnerText.ToString().Trim();
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_VERSION") != null)
                        strPOLVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_VERSION").InnerText.ToString().Trim();


                    string strClientTop = "<CLIENT_TOP_INFO PRIMARY_APP_NAME ='" + strFName + ' ' + strMName + ' ' + strLName + "'" +
                        " PRIMARY_APP_ADDRESS1 ='" + strAdd1 + "'" +
                        " PRIMARY_APP_ADDRESS2 ='" + strAdd2 + "'" +
                        " PRIMARY_APP_CITY ='" + strCity + "'" +
                        " PRIMARY_APP_STATE ='" + strState + "'" +
                        " PRIMARY_APP_ZIPCODE ='" + strZipcode + "'" +

                        " AGENCY_ADD1='" + strAgencyAdd1 + "'" +
                        " AGENCY_ADD2='" + strAgencyAdd2 + "'" +
                        " AGENCY_CITY='" + strAgencyCity + "'" +
                        " AGENCY_STATE='" + strAgencyState + "'" +
                        " AGENCY_ZIP='" + strAgencyZip + "'" +

                        " AGENCY_NAME='" + strAgencyName + "'" +
                        " AGENCY_PHONE='" + strAgencyPhone + "'" +
                        " QQ_NUMBER='" + strQQNumber + "'" +
                        " APP_NUMBER='" + strAPPNumber + "'" +
                        " APP_VERSION='" + strAPPVersion + "'" +
                        " POL_NUMBER='" + strPOLNumber + "'" +
                        " POL_VERSION='" + strPOLVersion + "'" +
                        " STATE_CODE ='" + strStateCode + "'>";


                    /*" AGENCY_NAME='" + strAgencyName + "'" +
                        " QQ_NUMBER='" + strQQNumber + "'" +
                        " APP_NUMBER='" + strAPPNumber + "'" +
                        " APP_VERSION='" + strAPPVersion + "'" +
                        " POL_NUMBER='" + strPOLNumber + "'" +
                        " POL_VERSION='" + strPOLVersion + "'" +
                        " STATE_CODE ='" + strStateCode  + "'>";*/

                    //					XmlDocument xmlTempPremiumXML		=	new XmlDocument();
                    //					xmlTempPremiumXML.LoadXml(finalPremiumXML);
                    //					//
                    //					
                    //					XmlElement xmlTempPremiumElement	=	xmlTempPremiumXML.DocumentElement;
                    //					premiumComponent					=	xmlTempPremiumElement.InnerXml; 

                    finalPremiumXML = finalPremiumXML.Replace("</HEADER>", "</HEADER>" + strClientTop + "</CLIENT_TOP_INFO>");

                    //End Primary Applicants Nodes				
                }

                finalPremiumXML = finalPremiumXML.Replace("&", "&amp;");
                //				XmlDocument xmlTempPremiumXML		=	new XmlDocument();
                //				xmlTempPremiumXML.LoadXml(finalPremiumXML);
                //				//
                //				string premiumComponent	= "";
                //				XmlElement xmlTempPremiumElement	=	xmlTempPremiumXML.DocumentElement;
                //				premiumComponent					=	xmlTempPremiumElement.InnerXml; 
                //					
                return finalPremiumXML;

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }

        public string GetWatercraftQuoteXMLForQuickQuote(string inputXML)
        {

            //To be Open While Testing //
            /*inputXML= "	<QUICKQUOTE><POLICY><NEWBUSINESS>Y</NEWBUSINESS><RENEWAL>N</RENEWAL><STATENAME>MICHIGAN</STATENAME><QUOTEEFFDATE>06/15/2006</QUOTEEFFDATE><QUOTEEXPDATE>06/15/2007</QUOTEEXPDATE><TERRITORYCODE>1</TERRITORYCODE><POLICYTERMS>12</POLICYTERMS><ATTACHTOWOLVERINE>N</ATTACHTOWOLVERINE><HOMEAPPNUMBERDISPLAY></HOMEAPPNUMBERDISPLAY><HOMEAPPNUMBER></HOMEAPPNUMBER><HOMECUSTID></HOMECUSTID><HOMEAPPID></HOMEAPPID><HOMEPPVERID></HOMEPPVERID><BOATHOMEDISC>N</BOATHOMEDISC><INSURANCESCORE>726</INSURANCESCORE><PERSONALLIABILITY>100000</PERSONALLIABILITY><MEDICALPAYMENT>1000</MEDICALPAYMENT><MEDICALPAYMENTSOTHER>1000/25000</MEDICALPAYMENTSOTHER><MEDICALPAYMENTSOTHERLIMIT>25000</MEDICALPAYMENTSOTHERLIMIT><UNINSUREDBOATERS>50000</UNINSUREDBOATERS><UNATTACHEDEQUIPMENT>1500</UNATTACHEDEQUIPMENT><UNATTACHEDEQUIPMENT_DEDUCTIBLE>100</UNATTACHEDEQUIPMENT_DEDUCTIBLE><SCHEDULEDMISCSPORTS></SCHEDULEDMISCSPORTS></POLICY><BOATS><BOAT id=\'1\'><BOATROWID>1</BOATROWID><BOATTYPE>Inboard</BOATTYPE><BOATTYPECODE>I</BOATTYPECODE><BOATSTYLECODE>I</BOATSTYLECODE><YEAR>2006</YEAR><AGE>0</AGE><MANUFACTURER>Boat</MANUFACTURER><MODEL></MODEL><SERIALNUMBER></SERIALNUMBER><LENGTH>10</LENGTH><HORSEPOWER>15</HORSEPOWER><WEIGHT>10000</WEIGHT><CAPABLESPEED>15</CAPABLESPEED><WATERS>Inland Waters</WATERS>" 
            + " <WATERSCODE>I</WATERSCODE><MARKETVALUE>5000</MARKETVALUE><DEDUCTIBLE>250</DEDUCTIBLE><CONSTRUCTION>Wood</CONSTRUCTION><CONSTRUCTIONCODE>W</CONSTRUCTIONCODE><OP720>N</OP720><OP900>N</OP900><AV100>N</AV100><OP900_LIMIT>50000</OP900_LIMIT><COUNTYOFOPERATION></COUNTYOFOPERATION><COUNTYCODE></COUNTYCODE><TERRITORYDOCKEDIN></TERRITORYDOCKEDIN><STATEDOCKEDIN>MICHIGAN</STATEDOCKEDIN><DIESELENGINE>N</DIESELENGINE><SHORESTATION>N</SHORESTATION><HALONFIRE>N</HALONFIRE><LORANNAVIGATIONSYSTEM>N</LORANNAVIGATIONSYSTEM><DUALOWNERSHIP>N</DUALOWNERSHIP><REMOVESAILBOAT>N</REMOVESAILBOAT><MULTIBOATCREDIT>N</MULTIBOATCREDIT><PERSONALLIABILITY>100000</PERSONALLIABILITY><MEDICALPAYMENT>1000</MEDICALPAYMENT><MEDICALPAYMENTSOTHER>1000/25000</MEDICALPAYMENTSOTHER><MEDICALPAYMENTSOTHERLIMIT>25000</MEDICALPAYMENTSOTHERLIMIT><UNINSUREDBOATERS>50000</UNINSUREDBOATERS><UNATTACHEDEQUIPMENT>1500</UNATTACHEDEQUIPMENT><UNATTACHEDEQUIPMENT_DEDUCTIBLE>100</UNATTACHEDEQUIPMENT_DEDUCTIBLE><MVR>0</MVR><SUMOFVIOLATIONPOINTS>0</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>-1</SUMOFACCIDENTPOINTS></BOAT></BOATS><OPERATORS><OPERATOR id=\'1\'><OPERATORFNAME>d</OPERATORFNAME><OPERATORMNAME></OPERATORMNAME><OPERATORLNAME>jkjk</OPERATORLNAME><BIRTHDATE>05/04/2006</BIRTHDATE><GENDER>Male</GENDER>"
            + "<MARITALSTATUS>Single</MARITALSTATUS><YEARSLICENSED>5</YEARSLICENSED><POWERSQUADRONCOURSE>N</POWERSQUADRONCOURSE><COASTGUARDAUXILARYCOURSE>N</COASTGUARDAUXILARYCOURSE><BOATINGEXPERIENCESINCE></BOATINGEXPERIENCESINCE><HAS_5_YEARSOPERATOREXPERIENCE></HAS_5_YEARSOPERATOREXPERIENCE><BOATASSIGNEDASOPERATOR>1</BOATASSIGNEDASOPERATOR><BOATDRIVEDAS>Principal</BOATDRIVEDAS><MVR>0</MVR><SUMOFVIOLATIONPOINTS>0</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>-1</SUMOFACCIDENTPOINTS><AGEOFDRIVER>0</AGEOFDRIVER><DRIVERCLASS /><DRIVERCLASSCOMPONENT1>P</DRIVERCLASSCOMPONENT1><DRIVERCLASSCOMPONENT2 /><VIOLATIONS /></OPERATOR></OPERATORS></QUICKQUOTE> "; */




            #region Declearation
            string finalPremiumXML = "";
            string driverComponent = "";
            string boatComponent = "";
            string strAdditionalText = "";
            string scheduledSportsInputXML = "";
            string scheduledSportsComponent = "";
            string strProductFactorPath = "";
            string strPremiumXSLPath = "";
            string premiumXSLPath = "";
            string strStateName = "";
            string scheduledUnattachedComponent = "";
            string MinimumPremiumComponent = "";
            int SchRiskId;
            #endregion

            #region GET Watercraft Rating Quote
            try
            {


                //Get the QuoteXML *************************		
                inputXML = inputXML.Replace("$", "");
                if (inputXML != "<NewDataSet />" && inputXML != "")
                {


                    strAdditionalText = "<?xml version='1.0'?> " +
                        "<?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?>" +
                        "<!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple'" +
                        " xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'>" +
                        " <!ATTLIST person id ID #IMPLIED>" +
                        "]>";

                    XmlDocument xmlTempDoc = new XmlDocument();
                    string strInputXML = "<INPUTXML>" + inputXML + "</INPUTXML>";
                    strInputXML = strInputXML.Replace("&AMP;", "");
                    strInputXML = strInputXML.Replace("&", "&amp;");
                    //strInputXML = strInputXML.Replace("'","h673GSUYD7G3J73UDH");
                    xmlTempDoc.LoadXml(strInputXML);
                    XmlElement xmlTempElement = xmlTempDoc.DocumentElement;

                    strStateName = xmlTempElement.SelectSingleNode("QUICKQUOTE/POLICY/STATENAME").InnerText;


                    // Fetching the ProductFactor master Path as per the InputXML
                    inputXML = inputXML.Replace("&", "&amp;"); //Added on 18 April 2008 :Praveen
                    strProductFactorPath = GetProductFactorMasterPath(inputXML, "BOAT");

                    #region OPERATOR AND VIOLATION
                    /* Run the loop for each violations and transform
					 * Store the results in a variable
					 */


                    string driverInputXML = "";

                    //make inputs for drivers
                    strStateName = xmlTempElement.SelectSingleNode("QUICKQUOTE/POLICY/STATENAME").InnerText;
                    NEW_BUSINESS = xmlTempElement.SelectSingleNode("QUICKQUOTE/POLICY/NEWBUSINESS").InnerText;


                    XmlNode nodPolicy = xmlTempElement.FirstChild.FirstChild;
                    XmlNode nodVehicles = nodPolicy.NextSibling;
                    XmlNode nodDrivers = nodVehicles.NextSibling;
                    //string premiumXslPath = "";
                    foreach (XmlNode nodChild in nodDrivers.ChildNodes)
                    {
                        //premiumXslPath				=	System.Configuration.ConfigurationSettings.AppSettings.Get("PremiumDriverComponentXSL").ToString();
                        //premiumXslPath					=	"D:/Projects/EBX-DV25/source code/cms/cmsweb/XSL/Quote/Watercraft/Premium_OperatorComponent.xsl";
                        //SKB
                        //premiumXslPath					=	ClsCommon.GetKeyValueWithIP("PremiumXSLWatercraft_OperatorComponent"); 

                        driverInputXML = "<" + nodPolicy.ParentNode.Name + ">"
                            + "<" + nodPolicy.Name + ">" + nodPolicy.InnerXml + "</" + nodPolicy.Name + ">"
                            + "<" + nodVehicles.Name + ">" + nodVehicles.InnerXml + "</" + nodVehicles.Name + ">"
                            + "<" + nodChild.ParentNode.Name + ">" + nodChild.OuterXml + "</" + nodChild.ParentNode.Name + ">"
                            + "</" + nodPolicy.ParentNode.Name + ">";

                        strPremiumXSLPath = "";
                        //strPremiumXSLPath				=	driverInputXML.Replace("","'");
                        strPremiumXSLPath = driverInputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>OPERATOR</TYPE>");
                        strPremiumXSLPath = strPremiumXSLPath.Replace("\"", "'");
                        premiumXSLPath = GetProductPremiumPath(strPremiumXSLPath, "BOAT");
                        //Praveen

                        strProductFactorPath = GetProductFactorMasterPath(driverInputXML, "BOAT");
                        driverInputXML = "<INPUTXML>" + driverInputXML + "</INPUTXML>";
                        //driverInputXML					=	driverInputXML.Replace("&","&amp;");  //Commented on 18 April


                        string tempStr = GetPremiumComponent(driverInputXML, premiumXSLPath, strStateName);
                        tempStr = tempStr.Replace("<PRIMIUM>", "");
                        tempStr = tempStr.Replace("</PRIMIUM>", "");
                        //driverComponent					=	driverComponent + tempStr.Replace(strAdditionalText,"AUTO");
                        /*Added the OPERATOR Node*/
                        if (nodChild.Attributes["ID"] != null)
                            driverComponent = driverComponent + "<OPERATOR ID='" + nodChild.Attributes["ID"].Value.ToString() + "'>" + tempStr.Replace(strAdditionalText, "BOAT") + "</OPERATOR>";
                        else
                        {
                            if (nodChild.Attributes["id"] != null)
                                driverComponent = driverComponent + "<OPERATOR ID='" + nodChild.Attributes["id"].Value.ToString() + "'>" + tempStr.Replace(strAdditionalText, "BOAT") + "</OPERATOR>";
                        }



                        //For each driver get the Violation/Accident for display (if any)
                        XmlNode nodViolations = nodChild.SelectSingleNode("VIOLATIONS");
                        if (nodViolations != null && nodViolations.HasChildNodes == true)
                        {
                            XmlNodeList nodLstViolation = nodViolations.SelectNodes("VIOLATION");
                            if (nodLstViolation.Count > 0)
                            {
                                //premiumXslPath				=	System.Configuration.ConfigurationSettings.AppSettings.Get("PremiumViolationComponentXSL").ToString();
                                //premiumXslPath					=	"D:/Projects/EBX-DV25/source code/cms/cmsweb/XSL/Quote/Watercraft/Premium_ViolationComponent.xsl";
                                //premiumXslPath					=	ClsCommon.GetKeyValueWithIP("PremiumXSLWatercraft_ViolationComponent"); 

                                foreach (XmlNode nodTemp in nodLstViolation)
                                {
                                    string violationInputXML = "<QUICKQUOTE>" + nodTemp.OuterXml + "</QUICKQUOTE>";
                                    driverInputXML = driverInputXML.Replace("<INPUTXML>", "");
                                    driverInputXML = driverInputXML.Replace("</INPUTXML>", "");

                                    strPremiumXSLPath = "";
                                    strPremiumXSLPath = driverInputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>VIOLATION</TYPE>");
                                    premiumXSLPath = GetProductPremiumPath(strPremiumXSLPath, "BOAT");
                                    violationInputXML = "<INPUTXML>" + violationInputXML + "</INPUTXML>";
                                    violationInputXML = violationInputXML.Replace("&", "&amp;");

                                    tempStr = GetPremiumComponent(violationInputXML, premiumXSLPath);
                                    tempStr = tempStr.Replace("<PRIMIUM>", "");
                                    tempStr = tempStr.Replace("</PRIMIUM>", "");
                                    if (nodTemp.Attributes["ID"] != null)
                                        driverComponent = driverComponent + "<VIOLATION ID='" + nodTemp.Attributes["ID"].Value.ToString() + "'>" + tempStr.Replace(strAdditionalText, "BOAT") + "</VIOLATION>";
                                    else
                                    {
                                        if (nodTemp.Attributes["id"] != null)
                                            driverComponent = driverComponent + "<VIOLATION ID='" + nodTemp.Attributes["id"].Value.ToString() + "'>" + tempStr.Replace(strAdditionalText, "BOAT") + "</VIOLATION>";

                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region BOAT
                    /* Run the loop for each driver component and transform
					 * Store the results in a variable
					 */
                    string BdriverInputXML = "";
                    //make inputs for drivers
                    XmlNode BnodPolicy = xmlTempElement.FirstChild.FirstChild;
                    XmlNode BnodVehicles = BnodPolicy.NextSibling;
                    XmlNode BnodDrivers = BnodVehicles.NextSibling;
                    string BoatCount = BnodVehicles.ChildNodes.Count.ToString();
                    foreach (XmlNode nodChild in BnodVehicles.ChildNodes)
                    {

                        BdriverInputXML = "<" + BnodPolicy.ParentNode.Name + ">"
                            + "<" + BnodPolicy.Name + ">" + BnodPolicy.InnerXml + "</" + BnodPolicy.Name + ">"
                            + "<" + nodChild.ParentNode.Name + ">" + nodChild.OuterXml + "</" + nodChild.ParentNode.Name + ">"
                            + BnodDrivers.OuterXml
                            + "</" + BnodPolicy.ParentNode.Name + ">";

                        BdriverInputXML = "<INPUTXML>" + BdriverInputXML + "</INPUTXML>";

                        //BdriverInputXML = BdriverInputXML.Replace("&","&amp;"); COmmented on 18 April 2008



                        BdriverInputXML = BdriverInputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE>" + "<BOATCOUNT>" + BoatCount + "</BOATCOUNT>");

                        //Inserted By SKB START


                        strPremiumXSLPath = "";
                        strPremiumXSLPath = BdriverInputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>BOAT</TYPE>");
                        premiumXSLPath = GetProductPremiumPath(strPremiumXSLPath, "BOAT");
                        //BdriverInputXML				=	"<INPUTXML>" + BdriverInputXML + "</INPUTXML>";

                        //string strKeyValue			=	ClsCommon.GetKeyValueWithIP("PremiumXSLWatercraft_BoatComponent").ToString();

                        QEngineDll.QuoteEngine strGetPath = new QEngineDll.QuoteEngine(BdriverInputXML, premiumXSLPath, strProductFactorPath);
                        string shortestPath = strGetPath.GetPath();
                        string tempStr = strGetPath.CalculatePremium(shortestPath.Trim());
                        //Inserted By SKB END

                        tempStr = tempStr.Replace("<PRIMIUM>", "");
                        tempStr = tempStr.Replace("</PRIMIUM>", "");

                        string riskId = "", riskType = "";

                        if (nodChild.Attributes["ID"] != null)
                        {
                            riskId = nodChild.Attributes["ID"].Value.ToString();
                            if (nodChild.Attributes["BOATTYPE"] != null)
                                riskType = nodChild.Attributes["BOATTYPE"].Value.ToString();


                        }
                        else //For old records 
                        {
                            if (nodChild.Attributes["id"] != null)
                            {
                                riskId = nodChild.Attributes["id"].Value.ToString();
                                if (nodChild.Attributes["BOATTYPE"] != null)
                                    riskType = nodChild.Attributes["BOATTYPE"].Value.ToString();
                            }
                        }
                        boatComponent = boatComponent + "<RISK ID='" + riskId + "' TYPE='" + riskType + "'>" + tempStr.Replace(strAdditionalText, "") + "</RISK>";

                    }
                    #endregion

                    #region SCHEDULED MISCELLANEOUS SPORTS EQUIPMENT
                    /* FOR scheduled sports items */

                    int RiskCount = 1;
                    scheduledSportsInputXML = "";
                    scheduledSportsComponent = "";
                    nodPolicy = xmlTempElement.FirstChild.FirstChild;
                    XmlNode nodScheduledItems = nodPolicy.SelectSingleNode("SCHEDULEDMISCSPORTS");
                    SchRiskId = nodScheduledItems.ChildNodes.Count;
                    XmlNode nodBoats = nodPolicy.NextSibling;
                    foreach (XmlNode nodChild in nodScheduledItems.ChildNodes)
                    {

                        scheduledSportsInputXML = "<" + nodPolicy.ParentNode.Name + ">"
                            + "<" + nodPolicy.Name + ">" + nodPolicy.SelectSingleNode("ATTACHTOWOLVERINE").OuterXml + nodPolicy.SelectSingleNode("STATENAME").OuterXml + nodPolicy.SelectSingleNode("BOATHOMEDISC").OuterXml
                            + "<" + nodChild.ParentNode.Name + ">" + nodChild.OuterXml + "</" + nodChild.ParentNode.Name + ">"
                            + "</" + nodPolicy.Name + ">"
                            + "</" + nodPolicy.ParentNode.Name + ">";

                        //scheduledSportsInputXML	= scheduledSportsInputXML.Replace("&","&amp;");  

                        //This is for fetching the PorductfactorPath

                        XmlDocument xmlCheckQQEffDate = new XmlDocument();
                        xmlCheckQQEffDate.LoadXml(BdriverInputXML);

                        //Get the QUOTEEFFDATE from the input
                        XmlNodeList nodlstdate = xmlCheckQQEffDate.GetElementsByTagName("QUOTEEFFDATE");
                        //XmlNodeList nodDateList;
                        string strQEffectiveDate = "<QUOTEEFFDATE>" + nodlstdate.Item(0).InnerText + "</QUOTEEFFDATE>";

                        strPremiumXSLPath = "";
                        strPremiumXSLPath = scheduledSportsInputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>SCHEDULEDEQUIPMENT</TYPE>");
                        strPremiumXSLPath = strPremiumXSLPath.Replace("<POLICY>", "<POLICY>" + strQEffectiveDate);
                        premiumXSLPath = GetProductPremiumPath(strPremiumXSLPath, "BOAT");
                        //scheduledSportsInputXML =	scheduledSportsInputXML.Replace("<POLICY>","<POLICY>" + strQEffectiveDate);
                        //strProductFactorPath	=	GetProductFactorMasterPath(scheduledSportsInputXML,"BOAT");
                        //Added By PK (START)
                        scheduledSportsInputXML = "<INPUTXML>" + scheduledSportsInputXML + "</INPUTXML>";
                        //string strKeyValue1		=	ClsCommon.GetKeyValueWithIP("PremiumXSLWatercraft_ScheduledEquipmentComponent").ToString();
                        //Added By PK( END)
                        QEngineDll.QuoteEngine strGetPath = new QEngineDll.QuoteEngine(scheduledSportsInputXML, premiumXSLPath, strProductFactorPath);
                        string shortestPath = strGetPath.GetPath();
                        string tempStr = strGetPath.CalculatePremium(shortestPath.Trim());



                        //Inserted By SKB END

                        tempStr = tempStr.Replace("<PRIMIUM>", "");
                        tempStr = tempStr.Replace("</PRIMIUM>", "");
                        /*Added RISK ID AND TYPE*/
                        string riskIdMisc = "";
                        const string riskMiscType = "MSE";
                        if (RiskCount <= SchRiskId)
                        {
                            riskIdMisc = nodChild.Attributes["ID"].Value.ToString();
                            //riskIdMisc = RiskCount.ToString();
                            RiskCount++;

                        }

                        scheduledSportsComponent = scheduledSportsComponent + "<RISK ID ='" + riskIdMisc + "' TYPE = '" + riskMiscType + "' >" + tempStr.Replace(strAdditionalText, "") + "</RISK>";
                        //scheduledSportsComponent = scheduledSportsComponent + "<SCHEDULEDMISCSPORTS>" + tempStr.Replace(strAdditionalText,"") +"</SCHEDULEDMISCSPORTS>";
                    }

                    #endregion
                    // added by Neeraj Singh for Unattached Equipment
                    #region Unattached Equipment
                    string scheduledUnattachedInputXML = "";
                    scheduledUnattachedComponent = "";
                    nodPolicy = xmlTempElement.FirstChild.FirstChild;
                    XmlNode nodBoat = xmlTempElement.SelectSingleNode("QUICKQUOTE/BOATS/BOAT");
                    XmlNode nodUnattachedItems = nodBoat.SelectSingleNode("UNATTACHEDEQUIPMENT");

                    foreach (XmlNode nodChild in nodUnattachedItems.ChildNodes)
                    {

                        scheduledUnattachedInputXML = "<" + (nodBoat.ParentNode).ParentNode.Name + ">"
                            + nodPolicy.OuterXml
                            + "<" + nodBoat.Name + ">" + nodBoat.SelectSingleNode("UNATTACHEDEQUIPMENT").OuterXml + nodBoat.SelectSingleNode("UNATTACHEDEQUIPMENT_DEDUCTIBLE").OuterXml
                            + "<" + nodChild.ParentNode.Name + ">" + nodChild.OuterXml + "</" + nodChild.ParentNode.Name + ">"
                            + "</" + nodBoat.Name + ">"
                            + "</" + (nodBoat.ParentNode).ParentNode.Name + ">";

                        scheduledUnattachedInputXML = scheduledUnattachedInputXML.Replace("&", "&amp;");

                        //This is for fetching the PorductfactorPath

                        XmlDocument xmlCheckQQEffDate = new XmlDocument();
                        xmlCheckQQEffDate.LoadXml(BdriverInputXML);

                        //Get the QUOTEEFFDATE from the input
                        XmlNodeList nodlstdate = xmlCheckQQEffDate.GetElementsByTagName("QUOTEEFFDATE");
                        //XmlNodeList nodDateList;
                        string strQEffectiveDate = "<QUOTEEFFDATE>" + nodlstdate.Item(0).InnerText + "</QUOTEEFFDATE>";

                        strPremiumXSLPath = "";
                        strPremiumXSLPath = scheduledUnattachedInputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>UNATTACHEDEQUIPMENT</TYPE>");
                        strPremiumXSLPath = strPremiumXSLPath.Replace("<POLICY>", "<POLICY>" + strQEffectiveDate);
                        premiumXSLPath = GetProductPremiumPath(strPremiumXSLPath, "BOAT");
                        scheduledUnattachedInputXML = "<INPUTXML>" + scheduledUnattachedInputXML + "</INPUTXML>";
                        QEngineDll.QuoteEngine strGetPath = new QEngineDll.QuoteEngine(scheduledUnattachedInputXML, premiumXSLPath, strProductFactorPath);
                        string shortestPath = strGetPath.GetPath();
                        string tempStr = strGetPath.CalculatePremium(shortestPath.Trim());


                        tempStr = tempStr.Replace("<PRIMIUM>", "");
                        tempStr = tempStr.Replace("</PRIMIUM>", "");
                        /*Added RISK ID AND TYPE*/
                        string riskIdMisc = "";
                        //const string riskMiscType = "MSE"; 
                        //Above constant string has been replaced for Watercraft Unattached Equipment Premium
                        const string riskMiscType = "UAE";
                        if (nodBoat.Attributes["ID"] != null)
                        {
                            riskIdMisc = "1";//nodBoat.Attributes["ID"].Value.ToString();
                        }
                        scheduledUnattachedComponent = scheduledUnattachedComponent + "<RISK ID ='" + riskIdMisc + "' TYPE = '" + riskMiscType + "' >" + tempStr.Replace(strAdditionalText, "") + "</RISK>";

                    }

                    #endregion

                    finalPremiumXML = strAdditionalText + "<PRIMIUM>" + driverComponent + boatComponent + scheduledUnattachedComponent + scheduledSportsComponent + "</PRIMIUM>";
                    finalPremiumXML = finalPremiumXML.Replace("&", "&amp;");
                    //RiskCount=0;

                }

                //---------------------------------------------------------------------------------

                //added by praveen singh  
                string str_Title = "";
                string RATING_HEADER = "";
                RATING_HEADER = ClsCommon.GetKeyValue("Rating_Header");
                str_Title = "<HEADER TITLE='" + RATING_HEADER + "'> </HEADER></PRIMIUM>";
                finalPremiumXML = finalPremiumXML.Replace("</PRIMIUM>", str_Title);

                //to add the Business type 				

                if (NEW_BUSINESS.ToUpper().Trim() == "TRUE" || NEW_BUSINESS.ToUpper().Trim() == "Y")
                {
                    BUSINESS_TYPE = "New";
                    strBusinessType = "New";//Added by Manoj 16.Sep.2009
                }
                else
                {
                    BUSINESS_TYPE = "Ren";
                    strBusinessType = "Ren";//Added by Manoj 16.Sep.2009
                }
                //to add the Qeffective date rate and effective date 

                QUOTE_DATE = " QUOTE_DATE='" + DateTime.Now.ToShortDateString().ToString() + "'";
                QUOTE_EFFECTIVE_DATE = " QUOTE_EFFECTIVE_DATE='" + string.Format(QUOTE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";
                RATE_EFFECTIVE_DATE = " RATE_EFFECTIVE_DATE='" + string.Format(RATE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";
                BUSINESS_TYPE = " BUSINESS_TYPE='" + BUSINESS_TYPE + "'";

                string final_string = "<HEADER " + QUOTE_DATE + QUOTE_EFFECTIVE_DATE + RATE_EFFECTIVE_DATE + BUSINESS_TYPE + " ";

                finalPremiumXML = finalPremiumXML.Replace("<HEADER", final_string);

                //Get the Primary App Nodes
                XmlDocument xmlAppDoc = new XmlDocument();
                string strInputAppXML = "<INPUTXML>" + inputXML + "</INPUTXML>";
                string strFName = "";
                string strLName = "";
                string strAdd1 = "";
                string strAdd2 = "";
                string strCity = "";
                string strState = "";
                string strZipcode = "";
                string strStateCode = "";
                //AGency Info
                string strAgencyAdd1 = "";
                string strAgencyAdd2 = "";
                string strAgencyCity = "";
                string strAgencyState = "";
                string strAgencyZip = "";
                string strAgencyName = "";
                string strAgencyPhone = "";
                //QQ,APP,POL Info				
                string strQQNumber = "";
                string strAPPNumber = "";
                string strAPPVersion = "";
                string strPOLNumber = "";
                string strPOLVersion = "";

                strInputAppXML = strInputAppXML.Replace("&", "&amp;");
                xmlAppDoc.LoadXml(strInputAppXML);
                XmlElement xmlAppElement = xmlAppDoc.DocumentElement;
                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/FIRST_NAME") != null)
                    strFName = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/FIRST_NAME").InnerText;
                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/LAST_NAME") != null)
                    strLName = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/LAST_NAME").InnerText;
                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS1") != null)
                    strAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS1").InnerText;
                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS2") != null)
                    strAdd2 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS2").InnerText;
                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/CITY") != null)
                    strCity = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/CITY").InnerText;
                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE") != null)
                    strState = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE").InnerText;
                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ZIP_CODE") != null)
                    strZipcode = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ZIP_CODE").InnerText;

                //AGency Info

                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1") != null)
                    strAgencyAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1").InnerText;

                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1") != null)
                    strAgencyAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1").InnerText;

                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_CITY") != null)
                    strAgencyCity = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_CITY").InnerText;


                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_STATE") != null)
                    strAgencyState = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_STATE").InnerText;

                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ZIP") != null)
                    strAgencyZip = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ZIP").InnerText;


                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME") != null)
                    strAgencyName = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME").InnerText;

                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_PHONE") != null)
                    strAgencyPhone = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_PHONE").InnerText;

                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE_CODE") != null)
                    strStateCode = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE_CODE").InnerText;
                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/QQNUMBER") != null)
                    strQQNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/QQNUMBER").InnerText.ToString().Trim();
                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_NUMBER") != null)
                    strAPPNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_NUMBER").InnerText.ToString().Trim();
                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_VERSION") != null)
                    strAPPVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_VERSION").InnerText.ToString().Trim();
                //POL
                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_NUMBER") != null)
                    strPOLNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_NUMBER").InnerText.ToString().Trim();
                if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_VERSION") != null)
                    strPOLVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_VERSION").InnerText.ToString().Trim();





                string strClientTop = "<CLIENT_TOP_INFO   PRIMARY_APP_NAME ='" + strFName + ' ' + strLName + "'" +
                    " PRIMARY_APP_ADDRESS1 ='" + strAdd1 + "'" +
                    " PRIMARY_APP_ADDRESS2 ='" + strAdd2 + "'" +
                    " PRIMARY_APP_CITY ='" + strCity + "'" +
                    " PRIMARY_APP_STATE ='" + strState + "'" +
                    " PRIMARY_APP_ZIPCODE ='" + strZipcode + "'" +

                    " AGENCY_ADD1='" + strAgencyAdd1 + "'" +
                    " AGENCY_ADD2='" + strAgencyAdd2 + "'" +
                    " AGENCY_CITY='" + strAgencyCity + "'" +
                    " AGENCY_STATE='" + strAgencyState + "'" +
                    " AGENCY_ZIP='" + strAgencyZip + "'" +

                    " AGENCY_NAME='" + strAgencyName + "'" +
                    " AGENCY_PHONE='" + strAgencyPhone + "'" +
                    " QQ_NUMBER='" + strQQNumber + "'" +
                    " APP_NUMBER='" + strAPPNumber + "'" +
                    " APP_VERSION='" + strAPPVersion + "'" +
                    " POL_NUMBER='" + strPOLNumber + "'" +
                    " POL_VERSION='" + strPOLVersion + "'" +
                    " STATE_CODE ='" + strStateCode + "'>";

                finalPremiumXML = finalPremiumXML.Replace("</HEADER>", "</HEADER>" + strClientTop + "</CLIENT_TOP_INFO>");

                //End Primary Applicants Nodes

                finalPremiumXML = finalPremiumXML.Replace("\r\n", "");

                //Scheduled Miscellaneous Sports Equipment - Minimum Premium - Asfa Praveen (19-June-2007)
                strProductFactorPath = GetProductFactorMasterPath(inputXML, "BOAT");

                string SchMinInputXML = inputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>POLICY</TYPE>");
                premiumXSLPath = GetProductPremiumPath(SchMinInputXML, "BOAT");
                SchMinInputXML = "<INPUTXML>" + SchMinInputXML + "</INPUTXML>";

                string strTemp = "<?xml version='1.0' ?> <?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?><!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple' xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'> <!ATTLIST person id ID #IMPLIED>]>";
                finalPremiumXML = finalPremiumXML.Substring(strTemp.Length - 1);
                //finalPremiumXML = finalPremiumXML.Replace("&",""); --Commented on 18 April 2008

                QEngineDll.QuoteEngine strGetPath1 = new QEngineDll.QuoteEngine(finalPremiumXML, premiumXSLPath, strProductFactorPath);
                string shortestPath1 = strGetPath1.GetPath();
                string tempStr1 = strGetPath1.CalculatePremium(shortestPath1.Trim());

                tempStr1 = tempStr1.Replace("<PRIMIUM>", "");
                tempStr1 = tempStr1.Replace("</PRIMIUM>", "");
                MinimumPremiumComponent = MinimumPremiumComponent + tempStr1.Replace(strAdditionalText, "");
                string riskType1 = "";
                riskType1 = "MC";
                MinimumPremiumComponent = "<MINIM  TYPE='" + riskType1 + "'>" + MinimumPremiumComponent + "</MINIM>";

                //End  Minimum Premium XML
                finalPremiumXML = finalPremiumXML.Replace("</PRIMIUM>", MinimumPremiumComponent + "</PRIMIUM>");

                finalPremiumXML = finalPremiumXML.Replace("&", "&amp;");
                finalPremiumXML = finalPremiumXML.Replace("\r\n", "");
                finalPremiumXML = finalPremiumXML.Replace("\t", "");

                // ACCESS the premium xml for minimum premium component for Physical Damage, Watercraft Liability

                XmlDocument docPremiumInput = new XmlDocument();
                docPremiumInput.LoadXml(finalPremiumXML);

                XmlElement xmlTempElementinPremiumxml = docPremiumInput.DocumentElement;
                XmlNode nodpremiumxml = xmlTempElementinPremiumxml;

                XmlNode nodTempBoatInPremium, nodTempBoatInPremium_min, nodTempBoat, nodTempRisk, nodTempActBoatInPremium;

                int countOfBoat = 0, TotalPremium = 0, counts = 1;

                // count number of Boat

                XmlNodeList nodBoatInInput = docPremiumInput.SelectNodes("PRIMIUM/RISK[@TYPE='B']");
                countOfBoat = nodBoatInInput.Count;
                /*
                1) Fetch physical damage component
                2) Fetch watercraft liability premium
                3) Check for values in physical damage premium if not null then add adjust minimum premium in it 
                4) otherwise add Watercraft liability + adjust premium
                */
                nodTempBoatInPremium_min = docPremiumInput.SelectSingleNode("PRIMIUM/MINIM/STEP[@COMPONENT_CODE='MINIMUMPRE']/@STEPPREMIUM");
                if (nodTempBoatInPremium_min.InnerText != "" && nodTempBoatInPremium_min.InnerText != "0")
                {
                    foreach (XmlNode nodChild in nodpremiumxml.ChildNodes)
                    {

                        nodTempBoat = docPremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='" + counts.ToString() + "' and @TYPE='B']/STEP");
                        if (nodTempBoat != null)
                        {
                            nodTempRisk = docPremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='" + counts.ToString() + "' and @TYPE='B']");
                            nodTempBoatInPremium = docPremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='" + counts.ToString() + "' and @TYPE='B']/STEP[@COMPONENT_CODE='BOAT_PD']/@STEPPREMIUM");
                            nodTempActBoatInPremium = docPremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='" + counts.ToString() + "' and @TYPE='B']/STEP[@COMPONENT_CODE='BOAT_PD']/@COMP_ACT_PRE");
                            if (nodTempRisk != null)
                            {
                                if (nodTempBoatInPremium.InnerText != "" && nodTempBoatInPremium.InnerText != "0")
                                {
                                    // Coverage Watercraft Physical Damage Premium
                                    TotalPremium = System.Convert.ToInt32(nodTempBoatInPremium.InnerText) + System.Convert.ToInt32(nodTempBoatInPremium_min.InnerText);
                                    nodTempBoatInPremium.InnerText = TotalPremium.ToString();
                                    nodTempActBoatInPremium.InnerText = TotalPremium.ToString();
                                }
                                else
                                {
                                    // Coverage Watercraft Liability Premium
                                    nodTempBoatInPremium = docPremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='" + counts.ToString() + "' and @TYPE='B']/STEP[@COMPONENT_CODE='BOAT_LIABILITY_PREMIUM']/@STEPPREMIUM");
                                    nodTempActBoatInPremium = docPremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='" + counts.ToString() + "' and @TYPE='B']/STEP[@COMPONENT_CODE='BOAT_LIABILITY_PREMIUM']/@COMP_ACT_PRE");
                                    if (nodTempBoatInPremium.InnerText == "")
                                    {
                                        nodTempBoatInPremium.InnerText = "0";
                                        nodTempActBoatInPremium.InnerText = "0";
                                    }
                                    TotalPremium = System.Convert.ToInt32(nodTempBoatInPremium.InnerText) + System.Convert.ToInt32(nodTempBoatInPremium_min.InnerText);
                                    nodTempBoatInPremium.InnerText = TotalPremium.ToString();
                                    nodTempActBoatInPremium.InnerText = TotalPremium.ToString();
                                }

                                nodTempBoatInPremium = docPremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='" + counts.ToString() + "']/STEP[@COMPONENT_CODE='SUMTOTAL']/@STEPPREMIUM");
                                nodTempActBoatInPremium = docPremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='" + counts.ToString() + "']/STEP[@COMPONENT_CODE='SUMTOTAL']/@COMP_ACT_PRE");
                                TotalPremium = System.Convert.ToInt32(nodTempBoatInPremium.InnerText) + System.Convert.ToInt32(nodTempBoatInPremium_min.InnerText);
                                nodTempBoatInPremium.InnerText = TotalPremium.ToString();
                                nodTempActBoatInPremium.InnerText = TotalPremium.ToString();
                            }
                        }
                        counts++;
                    }
                }

                // Removing Scheduled Miscellaneous Sports Equipment heading from premium xml
                // Loading Input xml
                XmlDocument xmlTempInputDoc = new XmlDocument();
                xmlTempInputDoc.LoadXml(inputXML);
                // final premium xml
                finalPremiumXML = docPremiumInput.InnerXml;
                //Loading premium xml
                XmlDocument xmlSchDoc = new XmlDocument();
                xmlSchDoc.LoadXml(finalPremiumXML);
                XmlNode nodetempSch, nodetempSchMin, nodtempSchAct;

                //nodetempCountSch = xmlTempInputDoc.SelectSingleNode("QUICKQUOTE/POLICY/SCHEDULEDMISCSPORTS");
                XmlNodeList nodetempCountSch = xmlSchDoc.SelectNodes("PRIMIUM/RISK[@TYPE='MSE']");
                //SchRiskId = nodetempCountSch.Count;
                // remove above mentioned text from all risk except first one
                int cntMse = 0;
                string riskSchId = "";
                foreach (XmlNode Nod in nodetempCountSch)
                {
                    nodetempSch = Nod.SelectSingleNode("STEP/@GROUPDESC");
                    if (nodetempSch != null && cntMse == 0)
                    {
                        riskSchId = Nod.ParentNode.SelectSingleNode("RISK").Attributes["ID"].Value;
                        //nodetempSch.InnerText = "";
                    }
                    else
                    {
                        nodetempSch.InnerText = "";
                    }
                    cntMse++;
                }

                // If minimum premium schedule equipment add to first Schdule Equipment

                nodetempSchMin = xmlSchDoc.SelectSingleNode("PRIMIUM/MINIM[@TYPE='MC']/STEP[@COMPONENT_TYPE='P' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @COMPONENT_CODE='MINIMUMPREMIUM_SCH']/@STEPPREMIUM");
                nodetempSch = null;
                nodtempSchAct = null;
                if (riskSchId != "")
                {
                    nodetempSch = xmlSchDoc.SelectSingleNode("PRIMIUM/RISK[@ID=" + riskSchId + " and @TYPE='MSE']/STEP[@COMPONENT_TYPE='P' and @COMPONENT_CODE='SUMTOTAL_S']/@STEPPREMIUM");
                    nodtempSchAct = xmlSchDoc.SelectSingleNode("PRIMIUM/RISK[@ID=" + riskSchId + " and @TYPE='MSE']/STEP[@COMPONENT_TYPE='P' and @COMPONENT_CODE='SUMTOTAL_S']/@COMP_ACT_PRE");
                }
                if (nodetempSch != null)
                {
                    if (nodetempSchMin != null)
                    {
                        nodetempSch.InnerText = System.Convert.ToString(System.Convert.ToInt32(nodetempSch.InnerText) + System.Convert.ToInt32(nodetempSchMin.InnerText));
                        nodtempSchAct.InnerText = System.Convert.ToString(System.Convert.ToInt32(nodtempSchAct.InnerText) + System.Convert.ToInt32(nodetempSchMin.InnerText));
                        nodetempSchMin.InnerText = "";
                    }
                }
                return xmlSchDoc.InnerXml;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
            #endregion
        }




        public string GetUmbrelllaQuoteXmlForQuickQuote(string strInputXml)
        {
            #region UMB_premiumXml

            #region Declearation
            string finalPremiumXML = "";
            string strAdditionalText = "";
            string strProductFactorPath = "";
            string strPremiumXSLPath = "";
            string premiumXSLPath = "";
            string strStateName = "";
            string WatercraftComponent = "";
            string UmbrellaComponent = "";
            int WatercraftRiskId = 1;
            #endregion

            #region GET Umbrella Rating Quote
            try
            {


                //Get the QuoteXML *************************		
                strInputXml = strInputXml.Replace("$", "");
                if (strInputXml != "<NewDataSet />" && strInputXml != "")
                {


                    strAdditionalText = "<?xml version='1.0'?> " +
                        "<?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?>" +
                        "<!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple'" +
                        " xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'>" +
                        " <!ATTLIST person id ID #IMPLIED>" +
                        "]>";

                    XmlDocument xmlTempDoc = new XmlDocument();
                    string strInputXML = "<INPUTXML>" + strInputXml + "</INPUTXML>";
                    strInputXML = strInputXML.Replace("&", "&amp;");
                    //strInputXML = strInputXML.Replace("'","h673GSUYD7G3J73UDH");
                    xmlTempDoc.LoadXml(strInputXML);
                    XmlElement xmlTempElement = xmlTempDoc.DocumentElement;

                    strStateName = xmlTempElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/STATENAME").InnerText;


                    // Fetching the ProductFactor master Path as per the InputXML
                    strProductFactorPath = GetProductFactorMasterPath(strInputXml, "UMB");

                    #region Waetrcraft

                    string WaetrcraftInputXML = "";

                    NEW_BUSINESS = xmlTempElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/NEWBUSINESSFACTOR").InnerText;
                    XmlNode nodUmbrella = xmlTempElement.SelectSingleNode("QUICKQUOTE");
                    XmlNode nodWaterCraftExposer = xmlTempElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/WATERCRAFT_EXPOSURES");
                    XmlNode nodApplicantinfo = xmlTempElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION");
                    XmlNode nodPersonal_Auto = xmlTempElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PERSONAL_AUTO_EXPOSURES");

                    //string premiumXslPath = "";
                    foreach (XmlNode nodChild in nodWaterCraftExposer.ChildNodes)
                    {

                        WaetrcraftInputXML = "<" + (nodWaterCraftExposer.ParentNode).ParentNode.Name + ">"
                            + "<" + nodWaterCraftExposer.ParentNode.Name + ">"
                            + "<" + nodApplicantinfo.Name + ">" + nodApplicantinfo.InnerXml + "</" + nodApplicantinfo.Name + ">"
                            + "<" + nodPersonal_Auto.Name + ">" + nodPersonal_Auto.InnerXml + "</" + nodPersonal_Auto.Name + ">"
                            + "<" + nodWaterCraftExposer.Name + ">"
                            + "<" + nodChild.Name + ">" + nodChild.InnerXml + "</" + nodChild.Name + ">"
                            + "</" + nodWaterCraftExposer.Name + ">"
                            + "</" + nodWaterCraftExposer.ParentNode.Name + ">"
                            + "</" + (nodWaterCraftExposer.ParentNode).ParentNode.Name + ">";
                        //WaetrcraftInputXML				=	WaetrcraftInputXML.Replace("<APPLICANT_INFIORMATION>","");
                        //WaetrcraftInputXML				=	WaetrcraftInputXML.Replace("</APPLICANT_INFIORMATION>","");
                        strPremiumXSLPath = WaetrcraftInputXML;
                        strPremiumXSLPath = WaetrcraftInputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>WATERCRAFT</TYPE>");
                        premiumXSLPath = GetProductPremiumPath(strPremiumXSLPath, "UMB");



                        strProductFactorPath = GetProductFactorMasterPath(WaetrcraftInputXML, "UMB");
                        WaetrcraftInputXML = "<INPUTXML>" + WaetrcraftInputXML + "</INPUTXML>";

                        WaetrcraftInputXML = WaetrcraftInputXML.Replace("&", "&amp;");


                        string tempStr = GetPremiumComponent(WaetrcraftInputXML, premiumXSLPath, strProductFactorPath);
                        tempStr = tempStr.Replace("<PRIMIUM>", "");
                        tempStr = tempStr.Replace("</PRIMIUM>", "");
                        //WatercraftComponent					=	WatercraftComponent + tempStr.Replace(strAdditionalText,"");
                        string riskIdMisc = "";
                        string riskMiscType = "WC";

                        if (nodChild.Attributes["ID"] != null)
                        {
                            riskIdMisc = nodChild.Attributes["ID"].Value.ToString();
                            if (nodChild.Attributes["BOATTYPE"] != null)
                                riskMiscType = nodChild.Attributes["BOATTYPE"].Value.ToString();
                            WatercraftComponent = WatercraftComponent + "<RISK ID ='" + WatercraftRiskId + "' TYPE = '" + riskMiscType + "'>" + tempStr.Replace(strAdditionalText, "") + "</RISK>";
                        }
                        WatercraftRiskId++;

                    }
                    #endregion

                    #region Umbrella


                    string UmbrellaInputXML = "";

                    NEW_BUSINESS = xmlTempElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/NEWBUSINESSFACTOR").InnerText;
                    XmlNode nodUmbrellas = xmlTempElement.SelectSingleNode("QUICKQUOTE");

                    foreach (XmlNode nodChild in nodUmbrellas.ChildNodes)
                    {
                        strPremiumXSLPath = strInputXml;
                        strPremiumXSLPath = strInputXml.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>UMBRELLA</TYPE>");
                        premiumXSLPath = GetProductPremiumPath(strPremiumXSLPath, "UMB");


                        UmbrellaInputXML = strInputXml;
                        strProductFactorPath = GetProductFactorMasterPath(strInputXml, "UMB");
                        UmbrellaInputXML = "<INPUTXML>" + UmbrellaInputXML + "</INPUTXML>";
                        UmbrellaInputXML = UmbrellaInputXML.Replace("&", "&amp;");
                        UmbrellaInputXML = UmbrellaInputXML.Replace("<APPLICANT_INFIORMATION>", "");
                        UmbrellaInputXML = UmbrellaInputXML.Replace("</APPLICANT_INFIORMATION>", "");

                        string tempStr = GetPremiumComponent(UmbrellaInputXML, premiumXSLPath, strProductFactorPath);
                        tempStr = tempStr.Replace("<PRIMIUM>", "");
                        tempStr = tempStr.Replace("</PRIMIUM>", "");
                        //UmbrellaComponent					=	UmbrellaComponent + tempStr.Replace(strAdditionalText,"UMB");
                        string riskIdMisc = "";
                        const string riskMiscType = "UMBL";

                        //if(nodChild.Attributes["ID"]!=null)
                        //{
                        riskIdMisc = "1"; //nodChild.Attributes["ID"].Value.ToString();
                        //UmbrellaInputXML					=	UmbrellaComponent + "<UMBRELLA ID='"+nodChild.Attributes["ID"].Value.ToString() +"'>"+ tempStr.Replace(strAdditionalText,"UMB")+"</UMBRELLA>";
                        //}
                        UmbrellaComponent = UmbrellaComponent + "<RISK ID ='" + riskIdMisc + "' TYPE = '" + riskMiscType + "' >" + tempStr.Replace(strAdditionalText, "") + "</RISK>";

                    }
                    #endregion

                    finalPremiumXML = strAdditionalText + "<PRIMIUM>" + UmbrellaComponent + WatercraftComponent + "</PRIMIUM>";
                    finalPremiumXML = finalPremiumXML.Replace("&", "&amp;");


                    string str_Title = "";
                    string RATING_HEADER = "";
                    RATING_HEADER = ClsCommon.GetKeyValue("Rating_Header");
                    str_Title = "<HEADER TITLE='" + RATING_HEADER + "'> </HEADER></PRIMIUM>";
                    finalPremiumXML = finalPremiumXML.Replace("</PRIMIUM>", str_Title);

                    //to add the Business type 


                    if (NEW_BUSINESS.ToUpper().Trim() == "TRUE" || NEW_BUSINESS.ToUpper().Trim() == "Y")
                    {
                        BUSINESS_TYPE = "New";
                        strBusinessType = "New";//Added by manoj 16.Sep.2009
                    }
                    else
                    {
                        BUSINESS_TYPE = "Ren";
                        strBusinessType = "Ren";//Added by manoj 16.Sep.2009
                    }
                    //to add the Qeffective date rate and effective date 

                    QUOTE_DATE = " QUOTE_DATE='" + DateTime.Now.ToShortDateString().ToString() + "'";
                    QUOTE_EFFECTIVE_DATE = " QUOTE_EFFECTIVE_DATE='" + string.Format(QUOTE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";
                    RATE_EFFECTIVE_DATE = " RATE_EFFECTIVE_DATE='" + string.Format(RATE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";
                    BUSINESS_TYPE = " BUSINESS_TYPE='" + BUSINESS_TYPE + "'";

                    string final_string = "<HEADER " + QUOTE_DATE + QUOTE_EFFECTIVE_DATE + RATE_EFFECTIVE_DATE + BUSINESS_TYPE + " ";

                    finalPremiumXML = finalPremiumXML.Replace("<HEADER", final_string);

                    //Get the Primary App Nodes
                    XmlDocument xmlAppDoc = new XmlDocument();
                    string strInputAppXML = "<INPUTXML>" + strInputXml + "</INPUTXML>";
                    string strFName = "";
                    string strLName = "";
                    string strAdd1 = "";
                    string strAdd2 = "";
                    string strCity = "";
                    string strState = "";
                    string strZipcode = "";
                    string strStateCode = "";
                    //AGency Info
                    string strAgencyAdd1 = "";
                    string strAgencyAdd2 = "";
                    string strAgencyCity = "";
                    string strAgencyState = "";
                    string strAgencyZip = "";
                    string strAgencyName = "";
                    string strAgencyPhone = "";
                    //QQ,APP,POL Info				
                    string strQQNumber = "";
                    string strAPPNumber = "";
                    string strAPPVersion = "";
                    string strPOLNumber = "";
                    string strPOLVersion = "";

                    strInputAppXML = strInputAppXML.Replace("&", "&amp;");
                    xmlAppDoc.LoadXml(strInputAppXML);
                    XmlElement xmlAppElement = xmlAppDoc.DocumentElement;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/FIRST_NAME") != null)
                        strFName = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/FIRST_NAME").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/LAST_NAME") != null)
                        strLName = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/LAST_NAME").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/ADDRESS1") != null)
                        strAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/ADDRESS1").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/ADDRESS2") != null)
                        strAdd2 = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/ADDRESS2").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/CITY") != null)
                        strCity = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/CITY").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/STATE") != null)
                        strState = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/STATE").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/ZIP_CODE") != null)
                        strZipcode = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/ZIP_CODE").InnerText;

                    //AGency Info

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/AGENCY_ADD1") != null)
                        strAgencyAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/AGENCY_ADD1").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/AGENCY_ADD1") != null)
                        strAgencyAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/AGENCY_ADD1").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/AGENCY_CITY") != null)
                        strAgencyCity = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/AGENCY_CITY").InnerText;


                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/AGENCY_STATE") != null)
                        strAgencyState = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/AGENCY_STATE").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/AGENCY_ZIP") != null)
                        strAgencyZip = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/AGENCY_ZIP").InnerText;


                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME") != null)
                        strAgencyName = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/AGENCY_PHONE") != null)
                        strAgencyPhone = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/AGENCY_PHONE").InnerText.ToString().Trim();
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/STATE_CODE") != null)
                        strStateCode = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/PRIMARYAPPLICANT/STATE_CODE").InnerText;
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/QQNUMBER") != null)
                        strQQNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/QQNUMBER").InnerText.ToString().Trim();
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/APP_NUMBER") != null)
                        strAPPNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/APP_NUMBER").InnerText.ToString().Trim();
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/APP_VERSION") != null)
                        strAPPVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/APP_VERSION").InnerText.ToString().Trim();
                    //POL
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/POL_NUMBER") != null)
                        strPOLNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/POL_NUMBER").InnerText.ToString().Trim();
                    if (xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/POL_VERSION") != null)
                        strPOLVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/POL_VERSION").InnerText.ToString().Trim();





                    string strClientTop = "<CLIENT_TOP_INFO   PRIMARY_APP_NAME ='" + strFName + ' ' + strLName + "'" +
                        " PRIMARY_APP_ADDRESS1 ='" + strAdd1 + "'" +
                        " PRIMARY_APP_ADDRESS2 ='" + strAdd2 + "'" +
                        " PRIMARY_APP_CITY ='" + strCity + "'" +
                        " PRIMARY_APP_STATE ='" + strState + "'" +
                        " PRIMARY_APP_ZIPCODE ='" + strZipcode + "'" +

                        " AGENCY_ADD1='" + strAgencyAdd1 + "'" +
                        " AGENCY_ADD2='" + strAgencyAdd2 + "'" +
                        " AGENCY_CITY='" + strAgencyCity + "'" +
                        " AGENCY_STATE='" + strAgencyState + "'" +
                        " AGENCY_ZIP='" + strAgencyZip + "'" +

                        " AGENCY_NAME='" + strAgencyName + "'" +
                        " AGENCY_PHONE='" + strAgencyPhone + "'" +
                        " QQ_NUMBER='" + strQQNumber + "'" +
                        " APP_NUMBER='" + strAPPNumber + "'" +
                        " APP_VERSION='" + strAPPVersion + "'" +
                        " POL_NUMBER='" + strPOLNumber + "'" +
                        " POL_VERSION='" + strPOLVersion + "'" +
                        " STATE_CODE ='" + strStateCode + "'>";

                    finalPremiumXML = finalPremiumXML.Replace("</HEADER>", "</HEADER>" + strClientTop + "</CLIENT_TOP_INFO>");
                    finalPremiumXML = finalPremiumXML.Replace("ND", "");
                    //End Primary Applicants Nodes


                    //return finalPremiumXML ;

                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(finalPremiumXML);

                    XmlNode nodetempminimumpremium, nodtempumbrala, nodetempwatercraft;
                    int countofRisk;
                    // count of risk
                    XmlNodeList nodRisk = xmldoc.SelectNodes("PRIMIUM/RISK[@TYPE='WC']");
                    countofRisk = nodRisk.Count;
                    nodtempumbrala = xmldoc.SelectSingleNode("PRIMIUM/RISK[@ID='1' and @TYPE='UMBL']");
                    // Fetch minimum premium from umbrella risk
                    nodetempminimumpremium = xmldoc.SelectSingleNode("PRIMIUM/RISK[@ID='1' and @TYPE='UMBL']/STEP[@COMPONENT_CODE='MINIMUMPREMIUM']");
                    nodetempwatercraft = xmldoc.SelectSingleNode("PRIMIUM/RISK[@ID='" + countofRisk + "' and @TYPE='WC']");
                    if (nodetempwatercraft != null)
                    {
                        if (nodetempminimumpremium != null)
                        {
                            nodtempumbrala.RemoveChild(nodetempminimumpremium);
                        }
                        //nodetempwatercraft=xmldoc.SelectSingleNode("PRIMIUM/RISK[@TYPE='WC']");
                        nodetempwatercraft.AppendChild(nodetempminimumpremium);

                        /*for (int count =1; count<=countofRisk; count++)
                        {
							
                            if((countofRisk - 1) == count)
                            {
                                if(nodetempminimumpremium != null)
                                {
                                    nodetempwatercraft.RemoveChild(nodetempminimumpremium);
                                    //nodTempVehicleInPremium_min =	xmldoc.SelectSingleNode("PRIMIUM/MINIM/STEP[@COMPONENT_CODE='BI_MINMUM']/@STEPPREMIUM");
                                }
                                nodetempwatercraft = xmldoc.SelectSingleNode("PRIMIUM/RISK[@ID='"+count.ToString()+"' and @TYPE='WC']");
                                if(nodetempwatercraft != null)
                                {
                                    nodetempwatercraft.AppendChild(nodetempminimumpremium);
                                    //xmldoc.InsertAfter(nodetempminimumpremium,nodetempwatercraft.LastChild);
                                }
                            }
                        }*/
                    }
                    return xmldoc.InnerXml;
                }

            }

            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

            #endregion
            #endregion
            return strInputXml;
        }


        public string GetMotorcycleQuoteXMLForQuickQuote(string inputXML)
        {
            #region Declearation
            //string inputXML					=	"";		
            string finalPremiumXML = "";
            string driverComponent = "";
            string vehicleComponent = "";
            string strProductFactorPath = "";
            string MotorcycleInput = "";
            string premiumXSLPath = "";
            string MinimumInputXML = "";
            string MinimumPremiumComponent = "";
            string riskId = "", riskType = "";
            #endregion

            string CpyInputXML = "";

            #region Get Motorcycle Rating Quote
            try
            {
                #region		GENERATE QUOTE		<QUICKQUOTE>

                //Get the QuoteXML *************************	
                //inputXML	=	inputXML.Replace("&","&amp;");
                inputXML = inputXML.ToUpper();
                inputXML = inputXML.Replace("&AMP;", "&amp;");
                //inputXML = inputXML.Replace("'","h673GSUYD7G3J73UDH");


                if (inputXML != "<NewDataSet />" && inputXML != "")
                {

                    inputXML = inputXML.Replace("<?XML VERSION=\"1.0\" ENCODING=\"UTF-8\"?>", "");
                    CpyInputXML = inputXML;

                    //Fetching the ProductFactorPath [START]
                    XmlDocument xmlCheckQQEffDate = new XmlDocument();
                    xmlCheckQQEffDate.LoadXml(inputXML);
                    //Get the QUOTEEFFDATE from the input
                    XmlNodeList nodlstdate = xmlCheckQQEffDate.GetElementsByTagName("QUOTEEFFDATE");
                    string strQEffectiveDate = "<QUOTEEFFDATE>" + nodlstdate.Item(0).InnerText + "</QUOTEEFFDATE>";

                    strStateName = xmlCheckQQEffDate.SelectSingleNode("QUICKQUOTE/POLICY/STATENAME").InnerText;


                    strProductFactorPath = "";
                    strProductFactorPath = GetProductFactorMasterPath(inputXML, "CYCL");
                    //Fetching the ProductFactorPath [END]

                    string strAdditionalText = "<?xml version='1.0'?> " +
                        "<?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?>" +
                        "<!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple'" +
                        " xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'>" +
                        " <!ATTLIST person id ID #IMPLIED>" +
                        "]>";

                    XmlDocument xmlTempDoc = new XmlDocument();
                    string strInputXML = "<INPUTXML>" + inputXML + "</INPUTXML>";
                    xmlTempDoc.LoadXml(strInputXML);
                    XmlElement xmlTempElement = xmlTempDoc.DocumentElement;



                    /* Run the loop for each driver component and transform
                     * Store the results in a variable
                     */
                    string driverInputXML = "";
                    //make inputs for drivers
                    XmlNode nodPolicy = xmlTempElement.FirstChild.FirstChild;
                    XmlNode nodVehicles = nodPolicy.NextSibling;
                    XmlNode nodDrivers = nodVehicles.NextSibling;
                    foreach (XmlNode nodChild in nodDrivers.ChildNodes)
                    {

                        #region DRIVER COMPONENT
                        driverInputXML = "<" + nodPolicy.ParentNode.Name + ">"
                            + "<" + nodPolicy.Name + ">" + nodPolicy.InnerXml + "</" + nodPolicy.Name + ">"
                            + "<" + nodVehicles.Name + ">" + nodVehicles.InnerXml + "</" + nodVehicles.Name + ">"
                            + "<" + nodChild.ParentNode.Name + ">" + nodChild.OuterXml + "</" + nodChild.ParentNode.Name + ">"
                            + "</" + nodPolicy.ParentNode.Name + ">";



                        //string premiumXSLPath				=	ClsCommon.GetKeyValueWithIP("PremiumXSLMotorcycle_DriverComponentt");
                        MotorcycleInput = driverInputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>DRIVER</TYPE>");
                        premiumXSLPath = "";
                        premiumXSLPath = GetProductPremiumPath(MotorcycleInput, "CYCL");

                        driverInputXML = "<INPUTXML>" + driverInputXML + "</INPUTXML>";

                        QEngineDll.QuoteEngine strGetPath = new QEngineDll.QuoteEngine(driverInputXML, premiumXSLPath, strProductFactorPath);
                        string shortestPath = strGetPath.GetPath();
                        string tempStr = strGetPath.CalculatePremium(shortestPath.Trim());

                        //string tempStr = GetPremiumComponent(driverInputXML,"D:/Projects/EBX-DV25/source code/cms/cmsweb/XSL/Quote/Motorcycle/Premium_DriverComponent.xsl");

                        tempStr = tempStr.Replace("<PRIMIUM>", "");
                        tempStr = tempStr.Replace("</PRIMIUM>", "");
                        driverComponent = driverComponent + tempStr.Replace(strAdditionalText, "");

                        #region	For each driver get the Violation/Accident for display (if any)

                        XmlNode nodViolationAppl = nodChild.SelectSingleNode("VIOLATION_APPLICABLE");
                        XmlNode nodViolations = nodChild.SelectSingleNode("VIOLATIONS");
                        if (nodViolations != null && nodViolations.HasChildNodes == true)
                        {
                            XmlNodeList nodLstViolation = nodViolations.SelectNodes("VIOLATION");
                            if (nodLstViolation.Count > 0)
                            {
                                XmlNode nodTempDate = nodPolicy.SelectSingleNode("QUOTEEFFDATE");
                                string strQuoteEffectiveDate = "<QUOTEEFFDATE></QUOTEEFFDATE>";

                                if (nodTempDate != null)
                                {
                                    strQuoteEffectiveDate = nodTempDate.OuterXml.ToString().Trim();
                                }
                                //string premiumXslPath				=	ClsCommon.GetKeyValueWithIP("PremiumXSLMotorcycle_ViolationComponent");

                                nodTempDate = nodPolicy.SelectSingleNode("STATENAME");
                                string strTempStateName = "<STATENAME></STATENAME>";

                                if (nodTempDate != null)
                                {
                                    strTempStateName = nodTempDate.OuterXml.ToString().Trim();
                                }

                                foreach (XmlNode nodTemp in nodLstViolation)
                                {


                                    string violationInputXML = "";
                                    if (nodViolationAppl != null)
                                    {
                                        violationInputXML = "<QUICKQUOTE>" + strQuoteEffectiveDate + strTempStateName + nodTemp.OuterXml + nodViolationAppl.OuterXml + "</QUICKQUOTE>";
                                    }
                                    else
                                    {
                                        violationInputXML = "<QUICKQUOTE>" + strQuoteEffectiveDate + strTempStateName + nodTemp.OuterXml + "</QUICKQUOTE>";
                                    }
                                    strProductFactorPath = GetProductFactorMasterPath(violationInputXML, "CYCL");

                                    violationInputXML = violationInputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>VIOLATION</TYPE>");
                                    premiumXSLPath = GetProductPremiumPath(violationInputXML, "CYCL");
                                    violationInputXML = "<INPUTXML>" + violationInputXML + "</INPUTXML>";
                                    tempStr = GetPremiumComponent(violationInputXML, premiumXSLPath);
                                    tempStr = tempStr.Replace("<PRIMIUM>", "");
                                    tempStr = tempStr.Replace("</PRIMIUM>", "");
                                    driverComponent = driverComponent + tempStr.Replace(strAdditionalText, "CYCL");
                                }
                            }
                        }

                        #endregion


                        # endregion END OF DRIVER COMPONENT

                    }

                    #region	VEHICLE COMPONENT

                    /* Run the loop for vehicles component for each vehicle and transform
					 * Store the results in a variable */
                    string vehicleInputXML = "";
                    //make inputs for vehicles
                    nodPolicy = xmlTempElement.FirstChild.FirstChild;
                    nodVehicles = nodPolicy.NextSibling;
                    nodDrivers = nodVehicles.NextSibling;

                    foreach (XmlNode nodChild in nodVehicles.ChildNodes)
                    {
                        vehicleInputXML = "<" + nodPolicy.ParentNode.Name + ">"
                            + "<" + nodPolicy.Name + ">" + nodPolicy.InnerXml + "</" + nodPolicy.Name + ">"
                            + "<" + nodChild.ParentNode.Name + ">" + nodChild.OuterXml + "</" + nodChild.ParentNode.Name + ">"
                            + "<" + nodDrivers.Name + ">" + nodDrivers.InnerXml + "</" + nodDrivers.Name + ">"
                            + "</" + nodPolicy.ParentNode.Name + ">";


                        //string premiumXSLPath				=	ClsCommon.GetKeyValueWithIP("PremiumXSLMotorcycle_VehicleComponent");
                        MotorcycleInput = "";
                        MotorcycleInput = vehicleInputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>VEHICLE</TYPE>");
                        premiumXSLPath = "";
                        premiumXSLPath = GetProductPremiumPath(MotorcycleInput, "CYCL");
                        strProductFactorPath = GetProductFactorMasterPath(MotorcycleInput, "CYCL");


                        vehicleInputXML = "<INPUTXML>" + MotorcycleInput + "</INPUTXML>";
                        vehicleInputXML = vehicleInputXML.Replace("\"", "'");


                        QEngineDll.QuoteEngine strGetPath = new QEngineDll.QuoteEngine(vehicleInputXML, premiumXSLPath, strProductFactorPath);
                        string shortestPath = strGetPath.GetPath();
                        string tempStr = strGetPath.CalculatePremium(shortestPath.Trim());


                        //added by praveen singh 
                        string str_Title = "";
                        string RATING_HEADER = "";
                        RATING_HEADER = ClsCommon.GetKeyValue("Rating_Header");
                        str_Title = "<HEADER TITLE='" + RATING_HEADER + "'> </HEADER></PRIMIUM>";
                        tempStr = tempStr.Replace("</PRIMIUM>", str_Title);


                        //to add the Business type 
                        NEW_BUSINESS = xmlTempDoc.SelectSingleNode("INPUTXML/QUICKQUOTE/POLICY/NEWBUSINESS").InnerText;


                        if (NEW_BUSINESS.ToString().ToUpper().Trim() == "TRUE" || NEW_BUSINESS.ToString().ToUpper().Trim() == "Y")
                        {
                            BUSINESS_TYPE = "New";
                            strBusinessType = "New";//Added by Manoj 16.Sep.2009
                        }
                        else
                        {
                            BUSINESS_TYPE = "Ren";
                            strBusinessType = "Ren";//Added by Manoj 16.Sep.2009
                        }

                        //to add the Qeffective date rate and effective date 

                        QUOTE_DATE = " QUOTE_DATE='" + DateTime.Now.ToShortDateString().ToString() + "'";
                        QUOTE_EFFECTIVE_DATE = " QUOTE_EFFECTIVE_DATE='" + string.Format(QUOTE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";
                        RATE_EFFECTIVE_DATE = " RATE_EFFECTIVE_DATE='" + string.Format(RATE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";

                        BUSINESS_TYPE = " BUSINESS_TYPE='" + BUSINESS_TYPE + "'";

                        string final_string = "<HEADER " + QUOTE_DATE + QUOTE_EFFECTIVE_DATE + RATE_EFFECTIVE_DATE + BUSINESS_TYPE + " ";

                        tempStr = tempStr.Replace("<HEADER", final_string);

                        //Get the Primary App Nodes
                        XmlDocument xmlAppDoc = new XmlDocument();
                        string strInputAppXML = "<INPUTXML>" + inputXML + "</INPUTXML>";
                        string strFName = "";
                        string strMName = "";
                        string strLName = "";
                        string strAdd1 = "";
                        string strAdd2 = "";
                        string strCity = "";
                        string strState = "";
                        string strZipcode = "";
                        string strStateCode = "";
                        //AGency Info
                        string strAgencyAdd1 = "";
                        string strAgencyAdd2 = "";
                        string strAgencyCity = "";
                        string strAgencyState = "";
                        string strAgencyZip = "";
                        string strAgencyName = "";
                        string strAgencyPhone = "";
                        //QQ,APP,POL Info				
                        string strQQNumber = "";
                        string strAPPNumber = "";
                        string strAPPVersion = "";
                        string strPOLNumber = "";
                        string strPOLVersion = "";
                        string strAPP_TERMS = "";

                        strInputAppXML = strInputAppXML.Replace("&", "&amp;");
                        xmlAppDoc.LoadXml(strInputAppXML);
                        XmlElement xmlAppElement = xmlAppDoc.DocumentElement;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/FIRST_NAME") != null)
                            strFName = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/FIRST_NAME").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/MIDDLE_NAME") != null)
                            strMName = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/MIDDLE_NAME").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/LAST_NAME") != null)
                            strLName = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/LAST_NAME").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS1") != null)
                            strAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS1").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS2") != null)
                            strAdd2 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS2").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/CITY") != null)
                            strCity = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/CITY").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE") != null)
                            strState = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ZIP_CODE") != null)
                            strZipcode = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/ZIP_CODE").InnerText;

                        //AGency Info

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1") != null)
                            strAgencyAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1").InnerText;

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1") != null)
                            strAgencyAdd1 = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1").InnerText;

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_CITY") != null)
                            strAgencyCity = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_CITY").InnerText;


                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_STATE") != null)
                            strAgencyState = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_STATE").InnerText;

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ZIP") != null)
                            strAgencyZip = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ZIP").InnerText;


                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME") != null)
                            strAgencyName = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_DISPLAY_NAME").InnerText;

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_PHONE") != null)
                            strAgencyPhone = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_PHONE").InnerText;

                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE_CODE") != null)
                            strStateCode = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE_CODE").InnerText;
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/QQNUMBER") != null)
                            strQQNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/QQNUMBER").InnerText.ToString().Trim();
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_NUMBER") != null)
                            strAPPNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_NUMBER").InnerText.ToString().Trim();
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_VERSION") != null)
                            strAPPVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/APP_VERSION").InnerText.ToString().Trim();
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_NUMBER") != null)
                            strPOLNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_NUMBER").InnerText.ToString().Trim();
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_VERSION") != null)
                            strPOLVersion = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POL_VERSION").InnerText.ToString().Trim();
                        if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POLICYTERMS") != null)
                            strAPP_TERMS = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/POLICYTERMS").InnerText.ToString().Trim();


                        string strClientTop = "<CLIENT_TOP_INFO PRIMARY_APP_NAME ='" + strFName + ' ' + strMName + ' ' + strLName + "'" +
                            //" PRIMARY_APP_LNAME ='" + strLName + "'" +
                            " PRIMARY_APP_ADDRESS1 ='" + strAdd1 + "'" +
                            " PRIMARY_APP_ADDRESS2 ='" + strAdd2 + "'" +
                            " PRIMARY_APP_CITY ='" + strCity + "'" +
                            " PRIMARY_APP_STATE ='" + strState + "'" +
                            " PRIMARY_APP_ZIPCODE ='" + strZipcode + "'" +

                            " AGENCY_ADD1='" + strAgencyAdd1 + "'" +
                            " AGENCY_ADD2='" + strAgencyAdd2 + "'" +
                            " AGENCY_CITY='" + strAgencyCity + "'" +
                            " AGENCY_STATE='" + strAgencyState + "'" +
                            " AGENCY_ZIP='" + strAgencyZip + "'" +

                            " AGENCY_NAME='" + strAgencyName + "'" +
                            " AGENCY_PHONE='" + strAgencyPhone + "'" +
                            " QQ_NUMBER='" + strQQNumber + "'" +
                            " APP_NUMBER='" + strAPPNumber + "'" +
                            " APP_VERSION='" + strAPPVersion + "'" +
                            " POL_NUMBER='" + strPOLNumber + "'" +
                            " POL_VERSION='" + strPOLVersion + "'" +
                            " STATE_CODE ='" + strStateCode + "'" +
                            " APP_TERMS='" + strAPP_TERMS + "'>";
                        tempStr = tempStr.Replace("</HEADER>", "</HEADER>" + strClientTop + "</CLIENT_TOP_INFO>");

                        //End Primary Applicants Nodes

                        //string tempStr = GetPremiumComponent(vehicleInputXML,"D:/Projects/EBX-DV25/source code/cms/cmsweb/XSL/Quote/Motorcycle/Premium_VehicleComponent.xsl");
                        tempStr = tempStr.Replace("<PRIMIUM>", "");
                        tempStr = tempStr.Replace("</PRIMIUM>", "");
                        //vehicleComponent	=	vehicleComponent + tempStr.Replace(strAdditionalText,"");
                        /* Added Risk ID */

                        if (nodChild.Attributes["ID"] != null)
                        {
                            riskId = nodChild.Attributes["ID"].Value.ToString();
                            if (nodChild.SelectSingleNode("VEHICLETYPE") != null)
                                riskType = nodChild.SelectSingleNode("VEHICLETYPE").InnerText.ToString().Trim();
                        }
                        vehicleComponent = vehicleComponent + "<RISK ID='" + riskId + "' TYPE='" + riskType + "'>" + tempStr.Replace(strAdditionalText, "") + "</RISK>";
                        /*END  Added Risk ID */



                    }
                    #endregion

                    //Final XML string
                    finalPremiumXML = strAdditionalText + "<PRIMIUM>" + driverComponent + vehicleComponent + "</PRIMIUM>";
                    finalPremiumXML = finalPremiumXML.Replace("&", "&amp;");
                    // Total Minimum Premium For suspended comp  type
                    XmlDocument docpremiumXml = new XmlDocument();
                    docpremiumXml.LoadXml(finalPremiumXML);
                    XmlNode nodtempCompOnly, nodTempMinSusPrem;
                    XmlNodeList nodVehiclesInput = docpremiumXml.SelectNodes("PRIMIUM/RISK[@TYPE='A' or @TYPE='E' or @TYPE='G' or @TYPE='H' or @TYPE='T' or @TYPE='X']");

                    // For suspended comp vehicle add minimum premium (if exists) to Comp coverage
                    foreach (XmlNode nod in nodVehiclesInput)
                    {
                        nodtempCompOnly = nod.SelectSingleNode("STEP[@TS='0']/@GROUPDESC");
                        if (nodtempCompOnly != null && nodtempCompOnly.InnerText.ToUpper().Trim() == "MOTORCYCLES (COMPREHENSIVE ONLY)")
                        {
                            nodTempMinSusPrem = nod.SelectSingleNode("STEP[@COMPONENT_CODE='MIN_PREM_SUS']/@STEPPREMIUM");
                            nodtempCompOnly = nod.SelectSingleNode("STEP[@COMPONENT_CODE='COMP']/@STEPPREMIUM");
                            if (nodTempMinSusPrem != null && nodtempCompOnly != null)
                            {
                                if (nodTempMinSusPrem.InnerText != "" && nodTempMinSusPrem.InnerText != "0" && nodtempCompOnly.InnerText != "")
                                {
                                    nodtempCompOnly.InnerText = System.Convert.ToString(System.Convert.ToInt32(nodtempCompOnly.InnerText) + System.Convert.ToInt32(nodTempMinSusPrem.InnerText));
                                }
                            }
                            nodtempCompOnly = nod.SelectSingleNode("STEP[@COMPONENT_CODE='COMP']/@COMP_ACT_PRE");
                            if (nodTempMinSusPrem != null && nodtempCompOnly != null)
                            {
                                if (nodTempMinSusPrem.InnerText != "" && nodTempMinSusPrem.InnerText != "0" && nodtempCompOnly.InnerText != "")
                                {
                                    nodtempCompOnly.InnerText = System.Convert.ToString(System.Convert.ToInt32(nodtempCompOnly.InnerText) + System.Convert.ToInt32(nodTempMinSusPrem.InnerText));
                                    nodTempMinSusPrem.InnerText = "Included";
                                }
                            }

                        }

                    }
                    // count number of vehicle

                    //XmlNodeList nodVehiclesInInput = docpremiumInput.SelectNodes("PRIMIUM/RISK");
                    //Added by Manoj Rathore on 5 Jun 2009


                    //Start Minimum Premium XML

                    MinimumInputXML = CpyInputXML.Replace("<QUICKQUOTE>", "<QUICKQUOTE><TYPE>MINIMUMPREMIUM</TYPE>");
                    premiumXSLPath = GetProductPremiumPath(MinimumInputXML, "CYCL");
                    MinimumInputXML = "<INPUTXML>" + MinimumInputXML + "</INPUTXML>";

                    string strTemp = "<?xml version='1.0' ?> <?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?><!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple' xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'> <!ATTLIST person id ID #IMPLIED>]>";
                    finalPremiumXML = docpremiumXml.SelectSingleNode("PRIMIUM").OuterXml;
                    //finalPremiumXML = finalPremiumXML.Substring(strTemp.Length-1);
                    finalPremiumXML = finalPremiumXML.Replace("&", "");
                    QEngineDll.QuoteEngine strGetPath1 = new QEngineDll.QuoteEngine(finalPremiumXML, premiumXSLPath, strProductFactorPath);
                    string shortestPath1 = strGetPath1.GetPath();
                    string tempStr1 = strGetPath1.CalculatePremium(shortestPath1.Trim());

                    tempStr1 = tempStr1.Replace("<PRIMIUM>", "");
                    tempStr1 = tempStr1.Replace("</PRIMIUM>", "");
                    MinimumPremiumComponent = MinimumPremiumComponent + tempStr1.Replace(strAdditionalText, "");
                    riskType = "";
                    riskType = "MC";
                    MinimumPremiumComponent = "<MINIM  TYPE='" + riskType + "'>" + MinimumPremiumComponent + "</MINIM>";

                    //End  Minimum Premium XML
                    finalPremiumXML = finalPremiumXML.Replace("<PRIMIUM>", "");
                    finalPremiumXML = finalPremiumXML.Replace("</PRIMIUM>", "");
                    //finalPremiumXML="";
                    //finalPremiumXML = strAdditionalText + "<PRIMIUM>" + driverComponent + vehicleComponent + MinimumPremiumComponent + "</PRIMIUM>";
                    finalPremiumXML = strAdditionalText + "<PRIMIUM>" + finalPremiumXML + MinimumPremiumComponent + "</PRIMIUM>";


                }

                finalPremiumXML = finalPremiumXML.Replace("&", "&amp;");

                // ACCESS the premium xml for minimum premium component for BI, PD, CSL
                XmlDocument docpremiumInput = new XmlDocument();
                docpremiumInput.LoadXml(finalPremiumXML);

                XmlNode nodTempVehicleInPremium, nodTempVehicleInPremium_min, nodTempPolicyMinPrem, nodTempVehicleInActPremium, nodtempVehiclebimin, nodtempVehiclepdmin, nodtempVehiclecslmin, nodtempCompVehicle;
                //string premiumValueInPremium = "", vehicleid = "";
                int TotalPremium = 0, intPolicyPrem = 0, flgttlPre = 0, flgNrVhil = 0, cntrRsk = 0;//countOfVehicles = 0,
                double intFacturdMin = 0.00;

                XmlNodeList nodVehiclesInInput = docpremiumInput.SelectNodes("PRIMIUM/RISK[@TYPE='A' or @TYPE='E' or @TYPE='G' or @TYPE='H' or @TYPE='T' or @TYPE='X']");
                nodTempPolicyMinPrem = docpremiumInput.SelectSingleNode("PRIMIUM/MINIM/STEP[@COMPONENT_CODE='MINIMUMPRE']/@STEPPREMIUM");

                //countOfVehicles = nodVehiclesInInput.Count;
                //check if any comp only vehicle present
                foreach (XmlNode nod in nodVehiclesInInput)
                {
                    //nodtempCompVehicle = docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+count.ToString()+"']/STEP[@TS='0']/@GROUPDESC");
                    nodtempCompVehicle = nod.SelectSingleNode("STEP[@TS='0']/@GROUPDESC");
                    if (nodtempCompVehicle != null && nodtempCompVehicle.InnerText.ToUpper().Trim() == "MOTORCYCLES (COMPREHENSIVE ONLY)")
                    {
                        flgttlPre++;
                    }
                    if (nodtempCompVehicle != null && nodtempCompVehicle.InnerText.ToUpper().Trim().IndexOf("COMPREHENSIVE ONLY") < 0)
                    {
                        flgNrVhil++;
                    }
                }
                //for (int count =1; count<=countOfVehicles;count++)
                foreach (XmlNode node in nodVehiclesInInput)
                {
                    int intBiMin = 0, intPdMin = 0, intCslMin = 0,TotalActPremium = 0;// intBiActMin = 0, intPdActMin = 0, intCslActMin = 0, 
                    //vehicleid = "RISK"+count.ToString();
                    //vehicleid = "RISK"+countOfVehicles.ToString();
                    // coverage BI
                    /*nodtempCompVehicle = docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+count.ToString()+"']/STEP[@TS='0']/@GROUPDESC");
                    nodTempVehicleInPremium =	docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+count.ToString()+"']/STEP[@COMPONENT_CODE='BI']/@STEPPREMIUM");
                    nodTempVehicleInActPremium = docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+count.ToString()+"']/STEP[@COMPONENT_CODE='BI']/@COMP_ACT_PRE");
                    nodTempVehicleInPremium_min =	docpremiumInput.SelectSingleNode("PRIMIUM/MINIM/STEP[@COMPONENT_CODE='BI_MINMUM']/@STEPPREMIUM");*/

                    nodtempCompVehicle = node.SelectSingleNode("STEP[@TS='0']/@GROUPDESC");
                    nodTempVehicleInPremium = node.SelectSingleNode("STEP[@COMPONENT_CODE='BI']/@STEPPREMIUM");
                    nodTempVehicleInActPremium = node.SelectSingleNode("STEP[@COMPONENT_CODE='BI']/@COMP_ACT_PRE");
                    nodTempVehicleInPremium_min = docpremiumInput.SelectSingleNode("PRIMIUM/MINIM/STEP[@COMPONENT_CODE='BI_MINMUM']/@STEPPREMIUM");
                    nodtempVehiclebimin = docpremiumInput.SelectSingleNode("PRIMIUM/MINIM/STEP[@COMPONENT_CODE='BI_MINMUM']");
                    if (nodtempCompVehicle != null)
                    {
                        if (nodtempCompVehicle.InnerText.ToUpper().Trim() != "MOTORCYCLES (COMPREHENSIVE ONLY)")
                        {
                            if (nodTempVehicleInPremium != null)
                            {
                                if (nodTempVehicleInPremium_min != null)
                                {
                                    if (nodTempVehicleInPremium_min.InnerText != "")
                                    {
                                        bool IsNumeric = false;
                                        try
                                        {
                                            int iTest = Int32.Parse(nodTempVehicleInPremium_min.InnerText);
                                            IsNumeric = true;
                                        }
                                        catch (Exception ex)
                                        {
                                            Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                                            IsNumeric = false;
                                        }
                                        if (IsNumeric)
                                        {
                                            // If minimum prem is $1 then add it to fisrt risk Bi component 
                                            if (nodTempPolicyMinPrem != null && nodTempPolicyMinPrem.InnerText == "1" && cntrRsk == 0)
                                            {
                                                intBiMin = System.Convert.ToInt32(nodTempPolicyMinPrem.InnerText);
                                                TotalPremium = System.Convert.ToInt32(nodTempVehicleInPremium.InnerText) + System.Convert.ToInt32(nodTempPolicyMinPrem.InnerText);
                                                nodTempVehicleInPremium.InnerText = TotalPremium.ToString();
                                                if (nodTempVehicleInActPremium != null && nodTempVehicleInActPremium.InnerText != "")
                                                {
                                                    TotalActPremium = System.Convert.ToInt32(nodTempVehicleInActPremium.InnerText) + System.Convert.ToInt32(nodTempPolicyMinPrem.InnerText);
                                                    nodTempVehicleInActPremium.InnerText = TotalActPremium.ToString();
                                                }
                                            }
                                            else
                                            {
                                                intBiMin = System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText);
                                                TotalPremium = System.Convert.ToInt32(nodTempVehicleInPremium.InnerText) + System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText);
                                                nodTempVehicleInPremium.InnerText = TotalPremium.ToString();
                                                if (nodTempVehicleInActPremium != null && nodTempVehicleInActPremium.InnerText != "")
                                                {
                                                    TotalActPremium = System.Convert.ToInt32(nodTempVehicleInActPremium.InnerText) + System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText);
                                                    nodTempVehicleInActPremium.InnerText = TotalActPremium.ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            // coverage PD
                            //nodTempVehicleInPremium =	docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+count.ToString()+"']/STEP[@COMPONENT_CODE='PD']/@STEPPREMIUM");
                            //nodTempVehicleInActPremium = docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+count.ToString()+"']/STEP[@COMPONENT_CODE='PD']/@COMP_ACT_PRE");
                            nodTempVehicleInPremium = node.SelectSingleNode("STEP[@COMPONENT_CODE='PD']/@STEPPREMIUM");
                            nodTempVehicleInActPremium = node.SelectSingleNode("STEP[@COMPONENT_CODE='PD']/@COMP_ACT_PRE");
                            nodTempVehicleInPremium_min = docpremiumInput.SelectSingleNode("PRIMIUM/MINIM/STEP[@COMPONENT_CODE='PD_MINMUM']/@STEPPREMIUM");
                            nodtempVehiclepdmin = docpremiumInput.SelectSingleNode("PRIMIUM/MINIM/STEP[@COMPONENT_CODE='PD_MINMUM']");
                            if (nodTempVehicleInPremium != null)
                            {
                                if (nodTempVehicleInPremium_min != null)
                                {
                                    if (nodTempVehicleInPremium_min.InnerText != "")
                                    {
                                        bool IsNumeric = false;
                                        try
                                        {
                                            int iTest = Int32.Parse(nodTempVehicleInPremium_min.InnerText);
                                            IsNumeric = true;
                                        }
                                        catch (Exception ex)
                                        {
                                            Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                                            IsNumeric = false;
                                        }
                                        if (IsNumeric)
                                        {
                                            intPdMin = System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText);
                                            TotalPremium = System.Convert.ToInt32(nodTempVehicleInPremium.InnerText) + System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText);
                                            nodTempVehicleInPremium.InnerText = TotalPremium.ToString();
                                            if (nodTempVehicleInActPremium != null && nodTempVehicleInActPremium.InnerText != "")
                                            {
                                                TotalActPremium = System.Convert.ToInt32(nodTempVehicleInActPremium.InnerText) + System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText);
                                                nodTempVehicleInActPremium.InnerText = TotalActPremium.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                            // coverage csl

                            //nodTempVehicleInPremium =	docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+count.ToString()+"']/STEP[@COMPONENT_CODE='CSL']/@STEPPREMIUM");
                            //nodTempVehicleInActPremium = docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+count.ToString()+"']/STEP[@COMPONENT_CODE='CSL']/@COMP_ACT_PRE");
                            nodTempVehicleInPremium = node.SelectSingleNode("STEP[@COMPONENT_CODE='CSL']/@STEPPREMIUM");
                            nodTempVehicleInActPremium = node.SelectSingleNode("STEP[@COMPONENT_CODE='CSL']/@COMP_ACT_PRE");
                            nodTempVehicleInPremium_min = docpremiumInput.SelectSingleNode("PRIMIUM/MINIM/STEP[@COMPONENT_CODE='CSL_MINMUM']/@STEPPREMIUM");
                            nodtempVehiclecslmin = docpremiumInput.SelectSingleNode("PRIMIUM/MINIM/STEP[@COMPONENT_CODE='CSL_MINMUM']");
                            if (nodTempVehicleInPremium != null)
                            {
                                if (nodTempVehicleInPremium_min != null)
                                {
                                    if (nodTempVehicleInPremium_min.InnerText != "")
                                    {
                                        bool IsNumeric = false;
                                        try
                                        {
                                            int iTest = Int32.Parse(nodTempVehicleInPremium_min.InnerText);
                                            IsNumeric = true;
                                        }
                                        catch (Exception ex)
                                        {
                                            Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                                            IsNumeric = false;
                                        }
                                        if (IsNumeric)
                                        {
                                            intCslMin = System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText);
                                            TotalPremium = System.Convert.ToInt32(nodTempVehicleInPremium.InnerText) + System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText);
                                            nodTempVehicleInPremium.InnerText = TotalPremium.ToString();
                                            if (nodTempVehicleInActPremium != null && nodTempVehicleInActPremium.InnerText != "")
                                            {
                                                TotalActPremium = System.Convert.ToInt32(nodTempVehicleInActPremium.InnerText) + System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText);
                                                nodTempVehicleInActPremium.InnerText = TotalActPremium.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                            //To add Minimum Adjustment Amount in Total Motorcycle Premium - Asfa Praveen(05/July/2007)

                            //nodTempVehicleInPremium =	docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+count.ToString()+"']/STEP[@COMPONENT_CODE='SUMTOTAL']/@STEPPREMIUM");
                            //nodTempVehicleInActPremium = docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+count.ToString()+"']/STEP[@COMPONENT_CODE='SUMTOTAL']/@COMP_ACT_PRE");
                            nodTempVehicleInPremium = node.SelectSingleNode("STEP[@COMPONENT_CODE='SUMTOTAL']/@STEPPREMIUM");
                            nodTempVehicleInActPremium = node.SelectSingleNode("STEP[@COMPONENT_CODE='SUMTOTAL']/@COMP_ACT_PRE");
                            nodTempVehicleInPremium_min = docpremiumInput.SelectSingleNode("PRIMIUM/MINIM/STEP[@COMPONENT_CODE='MINIMUMPRE']/@STEPPREMIUM");

                            TotalPremium = System.Convert.ToInt32(nodTempVehicleInPremium.InnerText) + intBiMin + intPdMin + intCslMin;
                            nodTempVehicleInPremium.InnerText = TotalPremium.ToString();
                            intPolicyPrem += TotalPremium;
                            if (nodTempVehicleInActPremium != null && nodTempVehicleInActPremium.InnerText != "")
                            {
                                TotalActPremium = System.Convert.ToInt32(nodTempVehicleInActPremium.InnerText) + intBiMin + intPdMin + intCslMin;
                                nodTempVehicleInActPremium.InnerText = TotalActPremium.ToString();
                            }
                        }
                    }
                    // prm adjustment for comp only vehicle
                    if (flgttlPre != 0 && nodtempCompVehicle.InnerText.ToUpper().Trim() == "MOTORCYCLES (COMPREHENSIVE ONLY)")
                    {
                        //nodTempVehicleInPremium =	docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+count.ToString()+"']/STEP[@COMPONENT_CODE='SUMTOTAL']/@STEPPREMIUM");
                        //nodTempVehicleInActPremium = docpremiumInput.SelectSingleNode("PRIMIUM/RISK[@ID='"+count.ToString()+"']/STEP[@COMPONENT_CODE='SUMTOTAL']/@COMP_ACT_PRE");
                        nodTempVehicleInPremium = node.SelectSingleNode("STEP[@COMPONENT_CODE='SUMTOTAL']/@STEPPREMIUM");
                        nodTempVehicleInActPremium = node.SelectSingleNode("STEP[@COMPONENT_CODE='SUMTOTAL']/@COMP_ACT_PRE");
                        nodTempVehicleInPremium_min = docpremiumInput.SelectSingleNode("PRIMIUM/MINIM/STEP[@COMPONENT_CODE='MINIMUMPRE']/@STEPPREMIUM");
                        if (nodTempVehicleInPremium_min.InnerText != "" && nodTempVehicleInPremium_min.InnerText != "0")
                        {
                            intFacturdMin = System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText) / flgttlPre;
                            intFacturdMin = System.Convert.ToDouble(nodTempVehicleInPremium_min.InnerText) - (intFacturdMin * double.Parse(flgttlPre.ToString()));
                        }
                        if (nodTempVehicleInPremium_min != null && nodTempVehicleInPremium_min.InnerText != "")
                        {
                            if (nodTempVehicleInPremium != null && nodTempVehicleInPremium.InnerText != "")
                            {
                                if (flgNrVhil == 0)
                                {
                                    if (cntrRsk == 0)
                                        TotalPremium = System.Convert.ToInt32(nodTempVehicleInPremium.InnerText) + ((System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText) / flgttlPre)) + System.Convert.ToInt32(intFacturdMin);
                                    else
                                        TotalPremium = System.Convert.ToInt32(nodTempVehicleInPremium.InnerText) + ((System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText) / flgttlPre));
                                    nodTempVehicleInPremium.InnerText = TotalPremium.ToString();
                                }
                                else
                                    TotalPremium = System.Convert.ToInt32(nodTempVehicleInPremium.InnerText);
                                // if only sus comp vehicle in policy then add it to comp prem
                                if (flgttlPre != 0 && flgNrVhil == 0)
                                {
                                    nodTempVehicleInPremium = node.SelectSingleNode("STEP[@COMPONENT_CODE='COMP']/@STEPPREMIUM");
                                    if (nodTempVehicleInPremium != null && nodTempVehicleInPremium.InnerText != "")
                                    {
                                        int intCmpPrm = 0;
                                        if (cntrRsk == 0)
                                            intCmpPrm = Convert.ToInt32(nodTempVehicleInPremium.InnerText) + (System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText) / flgttlPre) + System.Convert.ToInt32(intFacturdMin);
                                        else
                                            intCmpPrm = Convert.ToInt32(nodTempVehicleInPremium.InnerText) + (System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText) / flgttlPre);
                                        nodTempVehicleInPremium.InnerText = intCmpPrm.ToString();
                                    }

                                }
                            }
                            if (nodTempVehicleInActPremium != null && nodTempVehicleInActPremium.InnerText != "")
                            {
                                if (flgNrVhil == 0)
                                {
                                    if (cntrRsk == 0)
                                        TotalPremium = System.Convert.ToInt32(nodTempVehicleInActPremium.InnerText) + (System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText) / flgttlPre) + System.Convert.ToInt32(intFacturdMin);
                                    else
                                        TotalPremium = System.Convert.ToInt32(nodTempVehicleInActPremium.InnerText) + (System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText) / flgttlPre);
                                    nodTempVehicleInActPremium.InnerText = TotalPremium.ToString();
                                }
                                else
                                    TotalPremium = System.Convert.ToInt32(nodTempVehicleInActPremium.InnerText);
                                nodTempVehicleInActPremium.InnerText = TotalPremium.ToString();
                                // if only sus comp vehicle in policy then add it to comp prem
                                if (flgttlPre != 0 && flgNrVhil == 0)
                                {
                                    nodTempVehicleInActPremium = node.SelectSingleNode("STEP[@COMPONENT_CODE='COMP']/@COMP_ACT_PRE");
                                    if (nodTempVehicleInActPremium != null && nodTempVehicleInActPremium.InnerText != "")
                                    {
                                        int intCmpActPrm = 0;
                                        if (cntrRsk == 0)
                                            intCmpActPrm = System.Convert.ToInt32(nodTempVehicleInActPremium.InnerText) + (System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText) / flgttlPre) + System.Convert.ToInt32(intFacturdMin);
                                        else
                                            intCmpActPrm = System.Convert.ToInt32(nodTempVehicleInActPremium.InnerText) + (System.Convert.ToInt32(nodTempVehicleInPremium_min.InnerText) / flgttlPre);
                                        nodTempVehicleInActPremium.InnerText = intCmpActPrm.ToString();
                                    }
                                }
                            }
                        }
                        intPolicyPrem += TotalPremium;

                    }
                    cntrRsk++;
                }
                nodTempVehicleInPremium = docpremiumInput.SelectSingleNode("PRIMIUM/MINIM/STEP[@COMPONENT_CODE='FINAL_PREMIUM']/@STEPPREMIUM");
                if (nodTempVehicleInPremium != null)
                    nodTempVehicleInPremium.InnerText = intPolicyPrem.ToString();

                return docpremiumInput.InnerXml; ;
                # endregion
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
            #endregion
        }



        public string GetPremiumComponent(string inputXML, string premiumXSLPath)
        {
            try
            {

                QEngineDll.QuoteEngine strGetPath = new QEngineDll.QuoteEngine(inputXML, premiumXSLPath, "D:/Projects/EBX-DV25/source code/cms/cmsweb/XSL/Quote/ho/ho-3/Input.xsl");
                string shortestPath = strGetPath.GetPath();
                string premiumXML = strGetPath.CalculatePremium(shortestPath.Trim());
                return premiumXML;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }

        public string GetPremiumComponent(string inputXML, string premiumXSLPath, string productFactorMasterXMLPath)
        {
            try
            {

                QEngineDll.QuoteEngine strGetPath = new QEngineDll.QuoteEngine(inputXML, premiumXSLPath, productFactorMasterXMLPath);
                string shortestPath = strGetPath.GetPath();
                string premiumXML = strGetPath.CalculatePremium(shortestPath.Trim());
                return premiumXML;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }



        #region METHODS ADDED FOR CAPITAL RATER COMPARISON

        /// <summary>
        /// Accepts  the premium XML, acord input xml from Capital Rater
        /// Returns an acord input xml with premium nodes.
        /// </summary>		
        /// <param name="acordInputXML">inputXML that has to be returned with added nodes </param>
        /// <param name="premiumXML">premium xml from which premium values have to be picked for inserting into acordXML </param>
        /// <param name="lobid">lobid for lob specific calculations</param>
        /// <returns>string</returns>
        public string AttachPremiumToAcordInput(string acordInputXML, string premiumXML, string lobId, string InsuranceScoreId)
        {
            try
            {
                string finalAcordInput = "";

                if (lobId == LOB_HOME)
                    finalAcordInput = AttachPremiumToAcordInputForHome(acordInputXML, premiumXML);
                else if (lobId == LOB_PRIVATE_PASSENGER)
                    finalAcordInput = AttachPremiumToAcordInputForAuto(acordInputXML, premiumXML, InsuranceScoreId);
                else if (lobId == LOB_MOTORCYCLE)
                    finalAcordInput = AttachPremiumToAcordInputForMotorCycle(acordInputXML, premiumXML);
                else if (lobId == LOB_RENTAL_DWELLING)
                    finalAcordInput = AttachPremiumToAcordInputForRentalDwelling(acordInputXML, premiumXML);
                else if (lobId == LOB_UMBRELLA)
                    finalAcordInput = AttachPremiumToAcordInputForUmbrella(acordInputXML, premiumXML);
                else if (lobId == LOB_WATERCRAFT)
                    finalAcordInput = AttachPremiumToAcordInputForWatercraft(acordInputXML, premiumXML);

                return finalAcordInput;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        /// <summary>
        /// Accepts  the AcordOutput XML, 
        /// Returns an AcordOutput xml with payment plan nodes.
        /// </summary>		
        /// <param name="AcordOutputXML">output xml that has to be returned with added nodes </param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns>string</returns>
        public string AttachPaymentPlanToAcordOutputXML(string AcordOutputXML, string PaymentPlanXML, string lobId)
        {
            try
            {
                string finalAcordOutput = "";

                if (lobId == LOB_HOME)
                    finalAcordOutput = AttachPaymentPlanToAcordOutputForHome(AcordOutputXML, PaymentPlanXML);
                else if (lobId == LOB_PRIVATE_PASSENGER)
                    finalAcordOutput = AttachPaymentPlanToAcordOutputForAuto(AcordOutputXML, PaymentPlanXML);
                else if (lobId == LOB_MOTORCYCLE)
                    finalAcordOutput = AttachPaymentPlanToAcordOutputForMotorCycle(AcordOutputXML, PaymentPlanXML);
                else if (lobId == LOB_RENTAL_DWELLING)
                    finalAcordOutput = AttachPaymentPlanToAcordOutputForRentalDwelling(AcordOutputXML, PaymentPlanXML);
                else if (lobId == LOB_UMBRELLA)
                    finalAcordOutput = AttachPaymentPlanToAcordOutputForUmbrella(AcordOutputXML, PaymentPlanXML);
                else if (lobId == LOB_WATERCRAFT)
                    finalAcordOutput = AttachPaymentPlanToAcordOutputForWatercraft(AcordOutputXML, PaymentPlanXML);
                return finalAcordOutput;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }


        /// <summary>
        /// Accepts  the premium XML, acord input xml from Capital Rater for Auto
        /// Returns an acord input xml with premium nodes.
        /// </summary>		
        /// <param name="acordInputXML">inputXML that has to be returned with added nodes </param>
        /// <param name="premiumXML">premium xml from which premium values have to be picked for inserting into acordXML </param>
        /// <returns>string</returns>
        public string AttachPremiumToAcordInputForAuto(string acordInputXML, string premiumXML, string strInsuranceScoreId)
        {
            try
            {
                string finalAcordInput = acordInputXML;

                //load the acordinput xml
                acordInputXML = acordInputXML.Replace("&gt;", ">");
                acordInputXML = acordInputXML.Replace("&lt;", "<");
                acordInputXML = acordInputXML.Replace("\"", "'");
                acordInputXML = acordInputXML.Replace("\r", "");
                acordInputXML = acordInputXML.Replace("\n", "");
                acordInputXML = acordInputXML.Replace("\t", "");

                XmlDocument docAcordInput = new XmlDocument();
                docAcordInput.LoadXml(acordInputXML);


                //load the premium xml 
                XmlDocument docPremiumXML = new XmlDocument();
                docPremiumXML.LoadXml(premiumXML);


                #region AUTO COVERAGES
                /* pick each steppremium attribute node of premium xml and insert a new 'premium' node into the resp coverage node in the acord xml
				* 1. read node from premium
				* 2. find node from acordinput 
				* 3. append node in acordinput
				*/

                XmlNode nodTempCovgInPremium, nodTempCovgInAcord, nodTempMccaInAcord;
                XmlElement nodPremiumInAcord, nodTempAmtInAcord, nodTempEffDate;
                XmlNodeList lstCrditSur;
                string premiumValueInPremium = "", persVehId = "", terr = "", sym = "";
                int countOfVehicles = 0, TotalPolicyPremium = 0;

                //count the vehicle nodes. run loop for each node.
                XmlNodeList nodVehiclesInAcordInput = docAcordInput.SelectNodes("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh");
                countOfVehicles = nodVehiclesInAcordInput != null ? nodVehiclesInAcordInput.Count : 0;


                for (int count = 1; count <= countOfVehicles; count++)
                {
                    int MCCAPrm = 0;
                    persVehId = "PersVeh" + count.ToString();
                    //Residual Bodily Injury Liability
                    //BI  = covg code (BI)				
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='BI']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }

                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='BI']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);

                    }

                    //Residual Property Damage Liability
                    //PD= covg code(PD)

                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='PD']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='PD']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }


                    //Personal Injury Protection 
                    //PIP =covg code(PIP)
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='PIP']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='PIP']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }
                    //Uninsured Motorists Property Damage				
                    // UIMPD= covg code(UMPD)

                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='UIMPD']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='UMPD']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }
                    //Mini-Tort PD Liability
                    // M_TRT=  covg code(TORT)
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='M_TRT']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='LPD']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }

                    //Road Service 
                    // RD_SRVC=covg code(TL)
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='RD_SRVC']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='TL']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }

                    //Rental Reimbursement
                    // RNT_RMBRS=covg code(RREIM)
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='RNT_RMBRS']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='RREIM']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }
                    //Loan/Lease Gap Coverage
                    // LN_LSE= covg code(LOAN)
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='LN_LSE']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='LOAN']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }
                    //Loan/Lease Gap Coverage
                    // LN_LSE= covg code(LEASE)
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='LN_LSE']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='LEASE']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }
                    //Sound Reproducing Tapes
                    // SND_RPR= covg code(SORPE)
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='SND_RPR']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='SORPE']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }
                    //Sound Receiving And Transmitting Equipment
                    // SND_RCV= covg code(SORCV)
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='SND_RCV']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='SORCV']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }
                    //Extra Equipment – Comprehensive
                    // XTR_COMP= covg code(CCOMP)
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='XTR_COMP']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='CCOMP']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }
                    //Extra Equipment -  Collision 
                    // XTR_COLL=  covg code(CCOL)
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='XTR_COLL']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='CCOL']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }
                    //Property Protection Insurance
                    //PPI= covg code(PPI)


                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='PPI']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='PPI']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }
                    //Damage to Your Auto – Comprehensive
                    //COMP= covg code(COMP)

                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='COMP']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='COMP']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }
                    //Medical Payments
                    // MED_PMT= covg code(MEDPM)

                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='MED_PMT']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='MEDPM']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }
                    //Damage to Your Auto – Collision
                    //COLL= covg code(COLL)

                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='COLL']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='COLL']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }

                    //XTR_COMP= covg code(CCOMP)
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='XTR_COMP']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='CCOMP']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }

                    //Residual Liability (BI and PD)
                    //CSL= covg code (CSL)
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='CSL']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='CSL']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }

                    //Michigan Statutory Assessments
                    // MCCAFEE= covg code (MCCA)
                    lstCrditSur = docPremiumXML.SelectNodes("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='MCCAFEE']");
                    if (lstCrditSur != null)
                    {
                        foreach (XmlNode tmpnod in lstCrditSur)
                        {
                            if (tmpnod.Attributes["STEPPREMIUM"].Value.ToString() != "")
                            {
                                bool IsNumeric = false;
                                try
                                {
                                    int iTest = Int32.Parse(tmpnod.Attributes["STEPPREMIUM"].Value.ToString());
                                    IsNumeric = true;
                                }
                                catch (Exception ex)
                                {
                                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                                    IsNumeric = false;
                                }
                                if (IsNumeric)
                                {
                                    MCCAPrm += int.Parse(tmpnod.Attributes["STEPPREMIUM"].Value.ToString());
                                }
                            }
                        }
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']");
                    if (nodTempCovgInAcord != null)
                    {
                        // create element for coverage code mcca and update that node with MCCAFEE
                        nodPremiumInAcord = docAcordInput.CreateElement("Coverage");
                        nodTempAmtInAcord = docAcordInput.CreateElement("CoverageCd");
                        nodTempAmtInAcord.InnerText = "MCCA";
                        nodTempCovgInAcord.AppendChild(nodPremiumInAcord);
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);

                        nodTempMccaInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = MCCAPrm.ToString();
                        nodTempMccaInAcord.AppendChild(nodTempAmtInAcord);
                        nodPremiumInAcord.InsertAfter(nodTempMccaInAcord, nodPremiumInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodPremiumInAcord.InsertAfter(nodTempEffDate, nodPremiumInAcord.LastChild);
                    }
                    //Uninsured Motorists
                    //UM= covg code (UM)

                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='UM']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='UM']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }
                    //UMCSL= covg code (UMCSL)

                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='UM']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='UMCSL']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }
                    //UIMCSL= covg code (UIMCSL)

                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='UIM']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='UIMCSL']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }

                    //Underinsured Motorists
                    //UIM = covg code(UMUIM)
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='UIM']");
                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']/Coverage[CoverageCd='UIM']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }

                    //Sum Total

                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='SUMTOTAL']");

                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                        if (premiumValueInPremium != "")
                        {
                            TotalPolicyPremium += int.Parse(premiumValueInPremium);
                        }
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("SUMTOTAL");
                        nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                        nodTempAmtInAcord.InnerText = premiumValueInPremium;
                        nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                        nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                        nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                        nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                        nodTempCovgInAcord.InsertAfter(nodTempEffDate, nodTempCovgInAcord.LastChild);
                    }


                    //string filePath =@"D:\test.xml";			 

                    //FileStream fsxml = new FileStream(filePath,FileMode.OpenOrCreate,FileAccess.Write,FileShare.ReadWrite);

                    // XML Document Saved
                    //docAcordInput.Save(fsxml);

                    // vehicle class update 
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='SYM_CLS']");

                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = "";
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPDESC"].Value.ToString();
                        if (premiumValueInPremium.Length > 0)
                        {
                            if (premiumValueInPremium.IndexOf("Class") > 0)
                            {
                                premiumValueInPremium = premiumValueInPremium.Substring(premiumValueInPremium.IndexOf("Class"));
                                if (premiumValueInPremium.IndexOf("Class") >= 0)
                                    premiumValueInPremium = premiumValueInPremium.Replace("Class", "").Trim();
                            }
                            else
                                premiumValueInPremium = "";
                        }

                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("RateClassCd");
                        nodPremiumInAcord.InnerText = premiumValueInPremium;
                        nodTempCovgInAcord.AppendChild(nodPremiumInAcord);
                    }
                    // vehicle GARAGED LOCATION 
                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='GRG_LOC']");

                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPDESC"].Value.ToString();
                        if (premiumValueInPremium != "")
                        {
                            if (premiumValueInPremium.ToUpper().IndexOf("TERRITORY :") > 0)
                                terr = premiumValueInPremium.Substring(premiumValueInPremium.ToUpper().LastIndexOf("TERRITORY :"), premiumValueInPremium.Length - premiumValueInPremium.ToUpper().LastIndexOf("TERRITORY :"));
                            if (terr.IndexOf("TERRITORY :") >= 0)
                                terr = terr.ToUpper().Replace("TERRITORY :", "").Trim();
                        }
                    }

                    // Vehicle Symbol 

                    nodTempCovgInPremium = docPremiumXML.SelectSingleNode("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_CODE='SYM_CLS']");

                    if (nodTempCovgInPremium != null)
                    {
                        premiumValueInPremium = nodTempCovgInPremium.Attributes["STEPDESC"].Value.ToString();
                        if (premiumValueInPremium != "")
                        {
                            if (premiumValueInPremium.ToUpper().IndexOf("SYMBOL") >= 0 && premiumValueInPremium.IndexOf(",") >= 0)
                                sym = premiumValueInPremium.ToUpper().Substring(premiumValueInPremium.ToUpper().IndexOf("SYMBOL"), premiumValueInPremium.IndexOf(","));
                            if (sym.ToUpper().IndexOf("SYMBOL:") >= 0)
                                sym = sym.ToUpper().Replace("SYMBOL:", "").Trim();
                        }
                    }
                    nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']");
                    if (nodTempCovgInAcord != null)
                    {
                        nodPremiumInAcord = docAcordInput.CreateElement("VehSymbolCd");
                        nodPremiumInAcord.InnerText = sym;
                        nodTempCovgInAcord.AppendChild(nodPremiumInAcord);

                    }

                    // Credit and Surcharge
                    lstCrditSur = docPremiumXML.SelectNodes("PRIMIUM/RISK[@ID='" + count.ToString() + "']/STEP[@COMPONENT_TYPE='S' or @COMPONENT_TYPE='D']");

                    foreach (XmlNode node in lstCrditSur)
                    {
                        string txtAcordCode = "";
                        string txtCred = node.SelectSingleNode("@COMP_REMARKS").InnerText;
                        /*	if(txtCred !="")
                            {
                                if(txtCred.ToUpper().IndexOf("DISCOUNT")>=0 && txtCred.IndexOf("-")>=0)
                                {

                                    txtCred=txtCred.Substring(txtCred.ToUpper().IndexOf("DISCOUNT"),txtCred.LastIndexOf("-")-1);
                                }
                                else if(txtCred.ToUpper().IndexOf("SURCHARGE")>=0 && txtCred.IndexOf("-")>=0)
                                {
                                    txtCred=txtCred.Substring(txtCred.ToUpper().IndexOf("SURCHARGE"),txtCred.LastIndexOf("-")-1);
                                }
                                else
                                    txtCred=txtCred;
                            }*/
                        string txtPrem = node.SelectSingleNode("@STEPPREMIUM").InnerText;
                        string txtCode = node.SelectSingleNode("@COMPONENT_CODE").InnerText;
                        string txtExtAd = node.SelectSingleNode("@COM_EXT_AD").InnerText;
                        switch (txtCode)
                        {
                            case "D_WRK_LSS":
                                txtAcordCode = "";
                                break;
                            case "D_ST_BLT":
                                txtAcordCode = "";
                                break;
                            case "D_AR_BG":
                                txtAcordCode = "";
                                break;
                            case "D_ANT_LCK_BRK":
                                txtAcordCode = "ABS";
                                break;
                            case "D_MLT_CR":
                                txtAcordCode = "MCAR";
                                break;
                            case "D_MLT_POL":
                                txtAcordCode = "";
                                break;
                            case "D_TRLBLZR":
                                txtAcordCode = "";
                                break;
                            case "D_INS_SCR":
                                txtAcordCode = "INSCR";
                                break;
                            case "D_PRM_DVR":
                                txtAcordCode = "";
                                break;
                            case "D_GD_STDNT":
                                txtAcordCode = "GSD";
                                break;
                            case "S_BUSI_CHAR":
                                txtAcordCode = "";
                                break;
                            case "S_ACCI_VIOL":
                                txtAcordCode = "";
                                break;
                            default:
                                txtAcordCode = "";
                                break;
                        }
                        nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='" + persVehId + "']");
                        if (txtPrem != "" && txtPrem != "0" && txtPrem != "1.00" && txtPrem != "0.00" && txtPrem != "1")
                        {
                            if (nodTempCovgInAcord != null)
                            {
                                nodPremiumInAcord = docAcordInput.CreateElement("CreditOrSurcharge");
                                nodTempAmtInAcord = docAcordInput.CreateElement("CreditSurchargeCd");
                                nodTempAmtInAcord.InnerText = txtAcordCode;
                                nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                                nodTempCovgInAcord.InsertAfter(nodPremiumInAcord, nodTempCovgInAcord.LastChild);

                                nodTempAmtInAcord = docAcordInput.CreateElement("NumericValue");
                                nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                                nodTempEffDate = docAcordInput.CreateElement("FormatPct");
                                nodTempEffDate.InnerText = txtExtAd.Trim();
                                nodTempAmtInAcord.AppendChild(nodTempEffDate);

                                nodTempAmtInAcord = docAcordInput.CreateElement("SecondaryCd");
                                nodTempAmtInAcord.InnerText = txtCode.Trim();
                                nodPremiumInAcord.AppendChild(nodTempAmtInAcord);

                                nodTempAmtInAcord = docAcordInput.CreateElement("com.brics_CreditDesc");
                                nodTempAmtInAcord.InnerText = txtCred.Trim();
                                nodPremiumInAcord.AppendChild(nodTempAmtInAcord);
                            }
                        }
                    }
                }
                // Total Policy Premium
                nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy");
                if (nodTempCovgInAcord != null)
                {
                    nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                    nodPremiumInAcord.InnerText = TotalPolicyPremium.ToString();
                    nodTempCovgInAcord.AppendChild(nodPremiumInAcord);
                }


                //Territory Code
                nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/Location/Addr");
                if (nodTempCovgInAcord != null)
                {
                    nodPremiumInAcord = docAcordInput.CreateElement("TerritoryCd");
                    nodPremiumInAcord.InnerText = terr.ToString();
                    nodTempCovgInAcord.AppendChild(nodPremiumInAcord);
                }

                //Insurance Score Id, Insurance Score, Insurance Score date
                string[] arrInsuRq = new string[0];
                arrInsuRq = strInsuranceScoreId.Split('^');

                if (arrInsuRq.Length > 1)
                {
                    nodTempCovgInPremium = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/CreditScoreInfo/CnfNO");
                    if (nodTempCovgInPremium != null)
                    {
                        //if(nodTempCovgInPremium.InnerText=="")
                        //{
                        nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/CreditScoreInfo");
                        nodTempCovgInAcord.RemoveChild(nodTempCovgInPremium);
                        if (nodTempCovgInAcord != null)
                        {
                            nodPremiumInAcord = docAcordInput.CreateElement("CnfNO");
                            nodPremiumInAcord.InnerText = arrInsuRq[0].ToString();
                            nodTempCovgInAcord.AppendChild(nodPremiumInAcord);
                        }
                        //}
                    }
                    else
                    {
                        nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/CreditScoreInfo");
                        if (nodTempCovgInAcord != null)
                        {
                            nodPremiumInAcord = docAcordInput.CreateElement("CnfNO");
                            nodPremiumInAcord.InnerText = arrInsuRq[0].ToString();
                            nodTempCovgInAcord.AppendChild(nodPremiumInAcord);
                        }
                    }
                    if (arrInsuRq[1].ToString() != "")
                    {
                        nodTempCovgInPremium = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/CreditScoreInfo/CreditScore");
                        if (nodTempCovgInPremium != null)
                        {
                            if (arrInsuRq[1].ToString() == "-2")
                                nodTempCovgInPremium.InnerText = "No Hit";
                            else
                                nodTempCovgInPremium.InnerText = arrInsuRq[1].ToString();
                        }
                        else
                        {
                            nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/CreditScoreInfo");
                            nodPremiumInAcord = docAcordInput.CreateElement("CreditScoreDt");
                            if (arrInsuRq[1].ToString() == "-2")
                                nodPremiumInAcord.InnerText = "No Hit";
                            else
                                nodPremiumInAcord.InnerText = arrInsuRq[1].ToString();
                            nodTempCovgInAcord.AppendChild(nodPremiumInAcord);
                        }
                    }
                    if (arrInsuRq[2].ToString() != "")
                    {
                        nodTempCovgInPremium = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/CreditScoreInfo/CreditScoreDt");
                        if (nodTempCovgInPremium != null)
                            nodTempCovgInPremium.InnerText = arrInsuRq[2].ToString();
                        else
                        {
                            nodTempCovgInAcord = docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/CreditScoreInfo");
                            nodPremiumInAcord = docAcordInput.CreateElement("CreditScoreDt");
                            nodPremiumInAcord.InnerText = arrInsuRq[2].ToString();
                            nodTempCovgInAcord.AppendChild(nodPremiumInAcord);
                        }
                    }
                }
                finalAcordInput = docAcordInput.OuterXml;
                #endregion

                return finalAcordInput;

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        public string AttachPremiumToAcordInputForHome(string acordInputXML, string premiumXML)
        {
            try
            {
                //DWELL= covg code( COV_A)
                //


                return "";
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        public string AttachPremiumToAcordInputForMotorCycle(string acordInputXML, string premiumXML)
        {
            try
            {
                return "";
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        public string AttachPremiumToAcordInputForRentalDwelling(string acordInputXML, string premiumXML)
        {
            try
            {

                //string finalAcordInput=acordInputXML;

                //load the acordinput xml
                /*acordInputXML=acordInputXML.Replace("&gt;",">");
                acordInputXML=acordInputXML.Replace("&lt;","<");
                acordInputXML=acordInputXML.Replace("\"","'");
                acordInputXML=acordInputXML.Replace("\r","");
                acordInputXML=acordInputXML.Replace("\n","");
                acordInputXML=acordInputXML.Replace("\t","");

                XmlDocument docAcordInput = new XmlDocument();
                docAcordInput.LoadXml(acordInputXML);


                //load the premium xml 
                XmlDocument docPremiumXML = new XmlDocument();
                docPremiumXML.LoadXml(premiumXML);*/


                //#region Rental COVERAGES
                /* pick each steppremium attribute node of premium xml and insert a new 'premium' node into the resp coverage node in the acord xml
                * 1. read node from premium
                * 2. find node from acordinput 
                * 3. append node in acordinput
                */

                /*XmlNode nodTempCovgInPremium, nodTempCovgInAcord;
                XmlElement nodPremiumInAcord,nodTempAmtInAcord, nodTempEffDate;
                string premiumValueInPremium="";*/


                //DWELL  = covg code (COV_A)				
                /*nodTempCovgInPremium =	docPremiumXML.SelectSingleNode("PRIMIUM/RISK/STEP[@COMPONENT_CODE='COV_A']");
                if (nodTempCovgInPremium != null)
                {
                    premiumValueInPremium=nodTempCovgInPremium.Attributes["STEPPREMIUM"].Value.ToString();
                }
							
                nodTempCovgInAcord			=	docAcordInput.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh[@id='PersVeh1']/Coverage[CoverageCd='BI']");
                if (nodTempCovgInAcord != null)
                {
                    nodPremiumInAcord = docAcordInput.CreateElement("CurrentTermAmt");
                    nodTempAmtInAcord = docAcordInput.CreateElement("Amt");
                    nodTempAmtInAcord.InnerText = premiumValueInPremium;					
                    nodPremiumInAcord.AppendChild(nodTempAmtInAcord);					
                    nodTempCovgInAcord.InsertAfter(nodPremiumInAcord,nodTempCovgInAcord.LastChild);					

                    nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                    nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                    nodTempCovgInAcord.InsertAfter(nodTempEffDate,nodTempCovgInAcord.LastChild);						
				 
                }*/
                return "";
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }


        public string AttachPremiumToAcordInputForUmbrella(string acordInputXML, string premiumXML)
        {
            try
            {
                return "";
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        public string AttachPremiumToAcordInputForWatercraft(string acordInputXML, string premiumXML)
        {
            try
            {
                return "";
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        /// <summary>
        /// Accepts  the acordOutPut XML Auto
        /// Returns an acord OutPut xml with Payment Plan Nodes.
        /// </summary>		
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns>string</returns>
        public string AttachPaymentPlanToAcordOutputForHome(string acordOutPutXML, string PaymentPlanXML)
        {
            try
            {
                return "";
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        public string AttachPaymentPlanToAcordOutputForAuto(string acordOutPutXML, string PaymentPlanXML)
        {
            try
            {
                // node will be in following form 
                /*<PersPolicy>
                <CurrentTermAmt>288</CurrentTermAmt>
                <PaymentOption>
                    <PaymentPlanCd>FL</PaymentPlanCd>
                    <DepositAmt>
                        <Amt>288.00</Amt>
                    </DepositAmt>
                    <InstallmentFeeAmt>
                        <Amt>0.00</Amt>
                    </InstallmentFeeAmt>
                    <NumPayments>1</NumPayments>
                    <Description>Paid in full</Description>
                    <com.hmic_PaymentPlan>Full Pay</com.hmic_PaymentPlan>
                    <com.hmic_Installment>0.00</com.hmic_Installment>
                    <com.hmic_TotalOfPayments>288.00</com.hmic_TotalOfPayments>
                </PaymentOption>*/
                #region AUTO PAYMENT PLAN
                string finalAcordOutPut = acordOutPutXML;
                XmlDocument docAcordOutputXML = new XmlDocument();
                docAcordOutputXML.LoadXml(finalAcordOutPut);

                XmlDocument docPaymentXML = new XmlDocument();
                docPaymentXML.LoadXml(PaymentPlanXML);
                /* pick each steppremium attribute node of premium xml and insert a new 'premium' node into the resp coverage node in the acord xml
                * 1. read node from perspolicy
                * 2. update perspolicy node with payment plan 
                */
                XmlNode nodTempPolicy;//, nodTempPymentOption;
                XmlElement nodPymentOptionInAcord;//, nodPymentPlanCD, nodDepositAmt, nodAmt, nodNumPayments, nodDescription, nodnodFirstPaymentDueDt, nodTempInstalmntFee,
                  //  nodTempNumPayments, nodbrics_Installment, nodMethodPaymentCd, nodbrics_TotalOfPayments, nodFirstPaymentDueDt;
                //string premiumValueInPremium = "", persVehId = "";
                //int countOfVehicles = 0;

                // Parse Policy node
                nodTempPolicy = docAcordOutputXML.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy");
                if (nodTempPolicy != null)
                {
                    if (docPaymentXML.SelectSingleNode("brics_PaymentOption") != null)
                    {
                        nodPymentOptionInAcord = docAcordOutputXML.CreateElement("brics_PaymentOption");
                        nodPymentOptionInAcord.InnerText = docPaymentXML.SelectSingleNode("brics_PaymentOption").InnerXml;
                        nodTempPolicy.InsertAfter(nodPymentOptionInAcord, nodTempPolicy.LastChild);
                    }
                    //					foreach(XmlNode node in docPaymentXML.SelectNodes("brics_PaymentOption/PaymentOption"))
                    //					{
                    //						nodPymentOptionInAcord = docAcordOutputXML.CreateElement("PaymentOption");
                    //						nodTempPolicy.AppendChild(nod);
                    //					}
                    /*
                    nodPymentOptionInAcord = docAcordOutputXML.CreateElement("PaymentOption");
                    nodPymentPlanCD = docAcordOutputXML.CreateElement("PaymentPlanCd");
                    nodDepositAmt = docAcordOutputXML.CreateElement("DepositAmt");
                    nodAmt = docAcordOutputXML.CreateElement("Amt");
                    nodTempInstalmntFee = docAcordOutputXML.CreateElement("InstallmentFeeAmt");
                    nodNumPayments = docAcordOutputXML.CreateElement("NumPayments");
                    nodDescription = docAcordOutputXML.CreateElement("Description");
                    nodFirstPaymentDueDt = docAcordOutputXML.CreateElement("FirstPaymentDueDt");
                    nodbrics_Installment = docAcordOutputXML.CreateElement("com.brics_Installment");
                    nodbrics_TotalOfPayments = docAcordOutputXML.CreateElement("com.brics_TotalOfPayments");
                    nodMethodPaymentCd = docAcordOutputXML.CreateElement("MethodPaymentCd");
                    */
                    /*nodTempAmtInAcord.InnerText = premiumValueInPremium;					
                    nodPremiumInAcord.AppendChild(nodTempAmtInAcord);					
                    nodTempCovgInAcord.InsertAfter(nodPremiumInAcord,nodTempCovgInAcord.LastChild);					

                    nodTempEffDate = docAcordInput.CreateElement("EffectiveDt");
                    nodTempEffDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                    nodTempCovgInAcord.InsertAfter(nodTempEffDate,nodTempCovgInAcord.LastChild);	*/

                }
                finalAcordOutPut = docAcordOutputXML.OuterXml;
                finalAcordOutPut = finalAcordOutPut.Replace("&lt;", "<");
                finalAcordOutPut = finalAcordOutPut.Replace("&gt;", ">");
                return finalAcordOutPut;
                #endregion
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }
        public string AttachPaymentPlanToAcordOutputForMotorCycle(string acordOutPutXML, string PaymentPlanXML)
        {
            try
            {
                return "";
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        public string AttachPaymentPlanToAcordOutputForRentalDwelling(string acordOutPutXML, string PaymentPlanXML)
        {
            try
            {
                return "";
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        public string AttachPaymentPlanToAcordOutputForUmbrella(string acordOutPutXML, string PaymentPlanXML)
        {
            try
            {
                return "";
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        public string AttachPaymentPlanToAcordOutputForWatercraft(string acordOutPutXML, string PaymentPlanXML)
        {
            try
            {
                return "";
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        /// <summary>
        /// Accepts  the agency id and returns the details 
        /// Returns all active records corresponding to the agency nodes that have not been converted into aplication.
        /// </summary>		
        /// <param name="agencyID">agency id </param>
        /// <returns>DataSet</returns>
        public static DataSet FetchDataFromAcordQuoteDetails(string agencyID)
        {
            string strStoredProc = "Proc_GetAcordQuoteDetails";
            DataSet dsAcordQuoteDetails = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@AGENCY_ID", agencyID, SqlDbType.Int);

                dsAcordQuoteDetails = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsAcordQuoteDetails;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }

        #endregion


        #endregion

        #region Constructors
        /// <summary>
        /// deafault constructor
        /// </summary>
        public ClsGenerateQuote(string SystemID)
        {
            boolTransactionLog = base.TransactionLogRequired;
            base.strActivateDeactivateProcedure = ACTIVATE_DEACTIVATE_PROC;
            mSystemID = SystemID;
        }
        #endregion

        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objGenerateQuoteInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int Add(Cms.Model.Quote.ClsGenerateQuoteInfo objGenerateQuoteInfo)
        {

            string strStoredProc = "";
            strStoredProc = "Proc_InsertQuote";

            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objGenerateQuoteInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_ID", objGenerateQuoteInfo.APP_ID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", objGenerateQuoteInfo.APP_VERSION_ID);
                objDataWrapper.AddParameter("@QUOTE_TYPE", objGenerateQuoteInfo.QUOTE_TYPE);
                objDataWrapper.AddParameter("@QUOTE_DESCRIPTION", objGenerateQuoteInfo.QUOTE_DESCRIPTION);
                objDataWrapper.AddParameter("@IS_ACCEPTED", objGenerateQuoteInfo.IS_ACCEPTED);
                objDataWrapper.AddParameter("@IS_ACTIVE", objGenerateQuoteInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY", objGenerateQuoteInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objGenerateQuoteInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@QUOTE_XML", objGenerateQuoteInfo.QUOTE_XML);
                objDataWrapper.AddParameter("@QUOTE_INPUT_XML", objGenerateQuoteInfo.QUOTE_INPUT_XML);
                // For HOME-BOAT case
                objDataWrapper.AddParameter("@QUOTE_ID", objGenerateQuoteInfo.QUOTE_ID);
                //Added by Manoj Rathore Sept.08.2009
                objDataWrapper.AddParameter("@RATE_EFFECTIVE_DATE", objGenerateQuoteInfo.RATE_EFFECTIVE_DATE);
                objDataWrapper.AddParameter("@BUSINESS_TYPE", objGenerateQuoteInfo.BUSINESS_TYPE);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@QID", null, SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    objGenerateQuoteInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objGenerateQuoteInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.CLIENT_ID = objGenerateQuoteInfo.CUSTOMER_ID;
                    objTransactionInfo.APP_ID = objGenerateQuoteInfo.APP_ID;
                    objTransactionInfo.APP_VERSION_ID = objGenerateQuoteInfo.APP_VERSION_ID;
                    objTransactionInfo.RECORDED_BY = objGenerateQuoteInfo.CREATED_BY;
                    objTransactionInfo.RECORD_DATE_TIME = objGenerateQuoteInfo.CREATED_DATETIME;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1448", ""); //"Rate has been added";
                    objTransactionInfo.CHANGE_XML = strTranXML;

                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }


                returnResult = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }




        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objGenerateQuoteInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int Add_Pol(ClsGeneratePolicyQuoteInfo objGeneratePolicyQuoteInfo)
        {
            string strStoredProc = "Proc_InsertQuote_Pol";
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objGeneratePolicyQuoteInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", objGeneratePolicyQuoteInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objGeneratePolicyQuoteInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@QUOTE_TYPE", objGeneratePolicyQuoteInfo.QUOTE_TYPE);
                objDataWrapper.AddParameter("@QUOTE_DESCRIPTION", objGeneratePolicyQuoteInfo.QUOTE_DESCRIPTION);
                objDataWrapper.AddParameter("@IS_ACCEPTED", objGeneratePolicyQuoteInfo.IS_ACCEPTED);
                objDataWrapper.AddParameter("@IS_ACTIVE", objGeneratePolicyQuoteInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY", objGeneratePolicyQuoteInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objGeneratePolicyQuoteInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@QUOTE_XML", objGeneratePolicyQuoteInfo.QUOTE_XML);
                objDataWrapper.AddParameter("@QUOTE_INPUT_XML", objGeneratePolicyQuoteInfo.QUOTE_INPUT_XML);
                // Home- Boat			
                objDataWrapper.AddParameter("@QUOTE_ID", objGeneratePolicyQuoteInfo.QUOTE_ID);
                //Added by Manoj Rathore Sept.08.2009
                objDataWrapper.AddParameter("@RATE_EFFECTIVE_DATE", objGeneratePolicyQuoteInfo.RATE_EFFECTIVE_DATE);
                objDataWrapper.AddParameter("@BUSINESS_TYPE", objGeneratePolicyQuoteInfo.BUSINESS_TYPE);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@QID", null, SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    objGeneratePolicyQuoteInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objGeneratePolicyQuoteInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.CLIENT_ID = objGeneratePolicyQuoteInfo.CUSTOMER_ID;
                    objTransactionInfo.POLICY_ID = objGeneratePolicyQuoteInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objGeneratePolicyQuoteInfo.POLICY_VERSION_ID;
                    objTransactionInfo.RECORDED_BY = objGeneratePolicyQuoteInfo.CREATED_BY;
                    objTransactionInfo.RECORD_DATE_TIME = objGeneratePolicyQuoteInfo.CREATED_DATETIME;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1448", ""); //"Rate has been added";
                    objTransactionInfo.CHANGE_XML = strTranXML;

                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                returnResult = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }
        #endregion

        /// <summary>
        /// Saves Policy Qote
        /// </summary>
        /// <param name="objGeneratePolicyQuoteInfo"></param>
        /// <param name="objDataWrapper"></param>
        /// <returns></returns>
        public int Add_Pol(ClsGeneratePolicyQuoteInfo objGeneratePolicyQuoteInfo, DataWrapper objDataWrapper)
        {
            string strStoredProc = "Proc_InsertQuote_Pol";
            DateTime RecordDate = DateTime.Now;
            int returnResult = 0;
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", objGeneratePolicyQuoteInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", objGeneratePolicyQuoteInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objGeneratePolicyQuoteInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@QUOTE_TYPE", objGeneratePolicyQuoteInfo.QUOTE_TYPE);
                objDataWrapper.AddParameter("@QUOTE_DESCRIPTION", objGeneratePolicyQuoteInfo.QUOTE_DESCRIPTION);
                objDataWrapper.AddParameter("@IS_ACCEPTED", objGeneratePolicyQuoteInfo.IS_ACCEPTED);
                objDataWrapper.AddParameter("@IS_ACTIVE", objGeneratePolicyQuoteInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY", objGeneratePolicyQuoteInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objGeneratePolicyQuoteInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@QUOTE_XML", objGeneratePolicyQuoteInfo.QUOTE_XML);
                objDataWrapper.AddParameter("@QUOTE_INPUT_XML", objGeneratePolicyQuoteInfo.QUOTE_INPUT_XML);
                objDataWrapper.AddParameter("@QUOTE_ID", objGeneratePolicyQuoteInfo.QUOTE_ID);
                objDataWrapper.AddParameter("@RATE_EFFECTIVE_DATE", objGeneratePolicyQuoteInfo.RATE_EFFECTIVE_DATE);
                objDataWrapper.AddParameter("@BUSINESS_TYPE", objGeneratePolicyQuoteInfo.BUSINESS_TYPE);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@QID", null, SqlDbType.Int, ParameterDirection.Output);

                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                returnResult = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();

                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }



        #region Update method
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldGenerateQuoteInfo">Model object having old information</param>
        /// <param name="objGenerateQuoteInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        //		public int Update(clsGenerateQuoteInfo objOldGenerateQuoteInfo,clsGenerateQuoteInfo objGenerateQuoteInfo)
        //		{
        //			string strTranXML;
        //			int returnResult = 0;
        //			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
        //			try 
        //			{
        //				objDataWrapper.AddParameter("@CUSTOMER_ID",objGenerateQuoteInfo.CUSTOMER_ID);
        //				objDataWrapper.AddParameter("@QUOTE_ID",objGenerateQuoteInfo.QUOTE_ID);
        //				objDataWrapper.AddParameter("@QUOTE_VERSION_ID",objGenerateQuoteInfo.QUOTE_VERSION_ID);
        //				objDataWrapper.AddParameter("@APP_ID",objGenerateQuoteInfo.APP_ID);
        //				objDataWrapper.AddParameter("@APP_VERSION_ID",objGenerateQuoteInfo.APP_VERSION_ID);
        //				objDataWrapper.AddParameter("@QUOTE_TYPE",objGenerateQuoteInfo.QUOTE_TYPE);
        //				objDataWrapper.AddParameter("@QUOTE_NUMBER",objGenerateQuoteInfo.QUOTE_NUMBER);
        //				objDataWrapper.AddParameter("@QUOTE_DESCRIPTION",objGenerateQuoteInfo.QUOTE_DESCRIPTION);
        //				objDataWrapper.AddParameter("@IS_ACCEPTED",objGenerateQuoteInfo.IS_ACCEPTED);
        //				objDataWrapper.AddParameter("@IS_ACTIVE",objGenerateQuoteInfo.IS_ACTIVE);
        //				objDataWrapper.AddParameter("@CREATED_BY",objGenerateQuoteInfo.CREATED_BY);
        //				objDataWrapper.AddParameter("@CREATED_DATETIME",objGenerateQuoteInfo.CREATED_DATETIME);
        //				objDataWrapper.AddParameter("@MODIFIED_BY",objGenerateQuoteInfo.MODIFIED_BY);
        //				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGenerateQuoteInfo.LAST_UPDATED_DATETIME);
        //				if(TransactionRequired) 
        //				{
        //					objGenerateQuoteInfo.TransactLabel = ClsCommon.MapTransactionLabel(".aspx.resx");string strUpdate = objBuilder.GetUpdateSQL(objOldGenerateQuoteInfo,objGenerateQuoteInfo,out strTranXML);
        //
        //					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
        //					objTransactionInfo.TRANS_TYPE_ID	=	3;
        //					objTransactionInfo.RECORDED_BY		=	objGenerateQuoteInfo.MODIFIED_BY;
        //					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
        //					objTransactionInfo.CHANGE_XML		=	strTranXML;
        //					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
        //
        //				}
        //				else
        //				{
        //					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
        //				}
        //				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
        //				return returnResult;
        //			}
        //			catch(Exception ex)
        //			{
        //				throw(ex);
        //			}
        //			finally
        //			{
        //				if(objDataWrapper != null) 
        //				{
        //					objDataWrapper.Dispose();
        //				}
        //				if(objBuilder != null) 
        //				{
        //					objBuilder = null;
        //				}
        //			}
        //		}
        //		 

        #endregion

        # region Verification  of InputXML


        /// <summary>
        /// Methods for Verification  of InputXML
        /// </summary>
        /// <param name="inputXML">InputXML</param>
        /// <param name="appLob">LobId of the LOB</param>
        /// <returns>String in the format String#0 or String#1. 0 impies invalid input. 1 implies valid input</returns>
        public string InputXmlVerification(string inputXML, string appLobID)
        {
            string strlobID = "";
            string strInputXML;
            //int customerId, appID, appversionID;

            //string strQuote_ID;
            //string strLOB;
            string validXML = "";

            strInputXML = "";
            strlobID = appLobID;
            strInputXML = inputXML;

            try
            {
                string strCSSNo = "4"; //GetColorScheme();

                XmlDocument inputXmlDoc = new XmlDocument();

                strInputXML = strInputXML.Replace("&AMP;", "&amp;");
                strInputXML = strInputXML.Replace("</QUICKQUOTE>", "<CSSNUM CSSVALUE =' " + strCSSNo + " '/></QUICKQUOTE>");
                strInputXML = strInputXML.Replace("\n", "");
                strInputXML = strInputXML.Replace("\r", "");
                strInputXML = strInputXML.Replace("\t", "");
                inputXmlDoc.LoadXml(strInputXML);

                string xslFilePath = "";     // this variable stores path of the xsl
                string strReplacedXSLPath = "";
                XmlNodeList factorNode;
                XmlNode quotefactorNode;
                string strNodeXML = "";
                switch (int.Parse(strlobID))
                {
                    case 1:         //LOB_HOME:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("HO_Input_Xml_Verification");
                        factorNode = inputXmlDoc.SelectNodes("QUICKQUOTE/DWELLINGDETAILS");
                        if (factorNode != null && factorNode.Count > 0)
                        {
                            strNodeXML = factorNode.Item(0).OuterXml;
                            strReplacedXSLPath = GetProductFactorMasterPath(strNodeXML, "HO");  // objQ.GetProductFactorMasterPath(strNodeXML,"HO");
                        }

                        break;
                    case 2:         //LOB_PRIVATE_PASSENGER
                        xslFilePath = ClsCommon.GetKeyValueWithIP("Auto_Input_Xml_Verification");
                        quotefactorNode = inputXmlDoc.SelectSingleNode("QUICKQUOTE");
                        if (quotefactorNode != null)
                        { strNodeXML = quotefactorNode.OuterXml; }
                        string strReplacedXSLPath_P = GetProductFactorMasterPath(strInputXML, "AUTOP");  // objQ.GetProductFactorMasterPath(strInputXML,"AUTOP");
                        strInputXML = strInputXML.Replace("<CALLEDFROM>DISCOUNTSP</CALLEDFROM>", "<CALLEDFROM>DISCOUNTSC</CALLEDFROM>");
                        string strReplacedXSLPath_C = GetProductFactorMasterPath(strInputXML, "AUTOC");  //objQ.GetProductFactorMasterPath(strInputXML,"AUTOC");

                        strReplacedXSLPath = strReplacedXSLPath_P + "^" + strReplacedXSLPath_C;
                        break;

                    case 3:          //LOB_MOTORCYCLE
                        xslFilePath = ClsCommon.GetKeyValueWithIP("MotorCycle_Input_Xml_Verification");
                        strReplacedXSLPath = GetProductFactorMasterPath(strInputXML, "CYCL"); // objQ.GetProductFactorMasterPath(strInputXML,"CYCL");
                        break;
                    case 4:         //LOB_WATERCRAFT:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("WaterCraft_Input_Xml_Verification");
                        strReplacedXSLPath = GetProductFactorMasterPath(strInputXML, "BOAT"); // objQ.GetProductFactorMasterPath(strInputXML,"BOAT");
                        break;
                    case 5:         //LOB_UMBRELLA
                        xslFilePath = ClsCommon.GetKeyValueWithIP("Umb_Input_Xml_Verification");
                        strReplacedXSLPath = GetProductFactorMasterPath(strInputXML, "UMB"); // objQ.GetProductFactorMasterPath(strInputXML,"BOAT");

                        break;
                    case 6:          //LOB_RENTAL_DWELLING
                        xslFilePath = ClsCommon.GetKeyValueWithIP("Rental_Input_Xml_Verification");
                        factorNode = inputXmlDoc.SelectNodes("QUICKQUOTE/DWELLINGDETAILS");
                        if (factorNode != null)
                        { strNodeXML = factorNode.Item(0).OuterXml; }
                        strReplacedXSLPath = GetProductFactorMasterPath(strNodeXML, "REDW"); // objQ.GetProductFactorMasterPath(strNodeXML,"REDW");
                        break;
                    case 7:          //General Liability
                        strReplacedXSLPath = "";
                        break;

                }


                XmlDocument doc = new XmlDocument();
                doc.Load(xslFilePath);
                string strModifiedPrmXsl = doc.InnerXml;

                //In case Of Auto Split The Personal Vehicle and Commercial Vehicle path
                if (strlobID == LOB_PRIVATE_PASSENGER)
                {
                    string[] strFilePath = strReplacedXSLPath.Split('^');
                    strFilePath[0].Replace("\r", "");
                    strFilePath[0].Replace("\t", "");
                    strFilePath[0].Replace("\n", "");
                    strFilePath[1].Replace("\r", "");
                    strFilePath[1].Replace("\t", "");
                    strFilePath[1].Replace("\n", "");

                    //					string strPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetApplicationPath();
                    //					strPath=strPath+strFilePath[1];

                    string strPath = Cms.BusinessLayer.BlCommon.ClsCommon.WebAppUNCPath;
                    strPath = System.IO.Path.GetFullPath(strPath + @"\" + strFilePath[1]);

                    strModifiedPrmXsl = strModifiedPrmXsl.Replace("FactorPathPer", strFilePath[0].Trim().ToString());
                    strModifiedPrmXsl = strModifiedPrmXsl.Replace("FactorPathCom", strPath.Trim().ToString());

                }
                else
                {
                    strModifiedPrmXsl = strModifiedPrmXsl.Replace("FactorPath", strReplacedXSLPath);
                }
                //XslTransform xslt = new XslTransform();
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(new XmlTextReader(new StringReader(strModifiedPrmXsl)));
                StringWriter writer = new StringWriter();
                XmlDocument xmlDocTemp = new XmlDocument();
                strInputXML = strInputXML.Replace("&gt;", ">");
                strInputXML = strInputXML.Replace("&lt;", "<");
                strInputXML = strInputXML.Replace("\"", "'");
                strInputXML = strInputXML.Replace("\r", "");
                strInputXML = strInputXML.Replace("\n", "");
                strInputXML = strInputXML.Replace("\t", "");

                xmlDocTemp.LoadXml(strInputXML);
                XPathNavigator nav = ((IXPathNavigable)xmlDocTemp).CreateNavigator();
                xslt.Transform(nav, null, writer);
                string returnString = "";
                returnString = writer.ToString();

                XmlDocument finalXML = new XmlDocument();
                //LINK doesnt have a close tag. so comment before loading and uncomment after loading.

                returnString = returnString.Replace("&gt;", ">");
                returnString = returnString.Replace("&lt;", "<");
                returnString = returnString.Replace("\"", "'");
                returnString = returnString.Replace("\r", "");
                returnString = returnString.Replace("\n", "");
                returnString = returnString.Replace("\t", "");
                returnString = returnString.Replace("<META", "<!-- <META");
                returnString = returnString.Replace("rel='stylesheet'>", "rel='stylesheet'>-->");
                finalXML.LoadXml(returnString);
                returnString = returnString.Replace("<!-- <META", "<META");
                returnString = returnString.Replace("rel='stylesheet'>-->", "rel='stylesheet'>");
                returnString = returnString.Replace("<META http-equiv='Content-Type' content='text/html; charset=utf-16'>", "<META http-equiv='Content-Type' content='text/html; charset=utf-16'/>");
                returnString = returnString.Replace("<LINK id='lk' href='/cms/cmsweb/css/css1.css' type='text/css' rel='stylesheet'>", "<LINK id='lk' href='/cms/cmsweb/css/css1.css' type='text/css' rel='stylesheet'/>");
                //string strDisplay;
                XmlNodeList nodLst = finalXML.GetElementsByTagName("returnValue");

                if (nodLst != null && nodLst.Count > 0)
                {
                    string verifiedXML = nodLst.Item(0).InnerText; //1 or 0
                    if (verifiedXML.Trim().Equals("0"))
                    {
                        validXML = "0";
                    }
                    else
                    {
                        validXML = "1";//save the html in table and update show_quote=1;
                    }
                }

                returnString = returnString + "#" + validXML;
                return returnString;
            }

            catch (Exception exc)
            {
                throw new Exception("Error occured while generating InputXML", exc);
                //throw(exc);
            }
            finally
            { }

        }


        #endregion

        #region Genrate New Rate Quote For Product

        public string GetPolicyProductInputXML(int iCustomerId, int iPolicyId, int iPolicyVersionId, int iLobId, int colorscheme, int iUserId, string calledfrom)
        {
            ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
            string InputXML = objGeneralInformation.GenrateRiskCoveragesXml(iCustomerId, iPolicyId, iPolicyVersionId, iLobId, colorscheme, iUserId);
            return InputXML;
        }

        public int GetCoverages_QuoteId(int iCustomerId, int iPolicyId, int iPolicyVersionId, int iLobId, int colorscheme, int iUserId, string calledfrom)
        {

            ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
            string Input_XML = "",Rate_XML = "";//strRateXML, FinalHML = ""
            int QoteId = 0;

            try
            {
                Input_XML = objGeneralInformation.GenrateRiskCoveragesXml(iCustomerId, iPolicyId, iPolicyVersionId, iLobId, colorscheme, iUserId);
                Rate_XML = GetRateXMLForInsert(Input_XML, colorscheme);

                if (Rate_XML != "")
                {
                    if (calledfrom == "QAPP")
                    {

                        //int QuoteId = 0;
                        //XmlDocument xmldoc = new XmlDocument();
                        //xmldoc.LoadXml(Input_XML);
                        //if (xmldoc.SelectSingleNode("QUOTE/POLICY/POLICY_STATUS") != null)
                        //{
                        //    BusinessType = xmldoc.SelectSingleNode("QUOTE/POLICY/POLICY_STATUS").InnerXml;
                        //}
                        ClsGenerateQuoteInfo objGenerateQuoteInfo = new ClsGenerateQuoteInfo();
                        objGenerateQuoteInfo.CUSTOMER_ID = iCustomerId;
                        objGenerateQuoteInfo.APP_ID = iPolicyId;
                        objGenerateQuoteInfo.APP_VERSION_ID = iPolicyVersionId;
                        objGenerateQuoteInfo.QUOTE_XML = Rate_XML;
                        objGenerateQuoteInfo.QUOTE_INPUT_XML = Input_XML;
                        objGenerateQuoteInfo.IS_ACTIVE = "Y";
                        objGenerateQuoteInfo.QUOTE_DESCRIPTION = "New Quote";
                        objGenerateQuoteInfo.CREATED_BY = iUserId;
                        objGenerateQuoteInfo.CREATED_DATETIME = DateTime.Now;
                        objGenerateQuoteInfo.QUOTE_TYPE = "Rate XMl";
                        objGenerateQuoteInfo.IS_ACCEPTED = "N";
                        objGenerateQuoteInfo.RATE_EFFECTIVE_DATE = ConvertToDate(DateTime.Now.ToString());
                        objGenerateQuoteInfo.BUSINESS_TYPE = "QAPP";
                        objGenerateQuoteInfo.QUOTE_ID = 0;

                        QoteId = Add(objGenerateQuoteInfo);

                    }
                    else
                    {
                        QoteId = InsertCoverageXML(Input_XML, Rate_XML, iCustomerId, iPolicyId, iPolicyVersionId, iUserId);
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
            return QoteId;


        }
        public string GetCoveragesXmlTransform_HTML(string inputXML)
        {
            string strReturn = "";
            string strInputXML = "";
            strInputXML = inputXML;
            string strCSSNo = "4";
            //StringWriter swriter;
            try
            {
                XmlDocument inputXmlDoc = new XmlDocument();

                string strAdditionalText = "<?xml version='1.0'?> " +
                      "<?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?>" +
                      "<!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple'" +
                      " xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'>" +
                      " <!ATTLIST person id ID #IMPLIED>" +
                      "]>";
                strInputXML = strInputXML.Replace(strAdditionalText, "");
                strInputXML = strInputXML.Replace("&AMP;", "&amp;");
                strInputXML = strInputXML.Replace("</QUOTE>", "<CSSNUM CSSVALUE =' " + strCSSNo + " '/></QUOTE>");
                strInputXML = strInputXML.Replace("\n", "");
                strInputXML = strInputXML.Replace("\r", "");
                strInputXML = strInputXML.Replace("\t", "");
                inputXmlDoc.LoadXml(strInputXML);

                string xslFilePath = "";             // this variable stores path of the xsl            


                //xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathNamedPerilsRating");
                // xslFilePath = ClsCommon.GetKeyValueWithIP("FinalQuoteAuto");
                //FilePathRatingXML

                xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathRatingXML");
                //XslTransform trnsTemp = new XslTransform();
                XslCompiledTransform trnsTemp = new XslCompiledTransform();
                trnsTemp.Load(xslFilePath);
                XPathNavigator nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                StringWriter swGetHTML = new StringWriter();
                trnsTemp.Transform(nav, null, swGetHTML);
                string rateHTML = swGetHTML.ToString();
                strReturn = rateHTML;

                return strReturn;               //Return OutPut String
            }
            catch (Exception ex) 
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return null; 
            }

        }
        private int InsertCoverageXML(string Input_Xml, string Rate_XML, int iCUSTOMER_ID, int iPOLICY_ID, int iPOLICY_VERSION_ID, int userid)
        {
            int retval = 0;
            DataSet ds = null;
            string oldinputXMl = string.Empty;
            string BusinessType = "";
            //int QuoteId = 0;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(Input_Xml);

            //xmldoc.Load("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>"+Input_Xml);
            if (xmldoc.SelectSingleNode("QUOTE/POLICY/POLICY_STATUS") != null)
            {
                BusinessType = xmldoc.SelectSingleNode("QUOTE/POLICY/POLICY_STATUS").InnerXml;
            }
            try
            {
                string strPolicyQuote = GetPolicyQuoteCode(iCUSTOMER_ID, iPOLICY_ID, iPOLICY_VERSION_ID);
                ds = GetQuoteOldXML_Policy(iCUSTOMER_ID, iPOLICY_ID, iPOLICY_VERSION_ID, strPolicyQuote);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    oldinputXMl = ds.Tables[0].Rows[0]["QUOTE_INPUT_XML"].ToString();
                    if (oldinputXMl != Input_Xml)
                    {
                        //Convert it into xmldoc                      
                        retval = InsertCoverageXML(Input_Xml, Rate_XML, iCUSTOMER_ID, iPOLICY_ID, iPOLICY_VERSION_ID, userid, BusinessType);
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[0]["QUOTE_ID"].ToString() != "")
                            retval = int.Parse(ds.Tables[0].Rows[0]["QUOTE_ID"].ToString());
                    }
                }
                else
                {

                    if (xmldoc.SelectSingleNode("QUOTE/POLICY/POLICY_STATUS") != null)
                    {
                        BusinessType = xmldoc.SelectSingleNode("QUOTE/POLICY/POLICY_STATUS").InnerXml;
                    }
                    retval = InsertCoverageXML(Input_Xml, Rate_XML, iCUSTOMER_ID, iPOLICY_ID, iPOLICY_VERSION_ID, userid, BusinessType);

                }
                return retval;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private int InsertCoverageXML(string Input_Xml, string Rate_XML, int iCUSTOMER_ID, int iPOLICY_ID, int iPOLICY_VERSION_ID, int userid, string BusinessType)
        {
            int retunval = 0;
            ClsGeneratePolicyQuoteInfo objGeneratePolicyQuoteInfo = new ClsGeneratePolicyQuoteInfo();
            objGeneratePolicyQuoteInfo.CUSTOMER_ID = iCUSTOMER_ID;
            objGeneratePolicyQuoteInfo.POLICY_ID = iPOLICY_ID;
            objGeneratePolicyQuoteInfo.POLICY_VERSION_ID = iPOLICY_VERSION_ID;
            objGeneratePolicyQuoteInfo.QUOTE_TYPE = "Rate XMl";
            objGeneratePolicyQuoteInfo.QUOTE_DESCRIPTION = "Premium";
            objGeneratePolicyQuoteInfo.IS_ACCEPTED = "N";
            objGeneratePolicyQuoteInfo.IS_ACTIVE = "Y";
            objGeneratePolicyQuoteInfo.CREATED_BY = userid;
            objGeneratePolicyQuoteInfo.CREATED_DATETIME = ConvertToDate(DateTime.Now.ToString());
            objGeneratePolicyQuoteInfo.QUOTE_XML = Rate_XML;
            objGeneratePolicyQuoteInfo.QUOTE_INPUT_XML = Input_Xml;
            objGeneratePolicyQuoteInfo.QUOTE_ID = 0;
            objGeneratePolicyQuoteInfo.RATE_EFFECTIVE_DATE = ConvertToDate(DateTime.Now.ToString());
            objGeneratePolicyQuoteInfo.BUSINESS_TYPE = BusinessType;
            retunval = this.Add_Pol(objGeneratePolicyQuoteInfo);
            return retunval;
        }


        #endregion

        #region Genrate Plain Rate XML

        public string GetRateXMLForInsert(string inputXML, int cssnum)
        {
            NumberFormatInfo numberFormatInfo = null;
            try
            {
                StringBuilder strbldrRiskNode = new StringBuilder();
                string ProductName = "";
                string SubLob = "";
                XmlNodeList nodlstCOVERAGES;
                string PolicyCurrency = "";
                string FinalCoveragesXML = "";
                string FinalHeaderXML = "";
                string FinalClientTop = "";
                string FinalPremimumXML = "";
                string startPremiumtag = "<PRIMIUM>", closedPremiumtag = "</PRIMIUM>";
                //Get the QuoteXML *************************		 
                if (inputXML != "<NewDataSet />" && inputXML != "")
                {

                    string strAdditionalText = "<?xml version='1.0'?> " +
                        "<?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?>" +
                        "<!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple'" +
                        " xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'>" +
                        " <!ATTLIST person id ID #IMPLIED>" +
                        "]>";
                    //  strAdditionalText = "";
                    double RISK_PREMIUM = 0;
                    double TOTAL_RISK_PREMIUM = 0;
                    XmlDocument xmlTempDoc = new XmlDocument();
                    inputXML = inputXML.Replace("\"", "'");
                    inputXML = inputXML.Replace("<?xml version='1.0' encoding='utf-8'?>", "");
                    // inputXML = inputXML.ToUpper();
                    inputXML = inputXML.Replace("&AMP;", "&amp;");



                    string strInputXML = "<INPUTXML>" + inputXML + "</INPUTXML>";
                    xmlTempDoc.LoadXml(strInputXML);
                    XmlElement xmlTempElement = xmlTempDoc.DocumentElement;

                    //Set Policy Currency
                    //PolicyCurrency = xmlTempDoc.SelectSingleNode("INPUTXML/QUOTE/POLICY/POLICY_CURRENCY").InnerXml.ToString();
                    PolicyCurrency = "1";
                    if (PolicyCurrency != "")
                        numberFormatInfo = ClsCommon.GetNumberFormat(int.Parse(PolicyCurrency));
                    if (numberFormatInfo == null)
                        numberFormatInfo = ClsCommon.GetNumberFormat(1);

                    if (xmlTempDoc.SelectSingleNode("INPUTXML/QUOTE/POLICY/LOB") != null)
                    {
                        ProductName = xmlTempDoc.SelectSingleNode("INPUTXML/QUOTE/POLICY/LOB").InnerText.ToString();
                        SubLob = xmlTempDoc.SelectSingleNode("INPUTXML/QUOTE/POLICY/SUB_LOB_DESC").InnerText.ToString();
                    }

                    int count = xmlTempDoc.SelectNodes("INPUTXML/QUOTE/COVERAGES").Count;

                    if (count < 1)
                        return null;
                    else
                    {
                        nodlstCOVERAGES = xmlTempDoc.SelectNodes("INPUTXML/QUOTE/COVERAGES");

                        string oldstrRISK_ID = "", strRISK_ID = "", strCovdetails = "";

                        int nodeno = 0;

                        if (xmlTempDoc.SelectNodes("INPUTXML/QUOTE/COVERAGES/COVERAGE").Count > 0)
                        {
                            foreach (XmlNode node in xmlTempDoc.SelectNodes("INPUTXML/QUOTE/COVERAGES/COVERAGE"))
                            {
                                strRISK_ID = node.SelectSingleNode("RISK_ID").InnerText;

                                if (strRISK_ID != oldstrRISK_ID)
                                {
                                    if (oldstrRISK_ID != "")
                                    {
                                        strCovdetails = genrate_riskformat_XML("TOTAL_RISK_PREMIUM", RISK_PREMIUM, nodeno, numberFormatInfo);
                                        strbldrRiskNode.Append(strCovdetails);
                                        TOTAL_RISK_PREMIUM = TOTAL_RISK_PREMIUM + RISK_PREMIUM;
                                        strCovdetails = genrate_riskformat_XML("FINAL_PREMIUM", TOTAL_RISK_PREMIUM, 0, numberFormatInfo);
                                        strbldrRiskNode.Append(strCovdetails);
                                        strbldrRiskNode.Append("</RISK>");

                                        nodeno = 0;
                                        RISK_PREMIUM = 0;
                                    }

                                    strbldrRiskNode.Append("<RISK ID='" + strRISK_ID + "' TYPE='PRODUCT'" + " RISK_DESC='" + node.SelectSingleNode("LOCATION").InnerText + "'>");
                                    strCovdetails = genrate_riskformat_XML("QDATE", 0, 0, numberFormatInfo);
                                    strbldrRiskNode.Append(strCovdetails);
                                    oldstrRISK_ID = strRISK_ID;
                                }
                                //QDATE

                                strCovdetails = genrate_riskformat_XML(node, nodeno, ProductName, "PREMIUM_STEP", numberFormatInfo);
                                strbldrRiskNode.Append(strCovdetails);
                                nodeno++;
                                if (node.SelectSingleNode("WRITTEN_PREMIUM").InnerText != null && node.SelectSingleNode("WRITTEN_PREMIUM").InnerText != "")
                                    RISK_PREMIUM = RISK_PREMIUM + double.Parse(node.SelectSingleNode("WRITTEN_PREMIUM").InnerText, numberFormatInfo);

                            }
                            TOTAL_RISK_PREMIUM = TOTAL_RISK_PREMIUM + RISK_PREMIUM;
                            strCovdetails = genrate_riskformat_XML("TOTAL_RISK_PREMIUM", RISK_PREMIUM, nodeno, numberFormatInfo);
                            strbldrRiskNode.Append(strCovdetails);
                            strCovdetails = genrate_riskformat_XML("FINAL_PREMIUM", TOTAL_RISK_PREMIUM, 0, numberFormatInfo);
                            strbldrRiskNode.Append(strCovdetails);
                            strbldrRiskNode.Append("</RISK>");
                        }
                        string inputXML1 = strbldrRiskNode.ToString();
                        inputXML1 = inputXML1.Replace("\"", "'");
                        inputXML1 = inputXML1.Replace("<?xml version='1.0' encoding='utf-8'?>", "");
                        //inputXML1 = inputXML1.ToUpper();
                        inputXML1 = inputXML1.Replace("&AMP;", "&amp;");
                        FinalCoveragesXML = inputXML1;
                    }

                    string TITLE = "";
                    string RATING_HEADER = "", CSSNUM = "";
                    RATING_HEADER = ClsCommon.GetKeyValue("Rating_Header");
                    string PRODUCT_NAME = ProductName + " - " + SubLob;

                    XmlElement xmlAppElement = xmlTempDoc.DocumentElement;

                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/APP_EFFECTIVE_DATE") != null)
                        QUOTE_EFFECTIVE_DATE = xmlAppElement.SelectSingleNode("QUOTE/POLICY/APP_EFFECTIVE_DATE").InnerText;

                    TITLE = "";
                    QUOTE_DATE = "";
                    //QUOTE_EFFECTIVE_DATE  = " QUOTE_DATE='" + DateTime.Now.ToShortDateString().ToString() + "'";
                    QUOTE_EFFECTIVE_DATE = "";
                    RATE_EFFECTIVE_DATE = "";
                    BUSINESS_TYPE = "";
                    //CSSNUM = " CSSNUM='" + cssnum + "'";
                   // PRODUCT_NAME = "";


                    TITLE = "TITLE='" + RATING_HEADER + "'";
                    QUOTE_DATE = " QUOTE_DATE='" + DateTime.Now.ToString("MM/dd/yyyy").ToString() + "'";
                    //QUOTE_EFFECTIVE_DATE  = " QUOTE_DATE='" + DateTime.Now.ToShortDateString().ToString() + "'";
                    QUOTE_EFFECTIVE_DATE = " QUOTE_EFFECTIVE_DATE='" + string.Format(QUOTE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";
                    RATE_EFFECTIVE_DATE = " RATE_EFFECTIVE_DATE='" + string.Format(RATE_EFFECTIVE_DATE, "ddd, dd Month yyyy") + "'";
                    BUSINESS_TYPE = " BUSINESS_TYPE='" + BUSINESS_TYPE + "'";
                    //CSSNUM = " CSSNUM='" + cssnum + "'";
                    PRODUCT_NAME = " PRODUCT_NAME='" + PRODUCT_NAME + "'";


                    string final_string = "<HEADER " + TITLE + QUOTE_DATE + QUOTE_EFFECTIVE_DATE + RATE_EFFECTIVE_DATE + BUSINESS_TYPE + CSSNUM + PRODUCT_NAME + "> </HEADER>";
                    FinalHeaderXML = final_string;
                    FinalHeaderXML = FinalHeaderXML.Replace("\"", "'");
                    FinalHeaderXML = FinalHeaderXML.Replace("<?xml version='1.0' encoding='utf-8'?>", "");
                    // FinalHeaderXML = FinalHeaderXML.ToUpper();
                    FinalHeaderXML = FinalHeaderXML.Replace("&AMP;", "&amp;");

                    string strFName = "", strMName = "", strLName = "", strAdd1 = "", strAdd2 = "", strCity = "";
                    string strState = "", strZipcode = "", strAgencyAdd1 = "", strAgencyCity = "", strAgencyState = "", strAgencyAdd2 = "";
                    string strAgencyZip = "", strAgencyName = "", strAgencyPhone = "", strStateCode = "", strQQNumber = "", strAPPNumber = "";
                    string strAPPVersion = "", strPOLNumber = "", strPOLVersion = "";
                    string strPolicy_Currency = "", Currency = "", POLICY_STATUS_CODE = "";//POL_POLICY_STATUS = "",



                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/POLICY_CURRENCY") != null)
                        strPolicy_Currency = xmlAppElement.SelectSingleNode("QUOTE/POLICY/POLICY_CURRENCY").InnerText;

                    // if (strPolicy_Currency == "14546")
                    //Currency = "$ ";
                    //else if (strPolicy_Currency == "14545")
                    // Currency = "R$ ";
                    Currency = ClsCommon.GetPolicyCurrencySymbol(strPolicy_Currency);

                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/FIRST_NAME") != null)
                        strFName = xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/FIRST_NAME").InnerText;


                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/MIDDLE_NAME") != null)
                        strMName = xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/MIDDLE_NAME").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/LAST_NAME") != null)
                        strLName = xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/LAST_NAME").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS1") != null)
                        strAdd1 = xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS1").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS2") != null)
                        strAdd2 = xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/ADDRESS2").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/CITY") != null)
                        strCity = xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/CITY").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/STATE") != null)
                        strState = xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/STATE").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/ZIP_CODE") != null)
                        strZipcode = xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/ZIP_CODE").InnerText;


                    //AGency Info

                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1") != null)
                        strAgencyAdd1 = xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD1").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD2") != null)
                        strAgencyAdd2 = xmlAppElement.SelectSingleNode("QUOTE/POLICY/PRIMARYAPPLICANT/AGENCY_ADD2").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/AGENCY_CITY") != null)
                        strAgencyCity = xmlAppElement.SelectSingleNode("QUOTE/POLICY/AGENCY_CITY").InnerText;


                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/AGENCY_STATE") != null)
                        strAgencyState = xmlAppElement.SelectSingleNode("QUOTE/POLICY/AGENCY_STATE").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/AGENCY_ZIP") != null)
                        strAgencyZip = xmlAppElement.SelectSingleNode("QUOTE/POLICY/AGENCY_ZIP").InnerText;


                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/AGENCY_DISPLAY_NAME") != null)
                        strAgencyName = xmlAppElement.SelectSingleNode("QUOTE/POLICY/AGENCY_DISPLAY_NAME").InnerText;

                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/AGENCY_PHONE") != null)
                        strAgencyPhone = xmlAppElement.SelectSingleNode("QUOTE/POLICY/AGENCY_PHONE").InnerText;

                    //Get Policy Status Code

                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/POLICY_STATUS_CODE") != null)
                        POLICY_STATUS_CODE = xmlAppElement.SelectSingleNode("QUOTE/POLICY/POLICY_STATUS_CODE").InnerText;

                    //if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE_CODE") != null)
                    //    strStateCode = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/PRIMARYAPPLICANT/STATE_CODE").InnerText;

                    //if (xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/QQNUMBER") != null)
                    //    strQQNumber = xmlAppElement.SelectSingleNode("QUICKQUOTE/POLICY/QQNUMBER").InnerText.ToString().Trim();

                    if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/POL_POLICY_STATUS") != null)
                    {
                        if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/POL_POLICY_STATUS").InnerText.ToString().Trim() == "")
                        {
                            //APP

                            if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/APP_NUMBER") != null)
                            {
                                strAPPNumber = xmlAppElement.SelectSingleNode("QUOTE/POLICY/APP_NUMBER").InnerText.ToString().Trim();
                                strPOLNumber = xmlAppElement.SelectSingleNode("QUOTE/POLICY/APP_NUMBER").InnerText.ToString().Trim();
                            }
                            if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/POLICY_DISP_VERSION") != null)
                            {
                                //  strAPPVersion = xmlAppElement.SelectSingleNode("QUOTE/POLICY/APP_VERSION").InnerText.ToString().Trim();
                                strPOLVersion = xmlAppElement.SelectSingleNode("QUOTE/POLICY/POLICY_DISP_VERSION").InnerText.ToString().Trim();
                            }
                        }
                        else
                        {
                            //POL
                            if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/POLICY_NUMBER") != null)
                                strPOLNumber = xmlAppElement.SelectSingleNode("QUOTE/POLICY/POLICY_NUMBER").InnerText.ToString().Trim();
                            if (xmlAppElement.SelectSingleNode("QUOTE/POLICY/POLICY_DISP_VERSION") != null)
                                strPOLVersion = xmlAppElement.SelectSingleNode("QUOTE/POLICY/POLICY_DISP_VERSION").InnerText.ToString().Trim();
                        }
                    }
                    string strClientTop = "<CLIENT_TOP_INFO PRIMARY_APP_NAME ='" + strFName + ' ' + strMName + ' ' + strLName + "'" +
                        " PRIMARY_APP_ADDRESS1 ='" + strAdd1 + "'" +
                        " PRIMARY_APP_ADDRESS2 ='" + strAdd2 + "'" +
                        " PRIMARY_APP_CITY ='" + strCity + "'" +
                        " PRIMARY_APP_STATE ='" + strState + "'" +
                        " PRIMARY_APP_ZIPCODE ='" + strZipcode + "'" +

                        " AGENCY_ADD1='" + strAgencyAdd1 + "'" +
                        " AGENCY_ADD2='" + strAgencyAdd2 + "'" +
                        " AGENCY_CITY='" + strAgencyCity + "'" +
                        " AGENCY_STATE='" + strAgencyState + "'" +
                        " AGENCY_ZIP='" + strAgencyZip + "'" +

                        " AGENCY_NAME='" + strAgencyName + "'" +
                        " AGENCY_PHONE='" + strAgencyPhone + "'" +
                        " QQ_NUMBER='" + strQQNumber + "'" +
                        " APP_NUMBER='" + strAPPNumber + "'" +
                        " APP_VERSION='" + strAPPVersion + "'" +
                        " POL_NUMBER='" + strPOLNumber + "'" +
                        " POL_VERSION='" + strPOLVersion + "'" +
                        " POLICY_CURRENCY='" + strPolicy_Currency + "'" +
                        " CURRENCY='" + Currency + "'" +
                        " POLICY_STATUS_CODE='" + POLICY_STATUS_CODE + "'" +
                        " STATE_CODE ='" + strStateCode + "'>";
                    strClientTop = strClientTop + "</CLIENT_TOP_INFO>";

                    FinalClientTop = strClientTop;
                    FinalClientTop = FinalClientTop.Replace("\"", "'");
                    FinalClientTop = FinalClientTop.Replace("<?xml version='1.0' encoding='utf-8'?>", "");
                    FinalClientTop = FinalClientTop.Replace("&AMP;", "&amp;");


                    FinalPremimumXML = strAdditionalText + startPremiumtag + FinalCoveragesXML + FinalHeaderXML + FinalClientTop + closedPremiumtag;
                }

                FinalPremimumXML = FinalPremimumXML.Replace("&", "&amp;");

                return FinalPremimumXML;

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }
        private string genrate_riskformat_XML(XmlNode XML, int stepid, string ProductName, string StepType, NumberFormatInfo NumberFormat)
        {
            string OutXML = string.Empty;
            string strPremium = XML.SelectSingleNode("WRITTEN_PREMIUM").InnerText;
            string strLimt = XML.SelectSingleNode("LIMIT_1").InnerText;
            string strDeductable = XML.SelectSingleNode("DEDUCTIBLE_1").InnerText;
            //double dPremium = 0;
            if (strPremium != "")
            {
                double WrittenPremium = double.Parse(strPremium, NumberFormat);
                strPremium = String.Format(NumberFormat, "{0:0,0.00}", WrittenPremium);
            }
            if (strLimt != "")
            {
                double Limt = double.Parse(strLimt, NumberFormat);
                if (Limt != 0)
                    strLimt = String.Format(NumberFormat, "{0:0,0.00}", Limt);
            }
            if (strDeductable != "")
            {
                double Deductable = double.Parse(strDeductable, NumberFormat);
                if (Deductable != 0)
                    strDeductable = String.Format(NumberFormat, "{0:0,0.00}", Deductable);

            }



            if (StepType.ToUpper() == "PREMIUM_STEP")
            {
                OutXML = "<STEP TS='" + stepid.ToString() + "' RNO='' PRODUCTDESC='" +
                    ProductName + "' PRODUCTVALUE='' GROUPDESC='' GROUPVAL='' STEPDESC='" + XML.SelectSingleNode("COV_DES").InnerText
                    + "' STEPDEDUCTABLE='" + XML.SelectSingleNode("MINIMUM_DEDUCTIBLE").InnerText + "' STEPLIMIT='"
                    + strLimt//XML.SelectSingleNode("LIMIT_1").InnerText 
                    + "' STEPPREMIUM='" + strPremium // + XML.SelectSingleNode("WRITTEN_PREMIUM").InnerText
                    + "' LIMITVALUE='" + strLimt//+ XML.SelectSingleNode("LIMIT_1").InnerText 
                    + "' COMPONENT_TYPE='P' COMP_EXT='"
                    + XML.SelectSingleNode("COVERAGE_CODE_ID").InnerText + "' COMPONENT_CODE='"
                    + XML.SelectSingleNode("COMPONENT_CODE").InnerText + "' DEDUCTIBLEVALUE='"
                    + strDeductable //XML.SelectSingleNode("DEDUCTIBLE_1").InnerText 
                    + "' COMP_ACT_PRE='" + strPremium //XML.SelectSingleNode("WRITTEN_PREMIUM").InnerText.ToString() 
                    + "'/>";
            }

            //COMPONENT_CODE
            //COMPONENT_CODE='SUMTOTAL'
            return OutXML;

        }

        private string genrate_riskformat_XML(string StepType, double RISK_PREMIUM, int no, NumberFormatInfo NumberFormat)
        {
            string OutXML = string.Empty;



            if (StepType.ToUpper() == "TOTAL_RISK_PREMIUM")
            {
                OutXML = "<STEP TS = '" + no + "' RNO ='1' PRODUCTDESC = '' PRODUCTVALUE = '' GROUPDESC = '' GROUPVAL = '' STEPDESC ='" + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1886","") + "' STEPDEDUCTABLE = ''" +
                    " COMPONENT_CODE='SUMTOTAL' STEPLIMIT ='' STEPPREMIUM  = '" + String.Format(NumberFormat, "{0:0,0.00}", RISK_PREMIUM) + "' LIMITVALUE =''" +
                " COMPONENT_TYPE='P' COMP_ACT_PRE='" + String.Format(NumberFormat, "{0:0,0.00}", RISK_PREMIUM) + "' ></STEP>";
                //.ToString() + NumberFormat.CurrencyDecimalSeparator + "00" +
            }
            else if (StepType.ToUpper() == "FINAL_PREMIUM")
            {
                OutXML = "<STEP TS = 'Final Premium' RNO ='' PRODUCTDESC = '' PRODUCTVALUE = '' GROUPDESC = 'Final Premium' GROUPVAL = 'Final Premium' STEPDESC ='' STEPDEDUCTABLE = ''" +
                    " COMPONENT_CODE='' STEPLIMIT ='' STEPPREMIUM  = '" + String.Format(NumberFormat, "{0:0,0.00}", RISK_PREMIUM) + "' LIMITVALUE ='' COMPONENT_TYPE='P'></STEP>";

            }
            else if (StepType.ToUpper() == "QDATE")
            {
                OutXML = "<QDATE PREDATE = '" + DateTime.Now.ToLongDateString() + "' QEDATE = '"
                    + DateTime.Now.ToLongDateString() + "' REDATE = '"
                    + DateTime.Now.ToLongDateString() + "'></QDATE>";
            }

            return OutXML;

        }
        #endregion

    }
}
