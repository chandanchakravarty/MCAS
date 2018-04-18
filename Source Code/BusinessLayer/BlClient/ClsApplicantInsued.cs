/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date				: -	8/30/2005 4:49:49 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Mohit Gupta
<Modified By				: - 6/10/2005
<Purpose				: - Adding field IS_PRIMARY_APPLICANT.
*******************************************************************************************/ 
using System;
using System.Data;
using System.Data.SqlClient;
using Cms.Model;
using Cms.Model.Client;
using Cms.DataLayer;
using System.Xml;
using System.Text;

namespace Cms.BusinessLayer.BlClient
{
    /// <summary>
    /// Summary description for ClsApplicantInsued.
    /// </summary>
    public class ClsApplicantInsued : Cms.BusinessLayer.BlClient.ClsClient
    {
        private const string CLT_APPLICANT_LIST = "CLT_APPLICANT_LIST";

        #region Private Instance Variables
        private bool boolTransactionLog;
        //private int _APPLICANT_ID;
        private const string ACTIVATE_DEACTIVATE_PROC = "Proc_ActivateDeactivateCLT_APPLICANT_LIST";

        private int personalPrimaryApp;
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

        #region private Utility Functions
        #endregion

        #region Constructors
        /// <summary>
        /// deafault constructor
        /// </summary>
        public ClsApplicantInsued()
        {
            boolTransactionLog = base.TransactionLogRequired;
            base.strActivateDeactivateProcedure = ACTIVATE_DEACTIVATE_PROC;
            personalPrimaryApp = 0;
        }
        public ClsApplicantInsued(int primaryApp)
        {
            boolTransactionLog = base.TransactionLogRequired;
            base.strActivateDeactivateProcedure = ACTIVATE_DEACTIVATE_PROC;
            personalPrimaryApp = primaryApp;

        }

        #endregion

        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="ObjApplicantInsued">Model class object.</param>
        /// <returns>No of records effected.</returns>

        public int Add(ClsApplicantInsuedInfo ObjApplicantInsued)
        {
            string strCustomInfo = "";
            string Name = "";
            return Add(ObjApplicantInsued, strCustomInfo, Name);
        }
        public int Add(ClsApplicantInsuedInfo ObjApplicantInsued, string strCustomInfo, string Name)
        {

            string strStoredProc = "Proc_InsertCLT_APPLICANT_INSURED";
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {

               

                objDataWrapper.AddParameter("@CUSTOMER_ID", ObjApplicantInsued.CUSTOMER_ID);               
                objDataWrapper.AddParameter("@SUFFIX", ObjApplicantInsued.SUFFIX);
                //--------------------ADDED BY ABHINAV
                objDataWrapper.AddParameter("@ACCOUNT_TYPE", ObjApplicantInsued.ACCOUNT_TYPE);
                objDataWrapper.AddParameter("@ACCOUNT_NUMBER", ObjApplicantInsued.ACCOUNT_NUMBER);
                objDataWrapper.AddParameter("@BANK_NUMBER", ObjApplicantInsued.BANK_NUMBER);
                objDataWrapper.AddParameter("@BANK_BRANCH", ObjApplicantInsued.BANK_BRANCH);
                objDataWrapper.AddParameter("@BANK_NAME", ObjApplicantInsued.BANK_NAME);
                            
                objDataWrapper.AddParameter("@IS_ACTIVE", ObjApplicantInsued.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY", ObjApplicantInsued.CREATED_BY);
                //---------------------Added by mohit---------------

                objDataWrapper.AddParameter("@CO_APPLI_OCCU", ObjApplicantInsued.CO_APPLI_OCCU);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_NAME", ObjApplicantInsued.CO_APPLI_EMPL_NAME);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_ADDRESS", ObjApplicantInsued.CO_APPLI_EMPL_ADDRESS);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_ADDRESS1", ObjApplicantInsued.CO_APPLI_EMPL_ADDRESS1);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_CITY", ObjApplicantInsued.CO_APPLI_EMPL_CITY);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_COUNTRY", ObjApplicantInsued.CO_APPLI_EMPL_COUNTRY);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_STATE", ObjApplicantInsued.CO_APPLI_EMPL_STATE);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_ZIP_CODE", ObjApplicantInsued.CO_APPLI_EMPL_ZIP_CODE);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_PHONE", ObjApplicantInsued.CO_APPLI_EMPL_PHONE);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_EMAIL", ObjApplicantInsued.CO_APPLI_EMPL_EMAIL);
                //---------------------Added by Neeraj Singh---------------
                objDataWrapper.AddParameter("@CO_APPL_RELATIONSHIP", ObjApplicantInsued.CO_APPL_RELATIONSHIP);
                
               
                //Added By Lalit
                
                
                objDataWrapper.AddParameter("@ID_TYPE", ObjApplicantInsued.ID_TYPE);
                objDataWrapper.AddParameter("@ID_TYPE_NUMBER", ObjApplicantInsued.ID_TYPE_NUMBER);               
                objDataWrapper.AddParameter("@COMPLIMENT", ObjApplicantInsued.COMPLIMENT);
                if (ObjApplicantInsued.CO_APPLI_YEARS_WITH_CURR_EMPL != 0)
                {
                    //objDataWrapper.AddParameter("@CO_APPLI_YEARS_WITH_CURR_EMPL",DefaultValues.GetDoubleNullFromNegative(ObjApplicantInsued.CO_APPLI_YEARS_WITH_CURR_EMPL));
                    objDataWrapper.AddParameter("@CO_APPLI_YEARS_WITH_CURR_EMPL", ObjApplicantInsued.CO_APPLI_YEARS_WITH_CURR_EMPL);
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPLI_YEARS_WITH_CURR_EMPL", null);
                }

           


                if (ObjApplicantInsued.CO_APPL_YEAR_CURR_OCCU != 0)
                {
                    objDataWrapper.AddParameter("@CO_APPL_YEAR_CURR_OCCU", ObjApplicantInsued.CO_APPL_YEAR_CURR_OCCU);
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_YEAR_CURR_OCCU", null);
                }

            
               
                if (ObjApplicantInsued.CO_APPL_SSN_NO.Trim() != null)//Done for Itrack Issue 6063 on 7 July 09
                {
                    objDataWrapper.AddParameter("@CO_APPL_SSN_NO", ObjApplicantInsued.CO_APPL_SSN_NO);
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_SSN_NO", null);
                }

                //---------Added on 6/10/2005---------------.
                //objDataWrapper.AddParameter("@IS_PRIMARY_APPLICANT",ObjApplicantInsued.IS_PRIMARY_APPLICANT);
                //------------------End---------------------.

                // ---------------------------End---------------------- 

                // Added by mohit on 4/11/2005..						
                objDataWrapper.AddParameter("@DESC_CO_APPLI_OCCU", ObjApplicantInsued.DESC_CO_APPLI_OCCU);
                //  End

                //NEW DEFINED
                objDataWrapper.AddParameter("@APPLICANT_TYPE", ObjApplicantInsued.TYPE);
                objDataWrapper.AddParameter("@TITLE", ObjApplicantInsued.TITLE);
                objDataWrapper.AddParameter("@CONTACT_CODE", ObjApplicantInsued.CONTACT_CODE);
                objDataWrapper.AddParameter("@FIRST_NAME", ObjApplicantInsued.FIRST_NAME);
                objDataWrapper.AddParameter("@MIDDLE_NAME", ObjApplicantInsued.MIDDLE_NAME);
                objDataWrapper.AddParameter("@LAST_NAME", ObjApplicantInsued.LAST_NAME);         
                objDataWrapper.AddParameter("@ZIP_CODE", ObjApplicantInsued.ZIP_CODE);
                objDataWrapper.AddParameter("@ADDRESS1", ObjApplicantInsued.ADDRESS1);
                objDataWrapper.AddParameter("@NUMBER", ObjApplicantInsued.NUMBER);
                objDataWrapper.AddParameter("@ADDRESS2", ObjApplicantInsued.ADDRESS2);
                objDataWrapper.AddParameter("@DISTRICT", ObjApplicantInsued.DISTRICT);
                objDataWrapper.AddParameter("@CITY", ObjApplicantInsued.CITY);
                objDataWrapper.AddParameter("@STATE", ObjApplicantInsued.STATE);
                objDataWrapper.AddParameter("@COUNTRY", ObjApplicantInsued.COUNTRY);

                objDataWrapper.AddParameter("@CPF_CNPJ", ObjApplicantInsued.CPF_CNPJ);
                objDataWrapper.AddParameter("@PHONE", ObjApplicantInsued.PHONE);
                objDataWrapper.AddParameter("@BUSINESS_PHONE", ObjApplicantInsued.BUSINESS_PHONE);
                objDataWrapper.AddParameter("@EXT", ObjApplicantInsued.EXT);
                objDataWrapper.AddParameter("@MOBILE", ObjApplicantInsued.MOBILE);

                objDataWrapper.AddParameter("@FAX", ObjApplicantInsued.FAX);
                objDataWrapper.AddParameter("@EMAIL", ObjApplicantInsued.EMAIL);
                objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", ObjApplicantInsued.REGIONAL_IDENTIFICATION);
              //  objDataWrapper.AddParameter("@REG_ID_ISSUE", ObjApplicantInsued.REG_ID_ISSUE);
                if (ObjApplicantInsued.REG_ID_ISSUE != DateTime.MinValue)
                {
                    objDataWrapper.AddParameter("@REG_ID_ISSUE", ObjApplicantInsued.REG_ID_ISSUE);
                }
                else
                {
                    objDataWrapper.AddParameter("@REG_ID_ISSUE", null);
                }
                objDataWrapper.AddParameter("@ORIGINAL_ISSUE", ObjApplicantInsued.ORIGINAL_ISSUE);
                objDataWrapper.AddParameter("@POSITION", ObjApplicantInsued.POSITION);
                if (ObjApplicantInsued.CO_APPL_DOB.Ticks != 0)
                {
                    objDataWrapper.AddParameter("@CO_APPL_DOB", DefaultValues.GetDateNull(ObjApplicantInsued.CO_APPL_DOB));
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_DOB", null);
                }
                if (ObjApplicantInsued.CO_APPL_MARITAL_STATUS != null)
                {
                    objDataWrapper.AddParameter("@CO_APPL_MARITAL_STATUS", ObjApplicantInsued.CO_APPL_MARITAL_STATUS);
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_MARITAL_STATUS", null);

                }
                objDataWrapper.AddParameter("@CO_APPL_GENDER", ObjApplicantInsued.CO_APPL_GENDER);
                objDataWrapper.AddParameter("@NOTE", ObjApplicantInsued.NOTE);
                //END NEW DEFINED



                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@APPLICANT_ID", ObjApplicantInsued.APPLICANT_ID, SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    ObjApplicantInsued.TransactLabel = ClsClient.MapTransactionLabel(@"client\Aspx\AddApplicantInsued.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(ObjApplicantInsued);

                    if (ObjApplicantInsued.TYPE == 11110)
                    {
                        if (strTranXML != "" && strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                        {
                            XmlDocument XMLDoc = new XmlDocument();
                            XMLDoc.LoadXml(strTranXML);
                            XmlNodeList xnList = XMLDoc.SelectNodes("/LabelFieldMapping/Map[@field='FIRST_NAME']");
                            foreach (XmlElement node in xnList)
                            {
                                node.SetAttribute("label", Name);
                            }
                            strTranXML = XMLDoc.InnerXml;
                        }
                    }

                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    string CustomerName = "", CustomerCode = "";
                    string[] Customer = strCustomInfo.Split('~');
                    if (Customer.Length > 1)
                    {
                        CustomerName = Customer[0];
                        CustomerCode = Customer[1];
                    }

                    //					string sbStrXml = "";
                    //					StringBuilder sbTranXML=new StringBuilder();
                    //					string strCustXML="";
                    //					sbTranXML.Append("<LabelFieldMapping>");
                    //					strCustXML= strCustXML + "<Map label=\"Customer Name \" field=\"CUSTOMER_ID\" OldValue='' NewValue='"+ ObjApplicantInsued.CUSTOMER_ID + "' />";
                    //					sbTranXML.Append(strCustXML);
                    //					sbTranXML.Append("</LabelFieldMapping>");
                    //					sbStrXml = sbTranXML.ToString();
                    //					strTranXML= "<root>" + sbStrXml + strTranXML + "</root>";

                    objTransactionInfo.TRANS_TYPE_ID = 164;
                    objTransactionInfo.RECORDED_BY = ObjApplicantInsued.CREATED_BY;
                    //Modifid by shafi
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1524", "");//"New Applicant has been added.";
                    objTransactionInfo.CLIENT_ID = ObjApplicantInsued.CUSTOMER_ID;
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    objTransactionInfo.CUSTOM_INFO = "; Customer Name:" + CustomerName + "<br>" +
                        "; Customer Code:" + CustomerCode;
                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                int APPLICANT_ID = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();



                if (APPLICANT_ID == -1)
                {
                    return -1;
                }
                else
                {
                    ObjApplicantInsued.APPLICANT_ID = APPLICANT_ID;
                    //
                    //Update Application Table
                  //  UpdateAppCoverageOnCoApplicant(objDataWrapper, ObjApplicantInsued.CUSTOMER_ID, ObjApplicantInsued.APPLICANT_ID);
                    objDataWrapper.ClearParameteres();
                    //Update Policy Table
                    UpdatePolicyCoverageOnCoApplicant(objDataWrapper, ObjApplicantInsued.CUSTOMER_ID, ObjApplicantInsued.APPLICANT_ID);
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                    return ObjApplicantInsued.APPLICANT_ID;

                    //return returnResult;
                }
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
        /// Update Coverage Ho-82 on the bases of Occupation Of Applicant
        /// </summary>
        /// <param name="objDataWrapper"></param>
        /// <param name="ObjApplicantInsued"></param>
        public int AddCoApplicant(ClsApplicantInsuedInfo ObjApplicantInsued, DataWrapper objDataWrapper)
        {

            string strStoredProc = "Proc_InsertCLT_APPLICANT_INSURED_Acord";
            DateTime RecordDate = DateTime.Now;
            //	DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", ObjApplicantInsued.CUSTOMER_ID);
                objDataWrapper.AddParameter("@TITLE", ObjApplicantInsued.TITLE);
                objDataWrapper.AddParameter("@SUFFIX", ObjApplicantInsued.SUFFIX);
                objDataWrapper.AddParameter("@FIRST_NAME", ObjApplicantInsued.FIRST_NAME);
                objDataWrapper.AddParameter("@MIDDLE_NAME", ObjApplicantInsued.MIDDLE_NAME);
                objDataWrapper.AddParameter("@LAST_NAME", ObjApplicantInsued.LAST_NAME);
                objDataWrapper.AddParameter("@ADDRESS1", ObjApplicantInsued.ADDRESS1);
                objDataWrapper.AddParameter("@ADDRESS2", ObjApplicantInsued.ADDRESS2);
                objDataWrapper.AddParameter("@CITY", ObjApplicantInsued.CITY);
                objDataWrapper.AddParameter("@COUNTRY", ObjApplicantInsued.COUNTRY);
                objDataWrapper.AddParameter("@STATE", ObjApplicantInsued.STATE);
                objDataWrapper.AddParameter("@ZIP_CODE", ObjApplicantInsued.ZIP_CODE);
                objDataWrapper.AddParameter("@PHONE", ObjApplicantInsued.PHONE);
                objDataWrapper.AddParameter("@MOBILE", ObjApplicantInsued.MOBILE);
                objDataWrapper.AddParameter("@BUSINESS_PHONE", ObjApplicantInsued.BUSINESS_PHONE);
                objDataWrapper.AddParameter("@EXT", ObjApplicantInsued.EXT);
                objDataWrapper.AddParameter("@EMAIL", ObjApplicantInsued.EMAIL);
                objDataWrapper.AddParameter("@IS_ACTIVE", ObjApplicantInsued.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY", ObjApplicantInsued.CREATED_BY);
                //---------------------Added by mohit---------------

                objDataWrapper.AddParameter("@CO_APPLI_OCCU", ObjApplicantInsued.CO_APPLI_OCCU);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_NAME", ObjApplicantInsued.CO_APPLI_EMPL_NAME);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_ADDRESS", ObjApplicantInsued.CO_APPLI_EMPL_ADDRESS);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_ADDRESS1", ObjApplicantInsued.CO_APPLI_EMPL_ADDRESS1);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_CITY", ObjApplicantInsued.CO_APPLI_EMPL_CITY);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_COUNTRY", ObjApplicantInsued.CO_APPLI_EMPL_COUNTRY);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_STATE", ObjApplicantInsued.CO_APPLI_EMPL_STATE);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_ZIP_CODE", ObjApplicantInsued.CO_APPLI_EMPL_ZIP_CODE);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_PHONE", ObjApplicantInsued.CO_APPLI_EMPL_PHONE);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_EMAIL", ObjApplicantInsued.CO_APPLI_EMPL_EMAIL);
                //---------------------Added by Neeraj Singh---------------
                objDataWrapper.AddParameter("@CO_APPL_RELATIONSHIP", ObjApplicantInsued.CO_APPL_RELATIONSHIP);
                objDataWrapper.AddParameter("@CO_APPL_GENDER", ObjApplicantInsued.CO_APPL_GENDER);

                if (ObjApplicantInsued.CO_APPLI_YEARS_WITH_CURR_EMPL != 0)
                {
                    //objDataWrapper.AddParameter("@CO_APPLI_YEARS_WITH_CURR_EMPL",DefaultValues.GetDoubleNullFromNegative(ObjApplicantInsued.CO_APPLI_YEARS_WITH_CURR_EMPL));
                    objDataWrapper.AddParameter("@CO_APPLI_YEARS_WITH_CURR_EMPL", ObjApplicantInsued.CO_APPLI_YEARS_WITH_CURR_EMPL);
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPLI_YEARS_WITH_CURR_EMPL", null);
                }

                if (ObjApplicantInsued.CO_APPL_DOB.Ticks != 0)
                {
                    objDataWrapper.AddParameter("@CO_APPL_DOB", DefaultValues.GetDateNull(ObjApplicantInsued.CO_APPL_DOB));
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_DOB", null);
                }


                if (ObjApplicantInsued.CO_APPL_YEAR_CURR_OCCU != 0)
                {
                    objDataWrapper.AddParameter("@CO_APPL_YEAR_CURR_OCCU", ObjApplicantInsued.CO_APPL_YEAR_CURR_OCCU);
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_YEAR_CURR_OCCU", null);
                }

                if (ObjApplicantInsued.CO_APPL_MARITAL_STATUS != null)
                {
                    objDataWrapper.AddParameter("@CO_APPL_MARITAL_STATUS", ObjApplicantInsued.CO_APPL_MARITAL_STATUS);
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_MARITAL_STATUS", null);

                }
                if (ObjApplicantInsued.CO_APPL_SSN_NO != null)
                {
                    objDataWrapper.AddParameter("@CO_APPL_SSN_NO", ObjApplicantInsued.CO_APPL_SSN_NO);
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_SSN_NO", null);
                }

                //---------Added on 6/10/2005---------------.
                //objDataWrapper.AddParameter("@IS_PRIMARY_APPLICANT",ObjApplicantInsued.IS_PRIMARY_APPLICANT);
                //------------------End---------------------.

                // ---------------------------End---------------------- 

                // Added by mohit on 4/11/2005..						
                objDataWrapper.AddParameter("@DESC_CO_APPLI_OCCU", ObjApplicantInsued.DESC_CO_APPLI_OCCU);
                //  End





                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@APPLICANT_ID", ObjApplicantInsued.APPLICANT_ID, SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    ObjApplicantInsued.TransactLabel = ClsClient.MapTransactionLabel(@"client\Aspx\AddApplicantInsued.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(ObjApplicantInsued);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    string CustomerName = "", CustomerCode = "";
                    //						string []Customer = strCustomInfo.Split('~');
                    //						if(Customer.Length > 1)
                    //						{
                    //							CustomerName = Customer[0];
                    //							CustomerCode = Customer[1];
                    //						}

                    //					string sbStrXml = "";
                    //					StringBuilder sbTranXML=new StringBuilder();
                    //					string strCustXML="";
                    //					sbTranXML.Append("<LabelFieldMapping>");
                    //					strCustXML= strCustXML + "<Map label=\"Customer Name \" field=\"CUSTOMER_ID\" OldValue='' NewValue='"+ ObjApplicantInsued.CUSTOMER_ID + "' />";
                    //					sbTranXML.Append(strCustXML);
                    //					sbTranXML.Append("</LabelFieldMapping>");
                    //					sbStrXml = sbTranXML.ToString();
                    //					strTranXML= "<root>" + sbStrXml + strTranXML + "</root>";

                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = ObjApplicantInsued.CREATED_BY;
                    //Modifid by shafi
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1524", "");// "New Applicant has been added.";
                    objTransactionInfo.CLIENT_ID = ObjApplicantInsued.CUSTOMER_ID;
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    objTransactionInfo.CUSTOM_INFO = "; Customer Name:" + CustomerName + "<br>" +
                        "; Customer Code:" + CustomerCode;
                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                int APPLICANT_ID = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();



                if (APPLICANT_ID == -1)
                {
                    return -1;
                }
                else
                {
                    ObjApplicantInsued.APPLICANT_ID = APPLICANT_ID;
                    //
                    //Update Application Table
                    //						UpdateAppCoverageOnCoApplicant(objDataWrapper,ObjApplicantInsued.CUSTOMER_ID,ObjApplicantInsued.APPLICANT_ID);
                    //						objDataWrapper.ClearParameteres();
                    //						//Update Policy Table
                    //						UpdatePolicyCoverageOnCoApplicant(objDataWrapper,ObjApplicantInsued.CUSTOMER_ID,ObjApplicantInsued.APPLICANT_ID);
                    //						objDataWrapper.ClearParameteres();
                    //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                    return ObjApplicantInsued.APPLICANT_ID;

                    //return returnResult;
                }
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            //				finally
            //				{
            //					if(objDataWrapper != null) objDataWrapper.Dispose();
            //				}
        }
        /// <summary>
        /// Update Coverage Ho-82 on the bases of Occupation Of Applicant
        /// </summary>
        /// <param name="objDataWrapper"></param>
        /// <param name="customerId"></param>
        /// <param name="applicantId"></param>
        private void UpdateAppCoverageOnCoApplicant(DataWrapper objDataWrapper, int customerId, int applicantId)
        {
            objDataWrapper.AddParameter("@CUSTOMER_ID", customerId);
            objDataWrapper.AddParameter("@APPLICANT_ID", applicantId);
            objDataWrapper.ExecuteNonQuery("PROC_UPDATE_HOME_COVERAGE_BY_APPLICANT");

        }
        private void UpdatePolicyCoverageOnCoApplicant(DataWrapper objDataWrapper, int customerId, int applicantId)
        {
            objDataWrapper.AddParameter("@CUSTOMER_ID", customerId);
            objDataWrapper.AddParameter("@APPLICANT_ID", applicantId);
            objDataWrapper.ExecuteNonQuery("PROC_UPDATE_POLICY_HOME_COVERAGE_BY_APPLICANT");

        }
        public int Add(ClsApplicantInsuedInfo ObjApplicantInsued, int ApplicationFlag)
        {

            string strStoredProc = "Proc_InsertCLT_APPLICANT_INSURED";
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", ObjApplicantInsued.CUSTOMER_ID);
                objDataWrapper.AddParameter("@TITLE", ObjApplicantInsued.TITLE);
                objDataWrapper.AddParameter("@SUFFIX", ObjApplicantInsued.SUFFIX);
                objDataWrapper.AddParameter("@FIRST_NAME", ObjApplicantInsued.FIRST_NAME);
                objDataWrapper.AddParameter("@MIDDLE_NAME", ObjApplicantInsued.MIDDLE_NAME);
                objDataWrapper.AddParameter("@LAST_NAME", ObjApplicantInsued.LAST_NAME);
                objDataWrapper.AddParameter("@ADDRESS1", ObjApplicantInsued.ADDRESS1);
                objDataWrapper.AddParameter("@ADDRESS2", ObjApplicantInsued.ADDRESS2);
                objDataWrapper.AddParameter("@CITY", ObjApplicantInsued.CITY);
                objDataWrapper.AddParameter("@COUNTRY", ObjApplicantInsued.COUNTRY);
                objDataWrapper.AddParameter("@STATE", ObjApplicantInsued.STATE);
                objDataWrapper.AddParameter("@ZIP_CODE", ObjApplicantInsued.ZIP_CODE);
                objDataWrapper.AddParameter("@PHONE", ObjApplicantInsued.PHONE);
                objDataWrapper.AddParameter("@EMAIL", ObjApplicantInsued.EMAIL);
                objDataWrapper.AddParameter("@IS_ACTIVE", ObjApplicantInsued.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY", ObjApplicantInsued.CREATED_BY);
                //---------------------Added by mohit---------------

                objDataWrapper.AddParameter("@CO_APPLI_OCCU", ObjApplicantInsued.CO_APPLI_OCCU);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_NAME", ObjApplicantInsued.CO_APPLI_EMPL_NAME);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_ADDRESS", ObjApplicantInsued.CO_APPLI_EMPL_ADDRESS);
                objDataWrapper.AddParameter("@CO_APPL_RELATIONSHIP", ObjApplicantInsued.CO_APPL_RELATIONSHIP);
                objDataWrapper.AddParameter("@CO_APPL_GENDER", ObjApplicantInsued.CO_APPL_GENDER);
                if (ObjApplicantInsued.CO_APPLI_YEARS_WITH_CURR_EMPL != 0)
                {
                    //objDataWrapper.AddParameter("@CO_APPLI_YEARS_WITH_CURR_EMPL",DefaultValues.GetDoubleNullFromNegative(ObjApplicantInsued.CO_APPLI_YEARS_WITH_CURR_EMPL));
                    objDataWrapper.AddParameter("@CO_APPLI_YEARS_WITH_CURR_EMPL", ObjApplicantInsued.CO_APPLI_YEARS_WITH_CURR_EMPL);
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPLI_YEARS_WITH_CURR_EMPL", null);
                }

                if (ObjApplicantInsued.CO_APPL_DOB.Ticks != 0)
                {
                    objDataWrapper.AddParameter("@CO_APPL_DOB", DefaultValues.GetDateNull(ObjApplicantInsued.CO_APPL_DOB));
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_DOB", null);
                }


                if (ObjApplicantInsued.CO_APPL_YEAR_CURR_OCCU != 0)
                {
                    objDataWrapper.AddParameter("@CO_APPL_YEAR_CURR_OCCU", ObjApplicantInsued.CO_APPL_YEAR_CURR_OCCU);
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_YEAR_CURR_OCCU", null);
                }

                if (ObjApplicantInsued.CO_APPL_MARITAL_STATUS != null)
                {
                    objDataWrapper.AddParameter("@CO_APPL_MARITAL_STATUS", ObjApplicantInsued.CO_APPL_MARITAL_STATUS);
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_MARITAL_STATUS", null);

                }
                if (ObjApplicantInsued.CO_APPL_SSN_NO != null)
                {
                    objDataWrapper.AddParameter("@CO_APPL_SSN_NO", ObjApplicantInsued.CO_APPL_SSN_NO);
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_SSN_NO", null);
                }

                //---------Added on 6/10/2005---------------.
                //objDataWrapper.AddParameter("@IS_PRIMARY_APPLICANT",ObjApplicantInsued.IS_PRIMARY_APPLICANT);
                //------------------End---------------------.

                // ---------------------------End---------------------- 

                // Added by mohit on 4/11/2005..						
                objDataWrapper.AddParameter("@DESC_CO_APPLI_OCCU", ObjApplicantInsued.DESC_CO_APPLI_OCCU);
                //  End
                //Flag to indicate that the applicant has to be added at application level also
                objDataWrapper.AddParameter("@APPLICATION_FLAG", ApplicationFlag);



                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@APPLICANT_ID", ObjApplicantInsued.APPLICANT_ID, SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    ObjApplicantInsued.TransactLabel = ClsClient.MapTransactionLabel(@"client\Aspx\AddApplicantInsued.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(ObjApplicantInsued);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = ObjApplicantInsued.CREATED_BY;
                    //Modifid by shafi
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1524", "");//"New Applicant has been added.";
                    objTransactionInfo.CLIENT_ID = ObjApplicantInsued.CUSTOMER_ID;
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                int APPLICANT_ID = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (APPLICANT_ID == -1)
                {
                    return -1;
                }
                else
                {
                    ObjApplicantInsued.APPLICANT_ID = APPLICANT_ID;
                    return returnResult;
                }
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

        #region Update method
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldApplicantInsued">Model object having old information</param>
        /// <param name="ObjApplicantInsued">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        public int Update(ClsApplicantInsuedInfo objOldApplicantInsued, ClsApplicantInsuedInfo ObjApplicantInsued,string Name)
        {
            string strStoredProc = "Proc_UpdateCLT_APPLICANT_LIST";
            string strTranXML;
            int returnResult = 0;
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@APPLICANT_ID", ObjApplicantInsued.APPLICANT_ID);
                objDataWrapper.AddParameter("@CUSTOMER_ID", ObjApplicantInsued.CUSTOMER_ID);
               
                objDataWrapper.AddParameter("@SUFFIX", ObjApplicantInsued.SUFFIX);

                //objDataWrapper.AddParameter("@TITLE", ObjApplicantInsued.TITLE);
                //objDataWrapper.AddParameter("@FIRST_NAME", ObjApplicantInsued.FIRST_NAME);
                //objDataWrapper.AddParameter("@ADDRESS1", ObjApplicantInsued.ADDRESS1);
                //objDataWrapper.AddParameter("@ADDRESS2", ObjApplicantInsued.ADDRESS2);
                //objDataWrapper.AddParameter("@CITY", ObjApplicantInsued.CITY);
                //objDataWrapper.AddParameter("@COUNTRY", ObjApplicantInsued.COUNTRY);
                //objDataWrapper.AddParameter("@STATE", ObjApplicantInsued.STATE);
                // objDataWrapper.AddParameter("@ZIP_CODE", ObjApplicantInsued.ZIP_CODE);
                //objDataWrapper.AddParameter("@PHONE", ObjApplicantInsued.PHONE);
                //objDataWrapper.AddParameter("@MOBILE", ObjApplicantInsued.MOBILE);
                //objDataWrapper.AddParameter("@BUSINESS_PHONE", ObjApplicantInsued.BUSINESS_PHONE);
                //objDataWrapper.AddParameter("@EXT", ObjApplicantInsued.EXT);
                //objDataWrapper.AddParameter("@EMAIL", ObjApplicantInsued.EMAIL);

                
                objDataWrapper.AddParameter("@IS_ACTIVE", ObjApplicantInsued.IS_ACTIVE);
                objDataWrapper.AddParameter("@MODIFIED_BY", ObjApplicantInsued.MODIFIED_BY);
                //----------------------------Added by Mohit---------------------------

                objDataWrapper.AddParameter("@CO_APPLI_OCCU", ObjApplicantInsued.CO_APPLI_OCCU);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_NAME", ObjApplicantInsued.CO_APPLI_EMPL_NAME);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_ADDRESS", ObjApplicantInsued.CO_APPLI_EMPL_ADDRESS);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_ADDRESS1", ObjApplicantInsued.CO_APPLI_EMPL_ADDRESS1);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_CITY", ObjApplicantInsued.CO_APPLI_EMPL_CITY);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_COUNTRY", ObjApplicantInsued.CO_APPLI_EMPL_COUNTRY);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_STATE", ObjApplicantInsued.CO_APPLI_EMPL_STATE);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_ZIP_CODE", ObjApplicantInsued.CO_APPLI_EMPL_ZIP_CODE);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_PHONE", ObjApplicantInsued.CO_APPLI_EMPL_PHONE);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_EMAIL", ObjApplicantInsued.CO_APPLI_EMPL_EMAIL);
                objDataWrapper.AddParameter("@CO_APPL_RELATIONSHIP", ObjApplicantInsued.CO_APPL_RELATIONSHIP);
                objDataWrapper.AddParameter("@ACCOUNT_TYPE", ObjApplicantInsued.ACCOUNT_TYPE);
                objDataWrapper.AddParameter("@ACCOUNT_NUMBER", ObjApplicantInsued.ACCOUNT_NUMBER);
                objDataWrapper.AddParameter("@BANK_NUMBER", ObjApplicantInsued.BANK_NUMBER);
                objDataWrapper.AddParameter("@BANK_BRANCH", ObjApplicantInsued.BANK_BRANCH);
                objDataWrapper.AddParameter("@BANK_NAME", ObjApplicantInsued.BANK_NAME);
                
                if (ObjApplicantInsued.CO_APPLI_YEARS_WITH_CURR_EMPL == 0)
                {
                    objDataWrapper.AddParameter("@CO_APPLI_YEARS_WITH_CURR_EMPL", null);
                }
                else
                    objDataWrapper.AddParameter("@CO_APPLI_YEARS_WITH_CURR_EMPL", DefaultValues.GetDoubleNullFromNegative(ObjApplicantInsued.CO_APPLI_YEARS_WITH_CURR_EMPL));


                if (ObjApplicantInsued.CO_APPL_YEAR_CURR_OCCU == 0)
                {
                    objDataWrapper.AddParameter("@CO_APPL_YEAR_CURR_OCCU", null);
                }
                else
                    objDataWrapper.AddParameter("@CO_APPL_YEAR_CURR_OCCU", DefaultValues.GetDoubleNullFromNegative(ObjApplicantInsued.CO_APPL_YEAR_CURR_OCCU));

              
              

                objDataWrapper.AddParameter("@CO_APPL_SSN_NO", ObjApplicantInsued.CO_APPL_SSN_NO);

                //---------Added on 6/10/2005---------------.
                //objDataWrapper.AddParameter("@IS_PRIMARY_APPLICANT",ObjApplicantInsued.IS_PRIMARY_APPLICANT);
                //------------------End---------------------.

                //-----------------------------End-------------------------------------

                // Added by mohit on 4/11/2005..						
                objDataWrapper.AddParameter("@DESC_CO_APPLI_OCCU", ObjApplicantInsued.DESC_CO_APPLI_OCCU);
                objDataWrapper.AddParameter("@COMPLIMENT", ObjApplicantInsued.COMPLIMENT);
                objDataWrapper.AddParameter("@ID_TYPE", ObjApplicantInsued.ID_TYPE);
                objDataWrapper.AddParameter("@ID_TYPE_NUMBER", ObjApplicantInsued.ID_TYPE_NUMBER);  
                //  End

                //UPDATE NEW DEFINED 

                objDataWrapper.AddParameter("@APPLICANT_TYPE", ObjApplicantInsued.TYPE);
                objDataWrapper.AddParameter("@TITLE", ObjApplicantInsued.TITLE);
                objDataWrapper.AddParameter("@CONTACT_CODE", ObjApplicantInsued.CONTACT_CODE);
                objDataWrapper.AddParameter("@FIRST_NAME", ObjApplicantInsued.FIRST_NAME);
                objDataWrapper.AddParameter("@MIDDLE_NAME", ObjApplicantInsued.MIDDLE_NAME);
                objDataWrapper.AddParameter("@LAST_NAME", ObjApplicantInsued.LAST_NAME);
              
                
                objDataWrapper.AddParameter("@ZIP_CODE", ObjApplicantInsued.ZIP_CODE);
                objDataWrapper.AddParameter("@ADDRESS1", ObjApplicantInsued.ADDRESS1);
                objDataWrapper.AddParameter("@NUMBER", ObjApplicantInsued.NUMBER);
                objDataWrapper.AddParameter("@ADDRESS2", ObjApplicantInsued.ADDRESS2);
                objDataWrapper.AddParameter("@DISTRICT", ObjApplicantInsued.DISTRICT);
                objDataWrapper.AddParameter("@CITY", ObjApplicantInsued.CITY);
                objDataWrapper.AddParameter("@STATE", ObjApplicantInsued.STATE);
                objDataWrapper.AddParameter("@COUNTRY", ObjApplicantInsued.COUNTRY);

                objDataWrapper.AddParameter("@CPF_CNPJ", ObjApplicantInsued.CPF_CNPJ);
                objDataWrapper.AddParameter("@PHONE", ObjApplicantInsued.PHONE);
                objDataWrapper.AddParameter("@BUSINESS_PHONE", ObjApplicantInsued.BUSINESS_PHONE);
                objDataWrapper.AddParameter("@EXT", ObjApplicantInsued.EXT);
                objDataWrapper.AddParameter("@MOBILE", ObjApplicantInsued.MOBILE);
                
                objDataWrapper.AddParameter("@FAX", ObjApplicantInsued.FAX);
                objDataWrapper.AddParameter("@EMAIL", ObjApplicantInsued.EMAIL);
                objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", ObjApplicantInsued.REGIONAL_IDENTIFICATION);
               // objDataWrapper.AddParameter("@REG_ID_ISSUE", ObjApplicantInsued.REG_ID_ISSUE);
                if (ObjApplicantInsued.REG_ID_ISSUE != DateTime.MinValue)
                {
                    objDataWrapper.AddParameter("@REG_ID_ISSUE", ObjApplicantInsued.REG_ID_ISSUE);
                }
                else
                {
                    objDataWrapper.AddParameter("@REG_ID_ISSUE", null);
                }
                objDataWrapper.AddParameter("@ORIGINAL_ISSUE", ObjApplicantInsued.ORIGINAL_ISSUE);
                if (ObjApplicantInsued.CO_APPL_DOB.Ticks != 0)
                {
                    objDataWrapper.AddParameter("@CO_APPL_DOB", DefaultValues.GetDateNull(ObjApplicantInsued.CO_APPL_DOB));
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_DOB", null);
                }

                
                if (ObjApplicantInsued.CO_APPL_MARITAL_STATUS.Trim() == "")
                {
                    objDataWrapper.AddParameter("@CO_APPL_MARITAL_STATUS", null);
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_MARITAL_STATUS", ObjApplicantInsued.CO_APPL_MARITAL_STATUS);
                }
                objDataWrapper.AddParameter("@CO_APPL_GENDER", ObjApplicantInsued.CO_APPL_GENDER);
                objDataWrapper.AddParameter("@POSITION", ObjApplicantInsued.POSITION);
                objDataWrapper.AddParameter("@NOTE", ObjApplicantInsued.NOTE);

                //UPDATE END NEW DEFINED


                if (base.TransactionLogRequired)
                {
                    ObjApplicantInsued.TransactLabel = ClsClient.MapTransactionLabel(@"client\Aspx\AddApplicantInsued.aspx.resx");
                    objBuilder.GetUpdateSQL(objOldApplicantInsued, ObjApplicantInsued, out strTranXML);

                   

                    
                    //					string sbStrXml = "";
                    //					StringBuilder sbTranXML=new StringBuilder();
                    //					string strCustXML="";
                    //					sbTranXML.Append("<LabelFieldMapping>");
                    //					strCustXML= strCustXML + "<Map label=\"Customer Name \" field=\"CUSTOMER_ID\" OldValue='' NewValue='"+ ObjApplicantInsued.CUSTOMER_ID + "' />";
                    //					sbTranXML.Append(strCustXML);
                    //					sbTranXML.Append("</LabelFieldMapping>");
                    //					sbStrXml = sbTranXML.ToString();
                    //					
                    //					strTranXML= "<root>" + sbStrXml + strTranXML + "</root>";

                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 165;
                    objTransactionInfo.RECORDED_BY = ObjApplicantInsued.MODIFIED_BY;
                    objTransactionInfo.CLIENT_ID = ObjApplicantInsued.CUSTOMER_ID;
                    if (personalPrimaryApp == 1)
                    {
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1530", "");// "Applicant/Customer is modified.";
                    }
                    else
                    {
                        //modified By shafi
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1529", "");// "Applicant Information is modified.";
                    }


                    if (ObjApplicantInsued.TYPE == 11110)
                    {
                        if (strTranXML != "" && strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                        {
                            XmlDocument XMLDoc = new XmlDocument();
                            XMLDoc.LoadXml(strTranXML);
                            XmlNodeList xnList = XMLDoc.SelectNodes("/LabelFieldMapping/Map[@field='FIRST_NAME']");

                            foreach (XmlElement node in xnList)
                            {
                                node.SetAttribute("label", Name);
                            }
                            strTranXML = XMLDoc.InnerXml;
                        }
                    }
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    objTransactionInfo.CUSTOM_INFO = ";Customer Name = " + objOldApplicantInsued.CUSTOMER_FIRST_NAME + " " + objOldApplicantInsued.CUSTOMER_MIDDLE_NAME + " " + objOldApplicantInsued.CUSTOMER_LAST_NAME + ";Customer Code = " + objOldApplicantInsued.CUSTOMER_CODE;
                    //objTransactionInfo.CUSTOM_INFO		=	";Modified By = " + ObjApplicantInsued.MODIFIED_BY;

                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);

                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                objDataWrapper.ClearParameteres();

                //Update Coverage when Occupation is changed
                if (ObjApplicantInsued.CO_APPLI_OCCU != objOldApplicantInsued.CO_APPLI_OCCU)
                {
                    //Update Application Table
                   // UpdateAppCoverageOnCoApplicant(objDataWrapper, ObjApplicantInsued.CUSTOMER_ID, ObjApplicantInsued.APPLICANT_ID);
                    objDataWrapper.ClearParameteres();
                    //Update Policy Table
                    UpdatePolicyCoverageOnCoApplicant(objDataWrapper, ObjApplicantInsued.CUSTOMER_ID, ObjApplicantInsued.APPLICANT_ID);

                    objDataWrapper.ClearParameteres();
                }
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
                if (objBuilder != null)
                {
                    objBuilder = null;
                }
            }
        }

        ///
        public int Update(ClsApplicantInsuedInfo objOldApplicantInsued, ClsApplicantInsuedInfo ObjApplicantInsued, int ApplicationFlag)
        {
            string strStoredProc = "Proc_UpdateCLT_APPLICANT_LIST";
            string strTranXML;
            int returnResult = 0;
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@APPLICANT_ID", ObjApplicantInsued.APPLICANT_ID);
                objDataWrapper.AddParameter("@CUSTOMER_ID", ObjApplicantInsued.CUSTOMER_ID);
                objDataWrapper.AddParameter("@TITLE", ObjApplicantInsued.TITLE);
                objDataWrapper.AddParameter("@SUFFIX", ObjApplicantInsued.SUFFIX);
                objDataWrapper.AddParameter("@FIRST_NAME", ObjApplicantInsued.FIRST_NAME);
                objDataWrapper.AddParameter("@MIDDLE_NAME", ObjApplicantInsued.MIDDLE_NAME);
                objDataWrapper.AddParameter("@LAST_NAME", ObjApplicantInsued.LAST_NAME);
                objDataWrapper.AddParameter("@ADDRESS1", ObjApplicantInsued.ADDRESS1);
                objDataWrapper.AddParameter("@ADDRESS2", ObjApplicantInsued.ADDRESS2);
                objDataWrapper.AddParameter("@CITY", ObjApplicantInsued.CITY);
                objDataWrapper.AddParameter("@COUNTRY", ObjApplicantInsued.COUNTRY);
                objDataWrapper.AddParameter("@STATE", ObjApplicantInsued.STATE);
                objDataWrapper.AddParameter("@ZIP_CODE", ObjApplicantInsued.ZIP_CODE);
                objDataWrapper.AddParameter("@PHONE", ObjApplicantInsued.PHONE);
                objDataWrapper.AddParameter("@EMAIL", ObjApplicantInsued.EMAIL);
                objDataWrapper.AddParameter("@IS_ACTIVE", ObjApplicantInsued.IS_ACTIVE);
                objDataWrapper.AddParameter("@MODIFIED_BY", ObjApplicantInsued.MODIFIED_BY);
                objDataWrapper.AddParameter("@CO_APPL_RELATIONSHIP", ObjApplicantInsued.CO_APPL_RELATIONSHIP);
                objDataWrapper.AddParameter("@ACCOUNT_TYPE", ObjApplicantInsued.ACCOUNT_TYPE);
                objDataWrapper.AddParameter("@ACCOUNT_NUMBER", ObjApplicantInsued.ACCOUNT_NUMBER);
                objDataWrapper.AddParameter("@BANK_NUMBER", ObjApplicantInsued.BANK_NUMBER);
                objDataWrapper.AddParameter("@BANK_BRANCH", ObjApplicantInsued.BANK_BRANCH);
                objDataWrapper.AddParameter("@BANK_NAME", ObjApplicantInsued.BANK_NAME);
                //----------------------------Added by Mohit---------------------------

                objDataWrapper.AddParameter("@CO_APPLI_OCCU", ObjApplicantInsued.CO_APPLI_OCCU);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_NAME", ObjApplicantInsued.CO_APPLI_EMPL_NAME);
                objDataWrapper.AddParameter("@CO_APPLI_EMPL_ADDRESS", ObjApplicantInsued.CO_APPLI_EMPL_ADDRESS);
                objDataWrapper.AddParameter("@CO_APPL_RELATIONSHIP", ObjApplicantInsued.CO_APPL_RELATIONSHIP);
                objDataWrapper.AddParameter("@CO_APPL_GENDER", ObjApplicantInsued.CO_APPL_GENDER);
                if (ObjApplicantInsued.CO_APPLI_YEARS_WITH_CURR_EMPL == 0)
                {
                    objDataWrapper.AddParameter("@CO_APPLI_YEARS_WITH_CURR_EMPL", null);
                }
                else
                    objDataWrapper.AddParameter("@CO_APPLI_YEARS_WITH_CURR_EMPL", DefaultValues.GetDoubleNullFromNegative(ObjApplicantInsued.CO_APPLI_YEARS_WITH_CURR_EMPL));


                if (ObjApplicantInsued.CO_APPL_YEAR_CURR_OCCU == 0)
                {
                    objDataWrapper.AddParameter("@CO_APPL_YEAR_CURR_OCCU", null);
                }
                else
                    objDataWrapper.AddParameter("@CO_APPL_YEAR_CURR_OCCU", DefaultValues.GetDoubleNullFromNegative(ObjApplicantInsued.CO_APPL_YEAR_CURR_OCCU));

                if (ObjApplicantInsued.CO_APPL_MARITAL_STATUS.Trim() == "")
                {
                    objDataWrapper.AddParameter("@CO_APPL_MARITAL_STATUS", null);
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_MARITAL_STATUS", ObjApplicantInsued.CO_APPL_MARITAL_STATUS);
                }
                if (ObjApplicantInsued.CO_APPL_DOB.Ticks != 0)
                {
                    objDataWrapper.AddParameter("@CO_APPL_DOB", DefaultValues.GetDateNull(ObjApplicantInsued.CO_APPL_DOB));
                }
                else
                {
                    objDataWrapper.AddParameter("@CO_APPL_DOB", null);
                }


                objDataWrapper.AddParameter("@CO_APPL_SSN_NO", ObjApplicantInsued.CO_APPL_SSN_NO);

                //---------Added on 6/10/2005---------------.
                //objDataWrapper.AddParameter("@IS_PRIMARY_APPLICANT",ObjApplicantInsued.IS_PRIMARY_APPLICANT);
                //------------------End---------------------.

                //-----------------------------End-------------------------------------

                // Added by mohit on 4/11/2005..						
                objDataWrapper.AddParameter("@DESC_CO_APPLI_OCCU", ObjApplicantInsued.DESC_CO_APPLI_OCCU);
                //  End
                //Flag to indicate that the applicant has to be added at application level also
                objDataWrapper.AddParameter("@APPLICATION_FLAG", ApplicationFlag);


                if (base.TransactionLogRequired)
                {
                    ObjApplicantInsued.TransactLabel = ClsClient.MapTransactionLabel(@"client\Aspx\AddApplicantInsued.aspx.resx");
                    objBuilder.GetUpdateSQL(objOldApplicantInsued, ObjApplicantInsued, out strTranXML);

                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 3;
                    objTransactionInfo.RECORDED_BY = ObjApplicantInsued.MODIFIED_BY;
                    objTransactionInfo.CLIENT_ID = ObjApplicantInsued.CUSTOMER_ID;
                    if (personalPrimaryApp == 1)
                    {
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1530", "");// "Applicant/Customer is modified.";
                    }
                    else
                    {
                        //modified By shafi
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1529", "");//"Applicant Information is modified.";
                    }
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);

                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
                if (objBuilder != null)
                {
                    objBuilder = null;
                }
            }
        }
        #endregion

        #region "GetxmlMethods"
        public static string GetXmlForPageControls(int APPLICANT_ID)
        {
            string strSql = "Proc_GetXmlCLT_APPLICANT_LIST";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@APPLICANT_ID", APPLICANT_ID);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet.GetXml();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CUSTOMER_ID"></param>
        /// <param name="statusPrimary"></param>
        /// <returns></returns>
        public static int SetPrimaryApplicantCustomer(int CUSTOMER_ID, int APPLICANT_ID)
        {
            string strSql = "Proc_SetPrimaryApplicantCustomer";
            int result;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
            objDataWrapper.AddParameter("@APPLICANT_ID", APPLICANT_ID);
            result = objDataWrapper.ExecuteNonQuery(strSql);
            return result;
        }
        #endregion

        public static string CheckCustomerIsActive(int Cust_Id, out string isActive)
        {
            string strProcedure = "Proc_CheckCustomerIsActive";
            DataSet objDataSet = new DataSet();
            //string str = "";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@Cust_Id", Cust_Id);
                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@isActiveCust", SqlDbType.VarChar, ParameterDirection.Output);
                objSqlParameter.Size = 2;
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
                isActive = objSqlParameter.Value.ToString();
                return isActive;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        //Added by Sibin on 16 Feb 07 for Itrack Issue 4964
        public int GetPolicyNameInsured(int CustomerID, int ApplicantID)
        {
            string strStoredProc = "Proc_GetPolicy_NameInsured";
            DataSet dsTempXML;
            int result = 0;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@APPLICANT_ID", ApplicantID);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@result", SqlDbType.VarChar, ParameterDirection.Output);
                objSqlParameter.Size = 2;
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProc);
                result = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return result;
            }

            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
            }
        }

        public string GetNameInsuredPolicyNumber(int CustomerID, int ApplicantID)
        {
            string strStoredProc = "Proc_GetNameInsuredPolicies";
            string LobName = "";
            DataSet dsTemp;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@APPLICANT_ID", ApplicantID);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);

                dsTemp = new DataSet();
                dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                string returnResult = "";
                if (dsTemp != null)
                {
                    if (dsTemp.Tables[0].Rows.Count > 0)
                    {

                        for (int count = 0; count < dsTemp.Tables[0].Rows.Count; count++)
                        {
                            string status = dsTemp.Tables[0].Rows[count]["APP_STATUS"].ToString();
                            if (status == "Incomplete")
                            {
                                returnResult = "application -";
                            }
                            else
                            {
                                returnResult = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1929", "");// "policies -";
                            }
                            LobName = dsTemp.Tables[0].Rows[count][0].ToString();
                            returnResult += dsTemp.Tables[0].Rows[count][0].ToString();

                            if (count != (dsTemp.Tables[0].Rows.Count - 1))
                                returnResult += ", ";

                        }
                    }

                    else
                        returnResult = "";
                }
                else
                    returnResult = "";

                return returnResult;

            }

            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }
        //Added till here

        //Done for Itrack Issue 6761 on 27 Nov 09
        public static DataSet GetPolicy_PrimaryApplicant_NameInsured(int CustomerID, int PolicyId, int PolicyVersionId)
        {

            DataSet dsPrimaryNameInsured = new DataSet();

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId, SqlDbType.Int);
            dsPrimaryNameInsured = objDataWrapper.ExecuteDataSet("Proc_GetPolicy_PrimaryApplicant_NameInsured");

            return dsPrimaryNameInsured;
        }
        public static DataSet GetAplicantDetails(int APPLICANT_ID)
        {
            string strSql = "Proc_GetXmlCLT_APPLICANT_LIST";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@APPLICANT_ID", APPLICANT_ID);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet;
        }

        public string ActivateDeactivateApplicant(string strCode, string isActive, string strCustomInfo,int intUserId,int intCustomerId)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@CODE", strCode);
                objDataWrapper.AddParameter("@IS_ACTIVE", isActive);
                SqlParameter objPaam = (SqlParameter)objDataWrapper.AddParameter("@RET_VAL", System.Data.DbType.Int16, System.Data.ParameterDirection.ReturnValue);

                //objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure,objTranasction);

                if (this.TransactionLogRequired)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransaction = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransaction.CLIENT_ID = intCustomerId; 
                    objTransaction.RECORDED_BY = intUserId;
                    if (isActive.Trim()=="N")
                    objTransaction.TRANS_TYPE_ID = 168;              //Co-applicant Deactivated
                    else if (isActive.Trim() == "Y")
                        objTransaction.TRANS_TYPE_ID = 167;          //Co-Applicant  Activated
                    objTransaction.CUSTOM_INFO = strCustomInfo;
                    objDataWrapper.ExecuteNonQuery(ACTIVATE_DEACTIVATE_PROC, objTransaction);
                }
                else
                {
                    objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure);
                }
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (objPaam != null)
                {
                    return (objPaam.Value.ToString());
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }

    }
}

