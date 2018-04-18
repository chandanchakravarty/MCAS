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
	/// Summary description for ClsAddReinsuranceAggStopLoss.
	/// </summary>
	public class ClsAddReinsuranceAggStopLoss : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		# region D E C L A R A T I O N 

		private	bool boolTransactionLog;

		public ClsAddReinsuranceAggStopLoss()
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
		public int Add(ClsAddReinsuranceAggStopLossInfo objAddReinsuranceAggStopLossInfo)
		{
			string		strStoredProc	=	"Proc_InsertAggregateStopLoss";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CONTRACT_ID"					,objAddReinsuranceAggStopLossInfo.CONTRACT_ID);
				objDataWrapper.AddParameter("@REINSURANCE_COMPANY"			,objAddReinsuranceAggStopLossInfo.REINSURANCE_COMPANY);
				objDataWrapper.AddParameter("@REINSURANCE_ACC_NUMBER"		,objAddReinsuranceAggStopLossInfo.REINSURANCE_ACC_NUMBER);
				objDataWrapper.AddParameter("@EFFECTIVE_DATE"				,objAddReinsuranceAggStopLossInfo.EFFECTIVE_DATE);
				objDataWrapper.AddParameter("@EXPIRATION_DATE"				,objAddReinsuranceAggStopLossInfo.EXPIRATION_DATE);
				objDataWrapper.AddParameter("@LINE_OF_BUSINESS"				,objAddReinsuranceAggStopLossInfo.LINE_OF_BUSINESS);
				objDataWrapper.AddParameter("@COVERAGE_CODE"				,objAddReinsuranceAggStopLossInfo.COVERAGE_CODE);
				objDataWrapper.AddParameter("@CLASS_CODE"					,objAddReinsuranceAggStopLossInfo.CLASS_CODE);
				objDataWrapper.AddParameter("@PERIL"						,objAddReinsuranceAggStopLossInfo.PERIL);
				objDataWrapper.AddParameter("@STATED_AMOUNT"				,objAddReinsuranceAggStopLossInfo.STATED_AMOUNT);
				objDataWrapper.AddParameter("@SPECIFIC_LOSS_RATIO"			,objAddReinsuranceAggStopLossInfo.SPECIFIC_LOSS_RATIO);
				objDataWrapper.AddParameter("@IS_ACTIVE"					,objAddReinsuranceAggStopLossInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY"					,objAddReinsuranceAggStopLossInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME"				,objAddReinsuranceAggStopLossInfo.CREATED_DATETIME);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@AGGREGATE_ID",objAddReinsuranceAggStopLossInfo.AGGREGATE_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objAddReinsuranceAggStopLossInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsuranceAggStopLoss.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objAddReinsuranceAggStopLossInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objAddReinsuranceAggStopLossInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Aggregate Stop Loss Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int  intAggregateID= int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if ( intAggregateID == -1)
				{
					return -1;
				}
				else
				{
					objAddReinsuranceAggStopLossInfo.AGGREGATE_ID= intAggregateID;
					return intAggregateID;
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

		# endregion

		#region U P D A T E   
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldReinsuranceInfo">Model object having old information</param>
		/// <param name="objReinsuranceInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsAddReinsuranceAggStopLossInfo objOldAddReinsuranceAggStopLossInfo, ClsAddReinsuranceAggStopLossInfo objAddReinsuranceAggStopLossInfo)
		{
			string	strStoredProc	=	"Proc_UpdateAggregateStopLoss";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CONTRACT_ID"					,objAddReinsuranceAggStopLossInfo.CONTRACT_ID);
				objDataWrapper.AddParameter("@REINSURANCE_COMPANY"			,objAddReinsuranceAggStopLossInfo.REINSURANCE_COMPANY);
				objDataWrapper.AddParameter("@REINSURANCE_ACC_NUMBER"		,objAddReinsuranceAggStopLossInfo.REINSURANCE_ACC_NUMBER);
				objDataWrapper.AddParameter("@EFFECTIVE_DATE"				,objAddReinsuranceAggStopLossInfo.EFFECTIVE_DATE);
				objDataWrapper.AddParameter("@EXPIRATION_DATE"				,objAddReinsuranceAggStopLossInfo.EXPIRATION_DATE);
				objDataWrapper.AddParameter("@LINE_OF_BUSINESS"				,objAddReinsuranceAggStopLossInfo.LINE_OF_BUSINESS);
				objDataWrapper.AddParameter("@COVERAGE_CODE"				,objAddReinsuranceAggStopLossInfo.COVERAGE_CODE);
				objDataWrapper.AddParameter("@CLASS_CODE"					,objAddReinsuranceAggStopLossInfo.CLASS_CODE);
				objDataWrapper.AddParameter("@PERIL"						,objAddReinsuranceAggStopLossInfo.PERIL);
				objDataWrapper.AddParameter("@STATED_AMOUNT"				,objAddReinsuranceAggStopLossInfo.STATED_AMOUNT);
				objDataWrapper.AddParameter("@SPECIFIC_LOSS_RATIO"			,objAddReinsuranceAggStopLossInfo.SPECIFIC_LOSS_RATIO);
				objDataWrapper.AddParameter("@MODIFIED_BY"					,objAddReinsuranceAggStopLossInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME"		,objAddReinsuranceAggStopLossInfo.LAST_UPDATED_DATETIME);

				if(base.TransactionLogRequired) 
				{
					objAddReinsuranceAggStopLossInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsuranceAggStopLoss.aspx.resx");
					objBuilder.GetUpdateSQL(objOldAddReinsuranceAggStopLossInfo,objAddReinsuranceAggStopLossInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objAddReinsuranceAggStopLossInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Aggregate Stop Loss Has Been Updated";
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

		# endregion 

		#region G E T  D A T A   F O R   E D I T   M O D E

		public DataSet GetDataForPageControls(string CONTRACT_ID)
		{
			string strSql = "Proc_GetXMLMNT_REINSURANCE_AGGREGATE_STOP_LOSS";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CONTRACT_ID",CONTRACT_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		public string GetContractNameFromId(int CONTRACT_ID)
		{
			string strStroredProc  =  "PROC_GETCONTRACT_NAMEFROM_ID";
			DataSet oDs;
			try
			{
				Object[] objParam	=	new object[1];
				objParam[0]			=	CONTRACT_ID;
				oDs					=	Cms.DataLayer.DataWrapper.ExecuteDataset(ConnStr,strStroredProc,objParam);
				if(oDs.Tables[0].Rows.Count>0)
				{
					return oDs.Tables[0].Rows[0]["CONTRACT_NAME"].ToString();
				}
				else
                    return "";
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}

		#endregion
	}
}
