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
    public partial class RICOIReserveDetails : Cms.Claims.ClaimBase
    {
        ClsAddReserveDetails objAddReserveDetails = new ClsAddReserveDetails();
        System.Resources.ResourceManager objResourceMgr;

        public NumberFormatInfo nfi;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (GetPolicyCurrency() != String.Empty && GetPolicyCurrency() == enumCurrencyId.BR)
                nfi = new CultureInfo(enumCulture.BR, true).NumberFormat;
            else
                nfi = new CultureInfo(enumCulture.US, true).NumberFormat;

            base.ScreenId = "306_0_1";

            objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.RICOIReserveDetails", System.Reflection.Assembly.GetExecutingAssembly());



            if (!Page.IsPostBack)
            {

                SetCaptions();
                GetQueryStringValues();

                GetOldDataObject();

            }

        }


        private void SetCaptions()
        {
            lblTitle.Text = objResourceMgr.GetString("lblTitle");
            grdClaimCoverages.Columns[0].HeaderText = objResourceMgr.GetString("REIN_COMAPANY_NAME");
            grdClaimCoverages.Columns[1].HeaderText = objResourceMgr.GetString("COMP_TYPE");
            grdClaimCoverages.Columns[2].HeaderText = objResourceMgr.GetString("COMP_PERCENTAGE");
            grdClaimCoverages.Columns[3].HeaderText = objResourceMgr.GetString("RESERVE_AMT");
            grdClaimCoverages.Columns[4].HeaderText = objResourceMgr.GetString("TRAN_RESERVE_AMT");
            grdClaimCoverages.Columns[5].HeaderText = objResourceMgr.GetString("PAYMENT_AMT");
  
           
            grdClaimCoverages.EmptyDataText = objResourceMgr.GetString("GridEmpty");
            lblClaimNumber.Text = objResourceMgr.GetString("CLAIM_NUMBER");
       
        }

        private void GetQueryStringValues()
        {

            if (Request.QueryString["CLAIM_ID"] != null && Request.QueryString["CLAIM_ID"].ToString() != "")
                hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();

            if (Request.QueryString["RESERVE_ID"] != null && Request.QueryString["RESERVE_ID"].ToString() != "")
                hidRESERVE_ID.Value = Request.QueryString["RESERVE_ID"].ToString();

            if (Request.QueryString["ACTIVITY_ID"] != null && Request.QueryString["ACTIVITY_ID"].ToString() != "")
                hidACTIVITY_ID.Value = Request.QueryString["ACTIVITY_ID"].ToString();
            else
                hidACTIVITY_ID.Value = "0";

            //if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
            //    hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

            //if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
            //    hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();

            //if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
            //    hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();

            //if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
            //    hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();
            //else
           // hidLOB_ID.Value = GetLOBID();

           

        }


        private void GetOldDataObject()
        {
           
            int LangID = int.Parse(GetLanguageID());

            DataSet ds = objAddReserveDetails.GetClaimReserveDetails(int.Parse(hidCLAIM_ID.Value), int.Parse(hidACTIVITY_ID.Value), int.Parse(hidRESERVE_ID.Value), LangID);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grdClaimCoverages.DataSource = ds.Tables[0];
                grdClaimCoverages.DataBind();
            }
            else
            {
                grdClaimCoverages.DataSource = null;
                grdClaimCoverages.DataBind();
            }
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                lblClaimNumber.Text += ds.Tables[1].Rows[0][0].ToString();
            }

        }

        protected void grdClaimCoverages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
             

                if(e.Row.Cells[3].Text!="nbsp;" && e.Row.Cells[3].Text!="")
                   e.Row.Cells[3].Text = Convert.ToDouble(e.Row.Cells[3].Text).ToString("N", nfi);

                if (e.Row.Cells[4].Text != "nbsp;" && e.Row.Cells[4].Text != "")
                   e.Row.Cells[4].Text = Convert.ToDouble(e.Row.Cells[4].Text).ToString("N", nfi);

                if (e.Row.Cells[5].Text != "nbsp;" && e.Row.Cells[5].Text != "")
                   e.Row.Cells[5].Text = Convert.ToDouble(e.Row.Cells[5].Text).ToString("N", nfi);

                   nfi.NumberDecimalDigits = 2;
                if (e.Row.Cells[2].Text != "nbsp;" && e.Row.Cells[2].Text != "")
                     e.Row.Cells[2].Text = Convert.ToDouble(e.Row.Cells[2].Text).ToString("N", nfi);

            }
           
        }
    }
}
