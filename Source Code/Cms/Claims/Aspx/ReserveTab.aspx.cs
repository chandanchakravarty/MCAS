using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.Model.Claims;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlCommon;
using System.Data;
using System.Globalization;
using System.Collections;

namespace Cms.Claims.Aspx
{
    public partial class ReserveTab : Cms.Claims.ClaimBase
    {
        protected Cms.CmsWeb.WebControls.ClaimActivityTop cltClaimActivityTop;
        protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
     
        protected string ActivityClientID, ActivityTotalPaymentClientID;
      
        System.Resources.ResourceManager objResourceMgr;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "306_15";
            objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.ReserveTab", System.Reflection.Assembly.GetExecutingAssembly());
            
            GetQueryStringValues();
            ActivityClientID = cltClaimActivityTop.PanelClientID;
            ActivityTotalPaymentClientID = cltClaimActivityTop.PanelPaymentClientID;
            SetClaimActivityTop();
            SetClaimTop();
            SetTabs();

          
        }
        private void SetTabs()
        {
            string ClaimID = hidCLAIM_ID.Value;
            string CustomerID = hidCUSTOMER_ID.Value;
            string PolicyID = hidPOLICY_ID.Value;
            string PolicyVersionID = hidPOLICY_VERSION_ID.Value;
            string CalledFrom =hidCALLED_FROM.Value;
            string ActivityID =hidACTIVITY_ID.Value;
            string TabUrl = string.Empty;
            string TabTitle= string.Empty;
            
           // FOR RESERVE TAB
            TabCtl.TabLength = 200;
            TabUrl = "AddReserveDetails.aspx?ACTIVITY_ID=" + ActivityID + "&CUSTOMER_ID=" + CustomerID + "&POLICY_ID=" + PolicyID + "&POLICY_VERSION_ID=" + PolicyVersionID + "&CLAIM_ID=" + ClaimID + "&CALLED_FROM=" + CalledFrom; 
            TabTitle = "";
            //TabCtl.TabScreenIDs = "306_15";
            TabCtl.TabURLs = TabUrl;
            TabCtl.TabTitles = TabTitle;

            // FOR PAYEE TAB
            //TabCtl.TabLength = 200;
            //TabUrl = "PayeeIndex.aspx?ACTIVITY_ID=" + ActivityID + "&CLAIM_ID=" + ClaimID;
            //TabTitle = objResourceMgr.GetString("PAYEE_TAB");
            ////TabCtl.TabScreenIDs = "306_15";
            //TabCtl.TabURLs = TabCtl.TabURLs + "," + TabUrl;
            //TabCtl.TabTitles = TabCtl.TabTitles + "," + TabTitle;
           
        }
        private void SetClaimActivityTop()
        {
            if (hidCLAIM_ID.Value != "" && hidCLAIM_ID.Value != "0")
            {
                cltClaimActivityTop.ClaimID = int.Parse(hidCLAIM_ID.Value);
            }
            if (hidACTIVITY_ID.Value != "" && hidACTIVITY_ID.Value != "0")
            {
                cltClaimActivityTop.ActivityID = int.Parse(hidACTIVITY_ID.Value);
            }
            //cltClaimActivityTop.ActivityID = 1;
            //cltClaimActivityTop.ShowHeaderBand ="Claim";
            cltClaimActivityTop.Visible = true;
        }
        private void SetClaimTop()
        {

            if (hidCUSTOMER_ID.Value != "" && hidCUSTOMER_ID.Value != "0")
            {
                cltClaimTop.CustomerID = int.Parse(hidCUSTOMER_ID.Value);
            }

            if (hidPOLICY_ID.Value != "" && hidPOLICY_ID.Value != "0")
            {
                cltClaimTop.PolicyID = int.Parse(hidPOLICY_ID.Value);
            }

            if (hidPOLICY_VERSION_ID.Value != "" && hidPOLICY_VERSION_ID.Value != "0")
            {
                cltClaimTop.PolicyVersionID = int.Parse(hidPOLICY_VERSION_ID.Value);
            }
            if (hidCLAIM_ID.Value != "" && hidCLAIM_ID.Value != "0")
            {
                cltClaimTop.ClaimID = int.Parse(hidCLAIM_ID.Value);
            }
            if (hidLOB_ID.Value != "" && hidLOB_ID.Value != "0")
            {
                cltClaimTop.LobID = int.Parse(hidLOB_ID.Value);
            }

            cltClaimTop.ShowHeaderBand = "Claim";

            cltClaimTop.Visible = true;
        }

        private void GetQueryStringValues()
        {

            if (Request.QueryString["CALLED_FROM"] != null && Request.QueryString["CALLED_FROM"].ToString() != "")
                hidCALLED_FROM.Value = Request.QueryString["CALLED_FROM"];


            if (Request.QueryString["CLAIM_ID"] != null && Request.QueryString["CLAIM_ID"].ToString() != "")
               hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();              
            else
                hidCLAIM_ID.Value = GetClaimID();

            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();
            else
                hidCUSTOMER_ID.Value = GetCustomerID();

            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
                hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();
            else
                hidPOLICY_ID.Value = GetPolicyID();

            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
                hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();
            else
                hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();

            //if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
            //    hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();
            //else
            hidLOB_ID.Value = GetLOBID();

            if (Request.QueryString["ACTIVITY_ID"] != null && Request.QueryString["ACTIVITY_ID"].ToString() != "")
                hidACTIVITY_ID.Value = Request.QueryString["ACTIVITY_ID"].ToString();
            else
                hidACTIVITY_ID.Value = "0";

        }
    }
}
