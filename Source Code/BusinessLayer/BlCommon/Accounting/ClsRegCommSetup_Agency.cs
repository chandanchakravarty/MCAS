/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	5/30/2005 2:20:42 PM
<End Date				: -	
<Description			: - 	Business Logic for Regular Commission Setup - Agency.
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
	/// Business Logic for Regular Commission Setup - Agency.
	/// </summary>
	public class ClsRegCommSetup_Agency : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		ACT_REG_COMM_SETUP			=	"ACT_REG_COMM_SETUP";

#region Private Instance Variables
private			bool		boolTransactionLog;
private int _COMM_ID;
private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateACT_REG_COMM_SETUP";
private const string GET_COMMISSION_PROC	= "Proc_GetCommission";
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
		public ClsRegCommSetup_Agency()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		public ClsRegCommSetup_Agency(bool transactionLogRequired):this()
		{
			base.TransactionLogRequired = transactionLogRequired;
		}
#endregion

#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objRegCommSetup_AgencyInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsRegCommSetup_AgencyInfo objRegCommSetup_AgencyInfo)
		{
			string		strStoredProc	=	"Proc_InsertACT_REG_COMM_SETUP";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				if(objRegCommSetup_AgencyInfo.AGENCY_ID>0)
					objDataWrapper.AddParameter("@AGENCY_ID",objRegCommSetup_AgencyInfo.AGENCY_ID);
				else
					objDataWrapper.AddParameter("@AGENCY_ID",DBNull.Value);
				objDataWrapper.AddParameter("@COUNTRY_ID",objRegCommSetup_AgencyInfo.COUNTRY_ID);
				objDataWrapper.AddParameter("@COMMISSION_TYPE",objRegCommSetup_AgencyInfo.COMMISSION_TYPE);
				objDataWrapper.AddParameter("@STATE_ID",objRegCommSetup_AgencyInfo.STATE_ID);
				objDataWrapper.AddParameter("@LOB_ID",objRegCommSetup_AgencyInfo.LOB_ID);
				if(objRegCommSetup_AgencyInfo.SUB_LOB_ID>-1)
				{
					objDataWrapper.AddParameter("@SUB_LOB_ID",objRegCommSetup_AgencyInfo.SUB_LOB_ID);
					objDataWrapper.AddParameter("@CLASS_RISK",objRegCommSetup_AgencyInfo.CLASS_RISK);
					objDataWrapper.AddParameter("@TERM",objRegCommSetup_AgencyInfo.TERM);
				}
				else//in case policy inspection
				{
					objDataWrapper.AddParameter("@SUB_LOB_ID",DBNull.Value);
					objDataWrapper.AddParameter("@CLASS_RISK",DBNull.Value);
					objDataWrapper.AddParameter("@TERM",objRegCommSetup_AgencyInfo.TERM);
				}
				objDataWrapper.AddParameter("@AMOUNT_TYPE",objRegCommSetup_AgencyInfo.AMOUNT_TYPE);
				
				objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",objRegCommSetup_AgencyInfo.EFFECTIVE_FROM_DATE);
				objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",objRegCommSetup_AgencyInfo.EFFECTIVE_TO_DATE);
				objDataWrapper.AddParameter("@COMMISSION_PERCENT",objRegCommSetup_AgencyInfo.COMMISSION_PERCENT);
				objDataWrapper.AddParameter("@IS_ACTIVE",objRegCommSetup_AgencyInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objRegCommSetup_AgencyInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objRegCommSetup_AgencyInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objRegCommSetup_AgencyInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objRegCommSetup_AgencyInfo.LAST_UPDATED_DATETIME);
				if(objRegCommSetup_AgencyInfo.REMARKS != null && objRegCommSetup_AgencyInfo.REMARKS.Length>0)
					objDataWrapper.AddParameter("@REMARKS",objRegCommSetup_AgencyInfo.REMARKS);
				else
					objDataWrapper.AddParameter("@REMARKS",DBNull.Value);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@COMM_ID",objRegCommSetup_AgencyInfo.COMM_ID,SqlDbType.Int,ParameterDirection.Output);
				string commission_type ="";
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objRegCommSetup_AgencyInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddRegCommSetup_Agency.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objRegCommSetup_AgencyInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objRegCommSetup_AgencyInfo.CREATED_BY;
					commission_type = objRegCommSetup_AgencyInfo.COMMISSION_TYPE;
						switch(commission_type)
						{
							case "a" :
							case "A" :
								objTransactionInfo.TRANS_DESC		=	"New Additional Commission Setup - Agency Has Been Added";
								break;
							case "r" :
							case "R" :
								objTransactionInfo.TRANS_DESC		=	"New Regular Commission Setup - Agency Has Been Added";
								break;
							case "c" :
							case "C" :
								objTransactionInfo.TRANS_DESC		=	"New Complete App Bonus Has Been Added";
								break;
							case "p" :
							case "P" :
								objTransactionInfo.TRANS_DESC		=	"New Property Inspection Credit Has Been Added";
								break;
							default :
								objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
								break;
						}
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='SUB_LOB_ID' and @OldValue='0']","OldValue","All");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='SUB_LOB_ID' and @NewValue='0']","NewValue","All");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='TERM' and @OldValue='F']","OldValue","First Term(NBS)");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='TERM' and @NewValue='F']","NewValue","First Term(NBS)");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='TERM' and @OldValue='O']","OldValue","Other Term");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='TERM' and @NewValue='O']","NewValue","Other Term");
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int COMM_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (COMM_ID == -1 || COMM_ID == -2)
				{
					return COMM_ID;
				}
				else
				{
					objRegCommSetup_AgencyInfo.COMM_ID = COMM_ID;
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
/// <param name="objOldRegCommSetup_AgencyInfo">Model object having old information</param>
/// <param name="objRegCommSetup_AgencyInfo">Model object having new information(form control's value)</param>
/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsRegCommSetup_AgencyInfo objOldRegCommSetup_AgencyInfo,ClsRegCommSetup_AgencyInfo objRegCommSetup_AgencyInfo)
		{
			string		strStoredProc	=	"Proc_UpdateACT_REG_COMM_SETUP";
			string strTranXML="";
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				if(objRegCommSetup_AgencyInfo.AGENCY_ID>0)
					objDataWrapper.AddParameter("@AGENCY_ID",objRegCommSetup_AgencyInfo.AGENCY_ID);
				else
					objDataWrapper.AddParameter("@AGENCY_ID",DBNull.Value);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@COMM_ID",objRegCommSetup_AgencyInfo.COMM_ID,SqlDbType.Int,ParameterDirection.InputOutput);
				objDataWrapper.AddParameter("@COUNTRY_ID",objRegCommSetup_AgencyInfo.COUNTRY_ID);
				objDataWrapper.AddParameter("@STATE_ID",objRegCommSetup_AgencyInfo.STATE_ID);
				objDataWrapper.AddParameter("@LOB_ID",objRegCommSetup_AgencyInfo.LOB_ID);
				
				if(objRegCommSetup_AgencyInfo.SUB_LOB_ID>-1)
				{
					objDataWrapper.AddParameter("@SUB_LOB_ID",objRegCommSetup_AgencyInfo.SUB_LOB_ID);
					if(objRegCommSetup_AgencyInfo.SUB_LOB_ID>-1)
						objDataWrapper.AddParameter("@CLASS_RISK",objRegCommSetup_AgencyInfo.CLASS_RISK);
					else
						objDataWrapper.AddParameter("@CLASS_RISK",DBNull.Value);
					objDataWrapper.AddParameter("@TERM",objRegCommSetup_AgencyInfo.TERM);
				}
				else//in case policy inspection
				{
					objDataWrapper.AddParameter("@SUB_LOB_ID",DBNull.Value);
					objDataWrapper.AddParameter("@CLASS_RISK",DBNull.Value);
					objDataWrapper.AddParameter("@TERM",objRegCommSetup_AgencyInfo.TERM);
				}
				objDataWrapper.AddParameter("@AMOUNT_TYPE",objRegCommSetup_AgencyInfo.AMOUNT_TYPE);
				objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",objRegCommSetup_AgencyInfo.EFFECTIVE_FROM_DATE);
				objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",objRegCommSetup_AgencyInfo.EFFECTIVE_TO_DATE);
				objDataWrapper.AddParameter("@COMMISSION_PERCENT",objRegCommSetup_AgencyInfo.COMMISSION_PERCENT);
				if(objRegCommSetup_AgencyInfo.REMARKS != null && objRegCommSetup_AgencyInfo.REMARKS.Length>0)
					objDataWrapper.AddParameter("@REMARKS",objRegCommSetup_AgencyInfo.REMARKS);
				else
					objDataWrapper.AddParameter("@REMARKS",DBNull.Value);
				objDataWrapper.AddParameter("@MODIFIED_BY",objRegCommSetup_AgencyInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@COMMISSION_TYPE",objRegCommSetup_AgencyInfo.COMMISSION_TYPE);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objRegCommSetup_AgencyInfo.LAST_UPDATED_DATETIME);
				if(base.TransactionLogRequired) 
				{
					objRegCommSetup_AgencyInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddRegCommSetup_Agency.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldRegCommSetup_AgencyInfo,objRegCommSetup_AgencyInfo);
					
					string commission_type ="";	
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objRegCommSetup_AgencyInfo.MODIFIED_BY;
					commission_type = objRegCommSetup_AgencyInfo.COMMISSION_TYPE;
					switch(commission_type)
					{
						case "a" :
						case "A" :
							objTransactionInfo.TRANS_DESC		=	"Additional Commission Setup - Agency Has Been Updated";
							break;
						case "r" :
						case "R" :
							objTransactionInfo.TRANS_DESC		=	"Regular Commission Setup - Agency Has Been Updated";
							break;
						case "c" :
						case "C" :
							objTransactionInfo.TRANS_DESC		=	"Complete App Bonus Has Been Updated";
							break;
						case "p" :
						case "P" :
							objTransactionInfo.TRANS_DESC		=	"Property Inspection Credit Has Been Updated";
							break;
						default :
							objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
							break;
					}
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='SUB_LOB_ID' and @OldValue='0']","OldValue","All");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='SUB_LOB_ID' and @NewValue='0']","NewValue","All");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='TERM' and @OldValue='F']","OldValue","First Term(NBS)");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='TERM' and @NewValue='F']","NewValue","First Term(NBS)");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='TERM' and @OldValue='O']","OldValue","Other Term");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='TERM' and @NewValue='O']","NewValue","Other Term");
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int COMM_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if(returnResult <= 0)
					return COMM_ID;
				else
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
	public static string GetXmlForPageControls(string COMM_ID)
	{
        string strSql = "select t1.AGENCY_ID,t1.REMARKS,t1.COUNTRY_ID,t1.COMM_ID,t1.STATE_ID,t1.LOB_ID,t1.SUB_LOB_ID,t1.CLASS_RISK,t1.TERM,convert(varchar,t1.EFFECTIVE_FROM_DATE,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end) as EFFECTIVE_FROM_DATE,convert(varchar,t1.EFFECTIVE_TO_DATE,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end) as EFFECTIVE_TO_DATE,t1.COMMISSION_PERCENT,t1.IS_ACTIVE,t2.COUNTRY_NAME,t1.AMOUNT_TYPE";
		strSql += " from ACT_REG_COMM_SETUP t1,MNT_COUNTRY_LIST t2";
		strSql += " where  t1.COMM_ID="+COMM_ID+" and t1.COUNTRY_ID=t2.COUNTRY_ID";
		DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
		if(objDataSet.Tables[0].Rows.Count == 0)
			return "";
		else
			return objDataSet.GetXml();
	}

	public static string GetXmlForInflationPageControls(int stateid, int lobid, int zipcode, int inflationid)
	{
		string strSql = "SELECT INFLATION_ID,ZIP_CODE,STATE_ID,CONVERT(VARCHAR(20),EFFECTIVE_DATE,101) AS EFFECTIVE_DATE,CONVERT(VARCHAR(20),EXPIRY_DATE,101) AS EXPIRY_DATE,";
		strSql += "  FACTOR,LOB_ID,IS_ACTIVE";
		strSql += "  FROM INFLATION_COST_FACTORS";
		strSql += "  WHERE STATE_ID  = " +stateid;
		strSql += "  AND LOB_ID = " +lobid;
		strSql += "  AND ZIP_CODE = " +zipcode;
		strSql += "  AND INFLATION_ID = " +inflationid;
		DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
		if(objDataSet.Tables[0].Rows.Count == 0)
			return "";
		else
			return objDataSet.GetXml();
	}

#endregion

#region "Fill Drop down Functions"
//		public static void GetLOBInDropDown(DropDownList combo)
//		{
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
//			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("select LOB_ID,LOB_DESC from MNT_LOB_MASTER").Tables[0];
//			combo.Items.Clear();
//			combo.Items.Add(new ListItem("All","0"));
//			for(int i=0;i<objDataTable.DefaultView.Count;i++)
//			{
//				combo.Items.Add(new ListItem(objDataTable.DefaultView[i]["LOB_DESC"].ToString(),objDataTable.DefaultView[i]["LOB_ID"].ToString()));
//			}
//		}
//		public static void GetSUB_LOBInDropDown(DropDownList combo)
//		{
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
//			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("select SUB_LOB_ID,SUB_LOB_DESC from MNT_SUB_LOB_MASTER").Tables[0];
//			combo.Items.Clear();
//			combo.Items.Add(new ListItem("All","0"));
//			for(int i=0;i<objDataTable.DefaultView.Count;i++)
//			{
//				combo.Items.Add(new ListItem(objDataTable.DefaultView[i]["SUB_LOB_DESC"].ToString(),objDataTable.DefaultView[i]["SUB_LOB_ID"].ToString()));
//			}
//		}
		#endregion

		#region GetCommission
		public static DataTable GetCommission(string strCommissionType)
		{
			DataSet dsCommission = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@COMMISSION_TYPE", strCommissionType);
				dsCommission = objDataWrapper.ExecuteDataSet(GET_COMMISSION_PROC);
				return dsCommission.Tables[0];
				
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

#region Fetch Accounting Commission Function 
		public static DataTable  FetchCommissionClasses(int intState,int intLob, int intSubLob)
		{
			string		strStoredProc	=	"Proc_CommissionClasses";
			DataWrapper	objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@STATE_ID",intState);
			objDataWrapper.AddParameter("@LOB_ID",intLob);
			objDataWrapper.AddParameter("@SUB_LOB_ID",intSubLob);
			try
			{
				return objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
		#endregion 

		public int AddInflation(ClsInflationGuardSetup objInflationGuardInfo, out int InflationId)
		{
			string		strStoredProc	=	"PROC_INSERT_ACT_INFLATION_FACTOR";
			DateTime	RecordDate		=	DateTime.Now;
			InflationId=0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@STATE_ID",objInflationGuardInfo.STATE_ID);
				objDataWrapper.AddParameter("@ZIP_CODE",objInflationGuardInfo.ZIP_CODE);
				objDataWrapper.AddParameter("@LOB_ID",objInflationGuardInfo.LOB_ID);
				objDataWrapper.AddParameter("@EFFECTIVE_DATE",objInflationGuardInfo.EFFECTIVE_DATE);
				if(objInflationGuardInfo.EXPIRY_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@EXPIRY_DATE",objInflationGuardInfo.EXPIRY_DATE);
				objDataWrapper.AddParameter("@FACTOR",objInflationGuardInfo.FACTOR);
				objDataWrapper.AddParameter("@IS_ACTIVE",objInflationGuardInfo.IS_ACTIVE);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@INFLATION_ID", objInflationGuardInfo.INFLATION_ID,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlReturnParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN_VAL", -1,SqlDbType.Int,ParameterDirection.ReturnValue);
				int returnResult = 0;
				string strLOB ="";
				string strSTATE = "";
				if(objInflationGuardInfo.LOB_ID == 0 )
					strLOB = "Home & Rental";
				else if(objInflationGuardInfo.LOB_ID == 1)
					strLOB = "Home";
				else if(objInflationGuardInfo.LOB_ID ==6)
					strLOB ="Rental";
				
				if(objInflationGuardInfo.STATE_ID==14)
					strSTATE =  "Indiana";
				else if(objInflationGuardInfo.STATE_ID ==22)
					strSTATE = "Michigan";
				if(TransactionLogRequired)
				{
					objInflationGuardInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/InflationGuardDetails.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objInflationGuardInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objInflationGuardInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Inflation Guard information Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	";LOB = " + strLOB + "<br>Zip Code = " + objInflationGuardInfo.ZIP_CODE + "<br>State = " + strSTATE;	
		
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				if(objSqlReturnParameter!=null && objSqlReturnParameter.Value!=System.DBNull.Value && objSqlParameter.Value != System.DBNull.Value)
				{
					InflationId=int.Parse(objSqlParameter.Value.ToString()) ; 
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
					objDataWrapper.ClearParameteres();
					return int.Parse(objSqlParameter.Value.ToString());
				}
				else
				{
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return -1;
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


		public int UpdateInflation(ClsInflationGuardSetup objOldInflationGuardInfo,ClsInflationGuardSetup objInflationGuardInfo)
		{
			string		strStoredProc	=	"PROC_UPDATE_ACT_INFLATION_FACTOR";
			string strTranXML="";
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@INFLATION_ID",objInflationGuardInfo.INFLATION_ID);
				objDataWrapper.AddParameter("@STATE_ID",objInflationGuardInfo.STATE_ID);
				objDataWrapper.AddParameter("@ZIP_CODE",objInflationGuardInfo.ZIP_CODE);
				objDataWrapper.AddParameter("@LOB_ID",objInflationGuardInfo.LOB_ID);
				objDataWrapper.AddParameter("@EFFECTIVE_DATE",objInflationGuardInfo.EFFECTIVE_DATE);
				if(objInflationGuardInfo.EXPIRY_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@EXPIRY_DATE",objInflationGuardInfo.EXPIRY_DATE);				objDataWrapper.AddParameter("@FACTOR",objInflationGuardInfo.FACTOR);
				objDataWrapper.AddParameter("@OLD_STATE_ID",objOldInflationGuardInfo.STATE_ID);
				objDataWrapper.AddParameter("@OLD_ZIP_CODE",objOldInflationGuardInfo.ZIP_CODE);
				objDataWrapper.AddParameter("@OLD_LOB_ID",objOldInflationGuardInfo.LOB_ID);
				SqlParameter objSqlReturnParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN_VAL", -1,SqlDbType.Int,ParameterDirection.ReturnValue);
				string strLOB ="";
				string strSTATE = "";
				if(objOldInflationGuardInfo.LOB_ID == 0 )
					strLOB = "Home & Rental";
				else if(objOldInflationGuardInfo.LOB_ID == 1)
					strLOB = "Home";
				else if(objOldInflationGuardInfo.LOB_ID ==6)
					strLOB ="Rental";
				
				if(objOldInflationGuardInfo.STATE_ID==14)
					strSTATE =  "Indiana";
				else if(objOldInflationGuardInfo.STATE_ID ==22)
					strSTATE = "Michigan";

				if(base.TransactionLogRequired) 
				{
					objInflationGuardInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/InflationGuardDetails.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldInflationGuardInfo,objInflationGuardInfo);
					
						
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objInflationGuardInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Inflation Guard information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	";LOB = " + strLOB + "<br>Zip Code = " + objOldInflationGuardInfo.ZIP_CODE + "<br>State = " + strSTATE;	
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.ClearParameteres();
				
				if(objSqlReturnParameter!=null && objSqlReturnParameter.Value!=System.DBNull.Value)
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
					return int.Parse(objSqlReturnParameter.Value.ToString());
				}
				else
				{
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return -1;
				}
				//return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		
			finally
			{if(objDataWrapper != null) objDataWrapper.Dispose();}
		}

		public int DeleteInflation(ClsInflationGuardSetup objInflationGuardInfo)
		{
			string		strStoredProc	=	"PROC_DELETE_ACT_INFLATION_FACTOR";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@INFLATION_ID",objInflationGuardInfo.INFLATION_ID);
//				objDataWrapper.AddParameter("@STATE_ID",objInflationGuardInfo.STATE_ID);
//				objDataWrapper.AddParameter("@ZIP_CODE",objInflationGuardInfo.ZIP_CODE);
//				objDataWrapper.AddParameter("@LOB_ID",objInflationGuardInfo.LOB_ID);
				int returnResult = 0;
				string strLOB ="";
				string strSTATE = "";
				if(objInflationGuardInfo.LOB_ID == 0 )
					strLOB = "Home & Rental";
				else if(objInflationGuardInfo.LOB_ID == 1)
					strLOB = "Home";
				else if(objInflationGuardInfo.LOB_ID ==6)
					strLOB ="Rental";
				
				if(objInflationGuardInfo.STATE_ID==14)
					strSTATE =  "Indiana";
				else if(objInflationGuardInfo.STATE_ID ==22)
					strSTATE = "Michigan";

				if(TransactionLogRequired)
				{
					objInflationGuardInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/InflationGuardDetails.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objInflationGuardInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objInflationGuardInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Inflation Guard information Has Been Deleted";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	";LOB = " + strLOB + "<br>Zip Code = " + objInflationGuardInfo.ZIP_CODE + "<br>State = " + strSTATE;	
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
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}

}
}
