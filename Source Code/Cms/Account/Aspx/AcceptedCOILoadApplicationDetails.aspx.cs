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
    public partial class AcceptedCOILoadApplicationDetails : Cms.Account.AccountBase
    {
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIMPORT_REQUEST_ID;
        
        ClsAcceptedCOILoadDetails objClsAcceptedCOILoadDetails = new ClsAcceptedCOILoadDetails();
        System.Resources.ResourceManager objResourceMgr;
        string ErrorMode = "";

        #region PageLoad event
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "538_2";


            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.AcceptedCOILoadApplicationDetails", System.Reflection.Assembly.GetExecutingAssembly());

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

            if(hidMODE.Value=="COV_E")
                Mode = "COV";
            if (hidMODE.Value == "IST_E")
                Mode = "ISTC";

            if (hidMODE.Value == "CLM_D_E")
                Mode = "CLM";

            if (hidMODE.Value == "CLM_E")
                Mode = "CLMP";


            DataSet ds = objClsAcceptedCOILoadDetails.GetAcceptedCOLDetail(int.Parse(hidIMPORT_REQUEST_ID.Value), int.Parse(hidIMPORT_SERIAL_NO.Value), ClsCommon.BL_LANG_ID, Mode);

            

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
            if (e.Row.RowType != DataControlRowType.EmptyDataRow )
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