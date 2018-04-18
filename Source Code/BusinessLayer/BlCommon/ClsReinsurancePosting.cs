using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Maintenance;
using Cms.Model.Maintenance.Reinsurance;
using System.Web.UI.WebControls;
namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsReinsurancePosting.
	/// </summary>
	public class ClsReinsurancePosting : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable 
	{
		public ClsReinsurancePosting()
		{
		
		}
		public DataSet  GetComboBox()
		{
			string strProc="Proc_GetReinsureAccountsInDropDown";
			DataSet objData;
			
			objData=Cms.DataLayer.DataWrapper.ExecuteDataset(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,strProc);
		    return objData;
	   }
		//Get The Contact Information
		public DataSet GetContractInformation(int ContractId)
		{
			string strProc="Proc_GetContractInformation";
			DataSet assState;
			try
			{
				Object[] objParam=new object[1];
				objParam[0]=ContractId;
				assState=Cms.DataLayer.DataWrapper.ExecuteDataset(ConnStr,strProc,objParam);
				return assState;
			}
			catch(Exception ex)
			{
				throw(ex);
			}


		}
		//Add New Record
		public int Add(ClsReinsurancePostingInfo objdata)
		{
			string	strStoredProc	 = "Proc_AddReinsurancePostingContract";
			Cms.DataLayer.DataWrapper objDataWrapper = null;
			int intResult = 0;
			try
			{
			
				objDataWrapper = new Cms.DataLayer.DataWrapper(ConnStr,CommandType.StoredProcedure ,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				objDataWrapper.AddParameter("@CONTRACT_ID",objdata.contract_id);
				objDataWrapper.AddParameter("@GL_ID",objdata.GL_ID);
                objDataWrapper.AddParameter("@COMMISION_APPLICABLE",objdata.Commision_applicable);
				objDataWrapper.AddParameter("@REIN_PREMIUM_ACT",objdata.Rein_Premium_Act);
				objDataWrapper.AddParameter("@REIN_PAYMENT_ACT",objdata.Rein_Payment_Act);
				objDataWrapper.AddParameter("@REIN_COMMISION_ACT",DefaultValues.GetIntNull(objdata.Rein_Commision_Act));
				objDataWrapper.AddParameter("@REIN_COMMISION_RECEVABLE",DefaultValues.GetIntNull(objdata.Rein_Commision_Recevable));
                objDataWrapper.CommandType = CommandType.StoredProcedure;
			
			
				//Get the tran log XML , if present
				if (TransactionLogRequired)
				{
					objdata.TransactLabel  = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsurancePosting.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objdata);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.ENTITY_ID        =   objdata.contract_id;
					objTransactionInfo.RECORDED_BY		=	objdata.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					intResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
				objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.CommitTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES );
				intResult = 1;
			}
			catch(Exception ex)
			{
				throw ex;			
			}	
			finally
			{
				if (objDataWrapper != null)
					objDataWrapper.Dispose();
			}
			return intResult;
		}
		public string GetContactNameFromId(int Contact_id)
		{
			string strStroredProc  =  "PROC_GETCONTRACT_NAMEFROM_ID";
			DataSet assState;
			try
			{
				Object[] objParam=new object[1];
				objParam[0]=Contact_id;
				assState=Cms.DataLayer.DataWrapper.ExecuteDataset(ConnStr,strStroredProc,objParam);
				if(assState.Tables[0].Rows.Count>0)
				{
					return assState.Tables[0].Rows[0]["CONTRACT_NAME"].ToString();
				}
				return "";
			}
			catch(Exception ex)
			{
				throw(ex);
			}


		}
		//Update The Record 
		public int Update(ClsReinsurancePostingInfo objOlddata,ClsReinsurancePostingInfo objdata)
		{
			string	strStoredProc	 = "Proc_UpdateReinsurancePostingContract";
			string strTranXML;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			Cms.DataLayer.DataWrapper objDataWrapper = null;
			int intResult = 0;
			try
			{
			
				objDataWrapper = new Cms.DataLayer.DataWrapper(ConnStr,CommandType.Text,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				objDataWrapper.AddParameter("@CONTRACT_ID",objdata.contract_id);
				objDataWrapper.AddParameter("@COMMISION_APPLICABLE",objdata.Commision_applicable);
				objDataWrapper.AddParameter("@REIN_PREMIUM_ACT",objdata.Rein_Premium_Act);
				objDataWrapper.AddParameter("@REIN_PAYMENT_ACT",objdata.Rein_Payment_Act);
				objDataWrapper.AddParameter("@GL_ID",objdata.GL_ID);
				objDataWrapper.AddParameter("@REIN_COMMISION_ACT",DefaultValues.GetIntNull(objdata.Rein_Commision_Act));
				objDataWrapper.AddParameter("@REIN_COMMISION_RECEVABLE",DefaultValues.GetIntNull(objdata.Rein_Commision_Recevable));
				objDataWrapper.CommandType = CommandType.StoredProcedure;
				if(base.TransactionLogRequired) 
				{
					objdata.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsurancePosting.aspx.resx");
					objBuilder.GetUpdateSQL(objOlddata,objdata,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.ENTITY_ID        =    objdata.contract_id;
					objTransactionInfo.RECORDED_BY 		=	objdata.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					intResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				//}	
				objDataWrapper.CommitTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES );
				intResult = 1;
			}
			catch(Exception ex)
			{
				throw ex;			
			}	
			finally
			{
				if (objDataWrapper != null)
					objDataWrapper.Dispose();
			}
			return intResult;
		}
	}
}
