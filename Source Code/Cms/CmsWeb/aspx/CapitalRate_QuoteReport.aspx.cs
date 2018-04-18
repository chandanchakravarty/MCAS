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
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using Microsoft.Xml.XQuery;
using Cms.BusinessLayer.BlQuote;
using Cms.BusinessLayer.BlCommon;

namespace Cms.CmsWeb.aspx
{
	/// <summary>
	/// Summary description for CapitalRate_QuoteReport.
	/// </summary>
	public class CapitalRate_QuoteReport :  Cms.CmsWeb.cmsbase
	{
		private const string LOB_PRIVATE_PASSENGER = "2";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			string strGuid = Request.QueryString["gID"].ToString().Trim();
			XPathNavigator nav;
			string lobID=LOB_PRIVATE_PASSENGER,finalQuoteXSLPath ="",premiumXml="";
			try
			{
				// fetch the premium from the ACORD_QUOTE_DETAILS on the basis of GUID
					DataSet dsPremiumXml = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT PREMIUM_XML  FROM ACORD_QUOTE_DETAILS WHERE INSURANCE_SVC_RQ="+ "'" + strGuid + "'");
					premiumXml = ""; // <-- pick here from table.
					if (dsPremiumXml.Tables[0].Rows[0]["PREMIUM_XML"].ToString().Trim()!="")
					{
						premiumXml = dsPremiumXml.Tables[0].Rows[0]["PREMIUM_XML"].ToString();
					}

					// fetch the AUTO final quote xsl path
					switch(lobID)
					{
					 
						case LOB_PRIVATE_PASSENGER:
							finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteAuto");//System.Configuration.ConfigurationSettings.AppSettings.Get("FinalQuoteAuto").ToString();
							
							break;			 
						default:
							break;
					
					}

 
					string cssnum="";
					cssnum="<HEADER" + " CSSNUM=\"" + GetColorScheme() + "\"";
					premiumXml=premiumXml.Replace("<HEADER",cssnum); 


					XmlDocument xmlDocInput = new XmlDocument();
					xmlDocInput.LoadXml(premiumXml);
					//Transform the Input XMl to generate the premium
					//XslTransform tr	= new XslTransform();				
                    XslCompiledTransform tr = new XslCompiledTransform();
					tr.Load(finalQuoteXSLPath);

					nav = ((IXPathNavigable) xmlDocInput).CreateNavigator();
					StringWriter swReport = new StringWriter();
					tr.Transform(nav,null,swReport);
					
					string strPremium = swReport.ToString();
					Response.Write(strPremium);
					Response.End();

			
			
			}
			catch(Exception exec)
			{throw(exec);}
			finally{}

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
