/******************************************************************************************
<Author					: -  Lalit Chauhan  
<Start Date				: -	 29 -March-2010
<End Date				: -	
<Description			: -  Add New Remuneration
<Review Date			: - 
<Reviewed By			: - 	
 
Modification History
<Modified Date			: 
<Modified By			: 
<Purpose				: 
						
****************************************************************************************** */

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
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Configuration;

using Cms.BusinessLayer.BlApplication;
using Cms.Model.Application;
using Cms.Model.Quote;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BlProcess;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlQuote;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Policy;
using System.Resources;


namespace Cms.Policies.Aspx
{
    public partial class PolicyRemuneration : Cms.Policies.policiesbase
    {
        #region Page controls declaration
        protected System.Web.UI.WebControls.Label capBROKER;

        protected System.Web.UI.WebControls.Label lblManHeader;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvBROKER;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCommission;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCOMMISSION;
        protected System.Web.UI.WebControls.RegularExpressionValidator revBRANCH;
        protected System.Web.UI.WebControls.CustomValidator csvCOMMISSION;
        protected System.Web.UI.WebControls.GridView grdBROKER;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCommission;
        public static string HeaderBroKerName = "Broker Name";
        public static string HeaderCommission = "Commission";
        public int CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID;

        protected System.Web.UI.WebControls.Label capBRANCH;
        protected System.Web.UI.WebControls.TextAlign txtBRANCH;
        protected System.Web.UI.WebControls.Label capCOMMISSION;
        protected System.Web.UI.WebControls.Label capName;
        protected CmsButton btnSave;
        protected CmsButton btnAdd;
        protected CmsButton btnDelete;
        //static DataSet ds = new DataSet();
        static DataSet objDataSet = new DataSet();
        ArrayList ArOld = new ArrayList();
        static int l = 0, rowindex;
        // static DataTable dtDefaultView = new DataTable();
        ResourceManager Objresources;
        public static int flag = 0;
        //static IList ILIST;
        static DataTable Table;
        DataTable dtt;
        DataTable dtCo_Applicant;
        protected string jscriptmsg;
        public static string confirmmessage, alertmsg;
        #endregion
        //Button btnAddMore;
        protected string DeleteAlert;
        //static DataTable dtCommission = null;
        #region Page Load Method
        protected void Page_Load(object sender, EventArgs e)
        {
            //Ajax.Utility.RegisterTypeForAjax(typeof(PolicyRemuneration));
            base.ScreenId = "224_26";
            Objresources = new System.Resources.ResourceManager("Cms.Policies.Aspx.PolicyRemuneration", System.Reflection.Assembly.GetExecutingAssembly());

            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString().Length > 0)
            {
                hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"];
                CUSTOMER_ID = int.Parse(Request.QueryString["CUSTOMER_ID"]);
            }
            else
            {
                CUSTOMER_ID = int.Parse(GetCustomerID());
                hidCUSTOMER_ID.Value = GetCustomerID();
            }
            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString().Length > 0)
            {
                POLICY_ID = int.Parse(Request.QueryString["POLICY_ID"]);
                hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"];
            }

            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString().Length > 0)
            {
                POLICY_VERSION_ID = int.Parse(Request.QueryString["POLICY_VERSION_ID"]);
                hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"];
            }
            jscriptmsg = ClsMessages.GetMessage(ScreenId, "10");

            dtCo_Applicant = GetCO_ApplicantDetails();
            SetCaption();
            if (!IsPostBack)
            {
                //ILIST = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("COMTYP");
                string strLob = GetLOBID() == "" ? "0" : GetLOBID();


                Table = ClsGeneralInformation.GetCommissionType("COM", int.Parse(strLob));
                ClsAgency objAgency = new ClsAgency();
                objDataSet = objAgency.FillAgency();
                Session.Add("AGENCY", objDataSet);
                SetBrokers();
               
                SetMessages();
                string Pol_l_DefaultValues = PolicyLabelCommission();
                if (Pol_l_DefaultValues.Contains("^"))
                {
                    numberFormatInfo.PercentDecimalDigits = 4;
                    string[] pol_values = Pol_l_DefaultValues.Split('^');
                    txtPOLICY_LEVEL_COMMISSION.Text = pol_values[0].ToString() == "" ? Convert.ToDouble("0").ToString("N", numberFormatInfo) : Convert.ToDouble(pol_values[0].ToString()).ToString("N", numberFormatInfo);
                    flag = int.Parse(pol_values[1].ToString());
                }
                getNamedInsured();

                hidSCRIPT_MSG.Value = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "6");//6


                if (strLob != "")
                    hidPRODUCT_TYPE.Value = GetTransaction_Type();//GetProduct_Type(int.Parse(strLob));

                GridBind();
            }


            if (hidRisk_ID.Value != "")
            {
                AddGridrow(int.Parse(hidRisk_ID.Value));
            }
            Response.Write(hidRisk_ID.Value);


            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnDelete.PermissionString = gstrSecurityXML;

            btnAdd.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnAdd.PermissionString = gstrSecurityXML;

            //btnSave.Attributes.Add("onclick", "javascript:return validateBroker();");
        }
        #endregion

        private void SetCaption()
        {
            capPOLICY_LEVEL_COMMISSION.Text = Objresources.GetString("capPOLICY_LEVEL_COMMISSION");
            lblManHeader.Text = Objresources.GetString("lblManHeader");
            confirmmessage = Objresources.GetString("confirmmessage");
            alertmsg = Objresources.GetString("alertmsg");
            lblHeader.Text = Objresources.GetString("lblHeader");
            btnAdd.Text = ClsMessages.GetButtonsText(ScreenId, "btnSelect");
           DeleteAlert =  Objresources.GetString("deleteleaderalert");
            

        } //Set Cation From Resource File For Multilingual

        private void SetBrokers()
        {
            try
            {
                ClsAgency objAgency = new ClsAgency();
                DataSet ds = null;
                ds = objAgency.FillAgency();
                DataTable dtBROKER = new DataTable();
                DataTable dtSALES_AGENT = new DataTable();

                int Broker = (int)enumAgencyType.BROKER_AGENCY;
                int SalesAgent = (int)enumAgencyType.SALES_AGENT;

                if (ds.Tables[0].Select("AGENCY_TYPE =" + Broker.ToString()).Length > 0)
                    dtBROKER = ds.Tables[0].Select("AGENCY_TYPE =" + Broker.ToString()).CopyToDataTable().Copy();
                if (ds.Tables[0].Select("AGENCY_TYPE =" + SalesAgent.ToString()).Length > 0)
                    dtSALES_AGENT = ds.Tables[0].Select("AGENCY_TYPE =" + SalesAgent.ToString()).CopyToDataTable().Copy();


                hidSALES_AGENT.Value = ClsCommon.ConvertDataTableToDDLString(dtSALES_AGENT);
                hidBROKER.Value = ClsCommon.ConvertDataTableToDDLString(dtBROKER);
                dtBROKER.Dispose();
                dtSALES_AGENT.Dispose();
                ds.Dispose();
            }
            catch
            {
                hidSALES_AGENT.Value = "";
                hidBROKER.Value = "";
            }
        }
        private void SetMessages()
        {
            HeaderBroKerName = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "3");
            HeaderCommission = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "4");
        } //Set validator Error Messaqge of  from customizedmessages.xml

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                ArrayList alpremunerationobj = new ArrayList();
                DataTable DtCommission = null;
                int retunvalue = 0;
                double dtotalPercent = 0; //Added by Charles on 7-Apr-10 // updated by sonal to make checkbox checked option on june 15 2010
                if (Session["COMMISSION"] != null)
                    DtCommission = (DataTable)Session["COMMISSION"];


                foreach (GridViewRow rw in grdBROKER.Rows)
                {
                    if (rw.RowType == DataControlRowType.DataRow)
                    {

                        TextBox txtcommision = (TextBox)rw.Cells[6].FindControl("txtCOMMISSION");
                        Label BrokerID = (Label)rw.Cells[2].FindControl("capBROKER_ID");
                        HiddenField HdnBrokerID = (HiddenField)rw.Cells[2].FindControl("HdnBrokerID");
                        HiddenField hdnPRODUCT_RISK_ID = (HiddenField)rw.Cells[8].FindControl("hdnPRODUCT_RISK_ID");
                        HiddenField hidSELECT = (HiddenField)rw.Cells[0].FindControl("hidSELECT");

                        DropDownList cmbCOMMISSION_TYPE = (DropDownList)rw.FindControl("cmbCOMMISSION_TYPE");
                        CheckBox chkSELECT = (CheckBox)rw.FindControl("chkSELECT");




                        if (chkSELECT != null && chkSELECT.Checked == true)
                        {
                            if (txtcommision.Text != "" && cmbCOMMISSION_TYPE.SelectedValue != "" && int.Parse(cmbCOMMISSION_TYPE.SelectedValue) == 43)
                            {
                                dtotalPercent = dtotalPercent + double.Parse(txtcommision.Text, numberFormatInfo);
                            }

                            if (hidSELECT.Value == "1" && hdnPRODUCT_RISK_ID != null && hdnPRODUCT_RISK_ID.Value != "")
                            {
                                ClsPolicyRemunerationInfo objnewPolicyRemunerationInfo = new ClsPolicyRemunerationInfo();
                                //Label lb = (Label)rw.Cells[1].FindControl("capREMUNERATION_ID");
                                HiddenField hidREMUNERATION_ID = (HiddenField)rw.Cells[0].FindControl("hidREMUNERATION_ID");
                                if (hidREMUNERATION_ID.Value != null && hidREMUNERATION_ID.Value != "0" && hidREMUNERATION_ID.Value != "")
                                {
                                    //Update Broker                            
                                    objnewPolicyRemunerationInfo = this.getformvalue(rw, ref objnewPolicyRemunerationInfo);
                                    objnewPolicyRemunerationInfo.REMUNERATION_ID.CurrentValue = int.Parse(hidREMUNERATION_ID.Value);
                                    objnewPolicyRemunerationInfo.LAST_UPDATED_DATETIME.CurrentValue = DateTime.Now;
                                    objnewPolicyRemunerationInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                                    objnewPolicyRemunerationInfo.IS_ACTIVE.CurrentValue = "Y";
                                    objnewPolicyRemunerationInfo.ACTION = "U";

                                    alpremunerationobj.Add(objnewPolicyRemunerationInfo);
                                    //int i = 0;
                                    for (int j = DtCommission.Rows.Count - 1; j >= 0; j--)
                                    {
                                        if (DtCommission.Rows[j]["REMUNERATION_ID"] != null && DtCommission.Rows[j]["REMUNERATION_ID"].ToString() == hidREMUNERATION_ID.Value)
                                        {
                                            DtCommission.Rows.Remove(DtCommission.Rows[j]);
                                            DtCommission.AcceptChanges();
                                        }
                                    }

                                }
                                else
                                {
                                    //Add New Broker
                                    objnewPolicyRemunerationInfo = this.getformvalue(rw, ref objnewPolicyRemunerationInfo);
                                    objnewPolicyRemunerationInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                                    objnewPolicyRemunerationInfo.CREATED_DATETIME.CurrentValue = DateTime.Now;
                                    objnewPolicyRemunerationInfo.IS_ACTIVE.CurrentValue = "Y";
                                    objnewPolicyRemunerationInfo.ACTION = "I";
                                    alpremunerationobj.Add(objnewPolicyRemunerationInfo);

                                }
                            }
                        }
                    }
                }
                //Added by Charles on 7-Apr-10 
                //if (txtPOLICY_LEVEL_COMMISSION.Text.Trim() != "0.00" && (dtotalPercent > 100))//(dtotalPercent < 100 || dtotalPercent > 100))

                foreach (DataRow dr in DtCommission.Rows)
                {

                    if (dr["COMMISSION_PERCENT"] != null && dr["COMMISSION_PERCENT"].ToString() != "")
                    {
                        if (dr["COMMISSION_TYPE"].ToString() == "43")
                            dtotalPercent = dtotalPercent + double.Parse(dr["COMMISSION_PERCENT"].ToString());
                    }
                }

                if (hidPRODUCT_TYPE.Value != MASTER_POLICY)
                {
                    if (txtPOLICY_LEVEL_COMMISSION.Text != Convert.ToDouble("0").ToString("N", numberFormatInfo))
                    {
                        if (dtotalPercent < 100 || dtotalPercent > 100)
                        {
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "7");
                            //if (Session["COMMISSION"] != null) { dtCommission = (DataTable)Session["COMMISSION"]; }
                            hidReject.Value = "0";
                            hidAction.Value = "REJECT";
                            MaintainLastRow(grdBROKER);
                            PlaceAddMoreButton();
                            GridBind();
                            hidAction.Value = "";
                        }
                        else
                        {
                            retunvalue = objGeneralInformation.AddUpdateBroker(alpremunerationobj);
                        }
                    }
                    else
                    {
                        if (dtotalPercent > 100)
                        {
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "12");
                            //if (Session["COMMISSION"] != null) { dtCommission = (DataTable)Session["COMMISSION"]; }
                            hidReject.Value = "0";
                            hidAction.Value = "REJECT";
                            MaintainLastRow(grdBROKER);
                            PlaceAddMoreButton();
                            GridBind();
                            hidAction.Value = "";

                        }
                        else
                        {
                            retunvalue = objGeneralInformation.AddUpdateBroker(alpremunerationobj);
                        }
                    }

                    //if (txtPOLICY_LEVEL_COMMISSION.Text != Convert.ToDouble("0").ToString("N", numberFormatInfo))
                    //{
                    //    if (dtotalPercent < 100 || dtotalPercent > 100)
                    //    {
                    //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "7");
                    //        //GridBind();
                    //        PlaceAddMoreButton();
                    //        if (Session["COMMISSION"] != null) { dtCommission = (DataTable)Session["COMMISSION"]; }
                    //        hidReject.Value = "1";
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                    //    if (dtotalPercent > 100)
                    //    {
                    //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "12");
                    //        //GridBind();
                    //        PlaceAddMoreButton();
                    //        if (Session["COMMISSION"] != null) { dtCommission = (DataTable)Session["COMMISSION"]; }
                    //        hidReject.Value = "1";
                    //        return;
                    //    }
                    //}
                }
                else
                {
                    retunvalue = objGeneralInformation.AddUpdateBroker(alpremunerationobj);
                    hidReject.Value = "0";
                }
                if (retunvalue > 0)
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "29");
                    GridBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                AddGridrow();  //Add New blank row in Grid View
            }
            catch
            {
                //lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("334") + ":-" + ex.Message;// GetMessage(ScreenId, "7");//334
            }

        }  //Add Button click 

        #region Delete Broker Grid

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            MaintainLastRow(grdBROKER);
            ArOld.Clear();
            int retunvalue = 0;
            string checkdefault = string.Empty;
            string checkleader = string.Empty;
            ClsPolicyRemunerationInfo objClsPolicyRemunerationInfo = new ClsPolicyRemunerationInfo();
            ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
            DataSet ds = null;
            DataTable dtCommission = null;
            if (Session["REMUNERATION"] != null)
                ds = (DataSet)Session["REMUNERATION"];
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (GridViewRow row in grdBROKER.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {

                            CheckBox chkbox = (CheckBox)row.Cells[0].FindControl("chkSELECT");


                            if (chkbox.Checked)
                            {
                                //CheckBox chkleader = (CheckBox)row.Cells[8].FindControl("chkLEADER");
                                //if (chkleader.Checked != true)
                                //{
                                hidAction.Value = "DELETE";
                                checkdefault = "N";
                                checkleader = "N";
                                Label lb = (Label)row.Cells[1].FindControl("capREMUNERATION_ID");
                                Label BrokerID = (Label)row.Cells[2].FindControl("capBROKER_ID");
                                DropDownList cmbCOMMISSION_TYPE = (DropDownList)row.Cells[3].FindControl("cmbCOMMISSION_TYPE");
                                DropDownList cmbCO_APPLICANT_ID = (DropDownList)row.Cells[3].FindControl("cmbCO_APPLICANT_ID");
                                if (BrokerID != null)
                                {
                                    if (BrokerID.Text != "")
                                    {
                                        //Added By Lalit for master policy if 
                                        //primary co-applicant with default broker can not be deleted
                                        if (hidPRODUCT_TYPE.Value == MASTER_POLICY)
                                        {
                                            if (Convert.ToInt32(BrokerID.Text) == flag && cmbCOMMISSION_TYPE.SelectedValue == "43" && IS_PrimartAppicant(int.Parse(cmbCO_APPLICANT_ID.SelectedValue.ToString())) == true)
                                            {
                                                checkdefault = "Y";
                                            }

                                        }
                                        else if (Convert.ToInt32(BrokerID.Text) == flag && cmbCOMMISSION_TYPE.SelectedValue == "43")
                                        {

                                            checkdefault = "Y";
                                        }
                                    }
                                }

                                if (lb.Text != null && lb.Text != "0" && lb.Text != "" && checkdefault != "Y")
                                {
                                    objClsPolicyRemunerationInfo.REMUNERATION_ID.CurrentValue = int.Parse(lb.Text);
                                    objClsPolicyRemunerationInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                                    objClsPolicyRemunerationInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                                    objClsPolicyRemunerationInfo.POLICY_ID.CurrentValue = POLICY_ID;
                                    objClsPolicyRemunerationInfo.POLICY_VERSION_ID.CurrentValue = POLICY_VERSION_ID;
                                    retunvalue = objGeneralInformation.DeleteBroker(objClsPolicyRemunerationInfo);
                                }
                                if (checkdefault == "N")
                                {
                                    int rowindex = row.RowIndex;
                                    ds.Tables[0].Rows[rowindex].BeginEdit();
                                    ds.Tables[0].Rows[rowindex]["STATUS"] = "N";
                                    ds.Tables[0].Rows[rowindex].EndEdit();
                                    ds.AcceptChanges();
                                    retunvalue = 1;
                                    string remunerationID = ds.Tables[0].Rows[rowindex]["REMUNERATION_ID"].ToString();
                                    string BrokerId = ds.Tables[0].Rows[rowindex]["BROKER_ID"].ToString();
                                    dtCommission = (DataTable)Session["COMMISSION"];
                                    if (dtCommission.Rows.Count > 0)
                                    {
                                        int i = 0;
                                        for (i = dtCommission.Rows.Count - 1; i >= 0; i--)//i = 0; i < dtCommission.Rows.Count; i++)
                                        {
                                            if (dtCommission.Rows[i]["REMUNERATION_ID"].ToString() == remunerationID && dtCommission.Rows[i]["BROKER_ID"].ToString() == BrokerId)
                                            {
                                                dtCommission.Rows[i].Delete();
                                                dtCommission.AcceptChanges();
                                            }
                                        }
                                    }

                                }

                                if (retunvalue > 0)
                                {
                                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("5");
                                    base.OpenEndorsementDetails();
                                }
                                else
                                {
                                    lblMessage.Text = "";
                                }
                                //}
                                //else
                                //{
                                //    lblMessage.Text = Objresources.GetString("deleteleaderalert"); ;
                                //}
                            }
                        }
                        else
                        {
                            TextBox Commission_Percent = (TextBox)row.Cells[4].FindControl("txtCOMMISSION");
                            ArOld.Add(Commission_Percent.Text);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("334") + ":-" + ex.Message;// GetMessage(ScreenId, "7");//334

                }
            }
            Session["COMMISSION"] = dtCommission;
            Session["REMUNERATION"] = ds;
            GridBind();
            hidAction.Value = "";
        } //Delete Grid row from gridview        

        #endregion

        private void GridBind()
        {
            DataSet ds = null; //= new DataSet();
            DataView dv = null;
            if (hidAction.Value != "DELETE" && hidAction.Value != "ADDNEW" && hidAction.Value != "REJECT")
            {
                DataTable dtCommission = null;
                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                ds = objGeneralInformation.GetBrokerList(int.Parse(Request.QueryString["CUSTOMER_ID"]), int.Parse(Request.QueryString["POLICY_ID"]), int.Parse(Request.QueryString["POLICY_VERSION_ID"]));
                dtCommission = ds.Tables[0].Copy();
                if (Session["COMMISSION"] != null && Session["REMUNERATION"] != null)
                {
                    Session["COMMISSION"] = dtCommission;
                    Session["REMUNERATION"] = ds;
                }
                else
                {
                    Session.Add("COMMISSION", dtCommission);
                    Session.Add("REMUNERATION", ds);
                }
            }
            if (Session["REMUNERATION"] != null)
            {
                ds = (DataSet)Session["REMUNERATION"];
            }
            if (ds != null)
                dv = new DataView(ds.Tables[0], "STATUS='Y'", "", DataViewRowState.CurrentRows);


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                for (i = ds.Tables[0].Rows.Count - 1; i >= 0; i--)
                {
                    if (ds.Tables[0].Rows[i]["STATUS"].ToString() == "N")
                    {
                        ds.Tables[0].Rows[i].Delete();
                        ds.AcceptChanges();
                    }
                }
            }

            //grdBROKER.DataSource = dv;
            grdBROKER.DataSource = AddEmptyRow(dv);
            grdBROKER.DataBind();
            PlaceAddMoreButton();
            hidROW_COUNT.Value = grdBROKER.Rows.Count.ToString();

            if (hidPRODUCT_TYPE.Value != MASTER_POLICY)//for master policy ,if not master policy no need to display co-applicant names
            {
                grdBROKER.Columns[3].Visible = false;
            }
        }  //Bind Grid after delete or fetch data from database;

        private void PlaceAddMoreButton()
        {
            Table table = (Table)this.grdBROKER.Controls[0];

            string lastState = "-1";
            string currentState = string.Empty;
            int realIndex = 0;
            foreach (GridViewRow row in grdBROKER.Rows)
            {

                Label lblPRODUCT_RISK_ID = (Label)row.Cells[9].FindControl("lblPRODUCT_RISK_ID");
                if (lblPRODUCT_RISK_ID != null)
                {
                    currentState = lblPRODUCT_RISK_ID.Text;// Convert.ToString(this.grdBROKER.DataKeys[1].Value);
                    realIndex = table.Rows.GetRowIndex(row);
                }
                else
                {
                    // realIndex = table.Rows.GetRowIndex(row);
                }
                if (lastState == "-1" || row.RowIndex == 1)
                    lastState = currentState;

                if (currentState != lastState)
                {


                    table.Controls.AddAt(realIndex - 1, CreateNewRow(realIndex - 1, this.grdBROKER.Columns.Count, lastState, lastState));

                    lastState = currentState;
                }
            }

            table.Controls.AddAt(table.Rows.Count - 1, CreateNewRow(table.Rows.Count - 1, this.grdBROKER.Columns.Count, lastState, currentState));
        }

        private GridViewRow CreateNewRow(int Index, int ItemCount, string CurrentState, string Riskid)
        {

            GridViewRow groupHeaderRow = new GridViewRow(Index, Index, DataControlRowType.Separator, DataControlRowState.Normal);

            TableCell newCell = new TableCell();

            newCell.ColumnSpan = ItemCount;

            Button btnAddMore1 = new Button();
            btnAddMore1.CssClass = "clsButton";
            btnAddMore1.ID = "btnAddMore" + Riskid;
            btnAddMore1.Text = "Add";
            btnAddMore1.CausesValidation = false;
            btnAddMore1.CommandArgument = Riskid;
            btnAddMore1.CommandName = Index.ToString();
            btnAddMore1.Click += new EventHandler(btnAddMore_Click);
            btnAddMore1.Attributes.Add("onclick", "addnew('" + Riskid + "');");

            newCell.Controls.Add(btnAddMore1);
            newCell.BackColor = System.Drawing.Color.FromArgb(233, 229, 229);

            groupHeaderRow.Cells.Add(newCell);
            return groupHeaderRow;
        }

        //Added By Lalit jan 03,2011
        private void MaintainLastRow(GridView Gv)
        {
            DataSet ds = null;

            if (Session["REMUNERATION"] != null)
                ds = (DataSet)Session["REMUNERATION"];

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (GridViewRow rw in Gv.Rows)
                {
                    int Branch;
                    double Commission;
                    if (rw.RowType == DataControlRowType.DataRow)
                    {
                        HiddenField hidCO_APPLICANT_ID = (HiddenField)rw.FindControl("hidCO_APPLICANT_ID");
                        HiddenField hidCOMMISSION_TYPE = (HiddenField)rw.FindControl("hidCOMMISSION_TYPE");
                        DropDownList cmbCO_APPLICANT_ID = (DropDownList)rw.FindControl("cmbCO_APPLICANT_ID");
                        HiddenField HdnBrokerID = (HiddenField)rw.FindControl("HdnBrokerID");
                        TextBox BRANCH = (TextBox)rw.FindControl("txtBRANCH");
                        TextBox commission = (TextBox)rw.FindControl("txtCOMMISSION");
                        CheckBox chkLEADER = (CheckBox)rw.FindControl("chkLEADER");
                        CheckBox chkSELECT = (CheckBox)rw.FindControl("chkSELECT");

                        int RowIndex = rw.RowIndex;
                        if (ds.Tables[0].Rows[RowIndex] != null)
                        {
                            ds.Tables[0].Rows[RowIndex].BeginEdit();
                            if (HdnBrokerID.Value != "")
                                ds.Tables[0].Rows[RowIndex]["BROKER_ID"] = int.Parse(HdnBrokerID.Value);
                            if (hidCOMMISSION_TYPE.Value != "")
                                ds.Tables[0].Rows[RowIndex]["COMMISSION_TYPE"] = double.Parse(hidCOMMISSION_TYPE.Value);
                            if (BRANCH.Text != "" && int.TryParse(BRANCH.Text, out Branch))
                                ds.Tables[0].Rows[RowIndex]["BRANCH"] = Branch; //int.Parse(Branch.Text);

                            if (commission.Text != "" && double.TryParse(commission.Text, out Commission))
                            {
                                // CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                                // NumberFormatInfo numberForinfo = cultureInfo.NumberFormat;
                                //Type tt = ds.Tables[0].Columns[7].DataType;
                                //double cultureCommission =
                                ds.Tables[0].Rows[RowIndex]["COMMISSION_PERCENT"] = double.Parse(commission.Text, numberFormatInfo);// Convert.ToDouble(commission.Text, numberForinfo);//.Parse()
                                //ds.Tables[0].Rows[RowIndex]["COMMISSION_PERCENT"] = commission.Text;

                            }
                            if (hidPRODUCT_TYPE.Value != MASTER_POLICY)
                            {

                                ds.Tables[0].Rows[RowIndex]["CO_APPLICANT_ID"]
                                    = ClsGeneralInformation.GetPolicyPrimary_Applicant(int.Parse(hidCUSTOMER_ID.Value),
                                    int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));

                            }
                            else
                                if (cmbCO_APPLICANT_ID.SelectedValue != "")
                                    ds.Tables[0].Rows[RowIndex]["CO_APPLICANT_ID"] = int.Parse(cmbCO_APPLICANT_ID.SelectedValue);


                            if (chkLEADER.Checked)
                                ds.Tables[0].Rows[RowIndex]["LEADER"] = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
                            else
                                ds.Tables[0].Rows[RowIndex]["LEADER"] = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);


                            ds.Tables[0].Rows[RowIndex].EndEdit();
                            ds.AcceptChanges();





                        }

                    }

                }
                Session["REMUNERATION"] = ds;
            }
        }

        void btnAddMore_Click(object sender, EventArgs e)
        {
            int btnIndex = Convert.ToInt32(((Button)sender).ID.Substring(10, 1));
            AddGridrow(btnIndex);
            //hidRisk_ID.Value = btnIndex.ToString();
        }

        private void AddGridrow()
        {
            DataSet ds = null;
            if (Session["REMUNERATION"] != null)
                ds = (DataSet)Session["REMUNERATION"];



            DataRow dr = ds.Tables[0].NewRow();
            dr["REMUNERATION_ID"] = DBNull.Value;
            dr["BROKER_ID"] = DBNull.Value;
            dr["BROKER_NAME"] = DBNull.Value;
            dr["BRANCH"] = DBNull.Value;
            dr["STATUS"] = "Y";
            dr["LEADER"] = DBNull.Value;
            dr["COMMISSION_TYPE"] = DBNull.Value;
            dr["AMOUNT"] = DBNull.Value;
            dr["NAME"] = DBNull.Value;

            if (Convert.ToDouble(txtPOLICY_LEVEL_COMMISSION.Text) != Convert.ToDouble("0"))
            {
                dr["COMMISSION_PERCENT"] = DBNull.Value;
            }
            else
            {
                dr["COMMISSION_PERCENT"] = DBNull.Value;
            }
            ds.Tables[0].Rows.Add(dr);
            rowindex = ds.Tables[0].Rows.IndexOf(dr);
            hidAction.Value = "ADDNEW";
            Session["REMUNERATION"] = ds;
            GridBind();

            hidAction.Value = "";
        } // Add new grid view  when click on  add button

        public void AddGridrow(int RISK_ID)
        {
            MaintainLastRow(grdBROKER);
            DataSet ds = null;
            if (Session["REMUNERATION"] != null)
                ds = (DataSet)Session["REMUNERATION"];


            DataRow dr = ds.Tables[0].NewRow();
            dr["PRODUCT_RISK_ID"] = RISK_ID;
            dr["REMUNERATION_ID"] = DBNull.Value;
            dr["BROKER_ID"] = DBNull.Value;
            dr["BROKER_NAME"] = DBNull.Value;
            dr["BRANCH"] = DBNull.Value;
            dr["STATUS"] = "Y";
            dr["LEADER"] = DBNull.Value;
            dr["COMMISSION_TYPE"] = DBNull.Value;
            dr["AMOUNT"] = DBNull.Value;
            dr["NAME"] = DBNull.Value;
            if (Convert.ToDouble(txtPOLICY_LEVEL_COMMISSION.Text) != Convert.ToDouble("0"))
            {
                dr["COMMISSION_PERCENT"] = DBNull.Value;
            }
            else
            {
                dr["COMMISSION_PERCENT"] = DBNull.Value;
            }
            ds.Tables[0].Rows.Add(dr);
            rowindex = ds.Tables[0].Rows.IndexOf(dr);
            hidAction.Value = "ADDNEW";
            Session["REMUNERATION"] = ds;
            GridBind();
            hidAction.Value = "";
            hidRisk_ID.Value = "";

        }

        private ClsPolicyRemunerationInfo getformvalue(GridViewRow rw, ref ClsPolicyRemunerationInfo objPolicyRemunerationInfo)
        {
            //int intCommission = (int)enumCommissionType.COMMISSION;
            //int intEnrollmentFees = (int)enumCommissionType.ENROLLMENT_FEE;
            //int intPro_Labore = (int)enumCommissionType.PRO_LABORE;
            //int BROKER_AGENCY = (int)enumAgencyType.BROKER_AGENCY;
            //int SALES_AGENT = (int)enumAgencyType.SALES_AGENT;

            objPolicyRemunerationInfo.CUSTOMER_ID.CurrentValue = CUSTOMER_ID;
            objPolicyRemunerationInfo.POLICY_ID.CurrentValue = POLICY_ID;
            objPolicyRemunerationInfo.POLICY_VERSION_ID.CurrentValue = POLICY_VERSION_ID;
            TextBox commission = (TextBox)rw.FindControl("txtCOMMISSION");
            if (commission != null)
                if (commission.Text != string.Empty)
                    objPolicyRemunerationInfo.COMMISSION_PERCENT.CurrentValue = double.Parse(commission.Text, numberFormatInfo);
                else
                    objPolicyRemunerationInfo.COMMISSION_PERCENT.CurrentValue = 0;


            TextBox BRANCH = (TextBox)rw.FindControl("txtBRANCH");
            if (BRANCH != null)
                if (BRANCH.Text != string.Empty && BRANCH.Text != null)
                    objPolicyRemunerationInfo.BRANCH.CurrentValue = int.Parse(BRANCH.Text);
                else
                    objPolicyRemunerationInfo.BRANCH.CurrentValue = GetEbixIntDefaultValue();


            TextBox txtAMOUNT = (TextBox)rw.FindControl("txtAMOUNT");
            if (txtAMOUNT != null && txtAMOUNT.Text != "")
                objPolicyRemunerationInfo.AMOUNT.CurrentValue = double.Parse(txtAMOUNT.Text, numberFormatInfo);
            else
                objPolicyRemunerationInfo.AMOUNT.CurrentValue = GetEbixDoubleDefaultValue();

            CheckBox chkLEADER = (CheckBox)rw.FindControl("chkLEADER");
            if (chkLEADER.Checked)
            {
                objPolicyRemunerationInfo.LEADER.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
            }
            else
            {
                objPolicyRemunerationInfo.LEADER.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);
            }


            DropDownList cmbCOMMISSION_TYPE = (DropDownList)rw.FindControl("cmbCOMMISSION_TYPE");
            HiddenField hidCOMMISSION_TYPE = (HiddenField)rw.Cells[3].FindControl("hidCOMMISSION_TYPE");

            //if (cmbCOMMISSION_TYPE != null && cmbCOMMISSION_TYPE.SelectedValue != "")
            //    objPolicyRemunerationInfo.COMMISSION_TYPE.CurrentValue = int.Parse(cmbCOMMISSION_TYPE.SelectedValue);

            if (hidCOMMISSION_TYPE.Value != null && hidCOMMISSION_TYPE.Value != "")
                objPolicyRemunerationInfo.COMMISSION_TYPE.CurrentValue = int.Parse(hidCOMMISSION_TYPE.Value);
            else
                objPolicyRemunerationInfo.COMMISSION_TYPE.CurrentValue = GetEbixIntDefaultValue();

            HiddenField HdnBrokerID = (HiddenField)rw.Cells[2].FindControl("HdnBrokerID");
            DropDownList cmbName = (DropDownList)rw.FindControl("cmbName");
            if (HdnBrokerID != null && HdnBrokerID.Value != "")
            {
                objPolicyRemunerationInfo.BROKER_ID.CurrentValue = int.Parse(HdnBrokerID.Value);

            }
            else
            {
                objPolicyRemunerationInfo.BROKER_ID.CurrentValue = GetEbixIntDefaultValue();

            }
            TextBox txtNAME = (TextBox)rw.FindControl("txtNAME");

            if (cmbCOMMISSION_TYPE.SelectedValue.Trim() == "")//  	 old value of Pro-Labore 14631 
                objPolicyRemunerationInfo.NAME.CurrentValue = cmbName.SelectedItem.Text;


            //Label lblPRODUCT_RISK_ID = (Label)rw.FindControl("lblPRODUCT_RISK_ID");
            HiddenField lblPRODUCT_RISK_ID = (HiddenField)rw.Cells[8].FindControl("hdnPRODUCT_RISK_ID");
            if (lblPRODUCT_RISK_ID != null && lblPRODUCT_RISK_ID.Value != "")
            {

                objPolicyRemunerationInfo.PRODUCT_RISK_ID.CurrentValue = int.Parse(lblPRODUCT_RISK_ID.Value);
            }
            else
            {
                objPolicyRemunerationInfo.PRODUCT_RISK_ID.CurrentValue = GetEbixIntDefaultValue();
            }


            //add Co-Applicant
            if (hidPRODUCT_TYPE.Value != MASTER_POLICY)
            {
                objPolicyRemunerationInfo.CO_APPLICANT_ID.CurrentValue = ClsGeneralInformation.GetPolicyPrimary_Applicant(objPolicyRemunerationInfo.CUSTOMER_ID.CurrentValue, objPolicyRemunerationInfo.POLICY_ID.CurrentValue, objPolicyRemunerationInfo.POLICY_VERSION_ID.CurrentValue);

            }
            else
            {
                DropDownList cmbCO_APPLICANT_ID = (DropDownList)rw.Cells[3].FindControl("cmbCO_APPLICANT_ID");
                if (cmbCO_APPLICANT_ID.SelectedValue != "")
                {
                    objPolicyRemunerationInfo.CO_APPLICANT_ID.CurrentValue = int.Parse(cmbCO_APPLICANT_ID.SelectedValue);
                }
                else
                {
                    objPolicyRemunerationInfo.CO_APPLICANT_ID.CurrentValue = 0;
                }
            }

            return objPolicyRemunerationInfo;

        } //get all user Input value from UI .

        protected void grdBROKER_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                Label capBRANCH = (Label)e.Row.FindControl("capBRANCH");
                capBRANCH.Text = Objresources.GetString("capBRANCH");
                Label capCOMMISSION = (Label)e.Row.FindControl("capCOMMISSION");
                capCOMMISSION.Text = Objresources.GetString("capCOMMISSION");
                Label capName = (Label)e.Row.FindControl("capName");
                capName.Text = Objresources.GetString("capName");
                Label capCOMMISSION_TYPE = (Label)e.Row.FindControl("capCOMMISSION_TYPE");
                capCOMMISSION_TYPE.Text = Objresources.GetString("capCOMMISSION_TYPE");
                Label capAMOUNT = (Label)e.Row.FindControl("capAMOUNT");
                capAMOUNT.Text = Objresources.GetString("capAMOUNT");
                Label capLEADER = (Label)e.Row.FindControl("capLEADER");
                capLEADER.Text = Objresources.GetString("capLEADER");
                Label capCO_APPLICANT_ID = (Label)e.Row.FindControl("capCO_APPLICANT_ID");
                capCO_APPLICANT_ID.Text = Objresources.GetString("cmbCO_APPLICANT_ID");
                System.Web.UI.HtmlControls.HtmlInputCheckBox chkALLSELECT = (System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Row.FindControl("chkALLSELECT");
                chkALLSELECT.Attributes.Add("onclick", "javascript:SelectAll(this)");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int flagg = 0;
                Label lblRISK_ID = (Label)e.Row.FindControl("lblPRODUCT_RISK_ID");
                Label lblRISK_NAME = (Label)e.Row.FindControl("lblRISK_NAME");
                string RISK_NAME = lblRISK_NAME.Text;
                rfvCommission = (RequiredFieldValidator)e.Row.FindControl("rfvCOMMISSION");

                rfvCommission.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2");
                revCOMMISSION = (RegularExpressionValidator)e.Row.FindControl("revCOMMISSION");
                revCOMMISSION.ValidationExpression = aRegExpDoublePositiveWithZeroFourDeci;
                revCOMMISSION.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
                revBRANCH = (RegularExpressionValidator)e.Row.FindControl("revBRANCH");
                revBRANCH.ValidationExpression = aRegExpInteger;//aRegExpAlphaNumStrict
                revBRANCH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                revCOMMISSION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "5");
                csvCOMMISSION = (CustomValidator)e.Row.FindControl("csvCOMMISSION");
                csvCOMMISSION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "5");
                RequiredFieldValidator revCOMMISSION_TYPE = (RequiredFieldValidator)e.Row.FindControl("rfvCOMMISSION_TYPE");
                revCOMMISSION_TYPE.IsValid = true;
                revCOMMISSION_TYPE.Enabled = false;
                revCOMMISSION_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "8");//"please select commission Type
                RequiredFieldValidator rfvBROKER_NAME = (RequiredFieldValidator)e.Row.FindControl("rfvNAME");

                rfvBROKER_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1");

                RequiredFieldValidator rfvAMOUNT = (RequiredFieldValidator)e.Row.FindControl("rfvAMOUNT");
                RegularExpressionValidator revAMOUNT = (RegularExpressionValidator)e.Row.FindControl("revAMOUNT");
                rfvAMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("335");//216//"Please enter amount";

                revAMOUNT.ValidationExpression = aRegExpCurrencyformat;
                revAMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
                CustomValidator csvAMOUNT = (CustomValidator)e.Row.FindControl("csvAMOUNT");

                //Fill Co-Applicant

                RequiredFieldValidator rfvCO_APPLICANT_ID = (RequiredFieldValidator)e.Row.FindControl("rfvCO_APPLICANT_ID");
                DropDownList cmbCO_APPLICANT_ID = (DropDownList)e.Row.FindControl("cmbCO_APPLICANT_ID");
                HiddenField hidCO_APPLICANT_ID = (HiddenField)e.Row.FindControl("hidCO_APPLICANT_ID");

                rfvCO_APPLICANT_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "11");

                //if (hidPRODUCT_TYPE.Value == "MASTER_POLICY")   //
                //{


                this.FillCo_Applicants(cmbCO_APPLICANT_ID, hidCO_APPLICANT_ID.Value);



                if (hidPRODUCT_TYPE.Value != MASTER_POLICY)
                {
                    rfvCO_APPLICANT_ID.Enabled = false;
                    rfvCO_APPLICANT_ID.Attributes.Add("style", "display:none");
                }


                if (dtt.Rows[e.Row.RowIndex]["Status"].ToString() == "Group")
                {
                    //lblRISK_ID.Text = dv.Table.Rows[e.Row.RowIndex]["PRODUCT_RISK_ID"].ToString();
                    if (e.Row.Cells.Count > 0)
                    {
                        for (int i = 0; i < e.Row.Cells.Count; i++)
                            e.Row.Cells[i].Controls.Clear();
                    }

                    Label ll = new Label();
                    ll.ID = "lblRISK_LABEL";
                    ll.Text = RISK_NAME;
                    //TableCell tc = new TableCell();
                    //tc.Controls.Add(ll);
                    e.Row.Cells[4].Controls.Add(ll);
                    flagg = 1;
                }
                if (flagg != 1)
                {

                    Label BrokerID = (Label)e.Row.FindControl("capBROKER_ID");
                    DropDownList cmbName = (DropDownList)e.Row.FindControl("cmbName");
                    cmbName.Attributes.Add("onchange", "javascript:setBrokerId(this);");

                    Label COMMISSION_TYPE = (Label)e.Row.FindControl("COMMISSION_TYPE");
                    Label lblLEADER = (Label)e.Row.FindControl("lblLEADER");
                    TextBox commission = (TextBox)e.Row.FindControl("txtCOMMISSION");

                    //commission.Text = Convert.ToDouble(commission.Text, numberFormatInfo).ToString();


                    if (commission != null)
                        commission.Attributes.Add("onblur", "javascript:this.value=formatRate(this.value)");


                    DropDownList cmbCOMMISSION_TYPE = (DropDownList)e.Row.FindControl("cmbCOMMISSION_TYPE");

                    TextBox txtNAME = (TextBox)e.Row.FindControl("txtNAME");
                    CheckBox chkLEADER = (CheckBox)e.Row.Cells[4].FindControl("chkLEADER");
                    CheckBox chkSELECT = (CheckBox)e.Row.FindControl("chkSELECT");
                    TextBox txtAMOUNT = (TextBox)e.Row.FindControl("txtAMOUNT");
                    HiddenField hdnPRODUCT_RISK_ID = (HiddenField)e.Row.FindControl("hdnPRODUCT_RISK_ID");
                    HiddenField hidSELECT = (HiddenField)e.Row.FindControl("hidSELECT");
                    HiddenField hidCOMMISSION_TYPE = (HiddenField)e.Row.FindControl("hidCOMMISSION_TYPE");
                    hidCOMMISSION_TYPE.Value = COMMISSION_TYPE.Text;
                    if (BrokerID != null && BrokerID.Text != "")
                    {
                        if (Convert.ToInt32(BrokerID.Text) == flag && int.Parse(COMMISSION_TYPE.Text) == 43)//if (Convert.ToInt32(BrokerID.Text) == flag && int.Parse(ds.Tables[0].Rows[e.Row.RowIndex]["COMMISSION_TYPE"].ToString()) == 43)
                        {
                            if (hidPRODUCT_TYPE.Value == MASTER_POLICY)
                            {
                                if (hidCO_APPLICANT_ID.Value != "")
                                {
                                    if (IS_PrimartAppicant(int.Parse(hidCO_APPLICANT_ID.Value)))
                                    {
                                        chkSELECT.Enabled = false;
                                        chkSELECT.Checked = true;// added by sonal to save check box checked rows if user want to save only first default row
                                        hidSELECT.Value = "1";
                                        cmbCOMMISSION_TYPE.Enabled = false;
                                        cmbName.Enabled = false;
                                        chkLEADER.Checked = true;
                                    }
                                }
                            }
                            else
                            {
                                chkSELECT.Enabled = false;
                                chkSELECT.Checked = true;// added by sonal to save check box checked rows if user want to save only first default row
                                hidSELECT.Value = "1";
                                cmbCOMMISSION_TYPE.Enabled = false;
                                cmbName.Enabled = false;
                                chkLEADER.Checked = true;
                            }
                        }
                    }

                    chkLEADER.Attributes.Add("onclick", "CheckLeader(this,'" + lblRISK_ID.Text + "')");
                    txtNAME.ReadOnly = true;
                    cmbCOMMISSION_TYPE.Attributes.Add("onchange", "javascript:onChangeType('" + cmbCOMMISSION_TYPE.ClientID.ToString() + "','" + cmbName.ClientID.ToString() + "','" + txtNAME.ClientID.ToString() + "');BindBrokerName(this);");
                    chkSELECT.Attributes.Add("onclick", "javascript:onCheckedChange(this.id)");
                    chkLEADER.Attributes.Add("onclick", "javascript:CheckLeader(this,'" + hdnPRODUCT_RISK_ID.Value + "')");
                    if (cmbCOMMISSION_TYPE != null)
                    {
                        if (COMMISSION_TYPE.Text != "")//if (ds.Tables[0].Rows[e.Row.RowIndex]["COMMISSION_TYPE"].ToString() != "")
                            Bind_GridCommissionType(cmbCOMMISSION_TYPE, int.Parse(COMMISSION_TYPE.Text));//Bind_GridCommissionType(cmbCOMMISSION_TYPE, int.Parse(ds.Tables[0].Rows[e.Row.RowIndex]["COMMISSION_TYPE"].ToString()));
                        else
                            Bind_GridCommissionType(cmbCOMMISSION_TYPE, 0);

                        //cmbCOMMISSION_TYPE.Attributes.Add("onchange", "javascript:BindBrokerName(this)");

                        //if (COMMISSION_TYPE.Text == "45")//if (ds.Tables[0].Rows[e.Row.RowIndex]["COMMISSION_TYPE"].ToString() == "45") //For Pro-Labour Commssion Type   14631
                        //{
                        //    txtNAME.Attributes.Add("style", "display:inline");
                        //    cmbName.Attributes.Add("style", "display:none");
                        //    rfvBROKER_NAME.Attributes.Add("enabled", "false");
                        //    rfvBROKER_NAME.Attributes.Add("isValid", "true");
                        //    rfvBROKER_NAME.Attributes.Add("style", "display:none");

                        //}
                        //else
                        //{
                        txtNAME.Attributes.Add("style", "display:none");
                        cmbName.Attributes.Add("style", "display:inline");


                    }

                    if (cmbName != null)
                    {
                        if (BrokerID.Text != "")//if (ds.Tables[0].Rows[e.Row.RowIndex]["BROKER_ID"].ToString() != "")
                            Bind_GridBrokerName(cmbName, int.Parse(BrokerID.Text), COMMISSION_TYPE.Text);//Bind_GridBrokerName(cmbName, int.Parse(ds.Tables[0].Rows[e.Row.RowIndex]["BROKER_ID"].ToString()));
                        else
                            Bind_GridBrokerName(cmbName, 0, COMMISSION_TYPE.Text);


                    }



                    txtNAME.Text = hidNAMED_INSURED.Value;


                    if (txtAMOUNT != null)
                        txtAMOUNT.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value)");


                    if (chkLEADER != null)
                    {
                        int LookUpYes = (int)enumYESNO_LOOKUP_UNIQUE_ID.YES;
                        if (lblLEADER.Text == LookUpYes.ToString())//if (ds.Tables[0].Rows[e.Row.RowIndex]["LEADER"].ToString() == "10963")  //check leader
                            chkLEADER.Checked = true;
                        else
                            chkLEADER.Checked = false;
                    }

                    //format Commission and amount according to policy currency

                    if (commission.Text != "")
                        commission.Text = Convert.ToDouble(((TextBox)e.Row.FindControl("txtCOMMISSION")).Text).ToString("N", numberFormatInfo);

                    if (txtAMOUNT.Text != "")
                        txtAMOUNT.Text = Convert.ToDouble(((TextBox)e.Row.FindControl("txtAMOUNT")).Text).ToString("N", numberFormatInfo);


                    if (ArOld.Count > 0)
                    {
                        commission.Text = ArOld[l].ToString();
                        l++;
                    }
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                l = 0;
            }

        }//Bind grid View Data at grid Load from Data Set

        private string PolicyLabelCommission()
        {
            ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
            string PlicyLebelCommission = objGeneralInformation.GetPolicyLavelCommission(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID);
            return PlicyLebelCommission;
        } // Fill Policy Lavel Commission and get default broker Id 

        private void Bind_GridCommissionType(DropDownList cmbCOMMISSION_TYPE, int SelectedValue)
        {
            //cmbCOMMISSION_TYPE.DataSource = ILIST;
            if (Table != null && Table.Rows.Count > 0)
            {
                cmbCOMMISSION_TYPE.DataSource = Table;
                cmbCOMMISSION_TYPE.DataTextField = "DISPLAY_DESCRIPTION";//"LookupDesc";//"DISPLAY_DESCRIPTION";
                cmbCOMMISSION_TYPE.DataValueField = "TRAN_ID";//"LookupID";//"TRAN_ID";
                cmbCOMMISSION_TYPE.DataBind();
                cmbCOMMISSION_TYPE.Items.Insert(0, "");
                cmbCOMMISSION_TYPE.SelectedIndex = cmbCOMMISSION_TYPE.Items.IndexOf(cmbCOMMISSION_TYPE.Items.FindByValue(SelectedValue.ToString()));
            }
        }//Bind Commission Type in gridview Combo

        private void Bind_GridBrokerName(DropDownList cmbCOMMISSION_TYPE, int SelectedValue, string CommissionType)
        {
            if (CommissionType != "")
            {
                DataTable dtBroker_Name = new DataTable();
                string TextField = "";
                string ValueField = "";
                int intCommission = (int)enumCommissionType.COMMISSION;
                int intEnrollmentFees = (int)enumCommissionType.ENROLLMENT_FEE;
                int intPro_Labore = (int)enumCommissionType.PRO_LABORE;
                int Broker = (int)enumAgencyType.BROKER_AGENCY;
                int SalesAgent = (int)enumAgencyType.SALES_AGENT;

                string FilterExpression = "";
                if (Convert.ToInt32(CommissionType) == intCommission)
                    FilterExpression = Broker.ToString();
                else if (Convert.ToInt32(CommissionType) == intEnrollmentFees || Convert.ToInt32(CommissionType) == intPro_Labore) //if commission type is pro-Labore or enrollment fess then brokers are sales agent type
                    FilterExpression = SalesAgent.ToString() + "," + Broker.ToString();

                //if (Convert.ToInt32(CommissionType) == intPro_Labore)
                //{
                //    dtBroker_Name.Clear();
                //    dtBroker_Name = dtCo_Applicant.Copy();
                //    TextField = "APPLICANTNAME";
                //    ValueField = "APPLICANT_ID";
                //}
                //else
                //{
                dtBroker_Name.Clear();
                TextField = "AGENCY_NAME_ACTIVE_STATUS";
                ValueField = "AGENCY_ID";
                if (objDataSet.Tables[0].Select("AGENCY_TYPE IN(" + FilterExpression + ")").Length > 0)
                    dtBroker_Name = objDataSet.Tables[0].Select("AGENCY_TYPE IN(" + FilterExpression + ")", "AGENCY_TYPE desc,AGENCY_NAME_ACTIVE_STATUS asc").CopyToDataTable().Copy();
                else
                    dtBroker_Name = objDataSet.Tables[0].Clone();
                //}


                cmbCOMMISSION_TYPE.Items.Clear();
                cmbCOMMISSION_TYPE.DataSource = dtBroker_Name;
                cmbCOMMISSION_TYPE.DataTextField = TextField;
                cmbCOMMISSION_TYPE.DataValueField = ValueField;
                cmbCOMMISSION_TYPE.DataBind();
                cmbCOMMISSION_TYPE.Items.Insert(0, "");
                cmbCOMMISSION_TYPE.SelectedIndex = cmbCOMMISSION_TYPE.Items.IndexOf(cmbCOMMISSION_TYPE.Items.FindByValue(SelectedValue.ToString()));
            }
            else
                cmbCOMMISSION_TYPE.Items.Insert(0, "");
        }//Broker List for grid dropdown

        private void getNamedInsured()
        {
            DataTable dt = new DataTable();
            dt = ClsGeneralInformation.CheckApplicantForPolicy(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID);

            DataRow[] drPrimary_Applicant = dt.Select("IS_PRIMARY_APPLICANT=1"); //Select the primary Applicant 

            if (drPrimary_Applicant.Length != 0)
                hidNAMED_INSURED.Value = drPrimary_Applicant[0]["APPLICANTNAME"].ToString();
            else
                hidNAMED_INSURED.Value = "";

        } //get Primary applicant name insured

        private DataTable AddEmptyRow(DataView dvv)
        {
            int RiskId = 0;
            // dtt = dvv.Table;
            dtt = dvv.Table.Clone();
            int index = 0;
            foreach (DataRow rw in dvv.Table.Select(null, "PRODUCT_RISK_ID"))
            {
                if (int.Parse(rw["PRODUCT_RISK_ID"].ToString()) != RiskId)
                {
                    RiskId = int.Parse(rw["PRODUCT_RISK_ID"].ToString());

                    DataRow dr = dtt.NewRow();
                    dr["PRODUCT_RISK_ID"] = DBNull.Value;
                    dr["REMUNERATION_ID"] = DBNull.Value;
                    dr["BROKER_ID"] = DBNull.Value;
                    dr["BROKER_NAME"] = DBNull.Value;
                    dr["BRANCH"] = DBNull.Value;
                    dr["STATUS"] = "Group";
                    dr["LEADER"] = DBNull.Value;
                    dr["COMMISSION_TYPE"] = DBNull.Value;
                    dr["AMOUNT"] = DBNull.Value;
                    dr["NAME"] = DBNull.Value;
                    dr["RISK_NAME"] = rw["RISK_NAME"];
                    dr["CO_APPLICANT_ID"] = DBNull.Value;
                    if (Convert.ToDouble(txtPOLICY_LEVEL_COMMISSION.Text) != Convert.ToDouble("0"))
                    {
                        dr["COMMISSION_PERCENT"] = DBNull.Value;
                    }
                    else
                    {
                        dr["COMMISSION_PERCENT"] = DBNull.Value;
                    }

                    dtt.Rows.Add(dr);
                    dtt.AcceptChanges();
                    //dtt.Rows.Add(dr);
                }

                //else 
                ////{
                DataRow dr2 = dtt.NewRow();
                dr2["REMUNERATION_ID"] = rw["REMUNERATION_ID"];
                dr2["PRODUCT_RISK_ID"] = rw["PRODUCT_RISK_ID"];
                dr2["BROKER_ID"] = rw["BROKER_ID"];
                dr2["BROKER_NAME"] = rw["BROKER_NAME"];
                dr2["BRANCH"] = rw["BRANCH"];
                dr2["STATUS"] = rw["STATUS"];
                dr2["LEADER"] = rw["LEADER"];
                dr2["COMMISSION_TYPE"] = rw["COMMISSION_TYPE"];
                dr2["AMOUNT"] = rw["AMOUNT"];
                dr2["NAME"] = rw["NAME"];
                dr2["COMMISSION_PERCENT"] = rw["COMMISSION_PERCENT"];
                dr2["RISK_NAME"] = rw["RISK_NAME"];
                dr2["CO_APPLICANT_ID"] = rw["CO_APPLICANT_ID"];
                dtt.Rows.Add(dr2);
                dtt.AcceptChanges();
                //}
                index = index + 1;
                //}

            }
            return dtt;
            //get Primary applicant name insured
        }

        private void FillCo_Applicants(DropDownList cmbCO_APPLICANT, string Selectedvalue)
        {

            //DataTable dt = new DataTable();
            // dt = ClsGeneralInformation.CheckApplicantForPolicy(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID);
            string[] COLUMN_NAME = { "APPLICANT_ID", "APPLICANTNAME" };
            cmbCO_APPLICANT.DataSource = dtCo_Applicant.DefaultView.ToTable("CO_APPLICANTS", true, COLUMN_NAME);
            cmbCO_APPLICANT.DataTextField = "APPLICANTNAME";
            cmbCO_APPLICANT.DataValueField = "APPLICANT_ID";
            cmbCO_APPLICANT.DataBind();
            cmbCO_APPLICANT.Items.Insert(0, "");
            if (Selectedvalue != "")
                cmbCO_APPLICANT.SelectedValue = Selectedvalue;
        }

        private bool IS_PrimartAppicant(int ApplicantID)
        {
            bool IS_PRIMARY_APPLICANT = false;
            foreach (DataRow dr in dtCo_Applicant.Rows)
            {
                if (dr["APPLICANT_ID"].ToString() == ApplicantID.ToString() && dr["IS_PRIMARY_APPLICANT"].ToString() == "1")
                {
                    IS_PRIMARY_APPLICANT = true;
                }
            }
            return IS_PRIMARY_APPLICANT;
        }

        private DataTable GetCO_ApplicantDetails()
        {
            DataTable dt = null;
            dt = ClsGeneralInformation.CheckApplicantForPolicy(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID);
            return dt;
        }

        private static DataTable GetCO_ApplicantDetails(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID)
        {
            DataTable dt = null;
            dt = ClsGeneralInformation.CheckApplicantForPolicy(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID);
            return dt;
        }

        protected void grdBROKER_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.EnableViewState;
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                //LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton1");
                //if (lb != null)
                //{
                //    if (dt.Rows.Count > 1)
                //    {
                //        if (e.Row.RowIndex == dt.Rows.Count - 1)
                //        {
                //            lb.Visible = false;
                //        }
                //    }
                //    else
                //    {
                //        lb.Visible = false;
                //    }
                //}
            }
        }

        [System.Web.Services.WebMethod(true)]
        public static System.Collections.Generic.Dictionary<string, object> GetBrokerName(string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID, string COMMISSION_TYPE)
        {
            //  ClsAgency objAgency = new ClsAgency();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


            int intCommission = (int)enumCommissionType.COMMISSION;
            int intEnrollmentFees = (int)enumCommissionType.ENROLLMENT_FEE;
            int intPro_Labore = (int)enumCommissionType.PRO_LABORE;
            int BROKER_AGENCY = (int)enumAgencyType.BROKER_AGENCY;
            int SALES_AGENT = (int)enumAgencyType.SALES_AGENT;
            try
            {
                System.Collections.Generic.Dictionary<string, object> dd = new System.Collections.Generic.Dictionary<string, object>();
                if (COMMISSION_TYPE != "")
                {
                    //if (int.Parse(COMMISSION_TYPE) == intPro_Labore)
                    //    dt = GetCO_ApplicantDetails(int.Parse(CUSTOMER_ID), int.Parse(POLICY_ID), int.Parse(POLICY_VERSION_ID));
                    //else
                    if (HttpContext.Current.Session["AGENCY"] != null)
                        ds = (DataSet)HttpContext.Current.Session["AGENCY"]; //objAgency.FillAgency();

                    if (int.Parse(COMMISSION_TYPE) == intCommission)
                        dt = ds.Tables[0].Select("AGENCY_TYPE =" + BROKER_AGENCY.ToString()).CopyToDataTable<DataRow>();
                    else if (int.Parse(COMMISSION_TYPE) == intEnrollmentFees || int.Parse(COMMISSION_TYPE) == intPro_Labore) //Sales agent for pro-Labore and enrollmentfee
                        dt = ds.Tables[0].Select("AGENCY_TYPE =" + SALES_AGENT.ToString()).CopyToDataTable<DataRow>();

                }
                dt.TableName = "Table";
                return Cms.CmsWeb.support.ClsjQueryCommon.ToJson(dt);
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["AGENCY"] != null)
                    ds = (DataSet)HttpContext.Current.Session["AGENCY"]; //objAgency.FillAgency();
                dt.TableName = "Table";
                dt = ds.Tables[0].Clone();
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return Cms.CmsWeb.support.ClsjQueryCommon.ToJson(dt);
                
                //throw ex;
            }

        }


    }



}
