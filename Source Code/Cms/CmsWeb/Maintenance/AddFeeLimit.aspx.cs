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
    public partial class AddFeeLimit : Cms.CmsWeb.cmsbase
    {
        ClsInterestRates objInterestRates = new ClsInterestRates();
        System.Resources.ResourceManager objresource;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "561_0";
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;
            objresource = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddFeeLimit", System.Reflection.Assembly.GetExecutingAssembly());
            if (GetLanguageID() == "2")  //Added by aditya for bug # 1761
            {
                txtMAXIMUM_LIMIT.Attributes.Add("onblur", "javascript:this.value=formatRateTextValue(this.value,4);formatRateBase(this.value);ValidatorOnChange();");
            }
            else
            {
                txtMAXIMUM_LIMIT.Attributes.Add("onblur", "javascript:this.value=formatRateTextValue(this.value,1);formatCurrencyRate(this.value);ValidatorOnChange();");
            }
            if (GetLanguageID() == "2")
            {
                txtFEES_PERCENTAGE.Attributes.Add("onblur", "javascript:this.value=formatRateTextValue(this.value,4);formatRateBase(this.value);ValidatorOnChange();");
            }
            else
            {
                txtFEES_PERCENTAGE.Attributes.Add("onblur", "javascript:this.value=formatRateTextValue(this.value,1);formatCurrencyRate(this.value);ValidatorOnChange();"); //Added till here
            }
            
            if (!Page.IsPostBack)
            {
                SetCaption();
                SetErrorMessages();
                SetFormValues();               
                GetOldXML();
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
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void GetOldXML()
        {
            ClsInterestRates objInterestRates = new ClsInterestRates();
            ClsInterestRatesInfo objInterestRatesInfo = new ClsInterestRatesInfo();            
            hidOldData.Value = objInterestRates.GetOldXML();            
        }

        private void SetErrorMessages()
        {
            revMAXIMUM_LIMIT.ValidationExpression = aRegExpDoublePositiveNonZero;
            revMAXIMUM_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            csvMAXIMUM_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            rfvMAXIMUM_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
            revFEES_PERCENTAGE.ValidationExpression = aRegExpDoublePositiveNonZero;
            revFEES_PERCENTAGE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            csvFEES_PERCENTAGE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            rfvFEES_PERCENTAGE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
        }

        private void SetCaption()
        {
            capMAXIMUM_LIMIT.Text = objresource.GetString("capMAXIMUM_LIMIT");
            capFEES_PERCENTAGE.Text = objresource.GetString("capFEES_PERCENTAGE");           
        }

        private Model.Maintenance.ClsInterestRatesInfo getFormValues()
        {
            ClsInterestRatesInfo objInterestRatesInfo;
            objInterestRatesInfo = new ClsInterestRatesInfo();

            if (txtMAXIMUM_LIMIT.Text != "")
            {
                if (GetLanguageID() == "2")
                {
                    objInterestRatesInfo.MAXIMUM_LIMIT.CurrentValue = Convert.ToDouble(txtMAXIMUM_LIMIT.Text, NfiBaseCurrency);
                }
                else
                {
                    objInterestRatesInfo.MAXIMUM_LIMIT.CurrentValue = Convert.ToDouble(txtMAXIMUM_LIMIT.Text); //Changed by aditya for bug # 1761
                }
            }

            if (txtFEES_PERCENTAGE.Text != "")
            {
                if (GetLanguageID() == "2")
                {
                    objInterestRatesInfo.FEES_PERCENTAGE.CurrentValue = Convert.ToDouble(txtFEES_PERCENTAGE.Text, NfiBaseCurrency);
                }
                else
                {
                    objInterestRatesInfo.FEES_PERCENTAGE.CurrentValue = Convert.ToDouble(txtFEES_PERCENTAGE.Text);
                }
            }

            return objInterestRatesInfo;

        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            //Saving form values
            SaveFormValues();
        }

        private bool doValidationCheck()
        {
            try
            {
                if (txtMAXIMUM_LIMIT.Text.Trim().Equals(""))
                {
                    return false;
                }
                if (txtFEES_PERCENTAGE.Text.Trim().Equals(""))
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


        private void SetFormValues()
        {             
            ClsSystemParams clsSystemParameter = new ClsSystemParams();
            DataSet dsDataSet = new DataSet();

            dsDataSet = clsSystemParameter.GetSystemParams();

            try
            {
                if (dsDataSet != null)
                {
                    if (dsDataSet.Tables[0].Rows.Count > 0)
                    {

                        if (GetLanguageID() == "2") //Changed by aditya for bug # 1761
                        {
                            if (dsDataSet.Tables[0].Rows[0]["MAXIMUM_LIMIT"] != System.DBNull.Value)
                            { //Added by Aditya for TFS BUG # 2425
                                txtMAXIMUM_LIMIT.Text = Convert.ToDouble(dsDataSet.Tables[0].Rows[0]["MAXIMUM_LIMIT"]).ToString("N", NfiBaseCurrency);

                            }
                        }
                        else
                            txtMAXIMUM_LIMIT.Text = Convert.ToDouble(dsDataSet.Tables[0].Rows[0]["MAXIMUM_LIMIT"]).ToString();

                        if (GetLanguageID() == "2") //Changed by aditya for bug # 1761
                        {
                            if (dsDataSet.Tables[0].Rows[0]["FEES_PERCENTAGE"] != System.DBNull.Value)
                            { //Added by Aditya for TFS BUG # 2425
                                txtFEES_PERCENTAGE.Text = Convert.ToDouble(dsDataSet.Tables[0].Rows[0]["FEES_PERCENTAGE"]).ToString("N", NfiBaseCurrency);

                            }
                        }
                        else
                            txtFEES_PERCENTAGE.Text = Convert.ToDouble(dsDataSet.Tables[0].Rows[0]["FEES_PERCENTAGE"]).ToString();
                       
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dsDataSet.Dispose();
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
                 //base.PopulateModelObject(objOldInterestRatesInfo, hidOldData.Value);

                //int ModifiedBy = int.Parse()
                if (doValidationCheck())
                {
                    ClsInterestRates objInterestRates = new ClsInterestRates();
                    objInterestRates = new ClsInterestRates();
                    intRetVal = objInterestRates.Updatefees(objInterestRatesInfo);
                    if (intRetVal > 0)
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "31");//base.RECORD_UPDATED;
                        GetOldXML();
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


                //				lblMessage.Text			=	"Error occured while saving information, Following error occured. " 
                //					+ "\n" + ex.Message + "\n Try again!";
                //				lblMessage.Visible		=	true;
            }
            finally
            {
                if (objInterestRates != null)
                    objInterestRates.Dispose();
            }
        }
    }
}