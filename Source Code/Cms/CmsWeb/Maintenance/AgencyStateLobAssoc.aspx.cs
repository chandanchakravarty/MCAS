/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> April 20,2006
	<End Date				: - >
	<Description			: - > Page is used to assign limits to authority
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History


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
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlCommon;




namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// 
	/// </summary>
	public class AgencyStateLobAssoc : Cms.CmsWeb.cmsbase  
	{
		#region Page controls declaration
		
		
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************				
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;		
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE_ID;
		protected System.Web.UI.WebControls.CheckBox chkMoveAll;
		protected System.Web.UI.WebControls.Label lblMoveAll;
		protected System.Web.UI.WebControls.Label capUnassignLossLob;
		protected System.Web.UI.WebControls.Label capAssignedLossLob;
		protected System.Web.UI.WebControls.ListBox cmbAssignLossLob;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAssignLossCodes;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnUnAssignLossLob;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRESET;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOSS_CODE_TYPE;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMNT_AGENCY_STATE_LOB_ASSOC_ID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_ID;
		protected System.Web.UI.WebControls.Label capSTATE_ID;
		protected System.Web.UI.WebControls.ListBox cmbUnAssignLossLob;		
		protected System.Web.UI.HtmlControls.HtmlTableRow trLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLossCodes;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldLOBCODE;
        protected System.Web.UI.WebControls.Label  lblpleaseclick;
        

		#endregion
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			base.ScreenId="10_5";
			
			lblMessage.Visible = false;			
			

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;			
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AgencyStateLobAssoc" ,System.Reflection.Assembly.GetExecutingAssembly());
			

			if(!Page.IsPostBack)
			{	
				//				ShowHideControls(false);	
				GetQueryStringValues();
				cmbSTATE_ID.Attributes.Add("onClick","javascript: return cmbLOB_ID_Changed();");
				chkMoveAll.Attributes.Add("onClick","javascript: return AssignAllLossCodes();");
				LoadDropDowns();
				PopulateLobDropdown();
				cmbUnAssignLossLob.Attributes.Add("ondblclick","javascript:AssignLossCodes();");
				cmbAssignLossLob.Attributes.Add("ondblclick","javascript:UnAssignLossCodes();");				
				btnSave.Attributes.Add("onClick","CountAssignLossCodes();");
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
				SetCaptions();
				SetErrorMessages();

                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/support/PageXML/" + GetSystemId(), "AgencyStateLobAssoc.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() + "/AgencyStateLobAssoc.xml");
                }
                cmbSTATE_ID.Attributes.Add("style", "display:inline");				
			}
			else if(hidRESET.Value=="1")
			{
				if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="")
				{
					cmbSTATE_ID.SelectedValue = Request.QueryString["LOB_ID"].ToString();					
				}
				hidRESET.Value="0";

			}	
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.cmbSTATE_ID.SelectedIndexChanged += new System.EventHandler(this.cmbSTATE_ID_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion		

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvSTATE_ID.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("35");
			//			rfvEXPERT_SERVICE_TYPE.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
		}

		#endregion

		private void cmbSTATE_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				PopulateLobDropdown();	
				chkMoveAll.Checked = false;
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}

		

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function
				Cms.BusinessLayer.BlCommon.ClsAgencyStateLobAssoc objAgencyStateLobAssoc = new Cms.BusinessLayer.BlCommon.ClsAgencyStateLobAssoc();

				//Retreiving the form values into model class object
				Cms.Model.Maintenance.ClsAgencyStateLobAssocInfo objAgencyStateLobAssocInfo = GetFormValue();

				objAgencyStateLobAssocInfo.CREATED_BY = int.Parse(GetUserId());
				objAgencyStateLobAssocInfo.CREATED_DATETIME = DateTime.Now;
				objAgencyStateLobAssocInfo.IS_ACTIVE="Y"; 
					
				//Calling the add method of business layer class
				intRetVal = objAgencyStateLobAssoc.Add(objAgencyStateLobAssocInfo,hidLOSS_CODE_TYPE.Value,hidOldLOBCODE.Value);				

				if(intRetVal>-1)
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
					hidFormSaved.Value			=	"1";
					hidIS_ACTIVE.Value = "Y";
					PopulateLobDropdown();
				}
				else if(intRetVal == -1) //Duplicate Authority Limit
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
				//				if(objAgency!= null)
				//					objAgency.Dispose();
			}
		}
		private void SetCaptions()
		{
            capSTATE_ID.Text = objResourceMgr.GetString("cmbSTATE_ID");
			lblMoveAll.Text=		objResourceMgr.GetString("chkMoveAll");			
			capAssignedLossLob.Text=		objResourceMgr.GetString("cmbAssignLossLob");
			capUnassignLossLob.Text=		objResourceMgr.GetString("cmbUnAssignLossLob");
            lblpleaseclick.Text = objResourceMgr.GetString("lblpleaseclick");
		}
	

		#region GetFormValue
		private ClsAgencyStateLobAssocInfo GetFormValue()
		{
			ClsAgencyStateLobAssocInfo objAgencyStateLobAssocInfo = new ClsAgencyStateLobAssocInfo();
           
                if (cmbSTATE_ID.SelectedItem != null && cmbSTATE_ID.SelectedItem.Value != "")
                    if (cmbSTATE_ID.SelectedItem.ToString() == "Singapore")
                    {
                        objAgencyStateLobAssocInfo.STATE_ID = 92;
                    }
                    else
                    {
                        objAgencyStateLobAssocInfo.STATE_ID = int.Parse(cmbSTATE_ID.SelectedItem.Value);
                    }
			objAgencyStateLobAssocInfo.AGENCY_ID = int.Parse(hidAGENCY_ID.Value);
			return objAgencyStateLobAssocInfo;
		}
		#endregion

		#region LoadDropDowns
		private void LoadDropDowns()
		{

            //add by Rahul Dwivedi on date 02 dec 2011
            if (GetSystemId().ToUpper() == "S001" || GetSystemId() == "SUAT")
            {

                DataTable dtCountry = Cms.CmsWeb.ClsFetcher.AllCountry;
                cmbSTATE_ID.DataSource = dtCountry;
                cmbSTATE_ID.DataTextField = "COUNTRY_NAME";
                cmbSTATE_ID.DataValueField = "COUNTRY_ID";
               
            }
            else
            {
                DataTable dtState = Cms.CmsWeb.ClsFetcher.ActiveState;
                cmbSTATE_ID.DataSource = dtState;
                cmbSTATE_ID.DataTextField = "STATE_NAME";
                cmbSTATE_ID.DataValueField = "STATE_ID";
               
            }
            cmbSTATE_ID.DataBind();
			ListItem lItem = cmbSTATE_ID.Items.FindByValue(((int)enumState.Arkansas).ToString());
			if(lItem!=null)
				cmbSTATE_ID.Items.Remove(lItem);
			cmbSTATE_ID.Items.Insert(0,new ListItem("",""));
			cmbSTATE_ID.SelectedIndex=0;			
		}
		private void PopulateLobDropdown()
		{
			Cms.BusinessLayer.BlCommon.ClsAgencyStateLobAssoc objAgencyStateLobAssoc = new Cms.BusinessLayer.BlCommon.ClsAgencyStateLobAssoc();			
			if(cmbSTATE_ID.SelectedItem!=null && cmbSTATE_ID.SelectedItem.Value!="")
			{
                DataSet dsLOB = null; 
                 //add by Rahul Dwivedi on date 02 dec 2011
                if (GetSystemId().ToUpper() == "S001" || GetSystemId() == "SUAT")
                    dsLOB = objAgencyStateLobAssoc.GetMNT_AGENCY_COUNTRY_LOB_ASSOC(int.Parse(hidAGENCY_ID.Value), int.Parse(cmbSTATE_ID.SelectedItem.Value));
                else
                dsLOB = objAgencyStateLobAssoc.GetMNT_AGENCY_STATE_LOB_ASSOC(int.Parse(hidAGENCY_ID.Value),int.Parse(cmbSTATE_ID.SelectedItem.Value));

				if(dsLOB!=null && dsLOB.Tables.Count>0)
				{
					cmbUnAssignLossLob.Items.Clear();
					if(dsLOB.Tables[0]!=null && dsLOB.Tables[0].Rows.Count>0)
					{
						cmbUnAssignLossLob.DataSource = dsLOB.Tables[0];
						cmbUnAssignLossLob.DataTextField="LOB_DESC";
						cmbUnAssignLossLob.DataValueField="LOB_ID"; 
						cmbUnAssignLossLob.DataBind();
					}							
					cmbAssignLossLob.Items.Clear();
					if(dsLOB.Tables[1]!=null && dsLOB.Tables[1].Rows.Count>0)
					{
						cmbAssignLossLob.DataSource = dsLOB.Tables[1];
						cmbAssignLossLob.DataTextField="LOB_DESC";
						cmbAssignLossLob.DataValueField="LOB_ID"; 
						cmbAssignLossLob.DataBind();
					}
				}
			}
		}
		#endregion

		private void GetQueryStringValues()
		{
			if(Request.QueryString["EntityId"]!=null && Request.QueryString["EntityId"].ToString()!="")
				hidAGENCY_ID.Value = Request.QueryString["EntityId"].ToString();
			else
			{
				string strCarrierSystemID = Cms.CmsWeb.cmsbase.CarrierSystemID;
				string strAgencyID = GetSystemId();
				if(strCarrierSystemID.ToUpper()!=strAgencyID.ToUpper())
				{
					hidAGENCY_ID.Value = ClsAgency.GetAgencyIDFromCode(strAgencyID).ToString();
				}
				else
					hidAGENCY_ID.Value = "0";
			}
			
			//	hidAGENCY_ID.Value = "0";
		}
	}
}
