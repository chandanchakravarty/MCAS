/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date				: -	5/24/2005 3:22:30 PM
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
using Cms.Model.Application;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsUmbrellaPolicyOthers.
	/// </summary>
	public class ClsUmbrellaPolicyOthers : Cms.BusinessLayer.BlApplication.clsapplication ,IDisposable
	{
		private const string APP_UMBRELLA_POL_INFO_OTHER="APP_UMBRELLA_POL_INFO_OTHER";
		private bool boolTransactionLog;
		private bool boolTransactionRequired			= true;
		//private const string ACTIVATE_DEACTIVATE_PROC="Proc_ActivateDeactivateAPP_UMBRELLA_POL_INFO_OTHER";
		//private int _APP_VERSION_ID;
			

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
		
		public ClsUmbrellaPolicyOthers()
		{
			boolTransactionLog	= base.TransactionLogRequired;
		//	base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;			
		}
		
		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="ObjUmbrellaPolicyOthersInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsUmbrellaPolicyOthersInfo ObjUmbrellaPolicyOthersInfo)
		{
			string strStoredProc="Proc_InsertAPP_UMBRELLA_POL_INFO_OTHER";
			DateTime RecordDate	=DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjUmbrellaPolicyOthersInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",ObjUmbrellaPolicyOthersInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",ObjUmbrellaPolicyOthersInfo.APP_VERSION_ID);
				if(ObjUmbrellaPolicyOthersInfo.COMBINED_SINGLE_LIMIT==0)
				{
					objDataWrapper.AddParameter("@COMBINED_SINGLE_LIMIT",null);
				}
				else
				objDataWrapper.AddParameter("@COMBINED_SINGLE_LIMIT",ObjUmbrellaPolicyOthersInfo.COMBINED_SINGLE_LIMIT);
				if(ObjUmbrellaPolicyOthersInfo.BODILY_INJURY==0)
				{
					objDataWrapper.AddParameter("@BODILY_INJURY",null);
				}
				else
				objDataWrapper.AddParameter("@BODILY_INJURY",ObjUmbrellaPolicyOthersInfo.BODILY_INJURY);

				if(ObjUmbrellaPolicyOthersInfo.PROPERTY_DAMAGE==0)
				{
					objDataWrapper.AddParameter("@PROPERTY_DAMAGE",null);
				}
				else
				objDataWrapper.AddParameter("@PROPERTY_DAMAGE",ObjUmbrellaPolicyOthersInfo.PROPERTY_DAMAGE);
				objDataWrapper.AddParameter("@IS_ACTIVE",ObjUmbrellaPolicyOthersInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",ObjUmbrellaPolicyOthersInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",ObjUmbrellaPolicyOthersInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				//objDataWrapper.AddParameter("@MODIFIED_BY",ObjUmbrellaPolicyOthersInfo.MODIFIED_BY);
				//objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjUmbrellaPolicyOthersInfo.LAST_UPDATED_DATETIME);
				//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@APP_VERSION_ID",ObjUmbrellaPolicyOthersInfo.APP_VERSION_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					ObjUmbrellaPolicyOthersInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\AddUmbrellaPolicyOthers.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(ObjUmbrellaPolicyOthersInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = ObjUmbrellaPolicyOthersInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = ObjUmbrellaPolicyOthersInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = ObjUmbrellaPolicyOthersInfo.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	ObjUmbrellaPolicyOthersInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New umbrella policy is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				//int APP_VERSION_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				if (APP_VERSION_ID == -1)
//				{
//					return -1;
//				}
//				else
//				{
//					ObjUmbrellaPolicyOthersInfo.APP_VERSION_ID = APP_VERSION_ID;
//					return returnResult;
//				}
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
		/// <param name="objOldUmbrellaPolicyOthersInfo">Model object having old information</param>
		/// <param name="ObjUmbrellaPolicyOthersInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsUmbrellaPolicyOthersInfo objOldUmbrellaPolicyOthersInfo,ClsUmbrellaPolicyOthersInfo ObjUmbrellaPolicyOthersInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UpdateAPP_UMBRELLA_POL_INFO_OTHER";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjUmbrellaPolicyOthersInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",ObjUmbrellaPolicyOthersInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",ObjUmbrellaPolicyOthersInfo.APP_VERSION_ID);
				if(ObjUmbrellaPolicyOthersInfo.COMBINED_SINGLE_LIMIT==0)
				{
					objDataWrapper.AddParameter("@COMBINED_SINGLE_LIMIT",null);
				}
				else
				objDataWrapper.AddParameter("@COMBINED_SINGLE_LIMIT",ObjUmbrellaPolicyOthersInfo.COMBINED_SINGLE_LIMIT);

				if(ObjUmbrellaPolicyOthersInfo.BODILY_INJURY==0)
				{
					objDataWrapper.AddParameter("@BODILY_INJURY",null);
				}
				else
				objDataWrapper.AddParameter("@BODILY_INJURY",ObjUmbrellaPolicyOthersInfo.BODILY_INJURY);
				if(ObjUmbrellaPolicyOthersInfo.PROPERTY_DAMAGE==0)
				{
					objDataWrapper.AddParameter("@PROPERTY_DAMAGE",null);
				}
				else

				objDataWrapper.AddParameter("@PROPERTY_DAMAGE",ObjUmbrellaPolicyOthersInfo.PROPERTY_DAMAGE);
				objDataWrapper.AddParameter("@IS_ACTIVE",ObjUmbrellaPolicyOthersInfo.IS_ACTIVE);
				//objDataWrapper.AddParameter("@CREATED_BY",ObjUmbrellaPolicyOthersInfo.CREATED_BY);
				//objDataWrapper.AddParameter("@CREATED_DATETIME",ObjUmbrellaPolicyOthersInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",ObjUmbrellaPolicyOthersInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjUmbrellaPolicyOthersInfo.LAST_UPDATED_DATETIME);
				if(TransactionRequired) 
				{
					ObjUmbrellaPolicyOthersInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\AddUmbrellaPolicyOthers.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldUmbrellaPolicyOthersInfo,ObjUmbrellaPolicyOthersInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID = ObjUmbrellaPolicyOthersInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = ObjUmbrellaPolicyOthersInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = ObjUmbrellaPolicyOthersInfo.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	ObjUmbrellaPolicyOthersInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Umbrella policy is modified";
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
	
		//GetUmbrellaPolicyOthersXml
		#region GetUmbrellaPolicyOthersXml
		public static string GetUmbrellaPolicyOthersXml(int intCustomer_ID,int intApp_ID,int intApp_Version_ID)
		{
			
			string strStoredProc = "Proc_GetUmbrellaPolicyOthersXml";
			DataSet dsUmbrella = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomer_ID);
				objDataWrapper.AddParameter("@APP_ID",intApp_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intApp_Version_ID);
				dsUmbrella = objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsUmbrella.Tables[0].Rows.Count != 0)
				{
					return dsUmbrella.GetXml();
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




