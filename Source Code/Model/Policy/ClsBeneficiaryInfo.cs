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
    public class ClsBeneficiaryInfo : ClsModelBaseClass, IDisposable
    {
                /// <summary>
        /// Initialize the default value 
        /// </summary>
        public ClsBeneficiaryInfo()
        {
            //this.SetColumnsName();
            this.PropertyCollection();
        }
        public void Dispose()
        {
            System.GC.ReRegisterForFinalize(this);
        }      
         private void PropertyCollection()
        {
            /* if (ClauseName == base.SelectClause)
             {
                 base.htPropertyCollection.Add("PERIL_ID", PERIL_ID);
             }*/
            //if (ClauseName == base.AddClause || ClauseName == "")
            //{
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);
            base.htPropertyCollection.Add("RISK_ID", RISK_ID);
            base.htPropertyCollection.Add("BENEFICIARY_ID", BENEFICIARY_ID);
            base.htPropertyCollection.Add("BENEFICIARY_NAME", BENEFICIARY_NAME);
            base.htPropertyCollection.Add("BENEFICIARY_SHARE", BENEFICIARY_SHARE);
            base.htPropertyCollection.Add("BENEFICIARY_RELATION", BENEFICIARY_RELATION);
           
        }//private void PropertyCollection()s

         #region Declare the Property for every data table columns

         /// <summary>
         /// Declare the Property for every data table columns 
         /// </summary>
        #endregion



          #region Database schema details


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


         public EbixInt32 RISK_ID
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ID"]) == null ? new EbixInt32("RISK_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ID"]);
             }
             set
             {
                 ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ID"]).CurrentValue = Convert.ToInt32(value);
             }
         }//public EbixInt32 POLICY_ID

         public EbixInt32 BENEFICIARY_ID
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BENEFICIARY_ID"]) == null ? new EbixInt32("BENEFICIARY_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BENEFICIARY_ID"]);
             }
             set
             {
                 ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BENEFICIARY_ID"]).CurrentValue = Convert.ToInt32(value);
             }
         }//public EbixInt32 BENEFICIARY_ID 


        // model for database field BENEFICIARY_NAME(string)
         public EbixString BENEFICIARY_NAME
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BENEFICIARY_NAME"]) == null ? new EbixString("BENEFICIARY_NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BENEFICIARY_NAME"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BENEFICIARY_NAME"]).CurrentValue = Convert.ToString(value);
            }
        }

        // model for database field BENEFICIARY_SHARE(string)
        public EbixDouble BENEFICIARY_SHARE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["BENEFICIARY_SHARE"]) == null ? new EbixDouble("BENEFICIARY_SHARE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["BENEFICIARY_SHARE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["BENEFICIARY_SHARE"]).CurrentValue = Convert.ToDouble(value);
            }
        }

        // model for database field BENEFICIARY_RELATION(string)
        public EbixString BENEFICIARY_RELATION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BENEFICIARY_RELATION"]) == null ? new EbixString("BENEFICIARY_RELATION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BENEFICIARY_RELATION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BENEFICIARY_RELATION"]).CurrentValue = Convert.ToString(value);
            }
        }
        #endregion

        public int AddBeneficiaryInformation()
        {
            int returnResult = 0;
            try
            {

                base.Proc_Add_Name = "Proc_InsertBeneficiary";
                base.ProcReturnValue = true;  
                base.ReturnIDName = "@BENEFICIARY_ID";
               
                //For Transaction Log
                base.TRANS_TYPE_ID = 322;

                //end 
                this.IS_ACTIVE.IsDBParam = false;
                this.BENEFICIARY_ID.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.BENEFICIARY_ID.IsDBParam = false;       
                returnResult = base.Save();
                returnResult = Proc_ReturnValue;
                this.BENEFICIARY_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
                

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnResult;
        }//public int AddNamedParilsData()
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldPriorPolicyInfo">Model object having old information</param>
        /// <param name="objPriorPolicyInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        public int UpdateBeneficiaryInformation()
        {
            int returnValue = 0;
            try
            {


                base.Proc_Update_Name = "Proc_UpdateBeneficiary";

                base.ProcReturnValue = true;  
                //For Transaction Log
                base.TRANS_TYPE_ID = 323;

                //end 
                //this.CUSTOMER_ID.IsDBParam = false;
                //this.POLICY_ID.IsDBParam = false;
                //this.POLICY_VERSION_ID.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;

                returnValue = base.Update();
                returnValue = Proc_ReturnValue;

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnValue;
        }
        public DataSet FetchData()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetBeneficiaryInformation";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@RISK_ID", RISK_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@BENEFICIARY_ID", BENEFICIARY_ID.CurrentValue);
                
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }//public DataSet FetchData(int Peril_ID)


        public DataSet GetTotalShareofBeneficiary(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int RISK_ID, int BENEFICIARY_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetTotalBeneficiaryShare";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                base.htGetDataParamCollections.Add("@RISK_ID", RISK_ID);
                base.htGetDataParamCollections.Add("@BENEFICIARY_ID", BENEFICIARY_ID);

                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }//public DataSet FetchData(int Peril_ID)

        public int DeleteBeneficiaryInformation()
        {
            int returnValue = 0;
            try
            {


                base.Proc_Delete_Name = "Proc_DeleteBeneficiaryInfo";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@RISK_ID", RISK_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@BENEFICIARY_ID", BENEFICIARY_ID.CurrentValue);
                


                ////For Transaction Log
                //base.TRANS_TYPE_ID = 323;

                ////end 
                ////this.CUSTOMER_ID.IsDBParam = false;
                ////this.POLICY_ID.IsDBParam = false;
                ////this.POLICY_VERSION_ID.IsDBParam = false;
                //this.CREATED_BY.IsDBParam = false;
                //this.CREATED_DATETIME.IsDBParam = false;
                //base.IS_ACTIVE.IsDBParam = false;

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

