#region Page Info
/******************************************************************************************
<Author					: - Swastika
<Start Date				: -	6/13/2006 5:58:17 PM
<End Date				: -	
<Description			: - Other Locations (HomeOwners)	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
		#endregion

#region Libraries
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using Cms.DataLayer;
using Cms.Model.Application;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Policy.Homeowners;
		#endregion
namespace Cms.BusinessLayer.BlApplication
{
	public class ClsOtherLocation : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_OTHER_LOCATIONS			=	"APP_OTHER_LOCATIONS";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAPP_OTHER_LOCATIONS";
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

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsOtherLocation()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		// Add Other Locations at Application level
		public int Add(ClsOtherLocationsInfo objOtherLocationsInfo)
		{
			string		strStoredProc	=	"Proc_InsertAPP_OTHER_LOCATIONS";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOtherLocationsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objOtherLocationsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOtherLocationsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objOtherLocationsInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@LOC_NUM",objOtherLocationsInfo.LOC_NUM);
				objDataWrapper.AddParameter("@LOC_ADD1",objOtherLocationsInfo.LOC_ADD1);
				objDataWrapper.AddParameter("@LOC_CITY",objOtherLocationsInfo.LOC_CITY);
				objDataWrapper.AddParameter("@LOC_COUNTY",objOtherLocationsInfo.LOC_COUNTY);
				objDataWrapper.AddParameter("@LOC_STATE",objOtherLocationsInfo.LOC_STATE);
				objDataWrapper.AddParameter("@LOC_ZIP",objOtherLocationsInfo.LOC_ZIP);

				if (objOtherLocationsInfo.PHOTO_ATTACHED!= 0)
					objDataWrapper.AddParameter("@PHOTO_ATTACHED",objOtherLocationsInfo.PHOTO_ATTACHED);
				else
					objDataWrapper.AddParameter("@PHOTO_ATTACHED",null);
				if (objOtherLocationsInfo.OCCUPIED_BY_INSURED != 0)
					objDataWrapper.AddParameter("@OCCUPIED_BY_INSURED",objOtherLocationsInfo.OCCUPIED_BY_INSURED);
				else
					objDataWrapper.AddParameter("@OCCUPIED_BY_INSURED",null);
				objDataWrapper.AddParameter("@DESCRIPTION",objOtherLocationsInfo.DESCRIPTION);
				
				objDataWrapper.AddParameter("@CREATED_BY",objOtherLocationsInfo.CREATED_BY);
				
				if (objOtherLocationsInfo.CREATED_DATETIME > DateTime.MinValue)
					objDataWrapper.AddParameter("@CREATED_DATETIME",objOtherLocationsInfo.CREATED_DATETIME);
				else
					objDataWrapper.AddParameter("@CREATED_DATETIME",null);

				

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@LOCATION_ID",objOtherLocationsInfo.LOCATION_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objOtherLocationsInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/HomeOwners/AddOtherLocations.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objOtherLocationsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//Done for Itrack Issue 6655 on 2 Nov 09
					objTransactionInfo.CLIENT_ID		=   objOtherLocationsInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID			=	objOtherLocationsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objOtherLocationsInfo.APP_VERSION_ID;
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objOtherLocationsInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Other Location has been Added";//Done for Itrack Issue 6655 on 25 Nov 09
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				}

				int LOCATION_ID = -1;

				if (returnResult > 0)
					LOCATION_ID = int.Parse(objSqlParameter.Value.ToString());
				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if (LOCATION_ID == -1)
				{
					return -1;
				}
				else
				{
					objOtherLocationsInfo.LOCATION_ID = LOCATION_ID;
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

		// Add Other Locations at Policy level
		public int PolAdd(ClsPolicyOtherLocationsInfo objOtherLocationsInfo) // ==< CHANGE CLASS
		{
			string		strStoredProc	=	"Proc_InsertPOL_OTHER_LOCATIONS";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOtherLocationsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objOtherLocationsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objOtherLocationsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objOtherLocationsInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@LOC_NUM",objOtherLocationsInfo.LOC_NUM);
				objDataWrapper.AddParameter("@LOC_ADD1",objOtherLocationsInfo.LOC_ADD1);
				objDataWrapper.AddParameter("@LOC_CITY",objOtherLocationsInfo.LOC_CITY);
				objDataWrapper.AddParameter("@LOC_COUNTY",objOtherLocationsInfo.LOC_COUNTY);
				objDataWrapper.AddParameter("@LOC_STATE",objOtherLocationsInfo.LOC_STATE);
				objDataWrapper.AddParameter("@LOC_ZIP",objOtherLocationsInfo.LOC_ZIP);

				if (objOtherLocationsInfo.PHOTO_ATTACHED!= 0)
					objDataWrapper.AddParameter("@PHOTO_ATTACHED",objOtherLocationsInfo.PHOTO_ATTACHED);
				else
					objDataWrapper.AddParameter("@PHOTO_ATTACHED",null);
				if (objOtherLocationsInfo.OCCUPIED_BY_INSURED != 0)
					objDataWrapper.AddParameter("@OCCUPIED_BY_INSURED",objOtherLocationsInfo.OCCUPIED_BY_INSURED);
				else
					objDataWrapper.AddParameter("@OCCUPIED_BY_INSURED",null);
				objDataWrapper.AddParameter("@DESCRIPTION",objOtherLocationsInfo.DESCRIPTION);
				
				objDataWrapper.AddParameter("@CREATED_BY",objOtherLocationsInfo.CREATED_BY);
				
				if (objOtherLocationsInfo.CREATED_DATETIME > DateTime.MinValue)
					objDataWrapper.AddParameter("@CREATED_DATETIME",objOtherLocationsInfo.CREATED_DATETIME);
				else
					objDataWrapper.AddParameter("@CREATED_DATETIME",null);

				

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@LOCATION_ID",objOtherLocationsInfo.LOCATION_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objOtherLocationsInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/Homeowner/AddPolOtherLocations.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objOtherLocationsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//Done for Itrack Issue 6655 on 2 Nov 09
					objTransactionInfo.CLIENT_ID		=   objOtherLocationsInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID		=	objOtherLocationsInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objOtherLocationsInfo.POLICY_VERSION_ID;
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objOtherLocationsInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Other Location has been Added";//Done for Itrack Issue 6655 on 25 Nov 09
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				}

				int LOCATION_ID = -1;

				if (returnResult > 0)
					LOCATION_ID = int.Parse(objSqlParameter.Value.ToString());
				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if (LOCATION_ID == -1)
				{
					return -1;
				}
				else
				{
					objOtherLocationsInfo.LOCATION_ID = LOCATION_ID;
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
		// Update at App level
		public int Update(ClsOtherLocationsInfo objOldOtherLocationsInfo,ClsOtherLocationsInfo objOtherLocationsInfo)
		{
			string		strStoredProc	=	"Proc_UpdateAPP_OTHER_LOCATIONS";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOtherLocationsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objOtherLocationsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOtherLocationsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objOtherLocationsInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@LOCATION_ID",objOtherLocationsInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@LOC_NUM",objOtherLocationsInfo.LOC_NUM);
				objDataWrapper.AddParameter("@LOC_ADD1",objOtherLocationsInfo.LOC_ADD1);
				objDataWrapper.AddParameter("@LOC_CITY",objOtherLocationsInfo.LOC_CITY);
				objDataWrapper.AddParameter("@LOC_COUNTY",objOtherLocationsInfo.LOC_COUNTY);
				objDataWrapper.AddParameter("@LOC_STATE",objOtherLocationsInfo.LOC_STATE);
				objDataWrapper.AddParameter("@LOC_ZIP",objOtherLocationsInfo.LOC_ZIP);
				objDataWrapper.AddParameter("@PHOTO_ATTACHED",objOtherLocationsInfo.PHOTO_ATTACHED);
				objDataWrapper.AddParameter("@OCCUPIED_BY_INSURED",objOtherLocationsInfo.OCCUPIED_BY_INSURED);
				objDataWrapper.AddParameter("@DESCRIPTION",objOtherLocationsInfo.DESCRIPTION);
				

				objDataWrapper.AddParameter("@MODIFIED_BY",objOtherLocationsInfo.MODIFIED_BY);

				if (objOtherLocationsInfo.LAST_UPDATED_DATETIME > DateTime.MinValue)
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objOtherLocationsInfo.LAST_UPDATED_DATETIME);
				else
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);

				if(base.TransactionLogRequired) 
				{
					objOtherLocationsInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/HomeOwners/AddOtherLocations.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldOtherLocationsInfo,objOtherLocationsInfo);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						//Done for Itrack Issue 6655 on 2 Nov 09
						objTransactionInfo.CLIENT_ID		=   objOtherLocationsInfo.CUSTOMER_ID;
						objTransactionInfo.APP_ID			=	objOtherLocationsInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objOtherLocationsInfo.APP_VERSION_ID;
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objOtherLocationsInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Other Location has been updated";//Done for Itrack Issue 6655 on 25 Nov 09
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

					}
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

		// Update at Pol level
		public int PolUpdate(ClsPolicyOtherLocationsInfo objOldOtherLocationsInfo,ClsPolicyOtherLocationsInfo objOtherLocationsInfo)
		{
			string		strStoredProc	=	"Proc_UpdatePOL_OTHER_LOCATIONS";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOtherLocationsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objOtherLocationsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objOtherLocationsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objOtherLocationsInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@LOCATION_ID",objOtherLocationsInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@LOC_NUM",objOtherLocationsInfo.LOC_NUM);
				objDataWrapper.AddParameter("@LOC_ADD1",objOtherLocationsInfo.LOC_ADD1);
				objDataWrapper.AddParameter("@LOC_CITY",objOtherLocationsInfo.LOC_CITY);
				objDataWrapper.AddParameter("@LOC_COUNTY",objOtherLocationsInfo.LOC_COUNTY);
				objDataWrapper.AddParameter("@LOC_STATE",objOtherLocationsInfo.LOC_STATE);
				objDataWrapper.AddParameter("@LOC_ZIP",objOtherLocationsInfo.LOC_ZIP);
				objDataWrapper.AddParameter("@PHOTO_ATTACHED",objOtherLocationsInfo.PHOTO_ATTACHED);
				objDataWrapper.AddParameter("@OCCUPIED_BY_INSURED",objOtherLocationsInfo.OCCUPIED_BY_INSURED);
				objDataWrapper.AddParameter("@DESCRIPTION",objOtherLocationsInfo.DESCRIPTION);
				

				objDataWrapper.AddParameter("@MODIFIED_BY",objOtherLocationsInfo.MODIFIED_BY);

				if (objOtherLocationsInfo.LAST_UPDATED_DATETIME > DateTime.MinValue)
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objOtherLocationsInfo.LAST_UPDATED_DATETIME);
				else
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);

				if(base.TransactionLogRequired) 
				{
					objOtherLocationsInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/Homeowner/AddPolOtherLocations.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldOtherLocationsInfo,objOtherLocationsInfo);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						//Done for Itrack Issue 6655 on 2 Nov 09
						objTransactionInfo.CLIENT_ID		=   objOtherLocationsInfo.CUSTOMER_ID;
						objTransactionInfo.POLICY_ID		=	objOtherLocationsInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID	=	objOtherLocationsInfo.POLICY_VERSION_ID;
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objOtherLocationsInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Other Location has been updated";//Done for Itrack Issue 6655 on 25 Nov 09
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

					}
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

		#region Activate/Deactivate
		public void ActivateDeactivate(ClsOtherLocationsInfo objOtherLocationsInfo,string strStatus)
		{
			string	strStoredProc =	"Proc_ActivateDeactivateOtherLocations";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			try
			{
			objWrapper.AddParameter("@CUSTOMER_ID",objOtherLocationsInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objOtherLocationsInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objOtherLocationsInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@LOCATION_ID",objOtherLocationsInfo.LOCATION_ID);
			objWrapper.AddParameter("@IS_ACTIVE",strStatus);
			SqlParameter paramRetVal = (SqlParameter)objWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);

			int returnResult = 0;
			if(TransactionLogRequired)
			{																			
				objOtherLocationsInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/HomeOwners/AddOtherLocations.aspx.resx");
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.APP_ID			=	objOtherLocationsInfo.APP_ID;
				objTransactionInfo.APP_VERSION_ID	=	objOtherLocationsInfo.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID		=	objOtherLocationsInfo.CUSTOMER_ID;
				objTransactionInfo.RECORDED_BY		=	objOtherLocationsInfo.MODIFIED_BY;
				if(strStatus.ToUpper()=="Y")
					objTransactionInfo.TRANS_DESC		=	"Other Location is Activated";
				else
					objTransactionInfo.TRANS_DESC		=	"Other Location is Deactivated";
				returnResult	= objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
			}
			else
			{
				returnResult	= objWrapper.ExecuteNonQuery(strStoredProc);
			}				
			objWrapper.ClearParameteres();
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
		}
		catch(Exception ex)
		{
			throw(ex);				
		}
		finally
		{
			if(objWrapper != null) objWrapper.Dispose();
		}	
	}
		
		public void PolActivateDeactivate(ClsPolicyOtherLocationsInfo objOtherLocationsInfo,string strStatus)
		{
			string	strStoredProc =	"Proc_ActivateDeactivatePolOtherLocations";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			try
			{
				objWrapper.AddParameter("@CUSTOMER_ID",objOtherLocationsInfo.CUSTOMER_ID);
				objWrapper.AddParameter("@POLICY_ID",objOtherLocationsInfo.POLICY_ID);
				objWrapper.AddParameter("@POLICY_VERSION_ID",objOtherLocationsInfo.POLICY_VERSION_ID);
				objWrapper.AddParameter("@LOCATION_ID",objOtherLocationsInfo.LOCATION_ID);
				objWrapper.AddParameter("@IS_ACTIVE",strStatus);
				SqlParameter paramRetVal = (SqlParameter)objWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objOtherLocationsInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/Homeowner/AddPolOtherLocations.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID			=	1;
					objTransactionInfo.POLICY_ID				=	objOtherLocationsInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objOtherLocationsInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID				=	objOtherLocationsInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY				=	objOtherLocationsInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC			=	"Other Location is Activated";
					else
						objTransactionInfo.TRANS_DESC			=	"Other Location is Deactivated";
					returnResult	= objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objWrapper.ClearParameteres();
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
			}
			catch(Exception ex)
			{
				throw(ex);				
			}
			finally
			{
				if(objWrapper != null) objWrapper.Dispose();
			}	
		}


		#endregion

		#region "Get Methods"

		//Get APP Info
		public static string GetXmlForPageControls(int intCustomerId,int intAppid, int intAppVersionId, int intLocationId)
		{
			string strSql = "Proc_GetXMLAPP_OTHER_LOCATIONS";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
			objDataWrapper.AddParameter("@APP_ID",intAppid);
			objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
			objDataWrapper.AddParameter("@LOCATION_ID",intLocationId);

			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}
		// Get POL Info
		public static string PolGetXmlForPageControls(int intCustomerId,int intPolicyid, int intPolicyVersionId, int intLocationId)
		{
			string strSql = "Proc_GetXMLPOL_OTHER_LOCATIONS";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
			objDataWrapper.AddParameter("@POLICY_ID",intPolicyid);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVersionId);
			objDataWrapper.AddParameter("@LOCATION_ID",intLocationId);

			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}

		// Gets Unique Loc_Num for App
		public string GetAppLocationNumber(int intCustomerId, int intAppId, int intAppVersionId)
		{
			string strStoredProc = "Proc_AppOtherLocationNumber";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
									 
			objWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
			objWrapper.AddParameter("@APP_ID",intAppId);
			objWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
				
			DataSet ds = new DataSet();
			try
			{
				ds = objWrapper.ExecuteDataSet(strStoredProc);
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	
				if (ds.Tables[0].Rows.Count>0)
				{
					return ds.Tables[0].Rows[0]["Loc_Num"].ToString();
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
		
		// Gets Unique Loc_Num for Pol
		public string GetPolLocationNumber(int intCustomerId, int intPolicyId, int intPolicyVersionId)
		{
			string strStoredProc = "Proc_PolOtherLocationNumber";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
									 
			objWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
			objWrapper.AddParameter("@POLICY_ID",intPolicyId);
			objWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVersionId);
				
			DataSet ds = new DataSet();
			try
			{
				ds = objWrapper.ExecuteDataSet(strStoredProc);
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	
				if (ds.Tables[0].Rows.Count>0)
				{
					return ds.Tables[0].Rows[0]["Loc_Num"].ToString();
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
	}
}
