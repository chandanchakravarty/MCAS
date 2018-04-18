using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Claims;
using Cms.BusinessLayer.BlCommon;


namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// Summary description for ClsDriverDetails.
	/// </summary>
	public class ClsDriverDetails : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		GetCLM_DRIVER_INFORMATION		=	"Proc_GetCLM_DRIVER_INFORMATION";
		private const	string		InsertCLM_DRIVER_INFORMATION		=	"Proc_InsertCLM_DRIVER_INFORMATION";
		private const	string		UpdateCLM_DRIVER_INFORMATION		=	"Proc_UpdateCLM_DRIVER_INFORMATION";				
		private const	string		CheckForOwnerData		=	"Proc_CheckForOwnerData";
		private const	string		GET_OWNERS_LIST					=	"Proc_GET_OWNERS_LIST";
		private const	string		GET_POLICY_DRIVERS_LIST			=	"Proc_GetPolicyDriversListForClaims";
		private const	string		GET_POLICY_DRIVERS_DETAILS		=	"Proc_GetPolicyDriversDetails";
		private const	string		GetClaimDriversInformationLookupUp	=	"Proc_GetClaimDriversInformationLookupUp";
		
		
		public ClsDriverDetails()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Add(Insert) functions
		public int Add(ClsDriverDetailsInfo objDriverDetailsInfo)
		{			
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			try
			{				
				objDataWrapper.AddParameter("@CLAIM_ID",objDriverDetailsInfo.CLAIM_ID);
				//Done for Itrack Issue 6932 on 1 Feb 2010
				DataSet ds = new DataSet();
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				int lobID = int.Parse(ds.Tables[0].Rows[0]["LOB_ID"].ToString());//Done for Itrack Issue 6932 on 3 June 2010

				objDataWrapper.AddParameter("@TYPE_OF_DRIVER",objDriverDetailsInfo.TYPE_OF_DRIVER);
				objDataWrapper.AddParameter("@NAME",objDriverDetailsInfo.NAME);
				objDataWrapper.AddParameter("@ADDRESS1",objDriverDetailsInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objDriverDetailsInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objDriverDetailsInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objDriverDetailsInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objDriverDetailsInfo.ZIP);
				objDataWrapper.AddParameter("@HOME_PHONE",objDriverDetailsInfo.HOME_PHONE);
				objDataWrapper.AddParameter("@MOBILE_PHONE",objDriverDetailsInfo.MOBILE_PHONE);				
				objDataWrapper.AddParameter("@CREATED_BY",objDriverDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objDriverDetailsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@WORK_PHONE",objDriverDetailsInfo.WORK_PHONE);
				objDataWrapper.AddParameter("@EXTENSION",objDriverDetailsInfo.EXTENSION);	
			
				objDataWrapper.AddParameter("@RELATION_INSURED",objDriverDetailsInfo.RELATION_INSURED);
				if(objDriverDetailsInfo.DATE_OF_BIRTH!=DateTime.MinValue)
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",objDriverDetailsInfo.DATE_OF_BIRTH);
				else
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",System.DBNull.Value);
				objDataWrapper.AddParameter("@LICENSE_NUMBER",objDriverDetailsInfo.LICENSE_NUMBER);
				objDataWrapper.AddParameter("@LICENSE_STATE",objDriverDetailsInfo.LICENSE_STATE);
				objDataWrapper.AddParameter("@PURPOSE_OF_USE",objDriverDetailsInfo.PURPOSE_OF_USE);				
				objDataWrapper.AddParameter("@USED_WITH_PERMISSION",objDriverDetailsInfo.USED_WITH_PERMISSION);
				objDataWrapper.AddParameter("@DESCRIBE_DAMAGE",objDriverDetailsInfo.DESCRIBE_DAMAGE);
				objDataWrapper.AddParameter("@ESTIMATE_AMOUNT",objDriverDetailsInfo.ESTIMATE_AMOUNT);
				objDataWrapper.AddParameter("@OTHER_VEHICLE_INSURANCE",objDriverDetailsInfo.OTHER_VEHICLE_INSURANCE);				
				objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);				
				objDataWrapper.AddParameter("@COUNTRY",objDriverDetailsInfo.COUNTRY);				
				objDataWrapper.AddParameter("@SEX",objDriverDetailsInfo.SEX);				
				objDataWrapper.AddParameter("@SSN",objDriverDetailsInfo.SSN);
				objDataWrapper.AddParameter("@VEHICLE_OWNER",objDriverDetailsInfo.VEHICLE_OWNER);
				objDataWrapper.AddParameter("@TYPE_OF_OWNER",objDriverDetailsInfo.TYPE_OF_OWNER);
				objDataWrapper.AddParameter("@DRIVERS_INJURY",objDriverDetailsInfo.DRIVERS_INJURY);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					//Done for Itrack Issue 6932 on 3 June 2010
					if(lobID == 1 || lobID == 4 || lobID == 6)
						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddWatercraftDriverDetails.aspx.resx");
					else
						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddDriverDetails.aspx.resx");

					string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 1 Feb 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;					
					objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1688","");//"Driver Details is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 1 Feb 2010
					returnResult = objDataWrapper.ExecuteNonQuery(InsertCLM_DRIVER_INFORMATION,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_DRIVER_INFORMATION);
				

				int DRIVER_ID = 0;
				if (returnResult>0)
				{
					DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());					
					objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
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

		#region Update Claims Notification method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldLocationInfo">Model object having old information</param>
		/// <param name="objLocationInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(Cms.Model.Claims.ClsDriverDetailsInfo objOldDriverDetailsInfo, Cms.Model.Claims.ClsDriverDetailsInfo objDriverDetailsInfo)
		{
			int returnResult = 0;	
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);						
			try 
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objDriverDetailsInfo.CLAIM_ID);
				//Done for Itrack Issue 6932 on 9 Feb 2010
				DataSet ds = new DataSet();
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				int lobID = int.Parse(ds.Tables[0].Rows[0]["LOB_ID"].ToString());//Done for Itrack Issue 6932 on 3 June 2010

				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);
				//objDataWrapper.AddParameter("@CLAIM_ID",objDriverDetailsInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@TYPE_OF_DRIVER",objDriverDetailsInfo.TYPE_OF_DRIVER);
				objDataWrapper.AddParameter("@NAME",objDriverDetailsInfo.NAME);
				objDataWrapper.AddParameter("@ADDRESS1",objDriverDetailsInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objDriverDetailsInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objDriverDetailsInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objDriverDetailsInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objDriverDetailsInfo.ZIP);
				objDataWrapper.AddParameter("@HOME_PHONE",objDriverDetailsInfo.HOME_PHONE);
				objDataWrapper.AddParameter("@WORK_PHONE",objDriverDetailsInfo.WORK_PHONE);
				objDataWrapper.AddParameter("@MOBILE_PHONE",objDriverDetailsInfo.MOBILE_PHONE);
				objDataWrapper.AddParameter("@EXTENSION",objDriverDetailsInfo.EXTENSION);				
				objDataWrapper.AddParameter("@MODIFIED_BY",objDriverDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objDriverDetailsInfo.@LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@RELATION_INSURED",objDriverDetailsInfo.RELATION_INSURED);
				if(objDriverDetailsInfo.DATE_OF_BIRTH!=DateTime.MinValue)
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",objDriverDetailsInfo.DATE_OF_BIRTH);
				else
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",System.DBNull.Value);
				objDataWrapper.AddParameter("@LICENSE_NUMBER",objDriverDetailsInfo.LICENSE_NUMBER);
				objDataWrapper.AddParameter("@LICENSE_STATE",objDriverDetailsInfo.LICENSE_STATE);
				objDataWrapper.AddParameter("@PURPOSE_OF_USE",objDriverDetailsInfo.PURPOSE_OF_USE);				
				objDataWrapper.AddParameter("@USED_WITH_PERMISSION",objDriverDetailsInfo.USED_WITH_PERMISSION);
				objDataWrapper.AddParameter("@DESCRIBE_DAMAGE",objDriverDetailsInfo.DESCRIBE_DAMAGE);
				objDataWrapper.AddParameter("@ESTIMATE_AMOUNT",objDriverDetailsInfo.ESTIMATE_AMOUNT);
				objDataWrapper.AddParameter("@OTHER_VEHICLE_INSURANCE",objDriverDetailsInfo.OTHER_VEHICLE_INSURANCE);				
				objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);	
				objDataWrapper.AddParameter("@COUNTRY",objDriverDetailsInfo.COUNTRY);
				objDataWrapper.AddParameter("@SEX",objDriverDetailsInfo.SEX);
				objDataWrapper.AddParameter("@SSN",objDriverDetailsInfo.SSN);
				objDataWrapper.AddParameter("@VEHICLE_OWNER",objDriverDetailsInfo.VEHICLE_OWNER);
				objDataWrapper.AddParameter("@TYPE_OF_OWNER",objDriverDetailsInfo.TYPE_OF_OWNER);
				objDataWrapper.AddParameter("@DRIVERS_INJURY",objDriverDetailsInfo.DRIVERS_INJURY);
				if(TransactionLogRequired) 
				{
					//Done for Itrack Issue 6932 on 3 June 2010
					if(lobID == 1 || lobID == 4 || lobID == 6)
						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddWatercraftDriverDetails.aspx.resx");
					else
						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddDriverDetails.aspx.resx");

					string strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo,objDriverDetailsInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;			
					//Done for Itrack Issue 6932 on 9 Feb 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;	
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1689", "");// "Driver Details is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 9 Feb 2010
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_DRIVER_INFORMATION,objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_DRIVER_INFORMATION);
				objDataWrapper.ClearParameteres();
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
				if(objBuilder != null) 
				{
					objBuilder = null;
				}
			}
		}
		
		
		
		#endregion

		#region Get Driver Details Old Data
		/// <summary>
		/// Returns only the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static DataSet GetDriverDetails(int DRIVER_ID,int CLAIM_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@DRIVER_ID",DRIVER_ID);
			objWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);			
			
			DataSet ds = objWrapper.ExecuteDataSet(GetCLM_DRIVER_INFORMATION);			
			if(ds!=null && ds.Tables.Count>0)
				return ds;
			else
				return null;
		}

		#endregion						

		
		public static int OwnerExistsForClaim(int intCLAIM_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);							
			try
			{
				objWrapper.AddParameter("@CLAIM_ID",intCLAIM_ID);	
				SqlParameter retParam = (SqlParameter)objWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);			
				objWrapper.ExecuteNonQuery(CheckForOwnerData);			
				int returnValue = Convert.ToInt32(retParam.Value);
				return returnValue;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objWrapper!=null)
					objWrapper.Dispose();
			}
		}
		public DataTable GetClaimBoats(int ClaimID)
		{
			string strSql = "Proc_GetClaimBoats";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",ClaimID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.Tables[0];
		}

		#region Get List of Drivers against current policy
		public static DataTable GetPolicyDriversList(int CUSTOMER_ID,int POLICY_ID, int POLICY_VERSION_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",POLICY_ID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID);
			
			DataSet ds = objWrapper.ExecuteDataSet(GET_POLICY_DRIVERS_LIST);			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}
		#endregion
		
		#region Get Details of Drivers selected
		public static DataTable GetPolicyDriversDetails(int CUSTOMER_ID,int POLICY_ID, int POLICY_VERSION_ID,int DRIVER_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",POLICY_ID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID);
			objWrapper.AddParameter("@DRIVER_ID",DRIVER_ID);			
			
			DataSet ds = objWrapper.ExecuteDataSet(GET_POLICY_DRIVERS_DETAILS);			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}
		#endregion

		#region Get List of owners for the current claim_id
		public static DataTable GetOwnersList(int intCLAIM_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);							
			DataSet dsOwners = new DataSet();
			try
			{
				objWrapper.AddParameter("@CLAIM_ID",intCLAIM_ID);	
				SqlParameter retParam = (SqlParameter)objWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);			
				dsOwners = objWrapper.ExecuteDataSet(GET_OWNERS_LIST);
				if(dsOwners!=null && dsOwners.Tables.Count>0)
					return dsOwners.Tables[0];
				else
					return null;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objWrapper!=null)
					objWrapper.Dispose();
				if(dsOwners!=null)
					dsOwners.Dispose();
			}
		}
		#endregion

		#region GetClaimsDriverLookUp
		public static DataSet GetClaimsDriverLookUp(int CUSTOMER_ID,int POLICY_ID, int POLICY_VERSION_ID, int CLAIM_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);							
			DataSet dsLookup = new DataSet();
			try
			{
				objWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);	
				objWrapper.AddParameter("@POLICY_ID",POLICY_ID);	
				objWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID);	
				objWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);	
				
				dsLookup = objWrapper.ExecuteDataSet(GetClaimDriversInformationLookupUp);
				if(dsLookup!=null && dsLookup.Tables.Count>0)
					return dsLookup;
				else
					return null;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objWrapper!=null)
					objWrapper.Dispose();
				if(dsLookup!=null)
					dsLookup.Dispose();
			}
		}
		#endregion
		
		//Done for Itrack Issue 6053 on 9 Sept 2009
		public int DeleteDriverDetails(int claimID, int driverID,ClsDriverDetailsInfo objDriverDetailsInfo)
		{
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			string CLM_DELETEDRIVER= "PROC_CLM_DELETEDRIVER";
			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",claimID);
				//Done for Itrack Issue 6932 on 2 Feb 2010
				DataSet ds = new DataSet();
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@DRIVER_ID",driverID);

				if(TransactionLogRequired) 
				{		
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddDriverDetails.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();									
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
					//Done for Itrack Issue 6932 on 2 Feb 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1690", "");// "Driver Details is deleted";
					objTransactionInfo.CHANGE_XML		=	 strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 2 Feb 2010
					returnResult = objDataWrapper.ExecuteNonQuery(CLM_DELETEDRIVER,objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery(CLM_DELETEDRIVER);
				

				return returnResult;
				
			}
			catch(Exception exc)
			{
				throw(exc);
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
		
		public int ActivateDeactivateDriverDetails(int claimID, int driverID, string flagActivateDeactivate,ClsDriverDetailsInfo objDriverDetailsInfo)//Done for Itrack Issue 5833 on 21 July 2009
		{
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			string CLM_ACTIVATEDEACTIVATEDRIVERDETAILS= "PROC_CLM_ACTIVATEDEACTIVATEDRIVERDETAILS";
			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",claimID);
				//Done for Itrack Issue 6932 on 2 Feb 2010
				DataSet ds = new DataSet();
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@DRIVER_ID",driverID);
				objDataWrapper.AddParameter("@ACTIVATEDEACTIVATE",flagActivateDeactivate);
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddDriverDetails.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);									
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
					//Done for Itrack Issue 6932 on 2 Feb 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;

					if(flagActivateDeactivate=="Y")
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1691","");//"Driver Details is Activated";
					else
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1692", "");// "Driver Details is Deactivated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 2 Feb 2010
					returnResult = objDataWrapper.ExecuteNonQuery(CLM_ACTIVATEDEACTIVATEDRIVERDETAILS,objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery(CLM_ACTIVATEDEACTIVATEDRIVERDETAILS);

				return returnResult;
				
			}
			catch(Exception exc)
			{
				throw(exc);
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
	//Added till here	
	}
}
