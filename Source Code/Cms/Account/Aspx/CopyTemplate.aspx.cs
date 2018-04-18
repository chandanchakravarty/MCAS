#region Namespaces
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
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlAccount;
using Cms.CmsWeb;
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.Controls;
using System.Xml;
#endregion

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for CopyTemplate.
	/// </summary>
	public class CopyTemplate : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.HtmlControls.HtmlForm Form1;

		protected System.Web.UI.WebControls.Label lblHeader;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCommisionType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFor;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTitle;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidJournal_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnSubmit;
		protected Cms.CmsWeb.Controls.CmsButton btnClose;
		protected Cms.CmsWeb.Controls.CmsButton btnSavClos;

		protected System.Web.UI.WebControls.DataGrid dgrTemplate;

		protected int gIntSaved=0;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFillgrid;	
		public bool anyRecordSelected=false;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId	=	"";

			btnSubmit.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSubmit.PermissionString = gstrSecurityXML;

			btnSavClos.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSavClos.PermissionString = gstrSecurityXML;

			btnClose.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
			btnClose.PermissionString = gstrSecurityXML;

			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";

			switch (int.Parse(colorScheme))
			{
				case 1:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
					break;
				case 2:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();     
					break;
				case 3:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR3").ToString();     
					break;
				case 4:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR4").ToString();     
					break;
			}

			if(colors!="")
			{
				string [] baseColor=colors.Split(new char []{','});  
				if(baseColor.Length>0)
					colors= "#" + baseColor[0];
			}
			#endregion 

			if(!Page.IsPostBack)
			{
				if(Request.QueryString["CalledFrom"]!=null && Request.QueryString["CalledFrom"].ToString().Length>0)
				{
					hidCalledFrom.Value = Request.QueryString["CalledFrom"].ToString();
				}
				btnClose.Attributes.Add("onClick","javascript:CloseCopy();");
				//btnSubmit.Attributes.Add("onClick","javascript:SubmitCopy();");
				BindGrid();	
			
			}
		}
		XmlDocument GetDataGridNodes(DataGrid dgRecords,XmlDocument xmlDoc)
		{

			CheckBox chkBox;
			foreach(DataGridItem dgi in dgRecords.Items)
			{
				chkBox=(CheckBox)dgi.FindControl("chkSelect");
				if (chkBox != null && chkBox.Checked)
				{
					anyRecordSelected = true;//atleast one record is chosen
					
					XmlElement parentNode = null;
					parentNode  = xmlDoc.CreateElement("Template");
					parentNode.SetAttribute("ID", dgi.ItemIndex.ToString());
					XmlNode xNode = xmlDoc.CreateElement("CREATED_BY");
					xNode.InnerText = GetUserId();
					parentNode.AppendChild(xNode);

					xNode = xmlDoc.CreateElement(((BoundColumn)dgRecords.Columns[6]).DataField);						
					xNode.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecodeXMLCharacters(dgi.Cells[6].Text.Trim());
					parentNode.AppendChild(xNode);				
					
					xNode = xmlDoc.CreateElement(((BoundColumn)dgRecords.Columns[1]).DataField);						
					xNode.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecodeXMLCharacters(dgi.Cells[1].Text.Trim());
					parentNode.AppendChild(xNode);
					
					//					for (int i=1 ; i<dgi.Cells.Count ; i++)
					//					{
					//						xNode = xmlDoc.CreateElement(((BoundColumn)dgRecords.Columns[i]).DataField);						
					//						xNode.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecodeXMLCharacters(dgi.Cells[i].Text.Trim());
					//						parentNode.AppendChild(xNode);
					//					}
					xmlDoc.DocumentElement.PrependChild(parentNode);
				}
			}
			return xmlDoc;

		}

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				XmlDocument xmlDoc = new XmlDocument();			
				XmlElement rootNode = null;
				rootNode = xmlDoc.CreateElement(Cms.BusinessLayer.BlAccount.ClsJournalEntryMaster.TEMPLATE_PARENT_NODE);
				//				rootNode.SetAttribute("CustomerID",hidCustomerID.Value);
				//				rootNode.SetAttribute("AppID",hidAppID.Value);
				//				rootNode.SetAttribute("AppVersionID",hidAppVersionID.Value);
				xmlDoc.AppendChild(rootNode);
				xmlDoc = GetDataGridNodes(dgrTemplate,xmlDoc);			
				
				if  (xmlDoc.ChildNodes.Item(0).ChildNodes.Count==0)      
				{
					lblMessage.Text="Please Select atleast one record to copy.";							
				}
				int intRetVal = -1;	
				Cms.BusinessLayer.BlAccount.ClsJournalEntryMaster objJournalEntryMaster = new Cms.BusinessLayer.BlAccount.ClsJournalEntryMaster();			
				intRetVal = objJournalEntryMaster.copyTemplate(xmlDoc,Convert.ToInt32(GetUserId()));
				
				//Added for Itrack # 3915 on 24-March-2008
				if  (xmlDoc.ChildNodes.Item(0).ChildNodes.Count>0)      
				{
					if(intRetVal>0)
					{
						lblMessage.Text=ClsMessages.FetchGeneralMessage("859");
						lblMessage.Visible=true;
						//btnSubmit.Visible=false;
						//dgrTemplate.Visible=false;
						btnClose.Visible=true;
						hidFormSaved.Value = "1";
						string strScript="";

						strScript = @"<script language='javascript'>" + 
							"window.opener.parent.RefreshWebgrid('');" +
							"</script>" 
							;
						//Copy template require Max JE ID.
						hidJournal_ID.Value = ClsJournalEntryMaster.GetMaxJID().ToString();

						if (!ClientScript.IsStartupScriptRegistered("Refresh"))
						{
							ClientScript.RegisterStartupScript(this.GetType(),"Refresh",strScript);
						}

						strScript = @"<script language='javascript'>" + 
							"SubmitCopy();" +
							"</script>" 
							;
				
						if (!ClientScript.IsStartupScriptRegistered(""))
						{
							ClientScript.RegisterStartupScript(this.GetType(),"",strScript);
						}

					}
				}
				else if(intRetVal==-1 || xmlDoc.ChildNodes.Item(0).ChildNodes.Count==0)
				{
					hidFormSaved.Value = "0";
					lblMessage.Text=ClsMessages.FetchGeneralMessage("858");
				}
				else
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");					
					hidFormSaved.Value		=	"2";
				}
				lblMessage.Visible = true;

			}
			catch(Exception ex)
			{
				gIntSaved=0;
				lblMessage.Text=ex.Message;
				lblMessage.Visible=true;			
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			
		}

		private void btnSavClos_Click(object sender, System.EventArgs e)
		{
			btnSubmit_Click(null,null);
			ClientScript.RegisterStartupScript(this.GetType(),"CloseWin","<script>javascript:window.opener.location=window.opener.location;window.close();</script>");
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
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnSavClos.Click += new System.EventHandler(this.btnSavClos_Click);
			

		}
		#endregion


		private void BindGrid()
		{
			try
			{
					DataTable dtTemplate = new DataTable();
				lblHeader.Text = "List of Journal Entries - Template";
									
				dtTemplate = Cms.BusinessLayer.BlAccount.ClsJournalEntryMaster.GetTemplate();
				if(dtTemplate.Rows.Count>0)
				{
					btnSubmit.Enabled=true;
				}
				else
				{	
					btnSubmit.Enabled=false;
				}					
				dgrTemplate.DataSource=dtTemplate.DefaultView;
				dgrTemplate.DataBind();
				dgrTemplate.Visible = true;
			}
			
			catch(Exception ex)
			{
				lblMessage.Text=ex.Message;
				lblMessage.Visible=true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}
		
	}
}