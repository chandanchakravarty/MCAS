/******************************************************************************************
<Author				: -   Gaurav Tyagi
<Start Date				: -	9/5/2005 5:18:43 PM
<End Date				: -	
<Description				: - 	Code behind class for showing the default hierarchy page.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - Shows the default hierarchy page.
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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Maintenance;
using System.Resources;
using System.Reflection;
namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Shows the default hierarchy page.
	/// </summary>
	public class AddDefaultHierarchy : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration
        protected System.Web.UI.WebControls.Label capMessages;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capEntityName;
		protected System.Web.UI.WebControls.Label lblEntityName;
		protected System.Web.UI.WebControls.Label capDivison;
		protected System.Web.UI.WebControls.DropDownList cmbDivision;
		protected System.Web.UI.WebControls.CustomValidator cvDivision;
		protected System.Web.UI.WebControls.Label capDepartment;
		protected System.Web.UI.WebControls.DropDownList cmbDepartment;
		protected System.Web.UI.WebControls.CustomValidator cvDepartment;
		protected System.Web.UI.WebControls.Label capProfitCenter;
		protected System.Web.UI.WebControls.DropDownList cmbProfitCenter;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDepartmentXml;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidProfitCenterXml;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDeptId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidProfitCenterId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREC_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDepartment;
        protected System.Web.UI.WebControls.Label capDepartmentid;
		protected System.Web.UI.WebControls.CustomValidator cvProfitCenter;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDivision;
        protected System.Web.UI.WebControls.Label capDivisionid;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidProfit;
        protected System.Web.UI.WebControls.Label capProfit;
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		//Defining the business layer class object
		ClsDefaultHierarchy  objDefaultHierarchy ;
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
			cvDivision.ErrorMessage= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			cvDepartment.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			cvProfitCenter.ErrorMessage= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			hidDepartmentXml.Value = ClsDivision.GetXmlForDepartmentDropDown();
			hidProfitCenterXml.Value = ClsDepartment.GetXmlForProfitCenterDropDown();

			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			cmbDivision.Attributes.Add("OnChange","javascript:FillDepartment();");
			cmbDepartment.Attributes.Add("OnChange","javascript:FillProfitCenter();");
			cmbProfitCenter.Attributes.Add("OnChange","javascript:GetProfitCenterId();");

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="10_4";
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddDefaultHierarchy", System.Reflection.Assembly.GetExecutingAssembly());
			lblMessage.Visible = false;
			SetErrorMessages();
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            
           
           

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
	

			btnSave.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;
			
			string strCarrierSystemID = Cms.CmsWeb.cmsbase.CarrierSystemID;
			string strAgencyID = GetSystemId();
			if(strCarrierSystemID.ToUpper()!=strAgencyID.ToUpper())
				btnSave.Enabled = false;
	
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			if(!Page.IsPostBack)
			{
				ClsDivision.GetDivisionDropDown(cmbDivision);
				//cmbDivision.Items.Insert(0,"Select Division");
                //var tr = hidDivision.Value;
                //cmbDivision.Items.Insert(0,tr);

				if(Request.QueryString["AGENCY_ID"]!=null && Request.QueryString["AGENCY_ID"].ToString().Length>0)
					hidOldData.Value = ClsDefaultHierarchy.GetXmlForPageControls(Convert.ToInt32(Request.QueryString["AGENCY_ID"].ToString()));
				else
				{
					int intAgencyID = ClsAgency.GetAgencyIDFromCode(GetSystemId());
					hidOldData.Value = ClsDefaultHierarchy.GetXmlForPageControls(intAgencyID);
				}

                
				#region "Loading singleton"
				#endregion//Loading singleton
                SetCaptions();
                hidDepartment.Value = capDepartmentid.Text;
                hidDivision.Value = capDivisionid.Text;
                hidProfit.Value = capProfit.Text;
                var tr = hidDivision.Value;
                cmbDivision.Items.Insert(0, tr);
                capProfit.Visible = false;
                capDivisionid.Visible = false;
                capDepartmentid.Visible = false;
         
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
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsDefaultHierarchyInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsDefaultHierarchyInfo objDefaultHierarchyInfo;
			objDefaultHierarchyInfo = new ClsDefaultHierarchyInfo();

			if(Request.QueryString["AGENCY_ID"]!=null && Request.QueryString["AGENCY_ID"].Length>0)
			objDefaultHierarchyInfo.AGENCY_ID = Convert.ToInt32(Request.QueryString["AGENCY_ID"].ToString());
			objDefaultHierarchyInfo.DIV_ID=	Convert.ToInt32(cmbDivision.SelectedValue);
			objDefaultHierarchyInfo.DEPT_ID=	Convert.ToInt32(hidDeptId.Value.ToString());
			objDefaultHierarchyInfo.PC_ID=	Convert.ToInt32(hidProfitCenterId.Value.ToString());
			objDefaultHierarchyInfo.IS_ACTIVE ="Y";

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidREC_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objDefaultHierarchyInfo;
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
				int intRetVal;	//For retreiving the return value of business class save function
				objDefaultHierarchy = new  ClsDefaultHierarchy();

				//Retreiving the form values into model class object
				ClsDefaultHierarchyInfo objDefaultHierarchyInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objDefaultHierarchyInfo.CREATED_BY = int.Parse(GetUserId());
					objDefaultHierarchyInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objDefaultHierarchy.Add(objDefaultHierarchyInfo);

					if(intRetVal>0)
					{
						hidREC_ID.Value = objDefaultHierarchyInfo.REC_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidOldData.Value = ClsDefaultHierarchy.GetXmlForPageControls(Convert.ToInt32(Request.QueryString["AGENCY_ID"]));
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
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
					ClsDefaultHierarchyInfo objOldDefaultHierarchyInfo;
					objOldDefaultHierarchyInfo = new ClsDefaultHierarchyInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldDefaultHierarchyInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objDefaultHierarchyInfo.REC_ID = Convert.ToInt32(strRowId);
					objDefaultHierarchyInfo.MODIFIED_BY = int.Parse(GetUserId());
					objDefaultHierarchyInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objDefaultHierarchyInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= objDefaultHierarchy.Update(objOldDefaultHierarchyInfo,objDefaultHierarchyInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidOldData.Value = ClsDefaultHierarchy.GetXmlForPageControls(Convert.ToInt32(Request.QueryString["AGENCY_ID"]));
						hidFormSaved.Value		=	"1";
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
			hidFormSaved.Value="0";
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objDefaultHierarchy!= null)
					objDefaultHierarchy.Dispose();
			}
		}

		
		#endregion
		private void SetCaptions()
		{
            capDivison.Text = objResourceMgr.GetString("cmbDivison");
            capDepartment.Text = objResourceMgr.GetString("cmbDepartment");
            capProfitCenter.Text = objResourceMgr.GetString("cmbProfitCenter");
            capDepartmentid.Text = objResourceMgr.GetString("capDepartmentid");
            capDivisionid.Text = objResourceMgr.GetString("capDivisionid");
            capProfit.Text = objResourceMgr.GetString("capProfit");
            
		}
		
	}
}
