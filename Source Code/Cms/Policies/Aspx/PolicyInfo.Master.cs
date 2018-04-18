using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

namespace Cms.Policies.Aspx
{
    public partial class PolicyInfo : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            //trvMenuControl.Attributes.Add("onclick", "return OnTreeClick(event)");
            //HttpContext.Current.Session["LOBString"] = "PAPEACC";
            CmsWeb.cmsbase a = new CmsWeb.cmsbase();
            //a.SetLOBString("ARPERIL");
            a.SetTransaction_Type("14559"); hidCUSTOMER_ID.Value = a.GetCustomerID();
            hidPOLICY_ID.Value = a.GetPolicyID();
            hidPOLICY_VERSION_ID.Value = a.GetPolicyVersionID();
            hidLOB_ID.Value = a.GetLOBID();
            hidLob_String.Value = a.GetLOBString();

            hlkCustomerAssistant.NavigateUrl = "/cms/client/Aspx/customermanagerindex.aspx";
            hlkCUSTOMER_NAME.NavigateUrl = "/Cms/Policies/Aspx/WebForm1.aspx?CALLED_FROM=ABCD&Path=../../../cms/client/Aspx/AddCustomer.aspx";

            _GetCustomerDetails();
            if (hidCUSTOMER_ID.Value != "" && hidPOLICY_VERSION_ID.Value != "" && hidPOLICY_ID.Value != "")
                _GetPolicyDetails(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
            //if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "") 
            //{
            //    hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();
            //}
            //if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
            //{
            //    hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();
            //}
            //if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
            //{
            //    hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();
            //}
            //if (Request.QueryString["Lob_String"] != null && Request.QueryString["Lob_String"].ToString() != "")
            //{
            //    hidLob_String.Value = Request.QueryString["Lob_String"].ToString();
            //}
            //if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
            //{
            //    hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();
            //}
            if (Request.QueryString["CALLED_FROM"] != null && Request.QueryString["CALLED_FROM"].ToString() != "")
            {
                hidCALLED_FROM.Value = Request.QueryString["CALLED_FROM"].ToString();
            }
            if (Request.QueryString["Path"] != null && Request.QueryString["Path"].ToString() != "")
            {
                hidOpenPath.Value = Request.QueryString["Path"].ToString();
            }


        }

        private void OpenPage()
        {

        }

        protected void trvMenuControl_TreeNodeCollapsed(object sender, TreeNodeEventArgs e)
        {
            //e.Node.ImageToolTip = 1; 
            e.Node.Selected = true;
        }

        protected void trvMenuControl_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            e.Node.Selected = false;   //

        }
        private void _GetCustomerDetails()
        {
            DataSet ds = null;

            Cms.BusinessLayer.BlClient.ClsCustomer objCustomer = new BusinessLayer.BlClient.ClsCustomer();
            if (hidCUSTOMER_ID.Value != "")
                ds = objCustomer.CustomerDetails(int.Parse(hidCUSTOMER_ID.Value));
            ds.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString();if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblCUSTOMER_NAME.Text = ds.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString();// +" " + ds.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString() + " " + ds.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString();
                //lblCUSTOMER_ADDRESS_1.Text = ds.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();// +" " + ds.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString() + " " + ds.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString();
                //lblCUSTOMER_ADDRESS_2.Text = ds.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
                lblCUSTOMER_TYPE.Text = ds.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString();
                lblCUSTOMER_ADDRESS.Text = ds.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
                lblCityStateZip.Text = ds.Tables[0].Rows[0]["CITY_STATE_ZIP"].ToString();
                lblCUSTOMER_PHONE.Text = ds.Tables[0].Rows[0]["CUSTOMER_HOME_PHONE"].ToString();
                lblCUSTOMER_EMAIL.Text = ds.Tables[0].Rows[0]["CUSTOMER_Email"].ToString();
            }

        }
        private void _GetPolicyDetails(int Customer_id, int Policy_id, int Policy_version_id)
        {
            DataSet DsPolicy = null;
            
            Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenralInformation = new BusinessLayer.BlApplication.ClsGeneralInformation();
            DsPolicy = objGenralInformation.GetClaimPolicyDataSet(Customer_id, Policy_id, Policy_version_id);
            if (DsPolicy != null && DsPolicy.Tables.Count > 0 && DsPolicy.Tables[0].Rows.Count > 0)
            {
                lblPOLICY_NO.Text = DsPolicy.Tables[0].Rows[0]["APP_NUMBER"].ToString();
                lblPOL_VERSION_ID.Text = DsPolicy.Tables[0].Rows[0]["POLICY_DISPLAY_VERSION"].ToString();

                lblEFF_DATE.Text = DsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString();
                lblEXP_DATE.Text = DsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString();

                lblLOB.Text = DsPolicy.Tables[0].Rows[0]["LOB"].ToString();
                lblStatus.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(DsPolicy.Tables[0].Rows[0]["POLICY_STATUS_CODE"].ToString());

                lblAgency.Text = DsPolicy.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString();
                lblPOL_CURRENCY.Text = DsPolicy.Tables[0].Rows[0]["POLICY_CURRENCY"].ToString();
            }
            DsPolicy.Dispose();
        }

    }
}