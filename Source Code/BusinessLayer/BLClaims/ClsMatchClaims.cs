/******************************************************************************************
<Author				: -	Sumit Chhabra
<Start Date			: -	May 10,2006
<End Date			: -	
<Description		: -	Class file for Match Policy
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
	/// Summary description for ClsMatchClaims.
	/// </summary>
	public class ClsMatchClaims : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private			bool		boolTransactionRequired;
		private const string ASSIGN_CLAIM_TO_POLICY = "Proc_AssignClaimToPolicy";			
		private const string GET_USER_AUTHORITY_LEVEL	= "Proc_GetUserAuthorityLevel";	
		public ClsMatchClaims()
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

		#region Assign Claim to Policy
		/// <summary>
		/// Returns only the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public int AssignClaimToPolicy(string CUSTOMER_ID,string POLICY_ID,string POLICY_VERSION_ID,string DUMMY_POLICY_ID,string CLAIM_ID,string CONTINUE_WITH_LOB)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",POLICY_ID);			
			objWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID);			
			objWrapper.AddParameter("@DUMMY_POLICY_ID",DUMMY_POLICY_ID);
			objWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);	
			objWrapper.AddParameter("@CONTINUE_WITH_LOB",CONTINUE_WITH_LOB);				
			SqlParameter objSqlParameter  = (SqlParameter) objWrapper.AddParameter("@RETURN","0",SqlDbType.Int,ParameterDirection.ReturnValue);
			
			objWrapper.ExecuteNonQuery(ASSIGN_CLAIM_TO_POLICY);			
			int retVal = int.Parse(objSqlParameter.Value.ToString());
			return retVal;
		}

		#endregion				

		#region Get Authority Level for user
		public static bool GetUserAuthorityLevel(string User,string DUMMY_POLICY_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@USER_ID",User);			
			objWrapper.AddParameter("@DUMMY_POLICY_ID",DUMMY_POLICY_ID);			
			DataSet dsTemp = objWrapper.ExecuteDataSet(GET_USER_AUTHORITY_LEVEL);			
			if(dsTemp!=null && dsTemp.Tables.Count>0 && dsTemp.Tables[0].Rows.Count>0 && dsTemp.Tables[0].Rows[0]["CLAIM_ON_DUMMY_POLICY"]!=null && dsTemp.Tables[0].Rows[0]["CLAIM_ON_DUMMY_POLICY"].ToString().ToUpper()=="TRUE")
				return true;
			else
				return false;
		}
		#endregion
		
	}
}
