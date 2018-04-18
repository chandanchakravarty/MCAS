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
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.IO;
using Cms.CmsWeb.webservices;
using Cms.Blcommon;
using Cms.Model.Maintenance;
using Cms.EbixDataTypes;
using System.Globalization;

namespace Cms.CmsWeb.Maintenance
{
    public partial class InterestRateDetail : Cms.CmsWeb.cmsbase
    {
        System.Resources.ResourceManager objresource;
        private string strRowId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "560_0";
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;
            hlkRATE_EFFECTIVE_DATE.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtRATE_EFFECTIVE_DATE'), document.getElementById('txtRATE_EFFECTIVE_DATE'))");
            //txtDate.Attributes.Add("onBlur", "FormatDate()");
            objresource = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.InterestRateDetail", System.Reflection.Assembly.GetExecutingAssembly());
            btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");
            if (GetLanguageID() == "2")
            {
                txtINTEREST_RATE.Attributes.Add("onblur", "javascript:this.value=formatRateTextValue(this.value,4);formatRateBase(this.value);ValidatorOnChange();");
            }
            else
            {
                txtINTEREST_RATE.Attributes.Add("onblur", "javascript:this.value=formatRateTextValue(this.value,1);formatCurrencyRate(this.value);ValidatorOnChange();");
            }
            //txtINTEREST_RATE.Attributes.Add("onBlur", "this.value=formatRateBase(this.value,4);");            

            if (!Page.IsPostBack)
            {
                BindInstallments();
                SetCaption();
                BindInterestType();
                SetErrorMessages();

                if (Request.QueryString["INTEREST_RATE_ID"] != null && Request.QueryString["INTEREST_RATE_ID"].ToString() != "")
                {
                    hidInterestRateID.Value = Request.QueryString["INTEREST_RATE_ID"].ToString();
                    this.GetOldDataObject();
                }
                else if (Request.QueryString["INTEREST_RATE_ID"] == null)
                {
                    hidInterestRateID.Value = "NEW";
                }
                strRowId = hidInterestRateID.Value;


            }
        }

         private void GetOldDataObject()
        {
             try
            {
                ClsInterestRates objInterestRates = new ClsInterestRates();
                ClsInterestRatesInfo objInterestRatesInfo = new ClsInterestRatesInfo();
                objInterestRatesInfo = objInterestRates.FetchData(int.Parse(hidInterestRateID.Value));
                //PopulatePageFromEbixModelObject(this.Page, objInterestRatesInfo);                

                if (objInterestRatesInfo.NO_OF_INSTALLMENTS.CurrentValue.ToString() != "" && objInterestRatesInfo.NO_OF_INSTALLMENTS.CurrentValue.ToString() != null)
                {
                    string No_of_Installments = objInterestRatesInfo.NO_OF_INSTALLMENTS.CurrentValue.ToString();
                    cmbNO_OF_INSTALLMENTS.SelectedValue = No_of_Installments;                    
                }

                if (objInterestRatesInfo.INTEREST_TYPE.CurrentValue.ToString() != "" && objInterestRatesInfo.INTEREST_TYPE.CurrentValue.ToString() != null)
                {
                    string Interest_type = objInterestRatesInfo.INTEREST_TYPE.CurrentValue.ToString();
                    cmbINTEREST_TYPE.SelectedValue = Interest_type;
                }

                if (GetLanguageID() == "2") //Changed by aditya for bug # 1761
                {
                    txtINTEREST_RATE.Text = objInterestRatesInfo.INTEREST_RATE.CurrentValue.ToString("N", NfiBaseCurrency);
                    txtINTEREST_RATE.Text = String.Format("{0:0.0000}", double.Parse(txtINTEREST_RATE.Text));
                }
                else
                {
                    txtINTEREST_RATE.Text = objInterestRatesInfo.INTEREST_RATE.CurrentValue.ToString();
                    txtINTEREST_RATE.Text = String.Format("{0:0.0000}", double.Parse(txtINTEREST_RATE.Text));
                }
                                
                txtRATE_EFFECTIVE_DATE.Text = objInterestRatesInfo.RATE_EFFECTIVE_DATE.CurrentValue.ToShortDateString();

                base.SetPageModelObject(objInterestRatesInfo);

            }
             catch (Exception ex)
             {
                 lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
                 lblMessage.Visible = true;
                 Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
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
           this.btnSave.Click += new System.EventHandler(this.btnSave_Click);            

        }
        #endregion

        private void BindInstallments()
        {
            cmbNO_OF_INSTALLMENTS.Items.Add("");
            cmbNO_OF_INSTALLMENTS.Items.Add("1");
            cmbNO_OF_INSTALLMENTS.Items.Add("2");
            cmbNO_OF_INSTALLMENTS.Items.Add("3");
            cmbNO_OF_INSTALLMENTS.Items.Add("4");
            cmbNO_OF_INSTALLMENTS.Items.Add("5");
            cmbNO_OF_INSTALLMENTS.Items.Add("6");
            cmbNO_OF_INSTALLMENTS.Items.Add("7");
            cmbNO_OF_INSTALLMENTS.Items.Add("8");
            cmbNO_OF_INSTALLMENTS.Items.Add("9");
            cmbNO_OF_INSTALLMENTS.Items.Add("10");
            cmbNO_OF_INSTALLMENTS.Items.Add("11");
            cmbNO_OF_INSTALLMENTS.Items.Add("12");
        }

        private void BindInterestType()
        {
            cmbINTEREST_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("INTTYP");
            cmbINTEREST_TYPE.DataTextField = "LookupDesc";
            cmbINTEREST_TYPE.DataValueField = "LookupID";
            cmbINTEREST_TYPE.DataBind();
        }

        private void SetErrorMessages()
        {
            revINTEREST_RATE.ValidationExpression = aRegExpDoublePositiveNonZero;
            revINTEREST_RATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            csvINTEREST_RATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            rfvINTEREST_RATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
            rfvRATE_EFFECTIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
            rfvNO_OF_INSTALLMENTS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            revRATE_EFFECTIVE_DATE.ValidationExpression = aRegExpDate;
            csvRATE_EFFECTIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2077");
        }

        private void SetCaption()
        {
            capDate.Text = objresource.GetString("capDate");
            capNO_OF_INSTALLMENTS.Text = objresource.GetString("capNO_OF_INSTALLMENTS");
            capINTEREST_TYPE.Text = objresource.GetString("capINTEREST_TYPE");
            capINTEREST_RATE.Text = objresource.GetString("capINTEREST_RATE");
         }
        
        private void getFormValues(ClsInterestRatesInfo objInterestRatesInfo)
        {
            if (txtRATE_EFFECTIVE_DATE.Text.Trim() != "")
            { objInterestRatesInfo.RATE_EFFECTIVE_DATE.CurrentValue = ConvertToDate(txtRATE_EFFECTIVE_DATE.Text); }


            if (cmbNO_OF_INSTALLMENTS.SelectedValue != "")
            {
                objInterestRatesInfo.NO_OF_INSTALLMENTS.CurrentValue = Convert.ToInt32(cmbNO_OF_INSTALLMENTS.SelectedValue);
            }

            if (cmbINTEREST_TYPE.SelectedValue != "")
            {
                objInterestRatesInfo.INTEREST_TYPE.CurrentValue = Convert.ToInt32(cmbINTEREST_TYPE.SelectedValue);
            }

            if (txtINTEREST_RATE.Text != "")
            {
                if (GetLanguageID() == "2")
                {
                    objInterestRatesInfo.INTEREST_RATE.CurrentValue = Convert.ToDouble(txtINTEREST_RATE.Text, NfiBaseCurrency);
                }
                else
                {
                    objInterestRatesInfo.INTEREST_RATE.CurrentValue = Convert.ToDouble(txtINTEREST_RATE.Text); //Changed by aditya for bug # 1761
                }
            }

        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            int intRetval;


            ClsInterestRatesInfo objInterestRatesInfo;
            ClsInterestRates objInterestRates = new ClsInterestRates();
            try
            {
                //For new item to add
                strRowId = hidInterestRateID.Value;
                if (strRowId.ToUpper().Equals("NEW"))
                {
                    objInterestRatesInfo = new ClsInterestRatesInfo();
                    this.getFormValues(objInterestRatesInfo);

                    objInterestRatesInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objInterestRatesInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                    objInterestRatesInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;
                    intRetval = objInterestRates.AddInterestRateInformation(objInterestRatesInfo);
                    hidInterestRateID.Value = objInterestRatesInfo.INTEREST_RATE_ID.CurrentValue.ToString();

                    if (intRetval > 0)
                    {
                        hidInterestRateID.Value = objInterestRatesInfo.INTEREST_RATE_ID.CurrentValue.ToString();

                        this.GetOldDataObject();

                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";


                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                    }

                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                }
                //For The Update case
                else
                {

                    objInterestRatesInfo = (ClsInterestRatesInfo)base.GetPageModelObject();
                    this.getFormValues(objInterestRatesInfo);

                    objInterestRatesInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objInterestRatesInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objInterestRates.UpdateInterestRateInformation(objInterestRatesInfo);

                    if (intRetval > 0)
                    {
                        hidInterestRateID.Value = objInterestRatesInfo.INTEREST_RATE_ID.CurrentValue.ToString();
                        this.GetOldDataObject();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");                        
                        hidFormSaved.Value = "1";


                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                    }

                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClsInterestRatesInfo objInterestRatesInfo;
            ClsInterestRates objInterestRates = new ClsInterestRates();

            int intRetval;

            try
            {
                objInterestRatesInfo = (ClsInterestRatesInfo)base.GetPageModelObject();

                objInterestRatesInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                intRetval = objInterestRates.DeleteInterestRateInformation(objInterestRatesInfo);

                if (intRetval > 0)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                    hidFormSaved.Value = "1";
                    trBody.Attributes.Add("style", "display:none");

                }
                else if (intRetval == -1)
                {
                    lblDelete.Text = ClsMessages.GetMessage(base.ScreenId, "128");
                    hidFormSaved.Value = "2";
                }
                lblDelete.Visible = true;

            }


            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }

    }
}