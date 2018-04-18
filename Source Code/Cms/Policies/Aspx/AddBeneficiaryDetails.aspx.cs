using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.Model.Policy;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlCommon;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.Blapplication;
using Cms.BusinessLayer.BlApplication;
using Cms.DataLayer;
using System.Xml;
using System.IO;
using Cms.EbixDataTypes;
using System.Web.UI.HtmlControls;

namespace Cms.Policies.Aspx
{
    public partial class AddBeneficiaryDetails : Cms.Policies.policiesbase
    {
        System.Resources.ResourceManager objResourceMgr;
        ClsBeneficiaryDetails objBeneficiaryDetails = new ClsBeneficiaryDetails();
      
        private String strRowId = String.Empty;
        string CalledFrom = string.Empty;
        protected System.Web.UI.HtmlControls.HtmlTable tblBody;
 protected global::System.Web.UI.HtmlControls.HtmlTableRow trBody;

        protected void Page_Load(object sender, EventArgs e)
        {
             if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
                CalledFrom = Request.QueryString["CalledFrom"].ToString().Trim();
             switch (CalledFrom)
             {
                 case "CPCACC":
                     base.ScreenId = GROUP_PERSONAL_ACCIDENTscreenId.BENEFICIARY_INFO;
                     break;
                 case "GRPLF":
                     base.ScreenId = GROUP_LIFEscreenId.BENEFICIARY_INFO;
                     break;

                     // for itrack 1161
                 case "INDPA":
                     base.ScreenId = INDIVIDUAL_PERSONAL_ACCIDENTscreenId.BENEFICIARY_INFO;//"116_1_5_0";// add beneficiary for Individual Personal Accident
                     break;
                 default:
                     base.ScreenId = INDIVIDUAL_PERSONAL_ACCIDENTscreenId.BENEFICIARY_INFO;
                     break;
             }

             
           
            lblMessage.Visible = false;
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;


            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

           // END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.AddBeneficiaryDetails", System.Reflection.Assembly.GetExecutingAssembly());

           
		
            if (!IsPostBack)
            {

               // txtBENEFICIARY_SHARE.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);ValidatorOnChange();");
                SetCaptions();
                SetErrorMessages();
                hidRisk_Id.Value = Request.QueryString["RISK_ID"];// changed by praveer for TFS # 2393
                if (Request.QueryString["BENEFICIARY_ID"] != null && Request.QueryString["BENEFICIARY_ID"].ToString() != "")
                {
                    hidBeneficiaryIndexID.Value = Request.QueryString["BENEFICIARY_ID"].ToString();
                   
                    this.GetOldDataObject();
                }
                else if (Request.QueryString["BENEFICIARY_ID"] == null)
                {
                    hidBeneficiaryIndexID.Value = "NEW";
                    btnDelete.Enabled = false;
                }

             
                
               

            }
            if (Request.QueryString["CO_APPLICANT_ID"] != null && Request.QueryString["CO_APPLICANT_ID"].ToString() != "")
                hidCO_APPLICANT_ID.Value = Request.QueryString["CO_APPLICANT_ID"];

            SetCustomSecurityxml(hidCO_APPLICANT_ID.Value, CalledFrom);
           
        }

        private void GetOldDataObject()
        {


            //numberFormatInfo.NumberDecimalDigits = 2;
            //if (hidBeneficiaryIndexID.Value == "NEW")
            //{
            //    hidOLDTOTALPERCENT.Value = objBeneficiaryDetails.GetTotalShareofBeneficiary(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), int.Parse(hidRisk_Id.Value), 0);
            //}
            //else
            //{
            //   hidOLDTOTALPERCENT.Value = objBeneficiaryDetails.GetTotalShareofBeneficiary(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), int.Parse(hidRisk_Id.Value), int.Parse(hidBeneficiaryIndexID.Value));
                ClsBeneficiaryInfo objBeneficiaryInfo = new ClsBeneficiaryInfo();
               // int Beneficiary_Id = int.Parse(hidBeneficiaryIndexID.Value);
                objBeneficiaryInfo.BENEFICIARY_ID.CurrentValue = int.Parse(hidBeneficiaryIndexID.Value);
                objBeneficiaryInfo.RISK_ID.CurrentValue = int.Parse(hidRisk_Id.Value);// changed by praveer for TFS # 2393
                

                objBeneficiaryInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
                objBeneficiaryInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
                objBeneficiaryInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());
                //DataTable dt = objBeneficiaryDetails.FetchBenificiaryData(ref objBeneficiaryInfo);

                if (objBeneficiaryDetails.FetchBenificiaryData(ref objBeneficiaryInfo))
                {
                    PopulatePageFromEbixModelObject(this.Page, objBeneficiaryInfo);
                    
                  
                    base.SetPageModelObject(objBeneficiaryInfo);
                    btnReset.Enabled = true;
                    

                    
                }
            //}
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;


            ClsBeneficiaryInfo objBeneficiaryInfo;
            try
            {
                //For new item to add
                strRowId = hidBeneficiaryIndexID.Value;
                if (strRowId.ToUpper().Equals("NEW"))
                {
                    objBeneficiaryInfo = new ClsBeneficiaryInfo();
                    this.getFormValues(objBeneficiaryInfo);

                    objBeneficiaryInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
                    objBeneficiaryInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
                    objBeneficiaryInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());
                    objBeneficiaryInfo.RISK_ID.CurrentValue = int.Parse(hidRisk_Id.Value);

                    objBeneficiaryInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objBeneficiaryInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                    objBeneficiaryInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;

                    intRetval = objBeneficiaryDetails.AddBeneficiaryInformation(objBeneficiaryInfo);

                    if (intRetval > 0)
                    {
                        hidBeneficiaryIndexID.Value = objBeneficiaryInfo.BENEFICIARY_ID.CurrentValue.ToString();

                        this.GetOldDataObject();

                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";
                        btnDelete.Enabled = true;

                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                    }

                    else if (intRetval == -2)
                    {
                        
                            lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "2");
                        hidFormSaved.Value = "2";

                    }

                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                }
                //For The Update cse
                else
                {

                    objBeneficiaryInfo = (ClsBeneficiaryInfo)base.GetPageModelObject();
                    this.getFormValues(objBeneficiaryInfo);

                    objBeneficiaryInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
                    objBeneficiaryInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
                    objBeneficiaryInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());
                    objBeneficiaryInfo.RISK_ID.CurrentValue = int.Parse(hidRisk_Id.Value);

                    objBeneficiaryInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objBeneficiaryInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objBeneficiaryDetails.UpdateBeneficiaryInformation(objBeneficiaryInfo);

                    if (intRetval > 0)
                    {
                        hidBeneficiaryIndexID.Value = objBeneficiaryInfo.BENEFICIARY_ID.CurrentValue.ToString();
                        this.GetOldDataObject();
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

                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "2");
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
            ClsBeneficiaryInfo objBeneficiaryInfo;
            int intRetval;

            try
            {
              objBeneficiaryInfo = (ClsBeneficiaryInfo)base.GetPageModelObject();

                objBeneficiaryInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());


                    intRetval = objBeneficiaryDetails.DeleteBeneficiaryInformation(objBeneficiaryInfo);

                    if (intRetval > 0)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                    hidFormSaved.Value = "1";
                    //hidBeneficiaryIndexID.Value = "";
                    //hidRisk_Id.Value = "";
                    //hidOLDTOTALPERCENT.Value = "";
                    //btnDelete.Visible = false;
                    //btnReset.Visible = false;
                    //btnSave.Visible = false;

                    trBody1.Attributes.Add("style", "display:none");
                    trBody.Attributes.Add("style", "display:none");
                    capmsg.Attributes.Add("style", "display:none");
                }
                else if (intRetval == -1)
                {
                    lblDelete.Text = ClsMessages.GetMessage(base.ScreenId, "128");
                    hidFormSaved.Value = "2";
                }
                lblDelete.Visible = true;
                lblMessage.Visible = false;
                hidRisk_Id.Value = "";

            }
            
            
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }
     


       

        private void SetCaptions()
        {

            capBENEFICIARY_NAME.Text = objResourceMgr.GetString("txtBENEFICIARY_NAME");
            capBENEFICIARY_SHARE.Text = objResourceMgr.GetString("txtBENEFICIARY_SHARE");
            capBENEFICIARY_RELATION.Text = objResourceMgr.GetString("txtBENEFICIARY_RELATION");
            hidmsg1.Value = objResourceMgr.GetString("hidmsg1");
            hidmsg2.Value = objResourceMgr.GetString("hidmsg2");

        }

        private void SetErrorMessages()
        {
            revBENEFICIARY_SHARE.ValidationExpression = aRegExpDoublePositiveWithZeroFourDeci;
            revBENEFICIARY_SHARE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
            //hidmsg1.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
            //hidmsg2.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            rfvBENEFICIARY_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
            csvBENEFICIARY_SHARE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
        }

        private void getFormValues(ClsBeneficiaryInfo objBeneficiaryInfo)
        {

            if (txtBENEFICIARY_NAME.Text.Trim() != "")
            { objBeneficiaryInfo.BENEFICIARY_NAME.CurrentValue = Convert.ToString(txtBENEFICIARY_NAME.Text); }

            if (txtBENEFICIARY_SHARE.Text.Trim() != "")
            { objBeneficiaryInfo.BENEFICIARY_SHARE.CurrentValue = Convert.ToDouble(txtBENEFICIARY_SHARE.Text, numberFormatInfo); }
            else
                objBeneficiaryInfo.BENEFICIARY_SHARE.CurrentValue = 0;

            if (txtBENEFICIARY_RELATION.Text.Trim() != "")
            { objBeneficiaryInfo.BENEFICIARY_RELATION.CurrentValue = Convert.ToString(txtBENEFICIARY_RELATION.Text); }

            objBeneficiaryInfo.CUSTOMER_ID.CurrentValue = Convert.ToInt32(GetCustomerID());
            objBeneficiaryInfo.POLICY_ID.CurrentValue = Convert.ToInt32(GetPolicyID());
            objBeneficiaryInfo.POLICY_VERSION_ID.CurrentValue = Convert.ToInt16(GetPolicyVersionID());

            
        }
        private bool SetCustomSecurityxml(string CO_APP_ID, string CalledFrom)
        {
            bool Valid = true;
            if (CalledFrom.ToUpper().Trim() != "PAPEACC" && //for personal accident for passenges
                CalledFrom.ToUpper().Trim() != "CLTVEHICLEINFO" //civil Liability Transportation
                && CalledFrom.ToUpper().Trim() != "FLVEHICLEINFO" //Facultative Liability
                && CalledFrom.ToUpper().Trim() != "GRPLF" //Group life
                && CalledFrom.ToUpper() != "CPCACC" //Group Personal Accident for Passenger
                )
                return Valid;

            if (GetTransaction_Type().Trim() == MASTER_POLICY)
            {
                string SecurityXml = base.CustomSecurityXml(CO_APP_ID);
                btnSave.PermissionString = SecurityXml;
            }
            return Valid;
        }
         
    }
}
