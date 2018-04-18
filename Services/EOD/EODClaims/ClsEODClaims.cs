using System;
using EODCommon;
using Cms.BusinessLayer.BLClaims;
using System.Data ;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;

namespace EODClaims
{
    public class ClsEODClaims : ClsEODCommon
    {
        string strCnnString ;
		Cms.DataLayer.DataWrapper  objDataWrapper;
        public ClsEODClaims()
		{
			strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			//objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
		}

		public void DoCleanUP()
		{
			objDataWrapper.Dispose();
		}

        #region LaunchAuto Close Reserve/Claims/Update Reserve Method
        /// <summary>
        /// Will Close Pending Reserve/Claims
        /// </summary>
        public void LaunchAutoCloseClaims()
        {
            DataTable dtClaims;
            EODLogInfo objLog = new EODLogInfo();
            objLog.Activity = (int)EODActivities.ClaimsProcessing;
            objLog.SubActivity = (int)EODActivities.AutoClaimsClose;

            try
            {
                objLog.ActivtyDescription = "Fetching Claims to Close : ";
                objLog.StartDateTime = System.DateTime.Now;
                dtClaims = GetClaimsToClose();
            }
            catch (Exception ex)
            {
                objLog.Status = ActivityStatus.FAILED;
                objLog.EndDateTime = DateTime.Now;
                objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                WriteLog(objLog);
                return;
            }
            objLog.ActivtyDescription += dtClaims.Rows.Count.ToString() + " Records to process";
            objLog.Status = ActivityStatus.SUCCEDDED;
            objLog.EndDateTime = DateTime.Now;
            WriteLog(objLog);

            for (int i = 0; i < dtClaims.Rows.Count; i++)
            {
                try
                {
                    DataRow dr = dtClaims.Rows[i];

                    objLog.ActivtyDescription = "Auto Closing Claims"; 
                    objLog.StartDateTime = System.DateTime.Now;
                    objLog.ClientID = int.Parse(dr["CUSTOMER_ID"].ToString());
                    objLog.PolicyID = int.Parse(dr["POLICY_ID"].ToString());
                    objLog.PolicyVersionID = int.Parse(dr["POLICY_VERSION_ID"].ToString());
                    objLog.AdditionalInfo = "Claim ID = " + dr["CLAIM_ID"].ToString();
                    AutoCloseClaims(int.Parse(dr["CLAIM_ID"].ToString()));
                }
                catch (Exception ex)
                {
                    objLog.Status = ActivityStatus.FAILED;
                    objLog.EndDateTime = DateTime.Now;
                    objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                    WriteLog(objLog);
                    return;
                }
                objLog.Status = ActivityStatus.SUCCEDDED;
                objLog.EndDateTime = DateTime.Now;
                WriteLog(objLog);
            }
        }
        public void LaunchAutoUpdateReserve()
        {
            DataTable dtClaims;
            EODLogInfo objLog = new EODLogInfo();
            objLog.Activity = (int)EODActivities.ClaimsProcessing;
            objLog.SubActivity = (int)EODActivities.ClaimReserveUpdate;
            try
            {
                objLog.ActivtyDescription = "Fetching Claims to Update Reserve : ";
                objLog.StartDateTime = System.DateTime.Now;
                dtClaims = GetClaimsToUpdateReserve();
            }
            catch (Exception ex)
            {
                objLog.Status = ActivityStatus.FAILED;
                objLog.EndDateTime = DateTime.Now;
                objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                WriteLog(objLog);
                return;
            }
            objLog.ActivtyDescription += dtClaims.Rows.Count.ToString() + " Records to process";
            objLog.Status = ActivityStatus.SUCCEDDED;
            objLog.EndDateTime = DateTime.Now;
            WriteLog(objLog);

            for (int i = 0; i < dtClaims.Rows.Count; i++)
            {
                try
                {
                    DataRow dr = dtClaims.Rows[i];

                    objLog.ActivtyDescription = "Auto updating Claim : "; 
                    objLog.StartDateTime = System.DateTime.Now;
                    objLog.ClientID = int.Parse(dr["CUSTOMER_ID"].ToString());
                    objLog.PolicyID = int.Parse(dr["POLICY_ID"].ToString());
                    objLog.PolicyVersionID = int.Parse(dr["POLICY_VERSION_ID"].ToString());
                    objLog.AdditionalInfo = "Claim ID = " + dr["CLAIM_ID"].ToString();
                    AutoUpdateReserves(int.Parse(dr["CLAIM_ID"].ToString()));
                }
                catch (Exception ex)
                {
                    objLog.Status = ActivityStatus.FAILED;
                    objLog.EndDateTime = DateTime.Now;
                    objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                    WriteLog(objLog);
                    return;
                }
                objLog.Status = ActivityStatus.SUCCEDDED;
                objLog.EndDateTime = DateTime.Now;
                WriteLog(objLog);
            }

        }

        #endregion
        #region Close Claims/update Claims Reserve Methods
        private void AutoCloseClaims(int ClaimID)
        {
            objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CLAIM_ID", ClaimID);
                objDataWrapper.AddParameter("@CREATED_BY", EOD_USER_ID);
                objDataWrapper.AddParameter("@CREATED_DATETIME", DateTime.Now);
                objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);

                objDataWrapper.ExecuteNonQuery("Proc_CloseClaim");

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            finally
            {
                objDataWrapper.Dispose();
            }

        }
        /*
        private void AutoCloseClaims()
        {
            objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                ClsActivity objActivity = new ClsActivity();
                int retVal = objActivity.CloseClaim(objDataWrapper, EOD_USER_ID, DateTime.Now, ClsCommon.BL_LANG_ID);
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            finally {
                objDataWrapper.Dispose();
            }
            
        }*/
        private void AutoUpdateReserves(int ClaimID)
        {
            objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CLAIM_ID", ClaimID);
                objDataWrapper.AddParameter("@CREATED_BY", EOD_USER_ID);
                objDataWrapper.AddParameter("@CREATED_DATETIME", DateTime.Now);
                objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);

                objDataWrapper.ExecuteNonQuery("Proc_AutoReserveClaimActivity");
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
            
        }
        /*
        private void AutoUpdateReserves()
        {
            objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                ClsActivity objActivity = new ClsActivity();
                int retVal = objActivity.UpdateClaimActivityReserve(objDataWrapper, EOD_USER_ID, DateTime.Now, ClsCommon.BL_LANG_ID);
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            finally
            {
                objDataWrapper.Dispose();
            }

        }*/
        #endregion
        private DataTable GetClaimsToClose()
        {
            string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetClaimsToClose");
            objDataWrapper.Dispose();
            return ds.Tables[0];
        }
        private DataTable GetClaimsToUpdateReserve()
        {
            string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetClaimsToAutoReserveUpdate");
            objDataWrapper.Dispose();
            return ds.Tables[0];
        }
    }
}
