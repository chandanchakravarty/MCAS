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
    public class ClsClausesInfo  : ClsModelBaseClass
    {
        public ClsClausesInfo()
        {
            this.PropertyCollection();
        }

        #region Delare the add parameter collection for the data wrapper class
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("CLAUSE_ID", CLAUSE_ID);
            //base.htPropertyCollection.Add("TYPE_ID", TYPE_ID);
            base.htPropertyCollection.Add("LOB_ID", LOB_ID);
           base.htPropertyCollection.Add("SUBLOB_ID", SUBLOB_ID);
            base.htPropertyCollection.Add("CLAUSE_TITLE", CLAUSE_TITLE);
            base.htPropertyCollection.Add("CLAUSE_DESCRIPTION", CLAUSE_DESCRIPTION);
            base.htPropertyCollection.Add("CLAUSE_TYPE", CLAUSE_TYPE);
            base.htPropertyCollection.Add("PROCESS_TYPE", PROCESS_TYPE);
           base.htPropertyCollection.Add("ATTACH_FILE_NAME", ATTACH_FILE_NAME);
           base.htPropertyCollection.Add("CLAUSE_CODE", CLAUSE_CODE);

        }
        #endregion

        #region Declare the Property for every data table columns
        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
        public EbixInt32 CLAUSE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAUSE_ID"]) == null ? new EbixInt32("CLAUSE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAUSE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAUSE_ID"]).CurrentValue = Convert.ToInt32(value);
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

        public EbixInt32 SUBLOB_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUBLOB_ID"]) == null ? new EbixInt32("SUBLOB_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUBLOB_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUBLOB_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString CLAUSE_TITLE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_TITLE"]) == null ? new EbixString("CLAUSE_TITLE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_TITLE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_TITLE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString CLAUSE_DESCRIPTION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_DESCRIPTION"]) == null ? new EbixString("CLAUSE_DESCRIPTION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_DESCRIPTION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_DESCRIPTION"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixInt32 CLAUSE_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAUSE_TYPE"]) == null ? new EbixInt32("CLAUSE_TYPE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAUSE_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAUSE_TYPE"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        public EbixInt32 PROCESS_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PROCESS_TYPE"]) == null ? new EbixInt32("PROCESS_TYPE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PROCESS_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PROCESS_TYPE"]).CurrentValue = Convert.ToInt32(value);
            }
        }



        public EbixString ATTACH_FILE_NAME
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ATTACH_FILE_NAME"]) == null ? new EbixString("ATTACH_FILE_NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ATTACH_FILE_NAME"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ATTACH_FILE_NAME"]).CurrentValue = Convert.ToString(value);
            }
        }
       
        public EbixString CLAUSE_CODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_CODE"]) == null ? new EbixString("CLAUSE_CODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_CODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_CODE"]).CurrentValue = Convert.ToString(value);
            }
        }
        #endregion



        #region Methods


        public int ADDClausesData()
        {
            int returnResult = 0;
            try
            {

                base.Proc_Add_Name = "Proc_InsertClausesDetails";

                base.ReturnIDName = "@CLAUSE_ID";

                //For Transaction Log
                base.TRANS_TYPE_ID = 146;
                base.CLIENT_ID = 0;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = 0;
                base.POLICYVERTRACKING_ID =0;
                //base.TRANS_DESC = "Clauses added";
                //end 

                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CLAUSE_ID.IsDBParam = false;

                base.ProcReturnValue = true;

                returnResult = base.Save();

                if (returnResult > 0)
                {
                    this.CLAUSE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
                }
                else
                {
                    return base.Proc_ReturnValue;
                }
               

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }

        public DataSet FetchData(int CLAUSE_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetClausesDetailsData";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CLAUSE_ID", CLAUSE_ID);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }
        public DataSet FetchDataForValidation(int LOB_ID, int SUBLOB_ID)
        {
           
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "[Proc_FetchClausesValidate]";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@LOB_ID", LOB_ID);
                base.htGetDataParamCollections.Add("@SUBLOB_ID", SUBLOB_ID);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }

        /// <summary>
        /// Delete DiscountSurcharge Data 
        /// </summary>
        /// <returns></returns>
        public int DeleteClauses()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Delete_Name = "Proc_DeleteClausesDetailsData";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CLAUSE_ID", CLAUSE_ID.CurrentValue);
                
                //For Transaction Log
                base.TRANS_TYPE_ID = 148;
                base.CLIENT_ID = 0;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = 0;
                base.POLICYVERTRACKING_ID = 0;
                //base.TRANS_DESC = "Clauses Deleted";
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
        /// Deactivate Clauses
        /// </summary>
        /// <returns></returns>
        public int ActivateDeactivateClausesDetails()
        {
            int returnValue = 0;
            try
            {
                base.Proc_ActivateDeactivate_Name = "[Proc_ActivateDeactivateClausesDetail]";

                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CLAUSE_ID", CLAUSE_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@IS_ACTIVE", IS_ACTIVE.CurrentValue);


                //For Transaction Log
               
                base.CLIENT_ID = 0;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = 0;
                base.POLICYVERTRACKING_ID =0;

                if (IS_ACTIVE.CurrentValue.ToString() == "Y")
                {
                    base.TRANS_TYPE_ID = 149;
                    //base.TRANS_DESC = "MariTime Activated";
                }
                else
                {
                    base.TRANS_TYPE_ID = 150;
                    //base.TRANS_DESC = "Clauses Deactivated";
                }
                
                returnValue = base.ActivateDeactivate();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return returnValue;

        }

        /// <summary>
        /// Update Clauses
        /// </summary>
        /// <returns></returns>
        public int UpdateClausesDetails()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Update_Name = "Proc_UpdateClausesDetails";

                //For Transaction Log
                base.TRANS_TYPE_ID = 147;
                base.CLIENT_ID = 0;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = 0;
                base.POLICYVERTRACKING_ID = 0;

                //base.TRANS_DESC = "Clauses Updated";
                //end 

                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;

                base.ProcReturnValue = true;
                returnValue = base.Update();

               
                

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }

            if (returnValue > 0)
            { 
                // After successfull execution of procedure this returns -1
                return returnValue;
            }
            else
            {
                // After successfull execution of procedure this returns
                // user defined value in the procedure
                return base.Proc_ReturnValue;
            }
           
        }

        public int UpdateAttachment(string pol, string Attach)
        {
            int returnValue = 0;

            try
            {
                base.Proc_Update_Name = "Proc_UpdateClausesAttachment";

              
               


                CLAUSE_ID.CurrentValue = int.Parse(pol);
                ATTACH_FILE_NAME.CurrentValue = Attach;
                MODIFIED_BY.IsDBParam = false;
                ATTACH_FILE_NAME.IsDBParam = true;
               // IS_ACTIVE.IsDBParam = false;
                CREATED_BY.IsDBParam = false;
                CREATED_DATETIME.IsDBParam = false;
                LAST_UPDATED_DATETIME.IsDBParam = false;
                CLAUSE_DESCRIPTION.IsDBParam = false;
                CLAUSE_TITLE.IsDBParam = false;
                CLAUSE_TYPE.IsDBParam = false;
               // SUSEP_LOB_ID.IsDBParam = false;
                CLAUSE_CODE.IsDBParam = false;
                base.ReturnIDName = "";
                SUBLOB_ID.IsDBParam = false;
                PROCESS_TYPE.IsDBParam = false;
                LOB_ID.IsDBParam = false;
                IS_ACTIVE.IsDBParam = false;
                //base.htGetDataParamCollections.Add("@POL_CLAUSE_ID", pol);
                //base.htGetDataParamCollections.Add("@ATTACH_FILE_NAME", Attach);
                //base.TRANS_TYPE_ID = 118;
                //base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                //base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                //base.POLICYID = POLICY_ID.CurrentValue;
                //base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                RequiredTransactionLog = false;
                returnValue = base.Update();

            }
            catch
            {
                returnValue = 0;
            }

            return returnValue;

        }

        #endregion
    }
}
