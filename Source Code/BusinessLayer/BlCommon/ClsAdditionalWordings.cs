/******************************************************************************************
<Author					: -   Swarup
<Start Date				: -	22-Feb-2007 
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - E:\Projects\EBX-DV25\Source Code\businesslayer\blcommon\
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer;
//using Cms.BusinessLayer.BlApplication;
//using Cms.Client;
using Cms.DataLayer;
using Cms.Model.Maintenance;
namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// mnm,
	/// </summary>
public class ClsAdditionalWordings : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
{
	private const	string		MNT_PROCESS_WORDINGS			=	"MNT_PROCESS_WORDINGS";

	#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int WORDINGS_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateMNT_PROCESS_WORDINGS";
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
	public ClsAdditionalWordings()
	{
		boolTransactionLog	= base.TransactionLogRequired;
		base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
	}
	#endregion

	#region Add(Insert) functions
			/// <summary>
			/// Saves the information passed in model object to database.
			/// </summary>
			/// <param name="objEndorsementAttachmentInfo">Model class object.</param>
			/// <returns>No of records effected.</returns>
	public int Add(ClsAdditionalWordingsInfo objAdditionalWordingsInfo)
	{
		string		strStoredProc	=	"Proc_InsertAdditionalWordings";
		DateTime	RecordDate		=	DateTime.Now;
		DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

		try
		{
			objDataWrapper.AddParameter("@WORDINGS_ID",objAdditionalWordingsInfo.WORDINGS_ID);
			objDataWrapper.AddParameter("@STATE_ID",objAdditionalWordingsInfo.STATE_ID);
			objDataWrapper.AddParameter("@PROCESS_ID",objAdditionalWordingsInfo.PROCESS_ID);
			objDataWrapper.AddParameter("@LOB_ID",objAdditionalWordingsInfo.LOB_ID);
			objDataWrapper.AddParameter("@PDF_WORDINGS",objAdditionalWordingsInfo.PDF_WORDINGS);
			objDataWrapper.AddParameter("@IS_ACTIVE",objAdditionalWordingsInfo.IS_ACTIVE);
			objDataWrapper.AddParameter("@CREATED_BY",objAdditionalWordingsInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",objAdditionalWordingsInfo.CREATED_DATETIME );

			int returnResult = 0;
			if(TransactionLogRequired)
			{
				objAdditionalWordingsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Cmsweb/Maintenance/AdditionalWordings.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objAdditionalWordingsInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.RECORDED_BY		=	objAdditionalWordingsInfo.CREATED_BY;
                objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1638", "");// "New Additional Wording is added";
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
		catch
		{
	//		throw(ex);
			return -1;
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
			/// <param name="objOldEndorsementAttachmentInfo">Model object having old information</param>
			/// <param name="objEndorsementAttachmentInfo">Model object having new information(form control's value)</param>
			/// <returns>No. of rows updated (1 or 0)</returns>
	public int Update(ClsAdditionalWordingsInfo objOldAdditionalWordingsInfo,ClsAdditionalWordingsInfo objAdditionalWordingsInfo, string Custom)
	{
		string		strStoredProc	=	"Proc_UpdateAdditionalWordings";
		string strTranXML;
		int returnResult = 0;
		SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
		DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
		try 
		{
			string process="", state="", lob = "";
			string []arrCustom = Custom.Split('~');
			if(arrCustom.Length >= 3)
			{
				process = arrCustom[0];
				state = arrCustom[1];
				lob = arrCustom[2];
			}

			objDataWrapper.AddParameter("@WORDINGS_ID",objAdditionalWordingsInfo.WORDINGS_ID);
			objDataWrapper.AddParameter("@STATE_ID",objAdditionalWordingsInfo.STATE_ID);
			objDataWrapper.AddParameter("@PROCESS_ID",objAdditionalWordingsInfo.PROCESS_ID);
			objDataWrapper.AddParameter("@LOB_ID",objAdditionalWordingsInfo.LOB_ID);
			objDataWrapper.AddParameter("@PDF_WORDINGS",objAdditionalWordingsInfo.PDF_WORDINGS);
			objDataWrapper.AddParameter("@IS_ACTIVE",objAdditionalWordingsInfo.IS_ACTIVE);
			objDataWrapper.AddParameter("@MODIFIED_BY",objAdditionalWordingsInfo.MODIFIED_BY);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objAdditionalWordingsInfo.LAST_UPDATED_DATETIME );

			if(base.TransactionLogRequired) 
			{
				objAdditionalWordingsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Cmsweb/Maintenance/AdditionalWordings.aspx.resx");
				strTranXML =objBuilder.GetTransactionLogXML(objOldAdditionalWordingsInfo,objAdditionalWordingsInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	3;
				objTransactionInfo.RECORDED_BY		=	objAdditionalWordingsInfo.MODIFIED_BY;
                objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1639", "");// "Additional Wording is modified";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				objTransactionInfo.CUSTOM_INFO      =  "; State:" + state +"<br>" 
													   +"; LOB:"  + lob + "<br>"
													   +"; Policy Process:" + process ;

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

	#region "GetxmlMethods"
	public static DataSet GetXmlForPageControls(int WORDINGS_ID)
	{
		string strSql = "Proc_GetXMLAdditionalWordings ";
		DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
		objDataWrapper.AddParameter("@WORDINGS_ID",WORDINGS_ID);
		DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
		return objDataSet;
	}
	#endregion


	}
}


//using System;
//
//namespace Cms.BusinessLayer.BlCommon
//{
//	/// <summary>
//	/// Summary description for ClsAdditionalWordings.
//	/// </summary>
//	public class ClsAdditionalWordings
//	{
//		public ClsAdditionalWordings()
//		{
//			//
//			// TODO: Add constructor logic here
//			//
//		}
//	}
//}
