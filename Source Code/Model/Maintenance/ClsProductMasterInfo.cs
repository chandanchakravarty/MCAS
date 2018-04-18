/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	01-Sep-2010
<End Date			: -	
<Description		: - 
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
 
namespace Cms.Model.Maintenance
{
   [Serializable]
    public class ClsProductMasterInfo : ClsModelBaseClass
    {
        private String _DisplayMessage = String.Empty;
        private String _FormSaved = String.Empty;

        public ClsProductMasterInfo() 
        {
            this.PropertyCollection();
        }
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>
        private void PropertyCollection()
        {

            base.htPropertyCollection.Add("LOB_ID", LOB_ID);
            base.htPropertyCollection.Add("LOB_CODE", LOB_CODE);
            base.htPropertyCollection.Add("LOB_DESC", LOB_DESC);
            base.htPropertyCollection.Add("LOB_CATEGORY", LOB_CATEGORY);
            base.htPropertyCollection.Add("LOB_TYPE", LOB_TYPE);
            base.htPropertyCollection.Add("LOB_PKG", LOB_PKG);
            base.htPropertyCollection.Add("LOB_ACORD_STD", LOB_ACORD_STD);
            base.htPropertyCollection.Add("DEF_CLAIMS_TYPE", DEF_CLAIMS_TYPE);
            base.htPropertyCollection.Add("LOB_PREFIX", LOB_PREFIX);
            base.htPropertyCollection.Add("LOB_SUFFIX", LOB_SUFFIX);
           
            base.htPropertyCollection.Add("LOB_SEED", LOB_SEED);
            base.htPropertyCollection.Add("OVERRIDE_LOB_PREFIX", OVERRIDE_LOB_PREFIX);
            base.htPropertyCollection.Add("REWRITE_SEED", REWRITE_SEED);
            base.htPropertyCollection.Add("SUSEP_LOB_ID", SUSEP_LOB_ID);
            base.htPropertyCollection.Add("SUSEP_LOB_CODE", SUSEP_LOB_CODE);
            base.htPropertyCollection.Add("COMMISSION_LEVEL", COMMISSION_LEVEL);
            base.htPropertyCollection.Add("APPLICABLE_COMMISSION", APPLICABLE_COMMISSION);
            base.htPropertyCollection.Add("SUSEP_PROCESS_NUMBERS", SUSEP_PROCESS_NUMBERS);
            base.htPropertyCollection.Add("ADMINISTRATIVE_EXPENSE", ADMINISTRATIVE_EXPENSE);

            base.htPropertyCollection.Add("LOB_SUSEPCODE_ID", LOB_SUSEPCODE_ID);
            base.htPropertyCollection.Add("EFFECTIVE_FROM", EFFECTIVE_FROM);
            base.htPropertyCollection.Add("EFFECTIVE_TO", EFFECTIVE_TO);
            
        }//private void PropertyCollection()
        
        
        #region Declare the Property for every data table columns

        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
        public EbixInt32 LOB_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOB_ID"]) == null ? new EbixInt32("LOB_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOB_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOB_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 LOB_ID 
        public EbixString LOB_CODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_CODE"]) == null ? new EbixString("LOB_CODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_CODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_CODE"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString LOB_CODE
        public EbixString LOB_DESC
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_DESC"]) == null ? new EbixString("LOB_DESC") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_DESC"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_DESC"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString LOB_DESC
        public EbixString LOB_CATEGORY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_CATEGORY"]) == null ? new EbixString("LOB_CATEGORY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_CATEGORY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_CATEGORY"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString LOB_CATEGORY
        public EbixString LOB_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_TYPE"]) == null ? new EbixString("LOB_TYPE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_TYPE"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString LOB_TYPE
        public EbixString LOB_PKG
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_PKG"]) == null ? new EbixString("LOB_PKG") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_PKG"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_PKG"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString LOB_PKG
        public EbixString LOB_ACORD_STD
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_ACORD_STD"]) == null ? new EbixString("LOB_ACORD_STD") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_ACORD_STD"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_ACORD_STD"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString LOB_ACORD_STD
        public EbixString DEF_CLAIMS_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DEF_CLAIMS_TYPE"]) == null ? new EbixString("DEF_CLAIMS_TYPE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DEF_CLAIMS_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DEF_CLAIMS_TYPE"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString DEF_CLAIMS_TYPE
        public EbixString LOB_PREFIX
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_PREFIX"]) == null ? new EbixString("LOB_PREFIX") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_PREFIX"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_PREFIX"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString LOB_PREFIX
        public EbixString LOB_SUFFIX
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_SUFFIX"]) == null ? new EbixString("LOB_SUFFIX") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_SUFFIX"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_SUFFIX"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString LOB_SUFFIX
        public EbixInt32 LOB_SEED
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOB_SEED"]) == null ? new EbixInt32("LOB_SEED") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOB_SEED"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOB_SEED"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 LOB_SEED 
        public EbixInt32 MAPPING_LOOKUP_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MAPPING_LOOKUP_ID"]) == null ? new EbixInt32("MAPPING_LOOKUP_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MAPPING_LOOKUP_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MAPPING_LOOKUP_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 MAPPING_LOOKUP_ID 
        public EbixString OVERRIDE_LOB_PREFIX
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OVERRIDE_LOB_PREFIX"]) == null ? new EbixString("OVERRIDE_LOB_PREFIX") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OVERRIDE_LOB_PREFIX"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OVERRIDE_LOB_PREFIX"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString OVERRIDE_LOB_PREFIX
        public EbixString REWRITE_SEED
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REWRITE_SEED"]) == null ? new EbixString("REWRITE_SEED") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REWRITE_SEED"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REWRITE_SEED"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString REWRITE_SEED
        public EbixInt32 SUSEP_LOB_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUSEP_LOB_ID"]) == null ? new EbixInt32("SUSEP_LOB_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUSEP_LOB_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUSEP_LOB_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 SUSEP_LOB_ID 
        public EbixString SUSEP_LOB_CODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SUSEP_LOB_CODE"]) == null ? new EbixString("SUSEP_LOB_CODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SUSEP_LOB_CODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SUSEP_LOB_CODE"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString SUSEP_LOB_CODE
        public EbixString COMMISSION_LEVEL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COMMISSION_LEVEL"]) == null ? new EbixString("COMMISSION_LEVEL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COMMISSION_LEVEL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COMMISSION_LEVEL"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString COMMISSION_LEVEL

        public EbixString APPLICABLE_COMMISSION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["APPLICABLE_COMMISSION"]) == null ? new EbixString("APPLICABLE_COMMISSION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["APPLICABLE_COMMISSION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["APPLICABLE_COMMISSION"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString APPLICABLE_COMMISSION

        public EbixString SUSEP_PROCESS_NUMBERS //Use SUSEP Process Number
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SUSEP_PROCESS_NUMBERS"]) == null ? new EbixString("SUSEP_PROCESS_NUMBERS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SUSEP_PROCESS_NUMBERS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SUSEP_PROCESS_NUMBERS"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString SUSEP_PROCESS_NUMBERS


        public EbixDouble ADMINISTRATIVE_EXPENSE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["ADMINISTRATIVE_EXPENSE"]) == null ? new EbixDouble("ADMINISTRATIVE_EXPENSE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["ADMINISTRATIVE_EXPENSE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["ADMINISTRATIVE_EXPENSE"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble ADMINISTRATIVE_EXPENSE
        

       //Added three more property for Product Susep Code details Table - itrack - 1439
        //LOB_SUSEPCODE_ID
        //EFFECTIVE_FROM
        //EFFECTIVE_TO
        public EbixInt32 LOB_SUSEPCODE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOB_SUSEPCODE_ID"]) == null ? new EbixInt32("LOB_SUSEPCODE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOB_SUSEPCODE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOB_SUSEPCODE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 LOB_SUSEPCODE_ID 
        
        public EbixDateTime EFFECTIVE_FROM
        {
            get { return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_FROM"]) == null ? new EbixDateTime("EFFECTIVE_FROM") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_FROM"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_FROM"]).CurrentValue = Convert.ToDateTime(value);

            }
        }//public EbixDateTime EFFECTIVE_FROM

        public EbixDateTime EFFECTIVE_TO
        {
            get { return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_TO"]) == null ? new EbixDateTime("EFFECTIVE_TO") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_TO"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_TO"]).CurrentValue = Convert.ToDateTime(value);

            }
        }//public EbixDateTime EFFECTIVE_TO

       //Added till here 


        #endregion

      
        
        public DataSet FetchProductMasterInfoUsingLobId()
        {

            DataSet DsData = null;
            try
            {
                base.Proc_FetchData = "PROC_FETCH_MNT_LOB_MASTER";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@LOB_ID", LOB_ID.CurrentValue);
               
                DsData = base.GetData();

            }
            catch (Exception Ex)
            {
                throw new Exception("Error while retriving MNT_LOB_MASTER details : " + Ex);
            }
            finally { }
            return DsData;

        }  //public DataSet FetchProductMasterInfoUsingLobId()

 
        /// <summary>
        /// Update the product info details using LOB_ID
        /// </summary>
        /// <returns></returns>
        public int UpdateProductInfo()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Update_Name = "Proc_UpdateProductInfo";

                //For Transaction Log
                base.TRANS_TYPE_ID = 270;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;

                //end 

                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;
                this.LOB_CODE.IsDBParam = false;
                this.LOB_PKG.IsDBParam = false;
                this.LOB_ACORD_STD.IsDBParam = false;       
                this.DEF_CLAIMS_TYPE.IsDBParam = false;   
                this.MAPPING_LOOKUP_ID.IsDBParam = false;
                this.OVERRIDE_LOB_PREFIX.IsDBParam = false;
                //this.SUSEP_PROCESS_NUMBERS.IsDBParam = false;
                //Off the properties - Added by Pradeep - itrack - 1493
                this.LOB_SUSEPCODE_ID.IsDBParam = false;
                this.EFFECTIVE_FROM.IsDBParam = false;
                this.EFFECTIVE_TO.IsDBParam = false;
                //Added till here 

                returnValue = base.Update();


            }//try
            catch (Exception ex)
            {
                throw new Exception("Error while Updating MNT_LOB_MASTER details : " + ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        }//public int UpdateProductInfo()
        
        #region For Product SUSEP CODE Master Details information By Pradeep Kushwaha

        public DataSet FetchProductSUSEPCODEMasterInfoUsingLobId()
        {

            DataSet DsData = null;
            try
            {
                base.Proc_FetchData = "Proc_GetProductSusepCodeDetails";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@LOB_ID", LOB_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@LOB_SUSEPCODE_ID", LOB_SUSEPCODE_ID.CurrentValue);

                DsData = base.GetData();

            }
            catch (Exception Ex)
            {
                throw new Exception("Error while retriving MNT_LOB_SUSEPCODE_MASTER details : " + Ex);
            }
            finally { }
            return DsData;

        }//public DataSet FetchProductSUSEPCODEMasterInfoUsingLobId()
       
       
        /// <summary>
        /// Update the product Susep Code info details using LOB_ID and LOB_SUSEPCODE_ID
        /// </summary>
        /// <returns></returns>
        public int UpdateProductSUSEPCODEInfo()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Update_Name = "Proc_UpdateProductSusepCodeDetails";

                //For Transaction Log
                base.TRANS_TYPE_ID = 270;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                //end 
                base.CREATED_BY.IsDBParam = false;
                base.CREATED_DATETIME.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;
               
                this.LOB_CODE.IsDBParam = false;
                this.LOB_DESC.IsDBParam = false;
                this.LOB_CATEGORY.IsDBParam = false;
                this.LOB_TYPE.IsDBParam = false;
                this.LOB_PKG.IsDBParam = false;
                this.LOB_ACORD_STD.IsDBParam = false;
                this.DEF_CLAIMS_TYPE.IsDBParam = false;
                this.LOB_PREFIX.IsDBParam = false;
                this.LOB_SUFFIX.IsDBParam = false;
                this.LOB_SEED.IsDBParam = false;
                this.OVERRIDE_LOB_PREFIX.IsDBParam = false;
                this.REWRITE_SEED.IsDBParam = false;
                this.SUSEP_LOB_ID.IsDBParam = false;
                this.COMMISSION_LEVEL.IsDBParam = false;
                this.APPLICABLE_COMMISSION.IsDBParam = false;
                this.SUSEP_PROCESS_NUMBERS.IsDBParam = false;
                this.ADMINISTRATIVE_EXPENSE.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;
                returnValue = base.Update();

            }//try
            catch (Exception ex)
            {
                throw new Exception("Error while Updating MNT_LOB_SUSEPCODE_MASTER details : " + ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        }//public int UpdateProductSUSEPCODEInfo()
        /// <summary>
        /// Insert the product Susep Code info details 
        /// </summary>
        /// <returns></returns>
        public int InsertProductSUSEPCODEInfo()
        {
            int returnResult = 0;
            try
            {

                base.Proc_Add_Name = "Proc_InsertProductSusepCodeDetails";

                base.ReturnIDName = "@LOB_SUSEPCODE_ID";

                //For Transaction Log
                base.TRANS_TYPE_ID = 88;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                //end 

                this.LOB_CODE.IsDBParam = false;
                this.LOB_DESC.IsDBParam = false;
                this.LOB_CATEGORY.IsDBParam = false;
                this.LOB_TYPE.IsDBParam = false;
                this.LOB_PKG.IsDBParam = false;
                this.LOB_ACORD_STD.IsDBParam = false;
                this.DEF_CLAIMS_TYPE.IsDBParam = false;
                this.LOB_PREFIX.IsDBParam = false;
                this.LOB_SUFFIX.IsDBParam = false;
                this.LOB_SEED.IsDBParam = false;
                this.OVERRIDE_LOB_PREFIX.IsDBParam = false;
                this.REWRITE_SEED.IsDBParam = false;
                this.SUSEP_LOB_ID.IsDBParam = false;
                this.COMMISSION_LEVEL.IsDBParam = false;
                this.APPLICABLE_COMMISSION.IsDBParam = false;
                this.SUSEP_PROCESS_NUMBERS.IsDBParam = false;
                this.ADMINISTRATIVE_EXPENSE.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;
                this.LOB_SUSEPCODE_ID.IsDBParam = false;

                ProcReturnValue = true;
                returnResult = base.Save();
                returnResult = Proc_ReturnValue;

                this.LOB_SUSEPCODE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw new Exception("Error while Inserting MNT_LOB_SUSEPCODE_MASTER details : " + ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }//public int InsertProductSUSEPCODEInfo()

        /// <summary>
        /// Delete the product Susep Code info details using LOB_ID and LOB_SUSEPCODE_ID
        /// </summary>
        /// <returns></returns>
        public int DeleteProductSUSEPCODEInfo()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Delete_Name = "Proc_DeleteProductSusepCodeDetails";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@LOB_SUSEPCODE_ID", LOB_SUSEPCODE_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@LOB_ID", LOB_ID.CurrentValue);
                
                //For Transaction Log
                base.TRANS_TYPE_ID = 89;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                //end 
                returnValue = base.Delete();

            }//try
            catch (Exception ex)
            {
                throw new Exception("Error while Deleting MNT_LOB_SUSEPCODE_MASTER details : " + ex);
            }//catch (Ex
            return returnValue;

        }//public int DeleteProductSUSEPCODEInfo()
        #endregion
    }
}
