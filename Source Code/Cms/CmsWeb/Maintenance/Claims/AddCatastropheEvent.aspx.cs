/******************************************************************************************
<Author					: -		Vijay Arora
<Start Date				: -		4/24/2006 2:35:29 PM
<End Date				: -	
<Description			: - 	Class for Catastrophe Event Types
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
using Cms.BusinessLayer.BlCommon;

namespace Cms.CmsWeb.Maintenance.Claims
{
	/// <summary>
	/// 
	/// </summary>
	public class AddCatastropheEvent : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.DropDownList cmbCATASTROPHE_EVENT_TYPE;
		protected System.Web.UI.WebControls.TextBox txtDATE_FROM;
		protected System.Web.UI.WebControls.TextBox txtDATE_TO;
		protected System.Web.UI.WebControls.TextBox txtDESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtCAT_CODE;
		protected System.Web.UI.WebControls.Label capCATASTROPHE_EVENT_TYPE;
		protected System.Web.UI.WebControls.Label capDATE_FROM;
		protected System.Web.UI.WebControls.Label capDATE_TO;
		protected System.Web.UI.WebControls.Label capDESCRIPTION;
		protected System.Web.UI.WebControls.Label capCAT_CODE;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCATASTROPHE_EVENT_ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trbody;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCATASTROPHE_EVENT_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE_FROM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE_TO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESCRIPTION;
		protected System.Web.UI.WebControls.HyperLink hlkDATE_FROM;
		protected System.Web.UI.WebControls.HyperLink hlkDATE_TO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_FROM;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_TO;
		protected System.Web.UI.WebControls.CustomValidator csvDATE_TO;
        protected System.Web.UI.WebControls.Label capMessages;


        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDATE_FROM_TEXT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDATE_TO_TEXT;
		#endregion

		#region Local form variables
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		ClsCatastropheEvent  objCE = new ClsCatastropheEvent();
		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvCATASTROPHE_EVENT_TYPE.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvDATE_FROM.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvDATE_TO.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvDESCRIPTION.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			revDATE_FROM.ValidationExpression	= aRegExpDate;
			revDATE_FROM.ErrorMessage			=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revDATE_TO.ValidationExpression	= aRegExpDate;
			revDATE_TO.ErrorMessage			=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			csvDATE_TO.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
		}
		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			hlkDATE_FROM.Attributes.Add("OnClick","fPopCalendar(document.CLM_CATASTROPHE_EVENT.txtDATE_FROM ,document.CLM_CATASTROPHE_EVENT.txtDATE_FROM)"); //Javascript Implementation for Calender		3
			hlkDATE_TO.Attributes.Add("OnClick","fPopCalendar(document.CLM_CATASTROPHE_EVENT.txtDATE_TO ,document.CLM_CATASTROPHE_EVENT.txtDATE_TO)"); //Javascript Implementation for Calender		

			#region Setting Screen ID
			base.ScreenId="299_0";
			#endregion

			lblMessage.Visible = false;
			SetErrorMessages();
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write; //Permission made Write instead of Execute by Sibin on 22 Oct 08
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;  //Permission made Write instead of Execute by Sibin on 22 Oct 08
			btnSave.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;  //Permission made Write instead of Execute by Sibin on 22 Oct 08
			btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Claims.AddCatastropheEvent" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
                if (Request.QueryString["CATASTROPHE_EVENT_ID"] != null && Request.QueryString["CATASTROPHE_EVENT_ID"].ToString().Length > 0)
                {
                    hidCATASTROPHE_EVENT_ID.Value = Request.QueryString["CATASTROPHE_EVENT_ID"].ToString();
                }
                else
                {
                    hidCATASTROPHE_EVENT_ID.Value = "";
                }
				GetOldDataXML();

				SetCaptions();

				#region "Loading singleton"
                DataTable dtTemp = ClsDefaultValues.GetDefaultValuesDetails(1);
				cmbCATASTROPHE_EVENT_TYPE.DataSource = dtTemp;
				cmbCATASTROPHE_EVENT_TYPE.DataTextField = "DETAIL_TYPE_DESCRIPTION";
				cmbCATASTROPHE_EVENT_TYPE.DataValueField = "DETAIL_TYPE_ID";
				cmbCATASTROPHE_EVENT_TYPE.DataBind();
				#endregion
			}
		}//end pageload
		#endregion
		
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsCatastropheEventInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsCatastropheEventInfo objCEInfo = new ClsCatastropheEventInfo();

			objCEInfo.CATASTROPHE_EVENT_TYPE=	int.Parse(cmbCATASTROPHE_EVENT_TYPE.SelectedValue);
			objCEInfo.DATE_FROM=	Convert.ToDateTime(txtDATE_FROM.Text);
			objCEInfo.DATE_TO=	Convert.ToDateTime(txtDATE_TO.Text);
			objCEInfo.DESCRIPTION=	txtDESCRIPTION.Text;
			objCEInfo.CAT_CODE=	txtCAT_CODE.Text;

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidCATASTROPHE_EVENT_ID.Value;
		
			//Returning the model object
			return objCEInfo;
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
				objCE = new  ClsCatastropheEvent();

				//Retreiving the form values into model class object
				ClsCatastropheEventInfo objCEInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objCEInfo.CREATED_BY = int.Parse(GetUserId());
					objCEInfo.CREATED_DATETIME = DateTime.Now; 
					
					//Calling the add method of business layer class
					intRetVal = objCE.Add(objCEInfo);

					if(intRetVal>0)
					{
						hidCATASTROPHE_EVENT_ID.Value = objCEInfo.CATASTROPHE_EVENT_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						//hidOldData.Value = ClsCatastropheEvent.GetXmlForPageControls(hidCATASTROPHE_EVENT_ID.Value);

                        GetOldDataXML();

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
					ClsCatastropheEventInfo objOldCEInfo;
					objOldCEInfo = new ClsCatastropheEventInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldCEInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objCEInfo.CATASTROPHE_EVENT_ID = int.Parse(strRowId);
					objCEInfo.MODIFIED_BY = int.Parse(GetUserId());
					objCEInfo.LAST_UPDATED_DATETIME = DateTime.Now; 
					
					//Updating the record using business layer class object
					intRetVal	= objCE.Update(objOldCEInfo,objCEInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
                        hidCATASTROPHE_EVENT_ID.Value = objCEInfo.CATASTROPHE_EVENT_ID.ToString();
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						//hidOldData.Value = ClsCatastropheEvent.GetXmlForPageControls(strRowId);
                        GetOldDataXML();
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
				if(objCE!= null)
					 objCE.Dispose();
			}
		}
		#endregion

		#region Set Caption and Get Old Data XML
		private void SetCaptions()
		{
			capCATASTROPHE_EVENT_TYPE.Text	=		objResourceMgr.GetString("cmbCATASTROPHE_EVENT_TYPE");
			capDATE_FROM.Text				=		objResourceMgr.GetString("txtDATE_FROM");
			capDATE_TO.Text					=		objResourceMgr.GetString("txtDATE_TO");
			capDESCRIPTION.Text				=		objResourceMgr.GetString("txtDESCRIPTION");
			capCAT_CODE.Text				=		objResourceMgr.GetString("txtCAT_CODE");
            //btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");
		}
		private void GetOldDataXML()
		{
            if (hidCATASTROPHE_EVENT_ID.Value!="")
            {
                btnActivateDeactivate.Visible = true;
                hidOldData.Value = ClsCatastropheEvent.GetXmlForPageControls(hidCATASTROPHE_EVENT_ID.Value);

                string strDATE_FROM = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("DATE_FROM", hidOldData.Value);
                string strDATE_TO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("DATE_TO", hidOldData.Value);
                string strIS_ACTIVE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value);



                if (strDATE_FROM != null && strDATE_FROM != "")
                    hidDATE_FROM_TEXT.Value = ConvertToDateCulture(Convert.ToDateTime(strDATE_FROM));
                    //hidDATE_FROM_TEXT.Value = strDATE_FROM.Trim();
                else
                    hidDATE_FROM_TEXT.Value = "";

                if (strDATE_TO != null && strDATE_TO != "")
                    hidDATE_TO_TEXT.Value = ConvertToDateCulture(Convert.ToDateTime(strDATE_TO));
                    //hidDATE_TO_TEXT.Value = strDATE_TO.Trim();
                else
                    hidDATE_TO_TEXT.Value = "";


                if (strIS_ACTIVE == "Y")
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315"); //Deactivate";
                else
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314"); //Activate";
            }
            else
                btnActivateDeactivate.Visible = false;

	     }
		#endregion	

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				ClsCatastropheEvent objBL = new ClsCatastropheEvent();				
				string strRetVal = "";

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
					objBL.TransactionInfoParams = objStuTransactionInfo;
					strRetVal = objBL.ActivateDeactivate(hidCATASTROPHE_EVENT_ID.Value,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					objBL.TransactionInfoParams = objStuTransactionInfo;
					objBL.ActivateDeactivate(hidCATASTROPHE_EVENT_ID.Value,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				
				if (strRetVal == "-1")
				{
					/*Profit Center is assigned*/
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"514");
				}

				lblMessage.Visible = true;
				hidFormSaved.Value			=	"0";
				ClientScript.RegisterStartupScript(this.GetType(),"RefreshGRid","<script>RefreshWebGrid(1,1,true)</script>");
				GetOldDataXML();
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		
		}
		
	}
}
