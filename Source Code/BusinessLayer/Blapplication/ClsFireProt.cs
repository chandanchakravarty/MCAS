/******************************************************************************************
<Author				: -   Priya
<Start Date				: -	5/20/2005 1:01:32 PM
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
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Policy.Homeowners;   

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsFireProt : Cms.BusinessLayer.BlApplication.clsapplication
	{
		private const	string		APP_HOME_OWNER_FIRE_PROT_CLEAN			=	"APP_HOME_OWNER_FIRE_PROT_CLEAN";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		// private int _FUEL_ID;
		//private const string ACTIVATE_DEACTIVATE_PROC	= "";
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
		public ClsFireProt()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			//base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="ObjFireProtInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(Cms.Model.Application.HomeOwners.ClsFireProtInfo ObjFireProtInfo)
		{
			string		strStoredProc	=	"Proc_InsertFireProtClean";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{   objDataWrapper.AddParameter("@FUEL_ID",ObjFireProtInfo.FUEL_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjFireProtInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",ObjFireProtInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",ObjFireProtInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@IS_SMOKE_DETECT0R",ObjFireProtInfo.IS_SMOKE_DETECTOR);
				                             
				objDataWrapper.AddParameter("@IS_PROTECTIVE_MAT_FLOOR",ObjFireProtInfo.IS_PROTECTIVE_MAT_FLOOR);
				objDataWrapper.AddParameter("@IS_PROTECTIVE_MAT_WALLS",ObjFireProtInfo.IS_PROTECTIVE_MAT_WALLS);
				objDataWrapper.AddParameter("@PROT_MAT_SPACED",ObjFireProtInfo.PROT_MAT_SPACED);
				objDataWrapper.AddParameter("@STOVE_SMOKE_PIPE_CLEANED",ObjFireProtInfo.STOVE_SMOKE_PIPE_CLEANED);
				objDataWrapper.AddParameter("@STOVE_CLEANER",ObjFireProtInfo.STOVE_CLEANER);
				objDataWrapper.AddParameter("@REMARKS",ObjFireProtInfo.REMARKS);
				//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@FUEL_ID",ObjFireProtInfo.FUEL_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					ObjFireProtInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/Homeowners/AddFireProt.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(ObjFireProtInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = ObjFireProtInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = ObjFireProtInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = ObjFireProtInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	ObjFireProtInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1719", "");// "New fire protection/Cleaning information is added";//Changed by Charles on 21-Oct-09 for Itrack 6599
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
		/// <param name="objOldFireProtInfo">Model object having old information</param>
		/// <param name="ObjFireProtInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(Cms.Model.Application.HomeOwners.ClsFireProtInfo objOldFireProtInfo,Cms.Model.Application.HomeOwners.ClsFireProtInfo ObjFireProtInfo)
		{ 
			string strStoredProc="Proc_UpdateFireProtClean";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjFireProtInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",ObjFireProtInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",ObjFireProtInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@FUEL_ID",ObjFireProtInfo.FUEL_ID);
				objDataWrapper.AddParameter("@IS_SMOKE_DETECTOR",ObjFireProtInfo.IS_SMOKE_DETECTOR);
				objDataWrapper.AddParameter("@IS_PROTECTIVE_MAT_FLOOR",ObjFireProtInfo.IS_PROTECTIVE_MAT_FLOOR);
				objDataWrapper.AddParameter("@IS_PROTECTIVE_MAT_WALLS",ObjFireProtInfo.IS_PROTECTIVE_MAT_WALLS);
				objDataWrapper.AddParameter("@PROT_MAT_SPACED",ObjFireProtInfo.PROT_MAT_SPACED);
				objDataWrapper.AddParameter("@STOVE_SMOKE_PIPE_CLEANED",ObjFireProtInfo.STOVE_SMOKE_PIPE_CLEANED);
				objDataWrapper.AddParameter("@STOVE_CLEANER",ObjFireProtInfo.STOVE_CLEANER);
				objDataWrapper.AddParameter("@REMARKS",ObjFireProtInfo.REMARKS);
				if(TransactionLogRequired) 
				{
					ObjFireProtInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/Homeowners/AddFireProt.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldFireProtInfo,ObjFireProtInfo);
					if(strTranXML=="<LabelFieldMapping></LabelFieldMapping>" || strTranXML=="") 
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{						
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.APP_ID = ObjFireProtInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = ObjFireProtInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = ObjFireProtInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	ObjFireProtInfo.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1720", "");// "Fire prot information is modified";
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

		#region GetFireProtXml
		public static string GetFireProtXml(int intCustoemrId, int intAppId, int intAppVersionId, int intFuelId)
		{
			string strStoredProc = "Proc_GetFireProtInformation";
			DataSet dsFireProt= new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustoemrId);
				objDataWrapper.AddParameter("@APP_ID",intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
				objDataWrapper.AddParameter("@FUEL_ID",intFuelId);

				dsFireProt= objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsFireProt.Tables[0].Rows.Count != 0)
				{
					return dsFireProt.GetXml();
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

		#region POLICY FUNCTIONS
		public static string GetPolicyFireProtXml(int intCustoemrId, int intPolId, int intPolVersionId, int intFuelId)
		{
			string strStoredProc = "Proc_GetPolicyFireProtInformation";
			DataSet dsFireProt= new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustoemrId);
				objDataWrapper.AddParameter("@POL_ID",intPolId);
				objDataWrapper.AddParameter("@POL_VERSION_ID",intPolVersionId);
				objDataWrapper.AddParameter("@FUEL_ID",intFuelId);

				dsFireProt= objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsFireProt.Tables[0].Rows.Count != 0)
				{
					return dsFireProt.GetXml();
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
		/// <param name="ObjFireProtInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsPolicyFireProtInfo ObjFireProtInfo)
		{
			string		strStoredProc	=	"Proc_InsertPolicyFireProtClean";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@FUEL_ID",ObjFireProtInfo.FUEL_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjFireProtInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",ObjFireProtInfo.POLICY_ID );
				objDataWrapper.AddParameter("@POL_VERSION_ID",ObjFireProtInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@IS_SMOKE_DETECT0R",ObjFireProtInfo.IS_SMOKE_DETECTOR);
				                             
				objDataWrapper.AddParameter("@IS_PROTECTIVE_MAT_FLOOR",ObjFireProtInfo.IS_PROTECTIVE_MAT_FLOOR);
				objDataWrapper.AddParameter("@IS_PROTECTIVE_MAT_WALLS",ObjFireProtInfo.IS_PROTECTIVE_MAT_WALLS);
				objDataWrapper.AddParameter("@PROT_MAT_SPACED",ObjFireProtInfo.PROT_MAT_SPACED);
				objDataWrapper.AddParameter("@STOVE_SMOKE_PIPE_CLEANED",ObjFireProtInfo.STOVE_SMOKE_PIPE_CLEANED);
				objDataWrapper.AddParameter("@STOVE_CLEANER",ObjFireProtInfo.STOVE_CLEANER);
				objDataWrapper.AddParameter("@REMARKS",ObjFireProtInfo.REMARKS);
				//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@FUEL_ID",ObjFireProtInfo.FUEL_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					ObjFireProtInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/Homeowner/PolicyAddFireProt.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(ObjFireProtInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID  = ObjFireProtInfo.POLICY_ID ;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = ObjFireProtInfo.POLICY_VERSION_ID ;
					objTransactionInfo.CLIENT_ID = ObjFireProtInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	ObjFireProtInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1719", "");//"New fire protection/Cleaning information is added";//Changed by Charles on 21-Oct-09 for Itrack 6599
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

		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldFireProtInfo">Model object having old information</param>
		/// <param name="ObjFireProtInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsPolicyFireProtInfo objOldFireProtInfo,ClsPolicyFireProtInfo ObjFireProtInfo)
		{ 
			string strStoredProc="Proc_UpdatePolicyFireProtClean";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjFireProtInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",ObjFireProtInfo.POLICY_ID );
				objDataWrapper.AddParameter("@POL_VERSION_ID",ObjFireProtInfo.POLICY_VERSION_ID );
				objDataWrapper.AddParameter("@FUEL_ID",ObjFireProtInfo.FUEL_ID);
				objDataWrapper.AddParameter("@IS_SMOKE_DETECTOR",ObjFireProtInfo.IS_SMOKE_DETECTOR);
				objDataWrapper.AddParameter("@IS_PROTECTIVE_MAT_FLOOR",ObjFireProtInfo.IS_PROTECTIVE_MAT_FLOOR);
				objDataWrapper.AddParameter("@IS_PROTECTIVE_MAT_WALLS",ObjFireProtInfo.IS_PROTECTIVE_MAT_WALLS);
				objDataWrapper.AddParameter("@PROT_MAT_SPACED",ObjFireProtInfo.PROT_MAT_SPACED);
				objDataWrapper.AddParameter("@STOVE_SMOKE_PIPE_CLEANED",ObjFireProtInfo.STOVE_SMOKE_PIPE_CLEANED);
				objDataWrapper.AddParameter("@STOVE_CLEANER",ObjFireProtInfo.STOVE_CLEANER);
				objDataWrapper.AddParameter("@REMARKS",ObjFireProtInfo.REMARKS);
				if(TransactionLogRequired) 
				{
					ObjFireProtInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/Homeowner/PolicyAddFireProt.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldFireProtInfo,ObjFireProtInfo);
					if(strTranXML=="<LabelFieldMapping></LabelFieldMapping>" || strTranXML=="") 
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
					
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.POLICY_ID = ObjFireProtInfo.POLICY_ID ;
						objTransactionInfo.POLICY_VER_TRACKING_ID  = ObjFireProtInfo.POLICY_VERSION_ID ;
						objTransactionInfo.CLIENT_ID = ObjFireProtInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	ObjFireProtInfo.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1720", "");// "Fire prot information is modified";
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
