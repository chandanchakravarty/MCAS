/******************************************************************************************
	<Author					: Nidhi Sahay >
	<Start Date				: Nov 20,2006	>
	<End Date				: - >
	<Description			: Temporary file for Capital Rating Conversion approach>
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - > 
	<Modified By			: - >  
	<Purpose				: - >  

************************************************************************************/



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
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlApplication;
using System.IO;
//using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using Microsoft.Xml.XQuery;
using Cms.BusinessLayer.BlQuote;
using Cms.BusinessLayer.BlCommon;
using System.Reflection;
using System.Resources;

namespace Cms.CmsWeb.Maintenance
{

	enum LOBType
	{
		HO				=	1,
		AUTO			=	2,
		MOTERCYCLE		=	3,
		RENTALDEWLLING	=	4,
		WATERCRAFT		=	5
	}
	

	/// <summary>
	/// Summary description for CapitalRateComparison_Test.
	/// </summary>
	public class CapitalRateComparison_Test : Cms.CmsWeb.cmsbase
	{
	
		public string CapitalException
		{
			get
			{
				return lblMessage.Text;
			}
		}
	 
	 
		
		#region Form Variable

			
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capATTACH_FILE_NAME;
		protected System.Web.UI.WebControls.Label lblATTACH_FILE_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvATTACH_FILE_NAME;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnFileName;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtATTACH_FILE_NAME;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRootPath;	// Holds the type of entity, to which the file will attach
        protected System.Web.UI.WebControls.Label capMessages;

		 

		private const string LOB_HOME="1";
		private const string LOB_PRIVATE_PASSENGER="2";
		private const string LOB_MOTORCYCLE="3";
		private const string LOB_WATERCRAFT="4";
		private const string LOB_UMBRELLA="5";		
		private const string LOB_RENTAL_DWELLING="6";
        ResourceManager objResourceMgr = null;
		protected Cms.CmsWeb.Controls.CmsButton btnMakeApp;
		protected System.Web.UI.WebControls.Label capRUID;
		protected System.Web.UI.WebControls.DropDownList cmbRUID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRUID;
	 
		private string agencyID="0";
		protected Cms.CmsWeb.Controls.CmsButton btnShowQuote; //TO be changed later

		protected System.Web.UI.HtmlControls.HtmlForm frmCapitalInput;

		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			
			base.ScreenId	=	"0";

			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.CapitalRateComparison_Test" ,System.Reflection.Assembly.GetExecutingAssembly());
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            btnSave.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnMakeApp.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnMakeApp.PermissionString	=	gstrSecurityXML;

			btnShowQuote.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnShowQuote.PermissionString	=	gstrSecurityXML;
			btnShowQuote.Attributes.Add("onclick","javascript:ShowCapitalQuote();");
			if (!Page.IsPostBack)
			{
				rfvATTACH_FILE_NAME.Text = "Pls select the file";
				rfvATTACH_FILE_NAME.Enabled =false;
				PopulateCombos(agencyID);
				lblMessage.Visible	=	false;
                SetCaptions();
				//MAke app through Capital Rater link :
				if(Session["GUID"]!=null && Session["GUID"].ToString()!="")
				{
					MakeApplicationFromCapital(Session["GUID"].ToString(),"0");
				}

			}
			
			 
		}
		
		private void PopulateCombos(string agencyID)
		{
			DataSet dsTemp = ClsGenerateQuote.FetchDataFromAcordQuoteDetails(agencyID);
			cmbRUID.DataSource		= dsTemp;
			cmbRUID.DataTextField	= "INSURANCE_SVC_RQ";
			cmbRUID.DataValueField	= "ACORD_QUOTE_NUMBER";
			cmbRUID.DataBind();
			cmbRUID.Items.Insert(0,"");
			cmbRUID.Items[0].Selected=true; 
		
		}
		private void SetErrorMessages()
		{
			rfvATTACH_FILE_NAME.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"100");
		}
        private void SetCaptions()
        {
            capATTACH_FILE_NAME.Text = objResourceMgr.GetString("txtATTACH_FILE_NAME");
            capRUID.Text = objResourceMgr.GetString("cmbRUID");
           
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnMakeApp.Click += new System.EventHandler(this.btnMakeApp_Click);
			this.btnShowQuote.Click += new System.EventHandler(this.btnShowQuote_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{			
			string strAcordXML ="", AgencyId="0";
			try
			{				
				// Call a webService 
				webservices.AcordWebServices objAcordWebServices = new Cms.CmsWeb.webservices.AcordWebServices();
				
				string sFileName = "",strAcordOutputXML,outputsFileName ="",outputFileName="";					
				//sFileName=txtATTACH_FILE_NAME.PostedFile.FileName.ToString();				
				//<add key ="UploadURL"		value="/cms/Upload" />
				string sFileDir= "C:\\";	//For local
				//string sFileDir= Server.MapPath("../../Upload/CapitalRating/");	
				
				//if directory not exist on that location then this code will create new directory on map path location
				System.IO.Directory.CreateDirectory(sFileDir);

				//*************************************
				if (( txtATTACH_FILE_NAME.PostedFile != null) && (txtATTACH_FILE_NAME.PostedFile.ContentLength > 0))
				{
					//determine file name
					sFileName = System.IO.Path.GetFileName(txtATTACH_FILE_NAME.PostedFile.FileName);
					txtATTACH_FILE_NAME.PostedFile.SaveAs(sFileDir + sFileName);
					sFileName = sFileDir + sFileName;
					// Get the Acord XML from given path
					XmlDocument myXmlDocument = new XmlDocument();
					myXmlDocument.Load(sFileName);
					strAcordXML= myXmlDocument.OuterXml;
					XmlNode RqUIDXmlNode = myXmlDocument.SelectSingleNode("ACORD/InsuranceSvcRq/RqUID");                	
					string strRqUIDXmlNode =RqUIDXmlNode.InnerText;
//					objAcordWebServices.SaveAcordXML(strAcordXML,strRqUIDXmlNode);
//					//Save File on disk
					strAcordOutputXML = objAcordWebServices.ReturnAcordXMLPremium(strAcordXML,AgencyId);
					string[] verificationmessage = strAcordOutputXML.Split('^');

					if (verificationmessage.Length>1) // implies tht there is problem in verification layer
					{
						string verficationstring = verificationmessage[1].ToString();
						Session.Add("HtmlStringForXmlVer",verficationstring);
						string strPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetApplicationPath();
						Response.Write("<script> window.open('" + strPath + "/application/Aspx/ShowInputVerificationXml.aspx?') </script>");
					}
					else if(strAcordOutputXML==ClsCommon.ServiceAuthenticationMsg)
					{
						lblMessage.Text	=	ClsCommon.ServiceAuthenticationMsg;
						lblMessage.Visible	=	true;
					}
					else if(strAcordOutputXML.IndexOf("ACORD")<0)
					{
						lblMessage.Text	=	"Agency does not Exist for this Request.";
						lblMessage.Visible	=	true;
					}
					else
					{
					
						int intIndex	=	sFileName.LastIndexOf("\\");
						outputFileName =	sFileName.Substring(intIndex+1);	//Taking only file name not whole 
						outputsFileName = sFileName.Substring(0,intIndex);
						outputFileName=outputFileName.Replace(".xml","_Output.xml");
						outputsFileName = outputsFileName +"\\"+ outputFileName;

						XmlDocument docAcordInput = new XmlDocument();
						docAcordInput.LoadXml(strAcordOutputXML);
						FileStream fsxml = new FileStream(outputsFileName,FileMode.OpenOrCreate,FileAccess.Write,FileShare.ReadWrite);
						docAcordInput.Save(fsxml);
						fsxml.Close();
						lblMessage.Text	=	"Updated File saved in ('"+ outputsFileName +"')";
						lblMessage.Visible	=	true;
						
						//populate the combo
						PopulateCombos(agencyID);

					}
					
				}
				else
				{
					lblMessage.Text	=	"Please select File Name.";
					lblMessage.Visible	=	true;
				}
			}
			
			catch(Exception ex)
			{
			
				lblMessage.Text	=	ex.Message;
				lblMessage.Visible	=	true;
			}		
			finally
			{}
		}

		private void btnMakeApp_Click(object sender, System.EventArgs e)
		{
			//making web services
			
			try
			{
				#region  temporary setting - TO BE REMOVED LATER.
				/*
				string acordModifiedXML="",acordModifiedFilePath="", GUID="0", agencyID="0", lobID=LOB_PRIVATE_PASSENGER;
				XmlDocument docModifiedFile = new XmlDocument();
				//pick the selected file
				if (( txtATTACH_FILE_NAME.PostedFile != null) && (txtATTACH_FILE_NAME.PostedFile.ContentLength > 0))
				{
					//determine file name
					acordModifiedFilePath  = txtATTACH_FILE_NAME.Value.ToString();
					if (acordModifiedFilePath.Trim() != "")
					{
						docModifiedFile.Load(acordModifiedFilePath);
						//load the file and get the acord xml string
						acordModifiedXML = docModifiedFile.OuterXml;

						//fetch the GUID from the acordXML
						XmlNode nodTemp  = docModifiedFile.SelectSingleNode("ACORD/InsuranceSvcRq/RqUID");
						if (nodTemp != null)
						{
							GUID = nodTemp.InnerText;
						}						 
					}
				}
				*/
				#endregion

				// Call a webService 
				webservices.AcordWebServices objAcordWebServices = new Cms.CmsWeb.webservices.AcordWebServices();
				
				string GUID="0",agencyID="0",lobID=LOB_PRIVATE_PASSENGER;
				if(cmbRUID.SelectedItem!=null)
				GUID=cmbRUID.SelectedItem.Text.ToString().Trim();
			
				if (GUID.Trim() =="")
				{
					lblMessage.Text	=	"Please select the service request id. <br>In case there are no request Ids then please select file and generate quote first.";
					lblMessage.Visible	=	true;
				}
				else
				{
					
					//fetch the lobcd from the acord and create the object accordingly				
					if (lobID==LOB_PRIVATE_PASSENGER)
					{
						if(!ApplicationExists(GUID))
						{
							objAcordWebServices.AuthenticationTokenHeader = new AuthenticationToken(); 
							objAcordWebServices.AuthenticationTokenHeader.TokenValue  = System.Configuration.ConfigurationManager.AppSettings.Get("CapitalAuthenticationToken");
							string ReturnMakeAppNumber = objAcordWebServices.ReturnMakeAppNumber(GUID,agencyID);
							if(ReturnMakeAppNumber != "noAgency")
							{
								string[] strNumber = ReturnMakeAppNumber.Split('/');
								string appNumber=strNumber[0];
								string appID=strNumber[1];
								string appVersionID=strNumber[2];
								string customerID=strNumber[3];
								Response.Redirect("/cms/Application/aspx/ApplicationTab.aspx?customer_id= " + customerID + " &app_id="+ appID +" &app_version_id= " + appVersionID + "");
							}
							else
							{
								lblMessage.Text="Agency does not Exist for this Request.";
								lblMessage.Visible= true;
							}
						}
						else
						{
							lblMessage.Text="Application Exists for this GUID.";
							lblMessage.Visible= true;

						}

						
						
					}
					else
					{
						lblMessage.Text="We are working on this LOB";
						lblMessage.Visible= true;
					}
				}	
			

				
			}
			catch(Exception ex)
			{
				lblMessage.Text=ex.ToString();
				lblMessage.Visible= true;							
			}
			
		}
		#region AGENCY LOGIN MAKE APP : 19 July 2007
		public void MakeApplicationFromCapital(string Uid,string agencyId)
		{
			string GUID="0",agencyID="0",lobID=LOB_PRIVATE_PASSENGER;
			GUID=Uid;
			string appNumber="",appID="",appVersionID="",customerID="",appStatus="";		
			// Call a webService 
			try
			{
				webservices.AcordWebServices objAcordWebServices = new Cms.CmsWeb.webservices.AcordWebServices();
				if (lobID==LOB_PRIVATE_PASSENGER)
				{		
					#region Check Existing Application
					ClsAcord ObjAcord = new ClsAcord(CarrierSystemID);
					string appData  = ObjAcord.CheckApplicationExists(GUID,"CAPITAL");
					if(appData!="1")
					{
						string[] strID = appData.Split('/');
						customerID = strID[0];
						appID = strID[1];
						appVersionID = strID[2];
						appStatus = strID[3];
					}
			
					if(appStatus == "EXISTS")
					{
						Response.Redirect("/cms/Application/aspx/ApplicationTab.aspx?customer_id= " + customerID + " &app_id="+ appID +" &app_version_id= " + appVersionID + "");
					}
						#endregion
					else
					{
						string ReturnMakeAppNumber = objAcordWebServices.ReturnMakeAppNumber(GUID,agencyID);
						string[] strNumber = ReturnMakeAppNumber.Split('/');
						appNumber = strNumber[0];
						appID = strNumber[1];
						appVersionID = strNumber[2];
						customerID = strNumber[3];
						//Clear Capital Login Sessions:
						Session["GUID"] ="";
						Session["SYSTEM_ID"]="";
						Session["USER_ID"]="";

						Response.Redirect("/cms/Application/aspx/ApplicationTab.aspx?customer_id= " + customerID + " &app_id="+ appID +" &app_version_id= " + appVersionID + "");
						
					}
				
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	"Error while making Application, Error Description  " + ex.Message;
				lblMessage.Visible	=	true;
				Server.Transfer("/cms/cmsweb/aspx/CapitalRaterLogin.aspx");
			}

		}
		#endregion
		#region CHECK APPLICATION
		public bool ApplicationExists(string GUID)
		{
			try
			{
				if(GUID!="")
				{
					ClsAcord ObjAcord = new ClsAcord(CarrierSystemID);
					string appStatus  = ObjAcord.CheckApplicationExists(GUID,"APP");
					if(appStatus == "2")
						return(true);
					else
						return(false);

				}
				

			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
			return(true);

		}
		#endregion

		private void btnShowQuote_Click(object sender, System.EventArgs e)
		{
			try
			{
				//XPathNavigator nav;
				string GUID="0";//,agencyID="0",lobID=LOB_PRIVATE_PASSENGER,finalQuoteXSLPath ="",premiumXml="";
				if(cmbRUID.SelectedItem!=null)
					GUID=cmbRUID.SelectedItem.Text.ToString().Trim();
			
				if (GUID.Trim() =="")
				{
					lblMessage.Text	=	"Pls select the service request id. <br>In case there are no request Ids then please select file and generate quote first.";
					lblMessage.Visible	=	true;
				}
							
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}	

	}
}
