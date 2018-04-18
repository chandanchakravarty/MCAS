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
	/// Summary description for ClsDriverDetails.
	/// </summary>
	public class ClsScheduledItems : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		
		
		public ClsScheduledItems()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public  DataSet GetPolInlandCoverages(int customerID, int policyID, 
			int policyVersionID, string polType)
		{
			string	strStoredProc =	"Proc_GetCLM_POL_HOME_OWNER_SCH_ITEMS_CVGS";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);	
			objWrapper.AddParameter("@POL_TYPE",polType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}

		
		
	}
}
