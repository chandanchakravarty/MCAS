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
using Cms.DataLayer;
using Cms.CmsWeb;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Configuration;
using System.IO;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Threading;
using System.Globalization;
namespace Cms.Reports.Aspx
{
    //{[DllImport("kernel32.dll")] private static extern 

    public partial class Susep_Reports : Cms.CmsWeb.cmsbase
    {
        System.Resources.ResourceManager objResourceMgr;
        string date = "";
        DataSet ds = new DataSet();
        String OUTPUT_FORMAT = String.Empty;
        String REPORT_NAME = String.Empty;
        String PROC_NAME = String.Empty;
        string File_Name = string.Empty;
        System.Globalization.CultureInfo oldculture;
        protected void Page_Load(object sender, EventArgs e)
        {

           

            base.ScreenId = "536";
            hlkExpirationStartDate.Attributes.Add("OnClick", "fPopCalendar(document.forms[0].txtExpirationStartDate,document.forms[0].txtExpirationStartDate)");
            hlkinitialDate.Attributes.Add("OnClick", "fPopCalendar(document.forms[0].txtinitialDate,document.forms[0].txtinitialDate)");
            revExpirationStartDate.ValidationExpression = aRegExpDate;
            revExpirationStartDate.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1380");
            revinitialDate.ValidationExpression = aRegExpDate;
            revinitialDate.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1380");
            btnresetreport.Attributes.Add("OnClick", "javascript:return ResetForm();");
            SetCultureThread(GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Cms.Reports.Aspx.Susep_Reports", System.Reflection.Assembly.GetExecutingAssembly());
            if (!Page.IsPostBack)
            {

                SetCaptions();
                SetErrorMessages();
                this.BindReportList();

            }

            btnresetreport.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnresetreport.PermissionString = gstrSecurityXML;
            btnReport.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnReport.PermissionString = gstrSecurityXML;
            var result = Request.Params["__EVENTARGUMENT"];
            if (result == "Yes_clicked")
            {
                GenerateTxt();

            }
        }

        private void BindReportList()
        {
            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {


                ds = objDataWrapper.ExecuteDataSet("Proc_SUSEP_Report");
                cmbReportList.DataSource = ds.Tables[0];
                cmbReportList.DataTextField = "REPORT_NAME";
                cmbReportList.DataValueField = "REPORT_ID";
                cmbReportList.DataBind();
                this.cmbReportList.Items.Insert(0, " ");

                if (ds != null)
                {
                    ViewState["DsSusepProcNames"] = ds;
                }

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

                rfvcmbReoportlist.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
                rfvinitialdate.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
                rfvExpirationStartDate.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
                cpvEND_Date.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }

        }

        private void SetCaptions()
        {
            lblheader_field.Text = objResourceMgr.GetString("lblheader_field");
            btnReport.Text = objResourceMgr.GetString("btnReport");
            btnresetreport.Text = objResourceMgr.GetString("btnresetreport");
            lblinitialDate.Text = objResourceMgr.GetString("lblIntialDate");
            lblRI.Text = objResourceMgr.GetString("lblRI");
            cpvEND_Date.Text = objResourceMgr.GetString("cpvEND_Date");
            rfvcmbReoportlist.Text = objResourceMgr.GetString("rfvcmbReoportlist");
            lblExpirationStartDate.Text = objResourceMgr.GetString("lblExpirationStartDate");
        }

        private DataSet GetProcedureName()
        {
            DateTime dtExpirationStartDate;
            DateTime dtInitialDate;
            DateTimeFormatInfo DateFormatinfoBR = new CultureInfo(enumCulture.BR, true).DateTimeFormat;
            DateTimeFormatInfo DateFormatinfoUS = new CultureInfo(enumCulture.US, true).DateTimeFormat;



            if (txtinitialDate.Text.ToString().Trim() != "")
            {

                if (oldculture.Name == enumCulture.BR)
                    dtInitialDate = Convert.ToDateTime(Convert.ToDateTime(txtinitialDate.Text, DateFormatinfoBR), DateFormatinfoUS);
                else
                    dtInitialDate = Convert.ToDateTime(txtinitialDate.Text, DateFormatinfoUS);
            }
            else
                dtInitialDate = ConvertToDate(null);

            if (txtExpirationStartDate.ToString().Trim() != "")
            {

                if (oldculture.Name == enumCulture.BR)
                    dtExpirationStartDate = Convert.ToDateTime(Convert.ToDateTime(txtExpirationStartDate.Text, DateFormatinfoBR), DateFormatinfoUS);
                else
                    dtExpirationStartDate = Convert.ToDateTime(txtExpirationStartDate.Text, DateFormatinfoUS);



            }
            else
                dtExpirationStartDate = ConvertToDate(null);

            DataSet dsReportResult = new DataSet();
            int Reposrt_id = 0;

            if (cmbReportList.SelectedValue.ToString() != string.Empty)
            {
                Reposrt_id = int.Parse(cmbReportList.SelectedValue);

                if (((DataSet)ViewState["DsSusepProcNames"]) != null)
                {
                    ds = (DataSet)ViewState["DsSusepProcNames"];
                    DataRow[] dr = ds.Tables[0].Select("REPORT_ID='" + Reposrt_id + "'");
                    PROC_NAME = dr[0]["PROC_NAME"].ToString();
                    OUTPUT_FORMAT = dr[0]["OUTPUT_FORMAT"].ToString();
                    REPORT_NAME = dr[0]["REPORT_NAME"].ToString();
                    File_Name = dr[0]["FILE_NAME"].ToString();
                    ViewState["PROC_NAME"] = PROC_NAME;


                }

                try
                {

                    ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
                    switch (PROC_NAME)
                    {

                        case "PROC_SUSEP_R_RURAL":
                        //itrack 1164
                        case "PROC_PPNG_REPORT":
                        case "PROC_RVNE_REPORT":
                        case "PROC_IBNR_REPORT":
                        case "PROC_REL_SIN_RET_EPG":
                        case "PROC_SUSEP_GROUP_PREMIUM_STATISTICS"://1061
                        case "PROC_SUSEP_STATISTICS_RISK_INSURANCE"://1064
                        case "PROC_SUSEP_FORM_14A_REGIONAL_DISTRIBUTION"://1053
                            dsReportResult = objGenInfo.GetSp_Susep_FromDate_ToDate(dtInitialDate, dtExpirationStartDate, PROC_NAME);
                            break;
                        default:
                            dsReportResult = objGenInfo.GetSp_Susep_Report(dtInitialDate, PROC_NAME);
                            break;



                    }

                }
                catch (Exception ex)
                {
                    lblErr.Visible = true;
                    lblErr.Text = ex.Message.ToString();

                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                }
            }
            return dsReportResult;

        }


        public void GenerateTxt()
        {


            oldculture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            ds = GetProcedureName();

            if (Convert.ToString(ViewState["PROC_NAME"]) == "")
            {
                lblErr.Visible = true;
                lblErr.Text = objResourceMgr.GetString("errMsg");

                return;
            }
            else
            {
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {

                    lblErr.Visible = true;
                    lblErr.Text = objResourceMgr.GetString("errMsg");

                }
            }

            StringBuilder strBuild = new StringBuilder();
            string strTemp = "";
            string dd = DateTime.Now.Day.ToString();
            string mon = DateTime.Now.Month.ToString();
            string yy = DateTime.Now.Year.ToString();

            string strUserName = ConfigurationManager.AppSettings.Get("IUserName");
            string strPassWd = ConfigurationManager.AppSettings.Get("IPassWd");
            string strDomain = ConfigurationManager.AppSettings.Get("IDomain");
            Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new ClsAttachment();
            dd = dd.Length < 2 ? dd.PadLeft(2, '0') : dd.ToString();
            mon = mon.Length < 2 ? mon.PadLeft(2, '0') : mon.ToString();

            string date_cur = dd + mon + yy;
            try
            {

                switch (OUTPUT_FORMAT)
                {

                    case "Txt":



                        if (ds != null)
                        {

                            try
                            {
                                //int i = ds.Tables[0].Rows.Count;
                                if (ds.Tables.Count> 0   && ds.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow dr in ds.Tables[0].Rows)
                                    {
                                        //Applied The below code, for txt file extraction,  tfs id 792.
                                        strTemp = "";
                                      string  strFullLengthString = "";
                                        foreach (DataColumn dc in ds.Tables[0].Columns)
                                        {
                                            strTemp = dr[dc].ToString();
                                            strFullLengthString = strFullLengthString + strTemp;
                                        }
                                        //if(ds.Tables[0].Rows.
                                        //strBuild.Append(System.Environment.NewLine);
                                        strBuild.AppendLine(strFullLengthString);
                                    }
                                }


                                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                                Byte[] bytes = encoding.GetBytes(strBuild.ToString());
                                string strPath = ConfigurationManager.AppSettings["PdfPath"] + "/" + REPORT_NAME + "_" + date_cur + ".txt";
                                if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
                                {
                                    if (!File.Exists(strPath))
                                    {

                                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(strPath, true, Encoding.ASCII))
                                        {
                                            if (strBuild.Length > 0)
                                            {
                                                file.Write(strBuild);
                                            }
                                        }
                                    }

                                    else
                                    {
                                        File.Delete(strPath);
                                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(strPath, true, Encoding.ASCII))
                                        {
                                            if (strBuild.Length > 0)
                                            {
                                                file.Write(strBuild);
                                            }
                                        }
                                    }




                                    string sGenName = System.IO.Path.GetFileName(strPath);

                                    if (System.IO.File.Exists(strPath))
                                    {

                                        Response.ClearHeaders();
                                        Response.ClearContent();
                                        Response.AddHeader("Content-disposition", "attachment; filename=" + sGenName);
                                        Response.ContentType = "application/octet-stream";
                                        Response.Clear();
                                        Response.WriteFile(strPath);
                                        Response.End();

                                    }

                                }

                            }
                            catch (Exception ex)
                            {
                                lblErr.Visible = true;
                                lblErr.Text = ex.Message.ToString();

                                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                            }
                        }
                        break;

                    case "Excel":

                        if (ds != null && ds.Tables.Count> 0 )
                        {
                            try
                            {
                                string path = File_Name;
                                ExportDataSetToExcel(ds, path);
                            }
                            catch (Exception ex)
                            {
                                lblErr.Visible = true;
                                lblErr.Text = ex.Message.ToString();

                                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

                            }
                        }

                        break;

                    case "DBF":


                        if (ds != null && ds.Tables.Count> 0   )
                        {
                            string UserID = GetUserId();
                            string strPath = ConfigurationManager.AppSettings["PdfPath"];
                            string DBF_File_Path = strPath + "/" + "DBF_REPORTS";

                            if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
                            {
                                if (!Directory.Exists(DBF_File_Path))
                                {

                                    System.IO.Directory.CreateDirectory(DBF_File_Path);
                                }
                                if (!Directory.Exists(DBF_File_Path + "/DBF_Copy/"))
                                {
                                    System.IO.Directory.CreateDirectory(DBF_File_Path + "/DBF_Copy");

                                }
                                if (!Directory.Exists(DBF_File_Path + "/DBF_Copy/" + UserID))
                                {
                                    System.IO.Directory.CreateDirectory(DBF_File_Path + "/DBF_Copy/" + UserID);
                                }
                                //open connection to dbf file. if file does not exist create it first

                                string sqlDbfConnection = "Provider=microsoft.ace.oledb.12.0;Data Source=" + DBF_File_Path + "/DBF_Copy/" + UserID + ";Extended Properties=dBASE III;User ID=Admin;Password=";

                               // Iterate through the columns of the datatable to set the data bound field dynamically.
                                String DataTableColumns = String.Empty;
                                String DataTableColumn = String.Empty;
                                DataTableColumn += " ( ";
                                DataTableColumns += " ( ";
                                ArrayList arColumnsName = new ArrayList();

                                foreach (DataColumn col in ds.Tables[0].Columns)
                                {
                                    if (col.ColumnName.ToString().ToUpper() != "POLICY_NUMBER" && col.ColumnName.ToString().ToUpper() != "CLAIM_NUMBER")
                                    {
                                        DataTableColumns += "[" + col.ColumnName + "] Char(50),";
                                        DataTableColumn += "[" + col.ColumnName + "] , ";
                                        arColumnsName.Add(col.ColumnName);
                                    }
                                }
                                DataTableColumns = DataTableColumns.Substring(0, DataTableColumns.LastIndexOf(','));
                                DataTableColumn = DataTableColumn.Substring(0, DataTableColumn.LastIndexOf(','));
                                DataTableColumns += " )";
                                DataTableColumn += " )";
                                string SubFile = "";
                                if (File_Name.Split('.')[0].ToString().Length > 8)
                                {
                                    if (PROC_NAME == "PROC_SUSEP_PREMRECEC_PREMIUM_RECEIVED_CHECKOUT_INSURER")
                                    {
                                        SubFile = "PREMREC2.dbf";
                                    }
                                    else
                                    {

                                        SubFile = File_Name.Split('.')[0].ToString().Substring(0, 8);

                                        SubFile = SubFile + ".DBF";
                                    }
                                }
                                else
                                {
                                    SubFile = File_Name;

                                }



                                if (File.Exists(DBF_File_Path + "/" + SubFile))
                                {
                                    if (File.Exists(DBF_File_Path + "/" + "DBF_Copy/" + UserID + "/" + SubFile))
                                        File.Delete(DBF_File_Path + "/" + "DBF_Copy/" + UserID + "/" + SubFile);
                                    File.Copy(DBF_File_Path + "/" + SubFile, DBF_File_Path + "/" + "DBF_Copy" + "/" + UserID + "/" + SubFile);

                                }
                                using (OleDbConnection connection = new OleDbConnection(sqlDbfConnection))
                                {


                                    OleDbCommand command = new OleDbCommand();
                                    command.Connection = connection;
                                    //Applied Condition for 1069 itrack, modified by naveen 
                                    if (SubFile == "PREMIT.DBF" || SubFile=="AT_MOR.DBF" || SubFile=="SINPAG.DBF")
                                    {
                                    try
                                    {
                                        connection.Open();
                                        foreach (DataRow datrow in ds.Tables[0].Rows)
                                        {

                                            String strrow = String.Empty;

                                            for (int count = 0; count < arColumnsName.Count; count++)
                                            {

                                                strrow += "'" + datrow[count].ToString().Replace("'", "") + "'" + " , ";
                                            }

                                            strrow = strrow.Substring(0, strrow.LastIndexOf(','));

                                            // command.CommandText = "INSERT INTO " + SubFile + "(" + " SEQUENCE, COD_CIA, DT_BASE, TIPO_PES, NOME_SUBSC, [CPF/CNPJ], ENDERECO,BAIRRO, CIDADE, UF, CEP, PAIS" + ")" + " VALUES (" + strrow + ")";
                                            command.CommandText = "INSERT INTO " + SubFile + " VALUES (" + strrow + ")";
                                            command.ExecuteNonQuery();

                                        }


                                        string sGenName = System.IO.Path.GetFileName(DBF_File_Path + "/DBF_Copy/" + UserID + "/" + SubFile);
                                        if (File.Exists(DBF_File_Path + "/" + "DBF_Copy/" + UserID + "/" + SubFile))
                                        {

                                            Response.ClearHeaders();
                                            Response.ClearContent();
                                            Response.AddHeader("Content-disposition", "attachment; filename=" + sGenName);
                                            Response.ContentType = "application/octet-stream";
                                            Response.Clear();
                                            Response.WriteFile(DBF_File_Path + "/DBF_Copy/" + UserID + "/" + SubFile);
                                            Response.End();

                                        }
                                    }

                                    catch (OleDbException ex)
                                    {
                                        lblErr.Visible = true;
                                        lblErr.Text = ex.Message.ToString();
                                        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                                    }
                                   
                                    }
                                    else
                                    {
                                    //Below Code is applied for bulk update of dbf template, modified by Naveen Pujari, itrack 529
                                    try
                                    {
                                        command.Connection = connection;
                                        command.CommandText = "select * from " + SubFile;
                                        OleDbDataAdapter Daoledb = new OleDbDataAdapter(command);
                                        DataSet dsoledb = new System.Data.DataSet();
                                        Daoledb.FillSchema(dsoledb, SchemaType.Source);
                                        Daoledb.Fill(dsoledb);
                                       
                                        DataColumnCollection DCSqlColumns = null;
                                        String OledbColumnName = string.Empty;
                                        String SQlColumnName = string.Empty;
                                        DataColumn dc = new System.Data.DataColumn();
                                       int counttest = 0;
                                        //DataRow row = dsoledb.Tables[0].NewRow();
                                        foreach (DataRow dtRw in ds.Tables[0].Rows)
                                        {

                                            DataRow row1 = dsoledb.Tables[0].NewRow();
                                            for (int i = 0; i < dtRw.Table.Columns.Count; i++) //loop for Sql dataset column
                                            {

                                                //DCOledb = row1.Table.Columns;
                                                DCSqlColumns = dtRw.Table.Columns;
                                                SQlColumnName = DCSqlColumns[i].ColumnName;
                                                //SQlColumnName = dtRw.Table.Columns
                                                 for (int i1 = 0; i1 < row1.Table.Columns.Count; i1++) // loop for Oledb template column 
                                                 {


                                                     DataColumnCollection DCCOlDB = row1.Table.Columns;
                                                     OledbColumnName = DCCOlDB[i1].ColumnName;
                                                     if (SQlColumnName == OledbColumnName)
                                                     {
                                                         row1[i1] = dtRw[i];
                                                         break;
                                                     }


                                                 }

                                                
                                                
                                            }
                                            dsoledb.Tables[0].Rows.Add(row1);

                                            
                                            counttest = counttest + 1;
                                        }

                                        OleDbCommandBuilder cb = new OleDbCommandBuilder(Daoledb);
                                        Daoledb.InsertCommand = cb.GetInsertCommand();
                                        Daoledb.Update(dsoledb);
                                        connection.Close();
                                        string sGenName = System.IO.Path.GetFileName(DBF_File_Path + "/DBF_Copy/" + UserID + "/" + SubFile);
                                        if (File.Exists(DBF_File_Path + "/" + "DBF_Copy/" + UserID + "/" + SubFile))
                                        {

                                            Response.ClearHeaders();
                                            Response.ClearContent();
                                            Response.Clear();
                                            Response.AddHeader("Content-disposition", "attachment; filename=" + sGenName);
                                            Response.ContentType = "application/octet-stream";
                                            Response.Clear();
                                            Response.WriteFile(DBF_File_Path + "/DBF_Copy/" + UserID + "/" + SubFile);
                                            Response.End();

                                        }


                                    }
                                    catch (OleDbException ex)
                                    {
                                        lblErr.Visible = true;
                                        lblErr.Text = ex.Message.ToString();
                                        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                                    }
                                    }


                                }

                            }


                        }

                        break;
                }
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
       

        private void ExportDataSetToExcel(DataSet ds, string filename)
        {
            //Modified by Pradeep on - 24- Aug -2011

            HttpResponse response = HttpContext.Current.Response;

            // first let's clean up the response.object   
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Charset = "";

            // set the response mime type for excel   
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
            //string strProcName = "";

            //if (Convert.ToString(ViewState["PROC_NAME"]) != "")
            //{
            //    strProcName = ViewState["PROC_NAME"].ToString();
            //}
            //else
            //{
            //    return;

            //}

            // create a string writer   
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    
                    if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                    {
                        LoadGeneralDynamicGrid(ds.Tables[0], htw);
                    }
                    DateTime dtExpirationStartDate=DateTime.Now;
                    DateTime dtInitialDate=DateTime.Now;
                    
                    DateTimeFormatInfo DateFormatinfoBR = new CultureInfo(enumCulture.BR, true).DateTimeFormat;
                    DateTimeFormatInfo DateFormatinfoUS = new CultureInfo(enumCulture.US, true).DateTimeFormat;
                    DateTime dt2 = new DateTime();


                    if (txtinitialDate.Text.ToString().Trim() != "")
                    {

                        if (oldculture.Name == enumCulture.BR)
                            dtInitialDate = Convert.ToDateTime(Convert.ToDateTime(txtinitialDate.Text, DateFormatinfoBR), DateFormatinfoUS);
                        else
                            dtInitialDate = Convert.ToDateTime(txtinitialDate.Text, DateFormatinfoUS);
                    }
                    else
                        dtInitialDate = ConvertToDate(null);

                    if (txtExpirationStartDate.ToString().Trim() != "")
                    {

                        if (oldculture.Name == enumCulture.BR)
                            dtExpirationStartDate = Convert.ToDateTime(Convert.ToDateTime(txtExpirationStartDate.Text, DateFormatinfoBR), DateFormatinfoUS);
                        else
                            dtExpirationStartDate = Convert.ToDateTime(txtExpirationStartDate.Text, DateFormatinfoUS);
                    }
                    else
                    {
                        dtExpirationStartDate = ConvertToDate(null);
                    }
                    if (dt2.ToString().Trim() != "")
                    {

                        if (oldculture.Name == enumCulture.BR)
                            dt2 = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now, DateFormatinfoBR), DateFormatinfoUS);
                        else
                            dt2 = Convert.ToDateTime(DateTime.Now, DateFormatinfoUS);
                    }
                    else
                        dt2 = ConvertToDate(null);





                    //DateTime dt = new DateTime();
                    //dt = Convert.ToDateTime(txtinitialDate.Text);
                    //DateTime dt1 = new DateTime();
                    //dt1 = Convert.ToDateTime(txtExpirationStartDate.Text);
                    //DateTime dt2 = new DateTime();
                    //dt2 = DateTime.Now;
                    switch (PROC_NAME)
                    {

                        
                        case "PROC_RVNE_REPORT":
                             response.Write("<html><head><meta http-equiv=Content-Type content='text/html; charset=utf-8'>");
                    response.Write("<style>td{mso-number-format:\\@}</style>");
                    response.Write("<h5><table border=0 cellpadding=0 cellspacing=0 width=1768 style='border-collapse:collapse;table-layout:fixed;width:1328pt'> " +
                    "<tr class=xl67 height=41 style='mso-height-source:userset;height:30.75pt'> " +
                    "<td height=41 class=xl67 width=121 style='height:30.75pt;width:91pt'></td> " +
                    "<td class=xl67 width=87 style='width:65pt'></td> " +
                    "<td colspan=10 class=xl68 width=1100 style='font-family:arial;color:black;font-size:17px;text-align:center;font-weight:700;'>Relação das emissões realizadas no período de " + dtInitialDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " até " + dtExpirationStartDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + "</td></tr> " +
                    "<tr class=xl67 height=40 style='mso-height-source:userset;height:30.0pt'> " +
                    "<td height=40 colspan=2 class=xl67 style='height:30.0pt;mso-ignore:colspan'></td> " +
                    "<td colspan=10 class=xl68 width=1100 style='font-family:arial;color:black;font-size:17px;text-align:center;font-weight:700;'>Emissão em " + dt2.ToString("dd/MM/yyyy - hh:mm", CultureInfo.InvariantCulture) + " </td> " +
                    "<td class=xl67></td> " +
                    "</tr></table></h5>");
                    response.Write("<table border=0 cellpadding=0 cellspacing=0 width=1768 style='border-collapse:collapse;table-layout:fixed;width:1328pt'> " +
                      "<tr class=xl67 height=41 style='mso-height-source:userset;height:30.75pt'> <td scope='col'  style='border:none';>&nbsp;</td> " +
                      "<td scope='col'  style='border:none';>&nbsp;</td> " +
                      "<td scope='col'  style='border:none';>&nbsp;</td> " +
                      "<td scope='col'  style='border:none';>&nbsp;</td> " +
                      "<td scope='col'  style='border:none';>&nbsp;</td> " +
                      "<td scope='col'  style='border:none';>&nbsp;</td> " +
                      "<td scope='col' colspan = 5 style='border:1px solid black;text-align:center;font-weight:700;margin-top:1cm;font-size:15px;';>Valores da Emissão</td> " +
                      "<td scope='col' colspan = 4 style='border:1px solid black;text-align:center;font-weight:700;margin-top:1cm;font-size:15px;';>Cessões</td> " +
                      "<td scope='col' colspan = 2 style='border:1px solid black;text-align:center;font-weight:700;margin-top:1cm;font-size:15px;';>Comercialização</td> " +
                      "</tr></table>");
                            break;
                        case "PROC_IBNR_REPORT":
                             response.Write("<html><head><meta http-equiv=Content-Type content='text/html; charset=utf-8'>");
                    response.Write("<style>td{mso-number-format:\\@}</style>");
                    response.Write("<h5><table border=0 cellpadding=0 cellspacing=0 width=1768 style='border-collapse:collapse;table-layout:fixed;width:1328pt'> " +
                    "<tr class=xl67 height=41 style='mso-height-source:userset;height:30.75pt'> " +
                    "<td height=41 class=xl67 width=121 style='height:30.75pt;width:91pt'></td> " +
                    "<td class=xl67 width=87 style='width:65pt'></td> " +
                    "<td colspan=10 class=xl68 width=1100 style='font-family:arial;color:black;font-size:17px;text-align:center;font-weight:700;'>Relação dos sinistros avisados no período de " + dtInitialDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " até " + dtExpirationStartDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + "</td></tr> " +
                    "<tr class=xl67 height=40 style='mso-height-source:userset;height:30.0pt'> " +
                    "<td height=40 colspan=2 class=xl67 style='height:30.0pt;mso-ignore:colspan'></td> " +
                    "<td colspan=10 class=xl68 width=1100 style='font-family:arial;color:black;font-size:17px;text-align:center;font-weight:700;'>Emissão em " + dt2.ToString("dd/MM/yyyy - hh:mm", CultureInfo.InvariantCulture) + " </td> " +
                    "<td class=xl67></td> " +
                    "</tr></table></h5>");
                    response.Write("<table border=0 cellpadding=0 cellspacing=0 width=1768 style='border-collapse:collapse;table-layout:fixed;width:1328pt'> " +
                   "<tr class=xl67 height=41 style='mso-height-source:userset;height:30.75pt'> <td scope='col'  style='border:none';>&nbsp;</td> " +
                   "<td scope='col'  style='border:none';>&nbsp;</td> " +
                   "<td scope='col'  style='border:none';>&nbsp;</td> " +
                   "<td scope='col'  style='border:none';>&nbsp;</td> " +
                   "<td scope='col'  style='border:none';>&nbsp;</td> " +
                   "<td scope='col'  style='border:none';>&nbsp;</td> " +
                   "<td scope='col'  style='border:none';>&nbsp;</td> " +
                   "<td scope='col'  style='border:none';>&nbsp;</td> " +
                   "<td scope='col' colspan = 4 style='border:1px solid black;text-align:center;font-weight:700;margin-top:1cm;font-size:15px;';>Valores</td> " +
                   "</tr></table>");
                            break;
                        case "PROC_REL_SIN_RET_EPG":
                             response.Write("<html><head><meta http-equiv=Content-Type content='text/html; charset=utf-8'>");
                    response.Write("<style>td{mso-number-format:\\@}</style>");
                    response.Write("<h5><table border=0 cellpadding=0 cellspacing=0 width=1768 style='border-collapse:collapse;table-layout:fixed;width:1328pt'> " +
                    "<tr class=xl67 height=41 style='mso-height-source:userset;height:30.75pt'> " +
                    "<td height=41 class=xl67 width=121 style='height:30.75pt;width:91pt'></td> " +
                    "<td class=xl67 width=87 style='width:65pt'></td> " +
                    "<td colspan=10 class=xl68 width=1100 style='font-family:arial;color:black;font-size:17px;text-align:center;font-weight:700;'>Relação dos sinistros retidos e prêmios ganhos  no período de " + dtInitialDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " até " + dtExpirationStartDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + "</td></tr> " +
                    "<tr class=xl67 height=40 style='mso-height-source:userset;height:30.0pt'> " +
                    "<td height=40 colspan=2 class=xl67 style='height:30.0pt;mso-ignore:colspan'></td> " +
                    "<td colspan=10 class=xl68 width=1100 style='font-family:arial;color:black;font-size:17px;text-align:center;font-weight:700;'>Emissão em " + dt2.ToString("dd/MM/yyyy - hh:mm", CultureInfo.InvariantCulture) + " </td> " +
                    "<td class=xl67></td> " +
                    "</tr></table></h5>");

                            break;
                    case "PROC_PPNG_REPORT":
                            response.Write("<html><head><meta http-equiv=Content-Type content='text/html; charset=utf-8'>");
                            response.Write("<style>td{mso-number-format:\\@}</style>");
                            //response.Write("<h5><table border=0 cellpadding=0 cellspacing=0 width=1768 style='border-collapse:collapse;table-layout:fixed;width:1328pt'> " +
                            //"</table></h5>");

                            break;
                        default:
                            break;


                    }
                   
                   
                  
                    response.Write("");
                    response.Write(sw.ToString());
                    response.Write("</body></html>");
                    response.End();
                }

            }
            //till here 

        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void btnresetreport_Click(object sender, EventArgs e)
        {

        }

   

        protected void btnReport_Click(object sender, EventArgs e)
        {


            try
            {
               
              // GenerateTxt();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "Script", "<script>document.getElementById('btnReport').disabled = false;</script>");
            }
            catch (Exception ex)
            {

                lblErr.Visible = true;
                lblErr.Text = ex.Message.ToString();

                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

            }
        }


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
                if (col.ColumnName.ToString().ToUpper() != "POLICY_NUMBER" && col.ColumnName.ToString().ToUpper() != "CLAIM_NUMBER")
                {
                    BoundField bfield = new BoundField();

                    //Initalize the DataField value.
                    bfield.DataField = col.ColumnName;
                    //bfield.HtmlEncodeFormatString
                    //Initialize the HeaderText field value.
                    bfield.HeaderText = col.ColumnName;
                    //Add the newly created bound field to the GridView.
                    //Gv_General.Columns[countcolumn].ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                    Gv_General.Columns.Add(bfield);
                }
            }

        }

       

    }
}
