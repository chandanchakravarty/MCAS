/******************************************************************************************
<Author				: -   Nidhi
<Start Date				: -	5/18/2005 6:14:50 PM
<End Date				: -	
<Description				: - 	Business Class for Watercraft Engine Info
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified

<Modified Date			: - Anurag Verma
<Modified By			: - Oct 11, 2005
<Purpose				: - updating add watercraft and update watercraft functions

<Modified Date			: - Vijay Arora
<Modified By			: - 22-11-2005
<Purpose				: - Added the Policy Functions
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlApplication;
//using Cms.Model.Application.Watercrafts ;
using Cms.Model.Policy.Watercraft;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon ;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsWatercraftEngine.
	/// </summary>
	public class ClsWatercraftEngine: clsapplication 
	{
		
		private const	string		APP_WATERCRAFT_ENGINE_INFO			=	"APP_WATERCRAFT_ENGINE_INFO";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		// private int _ENGINE_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAPP_WATERCRAFT_ENGINE_INFO";
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
		public static string FetchWatercraftEngineInfoXML(int CustomerID,int AppID, int AppVersionID, int EngineID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@ENGINE_ID",EngineID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetWatercraftEngineInfo");
			
				//return dsTemp.GetXml();				
				if (dsTemp.Tables[0].Rows.Count != 0)				
					return ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);				
				else
					return "";
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		
		}

		public static int GetNewWatercraftEngineNumber(int CustomerID,int AppID, int AppVersionID, int boatNo)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@BOAT_NO",boatNo,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAppNewWatercraftEngineNumber");
				return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());		
			} 
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
	}

		public static int GetPolNewWatercraftEngineNumber(int CustomerID,int PolicyID, int PolicyVersionID,string CalledFrom, int boatNo)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@BOAT_NO",boatNo,SqlDbType.Int);
				if (CalledFrom == "UMB")
				{dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolNewUmbWatercraftEngineNumber");
				}
				else
				{dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolNewWatercraftEngineNumber");}
				return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());		
			} 
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}

		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsWatercraftEngine()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objWatercraftEngineInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		/*public int Add(ClsWatercraftEngineInfo objWatercraftEngineInfo)
		{
			string		strStoredProc	=	"Proc_InsertWatercraftEngineInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEngineInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftEngineInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftEngineInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ENGINE_NO",objWatercraftEngineInfo.ENGINE_NO);
				objDataWrapper.AddParameter("@YEAR",objWatercraftEngineInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWatercraftEngineInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWatercraftEngineInfo.MODEL);
				objDataWrapper.AddParameter("@SERIAL_NO",objWatercraftEngineInfo.SERIAL_NO);
				objDataWrapper.AddParameter("@HORSEPOWER",objWatercraftEngineInfo.HORSEPOWER);
				objDataWrapper.AddParameter("@IS_ACTIVE",objWatercraftEngineInfo.IS_ACTIVE);
			
			
				if(objWatercraftEngineInfo.INSURING_VALUE !=0)
					objDataWrapper.AddParameter("@INSURING_VALUE",objWatercraftEngineInfo.INSURING_VALUE);

				objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objWatercraftEngineInfo.ASSOCIATED_BOAT);
				objDataWrapper.AddParameter("@OTHER",objWatercraftEngineInfo.OTHER);				
				objDataWrapper.AddParameter("@CREATED_BY",objWatercraftEngineInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objWatercraftEngineInfo.CREATED_DATETIME);			 
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ENGINE_ID",objWatercraftEngineInfo.ENGINE_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLog)
				{
					objWatercraftEngineInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftEngineDetails.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftEngineInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objWatercraftEngineInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = objWatercraftEngineInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objWatercraftEngineInfo.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftEngineInfo.CREATED_BY;
					//objTransactionInfo.TRANS_DESC		=	"New engine detail is added";
					objTransactionInfo.TRANS_DESC		=	" Outboard Engine Information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int ENGINE_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (ENGINE_ID == -1)
				{
					return -1;
				}
				else if(ENGINE_ID == -2)
				{
					return -2;
				}
				else
				{
					objWatercraftEngineInfo.ENGINE_ID = ENGINE_ID;
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
        */
		public int DeletePolicyEngine(ClsPolicyWatercraftEngineInfo objWatercraftEngineInfo)
		{

			string		strStoredProc	=	"Proc_DeletePolWatercraftEngine";		
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMERID",objWatercraftEngineInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftEngineInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftEngineInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@ENGINE_ID",objWatercraftEngineInfo.ENGINE_ID);
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					objWatercraftEngineInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\Watercraft\PolicyAddWatercraftEngine.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID = objWatercraftEngineInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID= objWatercraftEngineInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objWatercraftEngineInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftEngineInfo.MODIFIED_BY;					
					objTransactionInfo.TRANS_DESC		=	"Outboard Engine Info has been deleted";				
					objTransactionInfo.CUSTOM_INFO		=	";Year = " + objWatercraftEngineInfo.YEAR + ";Make = " + objWatercraftEngineInfo.MAKE + ";Model = " + objWatercraftEngineInfo.MODEL + ";Serial # = " + objWatercraftEngineInfo.SERIAL_NO;
									
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
		public void ActivateDeactivatePolWatercraftengine(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEngineInfo objWatercraftEngineInfo, string strStatus,string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivatePolWatercraftEngine";		
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEngineInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftEngineInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftEngineInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@ENGINE_ID",objWatercraftEngineInfo.ENGINE_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);		
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					objWatercraftEngineInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\Watercraft\PolicyAddWatercraftEngine.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID = objWatercraftEngineInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID= objWatercraftEngineInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objWatercraftEngineInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftEngineInfo.MODIFIED_BY;
					objTransactionInfo.CUSTOM_INFO		=	";Year = " + objWatercraftEngineInfo.YEAR + ";Make = " + objWatercraftEngineInfo.MAKE + ";Model = " + objWatercraftEngineInfo.MODEL + ";Serial # = " + objWatercraftEngineInfo.SERIAL_NO;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"Outboard Engine Info has been activated";
					else 
						objTransactionInfo.TRANS_DESC		=	"Outboard Engine Info has been deactivated";
					
									
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
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
        /*
		public void ActivateDeactivateWatercraftengine(ClsWatercraftEngineInfo objWatercraftEngineInfo, string strStatus,string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateWatercraftengine";		
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEngineInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftEngineInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftEngineInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ENGINE_ID",objWatercraftEngineInfo.ENGINE_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);			
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					objWatercraftEngineInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftEngineDetails.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objWatercraftEngineInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID= objWatercraftEngineInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objWatercraftEngineInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftEngineInfo.MODIFIED_BY;
					objTransactionInfo.CUSTOM_INFO		=	";Year = " + objWatercraftEngineInfo.YEAR + ";Make = " + objWatercraftEngineInfo.MAKE + ";Model = " + objWatercraftEngineInfo.MODEL + ";Serial # = " + objWatercraftEngineInfo.SERIAL_NO;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"Outboard Engine Info has been activated";
					else 
						objTransactionInfo.TRANS_DESC		=	"Outboard Engine Info has been deactivated";
					
									
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
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

		public int DeleteEngine(ClsWatercraftEngineInfo objWatercraftEngineInfo)
		{
			string		strStoredProc	=	"Proc_DeleteAppWatercraftEngine";		
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMERID",objWatercraftEngineInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APPID",objWatercraftEngineInfo.APP_ID);
				objDataWrapper.AddParameter("@APPVERSIONID",objWatercraftEngineInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ENGINE_ID",objWatercraftEngineInfo.ENGINE_ID);	
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					objWatercraftEngineInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftEngineDetails.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objWatercraftEngineInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID= objWatercraftEngineInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objWatercraftEngineInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftEngineInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Outboard Engine Info has been deleted";									
					objTransactionInfo.CUSTOM_INFO		=	";Year = " + objWatercraftEngineInfo.YEAR + ";Make = " + objWatercraftEngineInfo.MAKE + ";Model = " + objWatercraftEngineInfo.MODEL + ";Serial # = " + objWatercraftEngineInfo.SERIAL_NO;				
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


		public int Add(ClsWatercraftEngineInfo objWatercraftEngineInfo,string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_InsertWatercraftEngineInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEngineInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftEngineInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftEngineInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ENGINE_NO",objWatercraftEngineInfo.ENGINE_NO);
				objDataWrapper.AddParameter("@YEAR",objWatercraftEngineInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWatercraftEngineInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWatercraftEngineInfo.MODEL);
				objDataWrapper.AddParameter("@SERIAL_NO",objWatercraftEngineInfo.SERIAL_NO);
				objDataWrapper.AddParameter("@HORSEPOWER",objWatercraftEngineInfo.HORSEPOWER);
				objDataWrapper.AddParameter("@IS_ACTIVE",objWatercraftEngineInfo.IS_ACTIVE);
			
			
				if(objWatercraftEngineInfo.INSURING_VALUE !=0)
					objDataWrapper.AddParameter("@INSURING_VALUE",objWatercraftEngineInfo.INSURING_VALUE);
				objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objWatercraftEngineInfo.ASSOCIATED_BOAT);
				objDataWrapper.AddParameter("@FUEL_TYPE",objWatercraftEngineInfo.FUEL_TYPE);
				objDataWrapper.AddParameter("@OTHER",objWatercraftEngineInfo.OTHER);				
				objDataWrapper.AddParameter("@CREATED_BY",objWatercraftEngineInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objWatercraftEngineInfo.CREATED_DATETIME);			 
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ENGINE_ID",objWatercraftEngineInfo.ENGINE_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLog)
				{
					objWatercraftEngineInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftEngineDetails.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftEngineInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objWatercraftEngineInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = objWatercraftEngineInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objWatercraftEngineInfo.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftEngineInfo.CREATED_BY;
					//objTransactionInfo.TRANS_DESC		=	"New engine detail is added";
					objTransactionInfo.TRANS_DESC		=	" Outboard Engine Information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int ENGINE_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (ENGINE_ID == -1)
				{
					return -1;
				}
				else
				{
					objWatercraftEngineInfo.ENGINE_ID = ENGINE_ID;
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
		}*/
		#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldWatercraftEngineInfo">Model object having old information</param>
		/// <param name="objWatercraftEngineInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		/*public int Update(ClsWatercraftEngineInfo objOldWatercraftEngineInfo,ClsWatercraftEngineInfo objWatercraftEngineInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string		strStoredProc	=	"Proc_UpdateWatercraftEngineInfo";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEngineInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftEngineInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftEngineInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ENGINE_ID",objWatercraftEngineInfo.ENGINE_ID );
				objDataWrapper.AddParameter("@ENGINE_NO",objWatercraftEngineInfo.ENGINE_NO);
				objDataWrapper.AddParameter("@YEAR",objWatercraftEngineInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWatercraftEngineInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWatercraftEngineInfo.MODEL);
				objDataWrapper.AddParameter("@SERIAL_NO",objWatercraftEngineInfo.SERIAL_NO);
				objDataWrapper.AddParameter("@HORSEPOWER",objWatercraftEngineInfo.HORSEPOWER);
				
				if(objWatercraftEngineInfo.INSURING_VALUE!=0)
				objDataWrapper.AddParameter("@INSURING_VALUE",objWatercraftEngineInfo.INSURING_VALUE);
				objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objWatercraftEngineInfo.ASSOCIATED_BOAT);
				objDataWrapper.AddParameter("@OTHER",objWatercraftEngineInfo.OTHER);
				objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftEngineInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftEngineInfo.LAST_UPDATED_DATETIME);
				if(TransactionLog) 
				{
					objWatercraftEngineInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftEngineDetails.aspx.resx");
					string strUpdate = objBuilder.GetUpdateSQL(objOldWatercraftEngineInfo,objWatercraftEngineInfo,out strTranXML);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.CLIENT_ID = objWatercraftEngineInfo.CUSTOMER_ID;
						objTransactionInfo.APP_ID = objWatercraftEngineInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objWatercraftEngineInfo.APP_VERSION_ID;
						objTransactionInfo.RECORDED_BY		=	objWatercraftEngineInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	" Outboard Engine Information is modified";
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

		public int Update(ClsWatercraftEngineInfo objOldWatercraftEngineInfo,ClsWatercraftEngineInfo objWatercraftEngineInfo,string strCustomInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string		strStoredProc	=	"Proc_UpdateWatercraftEngineInfo";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEngineInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftEngineInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftEngineInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ENGINE_ID",objWatercraftEngineInfo.ENGINE_ID );
				objDataWrapper.AddParameter("@ENGINE_NO",objWatercraftEngineInfo.ENGINE_NO);
				objDataWrapper.AddParameter("@YEAR",objWatercraftEngineInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWatercraftEngineInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWatercraftEngineInfo.MODEL);
				objDataWrapper.AddParameter("@SERIAL_NO",objWatercraftEngineInfo.SERIAL_NO);
				objDataWrapper.AddParameter("@HORSEPOWER",objWatercraftEngineInfo.HORSEPOWER);
				
				if(objWatercraftEngineInfo.INSURING_VALUE!=0)
					objDataWrapper.AddParameter("@INSURING_VALUE",objWatercraftEngineInfo.INSURING_VALUE);
				objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objWatercraftEngineInfo.ASSOCIATED_BOAT);
				objDataWrapper.AddParameter("@FUEL_TYPE",objWatercraftEngineInfo.FUEL_TYPE);
				objDataWrapper.AddParameter("@OTHER",objWatercraftEngineInfo.OTHER);
				objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftEngineInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftEngineInfo.LAST_UPDATED_DATETIME);
				if(TransactionLog) 
				{
					objWatercraftEngineInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftEngineDetails.aspx.resx");
					string strUpdate = objBuilder.GetUpdateSQL(objOldWatercraftEngineInfo,objWatercraftEngineInfo,out strTranXML);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.CLIENT_ID = objWatercraftEngineInfo.CUSTOMER_ID;
						objTransactionInfo.APP_ID = objWatercraftEngineInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objWatercraftEngineInfo.APP_VERSION_ID;
						objTransactionInfo.RECORDED_BY		=	objWatercraftEngineInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	" Outboard Engine Information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
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
		}*/
		#endregion


		#region POLICY FUNCTIONS

		/// <summary>
		/// Gets the Policy WaterCraft Engine Details.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="EngineID"></param>
		/// <returns></returns>
		public static string FetchPolicyWatercraftEngineInfoXML(int CustomerID,int PolicyID, int PolicyVersionID, int EngineID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@ENGINE_ID",EngineID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyWatercraftEngineInfo");
			
				return dsTemp.GetXml();
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		
		}


		public static string FetchPolicyUmbrellaWatercraftEngineInfoXML(int CustomerID,int PolicyID, int PolicyVersionID, int EngineID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@ENGINE_ID",EngineID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyUmbrellaWatercraftEngineInfo");
			
				return dsTemp.GetXml();
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		
		}

		
		/// <summary>
		/// Saves the policy watercarft Engine Details.
		/// </summary>
		/// <param name="objWatercraftEngineInfo"></param>
		/// <returns></returns>
		public int AddPolicyWaterCraftEngine(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEngineInfo objWatercraftEngineInfo)
		{
			string		strStoredProc	=	"Proc_InsertPolicyWatercraftEngine";
			int ENGINE_ID = -1;
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEngineInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftEngineInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftEngineInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@ENGINE_NO",objWatercraftEngineInfo.ENGINE_NO);
				objDataWrapper.AddParameter("@YEAR",objWatercraftEngineInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWatercraftEngineInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWatercraftEngineInfo.MODEL);
				objDataWrapper.AddParameter("@SERIAL_NO",objWatercraftEngineInfo.SERIAL_NO);
				objDataWrapper.AddParameter("@HORSEPOWER",objWatercraftEngineInfo.HORSEPOWER);
			
			
				if(objWatercraftEngineInfo.INSURING_VALUE !=0)
					objDataWrapper.AddParameter("@INSURING_VALUE",objWatercraftEngineInfo.INSURING_VALUE);

				objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objWatercraftEngineInfo.ASSOCIATED_BOAT);
				objDataWrapper.AddParameter("@FUEL_TYPE",objWatercraftEngineInfo.FUEL_TYPE);
				objDataWrapper.AddParameter("@OTHER",objWatercraftEngineInfo.OTHER);				
				objDataWrapper.AddParameter("@CREATED_BY",objWatercraftEngineInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objWatercraftEngineInfo.CREATED_DATETIME);			 
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ENGINE_ID",objWatercraftEngineInfo.ENGINE_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLog)
				{
					objWatercraftEngineInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\Watercraft\PolicyAddWatercraftEngine.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftEngineInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objWatercraftEngineInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID = objWatercraftEngineInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objWatercraftEngineInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftEngineInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New policy engine detail is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if(returnResult>0)
				{
					ENGINE_ID = int.Parse(objSqlParameter.Value.ToString());
					objWatercraftEngineInfo.ENGINE_ID = ENGINE_ID;
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				else
				{
					objDataWrapper.ClearParameteres();
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				}				
				return ENGINE_ID;
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


		public int AddPolicyUmbrellaWaterCraftEngine(Cms.Model.Policy.Umbrella.ClsWaterCraftEngineInfo objWatercraftEngineInfo)
		{
			string		strStoredProc	=	"Proc_InsertPolicyUmbrellaWatercraftEngine";
			DateTime	RecordDate		=	DateTime.Now;
			int ENGINE_ID = -1;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEngineInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftEngineInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftEngineInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@ENGINE_NO",objWatercraftEngineInfo.ENGINE_NO);
				objDataWrapper.AddParameter("@YEAR",objWatercraftEngineInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWatercraftEngineInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWatercraftEngineInfo.MODEL);
				objDataWrapper.AddParameter("@SERIAL_NO",objWatercraftEngineInfo.SERIAL_NO);
				objDataWrapper.AddParameter("@HORSEPOWER",objWatercraftEngineInfo.HORSEPOWER);
			
			
				if(objWatercraftEngineInfo.INSURING_VALUE !=0)
					objDataWrapper.AddParameter("@INSURING_VALUE",objWatercraftEngineInfo.INSURING_VALUE);

				objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objWatercraftEngineInfo.ASSOCIATED_BOAT);
				objDataWrapper.AddParameter("@OTHER",objWatercraftEngineInfo.OTHER);				
				objDataWrapper.AddParameter("@CREATED_BY",objWatercraftEngineInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objWatercraftEngineInfo.CREATED_DATETIME);			 
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ENGINE_ID",objWatercraftEngineInfo.ENGINE_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLog)
				{
					objWatercraftEngineInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\Watercraft\PolicyAddWatercraftEngine.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftEngineInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objWatercraftEngineInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID = objWatercraftEngineInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objWatercraftEngineInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftEngineInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New policy engine detail is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if(returnResult>0)
				{
					ENGINE_ID = int.Parse(objSqlParameter.Value.ToString());
					objWatercraftEngineInfo.ENGINE_ID = ENGINE_ID;
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				else
				{
					objDataWrapper.ClearParameteres();
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				}
				return ENGINE_ID;
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
		/// Updates the policy watercraft engine Details.
		/// </summary>
		/// <param name="objOldWatercraftEngineInfo"></param>
		/// <param name="objWatercraftEngineInfo"></param>
		/// <returns></returns>
		public int UpdatePolicyWaterCraftEngine(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEngineInfo objOldWatercraftEngineInfo,Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEngineInfo objWatercraftEngineInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string		strStoredProc	=	"Proc_UpdatePolicyWatercraftEngine";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEngineInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftEngineInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftEngineInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@ENGINE_ID",objWatercraftEngineInfo.ENGINE_ID );
				objDataWrapper.AddParameter("@ENGINE_NO",objWatercraftEngineInfo.ENGINE_NO);
				objDataWrapper.AddParameter("@YEAR",objWatercraftEngineInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWatercraftEngineInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWatercraftEngineInfo.MODEL);
				objDataWrapper.AddParameter("@SERIAL_NO",objWatercraftEngineInfo.SERIAL_NO);
				objDataWrapper.AddParameter("@HORSEPOWER",objWatercraftEngineInfo.HORSEPOWER);
				
				if(objWatercraftEngineInfo.INSURING_VALUE!=0)
					objDataWrapper.AddParameter("@INSURING_VALUE",objWatercraftEngineInfo.INSURING_VALUE);
				objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objWatercraftEngineInfo.ASSOCIATED_BOAT);
				objDataWrapper.AddParameter("@FUEL_TYPE",objWatercraftEngineInfo.FUEL_TYPE);
				objDataWrapper.AddParameter("@OTHER",objWatercraftEngineInfo.OTHER);
				objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftEngineInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftEngineInfo.LAST_UPDATED_DATETIME);
				if(TransactionLog) 
				{
					objWatercraftEngineInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\Watercraft\PolicyAddWatercraftEngine.aspx.resx");
					string strUpdate = objBuilder.GetUpdateSQL(objOldWatercraftEngineInfo,objWatercraftEngineInfo,out strTranXML);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.CLIENT_ID = objWatercraftEngineInfo.CUSTOMER_ID;
						objTransactionInfo.POLICY_ID = objWatercraftEngineInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objWatercraftEngineInfo.POLICY_VERSION_ID;
						objTransactionInfo.RECORDED_BY		=	objWatercraftEngineInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Policy watercraft engine detail is modified";
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


		public int UpdatePolicyUmbrellaWaterCraftEngine(Cms.Model.Policy.Umbrella.ClsWaterCraftEngineInfo objOldWatercraftEngineInfo,Cms.Model.Policy.Umbrella.ClsWaterCraftEngineInfo objWatercraftEngineInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string		strStoredProc	=	"Proc_UpdatePolicyUmbrellaWatercraftEngine";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEngineInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftEngineInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftEngineInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@ENGINE_ID",objWatercraftEngineInfo.ENGINE_ID );
				objDataWrapper.AddParameter("@ENGINE_NO",objWatercraftEngineInfo.ENGINE_NO);
				objDataWrapper.AddParameter("@YEAR",objWatercraftEngineInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWatercraftEngineInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWatercraftEngineInfo.MODEL);
				objDataWrapper.AddParameter("@SERIAL_NO",objWatercraftEngineInfo.SERIAL_NO);
				objDataWrapper.AddParameter("@HORSEPOWER",objWatercraftEngineInfo.HORSEPOWER);
				
				if(objWatercraftEngineInfo.INSURING_VALUE!=0)
					objDataWrapper.AddParameter("@INSURING_VALUE",objWatercraftEngineInfo.INSURING_VALUE);
				objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objWatercraftEngineInfo.ASSOCIATED_BOAT);
				objDataWrapper.AddParameter("@OTHER",objWatercraftEngineInfo.OTHER);
				objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftEngineInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftEngineInfo.LAST_UPDATED_DATETIME);
				if(TransactionLog) 
				{
					objWatercraftEngineInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\Watercraft\PolicyAddWatercraftEngine.aspx.resx");
					string strUpdate = objBuilder.GetUpdateSQL(objOldWatercraftEngineInfo,objWatercraftEngineInfo,out strTranXML);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.CLIENT_ID = objWatercraftEngineInfo.CUSTOMER_ID;
						objTransactionInfo.POLICY_ID = objWatercraftEngineInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objWatercraftEngineInfo.POLICY_VERSION_ID;
						objTransactionInfo.RECORDED_BY		=	objWatercraftEngineInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Policy watercraft engine detail is modified";
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
		 
	}
}
