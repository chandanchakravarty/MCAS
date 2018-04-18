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
using Cms.CmsWeb;
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.Controls;
using System.Xml;
#endregion

namespace Cms.CmsWeb.Accounting
{
	/// <summary>
	/// Summary description for CopyCommission.
	/// </summary>
	public class CopyCommission : Cms.CmsWeb.cmsbase
	{
		#region Declarations
		protected System.Web.UI.WebControls.Label lblHeader;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCommisionType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFor;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTitle;
		protected Cms.CmsWeb.Controls.CmsButton btnSubmit;
		protected Cms.CmsWeb.Controls.CmsButton btnClose;		
		protected int gIntSaved=0;				
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.DataGrid dgrRegularCommission;
		protected System.Web.UI.WebControls.DataGrid dgrAdditionalCommission;
		protected System.Web.UI.WebControls.DataGrid dgrPropertyInspection;
		protected System.Web.UI.WebControls.DataGrid dgrCompleteApp;
			
		public string strCalledFrom="";
		#endregion
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFillgrid;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBxml;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidsubLOBxml;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidClassxml;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateID;
		public bool anyRecordSelected=false;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId	=	"";

			btnSubmit.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSubmit.PermissionString = gstrSecurityXML;
			//btnSubmit.Attributes.Add("onclick","javascript:fillOpener();");
			GetQueryStringValues();
			if(!Page.IsPostBack)
			{
				BindGrid();	
				// Fill XML with LOBs
				DataTable dtTmp = new DataTable();
				dtTmp = Cms.CmsWeb.ClsSingleton.LOBs;
				DataSet dsTmp = new DataSet();
				dsTmp.Tables.Add(dtTmp.Copy());
				hidLOBxml.Value = dsTmp.GetXml();
				//Fill XML with Sub-LOBs
				hidsubLOBxml.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXmlForLobByState();
				//Fill XML with class
				ClsGeneralInformation objGen = new ClsGeneralInformation();
				//				hidClassxml.Value = objGen.GetAllClassOnStateId(0);
			}
			
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
		}
		#endregion 
			
		private void BindGrid()
		{
			try
			{	
				switch(hidCommisionType.Value)
				{
						
					case "R":
						DataTable dtRegularCommission = new DataTable();
						lblHeader.Text = "List of Regular Commission Setup - Agency";
									
						dtRegularCommission = Cms.BusinessLayer.BlCommon.Accounting.ClsRegCommSetup_Agency.GetCommission(hidCommisionType.Value);
						if(dtRegularCommission.Rows.Count>0)
						{
							btnSubmit.Enabled=true;
						}
						else
						{	
							btnSubmit.Enabled=false;
						}					
						dgrRegularCommission.DataSource=dtRegularCommission.DefaultView;
						dgrRegularCommission.DataBind();
						dgrRegularCommission.Visible = true;
						break;
					case "A" :
						DataTable dtAdditionalCommission = new DataTable();
						lblHeader.Text = "List of Additional Commission Setup - Agency";
									
						dtAdditionalCommission = Cms.BusinessLayer.BlCommon.Accounting.ClsRegCommSetup_Agency.GetCommission(hidCommisionType.Value);
						if(dtAdditionalCommission.Rows.Count>0)
						{
							btnSubmit.Enabled=true;
						}
						else
						{	
							btnSubmit.Enabled=false;
						}					
						dgrAdditionalCommission.DataSource=dtAdditionalCommission.DefaultView;
						dgrAdditionalCommission.DataBind();
						dgrAdditionalCommission.Visible = true;
						break;
					case "P" :
						DataTable dtPropertyInspection = new DataTable();
						lblHeader.Text = "List of Additional Commission Setup - Agency";
									
						dtPropertyInspection = Cms.BusinessLayer.BlCommon.Accounting.ClsRegCommSetup_Agency.GetCommission(hidCommisionType.Value);
						if(dtPropertyInspection.Rows.Count>0)
						{
							btnSubmit.Enabled=true;
						}
						else
						{	
							btnSubmit.Enabled=false;
						}					
						dgrPropertyInspection.DataSource=dtPropertyInspection.DefaultView;
						dgrPropertyInspection.DataBind();
						dgrPropertyInspection.Visible = true;
						break;
					case "C" :
						DataTable dtCompleteApp = new DataTable();
						lblHeader.Text = "List of Complete App Bonus";
									
						dtCompleteApp = Cms.BusinessLayer.BlCommon.Accounting.ClsRegCommSetup_Agency.GetCommission(hidCommisionType.Value);
						if(dtCompleteApp.Rows.Count>0)
						{
							btnSubmit.Enabled=true;
						}
						else
						{	
							btnSubmit.Enabled=false;
						}					
						dgrCompleteApp.DataSource=dtCompleteApp.DefaultView;
						dgrCompleteApp.DataBind();
						dgrCompleteApp.Visible = true;
						break;
					default:
						return;
				}
						
				
			}
			catch(Exception ex)
			{
				lblMessage.Text=ex.Message;
				lblMessage.Visible=true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}
		
		#region GetDataGridNodes
		string GetDataGridNodes(DataGrid dgRecords,string strReturn)
		{

			CheckBox chkBox;
			//XmlNode xNode;
			//XmlElement parentNode = null;
			string strfillGrid = null;
			//string strStateName = "";
			foreach(DataGridItem dgi in dgRecords.Items)
			{
				chkBox=(CheckBox)dgi.FindControl("chkSelect");
				if (chkBox != null && chkBox.Checked)
				{
					anyRecordSelected = true;//atleast one record is chosen
								
					for (int i=1 ; i<dgi.Cells.Count ; i++)
					{
						//strfillGrid = .CreateElement(((BoundColumn)dgRecords.Columns[i]).DataField);						
						strfillGrid = strfillGrid + Cms.BusinessLayer.BlCommon.ClsCommon.DecodeXMLCharacters(dgi.Cells[i].Text.Trim()) + "~";
						//parentNode.AppendChild(xNode);
					}
					//xmlDoc.DocumentElement.PrependChild(parentNode);
				}
			}
			if(strfillGrid != null)
			{
				strfillGrid=strfillGrid.TrimEnd('~');
			}
			strReturn=strfillGrid;
			return strReturn;

		}
		#endregion

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			//XmlDocument xmlDoc = new XmlDocument();	
			try
			{
				string FillDataGrid = null;
				string strScript="";
				switch(hidCommisionType.Value)
				{
					case "R":
						FillDataGrid = GetDataGridNodes(dgrRegularCommission,FillDataGrid);
						if(FillDataGrid!= null)
						{
							hidFillgrid.Value= FillDataGrid;
							strScript = @"<script language='javascript'>" + 
								"fillRegularCommissionOpener();" +
								"window.close();" + 
								"</script>" 
								;
						}
						else
						{
							strScript = @"<script language='javascript'>" + 
								"alert('Please select record');" +
								"</script>" 
								;
						}
						break;
					case "A" :
						FillDataGrid = GetDataGridNodes(dgrAdditionalCommission,FillDataGrid);
						if(FillDataGrid!= null)
						{
							hidFillgrid.Value= FillDataGrid;
							//string strScript="";
							strScript = @"<script language='javascript'>" + 
								"fillAdditionalCommissionOpener();" +
								"window.close();" + 
								"</script>" 
								;
						}
						else
						{
							strScript = @"<script language='javascript'>" + 
								"alert('Please select record');" +
								"</script>" 
								;
						}
						break;
					case "P" :
						FillDataGrid = GetDataGridNodes(dgrPropertyInspection,FillDataGrid);
						if(FillDataGrid!= null)
						{
							hidFillgrid.Value= FillDataGrid;
							//string strScript="";
							strScript = @"<script language='javascript'>" + 
								"fillPropertyInspectionOpener();" +
								"window.close();" + 
								"</script>" 
								;
						}
						else
						{
							strScript = @"<script language='javascript'>" + 
								"alert('Please select record');" +
								"</script>" 
								;
						}
						break;
					case "C" :
						FillDataGrid = GetDataGridNodes(dgrCompleteApp,FillDataGrid);
						if(FillDataGrid!= null)
						{
							hidFillgrid.Value= FillDataGrid;
							//string strScript="";
							strScript = @"<script language='javascript'>" + 
								"fillCompleteAppOpener();" +
								"window.close();" + 
								"</script>" 
								;
						}
						else
						{
							strScript = @"<script language='javascript'>" + 
								"alert('Please select record');" +
								"</script>" 
								;
						}
						break;
					default:
						return;
				}
				//				Response.Write(strScript);
				if (!ClientScript.IsStartupScriptRegistered("Refresh"))
				{
					ClientScript.RegisterStartupScript(this.GetType(),"Refresh",strScript);
				}
				//
				//				if(intRetVal>0)
				//				{
				//					hidFormSaved.Value = "1";
				//					string strScript="";
				//					strScript = @"<script language='javascript'>" + 
				//						"window.opener.RefreshWebgrid('');" +
				//						"window.close();" + 
				//						"</script>" 
				//						;
				//				
				//					if (!Page.IsStartupScriptRegistered("Refresh"))
				//					{
				//						Page.RegisterStartupScript("Refresh",strScript);
				//					}
				//
				//				}
			}
			catch(Exception ex)
			{
				gIntSaved=0;
				lblMessage.Text=ex.Message;
				lblMessage.Visible=true;			
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
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
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void GetQueryStringValues()
		{
			if (Request.QueryString["COMMISSION_TYPE"] != null && Request.QueryString["COMMISSION_TYPE"].ToString() != "")
			{ 						
				hidCommisionType.Value=Request.QueryString["COMMISSION_TYPE"].ToString(); 
			}				
			
		}

		
	}
}
