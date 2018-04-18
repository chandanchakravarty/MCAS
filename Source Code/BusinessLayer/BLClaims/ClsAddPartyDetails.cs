/******************************************************************************************
<Author				: -   Amar
<Start Date				: -	5/8/2006 5:11:10 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Claims;
using Cms.BusinessLayer.BlCommon;

namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// Test
	/// </summary>
	public class ClsAddPartyDetails : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		CLM_PARTIES			=	"CLM_PARTIES";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _PARTY_ID;		
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
		public ClsAddPartyDetails()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			//base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objAddPartyDetailsInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsAddPartyDetailsInfo objAddPartyDetailsInfo,string ExpertServiceType, string OldExpertServiceId, out string NewExpertServiceId)
		{
			string		strStoredProc	=	"Proc_InsertCLM_PARTIES";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			int returnResult = 0;
			NewExpertServiceId = "0";
			string ExistsExpertServiceId = "-1";
			try
			{
				
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objAddPartyDetailsInfo.CLAIM_ID);

				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@PARTY_ID",objAddPartyDetailsInfo.PARTY_ID);
				//objDataWrapper.AddParameter("@CLAIM_ID",objAddPartyDetailsInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@VENDOR_CODE",objAddPartyDetailsInfo.VENDOR_CODE);
				DataSet dsExp = objDataWrapper.ExecuteDataSet("Proc_GetExpertServiceId4ClaimParty");
				if(dsExp.Tables.Count > 0 && dsExp.Tables[0].Rows.Count > 0)
				{
					ExistsExpertServiceId = dsExp.Tables[0].Rows[0]["EXPERT_SERVICE_ID"].ToString();
				}
				objDataWrapper.ClearParameteres();

				if(objAddPartyDetailsInfo.PARTY_TYPE_ID==int.Parse(((int)ClsClaims.enumClaimPartyTypes.EXPERTSERVICEPROVIDERS).ToString()))
				{
					if(OldExpertServiceId == "NEW")
					{
						if(ExistsExpertServiceId != "-1")
						{
							OldExpertServiceId = ExistsExpertServiceId;
						}
					}
					returnResult =  AddUpdateExpertData(objDataWrapper,objAddPartyDetailsInfo,ExpertServiceType,OldExpertServiceId,out NewExpertServiceId);
					if(returnResult<0)
						return returnResult;
					objDataWrapper.ClearParameteres();
				}
				else
				{
					if(ExistsExpertServiceId != "-1")
					{
						objAddPartyDetailsInfo.VENDOR_CODE = "";
					}
				}
				returnResult = 0;
				objDataWrapper.AddParameter("@CLAIM_ID",objAddPartyDetailsInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@NAME",objAddPartyDetailsInfo.NAME);
				objDataWrapper.AddParameter("@ADDRESS1",objAddPartyDetailsInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objAddPartyDetailsInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objAddPartyDetailsInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objAddPartyDetailsInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objAddPartyDetailsInfo.ZIP);
				objDataWrapper.AddParameter("@CONTACT_PHONE",objAddPartyDetailsInfo.CONTACT_PHONE);
				objDataWrapper.AddParameter("@CONTACT_EMAIL",objAddPartyDetailsInfo.CONTACT_EMAIL);
				objDataWrapper.AddParameter("@OTHER_DETAILS",objAddPartyDetailsInfo.OTHER_DETAILS);
				objDataWrapper.AddParameter("@CREATED_BY",objAddPartyDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objAddPartyDetailsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@PARTY_TYPE_ID",objAddPartyDetailsInfo.PARTY_TYPE_ID);
				objDataWrapper.AddParameter("@COUNTRY",objAddPartyDetailsInfo.COUNTRY);
				if(objAddPartyDetailsInfo.AGE==0 || objAddPartyDetailsInfo.AGE==-1)
						objDataWrapper.AddParameter("@AGE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@AGE",objAddPartyDetailsInfo.AGE);
				objDataWrapper.AddParameter("@EXTENT_OF_INJURY",objAddPartyDetailsInfo.EXTENT_OF_INJURY);
				objDataWrapper.AddParameter("@REFERENCE",objAddPartyDetailsInfo.REFERENCE);
				objDataWrapper.AddParameter("@PARTY_DETAIL",objAddPartyDetailsInfo.PARTY_DETAIL);
				objDataWrapper.AddParameter("@BANK_NAME",objAddPartyDetailsInfo.BANK_NAME);
                // Added by Santosh Gautam on 15 Nov 2010
                objDataWrapper.AddParameter("@AGENCY_BANK", objAddPartyDetailsInfo.AGENCY_BANK);
				objDataWrapper.AddParameter("@ACCOUNT_NUMBER",objAddPartyDetailsInfo.ACCOUNT_NUMBER);
				objDataWrapper.AddParameter("@ACCOUNT_NAME",objAddPartyDetailsInfo.ACCOUNT_NAME);
				objDataWrapper.AddParameter("@CONTACT_PHONE_EXT",objAddPartyDetailsInfo.CONTACT_PHONE_EXT);
				objDataWrapper.AddParameter("@CONTACT_FAX",objAddPartyDetailsInfo.CONTACT_FAX);
				objDataWrapper.AddParameter("@PARTY_TYPE_DESC",objAddPartyDetailsInfo.PARTY_TYPE_DESC);
				objDataWrapper.AddParameter("@PARENT_ADJUSTER",objAddPartyDetailsInfo.PARENT_ADJUSTER);
				objDataWrapper.AddParameter("@FEDRERAL_ID",objAddPartyDetailsInfo.FEDRERAL_ID);
				objDataWrapper.AddParameter("@PROCESSING_OPTION_1099",objAddPartyDetailsInfo.PROCESSING_OPTION_1099);
				objDataWrapper.AddParameter("@MASTER_VENDOR_CODE",objAddPartyDetailsInfo.MASTER_VENDOR_CODE);
				objDataWrapper.AddParameter("@VENDOR_CODE",objAddPartyDetailsInfo.VENDOR_CODE);
				objDataWrapper.AddParameter("@CONTACT_NAME",objAddPartyDetailsInfo.CONTACT_NAME);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE",objAddPartyDetailsInfo.EXPERT_SERVICE_TYPE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE_DESC",objAddPartyDetailsInfo.EXPERT_SERVICE_TYPE_DESC);

				objDataWrapper.AddParameter("@SUB_ADJUSTER",objAddPartyDetailsInfo.SUB_ADJUSTER);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_CONTACT_NAME",objAddPartyDetailsInfo.SUB_ADJUSTER_CONTACT_NAME);
				objDataWrapper.AddParameter("@SA_ADDRESS1",objAddPartyDetailsInfo.SA_ADDRESS1);
				objDataWrapper.AddParameter("@SA_ADDRESS2",objAddPartyDetailsInfo.SA_ADDRESS2);
				objDataWrapper.AddParameter("@SA_CITY",objAddPartyDetailsInfo.SA_CITY);
				objDataWrapper.AddParameter("@SA_COUNTRY",objAddPartyDetailsInfo.SA_COUNTRY);
				objDataWrapper.AddParameter("@SA_STATE",objAddPartyDetailsInfo.SA_STATE);
				objDataWrapper.AddParameter("@SA_ZIPCODE",objAddPartyDetailsInfo.SA_ZIPCODE);
				objDataWrapper.AddParameter("@SA_PHONE",objAddPartyDetailsInfo.SA_PHONE);
				objDataWrapper.AddParameter("@SA_FAX",objAddPartyDetailsInfo.SA_FAX);
				
				//Added by Mohit Agarwal 20-Aug-2008
				objDataWrapper.AddParameter("@MAIL_1099_ADD1",objAddPartyDetailsInfo.MAIL_1099_ADD1);
				objDataWrapper.AddParameter("@MAIL_1099_ADD2",objAddPartyDetailsInfo.MAIL_1099_ADD2);
				objDataWrapper.AddParameter("@MAIL_1099_CITY",objAddPartyDetailsInfo.MAIL_1099_CITY);
				objDataWrapper.AddParameter("@MAIL_1099_STATE",objAddPartyDetailsInfo.MAIL_1099_STATE);
				objDataWrapper.AddParameter("@MAIL_1099_ZIP",objAddPartyDetailsInfo.MAIL_1099_ZIP);
				objDataWrapper.AddParameter("@MAIL_1099_COUNTRY",objAddPartyDetailsInfo.MAIL_1099_COUNTRY);

				objDataWrapper.AddParameter("@MAIL_1099_NAME",objAddPartyDetailsInfo.MAIL_1099_NAME);
				objDataWrapper.AddParameter("@W9_FORM",objAddPartyDetailsInfo.W9_FORM);
                objDataWrapper.AddParameter("@ACCOUNT_TYPE", objAddPartyDetailsInfo.ACCOUNT_TYPE);

                objDataWrapper.AddParameter("@DISTRICT", objAddPartyDetailsInfo.DISTRICT);
                objDataWrapper.AddParameter("@REGIONAL_ID", objAddPartyDetailsInfo.REGIONAL_ID);
                objDataWrapper.AddParameter("@REGIONAL_ID_ISSUANCE", objAddPartyDetailsInfo.REGIONAL_ID_ISSUANCE);
                if(objAddPartyDetailsInfo.REGIONAL_ID_ISSUE_DATE!=DateTime.MinValue)
                  objDataWrapper.AddParameter("@REGIONAL_ID_ISSUE_DATE", objAddPartyDetailsInfo.REGIONAL_ID_ISSUE_DATE);
                else
                 objDataWrapper.AddParameter("@REGIONAL_ID_ISSUE_DATE", DBNull.Value);

                objDataWrapper.AddParameter("@BANK_NUMBER", objAddPartyDetailsInfo.BANK_NUMBER);
                objDataWrapper.AddParameter("@BANK_BRANCH", objAddPartyDetailsInfo.BANK_BRANCH);
                objDataWrapper.AddParameter("@MARITAL_STATUS", objAddPartyDetailsInfo.MARITAL_STATUS);
                objDataWrapper.AddParameter("@GENDER", objAddPartyDetailsInfo.GENDER);
                objDataWrapper.AddParameter("@PARTY_TYPE", objAddPartyDetailsInfo.PARTY_TYPE);
                objDataWrapper.AddParameter("@PAYMENT_METHOD", objAddPartyDetailsInfo.PAYMENT_METHOD);
                objDataWrapper.AddParameter("@PARTY_CPF_CNPJ", objAddPartyDetailsInfo.PARTY_CPF_CNPJ);
                objDataWrapper.AddParameter("@IS_BENEFICIARY", objAddPartyDetailsInfo.IS_BENEFICIARY);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@PARTY_ID",objAddPartyDetailsInfo.PARTY_ID,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objReturnSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue);

				
				if(TransactionLogRequired)
				{
					objAddPartyDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddPartyDetails.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objAddPartyDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//Done for Itrack Issue 6932 on 19 Jan 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objAddPartyDetailsInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1836", "");//"Party Detail Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
                    objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1794","") + claimNumber; //"Claim Number : " + claimNumber;//Done for Itrack Issue 6932 on 19 Jan 2010
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int PARTY_ID = int.Parse(objSqlParameter.Value.ToString());
				if(objReturnSqlParameter!=null && objReturnSqlParameter.Value.ToString()!="")
					returnResult = int.Parse(objReturnSqlParameter.Value.ToString());

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (returnResult>0 )				
					objAddPartyDetailsInfo.PARTY_ID = PARTY_ID;
				return returnResult;
				
//				if (PARTY_ID == -1)
//				{
//					return -1;
//				}
//				else
//				{
//					objAddPartyDetailsInfo.PARTY_ID = PARTY_ID;
//					return returnResult;
//				}
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
		/// Add Parties for Property Damage
		/// </summary>
		/// <param name="objAddPartyDetailsInfo"></param>
		/// <param name="ExpertServiceType"></param>
		/// <param name="OldExpertServiceId"></param>
		/// <param name="NewExpertServiceId"></param>
		/// <returns></returns>
		public int AddPartiesPropDamage(ClsAddPartyDetailsInfo objAddPartyDetailsInfo,string ExpertServiceType, string OldExpertServiceId, out string NewExpertServiceId)
		{
			string		strStoredProc	=	"Proc_InsertCLM_PARTIES";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			int returnResult = 0;
			NewExpertServiceId = "0";
			string ExistsExpertServiceId = "-1";
			try
			{
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objAddPartyDetailsInfo.CLAIM_ID);

				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@PARTY_ID",objAddPartyDetailsInfo.PARTY_ID);
				//objDataWrapper.AddParameter("@CLAIM_ID",objAddPartyDetailsInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@VENDOR_CODE",objAddPartyDetailsInfo.VENDOR_CODE);
				DataSet dsExp = objDataWrapper.ExecuteDataSet("Proc_GetExpertServiceId4ClaimParty");
				if(dsExp.Tables.Count > 0 && dsExp.Tables[0].Rows.Count > 0)
				{
					ExistsExpertServiceId = dsExp.Tables[0].Rows[0]["EXPERT_SERVICE_ID"].ToString();
				}
				objDataWrapper.ClearParameteres();

				if(objAddPartyDetailsInfo.PARTY_TYPE_ID==int.Parse(((int)ClsClaims.enumClaimPartyTypes.EXPERTSERVICEPROVIDERS).ToString()))
				{
					if(OldExpertServiceId == "NEW")
					{
						if(ExistsExpertServiceId != "-1")
						{
							OldExpertServiceId = ExistsExpertServiceId;
						}
					}
					returnResult =  AddUpdateExpertData(objDataWrapper,objAddPartyDetailsInfo,ExpertServiceType,OldExpertServiceId,out NewExpertServiceId);
					if(returnResult<0)
						return returnResult;
					objDataWrapper.ClearParameteres();
				}
				else
				{
					if(ExistsExpertServiceId != "-1")
					{
						objAddPartyDetailsInfo.VENDOR_CODE = "";
					}
				}

				returnResult = 0;
				objDataWrapper.AddParameter("@CLAIM_ID",objAddPartyDetailsInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@NAME",objAddPartyDetailsInfo.NAME);
				objDataWrapper.AddParameter("@ADDRESS1",objAddPartyDetailsInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objAddPartyDetailsInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objAddPartyDetailsInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objAddPartyDetailsInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objAddPartyDetailsInfo.ZIP);
				objDataWrapper.AddParameter("@CONTACT_PHONE",objAddPartyDetailsInfo.CONTACT_PHONE);
				objDataWrapper.AddParameter("@CONTACT_EMAIL",objAddPartyDetailsInfo.CONTACT_EMAIL);
				objDataWrapper.AddParameter("@OTHER_DETAILS",objAddPartyDetailsInfo.OTHER_DETAILS);
				objDataWrapper.AddParameter("@CREATED_BY",objAddPartyDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objAddPartyDetailsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@PARTY_TYPE_ID",objAddPartyDetailsInfo.PARTY_TYPE_ID);
				objDataWrapper.AddParameter("@COUNTRY",objAddPartyDetailsInfo.COUNTRY);
				if(objAddPartyDetailsInfo.AGE==0 || objAddPartyDetailsInfo.AGE==-1)
					objDataWrapper.AddParameter("@AGE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@AGE",objAddPartyDetailsInfo.AGE);
				objDataWrapper.AddParameter("@EXTENT_OF_INJURY",objAddPartyDetailsInfo.EXTENT_OF_INJURY);
				objDataWrapper.AddParameter("@REFERENCE",objAddPartyDetailsInfo.REFERENCE);
				objDataWrapper.AddParameter("@PARTY_DETAIL",objAddPartyDetailsInfo.PARTY_DETAIL);
				objDataWrapper.AddParameter("@BANK_NAME",objAddPartyDetailsInfo.BANK_NAME);
                // Added by Santosh Gautam on 15 Nov 2010
                objDataWrapper.AddParameter("@AGENCY_BANK", objAddPartyDetailsInfo.AGENCY_BANK);
				objDataWrapper.AddParameter("@ACCOUNT_NUMBER",objAddPartyDetailsInfo.ACCOUNT_NUMBER);
				objDataWrapper.AddParameter("@ACCOUNT_NAME",objAddPartyDetailsInfo.ACCOUNT_NAME);
				objDataWrapper.AddParameter("@CONTACT_PHONE_EXT",objAddPartyDetailsInfo.CONTACT_PHONE_EXT);
				objDataWrapper.AddParameter("@CONTACT_FAX",objAddPartyDetailsInfo.CONTACT_FAX);
				objDataWrapper.AddParameter("@PARTY_TYPE_DESC",objAddPartyDetailsInfo.PARTY_TYPE_DESC);
				objDataWrapper.AddParameter("@PARENT_ADJUSTER",objAddPartyDetailsInfo.PARENT_ADJUSTER);
				objDataWrapper.AddParameter("@FEDRERAL_ID",objAddPartyDetailsInfo.FEDRERAL_ID);
				objDataWrapper.AddParameter("@PROCESSING_OPTION_1099",objAddPartyDetailsInfo.PROCESSING_OPTION_1099);
				objDataWrapper.AddParameter("@MASTER_VENDOR_CODE",objAddPartyDetailsInfo.MASTER_VENDOR_CODE);
				objDataWrapper.AddParameter("@VENDOR_CODE",objAddPartyDetailsInfo.VENDOR_CODE);
				objDataWrapper.AddParameter("@CONTACT_NAME",objAddPartyDetailsInfo.CONTACT_NAME);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE",objAddPartyDetailsInfo.EXPERT_SERVICE_TYPE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE_DESC",objAddPartyDetailsInfo.EXPERT_SERVICE_TYPE_DESC);

				objDataWrapper.AddParameter("@SUB_ADJUSTER",objAddPartyDetailsInfo.SUB_ADJUSTER);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_CONTACT_NAME",objAddPartyDetailsInfo.SUB_ADJUSTER_CONTACT_NAME);
				objDataWrapper.AddParameter("@SA_ADDRESS1",objAddPartyDetailsInfo.SA_ADDRESS1);
				objDataWrapper.AddParameter("@SA_ADDRESS2",objAddPartyDetailsInfo.SA_ADDRESS2);
				objDataWrapper.AddParameter("@SA_CITY",objAddPartyDetailsInfo.SA_CITY);
				objDataWrapper.AddParameter("@SA_COUNTRY",objAddPartyDetailsInfo.SA_COUNTRY);
				objDataWrapper.AddParameter("@SA_STATE",objAddPartyDetailsInfo.SA_STATE);
				objDataWrapper.AddParameter("@SA_ZIPCODE",objAddPartyDetailsInfo.SA_ZIPCODE);
				objDataWrapper.AddParameter("@SA_PHONE",objAddPartyDetailsInfo.SA_PHONE);
				objDataWrapper.AddParameter("@SA_FAX",objAddPartyDetailsInfo.SA_FAX);
				objDataWrapper.AddParameter("@PROP_DAMAGED_ID",objAddPartyDetailsInfo.PROP_DAMAGED_ID);
				
				//Added by Mohit Agarwal 20-Aug-2008
				objDataWrapper.AddParameter("@MAIL_1099_ADD1",objAddPartyDetailsInfo.MAIL_1099_ADD1);
				objDataWrapper.AddParameter("@MAIL_1099_ADD2",objAddPartyDetailsInfo.MAIL_1099_ADD2);
				objDataWrapper.AddParameter("@MAIL_1099_CITY",objAddPartyDetailsInfo.MAIL_1099_CITY);
				objDataWrapper.AddParameter("@MAIL_1099_STATE",objAddPartyDetailsInfo.MAIL_1099_STATE);
				objDataWrapper.AddParameter("@MAIL_1099_ZIP",objAddPartyDetailsInfo.MAIL_1099_ZIP);
				objDataWrapper.AddParameter("@MAIL_1099_COUNTRY",objAddPartyDetailsInfo.MAIL_1099_COUNTRY);

				objDataWrapper.AddParameter("@MAIL_1099_NAME",objAddPartyDetailsInfo.MAIL_1099_NAME);
                objDataWrapper.AddParameter("@ACCOUNT_TYPE", objAddPartyDetailsInfo.ACCOUNT_TYPE);

				objDataWrapper.AddParameter("@W9_FORM",objAddPartyDetailsInfo.W9_FORM);
                objDataWrapper.AddParameter("@DISTRICT", objAddPartyDetailsInfo.DISTRICT);
                objDataWrapper.AddParameter("@REGIONAL_ID", objAddPartyDetailsInfo.REGIONAL_ID);
                objDataWrapper.AddParameter("@REGIONAL_ID_ISSUANCE", objAddPartyDetailsInfo.REGIONAL_ID_ISSUANCE);
                if (objAddPartyDetailsInfo.REGIONAL_ID_ISSUE_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@REGIONAL_ID_ISSUE_DATE", objAddPartyDetailsInfo.REGIONAL_ID_ISSUE_DATE);
                else
                    objDataWrapper.AddParameter("@REGIONAL_ID_ISSUE_DATE", DBNull.Value);

                objDataWrapper.AddParameter("@BANK_NUMBER", objAddPartyDetailsInfo.BANK_NUMBER);
                objDataWrapper.AddParameter("@BANK_BRANCH", objAddPartyDetailsInfo.BANK_BRANCH);
                objDataWrapper.AddParameter("@MARITAL_STATUS", objAddPartyDetailsInfo.MARITAL_STATUS);
                objDataWrapper.AddParameter("@GENDER", objAddPartyDetailsInfo.GENDER);
                objDataWrapper.AddParameter("@PARTY_TYPE", objAddPartyDetailsInfo.PARTY_TYPE);
                objDataWrapper.AddParameter("@PAYMENT_METHOD", objAddPartyDetailsInfo.PAYMENT_METHOD);
                objDataWrapper.AddParameter("@PARTY_CPF_CNPJ", objAddPartyDetailsInfo.PARTY_CPF_CNPJ);
                objDataWrapper.AddParameter("@IS_BENEFICIARY", objAddPartyDetailsInfo.IS_BENEFICIARY);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@PARTY_ID",objAddPartyDetailsInfo.PARTY_ID,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objReturnSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue);

				
				if(TransactionLogRequired)
				{
					objAddPartyDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddPartyDetails.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objAddPartyDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//Done for Itrack Issue 6932 on 19 Jan 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objAddPartyDetailsInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1836", "");// "Party Detail Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
                    objTransactionInfo.CUSTOM_INFO =Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1794","") + claimNumber; //"Claim Number : " + claimNumber;//Done for Itrack Issue 6932 on 19 Jan 2010
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int PARTY_ID = int.Parse(objSqlParameter.Value.ToString());
				if(objReturnSqlParameter!=null && objReturnSqlParameter.Value.ToString()!="")
					returnResult = int.Parse(objReturnSqlParameter.Value.ToString());

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (returnResult>0 )				
					objAddPartyDetailsInfo.PARTY_ID = PARTY_ID;
				return returnResult;
				
				//				if (PARTY_ID == -1)
				//				{
				//					return -1;
				//				}
				//				else
				//				{
				//					objAddPartyDetailsInfo.PARTY_ID = PARTY_ID;
				//					return returnResult;
				//				}
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


		#region Add Update Expert Service Data
		private int AddUpdateExpertData(DataWrapper objDataWrapper,ClsAddPartyDetailsInfo objPartyDetailsInfo, string ExpertServiceType, string OldExpertServiceId, out string NewExpertServiceId)
		{
			NewExpertServiceId="0";
			int intRetVal=0;
			Cms.Model.Maintenance.Claims.ClsExpertServiceProvidersInfo objExpertInfo = new Cms.Model.Maintenance.Claims.ClsExpertServiceProvidersInfo();
			/*if(ExpertServiceType!="" && ExpertServiceType.Split('~').Length>0)
			{
				objExpertInfo.EXPERT_SERVICE_TYPE = int.Parse(ExpertServiceType.Split('~')[0].ToString());
				objExpertInfo.EXPERT_SERVICE_TYPE_DESC = ExpertServiceType.Split('~')[1].ToString();
			}	
			else
				objExpertInfo.EXPERT_SERVICE_TYPE = 191 ;//Default value for Other option when nothing is selected
			*/
			objExpertInfo.EXPERT_SERVICE_TYPE = objPartyDetailsInfo.EXPERT_SERVICE_TYPE;
			objExpertInfo.EXPERT_SERVICE_TYPE_DESC = objPartyDetailsInfo.EXPERT_SERVICE_TYPE_DESC;
			
			objExpertInfo.EXPERT_SERVICE_NAME = objPartyDetailsInfo.NAME.Trim();
			objExpertInfo.EXPERT_SERVICE_ADDRESS1 = objPartyDetailsInfo.ADDRESS1.Trim();
			objExpertInfo.EXPERT_SERVICE_ADDRESS2 = objPartyDetailsInfo.ADDRESS2.Trim();
			objExpertInfo.EXPERT_SERVICE_CITY = objPartyDetailsInfo.CITY.Trim();
			objExpertInfo.EXPERT_SERVICE_COUNTRY = objPartyDetailsInfo.COUNTRY.ToString();
			objExpertInfo.EXPERT_SERVICE_STATE = objPartyDetailsInfo.STATE;
			objExpertInfo.EXPERT_SERVICE_ZIP = objPartyDetailsInfo.ZIP.Trim();
			objExpertInfo.EXPERT_SERVICE_MASTER_VENDOR_CODE = objPartyDetailsInfo.MASTER_VENDOR_CODE.Trim();
			objExpertInfo.EXPERT_SERVICE_VENDOR_CODE = objPartyDetailsInfo.VENDOR_CODE.Trim();
			objExpertInfo.EXPERT_SERVICE_CONTACT_NAME = objPartyDetailsInfo.CONTACT_NAME.Trim();
			objExpertInfo.EXPERT_SERVICE_CONTACT_FAX = objPartyDetailsInfo.CONTACT_FAX.Trim();
			objExpertInfo.EXPERT_SERVICE_CONTACT_PHONE = objPartyDetailsInfo.CONTACT_PHONE.Trim();
			objExpertInfo.EXPERT_SERVICE_CONTACT_PHONE_EXT = objPartyDetailsInfo.CONTACT_PHONE_EXT.Trim();
			objExpertInfo.EXPERT_SERVICE_CONTACT_EMAIL = objPartyDetailsInfo.CONTACT_EMAIL.Trim();
			objExpertInfo.EXPERT_SERVICE_FEDRERAL_ID = objPartyDetailsInfo.FEDRERAL_ID.Trim();
			objExpertInfo.EXPERT_SERVICE_1099_PROCESSING_OPTION = objPartyDetailsInfo.PROCESSING_OPTION_1099;
			objExpertInfo.PARTY_DETAIL = objPartyDetailsInfo.PARTY_DETAIL;
			objExpertInfo.EXTENT_OF_INJURY = objPartyDetailsInfo.EXTENT_OF_INJURY.Trim();
			//objExpertInfo.OTHER_DETAILS = objPartyDetailsInfo.OTHER_DETAILS.Trim();//Commented by Sibin on 28 April 2009 for Itrack Issue 4964
			objExpertInfo.BANK_NAME = objPartyDetailsInfo.BANK_NAME.Trim();
            // Added by Santosh Gautam on 15 Nov 2010
            objExpertInfo.AGENCY_BANK = objPartyDetailsInfo.AGENCY_BANK.Trim();
			objExpertInfo.ACCOUNT_NAME = objPartyDetailsInfo.ACCOUNT_NAME.Trim();
			objExpertInfo.ACCOUNT_NUMBER = objPartyDetailsInfo.ACCOUNT_NUMBER.Trim();
			objExpertInfo.PARENT_ADJUSTER = objPartyDetailsInfo.PARENT_ADJUSTER;
			objExpertInfo.AGE = objPartyDetailsInfo.AGE;
			
			//Added by Mohit Agarwal 20-Aug-2008
			objExpertInfo.MAIL_1099_ADD1 = objPartyDetailsInfo.MAIL_1099_ADD1;
			objExpertInfo.MAIL_1099_ADD2 = objPartyDetailsInfo.MAIL_1099_ADD2;
			objExpertInfo.MAIL_1099_CITY = objPartyDetailsInfo.MAIL_1099_CITY;
			objExpertInfo.MAIL_1099_STATE = objPartyDetailsInfo.MAIL_1099_STATE;
			objExpertInfo.MAIL_1099_ZIP = objPartyDetailsInfo.MAIL_1099_ZIP;
			objExpertInfo.MAIL_1099_COUNTRY = objPartyDetailsInfo.MAIL_1099_COUNTRY;

			objExpertInfo.MAIL_1099_NAME = objPartyDetailsInfo.MAIL_1099_NAME;
			objExpertInfo.W9_FORM = objPartyDetailsInfo.W9_FORM;

			objExpertInfo.MODIFIED_BY  = objExpertInfo.CREATED_BY = objPartyDetailsInfo.CREATED_BY;
			objExpertInfo.LAST_UPDATED_DATETIME = objExpertInfo.CREATED_DATETIME = objPartyDetailsInfo.CREATED_DATETIME;			
			objExpertInfo.IS_ACTIVE="Y"; 
			
			Cms.BusinessLayer.BLClaims.ClsExpertServiceProviders objExpert = new ClsExpertServiceProviders();
			if(OldExpertServiceId.ToUpper()=="NEW")
			{	
				intRetVal = objExpert.Add(objExpertInfo,objDataWrapper);
				if(intRetVal>0)
					NewExpertServiceId = objExpertInfo.EXPERT_SERVICE_ID.ToString();
			}
			else
			{
				objExpertInfo.EXPERT_SERVICE_ID = int.Parse(OldExpertServiceId);
				intRetVal = objExpert.Update(objExpertInfo,objExpertInfo,objDataWrapper);
			}
			return intRetVal;
		}
		#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldAddPartyDetailsInfo">Model object having old information</param>
		/// <param name="objAddPartyDetailsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsAddPartyDetailsInfo objOldAddPartyDetailsInfo,ClsAddPartyDetailsInfo objAddPartyDetailsInfo, string ExpertServiceType, string OldExpertServiceId, out string NewExpertServiceId)
		{
			string		strStoredProc	=	"Proc_UpdateCLM_PARTIES";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string ExistsExpertServiceId = "-1";
			try
			{
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objAddPartyDetailsInfo.CLAIM_ID);

				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@PARTY_ID",objAddPartyDetailsInfo.PARTY_ID);
				//objDataWrapper.AddParameter("@CLAIM_ID",objAddPartyDetailsInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@VENDOR_CODE",objAddPartyDetailsInfo.VENDOR_CODE);
				DataSet dsExp = objDataWrapper.ExecuteDataSet("Proc_GetExpertServiceId4ClaimParty");
				if(dsExp.Tables.Count > 0 && dsExp.Tables[0].Rows.Count > 0)
				{
					ExistsExpertServiceId = dsExp.Tables[0].Rows[0]["EXPERT_SERVICE_ID"].ToString();
				}
				objDataWrapper.ClearParameteres();

				NewExpertServiceId = "0";
				if(objAddPartyDetailsInfo.PARTY_TYPE_ID==int.Parse(((int)ClsClaims.enumClaimPartyTypes.EXPERTSERVICEPROVIDERS).ToString()))
				{
					if(OldExpertServiceId == "NEW")
					{
						if(ExistsExpertServiceId != "-1")
						{
							OldExpertServiceId = ExistsExpertServiceId;
						}
					}

					returnResult =  AddUpdateExpertData(objDataWrapper,objAddPartyDetailsInfo,ExpertServiceType,OldExpertServiceId,out NewExpertServiceId);
					if(returnResult<0)
						return returnResult;
					objDataWrapper.ClearParameteres();
				}
				else
				{
					if(ExistsExpertServiceId != "-1")
					{
						objAddPartyDetailsInfo.VENDOR_CODE = "";
					}
				}

				returnResult = 0;

				objDataWrapper.AddParameter("@PARTY_ID",objAddPartyDetailsInfo.PARTY_ID);
				objDataWrapper.AddParameter("@CLAIM_ID",objAddPartyDetailsInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@NAME",objAddPartyDetailsInfo.NAME);
				objDataWrapper.AddParameter("@ADDRESS1",objAddPartyDetailsInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objAddPartyDetailsInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objAddPartyDetailsInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objAddPartyDetailsInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objAddPartyDetailsInfo.ZIP);
				objDataWrapper.AddParameter("@CONTACT_PHONE",objAddPartyDetailsInfo.CONTACT_PHONE);
				objDataWrapper.AddParameter("@CONTACT_EMAIL",objAddPartyDetailsInfo.CONTACT_EMAIL);
				objDataWrapper.AddParameter("@OTHER_DETAILS",objAddPartyDetailsInfo.OTHER_DETAILS);
				objDataWrapper.AddParameter("@MODIFIED_BY",objAddPartyDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objAddPartyDetailsInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@PARTY_TYPE_ID",objAddPartyDetailsInfo.PARTY_TYPE_ID);
				objDataWrapper.AddParameter("@COUNTRY",objAddPartyDetailsInfo.COUNTRY);
				if(objAddPartyDetailsInfo.AGE==0 || objAddPartyDetailsInfo.AGE==-1)
					objDataWrapper.AddParameter("@AGE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@AGE",objAddPartyDetailsInfo.AGE);
				objDataWrapper.AddParameter("@EXTENT_OF_INJURY",objAddPartyDetailsInfo.EXTENT_OF_INJURY);
				objDataWrapper.AddParameter("@REFERENCE",objAddPartyDetailsInfo.REFERENCE);
				objDataWrapper.AddParameter("@PARTY_DETAIL",objAddPartyDetailsInfo.PARTY_DETAIL);
				objDataWrapper.AddParameter("@BANK_NAME",objAddPartyDetailsInfo.BANK_NAME);
                // Added by Santosh Gautam on 15 Nov 2010
                objDataWrapper.AddParameter("@AGENCY_BANK", objAddPartyDetailsInfo.AGENCY_BANK);
				objDataWrapper.AddParameter("@ACCOUNT_NUMBER",objAddPartyDetailsInfo.ACCOUNT_NUMBER);
				objDataWrapper.AddParameter("@ACCOUNT_NAME",objAddPartyDetailsInfo.ACCOUNT_NAME);
				objDataWrapper.AddParameter("@CONTACT_PHONE_EXT",objAddPartyDetailsInfo.CONTACT_PHONE_EXT);
				objDataWrapper.AddParameter("@CONTACT_FAX",objAddPartyDetailsInfo.CONTACT_FAX);
				objDataWrapper.AddParameter("@PARTY_TYPE_DESC",objAddPartyDetailsInfo.PARTY_TYPE_DESC);
				objDataWrapper.AddParameter("@PARENT_ADJUSTER",objAddPartyDetailsInfo.PARENT_ADJUSTER);
				objDataWrapper.AddParameter("@FEDRERAL_ID",objAddPartyDetailsInfo.FEDRERAL_ID);
				objDataWrapper.AddParameter("@PROCESSING_OPTION_1099",objAddPartyDetailsInfo.PROCESSING_OPTION_1099);
				objDataWrapper.AddParameter("@MASTER_VENDOR_CODE",objAddPartyDetailsInfo.MASTER_VENDOR_CODE);
				objDataWrapper.AddParameter("@VENDOR_CODE",objAddPartyDetailsInfo.VENDOR_CODE);
				objDataWrapper.AddParameter("@CONTACT_NAME",objAddPartyDetailsInfo.CONTACT_NAME);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE",objAddPartyDetailsInfo.EXPERT_SERVICE_TYPE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE_DESC",objAddPartyDetailsInfo.EXPERT_SERVICE_TYPE_DESC);
				objDataWrapper.AddParameter("@SUB_ADJUSTER",objAddPartyDetailsInfo.SUB_ADJUSTER);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_CONTACT_NAME",objAddPartyDetailsInfo.SUB_ADJUSTER_CONTACT_NAME);
				objDataWrapper.AddParameter("@SA_ADDRESS1",objAddPartyDetailsInfo.SA_ADDRESS1);
				objDataWrapper.AddParameter("@SA_ADDRESS2",objAddPartyDetailsInfo.SA_ADDRESS2);
				objDataWrapper.AddParameter("@SA_CITY",objAddPartyDetailsInfo.SA_CITY);
				objDataWrapper.AddParameter("@SA_COUNTRY",objAddPartyDetailsInfo.SA_COUNTRY);
				objDataWrapper.AddParameter("@SA_STATE",objAddPartyDetailsInfo.SA_STATE);
				objDataWrapper.AddParameter("@SA_ZIPCODE",objAddPartyDetailsInfo.SA_ZIPCODE);
				objDataWrapper.AddParameter("@SA_PHONE",objAddPartyDetailsInfo.SA_PHONE);
				objDataWrapper.AddParameter("@SA_FAX",objAddPartyDetailsInfo.SA_FAX);

				//Added by Mohit Agarwal 20-Aug-2008
				objDataWrapper.AddParameter("@MAIL_1099_ADD1",objAddPartyDetailsInfo.MAIL_1099_ADD1);
				objDataWrapper.AddParameter("@MAIL_1099_ADD2",objAddPartyDetailsInfo.MAIL_1099_ADD2);
				objDataWrapper.AddParameter("@MAIL_1099_CITY",objAddPartyDetailsInfo.MAIL_1099_CITY);
				objDataWrapper.AddParameter("@MAIL_1099_STATE",objAddPartyDetailsInfo.MAIL_1099_STATE);
				objDataWrapper.AddParameter("@MAIL_1099_ZIP",objAddPartyDetailsInfo.MAIL_1099_ZIP);
				objDataWrapper.AddParameter("@MAIL_1099_COUNTRY",objAddPartyDetailsInfo.MAIL_1099_COUNTRY);

				objDataWrapper.AddParameter("@MAIL_1099_NAME",objAddPartyDetailsInfo.MAIL_1099_NAME);
				objDataWrapper.AddParameter("@W9_FORM",objAddPartyDetailsInfo.W9_FORM);
                objDataWrapper.AddParameter("@ACCOUNT_TYPE", objAddPartyDetailsInfo.ACCOUNT_TYPE);
                objDataWrapper.AddParameter("@DISTRICT", objAddPartyDetailsInfo.DISTRICT);
                objDataWrapper.AddParameter("@REGIONAL_ID", objAddPartyDetailsInfo.REGIONAL_ID);
                objDataWrapper.AddParameter("@REGIONAL_ID_ISSUANCE", objAddPartyDetailsInfo.REGIONAL_ID_ISSUANCE);
                if (objAddPartyDetailsInfo.REGIONAL_ID_ISSUE_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@REGIONAL_ID_ISSUE_DATE", objAddPartyDetailsInfo.REGIONAL_ID_ISSUE_DATE);
                else
                    objDataWrapper.AddParameter("@REGIONAL_ID_ISSUE_DATE", DBNull.Value);

                objDataWrapper.AddParameter("@BANK_NUMBER", objAddPartyDetailsInfo.BANK_NUMBER);
                objDataWrapper.AddParameter("@BANK_BRANCH", objAddPartyDetailsInfo.BANK_BRANCH);
                objDataWrapper.AddParameter("@MARITAL_STATUS", objAddPartyDetailsInfo.MARITAL_STATUS);
                objDataWrapper.AddParameter("@GENDER", objAddPartyDetailsInfo.GENDER);
                objDataWrapper.AddParameter("@PARTY_TYPE", objAddPartyDetailsInfo.PARTY_TYPE);
                objDataWrapper.AddParameter("@PAYMENT_METHOD", objAddPartyDetailsInfo.PAYMENT_METHOD);
                objDataWrapper.AddParameter("@PARTY_CPF_CNPJ", objAddPartyDetailsInfo.PARTY_CPF_CNPJ);
                objDataWrapper.AddParameter("@IS_BENEFICIARY", objAddPartyDetailsInfo.IS_BENEFICIARY);
				SqlParameter objReturnSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue);
				if(base.TransactionLogRequired) 
				{
					objAddPartyDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddPartyDetails.aspx.resx");
					objBuilder.GetUpdateSQL(objOldAddPartyDetailsInfo,objAddPartyDetailsInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 19 Jan 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objAddPartyDetailsInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1837", "");// "Party Detail Has Been Updated";
					if(strTranXML!="<LabelFieldMapping><Map label=\"Business Owners Name\" field=\"MAIL_1099_NAME\" /></LabelFieldMapping>")
						objTransactionInfo.CHANGE_XML		=	strTranXML;
                    objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1794","") + claimNumber; //"Claim Number : " + claimNumber;//Done for Itrack Issue 6932 on 19 Jan 2010
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if(objReturnSqlParameter!=null && objReturnSqlParameter.Value.ToString()!="")
					returnResult = int.Parse(objReturnSqlParameter.Value.ToString());
			
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

		#region Update Parties from Property damage : Added Sep 28 2007
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objOldAddPartyDetailsInfo"></param>
		/// <param name="objAddPartyDetailsInfo"></param>
		/// <param name="ExpertServiceType"></param>
		/// <param name="OldExpertServiceId"></param>
		/// <param name="NewExpertServiceId"></param>
		/// <returns></returns>
		public int UpdatePartiesPropDamage(ClsAddPartyDetailsInfo objOldAddPartyDetailsInfo,ClsAddPartyDetailsInfo objAddPartyDetailsInfo, string ExpertServiceType, string OldExpertServiceId, out string NewExpertServiceId)
		{
			string		strStoredProc	=	"Proc_UpdateCLM_PARTIES_PROP_DAMAGE";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string ExistsExpertServiceId = "-1";
			try
			{
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objAddPartyDetailsInfo.CLAIM_ID);

				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.ClearParameteres();//Added for Itrack Issue 7768 on 9 Aug 2010
				objDataWrapper.AddParameter("@PARTY_ID",objAddPartyDetailsInfo.PARTY_ID);
				objDataWrapper.AddParameter("@CLAIM_ID",objAddPartyDetailsInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@VENDOR_CODE",objAddPartyDetailsInfo.VENDOR_CODE);
				DataSet dsExp = objDataWrapper.ExecuteDataSet("Proc_GetExpertServiceId4ClaimParty");
				if(dsExp.Tables.Count > 0 && dsExp.Tables[0].Rows.Count > 0)
				{
					ExistsExpertServiceId = dsExp.Tables[0].Rows[0]["EXPERT_SERVICE_ID"].ToString();
				}
				objDataWrapper.ClearParameteres();

				NewExpertServiceId = "0";
				if(objAddPartyDetailsInfo.PARTY_TYPE_ID==int.Parse(((int)ClsClaims.enumClaimPartyTypes.EXPERTSERVICEPROVIDERS).ToString()))
				{
					if(OldExpertServiceId == "NEW")
					{
						if(ExistsExpertServiceId != "-1")
						{
							OldExpertServiceId = ExistsExpertServiceId;
						}
					}

					returnResult =  AddUpdateExpertData(objDataWrapper,objAddPartyDetailsInfo,ExpertServiceType,OldExpertServiceId,out NewExpertServiceId);
					if(returnResult<0)
						return returnResult;
					objDataWrapper.ClearParameteres();
				}
				else
				{
					if(ExistsExpertServiceId != "-1")
					{
						objAddPartyDetailsInfo.VENDOR_CODE = "";
					}
				}

				returnResult = 0;

				objDataWrapper.AddParameter("@PARTY_ID",objAddPartyDetailsInfo.PARTY_ID);
				objDataWrapper.AddParameter("@CLAIM_ID",objAddPartyDetailsInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@NAME",objAddPartyDetailsInfo.NAME);
				objDataWrapper.AddParameter("@ADDRESS1",objAddPartyDetailsInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objAddPartyDetailsInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objAddPartyDetailsInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objAddPartyDetailsInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objAddPartyDetailsInfo.ZIP);
				objDataWrapper.AddParameter("@CONTACT_PHONE",objAddPartyDetailsInfo.CONTACT_PHONE);
				objDataWrapper.AddParameter("@CONTACT_EMAIL",objAddPartyDetailsInfo.CONTACT_EMAIL);
				objDataWrapper.AddParameter("@OTHER_DETAILS",objAddPartyDetailsInfo.OTHER_DETAILS);
				objDataWrapper.AddParameter("@MODIFIED_BY",objAddPartyDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objAddPartyDetailsInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@PARTY_TYPE_ID",objAddPartyDetailsInfo.PARTY_TYPE_ID);
				objDataWrapper.AddParameter("@COUNTRY",objAddPartyDetailsInfo.COUNTRY);
				if(objAddPartyDetailsInfo.AGE==0 || objAddPartyDetailsInfo.AGE==-1)
					objDataWrapper.AddParameter("@AGE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@AGE",objAddPartyDetailsInfo.AGE);
				objDataWrapper.AddParameter("@EXTENT_OF_INJURY",objAddPartyDetailsInfo.EXTENT_OF_INJURY);
				objDataWrapper.AddParameter("@REFERENCE",objAddPartyDetailsInfo.REFERENCE);
				objDataWrapper.AddParameter("@PARTY_DETAIL",objAddPartyDetailsInfo.PARTY_DETAIL);
				objDataWrapper.AddParameter("@BANK_NAME",objAddPartyDetailsInfo.BANK_NAME);
                // Added by Santosh Gautam on 15 Nov 2010
                objDataWrapper.AddParameter("@AGENCY_BANK", objAddPartyDetailsInfo.AGENCY_BANK);
				objDataWrapper.AddParameter("@ACCOUNT_NUMBER",objAddPartyDetailsInfo.ACCOUNT_NUMBER);
				objDataWrapper.AddParameter("@ACCOUNT_NAME",objAddPartyDetailsInfo.ACCOUNT_NAME);
				objDataWrapper.AddParameter("@CONTACT_PHONE_EXT",objAddPartyDetailsInfo.CONTACT_PHONE_EXT);
				objDataWrapper.AddParameter("@CONTACT_FAX",objAddPartyDetailsInfo.CONTACT_FAX);
				objDataWrapper.AddParameter("@PARTY_TYPE_DESC",objAddPartyDetailsInfo.PARTY_TYPE_DESC);
				objDataWrapper.AddParameter("@PARENT_ADJUSTER",objAddPartyDetailsInfo.PARENT_ADJUSTER);
				objDataWrapper.AddParameter("@FEDRERAL_ID",objAddPartyDetailsInfo.FEDRERAL_ID);
				objDataWrapper.AddParameter("@PROCESSING_OPTION_1099",objAddPartyDetailsInfo.PROCESSING_OPTION_1099);
				objDataWrapper.AddParameter("@MASTER_VENDOR_CODE",objAddPartyDetailsInfo.MASTER_VENDOR_CODE);
				objDataWrapper.AddParameter("@VENDOR_CODE",objAddPartyDetailsInfo.VENDOR_CODE);
				objDataWrapper.AddParameter("@CONTACT_NAME",objAddPartyDetailsInfo.CONTACT_NAME);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE",objAddPartyDetailsInfo.EXPERT_SERVICE_TYPE);
				objDataWrapper.AddParameter("@EXPERT_SERVICE_TYPE_DESC",objAddPartyDetailsInfo.EXPERT_SERVICE_TYPE_DESC);
				objDataWrapper.AddParameter("@SUB_ADJUSTER",objAddPartyDetailsInfo.SUB_ADJUSTER);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_CONTACT_NAME",objAddPartyDetailsInfo.SUB_ADJUSTER_CONTACT_NAME);
				objDataWrapper.AddParameter("@SA_ADDRESS1",objAddPartyDetailsInfo.SA_ADDRESS1);
				objDataWrapper.AddParameter("@SA_ADDRESS2",objAddPartyDetailsInfo.SA_ADDRESS2);
				objDataWrapper.AddParameter("@SA_CITY",objAddPartyDetailsInfo.SA_CITY);
				objDataWrapper.AddParameter("@SA_COUNTRY",objAddPartyDetailsInfo.SA_COUNTRY);
				objDataWrapper.AddParameter("@SA_STATE",objAddPartyDetailsInfo.SA_STATE);
				objDataWrapper.AddParameter("@SA_ZIPCODE",objAddPartyDetailsInfo.SA_ZIPCODE);
				objDataWrapper.AddParameter("@SA_PHONE",objAddPartyDetailsInfo.SA_PHONE);
				objDataWrapper.AddParameter("@SA_FAX",objAddPartyDetailsInfo.SA_FAX);
				objDataWrapper.AddParameter("@PROP_DAMAGED_ID",objAddPartyDetailsInfo.PROP_DAMAGED_ID);

				//Added by Mohit Agarwal 20-Aug-2008
				objDataWrapper.AddParameter("@MAIL_1099_ADD1",objAddPartyDetailsInfo.MAIL_1099_ADD1);
				objDataWrapper.AddParameter("@MAIL_1099_ADD2",objAddPartyDetailsInfo.MAIL_1099_ADD2);
				objDataWrapper.AddParameter("@MAIL_1099_CITY",objAddPartyDetailsInfo.MAIL_1099_CITY);
				objDataWrapper.AddParameter("@MAIL_1099_STATE",objAddPartyDetailsInfo.MAIL_1099_STATE);
				objDataWrapper.AddParameter("@MAIL_1099_ZIP",objAddPartyDetailsInfo.MAIL_1099_ZIP);
				objDataWrapper.AddParameter("@MAIL_1099_COUNTRY",objAddPartyDetailsInfo.MAIL_1099_COUNTRY);
				
				objDataWrapper.AddParameter("@MAIL_1099_NAME",objAddPartyDetailsInfo.MAIL_1099_NAME);
				objDataWrapper.AddParameter("@W9_FORM",objAddPartyDetailsInfo.W9_FORM);
                objDataWrapper.AddParameter("@ACCOUNT_TYPE", objAddPartyDetailsInfo.ACCOUNT_TYPE);
                objDataWrapper.AddParameter("@DISTRICT", objAddPartyDetailsInfo.DISTRICT);
                objDataWrapper.AddParameter("@REGIONAL_ID", objAddPartyDetailsInfo.REGIONAL_ID);
                objDataWrapper.AddParameter("@REGIONAL_ID_ISSUANCE", objAddPartyDetailsInfo.REGIONAL_ID_ISSUANCE);
                if (objAddPartyDetailsInfo.REGIONAL_ID_ISSUE_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@REGIONAL_ID_ISSUE_DATE", objAddPartyDetailsInfo.REGIONAL_ID_ISSUE_DATE);
                else
                    objDataWrapper.AddParameter("@REGIONAL_ID_ISSUE_DATE", DBNull.Value);

                objDataWrapper.AddParameter("@BANK_NUMBER", objAddPartyDetailsInfo.BANK_NUMBER);
                objDataWrapper.AddParameter("@BANK_BRANCH", objAddPartyDetailsInfo.BANK_BRANCH);
                objDataWrapper.AddParameter("@MARITAL_STATUS", objAddPartyDetailsInfo.MARITAL_STATUS);
                objDataWrapper.AddParameter("@GENDER", objAddPartyDetailsInfo.GENDER);
                objDataWrapper.AddParameter("@PARTY_TYPE", objAddPartyDetailsInfo.PARTY_TYPE);
                objDataWrapper.AddParameter("@PAYMENT_METHOD", objAddPartyDetailsInfo.PAYMENT_METHOD);
                objDataWrapper.AddParameter("@PARTY_CPF_CNPJ", objAddPartyDetailsInfo.PARTY_CPF_CNPJ);
                objDataWrapper.AddParameter("@IS_BENEFICIARY", objAddPartyDetailsInfo.IS_BENEFICIARY);

				SqlParameter objReturnSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue);
				if(base.TransactionLogRequired) 
				{
					objAddPartyDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddPartyDetails.aspx.resx");
					objBuilder.GetUpdateSQL(objOldAddPartyDetailsInfo,objAddPartyDetailsInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 19 Jan 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objAddPartyDetailsInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1837", "");// "Party Detail Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
                    objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1794","") + claimNumber; //"Claim Number : " + claimNumber;//Done for Itrack Issue 6932 on 19 Jan 2010
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if(objReturnSqlParameter!=null && objReturnSqlParameter.Value.ToString()!="")
					returnResult = int.Parse(objReturnSqlParameter.Value.ToString());
			
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

		#region "GetxmlMethods"
        public static string GetXmlForPageControls(string PARTY_ID, string CLAIM_ID, int LANG_ID)
		{
            DataSet objDataSet = GetValuesForParty(CLAIM_ID, PARTY_ID, LANG_ID);
			return objDataSet.GetXml();
		}

		public static DataSet GetValuesForParty(string CLAIM_ID,string PARTY_ID,int LANG_ID)
		{
			string strSql = "Proc_GetCLM_PARTIES";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@PARTY_ID",PARTY_ID);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
            objDataWrapper.AddParameter("@LANG_ID", LANG_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		public static DataSet GetValuesForPartyTypes(string CLAIM_ID,string PARTY_TYPE_ID)
		{
			string strSql = "Proc_GetCLM_PARTIES_BY_PARTY_TYPE";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@PARTY_TYPE_ID",PARTY_TYPE_ID);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		#endregion

		//added sep 27 2007
		#region "GetxmlMethods for Parties in PD"
		public static string GetXmlForPageControlsPD(string PROP_DAMAGED_ID,string CLAIM_ID)
		{
			DataSet objDataSet = GetValuesForPartyPD(CLAIM_ID,PROP_DAMAGED_ID);
			return objDataSet.GetXml();
		}

		public static DataSet GetValuesForPartyPD(string CLAIM_ID,string PROP_DAMAGED_ID)
		{
			string strSql = "Proc_GetCLM_PARTIES_FOR_PROP_DAMAGE";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@PROP_DAMAGED_ID",PROP_DAMAGED_ID);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		#endregion


		/// <summary>
		/// Function to get all the Party types from the database
		/// </summary>
		/// <author>Amar</author>
		/// <returns>DataTable</returns>
        public static DataTable GetPartyTypes(int LangID)
		{
			string strSql = "PROC_ExecuteGridQuery";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@Type","PartyType");
            objDataWrapper.AddParameter("@LANG_ID", LangID);
			return objDataWrapper.ExecuteDataSet(strSql).Tables[0];
		}

		/// <summary>
		/// Function to fetch Customer Attached with the Policy and 
		/// Claimant name entered in the Claim Notification Screen
		/// </summary>
		/// <param name="CLAIM_ID">Claim ID</param>
		/// <returns></returns>
		public static string[] GetDefaultCustomerClaimantNames(int CLAIM_ID)
		{
			string[] arr = new string[]{"","","","","","","","","",""};
			string strSql = "Proc_GetClaimantInsuredName";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			DataSet oDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if(oDataSet.Tables[0].Rows.Count>0)
			{
				arr[0] = oDataSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString();
				arr[1] = oDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
				arr[2] = oDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
				arr[3] = oDataSet.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString();
				arr[4] = oDataSet.Tables[0].Rows[0]["CUSTOMER_STATE"].ToString();
				arr[5] = oDataSet.Tables[0].Rows[0]["CUSTOMER_COUNTRY"].ToString();
				arr[6] = oDataSet.Tables[0].Rows[0]["ZIPCODE"].ToString();
				arr[7] = oDataSet.Tables[0].Rows[0]["CUSTOMER_PHONE"].ToString();
				arr[8] = oDataSet.Tables[0].Rows[0]["CUSTOMER_EMAIL"].ToString();
				arr[9] = oDataSet.Tables[0].Rows[0]["CLAIMANT_NAME"].ToString();
			}
			
			return arr;
		}

		#region Activate Deactivate method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldAddPartyDetailsInfo">Model object having old information</param>
		/// <param name="objAddPartyDetailsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int ActivateDeactivate(ClsAddPartyDetailsInfo objAddPartyDetailsInfo,string strStatus)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateCLM_PARTIES";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objAddPartyDetailsInfo.CLAIM_ID);
				//Done for Itrack Issue 6932 on 1 Feb 2010
				DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
	
				objDataWrapper.AddParameter("@PARTY_ID",objAddPartyDetailsInfo.PARTY_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);				
				
				if(base.TransactionLogRequired) 
				{
					objAddPartyDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddPartyDetails.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objAddPartyDetailsInfo);
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 1 Feb 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objAddPartyDetailsInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="N")
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1838","");//"Party Detail Has Been Deactivated.";
					else
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1839", "");// "Party Detail Has Been Activated.";	
					objTransactionInfo.CHANGE_XML		=	strTranXML;
                    objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1794","") + claimNumber; //"Claim Number : " + claimNumber;//Done for Itrack Issue 6932 on 1 Feb 2010

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
	}
}
