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
using System.Reflection;
using System.Resources;
using Cms.DataLayer;
using Cms.CmsWeb;
using System.IO;
using System.Text;

namespace Cms.Reports.Aspx
{
    public partial class IssuanceReport : Cms.CmsWeb.cmsbase
    {
        protected System.Web.UI.WebControls.Panel Panel1;
        protected System.Web.UI.WebControls.Label lblRI;
        protected System.Web.UI.WebControls.DropDownList lstHierarchy;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
        protected System.Web.UI.WebControls.TextBox txtRI_COI;
        protected System.Web.UI.HtmlControls.HtmlImage imgAGENCY_NAME;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidHierarchySelected;
        protected System.Web.UI.WebControls.Label lblHierarchy;
        protected Cms.CmsWeb.Controls.CmsButton btnReport;
        public string URL;
        System.Resources.ResourceManager objResourceMgr;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "533";
            objResourceMgr = new System.Resources.ResourceManager("Cms.Reports.Aspx.IssuanceReport", System.Reflection.Assembly.GetExecutingAssembly());
            btnReport.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnReport.PermissionString = gstrSecurityXML;
            URL = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
            setCaptions();
        }
        protected void txtPOLICY_NUMBER_TextChanged(object sender, EventArgs e)
        {

        }
        private void setCaptions()
        {
            capHeader.Text = objResourceMgr.GetString("capHeader");
            lblRI.Text = objResourceMgr.GetString("lblRI");
            btnReport.Text = objResourceMgr.GetString("btnReport");
            hidPolicy.Value = objResourceMgr.GetString("hidPolicy");
        }
        private void ExportDataSetToExcel(DataSet ds, string filename)
        {

            HttpResponse response = HttpContext.Current.Response;

            
            // first let's clean up the response.object   
            response.Clear();
            response.Charset = "";

            // ********* by Raman (Itrack 1376) 
            response.ClearContent();
            response.ClearHeaders();
            response.AddHeader("cache-control", "max-age=1");
            //*************************************


            // set the response mime type for excel   
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");

            // create a string writer   
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    

                    //GridView gv = new GridView();
                    gv.DataSource = ds.Tables[0];                    
                    gv.DataBind();
                    gv.RenderControl(htw);


                    response.Write(sw.ToString());
                    response.End();
                }
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                string path = "Policy_Portfolio.xls";
                Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                if (Convert.ToInt32(hidCUSTOMER_ID.Value.ToString()) > 0 && Convert.ToInt32(hidPOLICY_ID.Value.ToString()) > 0 &&
                    Convert.ToInt32(hidPOLICY_VERSION_ID.Value.ToString()) > 0)
                {
                    objDataWrapper.AddParameter("@CUSTOMER_ID", Convert.ToInt32(hidCUSTOMER_ID.Value.ToString()));
                    objDataWrapper.AddParameter("@POLICY_ID", Convert.ToInt32(hidPOLICY_ID.Value.ToString()));
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", Convert.ToInt32(hidPOLICY_VERSION_ID.Value.ToString()));
                }

                DataSet ds = new DataSet();
                ds = objDataWrapper.ExecuteDataSet("PROC_POLICY_Portfolio");
                ExportDataSetToExcel(ds, path);
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }


            


        }

        protected void gv_DataBound(object sender, EventArgs e)
        {
            
        }

        

    }
}
