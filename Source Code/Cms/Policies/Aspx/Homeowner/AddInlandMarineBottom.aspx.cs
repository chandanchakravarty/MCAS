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
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlApplication.HomeOwners;
using Cms.Model.Policy;
using Cms.Model.Policy.HomeOwners;
using Cms.Model.Policy.Homeowners;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.Controls;

namespace Policies.Aspx.Homeowner
{
	/// <summary>
	/// Summary description for AddInlandMarineBottom.
	/// </summary>
	public class AddInlandMarineBottom : Cms.Policies.policiesbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.TextBox txtITEM_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtITEM_DESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtITEM_SERIAL_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtITEM_INSURING_VALUE;
		protected System.Web.UI.WebControls.DropDownList cmbITEM_APPRAISAL_BILL;
		protected System.Web.UI.WebControls.DropDownList cmbITEM_PICTURE_ATTACHED;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_ITEM_NUMER_FOR_ADD;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		//protected Cms.CmsWeb.Controls.CmsButton btnDelete;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvITEM_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvITEM_DESCRIPTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvITEM_INSURING_VALUE;

		protected System.Web.UI.WebControls.RegularExpressionValidator revITEM_NUMBER;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revITEM_SERIAL_NUMBER;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revITEM_INSURING_VALUE;
		protected System.Web.UI.WebControls.Label lblMessage;

		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved,strCalledFrom;
		
		protected System.Web.UI.WebControls.Label capITEM_NUMBER;
		protected System.Web.UI.WebControls.Label capITEM_DESCRIPTION;
		protected System.Web.UI.WebControls.Label capITEM_SERIAL_NUMBER;
		protected System.Web.UI.WebControls.Label capITEM_INSURING_VALUE;
		protected System.Web.UI.WebControls.Label capITEM_APPRAISAL_BILL;
		protected System.Web.UI.WebControls.Label capITEM_PICTURE_ATTACHED;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidITEM_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidITEM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidITEM_DETAIL_ID;
		protected System.Web.UI.HtmlControls.HtmlForm POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS;
		protected System.Web.UI.WebControls.RangeValidator rngINSURING_VALUE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revITEM_INSURING_VALUE;
		//Defining the business layer class object
		clsAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS  objBussObjSchCvgsDetails ;
		
		string customer_id,pol_id,pol_version_id;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.CustomValidator csvITEM_DESCRIPTION;
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
			rfvITEM_NUMBER.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("808");
			rfvITEM_DESCRIPTION.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416");
			rfvITEM_INSURING_VALUE.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("554");
			revITEM_NUMBER.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

			//revITEM_SERIAL_NUMBER.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			//revITEM_INSURING_VALUE.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			revITEM_NUMBER.ValidationExpression			=	aRegExpInteger;
			//revITEM_SERIAL_NUMBER.ValidationExpression	=	aRegExpap;
			//revITEM_INSURING_VALUE.ValidationExpression	=	aregex;
			csvITEM_DESCRIPTION.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("455");
		}
		#endregion


		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			#region Setting ScreenId
			// Check from where the screen is called.
			if (Request.QueryString["CalledFrom"] != null || Request.QueryString["CalledFrom"] !="")
			{
				strCalledFrom=Request.QueryString["CalledFrom"].ToString();				
			}			
			//Setting screen Id.	
			switch (strCalledFrom) 
			{
				case "Home":
				case "HOME":
					base.ScreenId="236_0_0";
					break;
				case "Rental":
				case "RENTAL":
					base.ScreenId="161_0_0";
					break;			
			}
			#endregion
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				
			btnReset.CmsButtonClass					=	CmsButtonType.Execute;
			btnReset.PermissionString				=	gstrSecurityXML;

			btnSave.CmsButtonClass					=	CmsButtonType.Execute;
			btnSave.PermissionString				=	gstrSecurityXML;

			
			btnDelete.CmsButtonClass				=	CmsButtonType.Delete;
			btnDelete.PermissionString				=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=   CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=   gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************


			lblMessage.Visible = false;
			SetErrorMessages();

			objResourceMgr = new System.Resources.ResourceManager("Cms.BusinessLayer.BlApplication.AddInlandMarineBOTTOM" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{	
				customer_id=  GetCustomerID();
				pol_id= GetPolicyID(); 
				pol_version_id = GetPolicyVersionID(); 
						
				
				if(Request.QueryString["ITEM_ID"] != null )
					hidITEM_ID.Value		= Request.QueryString["ITEM_ID"].ToString();

				if(Request.QueryString["ITEM_DETAIL_ID"] != null && Request.QueryString["ITEM_DETAIL_ID"].ToString()  != "0")
					hidITEM_DETAIL_ID.Value	= Request.QueryString["ITEM_DETAIL_ID"].ToString();					

				//Edit case -> Get data from DB to display
			
				if (hidITEM_DETAIL_ID.Value != "0" && hidITEM_DETAIL_ID.Value != "")
				{
					DataSet ds = new DataSet();
					clsAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS obj = new clsAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS();
					ds = obj.getDataSCH_ITEMS_CVGS_DETAIL_Policy(customer_id,pol_id,pol_version_id,hidITEM_ID.Value,hidITEM_DETAIL_ID.Value);
					hidOldData.Value		= ds.GetXml();
					hidFormSaved.Value = "0";
					obj.Dispose();
				}
					//Add new case get MAX Item num from DB based on CustID,POLID,VerID and ItemID
				else
				{
					clsAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS obj = new clsAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS();
					hidNEW_ITEM_NUMER_FOR_ADD.Value = obj.getMaxItemNumberPolicy(customer_id,pol_id,pol_version_id,hidITEM_ID.Value);
					obj.Dispose();
				}
									
			
				#region "Loading singleton"
				#endregion//Loading singleton
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
		private  clsPolSchItemsCvgsDetailsInfo GetFormValue()
		{
			
			//Creating the Model object for holding the New data
			clsPolSchItemsCvgsDetailsInfo objSCH_ITEMS_CVGS_DETAILS_Info;
			objSCH_ITEMS_CVGS_DETAILS_Info = new clsPolSchItemsCvgsDetailsInfo();

			objSCH_ITEMS_CVGS_DETAILS_Info.CUSTOMER_ID				=   int.Parse(GetCustomerID());
			objSCH_ITEMS_CVGS_DETAILS_Info.POL_ID					=	int.Parse(GetPolicyID());
			objSCH_ITEMS_CVGS_DETAILS_Info.POL_VERSION_ID			=   int.Parse(GetPolicyVersionID());

			objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_ID					=   hidITEM_ID.Value!=""?int.Parse(hidITEM_ID.Value):0;
			objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_DETAIL_ID			=   hidITEM_DETAIL_ID.Value != "NEW"?int.Parse(hidITEM_DETAIL_ID.Value):0;

			objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_NUMBER				=	txtITEM_NUMBER.Text;
			objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_DESCRIPTION			=	txtITEM_DESCRIPTION.Text;
			objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_SERIAL_NUMBER		=	txtITEM_SERIAL_NUMBER.Text;
			objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_INSURING_VALUE		=	Double.Parse(txtITEM_INSURING_VALUE.Text);
			objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_APPRAISAL_BILL		=	cmbITEM_APPRAISAL_BILL.SelectedValue;
			objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_PICTURE_ATTACHED	=	cmbITEM_PICTURE_ATTACHED.SelectedValue;

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidITEM_DETAIL_ID.Value;
			oldXML			=	hidOldData.Value;
			//Returning the model object

			return objSCH_ITEMS_CVGS_DETAILS_Info;
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
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
				int intRetVal;	//For retreiving the return value of business class save function
				objBussObjSchCvgsDetails = new  clsAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS();

				//Retreiving the form values into model class object
				clsPolSchItemsCvgsDetailsInfo objSCH_ITEMS_CVGS_DETAILS_Info = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW") || strRowId.ToUpper().Equals("0")  || strRowId.ToUpper().Equals("")) //save case
				{
					objSCH_ITEMS_CVGS_DETAILS_Info.CREATED_BY = int.Parse(GetUserId());
					objSCH_ITEMS_CVGS_DETAILS_Info.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objBussObjSchCvgsDetails.AddPolicy(objSCH_ITEMS_CVGS_DETAILS_Info);

					if(intRetVal>0)
					{
						hidITEM_NUMBER.Value	= objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_NUMBER.ToString();
						hidITEM_DETAIL_ID.Value = objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_DETAIL_ID.ToString();
						lblMessage.Text			= ClsMessages.GetMessage(base.ScreenId,"29");
						
						hidFormSaved.Value		= "1";						
						hidIS_ACTIVE.Value		= "Y";
						//Opening the endorsement details page
						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.FetchGeneralMessage("814");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -2)
					{
						lblMessage.Text				=		"Please save the Parent grid prior to saving Details record.";
						hidOldData.Value			=		"";
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text				=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{	
					objSCH_ITEMS_CVGS_DETAILS_Info.MODIFIED_BY = int.Parse(GetUserId());
					objSCH_ITEMS_CVGS_DETAILS_Info.LAST_UPDATED_DATETIME = DateTime.Now;

					//base.PopulateModelObject(objSCH_ITEMS_CVGS_DETAILS_Info,hidOldData.Value);

					intRetVal	= objBussObjSchCvgsDetails.UpdatePolicy(objSCH_ITEMS_CVGS_DETAILS_Info);

					if( intRetVal > 0 )
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";		
						//Opening the endorsement details page
						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)	
					{
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("814");
						hidFormSaved.Value		=	"1";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;					
				}


				DataSet ds = new DataSet();
				clsAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS obj = new clsAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS();
				ds = obj.getDataSCH_ITEMS_CVGS_DETAIL_Policy (objSCH_ITEMS_CVGS_DETAILS_Info.CUSTOMER_ID.ToString(),
					objSCH_ITEMS_CVGS_DETAILS_Info.POL_ID.ToString(),
					objSCH_ITEMS_CVGS_DETAILS_Info.POL_VERSION_ID.ToString(),
					objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_ID.ToString(),
					objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_DETAIL_ID.ToString());
				hidOldData.Value		= ds.GetXml();
				ClientScript.RegisterStartupScript(this.GetType(),"Scriptcode", "<script language=JavaScript>RefreshWebGrid(1,"+objSCH_ITEMS_CVGS_DETAILS_Info.ITEM_DETAIL_ID.ToString()+", false);</script>"); 
				SetActivateDeactivate();
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
				if(objBussObjSchCvgsDetails!= null)
					objBussObjSchCvgsDetails.Dispose();
			}
		}

		#endregion

		#region Activate/ Deactivate feature
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			clsAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS objSchItemsCvgsDetails = new  clsAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS();
			clsPolSchItemsCvgsDetailsInfo objSchItemsCvgsDetailsInfo = GetFormValue();

			try
			{
				
				objSchItemsCvgsDetailsInfo.ITEM_DETAIL_ID = int.Parse(hidITEM_DETAIL_ID.Value);
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{		 			
					objSchItemsCvgsDetails.ActivateDeactivatePolicy(objSchItemsCvgsDetailsInfo,"N");						
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
					ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidITEM_DETAIL_ID.Value + ",true);</script>");
									
				}
				else
				{									
					
					objSchItemsCvgsDetails.ActivateDeactivatePolicy(objSchItemsCvgsDetailsInfo,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				hidFormSaved.Value			=	"0";
				//hidOldData.Value = objSchItemsCvgsDetails.getDataSCH_ITEMS_CVGS_DETAIL(GetCustomerID(), GetAppID(),GetAppVersionID(),hidITEM_ID.Value,hidITEM_DETAIL_ID.Value);
				DataSet ds=new DataSet(); 
				ds = objSchItemsCvgsDetails.getDataSCH_ITEMS_CVGS_DETAIL_Policy(GetCustomerID(), GetPolicyID(),GetPolicyVersionID(),hidITEM_ID.Value,hidITEM_DETAIL_ID.Value);
				if(ds.Tables[0].Rows.Count>0 )
					hidOldData.Value=Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(ds.Tables[0]);
				ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidITEM_DETAIL_ID.Value + ",true);</script>");
				SetActivateDeactivate();
				//Opening the endorsement details page
				base.OpenEndorsementDetails();
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
		private void SetActivateDeactivate()
		{
			try
			{
				hidIS_ACTIVE.Value	=	hidIS_ACTIVE.Value.Trim();
				btnActivateDeactivate.Visible=true;
				if (hidIS_ACTIVE.Value == "N")
				{
					btnActivateDeactivate.Text = "Activate";
				}
				else if (hidIS_ACTIVE.Value == "Y")
				{
					btnActivateDeactivate.Text = "Deactivate";
				}
				else
					btnActivateDeactivate.Visible=false;
			}
			catch(Exception objEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
			}
		}
	
		#endregion
		
		#region Delete feature
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;
			clsAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS objSchItemsCvgsDetails = new  clsAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS();
			clsPolSchItemsCvgsDetailsInfo objSchItemsCvgsDetailsInfo = GetFormValue();

			try
			{
				
				objSchItemsCvgsDetailsInfo.ITEM_DETAIL_ID = int.Parse(hidITEM_DETAIL_ID.Value);
				intRetVal = objSchItemsCvgsDetails.DeletePolicy(objSchItemsCvgsDetailsInfo);						
				if(intRetVal>0)
				{
					lblMessage.Text			= Cms.CmsWeb.ClsMessages.GetMessage("G","127");
					hidFormSaved.Value = "5";
					hidOldData.Value = "";
					trBody.Attributes.Add("style","display:none");
					//Opening the endorsement details page
					base.OpenEndorsementDetails();
				}
				else if(intRetVal == -1)
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"128");
					hidFormSaved.Value		=	"2";
				}
				lblMessage.Visible = true;
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

	}
}
