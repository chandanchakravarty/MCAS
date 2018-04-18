using System;
using System.Data;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Collections;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Business logic for Contacts entity used to add,update,Activate/deactivate various contacts in the system.
	/// </summary>
	public class ClsContactsManager:Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		CONTACT_LIST_TABLE			=	"MNT_CONTACT_LIST";
	    private const  string   ACTIVATE_DEACTIVATE_PROCEDURE = "Proc_ActivateDeactivateContact";

		#region Private Instance Variables
			private			bool		boolTransactionLog;		
			private int conatctId;
		#endregion
		#region Public Properties
		public int 	ConatctId
		{
			get
			{
				return conatctId;
			}
		}
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
		public ClsContactsManager():base(ACTIVATE_DEACTIVATE_PROCEDURE)
		{
			boolTransactionLog	= base.TransactionLogRequired;	
		}

        public string ContactActivateDeactivate(string strCode, string isActive, string strCustomInfo, int RecordedBy)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                int CustomerId = 0;
                objDataWrapper.AddParameter("@CODE", strCode);
                objDataWrapper.AddParameter("@IS_ACTIVE", isActive);
                SqlParameter objPaam = (SqlParameter)objDataWrapper.AddParameter("@RET_VAL", System.Data.DbType.Int16, System.Data.ParameterDirection.ReturnValue);

                //objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure,objTranasction);

                if (this.TransactionLogRequired)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransaction = new Cms.Model.Maintenance.ClsTransactionInfo();
                    // objTransaction.TRANS_TYPE_ID = transactionID;
                    if (isActive == "N")
                        objTransaction.TRANS_TYPE_ID = 247;
                    else if (isActive == "Y")
                        objTransaction.TRANS_TYPE_ID = 246;
                    objTransaction.RECORDED_BY = RecordedBy;
                    objTransaction.CLIENT_ID = CustomerId;
                    objTransaction.CUSTOM_INFO = strCustomInfo;
                    objDataWrapper.ExecuteNonQuery("Proc_ActivateDeactivateContact", objTransaction);
                }
                else
                {
                    objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure);
                }
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (objPaam != null)
                {
                    return (objPaam.Value.ToString());
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }
		#endregion	



		#region "Public utility Methods"
		
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Overload of add that recieves minimum parameters required to save a contact
		/// User is generally required to call this method.
		/// </summary>
		/// <param name="CONTACT_CODE"></param>
		/// <param name="CONTACT_TYPE_ID"></param>
		/// <param name="CONTACT_SALUTATION"></param>
		/// <param name="CONTACT_POS"></param>
		/// <param name="INDIVIDUAL_CONTACT_ID"></param>
		/// <param name="CONTACT_FNAME"></param>
		/// <param name="CONTACT_MNAME"></param>
		/// <param name="CONTACT_LNAME"></param>
		/// <param name="CONTACT_ADD1"></param>
		/// <param name="CONTACT_ADD2"></param>
		/// <param name="CONTACT_CITY"></param>
		/// <param name="CONTACT_STATE"></param>
		/// <param name="CONTACT_ZIP"></param>
		/// <param name="CONTACT_COUNTRY"></param>
		/// <param name="CONTACT_BUSINESS_PHONE"></param>
		/// <param name="CONTACT_EXT"></param>
		/// <param name="CONTACT_FAX"></param>
		/// <param name="CONTACT_MOBILE"></param>
		/// <param name="CONTACT_EMAIL"></param>
		/// <param name="CONTACT_PAGER"></param>
		/// <param name="CONTACT_HOME_PHONE"></param>
		/// <param name="CONTACT_TOLL_FREE"></param>
		/// <param name="CONTACT_NOTE"></param>
		/// <param name="CONTACT_AGENCY_ID"></param>
		/// <param name="CREATED_BY"></param>
		/// <returns></returns>
		/*public int Add(
			string CONTACT_CODE,
			int CONTACT_TYPE_ID,
			string CONTACT_SALUTATION,
			string CONTACT_POS,
			int INDIVIDUAL_CONTACT_ID,
			string CONTACT_FNAME,
			string CONTACT_MNAME,
			string CONTACT_LNAME,
			string CONTACT_ADD1,
			string CONTACT_ADD2,
			string CONTACT_CITY,
			string CONTACT_STATE,
			string CONTACT_ZIP,
			string CONTACT_COUNTRY,
			string CONTACT_BUSINESS_PHONE,
			string CONTACT_EXT,
			string CONTACT_FAX,
			string CONTACT_MOBILE,
			string CONTACT_EMAIL,
			string CONTACT_PAGER,
			string CONTACT_HOME_PHONE,
			string CONTACT_TOLL_FREE,
			string CONTACT_NOTE,
			int CONTACT_AGENCY_ID,
			int CREATED_BY)
		{ 
			return Add(
			 CONTACT_CODE,
			 CONTACT_TYPE_ID,
			 CONTACT_SALUTATION,
			 CONTACT_POS,
			 INDIVIDUAL_CONTACT_ID,
			 CONTACT_FNAME,
			 CONTACT_MNAME,
			 CONTACT_LNAME,
			 CONTACT_ADD1,
			 CONTACT_ADD2,
			 CONTACT_CITY,
			 CONTACT_STATE,
			 CONTACT_ZIP,
			 CONTACT_COUNTRY,
			 CONTACT_BUSINESS_PHONE,
			 CONTACT_EXT,
			 CONTACT_FAX,
			 CONTACT_MOBILE,
			 CONTACT_EMAIL,
			 CONTACT_PAGER,
			 CONTACT_HOME_PHONE,
			 CONTACT_TOLL_FREE,
			 CONTACT_NOTE,
			 CONTACT_AGENCY_ID,"Y",
			 CREATED_BY,DateTime.Now,CREATED_BY,DateTime.Now,true);
			
		}*/
		/// <summary>
		/// Overload of add that recieves All parameters required to save a contact
		/// </summary>
		/// <param name="CONTACT_CODE"></param>
		/// <param name="CONTACT_TYPE_ID"></param>
		/// <param name="CONTACT_SALUTATION"></param>
		/// <param name="CONTACT_POS"></param>
		/// <param name="INDIVIDUAL_CONTACT_ID"></param>
		/// <param name="CONTACT_FNAME"></param>
		/// <param name="CONTACT_MNAME"></param>
		/// <param name="CONTACT_LNAME"></param>
		/// <param name="CONTACT_ADD1"></param>
		/// <param name="CONTACT_ADD2"></param>
		/// <param name="CONTACT_CITY"></param>
		/// <param name="CONTACT_STATE"></param>
		/// <param name="CONTACT_ZIP"></param>
		/// <param name="CONTACT_COUNTRY"></param>
		/// <param name="CONTACT_BUSINESS_PHONE"></param>
		/// <param name="CONTACT_EXT"></param>
		/// <param name="CONTACT_FAX"></param>
		/// <param name="CONTACT_MOBILE"></param>
		/// <param name="CONTACT_EMAIL"></param>
		/// <param name="CONTACT_PAGER"></param>
		/// <param name="CONTACT_HOME_PHONE"></param>
		/// <param name="CONTACT_TOLL_FREE"></param>
		/// <param name="CONTACT_NOTE"></param>
		/// <param name="CONTACT_AGENCY_ID"></param>
		/// <param name="IS_ACTIVE"></param>
		/// <param name="CREATED_BY"></param>
		/// <param name="CREATED_DATETIME"></param>
		/// <param name="MODIFIED_BY"></param>
		/// <param name="LAST_UPDATED_DATETIME"></param>
		/// <param name="TransactionLogReq"></param>
		/// <returns></returns>
		public int Add(Cms.Model.Maintenance.ClsContactsManagerInfo objContactsManagerInfo,string CalledFrom)
		{
			string		strStoredProc	=	"Proc_InsertContactList";
			DateTime	RecordDate		=	DateTime.Now;
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@CONTACT_CODE",objContactsManagerInfo.CONTACT_CODE);
				objDataWrapper.AddParameter("@CONTACT_TYPE_ID",objContactsManagerInfo.CONTACT_TYPE_ID);
				objDataWrapper.AddParameter("@CONTACT_SALUTATION",objContactsManagerInfo.CONTACT_SALUTATION);
				objDataWrapper.AddParameter("@CONTACT_POS",objContactsManagerInfo.CONTACT_POS);
				if(objContactsManagerInfo.INDIVIDUAL_CONTACT_ID<=0)
					objDataWrapper.AddParameter("@INDIVIDUAL_CONTACT_ID",DBNull.Value);
				else
					objDataWrapper.AddParameter("@INDIVIDUAL_CONTACT_ID",objContactsManagerInfo.INDIVIDUAL_CONTACT_ID);
				objDataWrapper.AddParameter("@CONTACT_FNAME",objContactsManagerInfo.CONTACT_FNAME);
				objDataWrapper.AddParameter("@CONTACT_MNAME",objContactsManagerInfo.CONTACT_MNAME);
				objDataWrapper.AddParameter("@CONTACT_LNAME",objContactsManagerInfo.CONTACT_LNAME);
				objDataWrapper.AddParameter("@CONTACT_ADD1",objContactsManagerInfo.CONTACT_ADD1);
				objDataWrapper.AddParameter("@CONTACT_ADD2",objContactsManagerInfo.CONTACT_ADD2);
				objDataWrapper.AddParameter("@CONTACT_CITY",objContactsManagerInfo.CONTACT_CITY);
				objDataWrapper.AddParameter("@CONTACT_STATE",objContactsManagerInfo.CONTACT_STATE);
				objDataWrapper.AddParameter("@CONTACT_ZIP",objContactsManagerInfo.CONTACT_ZIP);
				objDataWrapper.AddParameter("@CONTACT_COUNTRY",objContactsManagerInfo.CONTACT_COUNTRY);
				objDataWrapper.AddParameter("@CONTACT_BUSINESS_PHONE",objContactsManagerInfo.CONTACT_BUSINESS_PHONE);
				objDataWrapper.AddParameter("@CONTACT_EXT",objContactsManagerInfo.CONTACT_EXT);
				objDataWrapper.AddParameter("@CONTACT_FAX",objContactsManagerInfo.CONTACT_FAX);
				objDataWrapper.AddParameter("@CONTACT_MOBILE",objContactsManagerInfo.CONTACT_MOBILE);
				objDataWrapper.AddParameter("@CONTACT_EMAIL",objContactsManagerInfo.CONTACT_EMAIL);
				objDataWrapper.AddParameter("@CONTACT_PAGER",objContactsManagerInfo.CONTACT_PAGER);
				objDataWrapper.AddParameter("@CONTACT_HOME_PHONE",objContactsManagerInfo.CONTACT_HOME_PHONE);
				objDataWrapper.AddParameter("@CONTACT_TOLL_FREE",objContactsManagerInfo.CONTACT_TOLL_FREE);
				objDataWrapper.AddParameter("@CONTACT_NOTE",objContactsManagerInfo.CONTACT_NOTE);
				objDataWrapper.AddParameter("@CONTACT_AGENCY_ID",objContactsManagerInfo.CONTACT_AGENCY_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",objContactsManagerInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objContactsManagerInfo.CREATED_BY);
                objDataWrapper.AddParameter("@ACTIVITY", objContactsManagerInfo.ACTIVITY);
                if (objContactsManagerInfo.REG_ID_ISSUE_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", objContactsManagerInfo.REG_ID_ISSUE_DATE);
                else
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", null);
                if (objContactsManagerInfo.DATE_OF_BIRTH != DateTime.MinValue)
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", objContactsManagerInfo.DATE_OF_BIRTH);
                else
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", null);

                objDataWrapper.AddParameter("@REG_ID_ISSUE", objContactsManagerInfo.REG_ID_ISSUE);
                objDataWrapper.AddParameter("@CPF_CNPJ", objContactsManagerInfo.CPF_CNPJ);
                objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", objContactsManagerInfo.REGIONAL_IDENTIFICATION);
				if(objContactsManagerInfo.CREATED_DATETIME == DateTime.MinValue)
					objDataWrapper.AddParameter("@CREATED_DATETIME", System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@CREATED_DATETIME",objContactsManagerInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objContactsManagerInfo.MODIFIED_BY);
				if(objContactsManagerInfo.LAST_UPDATED_DATETIME == DateTime.MinValue)
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objContactsManagerInfo.LAST_UPDATED_DATETIME);

                objDataWrapper.AddParameter("@NUMBER", objContactsManagerInfo.NUMBER);
                objDataWrapper.AddParameter("@DISTRICT", objContactsManagerInfo.DISTRICT);
                objDataWrapper.AddParameter("@REGIONAL_ID_TYPE", objContactsManagerInfo.REGIONAL_ID_TYPE);
                objDataWrapper.AddParameter("@NATIONALITY", objContactsManagerInfo.NATIONALITY);
                SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CONTACT_ID",DbType.Int32,ParameterDirection.Output);

				
				int  returnResult = 0;//transactEntry = 0,
//				returnResult				=	objDataWrapper.ExecuteNonQuery(strStoredProc);
//				objDataWrapper.ClearParameteres();
//				if(TransactionLogReq)
//				{
//					string strTransactionDescription = GetCustomizedMessage("/config/Maintenance/TransactionLog/Insert");
//					strTransactionDescription = ReplaceCustomizedMessageTag(strTransactionDescription,fromUserId.ToString(), "New Rec.");
//					transactEntry = AddEntryInTransactionLog(objDataWrapper, fromUserId, strTransactionDescription);
//				}
				if(TransactionLogRequired)
				{
					objContactsManagerInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/addcontact.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objContactsManagerInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	244;
                    if (CalledFrom == "CUSTOMER")
                    {
                        objTransactionInfo.CLIENT_ID = objContactsManagerInfo.INDIVIDUAL_CONTACT_ID;
                      
                    }
                    objTransactionInfo.RECORDED_BY		=	objContactsManagerInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1525", "");// "New Contact Has Been Added";
                   
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int DIV_ID = int.Parse(objSqlParameter.Value.ToString());
				conatctId = int.Parse(objSqlParameter.Value.ToString());
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

		#endregion	

        /// <summary>
        /// Fetches Contact List from MNT_CONTACT_LIST
        /// </summary>
        /// <param name="Customer_ID">Customer ID</param>
        /// <returns>Data Table</returns>
        /// Added by Charles on 17-Mar-10 for Policy Page Implementation
        public static DataTable FetchContactList(string Customer_ID)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.ClearParameteres();

                if(Customer_ID !=null)
                    objDataWrapper.AddParameter("@CUSTOMER_ID", Customer_ID);

                DataSet dsTemp = new DataSet();
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchContactList");
                objDataWrapper.ClearParameteres();                

                if (dsTemp.Tables[0].Rows.Count > 0)
                    return dsTemp.Tables[0];
                else
                    return null;
            }
            catch(Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objDataWrapper = null;
            }
        }

		#region "Update Functions"
		/*
		/// <summary>
		/// Overload of update that recieves minimum parameters that may be required to update a contact
		/// </summary>
		/// <param name="CONTACT_CODE"></param>
		/// <param name="CONTACT_TYPE_ID"></param>
		/// <param name="CONTACT_SALUTATION"></param>
		/// <param name="CONTACT_POS"></param>
		/// <param name="INDIVIDUAL_CONTACT_ID"></param>
		/// <param name="CONTACT_FNAME"></param>
		/// <param name="CONTACT_MNAME"></param>
		/// <param name="CONTACT_LNAME"></param>
		/// <param name="CONTACT_ADD1"></param>
		/// <param name="CONTACT_ADD2"></param>
		/// <param name="CONTACT_CITY"></param>
		/// <param name="CONTACT_STATE"></param>
		/// <param name="CONTACT_ZIP"></param>
		/// <param name="CONTACT_COUNTRY"></param>
		/// <param name="CONTACT_BUSINESS_PHONE"></param>
		/// <param name="CONTACT_EXT"></param>
		/// <param name="CONTACT_FAX"></param>
		/// <param name="CONTACT_MOBILE"></param>
		/// <param name="CONTACT_EMAIL"></param>
		/// <param name="CONTACT_PAGER"></param>
		/// <param name="CONTACT_HOME_PHONE"></param>
		/// <param name="CONTACT_TOLL_FREE"></param>
		/// <param name="CONTACT_NOTE"></param>
		/// <param name="CONTACT_AGENCY_ID"></param>
		/// <param name="MODIFIED_BY"></param>
		/// <param name="oldXML"></param>
		/// <returns></returns>
		public int Update(string CONTACT_CODE,
			int CONTACT_TYPE_ID,
			string CONTACT_SALUTATION,
			string CONTACT_POS,
			int INDIVIDUAL_CONTACT_ID,
			string CONTACT_FNAME,
			string CONTACT_MNAME,
			string CONTACT_LNAME,
			string CONTACT_ADD1,
			string CONTACT_ADD2,
			string CONTACT_CITY,
			string CONTACT_STATE,
			string CONTACT_ZIP,
			string CONTACT_COUNTRY,
			string CONTACT_BUSINESS_PHONE,
			string CONTACT_EXT,
			string CONTACT_FAX,
			string CONTACT_MOBILE,
			string CONTACT_EMAIL,
			string CONTACT_PAGER,
			string CONTACT_HOME_PHONE,
			string CONTACT_TOLL_FREE,
			string CONTACT_NOTE,
			int CONTACT_AGENCY_ID,
			int MODIFIED_BY,string oldXML)
			{
				return Update(
			 CONTACT_CODE,
			 CONTACT_TYPE_ID,
			 CONTACT_SALUTATION,
			 CONTACT_POS,
			 INDIVIDUAL_CONTACT_ID,
			 CONTACT_FNAME,
			 CONTACT_MNAME,
			 CONTACT_LNAME,
			 CONTACT_ADD1,
			 CONTACT_ADD2,
			 CONTACT_CITY,
			 CONTACT_STATE,
			 CONTACT_ZIP,
			 CONTACT_COUNTRY,
			 CONTACT_BUSINESS_PHONE,
			 CONTACT_EXT,
			 CONTACT_FAX,
			 CONTACT_MOBILE,
			 CONTACT_EMAIL,
			 CONTACT_PAGER,
			 CONTACT_HOME_PHONE,
			 CONTACT_TOLL_FREE,
			 CONTACT_NOTE,
			 CONTACT_AGENCY_ID,
			 MODIFIED_BY,DateTime.Now,true,oldXML);
			}
		/// <summary>
		/// Overload of update that recieves all parameters that may be required to update a contact
		/// </summary>
		/// <param name="CONTACT_CODE"></param>
		/// <param name="CONTACT_TYPE_ID"></param>
		/// <param name="CONTACT_SALUTATION"></param>
		/// <param name="CONTACT_POS"></param>
		/// <param name="INDIVIDUAL_CONTACT_ID"></param>
		/// <param name="CONTACT_FNAME"></param>
		/// <param name="CONTACT_MNAME"></param>
		/// <param name="CONTACT_LNAME"></param>
		/// <param name="CONTACT_ADD1"></param>
		/// <param name="CONTACT_ADD2"></param>
		/// <param name="CONTACT_CITY"></param>
		/// <param name="CONTACT_STATE"></param>
		/// <param name="CONTACT_ZIP"></param>
		/// <param name="CONTACT_COUNTRY"></param>
		/// <param name="CONTACT_BUSINESS_PHONE"></param>
		/// <param name="CONTACT_EXT"></param>
		/// <param name="CONTACT_FAX"></param>
		/// <param name="CONTACT_MOBILE"></param>
		/// <param name="CONTACT_EMAIL"></param>
		/// <param name="CONTACT_PAGER"></param>
		/// <param name="CONTACT_HOME_PHONE"></param>
		/// <param name="CONTACT_TOLL_FREE"></param>
		/// <param name="CONTACT_NOTE"></param>
		/// <param name="CONTACT_AGENCY_ID"></param>
		/// <param name="MODIFIED_BY"></param>
		/// <param name="LAST_UPDATED_DATETIME"></param>
		/// <param name="TransactionLogReq"></param>
		/// <param name="oldXML"></param>
		/// <returns></returns>
		public int Update(string CONTACT_CODE, int CONTACT_TYPE_ID, 
			string CONTACT_SALUTATION, string CONTACT_POS, int INDIVIDUAL_CONTACT_ID, string CONTACT_FNAME, 
			string CONTACT_MNAME, string CONTACT_LNAME, string CONTACT_ADD1, string CONTACT_ADD2, 
			string CONTACT_CITY, string CONTACT_STATE, string CONTACT_ZIP, string CONTACT_COUNTRY, 
			string CONTACT_BUSINESS_PHONE, string CONTACT_EXT, string CONTACT_FAX, string CONTACT_MOBILE, 
			string CONTACT_EMAIL, string CONTACT_PAGER, string CONTACT_HOME_PHONE, string CONTACT_TOLL_FREE, 
			string CONTACT_NOTE, int CONTACT_AGENCY_ID,  
			int MODIFIED_BY, DateTime LAST_UPDATED_DATETIME,bool TransactionLogReq,string oldXML)
		{
			XmlDocument		xmlDoc					=		new XmlDocument();
			xmlDoc.LoadXml(oldXML);
			StringBuilder	updateSql				=		new StringBuilder();
			updateSql.Append("UPDATE  MNT_CONTACT_LIST  set ");
			int oldCONTACT_ID = int.Parse(GetNodeValue(xmlDoc,"//CONTACT_ID"));
			string oldCONTACT_CODE = GetNodeValue(xmlDoc,"//CONTACT_CODE");
			int oldCONTACT_TYPE_ID = int.Parse(GetNodeValue(xmlDoc,"//CONTACT_TYPE_ID"));
			string oldCONTACT_SALUTATION = GetNodeValue(xmlDoc,"//CONTACT_SALUTATION");
			string oldCONTACT_POS = GetNodeValue(xmlDoc,"//CONTACT_POS");
			int oldINDIVIDUAL_CONTACT_ID=0;
			if(GetNodeValue(xmlDoc,"//INDIVIDUAL_CONTACT_ID")!="")
				oldINDIVIDUAL_CONTACT_ID = int.Parse(GetNodeValue(xmlDoc,"//INDIVIDUAL_CONTACT_ID"));
			string oldCONTACT_FNAME = GetNodeValue(xmlDoc,"//CONTACT_FNAME");
			string oldCONTACT_MNAME = GetNodeValue(xmlDoc,"//CONTACT_MNAME");
			string oldCONTACT_LNAME = GetNodeValue(xmlDoc,"//CONTACT_LNAME");
			string oldCONTACT_ADD1 = GetNodeValue(xmlDoc,"//CONTACT_ADD1");
			string oldCONTACT_ADD2 = GetNodeValue(xmlDoc,"//CONTACT_ADD2");
			string oldCONTACT_CITY = GetNodeValue(xmlDoc,"//CONTACT_CITY");
			string oldCONTACT_STATE = GetNodeValue(xmlDoc,"//CONTACT_STATE");
			string oldCONTACT_ZIP = GetNodeValue(xmlDoc,"//CONTACT_ZIP");
			string oldCONTACT_COUNTRY = GetNodeValue(xmlDoc,"//CONTACT_COUNTRY");
			string oldCONTACT_BUSINESS_PHONE = GetNodeValue(xmlDoc,"//CONTACT_BUSINESS_PHONE");
			string oldCONTACT_EXT = GetNodeValue(xmlDoc,"//CONTACT_EXT");
			string oldCONTACT_FAX = GetNodeValue(xmlDoc,"//CONTACT_FAX");
			string oldCONTACT_MOBILE = GetNodeValue(xmlDoc,"//CONTACT_MOBILE");
			string oldCONTACT_EMAIL = GetNodeValue(xmlDoc,"//CONTACT_EMAIL");
			string oldCONTACT_PAGER = GetNodeValue(xmlDoc,"//CONTACT_PAGER");
			string oldCONTACT_HOME_PHONE = GetNodeValue(xmlDoc,"//CONTACT_HOME_PHONE");
			string oldCONTACT_TOLL_FREE = GetNodeValue(xmlDoc,"//CONTACT_TOLL_FREE");
			string oldCONTACT_NOTE = GetNodeValue(xmlDoc,"//CONTACT_NOTE");
		
			int oldMODIFIED_BY = int.Parse(GetNodeValue(xmlDoc,"//MODIFIED_BY"));
			
			bool isDataChanged = false;
			if(CONTACT_CODE.Trim()!=oldCONTACT_CODE.Trim())
			{
			//	CheckContactCodeAlreadyExists(CONTACT_CODE);
				updateSql.Append("CONTACT_CODE = '" +ReplaceInvalidCharecter(CONTACT_CODE) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_TYPE_ID!=oldCONTACT_TYPE_ID)
			{
				updateSql.Append("CONTACT_TYPE_ID = " +  CONTACT_TYPE_ID + ", ");
				isDataChanged = true;
			}
			if(CONTACT_SALUTATION!=oldCONTACT_SALUTATION)
			{
				updateSql.Append("CONTACT_SALUTATION = '" + ReplaceInvalidCharecter(CONTACT_SALUTATION) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_POS!=oldCONTACT_POS)
			{
				updateSql.Append("CONTACT_POS = '" + ReplaceInvalidCharecter(CONTACT_POS) + "', ");
				isDataChanged = true;
			}
			if(INDIVIDUAL_CONTACT_ID==0)
			{
				updateSql.Append("INDIVIDUAL_CONTACT_ID = null, ");
				isDataChanged = true;
			}
			else
			{
				if(INDIVIDUAL_CONTACT_ID!=oldINDIVIDUAL_CONTACT_ID)
				{
					updateSql.Append("INDIVIDUAL_CONTACT_ID = " + INDIVIDUAL_CONTACT_ID + ", ");
					isDataChanged = true;
				}
			}
			if(CONTACT_FNAME!=oldCONTACT_FNAME)
			{
				updateSql.Append("CONTACT_FNAME = '" + ReplaceInvalidCharecter(CONTACT_FNAME) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_MNAME!=oldCONTACT_MNAME)
			{
				updateSql.Append("CONTACT_MNAME = '" + ReplaceInvalidCharecter(CONTACT_MNAME) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_LNAME!=oldCONTACT_LNAME)
			{
				updateSql.Append("CONTACT_LNAME = '" + ReplaceInvalidCharecter(CONTACT_LNAME) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_ADD1!=oldCONTACT_ADD1)
			{
				updateSql.Append("CONTACT_ADD1 = '" + ReplaceInvalidCharecter(CONTACT_ADD1) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_ADD2!=oldCONTACT_ADD2)
			{
				updateSql.Append("CONTACT_ADD2 = '" + ReplaceInvalidCharecter(CONTACT_ADD2) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_CITY!=oldCONTACT_CITY)
			{
				updateSql.Append("CONTACT_CITY = '" + ReplaceInvalidCharecter(CONTACT_CITY) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_STATE!=oldCONTACT_STATE)
			{
				updateSql.Append("CONTACT_STATE = '" + ReplaceInvalidCharecter(CONTACT_STATE) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_ZIP!=oldCONTACT_ZIP)
			{
				updateSql.Append("CONTACT_ZIP = '" + ReplaceInvalidCharecter(CONTACT_ZIP) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_COUNTRY!=oldCONTACT_COUNTRY)
			{
				updateSql.Append("CONTACT_COUNTRY = '" + ReplaceInvalidCharecter(CONTACT_COUNTRY) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_BUSINESS_PHONE!=oldCONTACT_BUSINESS_PHONE)
			{
				updateSql.Append("CONTACT_BUSINESS_PHONE = '" + ReplaceInvalidCharecter(CONTACT_BUSINESS_PHONE) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_EXT!=oldCONTACT_EXT)
			{
				updateSql.Append("CONTACT_EXT = '" + ReplaceInvalidCharecter(CONTACT_EXT) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_FAX!=oldCONTACT_FAX)
			{
				updateSql.Append("CONTACT_FAX = '" + ReplaceInvalidCharecter(CONTACT_FAX) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_MOBILE!=oldCONTACT_MOBILE)
			{
				updateSql.Append("CONTACT_MOBILE = '" + ReplaceInvalidCharecter(CONTACT_MOBILE) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_EMAIL!=oldCONTACT_EMAIL)
			{
				updateSql.Append("CONTACT_EMAIL = '" + ReplaceInvalidCharecter(CONTACT_EMAIL) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_PAGER!=oldCONTACT_PAGER)
			{
				updateSql.Append("CONTACT_PAGER = '" + ReplaceInvalidCharecter(CONTACT_PAGER) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_HOME_PHONE!=oldCONTACT_HOME_PHONE)
			{
				updateSql.Append("CONTACT_HOME_PHONE = '" + ReplaceInvalidCharecter(CONTACT_HOME_PHONE) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_TOLL_FREE!=oldCONTACT_TOLL_FREE)
			{
				updateSql.Append("CONTACT_TOLL_FREE = '" + ReplaceInvalidCharecter(CONTACT_TOLL_FREE) + "', ");
				isDataChanged = true;
			}
			if(CONTACT_NOTE!=oldCONTACT_NOTE)
			{
				CONTACT_NOTE = ReplaceInvalidCharecter(CONTACT_NOTE);
				if(CONTACT_NOTE.Length>256)
					CONTACT_NOTE = CONTACT_NOTE.Substring(0,255);
				updateSql.Append("CONTACT_NOTE = '" + CONTACT_NOTE + "', ");
				isDataChanged = true;
			}
//			if(CONTACT_AGENCY_ID!=oldCONTACT_AGENCY_ID)
//			{
//				updateSql.Append("CONTACT_AGENCY_ID = " + CONTACT_AGENCY_ID + ", ");
//			}
			if(MODIFIED_BY!=oldMODIFIED_BY)
			{
				updateSql.Append("MODIFIED_BY = " + MODIFIED_BY + ", ");
				isDataChanged = true;
			}
		
			if(isDataChanged)
			{
				updateSql.Append("LAST_UPDATED_DATETIME = '" + LAST_UPDATED_DATETIME +"'");
			
				int lengthSql						=	updateSql.ToString().LastIndexOf(",");
				int returnResult = 0;
//				if(lengthSql	!=	-1)
//				{
					updateSql.Append(" where CONTACT_ID = " + oldCONTACT_ID);

					string strUpdateSql = "IF Not Exists(SELECT CONTACT_CODE "
						+ " FROM MNT_CONTACT_LIST" 
						+ " WHERE CONTACT_CODE='" + ReplaceInvalidCharecter(CONTACT_CODE)
						+ "' AND CONTACT_ID<>" + oldCONTACT_ID 
						+ ")"
						+ updateSql.ToString();
					DataWrapper objDataWrapper			=	new DataWrapper(ConnStr, CommandType.Text,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
					try
					{
						returnResult					=	objDataWrapper.ExecuteNonQuery(strUpdateSql);
						/*if(TransactionLogReq)
						{
						string strTransactionDescription = GetCustomizedMessage("/config/xxxx/TransactionLog/Update");
						strTransactionDescription = ReplaceCustomizedMessageTag(strTransactionDescription,fromUserId.ToString(), PK_Field.ToString());
						transactEntry = 1;
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
						if(xmlDoc != null) xmlDoc = null;
						if(updateSql != null) updateSql = null;
						if(objDataWrapper != null) objDataWrapper.Dispose();
					}
//				}
//				else
//				{
//					return 0;
//				}
			}
			else
			{
				return 1;
			}
			
		}
		*/
		#endregion

		public int Update(Cms.Model.Maintenance.ClsContactsManagerInfo objContactsManagerInfo, Cms.Model.Maintenance.ClsContactsManagerInfo objOldContactsManagerInfo,string CalledFrom)
		{
			string	strStoredProc	=	"Proc_UpdateContactList";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CONTACT_ID",objContactsManagerInfo.CONTACT_ID);
				objDataWrapper.AddParameter("@CONTACT_CODE",objContactsManagerInfo.CONTACT_CODE);
				objDataWrapper.AddParameter("@CONTACT_TYPE_ID",objContactsManagerInfo.CONTACT_TYPE_ID);
				objDataWrapper.AddParameter("@CONTACT_SALUTATION",objContactsManagerInfo.CONTACT_SALUTATION);
				objDataWrapper.AddParameter("@CONTACT_POS",objContactsManagerInfo.CONTACT_POS);
				if(objContactsManagerInfo.INDIVIDUAL_CONTACT_ID<=0)
					objDataWrapper.AddParameter("@INDIVIDUAL_CONTACT_ID",DBNull.Value);
				else
					objDataWrapper.AddParameter("@INDIVIDUAL_CONTACT_ID",objContactsManagerInfo.INDIVIDUAL_CONTACT_ID);
				objDataWrapper.AddParameter("@CONTACT_FNAME",objContactsManagerInfo.CONTACT_FNAME);
				objDataWrapper.AddParameter("@CONTACT_MNAME",objContactsManagerInfo.CONTACT_MNAME);
				objDataWrapper.AddParameter("@CONTACT_LNAME",objContactsManagerInfo.CONTACT_LNAME);
				objDataWrapper.AddParameter("@CONTACT_ADD1",objContactsManagerInfo.CONTACT_ADD1);
				objDataWrapper.AddParameter("@CONTACT_ADD2",objContactsManagerInfo.CONTACT_ADD2);
				objDataWrapper.AddParameter("@CONTACT_CITY",objContactsManagerInfo.CONTACT_CITY);
				objDataWrapper.AddParameter("@CONTACT_STATE",objContactsManagerInfo.CONTACT_STATE);
				objDataWrapper.AddParameter("@CONTACT_ZIP",objContactsManagerInfo.CONTACT_ZIP);
				objDataWrapper.AddParameter("@CONTACT_COUNTRY",objContactsManagerInfo.CONTACT_COUNTRY);
				objDataWrapper.AddParameter("@CONTACT_BUSINESS_PHONE",objContactsManagerInfo.CONTACT_BUSINESS_PHONE);
				objDataWrapper.AddParameter("@CONTACT_EXT",objContactsManagerInfo.CONTACT_EXT);
				objDataWrapper.AddParameter("@CONTACT_FAX",objContactsManagerInfo.CONTACT_FAX);
				objDataWrapper.AddParameter("@CONTACT_MOBILE",objContactsManagerInfo.CONTACT_MOBILE);
				objDataWrapper.AddParameter("@CONTACT_EMAIL",objContactsManagerInfo.CONTACT_EMAIL);
				objDataWrapper.AddParameter("@CONTACT_PAGER",objContactsManagerInfo.CONTACT_PAGER);
				objDataWrapper.AddParameter("@CONTACT_HOME_PHONE",objContactsManagerInfo.CONTACT_HOME_PHONE);
				objDataWrapper.AddParameter("@CONTACT_TOLL_FREE",objContactsManagerInfo.CONTACT_TOLL_FREE);
				objDataWrapper.AddParameter("@CONTACT_NOTE",objContactsManagerInfo.CONTACT_NOTE);
				objDataWrapper.AddParameter("@CONTACT_AGENCY_ID",objContactsManagerInfo.CONTACT_AGENCY_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",objContactsManagerInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@ACTIVITY", objContactsManagerInfo.ACTIVITY);
                if (objContactsManagerInfo.REG_ID_ISSUE_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", objContactsManagerInfo.REG_ID_ISSUE_DATE);
                else
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", null);
                if (objContactsManagerInfo.DATE_OF_BIRTH != DateTime.MinValue)
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", objContactsManagerInfo.DATE_OF_BIRTH);
                else
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", null);

                objDataWrapper.AddParameter("@REG_ID_ISSUE", objContactsManagerInfo.REG_ID_ISSUE);
                objDataWrapper.AddParameter("@CPF_CNPJ", objContactsManagerInfo.CPF_CNPJ);
                objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", objContactsManagerInfo.REGIONAL_IDENTIFICATION);
				objDataWrapper.AddParameter("@MODIFIED_BY",objContactsManagerInfo.MODIFIED_BY);
				if(objContactsManagerInfo.LAST_UPDATED_DATETIME == DateTime.MinValue)
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objContactsManagerInfo.LAST_UPDATED_DATETIME);
                objDataWrapper.AddParameter("@NUMBER", objContactsManagerInfo.NUMBER);
                objDataWrapper.AddParameter("@DISTRICT", objContactsManagerInfo.DISTRICT);
			  objDataWrapper.AddParameter("@REGIONAL_ID_TYPE", objContactsManagerInfo.REGIONAL_ID_TYPE);
              objDataWrapper.AddParameter("@NATIONALITY", objContactsManagerInfo.NATIONALITY);
				if(TransactionLogRequired) 
				{

					objContactsManagerInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cmsweb/Maintenance/addcontact.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldContactsManagerInfo,objContactsManagerInfo);
					objTransactionInfo.TRANS_TYPE_ID	=	245;
                    objTransactionInfo.CLIENT_ID = objTransactionInfo.CLIENT_ID;
                    if (CalledFrom == "CUSTOMER")
                        objTransactionInfo.CLIENT_ID = objContactsManagerInfo.INDIVIDUAL_CONTACT_ID;

                    objTransactionInfo.RECORDED_BY = objContactsManagerInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1526", "");// "Contact List Has Been Modified";
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

		#region "Fill Drop down Functions"
		public static void GetContactTypesInDropDown(DropDownList objDropDownList,int Lang_Id)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LANG_ID", BlCommon.ClsCommon.BL_LANG_ID);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillContactTypeDropDown").Tables[0];
			objDropDownList.DataSource = objDataTable;
			objDropDownList.DataTextField = "CONTACT_TYPE_DESC";
			objDropDownList.DataValueField = "CONTACT_TYPE_ID";
			
			objDropDownList.DataBind();
			objDropDownList.Items.Insert(0,"");
		}
		public static string GetContactName(string CONTACT_ID)
		{
			return DataWrapper.ExecuteScalar(ConnStr,CommandType.Text,"select CONTACT_FNAME+' '+CONTACT_LNAME from MNT_CONTACT_LIST where CONTACT_ID="+CONTACT_ID).ToString();
		}
		#endregion

		#region "Get Xml Methods"
		public static string GetXmlForPageControls(string strContactId,string contactTypeId,string EntityId)
		{
			string strSql = GetQuery(strContactId,contactTypeId,EntityId);	
			
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			return objDataSet.GetXml();
		}
		private static string GetQuery(string strContactId,string contactTypeId,string EntityId)
		{
            string strSql = "select cList.CONTACT_ID,cList.NUMBER,cList.DISTRICT,cList.CONTACT_CODE,cList.CONTACT_ADD1,cList.CONTACT_BUSINESS_PHONE,cList.CONTACT_FAX,cList.CONTACT_MOBILE,cList.CONTACT_LNAME,cList.CONTACT_FNAME,cList.CONTACT_MNAME,cList.CONTACT_ADD2,cList.CONTACT_CITY,cList.CONTACT_STATE,cList.CONTACT_ZIP,cList.CONTACT_COUNTRY,cList.CONTACT_EXT,cList.CONTACT_EMAIL,cList.CONTACT_PAGER,cList.CONTACT_HOME_PHONE,cList.CONTACT_TOLL_FREE,cList.CONTACT_NOTE,cList.IS_ACTIVE,cList.CONTACT_SALUTATION,CONTACT_POS,cList.CONTACT_TYPE_ID,cList.INDIVIDUAL_CONTACT_ID,cList.MODIFIED_BY,cList.CPF_CNPJ,CONVERT(VARCHAR,cList.DATE_OF_BIRTH,CASE WHEN " + ClsCommon.BL_LANG_ID + "=2 THEN 103 ELSE 103 END) DATE_OF_BIRTH,cLIST.REGIONAL_IDENTIFICATION,CONVERT(VARCHAR, cList.REG_ID_ISSUE_DATE,CASE WHEN " + ClsCommon.BL_LANG_ID + "=2 THEN 103 ELSE 101 END)  REG_ID_ISSUE_DATE,cList.ACTIVITY,cList.REG_ID_ISSUE,cList.REGIONAL_ID_TYPE,cList.NATIONALITY ";
			//cid1,ccode2,add1 4,phone5,fax6,mobile7,lname8,fname9,mname10,add2 11,																												city12,state13,province14,zip15,country16,ext17,email18,														pager19,hphone20,tollfree21,note22 , IS_ACTIVE 23, CONTACT_SALUTATION 24,CONTACT_POS 25,CONTACT_TYPE_ID26,INDIVIDUAL_CONTACT_ID27,GetQuery32
			//1,2,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27
			if(EntityId==null || EntityId.Length==0)
			{
				strSql += " from MNT_CONTACT_LIST cList, MNT_CONTACT_TYPES cTypes";
				strSql += " where CONTACT_ID="+strContactId + " and cList.CONTACT_TYPE_ID = cTypes.CONTACT_TYPE_ID";
			}
			else
			{
				switch(contactTypeId)
				{
					case "1"://Agency
						strSql += ",AGENCY_DISPLAY_NAME as INDIVIDUAL_CONTACT_NAME";
						strSql += " from MNT_CONTACT_LIST cList, MNT_CONTACT_TYPES cTypes";
						strSql += 	",MNT_AGENCY_LIST";
						strSql += " where CONTACT_ID="+strContactId + " and cList.CONTACT_TYPE_ID = cTypes.CONTACT_TYPE_ID";
						strSql += " and AGENCY_ID="+EntityId;
						break;
					case "2"://Customer
						strSql += ",CUSTOMER_FIRST_NAME + ' ' +CUSTOMER_LAST_NAME as INDIVIDUAL_CONTACT_NAME";
						strSql += " from MNT_CONTACT_LIST cList, MNT_CONTACT_TYPES cTypes";
						strSql += 	",CLT_CUSTOMER_LIST";
						strSql += " where CONTACT_ID="+strContactId + " and cList.CONTACT_TYPE_ID = cTypes.CONTACT_TYPE_ID";
						strSql += " and CUSTOMER_ID="+EntityId;
						break;
					case "3"://Finance Company
						strSql += ",COMPANY_NAME as INDIVIDUAL_CONTACT_NAME";//***
						strSql += " from MNT_CONTACT_LIST cList, MNT_CONTACT_TYPES cTypes";
						strSql += 	",MNT_FINANCE_COMPANY_LIST";//***
						strSql += " where CONTACT_ID="+strContactId + " and cList.CONTACT_TYPE_ID = cTypes.CONTACT_TYPE_ID";
						strSql += " and COMPANY_ID="+EntityId;//***
						break;
					case "4"://Holder
						strSql += ",HOLDER_NAME as INDIVIDUAL_CONTACT_NAME";//***
						strSql += " from MNT_CONTACT_LIST cList, MNT_CONTACT_TYPES cTypes";
						strSql += 	",MNT_HOLDER_INTEREST_LIST";//***
						strSql += " where CONTACT_ID="+strContactId + " and cList.CONTACT_TYPE_ID = cTypes.CONTACT_TYPE_ID";
						strSql += " and HOLDER_ID="+EntityId;//***
						break;
					case "5"://Industry Provider //under construction
						/*	strSql += ",COMPANY_NAME as INDIVIDUAL_CONTACT_NAME";//***
							strSql += " from MNT_CONTACT_LIST cList, MNT_CONTACT_TYPES cTypes";
							strSql += 	",MNT_FINANCE_COMPANY_LIST";//***
							strSql += " where CONTACT_ID="+strContactId + " and cList.CONTACT_TYPE_ID = cTypes.CONTACT_TYPE_ID";
							strSql += " and COMPANY_ID="+EntityId;//***
							break;*/
					case "6"://Personal
						strSql += ",CONTACT_FNAME + ' ' + CONTACT_LNAME as INDIVIDUAL_CONTACT_NAME";//***
						strSql += " from MNT_CONTACT_LIST cList, MNT_CONTACT_TYPES cTypes";
						strSql += " where CONTACT_ID="+strContactId + " and cList.CONTACT_TYPE_ID = cTypes.CONTACT_TYPE_ID";
						break;
					case "7"://Tax Entity
						strSql += ",TAX_NAME as INDIVIDUAL_CONTACT_NAME";//***
						strSql += " from MNT_CONTACT_LIST cList, MNT_CONTACT_TYPES cTypes";
						strSql += 	",MNT_TAX_ENTITY_LIST";//***
						strSql += " where CONTACT_ID="+strContactId + " and cList.CONTACT_TYPE_ID = cTypes.CONTACT_TYPE_ID";
						strSql += " and TAX_ID="+EntityId;//***
						break;
					case "8"://Vendor
						strSql += ",VENDOR_FNAME + ' ' +VENDOR_LNAME as INDIVIDUAL_CONTACT_NAME";//***
						strSql += " from MNT_CONTACT_LIST cList, MNT_CONTACT_TYPES cTypes";
						strSql += 	",MNT_VENDOR_LIST";//***
						strSql += " where CONTACT_ID="+strContactId + " and cList.CONTACT_TYPE_ID = cTypes.CONTACT_TYPE_ID";
						strSql += " and VENDOR_ID="+EntityId;//***
						break;
					default:
						
						break;
				}
			}
            
			return strSql;
		}
		#endregion

		#region IDisposable Members
		/// <summary>
		/// Frees the resources occupied by the object.
		/// </summary>
		public void Dispose()
		{
			base.Dispose();
		}

		#endregion

		public int Delete(int intContactId, string customInfo, int modifiedBy, int iClientid)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			string	strStoredProc =	"Proc_DeleteContact";
			objDataWrapper.AddParameter("@CONTACT_ID",intContactId);
			int intResult=0;// = objDataWrapper.ExecuteNonQuery(strStoredProc);	
			if(TransactionLogRequired) 
			{
				//ObjProfitCenterInfo.TransactLabel  = ClsCommon.MapTransactionLabel("cmsWeb/Maintenance/addProfitcenter.aspx.resx");
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				//strTranXML = objBuilder.GetTransactionLogXML(objOldAddProfitCenterInfo,ObjProfitCenterInfo);
				objTransactionInfo.TRANS_TYPE_ID	=	250;
				objTransactionInfo.RECORDED_BY		=	modifiedBy;
                objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1532", "");//"Contact List Has Been deleted";				
				objTransactionInfo.CUSTOM_INFO      = customInfo;
                objTransactionInfo.CLIENT_ID = iClientid;
				intResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

			}
			else
			{
				intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			return intResult;
		
		}

	}
}

