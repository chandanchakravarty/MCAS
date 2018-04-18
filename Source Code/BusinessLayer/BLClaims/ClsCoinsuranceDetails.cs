using System;
using System.Collections.Generic;
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
    public class ClsCoinsuranceDetails : Cms.BusinessLayer.BLClaims.ClsClaims
    {
        public ClsCoinsuranceDetails()
        {
        }


        public DataSet GetClaimCoinsuranceDetails(ref ClsCoinsuranceInfo objClsCoinsuranceInfo)
        {

            DataSet ds = null;

            try
            {
                ds = objClsCoinsuranceInfo.GetClaimCoinsuranceDetails();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objClsCoinsuranceInfo);
                    return ds;
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

        public int AddClaimCoinsuranceDetails(ClsCoinsuranceInfo objClsCoinsuranceInfo)
        {
            int returnValue = 0;

            if (objClsCoinsuranceInfo.RequiredTransactionLog)
            {
                objClsCoinsuranceInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddCoinsuranceDetails.aspx.resx");


                returnValue = objClsCoinsuranceInfo.AddClaimCoinsuranceDetails();

            }
            return returnValue;
        }

        public int UpdateClaimCoinsuranceDetails(ClsCoinsuranceInfo objClsCoinsuranceInfo)
        {
            int returnValue = 0;

            if (objClsCoinsuranceInfo.RequiredTransactionLog)
            {
                objClsCoinsuranceInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddCoinsuranceDetails.aspx.resx");


                returnValue = objClsCoinsuranceInfo.UpdateClaimCoinsuranceDetails();

            }
            return returnValue;
        }
    }
}
