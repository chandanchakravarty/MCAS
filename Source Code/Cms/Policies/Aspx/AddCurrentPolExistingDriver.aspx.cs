/******************************************************************************************
<Author					: -  Swastika Gaur
<Start Date				: -	 27th Mar '06
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
	/// Summary description for AddCurrentPolExistingDriver.
	/// </summary>
	public class AddCurrentPolExistingDriver : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label lblHeader;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgrExistingDriver;
		protected Cms.CmsWeb.Controls.CmsButton btnSubmit;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTitle;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFor;

		private int intFromUserId;
		protected string  strTitle;
		protected int gIntSaved=0;
		private const string CALLED_FROM_UMBRELLA="UMB";
		private const string CALLED_FROM_WATERCRAFT="WAT";
		private const string CALLED_FROM_HOME="HOME";
		private const string CALLED_FROM_RENT="RENT";
		private const string CALLED_FROM_MOTORCYCLE="MOT";
		public string strCalledFrom="";
		public string strCalledFor="";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region setting screen id
			if(Request.QueryString["calledfrom"]!=null )
				strCalledFrom=Request.QueryString["calledfrom"].ToString(); 
			

			switch(strCalledFrom.ToUpper())
			{
				case "PPA" :
					base.ScreenId	=	"45_0_0";
					strTitle="Copy Policy Drivers/Household Members";
					break;
				case "MOT" :
					base.ScreenId	=	"49_0_0";
					strTitle="Copy Policy Drivers";
					break;
				case "UMB" :
					base.ScreenId	=	"84_0_0";
					strTitle="Copy Policy Drivers/Operators";
					break;
				case "WAT" :
					base.ScreenId	=	"73_0_0";
					strTitle="Copy Policy Operators";
					break;
				case "HOME" :
					base.ScreenId	=	"149_0_0";
					strTitle="Copy Policy Operators";
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
				if(Request.QueryString["CalledFor"]!=null && Request.QueryString["CalledFor"].ToString()!="")
				{
					hidCalledFor.Value =Request.QueryString["CalledFor"].ToString();
				}

				
				if (hidCalledFrom.Value.Trim().ToUpper()== "CALLED_FROM_WATERCRAFT" || hidCalledFrom.Value.Trim().ToUpper()== "HOME" || hidCalledFrom.Value.Trim().ToUpper()== "RENT"||( hidCalledFrom.Value.Trim().ToUpper()== "WAT" ))
				{
					lblHeader.Text="Copy Policy Operators";
					hidTitle.Value=strTitle;
					dgrExistingDriver.Columns[6].HeaderText="Operator Code";
					dgrExistingDriver.Columns[7].HeaderText="Operator Name";
				}
				else if((hidCalledFrom.Value.Trim().ToUpper()== "CALLED_FROM_MOTORCYCLE") || (hidCalledFrom.Value.Trim().ToUpper()== "PPA" )||( hidCalledFrom.Value.Trim().ToUpper()== "MOT"))
				{
					hidTitle.Value=strTitle ;
					lblHeader.Text="Copy Policy Drivers/Household Members";
					dgrExistingDriver.Columns[6].HeaderText="Driver Code";
					dgrExistingDriver.Columns[7].HeaderText="Driver Name";
				}
				else if(hidCalledFrom.Value.Trim().ToUpper()=="UMB")
				{
					hidTitle.Value="Copy Policy Drivers/Operators";
					lblHeader.Text="Copy Policy Drivers/Operators";
					dgrExistingDriver.Columns[6].HeaderText="Driver/Operator Code";
					dgrExistingDriver.Columns[7].HeaderText="Driver/Operator Name";

				}
				else
				{
					if(hidCalledFor.Value.Trim().ToUpper()=="PPA")
					{
						hidTitle.Value="Copy Policy Drivers";
						lblHeader.Text="Copy Policy Drivers/Household Members";
						dgrExistingDriver.Columns[6].HeaderText="Driver Code";
						dgrExistingDriver.Columns[7].HeaderText="Driver Name";
					}
					else if(hidCalledFor.Value.Trim().ToUpper()=="WAT")
					{
						hidTitle.Value="Copy Policy Operators";
						lblHeader.Text="Copy Policy Operators";
						dgrExistingDriver.Columns[6].HeaderText="Operator Code";
						dgrExistingDriver.Columns[7].HeaderText="Operator Name";

					}
				}


				try
				{	
					DataTable dtDriver;
					if (hidCalledFrom.Value.Trim().ToUpper()== CALLED_FROM_UMBRELLA )
					{
						dtDriver=ClsDriverDetail.FetchPolicyExistingDriverForUmbrella(int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value));
					}

					else if (hidCalledFrom.Value.Trim().ToUpper()== CALLED_FROM_WATERCRAFT || hidCalledFrom.Value.Trim().ToUpper()==CALLED_FROM_HOME || hidCalledFrom.Value.Trim().ToUpper()==CALLED_FROM_RENT)
					{
						dtDriver=ClsDriverDetail.FetchPolicyExistingDriverForWatercraft(int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value));
						
					}
					else
					{
						dtDriver=ClsDriverDetail.FetchPolicyExistingDriverFromCurrentApp(int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value));
					}
							
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
			ClsDriverDetail objDriverDetail = new ClsDriverDetail();
			dt.Columns.Add("CustomerID",typeof(int));
			dt.Columns.Add("PolicyID",typeof(int));
			dt.Columns.Add("PolicyVersionID",typeof(int));
			dt.Columns.Add("DriverID",typeof(int));
			dt.Columns.Add("DriverCode",typeof(string));
			dt.Columns.Add("DriverName",typeof(string));
			try
			{
				bool blChecked=false;
				// Code for finding the checked vehicle in the datagrid.
				foreach(DataGridItem dgi in dgrExistingDriver.Items)
				{
					chkBox=(CheckBox)dgi.FindControl("chkSelect");
					if (chkBox != null && chkBox.Checked)
					{
						DataRow dr=dt.NewRow();
						dr["CustomerID"]=dgi.Cells[1].Text;
						dr["PolicyID"]=dgi.Cells[2].Text;
						dr["PolicyVersionID"]=dgi.Cells[4].Text;
						dr["DriverID"]=int.Parse(dgrExistingDriver.DataKeys[dgi.ItemIndex].ToString());
						dr["DriverName"]=dgi.Cells[7].Text;
						dr["DriverCode"]=dgi.Cells[6].Text;
						dt.Rows.Add(dr);
					}			
				}
				if (dt.Rows.Count > 0)	
				{	
					if (hidCalledFrom.Value.Trim().ToUpper()== CALLED_FROM_UMBRELLA )
					{
//						if(hidCalledFor.Value.Trim().ToUpper()=="WAT")
//						{
//							objDriverDetail.InsertPolicyExistingUmbrellaOperator(dt,int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value),intFromUserId);					
//						}
//						else
//						{
							objDriverDetail.InsertPolicyExistingUmbrellaDriver(dt,int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value),intFromUserId);					
//						}
						base.OpenEndorsementDetails();//Added for Itrack Issue 5655 on 29 April 2009
					}
					else if (hidCalledFrom.Value.Trim().ToUpper()== CALLED_FROM_WATERCRAFT || hidCalledFrom.Value.Trim().ToUpper()==CALLED_FROM_HOME || hidCalledFrom.Value.Trim().ToUpper()==CALLED_FROM_RENT)
					{
						objDriverDetail.InsertPolicyExistingWatercraftDriver(dt,int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value),intFromUserId);					
						base.OpenEndorsementDetails();//Added for Itrack Issue 5655 on 29 April 2009
					}
					else if(hidCalledFrom.Value.Trim().ToUpper()== CALLED_FROM_MOTORCYCLE )
					{
						objDriverDetail.InsertPolicyExistingDriver(dt,int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value),intFromUserId,hidCalledFrom.Value);					
						base.OpenEndorsementDetails();//Added for Itrack Issue 5655 on 29 April 2009
					}
					else
					{
						objDriverDetail.InsertPolicyExistingDriver(dt,int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value),intFromUserId,hidCalledFrom.Value);
						base.OpenEndorsementDetails();//Added for Itrack Issue 5655 on 29 April 2009
					}
					hidFormSaved.Value="1";
				}	
				if(blChecked==true)	
					gIntSaved=1;
				if(hidFormSaved.Value=="1")
				{
					lblMessage.Text="Selected applicants have been copied successfully";
					lblMessage.Visible=true;
					gIntSaved=1;
					}
				else
					if(blChecked==false)
				{
					lblMessage.Visible=true;
					if (hidCalledFrom.Value.Trim().ToUpper()== "CALLED_FROM_WATERCRAFT" || hidCalledFrom.Value.Trim().ToUpper()== "HOME" || hidCalledFrom.Value.Trim().ToUpper()== "RENT"||( hidCalledFrom.Value.Trim().ToUpper()== "WAT" ))
					{
						lblMessage.Text="Please select Policy Operator.";
					}
					//RPSINGH -> Added condition for unbrella policy -> "UMB"
					else if((hidCalledFrom.Value.Trim().ToUpper()== "CALLED_FROM_MOTORCYCLE") || (hidCalledFrom.Value.Trim().ToUpper()== "PPA" )||( hidCalledFrom.Value.Trim().ToUpper()== "MOT") || (hidCalledFrom.Value.Trim().ToUpper()== "UMB"))
					{
						lblMessage.Text="Please select Policy Driver.";
					}
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
					ClientScript.RegisterStartupScript(this.GetType(),"Refresh",strScript);
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
