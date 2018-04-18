using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.Model.Account;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlCommon;
using System.Data;
using System.Globalization;


namespace Cms.Account.Aspx
{
    public partial class InitialLoadApplicationDetails : Cms.Account.AccountBase
    {
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidIMPORT_REQUEST_ID;

        ClsAcceptedCOILoadDetails objClsAcceptedCOILoadDetails = new ClsAcceptedCOILoadDetails();
        System.Resources.ResourceManager objResourceMgr;
        string ErrorMode = "";

        #region PageLoad event
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "551_4";


            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.InitialLoadApplicationDetails", System.Reflection.Assembly.GetExecutingAssembly());

            if (!Page.IsPostBack)
            {
                setcaption();
                GetQueryStringValues();

                LoadGridData();

            }
        }
        #endregion

        private void GetQueryStringValues()
        {

            if (Request.QueryString["IMPORT_REQUEST_ID"] != null && Request.QueryString["IMPORT_REQUEST_ID"].ToString() != "")
                hidIMPORT_REQUEST_ID.Value = Request.QueryString["IMPORT_REQUEST_ID"].ToString();
            if (Request.QueryString["IMPORT_SERIAL_NO"] != null && Request.QueryString["IMPORT_SERIAL_NO"].ToString() != "")
                hidIMPORT_SERIAL_NO.Value = Request.QueryString["IMPORT_SERIAL_NO"].ToString();
            if (Request.QueryString["pageMode"] != null && Request.QueryString["pageMode"].ToString() != "")
                hidMODE.Value = Request.QueryString["pageMode"].ToString();

        }
        private void LoadGridData()
        {

            string Mode = "APP";
            switch (hidMODE.Value)
            {
                case "14936_E": Mode = "CUST"; break; // FOR CUSTOMER
                case "14937_E": Mode = "CAPP"; break; // FOR COAPPLICANT
                case "14938_E": Mode = "CONT"; break; // FOR CONTACT
                case "14939_E": Mode = "POLC"; break;
                case "14960_E": Mode = "LOCA"; break; // FOR Location
                case "14942_E": Mode = "REIS"; break; // FOR Reinsurance
                case "14941_E": Mode = "COIS"; break; // FOR Coinsurance
                case "14940_E": Mode = "REMU"; break; // FOR Remuneration
                case "14962_E": Mode = "CLSS"; break; // FOR Clauses
                case "14963_E": Mode = "PDSC"; break; // FOR Policy Discounts
                case "14961_E": Mode = "PAPP"; break; // FOR Co-Applicant
                case "14969_E": Mode = "BILL"; break; // FOR BILLING INFO
                case "15008_E": Mode = "PRSK"; break; // FOR RISK INFO
                case "14966_E": Mode = "RDSC"; break; // FOR RISK DISCOUNT
                case "14967_E": Mode = "PBEN"; break; // FOR BENEFICIARY
                case "14943_E": Mode = "CLMN"; break; // FOR CLAIM NOTIFICATION
                case "14944_E": Mode = "CPRT"; break; // FOR PARTIES
                case "14997_E": Mode = "CACT"; break; // FOR PARTIES
                case "14998_E": Mode = "PMNT"; break; // FOR CLAIM PAYMENT
                case "14999_E": Mode = "VITM"; break; // FOR VICTIM
                case "15000_E": Mode = "TPRT"; break; // FOR THIRD PARTY DAMAGE
                case "15001_E": Mode = "CRSK"; break; // FOR RISK DETAIL
                case "15002_E": Mode = "CPYE"; break; // FOR PAYEE
                case "15003_E": Mode = "OCCD"; break; // FOR OCCURANCE DETAILS
                case "15004_E": Mode = "LTGN"; break; // FOR LITIGATION
                case "15005_E": Mode = "CCOV"; break; // FOR CLAIM COVERAGES
                case "15006_E": Mode = "CCOI"; break; // FOR COINSURANCE DETAILS 
                case "15007_E": Mode = "RESV"; break; // FOR CLAIM RESERVE
                case "14968_E": Mode = "PCOV"; break; // FOR POLICY RISK COVERAGES
                default: Mode = "CUST"; break;
            }

            DataSet ds = objClsAcceptedCOILoadDetails.GetInitialLoadImportDetails(int.Parse(hidIMPORT_REQUEST_ID.Value), int.Parse(hidIMPORT_SERIAL_NO.Value), ClsCommon.BL_LANG_ID, Mode);



            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    ErrorMode = ds.Tables[0].Rows[0]["ERROR_MODE"].ToString().Trim();

                    // HERE VE => VALIDATION ERROR, IT MEANS ONLY ONE ERROR NEED TO DISPLAY ON PAGE


                    grdErrorDetails.DataSource = ds.Tables[1];
                    grdErrorDetails.DataBind();

                }
            }

        }



        protected void grdErrorDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.EmptyDataRow)
            {
                if (ErrorMode == "VE")
                {
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                }

            }

        }

        private void setcaption()
        {
            grdErrorDetails.Columns[0].HeaderText = objResourceMgr.GetString("ERROR_DESC");
            grdErrorDetails.Columns[1].HeaderText = objResourceMgr.GetString("ERROR_COLUMN");
            grdErrorDetails.Columns[2].HeaderText = objResourceMgr.GetString("ERROR_COLUMN_VALUE");
        }
    }
}