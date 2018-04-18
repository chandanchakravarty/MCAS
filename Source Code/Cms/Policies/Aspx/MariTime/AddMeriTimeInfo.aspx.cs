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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.CmsWeb.Controls;
using Cms.Model.Policy;


namespace Cms.Policies.Aspx.MariTime
{
    public partial class AddMeriTimeInfo : Cms.Policies.policiesbase
    {
        #region Adding webcontrols on page
        protected System.Web.UI.WebControls.Label lblDelete;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capNAME_OF_VESSEL;
        protected System.Web.UI.WebControls.TextBox txtNAME_OF_VESSEL;
        protected System.Web.UI.WebControls.Label capTYPE_OF_VESSEL;
        protected System.Web.UI.WebControls.TextBox txtTYPE_OF_VESSEL;
        protected System.Web.UI.WebControls.Label capMANUFACTURE_YEAR;
        protected System.Web.UI.WebControls.TextBox txtMANUFACTURE_YEAR;
        protected System.Web.UI.WebControls.Label capMANUFACTURER;
        protected System.Web.UI.WebControls.TextBox txtMANUFACTURER;
        protected System.Web.UI.WebControls.Label capBUILDER;
        protected System.Web.UI.WebControls.TextBox txtBUILDER;
        protected System.Web.UI.WebControls.Label capCONSTRUCTION;
        protected System.Web.UI.WebControls.TextBox txtCONSTRUCTION;
        protected System.Web.UI.WebControls.Label capPROPULSION;
        protected System.Web.UI.WebControls.TextBox txtPROPULSION;
        protected System.Web.UI.WebControls.Label capCLASSIFICATION;
        protected System.Web.UI.WebControls.TextBox txtCLASSIFICATION;
        protected System.Web.UI.WebControls.Label capLOCAL_OPERATION;
        protected System.Web.UI.WebControls.TextBox txtLOCAL_OPERATION;
        protected System.Web.UI.WebControls.Label capLIMIT_NAVIGATION;
        protected System.Web.UI.WebControls.TextBox txtLIMIT_NAVIGATION;
        protected System.Web.UI.WebControls.Label capPORT_REGISTRATION;
        protected System.Web.UI.WebControls.TextBox txtPORT_REGISTRATION;
        protected System.Web.UI.WebControls.Label capREGISTRATION_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtREGISTRATION_NUMBER;
        protected System.Web.UI.WebControls.Label capTIE_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtTIE_NUMBER;
        protected System.Web.UI.WebControls.Label capVESSEL_ACTION_NAUTICO_CLUB;
        protected System.Web.UI.WebControls.CheckBox chkVESSEL_ACTION_NAUTICO_CLUB;
        protected System.Web.UI.WebControls.Label capNAME_OF_CLUB;
        protected System.Web.UI.WebControls.TextBox txtNAME_OF_CLUB;
        protected System.Web.UI.WebControls.Label capLOCAL_CLUB;
        protected System.Web.UI.WebControls.TextBox txtLOCAL_CLUB;
        protected System.Web.UI.WebControls.Label capNUMBER_OF_CREW;
        protected System.Web.UI.WebControls.TextBox txtNUMBER_OF_CREW;
        protected System.Web.UI.WebControls.Label capNUMBER_OF_PASSENGER;
        protected System.Web.UI.WebControls.TextBox txtNUMBER_OF_PASSENGER;
        protected System.Web.UI.WebControls.Label capREMARKS;
        protected System.Web.UI.WebControls.TextBox txtREMARKS;
        protected System.Web.UI.WebControls.Label capVESSEL_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtVESSEL_NUMBER;
       	protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
        //protected System.Web.UI.WebControls.CustomValidator csvREMARKS;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNAME_OF_VESSEL;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMANUFACTURE_YEAR;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMANUFACTURER;
        protected System.Web.UI.WebControls.RangeValidator rngMANUFACTURE_YEAR;
        protected HtmlInputHidden hidPOL_ID;
        protected HtmlInputHidden hidPOL_VERSION_ID;
        protected HtmlInputHidden hidCUSTOMER_ID;
        #endregion

        System.Resources.ResourceManager objResourceMgr;
        int MariTimeID;
        ClsProducts objProducts = new ClsProducts();
        private string strRowId = "";
        public string CALLEDFROM = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region setting screen id
            base.ScreenId = "455_1";
            hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl");
            #endregion
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");            
            this.SetErrorMessages();
            
            #region setting security Xml
            btnDelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;
            hidPOL_ID.Value = GetPolicyID();
            hidPOL_VERSION_ID.Value = GetPolicyVersionID();
            hidCUSTOMER_ID.Value = GetCustomerID();
            if (Request.QueryString["CALLEDFROM"] != null)
            {
                CALLEDFROM =Request.QueryString["CALLEDFROM"].ToString();
            }
            #endregion

            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.MariTime.AddMeriTimeInfo", System.Reflection.Assembly.GetExecutingAssembly());
            if (!Page.IsPostBack)
            {
                SetCaptions();
                FillCombo();
                FillRiskData();

                btnReset.Attributes.Add("onclick", "javascript:return ResetTheForm();");

              
                if (Request.QueryString["MARITIME_ID"] != null && Request.QueryString["MARITIME_ID"].ToString() != "" && Request.QueryString["MARITIME_ID"].ToString() != "NEW")
                {

                    hidMariTimeID.Value = Request.QueryString["MARITIME_ID"].ToString();
                    btnDelete.Enabled = true;
                    btnActivateDeactivate.Enabled = true;
                    btnDelete.Visible = true;
                    this.GetOldDataObject(Convert.ToInt32(Request.QueryString["MARITIME_ID"].ToString()));
                    
                }
                else //if (Request.QueryString["MARITIME_ID"] == null)
                {
                    btnActivateDeactivate.Enabled = false;
                    btnDelete.Enabled = false;
                    hidMariTimeID.Value = "NEW";
                    this.FillVesselNumber();
                }
                strRowId = hidMariTimeID.Value;
                this.BindExceededPremium();
            }
        }//protected void Page_Load(object sender, EventArgs e)

        #region setcaption in resource file
        private void SetCaptions()
        {
            capVESSEL_NUMBER.Text = objResourceMgr.GetString("txtVESSEL_NUMBER");
            capNAME_OF_VESSEL.Text = objResourceMgr.GetString("txtNAME_OF_VESSEL");
            capTYPE_OF_VESSEL.Text = objResourceMgr.GetString("txtTYPE_OF_VESSEL");
            capMANUFACTURE_YEAR.Text = objResourceMgr.GetString("txtMANUFACTURE_YEAR");
            capMANUFACTURER.Text = objResourceMgr.GetString("txtMANUFACTURER");
            capBUILDER.Text = objResourceMgr.GetString("txtBUILDER");
            capCONSTRUCTION.Text = objResourceMgr.GetString("txtCONSTRUCTION");
            capPROPULSION.Text = objResourceMgr.GetString("txtPROPULSION");
            capCLASSIFICATION.Text = objResourceMgr.GetString("txtCLASSIFICATION");
            capLOCAL_OPERATION.Text = objResourceMgr.GetString("txtLOCAL_OPERATION");
            capLIMIT_NAVIGATION.Text = objResourceMgr.GetString("txtLIMIT_NAVIGATION");
            capPORT_REGISTRATION.Text = objResourceMgr.GetString("txtPORT_REGISTRATION");
            capREGISTRATION_NUMBER.Text = objResourceMgr.GetString("txtREGISTRATION_NUMBER");
            capTIE_NUMBER.Text = objResourceMgr.GetString("txtTIE_NUMBER");
            capVESSEL_ACTION_NAUTICO_CLUB.Text = objResourceMgr.GetString("chkVESSEL_ACTION_NAUTICO_CLUB");
            capNAME_OF_CLUB.Text = objResourceMgr.GetString("txtNAME_OF_CLUB");
            capLOCAL_CLUB.Text = objResourceMgr.GetString("txtLOCAL_CLUB");
            capNUMBER_OF_CREW.Text = objResourceMgr.GetString("txtNUMBER_OF_CREW");
            capNUMBER_OF_PASSENGER.Text = objResourceMgr.GetString("txtNUMBER_OF_PASSENGER");
            capREMARKS.Text = objResourceMgr.GetString("txtREMARKS");
            capExceededPremium.Text = objResourceMgr.GetString("cmbExceeded_Premium");

        }   //private void SetCaptions()
        #endregion

        private void SetErrorMessages()
        {

            revVESSEL_NUMBER.ValidationExpression = aRegExpInteger;
            revVESSEL_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");

            revNAME_OF_VESSEL.ValidationExpression = aRegExpTextArea100;
            revNAME_OF_VESSEL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");

            revTYPE_OF_VESSEL.ValidationExpression = aRegExpTextArea100;
            revTYPE_OF_VESSEL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");

            //revMANUFACTURE_YEAR.ValidationExpression = aRegExpInteger;
            //this.csvMANUFACTURE_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
            rfvMANUFACTURE_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
            //revMANUFACTURE_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");

            revMANUFACTURER.ValidationExpression = aRegExpTextArea100;
            revMANUFACTURER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");

            revBUILDER.ValidationExpression = aRegExpTextArea100;
            revBUILDER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");

            revCONSTRUCTION.ValidationExpression = aRegExpTextArea100;
            revCONSTRUCTION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");

            revPROPULSION.ValidationExpression = aRegExpTextArea100;
            revPROPULSION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11");

            revCLASSIFICATION.ValidationExpression = aRegExpTextArea100;
            revCLASSIFICATION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "12");

            revLOCAL_OPERATION.ValidationExpression = aRegExpTextArea100;
            revLOCAL_OPERATION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");

            revLIMIT_NAVIGATION.ValidationExpression = aRegExpTextArea100;
            revLIMIT_NAVIGATION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14");

            revPORT_REGISTRATION.ValidationExpression = aRegExpTextArea100;
            revPORT_REGISTRATION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "15");

            revREGISTRATION_NUMBER.ValidationExpression = aRegExpTextArea100;
            revREGISTRATION_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "16");

            revTIE_NUMBER.ValidationExpression = aRegExpTextArea100;
            revTIE_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");

            revNAME_OF_CLUB.ValidationExpression = aRegExpTextArea100;
            revNAME_OF_CLUB.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "19");

            revLOCAL_CLUB.ValidationExpression = aRegExpTextArea100;
            revLOCAL_CLUB.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");

            revNUMBER_OF_CREW.ValidationExpression = aRegExpInteger;
            revNUMBER_OF_CREW.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21");

            revNUMBER_OF_PASSENGER.ValidationExpression = aRegExpInteger;
            revNUMBER_OF_PASSENGER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "22");

            //revtREMARKS.ValidationExpression = aRegExpTextArea500;
            //revtREMARKS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "23");

            //csvREMARKS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "23");

            rfvVESSEL_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
            rfvNAME_OF_VESSEL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
            rfvTYPE_OF_VESSEL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvNAME_OF_CLUB.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");
            hidVESSEL_NUMBER.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "24");
            hidVESSEL_NUMBER_Msg.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");

            rngMANUFACTURE_YEAR.MaximumValue = (DateTime.Now.Year + 1).ToString();
            rngMANUFACTURE_YEAR.MinimumValue = aAppMinYear;
            rngMANUFACTURE_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "221");
        }

        private void BindExceededPremium()
        {
            cmbExceeded_Premium.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbExceeded_Premium.DataTextField = "LookupDesc";
            cmbExceeded_Premium.DataValueField = "LookupID";
            cmbExceeded_Premium.DataBind();
        }

        /// <summary>
        /// User to Set the Form control (s)'s value Data in the Model info object
        /// </summary>
        /// <param name="objMariTimeInfo"></param>
        private void GetFormValue(ClsMeritimeInfo objMariTimeInfo)
        {
            objMariTimeInfo.ORIGINAL_VERSION_ID.CurrentValue = 0;// int.Parse(GetPolicyVersionID());
                if (txtVESSEL_NUMBER.Text != "")
                    objMariTimeInfo.VESSEL_NUMBER.CurrentValue = Convert.ToDouble(txtVESSEL_NUMBER.Text);
                else
                    objMariTimeInfo.VESSEL_NUMBER.CurrentValue = GetEbixDoubleDefaultValue();
                //if (txtNAME_OF_VESSEL.Text != "")
                //    objMariTimeInfo.NAME_OF_VESSEL.CurrentValue = txtNAME_OF_VESSEL.Text;
                //else
                //    objMariTimeInfo.NAME_OF_VESSEL.CurrentValue = String.Empty;
                if (cmbNAME_OF_VESSEL.Text != "")
                    objMariTimeInfo.NAME_OF_VESSEL.CurrentValue = cmbNAME_OF_VESSEL.SelectedValue ;
                else
                    objMariTimeInfo.NAME_OF_VESSEL.CurrentValue = String.Empty;
                if (txtTYPE_OF_VESSEL.Text != "")
                    objMariTimeInfo.TYPE_OF_VESSEL.CurrentValue = txtTYPE_OF_VESSEL.Text;
                else
                    objMariTimeInfo.TYPE_OF_VESSEL.CurrentValue = String.Empty;

                if (txtMANUFACTURE_YEAR.Text.Trim() != "")
                    objMariTimeInfo.MANUFACTURE_YEAR.CurrentValue = Convert.ToInt32(txtMANUFACTURE_YEAR.Text);
                else
                    objMariTimeInfo.MANUFACTURE_YEAR.CurrentValue = GetEbixIntDefaultValue();

                if (txtMANUFACTURER.Text != "")
                    objMariTimeInfo.MANUFACTURER.CurrentValue = txtMANUFACTURER.Text;
                else
                    objMariTimeInfo.MANUFACTURER.CurrentValue = String.Empty;
                if (txtBUILDER.Text != "")
                    objMariTimeInfo.BUILDER.CurrentValue = txtBUILDER.Text;
                else
                    objMariTimeInfo.BUILDER.CurrentValue = String.Empty;

                if (txtCONSTRUCTION.Text != "")
                    objMariTimeInfo.CONSTRUCTION.CurrentValue = txtCONSTRUCTION.Text;
                else
                    objMariTimeInfo.CONSTRUCTION.CurrentValue = String.Empty;

                if(txtPROPULSION.Text!="")
                    objMariTimeInfo.PROPULSION.CurrentValue = txtPROPULSION.Text;
                else
                    objMariTimeInfo.PROPULSION.CurrentValue = String.Empty;
                if( txtCLASSIFICATION.Text!="")
                    objMariTimeInfo.CLASSIFICATION.CurrentValue = txtCLASSIFICATION.Text;
                else
                    objMariTimeInfo.CLASSIFICATION.CurrentValue = String.Empty;
                if (txtLOCAL_OPERATION.Text != "")
                    objMariTimeInfo.LOCAL_OPERATION.CurrentValue = txtLOCAL_OPERATION.Text;
                else
                    objMariTimeInfo.LOCAL_OPERATION.CurrentValue = String.Empty;
                
                if( txtLIMIT_NAVIGATION.Text!="")
                    objMariTimeInfo.LIMIT_NAVIGATION.CurrentValue = txtLIMIT_NAVIGATION.Text;
                else
                    objMariTimeInfo.LIMIT_NAVIGATION.CurrentValue = String.Empty;
                if(txtPORT_REGISTRATION.Text!="")
                    objMariTimeInfo.PORT_REGISTRATION.CurrentValue = txtPORT_REGISTRATION.Text;
                else
                    objMariTimeInfo.PORT_REGISTRATION.CurrentValue = String.Empty;
                 
                if(txtREGISTRATION_NUMBER.Text!="")
                    objMariTimeInfo.REGISTRATION_NUMBER.CurrentValue = txtREGISTRATION_NUMBER.Text;
                else
                    objMariTimeInfo.REGISTRATION_NUMBER.CurrentValue =String.Empty;
                if( txtTIE_NUMBER.Text!="")
                    objMariTimeInfo.TIE_NUMBER.CurrentValue = txtTIE_NUMBER.Text;
                else
                    objMariTimeInfo.TIE_NUMBER.CurrentValue = String.Empty;
                if(txtNAME_OF_CLUB.Text!="")
                    objMariTimeInfo.NAME_OF_CLUB.CurrentValue = txtNAME_OF_CLUB.Text;
                else
                    objMariTimeInfo.NAME_OF_CLUB.CurrentValue = String.Empty;

                if(txtLOCAL_CLUB.Text!="")
                    objMariTimeInfo.LOCAL_CLUB.CurrentValue = txtLOCAL_CLUB.Text;
                else
                    objMariTimeInfo.LOCAL_CLUB.CurrentValue = String.Empty;

                if (txtNUMBER_OF_CREW.Text.Trim() != "")
                    objMariTimeInfo.NUMBER_OF_CREW.CurrentValue = Convert.ToInt32(txtNUMBER_OF_CREW.Text);
                else
                    objMariTimeInfo.NUMBER_OF_CREW.CurrentValue = GetEbixIntDefaultValue();
                if (txtNUMBER_OF_PASSENGER.Text.Trim() != "")
                    objMariTimeInfo.NUMBER_OF_PASSENGER.CurrentValue = Convert.ToInt32(txtNUMBER_OF_PASSENGER.Text);
                else
                    objMariTimeInfo.NUMBER_OF_PASSENGER.CurrentValue = GetEbixIntDefaultValue();
                if (txtREMARKS.Text.Trim() != "")
                { objMariTimeInfo.REMARKS.CurrentValue = txtREMARKS.Text; }

                if (cmbExceeded_Premium.SelectedValue != "")
                    objMariTimeInfo.EXCEEDED_PREMIUM.CurrentValue = int.Parse(cmbExceeded_Premium.SelectedValue);
                else
                    objMariTimeInfo.EXCEEDED_PREMIUM.CurrentValue = GetEbixIntDefaultValue();

        }//private void GetFormValue(ClsMeritimeInfo objMariTimeInfo)
       
        /// <summary>
        /// Use to Get the Old Data Based on the ID
        /// </summary>
        /// <param name="MariTime_ID"></param>
        private void GetOldDataObject(Int32 MariTime_ID)
        {
            ClsMeritimeInfo objMeritimeInfo=new ClsMeritimeInfo();

            objMeritimeInfo.MARITIME_ID.CurrentValue = MariTime_ID;
            objMeritimeInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
            objMeritimeInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
            objMeritimeInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());
            string policystatus = GetPolicyStatus();
            if (objProducts.FetchData(ref objMeritimeInfo))
            {
                PopulatePageFromEbixModelObject(this.Page, objMeritimeInfo);
                
                txtVESSEL_NUMBER.Text = objMeritimeInfo.VESSEL_NUMBER.CurrentValue.ToString();
                hidOLD_VESSEL_NUMBER.Value = objMeritimeInfo.VESSEL_NUMBER.CurrentValue.ToString();
                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objMeritimeInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                // itrack no 867
                string originalversion = objMeritimeInfo.ORIGINAL_VERSION_ID.CurrentValue.ToString();
                if (originalversion != GetPolicyVersionID() && policystatus == "UENDRS" && originalversion != "0")
                {
                    btnActivateDeactivate.Visible = false;
                }
                if (objMeritimeInfo.NAME_OF_CLUB.CurrentValue.ToString().Trim() != "")
                {
                        chkVESSEL_ACTION_NAUTICO_CLUB.Checked = true;
                        //Div_Name_Of_Club.Attributes.Add("display","inline");
                }

                base.SetPageModelObject(objMeritimeInfo);
            }//if (objProducts.FetchData(ref objMeritimeInfo)

        }//private void GetOldDataObject(Int32 MariTime_ID)

        ///// <summary>
        ///// Required method for Designer support - do not modify
        ///// the contents of this method with the code editor.
        ///// </summary>

        private void FillCombo()
        {

            ClsMeritimeInfo obj = new ClsMeritimeInfo();    
            DataSet ds = obj.getNameOfVessel();
            cmbNAME_OF_VESSEL.DataSource = ds;
            cmbNAME_OF_VESSEL.DataTextField = "VESSEL_NAME";
            cmbNAME_OF_VESSEL.DataValueField = "VESSEL_ID";
            cmbNAME_OF_VESSEL.DataBind();
            cmbNAME_OF_VESSEL.Items.Insert(0, "");


        }
        private void FillRiskData()
        {
            int strVesselID;
            ClsMeritimeInfo obj = new ClsMeritimeInfo();
            if (cmbNAME_OF_VESSEL.SelectedValue != "")
            {
                strVesselID = int.Parse(cmbNAME_OF_VESSEL.SelectedValue);
                if (strVesselID > 0)
                {
                    DataSet dsRisk = obj.getVesselDataatRisk(strVesselID);
                    txtMANUFACTURE_YEAR.Text = dsRisk.Tables[0].Rows[0]["YEAR_BUILT"].ToString();
                    txtTYPE_OF_VESSEL.Text = dsRisk.Tables[0].Rows[0]["FLAG"].ToString();
                }
            }
        }
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.cmbNAME_OF_VESSEL.SelectedIndexChanged += new System.EventHandler(this.cmbNAME_OF_VESSEL_SelectedIndexChanged);
        }//private void InitializeComponent()

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClsMeritimeInfo objMariTimeInfo;
            Cms.BusinessLayer.Blapplication.ClsAircraftDetails objClsAircraftDetails = new BusinessLayer.Blapplication.ClsAircraftDetails();
            try
            {
                if (hidMariTimeID.Value != "")
                {
                    objMariTimeInfo = (ClsMeritimeInfo)base.GetPageModelObject();

                    objMariTimeInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                    int intRetval = objProducts.DeleteMariTime(objMariTimeInfo);
                    objClsAircraftDetails.DeletePageControl(hidMariTimeID.Value);
                    if (intRetval > 0)
                    {
                        lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                        hidFormSaved.Value = "1";
                        trBody.Attributes.Add("style", "display:none");
                        hidMariTimeID.Value = "";
                    }
                    else if (intRetval == -1)
                    {
                        lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "128");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = true;
                    lblMessage.Visible = false;
                }
                    
               
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }//protected void btnDelete_Click(object sender, EventArgs e)

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int intRetval;
            strRowId = hidMariTimeID.Value;
            ClsMeritimeInfo objMariTimeInfo;
            try
            {
                //For The Save Case                
                if (strRowId.ToUpper().Equals("NEW"))
                {

                    objMariTimeInfo = new ClsMeritimeInfo();
                    this.GetFormValue(objMariTimeInfo);

                    objMariTimeInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
                    objMariTimeInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
                    objMariTimeInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());

                    objMariTimeInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objMariTimeInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToShortDateString());
                    objMariTimeInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;
                    objMariTimeInfo.CO_APPLICANT_ID.CurrentValue = ClsGeneralInformation.GetPolicyPrimary_Applicant(objMariTimeInfo.CUSTOMER_ID.CurrentValue, objMariTimeInfo.POLICY_ID.CurrentValue, objMariTimeInfo.POLICY_VERSION_ID.CurrentValue);
                    
                    intRetval = objProducts.AddMariTime(objMariTimeInfo);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject(objMariTimeInfo.MARITIME_ID.CurrentValue);
                        
                        hidMariTimeID.Value = objMariTimeInfo.MARITIME_ID.CurrentValue.ToString();
                        btnActivateDeactivate.Enabled = true;
                        btnDelete.Enabled = true;
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objMariTimeInfo.IS_ACTIVE.CurrentValue.Trim());

                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";
                        base.OpenEndorsementDetails();
                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -2)
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "24");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
                } // if (strRowId.ToUpper().Equals("NEW"))
                else //For The Update cse
                {


                    objMariTimeInfo = (ClsMeritimeInfo)base.GetPageModelObject();
                    this.GetFormValue(objMariTimeInfo);
                    objMariTimeInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;
                    objMariTimeInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objMariTimeInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToShortDateString());

                    intRetval = objProducts.UpdateMariTime(objMariTimeInfo);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject(objMariTimeInfo.MARITIME_ID.CurrentValue);
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                        hidFormSaved.Value = "1";

                    }
                    else if (intRetval == -1)
                    {
                         
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -2)
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "24");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
            finally
            {
                //if (objNamedPerilsinfo != null)
                //    objNamedPerilsinfo.Dispose();
            }
        }//protected void btnSave_Click(object sender, EventArgs e)

        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {

            ClsMeritimeInfo objMariTimeInfo;

           try
           {
               if (int.TryParse(hidMariTimeID.Value, out MariTimeID))
               {
                   objMariTimeInfo = (ClsMeritimeInfo)base.GetPageModelObject();

                   if (objMariTimeInfo.IS_ACTIVE.CurrentValue == "Y")
                   { objMariTimeInfo.IS_ACTIVE.CurrentValue = "N"; }
                   else
                   { objMariTimeInfo.IS_ACTIVE.CurrentValue = "Y"; }


                   objMariTimeInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                   objMariTimeInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToString());


                   int intRetval = objProducts.ActivateDeactivateMariTime(objMariTimeInfo);
                   if (intRetval > 0)
                   {
                       if (objMariTimeInfo.IS_ACTIVE.CurrentValue == "N")
                       {
                           lblMessage.Text = ClsMessages.FetchGeneralMessage("9");
                           // itrack no 867
                          // btnActivateDeactivate.Visible = false;
                       }
                       else
                       {
                           lblMessage.Text = ClsMessages.FetchGeneralMessage("7");
                       }
                       btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objMariTimeInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                       hidFormSaved.Value = "1";

                       SetPageModelObject(objMariTimeInfo);
                   }
                   lblDelete.Visible = false;
                   lblMessage.Visible = true;
               }
              
           }
           catch (Exception ex)
           {
               lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
               lblMessage.Visible = true;
               Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
               hidFormSaved.Value = "2";

           }
       
        }//protected void btnActivateDeactivate_Click(object sender, EventArgs e)

        private void FillVesselNumber()
        {

            String Numbers = GetMaxIdOfVesselNumber(hidCUSTOMER_ID.Value, hidPOL_ID.Value, hidPOL_VERSION_ID.Value, "0", "3");

            if (Numbers != String.Empty)
            {
                String[] Number = Numbers.Split(',');
                if (Number[0] == "1" && Number[1] == "0" && Number[2] != "0"  && Number[3] == "3")
                   txtVESSEL_NUMBER.Text = Number[2].ToString();
                else
                    txtVESSEL_NUMBER.Text = String.Empty;
            }
        }

        [System.Web.Services.WebMethod]
        public static String GetMaxIdOfVesselNumber(String CUSTOMER_ID, String POLICY_ID, String POLICY_VERSION_ID, String VESSEL_NUMBER, String flag)
        {

            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice

            String ReturnValue = String.Empty;
            Int64 COMMODITYNUMBER = 0;
            Int64 VESSELNUMBER  = Convert.ToInt64(VESSEL_NUMBER);
            
            String CALLEDFOR = "VesselNo";
            Int64 NUMBER=0;
                                                          
            
            ReturnValue = obj.GetMaxIDofVesselNoandVoyageNo(Convert.ToInt32(CUSTOMER_ID),Convert.ToInt32( POLICY_ID),Convert.ToInt32( POLICY_VERSION_ID), VESSELNUMBER, COMMODITYNUMBER,ref NUMBER, CALLEDFOR,Convert.ToInt32(flag));

            if (ReturnValue != "-1" )
                ReturnValue = ReturnValue +","+ VESSELNUMBER + "," + NUMBER + "," + flag;
            else
                ReturnValue = String.Empty;

            return ReturnValue;
        }

        protected void cmbNAME_OF_VESSEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRiskData();
        }

       

       
    }
}
