/******************************************************************************************
<Author					: - Sneha
<Start Date				: -	23/11/2011
<End Date				: -	
<Description			: - 
<Review Date			: - 
<Reviewed By			: - 	
Modification History
********************************************************************************************/


using System;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data;
using System.Configuration;
using Cms.Model.Application;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using Cms.Model;
using Cms.Model.Policy;
using System.Collections;
using Cms.Model.Account;

namespace Cms.BusinessLayer.BlApplication
{
    public class ClsWarehouseDetail : Cms.BusinessLayer.BlCommon.ClsCommon
    {
        public ClsWarehouseDetail()
        { 
        }
        public int AddWareHouseDetlADD(ClsWarehouseDetailInfo objWarehouseDetailInfo, string XmlFullFilePath)
        {
            int returnValue = 0;

            if (objWarehouseDetailInfo.RequiredTransactionLog)
            {
                objWarehouseDetailInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);
                returnValue = objWarehouseDetailInfo.AddWareHouseDetlADD();

            }
            return returnValue;
        }

        public int updateWareHouseDetl(ClsWarehouseDetailInfo objWarehouseDetailInfo, string XmlFullFilePath)
        {
            int returnValue = 0;

            if (objWarehouseDetailInfo.RequiredTransactionLog)
            {
                objWarehouseDetailInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);

                returnValue = objWarehouseDetailInfo.updateWareHouseDetl();

            }
            return returnValue;
        }

        public ClsWarehouseDetailInfo FetchData(Int32 WAREHOUSE_ID, string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID, string LOCATION_ID, string PREMISES_ID)
        {


            DataSet ds = null;
            ClsWarehouseDetailInfo objWarehouseDetailInfo = new ClsWarehouseDetailInfo();

            try
            {
                ds = objWarehouseDetailInfo.FetchData(WAREHOUSE_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, LOCATION_ID, PREMISES_ID);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objWarehouseDetailInfo);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return objWarehouseDetailInfo;


        }

        public ClsWarehouseDetailInfo FetchId(string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID, string PREMISES_ID, string LOCATION_ID)
        {


            DataSet ds = null;
            ClsWarehouseDetailInfo objWarehouseDetailInfo = new ClsWarehouseDetailInfo();

            try
            {
                ds = objWarehouseDetailInfo.FetchId(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, PREMISES_ID, LOCATION_ID);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objWarehouseDetailInfo);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return objWarehouseDetailInfo;


        }

        public int DelWareHouseDetl(ClsWarehouseDetailInfo objWarehouseDetailInfo, string XmlFullFilePath)
        {
            int returnValue = 0;

            if (objWarehouseDetailInfo.RequiredTransactionLog)
            {
                objWarehouseDetailInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);
                returnValue = objWarehouseDetailInfo.DelWareHouseDetl();

            }
            return returnValue;
        }
    }
}
