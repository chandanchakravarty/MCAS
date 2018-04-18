	/******************************************************************************************
	<Author					: -   Ajit Singh Chahal
	<Start Date				: -   16 jun2, 2005 11:31:31 AM
	<End Date				: -	
	<Description			: -   To Print chart of accounts.
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
using Cms.Model.Maintenance.Accounting;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlCommon;
namespace Cms.CmsWeb.Accounting
{
	/// <summary>
	/// Summary description for ChartOfAccountsPrint.
	/// </summary>
	public class ChartOfAccountsPrint :  Cms.CmsWeb.cmsbase
	{

		protected System.Web.UI.WebControls.Literal litReport;
        protected System.Web.UI.HtmlControls.HtmlInputButton print;
        protected System.Web.UI.HtmlControls.HtmlInputButton close;
        protected System.Web.UI.HtmlControls.HtmlInputButton print2;
        protected System.Web.UI.HtmlControls.HtmlInputButton close2;
        protected ResourceManager objResourceMgr; 
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId	="125_0_0";
            objResourceMgr = new System.Resources.ResourceManager("cms.cmsweb.Maintenance.SystemParams", System.Reflection.Assembly.GetExecutingAssembly());
			//ClsGlAccounts.GetChartOfAccounts().WriteXml("c:\\ajit.xml");
			string xml =   ClsGlAccounts.GetChartOfAccounts().GetXml();
			string time = DateTime.Now.TimeOfDay.ToString();
			string dat = DateTime.Now.ToString();
			dat = dat.Substring(0,dat.IndexOf(' '));
			time = time.Substring(0,time.IndexOf('.'));
			xml = xml.Replace("<NewDataSet>","<NewDataSet><Date>" + dat + "</Date>" + "<Time>" +  time + "</Time>");
			XmlDocument xmlDocInput = new XmlDocument();
			xmlDocInput.LoadXml(xml); 
            //XslTransform tr = new XslTransform();
            XslCompiledTransform tr = new XslCompiledTransform();
            {
                if (GetLanguageID() == "2")
                {
                    tr.Load(Server.MapPath("/cms/Account/Support/ChartOfAccounts.pt-BR.xsl"));
                }
                else
                {
                    tr.Load(Server.MapPath("/cms/Account/Support/ChartOfAccounts.xsl"));
                }
            }
			XPathNavigator nav = ((IXPathNavigable) xmlDocInput).CreateNavigator();
			StringWriter sw1 = new StringWriter();
			tr.Transform(nav,null,sw1);
			litReport.Text =  sw1.ToString();
            SetCaptions();
			sw1.Close();
		}
       
		#region Web Form Designer generated code
        private void SetCaptions()
        {
            print.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1505");
            close.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1506");
            print2.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1505");
            close2.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1506");
        }
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}

    
}
