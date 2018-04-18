using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.Model.Support;
using Cms.EbixDataTypes;
using System.Data;

namespace Cms.Model.Policy.Accident
{
    [Serializable]
    public class ClsIndividualInfo : ClsModelBaseClass
    {
        public ClsIndividualInfo()
        {
            this.PropertyCollection();
        }

        #region Delare the add parameter collection for the data wrapper class
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("PERSONAL_INFO_ID", PERSONAL_INFO_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("INDIVIDUAL_NAME", INDIVIDUAL_NAME    );
            base.htPropertyCollection.Add("CODE", CODE);
            base.htPropertyCollection.Add("POSITION_ID", POSITION_ID);
            base.htPropertyCollection.Add("CPF_NUM", CPF_NUM);
            base.htPropertyCollection.Add("STATE_ID", STATE_ID);
            base.htPropertyCollection.Add("DATE_OF_BIRTH", DATE_OF_BIRTH);
            base.htPropertyCollection.Add("GENDER", GENDER);
            base.htPropertyCollection.Add("REG_IDEN", REG_IDEN);
            base.htPropertyCollection.Add("REG_ID_ISSUES", REG_ID_ISSUES);
            base.htPropertyCollection.Add("REMARKS", REMARKS);
            base.htPropertyCollection.Add("COUNTRY_ID", COUNTRY_ID);
            base.htPropertyCollection.Add("REG_ID_ORG", REG_ID_ORG);
            base.htPropertyCollection.Add("APPLICANT_ID", APPLICANT_ID);
            //Itrack 851 Added by pradeep Kushwaha on 22 -feb - 2011
            base.htPropertyCollection.Add("IS_SPOUSE_OR_CHILD", IS_SPOUSE_OR_CHILD);
            base.htPropertyCollection.Add("MAIN_INSURED", MAIN_INSURED);
            base.htPropertyCollection.Add("TYPE", TYPE);
            //Itrack 1051 Added by pradeep Kushwaha on 04 -April - 2011
            base.htPropertyCollection.Add("CITY_OF_BIRTH", CITY_OF_BIRTH);
            base.htPropertyCollection.Add("ORIGINAL_VERSION_ID", ORIGINAL_VERSION_ID);
            base.htPropertyCollection.Add("MARITAL_STATUS", MARITAL_STATUS);
            base.htPropertyCollection.Add("EXCEEDED_PREMIUM", EXCEEDED_PREMIUM);
            //added till here 
        }
        #endregion

        #region Declare the Property for every data table columns
        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
        public EbixInt32 PERSONAL_INFO_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PERSONAL_INFO_ID"]) == null ? new EbixInt32("PERSONAL_INFO_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PERSONAL_INFO_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PERSONAL_INFO_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

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
        }

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
        }
        public EbixInt32 ORIGINAL_VERSION_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ORIGINAL_VERSION_ID"]) == null ? new EbixInt32("ORIGINAL_VERSION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ORIGINAL_VERSION_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ORIGINAL_VERSION_ID"]).CurrentValue = Convert.ToInt32(value);
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
        }

        public EbixString INDIVIDUAL_NAME
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["INDIVIDUAL_NAME"]) == null ? new EbixString("INDIVIDUAL_NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["INDIVIDUAL_NAME"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["INDIVIDUAL_NAME"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString CODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CODE"]) == null ? new EbixString("CODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CODE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixInt32 POSITION_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POSITION_ID"]) == null ? new EbixInt32("POSITION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POSITION_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POSITION_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString CPF_NUM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CPF_NUM"]) == null ? new EbixString("CPF_NUM") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CPF_NUM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CPF_NUM"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixInt32 STATE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["STATE_ID"]) == null ? new EbixInt32("STATE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["STATE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["STATE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixDateTime DATE_OF_BIRTH
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["DATE_OF_BIRTH"]) == null ? new EbixDateTime("DATE_OF_BIRTH") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["DATE_OF_BIRTH"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["DATE_OF_BIRTH"]).CurrentValue = Convert.ToDateTime(value);
            }
        }

        public EbixInt32 GENDER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["GENDER"]) == null ? new EbixInt32("GENDER") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["GENDER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["GENDER"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString REG_IDEN
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REG_IDEN"]) == null ? new EbixString("REG_IDEN") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REG_IDEN"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REG_IDEN"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixDateTime REG_ID_ISSUES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["REG_ID_ISSUES"]) == null ? new EbixDateTime("REG_ID_ISSUES") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["REG_ID_ISSUES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["REG_ID_ISSUES"]).CurrentValue = Convert.ToDateTime(value);
            }
        }

        public EbixString REMARKS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REMARKS"]) == null ? new EbixString("REMARKS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REMARKS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REMARKS"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixInt32 COUNTRY_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COUNTRY_ID"]) == null ? new EbixInt32("COUNTRY_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COUNTRY_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COUNTRY_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString REG_ID_ORG
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REG_ID_ORG"]) == null ? new EbixString("REG_ID_ORG") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REG_ID_ORG"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REG_ID_ORG"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixInt32 APPLICANT_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["APPLICANT_ID"]) == null ? new EbixInt32("APPLICANT_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["APPLICANT_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["APPLICANT_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        //Itrack 851 Added by pradeep Kushwaha on 22 -feb - 2011 
        public EbixInt32 IS_SPOUSE_OR_CHILD
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IS_SPOUSE_OR_CHILD"]) == null ? new EbixInt32("IS_SPOUSE_OR_CHILD") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IS_SPOUSE_OR_CHILD"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IS_SPOUSE_OR_CHILD"]).CurrentValue = Convert.ToInt32(value);
            }
        }//IS_SPOUSE_OR_CHILD
        public EbixInt32 MAIN_INSURED
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MAIN_INSURED"]) == null ? new EbixInt32("MAIN_INSURED") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MAIN_INSURED"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MAIN_INSURED"]).CurrentValue = Convert.ToInt32(value);
            }
        }//MAIN_INSURED
        public EbixInt32 TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE"]) == null ? new EbixInt32("TYPE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE"]).CurrentValue = Convert.ToInt32(value);
            }
        }//MAIN_INSURED
        //Itrack 1051 Added by pradeep Kushwaha on 22 -feb - 2011 
        public EbixString CITY_OF_BIRTH
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CITY_OF_BIRTH"]) == null ? new EbixString("CITY_OF_BIRTH") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CITY_OF_BIRTH"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CITY_OF_BIRTH"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString MARITAL_STATUS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MARITAL_STATUS"]) == null ? new EbixString("MARITAL_STATUS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MARITAL_STATUS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MARITAL_STATUS"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixInt32 EXCEEDED_PREMIUM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["EXCEEDED_PREMIUM"]) == null ? new EbixInt32("EXCEEDED_PREMIUM") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["EXCEEDED_PREMIUM"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["EXCEEDED_PREMIUM"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        
        //Till here 
        #endregion

        #region Methods
        /// <summary>
        /// Fetch Applicants\Co-Applicants based on PolicyId,PolicyVersionId,CustomerId
        /// </summary>
        /// <returns></returns>
        public DataSet FetchApplicants(int CUSTOMER_ID, int POLICY_VERSION_ID, int POLICY_ID)
        {
            DataSet dsCount = null;
        
            try
            {
                base.Proc_FetchData = "Proc_FetchApplicant";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                dsCount = base.GetData();


            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }

        public DataSet FetchData()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetPolPersonalAccidentInfo";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@PERSONAL_INFO_ID", PERSONAL_INFO_ID.CurrentValue);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }
        /// <summary>
        /// Use to get the insured object details 
        /// </summary>
        /// <returns></returns>
        public DataSet FetchInsuredObjectData(int PERSONAL_INFO_ID, int CUSTOMER_ID,int POLICY_ID, int POLICY_VERSION_ID,  String CALLED_FOR)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetAccidentInfoInsuredObject";
                base.htGetDataParamCollections.Clear();
                
                base.htGetDataParamCollections.Add("@PERSONAL_INFO_ID", PERSONAL_INFO_ID);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                base.htGetDataParamCollections.Add("@CALLED_FOR", CALLED_FOR);
                
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }
        public int ADDPersonalAccidentData()
        {
            int returnResult = 0;
            try
            {

                base.Proc_Add_Name = "Proc_InsertPolPersonalAccidentInfo";

                base.ReturnIDName = "@PERSONAL_INFO_ID";

                //For Transaction Log
                base.ProcReturnValue = true;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //end 

                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.PERSONAL_INFO_ID.IsDBParam = false;
                this.TYPE.IsDBParam = false;

                returnResult = base.Save();

                this.PERSONAL_INFO_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
                returnResult = Proc_ReturnValue;
            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }

            
                return returnResult;
        }

        /// <summary>
        /// Update Individual Data
        /// </summary>
        /// <returns></returns>
        public int UpdatePersonalAccidentData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Update_Name = "Proc_UpdatePolPersonalAccidentInfo";

                //For Transaction Log
                this.ORIGINAL_VERSION_ID.IsDBParam = false;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;     
                //end 
                base.ProcReturnValue = true;
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
               // this.APPLICANT_ID.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.TYPE.IsDBParam = false;
                returnValue = base.Update();
                returnValue = Proc_ReturnValue;

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            
                
            return returnValue;
        }

        /// <summary>
        /// Delete Individual Data
        /// </summary>
        /// <returns></returns>
        public int DeletePersonalAccidentData()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Delete_Name = "Proc_DeletePolPersonalAccidentInfo";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@PERSONAL_INFO_ID", PERSONAL_INFO_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);

                //For Transaction Log
                
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //end 

                returnValue = base.Delete();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Ex
            return returnValue;

        }

        /// <summary>
        /// ActivateDeactivate Individual Data
        /// </summary>
        /// <returns></returns>
        public int ActivateDeactivatePersonalAccidentData()
        {
            int returnValue = 0;
            try
            {
                base.Proc_ActivateDeactivate_Name = "Proc_ActivateDeactivatePolPersonalAccidentInfo";

                base.htGetDataParamCollections.Clear();
                
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@PERSONAL_INFO_ID", PERSONAL_INFO_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@IS_ACTIVE", IS_ACTIVE.CurrentValue);


                //For Transaction Log

                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;


               
                returnValue = base.ActivateDeactivate();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return returnValue;

        }

        /// <summary>
        /// Fetch Applicants\Co-Applicants based on PolicyId,PolicyVersionId,CustomerId
        /// </summary>
        /// <returns></returns>
        public DataSet FetchApplicantsDetails(int APPLICANT_ID, Int32 CUSTOMER_ID, Int32 POLICY_VERSION_ID, Int32 POLICY_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_FetchApplicantDetails";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@APPLICANT_ID", APPLICANT_ID);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }
        #endregion
    }
}
