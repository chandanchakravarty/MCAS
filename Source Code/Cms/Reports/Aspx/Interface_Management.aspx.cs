using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.CmsWeb;
using System.Resources;
using System.Reflection;
using System.Collections;
using System.Drawing;

namespace Cms.Reports.Aspx
{
    public partial class Interface_Management : Cms.CmsWeb.cmsbase
    {
        protected System.Web.UI.WebControls.Label lblRecord_NotFound;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvInputOutput;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvInceptionStartDate;
        ResourceManager Objresources;
        DataSet ds = new DataSet();
        Hashtable hstInterface_Type = new Hashtable();
        string Argument;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "537";
            btnCancel.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnCancel.PermissionString = gstrSecurityXML;
            btnSearch.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnSearch.PermissionString = gstrSecurityXML;
            btnReProcess.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnReProcess.PermissionString = gstrSecurityXML;
            btnShowRecord.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnShowRecord.PermissionString = gstrSecurityXML;
            
            Objresources = new System.Resources.ResourceManager("Cms.Reports.Aspx.Interface_Management", System.Reflection.Assembly.GetExecutingAssembly());
            

            hlkCREATE_DATE.Attributes.Add("OnClick", "fPopCalendar(txtCREATE_DATE,txtCREATE_DATE)");
            //hlkCREATE_DATE.Attributes.Add("OnClick", "fPopCalendar(document.PAGNET_EXPORT_FILES.txtCREATE_DATE,document.PAGNET_EXPORT_FILES.txtCREATE_DATE)");
            hlkInceptionEndDate.Attributes.Add("OnClick", "fPopCalendar(document.forms[0].txtInceptionEndDate,document.forms[0].txtInceptionEndDate)");
            headerRecord_FollowUp.Attributes.Add("style", "display:none");
            lblRecord_NotFound.Visible = false;
            if (!(Page.IsPostBack))
            {
                BindInterface_file();                
                SetErrorMsg();
                headerSearch_List.Attributes.Add("style", "display:none");
                BindRblFileRecord();
                BindrblInputOutput();
                capInterface_Type.Text = Objresources.GetString("capInterface_Type");
                capInterface_File.Text = Objresources.GetString("capInterface_File");
                capInputOutput.Text = Objresources.GetString("capInputOutput");
                capFile_Date.Text = Objresources.GetString("capFile_Date");
                lblInceptionStartDate.Text = Objresources.GetString("lblInceptionStartDate");
                lblInceptionEndDate.Text = Objresources.GetString("lblInceptionEndDate");
                btnCancel.Text = Objresources.GetString("btnCancel");
                btnSearch.Text = Objresources.GetString("btnSearch");
                lblSearch.Text = Objresources.GetString("lblSearch");
                lblHeader.Text = Objresources.GetString("lblHeader");
                lblRecords_Followup.Text = Objresources.GetString("lblRecords_Followup");
                btnReProcess.Text = Objresources.GetString("btnReProcess");
                btnReProcess.Attributes.Add("style", "display:none");
            }



           
           if (Request.Form["__EVENTTARGET"] == "btnShowRecord")// && Request.Form["__EVENTARGUMENT"] == "ERR")
           {
              // int row;
               Argument = Request.Form["__EVENTARGUMENT"];
               /*row = Convert.ToInt32(Argument.Substring(Argument.IndexOf('^')+1,Argument.Length-1-Argument.IndexOf('^')));
               dgrInterface.RowStyle.BackColor = System.Drawing.Color.Crimson  //"#F3F3F3";
               dgrInterface.Rows[row].BackColor = System.Drawing.Color.Yellow;*/
               btnShowRecord_Click(null, null);
           }
          
        }

        private void BindRblFileRecord()
        {

            Hashtable hsFileRecord = new Hashtable();
            string strKey1 = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2018");
            string strKey2 = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2019");


            hsFileRecord.Add(strKey1, "1");
            hsFileRecord.Add(strKey2, "2");

            rblFileRecord.DataSource = hsFileRecord;
            rblFileRecord.DataTextField = "key";
            rblFileRecord.DataValueField = "value";
            rblFileRecord.DataBind();
        }

        private void BindrblInputOutput()
        {

            Hashtable hsInputOutput = new Hashtable();
            string strKey3 = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2020");
            string strKey4 = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2021");


            hsInputOutput.Add(strKey3, "1");
            hsInputOutput.Add(strKey4, "2");

            rblInputOutput.DataSource = hsInputOutput;
            rblInputOutput.DataTextField = "key";
            rblInputOutput.DataValueField = "value";
            rblInputOutput.DataBind();
        }
        private void BindDropDownStatus()
        {
            if (Cms.BusinessLayer.BlCommon.ClsCommon.BL_LANG_ID == 2)
            {
                Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
                ds = objDataWrapper.ExecuteDataSet("PROC_PAGNET_GET_RECORD_STATUS");
                cmbStatus.DataSource = ds;
                cmbStatus.DataTextField = "PORT_DESCRIPTION";
                cmbStatus.DataValueField = "ENG_DESCRIPTION";
                cmbStatus.DataBind();
            }

            else
            {
                Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
                ds = objDataWrapper.ExecuteDataSet("PROC_PAGNET_GET_RECORD_STATUS");
                cmbStatus.DataSource = ds;
                cmbStatus.DataTextField = "ENG_DESCRIPTION";
                cmbStatus.DataValueField = "ENG_DESCRIPTION";
                cmbStatus.DataBind();


            }
        }

        private void BindInterface_file()
        {
            
            hstInterface_Type.Add("Second", "Pagnet");
            hstInterface_Type.Add("Third", "Sun");

            foreach (DictionaryEntry Item in hstInterface_Type)
            {
                ListItem newListItem = new ListItem();
                newListItem.Text = Item.Value.ToString();
                newListItem.Value = Item.Key.ToString();
                cmbInterface_Type.Items.Add(newListItem);

            }

            cmbInterface_Type.Items.Insert(0, "");
        }


        protected void SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable hstInterface_File = new Hashtable();
            if (Cms.BusinessLayer.BlCommon.ClsCommon.BL_LANG_ID == 2)
            {
                hstInterface_File.Add(1, "Comissão");
                hstInterface_File.Add(2, "Despesas de Sinistro");
                hstInterface_File.Add(3, "Indenização de Sinistro");
                hstInterface_File.Add(4, "Restituição/Devolução Segurado");
                hstInterface_File.Add(5, "Cosseguro ou Resseguro");
            }
            else
            {
                hstInterface_File.Add(1, "Commission");
                hstInterface_File.Add(2, "Claim Expense");
                hstInterface_File.Add(3, "Claim Indemnity");
                hstInterface_File.Add(4, "Customer Refund");
                hstInterface_File.Add(5, "CO or RI");
            }
            Hashtable hstInterface_File_Sun = new Hashtable();
            hstInterface_File_Sun.Add(1, "Sun");
            cmbInterface_File.Items.Clear();
            if (cmbInterface_Type.SelectedIndex == 2)
            {
                foreach (DictionaryEntry Item in hstInterface_File)
                {
                    ListItem newListItem = new ListItem();
                    newListItem.Text = Item.Value.ToString();
                    newListItem.Value = Item.Key.ToString();
                    cmbInterface_File.Items.Add(newListItem);
                }
                rblFileRecord.Enabled = true;
                rblInputOutput.Enabled = true;
                rfvInterface_Type.Enabled = true;
                rfvInputOutput.Enabled = true;
                rfvFileRecord.Enabled = true;
                gvRecordSearch.Visible = true;
                dgrInterface.Visible = true;
                gvMeleventos.Visible = false;
                gvSunRecordSearch.Visible = false;

                btnReProcess.Visible = true;
                dgrShowRecords.Visible = true;

            }

            else if (cmbInterface_Type.SelectedIndex == 1)
            {

                foreach (DictionaryEntry Item in hstInterface_File_Sun)
                {
                    ListItem newListItem = new ListItem();
                    newListItem.Text = Item.Value.ToString();
                    newListItem.Value = Item.Key.ToString();
                    cmbInterface_File.Items.Add(newListItem);
                }
                rblFileRecord.Enabled = false;
                rblInputOutput.Enabled =false;
                rfvInterface_Type.Enabled = false;
                rfvFileRecord.Enabled = false;
                rfvInputOutput.Enabled = false;
                gvRecordSearch.Visible = false;
                dgrShowRecords.Visible = false;
                dgrInterface.Visible = false;
                btnReProcess.Visible = false;
                //gvMeleventos.Visible = true;
            }


        }

        public  string GetFileName(string strFilePath)
        {
            string fileName = "";
            try
            {
                
                if (!String.IsNullOrEmpty(strFilePath))
                {
                    fileName = System.IO.Path.GetFileName(strFilePath);                    
                }
                
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }

            return fileName;
             
        }

        private void GetInterfaceData(string INTERFACE_FILE, int FILE_TYPE, string FROM_DATE, string TO_DATE)
        {

            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.AddParameter("@INTERFACE_FILE", INTERFACE_FILE);
                objDataWrapper.AddParameter("@FILE_TYPE", FILE_TYPE);
                objDataWrapper.AddParameter("@FROM_DATE",String.Format("{0:MM/dd/yyyy}", ConvertToDate(FROM_DATE))); 
                objDataWrapper.AddParameter("@TO_DATE", String.Format("{0:MM/dd/yyyy}", ConvertToDate(TO_DATE)));
                //objDataWrapper.AddParameter("@STATUS", status);
                ds = objDataWrapper.ExecuteDataSet("Proc_Fetch_Interface_Management");
                
                ////itrack # 1405//////

                if (Cms.BusinessLayer.BlCommon.ClsCommon.BL_LANG_ID == 2)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["FILE_TYPE"].ToString() == "Commission")
                        {
                            dr["FILE_TYPE"] = "Comissão";
                        }

                        if (dr["FILE_TYPE"].ToString() == "Claim Expense")
                        {
                            dr["FILE_TYPE"] = "Despesas de Sinistro";
                        }

                        if (dr["FILE_TYPE"].ToString() == "Claim Indemnity")
                        {
                            dr["FILE_TYPE"] = "Indenização de Sinistro";
                        }
                        if (dr["FILE_TYPE"].ToString() == "Customer Refund")
                        {
                            dr["FILE_TYPE"] = "Restituição/Devolução Segurado";
                        }
                        if (dr["FILE_TYPE"].ToString() == "CO or RI")
                        {
                            dr["FILE_TYPE"] = "Cosseguro ou Resseguro";
                        }
                        if (dr["INPUT_OUTPUT"].ToString() == "Output")
                        {
                            dr["INPUT_OUTPUT"] = "Saída";
                        }
                        if (dr["INPUT_OUTPUT"].ToString() == "Input")
                        {
                            dr["INPUT_OUTPUT"] = "Entrada";
                        }

                       

                    }
                }
                //DataTable dt = new DataTable();
                //headerRecord_FollowUp.Visible = true;
                //headerRecord_FollowUp.Style.Add("display", "none");
                btnReProcess.Style.Add("display", "none"); 
               // btnReProcess.Visible = true;
                dgrShowRecords.Style.Add("display", "none"); 
                //dgrShowRecords.Visible = true;
                lblRecord_NotFound.Text = "";
                if (ds != null && ds.Tables.Count > 0)
                {
                    dgrInterface.DataSource = ds;
                    dgrInterface.DataBind();
                }
                if (ds.Tables[0].Rows.Count < 1 || ds == null || ds.Tables.Count < 1)
                {
                    //headerRecord_FollowUp.Visible = false;
                    //headerRecord_FollowUp.Style.Add("display", "none");
                    btnReProcess.Style.Add("display", "none"); 
                    //btnReProcess.Visible = false;
                    dgrShowRecords.Style.Add("display", "none"); 
                    //dgrShowRecords.Visible = false;
                    lblRecord_NotFound.Visible = true;
                    lblRecord_NotFound.Text = Objresources.GetString("lblRecord_NotFound");//"No Record Exist";
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }



        private void GetMeleventosData(string FROM_DATE,string TO_DATE)
        {

            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.AddParameter("@FROM_DATE", String.Format("{0:MM/dd/yyyy}", ConvertToDate(FROM_DATE)));
                objDataWrapper.AddParameter("@TO_DATE", String.Format("{0:MM/dd/yyyy}", ConvertToDate(TO_DATE)));
                

                ds = objDataWrapper.ExecuteDataSet("PROC_GET_MELEVENTOS_FOR_INTERFACE_MGMT");

                if (ds != null && ds.Tables.Count > 0)
                {
                    
                        gvMeleventos.DataSource = ds;
                        gvMeleventos.DataBind();
                    

                }
                if (ds.Tables[0].Rows.Count < 1 || ds == null || ds.Tables.Count < 1)
                {
                    //if (flag_status == 1)
                    //{
                        lblRecord_NotFound.Visible = true;
                        lblRecord_NotFound.Text = Objresources.GetString("lblRecord_NotFound");
                    //}
                    return;
                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        private void GetRecordData(int FILE_ID, int flag_status, string status, string FROM_DATE, string TO_DATE)
        {

            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.AddParameter("@FILE_ID", FILE_ID);
                objDataWrapper.AddParameter("@flagStatus", flag_status);
                objDataWrapper.AddParameter("@STATUS", status);
                if (flag_status == 1)
                {
                    objDataWrapper.AddParameter("@FROM_DATE", String.Format("{0:MM/dd/yyyy}", ConvertToDate(FROM_DATE)));
                    objDataWrapper.AddParameter("@TO_DATE", String.Format("{0:MM/dd/yyyy}", ConvertToDate(TO_DATE)));
                    objDataWrapper.AddParameter("@FILE_TYPE", Convert.ToInt32(cmbInterface_File.SelectedValue));
                }
                ds = objDataWrapper.ExecuteDataSet("Proc_Fetch_Interface_Records");

                //applied the below code to  change the record on the gridview display, changes by naveen , itrack 1405
                if (Cms.BusinessLayer.BlCommon.ClsCommon.BL_LANG_ID == 2)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["RETURN_STATUS"].ToString() == "Record Generated")
                        {
                            dr["RETURN_STATUS"] = "Registro Criado";
                        }
                        if (dr["RETURN_STATUS"].ToString() == "Sent to Pagnet")
                        {
                            dr["RETURN_STATUS"] = "Enviado para o Pagnet";
                        }
                        if (dr["RETURN_STATUS"].ToString() == "Re-sent to Pagnet")
                        {
                            dr["RETURN_STATUS"] = "Re-enviado para o Pagnet";
                        }
                        if (dr["REFUND_PAYMENT_DESCRIPTION"].ToString() == "Professional custa serviço para reivindicar normal para o CO")
                        {
                            dr["REFUND_PAYMENT_DESCRIPTION"] = "Custos do Profissional para reivindicação normal de Cosseguro";
                        }
                        if (dr["REFUND_PAYMENT_DESCRIPTION"].ToString() == "Reembolso ao Segurado Premium")
                        {
                            dr["REFUND_PAYMENT_DESCRIPTION"] = "Prêmio de Reembolso para Segurado";
                        }
                        if (dr["REFUND_PAYMENT_DESCRIPTION"].ToString() == "Pagamento CO")
                        {
                            dr["REFUND_PAYMENT_DESCRIPTION"] = "Pagamento de Cosseguro";
                        }
                        if (dr["REFUND_PAYMENT_DESCRIPTION"].ToString() == "Despesa com pedido normal de CO aceite")
                        {
                            dr["REFUND_PAYMENT_DESCRIPTION"] = "Despesa com pedido normal de CO Aceito";
                        }
                        if (dr["REFUND_PAYMENT_DESCRIPTION"].ToString() == "Indenização por pedido normal para aceite CO")
                        {
                            dr["REFUND_PAYMENT_DESCRIPTION"] = "Indenização de pedido normal para CO Aceito";
                        }
                        if (dr["REFUND_PAYMENT_DESCRIPTION"].ToString() == "Pagamento RI")
                        {
                            dr["REFUND_PAYMENT_DESCRIPTION"] = "Pagamento de Resseguro";
                        }
                        if (dr["REFUND_PAYMENT_DESCRIPTION"].ToString() == "Indenização por reivindicação legal para aceite CO")
                        {
                            dr["REFUND_PAYMENT_DESCRIPTION"] = "Indenização por Sinistro Jurídico de CO Aceito";
                        }
                        if (dr["REFUND_PAYMENT_DESCRIPTION"].ToString() =="Custos do Profissional para reivindicação normal de Cosseguro")
                        {
                            dr["REFUND_PAYMENT_DESCRIPTION"] = "Custos do Profissional para Sinistro Normal de Cosseguro";
                        }
                        if (dr["REFUND_PAYMENT_DESCRIPTION"].ToString() == "Agente de Pagamento de Comissão")
                        {
                            dr["REFUND_PAYMENT_DESCRIPTION"] = "Pagamento de Comissão do Corretor";
                        }

                        if (dr["REFUND_PAYMENT_DESCRIPTION"].ToString() == "Pagamento de comissão de corretagem")
                        {
                            dr["REFUND_PAYMENT_DESCRIPTION"] = "Pagamento de Comissão do Corretor";
                        }




                    }
                }


                if (ds != null && ds.Tables.Count > 0)
                {
                    if(flag_status==0)
                    {
                    dgrShowRecords.DataSource = ds;
                    dgrShowRecords.DataBind();
                    }
                    else
                    {
                        gvRecordSearch.DataSource = ds;
                        gvRecordSearch.DataBind();
                    }

                }
                if (ds.Tables[0].Rows.Count < 1 || ds == null || ds.Tables.Count < 1)
                {
                    if (flag_status == 1)
                    {
                        lblRecord_NotFound.Visible = true;
                        lblRecord_NotFound.Text = Objresources.GetString("lblRecord_NotFound");
                    }
                    return;
                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }







        private void GetReprocessData(int FILE_ID,int flag_reprocess)
        {

            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.AddParameter("@FILE_ID", FILE_ID);
                objDataWrapper.AddParameter("@FLAG_REPROCESS", flag_reprocess);
                int Rows_Updated = objDataWrapper.ExecuteNonQuery("PROC_REPROCESS_PAGNET_EXPORT_RECORD");
               // string strValue = "<script>alert(hiii)</script>";
                string script = "<script type='text/javascript'>alert('Records updated successfully!!');</script>";

                ClientScript.RegisterStartupScript(this.GetType(), "Show Message", script);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }





        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            try
            {

               // dgrShowRecords.Style.Add("display", "none"); 
                //dgrShowRecords.Visible = false;
                if (cmbInterface_Type.SelectedIndex == 2)
                {

                    if (rblFileRecord.SelectedIndex == 0)
                    {
                        this.GetInterfaceData(cmbInterface_File.SelectedValue, int.Parse(rblInputOutput.SelectedValue), txtCREATE_DATE.Text, txtInceptionEndDate.Text);
                        headerSearch_List.Attributes.Add("style", "display:inline");
                        //headerRecord_FollowUp.Attributes.Add("style", "display:none");
                    }
                    else if (rblFileRecord.SelectedIndex == 1)
                    {

                        GetRecordData(Convert.ToInt32(cmbInterface_File.SelectedValue), 1, cmbStatus.SelectedValue, txtCREATE_DATE.Text, txtInceptionEndDate.Text);
                        headerSearch_List.Attributes.Add("style", "display:inline");
                    }
                    else
                    {

                    }
                }
                else if (cmbInterface_Type.SelectedIndex == 1)
                {
                    gvMeleventos.Visible = true;
                    GetMeleventosData(txtCREATE_DATE.Text, txtInceptionEndDate.Text);
                   
                }
                else
                {
 
                }
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                //throw (ex);
            }

        }

        

        protected void dgrInterface_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Label capFILE_ID = (Label)e.Row.FindControl("capFILE_ID");
              //  capFILE_ID.Text = Objresources.GetString("capFILE_ID");
                //Label capInputOutput = (Label)e.Row.FindControl("capInputOutput");
                //capInputOutput.Text = Objresources.GetString("capInputOutput");
                Label l1 = (Label)e.Row.FindControl("capFILE_TYPE");
                l1.Text = Objresources.GetString("capFILE_TYPE");
                Label l2 = (Label)e.Row.FindControl("capFILE_NAMES");
                l2.Text = Objresources.GetString("capFILE_NAMES");
                Label l3 = (Label)e.Row.FindControl("capINPUT_OUTPUT");
                l3.Text = Objresources.GetString("capINPUT_OUTPUT");
                Label l4 = (Label)e.Row.FindControl("capCREATE_DATE");
                l4.Text = Objresources.GetString("capCREATE_DATE");
                Label l5 = (Label)e.Row.FindControl("capNUMBER_OF_RECORDS");
                l5.Text = Objresources.GetString("capNUMBER_OF_RECORDS");
                Label l6 = (Label)e.Row.FindControl("capCURRENT_FILE_FOLDER");
                l6.Text = Objresources.GetString("capCURRENT_FILE_FOLDER");
            }
            if (e.Row.RowType == DataControlRowType.DataRow) 
            {
                //e.Row.Attributes.Add("OnMouseOver", "javascript:OnMouseOver(this);");// this.style.backgroundColor = 'lightsteelblue';");
                e.Row.Attributes.Add("OnMouseOver", "javascript:OnMouseOver(this," + e.Row.RowIndex + ");");
                e.Row.Attributes.Add("onclick", "javascript:return OnSelect(this,"+e.Row.RowIndex+");");
                //e.Row.Attributes.Add("OnMouseOut", "javascript:OnMouseOut(this);");// this.style.backgroundColor = '#f3f3f3';");
                e.Row.Attributes.Add("OnMouseOut", "javascript:OnMouseOut(this," + e.Row.RowIndex + ");");
               // e.Row.Attributes.Add("ondblclick", "javascript:onDoubleClick(this);");// this.style.backgroundColor = '#f3f3f3';");
               // headerSearch_List.Attributes.Add("style", "display:inline"); 
            }



            
        }


        private void SetErrorMsg()
        {
            revCREATE_DATE.ValidationExpression = aRegExpDate;
            revCREATE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1380");
            revInceptionEndDate.ValidationExpression = aRegExpDate;
            revInceptionEndDate.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1380");
             rfvInterface_Type.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1");
            rfvInputOutput.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2");
            rfvCREATE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "3");
            rfvInceptionEndDate.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "5");
            revInceptionEndDate.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            rfvFileRecord.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "4");
            cmpToDate.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("733");
        }

        protected void btnShowRecord_Click(object sender, EventArgs e)
        {
            try
            {
                dgrShowRecords.Attributes.Add("style", "display:inline");
                headerRecord_FollowUp.Style.Add("display", "inline");
                btnReProcess.Style.Add("style", "display:inline");
                GetRecordData(Convert.ToInt32(hidFILE_ID.Value), 0, "", "", "");
                //headerRecord_FollowUp.Attributes.Add("style", "display:inline");

                string FileFolder = "";
                int iRowindex = 0;
                if (Argument.Contains("^"))
                {
                    string[] arrString = Argument.Split('^');
                    FileFolder = arrString[0].ToString().ToUpper();
                    if (arrString[1].ToString() != "")
                        iRowindex = int.Parse(arrString[1]);
                }
                if (FileFolder == "ERR" || FileFolder == "APR")
                {
                    ViewState["FileFolder"] = FileFolder;
                    btnReProcess.Attributes.Add("style", "display:inline");
                }
                else
                {
                    btnReProcess.Attributes.Add("style", "display:none");
                }
                //dgrInterface.Rows[iRowindex].BackColor = ColorTranslator.FromHtml("#1E90FF");


                foreach (GridViewRow row in dgrInterface.Rows)
                {
                    if (row.RowIndex == iRowindex)
                    {
                        row.BackColor = ColorTranslator.FromHtml("#1E90FF");
                        //break;
                    }
                    else
                    {
                        row.BackColor = ColorTranslator.FromHtml("#f3f3f3");
                    }
                }
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }

            
        }

        protected void btnReProcess_Click(object sender, EventArgs e)
        {
            try
            {

                if (ViewState["FileFolder"] != null)
                {
                    if (ViewState["FileFolder"].ToString() == "ERR")
                    {
                        GetReprocessData(Convert.ToInt32(hidFILE_ID.Value), 0);
                        headerRecord_FollowUp.Style.Add("display", "inline");
                    }
                    else if (ViewState["FileFolder"].ToString() == "APR")
                    {
                        GetReprocessData(Convert.ToInt32(hidFILE_ID.Value), 1);
                        headerRecord_FollowUp.Style.Add("display", "inline");
                    }
                    else
                    {

                    }
                }
            }           
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                //throw (ex);
            }

          
          
        }

        

        protected void rblFileRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblFileRecord.SelectedIndex == 0)
            {
                trStatus.Visible = false;
                trInputOutput.Visible = true;
                gvRecordSearch.Visible = false;
                btnReProcess.Visible = true;
                dgrInterface.Visible = true;
                dgrShowRecords.Visible = true;
                
            }
            else if (rblFileRecord.SelectedIndex == 1)
            {
                trStatus.Visible = true;
                trInputOutput.Visible = false;
                BindDropDownStatus();
                dgrInterface.Visible = false;
                dgrShowRecords.Visible = false;
                btnReProcess.Visible = false;
                gvRecordSearch.Visible = true;
                btnReProcess.Visible = false;
            }
            else
            { 
            }
        }

        protected void gvMeleventos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Item.Cells[7].Text = Objresources.GetString("ZIP_CODE");
                Label l7 = (Label)e.Row.FindControl("capSERIAL_NO");
                l7.Text = Objresources.GetString("capSERIAL_NO");
                Label l8 = (Label)e.Row.FindControl("capFILE_NAMES");
                l8.Text = Objresources.GetString("capFILE_NAMES");
                Label l9 = (Label)e.Row.FindControl("capCreateDate");
                l9.Text = Objresources.GetString("capCreateDate");


            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((Label)e.Row.FindControl("lblFILE_NAMES")).Text = GetFileName(DataBinder.Eval(e.Row.DataItem, "FILE_NAMES").ToString());
                //e.Row.Cells[1].Text = GetFileName(DataBinder.Eval(e.Row.DataItem, "FILE_NAMES").ToString());
                string dd = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "START_DATETIME").ToString()).ToString();
                //e.Row.Cells[2].Text = ConvertToDate(dd).ToString();
                ((Label)e.Row.FindControl("lblCreateDate")).Text =  ConvertToDate(dd).ToString();

                                                 
                 //e.Row.Attributes.Add("OnMouseOver", "javascript:OnMouseOver(this," + e.Row.RowIndex + ");");
                 //e.Row.Attributes.Add("onclick", "javascript:return OnSelect(this," + e.Row.RowIndex + ");");
                 //e.Row.Attributes.Add("OnMouseOut", "javascript:OnMouseOut(this," + e.Row.RowIndex + ");");
                   
               
            }

        }

        protected void gvMeleventos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "select")
                {
                    int index = Convert.ToInt32(e.CommandArgument);


                    GridView customersGridView = (GridView)e.CommandSource;                  

                    string strDate ="";                   
                    strDate = strDate = ((Label)customersGridView.Rows[index].FindControl("lblFILE_NAMES")).Text;
                   
                    string strDate_org = "";

                    if (strDate.IndexOf('_') > 0)
                    {
                        strDate_org = strDate.Substring(strDate.IndexOf('_') + 1, strDate.Length - strDate.IndexOf('_') - 2);
                        strDate = strDate_org.Substring(2, 2);
                        strDate = strDate + '/' + strDate_org.Substring(0, 2);
                        strDate = strDate + '/' + strDate_org.Substring(4, 4);
                    }
                    else
                    {
                        return;
                    }


                    foreach (GridViewRow row in gvMeleventos.Rows)
                    {
                        row.BackColor = ColorTranslator.FromHtml("#f3f3f3");
                        //break;#1E90FF
                    }
                    customersGridView.Rows[index].BackColor = ColorTranslator.FromHtml("#1E90FF");

                    Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
                    DataSet ds = new DataSet();

                    //string strDate = "";                
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.AddParameter("@INCEPTION_DATE", strDate);
                    ds = objDataWrapper.ExecuteDataSet("Proc_MelEventos_Issuance");
                    //ds.Tables[0].Columns[3].ColumnName = "Batch_Code";
                    //ds.Tables[0].Columns[5].ColumnName = "Event_Code";
                    //ds.Tables[0].Columns[6].ColumnName = "Posting_Date";
                    ds.Tables[0].Columns[17].ColumnName = "Amount";

                    gvSunRecordSearch.Visible = true;
                    gvSunRecordSearch.DataSource = ds;
                    gvSunRecordSearch.DataBind();

                    ds.Dispose();


                }
               //GetMeleventosData(txtCREATE_DATE.Text, txtInceptionEndDate.Text);
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
           

        }

        protected void gvRecordSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Item.Cells[7].Text = Objresources.GetString("ZIP_CODE");
                Label l10 = (Label)e.Row.FindControl("capRECORD_ID");
                l10.Text = Objresources.GetString("capRECORD_ID");
                Label l11 = (Label)e.Row.FindControl("capAPÓLICE");
                l11.Text = Objresources.GetString("capAPÓLICE");
                Label l2 = (Label)e.Row.FindControl("capPAYMENT_METHOD");
                l2.Text = Objresources.GetString("capPAYMENT_METHOD");
                Label l13 = (Label)e.Row.FindControl("capPAYMENT_ID");
                l13.Text = Objresources.GetString("capPAYMENT_ID");
                Label l14 = (Label)e.Row.FindControl("capCARRIER_POLICY_BRANCH_CODE");
                l14.Text = Objresources.GetString("capCARRIER_POLICY_BRANCH_CODE");
                Label l15 = (Label)e.Row.FindControl("capREFUND_AMOUNT");
                l15.Text = Objresources.GetString("capREFUND_AMOUNT");
                Label l16 = (Label)e.Row.FindControl("capPAYMENT_DESCRIPTION");
                l16.Text = Objresources.GetString("capPAYMENT_DESCRIPTION");
                Label l17 = (Label)e.Row.FindControl("capFILE_NAMES");
                l17.Text = Objresources.GetString("capFILE_NAMES");
                Label l18 = (Label)e.Row.FindControl("capENDORSEMENT_NUMBER");
                l18.Text = Objresources.GetString("capENDORSEMENT_NUMBER");
                Label l19 = (Label)e.Row.FindControl("capINSTALLMENT_NUMBER");
                l19.Text = Objresources.GetString("capINSTALLMENT_NUMBER");
                Label l20 = (Label)e.Row.FindControl("capCLAIM_NUMBER");
                l20.Text = Objresources.GetString("capCLAIM_NUMBER");
                Label l21 = (Label)e.Row.FindControl("capBENEFICIARY_NAME");
                l21.Text = Objresources.GetString("capBENEFICIARY_NAME");
                Label l22 = (Label)e.Row.FindControl("capPAYEE_ID_CPF_CNPJ");
                l22.Text = Objresources.GetString("capPAYEE_ID_CPF_CNPJ");
                Label l23 = (Label)e.Row.FindControl("capRETURN_STATUS");
                l23.Text = Objresources.GetString("capRETURN_STATUS");
            }

        }


        protected void dgrShowRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Item.Cells[7].Text = Objresources.GetString("ZIP_CODE");
                Label l24 = (Label)e.Row.FindControl("capRECORD_ID");
                l24.Text = Objresources.GetString("capRECORD_ID");
                Label l25 = (Label)e.Row.FindControl("capAPÓLICE");
                l25.Text = Objresources.GetString("capAPÓLICE");
                Label l26 = (Label)e.Row.FindControl("capPAYMENT_METHOD");
                l26.Text = Objresources.GetString("capPAYMENT_METHOD");
                Label l27 = (Label)e.Row.FindControl("capPAYMENT_ID");
                l27.Text = Objresources.GetString("capPAYMENT_ID");
                Label l28 = (Label)e.Row.FindControl("capCARRIER_POLICY_BRANCH_CODE");
                l28.Text = Objresources.GetString("capCARRIER_POLICY_BRANCH_CODE");
                Label l29 = (Label)e.Row.FindControl("capREFUND_AMOUNT");
                l29.Text = Objresources.GetString("capREFUND_AMOUNT");
                Label l30 = (Label)e.Row.FindControl("capPAYMENT_DESCRIPTION");
                l30.Text = Objresources.GetString("capPAYMENT_DESCRIPTION");
                Label l31 = (Label)e.Row.FindControl("capFILE_NAMES");
                l31.Text = Objresources.GetString("capFILE_NAMES");
                Label l32 = (Label)e.Row.FindControl("capENDORSEMENT_NUMBER");
                l32.Text = Objresources.GetString("capENDORSEMENT_NUMBER");
                Label l33 = (Label)e.Row.FindControl("capINSTALLMENT_NUMBER");
                l33.Text = Objresources.GetString("capINSTALLMENT_NUMBER");
                Label l34 = (Label)e.Row.FindControl("capCLAIM_NUMBER");
                l34.Text = Objresources.GetString("capCLAIM_NUMBER");
                Label l35 = (Label)e.Row.FindControl("capBENEFICIARY_NAME");
                l35.Text = Objresources.GetString("capBENEFICIARY_NAME");
                Label l36 = (Label)e.Row.FindControl("capPAYEE_ID_CPF_CNPJ");
                l36.Text = Objresources.GetString("capPAYEE_ID_CPF_CNPJ");
                Label l37 = (Label)e.Row.FindControl("capRETURN_STATUS");
                l37.Text = Objresources.GetString("capRETURN_STATUS");
            }

        }
        protected void gvSunRecordSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Item.Cells[7].Text = Objresources.GetString("ZIP_CODE");
                Label l38 = (Label)e.Row.FindControl("capBatch_Code");
                l38.Text = Objresources.GetString("capBatch_Code");
                Label l39 = (Label)e.Row.FindControl("capEvent_Code");
                l39.Text = Objresources.GetString("capEvent_Code");
                Label l40 = (Label)e.Row.FindControl("capPosting_Date");
                l40.Text = Objresources.GetString("capPosting_Date");
                Label l41 = (Label)e.Row.FindControl("capAmount");
                l41.Text = Objresources.GetString("capAmount");


            }

        }

       


    }
}
