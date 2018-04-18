/******************************************************************************************
<Author				: - Santosh KUmar Gautam
<Start Date			: -	22-11-2010
<End Date			: -	
<Description		: - 
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: -  
<Modified By		: -  
<Purpose			: - 
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Claims;
using System.Data;
using Cms.Model.Support;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Xml;


namespace Cms.BusinessLayer.BLClaims
 {
    public class ClsLitigationInformation :   Cms.BusinessLayer.BLClaims.ClsClaims
    {

        public ClsLitigationInformation()
          { }

        public int AddLitigationInformation(ClsLitigationInfo objLitigationInfo)
        {
            int returnValue = 0;

            if (objLitigationInfo.RequiredTransactionLog)
            {
                objLitigationInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddLitigationInformation.aspx.resx");


                returnValue = objLitigationInfo.AddLitigationInformation();

            }
            return returnValue;
        }

        public int UpdateLitigationInformation(ClsLitigationInfo objLitigationInfo)
        {
            int returnValue = 0;

            if (objLitigationInfo.RequiredTransactionLog)
            {
                objLitigationInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddLitigationInformation.aspx.resx");

                returnValue = objLitigationInfo.UpdateLitigationInformation();

            }
            return returnValue;
        }

        public DataTable FetchData(ref ClsLitigationInfo objLitigationInfo)
        {

            DataSet ds = null;

            try
            {
                ds = objLitigationInfo.FetchData();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objLitigationInfo);
                    return ds.Tables[0];
                }//if (dsCount.Tables[0].Rows.Count != 0)
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }



        }



        public DataTable GetClaimExpertServiceProvider()
        {
            ClsLitigationInfo objLitigationInfo = new ClsLitigationInfo();

            DataSet ds = objLitigationInfo.GetClaimExpertServiceProvider();

            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }




        }


        public int ActivateDeactivate(ClsLitigationInfo objLitigationInfo)
        {
            int returnValue = 0;

            if (objLitigationInfo.RequiredTransactionLog)
            {
                objLitigationInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Claims/Aspx/AddLitigationInformation.aspx.resx");

                returnValue = objLitigationInfo.ActivateDeactivate();

            }
            return returnValue;
        }

    }
}
