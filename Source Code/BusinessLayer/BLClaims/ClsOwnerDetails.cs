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
	/// Summary description for ClsOwnerDetails.
	/// </summary>
	public class ClsOwnerDetails : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		GetCLM_OWNER_INFORMATION		=	"Proc_GetCLM_OWNER_INFORMATION";
		private const	string		InsertCLM_OWNER_INFORMATION		=	"Proc_InsertCLM_OWNER_INFORMATION";
		private const	string		UpdateCLM_OWNER_INFORMATION		=	"Proc_UpdateCLM_OWNER_INFORMATION";
		private const	string		Get_NamedInsured				=	"Proc_GetNamedInsured";
		private const	string		Get_NamedInsuredDetails			=	"Proc_GetNamedInsuredDetails";
		
		
		public ClsOwnerDetails()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Add(Insert) functions
		public int Add(ClsOwnerDetailsInfo objOwnerDetailsInfo)
		{			
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			try
			{				
				objDataWrapper.AddParameter("@CLAIM_ID",objOwnerDetailsInfo.CLAIM_ID);
				//Done for Itrack Issue 6932 on 1 Feb 2010
				DataSet ds = new DataSet();
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@TYPE_OF_OWNER",objOwnerDetailsInfo.TYPE_OF_OWNER);
				objDataWrapper.AddParameter("@NAME",objOwnerDetailsInfo.NAME);
				objDataWrapper.AddParameter("@ADDRESS1",objOwnerDetailsInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objOwnerDetailsInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objOwnerDetailsInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objOwnerDetailsInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objOwnerDetailsInfo.ZIP);
				objDataWrapper.AddParameter("@HOME_PHONE",objOwnerDetailsInfo.HOME_PHONE);
				objDataWrapper.AddParameter("@MOBILE_PHONE",objOwnerDetailsInfo.MOBILE_PHONE);
				objDataWrapper.AddParameter("@DEFAULT_PHONE_TO_NOTICE",objOwnerDetailsInfo.DEFAULT_PHONE_TO_NOTICE);
				objDataWrapper.AddParameter("@PRODUCTS_INSURED_IS",objOwnerDetailsInfo.PRODUCTS_INSURED_IS);
				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objOwnerDetailsInfo.OTHER_DESCRIPTION);
				objDataWrapper.AddParameter("@TYPE_OF_PRODUCT",objOwnerDetailsInfo.TYPE_OF_PRODUCT);
				objDataWrapper.AddParameter("@WHERE_PRODUCT_SEEN",objOwnerDetailsInfo.WHERE_PRODUCT_SEEN);
				objDataWrapper.AddParameter("@OTHER_LIABILITY",objOwnerDetailsInfo.OTHER_LIABILITY);
				objDataWrapper.AddParameter("@CREATED_BY",objOwnerDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objOwnerDetailsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@WORK_PHONE",objOwnerDetailsInfo.WORK_PHONE);
				objDataWrapper.AddParameter("@EXTENSION",objOwnerDetailsInfo.EXTENSION);
				objDataWrapper.AddParameter("@VEHICLE_OWNER",objOwnerDetailsInfo.VEHICLE_OWNER);				
				objDataWrapper.AddParameter("@TYPE_OF_HOME",objOwnerDetailsInfo.TYPE_OF_HOME);				
				objDataWrapper.AddParameter("@VEHICLE_ID",objOwnerDetailsInfo.VEHICLE_ID);				
				objDataWrapper.AddParameter("@COUNTRY",objOwnerDetailsInfo.COUNTRY);				

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@OWNER_ID",objOwnerDetailsInfo.OWNER_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					objOwnerDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddOwnerDetails.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objOwnerDetailsInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 1 Feb 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objOwnerDetailsInfo.CREATED_BY;					
					objTransactionInfo.TRANS_DESC		=	"Owner Details is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 1 Feb 2010
					returnResult = objDataWrapper.ExecuteNonQuery(InsertCLM_OWNER_INFORMATION,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_OWNER_INFORMATION);
				

				int OWNER_ID = 0;
				if (returnResult>0)
				{
					OWNER_ID = int.Parse(objSqlParameter.Value.ToString());					
					objOwnerDetailsInfo.OWNER_ID = OWNER_ID;
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
		public int Update(Cms.Model.Claims.ClsOwnerDetailsInfo objOldOwnerDetailsInfo, Cms.Model.Claims.ClsOwnerDetailsInfo objOwnerDetailsInfo)
		{
			int returnResult = 0;	
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);						
			try 
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objOwnerDetailsInfo.CLAIM_ID);
				//Done for Itrack Issue 6932 on 9 Feb 2010
				DataSet ds = new DataSet();
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@OWNER_ID",objOwnerDetailsInfo.OWNER_ID);
				//objDataWrapper.AddParameter("@CLAIM_ID",objOwnerDetailsInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@TYPE_OF_OWNER",objOwnerDetailsInfo.TYPE_OF_OWNER);
				objDataWrapper.AddParameter("@NAME",objOwnerDetailsInfo.NAME);
				objDataWrapper.AddParameter("@ADDRESS1",objOwnerDetailsInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objOwnerDetailsInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objOwnerDetailsInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objOwnerDetailsInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objOwnerDetailsInfo.ZIP);
				objDataWrapper.AddParameter("@HOME_PHONE",objOwnerDetailsInfo.HOME_PHONE);
				objDataWrapper.AddParameter("@MOBILE_PHONE",objOwnerDetailsInfo.MOBILE_PHONE);
				objDataWrapper.AddParameter("@DEFAULT_PHONE_TO_NOTICE",objOwnerDetailsInfo.DEFAULT_PHONE_TO_NOTICE);
				objDataWrapper.AddParameter("@PRODUCTS_INSURED_IS",objOwnerDetailsInfo.PRODUCTS_INSURED_IS);
				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objOwnerDetailsInfo.OTHER_DESCRIPTION);
				objDataWrapper.AddParameter("@TYPE_OF_PRODUCT",objOwnerDetailsInfo.TYPE_OF_PRODUCT);
				objDataWrapper.AddParameter("@WHERE_PRODUCT_SEEN",objOwnerDetailsInfo.WHERE_PRODUCT_SEEN);
				objDataWrapper.AddParameter("@OTHER_LIABILITY",objOwnerDetailsInfo.OTHER_LIABILITY);
				objDataWrapper.AddParameter("@MODIFIED_BY",objOwnerDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objOwnerDetailsInfo.@LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@WORK_PHONE",objOwnerDetailsInfo.WORK_PHONE);
				objDataWrapper.AddParameter("@EXTENSION",objOwnerDetailsInfo.EXTENSION);
				objDataWrapper.AddParameter("@VEHICLE_OWNER",objOwnerDetailsInfo.VEHICLE_OWNER);
				objDataWrapper.AddParameter("@VEHICLE_ID",objOwnerDetailsInfo.VEHICLE_ID);	
				objDataWrapper.AddParameter("@COUNTRY",objOwnerDetailsInfo.COUNTRY);				
				
				if(TransactionLogRequired) 
				{
					objOwnerDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddOwnerDetails.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objOldOwnerDetailsInfo,objOwnerDetailsInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;	
					//Done for Itrack Issue 6932 on 9 Feb 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;			
					objTransactionInfo.RECORDED_BY		=	objOwnerDetailsInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Owner Details is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 9 Feb 2010
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_OWNER_INFORMATION,objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_OWNER_INFORMATION);
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

		#region Get Claims Notification Old Data
		/// <summary>
		/// Returns only the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static DataSet GetOwnerDetails(int OWNER_ID,int CLAIM_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@OWNER_ID",OWNER_ID);
			objWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);			
			
			DataSet ds = objWrapper.ExecuteDataSet(GetCLM_OWNER_INFORMATION);			
			if(ds!=null && ds.Tables.Count>0)
				return ds;
			else
				return null;
		}

		#endregion				

		#region Get Named Insured 
		/// <summary>
		/// Returns only the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static DataTable GetNamedInsured(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID,int VEHICLE_OWNER,int CLAIM_ID,int vehicleID)//Added for Itrack Issue 6053 on 31 July 2009
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",POLICY_ID);			
			objWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID);			
			objWrapper.AddParameter("@VEHICLE_OWNER",VEHICLE_OWNER);
			objWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			//Added for Itrack Issue 6053 on 31 July 2009
			if(vehicleID!=0)
			{
				objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			}
			  
			DataSet ds = objWrapper.ExecuteDataSet(Get_NamedInsured);			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}

		/*public static DataTable GetNamedInsured(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID,int VEHICLE_OWNER,int CLAIM_ID, int NAME_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",POLICY_ID);			
			objWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID);			
			objWrapper.AddParameter("@VEHICLE_OWNER",VEHICLE_OWNER);
			objWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);			
			objWrapper.AddParameter("@NAME_ID",NAME_ID);			
			
			DataSet ds = objWrapper.ExecuteDataSet(Get_NamedInsured);			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}*/

		public static DataTable GetNamedInsured(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID,int VEHICLE_OWNER,int CLAIM_ID, string strNAME_ID)
		{
			return GetNamedInsured(CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,VEHICLE_OWNER,CLAIM_ID, strNAME_ID,"",0);//Added for Itrack Issue 6053 on 31 July 2009
		}

		public static DataTable GetNamedInsured(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID,int VEHICLE_OWNER,int CLAIM_ID, string strNAME_ID, string strDRIVER_TYPE,int vehicleID)//Added for Itrack Issue 6053 on 31 July 2009
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",POLICY_ID);			
			objWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID);			
			objWrapper.AddParameter("@VEHICLE_OWNER",VEHICLE_OWNER);
			objWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);			
			objWrapper.AddParameter("@DRIVER_TYPE",strDRIVER_TYPE);
			//Added for Itrack Issue 6053 on 31 July 2009
			if(vehicleID!=0)
			{
				objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			}
			if(strNAME_ID!="" && strNAME_ID.Split('^').Length>0)
			{
				objWrapper.AddParameter("@NAME_ID",strNAME_ID.Split('^')[0].ToString());			
				objWrapper.AddParameter("@NAMED_INSURED_TYPE",strNAME_ID.Split('^')[1].ToString());			
			}
			
			
			DataSet ds = objWrapper.ExecuteDataSet(Get_NamedInsured);			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}


		#endregion				

		#region Get Named Insured  Details
		/// <summary>
		/// Returns only the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static DataTable GetNamedInsureDetails(int VEHICLE_OWNER, int NAMED_INSURED_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@VEHICLE_OWNER",VEHICLE_OWNER);
			objWrapper.AddParameter("@NAMED_INSURED_ID",NAMED_INSURED_ID);						
			
			DataSet ds = objWrapper.ExecuteDataSet(Get_NamedInsuredDetails);			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}

		#endregion				
		
	}
}
