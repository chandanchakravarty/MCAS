/* ***************************************************************************************
   Author		: Swarup Kumar Pal 
   Creation Date: August 14, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: Class file for Loss Layer 
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
	/// Summary description for ClsLossLayer.
	/// </summary>
	public class ClsLossLayer:Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		# region D E C L A R A T I O N 

		private	bool boolTransactionLog;
		public ClsLossLayer()
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
		
		public int Add(ClsLossLayerInfo objLossLayerInfo)
		{
			string		strStoredProc	=	"Proc_InsertREIN_LOSSLAYER";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CONTRACT_ID",objLossLayerInfo.CONTRACT_ID);
				objDataWrapper.AddParameter("@LAYER",objLossLayerInfo.LAYER);
				objDataWrapper.AddParameter("@COMPANY_RETENTION",objLossLayerInfo.COMPANY_RETENTION);
				objDataWrapper.AddParameter("@LAYER_AMOUNT",objLossLayerInfo.LAYER_AMOUNT);
				objDataWrapper.AddParameter("@RETENTION_AMOUNT",objLossLayerInfo.RETENTION_AMOUNT);
				objDataWrapper.AddParameter("@RETENTION_PERCENTAGE",objLossLayerInfo.RETENTION_PERCENTAGE);
				objDataWrapper.AddParameter("@REIN_CEDED",objLossLayerInfo.REIN_CEDED);
				objDataWrapper.AddParameter("@REIN_CEDED_PERCENTAGE",objLossLayerInfo.REIN_CEDED_PERCENTAGE);
				objDataWrapper.AddParameter("@CREATED_BY",objLossLayerInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objLossLayerInfo.CREATED_DATETIME);				
			
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@LOSS_LAYER_ID",objLossLayerInfo.LOSS_LAYER_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objLossLayerInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/Maintenance/Reinsurance/LossLayer.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objLossLayerInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objLossLayerInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Loss Layer Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int  intLOSS_LAYER_ID= int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (intLOSS_LAYER_ID == -1)
				{
					return -1;
				}
				else
				{
					objLossLayerInfo.LOSS_LAYER_ID= intLOSS_LAYER_ID;
					return intLOSS_LAYER_ID;
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) 
					objDataWrapper.Dispose();
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
		public int Update(ClsLossLayerInfo objOldLossLayerInfo,ClsLossLayerInfo objLossLayerInfo)
		{
			string	strStoredProc	=	"Proc_UpdateREIN_LOSSLAYER";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@LOSS_LAYER_ID",objLossLayerInfo.LOSS_LAYER_ID);
				objDataWrapper.AddParameter("@LAYER",objLossLayerInfo.LAYER);
				objDataWrapper.AddParameter("@COMPANY_RETENTION",objLossLayerInfo.COMPANY_RETENTION);
				objDataWrapper.AddParameter("@LAYER_AMOUNT",objLossLayerInfo.LAYER_AMOUNT);
				objDataWrapper.AddParameter("@RETENTION_AMOUNT",objLossLayerInfo.RETENTION_AMOUNT);
				objDataWrapper.AddParameter("@RETENTION_PERCENTAGE",objLossLayerInfo.RETENTION_PERCENTAGE);
				objDataWrapper.AddParameter("@REIN_CEDED",objLossLayerInfo.REIN_CEDED);
				objDataWrapper.AddParameter("@REIN_CEDED_PERCENTAGE",objLossLayerInfo.REIN_CEDED_PERCENTAGE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objLossLayerInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objLossLayerInfo.LAST_UPDATED_DATETIME);				
			
				
				if(base.TransactionLogRequired) 
				{
					objLossLayerInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/Maintenance/Reinsurance/LossLayer.aspx.resx");
					objBuilder.GetUpdateSQL(objOldLossLayerInfo, objLossLayerInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objLossLayerInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Loss Layer Has Been Updated";
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

		#region		ACTIVATE/DEACTIVATE
		public void ActivateDeactivateLossLayer(ClsLossLayerInfo objLossLayerInfo,string strLoss,string isActive)
		{
			DataWrapper objDataWrapper	=	new DataWrapper( ConnStr, CommandType.StoredProcedure);
			string strActivateDeactivateProcedure="Proc_ActivateDeactivateLossLayer";
			try
			{
				objDataWrapper.AddParameter("@LOSS_LAYER_ID",strLoss);
				objDataWrapper.AddParameter("@IS_ACTIVE",isActive);
				if(TransactionLogRequired) 
				{
					objLossLayerInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/Maintenance/Reinsurance/LossLayer.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objLossLayerInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID		=	3;
					if(isActive.Equals("Y"))
						objTransactionInfo.TRANS_DESC		=	"Loss Layer has been activated.";
					else
						objTransactionInfo.TRANS_DESC		=	"Loss Layer has been deactivated.";
					objTransactionInfo.RECORDED_BY			=	objLossLayerInfo.CREATED_BY;
					objTransactionInfo.CHANGE_XML			=	strTranXML;
//					objTransactionInfo.CUSTOM_INFO			=	strLoss;
					objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure, objTransactionInfo);
				}
				else
					objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure);

				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}	
		}

		#endregion

		# region DELETE

		/// <summary>
		/// Delete the whole record of spefied id 
		/// </summary>
		/// <returns>Nos of rows deleted</returns>
		public int Delete(ClsLossLayerInfo objLossLayerInfo,int intLossLayerId, int UserId)
		{
			try
			{
				/*Calling the stored procedure to get the maximum Journal entry no*/
				String strStoredProc = "Proc_DeleteREIN_LOSSLAYER";
				int Value;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@LOSS_LAYER_ID", intLossLayerId);
				SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				if(TransactionLogRequired) 
				{
					objLossLayerInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/Maintenance/Reinsurance/LossLayer.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objLossLayerInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objLossLayerInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Loss Layer has been deleted.";	
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//objTransactionInfo.CUSTOM_INFO = "Journal Entry # " + strJournalNo;
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
				}
				else
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc);

				Value = int.Parse(objRetVal.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();

				return Value ;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		
		#endregion

		#region GetLossLayerInfo
		/// <summary>
		/// Returns the data in the form of XML of specified intCoverageId
		/// </summary>
		/// <param name="LossLayerId">LossLayerId whose data will be returned</param>
		/// <returns>XML of data</returns>
		public static string GetLossLayerInfo(int intContractId,int intLossLayerId)
		{
			String strStoredProc = "Proc_GetREIN_LOSSLAYER";
			DataSet dsLossLayerInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CONTRACT_ID",intContractId);
				objDataWrapper.AddParameter("@LOSS_LAYER_ID",intLossLayerId);
				
				dsLossLayerInfo = objDataWrapper.ExecuteDataSet(strStoredProc);
				
				if (dsLossLayerInfo.Tables[0].Rows.Count != 0)
				{
					return dsLossLayerInfo.GetXml();
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
