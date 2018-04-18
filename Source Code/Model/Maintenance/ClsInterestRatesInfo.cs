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
    public class ClsInterestRatesInfo : ClsModelBaseClass
    {
        public ClsInterestRatesInfo()
        {
            this.PropertyCollection();
        }
         #region Delare the add parameter collection for the data wrapper class
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>
        /// 

        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("INTEREST_RATE_ID", INTEREST_RATE_ID);
            base.htPropertyCollection.Add("NO_OF_INSTALLMENTS", NO_OF_INSTALLMENTS);
            base.htPropertyCollection.Add("INTEREST_TYPE", INTEREST_TYPE);
            base.htPropertyCollection.Add("INTEREST_RATE", INTEREST_RATE);
            base.htPropertyCollection.Add("RATE_EFFECTIVE_DATE", RATE_EFFECTIVE_DATE);
            base.htPropertyCollection.Add("MAXIMUM_LIMIT", MAXIMUM_LIMIT);
            base.htPropertyCollection.Add("FEES_PERCENTAGE", FEES_PERCENTAGE);
            base.htPropertyCollection.Add("IOF_PERCENTAGE", IOF_PERCENTAGE);
            base.htPropertyCollection.Add("LOB_ID", LOB_ID);
        }
         #endregion
        #region Declare the Property for every data table columns
        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>

        public EbixInt32 INTEREST_RATE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INTEREST_RATE_ID"]) == null ? new EbixInt32("INTEREST_RATE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INTEREST_RATE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INTEREST_RATE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        public EbixInt32 NO_OF_INSTALLMENTS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["NO_OF_INSTALLMENTS"]) == null ? new EbixInt32("NO_OF_INSTALLMENTS") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["NO_OF_INSTALLMENTS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["NO_OF_INSTALLMENTS"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 INTEREST_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INTEREST_TYPE"]) == null ? new EbixInt32("INTEREST_TYPE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INTEREST_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INTEREST_TYPE"]).CurrentValue = Convert.ToInt32(value);
            }
        }


        public EbixDateTime RATE_EFFECTIVE_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RATE_EFFECTIVE_DATE"]) == null ? new EbixDateTime("RATE_EFFECTIVE_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RATE_EFFECTIVE_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RATE_EFFECTIVE_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }

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

        public EbixDouble MAXIMUM_LIMIT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MAXIMUM_LIMIT"]) == null ? new EbixDouble("MAXIMUM_LIMIT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MAXIMUM_LIMIT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MAXIMUM_LIMIT"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble MAXIMUM_LIMIT

        public EbixDouble FEES_PERCENTAGE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["FEES_PERCENTAGE"]) == null ? new EbixDouble("FEES_PERCENTAGE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["FEES_PERCENTAGE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["FEES_PERCENTAGE"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble FEES_PERCENTAGE

        public EbixDouble IOF_PERCENTAGE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["IOF_PERCENTAGE"]) == null ? new EbixDouble("IOF_PERCENTAGE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["IOF_PERCENTAGE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["IOF_PERCENTAGE"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble IOF_PERCENTAGE

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

        #endregion
        public DataSet FetchData(int INTEREST_RATE_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetInterestRates";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@INTEREST_RATE_ID", INTEREST_RATE_ID);
               // this.MAXIMUM_LIMIT.IsDBParam = false;
                //this.FEES_PERCENTAGE.IsDBParam = false;
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }

        public int AddInterestRateInformation()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Add_Name = "Proc_InsertInterestRates";

                base.ReturnIDName = "@INTEREST_RATE_ID";

                //For Transaction Log
                base.TRANS_TYPE_ID = 322;

                //end 
                this.MAXIMUM_LIMIT.IsDBParam = false;
                this.FEES_PERCENTAGE.IsDBParam = false;
                this.LOB_ID.IsDBParam = false;
                this.IOF_PERCENTAGE.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.INTEREST_RATE_ID.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;

                this.RequiredTransactionLog = false;
                this.ProcReturnValue = true;

                returnResult = base.Save();             

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            //finally { }
            return returnResult;
        }

        public int UpdateInterestRateInformation()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Update_Name = "Proc_UpdateInterestRates";

                //For Transaction Log
                base.TRANS_TYPE_ID = 323;

                //end 
                this.MAXIMUM_LIMIT.IsDBParam = false;
                this.FEES_PERCENTAGE.IsDBParam = false;
                this.IOF_PERCENTAGE.IsDBParam = false;
                this.LOB_ID.IsDBParam = false;
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

        public int DeleteInterestRateInformation()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Delete_Name = "Proc_DeleteInteresrRates";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@INTEREST_RATE_ID", INTEREST_RATE_ID.CurrentValue);

                returnValue = base.Delete();


            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Ex
            return returnValue;

        }

        public int Updatefees()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Update_Name = "Proc_Updatefees";
                
                //For Transaction Log
                base.TRANS_TYPE_ID = 323;

                //end 
                this.INTEREST_RATE_ID.IsDBParam = false;
                this.INTEREST_RATE.IsDBParam = false;
                this.INTEREST_TYPE.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LOB_ID.IsDBParam = false;
                this.IOF_PERCENTAGE.IsDBParam = false;
                this.NO_OF_INSTALLMENTS.IsDBParam = false;
                this.RATE_EFFECTIVE_DATE.IsDBParam = false;
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

        public int UpdateIOFDetails()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Update_Name = "Proc_UpdateIOFDetails";

                //For Transaction Log
                base.TRANS_TYPE_ID = 323;

                //end 
                this.INTEREST_RATE_ID.IsDBParam = false;
                this.INTEREST_RATE.IsDBParam = false;
                this.INTEREST_TYPE.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.MAXIMUM_LIMIT.IsDBParam = false;
                this.FEES_PERCENTAGE.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.NO_OF_INSTALLMENTS.IsDBParam = false;
                this.RATE_EFFECTIVE_DATE.IsDBParam = false;
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

    }
}
