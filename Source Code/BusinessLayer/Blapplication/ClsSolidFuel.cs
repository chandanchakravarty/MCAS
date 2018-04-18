/******************************************************************************************
<Author				: -   
<Start Date				: -	5/19/2005 9:45:41 AM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Policy.Homeowners;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsSolidFuel : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_HOME_OWNER_SOLID_FUEL			=	"APP_HOME_OWNER_SOLID_FUEL";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		
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
		public ClsSolidFuel()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}

		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAPP_SOLID_FUEL";

		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="ObjSolidFuelInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(Cms.Model.Application.HomeOwners.ClsSolidFuelInfo ObjSolidFuelInfo)
		{
			string		strStoredProc	=	"Proc_InsertSolidFuel";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjSolidFuelInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",ObjSolidFuelInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",ObjSolidFuelInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@LOCATION_ID",ObjSolidFuelInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@SUB_LOC_ID",ObjSolidFuelInfo.SUB_LOC_ID);
				objDataWrapper.AddParameter("@MANUFACTURER",ObjSolidFuelInfo.MANUFACTURER);
				objDataWrapper.AddParameter("@BRAND_NAME",ObjSolidFuelInfo.BRAND_NAME);
				objDataWrapper.AddParameter("@MODEL_NUMBER",ObjSolidFuelInfo.MODEL_NUMBER);
				objDataWrapper.AddParameter("@FUEL",ObjSolidFuelInfo.FUEL);
				objDataWrapper.AddParameter("@STOVE_TYPE",ObjSolidFuelInfo.STOVE_TYPE);
				objDataWrapper.AddParameter("@HAVE_LABORATORY_LABEL",ObjSolidFuelInfo.HAVE_LABORATORY_LABEL);
				objDataWrapper.AddParameter("@IS_UNIT",ObjSolidFuelInfo.IS_UNIT);
				objDataWrapper.AddParameter("@UNIT_OTHER_DESC",ObjSolidFuelInfo.UNIT_OTHER_DESC);
				objDataWrapper.AddParameter("@CONSTRUCTION",ObjSolidFuelInfo.CONSTRUCTION);
				objDataWrapper.AddParameter("@LOCATION",ObjSolidFuelInfo.LOCATION);
				objDataWrapper.AddParameter("@LOC_OTHER_DESC",ObjSolidFuelInfo.LOC_OTHER_DESC);

				if(ObjSolidFuelInfo.YEAR_DEVICE_INSTALLED==0)
				{
					objDataWrapper.AddParameter("@YEAR_DEVICE_INSTALLED",null);
				}
				else
				  objDataWrapper.AddParameter("@YEAR_DEVICE_INSTALLED",ObjSolidFuelInfo.YEAR_DEVICE_INSTALLED);
				 
				objDataWrapper.AddParameter("@WAS_PROF_INSTALL_DONE",ObjSolidFuelInfo.WAS_PROF_INSTALL_DONE);
				objDataWrapper.AddParameter("@INSTALL_INSPECTED_BY",ObjSolidFuelInfo.INSTALL_INSPECTED_BY);
				objDataWrapper.AddParameter("@INSTALL_OTHER_DESC",ObjSolidFuelInfo.INSTALL_OTHER_DESC);
				objDataWrapper.AddParameter("@HEATING_USE",ObjSolidFuelInfo.HEATING_USE);
				objDataWrapper.AddParameter("@HEATING_SOURCE",ObjSolidFuelInfo.HEATING_SOURCE);
				objDataWrapper.AddParameter("@OTHER_DESC",ObjSolidFuelInfo.OTHER_DESC);
				objDataWrapper.AddParameter("@CREATED_BY",ObjSolidFuelInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",ObjSolidFuelInfo.CREATED_DATETIME);
				
				if (ObjSolidFuelInfo.STOVE_INSTALLATION_CONFORM_SPECIFICATIONS != 0)
				{
					objDataWrapper.AddParameter("@STOVE_INSTALLATION_CONFORM_SPECIFICATIONS",ObjSolidFuelInfo.STOVE_INSTALLATION_CONFORM_SPECIFICATIONS);
				}
				else
				{
					objDataWrapper.AddParameter("@STOVE_INSTALLATION_CONFORM_SPECIFICATIONS", null);
				}

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@FUEL_ID",ObjSolidFuelInfo.FUEL_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					ObjSolidFuelInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/Homeowners/AddSolidFuel.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(ObjSolidFuelInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
					objTransactionInfo.APP_ID = ObjSolidFuelInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = ObjSolidFuelInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = ObjSolidFuelInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"New solid fuel is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				if (returnResult > 0 )
				{
					ObjSolidFuelInfo.FUEL_ID = int.Parse(objSqlParameter.Value.ToString());
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
		/// <param name="objOldSolidFuelInfo">Model object having old information</param>
		/// <param name="ObjSolidFuelInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(Cms.Model.Application.HomeOwners.ClsSolidFuelInfo objOldSolidFuelInfo,Cms.Model.Application.HomeOwners.ClsSolidFuelInfo ObjSolidFuelInfo)
		{
			string strTranXML;
			string strStoredProc="Proc_UpdateSolidFuel";
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjSolidFuelInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",ObjSolidFuelInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",ObjSolidFuelInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@FUEL_ID",ObjSolidFuelInfo.FUEL_ID);
				objDataWrapper.AddParameter("@LOCATION_ID",ObjSolidFuelInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@SUB_LOC_ID",ObjSolidFuelInfo.SUB_LOC_ID);
				objDataWrapper.AddParameter("@MANUFACTURER",ObjSolidFuelInfo.MANUFACTURER);
				objDataWrapper.AddParameter("@BRAND_NAME",ObjSolidFuelInfo.BRAND_NAME);
				objDataWrapper.AddParameter("@MODEL_NUMBER",ObjSolidFuelInfo.MODEL_NUMBER);
				objDataWrapper.AddParameter("@FUEL",ObjSolidFuelInfo.FUEL);
				objDataWrapper.AddParameter("@STOVE_TYPE",ObjSolidFuelInfo.STOVE_TYPE);
				objDataWrapper.AddParameter("@HAVE_LABORATORY_LABEL",ObjSolidFuelInfo.HAVE_LABORATORY_LABEL);
				objDataWrapper.AddParameter("@IS_UNIT",ObjSolidFuelInfo.IS_UNIT);
				objDataWrapper.AddParameter("@UNIT_OTHER_DESC",ObjSolidFuelInfo.UNIT_OTHER_DESC);
				objDataWrapper.AddParameter("@CONSTRUCTION",ObjSolidFuelInfo.CONSTRUCTION);
				objDataWrapper.AddParameter("@LOCATION",ObjSolidFuelInfo.LOCATION);
				objDataWrapper.AddParameter("@LOC_OTHER_DESC",ObjSolidFuelInfo.LOC_OTHER_DESC);

				if(ObjSolidFuelInfo.YEAR_DEVICE_INSTALLED==0)
				{
					objDataWrapper.AddParameter("@YEAR_DEVICE_INSTALLED",null);
				}
				else
				{
					objDataWrapper.AddParameter("@YEAR_DEVICE_INSTALLED",ObjSolidFuelInfo.YEAR_DEVICE_INSTALLED);
				}
				
				objDataWrapper.AddParameter("@WAS_PROF_INSTALL_DONE",ObjSolidFuelInfo.WAS_PROF_INSTALL_DONE);
				objDataWrapper.AddParameter("@INSTALL_INSPECTED_BY",ObjSolidFuelInfo.INSTALL_INSPECTED_BY);
				objDataWrapper.AddParameter("@INSTALL_OTHER_DESC",ObjSolidFuelInfo.INSTALL_OTHER_DESC);
				objDataWrapper.AddParameter("@HEATING_USE",ObjSolidFuelInfo.HEATING_USE);
				objDataWrapper.AddParameter("@HEATING_SOURCE",ObjSolidFuelInfo.HEATING_SOURCE);
				objDataWrapper.AddParameter("@OTHER_DESC",ObjSolidFuelInfo.OTHER_DESC);
				objDataWrapper.AddParameter("@MODIFIED_BY",ObjSolidFuelInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjSolidFuelInfo.LAST_UPDATED_DATETIME);

				if (ObjSolidFuelInfo.STOVE_INSTALLATION_CONFORM_SPECIFICATIONS != 0)
				{
					objDataWrapper.AddParameter("@STOVE_INSTALLATION_CONFORM_SPECIFICATIONS",ObjSolidFuelInfo.STOVE_INSTALLATION_CONFORM_SPECIFICATIONS);
				}
				else
				{
					objDataWrapper.AddParameter("@STOVE_INSTALLATION_CONFORM_SPECIFICATIONS", null);
				}

				if(TransactionLogRequired) 
				{
					ObjSolidFuelInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/Homeowners/AddSolidFuel.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldSolidFuelInfo,ObjSolidFuelInfo);
					if(strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
						objTransactionInfo.APP_ID = ObjSolidFuelInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = ObjSolidFuelInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = ObjSolidFuelInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Solid fuel is modified";
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

		#region GetSolidFuelXml
		public static string GetSolidFuelXml(int intCustoemrId, int intAppId, int intAppVersionId, int intFuelId)
		{
			string strStoredProc = "Proc_GetSolidFuelInformation";
			DataSet dsSolidFuel= new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustoemrId);
				objDataWrapper.AddParameter("@APP_ID",intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
				objDataWrapper.AddParameter("@FUEL_ID",intFuelId);

				dsSolidFuel= objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsSolidFuel.Tables[0].Rows.Count != 0)
				{
					return dsSolidFuel.GetXml();
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
		#endregion

		#region Delete Method
		
		public int Delete(ClsSolidFuelInfo objSolidFuelInfo)
		{
			int returnResult = 0;
			string		strStoredProc	=	"Proc_DeleteAppSolidFuel";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objSolidFuelInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objSolidFuelInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objSolidFuelInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@FUEL_ID",objSolidFuelInfo.FUEL_ID);
					

				if(TransactionLogRequired) 
				{			
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;   
					objTransactionInfo.RECORDED_BY		=	objSolidFuelInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objSolidFuelInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objSolidFuelInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objSolidFuelInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Solid Fuel has been Deleted";
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				

				if(returnResult > 0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
			}
		}
		#endregion

		#region POLICY FUNCTIONS
		public static string GetPolicySolidFuelXml(int intCustoemrId, int intPolId, int intPolVersionId, int intFuelId)
		{
			string strStoredProc = "Proc_GetPolicySolidFuelInformation";
			DataSet dsSolidFuel= new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustoemrId);
				objDataWrapper.AddParameter("@POL_ID",intPolId);
				objDataWrapper.AddParameter("@POL_VERSION_ID",intPolVersionId);
				objDataWrapper.AddParameter("@FUEL_ID",intFuelId);

				dsSolidFuel= objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsSolidFuel.Tables[0].Rows.Count != 0)
				{
					return dsSolidFuel.GetXml();
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
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="ObjSolidFuelInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsPolicySolidFuelInfo ObjSolidFuelInfo)
		{
			string		strStoredProc	=	"Proc_InsertPolicySolidFuel";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjSolidFuelInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",ObjSolidFuelInfo.POLICY_ID );
				objDataWrapper.AddParameter("@POL_VERSION_ID",ObjSolidFuelInfo.POLICY_VERSION_ID );
				objDataWrapper.AddParameter("@LOCATION_ID",ObjSolidFuelInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@SUB_LOC_ID",ObjSolidFuelInfo.SUB_LOC_ID);
				objDataWrapper.AddParameter("@MANUFACTURER",ObjSolidFuelInfo.MANUFACTURER);
				objDataWrapper.AddParameter("@BRAND_NAME",ObjSolidFuelInfo.BRAND_NAME);
				objDataWrapper.AddParameter("@MODEL_NUMBER",ObjSolidFuelInfo.MODEL_NUMBER);
				objDataWrapper.AddParameter("@FUEL",ObjSolidFuelInfo.FUEL);
				objDataWrapper.AddParameter("@STOVE_TYPE",ObjSolidFuelInfo.STOVE_TYPE);
				objDataWrapper.AddParameter("@HAVE_LABORATORY_LABEL",ObjSolidFuelInfo.HAVE_LABORATORY_LABEL);
				objDataWrapper.AddParameter("@IS_UNIT",ObjSolidFuelInfo.IS_UNIT);
				objDataWrapper.AddParameter("@UNIT_OTHER_DESC",ObjSolidFuelInfo.UNIT_OTHER_DESC);
				objDataWrapper.AddParameter("@CONSTRUCTION",ObjSolidFuelInfo.CONSTRUCTION);
				objDataWrapper.AddParameter("@LOCATION",ObjSolidFuelInfo.LOCATION);
				objDataWrapper.AddParameter("@LOC_OTHER_DESC",ObjSolidFuelInfo.LOC_OTHER_DESC);

				if(ObjSolidFuelInfo.YEAR_DEVICE_INSTALLED==0)
				{
					objDataWrapper.AddParameter("@YEAR_DEVICE_INSTALLED",null);
				}
				else
					objDataWrapper.AddParameter("@YEAR_DEVICE_INSTALLED",ObjSolidFuelInfo.YEAR_DEVICE_INSTALLED);
				 
				objDataWrapper.AddParameter("@WAS_PROF_INSTALL_DONE",ObjSolidFuelInfo.WAS_PROF_INSTALL_DONE);
				objDataWrapper.AddParameter("@INSTALL_INSPECTED_BY",ObjSolidFuelInfo.INSTALL_INSPECTED_BY);
				objDataWrapper.AddParameter("@INSTALL_OTHER_DESC",ObjSolidFuelInfo.INSTALL_OTHER_DESC);
				objDataWrapper.AddParameter("@HEATING_USE",ObjSolidFuelInfo.HEATING_USE);
				objDataWrapper.AddParameter("@HEATING_SOURCE",ObjSolidFuelInfo.HEATING_SOURCE);
				objDataWrapper.AddParameter("@OTHER_DESC",ObjSolidFuelInfo.OTHER_DESC);
				objDataWrapper.AddParameter("@CREATED_BY",ObjSolidFuelInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",ObjSolidFuelInfo.CREATED_DATETIME);

				if (ObjSolidFuelInfo.STOVE_INSTALLATION_CONFORM_SPECIFICATIONS != 0)
				{
					objDataWrapper.AddParameter("@STOVE_INSTALLATION_CONFORM_SPECIFICATIONS",ObjSolidFuelInfo.STOVE_INSTALLATION_CONFORM_SPECIFICATIONS);
				}
				else
				{
					objDataWrapper.AddParameter("@STOVE_INSTALLATION_CONFORM_SPECIFICATIONS", null);
				}

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@FUEL_ID",ObjSolidFuelInfo.FUEL_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					ObjSolidFuelInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/Homeowner/PolicyAddSolidFuel.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(ObjSolidFuelInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
					objTransactionInfo.POLICY_ID = ObjSolidFuelInfo.POLICY_ID ;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = ObjSolidFuelInfo.POLICY_VERSION_ID ;
					objTransactionInfo.CLIENT_ID = ObjSolidFuelInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"New solid fuel is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				if (returnResult > 0 )
				{
					ObjSolidFuelInfo.FUEL_ID = int.Parse(objSqlParameter.Value.ToString());
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

		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldSolidFuelInfo">Model object having old information</param>
		/// <param name="ObjSolidFuelInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsPolicySolidFuelInfo objOldSolidFuelInfo,ClsPolicySolidFuelInfo ObjSolidFuelInfo)
		{
			string strTranXML;
			string strStoredProc="Proc_UpdatePolicySolidFuel";
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjSolidFuelInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",ObjSolidFuelInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",ObjSolidFuelInfo.POLICY_VERSION_ID );
				objDataWrapper.AddParameter("@FUEL_ID",ObjSolidFuelInfo.FUEL_ID);
				objDataWrapper.AddParameter("@LOCATION_ID",ObjSolidFuelInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@SUB_LOC_ID",ObjSolidFuelInfo.SUB_LOC_ID);
				objDataWrapper.AddParameter("@MANUFACTURER",ObjSolidFuelInfo.MANUFACTURER);
				objDataWrapper.AddParameter("@BRAND_NAME",ObjSolidFuelInfo.BRAND_NAME);
				objDataWrapper.AddParameter("@MODEL_NUMBER",ObjSolidFuelInfo.MODEL_NUMBER);
				objDataWrapper.AddParameter("@FUEL",ObjSolidFuelInfo.FUEL);
				objDataWrapper.AddParameter("@STOVE_TYPE",ObjSolidFuelInfo.STOVE_TYPE);
				objDataWrapper.AddParameter("@HAVE_LABORATORY_LABEL",ObjSolidFuelInfo.HAVE_LABORATORY_LABEL);
				objDataWrapper.AddParameter("@IS_UNIT",ObjSolidFuelInfo.IS_UNIT);
				objDataWrapper.AddParameter("@UNIT_OTHER_DESC",ObjSolidFuelInfo.UNIT_OTHER_DESC);
				objDataWrapper.AddParameter("@CONSTRUCTION",ObjSolidFuelInfo.CONSTRUCTION);
				objDataWrapper.AddParameter("@LOCATION",ObjSolidFuelInfo.LOCATION);
				objDataWrapper.AddParameter("@LOC_OTHER_DESC",ObjSolidFuelInfo.LOC_OTHER_DESC);

				if(ObjSolidFuelInfo.YEAR_DEVICE_INSTALLED==0)
				{
					objDataWrapper.AddParameter("@YEAR_DEVICE_INSTALLED",null);
				}
				else
					objDataWrapper.AddParameter("@YEAR_DEVICE_INSTALLED",ObjSolidFuelInfo.YEAR_DEVICE_INSTALLED);
				
				objDataWrapper.AddParameter("@WAS_PROF_INSTALL_DONE",ObjSolidFuelInfo.WAS_PROF_INSTALL_DONE);
				objDataWrapper.AddParameter("@INSTALL_INSPECTED_BY",ObjSolidFuelInfo.INSTALL_INSPECTED_BY);
				objDataWrapper.AddParameter("@INSTALL_OTHER_DESC",ObjSolidFuelInfo.INSTALL_OTHER_DESC);
				objDataWrapper.AddParameter("@HEATING_USE",ObjSolidFuelInfo.HEATING_USE);
				objDataWrapper.AddParameter("@HEATING_SOURCE",ObjSolidFuelInfo.HEATING_SOURCE);
				objDataWrapper.AddParameter("@OTHER_DESC",ObjSolidFuelInfo.OTHER_DESC);
				objDataWrapper.AddParameter("@MODIFIED_BY",ObjSolidFuelInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjSolidFuelInfo.LAST_UPDATED_DATETIME);

				if (ObjSolidFuelInfo.STOVE_INSTALLATION_CONFORM_SPECIFICATIONS != 0)
				{
					objDataWrapper.AddParameter("@STOVE_INSTALLATION_CONFORM_SPECIFICATIONS",ObjSolidFuelInfo.STOVE_INSTALLATION_CONFORM_SPECIFICATIONS);
				}
				else
				{
					objDataWrapper.AddParameter("@STOVE_INSTALLATION_CONFORM_SPECIFICATIONS", null);
				}
				if(TransactionLogRequired) 
				{
					ObjSolidFuelInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/Homeowner/PolicyAddSolidFuel.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldSolidFuelInfo,ObjSolidFuelInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
						objTransactionInfo.POLICY_ID  = ObjSolidFuelInfo.POLICY_ID ;
						objTransactionInfo.POLICY_VER_TRACKING_ID  = ObjSolidFuelInfo.POLICY_VERSION_ID ;
						objTransactionInfo.CLIENT_ID = ObjSolidFuelInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Solid fuel is modified";
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

		//public int ActivateDeactivatePolSolidFuel(int CustomerId, int PolId, int PolVersionId,string strStatus,int FUEL_ID)
		public int ActivateDeactivatePolSolidFuel(ClsPolicySolidFuelInfo objSolidFuelInfo,string strStatus)//Changed by Charles on 21-Oct-09 for Itrack 6599
		{
			int intRetval = 0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string strStoredProc = "Proc_ActivateDeactivatePOL_HOME_OWNER_SOLID_FUEL";
			try
			{
				/*objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@POL_ID",PolId);
				objDataWrapper.AddParameter("@POL_VERSION_ID",PolVersionId);
				objDataWrapper.AddParameter("@FUEL_ID",FUEL_ID);*/ //Commented by Charles on 21-Oct-09 for Itrack 6599
				
				//Added by Charles on 21-Oct-09 for Itrack 6599
				objDataWrapper.AddParameter("@CUSTOMER_ID",objSolidFuelInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objSolidFuelInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objSolidFuelInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);
				objDataWrapper.AddParameter("@FUEL_ID",objSolidFuelInfo.FUEL_ID);
				if(TransactionLogRequired)
				{					
					Cms.Model.Maintenance.ClsTransactionInfo objTransaction = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransaction.RECORDED_BY				= objSolidFuelInfo.MODIFIED_BY;					
					objTransaction.CLIENT_ID				= objSolidFuelInfo.CUSTOMER_ID;
					objTransaction.TRANS_TYPE_ID			= 2;
					objTransaction.POLICY_ID				= objSolidFuelInfo.POLICY_ID;
					objTransaction.POLICY_VER_TRACKING_ID	= objSolidFuelInfo.POLICY_VERSION_ID;					
					if(strStatus.ToUpper()=="Y")
						objTransaction.TRANS_DESC		=	"Solid Fuel Has Been Activated";
					else 
						objTransaction.TRANS_DESC		=	"Solid Fuel Has Been Deactivated";								

					intRetval = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransaction);								
				}
				else
				{
					intRetval=objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				//Added till here
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
			if(intRetval>0)
			{
				return intRetval;
			}
			else
			{
				return -1;
			}
		}

		//Function added by Charles on 21-Oct-09 for Itrack 6599
		public void ActivateDeactivateSolidFuel(ClsSolidFuelInfo objSolidFuelInfo,string strStatus)
		{			
			int intRetval = 0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string strStoredProc = "Proc_ActivateDeactivateAPP_SOLID_FUEL";
			try
			{				
				objDataWrapper.AddParameter("@CUSTOMER_ID",objSolidFuelInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objSolidFuelInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objSolidFuelInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);
				objDataWrapper.AddParameter("@CODE",objSolidFuelInfo.FUEL_ID);
				if(TransactionLogRequired)
				{					
					Cms.Model.Maintenance.ClsTransactionInfo objTransaction = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransaction.RECORDED_BY				= objSolidFuelInfo.MODIFIED_BY;					
					objTransaction.CLIENT_ID				= objSolidFuelInfo.CUSTOMER_ID;
					objTransaction.TRANS_TYPE_ID			= 2;
					objTransaction.APP_ID					= objSolidFuelInfo.APP_ID;
					objTransaction.APP_VERSION_ID			= objSolidFuelInfo.APP_VERSION_ID;					
					if(strStatus.ToUpper()=="Y")
						objTransaction.TRANS_DESC		=	"Solid Fuel Has Been Activated";
					else 
						objTransaction.TRANS_DESC		=	"Solid Fuel Has Been Deactivated";								

					intRetval = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransaction);								
				}
				else
				{
					intRetval = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				if(objDataWrapper != null)
					objDataWrapper.Dispose();
			}			
		}
		
		public int Delete(ClsPolicySolidFuelInfo ObjSolidFuelInfo)
		{
			int returnResult = 0;
			string		strStoredProc	=	"Proc_DeletePolSolidFuel";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjSolidFuelInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",ObjSolidFuelInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",ObjSolidFuelInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@FUEL_ID",ObjSolidFuelInfo.FUEL_ID);
					

				if(TransactionLogRequired) 
				{			
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID			=	3;   
					objTransactionInfo.RECORDED_BY				=	ObjSolidFuelInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID				=	ObjSolidFuelInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	ObjSolidFuelInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID				=	ObjSolidFuelInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC				=	"Solid Fuel has been Deleted";
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				

				if(returnResult > 0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
			}
		}
		#endregion

		
	}
}
