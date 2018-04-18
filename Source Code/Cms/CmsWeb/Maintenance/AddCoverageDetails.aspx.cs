/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date				: -	7/4/2005 12:44:45 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 17/08/2005	
<Modified By				: - Anurag Verma
<Purpose				: - Chaingng maxlength of coverage code field from 5 to 8

<Modified Date			: - 05-08-2006
<Modified By			: - Ravindra Gupta
<Purpose				: - Removed Activate/Deactivate Button,added Start Date,End Date and
<							Disabled Date
*******************************************************************************************/
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
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls;
using System.Resources;
using System.Reflection;
using Cms.ExceptionPublisher;
using System.Xml;
using Cms.BusinessLayer.BlCommon;
using System.IO;

namespace Cms.CmsWeb.Maintenance
{
    /// <summary>
    /// Summary description for AddCoverageDetails.
    /// </summary>
    public class AddCoverageDetails : Cms.CmsWeb.cmsbase
    {
        protected System.Web.UI.WebControls.Label lblMessage;

        string oldXML;
        System.Resources.ResourceManager objResourceMgr;
        private string strRowId, strFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidENDORSEMENT_ID;
        protected System.Web.UI.WebControls.Label capCOV_REF_CODE;
        protected System.Web.UI.WebControls.TextBox txtCOV_REF_CODE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOV_REF_CODE;
        protected System.Web.UI.WebControls.Label capCOV_CODE;
        protected System.Web.UI.WebControls.TextBox txtCOV_CODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOV_CODE;
        protected System.Web.UI.WebControls.Label capCOV_DES;
        protected System.Web.UI.WebControls.TextBox txtCOV_DES;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOV_DES;
        protected System.Web.UI.WebControls.Label capSTATE_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_ID;
        protected System.Web.UI.WebControls.Label capLOB_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.WebControls.DropDownList cmbSTATE_ID;
        protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;
        protected System.Web.UI.WebControls.Label capDefault;
        protected System.Web.UI.WebControls.DropDownList cmbIS_DEFAULT;
        //protected System.Web.UI.WebControls.DropDownList cmbIS_Default;
        protected DataSet dsCoverageDetails;
        protected System.Web.UI.WebControls.Label capTYPE;
        protected System.Web.UI.WebControls.DropDownList cmbTYPE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvTYPE;
        protected System.Web.UI.WebControls.Label capPURPOSE;
        protected System.Web.UI.WebControls.DropDownList cmbPURPOSE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPURPOSE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOV_REF_CODE;
        protected System.Web.UI.WebControls.Label capLIMITTYPE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIMITTYPE;
        protected System.Web.UI.WebControls.Label lblLIMITTYPE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidMode;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLimitTypeValue;
        protected System.Web.UI.WebControls.CheckBox chkIsLimitApplicable;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDeductibleType;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDeductTypeValue;
        protected System.Web.UI.WebControls.Label capLimit_Applicable;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hid_CoverageUsed;
        protected System.Web.UI.WebControls.DropDownList cmbLIMIT_TYPE;
        protected System.Web.UI.WebControls.Label capINCLUDED;
        protected System.Web.UI.WebControls.TextBox txtINCLUDED;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvINCLUDED;
        protected System.Web.UI.WebControls.Label capCOVERAGE_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbCOVERAGE_TYPE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revINCLUDED;
        protected System.Web.UI.WebControls.Label capIS_MANDATORY;
        protected System.Web.UI.WebControls.DropDownList cmbIS_MANDATORY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_MANDATORY;
        protected System.Web.UI.WebControls.Label capRANK;
        protected System.Web.UI.WebControls.Label capheader;
        protected System.Web.UI.WebControls.TextBox txtRANK;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvRANK;
        protected System.Web.UI.WebControls.RegularExpressionValidator revRANK;
        protected System.Web.UI.WebControls.Label capEFFECTIVE_FROM_DATE;
        protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_FROM_DATE;
        protected System.Web.UI.WebControls.Label capEFFECTIVE_TO_DATE;
        protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_TO_DATE;
        protected System.Web.UI.WebControls.Label capDISABLED_DATE;
        protected System.Web.UI.WebControls.TextBox txtDISABLED_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_FROM_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_TO_DATE;
        //protected System.Web.UI.HtmlControls.HtmlTableRow trType;
        protected System.Web.UI.WebControls.HyperLink hlkDISABLED_DATE;
        protected System.Web.UI.WebControls.CompareValidator cpvEFFECTIVE_TO_DATE;
        protected System.Web.UI.WebControls.CompareValidator cpvDISABLED_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_FROM_DATE;
        protected System.Web.UI.WebControls.CustomValidator csvEFFECTIVE_TO_DATE;
        protected System.Web.UI.WebControls.CustomValidator csvDISABLED_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_FROM_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_TO_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDISABLED_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidISLIMITAPPLICABLE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOVERAGE_TYPE;
        protected System.Web.UI.WebControls.Label lblMsg;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
        protected System.Web.UI.WebControls.Label capAddDeduct_Applicable;
        protected System.Web.UI.WebControls.Label capAddDeductibleType;
        protected System.Web.UI.WebControls.Label lblAddDeductibleType;
        protected System.Web.UI.WebControls.DropDownList cmbADDDEDUCTIBLE_TYPE;
        protected System.Web.UI.WebControls.Label capDeduct_Applicable;
        protected System.Web.UI.WebControls.CheckBox chkIsDeductApplicable;
        protected System.Web.UI.WebControls.Label capDeductibleType;
        protected System.Web.UI.WebControls.Label lblDeductibleType;
        protected System.Web.UI.WebControls.DropDownList cmbDEDUCTIBLE_TYPE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvADDDEDUCTIBLE_TYPE;
        protected System.Web.UI.HtmlControls.HtmlTableRow trADDAPPLICABLE;
        protected System.Web.UI.WebControls.CheckBox chkISADDDEDUCTIBLE_APP;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidISADDDEDUCTIBLE_APP;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidISDEDUCTIBELEAPPLICABLE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDrawLimit;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDrawDeductible;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDrawAddDeductible;
        protected System.Web.UI.WebControls.RegularExpressionValidator revMANDATORY_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revNON_MANDATORY_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDEFAULT_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revNON_DEFAULT_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMANDATORY_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNON_MANDATORY_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEFAULT_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNON_DEFAULT_DATE;
        protected System.Web.UI.WebControls.Label capDISPLAYONCLAIM;
        protected System.Web.UI.WebControls.Label capCLAIMRESERVEAPPLY;
        protected System.Web.UI.WebControls.DropDownList cmbDISPLAYONCLAIM;
        protected System.Web.UI.WebControls.DropDownList cmbCLAIMRESERVEAPPLY;
        protected System.Web.UI.WebControls.DropDownList cmbSUB_LOB_ID;
        protected System.Web.UI.WebControls.Label capSUB_LOB_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUB_LOB_ID;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSUB_LOB_ID;
        protected System.Web.UI.WebControls.Label CapAddLookup;
        protected System.Web.UI.WebControls.Label CapRein;
        protected System.Web.UI.WebControls.Label CapComm;
        protected const string TypeFlat = "1";
        protected const string TypeSplit = "2";
        protected System.Web.UI.WebControls.Label capREINSURANCE;
        protected System.Web.UI.WebControls.DropDownList cmbREINSURANCE;
        protected System.Web.UI.WebControls.Label capReinsuranceCov;
        protected System.Web.UI.WebControls.DropDownList cmbReinsuranceCov;
        protected System.Web.UI.WebControls.Label capASLOB;
        protected System.Web.UI.WebControls.DropDownList cmbASLOB;
        protected System.Web.UI.WebControls.Label capReinsuranceCalc;
        protected System.Web.UI.WebControls.DropDownList cmbReinsuranceCalc;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOV_IDNew;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOV_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
        protected System.Web.UI.WebControls.Label capFORM_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtFORM_NUMBER;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_REFRESH;
        protected System.Web.UI.WebControls.Label capRptBkt;
        protected System.Web.UI.WebControls.DropDownList cmbReinRptBkt;
        protected System.Web.UI.WebControls.DropDownList cmbREIN_REPORT_BUCK;

        protected System.Web.UI.WebControls.Label capREIN_REPORT_BUCK_COMM;
        protected System.Web.UI.WebControls.DropDownList cmbREIN_REPORT_BUCK_COMM;

        protected System.Web.UI.WebControls.Label capCOMM_VEHICLE;
        protected System.Web.UI.WebControls.DropDownList cmbCOMM_VEHICLE;
        protected System.Web.UI.WebControls.Label capCOMM_REIN_COV_CAT;
        protected System.Web.UI.WebControls.DropDownList cmbCOMM_REIN_COV_CAT;
        protected System.Web.UI.WebControls.Label capREIN_ASLOB;
        protected System.Web.UI.WebControls.DropDownList cmbREIN_ASLOB;
        protected System.Web.UI.WebControls.Label capCOMM_CALC;
        protected System.Web.UI.WebControls.DropDownList cmbCOMM_CALC;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMM_REIN_COV_CAT;
        protected System.Web.UI.WebControls.Label capMANDATORY_DATE;
        protected System.Web.UI.WebControls.Label capNON_MANDATORY_DATE;
        protected System.Web.UI.WebControls.Label capDEFAULT_DATE;
        protected System.Web.UI.WebControls.Label capNON_DEFAULT_DATE;
        protected System.Web.UI.WebControls.TextBox txtMANDATORY_DATE;
        protected System.Web.UI.WebControls.TextBox txtNON_MANDATORY_DATE;
        protected System.Web.UI.WebControls.TextBox txtDEFAULT_DATE;
        protected System.Web.UI.WebControls.TextBox txtNON_DEFAULT_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkMANDATORY_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkDEFAULT_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkNON_MANDATORY_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkNON_DEFAULT_DATE;
        protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
        //Ankit Is Main drop down added
        protected System.Web.UI.WebControls.CheckBox chkIS_MAIN;
        protected System.Web.UI.WebControls.Label capISMAIN;
        protected System.Web.UI.WebControls.Label capMANDATORY;
        protected System.Web.UI.HtmlControls.HtmlInputHidden HidAPPDEDUCT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden HidDeductApplicable;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidMess;
        //Shikha - itrack - 1226
        protected System.Web.UI.WebControls.Label capCOV_TYPE_ABBR;
        protected System.Web.UI.WebControls.Label capSUSEP_COV_CODE;
        protected System.Web.UI.WebControls.TextBox txtCOV_TYPE_ABBR;
        protected System.Web.UI.WebControls.TextBox txtSUSEP_COV_CODE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revSUSEP_COV_CODE;
        protected System.Web.UI.WebControls.CustomValidator csvMANDATORY_DATE;
        protected System.Web.UI.WebControls.CustomValidator csvNON_MANDATORY_DATE;
        protected System.Web.UI.WebControls.CustomValidator csvDEFAULT_DATE;
        protected System.Web.UI.WebControls.CustomValidator csvNON_DEFAULT_DATE;
        protected System.Web.UI.WebControls.CustomValidator csvDISABLED_DATE1;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidtab1;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidtab2;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidtab3;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidtab4;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidtab5;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidtab6;

        public String StrAll;
//        ClsStates objClsStates;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_ASLOB;

        ClsCoverageDetails objAddCoverageDetails;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE;
        protected const int REINSURANCE_COVERAGE = 3;
        /// <summary>
        /// Method to set validation control error masessages.
        /// Parameters: none
        /// Return Type: none
        /// </summary>

        private void Page_Load(object sender, System.EventArgs e)
        {
            //Sumit:15-02-2006:Save button disabled
            //btnSave.Enabled=false; // Commented By Ravindra (05-08-2006)
            //btnReset.Attributes.Add("onclick","javascript:return ResetForm('MNT_COVERAGE');");

            Ajax.Utility.RegisterTypeForAjax(typeof(AddCoverageDetails));
            btnActivateDeactivate.Visible = false;  //Ravindra(05-08-2006)

            btnReset.Attributes.Add("onclick", "javascript:return ResetScreen();");
            chkIsLimitApplicable.Attributes.Add("onclick", "javascript:return LimitCheck();");
            chkIsDeductApplicable.Attributes.Add("onclick", "javascript:return DeductCheck();");
            //			btnSave.Attributes.Add("onclick","javascript:return SaveScreen();"); 

           
            hlkEFFECTIVE_FROM_DATE.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtEFFECTIVE_FROM_DATE'), document.getElementById('txtEFFECTIVE_FROM_DATE'))");
            hlkEFFECTIVE_TO_DATE.Attributes.Add("OnClick", "fPopCalendar(document.MNT_COVERAGE.txtEFFECTIVE_TO_DATE, document.MNT_COVERAGE.txtEFFECTIVE_TO_DATE)");
            hlkDISABLED_DATE.Attributes.Add("OnClick", "fPopCalendar(document.MNT_COVERAGE.txtDISABLED_DATE, document.MNT_COVERAGE.txtDISABLED_DATE)");
            hlkMANDATORY_DATE.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtMANDATORY_DATE'), document.getElementById('txtMANDATORY_DATE'))");
            hlkNON_MANDATORY_DATE.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtNON_MANDATORY_DATE'), document.getElementById('txtNON_MANDATORY_DATE'))");
            hlkDEFAULT_DATE.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtDEFAULT_DATE'), document.getElementById('txtDEFAULT_DATE'))");
            hlkNON_DEFAULT_DATE.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtNON_DEFAULT_DATE'), document.getElementById('txtNON_DEFAULT_DATE'))");
            //hidLOB_ID.Value = GetLOBID().ToString();
            //base.ScreenId = "198_0";
            base.ScreenId = "492_0";
            lblMessage.Visible = false;
            cmbDEDUCTIBLE_TYPE.Enabled = false;
            cmbLIMIT_TYPE.Enabled = false;
            
            //START:*********** Setting permissions and class (Read/write/execute/delete)  

            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  

            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddCoverageDetails", System.Reflection.Assembly.GetExecutingAssembly());
            //GetHiddenValues();

            if (!Page.IsPostBack)
            {
                SetHiddenFields();
                SetCaptions();
                bindData();
                SetValidators();
                FillCombo();
                //LoadCombo();
                hidMess.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");
                ClsCoverageDetails objCoverageDetails = new ClsCoverageDetails();
                //txtRANK.Text = objCoverageDetails.GetNewOrderNo();
                GetOldDataXML();

                int stateid = -1;
                string lobid = "";

                String sysId = GetSystemId().ToString();

                if (dsCoverageDetails.Tables[0].Rows.Count > 0)
                {
                    hidISLIMITAPPLICABLE.Value = dsCoverageDetails.Tables[0].Rows[0]["IsLimitApplicable"].ToString();
                    hidISDEDUCTIBELEAPPLICABLE.Value = dsCoverageDetails.Tables[0].Rows[0]["IsDeductApplicable"].ToString();
                    hidISADDDEDUCTIBLE_APP.Value = dsCoverageDetails.Tables[0].Rows[0]["ISADDDEDUCTIBLE_APP"].ToString();
                    hidSUB_LOB_ID.Value = dsCoverageDetails.Tables[0].Rows[0]["SUB_LOB_ID"].ToString();
                    stateid = int.Parse(dsCoverageDetails.Tables[0].Rows[0]["STATE_ID"].ToString());
                    lobid = dsCoverageDetails.Tables[0].Rows[0]["LOB_ID"].ToString();
                    //txtRANK.Text = objCoverageDetails.GetNewOrderNo(stateid, lobid);
                    cmbSTATE_ID.SelectedValue = stateid.ToString();
                    DataSet ds = new DataSet();
                    ds = Cms.BusinessLayer.BlApplication.clsapplication.GetLobByStateId(stateid);
                    cmbLOB_ID.DataSource = ds.Tables[0].DefaultView;
                    cmbLOB_ID.DataValueField = "LOB_ID";
                    cmbLOB_ID.DataTextField = "LOB_DESC";
                    cmbLOB_ID.DataBind();
                    cmbLOB_ID.Items.Insert(0, "");
                    if (dsCoverageDetails.Tables[0].Rows[0]["LIMIT_TYPE"]!=null && dsCoverageDetails.Tables[0].Rows[0]["LIMIT_TYPE"].ToString().Trim() != "")
                    {
                       
                            int limitType = int.Parse(dsCoverageDetails.Tables[0].Rows[0]["LIMIT_TYPE"].ToString());
                            hidLimitTypeValue.Value = limitType.ToString();
                        

                    }
                    if (dsCoverageDetails.Tables[0].Rows[0]["DEDUCTIBLE_TYPE"]!=null && dsCoverageDetails.Tables[0].Rows[0]["DEDUCTIBLE_TYPE"].ToString().Trim() != "")
                    {
                       
                            int DeductType = int.Parse(dsCoverageDetails.Tables[0].Rows[0]["DEDUCTIBLE_TYPE"].ToString());
                            hidDeductTypeValue.Value = DeductType.ToString();
                       
                    }

                   

                }

                //Modified by Ruchika on 10-Jan-2012 for TFS bug # 836
                if ((sysId.ToUpper() == "S001") || (sysId.ToUpper() == "SUAT"))
                {
                    
                    DataTable dtProd = Cms.CmsWeb.ClsFetcher.LOBs;
                    cmbLOB_ID.DataSource = dtProd;
                    cmbLOB_ID.DataTextField = "LOB_DESC";
                    cmbLOB_ID.DataValueField = "LOB_ID";
                    SetCultureThread(GetLanguageCode());
                    cmbLOB_ID.DataBind();
                    cmbLOB_ID.Items.Insert(0, new ListItem("All", "0"));   
                }
               
                //Added by Ruchika Chauhan for TFS # 846 on 27-Dec-2011
                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/support/PageXML/" + GetSystemId(), "AddCoverageDetails.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "CmsWeb/support/PageXML/" + GetSystemId() + "/AddCoverageDetails.xml");
                }

                LoadData();
                lblLIMITTYPE.Visible = false;
                lblDeductibleType.Visible = false;

                //Added by Ruchika on 11-Jan-2012 for TFS Bug #836
                if ((GetSystemId().ToString().ToUpper() == "S001") || (GetSystemId().ToString().ToUpper() == "SUAT"))
                {
                    imgSelect.Disabled = true;
                    imgSelect.Visible = false;
                }

            }

            if (hidCOV_ID.Value.ToUpper().Trim() == "NEW" || hidCOV_ID.Value.ToUpper().Trim() == "0" || hidOldData.Value == "")
            {
                if (imgSelect.Attributes["onclick"] != null)
                    imgSelect.Attributes.Remove("onclick");

                imgSelect.Attributes.Add("onclick", "javascript:ShowMultiLingualPopup('COV_ID','" + 0 + "','MNT_COVERAGE','MNT_COVERAGE_MULTILINGUAL','COV_DES');return false;");
            }
            else
            {
                if (imgSelect.Attributes["onclick"] != null)
                    imgSelect.Attributes.Remove("onclick");

                imgSelect.Attributes.Add("onclick", "javascript:ShowMultiLingualPopup('COV_ID','" + hidCOV_ID.Value + "','MNT_COVERAGE','MNT_COVERAGE_MULTILINGUAL','COV_DES');return false;");
            }
            //			LoadData();
        }
        private void FillCombo()
        {
            DataTable dt = Cms.CmsWeb.ClsFetcher.ActiveState;
            cmbSTATE_ID.DataSource = dt;
            cmbSTATE_ID.DataTextField = "State_Name";
            cmbSTATE_ID.DataValueField = "State_Id";
            cmbSTATE_ID.DataBind();
            cmbSTATE_ID.Items.Insert(0, "");
            cmbSTATE_ID.Items.Insert(1, new ListItem(StrAll, "0"));

            cmbCOVERAGE_TYPE.DataSource = ClsCommon.GetLookup("COVTP");
            cmbCOVERAGE_TYPE.DataTextField = "LookupDesc";
            cmbCOVERAGE_TYPE.DataValueField = "LookupCode";
            cmbCOVERAGE_TYPE.DataBind();
            cmbCOVERAGE_TYPE.Items.Insert(0, "");

            cmbIS_MANDATORY.DataSource = ClsCommon.GetLookup("YESNO");
            cmbIS_MANDATORY.DataTextField = "LookupDesc";
            cmbIS_MANDATORY.DataValueField = "LookupCode";
            cmbIS_MANDATORY.DataBind();
            cmbIS_MANDATORY.Items.Insert(0, "");

            cmbIS_DEFAULT.DataSource = ClsCommon.GetLookup("YESNO");
            cmbIS_DEFAULT.DataTextField = "LookupDesc";
            cmbIS_DEFAULT.DataValueField = "LookupCode";
            cmbIS_DEFAULT.DataBind();
            cmbIS_DEFAULT.Items.Insert(0, "");

            cmbPURPOSE.DataSource = ClsCommon.GetLookup("COVPPS");
            cmbPURPOSE.DataTextField = "LookupDesc";
            cmbPURPOSE.DataValueField = "LookupCode";
            cmbPURPOSE.DataBind();
            cmbPURPOSE.Items.Insert(0, "");

            cmbLIMIT_TYPE.DataSource = ClsCommon.GetLookup("LMTYP");
            cmbLIMIT_TYPE.DataTextField = "LookupDesc";
            cmbLIMIT_TYPE.DataValueField = "LookupCode";
            cmbLIMIT_TYPE.DataBind();
            cmbLIMIT_TYPE.Items.Insert(0, "");

            cmbDEDUCTIBLE_TYPE.DataSource = ClsCommon.GetLookup("DEDT");
            cmbDEDUCTIBLE_TYPE.DataTextField = "LookupDesc";
            cmbDEDUCTIBLE_TYPE.DataValueField = "LookupCode";
            cmbDEDUCTIBLE_TYPE.DataBind();
            cmbDEDUCTIBLE_TYPE.Items.Insert(0, "");



            cmbREINSURANCE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            cmbREINSURANCE.DataTextField = "LookupDesc";
            cmbREINSURANCE.DataValueField = "LookupID";
            cmbREINSURANCE.DataBind();
            cmbREINSURANCE.Items.Insert(0, "");
            //cmbREINSURANCE.SelectedIndex = 2;

            //cmbReinsuranceCov.DataSource=Cms.CmsWeb.ClsFetcher.ReinsuranceCoverageCode;
            //			cmbReinsuranceCov.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RCC");
            cmbReinsuranceCov.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RRB");
            cmbReinsuranceCov.DataTextField = "LookupDesc";
            cmbReinsuranceCov.DataValueField = "LookupID";
            cmbReinsuranceCov.DataBind();
            cmbReinsuranceCov.Items.Insert(0, "");


            cmbASLOB.DataSource = Cms.CmsWeb.ClsFetcher.ASLOB;
            cmbASLOB.DataTextField = "LOOKUP_VALUE_DESC";
            cmbASLOB.DataValueField = "LOOKUP_UNIQUE_ID";
            cmbASLOB.DataBind();
            cmbASLOB.Items.Insert(0, "");


            cmbREIN_REPORT_BUCK.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RRB");
            cmbREIN_REPORT_BUCK.DataTextField = "LookupDesc";
            cmbREIN_REPORT_BUCK.DataValueField = "LookupID";
            cmbREIN_REPORT_BUCK.DataBind();
            cmbREIN_REPORT_BUCK.Items.Insert(0, "");

            cmbREIN_REPORT_BUCK_COMM.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RRB");
            cmbREIN_REPORT_BUCK_COMM.DataTextField = "LookupDesc";
            cmbREIN_REPORT_BUCK_COMM.DataValueField = "LookupID";
            cmbREIN_REPORT_BUCK_COMM.DataBind();
            cmbREIN_REPORT_BUCK_COMM.Items.Insert(0, "");


            cmbReinsuranceCalc.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RC"); ;
            cmbReinsuranceCalc.DataTextField = "LookupDesc";
            cmbReinsuranceCalc.DataValueField = "LookupID";
            cmbReinsuranceCalc.DataBind();
            cmbReinsuranceCalc.Items.Insert(0, "");


            cmbCOMM_VEHICLE.DataSource = ClsCommon.GetLookup("YESNO");
            cmbCOMM_VEHICLE.DataTextField = "LookupDesc";
            cmbCOMM_VEHICLE.DataValueField = "LookupID";
            cmbCOMM_VEHICLE.DataBind();
            cmbCOMM_VEHICLE.Items.Insert(0, "");

            cmbCOMM_REIN_COV_CAT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RCCC"); ;
            cmbCOMM_REIN_COV_CAT.DataTextField = "LookupDesc";
            cmbCOMM_REIN_COV_CAT.DataValueField = "LookupID";
            cmbCOMM_REIN_COV_CAT.DataBind();
            cmbCOMM_REIN_COV_CAT.Items.Insert(0, "");


            cmbREIN_ASLOB.DataSource = Cms.CmsWeb.ClsFetcher.ASLOB;
            cmbREIN_ASLOB.DataTextField = "LOOKUP_VALUE_DESC";
            cmbREIN_ASLOB.DataValueField = "LOOKUP_UNIQUE_ID";
            cmbREIN_ASLOB.DataBind();
            cmbREIN_ASLOB.Items.Insert(0, "");


            cmbCOMM_CALC.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RC"); ;
            cmbCOMM_CALC.DataTextField = "LookupDesc";
            cmbCOMM_CALC.DataValueField = "LookupID";
            cmbCOMM_CALC.DataBind();
            cmbCOMM_CALC.Items.Insert(0, "");

            //-----------Added by praveen kumar 19/08/2010 starts---------
            cmbDISPLAYONCLAIM.DataSource = ClsCommon.GetLookup("YESNO");
            cmbDISPLAYONCLAIM.DataTextField = "LookupDesc";
            cmbDISPLAYONCLAIM.DataValueField = "LookupID";
            cmbDISPLAYONCLAIM.DataBind();
            cmbDISPLAYONCLAIM.Items.Insert(0, "");

            cmbCLAIMRESERVEAPPLY.DataSource = ClsCommon.GetLookup("YESNO");
            cmbCLAIMRESERVEAPPLY.DataTextField = "LookupDesc";
            cmbCLAIMRESERVEAPPLY.DataValueField = "LookupID";
            cmbCLAIMRESERVEAPPLY.DataBind();
            cmbCLAIMRESERVEAPPLY.Items.Insert(0, "");

           


            //-----------Added by praveen kumar 19/08/2010 Ends---------

            //ANKIT
            //cmbIS_MAIN.DataSource = ClsCommon.GetLookup("YESNO");
            //cmbIS_MAIN.DataTextField = "LookupDesc";
            //cmbIS_MAIN.DataValueField = "LookupID";
            //cmbIS_MAIN.DataBind();
            //cmbIS_MAIN.Items.Insert(0, "");

        }
        private void BindSUBLOBs(int iLOB_ID,int SelectedValues)
        {
            cmbSUB_LOB_ID.DataSource = ClsEndorsmentDetails.GetSUBLOBs(iLOB_ID.ToString(),GetLanguageID()).Tables[0];
            cmbSUB_LOB_ID.DataTextField = "SUB_LOB_DESC";
            cmbSUB_LOB_ID.DataValueField = "SUB_LOB_ID";
            cmbSUB_LOB_ID.DataBind();
            cmbSUB_LOB_ID.Items.Insert(0, "");
            cmbSUB_LOB_ID.SelectedValue = SelectedValues.ToString();
        }

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
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void LoadData()
        {
            if (dsCoverageDetails.Tables[0].Rows.Count > 0)
            {
                hidLOB_ID.Value = dsCoverageDetails.Tables[0].Rows[0]["LOB_ID"].ToString();
                hidSUB_LOB_ID.Value = dsCoverageDetails.Tables[0].Rows[0]["SUB_LOB_ID"].ToString();
                //if (hidLOB_ID.Value != "" && hidSUB_LOB_ID.Value != "")
                //    this.BindSUBLOBs(int.Parse(hidLOB_ID.Value), int.Parse(hidSUB_LOB_ID.Value));
                txtCOV_CODE.Text = dsCoverageDetails.Tables[0].Rows[0]["COV_CODE"].ToString();
                txtCOV_DES.Text = dsCoverageDetails.Tables[0].Rows[0]["COV_DES"].ToString();
                txtCOV_REF_CODE.Text = dsCoverageDetails.Tables[0].Rows[0]["COV_REF_CODE"].ToString();
                if(dsCoverageDetails.Tables[0].Rows[0]["DISABLED_DATE"].ToString() != "")
                txtDISABLED_DATE.Text = ConvertDBDateToCulture(dsCoverageDetails.Tables[0].Rows[0]["DISABLED_DATE"].ToString());
                if (dsCoverageDetails.Tables[0].Rows[0]["EFFECTIVE_FROM_DATE"].ToString() != "")
                txtEFFECTIVE_FROM_DATE.Text = ConvertDBDateToCulture(dsCoverageDetails.Tables[0].Rows[0]["EFFECTIVE_FROM_DATE"].ToString());
                if (dsCoverageDetails.Tables[0].Rows[0]["EFFECTIVE_TO_DATE"].ToString() != "")
                txtEFFECTIVE_TO_DATE.Text = ConvertDBDateToCulture(dsCoverageDetails.Tables[0].Rows[0]["EFFECTIVE_TO_DATE"].ToString());
                txtINCLUDED.Text = dsCoverageDetails.Tables[0].Rows[0]["INCLUDED"].ToString();
                txtRANK.Text = dsCoverageDetails.Tables[0].Rows[0]["RANK"].ToString().Trim();
                txtFORM_NUMBER.Text = dsCoverageDetails.Tables[0].Rows[0]["FORM_NUMBER"].ToString().Trim();
                ClsCommon.SelectValueinDDL(cmbCOVERAGE_TYPE, dsCoverageDetails.Tables[0].Rows[0]["COVERAGE_TYPE"].ToString().Trim());
                string isdefault;
                if (dsCoverageDetails.Tables[0].Rows[0]["IS_DEFAULT"].ToString().ToUpper() == "TRUE")
                {
                    isdefault = "1";
                }

                else
                {
                    isdefault = "0";
                }


                ClsCommon.SelectValueinDDL(cmbIS_DEFAULT, isdefault);
                ClsCommon.SelectValueinDDL(cmbIS_MANDATORY, dsCoverageDetails.Tables[0].Rows[0]["IS_MANDATORY"].ToString().Trim());

                ClsCommon.SelectValueinDDL(cmbLOB_ID, dsCoverageDetails.Tables[0].Rows[0]["LOB_ID"].ToString().Trim());
                //ClsCommon.SelectValueinDDL(cmbSUB_LOB_ID, dsCoverageDetails.Tables[0].Rows[0]["SUB_LOB_ID"].ToString().Trim());
                ClsCommon.SelectValueinDDL(cmbPURPOSE, dsCoverageDetails.Tables[0].Rows[0]["PURPOSE"].ToString().Trim());
                ClsCommon.SelectValueinDDL(cmbSTATE_ID, dsCoverageDetails.Tables[0].Rows[0]["STATE_ID"].ToString().Trim());
                ClsCommon.SelectValueinDDL(cmbTYPE, dsCoverageDetails.Tables[0].Rows[0]["TYPE"].ToString().Trim());

                // Added by swarup
                ClsCommon.SelectValueinDDL(cmbREINSURANCE, dsCoverageDetails.Tables[0].Rows[0]["REINSURANCE_LOB"].ToString().Trim());
                ClsCommon.SelectValueinDDL(cmbReinsuranceCov, dsCoverageDetails.Tables[0].Rows[0]["REINSURANCE_COV"].ToString().Trim());
                ClsCommon.SelectValueinDDL(cmbASLOB, dsCoverageDetails.Tables[0].Rows[0]["ASLOB"].ToString().Trim());
                ClsCommon.SelectValueinDDL(cmbReinsuranceCalc, dsCoverageDetails.Tables[0].Rows[0]["REINSURANCE_CALC"].ToString().Trim());

                ClsCommon.SelectValueinDDL(cmbCOMM_VEHICLE, dsCoverageDetails.Tables[0].Rows[0]["COMM_VEHICLE"].ToString().Trim());
                ClsCommon.SelectValueinDDL(cmbCOMM_REIN_COV_CAT, dsCoverageDetails.Tables[0].Rows[0]["COMM_REIN_COV_CAT"].ToString().Trim());
                ClsCommon.SelectValueinDDL(cmbREIN_ASLOB, dsCoverageDetails.Tables[0].Rows[0]["REIN_ASLOB"].ToString().Trim());
                ClsCommon.SelectValueinDDL(cmbCOMM_CALC, dsCoverageDetails.Tables[0].Rows[0]["COMM_CALC"].ToString().Trim());

                //Added by Praveen Kumar on 19/08/2010
                ClsCommon.SelectValueinDDL(cmbDISPLAYONCLAIM, dsCoverageDetails.Tables[0].Rows[0]["DISPLAY_ON_CLAIM"].ToString().Trim());
                ClsCommon.SelectValueinDDL(cmbCLAIMRESERVEAPPLY, dsCoverageDetails.Tables[0].Rows[0]["CLAIM_RESERVE_APPLY"].ToString().Trim());


                //				if (hidTYPE.Value==REINSURANCE_COVERAGE.ToString()) 
                //				{
                ClsCommon.SelectValueinDDL(cmbREIN_REPORT_BUCK, dsCoverageDetails.Tables[0].Rows[0]["REIN_REPORT_BUCK"].ToString().Trim());
                ClsCommon.SelectValueinDDL(cmbREIN_REPORT_BUCK_COMM, dsCoverageDetails.Tables[0].Rows[0]["REIN_REPORT_BUCK_COMM"].ToString().Trim());
                //				}

                if (dsCoverageDetails.Tables[0].Rows[0]["IsLimitApplicable"].ToString() == "Y" || dsCoverageDetails.Tables[0].Rows[0]["IsLimitApplicable"].ToString() == "1")
                {
                    chkIsLimitApplicable.Checked = true;
                    ClsCommon.SelectValueinDDL(cmbLIMIT_TYPE, dsCoverageDetails.Tables[0].Rows[0]["LIMIT_TYPE"].ToString().Trim());
                    if (dsCoverageDetails.Tables[0].Rows[0]["LIMIT_TYPE"].ToString().Trim() == TypeFlat.ToString() || dsCoverageDetails.Tables[0].Rows[0]["LIMIT_TYPE"].ToString().Trim() == TypeSplit.ToString())
                        hidDrawLimit.Value = "1";
                }
                else
                    cmbLIMIT_TYPE.SelectedIndex = 0;
                if (dsCoverageDetails.Tables[0].Rows[0]["IsDeductApplicable"].ToString() == "Y" || dsCoverageDetails.Tables[0].Rows[0]["IsDeductApplicable"].ToString() == "1")
                {
                    chkIsDeductApplicable.Checked = true;
                    ClsCommon.SelectValueinDDL(cmbDEDUCTIBLE_TYPE, dsCoverageDetails.Tables[0].Rows[0]["DEDUCTIBLE_TYPE"].ToString().Trim());
                    if (dsCoverageDetails.Tables[0].Rows[0]["DEDUCTIBLE_TYPE"].ToString().Trim() == TypeFlat.ToString() || dsCoverageDetails.Tables[0].Rows[0]["DEDUCTIBLE_TYPE"].ToString().Trim() == TypeSplit.ToString())
                        hidDrawDeductible.Value = "1";
                }
                else
                    cmbDEDUCTIBLE_TYPE.SelectedIndex = 0;
                if (dsCoverageDetails.Tables[0].Rows[0]["ISADDDEDUCTIBLE_APP"].ToString() == "Y" || dsCoverageDetails.Tables[0].Rows[0]["ISADDDEDUCTIBLE_APP"].ToString() == "1")
                {
                    chkISADDDEDUCTIBLE_APP.Checked = true;
                    ClsCommon.SelectValueinDDL(cmbADDDEDUCTIBLE_TYPE, dsCoverageDetails.Tables[0].Rows[0]["IsDeductApplicable"].ToString().Trim());
                    if (dsCoverageDetails.Tables[0].Rows[0]["ISADDDEDUCTIBLE_APP"].ToString().Trim() == TypeFlat.ToString() || dsCoverageDetails.Tables[0].Rows[0]["ISADDDEDUCTIBLE_APP"].ToString().Trim() == TypeSplit.ToString())

                        //					ClsCommon.SelectValueinDDL(cmbADDDEDUCTIBLE_TYPE,dsCoverageDetails.Tables[0].Rows[0]["ADDDEDUCTIBLE_TYPE"].ToString().Trim());   
                        //					if (dsCoverageDetails.Tables[0].Rows[0]["ADDDEDUCTIBLE_TYPE"].ToString().Trim()==TypeFlat.ToString() || dsCoverageDetails.Tables[0].Rows[0]["ADDDEDUCTIBLE_TYPE"].ToString().Trim()== TypeSplit.ToString())
                        hidDrawAddDeductible.Value = "1";
                }
                else
                    cmbADDDEDUCTIBLE_TYPE.SelectedIndex = 0;
                if (dsCoverageDetails.Tables[0].Rows[0]["MANDATORY_DATE"].ToString() != "")
                txtMANDATORY_DATE.Text = ConvertDBDateToCulture(dsCoverageDetails.Tables[0].Rows[0]["MANDATORY_DATE"].ToString());
                if (dsCoverageDetails.Tables[0].Rows[0]["NON_MANDATORY_DATE"].ToString() != "")
                txtNON_MANDATORY_DATE.Text = ConvertDBDateToCulture(dsCoverageDetails.Tables[0].Rows[0]["NON_MANDATORY_DATE"].ToString());
                if (dsCoverageDetails.Tables[0].Rows[0]["DEFAULT_DATE"].ToString() != "")
                txtDEFAULT_DATE.Text = ConvertDBDateToCulture(dsCoverageDetails.Tables[0].Rows[0]["DEFAULT_DATE"].ToString());
                if (dsCoverageDetails.Tables[0].Rows[0]["NON_DEFAULT_DATE"].ToString() != "")
                txtNON_DEFAULT_DATE.Text =ConvertDBDateToCulture( dsCoverageDetails.Tables[0].Rows[0]["NON_DEFAULT_DATE"].ToString());

                //aNKIT

                if (dsCoverageDetails.Tables[0].Rows[0]["IS_MAIN"].ToString() == "1")
                {
                    chkIS_MAIN.Checked = true;
                }
                else
                {
                    chkIS_MAIN.Checked = false;
                }

                //Shikha itrack - 1226

                txtSUSEP_COV_CODE.Text = dsCoverageDetails.Tables[0].Rows[0]["SUSEP_COV_CODE"].ToString();
                txtCOV_TYPE_ABBR.Text = dsCoverageDetails.Tables[0].Rows[0]["COV_TYPE_ABBR"].ToString();

            }


        }
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFetchInfo(int iLOB_ID)
        {
            DataSet ds = null;
            ds = new DataSet();
            try
            {


                DataTable dt1 = new DataTable();
                try
                {
                    return ClsEndorsmentDetails.GetSUBLOBs(iLOB_ID.ToString(),GetLanguageID());//.Tables[0];
                }
                catch { return null; }
                //{ }
                //ds.Tables.Add(dt1.Clone());
                //ds.Tables[0].TableName = "SUBLOBS";
                //return ds;
            }

            //    DataTable dt2 = new DataTable();
            //    try
            //    {
            //        dt2 = ClsGeneralInformation.GetBillType(iLOB_ID, "POL");
            //    }
            //    catch
            //    { }
            //    ds.Tables.Add(dt2.Copy());
            //    ds.Tables[1].TableName = "BILLTYPE";

            //    DataTable dt3 = new DataTable();
            //    try
            //    {
            //        dt3 = ClsGeneralInformation.GetLOBTerms(iLOB_ID).Tables[0];
            //    }

            //    catch
            //    { }
            //    ds.Tables.Add(dt3.Copy());
            //    ds.Tables[2].TableName = "APPTERMS";
            //    DataTable dt4 = new DataTable();
            //    try
            //    {
            //        dt4 = objClsStates.PopLateAssignedState(iLOB_ID).Tables[0];
            //    }

            //    catch
            //    {

            //    }
            //    ds.Tables.Add(dt4.Copy());
            //    ds.Tables[3].TableName = "STATE";

            //    return ds;
            //}
            catch
            {
                return null;
            }
        }
        public void GetHiddenValues()
        {
            dsCoverageDetails = ClsCoverageDetails.GetCoverageDetail(int.Parse(hidCOV_ID.Value), hidTYPE.Value);
            if (dsCoverageDetails.Tables[0].Rows.Count == 0)
            {
                hidOldData.Value = "";
            }
            else
            {
                //LoadData();
                hidISLIMITAPPLICABLE.Value = dsCoverageDetails.Tables[0].Rows[0]["IsLimitApplicable"].ToString();
                hidISDEDUCTIBELEAPPLICABLE.Value = dsCoverageDetails.Tables[0].Rows[0]["IsDeductApplicable"].ToString();
                hidISADDDEDUCTIBLE_APP.Value = dsCoverageDetails.Tables[0].Rows[0]["ISADDDEDUCTIBLE_APP"].ToString();
                hidSUB_LOB_ID.Value = dsCoverageDetails.Tables[0].Rows[0]["SUB_LOB_ID"].ToString();
                hidOldData.Value = dsCoverageDetails.GetXml();
                hidMode.Value = "Edit";
            }

        }

        private void GetOldDataXML()
        {
            GetHiddenValues();
            if (hidOldData.Value.Trim() != "")
            {
                cmbCOVERAGE_TYPE.Enabled = false;
                cmbTYPE.Enabled = false;
                cmbDEDUCTIBLE_TYPE.Enabled = false;
                cmbADDDEDUCTIBLE_TYPE.Enabled = false;
                cmbIS_DEFAULT.Enabled = true;
                cmbIS_MANDATORY.Enabled = true;
                //txtCOV_DES.Enabled = true;
                cmbLIMIT_TYPE.Enabled = false;
                cmbLOB_ID.Enabled = false;
                cmbPURPOSE.Enabled = false;
                cmbSTATE_ID.Enabled = false;
               // cmbSUB_LOB_ID.Enabled = false;
                //ankit
                chkIS_MAIN.Enabled = true;
                //cmbREINSURANCE.Enabled=false;
                chkIsDeductApplicable.Enabled = false;
                chkIsLimitApplicable.Enabled = false;
                chkISADDDEDUCTIBLE_APP.Enabled = false;
                txtCOV_CODE.Attributes.Add("readOnly", "true");
                //txtCOV_DES.Attributes.Add("readOnly","false");
                txtCOV_REF_CODE.Attributes.Add("readOnly", "true");
                txtINCLUDED.Attributes.Add("readOnly", "true");
                //txtRANK.Attributes.Add("readOnly","true");

            }


        }
        private void SetHiddenFields()
        {
            if (Request.QueryString["COV_ID"] != null && Request.QueryString["COV_ID"].ToString() != "")
            {
                hidCOV_ID.Value = Request.QueryString["COV_ID"].ToString();
                string[] covTwo = hidCOV_ID.Value.Split(',');
                if (covTwo.Length > 1)
                {
                    hidCOV_ID.Value = covTwo[1];
                    //					hidCOV_IDNew.Value= covTwo[0];
                }
                if (Request.QueryString["LobDesc"] == null)
                    hidCOV_IDNew.Value = hidCOV_ID.Value;
                if (hidCalledFrom.Value == "Index")
                    hidCOV_IDNew.Value = "0";
                if (Request.QueryString["TYPE"] != null)
                    hidTYPE.Value = Request.QueryString["TYPE"].ToString();
                hidENDORSEMENT_ID.Value = ClsCoverageDetails.GetEndorsementId(int.Parse(hidCOV_ID.Value)).ToString();
            }
        }
        #region GetFormValue
        /// <summary>
        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>
        private ClsCoverageDetailsInfo GetFormValue()
        {
            //Creating the Model object for holding the New data
            ClsCoverageDetailsInfo objCoverageDetailsInfo;
            objCoverageDetailsInfo = new ClsCoverageDetailsInfo();
            objCoverageDetailsInfo.COV_REF_CODE = txtCOV_REF_CODE.Text;
            objCoverageDetailsInfo.COV_CODE = txtCOV_CODE.Text;
            objCoverageDetailsInfo.COV_DES = txtCOV_DES.Text;
            objCoverageDetailsInfo.FORM_NUMBER = txtFORM_NUMBER.Text.Trim();

            //shikha- itrack - 1226

            objCoverageDetailsInfo.COV_TYPE_ABBR = txtCOV_TYPE_ABBR.Text;
            if(txtSUSEP_COV_CODE.Text.Trim() != "")
            objCoverageDetailsInfo.SUSEP_COV_CODE = Convert.ToInt32(txtSUSEP_COV_CODE.Text.Trim());

            //objCoverageDetailsInfo.SUB_LOB_ID = int.Parse(cmbSUB_LOB_ID.SelectedValue);
            if (hidSUB_LOB_ID.Value != "" && hidSUB_LOB_ID.Value != null)
            {
                objCoverageDetailsInfo.SUB_LOB_ID = int.Parse(hidSUB_LOB_ID.Value);
            }

            if (cmbSTATE_ID.SelectedValue.Trim() != "")
                objCoverageDetailsInfo.STATE_ID = int.Parse(cmbSTATE_ID.SelectedValue);
            objCoverageDetailsInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

            if (cmbLOB_ID.SelectedValue == "")
            {
                objCoverageDetailsInfo.LOB_ID = 0;
            }
            else
            {
                objCoverageDetailsInfo.LOB_ID = int.Parse(cmbLOB_ID.SelectedValue);
            }

            if (cmbTYPE.SelectedValue == "")
            {
                objCoverageDetailsInfo.TYPE = 0;
            }
            else
            {
                objCoverageDetailsInfo.TYPE = int.Parse(cmbTYPE.SelectedValue);
            }
            if (cmbPURPOSE.SelectedValue == "")
            {
                objCoverageDetailsInfo.PURPOSE = 0;
            }
            else
            {
                objCoverageDetailsInfo.PURPOSE = int.Parse(cmbPURPOSE.SelectedValue);
            }

            if (cmbLIMIT_TYPE.SelectedValue == "" || chkIsLimitApplicable.Checked == false)
            {
                objCoverageDetailsInfo.LIMIT_TYPE = 0;
            }
            else
            {
                objCoverageDetailsInfo.LIMIT_TYPE = int.Parse(cmbLIMIT_TYPE.SelectedValue);
            }

            if (cmbDEDUCTIBLE_TYPE.SelectedValue == "" || chkIsDeductApplicable.Checked == false)
            {
                objCoverageDetailsInfo.DEDUCTIBLE_TYPE = 0;
            }
            else
            {
                objCoverageDetailsInfo.DEDUCTIBLE_TYPE = int.Parse(cmbDEDUCTIBLE_TYPE.SelectedValue);
            }

            if (cmbIS_DEFAULT.SelectedValue == "1")
            {
                objCoverageDetailsInfo.IS_DEFAULT = true;
            }
            else
            {
                objCoverageDetailsInfo.IS_DEFAULT = false;
            }
            if (chkIsLimitApplicable.Checked)
            {
                objCoverageDetailsInfo.IsLimitApplicable = 1;
                hidDrawLimit.Value = "1";
            }
            else
            {
                objCoverageDetailsInfo.IsLimitApplicable = 0;
            }
            if (chkIsDeductApplicable.Checked || chkISADDDEDUCTIBLE_APP.Checked)
            {
                objCoverageDetailsInfo.IsDeductApplicable = 1;
            }
            else
            {
                objCoverageDetailsInfo.IsDeductApplicable = 0;
            }

            lblLIMITTYPE.Text = cmbLIMIT_TYPE.SelectedItem.Text.ToString();
            hidLimitTypeValue.Value = cmbLIMIT_TYPE.SelectedValue;

            lblDeductibleType.Text = cmbDEDUCTIBLE_TYPE.SelectedItem.Text.ToString();
            hidDeductTypeValue.Value = cmbDEDUCTIBLE_TYPE.SelectedValue;

            objCoverageDetailsInfo.COVERAGE_TYPE = cmbCOVERAGE_TYPE.SelectedValue;
            if (txtINCLUDED.Text.Trim() != "")
                objCoverageDetailsInfo.INCLUDED = Convert.ToDouble(txtINCLUDED.Text.Trim());


            if (txtEFFECTIVE_FROM_DATE.Text.Trim() != "")
                objCoverageDetailsInfo.EFFECTIVE_FROM_DATE = ConvertToDate(txtEFFECTIVE_FROM_DATE.Text);

            if (txtEFFECTIVE_TO_DATE.Text.Trim() != "")
                objCoverageDetailsInfo.EFFECTIVE_TO_DATE = ConvertToDate(txtEFFECTIVE_TO_DATE.Text);

            if (txtDISABLED_DATE.Text.Trim() != "")
                objCoverageDetailsInfo.DISABLED_DATE = ConvertToDate(txtDISABLED_DATE.Text);

            if (cmbIS_MANDATORY.SelectedValue != "")
                objCoverageDetailsInfo.IS_MANDATORY = Convert.ToInt32(cmbIS_MANDATORY.SelectedValue);
            else
                objCoverageDetailsInfo.IS_MANDATORY = 0;
            if (txtRANK.Text.Trim() != "")
                objCoverageDetailsInfo.RANK = int.Parse(txtRANK.Text.Trim());
            else
                objCoverageDetailsInfo.RANK = 0;
            if (cmbREINSURANCE.SelectedValue != "")
                objCoverageDetailsInfo.REINSURANCE_LOB = Convert.ToInt32(cmbREINSURANCE.SelectedValue);
            else
                objCoverageDetailsInfo.REINSURANCE_LOB = 0;
            if (cmbReinsuranceCov.SelectedValue != "")
                objCoverageDetailsInfo.REINSURANCE_COV = Convert.ToInt32(cmbReinsuranceCov.SelectedValue);
            else
                objCoverageDetailsInfo.REINSURANCE_COV = 0;
            if (cmbASLOB.SelectedValue != "")
                objCoverageDetailsInfo.ASLOB = Convert.ToInt32(cmbASLOB.SelectedValue);
            else
                objCoverageDetailsInfo.ASLOB = 0;
            if (cmbReinsuranceCalc.SelectedValue != "")
                objCoverageDetailsInfo.REINSURANCE_CALC = Convert.ToInt32(cmbReinsuranceCalc.SelectedValue);
            else
                objCoverageDetailsInfo.REINSURANCE_CALC = 0;

            if (cmbREIN_REPORT_BUCK.SelectedValue != "")
                objCoverageDetailsInfo.REIN_REPORT_BUCK = Convert.ToInt32(cmbREIN_REPORT_BUCK.SelectedValue);
            else
                objCoverageDetailsInfo.REIN_REPORT_BUCK = 0;

            if (cmbREIN_REPORT_BUCK_COMM.SelectedValue != "")
                objCoverageDetailsInfo.REIN_REPORT_BUCK_COMM = Convert.ToInt32(cmbREIN_REPORT_BUCK_COMM.SelectedValue);
            else
                objCoverageDetailsInfo.REIN_REPORT_BUCK_COMM = 0;


            if (cmbCOMM_VEHICLE.SelectedValue != "")
                objCoverageDetailsInfo.COMM_VEHICLE = Convert.ToInt32(cmbCOMM_VEHICLE.SelectedValue);
            else
                objCoverageDetailsInfo.COMM_VEHICLE = 0;

            if (cmbCOMM_REIN_COV_CAT.SelectedValue != "")
                objCoverageDetailsInfo.COMM_REIN_COV_CAT = Convert.ToInt32(cmbCOMM_REIN_COV_CAT.SelectedValue);
            else
                objCoverageDetailsInfo.COMM_REIN_COV_CAT = 0;

            if (cmbREIN_ASLOB.SelectedValue != "")
                objCoverageDetailsInfo.REIN_ASLOB = Convert.ToInt32(cmbREIN_ASLOB.SelectedValue);
            else
                objCoverageDetailsInfo.REIN_ASLOB = 0;

            if (cmbCOMM_CALC.SelectedValue != "")
                objCoverageDetailsInfo.COMM_CALC = Convert.ToInt32(cmbCOMM_CALC.SelectedValue);
            else
                objCoverageDetailsInfo.COMM_CALC = 0;
            //if (hidSUB_LOB_ID.Value != "")
            //    objCoverageDetailsInfo.SUB_LOB_ID = Convert.ToInt32(hidSUB_LOB_ID.Value);
            //else
            //    objCoverageDetailsInfo.SUB_LOB_ID = 0;

            strFormSaved = hidFormSaved.Value;
            if (hidOldData.Value != "")
            {
                strRowId = hidCOV_ID.Value;
            }
            else
            {
                strRowId = "New";
            }
            oldXML = hidOldData.Value;
            //added by pravesh as addition deductible applicable only for Home and Rental
            //			if (hidLOB_ID.Value =="1" || hidLOB_ID.Value =="6")
            if (cmbLOB_ID.SelectedValue == "1" || cmbLOB_ID.SelectedValue == "6")
            {
                if (cmbADDDEDUCTIBLE_TYPE.SelectedValue == "" || chkISADDDEDUCTIBLE_APP.Checked == false)
                {
                    objCoverageDetailsInfo.ADDDEDUCTIBLE_TYPE = 0;
                }
                else
                {
                    objCoverageDetailsInfo.ADDDEDUCTIBLE_TYPE = int.Parse(cmbADDDEDUCTIBLE_TYPE.SelectedValue);
                }
                if (chkISADDDEDUCTIBLE_APP.Checked)
                {
                    objCoverageDetailsInfo.ISADDDEDUCTIBLE_APP = 1;
                }
                else
                {
                    objCoverageDetailsInfo.ISADDDEDUCTIBLE_APP = 0;
                }
            }

            if (txtMANDATORY_DATE.Text.Trim() != "")
                objCoverageDetailsInfo.MANDATORY_DATE = ConvertToDate(txtMANDATORY_DATE.Text);
            if (txtNON_MANDATORY_DATE.Text.Trim() != "")
                objCoverageDetailsInfo.NON_MANDATORY_DATE = ConvertToDate(txtNON_MANDATORY_DATE.Text);
            if (txtDEFAULT_DATE.Text.Trim() != "")
                objCoverageDetailsInfo.DEFAULT_DATE = ConvertToDate(txtDEFAULT_DATE.Text);
            if (txtNON_DEFAULT_DATE.Text.Trim() != "")
                objCoverageDetailsInfo.NON_DEFAULT_DATE = ConvertToDate(txtNON_DEFAULT_DATE.Text);
            //
            //---------Added by Praveen Kumar 19/08/2010 starts--------------
            if (cmbDISPLAYONCLAIM.SelectedValue != "")
                objCoverageDetailsInfo.DISPLAY_ON_CLAIM = Convert.ToInt32(cmbDISPLAYONCLAIM.SelectedValue);
            else
                objCoverageDetailsInfo.DISPLAY_ON_CLAIM = 0;

            if (cmbCLAIMRESERVEAPPLY.SelectedValue != "")
                objCoverageDetailsInfo.CLAIM_RESERVE_APPLY = Convert.ToInt32(cmbCLAIMRESERVEAPPLY.SelectedValue);
            else
                objCoverageDetailsInfo.CLAIM_RESERVE_APPLY = 0;
            //---------Added by Praveen Kumar 19/08/2010 Ends --------------


            ////Ankit 26 oct 2010
            if (chkIS_MAIN.Checked == false)
            {
                objCoverageDetailsInfo.IS_MAIN = 0;
            }
            else
            {
                objCoverageDetailsInfo.IS_MAIN = 1;
            }

            return objCoverageDetailsInfo;
        }
        #endregion

        //Added by Mohit Agarwal 12-Jun-2007
        private void SetRuleXML(string lob_id, string state_id, string cov_code, DateTime start_date, DateTime end_date)
        {
            try
            {
                string RuleXML = "/cms/cmsweb/support/coverages/";

                if (start_date == DateTime.MinValue)
                    start_date = Convert.ToDateTime("1/1/1950");
                if (end_date == DateTime.MinValue)
                    end_date = Convert.ToDateTime("12/30/3000");

                string NodeText = "<Rule Action=\"Database\" StartDate=\"" + start_date.ToString("yyyy-MM-dd") + "\" EndDate=\"" + end_date.ToString("yyyy-MM-dd") + "\" STATE_ID = \"" + state_id + "\"><Conditions><Condition Operand1=\"1\" Operator=\"==\" Operand2=\"1\" OperandType=\"Double\"><ToGrant CoverageCode=\"" + cov_code + "\"><Limit Value=\"\" Operand1=\"\" Operator=\"\" Operand2=\"\" LimitID=\"\"></Limit><Deductible Value=\"\" Operand1=\"\" Operator=\"\" Operand2=\"\" DeducID=\"\"></Deductible></ToGrant></Condition></Conditions></Rule>";
                switch (lob_id)
                {
                    //Boat 
                    case "4": RuleXML += "WaterDefaultCoverageRule.xml";
                        break;
                    //Home 
                    case "1": RuleXML += "HomeCoveragesRule.xml";
                        break;
                    //Auto 
                    case "2": RuleXML += "AutoDefaultCoverageRule.xml";
                        break;
                    //Motor 
                    case "3": RuleXML += "MotorDefaultCoverageRule.xml";
                        break;
                    //Umbrella 
                    case "5": RuleXML += "UmbDefaultCoverageRule.xml";
                        break;
                    //Rental 
                    case "6": RuleXML += "RentalCoveragesRule.xml";
                        break;
                    default:
                        return;
                        break;
                }

                string filePath = System.Web.HttpContext.Current.Server.MapPath(RuleXML);

                File.SetAttributes(filePath, FileAttributes.Normal);

                XmlDocument RuleDoc = new XmlDocument();
                RuleDoc.Load(filePath);

                XmlNode node = RuleDoc.SelectSingleNode("Root/Group[@Code='Default']");

                node.InnerXml = NodeText + node.InnerXml;

                System.IO.StreamWriter rulefile = new System.IO.StreamWriter(filePath);
                rulefile.Write(RuleDoc.InnerXml);
                rulefile.Close();

                //			RuleDoc.Save(filePath);
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        #region "Web Event Handlers"
        /// <summary>
        /// If form is posted back then add entry in database using the BL object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                int intRetVal = 0;	//For retreiving the return value of business class save function
                int covCount = 0, intEndorsementID = 0;
                objAddCoverageDetails = new ClsCoverageDetails();

                //Retreiving the form values into model class object
                if (txtRANK.Text.Trim() == "")
                    txtRANK.Text = objAddCoverageDetails.GetNewOrderNo(92, cmbLOB_ID.SelectedValue);
                ClsCoverageDetailsInfo objCoverageDetailsInfo = GetFormValue();
                objCoverageDetailsInfo.IS_ACTIVE = "Y";
                int defaultCount = 0;
                ClsCoverageDetails.FindDefaultLOBForCoverage(objCoverageDetailsInfo.LOB_ID, out defaultCount);

                if (hidOldData.Value.Trim() == "") //save case
                {
                    objCoverageDetailsInfo.CREATED_BY = int.Parse(GetUserId());

                    if (objCoverageDetailsInfo.TYPE == REINSURANCE_COVERAGE)
                        covCount = CheckUniqueCovInState(txtCOV_CODE.Text, int.Parse(cmbSTATE_ID.SelectedValue), int.Parse(cmbLOB_ID.SelectedValue), "REINSURANCE");
                    else
                        covCount = CheckUniqueCovInState(txtCOV_CODE.Text, 92, int.Parse(cmbLOB_ID.SelectedValue),"");

                    if (covCount > 0)
                    {
                        intRetVal = -3;
                    }
                    if (intRetVal == 0)
                    {
                        objCoverageDetailsInfo.IS_SYSTEM_GENERAED = "Y";
                        if (objCoverageDetailsInfo.TYPE == REINSURANCE_COVERAGE) // if coverage is Reinsurance coverage
                        {
                            //objCoverageDetailsInfo.REINSURANCE_LOB = 10963;
                            intRetVal = objAddCoverageDetails.AddReinsuranceCoverage(objCoverageDetailsInfo);
                        }
                        else

                            intRetVal = objAddCoverageDetails.Add(objCoverageDetailsInfo, out intEndorsementID);
                    }
                    if (intRetVal > 0)
                    {
                        hidCOV_ID.Value = objCoverageDetailsInfo.COV_ID.ToString();
                        if (intEndorsementID > 0)
                            hidENDORSEMENT_ID.Value = intEndorsementID.ToString();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        
                        hidFormSaved.Value = "1";
                        hidCOV_IDNew.Value = objCoverageDetailsInfo.COV_ID.ToString();
                        //hidSUB_LOB_ID.Value = objCoverageDetailsInfo.SUB_LOB_ID.ToString();
                        hidCalledFrom.Value = "Add";
                        if (objCoverageDetailsInfo.TYPE == REINSURANCE_COVERAGE)
                            hidTYPE.Value = "3";
                        GetOldDataXML();
                        hidIS_ACTIVE.Value = "Y";
                        hidMode.Value = "Add";


                        if (imgSelect.Attributes["onclick"] != null)
                            imgSelect.Attributes.Remove("onclick");

                        imgSelect.Attributes.Add("onclick", "javascript:ShowMultiLingualPopup('COV_ID','" + hidCOV_ID.Value + "','MNT_COVERAGE','MNT_COVERAGE_MULTILINGUAL','COV_DES');return false;");


                        /* Commented by Pravesh on 14 Jan 2008 as Default Coverage will be handeled from Database Level
                          if(objCoverageDetailsInfo.IS_DEFAULT == true && objCoverageDetailsInfo.IS_MANDATORY == 1 && objCoverageDetailsInfo.TYPE!=REINSURANCE_COVERAGE)
                            SetRuleXML(objCoverageDetailsInfo.LOB_ID.ToString(), objCoverageDetailsInfo.STATE_ID.ToString(), objCoverageDetailsInfo.COV_CODE, objCoverageDetailsInfo.EFFECTIVE_FROM_DATE, objCoverageDetailsInfo.EFFECTIVE_TO_DATE);
                        */
                    }
                    else if (intRetVal == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                        //btnActivateDeactivate.Enabled = false;
                    }
                    else if (intRetVal == -3)
                    {
                        lblMessage.Text = "Duplicate Coverage Code!.Please Change Coverage Code.";
                        hidFormSaved.Value = "2";
                        //btnActivateDeactivate.Enabled = false;
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                    GetHiddenValues();
                    this.LoadData();

                }
                else //UPDATE CASE
                {

                    //Creating the Model object for holding the Old data
                    ClsCoverageDetailsInfo objOldCoverageDetailsInfo;
                    objOldCoverageDetailsInfo = new ClsCoverageDetailsInfo();

                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldCoverageDetailsInfo, hidOldData.Value);

                    //Setting those values into the Model object which are not in the page
                    objCoverageDetailsInfo.COV_ID = int.Parse(strRowId);
                    //Uncommented for Itrack Issue 6508 on 8 Oct 09
                    objCoverageDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());

                    //objCoverageDetailsInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    objCoverageDetailsInfo.IS_ACTIVE = hidIS_ACTIVE.Value;
                    if (objCoverageDetailsInfo.TYPE == REINSURANCE_COVERAGE) // if coverage is Reinsurance coverage
                    {
                        //objCoverageDetailsInfo.REINSURANCE_LOB = 10963;
                        intRetVal = objAddCoverageDetails.UpdateReinsuranceCoverage(objOldCoverageDetailsInfo, objCoverageDetailsInfo);
                    }
                    else
                        intRetVal = objAddCoverageDetails.Update(objOldCoverageDetailsInfo, objCoverageDetailsInfo);

                    if (intRetVal > 0)			// update successfully performed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31");
                        hidFormSaved.Value = "1";
                        hidOldData.Value = ClsCoverageDetails.GetCoverageDetailXml(int.Parse(hidCOV_ID.Value));
                        hidMode.Value = "Edit";
                        CheckCoverageUsed();
                    }
                    else if (intRetVal == -1)	// Duplicate code exist, update failed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "18");
                        hidFormSaved.Value = "1";
                    }
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "20");
                        hidFormSaved.Value = "1";
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
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (objAddCoverageDetails != null)
                    objAddCoverageDetails.Dispose();
            }
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        private void CheckCoverageUsed()
        {
            int Count = 0;
            ClsCoverageDetails.CheckCoverageUsed(int.Parse(hidCOV_ID.Value), out Count);
            if (Count > 0)
            {
                hid_CoverageUsed.Value = "1";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private int CheckUniqueCovInState(string covCode, int state, int lob, string strCalledFrom)
        {
            int covCount = 0;
            ClsCoverageDetails.CheckUniqueCovInState(covCode, state, lob, out covCount, strCalledFrom);
            return covCount;
        }

        private void SetCaptions()
        {
            capCOV_REF_CODE.Text = objResourceMgr.GetString("txtCOV_REF_CODE");
            capCOV_CODE.Text = objResourceMgr.GetString("txtCOV_CODE");
            capCOV_DES.Text = objResourceMgr.GetString("txtCOV_DES");
            capSTATE_ID.Text = objResourceMgr.GetString("cmbSTATE_ID");
            capLOB_ID.Text = objResourceMgr.GetString("cmbLOB_ID");
            capDefault.Text = objResourceMgr.GetString("cmbIS_DEFAULT");
            capPURPOSE.Text = objResourceMgr.GetString("cmbPURPOSE");
            capTYPE.Text = objResourceMgr.GetString("cmbTYPE");
            capLIMITTYPE.Text = objResourceMgr.GetString("cmbLIMITTYPE");
            capLimit_Applicable.Text = objResourceMgr.GetString("chkLimitApplicable");
            capDeduct_Applicable.Text = objResourceMgr.GetString("chkDeductApplicable");
            capDeductibleType.Text = objResourceMgr.GetString("cmbDeductibleType");
            capSUB_LOB_ID.Text = objResourceMgr.GetString("cmbSUB_LOB_ID");
            //Gaurav :Start: Added on 03-oct-2005, new field added
            capIS_MANDATORY.Text = objResourceMgr.GetString("cmbIS_MANDATORY");
            capRANK.Text = objResourceMgr.GetString("txtRANK");
            capINCLUDED.Text = objResourceMgr.GetString("txtINCLUDED");
            capCOVERAGE_TYPE.Text = objResourceMgr.GetString("cmbCOVERAGE_TYPE");
            //Gaurav :End: Added on 03-oct-2005, new field added
            capEFFECTIVE_FROM_DATE.Text = objResourceMgr.GetString("txtEFFECTIVE_FROM_DATE");
            capEFFECTIVE_TO_DATE.Text = objResourceMgr.GetString("txtEFFECTIVE_TO_DATE");
            capDISABLED_DATE.Text = objResourceMgr.GetString("txtDISABLED_DATE");
            capAddDeduct_Applicable.Text = objResourceMgr.GetString("chkAddDeductApplicable"); ;
            capAddDeductibleType.Text = objResourceMgr.GetString("cmbAddDeductibleType");

            capREINSURANCE.Text = objResourceMgr.GetString("cmbREINSURANCE");
            capReinsuranceCov.Text = objResourceMgr.GetString("cmbReinsuranceCov");
            capASLOB.Text = objResourceMgr.GetString("cmbASLOB");
            capReinsuranceCalc.Text = objResourceMgr.GetString("cmbReinsuranceCalc");

            capRptBkt.Text = objResourceMgr.GetString("cmbREIN_REPORT_BUCK");
            capREIN_REPORT_BUCK_COMM.Text = objResourceMgr.GetString("cmbREIN_REPORT_BUCK_COMM");

            capCOMM_VEHICLE.Text = objResourceMgr.GetString("cmbCOMM_VEHICLE");
            capCOMM_REIN_COV_CAT.Text = objResourceMgr.GetString("cmbCOMM_REIN_COV_CAT");
            capREIN_ASLOB.Text = objResourceMgr.GetString("cmbREIN_ASLOB");
            capCOMM_CALC.Text = objResourceMgr.GetString("cmbCOMM_CALC");
            capFORM_NUMBER.Text = objResourceMgr.GetString("txtFORM_NUMBER");
            capMANDATORY_DATE.Text = objResourceMgr.GetString("txtMANDATORY_DATE");
            capNON_MANDATORY_DATE.Text = objResourceMgr.GetString("txtNON_MANDATORY_DATE");
            capDEFAULT_DATE.Text = objResourceMgr.GetString("txtDEFAULT_DATE");
            capNON_DEFAULT_DATE.Text = objResourceMgr.GetString("txtNON_DEFAULT_DATE");
            capDISPLAYONCLAIM.Text = objResourceMgr.GetString("cmbDISPLAYONCLAIM");
            capCLAIMRESERVEAPPLY.Text = objResourceMgr.GetString("cmbCLAIMRESERVEAPPLY");
            //ankit
            capISMAIN.Text = objResourceMgr.GetString("capISMAIN");
            //capMANDATORY.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168"); 
            //lblMsg.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21"); 
            capheader.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

            capCOV_TYPE_ABBR.Text = objResourceMgr.GetString("txtCOV_TYPE_ABBR");
            capSUSEP_COV_CODE.Text = objResourceMgr.GetString("txtSUSEP_COV_CODE");
            StrAll = objResourceMgr.GetString("StrAll");
            CapComm.Text = objResourceMgr.GetString("CapComm");
            CapAddLookup.Text = objResourceMgr.GetString("CapAddLookup");
            CapRein.Text = objResourceMgr.GetString("CapRein");
            HidAPPDEDUCT.Value = objResourceMgr.GetString("chkAddDeductApplicable");
            HidDeductApplicable.Value = objResourceMgr.GetString("cmbDeductibleType");
            hidtab1.Value = objResourceMgr.GetString("hidtab1");
            hidtab2.Value = objResourceMgr.GetString("hidtab2");
            hidtab3.Value = objResourceMgr.GetString("hidtab3");
            hidtab4.Value = objResourceMgr.GetString("hidtab4");
            hidtab5.Value = objResourceMgr.GetString("hidtab5");
            hidtab6.Value = objResourceMgr.GetString("hidtab6");

        }
        //private void LoadCombo()
        //{
        //    try
        //    {


        //        string SubLobXML = ClsCommon.GetXmlForLobWithoutState();
        //        System.IO.StringReader sr = new System.IO.StringReader(SubLobXML);
        //        DataSet ds = new DataSet();
        //        ds.ReadXml(sr);
        //        DataTable dt = ds.Tables[0];
        //        DataView dvSubLob = new DataView(dt);
        //        dvSubLob.RowFilter = "LOB_ID=" + hidLOB_ID.Value;// +" AND STATE_ID=" + stateId.ToString();

        //        cmbSUB_LOB_ID.DataSource = dvSubLob;
        //        cmbSUB_LOB_ID.DataTextField = "SUB_LOB_DESC";
        //        cmbSUB_LOB_ID.DataValueField = "SUB_LOB_ID";
        //        cmbSUB_LOB_ID.DataBind();
        //        cmbSUB_LOB_ID.Items.Insert(0, "");
        //        ds.Dispose();
        //        sr.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
        //    }
        //}


        private void SetValidators()
        {


            rfvLOB_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "3");
            rfvCOV_CODE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "2");
            //rfvCOV_REF_CODE.ErrorMessage=ClsMessages.GetMessage(base.ScreenId,"1");
            rfvSTATE_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "4");
            rfvCOV_DES.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "6");
            rfvTYPE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "18");
            rfvCOVERAGE_TYPE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "7");
            rfvPURPOSE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "8");
            rfvLIMITTYPE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "9");
            rfvDeductibleType.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "22");


            //Gaurav :Start: Added on 03-oct-2005, new field added
            rfvINCLUDED.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "11");
            revINCLUDED.ValidationExpression = aRegExpDecimal;
            revDEFAULT_DATE.ValidationExpression = aRegExpDate;
            revDEFAULT_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            revNON_DEFAULT_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            revNON_MANDATORY_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            revMANDATORY_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            rfvDEFAULT_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1096");
            rfvNON_DEFAULT_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1099");
            rfvNON_MANDATORY_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1106");
            rfvMANDATORY_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1107");
           // rfvSUB_LOB_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1191");
            csvMANDATORY_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2077");
            csvNON_MANDATORY_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2077");
            csvNON_DEFAULT_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2077");
            csvDEFAULT_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2077");
            csvDISABLED_DATE1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2077");
           

            revNON_DEFAULT_DATE.ValidationExpression = aRegExpDate;
            revNON_MANDATORY_DATE.ValidationExpression = aRegExpDate;
            revMANDATORY_DATE.ValidationExpression = aRegExpDate;


            revINCLUDED.ErrorMessage = ClsMessages.FetchGeneralMessage("163");
            revRANK.ValidationExpression = aRegExpInteger;
            revRANK.ErrorMessage = ClsMessages.FetchGeneralMessage("163");
            //rfvRANK.ErrorMessage =ClsMessages.GetMessage(base.ScreenId,"12");
            rfvIS_MANDATORY.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "13");
            //Gaurav :End Added on 03-oct-2005, new field added
            revEFFECTIVE_FROM_DATE.ValidationExpression = aRegExpDate;
            revEFFECTIVE_FROM_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            revEFFECTIVE_TO_DATE.ValidationExpression = aRegExpDate;
            revEFFECTIVE_TO_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2077");
            revDISABLED_DATE.ValidationExpression = aRegExpDate;
            revDISABLED_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            rfvEFFECTIVE_FROM_DATE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "14");
            csvEFFECTIVE_TO_DATE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "15");
            csvDISABLED_DATE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "16");
            rfvADDDEDUCTIBLE_TYPE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "17");
            rfvCOMM_REIN_COV_CAT.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "19");
            //rfvREIN_ASLOB.ErrorMessage =ClsMessages.GetMessage(base.ScreenId,"20");			

            revSUSEP_COV_CODE.ErrorMessage = ClsMessages.FetchGeneralMessage("163");
            revSUSEP_COV_CODE.ValidationExpression = aRegExpInteger;
        }

        public void cmbSTATE_ID_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            hidFormSaved.Value = "4";
            if (cmbSTATE_ID.SelectedIndex == 0)
                return;
            hidSTATE_ID.Value = cmbSTATE_ID.SelectedValue;

            //Commented by Ruchika Chauhan for TFS # 846 on 27-Dec-2011
            //DataTable dt = ClsCoverageDetails.GetLobForSelectedState(int.Parse(cmbSTATE_ID.SelectedValue)); 
            DataTable dt = ClsCoverageDetails.GetLobForSelectedState(92);
            cmbLOB_ID.DataSource = dt.DefaultView;
            cmbLOB_ID.DataTextField = "LOB_DESC";
            cmbLOB_ID.DataValueField = "LOB_ID";
            cmbLOB_ID.DataBind();
            cmbLOB_ID.Items.Insert(0, "");
        }

        private void bindData()
        {
            cmbTYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%CVER");
            cmbTYPE.DataTextField = "LookupDesc";
            cmbTYPE.DataValueField = "LookupCode";
            cmbTYPE.DataBind();
            cmbTYPE.Items.Insert(0, "");
        }

        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {
            ClsCoverageDetails objCoverageDetails = new ClsCoverageDetails();
            try
            {
                Cms.BusinessLayer.BlCommon.stuTransactionInfo objStuTransactionInfo = new Cms.BusinessLayer.BlCommon.stuTransactionInfo();
                objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
                objStuTransactionInfo.loggedInUserName = GetUserName();
                //objUser =  new ClsUser();

                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
                    //objCoverageDetails.TransactionInfoParams = objStuTransactionInfo;
                    objCoverageDetails.ActivateDeactivate(hidCOV_ID.Value, "N");
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
                    hidIS_ACTIVE.Value = "N";
                }
                else
                {
                    objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
                    objCoverageDetails.TransactionInfoParams = objStuTransactionInfo;
                    objCoverageDetails.ActivateDeactivate(hidCOV_ID.Value, "Y");
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");
                    hidIS_ACTIVE.Value = "Y";
                }
                hidFormSaved.Value = "1";
                GetOldDataXML();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                lblMessage.Visible = true;
                //				if(objUser!= null)
                //					objUser.Dispose();
            }
        }
    }
}






