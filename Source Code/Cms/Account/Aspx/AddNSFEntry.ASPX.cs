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
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Account;
using Cms.CmsWeb;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for DepositDetails.
	/// </summary>
	public class AddNSFEntry : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgDepositDetails;
	
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton Cmsbutton1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGLAccountXML;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICYINFO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicy;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersion;
		protected System.Web.UI.WebControls.Label lblMsg;
		public string URL;

		private void Page_Load(object sender, System.EventArgs e)
		{

			btnSave.Attributes.Add("onclick","javascript:Page_ClientValidate();return DoValidationCheckOnSave();");
			URL = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
			
			//Setting the screen id
			base.ScreenId = "347";
			#region setting security permission
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			

			btnSave.CmsButtonClass = CmsButtonType.Write;
			btnSave.PermissionString = gstrSecurityXML;

			Cmsbutton1.CmsButtonClass = CmsButtonType.Write;
			Cmsbutton1.PermissionString = gstrSecurityXML;

		//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			#endregion
			

			if ( !Page.IsPostBack )
			{
				BindGrid();                				
			}
		}

		public void BindGrid()
		{
			DataTable dt=new DataTable("Dummy");
			dt.Columns.Add("First");
			for(int i=0; i<11;i++)
			{
				DataRow  dr=dt.NewRow();
				dr[0]=i;
				dt.Rows.Add(dr);
			}
			dgDepositDetails.DataSource =dt;
			dgDepositDetails.DataBind();

		}

		/// <summary>
		/// Get query string from url into hidden controls
		/// </summary>
	
		/// <summary>
		/// Fetch other information abour the deposit details screeen
		/// </summary>
	
		/// <summary>
		/// Binds the data set with the data grid
		/// </summary>
		
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
			this.dgDepositDetails.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgDepositDetails_ItemDataBound);
			this.Cmsbutton1.Click += new System.EventHandler(this.Cmsbutton1_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			Save();
			BindGrid();
		}

		private void dgDepositDetails_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			
			if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{
				int index = e.Item.ItemIndex + 2;	

				TextBox txtTOTAL_DUE = (TextBox)e.Item.FindControl("txtTOTAL_DUE");
				txtTOTAL_DUE.Attributes.Add("onblur","javascript:FormatAmount(this);");
				
				TextBox txtPolicyNo = (TextBox)e.Item.FindControl("txtPolicyNo");				
				SetPolicyAttributes(txtPolicyNo, e.Item.ItemIndex + 2);
				
				TextBox txtCheckPolicyNo = (TextBox)e.Item.FindControl("txtCheckPolicyNo");
				//Added For Itrack Issue #6190
				txtCheckPolicyNo.Attributes.Add("onBlur","FillPolDetails('" + txtCheckPolicyNo.ClientID + "','" + (e.Item.ItemIndex + 2).ToString() + "' )");
				
				//txtCheckPolicyNo.TextMode = TextBoxMode.Password;
				SetPolicyAttributes(txtCheckPolicyNo, e.Item.ItemIndex + 2);

				HtmlImage imgSelect = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("imgPOLICY_NO");
				if (imgSelect != null)
				{
					//SetPolicyTextBox
					imgSelect.Attributes.Add("onclick","OpenPolicyLookup('" + (e.Item.ItemIndex + 2).ToString() + "')" );
				}
				
				TextBox objTxt = (TextBox)e.Item.FindControl("txtPolicyNo");
				objTxt.Attributes.Add("onBlur","FillPolDetails('" + objTxt.ClientID + "','" + (e.Item.ItemIndex + 2).ToString() + "' )");

				
				SetValidators(e);
			}
		}

		/// <summary>
		/// Sets the attributes related to policy no
		/// </summary>
		/// <param name="txtPolicyNo"></param>
		/// <returns></returns>
		private void SetPolicyAttributes(TextBox txtPolicyNo, int intRowNo)
		{
			/*Modifieded by Asfa (12-June-2008) - iTrack #3906 (Note: 9)*/
			txtPolicyNo.Attributes.Add("onchange","javascript:checkPolicyNo(" + intRowNo.ToString() + ")");			
			//txtPolicyNo.Attributes.Add("onchange","javascript:checkPolicyNo(" + intRowNo.ToString() + ");checkPolicyStatus(" + intRowNo.ToString() + ");");
			txtPolicyNo.Attributes.Add("RowNo",intRowNo.ToString());
		}

		/// <summary>
		/// Sets the validatiors properties used in this page
		/// </summary>
		/// <param name="e">Data grid items</param>
		private void SetValidators(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			RegularExpressionValidator revValidator = (RegularExpressionValidator) e.Item.FindControl("revTOTAL_DUE");
			revValidator.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "116");
			revValidator.ValidationExpression = aRegExpDoublePositiveNonZero;
			revValidator.Attributes.Add("RowNo",(e.Item.ItemIndex + 2).ToString());
		
			CustomValidator csvValidate = (CustomValidator) e.Item.FindControl("csvPolicyNo");
			csvValidate.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "2");
			csvValidate.Attributes.Add("RowNo",(e.Item.ItemIndex + 2).ToString());

			csvValidate = (CustomValidator) e.Item.FindControl("csvCheckPolicyNo");
			csvValidate.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "2");
			csvValidate.Attributes.Add("RowNo",(e.Item.ItemIndex + 2).ToString());

			

		}

		private void Save()
		{
			ArrayList alRecr = new ArrayList();
			//Hashtable haBackUp
			foreach(DataGridItem dgi in dgDepositDetails.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					TextBox txtText;
					txtText = (TextBox)dgi.FindControl("txtTOTAL_DUE");

					if (txtText.Text == "")
						continue;

					ClsAddNSFEntryInfo  objInfo = new ClsAddNSFEntryInfo();
					//Saving the policy no and version no
					if (((HtmlInputHidden)dgi.FindControl("hidPOLICY_ID")).Value != "")
					{
						objInfo.POLICY_ID = int.Parse(((HtmlInputHidden)dgi.FindControl("hidPOLICY_ID")).Value);
						objInfo.POLICY_VERSION_ID = int.Parse(((HtmlInputHidden)dgi.FindControl("hidPOLICY_VERSION_ID")).Value);
						objInfo.CUSTOMER_ID  = int.Parse(((HtmlInputHidden)dgi.FindControl("hidCustomerId")).Value);

					}
					
					if (((TextBox)dgi.FindControl("txtPolicyNo")).Text != "")
					{
						objInfo.POLICY_NO = ((TextBox)dgi.FindControl("txtPolicyNo")).Text;
					}

					txtText = (TextBox)dgi.FindControl("txtTOTAL_DUE");
					if (txtText.Text.Trim() != "")
						objInfo.TOTAL_DUE = Double.Parse(txtText.Text);

					objInfo.TransID =(Convert.ToInt32(enumTransType.NSF));

					objInfo.CREATED_BY = int.Parse(GetUserId());
					objInfo.CREATED_DATETIME = DateTime.Now;
					objInfo.MODIFIED_BY = int.Parse(GetUserId());
					objInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//TO MOVE TO LOCAL VSS
//					if(hidPolicy.Value != null && hidPolicy.Value !="")
//					{			
//						objInfo.POLICY_ID = int.Parse(hidPolicy.Value.ToString());
//					}
//					if(hidPolicyVersion !=null && hidPolicyVersion.Value !="")
//					{
//						objInfo.POLICY_VERSION_ID = int.Parse(hidPolicyVersion.Value.ToString());
//					}
//					if(hidCustomer.Value !=null && hidCustomer.Value !="")
//					{
//						objInfo.CUSTOMER_ID = int.Parse(hidCustomer.Value.ToString());					
//					}
					alRecr.Add(objInfo);
				}
			}
			
			Cms.BusinessLayer.BlCommon.Accounting.ClsAddNSFEntry objNSFEntry=new Cms.BusinessLayer.BlCommon.Accounting.ClsAddNSFEntry();
			
			int returnResult=0;
			if(alRecr.Count!=0)
			{
				returnResult=objNSFEntry.InsertAdjustNfsEntry(alRecr);
				if ( returnResult >0)
				{
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"4");
				}
				else if(returnResult==-1)
				{
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "1");
				}
				else
				{
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "20");
				}
                ClientScript.RegisterStartupScript(this.GetType(),"Save", "<script >InsertPolicyTextBox();</script>");
				lblMessage.Visible = true;
			}
			else
			{
				lblMessage.Visible =true;
				lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"6");

			}
			
			
		}
		

		/// <summary>
		/// Shows the status of each line item, return bl class method
		/// </summary>
		/// <param name="alStatus"></param>
		private void ShowLineItemStatus(ArrayList alStatus)
		{
			try
			{
				int ctr = 0;
				foreach(DataGridItem dgi in dgDepositDetails.Items)
				{
					if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
					{

						TextBox txtText;
						txtText = (TextBox)dgi.FindControl("txtTOTAL_DUE");

						if (txtText.Text == "")
							continue;


						switch(int.Parse(alStatus[ctr].ToString()))
						{
							case -2:
								//Invalid policy
								((Label)dgi.FindControl("lblStatus")).Text = "Invalid policy number. Please enter a valid policy number.";
								break;
							case -1:
								((Label)dgi.FindControl("lblStatus")).Text = "Invalid policy number. Please enter a valid policy number.";
								break;
							default:
								((Label)dgi.FindControl("lblStatus")).Text = "";
								break;
						}
						ctr++;
					}
				}
			}
			catch(Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}

		
		/// <summary>
		/// Returns the value of specified node from hidGLAccountXML
		/// </summary>
		/// <param name="nodeName">Name of node whose value to returnes</param>
		/// <returns>Value of node</returns>
		private string GetValueFromGLAccountXML(string nodeName)
		{
			if (hidGLAccountXML.Value != "")
			{
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.LoadXml(hidGLAccountXML.Value);

				//Retreiving the GL ID and Fiscal Id
				System.Xml.XmlNode objNode = doc.SelectSingleNode("/NewDataSet/Table/" + nodeName );
				
				if(objNode != null)
				{	
					return objNode.InnerXml;
				}
				else
				{
					return "";
				}
			}
			else
			{
				return "";
			}
		}

		private void Cmsbutton1_Click(object sender, System.EventArgs e)
		{
			ArrayList alRecr = new ArrayList();
			//Hashtable haBackUp
			foreach(DataGridItem dgi in dgDepositDetails.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					TextBox txtText;
					txtText = (TextBox)dgi.FindControl("txtTOTAL_DUE");

					if (txtText.Text == "")
						continue;

					ClsAddNSFEntryInfo  objInfo = new ClsAddNSFEntryInfo();
					//Saving the policy no and version no
					if (((HtmlInputHidden)dgi.FindControl("hidPOLICY_ID")).Value != "")
					{
						objInfo.POLICY_ID = int.Parse(((HtmlInputHidden)dgi.FindControl("hidPOLICY_ID")).Value);
						objInfo.POLICY_VERSION_ID = int.Parse(((HtmlInputHidden)dgi.FindControl("hidPOLICY_VERSION_ID")).Value);
						objInfo.CUSTOMER_ID  = int.Parse(((HtmlInputHidden)dgi.FindControl("hidCustomerId")).Value);

					}
					
					if (((TextBox)dgi.FindControl("txtPolicyNo")).Text != "")
					{
						objInfo.POLICY_NO = ((TextBox)dgi.FindControl("txtPolicyNo")).Text;
					}

					txtText = (TextBox)dgi.FindControl("txtTOTAL_DUE");
					if (txtText.Text.Trim() != "")
						objInfo.TOTAL_DUE = Double.Parse(txtText.Text);

					objInfo.CREATED_BY = int.Parse(GetUserId());
					objInfo.CREATED_DATETIME = DateTime.Now;
					objInfo.MODIFIED_BY = int.Parse(GetUserId());
					objInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					
					objInfo.TransID =(Convert.ToInt32(enumTransType.LF));
					alRecr.Add(objInfo);
				}
			}
			
			Cms.BusinessLayer.BlCommon.Accounting.ClsAddNSFEntry objNSFEntry=new Cms.BusinessLayer.BlCommon.Accounting.ClsAddNSFEntry();
			int returnResult=objNSFEntry.InsertAdjustNfsEntry(alRecr);
			
			if ( returnResult >0)
			{
				//saved successfully
				btnSave.Visible =false;
				lblMessage.Text = ClsMessages.FetchGeneralMessage("2");
			}
			else if(returnResult==-2)
			{
				//error occured, showing the error message
				lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "9");
				//	ShowLineItemStatus(alStatus);
			}
			else
			{
				lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "20");
			}
            ClientScript.RegisterStartupScript(this.GetType(),"Save", "<script >InsertPolicyTextBox();</script>");
			lblMessage.Visible = true;
		}

		
	}
}
