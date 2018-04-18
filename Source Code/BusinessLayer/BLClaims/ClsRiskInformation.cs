
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
    public class ClsRiskInformation :   Cms.BusinessLayer.BLClaims.ClsClaims
    {
        public ClsRiskInformation()
        { }

        public DataTable GetRiskTypes( Int32 LobID, Int32 ClaimID, Int32 CustomerID, Int32 PolicyID, Int32 PolicyVersionID)
        {            
            ClsRiskInfo ObjRiskInformation = new ClsRiskInfo();

            DataSet ds = ObjRiskInformation.GetRiskTypes(LobID, ClaimID, CustomerID, PolicyID, PolicyVersionID, ClsCommon.BL_LANG_ID);

            if (ds.Tables.Count>0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }

           


        }

        public DataTable GetRiskTypeDetails(Int32 ClaimID ,Int32 LobID, Int32 RiskID, Int32 CustomerID, Int32 PolicyID, Int32 PolicyVersionID)
        {
            ClsRiskInfo ObjRiskInformation = new ClsRiskInfo();

            DataSet ds = ObjRiskInformation.GetRiskTypeDetails(ClaimID,LobID, RiskID, CustomerID, PolicyID, PolicyVersionID);

            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }




        }

        public int AddRiskInformation(ClsRiskInfo objClsRiskInfo)
        {
            int returnValue = 0;

            if (objClsRiskInfo.RequiredTransactionLog)
            {
                objClsRiskInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddRiskInformation.aspx.resx");


                returnValue = objClsRiskInfo.AddRiskInformation();

            }
            return returnValue;
        }

        public int UpdateRiskInformation(ClsRiskInfo objClsRiskInfo)
        {
            int returnValue = 0;

            if (objClsRiskInfo.RequiredTransactionLog)
            {
                objClsRiskInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddRiskInformation.aspx.resx");

                returnValue = objClsRiskInfo.UpdateRiskInformation();

            }
            return returnValue;
        }

        public DataTable FetchData(ref ClsRiskInfo objClsRiskInfo)
        {
                      
            DataSet ds = null;

            try
            {
                ds = objClsRiskInfo.FetchData();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objClsRiskInfo);
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

    }
 }