/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	  5/25/2005 10:21:14 AM
<End Date				: -	
<Description			: -   Business logic for Bank Information.
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
	/// Business logic for Bank Information.
	/// </summary>
	public class ClsBankInformation : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		ACT_BANK_INFORMATION			=	"ACT_BANK_INFORMATION";
		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateBankInformation";
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
		public ClsBankInformation()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		public ClsBankInformation(bool transactionLogRequired):this()
		{
			base.TransactionLogRequired = transactionLogRequired;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objBankInformationInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsBankInformationInfo objBankInformationInfo,string File1,string File2, string EntityType)
		{
			string		strStoredProc	=	"Proc_InsertBankInformation";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				string category = "";
				string group = "";
				string account = "";

				string []arrEntity = EntityType.Split('~');
				if(arrEntity.Length >= 3)
				{
					category = arrEntity[0];
					group = arrEntity[1];
					account = arrEntity[2];
				}

				objDataWrapper.AddParameter("@GL_ID",objBankInformationInfo.GL_ID);
				objDataWrapper.AddParameter("@ACCOUNT_ID",objBankInformationInfo.ACCOUNT_ID);
				objDataWrapper.AddParameter("@BANK_NAME",objBankInformationInfo.BANK_NAME);
				objDataWrapper.AddParameter("@BANK_ADDRESS1",objBankInformationInfo.BANK_ADDRESS1);
				objDataWrapper.AddParameter("@BANK_ADDRESS2",objBankInformationInfo.BANK_ADDRESS2);
				objDataWrapper.AddParameter("@BANK_CITY",objBankInformationInfo.BANK_CITY);
				objDataWrapper.AddParameter("@BANK_COUNTRY",objBankInformationInfo.BANK_COUNTRY);
				objDataWrapper.AddParameter("@BANK_STATE",objBankInformationInfo.BANK_STATE);
				objDataWrapper.AddParameter("@BANK_ZIP",objBankInformationInfo.BANK_ZIP);
				objDataWrapper.AddParameter("@BANK_ACC_TITLE",objBankInformationInfo.BANK_ACC_TITLE);
				objDataWrapper.AddParameter("@BANK_NUMBER",objBankInformationInfo.BANK_NUMBER);
				objDataWrapper.AddParameter("@STARTING_DEPOSIT_NUMBER",objBankInformationInfo.STARTING_DEPOSIT_NUMBER);
				objDataWrapper.AddParameter("@IS_CHECK_ISSUED",objBankInformationInfo.IS_CHECK_ISSUED);
				objDataWrapper.AddParameter("@IS_ACTIVE",objBankInformationInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objBankInformationInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objBankInformationInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objBankInformationInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objBankInformationInfo.LAST_UPDATED_DATETIME);
                objDataWrapper.AddParameter("@ACCOUNT_TYPE", objBankInformationInfo.ACCOUNT_TYPE);
                objDataWrapper.AddParameter("@BANK_TYPE", objBankInformationInfo.BANK_TYPE); //added by aditya for itrack # 1505 on 08-8-2011
				if(objBankInformationInfo.START_CHECK_NUMBER != -1)
					objDataWrapper.AddParameter("@START_CHECK_NUMBER",objBankInformationInfo.START_CHECK_NUMBER);
				else
					objDataWrapper.AddParameter("@START_CHECK_NUMBER",null);

				if(objBankInformationInfo.END_CHECK_NUMBER != -1)
					objDataWrapper.AddParameter("@END_CHECK_NUMBER",objBankInformationInfo.END_CHECK_NUMBER);
				else
					objDataWrapper.AddParameter("@END_CHECK_NUMBER",null);

				if(objBankInformationInfo.ROUTE_POSITION_CODE1 != "-1")
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE1",objBankInformationInfo.ROUTE_POSITION_CODE1);
				else
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE1",null);
				
				if(objBankInformationInfo.ROUTE_POSITION_CODE2 != "-1")
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE2",objBankInformationInfo.ROUTE_POSITION_CODE2);
				else
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE2",null);
				
				if(objBankInformationInfo.ROUTE_POSITION_CODE3 != "-1")
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE3",objBankInformationInfo.ROUTE_POSITION_CODE3);
				else
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE3",null);
				
				if(objBankInformationInfo.ROUTE_POSITION_CODE4 != "-1")
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE4",objBankInformationInfo.ROUTE_POSITION_CODE4);
				else
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE4",null);
				
				if(objBankInformationInfo.BANK_MICR_CODE != "-1")
					objDataWrapper.AddParameter("@BANK_MICR_CODE",objBankInformationInfo.BANK_MICR_CODE);
				else
					objDataWrapper.AddParameter("@BANK_MICR_CODE",null);
				objDataWrapper.AddParameter("@SIGN_FILE_1",File1);
				objDataWrapper.AddParameter("@SIGN_FILE_2",File2);
				objDataWrapper.AddParameter("@TRANSIT_ROUTING_NUMBER",objBankInformationInfo.TRANSIT_ROUTING_NUMBER);
				objDataWrapper.AddParameter("@COMPANY_ID",objBankInformationInfo.COMPANY_ID);
				
                //Added By Pradeep Kushwaha 
                
                //objDataWrapper.AddParameter("@BANK_ID",objBankInformationInfo.BANK_ID);
                objDataWrapper.AddParameter("@NUMBER",objBankInformationInfo.NUMBER);
                objDataWrapper.AddParameter("@REGISTERED",objBankInformationInfo.REGISTERED);
                objDataWrapper.AddParameter("@STARTING_OUR_NUMBER",objBankInformationInfo.STARTING_OUR_NUMBER);
                objDataWrapper.AddParameter("@ENDING_OUR_NUMBER", objBankInformationInfo.ENDING_OUR_NUMBER);
                objDataWrapper.AddParameter("@BRANCH_NUMBER", objBankInformationInfo.BRANCH_NUMBER);
                objDataWrapper.AddParameter("@AGREEMENT_NUMBER", objBankInformationInfo.AGREEMENT_NUMBER);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@BANK_ID", objBankInformationInfo.BANK_ID, SqlDbType.Int, ParameterDirection.Output);
                SqlParameter returnSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RETNVALUE", SqlDbType.Int, ParameterDirection.ReturnValue);

                //End added 

                //shikha - 57
                objDataWrapper.AddParameter("@ADD_NUMBER", objBankInformationInfo.ADD_NUMBER);

                int returnResult = 0;
				if(TransactionLogRequired)
				{
					objBankInformationInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddBankInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objBankInformationInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objBankInformationInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1528","");//"Bank Information Has Been Added";				

					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='START_CHECK_NUMBER' and @NewValue='-1' ]","NewValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='START_CHECK_NUMBER' and @OldValue='-1' ]","OldValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='END_CHECK_NUMBER' and @NewValue='-1' ]","NewValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='END_CHECK_NUMBER' and @OldValue='-1' ]","OldValue","null");

					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		= ";Category: " + category;
                    if (EntityType.Trim() != "")
                    {
                        if (group != " " && arrEntity[1] != "")
                        {
                            objTransactionInfo.CUSTOM_INFO += ";Sub Type: " + group;
                        }
                        objTransactionInfo.CUSTOM_INFO += ";Account No.: " + account;
                    }
					//Executing the query

                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);

				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
                if (objSqlParameter.Value.ToString()!="" && int.Parse(objSqlParameter.Value.ToString()) > 0)
                {
                    objBankInformationInfo.BANK_ID = int.Parse(objSqlParameter.Value.ToString());
                }
                if (returnSqlParameter.Value.ToString().Trim() != "")
                {
                    returnResult = int.Parse(returnSqlParameter.Value.ToString());
                }
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				{
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
		/// <param name="objOldBankInformationInfo">Model object having old information</param>
		/// <param name="objBankInformationInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsBankInformationInfo objOldBankInformationInfo,ClsBankInformationInfo objBankInformationInfo,string File1, string File2, string EntityType)
		{
			string		strStoredProc	=	"Proc_UpdateBankInformation";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				string category = "";
				string group = "";
				string account = "";

				string []arrEntity = EntityType.Split('~');
				if(arrEntity.Length >= 3)
				{
					category = arrEntity[0];
					group = arrEntity[1];
					account = arrEntity[2];
				}
				objDataWrapper.AddParameter("@GL_ID",objBankInformationInfo.GL_ID);
				objDataWrapper.AddParameter("@ACCOUNT_ID",objBankInformationInfo.ACCOUNT_ID);
				objDataWrapper.AddParameter("@BANK_NAME",objBankInformationInfo.BANK_NAME);
				objDataWrapper.AddParameter("@BANK_ADDRESS1",objBankInformationInfo.BANK_ADDRESS1);
				objDataWrapper.AddParameter("@BANK_ADDRESS2",objBankInformationInfo.BANK_ADDRESS2);
				objDataWrapper.AddParameter("@BANK_CITY",objBankInformationInfo.BANK_CITY);
				objDataWrapper.AddParameter("@BANK_COUNTRY",objBankInformationInfo.BANK_COUNTRY);
				objDataWrapper.AddParameter("@BANK_STATE",objBankInformationInfo.BANK_STATE);
				objDataWrapper.AddParameter("@BANK_ZIP",objBankInformationInfo.BANK_ZIP);
				objDataWrapper.AddParameter("@BANK_ACC_TITLE",objBankInformationInfo.BANK_ACC_TITLE);
				objDataWrapper.AddParameter("@BANK_NUMBER",objBankInformationInfo.BANK_NUMBER);
				objDataWrapper.AddParameter("@STARTING_DEPOSIT_NUMBER",objBankInformationInfo.STARTING_DEPOSIT_NUMBER);
				objDataWrapper.AddParameter("@IS_CHECK_ISSUED",objBankInformationInfo.IS_CHECK_ISSUED);
				objDataWrapper.AddParameter("@MODIFIED_BY",objBankInformationInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objBankInformationInfo.LAST_UPDATED_DATETIME);
                objDataWrapper.AddParameter("@ACCOUNT_TYPE", objBankInformationInfo.ACCOUNT_TYPE);
                objDataWrapper.AddParameter("@BANK_TYPE", objBankInformationInfo.BANK_TYPE);  //added by aditya for itrack # 1505 on 08-8-2011

				if(objBankInformationInfo.START_CHECK_NUMBER != -1)
					objDataWrapper.AddParameter("@START_CHECK_NUMBER",objBankInformationInfo.START_CHECK_NUMBER);
				else
					objDataWrapper.AddParameter("@START_CHECK_NUMBER",null);

				if(objBankInformationInfo.END_CHECK_NUMBER != -1)
					objDataWrapper.AddParameter("@END_CHECK_NUMBER",objBankInformationInfo.END_CHECK_NUMBER);
				else
					objDataWrapper.AddParameter("@END_CHECK_NUMBER",null);

				if(objBankInformationInfo.ROUTE_POSITION_CODE1 != "-1")
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE1",objBankInformationInfo.ROUTE_POSITION_CODE1);
				else
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE1",null);
				
				if(objBankInformationInfo.ROUTE_POSITION_CODE2 != "-1")
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE2",objBankInformationInfo.ROUTE_POSITION_CODE2);
				else
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE2",null);
				
				if(objBankInformationInfo.ROUTE_POSITION_CODE3 != "-1")
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE3",objBankInformationInfo.ROUTE_POSITION_CODE3);
				else
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE3",null);
				
				if(objBankInformationInfo.ROUTE_POSITION_CODE4 != "-1")
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE4",objBankInformationInfo.ROUTE_POSITION_CODE4);
				else
					objDataWrapper.AddParameter("@ROUTE_POSITION_CODE4",null);
				
				if(objBankInformationInfo.BANK_MICR_CODE != "-1")
					objDataWrapper.AddParameter("@BANK_MICR_CODE",objBankInformationInfo.BANK_MICR_CODE);
				else
					objDataWrapper.AddParameter("@BANK_MICR_CODE",null);
				
				objDataWrapper.AddParameter("@SIGN_FILE_1",File1);
				objDataWrapper.AddParameter("@SIGN_FILE_2",File2);
				objDataWrapper.AddParameter("@TRANSIT_ROUTING_NUMBER",objBankInformationInfo.TRANSIT_ROUTING_NUMBER);
				objDataWrapper.AddParameter("@COMPANY_ID",objBankInformationInfo.COMPANY_ID);

                //Added By Pradeep Kushwaha 
                objDataWrapper.AddParameter("@BANK_ID", objOldBankInformationInfo.BANK_ID);
                objDataWrapper.AddParameter("@NUMBER", objBankInformationInfo.NUMBER);
                objDataWrapper.AddParameter("@REGISTERED", objBankInformationInfo.REGISTERED);
                objDataWrapper.AddParameter("@STARTING_OUR_NUMBER", objBankInformationInfo.STARTING_OUR_NUMBER);
                objDataWrapper.AddParameter("@ENDING_OUR_NUMBER", objBankInformationInfo.ENDING_OUR_NUMBER);
                objDataWrapper.AddParameter("@BRANCH_NUMBER", objBankInformationInfo.BRANCH_NUMBER);
                objDataWrapper.AddParameter("@AGREEMENT_NUMBER", objBankInformationInfo.AGREEMENT_NUMBER);
                //End added 

                objDataWrapper.AddParameter("@ADD_NUMBER", objBankInformationInfo.ADD_NUMBER);
                SqlParameter returnSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RETNVALUE", SqlDbType.Int, ParameterDirection.ReturnValue);

				if(base.TransactionLogRequired) 
				{
					objBankInformationInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddBankInformation.aspx.resx");
					objBuilder.GetUpdateSQL(objOldBankInformationInfo,objBankInformationInfo,out strTranXML);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objBankInformationInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1527", "");// "Bank Information Has Been Updated";
					//objTransactionInfo.CUSTOM_INFO		= ";Category: " + category +";Sub Type: " + group + ";Account No.: " + account;
                    if (EntityType.Trim() != "")
                    {
                        objTransactionInfo.CUSTOM_INFO = ";Category: " + category;
                        if (group != " " && arrEntity[1] != "")
                        {
                            objTransactionInfo.CUSTOM_INFO += ";Sub Type: " + group;
                        }
                        objTransactionInfo.CUSTOM_INFO += ";Account No.: " + account;
                    }
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='START_CHECK_NUMBER' and @NewValue='-1' ]","NewValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='START_CHECK_NUMBER' and @OldValue='-1' ]","OldValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='END_CHECK_NUMBER' and @NewValue='-1' ]","NewValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='END_CHECK_NUMBER' and @OldValue='-1' ]","OldValue","null");
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
                if (returnSqlParameter.Value.ToString().Trim() != "")
                {
                    returnResult = int.Parse(returnSqlParameter.Value.ToString());
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
        public static string GetXmlForPageControls(string glID, string accountID, string bankid)
		{
            String strSql = String.Empty;

            if (accountID != null && accountID != "" && glID != "" && glID != null && bankid == null )
            {

                strSql = "select t1.GL_ID,t1.MODIFIED_BY,t1.IS_ACTIVE,t1.BANK_NAME,t1.BANK_ADDRESS1,t1.BANK_ADDRESS2,t1.BANK_CITY,t1.BANK_STATE,t1.BANK_ZIP,t1.BANK_ACC_TITLE,t1.BANK_NUMBER,t1.STARTING_DEPOSIT_NUMBER,t1.IS_CHECK_ISSUED,t1.START_CHECK_NUMBER,t1.END_CHECK_NUMBER,t1.ROUTE_POSITION_CODE1,t1.ROUTE_POSITION_CODE2,t1.ROUTE_POSITION_CODE3,t1.ROUTE_POSITION_CODE4,t1.BANK_MICR_CODE,t1.SIGN_FILE_1,t1.SIGN_FILE_2,t1.TRANSIT_ROUTING_NUMBER,t1.COMPANY_ID,t1.BANK_ID,t1.NUMBER,t1.REGISTERED,t1.STARTING_OUR_NUMBER,t1.ENDING_OUR_NUMBER,t1.ACCOUNT_TYPE,t1.BRANCH_NUMBER,t1.AGREEMENT_NUMBER,t1.BANK_COUNTRY,t1.ADD_NUMBER,t1.BANK_TYPE "; //added by aditya for itrack # 1505 on 08-8-2011
                strSql += " from ACT_BANK_INFORMATION t1";            
                strSql += " where  account_ID=" + accountID + " and t1.GL_ID =" + glID;
            }
            else
            {
                if (bankid != null && bankid!="" )
                {
                    strSql = "select t1.GL_ID,t1.MODIFIED_BY,t1.IS_ACTIVE,t1.BANK_NAME,t1.BANK_ADDRESS1,t1.BANK_ADDRESS2,t1.BANK_CITY,t1.BANK_STATE,t1.BANK_ZIP,t1.BANK_ACC_TITLE,t1.BANK_NUMBER,t1.STARTING_DEPOSIT_NUMBER,t1.IS_CHECK_ISSUED,t1.START_CHECK_NUMBER,t1.END_CHECK_NUMBER,t1.ROUTE_POSITION_CODE1,t1.ROUTE_POSITION_CODE2,t1.ROUTE_POSITION_CODE3,t1.ROUTE_POSITION_CODE4,t1.BANK_MICR_CODE,t1.SIGN_FILE_1,t1.SIGN_FILE_2,t1.TRANSIT_ROUTING_NUMBER,t1.COMPANY_ID,t1.BANK_ID,t1.NUMBER,t1.REGISTERED,t1.STARTING_OUR_NUMBER,t1.ENDING_OUR_NUMBER,t1.ACCOUNT_TYPE,t1.BRANCH_NUMBER,t1.AGREEMENT_NUMBER,t1.BANK_COUNTRY,t1.ADD_NUMBER,t1.BANK_TYPE  "; //added by aditya for itrack # 1505 on 08-8-2011
                    strSql += " from ACT_BANK_INFORMATION t1";

                    strSql += " where t1.BANK_ID=" + bankid;
                }
            }
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			if(objDataSet.Tables[0].Rows.Count<=0)
				return "";
			else
				return objDataSet.GetXml();
		}
		#endregion

		#region "Misc"
		public static int ResetCheckNumber(int account_ID)
		{
			string strStoredProc = "Proc_ResetCheckNumber";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@ACCOUNT_ID",account_ID);
			return objDataWrapper.ExecuteNonQuery(strStoredProc);
		}

		// Used in bank info to check for default EFT/CC account, if available or not.
		public static int FetchDefaultAccountID()
		{
			try
			{
				string strQuery = "SELECT TOP 1 BNK_CUST_DEP_EFT_CARD FROM ACT_GENERAL_LEDGER (NOLOCK)";
				SqlDataAdapter tmpDA = new SqlDataAdapter(strQuery,ConnStr);	
				DataSet ds = new DataSet();
				tmpDA.Fill(ds);
				if(ds.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty( ds.Tables[0].Rows[0][0].ToString()))
					return int.Parse(ds.Tables[0].Rows[0][0].ToString());	
				else
					return 0;
			}
			catch
			{
				throw ( new Exception("Error occured. Please try again !."));
			}
		}
		#endregion
	}
}
