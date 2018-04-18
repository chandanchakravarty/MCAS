/******************************************************************************************
<Author				    : -   Pravesh
<Start Date				: -	  26-Apr-2010
<End Date				: -	
<Description			: -   View Edit Deductible text
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls;
using System.Resources;
using System.Reflection;
using Cms.Model.Policy;

namespace Cms.CmsWeb.aspx
{
    public partial class ViewEditDeductible : Cms.CmsWeb.cmsbase
    {
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidAlert;
        private ResourceManager objResourceManager = null;
        //private DataTable dtTemp = null;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "";

            btnUpdate.CmsButtonClass = CmsButtonType.Write;
            btnUpdate.PermissionString = gstrSecurityXML;
            hidAlert.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1421");
            objResourceManager = new ResourceManager("Cms.CmsWeb.aspx.ViewEditDeductible", Assembly.GetExecutingAssembly());

            lblMessage.Visible = false;

            if (!IsPostBack)
            {
                btnUpdate.Attributes.Add("onclick", "javascript:SaveDeductibleText();");
                SetCaptions();
                SetErrorMessages();

                getQueryString();
                txtCOV_DESC.Text = hidCOV_DESC.Value;
                txtCOV_DESC.ReadOnly = true;
            }
        }
        private void SetErrorMessages()
        {
            //rfvCOV_DESC.ErrorMessage = ClsMessages.GetMessage("224_28", "16");
            rfvDEDUCTIBLE_TEXT.ErrorMessage = ClsMessages.FetchGeneralMessage("1767");
        }
        private void SetCaptions()
        {
            try
            {
                capCOV_DESC.Text = objResourceManager.GetString("txtCOV_DESC");
                capDEDUCTIBLE_TEXT.Text = objResourceManager.GetString("txtDEDUCTIBLE_TEXT");
                lblHeader.Text = objResourceManager.GetString("lblHeader");
                hidPageTitle.Value = objResourceManager.GetString("lblTitle");
                btnUpdate.Text = objResourceManager.GetString("btnUpdate");
                csvDEDUCTIBLE_TEXTS.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1928");
            }
            catch
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("1078");
                lblMessage.Visible = true;
            }
        }
        private void getQueryString()
        {
            // modified by praveer for itrack no 1362 ,problem occur while fetching record through QueryString
            if (Request.QueryString["COV_ID"] != null && Request.QueryString["COV_ID"].ToString().Trim() != "")
            {
                hidCOV_ID.Value = Request.QueryString["COV_ID"].ToString().Trim();
            }
             DataTable dt = ClsCoverageDetails.GetLobForCoverage(int.Parse(hidCOV_ID.Value));
             if (dt.Rows[0]["COV_DES"].ToString().Trim() != "")
             {
                 hidCOV_DESC.Value = dt.Rows[0]["COV_DES"].ToString().Trim();
             }
            //if (Request.QueryString["COV_DESC"] != null && Request.QueryString["COV_DESC"].ToString().Trim() != "")
            //{
            //    hidCOV_DESC.Value = Request.QueryString["COV_DESC"].ToString().Trim();
            //}
            if (Request.QueryString["DEDUCT_CTL_ID"] != null && Request.QueryString["DEDUCT_CTL_ID"].ToString().Trim() != "")
            {
                hidParentDeductId.Value = Request.QueryString["DEDUCT_CTL_ID"].ToString().Trim();
            }
            //if (Request.QueryString["DeductText"] != null && Request.QueryString["DeductText"].ToString().Trim() != "")
            //{
            //    txtDEDUCTIBLE_TEXT.Text = Request.QueryString["DeductText"].ToString().Trim();
            //}
            /*
            if (Request.QueryString["POL_DEDUCT_ID"] != null && Request.QueryString["POL_DEDUCT_ID"].ToString().Trim() != "" && Request.QueryString["POL_DEDUCT_ID"].ToString().Trim() != "0")
            {
                hidPOL_DEDUCT_ID.Value = Request.QueryString["POL_CLAUSE_ID"].ToString().Trim();
            }
            else
            {
                if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString().Trim() != "")
                {
                    hidCustomerID.Value = Request.QueryString["CUSTOMER_ID"].ToString().Trim();
                }

                if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString().Trim() != "")
                {
                    hidPolicyID.Value = Request.QueryString["POLICY_ID"].ToString().Trim();
                }

                if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString().Trim() != "")
                {
                    hidPolicyVersionID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString().Trim();
                }
                if (Request.QueryString["COV_ID"] != null && Request.QueryString["COV_ID"].ToString().Trim() != "")
                {
                    hidCOV_ID.Value = Request.QueryString["COV_ID"].ToString().Trim();
                }
                if (Request.QueryString["RISK_ID"] != null && Request.QueryString["RISK_ID"].ToString().Trim() != "")
                {
                    hidRISK_ID.Value = Request.QueryString["RISK_ID"].ToString().Trim();
                }
                if (Request.QueryString["COV_DESC"] != null && Request.QueryString["COV_DESC"].ToString().Trim() != "")
                {
                    hidCOV_DESC.Value = Request.QueryString["COV_DESC"].ToString().Trim();
                }
                if (Request.QueryString["DEDUCT_CTL_ID"] != null && Request.QueryString["DEDUCT_CTL_ID"].ToString().Trim() != "")
                {
                    hidParentDeductId.Value = Request.QueryString["DEDUCT_CTL_ID"].ToString().Trim();
                }
                if (Request.QueryString["DeductText"] != null && Request.QueryString["DeductText"].ToString().Trim() != "")
                {
                    txtDEDUCTIBLE_TEXT.Text = Request.QueryString["DeductText"].ToString().Trim();
                } 
                
            }*/
        }
    }
}
