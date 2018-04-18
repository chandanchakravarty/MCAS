/******************************************************************************************
<Author				: - Lalit Kumar Chauhan
<Start Date			: -	25-04-2010
<End Date			: -	
<Purpose			: - Genrate Billing Installments 
<Description		: - 
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 

*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.EbixDataTypes;
using Cms.Model.Support;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Xml;
using Cms.EbixDataLayer;

namespace Cms.Model.Policy
{
    [Serializable]
    public class ClsBillingDetailsInfo : ClsModelBaseClass, IDisposable
    {

        public ClsBillingDetailsInfo()
        {
            PropertyCollection();
        }
        public void Dispose()
        {
            System.GC.ReRegisterForFinalize(this);
        }
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);
            base.htPropertyCollection.Add("TOTAL_PREMIUM", TOTAL_PREMIUM);
            base.htPropertyCollection.Add("TOTAL_INTEREST_AMOUNT", TOTAL_INTEREST_AMOUNT);
            base.htPropertyCollection.Add("TOTAL_FEES", TOTAL_FEES);
            base.htPropertyCollection.Add("TOTAL_TAXES", TOTAL_TAXES);
            base.htPropertyCollection.Add("TOTAL_AMOUNT", TOTAL_AMOUNT);
            base.htPropertyCollection.Add("PLAN_ID", PLAN_ID);
            base.htPropertyCollection.Add("INSTALLMENT_AMOUNT", INSTALLMENT_AMOUNT);
            base.htPropertyCollection.Add("INTEREST_AMOUNT", INTEREST_AMOUNT);
            base.htPropertyCollection.Add("FEE", FEE);
            base.htPropertyCollection.Add("TAXES", TAXES);
            base.htPropertyCollection.Add("TOTAL", TOTAL);
            base.htPropertyCollection.Add("INSTALLMENT_EFFECTIVE_DATE", INSTALLMENT_EFFECTIVE_DATE);
            base.htPropertyCollection.Add("INSTALLMENT_NO", INSTALLMENT_NO);
            base.htPropertyCollection.Add("RETVAL", RETVAL);
            base.htPropertyCollection.Add("TRAN_TYPE", TRAN_TYPE);
            base.htPropertyCollection.Add("TRAN_PREMIUM_AMOUNT", TRAN_PREMIUM_AMOUNT);
            base.htPropertyCollection.Add("TRAN_INTEREST_AMOUNT", TRAN_INTEREST_AMOUNT);
            base.htPropertyCollection.Add("TRAN_FEE", TRAN_FEE);
            base.htPropertyCollection.Add("TRAN_TAXES", TRAN_TAXES);
            base.htPropertyCollection.Add("TRAN_TOTAL", TRAN_TOTAL);
            base.htPropertyCollection.Add("RELEASED_STATUS", RELEASED_STATUS);

            base.htPropertyCollection.Add("TOTAL_TRAN_PREMIUM", TOTAL_TRAN_PREMIUM);
            base.htPropertyCollection.Add("TOTAL_TRAN_INTEREST_AMOUNT", TOTAL_TRAN_INTEREST_AMOUNT);
            base.htPropertyCollection.Add("TOTAL_TRAN_FEES", TOTAL_TRAN_FEES);
            base.htPropertyCollection.Add("TOTAL_TRAN_TAXES", TOTAL_TRAN_TAXES);
            base.htPropertyCollection.Add("TOTAL_TRAN_AMOUNT", TOTAL_TRAN_AMOUNT);
            base.htPropertyCollection.Add("TOTAL_CHANGE_INFORCE_PRM", TOTAL_CHANGE_INFORCE_PRM);
            base.htPropertyCollection.Add("TOTAL_INFO_PRM", TOTAL_INFO_PRM);
            base.htPropertyCollection.Add("TOTAL_STATE_FEES", TOTAL_STATE_FEES);
            base.htPropertyCollection.Add("TOTAL_TRAN_STATE_FEES", TOTAL_TRAN_STATE_FEES);
            base.htPropertyCollection.Add("PRM_DIST_TYPE", PRM_DIST_TYPE);
            base.htPropertyCollection.Add("MODE_OF_PAYMENT", MODE_OF_PAYMENT);
            base.htPropertyCollection.Add("MODE_OF_DOWN_PAYMENT", MODE_OF_DOWN_PAYMENT);

            base.htPropertyCollection.Add("ENDO_NO", ENDO_NO);
            base.htPropertyCollection.Add("CO_APPLICANT_NAME", CO_APPLICANT_NAME);
            base.htPropertyCollection.Add("ROW_ID", ROW_ID);
            base.htPropertyCollection.Add("CO_APPLICANT_ID", CO_APPLICANT_ID);
            base.htPropertyCollection.Add("BOLETO_NO", BOLETO_NO);
            base.htPropertyCollection.Add("RECEIVED_DATE", RECEIVED_DATE);
            base.htPropertyCollection.Add("INSTALLMENT_EXPIRE_DATE", INSTALLMENT_EXPIRE_DATE);

            base.htPropertyCollection.Add("END_EFFECTIVE_DATE", END_EFFECTIVE_DATE);
            base.htPropertyCollection.Add("END_EXPIRY_DATE", END_EXPIRY_DATE);
            base.htPropertyCollection.Add("POLICY_EFFECTIVE_DATE", POLICY_EFFECTIVE_DATE);
            base.htPropertyCollection.Add("POLICY_EXPIRATION_DATE", POLICY_EXPIRATION_DATE);

            //Added by Kuldeep for some added fields on billing info
            base.htPropertyCollection.Add("TOTAL_BEFORE_GST", TOTAL_BEFORE_GST);
            base.htPropertyCollection.Add("TOTAL_AFTER_GST", TOTAL_AFTER_GST);
            base.htPropertyCollection.Add("GROSS_COMMISSION", GROSS_COMMISSION);
            base.htPropertyCollection.Add("GST_ON_COMMISSION", GST_ON_COMMISSION);
            base.htPropertyCollection.Add("TOTAL_COMM_AFTER_GST", TOTAL_COMM_AFTER_GST);

            //@PRM_DIST_TYPE
        }

        #region Declare Property

        public EbixInt32 CUSTOMER_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CUSTOMER_ID"]) == null ? new EbixInt32("CUSTOMER_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CUSTOMER_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CUSTOMER_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }        //public EbixInt32 CUSTOMER_ID 

        public EbixInt32 POLICY_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_ID"]) == null ? new EbixInt32("POLICY_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }          //public EbixInt32 POLICY_ID

        public EbixInt32 POLICY_VERSION_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]) == null ? new EbixInt32("POLICY_VERSION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }  //public EbixInt32 POLICY_VERSION_ID 

        public EbixDecimal TOTAL_PREMIUM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_PREMIUM"]) == null ? new EbixDecimal("TOTAL_PREMIUM") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_PREMIUM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_PREMIUM"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal TOTAL_INTEREST_AMOUNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_INTEREST_AMOUNT"]) == null ? new EbixDecimal("TOTAL_INTEREST_AMOUNT") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_INTEREST_AMOUNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_INTEREST_AMOUNT"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal TOTAL_FEES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_FEES"]) == null ? new EbixDecimal("TOTAL_FEES") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_FEES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_FEES"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal TOTAL_TAXES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TAXES"]) == null ? new EbixDecimal("TOTAL_TAXES") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TAXES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TAXES"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal TOTAL_AMOUNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_AMOUNT"]) == null ? new EbixDecimal("TOTAL_AMOUNT") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_AMOUNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_AMOUNT"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixInt32 RETVAL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RETVAL"]) == null ? new EbixInt32("RETVAL") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RETVAL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RETVAL"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 PLAN_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PLAN_ID"]) == null ? new EbixInt32("PLAN_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PLAN_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PLAN_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixDecimal INSTALLMENT_AMOUNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["INSTALLMENT_AMOUNT"]) == null ? new EbixDecimal("INSTALLMENT_AMOUNT") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["INSTALLMENT_AMOUNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["INSTALLMENT_AMOUNT"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal INTEREST_AMOUNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["INTEREST_AMOUNT"]) == null ? new EbixDecimal("INTEREST_AMOUNT") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["INTEREST_AMOUNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["INTEREST_AMOUNT"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal FEE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["FEE"]) == null ? new EbixDecimal("FEE") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["FEE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["FEE"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal TAXES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TAXES"]) == null ? new EbixDecimal("TAXES") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TAXES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TAXES"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal TOTAL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL"]) == null ? new EbixDecimal("TOTAL") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDateTime INSTALLMENT_EFFECTIVE_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["INSTALLMENT_EFFECTIVE_DATE"]) == null ? new EbixDateTime("INSTALLMENT_EFFECTIVE_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["INSTALLMENT_EFFECTIVE_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["INSTALLMENT_EFFECTIVE_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }

        public EbixInt32 INSTALLMENT_NO
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INSTALLMENT_NO"]) == null ? new EbixInt32("INSTALLMENT_NO") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INSTALLMENT_NO"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INSTALLMENT_NO"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString TRAN_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TRAN_TYPE"]) == null ? new EbixString("TRAN_TYPE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TRAN_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TRAN_TYPE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixDecimal TRAN_PREMIUM_AMOUNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TRAN_PREMIUM_AMOUNT"]) == null ? new EbixDecimal("TRAN_PREMIUM_AMOUNT") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TRAN_PREMIUM_AMOUNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TRAN_PREMIUM_AMOUNT"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal TRAN_INTEREST_AMOUNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TRAN_INTEREST_AMOUNT"]) == null ? new EbixDecimal("TRAN_INTEREST_AMOUNT") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TRAN_INTEREST_AMOUNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TRAN_INTEREST_AMOUNT"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal TRAN_FEE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TRAN_FEE"]) == null ? new EbixDecimal("TRAN_FEE") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TRAN_FEE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TRAN_FEE"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal TRAN_TAXES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TRAN_TAXES"]) == null ? new EbixDecimal("TRAN_TAXES") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TRAN_TAXES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TRAN_TAXES"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal TRAN_TOTAL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TRAN_TOTAL"]) == null ? new EbixDecimal("TRAN_TOTAL") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TRAN_TOTAL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TRAN_TOTAL"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixString RELEASED_STATUS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RELEASED_STATUS"]) == null ? new EbixString("RELEASED_STATUS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RELEASED_STATUS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RELEASED_STATUS"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixDecimal TOTAL_TRAN_PREMIUM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TRAN_PREMIUM"]) == null ? new EbixDecimal("TOTAL_TRAN_PREMIUM") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TRAN_PREMIUM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TRAN_PREMIUM"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal TOTAL_TRAN_INTEREST_AMOUNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TRAN_INTEREST_AMOUNT"]) == null ? new EbixDecimal("TOTAL_TRAN_INTEREST_AMOUNT") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TRAN_INTEREST_AMOUNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TRAN_INTEREST_AMOUNT"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal TOTAL_TRAN_FEES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TRAN_FEES"]) == null ? new EbixDecimal("TOTAL_TRAN_FEES") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TRAN_FEES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TRAN_FEES"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal TOTAL_TRAN_TAXES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TRAN_TAXES"]) == null ? new EbixDecimal("TOTAL_TRAN_TAXES") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TRAN_TAXES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TRAN_TAXES"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal TOTAL_TRAN_AMOUNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TRAN_AMOUNT"]) == null ? new EbixDecimal("TOTAL_TRAN_AMOUNT") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TRAN_AMOUNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_TRAN_AMOUNT"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixDecimal TOTAL_CHANGE_INFORCE_PRM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_CHANGE_INFORCE_PRM"]) == null ? new EbixDecimal("TOTAL_CHANGE_INFORCE_PRM") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_CHANGE_INFORCE_PRM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_CHANGE_INFORCE_PRM"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixInt32 PRM_DIST_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PRM_DIST_TYPE"]) == null ? new EbixInt32("PRM_DIST_TYPE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PRM_DIST_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PRM_DIST_TYPE"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixDecimal TOTAL_INFO_PRM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_INFO_PRM"]) == null ? new EbixDecimal("TOTAL_INFO_PRM") : ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_INFO_PRM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDecimal)htPropertyCollection["TOTAL_INFO_PRM"]).CurrentValue = Convert.ToDecimal(value);
            }
        }

        public EbixString ENDO_NO
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ENDO_NO"]) == null ? new EbixString("ENDO_NO") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ENDO_NO"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ENDO_NO"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixDouble TOTAL_STATE_FEES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_STATE_FEES"]) == null ? new EbixDouble("TOTAL_STATE_FEES") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_STATE_FEES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_STATE_FEES"]).CurrentValue = Convert.ToDouble(value);
            }
        }

        public EbixDouble TOTAL_TRAN_STATE_FEES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_TRAN_STATE_FEES"]) == null ? new EbixDouble("TOTAL_TRAN_STATE_FEES") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_TRAN_STATE_FEES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_TRAN_STATE_FEES"]).CurrentValue = Convert.ToDouble(value);
            }
        }
        public EbixString MODE_OF_PAYMENT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MODE_OF_PAYMENT"]) == null ? new EbixString("MODE_OF_PAYMENT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MODE_OF_PAYMENT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MODE_OF_PAYMENT"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString MODE_OF_DOWN_PAYMENT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MODE_OF_DOWN_PAYMENT"]) == null ? new EbixString("MODE_OF_DOWN_PAYMENT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MODE_OF_DOWN_PAYMENT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MODE_OF_DOWN_PAYMENT"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString CO_APPLICANT_NAME
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CO_APPLICANT_NAME"]) == null ? new EbixString("CO_APPLICANT_NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CO_APPLICANT_NAME"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CO_APPLICANT_NAME"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixInt32 ROW_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ROW_ID"]) == null ? new EbixInt32("ROW_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ROW_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ROW_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        public EbixInt32 CO_APPLICANT_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CO_APPLICANT_ID"]) == null ? new EbixInt32("CO_APPLICANT_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CO_APPLICANT_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CO_APPLICANT_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        //Added by Pradeep Kushwaha on 11-Nov-2010
        public EbixString BOLETO_NO
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BOLETO_NO"]) == null ? new EbixString("BOLETO_NO") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BOLETO_NO"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BOLETO_NO"]).CurrentValue = Convert.ToString(value);
            }
        }// public EbixString BOLETO_NO

        //Added by Lalit Chauhan on dec-15-2010
        public EbixDateTime RECEIVED_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RECEIVED_DATE"]) == null ? new EbixDateTime("RECEIVED_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RECEIVED_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RECEIVED_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }// public EbixString BOLETO_NO

        //Added by Pradeep Kushwaha on 18-Jan-2011
        public EbixDateTime INSTALLMENT_EXPIRE_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["INSTALLMENT_EXPIRE_DATE"]) == null ? new EbixDateTime("INSTALLMENT_EXPIRE_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["INSTALLMENT_EXPIRE_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["INSTALLMENT_EXPIRE_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }//INSTALLMENT_EXPIRE_DATE
        public EbixDateTime END_EFFECTIVE_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["END_EFFECTIVE_DATE"]) == null ? new EbixDateTime("END_EFFECTIVE_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["END_EFFECTIVE_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["END_EFFECTIVE_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }//END_EFFECTIVE_DATE
        public EbixDateTime END_EXPIRY_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["END_EXPIRY_DATE"]) == null ? new EbixDateTime("END_EXPIRY_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["END_EXPIRY_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["END_EXPIRY_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }//END_EXPIRY_DATE
        public EbixDateTime POLICY_EFFECTIVE_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["POLICY_EFFECTIVE_DATE"]) == null ? new EbixDateTime("POLICY_EFFECTIVE_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["POLICY_EFFECTIVE_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["POLICY_EFFECTIVE_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }//POLICY_EFFECTIVE_DATE
        public EbixDateTime POLICY_EXPIRATION_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["POLICY_EXPIRATION_DATE"]) == null ? new EbixDateTime("POLICY_EXPIRATION_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["POLICY_EXPIRATION_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["POLICY_EXPIRATION_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }//POLICY_EXPIRATION_DATE
        //Added till here 

        //aDDED BY kULDEEP FOR tfs 3408 ON 8-fEB-2012
        public EbixDouble TOTAL_BEFORE_GST
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_BEFORE_GST"]) == null ? new EbixDouble("TOTAL_BEFORE_GST") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_BEFORE_GST"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_BEFORE_GST"]).CurrentValue = Convert.ToDouble(value);
            }
        }
        public EbixDouble TOTAL_AFTER_GST
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_AFTER_GST"]) == null ? new EbixDouble("TOTAL_AFTER_GST") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_AFTER_GST"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_AFTER_GST"]).CurrentValue = Convert.ToDouble(value);
            }
        }

        public EbixDouble GROSS_COMMISSION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["GROSS_COMMISSION"]) == null ? new EbixDouble("GROSS_COMMISSION") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["GROSS_COMMISSION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["GROSS_COMMISSION"]).CurrentValue = Convert.ToDouble(value);
            }
        }
        public EbixDouble GST_ON_COMMISSION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["GST_ON_COMMISSION"]) == null ? new EbixDouble("GST_ON_COMMISSION") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["GST_ON_COMMISSION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["GST_ON_COMMISSION"]).CurrentValue = Convert.ToDouble(value);
            }
        }

        public EbixDouble TOTAL_COMM_AFTER_GST
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_COMM_AFTER_GST"]) == null ? new EbixDouble("TOTAL_COMM_AFTER_GST") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_COMM_AFTER_GST"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_COMM_AFTER_GST"]).CurrentValue = Convert.ToDouble(value);
            }
        }
        #endregion

        public DataSet GetPolicyInstallments(int iCustomerID, int iPolicyID, int iPolicyVersionID, int LangID)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "Proc_GetPolicyBillingInstalmentsDetails";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", iCustomerID);
                base.htGetDataParamCollections.Add("@POLICY_ID", iPolicyID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", iPolicyVersionID);
                base.htGetDataParamCollections.Add("@LANG_ID", LangID);
                ds = base.GetData();
            }
            catch { }
            finally { }
            return ds;

        }         //Add New Discount Percent In user Discount Percent List

        public DataSet CheckPolicyCreatedMode(int iCustomerID, int iPolicyID, int iPolicyVersionID)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "Proc_CheckPolicyCreatedMode";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", iCustomerID);
                base.htGetDataParamCollections.Add("@POLICY_ID", iPolicyID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", iPolicyVersionID);

                ds = base.GetData();
            }
            catch { }
            finally { }
            return ds;

        }         //Add New Discount Percent In user Discount Percent List

        /// <summary>
        /// Get the Boleto reprint installment details
        /// </summary>
        /// <param name="iCustomerID"></param>
        /// <param name="iPolicyID"></param>
        /// <param name="iPolicyVersionID"></param>
        /// <returns></returns>
        //Added by Pradeep Kushwaha on 08-Nov-2010
        public DataSet GetBoletoReprintInstallmentDetailsInfo(int iCustomerID, int iPolicyID, int iPolicyVersionID)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "Proc_GetBoletoReprintInstalmentsDetails";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", iCustomerID);
                base.htGetDataParamCollections.Add("@POLICY_ID", iPolicyID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", iPolicyVersionID);
                ds = base.GetData();
            }
            catch { }
            finally { }
            return ds;

        }//public DataSet GetBoletoReprintInstallmentDetailsInfo(int iCustomerID, int iPolicyID, int iPolicyVersionID)

        public DataSet GetPolicyNBSAmounts(int iCustomerID, int iPolicyID, int iPolicyVersionID, string strPolicy_Status)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "proc_GetPolicy_NBSAmount";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", iCustomerID);
                base.htGetDataParamCollections.Add("@POLICY_ID", iPolicyID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", iPolicyVersionID);
                base.htGetDataParamCollections.Add("@POLICY_STATUS", strPolicy_Status);

                ds = base.GetData();
            }
            catch { }
            finally { }
            return ds;

        }//Add New Discount Percent In user Discount Percent List
       //aDDED BY kULDEEP TO GET pOLICY pREMIUM AND pOLICY lEVEL cOMMISSION ON 9-FEB -2012
        public DataSet GetPolicyPremComm(int iCustomerID, int iPolicyID, int iPolicyVersionID)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "Proc_Get_Prem_Comm";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", iCustomerID);
                base.htGetDataParamCollections.Add("@POLICY_ID", iPolicyID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", iPolicyVersionID);
             
                ds = base.GetData();
            }
            catch { }
            finally { }
            return ds;

        }
        public int GenrateInstallments()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Add_Name = "Proc_InsertPolicyPremiumItems";
                base.ReturnIDName = "@RETVAL";

                #region N O T   S Q L   P R O C   P A R A M
                this.INSTALLMENT_NO.IsDBParam = false;
                this.INSTALLMENT_EFFECTIVE_DATE.IsDBParam = false;
                this.INSTALLMENT_AMOUNT.IsDBParam = false;
                this.INTEREST_AMOUNT.IsDBParam = false;
                this.FEE.IsDBParam = false;
                this.TAXES.IsDBParam = false;
                this.TOTAL.IsDBParam = false;
                this.TRAN_TYPE.IsDBParam = false;
                this.TRAN_PREMIUM_AMOUNT.IsDBParam = false;
                this.TRAN_INTEREST_AMOUNT.IsDBParam = false;
                this.TRAN_FEE.IsDBParam = false;
                this.TRAN_TAXES.IsDBParam = false;
                this.TRAN_TOTAL.IsDBParam = false;
                this.RELEASED_STATUS.IsDBParam = false;
                this.ENDO_NO.IsDBParam = false;
                this.RETVAL.IsDBParam = false;
                this.MODE_OF_PAYMENT.IsDBParam = false;
                this.MODE_OF_DOWN_PAYMENT.IsDBParam = false;
                base.MODIFIED_BY.IsDBParam = false;
                base.LAST_UPDATED_DATETIME.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;
                this.ROW_ID.IsDBParam = false;
                this.CO_APPLICANT_NAME.IsDBParam = false;
                this.CO_APPLICANT_ID.IsDBParam = false;
                this.BOLETO_NO.IsDBParam = false;//Added by Pradeep
                this.RECEIVED_DATE.IsDBParam = false;//Added by Lalit

                //Added by Pradeep on 18-Jan-2011
                this.INSTALLMENT_EXPIRE_DATE.IsDBParam = false;
                this.END_EFFECTIVE_DATE.IsDBParam = false;
                this.END_EXPIRY_DATE.IsDBParam = false;
                this.POLICY_EFFECTIVE_DATE.IsDBParam = false;
                this.POLICY_EXPIRATION_DATE.IsDBParam = false;
                //Added till here 
                ////Added by Kuldeep for TFS 3408

                //this.TOTAL_BEFORE_GST.IsDBParam = false;
                //this.TOTAL_AFTER_GST.IsDBParam = false;
                //this.GROSS_COMMISSION.IsDBParam = false;
                //this.GST_ON_COMMISSION.IsDBParam = false;
                //this.TOTAL_COMM_AFTER_GST.IsDBParam = false;
                //TILL HERE
                #endregion

                ProcReturnValue = true;

                base.TRANS_TYPE_ID = 222;                       //For Show installments has been genrated
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //  base.CUSTOM_INFO = Custommsg + "=5";

                returnResult = base.Save();
                returnResult = Proc_ReturnValue;

                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }



        }         //Add New Discount Percent In user Discount Percent List
        /// <summary>
        /// Use to add updated installment data in history table and also update installment details and plan data table
        /// </summary>
        /// <param name="ArobjClsPolicyBillingInfo"></param>
        /// <returns></returns>
        public int UpdateBoletoReprintInstallments(ArrayList objClsPolicyBillingInfoList)
        {
            StringBuilder sbTranXml = new StringBuilder();

            String ConnStr = EbixDataLayer.DataWrapper.ConnString = DBConnString;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            int retvalue = 0;
            try
            {
                if (objClsPolicyBillingInfoList.Count > 0)
                {


                    for (int i = 0; i < objClsPolicyBillingInfoList.Count; i++)
                    {
                        ClsBillingDetailsInfo objBillingInfo = new ClsBillingDetailsInfo();
                        objBillingInfo = (ClsBillingDetailsInfo)objClsPolicyBillingInfoList[i];

                        //sbTranXml.Append("<root>");
                        string strTranXML = "";
                        objBillingInfo.TransactLabel = this.TransactLabel;
                        objBillingInfo.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                        objBillingInfo.POLICYID = POLICY_ID.CurrentValue;
                        objBillingInfo.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                        objBillingInfo.RECORDED_BY = MODIFIED_BY.CurrentValue;

                        objBillingInfo.RequiredTransactionLog = false;
                        if (objBillingInfo.ACTION == "U")
                        {
                            strTranXML = "";
                            objDataWrapper.ClearParameteres();

                            SaveUpdateInstallmentDetailsHistory(objBillingInfo, objDataWrapper, out strTranXML);

                            if (strTranXML != "" && strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                            {
                                strTranXML = this.TotalNodesRemove(strTranXML);
                                sbTranXml.Append(strTranXML);
                            }

                        }
                        #region Commented on 19-Jan-2011 not to update Plan Data
                        //strTranXML = "";
                        //objDataWrapper.ClearParameteres();
                        //UpdatePlanDataForBoletoReprint(objBillingInfo, Proc_Update_Name, objDataWrapper, out strTranXML);
                        //if (strTranXML != "" && strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                        //{
                        //    strTranXML = this.InstallmentNodesRemove(strTranXML);
                        //    sbTranXml.Append(strTranXML);
                        //}
                        #endregion
                        //objBillingInfo.Dispose();
                        //if (i == objClsPolicyBillingInfoList.Count - 1)
                        //{
                        //    ClsPolicyBillingInfo objBillingInfoo = new ClsPolicyBillingInfo();
                        //    objBillingInfoo = (ClsPolicyBillingInfo)objClsPolicyBillingInfoList[i];
                        //    strTranXML = "";
                        //    objDataWrapper.ClearParameteres();
                        //    UpdatePlanDataForBoletoReprint(objBillingInfoo, Proc_Update_Name, objDataWrapper, out strTranXML);
                        //    if (strTranXML != "" && strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                        //    {
                        //        strTranXML = this.InstallmentNodesRemove(strTranXML);
                        //        sbTranXml.Append(strTranXML);
                        //    }
                        //}
                        objBillingInfo.Dispose();
                        //else
                        //{
                        //    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                        //    throw (new Exception("Error: DB Action not set for any Model object."));
                        //}

                    }

                    //sbTranXml.Append("</root>");
                    if (sbTranXml.ToString() != "")// || strCustomInfo!="")
                    {
                        string Txml = "<root>" + sbTranXml.ToString() + "</root>";
                        objDataWrapper.ClearParameteres();               //Type Id=223 For Installemnts Information Updated
                        int Tranreturnval = this.SaveTransaction(objDataWrapper, Txml.ToString(), 223);
                    }
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                    retvalue = 1;
                }
            }
            catch (Exception ex)
            {
                //retvalue = -1;
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while saving record.", ex.InnerException));

            }
            finally { objDataWrapper.Dispose(); }
            return retvalue;
        }

        public int UpdateInstallments(ArrayList ArobjClsPolicyBillingInfo)
        {
            StringBuilder sbTranXml = new StringBuilder();

            String ConnStr = EbixDataLayer.DataWrapper.ConnString = DBConnString;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            int retvalue = 0;
            try
            {
                if (ArobjClsPolicyBillingInfo.Count > 0)
                {


                    for (int i = 0; i < ArobjClsPolicyBillingInfo.Count; i++)
                    {
                        ClsBillingDetailsInfo objBillingInfo = new ClsBillingDetailsInfo();
                        objBillingInfo = (ClsBillingDetailsInfo)ArobjClsPolicyBillingInfo[i];

                        //sbTranXml.Append("<root>");
                        string strTranXML = "";
                        objBillingInfo.TransactLabel = this.TransactLabel;
                        objBillingInfo.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                        objBillingInfo.POLICYID = POLICY_ID.CurrentValue;
                        objBillingInfo.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                        objBillingInfo.RECORDED_BY = MODIFIED_BY.CurrentValue;

                        objBillingInfo.RequiredTransactionLog = false;
                        if (objBillingInfo.ACTION == "U")
                        {
                            strTranXML = "";
                            objDataWrapper.ClearParameteres();
                            UpdateInstallment(objBillingInfo, Proc_Update_Name, objDataWrapper, out strTranXML);
                            if (strTranXML != "" && strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                            {
                                strTranXML = this.TotalNodesRemove(strTranXML);
                                sbTranXml.Append(strTranXML);
                            }

                        }
                        objBillingInfo.Dispose();
                        if (i == ArobjClsPolicyBillingInfo.Count - 1)
                        {
                            ClsBillingDetailsInfo objBillingInfoo = new ClsBillingDetailsInfo();
                            objBillingInfoo = (ClsBillingDetailsInfo)ArobjClsPolicyBillingInfo[i];
                            strTranXML = "";
                            objDataWrapper.ClearParameteres();
                            UpdatePlanData(objBillingInfoo, Proc_Update_Name, objDataWrapper, out strTranXML);
                            if (strTranXML != "" && strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                            {
                                strTranXML = this.InstallmentNodesRemove(strTranXML);
                                sbTranXml.Append(strTranXML);
                            }
                        }
                        //else
                        //{
                        //    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                        //    throw (new Exception("Error: DB Action not set for any Model object."));
                        //}

                    }

                    //sbTranXml.Append("</root>");
                    if (sbTranXml.ToString() != "")// || strCustomInfo!="")
                    {
                        string Txml = "<root>" + sbTranXml.ToString() + "</root>";
                        objDataWrapper.ClearParameteres();               //Type Id=223 For Installemnts Information Updated
                        int Tranreturnval = this.SaveTransaction(objDataWrapper, Txml.ToString(), 223);
                    }
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                    retvalue = 1;
                }
            }
            catch (Exception ex)
            {
                //retvalue = -1;
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while saving record.", ex.InnerException));

            }
            finally { objDataWrapper.Dispose(); }
            return retvalue;
        }

        private int SaveUpdateInstallmentDetailsHistory(ClsBillingDetailsInfo objInfo, DataWrapper objDataWrapper, out string strTranXML)
        {

            int returnResult = 0;

            objInfo.Proc_Update_Name = "Proc_UpdatePolicyBillingInstallmentHistory";

            #region N O T   S Q L   P R O C   P A R A M

            objInfo.TOTAL_INTEREST_AMOUNT.IsDBParam = false;
            objInfo.TOTAL_FEES.IsDBParam = false;
            objInfo.TOTAL_TAXES.IsDBParam = false;
            objInfo.TOTAL_AMOUNT.IsDBParam = false;
            objInfo.PLAN_ID.IsDBParam = false;
            objInfo.RETVAL.IsDBParam = false;
            objInfo.RELEASED_STATUS.IsDBParam = false;
            objInfo.TOTAL_CHANGE_INFORCE_PRM.IsDBParam = false;
            objInfo.PRM_DIST_TYPE.IsDBParam = false;
            objInfo.CREATED_BY.IsDBParam = false;
            objInfo.CREATED_DATETIME.IsDBParam = false;
            objInfo.TOTAL_INFO_PRM.IsDBParam = false;
            objInfo.ENDO_NO.IsDBParam = false;
            objInfo.TOTAL_TRAN_INTEREST_AMOUNT.IsDBParam = false;
            objInfo.TOTAL_TRAN_FEES.IsDBParam = false;
            objInfo.TOTAL_TRAN_TAXES.IsDBParam = false;
            objInfo.TOTAL_TRAN_AMOUNT.IsDBParam = false;
            objInfo.IS_ACTIVE.IsDBParam = false;
            objInfo.TOTAL_STATE_FEES.IsDBParam = false;
            objInfo.TOTAL_TRAN_STATE_FEES.IsDBParam = false;
            objInfo.MODE_OF_PAYMENT.IsDBParam = false;
            objInfo.MODE_OF_DOWN_PAYMENT.IsDBParam = false;
            objInfo.CO_APPLICANT_NAME.IsDBParam = false;
            objInfo.CO_APPLICANT_ID.IsDBParam = false;
            objInfo.BOLETO_NO.IsDBParam = false;


            objInfo.INSTALLMENT_AMOUNT.IsDBParam = false;
            objInfo.FEE.IsDBParam = false;
            objInfo.TAXES.IsDBParam = false;
            objInfo.TRAN_PREMIUM_AMOUNT.IsDBParam = false;
            objInfo.TRAN_INTEREST_AMOUNT.IsDBParam = false;
            objInfo.TRAN_FEE.IsDBParam = false;
            objInfo.TRAN_TAXES.IsDBParam = false;
            objInfo.TRAN_TOTAL.IsDBParam = false;
            objInfo.TRAN_TYPE.IsDBParam = false;
            objInfo.TOTAL_TRAN_PREMIUM.IsDBParam = false;
            objInfo.TOTAL_PREMIUM.IsDBParam = false;
            objInfo.RECEIVED_DATE.IsDBParam = false;//Added by Lalit

            //Added by Pradeep on 18-Jan-2011

            //objInfo.INSTALLMENT_EXPIRE_DATE.IsDBParam = false;
            objInfo.END_EFFECTIVE_DATE.IsDBParam = false;
            objInfo.END_EXPIRY_DATE.IsDBParam = false;
            objInfo.POLICY_EFFECTIVE_DATE.IsDBParam = false;
            objInfo.POLICY_EXPIRATION_DATE.IsDBParam = false;
            //Added till here 
            objInfo.INTEREST_AMOUNT.IsDBParam = false;
            objInfo.TOTAL.IsDBParam = false;

            #endregion

            ProcReturnValue = true;

            returnResult = objInfo.Update(objDataWrapper);
            returnResult = Proc_ReturnValue;
            strTranXML = objInfo.GenerateTransactionLogXML_New(false);

            return returnResult;
        }

        private int UpdateInstallment(ClsBillingDetailsInfo objInfo, string ProcName, DataWrapper objDataWrapper, out string strTranXML)
        {

            int returnResult = 0;

            objInfo.Proc_Update_Name = "Proc_UpdatePolicyBillingInstallment";

            #region N O T   S Q L   P R O C   P A R A M
            objInfo.TOTAL.IsDBParam = false;
            objInfo.TOTAL_INTEREST_AMOUNT.IsDBParam = false;
            objInfo.TOTAL_FEES.IsDBParam = false;
            objInfo.TOTAL_TAXES.IsDBParam = false;
            objInfo.TOTAL_AMOUNT.IsDBParam = false;
            objInfo.PLAN_ID.IsDBParam = false;
            objInfo.RETVAL.IsDBParam = false;
            objInfo.RELEASED_STATUS.IsDBParam = false;
            objInfo.TOTAL_CHANGE_INFORCE_PRM.IsDBParam = false;
            objInfo.PRM_DIST_TYPE.IsDBParam = false;
            objInfo.CREATED_BY.IsDBParam = false;
            objInfo.CREATED_DATETIME.IsDBParam = false;
            objInfo.TOTAL_INFO_PRM.IsDBParam = false;
            objInfo.ENDO_NO.IsDBParam = false;
            objInfo.TOTAL_TRAN_INTEREST_AMOUNT.IsDBParam = false;
            objInfo.TOTAL_TRAN_FEES.IsDBParam = false;
            objInfo.TOTAL_TRAN_TAXES.IsDBParam = false;
            objInfo.TOTAL_TRAN_AMOUNT.IsDBParam = false;
            objInfo.IS_ACTIVE.IsDBParam = false;
            objInfo.TOTAL_STATE_FEES.IsDBParam = false;
            objInfo.TOTAL_TRAN_STATE_FEES.IsDBParam = false;
            objInfo.MODE_OF_PAYMENT.IsDBParam = false;
            objInfo.MODE_OF_DOWN_PAYMENT.IsDBParam = false;
            //objInfo.ROW_ID.IsDBParam = false;
            objInfo.CO_APPLICANT_NAME.IsDBParam = false;
            objInfo.CO_APPLICANT_ID.IsDBParam = false;
            objInfo.BOLETO_NO.IsDBParam = false;//Added by Pradeep
            objInfo.RECEIVED_DATE.IsDBParam = false;//Added by Lalit

            //Added by Pradeep on 18-Jan-2011
            objInfo.INSTALLMENT_EXPIRE_DATE.IsDBParam = false;
            objInfo.END_EFFECTIVE_DATE.IsDBParam = false;
            objInfo.END_EXPIRY_DATE.IsDBParam = false;
            objInfo.POLICY_EFFECTIVE_DATE.IsDBParam = false;
            objInfo.POLICY_EXPIRATION_DATE.IsDBParam = false;
            //Added till here 

            #endregion

            ProcReturnValue = true;

            returnResult = objInfo.Update(objDataWrapper);
            returnResult = Proc_ReturnValue;
            strTranXML = objInfo.GenerateTransactionLogXML_New(false);

            return returnResult;
        }
        private int UpdatePlanDataForBoletoReprint(ClsBillingDetailsInfo objInfo, string ProcName, DataWrapper objDataWrapper, out string strTranXML)
        {
            int returnResult = 0;

            objInfo.Proc_Update_Name = "Proc_UpdateInstall_Plan_Data_BoletoReprint";

            #region N O T   S Q L   P R O C   P A R A M
            objInfo.INSTALLMENT_AMOUNT.IsDBParam = false;
            objInfo.INTEREST_AMOUNT.IsDBParam = false;
            objInfo.FEE.IsDBParam = false;
            objInfo.TAXES.IsDBParam = false;
            objInfo.TOTAL.IsDBParam = false;
            objInfo.INSTALLMENT_EFFECTIVE_DATE.IsDBParam = false;
            objInfo.INSTALLMENT_NO.IsDBParam = false;
            objInfo.RETVAL.IsDBParam = false;
            objInfo.TRAN_TYPE.IsDBParam = false;
            objInfo.TRAN_PREMIUM_AMOUNT.IsDBParam = false;
            objInfo.TRAN_INTEREST_AMOUNT.IsDBParam = false;
            objInfo.TRAN_FEE.IsDBParam = false;
            objInfo.TRAN_TAXES.IsDBParam = false;
            objInfo.TRAN_TOTAL.IsDBParam = false;
            objInfo.RELEASED_STATUS.IsDBParam = false;
            objInfo.ENDO_NO.IsDBParam = false;
            objInfo.CREATED_BY.IsDBParam = false;
            objInfo.CREATED_DATETIME.IsDBParam = false;
            objInfo.PRM_DIST_TYPE.IsDBParam = false;
            objInfo.TOTAL_INFO_PRM.IsDBParam = false;
            objInfo.IS_ACTIVE.IsDBParam = false;
            objInfo.MODE_OF_PAYMENT.IsDBParam = false;
            objInfo.MODE_OF_DOWN_PAYMENT.IsDBParam = false;
            objInfo.CO_APPLICANT_NAME.IsDBParam = false;
            objInfo.ROW_ID.IsDBParam = false;
            objInfo.CO_APPLICANT_ID.IsDBParam = false;
            objInfo.BOLETO_NO.IsDBParam = false;//Added by Pradeep
            objInfo.RECEIVED_DATE.IsDBParam = false;//Added by Lalit
            //Added by Pradeep on 18-Jan-2011
            objInfo.INSTALLMENT_EXPIRE_DATE.IsDBParam = false;
            objInfo.END_EFFECTIVE_DATE.IsDBParam = false;
            objInfo.END_EXPIRY_DATE.IsDBParam = false;
            objInfo.POLICY_EFFECTIVE_DATE.IsDBParam = false;
            objInfo.POLICY_EXPIRATION_DATE.IsDBParam = false;
            //Added till here 
            #endregion

            #region A L L O W   S Q L   P R O C  P A R A M
            objInfo.TOTAL_TRAN_PREMIUM.IsDBParam = false;
            objInfo.TOTAL_TRAN_INTEREST_AMOUNT.IsDBParam = false;
            objInfo.TOTAL_TRAN_FEES.IsDBParam = false;
            objInfo.TOTAL_TRAN_TAXES.IsDBParam = false;
            objInfo.TOTAL_TRAN_AMOUNT.IsDBParam = false;
            objInfo.TOTAL_PREMIUM.IsDBParam = false;
            objInfo.TOTAL_INTEREST_AMOUNT.IsDBParam = true;
            objInfo.TOTAL_FEES.IsDBParam = false;
            objInfo.TOTAL_TAXES.IsDBParam = false;
            objInfo.TOTAL_AMOUNT.IsDBParam = true;
            objInfo.PLAN_ID.IsDBParam = false;
            objInfo.TOTAL_CHANGE_INFORCE_PRM.IsDBParam = false;
            objInfo.TOTAL_TRAN_STATE_FEES.IsDBParam = false;
            objInfo.TOTAL_STATE_FEES.IsDBParam = false;
            #endregion

            ProcReturnValue = true;
            returnResult = objInfo.Update(objDataWrapper);
            strTranXML = objInfo.GenerateTransactionLogXML_New(false);

            returnResult = Proc_ReturnValue;
            return returnResult;

        }
        private int UpdatePlanData(ClsBillingDetailsInfo objInfo, string ProcName, DataWrapper objDataWrapper, out string strTranXML)
        {
            int returnResult = 0;

            objInfo.Proc_Update_Name = "Proc_UpdateInstall_Plan_Data";

            #region N O T   S Q L   P R O C   P A R A M
            objInfo.INSTALLMENT_AMOUNT.IsDBParam = false;
            objInfo.INTEREST_AMOUNT.IsDBParam = false;
            objInfo.FEE.IsDBParam = false;
            objInfo.TAXES.IsDBParam = false;
            objInfo.TOTAL.IsDBParam = false;
            objInfo.INSTALLMENT_EFFECTIVE_DATE.IsDBParam = false;
            objInfo.INSTALLMENT_NO.IsDBParam = false;
            objInfo.RETVAL.IsDBParam = false;
            objInfo.TRAN_TYPE.IsDBParam = false;
            objInfo.TRAN_PREMIUM_AMOUNT.IsDBParam = false;
            objInfo.TRAN_INTEREST_AMOUNT.IsDBParam = false;
            objInfo.TRAN_FEE.IsDBParam = false;
            objInfo.TRAN_TAXES.IsDBParam = false;
            objInfo.TRAN_TOTAL.IsDBParam = false;
            objInfo.RELEASED_STATUS.IsDBParam = false;
            objInfo.ENDO_NO.IsDBParam = false;
            objInfo.CREATED_BY.IsDBParam = false;
            objInfo.CREATED_DATETIME.IsDBParam = false;
            objInfo.PRM_DIST_TYPE.IsDBParam = false;
            objInfo.TOTAL_INFO_PRM.IsDBParam = false;
            objInfo.IS_ACTIVE.IsDBParam = false;
            objInfo.MODE_OF_PAYMENT.IsDBParam = false;
            objInfo.MODE_OF_DOWN_PAYMENT.IsDBParam = false;
            objInfo.CO_APPLICANT_NAME.IsDBParam = false;
            objInfo.ROW_ID.IsDBParam = false;
            objInfo.CO_APPLICANT_ID.IsDBParam = false;
            objInfo.BOLETO_NO.IsDBParam = false;//Added by Pradeep
            objInfo.RECEIVED_DATE.IsDBParam = false;//Added by Lalit
            //Added by Pradeep on 18-Jan-2011
            objInfo.INSTALLMENT_EXPIRE_DATE.IsDBParam = false;
            objInfo.END_EFFECTIVE_DATE.IsDBParam = false;
            objInfo.END_EXPIRY_DATE.IsDBParam = false;
            objInfo.POLICY_EFFECTIVE_DATE.IsDBParam = false;
            objInfo.POLICY_EXPIRATION_DATE.IsDBParam = false;
            //Added till here 

         
           
            #endregion

            #region A L L O W   S Q L   P R O C  P A R A M
            objInfo.TOTAL_TRAN_PREMIUM.IsDBParam = true;
            objInfo.TOTAL_TRAN_INTEREST_AMOUNT.IsDBParam = true;
            objInfo.TOTAL_TRAN_FEES.IsDBParam = true;
            objInfo.TOTAL_TRAN_TAXES.IsDBParam = true;
            objInfo.TOTAL_TRAN_AMOUNT.IsDBParam = true;
            objInfo.TOTAL_PREMIUM.IsDBParam = true;
            objInfo.TOTAL_INTEREST_AMOUNT.IsDBParam = true;
            objInfo.TOTAL_FEES.IsDBParam = true;
            objInfo.TOTAL_TAXES.IsDBParam = true;
            objInfo.TOTAL_AMOUNT.IsDBParam = true;
            objInfo.PLAN_ID.IsDBParam = true;
            objInfo.TOTAL_CHANGE_INFORCE_PRM.IsDBParam = true;
            objInfo.TOTAL_TRAN_STATE_FEES.IsDBParam = true;
            objInfo.TOTAL_STATE_FEES.IsDBParam = true;
            #endregion

            ProcReturnValue = true;
            returnResult = objInfo.Update(objDataWrapper);
            strTranXML = objInfo.GenerateTransactionLogXML_New(false);

            returnResult = Proc_ReturnValue;
            return returnResult;

        }

        public DataSet GetCoveragesPremium(int iCustomerId, int iPolicyId, int iPolicyVersionId, int iPolicyLob)
        {
            DataSet ds = null;
            base.Proc_FetchData = "Proc_GetPolicyCoveragePremiumSum";
            base.htGetDataParamCollections.Clear();
            base.htGetDataParamCollections.Add("@CUSTOMER_ID", iCustomerId);
            base.htGetDataParamCollections.Add("@POLICY_ID", iPolicyId);
            base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", iPolicyVersionId);
            base.htGetDataParamCollections.Add("@LOB_ID", iPolicyLob);
            ds = base.GetData();
            return ds;
        }

        public int SaveTransaction(DataWrapper objDataWrapper, string TranXML, int transType_ID)
        {
            int returnval = 0;
            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

            objTransactionInfo.TRANS_TYPE_ID = transType_ID;
            objTransactionInfo.CHANGE_XML = TranXML;
            objTransactionInfo.CLIENT_ID = CUSTOMER_ID.CurrentValue;
            objTransactionInfo.POLICY_ID = POLICY_ID.CurrentValue;
            objTransactionInfo.POLICY_VER_TRACKING_ID = POLICY_VERSION_ID.CurrentValue;
            objTransactionInfo.RECORDED_BY = MODIFIED_BY.CurrentValue;
            objTransactionInfo.TRANS_DESC = "";

            returnval = base.MaintainTrans(objDataWrapper, objTransactionInfo);
            return returnval;

        }

        private string InstallmentNodesRemove(string xml)
        {

            XmlDocument xmldoc = new XmlDocument();

            xmldoc.LoadXml(xml);

            foreach (XmlNode node in xmldoc.SelectNodes("LabelFieldMapping"))
            {
                for (int i = node.ChildNodes.Count - 1; i >= 0; i--)
                {
                    if (node.ChildNodes[i].Attributes["field"].Value == "INSTALLMENT_DATE") { xmldoc.DocumentElement.RemoveChild(node.ChildNodes[i]); }
                    else if (node.ChildNodes[i].Attributes["field"].Value == "PREMIUM") { xmldoc.DocumentElement.RemoveChild(node.ChildNodes[i]); }
                    else if (node.ChildNodes[i].Attributes["field"].Value == "INTEREST_AMOUNT") { xmldoc.DocumentElement.RemoveChild(node.ChildNodes[i]); }
                    else if (node.ChildNodes[i].Attributes["field"].Value == "FEE") { xmldoc.DocumentElement.RemoveChild(node.ChildNodes[i]); }
                    else if (node.ChildNodes[i].Attributes["field"].Value == "TAXES") { xmldoc.DocumentElement.RemoveChild(node.ChildNodes[i]); }
                    else if (node.ChildNodes[i].Attributes["field"].Value == "TOTAL") { xmldoc.DocumentElement.RemoveChild(node.ChildNodes[i]); }
                    else if (node.ChildNodes[i].Attributes["field"].Value == "INSTALLMENT_NO") { xmldoc.DocumentElement.RemoveChild(node.ChildNodes[i]); }
                    else if (node.ChildNodes[i].Attributes["field"].Value == "TRAN_PREMIUM_AMOUNT") { xmldoc.DocumentElement.RemoveChild(node.ChildNodes[i]); }
                    else if (node.ChildNodes[i].Attributes["field"].Value == "TRAN_INTEREST_AMOUNT") { xmldoc.DocumentElement.RemoveChild(node.ChildNodes[i]); }
                    else if (node.ChildNodes[i].Attributes["field"].Value == "TRAN_FEE") { xmldoc.DocumentElement.RemoveChild(node.ChildNodes[i]); }
                    else if (node.ChildNodes[i].Attributes["field"].Value == "TRAN_TAXES") { xmldoc.DocumentElement.RemoveChild(node.ChildNodes[i]); }
                    else if (node.ChildNodes[i].Attributes["field"].Value == "INSTALLMENT_EFFECTIVE_DATE") { xmldoc.DocumentElement.RemoveChild(node.ChildNodes[i]); }

                }
            }


            return xmldoc.InnerXml.ToString();

        }

        private string TotalNodesRemove(string xml)
        {

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);
            XmlNodeList nodeList = xmldoc.GetElementsByTagName("LabelFieldMapping");
            foreach (XmlNode prnode in nodeList)
            {
                for (int i = prnode.ChildNodes.Count - 1; i >= 0; i--)
                {

                    if (prnode.ChildNodes[i].Attributes["field"].Value == "TOTAL_INTEREST_AMOUNT") { xmldoc.DocumentElement.RemoveChild(prnode.ChildNodes[i]); }
                    else if (prnode.ChildNodes[i].Attributes["field"].Value == "TOTAL_FEES") { xmldoc.DocumentElement.RemoveChild(prnode.ChildNodes[i]); }
                    else if (prnode.ChildNodes[i].Attributes["field"].Value == "TOTAL_TAXES") { xmldoc.DocumentElement.RemoveChild(prnode.ChildNodes[i]); }
                    else if (prnode.ChildNodes[i].Attributes["field"].Value == "TOTAL_AMOUNT") { xmldoc.DocumentElement.RemoveChild(prnode.ChildNodes[i]); }
                    else if (prnode.ChildNodes[i].Attributes["field"].Value == "BILLING_PLAN") { xmldoc.DocumentElement.RemoveChild(prnode.ChildNodes[i]); }
                    else if (prnode.ChildNodes[i].Attributes["field"].Value == "TOTAL_TRAN_PREMIUM") { xmldoc.DocumentElement.RemoveChild(prnode.ChildNodes[i]); }
                    else if (prnode.ChildNodes[i].Attributes["field"].Value == "TOTAL_TRAN_INTEREST_AMOUNT") { xmldoc.DocumentElement.RemoveChild(prnode.ChildNodes[i]); }
                    else if (prnode.ChildNodes[i].Attributes["field"].Value == "TOTAL_TRAN_FEES") { xmldoc.DocumentElement.RemoveChild(prnode.ChildNodes[i]); }
                    else if (prnode.ChildNodes[i].Attributes["field"].Value == "TOTAL_TRAN_TAXES") { xmldoc.DocumentElement.RemoveChild(prnode.ChildNodes[i]); }
                    else if (prnode.ChildNodes[i].Attributes["field"].Value == "TOTAL_TRAN_AMOUNT") { xmldoc.DocumentElement.RemoveChild(prnode.ChildNodes[i]); }
                }



            }

            return xmldoc.InnerXml.ToString();


        }

        public DataSet GetPolicy_ProcessStatus(int iCustomerId, int iPolicyId, int iPolicyVersionId)
        {
            DataSet ds = null;

            base.Proc_FetchData = "Proc_GetPolicyProcessStatus";
            base.htGetDataParamCollections.Clear();
            base.htGetDataParamCollections.Add("@CUSTOMER_ID", iCustomerId);
            base.htGetDataParamCollections.Add("@POLICY_ID", iPolicyId);
            base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", iPolicyVersionId);
            ds = base.GetData();

            return ds;
        }

        public DataSet GetInstallmentsDetailsNew(int iCustomerId, int iPolicyId, int iPolicyVersionId)
        {
            DataSet ds = null;

            base.Proc_FetchData = "PROC_GET_INSTALLMENT_DETAILS";
            base.htGetDataParamCollections.Clear();
            base.htGetDataParamCollections.Add("@CUSTOMER_ID", iCustomerId);
            base.htGetDataParamCollections.Add("@POLICY_ID", iPolicyId);
            base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", iPolicyVersionId);
            ds = base.GetData();

            return ds;
        }

        public int Check_InstallGenstatus(int iPolicyId, int iCustomerId, int iPolicyVersionId)
        {
            DataSet ds;
            int PlanId = 0;
            base.Proc_FetchData = "Proc_CheckInstallPlanData";
            base.htGetDataParamCollections.Clear();
            base.htGetDataParamCollections.Add("@CUSTOMER_ID", iCustomerId);
            base.htGetDataParamCollections.Add("@POLICY_ID", iPolicyId);
            base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", iPolicyVersionId);
            base.htGetDataParamCollections.Add("@CALLED_FOR", "INSTALLMENTS");
            ds = base.GetData();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                PlanId = int.Parse(ds.Tables[0].Rows[0][0].ToString());

            return PlanId;
        }

        public int GenrateMasterPolicyInstallments()
        {

            int returnResult = 0;
            try
            {
                base.Proc_Add_Name = "Proc_Generate_MasterPolicyInstallments";
                base.ReturnIDName = "@RETVAL";

                #region SQL Parameters
                this.CUSTOMER_ID.IsDBParam = true;
                this.POLICY_ID.IsDBParam = true;
                this.POLICY_VERSION_ID.IsDBParam = true;
                this.PLAN_ID.IsDBParam = false;
                #endregion

                #region N O T   S Q L   P R O C   P A R A M
                this.TOTAL_PREMIUM.IsDBParam = false;
                this.TOTAL_INTEREST_AMOUNT.IsDBParam = false;
                this.TOTAL_FEES.IsDBParam = false;
                this.TOTAL_TAXES.IsDBParam = false;
                this.TOTAL_AMOUNT.IsDBParam = false;
                this.INSTALLMENT_AMOUNT.IsDBParam = false;
                this.INTEREST_AMOUNT.IsDBParam = false;
                this.FEE.IsDBParam = false;
                this.TAXES.IsDBParam = false;
                this.TOTAL.IsDBParam = false;
                this.INSTALLMENT_EFFECTIVE_DATE.IsDBParam = false;
                this.INSTALLMENT_NO.IsDBParam = false;
                this.RETVAL.IsDBParam = false;
                this.TRAN_TYPE.IsDBParam = false;
                this.TRAN_PREMIUM_AMOUNT.IsDBParam = false;
                this.TRAN_INTEREST_AMOUNT.IsDBParam = false;
                this.TRAN_FEE.IsDBParam = false;
                this.TRAN_TAXES.IsDBParam = false;
                this.TRAN_TOTAL.IsDBParam = false;
                this.RELEASED_STATUS.IsDBParam = false;
                this.TOTAL_TRAN_PREMIUM.IsDBParam = false;
                this.TOTAL_TRAN_INTEREST_AMOUNT.IsDBParam = false;
                this.TOTAL_TRAN_FEES.IsDBParam = false;
                this.TOTAL_TRAN_TAXES.IsDBParam = false;
                this.TOTAL_TRAN_AMOUNT.IsDBParam = false;
                this.TOTAL_CHANGE_INFORCE_PRM.IsDBParam = false;
                this.TOTAL_INFO_PRM.IsDBParam = false;
                this.TOTAL_STATE_FEES.IsDBParam = false;
                this.TOTAL_TRAN_STATE_FEES.IsDBParam = false;
                this.PRM_DIST_TYPE.IsDBParam = false;
                this.MODE_OF_PAYMENT.IsDBParam = false;
                this.MODE_OF_DOWN_PAYMENT.IsDBParam = false;
                this.ENDO_NO.IsDBParam = false;
                this.CO_APPLICANT_NAME.IsDBParam = false;
                this.ROW_ID.IsDBParam = false;
                this.CO_APPLICANT_ID.IsDBParam = false;
                this.BOLETO_NO.IsDBParam = false;//Added by Pradeep
                this.RECEIVED_DATE.IsDBParam = false;//Added by Lalit

                //Added by Pradeep on 18-Jan-2011
                this.INSTALLMENT_EXPIRE_DATE.IsDBParam = false;
                this.END_EFFECTIVE_DATE.IsDBParam = false;
                this.END_EXPIRY_DATE.IsDBParam = false;
                this.POLICY_EFFECTIVE_DATE.IsDBParam = false;
                this.POLICY_EXPIRATION_DATE.IsDBParam = false;
                //Added till here 

                #endregion

                ProcReturnValue = true;//changed by praveer for itrack no 1761
                base.TRANS_TYPE_ID = 222;                       //For Show installments has been genrated
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                base.MODIFIED_BY.IsDBParam = false;
                base.LAST_UPDATED_DATETIME.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;
                //  base.CUSTOM_INFO = Custommsg + "=5";

                returnResult = base.Save();
                returnResult = Proc_ReturnValue;//changed by praveer for itrack no 1761

                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        public decimal GetMasterPolicyPremium(int iCustomerId, int iPolicyId, int iPolicyVersionId, int CreatedBy, int PlanId, string CalledFrom)
        {
            String ConnStr = EbixDataLayer.DataWrapper.ConnString = DBConnString;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            //base.Proc_FetchData = "Proc_GetPolicyCoveragePremiumSum";
            SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@TOTAL_RISK_PREMIUM", null, SqlDbType.Decimal, ParameterDirection.Output);

            int Ret = 0;
            decimal Premium = 0;
            objDataWrapper.AddParameter("@CUSTOMER_ID", iCustomerId);
            objDataWrapper.AddParameter("@POLICY_ID", iPolicyId);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", iPolicyVersionId);
            objDataWrapper.AddParameter("@CREATED_BY", CreatedBy);
            objDataWrapper.AddParameter("@CREATED_DATETIME", DateTime.Now);
            objDataWrapper.AddParameter("@PLAN_ID", PlanId);
            objDataWrapper.AddParameter("@CALLED_FROM", CalledFrom);
            Ret = objDataWrapper.ExecuteNonQuery("Proc_Generate_MasterPolicyInstallments");
            if (Ret > 0)
            {
                if (objSqlParameter.Value.ToString() != "")
                {
                    Premium = (decimal)objSqlParameter.Value;
                }
            } return Premium;

        }
    }

    //Save Complete Save/Update transaction

}

