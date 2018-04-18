/******************************************************************************************
<Author					: - Sneha
<Start Date				: -	25/11/2011
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
   public class ClsContractorsDetail : Cms.BusinessLayer.BlCommon.ClsCommon
    {
       public ClsContractorsDetail()
       {
       }
       public int AddConrtDetailADD(ClsContractorsDetailInfo objContractorsDetailInfo, string XmlFullFilePath)
       {
           int returnValue = 0;

           if (objContractorsDetailInfo.RequiredTransactionLog)
           {
               objContractorsDetailInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);
               returnValue = objContractorsDetailInfo.AddConrtDetailADD();

           }
           return returnValue;
       }

       public int updateConrtDetail(ClsContractorsDetailInfo objContractorsDetailInfo, string XmlFullFilePath)
       {
           int returnValue = 0;

           if (objContractorsDetailInfo.RequiredTransactionLog)
           {
               objContractorsDetailInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);

               returnValue = objContractorsDetailInfo.updateConrtDetail();

           }
           return returnValue;
       }

       public ClsContractorsDetailInfo FetchData(Int32 CONTRACTOR_ID, Int32 CUSTOMER_ID, Int32 POLICY_ID, Int32 POLICY_VERSION_ID, Int32 LOCATION_ID, Int32 PREMISES_ID)
       {


           DataSet ds = null;
           ClsContractorsDetailInfo objContractorsDetailInfo = new ClsContractorsDetailInfo();

           try
           {
               ds = objContractorsDetailInfo.FetchData(CONTRACTOR_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, LOCATION_ID, PREMISES_ID);
               if (ds.Tables[0].Rows.Count != 0)
               {
                   ClsCommon.PopulateEbixPageModel(ds, objContractorsDetailInfo);
               }

           }
           catch (Exception ex)
           {
               throw (ex);
           }
           finally { }
           return objContractorsDetailInfo;


       }

       public ClsContractorsDetailInfo FetchId(string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID, string PREMISES_ID, string LOCATION_ID)
       {


           DataSet ds = null;
           ClsContractorsDetailInfo objContractorsDetailInfo = new ClsContractorsDetailInfo();

           try
           {
               ds = objContractorsDetailInfo.FetchId(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, PREMISES_ID, LOCATION_ID);
               if (ds.Tables[0].Rows.Count != 0)
               {
                   ClsCommon.PopulateEbixPageModel(ds, objContractorsDetailInfo);
               }

           }
           catch (Exception ex)
           {
               throw (ex);
           }
           finally { }
           return objContractorsDetailInfo;


       }

       public int DelConrtDetail(ClsContractorsDetailInfo objContractorsDetailInfo, string XmlFullFilePath)
       {
           int returnValue = 0;

           if (objContractorsDetailInfo.RequiredTransactionLog)
           {
               objContractorsDetailInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);
               returnValue = objContractorsDetailInfo.DelConrtDetail();

           }
           return returnValue;
       }
    }
}
