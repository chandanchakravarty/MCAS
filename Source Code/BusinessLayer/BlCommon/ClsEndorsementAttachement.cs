/******************************************************************************************
<Author				: -   Gaurav
<Start Date				: -	10/20/2005 1:25:41 PM
<End Date				: -	
<Description				: - 	attachment
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
	public class ClsEndorsementAttachement : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		MNT_ENDORSEMENT_ATTACHMENT			=	"MNT_ENDORSEMENT_ATTACHMENT";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _ENDORSEMENT_ATTACH_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateMNT_ENDORSEMENT_ATTACHMENT";
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
		public ClsEndorsementAttachement()
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
		public int Add(ClsEndorsementAttachmentInfo objEndorsementAttachmentInfo, ref string InvalidAttach)
		{
			string		strStoredProc	=	"Proc_InsertEndorsementAttachment";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@ENDORSEMENT_ID",objEndorsementAttachmentInfo.ENDORSEMENT_ID);
				objDataWrapper.AddParameter("@ATTACH_FILE",objEndorsementAttachmentInfo.ATTACH_FILE);
				objDataWrapper.AddParameter("@VALID_DATE",objEndorsementAttachmentInfo.VALID_DATE);
				if( objEndorsementAttachmentInfo.EFFECTIVE_TO_DATE != Convert.ToDateTime(null) && objEndorsementAttachmentInfo.EFFECTIVE_TO_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",objEndorsementAttachmentInfo.EFFECTIVE_TO_DATE);
				if( objEndorsementAttachmentInfo.DISABLED_DATE != Convert.ToDateTime(null) && objEndorsementAttachmentInfo.DISABLED_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@DISABLED_DATE",objEndorsementAttachmentInfo.DISABLED_DATE);
				objDataWrapper.AddParameter("@FORM_NUMBER",objEndorsementAttachmentInfo.FORM_NUMBER);
				//objDataWrapper.AddParameter("@FORM_NUMBER",System.DBNull.Value);
				if( objEndorsementAttachmentInfo.EDITION_DATE != null && objEndorsementAttachmentInfo.EDITION_DATE != "")
					objDataWrapper.AddParameter("@EDITION_DATE",objEndorsementAttachmentInfo.EDITION_DATE);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ENDORSEMENT_ATTACH_ID",objEndorsementAttachmentInfo.ENDORSEMENT_ATTACH_ID,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objRetParam = (SqlParameter) objDataWrapper.AddParameter("@ATTACH_FILE1",InvalidAttach,SqlDbType.VarChar,ParameterDirection.Output,500);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objEndorsementAttachmentInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Cmsweb/Maintenance/AddEndorsementAttachment.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objEndorsementAttachmentInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objEndorsementAttachmentInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1708", "");// "Endorsement Attachment is Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int ENDORSEMENT_ATTACH_ID = int.Parse(objSqlParameter.Value.ToString());
				InvalidAttach = objRetParam.Value.ToString();

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (ENDORSEMENT_ATTACH_ID == -1)
				{
					return -1;
				}
				else
				{
					objEndorsementAttachmentInfo.ENDORSEMENT_ATTACH_ID = ENDORSEMENT_ATTACH_ID;
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

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldEndorsementAttachmentInfo">Model object having old information</param>
		/// <param name="objEndorsementAttachmentInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsEndorsementAttachmentInfo objOldEndorsementAttachmentInfo,ClsEndorsementAttachmentInfo objEndorsementAttachmentInfo)
		{
			string		strStoredProc	=	"Proc_UpdateEndorsementAttachment";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@ENDORSEMENT_ATTACH_ID",objEndorsementAttachmentInfo.ENDORSEMENT_ATTACH_ID);
				objDataWrapper.AddParameter("@ENDORSEMENT_ID",objEndorsementAttachmentInfo.ENDORSEMENT_ID);
				objDataWrapper.AddParameter("@ATTACH_FILE",objEndorsementAttachmentInfo.ATTACH_FILE);
				objDataWrapper.AddParameter("@VALID_DATE",objEndorsementAttachmentInfo.VALID_DATE);
				if( objEndorsementAttachmentInfo.EFFECTIVE_TO_DATE != Convert.ToDateTime(null) && objEndorsementAttachmentInfo.EFFECTIVE_TO_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",objEndorsementAttachmentInfo.EFFECTIVE_TO_DATE);
				if( objEndorsementAttachmentInfo.DISABLED_DATE != Convert.ToDateTime(null) && objEndorsementAttachmentInfo.DISABLED_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@DISABLED_DATE",objEndorsementAttachmentInfo.DISABLED_DATE);
				objDataWrapper.AddParameter("@FORM_NUMBER",objEndorsementAttachmentInfo.FORM_NUMBER);
				//objDataWrapper.AddParameter("@FORM_NUMBER",System.DBNull.Value);
				if( objEndorsementAttachmentInfo.EDITION_DATE != null && objEndorsementAttachmentInfo.EDITION_DATE != "")
					objDataWrapper.AddParameter("@EDITION_DATE",objEndorsementAttachmentInfo.EDITION_DATE);
				if(base.TransactionLogRequired) 
				{
					objEndorsementAttachmentInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Cmsweb/Maintenance/AddEndorsementAttachment.aspx.resx");
					objBuilder.GetUpdateSQL(objOldEndorsementAttachmentInfo,objEndorsementAttachmentInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objEndorsementAttachmentInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1709", "");// "Endorsement Attachment is Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

					/*if(objEndorsementAttachmentInfo.EFFECTIVE_TO_DATE.Ticks!=0) 
						objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",objEndorsementAttachmentInfo.EFFECTIVE_TO_DATE);
					else
						objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",DBNull.Value );
				
					if(objEndorsementAttachmentInfo.DISABLED_DATE.Ticks!=0)
						objDataWrapper.AddParameter("@DISABLED_DATE",objEndorsementAttachmentInfo.DISABLED_DATE );
					else
						objDataWrapper.AddParameter("@DISABLED_DATE",DBNull.Value );

					if(objEndorsementAttachmentInfo.EDITION_DATE!="")
						objDataWrapper.AddParameter("@EDITION_DATE",objEndorsementAttachmentInfo.EDITION_DATE );
					else
						objDataWrapper.AddParameter("@EDITION_DATE",DBNull.Value );*/

					objDataWrapper.AddParameter("@FORM_NUMBER",objEndorsementAttachmentInfo.FORM_NUMBER );


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

		#region Delete method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldEndorsementAttachmentInfo">Model object having old information</param>
		/// <param name="objEndorsementAttachmentInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Delete(int endorsement_attach_id, int userId)
		{
			string		strStoredProc	=	"Proc_DeleteEndorsementAttachment";
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@ENDORSEMENT_ATTACH_ID",endorsement_attach_id);
				if(base.TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	userId;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1710", "");// "Endorsement Attachment is Deleted";
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
		public static string GetXmlForPageControls(string ENDORSEMENT_ATTACH_ID)
		{
			string strSql = "Proc_GetXMLEndorsementAttachment";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@ENDORSEMENT_ATTACH_ID",ENDORSEMENT_ATTACH_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}
		#endregion


	}
}
