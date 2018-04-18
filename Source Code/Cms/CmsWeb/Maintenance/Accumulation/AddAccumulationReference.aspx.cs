/******************************************************************************************
<Author					: -		Kuldeep Saxena
<Start Date				: -		24/10/2011
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
using System.Resources;
using System.Reflection;
using Cms.Model.Maintenance.Claims;
using Cms.Model.Maintenance.Accumulation;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BLClaims;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Xml;
using Cms.Blcommon;
using Cms.BusinessLayer.BlCommon.Accumulation;
using Cms.BusinessLayer.BlCommon;

namespace CmsWeb.Maintenance
{
    public partial class AddAccumulationReference : Cms.CmsWeb.cmsbase
    {
        #region Page controls declaration
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidExtraCover;

        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.WebControls.Label lblMessage;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDETAIL_TYPE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTRAN_CODE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCRITERIA_DESC;	
        protected System.Web.UI.HtmlControls.HtmlTableRow trbody;
        protected System.Web.UI.HtmlControls.HtmlTableRow trTcode;

        protected System.Web.UI.WebControls.Label capMessages;
        protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkEXPIRATION_DATE;

       

        #endregion

        #region Local form variables
        //creating resource manager object (used for reading field and label mapping)
        //System.Resources.ResourceManager objResourceMgr;
        //Defining the business layer class object
        ClsAccumulationReference objDV;

        private string strRowId = "";
        private string TypeID = ""; 
        private string XmlSchemaFileName = ""; //Added by Agniswar for Singapore Implementation
        private string XmlFullFilePath = ""; //Added by Agniswar for Singapore Implementation

        protected System.Web.UI.WebControls.Label capACC_REF_NO;
        protected System.Web.UI.WebControls.Label capCRITERIA_ID;
        protected System.Web.UI.WebControls.Label capCRITERIA_VALUE;
        protected System.Web.UI.WebControls.Label capTREATY_CAPACITY_LIMIT;
        protected System.Web.UI.WebControls.Label capUSED_LIMIT;
        protected System.Web.UI.WebControls.Label capLOB_ID;

        protected System.Web.UI.WebControls.TextBox txtACC_REF_NO;
       
        protected System.Web.UI.WebControls.TextBox txtCRITERIA_VALUE;
        protected System.Web.UI.WebControls.TextBox txtTREATY_CAPACITY_LIMIT;
        protected System.Web.UI.WebControls.TextBox txtUSED_LIMIT;
        protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATE;
        protected System.Web.UI.WebControls.TextBox txtEXPIRATION_DATE;
        protected System.Web.UI.WebControls.DropDownList cmbCRITERIA_DESC;
        protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvACC_REF_NO;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCRITERIA_DESC;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCRITERIA_VALUE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvTREATY_CAPACITY_LIMIT;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSED_LIMIT;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;
        protected System.Web.UI.WebControls.CustomValidator csvEFFECTIVE_DATE;
        protected System.Web.UI.WebControls.CustomValidator csvEXPIRATION_DATE;

        #endregion
        #region Set Validators ErrorMessages
        /// <summary>
        /// Method to set validation control error masessages.
        /// Parameters: none
        /// Return Type: none
        /// </summary>
        private void SetErrorMessages()
        {
            //rfvDETAIL_TYPE_DESCRIPTION.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
            //rfvTRANSACTION_CODE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
            //rfvAssignedDrAcct.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1347"); //sneha
            //rfvAssignedCrAcct.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1348"); //sneha

            rfvACC_REF_NO.ErrorMessage = "Please enter Accumulation Reference Code";// Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
            rfvCRITERIA_DESC.ErrorMessage = "Please Select Criteria";// Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            rfvCRITERIA_VALUE.ErrorMessage = "Please enter criteria value";
            rfvTREATY_CAPACITY_LIMIT.ErrorMessage = "Please enter valid Treaty Capacity Limit";
            csvEFFECTIVE_DATE.ErrorMessage = "Invalid Effective Date";
            csvEXPIRATION_DATE.ErrorMessage = "Invalid Expiration Date";
        }
        #endregion


        #region PageLoad event


        private void Page_Load(object sender, System.EventArgs e)
        {

            Ajax.Utility.RegisterTypeForAjax(typeof(AddAccumulationReference));

            // btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
            btnReset.Attributes.Add("onclick", "javascript:return ResetForm();");
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            #region Setting Screen ID

            base.ScreenId = "568_0";


            //Added Till here

            #endregion
            hlkEFFECTIVE_DATE.Attributes.Add("onclick", "fPopCalendar(document.MNT_ACCUMULATION_REFERENCE.txtEFFECTIVE_DATE, document.MNT_ACCUMULATION_REFERENCE.txtEFFECTIVE_DATE)");
            hlkEXPIRATION_DATE.Attributes.Add("onclick", "fPopCalendar(document.MNT_ACCUMULATION_REFERENCE.txtEXPIRATION_DATE, document.MNT_ACCUMULATION_REFERENCE.txtEXPIRATION_DATE)");
            lblMessage.Visible = false;
            SetErrorMessages();

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write;  //Permission made Write instead of Execute by Sibin on 23 Oct 08
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;  //Permission made Write instead of Execute by Sibin on 23 Oct 08
            btnSave.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;
            btnActivateDeactivate.Visible = true;



            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            //objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Claims.AddDefaultValueClaims" ,System.Reflection.Assembly.GetExecutingAssembly());

            string strSysID = GetSystemId();
            if (strSysID == "ALBAUAT")
                strSysID = "ALBA";

            XmlSchemaFileName = "AddAccumulationReference.xml";
            XmlFullFilePath = Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/" + XmlSchemaFileName;

           
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ACC_ID"] != null && Request.QueryString["ACC_ID"].ToString().Length > 0)
                    hidTYPE_ID.Value = Request.QueryString["ACC_ID"].ToString();

                //btnSave.Attributes.Add("onClick","return SelectItem();");

                if (Request.QueryString["ACC_ID"] != null && Request.QueryString["ACC_ID"].ToString().Length > 0)
                {
                    hidDETAIL_TYPE_ID.Value = Request.QueryString["ACC_ID"].ToString();
                    hidOldData.Value = ClsAccumulationReference.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);                  
                }

               // SetCaptions();

                //if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "cmsweb/support/PageXML/" + strSysID, XmlSchemaFileName))
                //    setPageControls(Page, Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/" + XmlSchemaFileName);

             

                FillDropDown();
               // Load_criteria();                          


                // To be called after FillDropDown()
                if (Request.QueryString["ACC_ID"] != null && Request.QueryString["ACC_ID"].ToString().Length > 0)
                {
                    XmlDocument objXmlDoc = new XmlDocument();
                    objXmlDoc.LoadXml(hidOldData.Value);

                    hidLOB_ID.Value = objXmlDoc.SelectSingleNode("NewDataSet/Table/LOB_ID").InnerText;

                    PopulateDropDown();                    
                }
            }

        }//end pageload
        #endregion

        #region GetFormValue
        /// <summary>
        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>
        private ClsAccumulationReferenceInfo GetFormValue()
        {
            //Creating the Model object for holding the New data
            ClsAccumulationReferenceInfo objDVInfo = new ClsAccumulationReferenceInfo();
            objDVInfo.ACC_REF_NO=txtACC_REF_NO.Text;
            objDVInfo.LOB_ID =int.Parse(cmbLOB_ID.SelectedValue);
            objDVInfo.CRITERIA_ID=int.Parse(hidCRITERIA_DESC.Value);
            objDVInfo.CRITERIA_VALUE=txtCRITERIA_VALUE.Text;
            objDVInfo.TREATY_CAPACITY_LIMIT = (txtTREATY_CAPACITY_LIMIT.Text == null ? 0.00 : double.Parse(txtTREATY_CAPACITY_LIMIT.Text));
            objDVInfo.USED_LIMIT=(txtUSED_LIMIT.Text==""?0.00:double.Parse(txtUSED_LIMIT.Text));
            objDVInfo.EFFECTIVE_DATE =Convert.ToDateTime(Convert.ToDateTime(txtEFFECTIVE_DATE.Text.Trim()).ToShortDateString());
            objDVInfo.EXPIRATION_DATE =Convert.ToDateTime( Convert.ToDateTime(txtEXPIRATION_DATE.Text.Trim()).ToShortDateString()); 


            if (hidDETAIL_TYPE_ID.Value.ToUpper() != "NEW")
                objDVInfo.ACC_ID = int.Parse(hidDETAIL_TYPE_ID.Value);


            strRowId = hidDETAIL_TYPE_ID.Value;
            return objDVInfo;
        }
        #endregion



        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {

            this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        #region ActivateDeactivate Button
        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {

            ClsAccumulationReference objDefaultValues = new ClsAccumulationReference();
            ClsAccumulationReferenceInfo objDefaultValuesInfo = new ClsAccumulationReferenceInfo();

            try
            {

                base.PopulateModelObject(objDefaultValuesInfo, hidOldData.Value);

                objDefaultValuesInfo.CREATED_BY = int.Parse(GetUserId());
                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {


                    objDefaultValues.ActivateDeactivateDefaultValues(objDefaultValuesInfo, "N");
                    objDefaultValuesInfo.IS_ACTIVE = "N";
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objDefaultValuesInfo.IS_ACTIVE.ToString().Trim());
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
                    hidIS_ACTIVE.Value = "N";




                }
                else
                {

                    objDefaultValues.ActivateDeactivateDefaultValues(objDefaultValuesInfo, "Y");
                    objDefaultValuesInfo.IS_ACTIVE = "Y";
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objDefaultValuesInfo.IS_ACTIVE.ToString().Trim());
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");


                    hidIS_ACTIVE.Value = "Y";

                }


                hidFormSaved.Value = "0";
                GetOldDataXML();

                ClientScript.RegisterStartupScript(this.GetType(), "REFRESHGRID", "<script>RefreshWebGrid(1," + hidDETAIL_TYPE_ID.Value + ");</script>");

            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                ExceptionManager.Publish(ex);
            }
            finally
            {
                lblMessage.Visible = true;
                if (objDefaultValues != null)
                    objDefaultValues.Dispose();

            }
        }



        #endregion




        private void SetPageModelObject(ClsDefaultValuesInfo objDefaultValuesInfo)
        {
            throw new NotImplementedException();
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
                int intRetVal;	//For retreiving the return value of business class save function
                //For retreiving the return value of business class save function
                objDV = new ClsAccumulationReference();
                //Retreiving the form values into model class object
                ClsAccumulationReferenceInfo objDVInfo = GetFormValue();

                if (strRowId.ToUpper().Equals("NEW")) //save case
                {
                    //objDVInfo.TYPE_ID = int.Parse(Request["TYPE_ID"].ToString());
                    objDVInfo.CREATED_BY = int.Parse(GetUserId());
                    objDVInfo.IS_ACTIVE = "Y";

                    //Calling the add method of business layer class
                    intRetVal = objDV.Add(objDVInfo, XmlFullFilePath);

                    if (intRetVal > 0)
                    {
                        hidDETAIL_TYPE_ID.Value = objDVInfo.ACC_ID.ToString();
                        hidIS_ACTIVE.Value = "Y";
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                        hidFormSaved.Value = "1";
                        PopulateDropDown();
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
                } // end save case
                else //UPDATE CASE
                {

                    //Creating the Model object for holding the Old data
                    ClsAccumulationReferenceInfo objOldDVInfo = new ClsAccumulationReferenceInfo();
                    GetOldDataXML();
                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldDVInfo, hidOldData.Value);

                    //Setting those values into the Model object which are not in the page
                   //comment by kuldeep to solve problem in update
                    //objDVInfo.CRITERIA_ID = int.Parse(strRowId);
                    objDVInfo.MODIFIED_BY = int.Parse(GetUserId());

                    //Updating the record using business layer class object
                    intRetVal = objDV.Update(objOldDVInfo, objDVInfo, XmlFullFilePath);
                    if (intRetVal > 0)			// update successfully performed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "31");
                        hidFormSaved.Value = "1";

                        PopulateDropDown();
                        hidOldData.Value = ClsPolicyAccumulationDetails.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
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
            }
            finally
            {
                if (objDV != null)
                    objDV.Dispose();
            }
        }

        #endregion

        private void PopulateDropDown()
        {
            try
            {
                DataSet ds = AjaxFetchCriteria(int.Parse(hidLOB_ID.Value));
                DataTable dt = ds.Tables[0];
                cmbCRITERIA_DESC.DataSource = dt;
                cmbCRITERIA_DESC.DataTextField = "CRITERIA_DESC";
                cmbCRITERIA_DESC.DataValueField = "CRITERIA_ID";
                cmbCRITERIA_DESC.DataBind();
                cmbCRITERIA_DESC.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            { }
            if (hidOldData.Value!="" )
            {
                XmlDocument objXmlDoc = new XmlDocument();
            objXmlDoc.LoadXml(hidOldData.Value);
                hidCRITERIA_DESC.Value = objXmlDoc.SelectSingleNode("NewDataSet/Table/CRITERIA_ID").InnerText;
                cmbCRITERIA_DESC.SelectedValue = hidCRITERIA_DESC.Value;
                rfvCRITERIA_DESC.Validate();
            }
            else
            {
                cmbCRITERIA_DESC.SelectedValue = hidCRITERIA_DESC.Value;
                rfvCRITERIA_DESC.Validate();
            }

        }
        #region SetCaptions
        private void SetCaptions()
        {
            //capDETAIL_TYPE_DESCRIPTION.Text	 =	objResourceMgr.GetString("txtDETAIL_TYPE_DESCRIPTION");
            //lblTRANSACTION_CODE1.Text	 =	objResourceMgr.GetString("lblTRANSACTION_CODE1");
            //capIS_SYSTEM_GENERATED.Text	 =	objResourceMgr.GetString("capIS_SYSTEM_GENERATED");
            //capCreditAccount.Text = objResourceMgr.GetString("capCreditAccount");
            //capDebitAccount.Text = objResourceMgr.GetString("capDebitAccount");
            //capACCOUNTING_POSTING.Text = objResourceMgr.GetString("capACCOUNTING_POSTING");//sneha
            //capTRANSACTION_CATEGORY.Text = objResourceMgr.GetString("capTRANSACTION_CATEGORY");
        }

        #endregion

        #region GetOldDataXML
        private void GetOldDataXML()
        {
            if (hidDETAIL_TYPE_ID.Value != "")
            {
                hidOldData.Value = ClsAccumulationReference.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
            }
            else
                hidOldData.Value = "";

        }
        #endregion

        #region FillDropDown

        private void FillDropDown()
        {       
            cmbLOB_ID.DataSource = Cms.CmsWeb.ClsFetcher.LOBs;
            cmbLOB_ID.DataTextField = "LOB_DESC";
            cmbLOB_ID.DataValueField = "LOB_ID";
            cmbLOB_ID.DataBind();
            cmbLOB_ID.Items.Insert(0, new ListItem("", ""));
            cmbLOB_ID.SelectedIndex = -1;            
        }

        private void Load_criteria()
        {
            // Added by Kuldeep for Accumulation, Singapore Implementation  
            cmbCRITERIA_DESC.DataSource = AjaxFetchCriteria(int.Parse(cmbLOB_ID.SelectedValue));
            cmbCRITERIA_DESC.DataTextField = "CRITERIA_DESC";
            cmbCRITERIA_DESC.DataValueField = "CRITERIA_ID";
            cmbCRITERIA_DESC.DataBind();
            cmbCRITERIA_DESC.Items.Insert(0, new ListItem("", ""));
            

        }
        #endregion
        
        #region AJAX Methods
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFetchCriteria(int LOB_ID)
        {
            DataSet ds = null;

            try
            {
                ds = new DataSet();

                
                try
                {
                    ds= ClsAccumulationReference.GetAccumulationCriteriaList(LOB_ID);
                }
                catch
                { }
                


                return ds;
            }
            catch
            {
                return null;
            }
        }

         [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public string AjaxGenerateAccumulationReferenceCode(int LOB_ID)
        {
            string s = null;

                try
                {
                    s = ClsAccumulationReference.GenerateAccumulationReferenceCode(LOB_ID);
                }
                catch
                { return null;  }

                    return s;
                }

        #endregion
    }
}
