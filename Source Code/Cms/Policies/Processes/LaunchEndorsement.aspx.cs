/******************************************************************************************
<Author					: -  Lalit Chauhan  
<Start Date				: -	 26 -August- 2010
<End Date				: -	
<Description			: -  Launch Auto Endorsement
<Review Date			: - 
<Reviewed By			: - 	
 
Modification History
<Modified Date			: 
<Modified By			: 
<Purpose				: 
						
****************************************************************************************** */


#region Using Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using System.Resources;
using Cms.CmsWeb.Controls;
using System.Collections;
using Cms.BusinessLayer.BlAccount;
using System.Data;
using Cms.BusinessLayer.BlProcess;
using Cms.Model.Policy.Process;
using Cms.CmsWeb;
using System.Xml;
using Cms.BusinessLayer.BlApplication;

#endregion

namespace Cms.Policies.Processes
{
    public partial class LaunchEndorsement : Cms.Policies.policiesbase
    {
        string url = "";
        System.Resources.ResourceManager objResource;
        IList Ilist;
        ClsCommon objCommon = new ClsCommon();
        ClsEndorsmentProcess objProcess = new ClsEndorsmentProcess();
        DataSet dstemp;
        ClsGeneralInformation objGenInfo;
        protected void Page_Load(object sender, EventArgs e)
        {

            base.ScreenId = "501";
            //get Lookup url,open lookup window for policy No select
            url = ClsCommon.GetLookupURL();
            imgSelect.Attributes.Add("onclick", "javascript:OpenLookupWithFunction('" + url + "','POLICY_ID','POLICY_NUMBER','hidPolicyID','txtPOLICY_ID','PolicyForFeeRev','Policy','','');");

            //resource manager object for multilingual
            objResource = new System.Resources.ResourceManager("Cms.Policies.Processes.LaunchEndorsement", System.Reflection.Assembly.GetExecutingAssembly());


            //Buttons security xml
            btnFIND.CmsButtonClass = CmsButtonType.Read;
            btnFIND.PermissionString = gstrSecurityXML;

            btnLAUNCH.CmsButtonClass = CmsButtonType.Read;
            btnLAUNCH.PermissionString = gstrSecurityXML;

            btnLAUNCH.Attributes.Add("onclick", "javascript:EnableEndrfv();");
            btnFIND.Attributes.Add("onclick", "javascript:Disableallrfv();DisablePolicyIDrfv(true,'inline')");


            #region I S    P O S T B A C K
            if (!IsPostBack)
            {
                SetCaptions();   //set Label captions 
                SetErrorMsg();   //set validators error messages
                BindDropDowns(); //bind Endorsement option
            }
            #endregion


        }

        private void SetCaptions()
        {
            capPOLICY_ID.Text = objResource.GetString("txtPOLICY_ID");
            capHeaderLabel.Text = objResource.GetString("capHeaderLabel");
            lblUWRULES_STATUS.Text = objResource.GetString("lblUWRULES_STATUS");
            capENDORSEMENT_TYPE.Text = objResource.GetString("cmbENDORSEMENT_TYPE");
            capRISK_NAME.Text = objResource.GetString("cmbRISK_NAME");
            capCOVERAGES.Text = objResource.GetString("cmbCOVERAGES");
            capREDUCE_LIMIT.Text = objResource.GetString("txtREDUCE_LIMIT");
            capCOVERAGE_LIMIT.Text = objResource.GetString("txtCOVERAGE_LIMIT");



        }

        private void SetErrorMsg()
        {
            this.revREDUCE_LIMIT.ValidationExpression = aRegExpCurrencyformat;//aRegExpCurrencyformat;
            this.revREDUCE_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
            this.rfvREDUCE_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1171");

            this.rfvPOLICY_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1");
            this.rfvENDORSEMENT_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2");
            this.rfvRISK_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "3");
            this.rfvCOVERAGES.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "4");
            //this.revREDUCE_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "5");

            this.btnFIND.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(ScreenId, "btnFIND");
            this.btnLAUNCH.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(ScreenId, "btnLAUNCH");
            
            hidMess.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1128");

        }

        private void BindDropDowns()
        {
            Ilist = ClsCommon.GetLookup("ENDOPT");
            cmbENDORSEMENT_TYPE.DataSource = Ilist;
            cmbENDORSEMENT_TYPE.DataTextField = "LookupDesc";
            cmbENDORSEMENT_TYPE.DataValueField = "LookupID";
            cmbENDORSEMENT_TYPE.DataBind();
            cmbENDORSEMENT_TYPE.Items.Insert(0, "");
        }

        protected void btnLAUNCH_Click(object sender, EventArgs e)
        {
            //14676 Launch Endorsement
            //14677 Launch Endorsement and Reduce Limit
            //14678 Launch and Reduce Limit and Commit

            if (cmbENDORSEMENT_TYPE.SelectedValue != "" && txtPOLICY_ID.Text != "")
            {
                if (getPolicy())
                {
                    if (cmbENDORSEMENT_TYPE.SelectedValue == "14676") //launch auto endorsment
                    {
                        if (int.Parse(hidLOB_ID.Value) > 8)
                        this.LaunchEndorsment();
                        cmbENDORSEMENT_TYPE.SelectedValue = "";
                    }
                    else if (cmbENDORSEMENT_TYPE.SelectedValue == "14677") //launch auto endorsment and reduce limit
                    {
                        if (int.Parse(hidLOB_ID.Value) > 8)
                            this.LEndorsementRedLimit(txtREDUCE_LIMIT.Text, hidRISK_ID.Value, hidCOVERAGE_CODE_ID.Value);
                        cmbENDORSEMENT_TYPE.SelectedValue = "";
                    }
                    else if (cmbENDORSEMENT_TYPE.SelectedValue == "14678")//launch auto endorsment and reduce limit and committ
                    {
                        if (int.Parse(hidLOB_ID.Value) > 8)
                            this.LEndRedLimitCommitt(txtREDUCE_LIMIT.Text, hidRISK_ID.Value, hidCOVERAGE_CODE_ID.Value);
                        cmbENDORSEMENT_TYPE.SelectedValue = "";
                    }
                }
            }
            else
                lblMessage.Text = "";
        }

        protected void btnFIND_Click(object sender, EventArgs e)
        {
            getPolicy();
        }

        /// <summary>
        /// Launch Endorsement
        /// </summary>
        private void LaunchEndorsment()
        {
            try
            {
                hidPROCESS_ID.Value = "3";
                objProcess.BeginTransaction();
                if (objProcess.CheckProcessEligibility(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), 3) == 1)
                {
                    if (StartProcess())
                    {
                        verifyRule();

                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("682");
                    }

                }
                else
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "6");
                }
                objProcess.CommitTransaction();
            }
            catch (Exception ex)
            { 
                objProcess.RollbackTransaction();
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        private bool LEndorsementRedLimit(string ReducePremium, string Riskid, string Coverage_Code_id)
        {
            bool valid = false;
            try
            {
                hidPROCESS_ID.Value = "3";
                objProcess.BeginTransaction();
                if (objProcess.CheckProcessEligibility(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), 3) == 1)
                {
                    if (StartProcess())
                    {
                        if (this.ReduceLimit(ReducePremium, Riskid, Coverage_Code_id))
                        {
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "7");
                            objProcess.CommitTransaction();
                        }
                        else
                        {
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "6");
                            objProcess.RollbackTransaction();
                        }
                    }
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "6");
                        objProcess.RollbackTransaction();
                    }
                }
                else
                {
                    lblMessage.Text = "";
                }

            }
            catch (Exception ex)
            {
                objProcess.RollbackTransaction();
            Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return valid;
        }

        private void LEndRedLimitCommitt(string ReducePremium, string Riskid, string Coverage_Code_id)
        {
            //bool valid = false;
            try
            {
                hidPROCESS_ID.Value = "3";
                objProcess.BeginTransaction();
                if (objProcess.CheckProcessEligibility(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), 3) == 1)
                {
                    if (StartProcess())
                    {
                        if (this.ReduceLimit(ReducePremium, Riskid, Coverage_Code_id))
                        {
                            if (CommitProcess())
                            {
                                objProcess.CommitTransaction();
                                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("689");
                            }
                            else
                            {
                                objProcess.RollbackTransaction();
                                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "6");
                            }
                        }
                        else
                        {
                            objProcess.RollbackTransaction();
                        }
                    }
                }
                else
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "6");
                }

            }
            catch (Exception ex)
            { 
                objProcess.RollbackTransaction();
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            //return valid;
        }

        private bool StartProcess()
        {
            try
            {
                bool RetVal = false;
                Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();

                objProcessInfo = getValues();
                objProcessInfo.CREATED_BY = int.Parse(GetUserId());
                objProcessInfo.CREATED_DATETIME = DateTime.Now;

                ClsEndorsmentProcess objProcess = new ClsEndorsmentProcess();

                if (objProcess.StartProcess(objProcessInfo) == true)
                {

                    //saved successfully
                    hidROW_ID.Value = objProcessInfo.ROW_ID.ToString();
                    hidENDORSEMENT_NO.Value = objProcessInfo.ENDORSEMENT_NO.ToString();
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("682");
                    hidNEW_POLICY_VERSION_ID.Value = objProcessInfo.NEW_POLICY_VERSION_ID.ToString();
                    //hidDisplayBody.Value = "True";
                    //Generating the xml of old data
                    //GetOldDataXml();

                    //Setting the new policy in session
                    //SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);

                    RetVal = true;
                }
                else
                {
                    //lblMessage.Text = ClsMessages.FetchGeneralMessage("594");

                    //Hiding the commit and rollback buttons
                    //HideButtons();
                    // hidDisplayBody.Value = "False";
                    RetVal = false;
                }


                lblMessage.Visible = true;

                //Refresh the Policy Top.
                //cltPolicyTop.CallPageLoad();

                return RetVal;
            }
            catch (Exception objExp)
            {
                //Publishing the exception
                //Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                //lblMessage.Text = "Unable to start Process.Please Try later.";
                //lblMessage.Visible = true;
                ////Hiding the commit and rollback buttons
                //HideButtons();
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                return false;
            }

        }

        private ClsProcessInfo getValues()
        {
            ClsProcessInfo objProcessInfo = new ClsProcessInfo();
            get_Policydetails();
            GetPolicy_ProcessDetails();
            objProcessInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
            objProcessInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
            objProcessInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);

            if (hidPOLICY_EFF_DATE.Value != "")
                objProcessInfo.EFFECTIVE_DATETIME = ConvertToDate(hidPOLICY_EFF_DATE.Value);

            if (hidPOLICY_EXP_DATE.Value != "")
                objProcessInfo.EXPIRY_DATE = ConvertToDate(hidPOLICY_EXP_DATE.Value);

            objProcessInfo.PROPERTY_INSPECTION_CREDIT = "Y";//chkPROPERTY_INSPECTION_CREDIT.Checked ? "Y" : "N";

            if (hidENDORSEMENT_NO.Value.Trim() != "")
                objProcessInfo.ENDORSEMENT_NO = int.Parse(hidENDORSEMENT_NO.Value);

            objProcessInfo.COMMENTS = "";

            objProcessInfo.NEW_CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
            objProcessInfo.NEW_POLICY_ID = int.Parse(hidPOLICY_ID.Value);

            if (hidNEW_POLICY_VERSION_ID != null && hidNEW_POLICY_VERSION_ID.Value != "")
                objProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidNEW_POLICY_VERSION_ID.Value);
            else
                objProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);

            if (hidUNDERWRITER.Value != "" && hidUNDERWRITER.Value != "0")
                objProcessInfo.UNDERWRITER = Convert.ToInt32(hidUNDERWRITER.Value);

            objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
            objProcessInfo.CREATED_BY = int.Parse(GetUserId());

            objProcessInfo.POLICY_PREVIOUS_STATUS = objProcess.GetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID);
            objProcessInfo.POLICY_CURRENT_STATUS = objProcess.GetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);

            //if (chkINTERNAL_CHANGE.Checked)
            //objProcessInfo.INTERNAL_CHANGE = Convert.ToString((int)(enumYESNO_LOOKUP_CODE.YES));
            //else
            objProcessInfo.INTERNAL_CHANGE = Convert.ToString((int)(enumYESNO_LOOKUP_CODE.NO));

            // objProcessInfo.PRINTING_OPTIONS = 0;//int.Parse(Convert.ToString(hidPrinting_Options.Value) == "" ? "0" : hidPrinting_Options.Value);

            //if (cmbINSURED.SelectedItem != null && cmbINSURED.SelectedItem.Value != "")
            //    objProcessInfo.INSURED = int.Parse(cmbINSURED.SelectedItem.Value);

            //if (cmbAGENCY_PRINT.SelectedItem != null && cmbAGENCY_PRINT.SelectedItem.Value != "")
            //    objProcessInfo.AGENCY_PRINT = int.Parse(cmbAGENCY_PRINT.SelectedItem.Value);

            //if (cmbADVERSE_LETTER_REQD.SelectedItem != null && cmbADVERSE_LETTER_REQD.SelectedItem.Value != "")
            //    objProcessInfo.ADVERSE_LETTER_REQD = int.Parse(cmbADVERSE_LETTER_REQD.SelectedItem.Value);


            //if (cmbADD_INT.SelectedItem != null && cmbADD_INT.SelectedItem.Value != "")
            //{
            //    objProcessInfo.ADD_INT = int.Parse(cmbADD_INT.SelectedItem.Value);
            //    //MICHIGAN_MAILERS Itrack #4068 
            //    if (objProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString())
            //        || objProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString()))
            //    {
            //        objProcessInfo.ADD_INT_ID = hidADD_INT_ID.Value;
            //        if (chkSEND_ALL.Checked == true)
            //            objProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.YES);
            //        else
            //            objProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.NO);
            //    }
            //}
            //if (hidOldData.Value == "" || hidOldData.Value == "0")
            //{
            //    if (hidLOB_ID.Value == ((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value == ((int)enumLOB.CYCL).ToString())
            //    {
            //        objProcessInfo.AUTO_ID_CARD = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString());
            //    }
            //}
            //else
            //{
            //    if (hidLOB_ID.Value == ((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value == ((int)enumLOB.CYCL).ToString())
            //    {
            //        if (cmbAUTO_ID_CARD.SelectedItem != null && cmbAUTO_ID_CARD.SelectedItem.Value != "")
            //            objProcessInfo.AUTO_ID_CARD = int.Parse(cmbAUTO_ID_CARD.SelectedItem.Value);
            //    }
            //}
            //if (hidLOB_ID.Value == ((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value == ((int)enumLOB.CYCL).ToString())
            //{
            //    if (txtNO_COPIES.Text.Trim() != "")
            //        objProcessInfo.NO_COPIES = int.Parse(txtNO_COPIES.Text.Trim());
            //    else
            //        objProcessInfo.NO_COPIES = Cms.BusinessLayer.BlCommon.ClsCommon.PRINT_OPTIONS_AUTO_CYCL_NO_OF_COPIES;
            //}

            if (hidLOB_ID.Value != "" && hidLOB_ID.Value != "0")
                objProcessInfo.LOB_ID = int.Parse(hidLOB_ID.Value);
            if (hidSTATE_CODE.Value != "")
                objProcessInfo.STATE_CODE = hidSTATE_CODE.Value;
            if (hidSTATE_ID.Value != "" && hidSTATE_ID.Value != "0")
                objProcessInfo.STATE_ID = int.Parse(hidSTATE_ID.Value);

            objProcessInfo.PROCESS_ID = Convert.ToInt32(hidPROCESS_ID.Value);

            if (hidROW_ID.Value != null && hidROW_ID.Value != "")
                objProcessInfo.ROW_ID = Convert.ToInt32(hidROW_ID.Value);

            return objProcessInfo;
        }

        private void get_Policydetails()
        {
            Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
            DataSet dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value == "" ? "0" : hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value == "" ? "0" : hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value == "" ? "0" : hidPOLICY_VERSION_ID.Value));
            if (dsPolicy != null && dsPolicy.Tables[0].Rows.Count > 0)
            {
                hidUNDERWRITER.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString();

                hidLOB_ID.Value = dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString();

                if (dsPolicy.Tables[0].Rows[0]["STATE_CODE"] != null && dsPolicy.Tables[0].Rows[0]["STATE_CODE"].ToString() != "")
                    hidSTATE_CODE.Value = dsPolicy.Tables[0].Rows[0]["STATE_CODE"].ToString().Trim();
                if (dsPolicy.Tables[0].Rows[0]["STATE_ID"] != null && dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString() != "")
                    hidSTATE_ID.Value = dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString().Trim();
            }

        }

        private bool verifyRule()
        {
            return verifyRule("");
        }

        private bool verifyRule(string CalledFor)
        {
            try
            {
                string strRulesStatus = "0";
                bool valid = false;

                //string strRulesHTML = Cms.BusinessLayer.BlProcess.clsprocess.strHTML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value),out valid,out strRulesStatus);
                if (hidNEW_POLICY_VERSION_ID.Value != "" && hidNEW_POLICY_VERSION_ID.Value != "0")
                {

                    Cms.BusinessLayer.BlProcess.clsprocess objProcess = new clsprocess();

                    objProcess.SystemID = CarrierSystemID;

                    string strRulesHTML = objProcess.strHTML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidNEW_POLICY_VERSION_ID.Value), out valid, out strRulesStatus, "END");

                    if (valid && strRulesStatus == "0") // then commit
                    {
                        valid = true;
                    }
                    else
                    {
                        // show rules msg		
                        // chk here for referred/rejected cases
                        ChkReferedRejCaese(strRulesHTML, CalledFor);
                        myDIV.InnerHtml = strRulesHTML;
                        //myDIV.Visible = true;
                        myDIV.Attributes.Add("style", "display:inline");
                        trRuleHd.Visible = true;
                        valid = false;
                    }
                }
                return valid;
            }
            catch (Exception objExp)
            {
                throw (objExp);
                //return false;
            }
        }

        private void GetPolicy_ProcessDetails()
        {
            DataSet ds = null;
            ds = objCommon.GetPolicy_ProcessDetails(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["POLICY_EFFECTIVE_DATE"] != null)
                {
                    DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["POLICY_EFFECTIVE_DATE"].ToString());
                    if (dt < DateTime.Now)
                        hidPOLICY_EFF_DATE.Value = DateTime.Now.ToShortDateString();
                    else
                        hidPOLICY_EFF_DATE.Value = ds.Tables[0].Rows[0]["POLICY_EFFECTIVE_DATE"].ToString();
                }
                else
                    hidPOLICY_EFF_DATE.Value = DateTime.Now.ToString();

                if (ds.Tables[0].Rows[0]["POLICY_EXPIRATION_DATE"] != null)
                    hidPOLICY_EXP_DATE.Value = ds.Tables[0].Rows[0]["POLICY_EXPIRATION_DATE"].ToString();
                else
                    hidPOLICY_EXP_DATE.Value = DateTime.Now.ToString();
            }
        }

        private void ChkReferedRejCaese(string strRulesHTML, string strCalledFrom)
        {
            try
            {
                XmlDocument objXmlDocument = new XmlDocument();
                strRulesHTML = strRulesHTML.Replace("\t", "");
                strRulesHTML = strRulesHTML.Replace("\r\n", "");
                strRulesHTML = strRulesHTML.Replace("<LINK", "<!-- <LINK");
                strRulesHTML = strRulesHTML.Replace(" rel=\"stylesheet\"> ", "rel=\"stylesheet/\"> -->");
                strRulesHTML = strRulesHTML.Replace("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-16\">", "");
                objXmlDocument.LoadXml("<RULEHTML>" + strRulesHTML + "</RULEHTML>");

                //chk for referred

                XmlNodeList objXmlNodeList = objXmlDocument.GetElementsByTagName("ReferedStatus");
                XmlNodeList objXmlNodeListRej = objXmlDocument.GetElementsByTagName("returnValue");
                if ((objXmlNodeList != null && objXmlNodeList.Count > 0) || (objXmlNodeListRej != null && objXmlNodeListRej.Count > 0))
                {

                    if (objXmlNodeListRej.Item(0).InnerText == "0")
                    {
                        if (strCalledFrom == "COMMIT")
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1218");//"Unable to commit process. Because Policy has been rejected as shown below.";
                        else
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1219");//"Policy has been rejected as shown below.";
                        lblMessage.Visible = true;
                    }
                    else if (objXmlNodeList.Item(0).InnerText == "0")
                    {
                        //btnComitAynway.Visible = true;
                        //btnComitAynway.Visible=false;
                        if (strCalledFrom == "COMMIT")
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1220");//"Unable to commit process. Because Policy has been referred as shown below.";
                        else
                            lblMessage.Text = lblMessage.Text + "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1223");//Policy has been referred as shown below.";
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    if (strCalledFrom == "COMMIT")
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1221");//"Unable to commit process. Please fill the mandatory information as shown below.";
                    else
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1222");//"Please fill the mandatory information as shown below.";
                    lblMessage.Visible = true;
                    //btnComitAynway.Visible = false;
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1224")//"Following error occured. \n"
                    + ex.Message + "\n Please try later.";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        private void ShowErrorMessage(string msg)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language='javascript' type='text/javascript'>alert('" + msg + "');return false;</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=javascript>Init();alert('" + msg + "')</script>");

        }

        private bool getPolicy()
        {
            DataSet ds = null;
            bool Valid = false;
            try
            {
                if (txtPOLICY_ID.Text != "")
                {

                    ds = objCommon.GetPolicyDetails(txtPOLICY_ID.Text, "AUTO_END");
                }
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    hidCUSTOMER_ID.Value = ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();
                    hidPOLICY_ID.Value = ds.Tables[0].Rows[0]["POLICY_ID"].ToString();
                    hidPOLICY_VERSION_ID.Value = ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
                    hidPOLICY_EFF_DATE.Value = ds.Tables[0].Rows[0]["POLICY_EFF_DATE"].ToString();
                    hidPOLICY_EXP_DATE.Value = ds.Tables[0].Rows[0]["POLICY_EXP_DATE"].ToString();
                    hidLOB_ID.Value = ds.Tables[0].Rows[0]["POLICY_LOB"].ToString();
                    Valid = true;
                    hidPOLICY_FLAG.Value = "1";


                    dstemp = GetRiskDetails(hidCUSTOMER_ID.Value, hidPOLICY_ID.Value, hidPOLICY_VERSION_ID.Value, hidLOB_ID.Value);
                    if (dstemp != null && dstemp.Tables.Count > 0 && dstemp.Tables[0].Rows.Count > 0)
                    {
                        DataView dv = new DataView(dstemp.Tables[0]);

                        string[] parrys = { "LOCATION", "RISK_ID" };

                        cmbRISK_NAME.DataSource = dv.ToTable(true, parrys);
                        cmbRISK_NAME.DataTextField = "LOCATION";
                        cmbRISK_NAME.DataValueField = "RISK_ID";
                        cmbRISK_NAME.DataBind();
                        cmbRISK_NAME.Items.Insert(0, "");
                        Session.Add("dsPolicyCovDetils", dstemp);
                    }
                    else
                    {
                        hidPOLICY_FLAG.Value = "";
                        ShowErrorMessage(ClsMessages.FetchGeneralMessage("727"));
                    }
                }
                else
                {
                    cmbENDORSEMENT_TYPE.SelectedValue = "";
                    hidPOLICY_FLAG.Value = "";
                    ShowErrorMessage(ClsMessages.FetchGeneralMessage("727"));
                    lblMessage.Text = "";


                }
            }
            catch (Exception ex) { throw (ex); }
            return Valid;
        }

        private DataSet GetRiskDetails(string customer_id, string policy_id, string policy_version_id, string lob_id)
        {   
            DataSet ds = null;
            objGenInfo = new ClsGeneralInformation();
            ds = objGenInfo.GetPolicyCov_Details(int.Parse(customer_id), int.Parse(policy_id), int.Parse(policy_version_id), int.Parse(lob_id), int.Parse(GetLanguageID()));
            return ds;

        }

        private bool ReduceLimit(string ReducePremium, string RiskID, string Coverage_Code_id)
        {
            bool isReduce = false;
            objGenInfo = new ClsGeneralInformation();
            double dReducePremium = ReducePremium == "" ? 0 : double.Parse(ReducePremium);
            int retval = objGenInfo.UpdatePolicyCovLimit(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidNEW_POLICY_VERSION_ID.Value), int.Parse(hidLOB_ID.Value), int.Parse(RiskID), int.Parse(Coverage_Code_id), dReducePremium);
            if (retval > 0)
            {
                isReduce = true;
            }
            return isReduce;
        }

        private bool CommitProcess()
        {
            bool Commit = false;
            // complete the follow up that was generated by the system at the beginning of the process
            //string strRulesStatus="0";
            bool valid = false;
            valid = this.verifyRule("COMMIT");
            //Saving the information first - record any changes that changed by the user in the process log 
            //btnSave_Click(null, null);

            //if (Saved == false)
            //	return;	//Unable to saved hence can not be committed
            //Saved successfully, hence committing	
            if (valid)
            {
                try
                {

                    //ClsEndorsmentProcess objProcess = new ClsEndorsmentProcess();
                    ///ClsProcessInfo objProcessInfo = objProcess.GetRunningProcess (int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
                    ClsProcessInfo objProcessInfo = getValues();
                    objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS;
                    objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
                    objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
                    objProcessInfo.LOB_ID = int.Parse(hidLOB_ID.Value);


                    //Check EndorsementLog Details before Commiting Process.
                    //if (objProcess.IsEndorsementLogAvailable(objProcessInfo) == true)
                    //{
                    //Commiting the process 
                    if (objProcess.OutOfSequenceEndorsement(objProcessInfo))
                    {
                        //Out of sequence endorsement
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("610");
                        lblMessage.Visible = true;
                        return false;
                    }
                    if (objProcess.CommitProcess(objProcessInfo) == true)
                    {
                        hidPROCESS_ID.Value = objProcessInfo.PROCESS_ID.ToString();
                        //Committed successfully
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("689");
                        //hidDisplayBody.Value = "True";
                        //Hiding the buttons
                        //HideButtons();
                        getPolicy();
                        Commit = true;
                        //Updating the policy top,session and menus
                        //SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
                    }
                    else
                    {
                        //Error occured
                        if (ClsPolicyErrMsg.strMessage != "")
                            lblMessage.Text = ClsPolicyErrMsg.strMessage;
                        else
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1239");//"Unable to commit the process, please try later";
                        //btnCommit.Attributes.Add("style", "display:inline");
                        // btnCommitInProgress.Attributes.Add("style", "display:none");
                        // btnSave.Attributes.Add("style", "disabled:false");

                        //SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
                    }
                    //Updating the policy top,session and menus
                    //cltPolicyTop.CallPageLoad();
                    //SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);

                    lblMessage.Visible = true;
                    //}
                    //else
                    //{
                    //    //Message No Endorsement Available Can Not Commit Process
                    //    lblMessage.Text = ClsMessages.FetchGeneralMessage("1023");
                    //    lblMessage.Visible = true;

                    //}
                }
                catch (Exception objExp)
                {
                    //cltPolicyTop.UseRequestVariables = true;
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1225")//"Following error occurred while committing process. \n"
                        + objExp.Message + "\n Please try later.";
                    lblMessage.Visible = true;
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                    //btnCommit.Attributes.Add("style", "display:inline");
                    //btnCommitInProgress.Attributes.Add("style", "display:none");
                    // btnSave.Attributes.Add("style", "disabled:false");
                }
            }



            return Commit;
        }

        [System.Web.Services.WebMethod(true)]
        public static System.Collections.Generic.Dictionary<string, object> GetPolicy_Coverages(string Param)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            try
            {
                if (HttpContext.Current.Session["dsPolicyCovDetils"] != null)
                {
                    DataSet dsnew = (DataSet)HttpContext.Current.Session["dsPolicyCovDetils"];
                    d = Cms.CmsWeb.support.ClsjQueryCommon.ToJson(GetDataTable(dsnew.Tables[0].Select("RISK_ID=" + Param)));

                }

            }
            catch { }
            return d;
        }

        [System.Web.Services.WebMethod(true)]
        public static System.Collections.Generic.Dictionary<string, object> GetCov_Prm(string RISK_ID, string COVERAGE_CODE_ID)
        {
            try
            {
                if (HttpContext.Current.Session["dsPolicyCovDetils"] != null)
                {
                    DataSet dsnew = (DataSet)HttpContext.Current.Session["dsPolicyCovDetils"];

                    return Cms.CmsWeb.support.ClsjQueryCommon.ToJson(GetDataTable(dsnew.Tables[0].Select("RISK_ID=" + RISK_ID + "AND COVERAGE_CODE_ID=" + COVERAGE_CODE_ID)));
                }
                else
                    return null;
            }
            catch (Exception ex)
            { throw (ex); }
        }



        private static DataTable GetDataTable(DataRow[] dr)
        {
            DataTable dTable = new DataTable();
            try
            {
                if (dr.Length > 0)
                {
                    dTable = dr[0].Table.Clone();
                    foreach (DataRow rw in dr)
                    {
                        dTable.ImportRow(rw);
                    }
                }

            }
            catch { }
            return dTable;
        }

    }
}
