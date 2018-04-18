
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
    public class ClsVictimInfo : ClsModelBaseClass
    {
                /// <summary>
        /// Initialize the default value 
        /// </summary>
        public ClsVictimInfo()
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
           

        

            base.htPropertyCollection.Add("VICTIM_ID", VICTIM_ID);
            base.htPropertyCollection.Add("CLAIM_ID", CLAIM_ID);
            base.htPropertyCollection.Add("NAME", NAME);
            base.htPropertyCollection.Add("STATUS", STATUS);
            base.htPropertyCollection.Add("INJURY_TYPE", INJURY_TYPE);
            base.htPropertyCollection.Add("PAGE_MODE", PAGE_MODE);
        

            
        }//private void PropertyCollection()s
        
        #endregion

        #region Declare the Property for every data table columns

        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
      

       
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

        public EbixString NAME
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NAME"]) == null ? new EbixString("NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NAME"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NAME"]).CurrentValue = Convert.ToString(value);
            }
        }
      
        public EbixInt32 STATUS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["STATUS"]) == null ? new EbixInt32("STATUS") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["STATUS"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["STATUS"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 INJURY_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INJURY_TYPE"]) == null ? new EbixInt32("INJURY_TYPE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INJURY_TYPE"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INJURY_TYPE"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString PAGE_MODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PAGE_MODE"]) == null ? new EbixString("PAGE_MODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PAGE_MODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PAGE_MODE"]).CurrentValue = Convert.ToString(value);
            }
        }
        
        #endregion


        public DataSet GetClaimVictimList(int ClaimID)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "Proc_GetClaimVictimList";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CLAIM_ID", ClaimID);
             
                ds = base.GetData();

            }//try
            catch (Exception ex)
            { throw (ex); }//catch (Exception ex)
            return ds;
        }

        public int DeleteClaimVictim()
        {
            
            int returnResult = 0;
            try
            {
                this.TRANS_TYPE_ID = 336;
                base.Proc_Add_Name = "Proc_DeleteClaimVictim";

                base.ReturnIDName = "@ERROR_CODE";
               

             
                base.RECORDED_BY = CREATED_BY.CurrentValue;
           
              
                //end 
                this.IS_ACTIVE.IsDBParam = false;
               
                this.NAME.IsDBParam = false;
                this.STATUS.IsDBParam = false;
                this.INJURY_TYPE.IsDBParam = false;
                
                this.MODIFIED_BY.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;              
                this.IS_ACTIVE.IsDBParam = false;
                this.PAGE_MODE.IsDBParam = false;       
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
                base.Proc_FetchData = "Proc_GetClaimVictimByID";
                base.htGetDataParamCollections.Clear();

                base.htGetDataParamCollections.Add("@VICTIM_ID", VICTIM_ID.CurrentValue);        
                base.htGetDataParamCollections.Add("@CLAIM_ID", CLAIM_ID.CurrentValue);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }//public DataSet FetchData(int Peril_ID)
        

       

        public int AddClaimVictim()
        {
            int returnResult = 0;
            try
            {
                this.TRANS_TYPE_ID = 334;
                base.Proc_Add_Name = "Proc_InsertClaimVictim";

                base.ReturnIDName = "@VICTIM_ID";


             
                base.RECORDED_BY = CREATED_BY.CurrentValue;
           
              
                //end 
                this.IS_ACTIVE.IsDBParam = false;


                this.VICTIM_ID.IsDBParam = false;                
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                //  this.PERIL_ID.IsDBParam = false;

                returnResult = base.Save();

                this.VICTIM_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
                returnResult = base.ReturnIDNameValue; 

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }

  

        public int UpdateClaimVictim()
        {
            int returnValue = 0;
            try
            {
               
                base.htGetDataParamCollections.Clear();
                base.ReturnIDName = "@ERROR_CODE";
                //For Transaction Log               
                this.TRANS_TYPE_ID = 335;
                base.Proc_Update_Name = "Proc_UpdateClaimVictim";
                

                base.RECORDED_BY = CREATED_BY.CurrentValue;
                this.CREATED_BY.IsDBParam = false;
                this.NAME.IsDBParam = true;
                this.STATUS.IsDBParam = true;
                this.INJURY_TYPE.IsDBParam = true;    
                this.CREATED_DATETIME.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = true;
                this.PAGE_MODE.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = true;
                returnValue = base.Update();

                if (base.ReturnIDNameValue != 0)
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
