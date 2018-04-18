/*
Modification History
<Modified Date			: 13-Apr-10
<Modified By			: Charles Gomes
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.CmsWeb.Controls;
using System.Reflection;
using System.Resources;
using System.Data;
using System.Collections;
using Cms.Model.Policy;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace Cms.Policies.Aspx
{
    public class PolicyClauses : Cms.Policies.policiesbase
    {
        #region PAGE CONTROLS
        protected System.Web.UI.WebControls.Label capHeading;
        protected System.Web.UI.WebControls.Label capHeading1;
        protected System.Web.UI.WebControls.Label lblHeader;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capHEADER_CLAUSE_TITLE;
        protected System.Web.UI.WebControls.Label capHEADER_CLAUSE_TITLE_UD;
        protected System.Web.UI.WebControls.Label lblNoRows;
        protected System.Web.UI.WebControls.Label lblNoRows_UD;
        protected System.Web.UI.WebControls.Label lblView_Edit;
        protected System.Web.UI.WebControls.Label lblView;

        protected System.Web.UI.WebControls.GridView grdSystemDefinedClauses;
        protected System.Web.UI.WebControls.GridView grdUserDefinedClauses;

        protected Cms.CmsWeb.Controls.CmsButton btnDelete;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected Cms.CmsWeb.Controls.CmsButton btnAdd;
        protected Cms.CmsWeb.Controls.CmsButton btnDelete_UD;
        protected Cms.CmsWeb.Controls.CmsButton btnSave_UD;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSUB_LOB;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTransactLabel;
        protected System.Web.UI.HtmlControls.HtmlInputHidden lblDelete;
        protected System.Web.UI.HtmlControls.HtmlInputHidden lblAlertCheck;     
        protected System.Web.UI.HtmlControls.HtmlTableRow trNoRows;
        protected System.Web.UI.HtmlControls.HtmlTableRow trNoRows_UD;
    
        #endregion

        private ResourceManager objResourceMgr = null;
        private DataSet dsTemp = null;
        private DataTable dsTempTable = null;
        // changes by praveer for itrack no 1410
        private DataTable dtPolTemp = null;
        private ClsPolicyClauseInfo objPolicyClauseInfo = null;
        static string oldxml;
        
        static int Policy_Id = 0;
        static int Policy_Version_Id = 0;
        static int Customer_Id = 0;
        ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
        protected void Page_Load(object sender, System.EventArgs e)
        {
            #region setting screen id
            base.ScreenId = "224_28";
            #endregion

            #region setting security Xml
            btnDelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnDelete_UD.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
            btnDelete_UD.PermissionString = gstrSecurityXML;

            btnSave_UD.CmsButtonClass = CmsButtonType.Write;
            btnSave_UD.PermissionString = gstrSecurityXML;

            btnAdd.CmsButtonClass = CmsButtonType.Write;
            btnAdd.PermissionString = gstrSecurityXML;
            #endregion
            btnDelete.Visible = false;
            lblMessage.Visible = false;
          
            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.PolicyClauses", Assembly.GetExecutingAssembly());

            if (!Page.IsPostBack)
            {
                #region ADD ATTRIBUTES
                btnAdd.Attributes.Add("onclick", "javascript:openViewEditClauseWin('0','2'); return false;");
                btnSave.Attributes.Add("onclick", "javascript:return ShowAlertMessageForDelete('chkSELECT',false);");
                btnDelete.Attributes.Add("onclick", "javascript:return ShowAlertMessageForDelete('chkSELECT',true);");
                btnDelete_UD.Attributes.Add("onclick", "javascript:return ShowAlertMessageForDelete('chkSELECT_UD',true);");
                // changes by praveer for itrack no 1410
               // btnSave_UD.Attributes.Add("onclick", "javascript:return ShowAlertMessageForDelete('chkSELECT_UD',false);");
                #endregion

                hidTransactLabel.Value = ClsCommon.MapTransactionLabel("Policies/Aspx/PolicyClauses.aspx.resx");

                SetKeys();
                BindGrid();
                SetCaptions();
            }
        }

        private void SetKeys()
        {
            hidCustomerID.Value = GetCustomerID();
            hidPolicyID.Value = GetPolicyID();
            hidPolicyVersionID.Value = GetPolicyVersionID();
            hidLOBID.Value = GetLOBID();
            hidSUB_LOB.Value = GetSUB_LOB_ID();
            if (hidSUB_LOB.Value == "")
            {
                hidSUB_LOB.Value = "0";
            }
        }

        private void BindGrid()
        {
            try
            {
                objPolicyClauseInfo = new ClsPolicyClauseInfo();

                if (dsTemp != null)
                {
                    dsTemp = null;
                }

                if (dsTempTable != null)
                {
                    dsTempTable = null;
                }

                dsTemp = objPolicyClauseInfo.FetchClauses(int.Parse(hidLOBID.Value), int.Parse(hidSUB_LOB.Value), int.Parse(hidCustomerID.Value)
                    , int.Parse(hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value));

                if (dsTemp.Tables.Count > 0)
                {
                    if (dsTemp.Tables[1].Rows.Count > 0 && dsTemp.Tables[1].Select("CLAUSE_ID <> 0").Count<DataRow>() > 0)
                    {
                        dsTempTable = dsTemp.Tables[1].Select("CLAUSE_ID <> 0").CopyToDataTable<DataRow>();
                    }

                    if (dsTemp.Tables[0].Rows.Count > 0)
                    {
                        grdSystemDefinedClauses.DataSource = dsTemp.Tables[0];

                        trNoRows.Visible = false;
                       // btnDelete.Visible = true;
                        btnSave.Visible = true;
                    }
                    else
                    {
                        grdSystemDefinedClauses.DataSource = null;

                        trNoRows.Visible = true;
                       // btnDelete.Visible = false;
                        btnSave.Visible = false;
                    }
                    grdSystemDefinedClauses.DataBind();

                    if (dsTemp.Tables[1].Select("CLAUSE_ID = 0").Count<DataRow>() > 0)
                    {   // changes by praveer for itrack no 1410
                        dtPolTemp = dsTemp.Tables[1].Select("CLAUSE_ID = 0").CopyToDataTable<DataRow>();
                        grdUserDefinedClauses.DataSource = dtPolTemp;
                        trNoRows_UD.Visible = false;
                        btnDelete_UD.Visible = true;
                        btnSave_UD.Visible = true;
                        DataTable dtview = dsTemp.Tables[1];
                        oldxml = "";
                        oldxml = ClsCommon.GetXML(dtview);
                    }
                    else
                    {
                        grdUserDefinedClauses.DataSource = null;

                        trNoRows_UD.Visible = true;
                        btnSave_UD.Visible = false;
                        btnDelete_UD.Visible = false;
                    }
                    grdUserDefinedClauses.DataBind();

                    
                }
            }
            catch
            {
                lblMessage.Visible = true;
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "1");
            }
            finally
            {
                if (dsTemp != null)
                    dsTemp.Dispose();
            }
        }

        protected void grdUserDefinedClauses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string PolicyStatus = GetPolicyStatus();
            
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Label lbl = (Label)(e.Row.FindControl("capHEADER_CLAUSE_CODE"));
                    if (lbl != null)
                        lbl.Text = objResourceMgr.GetString("capHEADER_CLAUSE_CODE");

                     lbl = (Label)(e.Row.FindControl("capHEADER_CLAUSE_TITLE_UD"));
                    if (lbl != null)
                        lbl.Text = objResourceMgr.GetString("capHEADER_CLAUSE_TITLE");

                    lbl = (Label)(e.Row.FindControl("capHEADER_SUSEP_LOB_UD"));
                    if (lbl != null)
                        lbl.Text = objResourceMgr.GetString("capHEADER_SUSEP_LOB");
                }
                else if (e.Row.RowType == DataControlRowType.DataRow)
                {
                   
                        HtmlAnchor hlk = (HtmlAnchor)e.Row.FindControl("hlkView_Edit");
               
                    
                    Label lbl = (Label)e.Row.FindControl("capPOL_CLAUSE_ID");
                    Label lblPREVIOUS_VERSION_ID = (Label)e.Row.FindControl("capPREVIOUS_VERSION_ID");

                    //-------------- Added By Praveen Kumar 29/04/2010  starts --------------
                    // Populating SUSEP LOB
                    DropDownList Cmb_SUSEP_LOB = (DropDownList)e.Row.FindControl("cmbSUSEP_LOB_ID");
                    BindSUSEP_LOB(Cmb_SUSEP_LOB);
                    CheckBox chkbox = (CheckBox)e.Row.FindControl("chkSELECT_UD");
                    chkbox.Checked = true;
                    Label lblSUSEP_ID = (Label)e.Row.FindControl("capPOL_SUSEP_LOB_ID");
                    if (lblSUSEP_ID != null)
                        Cmb_SUSEP_LOB.SelectedValue = lblSUSEP_ID.Text == "" ? "0" : lblSUSEP_ID.Text;
                    // changes by praveer for itrack no 1410
                    if (dtPolTemp != null)
                        for (int i = 0; i < dtPolTemp.Rows.Count; i++)
                        {
                            if (dtPolTemp.Rows[i]["POL_CLAUSE_ID"].ToString().Trim() == lbl.Text.Trim() && dtPolTemp.Rows[i]["IS_ACTIVE"].ToString().Trim() == "N")
                            {
                                e.Row.ControlStyle.ForeColor = Color.Red;
                                chkbox.Checked = false;

                            }
                        }
                    //-------------- Added By Praveen Kumar 30/04/2010  Ends --------------

                    if (hlk != null && lbl != null && Cmb_SUSEP_LOB != null)
                    {
                        Label lbl1 = (Label)e.Row.FindControl("lblView_Edit");
                        if (lbl1 != null )
                        {

                            if (PolicyStatus == "" || PolicyStatus == "UENDRS")
                                lbl1.Text = objResourceMgr.GetString("hlkView_Edit");
                            else
                            {
                                
                                 lbl1.Text = objResourceMgr.GetString("hlkView");
                            }
                            if (lblPREVIOUS_VERSION_ID.Text != hidPolicyVersionID.Value) {
                                lbl1.Text = objResourceMgr.GetString("hlkView");
                            }
                        }

                       
                        // 2 is for user defined
                        hlk.HRef = "javascript:openViewEditClauseWin(" + lbl.Text.Trim() + ", '2');";
                    }

                }
            }
            catch
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "13");
                lblMessage.Visible = true;
            }
        }

        protected void grdSystemDefinedClauses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Label lbl = (Label)(e.Row.FindControl("capHEADER_CLAUSE_CODE"));
                    if (lbl != null)
                        lbl.Text = objResourceMgr.GetString("capHEADER_CLAUSE_CODE");

                    lbl = (Label)(e.Row.FindControl("capHEADER_CLAUSE_TITLE"));
                    if (lbl != null)
                        lbl.Text = objResourceMgr.GetString("capHEADER_CLAUSE_TITLE");

                    lbl = (Label)(e.Row.FindControl("capHEADER_SUSEP_LOB"));
                    if (lbl != null)
                        lbl.Text = objResourceMgr.GetString("capHEADER_SUSEP_LOB");

                }
                else if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = (Label)e.Row.FindControl("capCLAUSE_ID");

                    // This code will used when view edit link will used for saving data in pol_clause table
                    //hlk.Disabled = true;
                    if (lbl != null)
                    {
                        HtmlAnchor hlk = (HtmlAnchor)e.Row.FindControl("hlkView");

                        if (dsTempTable != null)
                        {
                            for (int i = 0; i < dsTempTable.Rows.Count; i++)
                            {
                                if (dsTempTable.Rows[i]["CLAUSE_ID"].ToString().Trim() == lbl.Text.Trim() && dsTempTable.Rows[i]["IS_ACTIVE"].ToString().Trim() == "Y")
                                {
                                    CheckBox chkSELECT = (CheckBox)e.Row.FindControl("chkSELECT");
                                    if (chkSELECT != null)
                                    {
                                        chkSELECT.Checked = true;

                                        //hlk.Disabled = false;
                                    }
                                    Label lblClauseCode = (Label)e.Row.FindControl("capCLAUSE_CODE");
                                    lblClauseCode.Text = dsTempTable.Rows[i]["CLAUSE_CODE"].ToString().Trim();

                                    Label lblClauseTitle = (Label)e.Row.FindControl("capCLAUSE_TITLE");
                                    lblClauseTitle.Text = dsTempTable.Rows[i]["CLAUSE_TITLE"].ToString().Trim();


                                   
                                }

                                if (dsTempTable.Rows[i]["CLAUSE_ID"].ToString().Trim() == lbl.Text.Trim() && dsTempTable.Rows[i]["IS_ACTIVE"].ToString().Trim() == "N")
                                {
                                    e.Row.ControlStyle.ForeColor = Color.Red;

                                }

                                if (dsTempTable.Rows[i]["SUBLOB_ID"].ToString().Trim() == "0" && dsTempTable.Rows[i]["CLAUSE_ID"].ToString().Trim() == lbl.Text.Trim())
                                {
                                    CheckBox chkSELECT = (CheckBox)e.Row.FindControl("chkSELECT");
                                    chkSELECT.Enabled = false;
                                }
                              
                            }
                        }
                        if (hlk != null && lbl != null)
                        {
                            Label lbl1 = (Label)e.Row.FindControl("lblView");
                            if (lbl1 != null)
                            {
                                lbl1.Text = objResourceMgr.GetString("hlkView");

                                CheckBox chkSELECT = (CheckBox)e.Row.FindControl("chkSELECT");
                                if (chkSELECT != null && chkSELECT.Checked)
                                {
                                    hlk.HRef = "javascript:openViewEditClauseWin(" + lbl.Text.Trim() + ", '1','1');";
                                }
                                else
                                {
                                    hlk.HRef = "javascript:openViewEditClauseWin(" + lbl.Text.Trim() + ", '1','0');";
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "11");
                lblMessage.Visible = true;
            }
        }

        //-------------- Added By Praveen Kumar 29/04/2010  starts --------------
        private void BindSUSEP_LOB(System.Web.UI.WebControls.DropDownList Cmb)
        {
          //  #region "Loading singleton"

            // CHANGES FOR ITRACK 682

            //string LANG_ID = GetLanguageID();
            //DataTable dt = ClsEndorsmentDetails.GetSUSEPLOBs(LANG_ID).Tables[0];//Cms.CmsWeb.ClsFetcher.Susep_lob;
            //Cmb.DataSource = dt;
            //Cmb.DataTextField = "SUSEP_LOB_DESC";
            //Cmb.DataValueField = "SUSEP_LOB_ID";
            //Cmb.DataBind();
            //Cmb.Items.Insert(0, new ListItem("", ""));

            DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
            Cmb.DataSource = dtLOBs;
            Cmb.DataTextField = "LOB_DESC";
            Cmb.DataValueField = "LOB_ID";
            Cmb.DataBind();
            Cmb.Items.Insert(0, new ListItem("", ""));

           // #endregion//Loading singleton

        }
        //-------------- Added By Praveen Kumar 29/04/2010  Ends --------------

        private void SetCaptions()
        {
            try
            {
                capHeading.Text = objResourceMgr.GetString("capHeading");
                capHeading1.Text = objResourceMgr.GetString("capHeading1");
                lblHeader.Text = objResourceMgr.GetString("lblHeader");
                lblDelete.Value = objResourceMgr.GetString("lblDelete");
                lblAlertCheck.Value = objResourceMgr.GetString("lblAlertCheck");

                Label lbl = (Label)(Page.FindControl("capHEADER_CLAUSE_TITLE"));
                if (lbl != null)
                    lbl.Text = objResourceMgr.GetString("capHEADER_CLAUSE_TITLE");

                lbl = (Label)(Page.FindControl("lblNoRows"));
                if (lbl != null)
                    lblNoRows.Text = objResourceMgr.GetString("lblNoRows");

                lbl = (Label)(Page.FindControl("lblNoRows_UD"));
                if (lbl != null)
                    lblNoRows_UD.Text = objResourceMgr.GetString("lblNoRows");

                CmsButton cmsb = (CmsButton)(Page.FindControl("btnAdd"));
                if (cmsb != null)
                    cmsb.Text = objResourceMgr.GetString("btnAdd");
                cmsb = (CmsButton)(Page.FindControl("btnDelete_UD"));
                if (cmsb != null)
                    cmsb.Text = objResourceMgr.GetString("btnDelete_UD");
                cmsb = (CmsButton)(Page.FindControl("btnSave_UD"));
                if (cmsb != null)
                    cmsb.Text = objResourceMgr.GetString("btnSave_UD");

            }
            catch
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "10");
                lblMessage.Visible = false;
            }
        }

        /// <summary>
        /// Delete System Defined Clauses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int retValue = 0;

            foreach (GridViewRow row in grdSystemDefinedClauses.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkbox = (CheckBox)row.FindControl("chkSELECT");
                    if (chkbox != null && chkbox.Checked)
                    {
                        objPolicyClauseInfo = new ClsPolicyClauseInfo();
                        objPolicyClauseInfo.TransactLabel = hidTransactLabel.Value;

                        Label lbl = (Label)row.FindControl("capCLAUSE_ID");
                        if (lbl != null)
                        {
                            objPolicyClauseInfo.CLAUSE_ID.CurrentValue = int.Parse(lbl.Text.Trim());
                        }
                        lbl = (Label)row.FindControl("capCLAUSE_TITLE");
                        if (lbl != null)
                        {
                            objPolicyClauseInfo.CLAUSE_TITLE.CurrentValue = lbl.Text.Trim();
                        }
                        lbl = (Label)row.FindControl("capCLAUSE_DESCRIPTION");
                        if (lbl != null)
                        {
                            objPolicyClauseInfo.CLAUSE_DESCRIPTION.CurrentValue = lbl.Text.Trim();
                        }

                        objPolicyClauseInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCustomerID.Value);
                        objPolicyClauseInfo.POLICY_ID.CurrentValue = int.Parse(hidPolicyID.Value);
                        objPolicyClauseInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPolicyVersionID.Value);
                        objPolicyClauseInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                        retValue = objPolicyClauseInfo.DeletePolClauses();
                        BindGrid();
                    }
                }
            }

            if (retValue > 0)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "3");
                lblMessage.Visible = true;
                base.OpenEndorsementDetails();
            }
            else if (retValue == -3)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "7");
                lblMessage.Visible = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int retValue = 0;
             int CreatedBy = int.Parse(GetUserId());
             objPolicyClauseInfo = new ClsPolicyClauseInfo();
             objPolicyClauseInfo.TransactLabel = hidTransactLabel.Value;
             objPolicyClauseInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCustomerID.Value);
             objPolicyClauseInfo.POLICY_ID.CurrentValue = int.Parse(hidPolicyID.Value);
             objPolicyClauseInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPolicyVersionID.Value);
             
            try
            {
               
                foreach (GridViewRow row in grdSystemDefinedClauses.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkbox = (CheckBox)row.FindControl("chkSELECT");
                        if (chkbox != null && chkbox.Checked)
                        {


                            
                            objPolicyClauseInfo = new ClsPolicyClauseInfo();
                           
                            objPolicyClauseInfo.TransactLabel = hidTransactLabel.Value;

                            Label lbl = (Label)row.FindControl("capCLAUSE_ID");
                            if (lbl != null)
                            {
                                objPolicyClauseInfo.CLAUSE_ID.CurrentValue = int.Parse(lbl.Text.Trim());
                            }
                            //lbl = (Label)row.FindControl("capCLAUSE_TITLE");
                            //if (lbl != null)
                            //{
                            //    objPolicyClauseInfo.CLAUSE_TITLE.CurrentValue = lbl.Text.Trim();
                            //}
                            //lbl = (Label)row.FindControl("capCLAUSE_DESCRIPTION");
                            //if (lbl != null)
                            //{
                            //    objPolicyClauseInfo.CLAUSE_DESCRIPTION.CurrentValue = lbl.Text.Trim();
                            //}


                            //-------------- Added By Praveen Kumar 10/05/2010  starts --------------

                            //Label lblSuSep = (Label)row.FindControl("capSUSEP_LOB_ID");
                            //if (lblSuSep != null && lblSuSep.Text.Trim() != "")
                            //{
                                objPolicyClauseInfo.SUSEP_LOB_ID.CurrentValue = int.Parse(GetLOBID());
                           // }

                            //-------------- Added By Praveen Kumar 10/05/2010  Ends --------------

                            objPolicyClauseInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCustomerID.Value);
                            objPolicyClauseInfo.POLICY_ID.CurrentValue = int.Parse(hidPolicyID.Value);
                            objPolicyClauseInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPolicyVersionID.Value);
                            objPolicyClauseInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());


                            retValue = objGeneralInformation.AddPolClauses(objPolicyClauseInfo);
                           // retValue = objPolicyClauseInfo.AddPolClauses(); 
                            //BindGrid();
                            // base.OpenEndorsementDetails();
                        }
                        else
                        {
                           

                            Label lbl = (Label)row.FindControl("capCLAUSE_ID");
                            if (lbl != null)
                            {
                                objPolicyClauseInfo.CLAUSE_ID.CurrentValue = int.Parse(lbl.Text.Trim());
                            }
                            objPolicyClauseInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());                           
                            retValue = objGeneralInformation.DeactivatePolClauses(objPolicyClauseInfo);
                            
                        }
                    }
                }


                if (retValue >= 0)
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "2");
                    lblMessage.Visible = true;
                    
                }
                else
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "8");
                    lblMessage.Visible = true;
                }
                BindGrid();
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "8");
                lblMessage.Visible = true;
            }

        }

        protected void btnSave_UD_Click(object sender, EventArgs e)
        {
            int retValue = 0;
            int retVal = 0;
            ArrayList arlObjClauses = new ArrayList();
            // changes by praveer for itrack no 1410 , deactivation of user defined clauses for document generation
            ArrayList arlObjUserClauses = new ArrayList();            
            ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();          
           
            try
            {
                Customer_Id = int.Parse(hidCustomerID.Value);
                Policy_Id = int.Parse(hidPolicyID.Value);
                Policy_Version_Id = int.Parse(hidPolicyVersionID.Value);
                int CreatedBy = int.Parse(GetUserId());
                
                foreach (GridViewRow row in grdUserDefinedClauses.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkbox = (CheckBox)row.FindControl("chkSELECT_UD");
                        if (chkbox != null && chkbox.Checked)
                        {
                            objPolicyClauseInfo = new ClsPolicyClauseInfo();
                            objPolicyClauseInfo.CLAUSE_ID.CurrentValue = 0;
                            objPolicyClauseInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCustomerID.Value);
                            objPolicyClauseInfo.POLICY_ID.CurrentValue = int.Parse(hidPolicyID.Value);
                            objPolicyClauseInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPolicyVersionID.Value);
                            objPolicyClauseInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                            objPolicyClauseInfo.TransactLabel = hidTransactLabel.Value;

                            Label lbl = (Label)row.FindControl("capCLAUSE_TITLE_UD");
                            if (lbl != null)
                            {
                                objPolicyClauseInfo.CLAUSE_TITLE.CurrentValue = lbl.Text.Trim();
                            }
                            lbl = (Label)row.FindControl("capCLAUSE_DESCRIPTION_UD");
                            if (lbl != null)
                            {
                                objPolicyClauseInfo.CLAUSE_DESCRIPTION.CurrentValue = lbl.Text.Trim();
                            }
                           

                            lbl = (Label)row.FindControl("capPOL_CLAUSE_ID");
                            if (lbl != null)
                            {
                                objPolicyClauseInfo.POL_CLAUSE_ID.CurrentValue = int.Parse(lbl.Text.Trim());
                            }

                            //-------------- Added By Praveen Kumar 29/04/2010  starts --------------

                            //   DropDownList ddl = (DropDownList)row.FindControl("cmbSUSEP_LOB_ID");
                            //   if (ddl.SelectedValue != "")
                            //   {
                            objPolicyClauseInfo.SUSEP_LOB_ID.CurrentValue = int.Parse(GetLOBID());
                            //  }
                            // else { objPolicyClauseInfo.SUSEP_LOB_ID.CurrentValue = 0; }

                            //-------------- Added By Praveen Kumar 29/04/2010  Ends --------------
                           
                            arlObjClauses.Add(objPolicyClauseInfo);                          

                        }
                        // changes by praveer for itrack no 1410 , deactivation of user defined clauses for document generation
                        else
                        {
                            ClsPolicyClauseInfo objPolicyClause = new ClsPolicyClauseInfo();
                            objPolicyClause.CLAUSE_ID.CurrentValue = 0;
                            objPolicyClause.CUSTOMER_ID.CurrentValue = int.Parse(hidCustomerID.Value);
                            objPolicyClause.POLICY_ID.CurrentValue = int.Parse(hidPolicyID.Value);
                            objPolicyClause.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPolicyVersionID.Value);
                            objPolicyClause.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                            objPolicyClause.TransactLabel = hidTransactLabel.Value;
                            Label lbl = (Label)row.FindControl("capPOL_CLAUSE_ID");
                            if (lbl != null)
                            {
                                objPolicyClause.POL_CLAUSE_ID.CurrentValue = int.Parse(lbl.Text.Trim());
                            }

                            arlObjUserClauses.Add(objPolicyClause);
                        }
                    }
                }

                retValue = objGeneralInformation.UpdatePolClauses(arlObjClauses, oldxml, Customer_Id, Policy_Id, Policy_Version_Id, CreatedBy);
              retVal = objGeneralInformation.DeactivatePolClauses(arlObjUserClauses, oldxml, Customer_Id, Policy_Id, Policy_Version_Id, CreatedBy);
                //retValue = objPolicyClauseInfo.UpdatePolClauses();
              if (retValue >= 0 && retVal >= 0)
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "14");
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "15");
                    lblMessage.Visible = true;
                }
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "15");
                lblMessage.Visible = true;
            }
            BindGrid();
            base.OpenEndorsementDetails();
        }

        protected void btnDelete_UD_Click(object sender, EventArgs e)
        {
            int retValue = 0;
            try
            {
                foreach (GridViewRow row in grdUserDefinedClauses.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkbox = (CheckBox)row.FindControl("chkSELECT_UD");
                        if (chkbox != null && chkbox.Checked)
                        {
                            objPolicyClauseInfo = new ClsPolicyClauseInfo();

                            objPolicyClauseInfo.TransactLabel = hidTransactLabel.Value;

                            Label lbl = (Label)row.FindControl("capCLAUSE_ID_UD");
                            if (lbl != null)
                            {
                                objPolicyClauseInfo.CLAUSE_ID.CurrentValue = int.Parse(lbl.Text.Trim());
                            }

                            lbl = (Label)row.FindControl("capPOL_CLAUSE_ID");
                            if (lbl != null)
                            {
                                objPolicyClauseInfo.POL_CLAUSE_ID.CurrentValue = int.Parse(lbl.Text.Trim());
                            }
                            lbl = (Label)row.FindControl("capCLAUSE_TITLE_UD");
                            if (lbl != null)
                            {
                                objPolicyClauseInfo.CLAUSE_TITLE.CurrentValue = lbl.Text.Trim();
                            }
                            lbl = (Label)row.FindControl("capCLAUSE_DESCRIPTION_UD");
                            if (lbl != null)
                            {
                                objPolicyClauseInfo.CLAUSE_DESCRIPTION.CurrentValue = lbl.Text.Trim();
                            }

                            objPolicyClauseInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCustomerID.Value);
                            objPolicyClauseInfo.POLICY_ID.CurrentValue = int.Parse(hidPolicyID.Value);
                            objPolicyClauseInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPolicyVersionID.Value);
                            objPolicyClauseInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                            retValue = objPolicyClauseInfo.DeletePolClauses();
                            BindGrid();
                        }
                    }
                }
                if (retValue > 0)
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "5");
                    lblMessage.Visible = true;
                    base.OpenEndorsementDetails();
                }
                else
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "6");
                    lblMessage.Visible = true;
                }
            }
            catch
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "6");
                lblMessage.Visible = true;
            }
        }
    }
}
