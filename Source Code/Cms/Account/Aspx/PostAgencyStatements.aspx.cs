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
using Cms.BusinessLayer.BlAccount;
using Cms.CmsWeb.Controls;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for PostAgencyStatements.
	/// </summary>
	public class PostAgencyStatements : Cms.Account.AccountBase
	{
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnPost;
		protected System.Web.UI.WebControls.DropDownList cmbMonth;
		protected System.Web.UI.WebControls.DropDownList cmbYEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMonth;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DropDownList CmbCommType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		string calledFrom="";
	    
		private void SetErrorMessage()
		{
			rfvMonth.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("477");
		}

		private void FillComboBox(int yearno,string commType)
		{	
		/*	//added for year drop down
			int currYear = System.DateTime.Now.Year;
			int prevYear = currYear -1;
			cmbYEAR.Items.Add(new ListItem(prevYear.ToString(),prevYear.ToString()));
			cmbYEAR.Items.Add(new ListItem(currYear.ToString(),currYear.ToString()));
		*/		
			//
		
			cmbMonth.Items.Clear();
			ClsAgencyStatement objAgencyStatement = new ClsAgencyStatement();
			cmbMonth.DataSource =  objAgencyStatement.GetMonths(yearno,commType);
			cmbMonth.DataTextField = "Month_Name";
			cmbMonth.DataValueField = "Month_Index";
			cmbMonth.DataBind();
			
		}



		private void FillCommType()
		{
			if(Request.QueryString["called_from"] != null && Request.QueryString["called_from"].ToString() != "")
				calledFrom = Request.QueryString["called_from"].ToString();

			if(calledFrom.Equals("ComAppComm"))
			{
				ListItem LiReg = new ListItem("Regular Commission","REG");
				ListItem LiAdc = new ListItem("Additional Commission","ADC");
				if(CmbCommType.Items.Contains(LiReg) && CmbCommType.Items.Contains(LiAdc))
				{
					CmbCommType.Items.Remove(LiReg);
					CmbCommType.Items.Remove(LiAdc);
				}
			}
			else
			{
				ListItem LiCom = new ListItem("Complete App Commission","CAC");
				if(CmbCommType.Items.Contains(LiCom))
				{
					CmbCommType.Items.Remove(LiCom);
				}
			}
		}
		


        //Added by uday to fill year
		private void FillYear()
		{
			//added for year drop down
			ClsAgencyStatement objAgencyStatement = new ClsAgencyStatement();
			int currYear = System.DateTime.Now.Year;
			int prevYear = currYear -1;	
			int retvalue = objAgencyStatement.GetYearAgencyStatement(prevYear,CmbCommType.SelectedValue.ToString().Trim());
			if(retvalue.ToString()=="1")
			{
				//if(cmbYEAR.Items.Contains(new ListItem(prevYear.ToString(),prevYear.ToString())))
				cmbYEAR.Items.Remove(new ListItem(prevYear.ToString(),prevYear.ToString()));
				cmbYEAR.Items.Remove(new ListItem(currYear.ToString(),currYear.ToString()));
			//	cmbYEAR.Items.Add(new ListItem(prevYear.ToString(),prevYear.ToString()));
				cmbYEAR.Items.Add(new ListItem(currYear.ToString(),currYear.ToString()));
			}
			else
			{
				cmbYEAR.Items.Remove(new ListItem(prevYear.ToString(),prevYear.ToString()));
				cmbYEAR.Items.Remove(new ListItem(currYear.ToString(),currYear.ToString()));
				cmbYEAR.Items.Add(new ListItem(prevYear.ToString(),prevYear.ToString()));
				cmbYEAR.Items.Add(new ListItem(currYear.ToString(),currYear.ToString()));
			}
			int yearno = Convert.ToInt32(cmbYEAR.SelectedValue);
		}
           
		//
		private void Page_Load(object sender, System.EventArgs e)
		{

			base.ScreenId = "225";
			btnPost.Attributes.Add("Onclick","javascript:HideShowTransactionInProgress();");

			btnPost.PermissionString = gstrSecurityXML;
			btnPost.CmsButtonClass = CmsButtonType.Write;

			btnReset.PermissionString = gstrSecurityXML;
			btnReset.CmsButtonClass = CmsButtonType.Write;

			btnReset.Attributes.Add("onclick","javascript:document.forms[0].reset();");

			FillCommType();
			
			if (!Page.IsPostBack )
			{	
				FillYear();
				FillComboBox(Convert.ToInt32(cmbYEAR.SelectedValue),CmbCommType.SelectedValue.ToString().Trim());
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
			this.btnPost.Click += new System.EventHandler(this.btnPrint_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.cmbYEAR.SelectedIndexChanged += new System.EventHandler(this.cmbYEAR_SelectedIndexChanged);
			this.CmbCommType.SelectedIndexChanged += new System.EventHandler(this.CmbCommType_SelectedIndexChanged);
			

		}
		#endregion

		private void cmbYEAR_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int currYear = System.DateTime.Now.Year;
			int prevYear = currYear -1;			
				FillComboBox(Convert.ToInt32(cmbYEAR.SelectedValue),CmbCommType.SelectedValue.ToString().Trim());
			
		}

		private void CmbCommType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillComboBox(Convert.ToInt32(cmbYEAR.SelectedValue),CmbCommType.SelectedValue.ToString().Trim());
			
		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			try
			{

				ClsAgencyStatement objAgencyStatement = new ClsAgencyStatement();
				int currYear = System.DateTime.Now.Year;
				int prevYear = currYear -1;
				int userID = int.Parse(GetUserId());
				int retvalue = objAgencyStatement.SaveAgencyStatement(int.Parse(cmbMonth.SelectedValue),int.Parse(cmbYEAR.SelectedValue),CmbCommType.SelectedValue.ToString().Trim(),userID);

				if(retvalue == -1)
				{
					lblMessage.Text = "Commission not posted for this month as no record exists for this month";			
				}
				else if (retvalue == -2)
				{
					lblMessage.Text = "Agency Statement " +"(" + CmbCommType.SelectedItem.Text + ")" + "for this month and year has already been processed.";			
				}
				else
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
					//added by uday
					if(cmbMonth.DataValueField=="12" && cmbYEAR.SelectedValue ==prevYear.ToString())					
						cmbYEAR.Items.Remove(prevYear.ToString());
					//
				}
			}
			catch(Exception objExp)
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("21") + "\n " + objExp.Message.ToString();
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}
	}
}
