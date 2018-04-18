using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;

namespace Cms.Policies.Aspx.BOP
{
    public partial class AddOldBuildingDetails : Cms.Policies.policiesbase
    {
        private String strRowId = String.Empty;
        private string XmlFullFilePath = "";
        public string CUSTOMER_ID;
        public string POLICY_ID;
        public string POLICY_VER_ID;
        ClsOldBuildingDetail objOldBuildingDetail = new ClsOldBuildingDetail();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "224_11";
            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;
            trBody.Attributes.Add("style", "display:none");
            trErrorMsgs.Attributes.Add("style", "display:none");
            string strSysID = GetSystemId();
            if (strSysID == "ALBAUAT")
                strSysID = "ALBA";
            XmlFullFilePath = Request.PhysicalApplicationPath + "/Policies/support/PageXml/" + strSysID + "/" + "AddOldBuildingDetails.xml";
            if (!IsPostBack)
            {
                getdata();
                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/Policies/support/PageXML/" + strSysID, "AddOldBuildingDetails.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "/Policies/support/PageXml/" + strSysID + "/AddOldBuildingDetails.xml");
                if (hidCUSTOMER_ID.Value != "" && hidPOLICY_ID.Value != "" && hidPOLICY_VERSION_ID.Value != "" && hidLOCATION_ID.Value != "" && hidPREMISES_ID.Value != "")
                {
                    hidCONTRACTOR_ID.Value = "1";
                    ClsOldBuildingDetailsInfo objOldBuildingDetailsInfo;

                    objOldBuildingDetailsInfo = objOldBuildingDetail.FetchId(hidCUSTOMER_ID.Value, hidPOLICY_ID.Value, hidPOLICY_VERSION_ID.Value, hidPREMISES_ID.Value, hidLOCATION_ID.Value);

                    hidCONTRACTOR_ID.Value = objOldBuildingDetailsInfo.OLDBLD_ID.CurrentValue.ToString();
                    if (hidCONTRACTOR_ID.Value == "-1")
                    {
                        hidCONTRACTOR_ID.Value = "NEW";
                        btnDelete.Style.Add("display", "none");
                    }
                    else
                    {
                        strRowId = hidCONTRACTOR_ID.Value;
                        this.GetOldDataObject();
                    }
                }
                else
                {
                    hidCONTRACTOR_ID.Value = "NEW";
                    btnDelete.Style.Add("display", "none");
                }
                strRowId = hidCONTRACTOR_ID.Value;
            }
        }

        private void GetOldDataObject()
        {
            ClsOldBuildingDetailsInfo objOldBuildingDetailsInfo = new ClsOldBuildingDetailsInfo();
            objOldBuildingDetailsInfo.OLDBLD_ID.CurrentValue = int.Parse(hidCONTRACTOR_ID.Value);

            objOldBuildingDetailsInfo = objOldBuildingDetail.FetchData(Int32.Parse(hidCONTRACTOR_ID.Value), Int32.Parse(hidCUSTOMER_ID.Value), Int32.Parse(hidPOLICY_ID.Value), Int32.Parse(hidPOLICY_VERSION_ID.Value), Int32.Parse(hidLOCATION_ID.Value), Int32.Parse(hidPREMISES_ID.Value));
            PopulatePageFromEbixModelObject(this.Page, objOldBuildingDetailsInfo);
            base.SetPageModelObject(objOldBuildingDetailsInfo);
            //if (objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue!= "")
            //{
            //    txtWHN_WIRING_UPDT.Text = objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue;
            //}
            //if (objOldBuildingDetailsInfo.WHN_ROOF_REPRD.CurrentValue != "")
            //{
            //    txtWHN_ROOF_REPRD.Text = objOldBuildingDetailsInfo.WHN_ROOF_REPRD.CurrentValue;
            //}
            //if (objOldBuildingDetailsInfo.WIRING_IN_CNDCT.CurrentValue != "")
            //{
            //    cmbWIRING_IN_CNDCT.SelectedValue = objOldBuildingDetailsInfo.WIRING_IN_CNDCT.CurrentValue;
            //} 
            //if (objOldBuildingDetailsInfo.WHN_ROOF_REPLCD.CurrentValue != "")
            //{
            //    txtWHN_ROOF_REPLCD.Text = objOldBuildingDetailsInfo.WHN_ROOF_REPLCD.CurrentValue;
            //} 
            //if (objOldBuildingDetailsInfo.FUSES_RPLCD.CurrentValue != "")
            //{
            //    cmbFUSES_RPLCD.SelectedValue = objOldBuildingDetailsInfo.FUSES_RPLCD.CurrentValue;
            //} 
            //if (objOldBuildingDetailsInfo.ROOF_MTRL.CurrentValue != "")
            //{
            //    cmbROOF_MTRL.SelectedValue = objOldBuildingDetailsInfo.ROOF_MTRL.CurrentValue;
            //} 
            //if (objOldBuildingDetailsInfo.ALM_WIRING.CurrentValue != "")
            //{
            //    cmbALM_WIRING.SelectedValue= objOldBuildingDetailsInfo.ALM_WIRING.CurrentValue;
            //} 
            //if (objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue != "")
            //{
            //    txtWHN_WIRING_UPDT.Text = objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue;
            //} if (objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue != "")
            //{
            //    txtWHN_WIRING_UPDT.Text = objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue;
            //} if (objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue != "")
            //{
            //    txtWHN_WIRING_UPDT.Text = objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue;
            //} if (objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue != "")
            //{
            //    txtWHN_WIRING_UPDT.Text = objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue;
            //} if (objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue != "")
            //{
            //    txtWHN_WIRING_UPDT.Text = objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue;
            //} if (objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue != "")
            //{
            //    txtWHN_WIRING_UPDT.Text = objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue;
            //} if (objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue != "")
            //{
            //    txtWHN_WIRING_UPDT.Text = objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue;
            //} if (objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue != "")
            //{
            //    txtWHN_WIRING_UPDT.Text = objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue;
            //} if (objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue != "")
            //{
            //    txtWHN_WIRING_UPDT.Text = objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue;
            //} if (objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue != "")
            //{
            //    txtWHN_WIRING_UPDT.Text = objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue;
            //} if (objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue != "")
            //{
            //    txtWHN_WIRING_UPDT.Text = objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue;
            //} if (objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue != "")
            //{
            //    txtWHN_WIRING_UPDT.Text = objOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue;
            //}
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
              int intRetval;
              ClsOldBuildingDetailsInfo objOldBuildingDetailsInfo;
              try
              {
                  strRowId = hidCONTRACTOR_ID.Value;
                  if (strRowId.ToUpper().Equals("NEW"))
                  {
                      objOldBuildingDetailsInfo = new ClsOldBuildingDetailsInfo();
                      this.getFormValues(objOldBuildingDetailsInfo);


                      objOldBuildingDetailsInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                      objOldBuildingDetailsInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                      objOldBuildingDetailsInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
                      objOldBuildingDetailsInfo.LOCATION_ID.CurrentValue = int.Parse(hidLOCATION_ID.Value);
                      objOldBuildingDetailsInfo.PREMISES_ID.CurrentValue = int.Parse(hidPREMISES_ID.Value);
                      objOldBuildingDetailsInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                      objOldBuildingDetailsInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                      intRetval = objOldBuildingDetail.AddOldBuildingDetailADD(objOldBuildingDetailsInfo, XmlFullFilePath);
                      hidCONTRACTOR_ID.Value = objOldBuildingDetailsInfo.OLDBLD_ID.CurrentValue.ToString();

                      if (intRetval > 0)
                      {
                          this.GetOldDataObject();
                          lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                          btnDelete.Style.Add("display", "inline");
                      }
                      else if (intRetval == -1)
                      {
                          lblMessage.Text = ClsMessages.FetchGeneralMessage("18");

                      }
                      else
                      {
                          lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                      }
                      lblMessage.Visible = true;
                      trErrorMsgs.Attributes.Add("style", "display:inline");
                  }
                  else                
                  {
                      objOldBuildingDetailsInfo = (ClsOldBuildingDetailsInfo)base.GetPageModelObject();
                      this.getFormValues(objOldBuildingDetailsInfo);

                      objOldBuildingDetailsInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                      objOldBuildingDetailsInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                      objOldBuildingDetailsInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
                      objOldBuildingDetailsInfo.LOCATION_ID.CurrentValue = int.Parse(hidLOCATION_ID.Value);
                      objOldBuildingDetailsInfo.PREMISES_ID.CurrentValue = int.Parse(hidPREMISES_ID.Value);
                      objOldBuildingDetailsInfo.OLDBLD_ID.CurrentValue = int.Parse(hidCONTRACTOR_ID.Value);

                      objOldBuildingDetailsInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                      objOldBuildingDetailsInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                      intRetval = objOldBuildingDetail.updateOldBuildingDetail(objOldBuildingDetailsInfo, XmlFullFilePath);



                      if (intRetval > 0)
                      {
                          hidCONTRACTOR_ID.Value = objOldBuildingDetailsInfo.OLDBLD_ID.CurrentValue.ToString();
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
                      trErrorMsgs.Attributes.Add("style", "display:inline");
                  
                  
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
        private void getFormValues(ClsOldBuildingDetailsInfo objClsOldBuildingDetailsInfo)
        {
            if (txtWHN_WIRING_UPDT.Text != "")
            {
                objClsOldBuildingDetailsInfo.WHN_WIRING_UPDT.CurrentValue = Convert.ToString(txtWHN_WIRING_UPDT.Text);
            }
            if (txtWHN_ROOF_REPRD.Text != "")
            {
                objClsOldBuildingDetailsInfo.WHN_ROOF_REPRD.CurrentValue = Convert.ToString(txtWHN_ROOF_REPRD.Text);
            }
            if (cmbWIRING_IN_CNDCT.SelectedValue != "")
            {
                objClsOldBuildingDetailsInfo.WIRING_IN_CNDCT.CurrentValue = Convert.ToString(cmbWIRING_IN_CNDCT.SelectedValue);
            }
            if (txtWHN_ROOF_REPLCD.Text != "")
            {
                objClsOldBuildingDetailsInfo.WHN_ROOF_REPLCD.CurrentValue = Convert.ToString(txtWHN_ROOF_REPLCD.Text);

            }
            if (cmbFUSES_RPLCD.SelectedValue != "")
            {
                objClsOldBuildingDetailsInfo.FUSES_RPLCD.CurrentValue = Convert.ToString(cmbFUSES_RPLCD.SelectedValue);
            }
            if (cmbROOF_MTRL.SelectedValue != "")
            {
                objClsOldBuildingDetailsInfo.ROOF_MTRL.CurrentValue = Convert.ToString(cmbROOF_MTRL.SelectedValue);
            }
            if (cmbALM_WIRING.SelectedValue!= "")
            {
                objClsOldBuildingDetailsInfo.ALM_WIRING.CurrentValue = Convert.ToString(cmbALM_WIRING.SelectedValue);
            }
            if (cmbSPF.SelectedValue!= "")
            {
                objClsOldBuildingDetailsInfo.SPF.CurrentValue = Convert.ToString(cmbSPF.SelectedValue);
            }
            if (txtWHN_PLBMG_MODRS.Text != "")
            {
                objClsOldBuildingDetailsInfo.WHN_PLBMG_MODRS.CurrentValue = Convert.ToString(txtWHN_PLBMG_MODRS.Text);
            }
            if (cmbANY_ABSTS.SelectedValue != "")
            {
                objClsOldBuildingDetailsInfo.ANY_ABSTS.CurrentValue = Convert.ToString(cmbANY_ABSTS.SelectedValue);
            }
            if (cmbTYP_WTR_PIPS.SelectedValue != "")
            {
                objClsOldBuildingDetailsInfo.TYP_WTR_PIPS.CurrentValue = Convert.ToString(cmbTYP_WTR_PIPS.SelectedValue);
            }
            if (cmbANY_FRB_ABSTS.SelectedValue != "")
            {
                objClsOldBuildingDetailsInfo.ANY_FRB_ABSTS.CurrentValue = Convert.ToString(cmbANY_FRB_ABSTS.SelectedValue);
            }
            if (txtWHN_HEATNG_MODRS.Text != "")
            {
                objClsOldBuildingDetailsInfo.WHN_HEATNG_MODRS.CurrentValue = Convert.ToString(txtWHN_HEATNG_MODRS.Text);
            }
            if (cmbTYP_SYS.SelectedValue!= "")
            {
                objClsOldBuildingDetailsInfo.TYP_SYS.CurrentValue = Convert.ToString(cmbTYP_SYS.SelectedValue);
            }
            if (cmbTYP_FUEL.SelectedValue != "")
            {
                objClsOldBuildingDetailsInfo.TYP_FUEL.CurrentValue = Convert.ToString(cmbTYP_FUEL.SelectedValue);
            }
        }
        

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClsOldBuildingDetailsInfo objClsOldBuildingDetailsInfo;
            int intRetval = 0;
            try
            {
                objClsOldBuildingDetailsInfo = (ClsOldBuildingDetailsInfo)base.GetPageModelObject();

                objClsOldBuildingDetailsInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                objClsOldBuildingDetailsInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                objClsOldBuildingDetailsInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
                objClsOldBuildingDetailsInfo.LOCATION_ID.CurrentValue = int.Parse(hidLOCATION_ID.Value);
                objClsOldBuildingDetailsInfo.PREMISES_ID.CurrentValue = int.Parse(hidPREMISES_ID.Value);
                objClsOldBuildingDetailsInfo.OLDBLD_ID.CurrentValue = int.Parse(hidCONTRACTOR_ID.Value);

                objClsOldBuildingDetailsInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                objClsOldBuildingDetailsInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                intRetval = objOldBuildingDetail.DelOldBuildingDetail(objClsOldBuildingDetailsInfo, XmlFullFilePath);

                if (intRetval > 0)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                    //hidFormSaved.Value = "5";
                    trBody.Attributes.Add("style", "display:inline");

                }
                else if (intRetval == -1)
                {
                    lblDelete.Text = ClsMessages.GetMessage(base.ScreenId, "128");
                    //hidFormSaved.Value = "2";
                }
                lblDelete.Visible = true;
                lblMessage.Visible = false;
                hidCONTRACTOR_ID.Value = "NEW";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void getdata()
        {

            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();
            else
                hidCUSTOMER_ID.Value = GetCustomerID();

            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
                hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();
            else
                hidPOLICY_ID.Value = GetPolicyID();

            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
                hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();
            else
                hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();

            if (Request.QueryString["PREMISES_ID"] != null && Request.QueryString["PREMISES_ID"].ToString() != "")
                hidPREMISES_ID.Value = Request.QueryString["PREMISES_ID"].ToString();
            else
                hidPREMISES_ID.Value = "1";

            if (Request.QueryString["LOCATION_ID"] != null && Request.QueryString["LOCATION_ID"].ToString() != "")
                hidLOCATION_ID.Value = Request.QueryString["LOCATION_ID"].ToString();
            else
                hidLOCATION_ID.Value = "1";
        }

    }
}