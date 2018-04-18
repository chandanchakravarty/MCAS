using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Application.HomeOwners;
using Cms.DataLayer; 
using Cms.Model.Application;
using Cms.Model.Policy;
using Cms.Model.Policy.HomeOwners;
using Cms.Model.Policy.Homeowners;

namespace Cms.BusinessLayer.BlApplication
{

	public class clsAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS			=	"APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS";
		private const	string		POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS			=	"POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS";
		private			bool		boolTransactionLog;	
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS";
		
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
			
		public clsAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
	

		#region Add (App/Pol)
		public int Add(clsSchItemsCvgsDetailsInfo objSCH_ITEMS_CVGS_DETAILS_Info)
		{
			string		strStoredProc	=	"PROC_INSERTAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objSCH_ITEMS_CVGS_DETAILS_Info.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objSCH_ITEMS_CVGS_DETAILS_Info.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objSCH_ITEMS_CVGS_DETAILS_Info.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ITEM_ID",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_ID);				
				objDataWrapper.AddParameter("@ITEM_DESCRIPTION",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_DESCRIPTION);
				objDataWrapper.AddParameter("@ITEM_SERIAL_NUMBER",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_SERIAL_NUMBER);
				objDataWrapper.AddParameter("@ITEM_INSURING_VALUE",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_INSURING_VALUE);
				if(objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_APPRAISAL_BILL != "0")
					objDataWrapper.AddParameter("@ITEM_APPRAISAL_BILL",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_APPRAISAL_BILL);
				else 
					objDataWrapper.AddParameter("@ITEM_APPRAISAL_BILL",DBNull.Value);
				if(objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_PICTURE_ATTACHED != "0")
					objDataWrapper.AddParameter("@ITEM_PICTURE_ATTACHED",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_PICTURE_ATTACHED);
				else
					objDataWrapper.AddParameter("@ITEM_PICTURE_ATTACHED",DBNull.Value);
				objDataWrapper.AddParameter("@ITEM_NUMBER",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_NUMBER);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ITEM_DETAIL_ID",0,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);

				int ITEM_DETAIL_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (ITEM_DETAIL_ID == -1)
				{
					return -1;
				}
				// -2 : Save Coverages/Scheduled Items first, before saving details
				else if (ITEM_DETAIL_ID == -2)
				{
					return -2;
				}
				else
				{
					objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_DETAIL_ID = ITEM_DETAIL_ID;
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

		public int AddPolicy(clsPolSchItemsCvgsDetailsInfo objSCH_ITEMS_CVGS_DETAILS_Info)
		{
			string		strStoredProc	=	"PROC_INSERTPOL_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objSCH_ITEMS_CVGS_DETAILS_Info.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objSCH_ITEMS_CVGS_DETAILS_Info.POL_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objSCH_ITEMS_CVGS_DETAILS_Info.POL_VERSION_ID);
				objDataWrapper.AddParameter("@ITEM_ID",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_ID);				
				objDataWrapper.AddParameter("@ITEM_DESCRIPTION",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_DESCRIPTION);
				objDataWrapper.AddParameter("@ITEM_SERIAL_NUMBER",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_SERIAL_NUMBER);
				objDataWrapper.AddParameter("@ITEM_INSURING_VALUE",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_INSURING_VALUE);
//				objDataWrapper.AddParameter("@ITEM_APPRAISAL_BILL",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_APPRAISAL_BILL);
				if(objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_APPRAISAL_BILL != "0")
					objDataWrapper.AddParameter("@ITEM_APPRAISAL_BILL",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_APPRAISAL_BILL);
				else 
					objDataWrapper.AddParameter("@ITEM_APPRAISAL_BILL",DBNull.Value);
				if(objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_PICTURE_ATTACHED != "0")
					objDataWrapper.AddParameter("@ITEM_PICTURE_ATTACHED",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_PICTURE_ATTACHED);
				else
					objDataWrapper.AddParameter("@ITEM_PICTURE_ATTACHED",DBNull.Value);
//				objDataWrapper.AddParameter("@ITEM_PICTURE_ATTACHED",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_PICTURE_ATTACHED);
				objDataWrapper.AddParameter("@ITEM_NUMBER",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_NUMBER);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ITEM_DETAIL_ID",0,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				int ITEM_DETAIL_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (ITEM_DETAIL_ID == -1)
				{
					return -1;
				}
				else if (ITEM_DETAIL_ID == -2)
				{
					return -2;
				}
				else
				{
					objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_DETAIL_ID = ITEM_DETAIL_ID;
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

		#region Update (App/Pol)
		public int Update(clsSchItemsCvgsDetailsInfo objSCH_ITEMS_CVGS_DETAILS_Info)
		{
			string		strStoredProc	=	"Proc_UpdateAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL";
			//string strTranXML;
			int returnResult			= 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper	= new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objSCH_ITEMS_CVGS_DETAILS_Info.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objSCH_ITEMS_CVGS_DETAILS_Info.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objSCH_ITEMS_CVGS_DETAILS_Info.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ITEM_ID",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_ID);
				objDataWrapper.AddParameter("@ITEM_DETAIL_ID",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_DETAIL_ID);
				objDataWrapper.AddParameter("@ITEM_NUMBER",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_NUMBER);
				objDataWrapper.AddParameter("@ITEM_DESCRIPTION",objSCH_ITEMS_CVGS_DETAILS_Info. ITEM_DESCRIPTION);
				objDataWrapper.AddParameter("@ITEM_SERIAL_NUMBER",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_SERIAL_NUMBER);
				objDataWrapper.AddParameter("@ITEM_INSURING_VALUE",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_INSURING_VALUE);
				if(objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_APPRAISAL_BILL != "0")
					objDataWrapper.AddParameter("@ITEM_APPRAISAL_BILL",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_APPRAISAL_BILL);
				else 
					objDataWrapper.AddParameter("@ITEM_APPRAISAL_BILL",DBNull.Value);
				if(objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_PICTURE_ATTACHED != "0")
					objDataWrapper.AddParameter("@ITEM_PICTURE_ATTACHED",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_PICTURE_ATTACHED);
				else
					objDataWrapper.AddParameter("@ITEM_PICTURE_ATTACHED",DBNull.Value);
//				objDataWrapper.AddParameter("@ITEM_APPRAISAL_BILL",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_APPRAISAL_BILL);
//				objDataWrapper.AddParameter("@ITEM_PICTURE_ATTACHED",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_PICTURE_ATTACHED);
				
				
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				
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

		public int UpdatePolicy(clsPolSchItemsCvgsDetailsInfo objSCH_ITEMS_CVGS_DETAILS_Info)
		{
			string		strStoredProc	=	"Proc_UpdatePOL_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL";
			//string strTranXML;
			int returnResult			= 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper	= new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objSCH_ITEMS_CVGS_DETAILS_Info.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objSCH_ITEMS_CVGS_DETAILS_Info.POL_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objSCH_ITEMS_CVGS_DETAILS_Info.POL_VERSION_ID);
				objDataWrapper.AddParameter("@ITEM_ID",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_ID);
				objDataWrapper.AddParameter("@ITEM_DETAIL_ID",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_DETAIL_ID);
				objDataWrapper.AddParameter("@ITEM_NUMBER",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_NUMBER);
				objDataWrapper.AddParameter("@ITEM_DESCRIPTION",objSCH_ITEMS_CVGS_DETAILS_Info. ITEM_DESCRIPTION);
				objDataWrapper.AddParameter("@ITEM_SERIAL_NUMBER",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_SERIAL_NUMBER);
				objDataWrapper.AddParameter("@ITEM_INSURING_VALUE",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_INSURING_VALUE);
//				objDataWrapper.AddParameter("@ITEM_APPRAISAL_BILL",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_APPRAISAL_BILL);
//				objDataWrapper.AddParameter("@ITEM_PICTURE_ATTACHED",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_PICTURE_ATTACHED);
				if(objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_APPRAISAL_BILL != "0")
					objDataWrapper.AddParameter("@ITEM_APPRAISAL_BILL",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_APPRAISAL_BILL);
				else 
					objDataWrapper.AddParameter("@ITEM_APPRAISAL_BILL",DBNull.Value);
				if(objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_PICTURE_ATTACHED != "0")
					objDataWrapper.AddParameter("@ITEM_PICTURE_ATTACHED",objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_PICTURE_ATTACHED);
				else
					objDataWrapper.AddParameter("@ITEM_PICTURE_ATTACHED",DBNull.Value);
				
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				
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

		#region Fetch Data(App/Pol)
		public DataSet getDataSCH_ITEMS_CVGS_DETAIL(string CUSTOMER_ID, string APP_ID, string APP_VERSION_ID, string ITEM_ID, string ITEM_DETAIL_ID)
		{
			string strSql = "PROC_GETXMLAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",APP_VERSION_ID);
			objDataWrapper.AddParameter("@ITEM_ID",ITEM_ID);
			objDataWrapper.AddParameter("@ITEM_DETAIL_ID",ITEM_DETAIL_ID);

			return (objDataWrapper.ExecuteDataSet(strSql));
			
		}

		public DataSet getDataSCH_ITEMS_CVGS_DETAIL_Policy(string CUSTOMER_ID, string POL_ID, string POL_VERSION_ID, string ITEM_ID, string ITEM_DETAIL_ID)
		{
			string strSql = "PROC_GETXMLPOL_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objDataWrapper.AddParameter("@POL_ID",POL_ID);
			objDataWrapper.AddParameter("@POL_VERSION_ID",POL_VERSION_ID);
			objDataWrapper.AddParameter("@ITEM_ID",ITEM_ID);
			objDataWrapper.AddParameter("@ITEM_DETAIL_ID",ITEM_DETAIL_ID);

			return (objDataWrapper.ExecuteDataSet(strSql));
			
		}

		#endregion

		#region Get MAX No (App/Pol)
		public string getMaxItemNumber(string CUSTOMER_ID, string APP_ID, string APP_VERSION_ID, string ITEM_ID)
		{
			string strSql = "PROC_GET_MAX_ITEM_NUMBER_IM_COVG";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",APP_VERSION_ID);
			objDataWrapper.AddParameter("@ITEM_ID",ITEM_ID);

			DataSet DS = objDataWrapper.ExecuteDataSet(strSql);
			string strMaxItemNumber = null;
			
			if (DS != null && DS.Tables[0] != null && DS.Tables[0].Rows[0]!= null)
				strMaxItemNumber = DS.Tables[0].Rows[0]["NEW_ITEM_NUMBER"].ToString();
			
			return strMaxItemNumber;
			
		}

		public string getMaxItemNumberPolicy(string CUSTOMER_ID, string POL_ID, string POL_VERSION_ID, string ITEM_ID)
		{
			string strSql = "PROC_GET_MAX_ITEM_NUMBER_IM_COVG_POL";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objDataWrapper.AddParameter("@POL_ID",POL_ID);
			objDataWrapper.AddParameter("@POL_VERSION_ID",POL_VERSION_ID);
			objDataWrapper.AddParameter("@ITEM_ID",ITEM_ID);

			DataSet DS = objDataWrapper.ExecuteDataSet(strSql);
			string strMaxItemNumber = null;
			
			if (DS != null && DS.Tables[0] != null && DS.Tables[0].Rows[0]!= null)
				strMaxItemNumber = DS.Tables[0].Rows[0]["NEW_ITEM_NUMBER"].ToString();
			
			return strMaxItemNumber;
			
		}

		#endregion

		#region Delete Method (App/Pol)
		public int Delete(clsSchItemsCvgsDetailsInfo objSchItemsCvgsDetailsInfo)
		{
			int returnResult = 0;
			string		strStoredProc	=	"Proc_DelAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objSchItemsCvgsDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objSchItemsCvgsDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objSchItemsCvgsDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ITEM_ID",objSchItemsCvgsDetailsInfo.ITEM_ID);				
				objDataWrapper.AddParameter("@ITEM_DETAIL_ID",objSchItemsCvgsDetailsInfo.ITEM_DETAIL_ID);			
				if(TransactionLogRequired) 
				{			
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;   //verify
					objTransactionInfo.RECORDED_BY		=	objSchItemsCvgsDetailsInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objSchItemsCvgsDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objSchItemsCvgsDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objSchItemsCvgsDetailsInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Item has been Deleted";
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

		public int DeletePolicy(clsPolSchItemsCvgsDetailsInfo objSchItemsCvgsDetailsInfo)
		{
			int returnResult = 0;
			string		strStoredProc	=	"Proc_DelPol_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objSchItemsCvgsDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objSchItemsCvgsDetailsInfo.POL_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objSchItemsCvgsDetailsInfo.POL_VERSION_ID);
				objDataWrapper.AddParameter("@ITEM_ID",objSchItemsCvgsDetailsInfo.ITEM_ID);				
				objDataWrapper.AddParameter("@ITEM_DETAIL_ID",objSchItemsCvgsDetailsInfo.ITEM_DETAIL_ID);			
				if(TransactionLogRequired) 
				{			
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID			=	3;   //verify
					objTransactionInfo.RECORDED_BY				=	objSchItemsCvgsDetailsInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID				=	objSchItemsCvgsDetailsInfo.POL_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objSchItemsCvgsDetailsInfo.POL_VERSION_ID;
					objTransactionInfo.CLIENT_ID				=	objSchItemsCvgsDetailsInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC				=	"Item has been Deleted";
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

		#region Activate/Deactivate
		public void ActivateDeactivate(clsSchItemsCvgsDetailsInfo objSchItemsCvgsDetailsInfo,string strStatus)
		{
			string	strStoredProc =	"Proc_ActivateDeactivateAppScheduledItemsCvgs";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			try
			{
				objWrapper.AddParameter("@CUSTOMER_ID",objSchItemsCvgsDetailsInfo.CUSTOMER_ID);
				objWrapper.AddParameter("@APP_ID",objSchItemsCvgsDetailsInfo.APP_ID);
				objWrapper.AddParameter("@APP_VERSION_ID",objSchItemsCvgsDetailsInfo.APP_VERSION_ID);
				objWrapper.AddParameter("@ITEM_ID",objSchItemsCvgsDetailsInfo.ITEM_ID);				
				objWrapper.AddParameter("@ITEM_DETAIL_ID",objSchItemsCvgsDetailsInfo.ITEM_DETAIL_ID);			
				objWrapper.AddParameter("@IS_ACTIVE",strStatus);
				SqlParameter paramRetVal = (SqlParameter)objWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objSchItemsCvgsDetailsInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("application/Aspx/HomeOwners/AddInlandMarineBOTTOM.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objSchItemsCvgsDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objSchItemsCvgsDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objSchItemsCvgsDetailsInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objSchItemsCvgsDetailsInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"Item is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	"Item is Deactivated";
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
		
		public void ActivateDeactivatePolicy(clsPolSchItemsCvgsDetailsInfo objSchItemsCvgsDetailsInfo,string strStatus)
		{
			string	strStoredProc =	"Proc_ActivateDeactivatePolScheduledItemsCvgs";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			try
			{
				objWrapper.AddParameter("@CUSTOMER_ID",objSchItemsCvgsDetailsInfo.CUSTOMER_ID);
				objWrapper.AddParameter("@POL_ID",objSchItemsCvgsDetailsInfo.POL_ID);
				objWrapper.AddParameter("@POL_VERSION_ID",objSchItemsCvgsDetailsInfo.POL_VERSION_ID);
				objWrapper.AddParameter("@ITEM_ID",objSchItemsCvgsDetailsInfo.ITEM_ID);				
				objWrapper.AddParameter("@ITEM_DETAIL_ID",objSchItemsCvgsDetailsInfo.ITEM_DETAIL_ID);			
				objWrapper.AddParameter("@IS_ACTIVE",strStatus);
				SqlParameter paramRetVal = (SqlParameter)objWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objSchItemsCvgsDetailsInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("Policies/Aspx/Homeowner/AddInlandMarineBottom.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID			=	1;
					objTransactionInfo.POLICY_ID				=	objSchItemsCvgsDetailsInfo.POL_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objSchItemsCvgsDetailsInfo.POL_VERSION_ID;
					objTransactionInfo.CLIENT_ID				=	objSchItemsCvgsDetailsInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY				=	objSchItemsCvgsDetailsInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC			=	"Item is Activated";
					else
						objTransactionInfo.TRANS_DESC			=	"Item is Deactivated";
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
	}
}
