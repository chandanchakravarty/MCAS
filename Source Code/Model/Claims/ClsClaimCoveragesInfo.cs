
/******************************************************************************************
<Author				: - Santosh Kumar Gautam
<Start Date			: - 09 Nov, 2010
<End Date			: -	
<Description		: - Model Class for Risk Information page functionality.
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

namespace Cms.Model.Claims
{
    [Serializable]
    public class ClsClaimCoveragesInfo : ClsModelBaseClass
    {
                /// <summary>
        /// Initialize the default value 
        /// </summary>
        public ClsClaimCoveragesInfo()
        {
            //this.SetColumnsName();
            this.PropertyCollection();
        }

        #region Delare the add the parameter collection for the data wrapper class
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>
        private void PropertyCollection()
        {
           

        

            base.htPropertyCollection.Add("COVERAGE_CODE_ID", COVERAGE_CODE_ID);
            base.htPropertyCollection.Add("CLAIM_ID", CLAIM_ID);
            base.htPropertyCollection.Add("IS_RISK_COVERAGE", IS_RISK_COVERAGE);
            base.htPropertyCollection.Add("RI_APPLIES", RI_APPLIES);
            base.htPropertyCollection.Add("COV_DES", COV_DES);
            base.htPropertyCollection.Add("LIMIT_OVERRIDE", LIMIT_OVERRIDE);
            base.htPropertyCollection.Add("LIMIT_1", LIMIT_1);
            base.htPropertyCollection.Add("MINIMUM_DEDUCTIBLE", MINIMUM_DEDUCTIBLE);
            base.htPropertyCollection.Add("POLICY_LIMIT", POLICY_LIMIT);
            base.htPropertyCollection.Add("DEDUCTIBLE1_AMOUNT_TEXT", DEDUCTIBLE1_AMOUNT_TEXT);
            base.htPropertyCollection.Add("CLAIM_COV_ID", CLAIM_COV_ID);
            base.htPropertyCollection.Add("IS_USER_CREATED", IS_USER_CREATED);
            base.htPropertyCollection.Add("VICTIM_ID", VICTIM_ID);
            base.htPropertyCollection.Add("CREATE_MODE", CREATE_MODE);
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);
            base.htPropertyCollection.Add("COVERAGE_SI_FLAG", COVERAGE_SI_FLAG);

            
          
            

              
            
        }//private void PropertyCollection()s
        
        #endregion

        #region Declare the Property for every data table columns

        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
      

       
        public EbixInt32 COVERAGE_CODE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COVERAGE_CODE_ID"]) == null ? new EbixInt32("COVERAGE_CODE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COVERAGE_CODE_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COVERAGE_CODE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 CLAIM_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAIM_ID"]) == null ? new EbixInt32("CLAIM_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAIM_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAIM_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString CREATE_MODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CREATE_MODE"]) == null ? new EbixString("CREATE_MODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CREATE_MODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CREATE_MODE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString IS_RISK_COVERAGE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_RISK_COVERAGE"]) == null ? new EbixString("IS_RISK_COVERAGE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_RISK_COVERAGE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_RISK_COVERAGE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString RI_APPLIES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RI_APPLIES"]) == null ? new EbixString("RI_APPLIES") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RI_APPLIES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RI_APPLIES"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString COV_DES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COV_DES"]) == null ? new EbixString("COV_DES") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COV_DES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COV_DES"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString LIMIT_OVERRIDE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LIMIT_OVERRIDE"]) == null ? new EbixString("LIMIT_OVERRIDE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LIMIT_OVERRIDE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LIMIT_OVERRIDE"]).CurrentValue = Convert.ToString(value);
            }
        }
       
        public EbixDouble LIMIT_1
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LIMIT_1"]) == null ? new EbixDouble("LIMIT_1") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LIMIT_1"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LIMIT_1"]).CurrentValue = Convert.ToDouble(value);
            }
        }       
        
        public EbixDouble MINIMUM_DEDUCTIBLE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MINIMUM_DEDUCTIBLE"]) == null ? new EbixDouble("MINIMUM_DEDUCTIBLE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MINIMUM_DEDUCTIBLE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MINIMUM_DEDUCTIBLE"]).CurrentValue = Convert.ToDouble(value);
            }
        }

        public EbixDouble POLICY_LIMIT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["POLICY_LIMIT"]) == null ? new EbixDouble("POLICY_LIMIT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["POLICY_LIMIT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["POLICY_LIMIT"]).CurrentValue = Convert.ToDouble(value);
            }
        }

        public EbixString DEDUCTIBLE1_AMOUNT_TEXT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DEDUCTIBLE1_AMOUNT_TEXT"]) == null ? new EbixString("DEDUCTIBLE1_AMOUNT_TEXT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DEDUCTIBLE1_AMOUNT_TEXT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DEDUCTIBLE1_AMOUNT_TEXT"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixInt32 CLAIM_COV_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAIM_COV_ID"]) == null ? new EbixInt32("CLAIM_COV_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAIM_COV_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAIM_COV_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString IS_USER_CREATED
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_USER_CREATED"]) == null ? new EbixString("IS_USER_CREATED") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_USER_CREATED"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_USER_CREATED"]).CurrentValue = Convert.ToString(value);
            }
        }


        public EbixInt32 VICTIM_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["VICTIM_ID"]) == null ? new EbixInt32("VICTIM_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["VICTIM_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["VICTIM_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }


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
            { //return _POLICY_VERSION_ID;
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]) == null ? new EbixInt32("POLICY_VERSION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]);
            }
            set
            {
                //_POLICY_VERSION_ID.CurrentValue = Convert.ToInt32(value);
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 POLICY_VERSION_ID 


        public EbixString COVERAGE_SI_FLAG
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COVERAGE_SI_FLAG"]) == null ? new EbixString("COVERAGE_SI_FLAG") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COVERAGE_SI_FLAG"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COVERAGE_SI_FLAG"]).CurrentValue = Convert.ToString(value);
            }
        }


        #endregion




        public DataSet GetClaimCoverages(int ClaimID, short LangID, string CareerCode)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "Proc_GetClaimCoverages";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CLAIM_ID", ClaimID);
                base.htGetDataParamCollections.Add("@LANG_ID", LangID);
                base.htGetDataParamCollections.Add("@CARRIER_CODE", CareerCode); 
                ds = base.GetData();

            }//try
            catch (Exception ex)
            { throw (ex); }//catch (Exception ex)
            return ds;
        }



        public DataTable GetProductCoverages(int LOB_ID, int CLAIM_ID, int LANG_ID, string FetchMode)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "Proc_GetClaimProductCoverages";
                base.htGetDataParamCollections.Clear(); 
                base.htGetDataParamCollections.Add("@LOB_ID", LOB_ID);
                base.htGetDataParamCollections.Add("@CLAIM_ID", CLAIM_ID);
                base.htGetDataParamCollections.Add("@LANG_ID", LANG_ID);
                base.htGetDataParamCollections.Add("@FETCH_MODE", LANG_ID);
                              
                ds = base.GetData();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return null;

            }//try
            catch (Exception ex)
            { throw (ex); }//catch (Exception ex)
            return null;
        }

        public int DeleteUserCreateCoverage()
        {
            
            int returnResult = 0;
            try
            {
                this.TRANS_TYPE_ID = 328;
                base.Proc_Add_Name = "Proc_DeleteCoveragesFromClaim";

                base.ReturnIDName = "@ERROR_CODE";



                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
           
              
                //end 
                this.IS_ACTIVE.IsDBParam = false;
                this.CREATE_MODE.IsDBParam = false;
                this.IS_USER_CREATED.IsDBParam = false;
                this.RI_APPLIES.IsDBParam = false;
                this.COV_DES.IsDBParam = false;
                this.VICTIM_ID.IsDBParam = false;
                this.IS_RISK_COVERAGE.IsDBParam = false;
                this.COVERAGE_SI_FLAG.IsDBParam = false;

                this.CUSTOMER_ID.IsDBParam = false;
                this.POLICY_ID.IsDBParam = false;
                this.POLICY_VERSION_ID.IsDBParam = false;

                this.MODIFIED_BY.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.COVERAGE_CODE_ID.IsDBParam = false;
                this.MINIMUM_DEDUCTIBLE.IsDBParam = false;
                this.DEDUCTIBLE1_AMOUNT_TEXT.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.IS_RISK_COVERAGE.IsDBParam = false;
                this.IS_USER_CREATED.IsDBParam = false;
                this.LIMIT_1.IsDBParam = false;
                this.LIMIT_OVERRIDE.IsDBParam = false;
                this.POLICY_LIMIT.IsDBParam = false;
                this.RI_APPLIES.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;

                //  this.PERIL_ID.IsDBParam = false;

                returnResult = base.Save();

                returnResult = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }

        public DataSet FetchData()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetClaimCoverageByID";
                base.htGetDataParamCollections.Clear();

                base.htGetDataParamCollections.Add("@CLAIM_COV_ID", CLAIM_COV_ID.CurrentValue);        
                base.htGetDataParamCollections.Add("@CLAIM_ID", CLAIM_ID.CurrentValue);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }//public DataSet FetchData(int Peril_ID)
        

       

        public int AddClaimCoverage()
        {
            int returnResult = 0;
            try
            {
                this.TRANS_TYPE_ID = 290;
                base.Proc_Add_Name = "Proc_InsertClaimCoverage";

                base.ReturnIDName = "@CLAIM_COV_ID  ";


             
                base.RECORDED_BY = CREATED_BY.CurrentValue;

                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;



                this.CUSTOMER_ID.IsDBParam = false;
                this.POLICY_ID.IsDBParam = false;
                this.POLICY_VERSION_ID.IsDBParam = false;
                this.COVERAGE_SI_FLAG.IsDBParam = true;
                //end 
                this.IS_ACTIVE.IsDBParam = false;
               
                this.IS_USER_CREATED.IsDBParam = false;
                this.RI_APPLIES.IsDBParam = false;
                this.COV_DES.IsDBParam = false;
                this.CLAIM_COV_ID.IsDBParam = false;
                this.IS_RISK_COVERAGE.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CREATE_MODE.IsDBParam = true;
                //  this.PERIL_ID.IsDBParam = false;

                returnResult = base.Save();

                this.CLAIM_COV_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
               // if (base.ReturnIDNameValue == -2)
                returnResult = ReturnIDNameValue;

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }

        public string GetRIAppliesFlag()
        {
           
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "Proc_GetSystemParams";
                base.htGetDataParamCollections.Clear(); 
               
                              
                ds = base.GetData();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count>0)
                    return ds.Tables[0].Rows[0]["CLM_RI_APPLIES_FLG"].ToString();
                else
                    return null;

            }//try
            catch (Exception ex)
            { throw (ex); }//catch (Exception ex)
            return null;
        }

        public int UpdateClaimCoverage()
        {
            int returnValue = 0;
            try
            {
                
                base.htGetDataParamCollections.Clear();
                base.ReturnIDName = "@ERROR_CODE";

                //For Transaction Log               
                this.TRANS_TYPE_ID = 291;
                base.Proc_Update_Name = "Proc_UpdateClaimCoverage";


                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                this.CUSTOMER_ID.IsDBParam = false;
                this.POLICY_ID.IsDBParam = false;
                this.POLICY_VERSION_ID.IsDBParam = false;
                this.COVERAGE_SI_FLAG.IsDBParam = true;

                //end 
                this.COV_DES.IsDBParam = false;
                this.RI_APPLIES.IsDBParam = false;
                this.IS_RISK_COVERAGE.IsDBParam = false;                
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;
                this.CREATE_MODE.IsDBParam = false;

                this.MODIFIED_BY.IsDBParam = true;

                this.COVERAGE_CODE_ID.IsDBParam = true;
                this.MINIMUM_DEDUCTIBLE.IsDBParam = true;
                this.DEDUCTIBLE1_AMOUNT_TEXT.IsDBParam = true;
                this.IS_USER_CREATED.IsDBParam = true;
                this.VICTIM_ID.IsDBParam = true;
                this.CLAIM_ID.IsDBParam = true;
                this.LIMIT_1.IsDBParam = true;
                this.LIMIT_OVERRIDE.IsDBParam = true;
                this.POLICY_LIMIT.IsDBParam = true;

                this.LAST_UPDATED_DATETIME.IsDBParam = true;

                returnValue = base.Update();

                if (base.ReturnIDNameValue!= 0)
                  returnValue = base.ReturnIDNameValue; //get the out parameter

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        }
    }
}
