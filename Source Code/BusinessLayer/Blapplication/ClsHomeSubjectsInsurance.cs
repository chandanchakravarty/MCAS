/******************************************************************************************
<Author					: -  Pradeep Iyer
<Start Date				: -	 May 19, 2005
<End Date				: -	
<Description			: - BL class for Subjects of Insurance
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified
*******************************************************************************************/ 

using System;
using System.Data;
using System.Data.SqlClient;
using Cms.Model.Application.HomeOwners;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsHomeSubjectsInsurance.
	/// </summary>
	public class ClsHomeSubjectsInsurance : Cms.BusinessLayer.BlApplication.clsapplication
	{
		public ClsHomeSubjectsInsurance()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public int UpdateRemarks(int customerID, int appID, int appVersionID, 
								int subInsuID,string remarks)
		{
			string	strStoredProc =	"Proc_UpdateHomeOwnerSubInsuRemarks";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@SUB_INSU_ID",subInsuID);
			objWrapper.AddParameter("@REMARKS",remarks);

			objWrapper.ExecuteNonQuery(strStoredProc);

			return 1;
		}

		public string GetRemarks(int customerID, int appID, int appVersionID, int subInsuID)
		{
			string	strStoredProc =	"Proc_GetHomeOwnerSubInsuRemarks";

			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[1] = new SqlParameter("@APP_ID",appID);
			sqlParams[2] = new SqlParameter("@APP_VERSION_ID",appVersionID);
			sqlParams[3] = new SqlParameter("@SUB_INSU_ID",subInsuID);

			string remarks = "";

			remarks = (string)SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams);
			
			return remarks;
			
		}

		/// <summary>
		/// Activates/Deactivates the current record
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="subInsuID"></param>
		/// <param name="action"></param>
		/// <returns></returns>
		public int ActivateDeactivate(int customerID, int appID, int appVersionID, int subInsuID, string action)
		{
			string	strStoredProc =	"Proc_ActivateDeactivateSubInsu";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@SUB_INSU_ID",subInsuID);
			objWrapper.AddParameter("@IS_ACTIVE",action);

			objWrapper.ExecuteNonQuery(strStoredProc);

			return 1;
		

		}
		
		/// <summary>
		/// Gets a single Subject of Insurance record
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="subInsuID"></param>
		/// <returns></returns>
		public DataSet GetHomeOwnerSubInsuByID(int customerID, int appID, int appVersionID, int subInsuID)
		{
			string	strStoredProc =	"Proc_GetHomeOwnerSubInsuByID";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@SUB_INSU_ID",subInsuID);
		

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}
		
		/// <summary>
		/// Inserts a record in the database
		/// </summary>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int Add(ClsSubjectsInsuranceInfo objInfo)
		{
			string	strStoredProc =	"Proc_InsertAPP_HOME_OWNER_SUB_INSU";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Homeowners/AddSubjectsInsurance.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@IS_BLANKET_COV",objInfo.IS_BLANKET_COV);
			objWrapper.AddParameter("@BLANKET_APPLY_TO",objInfo.BLANKET_APPLY_TO);
			objWrapper.AddParameter("@LOCATION_ID",DefaultValues.GetIntNullFromNegative(objInfo.LOCATION_ID));
			objWrapper.AddParameter("@SUB_LOC_ID",DefaultValues.GetIntNullFromNegative(objInfo.SUB_LOC_ID));
			objWrapper.AddParameter("@SUBJECT_OF_INSURANCE",objInfo.SUBJECT_OF_INSURANCE);
			objWrapper.AddParameter("@AMOUNT",DefaultValues.GetDoubleNullFromNegative(objInfo.AMOUNT));
			objWrapper.AddParameter("@OTHERS_DESC",objInfo.OTHERS_DESC);
			objWrapper.AddParameter("@DEDUCTIBLE",DefaultValues.GetDoubleNullFromNegative(objInfo.DEDUCTIBLE));
			objWrapper.AddParameter("@FORMS_CONDITIONS_APPLY",objInfo.FORMS_CONDITIONS_APPLY);
			objWrapper.AddParameter("@IS_PROPERTY_EVER_RENTED",objInfo.IS_PROPERTY_EVER_RENTED);
			objWrapper.AddParameter("@KEEP_WHEN_NOT_IN_USE",objInfo.KEEP_WHEN_NOT_IN_USE);
			objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			
			SqlParameter sqlParamRetVal = (SqlParameter) objWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);
			
			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = objInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1740", "");// "New Homeowner's subject of insurance is added.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;
		}
		
		/// <summary>
		/// Updates a record in the database
		/// </summary>
		/// <param name="objOldInfo"></param>
		/// <param name="objNewInfo"></param>
		/// <returns></returns>
		public int Update(ClsSubjectsInsuranceInfo objOldInfo,ClsSubjectsInsuranceInfo objNewInfo)
		{
			string	strStoredProc =	"Proc_UpdateAPP_HOME_OWNER_SUB_INSU";
			string strTranXML = "";
			
			if ( this.TransactionLogRequired)
			{
				objNewInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Homeowners/AddSubjectsInsurance.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objNewInfo);
		
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objNewInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objNewInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@SUB_INSU_ID",objNewInfo.SUB_INSU_ID);
			objWrapper.AddParameter("@IS_BLANKET_COV",objNewInfo.IS_BLANKET_COV);
			objWrapper.AddParameter("@BLANKET_APPLY_TO",objNewInfo.BLANKET_APPLY_TO);
			objWrapper.AddParameter("@LOCATION_ID",DefaultValues.GetIntNullFromNegative(objNewInfo.LOCATION_ID));
			objWrapper.AddParameter("@SUB_LOC_ID",DefaultValues.GetIntNullFromNegative(objNewInfo.SUB_LOC_ID));
			objWrapper.AddParameter("@SUBJECT_OF_INSURANCE",objNewInfo.SUBJECT_OF_INSURANCE);
			objWrapper.AddParameter("@AMOUNT",DefaultValues.GetDoubleNullFromNegative(objNewInfo.AMOUNT));
			objWrapper.AddParameter("@OTHERS_DESC",objNewInfo.OTHERS_DESC);
			objWrapper.AddParameter("@DEDUCTIBLE",DefaultValues.GetDoubleNullFromNegative(objNewInfo.DEDUCTIBLE));
			objWrapper.AddParameter("@FORMS_CONDITIONS_APPLY",objNewInfo.FORMS_CONDITIONS_APPLY);
			objWrapper.AddParameter("@IS_PROPERTY_EVER_RENTED",objNewInfo.IS_PROPERTY_EVER_RENTED);
			objWrapper.AddParameter("@KEEP_WHEN_NOT_IN_USE",objNewInfo.KEEP_WHEN_NOT_IN_USE);
			objWrapper.AddParameter("@MODIFIED_BY",objNewInfo.CREATED_BY);
			
			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = objNewInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objNewInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objNewInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objNewInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1739", "");// "Homeowner's subject of insurance is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}

			return 1;
		}

		
	}
}
