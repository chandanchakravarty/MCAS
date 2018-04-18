using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Cms.Model;
using Cms.Model.Support;
using Cms.EbixDataTypes;

namespace Cms.Model.Maintenance
{
     [Serializable]
    public class ClsRuleCollectioninfo : ClsModelBaseClass
  
    {
        public ClsRuleCollectioninfo()
        {
            this.PropertyCollection();
        }
         #region Delare the add parameter collection for the data wrapper class
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>


        private void PropertyCollection() {
           base.htPropertyCollection.Add("RULE_COLLECTION_ID", RULE_COLLECTION_ID);
            base.htPropertyCollection.Add("LOB_ID", LOB_ID);
            base.htPropertyCollection.Add("COUNTRY_ID", COUNTRY_ID);
            base.htPropertyCollection.Add("STATE_ID", STATE_ID);
            base.htPropertyCollection.Add("SUB_LOB_ID", SUB_LOB_ID); 
            base.htPropertyCollection.Add("EFFECTIVE_FROM", EFFECTIVE_FROM); 
            base.htPropertyCollection.Add("EFFECTIVE_TO", EFFECTIVE_TO);
            base.htPropertyCollection.Add("VALIDATION_ORDER", VALIDATION_ORDER);
            base.htPropertyCollection.Add("VALIDATE_NEXT_IF_FAILED", VALIDATE_NEXT_IF_FAILED);
            base.htPropertyCollection.Add("RULE_XML_PATH", RULE_XML_PATH);
            
        }
       #endregion

        #region Declare the Property for every data table columns
        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
        public EbixInt32 RULE_COLLECTION_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RULE_COLLECTION_ID"]) == null ? new EbixInt32("RULE_COLLECTION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RULE_COLLECTION_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RULE_COLLECTION_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }
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

        public EbixString STATE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["STATE_ID"]) == null ? new EbixString("STATE_ID") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["STATE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["STATE_ID"]).CurrentValue = Convert.ToString(value);
            }
        }

        //public EbixInt32 SUB_LOB_ID
        //{
        //    get
        //    {
        //        return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUB_LOB_ID"]) == null ? new EbixInt32("SUB_LOB_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUB_LOB_ID"]);
        //    }
        //    set
        //    {
        //        ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUB_LOB_ID"]).CurrentValue = Convert.ToInt32(value);
        //    }
        //}

        public EbixInt32 SUB_LOB_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUB_LOB_ID"]) == null ? new EbixInt32("SUB_LOB_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUB_LOB_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUB_LOB_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }


       public EbixDateTime EFFECTIVE_FROM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_FROM"]) == null ? new EbixDateTime("EFFECTIVE_FROM") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_FROM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_FROM"]).CurrentValue = Convert.ToDateTime(value);
            }
        }
        public EbixDateTime EFFECTIVE_TO
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_TO"]) == null ? new EbixDateTime("EFFECTIVE_TO") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_TO"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_TO"]).CurrentValue = Convert.ToDateTime(value);
            }
        }

        public EbixDouble VALIDATION_ORDER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["VALIDATION_ORDER"]) == null ? new EbixDouble("VALIDATION_ORDER") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["VALIDATION_ORDER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["VALIDATION_ORDER"]).CurrentValue = Convert.ToDouble(value);
            }
        }


        public EbixString VALIDATE_NEXT_IF_FAILED
        {
            get { return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VALIDATE_NEXT_IF_FAILED"]) == null ? new EbixString("VALIDATE_NEXT_IF_FAILED") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VALIDATE_NEXT_IF_FAILED"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VALIDATE_NEXT_IF_FAILED"]).CurrentValue = Convert.ToString(value);
            }
        }


        public EbixString RULE_XML_PATH
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RULE_XML_PATH"]) == null ? new EbixString("RULE_XML_PATH") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RULE_XML_PATH"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RULE_XML_PATH"]).CurrentValue = Convert.ToString(value);
            }
        }

        #endregion


        public DataSet FetchData(int RULE_COLLECTION_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetRuleCollectionInformation";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@RULE_COLLECTION_ID", RULE_COLLECTION_ID);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }

          public int AddRuleCollectionInformation()
        {
            int returnResult = 0;
            try
            {
                
                base.Proc_Add_Name = "Proc_InsertRuleCollection";

                base.ReturnIDName = "@RULE_COLLECTION_ID";

                //For Transaction Log
                base.TRANS_TYPE_ID = 322;
               
                //end 
               
                this.IS_ACTIVE.IsDBParam = false;
                this.RULE_COLLECTION_ID.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
         
                this.RequiredTransactionLog = false;
                this.ProcReturnValue = true;
                
               
                returnResult = base.Save();
              

                //this.RULE_COLLECTION_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            //finally { }
            return returnResult;
        }

        public int UpdateRuleCollectionInformation()
        {
            int returnValue = 0;
            try
            {

              
                base.Proc_Update_Name = "Proc_UpdateRuleCollection";
                

                //For Transaction Log
                base.TRANS_TYPE_ID = 323;
                
                //end 

                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false; 
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

        public int DeleteRuleCollection()
        {
            int returnValue = 0;
            try
            {


                base.Proc_Delete_Name = "Proc_DeleteRuleCollection";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@RULE_COLLECTION_ID", RULE_COLLECTION_ID.CurrentValue);

                returnValue = base.Delete();


            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Ex
            return returnValue;

        }


    }
}

