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
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb;
using Cms.Model.Maintenance.Security;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BlCommon;

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AddSecurityRights.
	/// </summary>
	public class AddSecurityRights : Cms.CmsWeb.cmsbase
	{
		#region Page Control Declarations
		protected System.Web.UI.WebControls.Label capSECTION;
		protected System.Web.UI.WebControls.Label capSUBSECTION;
		protected System.Web.UI.WebControls.Label capSELECTALL;
		protected System.Web.UI.WebControls.CheckBox  chkSELECTALL;
		protected System.Web.UI.WebControls.Label capAppPol;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPPPOL;
		protected System.Web.UI.WebControls.DropDownList cmbAPPPOL;
		protected System.Web.UI.WebControls.DropDownList cmbSECTION;
		protected System.Web.UI.WebControls.DropDownList cmbSUBSECTION;
        protected System.Web.UI.WebControls.Label capMessages;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppPol;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldDatas;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSECTION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSUBSECTION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSCREENLIST;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUSER_TYPE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUSER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCALLED_FOR;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMENU_XML;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidread;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidwrite;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidexecute;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hiddelete; 
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnGetScreen;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAll;
        string strCarrierSIN = "";

		System.Resources.ResourceManager objResourceMgr;
		#endregion
		
		#region local variable declaration
		ClsSecurity objSecurity;
		protected System.Web.UI.WebControls.Label caSELECTALL;
		int			intRetVal		=	0;
		#endregion

		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{
            strCarrierSIN = GetSystemId().ToString().ToUpper();
			string strCarrierSystemID = Cms.CmsWeb.cmsbase.CarrierSystemID;
			string strAgencyID = GetSystemId();
			if(strCarrierSystemID.ToUpper()!=strAgencyID.ToUpper())
				base.ScreenId	=	"10_1_2";
			else
				base.ScreenId	=	"21_1";
			
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			chkSELECTALL.Attributes.Add("onclick","javascript:checkComplete(this);");
            if (strCarrierSIN == "S001" || strCarrierSIN == "SUAT")
            {
                //cmbSUBSECTION.Attributes.Add("onChange", "javascript:fireAutoSelection();");
                //cmbSECTION.Attributes.Add("onChange", "javascript:fireAutoSelection();");
                btnGetScreen.Visible = false;
            }
                
            			
			lblMessage.Visible = false;
			SetErrorMessages();
            SetCaption();
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnGetScreen.CmsButtonClass	=	CmsButtonType.Read;//Permission Modified by Sibin on 9 Jan 08 for Itrack Issue 5272
			btnGetScreen.PermissionString	=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddSecurityRights" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				string strAgency=GetSystemId();
				if(Request.QueryString["CALLED_FOR"] != null && Request.QueryString["CALLED_FOR"] != "")
				{
					strAgency = Request.QueryString["CALLED_FOR"];
					hidCALLED_FOR.Value = strAgency;

					//					string mainpageVal = cmbSECTION.Items.FindByText("Diary").Value;
					//					cmbSECTION.Items.Remove(cmbSECTION.Items.FindByText("Diary"));
					//					cmbSECTION.Items.Add(new ListItem("Agent Home", "393"));
				}

				ClsCommon objBLCommon = new ClsCommon();
				hidMENU_XML.Value = objBLCommon.GetDefaultMenuXml(strAgency);
				GetSessionValues();
				GetOldDataXML();
				SetCaptions();
				FillCombo();

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
            
			this.btnGetScreen.Click += new System.EventHandler(this.btnGetScreen_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);
            this.cmbSECTION.SelectedIndexChanged += new EventHandler(cmbSECTION_SelectedIndexChanged); 
		}
		#endregion

		#region Get Session Values
		private void GetSessionValues()
		{
			this.hidUSER_TYPE_ID.Value	= Request.QueryString["UserTypeId"];
			this.hidUSER_ID.Value		= Request.QueryString["UserId"] == null ? "0" : Request.QueryString["UserId"];
		}
		#endregion

		#region Get Old Data
		private void GetOldDataXML()
		{
		}
		#endregion

		#region Set Captions
		 private void SetCaptions()
        {
            capSECTION.Text = objResourceMgr.GetString("cmbSECTION");
            capSELECTALL.Text = objResourceMgr.GetString("chkSELECTALL");
            capSUBSECTION.Text = objResourceMgr.GetString("cmbSUBSECTION");
            capAppPol.Text = objResourceMgr.GetString("cmbAppPol");
            btnGetScreen.Text = objResourceMgr.GetString("btnGetScreen");
            hidAll.Value = objResourceMgr.GetString("hidAll");
            hidread.Value = objResourceMgr.GetString("hidread");
            hidwrite.Value = objResourceMgr.GetString("hidwrite");
            hiddelete.Value = objResourceMgr.GetString("hiddelete");
            hidexecute.Value = objResourceMgr.GetString("hidexecute");

		}
		#endregion

		#region Fill Combo
		private void FillCombo()
         {
             if (ClsCommon.BL_LANG_ID ==2)
             {
                 cmbAPPPOL.Items.Insert(0, new ListItem("", ""));
                 cmbAPPPOL.Items.Insert(1, new ListItem("Proposta", "1"));
                 cmbAPPPOL.Items.Insert(2, new ListItem("Apolice", "2"));
             }
             else
             {
                 cmbAPPPOL.Items.Insert(0, new ListItem("", ""));
                 cmbAPPPOL.Items.Insert(1, new ListItem("Application", "1"));
                 cmbAPPPOL.Items.Insert(2, new ListItem("Policy", "2"));
             }
		}
		#endregion
        #region SetCaption
        private void SetCaption()
        {
            
        }
        #endregion
		#region Set Error Messages
		private void SetErrorMessages()
		{
		}
		#endregion

		#region Web Event Handler
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string[]	screenList;
			string permissionXML			=	"";
			screenList		=	hidSCREENLIST.Value.Split('~');
			ClsSecurityInfo	objSecurityInfo =	new ClsSecurityInfo();
			ClsSecurity		objSecurity		=	new ClsSecurity();
			objSecurity.LoadOldData(hidOldDatas.Value);
			foreach(string screen in screenList)
			{
				permissionXML				=	"<security>";
				if(screen != "")
				{
					if(Request[screen + "_read"] != null)
					{
						permissionXML		+=	"<Read>Y</Read>";
					}
					else
					{
						permissionXML		+=	"<Read>N</Read>";
					}
					if(Request[screen + "_write"] != null)
					{
						permissionXML		+=	"<Write>Y</Write>";
					}
					else
					{
						permissionXML		+=	"<Write>N</Write>";
					}
					if(Request[screen + "_delete"] != null)
					{
						permissionXML		+=	"<Delete>Y</Delete>";
					}
					else
					{
						permissionXML		+=	"<Delete>N</Delete>";
					}
					if(Request[screen + "_execute"] != null)
					{
						permissionXML		+=	"<Execute>Y</Execute>";
					}
					else
					{
						permissionXML		+=	"<Execute>N</Execute>";
					}
					permissionXML			+=	"</security>";
					if(!objSecurity.ComparePermission(screen,permissionXML))
					{
						objSecurityInfo.SCREEN_ID				=	screen;
						objSecurityInfo.PERMISSION_XML			=	permissionXML;
						objSecurityInfo.USER_TYPE_ID			=	int.Parse(hidUSER_TYPE_ID.Value);
						objSecurityInfo.USER_ID					=	int.Parse(hidUSER_ID.Value);
						objSecurityInfo.IS_ACTIVE				=	"Y";
						objSecurityInfo.LAST_UPDATED_DATETIME	=	DateTime.Now;
						objSecurityInfo.CREATED_DATETIME		=	DateTime.Now;
						objSecurityInfo.CREATED_BY				=	int.Parse(GetUserId());
						objSecurityInfo.MODIFIED_BY				=	int.Parse(GetUserId());
					
						intRetVal								=	objSecurity.AddSecurity(objSecurityInfo);
					}
					else
					{
						intRetVal								=	1;
					}
				}
			}
			int sectionId		=	int.Parse(Request["cmbSUBSECTION"]);
			//hidOldDatas.Value	=	objSecurity.GetScreenList(int.Parse(Request["cmbSUBSECTION"]),int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value));
			if(Request["cmbAPPPOL"]!=null)
				hidAppPol.Value =	Request["cmbAPPPOL"].ToString();
			//hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value));
			string strCarrierSystemID = Cms.CmsWeb.cmsbase.CarrierSystemID;
			string strAgencyID = GetSystemId();
			if(Request.QueryString["CALLED_FOR"] != null && Request.QueryString["CALLED_FOR"] != "")
				//strAgencyID = ClsAgency.GetAgencyIDFromCode(Request.QueryString["CALLED_FOR"]).ToString(); 
				strAgencyID = Request.QueryString["CALLED_FOR"].ToString(); 
			if(strCarrierSystemID.ToUpper()!=strAgencyID.ToUpper())
			{
				switch(hidAppPol.Value)
				{
					case "2":
						hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value),"POL",1);
						break;
					case "1":
						hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value),"APP",1);	
						break;
					default:
						hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value),1);
						break;
				}
			}
			else
			{
				switch(hidAppPol.Value)
				{
					case "2":
						hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value),"POL",0);
						break;
					case "1":
						hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value),"APP",0);	
						break;
					default:
						hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value),0);
						break;
				}
			}

			if(intRetVal > 0)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"29");
			}
			else
			{
				lblMessage.Text	=		ClsMessages.GetMessage(base.ScreenId,"20");
			}
			lblMessage.Visible	=	true;		
		}
		
		private void btnGetScreen_Click(object sender, System.EventArgs e)
		{
			objSecurity			=	new ClsSecurity();
			int sectionId		=	int.Parse(Request["cmbSUBSECTION"]);
            //sectionId = sectionId + 1;
			hidAppPol.Value="-1";
			hidSUBSECTION.Value	=	Request["cmbSUBSECTION"];
			hidSECTION.Value	=	Request["cmbSECTION"];
			if(Request["cmbAPPPOL"]!=null)
				hidAppPol.Value =	Request["cmbAPPPOL"].ToString();
			if (hidUSER_TYPE_ID.Value == "")
				hidUSER_TYPE_ID.Value = "0";
			//hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value));
			string strCarrierSystemID = Cms.CmsWeb.cmsbase.CarrierSystemID;
			string strAgencyID = GetSystemId();
			if(Request.QueryString["CALLED_FOR"] != null && Request.QueryString["CALLED_FOR"] != "")
				//strAgencyID = ClsAgency.GetAgencyIDFromCode(Request.QueryString["CALLED_FOR"]).ToString(); 
				strAgencyID = Request.QueryString["CALLED_FOR"].ToString(); 
			if(strCarrierSystemID.ToUpper()!=strAgencyID.ToUpper())
			{
				switch(hidAppPol.Value)
				{
					case "2":
						hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value),"POL",1);
						break;
					case "1":
						hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value),"APP",1);	
						break;
					default:
						hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value),1);
						break;
				}
			}
			else
			{
				switch(hidAppPol.Value)
				{
					case "2":
						hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value),"POL",0);
						break;
					case "1":
						hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value),"APP",0);	
						break;
					default:
						hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value),0);
						break;
				}
			}
			/*if(hidAppPol.Value=="2")
				hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value),"POL");
			if(hidAppPol.Value=="1")
				hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value),"APP");	
			else
				hidOldDatas.Value	=	objSecurity.GetScreenList(sectionId,int.Parse(hidUSER_TYPE_ID.Value),int.Parse(hidUSER_ID.Value));*/
		}
        private void cmbSECTION_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
		#endregion

       
	}
}
