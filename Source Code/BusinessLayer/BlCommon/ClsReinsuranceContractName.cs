/* ***************************************************************************************
   Author		: Deepak Batra 
   Creation Date: 05/01/2006
   Last Updated : 
   Reviewed By	: 
   Purpose		: Class file for Contract Names 
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
	/// Summary description for ClsReinsuranceContractName.
	/// </summary>
	public class ClsReinsuranceContractName : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		# region D E C L A R A T I O N 

		private const string MNT_CONTRACT_NAME	 =	"MNT_CONTRACT_NAME";
		private	bool boolTransactionLog;

		public ClsReinsuranceContractName()
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
		public int Add(ClsReinsuranceContractNameInfo objReinsuranceContractNameInfo)
		{
			string		strStoredProc	=	"Proc_InsertContractName";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CONTRACT_NAME"				,objReinsuranceContractNameInfo.CONTRACT_NAME);
				objDataWrapper.AddParameter("@CONTRACT_DESCRIPTION"			,objReinsuranceContractNameInfo.CONTRACT_DESCRITION);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CONTRACT_NAME_ID",objReinsuranceContractNameInfo.CONTRACT_NAME_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objReinsuranceContractNameInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsuranceContractName.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceContractNameInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//objTransactionInfo.RECORDED_BY		=	objReinsuranceContractNameInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Contract Name Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int  intContract_NameID= int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (intContract_NameID == -1)
				{
					return -1;
				}
				else
				{
					objReinsuranceContractNameInfo.CONTRACT_NAME_ID= intContract_NameID.ToString();
					return intContract_NameID;
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
		public int Update(ClsReinsuranceContractNameInfo objOldReinsuranceContractNameInfo, ClsReinsuranceContractNameInfo objReinsuranceContractNameInfo)
		{
			string	strStoredProc	=	"Proc_UpdateContractName";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CONTRACT_NAME_ID"				,objReinsuranceContractNameInfo.CONTRACT_NAME_ID);
				objDataWrapper.AddParameter("@CONTRACT_NAME"				,objReinsuranceContractNameInfo.CONTRACT_NAME);
				objDataWrapper.AddParameter("@CONTRACT_DESCRIPTION"			,objReinsuranceContractNameInfo.CONTRACT_DESCRITION);
				
				if(base.TransactionLogRequired) 
				{
					objReinsuranceContractNameInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsuranceContractName.aspx.resx");
					objBuilder.GetUpdateSQL(objOldReinsuranceContractNameInfo, objReinsuranceContractNameInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//objTransactionInfo.RECORDED_BY		=	objReinsuranceExcessLayerInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Contract Name Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
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

		public DataSet GetDataForPageControls(string CONTRACT_NAME_ID)
		{
			string strSql = "Proc_GetXMLMNT_REINSURANCE_CONTRACTNAME";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CONTRACT_NAME_ID",CONTRACT_NAME_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		#endregion
	}
}
