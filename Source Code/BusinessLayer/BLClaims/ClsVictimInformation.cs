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
    public class ClsVictimInformation :   Cms.BusinessLayer.BLClaims.ClsClaims
    {

        public ClsVictimInformation()
        {
        }

        public DataSet GetClaimVictimList(int ClaimID)
        {
            ClsVictimInfo objVictimInfo = new ClsVictimInfo();

            return objVictimInfo.GetClaimVictimList(ClaimID);


        }

        public int DeleteClaimVictim(ClsVictimInfo objVictimInfo)
        {
            int returnValue = 0;

            if (objVictimInfo.RequiredTransactionLog)
            {
                objVictimInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddVictims.aspx.resx");


                returnValue = objVictimInfo.DeleteClaimVictim();

            }
            return returnValue;
        }

        public DataTable FetchData(ref ClsVictimInfo objVictimInfo)
        {

            DataSet ds = null;

            try
            {
                ds = objVictimInfo.FetchData();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objVictimInfo);
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

        public int AddClaimVictim(ClsVictimInfo objVictimInfo)
        {
            int returnValue = 0;

            if (objVictimInfo.RequiredTransactionLog)
            {
                objVictimInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddVictims.aspx.resx");


                returnValue = objVictimInfo.AddClaimVictim();

            }
            return returnValue;
        }

        public int UpdateClaimVictim(ClsVictimInfo objVictimInfo)
        {
            int returnValue = 0;

            if (objVictimInfo.RequiredTransactionLog)
            {
                objVictimInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddVictims.aspx.resx");

                returnValue = objVictimInfo.UpdateClaimVictim();

            }
            return returnValue;
        }
    }
}
