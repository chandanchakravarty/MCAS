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
    public partial class AddIOFDetails : Cms.CmsWeb.cmsbase
    {
        ClsInterestRates objInterestRates = new ClsInterestRates();
        System.Resources.ResourceManager objresource;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "562_0";
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;
            objresource = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddIOFDetails", System.Reflection.Assembly.GetExecutingAssembly());
            if (GetLanguageID() == "2")  //added by aditya for bug # 1761
            {
                txtIOF_PERCENTAGE.Attributes.Add("onblur", "javascript:this.value=formatRateTextValue(this.value,4);formatRateBase(this.value);ValidatorOnChange();");
            }
            else
            {
                txtIOF_PERCENTAGE.Attributes.Add("onblur", "javascript:this.value=formatRateTextValue(this.value,1);formatCurrencyRate(this.value);ValidatorOnChange();");
            }
            
            if (!Page.IsPostBack)
            {
                SetCaption();
                SetErrorMessages();

                if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
                {

                    hidLOBID.Value = Request.QueryString["LOB_ID"].ToString();
                    this.GetIOFDetails(int.Parse(hidLOBID.Value));
                }
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

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            //Saving form values
            SaveFormValues();
        }

        private void SetErrorMessages()
        {
            revIOF_PERCENTAGE.ValidationExpression = aRegExpDoublePositiveWithZeroFourDeci;
            revIOF_PERCENTAGE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
            csvIOF_PERCENTAGE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            rfvIOF_PERCENTAGE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
        }

        private void SetCaption()
        {
            capLOB_DESC.Text = objresource.GetString("capLOB_DESC");
            capIOF_PERCENTAGE.Text = objresource.GetString("capIOF_PERCENTAGE");
        }

        private void GetIOFDetails( int  lobid)
        {
            ClsInterestRates objInterestRates = new ClsInterestRates();
            ClsInterestRatesInfo objInterestRatesInfo = new ClsInterestRatesInfo();
            DataTable dt;
            dt = ClsInterestRates.FetchIOFDetails(int.Parse(hidLOBID.Value));
            lblLOB_DESC.Text = dt.Rows[0]["LOB_DESC"].ToString();
            if (dt.Rows[0]["LOB_ID"].ToString() != "" && dt.Rows[0]["LOB_ID"].ToString() != null)
            {             
                hidLOBID.Value = dt.Rows[0]["LOB_ID"].ToString();
            }
           
                if (GetLanguageID() == "2") // Changed by aditya for bug # 1761
                {
                    txtIOF_PERCENTAGE.Text = Convert.ToDouble(dt.Rows[0]["IOF_PERCENTAGE"]).ToString("N", NfiBaseCurrency);
                    txtIOF_PERCENTAGE.Text = String.Format("{0:0.0000}", double.Parse(txtIOF_PERCENTAGE.Text));
                }
                else
                {
                    txtIOF_PERCENTAGE.Text = Convert.ToDouble(dt.Rows[0]["IOF_PERCENTAGE"]).ToString();
                    txtIOF_PERCENTAGE.Text = String.Format("{0:0.0000}", double.Parse(txtIOF_PERCENTAGE.Text));
                }
        }

        private void SaveFormValues()
        {
            try
            {
                int intRetVal;
                //Retreiving the form values into model class object
                ClsInterestRatesInfo objInterestRatesInfo = getFormValues();
                //Creating the Model object for holding the Old data
                ClsInterestRatesInfo objOldInterestRatesInfo;
                objOldInterestRatesInfo = new ClsInterestRatesInfo();
                
                if (doValidationCheck())
                {
                    ClsInterestRates objInterestRates = new ClsInterestRates();
                    objInterestRatesInfo.LOB_ID.CurrentValue = int.Parse(hidLOBID.Value);
                    intRetVal = objInterestRates.UpdateIOFDetails(objInterestRatesInfo);
                    if (intRetVal > 0)
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "31");//base.RECORD_UPDATED;
                        GetIOFDetails(int.Parse(hidLOBID.Value));
                        hidFormSaved.Value = "1";
                    }
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");//base.RECORD_UPDATE_FAILED;
                        hidFormSaved.Value = "2";
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
                if (objInterestRates != null)
                    objInterestRates.Dispose();
            }
        }
        
        private Model.Maintenance.ClsInterestRatesInfo getFormValues()
        {
            ClsInterestRatesInfo objInterestRatesInfo;
            objInterestRatesInfo = new ClsInterestRatesInfo();

            if (txtIOF_PERCENTAGE.Text != "")
            {
                if (GetLanguageID() == "2")
                {
                    objInterestRatesInfo.IOF_PERCENTAGE.CurrentValue = Convert.ToDouble(txtIOF_PERCENTAGE.Text, NfiBaseCurrency);
                }
                else
                {
                    objInterestRatesInfo.IOF_PERCENTAGE.CurrentValue = Convert.ToDouble(txtIOF_PERCENTAGE.Text); //changed by aditya for bug # 1761
                }
            }           

            return objInterestRatesInfo;

        }

        private bool doValidationCheck()
        {
            try
            {
                if (txtIOF_PERCENTAGE.Text.Trim().Equals(""))
                {
                    return false;
                }                

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}