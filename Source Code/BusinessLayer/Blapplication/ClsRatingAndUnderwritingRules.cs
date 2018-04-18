using System;
using System.Text;
using Cms.DataLayer;
using System.Data;
using System.Configuration;
using Cms.Model.Application;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using Cms.Model;
using Cms.BusinessLayer.BlApplication;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Linq;
using System.Xml.Linq;
using System.Collections;
//using Microsoft.Xml;
//using Microsoft.Xml.XQuery;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using AdvRuleParser;
using AdvRuleParser.RuleParserExceptions;
using Cms.Model.Maintenance;
namespace Cms.BusinessLayer.BlApplication
{
    /// <summary>
    /// Summary description for ClsRatingAndUnderwritingRules.
    /// </summary>
    public class ClsRatingAndUnderwritingRules : Cms.BusinessLayer.BlCommon.ClsCommon
    {
        private const string LOB_HOME = "1";
        private const string LOB_PRIVATE_PASSENGER = "2";
        private const string LOB_MOTORCYCLE = "3";
        private const string LOB_WATERCRAFT = "4";
        private const string LOB_UMBRELLA = "5";
        private const string LOB_RENTAL_DWELLING = "6";
        private const string LOB_GENERAL_LIABILITY = "7";
        private const string LOB_AVIATION = "8";
        private const string LOB_MOTOR = "38";

        protected Hashtable RuleKeys;
        protected Hashtable ProductMasterRuleKeys;
        #region Properties

        private int customerID = 0;
        private int PolicyID = 0;
        private int PolicyVersionID = 0;
        private string PolicyLobID = "";
        private string mSystemID;
        public NumberFormatInfo numberFormatInfo;
        public int pCustomerID
        {
            get
            {
                return customerID;
            }
            set
            {
                customerID = value;
            }
        }


        public int pPolicyID
        {
            get
            {
                return PolicyID;
            }
            set
            {
                PolicyID = value;
            }
        }


        public int pPolicyVersionID
        {
            get
            {
                return PolicyVersionID;
            }
            set
            {
                PolicyVersionID = value;
            }
        }
        public string pPolicyLobID
        {
            get
            {
                return PolicyLobID;
            }
            set
            {
                PolicyLobID = value;
            }
        }


        #endregion

        public ClsRatingAndUnderwritingRules(string SystemID)
        {
            RuleKeys = new Hashtable();
            ProductMasterRuleKeys = new Hashtable();
            mSystemID = SystemID;
        }

        public string CheckHTML(int customerID, int appId, int appVersionId)
        {
            DataSet ds = new DataSet();
            string strSPName = "Proc_GetAppVerificationXML";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@APP_ID", appId);
                objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionId);
                ds = objDataWrapper.ExecuteDataSet(strSPName);
                return ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        public string VerifyPolicy(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            string Result = "";

            try
            {

                //Fetch applicable rule collections for this policy
                DataSet lstRuleCollection = GetApplicableRuleCollectionForPolicy(CustomerID, PolicyID, PolicyVersionID);
                if (lstRuleCollection != null && lstRuleCollection.Tables.Count > 0 && lstRuleCollection.Tables[0].Rows.Count > 0)
                {
                    //Fetch Input XML for policy
                    ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
                    string InputXML = objGenInfo.GetPolicyInputXML(CustomerID, PolicyID, PolicyVersionID);

                    string SectionMasterPath = ClsCommon.CmsWebUrl + "support/UnderwritingRules/" + mSystemID + "/" + "SectionMaster.xml";
                    XmlDocument objSection = new XmlDocument();
                    objSection.Load(SectionMasterPath);

                    IUWRuleParser objRuleParser = new UWRuleParser(objSection);
                    RuleResultCollection ResultSet = new RuleResultCollection();
                    //for each rule collection call verify rules method of RuleParser
                    for (int RowCounter = 0; RowCounter < lstRuleCollection.Tables[0].Rows.Count; RowCounter++)
                    {
                        string RuleCollectionPath = lstRuleCollection.Tables[0].Rows[RowCounter]["RULE_XML_PATH"].ToString();
                        string FullPath = ClsCommon.CmsWebUrl + "support/UnderwritingRules/" + mSystemID + "/" + RuleCollectionPath;
                        XmlDocument objRule = new XmlDocument();
                        objRule.Load(FullPath);
                        objRuleParser.VerifyRules(objRule, InputXML, ResultSet);
                        //if validatenext is 0 and  result set has any voilated rule then break

                        ///Do not evaluate next rule collection if violation in current rule check
                        ///

                        if ((bool)lstRuleCollection.Tables[0].Rows[RowCounter]["VALIDATE_NEXT_IF_FAILED"] == false && HasVoilatedRules( ResultSet))
                        {
                            break;
                        }
                    }
                    //Format output to XML and return Result
                    //IRuleResultFormatter objResultFormatter = new RuleResultXMLFormatter();
                    //Result = objResultFormatter.ToXML(ResultSet);
                    if (HasVoilatedRules(ResultSet))
                    {
                        SaveRuleResult(CustomerID, PolicyID, PolicyVersionID, ResultSet);
                    }
                }
            }
            catch (BaseException ex)
            {
                Result = ex.GetType() + " -- " + ex.Message + " .. RuleID = " + ex.RuleID;
            }
            catch (Exception ex)
            {
                Result = ex.GetType() + " -- " + ex.Message;
            }

            return Result;
        }

        public string VerifySection(int CustomerID, int PolicyID, int PolicyVersionID, string SectionID)
        {
            string Result = "";

            try
            {

                //Fetch applicable rule collections for this policy
                DataSet lstRuleCollection = GetApplicableRuleCollectionForPolicySection(CustomerID, PolicyID, PolicyVersionID, SectionID);
                if (lstRuleCollection != null && lstRuleCollection.Tables.Count > 0 && lstRuleCollection.Tables[0].Rows.Count > 0)
                {
                    //Fetch Input XML for policy
                    ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
                    string InputXML = objGenInfo.GetPolicyInputXML(CustomerID, PolicyID, PolicyVersionID);

                    string SectionMasterPath = ClsCommon.CmsWebUrl + "support/UnderwritingRules/" + mSystemID + "/" + "SectionMaster.xml";
                    XmlDocument objSection = new XmlDocument();
                    objSection.Load(SectionMasterPath);

                    IUWRuleParser objRuleParser = new UWRuleParser(objSection);
                    RuleResultCollection ResultSet = new RuleResultCollection();

                    //for each rule collection call verify rules method of RuleParser
                    for (int RowCounter = 0; RowCounter < lstRuleCollection.Tables[0].Rows.Count; RowCounter++)
                    {
                        string RuleCollectionPath = lstRuleCollection.Tables[0].Rows[RowCounter]["RULE_XML_PATH"].ToString();
                        string FullPath = ClsCommon.CmsWebUrl + "support/UnderwritingRules/" + mSystemID + "/" + RuleCollectionPath;
                        XmlDocument objRule = new XmlDocument();
                        objRule.Load(FullPath);
                        objRuleParser.VerifyRules(objRule, InputXML, ResultSet);
                    }

                    //Format output to XML and return Result
                    //IRuleResultFormatter objResultFormatter = new RuleResultXMLFormatter();

                    //Result = objResultFormatter.ToXML(ResultSet);
                    SaveRuleResult(CustomerID, PolicyID, PolicyVersionID, ResultSet);
                }
            }
            catch (BaseException ex)
            {
                Result = ex.GetType() + " -- " + ex.Message + " .. RuleID = " + ex.RuleID;
            }
            catch (Exception ex)
            {
                Result = ex.GetType() + " -- " + ex.Message;
            }

            return Result;
        }

        private bool HasVoilatedRules(RuleResultCollection ResultSet)
        {
            foreach (string Section in ResultSet.Results.Keys)
            {
                Dictionary<PrimaryKey, ArrayList> objResultSet = (Dictionary<PrimaryKey, ArrayList>)ResultSet.Results[Section];
                foreach (PrimaryKey Keys in objResultSet.Keys)
                {

                    ArrayList arrResluts = objResultSet[Keys];
                    for (int ResultIterator = 0; ResultIterator < arrResluts.Count; ResultIterator++)
                    {
                        RuleResult objResult = (RuleResult)arrResluts[ResultIterator];
                        if (objResult.ResultCode)
                        {
                            return true;
                        }
                    }
                }
            }

            return false ;
        }

        private void SaveRuleResult(int CustomerID, int PolicyID, int PolicyVersionID, RuleResultCollection ResultSet)
        {
            DeleteRuleTableData(CustomerID, PolicyID, PolicyVersionID);
            int GroupRowId = 0;
            foreach (string Section in ResultSet.Results.Keys)
            {
                Dictionary<PrimaryKey, ArrayList> objResultSet = (Dictionary<PrimaryKey, ArrayList>)ResultSet.Results[Section];
                foreach (PrimaryKey Keys in objResultSet.Keys)
                {
                    //Create record in POL_UW_RULE_RESULT_RISK_GROUP Table - will return GroupRowId 
                    //Proc_InsertRuleResultRiskGroup
                    GroupRowId = InsertRuleResultRiskGroup(CustomerID, PolicyID, PolicyVersionID);
                    foreach (string ElemantName in Keys.PrimaryColumns.Keys)
                    {
                        //string Key = ElemantName.ToString();
                        //string Value = Keys.PrimaryColumns[ElemantName].ToString();
                        //Create Record in POL_UW_RULE_RESULT_RISK_PK for each iteration groupid will be GroupRowId generated above
                        //Proc_InsertRuleResultPrimaryColumn
                        InsertRuleRiskPk(GroupRowId, ElemantName.ToString(), Keys.PrimaryColumns[ElemantName].ToString());
                    }

                    //Check if this Primary Key combination exists for this policy_id and version_id
                    //If yes add rule against this ID

                    //Else create new record in POL_UW_RULE_RESULT_RISK_GROUP
                    //Add primary keys against this 
                    //and then add rules against this
                    ArrayList arrResluts = objResultSet[Keys];
                    for (int ResultIterator = 0; ResultIterator < arrResluts.Count; ResultIterator++)
                    {
                        RuleResult objResult = (RuleResult)arrResluts[ResultIterator];
                        if (objResult.ResultCode)
                        {
                            try
                            {
                                InsertRuleResultDetails(GroupRowId, objResult.CollectionCode, Convert.ToInt32(objResult.RuleID), Section, objResult.RuleCategory.ToString(), DateTime.Now.Date, Convert.ToInt32(objResult.MessageID));
                            }
                            catch (Exception ex)
                            {
                                objResult.ExceptionObject = ex;
                            }
                        }

                        if (objResult.ExceptionObject != null)
                        {

                        }
                    }
                }
            }
        }


        ///Rule Builder Logic
        ///
        /* Added by Rahul Kumar Dwivedi on 19 dec 2011    */

        private void DeleteRuleTableData(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objDataWrapper.ExecuteNonQuery("Proc_DeleteRuleResultGroups");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        private int InsertRuleResultRiskGroup(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            DataSet ds = null;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                ds = objDataWrapper.ExecuteDataSet("Proc_InsertRuleResultRiskGroup");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        }
        private void InsertRuleRiskPk(int GroupRowID, string KeyName, string KeyValue)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.ClearParameteres();

                objDataWrapper.AddParameter("@REF_GROUP_ROW_ID", GroupRowID);
                objDataWrapper.AddParameter("@KEYNAME", KeyName);
                objDataWrapper.AddParameter("@KEYVALUE", KeyValue);
                objDataWrapper.ExecuteNonQuery("Proc_InsertRuleResultRiskPk");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private void InsertRuleResultDetails(int REF_GROUP_ROW_ID, string RULE_COLLECTION_CODE, int RULE_ID, string SECTION_ID, string RULE_CATAGORY, DateTime VALIDATED_ON, int MESSAGE_ID)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@REF_GROUP_ROW_ID", REF_GROUP_ROW_ID);
                objDataWrapper.AddParameter("@RULE_COLLECTION_CODE", RULE_COLLECTION_CODE);
                objDataWrapper.AddParameter("@RULE_ID", RULE_ID);
                objDataWrapper.AddParameter("@SECTION_ID", SECTION_ID);
                objDataWrapper.AddParameter("@RULE_CATAGORY", RULE_CATAGORY);
                objDataWrapper.AddParameter("@VALIDATED_ON", VALIDATED_ON);
                objDataWrapper.AddParameter("@MESSAGE_ID", MESSAGE_ID);


                objDataWrapper.ExecuteDataSet("Proc_InsertRuleResultDetails");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /*----------------------- */

        private DataSet GetApplicableRuleCollectionForPolicy(int CustomerID, int PolicyID, int PolicyVersionID)
        {

            DataSet ds = null;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                ds = objDataWrapper.ExecuteDataSet("Proc_GetRuleCollectionsForPolicy");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return ds;
        }


        private DataSet GetApplicableRuleCollectionForPolicySection(int CustomerID, int PolicyID, int PolicyVersionID, string SectionID)
        {

            DataSet ds = null;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objDataWrapper.AddParameter("@SECTION_ID", SectionID);

                ds = objDataWrapper.ExecuteDataSet("Proc_GetRuleCollectionsForPolicySection");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return ds;
        }

        /// <summary>
        /// Submit Anyway Application To Policy
        /// </summary>
        /// <param name="PolicyLobID">LOB_ID as strinng</param>
        /// <param name="strCSSNo">CSS No.</param>
        /// <param name="validApp">out variable, Valid or not</param>
        /// <returns>strInputXML</returns>
        /// Added by Charles on 3-Jun-10 for Itrack 20
        public string SubmitAnywayPolVerify(int CustomerID, int PolicyID, int PolicyVersionID, string PolicyLobID, string strCSSNo, out bool validApp)
        {
            validApp = false;
            string strInputXML = "";
            bool validXML = false;

            pCustomerID = CustomerID;
            pPolicyID = PolicyID;
            pPolicyVersionID = PolicyVersionID;
            pPolicyLobID = PolicyLobID;

            try
            {
                strInputXML = GetRuleInputXML_Pol();
                if (int.Parse(PolicyLobID) > 8)
                {
                    validXML = ValidateProductInputRuleXML(ref strInputXML, PolicyLobID);
                    strInputXML = strInputXML.Replace("</INPUTXML>", "<CSSNUM CSSVALUE =' " + strCSSNo + " '/></INPUTXML>");
                }
                else
                {
                    validXML = ValidInputXML(strInputXML, PolicyLobID);
                    strInputXML = strInputXML.Replace("</INPUTXML>", "<CSSNUM CSSVALUE =' " + strCSSNo + " '/></INPUTXML>");
                }

                if (validXML)
                {
                    validApp = true;
                    //Added by Lalit April 30,2011.i - track #962
                    string strRulesStatus = "0";
                    strInputXML = strInputXML.Replace("\r\n", "");
                    strInputXML = this.FinalHtml_Pol(customerID, PolicyID, PolicyVersionID, strInputXML, PolicyLobID, out validApp, out strRulesStatus);


                }
                else
                {
                    strInputXML = InputMessagesXML(PolicyLobID, strInputXML);
                }

                return strInputXML;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }

        public string SubmitAnywayAppVerify(int customerID, int appID, int appVersionID, string appLobID, string strCSSNo, out int validApp, string strCalled)
        {
            validApp = 0;
            string strRulePath = "", strInputXML = "";
            try
            {
                ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();
                string returnString = "";
                //get the input xml for different lobs
                if (appLobID != "0")
                {
                    bool validXML = false;
                    //get the input xml against the application
                    objGeneralInfo.pCustomerID = customerID;
                    objGeneralInfo.pAppID = appID;
                    objGeneralInfo.pAppVersionID = appVersionID;
                    strInputXML = objGeneralInfo.GetRuleInputXML(appLobID, strCalled);
                    //check if the inputxml is valid
                    validXML = ValidInputXML(strInputXML, appLobID);
                    strInputXML = strInputXML.Replace("</INPUTXML>", "<CSSNUM CSSVALUE =' " + strCSSNo + " '/></INPUTXML>");
                    if (validXML)
                    {
                        validApp = 1;
                    }
                    else
                    {
                        // switch case on the basis of the lob to get the inputmessage xsl
                        switch (appLobID)
                        {
                            case LOB_HOME:
                                //strRulePath= System.Configuration.ConfigurationSettings.AppSettings.Get("FilePathInputMessageXSLHO").ToString();
                                strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLHO");
                                break;
                            case LOB_PRIVATE_PASSENGER:
                                //strRulePath= System.Configuration.ConfigurationSettings.AppSettings.Get("FilePathInputMessageXSLAuto").ToString();
                                strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLAuto");
                                break;
                            case LOB_MOTORCYCLE:
                                //strRulePath= System.Configuration.ConfigurationSettings.AppSettings.Get("FilePathInputMessageXSLMotor").ToString();
                                strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLMotor");
                                break;
                            case LOB_WATERCRAFT:
                                //strRulePath= System.Configuration.ConfigurationSettings.AppSettings.Get("FilePathInputMessageXSLWater").ToString();
                                strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLWater");
                                break;
                            case LOB_RENTAL_DWELLING:
                                //strRulePath= System.Configuration.ConfigurationSettings.AppSettings.Get("FilePathInputMessageXSLRental").ToString();
                                strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLRental");
                                break;
                            case LOB_UMBRELLA:
                                //strRulePath= System.Configuration.ConfigurationSettings.AppSettings.Get("FilePathInputMessageXSLUmbrella").ToString();
                                strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLUmbrella");
                                break;
                            case LOB_GENERAL_LIABILITY:
                                //strRulePath= System.Configuration.ConfigurationSettings.AppSettings.Get("FilePathInputMessageXSLGenLiability").ToString();
                                strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLGenLiability");
                                break;
                            default:
                                strRulePath = "";
                                break;
                        }

                        //Transform and show the message for invalid inputs
                        XslTransform xslt = new XslTransform();
                        xslt.Load(strRulePath);
                        StringWriter writer = new StringWriter();
                        XmlDocument xmlDocTemp = new XmlDocument();
                        xmlDocTemp.LoadXml(strInputXML);
                        XPathNavigator nav = ((IXPathNavigable)xmlDocTemp).CreateNavigator();
                        xslt.Transform(nav, null, writer);
                        returnString = writer.ToString();
                    }
                }
                return returnString;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        public string VerifyPolicy(int CustomerID, int PolicyID, int PolicyVersionID, string PolicyLobID, out bool validXML, string strCSSNo, out string strRulesStatus)
        {
            return VerifyPolicy(CustomerID, PolicyID, PolicyVersionID, PolicyLobID, out  validXML, strCSSNo, out strRulesStatus, "");
        }
        public string VerifyPolicy(int CustomerID, int PolicyID, int PolicyVersionID, string PolicyLobID, out bool validXML, string strCSSNo, out string strRulesStatus, string CalledFrom)
        {
            string strInputXML = "";
            strRulesStatus = "0";
            string strRuleHTML = "";
            validXML = Convert.ToBoolean(0);
            string POLICY_STATUS = string.Empty;
            string Language_code = enumCulture.US;
            try
            {
                //get the input xml for different lobs
                if (PolicyLobID != "0")
                {
                    ClsGeneralInformation objinformation = new ClsGeneralInformation();
                    //get the input xml against the application
                    pCustomerID = CustomerID;
                    pPolicyID = PolicyID;
                    pPolicyVersionID = PolicyVersionID;
                    pPolicyLobID = PolicyLobID;
                    // check wheter policy is already verified is yes fetch saved Rule XML
                    strInputXML = GetSavedRulesXML_Policy(CustomerID, PolicyID, PolicyVersionID, out strRuleHTML);
                    if (strInputXML == "")
                    {


                        strInputXML = GetRuleInputXML_Pol();
                        POLICY_STATUS = ClsCommon.FetchValueFromXML("POLICY_STATUS", strInputXML).ToString().ToUpper();
                        numberFormatInfo = new CultureInfo(Language_code, true).NumberFormat;

                        if (int.Parse(PolicyLobID) > 8) // Implementation for new Products
                        {
                            if (POLICY_STATUS == POLICY_STATUS_SUSPENDED || POLICY_STATUS == POLICY_STATUS_UNDER_ISSUE)
                                validXML = ValidateProductInputRuleXML(ref strInputXML, PolicyLobID, MainRuleType.ProcessDependent, SubRuleType.All);
                            else if (POLICY_STATUS == POLICY_STATUS_UNDER_ENDORSEMENT || POLICY_STATUS.Contains("END") || POLICY_STATUS == POLICY_STATUS_ENDORSEMENT_SUSPENSE)
                                validXML = ValidateProductInputRuleXML(ref strInputXML, PolicyLobID, MainRuleType.ProcessDependent, SubRuleType.Endorsement);
                            else if (POLICY_STATUS.Contains("REN") || POLICY_STATUS == POLICY_STATUS_RENEWED)
                                validXML = ValidateProductInputRuleXML(ref strInputXML, PolicyLobID, MainRuleType.ProcessDependent, SubRuleType.Renewal);
                            else if (POLICY_STATUS.Contains("USER") || POLICY_STATUS == POLICY_STATUS_CORRECTIVE_USER)
                                validXML = ValidateProductInputRuleXML(ref strInputXML, PolicyLobID, MainRuleType.ProcessDependent, SubRuleType.CorrectiveUser);
                            else
                                validXML = ValidateProductInputRuleXML(ref strInputXML, PolicyLobID);

                            strInputXML = strInputXML.Replace("</INPUTXML>", "<CSSNUM CSSVALUE =' " + strCSSNo + " '/></INPUTXML>");
                            if (PolicyLobID == LOB_MOTOR && POLICY_STATUS == "URENEW")
                            {
                                validXML = true;
                            }
                            if (validXML)
                            {
                                strInputXML = ReturnRulesMsg_Pol(pCustomerID, pPolicyID, pPolicyVersionID, PolicyLobID, strCSSNo, out validXML, strInputXML, out strRulesStatus);
                            }
                            else
                                strInputXML = InputMessagesXML(PolicyLobID, strInputXML);
                            return strInputXML;
                        }
                        strInputXML = objinformation.SetForbiddenRenewalRefferalRule(CustomerID.ToString(), PolicyID.ToString(), PolicyVersionID.ToString(), strInputXML);

                        //Added by Charles on 21-Apr-10 for Policy Page
                        if (strInputXML != null && strInputXML != "")
                        {
                            //check if the inputxml is valid
                            validXML = ValidInputXML(strInputXML, PolicyLobID);
                            strInputXML = strInputXML.Replace("</INPUTXML>", "<CSSNUM CSSVALUE =' " + strCSSNo + " '/></INPUTXML>");
                            // implementation of rules at policy level		
                            if (validXML)
                            {
                                //strInputXML=objinformation.SetForbiddenRenewalRefferalRule(CustomerID.ToString(),PolicyID.ToString(),PolicyVersionID.ToString(),strInputXML);
                                strInputXML = ReturnRulesMsg_Pol(pCustomerID, pPolicyID, pPolicyVersionID, PolicyLobID, strCSSNo, out validXML, strInputXML, out strRulesStatus);
                            }
                            else
                                strInputXML = InputMessagesXML(PolicyLobID, strInputXML);
                        }
                        else
                        {
                            validXML = true;
                        }

                    }
                    else
                    {
                        validXML = ValidInputXML(strInputXML, PolicyLobID);
                        getRuleStatus(strRuleHTML, out validXML, out strRulesStatus);
                        strInputXML = strRuleHTML.Substring(0, strRuleHTML.IndexOf("cms/cmsweb/css/css")) + "cms/cmsweb/css/css" + strCSSNo + strRuleHTML.Substring(strRuleHTML.IndexOf("cms/cmsweb/css/css") + 19);
                    }
                }
                return strInputXML;
            }
            catch (Exception objExp)
            {
                throw (objExp);
            }
        }

        // Added by Mohit Agarwal
        // 18-Jan-2007
        public string VerifyPolicyMandatory(int CustomerID, int PolicyID, int PolicyVersionID, string PolicyLobID, out bool validXML, string strCSSNo, out string strRulesStatus)
        {
            string strInputXML = "";
            strRulesStatus = "0";
            try
            {
                //get the input xml for different lobs
                if (PolicyLobID != "0")
                {
                    //get the input xml against the application
                    pCustomerID = CustomerID;
                    pPolicyID = PolicyID;
                    pPolicyVersionID = PolicyVersionID;
                    pPolicyLobID = PolicyLobID;
                    strInputXML = GetRuleInputXML_Pol();
                }
                //check if the inputxml is valid
                validXML = ValidInputXML(strInputXML, PolicyLobID);
                strInputXML = strInputXML.Replace("</INPUTXML>", "<CSSNUM CSSVALUE =' " + strCSSNo + " '/></INPUTXML>");
                // implementation of rules at policy level		
                //				if(validXML)
                //					strInputXML=ReturnRulesMsg_Pol(pCustomerID,pPolicyID,pPolicyVersionID,PolicyLobID,strCSSNo,out validXML,strInputXML,out strRulesStatus);
                //				else
                strInputXML = InputMessagesXML(PolicyLobID, strInputXML);
                return strInputXML;
            }
            catch (Exception objExp)
            {
                throw (objExp);
            }
        }

        public string VerifyApplication(int customerID, int appID, int appVersionID, string appLobID, string strCSSNo, out int validApp)
        {
            #region		VERIFY APPLICATION
            string strInputXML = "";
            validApp = 0;
            string strCalled = "SUBMIT", strRuleHTML = "";
            try
            {
                ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();
                string returnString = "";
                //get the input xml for different lobs
                if (appLobID != "0")
                {
                    bool validXML = false;
                    //get the input xml against the application
                    objGeneralInfo.pCustomerID = customerID;
                    objGeneralInfo.pAppID = appID;
                    objGeneralInfo.pAppVersionID = appVersionID;
                    strInputXML = GetSavedRulesXML_APP(customerID, appID, appVersionID, out strRuleHTML, out validApp);
                    if (strInputXML == "")
                    {
                        strInputXML = objGeneralInfo.GetRuleInputXML(appLobID, strCalled);
                        //check if the inputxml is valid
                        validXML = ValidInputXML(strInputXML, appLobID);
                        strInputXML = strInputXML.Replace("</INPUTXML>", "<CSSNUM CSSVALUE =' " + strCSSNo + " '/></INPUTXML>");
                        if (validXML)
                        {
                            returnString = ReturnRulesMsg(customerID, appID, appVersionID, appLobID, strCSSNo, out validApp, strInputXML);
                        }
                        else
                        {
                            returnString = InputMessagesXML(appLobID, strInputXML);
                            //UpdateQuote(customerID,appID,appVersionID);
                        }
                    }
                    else
                    {
                        validXML = ValidInputXML(strInputXML, appLobID);
                        strRuleHTML = strRuleHTML.Substring(0, strRuleHTML.IndexOf("cms/cmsweb/css/css")) + "cms/cmsweb/css/css" + strCSSNo + strRuleHTML.Substring(strRuleHTML.IndexOf("cms/cmsweb/css/css") + 19);
                        returnString = strRuleHTML;
                    }
                }
                return returnString;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
            # endregion

        }
        private void UpdateQuote(int CustomerID, int AppID, int AppVersionID)
        {
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID", AppID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionID, SqlDbType.Int);
                objDataWrapper.AddParameter("@SHOW_QUOTE", "0", SqlDbType.NChar);

                objDataWrapper.ExecuteNonQuery("Proc_UpdateApplicationQuoteForRules");


            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }
        private void getRuleStatus(string strRuleInputHTML, out bool validXML, out string strRulesStatus)
        {
            validXML = Convert.ToBoolean(0);
            strRulesStatus = "0";
            if (strRuleInputHTML.Trim() != "")
            {
                /* if the rule HTML has a returnvalue =0 then validApp=0 and strHTML=InputXML
                    *  =1 then 	validApp=1 and strHTML=transformed RuleHTML 
                 */
                XmlDocument xmlDocOutput = new XmlDocument();
                strRuleInputHTML = strRuleInputHTML.Replace("\t", "");
                strRuleInputHTML = strRuleInputHTML.Replace("\r\n", "");
                strRuleInputHTML = strRuleInputHTML.Replace("<LINK", "<!-- <LINK");
                strRuleInputHTML = strRuleInputHTML.Replace(" rel=\"stylesheet\"> ", "rel=\"stylesheet/\"> -->");
                strRuleInputHTML = strRuleInputHTML.Replace("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-16\">", "");
                xmlDocOutput.LoadXml("<RULEHTML>" + strRuleInputHTML + "</RULEHTML>");
                XmlNodeList nodLst = xmlDocOutput.GetElementsByTagName("returnValue");
                // chk here for all verified rules
                XmlNodeList nodLstAll = xmlDocOutput.GetElementsByTagName("verifyStatus");
                if (nodLstAll != null && nodLstAll.Count > 0)
                {
                    strRulesStatus = nodLstAll.Item(0).InnerText;
                }
                //chk for rejected rules
                if (nodLst != null && nodLst.Count > 0)
                {
                    string validPolicy = nodLst.Item(0).InnerText; //1 or 0
                    if (validPolicy.Trim().Equals("0"))
                        validXML = Convert.ToBoolean(0);
                    else
                        validXML = Convert.ToBoolean(1);
                }
            }
        }
        public string GetSavedRulesXML_Policy(int CustomerID, int PolicyID, int PolicyVersionID, out string strRuleHTML)
        {
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID, SqlDbType.Int);

                DataSet dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetSavedRuleXml_Pol");

                strRuleHTML = dsTemp.Tables[0].Rows[0]["APP_VERIFICATION_XML"].ToString();

                if (dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
                    return dsTemp.Tables[0].Rows[0]["RULE_INPUT_XML"].ToString();
                else
                    return "";

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }
        public string GetSavedRulesXML_APP(int CustomerID, int AppID, int AppVersionID, out string strRuleHTML, out int validApp)
        {
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID", AppID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionID, SqlDbType.Int);

                DataSet dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetSavedRuleXml_APP");

                strRuleHTML = dsTemp.Tables[0].Rows[0]["APP_VERIFICATION_XML"].ToString();
                if (dsTemp.Tables[0].Rows[0]["SHOW_QUOTE"].ToString() == "1")
                    validApp = 1;
                else
                    validApp = 0;
                if (dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
                    return dsTemp.Tables[0].Rows[0]["RULE_INPUT_XML"].ToString();
                else
                    return "";

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }
        public string GetRulesVerification(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POL_ID", PolicyID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POL_VERSION_ID", PolicyVersionID, SqlDbType.Int);

                DataSet dsTemp = objDataWrapper.ExecuteDataSet("PROC_Pol_VerificationXml");
                if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0 && dsTemp.Tables[0].Rows[0]["APP_VERIFICATION_XML"] != "")
                    return dsTemp.Tables[0].Rows[0]["APP_VERIFICATION_XML"].ToString();
                else
                    return "";


            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }
        private string ReturnRulesMsg_Pol(int customerID, int PolicyID, int PolicyVersionID, string polLobID, string strCSSNo, out bool validpol, string strInputXML, out string strRulesStatus)
        {
            strRulesStatus = "0";
            strInputXML = strInputXML.Replace("\r\n", "");
            //get the INPUT XML
            string strVerificationXML = "";
            // strOldInputXML="",strShowQuote="" ,

            //			DataSet ds =OldInputXML_Pol(customerID,PolicyID,PolicyVersionID);
            //			if(ds.Tables[0].Rows.Count>0)
            //			{
            //				strOldInputXML=ds.Tables[0].Rows[0]["RULE_INPUT_XML"].ToString();
            //				strVerificationXML=ds.Tables[0].Rows[0]["APP_VERIFICATION_XML"].ToString();
            //				strShowQuote=ds.Tables[0].Rows[0]["SHOW_QUOTE"].ToString();
            //			}			
            //			if(strOldInputXML!=strInputXML)
            //			{
            //if input xml is valid then get the final html.
            strVerificationXML = this.FinalHtml_Pol(customerID, PolicyID, PolicyVersionID, strInputXML, polLobID, out validpol, out strRulesStatus);
            //			}
            //			else 
            //				if(strShowQuote =="1")
            //					validpol=Convert.ToBoolean(1);
            //				else
            //				validpol=Convert.ToBoolean(0);
            return strVerificationXML;
        }

        // returns rules message at App level

        private string ReturnRulesMsg(int customerID, int appID, int appVersionID, string appLobID, string strCSSNo, out int validApp, string strInputXML)
        {
            strInputXML = strInputXML.Replace("\r\n", "");
            //get the INPUT XML
            string strOldInputXML = "", strShowQuote = "", returnString = "";
            DataSet ds = OldInputXML(customerID, appID, appVersionID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                strOldInputXML = ds.Tables[0].Rows[0]["RULE_INPUT_XML"].ToString();
                returnString = ds.Tables[0].Rows[0]["APP_VERIFICATION_XML"].ToString();
                strShowQuote = ds.Tables[0].Rows[0]["SHOW_QUOTE"].ToString();
            }
            if (strOldInputXML != strInputXML)
            {
                //if input xml is valid then get the final html.
                returnString = this.FinalHtml(customerID, appID, appVersionID, strInputXML, appLobID, out validApp);
            }
            else
                if (strShowQuote == "1")
                    validApp = 1;
                else
                    validApp = 0;
            return returnString;
        }

        // Return the messages 
        public string InputMessagesXML(string appLobID, string strInputXML)
        {
            string strRulePath = "", returnString = "";
            // switch case on the basis of the lob to get the inputmessage xsl

            string msgFilepath = ClsCommon.GetKeyValueWithIP("FilePathRuleMessages");

            if (ClsCommon.BL_LANG_CULTURE == enumCulture.BR)
            {
                msgFilepath = msgFilepath.Replace(".xml", "." + ClsCommon.BL_LANG_CULTURE + ".xml");
            }


            strInputXML = strInputXML.Replace("<INPUTXML>", "<INPUTXML>" + "<MESSAGE_FILE_PATH>" + msgFilepath + "</MESSAGE_FILE_PATH>");

            switch (appLobID)
            {
                case LOB_HOME:
                    strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLHO");
                    break;
                case LOB_PRIVATE_PASSENGER:
                    strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLAuto");
                    break;
                case LOB_MOTORCYCLE:
                    strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLMotor");
                    break;
                case LOB_WATERCRAFT:
                    strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLWater");
                    break;
                case LOB_RENTAL_DWELLING:
                    strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLRental");
                    break;
                case LOB_UMBRELLA:
                    strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLUmbrella");
                    break;
                case LOB_GENERAL_LIABILITY:
                    strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLGenLiability");
                    break;
                case LOB_AVIATION:
                    strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLAviation");
                    break;
                default://Added by Charles on 21-Apr-10
                    strRulePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLProducts");
                    break;
            }

            //Transform and show the message for invalid inputs
            XslTransform xslt = new XslTransform();
            xslt.Load(strRulePath);
            StringWriter writer = new StringWriter();
            XmlDocument xmlDocTemp = new XmlDocument();
            xmlDocTemp.LoadXml(strInputXML);
            XPathNavigator nav = ((IXPathNavigable)xmlDocTemp).CreateNavigator();
            xslt.Transform(nav, null, writer);
            returnString = writer.ToString();
            return returnString;

        }
        // get old xml at pol level
        public DataSet OldInputXML_Pol(int customerID, int polID, int polVersionID)
        {
            string strOldInputXML = "", strStoredProcName = "Proc_PolOldInputXML";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@POLICY_ID", polID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", polVersionID);
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

        public DataSet OldInputXML(int customerID, int appID, int appVersionID)
        {
            string strOldInputXML = "", strStoredProcName = "Proc_AppOldInputXML";
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

        // at policy level
        private string FinalHtml_Pol(int customerID, int polID, int polVersionID, string strInputXML, string LOBID, out bool validpol, out string strRulesStatus)
        {
            try
            {

                string strRulePath = "", returnString = "";
                // switch case on the basis of the lob to get the inputmessage xsl

                string msgFilepath = ClsCommon.GetKeyValueWithIP("FilePathRuleMessages");

                if (ClsCommon.BL_LANG_CULTURE == enumCulture.BR)
                {
                    msgFilepath = msgFilepath.Replace(".xml", "." + ClsCommon.BL_LANG_CULTURE + ".xml");
                }


                strInputXML = strInputXML.Replace("<INPUTXML>", "<INPUTXML>" + "<MESSAGE_FILE_PATH>" + msgFilepath + "</MESSAGE_FILE_PATH>");

                string strHTML = "";
                string xslFilePath = "";
                validpol = Convert.ToBoolean(0);
                strRulesStatus = "0";
                // Load the Rule InputXML
                XmlDocument xmlDocInput = new XmlDocument();
                xmlDocInput.LoadXml(strInputXML);
                //Transform the Rule Input XML to generate the premium
                switch (LOBID)
                {
                    case LOB_HOME:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathUnderwritingRulesHO");
                        break;
                    case LOB_PRIVATE_PASSENGER:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathUnderwritingRulesAuto");
                        break;
                    case LOB_MOTORCYCLE:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathUnderwritingRulesMotor");
                        break;
                    case LOB_WATERCRAFT:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathUnderwritingRulesWater");
                        break;
                    case LOB_RENTAL_DWELLING:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathUnderwritingRulesRental");
                        break;
                    case LOB_UMBRELLA:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathInputRulesXSLUmbrella");
                        break;
                    case LOB_GENERAL_LIABILITY:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLGenLiability");
                        break;
                    case LOB_AVIATION:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathUnderwritingRulesAviation");
                        break;
                    default://Added by Charles on 21-Apr-10
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathUnderwritingRulesProducts");
                        break;
                }

                XslTransform trnsTemp = new XslTransform();
                trnsTemp.Load(xslFilePath);
                XPathNavigator nav = ((IXPathNavigable)xmlDocInput).CreateNavigator();
                StringWriter swGetHTML = new StringWriter();
                trnsTemp.Transform(nav, null, swGetHTML);
                string ruleHTML = swGetHTML.ToString();
                strHTML = ruleHTML;
                string IS_UNDER_REVIEW = "";
                if (ruleHTML.Trim() != "")
                {
                    /* if the rule HTML has a returnvalue =0 then validApp=0 and strHTML=InputXML
                    *  =1 then 	validApp=1 and strHTML=transformed RuleHTML 
                    * save the HTML in the table app_list update app_list ..set show_quote=1  */
                    XmlDocument xmlDocOutput = new XmlDocument();
                    ruleHTML = ruleHTML.Replace("\t", "");
                    ruleHTML = ruleHTML.Replace("\r\n", "");
                    ruleHTML = ruleHTML.Replace("<LINK", "<!-- <LINK");
                    ruleHTML = ruleHTML.Replace(" rel=\"stylesheet\"> ", "rel=\"stylesheet/\"> -->");
                    ruleHTML = ruleHTML.Replace("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-16\">", "");
                    xmlDocOutput.LoadXml("<RULEHTML>" + ruleHTML + "</RULEHTML>");
                    XmlNodeList nodLst = xmlDocOutput.GetElementsByTagName("returnValue");
                    // chk here for all verified rules
                    XmlNodeList nodLstAll = xmlDocOutput.GetElementsByTagName("verifyStatus");
                    XmlNodeList objXmlNodeReffList = xmlDocOutput.GetElementsByTagName("ReferedStatus");
                    if (nodLst.Item(0).InnerText == "0")
                        IS_UNDER_REVIEW = "2"; //Reject 

                    else if (objXmlNodeReffList.Item(0).InnerText == "0" && LOBID != "1" )
                        IS_UNDER_REVIEW = "1"; //Reffered
                    else
                        IS_UNDER_REVIEW = "0"; //All data verified

                    if (nodLstAll != null && nodLstAll.Count > 0)
                    {
                        strRulesStatus = nodLstAll.Item(0).InnerText;
                    }
                    //chk for rejected rules
                    if (nodLst != null && nodLst.Count > 0)
                    {

                        string validPolicy = nodLst.Item(0).InnerText; //1 or 0
                        
                        if (validPolicy.Trim().Equals("0"))
                        {
                            //IF VERIFICATION OF  POL FAILS THEN UPDATE SHOW_QUOTE FLAG IN APP_LIST TABLE WITH 0
                            validpol = Convert.ToBoolean(0);
                            UpdateVerXML_Pol(customerID, polID, polVersionID, "", "", Convert.ToInt32(validpol), IS_UNDER_REVIEW);
                        }
                        else
                        {
                            validpol = Convert.ToBoolean(1);
                            //save the html in table and update show_quote=1;							
                            UpdateVerXML_Pol(customerID, polID, polVersionID, strHTML, strInputXML, Convert.ToInt32(validpol), IS_UNDER_REVIEW);
                        }

                    }
                }
                return strHTML;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        private string FinalHtml(int customerID, int appID, int appVersionID, string strInputXML, string LOBID, out int validApp)
        {
            try
            {
                string returnString = "";
                string xslFilePath = "";
                validApp = 0;
                // Load the Rule InputXML
                XmlDocument xmlDocInput = new XmlDocument();
                xmlDocInput.LoadXml(strInputXML);
                //
                //XmlNode

                //Transform the Rule Input XML to generate the premium
                switch (LOBID)
                {
                    case LOB_HOME:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathUnderwritingRulesHO");
                        break;
                    case LOB_PRIVATE_PASSENGER:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathUnderwritingRulesAuto");
                        break;
                    case LOB_MOTORCYCLE:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathUnderwritingRulesMotor");
                        break;
                    case LOB_WATERCRAFT:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathUnderwritingRulesWater");
                        break;
                    case LOB_RENTAL_DWELLING:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathUnderwritingRulesRental");
                        break;
                    case LOB_UMBRELLA:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathInputRulesXSLUmbrella");
                        break;
                    case LOB_GENERAL_LIABILITY:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathInputMessageXSLGenLiability");
                        break;
                    case LOB_AVIATION:
                        xslFilePath = ClsCommon.GetKeyValueWithIP("FilePathUnderwritingRulesAviation");
                        break;
                    default:
                        xslFilePath = "";
                        break;
                }

                XslTransform trnsTemp = new XslTransform();
                trnsTemp.Load(xslFilePath);
                XPathNavigator nav = ((IXPathNavigable)xmlDocInput).CreateNavigator();
                StringWriter swGetHTML = new StringWriter();
                trnsTemp.Transform(nav, null, swGetHTML);
                string ruleHTML = swGetHTML.ToString();
                returnString = ruleHTML;
                if (ruleHTML.Trim() != "")
                {
                    /* if the rule HTML has a returnvalue
                    * 	=0 then validApp=0 and returnString=InputXML
                    *  =1 then 
                    *			validApp=1 and returnString=transformed RuleHTML 
                    *			save the HTML in the table app_list
                    *			update app_list ..set show_quote=1  */
                    //string transformedXml	=	"<root>" + ruleHTML.Replace("\\\"","\"") + "</root>";
                    XmlDocument xmlDocOutput = new XmlDocument();
                    ruleHTML = ruleHTML.Replace("\t", "");
                    ruleHTML = ruleHTML.Replace("\r\n", "");
                    ruleHTML = ruleHTML.Replace("<LINK", "<!-- <LINK");
                    ruleHTML = ruleHTML.Replace(" rel=\"stylesheet\"> ", "rel=\"stylesheet/\"> -->");
                    ruleHTML = ruleHTML.Replace("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-16\">", "");
                    xmlDocOutput.LoadXml("<RULEHTML>" + ruleHTML + "</RULEHTML>");
                    XmlNodeList nodLst = xmlDocOutput.GetElementsByTagName("returnValue");
                    if (nodLst != null && nodLst.Count > 0)
                    {
                        string validApplication = nodLst.Item(0).InnerText; //1 or 0
                        if (validApplication.Trim().Equals("0"))
                        {
                            //RP -- 31 July 2006 - Gen Issue 3181
                            //IF VERIFICATION OF  APP FAILS THEN UPDATE SHOW_QUOTE FLAG IN APP_LIST TABLE WITH 0
                            validApp = 0;
                            StringExists(customerID, appID, appVersionID, "", "", validApp);
                        }
                        else
                        {
                            validApp = 1;
                            //save the html in table and update show_quote=1;
                            StringExists(customerID, appID, appVersionID, returnString, strInputXML, validApp);
                        }
                    }
                }
                return returnString;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        //This functions save the Verified XML 
        private void StringExists(int customerID, int appID, int appVersionID, string returnString, string strInputXML, int validApp)
        {
            string strVerificationXML, strStoredProcVerifyXML = "Proc_AppVerificationXML",
                strStoredProcForApplication = "Proc_UpdateApplicationForRules";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@APP_ID", appID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
                ds = objDataWrapper.ExecuteDataSet(strStoredProcVerifyXML);
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                objDataWrapper.ClearParameteres();
                strVerificationXML = ds.Tables[0].Rows[0][0].ToString();
                // if both the strings are not same or null then save otherwise not
                if (strVerificationXML != returnString || strVerificationXML == "")
                {
                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                    objDataWrapper.AddParameter("@APP_ID", appID);
                    objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
                    objDataWrapper.AddParameter("@SHOW_QUOTE", validApp);
                    objDataWrapper.AddParameter("@APP_VERIFICATION_XML", returnString);
                    objDataWrapper.AddParameter("@RULE_INPUT_XML", strInputXML);
                    objDataWrapper.ExecuteNonQuery(strStoredProcForApplication);
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                    objDataWrapper.ClearParameteres();
                }
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        //This is function for Updating Rule_Verification table in app_list table

        public void UpdateRuleVerification(int CustomerID, int AppID, int AppVersionID, int RulesVerification)
        {
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID", AppID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionID, SqlDbType.SmallInt);
                objDataWrapper.AddParameter("@RULE_VERIFICATION", RulesVerification, SqlDbType.SmallInt);

                objDataWrapper.ExecuteNonQuery("Proc_UpdateAppRuleVerification");


            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }


        //This functions save the Verified XML at pol level 
        private void UpdateVerXML_Pol(int customerID, int polID, int polVersionID, string strVerHTML, string strInputXML, int validPol, string IS_UNDER_REVIEW)
        {
            string strExistingVerHTML, strStoredProcVerifyXML = "PROC_Pol_VerificationXml",
                strStoredProcForApplication = "Proc_UpdatePolForRules";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@POL_ID", polID);
                objDataWrapper.AddParameter("@POL_VERSION_ID", polVersionID);
                ds = objDataWrapper.ExecuteDataSet(strStoredProcVerifyXML);
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                objDataWrapper.ClearParameteres();
                strExistingVerHTML = ds.Tables[0].Rows[0][0].ToString();
                // if both the strings are not same or null then save otherwise not
                if (strExistingVerHTML != strVerHTML || strExistingVerHTML == "")
                {
                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                    objDataWrapper.AddParameter("@POL_ID", polID);
                    objDataWrapper.AddParameter("@POL_VERSION_ID", polVersionID);
                    objDataWrapper.AddParameter("@SHOW_QUOTE", validPol);
                    objDataWrapper.AddParameter("@APP_VERIFICATION_XML", strVerHTML);
                    objDataWrapper.AddParameter("@RULE_INPUT_XML", strInputXML);
                    objDataWrapper.AddParameter("@IS_UNDER_REVIEW", IS_UNDER_REVIEW);
                    objDataWrapper.ExecuteNonQuery(strStoredProcForApplication);
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                    objDataWrapper.ClearParameteres();
                }
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        private bool ValidInputXML(string inputXML, string LOBID)
        {
            try
            {
                bool retVal = true;
                XmlDocument tempDoc = new XmlDocument();
                tempDoc.LoadXml(inputXML);
                XmlElement tempElement = tempDoc.DocumentElement;
                XmlNodeList tempNodes = tempElement.ChildNodes;
                switch (LOBID)
                {
                    case LOB_HOME:
                        // if primary heat type is Wood Stove 

                        XmlNode nodTemp = tempElement.SelectSingleNode("//SECONDARY_HEAT_TYPE");
                        string strSECONDARY_HEAT_TYPE = "";
                        if (nodTemp != null)
                            strSECONDARY_HEAT_TYPE = nodTemp.InnerXml.ToString();
                        else
                            strSECONDARY_HEAT_TYPE = "N";

                        foreach (XmlNode nodTempNode in tempNodes)
                        {
                            foreach (XmlNode nodTempChild in nodTempNode.ChildNodes)
                            {
                                foreach (XmlNode nodTempInnerChild in nodTempChild.ChildNodes)
                                {
                                    if (nodTempInnerChild.InnerText.Trim() == "") //|| nodTempInnerChild.InnerText.Trim()=="NULL")
                                    {

                                        if ((nodTempInnerChild.OuterXml == "<FUELERROR ERRFOUND=\"T\" />" && strSECONDARY_HEAT_TYPE != "Y") || (nodTempInnerChild.OuterXml == "<BOATERROR ERRFOUND=\"T\" />") || (nodTempInnerChild.OuterXml == "<OPERATORERROR ERRFOUND=\"T\" />") || (nodTempInnerChild.OuterXml == "<WATERCRAFTGENINFOERROR ERRFOUND=\"T\" />"))
                                        {
                                            retVal = true;
                                            break;
                                        }
                                        else
                                        {
                                            return false;
                                            //retVal =false;
                                            //break;
                                        }
                                    }
                                    foreach (XmlNode nodTempInnerChildChild in nodTempInnerChild.ChildNodes)
                                    {

                                        if (nodTempInnerChildChild.InnerText.Trim() == "") //|| nodTempInnerChildChild.InnerText.Trim()=="NULL")
                                        {
                                            //retVal =false;
                                            //break;
                                            return false;
                                        }

                                    }
                                }
                            }
                        }

                        // check here for watercraft case only 
                        // if  count >0 for any one of Boat,operator,WatercraftGeninfo
                        // Boat
                        int intBoatCount = 0;
                        int intOperatorCount = 0;
                        int intWCUQCount = 0;
                        XmlAttributeCollection xmlCountAtts = null;

                        if (tempElement.SelectSingleNode("//BOATS") != null)
                        {
                            nodTemp = tempElement.SelectSingleNode("//BOATS");
                            xmlCountAtts = nodTemp.Attributes;
                            intBoatCount = int.Parse(xmlCountAtts["COUNT"].InnerXml);
                        }
                        // Operator
                        if (tempElement.SelectSingleNode("//OPERATORS") != null)
                        {
                            nodTemp = tempElement.SelectSingleNode("//OPERATORS");
                            xmlCountAtts = nodTemp.Attributes;
                            intOperatorCount = int.Parse(xmlCountAtts["COUNT"].InnerXml);
                        }
                        //Watercraft UQ
                        if (tempElement.SelectSingleNode("//WATERCRAFTGENINFOS") != null)
                        {
                            nodTemp = tempElement.SelectSingleNode("//WATERCRAFTGENINFOS");
                            xmlCountAtts = nodTemp.Attributes;
                            intWCUQCount = int.Parse(xmlCountAtts["COUNT"].InnerXml);
                        }

                        if (intBoatCount > 0 || intOperatorCount > 0 || intWCUQCount > 0)
                        {
                            foreach (XmlNode nodTempNode in tempNodes)
                            {
                                foreach (XmlNode nodTempChild in nodTempNode.ChildNodes)
                                {
                                    foreach (XmlNode nodTempInnerChild in nodTempChild.ChildNodes)
                                    {
                                        if (nodTempInnerChild.InnerText.Trim() == "") //|| nodTempInnerChild.InnerText.Trim()=="NULL")
                                        {
                                            if (nodTempInnerChild.OuterXml == "<FUELERROR ERRFOUND=\"T\" />")
                                            {
                                                retVal = true;
                                                break;
                                            }
                                            else
                                                return false;

                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case LOB_PRIVATE_PASSENGER:
                        foreach (XmlNode nodTempNode in tempNodes)
                        {
                            foreach (XmlNode nodTempChild in nodTempNode.ChildNodes)
                            {
                                foreach (XmlNode nodTempInnerChild in nodTempChild.ChildNodes)
                                {

                                    //Commented by Manoj Rathore on 16 Jun 2009 Itrack # 5975
                                    //if(nodTempInnerChild.InnerText.Trim()=="" || nodTempInnerChild.InnerText.Trim()=="NULL")
                                    if (nodTempInnerChild.InnerText.Trim() == "")
                                    {
                                        return false;
                                        //retVal =false;
                                        //break;
                                    }
                                    foreach (XmlNode nodTempInnerChildChild in nodTempInnerChild.ChildNodes)
                                    {
                                        foreach (XmlNode nodTempInnerChildChildChild in nodTempInnerChildChild.ChildNodes)
                                        {
                                            //Commented by Manoj Rathore on 16 Jun 2009 Itrack # 5975
                                            //if(nodTempInnerChildChildChild.InnerText.Trim()=="" || nodTempInnerChildChildChild.InnerText.Trim()=="NULL")
                                            if (nodTempInnerChildChildChild.InnerText.Trim() == "")
                                            {
                                                return false;
                                                //retVal =false;
                                                //break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case LOB_MOTORCYCLE:
                        foreach (XmlNode nodTempNode in tempNodes)
                        {
                            foreach (XmlNode nodTempChild in nodTempNode.ChildNodes)
                            {
                                foreach (XmlNode nodTempInnerChild in nodTempChild.ChildNodes)
                                {

                                    if (nodTempInnerChild.InnerText.Trim() == "")//|| nodTempInnerChild.InnerText.Trim()=="NULL")
                                    {
                                        return false;
                                        //retVal =false;
                                        //break;
                                    }
                                }
                            }
                        }
                        break;
                    case LOB_WATERCRAFT:
                        foreach (XmlNode nodTempNode in tempNodes)
                        {
                            foreach (XmlNode nodTempChild in nodTempNode.ChildNodes)
                            {
                                foreach (XmlNode nodTempInnerChild in nodTempChild.ChildNodes)
                                {

                                    if (nodTempInnerChild.InnerText.Trim() == "") //|| nodTempInnerChild.InnerText.Trim()=="NULL")
                                    {
                                        return false;
                                        //retVal =false;
                                        //break;
                                    }
                                }
                            }
                        }
                        break;
                    case LOB_RENTAL_DWELLING:
                        foreach (XmlNode nodTempNode in tempNodes)
                        {
                            foreach (XmlNode nodTempChild in nodTempNode.ChildNodes)
                            {
                                foreach (XmlNode nodTempInnerChild in nodTempChild.ChildNodes)
                                {

                                    if (nodTempInnerChild.InnerText.Trim() == "") //|| nodTempInnerChild.InnerText.Trim()=="NULL")
                                    {
                                        if (nodTempInnerChild.OuterXml != "<FUELERROR ERRFOUND=\"T\" />")
                                        {
                                            return false;
                                            //retVal =false;
                                            //break;
                                        }
                                    }
                                    foreach (XmlNode nodTempInnerChildChild in nodTempInnerChild.ChildNodes)
                                    {

                                        if (nodTempInnerChildChild.InnerText.Trim() == "") // || nodTempInnerChildChild.InnerText.Trim()=="NULL")
                                        {
                                            return false;
                                            //retVal =false;
                                            //break;
                                        }

                                    }
                                }
                            }
                        }
                        break;
                    case LOB_UMBRELLA:
                        foreach (XmlNode nodTempNode in tempNodes)
                        {
                            foreach (XmlNode nodTempChild in nodTempNode.ChildNodes)
                            {
                                foreach (XmlNode nodTempInnerChild in nodTempChild.ChildNodes)
                                {

                                    if (nodTempInnerChild.InnerText.Trim() == "") //|| nodTempInnerChild.InnerText.Trim()=="NULL")
                                    {
                                        return false;
                                        //retVal =false;
                                        //break;
                                    }
                                }
                            }
                        }
                        break;
                    case LOB_GENERAL_LIABILITY:
                        retVal = false;
                        break;
                    case LOB_AVIATION:
                        foreach (XmlNode nodTempNode in tempNodes)
                        {
                            foreach (XmlNode nodTempChild in nodTempNode.ChildNodes)
                            {
                                foreach (XmlNode nodTempInnerChild in nodTempChild.ChildNodes)
                                {

                                    if (nodTempInnerChild.InnerText.Trim() == "")//|| nodTempInnerChild.InnerText.Trim()=="NULL")
                                    {
                                        return false;
                                        //retVal =false;
                                        //break;
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        //Added by Charles on 21-Apr-10
                        //Other LOB's
                        foreach (XmlNode nodTempNode in tempNodes)
                        {
                            foreach (XmlNode nodTempChild in nodTempNode.ChildNodes)
                            {
                                foreach (XmlNode nodTempInnerChild in nodTempChild.ChildNodes)
                                {

                                    if (nodTempInnerChild.InnerText.Trim() == "-1" || nodTempInnerChild.InnerText.Trim() == "")
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        break;
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


        # region Policy manadatory Information

        // To filter the string
        private string ReplaceString(string strReturnXML)
        {
            strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
            strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
            strReturnXML = strReturnXML.Replace("<Table>", "");
            strReturnXML = strReturnXML.Replace("</Table>", "");
            strReturnXML = strReturnXML.Replace("<Table3>", "");
            strReturnXML = strReturnXML.Replace("</Table3>", "");
            strReturnXML = strReturnXML.Replace("<Table4>", "");
            strReturnXML = strReturnXML.Replace("</Table4>", "");
            return strReturnXML;
        }


        // Get Policy Info 
        private string GetPolicyLevelInfoForRules(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();

            string strStoredProcForHO_ApplicationComponet = "Proc_GetPPARule_Pol";
            string strReturnXML = "";
            DataSet dsTempXML;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                if (strCalled != "FromHomeowner")
                {
                    returnString.Append("<INPUTXML>");
                    returnString.Append("<APPLICATIONS>");
                    // get the application info 
                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                    objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_ApplicationComponet);
                    objDataWrapper.ClearParameteres();
                    returnString.Append("<APPLICATION>");
                    strReturnXML = dsTempXML.GetXml();
                    strReturnXML = ReplaceString(strReturnXML);
                    returnString.Append(strReturnXML);
                    returnString.Append("</APPLICATION>");
                    returnString.Append("</APPLICATIONS>");
                }
                return returnString.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { }

        }

        /// <summary>
        /// This function will be used to return the input xml for all LOBs.
        /// This input xml will be checked against mandatory info of respective LOBs.
        /// </summary>
        /// <param name="customerID">Customer ID</param>
        /// <param name="PolicyID">Pol ID</param>
        /// <param name="PolicyVersionID">pol Version ID</param>
        /// <param name="PolicyLobID">Pol LOB ID</param>
        /// <returns>string: this return the inputs in form of a string</returns>		
        public string GetRuleInputXML_Pol()
        {
            try
            {
                string inputXML = "";
                string strCalled = "";
                if (PolicyLobID != "0")
                {
                    // switch case on the basis of the lob
                    switch (PolicyLobID)
                    {
                        case LOB_HOME:
                            inputXML = FetchHORuleInputXML_Pol(strCalled);
                            break;
                        case LOB_PRIVATE_PASSENGER:
                            inputXML = FetchAutoRuleInputXML_Pol(strCalled);
                            break;
                        case LOB_MOTORCYCLE:
                            inputXML = FetchMotorcycleRuleInputXML_Pol(strCalled);
                            break;
                        case LOB_WATERCRAFT:
                            inputXML = FetchWatercraftRuleInputXML_Pol(strCalled);
                            break;
                        case LOB_RENTAL_DWELLING:
                            inputXML = FetchRentalDwellingRuleInputXML_Pol(strCalled);
                            break;
                        case LOB_UMBRELLA:
                            inputXML = FetchUmbrellaRuleInputXML_Pol(strCalled);
                            break;
                        case LOB_AVIATION:
                            inputXML = FetchAviationRuleInputXML_Pol(strCalled);
                            break;

                        default:
                            inputXML = FetchProductsRuleInputXML_Pol(strCalled);
                            break;
                    }
                }
                return inputXML;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }
        }
        #region New Rule Impelmentation for New Products

        #region Rule enum
        public enum MainRuleType
        {
            AppDependent = 1,
            PolicyDependent = 2,
            ProcessDependent = 3
        }
        public enum SubSubRuleType
        {
            Mandatory = 1,
            Reject = 2,
            Refered = 3
        }
        public enum SubRuleType
        {
            All = 1,
            NewBusiness = 2,
            Endorsement = 3,
            Renewal = 4,
            CorrectiveUser = 5,
            Cancellation = 6
        }
        #endregion

        #region Private Rule Methods
        private ArrayList GetProductEffectiveRules(XmlNode parentNode, DateTime AppEffectiveDate, string StateId, string LobId)
        {
            ArrayList ruleNodes = new ArrayList();
            DateTime startDate, endDate;
            XmlNodeList dataNodes = parentNode.SelectNodes("Rule[@Action='Database']");
            foreach (XmlNode effectiveRule in dataNodes)
            {
                startDate = Convert.ToDateTime(effectiveRule.Attributes["StartDate"].Value);
                endDate = Convert.ToDateTime(effectiveRule.Attributes["EndDate"].Value);
                if (AppEffectiveDate >= startDate && AppEffectiveDate <= endDate)
                {
                    if ((effectiveRule.Attributes["STATE_ID"] == null
                        || effectiveRule.Attributes["STATE_ID"].Value == StateId
                        || effectiveRule.Attributes["STATE_ID"].Value == "0"
                        )
                        &&
                        (effectiveRule.Attributes["LOB_ID"] == null
                         || effectiveRule.Attributes["LOB_ID"].Value == LobId
                         || effectiveRule.Attributes["LOB_ID"].Value == "0"
                         )
                        )
                    {
                        ruleNodes.Add(effectiveRule);
                    }
                }
            }
            return ruleNodes;

        }
        private string EvalNode(XmlNode node, Hashtable MasterKeys)
        {
            string Operand1 = node.Attributes["Operand1"].Value;
            string Operand2 = node.Attributes["Operand2"].Value;
            string Operator = node.Attributes["Operator"].Value;
            string strTemp = "";

            if (Operand1.StartsWith("$"))
            {
                strTemp = Operand1.Substring(Operand1.IndexOf('$') + 1);
                if (!MasterKeys.Contains(strTemp)) return "NoKey";
                Operand1 = MasterKeys[strTemp].ToString();
            }
            strTemp = "";
            if (Operand2.StartsWith("$"))
            {
                strTemp = Operand2.Substring(Operand2.IndexOf('$') + 1);
                if (!MasterKeys.Contains(strTemp)) return "NoKey";
                Operand2 = MasterKeys[strTemp].ToString();
            }

            //For comparing Strings 
            if (node.Attributes["OperandType"] != null)
            {
                string OperandType = node.Attributes["OperandType"].Value;
                if (OperandType == "String")
                {
                    bool result = false;
                    if (Operator == "==")
                    {
                        if (Operand1 == Operand2)
                            result = true;
                        else
                            result = false;
                    }
                    if (Operator == "!=")
                    {
                        if (Operand1 == Operand2)
                            result = false;
                        else
                            result = true;
                    }
                    return result.ToString();
                }
            }//end of String comparision

            //Check if the operand starts with "$" sign if yes fetch value from 
            //respective key from key value pair

            if (Operand1.Trim() != "" && Operand2.Trim() != "" && Operator.Trim() != "")
            {
                RPNParser parser = new RPNParser();
                ArrayList arrExpr = parser.GetPostFixNotation(Operand1 + Operator + Operand2,
                    Type.GetType("System.Double"), false);
                string szResult = parser.Convert2String(arrExpr);
                return parser.EvaluateRPN(arrExpr, Type.GetType("System.Double"), null).ToString();
            }
            return "";

        }
        private string EvalNode(XmlNode node, XmlNode RuleNode)
        {
            string Operand1 = node.Attributes["Operand1"].Value;
            string Operand2 = node.Attributes["Operand2"].Value;
            string Operator = node.Attributes["Operator"].Value;
            string strTemp = "";

            if (Operand1.StartsWith("$"))
            {
                strTemp = Operand1.Substring(Operand1.IndexOf('$') + 1);
                if(RuleNode.SelectSingleNode(strTemp) != null)
                Operand1 = RuleNode.SelectSingleNode(strTemp).InnerText;
            }
            strTemp = "";
            if (Operand2.StartsWith("$"))
            {
                strTemp = Operand2.Substring(Operand2.IndexOf('$') + 1);
                Operand2 = RuleNode.SelectSingleNode(strTemp).InnerText;
            }

            //For comparing Strings 
            if (node.Attributes["OperandType"] != null)
            {
                string OperandType = node.Attributes["OperandType"].Value;
                if (OperandType == "String")
                {
                    bool result = false;
                    if (Operator == "==")
                    {
                        if (Operand1 == Operand2)
                            result = true;
                        else
                            result = false;
                    }
                    if (Operator == "!=")
                    {
                        if (Operand1 == Operand2)
                            result = false;
                        else
                            result = true;
                    }
                    return result.ToString();
                }
                if (OperandType == "Double")
                {
                    bool result = false;
                    if (Operator == "==")
                    {
                        if (Double.Parse(Operand1, this.numberFormatInfo) == Double.Parse(Operand2, this.numberFormatInfo))
                            result = true;
                        else
                            result = false;
                    }
                    else if (Operator == "!=")
                    {
                        if (Double.Parse(Operand1, this.numberFormatInfo) == Double.Parse(Operand2, this.numberFormatInfo))
                            result = false;
                        else
                            result = true;
                    }
                    else if (Operator == "&gt;" || Operator == ">")
                    {
                        if (Double.Parse(Operand1, this.numberFormatInfo) > Double.Parse(Operand2, this.numberFormatInfo))
                            result = true;
                        else
                            result = false;
                    }
                    else if (Operator == "&lt;" || Operator == "<")
                    {
                        if (Double.Parse(Operand1, this.numberFormatInfo) < Double.Parse(Operand2, this.numberFormatInfo))
                            result = true;
                        else
                            result = false;
                    }
                    else if (Operator == "&gt;=" || Operator == ">=")
                    {
                        if (Double.Parse(Operand1, this.numberFormatInfo) >= Double.Parse(Operand2, this.numberFormatInfo))
                            result = true;
                        else
                            result = false;
                    }
                    else if (Operator == "&lt;=" || Operator == "<=")
                    {
                        if (Double.Parse(Operand1, this.numberFormatInfo) <= Double.Parse(Operand2, this.numberFormatInfo))
                            result = true;
                        else
                            result = false;
                    }
                    return result.ToString();
                }
            }//end of String comparision

            //Check if the operand starts with "$" sign if yes fetch value from 
            //respective key from key value pair

            if (Operand1.Trim() != "" && Operand2.Trim() != "" && Operator.Trim() != "")
            {
                RPNParser parser = new RPNParser();
                ArrayList arrExpr = parser.GetPostFixNotation(Operand1 + Operator + Operand2,
                    Type.GetType("System.Double"), false);
                string szResult = parser.Convert2String(arrExpr);
                return parser.EvaluateRPN(arrExpr, Type.GetType("System.Double"), null).ToString();
            }
            return "";

        }
        private void AddRemoveScreenRuleNode(XmlNode xmlNode)
        {
            //Parse each ToCompare node
            XmlNodeList ToSetNodes = xmlNode.SelectNodes("ToCompare");
            string strToSetKey = ""; string strValue = "";
            foreach (XmlNode node in ToSetNodes)
            {
                strToSetKey = node.Attributes["Key"].Value.Replace("$", "");
                strValue = "";
                if (node.Attributes["Value"].Value.Trim() != "")
                {
                    strValue = node.Attributes["Value"].Value.Trim();
                    if (RuleKeys.Contains(strToSetKey)) RuleKeys.Remove(strToSetKey);
                    RuleKeys.Add(strToSetKey, strValue);
                }
            }
            //Parse each toRevoke node
            XmlNodeList ToRevokeNodes = xmlNode.SelectNodes("ToRevoke");
            string strRevokeKey = "";
            foreach (XmlNode node in ToRevokeNodes)
            {
                strRevokeKey = node.Attributes["Key"].Value.Replace("$", "");
                if (RuleKeys.Contains(strRevokeKey))
                    RuleKeys.Remove(strRevokeKey);
            }
        }
        private void InitialiseScreenRules(XmlNode ScreenNode, XmlNode RuleXmlNode)
        {
            if (RuleKeys.Count > 0) RuleKeys.Clear();
            XmlNodeList ConditionsNodeArray = ScreenNode.SelectNodes("Conditions");
            foreach (XmlNode ConditionsNode in ConditionsNodeArray)
            {
                this.AddRemoveScreenRuleNode(ConditionsNode);
                //Parse each condiation node
                XmlNodeList conditionNodeArray = ConditionsNode.SelectNodes("Condition");
                foreach (XmlNode conditionNode in conditionNodeArray) //Condition Loop
                {
                    string strResult = this.EvalNode(conditionNode, RuleXmlNode);
                    if (strResult == "True")
                    {
                        //add Condition verified rule
                        this.AddRemoveScreenRuleNode(conditionNode);
                        XmlNodeList subConditionsNodeArray = conditionNode.SelectNodes("SubCondition");
                        if (subConditionsNodeArray != null)
                        {
                            foreach (XmlNode subConditionNode in subConditionsNodeArray)
                            {
                                string strRes = this.EvalNode(subConditionNode, RuleXmlNode);
                                if (strRes == "True")
                                {
                                    this.AddRemoveScreenRuleNode(subConditionNode);
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            //IsInitialised = true;
        }
        // InitialiseRules initialise Hashtable from data fetched from SP
        protected void InitialiseMasterRules(int CustomerId, int Id, int VersionId, XmlDocument RuleDoc)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            XmlNode masterNode = RuleDoc.SelectSingleNode("Root/Master");
            string strQuery = "";
            XmlNode queryNode = masterNode.SelectSingleNode("Query[@Level='POLICY']");
            strQuery = queryNode.InnerText;

            objDataWrapper.ClearParameteres();
            objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
            objDataWrapper.AddParameter("@POLICY_ID", Id);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", VersionId);

            DataSet dtKeyValue = objDataWrapper.ExecuteDataSet(strQuery);

            objDataWrapper.ClearParameteres();

            if (ProductMasterRuleKeys.Count > 0)
            {
                ProductMasterRuleKeys.Clear();
            }
            XmlNodeList columnNodes = masterNode.SelectNodes("Column");

            if (dtKeyValue.Tables.Count > 0)
            {
                if (dtKeyValue.Tables[0].Rows.Count > 0)
                {

                    foreach (XmlNode node in columnNodes)
                    {
                        string strKey = node.Attributes["Code"].Value;
                        string strValue = "";
                        if (node.Attributes["MapColumnn"].Value.Trim() != "")
                        {
                            if (dtKeyValue.Tables[0].Rows[0][strKey] != DBNull.Value)
                            {
                                strValue = dtKeyValue.Tables[0].Rows[0][strKey].ToString().Trim();
                                //If the returned value is Negative make it Zero
                                if (strValue.StartsWith("-"))
                                {
                                    strValue = "0";
                                }
                            }
                            ProductMasterRuleKeys.Add(strKey, strValue);
                        }
                    }
                }
            }
            //AppEffectiveDate = Convert.ToDateTime(ProductMasterRuleKeys["APP_EFFECTIVE_DATE"]);
            //StateId = ProductMasterRuleKeys["STATE_ID"].ToString();

        }
        private string ValidateWithMasterKeys(string ScreenId, XmlNode ScreenNode)
        {
            //if (!ProductMasterRuleKeys.Contains(ScreenId)) return false;
            string retval = "NoKey";
            XmlNodeList ConditionsNodeArray = ScreenNode.SelectNodes("Conditions");
            foreach (XmlNode ConditionsNode in ConditionsNodeArray)
            {
                XmlNodeList conditionNodeArray = ConditionsNode.SelectNodes("Condition");
                foreach (XmlNode conditionNode in conditionNodeArray) //Condition Loop
                {
                    string strResult = this.EvalNode(conditionNode, ProductMasterRuleKeys);
                    if ((strResult == "True"))
                    {
                        retval = "True"; //return false;
                        return retval;
                    }
                    else if ((strResult == "NoKey"))
                        retval = "NoKey";
                    else
                    {
                        retval = "False";
                        XmlNodeList subConditionsNodeArray = conditionNode.SelectNodes("SubCondition");
                        if (subConditionsNodeArray != null)
                        {
                            foreach (XmlNode subConditionNode in subConditionsNodeArray)
                            {
                                string strRes = this.EvalNode(subConditionNode, ProductMasterRuleKeys);
                                if ((strRes == "True"))
                                {
                                    retval = "True";// return false;
                                    return retval;
                                }
                                else
                                    retval = strRes;
                            }
                        }
                        //break;
                    }
                }
            }
            return retval;
        }
        private void AddErrorNodeToRuleXML(string ScreenNode, string errorNode, ref StringBuilder returnString)
        {
            returnString.Append("<" + ScreenNode + ">");
            returnString.Append("<ERRORS>");
            returnString.Append("<" + errorNode + " ERRFOUND = 'T'/>");
            returnString.Append("</ERRORS>");
            returnString.Append("</" + ScreenNode + ">");
        }
        #endregion

        private bool ValidateRuleXMLByRuleNode(XmlDocument InputRuleDoc, XmlDocument RuleDoc, string LOBID, MainRuleType mRuleType, SubRuleType mSubRuleType)
        {
            bool retVal = true;
            string rootNode = "INPUTXML";
            XmlNode groupNode = RuleDoc.SelectSingleNode("Root/Group[@Code='" + mRuleType.ToString() + "']");
            XmlNode subGroupNode = groupNode.SelectSingleNode("SubGroup[@Code='" + mSubRuleType + "']");
            if (subGroupNode == null)
                subGroupNode = groupNode.SelectSingleNode("SubGroup[@Code='" + SubRuleType.All.ToString() + "']");
            //Parsing Mandatory Rule
            XmlNode subSubGroupNode = subGroupNode.SelectSingleNode("SubSubGroup[@Code='" + SubSubRuleType.Mandatory.ToString() + "']");
            string EffDate = InputRuleDoc.SelectSingleNode(rootNode + "/APPLICATIONS/APPLICATION/APP_EFFECTIVE_DATE").InnerText;
            string StateId = InputRuleDoc.SelectSingleNode(rootNode + "/APPLICATIONS/APPLICATION/STATE_ID").InnerText;
            InitialiseMasterRules(customerID, PolicyID, PolicyVersionID, RuleDoc);
            ArrayList arEffectiveRules = GetProductEffectiveRules(subSubGroupNode, Convert.ToDateTime(EffDate), StateId, LOBID);
            for (int i = 0; i < arEffectiveRules.Count; i++)
            {
                XmlNode ruleNode = (XmlNode)arEffectiveRules[i];
                XmlNodeList ScreensNodeArray = ruleNode.SelectNodes("Screen");
                foreach (XmlNode ScreenNode in ScreensNodeArray)
                {
                    string screenId = ScreenNode.Attributes["ID"].InnerText;
                    string IsRequired = ScreenNode.Attributes["IsRequired"].InnerText;
                    //if (IsRequired != "Y") continue;
                    XmlNode RuleXmlNode = InputRuleDoc.SelectSingleNode(rootNode + "/" + screenId);
                    if (RuleXmlNode == null) return false;
                    XmlAttribute AttCount = InputRuleDoc.CreateAttribute("COUNT");
                    foreach (XmlNode RiskRuleNode in RuleXmlNode.ChildNodes)
                    {
                        //InitialiseScreenRules(ScreenNode, RuleXmlNode);
                        string SubScreenID = RiskRuleNode.Name.ToString();
                        if (SubScreenID == "ERRORS")
                        {
                            AttCount.Value = "0";
                            string strValidKey = ValidateWithMasterKeys(screenId, ScreenNode);
                            if (strValidKey == "True")
                            {
                                if (IsRequired == "Y") retVal = false;
                            }
                            if (strValidKey == "NoKey")
                            {
                                if (IsRequired == "Y") retVal = false;
                            }

                            else if (strValidKey == "False")
                                AttCount.Value = "-1";
                            RuleXmlNode.Attributes.Append(AttCount);
                            continue;
                        }
                        AttCount.Value = RuleXmlNode.ChildNodes.Count.ToString();
                        RuleXmlNode.Attributes.Append(AttCount);
                        InitialiseScreenRules(ScreenNode, RiskRuleNode);
                        foreach (object key in RuleKeys.Keys)
                        {
                            XmlNode InnerNode = RiskRuleNode.SelectSingleNode(key.ToString());
                            if (InnerNode != null)
                            {
                                string RuleValue = InnerNode.InnerText;
                                string KeyValue = RuleKeys[key].ToString();
                                if (KeyValue.StartsWith("$"))
                                    KeyValue = RiskRuleNode.SelectSingleNode(KeyValue.Substring(1)).InnerText;
                                if (RuleValue == KeyValue)
                                {
                                    RiskRuleNode.SelectSingleNode(key.ToString()).InnerText = "";
                                    retVal = false;
                                }
                            }
                        }
                    }
                }
            }
            if (!retVal) return retVal;
            //Parsing Reject Rule
            subSubGroupNode = subGroupNode.SelectSingleNode("SubSubGroup[@Code='" + SubSubRuleType.Reject.ToString() + "']");
            arEffectiveRules = GetProductEffectiveRules(subSubGroupNode, Convert.ToDateTime(EffDate), StateId, LOBID);

            for (int i = 0; i < arEffectiveRules.Count; i++)
            {
                XmlNode ruleNode = (XmlNode)arEffectiveRules[i];
                XmlNodeList ScreensNodeArray = ruleNode.SelectNodes("Screen");
                foreach (XmlNode ScreenNode in ScreensNodeArray)
                {
                    string screenId = ScreenNode.Attributes["ID"].InnerText;
                    string IsRequired = ScreenNode.Attributes["IsRequired"].InnerText;
                    XmlNode RuleXmlNode = InputRuleDoc.SelectSingleNode(rootNode + "/" + screenId);
                    XmlAttribute AttCount = InputRuleDoc.CreateAttribute("COUNT");
                    foreach (XmlNode RiskRuleNode in RuleXmlNode.ChildNodes)
                    {
                        string SubScreenID = RiskRuleNode.Name.ToString();
                        if (SubScreenID == "ERRORS")
                        {
                            AttCount.Value = "0";
                            string strValidKey = ValidateWithMasterKeys(screenId, ScreenNode);
                            if (strValidKey == "True")
                            {
                                if (IsRequired == "Y") retVal = false;
                            }
                            else if (strValidKey == "False")
                                AttCount.Value = "-1";
                            RuleXmlNode.Attributes.Append(AttCount);
                            continue;
                        }

                        //if (SubScreenID == "ERRORS")
                        //{
                        //    AttCount.Value = "0";
                        //    RuleXmlNode.Attributes.Append(AttCount);
                        //    retVal = false; continue;
                        //}
                        AttCount.Value = RuleXmlNode.ChildNodes.Count.ToString();
                        RuleXmlNode.Attributes.Append(AttCount);
                        InitialiseScreenRules(ScreenNode, RiskRuleNode);
                        foreach (object key in RuleKeys.Keys)
                        {
                            XmlNode InnerNode = RiskRuleNode.SelectSingleNode(key.ToString());
                            if (InnerNode != null)
                            {
                                string RuleValue = InnerNode.InnerText;
                                string KeyValue = RuleKeys[key].ToString();
                                if (KeyValue.StartsWith("$"))
                                    KeyValue = RiskRuleNode.SelectSingleNode(KeyValue.Substring(1)).InnerText;
                                if (RuleValue == KeyValue)
                                {
                                    RiskRuleNode.SelectSingleNode(key.ToString()).InnerText = "Y";
                                }
                            }
                        }
                    }
                }
            }
            //Parsing refered Rule
            subSubGroupNode = subGroupNode.SelectSingleNode("SubSubGroup[@Code='" + SubSubRuleType.Refered.ToString() + "']");
            arEffectiveRules = GetProductEffectiveRules(subSubGroupNode, Convert.ToDateTime(EffDate), StateId, LOBID);

            for (int i = 0; i < arEffectiveRules.Count; i++)
            {
                XmlNode ruleNode = (XmlNode)arEffectiveRules[i];
                XmlNodeList ScreensNodeArray = ruleNode.SelectNodes("Screen");
                foreach (XmlNode ScreenNode in ScreensNodeArray)
                {
                    string screenId = ScreenNode.Attributes["ID"].InnerText;
                    string IsRequired = ScreenNode.Attributes["IsRequired"].InnerText;
                    XmlNode RuleXmlNode = InputRuleDoc.SelectSingleNode(rootNode + "/" + screenId);
                    XmlAttribute AttCount = InputRuleDoc.CreateAttribute("COUNT");
                    foreach (XmlNode RiskRuleNode in RuleXmlNode.ChildNodes)
                    {
                        string SubScreenID = RiskRuleNode.Name.ToString();
                        if (SubScreenID == "ERRORS")
                        {
                            AttCount.Value = "0";
                            string strValidKey = ValidateWithMasterKeys(screenId, ScreenNode);
                            if (strValidKey == "True")
                            {
                                if (IsRequired == "Y") retVal = false;
                            }
                            else if (strValidKey == "False")
                                AttCount.Value = "-1";
                            RuleXmlNode.Attributes.Append(AttCount);
                            continue;
                        }
                        AttCount.Value = RuleXmlNode.ChildNodes.Count.ToString();
                        RuleXmlNode.Attributes.Append(AttCount);
                        InitialiseScreenRules(ScreenNode, RiskRuleNode);
                        foreach (object key in RuleKeys.Keys)
                        {
                            XmlNode InnerNode = RiskRuleNode.SelectSingleNode(key.ToString());
                            if (InnerNode != null)
                            {
                                string RuleValue = InnerNode.InnerText;
                                string KeyValue = RuleKeys[key].ToString();
                                if (KeyValue.StartsWith("$"))
                                    KeyValue = RiskRuleNode.SelectSingleNode(KeyValue.Substring(1)).InnerText;
                                if (RuleValue == KeyValue)
                                {
                                    //InputRuleDoc.SelectSingleNode(rootNode + "/" + screenId + "/" + SubScreenID + "/" + key.ToString()).InnerText = "Y";
                                    RiskRuleNode.SelectSingleNode(key.ToString()).InnerText = "Y";
                                }
                            }
                        }
                    }
                }
            }
            return retVal;
        }
        private bool ValidateProductInputRuleXML(ref string inputXML, string LOBID)
        {
            return ValidateProductInputRuleXML(ref inputXML, LOBID, MainRuleType.AppDependent, SubRuleType.All);
        }
        private bool ValidateProductInputRuleXML(ref string inputXML, string LOBID, MainRuleType mRuleType, SubRuleType mSubRuleType)
        {
            try
            {
                string Language_code = enumCulture.US; //Added by aditya for itrack 1442 on 03/08/2011
                numberFormatInfo = new CultureInfo(Language_code, true).NumberFormat;
                bool retVal = true;
                XmlDocument tempDoc = new XmlDocument();
                tempDoc.LoadXml(inputXML);
                XmlElement tempElement = tempDoc.DocumentElement;
                XmlNodeList tempNodes = tempElement.ChildNodes;
                string CustomiseRuleFilePatch = ClsCommon.GetKeyValueWithIP("FilePathCustomiseProductsRuleXML");
                XmlDocument RuleDoc = new XmlDocument();
                RuleDoc.Load(CustomiseRuleFilePatch);

                retVal = ValidateRuleXMLByRuleNode(tempDoc, RuleDoc, LOBID, mRuleType, mSubRuleType);

                inputXML = tempDoc.InnerXml;
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
        /// <summary>
        /// Fetches Policy Level Product Rule 
        /// </summary>
        /// <param name="strCalled"></param>
        /// <returns></returns>
        // Get Policy Info 

        private string FetchProductsRuleInputXML_Pol(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();

            try
            {
                /*returnString.Remove(0, returnString.Length);
                string strResult = GetPolicyLevelInfoForRules(strCalled);
                returnString.Append(strResult);
                returnString.Append("</INPUTXML>");
                return returnString.ToString();
                */

                String strReturnXML = "";
                DataSet dsTemp;
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                // Get the Risks Ids against the customerID, PolicyID, PolicyVersionID
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objDataWrapper.AddParameter("@lang_id", BlCommon.ClsCommon.BL_LANG_ID);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetProductRule_Pol");
                objDataWrapper.ClearParameteres();
                returnString.Append("<INPUTXML>");

                // get the application info
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        strReturnXML = dr[0].ToString();
                        returnString.Append(strReturnXML);
                    }
                }
                else
                    AddErrorNodeToRuleXML("APPLICATIONS", "POLICYERROR", ref returnString);
                // get the Location info 
                if (dsTemp.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemp.Tables[1].Rows)
                    {
                        strReturnXML = dr[0].ToString();
                        returnString.Append(strReturnXML);
                    }
                }
                else
                    AddErrorNodeToRuleXML("LOCATIONS", "LOCATIONERROR", ref returnString);
                // get the Co-Insurance info 
                if (dsTemp.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemp.Tables[2].Rows)
                    {
                        strReturnXML = dr[0].ToString();
                        returnString.Append(strReturnXML);
                    }
                }
                else
                    AddErrorNodeToRuleXML("COINSURANCES", "COINSURANCEERROR", ref returnString);
                // get the Remuneration info 
                if (dsTemp.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemp.Tables[3].Rows)
                    {
                        strReturnXML = dr[0].ToString();
                        returnString.Append(strReturnXML);
                    }
                }
                else
                    AddErrorNodeToRuleXML("REMUNERATIONS", "REMUNERATIONERROR", ref returnString);

                // get the POl Clauses info 
                if (dsTemp.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemp.Tables[4].Rows)
                    {
                        strReturnXML = dr[0].ToString();
                        returnString.Append(strReturnXML);
                    }
                }
                else
                    AddErrorNodeToRuleXML("CLAUSES", "CLAUSESERROR", ref returnString);

                //get Discount/Surcharge info
                if (dsTemp.Tables[5].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemp.Tables[5].Rows)
                    {
                        strReturnXML = dr[0].ToString();
                        returnString.Append(strReturnXML);
                    }
                }
                else
                    AddErrorNodeToRuleXML("DISCOUNTS", "DISCOUNTERROR", ref returnString);
                //get Reinsurance info
                if (dsTemp.Tables[6].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemp.Tables[6].Rows)
                    {
                        strReturnXML = dr[0].ToString();
                        returnString.Append(strReturnXML);
                    }
                }
                else
                    AddErrorNodeToRuleXML("REINSURANCES", "REINSURANCEERROR", ref returnString);
                //get Risk info
                if (dsTemp.Tables[7].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemp.Tables[7].Rows)
                    {
                        strReturnXML = dr[0].ToString();
                        returnString.Append(strReturnXML);
                    }
                }
                else
                    AddErrorNodeToRuleXML("RISKS", "RISKERROR", ref returnString);
                //get Coverages info
                if (dsTemp.Tables[8].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemp.Tables[8].Rows)
                    {
                        strReturnXML = dr[0].ToString();
                        returnString.Append(strReturnXML);
                    }
                }
                else
                    AddErrorNodeToRuleXML("COVERAGES", "COVERAGEERROR", ref returnString);
                //get Protective Devices info
                if (dsTemp.Tables[9].Rows.Count > 0)
                {
                    //strReturnXML = dsTemp.Tables[9].Rows[0][0].ToString();
                    //returnString.Append(strReturnXML);
                    foreach (DataRow dr in dsTemp.Tables[9].Rows)
                    {
                        strReturnXML = dr[0].ToString();
                        returnString.Append(strReturnXML);
                    }
                }
                else
                    AddErrorNodeToRuleXML("P_DEVICES", "P_DEVICEERROR", ref returnString);
                //get Billing Info info
                if (dsTemp.Tables[10].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemp.Tables[10].Rows)
                    {
                        strReturnXML = dr[0].ToString();
                        returnString.Append(strReturnXML);
                    }
                }
                else
                    AddErrorNodeToRuleXML("BILLING_INFOS", "BILLING_INFOERROR", ref returnString);

                //returnString.Append("</INPUTXML>");
                //string strSystemID = mSystemID; 
                //return returnString.ToString();

                //GET APPLICANT INFO

                if (dsTemp.Tables[11].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemp.Tables[11].Rows)
                    {
                        strReturnXML = dr[0].ToString();
                        returnString.Append(strReturnXML);
                    }
                }
                else
                    AddErrorNodeToRuleXML("APPLICANTS", "APPLICANTS_INFOERROR", ref returnString);

                //GET REMUNERATION ENDORSEMENT INFO

                if (dsTemp.Tables[12].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemp.Tables[12].Rows)
                    {
                        strReturnXML = dr[0].ToString();
                        returnString.Append(strReturnXML);
                    }
                }
                else
                    AddErrorNodeToRuleXML("ENDORSEMENT_REMUNERATIONS", "ENDORSEMENT_REMUNERATIONS_INFOERROR", ref returnString);

                returnString.Append("</INPUTXML>");
                string strSystemID = mSystemID;
                return returnString.ToString();


            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }
        #endregion
        #region rule for Aviation
        private string GetAviationPolicyVehicleIDs(int customerID, int PolicyID, int PolicyVersionID, DataWrapper objWrapper)
        {
            string strStoredProc = "Proc_GetAviationPolicyVehicleIDs";
            if (objWrapper == null)
                objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strVehicleID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strVehicleID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strVehicleID = strVehicleID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strVehicleID;
        }

        private string FetchAviationRuleInputXML_Pol(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();
            string strProc_AviationVehicleComponent = "Proc_GetAviationRule_Vehicle_Pol";
            string strProc_AviationCoverageLimitComponent = "Proc_GetAviationRule_CoverageInfo_Pol";
            string strReturnXML = "", strSystemID = "";
            DataSet dsTempXML;
            try
            {
                strSystemID = mSystemID;
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                string strResult = GetPolicyLevelInfoForRules(strCalled);
                returnString.Append(strResult);
                // Get the Vehicle Ids against the customerID, PolicyID, PolicyVersionID
                //get the vehicle details for each vehicle				
                string vehicleIDs = GetAviationPolicyVehicleIDs(customerID, PolicyID, PolicyVersionID, objDataWrapper);
                int intVehicleNo = 0;
                string[] vehicleID = new string[0];
                if (vehicleIDs != "-1")
                {
                    vehicleID = vehicleIDs.Split('^');
                    intVehicleNo = vehicleID.Length;
                }
                returnString.Append("<VEHICLES COUNT='" + intVehicleNo + "'>");
                if (vehicleIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < vehicleID.Length; iCounter++)
                    {
                        string strVehicleID = vehicleID[iCounter];
                        returnString.Append("<VEHICLE ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.ClearParameteres();
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@Policy_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@VEHICLE_ID", vehicleID[iCounter]);
                        // get vehicle details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strProc_AviationVehicleComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();

                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        /*//1.Coverage
                          returnString.Append("<COVERAGE ID='"+ vehicleID[iCounter]+"'>");
                          objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
                          objDataWrapper.AddParameter("@POLICY_ID",PolicyID);
                          objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);	
                          objDataWrapper.AddParameter("@VEHICLE_ID",vehicleID[iCounter]);
                          objDataWrapper.AddParameter("@USER",strSystemID);
                          dsTempXML =	objDataWrapper.ExecuteDataSet(strStoredProcForPPA_CoverageLimitComponent);	
                          objDataWrapper.ClearParameteres();
                          strReturnXML = dsTempXML.GetXml();
                          strReturnXML = ReplaceString(strReturnXML);	

                          //Repalce Table1 Nodes.
                          strReturnXML = strReturnXML.Replace("<Table1>","");
                          strReturnXML = strReturnXML.Replace("</Table1>","");
                          strReturnXML = strReturnXML.Replace("<COVERAGE_DES>","<COV><COV_DES>");
                          strReturnXML = strReturnXML.Replace("</COVERAGE_DES>","</COV_DES></COV>");	
                          //
                          //Replace Table2 Nodes.
                          strReturnXML = strReturnXML.Replace("<Table2>","");
                          strReturnXML = strReturnXML.Replace("</Table2>","");
                          strReturnXML = strReturnXML.Replace("<LIMIT_DESCRIPTION>","<LIMIT><LIMIT_DES>");
                          strReturnXML = strReturnXML.Replace("</LIMIT_DESCRIPTION>","</LIMIT_DES></LIMIT>");
                          //********************
                          //Replace Table3 Nodes.
                          strReturnXML = strReturnXML.Replace("<Table3>","");
                          strReturnXML = strReturnXML.Replace("</Table3>","");
                          strReturnXML = strReturnXML.Replace("<DEDUCTIBLE_DESCRIPTION>","<DEDUCT><DEDUCT_DES>");
                          strReturnXML = strReturnXML.Replace("</DEDUCTIBLE_DESCRIPTION>","</DEDUCT_DES></DEDUCT>");
						
                          returnString.Append(strReturnXML);
                          returnString.Append("</COVERAGE>");		
                      */
                        returnString.Append("</VEHICLE>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</VEHICLES>");

                returnString.Append("</INPUTXML>");
                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        #endregion
        // PPA 
        private string FetchAutoRuleInputXML_Pol(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForAuto_VehicleComponent = "Proc_GetPPARule_Vehcile_Pol";
            string strStoredProcForAuto_DriverComponent = "Proc_GetPPARule_Drivers_Pol";
            string strStoredProcForAuto_GenInfoComponent = "Proc_GetPPARule_GenInfo_Pol";
            string strStoredProcForPPA_CoverageLimitComponent = "Proc_GetPPARule_CoverageInfo_Pol";
            string strStoredProcForPPA_MVRInfoComponent = "Proc_GetMotorcycleRule_DriverMVR_Pol";
            string strReturnXML = "", strSystemID = "";
            DataSet dsTempXML;
            try
            {
                //				if(IsEODProcess)
                //				{
                //					strSystemID = EODSystemID;
                //				}
                //				else
                //				{
                //					strSystemID  = System.Web.HttpContext.Current.Session["systemId"].ToString(); 
                //				}

                strSystemID = mSystemID;

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                string strResult = GetPolicyLevelInfoForRules(strCalled);
                returnString.Append(strResult);
                // Get the Vehicle Ids against the customerID, PolicyID, PolicyVersionID
                //get the vehicle details for each vehicle				
                string vehicleIDs = GetVehicleIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intVehicleNo = 0;
                string[] vehicleID = new string[0];
                if (vehicleIDs != "-1")
                {
                    vehicleID = vehicleIDs.Split('^');
                    intVehicleNo = vehicleID.Length;
                }
                returnString.Append("<VEHICLES COUNT='" + intVehicleNo + "'>");
                if (vehicleIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < vehicleID.Length; iCounter++)
                    {
                        string strVehicleID = vehicleID[iCounter];
                        returnString.Append("<VEHICLE ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@Policy_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@VEHICLE_ID", vehicleID[iCounter]);
                        // get vehicle details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_VehicleComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();

                        //Grandfather case for territory
                        //						strReturnXML = strReturnXML.Replace("<Table>","");
                        //						strReturnXML = strReturnXML.Replace("</Table>","");
                        //						strReturnXML = strReturnXML.Replace("<TERRITORY_DESCRIPTION>","<TERR><TERR_DES>");
                        //						strReturnXML = strReturnXML.Replace("</TERRITORY_DESCRIPTION>","</TERR_DES></TERR>");

                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        //1.Coverage

                        returnString.Append("<COVERAGE ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@VEHICLE_ID", vehicleID[iCounter]);
                        objDataWrapper.AddParameter("@USER", strSystemID);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForPPA_CoverageLimitComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);

                        //Repalce Table1 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table1>", "");
                        strReturnXML = strReturnXML.Replace("</Table1>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_DES>", "<COV><COV_DES>");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_DES>", "</COV_DES></COV>");
                        //
                        //Replace Table2 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table2>", "");
                        strReturnXML = strReturnXML.Replace("</Table2>", "");
                        strReturnXML = strReturnXML.Replace("<LIMIT_DESCRIPTION>", "<LIMIT><LIMIT_DES>");
                        strReturnXML = strReturnXML.Replace("</LIMIT_DESCRIPTION>", "</LIMIT_DES></LIMIT>");
                        //********************
                        //Replace Table3 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table3>", "");
                        strReturnXML = strReturnXML.Replace("</Table3>", "");
                        strReturnXML = strReturnXML.Replace("<DEDUCTIBLE_DESCRIPTION>", "<DEDUCT><DEDUCT_DES>");
                        strReturnXML = strReturnXML.Replace("</DEDUCTIBLE_DESCRIPTION>", "</DEDUCT_DES></DEDUCT>");

                        returnString.Append(strReturnXML);
                        returnString.Append("</COVERAGE>");
                        returnString.Append("</VEHICLE>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</VEHICLES>");

                //get the driver details for each driver

                string driverIDs = GetRuleDriverIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intDriverNo = 0;
                string[] driverID = new string[0];
                if (driverIDs != "-1")
                {
                    driverID = driverIDs.Split('^');
                    intDriverNo = driverID.Length;
                }
                returnString.Append("<DRIVERS COUNT='" + intDriverNo + "'>");
                if (driverIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < driverID.Length; iCounter++)
                    {
                        string strDriverId = driverID[iCounter];
                        returnString.Append("<DRIVER ID='" + driverID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@Policy_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@DRIVER_ID", driverID[iCounter]);
                        // get driver detial
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_DriverComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</DRIVER>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</DRIVERS>");
                //********************* Start Get MVR Information **************************
                string mvrIDs = GetRuleMVRIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intmvrNo = 0;
                string[] mvrID = new string[0];
                if (mvrIDs != "-1")
                {
                    mvrID = mvrIDs.Split('^');
                    intmvrNo = mvrID.Length;
                }

                if (mvrIDs != "-1")
                {
                    returnString.Append("<MVRS COUNT='" + intmvrNo + "'>");
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < mvrID.Length; iCounter++)
                    {
                        string strMVRId = mvrID[iCounter];
                        returnString.Append("<MVR ID='" + mvrID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        //objDataWrapper.AddParameter("@DRIVER_ID",mvrDriverID[iCounter]);
                        objDataWrapper.AddParameter("@APPMVRID", mvrID[iCounter]);
                        // get driver detial
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForPPA_MVRInfoComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</MVR>");

                    }
                    returnString.Append("</MVRS>");
                }



                //*************** End MVR Information	*********************************
                //get the underwriting questions
                returnString.Append("<AUTOGENINFOS>");
                returnString.Append("<AUTOGENINFO>");
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@Policy_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_GenInfoComponent);	// Gen Info		
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</AUTOGENINFO>");
                returnString.Append("</AUTOGENINFOS>");
                returnString.Append("</INPUTXML>");
                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        /// <summary>
        /// Get a  Vehicle Ids against an application
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="polID"></param>
        /// <param name="polVersionID"></param>
        /// <returns>Vehicle Ids  as a carat separated string . if no vehicles exist then it returns -1</returns>
        private string GetVehicleIDs_Pol(int customerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetVehicleIDs_Pol";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strVehicleID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strVehicleID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strVehicleID = strVehicleID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strVehicleID;
        }

        /// <summary>
        /// Gets Drivers Ids against one application for policy rule implementation
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns>Driver Ids as carat separated string </returns>
        private string GetRuleDriverIDs_Pol(int customerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetDrivers_Pol";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            int intCount = ds.Tables[0].Rows.Count;
            string strDriverIds = "-1";
            for (int i = 0; i < intCount; i++)
            {
                if (i == 0)
                {
                    strDriverIds = ds.Tables[0].Rows[i][0].ToString();
                }
                else
                {
                    strDriverIds = strDriverIds + '^' + ds.Tables[0].Rows[i][0].ToString();
                }
            }
            return strDriverIds;
        }

        /// <summary>
        /// Gets MVR Ids against one application for policy rule implementation
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns>MVR Ids as carat separated string </returns>
        private string GetRuleMVRIDs_Pol(int customerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetMVRIDs_Pol";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            int intCount = ds.Tables[0].Rows.Count;
            string strMVRIds = "-1";
            for (int i = 0; i < intCount; i++)
            {
                if (i == 0)
                {
                    strMVRIds = ds.Tables[0].Rows[i][0].ToString();
                }
                else
                {
                    strMVRIds = strMVRIds + '^' + ds.Tables[0].Rows[i][0].ToString();
                }
            }
            return strMVRIds;
        }
        // Motorcycle 
        private string FetchMotorcycleRuleInputXML_Pol(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForMC_VehicleComponent = "Proc_GetMCRule_Motorcycle_Pol";
            string strStoredProcForMC_DriverComponent = "Proc_GetMotorcycleRule_Drivers_Pol";
            string strStoredProcForMC_GenInfoComponent = "Proc_GetMotorcycleRule_GenInfo_Pol";
            string strStoredProcForMC_DriverMVRInfoComponent = "Proc_GetMotorcycleRule_DriverMVR_Pol";
            string strStoredProcForMC_CoverageLimitComponent = "Proc_GetPPARule_CoverageInfo_Pol";
            string strReturnXML = "", strSystemID = "";
            DataSet dsTempXML;
            try
            {
                //				if(IsEODProcess)
                //				{
                //					strSystemID = EODSystemID;
                //				}
                //				else
                //				{
                //					strSystemID  = System.Web.HttpContext.Current.Session["systemId"].ToString(); 
                //				}

                strSystemID = mSystemID;

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                string strResult = GetPolicyLevelInfoForRules(strCalled);
                returnString.Append(strResult);
                // Get the Vehicle Ids against the customerID, PolicyID, PolicyVersionID
                //get the vehicle details for each vehicle				
                string vehicleIDs = GetVehicleIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intVehicleNo = 0;
                string[] vehicleID = new string[0];
                if (vehicleIDs != "-1")
                {
                    vehicleID = vehicleIDs.Split('^');
                    intVehicleNo = vehicleID.Length;
                }
                returnString.Append("<MOTORCYCLES COUNT='" + intVehicleNo + "'>");
                if (vehicleIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < vehicleID.Length; iCounter++)
                    {
                        string strVehicleID = vehicleID[iCounter];
                        returnString.Append("<MOTORCYCLE ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@VEHICLE_ID", vehicleID[iCounter]);
                        // get vehicle details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMC_VehicleComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        //Grandfather case for territory
                        //						strReturnXML = strReturnXML.Replace("<Table>","");
                        //						strReturnXML = strReturnXML.Replace("</Table>","");
                        //						strReturnXML = strReturnXML.Replace("<TERRITORY_DESCRIPTION>","<TERR><TERR_DES>");
                        //						strReturnXML = strReturnXML.Replace("</TERRITORY_DESCRIPTION>","</TERR_DES></TERR>");
                        //*****************************
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        //Added by Manoj Rathore on 27th May 2009 itrack # 5889
                        //1.Coverage

                        returnString.Append("<COVERAGE ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@VEHICLE_ID", vehicleID[iCounter]);
                        objDataWrapper.AddParameter("@USER", strSystemID);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMC_CoverageLimitComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);

                        //Repalce Table1 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table1>", "");
                        strReturnXML = strReturnXML.Replace("</Table1>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_DES>", "<COV><COV_DES>");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_DES>", "</COV_DES></COV>");
                        //
                        //Replace Table2 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table2>", "");
                        strReturnXML = strReturnXML.Replace("</Table2>", "");
                        strReturnXML = strReturnXML.Replace("<LIMIT_DESCRIPTION>", "<LIMIT><LIMIT_DES>");
                        strReturnXML = strReturnXML.Replace("</LIMIT_DESCRIPTION>", "</LIMIT_DES></LIMIT>");
                        //********************
                        //Replace Table3 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table3>", "");
                        strReturnXML = strReturnXML.Replace("</Table3>", "");
                        strReturnXML = strReturnXML.Replace("<DEDUCTIBLE_DESCRIPTION>", "<DEDUCT><DEDUCT_DES>");
                        strReturnXML = strReturnXML.Replace("</DEDUCTIBLE_DESCRIPTION>", "</DEDUCT_DES></DEDUCT>");

                        returnString.Append(strReturnXML);
                        returnString.Append("</COVERAGE>");
                        //End Itrack # 5889
                        returnString.Append("</MOTORCYCLE>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</MOTORCYCLES>");
                //get the driver details for each driver			
                string driverIDs = GetRuleDriverIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intDriverNo = 0;
                string[] driverID = new string[0];
                if (driverIDs != "-1")
                {
                    driverID = driverIDs.Split('^');
                    intDriverNo = driverID.Length;
                }
                returnString.Append("<DRIVERS COUNT='" + intDriverNo + "'>");
                if (driverIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < driverID.Length; iCounter++)
                    {
                        string strDriverId = driverID[iCounter];
                        returnString.Append("<DRIVER ID='" + driverID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@DRIVER_ID", driverID[iCounter]);
                        // get driver detial
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMC_DriverComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</DRIVER>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</DRIVERS>");
                //************** Start Get MVR Information *****************
                string mvrIDs = GetRuleMVRIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intmvrNo = 0;
                string[] mvrID = new string[0];
                if (mvrIDs != "-1")
                {
                    mvrID = mvrIDs.Split('^');
                    intmvrNo = mvrID.Length;
                }
                if (mvrIDs != "-1")
                {
                    returnString.Append("<MVRS COUNT='" + intmvrNo + "'>");
                    // Run a loop to get the inputXML for each MVRIDS
                    for (int iCounter = 0; iCounter < mvrID.Length; iCounter++)
                    {
                        string strMVRId = mvrID[iCounter];
                        returnString.Append("<MVR ID='" + mvrID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        //objDataWrapper.AddParameter("@DRIVER_ID",mvrDriverID[iCounter]);
                        objDataWrapper.AddParameter("@APPMVRID", mvrID[iCounter]);
                        // get driver detial
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMC_DriverMVRInfoComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</MVR>");
                    }
                    returnString.Append("</MVRS>");
                }


                //End MVR Information	
                //get the underwriting questions
                returnString.Append("<MOTOTRCYCLEGENINFOS>");
                returnString.Append("<MOTOTRCYCLEGENINFO>");
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMC_GenInfoComponent);	// Gen Info		
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</MOTOTRCYCLEGENINFO>");
                returnString.Append("</MOTOTRCYCLEGENINFOS>");
                returnString.Append("</INPUTXML>");
                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        // HO
        private string FetchHORuleInputXML_Pol(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForHO_LocationComponet = "Proc_GetHORule_LocationInfo_Pol";
            string strStoredProcForHO_DwellingComponent = "Proc_GetHORule_DwellingInfo_Pol";
            string strStoredProcForHO_RatingComponent = "Proc_GetHORule_RatingInfo_Pol";
            string strStoredProcForHO_CoverageLimitComponent = "Proc_GetHORule_DwellingCoverageInfo_Pol";
            string strStoredProcForHO_GenInfoComponent = "Proc_GetHORule_GenInfo_Pol";
            string strStoredProcForHOSpl_GenInfoComponent = "Proc_GetHORule_Sch_Items_Cvgs_Pol";
            string strStoredProcForHO_SolidFuelComponent = "Proc_GetHORule_SolidFuelInfo_Pol";
            string strStoredProcForHO_RVInfoComponent = "Proc_GetHORule_RVInfo_Pol";
            string strStoredProcForHO_EndorsementComponent = "Proc_GetHORule_Endorsement_Pol";
            string strStoredProcForHO_AddInterest = "Proc_GetHORule_AddInterest_Pol";
            string strStoredProcForHO_BillingInfo = "Proc_GetFireRule_BillingInfo_Pol";
            string strReturnXML = "";
            DataSet dsTempXML;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);

                string strResult = GetPolicyLevelInfoForRules(strCalled);
                returnString.Append(strResult);
                //********* Check whether login user is agency.
                string strSystemID = mSystemID; //System.Web.HttpContext.Current.Session["systemId"].ToString(); 
                //*********
                // Get the Location Ids against the customerID, PolicyID, PolicyVersionID
                //get the Location details for each location				
                string LocationIDs = GetLocationIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intLocNo = 0;
                string[] LocationID = new string[0];
                if (LocationIDs != "-1")
                {
                    LocationID = LocationIDs.Split('^');
                    intLocNo = LocationID.Length;
                }
                returnString.Append("<LOCATIONS COUNT='" + intLocNo + "'>");
                if (LocationIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < LocationID.Length; iCounter++)
                    {
                        string strLocationID = LocationID[iCounter];
                        returnString.Append("<LOCATION ID='" + LocationID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@LOCATION_ID", LocationID[iCounter]);
                        // get Location details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_LocationComponet);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</LOCATION>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</LOCATIONS>");


                string PlanIDs = GetPlanIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intPlanNo = 0;
                string[] PlanID = new string[0];
                if (PlanIDs != "-1")
                {
                    PlanID = PlanIDs.Split('^');
                    intPlanNo = PlanID.Length;
                }
                returnString.Append("<BillingInfoS COUNT='" + intPlanNo + "'>");
                if (PlanIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < PlanID.Length; iCounter++)
                    {
                        string strLocationID = PlanID[iCounter];
                        returnString.Append("<Plan ID='" + PlanID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@PLAN_ID", PlanID[iCounter]);
                        // get Location details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_BillingInfo);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</Plan>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</BillingInfoS>");





                //get the Dwelling details for each dwelling
                string dwellingIDs = GetDwellingIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intDwellingNo = 0;
                string[] dwellingID = new string[0];
                if (dwellingIDs != "-1")
                {
                    dwellingID = dwellingIDs.Split('^');
                    intDwellingNo = dwellingID.Length;
                }
                returnString.Append("<DWELLINGS COUNT='" + intDwellingNo + "'>");
                if (dwellingIDs != "-1")
                {
                    // Run a loop to get the inputXML for each DwellingID
                    for (int iCounter = 0; iCounter < dwellingID.Length; iCounter++)
                    {

                        returnString.Append("<DWELLINGINFO>");
                        // 1. Dwelling
                        string strDwellingId = dwellingID[iCounter];
                        returnString.Append("<DWELLING ID='" + dwellingID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@DWELLING_ID", dwellingID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_DwellingComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</DWELLING>");
                        //2. Rating Info string 
                        //strDwellingId = dwellingID[iCounter];
                        //returnString.Append("<RATINGINFO ID='" + dwellingID[iCounter] + "'>");
                        //objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        //objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        //objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        //objDataWrapper.AddParameter("@DWELLING_ID", dwellingID[iCounter]);
                        //dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_RatingComponent);
                        //objDataWrapper.ClearParameteres();
                        //strReturnXML = dsTempXML.GetXml();
                        //strReturnXML = ReplaceString(strReturnXML);
                        //returnString.Append(strReturnXML);
                        //returnString.Append("</RATINGINFO>");
                        //3. Additional Interest string 
                        strDwellingId = dwellingID[iCounter];
                        returnString.Append("<ADDINTEREST ID='" + dwellingID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@DWELLING_ID", dwellingID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_AddInterest);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</ADDINTEREST>");
                        //4.Coverage
                        strDwellingId = dwellingID[iCounter];
                        returnString.Append("<COVERAGE ID='" + dwellingID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@DWELLING_ID", dwellingID[iCounter]);
                        objDataWrapper.AddParameter("@USER", strSystemID);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_CoverageLimitComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);

                        //Repalce Table1 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table1>", "");
                        strReturnXML = strReturnXML.Replace("</Table1>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_DES>", "<COV><COV_DES>");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_DES>", "</COV_DES></COV>");
                        //
                        //Replace Table2 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table2>", "");
                        strReturnXML = strReturnXML.Replace("</Table2>", "");
                        strReturnXML = strReturnXML.Replace("<LIMIT_DESCRIPTION>", "<LIMIT><LIMIT_DES>");
                        strReturnXML = strReturnXML.Replace("</LIMIT_DESCRIPTION>", "</LIMIT_DES></LIMIT>");
                        //********************
                        //Replace Table3 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table3>", "");
                        strReturnXML = strReturnXML.Replace("</Table3>", "");
                        strReturnXML = strReturnXML.Replace("<DEDUCTIBLE_DESCRIPTION>", "<DEDUCT><DEDUCT_DES>");
                        strReturnXML = strReturnXML.Replace("</DEDUCTIBLE_DESCRIPTION>", "</DEDUCT_DES></DEDUCT>");
                        //Replace Table4 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table4>", "");
                        strReturnXML = strReturnXML.Replace("</Table4>", "");
                        strReturnXML = strReturnXML.Replace("<ADDDEDUCTIBLE_DESCRIPTION>", "<ADDDEDUCT><ADDDEDUCT_DES>");
                        strReturnXML = strReturnXML.Replace("</ADDDEDUCTIBLE_DESCRIPTION>", "</ADDDEDUCT_DES></ADDDEDUCT>");


                        returnString.Append(strReturnXML);
                        returnString.Append("</COVERAGE>");
                        //4. Endorsement : Modified 20 June 2006
                        strDwellingId = dwellingID[iCounter];
                        returnString.Append("<ENDORSEMENT>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@DWELLING_ID", dwellingID[iCounter]);

                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_EndorsementComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</ENDORSEMENT>");

                        //End 
                        returnString.Append("</DWELLINGINFO>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</DWELLINGS>");

                // RV Info  start

                string RV_VehicleIDs = GetRV_VehicleIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intRV_VehNo = 0;
                string[] RV_VehicleID = new string[0];
                if (RV_VehicleIDs != "-1")
                {
                    RV_VehicleID = RV_VehicleIDs.Split('^');
                    intRV_VehNo = RV_VehicleID.Length;
                }
                returnString.Append("<RV_VEHICLES COUNT='" + intRV_VehNo + "'>");
                if (RV_VehicleIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < RV_VehicleID.Length; iCounter++)
                    {
                        //string strLocationID=RV_VehicleID[iCounter];
                        returnString.Append("<RV_VEHICLE RV_VEHICLEID='" + RV_VehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@RV_VEHICLEID", RV_VehicleID[iCounter]);

                        // get  RV Vehicle details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_RVInfoComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</RV_VEHICLE>");
                    }
                }
                /*else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";					
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }*/

                returnString.Append("</RV_VEHICLES>");

                //	RV INFO END 

                //get the underwriting questions
                returnString.Append("<HOGENINFOS>");
                returnString.Append("<HOGENINFO>");
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_GenInfoComponent);	// Gen Info		
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</HOGENINFO>");
                returnString.Append("</HOGENINFOS>");
                // Get the fuel Ids against the customerID, PolicyID, PolicyVersionID
                //get the fuel details for each fuel ID
                string strFuelIDs = GetFuelIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intFuelNo = 0;
                string[] arrFuelID = new string[0];
                if (strFuelIDs != "-1")
                {
                    arrFuelID = strFuelIDs.Split('^');
                    intFuelNo = arrFuelID.Length;
                }
                returnString.Append("<FUELINFOS COUNT='" + intFuelNo + "'>");
                if (strFuelIDs != "-1")
                {
                    // Run a loop to get the inputXML for each fuel ID
                    for (int iCounter = 0; iCounter < arrFuelID.Length; iCounter++)
                    {
                        string strFuelID = arrFuelID[iCounter];
                        returnString.Append("<FUELINFO ID='" + arrFuelID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@FUEL_ID", arrFuelID[iCounter]);
                        // get Location details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_SolidFuelComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</FUELINFO>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<FUELERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</FUELINFOS>");
                //get the scheduled items coverages (Inland Marine)
                returnString.Append("<SCHEDULEDITEMS>");
                returnString.Append("<SCHEDULEDITEM>");
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHOSpl_GenInfoComponent);	// Gen Info		
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</SCHEDULEDITEM>");
                returnString.Append("</SCHEDULEDITEMS>");
                //Watercraft rule applied if Boat attached with Homeowners Id
                string boatwithhomeowner = ClsGeneralInformation.GetBoatWithHomeowner(customerID, PolicyID, PolicyVersionID, "POL");
                if (boatwithhomeowner == "1")
                {
                    strCalled = "FromHomeowner";
                    string strWatercraftRuleInputXML = FetchWatercraftRuleInputXML_Pol(strCalled);
                    returnString.Append(strWatercraftRuleInputXML);
                }
                returnString.Append("</INPUTXML>");
                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        /// <summary>
        /// Gets a  Fuel Ids against an policy
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns>Fuel Ids  as a carat separated string . if no fuel id exist then it returns -1</returns>
        private string GetFuelIDs_Pol(int customerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_FuelIDs_Pol";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strFuelID = "-1";
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
                {
                    int intCount = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < intCount; i++)
                    {
                        if (i == 0)
                        {
                            strFuelID = ds.Tables[0].Rows[i][0].ToString();
                        }
                        else
                        {
                            strFuelID = strFuelID + '^' + ds.Tables[0].Rows[i][0].ToString();
                        }
                    }
                }
            }
            return strFuelID;
        }
        /// <summary>
        /// Gets a  Vehicle Ids against an application
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <returns>Vehicle Ids  as a carat separated string . if no vehicle exist then it returns -1</returns>
        public static string GetRV_VehicleIDs_Pol(int customerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetRV_VehicleIDs_Pol";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strRV_VehicleID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strRV_VehicleID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strRV_VehicleID = strRV_VehicleID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strRV_VehicleID;
        }

        /// <summary>
        /// Gets a  Location Ids against an application
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns>Location Ids  as a carat separated string . if no location exist then it returns -1</returns>
        private string GetDwellingIDs_Pol(int customerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetDwellingIDs_Pol";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strDwellingID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strDwellingID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strDwellingID = strDwellingID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strDwellingID;
        }


        /// <summary>
        /// Gets a  Location Ids against an application
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns>Location Ids  as a carat separated string . if no location exist then it returns -1</returns>
        private string GetLocationIDs_Pol(int customerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetLocationIDs_Pol";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strLocationID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strLocationID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strLocationID = strLocationID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strLocationID;
        }


        public string GetPlanIDs_Pol(int customerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetBillingIDs_Pol";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strPlanID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strPlanID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strPlanID = strPlanID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strPlanID;
        }

        //RD
        private string FetchRentalDwellingRuleInputXML_Pol(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForRD_LocationComponet = "Proc_GetRDRule_LocationInfo_Pol";
            string strStoredProcForRD_DwellingComponent = "Proc_GetRDRule_DwellingInfo_Pol";
            string strStoredProcForRD_RatingComponent = "Proc_GetRDRule_RatingInfo_Pol";
            string strStoredProcForRD_CoverageLimitComponent = "Proc_GetRDRule_DwellingCoverageInfo_Pol";
            string strStoredProcForRD_GenInfoComponent = "Proc_GetRDRule_GenInfo_Pol";
            string strStoredProcForRD_AddInterest = "Proc_GetRDRule_AddInterest_Pol";
            string strReturnXML = "";
            DataSet dsTempXML;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                string strResult = GetPolicyLevelInfoForRules(strCalled);
                returnString.Append(strResult);
                //********* Check whether login user is agency.
                string strSystemID = mSystemID; //System.Web.HttpContext.Current.Session["systemId"].ToString(); 
                //*********
                // Get the Location Ids against the customerID, PolicyID, PolicyVersionID
                //get the Location details for each location				
                string LocationIDs = GetLocationIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intLocNo = 0;
                string[] LocationID = new string[0];
                if (LocationIDs != "-1")
                {
                    LocationID = LocationIDs.Split('^');
                    intLocNo = LocationID.Length;
                }
                returnString.Append("<LOCATIONS COUNT='" + intLocNo + "'>");
                if (LocationIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < LocationID.Length; iCounter++)
                    {
                        string strLocationID = LocationID[iCounter];
                        returnString.Append("<LOCATION ID='" + LocationID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@LOCATION_ID", LocationID[iCounter]);
                        // get Location details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForRD_LocationComponet);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</LOCATION>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</LOCATIONS>");

                //get the Dwelling details for each dwelling
                string dwellingIDs = GetDwellingIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intDwellingNo = 0;
                string[] dwellingID = new string[0];
                if (dwellingIDs != "-1")
                {
                    dwellingID = dwellingIDs.Split('^');
                    intDwellingNo = dwellingID.Length;
                }
                returnString.Append("<DWELLINGS COUNT='" + intDwellingNo + "'>");
                if (dwellingIDs != "-1")
                {
                    // Run a loop to get the inputXML for each DwellingID
                    for (int iCounter = 0; iCounter < dwellingID.Length; iCounter++)
                    {

                        returnString.Append("<DWELLINGINFO>");
                        // 1. Dwelling
                        string strDwellingId = dwellingID[iCounter];
                        returnString.Append("<DWELLING ID='" + dwellingID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@DWELLING_ID", dwellingID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForRD_DwellingComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</DWELLING>");
                        //2. Rating Info string 
                        strDwellingId = dwellingID[iCounter];
                        returnString.Append("<RATINGINFO ID='" + dwellingID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@DWELLING_ID", dwellingID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForRD_RatingComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</RATINGINFO>");
                        //3. Additional Interest string 
                        strDwellingId = dwellingID[iCounter];
                        returnString.Append("<ADDINTEREST ID='" + dwellingID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@DWELLING_ID", dwellingID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForRD_AddInterest);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</ADDINTEREST>");
                        //4.Coverage
                        strDwellingId = dwellingID[iCounter];
                        returnString.Append("<COVERAGE ID='" + dwellingID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@DWELLING_ID", dwellingID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        objDataWrapper.AddParameter("@USER", strSystemID);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForRD_CoverageLimitComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);

                        //Repalce Table1 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table1>", "");
                        strReturnXML = strReturnXML.Replace("</Table1>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_DES>", "<COV><COV_DES>");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_DES>", "</COV_DES></COV>");
                        //
                        //Replace Table2 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table2>", "");
                        strReturnXML = strReturnXML.Replace("</Table2>", "");
                        strReturnXML = strReturnXML.Replace("<LIMIT_DESCRIPTION>", "<LIMIT><LIMIT_DES>");
                        strReturnXML = strReturnXML.Replace("</LIMIT_DESCRIPTION>", "</LIMIT_DES></LIMIT>");
                        //********************
                        //Replace Table3 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table3>", "");
                        strReturnXML = strReturnXML.Replace("</Table3>", "");
                        strReturnXML = strReturnXML.Replace("<DEDUCTIBLE_DESCRIPTION>", "<DEDUCT><DEDUCT_DES>");
                        strReturnXML = strReturnXML.Replace("</DEDUCTIBLE_DESCRIPTION>", "</DEDUCT_DES></DEDUCT>");
                        //********************
                        //Replace Table4 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table4>", "");
                        strReturnXML = strReturnXML.Replace("</Table4>", "");
                        strReturnXML = strReturnXML.Replace("<ADDDEDUCTIBLE_DESCRIPTION>", "<ADDDEDUCT><ADDDEDUCT_DES>");
                        strReturnXML = strReturnXML.Replace("</ADDDEDUCTIBLE_DESCRIPTION>", "</ADDDEDUCT_DES></ADDDEDUCT>");
                        //***********************
                        returnString.Append(strReturnXML);
                        returnString.Append("</COVERAGE>");
                        returnString.Append("</DWELLINGINFO>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</DWELLINGS>");
                //get the underwriting questions
                returnString.Append("<RDGENINFOS>");
                returnString.Append("<RDGENINFO>");
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForRD_GenInfoComponent);	// Gen Info		
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</RDGENINFO>");
                returnString.Append("</RDGENINFOS>");
                returnString.Append("</INPUTXML>");
                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        /// <summary>
        /// Gets MVR Ids against one application for policy rule implementation
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="pOLICY Id"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns>MVR Ids as carat separated string </returns>
        private string GetRuleWatMVRIDs(int customerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetWatMVRIDs_Pol";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            int intCount = ds.Tables[0].Rows.Count;
            string strMVRIds = "-1";
            for (int i = 0; i < intCount; i++)
            {
                if (i == 0)
                {
                    strMVRIds = ds.Tables[0].Rows[i]["DRIVER_ID"].ToString() + "~" + ds.Tables[0].Rows[i]["APP_WATER_MVR_ID"].ToString();
                }
                else
                {
                    strMVRIds = strMVRIds + '^' + ds.Tables[0].Rows[i]["DRIVER_ID"].ToString() + "~" + ds.Tables[0].Rows[i]["APP_WATER_MVR_ID"].ToString();
                }
            }
            return strMVRIds;
        }

        #region LOAD DEFAULT RULES XML
        public string GetRulesDefaultXml()
        {
            XmlDocument DataDoc = new XmlDocument();
            string strXmlFilePath = ClsCommon.GetKeyValueWithIP("RULES_DEFAULT");
            //DataDoc.Load("D://RULES_DEFAULT.XML");
            DataDoc.Load(strXmlFilePath);
            string strRulesXml = DataDoc.InnerXml;
            strRulesXml = strRulesXml.Replace("\"", "'");
            return strRulesXml;
        }
        #endregion

        //Watercraft
        private string FetchWatercraftRuleInputXML_Pol(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForWatercraft_BoatComponent = "Proc_GetWatercraftRule_Boat_Pol";
            string strStoredProcForWatercraft_OperatorComponent = "Proc_GetWatercraftRule_Operators_Pol";
            string strStoredProcForWatercraft_GenInfoComponent = "Proc_GetWatercraftRule_GenInfo_Pol";
            string strStoredProcForWatercraft_EquipmentComponent = "PROC_GETWATERCRAFTRULE_EQUIP_POL";
            string strStoredProcForWatercraft_TrailerInfoComponent = "Proc_GetWatercraftRule_TrailerInfo_Pol";
            string strStoredProcForWatercraft_MVRInfoComponent = "Proc_GetWatercraftRule_MVRInfo_Pol";
            string strReturnXML = "";
            DataSet dsTempXML;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                string strResult = GetPolicyLevelInfoForRules(strCalled);
                returnString.Append(strResult);
                // Check whether login user is agency.
                string strSystemID = mSystemID; //System.Web.HttpContext.Current.Session["systemId"].ToString(); 
                //
                string strRulesDefValue = GetRulesDefaultXml();
                // Get the Vehicle Ids against the customerID, PolicyID, PolicyVersionID
                //get the Boat details for each vehicle
                string strBoatID = GetBoatIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intBoatNo = 0;
                string[] BoatID = new string[0];
                if (strBoatID != "-1")
                {
                    BoatID = strBoatID.Split('^');
                    intBoatNo = BoatID.Length;
                }
                returnString.Append("<BOATS COUNT='" + intBoatNo + "'>");
                if (strBoatID != "-1")
                {
                    string[] vehicleID = new string[0];
                    vehicleID = strBoatID.Split('^');
                    // Run a loop to get the inputXML for each BOATID
                    for (int iCounter = 0; iCounter < vehicleID.Length; iCounter++)
                    {
                        returnString.Append("<BOAT ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@BOAT_ID", vehicleID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        objDataWrapper.AddParameter("@USER", strSystemID);
                        objDataWrapper.AddParameter("@DEFAULT_DOC", strRulesDefValue);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_BoatComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);

                        //Repalce Table1 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table1>", "");
                        strReturnXML = strReturnXML.Replace("</Table1>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_DES>", "<COV><COV_DES>");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_DES>", "</COV_DES></COV>");
                        //Replace Table2 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table2>", "");
                        strReturnXML = strReturnXML.Replace("</Table2>", "");
                        strReturnXML = strReturnXML.Replace("<LIMIT_DESCRIPTION>", "<LIMIT><LIMIT_DES>");
                        strReturnXML = strReturnXML.Replace("</LIMIT_DESCRIPTION>", "</LIMIT_DES></LIMIT>");
                        //********************
                        //Replace Table3 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table3>", "");
                        strReturnXML = strReturnXML.Replace("</Table3>", "");
                        strReturnXML = strReturnXML.Replace("<DEDUCTIBLE_DESCRIPTION>", "<DEDUCT><DEDUCT_DES>");
                        strReturnXML = strReturnXML.Replace("</DEDUCTIBLE_DESCRIPTION>", "</DEDUCT_DES></DEDUCT>");
                        //********************
                        int rowID = iCounter + 1;
                        string vehicleRowIDNode = "<BOATID>" + rowID.ToString() + "</BOATID>";
                        returnString.Append(vehicleRowIDNode);
                        returnString.Append(strReturnXML);
                        returnString.Append("</BOAT>");

                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<BOATERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</BOATS>");
                //get the Operator details for each operator

                string driverIDs = GetRuleWCDriverIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intOperatorNo = 0;
                string[] OperatorID = new string[0];
                if (driverIDs != "-1")
                {
                    OperatorID = driverIDs.Split('^');
                    intOperatorNo = OperatorID.Length;
                }
                returnString.Append("<OPERATORS COUNT='" + intOperatorNo + "'>");
                if (driverIDs != "-1")
                {
                    string[] driverID = new string[0];
                    driverID = driverIDs.Split('^');
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < driverID.Length; iCounter++)
                    {
                        returnString.Append("<OPERATOR ID='" + driverID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@DRIVER_ID", driverID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_OperatorComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</OPERATOR>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<OPERATORERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</OPERATORS>");
                //************** Start Get MVR Information **************************
                string mvrIDs = GetRuleWatMVRIDs(customerID, PolicyID, PolicyVersionID);
                int intmvrNo = 0;
                string[] mvrID = new string[0];
                if (mvrIDs != "-1")
                {
                    mvrID = mvrIDs.Split('^');
                    intmvrNo = mvrID.Length;
                }

                if (mvrIDs != "-1")
                {
                    returnString.Append("<MVRS COUNT='" + intmvrNo + "'>");
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < mvrID.Length; iCounter++)
                    {
                        string strMVRId = mvrID[iCounter];
                        string[] mvrdRIDs = strMVRId.Split('~');
                        string strDriverId = mvrdRIDs[0];
                        string MVRId = mvrdRIDs[1];

                        returnString.Append("<MVR ID='" + MVRId + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@DRIVER_ID", strDriverId);
                        objDataWrapper.AddParameter("@APPMVRID", MVRId);
                        // get driver detial
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_MVRInfoComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</MVR>");
                    }
                    returnString.Append("</MVRS>");
                }

                //********************* End MVR Information	************************************
                //	TRAILER INFO
                returnString.Append("<TRAILERS >");
                string strTrailerIDs = GetPolTrailerDs_Pol(customerID, PolicyID, PolicyVersionID);
                //				int intTrailerNo=0;
                //				string[] TrailerID=new string[0];
                //				if(strTrailerIDs !="-1")
                //				{
                //					TrailerID=strTrailerIDs.Split('^');
                //					intTrailerNo=TrailerID.Length;
                //				}
                //returnString.Append("<TRAILERS COUNT='" + intTrailerNo + "'>");				
                if (strTrailerIDs != "-1")
                {
                    string[] arrTrailerID = new string[0];
                    arrTrailerID = strTrailerIDs.Split('^');
                    // Run a loop to get the inputXML for each equipment ID
                    for (int iCounterForEquip = 0; iCounterForEquip < arrTrailerID.Length; iCounterForEquip++)
                    {
                        returnString.Append("<TRAILER ID='" + arrTrailerID[iCounterForEquip] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@TRAILER_ID", arrTrailerID[iCounterForEquip]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_TrailerInfoComponent); // Trailer Info		
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</TRAILER>");

                    }
                }
                //				else
                //				{
                //					returnString.Append("<ERRORS>");
                //					strReturnXML = "<TRAILERERROR ERRFOUND = 'T'/>";					
                //					returnString.Append(strReturnXML);
                //					returnString.Append("</ERRORS>");
                //				}
                returnString.Append("</TRAILERS>");

                /*GET EQUIPMENT DETAIL FOR WC  PK*/
                returnString.Append("<WATERCRAFTEQUIPMENTS>");
                string strEquipIDs = clsWatercraftInformation.GetEquipmentID_Pol(customerID, PolicyID, PolicyVersionID);
                if (strEquipIDs != "-1")
                {
                    string[] arrEquipID = new string[0];
                    arrEquipID = strEquipIDs.Split('^');
                    // Run a loop to get the inputXML for each equipment ID
                    for (int iCounterForEquip = 0; iCounterForEquip < arrEquipID.Length; iCounterForEquip++)
                    {
                        returnString.Append("<WATERCRAFTEQUIPMENT ID='" + arrEquipID[iCounterForEquip] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                        objDataWrapper.AddParameter("@EQUIP_ID", arrEquipID[iCounterForEquip]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_EquipmentComponent); // Equipment Info		
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</WATERCRAFTEQUIPMENT>");
                    }

                }
                returnString.Append("</WATERCRAFTEQUIPMENTS>");

                //get the underwriting questions for watercraft
                ClsWatercraftGenInformation obj = new ClsWatercraftGenInformation();
                int intWCGenCount = obj.FetchData_Pol(PolicyID, customerID, PolicyVersionID).Tables[0].Rows.Count;
                returnString.Append("<WATERCRAFTGENINFOS COUNT='" + intWCGenCount + "'>");
                if (intWCGenCount > 0)
                {
                    returnString.Append("<WATERCRAFTGENINFO>");
                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                    objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_GenInfoComponent);	// Gen Info		
                    objDataWrapper.ClearParameteres();
                    strReturnXML = dsTempXML.GetXml();
                    strReturnXML = ReplaceString(strReturnXML);
                    returnString.Append(strReturnXML);
                    returnString.Append("</WATERCRAFTGENINFO>");
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<WATERCRAFTGENINFOERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</WATERCRAFTGENINFOS>");
                if (strCalled != "FromHomeowner")
                {
                    returnString.Append("</INPUTXML>");
                }
                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }


        /// <summary>
        ///  Policy rule input xml for Umbrella
        /// </summary>
        /// <returns></returns>
        private string FetchUmbrellaRuleInputXML_Pol(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForUM_LocationComponet = "Proc_GetUMRule_LocationInfo_Pol";
            string strStoredProcForUM_BoatComponent = "Proc_GetUMRule_Boat_Pol";
            string strStoredProcForUM_RecrVehicleComponent = "Proc_GetUMRule_Rec_Vehicle_Pol";
            string strStoredProcForUM_UnderLayingofScheduleComponent = "Proc_GetUMRule_SchdUnderlayingInfo_Pol";
            string strStoredProcForUM_GenInfoComponent = "Proc_GetUMRule_GenInfo_Pol";
            string strStoredProcForRD_ExcesslimitEndorsements = "Proc_GetUMRule_Excesslimit_Endorsements_Pol";
            string strStoredProcForUM_DriverComponent = "";
            string strReturnXML = "";
            DataSet dsTempXML;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                string strResult = GetPolicyLevelInfoForRules(strCalled);
                returnString.Append(strResult);

                //********* Check whether login user is agency.
                string strSystemID = mSystemID; // System.Web.HttpContext.Current.Session["systemId"].ToString(); 
                //*********
                // Get the Location Ids against the customerID, PolicyID, PolicyVersionID
                //get the Location details for each location				
                string LocationIDs = ClsGeneralInformation.Get_PolUMLocationIDs(customerID, PolicyID, PolicyVersionID);
                int intLocNo = 0;
                string[] LocationID = new string[0];
                if (LocationIDs != "-1")
                {
                    LocationID = LocationIDs.Split('^');
                    intLocNo = LocationID.Length;
                }
                returnString.Append("<LOCATIONS COUNT='" + intLocNo + "'>");
                if (LocationIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < LocationID.Length; iCounter++)
                    {
                        string strLocationID = LocationID[iCounter];
                        returnString.Append("<LOCATION ID='" + LocationID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@POLICYID", PolicyID);
                        objDataWrapper.AddParameter("@POLICYVERSIONID", PolicyVersionID);
                        objDataWrapper.AddParameter("@LOCATIONID", LocationID[iCounter]);
                        // get Location details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForUM_LocationComponet);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</LOCATION>");
                    }
                }
                //				else
                //				{
                //					returnString.Append("<ERRORS>");
                //					strReturnXML = "<ERROR ERRFOUND = 'T'/>";					
                //					returnString.Append(strReturnXML);
                //					returnString.Append("</ERRORS>");
                //				}
                returnString.Append("</LOCATIONS>");

                //get the Boat details for each vehicle
                string strBoatID = clsWatercraftInformation.GetUMBoatIDs_Pol(customerID, PolicyID, PolicyVersionID);
                int intBoatNo = 0;
                string[] BoatID = new string[0];
                if (strBoatID != "-1")
                {
                    BoatID = strBoatID.Split('^');
                    intBoatNo = BoatID.Length;
                }
                returnString.Append("<BOATS COUNT='" + intBoatNo + "'>");
                if (strBoatID != "-1")
                {
                    string[] vehicleID = new string[0];
                    vehicleID = strBoatID.Split('^');
                    // Run a loop to get the inputXML for each BOATID
                    for (int iCounter = 0; iCounter < vehicleID.Length; iCounter++)
                    {
                        returnString.Append("<BOAT ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@POLICYID", PolicyID);
                        objDataWrapper.AddParameter("@POLICYVERSIONID", PolicyVersionID);
                        objDataWrapper.AddParameter("@BOATID", vehicleID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForUM_BoatComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        //Repalce Table1 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table1>", "");
                        strReturnXML = strReturnXML.Replace("</Table1>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_DES>", "<COV><COV_DES>");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_DES>", "</COV_DES></COV>");
                        //Add node for each vehicle  for count
                        int rowID = iCounter + 1;
                        string vehicleRowIDNode = "<BOATID>" + rowID.ToString() + "</BOATID>";
                        returnString.Append(vehicleRowIDNode);
                        returnString.Append(strReturnXML);
                        returnString.Append("</BOAT>");

                    }
                }
                //				else
                //				{
                //					returnString.Append("<ERRORS>");
                //					strReturnXML = "<BOATERROR ERRFOUND = 'T'/>";					
                //					returnString.Append(strReturnXML);
                //					returnString.Append("</ERRORS>");
                //				}
                returnString.Append("</BOATS>");
                // Recreational Vehicle
                //get the  Recreational vehicle Ids			
                string RVehicleIDs = ClsHomeRecrVehicles.GetPolRecreationVehicleIDs(customerID, PolicyID, PolicyVersionID);
                int intVehicleNo = 0;
                string[] RVehicleID = new string[0];
                if (RVehicleIDs != "-1")
                {
                    RVehicleID = RVehicleIDs.Split('^');
                    intVehicleNo = RVehicleID.Length;
                }
                returnString.Append("<RVEHICLES COUNT='" + intVehicleNo + "'>");

                // Check whether login user is agency.
                if (RVehicleIDs != "-1")
                {
                    // Run a loop to get the inputXML for each RVehicleID
                    for (int iCounter = 0; iCounter < RVehicleID.Length; iCounter++)
                    {
                        string strVehicleID = RVehicleID[iCounter];
                        returnString.Append("<RVEHICLE ID='" + RVehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@POLICYID", PolicyID);
                        objDataWrapper.AddParameter("@POLICYVERSIONID", PolicyVersionID);
                        objDataWrapper.AddParameter("@VEHICLEID", RVehicleID[iCounter]);

                        // get vehicle details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForUM_RecrVehicleComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</RVEHICLE>");
                    }
                }
                //				else
                //				{
                //					returnString.Append("<ERRORS>");
                //					strReturnXML = "<ERROR ERRFOUND = 'T'/>";					
                //					returnString.Append(strReturnXML);
                //					returnString.Append("</ERRORS>");
                //				}
                returnString.Append("</RVEHICLES>");
                // Umbrella Drivers 
                //get the driver details for each driver				
                //				string driverIDs = ClsDriverDetail.GetUMRuleDriverIDs(customerID,PolicyID,PolicyVersionID);
                //				int intDriverNo=0;
                //				string[] driverID = new string[0];
                //				if(driverIDs !="-1")
                //				{
                //					driverID = driverIDs.Split('^');					 
                //					intDriverNo=driverID.Length;	
                //				}
                //				returnString.Append("<DRIVERS COUNT='"+ intDriverNo +"'>");				
                //				if(driverIDs != "-1")
                //				{
                //					// Run a loop to get the inputXML for each vehicleID
                //					for (int iCounter=0; iCounter<driverID.Length ; iCounter++)
                //					{			
                //						string strDriverId=driverID[iCounter];
                //						returnString.Append("<DRIVER ID='"+ driverID[iCounter]+"'>");
                //						objDataWrapper.AddParameter("@CUSTOMERID",customerID);
                //						objDataWrapper.AddParameter("@POLICYID",PolicyID);
                //						objDataWrapper.AddParameter("@POLICYVERSIONID",PolicyVersionID);	
                //						objDataWrapper.AddParameter("@DRIVERID",driverID[iCounter]);						
                //						// get driver detial
                //						dsTempXML =	objDataWrapper.ExecuteDataSet(strStoredProcForUM_DriverComponent);			
                //						objDataWrapper.ClearParameteres();
                //						strReturnXML = dsTempXML.GetXml();
                //						strReturnXML= ReplaceString(strReturnXML);
                //						returnString.Append(strReturnXML);						
                //						returnString.Append("</DRIVER>");
                //					}
                //				}
                //				else
                //				{
                //					returnString.Append("<ERRORS>");
                //					strReturnXML = "<ERROR ERRFOUND = 'T'/>";					
                //					returnString.Append(strReturnXML);
                //					returnString.Append("</ERRORS>");
                //				}
                //				returnString.Append("</DRIVERS>");



                // Schedule of Underlaying 				
                returnString.Append("<SCHEDULES>");
                returnString.Append("<SCHEDULE>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@POLICYID", PolicyID);
                objDataWrapper.AddParameter("@POLICYVERSIONID", PolicyVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForUM_UnderLayingofScheduleComponent);
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</SCHEDULE>");
                returnString.Append("</SCHEDULES>");

                //Excess Limit and Endorsements
                returnString.Append("<EXCESSLIMITS>");
                returnString.Append("<EXCESSLIMIT>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@POLICYID", PolicyID);
                objDataWrapper.AddParameter("@POLICYVERSIONID", PolicyVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForRD_ExcesslimitEndorsements);
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</EXCESSLIMIT>");
                returnString.Append("</EXCESSLIMITS>");
                //****************************

                //get the underwriting questions
                returnString.Append("<UMGENINFOS>");
                returnString.Append("<UMGENINFO>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@POLICYID", PolicyID);
                objDataWrapper.AddParameter("@POLICYVERSIONID", PolicyVersionID);
                objDataWrapper.AddParameter("@DESC", strCalled);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForUM_GenInfoComponent);	// Gen Info		
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</UMGENINFO>");
                returnString.Append("</UMGENINFOS>");
                returnString.Append("</INPUTXML>");
                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        /// <summary>
        /// Gets Boat Ids  for policy rule implementation(Boat Operators)
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns>Boat Ids as carat separated string </returns>
        public static string GetRuleWCDriverIDs_Pol(int customerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetOperators_Pol";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            int intCount = ds.Tables[0].Rows.Count;
            string strDriverIds = "-1";
            for (int i = 0; i < intCount; i++)
            {
                if (i == 0)
                {
                    strDriverIds = ds.Tables[0].Rows[i][0].ToString();
                }
                else
                {
                    strDriverIds = strDriverIds + '^' + ds.Tables[0].Rows[i][0].ToString();
                }
            }
            return strDriverIds;
        }
        public static string GetPolTrailerDs_Pol(int customerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "PROC_GETPOLWATERCRAFT_TRAILERIDS";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strBoatID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strBoatID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strBoatID = strBoatID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strBoatID;
        }

        /// <summary>
        /// Gets a  Boat Ids against an policy
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns>Boat Ids  as a carat separated string . if no boat exist then it returns -1</returns>
        public static string GetBoatIDs_Pol(int customerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetRuleBoatIDs_Pol";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strBoatID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strBoatID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strBoatID = strBoatID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strBoatID;
        }


        # endregion

        public ClsRuleCollectioninfo FetchData(Int32 RULE_COLLECTION_ID)  //Added by Aditya for TFS Bug # 1003
        {

            DataSet ds = null;
            ClsRuleCollectioninfo objRuleCollectioninfo = new ClsRuleCollectioninfo();

            try
            {
                ds = objRuleCollectioninfo.FetchData(RULE_COLLECTION_ID);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objRuleCollectioninfo);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return objRuleCollectioninfo;


        }


        public int AddRuleCollectionInformation(ClsRuleCollectioninfo objRuleCollectioninfo) //Added by Aditya for TFS Bug # 1003
        {
            int returnValue = 0;

            if (objRuleCollectioninfo.RequiredTransactionLog)
            {
                objRuleCollectioninfo.TransactLabel =

                ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddRuleCollectionsDetail.aspx.resx");
                returnValue = objRuleCollectioninfo.AddRuleCollectionInformation();

            }
            return returnValue;
        }

        public int UpdateRuleCollectionInformation(ClsRuleCollectioninfo objRuleCollectioninfo)  //Added by Aditya for TFS Bug # 1003
        {
            int returnValue = 0;

            if (objRuleCollectioninfo.RequiredTransactionLog)
            {
                objRuleCollectioninfo.TransactLabel =

                ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddRuleCollectionsDetail.aspx.resx");

                returnValue = objRuleCollectioninfo.UpdateRuleCollectionInformation();

            }
            return returnValue;
        }

        public int DeleteRuleCollection(ClsRuleCollectioninfo objRuleCollectioninfo)  //Added by Aditya for TFS Bug # 1003
        {
            int returnValue = 0;

            if (objRuleCollectioninfo.RequiredTransactionLog)
            {
                objRuleCollectioninfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddRuleCollectionsDetail.aspx.resx");

                returnValue = objRuleCollectioninfo.DeleteRuleCollection();
            }
            return returnValue;
        }
        public int Insert_ReInsurance_Premium(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int PROCESS_ID, int CREATED_BY)
        {
            int retval = 0;
            string Insert_Proc = "Proc_InsertPOL_REINSURANCE_BREAKDOWN_DETAILS";
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            //objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
            objWrapper.AddParameter("@POLICY_ID", POLICY_ID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
            objWrapper.AddParameter("@CREATED_BY", CREATED_BY);
            objWrapper.AddParameter("@PROCESS_ID", PROCESS_ID);
            retval = objWrapper.ExecuteNonQuery(Insert_Proc);
            return retval;
        }

    }
}
