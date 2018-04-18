/******************************************************************************************
<Author				: -   Pradeep
<Start Date			: -	11/14/2005 4:02:26 PM
<End Date			: -	
<Description		: - 	Show the Personal article general information page.
<Review Date		: - 
<Reviewed By		: - 	

<Modified Date			: - 09/02/2006
<Modified By			: - Shafi
<Purpose				: - Fetch Data  for DESC_PROPERTY_USE_PROF_COMM


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
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.Controls;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Policy.Homeowners;
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Policy.Aspx.Homeowners
{
	/// <summary>
	/// Page class for AddPersArticleGenInformation.aspx.
	/// </summary>
	public class PolcyInlandMarineUnderwritingQue : Cms.Policies.policiesbase
	{

		
		protected System.Web.UI.WebControls.DropDownList cmbPROPERTY_EXHIBITED;
		protected System.Web.UI.WebControls.TextBox txtDESC_PROPERTY_EXHIBITED;
		protected System.Web.UI.WebControls.DropDownList cmbDEDUCTIBLE_APPLY;
		protected System.Web.UI.WebControls.TextBox txtDESC_DEDUCTIBLE_APPLY;
		protected System.Web.UI.WebControls.DropDownList cmbPROPERTY_USE_PROF_COMM;
		protected System.Web.UI.WebControls.DropDownList cmbOTHER_INSU_WITH_COMPANY;
		protected System.Web.UI.WebControls.TextBox txtDESC_INSU_WITH_COMPANY;
		protected System.Web.UI.WebControls.DropDownList cmbLOSS_OCCURED_LAST_YEARS;
		protected System.Web.UI.WebControls.TextBox txtDESC_LOSS_OCCURED_LAST_YEARS;
		protected System.Web.UI.WebControls.DropDownList cmbDECLINED_CANCELED_COVERAGE;
		protected System.Web.UI.WebControls.TextBox txtADD_RATING_COV_INFO;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.Label lblMessage;
		//protected System.Web.UI.HtmlControls.HtmlTableRow



		
		//START:*********** Local form variables *************
		//string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId = "New";
		private string strFormSaved;
		protected System.Web.UI.WebControls.Label capPROPERTY_EXHIBITED;
		protected System.Web.UI.WebControls.Label capDESC_PROPERTY_EXHIBITED;
		protected System.Web.UI.WebControls.Label capDEDUCTIBLE_APPLY;
		protected System.Web.UI.WebControls.Label capDESC_DEDUCTIBLE_APPLY;
		protected System.Web.UI.WebControls.Label capPROPERTY_USE_PROF_COMM;
		protected System.Web.UI.WebControls.Label capOTHER_INSU_WITH_COMPANY;
		protected System.Web.UI.WebControls.Label capDESC_INSU_WITH_COMPANY;
		protected System.Web.UI.WebControls.Label capLOSS_OCCURED_LAST_YEARS;
		protected System.Web.UI.WebControls.Label capDESC_LOSS_OCCURED_LAST_YEARS;
		protected System.Web.UI.WebControls.Label capDECLINED_CANCELED_COVERAGE;
		protected System.Web.UI.WebControls.Label capADD_RATING_COV_INFO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.HtmlControls.HtmlForm APP_HOME_OWNER_PER_ART_GEN_INFO;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMsg;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEDUCTIBLE_APPLY;
		//Defining the business layer class object
		ClsPersArticleGenInformation   objPersArticleGenInformation;
		protected System.Web.UI.WebControls.Label lblDESC_PROPERTY_EXHIBITED;
		protected System.Web.UI.WebControls.Label lblDESC_DEDUCTIBLE_APPLY;
		protected System.Web.UI.WebControls.Label lblDESC_INSU_WITH_COMPANY;
		protected System.Web.UI.WebControls.Label lblDESC_LOSS_OCCURED_LAST_YEARS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_PROPERTY_EXHIBITED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_DEDUCTIBLE_APPLY;
		protected System.Web.UI.WebControls.Label capDESC_PROPERTY_USE_PROF_COMM;
		protected System.Web.UI.WebControls.TextBox txtDESC_PROPERTY_USE_PROF_COMM;
		protected System.Web.UI.WebControls.Label lblDESC_PROPERTY_USE_PROF_COMM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_PROPERTY_USE_PROF_COMM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_INSU_WITH_COMPANY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_LOSS_OCCURED_LAST_YEARS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvADD_RATING_COV_INFO;
		string strCalledFrom="";
		//END:*********** Local variables *************
		
		


		
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting ScreenId
			// Check from where the screen is called.
			if (Request.QueryString["CalledFrom"] != null || Request.QueryString["CalledFrom"] !="")
			{
				strCalledFrom=Request.QueryString["CalledFrom"].ToString();
				
			}
		
			//Setting screen Id.	
			switch (strCalledFrom.ToUpper()) 
			{
				case "HOME":
					base.ScreenId="242";
					break;
				case "RENTAL":
					base.ScreenId="1164";
					break;
				default:
					base.ScreenId="242";
					break;
			}
			#endregion
			btnReset.Attributes.Add("onclick","javascript:return ClearForm();");

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			lblMessage.Visible = false;

			SetErrorMessage();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		= CmsButtonType.Write;
			btnReset.PermissionString	= gstrSecurityXML;
			
			btnSave.CmsButtonClass		= CmsButtonType.Write;
			btnSave.PermissionString	= gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			if(!Page.IsPostBack)
			{

				GetSessionValues();
				
				if (hidPOLICY_ID.Value == "" || hidPOLICY_VERSION_ID.Value == "" || hidCUSTOMER_ID.Value == "")
				{
					capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("109");
					capMessage.Visible=true;
					
					trBody.Attributes.Add("style","display:none");
					trMsg.Attributes.Add("style","display:inline");
					return;
				}
				else
				{
					trBody.Attributes.Add("style","display:inline");
					trMsg.Attributes.Add("style","display:none");

					cltClientTop.CustomerID = int.Parse(hidCUSTOMER_ID.Value);
					cltClientTop.PolicyID = int.Parse(hidPOLICY_ID.Value);
					cltClientTop.PolicyVersionID = int.Parse(hidPOLICY_VERSION_ID.Value);
					cltClientTop.ShowHeaderBand = "Policy";
					cltClientTop.Visible = true;
				}
				FillControls();
				//GetOldDataXML();
				LoadData();
				SetCaptions();
				SetValidators();
			}

			//Setting the workflow
			SetWorkflow();
		}//end pageload
		
		

		private void SetValidators()
		{
			rfvDEDUCTIBLE_APPLY.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("559");
		}
		
		

		
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsPolicyPersArticleGenInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsPolicyPersArticleGenInfo objPersArticleGenInfo = new ClsPolicyPersArticleGenInfo();
		
			objPersArticleGenInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objPersArticleGenInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			objPersArticleGenInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);

			objPersArticleGenInfo.PROPERTY_EXHIBITED = cmbPROPERTY_EXHIBITED.SelectedValue;
			if (cmbPROPERTY_EXHIBITED.SelectedValue == "1")
			{
				objPersArticleGenInfo.DESC_PROPERTY_EXHIBITED =	txtDESC_PROPERTY_EXHIBITED.Text;
			}
			else
			{
				objPersArticleGenInfo.DESC_PROPERTY_EXHIBITED ="";	
			}
			
			objPersArticleGenInfo.DEDUCTIBLE_APPLY = cmbDEDUCTIBLE_APPLY.SelectedValue;
			if (cmbDEDUCTIBLE_APPLY.SelectedValue == "1")
			{
				objPersArticleGenInfo.DESC_DEDUCTIBLE_APPLY = txtDESC_DEDUCTIBLE_APPLY.Text;
			}
			else
			{
				objPersArticleGenInfo.DESC_DEDUCTIBLE_APPLY = ""; 
			}
			
			objPersArticleGenInfo.PROPERTY_USE_PROF_COMM = cmbPROPERTY_USE_PROF_COMM.SelectedValue;
			
			// Added by mohit on 20/10/2005--------.
			if (cmbPROPERTY_USE_PROF_COMM.SelectedValue =="1")
			{
				objPersArticleGenInfo.DESC_PROPERTY_USE_PROF_COMM=txtDESC_PROPERTY_USE_PROF_COMM.Text;
			}
			else
			{	
				objPersArticleGenInfo.DESC_PROPERTY_USE_PROF_COMM="";
			}
			//-------------End----------------------. 
			

			objPersArticleGenInfo.OTHER_INSU_WITH_COMPANY = cmbOTHER_INSU_WITH_COMPANY.SelectedValue;
			if (cmbOTHER_INSU_WITH_COMPANY.SelectedValue == "1")
			{
				objPersArticleGenInfo.DESC_INSU_WITH_COMPANY = txtDESC_INSU_WITH_COMPANY.Text;
			}
			else
			{
				objPersArticleGenInfo.DESC_INSU_WITH_COMPANY = "";
			}
			
			objPersArticleGenInfo.LOSS_OCCURED_LAST_YEARS = cmbLOSS_OCCURED_LAST_YEARS.SelectedValue;
			if (cmbLOSS_OCCURED_LAST_YEARS.SelectedValue =="1")
			{
				objPersArticleGenInfo.DESC_LOSS_OCCURED_LAST_YEARS = txtDESC_LOSS_OCCURED_LAST_YEARS.Text;
			}
			else
			{
				objPersArticleGenInfo.DESC_LOSS_OCCURED_LAST_YEARS = "";
			}
			objPersArticleGenInfo.DECLINED_CANCELED_COVERAGE = cmbDECLINED_CANCELED_COVERAGE.SelectedValue;
			
			objPersArticleGenInfo.ADD_RATING_COV_INFO = txtADD_RATING_COV_INFO.Text;

		
			if ( this.hidOldData.Value == "" )
			{
				this.strRowId = "New";
			}
			else
			{
				this.strRowId = "1";
			}

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			
			return objPersArticleGenInfo;
		}
		
		private void SetErrorMessage()
		{
			//rfvADD_RATING_COV_INFO.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416");
			//rfvDEDUCTIBLE_APPLY.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416");
			rfvDESC_DEDUCTIBLE_APPLY.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416");
			rfvDESC_INSU_WITH_COMPANY.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416");
			rfvDESC_LOSS_OCCURED_LAST_YEARS.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416");
			rfvDESC_PROPERTY_EXHIBITED.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416");
			rfvDESC_PROPERTY_USE_PROF_COMM.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416");

		
		
		}

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
				objPersArticleGenInformation = new  ClsPersArticleGenInformation();

				//Retreiving the form values into model class object
				ClsPolicyPersArticleGenInfo objPersArticleGenInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objPersArticleGenInfo.CREATED_BY = int.Parse(GetUserId());
					objPersArticleGenInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objPersArticleGenInformation.AddGenInfo(objPersArticleGenInfo);

					if(intRetVal>0)
					{
						//hidPOLICY_VERSION_ID.Value = objPersArticleGenInfo.POLICY_VERSION_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						//GetOldDataXML();
						LoadData();
						SetWorkflow();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
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
					ClsPolicyPersArticleGenInfo objOldPersArticleGenInfo = new ClsPolicyPersArticleGenInfo();
					

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldPersArticleGenInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objPersArticleGenInfo.POLICY_VERSION_ID = int.Parse(this.hidPOLICY_VERSION_ID.Value);
					objPersArticleGenInfo.MODIFIED_BY = int.Parse(GetUserId());
					objPersArticleGenInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objPersArticleGenInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal = objPersArticleGenInformation.UpdateGenInfo(objOldPersArticleGenInfo, objPersArticleGenInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						//GetOldDataXML();
						LoadData();
						SetWorkflow();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
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
				//ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objPersArticleGenInformation != null)
					objPersArticleGenInformation.Dispose();
			}
		}
		

		/// <summary>
		/// Sets the caption of labels on page from resource
		/// </summary>
		private void SetCaptions()
		{
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policy.Aspx.Homeowners.PolcyInlandMarineUnderwritingQue" ,System.Reflection.Assembly.GetExecutingAssembly());

			capPROPERTY_EXHIBITED.Text		= objResourceMgr.GetString("cmbPROPERTY_EXHIBITED");
			capDESC_PROPERTY_EXHIBITED.Text	= objResourceMgr.GetString("txtDESC_PROPERTY_EXHIBITED");
			capDEDUCTIBLE_APPLY.Text		= objResourceMgr.GetString("cmbDEDUCTIBLE_APPLY");
			capDESC_DEDUCTIBLE_APPLY.Text	= objResourceMgr.GetString("txtDESC_DEDUCTIBLE_APPLY");
			capPROPERTY_USE_PROF_COMM.Text	= objResourceMgr.GetString("cmbPROPERTY_USE_PROF_COMM");
			capOTHER_INSU_WITH_COMPANY.Text	= objResourceMgr.GetString("cmbOTHER_INSU_WITH_COMPANY");
			capDESC_INSU_WITH_COMPANY.Text	= objResourceMgr.GetString("txtDESC_INSU_WITH_COMPANY");
			capLOSS_OCCURED_LAST_YEARS.Text	= objResourceMgr.GetString("cmbLOSS_OCCURED_LAST_YEARS");
			capDESC_LOSS_OCCURED_LAST_YEARS.Text = objResourceMgr.GetString("txtDESC_LOSS_OCCURED_LAST_YEARS");
			capDECLINED_CANCELED_COVERAGE.Text = objResourceMgr.GetString("cmbDECLINED_CANCELED_COVERAGE");
			capADD_RATING_COV_INFO.Text		= objResourceMgr.GetString("txtADD_RATING_COV_INFO");
			capDESC_PROPERTY_USE_PROF_COMM.Text=objResourceMgr.GetString("txtDESC_PROPERTY_USE_PROF_COMM");
			
		}

		
		/// <summary>
		/// Generates the XML of current record on page.
		/// </summary>
		private void GetOldDataXML()
		{
			ClsPersArticleGenInformation objPers = new ClsPersArticleGenInformation();

			DataSet ds = objPers.GetPolPersArticleGenInformation(int.Parse(hidCUSTOMER_ID.Value)
				, int.Parse(hidPOLICY_ID.Value)
				, int.Parse(hidPOLICY_VERSION_ID.Value));		

			hidOldData.Value = ds.GetXml();	

		}

		private void LoadData()
		{
			ClsPersArticleGenInformation objPers = new ClsPersArticleGenInformation();

			DataSet ds = objPers.GetPolPersArticleGenInformation(int.Parse(hidCUSTOMER_ID.Value)
				, int.Parse(hidPOLICY_ID.Value)
				, int.Parse(hidPOLICY_VERSION_ID.Value));		

			

			DataTable dt = ds.Tables[0];

			if ( dt.Rows.Count == 0 ) 
			{
				strRowId = "New";
				return;
			}

			ClsCommon.SelectValueinDDL(this.cmbPROPERTY_EXHIBITED,dt.Rows[0]["PROPERTY_EXHIBITED"]);
			ClsCommon.SelectValueinDDL(this.cmbDEDUCTIBLE_APPLY,dt.Rows[0]["DEDUCTIBLE_APPLY"]);
			ClsCommon.SelectValueinDDL(this.cmbPROPERTY_USE_PROF_COMM,dt.Rows[0]["PROPERTY_USE_PROF_COMM"]);
			ClsCommon.SelectValueinDDL(this.cmbOTHER_INSU_WITH_COMPANY,dt.Rows[0]["OTHER_INSU_WITH_COMPANY"]);
			ClsCommon.SelectValueinDDL(this.cmbLOSS_OCCURED_LAST_YEARS,dt.Rows[0]["LOSS_OCCURED_LAST_YEARS"]);
			ClsCommon.SelectValueinDDL(this.cmbDECLINED_CANCELED_COVERAGE,dt.Rows[0]["DECLINED_CANCELED_COVERAGE"]);
			
			txtDESC_PROPERTY_EXHIBITED.Text = Default.GetString(dt.Rows[0]["DESC_PROPERTY_EXHIBITED"]);
			txtDESC_DEDUCTIBLE_APPLY.Text = Default.GetString(dt.Rows[0]["DESC_DEDUCTIBLE_APPLY"]);
			txtDESC_INSU_WITH_COMPANY.Text = Default.GetString(dt.Rows[0]["DESC_INSU_WITH_COMPANY"]);
			txtDESC_LOSS_OCCURED_LAST_YEARS.Text = Default.GetString(dt.Rows[0]["DESC_LOSS_OCCURED_LAST_YEARS"]);
			txtADD_RATING_COV_INFO.Text = Default.GetString(dt.Rows[0]["ADD_RATING_COV_INFO"]);
            //Added By Shafi 09/01/06
			txtDESC_PROPERTY_USE_PROF_COMM.Text =Default.GetString(dt.Rows[0]["DESC_PROPERTY_USE_PROF_COMM"]);


			hidOldData.Value = ds.GetXml();	

			strRowId = hidPOLICY_VERSION_ID.Value;

		}

		/// <summary>
		/// Retreive the query string coming in url.
		/// </summary>
		private void GetSessionValues()
		{
			hidCUSTOMER_ID.Value = GetCustomerID();
			hidPOLICY_ID.Value = GetPolicyID();
			hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();
		}

		private void SetWorkflow()
		{
			if(base.ScreenId	==	"242" || base.ScreenId == "164" || base.ScreenId == "444")
			{
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCUSTOMER_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPOLICY_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPOLICY_VERSION_ID.Value);

				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
				myWorkFlow.WorkflowModule="POL";
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}

		private void FillControls()
		{
			
				IList lstYesNo =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");


				cmbPROPERTY_EXHIBITED.DataSource =lstYesNo;
				cmbPROPERTY_EXHIBITED.DataTextField="LookupDesc"; 
				cmbPROPERTY_EXHIBITED.DataValueField="LookupCode";
				cmbPROPERTY_EXHIBITED.DataBind();
				cmbPROPERTY_EXHIBITED.Items.Insert(0,"");


				cmbDEDUCTIBLE_APPLY.DataSource =lstYesNo;
				cmbDEDUCTIBLE_APPLY.DataTextField="LookupDesc"; 
				cmbDEDUCTIBLE_APPLY.DataValueField="LookupCode";
				cmbDEDUCTIBLE_APPLY.DataBind();
				cmbDEDUCTIBLE_APPLY.Items.Insert(0,"");

				cmbPROPERTY_USE_PROF_COMM.DataSource =lstYesNo;
				cmbPROPERTY_USE_PROF_COMM.DataTextField="LookupDesc"; 
				cmbPROPERTY_USE_PROF_COMM.DataValueField="LookupCode";
				cmbPROPERTY_USE_PROF_COMM.DataBind();
				cmbPROPERTY_USE_PROF_COMM.Items.Insert(0,"");

				cmbOTHER_INSU_WITH_COMPANY.DataSource =lstYesNo;
				cmbOTHER_INSU_WITH_COMPANY.DataTextField="LookupDesc"; 
				cmbOTHER_INSU_WITH_COMPANY.DataValueField="LookupCode";
				cmbOTHER_INSU_WITH_COMPANY.DataBind();
				cmbOTHER_INSU_WITH_COMPANY.Items.Insert(0,"");
				
				cmbLOSS_OCCURED_LAST_YEARS.DataSource =lstYesNo;
				cmbLOSS_OCCURED_LAST_YEARS.DataTextField="LookupDesc"; 
				cmbLOSS_OCCURED_LAST_YEARS.DataValueField="LookupCode";
				cmbLOSS_OCCURED_LAST_YEARS.DataBind();
				cmbLOSS_OCCURED_LAST_YEARS.Items.Insert(0,"");

				cmbDECLINED_CANCELED_COVERAGE.DataSource =lstYesNo; 
				cmbDECLINED_CANCELED_COVERAGE.DataTextField="LookupDesc"; 
				cmbDECLINED_CANCELED_COVERAGE.DataValueField="LookupCode";
				cmbDECLINED_CANCELED_COVERAGE.DataBind();
				cmbDECLINED_CANCELED_COVERAGE.Items.Insert(0,"");

		
		}
	}
}
