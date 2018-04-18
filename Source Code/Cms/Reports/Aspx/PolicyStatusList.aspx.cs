using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Cms.DataLayer;

namespace Cms.Reports.Aspx
{
    public partial class PolicyStatusList : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.GridView Grdvw;
        protected void Page_Load(object sender, EventArgs e)
        {
            Grdvw = new GridView();

            try
            {
                ExportDataSetToExcel(GetReportData("Proc_FetchPolicyStatus_Report"), "PolicyStatusReport.xls");
            }
            catch (Exception ex) {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Script", "<script>alert('Error while generating Report...');</script>");
            }
        }


        private void ExportDataSetToExcel(DataSet ds, string filename)
        {

            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Charset = "";

            // set the response mime type for excel   
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
            // create a string writer   
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                    {
                        LoadGeneralDynamicGrid(ds.Tables[0], htw);
                    }

                    response.Write("<html><head><meta http-equiv=Content-Type content='text/html; charset=utf-8'>");
                    response.Write("<style>td{mso-number-format:\\@}</style>");
                    response.Write(sw.ToString());
                    response.Write("</body></html>");
                    response.End();
                }

            }

        }
        /// <summary>
        /// Load General dynamic GripView for SUSEP Report  
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="htw"></param>
        private void LoadGeneralDynamicGrid(DataTable dt, HtmlTextWriter htw)
        {
            //create columns dynamically 
            //this.CreateDataColumns(dt);
            //Initialize the DataSource

            Grdvw.DataSource = dt;
            //Bind the datatable with the GridView.
            Grdvw.DataBind();
            Grdvw.RenderControl(htw);
        }
        /// <summary>
        /// This function use to bind the columns dynamically and set the header 
        /// </summary>
        /// <param name="dt"></param>
        private void CreateDataColumns(DataTable dt)
        {
            //Iterate through the columns of the datatable to set the data bound field dynamically.
            foreach (DataColumn col in dt.Columns)
            {
                    BoundField bfield = new BoundField();
                    //Initalize the DataField value.
                    bfield.DataField = col.ColumnName;
                    bfield.HeaderText = col.ColumnName;
                    Grdvw.Columns.Add(bfield);
            }

        }
        public DataSet GetReportData(string Procedure_Name)
        {
            DataSet dsReport = null;
            DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            dsReport = objDataWrapper.ExecuteDataSet(Procedure_Name);
            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();
            return dsReport;
        }

    }
}