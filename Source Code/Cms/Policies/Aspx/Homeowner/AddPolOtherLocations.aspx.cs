#region Page Info
/******************************************************************************************
<Author					: - Swastika Gaur
<Start Date				: -	6/13/2006 6:22:53 PM
<End Date				: -	
<Description			: - Other Locations (Homeowners)
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
#endregion

#region Libraries
using System;
using System.Resources;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using Cms.Model.Maintenance;
using Cms.Model.Policy;
using Cms.Model.Policy.Homeowners;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon;
#endregion

namespace Cms.Policies.Aspx.Homeowner
{
	/// <summary>
	/// Summary description for AddPolOtherLocations.
	/// </summary>
	public class AddPolOtherLocations : Cms.Policies.policiesbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.TextBox txtLOC_NUM;
		protected System.Web.UI.WebControls.TextBox txtLOC_ADD1;
		protected System.Web.UI.WebControls.TextBox txtLOC_CITY;
		protected System.Web.UI.WebControls.DropDownList cmbLOC_STATE;
		protected System.Web.UI.WebControls.TextBox txtLOC_ZIP;
		protected System.Web.UI.WebControls.DropDownList cmbPHOTO_ATTACHED;
		protected System.Web.UI.WebControls.DropDownList cmbOCCUPIED_BY_INSURED;
		protected System.Web.UI.WebControls.TextBox txtDESCRIPTION;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLocationCode;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_NUM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_ZIP;

		protected System.Web.UI.WebControls.RegularExpressionValidator revLOC_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOC_NUM;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.CustomValidator csvDESCRIPTION;

		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId,strFormSaved;
		protected System.Web.UI.WebControls.Label capLOC_NUM;
		protected System.Web.UI.WebControls.Label capLOC_ADD1;
		protected System.Web.UI.WebControls.Label capLOC_CITY;
		protected System.Web.UI.WebControls.Label capLOC_COUNTY;
		protected System.Web.UI.WebControls.Label capLOC_STATE;
		protected System.Web.UI.WebControls.Label capLOC_ZIP;
		protected System.Web.UI.WebControls.Label capPHOTO_ATTACHED;
		protected System.Web.UI.WebControls.Label capOCCUPIED_BY_INSURED;
		protected System.Web.UI.WebControls.Label capDESCRIPTION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOCATION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIsPostBack;
		//protected System.Web.UI.WebControls.TextBox txtLOC_COUNTY;
        protected System.Web.UI.WebControls.DropDownList cmbLOC_COUNTRY;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidURL;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.WebControls.CustomValidator csvZIP_CODE;
	
		//Defining the business layer class object
		ClsOtherLocation  objOtherLocation ;
		//END:*********** Local variables *************

		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvLOC_NUM.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"208");
			rfvLOC_STATE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"35");
			rfvLOC_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");
			revLOC_ZIP.ValidationExpression	=  aRegExpZip;
			revLOC_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");
			revLOC_NUM.ValidationExpression =  aRegExpInteger;
			revLOC_NUM.ErrorMessage         =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			csvDESCRIPTION.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("813");
		}
		#endregion
		
		#region Set Workflow
		protected void SetWorkFlow()
		{
			if(base.ScreenId == "239_8_0")
			{
//				CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, DWELLING_ID, LOCATION_ID
                myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}
		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			//Added For Itrack Issue #6584.
			Ajax.Utility.RegisterTypeForAjax(typeof(AddPolOtherLocations));  
			
			btnReset.Attributes.Add("onclick","javascript:return formReset();");			
			base.ScreenId="239_8_0";
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass					=	CmsButtonType.Execute;
			btnReset.PermissionString				=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Execute;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass					=	CmsButtonType.Execute;
			btnSave.PermissionString				=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Homeowner.AddPolOtherLocations" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				ClsOtherLocation objOtherLocation = new ClsOtherLocation();
				if(Request.QueryString["LOCATION_ID"]!=null && Request.QueryString["LOCATION_ID"].ToString().Length>0)
				{
					hidLOCATION_ID.Value = Request.QueryString["LOCATION_ID"].ToString();
					hidOldData.Value = ClsOtherLocation.PolGetXmlForPageControls(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), int.Parse(hidLOCATION_ID.Value));
				}
				int NewLocationNumber;
				NewLocationNumber = int.Parse(objOtherLocation.GetPolLocationNumber(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID())));
				if ( NewLocationNumber == -1 )
				{
					lblMessage.Visible = true;
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(this.ScreenId,"521");
					return;
				}
				txtLOC_NUM.Text=NewLocationNumber.ToString();
				hidLocationCode.Value=txtLOC_NUM.Text;
				// Lookup for County
				hidURL.Value = ClsCommon.GetLookupURL();
				imgSelect.Attributes.Add("onclick","javascript:OpenLookupForm();");
				
				SetCaptions();
				SetWorkFlow();
				#region "Loading singleton"
				
				DataTable dt = Cms.CmsWeb.ClsFetcher.State;
				cmbLOC_STATE.DataSource		= dt;
				cmbLOC_STATE.DataTextField	= "State_Name";
				cmbLOC_STATE.DataValueField	= "State_Id";
				cmbLOC_STATE.DataBind();
				cmbLOC_STATE.Items.Insert(0,"");


                //DataTable dtt = Cms.CmsWeb.ClsFetcher.State;
                cmbLOC_COUNTRY.DataSource = dt;
                cmbLOC_COUNTRY.DataTextField = "State_Name";
                cmbLOC_COUNTRY.DataValueField = "State_Id";
                cmbLOC_COUNTRY.DataBind();
                //cmbLOC_COUNTRY.Items.Insert(0, "");
				#endregion//Loading singleton

				FillCombos();
				hidIsPostBack.Value ="NOTPOST";
			}
				
		}//end pageload
		#endregion
		
		#region Fill Combos

		private void FillCombos()
		{
			ClsOtherLocation objOtherLocation = new  ClsOtherLocation();

			cmbPHOTO_ATTACHED.DataSource	 = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbPHOTO_ATTACHED.DataTextField  = "LookupDesc";
			cmbPHOTO_ATTACHED.DataValueField = "LookupID";
			cmbPHOTO_ATTACHED.DataBind();
			cmbPHOTO_ATTACHED.Items.Insert(0,"");

			cmbOCCUPIED_BY_INSURED.DataSource	  = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbOCCUPIED_BY_INSURED.DataTextField  = "LookupDesc";
			cmbOCCUPIED_BY_INSURED.DataValueField = "LookupID";
			cmbOCCUPIED_BY_INSURED.DataBind();
			cmbOCCUPIED_BY_INSURED.Items.Insert(0,"");

		}
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsPolicyOtherLocationsInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsPolicyOtherLocationsInfo objOtherLocationsInfo;
			objOtherLocationsInfo = new ClsPolicyOtherLocationsInfo();

			objOtherLocationsInfo.LOC_NUM=	int.Parse(txtLOC_NUM.Text);
			objOtherLocationsInfo.LOC_ADD1=	txtLOC_ADD1.Text;
			objOtherLocationsInfo.LOC_CITY=	txtLOC_CITY.Text;
			//objOtherLocationsInfo.LOC_COUNTY = txtLOC_COUNTY.Text;
            objOtherLocationsInfo.LOC_COUNTY = cmbLOC_COUNTRY.SelectedValue;

			objOtherLocationsInfo.LOC_STATE=	cmbLOC_STATE.SelectedValue;
			objOtherLocationsInfo.LOC_ZIP=	txtLOC_ZIP.Text;

			if (cmbPHOTO_ATTACHED.SelectedItem != null && cmbPHOTO_ATTACHED.SelectedValue.Trim() != "")
				objOtherLocationsInfo.PHOTO_ATTACHED = int.Parse(cmbPHOTO_ATTACHED.SelectedValue);

			if (cmbOCCUPIED_BY_INSURED.SelectedItem != null && cmbOCCUPIED_BY_INSURED.SelectedValue.Trim() != "")
				objOtherLocationsInfo.OCCUPIED_BY_INSURED =	int.Parse(cmbOCCUPIED_BY_INSURED.SelectedValue);
			
			objOtherLocationsInfo.DESCRIPTION=	txtDESCRIPTION.Text;

			objOtherLocationsInfo.POLICY_ID = int.Parse(GetPolicyID());
			objOtherLocationsInfo.POLICY_VERSION_ID = int.Parse(GetPolicyVersionID());
			objOtherLocationsInfo.CUSTOMER_ID = int.Parse(GetCustomerID());
			objOtherLocationsInfo.DWELLING_ID = int.Parse(Request.QueryString["DWELLING_ID"].ToString());



			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidLOCATION_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objOtherLocationsInfo;
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

		#region "Web Event Handlers  :: SAVE, ACTIVATE/DEACTIVATE"
		/// <summary>
		/// If form is posted back then add entry in database using the BL object
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// //Added For Itrack Issue #6584.
		[Ajax.AjaxMethod()]
		public string AjaxFetchZipForState(int stateID, string ZipID)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.FetchZipForState(stateID,ZipID);
			return result;
			
		}		
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objOtherLocation = new  ClsOtherLocation();

				//Retreiving the form values into model class object
				ClsPolicyOtherLocationsInfo objOtherLocationsInfo = GetFormValue();

				if(hidLOCATION_ID.Value.ToUpper().Equals("NEW")) //save case
				{
					objOtherLocationsInfo.CREATED_BY = int.Parse(GetUserId());
					objOtherLocationsInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objOtherLocation.PolAdd(objOtherLocationsInfo);

					if(intRetVal>0)
					{
						hidLOCATION_ID.Value = objOtherLocationsInfo.LOCATION_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidOldData.Value = ClsOtherLocation.PolGetXmlForPageControls(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), int.Parse(hidLOCATION_ID.Value));
						hidIS_ACTIVE.Value = "Y";
						hidIsPostBack.Value ="NOTPOST";
						btnActivateDeactivate.Text="Deactivate";
						SetWorkFlow();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.FetchGeneralMessage("674");
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
					ClsPolicyOtherLocationsInfo objOldOtherLocationsInfo;
					objOldOtherLocationsInfo = new ClsPolicyOtherLocationsInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldOtherLocationsInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objOtherLocationsInfo.LOCATION_ID = int.Parse(hidLOCATION_ID.Value);
					objOtherLocationsInfo.MODIFIED_BY = int.Parse(GetUserId());
					objOtherLocationsInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//Updating the record using business layer class object
					intRetVal	= objOtherLocation.PolUpdate(objOldOtherLocationsInfo,objOtherLocationsInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						hidOldData.Value = ClsOtherLocation.PolGetXmlForPageControls(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), int.Parse(hidLOCATION_ID.Value));
						//ITrack # 6178 28 July 09 -Manoj Rathore
						if(hidIS_ACTIVE.Value=="Y"){btnActivateDeactivate.Text="Deactivate";}
						else {btnActivateDeactivate.Text="Activate";}					
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("674");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
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
				//	ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objOtherLocation!= null)
					objOtherLocation.Dispose();
			}
		}
		
		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			objOtherLocation = new  ClsOtherLocation();
			ClsPolicyOtherLocationsInfo objOtherLocationsInfo = GetFormValue();

			try
			{
				
				objOtherLocationsInfo.LOCATION_ID = int.Parse(hidLOCATION_ID.Value);
				objOtherLocationsInfo.MODIFIED_BY = int.Parse(GetUserId());//Done for Itrack Issue 6655 on 25 Nov 09
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{		 			
						
					objOtherLocation.PolActivateDeactivate(objOtherLocationsInfo,"N");						
					btnActivateDeactivate.Text="Activate";	
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
                    ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID", "<script>RefreshWebGrid(1," + hidLOCATION_ID.Value + ",true);</script>");
									
				}
				else
				{									
					
					objOtherLocation.PolActivateDeactivate(objOtherLocationsInfo,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
					btnActivateDeactivate.Text="Deactivate";
				}
				hidFormSaved.Value			=	"0";
				hidOldData.Value = ClsOtherLocation.PolGetXmlForPageControls(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), int.Parse(hidLOCATION_ID.Value));
                ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID", "<script>RefreshWebGrid(1," + hidLOCATION_ID.Value + ",true);</script>");
			
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
			}
			finally
			{
				lblMessage.Visible = true;
			
			}
		}
		#endregion

		#region Set Captions
		private void SetCaptions()
		{
			capLOC_NUM.Text						=		objResourceMgr.GetString("txtLOC_NUM");
			capLOC_ADD1.Text					=		objResourceMgr.GetString("txtLOC_ADD1");
			capLOC_CITY.Text					=		objResourceMgr.GetString("txtLOC_CITY");
            capLOC_COUNTY.Text = objResourceMgr.GetString("cmbLOC_COUNTY");
			capLOC_STATE.Text					=		objResourceMgr.GetString("cmbLOC_STATE");
			capLOC_ZIP.Text						=		objResourceMgr.GetString("txtLOC_ZIP");
			capPHOTO_ATTACHED.Text				=		objResourceMgr.GetString("cmbPHOTO_ATTACHED");
			capOCCUPIED_BY_INSURED.Text			=		objResourceMgr.GetString("cmbOCCUPIED_BY_INSURED");
			capDESCRIPTION.Text					=		objResourceMgr.GetString("txtDESCRIPTION");
		}

		#endregion
	
	}
}
