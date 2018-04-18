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
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Cms.CmsWeb;

namespace Cms.Account.Aspx 
{
	/// <summary>
	/// Summary description for ChargeLateFees.
	/// </summary>
	public class AddINSF : Cms.Account.AccountBase  
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgDepositDetails;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGLAccountXML;
		protected Cms.CmsWeb.Controls.CmsButton Cmsbutton1;
		protected Cms.CmsWeb.Controls.CmsButton btnSave; 
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICYINFO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicy;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersion;
        
		
		public string URL;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			URL = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
			btnSave.Attributes.Add("onclick","javascript:Page_ClientValidate();return DoValidationCheckOnSave();");			
			base.ScreenId = "443_0";		//Commented and added on 18/9/2009 for #6135.
			btnSave.CmsButtonClass = CmsButtonType.Write;
			btnSave.PermissionString = gstrSecurityXML;

			if ( !Page.IsPostBack )
			{
				BindGrid();
			}
		}
		public void BindGrid()
		{
			DataTable dt = new DataTable("Fee");
			dt.Columns.Add("first");
			for (int i=0; i<=10; i++)
			{
				DataRow dr = dt.NewRow();
				dr[0] = i;
				dt.Rows.Add(dr);
			}
			dgDepositDetails.DataSource = dt;
			dgDepositDetails.DataBind();
		}

		private void dgDepositDetails_ItemDataBound (object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				int index = e.Item.ItemIndex + 2;


				TextBox txtTOTAL_DUE = (TextBox)e.Item.FindControl("txtTOTAL_DUE");
				txtTOTAL_DUE.Attributes.Add("onblur","javascript:FormatAmount(this);");
				
				TextBox txtPolicyNo = (TextBox)e.Item.FindControl("txtPolicyNo");				
				SetPolicyAttributes(txtPolicyNo, e.Item.ItemIndex + 2);
				
				TextBox txtCheckPolicyNo = (TextBox)e.Item.FindControl("txtCheckPolicyNo");
				SetPolicyAttributes(txtCheckPolicyNo, e.Item.ItemIndex + 2);

				HtmlImage imgSelect = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("imgPOLICY_NO");
				if (imgSelect != null)
				{					
					imgSelect.Attributes.Add("onclick","OpenPolicyLookup('" + (e.Item.ItemIndex + 2).ToString() + "')" );
				}
				
				TextBox objTxt = (TextBox)e.Item.FindControl("txtPolicyNo");
				objTxt.Attributes.Add("onBlur","FillPolDetails('" + objTxt.ClientID + "','" + (e.Item.ItemIndex + 2).ToString() + "' )");				
				
				txtCheckPolicyNo.Attributes.Add("onBlur","FillPolDetails('" + txtCheckPolicyNo.ClientID + "','" + (e.Item.ItemIndex + 2).ToString() + "' )");
				
				SetValidators(e);

				
				
			}
		}


		private void SetPolicyAttributes(TextBox txtPolicyNo, int intRowNo)
		{
			txtPolicyNo.Attributes.Add("onchange","javascript:checkPolicyNo(" + intRowNo.ToString() + ");");			
			txtPolicyNo.Attributes.Add("RowNo",intRowNo.ToString());
		}


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
			csvValidate.ErrorMessage = ClsMessages.GetMessage(base.ScreenId,"2");
			csvValidate.Attributes.Add("RowNo",(e.Item.ItemIndex + 2).ToString());

			

		}

		private void btnSave_Click (object sender, System.EventArgs e)
		{
			Save();
			BindGrid();
		}
       
		private void Save()
		{
			ArrayList alRecr = new ArrayList();			
			foreach(DataGridItem dgi in dgDepositDetails.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					TextBox txtText;
					txtText = (TextBox)dgi.FindControl("txtTOTAL_DUE");

					if (txtText.Text == "")
						continue;

					ClsAddNSFEntryInfo  objInfo = new ClsAddNSFEntryInfo();					
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

					objInfo.TransID =(Convert.ToInt32(enumTransType.CIF));

					objInfo.CREATED_BY = int.Parse(GetUserId());
					objInfo.CREATED_DATETIME = DateTime.Now;
					objInfo.MODIFIED_BY = int.Parse(GetUserId());
					objInfo.LAST_UPDATED_DATETIME = DateTime.Now;					
					alRecr.Add(objInfo);
				}
			}
			
			Cms.BusinessLayer.BlCommon.Accounting.ClsAddNSFEntry objNSFEntry=new Cms.BusinessLayer.BlCommon.Accounting.ClsAddNSFEntry();
			
			int returnResult=0;
			if(alRecr.Count!=0)
			{
				returnResult=objNSFEntry.InsertInstallmentFee(alRecr);
				if ( returnResult >0)
				{
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "4");
				}
				else if(returnResult==-1)
				{
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,  "1");
				}
				else
				{
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "20");
				}
				ClientScript.RegisterStartupScript(this.GetType(),"Save","<script >InsertPolicyTextBox();</script>");
				lblMessage.Visible = true;
			}
			else
			{
				lblMessage.Visible =true;
				lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "6");

			}
			
			
		}

		private void ShowLineItemStatus (ArrayList alist)
		{
			try
			{
				int ctr = 0;
				foreach (DataGridItem dgi in dgDepositDetails.Items)
				{
					if(dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem)
					{
						TextBox txtText;
						txtText =(TextBox)dgi.FindControl("txtTOTAL_DUE");

						if (txtText.Text == "")
							continue;
						ctr++;
					}
				}
			}
			catch (Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}

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
			//this.Cmsbutton1.Click += new System.EventHandler(this.Cmsbutton1_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	
	   
	  
	
	}
}
