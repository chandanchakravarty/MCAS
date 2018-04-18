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
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;

/******************************************************************************************
	<Author					: Priya Arora- >
	<Start Date				: Apr 25,2005-	>
	<End Date				: Apr 25,2005- >
	<Description			: This screen is used to add an attention note against the customer- >
	<Review Date			: - >
	<Reviewed By			: - >
	

	Modification History

	<Modified Date			: - > 22/6/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Changing image of client top control after change in attention note

	<Modified Date			: - > 09/11/2006
	<Modified By			: - > Mohit Agarwal
	<Purpose				: - > Added a function and tags in resx for adding data in transaction log
*******************************************************************************************/

namespace Cms.Client.Aspx
{
	/// <summary>
	/// Summary description for AttentionNotes.
	/// </summary>
	public class AttentionNotes : Cms.Client.clientbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_ATTENTION_NOTE;

		protected Cms.CmsWeb.Controls.CmsButton btnAddNewQuickQuote;
		protected Cms.CmsWeb.Controls.CmsButton btnAddNewApplication;

		System.Resources.ResourceManager objResourceMgr;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.Label capCUSTOMER_ATTENTION_NOTE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		int			intCUSTOMER_ID = 0;
		//string		strRowId ;
        public string strResult="";
		public string rootPathRed="";
		public string rootPathGrey="";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected System.Web.UI.WebControls.Label capAttentionNoteUpdated;
		protected System.Web.UI.WebControls.Label txtAttentionNoteUpdated;
		protected System.Web.UI.WebControls.CustomValidator csvCUSTOMER_ATTENTION_NOTE;
		protected System.Web.UI.WebControls.Label capATTENTION_NOTE;
		protected System.Web.UI.WebControls.TextBox txtATTENTION_NOTES;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE;
        public string head;
		int			intLoggedInUserID;

		private void Page_Load(object sender, System.EventArgs e)
		{
			intLoggedInUserID		=	int.Parse(base.GetUserId());
			if(Request.QueryString["ClientID"] != null && Request.QueryString["ClientID"] != "")
			{
				base.ScreenId			= "134_3";//can be Alpha Numeric 
				  
			}
			else
			{
				//Done for Itrack Issue 6539 on 9 Oct 09
				base.ScreenId			= "134_3";//can be Alpha Numeric
				//base.ScreenId			= "192_3";//can be Alpha Numeric
//				base.ScreenId			= "134_6";//can be Alpha Numeric 

			}
			//base.ScreenId			=	"134_3";//can be Alpha Numeric 
			   hidTYPE.Value             = base.ScreenId.ToString();
			
			// code for providing the back to serach button only on customer Detail form tabs. 
			if (Request.QueryString["BackOption"] != null && Request.QueryString["BackOption"].ToString()=="Y")
			{
				btnBack.Visible = true;
			}

			rootPathRed=Request.ApplicationPath.ToString() + "/cmsweb/images/att-ecs.gif"; 
			rootPathGrey=Request.ApplicationPath.ToString() + "/cmsweb/images/att-ecs-grey.gif"; 

			
			btnReset.Attributes.Add("onclick","javascript:return ResetForm();");
			btnBack.Attributes.Add("onclick","javascript:return BackToCustomer();");

            //Set the Culture Language Added By Lalit March 17,2010

            SetCultureThread(GetLanguageCode());
			objResourceMgr = new System.Resources.ResourceManager("Cms.Client.Aspx.AttentionNotes",System.Reflection.Assembly.GetExecutingAssembly());
			//txtCUSTOMER_ATTENTION_NOTE.Attributes.Add("OnKeyPress","javascript:MaxLength(this);");
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				
			btnBack.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Read;
			btnBack.PermissionString				=	gstrSecurityXML;	

			
			btnReset.CmsButtonClass					=		Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString				=		gstrSecurityXML;	
				
		//Added by Sibin on 07-10-08- Permission given Write instead of Read for Save Button
			
			btnSave.CmsButtonClass					=		Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString				=		gstrSecurityXML;	
			
			btnAddNewQuickQuote.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnAddNewQuickQuote.PermissionString				=	gstrSecurityXML;	

			btnAddNewApplication.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnAddNewApplication.PermissionString				=	gstrSecurityXML;	

			btnAddNewApplication.Attributes.Add("onclick","javascript:return GoToNewApplication();");
			btnAddNewQuickQuote.Attributes.Add("onclick","javascript:return GoToNewQuote();");


			if(!IsPostBack)
			{
				SetCaptions();
				hidCUSTOMER_ID.Value				=		GetCustomerID();//Request.Params["CustomerID"];
				intCUSTOMER_ID						=		Convert.ToInt32(hidCUSTOMER_ID.Value);
				GetOldData(true);
				//AttentionNotesDetails();
				csvCUSTOMER_ATTENTION_NOTE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("439");
				//Temporary Session to indicate that a new customer has been added.
				//Session set to null here so that message at customer page does not repeat
				if(Session["Insert"]!=null && Session["Insert"].ToString()!="")
					Session["Insert"]=null;
			}
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
		}
		private void SetCaptions()
		{
			capAttentionNoteUpdated.Text		=		objResourceMgr.GetString("txtAttentionNoteUpdated");
			capCUSTOMER_ATTENTION_NOTE.Text			=		objResourceMgr.GetString("txtCUSTOMER_ATTENTION_NOTE");
            capCUSTOMER_ATTENTION_NOTE.Text =objResourceMgr.GetString("capCUSTOMER_ATTENTION_NOTE");
            capAttentionNoteUpdated.Text = objResourceMgr.GetString("capAttentionNoteUpdated");
            btnAddNewApplication.Text = objResourceMgr.GetString("btnAddNewApplication");
            head = objResourceMgr.GetString("head");
		}

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

		private void btnReset_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			SaveFormValue();
		}

		private void SaveFormValue()
		{
			int intRetVal;	//For retreiving the return value of business class save function
				
			// Creating Business layer object to do processing
			
			ClsCustomer objCustomer = new  ClsCustomer();
			
			//Creating the Model object for holding the New data
			Model.Client.ClsCustomerInfo objNewCustomer = new Cms.Model.Client.ClsCustomerInfo();

			//Creating the Model object for holding the Old data
			Model.Client.ClsCustomerInfo objOldCustomer = new Cms.Model.Client.ClsCustomerInfo();

			//objCustomer = new  Cms.BusinessLayer.BlClient.ClsCustomer();
			
			try
			{
				intCUSTOMER_ID		= int.Parse(hidCUSTOMER_ID.Value);
					
				
				objNewCustomer.Customer_Attention_Note = txtCUSTOMER_ATTENTION_NOTE.Text;
				objNewCustomer.ATTENTION_NOTE_UPDATED  = DateTime.Now;
				
				//GetOldData(true);
				//Setting  the Old Page details(XML File containing old details) into the Model Object
				base.PopulateModelObject(objOldCustomer,hidOldData.Value);
					
				//Setting those values into the Model object which are not in the page
				objNewCustomer.CustomerId				=	intCUSTOMER_ID;
				objNewCustomer.MODIFIED_BY				=	int.Parse(GetUserId());
				objNewCustomer.CREATED_BY				=   int.Parse(GetUserId());
				objNewCustomer.LAST_UPDATED_DATETIME	=	DateTime.Now;
					
				//Setting those values into the Model object which are not in the page
				objOldCustomer.CustomerId		=	intCUSTOMER_ID;
					
				intRetVal						=	objCustomer.UpdateAttentionNotes(objOldCustomer,objNewCustomer);
					
				if( intRetVal > 0 )			// update successfully performed
				{
					lblMessage.Text			   =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
					GetOldData(false);
					txtAttentionNoteUpdated.Text =   objNewCustomer.ATTENTION_NOTE_UPDATED.ToString();
					hidFormSaved.Value		   =	"1";
                    if(objNewCustomer.Customer_Attention_Note.Trim()=="" || objNewCustomer.Customer_Attention_Note.Trim() ==null)
                        strResult                   = "1";

				}
				else if(intRetVal == -1)	// Duplicate code exist, update failed
				{	
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
					hidFormSaved.Value		=	"2";
				}
				else						// Error occured while processing, update failed
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
					hidFormSaved.Value		=	"2";
				}
				lblMessage.Visible=true;
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			}
		}
		
		private void AttentionNotesDetails(DataTable dtAttentionNotes)
		{
			
			txtCUSTOMER_ATTENTION_NOTE.Text = dtAttentionNotes.Rows[0][0].ToString();
			txtAttentionNoteUpdated.Text = dtAttentionNotes.Rows[0][1].ToString();

            
                if(txtCUSTOMER_ATTENTION_NOTE.Text.Trim()=="" || txtCUSTOMER_ATTENTION_NOTE.Text.Trim() ==null)
                    strResult                   = "1";
		}
		private void GetOldData(bool flag)
		{
			DataSet dsAttentionNotes = new DataSet();
			ClsCustomer objCustomer = new ClsCustomer();

			dsAttentionNotes = objCustomer.ViewAttentionNotes(intCUSTOMER_ID.ToString());
			hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsAttentionNotes.Tables[0]);
			if(flag)
				AttentionNotesDetails(dsAttentionNotes.Tables[0]);
		}
		private void txtAttentionNote_TextChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
