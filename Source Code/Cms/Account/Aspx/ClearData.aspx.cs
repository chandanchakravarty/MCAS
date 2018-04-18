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
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer;
using Cms.BusinessLayer.BlAccount;
//using Cms.DataLayer;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for ClearData.
	/// </summary>
	public class ClearData :  Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.DropDownList cmbCustomerID;
		protected System.Web.UI.WebControls.Button btnNext;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAlert;

		protected System.Web.UI.WebControls.DropDownList cmbPolClearCustomerID;
		protected System.Web.UI.WebControls.DropDownList cmbPolID;
		protected System.Web.UI.WebControls.Label capCUSTOMER_NAME		;
		protected System.Web.UI.WebControls.Label capCUST_NAME;
		protected System.Web.UI.WebControls.Label capPolicy_ID;
		protected System.Web.UI.WebControls.Button btnCustPol;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			lblMessage.Text = "";
			if(!IsPostBack)
			{
				btnCustPol.Attributes.Add("onClick","javascript: return CheckPolicy();");
				ClsAccount.GetCustomerDropDown(cmbCustomerID);
				ClsAccount.GetCustomerDropDownforPol(cmbPolClearCustomerID);
				
			}
			
			base.ScreenId = "376"; 
		}
		protected void Navigation_Click(Object sender,CommandEventArgs e )
		{
			ClsAccount objClsAccount = new ClsAccount();
					
			try 
			{
				int intResult=0;
				//To be Tested for all the data: 
				//if(cmbCustomerID.SelectedValue.ToString() == "")
				//cmbCustomerID.SelectedValue = "0";  //Clear Complete data 
				intResult = objClsAccount.ExecClearAcctData(int.Parse(cmbCustomerID.SelectedValue.ToString()));
				
				if(intResult>=0)
				{
					lblMessage.Text = "Account data has been deleted.";
				}
				else
				{
					lblMessage.Text = "Unable to delete the Accounting data.";
				}
				
				lblMessage.Visible=true;
			}
			catch(Exception objExp)
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("128");
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Visible=true;
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
//			this.cmbPolClearCustomerID.SelectedIndexChanged += new System.EventHandler(this.cmbPolClearCustomerID_SelectedIndexChanged);
			this.btnCustPol.Click += new System.EventHandler(this.btnCustPol_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		
//		private void cmbPolClearCustomerID_SelectedIndexChanged(object sender, System.EventArgs e)
//		{
//			ClsAccount objClsAccount = new ClsAccount();
//			try 
//			{
//				DataSet polds = objClsAccount.GetCustomerPolicies(int.Parse(cmbPolClearCustomerID.SelectedValue.ToString()));
//				cmbPolID.Items.Clear();
//				/*foreach(DataRow poldr in polds.Tables[0].Rows)
//				{
//					cmbPolID.Items.Add(poldr["POLICY_NUMBER"].ToString() + "-" + poldr["POLICY_DISP_VERSION"].ToString());
//				}*/
//				if(polds!=null && polds.Tables.Count>0 && polds.Tables[0]!=null && polds.Tables[0].Rows.Count>0)
//				{
//					cmbPolID.DataSource = polds;
//					cmbPolID.DataTextField = "POLICY_DISP_NUMBER";
//					cmbPolID.DataValueField = "POLICY";
//					cmbPolID.DataBind();
//					cmbPolID.Items.Insert(0,"");
//					
//				}
//
//			}
//			catch(Exception objExp)
//			{
//				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("128");
//				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
//				lblMessage.Visible=true;
//			}			
//	
//		}		


		private void btnCustPol_Click(object sender, System.EventArgs e)
		{
			ClsAccount objClsAccount = new ClsAccount();
			try 
			{
				int intResult=0;
						
				if(cmbPolID.SelectedItem!=null && cmbPolID.SelectedItem.Value!="")
				{
					string []strArray = cmbPolID.SelectedItem.Value.Split('^');
					int iPolicyId, iPolicyVersionId;
					if(strArray.Length>0)
					{
						iPolicyId = int.Parse(strArray[0].ToString());
						iPolicyVersionId = int.Parse(strArray[1].ToString());
						intResult = objClsAccount.ExecClearPolData(int.Parse(cmbPolClearCustomerID.SelectedValue.ToString()),iPolicyId,iPolicyVersionId);
					}
					//intResult = objClsAccount.ExecClearPolData(int.Parse(cmbPolClearCustomerID.SelectedValue.ToString()),int.Parse(cmbPolID.SelectedValue.ToString()));
					//intResult = objClsAccount.ExecClearPolData(int.Parse(cmbPolClearCustomerID.SelectedValue.ToString()),iPolicyId,iPolicyVersionId);
				}
				else
				{					
					intResult = objClsAccount.ExecClearPolData(int.Parse(cmbPolClearCustomerID.SelectedValue.ToString()));
				}
				
				if(intResult>=0)
				{
					lblMessage.Text = "Policy data has been deleted.";
					// Diary entry link of policy to be removed
					Response.Cookies["polNo" + ((Cms.CmsWeb.cmsbase)this.Page).GetSystemId() + "_" + ((Cms.CmsWeb.cmsbase)this.Page).GetUserId()].Expires = DateTime.Now;
					//if(cmbPolID.SelectedItem!=null && cmbPolID.SelectedItem.Value!="")
//					cmbPolClearCustomerID_SelectedIndexChanged(null,null);
					cmbPolClearCustomerID.SelectedIndex=0;
				}
				else
				{
					lblMessage.Text = "Unable to delete the Policy data.";
				}
				
				lblMessage.Visible=true;
				cmbPolID.Items.Clear();
			}
			catch(Exception objExp)
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("128") ;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Visible=true;
			}			
	
		}

		
	}
}
