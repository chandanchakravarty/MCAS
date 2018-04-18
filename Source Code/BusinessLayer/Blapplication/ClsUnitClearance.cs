/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date				: -	5/19/2005 12:57:26 PM
<End Date				: -	
<Description				: - 	
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
using Cms.BusinessLayer.BlApplication;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Application.HomeOwners;


namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsUnitClearance.
	/// </summary>
	public class ClsUnitClearance :Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_HOME_OWNER_UNIT_CLEARANCE			=	"APP_HOME_OWNER_UNIT_CLEARANCE";
		
		#region Private Instance Variables
		private			bool		boolTransactionLog;
		
		private bool boolTransactionRequired			= true;
		// private int _FUEL_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAPP_HOME_OWNER_UNIT_CLEARANCE";
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
		#region Public Properties
		
		
		public bool TransactionRequired
		{
			get
			{
				return boolTransactionRequired;
			}
			set
			{
				boolTransactionRequired=value;
			}
		}
		#endregion

		#region private Utility Functions
		#endregion
		public ClsUnitClearance()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objUnitClearanceInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsUnitClearanceInfo objUnitClearanceInfo)
		{
			
			string		strStoredProc	=	"Proc_InsertAPP_HOME_OWNER_UNIT_CLEARANCE";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objUnitClearanceInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objUnitClearanceInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objUnitClearanceInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@FUEL_ID",objUnitClearanceInfo.FUEL_ID);
				
				objDataWrapper.AddParameter("@CREATED_BY",objUnitClearanceInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objUnitClearanceInfo.CREATED_DATETIME);

				
				if(objUnitClearanceInfo.STOVE_INSTALL_SPEC !="")
					objDataWrapper.AddParameter("@STOVE_INSTALL_SPEC",objUnitClearanceInfo.STOVE_INSTALL_SPEC);
				else 
					objDataWrapper.AddParameter("@STOVE_INSTALL_SPEC",null);
				
				if(objUnitClearanceInfo.DIST_REAR_WALL_FEET != 0)
					objDataWrapper.AddParameter("@DIST_REAR_WALL_FEET",objUnitClearanceInfo.DIST_REAR_WALL_FEET);
				else 
					objDataWrapper.AddParameter("@DIST_REAR_WALL_FEET",null);
				
				if(objUnitClearanceInfo.DIST_REAR_WALL_INCHES != 0)
					objDataWrapper.AddParameter("@DIST_REAR_WALL_INCHES",objUnitClearanceInfo.DIST_REAR_WALL_INCHES);
				else
					objDataWrapper.AddParameter("@DIST_REAR_WALL_INCHES",null);
				
				if(objUnitClearanceInfo.DIST_LEFT_WALL_FEET != 0)
					objDataWrapper.AddParameter("@DIST_LEFT_WALL_FEET",objUnitClearanceInfo.DIST_LEFT_WALL_FEET);
				else
					objDataWrapper.AddParameter("@DIST_LEFT_WALL_FEET",null);

				if(objUnitClearanceInfo.DIST_LEFT_WALL_INCHES != 0)
					objDataWrapper.AddParameter("@DIST_LEFT_WALL_INCHES",objUnitClearanceInfo.DIST_LEFT_WALL_INCHES);
				else
					objDataWrapper.AddParameter("@DIST_LEFT_WALL_INCHES",null);
				
				if(objUnitClearanceInfo.DIST_RIGHT_WALL_FEET != 0)
					objDataWrapper.AddParameter("@DIST_RIGHT_WALL_FEET",objUnitClearanceInfo.DIST_RIGHT_WALL_FEET);
				else
					objDataWrapper.AddParameter("@DIST_RIGHT_WALL_FEET",null);
				
				if(objUnitClearanceInfo.DIST_RIGHT_WALL_INCHES != 0)
					objDataWrapper.AddParameter("@DIST_RIGHT_WALL_INCHES",objUnitClearanceInfo.DIST_RIGHT_WALL_INCHES);
				else
					objDataWrapper.AddParameter("@DIST_RIGHT_WALL_INCHES",null);
				
				if(objUnitClearanceInfo.DIST_BOTTOM_FLOOR_FEET != 0)
					objDataWrapper.AddParameter("@DIST_BOTTOM_FLOOR_FEET",objUnitClearanceInfo.DIST_BOTTOM_FLOOR_FEET);
				else
					objDataWrapper.AddParameter("@DIST_BOTTOM_FLOOR_FEET",null);
				
				if(objUnitClearanceInfo.DIST_BOTTOM_FLOOR_INCHES != 0)
					objDataWrapper.AddParameter("@DIST_BOTTOM_FLOOR_INCHES",objUnitClearanceInfo.DIST_BOTTOM_FLOOR_INCHES);
				else
					objDataWrapper.AddParameter("@DIST_BOTTOM_FLOOR_INCHES",null);

				if(objUnitClearanceInfo.DIA_PIPE_FEET != 0)
					objDataWrapper.AddParameter("@DIA_PIPE_FEET",objUnitClearanceInfo.DIA_PIPE_FEET);
				else
					objDataWrapper.AddParameter("@DIA_PIPE_FEET",null);
                
				if(objUnitClearanceInfo.DIA_PIPE_INCHES != 0)
					objDataWrapper.AddParameter("@DIA_PIPE_INCHES",objUnitClearanceInfo.DIA_PIPE_INCHES);
				else
					objDataWrapper.AddParameter("@DIA_PIPE_INCHES",null);
				
				if(objUnitClearanceInfo.FRONT_PROTECTION_FEET != 0)
					objDataWrapper.AddParameter("@FRONT_PROTECTION_FEET",objUnitClearanceInfo.FRONT_PROTECTION_FEET);
				else
					objDataWrapper.AddParameter("@FRONT_PROTECTION_FEET",null);
				
				if(objUnitClearanceInfo.FRONT_PROTECTION_INCHES != 0)
					objDataWrapper.AddParameter("@FRONT_PROTECTION_INCHES",objUnitClearanceInfo.FRONT_PROTECTION_INCHES);
				else
					objDataWrapper.AddParameter("@FRONT_PROTECTION_INCHES",null);

				if(objUnitClearanceInfo.STOVE_WALL_FEET != 0)				
					objDataWrapper.AddParameter("@STOVE_WALL_FEET",objUnitClearanceInfo.STOVE_WALL_FEET);
				else
					objDataWrapper.AddParameter("@STOVE_WALL_FEET",null);
				
				if(objUnitClearanceInfo.STOVE_WALL_INCHES != 0)				
                    objDataWrapper.AddParameter("@STOVE_WALL_INCHES",objUnitClearanceInfo.STOVE_WALL_INCHES);
				else
					objDataWrapper.AddParameter("@STOVE_WALL_INCHES",null);
				
				if(objUnitClearanceInfo.TOP_CEILING_FEET != 0)				
					objDataWrapper.AddParameter("@TOP_CEILING_FEET",objUnitClearanceInfo.TOP_CEILING_FEET);
				else
					objDataWrapper.AddParameter("@TOP_CEILING_FEET",null);

				if(objUnitClearanceInfo.TOP_CEILING_INCHES != 0)
				    objDataWrapper.AddParameter("@TOP_CEILING_INCHES",objUnitClearanceInfo.TOP_CEILING_INCHES);
				else
					objDataWrapper.AddParameter("@TOP_CEILING_INCHES",null);
				
				if(objUnitClearanceInfo.SHORT_DIST_WALL_FEET != 0)
                    objDataWrapper.AddParameter("@SHORT_DIST_WALL_FEET",objUnitClearanceInfo.SHORT_DIST_WALL_FEET);
				else
					objDataWrapper.AddParameter("@SHORT_DIST_WALL_FEET",null);

				if(objUnitClearanceInfo.SHORT_DIST_WALL_INCHES != 0)
					objDataWrapper.AddParameter("@SHORT_DIST_WALL_INCHES",objUnitClearanceInfo.SHORT_DIST_WALL_INCHES);
				else
						objDataWrapper.AddParameter("@SHORT_DIST_WALL_INCHES",null);
				
				if(objUnitClearanceInfo.SHORT_DIST_CEILING_FEET != 0)
					objDataWrapper.AddParameter("@SHORT_DIST_CEILING_FEET",objUnitClearanceInfo.SHORT_DIST_CEILING_FEET);
				else
					objDataWrapper.AddParameter("@SHORT_DIST_CEILING_FEET",null);

				if(objUnitClearanceInfo.SHORT_DIST_CEILING_INCHES != 0)
					objDataWrapper.AddParameter("@SHORT_DIST_CEILING_INCHES",objUnitClearanceInfo.SHORT_DIST_CEILING_INCHES);
				else
					objDataWrapper.AddParameter("@SHORT_DIST_CEILING_INCHES",null);
				
				if(objUnitClearanceInfo.DIST_COMBUSTIBLE_FEET != 0)
					objDataWrapper.AddParameter("@DIST_COMBUSTIBLE_FEET",objUnitClearanceInfo.DIST_COMBUSTIBLE_FEET);
                else
					objDataWrapper.AddParameter("@DIST_COMBUSTIBLE_FEET",null);
				
				if(objUnitClearanceInfo.DIST_COMBUSTIBLE_FEET != 0)
					objDataWrapper.AddParameter("@DIST_COMBUSTIBLE_INCHES",objUnitClearanceInfo.DIST_COMBUSTIBLE_INCHES);
				else
					objDataWrapper.AddParameter("@DIST_COMBUSTIBLE_INCHES",null);

				//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@FUEL_ID",objUnitClearanceInfo.FUEL_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objUnitClearanceInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\HomeOwners\UnitClearance.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objUnitClearanceInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
					objTransactionInfo.CLIENT_ID = objUnitClearanceInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = objUnitClearanceInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objUnitClearanceInfo.APP_VERSION_ID;
					objTransactionInfo.TRANS_DESC		=	"New unit clearance is added";
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

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldUnitClearanceInfo">Model object having old information</param>
		/// <param name="objUnitClearanceInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsUnitClearanceInfo objOldUnitClearanceInfo,ClsUnitClearanceInfo objUnitClearanceInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string		strStoredProc	=	"Proc_UpdateAPP_HOME_OWNER_UNIT_CLEARANCE";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objUnitClearanceInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objUnitClearanceInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objUnitClearanceInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@FUEL_ID",objUnitClearanceInfo.FUEL_ID);
				
				objDataWrapper.AddParameter("@MODIFIED_BY",objUnitClearanceInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objUnitClearanceInfo.LAST_UPDATED_DATETIME);
				
				if(objUnitClearanceInfo.STOVE_INSTALL_SPEC !="")
					objDataWrapper.AddParameter("@STOVE_INSTALL_SPEC",objUnitClearanceInfo.STOVE_INSTALL_SPEC);
				else 
					objDataWrapper.AddParameter("@STOVE_INSTALL_SPEC",null);
				
				if(objUnitClearanceInfo.DIST_REAR_WALL_FEET != 0)
					objDataWrapper.AddParameter("@DIST_REAR_WALL_FEET",objUnitClearanceInfo.DIST_REAR_WALL_FEET);
				else 
					objDataWrapper.AddParameter("@DIST_REAR_WALL_FEET",null);
				
				if(objUnitClearanceInfo.DIST_REAR_WALL_INCHES != 0)
					objDataWrapper.AddParameter("@DIST_REAR_WALL_INCHES",objUnitClearanceInfo.DIST_REAR_WALL_INCHES);
				else
					objDataWrapper.AddParameter("@DIST_REAR_WALL_INCHES",null);
				
				if(objUnitClearanceInfo.DIST_LEFT_WALL_FEET != 0)
					objDataWrapper.AddParameter("@DIST_LEFT_WALL_FEET",objUnitClearanceInfo.DIST_LEFT_WALL_FEET);
				else
					objDataWrapper.AddParameter("@DIST_LEFT_WALL_FEET",null);

				if(objUnitClearanceInfo.DIST_LEFT_WALL_INCHES != 0)
					objDataWrapper.AddParameter("@DIST_LEFT_WALL_INCHES",objUnitClearanceInfo.DIST_LEFT_WALL_INCHES);
				else
					objDataWrapper.AddParameter("@DIST_LEFT_WALL_INCHES",null);
				
				if(objUnitClearanceInfo.DIST_RIGHT_WALL_FEET != 0)
					objDataWrapper.AddParameter("@DIST_RIGHT_WALL_FEET",objUnitClearanceInfo.DIST_RIGHT_WALL_FEET);
				else
					objDataWrapper.AddParameter("@DIST_RIGHT_WALL_FEET",null);
				
				if(objUnitClearanceInfo.DIST_RIGHT_WALL_INCHES != 0)
					objDataWrapper.AddParameter("@DIST_RIGHT_WALL_INCHES",objUnitClearanceInfo.DIST_RIGHT_WALL_INCHES);
				else
					objDataWrapper.AddParameter("@DIST_RIGHT_WALL_INCHES",null);
				
				if(objUnitClearanceInfo.DIST_BOTTOM_FLOOR_FEET != 0)
					objDataWrapper.AddParameter("@DIST_BOTTOM_FLOOR_FEET",objUnitClearanceInfo.DIST_BOTTOM_FLOOR_FEET);
				else
					objDataWrapper.AddParameter("@DIST_BOTTOM_FLOOR_FEET",null);
				
				if(objUnitClearanceInfo.DIST_BOTTOM_FLOOR_INCHES != 0)
					objDataWrapper.AddParameter("@DIST_BOTTOM_FLOOR_INCHES",objUnitClearanceInfo.DIST_BOTTOM_FLOOR_INCHES);
				else
					objDataWrapper.AddParameter("@DIST_BOTTOM_FLOOR_INCHES",null);

				if(objUnitClearanceInfo.DIA_PIPE_FEET != 0)
					objDataWrapper.AddParameter("@DIA_PIPE_FEET",objUnitClearanceInfo.DIA_PIPE_FEET);
				else
					objDataWrapper.AddParameter("@DIA_PIPE_FEET",null);
                
				if(objUnitClearanceInfo.DIA_PIPE_INCHES != 0)
					objDataWrapper.AddParameter("@DIA_PIPE_INCHES",objUnitClearanceInfo.DIA_PIPE_INCHES);
				else
					objDataWrapper.AddParameter("@DIA_PIPE_INCHES",null);
				
				if(objUnitClearanceInfo.FRONT_PROTECTION_FEET != 0)
					objDataWrapper.AddParameter("@FRONT_PROTECTION_FEET",objUnitClearanceInfo.FRONT_PROTECTION_FEET);
				else
					objDataWrapper.AddParameter("@FRONT_PROTECTION_FEET",null);
				
				if(objUnitClearanceInfo.FRONT_PROTECTION_INCHES != 0)
					objDataWrapper.AddParameter("@FRONT_PROTECTION_INCHES",objUnitClearanceInfo.FRONT_PROTECTION_INCHES);
				else
					objDataWrapper.AddParameter("@FRONT_PROTECTION_INCHES",null);

				if(objUnitClearanceInfo.STOVE_WALL_FEET != 0)				
					objDataWrapper.AddParameter("@STOVE_WALL_FEET",objUnitClearanceInfo.STOVE_WALL_FEET);
				else
					objDataWrapper.AddParameter("@STOVE_WALL_FEET",null);
				
				if(objUnitClearanceInfo.STOVE_WALL_INCHES != 0)				
					objDataWrapper.AddParameter("@STOVE_WALL_INCHES",objUnitClearanceInfo.STOVE_WALL_INCHES);
				else
					objDataWrapper.AddParameter("@STOVE_WALL_INCHES",null);
				
				if(objUnitClearanceInfo.TOP_CEILING_FEET != 0)				
					objDataWrapper.AddParameter("@TOP_CEILING_FEET",objUnitClearanceInfo.TOP_CEILING_FEET);
				else
					objDataWrapper.AddParameter("@TOP_CEILING_FEET",null);

				if(objUnitClearanceInfo.TOP_CEILING_INCHES != 0)
					objDataWrapper.AddParameter("@TOP_CEILING_INCHES",objUnitClearanceInfo.TOP_CEILING_INCHES);
				else
					objDataWrapper.AddParameter("@TOP_CEILING_INCHES",null);
				
				if(objUnitClearanceInfo.SHORT_DIST_WALL_FEET != 0)
					objDataWrapper.AddParameter("@SHORT_DIST_WALL_FEET",objUnitClearanceInfo.SHORT_DIST_WALL_FEET);
				else
					objDataWrapper.AddParameter("@SHORT_DIST_WALL_FEET",null);

				if(objUnitClearanceInfo.SHORT_DIST_WALL_INCHES != 0)
					objDataWrapper.AddParameter("@SHORT_DIST_WALL_INCHES",objUnitClearanceInfo.SHORT_DIST_WALL_INCHES);
				else
					objDataWrapper.AddParameter("@SHORT_DIST_WALL_INCHES",null);
				
				if(objUnitClearanceInfo.SHORT_DIST_CEILING_FEET != 0)
					objDataWrapper.AddParameter("@SHORT_DIST_CEILING_FEET",objUnitClearanceInfo.SHORT_DIST_CEILING_FEET);
				else
					objDataWrapper.AddParameter("@SHORT_DIST_CEILING_FEET",null);

				if(objUnitClearanceInfo.SHORT_DIST_CEILING_INCHES != 0)
					objDataWrapper.AddParameter("@SHORT_DIST_CEILING_INCHES",objUnitClearanceInfo.SHORT_DIST_CEILING_INCHES);
				else
					objDataWrapper.AddParameter("@SHORT_DIST_CEILING_INCHES",null);
				
				if(objUnitClearanceInfo.DIST_COMBUSTIBLE_FEET != 0)
					objDataWrapper.AddParameter("@DIST_COMBUSTIBLE_FEET",objUnitClearanceInfo.DIST_COMBUSTIBLE_FEET);
				else
					objDataWrapper.AddParameter("@DIST_COMBUSTIBLE_FEET",null);
				
				if(objUnitClearanceInfo.DIST_COMBUSTIBLE_FEET != 0)
					objDataWrapper.AddParameter("@DIST_COMBUSTIBLE_INCHES",objUnitClearanceInfo.DIST_COMBUSTIBLE_INCHES);
				else
					objDataWrapper.AddParameter("@DIST_COMBUSTIBLE_INCHES",null);if(TransactionRequired) 
				{
					objUnitClearanceInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\HomeOwners\UnitClearance.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldUnitClearanceInfo,objUnitClearanceInfo);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID = objUnitClearanceInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = objUnitClearanceInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objUnitClearanceInfo.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
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

		#region GetUnitClearanceXml
		public static string GetUnitClearanceXml(int intCustomer_ID,int intApp_ID,int intApp_Version_ID ,int intFuel_ID)
		{
			
			string strStoredProc = "Proc_GetUnitClearance";
			DataSet dsProtectDevices = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomer_ID);
				objDataWrapper.AddParameter("@APP_ID",intApp_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intApp_Version_ID);
				objDataWrapper.AddParameter("@FUEL_ID",intFuel_ID);
				dsProtectDevices = objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsProtectDevices.Tables[0].Rows.Count != 0)
				{
					return dsProtectDevices.GetXml();
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


	}
}
