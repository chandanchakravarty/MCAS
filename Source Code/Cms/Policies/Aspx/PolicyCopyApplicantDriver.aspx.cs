/******************************************************************************************
<Author				: -  Swastika Gaur
<Start Date				: -	24th Mar'06 
<End Date				: -	
<Description			: - 	
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
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.Controls;


namespace Policies.Aspx
{
	/// <summary>
	/// Summary description for PolicyCopyApplicantDriver.
	/// </summary>
	public class PolicyCopyApplicantDriver : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label lblHeader;
		protected System.Web.UI.WebControls.DataGrid dgrExistingDriver;
		private int intFromUserId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected Cms.CmsWeb.Controls.CmsButton btnSubmit;
		protected Cms.CmsWeb.Controls.CmsButton btnClose;		
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFor;		
		protected int gIntSaved=0;
		private const string CALLED_FROM_UMBRELLA="UMB";
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		private const string CALLED_FROM_WATERCRAFT="WAT";
		private const string CALLED_FROM_HOME="HOME";
		private const string CALLED_FROM_RENT="RENT";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTitle;
		public string strCalledFrom="";

	
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region setting screen id
			if(Request.QueryString["calledfrom"]!=null )
				strCalledFrom=Request.QueryString["calledfrom"].ToString(); 

			switch(strCalledFrom)
			{
				case "PPA" :
					base.ScreenId	=	"45_0_0";
					break;
				case "MOT" :
					base.ScreenId	=	"49_0_0";
					break;
				case "UMB" :
					base.ScreenId	=	"84_0_0";
					break;
				case "WAT" :
					base.ScreenId	=	"73_0_0";
					break;
				case "HOME" :
					base.ScreenId	=	"149_0_0";
					break;
				case "RENT" :
					base.ScreenId	=	"167_0_0";
					break;
				default :
					base.ScreenId	=	"45_0_0";
					break;
			}
			#endregion

			
			btnSubmit.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSubmit.PermissionString = gstrSecurityXML;

			btnClose.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnClose.PermissionString = gstrSecurityXML;


			intFromUserId =	int.Parse(GetUserId());
			if (!IsPostBack)
			{				
				if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
				{ 						
					hidCustomerID.Value=Request.QueryString["CUSTOMER_ID"].ToString(); 
				}				
				if (Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString() != "")
				{ 						
					hidPolicyID.Value=Request.QueryString["POLICY_ID"].ToString();
				}				
				if (Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
				{ 					
					hidPolicyVersionID.Value=Request.QueryString["POLICY_VERSION_ID"].ToString();
				}		
				if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString() != "")
				{ 					
					hidCalledFrom.Value=Request.QueryString["CALLEDFROM"].ToString();
				}

				if (Request.QueryString["CALLEDFOR"]!=null && Request.QueryString["CALLEDFOR"].ToString() != "")
				{ 					
					hidCalledFor.Value=Request.QueryString["CALLEDFOR"].ToString();
				}
				
				btnClose.Attributes.Add("onClick","javascript: window.close();");


				try
				{	
					DataTable dtDriver;
					/*if (hidCalledFrom.Value.Trim().ToUpper()== CALLED_FROM_UMBRELLA )
					{
						dtDriver=ClsDriverDetail.FetchExistingDriverForUmbrella(int.Parse(hidCustomerID.Value),int.Parse(hidAppID.Value),int.Parse(hidAppVersionID.Value));
						
					}
					else if (hidCalledFrom.Value.Trim().ToUpper()== CALLED_FROM_WATERCRAFT || hidCalledFrom.Value.Trim().ToUpper()==CALLED_FROM_HOME || hidCalledFrom.Value.Trim().ToUpper()==CALLED_FROM_RENT)
					{
						dtDriver=ClsDriverDetail.FetchExistingDriverForWatercraft(int.Parse(hidCustomerID.Value),int.Parse(hidAppID.Value),int.Parse(hidAppVersionID.Value));
						
					}
					else
					{
						dtDriver=ClsDriverDetail.FetchExistingDriverFromCurrentApp(int.Parse(hidCustomerID.Value),int.Parse(hidAppID.Value),int.Parse(hidAppVersionID.Value));
					}*/
					dtDriver=ClsDriverDetail.FetchApplicantsForCustomer(int.Parse(hidCustomerID.Value));
					if(dtDriver.Rows.Count>0)
					{
						btnSubmit.Enabled=true;
					}
					else
					{	
						btnSubmit.Enabled=false;
					}
					dgrExistingDriver.DataSource=dtDriver.DefaultView;
					dgrExistingDriver.DataBind();
				}
				catch(Exception ex)
				{
					lblMessage.Text=ex.Message;
					lblMessage.Visible=true;
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
			}
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
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			CheckBox chkBox;
			DataTable dt=new DataTable();			
			dt.Columns.Add("ApplicantID",typeof(int));
			dt.Columns.Add("ApplicantName",typeof(string));
			ClsDriverDetail objDriverDetail	= new ClsDriverDetail();
			try
			{
				bool blChecked=false;
				// Code for finding the checked vehicle in the datagrid.
				foreach(DataGridItem dgi in dgrExistingDriver.Items)
				{
					chkBox=(CheckBox)dgi.FindControl("chkSelect");
					
					if (chkBox != null && chkBox.Checked)
					{
						blChecked=true;
						DataRow dr=dt.NewRow();
						//dr["CustomerID"]=hidCustomerID.v
						//dr["ApplicantID"]=dgi.Cells[1].Text;
						//dr["AppVersionID"]=dgi.Cells[4].Text;
						dr["ApplicantID"]=int.Parse(dgrExistingDriver.DataKeys[dgi.ItemIndex].ToString());
						dr["ApplicantName"]=dgi.Cells[2].Text;						
						dt.Rows.Add(dr);
					}			
				}
				if (dt.Rows.Count > 0)	
				{	
					/*if (hidCalledFrom.Value.Trim().ToUpper()== CALLED_FROM_UMBRELLA )
					{
						ClsDriverDetail.InsertExistingUmbrellaDriver(dt,int.Parse(hidCustomerID.Value),int.Parse(hidAppID.Value),int.Parse(hidAppVersionID.Value),intFromUserId);					
						
					}
					else if (hidCalledFrom.Value.Trim().ToUpper()== CALLED_FROM_WATERCRAFT || hidCalledFrom.Value.Trim().ToUpper()==CALLED_FROM_HOME || hidCalledFrom.Value.Trim().ToUpper()==CALLED_FROM_RENT)
					{
						ClsDriverDetail.InsertExistingWatercraftDriver(dt,int.Parse(hidCustomerID.Value),int.Parse(hidAppID.Value),int.Parse(hidAppVersionID.Value),intFromUserId);					
						
					}
					else
					{
						ClsDriverDetail.InsertExistingDriver(dt,int.Parse(hidCustomerID.Value),int.Parse(hidAppID.Value),int.Parse(hidAppVersionID.Value),intFromUserId);					
					}*/
					objDriverDetail.InsertPolicyApplicantsToDriver(dt,int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value),intFromUserId,hidCalledFrom.Value,hidCalledFor.Value);
					
					//ClsDriverDetail.InsertApplicantsToDriver(dt,int.Parse(hidCustomerID.Value),int.Parse(hidAppID.Value),int.Parse(hidAppVersionID.Value),intFromUserId,hidCalledFrom.Value);										
					hidFormSaved.Value="1";
					base.OpenEndorsementDetails();//Added for Itrack Issue 5655 on 29 April 2009
				}						
				if(blChecked==true)
					gIntSaved=1;
				if(hidFormSaved.Value=="1")
				{
					lblMessage.Text="Selected applicants have been copied successfully";
					lblMessage.Visible=true;
					btnSubmit.Visible=false;
					dgrExistingDriver.Visible=false;
					btnClose.Visible=true;
				}
				if(blChecked==false)
				{
					lblMessage.Visible=true;
					lblMessage.Text="Please select applicant.";
					gIntSaved=0;
					return ;
				}
			
				string strScript="";
			
				if (hidCalledFrom.Value.Trim().ToUpper()== CALLED_FROM_WATERCRAFT)
				{
					
					strScript = @"<script language='javascript'>" + 
						"window.opener.RefreshWebgrid('');" +
						"window.close();" + 
						"</script>";
				
				}
				else
				{
					strScript = @"<script language='javascript'>" + 
						"window.opener.RefreshWebgrid('');" +
						"window.close();" + 
						"</script>";
				}
				if (!ClientScript.IsStartupScriptRegistered("Refresh"))
				{
                    ClientScript.RegisterStartupScript(this.GetType(),"Refresh", strScript);
				}
				
			}
			catch(Exception ex)
			{
				gIntSaved=0;
				lblMessage.Text=ex.Message;
				lblMessage.Visible=true;			
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			} 		
		}		
	}

		}
	

