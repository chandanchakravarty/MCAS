using System;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;

namespace Cms.BusinessLayer.BlAccount
{
	/// <summary>
	/// Summary description for ClsPolicyPremium.
	/// </summary>
	public class ClsPolicyPremium:Cms.BusinessLayer.BlAccount.ClsAccount
	{
		public const string PROC_POST_PREMIUM = "Proc_InsertPremiumPolicyOpenItems";
		public const string PROC_POST_PREMIUM_CHANGE = "Proc_ChangePremiumPolicyOpenItems";

		public int PostPolicyPremium(int PolicyId, int PolicyVersionId, int CustomerId)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				
				DataSet dsPolicy = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text, "SELECT POLICY_ID, POLICY_VERSION_ID, CUSTOMER_ID, APP_ID, APP_VERSION_ID, PREMIUM_XML FROM ACT_PREMIUM_PROCESS_DETAILS WHERE ISNULL(PROCESS_STATUS,'') = ''", null);
				
				int iRows;
				System.Data.DataRow dr;
				double dblPremium, dblOtherFees, dblMCCAFees; 

				for( iRows=0; iRows < dsPolicy.Tables[0].Rows.Count; iRows++)
				{
					dr = dsPolicy.Tables[0].Rows[iRows];
					
					//Fetching the premium details from premium xml
					GetPremiumDetails(Convert.ToString(dr["PREMIUM_XML"]), out dblPremium, out dblOtherFees, out dblMCCAFees);
					
					objWrapper.AddParameter("@CUSTOMER_ID", dr["CUSTOMER_ID"]);
					objWrapper.AddParameter("@APP_ID", dr["APP_ID"]);
					objWrapper.AddParameter("@APP_VERSION_ID", dr["APP_VERSION_ID"]);

					objWrapper.AddParameter("@POLICY_ID", dr["POLICY_ID"]);
					objWrapper.AddParameter("@POLICY_VERSION_ID", dr["POLICY_VERSION_ID"]);

					objWrapper.AddParameter("@PREMIUM_AMOUNT", dblPremium);
					objWrapper.AddParameter("@MCCA_FEES", dblOtherFees);
					objWrapper.AddParameter("@OTHER_FEES", dblMCCAFees);

					objWrapper.AddParameter("@TRANS_DESC", null);
					objWrapper.AddParameter("@PARAM2", null);
					objWrapper.AddParameter("@PARAM3", null);
					objWrapper.AddParameter("@PARAM4", null);

					objWrapper.ExecuteNonQuery(PROC_POST_PREMIUM);	
					
					objWrapper.ClearParameteres();
				}

				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return iRows;
			}
			catch(Exception objExp)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw (objExp);
			}
		}

		/// <summary>
		/// This function will post all the pending policy premium to accounts
		/// </summary>
		/// <returns>Nof os policies posted in accounts</returns>
		public int PostPremiumDetails()
		{
			
			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				
				DataSet dsPolicy = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text, "SELECT POLICY_ID, POLICY_VERSION_ID, CUSTOMER_ID, APP_ID, APP_VERSION_ID, PREMIUM_XML FROM ACT_PREMIUM_PROCESS_DETAILS WHERE ISNULL(PROCESS_STATUS,'') = ''", null);
				
				int iRows;
				System.Data.DataRow dr;
				double dblPremium, dblOtherFees, dblMCCAFees; 

				for( iRows=0; iRows < dsPolicy.Tables[0].Rows.Count; iRows++)
				{
					dr = dsPolicy.Tables[0].Rows[iRows];
					
					//Fetching the premium details from premium xml
					GetPremiumDetails(Convert.ToString(dr["PREMIUM_XML"]), out dblPremium, out dblOtherFees, out dblMCCAFees);
					
					DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text, "SELECT POLICY_ID FROM ACT_CUSTOMER_OPEN_ITEMS WITH(NOLOCK) "
						+ " WHERE POLICY_ID = " + Convert.ToString(dr["POLICY_ID"])
						+ " AND POLICY_VERSION_ID = " + Convert.ToString(dr["POLICY_VERSION_ID"])
						+ " AND CUSTOMER_ID = " + Convert.ToString(dr["CUSTOMER_ID"])
						, null);
					
					if (ds.Tables[0].Rows.Count <= 0)
					{
						objWrapper.AddParameter("@CUSTOMER_ID", dr["CUSTOMER_ID"]);
						objWrapper.AddParameter("@APP_ID", dr["APP_ID"]);
						objWrapper.AddParameter("@APP_VERSION_ID", dr["APP_VERSION_ID"]);

						objWrapper.AddParameter("@POLICY_ID", dr["POLICY_ID"]);
						objWrapper.AddParameter("@POLICY_VERSION_ID", dr["POLICY_VERSION_ID"]);

						objWrapper.AddParameter("@PREMIUM_AMOUNT", dblPremium);
						objWrapper.AddParameter("@MCCA_FEES", dblOtherFees);
						objWrapper.AddParameter("@OTHER_FEES", dblMCCAFees);

						objWrapper.AddParameter("@PARAM1", null);
						objWrapper.AddParameter("@PARAM2", null);
						objWrapper.AddParameter("@PARAM3", null);
						objWrapper.AddParameter("@PARAM4", null);

						objWrapper.ExecuteNonQuery(PROC_POST_PREMIUM);	
					}
					else
					{
						objWrapper.AddParameter("@CUSTOMER_ID", dr["CUSTOMER_ID"]);
						objWrapper.AddParameter("@APP_ID", dr["APP_ID"]);
						objWrapper.AddParameter("@APP_VERSION_ID", dr["APP_VERSION_ID"]);

						objWrapper.AddParameter("@POLICY_ID", dr["POLICY_ID"]);
						objWrapper.AddParameter("@POLICY_VERSION_ID", dr["POLICY_VERSION_ID"]);

						objWrapper.AddParameter("@CHANGE_PREMIUM_AMOUNT", dblPremium);
						
						objWrapper.AddParameter("@PARAM1", null);
						objWrapper.AddParameter("@PARAM2", null);
						objWrapper.AddParameter("@PARAM3", null);
						objWrapper.AddParameter("@PARAM4", null);

						objWrapper.ExecuteNonQuery(PROC_POST_PREMIUM_CHANGE);
					}
					objWrapper.ClearParameteres();
				}

				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return iRows;
			}
			catch(Exception objExp)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw (objExp);
			}
			
		}

		/// <summary>
		/// Getting the details of premium from premium XML and return the ourput variable
		/// </summary>
		/// <param name="strPremiumXML">Premium XML containg the details of premium in the form of XML</param>
		/// <param name="dblPremium">Variable on which Premium will come</param>
		/// <param name="dblOtherFees">Variable on which other fees will come</param>
		/// <param name="dblMCCAFees">Variable on which MCCA fees will come</param>
		private void GetPremiumDetails(string strPremiumXML, out double dblPremium, out double dblOtherFees, out double dblMCCAFees)
		{
			/*As premium xml is not yet finalize, hence we are passing the hard code values from here*/
			/*In future, we will change this code and will retreive the values from xml nodes*/
			dblPremium		= 15000;
			dblOtherFees	= 50;
			dblMCCAFees		= 50;
		}
	}
}
