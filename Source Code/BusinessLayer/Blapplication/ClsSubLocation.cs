/******************************************************************************************
<Author				: -   Vijay Joshi
<Start Date				: -	5/12/2005 2:51:09 PM
<End Date				: -	
<Description				: - 	Business logic of add sub location page.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Application.HomeOwners;
namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Business Logic class for Add Sublocation Page.
	/// </summary>
	public class ClsSubLocation : Cms.BusinessLayer.BlApplication.clsapplication
	{
		private const string GET_LOCATION_INFO_PROC = "Proc_GetSubLocation";
		private string strStoredProc = "";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		// private int _SUB_LOC_ID;
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
		public ClsSubLocation()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion
		
		public int ActivateDeactivate(int customerID, int appID, int appVersionID,
			int subLocID,string action)
		{
			string	strStoredProc =	"Proc_ActivateDeactivateLocation";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@LOCATION_ID",subLocID);
			objWrapper.AddParameter("@IS_ACTIVE",action);
			
			SqlParameter paramRetVal = (SqlParameter)objWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);

			objWrapper.ExecuteNonQuery(strStoredProc);
			
			int retVal = Convert.ToInt32(paramRetVal.Value);
			return retVal;
		}

		#region Add(Insert) functions

		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objSubLocationInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsSubLocationInfo objSubLocationInfo)
		{
			string		strStoredProc	=	"Proc_InsertSubLocation";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", objSubLocationInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID", objSubLocationInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID", objSubLocationInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@LOCATION_ID", objSubLocationInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@SUB_LOC_NUMBER", objSubLocationInfo.SUB_LOC_NUMBER);
				objDataWrapper.AddParameter("@SUB_LOC_TYPE", objSubLocationInfo.SUB_LOC_TYPE);
				objDataWrapper.AddParameter("@SUB_LOC_DESC", objSubLocationInfo.SUB_LOC_DESC);
				objDataWrapper.AddParameter("@SUB_LOC_CITY_LIMITS", objSubLocationInfo.SUB_LOC_CITY_LIMITS);
				objDataWrapper.AddParameter("@SUB_LOC_INTEREST", objSubLocationInfo.SUB_LOC_INTEREST);
				objDataWrapper.AddParameter("@SUB_LOC_OCCUPIED_PERCENT", DefaultValues.GetDoubleNullFromNegative(objSubLocationInfo.SUB_LOC_OCCUPIED_PERCENT));
				//<Gaurav> 1 June 2005 ; START: Following fields not required now; BUG No<543>
				/*
				objDataWrapper.AddParameter("@SUB_LOC_YEAR_BUILT", objSubLocationInfo.SUB_LOC_YEAR_BUILT);
				objDataWrapper.AddParameter("@SUB_LOC_AREA_IN_SQ_FOOT", objSubLocationInfo.SUB_LOC_AREA_IN_SQ_FOOT);
				objDataWrapper.AddParameter("@SUB_LOC_PROT_CLASS", objSubLocationInfo.SUB_LOC_PROT_CLASS);
				objDataWrapper.AddParameter("@SUB_LOC_HYDRANT_DIST", objSubLocationInfo.SUB_LOC_HYDRANT_DIST);
				objDataWrapper.AddParameter("@SUB_LOC_FIRE_DIST", objSubLocationInfo.SUB_LOC_FIRE_DIST);
				objDataWrapper.AddParameter("@SUB_LOC_INSIDE_CITY_LIMITS", objSubLocationInfo.SUB_LOC_INSIDE_CITY_LIMITS);
				objDataWrapper.AddParameter("@SUB_LOC_SURROUND_EXP", objSubLocationInfo.SUB_LOC_SURROUND_EXP);
				*/
				//<Gaurav> 1 June 2005 ; END: Following fields not required now; BUG No<543>
				
				objDataWrapper.AddParameter("@SUB_LOC_OCC_DESC", objSubLocationInfo.SUB_LOC_OCC_DESC);
				objDataWrapper.AddParameter("@CREATED_BY", objSubLocationInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME", objSubLocationInfo.CREATED_DATETIME);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@SUB_LOC_ID",objSubLocationInfo.SUB_LOC_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objSubLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/Homeowners/AddSubLocation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objSubLocationInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objSubLocationInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objSubLocationInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objSubLocationInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objSubLocationInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New sublocation is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				objSubLocationInfo.SUB_LOC_ID = int.Parse(objSqlParameter.Value.ToString());
								
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				return objSubLocationInfo.SUB_LOC_ID;
				
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
		/// <param name="objOldSubLocationInfo">Model object having old information</param>
		/// <param name="objSubLocationInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsSubLocationInfo objOldSubLocationInfo,ClsSubLocationInfo objSubLocationInfo)
		{
			string strTranXML;
			int returnResult = 0;
			strStoredProc = "Proc_UpdateSubLocation";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objSubLocationInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objSubLocationInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objSubLocationInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@LOCATION_ID",objSubLocationInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@SUB_LOC_ID",objSubLocationInfo.SUB_LOC_ID);
				objDataWrapper.AddParameter("@SUB_LOC_NUMBER",objSubLocationInfo.SUB_LOC_NUMBER);
				objDataWrapper.AddParameter("@SUB_LOC_TYPE",objSubLocationInfo.SUB_LOC_TYPE);
				objDataWrapper.AddParameter("@SUB_LOC_DESC",objSubLocationInfo.SUB_LOC_DESC);
				objDataWrapper.AddParameter("@SUB_LOC_CITY_LIMITS",objSubLocationInfo.SUB_LOC_CITY_LIMITS);
				objDataWrapper.AddParameter("@SUB_LOC_INTEREST",objSubLocationInfo.SUB_LOC_INTEREST);
				objDataWrapper.AddParameter("@SUB_LOC_OCCUPIED_PERCENT",DefaultValues.GetDoubleNullFromNegative(objSubLocationInfo.SUB_LOC_OCCUPIED_PERCENT));
				
				//<Gaurav> 1 June 2005 ; START: Following fields not required now; BUG No<543>
				/*
				objDataWrapper.AddParameter("@SUB_LOC_YEAR_BUILT",objSubLocationInfo.SUB_LOC_YEAR_BUILT);
				objDataWrapper.AddParameter("@SUB_LOC_AREA_IN_SQ_FOOT",objSubLocationInfo.SUB_LOC_AREA_IN_SQ_FOOT);
				objDataWrapper.AddParameter("@SUB_LOC_PROT_CLASS",objSubLocationInfo.SUB_LOC_PROT_CLASS);
				objDataWrapper.AddParameter("@SUB_LOC_HYDRANT_DIST",objSubLocationInfo.SUB_LOC_HYDRANT_DIST);
				objDataWrapper.AddParameter("@SUB_LOC_FIRE_DIST",objSubLocationInfo.SUB_LOC_FIRE_DIST);
				objDataWrapper.AddParameter("@SUB_LOC_INSIDE_CITY_LIMITS",objSubLocationInfo.SUB_LOC_INSIDE_CITY_LIMITS);
				objDataWrapper.AddParameter("@SUB_LOC_SURROUND_EXP",objSubLocationInfo.SUB_LOC_SURROUND_EXP);
				*/
				//<Gaurav> 1 June 2005 ; END: Following fields not required now; BUG No<543>

				objDataWrapper.AddParameter("@SUB_LOC_OCC_DESC",objSubLocationInfo.SUB_LOC_OCC_DESC);
				objDataWrapper.AddParameter("@MODIFIED_BY",objSubLocationInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objSubLocationInfo.LAST_UPDATED_DATETIME);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@SUB_LOC_ID",objSubLocationInfo.SUB_LOC_ID,SqlDbType.Int,ParameterDirection.ReturnValue);

				if(TransactionLogRequired) 
				{
					objSubLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/Homeowners/AddSubLocation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldSubLocationInfo,objSubLocationInfo);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.APP_ID = objSubLocationInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objSubLocationInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objSubLocationInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objSubLocationInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Sublocation is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				return Convert.ToInt32(objSqlParameter.Value);

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

		#region GetSubLocationInfo
		public static string GetSubLocationInfo(int intCustomerId,int intAppid, int intAppVersionId, int intLocationId, int intSubLocationId)
		{

			DataSet dsSubLocInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@APP_ID",intAppid);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
				objDataWrapper.AddParameter("@LOCATION_ID",intLocationId);
				objDataWrapper.AddParameter("@SUB_LOC_ID",intSubLocationId);

				dsSubLocInfo = objDataWrapper.ExecuteDataSet(GET_LOCATION_INFO_PROC);
				
				if (dsSubLocInfo.Tables[0].Rows.Count != 0)
				{
					//return dsSubLocInfo.GetXml();
					return ClsCommon.GetXMLEncoded(dsSubLocInfo.Tables[0]);
				}
				else
				{
					return "";
				}
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}

		/// <summary>
		/// Returns the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static DataTable GetSublocationsForApplication(int intCustomerID,int intAppID, int intAppVersionID,int intLocationID)
		{
			string	strStoredProc =	"Proc_GetSubLocationsForApplication";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",intCustomerID);
			objWrapper.AddParameter("@APP_ID",intAppID);
			objWrapper.AddParameter("@APP_VERSION_ID",intAppVersionID);
			objWrapper.AddParameter("@LOCATION_ID",intLocationID);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds.Tables[0];
		}


		#endregion

		#region SelectSubLocationNumber

		public string SubLocationNumber(int intCustomerId, int intAppId, int intAppVersionId, int intLocationId)
		{
			string strStoredProc = "Proc_SubLocationNumber";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
									 
			objWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
			objWrapper.AddParameter("@APP_ID",intAppId);
			objWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
			objWrapper.AddParameter("@LOCATION_ID",intLocationId);
				
			DataSet ds = new DataSet();
			try
			{
				ds = objWrapper.ExecuteDataSet(strStoredProc);
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	
				if (ds.Tables[0].Rows.Count>0)
				{
					return ds.Tables[0].Rows[0]["Sub_Loc_Number"].ToString();
				}
				else
				{	
					return "";
				}
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}

		}
		#endregion
		
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objSubLocationInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Save(ClsSubLocationInfo objSubLocationInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_Save_SUBLOCATION_ACORD";
			DateTime	RecordDate		=	DateTime.Now;
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			
				objDataWrapper.AddParameter("@CUSTOMER_ID", objSubLocationInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID", objSubLocationInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID", objSubLocationInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@LOCATION_ID", objSubLocationInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@SUB_LOC_NUMBER", objSubLocationInfo.SUB_LOC_NUMBER);
				objDataWrapper.AddParameter("@SUB_LOC_TYPE", objSubLocationInfo.SUB_LOC_TYPE);
				objDataWrapper.AddParameter("@SUB_LOC_DESC", objSubLocationInfo.SUB_LOC_DESC);
				objDataWrapper.AddParameter("@SUB_LOC_CITY_LIMITS", objSubLocationInfo.SUB_LOC_CITY_LIMITS);
				objDataWrapper.AddParameter("@SUB_LOC_INTEREST", objSubLocationInfo.SUB_LOC_INTEREST);
				objDataWrapper.AddParameter("@SUB_LOC_OCCUPIED_PERCENT", DefaultValues.GetDoubleNullFromNegative(objSubLocationInfo.SUB_LOC_OCCUPIED_PERCENT));
				
				
				objDataWrapper.AddParameter("@SUB_LOC_OCC_DESC", objSubLocationInfo.SUB_LOC_OCC_DESC);
				objDataWrapper.AddParameter("@CREATED_BY", objSubLocationInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME", RecordDate);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", RecordDate);
				objDataWrapper.AddParameter("@MODIFIED_BY", objSubLocationInfo.MODIFIED_BY);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@SUB_LOC_ID",objSubLocationInfo.SUB_LOC_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				

				objSubLocationInfo.SUB_LOC_ID = int.Parse(objSqlParameter.Value.ToString());
								
				
				return objSubLocationInfo.SUB_LOC_ID;
				
			
		}
		

	}
}
