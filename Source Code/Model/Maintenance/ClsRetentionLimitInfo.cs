using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.Model.Support;
using Cms.EbixDataTypes;
using System.Data;

namespace Cms.Model.Maintenance
{
    [Serializable]
    public class ClsRetentionLimitInfo  : ClsModelBaseClass
    {
        public ClsRetentionLimitInfo()
        {
            this.PropertyCollection();
        }

        #region Delare the add parameter collection for the data wrapper class
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>


        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("RETENTION_LIMIT_ID", RETENTION_LIMIT_ID);
            base.htPropertyCollection.Add("REF_SUSEP_LOB_ID", REF_SUSEP_LOB_ID);
            base.htPropertyCollection.Add("RETENTION_LIMIT", RETENTION_LIMIT);
            
        }
        #endregion

        #region Declare the Property for every data table columns
        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
        public EbixInt32 RETENTION_LIMIT_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RETENTION_LIMIT_ID"]) == null ? new EbixInt32("RETENTION_LIMIT_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RETENTION_LIMIT_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RETENTION_LIMIT_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        public EbixInt32 REF_SUSEP_LOB_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REF_SUSEP_LOB_ID"]) == null ? new EbixInt32("REF_SUSEP_LOB_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REF_SUSEP_LOB_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REF_SUSEP_LOB_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        public EbixDouble RETENTION_LIMIT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RETENTION_LIMIT"]) == null ? new EbixDouble("RETENTION_LIMIT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RETENTION_LIMIT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RETENTION_LIMIT"]).CurrentValue = Convert.ToDouble(value);
            }

        }
        

        #endregion

        #region Methods




        public int AddRetentionLimit()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Add_Name = "Proc_Add_MNT_RETENTION_LIMIT";

                base.ReturnIDName = "@RETENTION_LIMIT_ID"; //Set the Out Parameter

                base.ProcReturnValue = true;
                //create transaction log
                base.TRANS_TYPE_ID = 445;
                base.RequiredTransactionLog = false;
                base.CREATED_DATETIME.IsDBParam = false;          //set the db parameter  
                base.LAST_UPDATED_DATETIME.IsDBParam = false;
                base.MODIFIED_BY.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;
                base.CREATED_BY.IsDBParam = false;
                base.ProcReturnValue = false;
                //base.TRANS_DESC = "Broker Commission Added";
                //end



                this.RETENTION_LIMIT_ID.IsDBParam = false;
                base.MODIFIED_BY.IsDBParam = false;               //set the parameter  
                base.LAST_UPDATED_DATETIME.IsDBParam = false;     //set the parameter    

                returnResult = base.Save();    //Add New record

                RETENTION_LIMIT_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
              
            }
            catch { }
            finally { }

            return returnResult;

        }

        



        public int UpdateRetensionLimitDetail()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Update_Name = "Proc_Update_MNT_RETENTION_LIMIT";         

                //Create Transaction log
                
                base.TRANS_TYPE_ID = 446;    
                base.RequiredTransactionLog = false; //Changed by Aditya for tfs bug # 404
                this.IS_ACTIVE.IsDBParam = false;                           
                base.CREATED_BY.IsDBParam = false;                //set the db parameter  
                base.CREATED_DATETIME.IsDBParam = false;          //set the db parameter  
                base.LAST_UPDATED_DATETIME.IsDBParam = false;
                base.MODIFIED_BY.IsDBParam = false;


                //base.ProcReturnValue = false;  //Changed by Aditya for tfs bug # 404
                returnValue = base.Update();

                // RETENTION_LIMIT_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter   //Changed by Aditya for tfs bug # 404
               

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }            return returnValue;
        }


        public int DeleteRetentionLimit()
        {

            int returnValue = 0;
            try
            {
                base.Proc_Delete_Name = "Proc_DeleteRetention_Limit";
                base.RequiredTransactionLog = false; 
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@RETENTION_LIMIT_ID", RETENTION_LIMIT_ID.CurrentValue);
               

                //For Transaction Log
                //base.TRANS_TYPE_ID = 447;
                
                //End 


                returnValue = base.Delete();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Ex
            return returnValue;



        }


        #endregion


    }

}
