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
using System.Reflection;
using System.Resources;
using System.Net;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;




namespace Cms.Account.Aspx
{
    public partial class InitialLoadChecksum : Cms.Account.AccountBase
    {
        DataSet dsTemp = null;
        DataTable dsTempTable = null;
        System.Resources.ResourceManager objResourceMgr;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "551_5";
           
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.InitialLoadChecksum", System.Reflection.Assembly.GetExecutingAssembly());
            btnStartImportProcess.CmsButtonClass = CmsButtonType.Write;
            btnStartImportProcess.PermissionString = gstrSecurityXML;
            btnExportReport.CmsButtonClass = CmsButtonType.Write;
            btnExportReport.PermissionString = gstrSecurityXML;
            

            if (!Page.IsPostBack)
            {
                setcaption();
                BindGrid();

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        
        
        private void setcaption()
        {
            grdInitialLoadchecksum.Columns[0].HeaderText = objResourceMgr.GetString("FILE_TYPE");
            grdInitialLoadchecksum.Columns[1].HeaderText = objResourceMgr.GetString("DISPLAY_FILE_NAME");
            grdInitialLoadchecksum.Columns[2].HeaderText = objResourceMgr.GetString("FAILED_RECORDS");
            grdInitialLoadchecksum.Columns[3].HeaderText = objResourceMgr.GetString("SUCCESS_RECORDS");
            lblHeader.Text = objResourceMgr.GetString("lblHeader");
            btnStartImportProcess.Text = objResourceMgr.GetString("btnStartImportProcess");
            btnExportReport.Text = objResourceMgr.GetString("btnExportReport");
            
        }
        
        private void BindGrid()
        {
            try
            {
               
                ClsAcceptedCOILoadInfo objAcceptedCOILoadInfo = new ClsAcceptedCOILoadInfo();
               
                if (dsTemp != null)
                {
                    dsTemp = null;
                }

                if (dsTempTable != null)
                {
                    dsTempTable = null;
                }

                if (Request.QueryString["IMPORT_REQUEST_ID"] != null && Request.QueryString["IMPORT_REQUEST_ID"].ToString() != "")

                    hidIMPORT_REQUEST_ID.Value = Request.QueryString["IMPORT_REQUEST_ID"].ToString();


                dsTemp = objAcceptedCOILoadInfo.FetchChecksum(int.Parse(hidIMPORT_REQUEST_ID.Value), ClsCommon.BL_LANG_ID);


                if (dsTemp.Tables.Count>0 && dsTemp.Tables[0].Rows.Count > 0)
                    {
                        grdInitialLoadchecksum.DataSource = dsTemp.Tables[0];

                    }
                    else
                    {
                        grdInitialLoadchecksum.DataSource = null;

                    }
                    grdInitialLoadchecksum.DataBind();

                    if (dsTemp.Tables.Count > 1 && dsTemp.Tables[1].Rows.Count > 0)
                    {
                        if (dsTemp.Tables[1].Rows[0]["REQUEST_STATUS"].ToString() == "WTCHS")
                            btnStartImportProcess.Visible = true;
                        else
                            btnStartImportProcess.Visible = false;

                    }

               }
            
            catch
            {
                lblMessage.Visible = true;
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "1");
            }
            finally
            {
                if (dsTemp != null)
                    dsTemp.Dispose();
            }
        }

        private int StartProcess()
        {
            int intRetval = 0;

            try
            {
                ClsAcceptedCOILoadInfo objAcceptedCOILoadInfo = new ClsAcceptedCOILoadInfo();
                ClsAcceptedCOILoadDetails objImportRequest = new ClsAcceptedCOILoadDetails();

                int ImportRequestID = 0;
                if (hidIMPORT_REQUEST_ID.Value != "")
                    ImportRequestID = int.Parse(hidIMPORT_REQUEST_ID.Value);

                //objAcceptedCOILoadInfo.REQUEST_STATUS.CurrentValue = (hidREQUEST_STATUS.Value);
                objAcceptedCOILoadInfo.IMPORT_REQUEST_ID.CurrentValue = ImportRequestID;
                objAcceptedCOILoadInfo.PROCESS_TYPE.CurrentValue = "IMQUEUE";
                intRetval = objImportRequest.StartImportProcess(objAcceptedCOILoadInfo);


                if (intRetval > 0)
                {
                    hidIMPORT_REQUEST_ID.Value = objAcceptedCOILoadInfo.IMPORT_REQUEST_ID.CurrentValue.ToString();
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                    btnStartImportProcess.Visible = false;
                    

                }
                else if (intRetval == -1)
                {
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                  
                }

                else
                {
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                    
                }

                lblMessage.Visible = true;


            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
               

            }

            return intRetval;


        }
            


        protected void btnStartImportProcess_Click(object sender, EventArgs e)
        {
            StartProcess();
        }

        protected void btnExportReport_Click(object sender, EventArgs e)
        {
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment; filename=FileName.xls");

        Response.Charset = "";

        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdInitialLoadchecksum.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();


        }
      
    }
}
