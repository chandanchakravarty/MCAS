using System;
using Cms.BusinessLayer;
using Cms.DataLayer;
using System.Xml;
using System.Data;
using System.Collections;

namespace Cms.BusinessLayer.BlQuote
{
    //Proc_GetPolQuoteDetails
    //Proc_GetQuoteDetails
    //Proc_InsertPremiumSplitDetails
    //Proc_InsertPremiumSplit
    public class PremiumDetails
    {
        private string strPremiumXML;
        private string strQuoteType;

        public string PremiumXML
        {
            get
            {
                return strPremiumXML;
            }
            set
            {
                strPremiumXML = value;
            }
        }
        public string QuoteType
        {
            get
            {
                return strQuoteType;
            }
            set
            {
                strQuoteType = value;
            }
        }

    }

    public class clsSplitPremium : Cms.BusinessLayer.BlCommon.ClsCommon
    {

        public const int POLICY_COMMIT_NEW_BUSINESS_PROCESS = 25;
        public const int POLICY_COMMIT_RENEWAL_PROCESS = 18;
        public const int POLICY_COMMIT_REWRITE_PROCESS = 32;
        public const int POLICY_COMMIT_CANCELLATION_PROCESS = 12;
        public const int POLICY_COMMIT_ENDORSEMENT_PROCESS = 14;

        public System.Collections.ArrayList SplitPremiumsApp(int CustomerId, int AppId, int AppVersionId, string ProcessType, DataWrapper objWrapper)
        {
            return SplitPremiums(CustomerId, AppId, AppVersionId, 0, 0, ProcessType, objWrapper);
        }

        public System.Collections.ArrayList SplitPremiumsPol(int CustomerId, int PolicyId, int PolicyVersionId, string ProcessType, DataWrapper objWrapper)
        {
            return SplitPremiums(CustomerId, 0, 0, PolicyId, PolicyVersionId, ProcessType, objWrapper);
        }
        public ArrayList GetPremiumXMLFromQuote(int CustomerId, int PolicyId, int PolicyVersionId, DataWrapper objWrapper)
        {
            return GetPremiumXMLFromQuote(CustomerId, PolicyId, PolicyVersionId, objWrapper, "");
        }
        public ArrayList GetPremiumXMLFromQuote(int CustomerId, int PolicyId, int PolicyVersionId, DataWrapper objWrapper, string strCALLEDFOR)
        {
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", (object)CustomerId.ToString(), SqlDbType.Int);
            objWrapper.AddParameter("@POLICY_ID", (object)PolicyId.ToString(), SqlDbType.Int);
            objWrapper.AddParameter("@POLICY_VERSION_ID", (object)PolicyVersionId.ToString(), SqlDbType.Int);
            objWrapper.AddParameter("@CALLEDFOR", (object)strCALLEDFOR.ToString(), SqlDbType.VarChar);
            DataSet DSTemp = objWrapper.ExecuteDataSet("Proc_GetPolQuoteDetails");
            System.Collections.ArrayList arrPremiumXml = new System.Collections.ArrayList();
            for (int i = 0; i < DSTemp.Tables[0].Rows.Count; i++)
            {
                string strPremiumXml = DSTemp.Tables[0].Rows[i]["QUOTE_XML"].ToString();
                string strQuoteType = DSTemp.Tables[0].Rows[i]["QUOTE_TYPE"].ToString();
                PremiumDetails objPremiumdetails = new PremiumDetails();

                objPremiumdetails.PremiumXML = strPremiumXml;
                objPremiumdetails.QuoteType = strQuoteType;
                arrPremiumXml.Add(objPremiumdetails);
            }
            objWrapper.ClearParameteres();
            return arrPremiumXml;
        }

        private System.Collections.ArrayList SplitPremiums(int CustomerId, int AppId, int AppVersionId, int PolicyId, int PolicyVersionId, string ProcessType, DataWrapper objWrapper)
        {
            try
            {
                bool retVal = false;
                System.Collections.ArrayList arrPremiumXml = new System.Collections.ArrayList();
                DataSet DSTemp = new DataSet();
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", (object)CustomerId.ToString(), SqlDbType.Int);
                if (AppId == 0 && AppVersionId == 0)
                {
                    objWrapper.AddParameter("@POLICY_ID", (object)PolicyId.ToString(), SqlDbType.Int);
                    objWrapper.AddParameter("@POLICY_VERSION_ID", (object)PolicyVersionId.ToString(), SqlDbType.Int);
                    DSTemp = objWrapper.ExecuteDataSet("Proc_GetPolQuoteDetails");
                }
                else
                {
                    objWrapper.AddParameter("@APP_ID", (object)AppId.ToString(), SqlDbType.Int);
                    objWrapper.AddParameter("@APP_VERSION_ID", (object)AppVersionId.ToString(), SqlDbType.Int);
                    DSTemp = objWrapper.ExecuteDataSet("Proc_GetQuoteDetails");
                }

                for (int i = 0; i < DSTemp.Tables[0].Rows.Count; i++)
                {
                    string strPremiumXml = DSTemp.Tables[0].Rows[i]["QUOTE_XML"].ToString();
                    string strQuoteType = DSTemp.Tables[0].Rows[i]["QUOTE_TYPE"].ToString();
                    PremiumDetails objPremiumdetails = new PremiumDetails();

                    objPremiumdetails.PremiumXML = strPremiumXml;
                    objPremiumdetails.QuoteType = strQuoteType;
                    arrPremiumXml.Add(objPremiumdetails);

                    retVal = SplitPremiums(CustomerId, AppId, AppVersionId, PolicyId, PolicyVersionId, strPremiumXml, ProcessType, objWrapper);
                }
                objWrapper.ClearParameteres();
                return arrPremiumXml;
            }
            catch (Exception objExc)
            {
                throw new Exception("Error while splitting premium at policy level", objExc);
            }
        }

        // Added by Mohit Agarwal 14-Nov-2007
        private string RemoveCompJunk(string comp_pre)
        {
            string comp_filter = comp_pre;
            int is_int = -1;
            for (int index = 0; index < comp_pre.Length; index++)
            {
                try
                {
                    is_int = int.Parse(comp_pre.Substring(index, 1));
                }
                catch (Exception)
                {
                    is_int = -1;
                }
                if (is_int != -1)
                    continue;
                else if (comp_pre.Substring(index, 1) != ".")
                    comp_filter = comp_filter.Replace(comp_pre.Substring(index, 1), "");
            }
            return comp_filter;
        }

        private bool SplitPremiums(int CustomerId, int AppId, int AppVersionId, int PolicyId, int PolicyVersionId, string PremiumXML, string ProcessType, DataWrapper objWrapper)
        {
            try
            {
                DataSet DsTemp = new DataSet();
                string SplitUniqeId = "0";

                PremiumXML = PremiumXML.Replace("<?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?><!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple' xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'> <!ATTLIST person id ID #IMPLIED>]>", "");
                XmlDocument objPremium = new XmlDocument();
                objPremium.LoadXml(PremiumXML);
                string strXPATH = @"PRIMIUM/RISK";
                foreach (XmlNode Node in objPremium.SelectNodes(strXPATH))
                {
                    objWrapper.ClearParameteres();
                    objWrapper.AddParameter("@CUSTOMER_ID", CustomerId, SqlDbType.Int);
                    objWrapper.AddParameter("@APP_ID", AppId, SqlDbType.Int);
                    objWrapper.AddParameter("@APP_VERSION_ID", AppVersionId, SqlDbType.Int);
                    objWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
                    objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId, SqlDbType.Int);
                    objWrapper.AddParameter("@RISK_ID", (object)GetAttributeValue(Node, "ID").ToString().Trim(), SqlDbType.Int);
                    objWrapper.AddParameter("@RISK_TYPE", (object)GetAttributeValue(Node, "TYPE").ToString().Trim(), SqlDbType.NVarChar);
                    objWrapper.AddParameter("@PROCESS_TYPE", ProcessType, SqlDbType.NVarChar);

                    DsTemp = objWrapper.ExecuteDataSet("Proc_InsertPremiumSplit");

                    SplitUniqeId = DsTemp.Tables[0].Rows[0]["UNIQUE_ID"].ToString();

                    foreach (XmlNode StepNode in Node.SelectNodes("STEP[@COMPONENT_TYPE!='' and @STEPPREMIUM!='' and @STEPPREMIUM!='0']"))
                    {
                        objWrapper.ClearParameteres();
                        objWrapper.AddParameter("@SPLIT_UNIQUE_ID", (object)SplitUniqeId.ToString(), SqlDbType.Int);
                        objWrapper.AddParameter("@COMPONENT_TYPE", (object)GetAttributeValue(StepNode, "COMPONENT_TYPE").ToString().Trim(), SqlDbType.NVarChar);
                        objWrapper.AddParameter("@COMPONENT_CODE", (object)GetAttributeValue(StepNode, "COMPONENT_CODE").ToString().Trim(), SqlDbType.NVarChar);
                        objWrapper.AddParameter("@LIMIT", (object)GetAttributeValue(StepNode, "LIMITVALUE").ToString().Trim(), SqlDbType.NVarChar);
                        objWrapper.AddParameter("@DEDUCTIBLE", (object)GetAttributeValue(StepNode, "DEDUCTIBLEVALUE").ToString().Trim(), SqlDbType.NVarChar);
                        if (GetAttributeValue(StepNode, "STEPPREMIUM").ToString().Trim() == "DESCRIPTIONNOTREQUIRED")
                        {
                            objWrapper.AddParameter("@PREMIUM", "", SqlDbType.NVarChar);
                        }
                        else
                        {
                            objWrapper.AddParameter("@PREMIUM", (object)GetAttributeValue(StepNode, "STEPPREMIUM").ToString().Trim(), SqlDbType.NVarChar);
                        }
                        objWrapper.AddParameter("@COMP_ACT_PREMIUM", (object)RemoveCompJunk(GetAttributeValue(StepNode, "COMP_ACT_PRE").ToString().Trim()), SqlDbType.NVarChar);
                        objWrapper.AddParameter("@COMP_REMARKS", (object)GetAttributeValue(StepNode, "COMP_REMARKS").ToString().Trim(), SqlDbType.NVarChar);
                        objWrapper.AddParameter("@COMP_EXT", (object)GetAttributeValue(StepNode, "COMP_EXT").ToString().Trim(), SqlDbType.NVarChar);
                        objWrapper.AddParameter("@COM_EXT_AD", (object)GetAttributeValue(StepNode, "COM_EXT_AD").ToString().Trim(), SqlDbType.NVarChar);
                        objWrapper.AddParameter("@REMARKS", (object)GetAttributeValue(StepNode, "COMPONENT_REMARKS").ToString().Trim(), SqlDbType.NVarChar);
                        objWrapper.AddParameter("@EXCEPTION_XML", (object)StepNode.OuterXml.ToString().Trim(), SqlDbType.NVarChar);
                        //added by Pravesh on dec 7 2007
                        if (ProcessType == POLICY_COMMIT_NEW_BUSINESS_PROCESS.ToString() || ProcessType == POLICY_COMMIT_RENEWAL_PROCESS.ToString() || ProcessType == POLICY_COMMIT_REWRITE_PROCESS.ToString())
                        {
                            objWrapper.AddParameter("@WRITTEN_PREM", (object)RemoveCompJunk(GetAttributeValue(StepNode, "COMP_ACT_PRE").ToString().Trim()), SqlDbType.NVarChar);
                            objWrapper.AddParameter("@CHANGE_INFORCE_PREM", (object)RemoveCompJunk(GetAttributeValue(StepNode, "COMP_ACT_PRE").ToString().Trim()), SqlDbType.NVarChar);
                        }
                        else
                        {
                            //objWrapper.AddParameter("@WRITTEN_PREM",null,SqlDbType.NVarChar);
                            //objWrapper.AddParameter("@CHANGE_INFORCE_PREM",null,SqlDbType.NVarChar);
                        }
                        // end here

                        objWrapper.ExecuteDataSet("Proc_InsertPremiumSplitDetails");
                    }
                }
            }
            catch (Exception objExc)
            {
                throw new Exception("Error while inserting data in Premium Split Tables", objExc);
            }

            return (true);
        }
        //Added by Pradeep Kushwaha on 16-July-2010
        /// <summary>
        /// Update the Old product covarages written premium 
        /// </summary>
        /// <param name="CustomerId">Customer Id</param>
        /// <param name="PolicyId">Policy Id</param>
        /// <param name="PolicyVersionId">Policy Version id</param>
        /// <param name="PremiumXML">Quote XML</param>
        /// <param name="objWrapper">Data Wrapper Object</param>
        /// <returns></returns>
        public bool UpdateProductsCoveragesPremium(int CustomerId, int PolicyId, int PolicyVersionId, string PremiumXML, DataWrapper objWrapper)
        {
            Boolean returnValue = false;
            try
            {
                if (PremiumXML == String.Empty)
                {
                    DataSet dsQuoteXml = new DataSet();
                    dsQuoteXml = this.FetchQuoteXML(CustomerId, 0, PolicyId, PolicyVersionId);
                    if (dsQuoteXml.Tables[0].Rows.Count > 0)
                    {
                        for (int CountQuoteXml = 0; CountQuoteXml < dsQuoteXml.Tables[0].Rows.Count; CountQuoteXml++)
                        {
                            string strPremiumXml = dsQuoteXml.Tables[0].Rows[CountQuoteXml]["QUOTE_XML"].ToString();
                            UpdateCoveragePremium(CustomerId, PolicyId, PolicyVersionId, strPremiumXml, objWrapper);
                        }
                        returnValue = true;
                    }
                    else
                        returnValue = false;
                }
                else
                {
                    UpdateCoveragePremium(CustomerId, PolicyId, PolicyVersionId, PremiumXML, objWrapper);
                    returnValue = true;
                }
            }
            catch (Exception objExc)
            {
                throw new Exception("Error while updating data in old product coverages tables", objExc);

            }

            return returnValue;
        }
        /// <summary>
        /// Use to update the old product coverages premium 
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="PolicyId"></param>
        /// <param name="PolicyVersionId"></param>
        /// <param name="PremiumXML"></param>
        /// <param name="objWrapper"></param>
        private void UpdateCoveragePremium(Int32 CustomerId, Int32 PolicyId, Int32 PolicyVersionId, String PremiumXML, DataWrapper objWrapper)
        {
            try
            {
                PremiumXML = PremiumXML.Replace("<?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?><!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple' xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'> <!ATTLIST person id ID #IMPLIED>]>", "");
                XmlDocument objPremium = new XmlDocument();
                objPremium.LoadXml(PremiumXML);
                string strXPATH = @"PRIMIUM/RISK";
                string COMPONENT_CODE = "";
                double WRITTEN_PREMIUM = 0;
                int flag = 0;
                foreach (XmlNode Node in objPremium.SelectNodes(strXPATH))
                {

                    foreach (XmlNode StepNode in Node.SelectNodes("STEP[@COMP_EXT!='' and @COMP_EXT!='0']"))
                    {
                        objWrapper.ClearParameteres();

                        if (RemoveCompJunk(GetAttributeValue(StepNode, "COMP_ACT_PRE")).ToString() != "" && GetAttributeValue(StepNode, "COMP_EXT").ToString().Trim() != "")
                        {
                            int Riskid = Convert.ToInt32(GetAttributeValue(Node, "ID").ToString().Trim());
                            WRITTEN_PREMIUM = Convert.ToDouble(RemoveCompJunk(GetAttributeValue(StepNode, "COMP_ACT_PRE")).ToString());
                            Int32 COVERAGE_CODE_ID = Convert.ToInt32(GetAttributeValue(StepNode, "COMP_EXT").ToString().Trim());
                            COMPONENT_CODE = GetAttributeValue(StepNode, "COMPONENT_CODE").ToString().Trim();

                            if (COMPONENT_CODE == "MCCAFEE")
                            {
                                if (flag == 0)
                                {
                                    WRITTEN_PREMIUM = 0;
                                    foreach (XmlNode COMPONENT_CODE_Node in Node.SelectNodes("STEP[@COMPONENT_CODE='" + COMPONENT_CODE + "']"))
                                    {
                                        WRITTEN_PREMIUM = WRITTEN_PREMIUM + Convert.ToDouble(RemoveCompJunk(GetAttributeValue(COMPONENT_CODE_Node, "COMP_ACT_PRE")).ToString());
                                        flag = 1;
                                    }
                                }
                                else { continue; }
                            }

                            objWrapper.ClearParameteres();
                            objWrapper.AddParameter("@CUSTOMER_ID", CustomerId, SqlDbType.Int);
                            objWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
                            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId, SqlDbType.Int);
                            objWrapper.AddParameter("@RISK_ID", Riskid, SqlDbType.Int);
                            objWrapper.AddParameter("@COVERAGE_CODE_ID", COVERAGE_CODE_ID, SqlDbType.Int);
                            objWrapper.AddParameter("@WRITTEN_PREMIUM", WRITTEN_PREMIUM, SqlDbType.Decimal);
                            objWrapper.ExecuteDataSet("Proc_UpdateProductsCoveragesPremium");
                            WRITTEN_PREMIUM = 0;
                            flag = 0;
                        }

                    }

                }
            }
            catch (Exception objExc)
            {
                throw new Exception("Error while updating data in old product coverages tables", objExc);

            }
        }

        //Added By Pradeep Kushwaha on 19-July-2010
        /// <summary>
        /// Accepts customerID ,policyID,polVersionID and quoteID (optional) and returns the policy quote html
        /// </summary>
        /// <param name="customerID">Customer ID</param>
        /// <param name="quoteID">quoteId of the Quote</param>
        /// <returns>Dataset</returns>

        public DataSet FetchQuoteXML(int customerId, int quoteId, int policyID, int polVersionID)
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
        private string GetAttributeValue(XmlNode Node, string AttributeName)
        {
            for (int AttributeCounter = 0; AttributeCounter < Node.Attributes.Count; AttributeCounter++)
            {
                XmlAttribute XmlAttrib = Node.Attributes[AttributeCounter];
                if (XmlAttrib.Name.ToUpper() == AttributeName.ToUpper())
                {
                    return XmlAttrib.Value;
                }
            }
            return "";
        }
        public int UpdateWrittenEnforcePremiumSplit(int CustomerId, int PolicyId, int PolicyVersionId, int PolicyBaseVersionId, string ProcessType, DataWrapper objWrapper)
        {
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", CustomerId, SqlDbType.Int);
            objWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId, SqlDbType.Int);
            objWrapper.AddParameter("@POLICY_BASE_VERSION_ID", PolicyBaseVersionId, SqlDbType.Int);
            objWrapper.AddParameter("@PROCESS_TYPE", ProcessType, SqlDbType.NVarChar);

            int RetVal = objWrapper.ExecuteNonQuery("Proc_UpdateWrittenEnforcePremiumSplit");
            return RetVal;


        }
    }

}
