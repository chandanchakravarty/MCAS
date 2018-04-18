

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;
using Cms.CmsWeb;
using System.Collections;
using System.Drawing;
using System.Xml;
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlProcess;
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlBoleto;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlApplication;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Configuration;
using System.IO;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Reports.Aspx
{
    public partial class ManagementReports : Cms.Account.AccountBase
    {

       ResourceManager objResourceMgr;
        string strUserName = ConfigurationManager.AppSettings.Get("IUserName");
        string strPassWd = ConfigurationManager.AppSettings.Get("IPassWd");
        string strDomain = ConfigurationManager.AppSettings.Get("IDomain");
      
        String OUTPUT_FORMAT = String.Empty;
        String REPORT_NAME = String.Empty;

        string date = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                base.ScreenId = "550";
                btnResetReport.Attributes.Add("onclick", "javascript:return ResetForm();");

                hlkInitialDate.Attributes.Add("OnClick", "fPopCalendar(document.forms[0].txtinitialDate,document.forms[0].txtinitialDate)");
                hlkEndDate.Attributes.Add("OnClick", "fPopCalendar(document.forms[0].txtenddate,document.forms[0].txtenddate)");
                objResourceMgr = new System.Resources.ResourceManager("Cms.Reports.Aspx.ManagementReports", System.Reflection.Assembly.GetExecutingAssembly());

                if (!IsPostBack)
                {
                    this.SetCaptions();
                    BinddManagementReportList();
                    BinddDivListist();
                    BindSUSEPLOBLIist();
                    LoadReportGroupByList();
                    SetErrorMessages();
                }
                btnResetReport.CmsButtonClass = CmsButtonType.Write;
                btnResetReport.PermissionString = gstrSecurityXML;
                btnRunReport.CmsButtonClass = CmsButtonType.Write;
                btnRunReport.PermissionString = gstrSecurityXML;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            

        }

        private void SetErrorMessages()
        {
            try
            {
                revenddate.ValidationExpression = aRegExpDate;
                revinitialDate.ValidationExpression = aRegExpDate;
                rfvddlReportName.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
                rfvGroupby.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
                rfvinitialdate.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
                rfvinenddate.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
                revinitialDate.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
                revenddate.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
                cpvEND_Date.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }

        }
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {          
            this.btnResetReport.Click += new System.EventHandler(this.btnReset_Click);
            this.btnRunReport.Click += new System.EventHandler(this.btnRunReport_Click);            

        }
         
        private void btnReset_Click(object sender, System.EventArgs e)
        {

        }


        private void btnRunReport_Click(object sender, System.EventArgs e)
        {

            GenerateManagementReport();
            /*
            string ReportUrlString;

            string ReportServerURL;

            ReportServerURL = "http://192.168.91.34/EbixAdvReportServer?";//ConfigurationManager.AppSettings["ReportServerURL"].ToString();

            //GenerateFile();

            ReportUrlString = ReportServerURL + "/ManagementReports/APURACAO DE RESULTADO OPERACIONAL GLOBAL DA COMPANHIA POR ORGAO VALORES EXPRESSOS EM REAIS";

            //this.Response.Redirect(ReportUrlString);

            string Script = @"<script>widow.open('" + ReportUrlString + "')</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "Report", Script);
            */
            
            //this.OpenReport("APURACAO DE RESULTADO OPERACIONAL GLOBAL DA COMPANHIA POR ORGAO VALORES EXPRESSOS EM REAIS");
            //ClsGeneralInformation  = new ClsGeneralInformation();
           
            
            if (ddlReportName.SelectedValue.ToString() != "" && ddlGroupby.SelectedValue.ToString() != "")
                this.OpenReport(ClsGeneralInformation.GetManagement_ReportName(int.Parse(ddlReportName.SelectedValue.ToString()), int.Parse(ddlGroupby.SelectedValue.ToString())));
        }

        private void SetCaptions()
        {
            try
            {
                lblheader_field.Text = objResourceMgr.GetString("lblheader_field");
                btnResetReport.Text = objResourceMgr.GetString("btnResetReport");
                btnRunReport.Text = objResourceMgr.GetString("btnRunReport");
                lblreportname.Text = objResourceMgr.GetString("lblreportname");
                lblGroupBy.Text = objResourceMgr.GetString("lblGroupBy");

                lblinitialdate.Text = objResourceMgr.GetString("lblinitialdate");
                lblenddate.Text = objResourceMgr.GetString("lblenddate");
                lbldivision.Text = objResourceMgr.GetString("lbldivision");
                lblsusepAccLOB.Text = objResourceMgr.GetString("lblsusepAccLOB");
                lblPolicyNumber.Text = objResourceMgr.GetString("lblPolicyNumber");
                llbClaimNUmber.Text = objResourceMgr.GetString("llbClaimNUmber");
                lblbrokercode.Text = objResourceMgr.GetString("lblbrokercode");
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
           

        }

        public void GenerateManagementReport()
        {

            GenerateReport();
            /*Commented by Lalit August 30,2011
            try
            {

                DataSet dsFethStoredPRoc = new DataSet();
                dsFethStoredPRoc = DSGetProcedureName();
                if (Convert.ToString(ViewState["PROC_NAME"]) == "")
                {
                    LblErrorMsg.Visible = true;
                    LblErrorMsg.Text = objResourceMgr.GetString("LblErrorMsg");
                    LblErrorMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    if (dsFethStoredPRoc == null || dsFethStoredPRoc.Tables[0].Rows.Count == 0)
                    {
                        LblErrorMsg.Visible = true;
                        LblErrorMsg.Text = objResourceMgr.GetString("LblErrorMsg");
                        LblErrorMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }

                StringBuilder strBuild = new StringBuilder();
                string dd = DateTime.Now.Day.ToString();
                string mon = DateTime.Now.Month.ToString();
                string yy = DateTime.Now.Year.ToString();
                dd = dd.Length < 2 ? dd.PadLeft(2, '0') : dd.ToString();
                mon = mon.Length < 2 ? mon.PadLeft(2, '0') : mon.ToString();
                string date_cur = dd + mon + yy;
                if (dsFethStoredPRoc != null && OUTPUT_FORMAT == "Excel")
                {
                    string path = REPORT_NAME + "_" + date_cur + ".xls";
                    ExportDataSetToExcel(dsFethStoredPRoc, path);

                }

            }

            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }*/
        }






        private int GenerateReport()
        {
            string ReportName=string.Empty;
            string GroupBy = string.Empty;
            //string InitialDate = string.Empty;
            //string EndDate = string.Empty;


            DateTime EndDate;
            DateTime InitialDate;


            if (txtinitialDate.Text.ToString().Trim() != "")
                InitialDate = ConvertToDate(txtinitialDate.Text);
            else
                InitialDate = ConvertToDate(null);

            if (txtenddate.Text.ToString().Trim() != "")
                EndDate = ConvertToDate(txtenddate.Text);
            else
                EndDate = ConvertToDate(null);

            string Division = string.Empty; 
            string SusepLOB = string.Empty;
            string Policy_NO = string.Empty;
            int Broker_Code = 0;
            string ClaimNo = string.Empty;
            int ORDER_BY = 0;
          int _Return = 0;
            ReportName = ddlReportName.SelectedItem.Text;
            GroupBy = ddlGroupby.SelectedItem.Text;
          
           string ddldiv = ddlDivision.SelectedItem.ToString();
           
           if (!string.IsNullOrWhiteSpace(ddlDivision.SelectedItem.Text.ToString()) &&  (!string.IsNullOrEmpty(ddlDivision.SelectedItem.Text.ToString())))
           //if (ddlDivision.SelectedItem.Text.ToString()!= string.Empty && ddlDivision.SelectedItem.Text.ToString()!= " ")
           {
               int Division_ID = Convert.ToInt32(ddlDivision.SelectedValue);

               DataSet DSDIV = (DataSet)ViewState["DSDIV"];
               DataRow[] dr = DSDIV.Tables[0].Select("DIV_ID='" + Division_ID + "'");
               Division = dr[0]["DIV_CODE"].ToString();
           }
          

           
           if (ddlsusepAccLOB.SelectedItem.Text.ToString() != " "  && ddlsusepAccLOB.SelectedItem.Text.ToString() != string.Empty)
            {
                SusepLOB = ddlsusepAccLOB.SelectedItem.Text;
            }
            if (txtpolicynumber.Text != "" && txtpolicynumber.Text != null)
            {
                Policy_NO = txtpolicynumber.Text;
            }
            if (txtbrokerCode.Text != "" && txtbrokerCode.Text != null)
            {
                Broker_Code = Convert.ToInt32(txtbrokerCode.Text);
            }
            if (txtClaimNo.Text != "" && txtClaimNo.Text != null)
            {
                ClaimNo = txtClaimNo.Text;
            }
            if (ddlGroupby.SelectedValue.ToString() != "" && ddlGroupby.SelectedValue.ToString() != null)
            {
                ORDER_BY = int.Parse(ddlGroupby.SelectedValue.ToString());
            }
            ClsGeneralInformation objGenInfo = new ClsGeneralInformation();

            objGenInfo.GenrateManagementReport( InitialDate,EndDate, Division, SusepLOB, Policy_NO,Broker_Code, ClaimNo,ORDER_BY);
            

            /* 
             if (ddlReportName.SelectedValue.ToString() != string.Empty)
             {
                  ReportId = int.Parse(ddlReportName.SelectedValue);
                 if (((DataSet)ViewState["DSManagementReport"]) != null)
                 {

                     dsReportResult = (DataSet)ViewState["DSManagementReport"];
                     DataRow[] dr = dsReportResult.Tables[0].Select("REPORT_ID='" + ReportId + "'");
                     Procedure_Name = dr[0]["PROC_NAME"].ToString();
                     OUTPUT_FORMAT = dr[0]["OUTPUT_FORMAT"].ToString();
                     REPORT_NAME = dr[0]["REPORT_NAME"].ToString();
                     ViewState["PROC_NAME"] = Procedure_Name;
                     ViewState["OUTPUT_FORMAT"] = OUTPUT_FORMAT;
                     ViewState["REPORT_NAME"] = REPORT_NAME;

                 }

                 try
                 {
                    
                  
                    
                     /* switch (Procedure_Name)
                     {
                           
                         case "SP_POLICY":

                             dsReportResult = objGenInfo.GetSp_Policy(InitialDate, EndDate, Division, SusepLOB, Policy_NO, Broker_Code, ORDER_BY,Procedure_Name);
                             break;

                         case "SP_INSTALLMENTS":

                             dsReportResult = objGenInfo.GetSp_Installment(InitialDate, EndDate, Division, SusepLOB, Policy_NO, ORDER_BY, Procedure_Name);
                             break;

                         case "SP_BROKER_INSTALLMENTS":
                             dsReportResult = objGenInfo.GetSp_BROKER_INSTALLMENTS(InitialDate, EndDate, Division, SusepLOB, Policy_NO, ORDER_BY, Procedure_Name);
                             break;

                         case "SP_CLAIM":
                             dsReportResult = objGenInfo.GetSp_CLAIM(InitialDate, EndDate, Division, SusepLOB, Policy_NO, ClaimNo, ORDER_BY, Procedure_Name);
                             break;
                       
                         case "SP_CLAIM_DETAILS":
                             dsReportResult = objGenInfo.GetSp_CLAIM_DETAILS(InitialDate, EndDate, ClaimNo,  Procedure_Name);
                             break;
                         default:
                             break;
                     }
                       

                 }


                 catch (Exception ex)
                 {
                     Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                 } 
             }*/
            //return dsReportResult;

            return _Return;

        }

        private void ExportDataSetToExcel(DataSet ds, string filename)
        {
            try
            {
                HttpResponse response = HttpContext.Current.Response;

                // first let's clean up the response.object   
                response.Clear();

                response.ClearContent();
                response.ClearHeaders();
                response.Charset = "";
                // set the response mime type for excel   
                response.ContentType = "application/vnd.ms-excel";
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                string strProcName = "";

                if (Convert.ToString(ViewState["PROC_NAME"]) != "")
                {
                    strProcName = ViewState["PROC_NAME"].ToString();
                }
                else
                {
                    return;

                }

                // create a string writer   
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                        {
                            //switch (strProcName)
                            //{
                            //    case "SP_POLICY":
                            //        gv_Policy.DataSource = ds.Tables[0];
                            //        gv_Policy.DataBind();
                            //        gv_Policy.RenderControl(htw);
                            //        break;

                            //    case "SP_INSTALLMENTS":
                            //        gv_Intallment.DataSource = ds.Tables[0];
                            //        gv_Intallment.DataBind();
                            //        gv_Intallment.RenderControl(htw);
                            //        break;

                            //    case "SP_BROKER_INSTALLMENTS":
                            //        gv_Broker_Intallment.DataSource = ds.Tables[0];
                            //        gv_Broker_Intallment.DataBind();
                            //        gv_Broker_Intallment.RenderControl(htw);
                            //        break;

                            //    case "SP_CLAIM":
                            //        gv_Claim.DataSource = ds.Tables[0];
                            //        gv_Claim.DataBind();
                            //        gv_Claim.RenderControl(htw);
                            //        break;

                            //    case "SP_CLAIM_DETAILS":
                            //        gv_Claim_Detail.DataSource = ds.Tables[0];
                            //        gv_Claim_Detail.DataBind();
                            //        gv_Claim_Detail.RenderControl(htw);
                            //        break;


                            //}
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
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            

        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }


        private void BinddManagementReportList()
        {
            try
            {
                int REPORT_TYPE = 14955;
                DataSet dsReport = new DataSet();
                ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
                dsReport = objGenInfo.GetManagementReportList(REPORT_TYPE);
                ddlReportName.DataSource = dsReport;
                ddlReportName.DataTextField = "REPORT_NAME";
                ddlReportName.DataValueField = "REPORT_ID";
                ddlReportName.DataBind();
                this.ddlReportName.Items.Insert(0, " ");

                if (dsReport != null)
                {
                    ViewState["DSManagementReport"] = dsReport;
                }
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }


        private void BinddDivListist()
        {

            DataSet dsDiv = new DataSet();
            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                dsDiv = objDataWrapper.ExecuteDataSet("Proc_DivisionsList");
                ddlDivision.DataSource = dsDiv.Tables[0];
                ddlDivision.DataTextField = "DIV_NAME";
                ddlDivision.DataValueField = "DIV_ID";
                ddlDivision.DataBind();
                this.ddlDivision.Items.Insert(0, " ");
                if (dsDiv != null)
                {
                    ViewState["DSDIV"] = dsDiv;
                }
                
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }



        private void BindSUSEPLOBLIist()
        {

            DataSet DsLob = new DataSet();
            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                DsLob = objDataWrapper.ExecuteDataSet("Proc_FetchLobList");
                ddlsusepAccLOB.DataSource = DsLob.Tables[0];
                ddlsusepAccLOB.DataTextField = "SUSEP_LOB_CODE";
                ddlsusepAccLOB.DataValueField = "LOB_ID";
                ddlsusepAccLOB.DataBind();
                this.ddlsusepAccLOB.Items.Insert(0, " ");


            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }


        private void LoadReportGroupByList()
        {
            ddlGroupby.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MGTRPT");
            ddlGroupby.DataTextField = "LookupDesc";
            ddlGroupby.DataValueField = "LookupID";
            ddlGroupby.DataBind();
           
        }
        #region commented region     
        //protected void ddlReportName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        ddlGroupby.Enabled = true;
        //        ddlGroupby.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MGTRPT");
        //        ddlGroupby.DataTextField = "LookupDesc";
        //        ddlGroupby.DataValueField = "LookupID";
        //        ddlGroupby.DataBind();

        //        if (ddlReportName.SelectedValue.ToString() == "24" || ddlReportName.SelectedValue.ToString() == "26")
        //        {

        //            ddlGroupby.Enabled = true;
        //            ListItem Li = new ListItem();
        //            Li = ddlGroupby.Items.FindByValue("14946");
        //            ddlGroupby.Items.Remove(Li);
        //            ListItem Li1 = new ListItem();
        //            Li1 = ddlGroupby.Items.FindByValue("14949");
        //            ddlGroupby.Items.Remove(Li1);
        //            ListItem li3 = new ListItem();
        //            li3 = ddlGroupby.Items.FindByValue("14951");
        //            ddlGroupby.Items.Remove(li3);
        //            ListItem li4 = new ListItem();
        //            li4 = ddlGroupby.Items.FindByValue("14952");
        //            ddlGroupby.Items.Remove(li4);
        //            ListItem li5 = new ListItem();
        //            li5 = ddlGroupby.Items.FindByValue("14953");
        //            ddlGroupby.Items.Remove(li5);
        //            ddlGroupby.Items.Insert(0, "");
        //        }
                
        //       if (ddlReportName.SelectedValue.ToString() == "27")
        //        {

        //            ddlGroupby.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
        //    }

        //}
        #endregion

        /// <summary>
        /// Load General dynamic GripView for SUSEP Report  
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="htw"></param>
        //Added by Pradeep Kushwaha on on 24- Aug- 2011 - workitem - 529
        private void LoadGeneralDynamicGrid(DataTable dt, HtmlTextWriter htw)
        {
            //create columns dynamically 
            this.CreateDataColumns(dt);
            //Initialize the DataSource

            Gv_General.DataSource = dt;
            //Bind the datatable with the GridView.
            Gv_General.DataBind();
            Gv_General.RenderControl(htw);
        }

        /// <summary>
        /// This function use to bind the columns dynamically and set the header 
        /// </summary>
        /// <param name="dt"></param>
        //Added by Pradeep Kushwaha on 24- Aug- 2011 - workitem - 529
        private void CreateDataColumns(DataTable dt)
        {
            //Iterate through the columns of the datatable to set the data bound field dynamically.
            foreach (DataColumn col in dt.Columns)
            {
                //for(int countcolumn=0; countcolumn<dt.Columns.Count;countcolumn++)
                //{
                //    DataColumn col = dt.Columns[0];
                //Declare the bound field and allocate memory for the bound field.
                BoundField bfield = new BoundField();

                //Initalize the DataField value.
                bfield.DataField = col.ColumnName;
                //bfield.HtmlEncodeFormatString
                //Initialize the HeaderText field value.
                bfield.HeaderText = col.ColumnName;
                //Add the newly created bound field to the GridView.
                //Gv_General.Columns[countcolumn].ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                Gv_General.Columns.Add(bfield);
                //}
            }

        }
        /// <summary>
        /// Opent Uploaded RDL Reports on Report Server
        /// </summary>
        /// <param name="ReportName"></param>
        private void OpenReport(string ReportName)
        {
            //string _ReportUrlString;
            //string _ReportServerURL;
            //string _ReportPath = "";

            ////get report server and path parameter from config
            //_ReportServerURL = System.Configuration.ConfigurationManager.AppSettings.Get("ReportServer").ToString();      //ConfigurationManager.AppSettings[""].ToString();
            //_ReportPath = System.Configuration.ConfigurationManager.AppSettings.Get("ReportPath").ToString(); 
            
            if (ReportName != "")//if report name is not blank
            {
               /* //complete report url Path
                _ReportUrlString = _ReportServerURL + _ReportPath+ ReportName;

                //script to open report by Url path in new window
                string Script = @"<script>window.open('" + _ReportUrlString + "',null,'resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50 ')</script>";
                
                ClientScript.RegisterStartupScript(this.GetType(), "Report", Script);
                */
                string url = "CustomReport.aspx?PageName=" + ReportName + "&";
                string Script = @"<script>window.open('" + url + "','ManagementReports','resizable=yes,scrollbar=yes,top=100,left=50');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "Report", Script);

            }
        }
       
       

    }
}