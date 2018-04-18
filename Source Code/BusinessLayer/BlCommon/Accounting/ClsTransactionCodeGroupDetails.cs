/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	6/7/2005 7:43:59 PM
<End Date				: -	
<Description				: - 	BL for Transacton code group details.
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
using Cms.DataLayer;
using Cms.Model.Maintenance.Accounting;
using System.Web.UI.WebControls;
using System.Collections;

namespace Cms.BusinessLayer.BlCommon.Accounting
{
	/// <summary>
	/// BL for Transacton code group details.
	/// </summary>
	public class ClsTransactionCodeGroupDetails : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		ACT_TRAN_CODE_GROUP_DETAILS			=	"ACT_TRAN_CODE_GROUP_DETAILS";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _DETAIL_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateACT_TRAN_CODE_GROUP_DETAILS";
		#endregion
		DataWrapper objDataWrapper;
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
		public ClsTransactionCodeGroupDetails()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		public ClsTransactionCodeGroupDetails(bool transactionLogRequired):this()
		{
			base.TransactionLogRequired = transactionLogRequired;
		}
		#endregion

		/// <summary>
		/// Adds transaction group details to database
		/// </summary>
		/// <param name="al">Array list containing the array of ClsDepositDetailsInfo objects</param>
		public int AddTransactionCodeGroupDetail(ArrayList al, out ArrayList alStatus)
		{
			DataWrapper objWrapper;
			objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			

			bool SaveStatus = true;

			int intRetVal;
			alStatus = new ArrayList();
			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					if (((ClsTransactionCodeGroupDetailsInfo)al[i]).DETAIL_ID <= 0)
					{
						intRetVal = Add(objWrapper, (ClsTransactionCodeGroupDetailsInfo)al[i]);
						
						alStatus.Add(intRetVal);
						if (intRetVal <= 0 )
						{
							//Some error occured, hence updating the save status flag
							SaveStatus = false;
						}
					}
					else
					{
						intRetVal = UpdateTransGroupDetail(objWrapper,(ClsTransactionCodeGroupDetailsInfo)al[i]);
						alStatus.Add(intRetVal);
						if (intRetVal <= 0 )

						{
							//Some error occured, hence updating the save status flag
							SaveStatus = false;
						}
					}
				}

				if (SaveStatus == false)
				{
					//Some error occured while saving deposit details, hence rollbacking
					objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
					return -1;
				}
				
				
				objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				return 1;

			}
			catch (Exception objExp)
			{
				objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				throw objExp;
			}
			finally
			{
				objWrapper.Dispose();
			}
		}



		//New 
		private int UpdateTransGroupDetail(DataLayer.DataWrapper objDataWrapper, ClsTransactionCodeGroupDetailsInfo ClsTransactionCodeGroupDetailsInfo)
		{
			string strStoredProc = "Proc_UpdateACT_TRAN_CODE_GROUP_DETAILS";
			int returnResult = 0;
			
			try
			{
			
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@DETAIL_ID",ClsTransactionCodeGroupDetailsInfo.DETAIL_ID);
				objDataWrapper.AddParameter("@TRAN_GROUP_ID",ClsTransactionCodeGroupDetailsInfo.TRAN_GROUP_ID);
				objDataWrapper.AddParameter("@TRAN_ID",ClsTransactionCodeGroupDetailsInfo.TRAN_ID);
				objDataWrapper.AddParameter("@DEF_SEQ",ClsTransactionCodeGroupDetailsInfo.DEF_SEQ);
				objDataWrapper.AddParameter("@MODIFIED_BY",ClsTransactionCodeGroupDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ClsTransactionCodeGroupDetailsInfo.LAST_UPDATED_DATETIME);
			
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				objDataWrapper.ClearParameteres();
				
			
				return returnResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			
		}

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="ClsTransactionCodeGroupDetailsInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add( DataLayer.DataWrapper objDataWrapper,ClsTransactionCodeGroupDetailsInfo ClsTransactionCodeGroupDetailsInfo)
		{
			string		strStoredProc	=	"Proc_InsertACT_TRAN_CODE_GROUP_DETAILS";
			DateTime	RecordDate		=	DateTime.Now;
			try
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@TRAN_GROUP_ID",ClsTransactionCodeGroupDetailsInfo.TRAN_GROUP_ID);
				objDataWrapper.AddParameter("@TRAN_ID",ClsTransactionCodeGroupDetailsInfo.TRAN_ID);
				objDataWrapper.AddParameter("@DEF_SEQ",ClsTransactionCodeGroupDetailsInfo.DEF_SEQ);
				objDataWrapper.AddParameter("@IS_ACTIVE",ClsTransactionCodeGroupDetailsInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",ClsTransactionCodeGroupDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",ClsTransactionCodeGroupDetailsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DETAIL_ID",ClsTransactionCodeGroupDetailsInfo.DETAIL_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;				
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				int DETAIL_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				
				if (DETAIL_ID == -1)
				{
					return -1;
				}
				else
				{
					ClsTransactionCodeGroupDetailsInfo.DETAIL_ID = DETAIL_ID;
					return returnResult;
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldTransactionCodeGroupDetailsInfo">Model object having old information</param>
		/// <param name="ClsTransactionCodeGroupDetailsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsTransactionCodeGroupDetailsInfo objOldTransactionCodeGroupDetailsInfo,ClsTransactionCodeGroupDetailsInfo ClsTransactionCodeGroupDetailsInfo)
		{
			string		strStoredProc	=	"Proc_UpdateACT_TRAN_CODE_GROUP_DETAILS";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
		
			try 
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@DETAIL_ID",ClsTransactionCodeGroupDetailsInfo.DETAIL_ID);
				objDataWrapper.AddParameter("@TRAN_GROUP_ID",ClsTransactionCodeGroupDetailsInfo.TRAN_GROUP_ID);
				objDataWrapper.AddParameter("@TRAN_ID",ClsTransactionCodeGroupDetailsInfo.TRAN_ID);
				objDataWrapper.AddParameter("@DEF_SEQ",ClsTransactionCodeGroupDetailsInfo.DEF_SEQ);
				objDataWrapper.AddParameter("@MODIFIED_BY",ClsTransactionCodeGroupDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ClsTransactionCodeGroupDetailsInfo.LAST_UPDATED_DATETIME);
				if(base.TransactionLogRequired) 
				{
					ClsTransactionCodeGroupDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddTransactionCodeGroupDetails.aspx.resx");
					objBuilder.GetUpdateSQL(objOldTransactionCodeGroupDetailsInfo,ClsTransactionCodeGroupDetailsInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	ClsTransactionCodeGroupDetailsInfo.MODIFIED_BY;
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

		public DataSet GetTransactionCodeDetails(string TRAN_GROUP_ID,int startRec,int endRec)	
		{
			
			try
			{
				string strProcName = "Proc_GetTransactionCodeDetails";
				objDataWrapper.ClearParameteres();

				objDataWrapper.AddParameter("@TRAN_GROUP_ID",TRAN_GROUP_ID);
				objDataWrapper.AddParameter("@PAGE_SIZE",endRec);
				objDataWrapper.AddParameter("@CURRENT_PAGE_INDEX",startRec);				
				DataSet ds ;				
				ds=objDataWrapper.ExecuteDataSet(strProcName);				
				return ds;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}

		}

		public static string[][] GetXmlForPageControls(string TRAN_GROUP_ID,int startRec,int endRec)	
		{
			string spName= "Proc_GetTransactionCodeDetailsXML";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			try
			{
				string tableName = "ACT_TRAN_CODE_GROUP_DETAILS";

				string strSQL = "select DETAIL_ID from "+tableName;
				strSQL += " where TRAN_GROUP_ID="+TRAN_GROUP_ID;
								
				DataSet objDataSet   = ExecuteInlineQuery1(strSQL,startRec,endRec);
				
				
				string[][] XMLs = new string[objDataSet.Tables[0].Rows.Count][];
				DataSet objDataSet2;
				for(int i=0;i<objDataSet.Tables[0].Rows.Count;i++)
				{
					XMLs[i] = new string[2];
					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@DETAIL_ID",objDataSet.Tables[0].Rows[i]["DETAIL_ID"]);
					objDataSet2 = objDataWrapper.ExecuteDataSet(spName);					
					XMLs[i][0] = objDataSet2.GetXml();
					XMLs[i][1] = objDataSet.Tables[0].Rows[i]["DETAIL_ID"].ToString();
				}				
				return XMLs;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		#endregion
		#region "Delete method"
		public void Delete(DataTable dt)
		{
			
			try
			{
				string deleteProcName = "Proc_DeleteACT_TRAN_CODE_GROUP_DETAILS";
				foreach(DataRow dr in dt.Rows)
				{
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@Detail_ID",int.Parse(dr["RowId"].ToString()),SqlDbType.Int);
					int result = objDataWrapper.ExecuteNonQuery(deleteProcName);
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{}
		}
		#endregion
		private static DataSet ExecuteInlineQuery1(string sql, int iStartRec, int iEndRec)
		{
			DataSet ds = new DataSet();
			System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(sql,ConnStr);
			da.Fill(ds,iStartRec,iEndRec,"Mapper");
			return ds;
		}
		public void Commit()
		{
			try
			{
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
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
