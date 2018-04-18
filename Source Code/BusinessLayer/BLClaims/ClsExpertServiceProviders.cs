using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Maintenance.Claims;
using Cms.BusinessLayer.BlCommon;


namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// Summary description for ClsExpertServiceProviders.
	/// </summary>
	public class ClsExpertServiceProviders : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		GetCLM_EXPERT_SERVICE_PROVIDERS			=	"Proc_GetCLM_EXPERT_SERVICE_PROVIDERS";
		private const	string		InsertCLM_EXPERT_SERVICE_PROVIDERS  	=	"Proc_InsertCLM_EXPERT_SERVICE_PROVIDERS";
		private const	string		UpdateCLM_EXPERT_SERVICE_PROVIDERS  	=	"Proc_UpdateCLM_EXPERT_SERVICE_PROVIDERS";
		private const	string		DeleteCLM_EXPERT_SERVICE_PROVIDERS  	=	"Proc_DeleteCLM_EXPERT_SERVICE_PROVIDERS";
		
		
		public ClsExpertServiceProviders()
		{
			base.strActivateDeactivateProcedure = "Proc_ActivateDeactivateCLM_EXPERT_SERVICE_PROVIDERS";
		}

		#region Add(Insert) functions
        public int Add(Cms.Model.Maintenance.Claims.ClsExpertServiceProvidersInfo objExpertServiceProvidersInfo, string XmlFullFilePath)
		{
            return Add(objExpertServiceProvidersInfo, null, XmlFullFilePath);
		}
        public int Add(Cms.Model.Maintenance.Claims.ClsExpertServiceProvidersInfo objExpertServiceProvidersInfo, DataWrapper objDataWrapper)
		{			
			DateTime	RecordDate		=	DateTime.Now;
			if(objDataWrapper==null)
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			try
			{	
				objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE",objExpertServiceProvidersInfo.EXPERT_SERVICE_TYPE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_NAME",objExpertServiceProvidersInfo.EXPERT_SERVICE_NAME);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_ADDRESS1",objExpertServiceProvidersInfo.EXPERT_SERVICE_ADDRESS1);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_ADDRESS2",objExpertServiceProvidersInfo.EXPERT_SERVICE_ADDRESS2);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CITY",objExpertServiceProvidersInfo.EXPERT_SERVICE_CITY);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_STATE",objExpertServiceProvidersInfo.EXPERT_SERVICE_STATE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_ZIP",objExpertServiceProvidersInfo.EXPERT_SERVICE_ZIP);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_VENDOR_CODE",objExpertServiceProvidersInfo.EXPERT_SERVICE_VENDOR_CODE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_NAME",objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_NAME);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_PHONE",objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_PHONE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_EMAIL",objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_EMAIL);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_FEDRERAL_ID",objExpertServiceProvidersInfo.EXPERT_SERVICE_FEDRERAL_ID);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_1099_PROCESSING_OPTION",objExpertServiceProvidersInfo.EXPERT_SERVICE_1099_PROCESSING_OPTION);
				objDataWrapper.AddParameter("@CREATED_BY",objExpertServiceProvidersInfo.CREATED_BY);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_MASTER_VENDOR_CODE",objExpertServiceProvidersInfo.EXPERT_SERVICE_MASTER_VENDOR_CODE);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objExpertServiceProvidersInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_COUNTRY",objExpertServiceProvidersInfo.EXPERT_SERVICE_COUNTRY);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE_DESC",objExpertServiceProvidersInfo.EXPERT_SERVICE_TYPE_DESC);
                objDataWrapper.AddParameter("@ACTIVITY", objExpertServiceProvidersInfo.ACTIVITY);
                if (objExpertServiceProvidersInfo.REG_ID_ISSUE_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE",objExpertServiceProvidersInfo.REG_ID_ISSUE_DATE);
                else
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE",null);
                if (objExpertServiceProvidersInfo.DATE_OF_BIRTH != DateTime.MinValue)
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH",objExpertServiceProvidersInfo.DATE_OF_BIRTH);
                else
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH",null);

                objDataWrapper.AddParameter("@REG_ID_ISSUE",objExpertServiceProvidersInfo.REG_ID_ISSUE);
                objDataWrapper.AddParameter("@CPF", objExpertServiceProvidersInfo.CPF);
                objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", objExpertServiceProvidersInfo.REGIONAL_IDENTIFICATION);


				objDataWrapper.AddParameter("@PARTY_DETAIL",objExpertServiceProvidersInfo.PARTY_DETAIL);
				if(objExpertServiceProvidersInfo.AGE!=0)
					objDataWrapper.AddParameter("@AGE",objExpertServiceProvidersInfo.AGE);
				else
					objDataWrapper.AddParameter("@AGE",System.DBNull.Value);
				//objDataWrapper.AddParameter("@AGE",objExpertServiceProvidersInfo.AGE);
				objDataWrapper.AddParameter("@EXTENT_OF_INJURY",objExpertServiceProvidersInfo.EXTENT_OF_INJURY);
				objDataWrapper.AddParameter("@OTHER_DETAILS",objExpertServiceProvidersInfo.OTHER_DETAILS);
				objDataWrapper.AddParameter("@BANK_NAME",objExpertServiceProvidersInfo.BANK_NAME);
				objDataWrapper.AddParameter("@ACCOUNT_NUMBER",objExpertServiceProvidersInfo.ACCOUNT_NUMBER);
				objDataWrapper.AddParameter("@ACCOUNT_NAME",objExpertServiceProvidersInfo.ACCOUNT_NAME);
				if(objExpertServiceProvidersInfo.PARENT_ADJUSTER!=0)
					objDataWrapper.AddParameter("@PARENT_ADJUSTER",objExpertServiceProvidersInfo.PARENT_ADJUSTER);
				else
					objDataWrapper.AddParameter("@PARENT_ADJUSTER",System.DBNull.Value);
				//objDataWrapper.AddParameter("@PARENT_ADJUSTER",objExpertServiceProvidersInfo.PARENT_ADJUSTER);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_PHONE_EXT",objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_PHONE_EXT);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_FAX",objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_FAX);
				
				//Added by Mohit Agarwal 20-Aug-2008
				objDataWrapper.AddParameter("@MAIL_1099_ADD1",objExpertServiceProvidersInfo.MAIL_1099_ADD1);
				objDataWrapper.AddParameter("@MAIL_1099_ADD2",objExpertServiceProvidersInfo.MAIL_1099_ADD2);
				objDataWrapper.AddParameter("@MAIL_1099_CITY",objExpertServiceProvidersInfo.MAIL_1099_CITY);
				objDataWrapper.AddParameter("@MAIL_1099_STATE",objExpertServiceProvidersInfo.MAIL_1099_STATE);
				objDataWrapper.AddParameter("@MAIL_1099_ZIP",objExpertServiceProvidersInfo.MAIL_1099_ZIP);
				objDataWrapper.AddParameter("@MAIL_1099_COUNTRY",objExpertServiceProvidersInfo.MAIL_1099_COUNTRY);

				objDataWrapper.AddParameter("@MAIL_1099_NAME",objExpertServiceProvidersInfo.MAIL_1099_NAME);
				objDataWrapper.AddParameter("@W9_FORM",objExpertServiceProvidersInfo.W9_FORM);
				//Added By Raghav for Itrack Issue 4810
				//objDataWrapper.AddParameter("@REQ_SPECIAL_HANDLING",objExpertServiceProvidersInfo.REQ_SPECIAL_HANDLING);
				//Added by Sibin on 22 Dec 08 for Itrack Issue 5216 
				if(objExpertServiceProvidersInfo.REQ_SPECIAL_HANDLING!=0)
					objDataWrapper.AddParameter("@REQ_SPECIAL_HANDLING",objExpertServiceProvidersInfo.REQ_SPECIAL_HANDLING);
				else
					objDataWrapper.AddParameter("@REQ_SPECIAL_HANDLING",System.DBNull.Value);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@EXPERT_SERVICE_ID",objExpertServiceProvidersInfo.EXPERT_SERVICE_ID,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlRetParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
                    objExpertServiceProvidersInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/claims/AddExpertServiceProviders.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objExpertServiceProvidersInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objExpertServiceProvidersInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Expert Service Providers has been added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(InsertCLM_EXPERT_SERVICE_PROVIDERS,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_EXPERT_SERVICE_PROVIDERS);
				

				int EXPERT_SERVICE_ID = 0;
				//if (returnResult>0)
				if(objSqlRetParameter!=null && objSqlRetParameter.Value.ToString()!="")
				{
					if(int.Parse(objSqlRetParameter.Value.ToString())>0)
					{
						EXPERT_SERVICE_ID = int.Parse(objSqlParameter.Value.ToString());					
						objExpertServiceProvidersInfo.EXPERT_SERVICE_ID = EXPERT_SERVICE_ID;
					}
					else
						returnResult =  int.Parse(objSqlRetParameter.Value.ToString());
				}
				else
					returnResult = -1;
				

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

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


         public int Add(Cms.Model.Maintenance.Claims.ClsExpertServiceProvidersInfo objExpertServiceProvidersInfo, DataWrapper objDataWrapper, string XmlFullFilePath)
        {
            DateTime RecordDate = DateTime.Now;
            if (objDataWrapper == null)
                objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            try
            {
                objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE", objExpertServiceProvidersInfo.EXPERT_SERVICE_TYPE);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_NAME", objExpertServiceProvidersInfo.EXPERT_SERVICE_NAME);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_ADDRESS1", objExpertServiceProvidersInfo.EXPERT_SERVICE_ADDRESS1);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_ADDRESS2", objExpertServiceProvidersInfo.EXPERT_SERVICE_ADDRESS2);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_CITY", objExpertServiceProvidersInfo.EXPERT_SERVICE_CITY);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_STATE", objExpertServiceProvidersInfo.EXPERT_SERVICE_STATE);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_ZIP", objExpertServiceProvidersInfo.EXPERT_SERVICE_ZIP);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_VENDOR_CODE", objExpertServiceProvidersInfo.EXPERT_SERVICE_VENDOR_CODE);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_NAME", objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_NAME);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_PHONE", objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_PHONE);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_EMAIL", objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_EMAIL);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_FEDRERAL_ID", objExpertServiceProvidersInfo.EXPERT_SERVICE_FEDRERAL_ID);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_1099_PROCESSING_OPTION", objExpertServiceProvidersInfo.EXPERT_SERVICE_1099_PROCESSING_OPTION);
                objDataWrapper.AddParameter("@CREATED_BY", objExpertServiceProvidersInfo.CREATED_BY);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_MASTER_VENDOR_CODE", objExpertServiceProvidersInfo.EXPERT_SERVICE_MASTER_VENDOR_CODE);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objExpertServiceProvidersInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_COUNTRY", objExpertServiceProvidersInfo.EXPERT_SERVICE_COUNTRY);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE_DESC", objExpertServiceProvidersInfo.EXPERT_SERVICE_TYPE_DESC);
                objDataWrapper.AddParameter("@ACTIVITY", objExpertServiceProvidersInfo.ACTIVITY);
                if (objExpertServiceProvidersInfo.REG_ID_ISSUE_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", objExpertServiceProvidersInfo.REG_ID_ISSUE_DATE);
                else
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", null);
                if (objExpertServiceProvidersInfo.DATE_OF_BIRTH != DateTime.MinValue)
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", objExpertServiceProvidersInfo.DATE_OF_BIRTH);
                else
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", null);

                objDataWrapper.AddParameter("@REG_ID_ISSUE", objExpertServiceProvidersInfo.REG_ID_ISSUE);
                objDataWrapper.AddParameter("@CPF", objExpertServiceProvidersInfo.CPF);
                objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", objExpertServiceProvidersInfo.REGIONAL_IDENTIFICATION);


                objDataWrapper.AddParameter("@PARTY_DETAIL", objExpertServiceProvidersInfo.PARTY_DETAIL);
                if (objExpertServiceProvidersInfo.AGE != 0)
                    objDataWrapper.AddParameter("@AGE", objExpertServiceProvidersInfo.AGE);
                else
                    objDataWrapper.AddParameter("@AGE", System.DBNull.Value);
                //objDataWrapper.AddParameter("@AGE",objExpertServiceProvidersInfo.AGE);
                objDataWrapper.AddParameter("@EXTENT_OF_INJURY", objExpertServiceProvidersInfo.EXTENT_OF_INJURY);
                objDataWrapper.AddParameter("@OTHER_DETAILS", objExpertServiceProvidersInfo.OTHER_DETAILS);
                objDataWrapper.AddParameter("@BANK_NAME", objExpertServiceProvidersInfo.BANK_NAME);
                objDataWrapper.AddParameter("@ACCOUNT_NUMBER", objExpertServiceProvidersInfo.ACCOUNT_NUMBER);
                objDataWrapper.AddParameter("@ACCOUNT_NAME", objExpertServiceProvidersInfo.ACCOUNT_NAME);
                if (objExpertServiceProvidersInfo.PARENT_ADJUSTER != 0)
                    objDataWrapper.AddParameter("@PARENT_ADJUSTER", objExpertServiceProvidersInfo.PARENT_ADJUSTER);
                else
                    objDataWrapper.AddParameter("@PARENT_ADJUSTER", System.DBNull.Value);
                //objDataWrapper.AddParameter("@PARENT_ADJUSTER",objExpertServiceProvidersInfo.PARENT_ADJUSTER);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_PHONE_EXT", objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_PHONE_EXT);
                objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_FAX", objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_FAX);

                //Added by Mohit Agarwal 20-Aug-2008
                objDataWrapper.AddParameter("@MAIL_1099_ADD1", objExpertServiceProvidersInfo.MAIL_1099_ADD1);
                objDataWrapper.AddParameter("@MAIL_1099_ADD2", objExpertServiceProvidersInfo.MAIL_1099_ADD2);
                objDataWrapper.AddParameter("@MAIL_1099_CITY", objExpertServiceProvidersInfo.MAIL_1099_CITY);
                objDataWrapper.AddParameter("@MAIL_1099_STATE", objExpertServiceProvidersInfo.MAIL_1099_STATE);
                objDataWrapper.AddParameter("@MAIL_1099_ZIP", objExpertServiceProvidersInfo.MAIL_1099_ZIP);
                objDataWrapper.AddParameter("@MAIL_1099_COUNTRY", objExpertServiceProvidersInfo.MAIL_1099_COUNTRY);

                objDataWrapper.AddParameter("@MAIL_1099_NAME", objExpertServiceProvidersInfo.MAIL_1099_NAME);
                objDataWrapper.AddParameter("@W9_FORM", objExpertServiceProvidersInfo.W9_FORM);
                //Added By Raghav for Itrack Issue 4810
                //objDataWrapper.AddParameter("@REQ_SPECIAL_HANDLING",objExpertServiceProvidersInfo.REQ_SPECIAL_HANDLING);
                //Added by Sibin on 22 Dec 08 for Itrack Issue 5216 
                if (objExpertServiceProvidersInfo.REQ_SPECIAL_HANDLING != 0)
                    objDataWrapper.AddParameter("@REQ_SPECIAL_HANDLING", objExpertServiceProvidersInfo.REQ_SPECIAL_HANDLING);
                else
                    objDataWrapper.AddParameter("@REQ_SPECIAL_HANDLING", System.DBNull.Value);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@EXPERT_SERVICE_ID", objExpertServiceProvidersInfo.EXPERT_SERVICE_ID, SqlDbType.Int, ParameterDirection.Output);
                SqlParameter objSqlRetParameter = (SqlParameter)objDataWrapper.AddParameter("@RETURN_VALUE", null, SqlDbType.Int, ParameterDirection.ReturnValue);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    objExpertServiceProvidersInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);
                    string strTranXML = objBuilder.GetTransactionLogXML(objExpertServiceProvidersInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 3;
                    objTransactionInfo.RECORDED_BY = objExpertServiceProvidersInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Expert Service Providers has been added";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    returnResult = objDataWrapper.ExecuteNonQuery(InsertCLM_EXPERT_SERVICE_PROVIDERS, objTransactionInfo);
                }
                else
                    returnResult = objDataWrapper.ExecuteNonQuery(InsertCLM_EXPERT_SERVICE_PROVIDERS);


                int EXPERT_SERVICE_ID = 0;
                //if (returnResult>0)
                if (objSqlRetParameter != null && objSqlRetParameter.Value.ToString() != "")
                {
                    if (int.Parse(objSqlRetParameter.Value.ToString()) > 0)
                    {
                        EXPERT_SERVICE_ID = int.Parse(objSqlParameter.Value.ToString());
                        objExpertServiceProvidersInfo.EXPERT_SERVICE_ID = EXPERT_SERVICE_ID;
                    }
                    else
                        returnResult = int.Parse(objSqlRetParameter.Value.ToString());
                }
                else
                    returnResult = -1;


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

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldLocationInfo">Model object having old information</param>
		/// <param name="objLocationInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
        public int Update(Cms.Model.Maintenance.Claims.ClsExpertServiceProvidersInfo objOldExpertServiceProvidersInfo, string customInfo, Cms.Model.Maintenance.Claims.ClsExpertServiceProvidersInfo objExpertServiceProvidersInfo, string XmlFullFilePath)
		{
			//return Update(objOldExpertServiceProvidersInfo,objExpertServiceProvidersInfo,null);
            return Update(objOldExpertServiceProvidersInfo, customInfo, objExpertServiceProvidersInfo, null, XmlFullFilePath);
			
		}
		public int Update(Cms.Model.Maintenance.Claims.ClsExpertServiceProvidersInfo objOldExpertServiceProvidersInfo,Cms.Model.Maintenance.Claims.ClsExpertServiceProvidersInfo objExpertServiceProvidersInfo,DataWrapper objDataWrapper)
		{
			int returnResult = 0;	
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			if(objDataWrapper==null)
				objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);						
			try 
			{
				objDataWrapper.AddParameter("@EXPERT_SERVICE_ID",objExpertServiceProvidersInfo.EXPERT_SERVICE_ID);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE",objExpertServiceProvidersInfo.EXPERT_SERVICE_TYPE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_NAME",objExpertServiceProvidersInfo.EXPERT_SERVICE_NAME);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_ADDRESS1",objExpertServiceProvidersInfo.EXPERT_SERVICE_ADDRESS1);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_ADDRESS2",objExpertServiceProvidersInfo.EXPERT_SERVICE_ADDRESS2);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CITY",objExpertServiceProvidersInfo.EXPERT_SERVICE_CITY);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_STATE",objExpertServiceProvidersInfo.EXPERT_SERVICE_STATE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_ZIP",objExpertServiceProvidersInfo.EXPERT_SERVICE_ZIP);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_VENDOR_CODE",objExpertServiceProvidersInfo.EXPERT_SERVICE_VENDOR_CODE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_NAME",objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_NAME);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_PHONE",objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_PHONE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_EMAIL",objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_EMAIL);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_FEDRERAL_ID",objExpertServiceProvidersInfo.EXPERT_SERVICE_FEDRERAL_ID);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_1099_PROCESSING_OPTION",objExpertServiceProvidersInfo.EXPERT_SERVICE_1099_PROCESSING_OPTION);
				objDataWrapper.AddParameter("@MODIFIED_BY",objExpertServiceProvidersInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objExpertServiceProvidersInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_COUNTRY",objExpertServiceProvidersInfo.EXPERT_SERVICE_COUNTRY);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_MASTER_VENDOR_CODE",objExpertServiceProvidersInfo.EXPERT_SERVICE_MASTER_VENDOR_CODE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE_DESC",objExpertServiceProvidersInfo.EXPERT_SERVICE_TYPE_DESC);								
				objDataWrapper.AddParameter("@PARTY_DETAIL",objExpertServiceProvidersInfo.PARTY_DETAIL);
                objDataWrapper.AddParameter("@ACTIVITY", objExpertServiceProvidersInfo.ACTIVITY);
                if (objExpertServiceProvidersInfo.REG_ID_ISSUE_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", objExpertServiceProvidersInfo.REG_ID_ISSUE_DATE);
                else
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE",null);
                if (objExpertServiceProvidersInfo.DATE_OF_BIRTH != DateTime.MinValue)
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", objExpertServiceProvidersInfo.DATE_OF_BIRTH);
                else
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH",null);
                objDataWrapper.AddParameter("@REG_ID_ISSUE", objExpertServiceProvidersInfo.REG_ID_ISSUE);
               
                objDataWrapper.AddParameter("@CPF", objExpertServiceProvidersInfo.CPF);
                objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", objExpertServiceProvidersInfo.REGIONAL_IDENTIFICATION);
				//objDataWrapper.AddParameter("@AGE",objExpertServiceProvidersInfo.AGE);
				if(objExpertServiceProvidersInfo.AGE!=0)
					objDataWrapper.AddParameter("@AGE",objExpertServiceProvidersInfo.AGE);
				else
					objDataWrapper.AddParameter("@AGE",System.DBNull.Value);
				objDataWrapper.AddParameter("@EXTENT_OF_INJURY",objExpertServiceProvidersInfo.EXTENT_OF_INJURY);
				objDataWrapper.AddParameter("@OTHER_DETAILS",objExpertServiceProvidersInfo.OTHER_DETAILS);
				objDataWrapper.AddParameter("@BANK_NAME",objExpertServiceProvidersInfo.BANK_NAME);
				objDataWrapper.AddParameter("@ACCOUNT_NUMBER",objExpertServiceProvidersInfo.ACCOUNT_NUMBER);
				objDataWrapper.AddParameter("@ACCOUNT_NAME",objExpertServiceProvidersInfo.ACCOUNT_NAME);
				//objDataWrapper.AddParameter("@PARENT_ADJUSTER",objExpertServiceProvidersInfo.PARENT_ADJUSTER);
				if(objExpertServiceProvidersInfo.PARENT_ADJUSTER!=0)
					objDataWrapper.AddParameter("@PARENT_ADJUSTER",objExpertServiceProvidersInfo.PARENT_ADJUSTER);
				else
					objDataWrapper.AddParameter("@PARENT_ADJUSTER",System.DBNull.Value);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_PHONE_EXT",objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_PHONE_EXT);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_FAX",objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_FAX);

				//Added by Mohit Agarwal 20-Aug-2008
				objDataWrapper.AddParameter("@MAIL_1099_ADD1",objExpertServiceProvidersInfo.MAIL_1099_ADD1);
				objDataWrapper.AddParameter("@MAIL_1099_ADD2",objExpertServiceProvidersInfo.MAIL_1099_ADD2);
				objDataWrapper.AddParameter("@MAIL_1099_CITY",objExpertServiceProvidersInfo.MAIL_1099_CITY);
				objDataWrapper.AddParameter("@MAIL_1099_STATE",objExpertServiceProvidersInfo.MAIL_1099_STATE);
				objDataWrapper.AddParameter("@MAIL_1099_ZIP",objExpertServiceProvidersInfo.MAIL_1099_ZIP);
				objDataWrapper.AddParameter("@MAIL_1099_COUNTRY",objExpertServiceProvidersInfo.MAIL_1099_COUNTRY);

				objDataWrapper.AddParameter("@MAIL_1099_NAME",objExpertServiceProvidersInfo.MAIL_1099_NAME);
				objDataWrapper.AddParameter("@W9_FORM",objExpertServiceProvidersInfo.W9_FORM);

				SqlParameter objSqlRetParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue);
				//Done for Itrack Issue 6932 on 10 Feb 2010 ---> to show Expert/Service Type description in transaction Log
				returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_EXPERT_SERVICE_PROVIDERS);
				
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@DETAIL_TYPE_ID",objExpertServiceProvidersInfo.EXPERT_SERVICE_TYPE);
				DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetValuesCLM_TYPE_DETAIL");
				string expertServiceType = ds.Tables[0].Rows[0]["DETAIL_TYPE_DESCRIPTION"].ToString();
				//Added till here
				if(TransactionLogRequired) 
				{
					objExpertServiceProvidersInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/claims/AddExpertServiceProviders.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objOldExpertServiceProvidersInfo,objExpertServiceProvidersInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objExpertServiceProvidersInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Expert Service Providers has been modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Done for Itrack Issue 6932 on 10 Feb 2010 ---> to show Expert/Service Type description in transaction Log
					objTransactionInfo.CUSTOM_INFO		=	"Expert/Service Type = " + expertServiceType + ";Expert Service Provider Name = " + objExpertServiceProvidersInfo.EXPERT_SERVICE_NAME + ";Vendor Code = " + objExpertServiceProvidersInfo.EXPERT_SERVICE_VENDOR_CODE;
					//returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_EXPERT_SERVICE_PROVIDERS,objTransactionInfo);
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				//Done for Itrack Issue 6932 on 10 Feb 2010 ---> to show Expert/Service Type description in transaction Log
//				else
//					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_EXPERT_SERVICE_PROVIDERS);
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if(objSqlRetParameter!=null && objSqlRetParameter.Value.ToString()!="" && int.Parse(objSqlRetParameter.Value.ToString())<0)
				{
					returnResult = int.Parse(objSqlRetParameter.Value.ToString());					
				}				
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
				if(objBuilder != null) 
				{
					objBuilder = null;
				}
			}
		}

        public int Update(Cms.Model.Maintenance.Claims.ClsExpertServiceProvidersInfo objOldExpertServiceProvidersInfo, string customInfo, Cms.Model.Maintenance.Claims.ClsExpertServiceProvidersInfo objExpertServiceProvidersInfo, DataWrapper objDataWrapper, string XmlFullFilePath)
		{
			int returnResult = 0;	
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			if(objDataWrapper==null)
				objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);						
			try 
			{
				objDataWrapper.AddParameter("@EXPERT_SERVICE_ID",objExpertServiceProvidersInfo.EXPERT_SERVICE_ID);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE",objExpertServiceProvidersInfo.EXPERT_SERVICE_TYPE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_NAME",objExpertServiceProvidersInfo.EXPERT_SERVICE_NAME);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_ADDRESS1",objExpertServiceProvidersInfo.EXPERT_SERVICE_ADDRESS1);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_ADDRESS2",objExpertServiceProvidersInfo.EXPERT_SERVICE_ADDRESS2);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CITY",objExpertServiceProvidersInfo.EXPERT_SERVICE_CITY);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_STATE",objExpertServiceProvidersInfo.EXPERT_SERVICE_STATE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_ZIP",objExpertServiceProvidersInfo.EXPERT_SERVICE_ZIP);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_VENDOR_CODE",objExpertServiceProvidersInfo.EXPERT_SERVICE_VENDOR_CODE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_NAME",objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_NAME);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_PHONE",objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_PHONE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_EMAIL",objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_EMAIL);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_FEDRERAL_ID",objExpertServiceProvidersInfo.EXPERT_SERVICE_FEDRERAL_ID);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_1099_PROCESSING_OPTION",objExpertServiceProvidersInfo.EXPERT_SERVICE_1099_PROCESSING_OPTION);
				objDataWrapper.AddParameter("@MODIFIED_BY",objExpertServiceProvidersInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objExpertServiceProvidersInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_COUNTRY",objExpertServiceProvidersInfo.EXPERT_SERVICE_COUNTRY);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_MASTER_VENDOR_CODE",objExpertServiceProvidersInfo.EXPERT_SERVICE_MASTER_VENDOR_CODE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE_DESC",objExpertServiceProvidersInfo.EXPERT_SERVICE_TYPE_DESC);
                objDataWrapper.AddParameter("@ACTIVITY", objExpertServiceProvidersInfo.ACTIVITY);
                objDataWrapper.AddParameter("@REG_ID_ISSUE", objExpertServiceProvidersInfo.REG_ID_ISSUE);
                if (objExpertServiceProvidersInfo.REG_ID_ISSUE_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", objExpertServiceProvidersInfo.REG_ID_ISSUE_DATE);
                else
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", null);
                if (objExpertServiceProvidersInfo.DATE_OF_BIRTH != DateTime.MinValue)
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", objExpertServiceProvidersInfo.DATE_OF_BIRTH);
                else
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", null);
                objDataWrapper.AddParameter("@CPF", objExpertServiceProvidersInfo.CPF);
                objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", objExpertServiceProvidersInfo.REGIONAL_IDENTIFICATION);

				objDataWrapper.AddParameter("@PARTY_DETAIL",objExpertServiceProvidersInfo.PARTY_DETAIL);
				//objDataWrapper.AddParameter("@AGE",objExpertServiceProvidersInfo.AGE);
				if(objExpertServiceProvidersInfo.AGE!=0)
					objDataWrapper.AddParameter("@AGE",objExpertServiceProvidersInfo.AGE);
				else
					objDataWrapper.AddParameter("@AGE",System.DBNull.Value);
				objDataWrapper.AddParameter("@EXTENT_OF_INJURY",objExpertServiceProvidersInfo.EXTENT_OF_INJURY);
				objDataWrapper.AddParameter("@OTHER_DETAILS",objExpertServiceProvidersInfo.OTHER_DETAILS);
				objDataWrapper.AddParameter("@BANK_NAME",objExpertServiceProvidersInfo.BANK_NAME);
				objDataWrapper.AddParameter("@ACCOUNT_NUMBER",objExpertServiceProvidersInfo.ACCOUNT_NUMBER);
				objDataWrapper.AddParameter("@ACCOUNT_NAME",objExpertServiceProvidersInfo.ACCOUNT_NAME);
				//objDataWrapper.AddParameter("@PARENT_ADJUSTER",objExpertServiceProvidersInfo.PARENT_ADJUSTER);
				if(objExpertServiceProvidersInfo.PARENT_ADJUSTER!=0)
					objDataWrapper.AddParameter("@PARENT_ADJUSTER",objExpertServiceProvidersInfo.PARENT_ADJUSTER);
				else
					objDataWrapper.AddParameter("@PARENT_ADJUSTER",System.DBNull.Value);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_PHONE_EXT",objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_PHONE_EXT);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_CONTACT_FAX",objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_FAX);

				//Added by Mohit Agarwal 20-Aug-2008
				objDataWrapper.AddParameter("@MAIL_1099_ADD1",objExpertServiceProvidersInfo.MAIL_1099_ADD1);
				objDataWrapper.AddParameter("@MAIL_1099_ADD2",objExpertServiceProvidersInfo.MAIL_1099_ADD2);
				objDataWrapper.AddParameter("@MAIL_1099_CITY",objExpertServiceProvidersInfo.MAIL_1099_CITY);
				objDataWrapper.AddParameter("@MAIL_1099_STATE",objExpertServiceProvidersInfo.MAIL_1099_STATE);
				objDataWrapper.AddParameter("@MAIL_1099_ZIP",objExpertServiceProvidersInfo.MAIL_1099_ZIP);
				objDataWrapper.AddParameter("@MAIL_1099_COUNTRY",objExpertServiceProvidersInfo.MAIL_1099_COUNTRY);
				//Added By Raghav for Itrack Issue 4810
				//objDataWrapper.AddParameter("@REQ_SPECIAL_HANDLING",objExpertServiceProvidersInfo.REQ_SPECIAL_HANDLING);	
				
				//Added by Sibin on 22 Dec 08 for Itrack Issue 5216 
				if(objExpertServiceProvidersInfo.REQ_SPECIAL_HANDLING!=0)
					objDataWrapper.AddParameter("@REQ_SPECIAL_HANDLING",objExpertServiceProvidersInfo.REQ_SPECIAL_HANDLING);
				else
					objDataWrapper.AddParameter("@REQ_SPECIAL_HANDLING",System.DBNull.Value);

				objDataWrapper.AddParameter("@MAIL_1099_NAME",objExpertServiceProvidersInfo.MAIL_1099_NAME);
				objDataWrapper.AddParameter("@W9_FORM",objExpertServiceProvidersInfo.W9_FORM);

				SqlParameter objSqlRetParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue);

				if(TransactionLogRequired) 
				{
                    objExpertServiceProvidersInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);//MapTransactionLabel("/cmsweb/maintenance/claims/AddExpertServiceProviders.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objOldExpertServiceProvidersInfo,objExpertServiceProvidersInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objExpertServiceProvidersInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Expert Service Providers has been modified";
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='PARENT_ADJUSTER' and @NewValue='0']","NewValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='PARENT_ADJUSTER' and @OldValue='0']","OldValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='AGE' and @NewValue='0']","NewValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='AGE' and @OldValue='0']","OldValue","null");
					
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO      =   customInfo;
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_EXPERT_SERVICE_PROVIDERS,objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_EXPERT_SERVICE_PROVIDERS);
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if(objSqlRetParameter!=null && objSqlRetParameter.Value.ToString()!="" && int.Parse(objSqlRetParameter.Value.ToString())<0)
				{
					returnResult = int.Parse(objSqlRetParameter.Value.ToString());					
				}				
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
				if(objBuilder != null) 
				{
					objBuilder = null;
				}
			}
		}
		
		#endregion

		#region GetLimitsOfAuthority
		/// <summary>
		/// Returns only the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
        public static string GetExpertServiceProviders(int intEXPERT_SERVICE_ID, int LANG_ID)													  
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@EXPERT_SERVICE_ID",intEXPERT_SERVICE_ID);
            objWrapper.AddParameter("@LANG_ID",LANG_ID);
			DataSet ds = objWrapper.ExecuteDataSet(GetCLM_EXPERT_SERVICE_PROVIDERS);			
			if(ds!=null && ds.Tables.Count>0)
				return Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(ds.Tables[0]);
			else
				return "";
		}

		#endregion

		#region GetExpertServiceProviderTypes
		public static DataTable GetExpertServiceProviderTypes()
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);							
			DataSet ds = objWrapper.ExecuteDataSet("Proc_GetExpertServiceProviderTypes");			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}
		#endregion

		
	}
}
