/******************************************************************************************
<Author					: -		Sneha
<Start Date				: -		25-10-2011
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
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

namespace Cms.Model.Maintenance
{
   [Serializable]
    public class clsMasterDetailInfo : ClsModelBaseClass
    {
        public clsMasterDetailInfo()
        {
            this.PropertyCollection();
        }
        #region Delare the add the parameter collection for the data wrapper class
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("TYPE_UNIQUE_ID",TYPE_UNIQUE_ID);
            base.htPropertyCollection.Add("TYPE_ID",TYPE_ID);
            base.htPropertyCollection.Add("TYPE_CODE",TYPE_CODE);
            base.htPropertyCollection.Add("TYPE_NAME",TYPE_NAME);
            base.htPropertyCollection.Add("ADDRESS",ADDRESS);
            base.htPropertyCollection.Add("ADDRESS1", ADDRESS);
            base.htPropertyCollection.Add("CITY",CITY);
            base.htPropertyCollection.Add("COUNTRY",COUNTRY);
            base.htPropertyCollection.Add("TEL_NO_OFF",TEL_NO_OFF);
            base.htPropertyCollection.Add("MOBILE_NO",MOBILE_NO);
            base.htPropertyCollection.Add("E_MAIL",E_MAIL);
            base.htPropertyCollection.Add("GST",GST);
            base.htPropertyCollection.Add("CONTACT_PERSON",CONTACT_PERSON);
            base.htPropertyCollection.Add("PROVINCE",PROVINCE);
            base.htPropertyCollection.Add("POST_CODE",POST_CODE);
            base.htPropertyCollection.Add("TEL_NO_RES",TEL_NO_RES);
            base.htPropertyCollection.Add("FAX_NO",FAX_NO);
            base.htPropertyCollection.Add("GST_REG_NO",GST_REG_NO);
            base.htPropertyCollection.Add("WITHHOLDING_TAX",WITHHOLDING_TAX);
            base.htPropertyCollection.Add("STATUS",STATUS);
            base.htPropertyCollection.Add("SOLICITOR_TYPE",SOLICITOR_TYPE);
            base.htPropertyCollection.Add("PRIVATE_E_MAIL",PRIVATE_E_MAIL);
            base.htPropertyCollection.Add("SURVEYOR_SOURCE",SURVEYOR_SOURCE);
            base.htPropertyCollection.Add("CLASSIFICATION",CLASSIFICATION);
            base.htPropertyCollection.Add("MEMO",MEMO);

         }
        #endregion
        #region Declare the Property for every data table columns
        public EbixInt32 TYPE_UNIQUE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE_UNIQUE_ID"]) == null ? new EbixInt32("TYPE_UNIQUE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE_UNIQUE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE_UNIQUE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 TYPE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE_ID"]) == null ? new EbixInt32("TYPE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString TYPE_CODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYPE_CODE"]) == null ? new EbixString("TYPE_CODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYPE_CODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYPE_CODE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString TYPE_NAME
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYPE_NAME"]) == null ? new EbixString("TYPE_NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYPE_NAME"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYPE_NAME"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString ADDRESS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ADDRESS"]) == null ? new EbixString("ADDRESS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ADDRESS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ADDRESS"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ADDRESS1
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ADDRESS1"]) == null ? new EbixString("ADDRESS1") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ADDRESS1"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ADDRESS1"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString CITY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CITY"]) == null ? new EbixString("CITY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CITY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CITY"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString COUNTRY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COUNTRY"]) == null ? new EbixString("COUNTRY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COUNTRY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COUNTRY"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString TEL_NO_OFF
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TEL_NO_OFF"]) == null ? new EbixString("TEL_NO_OFF") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TEL_NO_OFF"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TEL_NO_OFF"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString MOBILE_NO
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MOBILE_NO"]) == null ? new EbixString("MOBILE_NO") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MOBILE_NO"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MOBILE_NO"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString E_MAIL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["E_MAIL"]) == null ? new EbixString("E_MAIL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["E_MAIL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["E_MAIL"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixDouble GST
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["GST"]) == null ? new EbixDouble("GST") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["GST"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["GST"]).CurrentValue = Convert.ToDouble(value);
            }
        }

        public EbixString CONTACT_PERSON
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CONTACT_PERSON"]) == null ? new EbixString("CONTACT_PERSON") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CONTACT_PERSON"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CONTACT_PERSON"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString PROVINCE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROVINCE"]) == null ? new EbixString("PROVINCE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROVINCE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROVINCE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString POST_CODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POST_CODE"]) == null ? new EbixString("POST_CODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POST_CODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POST_CODE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString TEL_NO_RES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TEL_NO_RES"]) == null ? new EbixString("TEL_NO_RES") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TEL_NO_RES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TEL_NO_RES"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString FAX_NO
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FAX_NO"]) == null ? new EbixString("FAX_NO") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FAX_NO"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FAX_NO"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString GST_REG_NO
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["GST_REG_NO"]) == null ? new EbixString("GST_REG_NO") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["GST_REG_NO"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["GST_REG_NO"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixDouble WITHHOLDING_TAX
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["WITHHOLDING_TAX"]) == null ? new EbixDouble("WITHHOLDING_TAX") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["WITHHOLDING_TAX"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["WITHHOLDING_TAX"]).CurrentValue = Convert.ToDouble(value);
            }
        }

        public EbixString STATUS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["STATUS"]) == null ? new EbixString("STATUS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["STATUS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["STATUS"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString SOLICITOR_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SOLICITOR_TYPE"]) == null ? new EbixString("SOLICITOR_TYPE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SOLICITOR_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SOLICITOR_TYPE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString PRIVATE_E_MAIL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PRIVATE_E_MAIL"]) == null ? new EbixString("PRIVATE_E_MAIL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PRIVATE_E_MAIL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PRIVATE_E_MAIL"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString SURVEYOR_SOURCE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SURVEYOR_SOURCE"]) == null ? new EbixString("SURVEYOR_SOURCE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SURVEYOR_SOURCE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SURVEYOR_SOURCE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString CLASSIFICATION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLASSIFICATION"]) == null ? new EbixString("CLASSIFICATION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLASSIFICATION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLASSIFICATION"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString MEMO
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MEMO"]) == null ? new EbixString("MEMO") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MEMO"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MEMO"]).CurrentValue = Convert.ToString(value);
            }
        }
      
        #endregion

        public int AddMastersetupDetailInfo()
        {
            int returnResult = 0;
            try
            {

                base.Proc_Add_Name = "Proc_InsertMasterDetail";

                base.ReturnIDName = "@TYPE_UNIQUE_ID";

               
                this.TYPE_UNIQUE_ID.IsDBParam=false;
               // this.IS_ACTIVE.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.RequiredTransactionLog = false;
                this.ProcReturnValue = true;
                returnResult = base.Save();

                this.TYPE_UNIQUE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnResult;
        }

        public int UpdateMastersetUpInformation()
        {
            int returnValue = 0;
            try
            {


                base.Proc_Update_Name = "Proc_UpdateMasterDetail";

                this.IS_ACTIVE.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                //end 

               
                base.IS_ACTIVE.IsDBParam = false;

                returnValue = base.Update();


            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnValue;
        }

        public DataSet FetchData(int TYPE_UNIQUE_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetMasterDetail";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@TYPE_UNIQUE_ID", TYPE_UNIQUE_ID);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }

        public int ActivateDeactivateMasterDetail()
        {
            int returnValue = 0;
            try
            {

                base.Proc_ActivateDeactivate_Name = "Proc_Active_Deactive_MasterDetail";

                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@TYPE_UNIQUE_ID", TYPE_UNIQUE_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@IS_ACTIVE", IS_ACTIVE.CurrentValue);
                returnValue = base.ActivateDeactivate();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Ex
            return returnValue;

        }

       //--------------mnt_master_value----------

     
    }
}
