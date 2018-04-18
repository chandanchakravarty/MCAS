/******************************************************************************************
<Author					: -		Sneha
<Start Date				: -		18-11-2011
<End Date				: -	
<Description			: - 	

<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;   
using Cms.Model.Policy;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BlApplication;
using System.Data;
using System.Globalization;

namespace Cms.Policies.Aspx.BOP
{
    public partial class AddPropertyDetail : Cms.Policies.policiesbase
    {
        private String strRowId = String.Empty;
        private string XmlFullFilePath = "";
        public string CUSTOMER_ID;
        public string POLICY_ID;
        public string POLICY_VER_ID;
        Cms.BusinessLayer.BlApplication.ClsPropertyDetail objPropertyDetail = new ClsPropertyDetail();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "224_11";


            btnSave.CmsButtonClass = CmsButtonType.Write;  
            btnSave.PermissionString = gstrSecurityXML;

            btnReset.CmsButtonClass = CmsButtonType.Write;  
            btnReset.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnReset.Attributes.Add("onclick", "javascript:return ResetTheForm();");
            btnDelete.Attributes.Add("onclick", "javascript:return ResetTheForm();");

            btnReset.Attributes.Add("onclick", "javascript:return ResetTheForm();");
            btnDelete.Attributes.Add("onclick", "javascript:return ResetTheForm();");

            trBody.Attributes.Add("style", "display:none");
            trErrorMsgs.Attributes.Add("style", "display:none");

            string strSysID = GetSystemId();
            if (strSysID == "ALBAUAT")
                strSysID = "ALBA";

            XmlFullFilePath = Request.PhysicalApplicationPath + "/Policies/support/PageXml/" + strSysID + "/" + "AddPropertyDetail.xml";

            if (!Page.IsPostBack)
            {
                getdata();
                FillDropDown();

                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/Policies/support/PageXML/" + strSysID, "AddPropertyDetail.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "/Policies/support/PageXml/" + strSysID + "/AddPropertyDetail.xml");

                 if (hidCUSTOMER_ID.Value != "" && hidPOLICY_ID.Value != "" && hidPOLICY_VERSION_ID.Value!="" && hidLOCATION_ID.Value!="" && hidPREMISES_ID.Value!="")
                {
                    hidPROPERTY_ID.Value = "1";
                    ClsPropertyDetailInfo objPropertyDetailInfo;

                    objPropertyDetailInfo = objPropertyDetail.FetchId(hidCUSTOMER_ID.Value, hidPOLICY_ID.Value, hidPOLICY_VERSION_ID.Value,hidPREMISES_ID.Value,hidLOCATION_ID.Value);

                    hidPROPERTY_ID.Value = objPropertyDetailInfo.PROPERTY_ID.CurrentValue.ToString();
                    if (hidPROPERTY_ID.Value == "-1")
                    {
                        hidPROPERTY_ID.Value = "NEW";
                        btnDelete.Style.Add("display", "none");
                    }
                    else
                    {
                        strRowId = hidPROPERTY_ID.Value;
                        this.GetOldDataObject();
                    }
                }
                else
                {
                    hidPROPERTY_ID.Value = "NEW";
                    btnDelete.Style.Add("display", "none");
                }
                 strRowId = hidPROPERTY_ID.Value;

            
               
  

            }
        }

        protected void FillDropDown()
        {

            for (int i =100; i >=50; i=i-10)
            {
                cmbBPP_PERCENT_COINS.Items.Add(i.ToString());
                cmbBLD_PERCENT_COINS.Items.Add(i.ToString());
            }

            for (int i = 100; i >= 10; i = i - 10)
            {
                cmbPERCENT_SPRINKLERS.Items.Add(i.ToString());
                
            }


            cmbNUM_STORIES.Items.Insert(0,"1");
            cmbNUM_STORIES.Items.Insert(1, "2");
            cmbNUM_STORIES.Items.Insert(2, "3");
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


        private void GetOldDataObject()
        {

            ClsPropertyDetailInfo objPropertyDetailInfo = new ClsPropertyDetailInfo();

            objPropertyDetailInfo.PROPERTY_ID.CurrentValue = int.Parse(hidPROPERTY_ID.Value);

            objPropertyDetailInfo = objPropertyDetail.FetchData(Int32.Parse(hidPROPERTY_ID.Value), hidCUSTOMER_ID.Value, hidPOLICY_ID.Value, hidPOLICY_VERSION_ID.Value,hidLOCATION_ID.Value,hidPREMISES_ID.Value);
            //PopulatePageFromEbixModelObject(this.Page, objPropertyDetailInfo);
            if (objPropertyDetailInfo.PROP_DEDUCT.CurrentValue.ToString() != "" && objPropertyDetailInfo.PROP_DEDUCT.CurrentValue.ToString() != null)
            {
                cmbPROP_DEDUCT.SelectedValue = objPropertyDetailInfo.PROP_DEDUCT.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.OPT_CVG.CurrentValue.ToString() != "" && objPropertyDetailInfo.OPT_CVG.CurrentValue.ToString()!=null)
            {
                cmbOPT_CVG.SelectedValue = objPropertyDetailInfo.OPT_CVG.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.PROP_WNDSTORM.CurrentValue.ToString() != "" && objPropertyDetailInfo.PROP_WNDSTORM.CurrentValue.ToString()!=null)
            {
                cmbPROP_WNDSTORM.Text = objPropertyDetailInfo.PROP_WNDSTORM.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BLD_LMT.CurrentValue.ToString() != "" && objPropertyDetailInfo.BLD_LMT.CurrentValue.ToString()!=null)
            {
                txtBLD_LMT.Text= objPropertyDetailInfo.BLD_LMT.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BPP_LMT.CurrentValue.ToString() != "" && objPropertyDetailInfo.BPP_LMT.CurrentValue.ToString()!=null)
            {
                txtBPP_LMT.Text=objPropertyDetailInfo.BPP_LMT.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BLD_VALU.CurrentValue.ToString() != "" && objPropertyDetailInfo.BLD_VALU.CurrentValue.ToString()!=null)
            {
                cmbBLD_VALU.SelectedValue = objPropertyDetailInfo.BLD_VALU.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BPP_VALU.CurrentValue.ToString() != "" && objPropertyDetailInfo.BPP_VALU.CurrentValue.ToString()!=null)
            {
                cmbBPP_VALU.SelectedValue = objPropertyDetailInfo.BPP_VALU.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BLD_INF.CurrentValue.ToString() != "" && objPropertyDetailInfo.BLD_INF.CurrentValue.ToString()!=null)
            {
                cmbBLD_INF.SelectedValue= (objPropertyDetailInfo.BLD_INF.CurrentValue.ToString());
            }
            if (objPropertyDetailInfo.BPP_STOCK.CurrentValue.ToString() != "" && objPropertyDetailInfo.BPP_STOCK.CurrentValue.ToString()!=null)
            {
                txtBPP_STOCK.Text= objPropertyDetailInfo.BPP_STOCK.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.YEAR_BUILT.CurrentValue.ToString() != "" && objPropertyDetailInfo.YEAR_BUILT.CurrentValue.ToString()!=null)
            {
                txtYEAR_BUILT.Text= objPropertyDetailInfo.YEAR_BUILT.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.CONST_TYPE.CurrentValue.ToString() != "" && objPropertyDetailInfo.CONST_TYPE.CurrentValue.ToString()!=null)
            {
                cmbCONST_TYPE.SelectedValue= objPropertyDetailInfo.CONST_TYPE.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BI_WIRNG_YR.CurrentValue.ToString() != "" && objPropertyDetailInfo.BI_WIRNG_YR.CurrentValue.ToString()!=null)
            {
                txtBI_WIRNG_YR.Text= objPropertyDetailInfo.BI_WIRNG_YR.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.NUM_STORIES.CurrentValue.ToString() != "" && objPropertyDetailInfo.NUM_STORIES.CurrentValue.ToString()!=null)
            {
                cmbNUM_STORIES.SelectedValue= objPropertyDetailInfo.NUM_STORIES.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BI_ROOFING_YR.CurrentValue.ToString() != "" && objPropertyDetailInfo.BI_ROOFING_YR.CurrentValue.ToString()!=null)
            {
                txtBI_ROOFING_YR.Text= objPropertyDetailInfo.BI_ROOFING_YR.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BP_PRESENT.CurrentValue.ToString() != "" && objPropertyDetailInfo.BP_PRESENT.CurrentValue.ToString()!=null)
            {
                cmbBP_PRESENT.SelectedValue= objPropertyDetailInfo.BP_PRESENT.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BI_ROOF_TYP.CurrentValue.ToString() != "" && objPropertyDetailInfo.BI_ROOF_TYP.CurrentValue.ToString()!=null)
            {
                cmbBI_ROOF_TYP.SelectedValue= objPropertyDetailInfo.BI_ROOF_TYP.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BP_FNSHD.CurrentValue.ToString() != "" && objPropertyDetailInfo.BP_FNSHD.CurrentValue.ToString()!=null)
            {
                cmbBP_FNSHD.SelectedValue= objPropertyDetailInfo.BP_FNSHD.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BI_HEATNG_YR.CurrentValue.ToString() != "" && objPropertyDetailInfo.BI_HEATNG_YR.CurrentValue.ToString()!=null)
            {
                txtBI_HEATNG_YR.Text= objPropertyDetailInfo.BI_HEATNG_YR.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BP_OPEN.CurrentValue.ToString() != "" && objPropertyDetailInfo.BP_OPEN.CurrentValue.ToString()!=null)
            {
                cmbBP_OPEN.SelectedValue= objPropertyDetailInfo.BP_OPEN.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BI_WIND_CLASS.CurrentValue.ToString() != "" && objPropertyDetailInfo.BI_WIND_CLASS.CurrentValue.ToString()!=null)
            {
                cmbBI_WIND_CLASS.SelectedValue= objPropertyDetailInfo.BI_WIND_CLASS.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BLD_PERCENT_COINS.CurrentValue.ToString() != "" && objPropertyDetailInfo.BLD_PERCENT_COINS.CurrentValue.ToString()!=null)
            {
                cmbBLD_PERCENT_COINS.SelectedValue= objPropertyDetailInfo.BLD_PERCENT_COINS.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BPP_PERCENT_COINS.CurrentValue.ToString() != "" && objPropertyDetailInfo.BPP_PERCENT_COINS.CurrentValue.ToString() != null)
            {
                cmbBPP_PERCENT_COINS.SelectedValue = objPropertyDetailInfo.BPP_PERCENT_COINS.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.PERCENT_SPRINKLERS.CurrentValue.ToString() != "" && objPropertyDetailInfo.PERCENT_SPRINKLERS.CurrentValue.ToString() != null)
            {
                cmbPERCENT_SPRINKLERS.SelectedValue = objPropertyDetailInfo.PERCENT_SPRINKLERS.CurrentValue.ToString();
            }
            if (objPropertyDetailInfo.BI_PLMG_YR.CurrentValue.ToString() != "" && objPropertyDetailInfo.BI_PLMG_YR.CurrentValue.ToString()!=null)
            {
               txtBI_PLMG_YR.Text= objPropertyDetailInfo.BI_PLMG_YR.CurrentValue.ToString();
            }
            
            base.SetPageModelObject(objPropertyDetailInfo);
        }

        private void getFormValues(ClsPropertyDetailInfo objPropertyDetailInfo)
        {
            if (cmbPROP_DEDUCT.SelectedValue != "")
            {
                objPropertyDetailInfo.PROP_DEDUCT.CurrentValue = Convert.ToString(cmbPROP_DEDUCT.SelectedValue);
            }
            if (cmbOPT_CVG.SelectedValue != "")
            {
                objPropertyDetailInfo.OPT_CVG.CurrentValue = Convert.ToString(cmbOPT_CVG.SelectedValue);
            }
            if (cmbPROP_WNDSTORM.SelectedValue !="")
            {
                objPropertyDetailInfo.PROP_WNDSTORM.CurrentValue = Convert.ToString(cmbPROP_WNDSTORM.SelectedValue);
            }
            if (txtBLD_LMT.Text != "")
            {
                objPropertyDetailInfo.BLD_LMT.CurrentValue = Convert.ToString(txtBLD_LMT.Text);
            }
            if (txtBPP_LMT.Text != "")
            {
                objPropertyDetailInfo.BPP_LMT.CurrentValue = Convert.ToString(txtBPP_LMT.Text);
            }
            if (cmbBLD_VALU.SelectedValue !="" )
            {
                objPropertyDetailInfo.BLD_VALU.CurrentValue = Convert.ToString(cmbBLD_VALU.SelectedValue);
            }
            if (cmbBPP_VALU.SelectedValue != "")
            {
                objPropertyDetailInfo.BPP_VALU.CurrentValue = Convert.ToString(cmbBPP_VALU.SelectedValue);
            }
            if (cmbBLD_INF.SelectedValue != "")
            {
                objPropertyDetailInfo.BLD_INF.CurrentValue = Convert.ToString(cmbBLD_INF.SelectedValue);
            }
            if (txtBPP_STOCK.Text != "")
            {
                objPropertyDetailInfo.BPP_STOCK.CurrentValue = Convert.ToString(txtBPP_STOCK.Text);
            }
            if (txtYEAR_BUILT.Text != "")
            {
                objPropertyDetailInfo.YEAR_BUILT.CurrentValue = Convert.ToString(txtYEAR_BUILT.Text);
            }
            if (cmbCONST_TYPE.SelectedValue != "")
            {
                objPropertyDetailInfo.CONST_TYPE.CurrentValue = Convert.ToString(cmbCONST_TYPE.SelectedValue);
            }
            if (txtBI_WIRNG_YR.Text != "")
            {
                objPropertyDetailInfo.BI_WIRNG_YR.CurrentValue = Convert.ToString(txtBI_WIRNG_YR.Text);
            }
            if (cmbNUM_STORIES.SelectedValue != "")
            {
                objPropertyDetailInfo.NUM_STORIES.CurrentValue = Convert.ToString(cmbNUM_STORIES.SelectedValue);
            }
            if (txtBI_ROOFING_YR.Text != "")
            {
                objPropertyDetailInfo.BI_ROOFING_YR.CurrentValue = Convert.ToString(txtBI_ROOFING_YR.Text);
            }
            if (cmbBP_PRESENT.SelectedValue != "")
            {
                objPropertyDetailInfo.BP_PRESENT.CurrentValue = Convert.ToString(cmbBP_PRESENT.SelectedValue);
            }
            if (cmbBI_ROOF_TYP.SelectedValue != "")
            {
                objPropertyDetailInfo.BI_ROOF_TYP.CurrentValue = Convert.ToString(cmbBI_ROOF_TYP.SelectedValue);
            }
            if (cmbBP_FNSHD.SelectedValue != "")
            {
                objPropertyDetailInfo.BP_FNSHD.CurrentValue = Convert.ToString(cmbBP_FNSHD.SelectedValue);
            }
            if (txtBI_HEATNG_YR.Text != "")
            {
                objPropertyDetailInfo.BI_HEATNG_YR.CurrentValue = Convert.ToString(txtBI_HEATNG_YR.Text);
            }
            if (cmbBP_OPEN.SelectedValue != "")
            {
                objPropertyDetailInfo.BP_OPEN.CurrentValue = Convert.ToString(cmbBP_OPEN.SelectedValue);
            }
            if (cmbBI_WIND_CLASS.SelectedValue != "")
            {
                objPropertyDetailInfo.BI_WIND_CLASS.CurrentValue = Convert.ToString(cmbBI_WIND_CLASS.SelectedValue);
            }
            if (cmbBLD_PERCENT_COINS.SelectedValue != "")
            {
                objPropertyDetailInfo.BLD_PERCENT_COINS.CurrentValue = Convert.ToString(cmbBLD_PERCENT_COINS.SelectedValue);
            }
            if (cmbBPP_PERCENT_COINS.SelectedValue != "")
            {
                objPropertyDetailInfo.BPP_PERCENT_COINS.CurrentValue = Convert.ToString(cmbBPP_PERCENT_COINS.SelectedValue);
            }
            if (cmbPERCENT_SPRINKLERS.SelectedValue != "")
            {
                objPropertyDetailInfo.PERCENT_SPRINKLERS.CurrentValue = Convert.ToString(cmbPERCENT_SPRINKLERS.SelectedValue);
            }
            if (txtBI_PLMG_YR.Text != "")
            {
                objPropertyDetailInfo.BI_PLMG_YR.CurrentValue = Convert.ToString(txtBI_PLMG_YR.Text);
            }
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {


            int intRetval;
            ClsPropertyDetailInfo objPropertyDetailInfo;
            try
            {
                strRowId = hidPROPERTY_ID.Value;
                if (strRowId.ToUpper().Equals("NEW"))
                {
                    objPropertyDetailInfo = new ClsPropertyDetailInfo();
                    this.getFormValues(objPropertyDetailInfo);
                   

                    objPropertyDetailInfo.CUSTOMER_ID.CurrentValue = hidCUSTOMER_ID.Value;
                    objPropertyDetailInfo.POLICY_ID.CurrentValue = hidPOLICY_ID.Value;
                    objPropertyDetailInfo.POLICY_VERSION_ID.CurrentValue = hidPOLICY_VERSION_ID.Value;
                    objPropertyDetailInfo.LOCATION_ID.CurrentValue =int.Parse(hidLOCATION_ID.Value);
                    objPropertyDetailInfo.PREMISES_ID.CurrentValue = int.Parse(hidPREMISES_ID.Value);
                    objPropertyDetailInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objPropertyDetailInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objPropertyDetail.AddPropDetailADD(objPropertyDetailInfo, XmlFullFilePath);
                    hidPROPERTY_ID.Value = objPropertyDetailInfo.PROPERTY_ID.CurrentValue.ToString();


                    if (intRetval > 0)
                    {
                        hidPROPERTY_ID.Value = objPropertyDetailInfo.PROPERTY_ID.CurrentValue.ToString();

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

                    objPropertyDetailInfo = (ClsPropertyDetailInfo)base.GetPageModelObject();
                    this.getFormValues(objPropertyDetailInfo);

                    objPropertyDetailInfo.CUSTOMER_ID.CurrentValue = hidCUSTOMER_ID.Value;
                    objPropertyDetailInfo.POLICY_ID.CurrentValue = hidPOLICY_ID.Value;
                    objPropertyDetailInfo.POLICY_VERSION_ID.CurrentValue = hidPOLICY_VERSION_ID.Value;
                    objPropertyDetailInfo.LOCATION_ID.CurrentValue = int.Parse(hidLOCATION_ID.Value);
                    objPropertyDetailInfo.PREMISES_ID.CurrentValue = int.Parse(hidPREMISES_ID.Value);
                    objPropertyDetailInfo.PROPERTY_ID.CurrentValue = int.Parse(hidPROPERTY_ID.Value);

                    objPropertyDetailInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objPropertyDetailInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objPropertyDetail.updatePropDetail(objPropertyDetailInfo, XmlFullFilePath);



                    if (intRetval > 0)
                    {
                        hidPROPERTY_ID.Value = objPropertyDetailInfo.PROPERTY_ID.CurrentValue.ToString();
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {

            ClsPropertyDetailInfo objPropertyDetailInfo;
            int intRetval = 0;
            try
            {
                objPropertyDetailInfo = (ClsPropertyDetailInfo)base.GetPageModelObject();

                objPropertyDetailInfo.CUSTOMER_ID.CurrentValue = hidCUSTOMER_ID.Value;
                objPropertyDetailInfo.POLICY_ID.CurrentValue = hidPOLICY_ID.Value;
                objPropertyDetailInfo.POLICY_VERSION_ID.CurrentValue = hidPOLICY_VERSION_ID.Value;
                objPropertyDetailInfo.LOCATION_ID.CurrentValue = int.Parse(hidLOCATION_ID.Value);
                objPropertyDetailInfo.PREMISES_ID.CurrentValue = int.Parse(hidPREMISES_ID.Value);
                objPropertyDetailInfo.PROPERTY_ID.CurrentValue = int.Parse(hidPROPERTY_ID.Value);

                objPropertyDetailInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                objPropertyDetailInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                intRetval = objPropertyDetail.DelPropDetail(objPropertyDetailInfo, XmlFullFilePath);

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
                hidPROPERTY_ID.Value = "NEW";
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