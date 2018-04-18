using System;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data;
using System.Configuration;
using Cms.Model.Maintenance;
using System.Web.UI.WebControls;

/*******************************************************************************
<Modified Date			: - Anshuman
<Modified By			: - June 08, 2005
<Purpose				: - transaction description modified
*******************************************************************************/
namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsMortgage.
	/// </summary>
	public class ClsMortgage:Cms.BusinessLayer.BlCommon.ClsCommon
	{

		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateMortgage";
		private bool boolTransactionRequired			= true;
		private const string INSERT_PROC	= "Proc_InsertHolder";

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

		public ClsMortgage()
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
		private void AddParameters(Cms.Model.Maintenance.ClsHolderInfo objHolderInfo, DataWrapper objDataWrapper,char InsertUpdate)
		{
			objDataWrapper.AddParameter("@HolderCode",objHolderInfo.HOLDER_CODE );
			objDataWrapper.AddParameter("@HolderName",objHolderInfo.HOLDER_NAME);
			objDataWrapper.AddParameter("@Add1",objHolderInfo.HOLDER_ADD1);
			objDataWrapper.AddParameter("@Add2",objHolderInfo.HOLDER_ADD2);
			objDataWrapper.AddParameter("@City",objHolderInfo.HOLDER_CITY);
			objDataWrapper.AddParameter("@State",objHolderInfo.HOLDER_STATE);
			objDataWrapper.AddParameter("@Zip",objHolderInfo.HOLDER_ZIP);
			objDataWrapper.AddParameter("@Country",objHolderInfo.HOLDER_COUNTRY );
			objDataWrapper.AddParameter("@MainPhoneNo",objHolderInfo.HOLDER_MAIN_PHONE_NO );
			objDataWrapper.AddParameter("@Extension",objHolderInfo.HOLDER_EXT );
			objDataWrapper.AddParameter("@Mobile",objHolderInfo.HOLDER_MOBILE);
			objDataWrapper.AddParameter("@Fax",objHolderInfo.HOLDER_FAX);
			objDataWrapper.AddParameter("@EMail",objHolderInfo.HOLDER_EMAIL);
			objDataWrapper.AddParameter("@LegalEntity",objHolderInfo.HOLDER_LEGAL_ENTITY);
			objDataWrapper.AddParameter("@Type",objHolderInfo.HOLDER_TYPE);
			objDataWrapper.AddParameter("@Memo",objHolderInfo.HOLDER_MEMO);

			if (InsertUpdate == 'I')
			{
				objDataWrapper.AddParameter("@Created_By",objHolderInfo.CREATED_BY);
				objDataWrapper.AddParameter("@Created_Date",objHolderInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);

			}
			else
			{
				objDataWrapper.AddParameter("@Created_By",null);
				objDataWrapper.AddParameter("@Created_Date",null);
				objDataWrapper.AddParameter("@MODIFIED_BY",objHolderInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objHolderInfo.LAST_UPDATED_DATETIME);
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
		public int Add(Cms.Model.Maintenance.ClsHolderInfo objHolderInfo)
		{
			
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new 
				DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			try
			{
				AddParameters(objHolderInfo, objDataWrapper, 'I');
				System.Data.SqlClient.SqlParameter objParam =(System.Data.SqlClient.SqlParameter)objDataWrapper.AddParameter("@HolderId",null ,SqlDbType.Int,ParameterDirection.Output);
				
				int returnResult = 0;

				if(TransactionLogRequired) 
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objHolderInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objHolderInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Additional Interest is added";	
					objTransactionInfo.CHANGE_XML		=	strTranXML;


					objDataWrapper.ExecuteNonQuery(INSERT_PROC,objTransactionInfo);
					returnResult	=	1;
				}
				else//if no transaction required
				{
					returnResult	=	objDataWrapper.ExecuteNonQuery(INSERT_PROC);
				}

								
				int HOLDER_ID		=	int.Parse(objParam.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if (HOLDER_ID == -1)
				{
					return -1;
				}
				else
				{
					objHolderInfo.HOLDER_ID = HOLDER_ID;
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
		public int Update(Model.Maintenance.ClsHolderInfo objOldHolderInfo,Model.Maintenance.ClsHolderInfo objHolderInfo )
		{
			int returnResult = 0;

			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				AddParameters(objHolderInfo, objDataWrapper, 'U');
				objDataWrapper.AddParameter("@HolderId", objOldHolderInfo.HOLDER_ID);

				if(TransactionRequired) 
				{

					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();	
					string strTranXML;

					//Mapping field and Lebel to maintain the transction log into the database.
					objHolderInfo.TransactLabel = MapTransactionLabel("Cmsweb/Maintenance/AddMortgage.aspx.resx");

					strTranXML = objBuilder.GetTransactionLogXML(objOldHolderInfo,objHolderInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.RECORDED_BY		=	objHolderInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Additional Interest is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					returnResult = objDataWrapper.ExecuteNonQuery(INSERT_PROC,objTransactionInfo);
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(INSERT_PROC);
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
			}
			
		}
		#endregion

		#region "Fill Drop down Functions"
//		public static void GetHolderNamesInDropDown(DropDownList objDropDownList)
//		{
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
//			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillHolderDropDown").Tables[0];
//			objDropDownList.DataSource = objDataTable;
//			objDropDownList.DataTextField = "HOLDER_NAME";
//			objDropDownList.DataValueField = "HOLDER_ID";
//			objDropDownList.DataBind();
//		}
		public static void GetHolderNamesInDropDown(DropDownList objDropDownList, string selectedValue)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillHolderDropDown").Tables[0];
			objDropDownList.Items.Clear();
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				objDropDownList.Items.Add(new ListItem(objDataTable.DefaultView[i]["HOLDER_NAME"].ToString(),objDataTable.DefaultView[i]["HOLDER_ID"].ToString()));
				if(selectedValue!=null && selectedValue.Length>0 && objDataTable.DefaultView[i]["HOLDER_ID"].ToString().Equals(selectedValue))
					objDropDownList.SelectedIndex = i;
			}
		}
        //Get data to bind coolite combobox control - itrak - 1557
        public static void GetHolderNamesInDropDown(ref DataTable dt)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            dt = objDataWrapper.ExecuteDataSet("Proc_FillHolderDropDown").Tables[0];
             
        }
		public static void GetHolderNamesInDropDown(DropDownList objDropDownList)
		{
			GetHolderNamesInDropDown(objDropDownList,null);
		}
		#endregion

		#region Update From Additional Interest

		public int UpdateHolder(Model.Maintenance.ClsHolderInfo objOldHolderInfo,Model.Maintenance.ClsHolderInfo objHolderInfo)
		{
			int retVal;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			retVal	=	UpdateHolder(objOldHolderInfo,objHolderInfo,objDataWrapper);
			//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			return retVal;
		}
		public int UpdateHolder(Model.Maintenance.ClsHolderInfo objOldHolderInfo,Model.Maintenance.ClsHolderInfo objHolderInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_UpdateHolder";
			DateTime	RecordDate		=	DateTime.Now;
			
			//Set transaction label in the new object, if required
			if ( this.boolTransactionRequired)
			{
				objOldHolderInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(@"application\aspx\AdditionalInterest.aspx.resx");
			}

			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			//objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			try
			{
				//System.Data.SqlClient.SqlParameter objParam =(System.Data.SqlClient.SqlParameter)
				
				objDataWrapper.AddParameter("@Holderid",objHolderInfo.HOLDER_ID);
				objDataWrapper.AddParameter("@HolderCode",objHolderInfo.HOLDER_CODE );
				//will be changed later
				objDataWrapper.AddParameter("@HolderName",objHolderInfo.HOLDER_NAME);
				objDataWrapper.AddParameter("@Add1",objHolderInfo.HOLDER_ADD1);
				objDataWrapper.AddParameter("@Add2",objHolderInfo.HOLDER_ADD2);
				objDataWrapper.AddParameter("@City",objHolderInfo.HOLDER_CITY);
				objDataWrapper.AddParameter("@State",objHolderInfo.HOLDER_STATE);
				objDataWrapper.AddParameter("@Zip",objHolderInfo.HOLDER_ZIP);
				objDataWrapper.AddParameter("@Country",objHolderInfo.HOLDER_COUNTRY );
				objDataWrapper.AddParameter("@MainPhoneNo",objHolderInfo.HOLDER_MAIN_PHONE_NO );
				objDataWrapper.AddParameter("@Fax",objHolderInfo.HOLDER_FAX);
				objDataWrapper.AddParameter("@ModifiedBy",objHolderInfo.MODIFIED_BY);
				
				int returnResult = 0;

				if(TransactionLogRequired) 
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objOldHolderInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objHolderInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Holder Details Has Been Modified From Additional Interest";	
					objTransactionInfo.CHANGE_XML		=	strTranXML;


					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					returnResult	=	1;
				}
				else//if no transaction required
				{
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

								
				int HOLDER_ID		=	1;//int.Parse(objParam.Value.ToString());
				//objDataWrapper.ClearParameteres();
				//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if (HOLDER_ID == -1)
				{
					return -1;
				}
				else
				{
					objHolderInfo.HOLDER_ID = HOLDER_ID;
					return returnResult;
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			//finally
			//{
				//if(objDataWrapper != null) objDataWrapper.Dispose();
			//}

		}
		#endregion

		#region "Fill Drop down Functions"
		public DataSet FillHolder(int iVEHICLE_ID)
		{
			string		strStoredProc	=	"Proc_GetHolder";//Will be replaced with CONST
					
			DataSet dsAccounts = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			objDataWrapper.AddParameter("@VEHICLE_ID",iVEHICLE_ID,SqlDbType.Int);

			dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
			return dsAccounts;
		}

        public DataSet GetHolderList(int iVEHICLE_ID)
        {
            string strStoredProc = "Proc_GetHolderList";//Will be replaced with CONST

            DataSet dsAccounts = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            objDataWrapper.AddParameter("@DWELLING_ID", iVEHICLE_ID, SqlDbType.Int);

            dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
            return dsAccounts;
        }

        public DataSet FillHolderFromWatercraft(int iBOAT_ID)
        {
            string		strStoredProc	=	"Proc_GetWatercraftHolder";//Will be replaced with CONST
					
            DataSet dsAccounts = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

            objDataWrapper.AddParameter("@BOAT_ID",iBOAT_ID,SqlDbType.Int);

            dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
            return dsAccounts;
        }

        public DataSet FillHolderFromTrailer(int iTRAILER_ID)
        {
            string		strStoredProc	=	"Proc_GetTrailerHolder";//Will be replaced with CONST
					
            DataSet dsAccounts = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

            objDataWrapper.AddParameter("@TRAILER_ID",iTRAILER_ID,SqlDbType.Int);

            dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
            return dsAccounts;
        }

     
		public DataSet FillHolderFromHomeOwner(int iDwellingId)
		{
			string		strStoredProc	=	"Proc_GetHolderHomeOwner";//Will be replaced with CONST
					
			DataSet dsAccounts = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			objDataWrapper.AddParameter("@DWELLING_ID",iDwellingId,SqlDbType.Int);

			dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
			return dsAccounts;
		}

		public DataSet FillHolderDetails(string strHolderID, out string strHolderXML)
		{
			string		strStoredProc	=	"Proc_GetHolderDetails";//Will be replaced with CONST
					
			DataSet dsAccounts = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
			objDataWrapper.AddParameter("@HolderID",strHolderID,SqlDbType.Int);

			dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
			strHolderXML = dsAccounts.GetXml();
			return dsAccounts;
		}
		public DataSet FillHolderDetails(string strHolderID)
		{
			string		strStoredProc	=	"Proc_GetHolderDetails";//Will be replaced with CONST
					
			DataSet dsAccounts = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
			objDataWrapper.AddParameter("@HolderID",strHolderID,SqlDbType.Int);

			dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			return dsAccounts;
		}
		public static string GetHolderName(string HOLDER_ID)
		{
			return DataWrapper.ExecuteScalar(ConnStr,CommandType.Text,"select HOLDER_NAME as INDIVIDUAL_CONTACT_NAME from MNT_HOLDER_INTEREST_LIST where HOLDER_ID="+HOLDER_ID).ToString();
		}
		#endregion

		/// <summary>
		/// Selects a single record from MNT_HOLDER_INTEREST_LIST   
		/// </summary>
		/// <param name="companyID"></param>
		/// <returns></returns>
		public DataSet GetHolderByID(int holderID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			string	strStoredProc =	"Proc_GetHolderInterestByID";

			objDataWrapper.AddParameter("@HOLDER_ID",holderID);

			DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}
	}
}
