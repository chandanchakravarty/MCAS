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
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.Model.Policy.GeneralLiability ;

namespace Cms.Policies.Aspx.GeneralLiability
{
	/// <summary>
	/// Summary description for PolicyGeneralLiabilityDetails.
	/// </summary>
	public class PolicyGeneralLiabilityDetails :Cms.Policies.policiesbase 
	{
		#region Page Controls Declaration
		protected System.Web.UI.HtmlControls.HtmlImage imgSelectDescForClass;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblLOCATION_ID;		
		protected System.Web.UI.WebControls.Label capLOCATION_ID;
		//protected System.Web.UI.WebControls.DropDownList cmbLOCATION_ID;
		protected System.Web.UI.WebControls.Label capCLASS_CODE;
		protected System.Web.UI.WebControls.TextBox txtCLASS_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCLASS_CODE;
		protected System.Web.UI.WebControls.Label capBUSINESS_DESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtBUSINESS_DESCRIPTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBUSINESS_DESCRIPTION;
		protected System.Web.UI.WebControls.Label capCOVERAGE_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbCOVERAGE_TYPE;
		protected System.Web.UI.WebControls.Label capCOVERAGE_FORM;
		protected System.Web.UI.WebControls.DropDownList cmbCOVERAGE_FORM;
		protected System.Web.UI.WebControls.Label capEXPOSURE_BASE;
		protected System.Web.UI.WebControls.DropDownList cmbEXPOSURE_BASE;
		protected System.Web.UI.WebControls.Label capEXPOSURE;
		protected System.Web.UI.WebControls.TextBox txtEXPOSURE;
		protected System.Web.UI.WebControls.RangeValidator rngEXPOSURE;
		protected System.Web.UI.WebControls.Label capRATE;
		protected System.Web.UI.WebControls.TextBox txtRATE;
		protected System.Web.UI.WebControls.RangeValidator rngRATE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_GEN_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidClassCheck;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow; 
		#endregion

		//private string strRowId;		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="283_0";
			lblMessage.Visible = false;

			//btnReset.Attributes.Add("onClick","javascript:return ResetTheForm();");

			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString =	gstrSecurityXML;
//			if (Request.Form["__EVENTTARGET"] == "FetchCodes")
//			{   
//				FetchCodes();
//				return;
//			}
			SetCaptions();
			if(!Page.IsPostBack)
			{
				string url=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
				GetQueryStringValues();
				LoadDropDowns();				
				SetCaptions();
				GetOldDataXML();
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");			
//				txtCLASS_CODE.Attributes.Add("onBlur","javascript:FetchCodes();");
				//imgSelectDescForClass.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','LOOKUP_VALUE_CODE','LOOKUP_VALUE_DESC','txtCLASS_CODE','txtBUSINESS_DESCRIPTION','LookupTable','Business Description'," + "\"@LOOKUP_NAME=\'GLCC\'\");");
				  imgSelectDescForClass.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','LOOKUP_VALUE_CODE','LOOKUP_VALUE_DESC','txtCLASS_CODE','txtBUSINESS_DESCRIPTION','BussDesc','Business Description'," + "\"@LOOKUP_NAME=\'GLCC\'\");");								
				SetWorkFlow();
			}
		}

		#region GetQueryStringValues Function 
		private void GetQueryStringValues()
		{
//			if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"]!="")
				hidCUSTOMER_ID.Value = GetCustomerID();
//			if(Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"]!="")
				hidPOLICY_ID.Value = GetPolicyID();
//			if(Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"]!="")
				hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();
//			if(Request.QueryString["POLICY_GEN_ID"]!=null && Request.QueryString["POLICY_GEN_ID"]!="")
//				hidPOLICY_GEN_ID.Value = Request.QueryString["POLICY_GEN_ID"];
		}
		#endregion 

		#region GetOldDataXML Function 
		private void GetOldDataXML()
		{
//			if(hidPOLICY_GEN_ID.Value!="" && hidPOLICY_GEN_ID.Value!="0")
//			{
				hidOldData.Value =ClsGeneralLiabilityDetails.GetPolicyGenDetailsXML( 
					Convert.ToInt32(hidCUSTOMER_ID.Value),
					Convert.ToInt32(hidPOLICY_ID .Value),
					Convert.ToInt32(hidPOLICY_VERSION_ID .Value));
					//Convert.ToInt32(hidPOLICY_GEN_ID .Value));
//			}
//			else
//				hidOldData.Value="";
			
		}
		#endregion 

		#region FetchCodes
		private void FetchCodes()
		{
			if(txtCLASS_CODE.Text.Trim()!="")
			{
				string ClassCode = ClsGeneralLiabilityDetails.GetClassCode("GLCC",txtCLASS_CODE.Text.Trim());
				txtBUSINESS_DESCRIPTION.Text = ClassCode;
				hidFormSaved.Value="4";
				hidClassCheck.Value="0";
			}
			else
			{
				txtBUSINESS_DESCRIPTION.Text="";
			}

		}
		#endregion

		#region LoadDropDowns Function 
		private void LoadDropDowns()
		{
			try
			{
				DataTable dtLocations;
				//Check for user-type---Wolverine User or Agency User
//				string strCarrierSystemID = Cms.CmsWeb.cmsbase.CarrierSystemID;
//				string strSystemID = GetSystemId();
//				if(strCarrierSystemID.ToUpper()!=strSystemID.ToUpper())
//				{
//					//For agency user, show all the locations					
//					dtLocations =  ClsGeneralLiabilityDetails.GetPolicyLocations(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID .Value),"N");
//				}
//				else
//				{
//					//For wolverine user, show only one location
				lblLOCATION_ID.Text  =  ClsGeneralLiabilityDetails.GetPolicyLocations(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID .Value));
//				}
//				if(dtLocations!=null && dtLocations.Rows.Count>0)
//				{
//					cmbLOCATION_ID.DataSource = dtLocations;
//					cmbLOCATION_ID.DataTextField = "LOCATIONS";
//					cmbLOCATION_ID.DataValueField = "LOCATION_ID";
//					cmbLOCATION_ID.DataBind();
//					cmbLOCATION_ID.Items.Insert(0,"");
//				}

				#region Filling Coverage Type Drop Down List
				dtLocations = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("GLCT");
				DataView dv = dtLocations.DefaultView;
				cmbCOVERAGE_TYPE.DataSource=dv;		
				cmbCOVERAGE_TYPE.DataTextField="LookupDesc"; 
				cmbCOVERAGE_TYPE.DataValueField="LookupID";
				cmbCOVERAGE_TYPE.DataBind();
				cmbCOVERAGE_TYPE.Items.Insert(0,"");
				#endregion

				#region Filling Coverage Form Drop Down List
				dtLocations = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("GLCF");
				dv = dtLocations.DefaultView;
				cmbCOVERAGE_FORM.DataSource=dv;		
				cmbCOVERAGE_FORM.DataTextField="LookupDesc"; 
				cmbCOVERAGE_FORM.DataValueField="LookupID";
				cmbCOVERAGE_FORM.DataBind();
				cmbCOVERAGE_FORM.Items.Insert(0,"");
				#endregion

				#region Filling Exposure Base Drop Down List
				dtLocations = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("GLEXP");
				dv = dtLocations.DefaultView;
				cmbEXPOSURE_BASE.DataSource=dv;		
				cmbEXPOSURE_BASE.DataTextField="LookupDesc"; 
				cmbEXPOSURE_BASE.DataValueField="LookupID";
				cmbEXPOSURE_BASE.DataBind();
				cmbEXPOSURE_BASE.Items.Insert(0,"");
				#endregion
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				throw(ex);
			}
			finally{}
		}
		#endregion

		#region SetCaptions Function
		private void SetCaptions()
		{
			System.Resources.ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.GeneralLiability.PolicyGeneralLiabilityDetails",System.Reflection.Assembly.GetExecutingAssembly());
			capLOCATION_ID.Text			=		objResourceMgr.GetString("cmbLOCATION_ID");
			capCLASS_CODE.Text			=		objResourceMgr.GetString("txtCLASS_CODE");
			capBUSINESS_DESCRIPTION.Text=		objResourceMgr.GetString("txtBUSINESS_DESCRIPTION");
			capCOVERAGE_TYPE.Text		=		objResourceMgr.GetString("cmbCOVERAGE_TYPE");			
			capCOVERAGE_FORM.Text		=		objResourceMgr.GetString("cmbCOVERAGE_FORM");
			capEXPOSURE_BASE.Text		=		objResourceMgr.GetString("cmbEXPOSURE_BASE");
			capEXPOSURE.Text			=       objResourceMgr.GetString("txtEXPOSURE");     
			capRATE.Text				=       objResourceMgr.GetString("txtRATE");     
		}
		#endregion

		#region SetWorkFlow Function 
		private void SetWorkFlow()
		{
			if(base.ScreenId == "283_0")
			{
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

		#region GetFormValue
		private ClsGeneralLiabilityDetailsInfo GetFormValue()
		{
			ClsGeneralLiabilityDetailsInfo objGeneralLiabilityDetailsInfo = new ClsGeneralLiabilityDetailsInfo();
			objGeneralLiabilityDetailsInfo.CUSTOMER_ID			= int.Parse(hidCUSTOMER_ID.Value);
			objGeneralLiabilityDetailsInfo.POLICY_ID 			= int.Parse(hidPOLICY_ID .Value);
			objGeneralLiabilityDetailsInfo.POLICY_VERSION_ID 	= int.Parse(hidPOLICY_VERSION_ID .Value);
			objGeneralLiabilityDetailsInfo.CREATED_BY			= int.Parse(GetUserId());
			objGeneralLiabilityDetailsInfo.CREATED_DATETIME		= System.DateTime.Now;
			objGeneralLiabilityDetailsInfo.MODIFIED_BY			= int.Parse(GetUserId());
			objGeneralLiabilityDetailsInfo.LAST_UPDATED_DATETIME= System.DateTime.Now;

//			if(cmbLOCATION_ID.SelectedItem!=null && cmbLOCATION_ID.SelectedItem.Value!="")
//				objGeneralLiabilityDetailsInfo.LOCATION_ID		= int.Parse(cmbLOCATION_ID.SelectedItem.Value);
			objGeneralLiabilityDetailsInfo.CLASS_CODE			= txtCLASS_CODE.Text.Trim();
			objGeneralLiabilityDetailsInfo.BUSINESS_DESCRIPTION = txtBUSINESS_DESCRIPTION.Text.Trim();
			if(cmbCOVERAGE_TYPE.SelectedItem!=null && cmbCOVERAGE_TYPE.SelectedItem.Value!="")
				objGeneralLiabilityDetailsInfo.COVERAGE_TYPE	= int.Parse(cmbCOVERAGE_TYPE.SelectedItem.Value);
			if(cmbCOVERAGE_FORM.SelectedItem!=null && cmbCOVERAGE_FORM.SelectedItem.Value!="")
				objGeneralLiabilityDetailsInfo.COVERAGE_FORM	= int.Parse(cmbCOVERAGE_FORM.SelectedItem.Value);
			if(cmbEXPOSURE_BASE.SelectedItem!=null && cmbEXPOSURE_BASE.SelectedItem.Value!="")
				objGeneralLiabilityDetailsInfo.EXPOSURE_BASE	= int.Parse(cmbEXPOSURE_BASE.SelectedItem.Value);
			if(txtEXPOSURE.Text.Trim()!="")
				objGeneralLiabilityDetailsInfo.EXPOSURE				= int.Parse(txtEXPOSURE.Text.Trim());
			if(txtRATE.Text.Trim()!="")
				objGeneralLiabilityDetailsInfo.RATE					= int.Parse(txtRATE.Text.Trim());
//			if(hidOldData.Value=="")
//				strRowId = "NEW";
//			else
//			{
//				strRowId = hidPOLICY_GEN_ID .Value;
//				objGeneralLiabilityDetailsInfo.POLICY_GEN_ID 		= int.Parse(hidPOLICY_GEN_ID .Value);
//			}
			
			return objGeneralLiabilityDetailsInfo;
		}
		#endregion

		#region "Web Event Handler btnSave_Click "
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal=0;	//For retreiving the return value of business class save function
				ClsGeneralLiabilityDetailsInfo objGeneralLiabilityDetailsInfo = GetFormValue();
			 	ClsGeneralLiabilityDetails  objGeneralLiabilityDetails = new ClsGeneralLiabilityDetails();
				if(hidOldData.Value=="" || hidOldData.Value=="0")
				{
					//Calling the add method of business layer class
					intRetVal = objGeneralLiabilityDetails.AddPolicy(objGeneralLiabilityDetailsInfo);

					if(intRetVal>0)
					{
//						hidPOLICY_GEN_ID.Value = objGeneralLiabilityDetailsInfo.POLICY_GEN_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						GetOldDataXML();
                        base.OpenEndorsementDetails();

						hidIS_ACTIVE.Value = "Y";
						SetWorkFlow();
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
					ClsGeneralLiabilityDetailsInfo objOldGeneralLiabilityDetailsInfo = new ClsGeneralLiabilityDetailsInfo();			

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldGeneralLiabilityDetailsInfo,hidOldData.Value);

					//Updating the record using business layer class object
					intRetVal	= objGeneralLiabilityDetails.UpdatePolicy(objGeneralLiabilityDetailsInfo,objOldGeneralLiabilityDetailsInfo);
					if( intRetVal > 0 )			// update successfully performed
					{						
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
                        base.OpenEndorsementDetails();

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
				
			}
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
		}
		#endregion
	}
}
