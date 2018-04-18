/******************************************************************************************

Created By				: Ruchika Chauhan
Dated					: 12th December 2011
Purpose					: To enterDriver Information from within the QuoteInformation page 
  
<Modified Date			: 3rd January 2012- > 
<Modified By			: Ruchika Chauhan- > 
<Purpose				: Added some functions to make the page functional.- > 
******************************************************************************************/

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
using System.IO;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Quote;

namespace Cms.CmsWeb.aspx
{
    /// <summary>
    /// Summary description for QuoteDriverInformation.
    /// </summary>
    public class QuoteDriverInformation : Cms.CmsWeb.cmsbase
    {

        protected System.Web.UI.WebControls.Label capDriverName;
        protected System.Web.UI.WebControls.TextBox txtDriverName;

        protected System.Web.UI.WebControls.Label capDriverCode;
        protected System.Web.UI.WebControls.TextBox txtDriverCode;

        protected System.Web.UI.WebControls.Label capDriverGender;
        protected System.Web.UI.WebControls.DropDownList cmbDriverGender;

        protected System.Web.UI.WebControls.Label capDriverDOB;
        protected System.Web.UI.WebControls.TextBox txtDriverDOB;
        protected System.Web.UI.WebControls.HyperLink hlkDriverDOB;
        protected System.Web.UI.WebControls.Image imgDriverDOB;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDriverDOB;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDriverDOB;
        protected System.Web.UI.WebControls.CustomValidator csvDriverDOB;


        protected System.Web.UI.WebControls.Label capDriverDateLicensed;
        protected System.Web.UI.WebControls.TextBox txtDriverDateLicensed;
        protected System.Web.UI.WebControls.HyperLink hlkDriverDateLicensed;
        protected System.Web.UI.WebControls.Image imgDriverDateLicensed;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDriverDateLicensed;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDriverDateLicensed;
        protected System.Web.UI.WebControls.CustomValidator csvDriverDateLicensed;

        protected System.Web.UI.WebControls.Label capDriverLicenseNo;
        protected System.Web.UI.WebControls.TextBox txtDriverLicenseNo;
        protected System.Web.UI.WebControls.HyperLink hlkDriverLicenseNo;
        protected System.Web.UI.WebControls.Image imgDriverLicenseNo;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDriverLicenseNo;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDriverLicenseNo;
        protected System.Web.UI.WebControls.CustomValidator csvDriverLicenseNo;

        protected System.Web.UI.WebControls.DropDownList cmbDriverType;

        protected System.Web.UI.WebControls.DropDownList cmbDriverDrugViolation;

        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;

        protected System.Web.UI.WebControls.Label lblMessage;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidQUOTE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_TYPE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_DRUG_VIOLATION;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_DRUG_VIOLATION;

        private int No_of_drivers = 0;
        private static int TotalDrivers = 0;
        private int flag = 0;

        private String XmlSchemaFileName = "";
        private String XmlFullFilePath = "";
        ClsQuoteDriverInformation objQuoteDriverInformation;

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
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion


        private void Page_Load(object sender, System.EventArgs e)
        {
            base.ScreenId = "134_0";

            string strSysID = GetSystemId();


            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            XmlSchemaFileName = "QuoteDriverInformation.xml";
            XmlFullFilePath = Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/" + XmlSchemaFileName;

            if (!Page.IsPostBack)
            {
                TotalDrivers = 0;

                hlkDriverDOB.Attributes.Add("onclick", "fPopCalendar(document.APP_Q_Driver_Info.txtDriverDOB, document.APP_Q_Driver_Info.txtDriverDOB)");
                hlkDriverDateLicensed.Attributes.Add("onclick", "fPopCalendar(document.APP_Q_Driver_Info.txtDriverDateLicensed, document.APP_Q_Driver_Info.txtDriverDateLicensed)");

                //btnReset.Attributes.Add("onclick", "javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

                FillComboBox();

                //Added on 5-Jan-2012 for TFS # 1000
                if ((Session["TotalDrivers"] != null) && (Session["TotalDrivers"].ToString() != ""))
                {
                    No_of_drivers = int.Parse(Session["TotalDrivers"].ToString());
                    TotalDrivers = No_of_drivers;
                }
                else if (Session["TotalDrivers"] == null)
                {
                    No_of_drivers = 0;
                    TotalDrivers = No_of_drivers;
                }             

                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + strSysID, "QuoteDriverInformation.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID + "/QuoteDriverInformation.xml");
            }

                     
        }

        private void FillComboBox()
        {
            cmbDriverType.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MTD");
            cmbDriverType.DataTextField = "LookupDesc";
            cmbDriverType.DataValueField = "LookupID";
            cmbDriverType.DataBind();
            cmbDriverType.Items.Insert(0, new ListItem("", ""));

            cmbDriverDrugViolation.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            cmbDriverDrugViolation.DataTextField = "LookupDesc";
            cmbDriverDrugViolation.DataValueField = "LookupID";
            cmbDriverDrugViolation.DataBind();
            cmbDriverDrugViolation.Items.Insert(0, "");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TotalDrivers > 0)
                {

                    flag = 1;
                    //For retreiving the return value of business class save function
                    objQuoteDriverInformation = new ClsQuoteDriverInformation();
                    objQuoteDriverInformation.LoggedInUserId = int.Parse(GetUserId());

                    //Retreiving the form values into model class object
                    ClsQuoteDriverInformationInfo objQuoteDriverInfInfo = GetFormValue();

                    objQuoteDriverInfInfo.CREATED_BY = int.Parse(GetUserId());
                    objQuoteDriverInfInfo.CREATED_DATETIME = DateTime.Now;
                    objQuoteDriverInfInfo.IS_ACTIVE = "Y";

                    //Calling the add method of business layer class
                    int RetVal = objQuoteDriverInformation.Add(objQuoteDriverInfInfo);

                    if (RetVal > 0)
                    {
                        hidQUOTE_ID.Value = objQuoteDriverInfInfo.QUOTE_ID.ToString();
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "37");
                        hidFormSaved.Value = "1";
                        hidIS_ACTIVE.Value = "Y";
                    }
                    --TotalDrivers;
                    if (TotalDrivers > 0)
                    {
                        lblMessage.Text = "Enter New Driver Information";
                    }
                    else
                    {                        
                        lblMessage.Text = "Data Saved.";
                    }
                    clearForm();
                }
                else
                {
                    lblMessage.Text = "Data cannot be saved.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "36") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value	=	"2";
            }
            finally
            {
                if (objQuoteDriverInformation != null)
                    objQuoteDriverInformation.Dispose();
            }

        }//End of Save


        private void clearForm()
        {
            txtDriverCode.Text = "";
            txtDriverDateLicensed.Text = "";
            txtDriverDOB.Text = "";
            txtDriverLicenseNo.Text = "";
            txtDriverName.Text = "";
            cmbDriverDrugViolation.SelectedValue = "";
            cmbDriverGender.SelectedValue = "M";
            cmbDriverType.SelectedValue = "";         
        }


        private ClsQuoteDriverInformationInfo GetFormValue()
        {

            //Creating the Model object for holding the New data
            ClsQuoteDriverInformationInfo objQuoteDriverInfInfo = new ClsQuoteDriverInformationInfo();

            if ((Session["QUOTE_ID"] != null) && (Session["QUOTE_ID"].ToString() != ""))
            {
                objQuoteDriverInfInfo.QUOTE_ID = int.Parse(Session["QUOTE_ID"].ToString());
            }
            else
            {
                objQuoteDriverInfInfo.QUOTE_ID = int.Parse(hidQUOTE_ID.Value);
                //objQuoteDriverInfInfo.QUOTE_ID = hidQUOTE_ID.Value;
            }

            objQuoteDriverInfInfo.DRIVER_NAME = txtDriverName.Text;
            objQuoteDriverInfInfo.DRIVER_CODE = txtDriverCode.Text;
            objQuoteDriverInfInfo.DRIVER_GENDER = cmbDriverGender.SelectedValue;

            if (txtDriverDOB.Text != "")
            {
                objQuoteDriverInfInfo.DRIVER_DOB = ConvertToDate(txtDriverDOB.Text);
            }

            if (cmbDriverType.SelectedValue != "")
                objQuoteDriverInfInfo.DRIVER_TYPE = cmbDriverType.SelectedValue.Trim();
            else
            {                
                if (!string.IsNullOrEmpty(hidDRIVER_TYPE.Value) && hidDRIVER_TYPE.Value != "0")
                    objQuoteDriverInfInfo.DRIVER_TYPE = hidDRIVER_TYPE.Value.Trim();
            }

            objQuoteDriverInfInfo.DRIVER_LICENSE_NO = txtDriverLicenseNo.Text;

            if (txtDriverDateLicensed.Text != "")
            {
                objQuoteDriverInfInfo.DRIVER_LICENSED_DATE = ConvertToDate(txtDriverDateLicensed.Text);
            }

            if (cmbDriverDrugViolation.SelectedValue != "")
                objQuoteDriverInfInfo.DRIVER_DRUG_VIOLATION = cmbDriverDrugViolation.SelectedValue.Trim();
            else
            {
                if (!string.IsNullOrEmpty(hidDRIVER_DRUG_VIOLATION.Value) && hidDRIVER_DRUG_VIOLATION.Value != "0")
                    objQuoteDriverInfInfo.DRIVER_DRUG_VIOLATION = hidDRIVER_DRUG_VIOLATION.Value.Trim();
            }
            objQuoteDriverInfInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

            return objQuoteDriverInfInfo;

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearForm();
        }
    }
}

        
  

