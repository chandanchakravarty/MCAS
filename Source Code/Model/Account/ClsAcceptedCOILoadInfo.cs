
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

namespace Cms.Model.Account
{
    /// <summary>
    /// Database Model for Risk Information.
    /// </summary>
    [Serializable]
    public class ClsAcceptedCOILoadInfo : ClsModelBaseClass
    {

        #region Declare the Type object of every Property

      


        #endregion

        /// <summary>
        /// Initialize the default value 
        /// </summary>
        public ClsAcceptedCOILoadInfo()
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
            /* if (ClauseName == base.SelectClause)
             {
                 base.htPropertyCollection.Add("PERIL_ID", PERIL_ID);
             }*/
            //if (ClauseName == base.AddClause || ClauseName == "")
            //{

            base.htPropertyCollection.Add("IMPORT_REQUEST_FILE_ID", IMPORT_REQUEST_FILE_ID);
            base.htPropertyCollection.Add("IMPORT_REQUEST_ID", IMPORT_REQUEST_ID);
            base.htPropertyCollection.Add("IMPORT_FILE_NAME", IMPORT_FILE_NAME);
            base.htPropertyCollection.Add("IMPORT_FILE_PATH", IMPORT_FILE_PATH);
            base.htPropertyCollection.Add("IMPORT_FILE_TYPE", IMPORT_FILE_TYPE);
            base.htPropertyCollection.Add("SUBMITTED_BY", SUBMITTED_BY);
            base.htPropertyCollection.Add("FILE_IMPORTED_DATE", FILE_IMPORTED_DATE);
            base.htPropertyCollection.Add("FILE_IMPORTED_BY", FILE_IMPORTED_BY);
            base.htPropertyCollection.Add("REQUEST_DESC", REQUEST_DESC);
            base.htPropertyCollection.Add("REQUEST_STATUS", REQUEST_STATUS);
            base.htPropertyCollection.Add("SUBMITTED_DATE", SUBMITTED_DATE);
            base.htPropertyCollection.Add("HAS_ERRORS", HAS_ERRORS);
            base.htPropertyCollection.Add("IS_DELETED", IS_DELETED);
            base.htPropertyCollection.Add("DISPLAY_FILE_NAME", DISPLAY_FILE_NAME);
            base.htPropertyCollection.Add("IMPORT_FILE_GROUP_TYPE", IMPORT_FILE_GROUP_TYPE);
            base.htPropertyCollection.Add("PROCESS_TYPE", PROCESS_TYPE);

             
            
        }//private void PropertyCollection()s


        #endregion

        #region Declare the Property for every data table columns

        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
        public EbixInt32 IMPORT_REQUEST_FILE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IMPORT_REQUEST_FILE_ID"]) == null ? new EbixInt32("IMPORT_REQUEST_FILE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IMPORT_REQUEST_FILE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IMPORT_REQUEST_FILE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 IMPORT_REQUEST_FILE_ID 


        public EbixInt32 SUBMITTED_BY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUBMITTED_BY"]) == null ? new EbixInt32("SUBMITTED_BY") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUBMITTED_BY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUBMITTED_BY"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 IMPORT_REQUEST_FILE_ID 


        public EbixInt32 IMPORT_REQUEST_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IMPORT_REQUEST_ID"]) == null ? new EbixInt32("IMPORT_REQUEST_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IMPORT_REQUEST_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IMPORT_REQUEST_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 IMPORT_REQUEST_ID

      
        public EbixString IMPORT_FILE_NAME
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IMPORT_FILE_NAME"]) == null ? new EbixString("IMPORT_FILE_NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IMPORT_FILE_NAME"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IMPORT_FILE_NAME"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString IMPORT_FILE_PATH
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IMPORT_FILE_PATH"]) == null ? new EbixString("IMPORT_FILE_PATH") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IMPORT_FILE_PATH"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IMPORT_FILE_PATH"]).CurrentValue = Convert.ToString(value);
            }
        }
      
        public EbixInt32 IMPORT_FILE_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IMPORT_FILE_TYPE"]) == null ? new EbixInt32("IMPORT_FILE_TYPE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IMPORT_FILE_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IMPORT_FILE_TYPE"]).CurrentValue = Convert.ToInt32(value);
            }
        }

       

         public EbixDateTime FILE_IMPORTED_DATE
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["FILE_IMPORTED_DATE"]) == null ? new EbixDateTime("FILE_IMPORTED_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["FILE_IMPORTED_DATE"]);
             }
             set
             {
                 ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["FILE_IMPORTED_DATE"]).CurrentValue = Convert.ToDateTime(value);
             }
         }

         public EbixInt32 FILE_IMPORTED_BY
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["FILE_IMPORTED_BY"]) == null ? new EbixInt32("FILE_IMPORTED_BY") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["FILE_IMPORTED_BY"]);
             }
             set
             {
                 ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["FILE_IMPORTED_BY"]).CurrentValue = Convert.ToInt32(value);
             }
         }//public EbixInt32 IMPORT_REQUEST_FILE_ID 

         public EbixString REQUEST_DESC
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REQUEST_DESC"]) == null ? new EbixString("REQUEST_DESC") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REQUEST_DESC"]);
             }
             set
             {
                 ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REQUEST_DESC"]).CurrentValue = Convert.ToString(value);
             }
         }

         public EbixString REQUEST_STATUS
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REQUEST_STATUS"]) == null ? new EbixString("REQUEST_STATUS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REQUEST_STATUS"]);
             }
             set
             {
                 ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REQUEST_STATUS"]).CurrentValue = Convert.ToString(value);
             }
         }
         public EbixDateTime SUBMITTED_DATE
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["SUBMITTED_DATE"]) == null ? new EbixDateTime("SUBMITTED_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["SUBMITTED_DATE"]);
             }
             set
             {
                 ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["SUBMITTED_DATE"]).CurrentValue = Convert.ToDateTime(value);
             }
         }

         public EbixString HAS_ERRORS
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["HAS_ERRORS"]) == null ? new EbixString("HAS_ERRORS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["HAS_ERRORS"]);
             }
             set
             {
                 ((Cms.EbixDataTypes.EbixString)htPropertyCollection["HAS_ERRORS"]).CurrentValue = Convert.ToString(value);
             }
         }

         public EbixString IS_DELETED
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_DELETED"]) == null ? new EbixString("IS_DELETED") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_DELETED"]);
             }
             set
             {
                 ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_DELETED"]).CurrentValue = Convert.ToString(value);
             }
         }

         public EbixString DISPLAY_FILE_NAME
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DISPLAY_FILE_NAME"]) == null ? new EbixString("DISPLAY_FILE_NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DISPLAY_FILE_NAME"]);
             }
             set
             {
                 ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DISPLAY_FILE_NAME"]).CurrentValue = Convert.ToString(value);
             }
         }

         public EbixInt32 IMPORT_FILE_GROUP_TYPE
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IMPORT_FILE_GROUP_TYPE"]) == null ? new EbixInt32("IMPORT_FILE_GROUP_TYPE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IMPORT_FILE_GROUP_TYPE"]);
             }
             set
             {
                 ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IMPORT_FILE_GROUP_TYPE"]).CurrentValue = Convert.ToInt32(value);
             }
         }//public EbixInt32 IMPORT_FILE_GROUP_TYPE 

         public EbixString PROCESS_TYPE
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROCESS_TYPE"]) == null ? new EbixString("PROCESS_TYPE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROCESS_TYPE"]);
             }
             set
             {
                 ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROCESS_TYPE"]).CurrentValue = Convert.ToString(value);
             }
         }



        #endregion

        /// <summary>
        /// To get the location id and location details to display in dropdown
        /// </summary>
        /// <param name="ClaimID"></param>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
         public DataSet GetImportRequestDetails(Int32 IMPORT_REQUEST_ID, Int32 LANG_ID)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "PROC_MIG_GET_IMPORT_REQUEST_FILES";

                base.htGetDataParamCollections.Clear();

                base.htGetDataParamCollections.Add("@IMPORT_REQUEST_ID", IMPORT_REQUEST_ID);
                base.htGetDataParamCollections.Add("@LANG_ID", LANG_ID);      
                ds = base.GetData();
            }//try
            catch (Exception ex)
            { throw (ex); }//catch (Exception ex)
            return ds;
        }


         public DataSet GetAcceptedCOLDetail(Int32 IMPORT_REQUEST_ID, Int32 IMPORT_SERIAL_NO, Int32 LANG_ID, String MODE)
         {
             DataSet ds = null;
             try
             {
                 base.Proc_FetchData = "PROC_MIG_GET_EXCEPTION_DETAILS";

                 base.htGetDataParamCollections.Clear();

                 base.htGetDataParamCollections.Add("@IMPORT_REQUEST_ID", IMPORT_REQUEST_ID);
                 base.htGetDataParamCollections.Add("@IMPORT_SERIAL_NO", IMPORT_SERIAL_NO);
                 base.htGetDataParamCollections.Add("@LANG_ID", LANG_ID);
                 base.htGetDataParamCollections.Add("@MODE", MODE);
                 ds = base.GetData();
             }//try
             catch (Exception ex)
             { throw (ex); }//catch (Exception ex)
             return ds;
         }
        public int AddImportRequestFile()
        {
            int returnResult = 0;
            try
            {
                this.TRANS_TYPE_ID = 290;
                base.Proc_Add_Name = "PROC_MIG_INSERT_IMPORT_REQUEST_FILES";


                base.ReturnIDName = "@IMPORT_REQUEST_FILE_ID";

             
                //end 
                this.IMPORT_REQUEST_FILE_ID.IsDBParam = false;
                this.IMPORT_FILE_NAME.IsDBParam = true;
                this.DISPLAY_FILE_NAME.IsDBParam = true;
                this.IMPORT_FILE_PATH.IsDBParam = true;
                this.IMPORT_FILE_TYPE.IsDBParam = true;
                this.REQUEST_DESC.IsDBParam = false;
                this.FILE_IMPORTED_DATE.IsDBParam = true;
                this.FILE_IMPORTED_BY.IsDBParam = true;
                this.IMPORT_REQUEST_ID.IsDBParam = true;
                this.CREATED_BY.IsDBParam = false;
                this.REQUEST_STATUS.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.IMPORT_FILE_GROUP_TYPE.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.SUBMITTED_DATE.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.HAS_ERRORS.IsDBParam = false;
                this.IS_DELETED.IsDBParam = false;
                this.SUBMITTED_BY.IsDBParam = false;
                this.PROCESS_TYPE.IsDBParam = false;
                //this.FILE_IMPORTED_DATE.IsDBParam = true;
                //this.FILE_IMPORTED_BY.IsDBParam = true;
              //  this.PERIL_ID.IsDBParam = false;

                returnResult = base.Save();

                this.IMPORT_REQUEST_FILE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }
     
        public int AddImportRequest()
        {
            int returnResult = 0;
            try
            {
                this.TRANS_TYPE_ID = 290;
                base.Proc_Add_Name = "PROC_MIG_INSERT_IMPORT_REQUEST";

                base.ReturnIDName = "@IMPORT_REQUEST_ID";
                //end 
                this.IMPORT_REQUEST_FILE_ID.IsDBParam = false;
           
                this.IMPORT_FILE_NAME.IsDBParam = false;
                this.DISPLAY_FILE_NAME.IsDBParam = false;
                this.IMPORT_FILE_PATH.IsDBParam = false;
                this.IMPORT_FILE_TYPE.IsDBParam = false;
                this.REQUEST_STATUS.IsDBParam = false;
                this.FILE_IMPORTED_DATE.IsDBParam = false;
                this.FILE_IMPORTED_BY.IsDBParam = false;
                this.SUBMITTED_BY.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.REQUEST_DESC.IsDBParam = true;
                this.IMPORT_REQUEST_ID.IsDBParam = false;
                this.SUBMITTED_DATE.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.IMPORT_FILE_GROUP_TYPE.IsDBParam = false;
                this.HAS_ERRORS.IsDBParam = false;
                this.IS_DELETED.IsDBParam = false;
                this.PROCESS_TYPE.IsDBParam = false;
                //  this.PERIL_ID.IsDBParam = false;

                returnResult = base.Save();

                this.IMPORT_REQUEST_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }

        public int DeleteFile()
        {
            int RetValue = 0;

            try
            {
                base.Proc_Delete_Name = "PROC_MIG_DELETE_IMPORT_REQUEST_FILE";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@IMPORT_REQUEST_FILE_ID", IMPORT_REQUEST_FILE_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@IMPORT_REQUEST_ID", IMPORT_REQUEST_ID.CurrentValue);
                RetValue = base.Delete();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return RetValue;
        }//public DataSet FetchData(int Peril_ID)


        public int DeleteImportRequest()
        {
            int RetValue = 0;

            try
            {
                base.Proc_FetchData = "Proc_GetRiskInformation";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@IMPORT_REQUEST_ID", IMPORT_REQUEST_ID.CurrentValue);
                RetValue = base.Delete();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return RetValue;
        }//public DataSet FetchData(int Peril_ID)


        public DataSet Getfiletypes(int lang_id, int IMPORT_REQUEST_ID)
        {
          
            DataSet ds = null;

            try
            {
                base.Proc_FetchData = "PROC_MIG_GET_FILE_TYPES";
               
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@IMPORT_REQUEST_ID", IMPORT_REQUEST_ID);
                base.htGetDataParamCollections.Add("@LANG_ID", lang_id);

                ds = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return ds;
        }//public DataSet FetchData(int Peril_ID)


        public int UpdateImportRequest()
        {
            int RetValue = 0;

            try
            {
                base.Proc_Update_Name = "PROC_MIG_START_IMPORT_REQUEST_PROCESS";
                

                this.FILE_IMPORTED_BY.IsDBParam = false;
                this.IMPORT_FILE_PATH.IsDBParam = false;
                this.DISPLAY_FILE_NAME.IsDBParam = false;
                this.REQUEST_DESC.IsDBParam = false;
                this.REQUEST_STATUS.IsDBParam = false;
                this.FILE_IMPORTED_DATE.IsDBParam = false;
                this.IMPORT_REQUEST_FILE_ID.IsDBParam = false;
                this.IMPORT_FILE_NAME.IsDBParam = false;
                this.IMPORT_FILE_TYPE.IsDBParam = false;
              //  this.IMPORT_REQUEST_ID.IsDBParam = true;
                this.CREATED_BY.IsDBParam = false;
                this.IMPORT_FILE_GROUP_TYPE.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.SUBMITTED_DATE.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.HAS_ERRORS.IsDBParam = false;
                this.IS_DELETED.IsDBParam = false;
                this.SUBMITTED_BY.IsDBParam = false;
                base.ProcReturnValue = true;
                this.PROCESS_TYPE.IsDBParam = false;
                //base.htGetDataParamCollections.Clear();
                //base.htGetDataParamCollections.Add("@IMPORT_REQUEST_ID", IMPORT_REQUEST_ID.CurrentValue);
                RetValue = base.Update();
                

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return RetValue;
        }//public DataSet FetchData(int Peril_ID)


        // ADDED BY SANTOSH KR GAUTAM ON 18 AUG 2011 
        // =============================================================
        // BLOCK FOR INITIAL LOAD RELATED FUNCTIONS
        // =============================================================

        #region Initial Load 

        public DataSet GetInitialLoadFileTypes(int lang_id, int IMPORT_REQUEST_ID, int FileGroupType)
        {

            DataSet ds = null;

            try
            {
                base.Proc_FetchData = "PROC_MIG_IL_GET_FILE_TYPES";

                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@IMPORT_REQUEST_ID", IMPORT_REQUEST_ID);
                base.htGetDataParamCollections.Add("@FILE_GROUP", FileGroupType);
                base.htGetDataParamCollections.Add("@LANG_ID", lang_id);

                ds = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return ds;
        }

        public int AddInialLoadImportRequestFile()
        {
            int returnResult = 0;
            try
            {
                this.TRANS_TYPE_ID = 290;
                base.Proc_Add_Name = "PROC_MIG_IL_INSERT_IMPORT_REQUEST_FILES";


                base.ReturnIDName = "@IMPORT_REQUEST_FILE_ID";

                
                //end 
                this.IMPORT_REQUEST_FILE_ID.IsDBParam = false;
                this.IMPORT_FILE_NAME.IsDBParam = true;
                this.DISPLAY_FILE_NAME.IsDBParam = true;
                this.IMPORT_FILE_PATH.IsDBParam = true;
                this.IMPORT_FILE_TYPE.IsDBParam = true;
                this.REQUEST_DESC.IsDBParam = false;
                this.FILE_IMPORTED_DATE.IsDBParam = true;
                this.FILE_IMPORTED_BY.IsDBParam = true;
                this.IMPORT_REQUEST_ID.IsDBParam = true;
                this.CREATED_BY.IsDBParam = false;
                this.REQUEST_STATUS.IsDBParam = false;
                this.IMPORT_FILE_GROUP_TYPE.IsDBParam = true;
                this.CREATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.SUBMITTED_DATE.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.HAS_ERRORS.IsDBParam = false;
                this.IS_DELETED.IsDBParam = false;
                this.SUBMITTED_BY.IsDBParam = false;
                this.PROCESS_TYPE.IsDBParam = false;
                //this.FILE_IMPORTED_DATE.IsDBParam = true;
                //this.FILE_IMPORTED_BY.IsDBParam = true;
                //  this.PERIL_ID.IsDBParam = false;

                returnResult = base.Save();

                this.IMPORT_REQUEST_FILE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }

        public int AddInialLoadImportRequest()
        {
            int returnResult = 0;
            try
            {
                this.TRANS_TYPE_ID = 290;
                base.Proc_Add_Name = "PROC_MIG_IL_INSERT_IMPORT_REQUEST";

                base.ReturnIDName = "@IMPORT_REQUEST_ID";
                //end 
                this.IMPORT_REQUEST_FILE_ID.IsDBParam = false;

                this.IMPORT_FILE_NAME.IsDBParam = false;
                this.DISPLAY_FILE_NAME.IsDBParam = false;
                this.IMPORT_FILE_PATH.IsDBParam = false;
                this.IMPORT_FILE_TYPE.IsDBParam = false;
                this.IMPORT_FILE_GROUP_TYPE.IsDBParam = false;
                this.REQUEST_STATUS.IsDBParam = false;
                this.FILE_IMPORTED_DATE.IsDBParam = false;
                this.FILE_IMPORTED_BY.IsDBParam = false;
                this.SUBMITTED_BY.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.REQUEST_DESC.IsDBParam = true;
                this.IMPORT_REQUEST_ID.IsDBParam = false;
                this.SUBMITTED_DATE.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.IMPORT_FILE_GROUP_TYPE.IsDBParam = false;
                this.HAS_ERRORS.IsDBParam = false;
                this.IS_DELETED.IsDBParam = false;
                this.PROCESS_TYPE.IsDBParam = false;
                //  this.PERIL_ID.IsDBParam = false;

                returnResult = base.Save();

                this.IMPORT_REQUEST_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }
        //Added by Pradeep Kushwaha on 27-10-2011
        /// <summary>
        /// Get the Initial Load Layout Coumns details based on Column Type  SUSEP Code
        /// </summary>
        /// <param name="COLUMN_TYPE"></param>
        /// <param name="SUSEP_LOB_CODE"></param>
        /// <returns></returns>
        public DataSet GetInialLoadLoadLayOutCoumnsDetail(Int32 COLUMN_TYPE, String SUSEP_LOB_CODE)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "PROC_MIG_IL_GET_LAYOUT_COLUMNS";

                base.htGetDataParamCollections.Clear();

                base.htGetDataParamCollections.Add("@COLUMN_TYPE", COLUMN_TYPE);
                base.htGetDataParamCollections.Add("@SUSEP_LOB_CODE",  SUSEP_LOB_CODE);
                ds = base.GetData();
            }//try
            catch (Exception ex)
            { throw (ex); }//catch (Exception ex)
            return ds;
        }//public DataSet GetInialLoadLoadLayOutCoumnsDetail(Int32 COLUMN_TYPE, String SUSEP_LOB_CODE)
        //Added till here 


        public DataSet GetInialLoadImportRequestDetails(Int32 IMPORT_REQUEST_ID, Int32 LANG_ID)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "PROC_MIG_IL_GET_IMPORT_REQUEST_FILES";

                base.htGetDataParamCollections.Clear();

                base.htGetDataParamCollections.Add("@IMPORT_REQUEST_ID", IMPORT_REQUEST_ID);
                base.htGetDataParamCollections.Add("@LANG_ID", LANG_ID);
                ds = base.GetData();
            }//try
            catch (Exception ex)
            { throw (ex); }//catch (Exception ex)
            return ds;
        }


        public int DeleteInialLoadFile()
        {
            int RetValue = 0;

            try
            {
                base.Proc_Delete_Name = "PROC_MIG_IL_DELETE_IMPORT_REQUEST_FILE";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@IMPORT_REQUEST_FILE_ID", IMPORT_REQUEST_FILE_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@IMPORT_REQUEST_ID", IMPORT_REQUEST_ID.CurrentValue);
                RetValue = base.Delete();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return RetValue;
        }
        /// <summary>
        /// Delete inital load commited policy details data
        /// </summary>
        /// <param name="_IMPORT_REQUEST_ID"></param>
        /// <param name="_IMPORT_SERIAL_NO"></param>
        /// <param name="_CALLED_FOR"></param>
        /// <returns></returns>
        //Added by Pradeep Kushwaha on 14-Nov-2011
        public int DeleteInialLoadCommitedDetails(int _IMPORT_REQUEST_ID, string _IMPORT_SERIAL_NO, string _CALLED_FOR)
        {
            int RetValue = 0;

            try
            {
                base.Proc_Delete_Name = "PROC_MIG_IL_DELETE_POLICY_DETAILS";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@IMPORT_REQUEST_ID", _IMPORT_REQUEST_ID);
                base.htGetDataParamCollections.Add("@IMPORT_SERIAL_NO", _IMPORT_SERIAL_NO);
                base.htGetDataParamCollections.Add("@CALLED_FOR", _CALLED_FOR);
                RetValue = base.Delete();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return RetValue;
        }//public int DeleteInialLoadCommitedDetails(int _IMPORT_REQUEST_ID, string _IMPORT_SERIAL_NO, string _CALLED_FOR)


        public int UpdateInialLoadImportRequest()
        {
            int RetValue = 0;

            try
            {
                base.Proc_Update_Name = "PROC_MIG_IL_START_IMPORT_REQUEST_PROCESS";


                this.FILE_IMPORTED_BY.IsDBParam = false;
                this.IMPORT_FILE_PATH.IsDBParam = false;
                this.DISPLAY_FILE_NAME.IsDBParam = false;
                this.REQUEST_DESC.IsDBParam = false;
                this.REQUEST_STATUS.IsDBParam = false;
                this.FILE_IMPORTED_DATE.IsDBParam = false;
                this.IMPORT_REQUEST_FILE_ID.IsDBParam = false;
                this.IMPORT_FILE_NAME.IsDBParam = false;
                this.IMPORT_FILE_TYPE.IsDBParam = false;
                //  this.IMPORT_REQUEST_ID.IsDBParam = true;
                this.CREATED_BY.IsDBParam = false;
                this.IMPORT_FILE_GROUP_TYPE.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.SUBMITTED_DATE.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.HAS_ERRORS.IsDBParam = false;
                this.IS_DELETED.IsDBParam = false;
                this.SUBMITTED_BY.IsDBParam = false;
                base.ProcReturnValue = true;
                this.PROCESS_TYPE.IsDBParam = false;
                //base.htGetDataParamCollections.Clear();
                //base.htGetDataParamCollections.Add("@IMPORT_REQUEST_ID", IMPORT_REQUEST_ID.CurrentValue);
                RetValue = base.Update();


            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return RetValue;
        }


        public DataSet GetInitialLoadImportDetails(Int32 IMPORT_REQUEST_ID, Int32 IMPORT_SERIAL_NO, Int32 LANG_ID, String MODE)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "PROC_MIG_IL_GET_EXCEPTION_DETAILS";

                base.htGetDataParamCollections.Clear();

                base.htGetDataParamCollections.Add("@IMPORT_REQUEST_ID", IMPORT_REQUEST_ID);
                base.htGetDataParamCollections.Add("@IMPORT_SERIAL_NO", IMPORT_SERIAL_NO);
                base.htGetDataParamCollections.Add("@LANG_ID", LANG_ID);
                base.htGetDataParamCollections.Add("@MODE", MODE);
                ds = base.GetData();
            }//try
            catch (Exception ex)
            { throw (ex); }//catch (Exception ex)
            return ds;
        }


        public DataSet FetchChecksum(int IMPORT_REQUEST_ID, int Lang_Id)
        {
            DataSet dstemp = null;

            try
            {
                base.Proc_FetchData = "PROC_MIG_IL_GET_CHECKSUM_SUMMARY";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@IMPORT_REQUEST_ID", IMPORT_REQUEST_ID);
                base.htGetDataParamCollections.Add("@Lang_Id", Lang_Id);

                dstemp = base.GetData();

                if (dstemp != null)
                {
                    return dstemp;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }

        public int StartImportProcess()
        {
            int RetValue = 0;

            try
            {
                base.Proc_Update_Name = "PROC_MIG_IL_START_IMPORT_REQUEST_PROCESS";

                this.FILE_IMPORTED_BY.IsDBParam = false;
                this.IMPORT_FILE_PATH.IsDBParam = false;
                this.DISPLAY_FILE_NAME.IsDBParam = false;
                this.REQUEST_DESC.IsDBParam = false;
                this.REQUEST_STATUS.IsDBParam = false;
                this.FILE_IMPORTED_DATE.IsDBParam = false;
                this.IMPORT_REQUEST_FILE_ID.IsDBParam = false;
                this.IMPORT_FILE_NAME.IsDBParam = false;
                this.IMPORT_FILE_TYPE.IsDBParam = false;
                //  this.IMPORT_REQUEST_ID.IsDBParam = true;
                this.CREATED_BY.IsDBParam = false;
                this.IMPORT_FILE_GROUP_TYPE.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.SUBMITTED_DATE.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.HAS_ERRORS.IsDBParam = false;
                this.IS_DELETED.IsDBParam = false;
                this.SUBMITTED_BY.IsDBParam = false;
                                
                base.ProcReturnValue = true;
                //base.htGetDataParamCollections.Clear();
                //base.htGetDataParamCollections.Add("@IMPORT_REQUEST_ID", IMPORT_REQUEST_ID.CurrentValue);
                RetValue = base.Update();


            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return RetValue;
        }
        


        #endregion
      
    }
}

