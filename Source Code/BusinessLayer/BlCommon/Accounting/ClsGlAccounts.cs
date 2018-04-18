/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	  5/18/2005 2:34:35 PM
<End Date				: -	
<Description			: -   Business Logic for GL Accounts.
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
	/// Business Logic for GL Accounts.
	/// </summary>
	public class ClsGlAccounts : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		ACT_GL_ACCOUNTS			=	"ACT_GL_ACCOUNTS";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		// private int _ACCOUNT_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateACT_GL_ACCOUNTS";
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
		public ClsGlAccounts()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		public ClsGlAccounts(bool transactionLogRequired):this()
		{
			base.TransactionLogRequired = transactionLogRequired;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objGlAccountsInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsGlAccountsInfo objGlAccountsInfo)
		{
			string		strStoredProc	=	"Proc_InsertACT_GL_ACCOUNTS";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@GL_ID",objGlAccountsInfo.GL_ID);
				//objDataWrapper.AddParameter("@FISCAL_ID",objGlAccountsInfo.FISCAL_ID);
				objDataWrapper.AddParameter("@CATEGORY_TYPE",objGlAccountsInfo.CATEGORY_TYPE);
				objDataWrapper.AddParameter("@GROUP_TYPE",objGlAccountsInfo.GROUP_TYPE);
			
				objDataWrapper.AddParameter("@ACC_TYPE_ID",objGlAccountsInfo.ACC_TYPE_ID);

				if(objGlAccountsInfo.ACC_PARENT_ID<=0)
					objDataWrapper.AddParameter("@ACC_PARENT_ID",DBNull.Value);
				else	
					objDataWrapper.AddParameter("@ACC_PARENT_ID",objGlAccountsInfo.ACC_PARENT_ID);

				objDataWrapper.AddParameter("@ACC_NUMBER",objGlAccountsInfo.ACC_NUMBER);
				objDataWrapper.AddParameter("@ACC_LEVEL_TYPE",objGlAccountsInfo.ACC_LEVEL_TYPE);
				objDataWrapper.AddParameter("@ACC_DESCRIPTION",objGlAccountsInfo.ACC_DESCRIPTION);
				objDataWrapper.AddParameter("@ACC_TOTALS_LEVEL",objGlAccountsInfo.ACC_TOTALS_LEVEL);
				objDataWrapper.AddParameter("@ACC_JOURNAL_ENTRY",objGlAccountsInfo.ACC_JOURNAL_ENTRY);
				objDataWrapper.AddParameter("@ACC_CASH_ACCOUNT",objGlAccountsInfo.ACC_CASH_ACCOUNT);
				objDataWrapper.AddParameter("@ACC_CASH_ACC_TYPE",objGlAccountsInfo.ACC_CASH_ACC_TYPE);
				objDataWrapper.AddParameter("@ACC_DISP_NUMBER",objGlAccountsInfo.ACC_DISP_NUMBER);
				objDataWrapper.AddParameter("@ACC_PRODUCE_CHECK",objGlAccountsInfo.ACC_PRODUCE_CHECK);
				objDataWrapper.AddParameter("@ACC_HAS_CHILDREN",objGlAccountsInfo.ACC_HAS_CHILDREN);
				objDataWrapper.AddParameter("@ACC_NEST_LEVEL",objGlAccountsInfo.ACC_NEST_LEVEL);
				objDataWrapper.AddParameter("@ACC_CURRENT_BALANCE",objGlAccountsInfo.ACC_CURRENT_BALANCE);
				objDataWrapper.AddParameter("@IS_ACTIVE",objGlAccountsInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objGlAccountsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objGlAccountsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@ACC_RELATES_TO_TYPE",objGlAccountsInfo.ACC_RELATES_TO_TYPE);
				objDataWrapper.AddParameter("@BUDGET_CATEGORY_ID",objGlAccountsInfo.BUDGET_CATEGORY_ID);
				objDataWrapper.AddParameter("@WOLVERINE_USER_ID",objGlAccountsInfo.WOLVERINE_USER_ID);
				//objDataWrapper.AddParameter("@MODIFIED_BY",objGlAccountsInfo.MODIFIED_BY);
				//objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGlAccountsInfo.LAST_UPDATED_DATETIME);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ACCOUNT_ID",objGlAccountsInfo.ACCOUNT_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objGlAccountsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddGlAccountInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objGlAccountsInfo);
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='ACC_PARENT_ID' and @NewValue='0']","NewValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='ACC_PARENT_ID' and @OldValue='0']","OldValue","null");
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @NewValue='6']","NewValue","Asset");
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @NewValue='7']","NewValue","Liability");
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @NewValue='8']","NewValue","Equity");
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @NewValue='9']","NewValue","Income");
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @NewValue='10']","NewValue","Expense");
//					
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @OldValue='6']","OldValue","Asset");
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @OldValue='7']","OldValue","Liability");
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @OldValue='8']","OldValue","Equity");
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @OldValue='9']","OldValue","Income");
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @OldValue='10']","OldValue","Expense");	
			
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @OldValue='0']","OldValue","null");
					
					strTranXML = GlTransXML(strTranXML); //Modifeid
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objGlAccountsInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1729", "");// "GL Account Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int ACCOUNT_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (ACCOUNT_ID == -1)
				{
					return -1;
				}
				else
				{
					objGlAccountsInfo.ACCOUNT_ID = ACCOUNT_ID;
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
		/// <param name="objOldGlAccountsInfo">Model object having old information</param>
		/// <param name="objGlAccountsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsGlAccountsInfo objOldGlAccountsInfo,ClsGlAccountsInfo objGlAccountsInfo,string Category,string SubType,string ParentAccount)
		{
			string		strStoredProc	=	"Proc_UpdateACT_GL_ACCOUNTS";
			string strTranXML;
			int returnResult = 1;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@GL_ID",objGlAccountsInfo.GL_ID);
			//	objDataWrapper.AddParameter("@FISCAL_ID",objGlAccountsInfo.FISCAL_ID);
				objDataWrapper.AddParameter("@CATEGORY_TYPE",objGlAccountsInfo.CATEGORY_TYPE);
				objDataWrapper.AddParameter("@ACCOUNT_ID",objGlAccountsInfo.ACCOUNT_ID);
				objDataWrapper.AddParameter("@GROUP_TYPE",objGlAccountsInfo.GROUP_TYPE);
				objDataWrapper.AddParameter("@ACC_LEVEL_TYPE",objGlAccountsInfo.ACC_LEVEL_TYPE);				
				objDataWrapper.AddParameter("@ACC_TYPE_ID",objGlAccountsInfo.ACC_TYPE_ID);
				if(objGlAccountsInfo.ACC_PARENT_ID<=0)
					objDataWrapper.AddParameter("@ACC_PARENT_ID",DBNull.Value);
				else	
					objDataWrapper.AddParameter("@ACC_PARENT_ID",objGlAccountsInfo.ACC_PARENT_ID);

				objDataWrapper.AddParameter("@ACC_NUMBER",objGlAccountsInfo.ACC_NUMBER);				
				objDataWrapper.AddParameter("@ACC_DESCRIPTION",objGlAccountsInfo.ACC_DESCRIPTION);
				objDataWrapper.AddParameter("@ACC_TOTALS_LEVEL",objGlAccountsInfo.ACC_TOTALS_LEVEL);
				objDataWrapper.AddParameter("@ACC_JOURNAL_ENTRY",objGlAccountsInfo.ACC_JOURNAL_ENTRY);
				objDataWrapper.AddParameter("@ACC_CASH_ACCOUNT",objGlAccountsInfo.ACC_CASH_ACCOUNT);
				objDataWrapper.AddParameter("@ACC_CASH_ACC_TYPE",objGlAccountsInfo.ACC_CASH_ACC_TYPE);
				objDataWrapper.AddParameter("@ACC_DISP_NUMBER",objGlAccountsInfo.ACC_DISP_NUMBER);
				objDataWrapper.AddParameter("@ACC_PRODUCE_CHECK",objGlAccountsInfo.ACC_PRODUCE_CHECK);
				objDataWrapper.AddParameter("@ACC_HAS_CHILDREN",objGlAccountsInfo.ACC_HAS_CHILDREN);
				objDataWrapper.AddParameter("@ACC_NEST_LEVEL",objGlAccountsInfo.ACC_NEST_LEVEL);
				objDataWrapper.AddParameter("@ACC_CURRENT_BALANCE",objGlAccountsInfo.ACC_CURRENT_BALANCE);
				objDataWrapper.AddParameter("@ACC_RELATES_TO_TYPE",objGlAccountsInfo.ACC_RELATES_TO_TYPE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objGlAccountsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGlAccountsInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@BUDGET_CATEGORY_ID",objGlAccountsInfo.BUDGET_CATEGORY_ID);
				objDataWrapper.AddParameter("@WOLVERINE_USER_ID",objGlAccountsInfo.WOLVERINE_USER_ID);
				
				if(base.TransactionLogRequired) 
				{
					objGlAccountsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddGlAccountInformation.aspx.resx");
					string strUpdate = objBuilder.GetUpdateSQL(objOldGlAccountsInfo,objGlAccountsInfo,out strTranXML);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='ACC_PARENT_ID' and @NewValue='0']","NewValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='ACC_PARENT_ID' and @OldValue='0']","OldValue","null");
					
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @NewValue='0']","NewValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @OldValue='0']","OldValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @NewValue='6']","NewValue","Asset");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @NewValue='7']","NewValue","Liability");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @NewValue='8']","NewValue","Equity");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @NewValue='9']","NewValue","Income");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @NewValue='10']","NewValue","Expense");
					
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @OldValue='6']","OldValue","Asset");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @OldValue='7']","OldValue","Liability");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @OldValue='8']","OldValue","Equity");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @OldValue='9']","OldValue","Income");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='GROUP_TYPE' and @OldValue='10']","OldValue","Expense");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='BUDGET_CATEGORY_ID' and @NewValue='0']","NewValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='BUDGET_CATEGORY_ID' and @OldValue='0']","OldValue","null");

					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objGlAccountsInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1730", "");// "GL Account Has Been Updated";
					objTransactionInfo.CUSTOM_INFO      = "; Category :"+ Category;
					if(SubType!="")
					{
						objTransactionInfo.CUSTOM_INFO +="; Sub Type :" + SubType ;
					}
					objTransactionInfo.CUSTOM_INFO +="; Account Number:" + objGlAccountsInfo.ACC_NUMBER;
					if(ParentAccount!="")
					{
						objTransactionInfo.CUSTOM_INFO+="; Parent Account:" + ParentAccount;
					}
					
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
		#region Modify Transation XML 
		public string GlTransXML(string transXML)
		{
			System.Xml.XmlDocument transDoc = new System.Xml.XmlDocument();
            
			try
			{
				transDoc.LoadXml(transXML);
				System.Xml.XmlNode mapNodeACC_LEVEL_TYPE = transDoc.SelectSingleNode("//Map[@field='ACC_LEVEL_TYPE']");
				if(mapNodeACC_LEVEL_TYPE!=null)
				{
					XmlAttribute attr = mapNodeACC_LEVEL_TYPE.Attributes["NewValue"];
					if(attr.InnerText != "AS")
					{
						System.Xml.XmlNode mapNodeACC_TYPE_ID = transDoc.SelectSingleNode("//Map[@field='ACC_TYPE_ID']");
						transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeACC_TYPE_ID);                                              
					}
				}
			
				return transDoc.OuterXml;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{

			}		
            
		}
		#endregion

		#region "Delete"
		public int Delete(string AccountID)
		{
			string deleteProcName = "Proc_DeleteACT_GL_ACCOUNTS";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@ACCOUNT_ID",AccountID);
			return objDataWrapper.ExecuteNonQuery(deleteProcName);
		}
		#endregion
			
		#region "Get Xml Methods"
		//			public static string GetXmlForPageControls(string accountID)
		//			{
		//				string cashType="";
		//				return GetXmlForPageControls(accountID,ref cashType);
		//			}
		public static DataSet GetXmlForPageControls(string account_ID)
		{
			string strStoredProc = "Proc_GetGetXmlForGLAccounts";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@ACCOUNT_ID",account_ID);
			return objDataWrapper.ExecuteDataSet(strStoredProc);
		}


		#endregion


		#region "Fill Drop down Functions"
			
		public static void GetParentAcsInDropDown(DropDownList combo, string selectedValue)
		{
			string strStoredProc = "Proc_GetParentAccountsInDropDown";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0] ;
			combo.Items.Clear();
			combo.Items.Add(new ListItem("","0"));
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				combo.Items.Add(new ListItem(objDataTable.DefaultView[i]["ACC_DESCRIPTION"].ToString(),objDataTable.DefaultView[i]["ACCOUNT_ID"].ToString()));
				if(selectedValue!=null && selectedValue.Length>0 && selectedValue.Equals(objDataTable.DefaultView[i]["CATEGORY_ID"].ToString()))
					combo.SelectedIndex = i;
			}
		}

		public static void GetParentAcsInDropDown(DropDownList combo)
		{
			GetParentAcsInDropDown(combo,null);
		}
		// Get all Accounts in combo : Used in Distribut Cash Receipt
		public static void GetAllGLAccountsInDropDown(DropDownList combo)
		{
			string strStoredProc = "Proc_GetAllGLAccounts";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
			combo.Items.Clear();
			combo.Items.Add("");
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				combo.Items.Add(new ListItem(objDataTable.DefaultView[i]["ACC_DESCRIPTION"].ToString(),objDataTable.DefaultView[i]["ACCOUNT_ID"].ToString()));
			}
		}
		//Overload for Reins Check
		public static void GetAllGLAccountsInDropDownReins(DropDownList combo)
		{
			string strStoredProc = "Proc_GetGLAccounts_Reins";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
			combo.Items.Clear();
			combo.Items.Add("");
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				combo.Items.Add(new ListItem(objDataTable.DefaultView[i]["ACC_DESCRIPTION"].ToString(),objDataTable.DefaultView[i]["ACCOUNT_ID"].ToString()));
			}
		}

		public static void GetAccountsInDropDown(DropDownList combo, string GL_ID,string accountTypeID)
		{
			string strStoredProc = "Proc_GetGLAccountsInDropDown";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@GL_ID",GL_ID);
			objDataWrapper.AddParameter("@accountTypeID",accountTypeID);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
			combo.Items.Clear();
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				combo.Items.Add(new ListItem(objDataTable.DefaultView[i]["ACC_DESCRIPTION"].ToString(),objDataTable.DefaultView[i]["ACCOUNT_ID"].ToString()));
			}
            combo.Items.Insert(0, "");//Added by Pradeep Kushwaha to make empty of first index value on 30-August-2010 
		}

		public static void GetAccountsInDropDown(DropDownList[] comboArray, string GL_ID,string accountTypeID)
		{
			string strStoredProc = "Proc_GetGLAccountsInDropDown";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@GL_ID",GL_ID);
			objDataWrapper.AddParameter("@accountTypeID",accountTypeID);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
			for(int i=0;i<comboArray.Length;i++)
				comboArray[i].Items.Clear();
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				for(int j=0;j<comboArray.Length;j++)
					comboArray[j].Items.Add(new ListItem(objDataTable.DefaultView[i]["ACC_DESCRIPTION"].ToString(),objDataTable.DefaultView[i]["ACCOUNT_ID"].ToString()));
			}		
		}
		public static void GetAccountsInDropDown(DropDownList combo,string accountTypeID)
		{
			string strStoredProc = "Proc_GetAccountsInDropDown";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@accountTypeID",accountTypeID);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
			combo.Items.Clear();
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				combo.Items.Add(new ListItem(objDataTable.DefaultView[i]["ACC_DESCRIPTION"].ToString(),objDataTable.DefaultView[i]["ACCOUNT_ID"].ToString()));
			}
		}
		public static void GetAccountsInDropDown(DropDownList combo)
		{
			string strStoredProc = "Proc_GetAllAccountsInDropDown";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
			combo.Items.Clear();
			combo.Items.Add("");
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				combo.Items.Add(new ListItem(objDataTable.DefaultView[i]["ACC_DESCRIPTION"].ToString(),objDataTable.DefaultView[i]["ACCOUNT_ID"].ToString()));
			}
		}
		public static void GetCashAccountsInDropDown(DropDownList combo,int Relates_TO_Type)
		{
			string strStoredProc = "Proc_GetCashAccountsInDropDown";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@Relates_TO_Type",Relates_TO_Type);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
			combo.Items.Clear();
			combo.Items.Add("");
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				combo.Items.Add(new ListItem(objDataTable.DefaultView[i]["ACC_DESCRIPTION"].ToString(),objDataTable.DefaultView[i]["ACCOUNT_ID"].ToString()));
			}
		}
		public static void GetCashAccountsInDropDown(DropDownList combo)
		{
			GetCashAccountsInDropDown(combo,0);
		}

		public static void GetAccountsInDropdown(DropDownList objDropDown, int GL_ID, int FISCAL_ID)
		{
			string strStoredProc = "Proc_GetACT_GENERAL_LEDGER_ACCOUNTS_NOTIN_POSTING";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@GL_ID", GL_ID);
				objDataWrapper.AddParameter("@FISCAL_ID", FISCAL_ID);

				ds = objDataWrapper.ExecuteDataSet(strStoredProc);
				
				objDropDown.DataSource = ds.Tables[0];
				objDropDown.DataTextField = "ACC_DESCRIPTION";
				objDropDown.DataValueField = "ACCOUNT_ID";
				objDropDown.DataBind();

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
		public static void GetAccountsInDropdown(DropDownList objDropDown, int GL_ID, int FISCAL_ID,string ACC_JOURNAL_ENTRY)
		{
			string strStoredProc = "Proc_GetACT_GENERAL_LEDGER_ACCOUNTS_NOTIN_POSTING";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@GL_ID", GL_ID);
				objDataWrapper.AddParameter("@FISCAL_ID", FISCAL_ID);
				objDataWrapper.AddParameter("@ACC_JOURNAL_ENTRY", ACC_JOURNAL_ENTRY);

				ds = objDataWrapper.ExecuteDataSet(strStoredProc);
				
				objDropDown.DataSource = ds.Tables[0];
				objDropDown.DataTextField = "ACC_DESCRIPTION";
				objDropDown.DataValueField = "ACCOUNT_ID";
				objDropDown.DataBind();
				objDropDown.Items.Insert(0,"");
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

		public static DataSet GetCashAccountInfo(int FiscalId)
		{
			string strStoredProc = "Proc_GetACT_GENERAL_LEDGER_BANK_ACCOUNTS_INFO";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@FISCAL_ID", FiscalId);
				ds = objDataWrapper.ExecuteDataSet(strStoredProc);
				return ds;
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
		public static void GetAccountAttachmentsInDropDown(DropDownList combo,int ACCOUNT_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@ACCOUNT_ID",ACCOUNT_ID);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_GetAccountAttachments").Tables[0];
			combo.Items.Clear();
			combo.Items.Add("");
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				//combo.Items.Add(new ListItem(objDataTable.DefaultView[i]["ATTACH_FILE_DESC"].ToString(),objDataTable.DefaultView[i]["ATTACH_ID"].ToString()));
				if(combo.ID.Equals("cmbCHECKSIGN_1"))
					combo.Items.Add(new ListItem(objDataTable.DefaultView[i]["SIGN_FILE_1"].ToString()));
				else
					combo.Items.Add(new ListItem(objDataTable.DefaultView[i]["SIGN_FILE_2"].ToString()));
			}
		}

		// Gets Default Deposits Bank account
		public static string GetDefaultDepositsAcc()
		{
			string strSql = "SELECT TOP 1 BNK_DEPOSITS_DEFAULT_AC FROM ACT_GENERAL_LEDGER";
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			if (objDataSet.Tables[0].Rows.Count > 0)
			{
				return objDataSet.Tables[0].Rows[0][0].ToString();
			}
			else
			{
				return "";
			}
		}
	
		#endregion

		#region "Misc"

		public static string GetDepositNumberByAccountID(int intFiscalId, int intAccountID)
		{

            string strSql = " SELECT Convert(varchar,(IsNull( (SELECT Max(DEPOSIT_NUMBER)+ 1  "
                          + "FROM ACT_CURRENT_DEPOSITS WITH(NOLOCK) "
                          + "WHERE ACCOUNT_ID = BANK_INFO.BANK_ID), "
                          + "IsNull(BANK_INFO.STARTING_DEPOSIT_NUMBER,1)))) "
                          + "FROM  ACT_BANK_INFORMATION BANK_INFO WITH(NOLOCK)  "
                          + "WHERE BANK_INFO.BANK_ID=" + intAccountID; 
            //string strSql = "SELECT Convert(varchar,(IsNull((SELECT Max(DEPOSIT_NUMBER) + 1 FROM ACT_CURRENT_DEPOSITS WITH(NOLOCK) WHERE ACCOUNT_ID = GL_ACCOUNTS.ACCOUNT_ID), IsNull(BANK_INFO.STARTING_DEPOSIT_NUMBER,1))))"
            //    + " FROM "
            //    + " ACT_GL_ACCOUNTS GL_ACCOUNTS WITH(NOLOCK)"
            //    + " LEFT JOIN ACT_BANK_INFORMATION BANK_INFO WITH(NOLOCK) ON "
            //    + " GL_ACCOUNTS.ACCOUNT_ID = BANK_INFO.ACCOUNT_ID "
            //    + " WHERE Upper(ACC_CASH_ACCOUNT) = 'Y' "
            ////	+ " AND GL_ACCOUNTS.FISCAL_ID =" + intFiscalId.ToString() 
            //    + " AND GL_ACCOUNTS.ACCOUNT_ID =" + intAccountID;

			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);

			if (objDataSet.Tables[0].Rows.Count > 0)
			{
				return objDataSet.Tables[0].Rows[0][0].ToString();
			}
			else
			{
				return "";
			}
		}
		// Fetch account balance
		public static string GetAccountBalanceByAccountID(int intAccountID)
		{
			string strSql = " SELECT TOP 1 CURRENT_YTD_BALANCE "
						  + " FROM ACT_GENERAL_LEDGER_TOTALS "
						  + " WHERE ACCOUNT_ID = " + intAccountID
						  + " ORDER BY GL_TOTALS_ID DESC ";

			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);

			if (objDataSet.Tables[0].Rows.Count > 0)
			{
				return objDataSet.Tables[0].Rows[0][0].ToString();
			}
			else
			{
				return "";
			}
		}

		//used to display chart of a/c report
		public static DataSet GetChartOfAccounts()
		{
			string strSql = "Proc_PrintChartOfAccounts";
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			return objDataSet;
		}
		public static DataTable GetParentAcsGroups()
		{
			string strStoredProc = "Proc_GetParentAcsGroups";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			return objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
		}
		#endregion	

		public  DataSet GetAccountDetails(int frmSource,int toSource,string frmDate,string toDate,double accountNo,string updFrom,int lob,int state)
		{
			string strProc = "Proc_GetAccountDetails";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
			try
			{

				if(frmSource!=0)
					objDataWrapper.AddParameter("@FROMSOURCE",frmSource);
				else
					objDataWrapper.AddParameter("@FROMSOURCE",null);
				if(toSource!=0)
					objDataWrapper.AddParameter("@TOSOURCE",toSource);
				else
					objDataWrapper.AddParameter("@TOSOURCE",null);
				if(frmDate!="")
					objDataWrapper.AddParameter("@FROMDATE",frmDate);
				else
					objDataWrapper.AddParameter("@FROMDATE",null);
				if(toDate!="")
					objDataWrapper.AddParameter("@TODATE",toDate);
				else
					objDataWrapper.AddParameter("@TODATE",null);

				if(accountNo!=0)
					objDataWrapper.AddParameter("@ACCOUNT_ID",accountNo);
				else
					objDataWrapper.AddParameter("@ACCOUNT_ID",null);
				if(updFrom!="")
					objDataWrapper.AddParameter("@UPDATED_FROM",updFrom);
				else
					objDataWrapper.AddParameter("@UPDATED_FROM",null);
				if(state!=0)
					objDataWrapper.AddParameter("@STATE",state);
				else
					objDataWrapper.AddParameter("@STATE",null);
				if(lob!=0)
					objDataWrapper.AddParameter("@LOB",lob);
				else
					objDataWrapper.AddParameter("@LOB",null);

				objDataSet = objDataWrapper.ExecuteDataSet(strProc);

				if(objDataSet.Tables[0].Rows.Count >0)
					return objDataSet;
				else
					return null;
				
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				objDataSet.Dispose();
			}
		}

		public  DataSet GetEFTSweepHistoryDetails(string DateFromSpool,string DateToSpool, string DateFromSweep,string  DateToSweep,string EntityType)
		{
			string strProc = "Proc_GetEFTSweepHistory";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
			try
			{

				if(DateFromSpool!="")
					objDataWrapper.AddParameter("@FROM_DATE_SPOOL ",DateFromSpool);
				else
					objDataWrapper.AddParameter("@FROM_DATE_SPOOL ",null);
				
				if(DateToSpool!="")
					objDataWrapper.AddParameter("@TO_DATE_SPOOL",DateToSpool);
				else
					objDataWrapper.AddParameter("@TO_DATE_SPOOL",null);
				
				if(DateFromSweep!="")
					objDataWrapper.AddParameter("@FROM_DATE_SWEEP",DateFromSweep);
				else
					objDataWrapper.AddParameter("@FROM_DATE_SWEEP",null);
				
				if(DateToSweep!="")
					objDataWrapper.AddParameter("@TO_DATE_SWEEP",DateToSweep);
				else
					objDataWrapper.AddParameter("@TO_DATE_SWEEP",null);				
				
				if(EntityType!="")
					objDataWrapper.AddParameter("@ENTITY_TYPE",EntityType);
				else
					objDataWrapper.AddParameter("@ENTITY_TYPE",null);

				
				objDataSet = objDataWrapper.ExecuteDataSet(strProc);

				if(objDataSet.Tables[0].Rows.Count >0)
					return objDataSet;
				else
					return null;
				
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				objDataSet.Dispose();
			}
		}

        public static DataSet GetAccountInfo()
        {
            string strStoredProc = "Proc_Get_BANK_ACCOUNTS_INFO";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                ds = objDataWrapper.ExecuteDataSet(strStoredProc);
                return ds;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }
	}
}

