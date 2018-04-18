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
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication.HomeOwners;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Policy.Homeowners;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlCommon.Accumulation;

namespace Cms.Policies.Aspx.Homeowner
{
    public partial class PolicyAddAccumulationDetails : Cms.Policies.policiesbase
    {
        System.Resources.ResourceManager objResourceMgr;
        string strRowId = "";
        ClsPolicyAccumulationDetails objDV;
        private string XmlSchemaFileName = "";
        private string XmlFullFilePath = "";
        string strSysID="";
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = ClsCommon.GetLookupURL();

            base.ScreenId = "239_9_0";
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;
          
            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Homeowner.PolicyAddAccumulationDetails", System.Reflection.Assembly.GetExecutingAssembly());
            strSysID = GetSystemId();
            XmlSchemaFileName = "PolicyAddAccumulationDetails.xml";
            XmlFullFilePath = Request.PhysicalApplicationPath + "/Policies/support/PageXml/" + strSysID + "/" + XmlSchemaFileName;
            if(!IsPostBack)
            {
                //imgBusinessType.Attributes.Add("onclick", "javascript:OpenLookup('" + url + "','BUSINESS_TYPE_ID','TYPE_DESCRIPTION','hidCUSTOMER_BUSINESS_TYPE','txtCUSTOMER_BUSINESS_TYPE_NAME','BusinessTypeSingapore','Test','');return false;");
                //imgBusinessType.Attributes.Add("onclick", "javascript:OpenLookup('" + url + "','ACC_ID','ACC_REF_NO','hidDETAIL_TYPE_ID','txtAcc_ref','AccumulationReference','Accumulation Reference Numbers','');return false;");
                imgBusinessType.Attributes.Add("onclick", "javascript:OpenLookupWithFunction('" + url + "','ACC_ID','ACC_REF_NO','hidDETAIL_TYPE_ID','txtAcc_ref','AccumulationReference','Accumulation Reference Numbers','','PostFromLookup()');");
                SetAccumulationReferenceValuesAfterPolicyCommit(); 
            }
            else // Added by Ruchika Chauhan on 5-March-2012 for TFS Bug # 3635                
			{
                
				if (hidLOOKUP.Value == "Y")
				{
                    SetAccumulationReferenceValues();
					hidLOOKUP.Value="";
				}
                
			}
        }

        #region GetFormValue
        /// <summary>
        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>
        private ClsPolicyAccumulationDetailsInfo GetFormValue()
        {
            //Creating the Model object for holding the New data
            ClsPolicyAccumulationDetailsInfo objDVInfo = new ClsPolicyAccumulationDetailsInfo();
            objDVInfo.ACCUMULATION_CODE = txtAcc_code.Text;
            objDVInfo.ACC_REF_NO = txtAcc_ref.Text;
            objDVInfo.TOTAL_NO_OF_POLICIES = (txtTot_policies.Text == "" ? 0 : int.Parse(txtTot_policies.Text));
            objDVInfo.OWN_RETENTION_LIMIT = (txtOwn_ret_limit.Text == "" ? 0.00 : double.Parse(txtOwn_ret_limit.Text));
            objDVInfo.TREATY_CAPACITY_LIMIT = (txtTreaty_cap_limit.Text == "" ? 0.00 : double.Parse(txtTreaty_cap_limit.Text));
            objDVInfo.ACCUMULATION_LIMIT_AVAILABLE = (txtAcc_limit_avl.Text == "" ? 0.00 : double.Parse(txtAcc_limit_avl.Text));
            objDVInfo.TOTAL_SUM_INSURED = (txtTot_sum_insured.Text == "" ? 0.00 : double.Parse(txtTot_sum_insured.Text));            
            objDVInfo.FACULTATIVE_RI = (txtFacultative_RI.Text == "" ? 0.00 : double.Parse(txtFacultative_RI.Text));
            objDVInfo.GROSS_RETAINED_SUM_INSURED = (txtGross_ret_SI.Text == "" ? 0.00 : double.Parse(txtGross_ret_SI.Text));
            objDVInfo.OWN_RETENTION= (txtOwn_ret.Text == "" ? 0.00 : double.Parse(txtOwn_ret.Text));
            objDVInfo.QUOTA_SHARE = (txtQuota_share.Text == "" ? 0.00 : double.Parse(txtQuota_share.Text));
            objDVInfo.FIRST_SURPLUS = (txtIst_Surplus.Text == "" ? 0.00 : double.Parse(txtIst_Surplus.Text));
            objDVInfo.OWN_ABSOLUTE_NET_RETENSTION = (txtOwn_abs_net_ret.Text == "" ? 0.00 : double.Parse(txtOwn_abs_net_ret.Text));

           // if (hidDETAIL_TYPE_ID.Value.ToUpper() != "NEW")
                //objDVInfo.ACCUMULATION_ID= int.Parse(hidDETAIL_TYPE_ID.Value);


            strRowId = hidDETAIL_TYPE_ID.Value;


            return objDVInfo;
        }

        //Added by Ruchika Chauhan on 5-March-2012 for TFS Bug # 3635
        private void SetAccumulationReferenceValues()
        {
            hidLOOKUP.Value = ""; //Clearing the lookup field
            this.hidHOLDER_NAME.Value = this.txtAcc_ref.Text.Trim();

            DataTable dtHolder;
            DataRow rdHolderDetails;

            string TotalPolicies = "", AccCode = "", TotalSumInsured = "", AvailableLimit="";            

            ClsPolicyAccumulationDetails objHolder = new ClsPolicyAccumulationDetails();
            dtHolder = objHolder.FillAccumulationReference(int.Parse(hidDETAIL_TYPE_ID.Value), txtAcc_ref.Text, int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), out TotalPolicies, out TotalSumInsured, out AvailableLimit).Tables[0];
            if (dtHolder.Rows.Count > 0)
            {
                rdHolderDetails = dtHolder.Rows[0];
                txtTreaty_cap_limit.Text = (rdHolderDetails["TREATY_CAPACITY_LIMIT"].ToString() == null) ? "" : rdHolderDetails["TREATY_CAPACITY_LIMIT"].ToString();
                txtOwn_ret_limit.Text = (rdHolderDetails["CRITERIA_VALUE"].ToString() == null) ? "" : rdHolderDetails["CRITERIA_VALUE"].ToString();                

                txtTot_policies.Text = (TotalPolicies == "") ? "" : TotalPolicies;
                txtTot_sum_insured.Text = (TotalSumInsured == "") ? "" : TotalSumInsured;
                txtAcc_limit_avl.Text = (AvailableLimit == "") ? "" : AvailableLimit;

                //Added by kuldeep on 13-march-2012 for grid(List of Accumulations)
                BindGrid();
            }
            else
            {
                txtTreaty_cap_limit.Text="";
                txtOwn_ret_limit.Text="";
                txtAcc_limit_avl.Text="";
                txtTot_policies.Text="";
                txtTot_sum_insured.Text="";
            }

            AccCode = objHolder.FillAccumulationCode(int.Parse(hidDETAIL_TYPE_ID.Value)).Tables[0].Rows[0][0].ToString();
            txtAcc_code.Text = (AccCode == "") ? "" : AccCode;          
            hidFormSaved.Value = "2";
        }

        //added by kuldeep on 13-march-2012 for grid
        private void BindGrid()
        {
            DataTable dtAcc_policy_details;
            ClsPolicyAccumulationDetails objHolder = new ClsPolicyAccumulationDetails();
            dtAcc_policy_details = objHolder.GetAcccumulatedPolicyDetails(txtAcc_ref.Text).Tables[0];
            grdAccumulated_Policy_details.DataSource = dtAcc_policy_details;
            grdAccumulated_Policy_details.DataBind();
        }
        //Added by Ruchika Chauhan on 13-March-2012 for TFS Bug # 3635
        private void SetAccumulationReferenceValuesAfterPolicyCommit()
        {
            hidLOOKUP.Value = ""; //Clearing the lookup field
            this.hidHOLDER_NAME.Value = this.txtAcc_ref.Text.Trim();

            DataTable dtHolder;
            DataRow rdHolderDetails;
            //string TotalPolicies = "", AccCode = "", TotalSumInsured = "";

            ClsPolicyAccumulationDetails objHolder = new ClsPolicyAccumulationDetails();
            dtHolder = objHolder.FillAccumulationReferenceAfterPolicyCommit(int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), int.Parse(GetCustomerID())).Tables[0];
            if (dtHolder.Rows.Count > 0)
            {
                rdHolderDetails = dtHolder.Rows[0];

                txtAcc_ref.Text = (rdHolderDetails["ACC_REF_NO"].ToString() == "")? "":rdHolderDetails["ACC_REF_NO"].ToString();
                txtAcc_code.Text = (rdHolderDetails["ACCUMULATION_CODE"].ToString() == "")? "":rdHolderDetails["ACCUMULATION_CODE"].ToString();
                txtTot_policies.Text = (rdHolderDetails["TOTAL_NO_OF_POLICIES"].ToString() == "")? "":rdHolderDetails["TOTAL_NO_OF_POLICIES"].ToString();
                txtOwn_ret_limit.Text = (rdHolderDetails["OWN_RETENTION_LIMIT"].ToString() == "")? "":rdHolderDetails["OWN_RETENTION_LIMIT"].ToString();
                txtTreaty_cap_limit.Text = (rdHolderDetails["TREATY_CAPACITY_LIMIT"].ToString() == "")? "":rdHolderDetails["TREATY_CAPACITY_LIMIT"].ToString();
                txtAcc_limit_avl.Text = (rdHolderDetails["ACCUMULATION_LIMIT_AVAILABLE"].ToString() == "")? "":rdHolderDetails["ACCUMULATION_LIMIT_AVAILABLE"].ToString();
                txtTot_sum_insured.Text = (rdHolderDetails["TOTAL_SUM_INSURED"].ToString() == "")? "":rdHolderDetails["TOTAL_SUM_INSURED"].ToString();
                txtFacultative_RI.Text =(rdHolderDetails["FACULTATIVE_RI"].ToString() == "")? "":rdHolderDetails["FACULTATIVE_RI"].ToString();              
                txtGross_ret_SI.Text = (rdHolderDetails["GROSS_RETAINED_SUM_INSURED"].ToString() == "") ? "" : rdHolderDetails["GROSS_RETAINED_SUM_INSURED"].ToString();
                txtOwn_ret.Text = (rdHolderDetails["OWN_RETENTION"].ToString() == "") ? "" : rdHolderDetails["OWN_RETENTION"].ToString();
                txtIst_Surplus.Text = (rdHolderDetails["FIRST_SURPLUS"].ToString() == "") ? "" : rdHolderDetails["FIRST_SURPLUS"].ToString();
                txtQuota_share.Text = (rdHolderDetails["QUOTA_SHARE"].ToString() == "") ? "" : rdHolderDetails["QUOTA_SHARE"].ToString();
                txtOwn_abs_net_ret.Text = (rdHolderDetails["OWN_ABSOLUTE_NET_RETENSTION"].ToString() == "") ? "" : rdHolderDetails["OWN_ABSOLUTE_NET_RETENSTION"].ToString();

                //added by kuldeep on 13-march-2012 for grid
                BindGrid();
            }
            hidFormSaved.Value = "2";
        }
        private void btnSave_Click(object sender, System.EventArgs e)
        {

            try
            {
                int intRetVal;	//For retreiving the return value of business class save function
                //For retreiving the return value of business class save function
                objDV = new ClsPolicyAccumulationDetails();
                //Retreiving the form values into model class object
                ClsPolicyAccumulationDetailsInfo objDVInfo = GetFormValue();

                //if (strRowId.ToUpper().Equals("NEW")) //save case
                //{
                    //objDVInfo.TYPE_ID = int.Parse(Request["TYPE_ID"].ToString());
                    objDVInfo.CREATED_BY = int.Parse(GetUserId());
                    objDVInfo.IS_ACTIVE = "Y";


                    //Added by Ruchika Chauhan on 6-March-2012 for TFS Bug # 3635
                    objDVInfo.CUSTOMER_ID = int.Parse(GetCustomerID());
                    objDVInfo.POLICY_ID = int.Parse(GetPolicyID());
                    objDVInfo.POLICY_VERSION_ID = int.Parse(GetPolicyVersionID());
                    //Added till here

                    //Calling the add method of business layer class
                    intRetVal = objDV.Add(objDVInfo, XmlFullFilePath);

                    if (intRetVal > 0)
                    {
                        hidDETAIL_TYPE_ID.Value = objDVInfo.ACCUMULATION_ID.ToString();
                        hidIS_ACTIVE.Value = "Y";
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                        hidFormSaved.Value = "1";
                        
                        hidOldData.Value = ClsAccumulationReference.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);


                    }
                    else if (intRetVal == -1)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                //} // end save case
                //else //UPDATE CASE
                //{

                //    //Creating the Model object for holding the Old data
                //    ClsPolicyAccumulationDetailsInfo objOldDVInfo = new ClsPolicyAccumulationDetailsInfo();
                //    GetOldDataXML();
                //    //Setting  the Old Page details(XML File containing old details) into the Model Object
                //    base.PopulateModelObject(objOldDVInfo, hidOldData.Value);

                //    //Setting those values into the Model object which are not in the page
                //    //comment by kuldeep to solve problem in update
                //    //objDVInfo.CRITERIA_ID = int.Parse(strRowId);
                //    objDVInfo.MODIFIED_BY = int.Parse(GetUserId());

                //    //Updating the record using business layer class object
                //    intRetVal = objDV.Update(objOldDVInfo, objDVInfo, XmlFullFilePath);
                //    if (intRetVal > 0)			// update successfully performed
                //    {
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "31");
                //        hidFormSaved.Value = "1";

                //        //PopulateDropDown();
                //        hidOldData.Value = ClsPolicyAccumulationDetails.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
                //    }
                //    else if (intRetVal == -1)	// Duplicate code exist, update failed
                //    {
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "18");
                //        hidFormSaved.Value = "1";
                //    }
                //    else
                //    {
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "20");
                //        hidFormSaved.Value = "1";
                //    }
                //    lblMessage.Visible = true;
                //}
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
                if (objDV != null)
                    objDV.Dispose();
            }
        }
        private void GetOldDataXML()
        {
            if (hidDETAIL_TYPE_ID.Value != "")
            {
                hidOldData.Value = ClsPolicyAccumulationDetails.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
            }
            else
                hidOldData.Value = "";

        }
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {

          
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
    }

}