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
	public class ClientInstallmentPaymentSchedule : Cms.CmsWeb.cmsbase
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
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSelectedPolicyName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSelectedPolicy; 
		protected Cms.CmsWeb.Controls.CmsButton btnReport;

		protected string strSelectedClients = "";
		protected string strSelectedPolicies = "";
		protected Cms.CmsWeb.Controls.CmsButton btnSumbit;
		protected string strSelectedPoliciesName = "";
		public string strAgencyID="";
		protected string strQuery="";
        Cms.DataLayer.DataWrapper objDataWrapper;
		

		private void Page_Load(object sender, System.EventArgs e)
		{
            objDataWrapper  = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
			base.ScreenId="333";
			btnReport.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReport.PermissionString				=	gstrSecurityXML;	

			btnAddClient.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Execute;
			btnAddClient.PermissionString = gstrSecurityXML;
			
			btnRemoveClient.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Execute;
			btnRemoveClient.PermissionString = gstrSecurityXML;
			
			this.CheckBox1.Attributes.Add("onClick","javascript: ShowContact();");
			this.Checkbox2.Attributes.Add("onClick","javascript: ShowPolicy();");
			
			strSelectedPolicies = hidSelectedPolicy.Value;
			strSelectedPoliciesName = hidSelectedPolicyName.Value;
			
			btnReport.Attributes.Add("onClick","ShowReport();return false;");

			if (!Page.IsPostBack)
			{
				/* AGENCY VALUES
				* Check the SystemID of the logged in user.
				* If the user is not a Wolverine user then display records of that agency ONLY
				* else the normal flow follows */
				string  strSystemID			 = GetSystemId();

                string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
				if ( strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())
				{
					DataSet objDataSet =Cms.BusinessLayer.BlCommon.ClsAgency.GetAgencyIDAndNameFromCode(strSystemID);

					if (objDataSet.Tables[0].Rows.Count > 0 )
					{
						strAgencyID = objDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString();
						fillListBox(strAgencyID);
					}
				}
				else
					fillListBox(null);
			}
		}

		protected void fillListBox(string AgencyID)
		{
			string strSQL = "";
			if(AgencyID != null)
				//strSQL = "Select distinct (isnull(CUSTOMER_FIRST_NAME,'') + ' ' + isnull(CUSTOMER_MIDDLE_NAME,'') + ' '+ isnull(CUSTOMER_LAST_NAME,'')) as CUSTOMER_FIRST_NAME ,CUSTOMER_ID as ID FROM CLT_CUSTOMER_LIST  where customer_agency_id = '" + strAgencyID + "' order by customer_first_name";
				strSQL = "Select distinct (isnull(CUSTOMER_FIRST_NAME,'') + ' ' + isnull(CUSTOMER_MIDDLE_NAME,'') + ' '+ " +
					" isnull(CUSTOMER_LAST_NAME,'')) as CUSTOMER_FIRST_NAME ,PCL.CUSTOMER_ID as ID FROM CLT_CUSTOMER_LIST  INNER JOIN " +
					" POL_CUSTOMER_POLICY_LIST PCL ON CLT_CUSTOMER_LIST.CUSTOMER_ID=PCL.CUSTOMER_ID " +
					" where PCL.AGENCY_ID = '" + strAgencyID + "' order by customer_first_name ";

			else
				strSQL = "Select distinct (isnull(CUSTOMER_FIRST_NAME,'') + ' ' + isnull(CUSTOMER_MIDDLE_NAME,'') + ' '+ isnull(CUSTOMER_LAST_NAME,'')) as CUSTOMER_FIRST_NAME ,CUSTOMER_ID as ID FROM CLT_CUSTOMER_LIST  order by customer_first_name";	
				
			DataSet ds = objDataWrapper.ExecuteDataSet(strSQL);
			lstUnAssignUser.DataSource = ds.Tables[0];					
			lstUnAssignUser.DataTextField = "customer_first_name";
			lstUnAssignUser.DataValueField = "ID";
			lstUnAssignUser.DataBind();
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
				string  strSystemID			 = GetSystemId();

                string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
				if ( strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())
				{
					DataSet objDataSet =Cms.BusinessLayer.BlCommon.ClsAgency.GetAgencyIDAndNameFromCode(strSystemID);

					if (objDataSet.Tables[0].Rows.Count > 0 )
					{
						strAgencyID = objDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString();
					}
				}

				/*string strQuery = "SELECT CLT_CUSTOMER_LIST.CUSTOMER_ID, ISNULL(CUSTOMER_FIRST_NAME,' ') + ' ' +  ISNULL(CUSTOMER_MIDDLE_NAME,' ') +  ' ' +   ISNULL(CUSTOMER_LAST_NAME,' ')  AS CUSTOMER_NAME,  POLICY_ID,  POLICY_VERSION_ID,  POLICY_NUMBER  FROM POL_CUSTOMER_POLICY_LIST  INNER JOIN CLT_CUSTOMER_LIST   ON CLT_CUSTOMER_LIST.CUSTOMER_ID = POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID  WHERE CLT_CUSTOMER_LIST.CUSTOMER_ID IN (" + strSelectedClients + ")";  
				Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
				DataSet ds = objDataWrapper.ExecuteDataSet(strQuery);
				lstUnAssignPolicy.DataSource = ds.Tables[0];					
				lstUnAssignPolicy.DataTextField = "POLICY_NUMBER";
				lstUnAssignPolicy.DataValueField = "POLICY_NUMBER";
				lstUnAssignPolicy.DataBind();*/

				Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
				string strSql = "Select distinct Policy_Number,Policy_ID as PolicyID from POL_CUSTOMER_POLICY_LIST where Customer_id in (" + strSelectedClients + ")";
				if(strAgencyID != "")
					strSql += " AND AGENCY_ID = " + strAgencyID;

				DataSet ds = objDataWrapper.ExecuteDataSet(strSql);
				lstUnAssignPolicy.DataSource = ds.Tables[0];
				
				lstUnAssignPolicy.Items.Clear(); 
				lstAssignPolicy.Items.Clear(); 

				lstUnAssignPolicy.DataTextField = "Policy_Number";
				lstUnAssignPolicy.DataValueField = "PolicyID";
				lstUnAssignPolicy.DataBind();
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}	
		}	
	}
}
