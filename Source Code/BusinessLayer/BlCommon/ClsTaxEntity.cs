using System;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data;
using System.Configuration;
using Cms.Model.Maintenance;
using System.Web.UI.WebControls;

/******************************************************************
<Modified Date			: - Anshuman
<Modified By			: - June 08, 2005
<Purpose				: - transaction description modified
*******************************************************************/
namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsTaxEntity.
	/// </summary>
	public class ClsTaxEntity:Cms.BusinessLayer.BlCommon.ClsCommon
	{
		private const string MNT_TAX_ENTITY_LIST		= "MNT_TAX_ENTITY_LIST";
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateTaxEntity";
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

		public ClsTaxEntity()
		{
			base.strActivateDeactivateProcedure = ACTIVATE_DEACTIVATE_PROC;
		}
		 
		#region "New approach"
		/// <summary>
		/// Inserting Tax Entity details
		/// </summary>
		/// <param name="objTaxEntityInfo">Modal Object for Tax Entity</param>
		/// <returns></returns>
		public int Add(Cms.Model.Maintenance.ClsTaxEntityInfo objTaxEntityInfo)
		{
			
			string		strStoredProc	=	"Proc_InsertTaxEntity";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			
			try
			{
				objDataWrapper.AddParameter("@Code",objTaxEntityInfo.TAX_CODE);
			    objDataWrapper.AddParameter("@Name",objTaxEntityInfo.TAX_NAME );
				objDataWrapper.AddParameter("@Add1",objTaxEntityInfo.TAX_ADDRESS1 );
				objDataWrapper.AddParameter("@Add2",objTaxEntityInfo.TAX_ADDRESS2 );
				objDataWrapper.AddParameter("@City",objTaxEntityInfo.TAX_CITY);
				objDataWrapper.AddParameter("@Country",objTaxEntityInfo.TAX_COUNTRY );
				objDataWrapper.AddParameter("@State",objTaxEntityInfo.TAX_STATE);
				objDataWrapper.AddParameter("@Zip",objTaxEntityInfo.TAX_ZIP);
				objDataWrapper.AddParameter("@Extension",objTaxEntityInfo.TAX_EXT );
				objDataWrapper.AddParameter("@Phone",objTaxEntityInfo.TAX_PHONE );
			    objDataWrapper.AddParameter("@Fax",objTaxEntityInfo.TAX_FAX );
			    objDataWrapper.AddParameter("@EMail",objTaxEntityInfo.TAX_EMAIL);
				objDataWrapper.AddParameter("@Website",objTaxEntityInfo.TAX_WEBSITE );
				objDataWrapper.AddParameter("@Created_By",objTaxEntityInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@Created_DateTime",objTaxEntityInfo.CREATED_DATETIME  );
				System.Data.SqlClient.SqlParameter objParam = (System.Data.SqlClient.SqlParameter) objDataWrapper.AddParameter("@TaxEntityId",null,SqlDbType.Int,ParameterDirection.Output);
				
				int returnResult = 0;
				
				//if transaction required
				if(TransactionLogRequired) 
				{
					
					objTaxEntityInfo.TransactLabel	=	ClsCommon.MapTransactionLabel("cmsWeb/Maintenance/AddTaxEntity.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objTaxEntityInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objTaxEntityInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Tax entity is added";	
					objTransactionInfo.CHANGE_XML		=	strTranXML;					
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);;
				}
				else
				{
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
								
				int TaxEntityId		=	int.Parse(objParam.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if (TaxEntityId == -1)
				{
					return -1;
				}
				else
				{
					objTaxEntityInfo.TAX_ID = TaxEntityId;
					return returnResult;
				}
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		
		// <summary>
		/// Updates the form's modified value
		/// </summary>
		/// <param name="objOldTaxEntityInfo">model object having old information</param>
		/// <param name="objTaxEntityInfo">model object having new information(form control's value)</param>
		/// <returns>no. of rows updated (1 or 0)</returns>
		public int Update(ClsTaxEntityInfo objOldTaxEntityInfo,ClsTaxEntityInfo objTaxEntityInfo)
		{
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			
			string		strStoredProc	=	"Proc_UpdateTaxEntity";

			string strTranXML;
			int returnResult = 0;
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objDataWrapper.AddParameter("@Tax_ID",objTaxEntityInfo.TAX_ID);
			objDataWrapper.AddParameter("@Code",objTaxEntityInfo.TAX_CODE);
			objDataWrapper.AddParameter("@Name",objTaxEntityInfo.TAX_NAME );
			objDataWrapper.AddParameter("@Add1",objTaxEntityInfo.TAX_ADDRESS1 );
			objDataWrapper.AddParameter("@Add2",objTaxEntityInfo.TAX_ADDRESS2 );
			objDataWrapper.AddParameter("@City",objTaxEntityInfo.TAX_CITY);
			objDataWrapper.AddParameter("@Country",objTaxEntityInfo.TAX_COUNTRY );
			objDataWrapper.AddParameter("@State",objTaxEntityInfo.TAX_STATE);
			objDataWrapper.AddParameter("@Zip",objTaxEntityInfo.TAX_ZIP);
			objDataWrapper.AddParameter("@Extension",objTaxEntityInfo.TAX_EXT );
			objDataWrapper.AddParameter("@Phone",objTaxEntityInfo.TAX_PHONE );
			objDataWrapper.AddParameter("@Fax",objTaxEntityInfo.TAX_FAX );
			objDataWrapper.AddParameter("@EMail",objTaxEntityInfo.TAX_EMAIL);
			objDataWrapper.AddParameter("@Website",objTaxEntityInfo.TAX_WEBSITE );
			objDataWrapper.AddParameter("@Modified_By",objTaxEntityInfo.CREATED_BY);
			System.Data.SqlClient.SqlParameter objParam = (System.Data.SqlClient.SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
			
		 strTranXML = objBuilder.GetTransactionLogXML(objOldTaxEntityInfo,objTaxEntityInfo);
				
				try
				{
					if(strTranXML != "" && TransactionRequired) 
					{  

						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.RECORDED_BY		=	objTaxEntityInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Tax entity is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

						objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						returnResult = 1;
					}
					else
					{
						objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
					
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					
					
				}
				catch(Exception ex)
				{
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
				
			returnResult = Convert.ToInt32(objParam.Value);
			
			return  returnResult;
		}
		
		#endregion

		#region "Fill Drop down Functions"
//		public static void GetTaxEntityInDropDown(DropDownList objDropDownList)
//		{
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
//			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillTaxEntityDropDown").Tables[0];
//			objDropDownList.DataSource = objDataTable;
//			objDropDownList.DataTextField = "TAX_NAME";
//			objDropDownList.DataValueField = "TAX_ID";
//			objDropDownList.DataBind();
//		}
		public static void GetTaxEntityInDropDown(DropDownList objDropDownList, string selectedValue)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillTaxEntityDropDown").Tables[0];
			objDropDownList.Items.Clear();
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				objDropDownList.Items.Add(new ListItem(objDataTable.DefaultView[i]["TAX_NAME"].ToString(),objDataTable.DefaultView[i]["TAX_ID"].ToString()));
				if(selectedValue!=null && selectedValue.Length>0 && objDataTable.DefaultView[i]["TAX_ID"].ToString().Equals(selectedValue))
					objDropDownList.SelectedIndex = i;
			}
		}
        //Get data to bind Coolite control - itrack- 1557
        public static void GetTaxEntityInDropDown(ref DataTable dt)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            dt = objDataWrapper.ExecuteDataSet("Proc_FillTaxEntityDropDown").Tables[0];
         
        }
		public static void GetTaxEntityInDropDown(DropDownList objDropDownList)
		{
			GetTaxEntityInDropDown(objDropDownList,null);
		}
		
		public static string GetTaxName(string TAX_ID)
		{
			return DataWrapper.ExecuteScalar(ConnStr,CommandType.Text,"select TAX_NAME from MNT_TAX_ENTITY_LIST where TAX_ID="+TAX_ID).ToString();
		}
		#endregion

		/// <summary>
		/// Returns a single Finance company record
		/// </summary>
		/// <param name="companyID"></param>
		/// <returns></returns>
		public DataSet GetTaxEntityByID(int taxID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			string	strStoredProc =	"Proc_GetTaxEntityByID";

			objDataWrapper.AddParameter("@TAX_ID",taxID);

			DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}

		#region Delete functions
		/// <summary>
		/// deletes the information passed in model object to database.
		/// </summary>
		public int Delete(int intTaxID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			string	strStoredProc =	"Proc_DeleteTaxEntity";
			objDataWrapper.AddParameter("@TaxId",intTaxID);
			int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
			return intResult;
		
		}
		#endregion
	}
}
