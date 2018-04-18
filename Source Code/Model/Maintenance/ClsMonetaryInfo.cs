
/******************************************************************************************
<Author				: - Aditya Goel
<Start Date			: - 22 DEC, 2010
<End Date			: -	
<Description		: - Model Class for Monetary Information page functionality.
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
    public class ClsMonetaryInfo : ClsModelBaseClass
    {

               /// <summary>
        /// Initialize the default value 
        /// </summary>
        public ClsMonetaryInfo()
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

            base.htPropertyCollection.Add("ROW_ID", ROW_ID);
            base.htPropertyCollection.Add("DATE", DATE);
            base.htPropertyCollection.Add("INFLATION_RATE", INFLATION_RATE);
            base.htPropertyCollection.Add("INTEREST_RATE", INTEREST_RATE);
           
        }//private void PropertyCollection()s


        #endregion

        #region Declare the Property for every data table columns

        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
        public EbixInt32 ROW_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ROW_ID"]) == null ? new EbixInt32("ROW_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ROW_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ROW_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 ROW_ID 

        public EbixDouble INFLATION_RATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["INFLATION_RATE"]) == null ? new EbixDouble("INFLATION_RATE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["INFLATION_RATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["INFLATION_RATE"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble INFLATION_RATE

        public EbixDouble INTEREST_RATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["INTEREST_RATE"]) == null ? new EbixDouble("INTEREST_RATE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["INTEREST_RATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["INTEREST_RATE"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble INTEREST_RATE


        
                         

        public EbixDateTime DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["DATE"]) == null ? new EbixDateTime("DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }


        #endregion
       


        public int AddMonetaryInformation()
        {
            int returnResult = 0;
            try
            {
                
                base.Proc_Add_Name = "Proc_InsertMonetaryIndex";

                base.ReturnIDName = "@ROW_ID";

                //For Transaction Log
                base.TRANS_TYPE_ID = 322;
               
                //end 
               this.IS_ACTIVE.IsDBParam = false;
                this.ROW_ID.IsDBParam = false;
               this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
               
                returnResult = base.Save();

                this.ROW_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnResult;
        }//public int AddNamedParilsData()


        public int UpdateMonetaryInformation()
        {
            int returnValue = 0;
            try
            {

              
                base.Proc_Update_Name = "Proc_UpdateMonetaryIndex";
                

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
        }//public int AddNamedParilsData()

        public DataSet FetchData()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetMonetaryInformation";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@ROW_ID", ROW_ID.CurrentValue);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }//public DataSet FetchData(int Peril_ID)

    }
}

