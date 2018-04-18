/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	6/6/2005 5:55:53 PM
<End Date				: -	
<Description			: - 	 Business Logic for Transaction Codes.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Maintenance.Accounting;
using System.Web.UI.WebControls;

namespace Cms.BusinessLayer.BlCommon.Accounting
{
	/// <summary>
	///  Business Logic for Transaction Codes.
	/// </summary>
	public class ClsTransactionCodes: Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
			private const	string		ACT_TRANSACTION_CODES			=	"ACT_TRANSACTION_CODES";

			#region Private Instance Variables
			private			bool		boolTransactionLog;
			//private int _TRAN_ID;
			private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateACT_TRANSACTION_CODES";
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
			public ClsTransactionCodes()
			{
				boolTransactionLog	= base.TransactionLogRequired;
				base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
			}
			public ClsTransactionCodes(bool transactionLogRequired):this()
			{
				base.TransactionLogRequired = transactionLogRequired;
			}
			#endregion

			#region Add(Insert) functions
			/// <summary>
			/// Saves the information passed in model object to database.
			/// </summary>
			/// <param name="objTransactioncodesInfo">Model class object.</param>
			/// <returns>No of records effected.</returns>
			public int Add(ClsTransactionCodesInfo objTransactioncodesInfo)
			{
				string		strStoredProc	=	"Proc_InsertACT_TRANSACTION_CODES";
				DateTime	RecordDate		=	DateTime.Now;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				try
				{
					objDataWrapper.AddParameter("@CATEGOTY_CODE",objTransactioncodesInfo.CATEGOTY_CODE);
					objDataWrapper.AddParameter("@TRAN_TYPE",objTransactioncodesInfo.TRAN_TYPE);
					objDataWrapper.AddParameter("@TRAN_CODE",objTransactioncodesInfo.TRAN_CODE);
					objDataWrapper.AddParameter("@DISPLAY_DESCRIPTION",objTransactioncodesInfo.DISPLAY_DESCRIPTION);
					objDataWrapper.AddParameter("@PRINT_DESCRIPTION",objTransactioncodesInfo.PRINT_DESCRIPTION);
					if(objTransactioncodesInfo.TRAN_TYPE.Equals("fee") || objTransactioncodesInfo.TRAN_TYPE.Equals("dis"))
					{
						objDataWrapper.AddParameter("@DEF_AMT_CALC_TYPE",objTransactioncodesInfo.DEF_AMT_CALC_TYPE);
						objDataWrapper.AddParameter("@DEF_AMT",objTransactioncodesInfo.DEF_AMT);
					}
					else
					{
						objDataWrapper.AddParameter("@DEF_AMT_CALC_TYPE",DBNull.Value);
						objDataWrapper.AddParameter("@DEF_AMT",DBNull.Value);
					}
					
					if(objTransactioncodesInfo.TRAN_TYPE.Equals("pre"))
						objDataWrapper.AddParameter("@AGENCY_COMM_APPLIES",objTransactioncodesInfo.AGENCY_COMM_APPLIES);
					else
						objDataWrapper.AddParameter("@AGENCY_COMM_APPLIES",DBNull.Value);	
					if(objTransactioncodesInfo.TRAN_TYPE.Equals("fee"))
                        objDataWrapper.AddParameter("@GL_INCOME_ACC",objTransactioncodesInfo.GL_INCOME_ACC);
					else
						objDataWrapper.AddParameter("@GL_INCOME_ACC",DBNull.Value);
					objDataWrapper.AddParameter("@IS_DEF_NEGATIVE",objTransactioncodesInfo.IS_DEF_NEGATIVE);
					objDataWrapper.AddParameter("@IS_ACTIVE",objTransactioncodesInfo.IS_ACTIVE);
					objDataWrapper.AddParameter("@CREATED_BY",objTransactioncodesInfo.CREATED_BY);
					objDataWrapper.AddParameter("@CREATED_DATETIME",objTransactioncodesInfo.CREATED_DATETIME);
					objDataWrapper.AddParameter("@MODIFIED_BY",objTransactioncodesInfo.MODIFIED_BY);
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objTransactioncodesInfo.LAST_UPDATED_DATETIME);
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@TRAN_ID",objTransactioncodesInfo.TRAN_ID,SqlDbType.Int,ParameterDirection.Output);

					int returnResult = 0;
					if(TransactionLogRequired)
					{
						objTransactioncodesInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddTransactionCodes.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTranXML = objBuilder.GetTransactionLogXML(objTransactioncodesInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objTransactioncodesInfo.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
					int TRAN_ID = int.Parse(objSqlParameter.Value.ToString());
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					if (TRAN_ID == -1)
					{
						return -1;
					}
					else
					{
						objTransactioncodesInfo.TRAN_ID = TRAN_ID;
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
			/// <param name="objOldTransactioncodesInfo">Model object having old information</param>
			/// <param name="objTransactioncodesInfo">Model object having new information(form control's value)</param>
			/// <returns>No. of rows updated (1 or 0)</returns>
			public int Update(ClsTransactionCodesInfo objOldTransactioncodesInfo,ClsTransactionCodesInfo objTransactioncodesInfo)
			{
				string		strStoredProc	=	"Proc_UpdateACT_TRANSACTION_CODES";
				string strTranXML;
				int returnResult = 0;
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				try 
				{
					objDataWrapper.AddParameter("@TRAN_ID",objTransactioncodesInfo.TRAN_ID);
					objDataWrapper.AddParameter("@CATEGOTY_CODE",objTransactioncodesInfo.CATEGOTY_CODE);
					objDataWrapper.AddParameter("@TRAN_TYPE",objTransactioncodesInfo.TRAN_TYPE);
					objDataWrapper.AddParameter("@TRAN_CODE",objTransactioncodesInfo.TRAN_CODE);
					objDataWrapper.AddParameter("@DISPLAY_DESCRIPTION",objTransactioncodesInfo.DISPLAY_DESCRIPTION);
					objDataWrapper.AddParameter("@PRINT_DESCRIPTION",objTransactioncodesInfo.PRINT_DESCRIPTION);
					if(objTransactioncodesInfo.TRAN_TYPE.Equals("fee") || objTransactioncodesInfo.TRAN_TYPE.Equals("dis"))
					{
						objDataWrapper.AddParameter("@DEF_AMT_CALC_TYPE",objTransactioncodesInfo.DEF_AMT_CALC_TYPE);
						objDataWrapper.AddParameter("@DEF_AMT",objTransactioncodesInfo.DEF_AMT);
					}
					else
					{
						objDataWrapper.AddParameter("@DEF_AMT_CALC_TYPE",DBNull.Value);
						objDataWrapper.AddParameter("@DEF_AMT",DBNull.Value);
					}

					if(objTransactioncodesInfo.TRAN_TYPE.Equals("pre"))
                        objDataWrapper.AddParameter("@AGENCY_COMM_APPLIES",objTransactioncodesInfo.AGENCY_COMM_APPLIES);
					else
						objDataWrapper.AddParameter("@AGENCY_COMM_APPLIES",DBNull.Value);

					if(objTransactioncodesInfo.TRAN_TYPE.Equals("fee"))
						objDataWrapper.AddParameter("@GL_INCOME_ACC",objTransactioncodesInfo.GL_INCOME_ACC);
					else
						objDataWrapper.AddParameter("@GL_INCOME_ACC",DBNull.Value);
					objDataWrapper.AddParameter("@IS_DEF_NEGATIVE",objTransactioncodesInfo.IS_DEF_NEGATIVE);
					objDataWrapper.AddParameter("@MODIFIED_BY",objTransactioncodesInfo.MODIFIED_BY);
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objTransactioncodesInfo.LAST_UPDATED_DATETIME);
					if(base.TransactionLogRequired) 
					{
						objTransactioncodesInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddTransactionCodes.aspx.resx");
						objBuilder.GetUpdateSQL(objOldTransactioncodesInfo,objTransactioncodesInfo,out strTranXML);

						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objTransactioncodesInfo.MODIFIED_BY;
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
		
			#region "Get Xml Methods"
			public static string GetXmlForPageControls(string TRAN_ID)	
			{
				string spName= "Proc_GetTransactionCodesXML";
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				objDataWrapper.AddParameter("@TRAN_ID",TRAN_ID);
				return objDataWrapper.ExecuteDataSet(spName).GetXml();
			}
		#endregion
		
		}
	}
