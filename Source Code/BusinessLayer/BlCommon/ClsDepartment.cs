/******************************************************************************************
<Author				: -   Ashwani Kumar
<Start Date				: -	5/9/2005 2:00:07 PM
<End Date				: -	
<Description				: - 	Shows the Department page 
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
//using Cms.CustomException;
using Cms.Model.Maintenance;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary Description for Department 
	/// </summary>
	public class ClsDepartment : Cms.BusinessLayer.BlCommon.ClsCommon
	{
		private const string		MNT_DEPT_LIST			=	"MNT_DEPT_LIST";
	
		#region Private Instance Variables
		private			bool		boolTransactionLog;
		// private int _DEPT_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateDept";
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
		public ClsDepartment()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objDepartmentInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(Cms.Model.Maintenance.ClsDepartmentInfo objDepartmentInfo)
		{
			string		strStoredProc	=	"Proc_InsertDept";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@DEPT_CODE",objDepartmentInfo.DEPT_CODE);
				objDataWrapper.AddParameter("@DEPT_NAME",objDepartmentInfo.DEPT_NAME);
				objDataWrapper.AddParameter("@DEPT_ADD1",objDepartmentInfo.DEPT_ADD1);
				objDataWrapper.AddParameter("@DEPT_ADD2",objDepartmentInfo.DEPT_ADD2);
				objDataWrapper.AddParameter("@DEPT_CITY",objDepartmentInfo.DEPT_CITY);
				objDataWrapper.AddParameter("@DEPT_STATE",objDepartmentInfo.DEPT_STATE);
				objDataWrapper.AddParameter("@DEPT_ZIP",objDepartmentInfo.DEPT_ZIP);
				objDataWrapper.AddParameter("@DEPT_COUNTRY",objDepartmentInfo.DEPT_COUNTRY);
				objDataWrapper.AddParameter("@DEPT_PHONE",objDepartmentInfo.DEPT_PHONE);
				objDataWrapper.AddParameter("@DEPT_EXT",objDepartmentInfo.DEPT_EXT);
				objDataWrapper.AddParameter("@DEPT_FAX",objDepartmentInfo.DEPT_FAX);
				objDataWrapper.AddParameter("@DEPT_EMAIL",objDepartmentInfo.DEPT_EMAIL);
				objDataWrapper.AddParameter("@CREATED_BY",objDepartmentInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objDepartmentInfo.CREATED_DATETIME);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DEPT_ID",objDepartmentInfo.DEPT_ID,SqlDbType.Int,ParameterDirection.Output);
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objDepartmentInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsWeb/Maintenance/AddDepartment.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objDepartmentInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	208;
					objTransactionInfo.RECORDED_BY		=	objDepartmentInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1607", "");// "New department is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO      = "; Department Name:" + objDepartmentInfo.DEPT_NAME +"<br>"+ "; Department Code:" + objDepartmentInfo.DEPT_CODE;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int DEPT_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (DEPT_ID == -1)
				{
					return -1;
				}
				else
				{
					objDepartmentInfo.DEPT_ID = DEPT_ID;
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
		/// <param name="objOldDepartmentInfo">Model object having old information</param>
		/// <param name="objDepartmentInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsDepartmentInfo objOldDepartmentInfo,ClsDepartmentInfo objDepartmentInfo)
		{
			string	strStoredProc	=	"Proc_UpdateDepartment";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@DEPT_CODE",objDepartmentInfo.DEPT_CODE);
				objDataWrapper.AddParameter("@DEPT_NAME",objDepartmentInfo.DEPT_NAME);
				objDataWrapper.AddParameter("@DEPT_ADD1",objDepartmentInfo.DEPT_ADD1);
				objDataWrapper.AddParameter("@DEPT_ADD2",objDepartmentInfo.DEPT_ADD2);
				objDataWrapper.AddParameter("@DEPT_CITY",objDepartmentInfo.DEPT_CITY);
				objDataWrapper.AddParameter("@DEPT_STATE",objDepartmentInfo.DEPT_STATE);
				objDataWrapper.AddParameter("@DEPT_ZIP",objDepartmentInfo.DEPT_ZIP);
				objDataWrapper.AddParameter("@DEPT_COUNTRY",objDepartmentInfo.DEPT_COUNTRY);
				objDataWrapper.AddParameter("@DEPT_PHONE",objDepartmentInfo.DEPT_PHONE);
				objDataWrapper.AddParameter("@DEPT_EXT",objDepartmentInfo.DEPT_EXT);
				objDataWrapper.AddParameter("@DEPT_FAX",objDepartmentInfo.DEPT_FAX);
				objDataWrapper.AddParameter("@DEPT_EMAIL",objDepartmentInfo.DEPT_EMAIL);
				objDataWrapper.AddParameter("@IS_ACTIVE",objDepartmentInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@DEPT_ID",objDepartmentInfo.DEPT_ID);
				//objDataWrapper.AddParameter("@CREATED_BY",objDepartmentInfo.CREATED_BY);
				//objDataWrapper.AddParameter("@CREATED_DATETIME",objDepartmentInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objDepartmentInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objDepartmentInfo.LAST_UPDATED_DATETIME);
			
				if(TransactionLogRequired) 
				{

					objDepartmentInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cmsweb/Maintenance/AddDepartment.aspx.resx");Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldDepartmentInfo,objDepartmentInfo);
					objTransactionInfo.TRANS_TYPE_ID	=	209;
					objTransactionInfo.RECORDED_BY		=	objDepartmentInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1608", "");// "Department is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO      = "; Department Name:" + objDepartmentInfo.DEPT_NAME +"<br>"+ "; Department Code:" + objDepartmentInfo.DEPT_CODE;
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

		#region AssignProfitCenterToDepartment Page Functions
		/// <summary>
		/// This function is used to get all the active Deptarment Id & Name
		/// Function is called from AssignDepartmentToDivision Page 
		/// </summary>
		/// 
		/// 
		/// <returns>Return DataSet </returns>
		public DataSet PopulatePC()
		{
			//This variable  strStoredProc used to store the name of the Stored Proc to be executed
			const string strStoredProc = "Proc_DeptList";
			//dsReceive is used to store the dataset returened by function
			DataSet dsReceive = null; 
			try
			{						
				System.Data.DataRow dr = null;
				dsReceive= Cms.DataLayer.DataWrapper.ExecuteDatasetTypedParams(ConnStr,strStoredProc, dr);
				return dsReceive;
			}
			catch(Exception ex)
			{
				throw(ex);
			}				
		}



		/// <summary>
		/// This function is used to get all the assigned profit centers  Id & Name
		/// Function is called from AssignProfitCenterToDepartment Page 
		/// </summary>
		/// <param name="intDeptID"></param>
		/// <returns>Return DataSet </returns>
        public DataSet PopulateassignedPC(int intDeptID)
		{
			//This variable  strStoredProc used to store the name of the Stored Proc to be executed
            const string strStoredProc = "Proc_PopulateAssignedPC";
			DataSet dsReceive = null;
			try
			{	
				Object[] objParam = new object[2];
				objParam[0] = intDeptID ;
				objParam[1] = 'Y';
             
				dsReceive = Cms.DataLayer.DataWrapper.ExecuteDataset( ConnStr,strStoredProc,objParam); 
				return dsReceive;
			}
			catch(Exception ex)
			{
				throw(ex);
			}				
		}


		/// <summary>
		/// This function is used to get all the unassigned Prifit Centers Id & Name
		/// Function is called from AssignProfitCenterToDepartment Page 
		/// </summary>
		/// <param name="intDeptID"></param>
		/// <returns>Return DataSet </returns>
		/// 
		public DataSet PopulateUnassignedPC(int intDeptID)
		{
			//This variable  strStoredProc used to store the name of the Stored Proc to be executed
			const string strStoredProc = "Proc_PopulateUnassignedPC";
			DataSet dsReceive = null;

			try
			{				
				Object[] objParam = new object[1];
				objParam[0] = intDeptID ;
				dsReceive = Cms.DataLayer.DataWrapper.ExecuteDataset( ConnStr,strStoredProc,objParam); 
				return dsReceive;
			}
			catch(Exception ex)
			{
				throw(ex);
			}				

		}

		/// <summary>
		/// This function is used to save the assigned/unassigned Departments 
		/// Function is called from AssignDepartmentToDivision Page 
		/// </summary>
		/// <param name="intDeptID"></param>
		/// <returns>Return DataSet </returns>		public int Save(int intDeptID,int[] arrPCId)
		/// 
		public int Save(int intDeptID,int[] arrPCId)
		{
			//This variable  strStoredProc used to store the name of the Stored Proc to be executed
			const string strStoredProc = "Proc_AssignPCToDept";
			Cms.DataLayer.DataWrapper objDataWrapper = null;
			//This returnResult is used store the integer value returned from DataLayer
			int intResult = 0;
			try
			{
			
				objDataWrapper = new Cms.DataLayer.DataWrapper(ConnStr,CommandType.Text,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				objDataWrapper.ExecuteNonQuery("DELETE FROM MNT_DEPT_PC_MAPPING "
					+ " WHERE DEPT_ID = " + intDeptID );
				objDataWrapper.AddParameter("@DeptId",intDeptID);
				objDataWrapper.AddParameter("@PCId",arrPCId[0]);
				for(int i=1;i<arrPCId.Length;i++)
				{
					objDataWrapper.CommandType = CommandType.StoredProcedure;
					objDataWrapper.CommandParameters[1].Value =   arrPCId[i];
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
		
		#region Function GetXmlForProfitCenterDropDown()

		/// <summary>
		/// This is used to Generate XML for Division and Department mapping and will called form Add Accounting Entity Page,
		///  this function will stop post back of page to reterive value of other drop down on selected index change
		/// </summary>
		/// <returns></returns>
		public static string GetXmlForProfitCenterDropDown()
		{
			string strDeptXml="";
			string strSql = "Select DEPT_ID from MNT_DEPT_LIST order by DEPT_ID;";
			strSql += "SELECT 	DEPTPCLIST.DEPT_ID, DEPTPCLIST.PC_ID,MNT_PROFIT_CENTER_LIST.PC_NAME FROM MNT_DEPT_PC_MAPPING DEPTPCLIST INNER JOIN MNT_PROFIT_CENTER_LIST  ON MNT_PROFIT_CENTER_LIST.PC_ID= DEPTPCLIST.PC_ID WHERE	MNT_PROFIT_CENTER_LIST.IS_ACTIVE='Y'ORDER BY PC_NAME ASC";
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			strDeptXml ="<profitxml>";
			for(int i=0;i<objDataSet.Tables[0].Rows.Count;i++)
			{
				strDeptXml +="<department idd=\""+objDataSet.Tables[0].Rows[i][0]+"\">";
				for(int j=0;j<objDataSet.Tables[1].Rows.Count;j++)
				{
					if(objDataSet.Tables[1].Rows[j][0].ToString().Equals(objDataSet.Tables[0].Rows[i][0].ToString()))
					{
						strDeptXml += "<pc idd=\""+objDataSet.Tables[1].Rows[j][1]+"\" pcname=\""+objDataSet.Tables[1].Rows[j][2]+"\">";
						strDeptXml += "</pc>";
					}
				}
				strDeptXml+="</department>";
			}
			strDeptXml +="</profitxml>";
			return strDeptXml;
		}

#endregion 

		
		#region GetDepartmentXml
			public static string GetDepartmentXml(int intDept_ID)
			{
				//Customer
				string strStoredProc = "Proc_GetDepartment";
				DataSet dsDepartment = new DataSet();
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				try
				{
					objDataWrapper.AddParameter("@Dept_ID",intDept_ID);
					dsDepartment = objDataWrapper.ExecuteDataSet(strStoredProc);
					if (dsDepartment.Tables[0].Rows.Count != 0)
					{
						return dsDepartment.GetXml();
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



		/// <summary>
		/// This function is used to delete the Departments 
		/// </summary>
		/// <param name="intDeptID"></param>
		/// <returns>Return true if delete successfull else false </returns>		public int Save(int intDeptID,int[] arrPCId)
		public bool Delete(int intDeptId)
		{
			return true;
		}


		public int DeleteDepartment(int intDeptId,string customInfo, int modifiedBy)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			string	strStoredProc =	"Proc_DeleteDepartment";
			objDataWrapper.AddParameter("@DEPT_ID",intDeptId);
			int intResult;	
			if(TransactionLogRequired) 
			{
				//ObjProfitCenterInfo.TransactLabel  = ClsCommon.MapTransactionLabel("cmsWeb/Maintenance/addProfitcenter.aspx.resx");
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				//strTranXML = objBuilder.GetTransactionLogXML(objOldAddProfitCenterInfo,ObjProfitCenterInfo);
				objTransactionInfo.TRANS_TYPE_ID	=	207;
				objTransactionInfo.RECORDED_BY		=	modifiedBy;
                objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1609", "");// "Department Info is deleted";				
				objTransactionInfo.CUSTOM_INFO      = customInfo; 
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
