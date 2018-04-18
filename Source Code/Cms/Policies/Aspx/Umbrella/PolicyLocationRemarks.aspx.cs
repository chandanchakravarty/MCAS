/******************************************************************************************
	<Author					: -> Ravindra Kumar Gupta
	<Start Date				: -> 03-21-2006
	<End Date				: ->
	<Description			: -> Page to add umbrella Real Estate Location Remarks
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

namespace Cms.Policies.Aspx.Umbrella
{
	/// <summary>
	/// Summary description for PolicyLocationRemarks.
	/// </summary>
	public class PolicyLocationRemarks :Cms.Policies.policiesbase 
	{
		#region Page Controls Declaration
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capREMARKS;
		protected System.Web.UI.WebControls.TextBox txtREMARKS;
		protected System.Web.UI.WebControls.CustomValidator csvREMARKS;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOCATION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		#endregion
	
		#region PageLoad Function
		private void Page_Load(object sender, System.EventArgs e)
		{
		
			base.ScreenId			=	"274_2";
		
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "');");
				
			btnSave.CmsButtonClass					=		Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString				=		gstrSecurityXML;	

			btnReset.CmsButtonClass					=		Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString				=		gstrSecurityXML;
	
			csvREMARKS.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"431");
			if(!IsPostBack)
			{  
				GetQueryString();
				SetCaptions();
				GetOldDataXML();  
				SetWorkFlow();
			}
			
		}
		#endregion

		#region SetCaptions Function
		private void SetCaptions()
		{
			System.Resources.ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Umbrella.PolicyLocationRemarks" ,System.Reflection.Assembly.GetExecutingAssembly());
			capREMARKS.Text					=		objResourceMgr.GetString("txtREMARKS");
		}
		#endregion

		#region GetOldDataXML Function
		private void GetOldDataXML()
		{
			ClsRealEstateLocation ObjRealEstateLocation = new ClsRealEstateLocation();
			hidOldData.Value = ObjRealEstateLocation.ViewPolicyLocationRemarks(
				int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),
				int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(hidLOCATION_ID.Value));
		}
		#endregion 


		#region GetQueryString Function
		private void GetQueryString()
		{
			hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"];
			hidPOLICY_ID .Value = Request.Params["POLICY_ID"];
			hidPOLICY_VERSION_ID .Value = Request.Params["POLICY_VERSION_ID"];
			hidLOCATION_ID.Value = Request.Params["LOCATION_ID"];
		}
		#endregion

		#region SetWorkflow Function
		private void SetWorkFlow()
		{
			if(base.ScreenId == "274_2")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID ());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule ="POL";
				if(hidLOCATION_ID.Value!="0" && hidLOCATION_ID.Value.Trim() != "")
				{
					myWorkFlow.AddKeyValue("LOCATION_ID",hidLOCATION_ID.Value);
					myWorkFlow.AddKeyValue("REMARKS","isnull(REMARKS,0)");
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Web Event Handler btnSave_Click

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			int intRetVal;	
			int flag=0;
			ClsRealEstateLocation ObjRealEstateLocation = new  ClsRealEstateLocation();
			
			Model.Policy.Umbrella.ClsRealEstateLocationInfo  objInfo = new ClsRealEstateLocationInfo();
			Model.Policy.Umbrella.ClsRealEstateLocationInfo  objOldInfo = new ClsRealEstateLocationInfo();

			
			try
			{
				int intCUSTOMER_ID		= int.Parse(hidCUSTOMER_ID.Value);
				int intPOLICY_ID			= int.Parse(hidPOLICY_ID .Value);
				int intPOLICY_VER_ID       = int.Parse(hidPOLICY_VERSION_ID .Value);
				int intLOCATION_ID      = int.Parse(hidLOCATION_ID.Value);	
				
				objInfo.REMARKS = txtREMARKS.Text;
				
				base.PopulateModelObject(objOldInfo,hidOldData.Value);
			
				objInfo.CUSTOMER_ID	=	intCUSTOMER_ID;
				objInfo.POLICY_ID         = intPOLICY_ID ;
				objInfo.POLICY_VERSION_ID=intPOLICY_VER_ID ;
				objInfo.LOCATION_ID =     intLOCATION_ID;
				objInfo.MODIFIED_BY	=	int.Parse(GetUserId());
				objInfo.LAST_UPDATED_DATETIME	=	DateTime.Now;
					
				if(hidOldData.Value =="")
					flag=1;
				
				intRetVal=	ObjRealEstateLocation.UpdatePolicyRemarks(objOldInfo,objInfo);
					
				if( intRetVal > 0 )		
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"29");
					hidFormSaved.Value		=	"1";
					GetOldDataXML();
					base.OpenEndorsementDetails();
					SetWorkFlow();
				}
				if(flag==1)
					lblMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31");

				if(intRetVal == -1 || intRetVal == -2)
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
					hidFormSaved.Value		=	"2";
				}
				else if( intRetVal > 0 )						
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"29");
				else						
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
					hidFormSaved.Value		=	"2";
				}
				
				lblMessage.Visible=true;
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		
		}
		#endregion
	}
}
