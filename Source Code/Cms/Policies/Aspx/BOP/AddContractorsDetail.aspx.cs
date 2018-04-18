/******************************************************************************************
<Author					: -		Sneha
<Start Date				: -		24-11-2011
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
    public partial class AddContractorsDetail : Cms.Policies.policiesbase
    {
        private String strRowId = String.Empty;
        private string XmlFullFilePath = "";
        public string CUSTOMER_ID;
        public string POLICY_ID;
        public string POLICY_VER_ID;
        Cms.BusinessLayer.BlApplication.ClsContractorsDetail objContractorsDetail = new ClsContractorsDetail();
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
            XmlFullFilePath = Request.PhysicalApplicationPath + "/Policies/support/PageXml/" + strSysID + "/" + "AddContractorsDetail.xml";

                if (!Page.IsPostBack)
                {
                    getdata();
                    if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/Policies/support/PageXML/" + strSysID, "AddContractorsDetail.xml"))
                        setPageControls(Page, Request.PhysicalApplicationPath + "/Policies/support/PageXml/" + strSysID + "/AddContractorsDetail.xml");

                    if (hidCUSTOMER_ID.Value != "" && hidPOLICY_ID.Value != "" && hidPOLICY_VERSION_ID.Value != "" && hidLOCATION_ID.Value != "" && hidPREMISES_ID.Value != "")
                    {
                        hidCONTRACTOR_ID.Value = "1";
                        ClsContractorsDetailInfo objContractorsDetailInfo;

                        objContractorsDetailInfo = objContractorsDetail.FetchId(hidCUSTOMER_ID.Value, hidPOLICY_ID.Value, hidPOLICY_VERSION_ID.Value, hidPREMISES_ID.Value, hidLOCATION_ID.Value);

                        hidCONTRACTOR_ID.Value = objContractorsDetailInfo.CONTRACTOR_ID.CurrentValue.ToString();
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

        protected void GetOldDataObject()
        {
            ClsContractorsDetailInfo objContractorsDetailInfo = new ClsContractorsDetailInfo();
            objContractorsDetailInfo.CONTRACTOR_ID.CurrentValue = int.Parse(hidCONTRACTOR_ID.Value);
            
            objContractorsDetailInfo = objContractorsDetail.FetchData(Int32.Parse(hidCONTRACTOR_ID.Value), Int32.Parse(hidCUSTOMER_ID.Value), Int32.Parse(hidPOLICY_ID.Value), Int32.Parse(hidPOLICY_VERSION_ID.Value), Int32.Parse(hidLOCATION_ID.Value), Int32.Parse(hidPREMISES_ID.Value));
            PopulatePageFromEbixModelObject(this.Page, objContractorsDetailInfo);
            base.SetPageModelObject(objContractorsDetailInfo);
            
            if (objContractorsDetailInfo.IS_EXPL_ENVRNT.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_EXPL_ENVRNT.Checked = true;
            }
            else
            {
                chkIS_EXPL_ENVRNT.Checked = false;
            }

            if (objContractorsDetailInfo.IS_FIRE_ALARM.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_FIRE_ALARM.Checked = true;
            }
            else
            {
                chkIS_FIRE_ALARM.Checked = false;
            }

            if (objContractorsDetailInfo.IS_HOSPITALS.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_HOSPITALS.Checked = true;
            }
            else
            {
                chkIS_HOSPITALS.Checked = false;
            }

            if (objContractorsDetailInfo.IS_SWIMMING_POOL.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_SWIMMING_POOL.Checked = true;
            }
            else
            {
                chkIS_SWIMMING_POOL.Checked = false;
            }

            if (objContractorsDetailInfo.IS_BRG_ALARM.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_BRG_ALARM.Checked = true;
            }
            else
            {
                chkIS_BRG_ALARM.Checked = false;
            }

            if (objContractorsDetailInfo.IS_PWR_PLANTS.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_PWR_PLANTS.Checked = true;
            }
            else
            {
                chkIS_PWR_PLANTS.Checked = false;
            }

            if (objContractorsDetailInfo.IS_BCK_EQUIPMNT.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_BCK_EQUIPMNT.Checked = true;
            }
            else
            {
                chkIS_BCK_EQUIPMNT.Checked = false;
            }

            if (objContractorsDetailInfo.IS_LIVE_WIRES.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_LIVE_WIRES.Checked = true;
            }
            else
            {
                chkIS_LIVE_WIRES.Checked = false;
            }

            if (objContractorsDetailInfo.IS_ARPT_CONSTRCT.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_ARPT_CONSTRCT.Checked = true;
            }
            else
            {
                chkIS_ARPT_CONSTRCT.Checked = false;
            }

            if (objContractorsDetailInfo.IS_HIGH_VOLTAGE.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_HIGH_VOLTAGE.Checked = true;
            }
            else
            {
                chkIS_HIGH_VOLTAGE.Checked = false;
            }

            if (objContractorsDetailInfo.IS_TRAFFIC_WRK.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_TRAFFIC_WRK.Checked = true;
            }
            else
            {
                chkIS_TRAFFIC_WRK.Checked = false;
            }

            if (objContractorsDetailInfo.IS_LND_FILL.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_LND_FILL.Checked = true;
            }
            else
            {
                chkIS_LND_FILL.Checked = false;
            }

            if (objContractorsDetailInfo.IS_DAM_CONSTRNT.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_DAM_CONSTRNT.Checked = true;
            }
            else
            {
                chkIS_DAM_CONSTRNT.Checked = false;
            }

            if (objContractorsDetailInfo.MAJOR_ELECT.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkMAJOR_ELECT.Checked = true;
            }
            else
            {
                chkMAJOR_ELECT.Checked = false;
            }

            if (objContractorsDetailInfo.IS_REFINERY.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_REFINERY.Checked = true;
            }
            else
            {
                chkIS_REFINERY.Checked = false;
            }

            if (objContractorsDetailInfo.IS_HZD_MATERIAL.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_HZD_MATERIAL.Checked = true;
            }
            else
            {
                chkIS_HZD_MATERIAL.Checked = false;
            }

            if (objContractorsDetailInfo.IS_PETRO_PLNT.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_PETRO_PLNT.Checked = true;
            }
            else
            {
                chkIS_PETRO_PLNT.Checked = false;
            }

            if (objContractorsDetailInfo.IS_NUCL_PLNT.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_NUCL_PLNT.Checked = true;
            }
            else
            {
                chkIS_NUCL_PLNT.Checked = false;
            }

            if (objContractorsDetailInfo.IS_PWR_LINES.CurrentValue == Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES))
            {
                chkIS_PWR_LINES.Checked = true;
            }
            else
            {
                chkIS_PWR_LINES.Checked = false;
            }

            
        }

        private void getFormValues(ClsContractorsDetailInfo objContractorsDetailInfo)
        {
            if (txtTYP_CONTRACTOR.Text != "")
            {
                objContractorsDetailInfo.TYP_CONTRACTOR.CurrentValue = Convert.ToString(txtTYP_CONTRACTOR.Text);
            }
            if (txtTOT_CST_PST_YR.Text != "")
            {
                objContractorsDetailInfo.TOT_CST_PST_YR.CurrentValue = Convert.ToDouble(txtTOT_CST_PST_YR.Text);
            }
            if (txtYR_EXP.Text != "")
            {
                objContractorsDetailInfo.YR_EXP.CurrentValue = Convert.ToString(txtYR_EXP.Text);
            }
            if (txtCONT_LICENSE.Text != "")
            {
                objContractorsDetailInfo.CONT_LICENSE.CurrentValue = Convert.ToString(txtCONT_LICENSE.Text);
            }

            if (cmbLICENSE_HOLDER.SelectedValue != "")
            {
                objContractorsDetailInfo.LICENSE_HOLDER.CurrentValue = Convert.ToString(cmbLICENSE_HOLDER.SelectedValue);
            }

            if (cmbLMT_CONTRACTOR_OCC.SelectedValue != "")
            {
                objContractorsDetailInfo.LMT_CONTRACTOR_OCC.CurrentValue = Convert.ToString(cmbLMT_CONTRACTOR_OCC.SelectedValue);
            }
            if (cmbLMT_CONTRACTOR_AGG.SelectedValue != "")
            {
                objContractorsDetailInfo.LMT_CONTRACTOR_AGG.CurrentValue = Convert.ToString(cmbLMT_CONTRACTOR_AGG.SelectedValue);
            }

            if (chkIS_EXPL_ENVRNT.Checked)
            {
                objContractorsDetailInfo.IS_EXPL_ENVRNT.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_EXPL_ENVRNT.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }


            if (chkIS_FIRE_ALARM.Checked)
            {
                objContractorsDetailInfo.IS_FIRE_ALARM.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_FIRE_ALARM.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }


            if (chkIS_HOSPITALS.Checked)
            {
                objContractorsDetailInfo.IS_HOSPITALS.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_HOSPITALS.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkIS_SWIMMING_POOL.Checked)
            {
                objContractorsDetailInfo.IS_SWIMMING_POOL.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_SWIMMING_POOL.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkIS_BRG_ALARM.Checked)
            {
                objContractorsDetailInfo.IS_BRG_ALARM.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_BRG_ALARM.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkIS_PWR_PLANTS.Checked)
            {
                objContractorsDetailInfo.IS_PWR_PLANTS.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_PWR_PLANTS.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkIS_BCK_EQUIPMNT.Checked)
            {
                objContractorsDetailInfo.IS_BCK_EQUIPMNT.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_BCK_EQUIPMNT.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkIS_LIVE_WIRES.Checked)
            {
                objContractorsDetailInfo.IS_LIVE_WIRES.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_LIVE_WIRES.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkIS_ARPT_CONSTRCT.Checked)
            {
                objContractorsDetailInfo.IS_ARPT_CONSTRCT.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_ARPT_CONSTRCT.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkIS_HIGH_VOLTAGE.Checked)
            {
                objContractorsDetailInfo.IS_HIGH_VOLTAGE.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_HIGH_VOLTAGE.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkIS_TRAFFIC_WRK.Checked)
            {
                objContractorsDetailInfo.IS_TRAFFIC_WRK.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_TRAFFIC_WRK.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkIS_LND_FILL.Checked)
            {
                objContractorsDetailInfo.IS_LND_FILL.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_LND_FILL.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkIS_DAM_CONSTRNT.Checked)
            {
                objContractorsDetailInfo.IS_DAM_CONSTRNT.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_DAM_CONSTRNT.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkIS_REFINERY.Checked)
            {
                objContractorsDetailInfo.IS_REFINERY.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_REFINERY.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkIS_HZD_MATERIAL.Checked)
            {
                objContractorsDetailInfo.IS_HZD_MATERIAL.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_HZD_MATERIAL.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkIS_PETRO_PLNT.Checked)
            {
                objContractorsDetailInfo.IS_PETRO_PLNT.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_PETRO_PLNT.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkIS_NUCL_PLNT.Checked)
            {
                objContractorsDetailInfo.IS_NUCL_PLNT.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_NUCL_PLNT.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkIS_PWR_LINES.Checked)
            {
                objContractorsDetailInfo.IS_PWR_LINES.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.IS_PWR_LINES.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }

            if (chkMAJOR_ELECT.Checked)
            {
                objContractorsDetailInfo.MAJOR_ELECT.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            }
            else
            {
                objContractorsDetailInfo.MAJOR_ELECT.CurrentValue = Convert.ToString(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10963;
            }



            if (cmbDRW_PLANS.SelectedValue != "")
            {
                objContractorsDetailInfo.DRW_PLANS.CurrentValue = Convert.ToString(cmbDRW_PLANS.SelectedValue);
            }

            if (cmbOPR_BLASTING.SelectedValue != "")
            {
                objContractorsDetailInfo.OPR_BLASTING.CurrentValue = Convert.ToString(cmbOPR_BLASTING.SelectedValue);
            }
            if (cmbOPR_TRENCHING.SelectedValue != "")
            {
                objContractorsDetailInfo.OPR_TRENCHING.CurrentValue = Convert.ToString(cmbOPR_TRENCHING.SelectedValue);
            }

            if (cmbOPR_EXCAVACATION.SelectedValue != "")
            {
                objContractorsDetailInfo.OPR_EXCAVACATION.CurrentValue = Convert.ToString(cmbOPR_EXCAVACATION.SelectedValue);
            }

            if (cmbIS_SECTY_POLICY.SelectedValue != "")
            {
                objContractorsDetailInfo.IS_SECTY_POLICY.CurrentValue = Convert.ToString(cmbIS_SECTY_POLICY.SelectedValue);
            }
            if (cmbANY_DEMOLITION.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_DEMOLITION.CurrentValue = Convert.ToString(cmbANY_DEMOLITION.SelectedValue);
            }
            if (cmbANY_CRANES.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_CRANES.CurrentValue = Convert.ToString(cmbANY_CRANES.SelectedValue);
            }

            if (txtPERCENT_ROOFING.Text != "")
            {
                objContractorsDetailInfo.PERCENT_ROOFING.CurrentValue = Convert.ToString(txtPERCENT_ROOFING.Text);
            }

            if (cmbCARRY_LIMITS.SelectedValue != "")
            {
                objContractorsDetailInfo.CARRY_LIMITS.CurrentValue = Convert.ToString(cmbCARRY_LIMITS.SelectedValue);
            }
            if (cmbANY_SHOP_WRK.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_SHOP_WRK.CurrentValue = Convert.ToString(cmbANY_SHOP_WRK.SelectedValue);
            }
            if (txtPERCENT_RENOVATION.Text != "")
            {
                objContractorsDetailInfo.PERCENT_RENOVATION.CurrentValue = Convert.ToString(txtPERCENT_RENOVATION.Text);
            }

            if (cmbANY_GUTTING.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_GUTTING.CurrentValue = Convert.ToString(cmbANY_GUTTING.SelectedValue);
            }
            if (txtPERCENT_SNOWPLOWING.Text != "")
            {
                objContractorsDetailInfo.PERCENT_SNOWPLOWING.CurrentValue = Convert.ToString(txtPERCENT_SNOWPLOWING.Text);
            }
            if (cmbANY_WRK_LOAD.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_WRK_LOAD.CurrentValue = Convert.ToString(cmbANY_WRK_LOAD.SelectedValue);
            }
            if (txtPERCENT_PNTG_OUTSIDE.Text != "")
            {
                objContractorsDetailInfo.PERCENT_PNTG_OUTSIDE.CurrentValue = Convert.ToString(txtPERCENT_PNTG_OUTSIDE.Text);
            }
            if (cmbANY_PNTG_TNKS.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_PNTG_TNKS.CurrentValue = Convert.ToString(cmbANY_PNTG_TNKS.SelectedValue);
            }
            if (cmbANY_EPOXIES.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_EPOXIES.CurrentValue = Convert.ToString(cmbANY_EPOXIES.SelectedValue);
            }
            if (cmbANY_ACID.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_ACID.CurrentValue = Convert.ToString(cmbANY_ACID.SelectedValue);
            }
            if (cmbANY_LEASE_EQUIPMNT.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_LEASE_EQUIPMNT.CurrentValue = Convert.ToString(cmbANY_LEASE_EQUIPMNT.SelectedValue);
            }
            if (cmbANY_BOATS_OWND.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_BOATS_OWND.CurrentValue = Convert.ToString(cmbANY_BOATS_OWND.SelectedValue);
            }
            if (cmbDRCT_SIGHT_WRK_IN_PRGRSS.SelectedValue != "")
            {
                objContractorsDetailInfo.DRCT_SIGHT_WRK_IN_PRGRSS.CurrentValue = Convert.ToString(cmbDRCT_SIGHT_WRK_IN_PRGRSS.SelectedValue);
            }
            if (cmbPRDCT_SOLD_IN_APPL_NAME.SelectedValue != "")
            {
                objContractorsDetailInfo.PRDCT_SOLD_IN_APPL_NAME.CurrentValue = Convert.ToString(cmbPRDCT_SOLD_IN_APPL_NAME.SelectedValue);
            }
            if (cmbUTILITY_CMPNY_CALLED.SelectedValue != "")
            {
                objContractorsDetailInfo.UTILITY_CMPNY_CALLED.CurrentValue = Convert.ToString(cmbUTILITY_CMPNY_CALLED.SelectedValue);
            }


            if (txtTYP_IN_DGGN_PRCSS.Text != "")
            {
                objContractorsDetailInfo.TYP_IN_DGGN_PRCSS.CurrentValue = Convert.ToString(txtTYP_IN_DGGN_PRCSS.Text);
            }

            if (txtPERCENT_PNTG_SPRY.Text != "")
            {
                objContractorsDetailInfo.PERCENT_PNTG_SPRY.CurrentValue = Convert.ToString(txtPERCENT_PNTG_SPRY.Text);
            }

            if (txtPERCENT_PNTG_INSIDE.Text != "")
            {
                objContractorsDetailInfo.PERCENT_PNTG_INSIDE.CurrentValue = Convert.ToString(txtPERCENT_PNTG_INSIDE.Text);
            }

            if (txtPERCENT_SPRINKLE_WRK.Text != "")
            {
                objContractorsDetailInfo.PERCENT_SPRINKLE_WRK.CurrentValue = Convert.ToString(txtPERCENT_SPRINKLE_WRK.Text);
            }

            if (txtPERCENT_TREE_TRIMNG.Text != "")
            {
                objContractorsDetailInfo.PERCENT_TREE_TRIMNG.CurrentValue = Convert.ToString(txtPERCENT_TREE_TRIMNG.Text);
            }

            if (txtPER_RESIDENT.Text != "")
            {
                objContractorsDetailInfo.PER_RESIDENT.CurrentValue = Convert.ToString(txtPER_RESIDENT.Text);
            }

            if (txtPER_COMMERICAL.Text != "")
            {
                objContractorsDetailInfo.PER_COMMERICAL.CurrentValue = Convert.ToString(txtPER_COMMERICAL.Text);
            }

            if (txtPER_CONST.Text != "")
            {
                objContractorsDetailInfo.PER_CONST.CurrentValue = Convert.ToString(txtPER_CONST.Text);
            }

            if (txtPER_REMODEL.Text != "")
            {
                objContractorsDetailInfo.PER_REMODEL.CurrentValue = Convert.ToString(txtPER_REMODEL.Text);
            }


            if (cmbPERMIT_OBTAINED.SelectedValue != "")
            {
                objContractorsDetailInfo.PERMIT_OBTAINED.CurrentValue = Convert.ToString(cmbPERMIT_OBTAINED.SelectedValue);
            }
            if (cmbANY_EXCAVAION.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_EXCAVAION.CurrentValue = Convert.ToString(cmbANY_EXCAVAION.SelectedValue);
            }
            if (cmbANY_PEST_SPRAY.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_PEST_SPRAY.CurrentValue = Convert.ToString(cmbANY_PEST_SPRAY.SelectedValue);
            }
            if (cmbANY_WRK_OFFSEASON.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_WRK_OFFSEASON.CurrentValue = Convert.ToString(cmbANY_WRK_OFFSEASON.SelectedValue);
            }
            if (cmbANY_MIX_TRANSIT.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_MIX_TRANSIT.CurrentValue = Convert.ToString(cmbANY_MIX_TRANSIT.SelectedValue);
            }
            if (cmbANY_CONTSRUCTION_WRK.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_CONTSRUCTION_WRK.CurrentValue = Convert.ToString(cmbANY_CONTSRUCTION_WRK.SelectedValue);
            }
            if (cmbANY_WRK_ABVE_THREE_STORIES.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_WRK_ABVE_THREE_STORIES.CurrentValue = Convert.ToString(cmbANY_WRK_ABVE_THREE_STORIES.SelectedValue);
            }
            if (cmbANY_SCAFHOLDING_ABVE_TWELVE_FEET.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_SCAFHOLDING_ABVE_TWELVE_FEET.CurrentValue = Convert.ToString(cmbANY_SCAFHOLDING_ABVE_TWELVE_FEET.SelectedValue);
            }
            if (cmbANY_PNTG_TOWERS.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_PNTG_TOWERS.CurrentValue = Convert.ToString(cmbANY_PNTG_TOWERS.SelectedValue);
            }
            if (cmbANY_SPRAY_GUNS.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_SPRAY_GUNS.CurrentValue = Convert.ToString(cmbANY_SPRAY_GUNS.SelectedValue);
            }
            if (cmbANY_REMOVAL_DONE.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_REMOVAL_DONE.CurrentValue = Convert.ToString(cmbANY_REMOVAL_DONE.SelectedValue);
            }
            if (cmbANY_WAXING_FLOORS.SelectedValue != "")
            {
                objContractorsDetailInfo.ANY_WAXING_FLOORS.CurrentValue = Convert.ToString(cmbANY_WAXING_FLOORS.SelectedValue);
            }
          
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;
            ClsContractorsDetailInfo objContractorsDetailInfo;
            try
            {
                strRowId = hidCONTRACTOR_ID.Value;
                if (strRowId.ToUpper().Equals("NEW"))
                {
                    objContractorsDetailInfo = new ClsContractorsDetailInfo();
                    this.getFormValues(objContractorsDetailInfo);


                    objContractorsDetailInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                    objContractorsDetailInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                    objContractorsDetailInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
                    objContractorsDetailInfo.LOCATION_ID.CurrentValue = int.Parse(hidLOCATION_ID.Value);
                    objContractorsDetailInfo.PREMISES_ID.CurrentValue = int.Parse(hidPREMISES_ID.Value);
                    objContractorsDetailInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objContractorsDetailInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objContractorsDetail.AddConrtDetailADD(objContractorsDetailInfo, XmlFullFilePath);
                    hidCONTRACTOR_ID.Value = objContractorsDetailInfo.CONTRACTOR_ID.CurrentValue.ToString();


                    if (intRetval > 0)
                    {
                        hidCONTRACTOR_ID.Value = objContractorsDetailInfo.CONTRACTOR_ID.CurrentValue.ToString();

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

                    objContractorsDetailInfo = (ClsContractorsDetailInfo)base.GetPageModelObject();
                    this.getFormValues(objContractorsDetailInfo);

                    objContractorsDetailInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                    objContractorsDetailInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                    objContractorsDetailInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
                    objContractorsDetailInfo.LOCATION_ID.CurrentValue = int.Parse(hidLOCATION_ID.Value);
                    objContractorsDetailInfo.PREMISES_ID.CurrentValue = int.Parse(hidPREMISES_ID.Value);
                    objContractorsDetailInfo.CONTRACTOR_ID.CurrentValue = int.Parse(hidCONTRACTOR_ID.Value);

                    objContractorsDetailInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objContractorsDetailInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objContractorsDetail.updateConrtDetail(objContractorsDetailInfo, XmlFullFilePath);



                    if (intRetval > 0)
                    {
                        hidCONTRACTOR_ID.Value = objContractorsDetailInfo.CONTRACTOR_ID.CurrentValue.ToString();
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

            ClsContractorsDetailInfo objContractorsDetailInfo;
            int intRetval = 0;
            try
            {
                objContractorsDetailInfo = (ClsContractorsDetailInfo)base.GetPageModelObject();

                objContractorsDetailInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                objContractorsDetailInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                objContractorsDetailInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
                objContractorsDetailInfo.LOCATION_ID.CurrentValue = int.Parse(hidLOCATION_ID.Value);
                objContractorsDetailInfo.PREMISES_ID.CurrentValue = int.Parse(hidPREMISES_ID.Value);
                objContractorsDetailInfo.CONTRACTOR_ID.CurrentValue = int.Parse(hidCONTRACTOR_ID.Value);

                objContractorsDetailInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                objContractorsDetailInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                intRetval = objContractorsDetail.DelConrtDetail(objContractorsDetailInfo, XmlFullFilePath);

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


    }
}