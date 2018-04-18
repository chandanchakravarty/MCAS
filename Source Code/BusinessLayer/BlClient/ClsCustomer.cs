/******************************************************************************************
<Author					: -	Vijay
<Start Date				: -	
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Shrikant Bhatt
<Modified By			: - 11-04-2005
<Purpose				: - Applying model approach for update and add.

<Modified Date			: - Shrikant Bhatt
<Modified By			: - 12-04-2005
<Purpose				: - Applying model approach(transaction) for update ,add and activited/deactivited

<Modified Date			: - Pradeep Iyer
<Modified By			: - 14-04-2005
<Purpose				: - Changed GetUpdateSQL2 to GetUpdateSQL in update method

<Modified Date			: - Shrikant Bhatt
<Modified By			: - 19-04-2005
<Purpose				: - Changed GetUpdateSQL2 to GetUpdateSQL in update method

<Modified Date			: - 27 May,2005
<Modified By			: - Vijay 
<Purpose				: - Add populate client addres function

<Modified Date			: - Anshuman
<Modified By			: - June 08, 2005
<Purpose				: - transaction description modified

<Modified Date			: - Dec. 08 2005
<Modified By			: - Vijay Arora
<Purpose				: - Added the Policy Header Functions.
*******************************************************************************************/ 
using System;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data;
using System.Configuration;
using Cms.Model.Client;
using System.Data.SqlClient;
//using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using Cms.Model;
using System.Data.SqlTypes; 

namespace Cms.BusinessLayer.BlClient
{
	/// <summary>
	/// Summary description for ClsCustomer.
	/// </summary>
	public class ClsCustomer : Cms.BusinessLayer.BlClient.ClsClient
	{
		
		private const string CLT_CUSTOMER_LIST			= "CLT_CUSTOMER_LIST";
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateCustomer";
		private bool boolTransactionRequired			= true;
		private const string strStoredProcAN			= "Proc_UpdateAttentionNotes";
		private const string CUST_TYPE_COMMERCIAL = "11109";
		private const string CUST_TYPE_PERSONAL = "11110";

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

		public ClsCustomer()
		{
			base.strActivateDeactivateProcedure = ACTIVATE_DEACTIVATE_PROC;
		}
		
		/// <summary>
		/// Gets the lookup valuesused in the Customer page
		/// </summary>
		/// <returns></returns>
		public static DataSet GetLookups()
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

            objDataWrapper.AddParameter("@LANG_ID", BL_LANG_ID); //Added by Charles on 20-Apr-2010 for Multilingual Implementation
			DataSet dsLookup = objDataWrapper.ExecuteDataSet("Proc_GetCustomerLookup");

			return dsLookup;
		}

		public int SetInsuranceScore(Cms.Model.Client.ClsCustomerInfo objInfo,Cms.Model.Client.ClsCustomerInfo objOldInfo)
		{
			string		strStoredProc	=	"Proc_SetInsuranceScore";
			DateTime	RecordDate		=	DateTime.Now;
			int result=0;
			
			//Set transaction label in the new object, if required
			/* if ( this.boolTransactionRequired)
				{
					objCustomerInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"client\aspx\AddCustomer.aspx.resx");
				}*/

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CustomerId,SqlDbType.Int,ParameterDirection.Input);
			objDataWrapper.AddParameter("@SCORE",objInfo.CustomerInsuranceScore,SqlDbType.Int,ParameterDirection.Input);
			objDataWrapper.AddParameter("@REASON_CODE",objInfo.FACTOR1,SqlDbType.VarChar,ParameterDirection.Input);
			objDataWrapper.AddParameter("@REASON_CODE2",objInfo.FACTOR2,SqlDbType.VarChar,ParameterDirection.Input);
			objDataWrapper.AddParameter("@REASON_CODE3",objInfo.FACTOR3,SqlDbType.VarChar,ParameterDirection.Input);
			objDataWrapper.AddParameter("@REASON_CODE4",objInfo.FACTOR4,SqlDbType.VarChar,ParameterDirection.Input);
			objDataWrapper.AddParameter("@CREATED_BY",objInfo.MODIFIED_BY,SqlDbType.VarChar,ParameterDirection.Input);
			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@result",null,SqlDbType.Int,ParameterDirection.Output);
				
			int returnResult = 0;
				
			try
			{
				//if transaction required
				/*
				if(TransactionLogRequired)
				{   
					//objInfo.TransactLabel	=	ClsCommon.MapTransactionLabel(@"client\aspx\AddCustomer.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = getInsScoreTransXML(objInfo,objOldInfo); //objBuilder.GetTransactionLogXML(objOldInfo,objInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.CLIENT_ID		=	objInfo.CustomerId;
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Insurance Score has been fetched.";//functionality is achieved";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                    
				}
				else//if no transaction required
				{*/
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
				//}

				result = int.Parse(objSqlParameter.Value.ToString());
                    
				objDataWrapper.ClearParameteres();
				//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);
                    
				return result;
			}
			catch(Exception ex)
			{
				//objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		public int SetCapitalInsuranceScore(Cms.Model.Client.ClsCustomerInfo objInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_SetInsuranceScore";
			DateTime	RecordDate		=	DateTime.Now;
			int result=0;
			
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CustomerId,SqlDbType.Int,ParameterDirection.Input);
			objDataWrapper.AddParameter("@SCORE",objInfo.CustomerInsuranceScore,SqlDbType.Int,ParameterDirection.Input);
			objDataWrapper.AddParameter("@REASON_CODE",objInfo.FACTOR1,SqlDbType.VarChar,ParameterDirection.Input);
			objDataWrapper.AddParameter("@REASON_CODE2",objInfo.FACTOR2,SqlDbType.VarChar,ParameterDirection.Input);
			objDataWrapper.AddParameter("@REASON_CODE3",objInfo.FACTOR3,SqlDbType.VarChar,ParameterDirection.Input);
			objDataWrapper.AddParameter("@REASON_CODE4",objInfo.FACTOR4,SqlDbType.VarChar,ParameterDirection.Input);
			objDataWrapper.AddParameter("@CREATED_BY",objInfo.MODIFIED_BY,SqlDbType.VarChar,ParameterDirection.Input);
			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@result",null,SqlDbType.Int,ParameterDirection.Output);
			int returnResult = 0;
			returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
			result = int.Parse(objSqlParameter.Value.ToString());
			objDataWrapper.ClearParameteres();
			return result;
		}
		private string getInsScoreTransXML(Cms.Model.Client.ClsCustomerInfo objInfo,Cms.Model.Client.ClsCustomerInfo objOldInfo)
		{
			XmlDocument doc = new XmlDocument();
			XmlElement objElement=null;
			string strTRAN_XML = "";
			objElement = doc.CreateElement("LabelFieldMapping");
			doc.AppendChild (objElement); 

			if (objOldInfo.CustomerInsuranceScore!=objInfo.CustomerInsuranceScore)
			{
				objElement = doc.CreateElement("Map");
				objElement.SetAttribute("label","Insurance Score");
				objElement.SetAttribute("field","CUSTOMER_INSURANCE_SCORE");
				objElement.SetAttribute("OldValue",objOldInfo.CustomerInsuranceScore.ToString());
				objElement.SetAttribute("NewValue",objInfo.CustomerInsuranceScore.ToString());
				doc.DocumentElement.AppendChild(objElement);
			}
			if (objOldInfo.CustomerInsuranceReceivedDate!=objInfo.CustomerInsuranceReceivedDate)
			{
				objElement = doc.CreateElement("Map");
				objElement.SetAttribute("label","Received Date");
				objElement.SetAttribute("field","CUSTOMER_INSURANCE_RECEIVED_DATE");
				objElement.SetAttribute("OldValue",objOldInfo.CustomerInsuranceReceivedDate == DateTime.MinValue?"":objOldInfo.CustomerInsuranceReceivedDate.ToShortDateString());
				objElement.SetAttribute("NewValue",objInfo.CustomerInsuranceReceivedDate.ToShortDateString());
				doc.DocumentElement.AppendChild(objElement);
			}
			if (objOldInfo.CustomerReasonCode!=objInfo.FACTOR1)
			{
				objElement = doc.CreateElement("Map");
				objElement.SetAttribute("label","Reason Code 1");
				objElement.SetAttribute("field","CUSTOMER_REASON_CODE");
				objElement.SetAttribute("OldValue",objOldInfo.CustomerReasonCode);
				objElement.SetAttribute("NewValue",objInfo.FACTOR1);
				doc.DocumentElement.AppendChild(objElement);
			}
			if (objOldInfo.CustomerReasonCode2!=objInfo.FACTOR2)
			{
				objElement = doc.CreateElement("Map");
				objElement.SetAttribute("label","Reason Code 2");
				objElement.SetAttribute("field","CUSTOMER_REASON_CODE2");
				objElement.SetAttribute("OldValue",objOldInfo.CustomerReasonCode2 );
				objElement.SetAttribute("NewValue",objInfo.FACTOR2);
				doc.DocumentElement.AppendChild(objElement);
			}
			if (objOldInfo.CustomerReasonCode3!=objInfo.FACTOR3)
			{
				objElement = doc.CreateElement("Map");
				objElement.SetAttribute("label","Reason Code 3");
				objElement.SetAttribute("field","CUSTOMER_REASON_CODE3");
				objElement.SetAttribute("OldValue",objOldInfo.CustomerReasonCode3);
				objElement.SetAttribute("NewValue",objInfo.FACTOR3);
				doc.DocumentElement.AppendChild(objElement);
			}
			if (objOldInfo.CustomerReasonCode4!=objInfo.FACTOR4)
			{
				objElement = doc.CreateElement("Map");
				objElement.SetAttribute("label","Reason Code 4");
				objElement.SetAttribute("field","CUSTOMER_REASON_CODE4");
				objElement.SetAttribute("OldValue",objOldInfo.CustomerReasonCode4);
				objElement.SetAttribute("NewValue",objInfo.FACTOR4);
				doc.DocumentElement.AppendChild(objElement);
			}

			strTRAN_XML =  doc.InnerXml;
			return strTRAN_XML;
			
		}
		#region "New approach"
		/// <summary>
		/// Inserting Customer details
		/// </summary>
		/// <param name="objCustomerInfo">Modal Object for Customer</param>
		/// <returns></returns>
        public int Add(Cms.Model.Client.ClsCustomerInfo objCustomerInfo, out int iCustomerID,string Name)
        {
            string strStoredProc = "Proc_InsertCustomerGenInfo";


            DateTime RecordDate = DateTime.Now;

            //Set transaction label in the new object, if required
            if (this.boolTransactionRequired)
            {
                objCustomerInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"client\aspx\AddCustomer.aspx.resx");
            }
           

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            objDataWrapper.AddParameter("@CUSTOMER_ID", objCustomerInfo.CustomerId, SqlDbType.Int, ParameterDirection.Output);
            objDataWrapper.AddParameter("@CUSTOMER_CODE", objCustomerInfo.CustomerCode);
            objDataWrapper.AddParameter("@CUSTOMER_TYPE", objCustomerInfo.CustomerType);

           
            if (objCustomerInfo.CustomerParent != 0)
            {
                objDataWrapper.AddParameter("@CUSTOMER_PARENT", objCustomerInfo.CustomerParent);
            }
            else
            {
                objDataWrapper.AddParameter("@CUSTOMER_PARENT", null);
            }
           
            objDataWrapper.AddParameter("@CUSTOMER_FIRST_NAME", objCustomerInfo.CustomerFirstName);
            if (objCustomerInfo.CustomerType == CUST_TYPE_PERSONAL)
            {
                objDataWrapper.AddParameter("@CUSTOMER_MIDDLE_NAME", objCustomerInfo.CustomerMiddleName);
                objDataWrapper.AddParameter("@CUSTOMER_LAST_NAME", objCustomerInfo.CustomerLastName);
            }
            else
            {
                objDataWrapper.AddParameter("@CUSTOMER_MIDDLE_NAME", null);
                objDataWrapper.AddParameter("@CUSTOMER_LAST_NAME", null);
            }
            

            objDataWrapper.AddParameter("@CUSTOMER_ADDRESS1", objCustomerInfo.CustomerAddress1);
            objDataWrapper.AddParameter("@CUSTOMER_ADDRESS2", objCustomerInfo.CustomerAddress2);


            objDataWrapper.AddParameter("@CUSTOMER_CITY", objCustomerInfo.CustomerCity);
            objDataWrapper.AddParameter("@CUSTOMER_COUNTRY", objCustomerInfo.CustomerCountry);
            objDataWrapper.AddParameter("@CUSTOMER_STATE", objCustomerInfo.CustomerState);
            objDataWrapper.AddParameter("@CUSTOMER_ZIP", objCustomerInfo.CustomerZip);
            
            objDataWrapper.AddParameter("@CUSTOMER_CONTACT_NAME", objCustomerInfo.CustomerContactName);
            objDataWrapper.AddParameter("@CUSTOMER_BUSINESS_PHONE", objCustomerInfo.CustomerBusinessPhone);
            objDataWrapper.AddParameter("@CUSTOMER_EXT", objCustomerInfo.CustomerExt);
            objDataWrapper.AddParameter("@EMP_EXT ", objCustomerInfo.EMP_EXT);
            objDataWrapper.AddParameter("@CUSTOMER_HOME_PHONE", objCustomerInfo.CustomerHomePhone);
            objDataWrapper.AddParameter("@CUSTOMER_MOBILE", objCustomerInfo.CustomerMobile);
            objDataWrapper.AddParameter("@CUSTOMER_FAX", objCustomerInfo.CustomerFax);
           
            objDataWrapper.AddParameter("@CUSTOMER_Email", objCustomerInfo.CustomerEmail);
          
            if (objCustomerInfo.CustomerAgencyId != 0)
                objDataWrapper.AddParameter("@CUSTOMER_AGENCY_ID", objCustomerInfo.CustomerAgencyId);
            else
                objDataWrapper.AddParameter("@CUSTOMER_AGENCY_ID", null);

            
         
            objDataWrapper.AddParameter("@PREFIX", objCustomerInfo.PREFIX);
            objDataWrapper.AddParameter("@PER_CUST_MOBILE", objCustomerInfo.PER_CUST_MOBILE);
            objDataWrapper.AddParameter("@CREATED_BY", objCustomerInfo.CREATED_BY);
            objDataWrapper.AddParameter("@CREATED_DATETIME", objCustomerInfo.CREATED_DATETIME);
           
          

            //--------Start Addded By Lalit on March 11,2010

            objDataWrapper.AddParameter("@CPF_CNPJ", objCustomerInfo.CPF_CNPJ);
            objDataWrapper.AddParameter("@NUMBER", objCustomerInfo.NUMBER);
           
            objDataWrapper.AddParameter("@DISTRICT", objCustomerInfo.DISTRICT);
            
            objDataWrapper.AddParameter("@MAIN_TITLE", objCustomerInfo.MAIN_TITLE);
            objDataWrapper.AddParameter("@MAIN_POSITION", objCustomerInfo.MAIN_POSITION);
            objDataWrapper.AddParameter("@MAIN_CPF_CNPJ", objCustomerInfo.MAIN_CPF_CNPJ);
            objDataWrapper.AddParameter("@MAIN_ADDRESS", objCustomerInfo.MAIN_ADDRESS);
            objDataWrapper.AddParameter("@MAIN_NUMBER", objCustomerInfo.MAIN_NUMBER);
            objDataWrapper.AddParameter("@MAIN_COMPLIMENT", objCustomerInfo.MAIN_COMPLIMENT);
            objDataWrapper.AddParameter("@MAIN_DISTRICT", objCustomerInfo.MAIN_DISTRICT);
            objDataWrapper.AddParameter("@MAIN_NOTE", objCustomerInfo.MAIN_NOTE);
            objDataWrapper.AddParameter("@MAIN_CONTACT_CODE", objCustomerInfo.MAIN_CONTACT_CODE);
            objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", objCustomerInfo.REGIONAL_IDENTIFICATION);
          //  objDataWrapper.AddParameter("@REG_ID_ISSUE", objCustomerInfo.REG_ID_ISSUE);
            if (objCustomerInfo.REG_ID_ISSUE != DateTime.MinValue)
            {
                objDataWrapper.AddParameter("@REG_ID_ISSUE", objCustomerInfo.REG_ID_ISSUE);
            }
            else
            {
                objDataWrapper.AddParameter("@REG_ID_ISSUE", null);
            }
            objDataWrapper.AddParameter("@ORIGINAL_ISSUE", objCustomerInfo.ORIGINAL_ISSUE);
            objDataWrapper.AddParameter("@MAIN_ZIPCODE", objCustomerInfo.MAIN_ZIPCODE);
            objDataWrapper.AddParameter("@MAIN_CITY", objCustomerInfo.MAIN_CITY);
            objDataWrapper.AddParameter("@MAIN_COUNTRY", objCustomerInfo.MAIN_COUNTRY);
            objDataWrapper.AddParameter("@MAIN_STATE", objCustomerInfo.MAIN_STATE);
            objDataWrapper.AddParameter("@MAIN_FIRST_NAME",objCustomerInfo.MAIN_FIRST_NAME);
            objDataWrapper.AddParameter("@MAIN_MIDDLE_NAME",objCustomerInfo.MAIN_MIDDLE_NAME);
            objDataWrapper.AddParameter("@MAIN_LAST_NAME",objCustomerInfo.MAIN_LAST_NAME);
            objDataWrapper.AddParameter("@ID_TYPE", objCustomerInfo.ID_TYPE);
            objDataWrapper.AddParameter("@CADEMP", objCustomerInfo.CADEMP);
            objDataWrapper.AddParameter("@EMAIL_ADDRESS", objCustomerInfo.EMAIL_ADDRESS);
            objDataWrapper.AddParameter("@NATIONALITY", objCustomerInfo.NATIONALITY);
            objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION_TYPE", objCustomerInfo.REGIONAL_IDENTIFICATION_TYPE);
            objDataWrapper.AddParameter("@NET_ASSETS_AMOUNT", objCustomerInfo.NET_ASSETS_AMOUNT);
            objDataWrapper.AddParameter("@MONTHLY_INCOME", objCustomerInfo.MONTHLY_INCOME);
            objDataWrapper.AddParameter("@AMOUNT_TYPE", objCustomerInfo.AMOUNT_TYPE);
            objDataWrapper.AddParameter("@IS_POLITICALLY_EXPOSED", objCustomerInfo.IS_POLITICALLY_EXPOSED);
            objDataWrapper.AddParameter("@ACCOUNT_TYPE", objCustomerInfo.ACCOUNT_TYPE);
            objDataWrapper.AddParameter("@BANK_BRANCH", objCustomerInfo.BANK_BRANCH);
            objDataWrapper.AddParameter("@BANK_NUMBER", objCustomerInfo.BANK_NUMBER);
            objDataWrapper.AddParameter("@BANK_NAME", objCustomerInfo.BANK_NAME);
            objDataWrapper.AddParameter("@ACCOUNT_NUMBER", objCustomerInfo.ACCOUNT_NUMBER);

           
            
            //---------------End


            objDataWrapper.AddParameter("@CUSTOMER_PAGER_NO", objCustomerInfo.CustomerPagerNo);
            SqlDateTime sqldatenull = SqlDateTime.Null;
            objDataWrapper.AddParameter("@LAST_INSURANCE_SCORE_FETCHED", sqldatenull);
            //objDataWrapper.AddParameter("@Cust_Id",null,SqlDbType.Int,ParameterDirection.Output );

            // Added by Mohit on 4/11/2005
            objDataWrapper.AddParameter("@DESC_APPLICANT_OCCU", objCustomerInfo.DESC_APPLICANT_OCCU);
            // End
            //Added by Sumit On 10/04/2006
            objDataWrapper.AddParameter("@EMPLOYER_ADD1", objCustomerInfo.EMPLOYER_ADD1);
            objDataWrapper.AddParameter("@EMPLOYER_ADD2", objCustomerInfo.EMPLOYER_ADD2);
            objDataWrapper.AddParameter("@EMPLOYER_CITY", objCustomerInfo.EMPLOYER_CITY);
            objDataWrapper.AddParameter("@EMPLOYER_COUNTRY", objCustomerInfo.EMPLOYER_COUNTRY);
            objDataWrapper.AddParameter("@EMPLOYER_STATE", objCustomerInfo.EMPLOYER_STATE);
            objDataWrapper.AddParameter("@EMPLOYER_ZIPCODE", objCustomerInfo.EMPLOYER_ZIPCODE);
            objDataWrapper.AddParameter("@EMPLOYER_HOMEPHONE", objCustomerInfo.EMPLOYER_HOMEPHONE);
            objDataWrapper.AddParameter("@EMPLOYER_EMAIL", objCustomerInfo.EMPLOYER_EMAIL);
            if (objCustomerInfo.YEARS_WITH_CURR_OCCU > 0)
                objDataWrapper.AddParameter("@YEARS_WITH_CURR_OCCU", objCustomerInfo.YEARS_WITH_CURR_OCCU);
            else
                objDataWrapper.AddParameter("@YEARS_WITH_CURR_OCCU", System.DBNull.Value);
            objDataWrapper.AddParameter("@CUSTOMER_BUSINESS_TYPE", objCustomerInfo.CustomerBusinessType);
            objDataWrapper.AddParameter("@CUSTOMER_BUSINESS_DESC", objCustomerInfo.CustomerBusinessDesc);
            objDataWrapper.AddParameter("@CUSTOMER_WEBSITE", objCustomerInfo.CustomerWebsite);
            //objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_SCORE",DefaultValues.GetDecimalNullFromNegative(objCustomerInfo.CustomerInsuranceScore));
            objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_SCORE", objCustomerInfo.CustomerInsuranceScore);

            if (objCustomerInfo.CustomerInsuranceReceivedDate != DateTime.MinValue)
            {
                objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_RECEIVED_DATE", objCustomerInfo.CustomerInsuranceReceivedDate);
            }
            else
            {
                objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_RECEIVED_DATE", null);
            }
            objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE", objCustomerInfo.CustomerReasonCode);
            if (objCustomerInfo.CustomerReasonCode2 != "")
            {
                objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE2", objCustomerInfo.CustomerReasonCode2);
            }
            else
            {
                objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE2", null);
                //objCustomerInfo.CustomerReasonCode2 = 0;
            }
            if (objCustomerInfo.CustomerReasonCode3 != "")
            {
                objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE3", objCustomerInfo.CustomerReasonCode3);
            }
            else
            {
                objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE3", null);
                //objCustomerInfo.CustomerReasonCode3 = 0;
            }
            if (objCustomerInfo.CustomerReasonCode4 != "")
            {
                objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE4", objCustomerInfo.CustomerReasonCode4);
            }
            else
            {
                objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE4", null);
                //objCustomerInfo.CustomerReasonCode4 = 0;
            }

          
            objDataWrapper.AddParameter("@CUSTOMER_SUFFIX", objCustomerInfo.CustomerSuffix);
            objDataWrapper.AddParameter("@APPLICANT_OCCU", objCustomerInfo.APPLICANT_OCCU);
            objDataWrapper.AddParameter("@EMPLOYER_NAME", objCustomerInfo.EMPLOYER_NAME);
            objDataWrapper.AddParameter("@EMPLOYER_ADDRESS", objCustomerInfo.EMPLOYER_ADDRESS);
            objDataWrapper.AddParameter("@MARITAL_STATUS", objCustomerInfo.MARITAL_STATUS);
            objDataWrapper.AddParameter("@GENDER", objCustomerInfo.GENDER);
            objDataWrapper.AddParameter("@SSN_NO", objCustomerInfo.SSN_NO);

            if (objCustomerInfo.YEARS_WITH_CURR_EMPL > 0)
            {
                objDataWrapper.AddParameter("@YEARS_WITH_CURR_EMPL", objCustomerInfo.YEARS_WITH_CURR_EMPL);
            }

            if (objCustomerInfo.DATE_OF_BIRTH.Ticks != 0)
            {
                objDataWrapper.AddParameter("@DATE_OF_BIRTH", objCustomerInfo.DATE_OF_BIRTH);
            }

            /*objDataWrapper.AddParameter("@ALT_CUSTOMER_ADDRESS1",objCustomerInfo.Alt_CustomerAddress1 );
            objDataWrapper.AddParameter("@ALT_CUSTOMER_ADDRESS2",objCustomerInfo.Alt_CustomerAddress2 );
            objDataWrapper.AddParameter("@ALT_CUSTOMER_CITY",objCustomerInfo.Alt_CustomerCity);
            objDataWrapper.AddParameter("@ALT_CUSTOMER_COUNTRY",objCustomerInfo.Alt_CustomerCountry );
            objDataWrapper.AddParameter("@ALT_CUSTOMER_STATE",objCustomerInfo.Alt_CustomerState);
            objDataWrapper.AddParameter("@ALT_CUSTOMER_ZIP",objCustomerInfo.Alt_CustomerZip);*/

            SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@Cust_Id", null, SqlDbType.Int, ParameterDirection.Output);

            //objDataWrapper.ExecuteNonQuery(strStoredProc);
            //

            int returnResult = 0;

            try
            {
                //if transaction required
                if (TransactionRequired)
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objCustomerInfo);
                    if (objCustomerInfo.CustomerType.Trim() == "11110")
                    {
                        if (strTranXML != "" && strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                        {
                            XmlDocument XMLDoc = new XmlDocument();
                            XMLDoc.LoadXml(strTranXML);
                            XmlNodeList xnList = XMLDoc.SelectNodes("/LabelFieldMapping/Map[@field='CUSTOMER_FIRST_NAME']");

                            foreach (XmlElement node in xnList)
                            {
                                node.SetAttribute("label", Name);
                            }
                            strTranXML = XMLDoc.InnerXml;
                        }
                    }


                    if (returnResult > 0 && objSqlParameter.Value != System.DBNull.Value && ((strTranXML != "<LabelFieldMapping></LabelFieldMapping>") || (strTranXML != "")))
                    {
                        iCustomerID = int.Parse(objSqlParameter.Value.ToString());

                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                        objTransactionInfo.CLIENT_ID = iCustomerID;
                        objTransactionInfo.TRANS_TYPE_ID = 169;
                        objTransactionInfo.RECORDED_BY = objCustomerInfo.CREATED_BY;
                        if (int.Parse(objCustomerInfo.CustomerType) == 11110)
                        {
                            objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1457","");  //"New Customer/Applicant is added";
                        }
                        else
                        {
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='DATE_OF_BIRTH']");
                            objTransactionInfo.TRANS_DESC =Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1446", ""); //"New customer is added";
                        }
                        objTransactionInfo.CHANGE_XML = strTranXML;
                        objTransactionInfo.CUSTOM_INFO = ";"+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1790","") +"= " + objCustomerInfo.CustomerFirstName + " " + objCustomerInfo.CustomerMiddleName + " " + objCustomerInfo.CustomerLastName + ";"+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1792","") + objCustomerInfo.CustomerCode;//";Customer Name = " + objCustomerInfo.CustomerFirstName + " " + objCustomerInfo.CustomerMiddleName + " " + objCustomerInfo.CustomerLastName + ";Customer Code = " + objCustomerInfo.CustomerCode;

                        objDataWrapper.ExecuteNonQuery(objTransactionInfo);
                    }
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                iCustomerID = returnResult;

                if (returnResult != -1)
                {

                    //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                    iCustomerID = int.Parse(objSqlParameter.Value.ToString());



                    int CUSTOMER_ID = int.Parse(objDataWrapper.CommandParameters[0].Value.ToString());
                    objDataWrapper.ClearParameteres();

                    //Insert into Diary
                    //commented by anshuman
                    /*
                    Cms.Model.Diary.TodolistInfo objToDoListInfo = new Cms.Model.Diary.TodolistInfo();
				
                    objToDoListInfo.STARTTIME = System.DateTime.Now;
                    objToDoListInfo.ENDTIME = System.DateTime.Now;
                    objToDoListInfo.RECDATE = System.DateTime.Now;
                    objToDoListInfo.CREATED_DATETIME = DateTime.Now;
                    objToDoListInfo.FOLLOWUPDATE = DateTime.Parse(DateTime.Now.AddDays(5).ToString());
                    objToDoListInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    objToDoListInfo.SUBJECTLINE = "Customer added.";
                    objToDoListInfo.PRIORITY = "M";
                    objToDoListInfo.FROMUSERID=objCustomerInfo.CREATED_BY;
                    objToDoListInfo.CUSTOMER_ID=objCustomerInfo.CustomerId;
                    objToDoListInfo.TOUSERID=objCustomerInfo.CREATED_BY;

                    ClsDiary objDiary = new ClsDiary();
				
                    objDiary.TransactionLogRequired = true;

                    objDiary.Add(objToDoListInfo,objDataWrapper);
                    */
                    //Commit the transaction if all opeations are successfull.
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);
                }
                //				if (CUSTOMER_ID == -1)
                //				{
                //					return -1;
                //				}
                //				else
                //				{
                return returnResult;
                //				}
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }
		

		/// <summary>
		/// Calling Add method according to the class transaction required 
		/// </summary>
		/// <param name="objCustomerInfo"></param>
		/// <returns></returns>
		/*
		public int Add(Cms.Model.Client.ClsCustomerInfo objCustomerInfo)
		{
			return Add(objCustomerInfo,TransactionRequired);
		}
		*/
		
		/// <summary>
		/// Updates the form's modified value
		/// </summary>
		/// <param name="objOldCustomerInfo">model object having old information</param>
		/// <param name="objCustomerInfo">model object having new information(form control's value)</param>
		/// <returns>no. of rows updated (1 or 0)</returns>
		/*	public int Update(ClsCustomerInfo objOldCustomerInfo,ClsCustomerInfo objCustomerInfo)
			{
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			
				string strTranXML;
				int returnResult = 0;
			
				//Set transaction label in the new object, if required
				if ( this.boolTransactionRequired)
				{
					objCustomerInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"client\aspx\AddCustomer.aspx.resx");
				}

				objBuilder.WhereClause = " WHERE Customer_ID = " + objOldCustomerInfo.CustomerId.ToString();
				string strUpdate = objBuilder.GetUpdateSQL(objOldCustomerInfo,objCustomerInfo,out strTranXML);

				if(strUpdate != "")
				{
					DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.Text,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
					try
					{
						if(TransactionRequired) 
						{
							Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
							objTransactionInfo.TRANS_TYPE_ID	=	2;
							objTransactionInfo.RECORDED_BY		=	objCustomerInfo.MODIFIED_BY;
							objTransactionInfo.TRANS_DESC		=	"Customer Information Has Been Updated";
							objTransactionInfo.CHANGE_XML		=	strTranXML;

							objDataWrapper.ExecuteNonQuery(strUpdate,objTransactionInfo);
							returnResult = 1;
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
			}*/


        public int Update(Cms.Model.Client.ClsCustomerInfo objOldCustomerInfo, Cms.Model.Client.ClsCustomerInfo objCustomerInfo,string Name)
        {
            string strStoredProc = "Proc_UpdateCustomerInfo";


            DateTime RecordDate = DateTime.Now;

            //Set transaction label in the new object, if required
            if (this.boolTransactionRequired)
            {
                objCustomerInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"client\aspx\AddCustomer.aspx.resx");
            }

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            objDataWrapper.AddParameter("@CUSTOMER_ID", objCustomerInfo.CustomerId);
           
            objDataWrapper.AddParameter("@CUSTOMER_CODE", objCustomerInfo.CustomerCode);
            objDataWrapper.AddParameter("@CUSTOMER_TYPE", objCustomerInfo.CustomerType);

            if (objCustomerInfo.CustomerParent != 0)
            {
                objDataWrapper.AddParameter("@CUSTOMER_PARENT", objCustomerInfo.CustomerParent);
            }
            else
            {
                objDataWrapper.AddParameter("@CUSTOMER_PARENT", null);
            }

            objDataWrapper.AddParameter("@CUSTOMER_SUFFIX", objCustomerInfo.CustomerSuffix);
            objDataWrapper.AddParameter("@CUSTOMER_FIRST_NAME", objCustomerInfo.CustomerFirstName);


            if (objCustomerInfo.CustomerType == CUST_TYPE_PERSONAL)
            {
                objDataWrapper.AddParameter("@CUSTOMER_MIDDLE_NAME", objCustomerInfo.CustomerMiddleName);
                objDataWrapper.AddParameter("@CUSTOMER_LAST_NAME", objCustomerInfo.CustomerLastName);
            }
            else
            {
                objDataWrapper.AddParameter("@CUSTOMER_MIDDLE_NAME", null);
                objDataWrapper.AddParameter("@CUSTOMER_LAST_NAME", null);
            }

            objDataWrapper.AddParameter("@APPLICANT_OCCU", objCustomerInfo.APPLICANT_OCCU);
            objDataWrapper.AddParameter("@EMPLOYER_NAME", objCustomerInfo.EMPLOYER_NAME);
            objDataWrapper.AddParameter("@EMPLOYER_ADDRESS", objCustomerInfo.EMPLOYER_ADDRESS);
            objDataWrapper.AddParameter("@MARITAL_STATUS", objCustomerInfo.MARITAL_STATUS);
            objDataWrapper.AddParameter("@GENDER", objCustomerInfo.GENDER);
            objDataWrapper.AddParameter("@SSN_NO", objCustomerInfo.SSN_NO);

            if (objCustomerInfo.YEARS_WITH_CURR_EMPL > 0)
            {
                objDataWrapper.AddParameter("@YEARS_WITH_CURR_EMPL", objCustomerInfo.YEARS_WITH_CURR_EMPL);
            }

            if (objCustomerInfo.DATE_OF_BIRTH.Ticks != 0)
            {
                objDataWrapper.AddParameter("@DATE_OF_BIRTH", objCustomerInfo.DATE_OF_BIRTH);
            }
            objDataWrapper.AddParameter("@CUSTOMER_ADDRESS1", objCustomerInfo.CustomerAddress1);
            objDataWrapper.AddParameter("@CUSTOMER_ADDRESS2", objCustomerInfo.CustomerAddress2);
            objDataWrapper.AddParameter("@CUSTOMER_CITY", objCustomerInfo.CustomerCity);
            objDataWrapper.AddParameter("@CUSTOMER_COUNTRY", objCustomerInfo.CustomerCountry);
            objDataWrapper.AddParameter("@CUSTOMER_STATE", objCustomerInfo.CustomerState);
            objDataWrapper.AddParameter("@CUSTOMER_ZIP", objCustomerInfo.CustomerZip);
            objDataWrapper.AddParameter("@CUSTOMER_BUSINESS_TYPE", objCustomerInfo.CustomerBusinessType);
            objDataWrapper.AddParameter("@CUSTOMER_BUSINESS_DESC", objCustomerInfo.CustomerBusinessDesc);
            objDataWrapper.AddParameter("@CUSTOMER_CONTACT_NAME", objCustomerInfo.CustomerContactName);
            objDataWrapper.AddParameter("@CUSTOMER_BUSINESS_PHONE", objCustomerInfo.CustomerBusinessPhone);
            objDataWrapper.AddParameter("@CUSTOMER_EXT", objCustomerInfo.CustomerExt);
            objDataWrapper.AddParameter("@EMP_EXT", objCustomerInfo.EMP_EXT);
            objDataWrapper.AddParameter("@CUSTOMER_HOME_PHONE", objCustomerInfo.CustomerHomePhone);
            objDataWrapper.AddParameter("@CUSTOMER_MOBILE", objCustomerInfo.CustomerMobile);
            objDataWrapper.AddParameter("@CUSTOMER_FAX", objCustomerInfo.CustomerFax);
            objDataWrapper.AddParameter("@CUSTOMER_PAGER_NO", objCustomerInfo.CustomerPagerNo);
            objDataWrapper.AddParameter("@CUSTOMER_Email", objCustomerInfo.CustomerEmail);
            objDataWrapper.AddParameter("@CUSTOMER_WEBSITE", objCustomerInfo.CustomerWebsite);


            //objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_SCORE",DefaultValues.GetDecimalNullFromNegative(objCustomerInfo.CustomerInsuranceScore));	
            objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_SCORE", objCustomerInfo.CustomerInsuranceScore);

            if (objCustomerInfo.CustomerInsuranceReceivedDate.Ticks != 0)
            {
                objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_RECEIVED_DATE", objCustomerInfo.CustomerInsuranceReceivedDate);
            }
            else
            {
                objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_RECEIVED_DATE", null);
            }

            if (objCustomerInfo.CustomerAgencyId != 0)
                objDataWrapper.AddParameter("@CUSTOMER_AGENCY_ID", objCustomerInfo.CustomerAgencyId);
            else
                objDataWrapper.AddParameter("@CUSTOMER_AGENCY_ID", null);
            objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE", objCustomerInfo.CustomerReasonCode);
            if (objCustomerInfo.CustomerReasonCode2 != "")
            {
                objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE2", objCustomerInfo.CustomerReasonCode2);
            }
            else
            {
                objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE2", null);
                //objCustomerInfo.CustomerReasonCode2 = 0;
            }
            if (objCustomerInfo.CustomerReasonCode3 != "")
            {
                objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE3", objCustomerInfo.CustomerReasonCode3);
            }
            else
            {
                objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE3", null);
                //objCustomerInfo.CustomerReasonCode3 = 0;
            }
            if (objCustomerInfo.CustomerReasonCode4 != "")
            {
                objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE4", objCustomerInfo.CustomerReasonCode4);
            }
            else
            {
                objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE4", null);
                //objCustomerInfo.CustomerReasonCode4 = 0;
            }

            //Account Info Fields
            //objDataWrapper.AddParameter("@CustomerProducerId",objCustomerInfo.CustomerProducerId);
            //objDataWrapper.AddParameter("@CustomerLateCharges",objCustomerInfo.CustomerLateCharges);
            //objDataWrapper.AddParameter("@CustomerLateNotices",objCustomerInfo.CustomerLateNotices);
            //objDataWrapper.AddParameter("@CustomerAccountExecutiveId",objCustomerInfo.CustomerAccountExecutiveId);
            //objDataWrapper.AddParameter("@CustomerSendStatement",objCustomerInfo.CustomerSendStatement);
            //objDataWrapper.AddParameter("@CustomerReceivableDueDays",objCustomerInfo.CustomerReceivableDueDays);
            //objDataWrapper.AddParameter("@CUSTOMER_AGENCY_ID",objCustomerInfo.CustomerAgencyId);
            //objDataWrapper.AddParameter("@CustomerCsr",objCustomerInfo.CustomerCsr);
            //objDataWrapper.AddParameter("@CustomerReferredBy",objCustomerInfo.CustomerReferredBy);

            // End Account Info Fields
            objDataWrapper.AddParameter("@PREFIX", objCustomerInfo.PREFIX);
            objDataWrapper.AddParameter("@PER_CUST_MOBILE", objCustomerInfo.PER_CUST_MOBILE);
            objDataWrapper.AddParameter("@MODIFIED_BY", objCustomerInfo.MODIFIED_BY);
            objDataWrapper.AddParameter("@MODIFIED_DATETIME", objCustomerInfo.LAST_UPDATED_DATETIME);
            //objDataWrapper.AddParameter("@Cust_Id",null,SqlDbType.Int,ParameterDirection.Output );

            //SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@Cust_Id",null,SqlDbType.Int,ParameterDirection.Output);

            //objDataWrapper.ExecuteNonQuery(strStoredProc);
            //

            // Added by Mohit on 4/11/2005
            objDataWrapper.AddParameter("@DESC_APPLICANT_OCCU", objCustomerInfo.DESC_APPLICANT_OCCU);
            // End
            //Added by Sumit On 10/04/2006
            objDataWrapper.AddParameter("@EMPLOYER_ADD1", objCustomerInfo.EMPLOYER_ADD1);
            objDataWrapper.AddParameter("@EMPLOYER_ADD2", objCustomerInfo.EMPLOYER_ADD2);
            objDataWrapper.AddParameter("@EMPLOYER_CITY", objCustomerInfo.EMPLOYER_CITY);
            objDataWrapper.AddParameter("@EMPLOYER_COUNTRY", objCustomerInfo.EMPLOYER_COUNTRY);
            objDataWrapper.AddParameter("@EMPLOYER_STATE", objCustomerInfo.EMPLOYER_STATE);
            objDataWrapper.AddParameter("@EMPLOYER_ZIPCODE", objCustomerInfo.EMPLOYER_ZIPCODE);
            objDataWrapper.AddParameter("@EMPLOYER_HOMEPHONE", objCustomerInfo.EMPLOYER_HOMEPHONE);
            objDataWrapper.AddParameter("@EMPLOYER_EMAIL", objCustomerInfo.EMPLOYER_EMAIL);
            if (objCustomerInfo.YEARS_WITH_CURR_OCCU > 0)
                objDataWrapper.AddParameter("@YEARS_WITH_CURR_OCCU", objCustomerInfo.YEARS_WITH_CURR_OCCU);
            else
                objDataWrapper.AddParameter("@YEARS_WITH_CURR_OCCU", System.DBNull.Value);
            /*objDataWrapper.AddParameter("@ALT_CUSTOMER_ADDRESS1",objCustomerInfo.Alt_CustomerAddress1 );
            objDataWrapper.AddParameter("@ALT_CUSTOMER_ADDRESS2",objCustomerInfo.Alt_CustomerAddress2 );
            objDataWrapper.AddParameter("@ALT_CUSTOMER_CITY",objCustomerInfo.Alt_CustomerCity);
            objDataWrapper.AddParameter("@ALT_CUSTOMER_COUNTRY",objCustomerInfo.Alt_CustomerCountry );
            objDataWrapper.AddParameter("@ALT_CUSTOMER_STATE",objCustomerInfo.Alt_CustomerState);
            objDataWrapper.AddParameter("@ALT_CUSTOMER_ZIP",objCustomerInfo.Alt_CustomerZip);*/

            //ADDED BY LALIT

            objDataWrapper.AddParameter("@CPF_CNPJ", objCustomerInfo.CPF_CNPJ);
            objDataWrapper.AddParameter("@NUMBER", objCustomerInfo.NUMBER);

            objDataWrapper.AddParameter("@DISTRICT", objCustomerInfo.DISTRICT);

            objDataWrapper.AddParameter("@MAIN_TITLE", objCustomerInfo.MAIN_TITLE);
            objDataWrapper.AddParameter("@MAIN_POSITION", objCustomerInfo.MAIN_POSITION);
            objDataWrapper.AddParameter("@MAIN_CPF_CNPJ", objCustomerInfo.MAIN_CPF_CNPJ);
            objDataWrapper.AddParameter("@MAIN_ADDRESS", objCustomerInfo.MAIN_ADDRESS);
            objDataWrapper.AddParameter("@MAIN_NUMBER", objCustomerInfo.MAIN_NUMBER);
            objDataWrapper.AddParameter("@MAIN_COMPLIMENT", objCustomerInfo.MAIN_COMPLIMENT);
            objDataWrapper.AddParameter("@MAIN_DISTRICT", objCustomerInfo.MAIN_DISTRICT);
            objDataWrapper.AddParameter("@MAIN_NOTE", objCustomerInfo.MAIN_NOTE);
            objDataWrapper.AddParameter("@MAIN_CONTACT_CODE", objCustomerInfo.MAIN_CONTACT_CODE);
            objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", objCustomerInfo.REGIONAL_IDENTIFICATION);
          //  objDataWrapper.AddParameter("@REG_ID_ISSUE", objCustomerInfo.REG_ID_ISSUE);
            if (objCustomerInfo.REG_ID_ISSUE != DateTime.MinValue)
            {
                objDataWrapper.AddParameter("@REG_ID_ISSUE", objCustomerInfo.REG_ID_ISSUE);
            }
            else
            {
                objDataWrapper.AddParameter("@REG_ID_ISSUE", null);
            }
            objDataWrapper.AddParameter("@ORIGINAL_ISSUE", objCustomerInfo.ORIGINAL_ISSUE);
            objDataWrapper.AddParameter("@MAIN_ZIPCODE", objCustomerInfo.MAIN_ZIPCODE);
            objDataWrapper.AddParameter("@MAIN_CITY", objCustomerInfo.MAIN_CITY);
            objDataWrapper.AddParameter("@MAIN_COUNTRY", objCustomerInfo.MAIN_COUNTRY);
            objDataWrapper.AddParameter("@MAIN_STATE", objCustomerInfo.MAIN_STATE);
            objDataWrapper.AddParameter("@MAIN_FIRST_NAME", objCustomerInfo.MAIN_FIRST_NAME);
            objDataWrapper.AddParameter("@MAIN_MIDDLE_NAME", objCustomerInfo.MAIN_MIDDLE_NAME);
            objDataWrapper.AddParameter("@MAIN_LAST_NAME", objCustomerInfo.MAIN_LAST_NAME);
            objDataWrapper.AddParameter("@ID_TYPE", objCustomerInfo.ID_TYPE);
            objDataWrapper.AddParameter("@CADEMP", objCustomerInfo.CADEMP);
            objDataWrapper.AddParameter("@EMAIL_ADDRESS", objCustomerInfo.EMAIL_ADDRESS);
            objDataWrapper.AddParameter("@NATIONALITY", objCustomerInfo.NATIONALITY);
            objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION_TYPE", objCustomerInfo.REGIONAL_IDENTIFICATION_TYPE);
            objDataWrapper.AddParameter("@NET_ASSETS_AMOUNT", objCustomerInfo.NET_ASSETS_AMOUNT);
            objDataWrapper.AddParameter("@MONTHLY_INCOME", objCustomerInfo.MONTHLY_INCOME);
            objDataWrapper.AddParameter("@AMOUNT_TYPE", objCustomerInfo.AMOUNT_TYPE);
            objDataWrapper.AddParameter("@IS_POLITICALLY_EXPOSED", objCustomerInfo.IS_POLITICALLY_EXPOSED);
            objDataWrapper.AddParameter("@ACCOUNT_TYPE", objCustomerInfo.ACCOUNT_TYPE);
            objDataWrapper.AddParameter("@BANK_BRANCH", objCustomerInfo.BANK_BRANCH);
            objDataWrapper.AddParameter("@BANK_NUMBER", objCustomerInfo.BANK_NUMBER);
            objDataWrapper.AddParameter("@BANK_NAME", objCustomerInfo.BANK_NAME);
            objDataWrapper.AddParameter("@ACCOUNT_NUMBER", objCustomerInfo.ACCOUNT_NUMBER);
            SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@New_Cust_Id", SqlDbType.Int, ParameterDirection.Output);
            int returnResult = 0;

            try
            {
                //if transaction required
                if (TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objOldCustomerInfo, objCustomerInfo);
                    if (objCustomerInfo.CustomerType.Trim() == "11110")
                    {
                        
                        if (strTranXML == "<LabelFieldMapping></LabelFieldMapping>" && strTranXML == "")
                        {
                            XmlDocument XMLDoc = new XmlDocument();
                            XMLDoc.LoadXml(strTranXML);
                            XmlNodeList xnList = XMLDoc.SelectNodes("/LabelFieldMapping/Map[@field='CUSTOMER_FIRST_NAME']");

                            foreach (XmlElement node in xnList)
                            {
                                node.SetAttribute("label", Name);
                            }
                            strTranXML = XMLDoc.InnerXml;
                        }
                    }

                    if (strTranXML == "<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    else
                    {
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                        objTransactionInfo.CLIENT_ID = objCustomerInfo.CustomerId;
                        objTransactionInfo.TRANS_TYPE_ID = 170;
                        objTransactionInfo.RECORDED_BY = objCustomerInfo.MODIFIED_BY;
                        if (int.Parse(objCustomerInfo.CustomerType) == 11110) //Personal
                        {
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='CUSTOMER_CONTACT_NAME']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='CUSTOMER_BUSINESS_PHONE']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='CUSTOMER_EXT']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='CUSTOMER_MOBILE']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='CUSTOMER_WEBSITE']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='CUSTOMER_FAX']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='CUSTOMER_PAGER_NO']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='CUSTOMER_BUSINESS_TYPE']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='CUSTOMER_BUSINESS_DESC']");
                            objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1458", ""); //"Customer/Applicant is modified";
                        }
                        else //Commercial
                        {
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='EMPLOYER_ADD1']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='EMPLOYER_ADD2']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='EMPLOYER_CITY']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='EMPLOYER_COUNTRY']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='EMPLOYER_EMAIL']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='EMPLOYER_HOMEPHONE']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='EMPLOYER_ZIPCODE']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='EMPLOYER_STATE']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='EMPLOYER_NAME']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='YEARS_WITH_CURR_OCCU']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='EMP_EXT']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='DESC_APPLICANT_OCCU']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='YEARS_WITH_CURR_EMPL']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='APPLICANT_OCCU']");

                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='MARITAL_STATUS']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='DATE_OF_BIRTH']");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='SSN_NO']");

                            objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1459", "");// "Customer is modified";
                        }
                        objTransactionInfo.CHANGE_XML = strTranXML;
                        objTransactionInfo.CUSTOM_INFO = ";"+ Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1790","")+" = " + objCustomerInfo.CustomerFirstName + " " + objCustomerInfo.CustomerMiddleName + " " + objCustomerInfo.CustomerLastName + ";"+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1792","") + objCustomerInfo.CustomerCode;//";Customer Name = " + objCustomerInfo.CustomerFirstName + " " + objCustomerInfo.CustomerMiddleName + " " + objCustomerInfo.CustomerLastName + ";Customer Code = " + objCustomerInfo.CustomerCode;

                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                    }
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                //int iCustomerID		=	int.Parse(objSqlParameter.Value.ToString());



                int CUSTOMER_ID = int.Parse(objDataWrapper.CommandParameters[0].Value.ToString());
                objDataWrapper.ClearParameteres();

                //Insert into Diary
                /*
                            Cms.Model.Diary.TodolistInfo objToDoListInfo = new Cms.Model.Diary.TodolistInfo();
				
                            objToDoListInfo.STARTTIME = System.DateTime.Now;
                            objToDoListInfo.ENDTIME = System.DateTime.Now;
                            objToDoListInfo.RECDATE = System.DateTime.Now;
                            objToDoListInfo.CREATED_DATETIME = DateTime.Now;
                            objToDoListInfo.FOLLOWUPDATE = DateTime.Now;
                            objToDoListInfo.LAST_UPDATED_DATETIME = DateTime.Now;
				
                            objToDoListInfo.SUBJECTLINE = "Customer added.";
                            objToDoListInfo.PRIORITY = "M";

                            ClsDiary objDiary = new ClsDiary();
				
                            objDiary.TransactionLogRequired = false;

                            objDiary.Add(objToDoListInfo,objDataWrapper);
                            */
                //Commit the transaction if all opeations are successfull.
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (CUSTOMER_ID == -1)
                {
                    return -1;
                }
                else
                {
                    returnResult = int.Parse(objSqlParameter.Value.ToString());

                 
                    return returnResult;
                }
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }
		
		/*Remove this later
		public int Update(ClsCustomerInfo objOldCustomerInfo,ClsCustomerInfo objCustomerInfo)
		{
			return Update(objOldCustomerInfo,objCustomerInfo,TransactionRequired);
		}
		*/

//		public void ActivateDeactivate(string strCode,string isActive)
//		{
////			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.Text,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
////			try
////			{
////				if(isActive == "Y") 
////				{  
////					//objDataWrapper.ExecuteNonQuery(strUpdate,1,"Activated",ModifiedBy,);
////					returnResult = 1;
////				}
////				else
////				{
////					returnResult = objDataWrapper.ExecuteNonQuery(strUpdate);
////				}
////					
////				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
////					
////				return returnResult;
////			}
////			catch(Exception ex)
////			{
////				throw(ex);
////			}
////			finally
////			{
////				if(objDataWrapper != null) 
////				{
////					objDataWrapper.Dispose();
////				}
////				if(objBuilder != null) 
////				{
////					objBuilder = null;
////				}
////			}
//		}
		#endregion

		
		
		#region ATTENTION NOTE
		public int OldUpdateAttentionNotes(ClsCustomerInfo objOldCustomerInfo,ClsCustomerInfo objCustomerInfo)
		{
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			
			// string strTranXML;
			int returnResult = 0;
			
			//Set transaction label in the new object, if required
			//if ( this.boolTransactionRequired)
			//{
			//	objCustomerInfo.TransactLabel = ClsCommon.MapTransactionLabel("AddCustomer.aspx.resx");
			//}

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);

			objDataWrapper.AddParameter("@CustomerId",objCustomerInfo.CustomerId,SqlDbType.Int);
			objDataWrapper.AddParameter("@AttentionNote",objCustomerInfo.Customer_Attention_Note);
			objDataWrapper.AddParameter("@AttentionNoteUpdated",objCustomerInfo.ATTENTION_NOTE_UPDATED);

			//objDataWrapper.AddParameter("@RETURN_VALUE",null,SqlDbType.SmallInt,ParameterDirection.Output);
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.Text,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.ExecuteNonQuery(strStoredProcAN);
				returnResult	=	1;
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
		public int UpdateAttentionNotes(ClsCustomerInfo objOldCustomerInfo,ClsCustomerInfo objCustomerInfo)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			       
			try
			{
				objDataWrapper.AddParameter("@CustomerId",objCustomerInfo.CustomerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@AttentionNote",objCustomerInfo.Customer_Attention_Note);
				objDataWrapper.AddParameter("@AttentionNoteUpdated",objCustomerInfo.ATTENTION_NOTE_UPDATED);			

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					objCustomerInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"client\aspx\AttentionNotes.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objOldCustomerInfo,objCustomerInfo);
					if(strTranXML=="<LabelFieldMapping></LabelFieldMapping>" || strTranXML=="")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProcAN);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						
						objTransactionInfo.CLIENT_ID		=	objCustomerInfo.CustomerId;
                        if (objOldCustomerInfo.Customer_Attention_Note == "")
                        {
                            objTransactionInfo.TRANS_TYPE_ID = 171;
                            objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1460", "");// "Customer Attention Notes is added";
                        }
                        else
                        {
                            objTransactionInfo.TRANS_TYPE_ID = 206;
                            objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1461", ""); //"Customer Attention Notes is modified";		
                        }
						objTransactionInfo.RECORDED_BY		=	objCustomerInfo.CREATED_BY;
											
						objTransactionInfo.CHANGE_XML		=	strTranXML;
                        objTransactionInfo.CUSTOM_INFO = ";"+ Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1790","")+" = " + objOldCustomerInfo.CustomerFirstName + " " + objOldCustomerInfo.CustomerMiddleName + " " + objOldCustomerInfo.CustomerLastName + ";"+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1792","") + objOldCustomerInfo.CustomerCode; //";Customer Name = " + objOldCustomerInfo.CustomerFirstName + " " + objOldCustomerInfo.CustomerMiddleName + " " + objOldCustomerInfo.CustomerLastName + ";Customer Code = " + objOldCustomerInfo.CustomerCode;
						
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProcAN,objTransactionInfo);
					}
				}
				else//if no transaction required
				{
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProcAN);
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
		/// <summary>
		/// Function for viewing the details of Attention Notes
		/// </summary>
		/// <returns></returns>
		public DataSet ViewAttentionNotes(string strCustomerID)
		{
			DataSet dsAccounts = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CustomerID",strCustomerID,SqlDbType.Int);

			dsAccounts = objDataWrapper.ExecuteDataSet("Proc_GetAttentionDetails");
			
			return dsAccounts;
		}
		
		#endregion

		#region Customer E-Mail
		public DataTable FillUserEmail(int UserId)
		{
			string		strStoredProc	=	"Proc_GetNameEmail";//Will be replaced with CONST
						
			DataSet dsUserEmail= new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
						
			objDataWrapper.AddParameter("@UserId",UserId,SqlDbType.NVarChar);
			dsUserEmail = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			return dsUserEmail.Tables[0];
		}
		public DataTable FetchCustomerEMailAddress(int CustomerID)
		{
			
			DataSet dsCustomer	= new DataSet();
					

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@Customer_ID",CustomerID,SqlDbType.Int);

			dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetCustomerEmail");
			
			return dsCustomer.Tables[0];
		}
		public DataTable FetchApplicantEMailAddress(int CustomerID,bool CSR)
		{
			
			DataSet dsCustomer	= new DataSet();
					

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@Customer_ID",CustomerID,SqlDbType.Int);

			dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetApplicantEmail");
			if (CSR == true)
				return dsCustomer.Tables[1];
			else
				return dsCustomer.Tables[0];
		}
		//Email for Doc Merger
		public DataTable FetchApplicantEMailAddressDocMerge(int CustomerID,int PolicyID,int PolicyVerID)
		{
			
			DataSet dsCustomer	= new DataSet();
					

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICY_ID",PolicyID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVerID,SqlDbType.Int);

			dsCustomer = objDataWrapper.ExecuteDataSet("PROC_GETAPPLICANTEMAIL_DOC_MERGE");
//			if (CSR == true)
//				return dsCustomer.Tables[1];
//			else
				return dsCustomer.Tables[0];
		}
		
		
		#endregion

		#region "Fill Drop down Functions"
		public static void GetCustomerNamesInDropDown(DropDownList objDropDownList, string selectedValue)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillCustomerDropDown").Tables[0];
			//	objDropDownList.DataSource = objDataTable;
			//	objDropDownList.DataTextField = "CUSTOMER_FIRST_NAME";
			//objDropDownList.DataValueField = "CUSTOMER_ID";
			objDropDownList.Items.Clear();
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				objDropDownList.Items.Add(new ListItem(objDataTable.DefaultView[i]["CUSTOMER_FIRST_NAME"].ToString(),objDataTable.DefaultView[i]["CUSTOMER_ID"].ToString()));
				if(selectedValue!=null && selectedValue.Length>0 && objDataTable.DefaultView[i]["CUSTOMER_ID"].ToString().Equals(selectedValue))
					objDropDownList.SelectedIndex = i;
			}
			//objDropDownList.DataBind();
		}
        //Get Customer data to bind Coolite combox control - itrack - 1557
        public static void GetCustomerNamesInDropDown(ref DataTable dt)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            dt = objDataWrapper.ExecuteDataSet("Proc_FillCustomerDropDown").Tables[0];
            
        }


        public static DataTable GetCustomer() // Added by aditya, for itrack 1179
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            DataTable objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillCustomerDropDown").Tables[0];            
            return objDataTable;
        }

		public static void GetCustomerNamesInDropDown(DropDownList objDropDownList)
		{
			GetCustomerNamesInDropDown(objDropDownList,null);
		}
		public static string GetCustomerName(string CUSTOMER_ID)
		{
            return DataWrapper.ExecuteScalar(ConnStr, CommandType.Text, "select CUSTOMER_FIRST_NAME +' ' + CUSTOMER_LAST_NAME as INDIVIDUAL_CONTACT_NAME from CLT_CUSTOMER_LIST where CUSTOMER_ID=" + CUSTOMER_ID).ToString();
		}
		#endregion
		


		/// <summary>
		/// Function for creating Customer xml 
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <returns></returns>
		public string FillCustomerDetails(int CustomerID)
		{
			
			DataSet dsCustomer = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CustomerID",CustomerID,SqlDbType.Int);

			
			dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetCustomerDetails");
			
			string strXML = dsCustomer.GetXml();
			return strXML;
		}

		//Done for Itrack Issue 6565 on 6 Nov 09
		public string FillCustomer_Vehicle_Details(int CustomerID,int App_Pol_Id,int App_Pol_Version_Id,string calledFrom)
		{
			
			DataSet dsCustomer = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_POLICY_ID",App_Pol_Id,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_POLICY_VERSION_ID",App_Pol_Version_Id,SqlDbType.Int);
			objDataWrapper.AddParameter("@CALLED_FROM",calledFrom,SqlDbType.VarChar);
			dsCustomer = objDataWrapper.ExecuteDataSet("Proc_CUSTOMER_VEHICLE_DETAILS");
			
			string strXML = dsCustomer.GetXml();
			return strXML;
		}
		
		//RAvindra(08-26-2009)
        public string FillCustomerDetailsForClientTop(int CustomerID)
        {

            DataSet dsCustomer = new DataSet();

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CustomerID", CustomerID, SqlDbType.Int);


            dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetCustomerDetailsForClientTop");

            string strXML = dsCustomer.GetXml();
            return strXML;
        }
      
        public string FillCustomerDetailsForClientTop(int CustomerID, int LANG_ID)
		{
			
			DataSet dsCustomer = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CustomerID",CustomerID,SqlDbType.Int);
            objDataWrapper.AddParameter("@LANG_ID", LANG_ID, SqlDbType.Int);

			
			dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetCustomerDetailsForClientTop");
			
			string strXML = dsCustomer.GetXml();
			return strXML;
		}

			public DataSet FillAgencyCustomerDetails(int AgencyCode)
		{
			
			DataSet dsCustomer = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@AGENCY_ID",AgencyCode,SqlDbType.Int);

			dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetAgencyCustomerDetails");
			
			return dsCustomer;
		}
		

		#region UTILITY FUNCTIONS
		public static  string GetCustomerNameByID(int CustomerID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				
				DataSet dsCustomer = new DataSet();
			
				
				objDataWrapper.AddParameter("@CustomerID",CustomerID,SqlDbType.Int);

				dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetCustomerDetails");
				string RetVal="";
				if (dsCustomer!=null && dsCustomer.Tables[0].Rows.Count>0)
				{
					RetVal = (dsCustomer.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"]==null?"":dsCustomer.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"]) +" "+(dsCustomer.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"]==null?"":dsCustomer.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"])+" "+(dsCustomer.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"]==null?"":dsCustomer.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"]);
				}
				return RetVal;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 
			}

		}
		public static  string GetCustomerNameAndStateByID(int CustomerID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				
				DataSet dsCustomer = new DataSet();
			
				
				objDataWrapper.AddParameter("@CustomerID",CustomerID,SqlDbType.Int);

				dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetCustomerDetails");
				string RetVal="";
				if (dsCustomer!=null && dsCustomer.Tables[0].Rows.Count>0)
				{
					string name ="";
					name = (dsCustomer.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"]==null?"":dsCustomer.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"]) +
							" "+
						   (dsCustomer.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"]==null?"":dsCustomer.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"])+
							" "+
							(dsCustomer.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"]==null?"":dsCustomer.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"]);

					RetVal = name + "^" + (dsCustomer.Tables[0].Rows[0]["CUSTOMER_STATE"]==null?"":dsCustomer.Tables[0].Rows[0]["CUSTOMER_STATE"].ToString()) ;//(dsCustomer.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"]==null?"":dsCustomer.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"]) +" "+(dsCustomer.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"]==null?"":dsCustomer.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"])+" "+(dsCustomer.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"]==null?"":dsCustomer.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"])+"^"+(dsCustomer.Tables[0].Rows[0]["CUSTOMER_STATE"]==null?"":dsCustomer.Tables[0].Rows[0]["CUSTOMER_STATE"]);
				}
				return RetVal;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 
			}

		}
		
		public static  DataSet GetCustomerInfo(int CustomerID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				
				DataSet dsCustomer = new DataSet();
			
				
				objDataWrapper.AddParameter("@CustomerID",CustomerID,SqlDbType.Int);
				dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetCustomerInfo"); 				
				return dsCustomer;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 
			}

		}

		public static  DataSet GetCustomerDetails(int CustomerID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				
				DataSet dsCustomer = new DataSet();
			
				
				objDataWrapper.AddParameter("@CustomerID",CustomerID,SqlDbType.Int);
				dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetCustomerDetails"); 				
				return dsCustomer;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 
			}

		}
		/// <summary>
		/// Added on 28 August 2009 : Praveen Kasana
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <returns></returns>
		public static  DataSet GetCustomerSSN(int CustomerID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{				
				DataSet dsCustomer = new DataSet();
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetSSNCustomer"); 				
				return dsCustomer;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 
			}

		}
		#endregion

		/// <summary>
		/// Fetch the selected client address and shows it on address fields
		/// </summary>
		/// 
	
		public static void PopulateClientAddress(int CustomerId, TextBox txtAdd1
			, TextBox txtAdd2, TextBox txtCity
			, DropDownList cmbState, DropDownList cmbCountry
			, TextBox txtZip,TextBox txtMobile, TextBox txtBUSINESS_PHONE, TextBox txtEXT )
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				DataSet ds = new DataSet();
			
				
				objDataWrapper.AddParameter("@CustomerID", CustomerId, SqlDbType.Int);

				ds = objDataWrapper.ExecuteDataSet("Proc_GetCustomerDetails");
			
				if (ds.Tables.Count == 0)
					return;

				if (ds.Tables[0].Rows.Count > 0)
				{
					System.Data.DataRow dr = ds.Tables[0].Rows[0];

					if (txtAdd1.Text.Trim() == "")
						txtAdd1.Text = dr["CUSTOMER_ADDRESS1"].ToString();

					if (txtAdd2.Text.Trim() == "")
						txtAdd2.Text = dr["CUSTOMER_ADDRESS2"].ToString();

					if (txtCity.Text.Trim() == "")
						txtCity.Text = dr["CUSTOMER_CITY"].ToString();

					if (cmbState.SelectedIndex == -1)
						cmbState.SelectedValue = dr["CUSTOMER_STATE"].ToString();

					if (cmbCountry.SelectedIndex == -1)
						cmbCountry.SelectedValue = dr["CUSTOMER_COUNTRY"].ToString();

					if (txtZip.Text.Trim() == "")
						txtZip.Text	= dr["CUSTOMER_ZIP"].ToString();
					if (dr["CUSTOMER_TYPE"].ToString() != "11110")
					{
						if (txtMobile.Text.Trim() == "")
							txtMobile.Text	= dr["PER_CUST_MOBILE"].ToString();
					
						if (txtBUSINESS_PHONE.Text.Trim() == "")
							txtBUSINESS_PHONE.Text	= dr["CUSTOMER_BUSINESS_PHONE"].ToString();

						if (txtEXT.Text.Trim() == "")
							txtEXT.Text	= dr["CUSTOMER_EXT"].ToString();
					}
					else
					{
						if (txtMobile.Text.Trim() == "")
							txtMobile.Text	= dr["PER_CUST_MOBILE"].ToString();
					
						if (txtBUSINESS_PHONE.Text.Trim() == "")
							txtBUSINESS_PHONE.Text	= dr["EMPLOYER_HOMEPHONE"].ToString();

						if (txtEXT.Text.Trim() == "")
							txtEXT.Text	= dr["EMP_EXT"].ToString();

					}
				}
				ds.Dispose();
			}
			catch(Exception objEx)
			{
				throw(objEx);				
			}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 
			}
			
		}

		public static void PopulateClientAddress(int CustomerId, TextBox txtAdd1
			, TextBox txtAdd2, TextBox txtCity
			, DropDownList cmbState, DropDownList cmbCountry
			, TextBox txtZip)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				DataSet ds = new DataSet();
			
				
				objDataWrapper.AddParameter("@CustomerID", CustomerId, SqlDbType.Int);

				ds = objDataWrapper.ExecuteDataSet("Proc_GetCustomerDetails");
			
				if (ds.Tables.Count == 0)
					return;

				if (ds.Tables[0].Rows.Count > 0)
				{
					System.Data.DataRow dr = ds.Tables[0].Rows[0];

					if (txtAdd1.Text.Trim() == "")
						txtAdd1.Text = dr["CUSTOMER_ADDRESS1"].ToString();

					if (txtAdd2.Text.Trim() == "")
						txtAdd2.Text = dr["CUSTOMER_ADDRESS2"].ToString();

					if (txtCity.Text.Trim() == "")
						txtCity.Text = dr["CUSTOMER_CITY"].ToString();

					if (cmbState.SelectedIndex == -1)
						cmbState.SelectedValue = dr["CUSTOMER_STATE"].ToString();

					if (cmbCountry.SelectedIndex == -1)
						cmbCountry.SelectedValue = dr["CUSTOMER_COUNTRY"].ToString();

					if (txtZip.Text.Trim() == "")
						txtZip.Text	= dr["CUSTOMER_ZIP"].ToString();
					
				}
				ds.Dispose();
			}
			catch(Exception objEx)
			{
				throw(objEx);				
			}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 
			}
			
		}

		/// <summary>
		/// Fetch the specified client address in XML
		/// </summary>
		public static string PopulateClientAddress(int CustomerId)
		{
			//strCountyXML = "";
			DataSet ds = new DataSet();
			System.Text.StringBuilder objAddXml = new System.Text.StringBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				
				//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CustomerID", CustomerId, SqlDbType.Int);

				ds = objDataWrapper.ExecuteDataSet("Proc_GetCustomerDetails");
				objAddXml.Append("<address>");
			
				if (ds.Tables.Count == 0)
				{
					objAddXml.Append("</address>");
					return objAddXml.ToString();
				}

				if (ds.Tables[0].Rows.Count > 0)
				{
					System.Data.DataRow dr = ds.Tables[0].Rows[0];

					objAddXml.Append("<address1>");
					objAddXml.Append(dr["CUSTOMER_ADDRESS1"].ToString());
					objAddXml.Append("</address1>");

					objAddXml.Append("<address2>");
					objAddXml.Append(dr["CUSTOMER_ADDRESS2"].ToString());
					objAddXml.Append("</address2>");

					objAddXml.Append("<city>");
					objAddXml.Append(dr["CUSTOMER_CITY"].ToString());
					objAddXml.Append("</city>");

					objAddXml.Append("<country>");
					objAddXml.Append(dr["CUSTOMER_COUNTRY"].ToString());
					objAddXml.Append("</country>");
					
					objAddXml.Append("<state>");
					objAddXml.Append(dr["CUSTOMER_STATE"].ToString());
					objAddXml.Append("</state>");

					objAddXml.Append("<zip>");
					objAddXml.Append(dr["CUSTOMER_ZIP"].ToString());
					objAddXml.Append("</zip>");
					//Added By Swastika on 21st Mar'06 for Gen Iss # 2367
					objAddXml.Append("<phone>");
					objAddXml.Append(dr["CUSTOMER_HOME_PHONE"].ToString());
					objAddXml.Append("</phone>");

					objAddXml.Append("<email>");
					objAddXml.Append(dr["CUSTOMER_Email"].ToString());
					objAddXml.Append("</email>");
				
					objAddXml.Append("<mobile>");
					objAddXml.Append(dr["PER_CUST_MOBILE"].ToString());
					objAddXml.Append("</mobile>");

					objAddXml.Append("<CUSTOMER_BUSINESS_PHONE>");
					objAddXml.Append(dr["CUSTOMER_BUSINESS_PHONE"].ToString());
					objAddXml.Append("</CUSTOMER_BUSINESS_PHONE>");

					objAddXml.Append("<CUSTOMER_EXT>");
					objAddXml.Append(dr["CUSTOMER_EXT"].ToString());
					objAddXml.Append("</CUSTOMER_EXT>");

					objAddXml.Append("<EMPLOYER_HOMEPHONE>");
					objAddXml.Append(dr["EMPLOYER_HOMEPHONE"].ToString());
					objAddXml.Append("</EMPLOYER_HOMEPHONE>");

					objAddXml.Append("<EMP_EXT>");
					objAddXml.Append(dr["EMP_EXT"].ToString());
					objAddXml.Append("</EMP_EXT>");

                    objAddXml.Append("<NUMBER>");
                    objAddXml.Append(dr["NUMBER"].ToString());
                    objAddXml.Append("</NUMBER>");

                    objAddXml.Append("<DISTRICT>");
                    objAddXml.Append(dr["DISTRICT"].ToString());
                    objAddXml.Append("</DISTRICT>");

                    objAddXml.Append("</address>");
				}

				//				if (ds.Tables[1].Rows.Count > 0)
				//				{
				//					System.Data.DataRow dr;
				//					for (int ctr=0 ; ctr<ds.Tables[1].Rows.Count; ctr++)
				//					{
				//						dr = ds.Tables[1].Rows[ctr];
				//						strCountyXML += "<county>";
				//						strCountyXML += dr["LOBID"].ToString();
				//						strCountyXML += dr["COUNTY"].ToString();
				//						strCountyXML += dr["TERR"].ToString();
				//						strCountyXML += "</county>";
				//
				//					}
				//				}


				ds.Dispose();
			}
			catch(Exception objEx)
			{
				throw(objEx);				
			}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 
			
			}
			
			return objAddXml.ToString();
		}

		public DataSet FetchHomeOwnerPolicyList(int CustomerID)
		{
			DataSet ds = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMERID", CustomerID, SqlDbType.Int);
				ds = objDataWrapper.ExecuteDataSet("Proc_FetchHomePolicyListForCustomer");
				
				if (ds.Tables[0].Rows.Count > 0)				
					return ds;
				else
					return null;
				
				//ds.Dispose();
			}
			catch(Exception objEx)
			{
				throw(objEx);				
			}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 			
			}
		}


		/// <summary>
		/// Fetch the specified client address in XML
		/// </summary>
		public static string PopulateClientCounty(int CustomerId)
		{
			//strCountyXML = "";
			DataSet ds = new DataSet();
			System.Text.StringBuilder objAddXml = new System.Text.StringBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				

				objDataWrapper.AddParameter("@CustomerID", CustomerId, SqlDbType.Int);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCustomerCounty");
				
			
				if (ds.Tables.Count == 0)
				{
					objAddXml.Append("<COUNTYXML>");
					objAddXml.Append("</COUNTYXML>");
					return objAddXml.ToString();
				}
				
				if (ds.Tables[0].Rows.Count > 0)
				{
					System.Data.DataRow dr;
					objAddXml.Append("<COUNTYXML>");
					for (int ctr=0 ; ctr<ds.Tables[0].Rows.Count; ctr++)
					{
						dr = ds.Tables[0].Rows[ctr];
						objAddXml.Append("<LOB LOBID =\"" + dr["LOBID"].ToString() + "\">");
						objAddXml.Append("<COUNTY>");
						objAddXml.Append(dr["COUNTY"].ToString());
						objAddXml.Append("</COUNTY>");
						objAddXml.Append("<TERR>");
						objAddXml.Append(dr["TERR"].ToString());
						objAddXml.Append("</TERR>");
						objAddXml.Append("</LOB>");

					}
					objAddXml.Append("</COUNTYXML>");
				}


				ds.Dispose();
			}
			catch(Exception objEx)
			{
				throw(objEx);				
			}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 			
			}
			
			return objAddXml.ToString();
		}

		public void ActivateDeactivate(ClsCustomerInfo objCustomerInfo, string strStatus)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateCustomer";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			       
			try
			{
				objDataWrapper.AddParameter("@CODE",objCustomerInfo.CustomerId);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);				

				int returnResult = 0;
				if(TransactionLogRequired)
				{						
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					//objTransactionInfo.APP_ID			=	objCustomerInfo.APP_ID;
					//objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objCustomerInfo.CustomerId;
					objTransactionInfo.RECORDED_BY		=	objCustomerInfo.MODIFIED_BY;

                    if (strStatus.ToUpper() == "Y")
                    {
                        objTransactionInfo.TRANS_TYPE_ID = 162;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1462", ""); //"Customer is Activated";
                    }
                    else
                    {
                        objTransactionInfo.TRANS_TYPE_ID = 163;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1463", ""); //"Customer is Deactivated";
                    }
					//objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;					
                    objTransactionInfo.CUSTOM_INFO = ";"+ Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1790","")+" = " + objCustomerInfo.CustomerFirstName + " " + objCustomerInfo.CustomerMiddleName + " " + objCustomerInfo.CustomerLastName + "; "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1792","") + objCustomerInfo.CustomerCode; //";Customer Name = " + objCustomerInfo.CustomerFirstName + " " + objCustomerInfo.CustomerMiddleName + " " + objCustomerInfo.CustomerLastName + ";Customer Code = " + objCustomerInfo.CustomerCode;
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
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
		
		public DataSet CustomerDetails(int intCustomerId)
		{
			DataSet dsCustomer = new DataSet();
			DataWrapper objDataWrapper= new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CustomerId",intCustomerId);
			dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetCustomerDetailsManager");

			if(dsCustomer.Tables[0].Rows.Count>0)
			{
				return dsCustomer;
			}
			else
			{
				return null;
			}
			
		}
		
		/// <summary>
		/// Checks for the existence of a customer based on the parameters passed
		/// </summary>
		/// <param name="objCustomerInfo"></param>
		/// <param name="objDataWrapper"></param>
		/// <returns></returns>
		public int CheckCustomerExistence(Cms.Model.Client.ClsCustomerInfo objCustomerInfo,DataWrapper objDataWrapper)
		{
			string strStoredProc =	"Proc_CheckCustomerExistence";
			
			try
			{
			//DataWrapper gobjSqlHelper;
			//gobjSqlHelper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);	
				
			objDataWrapper.AddParameter("@CUSTOMER_FIRST_NAME",objCustomerInfo.CustomerFirstName);			
			objDataWrapper.AddParameter("@CUSTOMER_LAST_NAME",objCustomerInfo.CustomerLastName);
			objDataWrapper.AddParameter("@CUSTOMER_ADDRESS1",objCustomerInfo.CustomerAddress1);
			objDataWrapper.AddParameter("@CUSTOMER_CITY",objCustomerInfo.CustomerCity);
			objDataWrapper.AddParameter("@CUSTOMER_STATE",objCustomerInfo.CustomerState);
			objDataWrapper.AddParameter("@CUSTOMER_ZIP",objCustomerInfo.CustomerZip);
			objDataWrapper.AddParameter("@CUSTOMER_AGENCY_ID",objCustomerInfo.CustomerAgencyId);

			SqlParameter retVal = (SqlParameter)objDataWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);

			objDataWrapper.ExecuteNonQuery(strStoredProc);
			int returnResult = Convert.ToInt32(retVal.Value);
			objDataWrapper.ClearParameteres();
			objDataWrapper.Dispose();

			return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -2 ;
			}

		}

		/// <summary>
		/// Inserting Customer details
		/// </summary>
		/// <param name="objCustomerInfo">Modal Object for Customer</param>
		/// <returns></returns>
		public int AddCustomer(Cms.Model.Client.ClsCustomerInfo objCustomerInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_InsertCustomerGenInfo_ACORD";
			
			DateTime	RecordDate		=	DateTime.Now;
			
			//Set transaction label in the new object, if required
			

			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			SqlParameter objCustID = (SqlParameter)objDataWrapper.AddParameter("@CUSTOMER_ID",objCustomerInfo.CustomerId,SqlDbType.Int,ParameterDirection.Output);
			objDataWrapper.AddParameter("@CUSTOMER_CODE",objCustomerInfo.CustomerCode );
			objDataWrapper.AddParameter("@CUSTOMER_TYPE",objCustomerInfo.CustomerType);

			if (objCustomerInfo.CustomerParent != 0)
			{
				objDataWrapper.AddParameter("@CUSTOMER_PARENT", objCustomerInfo.CustomerParent);
			}
			else
			{
				objDataWrapper.AddParameter("@CUSTOMER_PARENT", null);
			}
			objDataWrapper.AddParameter("@CUSTOMER_SUFFIX",objCustomerInfo.CustomerSuffix);
			objDataWrapper.AddParameter("@CUSTOMER_FIRST_NAME",objCustomerInfo.CustomerFirstName );
			
			//if (objCustomerInfo.CustomerType == CUST_TYPE_PERSONAL)
			//{
				objDataWrapper.AddParameter("@CUSTOMER_MIDDLE_NAME",objCustomerInfo.CustomerMiddleName );
				objDataWrapper.AddParameter("@CUSTOMER_LAST_NAME",objCustomerInfo.CustomerLastName );
			//}
			//else
			//{
			//	objDataWrapper.AddParameter("@CUSTOMER_MIDDLE_NAME", null);
			//	objDataWrapper.AddParameter("@CUSTOMER_LAST_NAME", null);
			//}

			objDataWrapper.AddParameter("@CUSTOMER_ADDRESS1",objCustomerInfo.CustomerAddress1 );
			objDataWrapper.AddParameter("@CUSTOMER_ADDRESS2",objCustomerInfo.CustomerAddress2 );
			objDataWrapper.AddParameter("@CUSTOMER_CITY",objCustomerInfo.CustomerCity);
			objDataWrapper.AddParameter("@CUSTOMER_COUNTRY",objCustomerInfo.CustomerCountry );
			objDataWrapper.AddParameter("@CUSTOMER_STATE",objCustomerInfo.CustomerState);
			objDataWrapper.AddParameter("@CUSTOMER_ZIP",objCustomerInfo.CustomerZip);
			objDataWrapper.AddParameter("@CUSTOMER_BUSINESS_TYPE",objCustomerInfo.CustomerBusinessType );
			objDataWrapper.AddParameter("@CUSTOMER_BUSINESS_DESC",objCustomerInfo.CustomerBusinessDesc);
			objDataWrapper.AddParameter("@CUSTOMER_CONTACT_NAME",objCustomerInfo.CustomerContactName );
			objDataWrapper.AddParameter("@CUSTOMER_BUSINESS_PHONE",objCustomerInfo.CustomerBusinessPhone);
			objDataWrapper.AddParameter("@CUSTOMER_EXT",objCustomerInfo.CustomerExt );
			objDataWrapper.AddParameter("@CUSTOMER_HOME_PHONE",objCustomerInfo.CustomerHomePhone );
			objDataWrapper.AddParameter("@CUSTOMER_MOBILE",objCustomerInfo.CustomerMobile);
			//adding SSN
			objDataWrapper.AddParameter("@SSN_NO",objCustomerInfo.SSN_NO);

			objDataWrapper.AddParameter("@CUSTOMER_FAX",objCustomerInfo.CustomerFax );
			objDataWrapper.AddParameter("@CUSTOMER_PAGER_NO",objCustomerInfo.CustomerPagerNo );
			objDataWrapper.AddParameter("@CUSTOMER_Email",objCustomerInfo.CustomerEmail);
			objDataWrapper.AddParameter("@CUSTOMER_WEBSITE",objCustomerInfo.CustomerWebsite );
			
			if (objCustomerInfo.CustomerAgencyId != 0)
				objDataWrapper.AddParameter("@CUSTOMER_AGENCY_ID", objCustomerInfo.CustomerAgencyId );
			else
				objDataWrapper.AddParameter("@CUSTOMER_AGENCY_ID", null);

			
			//objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_SCORE",DefaultValues.GetDecimalNullFromNegative(objCustomerInfo.CustomerInsuranceScore));
			objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_SCORE",objCustomerInfo.CustomerInsuranceScore);
						
			if (objCustomerInfo.CustomerInsuranceReceivedDate != DateTime.MinValue)
			{
				objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_RECEIVED_DATE",objCustomerInfo.CustomerInsuranceReceivedDate );
			}
			else
			{
				objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_RECEIVED_DATE",null);
			}
			objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE",objCustomerInfo.CustomerReasonCode );
			if(objCustomerInfo.CustomerReasonCode2 != "")
			{
				objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE2",objCustomerInfo.CustomerReasonCode2 );
			}
			else
			{
				objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE2",null);
				//objCustomerInfo.CustomerReasonCode2 = 0;
			}
			if(objCustomerInfo.CustomerReasonCode3 != "")
			{
				objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE3",objCustomerInfo.CustomerReasonCode3 );
			}
			else
			{
				objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE3",null);
				//objCustomerInfo.CustomerReasonCode3 = 0;
			}
			if(objCustomerInfo.CustomerReasonCode4 != "")
			{
				objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE4",objCustomerInfo.CustomerReasonCode4 );
			}
			else
			{
				objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE4",null);
				//objCustomerInfo.CustomerReasonCode4 = 0;
			}

			//Account Info Fields
			objDataWrapper.AddParameter("@CustomerProducerId",objCustomerInfo.CustomerProducerId);
			objDataWrapper.AddParameter("@CustomerAccountExecutiveId",objCustomerInfo.CustomerAccountExecutiveId);

			objDataWrapper.AddParameter("@CustomerCsr",objCustomerInfo.CustomerCsr);
			objDataWrapper.AddParameter("@CustomerReferredBy",objCustomerInfo.CustomerReferredBy);

			// End Account Info Fields
			
			objDataWrapper.AddParameter("@PREFIX",objCustomerInfo.PREFIX);
			objDataWrapper.AddParameter("@CREATED_BY",objCustomerInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",objCustomerInfo.CREATED_DATETIME );
			SqlDateTime sqldatenull=SqlDateTime.Null; 
			objDataWrapper.AddParameter("@LAST_INSURANCE_SCORE_FETCHED",sqldatenull);
			
			// customer dob,marital status,gender
			objDataWrapper.AddParameter("@GENDER",objCustomerInfo.GENDER);
			objDataWrapper.AddParameter("@MARITAL_STATUS",objCustomerInfo.MARITAL_STATUS);
			if(objCustomerInfo.DATE_OF_BIRTH!= DateTime.MinValue)
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",objCustomerInfo.DATE_OF_BIRTH);
			else
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",null);
			objDataWrapper.AddParameter("@APPLICANT_OCCU",objCustomerInfo.APPLICANT_OCCU);
			//objDataWrapper.ExecuteNonQuery(strStoredProc);
			//
				
			int returnResult = 0;
			int iCustomerID	= 0;

			
			
			returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
			
			if ( objCustID.Value == System.DBNull.Value ) return -1;

			iCustomerID = Convert.ToInt32(objCustID.Value);

			return iCustomerID;

		}

		public int UpdateCustomer(Cms.Model.Client.ClsCustomerInfo objCustomerInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_UpdateCustomerInfo_ACORD";
			

			DateTime	RecordDate		=	DateTime.Now;
			
			//Set transaction label in the new object, if required
			if ( this.boolTransactionRequired)
			{
				//objCustomerInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"client\aspx\AddCustomer.aspx.resx");
			}

			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMER_ID",objCustomerInfo.CustomerId);
			objDataWrapper.AddParameter("@CUSTOMER_CODE",objCustomerInfo.CustomerCode );
			objDataWrapper.AddParameter("@CUSTOMER_TYPE",objCustomerInfo.CustomerType);
			
			if (objCustomerInfo.CustomerParent != 0)
			{
				objDataWrapper.AddParameter("@CUSTOMER_PARENT", objCustomerInfo.CustomerParent);
			}
			else
			{
				objDataWrapper.AddParameter("@CUSTOMER_PARENT", null);
			}
			
			objDataWrapper.AddParameter("@CUSTOMER_SUFFIX",objCustomerInfo.CustomerSuffix);
			objDataWrapper.AddParameter("@CUSTOMER_FIRST_NAME",objCustomerInfo.CustomerFirstName );
		

			//if (objCustomerInfo.CustomerType == CUST_TYPE_PERSONAL)
			//{
				objDataWrapper.AddParameter("@CUSTOMER_MIDDLE_NAME",objCustomerInfo.CustomerMiddleName );
				objDataWrapper.AddParameter("@CUSTOMER_LAST_NAME",objCustomerInfo.CustomerLastName );
			//}
			//else
			//{
			//	objDataWrapper.AddParameter("@CUSTOMER_MIDDLE_NAME", null);
			//	objDataWrapper.AddParameter("@CUSTOMER_LAST_NAME", null);
			//}
			
			objDataWrapper.AddParameter("@CUSTOMER_ADDRESS1",objCustomerInfo.CustomerAddress1 );
			objDataWrapper.AddParameter("@CUSTOMER_ADDRESS2",objCustomerInfo.CustomerAddress2 );
			objDataWrapper.AddParameter("@CUSTOMER_CITY",objCustomerInfo.CustomerCity);
			objDataWrapper.AddParameter("@CUSTOMER_COUNTRY",objCustomerInfo.CustomerCountry );
			objDataWrapper.AddParameter("@CUSTOMER_STATE",objCustomerInfo.CustomerState);
			objDataWrapper.AddParameter("@CUSTOMER_ZIP",objCustomerInfo.CustomerZip);
			objDataWrapper.AddParameter("@CUSTOMER_BUSINESS_TYPE",objCustomerInfo.CustomerBusinessType );
			objDataWrapper.AddParameter("@CUSTOMER_BUSINESS_DESC",objCustomerInfo.CustomerBusinessDesc);
			objDataWrapper.AddParameter("@CUSTOMER_CONTACT_NAME",objCustomerInfo.CustomerContactName );
			objDataWrapper.AddParameter("@CUSTOMER_BUSINESS_PHONE",objCustomerInfo.CustomerBusinessPhone);
			objDataWrapper.AddParameter("@CUSTOMER_EXT",objCustomerInfo.CustomerExt );
			objDataWrapper.AddParameter("@CUSTOMER_HOME_PHONE",objCustomerInfo.CustomerHomePhone );
			objDataWrapper.AddParameter("@CUSTOMER_MOBILE",objCustomerInfo.CustomerMobile);
			objDataWrapper.AddParameter("@CUSTOMER_FAX",objCustomerInfo.CustomerFax );
			objDataWrapper.AddParameter("@CUSTOMER_PAGER_NO",objCustomerInfo.CustomerPagerNo );
			objDataWrapper.AddParameter("@CUSTOMER_Email",objCustomerInfo.CustomerEmail);
			objDataWrapper.AddParameter("@CUSTOMER_WEBSITE",objCustomerInfo.CustomerWebsite );
			
			//objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_SCORE",DefaultValues.GetDecimalNullFromNegative(objCustomerInfo.CustomerInsuranceScore));
			objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_SCORE",objCustomerInfo.CustomerInsuranceScore);
				
			if (objCustomerInfo.CustomerInsuranceReceivedDate.Ticks != 0)
			{
				objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_RECEIVED_DATE",objCustomerInfo.CustomerInsuranceReceivedDate );
			}
			else
			{
				objDataWrapper.AddParameter("@CUSTOMER_INSURANCE_RECEIVED_DATE",null);
			}

			if (objCustomerInfo.CustomerAgencyId != 0)
				objDataWrapper.AddParameter("@CUSTOMER_AGENCY_ID", objCustomerInfo.CustomerAgencyId );
			else
				objDataWrapper.AddParameter("@CUSTOMER_AGENCY_ID", null);
			objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE",objCustomerInfo.CustomerReasonCode );
			if(objCustomerInfo.CustomerReasonCode2 != "")
			{
				objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE2",objCustomerInfo.CustomerReasonCode2 );
			}
			else
			{
				objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE2",null);
				//objCustomerInfo.CustomerReasonCode2 = 0;
			}
			if(objCustomerInfo.CustomerReasonCode3 != "")
			{
				objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE3",objCustomerInfo.CustomerReasonCode3 );
			}
			else
			{
				objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE3",null);
				//objCustomerInfo.CustomerReasonCode3 = 0;
			}
			if(objCustomerInfo.CustomerReasonCode4 != "")
			{
				objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE4",objCustomerInfo.CustomerReasonCode4 );
			}
			else
			{
				objDataWrapper.AddParameter("@CUSTOMER_REASON_CODE4",null);
				//objCustomerInfo.CustomerReasonCode4 = 0;
			}

			//Account Info Fields
			objDataWrapper.AddParameter("@CustomerProducerId",objCustomerInfo.CustomerProducerId);
			objDataWrapper.AddParameter("@CustomerAccountExecutiveId",objCustomerInfo.CustomerAccountExecutiveId);
			
			objDataWrapper.AddParameter("@CustomerCsr",objCustomerInfo.CustomerCsr);
			objDataWrapper.AddParameter("@CustomerReferredBy",objCustomerInfo.CustomerReferredBy);
			
			// customer dob,marital status,gender
			objDataWrapper.AddParameter("@GENDER",objCustomerInfo.GENDER);
			objDataWrapper.AddParameter("@MARITAL_STATUS",objCustomerInfo.MARITAL_STATUS);
			objDataWrapper.AddParameter("@DATE_OF_BIRTH",objCustomerInfo.DATE_OF_BIRTH);
			objDataWrapper.AddParameter("@APPLICANT_OCCU",objCustomerInfo.APPLICANT_OCCU);

			// End Account Info Fields
			objDataWrapper.AddParameter("@PREFIX",objCustomerInfo.PREFIX );
			objDataWrapper.AddParameter("@MODIFIED_BY",objCustomerInfo.MODIFIED_BY);
			objDataWrapper.AddParameter("@MODIFIED_DATETIME",objCustomerInfo.LAST_UPDATED_DATETIME);
			//objDataWrapper.AddParameter("@Cust_Id",null,SqlDbType.Int,ParameterDirection.Output );

			//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@Cust_Id",null,SqlDbType.Int,ParameterDirection.Output);

			//objDataWrapper.ExecuteNonQuery(strStoredProc);
			//
					
			int returnResult = 0;
						
			
			//if transaction required
			//if(TransactionLogRequired) 
			//{
				/*
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objOldCustomerInfo,objCustomerInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
								
					objTransactionInfo.CLIENT_ID		=	objCustomerInfo.CustomerId;
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objCustomerInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Customer is modified";	
					objTransactionInfo.CHANGE_XML		=	strTranXML;
								
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					*/
			//}
			//else//if no transaction required
			//{
				returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
			//}
				
			//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			//int iCustomerID		=	int.Parse(objSqlParameter.Value.ToString());

			

			int CUSTOMER_ID		=	int.Parse(objDataWrapper.CommandParameters[0].Value.ToString());
			objDataWrapper.ClearParameteres();

			//Commit the transaction if all opeations are successfull.
			//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			if (CUSTOMER_ID == -1)
			{
				return -1;
			}
			else
			{
				return returnResult;
			}
			
		}

		public DataTable FetchCustomerFaxNumber(int CustomerID)
		{
			
			DataSet dsCustomer	= new DataSet();
					

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@Customer_ID",CustomerID,SqlDbType.Int);

			dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetCustomerFax");
			
			return dsCustomer.Tables[0];
		}
		//Itrack 4965
		public DataSet GetPolicyInsuredName(int custId,int policyId,int policyVersionId)
		{
			string		strStoredProc	=	"Proc_GetPolicyInsuredName";//Will be replaced with CONST
			DataSet ds= new DataSet();
			try
			{
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
						
				objDataWrapper.AddParameter("@CUSTOMER_ID",custId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionId,SqlDbType.Int);
				ds = objDataWrapper.ExecuteDataSet(strStoredProc);
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			
			return ds;
		}


		public string GetCustomerLoss(string CUSTOMER_ID, string LOB, string StartLossDate, string EndLossDate)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{

				DataSet dsCustomer = new DataSet();
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);				
				objDataWrapper.AddParameter("@LOB",LOB);
				objDataWrapper.AddParameter("@STARTLOSSDATE",StartLossDate);
				objDataWrapper.AddParameter("@ENDLOSSDATE",EndLossDate);

				dsCustomer = objDataWrapper.ExecuteDataSet("GetCustomerLoss");
				string RetVal="";
				if (dsCustomer!=null && dsCustomer.Tables[0].Rows.Count>0)
				{
					RetVal = dsCustomer.GetXml();
				}
				return RetVal;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 
			}

		}

		//Added by Mohit Agarwal 20-Nov-2007
		public int CopyCustomerTabs(int CustomerID,int LoggedInUser,ref int newCustId)
		{			
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			objDataWrapper.AddParameter("@Customer_ID",CustomerID,SqlDbType.Int);
			//Added by Asfa (01-May-2008) - iTrack issue #4103
			objDataWrapper.AddParameter("@USER_ID",LoggedInUser,SqlDbType.Int);
			//Added by Uday on 13-March-2008           
			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@New_Cust_Id",SqlDbType.Int,ParameterDirection.Output);
			//
			try
			{
				int returnResult = objDataWrapper.ExecuteNonQuery("Proc_CopyCustomerTabs");
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
				//Added by uday on 13-March-2008				
				if(returnResult > 0 && objSqlParameter.Value!=System.DBNull.Value)
					newCustId		=	int.Parse(objSqlParameter.Value.ToString());
				//
				return returnResult;
				
			}
			catch
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				return -1;
				//throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		
		/// <summary>
		/// Function for viewing the details of Attention Notes
		/// </summary>
		/// <returns></returns>
		public DataSet GetCustomerStatus(int CustomerID, string strCalledFor)
		{
			DataSet dsCust = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@Customer_ID",CustomerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@CALLED_FOR",strCalledFor,SqlDbType.VarChar);

			dsCust = objDataWrapper.ExecuteDataSet("Proc_GetCustomerStatus");
			
			return dsCust;
		}

		#region Policy Agency Email FAX : 14 may 2008
		public DataSet GetPolicyAgencyEmail(int custId,int policyId,int policyVersionId)
		{
			string		strStoredProc	=	"Proc_GetPolicyAgencyEmail";//Will be replaced with CONST
			DataSet ds= new DataSet();
			try
			{
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
						
				objDataWrapper.AddParameter("@CUSTOMER_ID",custId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionId,SqlDbType.Int);
				ds = objDataWrapper.ExecuteDataSet(strStoredProc);
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			
			return ds;
		}
		#endregion
		


		#region Policy Header Functions
		/// <summary>
		/// Get the Policy Header Details
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <returns></returns>
		public DataSet GetPolicyHeaderDetails(int CustomerID, int PolicyID, int PolicyVersionID)
		{
			DataSet dsCustomer = new DataSet();
			DataWrapper objDataWrapper= new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objDataWrapper.AddParameter("@POLICY_ID",PolicyID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
            objDataWrapper.AddParameter("@lang_id",BlCommon.ClsCommon.BL_LANG_ID);
			dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetPolicyHeaderDetails");

			if(dsCustomer.Tables[0].Rows.Count>0)
			{
				return dsCustomer;
			}
			else
			{
				return null;
			}
			
		}
		#endregion
		#region Process Header Functions
		/// <summary>
		/// Get the Process Header Details
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <returns></returns>
		public DataSet GetProcessHeaderDetails(int CustomerID, int PolicyID, int PolicyVersionID)
		{
			DataSet dsProcess = new DataSet();
			DataWrapper objDataWrapper= new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objDataWrapper.AddParameter("@POLICY_ID",PolicyID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
			dsProcess = objDataWrapper.ExecuteDataSet("Proc_GetProcessHeaderDetails");

			if(dsProcess.Tables[0].Rows.Count>0)
			{
				return dsProcess;
			}
			else
			{
				return null;
			}
			
		}

		#endregion

        /// <summary>
        /// Get the Customer Details for copy policy
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
        public DataSet GetCustomerDetailsforCopyPolicy(int CustomerID,int POLICY_ID,int POLICY_VERSION_ID)
        {
            DataSet dsCustomerDetails = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);

            dsCustomerDetails = objDataWrapper.ExecuteDataSet("Proc_GetCustomerDetailsForCopyPolicy");
            if (dsCustomerDetails.Tables[0].Rows.Count > 0)
            {
                return dsCustomerDetails;
            }
            else
            {
                return null;
            }

        }
	}
}
