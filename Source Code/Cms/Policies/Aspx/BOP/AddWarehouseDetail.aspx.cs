/******************************************************************************************
<Author					: -		Sneha
<Start Date				: -		23-11-2011
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
    public partial class AddWarehouseDetail :Cms.Policies.policiesbase
    {
        private String strRowId = String.Empty;
        private string XmlFullFilePath = "";
        public string CUSTOMER_ID;
        public string POLICY_ID;
        public string POLICY_VER_ID;
        ClsWarehouseDetail objWarehouseDetail = new ClsWarehouseDetail();
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

            XmlFullFilePath = Request.PhysicalApplicationPath + "/Policies/support/PageXml/" + strSysID + "/" + "AddWarehouseDetail.xml";

            if (!Page.IsPostBack)
            {
                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/Policies/support/PageXML/" + strSysID, "AddWarehouseDetail.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "/Policies/support/PageXml/" + strSysID + "/AddWarehouseDetail.xml");
                FillDropDown();
                getdata();
                if (hidCUSTOMER_ID.Value != "" && hidPOLICY_ID.Value != "" && hidPOLICY_VERSION_ID.Value != "" && hidLOCATION_ID.Value != "" && hidPREMISES_ID.Value != "")
                {
                    hidWAREHOUSE_ID.Value = "1";
                    ClsWarehouseDetailInfo objWarehouseDetailInfo;

                    objWarehouseDetailInfo = objWarehouseDetail.FetchId(hidCUSTOMER_ID.Value, hidPOLICY_ID.Value, hidPOLICY_VERSION_ID.Value, hidPREMISES_ID.Value, hidLOCATION_ID.Value);

                    hidWAREHOUSE_ID.Value = objWarehouseDetailInfo.WAREHOUSE_ID.CurrentValue.ToString();
                    if (hidWAREHOUSE_ID.Value == "-1")
                    {
                        hidWAREHOUSE_ID.Value = "NEW";
                        btnDelete.Style.Add("display", "none");
                    }
                    else
                    {
                        strRowId = hidWAREHOUSE_ID.Value;
                        this.GetOldDataObject();
                    }
                }
                else
                {
                    hidWAREHOUSE_ID.Value = "NEW";
                    btnDelete.Style.Add("display", "none");
                }
                strRowId = hidWAREHOUSE_ID.Value;

            }

        }

        protected void FillDropDown()
        {
           cmbBUILDINGS.Items.Insert(0,"0");
           cmbBUILDINGS.Items.Insert(1,"1");
           cmbBUILDINGS.Items.Insert(2,"2");

           cmbSTORAGEUNITS.Items.Insert(0, "0");
           cmbSTORAGEUNITS.Items.Insert(1, "1");
           cmbSTORAGEUNITS.Items.Insert(2, "2");

           cmbNO_DYS_TENANT_PROP_SOLD.Items.Insert(0, "0");
           cmbNO_DYS_TENANT_PROP_SOLD.Items.Insert(1, "1");
           cmbNO_DYS_TENANT_PROP_SOLD.Items.Insert(2, "2");
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
            ClsWarehouseDetailInfo objWarehouseDetailInfo = new ClsWarehouseDetailInfo();

           // hidWAREHOUSE_ID.Value =Convert.ToString(objWarehouseDetailInfo.WAREHOUSE_ID.CurrentValue);

            objWarehouseDetailInfo = objWarehouseDetail.FetchData(Int32.Parse(hidWAREHOUSE_ID.Value), hidCUSTOMER_ID.Value, hidPOLICY_ID.Value, hidPOLICY_VERSION_ID.Value, hidLOCATION_ID.Value, hidPREMISES_ID.Value);
           // PopulatePageFromEbixModelObject(this.Page, objWarehouseDetailInfo);


            if (objWarehouseDetailInfo.BUILDINGS.CurrentValue.ToString() != "" && objWarehouseDetailInfo.BUILDINGS.CurrentValue.ToString() != null && objWarehouseDetailInfo.BUILDINGS.CurrentValue.ToString()!="-1")
            {
                cmbBUILDINGS.SelectedValue =Convert.ToInt32(objWarehouseDetailInfo.BUILDINGS.CurrentValue).ToString();
            }

            if (objWarehouseDetailInfo.OWN_MGMR.CurrentValue.ToString() != "" && objWarehouseDetailInfo.OWN_MGMR.CurrentValue.ToString() != null)
            {
                cmbOWN_MGMR.SelectedValue = objWarehouseDetailInfo.OWN_MGMR.CurrentValue.ToString();
            }
            if (objWarehouseDetailInfo.STORAGEUNITS.CurrentValue.ToString() != "" && objWarehouseDetailInfo.STORAGEUNITS.CurrentValue.ToString()!=null && objWarehouseDetailInfo.STORAGEUNITS.CurrentValue.ToString()!="-1")
            {
                cmbSTORAGEUNITS.SelectedValue = Convert.ToInt32(objWarehouseDetailInfo.STORAGEUNITS.CurrentValue).ToString();
            }
            if (objWarehouseDetailInfo.IS_FENCED.CurrentValue.ToString() != "" && objWarehouseDetailInfo.IS_FENCED.CurrentValue.ToString()!=null) 
            {
                cmbIS_FENCED.SelectedValue=objWarehouseDetailInfo.IS_FENCED.CurrentValue.ToString();
            }
            if (objWarehouseDetailInfo.RES_MGMR.CurrentValue.ToString() != "" && objWarehouseDetailInfo.RES_MGMR.CurrentValue.ToString()!=null)
            {
                cmbRES_MGMR.SelectedValue= objWarehouseDetailInfo.RES_MGMR.CurrentValue.ToString();
            }
            if (objWarehouseDetailInfo.IS_PRKNG_AVL.CurrentValue.ToString() != "" && objWarehouseDetailInfo.IS_PRKNG_AVL.CurrentValue.ToString()!=null)
            {
                cmbIS_PRKNG_AVL.SelectedValue= objWarehouseDetailInfo.IS_PRKNG_AVL.CurrentValue.ToString();
            }
            if (objWarehouseDetailInfo.DAYTIME_ATTNDT.CurrentValue.ToString() != "" && objWarehouseDetailInfo.DAYTIME_ATTNDT.CurrentValue.ToString()!=null)
            {
                cmbDAYTIME_ATTNDT.SelectedValue= objWarehouseDetailInfo.DAYTIME_ATTNDT.CurrentValue.ToString();
            }
            if (objWarehouseDetailInfo.IS_BOAT_PRKNG_AVL.CurrentValue.ToString() != "" && objWarehouseDetailInfo.IS_BOAT_PRKNG_AVL.CurrentValue.ToString()!=null)
            {
                  cmbIS_BOAT_PRKNG_AVL.SelectedValue=  objWarehouseDetailInfo.IS_BOAT_PRKNG_AVL.CurrentValue.ToString();
            }
            if (cmbANY_BUSS_ACTY.SelectedValue != "")
            {
                objWarehouseDetailInfo.ANY_BUSS_ACTY.CurrentValue = Convert.ToString(cmbANY_BUSS_ACTY.SelectedValue);
            }
            if (objWarehouseDetailInfo.GUARD_DOG.CurrentValue.ToString() != "" && objWarehouseDetailInfo.GUARD_DOG.CurrentValue.ToString()!=null)
            {
                cmbGUARD_DOG.SelectedValue= objWarehouseDetailInfo.GUARD_DOG.CurrentValue.ToString();
            }
            if (objWarehouseDetailInfo.VLT_STYLE.CurrentValue.ToString() != "" && objWarehouseDetailInfo.VLT_STYLE.CurrentValue.ToString()!=null)
            {
                cmbVLT_STYLE.SelectedValue= objWarehouseDetailInfo.VLT_STYLE.CurrentValue.ToString();
            }
            if (objWarehouseDetailInfo.ANY_FIREARMS.CurrentValue.ToString() != "" && objWarehouseDetailInfo.ANY_FIREARMS.CurrentValue.ToString()!=null)
            {
                cmbANY_FIREARMS.SelectedValue= objWarehouseDetailInfo.ANY_FIREARMS.CurrentValue.ToString();
            }
            if (objWarehouseDetailInfo.TRUCK_RENTAL.CurrentValue.ToString() != "" && objWarehouseDetailInfo.TRUCK_RENTAL.CurrentValue.ToString()!=null)
            {
                cmbTRUCK_RENTAL.SelectedValue= objWarehouseDetailInfo.TRUCK_RENTAL.CurrentValue.ToString();
            }
            if ( objWarehouseDetailInfo.TENANT_LCKS_CHK.CurrentValue.ToString() != "" &&  objWarehouseDetailInfo.TENANT_LCKS_CHK.CurrentValue.ToString()!=null)
            {
                cmbTENANT_LCKS_CHK.SelectedValue= objWarehouseDetailInfo.TENANT_LCKS_CHK.CurrentValue.ToString();
            }
            if (objWarehouseDetailInfo.MGMR_KYS_CUST_UNIT.CurrentValue.ToString() != "" && objWarehouseDetailInfo.MGMR_KYS_CUST_UNIT.CurrentValue.ToString()!=null)
            {
                cmbMGMR_KYS_CUST_UNIT.SelectedValue= objWarehouseDetailInfo.MGMR_KYS_CUST_UNIT.CurrentValue.ToString();
            }
            if (objWarehouseDetailInfo.ANY_BUSN_GUIDELINES.CurrentValue.ToString() != "" && objWarehouseDetailInfo.ANY_BUSN_GUIDELINES.CurrentValue.ToString()!=null)
            {
                cmbANY_BUSN_GUIDELINES.SelectedValue= objWarehouseDetailInfo.ANY_BUSN_GUIDELINES.CurrentValue.ToString();
            }
            if (objWarehouseDetailInfo.NOTICE_SENT.CurrentValue.ToString() != "" && objWarehouseDetailInfo.NOTICE_SENT.CurrentValue.ToString()!=null)
            {
               cmbNOTICE_SENT.SelectedValue= objWarehouseDetailInfo.NOTICE_SENT.CurrentValue.ToString();
            }
            if (objWarehouseDetailInfo.NO_DYS_TENANT_PROP_SOLD.CurrentValue.ToString() != "" && objWarehouseDetailInfo.NO_DYS_TENANT_PROP_SOLD.CurrentValue.ToString() != null && objWarehouseDetailInfo.NO_DYS_TENANT_PROP_SOLD.CurrentValue.ToString() != "-1")
            {
                cmbNO_DYS_TENANT_PROP_SOLD.SelectedValue=Convert.ToInt32(objWarehouseDetailInfo.NO_DYS_TENANT_PROP_SOLD.CurrentValue).ToString();
            }
            if (objWarehouseDetailInfo.SALES_TENANT_LST_TWELVE_MNTHS.CurrentValue.ToString() != "" && objWarehouseDetailInfo.SALES_TENANT_LST_TWELVE_MNTHS.CurrentValue.ToString() != null && objWarehouseDetailInfo.SALES_TENANT_LST_TWELVE_MNTHS.CurrentValue.ToString() != "-1")
            {
                txtSALES_TENANT_LST_TWELVE_MNTHS.Text = objWarehouseDetailInfo.SALES_TENANT_LST_TWELVE_MNTHS.CurrentValue.ToString();
            }
            else
            {
                txtSALES_TENANT_LST_TWELVE_MNTHS.Text = "";
            }
            if (objWarehouseDetailInfo.DISP_PUBL.CurrentValue.ToString() != "" && objWarehouseDetailInfo.DISP_PUBL.CurrentValue.ToString()!=null)
            {
                cmbDISP_PUBL.SelectedValue=objWarehouseDetailInfo.DISP_PUBL.CurrentValue.ToString();
            }
            if (objWarehouseDetailInfo.ANY_COLD_STORAGE.CurrentValue.ToString() != "" && objWarehouseDetailInfo.ANY_COLD_STORAGE.CurrentValue.ToString()!=null)
            {
                cmbANY_COLD_STORAGE.SelectedValue=objWarehouseDetailInfo.ANY_COLD_STORAGE.CurrentValue.ToString();
            }
            if (objWarehouseDetailInfo.ANY_CLIMATE_CNTL.CurrentValue.ToString() != "" && objWarehouseDetailInfo.ANY_CLIMATE_CNTL.CurrentValue.ToString()!=null)
            {
               cmbANY_CLIMATE_CNTL.SelectedValue= objWarehouseDetailInfo.ANY_CLIMATE_CNTL.CurrentValue.ToString();
            }
            if (objWarehouseDetailInfo.MGMR_TYPE.CurrentValue.ToString() != "" && objWarehouseDetailInfo.MGMR_TYPE.CurrentValue.ToString()!=null)
            {
                cmbMGMR_TYPE.SelectedValue=objWarehouseDetailInfo.MGMR_TYPE.CurrentValue.ToString();
            }
           

            base.SetPageModelObject(objWarehouseDetailInfo);   
        }

        private void getFormValues(ClsWarehouseDetailInfo objWarehouseDetailInfo)
        {
            if (cmbBUILDINGS.SelectedValue != "")
            {
                objWarehouseDetailInfo.BUILDINGS.CurrentValue = Convert.ToInt32(cmbBUILDINGS.SelectedValue);
            }
            if (cmbOWN_MGMR.SelectedValue != "")
            {
                objWarehouseDetailInfo.OWN_MGMR.CurrentValue = Convert.ToString(cmbOWN_MGMR.SelectedValue);
            }
            if (cmbSTORAGEUNITS.SelectedValue != "")
            {
                objWarehouseDetailInfo.STORAGEUNITS.CurrentValue = Convert.ToInt32(cmbSTORAGEUNITS.SelectedValue);
            }
            if (cmbIS_FENCED.SelectedValue != "")
            {
                objWarehouseDetailInfo.IS_FENCED.CurrentValue = Convert.ToString(cmbIS_FENCED.SelectedValue);
            }
            if (cmbRES_MGMR.SelectedValue != "")
            {
                objWarehouseDetailInfo.RES_MGMR.CurrentValue = Convert.ToString(cmbRES_MGMR.SelectedValue);
            }
            if (cmbIS_PRKNG_AVL.SelectedValue != "")
            {
                objWarehouseDetailInfo.IS_PRKNG_AVL.CurrentValue = Convert.ToString(cmbIS_PRKNG_AVL.SelectedValue);
            }
            if (cmbDAYTIME_ATTNDT.SelectedValue != "")
            {
                objWarehouseDetailInfo.DAYTIME_ATTNDT.CurrentValue = Convert.ToString(cmbDAYTIME_ATTNDT.SelectedValue);
            }
            if (cmbIS_BOAT_PRKNG_AVL.SelectedValue != "")
            {
                objWarehouseDetailInfo.IS_BOAT_PRKNG_AVL.CurrentValue = Convert.ToString(cmbIS_BOAT_PRKNG_AVL.SelectedValue);
            }
            if (cmbANY_BUSS_ACTY.SelectedValue != "")
            {
                objWarehouseDetailInfo.ANY_BUSS_ACTY.CurrentValue = Convert.ToString(cmbANY_BUSS_ACTY.SelectedValue);
            }
            if (cmbGUARD_DOG.SelectedValue != "")
            {
                objWarehouseDetailInfo.GUARD_DOG.CurrentValue = Convert.ToString(cmbGUARD_DOG.SelectedValue);
            }
            if (cmbVLT_STYLE.SelectedValue != "")
            {
                objWarehouseDetailInfo.VLT_STYLE.CurrentValue = Convert.ToString(cmbVLT_STYLE.SelectedValue);
            }
            if (cmbANY_FIREARMS.SelectedValue != "")
            {
                objWarehouseDetailInfo.ANY_FIREARMS.CurrentValue = Convert.ToString(cmbANY_FIREARMS.SelectedValue);
            }
            if (cmbTRUCK_RENTAL.SelectedValue != "")
            {
                objWarehouseDetailInfo.TRUCK_RENTAL.CurrentValue = Convert.ToString(cmbTRUCK_RENTAL.SelectedValue);
            }

            if (cmbTENANT_LCKS_CHK.SelectedValue != "")
            {
                objWarehouseDetailInfo.TENANT_LCKS_CHK.CurrentValue = Convert.ToString(cmbTENANT_LCKS_CHK.SelectedValue);
            }
            if (cmbMGMR_KYS_CUST_UNIT.SelectedValue != "")
            {
                objWarehouseDetailInfo.MGMR_KYS_CUST_UNIT.CurrentValue = Convert.ToString(cmbMGMR_KYS_CUST_UNIT.SelectedValue);
            }
            if (cmbANY_BUSN_GUIDELINES.SelectedValue != "")
            {
                objWarehouseDetailInfo.ANY_BUSN_GUIDELINES.CurrentValue = Convert.ToString(cmbANY_BUSN_GUIDELINES.SelectedValue);
            }
            if (cmbNOTICE_SENT.SelectedValue != "")
            {
                objWarehouseDetailInfo.NOTICE_SENT.CurrentValue = Convert.ToString(cmbNOTICE_SENT.SelectedValue);
            }
            if (cmbNO_DYS_TENANT_PROP_SOLD.SelectedValue != "")
            {
                objWarehouseDetailInfo.NO_DYS_TENANT_PROP_SOLD.CurrentValue = Convert.ToInt32(cmbNO_DYS_TENANT_PROP_SOLD.SelectedValue);
            }
            if (txtSALES_TENANT_LST_TWELVE_MNTHS.Text != "")
            {
                objWarehouseDetailInfo.SALES_TENANT_LST_TWELVE_MNTHS.CurrentValue = Convert.ToInt32(txtSALES_TENANT_LST_TWELVE_MNTHS.Text);
            }
            if (cmbDISP_PUBL.SelectedValue != "")
            {
                objWarehouseDetailInfo.DISP_PUBL.CurrentValue = Convert.ToString(cmbDISP_PUBL.SelectedValue);
            }
            if (cmbANY_COLD_STORAGE.SelectedValue != "")
            {
                objWarehouseDetailInfo.ANY_COLD_STORAGE.CurrentValue = Convert.ToString(cmbANY_COLD_STORAGE.SelectedValue);
            }
            if (cmbANY_CLIMATE_CNTL.SelectedValue != "")
            {
                objWarehouseDetailInfo.ANY_CLIMATE_CNTL.CurrentValue = Convert.ToString(cmbANY_CLIMATE_CNTL.SelectedValue);
            }
            if (cmbMGMR_TYPE.SelectedValue != "")
            {
                objWarehouseDetailInfo.MGMR_TYPE.CurrentValue = Convert.ToString(cmbMGMR_TYPE.SelectedValue);
            }
           


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;
            ClsWarehouseDetailInfo objWarehouseDetailInfo;
            try
            {
                strRowId = hidWAREHOUSE_ID.Value;
                if (strRowId.ToUpper().Equals("NEW"))
                {
                    objWarehouseDetailInfo = new ClsWarehouseDetailInfo();
                    this.getFormValues(objWarehouseDetailInfo);


                    objWarehouseDetailInfo.CUSTOMER_ID.CurrentValue = hidCUSTOMER_ID.Value;
                    objWarehouseDetailInfo.POLICY_ID.CurrentValue = hidPOLICY_ID.Value;
                    objWarehouseDetailInfo.POLICY_VERSION_ID.CurrentValue =int.Parse(hidPOLICY_VERSION_ID.Value);
                    objWarehouseDetailInfo.LOCATION_ID.CurrentValue = int.Parse(hidLOCATION_ID.Value);
                    objWarehouseDetailInfo.PREMISES_ID.CurrentValue = int.Parse(hidPREMISES_ID.Value);
                    objWarehouseDetailInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objWarehouseDetailInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objWarehouseDetail.AddWareHouseDetlADD(objWarehouseDetailInfo, XmlFullFilePath);
                    hidWAREHOUSE_ID.Value = objWarehouseDetailInfo.WAREHOUSE_ID.CurrentValue.ToString();


                    if (intRetval > 0)
                    {
                        hidWAREHOUSE_ID.Value = objWarehouseDetailInfo.WAREHOUSE_ID.CurrentValue.ToString();

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

                    objWarehouseDetailInfo = (ClsWarehouseDetailInfo)base.GetPageModelObject();
                    this.getFormValues(objWarehouseDetailInfo);

                    objWarehouseDetailInfo.CUSTOMER_ID.CurrentValue = hidCUSTOMER_ID.Value;
                    objWarehouseDetailInfo.POLICY_ID.CurrentValue = hidPOLICY_ID.Value;
                    objWarehouseDetailInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
                    objWarehouseDetailInfo.LOCATION_ID.CurrentValue = int.Parse(hidLOCATION_ID.Value);
                    objWarehouseDetailInfo.PREMISES_ID.CurrentValue = int.Parse(hidPREMISES_ID.Value);
                    objWarehouseDetailInfo.WAREHOUSE_ID.CurrentValue = int.Parse(hidWAREHOUSE_ID.Value);

                    objWarehouseDetailInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objWarehouseDetailInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objWarehouseDetail.updateWareHouseDetl(objWarehouseDetailInfo, XmlFullFilePath);



                    if (intRetval > 0)
                    {
                        hidWAREHOUSE_ID.Value = objWarehouseDetailInfo.WAREHOUSE_ID.CurrentValue.ToString();
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
            ClsWarehouseDetailInfo objWarehouseDetailInfo;
            int intRetval = 0;
            try
            {
                objWarehouseDetailInfo = (ClsWarehouseDetailInfo)base.GetPageModelObject();

                objWarehouseDetailInfo.CUSTOMER_ID.CurrentValue = hidCUSTOMER_ID.Value;
                objWarehouseDetailInfo.POLICY_ID.CurrentValue = hidPOLICY_ID.Value;
                objWarehouseDetailInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
                objWarehouseDetailInfo.LOCATION_ID.CurrentValue = int.Parse(hidLOCATION_ID.Value);
                objWarehouseDetailInfo.PREMISES_ID.CurrentValue = int.Parse(hidPREMISES_ID.Value);
                objWarehouseDetailInfo.WAREHOUSE_ID.CurrentValue = int.Parse(hidWAREHOUSE_ID.Value);

                objWarehouseDetailInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                objWarehouseDetailInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                intRetval = objWarehouseDetail.DelWareHouseDetl(objWarehouseDetailInfo, XmlFullFilePath);

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
                hidWAREHOUSE_ID.Value = "NEW";
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