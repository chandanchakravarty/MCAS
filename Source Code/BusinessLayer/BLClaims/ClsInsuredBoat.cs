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
	/// Summary description for ClsInsuredBoat.
	/// </summary>
	public class ClsInsuredBoat : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		GetCLM_INSURED_BOAT				=	"Proc_GetCLM_INSURED_BOAT";
		private const	string		InsertCLM_INSURED_BOAT			=	"Proc_InsertCLM_INSURED_BOAT";
		private const	string		UpdateCLM_INSURED_BOAT  		=	"Proc_UpdateCLM_INSURED_BOAT";	
		private const	string		GetPOLICY_BOAT			 		=	"Proc_GetPOLICY_BOAT";	
		
		
		
		public ClsInsuredBoat()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Add(Insert) functions
		public int Add(Cms.Model.Claims.ClsInsuredBoatInfo objInsuredBoatInfo)
		{			
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			try
			{				
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objInsuredBoatInfo.CLAIM_ID);

				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				
				objDataWrapper.AddParameter("@SERIAL_NUMBER",objInsuredBoatInfo.SERIAL_NUMBER);
				objDataWrapper.AddParameter("@YEAR",objInsuredBoatInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objInsuredBoatInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objInsuredBoatInfo.MODEL);
				objDataWrapper.AddParameter("@BODY_TYPE",objInsuredBoatInfo.BODY_TYPE);
				objDataWrapper.AddParameter("@LENGTH",objInsuredBoatInfo.LENGTH);
				objDataWrapper.AddParameter("@WEIGHT",objInsuredBoatInfo.WEIGHT);
				objDataWrapper.AddParameter("@HORSE_POWER",objInsuredBoatInfo.HORSE_POWER);
				objDataWrapper.AddParameter("@OTHER_HULL_TYPE",objInsuredBoatInfo.OTHER_HULL_TYPE);
				objDataWrapper.AddParameter("@PLATE_NUMBER",objInsuredBoatInfo.PLATE_NUMBER);
				objDataWrapper.AddParameter("@STATE",objInsuredBoatInfo.STATE);
				objDataWrapper.AddParameter("@INCLUDE_TRAILER",objInsuredBoatInfo.INCLUDE_TRAILER);
				objDataWrapper.AddParameter("@CREATED_BY",objInsuredBoatInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objInsuredBoatInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@POLICY_BOAT_ID",objInsuredBoatInfo.POLICY_BOAT_ID);
				objDataWrapper.AddParameter("@WHERE_BOAT_SEEN",objInsuredBoatInfo.WHERE_BOAT_SEEN);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@BOAT_ID",objInsuredBoatInfo.BOAT_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					objInsuredBoatInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddInsuredBoat.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objInsuredBoatInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 19 Jan 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objInsuredBoatInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1742", "");// "Insured Boat is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 19 Jan 2010
					returnResult = objDataWrapper.ExecuteNonQuery(InsertCLM_INSURED_BOAT,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_INSURED_BOAT);
				

				int intBOAT_ID = 0;
				if (returnResult>0)
				{
					intBOAT_ID = int.Parse(objSqlParameter.Value.ToString());					
					objInsuredBoatInfo.BOAT_ID = intBOAT_ID;
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

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldLocationInfo">Model object having old information</param>
		/// <param name="objLocationInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(Cms.Model.Claims.ClsInsuredBoatInfo objOldInsuredBoatInfo,Cms.Model.Claims.ClsInsuredBoatInfo objInsuredBoatInfo)
		{
			int returnResult = 0;	
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);						
			try 
			{
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objInsuredBoatInfo.CLAIM_ID);

				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@BOAT_ID",objInsuredBoatInfo.BOAT_ID);
				//objDataWrapper.AddParameter("@CLAIM_ID",objInsuredBoatInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@SERIAL_NUMBER",objInsuredBoatInfo.SERIAL_NUMBER);
				objDataWrapper.AddParameter("@YEAR",objInsuredBoatInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objInsuredBoatInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objInsuredBoatInfo.MODEL);
				objDataWrapper.AddParameter("@BODY_TYPE",objInsuredBoatInfo.BODY_TYPE);
				objDataWrapper.AddParameter("@LENGTH",objInsuredBoatInfo.LENGTH);
				objDataWrapper.AddParameter("@WEIGHT",objInsuredBoatInfo.WEIGHT);
				objDataWrapper.AddParameter("@HORSE_POWER",objInsuredBoatInfo.HORSE_POWER);
				objDataWrapper.AddParameter("@OTHER_HULL_TYPE",objInsuredBoatInfo.OTHER_HULL_TYPE);
				objDataWrapper.AddParameter("@PLATE_NUMBER",objInsuredBoatInfo.PLATE_NUMBER);
				objDataWrapper.AddParameter("@STATE",objInsuredBoatInfo.STATE);
				objDataWrapper.AddParameter("@INCLUDE_TRAILER",objInsuredBoatInfo.INCLUDE_TRAILER);
				objDataWrapper.AddParameter("@MODIFIED_BY",objInsuredBoatInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objInsuredBoatInfo.LAST_UPDATED_DATETIME);
				//objDataWrapper.AddParameter("@POLICY_BOAT_ID",objInsuredBoatInfo.POLICY_BOAT_ID);
				objDataWrapper.AddParameter("@WHERE_BOAT_SEEN",objInsuredBoatInfo.WHERE_BOAT_SEEN);
				if(TransactionLogRequired) 
				{
					objInsuredBoatInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddInsuredBoat.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objOldInsuredBoatInfo,objInsuredBoatInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 19 Jan 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objInsuredBoatInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1743", "");// "Insured Boat is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 19 Jan 2010
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_INSURED_BOAT,objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_INSURED_BOAT);

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

		#region Get Old Data
		/// <summary>
		/// Returns only the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static DataTable GetOldDataForPageControls(string CLAIM_ID,string strBOAT_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			objWrapper.AddParameter("@BOAT_ID",strBOAT_ID);
			DataSet ds = objWrapper.ExecuteDataSet(GetCLM_INSURED_BOAT);			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}

		public static bool IncludeTrailer(string ClaimID,string PolicyBoatID)
		{
			bool ReturnValue = false; 
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@CLAIM_ID",ClaimID);
			objWrapper.AddParameter("@POLICY_BOAT_ID",PolicyBoatID);
			
			SqlParameter objRetVal  = (SqlParameter) objWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.Output);
			
			objWrapper.ExecuteNonQuery("Proc_CheckTrailerIncluded");

			if(objRetVal != null && objRetVal.Value != DBNull.Value )
			{
				if(Convert.ToInt32(objRetVal.Value) == 1) 
				{
					ReturnValue = true;
				}
			}
			return ReturnValue; 
		}


		#endregion


		public static DataTable GetPolicyBoats(string strCLAIM_ID,string strBOAT_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);
			objWrapper.AddParameter("@BOAT_ID",strBOAT_ID);
			DataSet ds = objWrapper.ExecuteDataSet(GetPOLICY_BOAT);			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}
		
		//Added for Itrack Issue 5833 on 21 July 2009
		public int CheckClaimActivityReserve(int claimID,int insuredVehicleID,string calledFrom)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				string strSql = "Proc_CheckCLM_ACTIVITY_RESERVE";
				objDataWrapper.ClearParameteres();			
				objDataWrapper.AddParameter("@CLAIM_ID",claimID);
				objDataWrapper.AddParameter("@INSUREDVEHICLE_LOCATION_BOAT_ID",insuredVehicleID);
				objDataWrapper.AddParameter("@CALLED_FROM",calledFrom.ToUpper());
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RESULT",null,SqlDbType.Int,ParameterDirection.Output);
				int result= objDataWrapper.ExecuteNonQuery(strSql);
				result=  int.Parse(objSqlParameter.Value.ToString());
				return result;

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

		public int DeleteBoat(Cms.Model.Claims.ClsInsuredBoatInfo objInsuredBoatInfo,string calledFrom)//Modified for Itrack Issue 6932 on 4 June 2010
		{
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			
			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objInsuredBoatInfo.CLAIM_ID);
				//Modified for Itrack Issue 6932 on 4 June 2010
				DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@INSUREDVEHICLE_LOCATION_BOAT_ID",objInsuredBoatInfo.BOAT_ID);
				objDataWrapper.AddParameter("@CALLED_FROM",calledFrom);

				//Done for Itrack Issue 6932 on 3 June 2010
				if(TransactionLogRequired) 
				{
					objInsuredBoatInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddInsuredBoat.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objInsuredBoatInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objInsuredBoatInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1744", "");// "Insured Boat is deleted";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;
					returnResult = objDataWrapper.ExecuteNonQuery("PROC_DELETEINSUREDVEHICLE",objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery("PROC_DELETEINSUREDVEHICLE");

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
		
		public int ActivateDeactivateBoat(Cms.Model.Claims.ClsInsuredBoatInfo objInsuredBoatInfo, string flagActivateDeactivate,string calledFrom)//Modified for Itrack Issue 6932 on 4 June 2010
		{
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			
			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objInsuredBoatInfo.CLAIM_ID);
				//Modified for Itrack Issue 6932 on 4 June 2010
				DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@INSUREDVEHICLE_LOCATION_BOAT_ID",objInsuredBoatInfo.BOAT_ID);
				objDataWrapper.AddParameter("@ACTIVATEDEACTIVATE",flagActivateDeactivate);
				objDataWrapper.AddParameter("@CALLED_FROM",calledFrom);

				//Done for Itrack Issue 6932 on 3 June 2010
				if(TransactionLogRequired) 
				{
					objInsuredBoatInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddInsuredBoat.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objInsuredBoatInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objInsuredBoatInfo.CREATED_BY;
					if(flagActivateDeactivate == "N")
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1745","");//"Insured Boat is Deactivated";
					else
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1746","");//"Insured Boat is Activated";

					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;
					returnResult = objDataWrapper.ExecuteNonQuery("PROC_ACTIVATEDEACTIVATEINSUREDVEHICLE",objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery("PROC_ACTIVATEDEACTIVATEINSUREDVEHICLE");

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
