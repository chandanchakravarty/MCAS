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
using Cms.BusinessLayer.BlCommon.Accounting;

namespace Cms.CmsWeb.Accounting
{
	/// <summary>
	/// code behind for PostingInterface.
	/// This page hosts tabs for all other posting interface p[ages : asset, liability, equity, income, expense
	/// </summary>
	public class PostingInterface : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.WebControls.Label capGENLED; //sneha
        protected System.Web.UI.WebControls.Label capGENLEDPOST; //SNEHA
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableRow formTable;
		protected System.Web.UI.WebControls.Label capGL_ID;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.DropDownList cmbFISCAL_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFiscalID;
		public string strFiscalID ="";
		public string strTabID="";
		public const int GL_ID = 1;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

		
			
		private void Page_Load(object sender, System.EventArgs e)
		{
            base.ScreenId = "126";
			SetTabUrl();
			if(!Page.IsPostBack)
			{
				FillCombos();
				SetFiscalYear();
				strFiscalID = cmbFISCAL_ID.SelectedValue.ToString(); 
				
				// TabURLs="PiAsset.aspx?GL_ID=1,PiLiabilities.aspx?GL_ID=1,PiEquity.aspx?GL_ID=1,PiIncome.aspx?GL_ID=1,PiExpenses.aspx?GL_ID=1,BankAccountMapping.aspx?GL_ID=1"
				TabCtl.TabURLs = "PiAsset.aspx?GL_ID=" + GL_ID 
				+ "&FISCAL_ID=" + strFiscalID 
				+ "," 
				+ "PiLiabilities.aspx?GL_ID=" + GL_ID 
				+ "&FISCAL_ID=" + strFiscalID 
				+ "," 
				+ "PiEquity.aspx?GL_ID=" + GL_ID 
				+ "&FISCAL_ID=" + strFiscalID 
				+ "," 
				+ "PiIncome.aspx?GL_ID=" + GL_ID 
				+ "&FISCAL_ID=" + strFiscalID 
				+ "," 
				+ "PiExpenses.aspx?GL_ID=" + GL_ID 
				+ "&FISCAL_ID=" + strFiscalID 
				+ "," 
				+ "BankAccountMapping.aspx?GL_ID=" + GL_ID 
				+ "&FISCAL_ID=" + strFiscalID;

                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1359"); //sneha
			}
            
            capGENLED.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1331"); //sneha
            capGENLEDPOST.Text = "Posting Interface";//Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1332"); //sneha

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
			this.cmbFISCAL_ID.SelectedIndexChanged += new System.EventHandler(this.cmbFISCAL_ID_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		/// <summary>
		/// This function defaults the fiscal yr drop down to current yr
		/// </summary>
		private void SetFiscalYear()
		{
			try
			{
				DateTime tranDate = DateTime.Now;
				string fdate;

				for(int ctr = 0; ctr < cmbFISCAL_ID.Items.Count; ctr++)
				{
					fdate = cmbFISCAL_ID.Items[ctr].Text;
				
					if (fdate.Trim() == "")
					{
						continue;
					}
			
                    ////Getting the financial dates, from financial year
                    string d1 = fdate.Substring(fdate.IndexOf("(") + 1, 11);
                    string d2 = fdate.Substring(fdate.IndexOf("-") + 1, 11);

                    if (tranDate >= DateTime.Parse(d1) && tranDate <= DateTime.Parse(d2))
                    {
                        //Transaction date is in between financial dates
                        //Hence selecting this fiscal year
                        cmbFISCAL_ID.SelectedIndex = ctr;
                        break;
                    }	
				}
			}
			catch (Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}
		private void FillCombos()
		{
			try
			{
				Cms.BusinessLayer.BlCommon.Accounting.ClsGeneralLedger.GetGeneralLedgersIndropDown(cmbFISCAL_ID);
				//cmbFISCAL_ID.Items.Insert(0,new ListItem("",""));
						
			}
			catch(Exception objEx)
			{
				lblMessage.Text = objEx.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
			}
		}
		private void SetTabUrl()
		{
			if(hidFiscalID.Value!="")
			{
				TabCtl.TabURLs = "PiAsset.aspx?GL_ID=" + GL_ID 
					+ "&FISCAL_ID=" + hidFiscalID.Value 
					+ "," 
					+ "PiLiabilities.aspx?GL_ID=" + GL_ID 
					+ "&FISCAL_ID=" + hidFiscalID.Value 
					+ "," 
					+ "PiEquity.aspx?GL_ID=" + GL_ID 
					+ "&FISCAL_ID=" + hidFiscalID.Value 
					+ "," 
					+ "PiIncome.aspx?GL_ID=" + GL_ID 
					+ "&FISCAL_ID=" + hidFiscalID.Value 
					+ "," 
					+ "PiExpenses.aspx?GL_ID=" + GL_ID 
					+ "&FISCAL_ID=" + hidFiscalID.Value 
					+ "," 
					+ "BankAccountMapping.aspx?GL_ID=" + GL_ID 
					+ "&FISCAL_ID=" + hidFiscalID.Value;
                //TabCtl.TabTitles = Cms.CmsWeb.ClsMessages("TabCtl");

			}

		}

		private void cmbFISCAL_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cmbFISCAL_ID.SelectedValue != null && cmbFISCAL_ID.SelectedValue.ToString()!="")
			{
				hidFiscalID.Value = cmbFISCAL_ID.SelectedValue.ToString();
				
			}
		}
	


		#endregion
	}
}
