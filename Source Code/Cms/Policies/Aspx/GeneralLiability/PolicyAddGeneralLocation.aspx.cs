/******************************************************************************************
<Author					: - Priya
<Start Date				: -	8/23/2005 12:22:10 PM
<End Date				: -	
<Description			: - Implements functionality for general Location Screen	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Nov 09,2005
<Modified By			: - Sumit Chhabra
<Purpose				: - Event Hanlder for button btnSave for Click event has been added to page initialization code
<Modified Date			: - 16/12/2005
<Modified By			: - Sumit Chhabra
<Purpose				: - Field txtLOC_COUNTY has been commented at resource file as it is not being used anywhere on the form

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
using Cms.BusinessLayer.BlApplication.GeneralLiability;
using Cms.BusinessLayer.BlApplication;
using Cms.Model.Policy.GeneralLiability;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;

namespace Cms.Policies.Aspx.GeneralLiability
{
	/// <summary>
	/// 
	/// </summary>
	public class PolicyAddGeneralLocation : Cms.Policies.policiesbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.TextBox txtLOC_NUM;
		protected System.Web.UI.WebControls.TextBox txtLOC_ADD1;
		protected System.Web.UI.WebControls.TextBox txtLOC_ADD2;
		protected System.Web.UI.WebControls.TextBox txtLOC_CITY;
		protected System.Web.UI.WebControls.DropDownList cmbLOC_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbLOC_TERRITORY;
		protected System.Web.UI.WebControls.TextBox txtLOC_ZIP;
		protected System.Web.UI.WebControls.RangeValidator rngLOC_NUM;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOC_TERRITORY;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.Label capLOC_NUM;	
		protected System.Web.UI.WebControls.Label capLOC_ADD1;
		protected System.Web.UI.WebControls.Label capLOC_ADD2;		
		protected System.Web.UI.WebControls.Label capLOC_CITY;
		protected System.Web.UI.WebControls.Label capLOC_STATE;
		protected System.Web.UI.WebControls.Label capLOC_ZIP;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_NUM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_TERRITORY;
		
		

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		//protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOCATION_ID;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOC_NUM;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
        private string strFormSaved;//strRowId,
		//private int	intLoggedInUserID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hid;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOC_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOC_CITY;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSubmitZip;
		protected System.Web.UI.WebControls.Label lblPull;
		protected Cms.CmsWeb.Controls.CmsButton btnPullCustomerAddress;
		protected System.Web.UI.WebControls.Label capTERRITORY;
		//protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_LOB;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_ADD2;
		protected System.Web.UI.WebControls.DropDownList cmbLOC_COUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_COUNTRY;
		protected System.Web.UI.WebControls.Label capLOC_COUNTRY;
		//Defining the business layer class object
		ClsLocations  ObjLocations ;
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
			rfvLOC_NUM.ErrorMessage			 =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"208");
			rfvLOC_ADD1.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"32");
			rfvLOC_CITY.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"56");
			//rfvLOC_COUNTY.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"136");
			rfvLOC_STATE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"35");
			rfvLOC_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");
			revLOC_NUM.ValidationExpression	=	aRegExpInteger;
			revLOC_NUM.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"163");
			revLOC_ZIP.ValidationExpression	=  aRegExpZip;
			revLOC_CITY.ValidationExpression = aRegExpClientName;
			revLOC_ZIP.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");
			revLOC_CITY.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"39");	
			rfvLOC_TERRITORY.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("732");
			rngLOC_NUM.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			rfvLOC_COUNTRY.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"33");
			//revLOC_COUNTY.ValidationExpression	=aRegExpAlpha;
			//revLOC_COUNTY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"475");
		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:ResetForm('" + Page.Controls[0].ID + "' ); return TerritoryRule();");
			//string url = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
			//imgSelect.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','','COUNTY','','txtLOC_COUNTY','COUNTY','County')");
			
			string url=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
			//imgSelect.Attributes.Add("onclick",@"javascript:OpenLookup('"+url+"','TERR','TERR','','txtLOC_TERRITORY','Territory','Territory','@LOBID='+varLOB);");

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="282_0";
			lblMessage.Visible = false;
			SetErrorMessages();
			

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString =	gstrSecurityXML;

			btnPullCustomerAddress.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnPullCustomerAddress.PermissionString	=	gstrSecurityXML;

			/*base.RequiredPullCustAdd(txtADDRESS1, txtADDRESS2
				, txtCITY, cmbCOUNTRY, cmbSTATE
				, txtZIP_CODE, btnPullCustomerAddress);*/

			base.RequiredPullCustAddWithCountyEx(txtLOC_ADD1,txtLOC_ADD2,txtLOC_CITY
				,null,cmbLOC_STATE,txtLOC_ZIP,null
				,null,btnPullCustomerAddress,"TerritoryRule");

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.GeneralLiability.PolicyAddGeneralLocation" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				cmbLOC_STATE.Attributes.Add("onChange","javascript:return TerritoryRule();");				
				GetQueryStringValues();
				GetOldDataXML();
//				if(Request.QueryString["LOCATION_ID"]!=null && Request.QueryString["LOCATION_ID"].ToString().Length>0)
//				{
//					hidOldData.Value =ClsLocations.GetPolicyGeneralLocationsXml(
//						Convert.ToInt32(hidCustomerID.Value),
//						Convert.ToInt32(hidPolicyID.Value),
//						Convert.ToInt32(hidPolicyVersionID.Value));
//					//hidLOCATION_ID.Value	=	Request.QueryString["LOCATION_ID"];
//					//hidPOL_LOB.Value = ClsVehicleInformation.GetApplicationLOBID(hidCustomerID.Value,hidPolicyID.Value,hidPolicyVersionID.Value).ToString();
//					
//				}
//					//add new button has been pressed
//				else
//				{
//					int NewLocationNumber;
//					//NewLocationNumber=ClsLocations.GetNewLocationNumber("GEN");
//
//					NewLocationNumber=ClsLocations.GetPolicyNewLocationNumber(hidCustomerID.Value,hidPolicyID.Value,hidPolicyVersionID.Value);
//					if(NewLocationNumber>0)
//						txtLOC_NUM.Text=NewLocationNumber.ToString();
//					else
//						txtLOC_NUM.Text="";
//				}
					
				SetCaptions();
				#region set  cntrol
				SetWorkFlow();
				#endregion
				#region "Loading singleton"
				DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
				cmbLOC_COUNTRY.DataSource			= dt;
				cmbLOC_COUNTRY.DataTextField		= COUNTRY_NAME;
				cmbLOC_COUNTRY.DataValueField		= COUNTRY_ID;
				cmbLOC_COUNTRY.DataBind();
				cmbLOC_COUNTRY.SelectedIndex		= 0;

				dt = Cms.CmsWeb.ClsFetcher.ActiveState; 
				cmbLOC_STATE.DataSource		= dt;
				cmbLOC_STATE.DataTextField	= "State_Name";
				cmbLOC_STATE.DataValueField	= "State_Id";
				cmbLOC_STATE.DataBind();
				#endregion//Loading singleton

				#region Filling Territory Drop Down List
				DataTable dtable = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("GLTERR");
				DataView dv = dtable.DefaultView;
				cmbLOC_TERRITORY.DataSource=dv;		
				cmbLOC_TERRITORY.DataTextField="LookupDesc"; 
				cmbLOC_TERRITORY.DataValueField="LookupID";
				cmbLOC_TERRITORY.DataBind();
				cmbLOC_TERRITORY.Items.Insert(0,"");
				#endregion

			}
		}//end pageload
		#endregion
		/// <summary>
		/// validate posted data from form
		/// </summary>
		/// <returns>True if posted data is valid else false</returns>
		private bool doValidationCheck()
		{
			try
			{
				return true;
			}
			catch (Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return false;
			}
		}

		#region GetQueryStringValues
		private void GetQueryStringValues()
		{
//			if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="")
//			{
				hidCustomerID.Value = GetCustomerID();
//			}
//			if(Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString()!="")
//			{
				hidPolicyID.Value = GetPolicyID();
//			}
//			if(Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString()!="")
//			{
				hidPolicyVersionID.Value = GetPolicyVersionID();
//			}
		}
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsLocationsInfo GetFormValue()
		{
		
			//Creating the Model object for holding the New data
			ClsLocationsInfo ObjLocationsInfo = new ClsLocationsInfo();
			ObjLocationsInfo = new ClsLocationsInfo();
			ObjLocationsInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
			ObjLocationsInfo.POLICY_ID = int.Parse(hidPolicyID.Value);
			ObjLocationsInfo.POLICY_VERSION_ID = int.Parse(hidPolicyVersionID.Value);
			ObjLocationsInfo.LOC_NUM=	int.Parse(txtLOC_NUM.Text);
			ObjLocationsInfo.LOC_ADD1=	txtLOC_ADD1.Text;
			ObjLocationsInfo.LOC_ADD2=	txtLOC_ADD2.Text;
			ObjLocationsInfo.LOC_CITY=	txtLOC_CITY.Text;
			//ObjLocationsInfo.LOC_COUNTY=	txtLOC_COUNTY.Text;
			ObjLocationsInfo.LOC_STATE=	cmbLOC_STATE.SelectedValue;
			ObjLocationsInfo.LOC_ZIP=	txtLOC_ZIP.Text;
			ObjLocationsInfo.IS_ACTIVE ="Y";
			ObjLocationsInfo.LOC_COUNTRY = cmbLOC_COUNTRY.SelectedValue;
			// Added by mohit on 15/09.
			ObjLocationsInfo.LOC_TERRITORY=cmbLOC_TERRITORY.SelectedValue; 

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			//strRowId		=	hidLOCATION_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return ObjLocationsInfo;
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

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
				int intRetVal=0;	//For retreiving the return value of business class save function
				
				ObjLocations = new  ClsLocations();

				//Retreiving the form values into model class object
				ClsLocationsInfo ObjLocationsInfo = GetFormValue();

				if(hidOldData.Value=="" || hidOldData.Value=="0")
				{
					ObjLocationsInfo.CREATED_BY = int.Parse(GetUserId());
					ObjLocationsInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = ObjLocations.AddPolicyGenLocations(ObjLocationsInfo);

					if(intRetVal>0)
					{
						//hidLOCATION_ID.Value = ObjLocationsInfo.LOCATION_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						GetOldDataXML();
						hidIS_ACTIVE.Value = "Y";
						SetWorkFlow();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"674");
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
					ClsLocationsInfo objOldLocationsInfo;
					objOldLocationsInfo = new ClsLocationsInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldLocationsInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					//ObjLocationsInfo.LOCATION_ID = int.Parse(strRowId);
					ObjLocationsInfo.MODIFIED_BY = int.Parse(GetUserId());
					ObjLocationsInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//Updating the record using business layer class object
					intRetVal	= ObjLocations.UpdatePolicyGenLocation(objOldLocationsInfo,ObjLocationsInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						//hidLOCATION_ID.Value=ObjLocationsInfo.LOCATION_ID.ToString();
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"674");
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
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(ObjLocations!= null)
					ObjLocations.Dispose();
			}
		}

		
		#endregion
		private void SetCaptions()
		{
			capLOC_NUM.Text						=		objResourceMgr.GetString("txtLOC_NUM");
			capLOC_ADD1.Text						=		objResourceMgr.GetString("txtLOC_ADD1");
			capLOC_ADD2.Text						=		objResourceMgr.GetString("txtLOC_ADD2");
			capLOC_CITY.Text						=		objResourceMgr.GetString("txtLOC_CITY");
			//capLOC_COUNTY.Text						=		objResourceMgr.GetString("txtLOC_COUNTY");
			capLOC_STATE.Text						=		objResourceMgr.GetString("cmbLOC_STATE");
			capLOC_ZIP.Text						=		objResourceMgr.GetString("txtLOC_ZIP");
			capTERRITORY.Text					=        objResourceMgr.GetString("cmbLOC_TERRITORY");     
			capLOC_COUNTRY.Text				= objResourceMgr.GetString("cmbLOC_COUNTRY");

		}
		private void GetOldDataXML()
		{
			hidOldData.Value =ClsLocations.GetPolicyGeneralLocationsXml(
				Convert.ToInt32(hidCustomerID.Value),
				Convert.ToInt32(hidPolicyID.Value),
				Convert.ToInt32(hidPolicyVersionID.Value));
			//hidPOL_LOB.Value = ClsVehicleInformation.GetApplicationLOBID(hidCustomerID.Value,hidPolicyID.Value,hidPolicyVersionID.Value).ToString();
		}

		private void SetWorkFlow()
		{
			if(base.ScreenId == "282_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCustomerID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPolicyID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPolicyVersionID.Value);
//				if(hidLOCATION_ID.Value!="0" && hidLOCATION_ID.Value.Trim() != "")
//				{
//					myWorkFlow.AddKeyValue("LOCATION_ID",hidLOCATION_ID.Value);
//				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();		
				myWorkFlow.WorkflowModule="POL";
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}		
	}
}
