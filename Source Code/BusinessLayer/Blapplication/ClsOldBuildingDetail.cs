/******************************************************************************************
<Author					: - Amit Kr. Mishra
<Start Date				: -	26th November,2011
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
    public  class ClsOldBuildingDetail :Cms.BusinessLayer.BlCommon.ClsCommon
    {
       public ClsOldBuildingDetail()
       {
       }

       public int AddOldBuildingDetailADD(ClsOldBuildingDetailsInfo objOldBuildingDetailsInfo, string XmlFullFilePath)
       {
           int returnValue = 0;

           if (objOldBuildingDetailsInfo.RequiredTransactionLog)
           {
               objOldBuildingDetailsInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);
               returnValue = objOldBuildingDetailsInfo.AddOldBuildingDetailADD();

           }
           return returnValue;
       }
      
       public ClsOldBuildingDetailsInfo FetchData(Int32 OLDBLD_ID, Int32 CUSTOMER_ID, Int32 POLICY_ID, Int32 POLICY_VERSION_ID, Int32 LOCATION_ID, Int32 PREMISES_ID)
       {


           DataSet ds = null;
           ClsOldBuildingDetailsInfo objOldBuildingDetailsInfo = new ClsOldBuildingDetailsInfo();

           try
           {
               ds = objOldBuildingDetailsInfo.FetchData(OLDBLD_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, LOCATION_ID, PREMISES_ID);
               if (ds.Tables[0].Rows.Count != 0)
               {
                   ClsCommon.PopulateEbixPageModel(ds, objOldBuildingDetailsInfo);
               }

           }
           catch (Exception ex)
           {
               throw (ex);
           }
           finally { }
           return objOldBuildingDetailsInfo;


       }

       public ClsOldBuildingDetailsInfo FetchId(string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID, string PREMISES_ID, string LOCATION_ID)
       {


           DataSet ds = null;
           ClsOldBuildingDetailsInfo objOldBuildingDetailsInfo = new ClsOldBuildingDetailsInfo();

           try
           {
               ds = objOldBuildingDetailsInfo.FetchId(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, PREMISES_ID, LOCATION_ID);
               if (ds.Tables[0].Rows.Count != 0)
               {
                   ClsCommon.PopulateEbixPageModel(ds, objOldBuildingDetailsInfo);
               }

           }
           catch (Exception ex)
           {
               throw (ex);
           }
           finally { }
           return objOldBuildingDetailsInfo;


       }

       public int updateOldBuildingDetail(ClsOldBuildingDetailsInfo objOldBuildingDetailsInfo, string XmlFullFilePath)
       {
           int returnValue = 0;

           if (objOldBuildingDetailsInfo.RequiredTransactionLog)
           {
               objOldBuildingDetailsInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);

               returnValue = objOldBuildingDetailsInfo.updateOldBuildingDetail();

           }
           return returnValue;
       }

       public int DelOldBuildingDetail(ClsOldBuildingDetailsInfo objOldBuildingDetailsInfo, string XmlFullFilePath)
       {
           int returnValue = 0;

           if (objOldBuildingDetailsInfo.RequiredTransactionLog)
           {
               objOldBuildingDetailsInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);
               returnValue = objOldBuildingDetailsInfo.DelOldBuildingDetail();

           }
           return returnValue;
       }
    }
}
