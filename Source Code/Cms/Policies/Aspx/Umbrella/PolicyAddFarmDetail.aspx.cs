/******************************************************************************************
	<Author					: -> Ravindra Kumar Gupta
	<Start Date				: -> 03-20-2006
	<End Date				: ->
	<Description			: -> Page to add umbrella farms info (Policy)
	<Review Date			: ->
	<Reviewed By			: ->
	
	Modification History
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
	/// Summary description for PolictAddFarmDetail.
	/// </summary>
	public class PolicyAddFarmDetail :Cms.Policies.policiesbase 
	{

		#region Page Controls Declaration
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.Label capLOCATION_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtLOCATION_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOCATION_NUMBER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOCATION_NUMBER;
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
		protected System.Web.UI.WebControls.Label capNO_OF_ACRES;
		protected System.Web.UI.WebControls.TextBox txtNO_OF_ACRES;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNO_OF_ACRES;
		protected System.Web.UI.WebControls.Label capFULL_PART;
		protected System.Web.UI.WebControls.DropDownList cmbFULL_PART;
		protected System.Web.UI.WebControls.Label capOCCUPIED;
		protected System.Web.UI.WebControls.DropDownList cmbOCCUPIED;
		protected System.Web.UI.WebControls.Label capRENTED;
		protected System.Web.UI.WebControls.DropDownList cmbRENTED;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFARM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckZipSubmit;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_LOB;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.WebControls.RangeValidator rngLOCATION_NUMBER;
		protected System.Web.UI.WebControls.RangeValidator rngNO_OF_ACRES;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		
		protected System.Web.UI.WebControls.CustomValidator csvZIPCODE;
		public string WebServiceURL;		
		#endregion
	
		#region PageLoad Event
		private void Page_Load(object sender, System.EventArgs e)
		{
			Ajax.Utility.RegisterTypeForAjax(typeof(PolicyAddFarmDetail));

			WebServiceURL = ClsCommon.WebServiceURL;
			txtZIPCODE.Attributes.Add("OnBlur","javascript:DisableValidators();GetTerritory();");
			btnReset.Attributes.Add("onclick","javascript:return ResetForm();");
			btnActivateDeactivate.Attributes.Add("onclick","javascript:return ResetData();");
			// Added by Swarup on 30-mar-2007
			imgZipLookup.Attributes.Add("style","cursor:hand");
			base.VerifyAddress(hlkZipLookup, txtADDRESS_1,txtADDRESS_2
				, txtCITY, cmbSTATE, txtZIPCODE);
			string url = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
			imgSelect.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','COUNTY','COUNTY','','txtCOUNTY','COUNTY','County')");

			base.ScreenId ="279_0";
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");   

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
		
			btnSave.CmsButtonClass	=CmsButtonType.Write;
			btnSave.PermissionString =	gstrSecurityXML;
			
			btnPullCustomerAddress.CmsButtonClass	=CmsButtonType.Write;
			btnPullCustomerAddress.PermissionString =	gstrSecurityXML;
			
			btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
			btnActivateDeactivate.PermissionString = gstrSecurityXML;

			btnDelete.CmsButtonClass	=	CmsButtonType.Delete;
			btnDelete.PermissionString =	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			base.RequiredPullCustAddWithCounty(txtADDRESS_1, txtADDRESS_2, txtCITY
				, null, cmbSTATE, txtZIPCODE,txtCOUNTY,null, btnPullCustomerAddress);

			if(!Page.IsPostBack)
			{
				GetQueryString();
				
				SetCaptions();
				
				PopulateDropDown();
				
				SetWorkFlow();

				SetValidators();
				if(hidFARM_ID.Value == "0" || hidFARM_ID.Value == "" )
				{
					txtLOCATION_NUMBER.Text=ClsUmbrellaFarm.GetNextPolicyLocationNumber(
						Convert.ToInt32 (hidCUSTOMER_ID.Value),Convert.ToInt32 (hidPOLICY_ID .Value),
						Convert.ToInt32(hidPOLICY_VERSION_ID.Value )).ToString();
				
				}
				else
				{
					LoadData ();
				}
				
			}

		}
		#endregion


		#region Setcaptions Function

		private void SetCaptions()
		{

			System.Resources.ResourceManager objResourceMgr  = new System.Resources.ResourceManager("Cms.Policies.Aspx.Umbrella.PolicyAddFarmDetail" ,System.Reflection.Assembly.GetExecutingAssembly());
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
			capFULL_PART.Text				= objResourceMgr.GetString("cmbFULL_PART");
			capNO_OF_ACRES.Text 			= objResourceMgr.GetString("txtNO_OF_ACRES");
			capOCCUPIED.Text 				= objResourceMgr.GetString("cmbOCCUPIED");
			capRENTED.Text 					= objResourceMgr.GetString("cmbRENTED");


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
			cmbSTATE.Items.Insert(0,"");

			cmbOCCUPIED.DataSource= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbOCCUPIED.DataTextField ="LookupDesc"; 
			cmbOCCUPIED.DataValueField="LookupCode";
			cmbOCCUPIED.DataBind();
			cmbOCCUPIED.Items.Insert(0,"");

			cmbRENTED.DataSource= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbRENTED.DataTextField ="LookupDesc"; 
			cmbRENTED.DataValueField="LookupCode";
			cmbRENTED.DataBind();
			cmbRENTED.Items.Insert(0,"");

			cmbFULL_PART.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("FULLPT");
			cmbFULL_PART.DataTextField ="LookupDesc"; 
			cmbFULL_PART.DataValueField="LookupCode";
			cmbFULL_PART.DataBind();
			cmbFULL_PART.Items.Insert(0,"");

		}

		#endregion

		#region GetQueryString Function

		private void GetQueryString()
		{
			hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"];
			hidPOLICY_ID.Value = Request.Params["POLICY_ID"];
			hidPOLICY_VERSION_ID.Value = Request.Params["POLICY_VERSION_ID"];
			hidFARM_ID.Value = Request.Params["FARM_ID"];
		}

		#endregion

		#region SetWorkFlow function
		private void SetWorkFlow()
		{
			if(base.ScreenId == "279_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID ());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
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

        #region SetValidators Function

		private void SetValidators()
		{
			revLOCATION_NUMBER.ValidationExpression	       =aRegExpInteger;
			revLOCATION_NUMBER.ErrorMessage     =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("102"); 
			rfvADDRESS1.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"909");
			rfvCITY.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"56");
			rfvZIPCODE.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");
			rfvCOUNTY.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"509");
			rfv_STATE.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"35");
			revZIPCODE.ValidationExpression	=   aRegExpZip;;
			revPHONE_NUMBER.ValidationExpression	= aRegExpPhone;
			revFAX_NUMBER.ValidationExpression	=	aRegExpFax;
			revPHONE_NUMBER.ErrorMessage	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
			revFAX_NUMBER.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
			revZIPCODE.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"24");
			revNO_OF_ACRES.ValidationExpression  = aRegExpInteger ;
			revNO_OF_ACRES.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217"); 
			rngLOCATION_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216"); 
			rfvLOCATION_NUMBER.ErrorMessage     =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("208"); 
			rngNO_OF_ACRES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216"); 			
		}
		#endregion 

		#region LoadData Function

		private void LoadData()
		{
			int intCustomerID = Convert.ToInt32(hidCUSTOMER_ID.Value);
			int intPolicyID=Convert.ToInt32(hidPOLICY_ID .Value );
			int intPolicyVersionID =Convert.ToInt32(hidPOLICY_VERSION_ID.Value);
			int intFarmID=Convert.ToInt32(hidFARM_ID.Value );
			DataTable dtFarmInfo =ClsUmbrellaFarm.GetPolicyFarmInfo(intCustomerID,intPolicyID,intPolicyVersionID  ,intFarmID);
			
			hidOldData.Value=Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtFarmInfo);
			
			txtLOCATION_NUMBER.Text  = dtFarmInfo.Rows[0]["LOCATION_NUMBER"].ToString();
			txtADDRESS_1.Text=dtFarmInfo.Rows[0]["ADDRESS_1"].ToString ();
			txtADDRESS_2.Text=dtFarmInfo.Rows[0]["ADDRESS_2"].ToString();
			txtCITY .Text =dtFarmInfo.Rows[0]["CITY"].ToString();
			txtCOUNTY.Text = dtFarmInfo.Rows[0]["COUNTY"].ToString();
			txtFAX_NUMBER.Text =dtFarmInfo.Rows[0]["FAX_NUMBER"].ToString();
			if(dtFarmInfo.Rows[0]["NO_OF_ACRES"] != System.DBNull.Value) 
				txtNO_OF_ACRES .Text=dtFarmInfo.Rows[0]["NO_OF_ACRES"].ToString();
			if(txtNO_OF_ACRES.Text.Trim() == "0")
				txtNO_OF_ACRES.Text="";
			txtPHONE_NUMBER.Text=dtFarmInfo.Rows[0]["PHONE_NUMBER"].ToString();
			txtZIPCODE.Text =dtFarmInfo.Rows[0]["ZIPCODE"].ToString();
			int i =0;
			foreach(ListItem l in cmbSTATE.Items )
			{
				if(l.Value.ToString() == dtFarmInfo.Rows[0]["STATE"].ToString())
					cmbSTATE.SelectedIndex =i;
				i++;
			}
			
			if(dtFarmInfo.Rows[0]["RENTED_TO_OTHER"].ToString()=="N")
				cmbRENTED.SelectedIndex =1;
			else if(dtFarmInfo.Rows[0]["RENTED_TO_OTHER"].ToString()=="Y")
				cmbRENTED.SelectedIndex =2;

			if(dtFarmInfo.Rows[0]["OCCUPIED_BY_APPLICANT"].ToString()=="N")
				cmbOCCUPIED.SelectedIndex =1;
			else if(dtFarmInfo.Rows[0]["OCCUPIED_BY_APPLICANT"].ToString()=="Y")
				cmbOCCUPIED.SelectedIndex =2;

			if(dtFarmInfo.Rows[0]["EMP_FULL_PART"].ToString()=="F")
				cmbFULL_PART.SelectedIndex=2;
			else if(dtFarmInfo.Rows[0]["EMP_FULL_PART"].ToString()=="P")
				cmbFULL_PART.SelectedIndex=1;

			hidIS_ACTIVE.Value=dtFarmInfo.Rows[0]["IS_ACTIVE"].ToString();
			btnDelete.Enabled = true;


		}
		#endregion

		#region EventHandler For SaveButton
		private void btnSave_Click(object sender, System.EventArgs e)
		{
		
			ClsFarmDetailsInfo  objInfo = new ClsFarmDetailsInfo();
			ClsUmbrellaFarm objFarm =new ClsUmbrellaFarm ();
	
			objInfo.CUSTOMER_ID=int.Parse(hidCUSTOMER_ID.Value);
			objInfo.POLICY_ID =int.Parse(hidPOLICY_ID .Value);
			objInfo.POLICY_VERSION_ID =int.Parse(hidPOLICY_VERSION_ID .Value);
		
			objInfo.LOCATION_NUMBER =	int.Parse(txtLOCATION_NUMBER.Text);

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

			if(txtNO_OF_ACRES.Text.Trim() != "")
				objInfo.NO_OF_ACRES = Convert.ToInt32(txtNO_OF_ACRES.Text.Trim());

			if(cmbFULL_PART.SelectedIndex > 0)
			{
				if(cmbFULL_PART.SelectedIndex ==2)
					objInfo.EMP_FULL_PART = "F";
				else if(cmbFULL_PART.SelectedIndex ==1)
					objInfo.EMP_FULL_PART ="P";
			}

			if(cmbOCCUPIED .SelectedIndex >0)
			{
				if(cmbOCCUPIED.SelectedIndex==2)
					objInfo.OCCUPIED_BY_APPLICANT ="Y";
				else if(cmbOCCUPIED.SelectedIndex ==1)
					objInfo.OCCUPIED_BY_APPLICANT ="N";
			}

			if(cmbRENTED.SelectedIndex>0)
			{
				if(cmbRENTED.SelectedIndex==2)
					objInfo.RENTED_TO_OTHER ="Y";
				else if(cmbRENTED.SelectedIndex==1)
					objInfo.RENTED_TO_OTHER ="N";
			}

			int intRetVal;
			try
			{
				//Save Case
				if(hidFARM_ID.Value == "0" || hidFARM_ID.Value == "")
				{
					objInfo.CREATED_BY = int.Parse(GetUserId());
					
					intRetVal=objFarm.AddPolicy(objInfo);
					if(intRetVal==1)
					{
						lblMessage.Text =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("29");

						hidFARM_ID.Value = objInfo.FARM_ID.ToString();
						hidFormSaved.Value	="1";
						hidIS_ACTIVE.Value="Y";
						base.OpenEndorsementDetails();
						SetWorkFlow();
						base.OpenEndorsementDetails();
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
					ClsFarmDetailsInfo  objOldInfo = new ClsFarmDetailsInfo();
					base.PopulateModelObject (objOldInfo,hidOldData.Value );
					
					objInfo.MODIFIED_BY =int.Parse(GetUserId ());
					objOldInfo.CUSTOMER_ID=int.Parse(hidCUSTOMER_ID.Value);
					objOldInfo.POLICY_ID =int.Parse(hidPOLICY_ID .Value);
					objOldInfo.POLICY_VERSION_ID =int.Parse(hidPOLICY_VERSION_ID .Value);
					objInfo.FARM_ID = Convert.ToInt32(hidFARM_ID .Value);

					intRetVal=objFarm.UpdatePolicy(objInfo,objOldInfo);

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
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible=true;
			}
			
		}
		#endregion

		#region EventHandler for Delete Button
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;
			
			Cms.Model.Policy.Umbrella.ClsFarmDetailsInfo objInfo = new ClsFarmDetailsInfo();
			ClsUmbrellaFarm objFarm =new ClsUmbrellaFarm();
			
			objInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objInfo.POLICY_ID		= int.Parse(hidPOLICY_ID.Value);
			objInfo.POLICY_VERSION_ID= int.Parse(hidPOLICY_VERSION_ID.Value);
						
			if(hidFARM_ID.Value!=null && hidFARM_ID.Value!="")
				objInfo.FARM_ID = int.Parse(hidFARM_ID.Value);
			objInfo.MODIFIED_BY = int.Parse(GetUserId());
						
			intRetVal = objFarm.DeletePolicyUmbrella(objInfo);			
			
			if(intRetVal>0)
			{
				lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");				
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				trBody.Attributes.Add("style","display:none");				
				base.OpenEndorsementDetails();
				SetWorkFlow();
			}
			else if(intRetVal == -1)
			{
				lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}			
			lblDelete.Visible = true;			
		}

		#endregion

		#region EventHandler for ActivateDeactivate Button

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			ClsUmbrellaFarm objUmbrellaFarm=new ClsUmbrellaFarm();
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

			objUmbrellaFarm.ActivateDeactivatePolicyFarm(Convert.ToInt32 (hidCUSTOMER_ID.Value),Convert.ToInt32 (hidPOLICY_ID.Value),
				Convert.ToInt32(hidPOLICY_VERSION_ID.Value ),Convert.ToInt32(hidFARM_ID.Value ),strStatus);

			hidFormSaved.Value="0";
			lblMessage.Visible=true;
			int intCustomerID = Convert.ToInt32(hidCUSTOMER_ID.Value);
			int intPolicyID=Convert.ToInt32(hidPOLICY_ID .Value );
			int intPolicyVersionID =Convert.ToInt32(hidPOLICY_VERSION_ID .Value);
			int intFarmID=Convert.ToInt32(hidFARM_ID.Value );
			DataTable dtFarmInfo =ClsUmbrellaFarm.GetPolicyFarmInfo(intCustomerID,intPolicyID ,intPolicyVersionID  ,intFarmID);
			
			hidOldData.Value=Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtFarmInfo);
			ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidFARM_ID.Value + ");</script>");
			base.OpenEndorsementDetails();
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

		}
		#endregion
	}
}
