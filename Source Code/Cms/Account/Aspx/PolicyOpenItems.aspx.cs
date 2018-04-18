//------------------------------------------------------------------------------
// Created by Pradeep Kushwaha on 11-06-2010
// -
//
//
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Account;
namespace Cms.Account.Aspx
{
    public partial class PolicyOpenItems : Cms.Account.AccountBase
    {
        string url = "";
        System.Resources.ResourceManager objResourceMgr;
        protected string BT;
        protected void Page_Load(object sender, EventArgs e)
        {

            base.ScreenId = "500";
          
            /*********** Setting permissions and class (Read/write/execute/delete)  *************/
            SetPermissions();
            setErrorMessages();
            url = ClsCommon.GetLookupURL();
            BT = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2006");
            imgSelect.Attributes.Add("onclick", "javascript:OpenLookupWithFunction('" + url + "','POLICY_ID','POLICY_NUMBER','hidPolicyID','txtPOLICY_ID','PolicyForFeeRev','"+ BT +"','','');");

            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.PolicyOpenItems", System.Reflection.Assembly.GetExecutingAssembly());

            if (!IsPostBack)
            {
                SetCaptions();
                BindEmptyGridForCustomerOpenItem();
                BindEmptyGridForAgencyOpenItem();
                BindEmptyGridForCOIOpenItem();
            }
             
        }
        private void SetCaptions() 
        {
            capHeaderLabelOfCOI.Text = objResourceMgr.GetString("capHeaderLabelOfCOI");
            capHeaderLabel.Text = objResourceMgr.GetString("capHeaderLabel");
            capHeaderLabelOfCustomer.Text = objResourceMgr.GetString("capHeaderLabelOfCustomer");
            capHeaderLabelOfAgency.Text = objResourceMgr.GetString("capHeaderLabelOfAgency");
            capPOLICY_ID.Text = objResourceMgr.GetString("capPOLICY_ID");
            btnFind.Text = objResourceMgr.GetString("btnFind");
        }
                 
        private void SetPermissions()
        {
            btnFind.CmsButtonClass = CmsButtonType.Read;
            btnFind.PermissionString = gstrSecurityXML;

           

          
        }
        private void setErrorMessages()
        {
            //revPOLICY_ID.ValidationExpression = @"[A-Za-z0-9]";//  /^[a-z0-9]$/i"; //"^.*$"; // "/^[a-z0-9]{3,}$/i" //@"^[a-zA-Z'.\s]{1,40}$";//aRegExpAlphaNum;
            //revPOLICY_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("102");
            hidMess.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1128");
            rfvPOLICY_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");

        }
        private void BindGridOpenItem(string policyNo)
        {
            lblMessage.Visible = false;
            try
            {

                DataSet objDataSet = ClsAccount.GetPolicyOpenItems(policyNo);
              
                
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    GvCustomerOpenItem.DataSource = objDataSet.Tables[0];
                    GvCustomerOpenItem.DataBind();
                   
                }
                else
                {
                    BindEmptyGridForCustomerOpenItem();
                }
                if (objDataSet.Tables[1].Rows.Count > 0)
                {

                    GvAgencyOpenItem.DataSource = objDataSet.Tables[1];
                    GvAgencyOpenItem.DataBind();
                }
                else
                {
                    BindEmptyGridForAgencyOpenItem();
                }
                if (objDataSet.Tables[2].Rows.Count > 0)
                {

                    GvCOIOpenItem.DataSource = objDataSet.Tables[2];
                    GvCOIOpenItem.DataBind();
                }
                else
                {
                    BindEmptyGridForCOIOpenItem();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "8") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                //Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

            }


        }
        private void BindEmptyGridForCustomerOpenItem()
        {
            
            DataTable dt = new DataTable();
            dt.Columns.Add("TRAN_CODE_TYPE", typeof(string));
            dt.Columns.Add("PAYOR_TYPE", typeof(string));
            dt.Columns.Add("ITEM_TRAN_CODE", typeof(string));
            dt.Columns.Add("COMMISSION_TYPE", typeof(string));
            dt.Columns.Add("TRANS_DESC", typeof(string));
            dt.Columns.Add("DUE_DATE", typeof(string));
            dt.Columns.Add("TOTAL_DUE", typeof(string));
            dt.Columns.Add("TOTAL_PAID", typeof(string));
            dt.Columns.Add("CO_APPLICANT", typeof(string));

            
            DataRow dr = dt.NewRow();
            dr["TRAN_CODE_TYPE"] = String.Empty;
            dr["PAYOR_TYPE"] = String.Empty;
            dr["ITEM_TRAN_CODE"] = String.Empty;
            dr["COMMISSION_TYPE"] = String.Empty;
            dr["TRANS_DESC"] = String.Empty;
            dr["DUE_DATE"] = String.Empty;
            dr["TOTAL_DUE"] = String.Empty;
            dr["TOTAL_PAID"] = String.Empty;
            dr["CO_APPLICANT"] = String.Empty;
            dt.Rows.Add(dr);

            GvCustomerOpenItem.DataSource = dt;
            GvCustomerOpenItem.DataBind();
            if (GvCustomerOpenItem.Rows.Count > 0)
            {
                for (int i = 0; i < GvCustomerOpenItem.Rows[0].Cells.Count; i++)
                    GvCustomerOpenItem.Rows[0].Cells[i].Controls.Clear();
            }
        }
        private void BindEmptyGridForAgencyOpenItem()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AGENCY_NAME", typeof(string));
            dt.Columns.Add("COMMISSION_TYPE", typeof(string));
            dt.Columns.Add("TRANS_DESC", typeof(string));
            dt.Columns.Add("POSTING_DATE", typeof(string));
            dt.Columns.Add("TOTAL_DUE", typeof(string));
            dt.Columns.Add("TOTAL_PAID", typeof(string));
            dt.Columns.Add("AGENCY_COMM_AMT", typeof(string));
            dt.Columns.Add("GROSS_AMOUNT", typeof(string));
            dt.Columns.Add("NET_PREMIUM", typeof(string));
            dt.Columns.Add("CO_APPLICANT", typeof(string));
            DataRow dr = dt.NewRow();

            dr["AGENCY_NAME"] = String.Empty;
            dr["COMMISSION_TYPE"] = String.Empty;
            dr["TRANS_DESC"] = String.Empty;
            dr["POSTING_DATE"] = String.Empty;
            dr["TOTAL_DUE"] = String.Empty;
            dr["TOTAL_PAID"] = String.Empty;
            dr["AGENCY_COMM_AMT"] = String.Empty;
            dr["GROSS_AMOUNT"] = String.Empty;
            dr["NET_PREMIUM"] = String.Empty;
            dr["CO_APPLICANT"] = String.Empty;
            dt.Rows.Add(dr);

            GvAgencyOpenItem.DataSource = dt;
            GvAgencyOpenItem.DataBind();
            if (GvAgencyOpenItem.Rows.Count > 0)
            {
                for (int i = 0; i < GvAgencyOpenItem.Rows[0].Cells.Count; i++)
                    GvAgencyOpenItem.Rows[0].Cells[i].Controls.Clear();
            }
        }

        private void BindEmptyGridForCOIOpenItem()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("COMPANY_NAME", typeof(string));
            dt.Columns.Add("COMMISSION_TYPE", typeof(string));
            dt.Columns.Add("TRANS_DESC", typeof(string));
            dt.Columns.Add("POSTING_DATE", typeof(string));
            dt.Columns.Add("TOTAL_DUE", typeof(string));
            dt.Columns.Add("TOTAL_PAID", typeof(string));
            dt.Columns.Add("AGENCY_COMM_AMT", typeof(string));
            dt.Columns.Add("GROSS_AMOUNT", typeof(string));
            dt.Columns.Add("NET_PREMIUM", typeof(string));
            dt.Columns.Add("CO_APPLICANT", typeof(string));
            DataRow dr = dt.NewRow();

            dr["COMPANY_NAME"] = String.Empty;
            dr["COMMISSION_TYPE"] = String.Empty;
            dr["TRANS_DESC"] = String.Empty;
            dr["POSTING_DATE"] = String.Empty;
            dr["TOTAL_DUE"] = String.Empty;
            dr["TOTAL_PAID"] = String.Empty;
            dr["AGENCY_COMM_AMT"] = String.Empty;
            dr["GROSS_AMOUNT"] = String.Empty;
            dr["NET_PREMIUM"] = String.Empty;
            dr["CO_APPLICANT"] = String.Empty;
            dt.Rows.Add(dr);

            GvCOIOpenItem.DataSource = dt;
            GvCOIOpenItem.DataBind();
            if (GvCOIOpenItem.Rows.Count > 0)
            {
                for (int i = 0; i < GvCOIOpenItem.Rows[0].Cells.Count; i++)
                    GvCOIOpenItem.Rows[0].Cells[i].Controls.Clear();
            }
        }
        
        protected void btnFind_Click(object sender, EventArgs e)
        {
          
            BindGridOpenItem(txtPOLICY_ID.Text.ToString());
            
        }

        protected void GvCustomerOpenItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvCustomerOpenItem.PageIndex = e.NewPageIndex;
            BindGridOpenItem(txtPOLICY_ID.Text.ToString());
           
        }

        protected void GvAgencyOpenItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvAgencyOpenItem.PageIndex = e.NewPageIndex;
            BindGridOpenItem(txtPOLICY_ID.Text.ToString());
        }

        protected void GvCOIOpenItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvCOIOpenItem.PageIndex = e.NewPageIndex;
            BindGridOpenItem(txtPOLICY_ID.Text.ToString());
        }
        

        protected void GvCustomerOpenItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            NfiBaseCurrency.NumberDecimalDigits = 2;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label lblCustITEM_TRAN_CODE_TYPE = (Label)(e.Row.FindControl("capCustITEM_TRAN_CODE_TYPE"));
                if (lblCustITEM_TRAN_CODE_TYPE != null)
                    lblCustITEM_TRAN_CODE_TYPE.Text = objResourceMgr.GetString("capCustITEM_TRAN_CODE_TYPE");


                Label lblCustPAYOR_TYPE = (Label)(e.Row.FindControl("capCustPAYOR_TYPE"));
                if (lblCustPAYOR_TYPE != null)
                    lblCustPAYOR_TYPE.Text = objResourceMgr.GetString("capCustPAYOR_TYPE");


                Label lblCustITEM_TRAN_CODE = (Label)(e.Row.FindControl("capCustITEM_TRAN_CODE"));
                if (lblCustITEM_TRAN_CODE != null)
                    lblCustITEM_TRAN_CODE.Text = objResourceMgr.GetString("capCustITEM_TRAN_CODE");


                Label lblCustCOMMISSION_TYPE = (Label)(e.Row.FindControl("capCustCOMMISSION_TYPE"));
                if (lblCustCOMMISSION_TYPE != null)
                    lblCustCOMMISSION_TYPE.Text = objResourceMgr.GetString("capCustCOMMISSION_TYPE");

                Label lblCustTransDesc = (Label)(e.Row.FindControl("capCustTransDesc"));
                if (lblCustTransDesc != null)
                    lblCustTransDesc.Text = objResourceMgr.GetString("capCustTransDesc");

                Label lblCustDueDate = (Label)(e.Row.FindControl("capCustDueDate"));
                if (lblCustDueDate != null)
                    lblCustDueDate.Text = objResourceMgr.GetString("capCustDueDate");

                Label lblCustAmountDue = (Label)(e.Row.FindControl("capCustAmountDue"));
                if (lblCustAmountDue != null)
                    lblCustAmountDue.Text = objResourceMgr.GetString("capCustAmountDue");

                Label lblCustAmountPaid = (Label)(e.Row.FindControl("capCustAmountPaid"));
                if (lblCustAmountPaid != null)
                    lblCustAmountPaid.Text = objResourceMgr.GetString("capCustAmountPaid");

                Label lblCustCO_APP = (Label)(e.Row.FindControl("capCustCoApp"));
                if (lblCustCO_APP != null)
                    lblCustCO_APP.Text = objResourceMgr.GetString("capCustCoApp");
                              
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAMOUNT_DUE = (Label)e.Row.FindControl("lblAMOUNT_DUE");

                if (((Label)e.Row.FindControl("lblAMOUNT_DUE")).Text != "")
                    lblAMOUNT_DUE.Text = Convert.ToDouble(((Label)e.Row.FindControl("lblAMOUNT_DUE")).Text).ToString("N", NfiBaseCurrency);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAMOUNT_PAID = (Label)e.Row.FindControl("lblAMOUNT_PAID");

                if (((Label)e.Row.FindControl("lblAMOUNT_PAID")).Text != "")
                    lblAMOUNT_PAID.Text = Convert.ToDouble(((Label)e.Row.FindControl("lblAMOUNT_PAID")).Text).ToString("N", NfiBaseCurrency);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDUE_DATE = (Label)e.Row.FindControl("lblDUE_DATE");

                if (((Label)e.Row.FindControl("lblDUE_DATE")).Text != "")
                {
                   lblDUE_DATE.Text = Convert.ToDateTime((((Label)e.Row.FindControl("lblDUE_DATE")).Text)).ToShortDateString();
                    
                }
            }
        }

        protected void GvAgencyOpenItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            NfiBaseCurrency.NumberDecimalDigits = 2;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label lblcapAgencyAGENCY_NAME = (Label)(e.Row.FindControl("capAgencyAGENCY_NAME"));
                if (lblcapAgencyAGENCY_NAME != null)
                    lblcapAgencyAGENCY_NAME.Text = objResourceMgr.GetString("capAgencyAGENCY_NAME");

                Label lblCOMMISSION_TYPE = (Label)(e.Row.FindControl("capAgencyCOMMISSION_TYPE"));
                if (lblCOMMISSION_TYPE != null)
                    lblCOMMISSION_TYPE.Text = objResourceMgr.GetString("capAgencyCOMMISSION_TYPE");


                Label lblAgencyTransDesc = (Label)(e.Row.FindControl("capAgencyTransDesc"));
                if (lblAgencyTransDesc != null)
                    lblAgencyTransDesc.Text = objResourceMgr.GetString("capAgencyTransDesc");

                Label lblAgencyPostingDate = (Label)(e.Row.FindControl("capAgencyPostingDate"));
                if (lblAgencyPostingDate != null)
                    lblAgencyPostingDate.Text = objResourceMgr.GetString("capAgencyPostingDate");

                Label lblAgencyAmountDue = (Label)(e.Row.FindControl("capAgencyAmountDue"));
                if (lblAgencyAmountDue != null)
                    lblAgencyAmountDue.Text = objResourceMgr.GetString("capAgencyAmountDue");

                Label lblAgencyAmountPaid = (Label)(e.Row.FindControl("capAgencyAmountPaid"));
                if (lblAgencyAmountPaid != null)
                    lblAgencyAmountPaid.Text = objResourceMgr.GetString("capAgencyAmountPaid");

               
                Label lblAgencyAgencyCommAmt = (Label)(e.Row.FindControl("capAgencyAgencyCommAmt"));
                if (lblAgencyAgencyCommAmt != null)
                    lblAgencyAgencyCommAmt.Text = objResourceMgr.GetString("capAgencyAgencyCommAmt");

                Label lblAgencyGrossAmt = (Label)(e.Row.FindControl("capAgencyGrossAmt"));
                if (lblAgencyGrossAmt != null)
                    lblAgencyGrossAmt.Text = objResourceMgr.GetString("capAgencyGrossAmt");

                Label lblAgencyNePremium = (Label)(e.Row.FindControl("capAgencyNePremium"));
                if (lblAgencyNePremium != null)
                    lblAgencyNePremium.Text = objResourceMgr.GetString("capAgencyNePremium");

                Label lblAgencyCO_APP = (Label)(e.Row.FindControl("capAgencyCoApp"));
                if (lblAgencyCO_APP != null)
                    lblAgencyCO_APP.Text = objResourceMgr.GetString("capAgencyCoApp");

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAMOUNT_DUE = (Label)e.Row.FindControl("lblAMOUNT_DUE");

                if (((Label)e.Row.FindControl("lblAMOUNT_DUE")).Text != "")
                    lblAMOUNT_DUE.Text = Convert.ToDouble(((Label)e.Row.FindControl("lblAMOUNT_DUE")).Text).ToString("N", NfiBaseCurrency);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAMOUNT_PAID = (Label)e.Row.FindControl("lblAMOUNT_PAID");

                if (((Label)e.Row.FindControl("lblAMOUNT_PAID")).Text != "")
                    lblAMOUNT_PAID.Text = Convert.ToDouble(((Label)e.Row.FindControl("lblAMOUNT_PAID")).Text).ToString("N", NfiBaseCurrency);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAGENCY_COMM_AMT = (Label)e.Row.FindControl("lblAGENCY_COMM_AMT");

                if (((Label)e.Row.FindControl("lblAGENCY_COMM_AMT")).Text != "")
                    lblAGENCY_COMM_AMT.Text = Convert.ToDouble(((Label)e.Row.FindControl("lblAGENCY_COMM_AMT")).Text).ToString("N", NfiBaseCurrency);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGROSS_AMOUNT = (Label)e.Row.FindControl("lblGROSS_AMOUNT");

                if (((Label)e.Row.FindControl("lblGROSS_AMOUNT")).Text != "")
                    lblGROSS_AMOUNT.Text = Convert.ToDouble(((Label)e.Row.FindControl("lblGROSS_AMOUNT")).Text).ToString("N", NfiBaseCurrency);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNET_PREMIUM = (Label)e.Row.FindControl("lblNET_PREMIUM");

                if (((Label)e.Row.FindControl("lblNET_PREMIUM")).Text != "")
                    lblNET_PREMIUM.Text = Convert.ToDouble(((Label)e.Row.FindControl("lblNET_PREMIUM")).Text).ToString("N", NfiBaseCurrency);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDUE_DATE = (Label)e.Row.FindControl("lblDUE_DATE");

                if (((Label)e.Row.FindControl("lblDUE_DATE")).Text != "")
                {
                    lblDUE_DATE.Text = Convert.ToDateTime((((Label)e.Row.FindControl("lblDUE_DATE")).Text)).ToShortDateString();

                }
            }
        }

        protected void GvCOIOpenItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            NfiBaseCurrency.NumberDecimalDigits = 2;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label lblcapCO_NAME = (Label)(e.Row.FindControl("capCO_NAME"));
                if (lblcapCO_NAME != null)
                    lblcapCO_NAME.Text = objResourceMgr.GetString("capCO_NAME");

                Label lblCOMMISSION_TYPE = (Label)(e.Row.FindControl("capCOCOMMISSION_TYPE"));
                if (lblCOMMISSION_TYPE != null)
                    lblCOMMISSION_TYPE.Text = objResourceMgr.GetString("capCOCOMMISSION_TYPE");


                Label lblPLAN_DESCRIPTION = (Label)(e.Row.FindControl("capCOTransDesc"));
                if (lblPLAN_DESCRIPTION != null)
                    lblPLAN_DESCRIPTION.Text = objResourceMgr.GetString("capCOTransDesc");

                Label lblDUE_DATE = (Label)(e.Row.FindControl("capCOPostingDate"));
                if (lblDUE_DATE != null)
                    lblDUE_DATE.Text = objResourceMgr.GetString("capCOPostingDate");

                Label lblAMOUNT_DUE = (Label)(e.Row.FindControl("capCOAmountDue"));
                if (lblAMOUNT_DUE != null)
                    lblAMOUNT_DUE.Text = objResourceMgr.GetString("capCOAmountDue");

                Label lblAMOUNT_PAID = (Label)(e.Row.FindControl("capCOAmountPaid"));
                if (lblAMOUNT_PAID != null)
                    lblAMOUNT_PAID.Text = objResourceMgr.GetString("capCOAmountPaid");


                Label lblCO_COMM_AMT = (Label)(e.Row.FindControl("capCOCommAmt"));
                if (lblCO_COMM_AMT != null)
                    lblCO_COMM_AMT.Text = objResourceMgr.GetString("capCOCommAmt");

                Label lblGROSS_AMOUNT = (Label)(e.Row.FindControl("capCOGrossAmt"));
                if (lblGROSS_AMOUNT != null)
                    lblGROSS_AMOUNT.Text = objResourceMgr.GetString("capCOGrossAmt");

                Label lblNET_PREMIUM = (Label)(e.Row.FindControl("capCONePremium"));
                if (lblNET_PREMIUM != null)
                    lblNET_PREMIUM.Text = objResourceMgr.GetString("capCONePremium");

                Label lblCOICO_APP = (Label)(e.Row.FindControl("capCOICoApp"));
                if (lblCOICO_APP != null)
                    lblCOICO_APP.Text = objResourceMgr.GetString("capCOICoApp");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAMOUNT_DUE = (Label)e.Row.FindControl("lblAMOUNT_DUE");

                if (((Label)e.Row.FindControl("lblAMOUNT_DUE")).Text != "")
                    lblAMOUNT_DUE.Text = Convert.ToDouble(((Label)e.Row.FindControl("lblAMOUNT_DUE")).Text).ToString("N", NfiBaseCurrency);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAMOUNT_PAID = (Label)e.Row.FindControl("lblAMOUNT_PAID");

                if (((Label)e.Row.FindControl("lblAMOUNT_PAID")).Text != "")
                    lblAMOUNT_PAID.Text = Convert.ToDouble(((Label)e.Row.FindControl("lblAMOUNT_PAID")).Text).ToString("N", NfiBaseCurrency);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCO_COMM_AMT = (Label)e.Row.FindControl("lblCO_COMM_AMT");

                if (((Label)e.Row.FindControl("lblCO_COMM_AMT")).Text != "")
                    lblCO_COMM_AMT.Text = Convert.ToDouble(((Label)e.Row.FindControl("lblCO_COMM_AMT")).Text).ToString("N", NfiBaseCurrency);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGROSS_AMOUNT = (Label)e.Row.FindControl("lblGROSS_AMOUNT");

                if (((Label)e.Row.FindControl("lblGROSS_AMOUNT")).Text != "")
                    lblGROSS_AMOUNT.Text = Convert.ToDouble(((Label)e.Row.FindControl("lblGROSS_AMOUNT")).Text).ToString("N", NfiBaseCurrency);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNET_PREMIUM = (Label)e.Row.FindControl("lblNET_PREMIUM");

                if (((Label)e.Row.FindControl("lblNET_PREMIUM")).Text != "")
                    lblNET_PREMIUM.Text = Convert.ToDouble(((Label)e.Row.FindControl("lblNET_PREMIUM")).Text).ToString("N", NfiBaseCurrency);
            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDUE_DATE = (Label)e.Row.FindControl("lblDUE_DATE");

                if (((Label)e.Row.FindControl("lblDUE_DATE")).Text != "")
                {
                    lblDUE_DATE.Text = Convert.ToDateTime((((Label)e.Row.FindControl("lblDUE_DATE")).Text)).ToShortDateString();

                }
            }
        }

    }
}
