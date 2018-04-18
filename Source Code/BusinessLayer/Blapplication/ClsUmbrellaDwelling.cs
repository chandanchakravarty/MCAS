/******************************************************************************************
<Author					: -  Ravindra Kumar Gupta
<Start Date				: -	 Feb 15, 2006
<End Date				: -	
<Description			: -  Business Logic for UMBRELLA DWELLINGS
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -	Ravindra	
<Modified By			: - 03-21-206
<Purpose				: - To Add Policy Level Functions
************************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using Cms.Model.Application;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;


namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsUmbrellaDwelling.
	/// </summary>
	public class ClsUmbrellaDwelling : Cms.BusinessLayer.BlApplication.clsapplication
	{
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAPP_UMBRELLA_DWELLINGS_INFO";

		public ClsUmbrellaDwelling()
		{
			base.strActivateDeactivateProcedure =ACTIVATE_DEACTIVATE_PROC;
		}


		#region GetLocationDetails Function
		//Function to fetch details of a location
		public DataTable GetLocationDetails(int locationID)
		{

			string	strStoredProc	=	"Proc_UM_Get_Location_Details";	
			DataWrapper objDataWrapper=new DataWrapper (ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@LOCATION_ID",locationID);	
			DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
			return ds.Tables[0];
		}
		#endregion

		#region GetPolicyLocationDetails Function
		//Function to fetch details of a location
		public DataTable GetPolicyLocationDetails(int locationID)
		{

			string	strStoredProc	=	"Proc_UM_GetPolicyLocationDetails";	
			DataWrapper objDataWrapper=new DataWrapper (ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@LOCATION_ID",locationID);	
			DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
			return ds.Tables[0];
		}
		#endregion

		#region Add Function
		public int Add(ClsUmbrellaDwellingInfo objInfo,string strCustomInfo)
		{
			string	strStoredProc =	"Proc_UM_InsertAppDwellingsInfo";
			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AddUmbrellaDwelling.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@DWELLING_NUMBER",objInfo.DWELLING_NUMBER);
			objWrapper.AddParameter("@LOCATION_ID",DefaultValues.GetIntNullFromNegative(objInfo.LOCATION_ID));
			objWrapper.AddParameter("@SUB_LOC_ID",DefaultValues.GetIntNullFromNegative(objInfo.SUB_LOC_ID));
			objWrapper.AddParameter("@YEAR_BUILT",DefaultValues.GetIntNullFromNegative(objInfo.YEAR_BUILT));
			objWrapper.AddParameter("@PURCHASE_YEAR",DefaultValues.GetIntNullFromNegative(objInfo.PURCHASE_YEAR));
			objWrapper.AddParameter("@PURCHASE_PRICE",DefaultValues.GetDoubleNullFromNegative(objInfo.PURCHASE_PRICE));
			objWrapper.AddParameter("@MARKET_VALUE",DefaultValues.GetDoubleNullFromNegative(objInfo.MARKET_VALUE));
			objWrapper.AddParameter("@REPLACEMENT_COST",DefaultValues.GetDoubleNullFromNegative(objInfo.REPLACEMENT_COST));
			objWrapper.AddParameter("@BUILDING_TYPE",objInfo.BUILDING_TYPE);
			objWrapper.AddParameter("@OCCUPANCY",objInfo.OCCUPANCY);
			objWrapper.AddParameter("@NEED_OF_UNITS",DefaultValues.GetIntNullFromNegative(objInfo.NEED_OF_UNITS));
			objWrapper.AddParameter("@USAGE",objInfo.USAGE);
			objWrapper.AddParameter("@NEIGHBOURS_VISIBLE",objInfo.NEIGHBOURS_VISIBLE);
			objWrapper.AddParameter("@OCCUPIED_DAILY",objInfo.OCCUPIED_DAILY);
			objWrapper.AddParameter("@NO_WEEKS_RENTED",DefaultValues.GetIntNullFromNegative(objInfo.NO_WEEKS_RENTED));
			objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			objWrapper.AddParameter("@IS_ACTIVE",objInfo.IS_ACTIVE);
			if(objInfo.REPAIR_COST==0.00 || objInfo.REPAIR_COST==0)
			{
				objWrapper.AddParameter("@REPAIR_COST",System.DBNull.Value);
			}
			else
			{
				objWrapper.AddParameter("@REPAIR_COST",objInfo.REPAIR_COST);
               
			}

			SqlParameter sqlParamDwellingID = (SqlParameter) objWrapper.AddParameter("@DWELLING_ID",SqlDbType.Int,ParameterDirection.Output);
			
			int dwellingID = 0;

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
					objTransactionInfo.TRANS_DESC		=	"New Dwelling Info is added.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO =strCustomInfo;
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				
				dwellingID = Convert.ToInt32(sqlParamDwellingID.Value);
				
				//If negative value returned because of no insert
				if ( dwellingID <= 0 )
				{
					objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return dwellingID;
				}
							
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			

			return dwellingID;
		}
		#endregion

		#region AddPolicy Function
		public int AddPolicy(Cms.Model.Policy.Umbrella.ClsDwellingInfo  objInfo,string strCustomInfo)
		{
			string	strStoredProc =	"Proc_UM_InsertPolicyDwellingsInfo";
			string strTranXML = "";
			DateTime	RecordDate		=	DateTime.Now;
			
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyDwellingInfo.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID );
			objWrapper.AddParameter("@DWELLING_NUMBER",objInfo.DWELLING_NUMBER);
			objWrapper.AddParameter("@LOCATION_ID",DefaultValues.GetIntNullFromNegative(objInfo.LOCATION_ID));
			objWrapper.AddParameter("@SUB_LOC_ID",DefaultValues.GetIntNullFromNegative(objInfo.SUB_LOC_ID));
			objWrapper.AddParameter("@YEAR_BUILT",DefaultValues.GetIntNullFromNegative(objInfo.YEAR_BUILT));
			objWrapper.AddParameter("@PURCHASE_YEAR",DefaultValues.GetIntNullFromNegative(objInfo.PURCHASE_YEAR));
			objWrapper.AddParameter("@PURCHASE_PRICE",DefaultValues.GetDoubleNullFromNegative(objInfo.PURCHASE_PRICE));
			objWrapper.AddParameter("@MARKET_VALUE",DefaultValues.GetDoubleNullFromNegative(objInfo.MARKET_VALUE));
			objWrapper.AddParameter("@REPLACEMENT_COST",DefaultValues.GetDoubleNullFromNegative(objInfo.REPLACEMENT_COST));
			objWrapper.AddParameter("@BUILDING_TYPE",objInfo.BUILDING_TYPE);
			objWrapper.AddParameter("@OCCUPANCY",objInfo.OCCUPANCY);
			objWrapper.AddParameter("@NEED_OF_UNITS",DefaultValues.GetIntNullFromNegative(objInfo.NEED_OF_UNITS));
			objWrapper.AddParameter("@USAGE",objInfo.USAGE);
			objWrapper.AddParameter("@NEIGHBOURS_VISIBLE",objInfo.NEIGHBOURS_VISIBLE);
			objWrapper.AddParameter("@OCCUPIED_DAILY",objInfo.OCCUPIED_DAILY);
			objWrapper.AddParameter("@NO_WEEKS_RENTED",DefaultValues.GetIntNullFromNegative(objInfo.NO_WEEKS_RENTED));
			objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			objWrapper.AddParameter("@CREATED_DATETIME",RecordDate );
			objWrapper.AddParameter("@IS_ACTIVE",objInfo.IS_ACTIVE);
			if(objInfo.REPAIR_COST==0.00 || objInfo.REPAIR_COST==0)
			{
				objWrapper.AddParameter("@REPAIR_COST",System.DBNull.Value);
			}
			else
			{
				objWrapper.AddParameter("@REPAIR_COST",objInfo.REPAIR_COST);
               
			}

			SqlParameter sqlParamDwellingID = (SqlParameter) objWrapper.AddParameter("@DWELLING_ID",SqlDbType.Int,ParameterDirection.Output);
			
			int dwellingID = 0;

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
					objTransactionInfo.POLICY_VER_TRACKING_ID  = objInfo.POLICY_VERSION_ID;
					objTransactionInfo.POLICY_ID  = objInfo.POLICY_ID ;
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY ;
					objTransactionInfo.TRANS_DESC		=	"New Dwelling Info is added to policy.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO =strCustomInfo;
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				
				dwellingID = Convert.ToInt32(sqlParamDwellingID.Value);
				
				//If negative value returned because of no insert
				if ( dwellingID <= 0 )
				{
					objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return dwellingID;
				}
							
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			

			return dwellingID;
		}
		#endregion


		public static string  GetDwellingsID(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_UM_GetDwellingsID";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			string strDwellingId="-1";
			for(int i=0;i<intCount;i++)
			{
				if(i==0)
				{
					strDwellingId=ds.Tables[0].Rows[i][0].ToString()+ '^' + ds.Tables[0].Rows[i][1].ToString();
				}
				else
				{
					strDwellingId = strDwellingId + '~'  + ds.Tables[0].Rows[i][0].ToString()+ '^' + ds.Tables[0].Rows[i][1].ToString();
				}

				
			}
			return strDwellingId;
		}
		
		
		
		#region GetDwellingID Function
		public static string  GetDwellingID(int customerID, int appID, int appVersionID,int locationID)
		{
			string	strStoredProc =	"Proc_UM_GetDwellingID";
			string strDwellingId;
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@LOCATION_ID",locationID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			if(intCount==0)
			{
				strDwellingId="-1";
			}
			else
			{
				strDwellingId=ds.Tables[0].Rows[0][0].ToString ();
			}

			
			return strDwellingId;
		}
		#endregion

		#region GetPolicyDwellingID Function
		public static string  GetPolicyDwellingID(int customerID, int policyID, int policyVersionID,int locationID)
		{
			string	strStoredProc =	"Proc_UM_GetPolicyDwellingID";
			string strDwellingId;
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID );
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID );
			objWrapper.AddParameter("@LOCATION_ID",locationID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			if(intCount==0)
			{
				strDwellingId="-1";
			}
			else
			{
				strDwellingId=ds.Tables[0].Rows[0][0].ToString ();
			}

			
			return strDwellingId;
		}
		#endregion
		
		
		#region GetDwellingInfoByID Function
		public DataSet GetDwellingInfoByID(int customerID, int appID, int appVersionID, int dwellingID)
		{
			string	strStoredProc =	"Proc_UM_GetDwellingInfoByID";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}
		#endregion

		#region GetPolicyDwellingInfoByID Function
		public DataSet GetPolicyDwellingInfoByID(int customerID, int policyID, int policyVersionID, int dwellingID)
		{
			string	strStoredProc =	"Proc_UM_GetPolicyDwellingInfoByID";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}
		#endregion

		#region GetNextDwellingNumber Function
		public static int GetNextDwellingNumber(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_UM_GetNextDwelling_Number";
			int nextDwellingID = 0;

			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[1] = new SqlParameter("@APP_ID",appID);
			sqlParams[2] = new SqlParameter("@APP_VERSION_ID",appVersionID);
			
			try
			{
				nextDwellingID = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams));
			}
			catch(Exception ex)
			{
				throw(ex);
			}

			return nextDwellingID;
		}
		#endregion

		
		#region GetNextPolicyDwellingNumber Function
		public static int GetNextPolicyDwellingNumber(int customerID, int policyID, int policyVersionID)
		{
			string	strStoredProc =	"Proc_UM_GetNextPolicyDwelling_Number";
			int nextDwellingID = 0;

			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[1] = new SqlParameter("@POLICY_ID",policyID);
			sqlParams[2] = new SqlParameter("@POLICY_VERSION_ID",policyVersionID);
			
			try
			{
				nextDwellingID = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams));
			}
			catch(Exception ex)
			{
				throw(ex);
			}

			return nextDwellingID;
		}
		#endregion
		
		public DataTable GetRemainingLocations(int customerID, int appID,int appVersionID)
		{
			string	strStoredProc =	"Proc_UM_GetRemainingLocationsForApplication";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds.Tables[0];
		}

		
		public int Save(ClsUmbrellaDwellingInfo objInfo,DataWrapper objWrapper)
		{
			string	strStoredProc =	"Proc_UM_Save_APP_DWELLINGS_INFO_ACORD";
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@DWELLING_NUMBER",objInfo.DWELLING_NUMBER);
			objWrapper.AddParameter("@LOCATION_ID",DefaultValues.GetIntNullFromNegative(objInfo.LOCATION_ID));
			objWrapper.AddParameter("@SUB_LOC_ID",DefaultValues.GetIntNullFromNegative(objInfo.SUB_LOC_ID));
			objWrapper.AddParameter("@YEAR_BUILT",DefaultValues.GetIntNullFromNegative(objInfo.YEAR_BUILT));
			objWrapper.AddParameter("@PURCHASE_YEAR",DefaultValues.GetIntNullFromNegative(objInfo.PURCHASE_YEAR));
			objWrapper.AddParameter("@PURCHASE_PRICE",DefaultValues.GetDoubleNullFromNegative(objInfo.PURCHASE_PRICE));
			objWrapper.AddParameter("@MARKET_VALUE",DefaultValues.GetDoubleNullFromNegative(objInfo.MARKET_VALUE));
			objWrapper.AddParameter("@REPLACEMENT_COST",DefaultValues.GetDoubleNullFromNegative(objInfo.REPLACEMENT_COST));
			objWrapper.AddParameter("@BUILDING_TYPE",objInfo.BUILDING_TYPE);
			objWrapper.AddParameter("@OCCUPANCY",objInfo.OCCUPANCY);
			objWrapper.AddParameter("@NEED_OF_UNITS",DefaultValues.GetIntNullFromNegative(objInfo.NEED_OF_UNITS));
			objWrapper.AddParameter("@USAGE",objInfo.USAGE);
			objWrapper.AddParameter("@NEIGHBOURS_VISIBLE",objInfo.NEIGHBOURS_VISIBLE);
			objWrapper.AddParameter("@OCCUPIED_DAILY",objInfo.OCCUPIED_DAILY);
			objWrapper.AddParameter("@NO_WEEKS_RENTED",DefaultValues.GetIntNullFromNegative(objInfo.NO_WEEKS_RENTED));
			objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			objWrapper.AddParameter("@MODIFIED_BY",objInfo.MODIFIED_BY);
			
			SqlParameter sqlParamDwellingID = (SqlParameter) objWrapper.AddParameter("@DWELLING_ID",SqlDbType.Int,ParameterDirection.Output);
			
			objWrapper.ExecuteNonQuery(strStoredProc);
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objWrapper.ClearParameteres();

				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AddUmbrellaDwelling.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objInfo);

				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
				objTransactionInfo.TRANS_TYPE_ID	=	2;
				objTransactionInfo.APP_ID = objInfo.APP_ID;
				objTransactionInfo.APP_VERSION_ID = objInfo.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
				objTransactionInfo.RECORDED_BY		=	objInfo.MODIFIED_BY;
				objTransactionInfo.TRANS_DESC		=	"New Dwelling Info is added from Quick Quote.";
				objTransactionInfo.CHANGE_XML		=	strTranXML;

				objWrapper.ExecuteNonQuery(objTransactionInfo);

				objWrapper.ClearParameteres();
			}

			int dwellingID = Convert.ToInt32(sqlParamDwellingID.Value);
			
			objInfo.DWELLING_ID = dwellingID;
			
			return dwellingID;
			

		}

		#region Update Function
		
		public int Update(ClsUmbrellaDwellingInfo objOldInfo,ClsUmbrellaDwellingInfo objNewInfo,string strCustomInfo)
		{
			string	strStoredProc =	"Proc_UM_UpdateAppDwellingsInfo";
			string strTranXML = "";
			
			if ( this.TransactionLogRequired)
			{
				objNewInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AddUmbrellaDwelling.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objNewInfo);
		
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objNewInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objNewInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@DWELLING_ID",objNewInfo.DWELLING_ID);
			objWrapper.AddParameter("@DWELLING_NUMBER",objNewInfo.DWELLING_NUMBER);
			objWrapper.AddParameter("@LOCATION_ID",DefaultValues.GetIntNullFromNegative(objNewInfo.LOCATION_ID));
			objWrapper.AddParameter("@SUB_LOC_ID",DefaultValues.GetIntNullFromNegative(objNewInfo.SUB_LOC_ID));
			objWrapper.AddParameter("@YEAR_BUILT",DefaultValues.GetIntNullFromNegative(objNewInfo.YEAR_BUILT));
			objWrapper.AddParameter("@PURCHASE_YEAR",DefaultValues.GetIntNullFromNegative(objNewInfo.PURCHASE_YEAR));
			objWrapper.AddParameter("@PURCHASE_PRICE",DefaultValues.GetDoubleNullFromNegative(objNewInfo.PURCHASE_PRICE));
			objWrapper.AddParameter("@MARKET_VALUE",DefaultValues.GetDoubleNullFromNegative(objNewInfo.MARKET_VALUE));
			objWrapper.AddParameter("@REPLACEMENT_COST",DefaultValues.GetDoubleNullFromNegative(objNewInfo.REPLACEMENT_COST));
			objWrapper.AddParameter("@BUILDING_TYPE",objNewInfo.BUILDING_TYPE);
			objWrapper.AddParameter("@OCCUPANCY",objNewInfo.OCCUPANCY);
			objWrapper.AddParameter("@NEED_OF_UNITS",DefaultValues.GetIntNullFromNegative(objNewInfo.NEED_OF_UNITS));
			objWrapper.AddParameter("@USAGE",objNewInfo.USAGE);
			objWrapper.AddParameter("@NEIGHBOURS_VISIBLE",objNewInfo.NEIGHBOURS_VISIBLE);
			objWrapper.AddParameter("@OCCUPIED_DAILY",objNewInfo.OCCUPIED_DAILY);
			objWrapper.AddParameter("@NO_WEEKS_RENTED",DefaultValues.GetIntNullFromNegative(objNewInfo.NO_WEEKS_RENTED));
			objWrapper.AddParameter("@MODIFIED_BY",objNewInfo.CREATED_BY);
			objWrapper.AddParameter("@IS_ACTIVE",objNewInfo.IS_ACTIVE);
			if(objNewInfo.REPAIR_COST==0.00 || objNewInfo.REPAIR_COST==0 ||objNewInfo.REPAIR_COST==.00)
			{
				objWrapper.AddParameter("@REPAIR_COST",System.DBNull.Value);
			}
			else
			{
				objWrapper.AddParameter("@REPAIR_COST",objNewInfo.REPAIR_COST);
			}

			SqlParameter sqlParamRetVal = (SqlParameter) objWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);

			try
			{	
				if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
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
					objTransactionInfo.TRANS_DESC		=	"Dwelling Info is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO =strCustomInfo;
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);


				}
				
				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			
			int retVal = Convert.ToInt32(sqlParamRetVal.Value);
			
			return retVal;
		}
		#endregion

		#region UpdatePolicy Function
		
		public int UpdatePolicy(Cms.Model.Policy.Umbrella.ClsDwellingInfo  objOldInfo,
								Cms.Model.Policy.Umbrella.ClsDwellingInfo  objNewInfo,string strCustomInfo)
		{
			string	strStoredProc =	"Proc_UM_UpdatePolicyDwellingsInfo";
			string strTranXML = "";
			DateTime	RecordDate		=	DateTime.Now;

			if ( this.TransactionLogRequired)
			{
				objNewInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyDwellingInfo.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objNewInfo);
		
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",objNewInfo.POLICY_ID );
			objWrapper.AddParameter("@POLICY_VERSION_ID",objNewInfo.POLICY_VERSION_ID );
			objWrapper.AddParameter("@DWELLING_ID",objNewInfo.DWELLING_ID);
			objWrapper.AddParameter("@DWELLING_NUMBER",objNewInfo.DWELLING_NUMBER);
			objWrapper.AddParameter("@LOCATION_ID",DefaultValues.GetIntNullFromNegative(objNewInfo.LOCATION_ID));
			objWrapper.AddParameter("@YEAR_BUILT",DefaultValues.GetIntNullFromNegative(objNewInfo.YEAR_BUILT));
			objWrapper.AddParameter("@PURCHASE_YEAR",DefaultValues.GetIntNullFromNegative(objNewInfo.PURCHASE_YEAR));
			objWrapper.AddParameter("@PURCHASE_PRICE",DefaultValues.GetDoubleNullFromNegative(objNewInfo.PURCHASE_PRICE));
			objWrapper.AddParameter("@MARKET_VALUE",DefaultValues.GetDoubleNullFromNegative(objNewInfo.MARKET_VALUE));
			objWrapper.AddParameter("@REPLACEMENT_COST",DefaultValues.GetDoubleNullFromNegative(objNewInfo.REPLACEMENT_COST));
			objWrapper.AddParameter("@BUILDING_TYPE",objNewInfo.BUILDING_TYPE);
			objWrapper.AddParameter("@OCCUPANCY",objNewInfo.OCCUPANCY);
			objWrapper.AddParameter("@NEED_OF_UNITS",DefaultValues.GetIntNullFromNegative(objNewInfo.NEED_OF_UNITS));
			objWrapper.AddParameter("@USAGE",objNewInfo.USAGE);
			objWrapper.AddParameter("@NEIGHBOURS_VISIBLE",objNewInfo.NEIGHBOURS_VISIBLE);
			objWrapper.AddParameter("@OCCUPIED_DAILY",objNewInfo.OCCUPIED_DAILY);
			objWrapper.AddParameter("@NO_WEEKS_RENTED",DefaultValues.GetIntNullFromNegative(objNewInfo.NO_WEEKS_RENTED));
			objWrapper.AddParameter("@MODIFIED_BY",objNewInfo.MODIFIED_BY);;
			objWrapper.AddParameter("@LAST_UPDATED_DATETIME",RecordDate);
			objWrapper.AddParameter("@IS_ACTIVE",objNewInfo.IS_ACTIVE);
			if(objNewInfo.REPAIR_COST==0.00 || objNewInfo.REPAIR_COST==0 ||objNewInfo.REPAIR_COST==.00)
			{
				objWrapper.AddParameter("@REPAIR_COST",System.DBNull.Value);
			}
			else
			{
				objWrapper.AddParameter("@REPAIR_COST",objNewInfo.REPAIR_COST);
			}

			SqlParameter sqlParamRetVal = (SqlParameter) objWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);

			try
			{	
				if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
				{
					objWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID = objNewInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = objNewInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objNewInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objNewInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Dwelling Info is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO =strCustomInfo;
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);


				}
				
				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			
			int retVal = Convert.ToInt32(sqlParamRetVal.Value);
			
			return retVal;
		}
		#endregion
	}
}
