
/******************************************************************************************
<Author				: - Santosh KUmar Gautam
<Start Date			: -	30 Nov 2010
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

    public class ClsAddReserveDetails :  Cms.BusinessLayer.BLClaims.ClsClaims
    {
        ClsAddReserveDetailsInfo objAddReserveDetailsInfo;
        public ClsAddReserveDetails()
        { }

        public DataSet GetClaimCoveragesReserveDetails(Int32 ClaimID, Int32 ActivityID, Int32 LobID, Int32 LangID, string FetchMode)
        {
           

            try
            {
                objAddReserveDetailsInfo = new ClsAddReserveDetailsInfo();
                return objAddReserveDetailsInfo.GetClaimCoveragesReserveDetails(ClaimID, ActivityID, LobID, LangID, FetchMode);


            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }



        }

        public DataSet GetClaimPaymentAmountWithPersonalInjury(Int32 ClaimID, Int32 ActivityID)
        {


            try
            {
                objAddReserveDetailsInfo = new ClsAddReserveDetailsInfo();
                return objAddReserveDetailsInfo.GetClaimPaymentAmountWithPersonalInjury(ClaimID, ActivityID);


            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }



        }

        // ADDED BY SANTOSH KR GAUTAM ON 08 AUG 2011 FOR ITRACK 1043 AND TFS NO 38
        public DataSet GetTotalClaimPaymentofCoverage(Int32 ClaimID, Int32 CoverageID)
        {


            try
            {
                objAddReserveDetailsInfo = new ClsAddReserveDetailsInfo();
                return objAddReserveDetailsInfo.GetTotalClaimPaymentofCoverage(ClaimID, CoverageID);


            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }



        }

        public string GetActivityID(Int32 ClaimID)
        {
            try
            {
                objAddReserveDetailsInfo = new ClsAddReserveDetailsInfo();
                return objAddReserveDetailsInfo.GetActivityID(ClaimID);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }

          
        }


        public int AddReserveDetails(System.Collections.ArrayList aList, int ActivityID)
        {
            try
            {
                objAddReserveDetailsInfo = aList[0] as ClsAddReserveDetailsInfo;
                objAddReserveDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddReserveDetails.aspx.resx");

                return objAddReserveDetailsInfo.AddReserveDetails(aList, ActivityID);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }

        }

        public int CompleteClaimActivity(Int32 ClaimID, Int32 ActivityID,  Int32 CompletedBy)
        {
            try
            {
                objAddReserveDetailsInfo = new  ClsAddReserveDetailsInfo();
                objAddReserveDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddReserveDetails.aspx.resx");

                return objAddReserveDetailsInfo.CompleteClaimActivity(ClaimID, ActivityID, CompletedBy);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }

        }

        public int CalculateBreakdown(Int32 ClaimID, Int32 ActivityID)
        {
            try
            {
                objAddReserveDetailsInfo = new ClsAddReserveDetailsInfo();
                //objAddReserveDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddReserveDetails.aspx.resx");

                return objAddReserveDetailsInfo.CalculateBreakdown(ClaimID, ActivityID);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }

        }


        public DataSet GetClaimReserveDetails(Int32 ClaimID, Int32 ActivityID, Int32 ReserveID, Int32 LangID)
        {
            try
            {
                objAddReserveDetailsInfo = new ClsAddReserveDetailsInfo();
                return objAddReserveDetailsInfo.GetClaimReserveDetails(ClaimID,ActivityID,ReserveID,LangID);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }


        }
    }
}
