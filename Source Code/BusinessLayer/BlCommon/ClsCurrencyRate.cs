using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon;
using System.Data;

namespace Cms.BusinessLayer.BlCommon
{
   

    public class ClsCurrencyRate : Cms.Model.ClsCommonModel
    {
        public ClsCurrencyRate()
        { 
        }

        public int AddCurrencyRate(ClsCurrencyRateInfo objCurrencyRateinfo)
        {
            int returnValue = 0;

            if (objCurrencyRateinfo.RequiredTransactionLog)
            {
                objCurrencyRateinfo.TransactLabel = ClsCommon.MapTransactionLabel("CmsWeb/Maintenance/AddCurrencyRate.aspx.resx");
                returnValue = objCurrencyRateinfo.AddCurrencyRate();

            }
            return returnValue;
        }

        public int UpdateCurrencyRate(ClsCurrencyRateInfo objCurrencyRateinfo)
        {
            int returnValue = 0;

            if (objCurrencyRateinfo.RequiredTransactionLog)
            {
                objCurrencyRateinfo.TransactLabel = ClsCommon.MapTransactionLabel("CmsWeb/Maintenance/AddCurrencyRate.aspx.resx");


                returnValue = objCurrencyRateinfo.UpdateCurrencyRate();

            }//if (objCurrencyRateinfo.RequiredTransactionLog)

            return returnValue;
        }


        public ClsCurrencyRateInfo FetchData(Int32 CRR_RATE_ID)
        {

            DataSet dsCount = null;
            ClsCurrencyRateInfo objCurrencyRateinfo = new ClsCurrencyRateInfo();
            try
            {
                dsCount = objCurrencyRateinfo.FetchData(CRR_RATE_ID);

                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, objCurrencyRateinfo);
                }

            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return objCurrencyRateinfo;
        }

        public int ActivateDeactivateCurrencyRate(ClsCurrencyRateInfo objCurrencyRateinfo)
        {
            int returnValue = 0;
            if (objCurrencyRateinfo.RequiredTransactionLog)
            {
                objCurrencyRateinfo.TransactLabel = ClsCommon.MapTransactionLabel("CmsWeb/Maintenance/AddCurrencyRate.aspx.resx");

                returnValue = objCurrencyRateinfo.ActivateDeactivateCurrencyRate();
            }
            return returnValue;
        }
    
    }
}
