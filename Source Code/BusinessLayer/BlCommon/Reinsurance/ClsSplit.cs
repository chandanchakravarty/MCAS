/* ***************************************************************************************
   Author		: Harmanjeet Singh 
   Creation Date: April 27, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: Class file for Special Acceptance Amount 
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

namespace Cms.BusinessLayer.BlCommon.Reinsurance
{
	/// <summary>
	/// Summary description for ClsSplit.
	/// </summary>
	public class ClsSplit:Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		# region D E C L A R A T I O N 

		private const string MNT_REIN_SPLIT="MNT_REIN_SPLIT";
		private	bool boolTransactionLog;

		public ClsSplit()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= "Proc_MNT_REIN_DEACTIVATE_ACTIVATE_SPLIT";
		}

		# endregion 

		#region A D D  (I N S E R T)
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objReinsuranceInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		
		public int Add(ClsSplitInfo objSplitInfo,string CustomInfo)
		{
			string		strStoredProc	=	"Proc_MNT_REIN_INSERT_SPLIT";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				if(objSplitInfo.REIN_EFFECTIVE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@REIN_EFFECTIVE_DATE",objSplitInfo.REIN_EFFECTIVE_DATE);
				else
					objDataWrapper.AddParameter("@REIN_EFFECTIVE_DATE",System.DBNull.Value);
				objDataWrapper.AddParameter("@REIN_LINE_OF_BUSINESS",objSplitInfo.REIN_LINE_OF_BUSINESS);
				objDataWrapper.AddParameter("@REIN_STATE",objSplitInfo.REIN_STATE);
				objDataWrapper.AddParameter("@REIN_COVERAGE",objSplitInfo.REIN_COVERAGE);
				objDataWrapper.AddParameter("@REIN_IST_SPLIT",objSplitInfo.REIN_IST_SPLIT);
				objDataWrapper.AddParameter("@REIN_IST_SPLIT_COVERAGE",objSplitInfo.REIN_IST_SPLIT_COVERAGE);
				objDataWrapper.AddParameter("@REIN_2ND_SPLIT",objSplitInfo.REIN_2ND_SPLIT);
				objDataWrapper.AddParameter("@REIN_2ND_SPLIT_COVERAGE",objSplitInfo.REIN_2ND_SPLIT_COVERAGE);
				objDataWrapper.AddParameter("@POLICY_TYPE",objSplitInfo.POLICY_TYPE);
				objDataWrapper.AddParameter("@CREATED_BY",objSplitInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objSplitInfo.CREATED_DATETIME);				
			
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@REIN_SPLIT_DEDUCTION_ID",objSplitInfo.REIN_SPLIT_DEDUCTION_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objSplitInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/Reinsurance/MasterSetup/Split.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objSplitInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objSplitInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Split Deduction Has Been Added";
					objTransactionInfo.CUSTOM_INFO		=    CustomInfo;
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if(returnResult ==-2)
				{
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
				}
				int  intREIN_SPLIT_DEDUCTION_ID= int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (intREIN_SPLIT_DEDUCTION_ID == -1)
				{
					return -1;
				}
				else
				{
					objSplitInfo.REIN_SPLIT_DEDUCTION_ID= intREIN_SPLIT_DEDUCTION_ID;
					return intREIN_SPLIT_DEDUCTION_ID;
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
		public int Update(ClsSplitInfo objOldSplitInfo,ClsSplitInfo objSplitInfo)
		{
			string	strStoredProc	=	"Proc_MNT_REIN_UPDATE_SPLIT";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@REIN_SPLIT_DEDUCTION_ID",objSplitInfo.REIN_SPLIT_DEDUCTION_ID);
				//objDataWrapper.AddParameter("@REIN_EFFECTIVE_DATE",objSplitInfo.REIN_EFFECTIVE_DATE);
				if(objSplitInfo.REIN_EFFECTIVE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@REIN_EFFECTIVE_DATE",objSplitInfo.REIN_EFFECTIVE_DATE);
				else
					objDataWrapper.AddParameter("@REIN_EFFECTIVE_DATE",System.DBNull.Value);
				objDataWrapper.AddParameter("@REIN_LINE_OF_BUSINESS",objSplitInfo.REIN_LINE_OF_BUSINESS);
				objDataWrapper.AddParameter("@REIN_STATE",objSplitInfo.REIN_STATE);
				objDataWrapper.AddParameter("@REIN_COVERAGE",objSplitInfo.REIN_COVERAGE);
				objDataWrapper.AddParameter("@REIN_IST_SPLIT",objSplitInfo.REIN_IST_SPLIT);
				objDataWrapper.AddParameter("@REIN_IST_SPLIT_COVERAGE",objSplitInfo.REIN_IST_SPLIT_COVERAGE);
				objDataWrapper.AddParameter("@REIN_2ND_SPLIT",objSplitInfo.REIN_2ND_SPLIT);
				objDataWrapper.AddParameter("@REIN_2ND_SPLIT_COVERAGE",objSplitInfo.REIN_2ND_SPLIT_COVERAGE);
				objDataWrapper.AddParameter("@POLICY_TYPE",objSplitInfo.POLICY_TYPE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objSplitInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objSplitInfo.LAST_UPDATED_DATETIME);				
				
				SqlParameter paramRetVal = (SqlParameter) objDataWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);

				
				if(base.TransactionLogRequired) 
				{
					objSplitInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/Reinsurance/MasterSetup/Split.aspx.resx");
					objBuilder.GetUpdateSQL(objOldSplitInfo, objSplitInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objSplitInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Split Deduction Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if(Convert.ToInt32(paramRetVal.Value) ==-2)
				{
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return -2;
				}
				else
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
				}
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

		public DataSet GetDataForPageControls(string REIN_SPLIT_DEDUCTION_ID)
		{
			string strSql = "Proc_MNT_REIN_GETXML_SPLIT";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@REIN_SPLIT_DEDUCTION_ID",REIN_SPLIT_DEDUCTION_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		#endregion
				
		#region DEACTIVATE OR ACTIVATE CONTRACT TYPE

		public int GetDeactivateActivate(string REIN_SPLIT_DEDUCTION_ID,string Status_Check)
		{
			string strSql = "Proc_MNT_REIN_DEACTIVATE_ACTIVATE_SPLIT";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@REIN_SPLIT_DEDUCTION_ID",REIN_SPLIT_DEDUCTION_ID);
			objDataWrapper.AddParameter("@STATUS_CHECK",Status_Check,System.Data.SqlDbType.NVarChar);

			//DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			int returnResult	= objDataWrapper.ExecuteNonQuery(strSql);
			return returnResult;
		}

		#endregion

		#region G E T  COVERAGE DESCRIPTION

		public DataSet GetCoverageDesc(string REIN_STATE,string REIN_LINE_OF_BUSINESS)
		{
			
			string strSql = "Proc_MNT_REIN_GETCOVERAGEDESC";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@STATE_ID",REIN_STATE);
			objDataWrapper.AddParameter("@LOB_ID",REIN_LINE_OF_BUSINESS);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		#endregion

		#region Populate  COVERAGE DESCRIPTION

		public DataSet PopulateCoverageDesc(string REIN_SPLIT_DEDUCTION_ID)
		{
			
			String covNumber="";
			int covId = 0;
			DataSet objDataSet1=null;
			string strSql = "Proc_MNT_REIN_SPLIT_COVERAGE";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@REIN_SPLIT_DEDUCTION_ID",REIN_SPLIT_DEDUCTION_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);

			DataTable tbl=objDataSet.Tables[0];
			if(objDataSet.Tables[0].Rows.Count >0)
			{
				DataRow oDr  = objDataSet.Tables[0].Rows[0];
				covNumber=oDr["REIN_IST_SPLIT_COVERAGE"].ToString();
				covId = int.Parse(oDr["REIN_COVERAGE"].ToString());
				string strSql1 = "Proc_MNT_REIN_SPLIT_GETCOVERAGE";
				DataWrapper objDataWrapper1 = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper1.AddParameter("@COV_CODE",covNumber);
				objDataWrapper1.AddParameter("@COV_ID",covId);
				objDataSet1 = objDataWrapper1.ExecuteDataSet(strSql1);
			
				
			}
			return objDataSet1;
		}

		#endregion
	}
}




