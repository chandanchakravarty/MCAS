using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.EbixDataTypes;
using Cms.Model.Support;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using Cms.Model;

namespace Cms.Model.Account
{
    public class ClsAcceptedCOILoadApplicationInfo : ClsModelBaseClass
    {
            
        
        #region Declare the Type object of every Property

      


        #endregion

        /// <summary>
        /// Initialize the default value 
        /// </summary>
        public ClsAcceptedCOILoadApplicationInfo()
        {
            //this.SetColumnsName();
            this.PropertyCollection();
        }


        
        
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
            {
               
            }
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
        }
        public EbixInt32 IMPORT_SERIAL_NO
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IMPORT_SERIAL_NO"]) == null ? new EbixInt32("IMPORT_SERIAL_NO") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IMPORT_SERIAL_NO"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IMPORT_SERIAL_NO"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        public EbixString MODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MODE"]) == null ? new EbixString("MODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MODE"]).CurrentValue = Convert.ToString(value);
            }
        }
    }
}





