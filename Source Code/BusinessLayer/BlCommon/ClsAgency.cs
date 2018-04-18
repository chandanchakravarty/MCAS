/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date				: -	5/9/2005 4:55:32 PM
<End Date				: -	
<Description				: - 	dsfsd
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 08, 2005
<Purpose				: - transaction description modified

Test New VSS
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Maintenance;
using Cms.DataLayer;
using System.Web.UI.WebControls;
namespace Cms.BusinessLayer.BlCommon
{
    /// <summary>
    /// 
    /// </summary>
    public class ClsAgency : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
    {
        private const	string		MNT_AGENCY_LIST			=	"MNT_AGENCY_LIST";

        #region Private Instance Variables
        private			bool		boolTransactionLog;
        // private int _AGENCY_ID;
        private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAgency";
        #endregion

        #region Public Properties
        public bool TransactionLog
        {
            set
            {
                boolTransactionLog	=	value;
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
        public ClsAgency()
        {
            boolTransactionLog	= base.TransactionLogRequired;
            base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
        }
        #endregion

        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="ObjAgencyInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int Add(ClsAgencyInfo ObjAgencyInfo)
        {
            string		strStoredProc	=	"Proc_InsertAgency";
            DateTime	RecordDate		=	DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@AGENCY_CODE",ObjAgencyInfo.AGENCY_CODE);
				objDataWrapper.AddParameter("@AGENCY_COMBINED_CODE",ObjAgencyInfo.AGENCY_COMBINED_CODE);
				objDataWrapper.AddParameter("@AGENCY_DISPLAY_NAME",ObjAgencyInfo.AGENCY_DISPLAY_NAME);
				objDataWrapper.AddParameter("@AGENCY_DBA",ObjAgencyInfo.AGENCY_DBA);
				objDataWrapper.AddParameter("@AGENCY_LIC_NUM",ObjAgencyInfo.AGENCY_LIC_NUM);
				objDataWrapper.AddParameter("@AGENCY_ADD1",ObjAgencyInfo.AGENCY_ADD1);
				objDataWrapper.AddParameter("@AGENCY_ADD2",ObjAgencyInfo.AGENCY_ADD2);
				objDataWrapper.AddParameter("@AGENCY_CITY",ObjAgencyInfo.AGENCY_CITY);
				objDataWrapper.AddParameter("@AGENCY_STATE",ObjAgencyInfo.AGENCY_STATE);
				objDataWrapper.AddParameter("@AGENCY_ZIP",ObjAgencyInfo.AGENCY_ZIP);
                objDataWrapper.AddParameter("@AGENCY_COUNTRY",ObjAgencyInfo.AGENCY_COUNTRY);
				objDataWrapper.AddParameter("@AGENCY_PHONE",ObjAgencyInfo.AGENCY_PHONE);
				objDataWrapper.AddParameter("@AGENCY_EXT",ObjAgencyInfo.AGENCY_EXT);
				objDataWrapper.AddParameter("@AGENCY_FAX",ObjAgencyInfo.AGENCY_FAX);
				objDataWrapper.AddParameter("@AGENCY_SPEED_DIAL",ObjAgencyInfo.AGENCY_SPEED_DIAL);
				objDataWrapper.AddParameter("@AGENCY_EMAIL",ObjAgencyInfo.AGENCY_EMAIL);
				objDataWrapper.AddParameter("@AGENCY_WEBSITE",ObjAgencyInfo.AGENCY_WEBSITE);
				objDataWrapper.AddParameter("@AGENCY_PAYMENT_METHOD",0);     //NET in all cases          
				objDataWrapper.AddParameter("@AGENCY_BILL_TYPE",ObjAgencyInfo.AGENCY_BILL_TYPE);                                
				objDataWrapper.AddParameter("@CREATED_BY",ObjAgencyInfo.CREATED_BY);

                if (ObjAgencyInfo.CREATED_DATETIME != DateTime.MinValue)
                    objDataWrapper.AddParameter("@CREATED_DATETIME", ObjAgencyInfo.CREATED_DATETIME);
                else
                    objDataWrapper.AddParameter("@CREATED_DATETIME", System.DBNull.Value); 

				
				objDataWrapper.AddParameter("@PRINCIPAL_CONTACT",ObjAgencyInfo.PRINCIPAL_CONTACT);
				objDataWrapper.AddParameter("@OTHER_CONTACT",ObjAgencyInfo.OTHER_CONTACT);
				objDataWrapper.AddParameter("@FEDERAL_ID",ObjAgencyInfo.FEDERAL_ID);
			    objDataWrapper.AddParameter("@PROCESS_1099",ObjAgencyInfo.PROCESS_1099);
                objDataWrapper.AddParameter("@BROKER_TYPE", ObjAgencyInfo.BROKER_TYPE);
				//Added By Raghav for Special Handling Itrack Issue 4810
				objDataWrapper.AddParameter("@REQ_SPECIAL_HANDLING",ObjAgencyInfo.REQ_SPECIAL_HANDLING);
				//////////////////////////////////////////
				objDataWrapper.AddParameter("@ALLOWS_EFT",ObjAgencyInfo.ALLOWS_EFT);
				objDataWrapper.AddParameter("@ALLOWS_CUSTOMER_SWEEP",ObjAgencyInfo.ALLOWS_CUSTOMER_SWEEP);
                //Addedm By Pradeep Kushwaha
                objDataWrapper.AddParameter("@SUSEP_NUMBER", ObjAgencyInfo.SUSEP_NUMBER);
                objDataWrapper.AddParameter("@BROKER_CURRENCY", ObjAgencyInfo.BROKER_CURRENCY);
                objDataWrapper.AddParameter("@DISTRICT", ObjAgencyInfo.DISTRICT);
                objDataWrapper.AddParameter("@NUMBER", ObjAgencyInfo.NUMBER);
                objDataWrapper.AddParameter("@BROKER_CPF_CNPJ", ObjAgencyInfo.BROKER_CPF_CNPJ);
               // objDataWrapper.AddParameter("@BROKER_DATE_OF_BIRTH", ObjAgencyInfo.BROKER_DATE_OF_BIRTH);
                if (ObjAgencyInfo.BROKER_DATE_OF_BIRTH != DateTime.MinValue)
                    objDataWrapper.AddParameter("@BROKER_DATE_OF_BIRTH", ObjAgencyInfo.BROKER_DATE_OF_BIRTH);
                else
                    objDataWrapper.AddParameter("@BROKER_DATE_OF_BIRTH", System.DBNull.Value); 
                objDataWrapper.AddParameter("@BROKER_REGIONAL_ID", ObjAgencyInfo.BROKER_REGIONAL_ID);
                objDataWrapper.AddParameter("@REGIONAL_ID_ISSUANCE", ObjAgencyInfo.REGIONAL_ID_ISSUANCE);
               
                if(ObjAgencyInfo.REGIONAL_ID_ISSUE_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@REGIONAL_ID_ISSUE_DATE",ObjAgencyInfo.REGIONAL_ID_ISSUE_DATE);                
				else
					objDataWrapper.AddParameter("@REGIONAL_ID_ISSUE_DATE",System.DBNull.Value);    
                objDataWrapper.AddParameter("@BROKER_BANK_NUMBER", ObjAgencyInfo.BROKER_BANK_NUMBER);
                objDataWrapper.AddParameter("@MARITAL_STATUS", ObjAgencyInfo.MARITAL_STATUS);
                objDataWrapper.AddParameter("@GENDER", ObjAgencyInfo.GENDER);

                //
				if(ObjAgencyInfo.ALLOWS_EFT==int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()))
					objDataWrapper.AddParameter("@ACCOUNT_TYPE",null);
				else
					objDataWrapper.AddParameter("@ACCOUNT_TYPE",ObjAgencyInfo.ACCOUNT_TYPE);
				
                				
				//////////////////////////////////////////////Customer Sweep				

				if(ObjAgencyInfo.ALLOWS_CUSTOMER_SWEEP==int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()))
					objDataWrapper.AddParameter("@ACCOUNT_TYPE_2",null);
				else
					objDataWrapper.AddParameter("@ACCOUNT_TYPE_2",ObjAgencyInfo.ACCOUNT_TYPE_2);



				if(ObjAgencyInfo.ORIGINAL_CONTRACT_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@ORIGINAL_CONTRACT_DATE",ObjAgencyInfo.ORIGINAL_CONTRACT_DATE);                
				else
					objDataWrapper.AddParameter("@ORIGINAL_CONTRACT_DATE",System.DBNull.Value);                
				
				
				//objDataWrapper.AddParameter("@UNDERWRITER_ASSIGNED_AGENCY",ObjAgencyInfo.UNDERWRITER_ASSIGNED_AGENCY);                                
				objDataWrapper.AddParameter("@BANK_ACCOUNT_NUMBER",ObjAgencyInfo.BANK_ACCOUNT_NUMBER);
				objDataWrapper.AddParameter("@ROUTING_NUMBER",ObjAgencyInfo.ROUTING_NUMBER);
				objDataWrapper.AddParameter("@BANK_ACCOUNT_NUMBER1",ObjAgencyInfo.BANK_ACCOUNT_NUMBER1);
				objDataWrapper.AddParameter("@ROUTING_NUMBER1",ObjAgencyInfo.ROUTING_NUMBER1);
				
				objDataWrapper.AddParameter("@BANK_NAME",ObjAgencyInfo.BANK_NAME);
				objDataWrapper.AddParameter("@BANK_BRANCH",ObjAgencyInfo.BANK_BRANCH);

				//add By Kranti 
				objDataWrapper.AddParameter("@BANK_NAME_2",ObjAgencyInfo.BANK_NAME_2);
				objDataWrapper.AddParameter("@BANK_BRANCH_2",ObjAgencyInfo.BANK_BRANCH_2);


				objDataWrapper.AddParameter("@M_AGENCY_ADD_1",ObjAgencyInfo.M_AGENCY_ADD_1);
				objDataWrapper.AddParameter("@M_AGENCY_ADD_2",ObjAgencyInfo.M_AGENCY_ADD_2);
				objDataWrapper.AddParameter("@M_AGENCY_CITY",ObjAgencyInfo.M_AGENCY_CITY);
				objDataWrapper.AddParameter("@M_AGENCY_STATE",ObjAgencyInfo.M_AGENCY_STATE);
				objDataWrapper.AddParameter("@M_AGENCY_ZIP",ObjAgencyInfo.M_AGENCY_ZIP);
				objDataWrapper.AddParameter("@M_AGENCY_COUNTRY",ObjAgencyInfo.M_AGENCY_COUNTRY);
				//objDataWrapper.AddParameter("@M_AGENCY_PHONE",ObjAgencyInfo.M_AGENCY_PHONE);
				//objDataWrapper.AddParameter("@M_AGENCY_EXT",ObjAgencyInfo.M_AGENCY_EXT);
				//objDataWrapper.AddParameter("@M_AGENCY_FAX",ObjAgencyInfo.M_AGENCY_FAX);
                objDataWrapper.AddParameter("@AGENCY_TYPE_ID", ObjAgencyInfo.AGENCY_TYPE_ID);

				if(ObjAgencyInfo.TERMINATION_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@TERMINATION_DATE",ObjAgencyInfo.TERMINATION_DATE);                
				else
					objDataWrapper.AddParameter("@TERMINATION_DATE",System.DBNull.Value);    
				objDataWrapper.AddParameter("@TERMINATION_REASON",ObjAgencyInfo.TERMINATION_REASON);
				objDataWrapper.AddParameter("@NOTES",ObjAgencyInfo.NOTES);
				if(ObjAgencyInfo.NUM_AGENCY_CODE == 0)
				{objDataWrapper.AddParameter("@NUM_AGENCY_CODE",null);}
				else
				{objDataWrapper.AddParameter("@NUM_AGENCY_CODE",ObjAgencyInfo.NUM_AGENCY_CODE);}

				objDataWrapper.AddParameter("@AgencyName",ObjAgencyInfo.AgencyName);
				if(ObjAgencyInfo.CURRENT_CONTRACT_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@CURRENT_CONTRACT_DATE",ObjAgencyInfo.CURRENT_CONTRACT_DATE);
				else
					objDataWrapper.AddParameter("@CURRENT_CONTRACT_DATE",System.DBNull.Value);  
			//added by pravesh	
				if(ObjAgencyInfo.TERMINATION_DATE_RENEW!=DateTime.MinValue)
					objDataWrapper.AddParameter("@TERMINATION_DATE_RENEW",ObjAgencyInfo.TERMINATION_DATE_RENEW);                
				else
					objDataWrapper.AddParameter("@TERMINATION_DATE_RENEW",System.DBNull.Value);
    			objDataWrapper.AddParameter("@TERMINATION_NOTICE",ObjAgencyInfo.TERMINATION_NOTICE);  
				objDataWrapper.AddParameter("@INCORPORATED_LICENSE",ObjAgencyInfo.INCORPORATED_LICENSE); 
			//end here			
				objDataWrapper.AddParameter("@REVERIFIED_AC1",ObjAgencyInfo.REVERIFIED_AC1); 
				objDataWrapper.AddParameter("@REVERIFIED_AC2",ObjAgencyInfo.REVERIFIED_AC2); 

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@AGENCY_ID",ObjAgencyInfo.AGENCY_ID,SqlDbType.Int,ParameterDirection.Output);
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					ObjAgencyInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/addAgency.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(ObjAgencyInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	ObjAgencyInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1516", "");// "New agency is added";				
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	";Agency Name = " + ObjAgencyInfo.AGENCY_DISPLAY_NAME + ";Agency Code = " + ObjAgencyInfo.AGENCY_CODE;
					
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int AGENCY_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if(AGENCY_ID < 0 )
					return AGENCY_ID;
				else
				{
					ObjAgencyInfo.AGENCY_ID = AGENCY_ID;
					return returnResult;
				}
			}
            catch(Exception ex)
            {
                throw(ex);
            }
            finally
            {
                if(objDataWrapper != null) objDataWrapper.Dispose();
            }
        }
        #endregion

        #region Update method
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldAgencyInfo">Model object having old information</param>
        /// <param name="ObjAgencyInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        public int Update(ClsAgencyInfo objOldAgencyInfo,ClsAgencyInfo ObjAgencyInfo)
        {
            string strTranXML;
            int returnResult = 0;
            string strStoredProc="Proc_UpdateAgency";


            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            try 
            {
                objDataWrapper.AddParameter("@AGENCY_ID",ObjAgencyInfo.AGENCY_ID);
                objDataWrapper.AddParameter("@AGENCY_CODE",ObjAgencyInfo.AGENCY_CODE);
				objDataWrapper.AddParameter("@AGENCY_COMBINED_CODE",ObjAgencyInfo.AGENCY_COMBINED_CODE);
				objDataWrapper.AddParameter("@AGENCY_DISPLAY_NAME",ObjAgencyInfo.AGENCY_DISPLAY_NAME);
                objDataWrapper.AddParameter("@AGENCY_DBA",ObjAgencyInfo.AGENCY_DBA);
                objDataWrapper.AddParameter("@AGENCY_LIC_NUM",ObjAgencyInfo.AGENCY_LIC_NUM);
                objDataWrapper.AddParameter("@AGENCY_ADD1",ObjAgencyInfo.AGENCY_ADD1);
                objDataWrapper.AddParameter("@AGENCY_ADD2",ObjAgencyInfo.AGENCY_ADD2);
                objDataWrapper.AddParameter("@AGENCY_CITY",ObjAgencyInfo.AGENCY_CITY);
                objDataWrapper.AddParameter("@AGENCY_STATE",ObjAgencyInfo.AGENCY_STATE);
                objDataWrapper.AddParameter("@AGENCY_ZIP",ObjAgencyInfo.AGENCY_ZIP);
                objDataWrapper.AddParameter("@AGENCY_COUNTRY",ObjAgencyInfo.AGENCY_COUNTRY);
                objDataWrapper.AddParameter("@AGENCY_PHONE",ObjAgencyInfo.AGENCY_PHONE);
                objDataWrapper.AddParameter("@AGENCY_EXT",ObjAgencyInfo.AGENCY_EXT);
                objDataWrapper.AddParameter("@AGENCY_FAX",ObjAgencyInfo.AGENCY_FAX);
				objDataWrapper.AddParameter("@AGENCY_SPEED_DIAL",ObjAgencyInfo.AGENCY_SPEED_DIAL);
                objDataWrapper.AddParameter("@AGENCY_EMAIL",ObjAgencyInfo.AGENCY_EMAIL);
                objDataWrapper.AddParameter("@AGENCY_WEBSITE",ObjAgencyInfo.AGENCY_WEBSITE);
                objDataWrapper.AddParameter("@AGENCY_PAYMENT_METHOD",ObjAgencyInfo.AGENCY_PAYMENT_METHOD);                
                objDataWrapper.AddParameter("@AGENCY_BILL_TYPE",ObjAgencyInfo.AGENCY_BILL_TYPE);
                objDataWrapper.AddParameter("@MODIFIED_BY",ObjAgencyInfo.MODIFIED_BY);

                if (ObjAgencyInfo.LAST_UPDATED_DATETIME != DateTime.MinValue)
                    objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", ObjAgencyInfo.LAST_UPDATED_DATETIME);     
                else
                    objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", System.DBNull.Value); 

                         
				//Added By Raghav for Special Handling Itrack Issue 4810
				objDataWrapper.AddParameter("@REQ_SPECIAL_HANDLING",ObjAgencyInfo.REQ_SPECIAL_HANDLING);
				objDataWrapper.AddParameter("@PRINCIPAL_CONTACT",ObjAgencyInfo.PRINCIPAL_CONTACT);
				objDataWrapper.AddParameter("@OTHER_CONTACT",ObjAgencyInfo.OTHER_CONTACT);
				objDataWrapper.AddParameter("@FEDERAL_ID",ObjAgencyInfo.FEDERAL_ID);

				objDataWrapper.AddParameter("@ALLOWS_EFT",ObjAgencyInfo.ALLOWS_EFT);
				objDataWrapper.AddParameter("@ALLOWS_CUSTOMER_SWEEP",ObjAgencyInfo.ALLOWS_CUSTOMER_SWEEP);
				objDataWrapper.AddParameter("@PROCESS_1099",ObjAgencyInfo.PROCESS_1099);
                objDataWrapper.AddParameter("@AGENCY_TYPE_ID", ObjAgencyInfo.AGENCY_TYPE_ID);
                objDataWrapper.AddParameter("@BROKER_TYPE", ObjAgencyInfo.BROKER_TYPE);
                objDataWrapper.AddParameter("@DISTRICT", ObjAgencyInfo.DISTRICT);
                objDataWrapper.AddParameter("@NUMBER", ObjAgencyInfo.NUMBER);
                objDataWrapper.AddParameter("@BROKER_CPF_CNPJ", ObjAgencyInfo.BROKER_CPF_CNPJ);
                //objDataWrapper.AddParameter("@BROKER_DATE_OF_BIRTH", ObjAgencyInfo.BROKER_DATE_OF_BIRTH);
                if (ObjAgencyInfo.BROKER_DATE_OF_BIRTH != DateTime.MinValue)
                    objDataWrapper.AddParameter("@BROKER_DATE_OF_BIRTH", ObjAgencyInfo.BROKER_DATE_OF_BIRTH);
                else
                    objDataWrapper.AddParameter("@BROKER_DATE_OF_BIRTH", System.DBNull.Value); 
                objDataWrapper.AddParameter("@BROKER_REGIONAL_ID", ObjAgencyInfo.BROKER_REGIONAL_ID);
                objDataWrapper.AddParameter("@REGIONAL_ID_ISSUANCE", ObjAgencyInfo.REGIONAL_ID_ISSUANCE);
              //  objDataWrapper.AddParameter("@REGIONAL_ID_ISSUE_DATE", ObjAgencyInfo.REGIONAL_ID_ISSUE_DATE);
                if (ObjAgencyInfo.REGIONAL_ID_ISSUE_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@REGIONAL_ID_ISSUE_DATE", ObjAgencyInfo.REGIONAL_ID_ISSUE_DATE);
                else
                    objDataWrapper.AddParameter("@REGIONAL_ID_ISSUE_DATE", System.DBNull.Value); 
                objDataWrapper.AddParameter("@BROKER_BANK_NUMBER", ObjAgencyInfo.BROKER_BANK_NUMBER);
                objDataWrapper.AddParameter("@MARITAL_STATUS", ObjAgencyInfo.MARITAL_STATUS);
                objDataWrapper.AddParameter("@GENDER", ObjAgencyInfo.GENDER);



				if(ObjAgencyInfo.ALLOWS_EFT==int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()))
					objDataWrapper.AddParameter("@ACCOUNT_TYPE",null);
				else
					objDataWrapper.AddParameter("@ACCOUNT_TYPE",ObjAgencyInfo.ACCOUNT_TYPE);
				


				if(ObjAgencyInfo.ALLOWS_CUSTOMER_SWEEP==int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()))
					objDataWrapper.AddParameter("@ACCOUNT_TYPE_2",null);
				else
					objDataWrapper.AddParameter("@ACCOUNT_TYPE_2",ObjAgencyInfo.ACCOUNT_TYPE_2);
				
                				
				//////////////////////////////////////////////Customer Sweep //Itrack #4098
				

				/*if(ObjAgencyInfo.ALLOWS_CUSTOMER_SWEEP==int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()))
				{
					objDataWrapper.AddParameter("@ACCOUNT_TYPE_2",null);
				}
				else
				{
					objDataWrapper.AddParameter("@ACCOUNT_TYPE",ObjAgencyInfo.ACCOUNT_TYPE);
				}*/

				//Itrack #4098
				if(ObjAgencyInfo.ORIGINAL_CONTRACT_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@ORIGINAL_CONTRACT_DATE",ObjAgencyInfo.ORIGINAL_CONTRACT_DATE);                
				else
					objDataWrapper.AddParameter("@ORIGINAL_CONTRACT_DATE",System.DBNull.Value);   
                
				if(ObjAgencyInfo.CURRENT_CONTRACT_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@CURRENT_CONTRACT_DATE",ObjAgencyInfo.CURRENT_CONTRACT_DATE);                
				else
					objDataWrapper.AddParameter("@CURRENT_CONTRACT_DATE",System.DBNull.Value);   
				
				//objDataWrapper.AddParameter("@UNDERWRITER_ASSIGNED_AGENCY",ObjAgencyInfo.UNDERWRITER_ASSIGNED_AGENCY);                                
				
				if(ObjAgencyInfo.ALLOWS_EFT==10964)
				{
					objDataWrapper.AddParameter("@BANK_ACCOUNT_NUMBER",null);
					//objDataWrapper.AddParameter("@BANK_ACCOUNT_NUMBER1",null);

					objDataWrapper.AddParameter("@ROUTING_NUMBER",null);
					///objDataWrapper.AddParameter("@ROUTING_NUMBER1",null);

					objDataWrapper.AddParameter("@BANK_NAME",null);
					//objDataWrapper.AddParameter("@BANK_NAME_2",null);

					objDataWrapper.AddParameter("@BANK_BRANCH",null);
					//objDataWrapper.AddParameter("@BANK_BRANCH_2",null);

				}
				else
				{
					objDataWrapper.AddParameter("@BANK_ACCOUNT_NUMBER",ObjAgencyInfo.BANK_ACCOUNT_NUMBER);
					//objDataWrapper.AddParameter("@BANK_ACCOUNT_NUMBER1",ObjAgencyInfo.BANK_ACCOUNT_NUMBER1);

					objDataWrapper.AddParameter("@ROUTING_NUMBER",ObjAgencyInfo.ROUTING_NUMBER);
					//objDataWrapper.AddParameter("@ROUTING_NUMBER1",ObjAgencyInfo.ROUTING_NUMBER1);

					objDataWrapper.AddParameter("@BANK_NAME",ObjAgencyInfo.BANK_NAME);
					//objDataWrapper.AddParameter("@BANK_NAME_2",ObjAgencyInfo.BANK_NAME_2);

					objDataWrapper.AddParameter("@BANK_BRANCH",ObjAgencyInfo.BANK_BRANCH);
					//objDataWrapper.AddParameter("@BANK_BRANCH_2",ObjAgencyInfo.BANK_BRANCH_2);
				}

				//#4098 Customer Sweep
				if(ObjAgencyInfo.ALLOWS_CUSTOMER_SWEEP==10964)
				{
					//objDataWrapper.AddParameter("@BANK_ACCOUNT_NUMBER",null);
					objDataWrapper.AddParameter("@BANK_ACCOUNT_NUMBER1",null);

					//objDataWrapper.AddParameter("@ROUTING_NUMBER",null);
					objDataWrapper.AddParameter("@ROUTING_NUMBER1",null);

					//objDataWrapper.AddParameter("@BANK_NAME",null);
					objDataWrapper.AddParameter("@BANK_NAME_2",null);

					//objDataWrapper.AddParameter("@BANK_BRANCH",null);
					objDataWrapper.AddParameter("@BANK_BRANCH_2",null);

				}
				else
				{
					//objDataWrapper.AddParameter("@BANK_ACCOUNT_NUMBER",ObjAgencyInfo.BANK_ACCOUNT_NUMBER);
					objDataWrapper.AddParameter("@BANK_ACCOUNT_NUMBER1",ObjAgencyInfo.BANK_ACCOUNT_NUMBER1);

					//objDataWrapper.AddParameter("@ROUTING_NUMBER",ObjAgencyInfo.ROUTING_NUMBER);
					objDataWrapper.AddParameter("@ROUTING_NUMBER1",ObjAgencyInfo.ROUTING_NUMBER1);

					//objDataWrapper.AddParameter("@BANK_NAME",ObjAgencyInfo.BANK_NAME);
					objDataWrapper.AddParameter("@BANK_NAME_2",ObjAgencyInfo.BANK_NAME_2);

					//objDataWrapper.AddParameter("@BANK_BRANCH",ObjAgencyInfo.BANK_BRANCH);
					objDataWrapper.AddParameter("@BANK_BRANCH_2",ObjAgencyInfo.BANK_BRANCH_2);
				
				}
				//Customer SweepEnd



				objDataWrapper.AddParameter("@M_AGENCY_ADD_1",ObjAgencyInfo.M_AGENCY_ADD_1);
				objDataWrapper.AddParameter("@M_AGENCY_ADD_2",ObjAgencyInfo.M_AGENCY_ADD_2);
				objDataWrapper.AddParameter("@M_AGENCY_CITY",ObjAgencyInfo.M_AGENCY_CITY);
				objDataWrapper.AddParameter("@M_AGENCY_STATE",ObjAgencyInfo.M_AGENCY_STATE);
				objDataWrapper.AddParameter("@M_AGENCY_ZIP",ObjAgencyInfo.M_AGENCY_ZIP);
				objDataWrapper.AddParameter("@M_AGENCY_COUNTRY",ObjAgencyInfo.M_AGENCY_COUNTRY);
				objDataWrapper.AddParameter("@AgencyName",ObjAgencyInfo.AgencyName);
			//	objDataWrapper.AddParameter("@M_AGENCY_PHONE",ObjAgencyInfo.M_AGENCY_PHONE);
			//	objDataWrapper.AddParameter("@M_AGENCY_EXT",ObjAgencyInfo.M_AGENCY_EXT);
			//	objDataWrapper.AddParameter("@M_AGENCY_FAX",ObjAgencyInfo.M_AGENCY_FAX);
				//objDataWrapper.AddParameter("@ACCOUNT_TYPE",ObjAgencyInfo.ACCOUNT_TYPE);

				
				if(ObjAgencyInfo.TERMINATION_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@TERMINATION_DATE",ObjAgencyInfo.TERMINATION_DATE);                
				else
					objDataWrapper.AddParameter("@TERMINATION_DATE",System.DBNull.Value);  
	
				objDataWrapper.AddParameter("@TERMINATION_REASON",ObjAgencyInfo.TERMINATION_REASON);
				objDataWrapper.AddParameter("@NOTES",ObjAgencyInfo.NOTES);
				
				if(ObjAgencyInfo.NUM_AGENCY_CODE == 0)
				{objDataWrapper.AddParameter("@NUM_AGENCY_CODE",null);}
				else
				{objDataWrapper.AddParameter("@NUM_AGENCY_CODE",ObjAgencyInfo.NUM_AGENCY_CODE);}
				//added by pravesh	
				if(ObjAgencyInfo.TERMINATION_DATE_RENEW!=DateTime.MinValue)
					objDataWrapper.AddParameter("@TERMINATION_DATE_RENEW",ObjAgencyInfo.TERMINATION_DATE_RENEW);                
				else
					objDataWrapper.AddParameter("@TERMINATION_DATE_RENEW",System.DBNull.Value);
				objDataWrapper.AddParameter("@TERMINATION_NOTICE",ObjAgencyInfo.TERMINATION_NOTICE);   
 
				objDataWrapper.AddParameter("@INCORPORATED_LICENSE",ObjAgencyInfo.INCORPORATED_LICENSE);
				objDataWrapper.AddParameter("@REVERIFIED_AC1",ObjAgencyInfo.REVERIFIED_AC1);
				objDataWrapper.AddParameter("@REVERIFIED_AC2",ObjAgencyInfo.REVERIFIED_AC2);
                objDataWrapper.AddParameter("@SUSEP_NUMBER", ObjAgencyInfo.SUSEP_NUMBER);
                objDataWrapper.AddParameter("@BROKER_CURRENCY", ObjAgencyInfo.BROKER_CURRENCY);

				//end here			
			
				SqlParameter objRetParam = (SqlParameter) objDataWrapper.AddParameter("@RETVAL",System.Data.DbType.Int16,ParameterDirection.ReturnValue);
				
				
                if(TransactionLogRequired) 
                {
              

                    ObjAgencyInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/addagency.aspx.resx");
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldAgencyInfo,ObjAgencyInfo);
                    objTransactionInfo.TRANS_TYPE_ID	=	3;
                    objTransactionInfo.RECORDED_BY		=	ObjAgencyInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1517", "");//"Agency is modified";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	";Agency Name = " + ObjAgencyInfo.AGENCY_DISPLAY_NAME + ";Agency Code = " + ObjAgencyInfo.AGENCY_CODE;
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
				if(int.Parse(objRetParam.Value.ToString())>0)
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
				}
				else
				{
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return int.Parse(objRetParam.Value.ToString()	);
				}
            }
            catch(Exception ex)
            {
                throw(ex);
            }
            finally
            {
                if(objDataWrapper != null) 
                {
                    objDataWrapper.Dispose();
                }
                if(objBuilder != null) 
                {
                    objBuilder = null;
                }
            }
        }
        #endregion

        #region "Fill Drop down Functions"
        public static void GetAgencyNamesInDropDown(DropDownList objDropDownList, string selectedValue)
        {
            DataTable  objDataTable = GetAgencyDataset().Tables[0];
            objDropDownList.Items.Clear();
            for(int i=0;i<objDataTable.DefaultView.Count;i++)
            {
                objDropDownList.Items.Add(new ListItem(objDataTable.DefaultView[i]["AGENCY_DISPLAY_NAME"].ToString(),objDataTable.DefaultView[i]["AGENCY_ID"].ToString()));
                if(selectedValue!=null && selectedValue.Length>0 && objDataTable.DefaultView[i]["AGENCY_ID"].ToString().Equals(selectedValue))
                    objDropDownList.SelectedIndex = i;
            }
        }
        //Get Agency Name data to bind coolite combox control - itrack - 1557
        public static void GetAgencyNamesInDropDown(ref DataTable dt)
        {
           dt = GetAgencyDataset().Tables[0];
        }
//		public static void GetAllAgencyNamesInDropDown( ref DropDownList objDropDownList,string AppEffectiveDate)
//		{
//			DateTime appEffDate=Convert.ToDateTime(AppEffectiveDate);
//			DataTable  objDataTable = GetAgencyDataset().Tables[0];
//			objDropDownList.Items.Clear();
//			for(int i=0;i<objDataTable.DefaultView.Count;i++)
//			{
//				DateTime appTerminateDate=Convert.ToDateTime(objDataTable.Rows[i]["TERMINATION_DATE_NEW"].ToString());
//				objDropDownList.Items.Add(new ListItem(objDataTable.DefaultView[i]["AGENCY_NAME_ACTIVE_STATUS"].ToString(),objDataTable.DefaultView[i]["AGENCY_ID"].ToString()));
//				if(appEffDate.Date > appTerminateDate.Date )
//					//objDropDownList.Items[i].Attributes.Add("style", "color:red");
//					//objDropDownList.Items[i].Attributes.CssStyle.Add("COLOR","Red");
//					//objDropDownList.Items[i].Attributes.Add("Class","GrandFatheredCoverage");
//					objDropDownList.Items[i].Attributes.Add("style", "Background-color:red");
//			}
//		}
		public static void GetAllAgencyNamesInDropDown(DropDownList objDropDownList)
		{
			DataTable  objDataTable = GetAgencyDataset().Tables[0];
			objDropDownList.Items.Clear();
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				objDropDownList.Items.Add(new ListItem(objDataTable.DefaultView[i]["AGENCY_NAME_ACTIVE_STATUS"].ToString(),objDataTable.DefaultView[i]["AGENCY_ID"].ToString()));
			}
		}
		/*Commented by Charles on 21-Aug-09 for APP/POL Optimization
		public static void GetAgencyWithStatusInDropDown(DropDownList objDropDownList, string AppEffectiveDate)
		{
			DataTable  objDataTable = GetAgencyWithStatus(AppEffectiveDate).Tables[0];
			objDropDownList.Items.Clear();
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				objDropDownList.Items.Add(new ListItem(objDataTable.DefaultView[i]["AGENCY_NAME_ACTIVE_STATUS"].ToString(),objDataTable.DefaultView[i]["AGENCY_ID"].ToString()));
			}			
		}*/

        public static void GetAgencyNamesInDropDown(DropDownList objDropDownList)
        {
            GetAgencyNamesInDropDown(objDropDownList,null);
        }
		public static string GetAgencyName(string agencyID)
		{
			return DataWrapper.ExecuteScalar(ConnStr,CommandType.Text,"select AGENCY_DISPLAY_NAME from MNT_AGENCY_LIST where AGENCY_ID="+agencyID).ToString();
		}
        #endregion

        #region FETCHING DATA / BILL TYPE
        public DataSet FetchData(int agencyId)
        {
            string		strStoredProc	=	"Proc_FetchAgencyInfo";
            DataSet dsCount=null;
           			
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@AGENCY_ID",agencyId,SqlDbType.Int);
                objDataWrapper.AddParameter("@LANG_ID", BL_LANG_ID);
                dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
            }
            catch(Exception ex)
            {
                throw(ex);
            }
            finally
            {
				
            }
            return dsCount;
        }

		//THIS FUNCTION WILL SET DEFAULT BILL TYPE IN DROP DOWN
		public static void GetBillTypeAgency(DropDownList objDropDownList,int agencyId,string calledFrom)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@AGENCY_ID",agencyId);
			objDataWrapper.AddParameter("@CALLED_FROM",calledFrom);
            objDataWrapper.AddParameter("@Lang_id", ClsCommon.BL_LANG_ID);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_GetBillTypeForAgency").Tables[0];
			objDropDownList.DataSource = objDataTable;
			objDropDownList.DataTextField = "LOOKUP_VALUE_DESC";
			objDropDownList.DataValueField = "LOOKUP_UNIQUE_ID";
			objDropDownList.DataBind();
		}

        #endregion

		public int Delete(int intAgencyId)
		{
			return Delete(null,intAgencyId);
		}

		public int Delete(ClsAgencyInfo ObjAgencyInfo,int intAgencyId)
		{
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string	strStoredProc =	"Proc_DeleteAgency";
			try 
			{
				objDataWrapper.AddParameter("@AGENCY_ID",intAgencyId);
				SqlParameter objRetVal = (SqlParameter)objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				int returnResult = 0;
				if(TransactionLogRequired) 
				{			

					ObjAgencyInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/addAgency.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(ObjAgencyInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	ObjAgencyInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1574", "");// "Agency is Deleted";				
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	";Agency Name = " + ObjAgencyInfo.AGENCY_DISPLAY_NAME + ";Agency Code = " + ObjAgencyInfo.AGENCY_CODE;
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				//			int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
			return int.Parse(objRetVal.Value.ToString());

			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) 
				{
					objDataWrapper.Dispose();
				}				
			}
		
		}

		public static string GetAgencyCodeFromID(int AgencyID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@AGENCY_ID",AgencyID,SqlDbType.Int,ParameterDirection.Input);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAgencyCode");
			
				return dsTemp.Tables[0].Rows[0][0].ToString();
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}
        public static int GetAgencyIDFromCode(string AgencyCode)
        {
            try
            {
                DataSet dsTemp = new DataSet();
			
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@AGENCY_CODE",AgencyCode,SqlDbType.NVarChar,ParameterDirection.Input , 16);

                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAgencyID");
			
                return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());
				 
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {}
        }	
		public static DataSet GetAgencyIDAndNameFromCode(string AgencyCode)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@AGENCY_CODE",AgencyCode,SqlDbType.NVarChar,ParameterDirection.Input , 16);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAgencyID");
			
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}
		
		/// <summary>
		/// Returns the Agency ID matching the parameters
		/// </summary>
		/// <param name="ObjAgencyInfo"></param>
		/// <param name="objDataWrapper"></param>
		/// <returns></returns>
		public static int GetAgencyID(ClsAgencyInfo ObjAgencyInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_GetAgencyIDFromInfo";
			
			objDataWrapper.AddParameter("@AGENCY_DISPLAY_NAME",ObjAgencyInfo.AGENCY_DISPLAY_NAME);
			SqlParameter sRetVal = (SqlParameter)objDataWrapper.AddParameter("@AGENCY_ID",SqlDbType.SmallInt,ParameterDirection.Output);

			objDataWrapper.ExecuteNonQuery(strStoredProc);
			
			//No agency with this name found
			if ( sRetVal.Value == DBNull.Value )
			{
				return -1;
			}

			int agencyID =  Convert.ToInt32(sRetVal.Value);

			return agencyID;
		}

		public DataSet FillAgency()
		{
			return GetAgencyDataset();
		}

		public static DataSet GetAgencyDataset()
		{
			string		strStoredProc	=	"Proc_GetAgencyInfo";//Will be replaced with CONST
					
			DataSet dsAccounts = new DataSet();

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LANG_ID", BL_LANG_ID);//Added by Charles on 24-May-2010 for Multilingual Support   
			dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			return dsAccounts;

		}
		public DataSet GetCustomerAgency()
		{
			return GetCustomerAgencyDataset();
		}
		public static DataSet GetInactiveAgencyDataset(string AppEffectiveDate)
		{
			string		strStoredProc	=	"PROC_GETAGENCYINFO_INACTIVE";//Will be replaced with CONST
					
			DataSet dsAccounts = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DateTime appEffDate=Convert.ToDateTime(AppEffectiveDate);
			objDataWrapper.AddParameter("@APP_EFFECTIVE_DATE",appEffDate);
			dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			return dsAccounts;

		}
        public static DataSet GetAgencyWithStatus(string AppEffectiveDate, int iCustomerId)
        {
            return GetAgencyWithStatus(AppEffectiveDate, iCustomerId,0);

        }
		public static DataSet GetAgencyWithStatus(string AppEffectiveDate, int iCustomerId,int agencyID)
		{
			string		strStoredProc	=	"PROC_GetAgencyWithStatus";
					
			DataSet dsAccounts = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
            //DateTime appEffDate = ClsCommon.ConvertToDate(AppEffectiveDate);
            objDataWrapper.AddParameter("@APP_EFFECTIVE_DATE", ClsCommon.ConvertToDate(AppEffectiveDate));//Added by Charles on 17-May-10
			objDataWrapper.AddParameter("@CUSTOMER_ID",iCustomerId);//Added by Charles on 21-Aug-09 for APP/POL OPTIMISATION
            if (agencyID!=0)
            objDataWrapper.AddParameter("@AGENCY_ID", agencyID);
			dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			return dsAccounts;

		}
		public static DataSet GetCustomerAgencyDataset()
		{
			 string		strStoredProc	=	"Proc_GetAgency_CustomerInfo";//Will be replaced with CONST
					
			DataSet dsAccounts = new DataSet();			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);			
			dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			return dsAccounts;

		}
        // common function for selecting assigned and unassigned marketers
        public DataSet PopulateAgency_MrkUW(int intAgencyId, int intLobId, string strselectType)
        {
            string strStoredProc = "Proc_SelectAgecny_MrkUW";
            DataSet dsReceive = null;


            Object[] objParam = new object[3];
            objParam[1] = intAgencyId;
            objParam[0] = intLobId;
            objParam[2] = strselectType;
            dsReceive = Cms.DataLayer.DataWrapper.ExecuteDataset(ConnStr, strStoredProc, objParam);
            return dsReceive;

        }


        // new class for unassigned marketers because table is now changed 
        public DataSet PopulateUnassigned_Marketeer(int intAgencyId, int intLobId)
        {
            string strStoredProc = "Proc_Select_UnAssignedMarketeer";
            DataSet dsReceive = null;


            Object[] objParam = new object[2];
            objParam[1] = intAgencyId;
            objParam[0] = intLobId;
            dsReceive = Cms.DataLayer.DataWrapper.ExecuteDataset(ConnStr, strStoredProc, objParam);
            return dsReceive;

        }

		public DataSet PopulateUnassignedMarketeer()
		{
			string	strStoredProc	=	"Proc_SelectMarketeer";
			DataSet dsReceive		=	null;

			try
			{				
				//Object[] objParam = new object[1];
				//objParam[0] = intUnderwriterId ;
				dsReceive = Cms.DataLayer.DataWrapper.ExecuteDataset(ConnStr,strStoredProc); 
				return dsReceive;
			}
			catch(Exception ex)
			{
				throw(ex);
			}				

		}

        // new class for unassigned Under writers because table is now changed 
        public DataSet PopulateUnassigned_Underwriter(int intAgencyId, int intLobId)
        {
            string strStoredProc = "Proc_Select_UnAssignedUnderwriter";
            DataSet dsReceive = null;


            Object[] objParam = new object[2];
            objParam[1] = intAgencyId;
            objParam[0] = intLobId;
            dsReceive = Cms.DataLayer.DataWrapper.ExecuteDataset(ConnStr, strStoredProc, objParam);
            return dsReceive;

        }

		public DataSet PopulateUnassignedUnderwriter()
		{
			string	strStoredProc	=	"Proc_SelectUnderwriter";
			DataSet dsReceive		=	null;

			try
			{				
				//Object[] objParam = new object[1];
				//objParam[0] = intUnderwriterId ;
				dsReceive = Cms.DataLayer.DataWrapper.ExecuteDataset( ConnStr,strStoredProc); 
				return dsReceive;
			}
			catch(Exception ex)
			{
				throw(ex);
			}	
		}

        public int Save(int intLobId, string strUnderWriters, int intAgencyId, string strMarketeers, string strUnderwriterNames, string strMarketeerNames, string strLobName)
		{
            string strStoredProc = "Proc_InsertAgecnyUnderwriter";			
			int returnResult = 0;			
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
                objDataWrapper.AddParameter("@intAgencyId",intAgencyId);
				objDataWrapper.AddParameter("@intLobId",intLobId);
				objDataWrapper.AddParameter("@strUnderWriters",strUnderWriters);
				objDataWrapper.AddParameter("@strMarketeer",strMarketeers);
                if (TransactionLogRequired)
                {                    
                    ClsTransactionInfo objTransactionInfo = new ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 231;
                    objTransactionInfo.TRANS_DESC = "";//"Agency UW/Marketing assigned";
                    objTransactionInfo.CUSTOM_INFO = ";Agency ID = " + intAgencyId + ";Product = " + strLobName + ";Underwriters = " + strUnderwriterNames + ";Marketing = " + strMarketeerNames;
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) 
				{
					objDataWrapper.Dispose();
				}				
			}
		}

        //Added by Ruchika Chauhan on 1-Feb-2012 for TFS Bug # 3108        
        public static string GetAgencyCode()
        {
            string strStoredProc = "PROC_GenerateIntermediaryCode";

            DataSet dsICode = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            dsICode = objDataWrapper.ExecuteDataSet(strStoredProc);

            return dsICode.Tables[0].Rows[0][0].ToString();
        }

		public DataSet PopulateassignedMarketeer(int intAgencyId, int intLobId)
		{
			string	strStoredProc	= "Proc_SelectAgecnyMARKETING";
			DataSet dsReceive		= null;

		
			Object[] objParam = new object[2];
			objParam[1] = intAgencyId ;
			objParam[0] = intLobId ;
			dsReceive = Cms.DataLayer.DataWrapper.ExecuteDataset( ConnStr,strStoredProc,objParam); 
			return dsReceive;
				
		}
		/// <returns>Return DataSet </returns>
		public DataSet PopulateassignedUnderWriter(int intAgencyId, int intLobId)
		{
			string	strStoredProc	= "Proc_SelectAgecnyUnderwriter";
			DataSet dsReceive		= null;

		
				Object[] objParam = new object[2];
				objParam[1] = intAgencyId ;
				objParam[0] = intLobId ;
				dsReceive = Cms.DataLayer.DataWrapper.ExecuteDataset( ConnStr,strStoredProc,objParam); 
				return dsReceive;
				
		}

		//Added by praveen kumar(1-04-2009):to get the agency name and agency code of a particular user
		public static DataSet GETUSER_AGENCYNAME_CODE(int user_id)
		{
			string strStoredProc = "PROC_GETUSER_AGENCYNAME_CODE";
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
				objWrapper.AddParameter("@USERID",user_id);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
				return ds;
			}
			catch(Exception exc)
			{
				throw(exc);
			}
		}
    }


    

	//Creating new business layer class for AgencyStateLobAssoc table
	public class ClsAgencyStateLobAssoc : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		MNT_AGENCY_STATE_LOB_ASSOC			=	"MNT_AGENCY_STATE_LOB_ASSOC";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		// private int _AGENCY_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAgency";
		#endregion

		#region Public Properties
		public bool TransactionLog
		{
			set
			{
				boolTransactionLog	=	value;
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
		public ClsAgencyStateLobAssoc()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objAgencyStateLobAssocInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsAgencyStateLobAssocInfo objAgencyStateLobAssocInfo, string strLOB_ID_TYPES,string strOldLOBCODE)
		{
			string		strStoredProc	=	"Proc_InsertMNT_AGENCY_STATE_LOB_ASSOC";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@AGENCY_ID",objAgencyStateLobAssocInfo.AGENCY_ID);
				objDataWrapper.AddParameter("@STATE_ID",objAgencyStateLobAssocInfo.STATE_ID);
				objDataWrapper.AddParameter("@LOB_ID_TYPES",strLOB_ID_TYPES);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objAgencyStateLobAssocInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@CREATED_BY",objAgencyStateLobAssocInfo.CREATED_BY);				
				
//				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@MNT_AGENCY_STATE_LOB_ASSOC_ID",objAgencyStateLobAssocInfo.MNT_AGENCY_STATE_LOB_ASSOC_ID,SqlDbType.Int,ParameterDirection.Output);
				string []lobID=new string[10];//Added by Sibin for Itrack Issue 5502 on 3 March 09
				lobID=strLOB_ID_TYPES.Split(',');//Added by Sibin for Itrack Issue 5502 on 3 March 09
				int returnResult = 0;
				string strLOBCODE="";//Added by Sibin for Itrack Issue 5502 on 3 March 09
				if(TransactionLogRequired)
				{
					objAgencyStateLobAssocInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AgencyStateLobAssoc.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objAgencyStateLobAssocInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objAgencyStateLobAssocInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1575", "");// "Agency communication saved successfully";
					
					//Added by Sibin for Itrack Issue 5502 on 3 March 09
					for(int i=0;i<lobID.Length;i++)
					{
						int j=i;
						if(lobID[i] == ((int)enumLOB.AUTOP).ToString())
						{
							if(++j!= lobID.Length)
							{
								strLOBCODE += "Automobile, ";
							}
							else
								strLOBCODE += "Automobile";
						}
						else if(lobID[i] == ((int)enumLOB.HOME).ToString())
						{
							if(++j!= lobID.Length)
							{
								strLOBCODE += "Homeowners, ";//Done for Itrack Issue 5502 on 28 April 2009
							}
							else
								strLOBCODE += "Homeowners";//Done for Itrack Issue 5502 on 28 April 2009
						}
						else if(lobID[i] == ((int)enumLOB.BOAT).ToString())
						{
							if(++j!= lobID.Length)
							{
								strLOBCODE += "Watercraft, ";
							}
							else
								strLOBCODE += "Watercraft";
						}
						else if(lobID[i] == ((int)enumLOB.CYCL).ToString())
						{
							if(++j!= lobID.Length)
							{
								strLOBCODE += "Motorcycle, ";
							}
							else
								strLOBCODE += "Motorcycle";
						}
						else if(lobID[i] == ((int)enumLOB.REDW).ToString())
						{
							if(++j!= lobID.Length)
							{
								strLOBCODE += "Rental, ";
							}
							else
								strLOBCODE += "Rental";
						}
						else if(lobID[i] == ((int)enumLOB.UMB).ToString())
						{
							if(++j!= lobID.Length)
							{
								strLOBCODE += "Umbrella, ";
							}
							else
								strLOBCODE += "Umbrella";
						}
						else if(lobID[i] == ((int)enumLOB.GENL).ToString())
						{
							if(++j!= lobID.Length)
							{
								strLOBCODE += "General Liability, ";
							}
							else
								strLOBCODE += "General Liability";
						}
					}
					strTranXML ="<LabelFieldMapping><Map label='State :' field='State :' OldValue='' NewValue='" + objAgencyStateLobAssocInfo.STATE_ID +"' /><Map label='LOB :' field='LOB_ID' OldValue='"+ strOldLOBCODE +"' NewValue='" + strLOBCODE +"' /></LabelFieldMapping>";
					//Added till here
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					
					//objTransactionInfo.CUSTOM_INFO		=	";Agency Name = " + objAgencyStateLobAssocInfo.AGENCY_DISPLAY_NAME + ";Agency Code = " + objAgencyStateLobAssocInfo.AGENCY_CODE;
					
					//Added by Sibin for Itrack Issue 5502 on 27 Feb 09
					string agencycode= ClsAgency.GetAgencyCodeFromID(objAgencyStateLobAssocInfo.AGENCY_ID);
					DataSet ds = null;
					ds = ClsAgency.GetAgencyIDAndNameFromCode(agencycode);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						if(ds.Tables[0].Rows[0]["AGENCY_DISP_NAME"]!=null && ds.Tables[0].Rows[0]["AGENCY_DISP_NAME"].ToString()!="")
							objTransactionInfo.CUSTOM_INFO 		=	";Agency Name = " + ds.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString() + ";Agency Code = " + agencycode;
					}
					//Added till here
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
//				int MNT_AGENCY_STATE_LOB_ASSOC_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();				
				if(returnResult>0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);

				return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		#endregion


		public DataSet GetMNT_AGENCY_STATE_LOB_ASSOC(int AGENCY_ID, int STATE_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@AGENCY_ID",AGENCY_ID);
            objWrapper.AddParameter("@STATE_ID",STATE_ID);
			DataSet ds = objWrapper.ExecuteDataSet("Proc_GetMNT_AGENCY_STATE_LOB_ASSOC");			
			if(ds!=null && ds.Tables.Count>0)
				return ds;
			else
				return null;
		}

        public DataSet GetMNT_AGENCY_COUNTRY_LOB_ASSOC(int AGENCY_ID, int COUNTRY_ID)
        {
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@AGENCY_ID", AGENCY_ID);
            objWrapper.AddParameter("@COUNTRY_ID", COUNTRY_ID);
            DataSet ds = objWrapper.ExecuteDataSet("Proc_GetMNT_AGENCY_Country_LOB_ASSOC");
            if (ds != null && ds.Tables.Count > 0)
                return ds;
            else
                return null;
        }

        
	}
}
