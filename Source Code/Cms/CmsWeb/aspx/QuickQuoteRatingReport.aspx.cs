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
using Cms.CmsWeb;
using Cms.BusinessLayer.BlCommon;
using System.IO;
using Microsoft.Xml;
using Microsoft.Xml.XQuery;
using System.Xml.XPath;
using System.Xml;
using System.Xml.Xsl;

namespace Cms.CmsWeb.aspx
{
	/// <summary>
	/// Summary description for QuickQuoteRatingReport.
	/// </summary>
	public class QuickQuoteRatingReport: cmsbase
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				string strQuoteId = Request.QueryString["QuoteId"].ToString();
				string strClientId = Request.QueryString["ClientId"].ToString();
				string strLobCode = Request.QueryString["Lob"].ToString();

				string RatingReport = new ClsQuickQuote().GetQuickQuoteRatingReport(strClientId,strQuoteId);
				if(strLobCode!="AUTOP" && strLobCode!="CYCL")
				RatingReport = RatingReport.Replace("AMP;","");  
				//RatingReport = RatingReport.Replace("H673GSUYD7G3J73UDH","'");
				string cssnum="";
				cssnum="<HEADER" + " CSSNUM=\"" + GetColorScheme() + "\"";
				RatingReport=RatingReport.Replace("<HEADER",cssnum); 
				RatingReport = RatingReport.Replace("APOS;","apos;");
				if(strLobCode!="AUTOP" && strLobCode!="CYCL")
                    RatingReport = RatingReport.Replace("&amp;","&");
				else
                    RatingReport = RatingReport.Replace("&","&amp;");

				//Load the InputXML
				XmlDocument xmlDocRatingReport = new XmlDocument();
				xmlDocRatingReport.LoadXml(RatingReport);  
			
				string	finalQuoteXSLPath = "";
			
				if (strLobCode == "HOME")
					finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteHO3");//System.Configuration.ConfigurationSettings.AppSettings.Get("FinalQuoteHO3").ToString();
				else if (strLobCode == "AUTOP")
					finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteAuto");//System.Configuration.ConfigurationSettings.AppSettings.Get("FinalQuoteAuto").ToString();
				else if (strLobCode == "REDW")
					finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteREDW");//System.Configuration.ConfigurationSettings.AppSettings.Get("FinalQuoteAuto").ToString();
				else if (strLobCode == "BOAT")
					finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteBOAT");//System.Configuration.ConfigurationSettings.AppSettings.Get("FinalQuoteAuto").ToString();
				else if (strLobCode == "CYCL")
					finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteCYCL");//System.Configuration.ConfigurationSettings.AppSettings.Get("FinalQuoteAuto").ToString();

				//Transform the Input XMl to generate the premium
				//XslTransform tr	= new XslTransform();				
                XslCompiledTransform tr = new XslCompiledTransform();
				tr.Load(finalQuoteXSLPath);
				XPathNavigator nav = ((IXPathNavigable) xmlDocRatingReport).CreateNavigator();
				StringWriter swRatingReport = new StringWriter();
				tr.Transform(nav,null,swRatingReport);

				string finalQuote = swRatingReport.ToString().Replace("H673GSUYD7G3J73UDH","'");
				finalQuote = finalQuote.Replace("D673GSUYD7G3J73UDD","\"");
				Response.Write(finalQuote);
				
				//Response.Write(swRatingReport);
				Response.End();
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
