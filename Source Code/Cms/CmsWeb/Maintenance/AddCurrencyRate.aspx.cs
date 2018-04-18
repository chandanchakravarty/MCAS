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
using Cms.CmsWeb;
using System.Resources;
using System.Reflection;
using Cms.Model.Maintenance.Accounting;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.IO;
using Cms.Blcommon;
using Cms.Model.Maintenance;

namespace CmsWeb.Maintenance
{
    public partial class AddCurrencyRate : Cms.CmsWeb.cmsbase
    {
        #region page control declartion
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        #endregion
        #region Set Validators ErrorMessages
        private void SetErrorMessage()
        { 
            rfvCURRENCY_ID.ErrorMessage=ClsMessages.GetMessage(base.ScreenId,"1");
            rfvRATE.ErrorMessage=ClsMessages.GetMessage(base.ScreenId,"2");
            rfvRATE_EFFETIVE_FROM.ErrorMessage=ClsMessages.GetMessage(base.ScreenId,"3");
            rfvRATE_EFFETIVE_TO.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "5");
            revRATE_EFFETIVE_FROM.ValidationExpression = aRegExpDate;
            revRATE_EFFETIVE_FROM.ErrorMessage=ClsMessages.FetchGeneralMessage("22");
            revRATE_EFFETIVE_TO.ValidationExpression = aRegExpDate;
            revRATE_EFFETIVE_TO.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            cpvRATE_EFFETIVE_TO.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("447");
            revRATE.ValidationExpression = aRegExpDoublePositiveNonZero;
            revRATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");
            csvRATE_EFFETIVE_FROM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2077");
            csvRATE_EFFETIVE_TO.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2077");
            rvRate.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2075"); // Added by Agniswar for itrack #1619
            

        }
        #endregion
        #region Local form variable
       // ResourceManager objResourceMgr;
        ResourceManager objresource;
        ClsCurrencyRate objCurrencyRate = new ClsCurrencyRate();
        ClsCurrencyRateInfo objCurrencyRateinfo = new ClsCurrencyRateInfo();
        private string strRowId = "";
        #endregion

        

        #region PageLoad event
        private void Page_Load(object sender, System.EventArgs e)
        {
           
            base.ScreenId = "523_1";
           
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

           
            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            SetErrorMessage();
            objresource = new System.Resources.ResourceManager("Cmsweb.Maintenance.AddCurrencyRate", System.Reflection.Assembly.GetExecutingAssembly());
            hlkRATE_EFFETIVE_FROM.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtRATE_EFFETIVE_FROM'), document.getElementById('txtRATE_EFFETIVE_FROM'))");
            hlkRATE_EFFETIVE_TO.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtRATE_EFFETIVE_TO'), document.getElementById('txtRATE_EFFETIVE_TO'))");
            txtRATE_EFFETIVE_FROM.Attributes.Add("onBlur", "FormatDate()");
            txtRATE_EFFETIVE_TO.Attributes.Add("onBlur", "FormatDate()");

            if (GetLanguageID() == "2")
            {
                txtRATE.Attributes.Add("onblur", "javascript:validate();this.value=formatRateTextValue(this.value,2);formatRateBase(this.value);ValidatorOnChange();");
            }
            else
            {
                txtRATE.Attributes.Add("onblur", "javascript:validate();this.value=formatRateTextValue(this.value,1);formatCurrencyRate(this.value);ValidatorOnChange();");
            }
          
            btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");
            if (!IsPostBack) {
                SetDropdown();
                SetCaption();

                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "cmsweb/support/PageXML/" + GetSystemId(), "AddCurrencyRate.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + GetSystemId() + "/AddCurrencyRate.xml");            
                if (Request.QueryString["CRR_RATE_ID"] != null && Request.QueryString["CRR_RATE_ID"].ToString() != "")
                {

                    hidcurrencyrateId.Value = Request.QueryString["CRR_RATE_ID"].ToString();

                      this.GetOldDataObject(Convert.ToInt32(Request.QueryString["CRR_RATE_ID"].ToString()));
                    

                }
                else if (Request.QueryString["CRR_RATE_ID"] == null)
                {
                    btnActivateDeactivate.Visible = false;
                   
                    hidcurrencyrateId.Value = "NEW";

                }

              
              
           }

            strRowId = hidcurrencyrateId.Value; 
        }
        #endregion

        private void GetOldDataObject(Int32 CRR_RATE_ID)
        {

            objCurrencyRateinfo = objCurrencyRate.FetchData(CRR_RATE_ID);
            PopulatePageFromEbixModelObject(this.Page, objCurrencyRateinfo);
            btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objCurrencyRateinfo.IS_ACTIVE.CurrentValue.ToString().Trim());
            base.SetPageModelObject(objCurrencyRateinfo);
            hidcurrencyrateId.Value = objCurrencyRateinfo.CRR_RATE_ID.CurrentValue.ToString().Trim();
            if (objCurrencyRateinfo.RATE.CurrentValue.ToString() != "") //Edited by Agniswar for Itrack #1619
            {
                if (GetLanguageID() == "2")
                {
                    txtRATE.Text = objCurrencyRateinfo.RATE.CurrentValue.ToString("N", NfiBaseCurrency);
                    txtRATE.Text = String.Format("{0:0.00}", double.Parse(txtRATE.Text));
                }
                else
                {
                    txtRATE.Text = String.Format("{0:0.00}", double.Parse(txtRATE.Text)); // Line Added by Agniswar for Bug # 899
                }
            }
            


        }

        #region Set Caption

        private void SetCaption()
        {

            capCURRENCY_ID.Text = objresource.GetString("capCURRENCY_ID");
            capRATE.Text = objresource.GetString("capRATE");
            capRATE_EFFETIVE_FROM.Text = objresource.GetString("capRATE_EFFETIVE_FROM");
            capRATE_EFFETIVE_TO.Text = objresource.GetString("capRATE_EFFETIVE_TO");

        }
        #endregion

#region Set Dropdown

        private void SetDropdown() {
            DataTable dt = new DataTable();
            dt = Cms.CmsWeb.ClsFetcher.Currency;
            cmbCURRENCY_ID.DataSource = dt;
            cmbCURRENCY_ID.DataTextField = "CURR_DESC";
            cmbCURRENCY_ID.DataValueField = "CURRENCY_ID";
            cmbCURRENCY_ID.DataBind();
            cmbCURRENCY_ID.Items.Insert(0, "");
        }
#endregion


        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            //  this.btnResetSeries.Click += new System.EventHandler(this.btnResetSeries_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //  this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
        #region "Web Event Handlers"
        /// <summary>
        /// If form is posted back then add entry in database using the BL object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                int intRetVal;	//For retreiving the return value of business class save function
               

                //Retreiving the form values into model class object
              
                this.GetFormValue(objCurrencyRateinfo);

               if (strRowId.ToUpper().Equals("NEW")) //save case              
                {
                    objCurrencyRateinfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objCurrencyRateinfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
                    objCurrencyRateinfo.IS_ACTIVE.CurrentValue = "Y";

                    //Calling the add method of business layer class
                    intRetVal = objCurrencyRate.AddCurrencyRate(objCurrencyRateinfo);

                    if (intRetVal == 1)
                    {
                        if (objCurrencyRateinfo.CRR_RATE_ID.CurrentValue.ToString() != "1")
                        {
                            this.GetOldDataObject(objCurrencyRateinfo.CRR_RATE_ID.CurrentValue);
                        }
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";
                        btnActivateDeactivate.Visible = true;
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objCurrencyRateinfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    }
                    else if (intRetVal == -2) {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "4");
                    }

                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                } // end save case
                else //UPDATE CASE
                {


                    objCurrencyRateinfo = (ClsCurrencyRateInfo)base.GetPageModelObject();

                    this.GetFormValue(objCurrencyRateinfo);
                   
                    objCurrencyRateinfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objCurrencyRateinfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
                   

                    int intRetval = objCurrencyRate.UpdateCurrencyRate(objCurrencyRateinfo);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject(objCurrencyRateinfo.CRR_RATE_ID.CurrentValue);
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objCurrencyRateinfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                        hidFormSaved.Value = "1";

                    }

                    else if (intRetval == 0)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "4");
                    }
                    else
                    {
                        lblMessage.Text = lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                
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

            }
           
        }

        private void GetFormValue(ClsCurrencyRateInfo objCurrencyRateinfo) {

            if (cmbCURRENCY_ID.SelectedValue != "")
            {
                objCurrencyRateinfo.CURRENCY_ID.CurrentValue = Convert.ToInt32(cmbCURRENCY_ID.SelectedValue);
            }           
            if (txtRATE.Text != "")
            {
                if (GetLanguageID() == "2")
                {
                    objCurrencyRateinfo.RATE.CurrentValue = Convert.ToDouble(txtRATE.Text, NfiBaseCurrency);
                }
                else
                {
                    objCurrencyRateinfo.RATE.CurrentValue = Convert.ToDouble(txtRATE.Text);
                }
            }
            if (txtRATE_EFFETIVE_FROM.Text != "")
            {
                objCurrencyRateinfo.RATE_EFFETIVE_FROM.CurrentValue = ConvertToDate(txtRATE_EFFETIVE_FROM.Text);
            }
            if (txtRATE_EFFETIVE_TO.Text != "")
            {
                objCurrencyRateinfo.RATE_EFFETIVE_TO.CurrentValue = ConvertToDate(txtRATE_EFFETIVE_TO.Text);
            }       
        
        }

        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            

            try
            {
                objCurrencyRateinfo = (ClsCurrencyRateInfo)base.GetPageModelObject();

                if (objCurrencyRateinfo.IS_ACTIVE.CurrentValue == "Y")
                { objCurrencyRateinfo.IS_ACTIVE.CurrentValue = "N"; }
                else
                { objCurrencyRateinfo.IS_ACTIVE.CurrentValue = "Y"; }


                objCurrencyRateinfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                objCurrencyRateinfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);


                int intRetval = objCurrencyRate.ActivateDeactivateCurrencyRate(objCurrencyRateinfo);
                if (intRetval > 0)
                {
                    if (objCurrencyRateinfo.IS_ACTIVE.CurrentValue == "N")
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("41");
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objCurrencyRateinfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("40");
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objCurrencyRateinfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    }
                    hidFormSaved.Value = "1";

                    SetPageModelObject(objCurrencyRateinfo);
                }
                else if (intRetval == -2)
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "456_9");
                }
                
                lblMessage.Visible = true;

            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }
        #endregion

       

    }
}
