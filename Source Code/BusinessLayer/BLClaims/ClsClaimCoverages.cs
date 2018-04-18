/******************************************************************************************
<Author				: - Santosh KUmar Gautam
<Start Date			: -	26-11-2010
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
    public class ClsClaimCoverages :   Cms.BusinessLayer.BLClaims.ClsClaims
    {

        public ClsClaimCoverages()
        {
        }


        public DataSet GetClaimCoverages(int ClaimID, short LangID,string CareerCode)
        {
            ClsClaimCoveragesInfo objClaimCoveragesInfo = new ClsClaimCoveragesInfo();

            return objClaimCoveragesInfo.GetClaimCoverages(ClaimID, LangID, CareerCode);

           
        }

        public DataTable GetProductCoverages(int LOB_ID, int CLAIM_ID, int LANG_ID, string FetchMode)
        {
            ClsClaimCoveragesInfo objClaimCoveragesInfo = new ClsClaimCoveragesInfo();

            return objClaimCoveragesInfo.GetProductCoverages(LOB_ID, CLAIM_ID, LANG_ID, FetchMode);


        }
        public string GetRIAppliesFlag()
        {
            ClsClaimCoveragesInfo objClaimCoveragesInfo = new ClsClaimCoveragesInfo();

            return objClaimCoveragesInfo.GetRIAppliesFlag();


        }
       

        public int DeleteUserCreateCoverage(ClsClaimCoveragesInfo objClaimCoverageInfo)
        {
            int returnValue = 0;

            if (objClaimCoverageInfo.RequiredTransactionLog)
            {
                objClaimCoverageInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddClaimCoverages.aspx.resx");


                returnValue = objClaimCoverageInfo.DeleteUserCreateCoverage();

            }
            return returnValue;
        }

        public DataTable FetchData(ref ClsClaimCoveragesInfo objClaimCoveragesInfo)
        {

            DataSet ds = null;

            try
            {
                ds = objClaimCoveragesInfo.FetchData();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objClaimCoveragesInfo);
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

        public int AddClaimCoverage(ClsClaimCoveragesInfo objClaimCoverageInfo)
        {
            int returnValue = 0;

            if (objClaimCoverageInfo.RequiredTransactionLog)
            {
                objClaimCoverageInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddClaimCoverages.aspx.resx");


                returnValue = objClaimCoverageInfo.AddClaimCoverage();

            }
            return returnValue;
        }

        public int UpdateClaimCoverage(ClsClaimCoveragesInfo objClaimCoverageInfo)
        {
            int returnValue = 0;

            if (objClaimCoverageInfo.RequiredTransactionLog)
            {
                objClaimCoverageInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddClaimCoverages.aspx.resx");

                returnValue = objClaimCoverageInfo.UpdateClaimCoverage();

            }
            return returnValue;
        }
    }
}
