using System;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Maintenance;
using System.Web.UI.WebControls;

/*********************************************************************************
<Modified Date			: - Anshuman
<Modified By			: - June 08, 2005
<Purpose				: - transaction description modified


<Modified Date			: - Priya
<Modified By			: - June 16, 2005
<Purpose				: - Added Delete Button Functionality
**********************************************************************************/
namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsFinanceCompany.
	/// </summary>
	public class ClsFinanceCompany  : Cms.BusinessLayer.BlCommon.ClsCommon
	{
		private const string MNT_FINANCE_COMPANY_LIST	= "MNT_FINANCE_COMPANY_LIST";
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateFinanceCompany";
		private const string INSERT_STORED_PROC			=  "Proc_InsertFinanceCompany";
		private bool boolTransactionRequired			= true;

		public bool TransactionRequired
		{
			get
			{
				return boolTransactionRequired;
			}
			set
			{
				boolTransactionRequired=value;
			}
		}

		public ClsFinanceCompany()
		{
			base.strActivateDeactivateProcedure = ACTIVATE_DEACTIVATE_PROC;
		}

		
		#region Add parameter
		/// <summary>
		/// Add the parameters of proc from model object.
		/// </summary>
		/// <param name="objPriorPolicyInfo">Model class object.</param>
		/// <param name="objDatawrapper">Datawrapper class object.</param>
		/// <returns>void</returns>
		private void AddParameters(Cms.Model.Maintenance.ClsFinanceCompany objCompanyInfo, DataWrapper objDataWrapper,char InsertUpdate)
		{
			objDataWrapper.AddParameter("@CompanyCode",objCompanyInfo.CompanyCode );
			objDataWrapper.AddParameter("@CompanyName",objCompanyInfo.CompanyName);
			objDataWrapper.AddParameter("@Add1",objCompanyInfo.CompanyAdd1);
			objDataWrapper.AddParameter("@Add2",objCompanyInfo.CompanyAdd2);
			objDataWrapper.AddParameter("@City",objCompanyInfo.CompanyCity);
			objDataWrapper.AddParameter("@State",objCompanyInfo.CompanyState);
			objDataWrapper.AddParameter("@Zip",objCompanyInfo.CompanyZip);
			objDataWrapper.AddParameter("@Country",objCompanyInfo.CompanyCountry );
			objDataWrapper.AddParameter("@MainPhoneNo",objCompanyInfo.CompanyPhoneNo );
			objDataWrapper.AddParameter("@TollFreeNo",objCompanyInfo.CompanyTollFreeNo);
			objDataWrapper.AddParameter("@Extension",objCompanyInfo.CompanyExt);
			objDataWrapper.AddParameter("@Fax",objCompanyInfo.CompanyFax);
			objDataWrapper.AddParameter("@EMail",objCompanyInfo.CompanyEmail);
			objDataWrapper.AddParameter("@Website",objCompanyInfo.CompanyWebsite);
			objDataWrapper.AddParameter("@Mobile",objCompanyInfo.CompanyMobile);
			objDataWrapper.AddParameter("@Terms",objCompanyInfo.CompanyTerms);
			objDataWrapper.AddParameter("@TermsDesc",objCompanyInfo.CompanyTermsDescription);
			objDataWrapper.AddParameter("@Note",objCompanyInfo.CompanyNote);

			if (InsertUpdate == 'I')
			{
				objDataWrapper.AddParameter("@Created_By",objCompanyInfo.CREATED_BY);
				objDataWrapper.AddParameter("@Created_Date",objCompanyInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);

			}
			else
			{
				objDataWrapper.AddParameter("@Created_By",null);
				objDataWrapper.AddParameter("@Created_Date",null);
				objDataWrapper.AddParameter("@MODIFIED_BY",objCompanyInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objCompanyInfo.LAST_UPDATED_DATETIME);
			}
			objDataWrapper.AddParameter("@INSERTUPDATE",InsertUpdate.ToString());
		}
		#endregion

		#region Add

		/// <summary>
		/// Inserting Finance Company details
		/// </summary>
		/// <param name="objCompanyInfo">Modal Object for Finance Company</param>
		/// <returns></returns>
		public int Add(Cms.Model.Maintenance.ClsFinanceCompany objCompanyInfo)
		{
			// string		strStoredProc	=	"Proc_InsertFinanceCompany";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new 
				DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			try
			{

				AddParameters(objCompanyInfo,objDataWrapper,'I');
				System.Data.SqlClient.SqlParameter objParam = (System.Data.SqlClient.SqlParameter) objDataWrapper.AddParameter("@CompanyId",null ,SqlDbType.Int,ParameterDirection.Output);
				
				int returnResult = 0;

				if(TransactionLogRequired) 
				{
					//Mapping field and Lebel to maintain the transction log into the database.
					objCompanyInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cmsweb/Maintenance/AddFinanceCompany.aspx.resx");

					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objCompanyInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objCompanyInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1717", "");// "Finance company is added";	
					objTransactionInfo.CHANGE_XML		=	strTranXML;


					returnResult	=	objDataWrapper.ExecuteNonQuery(INSERT_STORED_PROC,objTransactionInfo);
				}
				else//if no transaction required
				{
					returnResult	=	objDataWrapper.ExecuteNonQuery(INSERT_STORED_PROC);
				}
			
				int COMPANY_ID		=	int.Parse(objParam.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if (COMPANY_ID == -1)
				{
					return -1;
				}
				else
				{
					objCompanyInfo.CompanyId = COMPANY_ID;
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
		/// Updates the form's modified value
		/// </summary>
		/// <param name="objOldCustomerInfo">model object having old information</param>
		/// <param name="objCustomerInfo">model object having new information(form control's value)</param>
		/// <returns>no. of rows updated (1 or 0)</returns>
		public int Update(Model.Maintenance.ClsFinanceCompany objOldCompanyInfo,Model.Maintenance.ClsFinanceCompany objCompanyInfo )
		{
			int returnResult = 0;

			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

			/*Adding parameters of stored proc */
			AddParameters(objCompanyInfo, objDataWrapper, 'U');
			objDataWrapper.AddParameter("@CompanyId",objOldCompanyInfo.CompanyId);
			SqlParameter paramRetVal = (SqlParameter) objDataWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);

			try
			{

				if(TransactionRequired) 
				{
					/*Maintening the transaction log*/
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					//string strUpdate, 
                        string strTranXML;

					objCompanyInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cmsweb/Maintenance/AddFinanceCompany.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldCompanyInfo, objCompanyInfo);
				
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.RECORDED_BY		=	objCompanyInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1718", "");// "Finance Company information is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objDataWrapper.ExecuteNonQuery(INSERT_STORED_PROC,objTransactionInfo);
					
				}
				else
				{
					objDataWrapper.ExecuteNonQuery(INSERT_STORED_PROC);
				}

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
			
			returnResult = Convert.ToInt32(paramRetVal.Value);

			return returnResult;

		}
		#endregion

		#region "Fill Drop down Functions"
//		public static void GetFinanceCompanyNamesInDropDown(DropDownList objDropDownList)
//		{
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
//			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillFinanceCompanyDropDown").Tables[0];
//			objDropDownList.DataSource = objDataTable;
//			objDropDownList.DataTextField = "COMPANY_NAME";
//			objDropDownList.DataValueField = "COMPANY_ID";
//			objDropDownList.DataBind();
//		}
		public static void GetFinanceCompanyNamesInDropDown(DropDownList objDropDownList, string selectedValue)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillFinanceCompanyDropDown").Tables[0];
			objDropDownList.Items.Clear();
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				objDropDownList.Items.Add(new ListItem(objDataTable.DefaultView[i]["COMPANY_NAME"].ToString(),objDataTable.DefaultView[i]["COMPANY_ID"].ToString()));
				if(selectedValue!=null && selectedValue.Length>0 && objDataTable.DefaultView[i]["COMPANY_ID"].ToString().Equals(selectedValue))
					objDropDownList.SelectedIndex = i;
			}
		}
        //Get data to bind coolite control - itrack - 1557
        public static void GetFinanceCompanyNamesInDropDown(ref DataTable dt)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            dt = objDataWrapper.ExecuteDataSet("Proc_FillFinanceCompanyDropDown").Tables[0];
            
        }
		public static void GetFinanceCompanyNamesInDropDown(DropDownList objDropDownList)
		{
			GetFinanceCompanyNamesInDropDown(objDropDownList,null);
		}
		public static string GetCompanyName(string COMPANY_ID)
		{
			return DataWrapper.ExecuteScalar(ConnStr,CommandType.Text,"select COMPANY_NAME as INDIVIDUAL_CONTACT_NAME from MNT_FINANCE_COMPANY_LIST where COMPANY_ID="+COMPANY_ID).ToString();
		}
		#endregion
		
		/// <summary>
		/// Returns a single Finance company record
		/// </summary>
		/// <param name="companyID"></param>
		/// <returns></returns>
		public DataSet GetFinanceCompanyByID(int companyID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			string	strStoredProc =	"Proc_GetFinanceCompanyByID";

			objDataWrapper.AddParameter("@COMPANY_ID",companyID);

			DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}

		#region Delete functions
		/// <summary>
		/// deletes the information passed in model object to database.
		/// </summary>
		public int Delete(int intCompanyID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			string	strStoredProc =	"Proc_DeleteCompany";
			objDataWrapper.AddParameter("@CompanyId",intCompanyID);
			int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
			return intResult;
		
		}
		#endregion

	}
}
