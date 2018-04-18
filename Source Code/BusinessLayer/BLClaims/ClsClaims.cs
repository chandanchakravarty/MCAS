using System;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data;
using System.Configuration;
using Cms.Model.Application;
using System.Data.SqlClient;
using Cms.BusinessLayer.BlCommon;
using System.Collections;

namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// Summary description for ClsClaims.
	/// </summary>
	public class ClsClaims:Cms.BusinessLayer.BlCommon.ClsCommon
	{
		public ClsClaims()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		//Enumeration for (Standard) Claim Party Types
		public enum enumClaimPartyTypes
		{
			CLAIMANT				= 9,
			EXPERTSERVICEPROVIDERS	= 11,
			INSURED					= 10,
			ADJUSTER				= 12,
			INJURED					= 13,
			WITNESS					= 14,
			PASSENGER				= 15,
			OTHER					= 111
		}
		public enum enumReinsCarrier
		{
			GenRe = 11781,
			ERC = 11782
		}

        public DataSet GetPolicyClaimDataSet(string CustomerID, string PolicyID, string PolicyVersionID, string ClaimID, int LangID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.VarChar);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID,SqlDbType.VarChar);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID,SqlDbType.VarChar);
				objDataWrapper.AddParameter("@CLAIM_ID",ClaimID,SqlDbType.VarChar);
                objDataWrapper.AddParameter("@LANG_ID", LangID, SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyClaimInformation");
			
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}


		public DataSet GetClaimDetails(int ClaimID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CLAIM_ID",ClaimID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetClaimDetails");
				return dsTemp;
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

		public static DataSet GetClaimCoverages(string strCLAIM_ID)
		{
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetCLM_COVERAGE_DESCRIPTION");
				if(dsTemp!=null)
					return dsTemp;
				else
					return null;
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

		public static bool ClaimExistsForCustomer(string CustomerID, string PolicyID, string PolicyVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
				SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);			
				objDataWrapper.ExecuteNonQuery("Proc_ClaimExistsForCustomer");
				int returnVal = int.Parse(retParam.Value.ToString());
				if(returnVal>0)
					return true;
				else
					return false;
			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return false;
			}
			finally{}

		}


		public static int ClaimsCheckOperation(string CheckIDs, string Operation)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@CHECK_IDS",CheckIDs);
			objWrapper.AddParameter("@OPERATION",Operation);
			int retValue = objWrapper.ExecuteNonQuery("PROC_PERFORM_CLAIM_CHECK_OPERATION");			
			return retValue;			
		}
	}
}
