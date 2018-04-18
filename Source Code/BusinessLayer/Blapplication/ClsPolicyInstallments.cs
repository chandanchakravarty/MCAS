using System;
using System.Collections.Generic;
using System.Linq;
using Cms.Model.Support;
using System.Data;
using System.Collections;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlCommon;

namespace Cms.BusinessLayer.Blapplication
{
    public class ClsPolicyInstallments
    {
        public ClsPolicyInstallments()
        {

        }

        public int GenrateInstallment(ClsPolicyBillingInfo BillingInfo)
        {
            int retunval = 0;
            try
            {
                if (BillingInfo.RequiredTransactionLog)
                {
                    BillingInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/BillingInfo.aspx.resx");
                    retunval = BillingInfo.GenrateInstallments();
                }
                else
                {
                    retunval = BillingInfo.GenrateInstallments();
                }

            }
            catch (Exception ex) { throw (ex); }
            return retunval;
        }

        public int SaveInstallments(ArrayList arlBillingInfo)
        {
            int retunval = 0;
            ClsPolicyBillingInfo BillingInfo = (ClsPolicyBillingInfo)arlBillingInfo[0];
            BillingInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/BillingInfo.aspx.resx");
            retunval = BillingInfo.UpdateInstallments(arlBillingInfo);
            return retunval;
        }
        /// <summary>
        /// It would save the installment details data in Installment history table
        /// </summary>
        /// <param name="arlBillingInfo"></param>
        /// <returns></returns>
        public int SaveBoletoReprintInstallmentsDetailsHistory(ArrayList arlBillingInfo)
        {
            int retunval = 0;
            ClsPolicyBillingInfo BillingInfo = (ClsPolicyBillingInfo)arlBillingInfo[0];
            BillingInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/BillingInfo.aspx.resx");
            retunval = BillingInfo.UpdateBoletoReprintInstallments(arlBillingInfo);
            return retunval;
        }
        public int SaveMasterPolicyInstallments(ArrayList arlBillingInfo)
        {
            int retunval = 0;
            ClsPolicyBillingInfo BillingInfo = (ClsPolicyBillingInfo)arlBillingInfo[0];
            BillingInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/MasterPolicyBillingInfo.aspx.resx");
            retunval = BillingInfo.UpdateInstallments(arlBillingInfo);
            return retunval;
        }

        public static List<Model.Policy.ClsPolicyBillingInfo> GetInstallmentDetails(int iCustomerId, int iPolicyId, int iPolicy_VersionId)
        {
            DataSet ds = null;
            ClsPolicyBillingInfo objInfo = new ClsPolicyBillingInfo();
            List<Model.Policy.ClsPolicyBillingInfo> BillingInfo = new List<Model.Policy.ClsPolicyBillingInfo>();
            ds = objInfo.GetPolicyInstallments(iCustomerId, iPolicyId, iPolicy_VersionId, BlCommon.ClsCommon.BL_LANG_ID);
            if (ds != null)
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ClsPolicyBillingInfo objmodelInfo = new ClsPolicyBillingInfo();
                    ClsCommon.PopulateEbixPageModel(dr, objmodelInfo);
                    BillingInfo.Add(objmodelInfo);
                }
            return BillingInfo;

        }
        /// <summary>
        /// Get the Installment details for Boleto  reprint
        /// </summary>
        /// <param name="iCustomerId"></param>
        /// <param name="iPolicyId"></param>
        /// <param name="iPolicy_VersionId"></param>
        /// <returns></returns>
        //Added by Pradeep Kushwaha on 08-Nov-2010
        public static List<Model.Policy.ClsPolicyBillingInfo> GetBoletoReprintInstallmentDetails(int iCustomerId, int iPolicyId, int iPolicy_VersionId)
        {
            DataSet ds = null;
            ClsPolicyBillingInfo objInfo = new ClsPolicyBillingInfo();
            List<Model.Policy.ClsPolicyBillingInfo> BillingInfo = new List<Model.Policy.ClsPolicyBillingInfo>();
            ds = objInfo.GetBoletoReprintInstallmentDetailsInfo(iCustomerId, iPolicyId, iPolicy_VersionId);
            if (ds != null)
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ClsPolicyBillingInfo objmodelInfo = new ClsPolicyBillingInfo();
                    ClsCommon.PopulateEbixPageModel(dr, objmodelInfo);
                    BillingInfo.Add(objmodelInfo);
                }
            return BillingInfo;

        }
        public DataSet Getpolicy_NBSAmount(int iCustomerId, int iPolicyId, int iPolicy_VersionId, string strPolicy_Status)
        {
            ClsPolicyBillingInfo objcls = new ClsPolicyBillingInfo();
            DataSet ds;
            try
            {
                ds = objcls.GetPolicyNBSAmounts(iCustomerId, iPolicyId, iPolicy_VersionId, strPolicy_Status);
                return ds;
            }
            catch (Exception ex) { throw (ex); }
        }

        public int GenrateMasterPolicyInstallment(ClsPolicyBillingInfo BillingInfo)
        {
            int retunval = 0;
            try
            {
                BillingInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/MasterPolicyBillingInfo.aspx.resx");
                retunval = BillingInfo.GenrateMasterPolicyInstallments();
            }
            catch (Exception ex) { throw (ex); }
            return retunval;
        }
        public decimal GetMasterPolicyPremium(ClsPolicyBillingInfo BillingInfo)
        {
            decimal Premium = 0;
            Premium = BillingInfo.GetMasterPolicyPremium(BillingInfo.CUSTOMER_ID.CurrentValue, BillingInfo.POLICY_ID.CurrentValue, BillingInfo.POLICY_VERSION_ID.CurrentValue, BillingInfo.CREATED_BY.CurrentValue,  BillingInfo.PLAN_ID.CurrentValue, "RULES");
            return Premium;
        }

    }
}
