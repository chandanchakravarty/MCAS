/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	25-oct-2010
<End Date			: -	
<Description		: -The Model is use to deal with  Deposit Details 
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: -  
<Modified By		: -  
<Purpose			: -  
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
using Cms.EbixDataLayer;

namespace Model.Account
{
    [Serializable]
    public class ClsAddDepositDetailsinfo : ClsModelBaseClass, IDisposable
    {
        public ClsAddDepositDetailsinfo()
        {
            this.PropertyCollection();
        }
        public void Dispose()
        {
            System.GC.ReRegisterForFinalize(this);
        }  
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);

            base.htPropertyCollection.Add("CD_LINE_ITEM_ID", CD_LINE_ITEM_ID);
            base.htPropertyCollection.Add("DEPOSIT_ID", DEPOSIT_ID);
            base.htPropertyCollection.Add("RISK_PREMIUM", RISK_PREMIUM);
            base.htPropertyCollection.Add("FEE", FEE);
            base.htPropertyCollection.Add("TAX", TAX);
            base.htPropertyCollection.Add("INTEREST", INTEREST);
            base.htPropertyCollection.Add("LATE_FEE", LATE_FEE);
            base.htPropertyCollection.Add("RECEIPT_AMOUNT", RECEIPT_AMOUNT);
            base.htPropertyCollection.Add("BOLETO_NO", BOLETO_NO);

            base.htPropertyCollection.Add("POLICY_NUMBER", POLICY_NUMBER);
            base.htPropertyCollection.Add("BANK_NUMBER", BANK_NUMBER);
            base.htPropertyCollection.Add("BANK_AGENCY_NUMBER", BANK_AGENCY_NUMBER);
            base.htPropertyCollection.Add("BANK_ACCOUNT_NUMBER", BANK_ACCOUNT_NUMBER);
            base.htPropertyCollection.Add("OUR_NUMBER", OUR_NUMBER);
            base.htPropertyCollection.Add("PAY_MODE", PAY_MODE);
            base.htPropertyCollection.Add("IS_EXCEPTION", IS_EXCEPTION);
            base.htPropertyCollection.Add ("CREATED_FROM", CREATED_FROM);
            base.htPropertyCollection.Add("PAGE_ID", PAGE_ID);
            base.htPropertyCollection.Add("IS_APPROVE", IS_APPROVE);

            base.htPropertyCollection.Add("RECEIPT_NUM", RECEIPT_NUM);
            base.htPropertyCollection.Add("RECEIPT_BRANCH_CITY", RECEIPT_BRANCH_CITY);
            base.htPropertyCollection.Add("RECEIPT_ISSUED_DATE", RECEIPT_ISSUED_DATE);
            base.htPropertyCollection.Add("PAYMENT_DATE", PAYMENT_DATE);
            base.htPropertyCollection.Add("EXCEPTION_REASON", EXCEPTION_REASON);
            base.htPropertyCollection.Add("POLICY_STATUS", POLICY_STATUS);
            base.htPropertyCollection.Add("DEPOSIT_NUMBER", DEPOSIT_NUMBER);
            base.htPropertyCollection.Add("DEPOSIT_TYPE", DEPOSIT_TYPE);

            base.htPropertyCollection.Add("COINS_CARRIER_LEADER", COINS_CARRIER_LEADER);
            base.htPropertyCollection.Add("POLICY_HOLDER_NAME", POLICY_HOLDER_NAME);
            base.htPropertyCollection.Add("SUSEP_CLASS_OF_BUSINESS", SUSEP_CLASS_OF_BUSINESS);
            base.htPropertyCollection.Add("LEADER_POLICY_ID", LEADER_POLICY_ID);
            base.htPropertyCollection.Add("LEADER_DOC_ID", LEADER_DOC_ID);
            base.htPropertyCollection.Add("BRANCH_COINS_ID", BRANCH_COINS_ID);
            base.htPropertyCollection.Add("COINSURANCE_ID", COINSURANCE_ID);
            base.htPropertyCollection.Add("INSTALLMENT_NO", INSTALLMENT_NO);
            base.htPropertyCollection.Add("NO_OF_INSTALLMENTS", NO_OF_INSTALLMENTS);
            base.htPropertyCollection.Add("COMMISSION_AMOUNT", COMMISSION_AMOUNT);
            
            base.htPropertyCollection.Add("TOTAL_PREMIUM_COLLECTION", TOTAL_PREMIUM_COLLECTION); //Added by Pradeep on 24-March-2011 itrack 913
            base.htPropertyCollection.Add("RECEIPT_FROM_ID", RECEIPT_FROM_ID);
            base.htPropertyCollection.Add("CALLED_FROM", CALLED_FROM);
            base.htPropertyCollection.Add("INCORRECT_OUR_NUMBER", INCORRECT_OUR_NUMBER);
            base.htPropertyCollection.Add("IS_COMMITED", IS_COMMITED);
            base.htPropertyCollection.Add("RECORD_ID", RECORD_ID);
            
            
        }
        #region Declare the Property for every data table columns


        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
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
        }//public EbixInt32 CUSTOMER_ID 

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
        }//public EbixInt32 POLICY_ID

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
        }//public EbixInt32 POLICY_VERSION_ID 

        public EbixInt32 CD_LINE_ITEM_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CD_LINE_ITEM_ID"]) == null ? new EbixInt32("CD_LINE_ITEM_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CD_LINE_ITEM_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CD_LINE_ITEM_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 CD_LINE_ITEM_ID
        
        public EbixInt32 DEPOSIT_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DEPOSIT_ID"]) == null ? new EbixInt32("DEPOSIT_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DEPOSIT_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DEPOSIT_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 DEPOSIT_ID
        
        public EbixDouble RISK_PREMIUM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RISK_PREMIUM"]) == null ? new EbixDouble("RISK_PREMIUM") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RISK_PREMIUM"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RISK_PREMIUM"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble RISK_PREMIUM

        public EbixDouble FEE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["FEE"]) == null ? new EbixDouble("FEE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["FEE"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["FEE"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble FEE

        public EbixDouble TAX
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TAX"]) == null ? new EbixDouble("TAX") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TAX"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TAX"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble TAX

        public EbixDouble INTEREST
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["INTEREST"]) == null ? new EbixDouble("INTEREST") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["INTEREST"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["INTEREST"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble INTEREST

        public EbixDouble LATE_FEE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LATE_FEE"]) == null ? new EbixDouble("LATE_FEE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LATE_FEE"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LATE_FEE"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble LATE_FEE

        public EbixDouble RECEIPT_AMOUNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RECEIPT_AMOUNT"]) == null ? new EbixDouble("RECEIPT_AMOUNT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RECEIPT_AMOUNT"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RECEIPT_AMOUNT"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble RECEIPT_AMOUNT

        public EbixInt32 BOLETO_NO
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BOLETO_NO"]) == null ? new EbixInt32("BOLETO_NO") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BOLETO_NO"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BOLETO_NO"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 BOLETO_NO 

        public EbixString POLICY_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_NUMBER"]) == null ? new EbixString("POLICY_NUMBER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_NUMBER"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_NUMBER"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixInt32 POLICY_NUMBER 

        public EbixString BANK_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BANK_NUMBER"]) == null ? new EbixString("BANK_NUMBER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BANK_NUMBER"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BANK_NUMBER"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixInt32 BANK_NUMBER 

        public EbixString BANK_AGENCY_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BANK_AGENCY_NUMBER"]) == null ? new EbixString("BANK_AGENCY_NUMBER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BANK_AGENCY_NUMBER"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BANK_AGENCY_NUMBER"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixInt32 BANK_AGENCY_NUMBER 

        public EbixString BANK_ACCOUNT_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BANK_ACCOUNT_NUMBER"]) == null ? new EbixString("BANK_ACCOUNT_NUMBER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BANK_ACCOUNT_NUMBER"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BANK_ACCOUNT_NUMBER"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixInt32 BANK_ACCOUNT_NUMBER 

        public EbixString OUR_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OUR_NUMBER"]) == null ? new EbixString("OUR_NUMBER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OUR_NUMBER"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OUR_NUMBER"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixInt32 OUR_NUMBER 

        public EbixInt32 PAY_MODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PAY_MODE"]) == null ? new EbixInt32("PAY_MODE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PAY_MODE"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PAY_MODE"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 PAY_MODE 

        public EbixString IS_EXCEPTION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_EXCEPTION"]) == null ? new EbixString("IS_EXCEPTION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_EXCEPTION"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_EXCEPTION"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixInt32 IS_EXCEPTION 

        public EbixString CREATED_FROM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CREATED_FROM"]) == null ? new EbixString("CREATED_FROM") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CREATED_FROM"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CREATED_FROM"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixInt32 CREATED_FROM 

        public EbixString PAGE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PAGE_ID"]) == null ? new EbixString("PAGE_ID") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PAGE_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PAGE_ID"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixInt32 PAGE_ID 

        public EbixString IS_APPROVE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_APPROVE"]) == null ? new EbixString("IS_APPROVE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_APPROVE"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_APPROVE"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixInt32 IS_APPROVE 

        public EbixString RECEIPT_NUM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RECEIPT_NUM"]) == null ? new EbixString("RECEIPT_NUM") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RECEIPT_NUM"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RECEIPT_NUM"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixInt32 RECEIPT_NUM 

        public EbixString RECEIPT_BRANCH_CITY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RECEIPT_BRANCH_CITY"]) == null ? new EbixString("RECEIPT_BRANCH_CITY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RECEIPT_BRANCH_CITY"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RECEIPT_BRANCH_CITY"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixInt32 RECEIPT_BRANCH_CITY
       
        public EbixDateTime RECEIPT_ISSUED_DATE
        {
            get { return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RECEIPT_ISSUED_DATE"]) == null ? new EbixDateTime("RECEIPT_ISSUED_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RECEIPT_ISSUED_DATE"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RECEIPT_ISSUED_DATE"]).CurrentValue = Convert.ToDateTime(value);

            }
        }//public EbixDateTime RECEIPT_ISSUED_DATE
        
        public EbixDateTime PAYMENT_DATE
        {
            get { return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["PAYMENT_DATE"]) == null ? new EbixDateTime("PAYMENT_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["PAYMENT_DATE"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["PAYMENT_DATE"]).CurrentValue = Convert.ToDateTime(value);

            }
        }//public EbixDateTime PAYMENT_DATE

        public EbixString POLICY_STATUS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_STATUS"]) == null ? new EbixString("POLICY_STATUS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_STATUS"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_STATUS"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixInt32 POLICY_STATUS 
        
        public EbixInt32 EXCEPTION_REASON
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["EXCEPTION_REASON"]) == null ? new EbixInt32("EXCEPTION_REASON") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["EXCEPTION_REASON"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["EXCEPTION_REASON"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 EXCEPTION_REASON 

        public EbixInt32 DEPOSIT_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DEPOSIT_NUMBER"]) == null ? new EbixInt32("DEPOSIT_NUMBER") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DEPOSIT_NUMBER"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DEPOSIT_NUMBER"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 DEPOSIT_NUMBER 

        public EbixString DEPOSIT_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DEPOSIT_TYPE"]) == null ? new EbixString("DEPOSIT_TYPE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DEPOSIT_TYPE"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DEPOSIT_TYPE"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixInt32 DEPOSIT_TYPE
        
        //Added Property for Co-Insurance  -by Pradeep Kushwaha on 10-Jan-2011

        public EbixString COINS_CARRIER_LEADER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COINS_CARRIER_LEADER"]) == null ? new EbixString("COINS_CARRIER_LEADER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COINS_CARRIER_LEADER"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COINS_CARRIER_LEADER"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString COINS_CARRIER_LEADER 

        public EbixString POLICY_HOLDER_NAME
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_HOLDER_NAME"]) == null ? new EbixString("POLICY_HOLDER_NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_HOLDER_NAME"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_HOLDER_NAME"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString POLICY_HOLDER_NAME

        public EbixString SUSEP_CLASS_OF_BUSINESS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SUSEP_CLASS_OF_BUSINESS"]) == null ? new EbixString("SUSEP_CLASS_OF_BUSINESS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SUSEP_CLASS_OF_BUSINESS"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SUSEP_CLASS_OF_BUSINESS"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString SUSEP_CLASS_OF_BUSINESS 

        public EbixString LEADER_POLICY_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_POLICY_ID"]) == null ? new EbixString("LEADER_POLICY_ID") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_POLICY_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_POLICY_ID"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString LEADER_POLICY_ID 

        public EbixString LEADER_DOC_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_DOC_ID"]) == null ? new EbixString("LEADER_DOC_ID") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_DOC_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_DOC_ID"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString LEADER_DOC_ID 

        public EbixString BRANCH_COINS_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BRANCH_COINS_ID"]) == null ? new EbixString("BRANCH_COINS_ID") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BRANCH_COINS_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BRANCH_COINS_ID"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString BRANCH_COINS_ID 

        public EbixString COINSURANCE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COINSURANCE_ID"]) == null ? new EbixString("COINSURANCE_ID") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COINSURANCE_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COINSURANCE_ID"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString COINSURANCE_ID 

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
        }//public EbixInt32 INSTALLMENT_NO 
    
        public EbixInt32 NO_OF_INSTALLMENTS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["NO_OF_INSTALLMENTS"]) == null ? new EbixInt32("NO_OF_INSTALLMENTS") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["NO_OF_INSTALLMENTS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["NO_OF_INSTALLMENTS"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 NO_OF_INSTALLMENTS 

        public EbixDouble COMMISSION_AMOUNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["COMMISSION_AMOUNT"]) == null ? new EbixDouble("COMMISSION_AMOUNT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["COMMISSION_AMOUNT"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["COMMISSION_AMOUNT"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble COMMISSION_AMOUNT
        //Added Till Here 
        //Added by Pradeep on 24-March-2011 itrack 913
        public EbixDouble TOTAL_PREMIUM_COLLECTION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_PREMIUM_COLLECTION"]) == null ? new EbixDouble("TOTAL_PREMIUM_COLLECTION") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_PREMIUM_COLLECTION"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOTAL_PREMIUM_COLLECTION"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble TOTAL_PREMIUM_COLLECTION
        //Added Till Here 
        public EbixInt32 RECEIPT_FROM_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RECEIPT_FROM_ID"]) == null ? new EbixInt32("RECEIPT_FROM_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RECEIPT_FROM_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RECEIPT_FROM_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 RECEIPT_FROM_ID 
        public EbixString CALLED_FROM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CALLED_FROM"]) == null ? new EbixString("CALLED_FROM") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CALLED_FROM"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CALLED_FROM"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixInt32 CALLED_FROM 
        public EbixString INCORRECT_OUR_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["INCORRECT_OUR_NUMBER"]) == null ? new EbixString("INCORRECT_OUR_NUMBER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["INCORRECT_OUR_NUMBER"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["INCORRECT_OUR_NUMBER"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixInt32 INCORRECT_OUR_NUMBER 

        public EbixString IS_COMMITED
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_COMMITED"]) == null ? new EbixString("IS_COMMITED") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_COMMITED"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_COMMITED"]).CurrentValue = Convert.ToString(value);
            }
        }

        //Added by pradeep - itrack 1722/TFS#1890
        public EbixInt32 RECORD_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RECORD_ID"]) == null ? new EbixInt32("RECORD_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RECORD_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RECORD_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 RECORD_ID 
        
        #endregion

        /// <summary>
        /// Use to get the Deposit Line items data
        /// </summary>
        /// <returns></returns>
        public DataSet FetchData()
        {
            DataSet dsCount = null;
            try
            {
                base.Proc_FetchData = "Proc_Get_Deposit_Line_Items";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@DEPOSIT_ID", DEPOSIT_ID.CurrentValue); 
                base.htGetDataParamCollections.Add("@CD_LINE_ITEM_ID", CD_LINE_ITEM_ID.CurrentValue);
                
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }//public DataSet FetchData()
        /// <summary>
        ///Get data of committed Deposit details
        /// </summary>
        /// <returns></returns>

        public DataSet FetchDataOfCommittedDeposit()
        {
            DataSet dsCount = null;
            try
            {
                base.Proc_FetchData = "Proc_GetDepositCommittedDetailsData";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@DEPOSIT_ID", DEPOSIT_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@CD_LINE_ITEM_ID", CD_LINE_ITEM_ID.CurrentValue);

                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }//public DataSet FetchData()
        /// <summary>
        /// Use to get the Commited Deposit ReceiptDetails  items data
        /// </summary>
        /// <returns></returns>
        //Added by Pradeep Kushwaha on 8 April-2011 for itrack-913
        public DataSet FetchDataOfDepositReceipt(Int32 LANG_ID)
        {
            DataSet dsCount = null;
            try
            {
                base.Proc_FetchData = "Proc_DepositReceipt";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@DEPOSIT_ID", DEPOSIT_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@LANG_ID", LANG_ID);
                
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }//public DataSet FetchData()
        
        /// <summary>
        /// Get Deposit Numbers of Same Boleto numbers 
        /// </summary>
        /// <returns></returns>
        public DataSet FetchDepositNumberOfRefundBoleto()
        {
            DataSet dsCount = null;
            try
            {
                base.ReturnIDName = "";
                base.Proc_FetchData = "Proc_GetDepositNumbersOfRefundBoleto";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@BOLETO_NO", BOLETO_NO.CurrentValue);

                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }//public DataSet FetchData()
        /// <summary>
        /// Use to Deposit Line items data, 
        /// Use Data Wrapper as Param
        /// </summary>
        /// <returns>int</returns>
        public int AddDepositLineItemsData(DataWrapper objWrapper,String DepositType)
        {


            int returnResult = 0;
            try
            {

                base.Proc_Add_Name = "Proc_Insert_Deposit_Line_Items";

                base.ReturnIDName = "@CD_LINE_ITEM_ID"; // //Set the out parameter
                //For Transaction Log
                base.TRANS_TYPE_ID = 277;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                //end 
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CD_LINE_ITEM_ID.IsDBParam = false;
                this.OUR_NUMBER.IsDBParam = false;
                this.IS_APPROVE.IsDBParam = false;
                this.RECEIPT_NUM.IsDBParam = false;
                this.RECEIPT_BRANCH_CITY.IsDBParam = false;
                this.RECEIPT_ISSUED_DATE.IsDBParam = false;
                this.POLICY_STATUS.IsDBParam = false;
                this.DEPOSIT_NUMBER.IsDBParam = false;
                
                this.COINS_CARRIER_LEADER.IsDBParam = false;
                this.POLICY_HOLDER_NAME.IsDBParam = false;
                this.SUSEP_CLASS_OF_BUSINESS.IsDBParam = false;
                
                //this.LEADER_POLICY_ID.IsDBParam = false;
                //this.LEADER_DOC_ID.IsDBParam = false;
                this.BRANCH_COINS_ID.IsDBParam = false;
                this.COINSURANCE_ID.IsDBParam = false;
                this.NO_OF_INSTALLMENTS.IsDBParam = false;
                this.IS_COMMITED.IsDBParam = false;                      // ADDED BY ATUL i-TRACK 913 NOTES 10-06-2011
                this.RECORD_ID.IsDBParam = false; //Added by pradeep - itrack 1722/TFS#1890
                
                ProcReturnValue = true;
                returnResult = base.Save(objWrapper);
                returnResult = Proc_ReturnValue;

                this.CD_LINE_ITEM_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;


        }//public int AddDepositLineItemsData()

        /// <summary>
        /// Use to Deposit Line items data
        /// </summary>
        /// <returns>int</returns>
        public int AddDepositLineItemsData()
        {


            int returnResult = 0;
            try
            {

                base.Proc_Add_Name = "Proc_Insert_Deposit_Line_Items";

                base.ReturnIDName = "@CD_LINE_ITEM_ID"; // //Set the out parameter
                //For Transaction Log
                base.TRANS_TYPE_ID = 277;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                
                //end 
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CD_LINE_ITEM_ID.IsDBParam = false;
                this.OUR_NUMBER.IsDBParam = false;
                this.IS_APPROVE.IsDBParam = false;
                this.RECEIPT_NUM.IsDBParam = false;
                this.RECEIPT_BRANCH_CITY.IsDBParam = false;
                this.RECEIPT_ISSUED_DATE.IsDBParam = false;
                this.POLICY_STATUS.IsDBParam = false;
                this.DEPOSIT_NUMBER.IsDBParam = false;
                
              //  this.DEPOSIT_TYPE.IsDBParam = false;

                this.COINS_CARRIER_LEADER.IsDBParam = false;
                this.POLICY_HOLDER_NAME.IsDBParam = false;
                this.SUSEP_CLASS_OF_BUSINESS.IsDBParam = false;
                //this.LEADER_POLICY_ID.IsDBParam = false;
                //this.LEADER_DOC_ID.IsDBParam = false;
                this.BRANCH_COINS_ID.IsDBParam = false;
                this.COINSURANCE_ID.IsDBParam = false;
                //this.INSTALLMENT_NO.IsDBParam = false;
                this.NO_OF_INSTALLMENTS.IsDBParam = false;
                //this.COMMISSION_AMOUNT.IsDBParam = false;
                #region Commented to show these properties - itrack 1495
                //this.BANK_NUMBER.IsDBParam = false;
                //this.BANK_AGENCY_NUMBER.IsDBParam = false;
                //this.BANK_ACCOUNT_NUMBER.IsDBParam = false;
                #endregion
                this.CALLED_FROM.IsDBParam = false;
                //this.INCORRECT_OUR_NUMBER.IsDBParam = false;
                this.IS_COMMITED.IsDBParam = false;                      // ADDED BY ATUL i-TRACK 913 NOTES 10-06-2011
                this.RECORD_ID.IsDBParam = false; //Added by pradeep - itrack 1722/TFS#1890

                ProcReturnValue = true;
                returnResult = base.Save();
                returnResult = Proc_ReturnValue;

                this.CD_LINE_ITEM_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;


        }//public int AddDepositLineItemsData()

        /// <summary>
        /// Use to Update Deposit Line items data
        /// </summary>
        /// <returns></returns>
        public int UpdateDepositLineItemsData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Update_Name = "Proc_Update_Deposit_Line_Items";

                //For Transaction Log
              
                base.TRANS_TYPE_ID = 278;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //end 
    
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.OUR_NUMBER.IsDBParam = false;
                this.CREATED_FROM.IsDBParam = false;
                this.PAGE_ID.IsDBParam = false;
                this.RECEIPT_NUM.IsDBParam = false;
                this.RECEIPT_BRANCH_CITY.IsDBParam = false;
                this.RECEIPT_ISSUED_DATE.IsDBParam = false;
                this.POLICY_STATUS.IsDBParam = false;
                this.DEPOSIT_NUMBER.IsDBParam = false;
                this.DEPOSIT_TYPE.IsDBParam = false;

                this.COINS_CARRIER_LEADER.IsDBParam = false;
                this.POLICY_HOLDER_NAME.IsDBParam = false;
                this.SUSEP_CLASS_OF_BUSINESS.IsDBParam = false;
                this.LEADER_POLICY_ID.IsDBParam = false;
                this.LEADER_DOC_ID.IsDBParam = false;
                this.BRANCH_COINS_ID.IsDBParam = false;
                this.COINSURANCE_ID.IsDBParam = false;
                this.INSTALLMENT_NO.IsDBParam = false;
                this.NO_OF_INSTALLMENTS.IsDBParam = false;
                //this.COMMISSION_AMOUNT.IsDBParam = false;
                this.CALLED_FROM.IsDBParam = false;
                this.INCORRECT_OUR_NUMBER.IsDBParam = false;
                this.IS_COMMITED.IsDBParam = false;                      // ADDED BY ATUL i-TRACK 913 NOTES 10-06-2011
                this.RECORD_ID.IsDBParam = false; //Added by pradeep - itrack 1722/TFS#1890

                returnValue = base.Update();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        }//public int UpdateDepositLineItemsData()

        /// <summary>
        /// Use to Delete Deposit Line items data
        /// </summary>
        /// <returns></returns>
        public int DeleteDepositLineItemsData()
        {
            int returnValue = 0;
            
            try
            {

                base.Proc_Delete_Name = "Proc_Delete_Deposit_Line_Items";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@DEPOSIT_ID", DEPOSIT_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@CD_LINE_ITEM_ID", CD_LINE_ITEM_ID.CurrentValue);


                //For Transaction Log

                base.TRANS_TYPE_ID = 279;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                this.IS_COMMITED.IsDBParam = false;                      // ADDED BY ATUL i-TRACK 913 NOTES 10-06-2011
                 
                //end 
               
                returnValue = base.Delete();
                

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        }//public int DeleteDepositLineItemsData()

        /// <summary>
        /// Use to Approve Deposit Line items data
        /// </summary>
        /// <returns></returns>
        public int ApproveDepositLineItemsData()
        {
            int returnValue = 0;

            try
            {

                base.Proc_Update_Name = "Proc_Approve_Deposit_Line_Items";

                #region H I D E DB P A R A M

                this.CUSTOMER_ID.IsDBParam = false;
                this.POLICY_ID.IsDBParam = false;
                this.POLICY_VERSION_ID.IsDBParam = false;
                this.RISK_PREMIUM.IsDBParam = false;
                this.FEE.IsDBParam = false;
                this.TAX.IsDBParam = false;
                this.INTEREST.IsDBParam = false;
                this.LATE_FEE.IsDBParam = false;
                this.RECEIPT_AMOUNT.IsDBParam = false;
                this.BOLETO_NO.IsDBParam = false;
                this.POLICY_NUMBER.IsDBParam = false;
                this.BANK_NUMBER.IsDBParam = false;
                this.BANK_AGENCY_NUMBER.IsDBParam = false;
                this.BANK_ACCOUNT_NUMBER.IsDBParam = false;
                this.OUR_NUMBER.IsDBParam = false;
                this.PAY_MODE.IsDBParam = false;
                this.IS_EXCEPTION.IsDBParam = false;
                this.CREATED_FROM.IsDBParam = false;
                this.PAGE_ID.IsDBParam = false;
                
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;

                this.RECEIPT_NUM.IsDBParam = false;
                this.RECEIPT_BRANCH_CITY.IsDBParam = false;
                this.RECEIPT_ISSUED_DATE.IsDBParam = false;
                this.PAYMENT_DATE.IsDBParam = false;
                this.EXCEPTION_REASON.IsDBParam = false;
                this.POLICY_STATUS.IsDBParam = false;
                this.DEPOSIT_NUMBER.IsDBParam = false;
                this.DEPOSIT_TYPE.IsDBParam = false;

                this.COINS_CARRIER_LEADER.IsDBParam = false;
                this.POLICY_HOLDER_NAME.IsDBParam = false;
                this.SUSEP_CLASS_OF_BUSINESS.IsDBParam = false;
                this.LEADER_POLICY_ID.IsDBParam = false;
                this.LEADER_DOC_ID.IsDBParam = false;
                this.BRANCH_COINS_ID.IsDBParam = false;
                this.COINSURANCE_ID.IsDBParam = false;
                this.INSTALLMENT_NO.IsDBParam = false;
                this.NO_OF_INSTALLMENTS.IsDBParam = false;
                this.COMMISSION_AMOUNT.IsDBParam = false;
                this.TOTAL_PREMIUM_COLLECTION.IsDBParam = false;
                this.RECEIPT_FROM_ID.IsDBParam = false;
                this.CALLED_FROM.IsDBParam = false;
                this.INCORRECT_OUR_NUMBER.IsDBParam = false;
                this.IS_COMMITED.IsDBParam = false;                      // ADDED BY ATUL i-TRACK 913 NOTES 10-06-2011
                this.RECORD_ID.IsDBParam = false; //Added by pradeep - itrack 1722/TFS#1890
                #endregion

                //For Transaction Log

                base.TRANS_TYPE_ID = 280;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                //end 

                returnValue = base.Update();


            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        }// public int ApproveDepositLineItemsData()


        /// <summary>
        /// Use to Generate Receipt of Deposit Line items data
        /// </summary>
        /// <returns></returns>
        public int GenerateReceiptDepositLineItemsData()
        {
            int returnValue = 0;

            try
            {

                base.Proc_Update_Name = "Proc_GenerateReceiptDepositLineItems";

                #region H I D E DB P A R A M

                this.CUSTOMER_ID.IsDBParam = false;
                this.POLICY_ID.IsDBParam = false;
                this.POLICY_VERSION_ID.IsDBParam = false;
                this.RISK_PREMIUM.IsDBParam = false;
                this.FEE.IsDBParam = false;
                this.TAX.IsDBParam = false;
                this.INTEREST.IsDBParam = false;
                this.LATE_FEE.IsDBParam = false;
                this.RECEIPT_AMOUNT.IsDBParam = false;
                this.BOLETO_NO.IsDBParam = false;
                this.POLICY_NUMBER.IsDBParam = false;
                this.BANK_NUMBER.IsDBParam = false;
                this.BANK_AGENCY_NUMBER.IsDBParam = false;
                this.BANK_ACCOUNT_NUMBER.IsDBParam = false;
                this.OUR_NUMBER.IsDBParam = false;
                this.PAY_MODE.IsDBParam = false;
                this.IS_EXCEPTION.IsDBParam = false;
                this.CREATED_FROM.IsDBParam = false;
                this.PAGE_ID.IsDBParam = false;

                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.IS_APPROVE.IsDBParam = false;
                
                this.RECEIPT_NUM.IsDBParam = false;
                this.RECEIPT_BRANCH_CITY.IsDBParam = false;
                this.PAYMENT_DATE.IsDBParam = false;
                this.EXCEPTION_REASON.IsDBParam = false;
                this.POLICY_STATUS.IsDBParam = false;
                this.DEPOSIT_NUMBER.IsDBParam = false;
                this.DEPOSIT_TYPE.IsDBParam = false;

                this.COINS_CARRIER_LEADER.IsDBParam = false;
                this.POLICY_HOLDER_NAME.IsDBParam = false;
                this.SUSEP_CLASS_OF_BUSINESS.IsDBParam = false;
                this.LEADER_POLICY_ID.IsDBParam = false;
                this.LEADER_DOC_ID.IsDBParam = false;
                this.BRANCH_COINS_ID.IsDBParam = false;
                this.COINSURANCE_ID.IsDBParam = false;
                this.INSTALLMENT_NO.IsDBParam = false;
                this.NO_OF_INSTALLMENTS.IsDBParam = false;
                this.COMMISSION_AMOUNT.IsDBParam = false;
                this.TOTAL_PREMIUM_COLLECTION.IsDBParam = false;
                this.RECEIPT_FROM_ID.IsDBParam = false;
                this.CALLED_FROM.IsDBParam = false;
                this.INCORRECT_OUR_NUMBER.IsDBParam = false;
                this.IS_COMMITED.IsDBParam = false;                      // ADDED BY ATUL i-TRACK 913 NOTES 10-06-2011
                this.RECORD_ID.IsDBParam = false; //Added by pradeep - itrack 1722/TFS#1890
                #endregion

                //For Transaction Log

                base.TRANS_TYPE_ID = 281;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
              

                //end 

                returnValue = base.Update();


            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        }// public int GenerateReceiptDepositLineItemsData()

        /// <summary>
        /// Use to get the Receipt generated details data 
        /// </summary>
        /// <returns></returns>
        public DataSet FetchDataReceiptGeneratedData(int Deposit_id, int Cd_line_item_id, string Receipt_num,int User_id)
        {
            DataSet dsCount = null;
            try
            {
                base.Proc_FetchData = "Proc_GetGeneratedReceiptDetails";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@DEPOSIT_ID", Deposit_id);
                base.htGetDataParamCollections.Add("@CD_LINE_ITEM_ID", Cd_line_item_id);
                base.htGetDataParamCollections.Add("@RECEIPT_NUM", Receipt_num);
                base.htGetDataParamCollections.Add("@USER_ID", User_id);

                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }//public DataSet FetchDataReceiptGeneratedData()

        //Added By Lalit to fecth new IIOF Report

        public DataSet FetchIOFReport(int DEPOSITE_ID) 
        {
            
            DataSet dsIOFReport = null;
            try
            {
                string Proc_Name = "PROC_IOF_REPORT";
                base.Proc_FetchData = Proc_Name;
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@DEPOSIT_ID", DEPOSITE_ID);
                dsIOFReport = base.GetData();
                
            }
            catch (Exception Ex) { throw (Ex); }
            return dsIOFReport;
        }
      
    }
}
