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
using System.Xml;

namespace Cms.CmsWeb.DocumentMerge
{
	/// <summary>
	/// Summary description for TemplateLoad.
	/// </summary>
	public class TemplateLoad : cmsbase
	{
		protected Cms.CmsWeb.WebControls.Menu bottomMenu;
		protected string strTreeVersion = "-";
		protected string strUserDefineVersion = "-";
		protected string strMode = "";
		protected string strId = "";
		protected string strUploadDownloadURL = "";
		protected string strUserId = "";
		protected string strDocumentMergeVersion;

		private void Page_Load(object sender, System.EventArgs e)
		{
			strMode = Request.QueryString["Mode"].ToString();
			
			if (strMode.ToUpper() == "EDIT")
			{
				strId = Request.QueryString["TemplateId"].ToString();
			
				XmlDocument objXmlTree = new XmlDocument();
				objXmlTree.Load(Server.MapPath("") + "\\W$18T.xml");
				strTreeVersion = GetAttributeValue(objXmlTree.SelectSingleNode("//brics"),"VERSION");
				objXmlTree.RemoveAll();
				objXmlTree = null;
				//We Need to write code here for user defined tree version.
				//Mean while hard coded version is used.
				strUserDefineVersion = "1.0.0.0";
			}
			else if (strMode.ToUpper() == "MERGE")
			{
				strId = Request.QueryString["MergeId"].ToString();
				base.ScreenId ="350";
				bottomMenu.Visible = true;
			}
			else if (strMode.ToUpper() == "TRANS")
				strId = Request.QueryString["TransId"].ToString();

			strUploadDownloadURL = System.Configuration.ConfigurationManager.AppSettings.Get("CmsWebUrl");
			strUserId = GetUserId();

            strDocumentMergeVersion = System.Configuration.ConfigurationManager.AppSettings.Get("DOCMERGEVERSION");
			
		}
		private string GetAttributeValue(XmlNode Node, string AttributeName)
		{
			for (int AttributeCounter = 0; AttributeCounter < Node.Attributes.Count;AttributeCounter++)
			{
				XmlAttribute XmlAttrib= Node.Attributes[AttributeCounter];
				if (XmlAttrib.Name.ToUpper() == AttributeName.ToUpper())
				{	
					return XmlAttrib.Value;
				}
			}
			return "";
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
