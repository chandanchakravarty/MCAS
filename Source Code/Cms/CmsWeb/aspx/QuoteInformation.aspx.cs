using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Configuration;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Application;
using Cms.Model.Client;
using Cms.Model.Quote;
using Cms.CmsWeb.Utils;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlQuote;
using WebSupergoo.ABCpdf7;


namespace Cms.CmsWeb.aspx
{
    public partial class QuoteInformation : Cms.CmsWeb.cmsbase
    {
        int intCUSTOMER_ID;
        int intPOLICY_ID;
        int intPOLICY_VERSION_ID;
        int intQQ_ID;
        ArrayList arrQQFormData = new ArrayList();
        WebSupergoo.ABCpdf7.Doc TheDoc;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "134_0";

            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnPrint.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnPrint.PermissionString = gstrSecurityXML;
            btnPrint.Attributes.Add("onclick", "javascript:Print();");

            btnFetch.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnFetch.PermissionString = gstrSecurityXML;
            btnFetch.Attributes.Add("onclick", "javascript:GetVehicleNo();");

            btnGenerate.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnGenerate.PermissionString = gstrSecurityXML;
                        
            SetSessionValues();

            string strQUOTENUM = Request.Params["QUOTE_NUM"];
            ClsQuickQuote objQQuote = new ClsQuickQuote();
            intQQ_ID = objQQuote.GetQuickQuoteDetails(hidCUSTOMER_ID.Value, strQUOTENUM);

            if (!Page.IsPostBack)
            {
                FillDropDown();

                if (intCUSTOMER_ID != -100)
                {
                    FillCustomerControl();
                }

                LoadData();

            }
        }


        private void FillCustomerControl()
        {
            DataSet dsCust = ClsCustomer.GetCustomerDetails(intCUSTOMER_ID);

            DataTable dt = dsCust.Tables[0];
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["CUSTOMER_ADDRESS1"] != System.DBNull.Value)
                {
                    txtAddress1.Text = dt.Rows[0]["CUSTOMER_ADDRESS1"].ToString();
                }

                if (dt.Rows[0]["CUSTOMER_ADDRESS2"] != System.DBNull.Value)
                {
                    txtAddress2.Text = dt.Rows[0]["CUSTOMER_ADDRESS2"].ToString();
                }

                if (dt.Rows[0]["CUSTOMER_CITY"] != System.DBNull.Value)
                {
                    txtCity.Text = dt.Rows[0]["CUSTOMER_CITY"].ToString();
                }

                //if (dt.Rows[0]["CUSTOMER_STATE"] != System.DBNull.Value)
                //{
                //    txtState.Text = dt.Rows[0]["CUSTOMER_STATE"].ToString();
                //}

                if (dt.Rows[0]["CUSTOMER_ZIP"] != System.DBNull.Value)
                {
                    txtZIP.Text = dt.Rows[0]["CUSTOMER_ZIP"].ToString();
                }

                if (dt.Rows[0]["MARITAL_STATUS"] != System.DBNull.Value && dt.Rows[0]["MARITAL_STATUS"].ToString().Trim() != "")
                {
                    cmbMaritalStatus.SelectedValue = dt.Rows[0]["MARITAL_STATUS"].ToString().Trim();
                }

                //if (dt.Rows[0]["CUSTOMER_CONTACT_NO"] != System.DBNull.Value)
                //{
                //    txtContactNum.Text = dt.Rows[0]["CUSTOMER_CONTACT_NO"].ToString();
                //}

                //if (dt.Rows[0]["COUNTRY_NAME"] != System.DBNull.Value)
                //{
                //    lblNationality.Text = dt.Rows[0]["COUNTRY_NAME"].ToString();
                //}
            }
        }


        private void SetSessionValues()
        {
            hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"];
            SetCustomerID(hidCUSTOMER_ID.Value);
            intCUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);

            hidPOLICY_ID.Value = Request.Params["POLICY_ID"];
            SetPolicyID(hidPOLICY_ID.Value);
            intPOLICY_ID = int.Parse(hidPOLICY_ID.Value);

            hidPOLICY_VERSION_ID.Value = Request.Params["POLICY_VERSION_ID"];
            SetPolicyVersionID(hidPOLICY_VERSION_ID.Value);
            intPOLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
        }

        private void FillDropDown()
        {
            cmbMaritalStatus.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MARST");
            cmbMaritalStatus.DataTextField = "LookupDesc";
            cmbMaritalStatus.DataValueField = "LookupID";
            cmbMaritalStatus.DataBind();
            cmbMaritalStatus.Items.Insert(0, "");
            cmbMaritalStatus.SelectedIndex = -1;

            //ClsAgency objAgency = new ClsAgency();
            //DataSet objDataSet = objAgency.FillAgency();
            //Changed by Kuldeep for TFS 2912 on 12-03-2012
            Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQQDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();
            DataSet objDataSet=objQQDetails.GetInsuranceComapniesDataset();
            cmbEXISTING_INSURER.Items.Clear();
            cmbEXISTING_INSURER.DataSource = objDataSet.Tables[0];
            //cmbEXISTING_INSURER.DataTextField					=	"AGENCY_DISPLAY_NAME";
            cmbEXISTING_INSURER.DataTextField = "REIN_COMAPANY_NAME";
            cmbEXISTING_INSURER.DataValueField = "REIN_COMAPANY_ID";
            //till here
            cmbEXISTING_INSURER.DataBind();
            cmbEXISTING_INSURER.Items.Insert(0, new ListItem("", ""));
            cmbEXISTING_INSURER.SelectedIndex = -1;
        }

        private void LoadData()
        {
            Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQuoteDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();
            DataSet dsRate = objQuoteDetails.FetchMotorQuoteDetail(intCUSTOMER_ID, intPOLICY_ID, intPOLICY_VERSION_ID);
            DataTable dtRate = dsRate.Tables[0];

            if (dtRate.Rows.Count != 0)
            {
                if (dtRate.Rows[0]["DATE_OF_QUOTATION"] != System.DBNull.Value)
                {
                    lblDOQ.Text = dtRate.Rows[0]["DATE_OF_QUOTATION"].ToString();
                }

                if (dtRate.Rows[0]["APP_NUMBER"] != System.DBNull.Value)
                {
                    lblQQ_NO.Text = dtRate.Rows[0]["APP_NUMBER"].ToString();
                }

                if (dtRate.Rows[0]["AGENCY_NAME"] != System.DBNull.Value)
                {
                    lblAGENT.Text = dtRate.Rows[0]["AGENCY_NAME"].ToString();
                }

                if (dtRate.Rows[0]["FINAL_PREMIUM"] != System.DBNull.Value)
                {
                    //lblTOTAL_PREMIUM.Text = dtRate.Rows[0]["FINAL_PREMIUM"].ToString();
                    lblTOTAL_PREMIUM.Text = String.Format("{0:0,0.00}", Convert.ToDouble(dtRate.Rows[0]["FINAL_PREMIUM"].ToString()));
                }

                if (dtRate.Rows[0]["NAMED_DRIVER_AMT"] != System.DBNull.Value)
                {
                    //lblEXCESS_NAMED.Text = dtRate.Rows[0]["NAMED_DRIVER_AMT"].ToString();
                    lblEXCESS_NAMED.Text = String.Format("{0:0,0.00}", Convert.ToDouble(dtRate.Rows[0]["NAMED_DRIVER_AMT"].ToString()));
                }

                if (dtRate.Rows[0]["UNNAMED_DRIVER_AMT"] != System.DBNull.Value)
                {
                    //lblEXCESS_UNNAMED.Text = dtRate.Rows[0]["UNNAMED_DRIVER_AMT"].ToString();
                    lblEXCESS_UNNAMED.Text = String.Format("{0:0,0.00}", Convert.ToDouble(dtRate.Rows[0]["UNNAMED_DRIVER_AMT"].ToString()));
                }

                if (dtRate.Rows[0]["CUSTOMER_TYPE_DESC"] != System.DBNull.Value)
                {
                    lblStatusType.Text = dtRate.Rows[0]["CUSTOMER_TYPE_DESC"].ToString();
                }

                string fName = "";
                string MName = "";
                string LName = "";

                if (dtRate.Rows[0]["CUSTOMER_FIRST_NAME"] != System.DBNull.Value)
                {
                    fName = dtRate.Rows[0]["CUSTOMER_FIRST_NAME"].ToString();
                }

                if (dtRate.Rows[0]["CUSTOMER_MIDDLE_NAME"] != System.DBNull.Value)
                {
                    MName = dtRate.Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString();
                }

                if (dtRate.Rows[0]["CUSTOMER_LAST_NAME"] != System.DBNull.Value)
                {
                    LName = dtRate.Rows[0]["CUSTOMER_LAST_NAME"].ToString();
                }

                if (MName == "" || MName == null)
                {
                    lblName.Text = fName + ' ' + LName;
                }
                else
                {
                    lblName.Text = fName + ' ' + MName + ' ' + LName;
                }


                if (dtRate.Rows[0]["CUSTOMER_ADDRESS1"] != System.DBNull.Value)
                {
                    txtAddress1.Text = dtRate.Rows[0]["CUSTOMER_ADDRESS1"].ToString();
                }

                if (dtRate.Rows[0]["CUSTOMER_ADDRESS2"] != System.DBNull.Value)
                {
                    txtAddress2.Text = dtRate.Rows[0]["CUSTOMER_ADDRESS2"].ToString();
                }

                if (dtRate.Rows[0]["CUSTOMER_CITY"] != System.DBNull.Value)
                {
                    txtCity.Text = dtRate.Rows[0]["CUSTOMER_CITY"].ToString();
                }

                if (dtRate.Rows[0]["CUSTOMER_STATE"] != System.DBNull.Value)
                {
                    txtState.Text = dtRate.Rows[0]["CUSTOMER_STATE"].ToString();
                }

                if (dtRate.Rows[0]["CUSTOMER_ZIP"] != System.DBNull.Value)
                {
                    txtZIP.Text = dtRate.Rows[0]["CUSTOMER_ZIP"].ToString();
                }

                if (dtRate.Rows[0]["CUSTOMER_CONTACT_NO"] != System.DBNull.Value)
                {
                    txtContactNum.Text = dtRate.Rows[0]["CUSTOMER_CONTACT_NO"].ToString();
                }

                if (dtRate.Rows[0]["COUNTRY_NAME"] != System.DBNull.Value)
                {
                    lblNationality.Text = dtRate.Rows[0]["COUNTRY_NAME"].ToString();
                }

                if (dtRate.Rows[0]["PASSPORT_NO"] != System.DBNull.Value)
                {
                    txtPassport.Text = dtRate.Rows[0]["PASSPORT_NO"].ToString();
                }

                if (dtRate.Rows[0]["GENDER"] != System.DBNull.Value)
                {
                    string strSex = dtRate.Rows[0]["GENDER"].ToString();

                    if (strSex == "F")
                    {
                        lblGENDER.Text = "Female";
                    }
                    else
                    {
                        lblGENDER.Text = "Male";
                    }
                }

                if (dtRate.Rows[0]["DATE_OF_BIRTH"] != System.DBNull.Value)
                {
                    lblDOB.Text = String.Format("{0:dd/MM/yyyy}",Convert.ToDateTime(dtRate.Rows[0]["DATE_OF_BIRTH"].ToString()));
                }

                if (dtRate.Rows[0]["MARITAL_STATUS"] != System.DBNull.Value)
                {
                    cmbMaritalStatus.SelectedValue = dtRate.Rows[0]["MARITAL_STATUS"].ToString().Trim();
                }

                if (dtRate.Rows[0]["IS_HOME_EMPLOYEE"] != System.DBNull.Value)
                {
                    string strOCC_TYPE = dtRate.Rows[0]["IS_HOME_EMPLOYEE"].ToString();

                    if (strOCC_TYPE == "1")
                    {
                        lblOCCUPATION.Text = "Indoor";
                    }
                    else
                    {
                        lblOCCUPATION.Text = "Outdoor";
                    }
                }

                if (dtRate.Rows[0]["DEMERIT_DISCOUNT"] != System.DBNull.Value)
                {
                    string Demerit_Desc = dtRate.Rows[0]["DEMERIT_DISCOUNT"].ToString();
                    if (Demerit_Desc == "1")
                        lblDemerit.Text = "Yes";
                    else
                        lblDemerit.Text = "No";
                }

                if (dtRate.Rows[0]["DRIVER_EXP_YEAR"] != System.DBNull.Value)
                {
                    lblDRIVE_EXP.Text = dtRate.Rows[0]["DRIVER_EXP_YEAR"].ToString();
                }

                if (dtRate.Rows[0]["IS_NEW"] != System.DBNull.Value)
                {
                    string strIs_New = dtRate.Rows[0]["IS_NEW"].ToString();

                    if (strIs_New == "1")
                    {
                        rbIS_NEW_YES.Checked = true;
                    }
                    else
                    {
                        rbIS_NEW_NO.Checked = true;
                    }
                }
                else
                {
                    rbIS_NEW_NO.Checked = true;
                }

                if (dtRate.Rows[0]["DATE_LTA_REGISTRATION"] != System.DBNull.Value)
                {
                    DateTime dtLTA = DateTime.Parse(dtRate.Rows[0]["DATE_LTA_REGISTRATION"].ToString());

                    txtREG_DD.Text = dtLTA.Day.ToString();
                    txtREG_MM.Text = dtLTA.Month.ToString();
                    txtREG_YYYY.Text = dtLTA.Year.ToString();
                }

                if (dtRate.Rows[0]["COVER_NOTE_NO"] != System.DBNull.Value)
                {
                    txtNoteNum.Text = dtRate.Rows[0]["COVER_NOTE_NO"].ToString();
                }

                if (dtRate.Rows[0]["REGISTRATION_NO"] != System.DBNull.Value)
                {
                    txtREG_NO.Text = dtRate.Rows[0]["REGISTRATION_NO"].ToString();
                }

                if (dtRate.Rows[0]["YEAR_OF_REG"] != System.DBNull.Value)
                {
                    lblYEAR_OF_REG.Text = dtRate.Rows[0]["YEAR_OF_REG"].ToString();
                }

                if (dtRate.Rows[0]["MAKE_DESC"] != System.DBNull.Value)
                {
                    lblMAKE.Text = dtRate.Rows[0]["MAKE_DESC"].ToString();
                }

                if (dtRate.Rows[0]["MODEL_DESC"] != System.DBNull.Value)
                {
                    lblMODEL.Text = dtRate.Rows[0]["MODEL_DESC"].ToString();
                }

                if (dtRate.Rows[0]["MODEL_TYPE_DESC"] != System.DBNull.Value)
                {
                    lblVEHICLE_TYPE.Text = dtRate.Rows[0]["MODEL_TYPE_DESC"].ToString();
                }

                if (dtRate.Rows[0]["ENG_CAPACITY"] != System.DBNull.Value)
                {
                    lblENG_CAPACITY.Text = dtRate.Rows[0]["ENG_CAPACITY"].ToString();
                }

                if (dtRate.Rows[0]["ENGINE_NO"] != System.DBNull.Value)
                {
                    txtENGINE_NO.Text = dtRate.Rows[0]["ENGINE_NO"].ToString();
                }

                if (dtRate.Rows[0]["CHASSIS_NO"] != System.DBNull.Value)
                {
                    txtChassisNo.Text = dtRate.Rows[0]["CHASSIS_NO"].ToString();
                }

                if (dtRate.Rows[0]["IS_UNDER_HIRE"] != System.DBNull.Value)
                {
                    string is_hired = dtRate.Rows[0]["IS_UNDER_HIRE"].ToString();

                    if (is_hired == "1")
                    {
                        rbHIRE_YES.Checked = true;
                    }
                    else
                    {
                        rbHIRE_NO.Checked = true;
                    }
                }

                if (dtRate.Rows[0]["FINANCE_COMP_NAME"] != System.DBNull.Value)
                {
                    txtNAME_FIN_COMP.Text = dtRate.Rows[0]["FINANCE_COMP_NAME"].ToString();
                }

                if (dtRate.Rows[0]["EXISTING_NCD"] != System.DBNull.Value)
                {
                    lblEXISTING_NCD.Text = dtRate.Rows[0]["EXISTING_NCD"].ToString();
                }

                if (dtRate.Rows[0]["EXISTING_INSURER"] != System.DBNull.Value)
                {
                    cmbEXISTING_INSURER.SelectedValue = dtRate.Rows[0]["EXISTING_INSURER"].ToString();
                }

                if (dtRate.Rows[0]["EXISTING_POL_NUM"] != System.DBNull.Value)
                {
                    txtEXIST_POL_NUM.Text = dtRate.Rows[0]["EXISTING_POL_NUM"].ToString();
                }

                if (dtRate.Rows[0]["EXIST_POL_EXP_DATE"] != System.DBNull.Value)
                {
                    DateTime dtPol_Exp_Date = DateTime.Parse(dtRate.Rows[0]["EXIST_POL_EXP_DATE"].ToString());

                    txtEXP_DD.Text = dtPol_Exp_Date.Day.ToString();
                    txtEXP_MM.Text = dtPol_Exp_Date.Month.ToString();
                    txtEXP_YY.Text = dtPol_Exp_Date.Year.ToString();
                }

                if (dtRate.Rows[0]["VEHICLE_NO"] != System.DBNull.Value)
                {
                    txtVehicleNo.Text = dtRate.Rows[0]["VEHICLE_NO"].ToString();
                }

                if (dtRate.Rows[0]["COV_DES"] != System.DBNull.Value)
                {
                    lblType.Text = dtRate.Rows[0]["COV_DES"].ToString();
                }

                if (dtRate.Rows[0]["NO_CLAIM_DISCOUNT"] != System.DBNull.Value)
                {
                    string strClaim_Desc = dtRate.Rows[0]["NO_CLAIM_DISCOUNT"].ToString();

                    if (strClaim_Desc == "1")
                        lblCLAIM_DISC.Text = "Yes";
                    else
                        lblCLAIM_DISC.Text = "No";
                }

                if (dtRate.Rows[0]["POLICY_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    DateTime dtExp = DateTime.Parse(dtRate.Rows[0]["POLICY_EFFECTIVE_DATE"].ToString());
                    txtFROM_DAY.Text = dtExp.Day.ToString();
                    txtFROM_MONTH.Text = dtExp.Month.ToString();
                    txtFROM_YEAR.Text = dtExp.Year.ToString();
                }

                if (dtRate.Rows[0]["POLICY_EXPIRATION_DATE"] != System.DBNull.Value)
                {
                    DateTime dtEff = DateTime.Parse(dtRate.Rows[0]["POLICY_EXPIRATION_DATE"].ToString());
                    txtTO_DAY.Text = dtEff.Day.ToString();
                    txtTO_MONTH.Text = dtEff.Month.ToString();
                    txtTO_YEAR.Text = dtEff.Year.ToString();
                }

                if (dtRate.Rows[0]["IS_DEMERIT_POINT"] != System.DBNull.Value)
                {
                    string Has_Demerit = dtRate.Rows[0]["IS_DEMERIT_POINT"].ToString();
                    if (Has_Demerit.Trim() == "1")
                        rbDEMERIT_YES.Checked = true;
                    else
                        rbDEMERIT_NO.Checked = true;
                }
                else
                {
                    rbDEMERIT_NO.Checked = true;
                }

                if (dtRate.Rows[0]["DEMERIT_DESC"] != System.DBNull.Value)
                {
                    txtDemerit_Desc.Text = dtRate.Rows[0]["DEMERIT_DESC"].ToString();
                }

                if (dtRate.Rows[0]["IS_REJECTED"] != System.DBNull.Value)
                {
                    string Is_Rejected = dtRate.Rows[0]["IS_REJECTED"].ToString();
                    if (Is_Rejected.Trim() == "1")
                        rbREJECT_YES.Checked = true;
                    else
                        rbREJECT_NO.Checked = true;
                }
                else
                {
                    rbREJECT_NO.Checked = true;
                }

                if (dtRate.Rows[0]["REJECTED_DESC"] != System.DBNull.Value)
                {
                    txtReject_Desc.Text = dtRate.Rows[0]["REJECTED_DESC"].ToString();
                }

                if (dtRate.Rows[0]["IS_DISEASE"] != System.DBNull.Value)
                {
                    string Is_Disease = dtRate.Rows[0]["IS_DISEASE"].ToString();
                    if (Is_Disease.Trim() == "1")
                        rbDISEASE_YES.Checked = true;
                    else
                        rbDISEASE_NO.Checked = true;
                }
                else
                {
                    rbDISEASE_NO.Checked = true;
                }

                if (dtRate.Rows[0]["DISEASE_DESC"] != System.DBNull.Value)
                {
                    txtDisease_Desc.Text = dtRate.Rows[0]["DISEASE_DESC"].ToString();
                }





            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Saving the values into the database
            SaveFormValue();

            LoadData();

        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {                
                TheDoc = new WebSupergoo.ABCpdf7.Doc();
                TheDoc.SetInfo(0, "HostWebBrowser", "0");

                
                string strHTML = GetVehiclePdfHtml();
                //GeneratePdf(strHTML, ref TheDoc);                
                
                int theID = 0;

                TheDoc.Rect.String = "3 5 590 735";
                theID = TheDoc.AddImageHtml(strHTML.ToString());
                //theID = TheDoc.AddImageHtml("<html><head></head><body><table><tr><td>HELLO WORLD</td></tr></table></body></html>");
                
                string strFilePath = ClsCommon.GetKeyValueWithIP("VehiclePdfFileName");

                if (File.Exists(strFilePath))
                {
                    File.Delete(strFilePath);
                }
                TheDoc.Save(strFilePath);

                string EncryptedPath = ClsCommon.CreateContentViewerURL(strFilePath, FILE_TYPE_PDF);
                //Response.Write("<script language='javascript'> window.open('" + EncryptedPath + "'); </script>");
                string pdfPath = System.Configuration.ConfigurationManager.AppSettings["PdfDocumentPathURL"].ToString();
                Response.Write("<script language='javascript'> window.open('" + pdfPath + "'); </script>");
            }
            catch (Exception ex)
            {
                //Response.Write("<script language='javascript'> alert('Error'); </script>");
                throw (new Exception("Error in Document printing:  " + ex.Message.ToString(), ex.InnerException));

            }


        }



        private void GeneratePdf(string HtmlFile, ref WebSupergoo.ABCpdf7.Doc TheDoc)
        {
            try
            {
                //Response.Write("<script language='javascript'> alert('Inside GeneratePdf'); </script>");
                int theID = 0;
                      
                TheDoc.Rect.String = "3 5 585 735";
                theID = TheDoc.AddImageHtml(HtmlFile.ToString());                
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'> alert('Error in GeneratePdf'); </script>");
                throw (new Exception("Error in Generating COI:  " + ex.Message.ToString(), ex.InnerException));

            }
        }

        protected string GetVehiclePdfHtml()
        {
            try
            {
                string strPdfXml = "";
                XmlDocument xPdfDocument = new XmlDocument();
                XslTransform XslDocument = new XslTransform();

                Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQQDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();
                DataSet dsRate = objQQDetails.GetQuickQuoteVehicleXml(intCUSTOMER_ID, intPOLICY_ID, intPOLICY_VERSION_ID);
                DataTable dtRate = dsRate.Tables[0];
                if (dtRate.Rows.Count != 0)
                {
                    if (dtRate.Rows[0][0] != System.DBNull.Value)
                    {
                        strPdfXml = dtRate.Rows[0][0].ToString();
                    }
                }

                if (strPdfXml != "" || strPdfXml == null)
                {
                    xPdfDocument.LoadXml(strPdfXml);
                }

                string strXSLPath = ClsCommon.GetKeyValueWithIP("QuickQuoteVehiclePdfXSLPath");
                XslDocument.Load(strXSLPath);

                XPathNavigator nav = ((IXPathNavigable)xPdfDocument).CreateNavigator();
                StringWriter swGetHTML = new StringWriter();
                XslDocument.Transform(nav, null, swGetHTML);
                string strReturnCoverHtml = swGetHTML.ToString();


                return strReturnCoverHtml;
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'> alert('Error in Generating HTML'); </script>");
                throw (new Exception("Error in html Document generation:  " + ex.Message.ToString(), ex.InnerException));

            }

        }

        private void SaveFormValue()
        {
            Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQQDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();
            objQQDetails.TransactionLogRequired = true;

            Model.Quote.ClsQuoteDetailsInfo objNewQuoteInfo = new Cms.Model.Quote.ClsQuoteDetailsInfo();
            Model.Quote.ClsCustomerParticluarInfo objCustomerInfo = new Cms.Model.Quote.ClsCustomerParticluarInfo();

            getFormValue(objNewQuoteInfo, objCustomerInfo);

            objNewQuoteInfo.CUSTOMER_ID = intCUSTOMER_ID;
            objNewQuoteInfo.POLICY_ID = intPOLICY_ID;
            objNewQuoteInfo.POLICY_VERSION_ID = intPOLICY_VERSION_ID;

            int RetVal = objQQDetails.Update_Motor_QQ_Detail(objNewQuoteInfo, objCustomerInfo);

            if (RetVal > 0)			// Value inserted successfully.
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2050");
            }
            else
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2053");
            }

            lblMessage.Visible = true;

        }

        private void getFormValue(ClsQuoteDetailsInfo objQQDetail,ClsCustomerParticluarInfo objCustDetail)
        {
            objCustDetail.CUSTOMER_ADDRESS1 = txtAddress1.Text.Trim();
            objCustDetail.CUSTOMER_ADDRESS2 = txtAddress2.Text.Trim();
            objCustDetail.CUSTOMER_CITY = txtCity.Text.Trim();
            objCustDetail.CUSTOMER_STATE = txtState.Text.Trim();
            objCustDetail.CUSTOMER_ZIP = txtZIP.Text.Trim();
            objCustDetail.CUSTOMER_CONTACT_NO = txtContactNum.Text.Trim();
            objCustDetail.PASSPORT_NO = txtPassport.Text.Trim();
            objCustDetail.MARITAL_STATUS = cmbMaritalStatus.SelectedValue;
            objCustDetail.EXISTING_INSURER = cmbEXISTING_INSURER.SelectedValue;
            objCustDetail.EXISTING_POL_NUM = txtEXIST_POL_NUM.Text.Trim();

            if (txtEXP_MM.Text.Trim() != "" && txtEXP_MM.Text.Trim() != null && txtEXP_DD.Text.Trim() != "" && txtEXP_DD.Text.Trim() != null && txtEXP_YY.Text.Trim() != "" && txtEXP_YY.Text.Trim() != null)
            {
                objCustDetail.EXIST_POL_EXP_DATE = txtEXP_MM.Text.Trim() + "/" + txtEXP_DD.Text.Trim() + "/" + txtEXP_YY.Text.Trim();
            }

            objCustDetail.VEHICLE_NO = txtVehicleNo.Text.Trim();
            

            if (rbIS_NEW_YES.Checked)
                objQQDetail.IS_NEW = "1";
            else
                objQQDetail.IS_NEW = "0";

            if (txtREG_MM.Text.Trim() != "" && txtREG_MM.Text.Trim() != null && txtREG_DD.Text.Trim() != "" && txtREG_DD.Text.Trim() != null && txtREG_YYYY.Text.Trim() != "" && txtREG_YYYY.Text.Trim() != null)
            {
                objQQDetail.DATE_LTA_REGISTRATION = txtREG_MM.Text.Trim() + "/" + txtREG_DD.Text.Trim() + "/" + txtREG_YYYY.Text.Trim();
            }

            objQQDetail.COVER_NOTE_NO = txtNoteNum.Text.Trim();
            objQQDetail.REGISTRATION_NO = txtREG_NO.Text.Trim();
            objQQDetail.ENGINE_NO = txtENGINE_NO.Text.Trim();
            objQQDetail.CHASSIS_NO = txtChassisNo.Text.Trim();

            if (rbHIRE_YES.Checked)
                objQQDetail.IS_UNDER_HIRE = "1";
            else
                objQQDetail.IS_UNDER_HIRE = "0";

            objQQDetail.FINANCE_COMP_NAME = txtNAME_FIN_COMP.Text.Trim();

            if (rbDEMERIT_YES.Checked)
            {
                objQQDetail.IS_DEMERIT_POINT = "1";
                objQQDetail.DEMERIT_DESC = txtDemerit_Desc.Text;
            }
            else
            {
                objQQDetail.IS_DEMERIT_POINT = "0";
                objQQDetail.DEMERIT_DESC = txtDemerit_Desc.Text;
            }

            
            if (rbREJECT_YES.Checked)
            {
                objQQDetail.IS_REJECTED = "1";
                objQQDetail.REJECTED_DESC = txtReject_Desc.Text;
            }
            else
            {
                objQQDetail.IS_REJECTED = "0";
                objQQDetail.REJECTED_DESC ="";
            }

            

            if (rbDISEASE_YES.Checked)
            {
                objQQDetail.IS_DISEASE = "1";
                objQQDetail.DISEASE_DESC = txtDisease_Desc.Text;
            }
            else
            {
                objQQDetail.IS_DISEASE = "0";
                objQQDetail.DISEASE_DESC = "";
            }

            








        }
    }
}
