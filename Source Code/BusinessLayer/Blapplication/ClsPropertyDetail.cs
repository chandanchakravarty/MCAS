/******************************************************************************************
<Author					: - Sneha
<Start Date				: -	22/11/2011
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
    public class ClsPropertyDetail : Cms.BusinessLayer.BlCommon.ClsCommon
    {
        public ClsPropertyDetail()
        {
        }

        public int AddPropDetailADD(ClsPropertyDetailInfo objPropertyDetailInfo, string XmlFullFilePath)
        {
            int returnValue = 0;

            if (objPropertyDetailInfo.RequiredTransactionLog)
            {
                objPropertyDetailInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);
                returnValue = objPropertyDetailInfo.AddPropDetailADD();

            }
            return returnValue;
        }

        public int updatePropDetail(ClsPropertyDetailInfo objPropertyDetailInfo, string XmlFullFilePath)
        {
            int returnValue = 0;

            if (objPropertyDetailInfo.RequiredTransactionLog)
            {
                objPropertyDetailInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);

                returnValue = objPropertyDetailInfo.updatePropDetail();

            }
            return returnValue;
        }

        public ClsPropertyDetailInfo FetchData(Int32 PROPERTY_ID, string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID,string LOCATION_ID, string PREMISES_ID)
        {


            DataSet ds = null;
            ClsPropertyDetailInfo objPropertyDetailInfo = new ClsPropertyDetailInfo();

            try
            {
                ds = objPropertyDetailInfo.FetchData(PROPERTY_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, LOCATION_ID,PREMISES_ID);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objPropertyDetailInfo);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return objPropertyDetailInfo;


        }

        public ClsPropertyDetailInfo FetchId(string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID, string PREMISES_ID, string LOCATION_ID)
        {


            DataSet ds = null;
            ClsPropertyDetailInfo objPropertyDetailInfo = new ClsPropertyDetailInfo();

            try
            {
                ds = objPropertyDetailInfo.FetchId(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, PREMISES_ID, LOCATION_ID);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objPropertyDetailInfo);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return objPropertyDetailInfo;


        }

        public int DelPropDetail(ClsPropertyDetailInfo objPropertyDetailInfo, string XmlFullFilePath)
        {
            int returnValue = 0;

            if (objPropertyDetailInfo.RequiredTransactionLog)
            {
                objPropertyDetailInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);
                returnValue = objPropertyDetailInfo.DelPropDetail();

            }
            return returnValue;
        }
    }
}
