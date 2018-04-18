

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
using Cms.Model.Application.HomeOwners;
using Cms.BusinessLayer.BlApplication;

namespace Cms.Policies.Aspx.Homeowner
{
	/// <summary>
	/// Summary description for CopyRecrVehicles.
	/// </summary>
	public class CopyRecrVehicles :  Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected Cms.CmsWeb.Controls.CmsButton btnOK;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.DataGrid dgSerialNumbers;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.DropDownList cmbNumber;
		protected Cms.CmsWeb.Controls.CmsButton btnClose;
		string strCalledFrom="";

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			#region Setting ScreenId
			// Check from where the screen is called.
			if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] !="")
			{
				strCalledFrom=Request.QueryString["CalledFrom"].ToString();
			}
		
			base.ScreenId="243_0";
			

			btnClose.Attributes.Add("onclick","javascript:window.close();");

			btnOK.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnOK.PermissionString = gstrSecurityXML;

			btnClose.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Execute;
			btnClose.PermissionString = gstrSecurityXML;

			btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString = gstrSecurityXML;
		}

		
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		

		private void InitializeComponent()
		{    
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			pnlGrid.Visible = true;

			int intCount = Convert.ToInt32(this.cmbNumber.SelectedItem.Value);
			ArrayList alItems = new ArrayList();

			for ( int i = 1; i < intCount + 1; i++ )
			{
				Serial objSerial = new Serial();
				objSerial.Text = "Serial Number for " + i.ToString();

				alItems.Add(objSerial);

			}
			
			dgSerialNumbers.DataSource = alItems;
			dgSerialNumbers.DataBind();

		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			int intRetVal = Save();
			
			//Company Id # exceeds the range
			if (intRetVal == -1)
			{
				lblMessage.Visible = true;
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"534");
				return;
			}

			if (intRetVal == 1)
			{
				string strScript = @"<script>" +// "window.opener.document.getElementById('lblMessage').style.display='inline';" +
					                            // "window.opener.document.getElementById('lblMessage').innerText='Information copied successfully';" + 
										"window.opener.Refresh(1,1);" + 
										"window.close();" + 
									 "</script>" 
										;
	

				if (!ClientScript.IsStartupScriptRegistered("Refresh"))
				{
					ClientScript.RegisterStartupScript(this.GetType(),"Refresh",strScript);

				}

			}


		}
		
		/// <summary>
		/// Copies the specified record the required number of times.
		/// </summary>
		/// <returns></returns>
		private int Save()
		{
			ArrayList alItems = new ArrayList();
			
			int intCustomerID = Convert.ToInt32(GetCustomerID());
			int intAppID = Convert.ToInt32(GetPolicyID());
			int intAppVersionID = Convert.ToInt32(GetPolicyVersionID());
			int intRecVehID = Convert.ToInt32(Request.QueryString["REC_VEH_ID"]);

			foreach(DataGridItem dgi in this.dgSerialNumbers.Items )
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					string strSerialNumber = ((TextBox)dgi.FindControl("txtSerialNumber")).Text.Trim();
					
					if ( strSerialNumber == "" )
					{
						lblMessage.Visible = true;
						lblMessage.Text = "Please enter the Serial number";
						return -4;
					}
					
					ClsRecrVehiclesInfo objInfo = new ClsRecrVehiclesInfo();
					
					objInfo.CUSTOMER_ID = intCustomerID;
					objInfo.APP_ID = intAppID;
					objInfo.APP_VERSION_ID = intAppVersionID;
					objInfo.REC_VEH_ID = intRecVehID;
					objInfo.SERIAL = strSerialNumber;

					alItems.Add(objInfo);

				}

			}

			ClsHomeRecrVehicles objVehicles = new ClsHomeRecrVehicles();
			ClsUmbrellaRecrVeh objUmbrella = new ClsUmbrellaRecrVeh();

			int intRetVal = 0;
			
			lblMessage.Visible = true;

			try
			{
				if ( Request.QueryString["CalledFrom"] == "HOME" )
				{
					intRetVal = objVehicles.CopyPolicy(alItems);
				}

				if ( Request.QueryString["CalledFrom"].ToString() == "Umbrella" )
				{
					intRetVal = objUmbrella.CopyUmbPolicy(alItems);
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return intRetVal;
			}
			
			return intRetVal;

		}

	}
	
	/// <summary>
	/// Class used for binding the datagrid
	/// </summary>
	public class Serial
	{
		public string text;
		public string serialNumber;

		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		public string SerialNumber
		{
			get { return serialNumber; }
			set { serialNumber = value ;}
		}

	}

}
