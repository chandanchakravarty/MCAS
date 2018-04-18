/******************************************************************************************
<Author				: -   Ashwani
<Start Date				: -	5/10/2005 10:30:35 AM
<End Date				: -	
<Description				: - 	Business layer class for the Add Division class.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 08, 2005
<Purpose				: - transaction description modified
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Maintenance;
using System.Web.UI.WebControls;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Business layer class for division.
	/// </summary>
	public class ClsDivision : Cms.BusinessLayer.BlCommon.ClsCommon
	{
		private const	string		MNT_DIV_LIST			=	"MNT_DIV_LIST";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		// private int _DIV_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateDivision";
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
		public ClsDivision()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objDivisionInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsDivisionInfo objDivisionInfo)
		{
			string		strStoredProc	=	"Proc_InsertDivision";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@DIV_CODE",objDivisionInfo.DIV_CODE);
				objDataWrapper.AddParameter("@DIV_NAME",objDivisionInfo.DIV_NAME);
				objDataWrapper.AddParameter("@DIV_ADD1",objDivisionInfo.DIV_ADD1);
				objDataWrapper.AddParameter("@DIV_ADD2",objDivisionInfo.DIV_ADD2);
				objDataWrapper.AddParameter("@DIV_CITY",objDivisionInfo.DIV_CITY);
				objDataWrapper.AddParameter("@DIV_STATE",objDivisionInfo.DIV_STATE);
				objDataWrapper.AddParameter("@DIV_ZIP",objDivisionInfo.DIV_ZIP);
				objDataWrapper.AddParameter("@DIV_COUNTRY",objDivisionInfo.DIV_COUNTRY);
				objDataWrapper.AddParameter("@DIV_PHONE",objDivisionInfo.DIV_PHONE);
				objDataWrapper.AddParameter("@DIV_EXT",objDivisionInfo.DIV_EXT);
				objDataWrapper.AddParameter("@DIV_FAX",objDivisionInfo.DIV_FAX);
				objDataWrapper.AddParameter("@DIV_EMAIL",objDivisionInfo.DIV_EMAIL);
				objDataWrapper.AddParameter("@CREATED_BY",objDivisionInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objDivisionInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@NAIC_CODE",objDivisionInfo.NAIC_CODE);
                objDataWrapper.AddParameter("@BRANCH_CODE", objDivisionInfo.BRANCH_CODE);
                objDataWrapper.AddParameter("@BRANCH_CNPJ", objDivisionInfo.BRANCH_CNPJ);
                objDataWrapper.AddParameter("@ACTIVITY", objDivisionInfo.ACTIVITY);
                if (objDivisionInfo.REG_ID_ISSUE_DATE != DateTime.MinValue)
                   objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", objDivisionInfo.REG_ID_ISSUE_DATE);
                else
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", System.DBNull.Value);

                objDataWrapper.AddParameter("@REG_ID_ISSUE", objDivisionInfo.REG_ID_ISSUE);

                if (objDivisionInfo.DATE_OF_BIRTH != DateTime.MinValue)
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", objDivisionInfo.DATE_OF_BIRTH);
                else
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", System.DBNull.Value);
              
                objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", objDivisionInfo.REGIONAL_IDENTIFICATION);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DIV_ID",objDivisionInfo.DIV_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objDivisionInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/adddivision.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objDivisionInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	196;
					objTransactionInfo.RECORDED_BY		=	objDivisionInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1605", "");// "New division is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO      = "; Division Name:" + objDivisionInfo.DIV_NAME +"<br>"+ "; Division Code:" + objDivisionInfo.DIV_CODE;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int DIV_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (DIV_ID == -1)
				{
					return -1;
				}
				else
				{
					objDivisionInfo.DIV_ID = DIV_ID;
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
		/// <param name="objOldDivisionInfo">Model object having old information</param>
		/// <param name="objDivisionInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsDivisionInfo objOldDivisionInfo,ClsDivisionInfo objDivisionInfo)
		{
			string strStoredProc ="Proc_UpdateDivision";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@DIV_CODE",objDivisionInfo.DIV_CODE);
				objDataWrapper.AddParameter("@DIV_NAME",objDivisionInfo.DIV_NAME);
				objDataWrapper.AddParameter("@DIV_ADD1",objDivisionInfo.DIV_ADD1);
				objDataWrapper.AddParameter("@DIV_ADD2",objDivisionInfo.DIV_ADD2);
				objDataWrapper.AddParameter("@DIV_CITY",objDivisionInfo.DIV_CITY);
				objDataWrapper.AddParameter("@DIV_STATE",objDivisionInfo.DIV_STATE);
				objDataWrapper.AddParameter("@DIV_ZIP",objDivisionInfo.DIV_ZIP);
				objDataWrapper.AddParameter("@DIV_COUNTRY",objDivisionInfo.DIV_COUNTRY);
				objDataWrapper.AddParameter("@DIV_PHONE",objDivisionInfo.DIV_PHONE);
				objDataWrapper.AddParameter("@DIV_EXT",objDivisionInfo.DIV_EXT);
				objDataWrapper.AddParameter("@DIV_FAX",objDivisionInfo.DIV_FAX);
				objDataWrapper.AddParameter("@DIV_EMAIL",objDivisionInfo.DIV_EMAIL);
				objDataWrapper.AddParameter("@IS_ACTIVE",objDivisionInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objDivisionInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@DIV_ID",objDivisionInfo.DIV_ID);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objDivisionInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@NAIC_CODE",objDivisionInfo.NAIC_CODE);
                objDataWrapper.AddParameter("@BRANCH_CODE", objDivisionInfo.BRANCH_CODE);
                objDataWrapper.AddParameter("@BRANCH_CNPJ", objDivisionInfo.BRANCH_CNPJ);
                objDataWrapper.AddParameter("@ACTIVITY", objDivisionInfo.ACTIVITY);
                if (objDivisionInfo.REG_ID_ISSUE_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", objDivisionInfo.REG_ID_ISSUE_DATE);
                else
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", System.DBNull.Value);

                objDataWrapper.AddParameter("@REG_ID_ISSUE", objDivisionInfo.REG_ID_ISSUE);
                if (objDivisionInfo.DATE_OF_BIRTH != DateTime.MinValue)
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", objDivisionInfo.DATE_OF_BIRTH);
                else
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", System.DBNull.Value);
                objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", objDivisionInfo.REGIONAL_IDENTIFICATION);

				if(TransactionLogRequired) 
				{
					objDivisionInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/adddivision.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldDivisionInfo,objDivisionInfo);
					objTransactionInfo.TRANS_TYPE_ID	=	197;
					objTransactionInfo.RECORDED_BY		=	objDivisionInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1606", "");// "Division is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO      = "; Division Name:" + objDivisionInfo.DIV_NAME +"<br>"+ "; Division Code:" + objDivisionInfo.DIV_CODE;
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

		#region GetDivisionXml
		public static string GetDivisionXml(int intDiv_ID,int Lang_ID)
		{
			//Customer
			string strStoredProc = "Proc_GetDivision";
			DataSet dsDivision = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@Div_ID",intDiv_ID);
                objDataWrapper.AddParameter("@LANG_ID", Lang_ID);
				dsDivision = objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsDivision.Tables[0].Rows.Count != 0)
				{
					return dsDivision.GetXml();
				}
				else
				{
					return "";
				}
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

		#region AssignDepartmentToDivision Page Functions
		/// <summary>
		/// This function is used to get all the active Divisions Id & Name
		/// Function is called from AssignDepartmentToDivision Page 
		/// </summary>
		/// 
		/// 
		/// <returns>Return DataSet </returns>
		public DataSet PopulateDivision()
		{
			string	strStoredProc = "Proc_DivisionsList";
			DataSet dsReceive=null;
			try
			{						
				System.Data.DataRow dr =null;
				dsReceive= Cms.DataLayer.DataWrapper.ExecuteDatasetTypedParams(ConnStr,strStoredProc, dr);
				return dsReceive;
			}
			catch(Exception ex)
			{
				throw(ex);
			}				
		}


		/// <summary>
		/// This function is used to get all the unassigned Departments Id & Name
		/// Function is called from AssignDepartmentToDivision Page 
		/// </summary>
		/// 
		/// 
		/// <returns>Return DataSet </returns>
		public DataSet PopulateUnassignedDepartment(int intDivisionID)
		{
			string	strStoredProc	=	"Proc_PopulateUnassignedDept";
			DataSet dsReceive		=	null;

			try
			{				
				Object[] objParam = new object[1];
				objParam[0] = intDivisionID ;
				dsReceive = Cms.DataLayer.DataWrapper.ExecuteDataset( ConnStr,strStoredProc,objParam); 
				return dsReceive;
			}
			catch(Exception ex)
			{
				throw(ex);
			}				

		}


		/// <summary>
		/// This function is used to get all the assigned Departments Id & Name
		/// Function is called from AssignDepartmentToDivision Page 
		/// </summary>
		/// 
		/// 
		/// <returns>Return DataSet </returns>
		public DataSet PopulateassignedDepartment(int intDivisionID)
		{
			string	strStoredProc	= "Proc_PopulateassignedDept";
			DataSet dsReceive		= null;

			try
			{	
				Object[] objParam = new object[2];
				objParam[0] = intDivisionID ;
				objParam[1] = 'Y' ;
				dsReceive = Cms.DataLayer.DataWrapper.ExecuteDataset( ConnStr,strStoredProc,objParam); 
				return dsReceive;
			}
			catch(Exception ex)
			{
				throw(ex);
			}				
		}
		#endregion
		
		#region Function GetXmlForDepartmentDropDown()
		/// <summary>
		/// This is used to Generate XML for Division and Department mapping and will called form Add Accounting Entity Page,
		///  this function will stop post back of page to reterive value of other drop down on selected index change
		/// </summary>
		/// <returns></returns>
		public static string GetXmlForDepartmentDropDown()
		{
			string strDeptXml="";
			string strSql = "Select DIV_ID from MNT_DIV_LIST order by DIV_ID;";
			strSql += "SELECT DIVDEPTLIST.DIV_ID,DIVDEPTLIST.DEPT_ID,MNT_DEPT_LIST.DEPT_NAME FROM MNT_DIV_DEPT_MAPPING DIVDEPTLIST INNER JOIN MNT_DEPT_LIST  ON MNT_DEPT_LIST.DEPT_ID= DIVDEPTLIST.DEPT_ID WHERE  MNT_DEPT_LIST.IS_ACTIVE='Y' ORDER BY DIVDEPTLIST.DIV_ID";
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			strDeptXml ="<deptxml>";
			for(int i=0;i<objDataSet.Tables[0].Rows.Count;i++)
			{
				strDeptXml +="<division idd=\""+objDataSet.Tables[0].Rows[i][0]+"\">";
				for(int j=0;j<objDataSet.Tables[1].Rows.Count;j++)
				{
					if(objDataSet.Tables[1].Rows[j][0].ToString().Equals(objDataSet.Tables[0].Rows[i][0].ToString()))
					{
						strDeptXml += "<dept idd=\""+objDataSet.Tables[1].Rows[j][1]+"\" deptname=\""+objDataSet.Tables[1].Rows[j][2]+"\">";
						strDeptXml += "</dept>";
					}
				}
				strDeptXml+="</division>";
			}
			strDeptXml +="</deptxml>";
			return strDeptXml;
		}
		#endregion

		#region "Fill Drop down Functions"
		public static void GetDivisionDropDown(DropDownList objDropDownList)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_DivisionsList").Tables[0];
			objDropDownList.DataSource = objDataTable;
			objDropDownList.DataTextField = "DIV_NAME";
			objDropDownList.DataValueField = "DIV_ID";
			objDropDownList.DataBind();
		}
		/// <summary>
		/// This function is used to fill Department drop down , on the basis of selected Division
		/// </summary>
		/// <param name="objDropDownList"></param>
		/// <param name="intDepartmentId"></param>
		/// <param name="strShowAll"></param>
		public static void GetDepartment(DropDownList objDropDownList,int intDivisionId,string strShowAll)
		{
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@DivisionId",intDivisionId);
			objDataWrapper.AddParameter("@ShowAll",strShowAll);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_PopulateassignedDept").Tables[0];
			objDropDownList.Items.Insert(0,"");
			objDropDownList.DataSource = objDataTable;
			objDropDownList.DataTextField = "DEPT_NAME";
			objDropDownList.DataValueField = "DEPT_ID";
			objDropDownList.DataBind();
		}
		public static void GetDefaultHierarchyDropDown(DropDownList objDropDownList)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillDefaultHierarchyDropDown").Tables[0];
			objDropDownList.DataSource = objDataTable;
			objDropDownList.DataTextField = "DIV_NAME DEPT_NAME PC_NAME";
			objDropDownList.DataValueField = "MYID";
			objDropDownList.DataBind();
		}

		//public bool Delete(int intDivisionId)
		//{
		//	return true;
		//}


		#endregion

		#region Save(int,int[]) Function
		/// <summary>
		/// This function is used to save the assigned/unassigned Departments 
		/// Function is called from AssignDepartmentToDivision Page 
		/// </summary>
		/// <param name="intDivisionID"></param>
		/// <param name="arrDeptId"></param>
		/// <returns>Return integer </returns>
		public int Save(int intDivisionID,int[] arrDeptId)
		{
			string	strStoredProc	= "Proc_AssignDeptToDiv";
			Cms.DataLayer.DataWrapper objDataWrapper = null;
			int intResult = 0;
			try
			{
			
				objDataWrapper = new Cms.DataLayer.DataWrapper(ConnStr,CommandType.Text,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				objDataWrapper.ExecuteNonQuery("DELETE FROM MNT_DIV_DEPT_MAPPING "
					+ " WHERE DIV_ID = " + intDivisionID );
				objDataWrapper.AddParameter("@DivisionId",intDivisionID);

				//Adding the parameters, values will be passed inside the loop
				objDataWrapper.AddParameter("@DeptId",0);
				
				for(int i=1;i<arrDeptId.Length;i++)
				{
					objDataWrapper.CommandType = CommandType.StoredProcedure;
					objDataWrapper.CommandParameters[1].Value =   arrDeptId[i];
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
		#endregion

		public int Delete(int intDivId,string customInfo,int modifiedBy)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			string	strStoredProc =	"Proc_DeleteDivision";
			objDataWrapper.AddParameter("@DIV_ID",intDivId);
			int intResult;	
			if(TransactionLogRequired) 
			{
				//ObjProfitCenterInfo.TransactLabel  = ClsCommon.MapTransactionLabel("cmsWeb/Maintenance/addProfitcenter.aspx.resx");
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				//strTranXML = objBuilder.GetTransactionLogXML(objOldAddProfitCenterInfo,ObjProfitCenterInfo);
				objTransactionInfo.TRANS_TYPE_ID	=	200;
				objTransactionInfo.RECORDED_BY		=	modifiedBy;
                objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1604", "");// "Division Info is deleted";				
				objTransactionInfo.CUSTOM_INFO      =    customInfo; 
				intResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

			}
			else
			{
				intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			return intResult;
		
		}
	}
}
