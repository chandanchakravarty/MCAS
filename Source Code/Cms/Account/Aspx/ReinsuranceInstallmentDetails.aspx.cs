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
using Cms.DataLayer;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;


using System.Collections;

namespace Cms.Account.Aspx
{
    public partial class ReinsuranceInstallmentDetails : Cms.Account.AccountBase
    {
        System.Resources.ResourceManager objResourceMgr;
        ClsAccount objAccount = new ClsAccount();
        DataSet ds = new DataSet();
        System.Globalization.CultureInfo oldculture;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "564_0";
            btnReport.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnReport.PermissionString = gstrSecurityXML;
            SetCultureThread(GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.ReinsuranceInstallmentDetails", System.Reflection.Assembly.GetExecutingAssembly());
            if (!Page.IsPostBack)
            {
                SetCaptions();
                SetErrorMessages();
                
            }

        }

        private void SetCaptions()
        {
            capheader_field.Text = objResourceMgr.GetString("capheader_field");
            capPolNumber.Text = objResourceMgr.GetString("capPolNumber");
            btnReport.Text = objResourceMgr.GetString("btnReport");
            rfvPOL_NUMBER.Text = objResourceMgr.GetString("rfvPOL_NUMBER");
        }

        private void SetErrorMessages()
        {
            try
            {
                rfvPOL_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        private void BindReportList()
        {
            oldculture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            string policy_number = "";
            policy_number = txtPolNumber.Text;
            ds = objAccount.GetReinsuranceInstallmentData(policy_number);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {

                lblErr.Visible = true;
                lblErr.Text = objResourceMgr.GetString("errMsg");

            }

            //if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            else
            {
                try
                {

                    ExportDataSetToExcel(ds);

                }
                catch (Exception ex)
                {
                    lblErr.Visible = true;
                    lblErr.Text = ex.Message.ToString();

                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                }
                finally
                {
                    Thread.CurrentThread.CurrentCulture = oldculture;
                }
            }
          

        }

        private void ExportDataSetToExcel(DataSet ds)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Charset = "";

            string file_name = "Reinsurance_Installment_Details.xls";

            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + file_name + "\"");

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                    {
                        LoadGeneralDynamicGrid(ds.Tables[0], htw);
                    }

                    response.Write("");
                    response.Write(sw.ToString());
                    response.Write("</body></html>");
                    response.End();
                }
            }
        }

        private void LoadGeneralDynamicGrid(DataTable dt, HtmlTextWriter htw)
        {

            //create columns dynamically 
            this.CreateDataColumns(dt);
            Gv_General.DataSource = dt;
            Gv_General.DataBind();
            Gv_General.RenderControl(htw);
        }

        private void CreateDataColumns(DataTable dt)
        {
            //Iterate through the columns of the datatable to set the data bound field dynamically.
            foreach (DataColumn col in dt.Columns)
            {
                //Declare the bound field and allocate memory for the bound field.
            
                    BoundField bfield = new BoundField();

                    //Initalize the DataField value.
                    bfield.DataField = col.ColumnName;
                    //bfield.HtmlEncodeFormatString
                    //Initialize the HeaderText field value.
                    bfield.HeaderText = col.ColumnName;
                    
                    Gv_General.Columns.Add(bfield);
                
            }

        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                this.BindReportList();
            }
            catch (Exception ex)
            {

                lblErr.Visible = true;
                lblErr.Text = ex.Message.ToString();

                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

    }
}