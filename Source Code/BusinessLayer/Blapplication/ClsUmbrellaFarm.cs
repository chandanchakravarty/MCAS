/******************************************************************************************
<Author					: -  Ravindra Kumar Gupta
<Start Date				: -	 03/10/2006
<End Date				: -	
<Description			: -  Business Logic for UMBRELLA FARM
<Review Date			: - 
<Reviewed By			: - 	
Modification History
************************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using Cms.Model.Application;
using Cms.Model.Policy.Umbrella;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsUmbrellaFarm.
	/// </summary>
	public class ClsUmbrellaFarm :Cms.BusinessLayer.BlApplication.clsapplication 
	{
		
		#region Constructor
		public ClsUmbrellaFarm()
		{
			
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="ObjUmbrellaFarmInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(Cms.Model.Application.ClsUmbrellaFarmInfo objUmbrellaFarmInfo)
		{
			string		strStoredProc	=	"Proc_InsertUmbrellaFarmInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@LOCATION_NUMBER",objUmbrellaFarmInfo.LOCATION_NUMBER );
				objDataWrapper.AddParameter("@CUSTOMER_ID",objUmbrellaFarmInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objUmbrellaFarmInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objUmbrellaFarmInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ADDRESS_1",objUmbrellaFarmInfo.ADDRESS_1);
				objDataWrapper.AddParameter("@ADDRESS_2",objUmbrellaFarmInfo.ADDRESS_2);
				objDataWrapper.AddParameter("@CITY",objUmbrellaFarmInfo.CITY);
				objDataWrapper.AddParameter("@COUNTY",objUmbrellaFarmInfo.COUNTY);
				objDataWrapper.AddParameter("@STATE",objUmbrellaFarmInfo.STATE);
				objDataWrapper.AddParameter("@ZIPCODE",objUmbrellaFarmInfo.ZIPCODE);
				objDataWrapper.AddParameter("@PHONE_NUMBER",objUmbrellaFarmInfo.PHONE_NUMBER);
				objDataWrapper.AddParameter("@FAX_NUMBER",objUmbrellaFarmInfo.FAX_NUMBER);
				if(objUmbrellaFarmInfo.NO_OF_ACRES == -1)
					objDataWrapper.AddParameter("@NO_OF_ACRES",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@NO_OF_ACRES",objUmbrellaFarmInfo.NO_OF_ACRES);
				objDataWrapper.AddParameter("@OCCUPIED_BY_APPLICANT",objUmbrellaFarmInfo.OCCUPIED_BY_APPLICANT);
				objDataWrapper.AddParameter("@RENTED_TO_OTHER",objUmbrellaFarmInfo.RENTED_TO_OTHER);
				objDataWrapper.AddParameter("@EMP_FULL_PART",objUmbrellaFarmInfo.EMP_FULL_PART);
				objDataWrapper.AddParameter("@CREATED_BY",objUmbrellaFarmInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",RecordDate);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@FARM_ID",objUmbrellaFarmInfo.FARM_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objUmbrellaFarmInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/AddUmbrellaFarm.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objUmbrellaFarmInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objUmbrellaFarmInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objUmbrellaFarmInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objUmbrellaFarmInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objUmbrellaFarmInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Umbrella Farm is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				if (returnResult > 0)
				{
					objUmbrellaFarmInfo.FARM_ID = int.Parse(objSqlParameter.Value.ToString());
				}

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

		#region AddPolicy functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="ObjUmbrellaFarmInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddPolicy(ClsFarmDetailsInfo objUmbrellaFarmInfo)
		{
			string		strStoredProc	=	"Proc_InsertPolicyFarmInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@LOCATION_NUMBER",objUmbrellaFarmInfo.LOCATION_NUMBER );
				objDataWrapper.AddParameter("@CUSTOMER_ID",objUmbrellaFarmInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objUmbrellaFarmInfo.POLICY_ID );
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objUmbrellaFarmInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@ADDRESS_1",objUmbrellaFarmInfo.ADDRESS_1);
				objDataWrapper.AddParameter("@ADDRESS_2",objUmbrellaFarmInfo.ADDRESS_2);
				objDataWrapper.AddParameter("@CITY",objUmbrellaFarmInfo.CITY);
				objDataWrapper.AddParameter("@COUNTY",objUmbrellaFarmInfo.COUNTY);
				objDataWrapper.AddParameter("@STATE",objUmbrellaFarmInfo.STATE);
				objDataWrapper.AddParameter("@ZIPCODE",objUmbrellaFarmInfo.ZIPCODE);
				objDataWrapper.AddParameter("@PHONE_NUMBER",objUmbrellaFarmInfo.PHONE_NUMBER);
				objDataWrapper.AddParameter("@FAX_NUMBER",objUmbrellaFarmInfo.FAX_NUMBER);
				objDataWrapper.AddParameter("@NO_OF_ACRES",objUmbrellaFarmInfo.NO_OF_ACRES);
				objDataWrapper.AddParameter("@OCCUPIED_BY_APPLICANT",objUmbrellaFarmInfo.OCCUPIED_BY_APPLICANT);
				objDataWrapper.AddParameter("@RENTED_TO_OTHER",objUmbrellaFarmInfo.RENTED_TO_OTHER);
				objDataWrapper.AddParameter("@EMP_FULL_PART",objUmbrellaFarmInfo.EMP_FULL_PART);
				objDataWrapper.AddParameter("@CREATED_BY",objUmbrellaFarmInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",RecordDate);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@FARM_ID",objUmbrellaFarmInfo.FARM_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objUmbrellaFarmInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyAddFarmDetail.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objUmbrellaFarmInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID = objUmbrellaFarmInfo.POLICY_ID ;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objUmbrellaFarmInfo.POLICY_VERSION_ID ;
					objTransactionInfo.CLIENT_ID = objUmbrellaFarmInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objUmbrellaFarmInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Policy umbrella farm is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				if (returnResult > 0)
				{
					objUmbrellaFarmInfo.FARM_ID = int.Parse(objSqlParameter.Value.ToString());
				}

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

		#region Update Function

		public int Update(ClsUmbrellaFarmInfo objUmbrellaFarmInfo,ClsUmbrellaFarmInfo objOldInfo)
		{
			string		strStoredProc	=	"Proc_UpdateUmbrellaFarmInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@LOCATION_NUMBER",objUmbrellaFarmInfo.LOCATION_NUMBER );
				objDataWrapper.AddParameter("@CUSTOMER_ID",objUmbrellaFarmInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objUmbrellaFarmInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objUmbrellaFarmInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ADDRESS_1",objUmbrellaFarmInfo.ADDRESS_1);
				objDataWrapper.AddParameter("@ADDRESS_2",objUmbrellaFarmInfo.ADDRESS_2);
				objDataWrapper.AddParameter("@CITY",objUmbrellaFarmInfo.CITY);
				objDataWrapper.AddParameter("@COUNTY",objUmbrellaFarmInfo.COUNTY);
				objDataWrapper.AddParameter("@STATE",objUmbrellaFarmInfo.STATE);
				objDataWrapper.AddParameter("@ZIPCODE",objUmbrellaFarmInfo.ZIPCODE);
				objDataWrapper.AddParameter("@PHONE_NUMBER",objUmbrellaFarmInfo.PHONE_NUMBER);
				objDataWrapper.AddParameter("@FAX_NUMBER",objUmbrellaFarmInfo.FAX_NUMBER);
				if(objUmbrellaFarmInfo.NO_OF_ACRES == -1)
					objDataWrapper.AddParameter("@NO_OF_ACRES",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@NO_OF_ACRES",objUmbrellaFarmInfo.NO_OF_ACRES);
				objDataWrapper.AddParameter("@OCCUPIED_BY_APPLICANT",objUmbrellaFarmInfo.OCCUPIED_BY_APPLICANT);
				objDataWrapper.AddParameter("@RENTED_TO_OTHER",objUmbrellaFarmInfo.RENTED_TO_OTHER);
				objDataWrapper.AddParameter("@EMP_FULL_PART",objUmbrellaFarmInfo.EMP_FULL_PART);
				objDataWrapper.AddParameter("@MODIFIED_BY",objUmbrellaFarmInfo.MODIFIED_BY );
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",RecordDate	);
				objDataWrapper.AddParameter("@FARM_ID",objUmbrellaFarmInfo.FARM_ID);
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objUmbrellaFarmInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/AddUmbrellaFarm.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objUmbrellaFarmInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.APP_ID = objUmbrellaFarmInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objUmbrellaFarmInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objUmbrellaFarmInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objUmbrellaFarmInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Umbrella Farm is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

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

		public int Delete(ClsUmbrellaFarmInfo objInfo)
		{
			string		strStoredProc	=	"Proc_DeleteUmbrellaFarmInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);							
				objDataWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@FARM_ID",objInfo.FARM_ID);				
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					
					objTransactionInfo.APP_ID	=	objInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objInfo.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.MODIFIED_BY;
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Farm Detailas is deleted." ;			
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
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


		public int DeletePolicyUmbrella(Cms.Model.Policy.Umbrella.ClsFarmDetailsInfo objInfo)
		{
			string		strStoredProc	=	"Proc_DeletePolicyUmbrellaFarmInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);							
				objDataWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@FARM_ID",objInfo.FARM_ID);				
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					
					objTransactionInfo.POLICY_ID	=	objInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.MODIFIED_BY;
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Farm Detailas is deleted." ;			
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
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

		#region UpdatePolicy Function

		public int UpdatePolicy(ClsFarmDetailsInfo  objUmbrellaFarmInfo,ClsFarmDetailsInfo  objOldInfo)
		{
			string		strStoredProc	=	"Proc_UpdatePolicyFarmInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@LOCATION_NUMBER",objUmbrellaFarmInfo.LOCATION_NUMBER );
				objDataWrapper.AddParameter("@CUSTOMER_ID",objUmbrellaFarmInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objUmbrellaFarmInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objUmbrellaFarmInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@ADDRESS_1",objUmbrellaFarmInfo.ADDRESS_1);
				objDataWrapper.AddParameter("@ADDRESS_2",objUmbrellaFarmInfo.ADDRESS_2);
				objDataWrapper.AddParameter("@CITY",objUmbrellaFarmInfo.CITY);
				objDataWrapper.AddParameter("@COUNTY",objUmbrellaFarmInfo.COUNTY);
				objDataWrapper.AddParameter("@STATE",objUmbrellaFarmInfo.STATE);
				objDataWrapper.AddParameter("@ZIPCODE",objUmbrellaFarmInfo.ZIPCODE);
				objDataWrapper.AddParameter("@PHONE_NUMBER",objUmbrellaFarmInfo.PHONE_NUMBER);
				objDataWrapper.AddParameter("@FAX_NUMBER",objUmbrellaFarmInfo.FAX_NUMBER);
				objDataWrapper.AddParameter("@NO_OF_ACRES",objUmbrellaFarmInfo.NO_OF_ACRES);
				objDataWrapper.AddParameter("@OCCUPIED_BY_APPLICANT",objUmbrellaFarmInfo.OCCUPIED_BY_APPLICANT);
				objDataWrapper.AddParameter("@RENTED_TO_OTHER",objUmbrellaFarmInfo.RENTED_TO_OTHER);
				objDataWrapper.AddParameter("@EMP_FULL_PART",objUmbrellaFarmInfo.EMP_FULL_PART);
				objDataWrapper.AddParameter("@MODIFIED_BY",objUmbrellaFarmInfo.MODIFIED_BY );
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",RecordDate	);
				objDataWrapper.AddParameter("@FARM_ID",objUmbrellaFarmInfo.FARM_ID);
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objUmbrellaFarmInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyAddFarmDetail.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objUmbrellaFarmInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.POLICY_ID =objUmbrellaFarmInfo.POLICY_ID ;
					objTransactionInfo.POLICY_VER_TRACKING_ID =objUmbrellaFarmInfo.POLICY_VERSION_ID ;
					objTransactionInfo.CLIENT_ID = objUmbrellaFarmInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objUmbrellaFarmInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Policy Umbrella Farm is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

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

		#region Activate deactivate function
		public void ActivateDeactivateFarm(int intCustomerId, int intAppId, int intAppVersionId, int intFarmId, string strStatus )
		{
			string strStoredProc = "Proc_ActivateDeactvateUmbrellaFarm";
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@APP_ID",intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
				objDataWrapper.AddParameter("@FARM_ID",intFarmId);
				objDataWrapper.AddParameter("@STATUS",strStatus);
				
				objDataWrapper.ExecuteNonQuery(strStoredProc);
				
			}
			catch(Exception ex)
			{
				throw (ex);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
		#endregion

		#region Activate deactivate  function for Policy
		public void ActivateDeactivatePolicyFarm(int intCustomerId, int intPolicyId, int intPolicyVersionId, int intFarmId, string strStatus )
		{
			string strStoredProc = "Proc_ActivateDeactvatePolicyFarm";
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@POLICY_ID",intPolicyId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVersionId);
				objDataWrapper.AddParameter("@FARM_ID",intFarmId);
				objDataWrapper.AddParameter("@STATUS",strStatus);
				
				objDataWrapper.ExecuteNonQuery(strStoredProc);
				
			}
			catch(Exception ex)
			{
				throw (ex);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
		#endregion

		#region GetFarmInfo Function
		public static DataTable GetFarmInfo(int customerId ,int appId,int appVersionId,int farmId)
		{
			string		strStoredProc	=	"Proc_GetUmbrellaFarmInfo";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);
				objDataWrapper.AddParameter("@FARM_ID",farmId,SqlDbType.Int);
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
	
			return dsCount.Tables[0];
		}
		#endregion

		#region GetPolicyFarmInfo Function
		public static DataTable GetPolicyFarmInfo(int customerId ,int policyId,int policyVersionId,int farmId)
		{
			string		strStoredProc	=	"Proc_GetPolicyFarmInfo";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionId,SqlDbType.Int);
				objDataWrapper.AddParameter("@FARM_ID",farmId,SqlDbType.Int);
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
	
			return dsCount.Tables[0];
		}
		#endregion

		#region GetNextLocationNumber Function

		public static int GetNextLocationNumber(int customerId ,int appId,int appVersionId)
		{
			string		strStoredProc	=	"Proc_GetNextFarmLocationNumber";
						
			int nextLocationNumber = 0;

			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerId);
			sqlParams[1] = new SqlParameter("@APP_ID",appId);
			sqlParams[2] = new SqlParameter("@APP_VERSION_ID",appVersionId);
			
			try
			{
				nextLocationNumber = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams));
			}
			catch(Exception ex)
			{
				throw(ex);
			}

			return nextLocationNumber;
						
		}

		#endregion

		#region GetNextPolicyLocationNumber Function

		public static int GetNextPolicyLocationNumber(int customerId ,int policyId,int policyVersionId)
		{
			string		strStoredProc	=	"Proc_GetNextPolicyLocationNumber";
						
			int nextLocationNumber = 0;

			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerId);
			sqlParams[1] = new SqlParameter("@POLICY_ID",policyId);
			sqlParams[2] = new SqlParameter("@POLICY_VERSION_ID",policyVersionId);
			
			try
			{
				nextLocationNumber = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams));
			}
			catch(Exception ex)
			{
				throw(ex);
			}

			return nextLocationNumber;
						
		}

		#endregion

	}
}
