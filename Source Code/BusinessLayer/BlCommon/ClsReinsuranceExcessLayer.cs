/* ***************************************************************************************
   Author		: Deepak Batra 
   Creation Date: 05/01/2006
   Last Updated : 
   Reviewed By	: 
   Purpose		: Class file for the Excess Layer
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Configuration;
using Cms.DataLayer;
using System.Web.UI.WebControls;
using Cms.Model.Maintenance.Reinsurance;


namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsReinsuranceExcessLayer.
	/// </summary>
	public class ClsReinsuranceExcessLayer : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		# region D E C L A R A T I O N 

		private const string MNT_REINSURANCE_EXCESS	 =	"MNT_REINSURANCE_EXCESS";
		private	bool boolTransactionLog;

		public ClsReinsuranceExcessLayer()
		{
			boolTransactionLog	= base.TransactionLogRequired;
		}

		# endregion 

		#region A D D  (I N S E R T)
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objReinsuranceInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddExcess(ClsReinsuranceExcessLayerInfo objReinsuranceExcessLayerInfo)
		{
			string		strStoredProc	=	"Proc_InsertExcessLayer";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CONTRACT_ID"					,objReinsuranceExcessLayerInfo.CONTRACT_ID);
				objDataWrapper.AddParameter("@LAYER_AMOUNT"					,objReinsuranceExcessLayerInfo.LAYER_AMOUNT);
				objDataWrapper.AddParameter("@UNDERLYING_AMOUNT"			,objReinsuranceExcessLayerInfo.UNDERLYING_AMOUNT);
				objDataWrapper.AddParameter("@LAYER_PREMIUM"				,objReinsuranceExcessLayerInfo.LAYER_PREMIUM);
				objDataWrapper.AddParameter("@CEDING_COMMISSION"			,objReinsuranceExcessLayerInfo.CEDING_COMMISSION);
				objDataWrapper.AddParameter("@AC_PREMIUM"					,objReinsuranceExcessLayerInfo.AC_PREMIUM);
				objDataWrapper.AddParameter("@IS_ACTIVE"					,objReinsuranceExcessLayerInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY"					,objReinsuranceExcessLayerInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME"				,objReinsuranceExcessLayerInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@PARTICIPATION_AMOUNT"			,objReinsuranceExcessLayerInfo.PARTICIPATION_AMOUNT);
				objDataWrapper.AddParameter("@PRORATA_AMOUNT"				,objReinsuranceExcessLayerInfo.PRORATA_AMOUNT);
				objDataWrapper.AddParameter("@LAYER_TYPE"					,objReinsuranceExcessLayerInfo.LAYER_TYPE);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@EXCESS_ID",objReinsuranceExcessLayerInfo.EXCESS_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objReinsuranceExcessLayerInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsuranceExcessLayer.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceExcessLayerInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceExcessLayerInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int  intExcessID= int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if ( intExcessID == -1)
				{
					return -1;
				}
				else
				{
					objReinsuranceExcessLayerInfo.EXCESS_ID= intExcessID;
					return intExcessID;
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

		public int AddProrata(ClsReinsuranceExcessLayerInfo objReinsuranceExcessLayerInfo)
		{
			string		strStoredProc	=	"Proc_InsertExcessLayer";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CONTRACT_ID"					,objReinsuranceExcessLayerInfo.CONTRACT_ID);
				objDataWrapper.AddParameter("@LAYER_AMOUNT"					,objReinsuranceExcessLayerInfo.LAYER_AMOUNT);
				objDataWrapper.AddParameter("@UNDERLYING_AMOUNT"			,objReinsuranceExcessLayerInfo.UNDERLYING_AMOUNT);
				objDataWrapper.AddParameter("@LAYER_PREMIUM"				,objReinsuranceExcessLayerInfo.LAYER_PREMIUM);
				objDataWrapper.AddParameter("@CEDING_COMMISSION"			,objReinsuranceExcessLayerInfo.CEDING_COMMISSION);
				objDataWrapper.AddParameter("@AC_PREMIUM"					,objReinsuranceExcessLayerInfo.AC_PREMIUM);
				objDataWrapper.AddParameter("@IS_ACTIVE"					,objReinsuranceExcessLayerInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY"					,objReinsuranceExcessLayerInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME"				,objReinsuranceExcessLayerInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@PARTICIPATION_AMOUNT"			,objReinsuranceExcessLayerInfo.PARTICIPATION_AMOUNT);
				objDataWrapper.AddParameter("@PRORATA_AMOUNT"				,objReinsuranceExcessLayerInfo.PRORATA_AMOUNT);
				objDataWrapper.AddParameter("@LAYER_TYPE"					,objReinsuranceExcessLayerInfo.LAYER_TYPE);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@EXCESS_ID",objReinsuranceExcessLayerInfo.EXCESS_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objReinsuranceExcessLayerInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsuranceQuotaShare.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceExcessLayerInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceExcessLayerInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Excess Layer Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int  intExcessID= int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if ( intExcessID == -1)
				{
					return -1;
				}
				else
				{
					objReinsuranceExcessLayerInfo.EXCESS_ID= intExcessID;
					return intExcessID;
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

		#region U P D A T E   
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldReinsuranceInfo">Model object having old information</param>
		/// <param name="objReinsuranceInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdateExcess(ClsReinsuranceExcessLayerInfo objOldReinsuranceExcessLayerInfo, ClsReinsuranceExcessLayerInfo objReinsuranceExcessLayerInfo)
		{
			string	strStoredProc	=	"Proc_UpdateExcessLayer";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@EXCESS_ID"					,objReinsuranceExcessLayerInfo.EXCESS_ID);
				objDataWrapper.AddParameter("@CONTRACT_ID"					,objReinsuranceExcessLayerInfo.CONTRACT_ID);
				objDataWrapper.AddParameter("@LAYER_AMOUNT"					,objReinsuranceExcessLayerInfo.LAYER_AMOUNT);
				objDataWrapper.AddParameter("@UNDERLYING_AMOUNT"			,objReinsuranceExcessLayerInfo.UNDERLYING_AMOUNT);
				objDataWrapper.AddParameter("@LAYER_PREMIUM"				,objReinsuranceExcessLayerInfo.LAYER_PREMIUM);
				objDataWrapper.AddParameter("@CEDING_COMMISSION"			,objReinsuranceExcessLayerInfo.CEDING_COMMISSION);
				objDataWrapper.AddParameter("@AC_PREMIUM"					,objReinsuranceExcessLayerInfo.AC_PREMIUM);
				objDataWrapper.AddParameter("@MODIFIED_BY"					,objReinsuranceExcessLayerInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME"		,objReinsuranceExcessLayerInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@PARTICIPATION_AMOUNT"			,objReinsuranceExcessLayerInfo.PARTICIPATION_AMOUNT);
				objDataWrapper.AddParameter("@PRORATA_AMOUNT"				,objReinsuranceExcessLayerInfo.PRORATA_AMOUNT);
				objDataWrapper.AddParameter("@LAYER_TYPE"					,objReinsuranceExcessLayerInfo.LAYER_TYPE);
				
				if(base.TransactionLogRequired) 
				{
					objReinsuranceExcessLayerInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsuranceExcessLayer.aspx.resx");
					objBuilder.GetUpdateSQL(objOldReinsuranceExcessLayerInfo,objReinsuranceExcessLayerInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceExcessLayerInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Excess Layer Has Been Updated";
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

		public int UpdateProrata(ClsReinsuranceExcessLayerInfo objOldReinsuranceExcessLayerInfo, ClsReinsuranceExcessLayerInfo objReinsuranceExcessLayerInfo)
		{
			string	strStoredProc	=	"Proc_UpdateExcessLayer";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@EXCESS_ID"					,objReinsuranceExcessLayerInfo.EXCESS_ID);
				objDataWrapper.AddParameter("@CONTRACT_ID"					,objReinsuranceExcessLayerInfo.CONTRACT_ID);
				objDataWrapper.AddParameter("@LAYER_AMOUNT"					,objReinsuranceExcessLayerInfo.LAYER_AMOUNT);
				objDataWrapper.AddParameter("@UNDERLYING_AMOUNT"			,objReinsuranceExcessLayerInfo.UNDERLYING_AMOUNT);
				objDataWrapper.AddParameter("@LAYER_PREMIUM"				,objReinsuranceExcessLayerInfo.LAYER_PREMIUM);
				objDataWrapper.AddParameter("@CEDING_COMMISSION"			,objReinsuranceExcessLayerInfo.CEDING_COMMISSION);
				objDataWrapper.AddParameter("@AC_PREMIUM"					,objReinsuranceExcessLayerInfo.AC_PREMIUM);
				objDataWrapper.AddParameter("@MODIFIED_BY"					,objReinsuranceExcessLayerInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME"		,objReinsuranceExcessLayerInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@PARTICIPATION_AMOUNT"			,objReinsuranceExcessLayerInfo.PARTICIPATION_AMOUNT);
				objDataWrapper.AddParameter("@PRORATA_AMOUNT"				,objReinsuranceExcessLayerInfo.PRORATA_AMOUNT);
				objDataWrapper.AddParameter("@LAYER_TYPE"					,objReinsuranceExcessLayerInfo.LAYER_TYPE);
				
				if(base.TransactionLogRequired) 
				{
					objReinsuranceExcessLayerInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsuranceQuotaShare.aspx.resx");
					objBuilder.GetUpdateSQL(objOldReinsuranceExcessLayerInfo,objReinsuranceExcessLayerInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceExcessLayerInfo.MODIFIED_BY;
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

		#region G E T  D A T A   F O R   E D I T   M O D E

		public DataSet GetDataForPageControls(string EXCESS_ID, string CONTRACT_ID, string LAYER_TYPE)
		{
			string strSql = "Proc_GetXMLMNT_REINSURANCE_EXCESSLAYER";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@EXCESS_ID",EXCESS_ID);
			objDataWrapper.AddParameter("@CONTRACT_ID",CONTRACT_ID);
			objDataWrapper.AddParameter("@LAYER_TYPE",LAYER_TYPE);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		#endregion
	}
}
