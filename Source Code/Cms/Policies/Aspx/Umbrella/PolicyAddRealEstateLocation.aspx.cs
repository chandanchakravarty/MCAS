/******************************************************************************************
	<Author					: -> Ravindra Kumar Gupta
	<Start Date				: -> 03-21-2006
	<End Date				: ->
	<Description			: -> Page to add umbrella Real Estate Location
	<Review Date			: ->
	<Reviewed By			: ->
	
	Modification History	:
	<Modified By			: -> Swastika Gaur
	<Modified Date			: -> 28th Apr'06
	<Purpose				: -> Added Delete button functionality.,GetFormValue()
******************************************************************************************/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlApplication;
using Cms.Model.Policy.Umbrella ;
using Cms.BusinessLayer.BlCommon;


namespace Cms.Policies.Aspx.Umbrella
{
	/// <summary>
	/// Summary description for PolicyAddRealEstateLocation.
	/// </summary>
	public class PolicyAddRealEstateLocation :Cms.Policies.policiesbase 
	{

		#region Page Control Declaration
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label capLOCATION_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtLOCATION_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOCATION_NUMBER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOCATION_NUMBER;
		protected System.Web.UI.WebControls.Label capCLIENT_LOCATION_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCLIENT_LOCATION_NUMBER;
		protected System.Web.UI.WebControls.Label capPullCustomerAddress;
		protected Cms.CmsWeb.Controls.CmsButton btnPullCustomerAddress;
		protected System.Web.UI.WebControls.Label capADDRESS_1;
		protected System.Web.UI.WebControls.TextBox txtADDRESS_1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvADDRESS1;
		protected System.Web.UI.WebControls.Label capADDRESS_2;
		protected System.Web.UI.WebControls.TextBox txtADDRESS_2;
		protected System.Web.UI.WebControls.Label capCITY;
		protected System.Web.UI.WebControls.TextBox txtCITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCITY;
		protected System.Web.UI.WebControls.Label capSTATE;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfv_STATE;
		protected System.Web.UI.WebControls.Label capZIPCODE;
		protected System.Web.UI.WebControls.TextBox txtZIPCODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvZIPCODE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revZIPCODE;
		protected System.Web.UI.WebControls.Label capCOUNTY;
		protected System.Web.UI.WebControls.TextBox txtCOUNTY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOUNTY;
		protected System.Web.UI.WebControls.Label capPHONE_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtPHONE_NUMBER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPHONE_NUMBER;
		protected System.Web.UI.WebControls.Label capFAX_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtFAX_NUMBER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFAX_NUMBER;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOCATION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckZipSubmit;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_LOB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDWELLING_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		string oldXML;
		private string strRowId, strFormSaved;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;

		protected System.Web.UI.WebControls.Label capOCCUPIED_BY;
		protected System.Web.UI.WebControls.Label capNUM_FAMILIES;
		protected System.Web.UI.WebControls.Label capBUSS_FARM_PURSUITS;
		protected System.Web.UI.WebControls.Label capBUSS_FARM_PURSUITS_DESC;
		protected System.Web.UI.WebControls.Label capPERS_INJ_COV_82;
		protected System.Web.UI.WebControls.Label capLOC_EXCLUDED;

		protected System.Web.UI.WebControls.TextBox txtNUM_FAMILIES;
		protected System.Web.UI.WebControls.TextBox txtBUSS_FARM_PURSUITS_DESC;		

		protected System.Web.UI.WebControls.DropDownList cmbOCCUPIED_BY;
		protected System.Web.UI.WebControls.DropDownList cmbBUSS_FARM_PURSUITS;
		protected System.Web.UI.WebControls.DropDownList cmbPERS_INJ_COV_82;
		protected System.Web.UI.WebControls.DropDownList cmbLOC_EXCLUDED;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBUSS_FARM_PURSUITS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBUSS_FARM_PURSUITS_DESC;
		protected System.Web.UI.WebControls.RangeValidator rngNUM_FAMILIES;
		protected int BUSS_PURSUIT_OTHER_BUSINESS = 11955;
		protected System.Web.UI.WebControls.CustomValidator csvZIPCODE;
		protected System.Web.UI.WebControls.Label capOTHER_POLICY;
		protected System.Web.UI.WebControls.DropDownList cmbOTHER_POLICY;
		public string WebServiceURL;
		#endregion
	
		#region PageLoad event

		private void Page_Load(object sender, System.EventArgs e)
		{
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");   
			Ajax.Utility.RegisterTypeForAjax(typeof(PolicyAddRealEstateLocation)); 
			WebServiceURL = ClsCommon.WebServiceURL;
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			//btnActivateDeactivate.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			txtZIPCODE.Attributes.Add("OnBlur","javascript:DisableValidators();GetTerritory();");
			
			base.ScreenId="274_0";
			lblMessage.Visible = false;

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

		
			btnSave.CmsButtonClass	=CmsButtonType.Write;
			btnSave.PermissionString =	gstrSecurityXML;
			
			btnPullCustomerAddress.CmsButtonClass	=CmsButtonType.Write;
			btnPullCustomerAddress.PermissionString =	gstrSecurityXML;
			
			btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
			btnActivateDeactivate.PermissionString = gstrSecurityXML;

			btnDelete.CmsButtonClass	= CmsButtonType.Delete;
			btnDelete.PermissionString	= gstrSecurityXML;

			base.RequiredPullCustAddWithCounty(txtADDRESS_1, txtADDRESS_2, txtCITY
				, null, cmbSTATE, txtZIPCODE,txtCOUNTY,null, btnPullCustomerAddress);
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			if(!Page.IsPostBack)
			{
				
				GetQueryString();
				
				SetCaptions();
				
				PopulateDropDown();
				
				SetWorkFlow();
				cmbBUSS_FARM_PURSUITS.Attributes.Add("onChange","javascript:cmbBUSS_FARM_PURSUITS_Change();");
				SetValidators();
				if(hidLOCATION_ID.Value == "0" || hidLOCATION_ID.Value == "" )
				{
					txtLOCATION_NUMBER.Text=ClsRealEstateLocation.GetNewPolicyLocationNumber(
						Convert.ToInt32 (hidCUSTOMER_ID.Value),Convert.ToInt32 (hidPOLICY_ID .Value),
						Convert.ToInt32(hidPOLICY_VERSION_ID.Value )).ToString();
				
				}
				else
				{
					LoadData ();
				}
			
				string rootPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetApplicationPath();
				string url = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
 
				imgSelect.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','COUNTY','COUNTY','','txtCOUNTY','COUNTY','County')");

							
			}
		}
		#endregion

		
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Policy.Umbrella.ClsRealEstateLocationInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Policy.Umbrella.ClsRealEstateLocationInfo objInfo;
			objInfo = new ClsRealEstateLocationInfo();

			objInfo.CUSTOMER_ID=int.Parse(hidCUSTOMER_ID.Value);
			objInfo.POLICY_ID =int.Parse(hidPOLICY_ID .Value);
			objInfo.POLICY_VERSION_ID =int.Parse(hidPOLICY_VERSION_ID .Value);
		
			objInfo.LOCATION_NUMBER =	int.Parse(txtLOCATION_NUMBER.Text);

			if(txtCLIENT_LOCATION_NUMBER.Text != "")
				objInfo.CLIENT_LOCATION_NUMBER=txtCLIENT_LOCATION_NUMBER.Text;
			
			objInfo.ADDRESS_1=	txtADDRESS_1.Text;
			objInfo.ADDRESS_2=	txtADDRESS_2.Text;
			objInfo.CITY=	txtCITY.Text;
			objInfo.COUNTY=	txtCOUNTY.Text;
			if (cmbSTATE.SelectedValue.Trim() != "")
			{
				objInfo.STATE=	int.Parse(cmbSTATE.SelectedValue);
			}
			objInfo.ZIPCODE=	txtZIPCODE.Text;
			objInfo.PHONE_NUMBER=	txtPHONE_NUMBER.Text;
			objInfo.FAX_NUMBER=	txtFAX_NUMBER.Text;
			if(cmbOCCUPIED_BY.SelectedItem!=null && cmbOCCUPIED_BY.SelectedItem.Value!="")
				objInfo.OCCUPIED_BY = int.Parse(cmbOCCUPIED_BY.SelectedItem.Value);
			if(txtNUM_FAMILIES.Text.Trim()!="")
				objInfo.NUM_FAMILIES = int.Parse(txtNUM_FAMILIES.Text.Trim());
			if(cmbBUSS_FARM_PURSUITS.SelectedItem!=null && cmbBUSS_FARM_PURSUITS.SelectedItem.Value!="")
				objInfo.BUSS_FARM_PURSUITS = int.Parse(cmbBUSS_FARM_PURSUITS.SelectedItem.Value);
			if(objInfo.BUSS_FARM_PURSUITS==BUSS_PURSUIT_OTHER_BUSINESS && txtBUSS_FARM_PURSUITS_DESC.Text.Trim()!="")
				objInfo.BUSS_FARM_PURSUITS_DESC = txtBUSS_FARM_PURSUITS_DESC.Text.Trim();
			if(cmbPERS_INJ_COV_82.SelectedItem!=null && cmbPERS_INJ_COV_82.SelectedItem.Value!="")
				objInfo.PERS_INJ_COV_82 = int.Parse(cmbPERS_INJ_COV_82.SelectedItem.Value);
			if(cmbLOC_EXCLUDED.SelectedItem!=null && cmbLOC_EXCLUDED.SelectedItem.Value!="")
				objInfo.LOC_EXCLUDED = int.Parse(cmbLOC_EXCLUDED.SelectedItem.Value);
			if(cmbOTHER_POLICY.SelectedItem!=null)
				objInfo.OTHER_POLICY = cmbOTHER_POLICY.SelectedItem.Value.ToString();

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			if(hidOldData.Value!="")
			{
				strRowId		=	hidLOCATION_ID.Value;
			}
			else
				strRowId="new";                        

			oldXML		= hidOldData.Value;
			//Returning the model object

			return objInfo;
		}
		#endregion
		

		#region LoadData Function

		private void LoadData()
		{
			int intCustomerID = Convert.ToInt32(hidCUSTOMER_ID.Value);
			int intPolicyID=Convert.ToInt32(hidPOLICY_ID .Value );
			int intPolicyVersionID =Convert.ToInt32(hidPOLICY_VERSION_ID.Value);
			int intLocationID=Convert.ToInt32(hidLOCATION_ID.Value );
			DataTable dtLocationInfo =ClsRealEstateLocation.GetPolicyRealEstateLocation(intCustomerID,intPolicyID,intPolicyVersionID,intLocationID);
			
			hidOldData.Value=Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtLocationInfo);
			
			txtLOCATION_NUMBER.Text  = dtLocationInfo.Rows[0]["LOCATION_NUMBER"].ToString();
			txtCLIENT_LOCATION_NUMBER.Text=dtLocationInfo.Rows[0]["CLIENT_LOCATION_NUMBER"].ToString();
			txtADDRESS_1.Text=dtLocationInfo.Rows[0]["ADDRESS_1"].ToString ();
			txtADDRESS_2.Text=dtLocationInfo.Rows[0]["ADDRESS_2"].ToString();
			txtCITY .Text =dtLocationInfo.Rows[0]["CITY"].ToString();
			txtCOUNTY.Text = dtLocationInfo.Rows[0]["COUNTY"].ToString();
			txtFAX_NUMBER.Text =dtLocationInfo.Rows[0]["FAX_NUMBER"].ToString();
			txtPHONE_NUMBER.Text=dtLocationInfo.Rows[0]["PHONE_NUMBER"].ToString();
			txtZIPCODE.Text =dtLocationInfo.Rows[0]["ZIPCODE"].ToString();
			int i =0;
			foreach(ListItem l in cmbSTATE.Items )
			{
				if(l.Value.ToString() == dtLocationInfo.Rows[0]["STATE"].ToString())
					cmbSTATE.SelectedIndex =i;
				i++;
			}
			
			hidIS_ACTIVE.Value=dtLocationInfo.Rows[0]["IS_ACTIVE"].ToString();
			


		}
		#endregion

		#region PopulateDropDown Function
		private void PopulateDropDown()
		{
			DataTable dt = Cms.CmsWeb.ClsFetcher.State;
			cmbSTATE.DataSource		= dt;
			cmbSTATE.DataTextField	= "State_Name";
			cmbSTATE.DataValueField	= "State_Id";				
			cmbSTATE.DataBind();

			cmbOCCUPIED_BY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("OCPB");
			cmbOCCUPIED_BY.DataTextField="LookupDesc"; 
			cmbOCCUPIED_BY.DataValueField="LookupID";
			cmbOCCUPIED_BY.DataBind();
			cmbOCCUPIED_BY.Items.Insert(0,"");

			cmbBUSS_FARM_PURSUITS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("BFP");
			cmbBUSS_FARM_PURSUITS.DataTextField="LookupDesc"; 
			cmbBUSS_FARM_PURSUITS.DataValueField="LookupID";
			cmbBUSS_FARM_PURSUITS.DataBind();
			cmbBUSS_FARM_PURSUITS.Items.Insert(0,"");

			cmbPERS_INJ_COV_82.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbPERS_INJ_COV_82.DataTextField="LookupDesc"; 
			cmbPERS_INJ_COV_82.DataValueField="LookupID";
			cmbPERS_INJ_COV_82.DataBind();
			cmbPERS_INJ_COV_82.Items.Insert(0,"");

			cmbLOC_EXCLUDED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbLOC_EXCLUDED.DataTextField="LookupDesc"; 
			cmbLOC_EXCLUDED.DataValueField="LookupID";
			cmbLOC_EXCLUDED.DataBind();
			
			ClsUmbrellaRecrVeh objUmbrellaRecrVeh = new ClsUmbrellaRecrVeh();
			dt = objUmbrellaRecrVeh.GetPolSelectedOtherPolicies(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value));
			if(dt!=null && dt.Rows.Count>0)
			{
				cmbOTHER_POLICY.DataSource = dt;
				cmbOTHER_POLICY.DataTextField = "POLICY_NUMBER_LOB";
				cmbOTHER_POLICY.DataValueField = "POLICY_NUMBER";
				cmbOTHER_POLICY.DataBind();
				cmbOTHER_POLICY.Items.Insert(0,new ListItem("",""));

			}
		}
		#endregion

		#region SetWorkFlow function
		private void SetWorkFlow()
		{
			if(base.ScreenId == "274_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID ());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				if(hidLOCATION_ID.Value!="0" && hidLOCATION_ID.Value.Trim() != "")
				{
					myWorkFlow.AddKeyValue("LOCATION_ID",hidLOCATION_ID.Value);
					myWorkFlow.AddKeyValue("REMARKS","isnull(REMARKS,0)");
				}
				myWorkFlow.WorkflowModule ="POL";
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}

		}
		#endregion

		#region Set Validators ErrorMessages
		private void SetValidators()
		{
			rfvLOCATION_NUMBER.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"208");
			rfvADDRESS1.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"909");
			rfvCITY.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"56");
			rfvZIPCODE.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");
			rfvCOUNTY.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"509");
			rfv_STATE.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"35");
			revZIPCODE.ValidationExpression	=   aRegExpZip;;
			revPHONE_NUMBER.ValidationExpression	= aRegExpPhone;
			revFAX_NUMBER.ValidationExpression	=	aRegExpFax;
			revLOCATION_NUMBER.ValidationExpression	       =aRegExpInteger;
			revLOCATION_NUMBER.ErrorMessage     =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("102");   
			revPHONE_NUMBER.ErrorMessage	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
			revFAX_NUMBER.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
			revZIPCODE.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"24");
			rngNUM_FAMILIES.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"967");
			rfvBUSS_FARM_PURSUITS.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"856");
			rfvBUSS_FARM_PURSUITS_DESC.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"857");
		}
		#endregion

		#region SetCaption Function
		private void SetCaptions()
		{
			
			System.Resources.ResourceManager objResourceMgr  = new System.Resources.ResourceManager("Cms.Policies.Aspx.Umbrella.PolicyAddRealEstateLocation" ,System.Reflection.Assembly.GetExecutingAssembly());
			capCLIENT_LOCATION_NUMBER.Text	= objResourceMgr.GetString("txtCLIENT_LOCATION_NUMBER");
			capLOCATION_NUMBER.Text			= objResourceMgr.GetString("txtLOCATION_NUMBER");
			capADDRESS_1.Text				= objResourceMgr.GetString("txtADDRESS_1");
			capADDRESS_2.Text				= objResourceMgr.GetString("txtADDRESS_2");
			capCITY.Text					= objResourceMgr.GetString("txtCITY");
			capCOUNTY.Text					= objResourceMgr.GetString("txtCOUNTY");
			capSTATE.Text					= objResourceMgr.GetString("cmbSTATE");
			capZIPCODE.Text					= objResourceMgr.GetString("txtZIPCODE");
			capPHONE_NUMBER.Text			= objResourceMgr.GetString("txtPHONE_NUMBER");
			capFAX_NUMBER.Text				= objResourceMgr.GetString("txtFAX_NUMBER");
			capPullCustomerAddress.Text		= objResourceMgr.GetString("btnPullCustomerAddress");
			capOCCUPIED_BY.Text		= objResourceMgr.GetString("cmbOCCUPIED_BY");
			capNUM_FAMILIES.Text		= objResourceMgr.GetString("txtNUM_FAMILIES");
			capBUSS_FARM_PURSUITS.Text		= objResourceMgr.GetString("cmbBUSS_FARM_PURSUITS");
			capBUSS_FARM_PURSUITS_DESC.Text		= objResourceMgr.GetString("txtBUSS_FARM_PURSUITS_DESC");
			capPERS_INJ_COV_82.Text		= objResourceMgr.GetString("cmbPERS_INJ_COV_82");
			capLOC_EXCLUDED.Text		= objResourceMgr.GetString("cmbLOC_EXCLUDED");	
			capOTHER_POLICY.Text				=		objResourceMgr.GetString("cmbOTHER_POLICY");


		}
		#endregion

		#region GetQueryString Function
		private void GetQueryString()
		{
			hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"];
			hidPOLICY_ID .Value = Request.Params["POLICY_ID"];
			hidPOLICY_VERSION_ID .Value = Request.Params["POLICY_VERSION_ID"];
			hidLOCATION_ID.Value = Request.Params["LOCATION_ID"];
			hidDWELLING_ID .Value =Request.Params["DWELLING_ID"];
				

		}
		#endregion
		[Ajax.AjaxMethod()]
		public string AjaxFetchZipForState(int stateID, string ZipID)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.FetchZipForState(stateID,ZipID);
			return result;
			
		}
		
		#region EventHandler For SaveButton
		private void btnSave_Click(object sender, System.EventArgs e)
		{
		
			ClsRealEstateLocationInfo   objInfo = new ClsRealEstateLocationInfo();
			ClsRealEstateLocation objLocation  =new ClsRealEstateLocation ();
			
			objInfo = GetFormValue();			
			int intRetVal;
			try
			{
				//Save Case
				if(strRowId.ToUpper().Equals("NEW"))
				{
					objInfo.CREATED_BY = int.Parse(GetUserId());
					
					intRetVal=objLocation.AddPolicy (objInfo);
					if(intRetVal==1)
					{
						lblMessage.Text =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("29");

						hidLOCATION_ID.Value = objInfo.LOCATION_ID .ToString();
						hidFormSaved.Value	="1";
						hidIS_ACTIVE.Value="Y";
						base.OpenEndorsementDetails();
						SetWorkFlow();
						LoadData ();
						
					}
					else
					{
						lblMessage.Text =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("165");
					}
				}
					//Update Case
				else
				{
					ClsRealEstateLocationInfo   objOldInfo = new ClsRealEstateLocationInfo();
					base.PopulateModelObject (objOldInfo,hidOldData.Value );
					
					objInfo.MODIFIED_BY =int.Parse(GetUserId ());
					objOldInfo.CUSTOMER_ID=int.Parse(hidCUSTOMER_ID.Value);
					objOldInfo.POLICY_ID =int.Parse(hidPOLICY_ID .Value);
					objOldInfo.POLICY_VERSION_ID =int.Parse(hidPOLICY_VERSION_ID .Value);
					objInfo.LOCATION_ID  = Convert.ToInt32(hidLOCATION_ID .Value);

					intRetVal=objLocation.UpdatePolicy(objOldInfo,objInfo);

					if(intRetVal == 1)
					{
						lblMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value="Y";
						base.OpenEndorsementDetails();
						LoadData ();
					}

				}
				lblMessage.Visible =true;
				
			}
			catch(Exception ex)
			{
				lblMessage.Text = "Information could't be saved this time " + ex.Message + " !Try again";
				lblMessage.Visible=true;
			}
			
		}
		#endregion

		#region EventHandler for ActivateDeactivate Button

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			ClsRealEstateLocation  objLocation=new ClsRealEstateLocation ();
			string strStatus="";
			if(hidIS_ACTIVE.Value =="Y")
			{
				strStatus="N";
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("41");
				hidIS_ACTIVE.Value ="N";
			}
			else if (hidIS_ACTIVE.Value =="N")
			{
				strStatus="Y";
				hidIS_ACTIVE.Value ="Y";
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("40");
			}

			objLocation.ActivateDeactivatePolicyLocation(Convert.ToInt32 (hidCUSTOMER_ID.Value),Convert.ToInt32 (hidPOLICY_ID.Value),
				Convert.ToInt32(hidPOLICY_VERSION_ID.Value ),Convert.ToInt32(hidLOCATION_ID.Value ),strStatus);

			hidFormSaved.Value="0";
			lblMessage.Visible=true;
			int intCustomerID = Convert.ToInt32(hidCUSTOMER_ID.Value);
			int intPolicyID=Convert.ToInt32(hidPOLICY_ID .Value );
			int intPolicyVersionID =Convert.ToInt32(hidPOLICY_VERSION_ID .Value);
			int intLocationID=Convert.ToInt32(hidLOCATION_ID.Value );
			DataTable dtLocationInfo =ClsRealEstateLocation .GetPolicyRealEstateLocation(intCustomerID,intPolicyID ,intPolicyVersionID  ,intLocationID);
			
			hidOldData.Value=Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtLocationInfo );
			ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidLOCATION_ID .Value + ");</script>");
			base.OpenEndorsementDetails();
		}
		#endregion

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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Delete Function
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;
			ClsRealEstateLocationInfo   objInfo = new ClsRealEstateLocationInfo();
			ClsRealEstateLocation objLocation  =new ClsRealEstateLocation ();
			base.PopulateModelObject (objInfo,hidOldData.Value );
			
			objInfo.MODIFIED_BY = int.Parse(GetUserId());
			if(hidLOCATION_ID.Value!=null && hidLOCATION_ID.Value!="")
				objInfo.LOCATION_ID=int.Parse(hidLOCATION_ID.Value);

			intRetVal = objLocation.DeletePolicyLocation(objInfo); // Calling BL Delete fxn.
						
			if(intRetVal>0)
			{
				lblDelete.Text			= Cms.CmsWeb.ClsMessages.GetMessage("G","127");
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				trBody.Attributes.Add("style","display:none");

			}
			else if(intRetVal == -1) // Dwelling is attached to the current location
			{
			
				lblDelete.Text			=	ClsMessages.FetchGeneralMessage("743");
				hidFormSaved.Value		=	"2";
			}
			SetWorkFlow();
			lblDelete.Visible = true;

		}
		[Ajax.AjaxMethod()]
		public string AjaxGetCountyForZip(string strZip)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.GetCountyForZip(strZip);
			return result;
		}
		#endregion
	}
}
