/******************************************************************************************
<Author				: -	Sumit Chhabra
<Start Date			: -	April 26,2006
<End Date			: -	
<Description		: -	Class file for Dummy Policy
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 

*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
//using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon ;
using Cms.Model.Claims;
using Cms.DataLayer;
using System.Globalization;
namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// Summary description for ClsDummyPolicy.
	/// </summary>
	public class ClsDummyPolicy : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private			bool		boolTransactionRequired;
		public ClsDummyPolicy()
		{
			boolTransactionRequired = base.TransactionLogRequired;
		}

		#region Public Properties
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

		#endregion

		#region Update
		public int Update(int ClaimId, int DummyPolicyId)
		{
			string		strStoredProc	=	"Proc_UpdateCLM_DUMMY_POLICY";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			//int intDUMMY_POLICY_ID;

			try
			{			
				objDataWrapper.AddParameter("@CLAIM_ID",ClaimId);		
				objDataWrapper.AddParameter("@DUMMY_POLICY_ID",DummyPolicyId);		

				int returnResult = 0;				
//				if(TransactionLogRequired)
//				{
//					objDummyPolicyInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"\Claims\Aspx\Policy\DummyPolicyPopUp.aspx.resx");
//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();					
//					string strTranXML = objBuilder.GetTransactionLogXML(objDummyPolicyInfo);
//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//					objTransactionInfo.TRANS_TYPE_ID	=	1;
//					objTransactionInfo.RECORDED_BY		=	objDummyPolicyInfo.CREATED_BY;					
//					objTransactionInfo.TRANS_DESC		=	"New DummyPolicy is added";
//					objTransactionInfo.CHANGE_XML		=	strTranXML;					
//					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);					
//				}
//				else
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return 1;
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

		public int Add(ClsDummyPolicyInfo objDummyPolicyInfo)
		{
			string		strStoredProc	=	"Proc_InsertCLM_DUMMY_POLICY";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			int intDUMMY_POLICY_ID;

			try
			{			
				objDataWrapper.AddParameter("@CLAIM_ID",objDummyPolicyInfo.CLAIM_ID);		
				objDataWrapper.AddParameter("@INSURED_NAME",objDummyPolicyInfo.INSURED_NAME);		
				objDataWrapper.AddParameter("@POLICY_NUMBER",objDummyPolicyInfo.POLICY_NUMBER);		
				objDataWrapper.AddParameter("@EFFECTIVE_DATE",objDummyPolicyInfo.EFFECTIVE_DATE);		
				objDataWrapper.AddParameter("@EXPIRATION_DATE",objDummyPolicyInfo.EXPIRATION_DATE);		
				objDataWrapper.AddParameter("@LOB_ID",objDummyPolicyInfo.LOB_ID);		
				objDataWrapper.AddParameter("@NOTES",objDummyPolicyInfo.NOTES);		
				objDataWrapper.AddParameter("@DUMMY_ADD1",objDummyPolicyInfo.DUMMY_ADD1);		
				objDataWrapper.AddParameter("@DUMMY_ADD2",objDummyPolicyInfo.DUMMY_ADD2);		
				objDataWrapper.AddParameter("@DUMMY_CITY",objDummyPolicyInfo.DUMMY_CITY);		
				objDataWrapper.AddParameter("@DUMMY_ZIP",objDummyPolicyInfo.DUMMY_ZIP);		
				objDataWrapper.AddParameter("@DUMMY_STATE",objDummyPolicyInfo.DUMMY_STATE);		
				objDataWrapper.AddParameter("@DUMMY_COUNTRY",objDummyPolicyInfo.DUMMY_COUNTRY);		
				objDataWrapper.AddParameter("@CREATED_BY",objDummyPolicyInfo.CREATED_BY);		
				objDataWrapper.AddParameter("@CREATED_DATETIME",objDummyPolicyInfo.CREATED_DATETIME);		

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DUMMY_POLICY_ID",objDummyPolicyInfo.DUMMY_POLICY_ID,SqlDbType.Int ,ParameterDirection.Output);

				int returnResult = 0;				
				if(TransactionLogRequired)
				{
					objDummyPolicyInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"\Claims\Aspx\Policy\DummyPolicyPopUp.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();					
					string strTranXML = objBuilder.GetTransactionLogXML(objDummyPolicyInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objDummyPolicyInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1693", "");// "New DummyPolicy is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;					
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);					
				}
				else
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				intDUMMY_POLICY_ID		=	int.Parse(objSqlParameter.Value.ToString());
				return intDUMMY_POLICY_ID;
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

		#region Get Claim data for dummy policy
		public static DataTable GetClaimDataForDummyPolicy(string strCLAIM_NUMBER,int intCLAIM_ID)
		{
			string		strStoredProc	=	"Proc_GetClaimDataForDummyPolicy";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			DataSet dsSet = new DataSet();
			try
			{			
				objDataWrapper.AddParameter("@CLAIM_ID",intCLAIM_ID);
				objDataWrapper.AddParameter("@CLAIM_NUMBER",strCLAIM_NUMBER);
				dsSet = objDataWrapper.ExecuteDataSet(strStoredProc);
				if(dsSet!=null && dsSet.Tables.Count>0)
					return dsSet.Tables[0];
				else
					return null;								
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
				if(dsSet!=null) dsSet.Dispose();
			}
		}

		#endregion
	}
}
