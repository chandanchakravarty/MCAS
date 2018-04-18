using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
using System.IO;
using System.Web.Configuration;
//using EbixPDF;


namespace Cms.BusinessLayer.BlProcess
{
    public class ClsProductPdfXml : Cms.BusinessLayer.BlCommon.ClsCommon 
    {
        private DataWrapper objWrapper;
        private XmlDocument PDFXMLDoc;
        private const string PAYER_INSURED = "14542";
        private const string COINSURANCE_FILLOWER = "14549";
        private const string COINSURANCE_DIRECT = "14547";
        String _Saveinfinal = "";
       
        public ClsProductPdfXml(DataWrapper objDataWrapper)
        {
            this.objWrapper = objDataWrapper;
        }
        public ClsProductPdfXml()
        {
            this.objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
        }
        #region fetch pdf xml Information for the policy
        private DataSet fetchPolicyDataforPdfXml(int CustomerID, int PolicyId, int PolVersionId)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);
                objWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);
                DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetProductPDFPolicyDetails");
                objWrapper.ClearParameteres();
                return dsTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /*
        public String BoletoORpremNoticeGenerateInitialLoad()
        {
            String ChkBoletoORPREM_Notice = "";
           
            objWrapper.ClearParameteres();
            DataSet SystemParams = objWrapper.ExecuteDataSet("Proc_GetSystemParams");
            objWrapper.ClearParameteres();
             
            if (SystemParams.Tables.Count > 0)
            {
                ChkBoletoORPREM_Notice = SystemParams.Tables[0].Rows[0]["NOTIFY_RECVE_INSURED"].ToString();

            }
            if (ChkBoletoORPREM_Notice == "14667")
            {
                return "BOLETO";
            }
            else if (ChkBoletoORPREM_Notice == "14668")
            {
                return "PREM_NOTICE";
            }
            return "";
        }*/
        /*
        private DataSet fetchPolicyDataforPdfXmlforInitialLoad(int CustomerID, int PolicyId, int PolVersionId)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);
                objWrapper.AddParameter("@LANG_ID",2);
                DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetProductPDFPolicyDetails");
                objWrapper.ClearParameteres();
                return dsTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }*/
        #endregion



        #region cededletter
        // Start sneha
        public DataSet fetch_CededClaimLetter(int ClaimID, int ActivityID, string PROCESS_TYPE)
        {
            try
            {

                objWrapper.ClearParameteres();
                objWrapper.TransactionRequired = DataWrapper.MaintainTransaction.NO;
                objWrapper.AddParameter("@CLAIM_ID", ClaimID);
                objWrapper.AddParameter("@ACTIVITY_ID", ActivityID);
                objWrapper.AddParameter("@PROCESS_TYPE", PROCESS_TYPE);
                objWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);
                DataSet ds = objWrapper.ExecuteDataSet("Proc_Ceded_COIFNOL_PAYMENTS");
                objWrapper.ClearParameteres();
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //For Letter 19
        private string CededCOILetterHTML(int ClaimID, int ActivityID, string PROCESS_TYPE)
        {

            try
            {
                DataSet ds = fetch_CededClaimLetter(ClaimID, ActivityID, PROCESS_TYPE);
                DataTable dt = new DataTable();
                if (ds == null || ds.Tables.Count < 0 || ds.Tables[0].Rows.Count < 1)
                {
                    return "";
                }

                StringBuilder HTML = new StringBuilder();
                 HTML.Append(ds.Tables[1].Rows[0]["LETTER"]);
                string str = HTML.ToString();
                return str;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        // For Letter 19
        public void generateCededCOILetter(int ClaimID, int ActivityID, int CustomerID, int PolicyID, int PolicyVersionID, int userID, string PROCESS_TYPE)
        {
            try
            {
                int RetValue = 0;
                DataSet ds = fetch_CededClaimLetter(ClaimID, ActivityID, PROCESS_TYPE);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string AgencyCode = "", CarrierCode = "";//, fileName = "";
                    int ProcessId = 0, ProcessRowId = 0;
                    Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo;
                    DataSet dsBRCO = getBrokerCoInsurer(CustomerID, PolicyID, PolicyVersionID);
                    CarrierCode = dsBRCO.Tables[3].Rows[0]["CARRIER_CODE"].ToString();
                    AgencyCode = dsBRCO.Tables[3].Rows[0]["AGENCY_CODE"].ToString();
                    objPrintJobsInfo = GetPrintJobsValues(CustomerID, PolicyID, PolicyVersionID, ProcessId, ProcessRowId, userID, "CLAIM", AgencyCode, CarrierCode);
                    objPrintJobsInfo.DOCUMENT_CODE = PROCESS_TYPE;
                    objPrintJobsInfo.CLAIM_ID = ClaimID;
                    objPrintJobsInfo.ACTIVITY_ID = ActivityID;
                    //-----------------------------------------------------------------------

                    DataTable dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string strDoc_Text = CededCOILetterHTML(ClaimID, ActivityID, PROCESS_TYPE);

                        if (strDoc_Text != "")
                            RetValue = this.SaveCeededCOI(ClaimID, ActivityID, strDoc_Text, PROCESS_TYPE);
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }    
     
        private int SaveCeededCOI(int ClaimId, int ActivityId, String strPolicyDocTEXT, string ProcessType)
        {
            int retVal = 0;
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.TransactionRequired = DataWrapper.MaintainTransaction.NO;
                objWrapper.AddParameter("@CLAIM_ID", ClaimId);
                objWrapper.AddParameter("@ACTIVITY_ID", ActivityId);
                objWrapper.AddParameter("@PAYEE_ID", 0);
                objWrapper.AddParameter("@DOC_TEXT", strPolicyDocTEXT);
                objWrapper.AddParameter("@PROCESS_TYPE", ProcessType);
                retVal = objWrapper.ExecuteNonQuery("Proc_SaveClaimDocument");
                objWrapper.ClearParameteres();

            }

            catch (Exception ex)
            {
                throw (ex);
            }
            return retVal;
        }

        #endregion

        //End Sneha


        #region generate PDF XML and Policy Documents
        public string generateDocuments(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {
            string strReturnString = "";
            strReturnString = generateDocuments(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, "PROCESS", objProcessInfo.COMPLETED_BY, objProcessInfo.PROCESS_ID, objProcessInfo.ROW_ID);
            return strReturnString;
        }
        public string generateDocuments(int CustomerID, int PolicyID, int PolicyVersionID, string CalledFor, int userID)
        {
            return generateDocuments(CustomerID, PolicyID, PolicyVersionID, CalledFor, userID, 0, 0);
        }
        //public string generateDocuments_Motor(int CustomerID, int PolicyID, int PolicyVersionID)
        //{
        //    return generateDocuments_Motor(CustomerID, PolicyID, PolicyVersionID);
        //}

        /*
        //Added by Pradeep kushwaha on 16-Nov-2011
        /// <summary>
        /// To generate initial load commited policy details data
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="userID"></param>
        /// <param name="ProcessId"></param>
        /// <param name="ProcessRowId"></param>
        /// <param name="CalledFrom"></param>
        /// <returns></returns>
        public string generateDocuments(int CustomerID, int PolicyID, int PolicyVersionID, int userID, int ProcessId, int ProcessRowId, string CalledFrom)
        {
            return generateDocuments(CustomerID, PolicyID, PolicyVersionID, "PROCESS", userID, ProcessId, ProcessRowId, CalledFrom);
        }*/
        public string generateDocuments(int CustomerID, int PolicyID, int PolicyVersionID, string CalledFor, int userID, int ProcessId, int ProcessRowId)
        {
            // fetching Policy details
            try
            {
                string Co_Insurance = "", Prem_NoticeFileName = "", AgencyCode = "", CarrierCode = "";
                DataSet dsPolicy=new DataSet();
                
                /*if(CalledFrom=="INITIAL_LOAD")
                    dsPolicy = fetchPolicyDataforPdfXmlforInitialLoad(CustomerID, PolicyID, PolicyVersionID);
                else*/
                    dsPolicy = fetchPolicyDataforPdfXml(CustomerID, PolicyID, PolicyVersionID);
                
                //generating XML from Policy Details for PDF

                String strPolicyPdfXml = GenerateDocumentdataXML(dsPolicy);//dsPolicy.GetXml();
                if (strPolicyPdfXml != "" && strPolicyPdfXml != "<POLICY_DOCUMENTS></POLICY_DOCUMENTS>")
                {
                    Co_Insurance = ClsCommon.FetchValueFromXML("CO_INSURANCE", strPolicyPdfXml);
                    AgencyCode = ClsCommon.FetchValueFromXML("AGENCY_CODE", strPolicyPdfXml);
                    CarrierCode = ClsCommon.FetchValueFromXML("REIN_COMAPANY_CODE", strPolicyPdfXml);
                }
                if (Co_Insurance != COINSURANCE_FILLOWER)
                    if (CalledFor == "PROCESS" && (ProcessId != 12 && ProcessId !=2))
                    {
                        // generate Boleto if payer is Insured - 14542 lookup id for Insured
                        PDFXMLDoc = new XmlDocument();
                        PDFXMLDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + strPolicyPdfXml);
                        XmlNode Node = PDFXMLDoc.SelectSingleNode("POLICY_DOCUMENTS/APPLICATION");
                        string Payer = getNodeValue(Node, "PAYOR");
                        if (Payer == PAYER_INSURED)
                        {
                            String GenerateBoleto_OR_Prem_Notice=string.Empty;
                            /*if (CalledFrom == "INITIAL_LOAD")
                                GenerateBoleto_OR_Prem_Notice = BoletoORpremNoticeGenerateInitialLoad();
                            else*/
                                //GenerateBoleto_OR_Prem_Notice = BoletoORpremNoticeGenerate();
                            
                            //if (GenerateBoleto_OR_Prem_Notice == "BOLETO")
                            //{
                            //    Prem_NoticeFileName = generatePolicyBoletos(CustomerID, PolicyID, PolicyVersionID, userID, "PROCESS", ProcessId, ProcessRowId, AgencyCode, CarrierCode);
                            //}
                            //else if (GenerateBoleto_OR_Prem_Notice == "PREM_NOTICE")
                            //{
                            //    Prem_NoticeFileName = GeneratePremiumNoticeXMLAndSave(CustomerID, PolicyID, PolicyVersionID, "PROCESS", userID);
                            //}

                        }

                        PDFXMLDoc = null;
                    }
                string fileName = "";
                //save PDF XML In DB
                if (strPolicyPdfXml != "" && strPolicyPdfXml != "<POLICY_DOCUMENTS></POLICY_DOCUMENTS>")
                {
                    this.SavePolicyDocumentXml(CustomerID, PolicyID, PolicyVersionID, strPolicyPdfXml, CalledFor);
                    /*
                    if (CalledFrom == "INITIAL_LOAD")
                        fileName = AddPrintJobEntriesforInitialLoad(CustomerID, PolicyID, PolicyVersionID, "POLICY_DOC", userID, ProcessId, ProcessRowId);
                    else*/

                        fileName = AddPrintJobEntries(CustomerID, PolicyID, PolicyVersionID, "POLICY_DOC", userID, ProcessId, ProcessRowId);
                    /* Code commented as Common Function to add Print job entries called
                    Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo;
                    objPrintJobsInfo = GetPrintJobsValues(CustomerID, PolicyID, PolicyVersionID, ProcessId, ProcessRowId, userID, "", AgencyCode, CarrierCode);
                    // adding entries in PRint jobs table for Each Co-Applicant to Generate and Print Policy Docs
                    DataSet dsBRCO = getBrokerCoInsurer(CustomerID, PolicyID, PolicyVersionID);
                    for (int i = 0; i < dsBRCO.Tables[0].Rows.Count; i++)
                    {
                        fileName = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1778", "") + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString();
                        objPrintJobsInfo.DOCUMENT_CODE = "POLICY_DOC";
                        objPrintJobsInfo.ENTITY_TYPE = "CUSTOMER";// +dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString();
                        if (dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString()!="")
                            objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString());
                        objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                        AddPrintJobs(objPrintJobsInfo);
                        string strPolicyDetl = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1449");
                        string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                        //this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, ClsCommon.FetchGeneralMessage("1166", "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);
                        this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, strPolicyDetl + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//Policy Document for Co-Applicant generated successfully
                    }

                    // adding entries in PRint jobs table for Each Broker to Generate and Print Policy Docs
                    for (int i = 0; i < dsBRCO.Tables[1].Rows.Count; i++)
                    {
                        fileName = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1777", "") + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[1].Rows[i]["BROKER_ID"].ToString();
                        objPrintJobsInfo.DOCUMENT_CODE = "POLICY_DOC";
                        objPrintJobsInfo.ENTITY_TYPE = "BROKER";
                        if (dsBRCO.Tables[1].Rows[i]["BROKER_ID"].ToString() != "")
                            objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[1].Rows[i]["BROKER_ID"].ToString());
                        objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                        AddPrintJobs(objPrintJobsInfo);
                        string strPolicyDoc = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1450");
                        string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                        this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, strPolicyDoc + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//Policy Document for Broker generated successfully

                    }*/
                    // adding entries in PRint jobs table for leader to Generate and Print Policy Docs
                    /*fileName = "LEADER" + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString();
                    objPrintJobsInfo.DOCUMENT_CODE = "POLICY_DOC";
                    objPrintJobsInfo.ENTITY_TYPE = "LEADER";
                    objPrintJobsInfo.ENTITY_ID = 0;
                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                    AddPrintJobs(objPrintJobsInfo);
                    */
                    // adding entries in PRint jobs table for follower to Generate and Print Policy Docs
                    /* * LEADER/FOLLOWER copy was not required, please delete it; itrack 964 note# 20
                    if (Co_Insurance != COINSURANCE_DIRECT) // if policy is other then direct
                    {
                        for (int i = 0; i < dsBRCO.Tables[2].Rows.Count; i++)
                        {
                            string leaderFolower = dsBRCO.Tables[2].Rows[i]["LEADER_FOLLOWER"].ToString();
                            leaderFolower = leaderFolower == "14548" ? Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1779", "") : Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1780", "");
                            fileName = leaderFolower + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[2].Rows[i]["COMPANY_ID"].ToString();
                            objPrintJobsInfo.DOCUMENT_CODE = "POLICY_DOC";
                            objPrintJobsInfo.ENTITY_TYPE = leaderFolower;
                            if (dsBRCO.Tables[2].Rows[i]["COMPANY_ID"].ToString() != "")
                                objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[2].Rows[i]["COMPANY_ID"].ToString());
                            objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                            AddPrintJobs(objPrintJobsInfo);
                            string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                            this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1625", "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Policy Document for Carrier/follower generated successfully"
                        }
                    }
                    */
                    /*
                    // adding entries in PRint jobs table for Carrier Print Policy Docs iTrack 964 Note # 20
                    for (int i = 0; i < dsBRCO.Tables[3].Rows.Count; i++)
                    {
                        string leaderFolower = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1958", "");
                        fileName = leaderFolower + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[3].Rows[i]["COMPANY_ID"].ToString();
                        objPrintJobsInfo.DOCUMENT_CODE = "POLICY_DOC";
                        objPrintJobsInfo.ENTITY_TYPE = leaderFolower;
                        if (dsBRCO.Tables[3].Rows[i]["COMPANY_ID"].ToString() != "")
                            objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[3].Rows[i]["COMPANY_ID"].ToString());
                        objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                        AddPrintJobs(objPrintJobsInfo);
                        string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                        this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1955", "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Policy Document for Carrier generated successfully."
                    }
                    // adding entries in PRint jobs table for Reinsurer Print Policy Docs iTrack 964 Note # 20
                    for (int i = 0; i < dsBRCO.Tables[4].Rows.Count; i++)
                    {
                        string leaderFolower = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1951", "");
                        fileName = leaderFolower + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[4].Rows[i]["COMPANY_ID"].ToString();
                        objPrintJobsInfo.DOCUMENT_CODE = "POLICY_DOC";
                        objPrintJobsInfo.ENTITY_TYPE = leaderFolower;
                        if (dsBRCO.Tables[4].Rows[i]["COMPANY_ID"].ToString() != "")
                            objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[4].Rows[i]["COMPANY_ID"].ToString());
                        objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                        AddPrintJobs(objPrintJobsInfo);
                        string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                        this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1954", "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Policy Document for Reinsurer generated successfully."
                    }
                    */
                }

                return fileName;
            }
            catch (Exception ex)
            {
                System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while generating Policy Documents XML and adding in Print job.");
                addInfo.Add("CustomerID", CustomerID.ToString());
                addInfo.Add("PolicyID", PolicyID.ToString());
                addInfo.Add("PolicyVersionID", PolicyVersionID.ToString());
                addInfo.Add("ProcessRowID", ProcessId.ToString());
                addInfo.Add("DOCUMENT_CODE", "PolicyDoc");
                ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex, addInfo);
                throw (ex);
            }
        }

        //Added By avijit for tfs 3545 & tfs 3573 for singapore PDF document Generation
        // Dated on 06/02/2012
        public string generateDocuments_Motor(int CustomerID, int PolicyID, int PolicyVersionID, String DOC_CODE, int CLAIM_ID, int ACTIVITY_ID, int PRINT_JOB_ID)
        {
            int LOB_ID = GetPolicyLOBID(CustomerID, PolicyID, PolicyVersionID);
            string strReturnXML = "", PDFName = "";
            string strReturnXMLClauses = "";
            String _Document_Name = "";
            String _Document_Path = "";
            int returnResult = 0;
            int IS_EOD;

            if (IsEODProcess)
                IS_EOD = 1;
            else
                IS_EOD = 0;

            DataWrapper objDatawrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            DataSet DSDocmentDetail = new DataSet();
            DataSet dsPNotice = null;

            string strStoredProc = "Proc_Fetch_DocCodePolicy";
            objDatawrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            objDatawrapper.ClearParameteres();
            objDatawrapper.AddParameter("@CustomerID", CustomerID);
            objDatawrapper.AddParameter("@PolicyID", PolicyID);
            objDatawrapper.AddParameter("@PolicyVersionID", PolicyVersionID);
            objDatawrapper.AddParameter("@DocumentCode", DOC_CODE);
            objDatawrapper.AddParameter("@ClaimID", CLAIM_ID);
            objDatawrapper.AddParameter("@ActivityID", ACTIVITY_ID);
            objDatawrapper.AddParameter("@Print_Job_ID", PRINT_JOB_ID);
            DSDocmentDetail = objDatawrapper.ExecuteDataSet(strStoredProc);
            objDatawrapper.ClearParameteres();

            if (DSDocmentDetail != null && DSDocmentDetail.Tables.Count > 0 && DSDocmentDetail.Tables[0].Rows.Count > 0)
            {

                String _DocumentType = DSDocmentDetail.Tables[0].Rows[0]["DOCUMENT_CODE"].ToString();
                Int32 _CUSTOMER_Id = Convert.ToInt32(DSDocmentDetail.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
                Int32 _Policy_Id = Convert.ToInt32(DSDocmentDetail.Tables[0].Rows[0]["POLICY_ID"].ToString());
                Int32 _Policy_Version = Convert.ToInt32(DSDocmentDetail.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
                Int32 _Print_Job_Id = Convert.ToInt32(DSDocmentDetail.Tables[0].Rows[0]["PRINT_JOBS_ID"].ToString());
                _Document_Name = DSDocmentDetail.Tables[0].Rows[0]["FILE_NAME"].ToString();
                _Document_Path = DSDocmentDetail.Tables[0].Rows[0]["URL_PATH"].ToString();
                String _ENTITY_TYPE = DSDocmentDetail.Tables[0].Rows[0]["ENTITY_TYPE"].ToString();
                Int32 _Claim_Id = Convert.ToInt32(DSDocmentDetail.Tables[0].Rows[0]["ClAIM_ID"].ToString());
                Int32 _Activity_Id = Convert.ToInt32(DSDocmentDetail.Tables[0].Rows[0]["ACTIVITY_ID"].ToString());
                Int32 _Entity_Id = Convert.ToInt32(DSDocmentDetail.Tables[0].Rows[0]["ENTITY_ID"].ToString());
            }
            
            objDatawrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
            objDatawrapper.AddParameter("@POLICY_ID", PolicyID, SqlDbType.Int);
            objDatawrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID, SqlDbType.Int);
            objDatawrapper.AddParameter("@CALLEDFROM", "", SqlDbType.VarChar);
            objDatawrapper.AddParameter("@LANG_ID", 3, SqlDbType.Int);
            System.Data.SqlClient.SqlParameter objSqlParameter = (System.Data.SqlClient.SqlParameter)objDatawrapper.AddParameter("@OUTPUTPROCESSID", SqlDbType.Int, ParameterDirection.Output);

            if (LOB_ID == 38) //For MOTOR LOB
            {
            dsPNotice = objDatawrapper.ExecuteDataSet("Proc_GetPolicyNewBusinessForMotor");
            objDatawrapper.ClearParameteres();

            if (objSqlParameter.Value != System.DBNull.Value)
            {
                returnResult = Convert.ToInt32(objSqlParameter.Value);
            }

            StringBuilder returnString = new StringBuilder();          
            
            returnString.Append("<INPUTXML>");
            returnString.Append("<INPUTXMLNODE>");
            strReturnXML = dsPNotice.Tables[0].Rows[0].ItemArray[0].ToString();
            //strReturnXML = strReturnXML.Replace("<NewDataSet>", "").Replace("</NewDataSet>", "").Replace("<Table>", "").Replace("</Table>", "").Replace("\r\n", "");
            returnString.Append(strReturnXML);
            if (dsPNotice.Tables[1] != null && dsPNotice.Tables[1].Rows.Count > 0)
            {
                strReturnXMLClauses = dsPNotice.Tables[1].Rows[0].ItemArray[0].ToString();
                returnString.Append(strReturnXMLClauses);
            }
            returnString.Append("</INPUTXMLNODE>"); 
            returnString.Append("</INPUTXML>");            
            //BlGeneratePdf.XmlToPDFParser objControlParse = new BlGeneratePdf.XmlToPDFParser();
            EAWXmlToPDFParser.XmlToPDFParser objControlParse = new EAWXmlToPDFParser.XmlToPDFParser();
            objControlParse.IDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain").ToString();
            objControlParse.IUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName").ToString();
            objControlParse.IPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd").ToString();
            if (IsEODProcess)
            {
                objControlParse.PdfTemplatePath = ClsCommon.GetKeyValueWithIP("PremiumNotice_Template_Path");
                objControlParse.PdfMapXml = ClsCommon.GetKeyValueWithIP("PDFCONTROLNAMEMAPPING_XML__Path");
                objControlParse.PdfOutPutPath = System.IO.Path.GetFullPath(_Document_Path);
            }
            else
            {

                objControlParse.PdfTemplatePath = ClsCommon.GetKeyValueWithIP("PDF_Template_Path");
                //objControlParse.PdfMapXml = ClsCommon.GetKeyValueWithIP("PDFCONTROLNAMEMAPPING_XML__Path");
                XmlDocument doc = new XmlDocument();
                if (returnResult == 24 || returnResult == 25)
                {
                    doc.Load(ClsCommon.GetKeyValueWithIP("NEWBUSINESSMAPPINGXML"));
                }
                else if (returnResult == 3 || returnResult == 14 || returnResult == 15)
                {
                    doc.Load(ClsCommon.GetKeyValueWithIP("ENDORSEMENTMAPPINGXML"));
                }
                else if (returnResult == 2 || returnResult == 12 || returnResult == 13)
                {
                    doc.Load(ClsCommon.GetKeyValueWithIP("CANCELLATIONMAPPINGXML"));
                }
                else if (returnResult == 5 || returnResult == 6 || returnResult == 18 || returnResult == 19 || returnResult == 20 || returnResult == 21)
                {
                    doc.Load(ClsCommon.GetKeyValueWithIP("RENEWALMAPPINGXML"));
                }


           
                objControlParse.PdfMapXml = doc.InnerXml;
                //objControlParse.PdfOutPutPath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\OUTPUTPDFs\\";
                objControlParse.PdfOutPutPath = System.Web.HttpContext.Current.Server.MapPath(_Document_Path + "//");

            }
            objControlParse.InputXml = returnString.ToString();
            objControlParse.PdfOutPutFileName = _Document_Name;
            PDFName = objControlParse.GeneratePdf();
            }
            else if (LOB_ID == 1) // For FIRE LOB
            {
                dsPNotice = objDatawrapper.ExecuteDataSet("PROC_GETPOLICYNEWBUSINESSFORFIRE");
                objDatawrapper.ClearParameteres();

                if (objSqlParameter.Value != System.DBNull.Value)
                {
                    returnResult = Convert.ToInt32(objSqlParameter.Value);
                }

                StringBuilder returnString = new StringBuilder();

                returnString.Append("<INPUTXML>");
                returnString.Append("<INPUTXMLNODE>");
                strReturnXML = dsPNotice.Tables[0].Rows[0].ItemArray[0].ToString();
                //strReturnXML = strReturnXML.Replace("<NewDataSet>", "").Replace("</NewDataSet>", "").Replace("<Table>", "").Replace("</Table>", "").Replace("\r\n", "");
                returnString.Append(strReturnXML);
                if (dsPNotice.Tables[1] != null && dsPNotice.Tables[1].Rows.Count > 0)
                {
                    strReturnXMLClauses = dsPNotice.Tables[1].Rows[0].ItemArray[0].ToString();
                    returnString.Append(strReturnXMLClauses);
                }
                returnString.Append("</INPUTXMLNODE>");
                returnString.Append("</INPUTXML>");
                //BlGeneratePdf.XmlToPDFParser objControlParse = new BlGeneratePdf.XmlToPDFParser();
                EAWXmlToPDFParser.XmlToPDFParser objControlParse = new EAWXmlToPDFParser.XmlToPDFParser();
                objControlParse.IDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain").ToString();
                objControlParse.IUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName").ToString();
                objControlParse.IPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd").ToString();
                if (IsEODProcess)
                {
                    objControlParse.PdfTemplatePath = ClsCommon.GetKeyValueWithIP("PremiumNotice_Template_Path");
                    objControlParse.PdfMapXml = ClsCommon.GetKeyValueWithIP("PDFCONTROLNAMEMAPPING_XML__Path");
                    objControlParse.PdfOutPutPath = System.IO.Path.GetFullPath(_Document_Path);
                }
                else
                {

                    objControlParse.PdfTemplatePath = ClsCommon.GetKeyValueWithIP("PDF_Template_Path");
                    //objControlParse.PdfMapXml = ClsCommon.GetKeyValueWithIP("PDFCONTROLNAMEMAPPING_XML__Path");
                    XmlDocument doc = new XmlDocument();
                    if (returnResult == 24 || returnResult == 25)
                    {
                        doc.Load(ClsCommon.GetKeyValueWithIP("PNBControlMap_Fire"));
                    }
                    else if (returnResult == 3 || returnResult == 14 || returnResult == 15)
                    {
                        doc.Load(ClsCommon.GetKeyValueWithIP("ENDORSEMENTMAPPINGXML"));
                    }
                    else if (returnResult == 2 || returnResult == 12 || returnResult == 13)
                    {
                        doc.Load(ClsCommon.GetKeyValueWithIP("CANCELLATIONMAPPINGXML"));
                    }
                    else if (returnResult == 5 || returnResult == 6 || returnResult == 18 || returnResult == 19 || returnResult == 20 || returnResult == 21)
                    {
                        doc.Load(ClsCommon.GetKeyValueWithIP("RENEWALMAPPINGXML_Fire"));
                    }

                    objControlParse.PdfMapXml = doc.InnerXml;
                    //objControlParse.PdfOutPutPath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\OUTPUTPDFs\\";
                    objControlParse.PdfOutPutPath = System.Web.HttpContext.Current.Server.MapPath(_Document_Path + "//");

                }
                objControlParse.InputXml = returnString.ToString();
                objControlParse.PdfOutPutFileName = _Document_Name;
                PDFName = objControlParse.GeneratePdf();

            }

            return PDFName; 
        }
        /*
        //Added by Pradeep Kushwaha 16-Nov-2011
        /// <summary>
        /// Get the initial load commit policy details data which need to be generate policy document and boleto
        /// </summary>
        public DataSet GetPolicyDocAndBoletoOfInitialLoadData()
        {
            try
            {
                objWrapper.ClearParameteres();
                DataSet dsInitalLoadcommitData = objWrapper.ExecuteDataSet("PROC_GET_MIG_IL_COMMIT_POLICY_DETAILS");
                objWrapper.ClearParameteres();
                return dsInitalLoadcommitData;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //Added by Pradeep Kushwaha 16-Nov-2011
        /// <summary>
        /// Generate initial load commit policy details data which need to be generate policy document and boleto
        /// </summary>
        /// <param name="processflag"></param>
        public void GeneratePolicyDocAndBoletoOfInitialLoadData(ref Boolean processflag){

            processflag = true; 
            DataSet InstallDetailsDs = GetPolicyDocAndBoletoOfInitialLoadData();
            foreach (DataRow dr in InstallDetailsDs.Tables[0].Rows)
            {
                this.generateDocuments(int.Parse(dr["CUSTOMER_ID"].ToString()), int.Parse(dr["POLICY_ID"].ToString()), int.Parse(dr["POLICY_VERSION_ID"].ToString()),
                    int.Parse(dr["CREATED_BY"].ToString()), int.Parse(dr["PROCESS_ID"].ToString()), int.Parse(dr["POLICY_VERSION_ID"].ToString()), "INITIAL_LOAD");
                
            }//foreach (DataRow dr in InstallDetailsDs.Tables[0].Rows)
            processflag = false;
        }
            */
        public string generatePolicyBoletos(int CustomerID, int PolicyID, int PolicyVersionID, int userID, string CalledFor, int ProcessId, int ProcessRowId, string AgencyCode, string CarrierCode)
        {
            //generate Boleto
            string fileName = "";
            BlBoleto.ClsBoleto objBoleto = new BlBoleto.ClsBoleto(this.objWrapper);
            /*
            //Added by pradeep Kushwaha for initial load to generate boleto
            ArrayList Files = new ArrayList();
            if (CalledFrom.ToString().ToUpper() == "INITIAL_LOAD")
            {
                Files = objBoleto.GenerateBoletoOfInitialLoadData(CustomerID, PolicyID, PolicyVersionID, userID);
                this.UpdateInitialLoadDocumentGeneratedData(CustomerID, PolicyID, PolicyVersionID);
            }
            else
             *   //Added till here 
             * */
            ArrayList   Files = objBoleto.GenerateBoletoFromCommitProcess(CustomerID, PolicyID, PolicyVersionID, userID);
          
            Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo;
            for (int i = 0; i < Files.Count; i++)
            {
                fileName = Files[i].ToString();
                objPrintJobsInfo = GetPrintJobsValues(CustomerID, PolicyID, PolicyVersionID, ProcessId, ProcessRowId, userID, "BOLETO", AgencyCode, CarrierCode);
                objPrintJobsInfo.DOCUMENT_CODE = "BOLETO";
                objPrintJobsInfo.ENTITY_TYPE = "CUSTOMER";
                string CoAppId = fileName.Substring(fileName.LastIndexOf('_') + 1);
                objPrintJobsInfo.ENTITY_ID = int.Parse(CoAppId == "" ? "0" : CoAppId);
                objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                AddPrintJobs(objPrintJobsInfo);

                string str = "/cms/Policies/Aspx/PolicyBoleto.aspx?CUSTOMER_ID=" + CustomerID.ToString()
                            + "&POLICY_ID=" + PolicyID.ToString()
                            + "&POLICY_VERSION_ID=" + PolicyVersionID.ToString()
                            + "&INSTALLMENT_NO=0&CO_APPLICANT_ID=" + fileName.Substring(fileName.LastIndexOf('_') + 1) + "&";
                string BoletoLink = "Boletos<COMMON_BOLETO_URL=window.open(" + str + "','Boleto','resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50" + ")>";
                //if(CalledFrom!="INITIAL_LOAD")
                    this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, ClsCommon.FetchGeneralMessage("1166", "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, BoletoLink, "", 0);
            }
            return fileName;
        }
        /*
        /// <summary>
        /// Update policy list table if the initial load document is generated
        /// </summary>
        /// <param name="_customer_id"></param>
        /// <param name="_policy_id"></param>
        /// <param name="_policy_version_id"></param>
        private void UpdateInitialLoadDocumentGeneratedData(int _customer_id,int _policy_id,int _policy_version_id)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", _customer_id);
                objWrapper.AddParameter("@POLICY_ID", _policy_id);
                objWrapper.AddParameter("@POLICY_VERSION_ID", _policy_version_id);
                objWrapper.AddParameter("@CALLED_FROM", "POLICY");
                int returnResult = objWrapper.ExecuteNonQuery("PROC_POL_IL_UPDATE_POLICY_BOLETO_DATA");
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
             
                ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                throw (ex);
                 
            }
        }
        */
        //Itrack 1383
        #region Insert Data into print jobs table 
        //Added by Pradeep on 18 July 2011
        public void InsertPrintJobsEntryOfBoletoRePrint(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int CO_APPLICANT_ID,int USER_ID)
        {
            String DOCUMENT_CODE = "BOLETO";
            int ENTITY_ID = CO_APPLICANT_ID;
            String fileName = String.Empty;
            fileName = "BOLETO_" + CUSTOMER_ID.ToString() + "_" + POLICY_ID.ToString() + "_" + POLICY_VERSION_ID.ToString() + "_" + CO_APPLICANT_ID.ToString();
            String  FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
            this.AddPrintJobsEntryOfBoletoReprint(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, DOCUMENT_CODE, FILE_NAME, ENTITY_ID, USER_ID);

            string str = "/cms/Policies/Aspx/PolicyBoleto.aspx?CUSTOMER_ID=" + CUSTOMER_ID.ToString()
                            + "&POLICY_ID=" + POLICY_ID.ToString()
                            + "&POLICY_VERSION_ID=" + POLICY_VERSION_ID.ToString()
                            + "&INSTALLMENT_NO=0&CO_APPLICANT_ID=" + fileName.Substring(fileName.LastIndexOf('_') + 1) + "&";
            string BoletoLink = "Boletos<COMMON_BOLETO_URL=window.open(" + str + "','Boleto','resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50" + ")>";
            this.WriteTransactionLog(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, ClsCommon.FetchGeneralMessage("1166", "") + "<BR>" + FILE_NAME, USER_ID, BoletoLink, "", 0);
        }
        public void AddPrintJobsEntryOfBoletoReprint(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, string DOCUMENT_CODE, string FILE_NAME, int ENTITY_ID, int CREATED_BY)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objWrapper.AddParameter("@DOCUMENT_CODE", DOCUMENT_CODE);
                objWrapper.AddParameter("@FILE_NAME", FILE_NAME);
                objWrapper.AddParameter("@ENTITY_ID", ENTITY_ID);
                objWrapper.AddParameter("@CREATED_BY", CREATED_BY);
                objWrapper.AddParameter("@CREATED_DATETIME", System.DateTime.Now);
                int returnResult = objWrapper.ExecuteNonQuery("Proc_InsertPrintJobsEntryOfBoletoRePrint");
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while adding entry in prin jobs.file not found.");
                addInfo.Add("CustomerID", CUSTOMER_ID.ToString());
                addInfo.Add("PolicyID", POLICY_ID.ToString());
                addInfo.Add("PolicyVersionID", POLICY_VERSION_ID.ToString());
                addInfo.Add("DOCUMENT_CODE", DOCUMENT_CODE);
                Exception ex1 = new Exception("Error while inserting Print job entry.File not found.");
                ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex, addInfo);
                throw (ex1);
                throw new Exception("Error while adding data to print jobs table", ex1);
            }

        }
        //Added till here 
        #endregion
        // REGION FOR APPLICTION_REFUSAL

        #region APPLICTION_REFUSAL

        private DataSet fetchApplication_Refusal(int CustomerID, int PolicyId, int PolVersionId)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);
                DataSet ds = objWrapper.ExecuteDataSet("Proc_Application_Refusal");
                objWrapper.ClearParameteres();
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //THIS CODE IS USED FOR CREATE HTML OF APPLICATION REFUSAL AND SAVE IT IN DATABASE

        private void Create_Html_AppRefusal(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            try
            {

                DataSet ds = fetchApplication_Refusal(CustomerID, PolicyID, PolicyVersionID);
                StringBuilder HTML = new StringBuilder();  // Creating an object of StringBuilder
                HTML.Append("<html>");// head
                HTML.Append("<head>");// head
                HTML.Append("<style type=text/css>");//css
                HTML.Append("#font{font-size:20px;font-family:Times New Roman;}");
                HTML.Append("#font1{font-size:22px;font-family:Times New Roman;}");
                HTML.Append("#font2{font-size:25px;font-family:Times New Roman;}");
                HTML.Append("#blackline{border-bottom:solid black 1.5px;}");
                HTML.Append("#style{font-style:italic;font-size:22px;font-family:Times New Roman;}");
                HTML.Append("#img{vertical-align:middle;}");
                HTML.Append("</style>");
                HTML.Append(" </head>");
                HTML.Append("<body>");
                HTML.Append("<table align=center width=100%>");
                //HTML.Append("<col width=40%>");
                //HTML.Append("<col width=60%>");
                HTML.Append("<tr>");
                HTML.Append("<td align=center>");
                HTML.Append("<img id=img alt='' width=100px height=95px src='Image/pic1.PNG' />");
                //HTML.Append("</td>");
                //HTML.Append("<td align=left>");
                HTML.Append("<b id=font2> COMPANHIA DE SEGUROS ALIANÇA DA BAHIA</b>");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr><td colspan=2><br><div id=blackline></div></td></tr>");
                HTML.Append("<tr align=right>");
                HTML.Append("<td colspan=2><b id=font1>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbspSÃO PAULO,&nbsp");
                HTML.Append(ds.Tables[0].Rows[0]["Sao_Paulo"].ToString());
                HTML.Append("</b>");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr><td colspan=2><br><br><br></td></tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font colspan=2><b>À");
                HTML.Append("<br />");
                HTML.Append(ds.Tables[0].Rows[0]["BROKER_NAME"].ToString());
                HTML.Append("</BR>");
                HTML.Append(ds.Tables[0].Rows[0]["BROKER_ADDRESS"].ToString());
                HTML.Append("</BR>");
                HTML.Append(ds.Tables[0].Rows[0]["ZIP"].ToString()); //Corretor de Seguro
                HTML.Append("</b>");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr><td colspan=2><br><br><br><br><br></td></tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font colspan=2><b>Ref.:</b>");
                //HTML.Append("<br />");
                HTML.Append("<b id=style>Segurado: ");  //Added by Aditya for Tfs bug # 1165
                HTML.Append(ds.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString());
                HTML.Append("<br />");
                HTML.Append("<b id=style>Proposta de Seguro Empresarial Nº. ");
                HTML.Append(ds.Tables[0].Rows[0]["PROPOSTA_DE_SEGURO"].ToString());
                HTML.Append("<br />");
                HTML.Append("<b id=style>Pedido do Corretor: ");
                HTML.Append(ds.Tables[0].Rows[0]["PEDIDO_DO_CORRETOR"].ToString());  // Added till here
               // HTML.Append("<b id=style>Proposta de Seguro");
                //HTML.Append(ds.Tables[0].Rows[0]["Ref"].ToString());
                HTML.Append("</b>");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr><td colspan=2><br><br><br><br><br></td></tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font1 colspan=2>Informamos a V. Sas. que o risco proposto encontra-se fora das normas de aceitação desta seguradora, de modo que não será possível a emissão da apólice solicitada.</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr><td colspan=2><br><br><br><br><br><br><br><br><br></td></tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font1 colspan=2>Atenciosamente</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr><td colspan=2><br><br><br></td></tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=style colspan=2><b>Cia. de Seguros Alianca da Bahia</b></td>");
                HTML.Append("</tr>");
                HTML.Append("<tr><td colspan=2><br><br><br><br><br><br><div id=blackline></div></td></tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=2>SUCURSAL ");
                HTML.Append(ds.Tables[0].Rows[0]["Branch_Name"].ToString());
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("</table>");
                //HTML.Append("</tr>");
                //HTML.Append("</td>");
                //HTML.Append("</table>");
                HTML.Append("</body>");
                HTML.Append("</html>");
                string str = HTML.ToString();
                str = str.Replace("'", "''");
                this.SavePolicyDocumentXml(CustomerID, PolicyID, PolicyVersionID, str, "REFUSAL");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public string generateRefusalLettter(int CustomerID, int PolicyID, int PolicyVersionID, int userID, int ProcessId, int ProcessRowId)
        {

            try
            {
                //generate and save refusal letter html/xml
                Create_Html_AppRefusal(CustomerID, PolicyID, PolicyVersionID);
                //  add entry in print job;

                return AddPrintJobEntries(CustomerID, PolicyID, PolicyVersionID, "REFUSAL", userID, ProcessId, ProcessRowId);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        #endregion
        /*
        //added by Pradeep Kushwaha on 17-nov-2011
        /// <summary>
        /// Initial Load data entry 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="DocumentCode"></param>
        /// <param name="userID"></param>
        /// <param name="ProcessId"></param>
        /// <param name="ProcessRowId"></param>
        /// <returns></returns>
        private string AddPrintJobEntriesforInitialLoad(int CustomerID, int PolicyID, int PolicyVersionID, string DocumentCode, int userID, int ProcessId, int ProcessRowId)
        {
            // fetching Policy details
            try
            {
                string AgencyCode = "", CarrierCode = "", fileName = "", CoAppMessageId = "", BrokerMessageID = "", CarrierMessageID = "", ReinsurerMessageID = "";
                if (DocumentCode == "REFUSAL")
                {
                    CoAppMessageId = "1617"; BrokerMessageID = "1618"; CarrierMessageID = "1956"; ReinsurerMessageID = "1952";
                }
                else if (DocumentCode == "CANC_NOTICE")
                {
                    CoAppMessageId = "1620"; BrokerMessageID = "1621"; CarrierMessageID = "1957"; ReinsurerMessageID = "1953";
                }
                else if (DocumentCode == "POLICY_DOC")
                {
                    CoAppMessageId = "1449"; BrokerMessageID = "1450"; CarrierMessageID = "1955"; ReinsurerMessageID = "1954";
                }
                Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo;
                DataSet dsBRCO = getBrokerCoInsurer(CustomerID, PolicyID, PolicyVersionID);
                CarrierCode = dsBRCO.Tables[3].Rows[0]["CARRIER_CODE"].ToString();
                AgencyCode = dsBRCO.Tables[3].Rows[0]["AGENCY_CODE"].ToString();
                objPrintJobsInfo = GetPrintJobsValues(CustomerID, PolicyID, PolicyVersionID, ProcessId, ProcessRowId, userID, "", AgencyCode, CarrierCode);
                for (int i = 0; i < dsBRCO.Tables[0].Rows.Count; i++)
                {   //for 1778---CLIENTE
                    //
                    fileName = "CLIENTE" + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString();
                    //fileName = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1778", "") + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString();
                    objPrintJobsInfo.DOCUMENT_CODE = DocumentCode;
                    objPrintJobsInfo.ENTITY_TYPE = "CUSTOMER";// +dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString();
                    if (dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString() != "")
                        objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString());
                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                    AddPrintJobs(objPrintJobsInfo);

                    string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                    //this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage(CoAppMessageId, "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Refusal Letter for Co-Applicant generated successfully."
                }
                // adding entries in PRint jobs table for Each Broker to Generate and Print Policy Docs
                for (int i = 0; i < dsBRCO.Tables[1].Rows.Count; i++)
                {   //1777--CORRETOR
                    fileName = "CORRETOR" + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[1].Rows[i]["BROKER_ID"].ToString();
                    //fileName = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1777", "") + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[1].Rows[i]["BROKER_ID"].ToString();
                    objPrintJobsInfo.DOCUMENT_CODE = DocumentCode;
                    objPrintJobsInfo.ENTITY_TYPE = "BROKER";
                    if (dsBRCO.Tables[1].Rows[i]["BROKER_ID"].ToString() != "")
                        objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[1].Rows[i]["BROKER_ID"].ToString());
                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                    AddPrintJobs(objPrintJobsInfo);
                    string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                    //this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage(BrokerMessageID, "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Refusal Letter for Broker generated successfully."

                }
                
                for (int i = 0; i < dsBRCO.Tables[3].Rows.Count; i++)
                {
                    //1958--TÉCNICA
                    string leaderFolower = "TÉCNICA";// Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1958", "");
                    fileName = leaderFolower + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[3].Rows[i]["COMPANY_ID"].ToString();
                    objPrintJobsInfo.DOCUMENT_CODE = DocumentCode;
                    objPrintJobsInfo.ENTITY_TYPE = leaderFolower;
                    if (dsBRCO.Tables[3].Rows[i]["COMPANY_ID"].ToString() != "")
                        objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[3].Rows[i]["COMPANY_ID"].ToString());
                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                    AddPrintJobs(objPrintJobsInfo);
                    string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                    //this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage(CarrierMessageID, "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Refusal Letter for Carrier generated successfully."
                }
                // adding entries in PRint jobs table for Reinsurer Print Policy Docs iTrack 964 Note # 20
                for (int i = 0; i < dsBRCO.Tables[4].Rows.Count; i++)
                {
                    //1951--Ressegurador
                    string leaderFolower = "Ressegurador";//Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1951", "");
                    fileName = leaderFolower + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[4].Rows[i]["COMPANY_ID"].ToString();
                    objPrintJobsInfo.DOCUMENT_CODE = DocumentCode;
                    objPrintJobsInfo.ENTITY_TYPE = leaderFolower;
                    if (dsBRCO.Tables[4].Rows[i]["COMPANY_ID"].ToString() != "")
                        objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[4].Rows[i]["COMPANY_ID"].ToString());
                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                    AddPrintJobs(objPrintJobsInfo);
                    string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                    //this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage(ReinsurerMessageID, "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Refusal Letter for Reinsurer generated successfully."
                }
                return fileName;
            }
            catch (Exception ex)
            {
                System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while adding 'inital load' " + DocumentCode + " in Print job.");
                addInfo.Add("CustomerID", CustomerID.ToString());
                addInfo.Add("PolicyID", PolicyID.ToString());
                addInfo.Add("PolicyVersionID", PolicyVersionID.ToString());
                addInfo.Add("ProcessRowID", ProcessId.ToString());
                addInfo.Add("DOCUMENT_CODE", DocumentCode);
                ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex, addInfo);
                throw (ex);
            }
        }
        */
        private string AddPrintJobEntries(int CustomerID, int PolicyID, int PolicyVersionID, string DocumentCode, int userID, int ProcessId, int ProcessRowId)
        {
            // fetching Policy details
            try
            {
                string AgencyCode = "", CarrierCode = "", fileName = "", CoAppMessageId = "", BrokerMessageID = "", CarrierMessageID = "", ReinsurerMessageID = "";
                if (DocumentCode == "REFUSAL")
                {
                    CoAppMessageId = "1617"; BrokerMessageID = "1618"; CarrierMessageID = "1956"; ReinsurerMessageID = "1952";
                }
                else if (DocumentCode == "CANC_NOTICE")
                {
                    CoAppMessageId = "1620"; BrokerMessageID = "1621"; CarrierMessageID = "1957"; ReinsurerMessageID = "1953";
                }
                else if (DocumentCode == "POLICY_DOC")
                {
                    CoAppMessageId = "1449"; BrokerMessageID = "1450"; CarrierMessageID = "1955"; ReinsurerMessageID = "1954";
                }
                Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo;
                DataSet dsBRCO = getBrokerCoInsurer(CustomerID, PolicyID, PolicyVersionID);
                CarrierCode = dsBRCO.Tables[3].Rows[0]["CARRIER_CODE"].ToString();
                AgencyCode = dsBRCO.Tables[3].Rows[0]["AGENCY_CODE"].ToString();
                objPrintJobsInfo = GetPrintJobsValues(CustomerID, PolicyID, PolicyVersionID, ProcessId, ProcessRowId, userID, "", AgencyCode, CarrierCode);
                for (int i = 0; i < dsBRCO.Tables[0].Rows.Count; i++)
                {
                    fileName = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1778", "") + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString();
                    objPrintJobsInfo.DOCUMENT_CODE = DocumentCode;
                    objPrintJobsInfo.ENTITY_TYPE = "CUSTOMER";// +dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString();
                    if (dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString() != "")
                        objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString());
                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                    AddPrintJobs(objPrintJobsInfo);

                    string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                    this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage(CoAppMessageId, "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Refusal Letter for Co-Applicant generated successfully."
                }
                // adding entries in PRint jobs table for Each Broker to Generate and Print Policy Docs
                for (int i = 0; i < dsBRCO.Tables[1].Rows.Count; i++)
                {
                    fileName = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1777", "") + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[1].Rows[i]["BROKER_ID"].ToString();
                    objPrintJobsInfo.DOCUMENT_CODE = DocumentCode;
                    objPrintJobsInfo.ENTITY_TYPE = "BROKER";
                    if (dsBRCO.Tables[1].Rows[i]["BROKER_ID"].ToString() != "")
                        objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[1].Rows[i]["BROKER_ID"].ToString());
                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                    AddPrintJobs(objPrintJobsInfo);
                    string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                    this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage(BrokerMessageID, "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Refusal Letter for Broker generated successfully."

                }
                // adding entries in PRint jobs table for follower to Generate and Print Policy Docs
                /* *LEADER/FOLLOWER copy was not required, please delete it; itrack 964 note# 20
                for (int i = 0; i < dsBRCO.Tables[2].Rows.Count; i++)
                {
                    string leaderFolower = dsBRCO.Tables[2].Rows[i]["LEADER_FOLLOWER"].ToString();
                    leaderFolower = leaderFolower == "14548" ? Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1779", "") : Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1780", "");
                    fileName = leaderFolower + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[2].Rows[i]["COMPANY_ID"].ToString();
                    objPrintJobsInfo.DOCUMENT_CODE = DocumentCode;
                    objPrintJobsInfo.ENTITY_TYPE = leaderFolower;
                    if (dsBRCO.Tables[2].Rows[i]["COMPANY_ID"].ToString() != "")
                        objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[2].Rows[i]["COMPANY_ID"].ToString());
                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                    AddPrintJobs(objPrintJobsInfo);
                    string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                    this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1619", "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Refusal Letter for follower generated successfully."
                }
                */
                // adding entries in PRint jobs table for Carrier Print Policy Docs iTrack 964 Note # 20
                for (int i = 0; i < dsBRCO.Tables[3].Rows.Count; i++)
                {
                    string leaderFolower = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1958", "");
                    fileName = leaderFolower + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[3].Rows[i]["COMPANY_ID"].ToString();
                    objPrintJobsInfo.DOCUMENT_CODE = DocumentCode;
                    objPrintJobsInfo.ENTITY_TYPE = leaderFolower;
                    if (dsBRCO.Tables[3].Rows[i]["COMPANY_ID"].ToString() != "")
                        objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[3].Rows[i]["COMPANY_ID"].ToString());
                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                    AddPrintJobs(objPrintJobsInfo);
                    string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                    this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage(CarrierMessageID, "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Refusal Letter for Carrier generated successfully."
                }
                // adding entries in PRint jobs table for Reinsurer Print Policy Docs iTrack 964 Note # 20
                for (int i = 0; i < dsBRCO.Tables[4].Rows.Count; i++)
                {
                    string leaderFolower = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1951", "");
                    fileName = leaderFolower + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[4].Rows[i]["COMPANY_ID"].ToString();
                    objPrintJobsInfo.DOCUMENT_CODE = DocumentCode;
                    objPrintJobsInfo.ENTITY_TYPE = leaderFolower;
                    if (dsBRCO.Tables[4].Rows[i]["COMPANY_ID"].ToString() != "")
                        objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[4].Rows[i]["COMPANY_ID"].ToString());
                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                    AddPrintJobs(objPrintJobsInfo);
                    string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                    this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage(ReinsurerMessageID, "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Refusal Letter for Reinsurer generated successfully."
                }
                return fileName;
            }
            catch (Exception ex)
            {
                System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while adding " + DocumentCode + " in Print job.");
                addInfo.Add("CustomerID", CustomerID.ToString());
                addInfo.Add("PolicyID", PolicyID.ToString());
                addInfo.Add("PolicyVersionID", PolicyVersionID.ToString());
                addInfo.Add("ProcessRowID", ProcessId.ToString());
                addInfo.Add("DOCUMENT_CODE", DocumentCode);
                ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex, addInfo);
                throw (ex);
            }
        }



        //REGION FOR CANCELATION_DUE_NOPAY

        #region CANCELATION_DUE_NOPAY

        private DataSet fetchPolicy_Canc_Nopayment(int CustomerID, int PolicyId, int PolVersionId)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);
                DataSet ds = objWrapper.ExecuteDataSet("Proc_PolicyCancelation_Nopayment");
                objWrapper.ClearParameteres();
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //THIS CODE IS USED FOR CREATE HTML OF CANCELLATION DUE TO NO PAYMENT AND SAVE IT IN DATABASE

        private string Create_Html_CancNoPay(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            try
            {
                DataSet ds = fetchPolicy_Canc_Nopayment(CustomerID, PolicyID, PolicyVersionID);
                DataTable dt = new DataTable();
                if (ds == null || ds.Tables.Count < 0 || ds.Tables[0].Rows.Count < 1)
                {
                    return "";
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    dt = ds.Tables[1];
                }
                StringBuilder HTML1 = new StringBuilder();  // Creating an object of StringBuilder
                int ctr = 0;
                while (ctr < dt.Rows.Count)
                {
                    HTML1.Append("<tr>");
                    HTML1.Append("<td>");
                    HTML1.Append(dt.Rows[ctr]["Due_Date"].ToString());
                    HTML1.Append("</td>");
                    HTML1.Append("<td>");
                    HTML1.Append(dt.Rows[ctr]["INSTALLMENT_NO"].ToString());
                    HTML1.Append("</td>");
                    HTML1.Append("<td>");
                    HTML1.Append(dt.Rows[ctr]["Installment_Amount"].ToString());
                    HTML1.Append("</td>");
                    HTML1.Append("</tr>");
                    ctr++;
                }
                StringBuilder HTML = new StringBuilder();  // Creating an object of StringBuilder
                HTML.Append("<html>");
                HTML.Append("<head>");// head

                HTML.Append("<style type=text/css>");//css

                HTML.Append("#font1{font-size:20px;font-family:Times New Roman;}");
                HTML.Append("#font{font-size:12pt;font-family:Times New Roman;}");
                HTML.Append("#blackline{border-bottom:solid black 1.5px;}");

                HTML.Append("</style>");
                HTML.Append(" </head>");
                HTML.Append("<body>");

                HTML.Append("<table align=center width=100% height=120%>");//table start
                HTML.Append("<col width=25%/>");
                HTML.Append("<col width=75%/>");
                HTML.Append("<tr>");
                HTML.Append("<td align=right><img alt='' width=95px height=90px src='/Cms/CmsWeb/images/AliancaLogo.gif' /></td>");
                HTML.Append("<td valign=middle id=font1><b>COMPANHIA DE SEGUROS ALIANÇA DA BAHIA</b></td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=2 valign=top><div id=blackline></div></td>");
                //HTML.Append("<td colspan=2>");
                //HTML.Append("<hr></hr>");
                //HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr><td id=font></td>");
                HTML.Append("<td id=font align=right>");
                HTML.Append("São Paulo, ");
                HTML.Append(ds.Tables[0].Rows[0]["Sao_Paulo"].ToString()); // <day> de <month> de <year>
                HTML.Append("</td></tr>");
                HTML.Append("<tr><td colspan=2 id=font><br />");
                HTML.Append("Prezado(s) Sr(s).: ");
                HTML.Append(ds.Tables[0].Rows[0]["APPLICANT_NAME"].ToString());//(applicant name or coapplicant name)
                HTML.Append("<br /></td></tr>");
                HTML.Append("<tr><td colspan=2 id=font>");
                HTML.Append("Comunicamos a(s) V. Sa.(s) que, nesta data, estamos cancelando a sua apólice de seguro conforme abaixo, por falta de pagamento.");
                HTML.Append("<br /></td></tr>");
                HTML.Append("<tr><td colspan=2 id=font>");
                HTML.Append("Caso a(s) referida(s) prestação(ões) já tenha(m) sido liquidada(s), solicitamos que nos remeta(m) por gentileza a(s) cópia(s) do(s) respectivo(s) carnê(s) autenticado(s) mecanicamente, a fim de ser(em) solucionado(s) de imediato.");
                HTML.Append("</td></tr>");
                HTML.Append("<tr><td colspan=2 id=font>");
                HTML.Append("<table width=100%>");
                HTML.Append("<col width=33%/>");
                HTML.Append("<col width=33%/>");
                HTML.Append("<col width=34%/>");
                HTML.Append("<tr><td valign=top>APÓLICE: ");
                HTML.Append(ds.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()); //<policy no.>
                HTML.Append("</td>");
                HTML.Append("<td valign=top>PROPOSTA: ");
                HTML.Append(ds.Tables[0].Rows[0]["APP_NUMBER"].ToString()); //<application no.>	
                HTML.Append("</td>");
                HTML.Append("<td valign=top>RAMO: ");
                HTML.Append(ds.Tables[0].Rows[0]["Product_Name"].ToString()); //<product name>
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td>ENDOSSO: ");
                HTML.Append(ds.Tables[0].Rows[0]["ENDORSEMENT_NO"].ToString()); //(endorsement no.)
                HTML.Append("</td>");
                HTML.Append("<td></td>");
                HTML.Append("<td></td>");
                HTML.Append("</tr>");
                //HTML.Append("<tr>");
                //HTML.Append("<td colspan=3></td>");
                //HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<table align=center border=0 cellspacing=0 width=100%>");
                HTML.Append("<col width=33%/>");
                HTML.Append("<col width=33%/>");
                HTML.Append("<col width=34%/>");
                HTML.Append("<tr>");
                HTML.Append("<td>");
                HTML.Append("VENCIMENTO");
                HTML.Append("</td>");
                HTML.Append("<td>");
                HTML.Append("PRESTAÇÃO");
                HTML.Append("</td>");
                HTML.Append("<td>");
                HTML.Append("VALOR");
                HTML.Append("</td>");
                HTML.Append("</b>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colsapn=3>");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append(HTML1);
                //HTML.Append("<br />");
                HTML.Append("</table>");
                HTML.Append("</tr>");
                //HTML.Append("<tr>"); // comentted bu shubhanshu on 28/07/2011
                //HTML.Append("<td colspan=3></td>");
                //HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3 id=font>Este acompanhamento faz parte da nossa preocupação em não deixá-lo(s) sem as coberturas e garantias do SEGURO.</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3 id=font>A sua apólice de seguro representa um valioso patrimônio, a qual deve ser preservada a qualquer custo.</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append(" <td colspan=3 id=font>Agradecendo-lhe(s), antecipadamente, seu pronunciamento, subscrevemo-nos.</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3 id=font>Cordialmente,</td>");
                HTML.Append(" </tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3 id=font><br /><br /><br />Cia. de Seguros Aliança da Bahia<br />Setor de Cobrança e Pagamento</td>");
                HTML.Append(" </tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3 id=font>");
                //Work item 80. Modified By Shubhanshu Panday.August 05 2011.itrack # 1104
                if (ctr == 0)
                {
                    HTML.Append("<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />");
                }
                else if (ctr == 1)
                {
                    HTML.Append("<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />");
                }
                else if (ctr == 2)
                {
                    HTML.Append("<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />");
                }
                else if (ctr == 3)
                {
                    HTML.Append("<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />");//shub
                }
                else if (ctr == 4)
                {
                    HTML.Append("<br /><br /><br /><br /><br /><br /><br /><br /><br /><br />");
                }
                else if (ctr == 5)
                {
                    HTML.Append("<br /><br /><br /><br /><br /><br /><br /><br /><br />");
                }
                else if (ctr == 6)
                {
                    HTML.Append("<br /><br /><br /><br /><br /><br /><br /><br />");
                }
                else if (ctr == 7)
                {
                    HTML.Append("<br /><br /><br /><br /><br /><br />");//shub
                }
                else if (ctr == 8)
                {
                    HTML.Append("<br /><br /><br /><br /><br />");//shub
                }
                else if (ctr == 9)
                {
                    HTML.Append("<br /><br /><br /><br />");//shub
                }
                else if (ctr == 10)
                {
                    HTML.Append("<br /><br /><br />");//shub
                }
                else if (ctr == 11)
                {
                    HTML.Append("<br />");//shub
                }
                HTML.Append("<hr></hr>");
                HTML.Append(ds.Tables[0].Rows[0]["Branch_Name"].ToString()); //Branch_Name,Street,number,complement,zip code,city,phone no,fax no
                HTML.Append("</td>");
                HTML.Append(" </tr>");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("</table>");

                HTML.Append("</body>");
                HTML.Append("<html>");





                /*DataTable dt = new DataTable();
                if (ds == null || ds.Tables.Count < 0 || ds.Tables[0].Rows.Count < 1)
                {
                    return "";
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    dt = ds.Tables[1];
                }
                StringBuilder HTML1 = new StringBuilder();  // Creating an object of StringBuilder
                int ctr = 0;
                while (ctr < dt.Rows.Count)
                {
                    HTML1.Append("<tr>");
                    HTML1.Append("<td>");
                    HTML1.Append(dt.Rows[ctr]["Due_Date"].ToString());
                    HTML1.Append("</td>");
                    HTML1.Append("<td align=right>");
                    HTML1.Append(dt.Rows[ctr]["INSTALLMENT_NO"].ToString());
                    HTML1.Append("</td>");
                    HTML1.Append("<td align=right>");
                    HTML1.Append(dt.Rows[ctr]["Installment_Amount"].ToString());
                    HTML1.Append("</td>");
                    HTML1.Append("</tr>");
                    ctr++;
                }
                StringBuilder HTML = new StringBuilder();  // Creating an object of StringBuilder
                HTML.Append("<html>");
                HTML.Append("<head>");// head

                HTML.Append("<style type=text/css>");//css

                HTML.Append("#font1{font-size:22px;font-family:Times New Roman;}");
                HTML.Append("#font{font-size:17px;font-family:Times New Roman;}");
                HTML.Append("#blackline{border-bottom:solid black 1.5px;}");

                HTML.Append("</style>");
                HTML.Append(" </head>");
                HTML.Append("<body>");

                HTML.Append("<table align=center width=100%>");//table start
                HTML.Append("<col width=30%/>");
                HTML.Append("<col width=70%/>");
                HTML.Append("<tr>");
                HTML.Append("<td align=right><img alt='' width=95px height=90px src='/Cms/CmsWeb/images/AliancaLogo.gif' /></td>");
                HTML.Append("<td valign=middle id=font1><b>COMPANHIA DE SEGUROS ALIANÇA DA BAHIA</b></td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=2 valign=top><div id=blackline></div></td>");
                HTML.Append("</tr>");
                HTML.Append("<tr><td id=font></td>");
                HTML.Append("<td id=font align=right>");
                HTML.Append("São Paulo,");
                HTML.Append(ds.Tables[0].Rows[0]["Sao_Paulo"].ToString()); // <day> de <month> de <year>
                HTML.Append("</td></tr>");
                HTML.Append("<tr><td colspan=2 id=font><br />");
                HTML.Append("Prezado(s) Sr(s).: ");
                HTML.Append(ds.Tables[0].Rows[0]["APPLICANT_NAME"].ToString());//(applicant name or coapplicant name)
                HTML.Append("</td></tr>");
                HTML.Append("<tr><td colspan=2 id=font>");
                HTML.Append("Comunicamos a(s) V. Sa.(s) que, nesta data, estamos cancelando a sua apólice de seguro conforme abaixo, por falta de pagamento.");
                HTML.Append("</td></tr>");
                HTML.Append("<tr><td colspan=2 id=font>");
                HTML.Append("Caso a(s) referida(s) prestação(ões) já tenha(m) sido liquidada(s), solicitamos que nos remeta(m) por gentileza a(s) cópia(s) do(s) respectivo(s) carnê(s) autenticado(s) mecanicamente, a fim de ser(em) solucionado(s) de imediato.");
                HTML.Append("</td></tr>");
                HTML.Append("<tr><td colspan=2 id=font>");
                HTML.Append("<table width=100%>");
                HTML.Append("<col width=33%/>");
                HTML.Append("<col width=33%/>");
                HTML.Append("<col width=34%/>");
                HTML.Append("<tr><td valign=top>APÓLICE: ");
                HTML.Append(ds.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()); //<policy no.>
                HTML.Append("</td>");
                HTML.Append("<td valign=top>PROPOSTA: ");
                HTML.Append(ds.Tables[0].Rows[0]["APP_NUMBER"].ToString()); //<application no.>	
                HTML.Append("</td>");
                HTML.Append("<td valign=top>RAMO: ");
                HTML.Append(ds.Tables[0].Rows[0]["Product_Name"].ToString()); //<product name>
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td>ENDOSSO: ");
                HTML.Append(ds.Tables[0].Rows[0]["ENDORSEMENT_NO"].ToString()); //(endorsement no.)
                HTML.Append("</td>");
                HTML.Append("<td></td>");
                HTML.Append("<td></td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3></td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<table align=center border=0 cellspacing=0 width=100%>");
                HTML.Append("<col width=33%/>");
                HTML.Append("<col width=33%/>");
                HTML.Append("<col width=34%/>");
                HTML.Append("<tr>");
                HTML.Append("<td>");
                HTML.Append("VENCIMENTO");
                HTML.Append("</td>");
                HTML.Append("<td>");
                HTML.Append("PRESTAÇÃO");
                HTML.Append("</td>");
                HTML.Append("<td>");
                HTML.Append("VALOR");
                HTML.Append("</td>");
                HTML.Append("</b>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colsapn=3>");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append(HTML1);
                HTML.Append("</table>");
                HTML.Append("</ItemTemplate>");
                HTML.Append("</asp:Repeater>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3></td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3 id=font>Este acompanhamento faz parte da nossa preocupação em não deixá-lo(s) sem as coberturas e garantias do SEGURO.</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append(" <td colspan=3 id=font>A sua apólice de seguro representa um valioso patrimônio, a qual deve ser preservada a qualquer custo.</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append(" <td colspan=3 id=font>Agradecendo-lhe(s), antecipadamente, seu pronunciamento, subscrevemo-nos.</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3 id=font>Cordialmente,</td>");
                HTML.Append(" </tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3 id=font><br /><br /><br /><br />Cia. de Seguros Aliança da Bahia<br />Setor de Cobrança e Pagamento</td>");
                HTML.Append(" </tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3 id=font>");
                int No_of_BR = 0;
                No_of_BR = 12 - ctr;
                for (int i = No_of_BR; i > 0; i--)
                {
                    HTML.Append("<br />");
                    No_of_BR--;
                }
                HTML.Append(ds.Tables[0].Rows[0]["Branch_Name"].ToString()); //Branch_Name,Street,number,complement,zip code,city,phone no,fax no
                HTML.Append("</td>");
                HTML.Append(" </tr>");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("</table>");

                HTML.Append("</body>");
                HTML.Append("<html>");*/


                string str = HTML.ToString();
                str = str.Replace("'", "''");
                return str;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public string generateCancellationDocuments(int CustomerID, int PolicyID, int PolicyVersionID, string CalledFor, int userID, int ProcessId, int ProcessRowId)
        {
            // fetching Policy details
            try
            {

                string str1 = Create_Html_CancNoPay(CustomerID, PolicyID, PolicyVersionID);
                this.SavePolicyDocumentXml(CustomerID, PolicyID, PolicyVersionID, str1, "CANC_NOTICE");
     
                return AddPrintJobEntries(CustomerID, PolicyID, PolicyVersionID, "CANC_NOTICE", userID, ProcessId, ProcessRowId);
                /*  Code commented as Common Function to add Print job entries called
                string AgencyCode = "", CarrierCode = "", fileName = "";
                Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo;
                DataSet dsBRCO = getBrokerCoInsurer(CustomerID, PolicyID, PolicyVersionID);
                CarrierCode = dsBRCO.Tables[3].Rows[0]["CARRIER_CODE"].ToString();
                AgencyCode = dsBRCO.Tables[3].Rows[0]["AGENCY_CODE"].ToString(); 
                
                objPrintJobsInfo = GetPrintJobsValues(CustomerID, PolicyID, PolicyVersionID, ProcessId, ProcessRowId, userID, "CANC_NOTICE", AgencyCode, CarrierCode);
                for (int i = 0; i < dsBRCO.Tables[0].Rows.Count; i++)
                {
                    fileName = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1778", "") + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString();
                    objPrintJobsInfo.DOCUMENT_CODE = "CANC_NOTICE";
                    objPrintJobsInfo.ENTITY_TYPE = "CUSTOMER";// +dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString();
                    if (dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString() != "")
                        objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[0].Rows[i]["CO_APPLICANT_ID"].ToString());
                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                    AddPrintJobs(objPrintJobsInfo);

                    string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                    this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1620", "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Cancellation Letter for Co-Applicant generated successfully."
                }
                // adding entries in PRint jobs table for Each Broker to Generate and Print Policy Docs
                for (int i = 0; i < dsBRCO.Tables[1].Rows.Count; i++)
                {
                    fileName = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1777", "") + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[1].Rows[i]["BROKER_ID"].ToString();
                    objPrintJobsInfo.DOCUMENT_CODE = "CANC_NOTICE";
                    objPrintJobsInfo.ENTITY_TYPE = "BROKER";
                    if (dsBRCO.Tables[1].Rows[i]["BROKER_ID"].ToString() != "")
                        objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[1].Rows[i]["BROKER_ID"].ToString());
                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                    AddPrintJobs(objPrintJobsInfo);
                    string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                    this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1621", "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Cancellation Letter for Broker generated successfully."

                }
                */
                // adding entries in PRint jobs table for follower to Generate and Print Policy Docs
                /* * LEADER/FOLLOWER copy was not required, please delete it; itrack 964 note# 20
                for (int i = 0; i < dsBRCO.Tables[2].Rows.Count; i++)
                {
                    string leaderFolower = dsBRCO.Tables[2].Rows[i]["LEADER_FOLLOWER"].ToString();
                    leaderFolower = leaderFolower == "14548" ? Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1779", "") : Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1780", "");
                    fileName = leaderFolower + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[2].Rows[i]["COMPANY_ID"].ToString();
                    objPrintJobsInfo.DOCUMENT_CODE = "CANC_NOTICE";
                    objPrintJobsInfo.ENTITY_TYPE = leaderFolower;
                    if (dsBRCO.Tables[2].Rows[i]["COMPANY_ID"].ToString() != "")
                        objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[2].Rows[i]["COMPANY_ID"].ToString());
                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                    AddPrintJobs(objPrintJobsInfo);
                    string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                    this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1622", "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Cancellation Letter for follower generated successfully."
                }
                */
                /*// adding entries in PRint jobs table for Carrier Print Policy Docs iTrack 964 Note # 20
                for (int i = 0; i < dsBRCO.Tables[3].Rows.Count; i++)
                {
                    string leaderFolower = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1958", "");
                    fileName = leaderFolower + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[3].Rows[i]["COMPANY_ID"].ToString();
                    objPrintJobsInfo.DOCUMENT_CODE = "CANC_NOTICE"; 
                    objPrintJobsInfo.ENTITY_TYPE = leaderFolower;
                    if (dsBRCO.Tables[3].Rows[i]["COMPANY_ID"].ToString() != "")
                        objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[3].Rows[i]["COMPANY_ID"].ToString());
                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                    AddPrintJobs(objPrintJobsInfo);
                    string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                    this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1957", "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Cancellation Letter for Carrier generated successfully."
                }
                // adding entries in PRint jobs table for Reinsurer Print Policy Docs iTrack 964 Note # 20
                for (int i = 0; i < dsBRCO.Tables[4].Rows.Count; i++)
                {
                    string leaderFolower = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1951", "");
                    fileName = leaderFolower + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + dsBRCO.Tables[4].Rows[i]["COMPANY_ID"].ToString();
                    objPrintJobsInfo.DOCUMENT_CODE = "CANC_NOTICE"; 
                    objPrintJobsInfo.ENTITY_TYPE = leaderFolower;
                    if (dsBRCO.Tables[4].Rows[i]["COMPANY_ID"].ToString() != "")
                        objPrintJobsInfo.ENTITY_ID = int.Parse(dsBRCO.Tables[4].Rows[i]["COMPANY_ID"].ToString());
                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                    AddPrintJobs(objPrintJobsInfo);
                    string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                    this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1953", "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Cancellation Letter for Reinsurer generated successfully."
                }
                return fileName;
                */



            }
            catch (Exception ex)
            {
                System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while generating Cancellation Letter XML and adding in Print job.");
                addInfo.Add("CustomerID", CustomerID.ToString());
                addInfo.Add("PolicyID", PolicyID.ToString());
                addInfo.Add("PolicyVersionID", PolicyVersionID.ToString());
                addInfo.Add("ProcessRowID", ProcessId.ToString());
                addInfo.Add("DOCUMENT_CODE", "CANC_NOTICE");
                ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex, addInfo);
                throw (ex);
            }
        }

        //REGION FOR CLAIM RECEIPT


        #endregion



        //REGION FOR CLAIM_RECEIPT

        #region CLAIM_RECEIPT

        // TO FETCH LIST OF PAYEE OF A CLAIM
        private DataSet GetClaimPayeeList(int ClaimID, int ActivityID)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CLAIM_ID", ClaimID);
                objWrapper.AddParameter("@ACTIVITY_ID", ActivityID);
                DataSet ds = objWrapper.ExecuteDataSet("Proc_GetClaimPayeeList");
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        // TO FETCH CLAIM RECIPT(HTML FORM) FOR A CLAIM AND ACTIVITY
        public DataSet GetClaimReports(int ClaimID, int ActivityID, string ReportType)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CLAIM_ID", ClaimID);
                objWrapper.AddParameter("@ACTIVITY_ID", ActivityID);
                objWrapper.AddParameter("@PROCESS_TYPE", ReportType);
                DataSet ds = objWrapper.ExecuteDataSet("Proc_GetProductClaimReciptHTML");
                objWrapper.ClearParameteres();
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        // TO FETCH DATA FOR HTML FORM OF CLAIM RECEIPT
        public DataSet fetch_ProductClaimReceipt(int ClaimID, int ActivityID, int PayeeID)
        {
            try
            {

                objWrapper.ClearParameteres();
                objWrapper.TransactionRequired = DataWrapper.MaintainTransaction.NO;
                objWrapper.AddParameter("@CLAIM_ID", ClaimID);
                objWrapper.AddParameter("@ACTIVITY_ID", ActivityID);
                objWrapper.AddParameter("@PAYEE_ID", PayeeID);
                DataSet ds = objWrapper.ExecuteDataSet("Proc_GetProductClaimRecipt");
                objWrapper.ClearParameteres();
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // THIS CODE IS USED FOR CREATE HTML OF PRODUCT CLAIM RECEIPT AND SAVE IT IN DATABASE
        private string GenerateClaimReceiptHTML(int ClaimID, int ActivityID, int PayeeID)
        {
            try
            {
                DataSet ds1 = fetch_ProductClaimReceipt(ClaimID, ActivityID, PayeeID);
                if (ds1.Tables.Count < 1 || ds1.Tables[0].Rows.Count < 1)
                    return "";
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                double COINSURANCE_PERCENT_TOTAL = 0;
                double PAYMENT_AMT = 0;
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
                {
                    dt = ds1.Tables[1];
                }
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[2].Rows.Count > 0)
                {
                    dt1 = ds1.Tables[2];
                }

                StringBuilder HTML1 = new StringBuilder(); // Creating an object of StringBuilder
                int ctr = 0;
                System.Globalization.NumberFormatInfo nfi = new System.Globalization.CultureInfo(enumCulture.BR, true).NumberFormat;
                nfi.NumberDecimalDigits = 2;

                while (ctr < dt.Rows.Count)
                {
                    HTML1.Append("<tr>");
                    HTML1.Append("<td valign=top id=font1>");
                    HTML1.Append(dt.Rows[ctr]["SUSEP_NUM"].ToString());
                    HTML1.Append("</td>");
                    HTML1.Append("<td valign=top id=font1>");
                    HTML1.Append(dt.Rows[ctr]["REIN_COMAPANY_NAME"].ToString());
                    HTML1.Append("</td>");
                    HTML1.Append("<td valign=top id=font1 align=right>");
                    HTML1.Append(Convert.ToDouble(dt.Rows[ctr]["COINSURANCE_PERCENT"]).ToString("N", nfi));
                    COINSURANCE_PERCENT_TOTAL += double.Parse(dt.Rows[ctr]["COINSURANCE_PERCENT"].ToString());
                    HTML1.Append("</td>");
                    HTML1.Append("<td valign=top id=font1 align=right>");
                    //HTML1.Append(dt.Rows[ctr]["PAYMENT_AMT"].ToString());
                    HTML1.Append(Convert.ToDouble(dt.Rows[ctr]["PAYMENT_AMT"]).ToString("N", nfi));
                    PAYMENT_AMT += double.Parse(dt.Rows[ctr]["PAYMENT_AMT"].ToString());
                    HTML1.Append("</td>");
                    HTML1.Append("</tr>");
                    ctr++;
                }


                StringBuilder HTML = new StringBuilder();  // Creating an object of StringBuilder
                HTML.Append("<html>");
                HTML.Append("<head>");// head
                HTML.Append("<style type=text/css>");//css
                HTML.Append("#m{text-align:right;border-style:solid;border-width:1px;border-color:black;}");
                HTML.Append("#font{font-size:14px;font-family:Arial;}");
                HTML.Append("#font3{font-size:13px;font-family:Arial;}");
                HTML.Append("#font1{font-family:Arial;font-size:10px;border-style:solid;border-width:1px;border-color:black;}");
                //HTML.Append("#font2{font-size:10px;font-family:Arial;border-style:solid;border-width:1px;border-color:black}"); /*commented By Ashu on 14 Jul 2011 --formatting issue*/
                HTML.Append("#font2{font-size:10px;font-family:Arial;}");/*Add By Ashu on 14 Jul 2011 --formatting issue*/
                HTML.Append("#font4{font-family:Arial;font-size:10px;}");
                /*Add By Ashu on 14 Jul 2011 --formatting issue*/
                HTML.Append(".mainDiv{height: 11.0in;border:0px solid red;}");
                HTML.Append(".divCodigo{float:left; border:1px solid #000000; margin-left:2px;  margin-right:2px; height:52.25%; overflow:hidden;}");
                /*Add By Ashu on 14 Jul 2011 --formatting issue End*/
                //HTML.Append("#font5{font-family:Arial;font-size:10px;border-style:solid;border-width:1px;border-color:black;}");
                HTML.Append("#border{font-family:Arial;border-style:solid;border-width:1px;border-color:black;}");
                HTML.Append("</style>");
                HTML.Append(" </head>");
                HTML.Append("<body>");
                HTML.Append("<div class=mainDiv>");/*add By Ashu on 14 Jul 2011 --formatting issue*/
                HTML.Append("<table align=center width=100%>");//table start
                HTML.Append("<col width=50%>");
                HTML.Append("<col width=50%>");
                HTML.Append("<tr class=home>");
                HTML.Append("<td rowspan=2 id=border>");
                HTML.Append("<table><tr><td>");
                HTML.Append("<img alt='' width=60px height=60px src='../../cmsweb/images/AliancaLogo.GIF' />");
                HTML.Append("</td>");
                HTML.Append("<td>");
                HTML.Append("<pre id=font>");
                HTML.Append("<b>");
                HTML.Append("COMPANHIA DE SEGUROS ALIANÇA DA BAHIA");
                HTML.Append("</b>");
                HTML.Append("<br />");
                //HTML.Append(ds1.Tables[0].Rows[0]["DIV_ADDRESS"].ToString());
                HTML.Append(ds1.Tables[0].Rows[0]["DIV_ADDRESS1"].ToString());
                HTML.Append(",<BR />");
                HTML.Append(ds1.Tables[0].Rows[0]["DIV_ADDRESS2"].ToString());

                HTML.Append("</pre>");
                HTML.Append("</td></tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font align=right colspan=2>");
                HTML.Append("C.S:  ");
                HTML.Append(ds1.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString());
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("</table></td>");
                HTML.Append("<td id=m>");
                HTML.Append("<b id=font>");
                HTML.Append("RECIBO DE SINISTRO Nº &nbsp&nbsp&nbsp");
                HTML.Append(ds1.Tables[0].Rows[0]["CLAIM_RECEIPT_NUMBER"].ToString()); // (claim receipt number)
                HTML.Append("</b></td></tr>");
                HTML.Append("<tr id=m>");
                HTML.Append("<td id=border align=center>");
                HTML.Append(ds1.Tables[0].Rows[0]["CLAIM_PAYMENT_TYPE"].ToString()); //(claim payment type (if it’s expense, indemnity))
                HTML.Append("</td></tr></table>");//End
                HTML.Append("<table align=center width=100%>");//Table Start
                HTML.Append("<tr valign=top>");
                HTML.Append("<td id=font1>");
                HTML.Append("ORGÃO EMISSOR<br>");//(Issuance Org Code) - (Issuance Org Description)
                HTML.Append(ds1.Tables[0].Rows[0]["BRANCH_CODE"].ToString());
                HTML.Append(" - ");
                HTML.Append(ds1.Tables[0].Rows[0]["DIV_NAME"].ToString());
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("RAMO<br>");
                HTML.Append(ds1.Tables[0].Rows[0]["SUSEP_LOB_CODE"].ToString()); //(SUSEP LOB Code – Description)
                HTML.Append(" - ");
                HTML.Append(ds1.Tables[0].Rows[0]["LOB_DESC"].ToString());
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("APÓLICE/BILHETE Nº<br>");
                HTML.Append(ds1.Tables[0].Rows[0]["POLICY_NUMBER"].ToString());  //(Policy no.)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("DOC. COMPLEMENTAR Nº<br>");
                HTML.Append(ds1.Tables[0].Rows[0]["ENDORSEMENT_NO"].ToString());//(Endorsement No)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("CORRETOR<br>");
                HTML.Append(ds1.Tables[0].Rows[0]["AGENCY_CODE"].ToString());  //(Broker code)
                HTML.Append("</td></tr></table>");//End
                //HTML.Append("<table><tr></tr></table>");
                HTML.Append("<table align=center width=100%>");//Table Start
                HTML.Append("<col width=7%/>");
                HTML.Append("<col width=20% />");
                HTML.Append("<col width=15% />");
                HTML.Append("<col width=13% />");
                HTML.Append("<col width=12% />");
                HTML.Append("<col width=17% />");
                HTML.Append("<col width=15% />");
                HTML.Append("<tr valign=top>");
                HTML.Append("<td id=font1>");
                HTML.Append("ÍTEM");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["ITEM"].ToString());
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("Nº DO SINISTRO");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["OFFCIAL_CLAIM_NUMBER"].ToString());
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("DATA DO CHEQUE/CC");
                HTML.Append("<br />");
                HTML.Append("");
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("BANCO PAGADOR");
                HTML.Append("<br />");
                HTML.Append("001");
                //HTML.Append(ds1.Tables[0].Rows[0]["BANK_NUMBER"].ToString()); //(Bank Code)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("Nº DO CHEQUE/CC");
                HTML.Append("<br />");
                HTML.Append("");
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("% INDENIZAÇÃO ALIANÇA");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["COINSURANCE_PERCENT_LEADER"].ToString());  //(CO percentage for Aliança)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("% INDENIZAÇÃO COSS.");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["COINSURANCE_PERCENT"].ToString());
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr valign=top>");
                HTML.Append("<td colspan=6 id=font1>");
                HTML.Append("NOME DO SEGURADO OU ESTIPULANTE");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["FIRST_NAME"].ToString());  //(Policy holder name)
                HTML.Append(" ");
                HTML.Append(ds1.Tables[0].Rows[0]["MIDDLE_NAME"].ToString());
                HTML.Append(" ");
                HTML.Append(ds1.Tables[0].Rows[0]["LAST_NAME"].ToString());
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("CONTROLE INTERNO");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["PAYMENT_DATETIME"].ToString());
                HTML.Append("</td></tr></table>");//End
                HTML.Append("<table align=center width=100%>");//Table Start
                HTML.Append("<col width=85/>");
                HTML.Append("<col width=15/>");
                HTML.Append("<tr valign=top>");
                HTML.Append("<td id=font1>");
                HTML.Append("NOME DO FAVORECIDO/BENEFICIÁRIO");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["NAME"].ToString());  //(Beneficiary name)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("COBERTURA");
                if (ds1.Tables[0].Rows[0]["CLAIM_PAYMENT_TYPE"].ToString() == "INDENIZAÇÃO")
                {
                    HTML.Append("<br />");
                    HTML.Append(ds1.Tables[0].Rows[0]["COVERAGE_CLAIMED_CODE"].ToString());  //(Coverage claimed code)
                }
                else
                    HTML.Append("");
                HTML.Append("</td></tr></table>");//End
                HTML.Append(" <table align=center width=100%>");//Table Start
                HTML.Append("<tr valign=top>");
                HTML.Append("<td id=font1>");
                HTML.Append("CPF / CNPJ Nº");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["PARTY_CPF_CNPJ"].ToString());
                HTML.Append("<br>");
                HTML.Append("");
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("DOCUMENTO DE IDENTIFICAÇÃO");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["REGIONAL_IDENTIFICATION"].ToString());  //(Regional ID #)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("ORGAO EXPEDIDOR");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["REGIONAL_ID_ISSUANCE"].ToString());  //(Regional ID Issuance Org)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("DATA DE EXPEDIÇÃO");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["REGIONAL_ID_ISSUE_DATE"].ToString());
                //HTML.Append(str.ToShortDateString());  //(Regional ID Issuance Date)
                HTML.Append("</td></tr></table>");//End
                HTML.Append("<table align=center width=100%>");//Table Start
                HTML.Append("<tr valign=top>");
                HTML.Append("<td colspan=3 id=font1>");
                HTML.Append("RUA / AV. / LOGRADOURO");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["ADDRESS1"].ToString());
                HTML.Append(" ");
                HTML.Append(ds1.Tables[0].Rows[0]["ADDRESS2"].ToString()); //(Address)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("BAIRRO");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["DISTRICT"].ToString());
                HTML.Append("</td></tr></table>");//End
                HTML.Append("<table align=center width=100%>");//Table Start
                HTML.Append("<col width=20%/>");
                HTML.Append("<col width=8%/>");
                HTML.Append("<col width=10%/>");
                HTML.Append("<col width=12%/>");
                HTML.Append("<col width=15%/>");
                HTML.Append("<col width=35%/>");
                HTML.Append("<tr valign=top>");
                HTML.Append("<td id=font1>");
                HTML.Append("MUNICÍPIO");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["CITY"].ToString());  //(City)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("ESTADO");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["STATE_CODE"].ToString());  //(State)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("PAÍS");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["COUNTRY_NAME"].ToString());  //(Country)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("CEP");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["ZIP"].ToString());  //(Zip Code)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("TELEFONE");
                HTML.Append("<br />");
                HTML.Append(ds1.Tables[0].Rows[0]["CONTACT_PHONE"].ToString());  //(Phone #)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("ATIVIDADE PRINCIPAL (SOMENTE PARA PESSOA JURÍDICA)");
                HTML.Append("<br />");
                HTML.Append("");  //(Main Occupation – just for Organization)
                HTML.Append("</td></tr></table>");//End
                HTML.Append("<table align=center width=100%>");//Table Start
                HTML.Append("<tr valign=top>");
                HTML.Append("<td id=font1>");
                HTML.Append("NOME DO REPRESENTANTE");
                HTML.Append("<br />");
                HTML.Append("</td></tr></table>");//End
                HTML.Append("<table align=center width=100%>");//Table Start
                HTML.Append("<tr valign=top>");
                HTML.Append("<td id=font1>");
                HTML.Append("CPF / CNPJ Nº");
                HTML.Append("<br />");
                //HTML.Append(ds1.Tables[0].Rows[0]["PARTY_CPF_CNPJ"].ToString());
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("DOCUMENTO DE IDENTIFICAÇÃO");
                HTML.Append("<br />");
                //HTML.Append(ds1.Tables[0].Rows[0]["REGIONAL_IDENTIFICATION"].ToString());
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("ORGAO EXPEDIDOR");
                HTML.Append("<br />");
                //HTML.Append(ds1.Tables[0].Rows[0]["REGIONAL_ID_ISSUANCE"].ToString());  //(Regional ID Issuance Org)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("DATA DE EXPEDIÇÃO");
                HTML.Append("<br />");
                //HTML.Append(ds1.Tables[0].Rows[0]["REGIONAL_ID_ISSUE_DATE"].ToString());
                HTML.Append("</td></tr></table>");//End
                HTML.Append("<table align=center width=100%>");//Table Start
                HTML.Append("<tr valign=top>");
                HTML.Append("<td id=font1>");
                HTML.Append("RUA / AV. / LOGRADOURO");
                HTML.Append("<br />");
                //HTML.Append(ds1.Tables[0].Rows[0]["ADDRESS1"].ToString());
                //HTML.Append(ds1.Tables[0].Rows[0]["ADDRESS2"].ToString()); //(Address)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("BAIRRO");
                HTML.Append("<br />");
                //HTML.Append(ds1.Tables[0].Rows[0]["DISTRICT"].ToString());
                HTML.Append("</td></tr></table>");//End
                HTML.Append("<table align=center width=100%>");//Table Start
                HTML.Append("<col width=20%/>");
                HTML.Append("<col width=8%/>");
                HTML.Append("<col width=10%/>");
                HTML.Append("<col width=12%/>");
                HTML.Append(" <col width=15%/>");
                HTML.Append(" <col width=35%/>");
                HTML.Append("<tr valign=top>");
                HTML.Append("<td id=font1>");
                HTML.Append("MUNICÍPIO");
                HTML.Append("<br />");
                //HTML.Append(ds1.Tables[0].Rows[0]["CITY"].ToString());  //(City)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("ESTADO");
                HTML.Append("<br />");
                //HTML.Append(ds1.Tables[0].Rows[0]["STATE_CODE"].ToString());  //(State)
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("PAÍS");
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("CEP");
                HTML.Append("<br />");
                //HTML.Append(ds1.Tables[0].Rows[0]["ZIP"].ToString());  //(Zip Code)
                HTML.Append("</td>");
                //HTML.Append("</td>"); //Comment By Ashu on 27 June 2011 --formatting issue
                HTML.Append("<td id=font1>");
                HTML.Append("TELEFONE");
                HTML.Append("</td>");
                HTML.Append("<td id=font1>");
                HTML.Append("ATIVIDADE PRINCIPAL (SOMENTE PARA PESSOA JURÍDICA)");
                HTML.Append("</td></tr></table>");//End

                /*Add By Ashu on 27 June 2011 --formatting issue*/
                HTML.Append("<div class=divCodigo >"); /*Add By Ashu on 14 Jul 2011 --formatting issue*/
                HTML.Append("<table align=center width=100% height=100%>");
                HTML.Append("<tr>");
                HTML.Append(" <td>");
                /*Add By Ashu on 14 Jul 2011 --formatting issue*/
                HTML.Append("<table width=100% height=100% cellspacing=0 cellpadding=0>");
                HTML.Append("<tr>");
                HTML.Append(" <td valign=top>");
                /*Add By Ashu on 14 Jul 2011 --formatting issue End*/
                /*Add By Ashu on 27 June 2011 --formatting issue End*/
                HTML.Append("<table align=center id=font2 cellspacing=0 cellpadding=0 width=100% height=56%>");//Table Start
                HTML.Append("<col width=60%/>");
                HTML.Append("<col width=20%/>");
                HTML.Append("<col width=20%/>");
                HTML.Append("<tr valign=top>");
                if (ds1.Tables[2].Rows[0]["REIN_COMPANY_PERCENTAGE"].ToString() != "")
                {
                    HTML.Append("<td>");
                    HTML.Append("***");
                    HTML.Append("<b>");
                    HTML.Append("RESSEGURO: ");
                    HTML.Append("</b>");
                    HTML.Append(ds1.Tables[2].Rows[0]["REIN_COMPANY_PERCENTAGE"].ToString());
                    HTML.Append("%");//(Reinsurance percentage)
                    HTML.Append("</td>");
                }
                else
                {
                    HTML.Append("<td>");
                    HTML.Append("</td>");
                }
                HTML.Append("<td align=center>");
                HTML.Append("");//(Reinsurer claim #)
                HTML.Append("</td>");
                HTML.Append("<td align=right>R$ ");
                if (ds1.Tables[0].Rows[0]["CLAIM_PAYMENT_TYPE"].ToString() == "SALVADOS" || ds1.Tables[0].Rows[0]["CLAIM_PAYMENT_TYPE"].ToString() == "RESSARCIMENTO")
                {
                    HTML.Append(ds1.Tables[0].Rows[0]["RECOVERY_AMOUNT"].ToString());  //(Claim amount related to RI)
                }
                else
                HTML.Append(ds1.Tables[0].Rows[0]["PAYMENT_AMT"].ToString());  //(Claim amount related to RI)
                HTML.Append("</td></tr>");
                HTML.Append("<tr valign=top>");
                HTML.Append("<td colspan=3 id=font4>");
                //HTML.Append("RECEBI(EMOS) DA(E) COMPANHIA DE SEGUROS ALIANCA DA BAHIA A IMPORTÂNCIA DE (Claim amount related to RI in words)<br><br><br><br><br>");
                HTML.Append(ds1.Tables[0].Rows[0]["TEXT_DESCRIPTION"].ToString());
                HTML.Append("<br><br>");
                HTML.Append("</td>");
                HTML.Append("</tr>");

                //PAYMENT AMOUNT For “DESPESA(50017)” and “HONORÁRIOS(50018)” 
                //START
                if (ds1.Tables[0].Rows[0]["CLAIM_PAYMENT_TYPE"].ToString() == "DESPESA" || ds1.Tables[0].Rows[0]["CLAIM_PAYMENT_TYPE"].ToString() == "HONORÁRIO")
                {
                    HTML.Append("<tr valign=top>");
                    HTML.Append("<td>");
                    if (ds1.Tables.Count > 3 && ds1.Tables[3].Rows.Count > 0)
                    {

                        HTML.Append("<table align=center id = border width=100%>");
                        HTML.Append("<col width=20%/>");
                        HTML.Append("<tr>");
                        HTML.Append("<td>");
                        HTML.Append("<table align=center  width=100%>");
                        HTML.Append("<col width=40%/>");
                        HTML.Append("<col width=20%/>");
                        HTML.Append("<col width=40%/>");
                        HTML.Append("<tr>");
                        HTML.Append("<td id=font4>");
                        HTML.Append("<b>DESPESAS</b>");
                        HTML.Append("</td>");
                        HTML.Append("<td>");
                        HTML.Append("=");
                        HTML.Append("</td>");
                        HTML.Append("<td align=right id=font4>");
                        HTML.Append(ds1.Tables[3].Rows[0]["LOSS_EXPENCE"].ToString());
                        HTML.Append("</td>");
                        HTML.Append("</tr>");

                        HTML.Append("<tr>");
                        HTML.Append("<td id=font4>");
                        HTML.Append("<b>HONORARIOS</b>");
                        HTML.Append("</td>");
                        HTML.Append("<td>");
                        HTML.Append("=");
                        HTML.Append("</td>");
                        HTML.Append("<td align=right id=font4>");
                        HTML.Append(ds1.Tables[3].Rows[0]["PROFESSIONAL_SERVICES_LOSS_EXPENSE"].ToString());
                        HTML.Append("</td>");
                        HTML.Append("</tr>");
                        HTML.Append("</table>");
                    }
                    
                    HTML.Append("</td>");
                    HTML.Append("</tr>");
  
                    HTML.Append("</table>");
                }

                //END
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3>");
                HTML.Append("&nbsp");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr valign=top>");
                HTML.Append("<td colspan=3>");


                /*In case of policy with ceded coinsurance, when Transaction Type = FULL (means ALBA paid its own share and follower shar to be recovered), we should retrieve following columns with proper information:
                - CODIGO: means carrier SUSEP code. It has to retrieve ALBA record and follower records;
                - NOME: means carriers names. For each record.
                - %: means CO share %age of each record.
                - VALOR: means CO share amount calculated over this payment for each record.
                - TOTAL: it is not a column. It is a last row to sum up the columns '%' and 'VALOR'.*/
                if (dt != null && dt.Rows.Count > 1)
                {
                    HTML.Append("<table align=center border=1 cellspacing=0 width=100%>");
                    HTML.Append("<col width=20%/>");
                    HTML.Append("<col width=40%/>");
                    HTML.Append("<col width=15%/>");
                    HTML.Append("<col width=25%/>");
                    HTML.Append("<tr>");
                    HTML.Append("<td id=font1 colspan=4>");

                    HTML.Append("DEMONSTRATIVO DAS COTAS-PARTES DO COSSEGURO");
                    HTML.Append("</td>");
                    HTML.Append("</tr>");
                    HTML.Append("<tr>");
                    HTML.Append("<td valign=top id=font1>");
                    HTML.Append("CODIGO");
                    HTML.Append("</td>");
                    HTML.Append("<td valign=top id=font1>");
                    HTML.Append("NOME");
                    HTML.Append("</td>");
                    HTML.Append("<td valign=top id=font1>");
                    HTML.Append("%");
                    HTML.Append("</td>");
                    HTML.Append("<td valign=top id=font1>");
                    HTML.Append("VALOR");
                    HTML.Append("</td>");
                    // HTML.Append("</b>"); /*Comment By Ashu on 27 June 2011 --formatting issue*/
                    HTML.Append("</tr>");
                    HTML.Append(HTML1);
                    //HTML.Append("</td>");/*Comment By Ashu on 27 June 2011 --formatting issue*/
                    HTML.Append("<tr>");
                    HTML.Append("<td id=font1 colspan=2 align=right>Total------");
                    HTML.Append("</td>");
                    if (HTML1.ToString() != "")
                    {
                        HTML.Append("<td id=font1 align=right>");

                        if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["COINSURANCE_PERCENT"].ToString() != "")
                        {
                            HTML.Append(Convert.ToDouble(COINSURANCE_PERCENT_TOTAL).ToString("N", nfi));
                        }
                        //Convert.ToDouble().ToString("N", nfi);
                        //HTML.Append("Coinsurance_Percentage");
                        HTML.Append("</td>");
                        HTML.Append("<td id=font1 align=right>");
                        if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["PAYMENT_AMT"].ToString() != "")
                        {
                            HTML.Append(Convert.ToDouble(PAYMENT_AMT).ToString("N", nfi));
                        }


                        //HTML.Append("Payment_Amount");
                        HTML.Append("</td>");
                    }
                    //Comment on July 14th 2011 by Ashu formatting issue
                    //else
                    //{
                    //    if (ctr == 0)
                    //    {
                    //        HTML.Append("<br /><br /><br /><br />");
                    //    }
                    //    else if (ctr == 1)
                    //    {
                    //        HTML.Append("<br /><br /><br />");
                    //    }
                    //    else if (ctr == 2)
                    //    {
                    //        HTML.Append("<br /><br />");
                    //    }
                    //    else if (ctr == 3)
                    //    {
                    //        HTML.Append("<br />");
                    //    }
                    //    else if (ctr == 4)
                    //    {
                    //        HTML.Append("");
                    //    }
                        
                    //}
                    //Comment on July 14th 2011 by Ashu formatting issue end
                    HTML.Append("</tr>");
                    //HTML.Append("</tr>"); /*Comment By Ashu on 27 June 2011 --formatting issue*/

                    HTML.Append("</table>");
                }
                HTML.Append("</br>"); 
                /*Add By Ashu on 27 June 2011 --formatting issue*/
                HTML.Append("</td>");
                HTML.Append("</tr>");
                /*Add By Ashu on 27 June 2011 --formatting issue End*/
                
                //Only retrieve this information in case of payment done by bank transfer 
                //start
                if (ds1.Tables[0].Rows[0]["PAYMENT_METHOD"].ToString() == "14707")
                {
                    HTML.Append("<tr>");//shubh
                    HTML.Append("<td id = font4>");
                    HTML.Append("ESTE RECIBO FOI PAGO ATRAVES DE CREDITO BANCARIO");
                    HTML.Append("</td>");
                    HTML.Append("</tr>");
                    HTML.Append("<tr>");
                    HTML.Append("<td valign=top id=font4>");
                    HTML.Append("BANCO: ");
                    HTML.Append(ds1.Tables[0].Rows[0]["BANK_NUMBER"].ToString());
                    HTML.Append("&nbsp&nbsp&nbsp");
                    HTML.Append("AGENCIA: ");
                    HTML.Append(ds1.Tables[0].Rows[0]["BANK_BRANCH"].ToString());
                    HTML.Append("&nbsp&nbsp&nbsp");
                    HTML.Append("C/C N-: ");
                    string AccountNumber = ds1.Tables[0].Rows[0]["ACCOUNT_NUMBER"].ToString();
                    if (AccountNumber != null && AccountNumber!="")
                    {
                        string Account_number1 = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(AccountNumber);
                        HTML.Append(Account_number1);
                    }//Added by abhinav Agarwal
                    
                    HTML.Append("</td>");
                    HTML.Append("</tr>");
                }
                else
                {
                    HTML.Append("<tr>");
                    HTML.Append("<td>");
                    HTML.Append("</td>");
                    HTML.Append("</tr>");
                }//Only retrieve this information in case of payment done by bank transfer 
                //end

                /*Add By Ashu on 18 Jul 2011 --formatting issue*/
                HTML.Append("</table></td></tr>");
                HTML.Append("<tr><td valign=bottom>");
                HTML.Append(" <table id=font2 width=100% cellspacing=0 cellpadding=0 align=center>");
                //tr-td-table
                HTML.Append(" <col width=15% />");
                HTML.Append(" <col width=65% />");
                HTML.Append(" <col width=20% />");
                /*Add By Ashu on 18 Jul 2011 --formatting issue End*/
                HTML.Append("<tr valign=top>");
                HTML.Append("<td colspan=2>");
                //HTML.Append("<table>");
                //HTML.Append("<tr>");
                //HTML.Append("<td>");
                HTML.Append("<br><br><br><br><br>-------------------------------------------------------------------------------------------------------------");
                HTML.Append("</td>");
                HTML.Append("<td ALIGN=RIGHT>");
                HTML.Append("<br><br><br><br><br>-------------------------------------------------------------------------");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr valign=top>");
                HTML.Append("<td colspan=2 align=center>");
               /*Add By Ashu on 27 June 2011 --formatting issue*/
               HTML.Append("<table width=100% id=font4>");
               HTML.Append("<col width=35% />");/*change By Ashu on 18 Jul 2011 --formatting issue*/
               HTML.Append("<col width=60% />");
               HTML.Append(" <col width=5% />");/*change By Ashu on 18 Jul 2011 --formatting issue*/
               HTML.Append("<tr>");
               HTML.Append("<td>");
               HTML.Append("</td>");
               HTML.Append("<td align=left>");
               /*Add By Ashu on 27 June 2011 --formatting issue End*/
                HTML.Append("LOCAL / DATA");
                HTML.Append("</td>");
                /*Add By Ashu on 27 June 2011 --formatting issue*/ 
                HTML.Append("<td>");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("</table>");
                HTML.Append("</td>");
                /*Add By Ashu on 27 June 2011 --formatting issue End*/
                HTML.Append("<td align=center>");
                /*Add By Ashu on 18 Jul 2011 --formatting issue*/
                HTML.Append("<table width=100% id=font4>");
                HTML.Append("<col width=55% />");
                HTML.Append("<col width=60% />");
                HTML.Append("<col width=5% />");
                HTML.Append("<tr>");
                HTML.Append("<td>");
                HTML.Append("</td>");
                HTML.Append("<td align=left>");
                /*Add By Ashu on 18 Jul 2011 --formatting issue end*/
                HTML.Append("ASSINATURA DO FAVORECIDO");
                /*Add By Ashu on 18 Jul 2011 --formatting issue*/
                HTML.Append(" </td>");
                HTML.Append("<td>");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("</table>");
                /*Add By Ashu on 18 Jul 2011 --formatting issue End*/
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr valign=top>");
                HTML.Append("<td colspan=2>");
                HTML.Append("</td>");
                HTML.Append("<td ALIGN=RIGHT><br /><br /><br />-------------------------------------------------------------------------");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                /*Add By Ashu on 27 June 2011 --formatting issue */
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3>");
                HTML.Append("<table width=100% cellpadding=0 cellspacing=0>");
                HTML.Append(" <col width=40% />");
                             HTML.Append("<col width=40% />");
                             HTML.Append("<col width=20% />");
                /*Add By Ashu on 27 June 2011 --formatting issue End*/
                HTML.Append("<tr valign=top>");
                HTML.Append("<td colspan=2>");
                HTML.Append("<table id=border width=40% HEIGHT=13%>");
                HTML.Append("<tr valign=top>");
                HTML.Append("<td align=center>");
                HTML.Append("ESTA VIA VOLTA CARIMBADA E ASSINADA PELO FAVORECIDO");
                //HTML.Append("</td></tr></tr></table>");/*Comment By Ashu on 27 June 2011 --formatting issue */
                /*Add By Ashu on 27 June 2011 --formatting issue */
                HTML.Append("</td></tr>");
                HTML.Append("</table></td>");
                /*Add By Ashu on 27 June 2011 --formatting issue End*/
                //HTML.Append("</td>"); /*Comment By Ashu on 27 June 2011 --formatting issue*/
                //HTML.Append("<td align=center>");/*Comment By Ashu on 27 June 2011 --formatting issue*/
                HTML.Append("<td align=left id=font4>");/*Add By Ashu on 27 June 2011 --formatting issue*/
                /*Add By Ashu on 18 Jul 2011 --formatting issue*/
                HTML.Append("<table width=100% id=font4>");
                HTML.Append(" <col width=55% />");
                HTML.Append("<col width=60% />");
                HTML.Append("<col width=5% />");
                HTML.Append("<tr>");
                HTML.Append("<td>");
                HTML.Append("</td>");
                HTML.Append("<td align=left>");
                /*commented By Ashu on 18 Jul 2011 --formatting issue End*/
                HTML.Append("CNPJ / CPF &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                HTML.Append("<br /><br /><br /><br /><br /><br /><br />");
                /*Add By Ashu on 18 Jul 2011 --formatting issue*/
                HTML.Append("</td>");
                HTML.Append("<td>");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("</table>");
                HTML.Append("</td>");
                /*Add By Ashu on 18 Jul 2011 --formatting issue End*/
                HTML.Append("</tr>");
               
                HTML.Append("</table>");//End
                /*Add By Ashu on 27 June 2011 --formatting issue*/
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("</table>");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("</table>");
                HTML.Append("</td></tr></table>");
                HTML.Append("</div>");
                /*Add By Ashu on 27 June 2011 --formatting issue end*/
                HTML.Append("<table width=100%>");
                HTML.Append("<tr>");
                HTML.Append("<td align=right id=font4>");
                HTML.Append("1º VIA MATRIZ - CONTABILIDADE");
                HTML.Append("</td>");
                HTML.Append("</tr>");
                HTML.Append("</table>");
                //HTML.Append("</form>");  /*Comment By Ashu on 27 June 2011 --formatting issue */
                //HTML.Append("</body>");   /*Comment By Ashu on 27 June 2011 --formatting issue */
                //HTML.Append("</table>");//End /*Comment By Ashu on 27 June 2011 --formatting issue */
                HTML.Append("</div>");
                HTML.Append("</body>");
                HTML.Append("</html>");
                string str = HTML.ToString();
                return str;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // THIS CODE GENERATE CLAIM RECEIPT CORRESPONDING TO EACH PAYEE ID
        public int generateProductClaimReceipt(int ClaimID, int ActivityID, int CustomerID, int PolicyID, int PolicyVersionID, int userID, string ToRegenerateDoc)
        {
            try
            {
                int RetValue = 0;
                DataSet ds = GetClaimPayeeList(ClaimID, ActivityID);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    //--------------------------------------------------------------------
                    // INSERT RECORD IN PRINT JOBS
                    //--------------------------------------------------------------------                        
                    string AgencyCode = "", CarrierCode = "", fileName = "";
                    int ProcessId = 0, ProcessRowId = 0;
                    Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo;
                    DataSet dsBRCO = getBrokerCoInsurer(CustomerID, PolicyID, PolicyVersionID);
                    CarrierCode = dsBRCO.Tables[3].Rows[0]["CARRIER_CODE"].ToString();
                    AgencyCode = dsBRCO.Tables[3].Rows[0]["AGENCY_CODE"].ToString();
                    objPrintJobsInfo = GetPrintJobsValues(CustomerID, PolicyID, PolicyVersionID, ProcessId, ProcessRowId, userID, "CLAIM", AgencyCode, CarrierCode);
                    objPrintJobsInfo.DOCUMENT_CODE = "CLM_RECEIPT";
                    objPrintJobsInfo.CLAIM_ID = ClaimID;
                    objPrintJobsInfo.ACTIVITY_ID = ActivityID;
                    //-----------------------------------------------------------------------

                    DataTable dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int PayeeID = int.Parse(dt.Rows[i]["PAYEE_ID"].ToString());
                        string strDoc_Text = GenerateClaimReceiptHTML(ClaimID, ActivityID, PayeeID);

                        if (strDoc_Text != "")
                            RetValue = this.SaveClaimDocument(ClaimID, ActivityID, PayeeID, strDoc_Text, "CLM_RECEIPT", ToRegenerateDoc);

                        if (RetValue > 0 && ToRegenerateDoc!="Y")
                        {





                            objPrintJobsInfo.ENTITY_ID = PayeeID;


                            // ===========================================================
                            // ADDED BY SANTOSH KUMAR GAUTAM ON 29 APR 2011 
                            // MAKE FOUR ENTRY FOR EACH PAYEE
                            // 1. CLAIM HEAD OFFICE
                            // 1. CLAIM DEPARTMENT
                            // 1. CLAIM BENEFICIARY
                            // 1. CLAIM REINSURER (IF CLAIM HAS ANY REINSURANCE PARTY)
                            // =============================================================

                            // ENTREY FOR CLAIM HEAD OFFICE
                            fileName = "CLM_HO" + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + PayeeID.ToString();
                            objPrintJobsInfo.ENTITY_TYPE = "CLM_HO";
                            objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                            AddPrintJobs(objPrintJobsInfo);

                            // ENTREY FOR CLAIM DEPARTMENT
                            fileName = "CLM_DEPT" + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + PayeeID.ToString();
                            objPrintJobsInfo.ENTITY_TYPE = "CLM_DEPT";
                            objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                            AddPrintJobs(objPrintJobsInfo);

                            // ENTREY FOR BENEFICIARY
                            fileName = "CLM_BENF" + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + PayeeID.ToString();
                            objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                            objPrintJobsInfo.ENTITY_TYPE = dt.Rows[i]["PARTY_TYPE"].ToString();
                            AddPrintJobs(objPrintJobsInfo);

                            // CLAIM REINSURER (IF CLAIM HAS ANY REINSURANCE PARTY)                          
                            if (ds.Tables.Count > 1)
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    fileName = "CLM_REINS" + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + PayeeID.ToString();
                                    objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                                    objPrintJobsInfo.ENTITY_TYPE = "CLM_REINS";
                                    AddPrintJobs(objPrintJobsInfo);
                                }
                            }
                            string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                            this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1623", "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Claim receipt generated successfully."
                        }

                    }
                }
                return RetValue;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // TO SAVE EACH CLAIM RECEIPT CORRESPONDING TO EACH PAYEE ID INTO DATABASE
        private int SaveClaimDocument(int ClaimId, int ActivityId, int PayeeId, String strPolicyDocTEXT, string ProcessType, string ToRegenerateDoc)
        {
            int retVal = 0;
            try
            {
                if (ToRegenerateDoc != "Y") // INSERT NEW RECORDS 
                {
                    objWrapper.ClearParameteres();
                    objWrapper.TransactionRequired = DataWrapper.MaintainTransaction.NO;
                    objWrapper.AddParameter("@CLAIM_ID", ClaimId);
                    objWrapper.AddParameter("@ACTIVITY_ID", ActivityId);
                    objWrapper.AddParameter("@PAYEE_ID", PayeeId);
                    objWrapper.AddParameter("@DOC_TEXT", strPolicyDocTEXT);
                    objWrapper.AddParameter("@PROCESS_TYPE", ProcessType);
                    retVal = objWrapper.ExecuteNonQuery("Proc_SaveClaimDocument");
                    objWrapper.ClearParameteres();
                }
                else // UPDATE EXISTING RECORDS
                {
                    objWrapper.ClearParameteres();
                    objWrapper.TransactionRequired = DataWrapper.MaintainTransaction.NO;
                    objWrapper.AddParameter("@CLAIM_ID", ClaimId);
                    objWrapper.AddParameter("@ACTIVITY_ID", ActivityId);
                    objWrapper.AddParameter("@PAYEE_ID", PayeeId);
                    objWrapper.AddParameter("@DOC_TEXT", strPolicyDocTEXT);
                    objWrapper.AddParameter("@PROCESS_TYPE", ProcessType);
                    retVal = objWrapper.ExecuteNonQuery("Proc_UpdateClaimReceiptDocument");
                }
            }

            catch (Exception ex)
            {
                throw (ex);
            }
            return retVal;
        }

        #endregion CLAIM_RECEIPT





        //REGION FOR REMIND LETTER

        #region REMIND LETTER





        public DataSet fetch_ProductClaimRemindLetter(int ClaimID, int ActivityID, int PayeeID)
        {
            try
            {

                objWrapper.ClearParameteres();
                objWrapper.TransactionRequired = DataWrapper.MaintainTransaction.NO;
                objWrapper.AddParameter("@CLAIM_ID", ClaimID);
                objWrapper.AddParameter("@ACTIVITY_ID", ActivityID);
                objWrapper.AddParameter("@PAYEE_ID", PayeeID);
                DataSet ds = objWrapper.ExecuteDataSet("Proc_GetProductClaimRecipt");
                objWrapper.ClearParameteres();
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //THIS CODE IS USED FOR CREATE HTML OF REMIND LETTER AND SAVE IT IN DATABASE

        private string GenerateClaimRemindLetterHTML(int ClaimID, int ActivityID, int PayeeID)
        {
            try
            {
                DataSet ds = fetch_ProductClaimRemindLetter(ClaimID, ActivityID, PayeeID);
                if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                    return "";

                StringBuilder HTML = new StringBuilder();  // Creating an object of StringBuilder
                HTML.Append("<html>");
                HTML.Append("<head>");// head
                HTML.Append("<style type=text/css>");//css
                HTML.Append("#font{font-size:18px;font-family:Courier;}");
                HTML.Append("#font1{font-size:22px;font-family:Arial;}");
                HTML.Append(".style1{width: 15%;}");
                HTML.Append(".style2{width: 17%;}");
                HTML.Append(".style3{width: 14%;}");
                HTML.Append("</style>");
                HTML.Append(" </head>");
                HTML.Append("<body>");
                HTML.Append("<table width=100% align=center>");
                HTML.Append("<col width=5%/>");
                HTML.Append("<col width=40%/>");
                HTML.Append("<col width=55%>");
                HTML.Append("<tr><td colspan=3  id=font>COMPANHIA DE SEGUROS ALIANÇA DA BAHIA</td></tr>");
                HTML.Append("<tr><td colspan=3 id=font>À Cosseguradora</td></tr>");
                HTML.Append(" <tr><td colspan=3 id=font>566.5 - Cia. VERA CRUZ VIDA E PREVIDENCIA S/A&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Sinistro Lider 00215765</td></tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>Ref.</td>");
                HTML.Append("<td align=left id=font>( )&nbsp&nbsp Aviso de Sinistro</td>");
                HTML.Append("<td align=left id=font>( )&nbsp&nbsp Despesas/Honorários</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td></td>");
                HTML.Append("<td id=font>(x)&nbsp&nbsp Cobrança de Sinistro</td>");
                HTML.Append("<td id=font>( )&nbsp&nbsp Ressarc.Salvados</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td></td>");
                HTML.Append("<td id=font>( )&nbsp&nbsp Pagto.Parcial/Adiantamento</td>");
                HTML.Append("<td id=font>( )&nbsp&nbsp Encerra.sem Indenização/Cancel.(-)</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td></td>");
                HTML.Append("<td id=font>(x)&nbsp&nbsp Pagamento Final </td>");
                HTML.Append("<td id=font>( )&nbsp&nbsp Reabertura de Sinistro</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3 id=font>------------------------------------------------------------------------------------------------------</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3 id=font>DADOS GERAIS</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3 id=font>------------------------------------------------------------------------------------------------------</td>");
                HTML.Append("</tr>");
                HTML.Append("</table>");
                HTML.Append("<table width=100% align=center>");
                HTML.Append("<col width=36%/>");
                HTML.Append("<col width=36%/>");
                HTML.Append("<col width=28%/>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>Nome do Segurado</td>");
                HTML.Append("<td id=font align=center>!&nbsp&nbsp&nbspNome Ramo</td>");
                HTML.Append("<td id=font align=center>!%Part.Congênere</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>ILDO PEREIRA GOMES</td>");
                HTML.Append("<td id=font align=center>&nbsp!Vida Em Grupo</td>");
                HTML.Append("<td id=font align=center> ! &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 50,0000</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=3 id=font>------------------------------------------------------------------------------------------------------</td>");
                HTML.Append("</tr>");
                HTML.Append(" </table>");
                HTML.Append("<table align=center width=100%>");
                HTML.Append("<col width=4%/>");
                HTML.Append("<col width=16%/>");
                HTML.Append("<col width=11%/>");
                HTML.Append("<col width=5%/>");
                HTML.Append("<col width=64%/>");
                HTML.Append("<tr>");
                HTML.Append(" <td id=font>N.Apólice</td>");
                HTML.Append("<td align=left id=font>!N.Ordem Apólice</td>");
                HTML.Append("<td align=left id=font>!N.Endosso</td>");
                HTML.Append("<td align=left id=font>!N.Ordem Endosso!</td>");
                HTML.Append("<td id=font align=left>Vigência</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>09300000505</td>");
                HTML.Append("<td id=font>!</td>");
                HTML.Append("<td align=left id=font>!2.0000063.8!</td>");
                HTML.Append("<td align=l;eft id=font>!&nbsp;&nbsp;&nbsp; 63&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; !</td>");
                HTML.Append("<td id=font align=left>01/01/2002 A 31/01/2002</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=5 id=font>------------------------------------------------------------------------------------------------------</td>");
                HTML.Append("</tr>");
                HTML.Append("</table>");
                HTML.Append("<table width=100% align=center>");
                HTML.Append("<col width=4%>");
                HTML.Append("<col width=10%/>");
                HTML.Append("<col width=10%/>");
                HTML.Append("<col width=25%/>");
                HTML.Append("<col width=41%/>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>Moeda</td>");
                HTML.Append("<td id=font align=left>!N.Item</td>");
                HTML.Append("<td align=left id=font>!</td>");
                HTML.Append("<td align=center id=font>Imp.Segurada!Sinistro</td>");
                HTML.Append("<td id=font>IRB!Dt.Sinistro!Dt.Aviso</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>R$ </td>");
                HTML.Append("<td id=font align=left>!100030084000000005340</td>");
                HTML.Append("<td align=left id=font>!</td>");
                HTML.Append("<td align=center id=font>36.450!</td>");
                HTML.Append("<td id=font>0&nbsp&nbsp!&nbsp 25/01/2002!01062002</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=5 id=font>------------------------------------------------------------------------------------------------------</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=5 id=font>DADOS ESPECIFICOS VG/APC</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=5 id=font>------------------------------------------------------------------------------------------------------</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=5 id=font>Nome do Estipulante&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp!Garantia Reclamada do Segurado Sinistrado</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=5 id=font>BEG CLUBE SEGURO DE VIDA - MEN!MA - ACIDENTE DE TRANSITO</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=5 id=font>------------------------------------------------------------------------------------------------------</td>");
                HTML.Append("</tr>");
                HTML.Append("</table>");
                HTML.Append("<table width=100% align=center>");
                HTML.Append("<col width=40%/>");
                HTML.Append("<col width=10%/>");
                HTML.Append("<col width=50%/>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>Nome do Segurado Principal</td>");
                HTML.Append("<td id=font align=right>!Dt.Nascimento</td>");
                HTML.Append("<td></td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>ILDO PEREIRA GOMES </td>");
                HTML.Append("<td id=font align=right>!&nbsp&nbsp 02/11/1968 </td>");
                HTML.Append("<td></td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=2 id=font>------------------------------------------------------------------------------------------------------</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>Nome do Segurado Sinistrado</td>");
                HTML.Append("<td id=font align=right>!Dt.Nascimento</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>ILDO PEREIRA GOMES </td>");
                HTML.Append("<td id=font align=right>!&nbsp&nbsp 02/11/1968 </td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td colspan=2 id=font>------------------------------------------------------------------------------------------------------</td>");
                HTML.Append("</tr>");
                HTML.Append("</table>");
                HTML.Append("<table width=100% align=center>");
                HTML.Append("<col width=10%/>");
                HTML.Append("<col width=22%/>");
                HTML.Append("<col width=20%/>");
                HTML.Append("<col width=10%/>");
                HTML.Append("<col width=10%/>");
                HTML.Append("<col width=23%/>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>Participações!</td>");
                HTML.Append("<td id=font align=right>Vl.TotalLider!</td>");
                HTML.Append("<td id=font>Part.Cosseg(R$)!</td>");
                HTML.Append("<td id=font>Part.Qt.Moeda&nbsp;&nbsp;&nbsp; !</td>");
                HTML.Append("<td id=font>Dt.Base&nbsp&nbsp;&nbsp;&nbsp;&nbsp; !</td>");
                HTML.Append("<td id=font>Fat.Index.</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td>--------------------!</td>");
                HTML.Append("<td>-----------------------------!</td>");
                HTML.Append("<td>--------------------------!</td>");
                HTML.Append("<td>--------------------------!</td>");
                HTML.Append("<td >--------------------!</td>");
                HTML.Append("<td>-----------------------</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>Estimativa&nbsp;&nbsp; !</td>");
                HTML.Append("<td align=right id=font>36.605&nbsp!</td>");
                HTML.Append("<td align=right id=font>18.302&nbsp!</td>");
                HTML.Append("<td align=right id=font>8.012,01&nbsp!</td>");
                HTML.Append("<td align=right id=font>1062002&nbsp!</td>");
                HTML.Append("<td align=left id=font>2,28438</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>Indenização&nbsp&nbsp!</td>");
                HTML.Append("<td align=right id=font>52.258,95&nbsp!</td>");
                HTML.Append("<td align=right id=font>26.129&nbsp!</td>");
                HTML.Append("<td align=right id=font>9.636,77&nbsp!</td>");
                HTML.Append("<td align=right id=font>30092010&nbsp!</td>");
                HTML.Append("<td align=left id=font>2,71143</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>Honorários&nbsp&nbsp&nbsp!</td>");
                HTML.Append("<td align=right id=font>0,00&nbsp!</td>");
                HTML.Append("<td align=right id=font>0&nbsp!</td>");
                HTML.Append("<td align=right id=font>0,00&nbsp!</td>");
                HTML.Append("<td align=right id=font>&nbsp&nbsp&nbsp&nbsp&nbsp!</td>");
                HTML.Append("<td align=left id=font></td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>Despesas&nbsp&nbsp&nbsp&nbsp&nbsp!</td>");
                HTML.Append("<td align=right id=font>0,00&nbsp!</td>");
                HTML.Append("<td align=right id=font>0&nbsp!</td>");
                HTML.Append("<td align=right id=font>0,00&nbsp!</td>");
                HTML.Append("<td align=right id=font>&nbsp&nbsp&nbsp&nbsp&nbsp!</td>");
                HTML.Append("<td align=left  id=font></td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>Ressarcimento!</td>");
                HTML.Append("<td align=right id=font>0,00&nbsp!</td>");
                HTML.Append("<td align=right id=font>0&nbsp!</td>");
                HTML.Append("<td align=right id=font>0,00&nbsp!</td>");
                HTML.Append("<td align=right id=font>&nbsp&nbsp&nbsp&nbsp&nbsp!</td>");
                HTML.Append("<td align=left id=font></td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>Salvados &nbsp;&nbsp;&nbsp;&nbsp;!</td>");
                HTML.Append("<td align=right id=font>0,00&nbsp!</td>");
                HTML.Append("<td align=right id=font>0&nbsp!</td>");
                HTML.Append("<td align=right id=font>0,00&nbsp!</td>");
                HTML.Append("<td align=right id=font>&nbsp&nbsp&nbsp&nbsp&nbsp!</td>");
                HTML.Append("<td align=left id=font></td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>------------ !</td>");
                HTML.Append("<td id=font>-------------------!</td>");
                HTML.Append("<td id=font>-----------------!</td>");
                HTML.Append("<td id=font>---------------- !</td>");
                HTML.Append("<td id=font>-------------!</td>");
                HTML.Append("<td id=font>---------------</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>Total Geral&nbsp&nbsp!</td>");
                HTML.Append("<td align=right id=font>52.258,95!</td>");
                HTML.Append("<td align=right id=font>26.129!</td>");
                HTML.Append("<td align=right id=font >9.636,77&nbsp!</td>");
                HTML.Append("<td align=left colspan=2 id=font>F.A.J.</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td id=font>-------------!</td>");
                HTML.Append("<td id=font>-------------------!</td>");
                HTML.Append("<td id=font>-----------------!</td>");
                HTML.Append("<td id=font>---------------- !</td>");
                HTML.Append("<td id=font>--------------</td>");
                HTML.Append("<td id=font>---------------</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr><td colspan=6 id=font>(x)Coloca a nossa disposição quantia referente a sua participação até 09/11/2010</td></tr>");
                HTML.Append("<tr><td colspan=6 id=font>( )Sinistro Sorteio já liquidado.A cota parte dessa congênere será debitada</td></tr>");
                HTML.Append("<tr><td colspan=6 id=font>&nbsp&nbsp através do L.C.C.(Lancto.Conta Corrente).<br><br></td></tr>");
                HTML.Append("<tr><td colspan=6 id=font>Observação: C.S. 22802053 N.Carta Aviso 056761 N.Carta Cobrança 058001 O.E. 22<br><br></td></tr>");
                HTML.Append("<tr><td colspan=6 id=font>SALVADOR&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 09 de NOVEMBRO de 2010</td></tr>");
                HTML.Append("<tr><td colspan=6 id=font>----------------------------------------------&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp -------------------------------------------</td></tr>");
                HTML.Append("<tr><td colspan=6 id=font>Local e Data &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Assinatura</td></tr>");
                HTML.Append("</table>");
                HTML.Append("</body>");
                HTML.Append("</html>");
                string str = HTML.ToString();
                return str;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public int generateProductClaimRemindLetter(int ClaimID, int ActivityID, int CustomerID, int PolicyID, int PolicyVersionID, int userID)
        {
            try
            {
                int RetValue = 0;
                DataSet ds = GetClaimPayeeList(ClaimID, ActivityID);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    //--------------------------------------------------------------------
                    // INSERT RECORD IN PRINT JOBS
                    //--------------------------------------------------------------------                        
                    string AgencyCode = "", CarrierCode = "", fileName = "";
                    int ProcessId = 0, ProcessRowId = 0;
                    Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo;
                    DataSet dsBRCO = getBrokerCoInsurer(CustomerID, PolicyID, PolicyVersionID);
                    CarrierCode = dsBRCO.Tables[3].Rows[0]["CARRIER_CODE"].ToString();
                    AgencyCode = dsBRCO.Tables[3].Rows[0]["AGENCY_CODE"].ToString();
                    objPrintJobsInfo = GetPrintJobsValues(CustomerID, PolicyID, PolicyVersionID, ProcessId, ProcessRowId, userID, "CLAIM", AgencyCode, CarrierCode);
                    objPrintJobsInfo.DOCUMENT_CODE = "CLM_REMIND";
                    objPrintJobsInfo.CLAIM_ID = ClaimID;
                    objPrintJobsInfo.ACTIVITY_ID = ActivityID;
                    //-----------------------------------------------------------------------

                    DataTable dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int PayeeID = int.Parse(dt.Rows[i]["PAYEE_ID"].ToString());
                        string strDoc_Text = GenerateClaimReceiptHTML(ClaimID, ActivityID, PayeeID);

                        if (strDoc_Text != "")
                            RetValue = this.SaveClaimDocument(ClaimID, ActivityID, PayeeID, strDoc_Text, "CLM_REMIND", "N");

                        if (RetValue > 0)
                        {

                            fileName = "CLM_REMIND" + "_" + CustomerID.ToString() + "_" + PolicyID.ToString() + "_" + PolicyVersionID.ToString() + "_" + PayeeID.ToString();

                            objPrintJobsInfo.ENTITY_TYPE = dt.Rows[i]["PARTY_TYPE"].ToString();

                            objPrintJobsInfo.ENTITY_ID = PayeeID;
                            objPrintJobsInfo.FILE_NAME = fileName + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
                            AddPrintJobs(objPrintJobsInfo);

                            string PDFlink = objPrintJobsInfo.FILE_NAME + "<COMMON_PDF_URL=window.open(\"" + objPrintJobsInfo.URL_PATH + "/" + objPrintJobsInfo.FILE_NAME + "\")>";
                            this.WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1624", "") + "<BR>" + objPrintJobsInfo.FILE_NAME, userID, PDFlink, "", 0);//"Claim remind letter generated successfully."
                        }

                    }
                }
                return RetValue;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        #endregion REMIND LETTER





        private string GenerateDocumentdataXML(DataSet ds)
        {
            string PdfDataxml = "";
            string tableXML = "";
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                strBuilder.Append("<POLICY_DOCUMENTS>");
                if (ds != null)
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        tableXML = "";
                        foreach (DataRow dr in dt.Rows)
                        {
                            tableXML += dr.ItemArray[0].ToString();

                        }
                        strBuilder.Append(tableXML);
                    }
                }
                strBuilder.Append("</POLICY_DOCUMENTS>");
            }
            catch (Exception ex) { throw new Exception("No data Found to generate pdf XML : " + ex.Message); }
            PdfDataxml = strBuilder.ToString();
            return PdfDataxml;
        }
        #endregion

        private DataSet getBrokerCoInsurer(int CustomerID, int PolicyId, int PolVersionId)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);
                DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetPolicyBrokerCoInsurer");
                objWrapper.ClearParameteres();
                return dsTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private void SavePolicyDocumentXml(int CustomerID, int PolicyId, int PolVersionId, String strPolicyDocXML, string doctype)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);
                objWrapper.AddParameter("@DOC_XML", strPolicyDocXML);
                objWrapper.AddParameter("@DOC_TYPE", doctype);
                int retVal = objWrapper.ExecuteNonQuery("Proc_SavePolicyDocumentXML");
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public String GetXmlString(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetPolicyDocumentXML");
                objWrapper.ClearParameteres();
                if (dsTemp.Tables[0].Rows.Count > 0)
                { return dsTemp.Tables[0].Rows[0]["DEC_CUSTOMERXML"].ToString(); }
                else
                { return ""; }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #region private functions
        private string getAttributeValue(System.Xml.XmlNode node, string strAttName)
        {
            foreach (XmlAttribute attri in node.Attributes)
            {
                if (attri.Name.ToUpper().Trim() == strAttName.ToUpper().Trim())
                {
                    return attri.Value;
                }
            }
            return "";
        }
        private string getNodeValue(System.Xml.XmlNode node, string strNodeName)
        {
            XmlNode xmlnode = node.SelectSingleNode(strNodeName);
            if (xmlnode != null)
                return xmlnode.InnerText;
            else
                return "";
        }
        public string RemoveJunkXmlCharacters(string strNodeContent)
        {
            strNodeContent = strNodeContent.Replace("&", "&amp;");
            strNodeContent = strNodeContent.Replace("<", "&lt;");
            strNodeContent = strNodeContent.Replace(">", "&gt;");
            strNodeContent = strNodeContent.Replace("'", "&apos;");
            strNodeContent = strNodeContent.Replace("\"", "&quot;");
            return strNodeContent;
        }
        #endregion
        #region write Transaction Log
        private void WriteTransactionLog(int CustomerID, int PolicyID, int PolicyVersionID, string TransactionDescription, int RecordedBy, string ProcessDesc, string TransactionChange, int TranTypeId)
        {
            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
            // 11 is taken as it's 'LOOKUP_VALUE_DESC' is null in table 'MNT_LOOKUP_VALUES'. This is need when value change from null to some value.
            TransactionChange = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(TransactionChange, "LabelFieldMapping/Map[@field='REQUESTED_BY' and @OldValue='0']", "OldValue", "11");
            objTransactionInfo.CUSTOM_INFO = ProcessDesc;
            if (TranTypeId != 0)
                objTransactionInfo.TRANS_TYPE_ID = TranTypeId;
            else
                objTransactionInfo.TRANS_TYPE_ID = 3;
            objTransactionInfo.CLIENT_ID = CustomerID;
            objTransactionInfo.POLICY_ID = PolicyID;
            objTransactionInfo.POLICY_VER_TRACKING_ID = PolicyVersionID;
            if (IsEODProcess)
                objTransactionInfo.RECORDED_BY = EODUserID;
            else
                objTransactionInfo.RECORDED_BY = RecordedBy;
            objTransactionInfo.TRANS_DESC = TransactionDescription;
            objTransactionInfo.CHANGE_XML = TransactionChange;
            objWrapper.ExecuteNonQuery(objTransactionInfo);
            objWrapper.ClearParameteres();
        }
        #endregion

        private DataSet GetGeneratedXML(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetPolicyDocumentXML");
                objWrapper.ClearParameteres();
                return dsTemp;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public String Saveinfinal
        {
            get { return _Saveinfinal; }
            set { _Saveinfinal = value; }
        }

        public String GeneratePolicyDocumentpdfFromXML(int CustomerID, int PolicyID, int PolicyVersionID, string AgencyCode)
        {
            String Pdfxml;
            DataSet oDS = GetGeneratedXML(CustomerID, PolicyID, PolicyVersionID);
            Pdfxml = oDS.Tables[0].Rows[0]["DEC_CUSTOMERXML"].ToString();
            //EbixPDF.ClsPolicyPDF objPolPdf = new EbixPDF.ClsPolicyPDF();
            String GenratedfileName = "";
            /*
            objPolPdf.AgencyCode = AgencyCode;
            objPolPdf.CustomerID = CustomerID;
            objPolPdf.PolicyID = PolicyID;
            objPolPdf.PolicyVersionID = PolicyVersionID;
            objPolPdf.FinalPathpdf = Saveinfinal;
            objPolPdf.CarrierSystemID = CarrierSystemID;
            String Path = System.Configuration.ConfigurationSettings.AppSettings["UploadURL"].ToString();
            objPolPdf.pdfPath = System.Web.HttpContext.Current.Server.MapPath(Path);
            objPolPdf.ImpersonationUserId  = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName").ToString().Trim();
            objPolPdf.ImpersonationPassword = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd").ToString().Trim();
            objPolPdf.ImpersonationDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain").ToString().Trim();

            GenratedfileName = objPolPdf.GeneratePolicyDocumentpdfs(Pdfxml);
            */
            if (GenratedfileName != "")
            {
                return GenratedfileName;
            }
            else
            {
                return "";
            }

        }
        public string GetPolicyAgencyCode(int CustomerID, int PolId, int PolVersionId, string strCalledFrom)
        {
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objWrapper.AddParameter("@AppPol_Id", PolId);
            objWrapper.AddParameter("@AppPolVersion_Id", PolVersionId);
            objWrapper.AddParameter("@CalledFrom", strCalledFrom);

            DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetPDFAgencyCode");
            objWrapper.ClearParameteres();
            if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0 && dsTemp.Tables[0].Rows[0]["AGENCY_CODE"] != null && dsTemp.Tables[0].Rows[0]["AGENCY_CODE"].ToString() != "")
                return dsTemp.Tables[0].Rows[0]["AGENCY_CODE"].ToString();
            else
                return "";
        }

        #region "Policy Premium Notice pdf"

        private DataSet fetchPolicyPremiumDataforPdfXml(int CustomerID, int PolicyId, int PolVersionId)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);
                objWrapper.AddParameter("@CALLEDFROM", null);
                objWrapper.AddParameter("@POLICYCURRENCYSYMBOL", Cms.BusinessLayer.BlCommon.ClsCommon.GetPolicyCurrencySymbol(""));
                objWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);
                DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetPolicyPremiumDataforPdf");
                objWrapper.ClearParameteres();
                return dsTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private string GenerateXMLDocumentdata(DataSet ds)
        {
            string PdfDataxml = "";
            string tableXML = "";
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                strBuilder.Append("<PREMIUM_NOTICE_DOCUMENTS>");
                if (ds != null)
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        tableXML = "";
                        foreach (DataRow dr in dt.Rows)
                        {
                            tableXML += dr.ItemArray[0].ToString();

                        }
                        strBuilder.Append(tableXML);
                    }
                }
                strBuilder.Append("</PREMIUM_NOTICE_DOCUMENTS>");
            }
            catch (Exception ex) { throw new Exception("No data Found to generate pdf XML : " + ex.Message); }
            PdfDataxml = strBuilder.ToString();
            return PdfDataxml;
        }
        public string GeneratePremiumNoticeXMLAndSave(int CustomerID, int PolicyID, int PolicyVersionID, string CalledFor, int userID)
        {
            // fetching premium Notice data
            DataSet dsPolicy = fetchPolicyPremiumDataforPdfXml(CustomerID, PolicyID, PolicyVersionID);
            //generating XML from premium Notice data for PDF
            String strPremNoticePdfXml = GenerateXMLDocumentdata(dsPolicy);

            //save XML In DB
            if (strPremNoticePdfXml != "" && strPremNoticePdfXml != "<PREMIUM_NOTICE_DOCUMENTS></PREMIUM_NOTICE_DOCUMENTS>")
                this.SavePolicyDocumentXml(CustomerID, PolicyID, PolicyVersionID, strPremNoticePdfXml, "PREM_NOTICE");
            string fileName = "";
            fileName = GeneratePolicyPremNoticepdfFromXML(CustomerID, PolicyID, PolicyVersionID, strPremNoticePdfXml);
            return fileName;

        }
        public String GeneratePolicyPremNoticepdfFromXML(int CustomerID, int PolicyID, int PolicyVersionID, String PremNoticeXML)
        {

            String GenratedfileName = "";
            /*
            EbixPDF.ClsPolicyPDF objPolPdf = new EbixPDF.ClsPolicyPDF();
            objPolPdf.AgencyCode = ClsCommon.FetchValueFromXML("AGENCY_CODE", PremNoticeXML);
            objPolPdf.CustomerID = CustomerID;
            objPolPdf.PolicyID = PolicyID;
            objPolPdf.PolicyVersionID = PolicyVersionID;
            // objPolPdf.FinalPathpdf = Saveinfinal;
            objPolPdf.CarrierSystemID = CarrierSystemID;
            String Path = System.Configuration.ConfigurationSettings.AppSettings["UploadURL"].ToString();
            objPolPdf.pdfPath = System.Web.HttpContext.Current.Server.MapPath(Path);

            objPolPdf.ImpersonationUserId = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName").ToString().Trim();
            objPolPdf.ImpersonationPassword = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd").ToString().Trim();
            objPolPdf.ImpersonationDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain").ToString().Trim();
            */
            //GenratedfileName = objPolPdf.GeneratePolicyPremiumNoticepdf(PremNoticeXML);
            // GenratedfileName = objPolPdf.GeneratePremPdfDoc(PremNoticeXML);
            if (GenratedfileName != "")
            {
                return GenratedfileName;
            }
            else
            {
                return "";
            }

        }

        public String BoletoORpremNoticeGenerate()
        {
            String ChkBoletoORPREM_Notice = "";

            Cms.BusinessLayer.BlCommon.ClsSystemParams objClsSystemParams = new Cms.BusinessLayer.BlCommon.ClsSystemParams();
            DataSet SystemParams = objClsSystemParams.GetSystemParams();

            if (SystemParams.Tables.Count > 0)
            {
                ChkBoletoORPREM_Notice = SystemParams.Tables[0].Rows[0]["NOTIFY_RECVE_INSURED"].ToString();

            }
            if (ChkBoletoORPREM_Notice == "14667")
            {
                return "BOLETO";
            }
            else if (ChkBoletoORPREM_Notice == "14668")
            {
                return "PREM_NOTICE";
            }
            return "";
        }

        #endregion
        #region print jobs object
        public Cms.Model.Policy.Process.ClsPrintJobsInfo GetPrintJobsValues(int CustomerID, int PolicyId, int PolicyVersionId, int ProcessId, int ProcessRowId, int userId, string strCalledFor, string AgencyCode, string carrierCode)
        {
            Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo = new Cms.Model.Policy.Process.ClsPrintJobsInfo();
            objPrintJobsInfo.CUSTOMER_ID = CustomerID;
            objPrintJobsInfo.POLICY_ID = PolicyId;
            objPrintJobsInfo.POLICY_VERSION_ID = PolicyVersionId;
            objPrintJobsInfo.PROCESS_ID = ProcessId;
            objPrintJobsInfo.PROCESS_ROW_ID = ProcessRowId;
            objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
            objPrintJobsInfo.PRINT_DATETIME = System.DateTime.Now;
            objPrintJobsInfo.PRINTED_DATETIME = System.DateTime.Now;
            AgencyCode = AgencyCode.Trim(); carrierCode = carrierCode.Trim();
            if (strCalledFor == "CANC_NOTICE")
            {
                objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + carrierCode + "/" + AgencyCode + "/" + CustomerID.ToString() + "/" + "POLICY/CANC_NOTICE" + "/" + "final";
            }
            else if (strCalledFor == "PREM_NOTICE")
            {
                objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + carrierCode + "/" + AgencyCode + "/" + CustomerID.ToString() + "/" + "POLICY/PREM_NOTICE" + "/" + "final";
            }
            else if (strCalledFor == "REINS_NOTICE")
            {
                objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + carrierCode + "/" + AgencyCode + "/" + CustomerID.ToString() + "/" + "POLICY/REINS_NOTICE" + "/" + "final";
            }
            else if (strCalledFor == "BOLETO")
            {
                objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + carrierCode + "/" + AgencyCode + "/" + CustomerID.ToString() + "/" + "POLICY" + "/BOLETO/" + "final";
            }
            else if (strCalledFor == "CLAIM")
            {
                objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + carrierCode + "/" + AgencyCode + "/" + CustomerID.ToString() + "/" + "CLAIM/" + "final";
            }
            else
            {
                objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + carrierCode + "/" + AgencyCode + "/" + CustomerID.ToString() + "/" + "POLICY" + "/" + "final";
            }

            objPrintJobsInfo.ONDEMAND_FLAG = ClsPolicyProcess.PRINT_JOBS_ON_DEMAND_FLAG;
            objPrintJobsInfo.CREATED_DATETIME = System.DateTime.Now;
            objPrintJobsInfo.CREATED_BY = userId;
            return objPrintJobsInfo;
        }
        public void AddPrintJobs(Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo)
        {
            try
            {
                string filename = "";
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", objPrintJobsInfo.CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", objPrintJobsInfo.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objPrintJobsInfo.POLICY_VERSION_ID);
                objWrapper.AddParameter("@DOCUMENT_CODE", objPrintJobsInfo.DOCUMENT_CODE);
                objWrapper.AddParameter("@PRINT_DATETIME", objPrintJobsInfo.PRINT_DATETIME);
                objWrapper.AddParameter("@PRINTED_DATETIME", null);
                string urlPath = objPrintJobsInfo.URL_PATH;
                urlPath = urlPath.Replace("./", "/");
                objWrapper.AddParameter("@URL_PATH", urlPath);
                objWrapper.AddParameter("@ONDEMAND_FLAG", objPrintJobsInfo.ONDEMAND_FLAG);
                objWrapper.AddParameter("@PRINT_SUCCESSFUL", objPrintJobsInfo.PRINT_SUCCESSFUL);
                objWrapper.AddParameter("@DUPLEX", objPrintJobsInfo.DUPLEX);
                objWrapper.AddParameter("@CREATED_DATETIME", objPrintJobsInfo.CREATED_DATETIME);
                objWrapper.AddParameter("@CREATED_BY", objPrintJobsInfo.CREATED_BY);
                objWrapper.AddParameter("@ENTITY_TYPE", objPrintJobsInfo.ENTITY_TYPE);
                objWrapper.AddParameter("@ENTITY_ID", objPrintJobsInfo.ENTITY_ID);
                objWrapper.AddParameter("@AGENCY_ID", objPrintJobsInfo.AGENCY_ID);
                objWrapper.AddParameter("@FILE_NAME", objPrintJobsInfo.FILE_NAME);
                objWrapper.AddParameter("@CLAIM_ID", objPrintJobsInfo.CLAIM_ID);
                objWrapper.AddParameter("@ACTIVITY_ID", objPrintJobsInfo.ACTIVITY_ID);
                filename = objPrintJobsInfo.FILE_NAME;
                objWrapper.AddParameter("@PROCESS_ID", objPrintJobsInfo.PROCESS_ROW_ID);

                int returnResult = objWrapper.ExecuteNonQuery("Proc_InsertPRINT_JOBS");
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while adding entry in prin jobs.file not found.");
                addInfo.Add("CustomerID", objPrintJobsInfo.CUSTOMER_ID.ToString());
                addInfo.Add("PolicyID", objPrintJobsInfo.POLICY_ID.ToString());
                addInfo.Add("PolicyVersionID", objPrintJobsInfo.POLICY_VERSION_ID.ToString());
                addInfo.Add("ProcessRowID", objPrintJobsInfo.PROCESS_ID.ToString());
                addInfo.Add("FileURLPath", objPrintJobsInfo.URL_PATH);
                addInfo.Add("FileName", objPrintJobsInfo.FILE_NAME);
                addInfo.Add("DOCUMENT_CODE", objPrintJobsInfo.DOCUMENT_CODE);
                Exception ex1 = new Exception("Error while inserting Print job entry.File not found.");
                ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex, addInfo);
                throw (ex1);
                throw new Exception("Error while adding data to print jobs table", ex1);
            }

        }
        #endregion
    }

}
