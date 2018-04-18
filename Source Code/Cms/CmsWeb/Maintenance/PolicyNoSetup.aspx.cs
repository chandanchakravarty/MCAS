/******************************************************************************************
<Author				: -   Shafi
<Start Date				: -	12/23/2005 
<Purpose				: - 
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
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
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls;

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsPolicyNoSetup : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration


		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		
		//creating resource manager object (used for reading field and label mapping)
		
		
		protected System.Web.UI.WebControls.DropDownList cmbStates;
		protected System.Web.UI.WebControls.ListBox lbAssignStates;
		protected System.Web.UI.WebControls.ListBox lbUnAssignStates;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateID;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DropDownList cmbLOB;
		protected System.Web.UI.HtmlControls.HtmlForm MNT_LOB_MASTER;
        protected System.Web.UI.WebControls.Label lblpleaseassign;
        protected System.Web.UI.WebControls.Label lblassignstate;
        protected System.Web.UI.WebControls.Label lblproduct;
        protected System.Web.UI.WebControls.Label lblassigned;
        protected System.Web.UI.WebControls.Label lblunassigned;
		ClsStates objClsStates;
        System.Resources.ResourceManager objResourceMgr;
		//Defining the business layer class object
		
		//END:*********** Local variables *************

		#endregion

		/*
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvLOB_ID.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvSEED.ErrorMessage			=   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			this.revLOB_SEED.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			revLOB_SEED.ValidationExpression  =  aRegExpInteger;
		}*/
		
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			//base.ScreenId="211";
			base.ScreenId="189";
			SetSecurityXml();
			objClsStates=new ClsStates();
			lbAssignStates.Attributes.Add("ondblclick","javascript:AssignStates();");
			lbUnAssignStates.Attributes.Add("ondblclick","javascript:UnAssignStates();");

            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.ClsPolicyNoSetup", System.Reflection.Assembly.GetExecutingAssembly());
            lblpleaseassign.Text = objResourceMgr.GetString("lblpleaseassign");
            lblassignstate.Text = objResourceMgr.GetString("lblassignstate");
            lblproduct.Text = objResourceMgr.GetString("lblproduct");
            lblunassigned.Text = objResourceMgr.GetString("lblunassigned");
            lblassigned.Text = objResourceMgr.GetString("lblassigned");
			if(!IsPostBack)
			{
				try
				{
					DataSet lob;
					lob=objClsStates.PoplateLob();
					cmbLOB.DataSource =lob.Tables[0];
					cmbLOB.DataValueField =lob.Tables[0].Columns[0].ToString();
					cmbLOB.DataTextField =lob.Tables[0].Columns[1].ToString();
					cmbLOB.DataBind();
					cmbLOB.Items.Insert(0,"");
					cmbLOB.SelectedIndex=0;
					PoplateAssignedStates();
					PoplateUnAssignedStates();
					this.btnSave.Attributes.Add("onClick","javascript:CountAssignDepts();");					
				}
		
				catch(Exception objExcep)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
				}
			}
		}


		//lblMessage.Visible = false;
		//SetErrorMessages();
			
		/*
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	   = CmsButtonType.Write;
			btnReset.PermissionString  = gstrSecurityXML;

			btnSave.CmsButtonClass	   = CmsButtonType.Write;
			btnSave.PermissionString   = gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************

			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.ClsPolicyNoSetup" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			if(!Page.IsPostBack)
			{
				GetOldDataXML(); 
				FillControls();
				
				SetCaptions();
		#region "Loading singleton"
		#endregion//Loading singleton
			}
			*/
		//end pageload
		#endregion
		#region PoplateUnAssignedStates 
		private void PoplateUnAssignedStates()
		{
			DataSet unAssign;
			try
			{
				if(Convert.ToInt32(cmbLOB.SelectedValue) >0)
				{
					unAssign =objClsStates.PopLateUnassignedState(Convert.ToInt32(cmbLOB.SelectedValue));
					lbUnAssignStates.DataSource=unAssign.Tables[0];
					lbUnAssignStates.DataTextField=unAssign.Tables[0].Columns[1].ToString();
					lbUnAssignStates.DataValueField=unAssign.Tables[0].Columns[0].ToString();
					lbUnAssignStates.DataBind();
                    lbUnAssignStates.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? "Selecione ....>>" : "Select States....>>"));
					lbUnAssignStates.SelectedIndex=0;

				}
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}


		}


		#endregion

        #region PoplateAssignedStates 
		private void PoplateAssignedStates()
		{

			DataSet unAssign;
			try
			{
				if(Convert.ToInt32(cmbLOB.SelectedValue) >0)
				{
					unAssign =objClsStates.PopLateAssignedState(Convert.ToInt32(cmbLOB.SelectedValue));
					lbAssignStates.DataSource=unAssign.Tables[0];
					lbAssignStates.DataTextField=unAssign.Tables[0].Columns[1].ToString();
					lbAssignStates.DataValueField=unAssign.Tables[0].Columns[0].ToString();
					lbAssignStates.DataBind();
					lbAssignStates.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? "<<...Estados Selecione" : "<<...Select States"));
                    lbAssignStates.SelectedIndex=0;

				}
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}

		}
		#endregion

		#region SetSecurityXml
		private void SetSecurityXml()
		{
			btnSave.CmsButtonClass=CmsButtonType.Write;
			btnSave.PermissionString=gstrSecurityXML;
		}
    	#endregion
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.cmbLOB.SelectedIndexChanged += new System.EventHandler(this.cmbLOB_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		
		


		
		#region btnSave Click
     
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				if(cmbLOB.SelectedValue.ToString()!="")
				{
					int intLobID ;
					intLobID = Convert.ToInt32(cmbLOB.SelectedValue);
					string lobDesc = cmbLOB.Items[cmbLOB.SelectedIndex].Text;
					//char[] arrDelimeter={','};
					//string [] arrStateId ;
					//arrStateId = hidStateID.Value.ToString().Split(',');
					//int i;
					//int[] intState = new int[arrStateId.Length];
					//for (i=1;i<arrStateId.Length;i++)
					//{
					//intState[i] = int.Parse(arrStateId[i]);
					//}
			
					int intResult;			
					intResult = objClsStates.Save(intLobID,hidStateID.Value.ToString());		

					if(intResult >= 1)
					{
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"3");
					}
					else
					{
						this.lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"4");
					}
					this.lblMessage.Visible=true;
				
					Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGen = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
                    string trans_desc = ClsMessages.GetMessage(base.ScreenId, "5");
                    string trans_custom = objResourceMgr.GetString("lblproduct") + lobDesc;
                    
					objGen.WriteTransactionLog(0, 0, 0, trans_desc, int.Parse(GetUserId()),trans_custom, "Application");

					PoplateUnAssignedStates();
					PoplateAssignedStates();

				}
				//Added By Raghav For Security Issue.
				else
				{
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"1");
					lblMessage.Visible = true;
				}
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}

		}

	
#endregion
		#region cmbLOB_SelectedIndexChanged Click

		private void cmbLOB_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				PoplateUnAssignedStates();
				PoplateAssignedStates();
				
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
		}
		#endregion

	}
		
}

