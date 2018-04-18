/******************************************************************************************
<Author					: - Amar
<Start Date				: -	5/1/2006 5:17:10 PM
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
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
	/// 
	/// </summary>
	public class ClsInsuredVehicle :  Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		CLM_INSURED_VEHICLE			=	"CLM_INSURED_VEHICLE";
		
		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _INSURED_VEHICLE_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "";
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
		public ClsInsuredVehicle()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objInsuredVehicleInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsInsuredVehicleInfo objInsuredVehicleInfo)
		{
			string		strStoredProc	=	"Proc_InsertCLM_INSURED_VEHICLE";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objInsuredVehicleInfo.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@NON_OWNED_VEHICLE",objInsuredVehicleInfo.NON_OWNED_VEHICLE);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objInsuredVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objInsuredVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objInsuredVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objInsuredVehicleInfo.VIN);
				objDataWrapper.AddParameter("@BODY_TYPE",objInsuredVehicleInfo.BODY_TYPE);
				objDataWrapper.AddParameter("@PLATE_NUMBER",objInsuredVehicleInfo.PLATE_NUMBER);
				objDataWrapper.AddParameter("@STATE",objInsuredVehicleInfo.STATE);
				objDataWrapper.AddParameter("@IS_ACTIVE",objInsuredVehicleInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objInsuredVehicleInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objInsuredVehicleInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@OWNER_ID",objInsuredVehicleInfo.OWNER_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objInsuredVehicleInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@WHERE_VEHICLE_SEEN",objInsuredVehicleInfo.WHERE_VEHICLE_SEEN);
				objDataWrapper.AddParameter("@WHEN_VEHICLE_SEEN",objInsuredVehicleInfo.WHEN_VEHICLE_SEEN);
				objDataWrapper.AddParameter("@PURPOSE_OF_USE",objInsuredVehicleInfo.PURPOSE_OF_USE);				
				objDataWrapper.AddParameter("@USED_WITH_PERMISSION",objInsuredVehicleInfo.USED_WITH_PERMISSION);
				objDataWrapper.AddParameter("@DESCRIBE_DAMAGE",objInsuredVehicleInfo.DESCRIBE_DAMAGE);
				if(objInsuredVehicleInfo.ESTIMATE_AMOUNT==0 || objInsuredVehicleInfo.ESTIMATE_AMOUNT==0.0)
					objDataWrapper.AddParameter("@ESTIMATE_AMOUNT",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@ESTIMATE_AMOUNT",objInsuredVehicleInfo.ESTIMATE_AMOUNT);
				objDataWrapper.AddParameter("@OTHER_VEHICLE_INSURANCE",objInsuredVehicleInfo.OTHER_VEHICLE_INSURANCE);				
				objDataWrapper.AddParameter("@POLICY_VEHICLE_ID",objInsuredVehicleInfo.POLICY_VEHICLE_ID);				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@INSURED_VEHICLE_ID",objInsuredVehicleInfo.INSURED_VEHICLE_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objInsuredVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddInsuredVehicle.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objInsuredVehicleInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//Done for Itrack Issue 6932 on 19 Jan 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objInsuredVehicleInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1747", "");// "Vehicle Information has been added";//Done for Itrack Issue 6932 on 1 Feb 2010
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 19 Jan 2010
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int INSURED_VEHICLE_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (INSURED_VEHICLE_ID == -1)
				{
					return -1;
				}
				else
				{
					objInsuredVehicleInfo.INSURED_VEHICLE_ID = INSURED_VEHICLE_ID;
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
		/// <param name="objOldInsuredVehicleInfo">Model object having old information</param>
		/// <param name="objInsuredVehicleInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsInsuredVehicleInfo objOldInsuredVehicleInfo,ClsInsuredVehicleInfo objInsuredVehicleInfo)
		{
			string		strStoredProc	=	"Proc_UpdateCLM_INSURED_VEHICLE";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objInsuredVehicleInfo.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@INSURED_VEHICLE_ID",objInsuredVehicleInfo.INSURED_VEHICLE_ID);
				//objDataWrapper.AddParameter("@CLAIM_ID",objInsuredVehicleInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@NON_OWNED_VEHICLE",objInsuredVehicleInfo.NON_OWNED_VEHICLE);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objInsuredVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objInsuredVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objInsuredVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objInsuredVehicleInfo.VIN);
				objDataWrapper.AddParameter("@BODY_TYPE",objInsuredVehicleInfo.BODY_TYPE);
				objDataWrapper.AddParameter("@PLATE_NUMBER",objInsuredVehicleInfo.PLATE_NUMBER);
				objDataWrapper.AddParameter("@STATE",objInsuredVehicleInfo.STATE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objInsuredVehicleInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objInsuredVehicleInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@OWNER_ID",objInsuredVehicleInfo.OWNER_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objInsuredVehicleInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@WHERE_VEHICLE_SEEN",objInsuredVehicleInfo.WHERE_VEHICLE_SEEN);
				objDataWrapper.AddParameter("@WHEN_VEHICLE_SEEN",objInsuredVehicleInfo.WHEN_VEHICLE_SEEN);
				objDataWrapper.AddParameter("@PURPOSE_OF_USE",objInsuredVehicleInfo.PURPOSE_OF_USE);				
				objDataWrapper.AddParameter("@USED_WITH_PERMISSION",objInsuredVehicleInfo.USED_WITH_PERMISSION);
				objDataWrapper.AddParameter("@DESCRIBE_DAMAGE",objInsuredVehicleInfo.DESCRIBE_DAMAGE);
				if(objInsuredVehicleInfo.ESTIMATE_AMOUNT==0 || objInsuredVehicleInfo.ESTIMATE_AMOUNT==0.0)
					objDataWrapper.AddParameter("@ESTIMATE_AMOUNT",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@ESTIMATE_AMOUNT",objInsuredVehicleInfo.ESTIMATE_AMOUNT);
				objDataWrapper.AddParameter("@OTHER_VEHICLE_INSURANCE",objInsuredVehicleInfo.OTHER_VEHICLE_INSURANCE);				
				objDataWrapper.AddParameter("@POLICY_VEHICLE_ID",objInsuredVehicleInfo.POLICY_VEHICLE_ID);				
				
				if(base.TransactionLogRequired) 
				{
					objInsuredVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddInsuredVehicle.aspx.resx");
					objBuilder.GetUpdateSQL(objOldInsuredVehicleInfo,objInsuredVehicleInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 19 Jan 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objInsuredVehicleInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1748", "");// "Vehicle Information has Been Updated";//Done for Itrack Issue 6932 on 1 Feb 2010
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 19 Jan 2010
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

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

		#region "GetxmlMethods"
		public static string GetXmlForPageControls(string CLAIM_ID, string INSURED_VEHICLE_ID)
		{
			string strSql = "Proc_GetXMLCLM_INSURED_VEHICLE";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			objDataWrapper.AddParameter("@INSURED_VEHICLE_ID",INSURED_VEHICLE_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if(objDataSet.Tables[0].Rows.Count>0)
				return objDataSet.GetXml();
			else
				return "";
		}
		#endregion


		/// <summary>
		/// Returns all the Vehicles attached with the Claim/Policy
		/// </summary>
		/// <param name="ClaimID"></param>
		/// <returns></returns>
		public DataTable GetPolicyVehicles(int ClaimID)
		{
			string strSql = "Proc_GetPolicyVehicles";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",ClaimID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.Tables[0];
		}

		public DataTable GetPolicyVehicles(int ClaimID, int VehicleID)
		{
			string strSql = "Proc_GetPolicyVehicles";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",ClaimID);
			objDataWrapper.AddParameter("@INSURED_VEHICLE_ID",VehicleID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.Tables[0];
		}

		public DataTable GetVehiclesForClaim(int ClaimID)
		{
			string strSql = "Proc_GetVehiclesForClaim";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",ClaimID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.Tables[0];
		}

		public DataTable GetVehiclesForClaim(int ClaimID, int VehicleID)
		{
			string strSql = "Proc_GetVehiclesForClaim";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",ClaimID);
			objDataWrapper.AddParameter("@VEHICLE_ID",VehicleID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.Tables[0];
		}

		
		/// <summary>
		/// List of owners based on ClaimID
		/// </summary>
		/// <param name="ClaimID"></param>
		/// <returns></returns>
		public DataTable GetClaimOwners(int ClaimID)
		{
			string strSql = "Proc_GetClaimOwners";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",ClaimID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.Tables[0];
		}

		/// <summary>
		/// List of drivers based on ClaimID
		/// </summary>
		/// <param name="ClaimID"></param>
		/// <returns></returns>
		public DataTable GetClaimDrivers(int ClaimID)
		{
			string strSql = "Proc_GetClaimDrivers";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",ClaimID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.Tables[0];
		}

		/// <summary>
		/// Returns all the Vehicles attached with the Claim
		/// </summary>
		/// <param name="ClaimID"></param>
		/// <returns></returns>
		public DataTable GetClaimVehicles(int ClaimID)
		{
			string strSql = "Proc_GetClaimVehicles";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",ClaimID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.Tables[0];
		}
		//Added for Itrack Issue 5833 on 20 May 2009
		public int CheckClaimActivityReserve(int claimID,int insuredVehicleID,string calledFrom)//Done for Itrack Issue 5833 on 21 July 2009
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				string strSql = "Proc_CheckCLM_ACTIVITY_RESERVE";
				objDataWrapper.ClearParameteres();			
				objDataWrapper.AddParameter("@CLAIM_ID",claimID);
				objDataWrapper.AddParameter("@INSUREDVEHICLE_LOCATION_BOAT_ID",insuredVehicleID);//Done for Itrack Issue 5833 on 21 July 2009
				objDataWrapper.AddParameter("@CALLED_FROM",calledFrom.ToUpper());//Done for Itrack Issue 5833 on 21 July 2009
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

		public int DeleteInsuredVehicle(int claimID, int insuredVehicleID,string calledFrom,int createdBy)//Done for Itrack Issue 5833 on 21 July 2009
		{
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			
			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",claimID);
				//Done for Itrack Issue 6932 on 9 Feb 2010
				DataSet ds = new DataSet();
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@INSUREDVEHICLE_LOCATION_BOAT_ID",insuredVehicleID);//Done for Itrack Issue 5833 on 21 July 2009
				objDataWrapper.AddParameter("@CALLED_FROM",calledFrom);//Done for Itrack Issue 5833 on 21 July 2009
				
				//Done for Itrack Issue 6932 on 9 Feb 2010
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	createdBy;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1749", "");// "Vehicle Information has Been Deleted";
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber + ";Vehicle Number = " + insuredVehicleID;//Done for Itrack Issue 6932 on 1 Feb 2010
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
		//Added till here
		//Done for Itrack Issue 5833 on 17 June 2009
		public int ActivateDeactivateInsuredVehicle(int claimID, int insuredVehicleID, string flagActivateDeactivate,string calledFrom,int createdBy)//Done for Itrack Issue 5833 on 21 July 2009
		{
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			
			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",claimID);
				//Done for Itrack Issue 6932 on 9 Feb 2010
				DataSet ds = new DataSet();
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@INSUREDVEHICLE_LOCATION_BOAT_ID",insuredVehicleID);//Done for Itrack Issue 5833 on 21 July 2009
				objDataWrapper.AddParameter("@ACTIVATEDEACTIVATE",flagActivateDeactivate);
				objDataWrapper.AddParameter("@CALLED_FROM",calledFrom);//Done for Itrack Issue 5833 on 21 July 2009

				//Done for Itrack Issue 6932 on 9 Feb 2010
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	createdBy;
					if(flagActivateDeactivate == "N")
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1750","");//"Vehicle Information has Been Deactivated";
					else
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1751", "");// "Vehicle Information has Been Activated";
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber + ";Vehicle Number = " + insuredVehicleID;//Done for Itrack Issue 6932 on 1 Feb 2010
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
	}
}
