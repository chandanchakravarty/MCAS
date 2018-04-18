/******************************************************************************************
<Author					: -  Vijay Arora
<Start Date				: -	 28-10-2005
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/
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
using Cms.BusinessLayer.BlApplication;
using Cms.Model.Application;
using Cms.Model.Quote;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlQuote;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Policy;
using System.Resources;
using System.Reflection;

namespace Cms.Policies.Aspx
{
    /// <summary>
    /// Summary description for PolicyNameIns.
    /// </summary>
    public class PolicyNameIns : Cms.Policies.policiesbase
    {
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
        //protected System.Web.UI.WebControls.LinkButton lnkAPPLICANTNAME;

        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnShowAllApplicants;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.WebControls.DataGrid dgrApplicant;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSavedStatus;
        protected System.Web.UI.WebControls.RadioButton rdbPRIMARY_APPLICANT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLobID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidMode;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;//Done on 26 May 2009 to enable showing of Prompt box(getUserConfirmation()) on tabbing from NameInsured tab to other tabs
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
        //Added by lalit 
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPRODUCT_TYPE;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        int intFromUserId;
        //Added By Lalit 23 April,2010 For Header caption
        protected System.Web.UI.WebControls.Label lblHeader;
        protected Cms.CmsWeb.Controls.CmsButton btnCustomerAssistant;
        protected Cms.CmsWeb.Controls.CmsButton btnCustomerCoApplicant;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPRIMARYAPP_ID;
        protected string PrimaryApplicantmsg;
        DataTable dtCheck = new DataTable();
        ResourceManager Objresources;
        private void Page_Load(object sender, System.EventArgs e)
        {
            base.ScreenId = "224_7";//Screen id set to 224_7 instead og 1001 to include it to policy permission list-Added by Sibin on 21 Oct 08

            Objresources = new System.Resources.ResourceManager("Cms.Policies.Aspx.PolicyNameIns", System.Reflection.Assembly.GetExecutingAssembly());
            setcaption();
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnShowAllApplicants.CmsButtonClass = CmsButtonType.Write;
            btnShowAllApplicants.PermissionString = gstrSecurityXML;

            btnCustomerAssistant.CmsButtonClass = CmsButtonType.Read;
            btnCustomerAssistant.PermissionString = gstrSecurityXML;

            btnCustomerCoApplicant.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnCustomerCoApplicant.PermissionString = gstrSecurityXML;

            btnCustomerAssistant.Visible = true;
            btnCustomerAssistant.Attributes.Add("onclick", "javascript:return BackToCustomer();");
            btnCustomerCoApplicant.Attributes.Add("onClick", "javascript:return AddNewCoApplicant();");

            intFromUserId = int.Parse(GetUserId());
            hidPolicyID.Value = GetPolicyID();
            hidPolicyVersionID.Value = GetPolicyVersionID();

            if (GetLOBID() != "")
                hidPRODUCT_TYPE.Value = GetTransaction_Type();//GetProduct_Type(int.Parse(GetLOBID()));
            else
                hidPRODUCT_TYPE.Value = SIMPLE_POLICY; //GetProduct_Type(int.Parse(GetLOBID()));

            btnSave.Attributes.Add("onclick", "javascript:return Validate()");
            btnReset.Attributes.Add("onclick", "javascript:EnableRfv()");

            if (!IsPostBack)
            {
                SetHiddenValue();
                try
                {
                    // check whether Primary & Secondary Applicant are set.

                    /*dtCheck=ClsGeneralInformation.CheckApplicantForPolicy(int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value));
                    if (dtCheck.Rows.Count > 0)
                    {

                        dgrApplicant.DataSource=dtCheck.DefaultView;
                        dgrApplicant.DataBind();
                        hidMode.Value="Update";	
                        MapChecked(dtCheck);
                    }
                    else
                    {
                        DataTable dtApplicant=new DataTable();
                        dtApplicant=ClsGeneralInformation.FetchCustApplicantInsured(int.Parse(hidCustomerID.Value));
                        if (dtApplicant.Rows.Count > 0)
                        {						
                            dgrApplicant.DataSource=dtApplicant.DefaultView;
                            dgrApplicant.DataBind();
                            hidMode.Value="Save";							
                        }
                        else
                        {
                            lblMessage.Visible=true;
                            lblMessage.Text=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
                        }
                    }*/

                    //Added by Charles on 12-May-09 for Itrack 5655           
                 


                    DataTable dt = new DataTable();
                    dt = ClsGeneralInformation.CheckApplicantForPolicy(int.Parse(hidCustomerID.Value), int.Parse(hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value));
                    if (dt.Rows.Count > 0)
                    {
                        dgrApplicant.DataSource = dt.DefaultView;
                        dgrApplicant.DataBind();
                        hidMode.Value = "Update";
                        hidOldData.Value = ClsCommon.GetXMLEncoded(dt);//Done on 26 May 2009 to enable showing of Prompt box(getUserConfirmation()) on tabbing from NameInsured tab to other tabs
                        MapChecked(dt);
                        //  itrack no 847 
                       // if (hidPRODUCT_TYPE.Value != MASTER_POLICY)
                       // {
                            //if not master policy then commission percentage & fees percentage shoulld not show
                            dgrApplicant.Columns[9].Visible = false;
                            dgrApplicant.Columns[10].Visible = false;
                            dgrApplicant.Columns[11].Visible = false;
                      //  }
                   DataTable dtcom = new DataTable();
                   string strLob = GetLOBID() == "" ? "0" : GetLOBID();
                   dtcom = ClsGeneralInformation.GetCommissionType("COM", int.Parse(strLob));
                        for(int i=0;i < dtcom.Rows.Count; i++ ){
                            if (hidPRODUCT_TYPE.Value == MASTER_POLICY && dtcom.Rows[i]["TRAN_ID"].ToString() == "43") {
                                dgrApplicant.Columns[9].Visible = true;                                
                            }                           
                             
                            if (hidPRODUCT_TYPE.Value == MASTER_POLICY && dtcom.Rows[i]["TRAN_ID"].ToString() == "44")
                            {
                                dgrApplicant.Columns[10].Visible = true;
                            }                           

                            if (hidPRODUCT_TYPE.Value == MASTER_POLICY && dtcom.Rows[i]["TRAN_ID"].ToString() == "45")
                            {
                                dgrApplicant.Columns[11].Visible = true;
                            }
                           

                        }
                             
                    }
                    else
                    {
                        btnShowAllApplicants_Click(null, null);
                    }
                    //Added till here
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.Visible = true;
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                }
            }
        }


        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            this.btnShowAllApplicants.Click += new System.EventHandler(this.btnShowAllApplicants_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);
            dgrApplicant.ItemCommand += new DataGridCommandEventHandler(dgrApplicant_ItemCommand);
            dgrApplicant.ItemDataBound += new DataGridItemEventHandler(dgrApplicant_ItemDataBound);
            //grdBROKER.RowDataBound +=new  GridViewRowEventHandler()
        }


        #endregion

        private void SetHiddenValue()
        {
            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
            {
                hidCustomerID.Value = Request.QueryString["CUSTOMER_ID"].ToString();
            }
            if (Request.QueryString["APP_ID"] != null && Request.QueryString["APP_ID"].ToString() != "")
            {
                hidAppID.Value = Request.QueryString["APP_ID"].ToString();
            }
            if (Request.QueryString["APP_VERSION_ID"] != null && Request.QueryString["APP_VERSION_ID"].ToString() != "")
            {
                hidAppVersionID.Value = Request.QueryString["APP_VERSION_ID"].ToString();
            }

            hidLobID.Value = GetLOBID();

        }

        private void MapChecked(DataTable dtCheck)
        {
            int applicant_Id;
            CheckBox chkBox;
            HtmlInputRadioButton htmRadio;
            foreach (DataRow dr in dtCheck.Rows)
            {
                foreach (DataGridItem dgi in dgrApplicant.Items)
                {
                    applicant_Id = int.Parse(dgrApplicant.DataKeys[dgi.ItemIndex].ToString());
                    if (applicant_Id == int.Parse(dr["APPLICANT_ID"].ToString()))
                    {
                        chkBox = (CheckBox)dgi.FindControl("chkSECONDARY_APPLICANT");
                        htmRadio = (HtmlInputRadioButton)dgi.FindControl("rdbPRIMARY_APPLICANT");

                        //chkBox.Checked condition, moved outside the if loop by Charles on 12-May-08 for Itrack 5655
                        chkBox.Checked = true;

                        if (int.Parse(dr["IS_PRIMARY_APPLICANT"].ToString()) == 1)
                        {
                            htmRadio.Checked = true;
                            hidPRIMARYAPP_ID.Value = dr["APPLICANT_ID"].ToString();
                            //chkBox.Checked=true, Removed by Charles on 12-May-08 for Itrack 5655
                        }
                        else
                        {
                            htmRadio.Checked = false;
                            //chkBox.Checked=false, Removed by Charles on 12-May-08 for Itrack 5655
                        }

                        //added by lalit   
                        TextBox txtCOMMISSION_PERCENT = (TextBox)dgi.FindControl("txtCOMMISSION_PERCENT");
                        if (dr["COMMISSION_PERCENT"].ToString() != "")
                            txtCOMMISSION_PERCENT.Text = double.Parse(dr["COMMISSION_PERCENT"].ToString()).ToString("N", numberFormatInfo);
                        //else
                        //    txtCOMMISSION_PERCENT.Text = double.Parse("0").ToString("N", numberFormatInfo);


                        TextBox txtFEES_PERCENT = (TextBox)dgi.FindControl("txtFEES_PERCENT");
                        if (dr["FEES_PERCENT"].ToString() != "")
                            txtFEES_PERCENT.Text = double.Parse(dr["FEES_PERCENT"].ToString()).ToString("N", numberFormatInfo);


                        TextBox txtPRO_LABORE_PERCENT = (TextBox)dgi.FindControl("txtPRO_LABORE_PERCENT");
                        if (dr["PRO_LABORE_PERCENT"].ToString() != "")
                            txtPRO_LABORE_PERCENT.Text = double.Parse(dr["PRO_LABORE_PERCENT"].ToString()).ToString("N", numberFormatInfo);

                    }
                }
            }
        }

        private void btnReset_Click(object sender, System.EventArgs e)
        {
            lblMessage.Text = "";
            try
            {
                // check whether Primary & Secondary Applicant are set.
                dtCheck = ClsGeneralInformation.CheckApplicantForPolicy(int.Parse(hidCustomerID.Value), int.Parse(hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value));
                if (dtCheck.Rows.Count > 0)
                {
                    dgrApplicant.DataSource = dtCheck.DefaultView;
                    dgrApplicant.DataBind();
                    hidMode.Value = "Update";
                    MapChecked(dtCheck);
                }
                else
                {
                    DataTable dtApplicant = new DataTable();
                    dtApplicant = ClsGeneralInformation.FetchCustApplicantInsured(int.Parse(hidCustomerID.Value));
                    if (dtApplicant.Rows.Count > 0)
                    {
                        dgrApplicant.DataSource = dtApplicant.DefaultView;
                        dgrApplicant.DataBind();
                        hidMode.Value = "Save";
                    }
                    else
                    {
                        //lblMessage.Visible=true;
                        //lblMessage.Text="Applicants for selected customer are not available";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }

            if (hidPRODUCT_TYPE.Value != MASTER_POLICY)
            {
                //if not master policy then commission percentage & fees percentage shoulld not show
                dgrApplicant.Columns[9].Visible = false;
                dgrApplicant.Columns[10].Visible = false;
                dgrApplicant.Columns[11].Visible = false;
            }
        }

        private void btnShowAllApplicants_Click(object sender, System.EventArgs e)
        {
            lblMessage.Text = "";
            DataTable dtApplicant = new DataTable();
            hidMode.Value = "Update";
            dtApplicant = ClsGeneralInformation.FetchCustApplicantInsured(int.Parse(hidCustomerID.Value));
            if (dtApplicant.Rows.Count > 0)
            {
                dgrApplicant.DataSource = dtApplicant.DefaultView;
                dgrApplicant.DataBind();
            }
            DataTable dt = new DataTable();
            dt = ClsGeneralInformation.CheckApplicantForPolicy(int.Parse(hidCustomerID.Value), int.Parse(hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value));
            MapChecked(dt);


            if (hidPRODUCT_TYPE.Value != MASTER_POLICY)
            {
                //if not master policy then commission percentage & fees percentage shoulld not show
                dgrApplicant.Columns[9].Visible = false;
                dgrApplicant.Columns[10].Visible = false;
                dgrApplicant.Columns[11].Visible = false;
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            //int k = 0;
            int applicant_Id;
            CheckBox chkBox;
            HtmlInputRadioButton htmRadio;
            ArrayList a1 = new ArrayList();
            ClsPolicyInsuredInfo objPolicy;
            foreach (DataGridItem dgi in dgrApplicant.Items)
            {
                chkBox = (CheckBox)dgi.FindControl("chkSECONDARY_APPLICANT");
                htmRadio = (HtmlInputRadioButton)dgi.FindControl("rdbPRIMARY_APPLICANT");
                if ((chkBox != null && chkBox.Checked == true) || (htmRadio != null && htmRadio.Checked == true))
                {
                    objPolicy = new ClsPolicyInsuredInfo();
                    applicant_Id = int.Parse(dgrApplicant.DataKeys[dgi.ItemIndex].ToString());
                    objPolicy.POLICY_ID = Convert.ToInt32(GetPolicyID());
                    objPolicy.POLICY_VERSION_ID = Convert.ToInt32(GetPolicyVersionID());
                    objPolicy.APPLICANT_ID = applicant_Id;
                    objPolicy.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
                    //objPolicy.APP_ID=int.Parse(hidAppID.Value);
                    //objPolicy.APP_VERSION_ID=int.Parse(hidAppVersionID.Value);

                    //Added by lalit on oct 20,2010, for master policy implimentation
                    TextBox txtCOMMISSION_PERCENT = (TextBox)dgi.FindControl("txtCOMMISSION_PERCENT");
                    if (txtCOMMISSION_PERCENT.Text != "")
                        objPolicy.COMMISSION_PERCENT = double.Parse(txtCOMMISSION_PERCENT.Text, numberFormatInfo);
                    else
                        objPolicy.COMMISSION_PERCENT = double.Parse("0");


                    TextBox txtFEES_PERCENT = (TextBox)dgi.FindControl("txtFEES_PERCENT");
                    if (txtFEES_PERCENT.Text != "")
                        objPolicy.FEES_PERCENT = double.Parse(txtFEES_PERCENT.Text, numberFormatInfo);
                    else
                        objPolicy.FEES_PERCENT = double.Parse("0");

                    TextBox txtPRO_LABORE_PERCENT = (TextBox)dgi.FindControl("txtPRO_LABORE_PERCENT");
                    if (txtPRO_LABORE_PERCENT.Text != "")
                        objPolicy.PRO_LABORE_PERCENT = double.Parse(txtPRO_LABORE_PERCENT.Text, numberFormatInfo);
                    else
                        objPolicy.PRO_LABORE_PERCENT = 0;


                    if (htmRadio != null && htmRadio.Checked == true)
                    {
                        objPolicy.IS_PRIMARY_APPLICANT = 1;
                    }
                    else
                    {
                        objPolicy.IS_PRIMARY_APPLICANT = 0;
                    }
                    if (hidMode.Value == "Save")
                    {
                        objPolicy.CREATED_BY = intFromUserId;
                    }
                    else
                    {
                        objPolicy.MODIFIED_BY = intFromUserId;
                    }
                    a1.Add(objPolicy);
                }
            }
            if (hidMode.Value == "Save")
            {
                if (a1.Count > 0)
                {
                    ClsGeneralInformation.SavePrimaryNamedInsured(a1);
                    DataTable dt = new DataTable();
                    dt = ClsGeneralInformation.CheckApplicantForPolicy(int.Parse(hidCustomerID.Value), int.Parse(hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value));
                    dgrApplicant.DataSource = dt.DefaultView;
                    dgrApplicant.DataBind();
                    MapChecked(dt);
                    base.OpenEndorsementDetails();
                    lblMessage.Visible = true;
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
                }
              else { lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5"); }
            }
            else if (hidMode.Value == "Update")
            {
                //Modified by lalit Feb 22,2011
                //if Co-applicant was considered on remuneration then co-applicant can't be deselected from co-applicant sceen. in case of master policy
                if (a1.Count > 0)
                {
                    bool Co_ApplicantExists = true;
                    DataSet ds = ClsGeneralInformation.CheckApplicantInRemuneration(int.Parse(hidCustomerID.Value), int.Parse(hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value));
                    if (GetTransaction_Type() == MASTER_POLICY)
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                bool Exists = false;
                                for (int i = 0; i < a1.Count; i++)
                                {
                                    ClsPolicyInsuredInfo Co_appID = (ClsPolicyInsuredInfo)a1[i];
                                    if (Co_appID.APPLICANT_ID.ToString() == dr["CO_APPLICANT_ID"].ToString())
                                    {
                                        Exists = true;
                                        break;
                                    }
                                }
                                if (Exists)
                                    continue;
                                else
                                {
                                    Co_ApplicantExists = false;
                                    break;
                                }
                            }
                        } if (!Co_ApplicantExists)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
                    }
                    else
                    {


                        ClsGeneralInformation.UpdatePrimaryNamedInsured(a1);
                        DataTable dt = new DataTable();
                        dt = ClsGeneralInformation.CheckApplicantForPolicy(int.Parse(hidCustomerID.Value), int.Parse(hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value));
                        dgrApplicant.DataSource = dt.DefaultView;
                        dgrApplicant.DataBind();
                        MapChecked(dt);
                        base.OpenEndorsementDetails();
                        lblMessage.Visible = true;
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
                    }
                    //}else if(RetVal == -2)
                    //    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
                }

                if (hidPRODUCT_TYPE.Value != MASTER_POLICY)
                {
                    //if not master policy then commission percentage & fees percentage shoulld not show
                    dgrApplicant.Columns[9].Visible = false;
                    dgrApplicant.Columns[9].Visible = false;
                    dgrApplicant.Columns[10].Visible = false;
                }
            }
            else 
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            }
        }

        protected void dgrApplicant_ItemCommand(object sender, DataGridCommandEventArgs e)
        {


        }
        protected void dgrApplicant_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            //LinkButton lnkbtn = (LinkButton)e.Item.FindControl("lnkAPPLICANTNAME");
            //if (lnkbtn != null) 
            //{
            //    lnkbtn.Attributes.Add("onclick", "javascript:return ShowApplicantDetails()");
            ////    //OnClientClick="ShowApplicantDetails('<%# Eval("APPLICANT_ID") %>')"
            //}
            // 

            //Modified  by Lalit for master policy implimentation

            if (e.Item.ItemType == ListItemType.Header)
            {
                Label capCOMMISSION_PERCENT = (Label)e.Item.FindControl("capCOMMISSION_PERCENT");
                capCOMMISSION_PERCENT.Text = Objresources.GetString("txtCOMMISSION_PERCENT");
                Label capFEES_PERCENT = (Label)e.Item.FindControl("capFEES_PERCENT");
                capFEES_PERCENT.Text = Objresources.GetString("txtFEES_PERCENT");
                Label capPRO_LABORE_PERCENT = (Label)e.Item.FindControl("capPRO_LABORE_PERCENT");
                capPRO_LABORE_PERCENT.Text = Objresources.GetString("txtPRO_LABORE_PERCENT");
                e.Item.Cells[7].Text = Objresources.GetString("ZIP_CODE");
                e.Item.Cells[6].Text = Objresources.GetString("STATE");
                e.Item.Cells[5].Text = Objresources.GetString("CITY");
                e.Item.Cells[4].Text = Objresources.GetString("ADDRESS");
                e.Item.Cells[3].Text = Objresources.GetString("Name");
                e.Item.Cells[2].Text = Objresources.GetString("POSITION");
                e.Item.Cells[1].Text = Objresources.GetString("Primary");
                e.Item.Cells[0].Text = Objresources.GetString("Add/Remove");


            }
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                numberFormatInfo.NumberDecimalDigits = 2;
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                RegularExpressionValidator revCOMMISSION_PERCENT = (RegularExpressionValidator)e.Item.FindControl("revCOMMISSION_PERCENT");
                revCOMMISSION_PERCENT.ValidationExpression = aRegExpDoublePositiveWithZeroFourDeci;
                revCOMMISSION_PERCENT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");

                RequiredFieldValidator rfvCOMMISSION_PERCENT = (RequiredFieldValidator)e.Item.FindControl("rfvCOMMISSION_PERCENT");
                rfvCOMMISSION_PERCENT.ErrorMessage = ClsMessages.FetchGeneralMessage("474");

                RequiredFieldValidator rfvPRO_LABORE_PERCENT = (RequiredFieldValidator)e.Item.FindControl("rfvPRO_LABORE_PERCENT");
                rfvPRO_LABORE_PERCENT.ErrorMessage = ClsMessages.FetchGeneralMessage("1188");

                CustomValidator csvCOMMISSION_PERCENT = (CustomValidator)e.Item.FindControl("csvCOMMISSION_PERCENT");
                csvCOMMISSION_PERCENT.ErrorMessage = ClsMessages.FetchGeneralMessage("1113");

                RegularExpressionValidator revFEES_PERCENT = (RegularExpressionValidator)e.Item.FindControl("revFEES_PERCENT");
                revFEES_PERCENT.ValidationExpression = aRegExpDoublePositiveWithZeroFourDeci;
                revFEES_PERCENT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");

                RequiredFieldValidator rfvFEES_PERCENT = (RequiredFieldValidator)e.Item.FindControl("rfvFEES_PERCENT");
                rfvFEES_PERCENT.ErrorMessage = ClsMessages.FetchGeneralMessage("1187");

                CustomValidator csvFEES_PERCENT = (CustomValidator)e.Item.FindControl("csvFEES_PERCENT");
                csvFEES_PERCENT.ErrorMessage = ClsMessages.FetchGeneralMessage("1113");

                CustomValidator csvPRO_LABORE_PERCENT = (CustomValidator)e.Item.FindControl("csvPRO_LABORE_PERCENT");
                csvPRO_LABORE_PERCENT.ErrorMessage = ClsMessages.FetchGeneralMessage("1113");


                RegularExpressionValidator revPRO_LABORE_PERCENT = (RegularExpressionValidator)e.Item.FindControl("revPRO_LABORE_PERCENT");
                revPRO_LABORE_PERCENT.ValidationExpression = aRegExpDoublePositiveWithZeroFourDeci;
                revPRO_LABORE_PERCENT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");

                TextBox txtCOMMISSION_PERCENT = (TextBox)e.Item.FindControl("txtCOMMISSION_PERCENT");
                txtCOMMISSION_PERCENT.Attributes.Add("onblur", "javascript:this.value= formatAmount(this.value,2)");
                txtCOMMISSION_PERCENT.Attributes.Add("onChange", "javascript:this.value= formatAmount(this.value,2)");
                
                TextBox txtFEES_PERCENT = (TextBox)e.Item.FindControl("txtFEES_PERCENT");

                TextBox txtPRO_LABORE_PERCENT = (TextBox)e.Item.FindControl("txtPRO_LABORE_PERCENT");

                if (txtFEES_PERCENT.Text != "")
                    txtFEES_PERCENT.Text = Convert.ToDouble(txtFEES_PERCENT.Text).ToString("N", numberFormatInfo);

                if (txtCOMMISSION_PERCENT.Text != "")
                    txtCOMMISSION_PERCENT.Text = Convert.ToDouble(txtCOMMISSION_PERCENT.Text).ToString("N", numberFormatInfo);

                if (txtPRO_LABORE_PERCENT.Text != "")
                    txtPRO_LABORE_PERCENT.Text = Convert.ToDouble(txtPRO_LABORE_PERCENT.Text).ToString("N", numberFormatInfo);

                CheckBox chkBox = (CheckBox)e.Item.FindControl("chkSECONDARY_APPLICANT");
                chkBox.Attributes.Add("onClick", "javascript:onselectEnableRfvs(this)");

                if (int.Parse(GetLOBID()) == 17 || int.Parse(GetLOBID()) == 18)
                {
                    txtPRO_LABORE_PERCENT.Text = "";
                    txtPRO_LABORE_PERCENT.ReadOnly = true;
                    txtFEES_PERCENT.Text = "";
                    txtFEES_PERCENT.ReadOnly = true;
                }
                else
                {
                    txtFEES_PERCENT.Attributes.Add("onblur", "javascript:this.value= formatAmount(this.value,2)");
                    txtFEES_PERCENT.Attributes.Add("onChange", "javascript:this.value= formatAmount(this.value,2)");
                    txtPRO_LABORE_PERCENT.Attributes.Add("onblur", "javascript:this.value= formatAmount(this.value,2)");
                    txtPRO_LABORE_PERCENT.Attributes.Add("onChange", "javascript:this.value= formatAmount(this.value,2)");

                }
               // Label APP_ID = (Label)e.Item.FindControl("APP_ID");
                //HtmlInputRadioButton htmRadio = (HtmlInputRadioButton)e.Item.FindControl("rdbPRIMARY_APPLICANT");
                //if (htmRadio.Checked == true)
                  //  hidPRIMARYAPP_ID.Value = APP_ID.Text;//e.Item.Cells[8].Text;//APPLICANT_ID

            }

        }
        private void setcaption()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            btnCustomerCoApplicant.Text = ClsMessages.FetchGeneralButtonsText("btnCustomerCoApplicant");
            btnCustomerAssistant.Text = ClsMessages.FetchGeneralButtonsText("btnCustomerAssistant");
            if (GetApplicationStatus().ToUpper().Trim() == "APPLICATION")
            {

                lblHeader.Text = Objresources.GetString("lblHeader_2");
                //btnShowAllApplicants.Text = Objresources.GetString("btnShowAllApplicants_2");
                btnShowAllApplicants.Text = ClsMessages.FetchGeneralButtonsText("btnShowAllApplicants_2");
            }
            else
            {
                lblHeader.Text = Objresources.GetString("lblHeader_1");
                //btnShowAllApplicants.Text = Objresources.GetString("btnShowAllApplicants_1");
                btnShowAllApplicants.Text = ClsMessages.FetchGeneralButtonsText("btnShowAllApplicants_1");
            }
            PrimaryApplicantmsg = ClsMessages.GetMessage(ScreenId, "4");

        }
    }
}
