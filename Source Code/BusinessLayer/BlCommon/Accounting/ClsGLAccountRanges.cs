	/******************************************************************************************
	<Author					: -   Ajit Singh Chahal
	<Start Date				: -   5/11/2005 11:31:31 AM
	<End Date				: -	
	<Description			: -   Business Logic for GL Account Ranges.
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
		/// Business Logic for GL Account Ranges.
		/// </summary>
		public class ClsGLAccountRanges : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
		{
			private const	string		ACT_GL_ACCOUNT_RANGES			=	"ACT_GL_ACCOUNT_RANGES";

			#region Private Instance Variables
			private			bool		boolTransactionLog;
			// private int _CATEGORY_ID;
			private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateGLAccountRanges";
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
			public ClsGLAccountRanges()
			{
				boolTransactionLog	= base.TransactionLogRequired;
				base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
			}
			public ClsGLAccountRanges(bool transactionLogRequired):this()
			{
				base.TransactionLogRequired = transactionLogRequired;
			}
			#endregion

			#region Add(Insert) functions
			/// <summary>
			/// Saves the information passed in model object to database.
			/// </summary>
			/// <param name="objGLAccountRangesInfo">Model class object.</param>
			/// <returns>No of records effected.</returns>
			public int Add(ClsGLAccountRangesInfo objGLAccountRangesInfo)
			{
				string		strStoredProc	=	"Proc_InsertGL_SubRanges";
				DateTime	RecordDate		=	DateTime.Now;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				try
				{
					objDataWrapper.AddParameter("@PARENT_CATEGORY_ID",objGLAccountRangesInfo.PARENT_CATEGORY_ID);
					objDataWrapper.AddParameter("@CATEGORY_DESC",objGLAccountRangesInfo.CATEGORY_DESC);
					objDataWrapper.AddParameter("@RANGE_FROM",objGLAccountRangesInfo.RANGE_FROM);
					objDataWrapper.AddParameter("@RANGE_TO",objGLAccountRangesInfo.RANGE_TO);
					objDataWrapper.AddParameter("@IS_ACTIVE",objGLAccountRangesInfo.IS_ACTIVE);
					objDataWrapper.AddParameter("@CREATED_BY",objGLAccountRangesInfo.CREATED_BY);
					objDataWrapper.AddParameter("@CREATED_DATETIME",objGLAccountRangesInfo.CREATED_DATETIME);
					objDataWrapper.AddParameter("@MODIFIED_BY",objGLAccountRangesInfo.MODIFIED_BY);
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGLAccountRangesInfo.LAST_UPDATED_DATETIME);
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CATEGORY_ID",objGLAccountRangesInfo.CATEGORY_ID,SqlDbType.Int,ParameterDirection.Output);

					int returnResult = 0;
					if(TransactionLogRequired)
					{
						objGLAccountRangesInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddSubRanges.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTranXML = objBuilder.GetTransactionLogXML(objGLAccountRangesInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objGLAccountRangesInfo.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"Sub Ranges Has Been Added";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
					int CATEGORY_ID = int.Parse(objSqlParameter.Value.ToString());
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					if (CATEGORY_ID == -1)
					{
						return -1;
					}
					else
					{
						objGLAccountRangesInfo.CATEGORY_ID = CATEGORY_ID;
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
			/// <param name="objOldGLAccountRangesInfo">Model object having old information</param>
			/// <param name="objGLAccountRangesInfo">Model object having new information(form control's value)</param>
			/// <returns>No. of rows updated (1 or 0)</returns>
			public int Update(ClsGLAccountRangesInfo objOldGLAccountRangesInfo,ClsGLAccountRangesInfo objGLAccountRangesInfo,string MainType)
			{
				string strTranXML;
				string		strStoredProc	=	"Proc_UpdateGL_SubRanges";
				int returnResult = 1;
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				try 
				{
					objDataWrapper.AddParameter("@CATEGORY_ID",objGLAccountRangesInfo.CATEGORY_ID);
					objDataWrapper.AddParameter("@PARENT_CATEGORY_ID",objGLAccountRangesInfo.PARENT_CATEGORY_ID);
					objDataWrapper.AddParameter("@CATEGORY_DESC",objGLAccountRangesInfo.CATEGORY_DESC);
					objDataWrapper.AddParameter("@RANGE_FROM",objGLAccountRangesInfo.RANGE_FROM);
					objDataWrapper.AddParameter("@RANGE_TO",objGLAccountRangesInfo.RANGE_TO);
					objDataWrapper.AddParameter("@IS_ACTIVE",objGLAccountRangesInfo.IS_ACTIVE);
					objDataWrapper.AddParameter("@MODIFIED_BY",objGLAccountRangesInfo.MODIFIED_BY);
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGLAccountRangesInfo.LAST_UPDATED_DATETIME);
					SqlParameter retVal=(SqlParameter)objDataWrapper.AddParameter("",null,ParameterDirection.ReturnValue);
					if(base.TransactionLogRequired) 
					{
						objGLAccountRangesInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddSubRanges.aspx.resx");
						objBuilder.GetUpdateSQL(objOldGLAccountRangesInfo,objGLAccountRangesInfo,out strTranXML);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						if(strTranXML=="")
							return 1;
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objGLAccountRangesInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Sub Ranges Has Been Updated";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CUSTOM_INFO		=   "; Main Type:" + MainType +"; Sub Type:" + objGLAccountRangesInfo.CATEGORY_DESC;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

					}
					else
					{
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
					if(retVal!=null && retVal.Value.ToString().Equals("-2"))
					{
						return -2;//implies that range can not be modified as accounts are defined in range.
					}
					else
					{
						objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
						return returnResult;
					}					
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
			/// <summary> //multiple updates
			/// Update method that recieves Model object to save.
			/// </summary>
			/// <param name="objOldGLAccountRangesInfo">Model object having old information</param>
			/// <param name="objGLAccountRangesInfo">Model object having new information(form control's value)</param>
			/// <returns>No. of rows updated (1 or 0)</returns>
			public int Update(ClsGLAccountRangesInfo[] objOldGLAccountRangesInfo,ClsGLAccountRangesInfo[] objGLAccountRangesInfo)
			{
				string strTranXML;
				string		strStoredProc	=	"Proc_UpdateGLAccountRanges";
				int returnResult = 1;
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				try 
				{
					for(int i=0;i<objGLAccountRangesInfo.Length;i++)
					{										  
						
						objDataWrapper.ClearParameteres();
					
						objDataWrapper.AddParameter("@CATEGORY_ID",objGLAccountRangesInfo[i].CATEGORY_ID);
						objDataWrapper.AddParameter("@RANGE_FROM",objGLAccountRangesInfo[i].RANGE_FROM);
						objDataWrapper.AddParameter("@RANGE_TO",objGLAccountRangesInfo[i].RANGE_TO);
					
						objDataWrapper.AddParameter("@MODIFIED_BY",objGLAccountRangesInfo[i].MODIFIED_BY);
						objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGLAccountRangesInfo[i].LAST_UPDATED_DATETIME);
						SqlParameter retVal=(SqlParameter)objDataWrapper.AddParameter("",null,ParameterDirection.ReturnValue);
						if(base.TransactionLogRequired) 
						{
							objGLAccountRangesInfo[i].TransactLabel =		ClsCommon.MapTransactionLabel("/cmsweb/accounting/ChartOfAccountsRange.aspx.resx");
							objBuilder.GetUpdateSQL(objOldGLAccountRangesInfo[i],objGLAccountRangesInfo[i],out strTranXML);
							if(strTranXML == "")
								continue;
							Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

							objTransactionInfo.TRANS_TYPE_ID	 =		3;
							objTransactionInfo.RECORDED_BY		 =		objGLAccountRangesInfo[i].MODIFIED_BY;
							objTransactionInfo.TRANS_DESC		 =		"Chart of Account Ranges Has Been Updated";
							objTransactionInfo.CHANGE_XML		 =		strTranXML;
							int intCgtType =i;
							string strCATEGORY_DESC ="";
							switch(intCgtType)
							{
								case 0:
									strCATEGORY_DESC ="Asset";
									break;
								case 1:
									strCATEGORY_DESC ="Liability";
									break;
								case 2:
									strCATEGORY_DESC ="Equity";
									break;
								case 3:
									strCATEGORY_DESC ="Income";
									break;
								case 4:
									strCATEGORY_DESC ="Expense";
									break;								
							}
							objTransactionInfo.CUSTOM_INFO =  "Main Type:" + strCATEGORY_DESC; 
							returnResult						 =		objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

						}
						else
						{
							returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
							if(returnResult==0 || returnResult==-1)
								break;
						}
					}
					
					if(!(returnResult==0 || returnResult==-1))
						objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
						
					
					//return returnResult;
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

			#region "Delete sub ranges"
			public int DeleteSubRanges(string CATEGORY_ID)
			{
				string deleteProcName = "Proc_DeleteACT_GL_ACCOUNT_RANGES";
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CATEGORY_ID",CATEGORY_ID);
				return objDataWrapper.ExecuteNonQuery(deleteProcName);
			}
			#endregion

			#region "GetData"
			public static DataTable GetData()
			{
				string strSql = "select CATEGORY_ID,CATEGORY_DESC,RANGE_FROM,RANGE_TO from " + ACT_GL_ACCOUNT_RANGES;
				strSql += " where Parent_category_id is null";
				DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
				return objDataSet.Tables[0];
			}

			#endregion
	
			#region "Get Xml Methods"
			public static string GetXmlForPageControls(string CATEGORY_ID)
			{
				string strSql = "select CATEGORY_ID,PARENT_CATEGORY_ID,CATEGORY_DESC,RANGE_FROM,RANGE_TO,IS_ACTIVE";
				strSql += ",substring(convert(varchar,RANGE_FROM),1,charindex('.',convert(varchar,RANGE_FROM),1)-1) as wholeRangeFrom,substring(convert(varchar,RANGE_FROM),charindex('.',convert(varchar,RANGE_FROM),1)+1,len(convert(varchar,RANGE_FROM))) as fracRangeFrom";
				strSql += ",substring(convert(varchar,RANGE_TO),1,charindex('.',convert(varchar,RANGE_TO),1)-1) as wholeRangeTO,substring(convert(varchar,RANGE_TO),charindex('.',convert(varchar,RANGE_TO),1)+1,len(convert(varchar,RANGE_TO))) as fracRangeTo";
                strSql += " from ACT_GL_ACCOUNT_RANGES";
				strSql += " where PARENT_CATEGORY_ID is not null and CATEGORY_ID="+CATEGORY_ID;
				DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
				return objDataSet.GetXml();
			}
			#endregion
			#region "Fill Drop down Functions"
			public static void GetParentCategoriesInDropDown(DropDownList objDropDownList, string selectedValue)
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
                objDataWrapper.AddParameter("@LANG_ID", BlCommon.ClsCommon.BL_LANG_ID);
                DataTable objDataTable = objDataWrapper.ExecuteDataSet("select ACC_TYPE_ID,case @LANG_ID when 2 then case when ACC_TYPE_DESC='Asset' then 'Ativos' else case when ACC_TYPE_DESC='Liability' then 'Responsabilidade' else case when ACC_TYPE_DESC='Equity' then 'Capital' else case when ACC_TYPE_DESC='Income' then 'Renda' else case when ACC_TYPE_DESC='Expense' then 'Despesa' end  end  end end end Else ACC_TYPE_DESC end as ACC_TYPE_DESC     from ACT_TYPE_MASTER").Tables[0];
				objDropDownList.Items.Clear();
				objDropDownList.Items.Add("");
				for(int i=0;i<objDataTable.DefaultView.Count;i++)
				{
					objDropDownList.Items.Add(new ListItem(objDataTable.DefaultView[i]["ACC_TYPE_DESC"].ToString(),objDataTable.DefaultView[i]["ACC_TYPE_ID"].ToString()));
					if(selectedValue!=null && selectedValue.Length>0 && objDataTable.DefaultView[i]["ACC_TYPE_ID"].ToString().Equals(selectedValue))
						objDropDownList.SelectedIndex = i;
				}
			}
			public static void GetParentCategoriesInDropDown(DropDownList objDropDownList)
			{
				GetParentCategoriesInDropDown(objDropDownList,null);
			}
			#endregion

			#region "Misc"
			public static DataTable GetGroups()
			{
				string strSql = "select ACC_TYPE_ID,ACC_TYPE_DESC from ACT_TYPE_MASTER";
				return DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql).Tables[0];
			}
			public static DataSet GetAccountRanges()
			{
				//main type ranges
				string strSql = "select CATEGORY_ID,range_from ,range_to";
				strSql += " from ACT_GL_ACCOUNT_RANGES"; 
				strSql += " where PARENT_CATEGORY_ID is null";
				strSql += " order by CATEGORY_ID;";
				
				//sub group type ranges
				for(int i=1;i<=5;i++)
				{
					strSql += "select PARENT_CATEGORY_ID,Range_From,RANGE_to,CATEGORY_DESC";
					strSql += " from ACT_GL_ACCOUNT_RANGES"; 
					strSql += " where PARENT_CATEGORY_ID ="+i.ToString();
					strSql += " order by PARENT_CATEGORY_ID;";
				}
			

				return DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			}
			public static DataTable GetSubRanges()
			{
				string sql = "select ACC_TYPE_DESC as 'Parent Type',CATEGORY_DESC as 'Type',RANGE_FROM as 'From',RANGE_TO as 'To' from ACT_GL_ACCOUNT_RANGES a,ACT_TYPE_MASTER b where is_active='Y' and a.PARENT_CATEGORY_ID = b.ACC_TYPE_ID order by ACC_TYPE_DESC asc";
				return DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,sql).Tables[0];
			}
			#endregion
			#region "GL Accouts Functions"
			public static void GetSubGroupsInDropDown(DropDownList combo, string selectedValue)
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
				DataTable  objDataTable = objDataWrapper.ExecuteDataSet("select CATEGORY_ID,CATEGORY_DESC from ACT_GL_ACCOUNT_RANGES where is_active='Y' and PARENT_CATEGORY_ID is not null order by CATEGORY_DESC").Tables[0];
				combo.Items.Clear();
				combo.Items.Add("");
				for(int i=0;i<objDataTable.DefaultView.Count;i++)
				{
					combo.Items.Add(new ListItem(objDataTable.DefaultView[i]["CATEGORY_DESC"].ToString(),objDataTable.DefaultView[i]["CATEGORY_ID"].ToString()));
					if(selectedValue!=null && selectedValue.Length>0 && selectedValue.Equals(objDataTable.DefaultView[i]["CATEGORY_ID"].ToString()))
						combo.SelectedIndex = i;
				}
			}
			public static void GetSubGroupsInDropDown(DropDownList combo)
			{
				GetSubGroupsInDropDown(combo,null);
			}
			public static DataTable GetSubRangesForAccounts()
			{
				string sql = "select CATEGORY_ID ,RANGE_FROM ,RANGE_TO,PARENT_CATEGORY_ID  from ACT_GL_ACCOUNT_RANGES where is_active='Y' and PARENT_CATEGORY_ID is not null  order by CATEGORY_DESC";
				return DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,sql).Tables[0];
			}
			#endregion
		}
	}
