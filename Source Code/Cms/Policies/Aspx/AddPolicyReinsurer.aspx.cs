using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.Model.Policy;
using System.Data;
using Cms.BusinessLayer.BlApplication;
using System.Resources;
using System.Collections;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.CmsWeb;
using Cms.CmsWeb.Utils;
using Cms.CmsWeb.WebControls;

namespace Cms.Policies.Aspx
{
    public partial class AddPolicyReinsurer : Cms.Policies.policiesbase
    {
        #region Variables
        static DataSet objDataSet = new DataSet();
        ResourceManager Objresources;
        protected Label capREINSURER_NAME;
        protected Label capCONTRACT_FACULTATIVE;
        protected Label capCONTRACT;
        protected Label capREINSURANCE_CEDED;
        protected Label capREINSURANCE_COMMISSION;
        protected Label capREINSURER_NUMBER;  //ashish
        protected DropDownList cmbCONTRACT_FACULTATIVE;
        protected DropDownList cmbMAX_NO_INSTALLMENT;  // Added by Aditya for TFS BUG # 2514
        protected DropDownList cmbRISK_ID;
        protected DropDownList cmbCONTRACT;
        protected TextBox txtREINSURANCE_CEDED;
        protected TextBox txtREINSURANCE_COMMISSION;
        protected TextBox txtREINSURER_NUMBER;//ashish
        protected TextBox txtCOMM_AMOUNT; //Added by Aditya for tfs bug # 177
        protected TextBox txtLAYER_AMOUNT;
        protected TextBox txtREIN_PREMIUM; //added till here
        protected RequiredFieldValidator rfvREINSURANCE_CEDED;
        protected RequiredFieldValidator rfvCONTRACT_FACULTATIVE;
        protected RequiredFieldValidator rfvREINSURER_NUMBER; 
        protected RegularExpressionValidator revREINSURANCE_CEDED;
        protected CustomValidator csvREINSURANCE_CEDED;
        protected RequiredFieldValidator rfvREINSURANCE_COMMISSION;
        //protected RequiredFieldValidator rfvREINSURER_NUMBER;//ashish
        protected RegularExpressionValidator revREINSURANCE_COMMISSION;
        //protected RegularExpressionValidator revREINSURER_NUMBER;//ashish
        protected CustomValidator csvREINSURANCE_COMMISSION;
        //protected CustomValidator csvREINSURER_NUMBER;//ashish
        public static string confirmmessage;
        public static string selectChk;
        ArrayList ArOld = new ArrayList();
        ClsReinsuranceInformation objReinsuranceInformation;
        DataSet Ds = new DataSet();
        static bool exists;
        protected List<ClsPolicyReinsurerInfo> Reinsurers = new List<ClsPolicyReinsurerInfo>();
        Int32 CUSTOMER_ID = 0;
        Int32 POLICY_ID = 0;
        Int32 POLICY_VERSION_ID = 0;
        double TotalReinsurance_Premium;  //added by aditya on 17-08-2011 for itrack 1415
        double TotalRetention_per;        //added by aditya on 17-08-2011 for itrack 1415
        double TotalComm_per;             //added by aditya on 17-08-2011 for itrack 1415
        double TotalComm_Amt;             //added by aditya on 17-08-2011 for itrack 1415
        DataSet dsTemp = null;            //added by aditya on 17-08-2011 for itrack 2705
        DataTable dsTempTable = null;     //added by aditya on 17-08-2011 for itrack 2705
        string rein_id;                   //added by aditya on 17-08-2011 for itrack 2705
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCultureThread(GetLanguageCode());
            base.ScreenId = "224_30";
            Objresources = new System.Resources.ResourceManager("Cms.Policies.Aspx.AddPolicyReinsurer", System.Reflection.Assembly.GetExecutingAssembly());
            objReinsuranceInformation = new ClsReinsuranceInformation();
            btnSelect.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSelect.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btndelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
            btndelete.PermissionString = gstrSecurityXML;

            btnsave_act.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnsave_act.PermissionString = "<Security><Read>Y</Read><Write>Y</Write><Delete>N</Delete><Execute>N</Execute></Security>";// gstrSecurityXML;

            btnSave_Contract.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write; //Added by Aditya for tfs bug # 180
            btnSave_Contract.PermissionString = gstrSecurityXML; //Added by Aditya for tfs bug # 180
            
            //txtCOMM_AMOUNT.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);ValidatorOnChange();");
            //txtLAYER_AMOUNT.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);ValidatorOnChange();");
           // txtREIN_PREMIUM.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);ValidatorOnChange();");
            //revMONTHLY_INCOME.ValidationExpression = aRegExpBaseCurrencyformat;
            //hidPolicyStatus.Value = GetApplicationStatus();//GetPolicyStatus();
            if (!IsPostBack)
            {
                //TabCtl.TabURLs = "ReinsuranceSurplusIndex.aspx?";

                //+ ","
                //+ "ReinsuranceQuotaShareIndex.aspx";
                PopulateReinsurers();
                SetCaption();
                GridBind(Reinsurers);
                BindGrid();  //added by aditya on 17-08-2011 for itrack 2705
                ClsPolicyReinsurerInfo objReinsurerInfo = new ClsPolicyReinsurerInfo(); //Added by Aditya for tfs bug # 180
                dsTemp = objReinsurerInfo.FetchPolicy_DETAILS(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()));
                if (dsTemp.Tables[0].Rows[0]["DISREGARD_RI_CONTRACT"] != System.DBNull.Value && dsTemp.Tables[0].Rows[0]["DISREGARD_RI_CONTRACT"] != "0")
                {
                    cmbDISREGARD_RI_CONTRACT.SelectedIndex  = cmbDISREGARD_RI_CONTRACT.Items.IndexOf(cmbDISREGARD_RI_CONTRACT.Items.FindByValue(dsTemp.Tables[0].Rows[0]["DISREGARD_RI_CONTRACT"].ToString())); 

                } //Added till here
            }
            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                CUSTOMER_ID = Convert.ToInt32(Request.QueryString["CUSTOMER_ID"].ToString());
            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
                POLICY_ID = Convert.ToInt32(Request.QueryString["POLICY_ID"].ToString());
            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
                POLICY_VERSION_ID = Convert.ToInt32(Request.QueryString["POLICY_VERSION_ID"].ToString());
            bindrptr(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID);
            //hidmessage1.Value = Objresources.GetString("hidmessage1");
        }

        #region Methods
        private void bindrptr(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID)
        {
            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
            objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
            objDataWrapper.AddParameter("@LANG_ID", int.Parse(GetLanguageID()));
            DataSet ds = new DataSet();
            ds = objDataWrapper.ExecuteDataSet("Proc_RITreatiesForPolicy");
            //DataTable dt = ds.Tables[0];
            if (ds != null && ds.Tables.Count > 0)
            {
                rptrein.DataSource = ds;
                rptrein.DataBind();
            }


        }

        protected void rptrein_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Header)
            {

                Label l1 = (Label)e.Item.FindControl("lblPOLICY_NUMBER");
                l1.Text = Objresources.GetString("lblPOLICY_NUMBER");
                Label l2 = (Label)e.Item.FindControl("lblREINSURER_NAME");
                l2.Text = Objresources.GetString("lblREINSURER_NAME");
                Label l3 = (Label)e.Item.FindControl("lblCONTRACT_NUMBER");
                l3.Text = Objresources.GetString("lblCONTRACT_NUMBER");
                Label l4 = (Label)e.Item.FindControl("lblLAYER");
                l4.Text = Objresources.GetString("lblLAYER");
                Label l5 = (Label)e.Item.FindControl("lblLAYER_AMOUNT");
                l5.Text = Objresources.GetString("lblLAYER_AMOUNT");
                Label l6 = (Label)e.Item.FindControl("lblRETENTION_PER");
                l6.Text = Objresources.GetString("lblRETENTION_PER");
                Label l7 = (Label)e.Item.FindControl("lblTRAN_PREMIUM");
                l7.Text = Objresources.GetString("lblTRAN_PREMIUM");
                //Label l8 = (Label)e.Item.FindControl("lblREIN_PREMIUM");
                //l8.Text = Objresources.GetString("lblREIN_PREMIUM");
                Label l9 = (Label)e.Item.FindControl("lblCOMM_AMOUNT");
                l9.Text = Objresources.GetString("lblCOMM_AMOUNT");
                Label l10 = (Label)e.Item.FindControl("lblCOMM_PER");
                l10.Text = Objresources.GetString("lblCOMM_PER");
                Label l11 = (Label)e.Item.FindControl("lblRISK_ID");
                l11.Text = Objresources.GetString("lblRISK_ID");
                Label l12 = (Label)e.Item.FindControl("lblREIN_CEDED");
                l12.Text = Objresources.GetString("lblREIN_CEDED");
                //Label l13 = (Label)e.Item.FindControl("lblTIV");
                //l13.Text = Objresources.GetString("lblTIV");
                Label l14 = (Label)e.Item.FindControl("lblRI_BREAKDOWN");
                l14.Text = Objresources.GetString("lblRI_BREAKDOWN");
                Label l15 = (Label)e.Item.FindControl("lblRATE");
                l15.Text = Objresources.GetString("lblRATE");

            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)  //added by aditya on 17-08-2011 for itrack 1415
            {
                Label l1 = (Label)e.Item.FindControl("lblDT_REIN_PREMIUM");
                TotalReinsurance_Premium += double.Parse(l1.Text);
                Label l2 = (Label)e.Item.FindControl("lblDT_RETENTION_PER");
                TotalRetention_per += double.Parse(l2.Text);
                Label l3 = (Label)e.Item.FindControl("lblDT_COMM_PER");
                TotalComm_per += double.Parse(l3.Text);
                Label l4 = (Label)e.Item.FindControl("lblDT_COMM_AMOUNT");
                TotalComm_Amt += double.Parse(l4.Text);
            }

            if (e.Item.ItemType == ListItemType.Footer)   //added by aditya on 17-08-2011 for itrack 1415
            {
                NfiBaseCurrency.NumberDecimalDigits = 3; //Added by Aditya for TFS BUG # 165
                numberFormatInfo.NumberDecimalDigits = 2; //Added by Aditya for TFS BUG # 165
                Label l1 = (Label)e.Item.FindControl("lblDT_REIN_PREMIUM");
                l1.Text = TotalReinsurance_Premium.ToString("N", numberFormatInfo);
                Label l2 = (Label)e.Item.FindControl("lblDT_RETENTION_PER");
                l2.Text = TotalRetention_per.ToString("N", NfiBaseCurrency);
                Label l3 = (Label)e.Item.FindControl("lblDT_COMM_PER");
                l3.Text = TotalComm_per.ToString("N", NfiBaseCurrency);
                Label l4 = (Label)e.Item.FindControl("lblDT_COMM_AMOUNT");
                l4.Text = TotalComm_Amt.ToString("N", numberFormatInfo);
                Label l16 = (Label)e.Item.FindControl("lblTotal"); //Added by aditya on 18-08-2011 for itrack # 1415
                l16.Text = Objresources.GetString("lblTotal");
            }
            btnsave_act.Visible = false;  //added by aditya on 17-08-2011 for itrack 2705 //Changed by adiya for tfs # 180

        }

        private void PopulateReinsurers()
        {
            ClsPolicyReinsurerInfo objPolicyReinsurerInfo = new ClsPolicyReinsurerInfo();
            objDataSet = objPolicyReinsurerInfo.FetchReinsurers(int.Parse(Request.QueryString["CUSTOMER_ID"]), int.Parse(Request.QueryString["POLICY_ID"]), int.Parse(Request.QueryString["POLICY_VERSION_ID"]));
            cmbREIN_COMAPANY_NAME.Items.Clear();
            cmbREIN_COMAPANY_NAME.DataSource = objDataSet.Tables[0];
            cmbREIN_COMAPANY_NAME.DataTextField = "REIN_COMAPANY_NAME";
            cmbREIN_COMAPANY_NAME.DataValueField = "REIN_COMAPANY_ID";
            cmbREIN_COMAPANY_NAME.DataBind();
            cmbREIN_COMAPANY_NAME.Items.Insert(0, new ListItem("", ""));
            cmbREIN_COMAPANY_NAME.SelectedIndex = -1;


            cmbDISREGARD_RI_CONTRACT.Items.Clear();
            cmbDISREGARD_RI_CONTRACT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbDISREGARD_RI_CONTRACT.DataTextField = "LookupDesc";
            cmbDISREGARD_RI_CONTRACT.DataValueField = "LookupID";
            cmbDISREGARD_RI_CONTRACT.DataBind();
            cmbDISREGARD_RI_CONTRACT.SelectedIndex = 2;//Disregard RI contract default select to No;


        }
        private void GridBind(List<ClsPolicyReinsurerInfo> Reinsurers)
        {
            if (hidAction.Value != "DELETE" && hidAction.Value != "ADDNEW")
            {
                //ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                //ds = objGeneralInformation.GetReinsurerInfo(int.Parse(Request.QueryString["CUSTOMER_ID"]), int.Parse(Request.QueryString["POLICY_ID"]), int.Parse(Request.QueryString["POLICY_VERSION_ID"]));
                Reinsurers = ClsGeneralInformation.GetReinsurer(int.Parse(Request.QueryString["CUSTOMER_ID"]), int.Parse(Request.QueryString["POLICY_ID"]), int.Parse(Request.QueryString["POLICY_VERSION_ID"]));
            }
            grdReinsurer.DataSource = Reinsurers;
            grdReinsurer.DataBind();
            SetPageModelObjects(Reinsurers);
            if (grdReinsurer.Rows.Count == 0)
            {
                btnSave.Style.Add("display", "none");
                btndelete.Style.Add("display", "none");
            }
        }

        private void SetCaption()
        {
            capREIN_COMAPANY_NAME.Text = Objresources.GetString("cmbREIN_COMAPANY_NAME");
            lblManHeader.Text = Objresources.GetString("lblManHeader");
            btnSave.Text = Objresources.GetString("btnSave");
            btndelete.Text = Objresources.GetString("btndelete");
            btnSelect.Text = Objresources.GetString("btnSelect");
            confirmmessage = Objresources.GetString("confirmmessage");
            selectChk = Objresources.GetString("selectChk");
            lblTitle.Text = Objresources.GetString("lblTitle");
            grdActPolicyReinInstallmentDetails.Columns[0].HeaderText = Objresources.GetString("REIN_COMAPANY_NAME"); //added by aditya on 17-08-2011 for itrack 2705
            grdActPolicyReinInstallmentDetails.Columns[1].HeaderText = Objresources.GetString("CONTRACT_NUMBER");
            grdActPolicyReinInstallmentDetails.Columns[2].HeaderText = Objresources.GetString("INSTALLMENT_NO");
            grdActPolicyReinInstallmentDetails.Columns[3].HeaderText = Objresources.GetString("INSTALLMENT_AMOUNT");
            grdActPolicyReinInstallmentDetails.Columns[4].HeaderText = Objresources.GetString("INSTALLMENT_EFFECTIVE_DATE");
            grdActPolicyReinInstallmentDetails.Columns[5].HeaderText = Objresources.GetString("RELEASED_STATUS");  //Added till here
            btnsave_act.Text = Objresources.GetString("btnsave_act");
            //Added By Lalit for tfs # 
            capDISREGARD_RI_CONTRACT.Text = Objresources.GetString("cmbDISREGARD_RI_CONTRACT");
        }

        private void AddGridrow(string REINSURER_NAME, int REINSURANCE_ID)
        {

            Reinsurers = (List<ClsPolicyReinsurerInfo>)GetPageModelObjects();
            ClsPolicyReinsurerInfo objReinsurer = new ClsPolicyReinsurerInfo();
            objReinsurer.REINSURANCE_ID.CurrentValue = 0;
            objReinsurer.COMPANY_ID.CurrentValue = REINSURANCE_ID;
            objReinsurer.REINSURER_NAME.CurrentValue = REINSURER_NAME;
            objReinsurer.CONTRACT_FACULTATIVE.CurrentValue = 0;
            objReinsurer.CONTRACT.CurrentValue = 0;
            objReinsurer.REINSURANCE_CEDED.CurrentValue = 0;
            objReinsurer.REINSURANCE_COMMISSION.CurrentValue = 0;
            objReinsurer.REINSURER_NUMBER.CurrentValue = "0";//ashish
            objReinsurer.ACTION = enumAction.Insert;
            objReinsurer.COMM_AMOUNT.CurrentValue = 0;  //Added by Aditya for tfs bug # 177
            objReinsurer.LAYER_AMOUNT.CurrentValue = 0;
            objReinsurer.REIN_PREMIUM.CurrentValue = 0; //Added till here
            Reinsurers.Add(objReinsurer);
            hidAction.Value = "ADDNEW";
            GridBind(Reinsurers);
            hidAction.Value = "";
            exists = true;
        }

        #endregion

        #region Control events
        protected void grdReinsurer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            numberFormatInfo.NumberDecimalDigits = 2;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label capREINSURER_NAME = (Label)e.Row.FindControl("capREINSURER_NAME");
                capREINSURER_NAME.Text = Objresources.GetString("capREINSURER_NAME");

                Label capCONTRACT_FACULTATIVE = (Label)e.Row.FindControl("capCONTRACT_FACULTATIVE");
                capCONTRACT_FACULTATIVE.Text = Objresources.GetString("cmbCONTRACT_FACULTATIVE");

                Label capCONTRACT = (Label)e.Row.FindControl("capCONTRACT");
                capCONTRACT.Text = Objresources.GetString("cmbCONTRACT");

                Label capREINSURANCE_CEDED = (Label)e.Row.FindControl("capREINSURANCE_CEDED");
                capREINSURANCE_CEDED.Text = Objresources.GetString("txtREINSURANCE_CEDED");

                Label capREINSURANCE_COMMISSION = (Label)e.Row.FindControl("capREINSURANCE_COMMISSION");
                capREINSURANCE_COMMISSION.Text = Objresources.GetString("txtREINSURANCE_COMMISSION");

                Label capREINSURER_NUMBER = (Label)e.Row.FindControl("capREINSURER_NUMBER");
                capREINSURER_NUMBER.Text = Objresources.GetString("txtREINSURER_NUMBER");//ashish

                Label capMAX_NO_INSTALLMENT = (Label)e.Row.FindControl("capMAX_NO_INSTALLMENT");  // Added by Aditya for TFS BUG # 2514
                capMAX_NO_INSTALLMENT.Text = Objresources.GetString("cmbMAX_NO_INSTALLMENT");

                Label capRISK_ID = (Label)e.Row.FindControl("capRISK_ID"); // Added by Aditya for TFS BUG # 2514
                capRISK_ID.Text = Objresources.GetString("cmbRISK_ID");

                Label capCOMM_AMOUNT = (Label)e.Row.FindControl("capCOMM_AMOUNT");  //Added by Aditya for tfs bug # 177
                capCOMM_AMOUNT.Text = Objresources.GetString("txtCOMM_AMOUNT");

                Label capLAYER_AMOUNT = (Label)e.Row.FindControl("capLAYER_AMOUNT");
                capLAYER_AMOUNT.Text = Objresources.GetString("txtLAYER_AMOUNT");

                Label capREIN_PREMIUM = (Label)e.Row.FindControl("capREIN_PREMIUM");
                capREIN_PREMIUM.Text = Objresources.GetString("txtREIN_PREMIUM");  //Added till here

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList cmbCONTRACT_FACULTATIVE = (DropDownList)e.Row.FindControl("cmbCONTRACT_FACULTATIVE");
                cmbCONTRACT_FACULTATIVE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CONRTP");
                cmbCONTRACT_FACULTATIVE.DataTextField = "LookupDesc";
                cmbCONTRACT_FACULTATIVE.DataValueField = "LookupID";
                cmbCONTRACT_FACULTATIVE.DataBind();
                cmbCONTRACT_FACULTATIVE.Items.Insert(0, "");


                int intCONTRACT_FACULTATIVE = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CONTRACT_FACULTATIVE.CurrentValue"));
                cmbCONTRACT_FACULTATIVE.SelectedIndex = cmbCONTRACT_FACULTATIVE.Items.IndexOf(cmbCONTRACT_FACULTATIVE.Items.FindByValue(intCONTRACT_FACULTATIVE.ToString()));
                if (cmbCONTRACT_FACULTATIVE.SelectedValue == "Contract")
                {
                    DropDownList cmbCONTRACT = (DropDownList)e.Row.FindControl("cmbCONTRACT");
                    cmbCONTRACT.DataSource = objReinsuranceInformation.GetReinsurance_ContractType(int.Parse(Request.QueryString["CUSTOMER_ID"]), int.Parse(Request.QueryString["POLICY_ID"]), int.Parse(Request.QueryString["POLICY_VERSION_ID"]));
                    cmbCONTRACT.DataTextField = "CONTRACT_TYPE_DESC";
                    cmbCONTRACT.DataValueField = "CONTRACTTYPEID";
                    cmbCONTRACT.DataBind();
                    cmbCONTRACT.Items.Insert(0, "");
                    int intCONTRACT = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CONTRACT.CurrentValue"));
                    cmbCONTRACT.SelectedIndex = cmbCONTRACT.Items.IndexOf(cmbCONTRACT.Items.FindByValue(intCONTRACT.ToString()));
                }
                Label lbltext = (Label)e.Row.FindControl("capREINSURANCE_ID");
                //CheckBox chkbox = (CheckBox)e.Row.Cells[0].FindControl("chkSELECT");
                //if (lbltext.Text != "0" || lbltext.Text != "")
                //{
                //    chkbox.Checked = true;
                //}
                //else
                //{
                //    chkbox.Checked = false;

                //}

                ((RequiredFieldValidator)e.Row.FindControl("rfvREINSURANCE_CEDED")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("493");
                ((RequiredFieldValidator)e.Row.FindControl("rfvREINSURANCE_COMMISSION")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("493");
                ((RequiredFieldValidator)e.Row.FindControl("rfvREINSURER_NUMBER")).ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");//ashish
                //((RequiredFieldValidator)e.Row.FindControl("rfvMAX_NO_INSTALLMENT")).ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");//Added by Aditya for tfs bug 2514
                ((RequiredFieldValidator)e.Row.FindControl("rfvRISK_ID")).ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11");//Added by Aditya for tfs bug 2514
                ((RegularExpressionValidator)e.Row.FindControl("revREINSURANCE_CEDED")).ValidationExpression = aRegExpDoublePositiveWithZero;
                ((RegularExpressionValidator)e.Row.FindControl("revREINSURANCE_CEDED")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
                ((CustomValidator)e.Row.FindControl("csvREINSURANCE_CEDED")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_8");

                ((RegularExpressionValidator)e.Row.FindControl("revREINSURANCE_COMMISSION")).ValidationExpression = aRegExpDoublePositiveWithZero;
                ((RegularExpressionValidator)e.Row.FindControl("revREINSURANCE_COMMISSION")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
                ((CustomValidator)e.Row.FindControl("csvREINSURANCE_COMMISSION")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_8");
                ((RegularExpressionValidator)e.Row.FindControl("revCOMM_AMOUNT")).ValidationExpression = aRegExpBaseCurrencyformat;  //Added by Aditya for tfs bug # 177
                ((RegularExpressionValidator)e.Row.FindControl("revCOMM_AMOUNT")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
                ((RegularExpressionValidator)e.Row.FindControl("revLAYER_AMOUNT")).ValidationExpression = aRegExpBaseCurrencyformat;
                ((RegularExpressionValidator)e.Row.FindControl("revLAYER_AMOUNT")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
                ((RegularExpressionValidator)e.Row.FindControl("revREIN_PREMIUM")).ValidationExpression = aRegExpBaseCurrencyformat;
                ((RegularExpressionValidator)e.Row.FindControl("revREIN_PREMIUM")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

                ((CustomValidator)e.Row.FindControl("csvCOMM_AMOUNT")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2065");  
                ((CustomValidator)e.Row.FindControl("csvLAYER_AMOUNT")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2065");
                ((CustomValidator)e.Row.FindControl("csvREIN_PREMIUM")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2065"); //Added till here
                //((RegularExpressionValidator)e.Row.FindControl("revREINSURER_NUMBER")).ValidationExpression = aRegExpDoublePositiveWithZero;//ashish
                //((RegularExpressionValidator)e.Row.FindControl("revREINSURER_NUMBER")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
                //((CustomValidator)e.Row.FindControl("csvREINSURER_NUMBER")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_8");
                ((RequiredFieldValidator)e.Row.FindControl("rfvCONTRACT_FACULTATIVE")).ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");

                if (e.Row.FindControl("txtREINSURANCE_CEDED") != null)
                {
                    ((TextBox)e.Row.FindControl("txtREINSURANCE_CEDED")).Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value,2)");
                    ((TextBox)e.Row.FindControl("txtREINSURANCE_CEDED")).Attributes.Add("onChange", "javascript:this.value=formatAmount(this.value,2)");
                }
                if (e.Row.FindControl("txtREINSURANCE_COMMISSION") != null)
                {
                    ((TextBox)e.Row.FindControl("txtREINSURANCE_COMMISSION")).Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value,2)");
                    ((TextBox)e.Row.FindControl("txtREINSURANCE_COMMISSION")).Attributes.Add("onChange", "javascript:this.value=formatAmount(this.value,2)");
                }

                if (e.Row.FindControl("txtCOMM_AMOUNT") != null)
                {
                    ((TextBox)e.Row.FindControl("txtCOMM_AMOUNT")).Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value)");
                    ((TextBox)e.Row.FindControl("txtCOMM_AMOUNT")).Attributes.Add("onChange", "javascript:this.value=formatAmount(this.value)");
                }

                if (e.Row.FindControl("txtLAYER_AMOUNT") != null)
                {
                    ((TextBox)e.Row.FindControl("txtLAYER_AMOUNT")).Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value)");
                    ((TextBox)e.Row.FindControl("txtLAYER_AMOUNT")).Attributes.Add("onChange", "javascript:this.value=formatAmount(this.value)");
                }

                if (e.Row.FindControl("txtREIN_PREMIUM") != null)
                {
                    ((TextBox)e.Row.FindControl("txtREIN_PREMIUM")).Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value)");
                    ((TextBox)e.Row.FindControl("txtREIN_PREMIUM")).Attributes.Add("onChange", "javascript:this.value=formatAmount(this.value)");
                }
                //if (e.Row.FindControl("txtREINSURER_NUMBER") != null)
                //((TextBox)e.Row.FindControl("txtREINSURER_NUMBER")).Attributes.Add("onblur", "javascript:this.value=formatRate(this.value)");//ashish

                DropDownList cmbMAX_NO_INSTALLMENT = (DropDownList)e.Row.FindControl("cmbMAX_NO_INSTALLMENT"); // Added by Aditya for TFS BUG # 2514              

                //cmbMAX_NO_INSTALLMENT.Items.Add("");  // Added by Aditya for TFS BUG # 2514
                cmbMAX_NO_INSTALLMENT.Items.Add("1");
                cmbMAX_NO_INSTALLMENT.Items.Add("2");
                cmbMAX_NO_INSTALLMENT.Items.Add("3");
                cmbMAX_NO_INSTALLMENT.Items.Add("4");
                cmbMAX_NO_INSTALLMENT.Items.Add("5");
                cmbMAX_NO_INSTALLMENT.Items.Add("6");
                cmbMAX_NO_INSTALLMENT.Items.Add("7");
                cmbMAX_NO_INSTALLMENT.Items.Add("8");
                cmbMAX_NO_INSTALLMENT.Items.Add("9");
                cmbMAX_NO_INSTALLMENT.Items.Add("10");
                cmbMAX_NO_INSTALLMENT.Items.Add("11");
                cmbMAX_NO_INSTALLMENT.Items.Add("12");

                int intMAX_NO_INSTALLMENT = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MAX_NO_INSTALLMENT.CurrentValue")); // Added by Aditya for TFS BUG # 2514
                cmbMAX_NO_INSTALLMENT.SelectedValue = intMAX_NO_INSTALLMENT.ToString(); // Added by Aditya for TFS BUG # 2514

                DropDownList cmbRISK_ID = (DropDownList)e.Row.FindControl("cmbRISK_ID"); // Added by Aditya for TFS BUG # 2514              
                ClsPolicyReinsurerInfo objPolicyReinsurerInfo = new ClsPolicyReinsurerInfo();
                DataSet ds = new DataSet();
                ds = objPolicyReinsurerInfo.FetchRiskDetails(int.Parse(GetLOBID()), int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()));
                cmbRISK_ID.DataSource = ds.Tables[0];
                cmbRISK_ID.DataTextField = "DESCRIPTION";
                cmbRISK_ID.DataValueField = "RISK_ID";
                cmbRISK_ID.DataBind();
                cmbRISK_ID.Items.Insert(0, new ListItem("", ""));

                int intRISK_ID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RISK_ID.CurrentValue")); // Added by Aditya for TFS BUG # 2514
                cmbRISK_ID.SelectedValue = intRISK_ID.ToString(); // Added by Aditya for TFS BUG # 2514

               

                double dblCOMM_AMOUNT = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "COMM_AMOUNT.CurrentValue")); // Added by Aditya for TFS BUG # 177
                ((TextBox)e.Row.FindControl("txtCOMM_AMOUNT")).Text= dblCOMM_AMOUNT.ToString("N", numberFormatInfo);

                double dblLAYER_AMOUNT = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "LAYER_AMOUNT.CurrentValue")); // Added by Aditya for TFS BUG # 177
                ((TextBox)e.Row.FindControl("txtLAYER_AMOUNT")).Text = dblLAYER_AMOUNT.ToString("N", numberFormatInfo);

                double dblREIN_PREMIUM = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "REIN_PREMIUM.CurrentValue")); // Added by Aditya for TFS BUG # 177
                ((TextBox)e.Row.FindControl("txtREIN_PREMIUM")).Text = dblREIN_PREMIUM.ToString("N", numberFormatInfo);

                

            }

        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            // _CheckExists();
            AddGridrow(cmbREIN_COMAPANY_NAME.SelectedItem.Text, int.Parse(cmbREIN_COMAPANY_NAME.SelectedValue));
            btnSave.Style.Add("display", "inline");
            btndelete.Style.Add("display", "inline");
        }

        private bool _CheckExists()
        {
            exists = false;
            if (cmbREIN_COMAPANY_NAME.SelectedIndex != -1 && cmbREIN_COMAPANY_NAME.SelectedIndex > 0)
            {
                foreach (GridViewRow row in grdReinsurer.Rows)
                {
                    string strCOMPANY_ID = ((Label)row.FindControl("capCOMPANY_ID")).Text.Trim(); ;
                    DropDownList DDL = ((DropDownList)row.FindControl("cmbRISK_ID"));
                    string strRISK_ID = DDL.SelectedValue;
                    foreach (GridViewRow row1 in grdReinsurer.Rows)
                    {
                        if (row1.RowIndex > row.RowIndex)
                        {
                            string strCOMPANY_ID1 = ((Label)row1.FindControl("capCOMPANY_ID")).Text.Trim(); ;
                            DropDownList DDL1 = ((DropDownList)row1.FindControl("cmbRISK_ID"));
                            string strRISK_ID1 = DDL1.SelectedValue;
                            if (strCOMPANY_ID1 == strCOMPANY_ID && strRISK_ID1 == strRISK_ID)
                            {
                                exists = true;
                                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "6"); //"Reinsurer Already Added";
                                break;
                            }

                        }
                    }
                }
            }
            else
            {
                lblMessage.Text = "";
            }
            return exists;
            //exists = true;
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            //ArOld.Clear();
            ArrayList arlObjReinsurance = new ArrayList();
            int retunvalue = 0;
            Reinsurers = (List<ClsPolicyReinsurerInfo>)GetPageModelObjects();
            //ClsPolicyReinsurerInfo objReinsurer = new ClsPolicyReinsurerInfo();
            ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
            foreach (GridViewRow row in grdReinsurer.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    CheckBox chkbox = (CheckBox)row.Cells[0].FindControl("chkSELECT");
                    if (chkbox.Checked)
                    {
                        hidAction.Value = "DELETE";

                        Label lb = (Label)row.Cells[1].FindControl("capREINSURANCE_ID");
                        if (lb.Text != null)
                        {
                            int CompanyId = Convert.ToInt32(((Label)row.FindControl("capCOMPANY_ID")).Text);
                            ClsPolicyReinsurerInfo objReinsurer = Reinsurers.Select((ob, id) => new { reinsurer = ob, REINID = id }).Where(ob => ob.reinsurer.REINSURANCE_ID.CurrentValue == int.Parse(lb.Text.Trim()) && ob.reinsurer.COMPANY_ID.CurrentValue == CompanyId).Select(ob => ob.reinsurer).First();
                            if (lb.Text != "0")
                            {
                                objReinsurer.REINSURANCE_ID.CurrentValue = int.Parse(lb.Text);
                                objReinsurer.POLICY_ID.CurrentValue = Convert.ToInt32(GetPolicyID());
                                objReinsurer.POLICY_VERSION_ID.CurrentValue = Convert.ToInt32(GetPolicyVersionID());
                                objReinsurer.CUSTOMER_ID.CurrentValue = Convert.ToInt32(GetCustomerID());
                                objReinsurer.CREATED_BY.CurrentValue = Convert.ToInt32(GetUserId());
                                objReinsurer.ACTION = enumAction.Delete;
                                arlObjReinsurance.Add(objReinsurer);
                                objReinsurer.RequiredTransactionLog = false;
                                //Reinsurers.Remove(objReinsurer);
                                //retunvalue = objGeneralInformation.DeleteReinsurer(objReinsurer);
                            }

                        }
                        //Label CompanyId = (Label)row.FindControl("capCOMPANY_ID");
                        //if (CompanyId != null)
                        //{
                        //    int count=ds.Tables[0].Rows.Count-1;
                        //    for (; count >= 0; count--)
                        //    {
                        //        if (ds.Tables[0].Rows[count]["COMPANY_ID"].ToString() == CompanyId.Text)
                        //        {

                        //            ds.Tables[0].Rows[count].Delete();
                        //            retunvalue = 1;
                        //        }
                        //        ds.AcceptChanges();
                        //    }
                        //}

                    }
                    else
                    {
                        //TextBox Commission_Percent = (TextBox)row.Cells[4].FindControl("txtCOMMISSION");
                        //ArOld.Add(Commission_Percent.Text);
                    }
                }
            }
            if (arlObjReinsurance.Count > 0)
            {
                retunvalue = objGeneralInformation.SaveReinsurer(arlObjReinsurance);

                //Reinsurers.Remove(arlObjReinsurance);

                if (retunvalue > 0)
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("5");
                    base.OpenEndorsementDetails();
                }
                else
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_1");
                }

            }
            hidAction.Value = "";
            GridBind(Reinsurers);

        }

        protected void cmbCONTRACT_SelectedIndexChanged(object sender, EventArgs e)
        {
            numberFormatInfo.NumberDecimalDigits = 2;
            DropDownList Contract = (DropDownList)sender;
            foreach (GridViewRow row in grdReinsurer.Rows)
            {
                Control ctrl = row.FindControl("cmbCONTRACT") as DropDownList;
                if (ctrl != null)
                {
                    DropDownList ddl1 = (DropDownList)ctrl;
                    //Comparing ClientID of the dropdown with sender
                    if (Contract.ClientID == ddl1.ClientID)
                    {
                        //ClientID is match so find the Textbox 
                        //control bind it with some dropdown data.
                        if (ddl1.SelectedIndex != 0)
                        {
                            ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                            Ds = objGeneralInformation.GetValues(Convert.ToInt32(ddl1.SelectedValue));
                            TextBox txtREINSURANCE_CEDED = row.FindControl("txtREINSURANCE_CEDED") as TextBox;
                            if (Ds.Tables[0].Rows.Count > 0)
                            { txtREINSURANCE_CEDED.Text = double.Parse(objGeneralInformation.GetValues(Convert.ToInt32(ddl1.SelectedValue)).Tables[0].Rows[0][0].ToString()).ToString("N", numberFormatInfo); }
                            TextBox txtREINSURANCE_COMMISSION = row.FindControl("txtREINSURANCE_COMMISSION") as TextBox;
                            if (Ds.Tables[1].Rows.Count > 0)
                            { txtREINSURANCE_COMMISSION.Text = double.Parse(objGeneralInformation.GetValues(Convert.ToInt32(ddl1.SelectedValue)).Tables[1].Rows[0][0].ToString()).ToString("N", numberFormatInfo); }
                            TextBox txtREINSURER_NUMBER = row.FindControl("txtREINSURER_NUMBER") as TextBox;
                            if (Ds.Tables[1].Rows.Count > 0)
                            {
                                txtREINSURER_NUMBER.Text = objGeneralInformation.GetValues(Convert.ToInt32(ddl1.SelectedValue)).Tables[1].Rows[0][0].ToString();
                            }//ashish                            
                            break;
                        }
                    }
                }

            }
        }

        private void BindGrid()  //Added by Aditya for TFS bug # 2705
        {
            try
            {
                ClsPolicyReinsurerInfo objReinsurerInfo = new ClsPolicyReinsurerInfo();

                if (dsTemp != null)
                {
                    dsTemp = null;
                }

                if (dsTempTable != null)
                {
                    dsTempTable = null;
                }

                dsTemp = objReinsurerInfo.FetchACT_POLICY_REIN_INSTALLMENT_DETAILS(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()));

                if (dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
                {
                    grdActPolicyReinInstallmentDetails.DataSource = dsTemp.Tables[0];

                }
                else
                {
                    grdActPolicyReinInstallmentDetails.DataSource = null;

                }
                grdActPolicyReinInstallmentDetails.DataBind();

            }

            catch
            {

            }
            finally
            {
                if (dsTemp != null)
                    dsTemp.Dispose();
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.btnsave_act.Click += new System.EventHandler(this.btnsave_act_Click);
            this.btnSave_Contract.Click += new System.EventHandler(this.btnSave_Contract_Click); //Added by Aditya for tfs bug # 180

        }
        #endregion

        private void btnSave_Contract_Click(object sender, System.EventArgs e) //Added by Aditya for tfs bug # 180
        {
            ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
            String CONTRACT_APPLCABLE = cmbDISREGARD_RI_CONTRACT.SelectedValue.ToString();
            int RetVal = objGeneralInformation.UpdatePolicyRIContactInfo(Convert.ToInt32(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), int.Parse(CONTRACT_APPLCABLE));
        }

        private void btnsave_act_Click(object sender, System.EventArgs e)
        {
            //Saving form values 
            SaveFormValues();
        }

        private void SaveFormValues()
        {
            try
            {
                int intRetVal;
                //Retreiving the form values into model class object               

                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                objGeneralInformation = new ClsGeneralInformation();

                foreach (GridViewRow row in grdActPolicyReinInstallmentDetails.Rows)
                {
                    ClsPolicyReinsurerInfo objReinsurerInfo = new ClsPolicyReinsurerInfo();
                    rein_id = grdActPolicyReinInstallmentDetails.DataKeys[row.RowIndex].Value.ToString();
                    objReinsurerInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
                    objReinsurerInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
                    objReinsurerInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());
                    objReinsurerInfo.IDEN_ROW_ID.CurrentValue = int.Parse(rein_id);
                    TextBox txtREIN_INSTALLMENT_NO = row.FindControl("txtREIN_INSTALLMENT_NO") as TextBox;
                    if (txtREIN_INSTALLMENT_NO.Text != "")
                    {
                        objReinsurerInfo.REIN_INSTALLMENT_NO.CurrentValue = Convert.ToString(txtREIN_INSTALLMENT_NO.Text);
                    }
                    intRetVal = objGeneralInformation.UpdateACT_POLICY_REIN_INSTALLMENT_DETAILS(objReinsurerInfo);

                    if (intRetVal > 0)
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "31");//base.RECORD_UPDATED;
                        BindGrid();
                    }
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");//base.RECORD_UPDATE_FAILED;
                    }
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + "\n Try again!";
                lblMessage.Visible = true;
                //Publishing the exception using the static method of Exception manager class
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

            }
            finally
            {

            }
        }         // Added till here

        protected void cmbCONTRACT_FACULTATIVE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList CONTRACT_FACULTATIVE = (DropDownList)sender;
            foreach (GridViewRow row in grdReinsurer.Rows)
            {
                Control ctrl = row.FindControl("cmbCONTRACT_FACULTATIVE") as DropDownList;
                if (ctrl != null)
                {
                    DropDownList ddl1 = (DropDownList)ctrl;
                    //Comparing ClientID of the dropdown with sender
                    if (CONTRACT_FACULTATIVE.ClientID == ddl1.ClientID)
                    {
                        DropDownList cmbCONTRACT = row.FindControl("cmbCONTRACT") as DropDownList;
                        if (CONTRACT_FACULTATIVE.SelectedValue == "Contract")
                        {
                            cmbCONTRACT.DataSource = objReinsuranceInformation.GetReinsurance_ContractType(int.Parse(Request.QueryString["CUSTOMER_ID"]), int.Parse(Request.QueryString["POLICY_ID"]), int.Parse(Request.QueryString["POLICY_VERSION_ID"]));
                            cmbCONTRACT.DataTextField = "CONTRACT_TYPE_DESC";
                            cmbCONTRACT.DataValueField = "CONTRACTTYPEID";
                            cmbCONTRACT.DataBind();
                            cmbCONTRACT.Items.Insert(0, "");
                            cmbCONTRACT.SelectedIndex = 0;
                        }
                        else
                        {
                            if (cmbCONTRACT.Items.Count > 0)
                            {
                                cmbCONTRACT.Items.Clear();
                                cmbCONTRACT.Items.Insert(0, "");
                                TextBox txtREINSURANCE_CEDED = row.FindControl("txtREINSURANCE_CEDED") as TextBox;
                                txtREINSURANCE_CEDED.Text = "";
                                TextBox txtREINSURANCE_COMMISSION = row.FindControl("txtREINSURANCE_COMMISSION") as TextBox;
                                txtREINSURANCE_COMMISSION.Text = "";
                                TextBox txtREINSURER_NUMBER = row.FindControl("txtREINSURER_NUMBER") as TextBox;
                                txtREINSURER_NUMBER.Text = "";//ashish
                            }
                        }
                    }
                }
            }
        }

        #endregion
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ArrayList arlObjReinsurance = new ArrayList();
            //check if duplicate recode add in grid ;
            if (_CheckExists()) { return; }


            try
            {
                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                Reinsurers = (List<ClsPolicyReinsurerInfo>)GetPageModelObjects();
                lblMessage.Text = "";
                foreach (GridViewRow row in grdReinsurer.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {

                        CheckBox chkbox = (CheckBox)row.Cells[0].FindControl("chkSELECT");
                        if (chkbox.Checked)
                        {
                            Label lb = (Label)row.FindControl("capREINSURANCE_ID");
                            if (lb.Text.Trim() == "") lb.Text = "0";
                            int CompanyId = Convert.ToInt32(((Label)row.FindControl("capCOMPANY_ID")).Text);
                            ClsPolicyReinsurerInfo objPolicyReinsurerInfo = Reinsurers.Select((ob, id) => new { reinsurer = ob, REINID = id }).Where(ob => ob.reinsurer.REINSURANCE_ID.CurrentValue == int.Parse(lb.Text.Trim()) && ob.reinsurer.COMPANY_ID.CurrentValue == CompanyId).Select(ob => ob.reinsurer).First();
                            objPolicyReinsurerInfo.CUSTOMER_ID.CurrentValue = Convert.ToInt32(GetCustomerID());
                            objPolicyReinsurerInfo.POLICY_ID.CurrentValue = Convert.ToInt32(GetPolicyID());
                            objPolicyReinsurerInfo.POLICY_VERSION_ID.CurrentValue = Convert.ToInt32(GetPolicyVersionID());

                            objPolicyReinsurerInfo.IS_ACTIVE.CurrentValue = "Y";

                            objPolicyReinsurerInfo.COMPANY_ID.CurrentValue = CompanyId;

                            if (((DropDownList)row.FindControl("cmbCONTRACT_FACULTATIVE")).SelectedIndex > 0)
                            { objPolicyReinsurerInfo.CONTRACT_FACULTATIVE.CurrentValue = Convert.ToInt32(((DropDownList)row.FindControl("cmbCONTRACT_FACULTATIVE")).SelectedValue); }

                            if (((DropDownList)row.FindControl("cmbCONTRACT")).SelectedIndex > 0)
                            { objPolicyReinsurerInfo.CONTRACT.CurrentValue = Convert.ToInt32(((DropDownList)row.FindControl("cmbCONTRACT")).SelectedValue); }

                            if (((TextBox)row.FindControl("txtREINSURANCE_CEDED")).Text != "")
                            { objPolicyReinsurerInfo.REINSURANCE_CEDED.CurrentValue = ConvertEbixDoubleValue(((TextBox)row.FindControl("txtREINSURANCE_CEDED")).Text); }

                            if (((TextBox)row.FindControl("txtREINSURANCE_COMMISSION")).Text != "")
                            { objPolicyReinsurerInfo.REINSURANCE_COMMISSION.CurrentValue = ConvertEbixDoubleValue(((TextBox)row.FindControl("txtREINSURANCE_COMMISSION")).Text); }

                            if (((TextBox)row.FindControl("txtREINSURER_NUMBER")).Text != "")
                            { objPolicyReinsurerInfo.REINSURER_NUMBER.CurrentValue = ((TextBox)row.FindControl("txtREINSURER_NUMBER")).Text.ToString(); }

                            objPolicyReinsurerInfo.CREATED_BY.CurrentValue = Convert.ToInt32(GetUserId());//ashish

                            if (((DropDownList)row.FindControl("cmbMAX_NO_INSTALLMENT")).SelectedIndex > 0)  // Added by Aditya for TFS BUG # 2514
                            { objPolicyReinsurerInfo.MAX_NO_INSTALLMENT.CurrentValue = Convert.ToInt32(((DropDownList)row.FindControl("cmbMAX_NO_INSTALLMENT")).SelectedValue); }

                            if (((DropDownList)row.FindControl("cmbRISK_ID")).SelectedIndex > 0)  // Added by Aditya for TFS BUG # 2514
                            { objPolicyReinsurerInfo.RISK_ID.CurrentValue = Convert.ToInt32(((DropDownList)row.FindControl("cmbRISK_ID")).SelectedValue); }

                            if (((TextBox)row.FindControl("txtCOMM_AMOUNT")).Text != "" && ((TextBox)row.FindControl("txtCOMM_AMOUNT")).Text != null)   //Added by Aditya for tfs bug # 177
                            { objPolicyReinsurerInfo.COMM_AMOUNT.CurrentValue = ConvertEbixDoubleValue(((TextBox)row.FindControl("txtCOMM_AMOUNT")).Text); }

                            if (((TextBox)row.FindControl("txtLAYER_AMOUNT")).Text != "" && ((TextBox)row.FindControl("txtLAYER_AMOUNT")).Text != null)
                            { objPolicyReinsurerInfo.LAYER_AMOUNT.CurrentValue = ConvertEbixDoubleValue(((TextBox)row.FindControl("txtLAYER_AMOUNT")).Text); }

                            if (((TextBox)row.FindControl("txtREIN_PREMIUM")).Text != "" && ((TextBox)row.FindControl("txtREIN_PREMIUM")).Text != null)
                            { objPolicyReinsurerInfo.REIN_PREMIUM.CurrentValue = ConvertEbixDoubleValue(((TextBox)row.FindControl("txtREIN_PREMIUM")).Text); }  //Added till here

                            if (lb.Text == "0" || lb.Text == "")//Save
                            {

                                objPolicyReinsurerInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToShortDateString());
                                objPolicyReinsurerInfo.ACTION = enumAction.Insert;
                                //int intRetval = objGeneralInformation.SaveReinsurer(objPolicyReinsurerInfo);
                                //GridBind();
                                /*if (intRetval > 0)
                                {
                                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("29");
                                }
                                else
                                {
                                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_4");
                                }*/
                                arlObjReinsurance.Add(objPolicyReinsurerInfo);

                            }

                            else//update
                            {

                                objPolicyReinsurerInfo.REINSURANCE_ID.CurrentValue = Convert.ToInt32(lb.Text);
                                objPolicyReinsurerInfo.MODIFIED_BY.CurrentValue = Convert.ToInt32(GetUserId());
                                objPolicyReinsurerInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToShortDateString());
                                objPolicyReinsurerInfo.ACTION = enumAction.Update;
                                objPolicyReinsurerInfo.RequiredTransactionLog = false;
                                //int intRetval = objGeneralInformation.UpdateReinsurer(objPolicyReinsurerInfo);
                                /*if (intRetval > 0)
                                {
                                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31");
                                }
                                else
                                {
                                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("");
                                }*/
                                arlObjReinsurance.Add(objPolicyReinsurerInfo);

                            }

                        }

                    }
                }
                if (arlObjReinsurance.Count > 0)
                {
                    int intRetval = objGeneralInformation.SaveReinsurer(arlObjReinsurance);
                    if (intRetval > 0)
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("29");
                    }
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_4");
                    }

                    
                }
                GridBind(Reinsurers);
                base.OpenEndorsementDetails();

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }


        }

    }
}
