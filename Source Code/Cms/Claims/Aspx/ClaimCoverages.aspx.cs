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
namespace Cms.Claims.Aspx
{
    public partial class ClaimCoverages : Cms.Claims.ClaimBase
    {
        System.Resources.ResourceManager objResourceMgr;
        ClsClaimCoverages objClaimCoverages = new ClsClaimCoverages();
        public NumberFormatInfo nfi;
        public string strCLM_RI_APPLIES_FLG = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GetPolicyCurrency() != String.Empty && GetPolicyCurrency() == enumCurrencyId.BR)
                nfi = new CultureInfo(enumCulture.BR, true).NumberFormat;
            else
                nfi = new CultureInfo(enumCulture.US, true).NumberFormat;
            
            base.ScreenId = "306_14_0";
          

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.ClaimCoverages", System.Reflection.Assembly.GetExecutingAssembly());

            if (!Page.IsPostBack)
            {
                GetQueryStringValues();
              
                SetCaptions();
                BindGrid();
            }
        }
        private void GetQueryStringValues()
        {

            if (Request.QueryString["CLAIM_ID"] != null && Request.QueryString["CLAIM_ID"].ToString() != "")
                hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();

            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
                hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();

            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
                hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();

            if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
                hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();

        }
        private void SetCaptions()
        {
        
            lblTitle.Text = objResourceMgr.GetString("lblTitle");
            grdClaimCoverages.Columns[0].HeaderText = objResourceMgr.GetString("COVERAGE_CODE_ID");
            grdClaimCoverages.Columns[1].HeaderText = objResourceMgr.GetString("IS_RISK_COVERAGE");
            grdClaimCoverages.Columns[2].HeaderText = objResourceMgr.GetString("COV_DES");
            grdClaimCoverages.Columns[3].HeaderText = objResourceMgr.GetString("RI_APPLIES");
            grdClaimCoverages.Columns[4].HeaderText = objResourceMgr.GetString("IS_ACTIVE");
            grdClaimCoverages.Columns[5].HeaderText = objResourceMgr.GetString("LIMIT_OVERRIDE");
            grdClaimCoverages.Columns[6].HeaderText = objResourceMgr.GetString("LIMIT_1");
            grdClaimCoverages.Columns[7].HeaderText = objResourceMgr.GetString("DEDUCTIBLE_1");
            grdClaimCoverages.Columns[8].HeaderText = objResourceMgr.GetString("POLICY_LIMIT");
            grdClaimCoverages.EmptyDataText = objResourceMgr.GetString("GridEmpty");
            LblPopupDetails.Text = objResourceMgr.GetString("LblPopupDetails");
        
        }
        private void BindGrid()
        {
            string CareerCode = GetSystemId();
            DataSet ds = objClaimCoverages.GetClaimCoverages(int.Parse(hidCLAIM_ID.Value), short.Parse(GetLanguageID()), CareerCode);
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                strCLM_RI_APPLIES_FLG = ds.Tables[1].Rows[0]["CLM_RI_APPLIES_FLG"].ToString();
            }
            if (ds.Tables.Count > 0)
            {
                grdClaimCoverages.DataSource = ds.Tables[0];
                grdClaimCoverages.DataBind();
            }
           
            
        }
        protected void grdClaimCoverages_RowDataBound(object sender, GridViewRowEventArgs e)
      {
          if (e.Row.RowType != DataControlRowType.EmptyDataRow)
          {
              e.Row.Cells[0].Visible = false;
              e.Row.Cells[1].Visible = false;
              e.Row.Cells[9].CssClass = "hiddenColum";
              if (strCLM_RI_APPLIES_FLG != "1")
                  e.Row.Cells[3].Visible = false;
              
             
          }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
             
                e.Row.Cells[6].Text = Convert.ToDouble(e.Row.Cells[6].Text).ToString("N", nfi);
             //   e.Row.Cells[7].Text = Convert.ToDouble(e.Row.Cells[7].Text).ToString("N", nfi); 
                //e.Row.Cells[8].Text = Convert.ToDouble(e.Row.Cells[8].Text).ToString("N", nfi); 
                Label lblDEDUCTIBLE_1 = e.Row.Cells[7].FindControl("lblDEDUCTIBLE_1") as Label;
                if (lblDEDUCTIBLE_1 != null)
                {
                    lblDEDUCTIBLE_1.Text= Convert.ToDouble(lblDEDUCTIBLE_1.Text).ToString("N", nfi); 
                }

                Image imgDEDUCTIBLE_1 = e.Row.Cells[7].FindControl("imgDEDUCTIBLE_1") as Image;

                if (e.Row.Cells[9].Text != "&nbsp;" && e.Row.Cells[9].Text.Length > 0)
                {
                    if (imgDEDUCTIBLE_1 != null)
                    {
                        imgDEDUCTIBLE_1.Attributes.Add("onclick", "javascript:ShowDetails(this," + e.Row.Cells[9].ClientID + ")");
                    }
                }
                else
                    imgDEDUCTIBLE_1.Visible = false;
                

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
