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
using Cms.ExceptionPublisher;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using System.Globalization;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlQuote;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Summary description for PolicyShowDiscountAndSurcharges.
	/// </summary>
	public class PolicyShowDiscountAndSurcharges : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label FinalLAbel;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlTableRow errorLabel;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			GetDiscountAndSurcharge();
//		
				if(GetLOBString().ToUpper()=="WAT")
					base.ScreenId = "250_1";
				SetWorkflow();
		}
//
		private void SetWorkflow()
		{
			if(base.ScreenId == "71_1" || base.ScreenId == "250_1")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());

				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
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

		private void GetDiscountAndSurcharge()
		{
			string strInputXML="",strlobID="";
			int customerId,policyID,policyversionID;
			
			try 
			{
				string strCSSNo = GetColorScheme();
			
				
				customerId=	int.Parse(GetCustomerID().ToString());
				policyID     =	int.Parse(GetPolicyID());
				policyversionID=	int.Parse(GetPolicyVersionID());

				ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();	
				//Get The LOB for the application
				strlobID = GetLOBID();
				ClsGenerateQuote objQ = new ClsGenerateQuote(CarrierSystemID);
				
				#region		GET INPUT XML 
				strInputXML="";
				//Get the input xml. For the aplication
				//Get The LOB From Query String
				string strLOB;
				if(Request.QueryString["CalledFrom"]!=null)
				{
					strLOB=Request.QueryString["CalledFrom"];
				}
				else
				{
					strLOB ="";
				}

				if(strLOB=="HWAT" && strlobID=="1")
				{
					strlobID="4";
				}
				strInputXML		= objGeneralInfo.GetPolicyInputXML(customerId,policyID,policyversionID,strlobID);
				//Encode Xml Charecters
				strInputXML = ClsCommon.EncodeXMLCharacters(strInputXML);
				XmlDocument docTest =new XmlDocument();
				strInputXML = ClsCommon.DecodeXMLCharacters(strInputXML); 
				strInputXML=strInputXML.ToUpper();
				
				if (strlobID == "3" )
				{
					strInputXML=strInputXML.Replace("<?XML VERSION=\"1.0\" ENCODING=\"UTF-8\"?>","");
					strInputXML = strInputXML.Remove(0,strInputXML.IndexOf("<QUICKQUOTE>",0));
				}
				
				strInputXML=strInputXML.Replace("&AMP;","&amp;");
				strInputXML = strInputXML.Replace("</QUICKQUOTE>","<CSSNUM CSSVALUE =' " + strCSSNo +  " '/></QUICKQUOTE>");
				strInputXML=strInputXML.Replace("\n","");
				strInputXML=strInputXML.Replace("\r","");
				strInputXML=strInputXML.Replace("\t","");
			
				XmlDocument inputXmlDoc =new XmlDocument();
				#endregion
				
				#region CHECK THE INPUT XML FOR BLANK NODES
				//Check For Existance Of Xml If No Data Found//
				XmlNode node;
				inputXmlDoc.LoadXml(strInputXML);
				
				node=inputXmlDoc.SelectSingleNode("ERROR");
				
				//If Information Not found or There is error In Xml
				if(node!=null)
				{
					errorLabel.Attributes.Add("style","display:inline");
					lblMessage.Text =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("727");
					return;

				}
				errorLabel.Attributes.Add("style","display:none");
			#endregion
				#region GET THE PATH OF FACTOR MASTER AND DISCOUNT XSL

				//Get The State Name
				string strStateName="";
				if (strlobID == "2" ||  strlobID == "3" || strlobID == "4")
				{
					strStateName=inputXmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/STATENAME").InnerText.ToString();
				}
				if (strlobID == "6" || strlobID == "1")
				{
					XmlNode nodState=inputXmlDoc.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/STATENAME");
					if(nodState==null)
					{
						errorLabel.Attributes.Add("style","display:inline");
						lblMessage.Text =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("727");
						return;

					}
					strStateName=inputXmlDoc.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/STATENAME").InnerText.ToString();
					strInputXML = strInputXML.Replace("</DWELLINGDETAILS>","<CSSNUM CSSVALUE =' " + strCSSNo +  " '/></DWELLINGDETAILS>");
					
				}

				string xslFilePath="" ;     // this variable stores path of the xsl
				string strReplacedXSLPath="";
				XmlNodeList factorNode; 
				string strNodeXML;
				switch(int.Parse(strlobID))
				{
					case 1:         //LOB_HOME:
						xslFilePath = ClsCommon.GetKeyValueWithIP("DisountAndSurcharge_HO");
						factorNode = inputXmlDoc.SelectNodes("QUICKQUOTE/DWELLINGDETAILS");
						strNodeXML=factorNode.Item(0).OuterXml;
						strReplacedXSLPath = objQ.GetProductFactorMasterPath(strNodeXML,"HO");
						break;
					case 2:         //LOB_PRIVATE_PASSENGER
						xslFilePath = ClsCommon.GetKeyValueWithIP("DisountAndSurcharge_Auto");
						strInputXML=strInputXML.Replace("<CALLEDFROM>CALLED</CALLEDFROM>","<CALLEDFROM>DISCOUNTSP</CALLEDFROM>");
						string strReplacedXSLPath_P = objQ.GetProductFactorMasterPath(strInputXML,"AUTOP");
						strInputXML=strInputXML.Replace("<CALLEDFROM>DISCOUNTSP</CALLEDFROM>","<CALLEDFROM>DISCOUNTSC</CALLEDFROM>");
						string strReplacedXSLPath_C = objQ.GetProductFactorMasterPath(strInputXML,"AUTOC");

						strReplacedXSLPath=strReplacedXSLPath_P + "^" + strReplacedXSLPath_C;
						break;
					case 3:          //LOB_MOTORCYCLE
						xslFilePath = ClsCommon.GetKeyValueWithIP("DisountAndSurcharge_Cycle");
						strReplacedXSLPath = objQ.GetProductFactorMasterPath(strInputXML,"CYCL");
						break;
					case 4:         //LOB_WATERCRAFT:
						xslFilePath = ClsCommon.GetKeyValueWithIP("DisountAndSurcharge_WaterCraft");
						strReplacedXSLPath = objQ.GetProductFactorMasterPath(strInputXML,"BOAT");
						break;
					case 5:         //LOB_UMBRELLA
						strReplacedXSLPath="";
						break;
					case 6:          //LOB_RENTAL_DWELLING
						xslFilePath =  ClsCommon.GetKeyValueWithIP("DisountAndSurcharge_RentalDwelling");
						factorNode  =  inputXmlDoc.SelectNodes("QUICKQUOTE/DWELLINGDETAILS");
						strNodeXML  =  factorNode.Item(0).OuterXml;
						strReplacedXSLPath = objQ.GetProductFactorMasterPath(strNodeXML,"REDW");
						break;
					case 7:          //General Liability
						strReplacedXSLPath="";
						break;

				}
				#endregion

				#region  TRANSFORM THE INPUT WITH THE DISCOUNT XSL 
				XmlDocument doc =new XmlDocument();
				doc.Load(xslFilePath);
				string strModifiedPrmXsl = doc.InnerXml;
				//In case Of Auto Split The Personal Vehicle and Commercial Vehicle path
				if(strlobID == "2")
				{
					string[] strFilePath =strReplacedXSLPath.Split('^');
					strFilePath[0].Replace("\r","");
					strFilePath[0].Replace("\t","");
					strFilePath[0].Replace("\n","");
					strFilePath[1].Replace("\r","");
					strFilePath[1].Replace("\t","");
					strFilePath[1].Replace("\n","");
					string strPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetApplicationPath();
					strPath=strPath+strFilePath[1];
					strModifiedPrmXsl = strModifiedPrmXsl.Replace("FactorPathPer", strFilePath[0]);
					strModifiedPrmXsl = strModifiedPrmXsl.Replace("FactorPathCom", strPath);

				}
				else
				{
					strModifiedPrmXsl = strModifiedPrmXsl.Replace("FactorPath", strReplacedXSLPath);
				}
				//XslTransform xslt = new XslTransform();
                XslCompiledTransform xslt = new XslCompiledTransform();
				xslt.Load( new XmlTextReader( new StringReader(strModifiedPrmXsl)));
				StringWriter writer = new StringWriter();
				XmlDocument xmlDocTemp = new XmlDocument();
				xmlDocTemp.LoadXml(strInputXML); 
				XPathNavigator nav = ((IXPathNavigable) xmlDocTemp).CreateNavigator();
				xslt.Transform(nav,null,writer);   
				string returnString ="";
				returnString= writer.ToString();
               
				//Show The Final Ouput
				FinalLAbel.Text=returnString;
															
				#endregion
			}
			 
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{}	
		 
		}

	}
}
