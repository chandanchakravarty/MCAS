/******************************************************************************************
<Author				: -   aaa
<Start Date				: -	4/4/2005 3:55:26 PM
<End Date				: -	
<Description				: - 	ddd
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: 17/05/2005- 
<Modified By				:Gaurav - 
<Purpose				: Added  GetXmlForPageControls() function to receive Xml- 

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
	/// sss
	/// </summary>
	#region "Fill EFTCodes"
	public enum EFTCodes
	{

		CheckingAccount =100,
		SavingAccount	=101,
		DebitEntry		=11,
		CreditEntry		=12 
	}
	#endregion
	
	public class clsVendor : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const string	MNT_VENDOR_LIST			=	"MNT_VENDOR_LIST";
        private const  string   ACTIVATE_DEACTIVATE_PROCEDURE = "Proc_ActivateDeactivateVendor";
		#region Private Instance Variables
		private int _VENDOR_ID;
		#endregion
		#region Public Properties
		public int 	VENDOR_ID
		{
			get
			{
				return _VENDOR_ID;
			}
		}
		
		#endregion
		#region private Utility Functions
		#endregion
		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public clsVendor()
		{
			base.strActivateDeactivateProcedure = ACTIVATE_DEACTIVATE_PROCEDURE;
		}
		public clsVendor(bool transactionLogRequired):this()
		{
			base.TransactionLogRequired = transactionLogRequired;
		}
		#endregion
		#region Add(Insert) functions
		/// <summary>
		/// Overload of add that recieves parameters required to save.
		/// User is generally required to call this method.
		/// </summary>
		/// <param name="VENDOR_CODE"></param>
		/// <returns></returns>
		public int add(ClsVendorInfo objVendor)
		{
			string		strStoredProc	=	"Proc_InsertVendor";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				//objDataWrapper.AddParameter("@VENDOR_ID",VENDOR_ID);
				objDataWrapper.AddParameter("@VENDOR_CODE",objVendor.VENDOR_CODE);
				objDataWrapper.AddParameter("@VENDOR_FNAME",objVendor.VENDOR_FNAME);
				objDataWrapper.AddParameter("@VENDOR_LNAME",objVendor.VENDOR_LNAME);
				objDataWrapper.AddParameter("@VENDOR_ADD1",objVendor.VENDOR_ADD1);
				objDataWrapper.AddParameter("@VENDOR_ADD2",objVendor.VENDOR_ADD2);
				objDataWrapper.AddParameter("@VENDOR_CITY",objVendor.VENDOR_CITY);
				objDataWrapper.AddParameter("@VENDOR_COUNTRY",objVendor.VENDOR_COUNTRY);
				objDataWrapper.AddParameter("@VENDOR_STATE",objVendor.VENDOR_STATE);
				objDataWrapper.AddParameter("@VENDOR_ZIP",objVendor.VENDOR_ZIP);
				objDataWrapper.AddParameter("@VENDOR_PHONE",objVendor.VENDOR_PHONE);
				objDataWrapper.AddParameter("@VENDOR_EXT",objVendor.VENDOR_EXT);
				objDataWrapper.AddParameter("@VENDOR_FAX",objVendor.VENDOR_FAX);
				objDataWrapper.AddParameter("@VENDOR_MOBILE",objVendor.VENDOR_MOBILE);
				objDataWrapper.AddParameter("@VENDOR_EMAIL",objVendor.VENDOR_EMAIL);
				objDataWrapper.AddParameter("@VENDOR_SALUTATION",objVendor.VENDOR_SALUTATION);
				objDataWrapper.AddParameter("@VENDOR_FEDERAL_NUM",objVendor.VENDOR_FEDERAL_NUM);
				objDataWrapper.AddParameter("@VENDOR_NOTE",objVendor.VENDOR_NOTE);
				objDataWrapper.AddParameter("@VENDOR_ACC_NUMBER",objVendor.VENDOR_ACC_NUMBER);
				objDataWrapper.AddParameter("@IS_ACTIVE",objVendor.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objVendor.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objVendor.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objVendor.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objVendor.LAST_UPDATED_DATETIME);
				//added by pravesh
				objDataWrapper.AddParameter("@BUSI_OWNERNAME",objVendor.BUSI_OWNERNAME);
				objDataWrapper.AddParameter("@COMPANY_NAME",objVendor.COMPANY_NAME);
				objDataWrapper.AddParameter("@CHK_MAIL_ADD1",objVendor.CHK_MAIL_ADD1);  
				objDataWrapper.AddParameter("@CHK_MAIL_ADD2",objVendor.CHK_MAIL_ADD2);  
				objDataWrapper.AddParameter("@CHK_MAIL_CITY",objVendor.CHK_MAIL_CITY);  
				objDataWrapper.AddParameter("@CHK_MAIL_STATE",objVendor.CHK_MAIL_STATE); 
				objDataWrapper.AddParameter("@CHKCOUNTRY",objVendor.CHKCOUNTRY); 
				objDataWrapper.AddParameter("@CHK_MAIL_ZIP",objVendor.CHK_MAIL_ZIP);
                objDataWrapper.AddParameter("@ACTIVITY", objVendor.ACTIVITY);
                if (objVendor.REG_ID_ISSUE_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", objVendor.REG_ID_ISSUE_DATE);
                else
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", System.DBNull.Value);
                if (objVendor.DATE_OF_BIRTH != DateTime.MinValue)
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", objVendor.DATE_OF_BIRTH);
                else
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", System.DBNull.Value);

                
                objDataWrapper.AddParameter("@REG_ID_ISSUE", objVendor.REG_ID_ISSUE);
                objDataWrapper.AddParameter("@CPF", objVendor.CPF);
                objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", objVendor.REGIONAL_IDENTIFICATION);

				objDataWrapper.AddParameter("@MAIL_1099_ADD1",objVendor.MAIL_1099_ADD1); 
				objDataWrapper.AddParameter("@MAIL_1099_ADD2",objVendor.MAIL_1099_ADD2); 
				objDataWrapper.AddParameter("@MAIL_1099_CITY",objVendor.MAIL_1099_CITY); 
				objDataWrapper.AddParameter("@MAIL_1099_STATE",objVendor.MAIL_1099_STATE); 
				objDataWrapper.AddParameter("@MAIL_1099_COUNTRY",objVendor.MAIL_1099_COUNTRY); 
				objDataWrapper.AddParameter("@MAIL_1099_ZIP",objVendor.MAIL_1099_ZIP); 
				objDataWrapper.AddParameter("@MAIL_1099_NAME",objVendor.MAIL_1099_NAME); 

				objDataWrapper.AddParameter("@PROCESS_1099_OPT",objVendor.PROCESS_1099_OPT); 
				objDataWrapper.AddParameter("@W9_FORM",objVendor.W9_FORM); 
				//Added By Raghav for Itrack Issue 4810
				objDataWrapper.AddParameter("@REQ_SPECIAL_HANDLING",objVendor.REQ_SPECIAL_HANDLING);
				objDataWrapper.AddParameter("@ALLOWS_EFT",objVendor.ALLOWS_EFT);

				if(objVendor.ALLOWS_EFT==int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()))
					objDataWrapper.AddParameter("@ACCOUNT_TYPE",null);
				else
					objDataWrapper.AddParameter("@ACCOUNT_TYPE",objVendor.ACCOUNT_TYPE);

				objDataWrapper.AddParameter("@BANK_NAME",objVendor.BANK_NAME);
				objDataWrapper.AddParameter("@BANK_BRANCH",objVendor.BANK_BRANCH);
				objDataWrapper.AddParameter("@DFI_ACCOUNT_NUMBER",objVendor.DFI_ACCOUNT_NUMBER);
				objDataWrapper.AddParameter("@ROUTING_NUMBER",objVendor.ROUTING_NUMBER);
				if(objVendor.ACCOUNT_VERIFIED_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@ACCOUNT_VERIFIED_DATE",objVendor.ACCOUNT_VERIFIED_DATE);
				else
					objDataWrapper.AddParameter("@ACCOUNT_VERIFIED_DATE",System.DBNull.Value);
                
				objDataWrapper.AddParameter("@ACCOUNT_ISVERIFIED",objVendor.ACCOUNT_ISVERIFIED);
				objDataWrapper.AddParameter("@REASON",objVendor.REASON);

				objDataWrapper.AddParameter("@REVERIFIED_AC",objVendor.REVERIFIED_AC);
                //Added By Chetna
                objDataWrapper.AddParameter("@SUSEP_NUM", objVendor.SUSEP_NUM);


     
				//end here
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@VENDOR_ID",objVendor.VENDOR_ID,SqlDbType.Int,ParameterDirection.Output);
				int returnResult = 0;
				if(base.TransactionLogRequired) 
				{
					
					objVendor.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/Maintenance/AddVendor.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objVendor);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objVendor.CREATED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1560", "");// "New vendor is added";	
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult=objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult=objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				//objVendor.VENDOR_ID=int.Parse(objSqlParameter.Value.ToString());
				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if(objSqlParameter!=null && objSqlParameter.Value.ToString()!="")
				{
					_VENDOR_ID = int.Parse(objSqlParameter.Value.ToString());
					//objVendor.VENDOR_ID=int.Parse(objSqlParameter.Value.ToString());
					return _VENDOR_ID;
				}
				else
				{
					return returnResult = -1; 				
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
		public int Update(ClsVendorInfo objVendor,ClsVendorInfo objOldVendor)
		{
			SqlUpdateBuilder	objBuilder	=	new SqlUpdateBuilder();
			string		strStoredProc		=	"Proc_UpdateVendor";
			
			string strTranXML;
			int returnResult = 0;

			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@VENDOR_ID",objVendor.VENDOR_ID);
				objDataWrapper.AddParameter("@VENDOR_CODE",objVendor.VENDOR_CODE);
				objDataWrapper.AddParameter("@VENDOR_FNAME",objVendor.VENDOR_FNAME);
				objDataWrapper.AddParameter("@VENDOR_LNAME",objVendor.VENDOR_LNAME);
				objDataWrapper.AddParameter("@VENDOR_ADD1",objVendor.VENDOR_ADD1);
				objDataWrapper.AddParameter("@VENDOR_ADD2",objVendor.VENDOR_ADD2);
				objDataWrapper.AddParameter("@VENDOR_CITY",objVendor.VENDOR_CITY);
				objDataWrapper.AddParameter("@VENDOR_COUNTRY",objVendor.VENDOR_COUNTRY);
				objDataWrapper.AddParameter("@VENDOR_STATE",objVendor.VENDOR_STATE);
				objDataWrapper.AddParameter("@VENDOR_ZIP",objVendor.VENDOR_ZIP);
				objDataWrapper.AddParameter("@VENDOR_PHONE",objVendor.VENDOR_PHONE);
				objDataWrapper.AddParameter("@VENDOR_EXT",objVendor.VENDOR_EXT);
				objDataWrapper.AddParameter("@VENDOR_FAX",objVendor.VENDOR_FAX);
				objDataWrapper.AddParameter("@VENDOR_MOBILE",objVendor.VENDOR_MOBILE);
				objDataWrapper.AddParameter("@VENDOR_EMAIL",objVendor.VENDOR_EMAIL);
				objDataWrapper.AddParameter("@VENDOR_SALUTATION",objVendor.VENDOR_SALUTATION);
				objDataWrapper.AddParameter("@VENDOR_FEDERAL_NUM",objVendor.VENDOR_FEDERAL_NUM);
				objDataWrapper.AddParameter("@VENDOR_NOTE",objVendor.VENDOR_NOTE);
				objDataWrapper.AddParameter("@VENDOR_ACC_NUMBER",objVendor.VENDOR_ACC_NUMBER);
				objDataWrapper.AddParameter("@MODIFIED_BY",objVendor.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objVendor.LAST_UPDATED_DATETIME);
				//added by pravesh
				objDataWrapper.AddParameter("@BUSI_OWNERNAME",objVendor.BUSI_OWNERNAME);
				objDataWrapper.AddParameter("@COMPANY_NAME",objVendor.COMPANY_NAME);
				objDataWrapper.AddParameter("@CHK_MAIL_ADD1",objVendor.CHK_MAIL_ADD1);  
				objDataWrapper.AddParameter("@CHK_MAIL_ADD2",objVendor.CHK_MAIL_ADD2);  
				objDataWrapper.AddParameter("@CHK_MAIL_CITY",objVendor.CHK_MAIL_CITY);  
				objDataWrapper.AddParameter("@CHK_MAIL_STATE",objVendor.CHK_MAIL_STATE); 
				objDataWrapper.AddParameter("@CHKCOUNTRY",objVendor.CHKCOUNTRY); 
				objDataWrapper.AddParameter("@CHK_MAIL_ZIP",objVendor.CHK_MAIL_ZIP); 

				objDataWrapper.AddParameter("@MAIL_1099_ADD1",objVendor.MAIL_1099_ADD1); 
				objDataWrapper.AddParameter("@MAIL_1099_ADD2",objVendor.MAIL_1099_ADD2); 
				objDataWrapper.AddParameter("@MAIL_1099_CITY",objVendor.MAIL_1099_CITY); 
				objDataWrapper.AddParameter("@MAIL_1099_STATE",objVendor.MAIL_1099_STATE); 
				objDataWrapper.AddParameter("@MAIL_1099_COUNTRY",objVendor.MAIL_1099_COUNTRY); 
				objDataWrapper.AddParameter("@MAIL_1099_ZIP",objVendor.MAIL_1099_ZIP); 
				objDataWrapper.AddParameter("@MAIL_1099_NAME",objVendor.MAIL_1099_NAME); 
				objDataWrapper.AddParameter("@PROCESS_1099_OPT",objVendor.PROCESS_1099_OPT); 
				objDataWrapper.AddParameter("@W9_FORM",objVendor.W9_FORM);
                objDataWrapper.AddParameter("@ACTIVITY", objVendor.ACTIVITY);
                if (objVendor.REG_ID_ISSUE_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", objVendor.REG_ID_ISSUE_DATE);
                else
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", null);
                if (objVendor.DATE_OF_BIRTH != DateTime.MinValue)
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", objVendor.DATE_OF_BIRTH);
                else
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH",null);
                objDataWrapper.AddParameter("@REG_ID_ISSUE", objVendor.REG_ID_ISSUE);
                
                objDataWrapper.AddParameter("@CPF", objVendor.CPF);
                objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", objVendor.REGIONAL_IDENTIFICATION);
				//Added By Raghav for Itrack Issue 4810
				objDataWrapper.AddParameter("@REQ_SPECIAL_HANDLING",objVendor.REQ_SPECIAL_HANDLING);
				objDataWrapper.AddParameter("@ALLOWS_EFT",objVendor.ALLOWS_EFT);
				if(objVendor.ALLOWS_EFT==int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()))
					objDataWrapper.AddParameter("@ACCOUNT_TYPE",null);
				else
					objDataWrapper.AddParameter("@ACCOUNT_TYPE",objVendor.ACCOUNT_TYPE);
				objDataWrapper.AddParameter("@BANK_NAME",objVendor.BANK_NAME);
				objDataWrapper.AddParameter("@BANK_BRANCH",objVendor.BANK_BRANCH);
				objDataWrapper.AddParameter("@DFI_ACCOUNT_NUMBER",objVendor.DFI_ACCOUNT_NUMBER);
				objDataWrapper.AddParameter("@ROUTING_NUMBER",objVendor.ROUTING_NUMBER);

				if(objVendor.ACCOUNT_VERIFIED_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@ACCOUNT_VERIFIED_DATE",objVendor.ACCOUNT_VERIFIED_DATE);
				else
					objDataWrapper.AddParameter("@ACCOUNT_VERIFIED_DATE",System.DBNull.Value);
				objDataWrapper.AddParameter("@ACCOUNT_ISVERIFIED",objVendor.ACCOUNT_ISVERIFIED);
				objDataWrapper.AddParameter("@REASON",objVendor.REASON);
				//Added reverif
				objDataWrapper.AddParameter("@REVERIFIED_AC",objVendor.REVERIFIED_AC);

				//Added By Chetna
                objDataWrapper.AddParameter("@SUSEP_NUM", objVendor.SUSEP_NUM);
				
     
				//end here
				if(TransactionLogRequired) 
				{
					

					objVendor.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/addvendor.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldVendor,objVendor);
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.RECORDED_BY		=	objVendor.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1561", "");// "Vendor is modified";
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
			/*
			objBuilder.WhereClause = " WHERE VENDOR_ID = " + objOldVendor.VENDOR_ID.ToString();
			string strUpdate = objBuilder.GetUpdateSQL(objOldVendor,objVendor,out strTranXML);
			if(strUpdate != "")
			{
				strUpdate	=	"IF Not Exists(SELECT VENDOR_CODE "
					+ " FROM MNT_VENDOR_LIST "
					+ " WHERE VENDOR_CODE='" + ReplaceInvalidCharecter(objVendor.VENDOR_CODE) 
					+ "' AND VENDOR_ID<>" + objVendor.VENDOR_ID
					+ ")"
					+ strUpdate;

			
				
				try
				{
					if(base.TransactionLogRequired) 
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.RECORDED_BY		=	objVendor.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vendor is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

						returnResult=objDataWrapper.ExecuteNonQuery(strUpdate,objTransactionInfo);
					}
					else
					{
						returnResult = objDataWrapper.ExecuteNonQuery(strUpdate);
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
			else
			{
				return 0;
			}
			*/

		}
		#endregion

			
		#region Delete functions
		/// <summary>
		/// deletes the information passed in model object to database.
		/// </summary>
		/// 

		public int Delete(int intRetVal)
		{
			return Delete(null,intRetVal);
		}

		public int Delete(ClsVendorInfo objVendorInfo,int intRetVal)
		{
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string	strStoredProc =	"Proc_DeleteVendor";
			try
			{
				objDataWrapper.AddParameter("@VendorId",intRetVal);
				int returnResult = 0;
				if(TransactionLogRequired) 
				{			

					objVendorInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/addVendor.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objVendorInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objVendorInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1562", "");// "Vendor is Deleted";				
					objTransactionInfo.CHANGE_XML		=	strTranXML;
//					objTransactionInfo.CUSTOM_INFO		=	";Agency Name = " + objVendorInfo.COMPANY_NAME + ";Agency Code = " + objVendorInfo.AGENCY_CODE;
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
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
				if(objDataWrapper != null) 
				{
					objDataWrapper.Dispose();
				}				
			}
		
		}
		#endregion

		#region "Get Xml Methods"
		public static string GetXmlForPageControls(string strVendorId,int LangID)
		{
            string str1 = "ISNULL(Convert(varchar,t1.REG_ID_ISSUE_DATE,case when " + LangID + "=2 then 103 else 101 end),'') AS REG_ID_ISSUE_DATE, ";
            string str2 = "ISNULL(Convert(varchar,t1.DATE_OF_BIRTH,case when " + LangID + "=2 then 103 else 101 end),'') AS DATE_OF_BIRTH,";
			string strXml="";
            string strSql = "select t1.VENDOR_ID,t1.VENDOR_CODE,t1.VENDOR_FNAME,t1.VENDOR_LNAME,t1.VENDOR_ADD1,t1.VENDOR_ADD2,t1.VENDOR_CITY,t1.VENDOR_COUNTRY,t1.VENDOR_STATE,t1.VENDOR_ZIP,t1.VENDOR_PHONE,t1.VENDOR_EXT,t1.VENDOR_FAX,t1.VENDOR_MOBILE,t1.VENDOR_EMAIL,t1.VENDOR_SALUTATION,t1.VENDOR_FEDERAL_NUM,t1.VENDOR_NOTE,t1.VENDOR_ACC_NUMBER,t1.IS_ACTIVE,t1.MODIFIED_BY,t1.ACTIVITY,"+str1+"t1.REG_ID_ISSUE,"+str2+"t1.CPF,t1.REGIONAL_IDENTIFICATION,t1.VENDOR_FNAME+ ' '+t1.VENDOR_LNAME as Name,t1.VENDOR_ADD1+' '+t1.VENDOR_ADD2 as Address,t2.STATE_NAME";
            strSql += ",t1.BUSI_OWNERNAME,t1.COMPANY_NAME,t1.CHK_MAIL_ADD1,t1.CHK_MAIL_ADD2,t1.CHK_MAIL_CITY,t1.CHK_MAIL_STATE,t1.CHKCOUNTRY,t1.CHK_MAIL_ZIP,t1.MAIL_1099_ADD1,t1.MAIL_1099_ADD2,t1.MAIL_1099_CITY,t1.MAIL_1099_STATE,t1.MAIL_1099_COUNTRY,t1.MAIL_1099_ZIP,t1.MAIL_1099_NAME,t1.PROCESS_1099_OPT,t1.W9_FORM,t1.ALLOWS_EFT,t1.BANK_NAME,t1.BANK_BRANCH,t1.DFI_ACCOUNT_NUMBER as BANK_ACCOUNT_NUMBER,t1.ROUTING_NUMBER,t1.SUSEP_NUM,CONVERT(VARCHAR,T1.ACCOUNT_VERIFIED_DATE,110) AS ACCOUNT_VERIFIED_DATE,CASE WHEN CONVERT(VARCHAR,T1.ACCOUNT_ISVERIFIED) = '10964' THEN 'No' WHEN CONVERT(VARCHAR,T1.ACCOUNT_ISVERIFIED) = '10963' THEN 'Yes' ELSE 'No' END AS ACCOUNT_ISVERIFIED, ISNULL(t1.REQ_SPECIAL_HANDLING,'10964') AS REQ_SPECIAL_HANDLING ,t1.REASON,t1.ACCOUNT_TYPE,isnull(t1.REVERIFIED_AC,'') REVERIFIED_AC";//SUSEP_NUM added By Chetna
            strSql += " from MNT_VENDOR_LIST t1 LEFT JOIN MNT_COUNTRY_STATE_LIST t2 on t1.VENDOR_STATE=t2.STATE_ID";
			strSql += " where VENDOR_ID=" + strVendorId;
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			if (objDataSet.Tables[0].Rows.Count == 0)
			{
				strXml ="";
			}
			else
			{
				strXml = objDataSet.GetXml().ToString();
			}
			return strXml;
		}
		#endregion

		#region "Fill Drop down Functions"
//		public static void GetVendorNamesInDropDown(DropDownList objDropDownList)
//		{
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
//			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillVendorDropDown").Tables[0];
//			objDropDownList.DataSource = objDataTable;
//			objDropDownList.DataTextField = "VENDOR_FNAME";
//			objDropDownList.DataValueField = "VENDOR_ID";
//			objDropDownList.DataBind();
//		}
		public static void GetVendorNamesInDropDown(DropDownList objDropDownList, string selectedValue)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillVendorDropDown").Tables[0];
			objDropDownList.Items.Clear();
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				objDropDownList.Items.Add(new ListItem(objDataTable.DefaultView[i]["COMPANY_NAME"].ToString(),objDataTable.DefaultView[i]["VENDOR_ID"].ToString()));
				if(selectedValue!=null && selectedValue.Length>0 && objDataTable.DefaultView[i]["VENDOR_ID"].ToString().Equals(selectedValue))
					objDropDownList.SelectedIndex = i;
			}
		}
        //get data to bind coolite control - itrack - 1557
        public static void GetVendorNamesInDropDown(ref DataTable dt)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            dt = objDataWrapper.ExecuteDataSet("Proc_FillVendorDropDown").Tables[0];
             
        }
		public static DataTable GetVendorNames()
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillVendorDropDown").Tables[0];
			return objDataTable;
		}
		public static void GetVendorNamesInDropDown(DropDownList objDropDownList)
		{
			GetVendorNamesInDropDown(objDropDownList,null);
		}
		public static string GetVendorName(string VENDOR_ID)
		{
			return DataWrapper.ExecuteScalar(ConnStr,CommandType.Text,"select VENDOR_FNAME +' ' + VENDOR_LNAME as INDIVIDUAL_CONTACT_NAME from MNT_VENDOR_LIST where VENDOR_ID="+VENDOR_ID).ToString();
		}
		#endregion

	}
}