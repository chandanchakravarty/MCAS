/******************************************************************************************
<Author					: -  Pradeep Iyer
<Start Date				: -	 May 23, 2005
<End Date				: -	
<Description			: -  BL class for UMBRELLA LIMITS
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified

<Modified Date			: -	Ravindra	
<Modified By			: - 03-22-206
<Purpose				: - To Add Policy Level Functions

*******************************************************************************************/ 

using System;
using System.Data;
using System.Data.SqlClient;
using Cms.Model.Application;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsUmbrellaLimits.
	/// </summary>
	public class ClsInformationLimits : Cms.BusinessLayer.BlApplication.clsapplication
	{
		public ClsInformationLimits()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		/// <summary>
		/// Gets a single Employment info record
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns></returns>
		public DataSet GetEmploymentInfoByID(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_GetEmploymentInfoByID";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}
		
		/// <summary>
		/// Updates a single employment record
		/// </summary>
		/// <param name="objOldInfo"></param>
		/// <param name="objNewInfo"></param>
		/// <returns></returns>
		public int UpdateEmployment(ClsEmploymentInfo objOldInfo,ClsEmploymentInfo objNewInfo)
		{
			string	strStoredProc =	"Proc_UpdateAPP_UMBRELLA_EMPLOYMENT_INFO";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objNewInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AddEmployment.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objNewInfo);
			}

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objNewInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objNewInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@APP_OCCUPATION",objNewInfo.APP_OCCUPATION);
			objDataWrapper.AddParameter("@CO_APP_OCCUPATION",objNewInfo.CO_APP_OCCUPATION);
			
			SqlParameter sqlParamRetVal = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);

			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = objNewInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objNewInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objNewInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
					objTransactionInfo.TRANS_DESC		=	"Employment Information is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;
			

		}
		
		/// <summary>
		/// Inserts a single employment record
		/// </summary>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int AddEmployment(ClsEmploymentInfo objInfo)
		{
			string	strStoredProc =	"Proc_InsertAPP_UMBRELLA_EMPLOYMENT_INFO";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AddEmployment.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objInfo);
			}

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@APP_OCCUPATION",objInfo.APP_OCCUPATION);
			objDataWrapper.AddParameter("@CO_APP_OCCUPATION",objInfo.CO_APP_OCCUPATION);
			
			SqlParameter sqlParamRetVal = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);

			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
					objTransactionInfo.APP_ID			=	objInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"New employment information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;
			

		}


		/// <summary>
		/// Gets the Umbrella limits information 
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns></returns>
		public DataSet GetUmbrellaLimit2(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_GetAPP_UMBRELLA_LIMITS2";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}

		#region GetPolicyLimit2 Function
		public DataSet GetPolicyLimit2(int customerID, int policyID, int policyVersionID)
		{
			string	strStoredProc =	"Proc_GetPOL_UMBRELLA_LIMITS2";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}
		#endregion
		
		/// <summary>
		/// Inserts a record in UMBRELLA LIMITS
		/// </summary>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int AddUmbrellaLimit2(ClsUmbrellaLimitsInfo objInfo)
		{
			string	strStoredProc =	"Proc_InsertAPP_UMBRELLA_LIMITS2";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AddUmbrellaLimits2.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objInfo);
			}

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@BASIC",DefaultValues.GetDoubleNullFromNegative(objInfo.BASIC));
			objDataWrapper.AddParameter("@RESIDENCES_OWNER_OCCUPIED",DefaultValues.GetDoubleNullFromNegative(objInfo.RESIDENCES_OWNER_OCCUPIED));
			objDataWrapper.AddParameter("@NUM_OF_RENTAL_UNITS",DefaultValues.GetIntNullFromNegative(objInfo.NUM_OF_RENTAL_UNITS));
			objDataWrapper.AddParameter("@RENTAL_UNITS",DefaultValues.GetDoubleNullFromNegative(objInfo.RENTAL_UNITS));
			objDataWrapper.AddParameter("@NUM_OF_AUTO",DefaultValues.GetIntNullFromNegative(objInfo.NUM_OF_AUTO));
			objDataWrapper.AddParameter("@AUTOMOBILES",DefaultValues.GetDoubleNullFromNegative(objInfo.AUTOMOBILES));
			objDataWrapper.AddParameter("@NUM_OF_OPERATORS",DefaultValues.GetIntNullFromNegative(objInfo.NUM_OF_OPERATORS));
			objDataWrapper.AddParameter("@OPER_UNDER_AGE",DefaultValues.GetDoubleNullFromNegative(objInfo.OPER_UNDER_AGE));
			objDataWrapper.AddParameter("@NUM_OF_UNLIC_RV",DefaultValues.GetIntNullFromNegative(objInfo.NUM_OF_UNLIC_RV));
			objDataWrapper.AddParameter("@UNLIC_RV",DefaultValues.GetDoubleNullFromNegative(objInfo.UNLIC_RV));
			objDataWrapper.AddParameter("@NUM_OF_UNINSU_MOTORIST",DefaultValues.GetIntNullFromNegative(objInfo.NUM_OF_UNINSU_MOTORIST));
			objDataWrapper.AddParameter("@UNISU_MOTORIST",DefaultValues.GetDoubleNullFromNegative(objInfo.UNISU_MOTORIST));
			objDataWrapper.AddParameter("@UNDER_INSURED_MOTORIST",DefaultValues.GetDoubleNullFromNegative(objInfo.UNDER_INSURED_MOTORIST));
			objDataWrapper.AddParameter("@WATERCRAFT",DefaultValues.GetDoubleNullFromNegative(objInfo.WATERCRAFT));
			objDataWrapper.AddParameter("@NUM_OF_OTHER",DefaultValues.GetIntNullFromNegative(objInfo.NUM_OF_OTHER));
			objDataWrapper.AddParameter("@OTHER",DefaultValues.GetDoubleNullFromNegative(objInfo.OTHER));
			objDataWrapper.AddParameter("@DEPOSIT",DefaultValues.GetDoubleNullFromNegative(objInfo.DEPOSIT));
			objDataWrapper.AddParameter("@ESTIMATED_TOTAL_PRE",DefaultValues.GetDoubleNullFromNegative(objInfo.ESTIMATED_TOTAL_PRE));
			objDataWrapper.AddParameter("@CALCULATIONS",objInfo.CALCULATIONS);
			objDataWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			
			SqlParameter sqlParamRetVal = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);
			
			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
					objTransactionInfo.APP_ID			=	objInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New umbrella limit is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;
		}
		


		#region AddPolicyLimit2 Function
		public int AddPolicyLimit2(Cms.Model.Policy.Umbrella.ClsPolicyLimitsInfo  objInfo)
		{
			string	strStoredProc =	"Proc_InsertPOL_UMBRELLA_LIMITS2";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyAddLimit2.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objInfo);
			}

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID );
			objDataWrapper.AddParameter("@BASIC",DefaultValues.GetDoubleNullFromNegative(objInfo.BASIC));
			objDataWrapper.AddParameter("@RESIDENCES_OWNER_OCCUPIED",DefaultValues.GetDoubleNullFromNegative(objInfo.RESIDENCES_OWNER_OCCUPIED));
			objDataWrapper.AddParameter("@NUM_OF_RENTAL_UNITS",DefaultValues.GetIntNullFromNegative(objInfo.NUM_OF_RENTAL_UNITS));
			objDataWrapper.AddParameter("@RENTAL_UNITS",DefaultValues.GetDoubleNullFromNegative(objInfo.RENTAL_UNITS));
			objDataWrapper.AddParameter("@NUM_OF_AUTO",DefaultValues.GetIntNullFromNegative(objInfo.NUM_OF_AUTO));
			objDataWrapper.AddParameter("@AUTOMOBILES",DefaultValues.GetDoubleNullFromNegative(objInfo.AUTOMOBILES));
			objDataWrapper.AddParameter("@NUM_OF_OPERATORS",DefaultValues.GetIntNullFromNegative(objInfo.NUM_OF_OPERATORS));
			objDataWrapper.AddParameter("@OPER_UNDER_AGE",DefaultValues.GetDoubleNullFromNegative(objInfo.OPER_UNDER_AGE));
			objDataWrapper.AddParameter("@NUM_OF_UNLIC_RV",DefaultValues.GetIntNullFromNegative(objInfo.NUM_OF_UNLIC_RV));
			objDataWrapper.AddParameter("@UNLIC_RV",DefaultValues.GetDoubleNullFromNegative(objInfo.UNLIC_RV));
			objDataWrapper.AddParameter("@NUM_OF_UNINSU_MOTORIST",DefaultValues.GetIntNullFromNegative(objInfo.NUM_OF_UNINSU_MOTORIST));
			objDataWrapper.AddParameter("@UNISU_MOTORIST",DefaultValues.GetDoubleNullFromNegative(objInfo.UNISU_MOTORIST));
			objDataWrapper.AddParameter("@UNDER_INSURED_MOTORIST",DefaultValues.GetDoubleNullFromNegative(objInfo.UNDER_INSURED_MOTORIST));
			objDataWrapper.AddParameter("@WATERCRAFT",DefaultValues.GetDoubleNullFromNegative(objInfo.WATERCRAFT));			
			objDataWrapper.AddParameter("@NUM_OF_OTHER",DefaultValues.GetDoubleNullFromNegative(objInfo.NUM_OF_OTHER));
			objDataWrapper.AddParameter("@OTHER",DefaultValues.GetDoubleNullFromNegative(objInfo.OTHER));
			objDataWrapper.AddParameter("@DEPOSIT",DefaultValues.GetDoubleNullFromNegative(objInfo.DEPOSIT));
			objDataWrapper.AddParameter("@ESTIMATED_TOTAL_PRE",DefaultValues.GetDoubleNullFromNegative(objInfo.ESTIMATED_TOTAL_PRE));
			objDataWrapper.AddParameter("@CALCULATIONS",objInfo.CALCULATIONS);
			objDataWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			
			SqlParameter sqlParamRetVal = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);
			
			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
					objTransactionInfo.POLICY_ID 		=	objInfo.POLICY_ID ;
					objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New umbrella limit is added in policy";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;
		}
		#endregion 

		/// <summary>
		/// Updates the record in UMBRELLA LIMITS
		/// </summary>
		/// <param name="objOldInfo"></param>
		/// <param name="objNewInfo"></param>
		/// <returns></returns>
		public int UpdateUmbrellaLimit2(ClsUmbrellaLimitsInfo objOldInfo,ClsUmbrellaLimitsInfo objNewInfo)
		{
			string	strStoredProc =	"Proc_UpdateAPP_UMBRELLA_LIMITS2";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objNewInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AddUmbrellaLimits2.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objNewInfo);
			}

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objNewInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objNewInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@BASIC",DefaultValues.GetDoubleNullFromNegative(objNewInfo.BASIC));
			objDataWrapper.AddParameter("@RESIDENCES_OWNER_OCCUPIED",DefaultValues.GetDoubleNullFromNegative(objNewInfo.RESIDENCES_OWNER_OCCUPIED));
			objDataWrapper.AddParameter("@NUM_OF_RENTAL_UNITS",DefaultValues.GetIntNullFromNegative(objNewInfo.NUM_OF_RENTAL_UNITS));
			objDataWrapper.AddParameter("@RENTAL_UNITS",DefaultValues.GetDoubleNullFromNegative(objNewInfo.RENTAL_UNITS));
			objDataWrapper.AddParameter("@NUM_OF_AUTO",DefaultValues.GetIntNullFromNegative(objNewInfo.NUM_OF_AUTO));
			objDataWrapper.AddParameter("@AUTOMOBILES",DefaultValues.GetDoubleNullFromNegative(objNewInfo.AUTOMOBILES));
			objDataWrapper.AddParameter("@NUM_OF_OPERATORS",DefaultValues.GetIntNullFromNegative(objNewInfo.NUM_OF_OPERATORS));
			objDataWrapper.AddParameter("@OPER_UNDER_AGE",DefaultValues.GetDoubleNullFromNegative(objNewInfo.OPER_UNDER_AGE));
			objDataWrapper.AddParameter("@NUM_OF_UNLIC_RV",DefaultValues.GetIntNullFromNegative(objNewInfo.NUM_OF_UNLIC_RV));
			objDataWrapper.AddParameter("@UNLIC_RV",DefaultValues.GetDoubleNullFromNegative(objNewInfo.UNLIC_RV));
			objDataWrapper.AddParameter("@NUM_OF_UNINSU_MOTORIST",DefaultValues.GetIntNullFromNegative(objNewInfo.NUM_OF_UNINSU_MOTORIST));
			objDataWrapper.AddParameter("@UNISU_MOTORIST",DefaultValues.GetDoubleNullFromNegative(objNewInfo.UNISU_MOTORIST));
			objDataWrapper.AddParameter("@UNDER_INSURED_MOTORIST",DefaultValues.GetDoubleNullFromNegative(objNewInfo.UNDER_INSURED_MOTORIST));
			objDataWrapper.AddParameter("@WATERCRAFT",DefaultValues.GetDoubleNullFromNegative(objNewInfo.WATERCRAFT));
			objDataWrapper.AddParameter("@NUM_OF_OTHER",DefaultValues.GetIntNullFromNegative(objNewInfo.NUM_OF_OTHER));
			objDataWrapper.AddParameter("@OTHER",DefaultValues.GetDoubleNullFromNegative(objNewInfo.OTHER));
			objDataWrapper.AddParameter("@DEPOSIT",DefaultValues.GetDoubleNullFromNegative(objNewInfo.DEPOSIT));
			objDataWrapper.AddParameter("@ESTIMATED_TOTAL_PRE",DefaultValues.GetDoubleNullFromNegative(objNewInfo.ESTIMATED_TOTAL_PRE));
			objDataWrapper.AddParameter("@CALCULATIONS",objNewInfo.CALCULATIONS);
			objDataWrapper.AddParameter("@MODIFIED_BY",objNewInfo.MODIFIED_BY);
			
			SqlParameter sqlParamRetVal = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);
			
			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
					objTransactionInfo.APP_ID			=	objNewInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objNewInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objNewInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objNewInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Umbrella limit is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;
		}

	
		#region UpdatePolicyLimits2 Function
		public int UpdatePolicyLimit2(Cms.Model.Policy.Umbrella.ClsPolicyLimitsInfo  objOldInfo,
										Cms.Model.Policy.Umbrella.ClsPolicyLimitsInfo  objNewInfo)
		{
			string	strStoredProc =	"Proc_UpdatePOL_UMBRELLA_LIMITS2";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objNewInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyAddLimit2.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objNewInfo);
			}

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@POLICY_ID",objNewInfo.POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",objNewInfo.POLICY_VERSION_ID);
			objDataWrapper.AddParameter("@BASIC",DefaultValues.GetDoubleNullFromNegative(objNewInfo.BASIC));
			objDataWrapper.AddParameter("@RESIDENCES_OWNER_OCCUPIED",DefaultValues.GetDoubleNullFromNegative(objNewInfo.RESIDENCES_OWNER_OCCUPIED));
			objDataWrapper.AddParameter("@NUM_OF_RENTAL_UNITS",DefaultValues.GetIntNullFromNegative(objNewInfo.NUM_OF_RENTAL_UNITS));
			objDataWrapper.AddParameter("@RENTAL_UNITS",DefaultValues.GetDoubleNullFromNegative(objNewInfo.RENTAL_UNITS));
			objDataWrapper.AddParameter("@NUM_OF_AUTO",DefaultValues.GetIntNullFromNegative(objNewInfo.NUM_OF_AUTO));
			objDataWrapper.AddParameter("@AUTOMOBILES",DefaultValues.GetDoubleNullFromNegative(objNewInfo.AUTOMOBILES));
			objDataWrapper.AddParameter("@NUM_OF_OPERATORS",DefaultValues.GetIntNullFromNegative(objNewInfo.NUM_OF_OPERATORS));
			objDataWrapper.AddParameter("@OPER_UNDER_AGE",DefaultValues.GetDoubleNullFromNegative(objNewInfo.OPER_UNDER_AGE));
			objDataWrapper.AddParameter("@NUM_OF_UNLIC_RV",DefaultValues.GetIntNullFromNegative(objNewInfo.NUM_OF_UNLIC_RV));
			objDataWrapper.AddParameter("@UNLIC_RV",DefaultValues.GetDoubleNullFromNegative(objNewInfo.UNLIC_RV));
			objDataWrapper.AddParameter("@NUM_OF_UNINSU_MOTORIST",DefaultValues.GetIntNullFromNegative(objNewInfo.NUM_OF_UNINSU_MOTORIST));
			objDataWrapper.AddParameter("@UNISU_MOTORIST",DefaultValues.GetDoubleNullFromNegative(objNewInfo.UNISU_MOTORIST));
			objDataWrapper.AddParameter("@UNDER_INSURED_MOTORIST",DefaultValues.GetDoubleNullFromNegative(objNewInfo.UNDER_INSURED_MOTORIST));
			objDataWrapper.AddParameter("@WATERCRAFT",DefaultValues.GetDoubleNullFromNegative(objNewInfo.WATERCRAFT));
			objDataWrapper.AddParameter("@NUM_OF_OTHER",DefaultValues.GetIntNullFromNegative(objNewInfo.NUM_OF_OTHER));
			objDataWrapper.AddParameter("@OTHER",DefaultValues.GetDoubleNullFromNegative(objNewInfo.OTHER));
			objDataWrapper.AddParameter("@DEPOSIT",DefaultValues.GetDoubleNullFromNegative(objNewInfo.DEPOSIT));
			objDataWrapper.AddParameter("@ESTIMATED_TOTAL_PRE",DefaultValues.GetDoubleNullFromNegative(objNewInfo.ESTIMATED_TOTAL_PRE));
			objDataWrapper.AddParameter("@CALCULATIONS",objNewInfo.CALCULATIONS);
			objDataWrapper.AddParameter("@MODIFIED_BY",objNewInfo.MODIFIED_BY);
			
			SqlParameter sqlParamRetVal = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);
			
			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
					objTransactionInfo.POLICY_ID 		=	objNewInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objNewInfo.POLICY_VERSION_ID ;
					objTransactionInfo.CLIENT_ID		=	objNewInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objNewInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Umbrella limit is modified in policy";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;
		}
		#endregion

		/// <summary>
		/// Gets the Umbrella limits information 
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns></returns>
		public DataSet GetUmbrellaLimit1(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_GetAPP_UMBRELLA_LIMITS1";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}

		
		#region GetPolicyLimit1 Function 
		public DataSet GetPolicyLimit1(int customerID, int policyID, int policyVersionID)
		{
			string	strStoredProc =	"Proc_GetPOL_UMBRELLA_LIMITS1";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}
		#endregion
		
		/// <summary>
		/// Inserts a record in UMBRELLA LIMITS
		/// </summary>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int AddUmbrellaLimit1(ClsUmbrellaLimitsInfo objInfo)
		{
			string	strStoredProc =	"Proc_InsertAPP_UMBRELLA_LIMITS1";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AddUmbrellaLimits1.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@POLICY_LIMITS",DefaultValues.GetDoubleNullFromNegative(objInfo.POLICY_LIMITS));
			objWrapper.AddParameter("@RETENTION_LIMITS",DefaultValues.GetDoubleNullFromNegative(objInfo.RETENTION_LIMITS));
			objWrapper.AddParameter("@UNINSURED_MOTORIST_LIMIT",DefaultValues.GetDoubleNullFromNegative(objInfo.UNINSURED_MOTORIST_LIMIT));
			objWrapper.AddParameter("@UNDERINSURED_MOTORIST_LIMIT",DefaultValues.GetDoubleNullFromNegative(objInfo.UNDER_INSURED_MOTORIST));
			objWrapper.AddParameter("@OTHER_LIMIT",DefaultValues.GetDoubleNullFromNegative(objInfo.OTHER_LIMIT));
			objWrapper.AddParameter("@OTHER_DESCRIPTION",objInfo.OTHER_DESCRIPTION);
			objWrapper.AddParameter("@TERRITORY",objInfo.TERRITORY);
			objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			if (objInfo.CLIENT_UPDATE_DATE!=DateTime.MinValue)
				objWrapper.AddParameter("@CLIENT_UPDATE_DATE",objInfo.CLIENT_UPDATE_DATE);
			else
				objWrapper.AddParameter("@CLIENT_UPDATE_DATE",null);
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
					objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
					objTransactionInfo.APP_ID			=	objInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New umbrella limit is added";
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
		


		#region AddPolicyLimit Function
		public int AddPolicyLimit1(Cms.Model.Policy.Umbrella.ClsPolicyLimitsInfo  objInfo)
		{
			string	strStoredProc =	"Proc_InsertPOL_UMBRELLA_LIMITS1";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyAddLimit1.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID);
			objWrapper.AddParameter("@POLICY_LIMITS",DefaultValues.GetDoubleNullFromNegative(objInfo.POLICY_LIMITS));
			objWrapper.AddParameter("@RETENTION_LIMITS",DefaultValues.GetDoubleNullFromNegative(objInfo.RETENTION_LIMITS));
			objWrapper.AddParameter("@UNINSURED_MOTORIST_LIMIT",DefaultValues.GetDoubleNullFromNegative(objInfo.UNINSURED_MOTORIST_LIMIT));
			objWrapper.AddParameter("@UNDERINSURED_MOTORIST_LIMIT",DefaultValues.GetDoubleNullFromNegative(objInfo.UNDER_INSURED_MOTORIST));
			objWrapper.AddParameter("@OTHER_LIMIT",DefaultValues.GetDoubleNullFromNegative(objInfo.OTHER_LIMIT));
			objWrapper.AddParameter("@OTHER_DESCRIPTION",objInfo.OTHER_DESCRIPTION);
			objWrapper.AddParameter("@TERRITORY",objInfo.TERRITORY);
			objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			if (objInfo.CLIENT_UPDATE_DATE!=DateTime.MinValue)
				objWrapper.AddParameter("@CLIENT_UPDATE_DATE",objInfo.CLIENT_UPDATE_DATE);
			else
				objWrapper.AddParameter("@CLIENT_UPDATE_DATE",null);

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
					objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
					objTransactionInfo.POLICY_ID 			=	objInfo.POLICY_ID; 
					objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New umbrella limit is added";
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
		#endregion


		/// <summary>
		/// Updates the record in UMBRELLA LIMITS
		/// </summary>
		/// <param name="objOldInfo"></param>
		/// <param name="objNewInfo"></param>
		/// <returns></returns>
		public int UpdateUmbrellaLimit1(ClsUmbrellaLimitsInfo objOldInfo,ClsUmbrellaLimitsInfo objNewInfo)
		{
			string	strStoredProc =	"Proc_UpdateAPP_UMBRELLA_LIMITS1";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objNewInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AddUmbrellaLimits1.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objNewInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objNewInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objNewInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@POLICY_LIMITS",DefaultValues.GetDoubleNullFromNegative(objNewInfo.POLICY_LIMITS));
			objWrapper.AddParameter("@RETENTION_LIMITS",DefaultValues.GetDoubleNullFromNegative(objNewInfo.RETENTION_LIMITS));
			objWrapper.AddParameter("@UNINSURED_MOTORIST_LIMIT",DefaultValues.GetDoubleNullFromNegative(objNewInfo.UNINSURED_MOTORIST_LIMIT));
			objWrapper.AddParameter("@UNDERINSURED_MOTORIST_LIMIT",DefaultValues.GetDoubleNullFromNegative(objNewInfo.UNDERINSURED_MOTORIST_LIMIT));
			objWrapper.AddParameter("@OTHER_LIMIT",DefaultValues.GetDoubleNullFromNegative(objNewInfo.OTHER_LIMIT));
			objWrapper.AddParameter("@OTHER_DESCRIPTION",objNewInfo.OTHER_DESCRIPTION);
			objWrapper.AddParameter("@TERRITORY",objNewInfo.TERRITORY);
			objWrapper.AddParameter("@MODIFIED_BY",objNewInfo.MODIFIED_BY);

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
					objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
					objTransactionInfo.APP_ID			=	objNewInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objNewInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objNewInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objNewInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Umbrella limit is modified";
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


		#region UpdatePolicyLimit1 function
		public int UpdatePolicyLimit1(Cms.Model.Policy.Umbrella.ClsPolicyLimitsInfo  objOldInfo,
										Cms.Model.Policy.Umbrella.ClsPolicyLimitsInfo  objNewInfo)
		{
			string	strStoredProc =	"Proc_UpdatePOL_UMBRELLA_LIMITS1";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objNewInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyAddLimit1.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objNewInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",objNewInfo.POLICY_ID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",objNewInfo.POLICY_VERSION_ID);
			objWrapper.AddParameter("@POLICY_LIMITS",DefaultValues.GetDoubleNullFromNegative(objNewInfo.POLICY_LIMITS));
			objWrapper.AddParameter("@RETENTION_LIMITS",DefaultValues.GetDoubleNullFromNegative(objNewInfo.RETENTION_LIMITS));
			objWrapper.AddParameter("@UNINSURED_MOTORIST_LIMIT",DefaultValues.GetDoubleNullFromNegative(objNewInfo.UNINSURED_MOTORIST_LIMIT));
			objWrapper.AddParameter("@UNDERINSURED_MOTORIST_LIMIT",DefaultValues.GetDoubleNullFromNegative(objNewInfo.UNDERINSURED_MOTORIST_LIMIT));
			objWrapper.AddParameter("@OTHER_LIMIT",DefaultValues.GetDoubleNullFromNegative(objNewInfo.OTHER_LIMIT));
			objWrapper.AddParameter("@OTHER_DESCRIPTION",objNewInfo.OTHER_DESCRIPTION);
			objWrapper.AddParameter("@TERRITORY",objNewInfo.TERRITORY);
			objWrapper.AddParameter("@MODIFIED_BY",objNewInfo.MODIFIED_BY);
			if (objNewInfo.CLIENT_UPDATE_DATE!=DateTime.MinValue)
				objWrapper.AddParameter("@CLIENT_UPDATE_DATE",objNewInfo.CLIENT_UPDATE_DATE);
			else
				objWrapper.AddParameter("@CLIENT_UPDATE_DATE",null);

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
					objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
					objTransactionInfo.POLICY_ID 		=	objNewInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objNewInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objNewInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objNewInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Umbrella limit is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;
		}
		#endregion

	}
}
