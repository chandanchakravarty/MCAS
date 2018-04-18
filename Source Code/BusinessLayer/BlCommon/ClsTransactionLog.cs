using System;
using System.Data;
using Cms.DataLayer;
using System.Text;
using System.Collections; 

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsTransactionLog.
	/// </summary>
	public class ClsTransactionLog :ClsCommon  
	{
		public ClsTransactionLog()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public string GetTransactionXML(int transId)
		{			
			return DataWrapper.ExecuteScalar(ConnStr,"Proc_GetTransactionXml",transId).ToString();
		}

		public DataTable GetTransactionXMLDataSet(int transId)
		{	
			DataSet dsTransDesc=new DataSet();
			dsTransDesc = DataWrapper.ExecuteDataset(ConnStr,"Proc_GetTransactionXml",transId);
			return dsTransDesc.Tables[0];
			//return DataWrapperConnStr,"Proc_GetTransactionXml",transId);
		}

		public DataTable GetTransactionDescription(int transId)
		{
			//return DataWrapper.ExecuteScalar(ConnStr,"Proc_GetTransactionDescription",transId).ToString();
			DataSet dsTransDesc=new DataSet();
			dsTransDesc = DataWrapper.ExecuteDataset(ConnStr,"Proc_GetTransactionDescription",transId);
			return dsTransDesc.Tables[0];
		}
		public DataTable GetApplicationDetail(int custId,int appId,int appVersionId)
		{
			DataSet dsTransDesc=new DataSet();
			//dsTransDesc=DataWrapper.ExecuteDataset(ConnStr,"Proc_GetApplicationNumber",custId,appId,appVersionId);
			dsTransDesc=DataWrapper.ExecuteDataset(ConnStr,"Proc_GetApplicationLOB",custId,appId,appVersionId);
			return dsTransDesc.Tables[0];			
		}

		public static string GetTransactionDetails(int trans_id)
		{
			//return DataWrapper.ExecuteScalar(ConnStr,"Proc_GetTransactionDescription",transId).ToString();
			string transDesc="";
			DataSet dsTransDesc=new DataSet();
			dsTransDesc = DataWrapper.ExecuteDataset(ConnStr,"Proc_GetTransactionTypeDetails",trans_id);
			if((dsTransDesc!=null) && (dsTransDesc.Tables.Count>0) && (dsTransDesc.Tables[0].Rows.Count>0))
				transDesc=dsTransDesc.Tables[0].Rows[0]["TRANS_DESC"].ToString();

			return transDesc;
		}
	}
}
