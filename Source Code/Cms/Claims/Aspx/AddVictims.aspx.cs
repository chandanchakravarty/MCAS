using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.Model.Claims;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlCommon;
using System.Data;
using System.Globalization;

namespace Cms.Claims.Aspx
{
    public partial class AddVictims : Cms.Claims.ClaimBase
    {
        System.Resources.ResourceManager objResourceMgr;
        ClsVictimInformation objVictimInformation = new ClsVictimInformation();      
        ClsVictimInfo objVictimInfo;

        private String strRowId = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            Ajax.Utility.RegisterTypeForAjax(typeof(AddClaimCoverages));

            base.ScreenId = "306_16_1";
            lblMessage.Visible = false;

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Write;
            btnDelete.PermissionString = gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddVictims", System.Reflection.Assembly.GetExecutingAssembly());

         

            if (!Page.IsPostBack)
            {

           
                GetQueryStringValues();
                SetCaptions();
                SetErrorMessages();
                FillDropdowns();
                
                GetOldDataObject();


            }
        }
       
        private void FillDropdowns()
        {


            cmbSTATUS.DataSource = ClsCommon.GetLookup("CLMSTA");
            cmbSTATUS.DataTextField = "LookupDesc";
            cmbSTATUS.DataValueField = "LookupID";
            cmbSTATUS.DataBind();

            cmbINJURY_TYPE.DataSource = ClsCommon.GetLookup("INJ_TP");
            cmbINJURY_TYPE.DataTextField = "LookupDesc";
            cmbINJURY_TYPE.DataValueField = "LookupID";
            cmbINJURY_TYPE.DataBind();
            
            
        }
        private void GetOldDataObject()
        {
            strRowId = hidVICTIM_ID.Value;
            if (strRowId.ToUpper().Equals("NEW"))
            {
                btnDelete.Visible = false;
                return;
            }

            objVictimInfo = new ClsVictimInfo();

            // FILL STATE AS PER THE COUNTRY BECAUSE STATE IS FILL BY USING AJAX WHICH IS CLEAR ON THE POSTBACK          

            objVictimInfo.VICTIM_ID.CurrentValue = int.Parse(hidVICTIM_ID.Value);
            objVictimInfo.CLAIM_ID.CurrentValue = int.Parse(hidCLAIM_ID.Value);

            DataTable dt = objVictimInformation.FetchData(ref objVictimInfo);

            if (dt != null && dt.Rows.Count > 0)
            {


                PopulatePageFromEbixModelObject(this.Page, objVictimInfo);
                
                base.SetPageModelObject(objVictimInfo);

               
            }
           
        }

      
       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;


            ClsVictimInfo objVictimInfo;
            try
            {
                //For new item to add
                strRowId = hidVICTIM_ID.Value;
                if (strRowId.ToUpper().Equals("NEW"))
                {

                    objVictimInfo = new ClsVictimInfo();
                    this.getFormValues(objVictimInfo);

                    objVictimInfo.CLAIM_ID.CurrentValue = int.Parse(hidCLAIM_ID.Value);//int.Parse(ViewStateClaimID);

                    //objClaimCoveragesInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value); //int.Parse(GetCustomerID());
                    //objClaimCoveragesInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);                  // int.Parse(GetPolicyID());
                    //objClaimCoveragesInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);               //int.Parse(GetPolicyVersionID());

                    objVictimInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objVictimInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                    objVictimInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;


                    intRetval = objVictimInformation.AddClaimVictim(objVictimInfo);
                   


                    if (intRetval > 0)
                    {


                        hidVICTIM_ID.Value = objVictimInfo.VICTIM_ID.CurrentValue.ToString();
                        this.GetOldDataObject();
                       
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";

                        btnDelete.Visible = true;
            
                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                         hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "4");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -4)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "6");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -5)//NUMBER OF PASSENGERS CANNOT BE GREATER THEN ACTUAL PASSENGERS GIVEN IN RISK SCREEN
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "9");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -6)//RISK IS NOT ADDED
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "8");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                  
                    lblMessage.Visible = true;
                }
                else //For The Update cse
                {

                    objVictimInfo = (ClsVictimInfo)base.GetPageModelObject();
                    this.getFormValues(objVictimInfo);
                    objVictimInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;

                    //objClaimCoveragesInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value); //int.Parse(GetCustomerID());
                    //objClaimCoveragesInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);                  // int.Parse(GetPolicyID());
                    //objClaimCoveragesInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);               //int.Parse(GetPolicyVersionID());

                    objVictimInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objVictimInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objVictimInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objVictimInformation.UpdateClaimVictim(objVictimInfo);

                    if (intRetval > 0)
                    {
                        
                        this.GetOldDataObject();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");

                      hidFormSaved.Value = "1";
                      btnDelete.Visible = true;

                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                         hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -4)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "7");
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
                //if (objNamedPerilsinfo != null)
                //    objNamedPerilsinfo.Dispose();
            }
        }

       

        private void getFormValues(ClsVictimInfo objVictimInfo)
        {

            try
            {
                
                    if (cmbSTATUS.SelectedValue!= "")
                        objVictimInfo.STATUS.CurrentValue = int.Parse(cmbSTATUS.SelectedValue);
                    else
                        objVictimInfo.STATUS.CurrentValue = 0;

                    if (cmbINJURY_TYPE.SelectedValue != "")
                        objVictimInfo.INJURY_TYPE.CurrentValue = int.Parse(cmbINJURY_TYPE.SelectedValue);
                    else
                        objVictimInfo.INJURY_TYPE.CurrentValue = 0;


                    objVictimInfo.NAME.CurrentValue = txtNAME.Text.Trim();


                    if (GetLOBID() == ((int)enumLOB.PAPEACC).ToString().ToString())
                        objVictimInfo.PAGE_MODE.CurrentValue = "PASS";
                    else
                        objVictimInfo.PAGE_MODE.CurrentValue = "VICTIM";



            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        private void GetQueryStringValues()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["VICTIM_ID"]) && Request.QueryString["VICTIM_ID"] != "NEW")
                hidVICTIM_ID.Value = Request.QueryString["VICTIM_ID"].ToString();
            else
                hidVICTIM_ID.Value = "NEW";

            if (Request.QueryString["CLAIM_ID"] != null && Request.QueryString["CLAIM_ID"].ToString() != "")
                hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();

        }

        private void SetCaptions()
        {
            capNAME.Text                        = objResourceMgr.GetString("txtNAME");
            capSTATUS.Text                      = objResourceMgr.GetString("cmbSTATUS");
            capINJURY_TYPE.Text                 = objResourceMgr.GetString("cmbINJURY_TYPE");          
            lblRequiredFieldsInformation.Text   = objResourceMgr.GetString("lblRequiredFieldsInformation");

        }

        private void SetErrorMessages()
        {
         
            rfvNAME.ErrorMessage        = ClsMessages.GetMessage(base.ScreenId, "1");
            rfvSTATUS.ErrorMessage      = ClsMessages.GetMessage(base.ScreenId, "2");
            rfvINJURY_TYPE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "3");

           
        }

       

      
        protected void btnDelete_Click(object sender, EventArgs e)
        {

            ClsVictimInfo objVictimInfo = (ClsVictimInfo)base.GetPageModelObject();
            lblMessage.Visible = true;

            if (objVictimInformation.DeleteClaimVictim(objVictimInfo) > 0)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "5");
              
            }
            else
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "4");
                hidFormSaved.Value = "1";

                btnDelete.Visible = false;
                btnReset.Visible = false;
                btnSave.Visible = false;
            }
        }

      
    }
}
