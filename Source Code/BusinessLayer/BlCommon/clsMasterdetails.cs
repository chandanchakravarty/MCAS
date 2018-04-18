/******************************************************************************************
<Author					: -		Sneha
<Start Date				: -		25-10-2011
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Maintenance;
using System.Data;
using Cms.Model.Support;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Maintenance.Security;

namespace Cms.BusinessLayer.BlCommon
{
    public class clsMasterdetails : Cms.BusinessLayer.BlCommon.ClsCommon, IDisposable
    {
        public clsMasterdetails()
        {
        }
        public int AddMasterSetupInformation(clsMasterDetailInfo objMasterDetailinfo, string XmlFullFilePath)
        {
            int returnValue = 0;

            if (objMasterDetailinfo.RequiredTransactionLog)
            {
                objMasterDetailinfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);
                returnValue = objMasterDetailinfo.AddMastersetupDetailInfo();

            }
            return returnValue;
        }

        public int UpdateMastersetUpInformation(Cms.Model.Maintenance.clsMasterDetailInfo objMasterDetailinfo, string XmlFullFilePath)
        {
            int returnValue = 0;

            if (objMasterDetailinfo.RequiredTransactionLog)
            {
                objMasterDetailinfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);

                returnValue = objMasterDetailinfo.UpdateMastersetUpInformation();

            }
            return returnValue;
        }

        public clsMasterDetailInfo FetchData(Int32 TYPE_UNIQUE_ID)
        {


            DataSet ds = null;
            clsMasterDetailInfo objMasterDetailInfo = new clsMasterDetailInfo();

            try
            {
                ds = objMasterDetailInfo.FetchData(TYPE_UNIQUE_ID);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objMasterDetailInfo);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return objMasterDetailInfo;


        }

        public int ActivateDeactivateMasterDetail(clsMasterDetailInfo objMasterDetailInfo, string XmlFullFilePath)
        {
            int returnValue = 0;
            if (objMasterDetailInfo.RequiredTransactionLog)
            {
                objMasterDetailInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);

                if (objMasterDetailInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                    objMasterDetailInfo.TRANS_TYPE_ID = 314;
                else
                    objMasterDetailInfo.TRANS_TYPE_ID = 315;

                returnValue = objMasterDetailInfo.ActivateDeactivateMasterDetail();
            }
            return returnValue;
        }

    }
}
