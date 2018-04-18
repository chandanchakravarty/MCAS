/******************************************************************************************
<Author				: - Lalit Kumar Chauhan
<Start Date			: -	22-03-2010
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

namespace Cms.Model.Policy
{
    [Serializable]
    public class ClsPolicyRemunerationInfo : ClsModelBaseClass
    {

        #region Declare the Type object of every Property

        private String _ACTION = string.Empty;        

        #endregion //Declare Property For the Action is Update or Save

        private const string POL_LOCATIONS = "POL_REMUNERATION";

        public ClsPolicyRemunerationInfo()
        {          
            this.PropertyCollection();
        }

        #region  Property Collection
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);
            base.htPropertyCollection.Add("BROKER_ID", BROKER_ID);
            base.htPropertyCollection.Add("COMMISSION_PERCENT", COMMISSION_PERCENT);
            base.htPropertyCollection.Add("COMMISSION_TYPE", COMMISSION_TYPE);
            base.htPropertyCollection.Add("BRANCH", BRANCH);
            base.htPropertyCollection.Add("REMUNERATION_ID", REMUNERATION_ID);
            base.htPropertyCollection.Add("LEADER", LEADER);
            base.htPropertyCollection.Add("AMOUNT", AMOUNT);
            base.htPropertyCollection.Add("NAME", NAME);
            base.htPropertyCollection.Add("PRODUCT_RISK_ID", PRODUCT_RISK_ID);
            base.htPropertyCollection.Add("CO_APPLICANT_ID", CO_APPLICANT_ID);
            
        }
        #endregion



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

      
     
        /// <summary>
        /// Model For DataBase REMUNERATION_ID
        /// </summary>
        public EbixInt32 REMUNERATION_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REMUNERATION_ID"]) == null ? new EbixInt32("REMUNERATION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REMUNERATION_ID"]);             
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REMUNERATION_ID"]).CurrentValue = Convert.ToInt32(value);
                
            }
        }
        /// <summary>
        /// Model For DataBase POLICY_ID
        /// </summary>
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
        /// <summary>
        /// Model For DataBase POLICY_VERSION_ID
        /// </summary>
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
        /// <summary>
        /// Model For DataBase BROKER_ID
        /// </summary>
        public EbixInt32 BROKER_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BROKER_ID"]) == null ? new EbixInt32("BROKER_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BROKER_ID"]);                             
                
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BROKER_ID"]).CurrentValue = Convert.ToInt32(value);
                
            }
        }
        /// <summary>
        /// Model For DataBase COMMISSION_PERCENT
        /// </summary>
        public EbixDouble COMMISSION_PERCENT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["COMMISSION_PERCENT"]) == null ? new EbixDouble("COMMISSION_PERCENT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["COMMISSION_PERCENT"]);                                             
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["COMMISSION_PERCENT"]).CurrentValue = Convert.ToDouble(value);
            }
        }
        /// <summary>
        /// Model For DataBase COMMISSION_TYPE
        /// </summary>
        public EbixInt32 COMMISSION_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COMMISSION_TYPE"]) == null ? new EbixInt32("COMMISSION_TYPE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COMMISSION_TYPE"]);                                             
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COMMISSION_TYPE"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        /// <summary>
        /// Model For DataBase BRANCH
        /// </summary>
        public EbixInt32 BRANCH
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BRANCH"]) == null ? new EbixInt32("BRANCH") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BRANCH"]);
                
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BRANCH"]).CurrentValue = Convert.ToInt32(value);

            }
        }

        /// <summary>
        /// Model For DataBase LEADER
        /// </summary>
        public EbixInt32 LEADER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LEADER"]) == null ? new EbixInt32("LEADER") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LEADER"]);

            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LEADER"]).CurrentValue = Convert.ToInt32(value);

            }
        }

        /// <summary>
        /// Model For DataBase AMOUNT
        /// </summary>
        public EbixDouble AMOUNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["AMOUNT"]) == null ? new EbixDouble("AMOUNT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["AMOUNT"]);

            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["AMOUNT"]).CurrentValue = Convert.ToDouble(value);

            }
        }

        
        /// <summary>
        /// Model For DataBase AMOUNT
        /// </summary>
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
        /// <summary>
        /// Model For DataBase PRODUCT_RISK_ID
        /// </summary>
        public EbixInt32 PRODUCT_RISK_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PRODUCT_RISK_ID"]) == null ? new EbixInt32("PRODUCT_RISK_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PRODUCT_RISK_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PRODUCT_RISK_ID"]).CurrentValue = Convert.ToInt32(value);

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

        

        

        /// <summary>
        /// Model Property for check gridview action
        /// </summary>
       // public string ACTION
       // {
       //     get
       //     {
       //         return _ACTION;
       //     }
       //     set
       //     {
       //         _ACTION = value;
       //     }
       //} 
        
        #endregion

        /// <summary>
        /// Add Broker With Commission Percentage
        /// </summary>
        /// <returns>Int RenumerationID</returns>        
        public int AddPlicyRemuneration()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Add_Name = "Proc_InsertRemuneration";

                base.ReturnIDName = "@REMUNERATION_ID"; //Set the Out Parameter

                base.ProcReturnValue = true;  
                //create transaction log
                base.TRANS_TYPE_ID = 154;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //base.TRANS_DESC = "Broker Commission Added";
                //end


                
                this.REMUNERATION_ID.IsDBParam = false;               
                base.MODIFIED_BY.IsDBParam = false;               //set the parameter  
                base.LAST_UPDATED_DATETIME.IsDBParam = false;     //set the parameter    

                returnResult = base.Save();    //Add New record

                REMUNERATION_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
                returnResult = Proc_ReturnValue;
            }
            catch  { }
            finally { }

            return returnResult;

        }//int AddPlicyRemuneration()


        /// <summary>
        /// Update Broker Commission & Branch Int UpdatePlicyRemuneration()
        /// </summary>
        /// <returns>Int Status</returns>
        public int UpdatePlicyRemuneration()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Update_Name = "Proc_UpdateRemuneration";
                //base.ReturnIDName = "@REMUNERATION_ID";

                //Create Transaction log
                base.TRANS_TYPE_ID = 155;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //base.TRANS_DESC = "Broker Commission Updated";
                //End


             
                this.IS_ACTIVE.IsDBParam = false;
                //this.POLICY_ID.IsDBParam = false;                 //set the db parameter 
                //this.CUSTOMER_ID.IsDBParam = false;               //set the db parameter 
                //this.POLICY_VERSION_ID.IsDBParam = false;         //set the db parameter                  
                base.CREATED_BY.IsDBParam = false;                //set the db parameter  
                base.CREATED_DATETIME.IsDBParam = false;          //set the db parameter  
                //this.BROKER_ID.IsDBParam = false;
                base.ProcReturnValue = true;                
                returnValue = base.Update();
               
               REMUNERATION_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
               returnValue = Proc_ReturnValue;
              
            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        } //int UpdatePlicyRemuneration()


        /// <summary>
        /// Delete Broker Commission From Remuneration
        /// </summary>
        /// <returns>Int Status</returns>
        public int DeletePlicyRemuneration()
        {

            int returnValue = 0;
            try
            {
                base.Proc_Delete_Name = "Proc_DeleteRemuneration";

                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@REMUNERATION_ID", REMUNERATION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);

                //For Transaction Log
                base.TRANS_TYPE_ID = 156;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //End 

          
                returnValue = base.Delete();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Ex
            return returnValue;

           

        }//int DeletePlicyRemuneration

        public DataSet FetchData(int REMUNERATION_ID, int CUSTOMER_ID, int POLICY_ID,int POLICY_VERSION_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetRemuneration";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@REMUNERATION_ID", REMUNERATION_ID);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }


    }
}
