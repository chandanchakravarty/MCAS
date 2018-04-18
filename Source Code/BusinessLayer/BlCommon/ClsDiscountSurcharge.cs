using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon;
using System.Data;

namespace Cms.Blcommon
{
    public class ClsDiscountSurcharge : Cms.Model.ClsCommonModel
    {
        public ClsDiscountSurcharge()
        {
            
        }


        public int AddDiscountSurcharge(ClsDiscountSurchargeInfo objDiscountSurcharge)
        {
            int returnValue = 0;

            if (objDiscountSurcharge.RequiredTransactionLog)
            {
                objDiscountSurcharge.TransactLabel = ClsCommon.MapTransactionLabel("CmsWeb/Maintenance/AddDiscountSurcharge.aspx.resx");
                returnValue = objDiscountSurcharge.ADDDiscountSurchargeData();

            }
            return returnValue;
        }

        /// <summary>
        /// Get the DiscountSurcharge Data using DISCOUNT_ID
        /// </summary>
        /// <param name="MARITIME_ID"></param>
        /// <returns></returns>
        public ClsDiscountSurchargeInfo FetchData(Int32 DISCOUNT_ID)
        {

            DataSet dsCount = null;
            ClsDiscountSurchargeInfo ObjDiscountSurchargeInfo = new ClsDiscountSurchargeInfo();
            try
            {
                dsCount = ObjDiscountSurchargeInfo.FetchData(DISCOUNT_ID);

                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, ObjDiscountSurchargeInfo);
                }

            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return ObjDiscountSurchargeInfo;
        }

        /// <summary>
        /// Delete the DiscountSurcharge Data Based on DISCOUNT_ID 
        /// </summary>
        /// <param name="ObjMaritimeInfo"></param>
        /// <returns></returns>
        public int DeleteDiscountSurcharge(ClsDiscountSurchargeInfo ObjDiscountSurchargeInfo)
        {
            int returnValue = 0;
            if (ObjDiscountSurchargeInfo.RequiredTransactionLog)
            {
                ObjDiscountSurchargeInfo.TransactLabel = ClsCommon.MapTransactionLabel("CmsWeb/Maintenance/AddDiscountSurcharge.aspx.resx");
                returnValue = ObjDiscountSurchargeInfo.DeleteDiscountSurchargeData();
            }
            return returnValue;
        }

        /// <summary>
        /// Activate and Deactivate the Discount Surcharge base on the DISCOUNT_ID and is_Activae 
        /// </summary>
        /// <param name="ObjMaritimeInfo"></param>
        /// <returns></returns>
        public int ActivateDeactivateDiscountSurcharge(ClsDiscountSurchargeInfo objDiscountSurcharge)
        {
            int returnValue = 0;
            if (objDiscountSurcharge.RequiredTransactionLog)
            {
                objDiscountSurcharge.TransactLabel = ClsCommon.MapTransactionLabel("CmsWeb/Maintenance/AddDiscountSurcharge.aspx.resx");

                returnValue = objDiscountSurcharge.ActivateDeactivateDiscountSurcharge();
            }
            return returnValue;
        }

        /// <summary>
        /// Update DiscountSurcharge Date Based On the DiscountId 
        /// </summary>
        /// <param name="ObjMaritimeInfo"></param>
        /// <returns></returns>
        public int UpdateDiscountSurcharge(ClsDiscountSurchargeInfo ObjDiscountSurchargeInfo)
        {
            int returnValue = 0;

            if (ObjDiscountSurchargeInfo.RequiredTransactionLog)
            {
                ObjDiscountSurchargeInfo.TransactLabel = ClsCommon.MapTransactionLabel("CmsWeb/Maintenance/AddDiscountSurcharge.aspx.resx");


                returnValue = ObjDiscountSurchargeInfo.UpdateDiscountSurcharge();

            }//if (ObjMaritimeInfo.RequiredTransactionLog)

            return returnValue;
        }//public int UpdateMariTime(ClsMeritimeInfo ObjMaritimeInfo)

    }
}
