/******************************************************************************************
<Author					: -		Vijay Arora
<Start Date				: -		4/20/2006 5:28:19 PM
<End Date				: -	
<Description			: - 	Class for Event Value Types
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
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Xml;
using Cms.Blcommon;
using Cms.BusinessLayer.BlCommon;



namespace Cms.CmsWeb.Maintenance.Claims
{
	
	public class AddDefaultValueClaims : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.WebControls.TextBox txtDETAIL_TYPE_DESCRIPTION;
		protected System.Web.UI.WebControls.CheckBox chkIS_SYSTEM_GENERATED;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidExtraCover;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;		
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capDETAIL_TYPE_DESCRIPTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDETAIL_TYPE_DESCRIPTION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDETAIL_TYPE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTRAN_CODE;
		protected System.Web.UI.HtmlControls.HtmlTableRow trbody;
		protected System.Web.UI.HtmlControls.HtmlTableRow trTcode;
		protected System.Web.UI.WebControls.Label lblTRANSACTION_CODE1;
		protected System.Web.UI.WebControls.Label capIS_SYSTEM_GENERATED;
		protected System.Web.UI.WebControls.DropDownList cmbTRANSACTION_CODE;
		protected System.Web.UI.WebControls.DropDownList cmbTRANSACTION_CATEGORY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRANSACTION_CODE;
        protected System.Web.UI.WebControls.Label capMessages;

        protected System.Web.UI.WebControls.TextBox txtLOSS_TYPE_CODE;
        protected System.Web.UI.WebControls.DropDownList cmbLOSS_DEPARTMENT;
        protected System.Web.UI.WebControls.DropDownList cmbLOSS_EXTRA_COVER;
       
		#endregion

		#region Local form variables
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		//Defining the business layer class object
		ClsDefaultValues objDV;
		protected System.Web.UI.WebControls.ListBox lstUnassignedDrAcct;
		protected System.Web.UI.WebControls.Button btnSelectAllDrAcct;
		protected System.Web.UI.WebControls.Button btnSelectDrAcct;
		protected System.Web.UI.WebControls.Button btnDeSelectDrAcct;
		protected System.Web.UI.WebControls.Button btnDeSelectAllDrAcct;
		protected System.Web.UI.WebControls.ListBox lstAssignedDrAcct;
		protected System.Web.UI.WebControls.ListBox lstUnassignedCrAcct;
		protected System.Web.UI.WebControls.Button btnSelectCrAcct;
		protected System.Web.UI.WebControls.Button btnDeSelectCrAcct;
		protected System.Web.UI.WebControls.Button btnDeSelectAllCrAcct;
		protected System.Web.UI.WebControls.ListBox lstAssignedCrAcct;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDebit;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAssignedDrAcct;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAssignedCrAcct;
		protected System.Web.UI.WebControls.Button btnSelectAllCrAcct;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnDebitAccount;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnCreditAccount;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnAccountingPosting;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAccountingPosting;
		protected System.Web.UI.HtmlControls.HtmlTableRow trCredit;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDrActList;
		protected System.Web.UI.HtmlControls.HtmlTableRow trCrActList;
        protected System.Web.UI.WebControls.Label capDebitAccount;
        protected System.Web.UI.WebControls.Label capCreditAccount;
        protected System.Web.UI.WebControls.Label capACCOUNTING_POSTING;//sneha
        protected System.Web.UI.WebControls.Label capTRANSACTION_CATEGORY;
		private string strRowId = "";
		private string TypeID = ""; //Added by Sibin on 23 Oct 08
        private string XmlSchemaFileName = ""; //Added by Agniswar for Singapore Implementation
		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvDETAIL_TYPE_DESCRIPTION.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvTRANSACTION_CODE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
            rfvAssignedDrAcct.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1347"); //sneha
            rfvAssignedCrAcct.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1348"); //sneha

		}
		#endregion

        #region AJAX Methods
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFetchExtraCoverages(int iLOB_ID)
        {
            DataSet ds = null;

            try
            {
                ds = new DataSet();

                DataTable dt1 = new DataTable();
                try
                {
                    dt1 = ClsLookup.GetLookupVehicleCoverages(iLOB_ID);
                }
                catch
                { }
                ds.Tables.Add(dt1.Copy());
                ds.Tables[0].TableName = "EXTRA_COVERAGE";

                return ds;
            }
            catch
            {
                return null;
            }
        }

        #endregion

		#region PageLoad event

       
		private void Page_Load(object sender, System.EventArgs e)
		{

            Ajax.Utility.RegisterTypeForAjax(typeof(AddDefaultValueClaims));

            btnReset.Attributes.Add("onclick", "javascript:return ResetForm();");
            capMessages.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
         #region Setting Screen ID

		//Added by Sibin on 20 Oct 08 to add it into Claims - Default Values Permission List

			if(Request.QueryString["TYPE_ID"] != null && Request.QueryString["TYPE_ID"] != "")
			{
				TypeID	=	Request.QueryString["TYPE_ID"];
			}

			
			switch(TypeID)
			{
				case "Catastrophe/Event Types" :
				case "1" :
					base.ScreenId	=	"288_0";
                    XmlSchemaFileName = "AddDefaultValueClaims_1.xml";
					break;

				case "Party Types" :
				case "2" :
					base.ScreenId	=	"289_0";
                    XmlSchemaFileName = "AddDefaultValueClaims_2.xml";
					break;

				case "Claimant Types" :
				case "3" :
					base.ScreenId	=	"290_0";
                    XmlSchemaFileName = "AddDefaultValueClaims_1.xml";
					break;

				case "Expert/Service Provider Types" :
				case "4" :
					base.ScreenId	=	"291_0";
                    XmlSchemaFileName = "AddDefaultValueClaims_1.xml";
					break;

				case "Loss/Sub Types" :
				case "5" :
					base.ScreenId	=	"292_0";
                    XmlSchemaFileName = "AddDefaultValueClaims.xml";
					break;
				
				case "Recovery Type" :
				case "6" :
					base.ScreenId	=	"293_0";
                    XmlSchemaFileName = "AddDefaultValueClaims_1.xml";
					break;

				case "Claim Status" :
				case "7" :
					base.ScreenId	=	"294_0";
                    XmlSchemaFileName = "AddDefaultValueClaims_7.xml";
					break;

				case "Claim Transaction Code" :
				case "8" :
					base.ScreenId	=	"295_0";
                    XmlSchemaFileName = "AddDefaultValueClaims_1.xml";

					break;

				case "Service Types List" :
				case "9" :
					base.ScreenId	=	"296_0";
                    XmlSchemaFileName = "AddDefaultValueClaims_1.xml";
					break;
 
			}
			//Added Till here
			//base.ScreenId="288_0"; Commented by Sibin on 23 Oct 08
			#endregion

			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;  //Permission made Write instead of Execute by Sibin on 23 Oct 08
			btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;  //Permission made Write instead of Execute by Sibin on 23 Oct 08
			btnSave.PermissionString	=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	= CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	= gstrSecurityXML;
            btnActivateDeactivate.Visible = true;
           
           
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Claims.AddDefaultValueClaims" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["TYPE_ID"]!=null && Request.QueryString["TYPE_ID"].ToString().Length>0)
					hidTYPE_ID.Value = Request.QueryString["TYPE_ID"].ToString();

               
                

				//btnSave.Attributes.Add("onClick","return SelectItem();");

				if (hidTYPE_ID.Value != "8")
				{
					lstAssignedCrAcct.Visible=false;
					lstAssignedDrAcct.Visible=false;
					lstUnassignedCrAcct.Visible=false;
					lstUnassignedDrAcct.Visible=false;
					
					btnSelectAllDrAcct.Visible=false;
					btnSelectDrAcct.Visible=false;
					btnDeSelectDrAcct.Visible=false;
					btnDeSelectAllDrAcct.Visible=false;


					btnSelectAllCrAcct.Visible=false;
					btnSelectCrAcct.Visible=false;
					btnDeSelectCrAcct.Visible=false;
					btnDeSelectAllCrAcct.Visible=false;

					rfvAssignedCrAcct.Enabled=false;
					rfvAssignedCrAcct.Visible=false;
					rfvAssignedDrAcct.Enabled=false;
					rfvAssignedDrAcct.Visible=false;

					spnDebitAccount.Visible=false;
					spnCreditAccount.Visible=false;
					spnAccountingPosting.Visible=false;

					trAccountingPosting.Visible=false;
					trDebit.Visible=false;
					trDrActList.Visible=false;
					trCredit.Visible=false;
					trCrActList.Visible=false;
                    

				}

				if(Request.QueryString["DETAIL_TYPE_ID"]!=null && Request.QueryString["DETAIL_TYPE_ID"].ToString().Length>0)
				{
					hidDETAIL_TYPE_ID.Value = Request.QueryString["DETAIL_TYPE_ID"].ToString();
					hidOldData.Value = ClsDefaultValues.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
                    //For Itrack 5322  "AddDefaultValueClaims.xml"
					if(hidOldData.Value!="")
                        hidTRAN_CODE.Value = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("TRANSACTION_CODE",hidOldData.Value.ToString());
					
				}
				
				SetCaptions();

                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";

                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "cmsweb/support/PageXML/" + strSysID, XmlSchemaFileName))
                    setPageControls(Page, Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/" + XmlSchemaFileName);

				FillDropDown();
				
				// To be called after FillDropDown()
				if(Request.QueryString["DETAIL_TYPE_ID"]!=null && Request.QueryString["DETAIL_TYPE_ID"].ToString().Length>0)
				{
					XmlDocument objXmlDoc = new XmlDocument();
					objXmlDoc.LoadXml(hidOldData.Value);
					
					string strDrAccts = objXmlDoc.ChildNodes.Item(0).ChildNodes.Item(0).SelectSingleNode("SelectedDebitLedgers").InnerText;
					string strCrAccts = objXmlDoc.ChildNodes.Item(0).ChildNodes.Item(0).SelectSingleNode("SelectedCreditLedgers").InnerText;

					setListBoxValues(lstUnassignedDrAcct,lstAssignedDrAcct,strDrAccts);
					setListBoxValues(lstUnassignedCrAcct,lstAssignedCrAcct,strCrAccts);

					SelectItem();

                    if (TypeID == "5")
                    {

                        if (objXmlDoc.ChildNodes.Item(0).ChildNodes.Item(0).SelectSingleNode("LOSS_DEPARTMENT") != null)
                        {

                            string strDept = objXmlDoc.ChildNodes.Item(0).ChildNodes.Item(0).SelectSingleNode("LOSS_DEPARTMENT").InnerText;

                            if (strDept != "" || strDept != null)
                            {
                                DataSet ds = AjaxFetchExtraCoverages(int.Parse(strDept));

                                cmbLOSS_EXTRA_COVER.DataSource = ds.Tables[0];
                                cmbLOSS_EXTRA_COVER.DataTextField = "COV_DES";
                                cmbLOSS_EXTRA_COVER.DataValueField = "COV_ID";
                                cmbLOSS_EXTRA_COVER.DataBind();
                                cmbLOSS_EXTRA_COVER.Items.Insert(0, new ListItem("", ""));
                            }
                        }
                    }
				}

                


			}
            FillExtracover();
            
            btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value.ToString()));
		}//end pageload
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsDefaultValuesInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsDefaultValuesInfo objDVInfo = new ClsDefaultValuesInfo();
			objDVInfo.DETAIL_TYPE_DESCRIPTION=	txtDETAIL_TYPE_DESCRIPTION.Text;

			/*if (cmbTRANSACTION_CODE.SelectedValue != "")
			{
				if (int.Parse(cmbTRANSACTION_CODE.SelectedValue) > 0)
					objDVInfo.TRANSACTION_CODE = int.Parse(cmbTRANSACTION_CODE.SelectedValue);
				else
					objDVInfo.TRANSACTION_CODE = 0;
			}
			else
				objDVInfo.TRANSACTION_CODE = 0;*/

			if(hidTRAN_CODE.Value!="")
                objDVInfo.TRANSACTION_CODE = int.Parse(hidTRAN_CODE.Value.ToString().Trim());
            else
				objDVInfo.TRANSACTION_CODE = 0;







			objDVInfo.TRANSACTION_CATEGORY= cmbTRANSACTION_CATEGORY.SelectedItem.Text;

			if(hidDETAIL_TYPE_ID.Value.ToUpper()!="NEW")
				objDVInfo.DETAIL_TYPE_ID = int.Parse(hidDETAIL_TYPE_ID.Value);

			strRowId = hidDETAIL_TYPE_ID.Value;

			if(chkIS_SYSTEM_GENERATED.Checked)
				objDVInfo.IS_SYSTEM_GENERATED = "Y";
			else
				objDVInfo.IS_SYSTEM_GENERATED = "N";

            if (GetLanguageID() == "3")
            {
                objDVInfo.LOSS_TYPE_CODE = txtLOSS_TYPE_CODE.Text;
                //objDVInfo.LOSS_DEPARTMENT = cmbLOSS_DEPARTMENT.SelectedItem.Text.ToString();
                //objDVInfo.LOSS_EXTRA_COVER = hidExtraCover.Value;

                objDVInfo.LOSS_DEPARTMENT = cmbLOSS_DEPARTMENT.SelectedValue;
                objDVInfo.LOSS_EXTRA_COVER = hidExtraCover.Value;
            }
			
			objDVInfo.SelectedDebitLedgers = getSelectedValues(lstAssignedDrAcct);
			objDVInfo.SelectedCreditLedgers = getSelectedValues(lstAssignedCrAcct);
           

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
			this.btnSelectAllDrAcct.Click += new System.EventHandler(this.btnSelectAllDrAcct_Click);
			this.btnSelectDrAcct.Click += new System.EventHandler(this.btnSelectDrAcct_Click);
			this.btnDeSelectDrAcct.Click += new System.EventHandler(this.btnDeSelectDrAcct_Click);
			this.btnDeSelectAllDrAcct.Click += new System.EventHandler(this.btnDeSelectAllDrAcct_Click);
			this.btnSelectAllCrAcct.Click += new System.EventHandler(this.btnSelectAllCrAcct_Click);
			this.btnSelectCrAcct.Click += new System.EventHandler(this.btnSelectCrAcct_Click);
			this.btnDeSelectCrAcct.Click += new System.EventHandler(this.btnDeSelectCrAcct_Click);
			this.btnDeSelectAllCrAcct.Click += new System.EventHandler(this.btnDeSelectAllCrAcct_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

        #region ActivateDeactivate Button
        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {

            ClsDefaultValues objDefaultValues = new ClsDefaultValues();
            ClsDefaultValuesInfo objDefaultValuesInfo = new ClsDefaultValuesInfo();


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

                ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID", "<script>RefreshWebGrid(1," + hidDETAIL_TYPE_ID.Value + ");</script>");

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
				objDV = new ClsDefaultValues();
				//Retreiving the form values into model class object
				ClsDefaultValuesInfo objDVInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objDVInfo.TYPE_ID = int.Parse(Request["TYPE_ID"].ToString());
					objDVInfo.CREATED_BY = int.Parse(GetUserId());
					objDVInfo.IS_ACTIVE = "Y";

					//Calling the add method of business layer class
					intRetVal = objDV.Add(objDVInfo);

					if(intRetVal>0)
					{
						hidDETAIL_TYPE_ID.Value = objDVInfo.DETAIL_TYPE_ID.ToString();
                        hidIS_ACTIVE.Value = "Y";
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value		=	"1";
						hidOldData.Value		=	ClsDefaultValues.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsDefaultValuesInfo objOldDVInfo = new ClsDefaultValuesInfo();
					GetOldDataXML();
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldDVInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objDVInfo.DETAIL_TYPE_ID = int.Parse(strRowId);
					objDVInfo.MODIFIED_BY = int.Parse(GetUserId());

					//Updating the record using business layer class object
					intRetVal	= objDV.Update(objOldDVInfo,objDVInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						hidOldData.Value		=	ClsDefaultValues.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"1";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			}
			finally
			{
				if(objDV!= null)
					objDV.Dispose();
			}
		}

		#endregion

		#region SetCaptions
		private void SetCaptions()
		{
			capDETAIL_TYPE_DESCRIPTION.Text	 =	objResourceMgr.GetString("txtDETAIL_TYPE_DESCRIPTION");
			lblTRANSACTION_CODE1.Text	 =	objResourceMgr.GetString("lblTRANSACTION_CODE1");
			capIS_SYSTEM_GENERATED.Text	 =	objResourceMgr.GetString("capIS_SYSTEM_GENERATED");
            capCreditAccount.Text = objResourceMgr.GetString("capCreditAccount");
            capDebitAccount.Text = objResourceMgr.GetString("capDebitAccount");
            capACCOUNTING_POSTING.Text = objResourceMgr.GetString("capACCOUNTING_POSTING");//sneha
            capTRANSACTION_CATEGORY.Text = objResourceMgr.GetString("capTRANSACTION_CATEGORY");
		}

		#endregion

		#region GetOldDataXML
		private void GetOldDataXML()
		{
			if (hidDETAIL_TYPE_ID.Value != "")
			{
				hidOldData.Value = ClsDefaultValues.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);				
			}
			else
				hidOldData.Value = "";
           
		}
		#endregion

		#region FillDropDown


        private void FillExtracover()
        {
            if (cmbLOSS_DEPARTMENT.SelectedIndex != 0)
            {
            DataSet ds = AjaxFetchExtraCoverages(int.Parse(cmbLOSS_DEPARTMENT.SelectedValue));

            cmbLOSS_EXTRA_COVER.DataSource = ds.Tables[0];
            cmbLOSS_EXTRA_COVER.DataTextField = "COV_DES";
            cmbLOSS_EXTRA_COVER.DataValueField = "COV_ID";
            cmbLOSS_EXTRA_COVER.DataBind();
            cmbLOSS_EXTRA_COVER.Items.Insert(0, new ListItem("", ""));
            }
        }
		private void FillDropDown()
		{
			cmbTRANSACTION_CODE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("TCODE");
			cmbTRANSACTION_CODE.DataTextField="LookupDesc";
			cmbTRANSACTION_CODE.DataValueField="LookupID";
			cmbTRANSACTION_CODE.DataBind();
			cmbTRANSACTION_CODE.Items.Insert(0,"");

			//Transaction Category
			cmbTRANSACTION_CATEGORY.Items.Add("General");
			cmbTRANSACTION_CATEGORY.Items.Add("Reinsurance");


			//Debit Account List
			lstUnassignedDrAcct.DataSource=Cms.BusinessLayer.BLClaims.ClsDefaultValues.GetLedgerAcctsForClaims();
			lstUnassignedDrAcct.DataTextField="DESC_TO_SHOW";
			lstUnassignedDrAcct.DataValueField="ACC_NUMBER";
			lstUnassignedDrAcct.DataBind();

			//Credit Account List
			lstUnassignedCrAcct.DataSource=Cms.BusinessLayer.BLClaims.ClsDefaultValues.GetLedgerAcctsForClaims();
			lstUnassignedCrAcct.DataTextField="DESC_TO_SHOW";
			lstUnassignedCrAcct.DataValueField="ACC_NUMBER";
			lstUnassignedCrAcct.DataBind();

            //Added by Agniswar for Singapore Implementation            
            cmbLOSS_DEPARTMENT.DataSource = Cms.CmsWeb.ClsFetcher.LOBs;
            cmbLOSS_DEPARTMENT.DataTextField = "LOB_DESC";
            cmbLOSS_DEPARTMENT.DataValueField = "LOB_ID";
            cmbLOSS_DEPARTMENT.DataBind();
            cmbLOSS_DEPARTMENT.Items.Insert(0, new ListItem("", ""));


		}
		#endregion

		private void btnSelectDrAcct_Click(object sender, System.EventArgs e)
		{
			if (lstUnassignedDrAcct.SelectedItem != null)
			{	
				lstAssignedDrAcct.ClearSelection();
				lstAssignedDrAcct.Items.Add(lstUnassignedDrAcct.SelectedItem);
				lstUnassignedDrAcct.Items.Remove(lstUnassignedDrAcct.SelectedItem);				
			}
			SelectItem();
		}

		private void btnDeSelectDrAcct_Click(object sender, System.EventArgs e)
		{
			if (lstAssignedDrAcct.SelectedItem != null)
			{	
				lstUnassignedDrAcct.ClearSelection();
				lstUnassignedDrAcct.Items.Add(lstAssignedDrAcct.SelectedItem);
				lstAssignedDrAcct.Items.Remove(lstAssignedDrAcct.SelectedItem);				
			}
			SelectItem();
		}

		private void btnSelectCrAcct_Click(object sender, System.EventArgs e)
		{
			if (lstUnassignedCrAcct.SelectedItem != null)
			{	
				lstAssignedCrAcct.ClearSelection();
				lstAssignedCrAcct.Items.Add(lstUnassignedCrAcct.SelectedItem);
				lstUnassignedCrAcct.Items.Remove(lstUnassignedCrAcct.SelectedItem);				
			}
			SelectItem();
		}

		private void btnDeSelectCrAcct_Click(object sender, System.EventArgs e)
		{
			if (lstAssignedCrAcct.SelectedItem != null)
			{	
				lstUnassignedCrAcct.ClearSelection();
				lstUnassignedCrAcct.Items.Add(lstAssignedCrAcct.SelectedItem);
				lstAssignedCrAcct.Items.Remove(lstAssignedCrAcct.SelectedItem);				
			}
			SelectItem();
		}

		private void TransferListBoxValues (ListBox lstSrc, ListBox lstTarget)
		{
			if (lstSrc.Items.Count > 0)
			{
				for(int ctr=0 ; ctr<lstSrc.Items.Count ; ctr++)
				{
					lstTarget.Items.Add(lstSrc.Items[ctr]); 
				}
				lstSrc.Items.Clear(); 
				hidFormSaved.Value = "3";
			}
			SelectItem();
		}
		
		private void btnSelectAllDrAcct_Click(object sender, System.EventArgs e)
		{
			TransferListBoxValues(lstUnassignedDrAcct,lstAssignedDrAcct);
		}

		private void btnDeSelectAllDrAcct_Click(object sender, System.EventArgs e)
		{
			TransferListBoxValues(lstAssignedDrAcct,lstUnassignedDrAcct);
		}

		private void btnSelectAllCrAcct_Click(object sender, System.EventArgs e)
		{
			TransferListBoxValues(lstUnassignedCrAcct,lstAssignedCrAcct);
		}

		private void btnDeSelectAllCrAcct_Click(object sender, System.EventArgs e)
		{
			TransferListBoxValues(lstAssignedCrAcct,lstUnassignedCrAcct);
		}

		private string getSelectedValues(ListBox lstBx)
		{
			if (lstBx.Items.Count>0)
			{
				string strSelectedIDs="";
				for (int ctr=0; ctr	<lstBx.Items.Count; ctr++)
				{					
					if (strSelectedIDs == "") 
						strSelectedIDs=lstBx.Items[ctr].Value;
					else
						strSelectedIDs=strSelectedIDs + "," + lstBx.Items[ctr].Value;
				}
				return strSelectedIDs;
			}
			else
			{
				return "0";
			}
		}

		private void SelectItem()
		{
			if (lstAssignedCrAcct.Items.Count>0)
			{
				lstAssignedCrAcct.ClearSelection();
				lstAssignedCrAcct.Items[0].Selected=true;
			}

			if (lstAssignedDrAcct.Items.Count>0)
			{
				lstAssignedDrAcct.ClearSelection();
				lstAssignedDrAcct.Items[0].Selected=true;
			}
		}


		private void setListBoxValues(ListBox lstSrc, ListBox lstTarget, string strSelectedIDs)
		{
			if (strSelectedIDs == null || strSelectedIDs == "" || strSelectedIDs == "0")
			{
				return; 
			}
			else
			{	
				foreach (string strID in strSelectedIDs.Split(','))
				{
					ListItem LI = new ListItem();
					LI = lstSrc.Items.FindByValue(strID);					
					if (LI != null)
					{
						lstTarget.Items.Add(LI);
						lstSrc.Items.Remove(LI);
					}
				}
			}
		}
	}
}
