/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	4/28/2005 8:48:58 PM
<End Date				: -	
<Description				: - 	Business Logic for PkgLobDetails.
<Review Date				: - 
<Reviewed By			: - 	

Modification History
<Modified Date			: - 03/05/2005
<Modified By			: - Pradeep Iyer 
<Purpose				: - Corrected the order of Transact XML methods in the Update method
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Application;
using Cms.BusinessLayer.BlCommon;
using System.Web.UI.WebControls;
namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Class that abstracts Business Logic for PkgLobDetails.
	/// </summary>
	public class clsPkgLobDetails : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_PKG_LOB_DETAILS			=	"APP_PKG_LOB_DETAILS";

		#region Private Instance Variables
			private			bool		boolTransactionLog;
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
		public clsPkgLobDetails()
		{
			boolTransactionLog	= base.TransactionLogRequired;
		}
		public clsPkgLobDetails(bool transactionLogRequired):this()
		{
			base.TransactionLogRequired = transactionLogRequired;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPkgLobDetails">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsPkgLobDetailsInfo objPkgLobDetails)
		{
			string		strStoredProc	=	"Proc_InsertAPP_PKG_LOB_DETAILS";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPkgLobDetails.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objPkgLobDetails.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objPkgLobDetails.APP_VERSION_ID);
				objDataWrapper.AddParameter("@LOB",objPkgLobDetails.LOB);
				if(objPkgLobDetails.SUB_LOB=="-1")
					objDataWrapper.AddParameter("@SUB_LOB",DBNull.Value);
				else
					objDataWrapper.AddParameter("@SUB_LOB",objPkgLobDetails.SUB_LOB);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@REC_ID",objPkgLobDetails.REC_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objPkgLobDetails.TransactLabel = ClsCommon.MapTransactionLabel("/application/aspx/AddPkgLobDetails.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objPkgLobDetails);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	LoggedInUserId;
					objTransactionInfo.APP_ID = objPkgLobDetails.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objPkgLobDetails.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objPkgLobDetails.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"New LOB is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int REC_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (REC_ID == -1)
				{
					return -1;
				}
				else
				{
					objPkgLobDetails.REC_ID = REC_ID;
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
		/// <param name="objOldPkgLobDetails">Model object having old information</param>
		/// <param name="objPkgLobDetails">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsPkgLobDetailsInfo objOldPkgLobDetails,ClsPkgLobDetailsInfo objPkgLobDetails)
		{
			string strTranXML;
			string		strStoredProc	=	"Proc_UpdateAPP_PKG_LOB_DETAILS";
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@REC_ID",objPkgLobDetails.REC_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPkgLobDetails.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objPkgLobDetails.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objPkgLobDetails.APP_VERSION_ID);
				objDataWrapper.AddParameter("@LOB",objPkgLobDetails.LOB);
				objDataWrapper.AddParameter("@SUB_LOB",objPkgLobDetails.SUB_LOB);
				if(TransactionLogRequired) 
				{
					objPkgLobDetails.TransactLabel = ClsCommon.MapTransactionLabel("/application/aspx/AddPkgLobDetails.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldPkgLobDetails,objPkgLobDetails);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.APP_ID = objPkgLobDetails.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objPkgLobDetails.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objPkgLobDetails.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"LOB is modified";
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

		#region GetLOBXML

		public static string GetXmlForLobByState()
		{
			string strSql = "Proc_GetLobAndSubLOBByState";
			string strReturnValue;

			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,strSql);

			if (objDataSet.Tables[0].Rows.Count == 0)
			{
				strReturnValue = "";
			}
			else
			{
				strReturnValue = objDataSet.GetXml();
			}

			return strReturnValue;
		}




		/// <summary>
		/// This is used to Generate XML for LOB AND SUB LOB
		///  this function will stop post back of page to reterive value of other drop down on selected index change
		/// </summary>
		/// <returns></returns>
		public static string GetXmlForLob()
		{
			string strSql = "Proc_GetLobAndSubLOBs";
			string strReturnValue;

			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,strSql);

			if (objDataSet.Tables[0].Rows.Count == 0)
			{
				strReturnValue = "";
			}
			else
			{
				strReturnValue = objDataSet.GetXml();
			}

			return strReturnValue;
		}

		#endregion
		
		#region "Get Xml Methods"
		public static string GetXmlForPageControls(string REC_ID)
		{
			string strSql = "select t1.SUB_LOB SUB_LOB1,t1.LOB,t1.SUB_LOB,t1.REC_ID,t1.CUSTOMER_ID,t1.APP_ID,t1.APP_VERSION_ID";
			strSql += " from APP_PKG_LOB_DETAILS t1";
			strSql += " where  REC_ID=" + REC_ID;
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			return objDataSet.GetXml();
		}
		#endregion

		public int Delete(int recId,int intCustomerId,int intAppId,int intAppVersionId)
		{

			
			int returnResult=0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			try 
			{
				objDataWrapper.AddParameter("@REC_ID",recId);
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@APP_ID",intAppId );
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId );
				returnResult=objDataWrapper.ExecuteNonQuery("Proc_DeleteAPP_PKG_LOB_DETAILS");
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
		}
	
	}

