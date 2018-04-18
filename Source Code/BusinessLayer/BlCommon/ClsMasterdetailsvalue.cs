/******************************************************************************************
<Author					: -		Sneha
<Start Date				: -		31-10-2011
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
    public class ClsMasterdetailsvalue : Cms.BusinessLayer.BlCommon.ClsCommon, IDisposable
    {
        public ClsMasterdetailsvalue()
        {
        }
 
        public int AddMasterValueInformation(ClsMasterDetailValueInfo objMasterDetailinfo, string XmlFullFilePath)
        {
            int returnValue = 0;

            if (objMasterDetailinfo.RequiredTransactionLog)
            {
                objMasterDetailinfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);
                returnValue = objMasterDetailinfo.AddMasterValueDetailInfo();

            }
            return returnValue;
        }

        public int UpdateMasterValueInformation(Cms.Model.Maintenance.ClsMasterDetailValueInfo objMasterDetailinfo, string XmlFullFilePath)
        {
            int returnValue = 0;

            if (objMasterDetailinfo.RequiredTransactionLog)
            {
                objMasterDetailinfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);

                returnValue = objMasterDetailinfo.UpdateMasterValueInformation();

            }
            return returnValue;
        }

        public int ActivateDeactivateMasterValue(ClsMasterDetailValueInfo objMasterDetailInfo, string XmlFullFilePath)
        {
            int returnValue = 0;
            if (objMasterDetailInfo.RequiredTransactionLog)
            {
                objMasterDetailInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);

                if (objMasterDetailInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                    objMasterDetailInfo.TRANS_TYPE_ID = 314;
                else
                    objMasterDetailInfo.TRANS_TYPE_ID = 315;

                returnValue = objMasterDetailInfo.ActivateDeactivateMasterValue();
            }
            return returnValue;
        }

        public ClsMasterDetailValueInfo FetchDataValue(Int32 TYPE_UNIQUE_ID)
        {


            DataSet ds = null;
            ClsMasterDetailValueInfo objMasterDetailInfo = new ClsMasterDetailValueInfo();

            try
            {
                ds = objMasterDetailInfo.FetchDataValue(TYPE_UNIQUE_ID);
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
    }
}
