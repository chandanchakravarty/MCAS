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
using Cms.DataLayer;
using Cms.CmsWeb;

namespace Reports.Aspx
{
	public class DemoReport : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.CheckBox CheckBox1;
		protected System.Web.UI.WebControls.ListBox lstUnAssignPolicy;
		protected System.Web.UI.WebControls.ListBox lstAssignPolicy;
		protected System.Web.UI.WebControls.ListBox lstUnAssignUser;
		protected System.Web.UI.WebControls.ListBox lstAssignUser;
		protected Cms.CmsWeb.Controls.CmsButton btnAddClient;
		protected Cms.CmsWeb.Controls.CmsButton btnRemoveClient;
		protected System.Web.UI.WebControls.CheckBox Checkbox2;
		protected Cms.CmsWeb.Controls.CmsButton btnSumbit;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSelectedPolicyName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSelectedPolicy; 
		protected string strSelectedClients = "";
		protected string strSelectedPolicies = "";
		protected string strSelectedPoliciesName = "";

		private void Page_Load(object sender, System.EventArgs e)
		{		
			base.ScreenId="795";
			btnSumbit.CmsButtonClass =	Cms.CmsWeb.Controls.CmsButtonType.Read;
			btnSumbit.PermissionString = gstrSecurityXML;

			btnAddClient.CmsButtonClass =	Cms.CmsWeb.Controls.CmsButtonType.Execute;
			btnAddClient.PermissionString = gstrSecurityXML;

			btnRemoveClient.CmsButtonClass =	Cms.CmsWeb.Controls.CmsButtonType.Execute;
			btnRemoveClient.PermissionString = gstrSecurityXML;
			
			this.CheckBox1.Attributes.Add("onClick","javascript: ShowContact();");
			this.Checkbox2.Attributes.Add("onClick","javascript: ShowPolicy();");
			
			strSelectedPolicies = hidSelectedPolicy.Value;
			strSelectedPoliciesName = hidSelectedPolicyName.Value;
			
			if(!IsPostBack)
			{
				try
				{
					Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
					DataSet ds = objDataWrapper.ExecuteDataSet("Select distinct (isnull(CUSTOMER_FIRST_NAME,'') + ' ' + isnull(CUSTOMER_MIDDLE_NAME,'') + ' '+ isnull(CUSTOMER_LAST_NAME,'')) as CUSTOMER_FIRST_NAME ,CUSTOMER_ID as ID FROM CLT_CUSTOMER_LIST order by customer_first_name");
					lstUnAssignUser.DataSource = ds.Tables[0];					
					lstUnAssignUser.DataTextField = "customer_first_name";
					lstUnAssignUser.DataValueField = "ID";
					lstUnAssignUser.DataBind();

					//lstUnAssignUser.Attributes.Add("ondblclick","AssignUsers();");
					//lstAssignUser.Attributes.Add("ondblclick","UnAssignUsers();");				
					
				}
				catch(Exception ex)
				{
					//throw ex;
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
				
			}	
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{			
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    
			this.btnAddClient.Click += new System.EventHandler(this.btnAddClient_Click);
			this.btnRemoveClient.Click += new System.EventHandler(this.btnRemoveClient_Click);
			this.btnSumbit.Click += new System.EventHandler(this.btnSumbit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		
		private void btnAddClient_Click(object sender, System.EventArgs e)
		{
			foreach(ListItem li in lstUnAssignUser.Items)
			{
				if (li.Selected == true)
				{
					lstAssignUser.Items.Add(new ListItem(li.Text,li.Value));
				}					
			}

			int i=0;
			for(i=lstUnAssignUser.Items.Count-1;i>=0;i--)
			{
				if (lstUnAssignUser.Items[i].Selected == true)
				{
					lstUnAssignUser.Items.Remove(lstUnAssignUser.Items[i]);
				}
			}			

			getPolicies();
			
			/*int i = 0; 
			for(i = 0; i < lstUnAssignUser.Items.Count;i++)
			{
				if (lstUnAssignUser.Items[i].Selected == true)
				{
					//lstAssignUser.Items.Add(li.Value);
					lstAssignUser.Items.Add(new ListItem(lstUnAssignUser.Items[i].Text,lstUnAssignUser.Items[i].Value));
					lstUnAssignUser.Items.Remove(new ListItem(lstUnAssignUser.Items[i].Text,lstUnAssignUser.Items[i].Value));
					//lstUnAssignUser.Items.RemoveAt(li.Value);
					//strCustomer+=li.Value + ",";
				}					
			}*/
			//lstAssignUser.Items.Add(lstUnAssignUser.SelectedItem);
			//lstUnAssignUser.Items.Remove(lstUnAssignUser.SelectedItem);
			//lstUnAssignUser.ClearSelection();
		}

		private void btnRemoveClient_Click(object sender, System.EventArgs e)
		{
			foreach(ListItem li in lstAssignUser.Items)
			{
				if (li.Selected == true)
				{
					lstUnAssignUser.Items.Add(new ListItem(li.Text,li.Value));
				}					
			}
						
			int i=0;
			for(i=lstAssignUser.Items.Count-1;i>=0;i--)
			{
				if (lstAssignUser.Items[i].Selected == true)
				{
					lstAssignUser.Items.Remove(lstAssignUser.Items[i]);
				}
			}
			getPolicies();
		}

		private void getPolicies()
		{
			strSelectedPolicies="";
			strSelectedPoliciesName="";
			hidSelectedPolicy.Value="";
			hidSelectedPolicyName.Value="";
			
			for (int i=0;i<lstAssignUser.Items.Count;i++)
			{
				strSelectedClients+=lstAssignUser.Items[i].Value + ",";
			}

			if (strSelectedClients!= "")
			{
				strSelectedClients=strSelectedClients.Substring(0,strSelectedClients.Length-1); 	
			}
			else
			{
				strSelectedClients="-1";
			}
						

			try
			{
				/*string strQuery = "SELECT CLT_CUSTOMER_LIST.CUSTOMER_ID, ISNULL(CUSTOMER_FIRST_NAME,' ') + ' ' +  ISNULL(CUSTOMER_MIDDLE_NAME,' ') +  ' ' +   ISNULL(CUSTOMER_LAST_NAME,' ')  AS CUSTOMER_NAME,  POLICY_ID,  POLICY_VERSION_ID,  POLICY_NUMBER  FROM POL_CUSTOMER_POLICY_LIST  INNER JOIN CLT_CUSTOMER_LIST   ON CLT_CUSTOMER_LIST.CUSTOMER_ID = POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID  WHERE CLT_CUSTOMER_LIST.CUSTOMER_ID IN (" + strSelectedClients + ")";  
				Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
				DataSet ds = objDataWrapper.ExecuteDataSet(strQuery);
				lstUnAssignPolicy.DataSource = ds.Tables[0];					
				lstUnAssignPolicy.DataTextField = "POLICY_NUMBER";
				lstUnAssignPolicy.DataValueField = "POLICY_NUMBER";
				lstUnAssignPolicy.DataBind();*/


				Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
				string strSql = "Select Policy_Number,Policy_ID as PolicyID from POL_CUSTOMER_POLICY_LIST where Customer_id in (" + strSelectedClients + ")";
				DataSet ds = objDataWrapper.ExecuteDataSet(strSql);
				lstUnAssignPolicy.DataSource = ds.Tables[0];
				
				lstUnAssignPolicy.Items.Clear(); 
				lstAssignPolicy.Items.Clear(); 

				//lstUnAssignPolicy.ClearSelection(); 
				lstUnAssignPolicy.DataTextField = "Policy_Number";
				lstUnAssignPolicy.DataValueField = "PolicyID";
				lstUnAssignPolicy.DataBind();
			}
			catch(Exception ex)
			{
				throw ex;
				//Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}	
		}

		private void btnSumbit_Click(object sender, System.EventArgs e)
		{
			
			if(CheckBox1.Checked == true)
			{
				strSelectedClients = "0";
				strSelectedPolicies = "0";
			}

			else if(Checkbox2.Checked == true)
			{
				strSelectedPolicies = "0";

				for (int i=0;i<lstAssignUser.Items.Count;i++)
				{
					strSelectedClients+=lstAssignUser.Items[i].Value + ",";
				}

				if (strSelectedClients!= "")
				{
					strSelectedClients=strSelectedClients.Substring(0,strSelectedClients.Length-1); 
				}
				else
				{
					strSelectedClients="-1";
				}
			}

			else
			{
				for (int i=0;i<lstAssignUser.Items.Count;i++)
				{
					strSelectedClients+=lstAssignUser.Items[i].Value + ",";
				}

				if (strSelectedClients!= "")
				{
						strSelectedClients=strSelectedClients.Substring(0,strSelectedClients.Length-1); 
				}
				else
				{
					strSelectedClients="-1";
				}


				/*for (int i=0;i<lstAssignPolicy.Items.Count;i++)
				{
					strSelectedPolicies+=lstAssignPolicy.Items[i].Value + ",";
				}
				if (strSelectedPolicies!= "")
				{
					strSelectedPolicies=strSelectedPolicies.Substring(0,strSelectedPolicies.Length-1);
				}
				else
				{
					strSelectedPolicies="-1";
				}*/

				if (strSelectedPolicies == "")
					strSelectedPolicies="-1";

				//check
				
				lstAssignPolicy.Items.Clear();
				string [] arrPolicyId ;
				string [] arrPolicyNames ;
				arrPolicyId = hidSelectedPolicy.Value.ToString().Split(',');
				arrPolicyNames = hidSelectedPolicyName.Value.ToString().Split(',');
				
				
				for(int i=0;i<arrPolicyId.Length;i++)
				{
					lstAssignPolicy.Items.Add(new ListItem(arrPolicyNames[i],arrPolicyId[i]));
				}
				
				//end

			}
			
		}
	}
}
